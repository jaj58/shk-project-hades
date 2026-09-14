<?php
/**
 * Banquet Good Sender — planning core.
 *
 * Pure functions only: no DB, no request/response, no clock reads. Every
 * function takes the times it needs as arguments so the ledger maths can be
 * exercised from tests/banquet_planner_test.php without a database.
 *
 * Shapes (plain arrays, keyed as noted):
 *
 *   $players[user_id]   = [user_id, name, last_seen, craftsmanship, sec_per_tile, carry[8]]
 *   $villages[vid]      = [village_id, user_id, name, x, y, has_hall, hall_cap,
 *                          levels[8], prod[8], buildings[8], levels_at_game, snapshot_at_game, merchants_free]
 *   $shipments[]        = [id|null, status, source, from_user_id, from_village_id, to_user_id,
 *                          to_village_id, good, amount, merchants, trader_id|null, eta_game|null,
 *                          lease_until|null, error|null, dirty(bool)]
 *
 * Two clocks exist. API time (PHP time()) drives leases and player liveness.
 * Game server time drives everything the client reads from the game: village
 * snapshots, level extrapolation and trader ETAs. A shipment settles by
 * comparing two GAME times (target snapshot vs ETA), so clock skew between the
 * API host and the game server can never settle a shipment early.
 */

if (isset($_SERVER['SCRIPT_FILENAME']) && realpath($_SERVER['SCRIPT_FILENAME']) === realpath(__FILE__)) {
    http_response_code(404);
    exit;
}

const BQ_NUM_GOODS = 8;
// Index order matches Banqueting.resourceLevels and the Craftsmanship unlock order.
const BQ_GOOD_NAMES = ['Venison', 'Furniture', 'Metalware', 'Clothes', 'Wine', 'Salt', 'Spices', 'Silk'];

const BQ_ACTIVE_STATUSES = ['leased', 'in_flight'];

function bq_default_settings(): array {
    return [
        // off | autofill | focus
        'mode'                       => 'off',
        // focus mode: every good is filled at these players' / villages' halls
        'focus_players'              => [],
        'focus_villages'             => [],
        'goods_enabled'              => array_fill(0, BQ_NUM_GOODS, true),
        // user_id => cap, village_id => cap. 0/absent = use the reported hall cap.
        // Always clamped to the reported cap: goods arriving over the cap are lost.
        'player_caps'                => (object)[],
        'village_caps'               => (object)[],
        // Donors never go below this many of a good.
        'keep_amount'                => 1000,
        // Leave this much headroom under the target so production during travel can't overflow.
        'safety_margin'              => 50,
        // Don't create orders smaller than this.
        'min_send'                   => 100,
        'max_merchants_per_order'    => 50,
        'max_orders_per_player'      => 10,
        // 0 = no limit
        'max_travel_minutes'         => 0,
        // A shipment keeps counting until the target reports a download taken this long after arrival.
        'settle_buffer_seconds'      => 300,
        'lease_seconds'              => 120,
        // autofill: a village "produces" a good when it has this many finished production buildings for it.
        // Building count, not rate: a full hall can stall production, and full producers are the best donors.
        'producer_min_buildings'     => 1,
        // autofill: also even out villages that make the same good (same fill % of cap),
        // once villages that don't make it have been served.
        'balance_goods'              => false,
        // Only rebalance when a village is more than this % of its cap off the balance level.
        'balance_tolerance_percent'  => 10,
        // Sends between two villages of the same player.
        'include_own_villages'       => true,
        // Send goods above the receiver's Craftsmanship level (storable but not banquetable).
        'send_unusable_goods'        => false,
        // Receivers whose last report is older than this are skipped (0 = never skip).
        'max_report_age_hours'       => 24,
        // user_ids excluded as both donor and receiver
        'paused_players'             => [],
    ];
}

