<?php
require_once __DIR__ . '/auth.php';
require_once __DIR__ . '/db.php';
require_once __DIR__ . '/_layout.php';
require_auth();

$pdo = get_db();

$stats = $pdo->query("
    SELECT
        (SELECT COUNT(*) FROM users) AS total_users,
        (SELECT COUNT(*) FROM license_keys) AS total_keys,
        (SELECT COUNT(*) FROM license_keys WHERE is_active = 1 AND valid_until >= NOW()) AS active_keys,
        (SELECT COUNT(*) FROM license_keys WHERE valid_until < NOW()) AS expired_keys,
        (SELECT COUNT(*) FROM license_keys WHERE is_active = 0) AS disabled_keys,
        (SELECT COUNT(*) FROM license_keys WHERE hwid IS NOT NULL) AS bound_keys,
        (SELECT COUNT(*) FROM license_keys WHERE hwid IS NULL) AS unbound_keys
")->fetch();

$recent_logs = $pdo->query("
    SELECT l.created_at, l.action, l.hwid, l.ip,
           k.key_string
    FROM license_log l
    JOIN license_keys k ON k.id = l.key_id
    ORDER BY l.created_at DESC
    LIMIT 15
")->fetchAll();

$version_file = __DIR__ . '/../api/version.json';
$version_data = file_exists($version_file) ? json_decode(file_get_contents($version_file), true) : [];
$current_version = $version_data['version'] ?? 'Unknown';

layout_head('Dashboard', 'dashboard');
?>

<div class="row g-3 mb-4">
    <?php
    $cards = [
        ['Users',          $stats['total_users'],   'primary'],
        ['Total Keys',     $stats['total_keys'],     'secondary'],
        ['Active Keys',    $stats['active_keys'],    'success'],
        ['Expired Keys',   $stats['expired_keys'],   'danger'],
        ['Disabled Keys',  $stats['disabled_keys'],  'warning'],
        ['HWID Bound',     $stats['bound_keys'],     'info'],
    ];
    foreach ($cards as [$label, $value, $color]):
    ?>
    <div class="col-md-2 col-sm-4">
        <div class="card stat-card text-center p-3">
            <div class="number"><?= $value ?></div>
            <div class="text-muted small"><?= $label ?></div>
        </div>
    </div>
    <?php endforeach; ?>
</div>

<div class="row g-3">
    <div class="col-md-8">
        <div class="card">
            <div class="card-header">Recent API Activity</div>
            <div class="card-body p-0">
                <table class="table table-striped mb-0 small">
                    <thead><tr>
                        <th>Time</th><th>Key</th><th>Action</th><th>HWID</th><th>IP</th>
                    </tr></thead>
                    <tbody>
                    <?php foreach ($recent_logs as $log): ?>
                    <tr>
                        <td class="text-muted"><?= htmlspecialchars($log['created_at']) ?></td>
                        <td><code><?= htmlspecialchars(mask_key($log['key_string'])) ?></code></td>
                        <td><?= action_badge($log['action']) ?></td>
                        <td class="text-muted"><?= $log['hwid'] ? substr(htmlspecialchars($log['hwid']), 0, 12) . '…' : '—' ?></td>
                        <td class="text-muted"><?= htmlspecialchars($log['ip'] ?? '—') ?></td>
                    </tr>
                    <?php endforeach; ?>
                    <?php if (!$recent_logs): ?>
                        <tr><td colspan="5" class="text-center text-muted py-3">No activity yet.</td></tr>
                    <?php endif; ?>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="card">
            <div class="card-header">Current Release</div>
            <div class="card-body">
                <div class="text-center mb-3">
                    <span class="badge bg-success fs-5 px-3 py-2">v<?= htmlspecialchars($current_version) ?></span>
                </div>
                <div class="small text-muted mb-1">Download URL:</div>
                <div class="small text-break mb-3" style="color:#888"><?= htmlspecialchars($version_data['download_url'] ?? '—') ?></div>
                <a href="settings.php" class="btn btn-gold btn-sm w-100">Edit Settings</a>
                <a href="upload.php" class="btn btn-outline-secondary btn-sm w-100 mt-2">Upload New Release</a>
            </div>
        </div>
    </div>
</div>

<?php
layout_foot();

function mask_key(string $key): string {
    $parts = explode('-', $key);
    if (count($parts) === 4) {
        return $parts[0] . '-****-****-' . $parts[3];
    }
    return substr($key, 0, 6) . '****';
}

function action_badge(string $action): string {
    $map = [
        'validate'  => 'success',
        'verify'    => 'primary',
        'hwid_bind' => 'warning',
        'denied'    => 'danger',
    ];
    $color = $map[$action] ?? 'secondary';
    return '<span class="badge bg-' . $color . '">' . htmlspecialchars($action) . '</span>';
}
