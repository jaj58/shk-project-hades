<?php
/**
 * CLI tests for api/banquet_planner.php. No database needed.
 *   php tests/banquet_planner_test.php
 */

require __DIR__ . '/../api/banquet_planner.php';

$failures = 0;
$passes = 0;

function check(bool $cond, string $label): void {
    global $failures, $passes;
    if ($cond) { $passes++; return; }
    $failures++;
    $bt = debug_backtrace()[0];
    fwrite(STDERR, "FAIL line {$bt['line']}: $label\n");
}

function player(int $uid, array $o = []): array {
    return array_merge([
        'user_id' => $uid, 'name' => "P$uid", 'last_seen' => 1000,
        'craftsmanship' => 8, 'sec_per_tile' => 10.0, 'carry' => array_fill(0, 8, 10),
    ], $o);
}

function village(int $vid, int $uid, array $o = []): array {
    return array_merge([
        'village_id' => $vid, 'user_id' => $uid, 'name' => "V$vid", 'x' => $vid, 'y' => 0,
        'has_hall' => true, 'hall_cap' => 2700,
        'levels' => array_fill(0, 8, 0), 'prod' => array_fill(0, 8, 0.0), 'buildings' => array_fill(0, 8, 0),
        'levels_at_game' => 50000, 'snapshot_at_game' => 50000, 'merchants_free' => 1000,
    ], $o);
}

function settings(array $o = []): array {
    return bq_normalize_settings(array_merge(['mode' => 'autofill', 'keep_amount' => 0,
        'safety_margin' => 0, 'min_send' => 10], $o));
}

function sum_to(array $rows, int $vid, int $good): int {
    $t = 0;
    foreach ($rows as $r) if ((int)$r['to_village_id'] === $vid && (int)$r['good'] === $good) $t += $r['amount'];
    return $t;
}

// Assign fake ids to planner rows the way the DB layer would.
function persist(array &$shipments): void {
    static $next = 1;
    foreach ($shipments as &$s) {
        if ($s['id'] === null) $s['id'] = $next++;
        $s['dirty'] = false;
    }
    unset($s);
}

$API_NOW = 1000;
$GAME_NOW = 50000;

// ── Settings normalisation ──────────────────────────────────────────────────
$s = bq_normalize_settings(['mode' => 'nonsense', 'min_send' => -5, 'lease_seconds' => 99999,
    'player_caps' => ['7' => 5400, 'x' => 1, '8' => 0], 'goods_enabled' => [true, false]]);
check($s['mode'] === 'off', 'invalid mode falls back to off');
check($s['min_send'] === 1, 'min_send clamped up');
check($s['lease_seconds'] === 900, 'lease clamped down');
check((array)$s['player_caps'] === ['7' => 5400], 'caps keep only positive numeric entries');
check($s['goods_enabled'][0] === true && $s['goods_enabled'][1] === false && $s['goods_enabled'][2] === false,
    'goods_enabled missing entries become false');
check(json_encode(bq_normalize_settings([])['player_caps']) === '{}', 'empty caps encode as an object');

// ── Projection ──────────────────────────────────────────────────────────────
$v = village(1, 1, ['levels' => [100, 0, 0, 0, 0, 0, 0, 0], 'prod' => [864, 0, 0, 0, 0, 0, 0, 0]]);
check(abs(bq_projected_level($v, 0, 50000 + 8640) - 186.4) < 0.001, 'production extrapolates per day');
$v['levels'][0] = 2690;
check(bq_projected_level($v, 0, 50000 + 86400) === 2700.0, 'projection stops at the hall cap');

// ── Effective cap never exceeds the reported cap ────────────────────────────
$s = settings(['player_caps' => ['1' => 5400], 'village_caps' => ['2' => 1500]]);
check(bq_effective_cap($s, village(1, 1)) === 2700, 'player override above reported cap is clamped');
check(bq_effective_cap($s, village(2, 1)) === 1500, 'village override wins over player override');
check(bq_effective_cap($s, village(1, 1, ['hall_cap' => 5400])) === 5400, 'card-doubled cap honoured');