/** Merge client-supplied settings over the defaults, clamping every value. */
function bq_normalize_settings($in): array {
    $d = bq_default_settings();
    if (!is_array($in)) return $d;

    $s = $d;
    if (isset($in['mode']) && in_array($in['mode'], ['off', 'autofill', 'focus'], true)) {
        $s['mode'] = $in['mode'];
    }
    $s['focus_players']  = bq_int_list($in['focus_players'] ?? []);
    $s['focus_villages'] = bq_int_list($in['focus_villages'] ?? []);
    $s['paused_players'] = bq_int_list($in['paused_players'] ?? []);

    if (isset($in['goods_enabled']) && is_array($in['goods_enabled'])) {
        for ($g = 0; $g < BQ_NUM_GOODS; $g++) {
            $s['goods_enabled'][$g] = !empty($in['goods_enabled'][$g]);
        }
    }

    $s['player_caps']  = bq_int_map($in['player_caps'] ?? []);
    $s['village_caps'] = bq_int_map($in['village_caps'] ?? []);

    $ints = [
        'keep_amount'                => [0, 1000000],
        'safety_margin'              => [0, 100000],
        'min_send'                   => [1, 100000],
        'max_merchants_per_order'    => [1, 1000],
        'max_orders_per_player'      => [1, 100],
        'max_travel_minutes'         => [0, 10080],
        'settle_buffer_seconds'      => [60, 86400],
        'lease_seconds'              => [30, 900],
        'producer_min_buildings'     => [1, 100],
        'max_report_age_hours'       => [0, 720],
        'balance_tolerance_percent'  => [1, 50],
    ];
    foreach ($ints as $k => [$min, $max]) {
        if (isset($in[$k]) && is_numeric($in[$k])) {
            $s[$k] = max($min, min($max, (int)$in[$k]));
        }
    }
    foreach (['include_own_villages', 'send_unusable_goods', 'balance_goods'] as $k) {
        if (array_key_exists($k, $in)) $s[$k] = (bool)$in[$k];
    }
    return $s;
}

function bq_int_list($v): array {
    if (!is_array($v)) return [];
    $out = [];
    foreach ($v as $x) {
        if (is_numeric($x)) $out[] = (int)$x;
    }
    return array_values(array_unique($out));
}

/** Maps keyed by id -> positive int. Returned as an object so json_encode keeps "{}" when empty. */
function bq_int_map($v) {
    $out = [];
    if (is_array($v) || is_object($v)) {
        foreach ((array)$v as $k => $x) {
            if (is_numeric($k) && is_numeric($x) && (int)$x > 0) $out[(string)(int)$k] = (int)$x;
        }
    }
    return (object)$out;
}

/** Best estimate of the game server's current time, from the offset measured at the last sync. */
function bq_game_now(int $api_now, int $game_offset_sec): int {
    return $api_now + $game_offset_sec;
}

function bq_player_online(array $player, int $api_now, int $ttl = 90): bool {
    return ($api_now - (int)$player['last_seen']) <= $ttl;
}

/** The fill target for a village: override if set, never above the reported cap. */
function bq_effective_cap(array $settings, array $village): int {
    $cap = (int)$village['hall_cap'];
    $vc  = (array)$settings['village_caps'];
    $pc  = (array)$settings['player_caps'];
    $vid = (string)$village['village_id'];
    $uid = (string)$village['user_id'];
    if (!empty($vc[$vid]))      $cap = min($cap, (int)$vc[$vid]);
    elseif (!empty($pc[$uid]))  $cap = min($cap, (int)$pc[$uid]);
    return max(0, $cap);
}

/**
 * The village's level of a good right now, from its last report plus production
 * since then. Never above the real hall cap (production stops there). Goods that
 * arrived after the village's last download are NOT in here — they're counted
 * through the shipment ledger instead.
 */
function bq_projected_level(array $village, int $good, int $game_now): float {
    $level = (float)($village['levels'][$good] ?? 0);
    $age   = max(0, $game_now - (int)$village['levels_at_game']);
    $level += ((float)($village['prod'][$good] ?? 0)) * $age / 86400.0;
    $cap = (int)$village['hall_cap'];
    if ($cap > 0 && $level > $cap) $level = (float)$cap;
    return max(0.0, $level);
}

function bq_new_shipment(array $fields, int $api_now): array {
    return array_merge([
        'id'              => null,
        'status'          => 'leased',
        'source'          => 'planner',
        'from_user_id'    => 0,
        'from_village_id' => 0,
        'to_user_id'      => 0,
        'to_village_id'   => 0,
        'good'            => 0,
        'amount'          => 0,
        'merchants'       => 0,
        'trader_id'       => null,
        'eta_game'        => null,
        'lease_until'     => null,
        'error'           => null,
        'created_at'      => $api_now,
        'dirty'           => true,
    ], $fields);
}

