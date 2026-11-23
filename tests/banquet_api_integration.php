<?php
/**
 * End-to-end check of api/banquet_api.php against a real MySQL.
 *   php tests/banquet_api_integration.php http://host/api/banquet_api.php
 * Uses a random group key, so it never touches an existing group.
 */

$url = $argv[1] ?? 'http://localhost/api/banquet_api.php';
$key = 'itest-' . bin2hex(random_bytes(6));
$failures = 0;
$passes = 0;

function check(bool $cond, string $label): void {
    global $failures, $passes;
    if ($cond) { $passes++; return; }
    $failures++;
    $bt = debug_backtrace()[0];
    fwrite(STDERR, "FAIL line {$bt['line']}: $label\n");
}

function call(string $action, array $body = []): array {
    global $url, $key;
    $body['action'] = $action;
    $body['key'] = $key;
    $ctx = stream_context_create(['http' => [
        'method' => 'POST', 'header' => "Content-Type: application/json\r\n",
        'content' => json_encode($body), 'ignore_errors' => true, 'timeout' => 20,
    ]]);
    $raw = file_get_contents($url, false, $ctx);
    $json = json_decode((string)$raw, true);
    if (!is_array($json)) {
        fwrite(STDERR, "Non-JSON response to $action: $raw\n");
        exit(2);
    }
    return $json;
}

$GAME = 1_800_000_000;    // game clock deliberately far from the API clock
$SALT = 5;

function sync(int $uid, array $villages, array $extra = []): array {
    global $GAME;
    return call('sync', array_merge([
        'player' => ['user_id' => $uid, 'name' => "Player$uid", 'world' => 'Test World', 'game_time' => $GAME,
            'craftsmanship' => 8, 'sec_per_tile' => 12.5, 'carry' => array_fill(0, 8, 10), 'client_version' => 'itest'],
        'villages' => $villages,
    ], $extra));
}

function v(int $vid, array $o = []): array {
    global $GAME;
    return array_merge(['village_id' => $vid, 'name' => "V$vid", 'x' => $vid, 'y' => 0, 'has_hall' => true,
        'hall_cap' => 2700, 'levels' => array_fill(0, 8, 0), 'prod' => array_fill(0, 8, 0), 'buildings' => array_fill(0, 8, 0),
        'levels_at_game' => $GAME, 'snapshot_at_game' => $GAME, 'merchants_free' => 500], $o);
}

// ── Validation ──────────────────────────────────────────────────────────────
$r = call('sync', ['key' => 'short']);
check($r['ok'] === false, 'short key rejected');
$r = call('get_state');
check($r['ok'] === true && $r['exists'] === false, 'unknown group reports exists=false without creating it');
$r = call('bogus');
check($r['ok'] === false && strpos($r['error'], 'Unknown action') === 0, 'unknown action rejected');

// ── Mode off: players join, nothing is planned ──────────────────────────────
$p1Villages = [v(100, ['levels' => [0, 0, 0, 0, 0, 2700, 0, 0], 'prod' => [0, 0, 0, 0, 0, 600, 0, 0],
    'buildings' => [0, 0, 0, 0, 0, 2, 0, 0]])];
$p2Villages = [v(200, ['levels' => [0, 0, 0, 0, 0, 1000, 0, 0]]), v(201)];
$r = sync(1, $p1Villages);
check($r['ok'] === true && $r['mode'] === 'off' && $r['orders'] === [], 'first sync creates group, mode off');
$r = sync(2, $p2Villages);
check($r['players_online'] === 2, 'two players online');

// ── Website switches to autofill ────────────────────────────────────────────
$r = call('set_settings', ['settings' => ['mode' => 'autofill', 'keep_amount' => 0, 'safety_margin' => 0,
    'min_send' => 10, 'player_caps' => ['2' => 2000]]]);
check($r['ok'] === true && $r['settings']['categories']['banquet']['mode'] === 'autofill', 'legacy flat settings saved as banquet');
check($r['settings']['categories']['resources']['mode'] === 'off', 'other categories stay off');
check($r['settings']['categories']['banquet']['player_caps'] == ['2' => 2000], 'caps round-trip');

$r = sync(1, $p1Villages);
$to = [200 => 0, 201 => 0];
foreach ($r['orders'] as $o) {
    check($o['good'] === $SALT && $o['from_village_id'] === 100, 'order is salt from the producer');
    check($o['lease_remaining_sec'] > 100, 'lease remaining reported relative to API time');
    $to[$o['to_village_id']] += $o['amount'];
}
// The donor only has 2700: the emptier village (201, capped at 2000) is served
// first, village 200 (1000/2000) gets what is left.
check($to[201] === 2000, 'emptiest village filled first, to the 2000 player cap');
check($to[200] === 700, 'remaining donor stock goes to the next village');
check($to[200] + $to[201] === 2700, 'donor never over-committed');
$orders = $r['orders'];

