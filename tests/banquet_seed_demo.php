<?php
/**
 * Seeds a demo group for eyeballing the website: 4 players (the example setup —
 * everyone makes venison/furniture/clothes, one each of metalware+wine / salt /
 * spice / silk, plus some wood/stone/iron/pitch, food and weapons), sets every
 * category up differently, and simulates some sends. Dunstan runs an older bot that
 * only reports banquet goods.
 *   php tests/banquet_seed_demo.php http://host/api/banquet_api.php demo-key
 */
$url = $argv[1] ?? 'http://localhost/api/banquet_api.php';
$key = $argv[2] ?? 'demo-banquet';
$G = time() + 3600; // pretend game clock
const N = 24;

function call($action, $body) {
    global $url, $key;
    $body['action'] = $action; $body['key'] = $key;
    $raw = file_get_contents($url, false, stream_context_create(['http' => ['method' => 'POST',
        'header' => "Content-Type: application/json\r\n", 'content' => json_encode($body), 'ignore_errors' => true]]));
    $j = json_decode($raw, true);
    if (!is_array($j) || !$j['ok']) { fwrite(STDERR, "$action failed: $raw\n"); exit(1); }
    return $j;
}

// [name, goods made (catalogue indices), old bot]
$players = [
    101 => ['Aldric',   [0, 1, 2, 3, 4, 8, 9, 12, 18, 19], false],
    102 => ['Brenna',   [0, 1, 3, 5, 8, 10, 13, 15, 20], false],
    103 => ['Cedric',   [0, 1, 3, 6, 9, 11, 14, 16, 17, 21, 22], false],
    104 => ['Dunstan',  [0, 1, 3, 7], true],
];
$names = ['Oakridge', 'Millbrook', 'Stonehaven', 'Ashford'];
$capByCat = [8 => 15000, 12 => 9000, 19 => 600];
function cap_for(int $g, int $hall): int {
    if ($g < 8) return $hall;
    if ($g < 12) return 15000;
    if ($g < 19) return 9000;
    return 600;
}
$villages = [];
$vid = 1000;
foreach ($players as $uid => [$pname, $makes, $old]) {
    $list = [];
    for ($i = 0; $i < 3; $i++) {
        $vid++;
        $hall = $uid === 101 ? 5400 : 2700;
        $n = $old ? 8 : N;
        $levels = []; $prod = []; $b = []; $caps = [];
        for ($g = 0; $g < $n; $g++) {
            $m = in_array($g, $makes, true) && ($i < 2 || $g <= 3 || $g >= 8);
            $cap = cap_for($g, $hall);
            $levels[] = $m ? (int)($cap * 0.7) + ($vid * 37 + $g * 101) % (int)($cap * 0.25)
                           : ($vid * 53 + $g * 71) % max(1, (int)($cap * 0.2));
            $prod[] = $m && ($g < 12) ? ($g < 8 ? 300 + $g * 25 : 2000 + $g * 50) : 0;
            $b[] = $m ? 1 + ($vid + $g) % 2 : 0;
            $caps[] = $cap;
        }
        $v = ['village_id' => $vid, 'name' => $names[$i] . ' ' . substr($pname, 0, 1), 'x' => 100 + $uid * 3 + $i * 7,
            'y' => 200 + $i * 5, 'has_hall' => !($uid === 104 && $i === 2), 'hall_cap' => $hall,
            'levels' => $levels, 'prod' => $prod, 'buildings' => $b, 'levels_at_game' => $G,
            'snapshot_at_game' => $G - 60 * $i, 'merchants_free' => 40 + $i * 10];
        if (!$old) {
            $v['caps'] = $caps;
            $v['merchants_home'] = 40 + $i * 10;
            $v['storage'] = ['hall' => true, 'stockpile' => true, 'granary' => !($uid === 102 && $i === 2),
                'inn' => $i !== 1, 'armoury' => $uid !== 103];
        }
        $list[] = $v;
    }
    $villages[$uid] = $list;
}