/**
 * Apply the sending client's order outcomes.
 *   $results[] = [order_id, outcome: sent|rejected|skipped, amount?, trader_id?, eta_game?, error?]
 * Only shipments owned by $user_id are touched.
 */
function bq_apply_results(array &$shipments, array $results, int $user_id): void {
    $byId = [];
    foreach ($shipments as $i => $s) {
        if ($s['id'] !== null) $byId[(int)$s['id']] = $i;
    }

    foreach ($results as $r) {
        $oid = (int)($r['order_id'] ?? 0);
        if (!isset($byId[$oid])) continue;
        $i = $byId[$oid];
        $s = &$shipments[$i];
        if ((int)$s['from_user_id'] !== $user_id) { unset($s); continue; }
        // A late result for a lease that already expired still wins: the goods may be on the road.
        if (!in_array($s['status'], ['leased', 'expired'], true)) { unset($s); continue; }

        $outcome = $r['outcome'] ?? '';
        if ($outcome === 'sent') {
            $traderId = isset($r['trader_id']) && (int)$r['trader_id'] > 0 ? (int)$r['trader_id'] : null;
            if ($traderId !== null && bq_find_trader($shipments, $traderId, $i) >= 0) {
                // Already adopted from the trader list in an earlier sync — don't count twice.
                $s['status'] = 'cancelled';
                $s['error']  = 'duplicate of adopted trader ' . $traderId;
            } else {
                $s['status']    = 'in_flight';
                $s['trader_id'] = $traderId;
                if (isset($r['amount']) && (int)$r['amount'] > 0) $s['amount'] = (int)$r['amount'];
                if (isset($r['eta_game']) && (int)$r['eta_game'] > 0) $s['eta_game'] = (int)$r['eta_game'];
            }
        } elseif ($outcome === 'rejected') {
            $s['status'] = 'failed';
            $s['error']  = substr((string)($r['error'] ?? 'rejected'), 0, 255);
        } elseif ($outcome === 'skipped') {
            $s['status'] = 'cancelled';
            $s['error']  = substr((string)($r['error'] ?? 'skipped by client'), 0, 255);
        } else {
            unset($s);
            continue;
        }
        $s['lease_until'] = null;
        $s['dirty'] = true;
        unset($s);
    }
}

function bq_find_trader(array $shipments, int $trader_id, int $exclude_index = -1): int {
    foreach ($shipments as $i => $s) {
        if ($i === $exclude_index) continue;
        if ($s['trader_id'] !== null && (int)$s['trader_id'] === $trader_id
            && in_array($s['status'], ['in_flight', 'settled'], true)) {
            return $i;
        }
    }
    return -1;
}

/**
 * Reconcile the sender's live outbound merchants into the ledger. This is what
 * makes the ledger robust to lost HTTP responses and to sends made outside the
 * planner (manual sends, the Trade module's player routes): any merchant heading
 * to a group village counts as in flight.
 *   $outbound[] = [trader_id, from_village_id, to_village_id, good, amount, eta_game]
 */