// A second sync before any result must not add orders.
$r = sync(1, $p1Villages);
check(count($r['orders']) === count($orders), 'leases stop duplicate orders on re-sync');

// ── Results: first order sent, second rejected ──────────────────────────────
$sent = $orders[0];
$rej  = $orders[1];
$eta  = $GAME + 900;
$r = sync(1, $p1Villages, [
    'results' => [
        ['order_id' => $sent['order_id'], 'outcome' => 'sent', 'trader_id' => 9001, 'amount' => $sent['amount'], 'eta_game' => $eta],
        ['order_id' => $rej['order_id'], 'outcome' => 'rejected', 'error' => 'Not enough traders'],
    ],
    'outbound' => [
        ['trader_id' => 9001, 'from_village_id' => 100, 'to_village_id' => $sent['to_village_id'], 'good' => $SALT,
         'amount' => $sent['amount'], 'eta_game' => $eta],
        // A manual send the planner didn't make: must be adopted.
        ['trader_id' => 9002, 'from_village_id' => 100, 'to_village_id' => 201, 'good' => 0, 'amount' => 40, 'eta_game' => $eta],
    ],
]);
check($r['ok'] === true && $r['in_flight'] === 2, 'sent order + adopted manual send are in flight');
$ids = array_column($r['orders'], 'order_id');
check(!in_array($sent['order_id'], $ids, true), 'sent order no longer offered');
// The rejected order's goods are needed again, so a fresh lease replaces it.
check(!in_array($rej['order_id'], $ids, true), 'rejected order not re-offered under the same id');

$state = call('get_state');
check($state['exists'] === true && count($state['players']) === 2, 'state lists players');
check(count($state['villages']) === 3, 'state lists villages');
$byId = [];
foreach ($state['active'] as $s) $byId[$s['id']] = $s;
check(isset($byId[$sent['order_id']]) && $byId[$sent['order_id']]['status'] === 'in_flight', 'ledger shows the send');
check($byId[$sent['order_id']]['eta_in_sec'] === 900, 'eta shown in game-clock seconds');
$failedSeen = false;
foreach ($state['recent'] as $s) if ($s['id'] === $rej['order_id'] && $s['status'] === 'failed') $failedSeen = true;
check($failedSeen, 'rejected order in recent history');
check($state['grid']['100'][$SALT]['skip'] === 'produces it', 'grid skip reason for producer');
check($state['grid']['201'][0]['inbound'] === 40, 'grid counts adopted venison inbound');

// ── Arrival without fresh data: refresh requested, still counting ───────────
$GAME = $eta + 400;
$r = sync(2, $p2Villages);   // p2 still reports its old snapshot
$refreshIds = array_column($r['refresh'], 'village_id');
check(in_array($sent['to_village_id'], $refreshIds, true), 'receiver asked to refresh the arrived village');
check($r['in_flight'] === 2, 'still counting without a fresh snapshot');

// ── Receiver refreshes: shipment settles ────────────────────────────────────
$fresh = $p2Villages;
foreach ($fresh as &$fv) {
    $fv['snapshot_at_game'] = $GAME;
    $fv['levels_at_game'] = $GAME;
    if ($fv['village_id'] === $sent['to_village_id']) $fv['levels'][$SALT] += $sent['amount'];
    if ($fv['village_id'] === 201) $fv['levels'][0] += 40;
}
unset($fv);
$r = sync(2, $fresh);
check($r['in_flight'] === 0 && $r['refresh'] === [], 'both shipments settled after the fresh snapshot');

// ── Clear + remove ──────────────────────────────────────────────────────────
$GAME += 10;
sync(1, $p1Villages);
$r = call('clear_shipments', ['all' => true]);
check($r['ok'] === true && $r['cleared'] >= 1, 'clear_shipments cancels open rows');

$r = call('remove_player', ['user_id' => 2]);
$state = call('get_state');
check(count($state['players']) === 1 && count($state['villages']) === 1, 'remove_player drops player and villages');

// A village the client hasn't downloaded (listed in all_village_ids only) keeps its old report.
sync(1, [v(101)], ['all_village_ids' => [100, 101]]);
$state = call('get_state');
check(count($state['villages']) === 2, 'undownloaded village kept via all_village_ids');

// Village set replacement: player 1 loses village 100.
sync(1, [v(101)]);
$state = call('get_state');
check(count($state['villages']) === 1 && $state['villages'][0]['village_id'] === 101, 'sync replaces the village set');

$r = call('leave', ['user_id' => 1]);
$state = call('get_state');
check($r['ok'] === true && $state['players'][0]['online'] === false, 'leave marks the player offline');