// Cards data as the bot reports it (banquet goods only): potential production per good
// with/without cards, and the banquet-relevant cards in play.
function demo_cards($uid) {
    global $villages;
    $with = array_fill(0, 8, 0.0);
    foreach ($villages[$uid] as $v) {
        for ($g = 0; $g < 8; $g++) $with[$g] += $v['prod'][$g];
    }
    $without = $with;
    $inPlay = [];
    if ($uid === 101) {
        $without[0] = round($with[0] / 10, 1);
        $inPlay[] = ['id' => 1286, 'name' => 'Venison production x10', 'expires_in_sec' => 5 * 3600];
        $inPlay[] = ['id' => 2822, 'name' => 'Expanded Keep Storage (hall x2)', 'expires_in_sec' => 26 * 3600];
    }
    if ($uid === 102) {
        $without[5] = round($with[5] / 5, 1);
        $inPlay[] = ['id' => 1300, 'name' => 'Salt production x5', 'expires_in_sec' => 2 * 3600 + 600];
        $inPlay[] = ['id' => 1538, 'name' => 'Advanced Carters (merchants x4)', 'expires_in_sec' => 40 * 60];
    }
    return ['hall_multiplier' => $uid === 101 ? 2 : 1, 'merchant_speed' => $uid === 102 ? 4 : 1,
        'prod_with_cards' => $with, 'prod_without_cards' => $without, 'in_play' => $inPlay];
}

$sync = function ($uid, $extra = []) use (&$villages, $players, &$G) {
    $old = $players[$uid][2];
    $player = ['user_id' => $uid, 'name' => $players[$uid][0], 'world' => 'Global Conflict 7', 'game_time' => $G,
        'craftsmanship' => $uid === 104 ? 6 : 8, 'sec_per_tile' => 4.2, 'carry' => array_fill(0, $old ? 8 : N, 10),
        'client_version' => $old ? 'old-demo' : 'demo', 'cards' => demo_cards($uid)];
    if (!$old) {
        $player['client'] = [
            'banquet'   => ['enabled' => true, 'reserve' => 0],
            'resources' => ['enabled' => true, 'reserve' => $uid === 101 ? 10 : 0],
            'food'      => ['enabled' => $uid !== 103, 'reserve' => 0],
            'weapons'   => ['enabled' => true, 'reserve' => 0],
        ];
    }
    return call('sync', array_merge(['player' => $player, 'villages' => $villages[$uid]], $extra));
};

foreach ($players as $uid => $_) $sync($uid);
call('set_settings', ['settings' => [
    'max_orders_per_player' => 12,
    'category_order' => ['banquet', 'food', 'resources', 'weapons'],
    'categories' => [
        'banquet'   => ['mode' => 'autofill', 'keep_amount' => 1000, 'min_send' => 100, 'player_caps' => ['103' => 2000]],
        'resources' => ['mode' => 'autofill', 'keep_amount' => 3000, 'min_send' => 500, 'balance_goods' => true],
        'food'      => ['mode' => 'focus', 'focus_players' => [102], 'keep_amount' => 2000, 'min_send' => 200],
        'weapons'   => ['mode' => 'off'],
    ],
]]);

// Each player syncs, "sends" its first two orders and reports them.
$trader = 500000;
foreach ($players as $uid => $_) {
    $r = $sync($uid);
    $results = []; $outbound = [];
    foreach (array_slice($r['orders'], 0, 4) as $i => $o) {
        $trader++;
        $eta = $G + 300 + $i * 420 + $uid;
        $results[] = ['order_id' => $o['order_id'], 'outcome' => 'sent', 'trader_id' => $trader, 'amount' => $o['amount'], 'eta_game' => $eta];
        $outbound[] = ['trader_id' => $trader, 'from_village_id' => $o['from_village_id'], 'to_village_id' => $o['to_village_id'],
            'good' => $o['good'], 'amount' => $o['amount'], 'eta_game' => $eta];
    }
    if (isset($r['orders'][4])) $results[] = ['order_id' => $r['orders'][4]['order_id'], 'outcome' => 'rejected', 'error' => 'Not enough traders'];
    $sync($uid, ['results' => $results, 'outbound' => $outbound]);
}
echo "Seeded group '$key'.\n";
