<?php
/**
 * Auto Bomb Multi — Coordination API
 *
 * Single-file PHP endpoint. State is kept in state.json next to this file.
 * All requests are POST with Content-Type: application/json.
 * Every request must include "session_key" matching SESSION_KEY below.
 *
 * Hosting: PHP 7.4+, the directory containing this file must be web-writable
 *          so state.json can be created/updated.
 */

define('HEARTBEAT_TTL', 30); // seconds before a player is considered disconnected

header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type');

if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit;
}

if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    respond_error('Only POST requests are accepted.');
}

$body = file_get_contents('php://input');
$req  = json_decode($body, true);

if (!$req || !isset($req['action'])) {
    respond_error('Missing action.');
}

// ── Session key → per-key state file ────────────────────────────────────────
// Each session key gets its own isolated state file. An empty key is rejected
// so two groups can never accidentally share state.
$session_key = isset($req['session_key']) ? trim($req['session_key']) : '';
if ($session_key === '') {
    respond_error('session_key is required.', 403);
}
// Use a short hash so the filename stays filesystem-safe regardless of key content.
$key_hash  = substr(hash('sha256', $session_key), 0, 16);
$state_file = __DIR__ . '/state_' . $key_hash . '.json';

// ── Load + lock state ────────────────────────────────────────────────────────
$fp = fopen($state_file, 'c+');
if (!$fp) respond_error('Cannot open state file.');
flock($fp, LOCK_EX);

$raw   = stream_get_contents($fp);
$state = ($raw && strlen($raw) > 2) ? json_decode($raw, true) : null;
if (!$state) {
    $state = make_empty_state();
}

// Expire stale players
expire_players($state);

// ── Dispatch ─────────────────────────────────────────────────────────────────
$action = $req['action'];
switch ($action) {
    case 'player_ready':         handle_player_ready($state, $req);       break;
    case 'get_status':           handle_get_status($state, $req);         break;
    case 'take_coordinator':     handle_take_coordinator($state, $req);   break;
    case 'set_attack_config':    handle_set_attack_config($state, $req);  break;
    case 'start_timer':          handle_start_timer($state, $req);        break;
    case 'prepare_attacks':       handle_prepare_attacks($state, $req);    break;
    case 'cancel_attacks':       handle_cancel_attacks($state, $req);     break;
    case 'attack_validated':     handle_attack_validated($state, $req);           break;
    case 'attack_prepared':      handle_attack_event($state, $req, 'prepared');    break;
    case 'attack_sent':          handle_attack_event($state, $req, 'sent');        break;
    case 'attack_failed':        handle_attack_failed($state, $req, false);        break;
    case 'attack_failed_prepare':handle_attack_failed($state, $req, true);         break;
    case 'report_armies_status': handle_report_armies_status($state, $req);        break;
    case 'queue_target':         handle_queue_target($state, $req);       break;
    case 'player_disconnect':    handle_player_disconnect($state, $req);  break;
    case 'reset_session':        handle_reset_session($state, $req);      break;
    default:                     respond_error('Unknown action: ' . $action);
}

// ── Save + unlock ─────────────────────────────────────────────────────────────
function save_and_respond($state, $extra = []) {
    global $fp;
    $state['last_updated'] = gmdate('Y-m-d\TH:i:s\Z');
    ftruncate($fp, 0);
    rewind($fp);
    fwrite($fp, json_encode($state, JSON_PRETTY_PRINT));
    flock($fp, LOCK_UN);
    fclose($fp);

    $out = array_merge(['ok' => true, 'server_time' => gmdate('Y-m-d\TH:i:s\Z')], $extra);
    echo json_encode($out);
    exit;
}

function respond_error($msg, $code = 400) {
    global $fp;
    if (isset($fp) && is_resource($fp)) { flock($fp, LOCK_UN); fclose($fp); }
    http_response_code($code);
    echo json_encode(['ok' => false, 'error' => $msg, 'server_time' => gmdate('Y-m-d\TH:i:s\Z')]);
    exit;
}

// ── Helpers ──────────────────────────────────────────────────────────────────

function make_empty_state() {
    return [
        'state'              => 'idle',
        'coordinator'        => '',
        'target_village_id'  => 0,
        'players'            => [],
        'attacks'            => [],
        'timer_settings'     => [],
        'scheduled_send_times' => [],
        'launch_id'          => '',
        'armies_return_status' => [],
        'target_queue'       => [],
        'interdict_detected' => false,
        'last_updated'       => gmdate('Y-m-d\TH:i:s\Z'),
    ];
}