// ── Every good: a bot that reports caps/storage/client ──────────────────────
$WOOD = 8;
$ALL = ['hall' => true, 'stockpile' => true, 'granary' => true, 'inn' => true, 'armoury' => true];

function sync_all(int $uid, array $villages, array $client = []): array {
    global $GAME;
    $c = [];
    foreach (['banquet', 'resources', 'food', 'weapons'] as $cat) $c[$cat] = $client[$cat] ?? ['enabled' => true, 'reserve' => 0];
    return call('sync', [
        'player' => ['user_id' => $uid, 'name' => "Player$uid", 'world' => 'Test World', 'game_time' => $GAME,
            'craftsmanship' => 8, 'sec_per_tile' => 12.5, 'carry' => array_fill(0, 24, 10), 'client_version' => 'itest',
            'client' => $c],
        'villages' => $villages,
    ]);
}

function va(int $vid, array $o = []): array {
    global $GAME, $ALL;
    return array_merge(['village_id' => $vid, 'name' => "V$vid", 'x' => $vid, 'y' => 0, 'has_hall' => true,
        'hall_cap' => 2700, 'levels' => array_fill(0, 24, 0), 'prod' => array_fill(0, 24, 0),
        'buildings' => array_fill(0, 24, 0), 'caps' => array_fill(0, 24, 3000), 'storage' => $ALL,
        'levels_at_game' => $GAME, 'snapshot_at_game' => $GAME, 'merchants_free' => 500, 'merchants_home' => 500], $o);
}

$GAME += 10;
$lv = array_fill(0, 24, 0); $lv[$WOOD] = 2500;
$bd = array_fill(0, 24, 0); $bd[$WOOD] = 1;
$p3 = [va(300, ['levels' => $lv, 'buildings' => $bd])];
$p4 = [va(400), va(401, ['storage' => array_merge($ALL, ['stockpile' => false])])];
sync_all(3, $p3);
sync_all(4, $p4);
// Player 1 (older bot, village 101) stays in the group.

$r = call('set_settings', ['settings' => ['categories' => [
    'banquet'   => ['mode' => 'off'],
    'resources' => ['mode' => 'autofill', 'keep_amount' => 0, 'safety_margin' => 0, 'min_send' => 10],
]]]);
check($r['ok'] === true && $r['settings']['categories']['resources']['mode'] === 'autofill', 'category settings saved');

$r = sync_all(3, $p3);
check($r['mode'] === 'off' && $r['modes']['resources'] === 'autofill', 'sync returns every category mode');
$woodTo = [];
foreach ($r['orders'] as $o) {
    check($o['good'] === $WOOD && $o['from_village_id'] === 300, 'wood order from the producer');
    $woodTo[$o['to_village_id']] = ($woodTo[$o['to_village_id']] ?? 0) + $o['amount'];
}
check(($woodTo[400] ?? 0) === 2500, 'wood goes to the village with a stockpile');
check(!isset($woodTo[401]) && !isset($woodTo[101]), 'no stockpile / older bot: no wood');

$state = call('get_state');
check(count($state['goods']) === 24 && $state['goods'][8]['name'] === 'Wood' && $state['goods'][8]['category'] === 'resources',
    'goods catalogue in state');
check(count($state['categories']) === 4 && $state['categories'][1]['id'] === 'resources', 'categories in state');
$vs = [];
foreach ($state['villages'] as $sv) $vs[$sv['village_id']] = $sv;
check($vs[400]['caps'][$WOOD] === 3000 && $vs[400]['storage']['stockpile'] === true, 'village caps and storage in state');
check($vs[101]['storage'] === null && $vs[101]['caps'][0] === 2700, 'older bot: no storage, hall cap as banquet cap');
check($state['grid']['401'][$WOOD]['skip'] === 'no stockpile', 'grid: no stockpile');
check($state['grid']['101'][$WOOD]['skip'] === 'bot too old', 'grid: bot too old');
$cats = array_unique(array_column($state['active'], 'category'));
check(in_array('resources', $cats, true), 'shipments carry their category');
$p = [];
foreach ($state['players'] as $sp) $p[$sp['user_id']] = $sp;
check($p[3]['client']['resources']['enabled'] === true && $p[1]['client'] === null, 'player client settings in state');

// Switching a category off cancels its open orders without the cancel flag.
$r = call('set_settings', ['settings' => ['categories' => ['resources' => ['mode' => 'off']]]]);
$state = call('get_state');
$leasedWood = 0;
foreach ($state['active'] as $s) if ($s['status'] === 'leased' && $s['good'] === $WOOD) $leasedWood++;
check($leasedWood === 0, 'category switched off: its leases cancelled');

echo "$passes passed, $failures failed\n";
exit($failures > 0 ? 1 : 0);