// ── Autofill: producers feed non-producers, never above cap ─────────────────
$players = [1 => player(1), 2 => player(2)];
$salt = 5;
$villages = [
    10 => village(10, 1, ['levels' => [0, 0, 0, 0, 0, 2700, 0, 0], 'prod' => [0, 0, 0, 0, 0, 500, 0, 0],
        'buildings' => [0, 0, 0, 0, 0, 1, 0, 0]]),
    20 => village(20, 2, ['levels' => [0, 0, 0, 0, 0, 1000, 0, 0]]),
    21 => village(21, 2, ['levels' => [0, 0, 0, 0, 0, 2650, 0, 0]]),
];
$ship = [];
$created = bq_plan($ship, settings(), $players, $villages, $API_NOW, $GAME_NOW);
check(sum_to($created, 20, $salt) === 1700, 'fills the gap exactly (1000 -> 2700)');
check(sum_to($created, 21, $salt) === 50, 'small final top-up allowed when it closes the gap');
check(sum_to($created, 10, $salt) === 0, 'producer is not a receiver of its own good');
foreach ($created as $c) check($c['good'] === $salt && $c['from_village_id'] === 10, 'only salt from the producer');

// Replanning with the leases in place must not create more.
persist($ship);
$again = bq_plan($ship, settings(), $players, $villages, $API_NOW + 5, $GAME_NOW + 5);
check(count($again) === 0, 'open leases count against the shortfall (no double orders)');

// A full producer whose production rate reads 0 (stalled at cap) is still a producer.
$vStalled = $villages; $vStalled[10]['prod'] = array_fill(0, 8, 0.0);
$ship = [];
check(sum_to(bq_plan($ship, settings(), $players, $vStalled, $API_NOW, $GAME_NOW), 20, $salt) === 1700,
    'stalled producer (rate 0, has building) still donates');
$ship = [];
check(count(bq_plan($ship, settings(['producer_min_buildings' => 2]), $players, $villages, $API_NOW, $GAME_NOW)) === 0,
    'producer_min_buildings raises the bar');

// ── Keep amount and merchant limits ─────────────────────────────────────────
$villages[10]['merchants_free'] = 50;   // 50 * carry 10 = 500 per round
$ship = [];
$created = bq_plan($ship, settings(['keep_amount' => 2000]), $players, $villages, $API_NOW, $GAME_NOW);
$total = 0; $merch = 0;
foreach ($created as $c) { $total += $c['amount']; $merch += $c['merchants']; }
check($total <= 500, 'never more than merchants * carry');
check($merch <= 50, 'merchant count respected across orders');
$ship = [];
$created = bq_plan($ship, settings(['keep_amount' => 2695]), $players, $villages, $API_NOW, $GAME_NOW);
check(count($created) === 0, 'donor below keep + min_send gives nothing');
$villages[10]['merchants_free'] = 1000;

// Whole merchant loads unless closing the gap: carry 30, 100 available -> 90.
$pl30 = [1 => player(1, ['carry' => array_fill(0, 8, 30)]), 2 => player(2)];
$ship = [];
$created = bq_plan($ship, settings(['keep_amount' => 2600]), $pl30, $villages, $API_NOW, $GAME_NOW);
check(count($created) === 1 && $created[0]['amount'] === 90 && $created[0]['merchants'] === 3,
    'partial availability rounds down to whole loads');

// ── Offline donors give nothing; offline receivers still get filled ─────────
$playersOff = [1 => player(1, ['last_seen' => 1]), 2 => player(2, ['last_seen' => 1])];
$ship = [];
check(count(bq_plan($ship, settings(), $playersOff, $villages, $API_NOW, $GAME_NOW)) === 0, 'offline donor ignored');
$playersRecvOff = [1 => player(1), 2 => player(2, ['last_seen' => 1])];
$ship = [];
check(sum_to(bq_plan($ship, settings(), $playersRecvOff, $villages, $API_NOW, $GAME_NOW), 20, $salt) === 1700,
    'offline receiver still filled from extrapolated levels');

// ── Craftsmanship gate + send_unusable_goods ────────────────────────────────
$playersLow = [1 => player(1), 2 => player(2, ['craftsmanship' => 5])];  // salt is index 5 -> locked
$ship = [];
check(count(bq_plan($ship, settings(), $playersLow, $villages, $API_NOW, $GAME_NOW)) === 0,
    'good above craftsmanship is skipped by default');