function expire_players(&$state) {
    $now = time();
    $active = [];
    $expired_names = [];
    foreach ($state['players'] as $p) {
        $last = isset($p['last_seen']) ? strtotime($p['last_seen']) : 0;
        if (($now - $last) <= HEARTBEAT_TTL) {
            $active[] = $p;
        } else {
            $expired_names[] = $p['name'];
        }
    }
    if (count($active) !== count($state['players'])) {
        $state['players'] = $active;
        // If coordinator expired, clear coordinator
        $found = false;
        foreach ($active as $p) {
            if ($p['name'] === $state['coordinator']) { $found = true; break; }
        }
        if (!$found) $state['coordinator'] = '';
        // Treat disconnected players as having returned — they can't report back
        foreach ($expired_names as $name) {
            if (isset($state['armies_return_status'][$name]) &&
                $state['armies_return_status'][$name] === 'waiting') {
                $state['armies_return_status'][$name] = 'returned';
            }
        }
    }
}

function find_player_index(&$state, $name) {
    foreach ($state['players'] as $i => $p) {
        if ($p['name'] === $name) return $i;
    }
    return -1;
}

function require_field($req, $field) {
    if (!isset($req[$field])) respond_error("Missing field: $field");
    return $req[$field];
}

function require_coordinator($state, $player_name) {
    if ($state['coordinator'] !== $player_name) {
        respond_error('Only the coordinator can perform this action.', 403);
    }
}

// ── Action handlers ───────────────────────────────────────────────────────────

function handle_player_ready(&$state, $req) {
    $name     = require_field($req, 'player_name');
    $villages = isset($req['villages']) ? $req['villages'] : [];
    $travel_target = isset($req['travel_target']) ? (int)$req['travel_target'] : 0;

    $idx = find_player_index($state, $name);
    $player = [
        'name'          => $name,
        'villages'      => $villages,
        'ready'         => true,
        'travel_target' => $travel_target,
        'last_seen'     => gmdate('Y-m-d\TH:i:s\Z'),
    ];

    if ($idx >= 0) {
        $state['players'][$idx] = $player;
    } else {
        $state['players'][] = $player;
    }

    save_and_respond($state, ['state_data' => $state]);
}

function handle_get_status(&$state, $req) {
    $name = isset($req['player_name']) ? $req['player_name'] : '';
    // Heartbeat: update last_seen for the requesting player
    if ($name !== '') {
        $idx = find_player_index($state, $name);
        if ($idx >= 0) $state['players'][$idx]['last_seen'] = gmdate('Y-m-d\TH:i:s\Z');
    }
    save_and_respond($state, ['state_data' => $state]);
}

function handle_take_coordinator(&$state, $req) {
    $name = require_field($req, 'player_name');
    $idx  = find_player_index($state, $name);
    if ($idx < 0) respond_error('Player not connected. Call player_ready first.');
    $state['coordinator'] = $name;
    save_and_respond($state, ['state_data' => $state]);
}

function handle_set_attack_config(&$state, $req) {
    $name    = require_field($req, 'player_name');
    $attacks = require_field($req, 'attacks');
    $target  = require_field($req, 'target_village_id');
    require_coordinator($state, $name);

    $state['target_village_id'] = (int)$target;
    // Merge new attack definitions; preserve runtime status from existing entries.
    // Key by player+village+vassal so a village staged as both a player and a vassal
    // attack keeps each entry's status independently.
    $existing = [];
    foreach ($state['attacks'] as $a) {
        $k = $a['source_player'] . '|' . $a['source_village_id'] . '|' . ($a['is_vassal'] ? 'v' : 'p');
        $existing[$k] = $a;
    }
    $merged = [];
    foreach ($attacks as $a) {
        $vid = (int)$a['source_village_id'];
        $isV = (bool)($a['is_vassal'] ?? false);
        $k   = $a['source_player'] . '|' . $vid . '|' . ($isV ? 'v' : 'p');
        $merged[] = [
            'source_player'    => $a['source_player'],
            'source_village_id'=> $vid,
            'parent_village_id'=> (int)($a['parent_village_id'] ?? 0),
            'is_vassal'        => $isV,
            'formation'        => $a['formation'],
            'stack'            => (int)$a['stack'],
            'card_type'        => (int)$a['card_type'],
            'captains_only'    => (bool)$a['captains_only'],
            'attack_type'      => (int)$a['attack_type'],
            'travel_time_seconds' => (float)($a['travel_time_seconds'] ?? 0),
            'selected'         => (bool)($a['selected'] ?? true),
            'status'           => isset($existing[$k]) ? $existing[$k]['status'] : 'queued',
        ];
    }
    $state['attacks'] = $merged;
    // Always transition to 'configured' unless actively launching.
    // This allows the coordinator to push a fresh config after a cancelled
    // or completed attack without needing a full reset first.
    if ($state['state'] !== 'launching') {
        $state['state'] = 'configured';
    }
    save_and_respond($state, ['state_data' => $state]);
}

