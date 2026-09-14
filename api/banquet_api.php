<?php
/**
 * Banquet Good Sender — coordination API.
 *
 * POST application/json. Every request carries "key" (the shared group key) and
 * "action". A group is created the first time a key is used by a bot (sync) or
 * by the website saving settings.
 *
 * Bot actions:
 *   sync      Report villages / merchants on the road / order outcomes, receive orders.
 *             Body: player{}, villages[] (downloaded villages), all_village_ids[] (every
 *             owned non-capital village), outbound[] (merchants on the road), results[].
 *   leave     Drop this player's open orders and mark them offline.
 *
 * Website actions (same key — anyone with the key can manage the group):
 *   get_state        Players, villages, the planner grid and the shipment ledger.
 *   set_settings     Replace the group settings (normalised server-side).
 *   clear_shipments  Stop counting in-flight shipments (ids[] or all=true).
 *   remove_player    Forget a player, their villages and their open orders.
 *
 * Planning logic lives in banquet_planner.php. This file only moves rows in and
 * out of MySQL, serialising each group with SELECT ... FOR UPDATE.
 */

header('Content-Type: application/json');
header('Cache-Control: no-store, no-cache, must-revalidate');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type');

if (($_SERVER['REQUEST_METHOD'] ?? '') === 'OPTIONS') {
    http_response_code(200);
    exit;
}

require_once __DIR__ . '/db_config.php';
require_once __DIR__ . '/banquet_planner.php';

const BQ_MAX_VILLAGES = 500;
const BQ_MAX_OUTBOUND = 2000;
const BQ_MAX_RESULTS  = 500;
const BQ_RECENT_SHIPMENTS = 150;

if (($_SERVER['REQUEST_METHOD'] ?? '') !== 'POST') {
    bq_error('Only POST requests are accepted.', 405);
}

$req = json_decode(file_get_contents('php://input'), true);
if (!is_array($req) || empty($req['action'])) bq_error('Missing action.');

$key = isset($req['key']) ? trim((string)$req['key']) : '';
if (strlen($key) < 6) bq_error('A group key of at least 6 characters is required.', 403);
$keyHash = hash('sha256', $key);

$pdo = bq_db();
$now = time();

try {
    switch ($req['action']) {
        case 'sync':            bq_handle_sync($pdo, $keyHash, $req, $now);            break;
        case 'leave':           bq_handle_leave($pdo, $keyHash, $req, $now);           break;
        case 'get_state':       bq_handle_get_state($pdo, $keyHash, $now);             break;
        case 'set_settings':    bq_handle_set_settings($pdo, $keyHash, $req, $now);    break;
        case 'clear_shipments': bq_handle_clear_shipments($pdo, $keyHash, $req, $now); break;
        case 'remove_player':   bq_handle_remove_player($pdo, $keyHash, $req, $now);   break;
        default:                bq_error('Unknown action: ' . substr((string)$req['action'], 0, 32));
    }
} catch (Throwable $e) {
    if ($pdo->inTransaction()) $pdo->rollBack();
    error_log('banquet_api: ' . $e->getMessage());
    bq_error('Server error.', 500);
}

// ── Plumbing ────────────────────────────────────────────────────────────────

function bq_db(): PDO {
    try {
        $dsn = 'mysql:host=' . DB_HOST . ';dbname=' . DB_NAME . ';charset=' . DB_CHARSET;
        return new PDO($dsn, DB_USER, DB_PASS, [
            PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
            PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
            PDO::ATTR_EMULATE_PREPARES   => false,
        ]);
    } catch (PDOException $e) {
        bq_error('Database connection failed.', 500);
    }
}

function bq_ok(array $data): void {
    echo json_encode(array_merge(['ok' => true, 'server_time' => time()], $data));
    exit;
}

function bq_error(string $message, int $code = 400): void {
    http_response_code($code);
    echo json_encode(['ok' => false, 'error' => $message, 'server_time' => time()]);
    exit;
}

/** Start a transaction holding the group's row lock. Returns the group row, or null. */
function bq_lock_group(PDO $pdo, string $keyHash, bool $create, int $now): ?array {
    if ($create) {
        $pdo->prepare('INSERT IGNORE INTO bq_groups (key_hash, settings_json, created_at, updated_at) VALUES (?, ?, ?, ?)')
            ->execute([$keyHash, json_encode(bq_default_settings()), $now, $now]);
    }
    $pdo->beginTransaction();
    $st = $pdo->prepare('SELECT * FROM bq_groups WHERE key_hash = ? FOR UPDATE');
    $st->execute([$keyHash]);
    $group = $st->fetch();
    if (!$group) {
        $pdo->rollBack();
        return null;
    }
    $group['settings'] = bq_normalize_settings(json_decode($group['settings_json'], true));
    return $group;
}