function bq_adopt_outbound(array &$shipments, array $outbound, int $user_id, array $villages, int $api_now): void {
    foreach ($outbound as $t) {
        $traderId = (int)($t['trader_id'] ?? 0);
        $to       = (int)($t['to_village_id'] ?? 0);
        $from     = (int)($t['from_village_id'] ?? 0);
        $good     = (int)($t['good'] ?? -1);
        $amount   = (int)($t['amount'] ?? 0);
        $eta      = (int)($t['eta_game'] ?? 0);
        if ($traderId <= 0 || $amount <= 0 || $good < 0 || $good >= BQ_NUM_GOODS) continue;
        if (!isset($villages[$to])) continue;             // not heading to a group village

        $known = bq_find_trader($shipments, $traderId);
        if ($known >= 0) {
            if ($eta > 0 && (int)$shipments[$known]['eta_game'] !== $eta) {
                $shipments[$known]['eta_game'] = $eta;
                $shipments[$known]['dirty'] = true;
            }
            continue;
        }

        // Attach to the planner row this merchant most likely came from: same route and
        // good, not yet tied to a trader. Prefer an exact amount match.
        $match = -1;
        foreach ($shipments as $i => $s) {
            if ($s['trader_id'] !== null) continue;
            if (!in_array($s['status'], ['leased', 'expired', 'in_flight'], true)) continue;
            if ((int)$s['from_village_id'] !== $from || (int)$s['to_village_id'] !== $to
                || (int)$s['good'] !== $good) continue;
            if ($match < 0 || (int)$s['amount'] === $amount) $match = $i;
            if ((int)$s['amount'] === $amount) break;
        }

        if ($match >= 0) {
            $s = &$shipments[$match];
            $s['status']      = 'in_flight';
            $s['trader_id']   = $traderId;
            $s['amount']      = $amount;
            if ($eta > 0) $s['eta_game'] = $eta;
            $s['lease_until'] = null;
            $s['dirty']       = true;
            unset($s);
            continue;
        }

        $shipments[] = bq_new_shipment([
            'status'          => 'in_flight',
            'source'          => 'adopted',
            'from_user_id'    => $user_id,
            'from_village_id' => $from,
            'to_user_id'      => (int)$villages[$to]['user_id'],
            'to_village_id'   => $to,
            'good'            => $good,
            'amount'          => $amount,
            'trader_id'       => $traderId,
            'eta_game'        => $eta > 0 ? $eta : null,
        ], $api_now);
    }
}

/**
 * Expire stale leases and settle arrived shipments.
 *
 * A shipment settles only once the TARGET has reported a village download taken
 * at least settle_buffer_seconds after the ETA. Until then the goods may or may
 * not be in the reported level, so we keep counting them — under-sending is
 * recoverable, over-sending wastes goods. If the target never reports again the
 * shipment simply keeps counting.
 */
function bq_settle(array &$shipments, array $villages, array $settings, int $api_now, int $game_now): void {
    $buffer = (int)$settings['settle_buffer_seconds'];
    foreach ($shipments as &$s) {
        if ($s['status'] === 'leased') {
            if ($s['lease_until'] !== null && (int)$s['lease_until'] < $api_now) {
                $s['status'] = 'expired';
                $s['dirty']  = true;
            }
            continue;
        }
        if ($s['status'] !== 'in_flight') continue;

        $eta = $s['eta_game'] !== null ? (int)$s['eta_game'] : 0;
        if ($eta <= 0) continue; // no ETA known: can't prove arrival, keep counting

        $to = (int)$s['to_village_id'];
        if (isset($villages[$to])) {
            if ((int)$villages[$to]['snapshot_at_game'] >= $eta + $buffer) {
                $s['status'] = 'settled';
                $s['dirty']  = true;
            }
        } elseif ($game_now >= $eta + $buffer) {
            // Target left the group (player removed / village lost): nothing left to protect.
            $s['status'] = 'settled';
            $s['dirty']  = true;
        }
    }
    unset($s);
}

/** Villages of $user_id with arrived shipments whose download is too old to show them. */
function bq_refresh_requests(array $shipments, array $villages, array $settings, int $user_id, int $game_now): array {
    $buffer = (int)$settings['settle_buffer_seconds'];
    $out = [];
    foreach ($shipments as $s) {
        if ($s['status'] !== 'in_flight' || (int)$s['to_user_id'] !== $user_id) continue;
        $eta = (int)$s['eta_game'];
        $to  = (int)$s['to_village_id'];
        if ($eta <= 0 || !isset($villages[$to])) continue;
        if ($game_now >= $eta + $buffer && (int)$villages[$to]['snapshot_at_game'] < $eta + $buffer) {
            $after = $eta + $buffer;
            if (!isset($out[$to]) || $after < $out[$to]) $out[$to] = $after;
        }
    }
    $list = [];
    foreach ($out as $vid => $after) $list[] = ['village_id' => $vid, 'after_game' => $after];
    return $list;
}

/**
 * Per village + good: what we believe is there and what is committed.
 * Returns [vid => [good => [level, inbound, leased_in, leased_out]]] and
 * [vid => merchants reserved by open leases].
 */