function handle_start_timer(&$state, $req) {
    $name = require_field($req, 'player_name');
    require_coordinator($state, $name);

    if (empty($state['attacks'])) respond_error('No attacks configured.');
    if ($state['target_village_id'] <= 0) respond_error('No target village set.');

    $stack_delay = (int)($req['stack_delay_seconds'] ?? 1);
    $fake_send   = (bool)($req['fake_send'] ?? false);
    $auto_cancel = (bool)($req['auto_cancel_on_interdict'] ?? true);

    $state['timer_settings'] = [
        'stack_delay_seconds'      => $stack_delay,
        'fake_send'                => $fake_send,
        'auto_cancel_on_interdict' => $auto_cancel,
    ];

    // Calculate scheduled send times server-side.
    // All attacks must arrive at the same base time; each stack adds stack_delay.
    // Buffer only needs to cover: poll lag (~2s) + 8s pre-prepare window + ~5s prepare call.
    // The launch thread prepares each attack just-in-time, so only the first attack's
    // prepare time matters for the buffer — subsequent attacks are handled in sequence.
    $now_ts = time();
    $max_travel = 0;
    $num_selected = 0;
    foreach ($state['attacks'] as $a) {
        if (!$a['selected']) continue;
        if ($a['travel_time_seconds'] > $max_travel) $max_travel = $a['travel_time_seconds'];
        $num_selected++;
    }

    $arrival_buffer = 15;
    $base_arrival_ts = $now_ts + $max_travel + $arrival_buffer;

    $send_times = [];
    foreach ($state['attacks'] as $a) {
        if (!$a['selected']) continue;
        $arrival_ts = $base_arrival_ts + ($a['stack'] - 1) * $stack_delay;
        $send_ts    = $arrival_ts - $a['travel_time_seconds'];
        // Key by player|village so the same village sent by two players (a player attack and
        // a vassal attack) gets independent send times instead of one overwriting the other.
        $key = $a['source_player'] . '|' . $a['source_village_id'];
        $send_times[$key] = gmdate('Y-m-d\TH:i:s\Z', (int)$send_ts);
    }

    $state['scheduled_send_times'] = $send_times;
    $state['state']                = 'launching';
    $state['launch_id']            = uniqid('abm', true);
    $state['interdict_detected']   = false;

    // Initialise army-return tracking for every player who has ≥1 selected attack.
    // Disconnected/no-attack players are not tracked so the coordinator isn't blocked.
    $tracked = [];
    foreach ($state['attacks'] as $a) {
        if ($a['selected']) $tracked[$a['source_player']] = 'waiting';
    }
    $state['armies_return_status'] = $tracked;

    // Reset all selected attack statuses for the new launch
    foreach ($state['attacks'] as &$a) {
        if ($a['selected']) $a['status'] = 'queued';
    }

    save_and_respond($state, ['state_data' => $state]);
}

function handle_attack_validated(&$state, $req) {
    $village_id = (int)require_field($req, 'source_village_id');
    $player     = isset($req['player_name']) ? $req['player_name'] : '';
    foreach ($state['attacks'] as &$a) {
        // Match on village + player so a village staged as both a player and a vassal
        // attack (different owners) updates only the correct entry.
        if ($a['source_village_id'] === $village_id &&
            ($player === '' || $a['source_player'] === $player)) {
            $a['status'] = 'validated';
            break;
        }
    }
    // Advance to 'prepared' once every selected attack has a terminal validation status
    if ($state['state'] === 'preparing') {
        $all_done = true;
        foreach ($state['attacks'] as $a) {
            if (!$a['selected']) continue;
            if (!in_array($a['status'], ['validated', 'failed_prepare', 'cancelled'])) {
                $all_done = false;
                break;
            }
        }
        if ($all_done) $state['state'] = 'prepared';
    }
    save_and_respond($state, ['state_data' => $state]);
}

function handle_prepare_attacks(&$state, $req) {
    $name = require_field($req, 'player_name');
    require_coordinator($state, $name);
    if (empty($state['attacks'])) respond_error('No attacks configured. Push config first.');
    $state['state'] = 'preparing';
    foreach ($state['attacks'] as &$a) {
        if ($a['selected'] && $a['status'] !== 'sent')
            $a['status'] = 'queued';
    }
    save_and_respond($state, ['state_data' => $state]);
}