$ship = [];
check(sum_to(bq_plan($ship, settings(['send_unusable_goods' => true]), $playersLow, $villages, $API_NOW, $GAME_NOW), 20, $salt) === 1700,
    'send_unusable_goods sends it anyway');

// ── No village hall / own villages / paused ─────────────────────────────────
$vNoHall = $villages; $vNoHall[20]['has_hall'] = false;
$ship = [];
check(sum_to(bq_plan($ship, settings(), $players, $vNoHall, $API_NOW, $GAME_NOW), 20, $salt) === 0, 'no hall, no delivery');

$vOwn = $villages; $vOwn[20]['user_id'] = 1; $vOwn[21]['user_id'] = 1;
$ship = [];
check(count(bq_plan($ship, settings(['include_own_villages' => false]), $players, $vOwn, $API_NOW, $GAME_NOW)) === 0,
    'include_own_villages=false blocks same-player sends');

$ship = [];
check(count(bq_plan($ship, settings(['paused_players' => [2]]), $players, $villages, $API_NOW, $GAME_NOW)) === 0,
    'paused receiver is skipped');

// ── Focus mode ──────────────────────────────────────────────────────────────
$villagesF = [
    10 => village(10, 1, ['levels' => array_fill(0, 8, 3500), 'hall_cap' => 5400, 'merchants_free' => 5000]),
    20 => village(20, 2, ['levels' => [2700, 0, 0, 0, 0, 0, 0, 0]]),
    30 => village(30, 3, ['levels' => [0, 0, 0, 0, 0, 0, 0, 0]]),
];
$playersF = [1 => player(1), 2 => player(2), 3 => player(3)];
$ship = [];
$created = bq_plan($ship, settings(['mode' => 'focus', 'focus_players' => [3], 'keep_amount' => 500,
    'max_orders_per_player' => 50]), $playersF, $villagesF, $API_NOW, $GAME_NOW);
for ($g = 0; $g < 8; $g++) check(sum_to($created, 30, $g) === 2700, "focus fills good $g to cap");
check(sum_to($created, 10, 0) === 0 && sum_to($created, 20, 1) === 0, 'non-focus villages receive nothing');
$venisonFrom20 = 0;
foreach ($created as $c) if ($c['from_village_id'] === 20 && $c['good'] === 0) $venisonFrom20 += $c['amount'];
check($venisonFrom20 <= 2200, 'focus donors keep their keep_amount');

// Nearest donor first: village 20 (x=20) is closer to 30 than village 10 (x=10).
$first = null;
foreach ($created as $c) if ($c['good'] === 0) { $first = $c; break; }
check($first !== null && $first['from_village_id'] === 20, 'nearest donor chosen first');

// ── Max orders per player ───────────────────────────────────────────────────
$ship = [];
$created = bq_plan($ship, settings(['mode' => 'focus', 'focus_players' => [3], 'max_orders_per_player' => 3]),
    $playersF, $villagesF, $API_NOW, $GAME_NOW);
$per = [];
foreach ($created as $c) $per[$c['from_user_id']] = ($per[$c['from_user_id']] ?? 0) + 1;
check(max($per) <= 3, 'max_orders_per_player respected');

// ── Full lifecycle: lease -> sent -> arrived -> settled ─────────────────────
$st = settings(['settle_buffer_seconds' => 300]);
$ship = [];
bq_plan($ship, $st, $players, $villages, $API_NOW, $GAME_NOW);
persist($ship);
$order = null;
foreach ($ship as $s) if ($s['to_village_id'] === 20) { $order = $s; break; }
$eta = $GAME_NOW + 600;

bq_apply_results($ship, [['order_id' => $order['id'], 'outcome' => 'sent', 'trader_id' => 777,
    'amount' => $order['amount'], 'eta_game' => $eta]], 1);
$row = null;
foreach ($ship as $s) if ($s['id'] === $order['id']) $row = $s;
check($row['status'] === 'in_flight' && $row['trader_id'] === 777 && $row['eta_game'] === $eta, 'sent -> in_flight');

// Result from the wrong user is ignored.
$other = null;
foreach ($ship as $s) if ($s['status'] === 'leased') { $other = $s; break; }
bq_apply_results($ship, [['order_id' => $other['id'], 'outcome' => 'rejected']], 2);
foreach ($ship as $s) if ($s['id'] === $other['id']) check($s['status'] === 'leased', 'foreign result ignored');