function bq_ledger(array $villages, array $shipments, int $game_now): array {
    $ledger = [];
    $leasedMerchants = [];
    foreach ($villages as $vid => $v) {
        for ($g = 0; $g < BQ_NUM_GOODS; $g++) {
            $ledger[$vid][$g] = [
                'level'      => bq_projected_level($v, $g, $game_now),
                'inbound'    => 0,
                'leased_in'  => 0,
                'leased_out' => 0,
            ];
        }
        $leasedMerchants[$vid] = 0;
    }
    foreach ($shipments as $s) {
        $to = (int)$s['to_village_id'];
        $from = (int)$s['from_village_id'];
        $g = (int)$s['good'];
        if ($s['status'] === 'in_flight' && isset($ledger[$to])) {
            $ledger[$to][$g]['inbound'] += (int)$s['amount'];
        } elseif ($s['status'] === 'leased') {
            if (isset($ledger[$to]))   $ledger[$to][$g]['leased_in'] += (int)$s['amount'];
            if (isset($ledger[$from])) {
                $ledger[$from][$g]['leased_out'] += (int)$s['amount'];
                $leasedMerchants[$from] += (int)$s['merchants'];
            }
        }
    }
    return [$ledger, $leasedMerchants];
}

function bq_is_producer(array $settings, array $village, int $good): bool {
    return (int)($village['buildings'][$good] ?? 0) >= (int)$settings['producer_min_buildings'];
}

function bq_is_focus(array $settings, array $village): bool {
    return in_array((int)$village['user_id'], $settings['focus_players'], true)
        || in_array((int)$village['village_id'], $settings['focus_villages'], true);
}

/**
 * Why a village can't receive a good at all (paused, no hall, stale report, not
 * researched...), or '' if it can. Mode-independent; shared by every fill rule.
 */
function bq_receiver_block(array $settings, array $players, array $village, int $good, int $game_now): string {
    if ($settings['mode'] === 'off') return 'off';
    if (empty($settings['goods_enabled'][$good])) return 'good disabled';

    $uid = (int)$village['user_id'];
    if (!isset($players[$uid])) return 'no player';
    if (in_array($uid, $settings['paused_players'], true)) return 'paused';
    if (empty($village['has_hall'])) return 'no village hall';

    $maxAge = (int)$settings['max_report_age_hours'];
    if ($maxAge > 0 && $game_now - (int)$village['levels_at_game'] > $maxAge * 3600) return 'report too old';

    if (!$settings['send_unusable_goods'] && $good >= (int)$players[$uid]['craftsmanship']) {
        return 'not researched';
    }
    return '';
}

/**
 * The amount a receiver should be filled to for one good (0 = not a receiver),
 * or a reason it's skipped. Shared by the planner and the website grid.
 * Balancing between producers is layered on top — see bq_balance_targets.
 */
function bq_target(array $settings, array $players, array $village, int $good, int $game_now): array {
    $block = bq_receiver_block($settings, $players, $village, $good, $game_now);
    if ($block !== '') return [0, $block];

    if ($settings['mode'] === 'focus') {
        if (!bq_is_focus($settings, $village)) return [0, 'not focused'];
    } else { // autofill
        if (bq_is_producer($settings, $village, $good)) {
            return [0, 'produces it'];
        }
    }
    return [bq_effective_cap($settings, $village), ''];
}

/** How much of a good a village can give away right now (before leases already issued). */
function bq_donor_available(array $settings, array $players, array $village, int $good, float $level, int $api_now): int {
    $mode = $settings['mode'];
    if ($mode === 'off' || empty($settings['goods_enabled'][$good])) return 0;
    $uid = (int)$village['user_id'];
    if (!isset($players[$uid]) || !bq_player_online($players[$uid], $api_now)) return 0;
    if (in_array($uid, $settings['paused_players'], true)) return 0;

    if ($mode === 'focus') {
        if (bq_is_focus($settings, $village)) return 0;
    } else { // autofill: only producers give their good away
        if (!bq_is_producer($settings, $village, $good)) {
            return 0;
        }
    }
    return max(0, (int)floor($level) - (int)$settings['keep_amount']);
}