function bq_load_players(PDO $pdo, int $groupId): array {
    $st = $pdo->prepare('SELECT * FROM bq_players WHERE group_id = ?');
    $st->execute([$groupId]);
    $out = [];
    foreach ($st->fetchAll() as $r) {
        $out[(int)$r['user_id']] = [
            'user_id'        => (int)$r['user_id'],
            'name'           => $r['name'],
            'world'          => $r['world'],
            'last_seen'      => (int)$r['last_seen'],
            'craftsmanship'  => (int)$r['craftsmanship'],
            'sec_per_tile'   => (float)$r['sec_per_tile'],
            'carry'          => bq_int_array(json_decode($r['carry_json'], true)),
            'cards'          => json_decode((string)$r['cards_json'], true),
            'client_version' => $r['client_version'],
        ];
    }
    return $out;
}

function bq_load_villages(PDO $pdo, int $groupId): array {
    $st = $pdo->prepare('SELECT * FROM bq_villages WHERE group_id = ?');
    $st->execute([$groupId]);
    $out = [];
    foreach ($st->fetchAll() as $r) {
        $out[(int)$r['village_id']] = [
            'village_id'       => (int)$r['village_id'],
            'user_id'          => (int)$r['user_id'],
            'name'             => $r['name'],
            'x'                => (int)$r['x'],
            'y'                => (int)$r['y'],
            'has_hall'         => (bool)$r['has_hall'],
            'hall_cap'         => (int)$r['hall_cap'],
            'levels'           => bq_int_array(json_decode($r['levels_json'], true)),
            'prod'             => bq_float_array(json_decode($r['prod_json'], true)),
            'buildings'        => bq_int_array(json_decode($r['buildings_json'], true)),
            'levels_at_game'   => (int)$r['levels_at_game'],
            'snapshot_at_game' => (int)$r['snapshot_at_game'],
            'merchants_free'   => (int)$r['merchants_free'],
            'reported_at'      => (int)$r['reported_at'],
        ];
    }
    return $out;
}