// The same trader appearing in the outbound list must not be counted twice.
$before = count($ship);
bq_adopt_outbound($ship, [['trader_id' => 777, 'from_village_id' => 10, 'to_village_id' => 20, 'good' => $salt,
    'amount' => $order['amount'], 'eta_game' => $eta]], 1, $villages, $API_NOW);
check(count($ship) === $before, 'known trader not re-adopted');

// Arrival passes but the target hasn't downloaded since: still counted, refresh requested.
$later = $eta + 400;
bq_settle($ship, $villages, $st, $API_NOW + 1000, $later);
foreach ($ship as $s) if ($s['id'] === $order['id']) check($s['status'] === 'in_flight', 'not settled without a fresh snapshot');
$refresh = bq_refresh_requests($ship, $villages, $st, 2, $later);
check(count($refresh) === 1 && $refresh[0]['village_id'] === 20 && $refresh[0]['after_game'] === $eta + 300,
    'refresh requested for the target village');

// Snapshot taken after arrival but inside the buffer: still counted.
$vSnap = $villages; $vSnap[20]['snapshot_at_game'] = $eta + 100;
bq_settle($ship, $vSnap, $st, $API_NOW + 1000, $later);
foreach ($ship as $s) if ($s['id'] === $order['id']) check($s['status'] === 'in_flight', 'snapshot inside buffer does not settle');

// Snapshot past eta + buffer: settled.
$vSnap[20]['snapshot_at_game'] = $eta + 300;
$vSnap[20]['levels'][$salt] = 1000 + $order['amount'];
$vSnap[20]['levels_at_game'] = $eta + 300;
bq_settle($ship, $vSnap, $st, $API_NOW + 1000, $later);
foreach ($ship as $s) if ($s['id'] === $order['id']) check($s['status'] === 'settled', 'settles after fresh snapshot past buffer');

// ── The overfill scenario: stale report + arrived goods ─────────────────────
// Target had 1000, we sent 1700 (arrived), target hasn't re-downloaded. The planner
// must still believe 2700 is (about to be) there and send nothing more.
$vStale = [
    10 => village(10, 1, ['levels' => [0, 0, 0, 0, 0, 2700, 0, 0], 'prod' => [0, 0, 0, 0, 0, 500, 0, 0],
        'buildings' => [0, 0, 0, 0, 0, 1, 0, 0]]),
    // Last downloaded two hours ago, before the merchant arrived an hour ago.
    20 => village(20, 2, ['levels' => [0, 0, 0, 0, 0, 1000, 0, 0],
        'levels_at_game' => $GAME_NOW - 7200, 'snapshot_at_game' => $GAME_NOW - 7200]),
];
$ship = [bq_new_shipment(['id' => 900, 'status' => 'in_flight', 'from_user_id' => 1, 'from_village_id' => 10,
    'to_user_id' => 2, 'to_village_id' => 20, 'good' => $salt, 'amount' => 1700, 'trader_id' => 55,
    'eta_game' => $GAME_NOW - 3600], $API_NOW)];
bq_settle($ship, $vStale, $st, $API_NOW, $GAME_NOW);
check($ship[0]['status'] === 'in_flight', 'arrived-but-unseen shipment keeps counting');
check(count(bq_plan($ship, $st, $players, $vStale, $API_NOW, $GAME_NOW)) === 0, 'no overfill while unseen');

// ── Lease expiry and late results ───────────────────────────────────────────
$ship = [];
bq_plan($ship, $st, $players, $villages, $API_NOW, $GAME_NOW);
persist($ship);
bq_settle($ship, $villages, $st, $API_NOW + 121, $GAME_NOW + 121);
$allExpired = true;
foreach ($ship as $s) if ($s['status'] !== 'expired') $allExpired = false;
check($allExpired, 'leases expire after lease_seconds');
$playersLater = [1 => player(1, ['last_seen' => $API_NOW + 121]), 2 => player(2, ['last_seen' => $API_NOW + 121])];
$replanned = bq_plan($ship, $st, $playersLater, $villages, $API_NOW + 121, $GAME_NOW + 121);
check(sum_to($replanned, 20, $salt) === 1700, 'expired leases release the shortfall');
persist($ship);