function bq_travel_seconds(array $players, array $from, array $to): float {
    $spt = (float)($players[(int)$from['user_id']]['sec_per_tile'] ?? 0);
    $dx = (float)$from['x'] - (float)$to['x'];
    $dy = (float)$from['y'] - (float)$to['y'];
    return sqrt($dx * $dx + $dy * $dy) * $spt;
}

/** A village's amount of a good once everything already committed has landed or left. */
function bq_effective_level(array $entry): float {
    return $entry['level'] + $entry['inbound'] + $entry['leased_in'] - $entry['leased_out'];
}

/**
 * Balance targets: [vid][good] => amount, for villages that PRODUCE a good (auto-fill
 * with balance_goods on). Plain auto-fill never sends a producer its own good, so two
 * venison villages could otherwise sit at 2000 and 500 forever.
 *
 * Every eligible producer of a good is filled to the same fraction of its cap:
 *   fraction = sum(effective levels) / sum(caps), capped at 1
 * so a 5400-cap hall holds twice what a 2700-cap hall does. Effective levels include
 * goods on the road and leased orders, so the fraction holds still while a rebalance
 * is in flight (no ping-pong). Needs at least two producers of the good.
 */
function bq_balance_targets(array $settings, array $players, array $villages, array $ledger, int $game_now): array {
    $out = [];
    if ($settings['mode'] !== 'autofill' || empty($settings['balance_goods'])) return $out;

    for ($g = 0; $g < BQ_NUM_GOODS; $g++) {
        $caps = [];
        $sumLevel = 0.0;
        $sumCap = 0;
        foreach ($villages as $vid => $v) {
            if (!bq_is_producer($settings, $v, $g)) continue;
            if (bq_receiver_block($settings, $players, $v, $g, $game_now) !== '') continue;
            $cap = bq_effective_cap($settings, $v);
            if ($cap <= 0) continue;
            $caps[$vid] = $cap;
            $sumLevel += max(0.0, bq_effective_level($ledger[$vid][$g]));
            $sumCap += $cap;
        }
        if (count($caps) < 2 || $sumCap <= 0) continue;
        $fraction = min(1.0, $sumLevel / $sumCap);
        foreach ($caps as $vid => $cap) {
            $out[$vid][$g] = (int)floor($fraction * $cap);
        }
    }
    return $out;
}

/** A village's balance tolerance in goods: balance_tolerance_percent of its cap, at least min_send. */
function bq_balance_tolerance(array $settings, array $village): int {
    $cap = bq_effective_cap($settings, $village);
    return max((int)$settings['min_send'], (int)ceil($cap * (int)$settings['balance_tolerance_percent'] / 100));
}

/** Merchants still free per village and open orders per player, after outstanding leases. */
function bq_capacity(array $villages, array $shipments, array $leasedMerchants): array {
    $merchantsFree = [];
    foreach ($villages as $vid => $v) {
        $merchantsFree[$vid] = max(0, (int)$v['merchants_free'] - ($leasedMerchants[$vid] ?? 0));
    }
    $ordersPerUser = [];
    foreach ($shipments as $s) {
        if ($s['status'] === 'leased') {
            $u = (int)$s['from_user_id'];
            $ordersPerUser[$u] = ($ordersPerUser[$u] ?? 0) + 1;
        }
    }
    return [$merchantsFree, $ordersPerUser];
}

/** Emptiest receivers first, then biggest shortfall; ids break ties so plans are deterministic. */
function bq_sort_needs(array &$needs): void {
    usort($needs, function ($a, $b) {
        if ($a['ratio'] != $b['ratio']) return $a['ratio'] < $b['ratio'] ? -1 : 1;
        if ($a['shortfall'] !== $b['shortfall']) return $b['shortfall'] - $a['shortfall'];
        if ($a['vid'] !== $b['vid']) return $a['vid'] - $b['vid'];
        return $a['good'] - $b['good'];
    });
}

/**
 * Turn needs into leased orders. $avail[vid][good] is what each donor may give in this
 * pass; it, the merchant counts and the per-player order counts are decremented as
 * orders are created. Nearest donors first; whole merchant loads except when a load
 * exactly closes the gap.
 */