/** Active shipments, recently expired leases (a late result may still revive them), and any rows for $traderIds. */
function bq_load_shipments(PDO $pdo, int $groupId, int $now, array $traderIds = []): array {
    $st = $pdo->prepare(
        "SELECT * FROM bq_shipments WHERE group_id = ?
           AND (status IN ('leased', 'in_flight') OR (status = 'expired' AND updated_at > ?))");
    $st->execute([$groupId, $now - 600]);
    $rows = $st->fetchAll();

    $traderIds = array_values(array_unique(array_filter(array_map('intval', $traderIds))));
    if ($traderIds) {
        foreach (array_chunk($traderIds, 500) as $chunk) {
            $in = implode(',', array_fill(0, count($chunk), '?'));
            $st = $pdo->prepare("SELECT * FROM bq_shipments WHERE group_id = ? AND trader_id IN ($in)");
            $st->execute(array_merge([$groupId], $chunk));
            $rows = array_merge($rows, $st->fetchAll());
        }
    }

    $out = [];
    $seen = [];
    foreach ($rows as $r) {
        if (isset($seen[$r['id']])) continue;
        $seen[$r['id']] = true;
        $out[] = bq_shipment_from_row($r);
    }
    return $out;
}

function bq_shipment_from_row(array $r): array {
    return [
        'id'              => (int)$r['id'],
        'status'          => $r['status'],
        'source'          => $r['source'],
        'from_user_id'    => (int)$r['from_user_id'],
        'from_village_id' => (int)$r['from_village_id'],
        'to_user_id'      => (int)$r['to_user_id'],
        'to_village_id'   => (int)$r['to_village_id'],
        'good'            => (int)$r['good'],
        'amount'          => (int)$r['amount'],
        'merchants'       => (int)$r['merchants'],
        'trader_id'       => $r['trader_id'] !== null ? (int)$r['trader_id'] : null,
        'eta_game'        => $r['eta_game'] !== null ? (int)$r['eta_game'] : null,
        'lease_until'     => $r['lease_until'] !== null ? (int)$r['lease_until'] : null,
        'error'           => $r['error'],
        'created_at'      => (int)$r['created_at'],
        'updated_at'      => (int)$r['updated_at'],
        'dirty'           => false,
    ];
}

/** Insert new rows and update dirty ones. Assigns ids to inserted rows. */
function bq_save_shipments(PDO $pdo, int $groupId, array &$shipments, int $now): void {
    $ins = $pdo->prepare(
        'INSERT INTO bq_shipments (group_id, status, source, from_user_id, from_village_id, to_user_id,
            to_village_id, good, amount, merchants, trader_id, eta_game, lease_until, error, created_at, updated_at)
         VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)');
    $upd = $pdo->prepare(
        'UPDATE bq_shipments SET status = ?, amount = ?, trader_id = ?, eta_game = ?, lease_until = ?,
            error = ?, updated_at = ? WHERE id = ? AND group_id = ?');

    foreach ($shipments as &$s) {
        if (!$s['dirty']) continue;
        if ($s['id'] === null) {
            $ins->execute([$groupId, $s['status'], $s['source'], $s['from_user_id'], $s['from_village_id'],
                $s['to_user_id'], $s['to_village_id'], $s['good'], $s['amount'], $s['merchants'],
                $s['trader_id'], $s['eta_game'], $s['lease_until'], $s['error'], $now, $now]);
            $s['id'] = (int)$pdo->lastInsertId();
        } else {
            $upd->execute([$s['status'], $s['amount'], $s['trader_id'], $s['eta_game'], $s['lease_until'],
                $s['error'], $now, $s['id'], $groupId]);
        }
        $s['updated_at'] = $now;
        $s['dirty'] = false;
    }
    unset($s);
}

function bq_int_array($v): array {
    $out = array_fill(0, BQ_NUM_GOODS, 0);
    if (is_array($v)) {
        for ($g = 0; $g < BQ_NUM_GOODS; $g++) $out[$g] = isset($v[$g]) && is_numeric($v[$g]) ? (int)$v[$g] : 0;
    }
    return $out;
}

function bq_float_array($v): array {
    $out = array_fill(0, BQ_NUM_GOODS, 0.0);
    if (is_array($v)) {
        for ($g = 0; $g < BQ_NUM_GOODS; $g++) $out[$g] = isset($v[$g]) && is_numeric($v[$g]) ? round((float)$v[$g], 2) : 0.0;
    }
    return $out;
}

function bq_str($v, int $max): string {
    return mb_substr(trim((string)$v), 0, $max);
}

// ── Bot: sync ───────────────────────────────────────────────────────────────

function bq_handle_sync(PDO $pdo, string $keyHash, array $req, int $now): void {
    $p = $req['player'] ?? null;
    if (!is_array($p) || (int)($p['user_id'] ?? 0) <= 0) bq_error('player.user_id is required.');
    $userId   = (int)$p['user_id'];
    $gameTime = (int)($p['game_time'] ?? 0);
    if ($gameTime <= 0) bq_error('player.game_time is required.');

    $villagesIn = is_array($req['villages'] ?? null) ? array_slice($req['villages'], 0, BQ_MAX_VILLAGES) : [];
    $outbound   = is_array($req['outbound'] ?? null) ? array_slice($req['outbound'], 0, BQ_MAX_OUTBOUND) : [];
    $results    = is_array($req['results'] ?? null)  ? array_slice($req['results'], 0, BQ_MAX_RESULTS)   : [];

    $group = bq_lock_group($pdo, $keyHash, true, $now);
    $gid = (int)$group['id'];
    $settings = $group['settings'];
    $offset = $gameTime - $now;

    $pdo->prepare('UPDATE bq_groups SET game_offset_sec = ?, updated_at = ? WHERE id = ?')
        ->execute([$offset, $now, $gid]);

    $pdo->prepare(
        'INSERT INTO bq_players (group_id, user_id, name, world, craftsmanship, sec_per_tile, carry_json,
            cards_json, client_version, last_seen)
         VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
         ON DUPLICATE KEY UPDATE name = VALUES(name), world = VALUES(world), craftsmanship = VALUES(craftsmanship),
            sec_per_tile = VALUES(sec_per_tile), carry_json = VALUES(carry_json), cards_json = VALUES(cards_json),
            client_version = VALUES(client_version), last_seen = VALUES(last_seen)')
        ->execute([
            $gid, $userId, bq_str($p['name'] ?? '', 64), bq_str($p['world'] ?? '', 64),
            max(0, min(BQ_NUM_GOODS, (int)($p['craftsmanship'] ?? 0))),
            max(0.0, (float)($p['sec_per_tile'] ?? 0)),
            json_encode(bq_int_array($p['carry'] ?? [])),
            isset($p['cards']) ? json_encode($p['cards']) : null,
            bq_str($p['client_version'] ?? '', 32),
            $now,
        ]);

    // Replace this player's village set.
    $keepIds = [];
    $up = $pdo->prepare(
        'INSERT INTO bq_villages (group_id, village_id, user_id, name, x, y, has_hall, hall_cap, levels_json,
            prod_json, buildings_json, levels_at_game, snapshot_at_game, merchants_free, reported_at)
         VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
         ON DUPLICATE KEY UPDATE user_id = VALUES(user_id), name = VALUES(name), x = VALUES(x), y = VALUES(y),
            has_hall = VALUES(has_hall), hall_cap = VALUES(hall_cap), levels_json = VALUES(levels_json),
            prod_json = VALUES(prod_json), buildings_json = VALUES(buildings_json), levels_at_game = VALUES(levels_at_game),
            snapshot_at_game = VALUES(snapshot_at_game), merchants_free = VALUES(merchants_free),
            reported_at = VALUES(reported_at)');
    foreach ($villagesIn as $v) {
        $vid = (int)($v['village_id'] ?? 0);
        if ($vid <= 0) continue;
        $keepIds[] = $vid;
        $up->execute([
            $gid, $vid, $userId, bq_str($v['name'] ?? '', 64), (int)($v['x'] ?? 0), (int)($v['y'] ?? 0),
            !empty($v['has_hall']) ? 1 : 0, max(0, (int)($v['hall_cap'] ?? 0)),
            json_encode(bq_int_array($v['levels'] ?? [])), json_encode(bq_float_array($v['prod'] ?? [])),
            json_encode(bq_int_array($v['buildings'] ?? [])),
            max(0, (int)($v['levels_at_game'] ?? $gameTime)), max(0, (int)($v['snapshot_at_game'] ?? 0)),
            max(0, (int)($v['merchants_free'] ?? 0)), $now,
        ]);
    }
    // all_village_ids lists every village the player still owns, including ones the
    // client hasn't downloaded this session (those keep their previous report).
    if (isset($req['all_village_ids']) && is_array($req['all_village_ids'])) {
        $keepIds = array_values(array_unique(array_merge($keepIds, bq_int_list($req['all_village_ids']))));
    }
    if ($keepIds) {
        $in = implode(',', array_fill(0, count($keepIds), '?'));
        $pdo->prepare("DELETE FROM bq_villages WHERE group_id = ? AND user_id = ? AND village_id NOT IN ($in)")
            ->execute(array_merge([$gid, $userId], $keepIds));
    } else {
        $pdo->prepare('DELETE FROM bq_villages WHERE group_id = ? AND user_id = ?')->execute([$gid, $userId]);
    }

    $players  = bq_load_players($pdo, $gid);
    $villages = bq_load_villages($pdo, $gid);
    $shipments = bq_load_shipments($pdo, $gid, $now, array_column($outbound, 'trader_id'));
    $gameNow = bq_game_now($now, $offset);

    bq_apply_results($shipments, $results, $userId);
    bq_adopt_outbound($shipments, $outbound, $userId, $villages, $now);
    bq_settle($shipments, $villages, $settings, $now, $gameNow);
    bq_plan($shipments, $settings, $players, $villages, $now, $gameNow);
    bq_save_shipments($pdo, $gid, $shipments, $now);
    $pdo->commit();

    $orders = [];
    $inFlight = 0;
    foreach ($shipments as $s) {
        if ($s['status'] === 'in_flight') $inFlight++;
        if ($s['status'] !== 'leased' || (int)$s['from_user_id'] !== $userId) continue;
        $to = $villages[(int)$s['to_village_id']] ?? null;
        $orders[] = [
            'order_id'            => $s['id'],
            'from_village_id'     => $s['from_village_id'],
            'to_village_id'       => $s['to_village_id'],
            'to_village_name'     => $to ? $to['name'] : '',
            'to_player_name'      => $to && isset($players[$to['user_id']]) ? $players[$to['user_id']]['name'] : '',
            'good'                => $s['good'],
            'amount'              => $s['amount'],
            'merchants'           => $s['merchants'],
            'lease_remaining_sec' => max(0, (int)$s['lease_until'] - $now),
        ];
    }

    $online = 0;
    foreach ($players as $pl) if (bq_player_online($pl, $now)) $online++;

    bq_ok([
        'mode'           => $settings['mode'],
        'orders'         => $orders,
        'refresh'        => bq_refresh_requests($shipments, $villages, $settings, $userId, $gameNow),
        'players_online' => $online,
        'in_flight'      => $inFlight,
    ]);
}

function bq_handle_leave(PDO $pdo, string $keyHash, array $req, int $now): void {
    $userId = (int)($req['user_id'] ?? 0);
    $group = bq_lock_group($pdo, $keyHash, false, $now);
    if (!$group) bq_ok([]);
    $gid = (int)$group['id'];
    $pdo->prepare("UPDATE bq_shipments SET status = 'cancelled', error = 'player left', lease_until = NULL,
            updated_at = ? WHERE group_id = ? AND from_user_id = ? AND status = 'leased'")
        ->execute([$now, $gid, $userId]);
    $pdo->prepare('UPDATE bq_players SET last_seen = 0 WHERE group_id = ? AND user_id = ?')
        ->execute([$gid, $userId]);
    $pdo->commit();
    bq_ok([]);
}

// ── Website ─────────────────────────────────────────────────────────────────

function bq_handle_get_state(PDO $pdo, string $keyHash, int $now): void {
    $group = bq_lock_group($pdo, $keyHash, false, $now);
    if (!$group) {
        $settings = bq_normalize_settings(null);
        bq_ok(['exists' => false, 'goods' => BQ_GOOD_NAMES, 'settings' => $settings, 'players' => [],
            'villages' => [], 'grid' => (object)[], 'active' => [], 'recent' => [],
            'settle_buffer_seconds' => $settings['settle_buffer_seconds']]);
    }
    $gid = (int)$group['id'];
    $settings = $group['settings'];
    $gameNow = bq_game_now($now, (int)$group['game_offset_sec']);

    $players   = bq_load_players($pdo, $gid);
    $villages  = bq_load_villages($pdo, $gid);
    $shipments = bq_load_shipments($pdo, $gid, $now);

    // Settle/expire so the page shows what the next sync would see, then persist it.
    bq_settle($shipments, $villages, $settings, $now, $gameNow);
    bq_save_shipments($pdo, $gid, $shipments, $now);

    $st = $pdo->prepare(
        "SELECT * FROM bq_shipments WHERE group_id = ? AND status NOT IN ('leased', 'in_flight')
          ORDER BY updated_at DESC, id DESC LIMIT " . BQ_RECENT_SHIPMENTS);
    $st->execute([$gid]);
    $recent = array_map('bq_shipment_from_row', $st->fetchAll());
    $pdo->commit();

    $playersOut = [];
    $villageCounts = [];
    foreach ($villages as $v) $villageCounts[$v['user_id']] = ($villageCounts[$v['user_id']] ?? 0) + 1;
    foreach ($players as $pl) {
        $playersOut[] = [
            'user_id'        => $pl['user_id'],
            'name'           => $pl['name'],
            'world'          => $pl['world'],
            'online'         => bq_player_online($pl, $now),
            'last_seen_ago'  => $pl['last_seen'] > 0 ? $now - $pl['last_seen'] : null,
            'craftsmanship'  => $pl['craftsmanship'],
            'sec_per_tile'   => $pl['sec_per_tile'],
            'cards'          => $pl['cards'],
            'client_version' => $pl['client_version'],
            'villages'       => $villageCounts[$pl['user_id']] ?? 0,
        ];
    }

    $villagesOut = [];
    foreach ($villages as $v) {
        $villagesOut[] = [
            'village_id'        => $v['village_id'],
            'user_id'           => $v['user_id'],
            'name'              => $v['name'],
            'x'                 => $v['x'],
            'y'                 => $v['y'],
            'has_hall'          => $v['has_hall'],
            'hall_cap'          => $v['hall_cap'],
            'effective_cap'     => bq_effective_cap($settings, $v),
            'prod'              => $v['prod'],
            'buildings'         => $v['buildings'],
            'merchants_free'    => $v['merchants_free'],
            'levels_age_sec'    => max(0, $gameNow - $v['levels_at_game']),
            'snapshot_age_sec'  => $v['snapshot_at_game'] > 0 ? max(0, $gameNow - $v['snapshot_at_game']) : null,
        ];
    }

    $shipOut = function (array $s) use ($gameNow, $now) {
        return [
            'id'              => $s['id'],
            'status'          => $s['status'],
            'source'          => $s['source'],
            'from_user_id'    => $s['from_user_id'],
            'from_village_id' => $s['from_village_id'],
            'to_user_id'      => $s['to_user_id'],
            'to_village_id'   => $s['to_village_id'],
            'good'            => $s['good'],
            'amount'          => $s['amount'],
            'merchants'       => $s['merchants'],
            'eta_in_sec'      => $s['eta_game'] !== null ? $s['eta_game'] - $gameNow : null,
            'error'           => $s['error'],
            'updated_ago'     => $now - (int)($s['updated_at'] ?? $now),
        ];
    };
    $active = array_values(array_filter($shipments, function ($s) {
        return in_array($s['status'], BQ_ACTIVE_STATUSES, true);
    }));

    bq_ok([
        'exists'    => true,
        'goods'     => BQ_GOOD_NAMES,
        'settings'  => $settings,
        'players'   => $playersOut,
        'villages'  => $villagesOut,
        'grid'      => (object)bq_grid($settings, $players, $villages, $shipments, $gameNow),
        'active'    => array_map($shipOut, $active),
        'recent'    => array_map($shipOut, $recent),
        'settle_buffer_seconds' => $settings['settle_buffer_seconds'],
    ]);
}

function bq_handle_set_settings(PDO $pdo, string $keyHash, array $req, int $now): void {
    if (!isset($req['settings']) || !is_array($req['settings'])) bq_error('settings object is required.');
    $group = bq_lock_group($pdo, $keyHash, true, $now);
    $settings = bq_normalize_settings($req['settings']);
    $pdo->prepare('UPDATE bq_groups SET settings_json = ?, updated_at = ? WHERE id = ?')
        ->execute([json_encode($settings), $now, (int)$group['id']]);

    // Orders planned under the old settings may no longer be wanted.
    if (!empty($req['cancel_open_orders']) || $settings['mode'] === 'off') {
        $pdo->prepare("UPDATE bq_shipments SET status = 'cancelled', error = 'settings changed', lease_until = NULL,
                updated_at = ? WHERE group_id = ? AND status = 'leased'")
            ->execute([$now, (int)$group['id']]);
    }
    $pdo->commit();
    bq_ok(['settings' => $settings]);
}

function bq_handle_clear_shipments(PDO $pdo, string $keyHash, array $req, int $now): void {
    $group = bq_lock_group($pdo, $keyHash, false, $now);
    if (!$group) bq_error('Unknown group.', 404);
    $gid = (int)$group['id'];
    if (!empty($req['all'])) {
        $st = $pdo->prepare("UPDATE bq_shipments SET status = 'cancelled', error = 'cleared from website',
                lease_until = NULL, updated_at = ? WHERE group_id = ? AND status IN ('leased', 'in_flight')");
        $st->execute([$now, $gid]);
    } else {
        $ids = bq_int_list($req['ids'] ?? []);
        if (!$ids) bq_error('ids[] or all=true is required.');
        $in = implode(',', array_fill(0, count($ids), '?'));
        $st = $pdo->prepare("UPDATE bq_shipments SET status = 'cancelled', error = 'cleared from website',
                lease_until = NULL, updated_at = ? WHERE group_id = ? AND status IN ('leased', 'in_flight') AND id IN ($in)");
        $st->execute(array_merge([$now, $gid], $ids));
    }
    $count = $st->rowCount();
    $pdo->commit();
    bq_ok(['cleared' => $count]);
}

function bq_handle_remove_player(PDO $pdo, string $keyHash, array $req, int $now): void {
    $userId = (int)($req['user_id'] ?? 0);
    if ($userId <= 0) bq_error('user_id is required.');
    $group = bq_lock_group($pdo, $keyHash, false, $now);
    if (!$group) bq_error('Unknown group.', 404);
    $gid = (int)$group['id'];
    $pdo->prepare("UPDATE bq_shipments SET status = 'cancelled', error = 'player removed', lease_until = NULL,
            updated_at = ? WHERE group_id = ? AND from_user_id = ? AND status = 'leased'")
        ->execute([$now, $gid, $userId]);
    $pdo->prepare('DELETE FROM bq_villages WHERE group_id = ? AND user_id = ?')->execute([$gid, $userId]);
    $pdo->prepare('DELETE FROM bq_players WHERE group_id = ? AND user_id = ?')->execute([$gid, $userId]);
    $pdo->commit();
    bq_ok([]);
}
