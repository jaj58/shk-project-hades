<?php
/**
 * Seeds a demo group for eyeballing the website: 4 players (the example setup —
 * everyone makes venison/furniture/clothes, one each of metalware+wine / salt /
 * spice / silk), switches to autofill, and simulates some sends.
 *   php tests/banquet_seed_demo.php http://host/api/banquet_api.php demo-key
 */
$url = $argv[1] ?? 'http://localhost/api/banquet_api.php';
$key = $argv[2] ?? 'demo-banquet';
$G = time() + 3600; // pretend game clock

function call($action, $body) {
    global $url, $key;
    $body['action'] = $action; $body['key'] = $key;
    $raw = file_get_contents($url, false, stream_context_create(['http' => ['method' => 'POST',
        'header' => "Content-Type: application/json\r\n", 'content' => json_encode($body), 'ignore_errors' => true]]));
    $j = json_decode($raw, true);
    if (!is_array($j) || !$j['ok']) { fwrite(STDERR, "$action failed: $raw\n"); exit(1); }
    return $j;
}

$players = [
    101 => ['Aldric',   [0, 1, 2, 3, 4]],
    102 => ['Brenna',   [0, 1, 3, 5]],
    103 => ['Cedric',   [0, 1, 3, 6]],
    104 => ['Dunstan',  [0, 1, 3, 7]],
];
$names = ['Oakridge', 'Millbrook', 'Stonehaven', 'Ashford'];
$villages = [];
$vid = 1000;
foreach ($players as $uid => [$pname, $makes]) {
    $list = [];
    for ($i = 0; $i < 3; $i++) {
        $vid++;
        $levels = []; $prod = []; $b = [];
        for ($g = 0; $g < 8; $g++) {
            $m = in_array($g, $makes, true) && ($i < 2 || $g <= 3);
            $levels[] = $m ? 2000 + ($vid * 37 + $g * 101) % 700 : ($vid * 53 + $g * 71) % 600;
            $prod[] = $m ? 300 + $g * 25 : 0;
            $b[] = $m ? 1 + ($vid + $g) % 2 : 0;
        }
        $list[] = ['village_id' => $vid, 'name' => $names[$i] . ' ' . substr($pname, 0, 1), 'x' => 100 + $uid * 3 + $i * 7,
            'y' => 200 + $i * 5, 'has_hall' => !($uid === 104 && $i === 2), 'hall_cap' => $uid === 101 ? 5400 : 2700,
            'levels' => $levels, 'prod' => $prod, 'buildings' => $b, 'levels_at_game' => $G,
            'snapshot_at_game' => $G - 60 * $i, 'merchants_free' => 40 + $i * 10];
    }
    $villages[$uid] = $list;
}

$sync = function ($uid, $extra = []) use (&$villages, $players, &$G) {
    return call('sync', array_merge([
        'player' => ['user_id' => $uid, 'name' => $players[$uid][0], 'world' => 'Global Conflict 7', 'game_time' => $G,
            'craftsmanship' => $uid === 104 ? 6 : 8, 'sec_per_tile' => 4.2, 'carry' => array_fill(0, 8, 10),
            'client_version' => 'demo', 'cards' => ['hall_multiplier' => $uid === 101 ? 2 : 1, 'merchant_speed' => $uid === 102 ? 4 : 1]],
        'villages' => $villages[$uid],
    ], $extra));
};

foreach ($players as $uid => $_) $sync($uid);
call('set_settings', ['settings' => ['mode' => 'autofill', 'keep_amount' => 1000, 'min_send' => 100,
    'player_caps' => ['103' => 2000]]]);

// Each player syncs, "sends" its first two orders and reports them.
$trader = 500000;
foreach ($players as $uid => $_) {
    $r = $sync($uid);
    $results = []; $outbound = [];
    foreach (array_slice($r['orders'], 0, 2) as $i => $o) {
        $trader++;
        $eta = $G + 300 + $i * 420 + $uid;
        $results[] = ['order_id' => $o['order_id'], 'outcome' => 'sent', 'trader_id' => $trader, 'amount' => $o['amount'], 'eta_game' => $eta];
        $outbound[] = ['trader_id' => $trader, 'from_village_id' => $o['from_village_id'], 'to_village_id' => $o['to_village_id'],
            'good' => $o['good'], 'amount' => $o['amount'], 'eta_game' => $eta];
    }
    if (isset($r['orders'][2])) $results[] = ['order_id' => $r['orders'][2]['order_id'], 'outcome' => 'rejected', 'error' => 'Not enough traders'];
    $sync($uid, ['results' => $results, 'outbound' => $outbound]);
}
echo "Seeded group '$key'.\n";