function bq_allocate(array &$shipments, array &$created, array $settings, array $players, array $villages,
                     array $needs, array &$avail, array &$merchantsFree, array &$ordersPerUser, int $api_now): void {
    $maxTravel = (int)$settings['max_travel_minutes'] * 60;
    $minSend   = (int)$settings['min_send'];
    $maxOrders = (int)$settings['max_orders_per_player'];
    $leaseEnd  = $api_now + (int)$settings['lease_seconds'];

    foreach ($needs as $need) {
        $to = $villages[$need['vid']];
        $g  = $need['good'];
        $shortfall = $need['shortfall'];

        $donors = [];
        foreach ($villages as $vid => $from) {
            if ($vid === $need['vid']) continue;
            if (($avail[$vid][$g] ?? 0) < $minSend || $merchantsFree[$vid] <= 0) continue;
            if (!$settings['include_own_villages'] && (int)$from['user_id'] === (int)$to['user_id']) continue;
            $travel = bq_travel_seconds($players, $from, $to);
            if ($maxTravel > 0 && $travel > $maxTravel) continue;
            $donors[] = ['vid' => $vid, 'travel' => $travel];
        }
        usort($donors, function ($a, $b) {
            if ($a['travel'] != $b['travel']) return $a['travel'] < $b['travel'] ? -1 : 1;
            return $a['vid'] - $b['vid'];
        });

        foreach ($donors as $d) {
            $fvid = $d['vid'];
            $from = $villages[$fvid];
            $fuid = (int)$from['user_id'];
            $carry = max(1, (int)($players[$fuid]['carry'][$g] ?? 1));

            // One donor may need several orders when max_merchants_per_order caps each load.
            while ($shortfall >= $minSend
                && ($ordersPerUser[$fuid] ?? 0) < $maxOrders
                && $avail[$fvid][$g] >= $minSend && $merchantsFree[$fvid] > 0) {

                $byMerchants = min($merchantsFree[$fvid], (int)$settings['max_merchants_per_order']) * $carry;
                $amount = min($shortfall, $avail[$fvid][$g], $byMerchants);
                if ($amount < $shortfall) $amount = intdiv($amount, $carry) * $carry;
                if ($amount < $minSend) break;
                $merchants = (int)ceil($amount / $carry);

                $row = bq_new_shipment([
                    'from_user_id'    => $fuid,
                    'from_village_id' => $fvid,
                    'to_user_id'      => (int)$to['user_id'],
                    'to_village_id'   => (int)$to['village_id'],
                    'good'            => $g,
                    'amount'          => $amount,
                    'merchants'       => $merchants,
                    'lease_until'     => $leaseEnd,
                ], $api_now);
                $shipments[] = $row;
                $created[]   = $row;

                $avail[$fvid][$g]     -= $amount;
                $merchantsFree[$fvid] -= $merchants;
                $shortfall            -= $amount;
                $ordersPerUser[$fuid]  = ($ordersPerUser[$fuid] ?? 0) + 1;
            }
            if ($shortfall < $minSend) break;
        }
    }
}

/**
 * Create new leased orders.
 *
 * Pass 1 (every mode): shortfall = target - (projected level + in flight + leased in) - safety margin.
 *   Emptiest receivers first; donors give what they hold above keep_amount.
 * Pass 2 (auto-fill with balance_goods): producers of the same good are evened out to the
 *   same fill fraction. It runs after pass 1's orders are counted, so villages that don't
 *   make a good are always served first — and they never give goods back.
 *
 * Returns the new shipment rows (also appended to $shipments).
 */