// A late "sent" for an expired lease revives it, and the extra new lease is why
// adoption matters: the sender's outbound list stops the next plan double-sending.
$expiredRow = null;
foreach ($ship as $s) if ($s['status'] === 'expired' && $s['to_village_id'] === 20) { $expiredRow = $s; break; }
bq_apply_results($ship, [['order_id' => $expiredRow['id'], 'outcome' => 'sent', 'trader_id' => 4242,
    'eta_game' => $GAME_NOW + 900]], 1);
foreach ($ship as $s) if ($s['id'] === $expiredRow['id']) check($s['status'] === 'in_flight', 'late sent revives expired lease');

// ── Adoption of unknown merchants ───────────────────────────────────────────
$ship = [];
bq_adopt_outbound($ship, [
    ['trader_id' => 1, 'from_village_id' => 10, 'to_village_id' => 20, 'good' => $salt, 'amount' => 300, 'eta_game' => $GAME_NOW + 60],
    ['trader_id' => 2, 'from_village_id' => 10, 'to_village_id' => 99999, 'good' => $salt, 'amount' => 300, 'eta_game' => $GAME_NOW + 60],
], 1, $villages, $API_NOW);
check(count($ship) === 1 && $ship[0]['source'] === 'adopted' && $ship[0]['to_user_id'] === 2,
    'merchant to a group village adopted; to a non-group village ignored');
$created = bq_plan($ship, settings(), $players, $villages, $API_NOW, $GAME_NOW);
check(sum_to($created, 20, $salt) === 1400, 'adopted merchant reduces the shortfall');

// Adoption attaches to a matching lease instead of adding a duplicate row.
$ship = [];
bq_plan($ship, settings(), $players, $villages, $API_NOW, $GAME_NOW);
persist($ship);
$lease = null;
foreach ($ship as $s) if ($s['to_village_id'] === 20) { $lease = $s; break; }
$count = count($ship);
bq_adopt_outbound($ship, [['trader_id' => 31337, 'from_village_id' => 10, 'to_village_id' => 20, 'good' => $salt,
    'amount' => $lease['amount'], 'eta_game' => $GAME_NOW + 60]], 1, $villages, $API_NOW);
check(count($ship) === $count, 'adoption attaches to the lease');
foreach ($ship as $s) if ($s['id'] === $lease['id']) check($s['status'] === 'in_flight' && $s['trader_id'] === 31337, 'lease became in_flight');

// Result arriving after adoption created a separate row is dropped as a duplicate.
$ship = [
    bq_new_shipment(['id' => 1, 'status' => 'leased', 'from_user_id' => 1, 'from_village_id' => 10, 'to_user_id' => 2,
        'to_village_id' => 20, 'good' => $salt, 'amount' => 500, 'lease_until' => $API_NOW + 100], $API_NOW),
    bq_new_shipment(['id' => 2, 'status' => 'in_flight', 'source' => 'adopted', 'from_user_id' => 1, 'from_village_id' => 10,
        'to_user_id' => 2, 'to_village_id' => 21, 'good' => $salt, 'amount' => 500, 'trader_id' => 808], $API_NOW),
];
bq_apply_results($ship, [['order_id' => 1, 'outcome' => 'sent', 'trader_id' => 808]], 1);
check($ship[0]['status'] === 'cancelled', 'result for an already-adopted trader is dropped');

// ── Report age ──────────────────────────────────────────────────────────────
$vOld = $villages; $vOld[20]['levels_at_game'] = $GAME_NOW - 25 * 3600;
$ship = [];
check(sum_to(bq_plan($ship, settings(), $players, $vOld, $API_NOW, $GAME_NOW), 20, $salt) === 0, 'stale receiver skipped');

// ── Mode off ────────────────────────────────────────────────────────────────
$ship = [];
check(count(bq_plan($ship, settings(['mode' => 'off']), $players, $villages, $API_NOW, $GAME_NOW)) === 0, 'off plans nothing');

// ── Grid ────────────────────────────────────────────────────────────────────
$grid = bq_grid(settings(), $players, $villages, [], $GAME_NOW);
check($grid['20'][$salt]['shortfall'] === 1700 && $grid['10'][$salt]['skip'] === 'produces it', 'grid reports shortfall and skip reasons');

echo "$passes passed, $failures failed\n";
exit($failures > 0 ? 1 : 0);