function handle_cancel_attacks(&$state, $req) {
    $reason = isset($req['reason']) ? $req['reason'] : '';
    if ($reason === 'interdict') {
        $state['interdict_detected'] = true;
    }
    $state['state'] = 'cancelled';
    foreach ($state['attacks'] as &$a) {
        if (!in_array($a['status'], ['sent'])) {
            $a['status'] = 'cancelled';
        }
    }
    save_and_respond($state, ['state_data' => $state]);
}

function handle_attack_event(&$state, $req, $new_status) {
    $village_id = (int)require_field($req, 'source_village_id');
    $player     = isset($req['player_name']) ? $req['player_name'] : '';
    foreach ($state['attacks'] as &$a) {
        // Match on village + player to disambiguate player vs vassal entries of the same village.
        if ($a['source_village_id'] === $village_id &&
            ($player === '' || $a['source_player'] === $player)) {
            $a['status'] = $new_status;
            break;
        }
    }
    // Auto-advance state based on what was just reported
    if ($new_status === 'prepared' && $state['state'] === 'preparing') {
        // If every selected attack is now prepared (or failed/cancelled), move to prepared
        $all_validated = true;
        foreach ($state['attacks'] as $a) {
            if (!$a['selected']) continue;
            if (!in_array($a['status'], ['prepared', 'failed_prepare', 'cancelled'])) {
                $all_validated = false;
                break;
            }
        }
        if ($all_validated) $state['state'] = 'prepared';
    } elseif ($new_status === 'sent') {
        $all_done = true;
        foreach ($state['attacks'] as $a) {
            if (!$a['selected']) continue;
            if (!in_array($a['status'], ['sent', 'cancelled', 'failed'])) {
                $all_done = false;
                break;
            }
        }
        if ($all_done) $state['state'] = 'complete';
    }
    save_and_respond($state, ['state_data' => $state]);
}

function handle_attack_failed(&$state, $req, $is_prepare) {
    $village_id = (int)require_field($req, 'source_village_id');
    $reason     = isset($req['reason']) ? $req['reason'] : '';
    $player     = isset($req['player_name']) ? $req['player_name'] : '';
    foreach ($state['attacks'] as &$a) {
        // Match on village + player to disambiguate player vs vassal entries of the same village.
        if ($a['source_village_id'] === $village_id &&
            ($player === '' || $a['source_player'] === $player)) {
            $a['status']         = $is_prepare ? 'failed_prepare' : 'failed';
            $a['failure_reason'] = $reason;
            break;
        }
    }
    if (!$is_prepare) {
        // A failed send cancels remaining attacks immediately
        $state['state'] = 'cancelled';
        foreach ($state['attacks'] as &$a) {
            if ($a['status'] === 'queued') $a['status'] = 'cancelled';
        }
    } elseif ($state['state'] === 'preparing') {
        // If every selected attack now has a terminal prepare status, advance to 'prepared'
        $all_done = true;
        foreach ($state['attacks'] as $a) {
            if (!$a['selected']) continue;
            if (!in_array($a['status'], ['validated', 'failed_prepare', 'cancelled'])) {
                $all_done = false;
                break;
            }
        }
        if ($all_done) $state['state'] = 'prepared';
    }
    save_and_respond($state, ['state_data' => $state]);
}

function handle_queue_target(&$state, $req) {
    $name       = require_field($req, 'player_name');
    $village_id = (int)require_field($req, 'village_id');
    $label      = isset($req['label']) ? $req['label'] : '';
    require_coordinator($state, $name);

    $state['target_queue'][] = [
        'village_id' => $village_id,
        'label'      => $label,
        'completed'  => false,
    ];
    save_and_respond($state, ['state_data' => $state]);
}

function handle_player_disconnect(&$state, $req) {
    $name = require_field($req, 'player_name');
    $state['players'] = array_values(array_filter($state['players'], function($p) use ($name) {
        return $p['name'] !== $name;
    }));
    if ($state['coordinator'] === $name) $state['coordinator'] = '';
    save_and_respond($state, ['state_data' => $state]);
}

function handle_reset_session(&$state, $req) {
    $name = require_field($req, 'player_name');
    require_coordinator($state, $name);
    $state = make_empty_state();
    save_and_respond($state, ['state_data' => $state]);
}

function handle_report_armies_status(&$state, $req) {
    $name   = require_field($req, 'player_name');
    $status = require_field($req, 'status'); // 'waiting' or 'returned'
    if (isset($state['armies_return_status'][$name])) {
        $state['armies_return_status'][$name] = $status;
    }
    save_and_respond($state, ['state_data' => $state]);
}