function bq_plan(array &$shipments, array $settings, array $players, array $villages, int $api_now, int $game_now): array {
    if ($settings['mode'] === 'off') return [];
    $created = [];

    // ── Pass 1: fill targets ────────────────────────────────────────────────
    [$ledger, $leasedMerchants] = bq_ledger($villages, $shipments, $game_now);

    $needs = [];
    foreach ($villages as $vid => $v) {
        for ($g = 0; $g < BQ_NUM_GOODS; $g++) {
            [$target] = bq_target($settings, $players, $v, $g, $game_now);
            if ($target <= 0) continue;
            $e = $ledger[$vid][$g];
            $counted = $e['level'] + $e['inbound'] + $e['leased_in'];
            $shortfall = (int)floor($target - $counted - (int)$settings['safety_margin']);
            if ($shortfall < (int)$settings['min_send']) continue;
            $needs[] = ['vid' => $vid, 'good' => $g, 'shortfall' => $shortfall, 'ratio' => $counted / $target];
        }
    }
    bq_sort_needs($needs);

    $avail = [];
    foreach ($villages as $vid => $v) {
        for ($g = 0; $g < BQ_NUM_GOODS; $g++) {
            $a = bq_donor_available($settings, $players, $v, $g, $ledger[$vid][$g]['level'], $api_now);
            $avail[$vid][$g] = max(0, $a - $ledger[$vid][$g]['leased_out']);
        }
    }
    [$merchantsFree, $ordersPerUser] = bq_capacity($villages, $shipments, $leasedMerchants);
    bq_allocate($shipments, $created, $settings, $players, $villages, $needs, $avail, $merchantsFree, $ordersPerUser, $api_now);

    // ── Pass 2: balance producers ───────────────────────────────────────────
    if ($settings['mode'] !== 'autofill' || empty($settings['balance_goods'])) return $created;

    // Re-read the ledger so pass 1's new leases count on both sides.
    [$ledger, $leasedMerchants] = bq_ledger($villages, $shipments, $game_now);
    $targets = bq_balance_targets($settings, $players, $villages, $ledger, $game_now);
    if (!$targets) return $created;

    $needs = [];
    $avail = [];
    foreach ($targets as $vid => $byGood) {
        $v = $villages[$vid];
        $tolerance = bq_balance_tolerance($settings, $v);
        $cap = max(1, bq_effective_cap($settings, $v));
        foreach ($byGood as $g => $target) {
            $e = $ledger[$vid][$g];
            $effective = bq_effective_level($e);

            // More than the tolerance below the balance level: receive.
            $shortfall = (int)floor($target - $effective);
            if ($shortfall >= $tolerance) {
                $needs[] = ['vid' => $vid, 'good' => $g, 'shortfall' => $shortfall, 'ratio' => $effective / $cap];
            }

            // More than the tolerance above it: give down to the balance level, but only
            // goods actually in the hall and never below keep_amount.
            $surplus = (int)floor($effective - $target);
            if ($surplus >= $tolerance) {
                $inHall = bq_donor_available($settings, $players, $v, $g, $e['level'], $api_now) - $e['leased_out'];
                $avail[$vid][$g] = max(0, min($surplus, $inHall));
            }
        }
    }
    if (!$needs) return $created;
    bq_sort_needs($needs);

    [$merchantsFree, $ordersPerUser] = bq_capacity($villages, $shipments, $leasedMerchants);
    bq_allocate($shipments, $created, $settings, $players, $villages, $needs, $avail, $merchantsFree, $ordersPerUser, $api_now);
    return $created;
}

/** Website grid: every village x good with the numbers the planner used. */
function bq_grid(array $settings, array $players, array $villages, array $shipments, int $game_now): array {
    [$ledger] = bq_ledger($villages, $shipments, $game_now);
    $balance = bq_balance_targets($settings, $players, $villages, $ledger, $game_now);
    $grid = [];
    foreach ($villages as $vid => $v) {
        $row = [];
        for ($g = 0; $g < BQ_NUM_GOODS; $g++) {
            [$target, $reason] = bq_target($settings, $players, $v, $g, $game_now);
            $balanced = isset($balance[$vid][$g]);
            if ($balanced) {
                $target = $balance[$vid][$g];
                $reason = '';
            }
            $e = $ledger[$vid][$g];
            $counted = $e['level'] + $e['inbound'] + $e['leased_in'];
            $row[] = [
                'level'     => (int)floor($e['level']),
                'inbound'   => $e['inbound'],
                'leased_in' => $e['leased_in'],
                'target'    => $target,
                'shortfall' => $target > 0 ? max(0, (int)floor($target - $counted)) : 0,
                'skip'      => $reason,
                'balance'   => $balanced,
            ];
        }
        $grid[(string)$vid] = $row;
    }
    return $grid;
}
