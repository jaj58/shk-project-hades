<?php
require_once __DIR__ . '/auth.php';
require_once __DIR__ . '/db.php';
require_once __DIR__ . '/_layout.php';
require_auth();

$pdo = get_db();

$per_page   = 50;
$page       = max(1, (int)($_GET['page'] ?? 1));
$offset     = ($page - 1) * $per_page;

$filter_action = $_GET['action'] ?? '';
$filter_key    = trim($_GET['key'] ?? '');
$filter_from   = trim($_GET['from'] ?? '');
$filter_to     = trim($_GET['to']   ?? '');

$where  = [];
$params = [];

if ($filter_action) {
    $where[]  = 'l.action = ?';
    $params[] = $filter_action;
}
if ($filter_key) {
    $where[]  = 'k.key_string LIKE ?';
    $params[] = '%' . $filter_key . '%';
}
if ($filter_from) {
    $where[]  = 'l.created_at >= ?';
    $params[] = $filter_from . ' 00:00:00';
}
if ($filter_to) {
    $where[]  = 'l.created_at <= ?';
    $params[] = $filter_to . ' 23:59:59';
}

$w_sql = $where ? ' WHERE ' . implode(' AND ', $where) : '';

$total = $pdo->prepare("SELECT COUNT(*) FROM license_log l JOIN license_keys k ON k.id = l.key_id" . $w_sql);
$total->execute($params);
$total_rows = (int)$total->fetchColumn();
$total_pages = max(1, (int)ceil($total_rows / $per_page));

$stmt = $pdo->prepare(
    "SELECT l.id, l.created_at, l.action, l.hwid, l.ip,
            k.key_string
     FROM license_log l
     JOIN license_keys k ON k.id = l.key_id"
    . $w_sql .
    " ORDER BY l.created_at DESC LIMIT {$per_page} OFFSET {$offset}"
);
$stmt->execute($params);
$logs = $stmt->fetchAll();

$actions = ['validate', 'verify', 'hwid_bind', 'denied'];

layout_head('Logs', 'logs');
?>

<form class="row g-2 mb-3" method="GET">
    <div class="col-auto">
        <select name="action" class="form-select form-select-sm">
            <option value="">All Actions</option>
            <?php foreach ($actions as $a): ?>
            <option value="<?= $a ?>" <?= $filter_action === $a ? 'selected' : '' ?>><?= $a ?></option>
            <?php endforeach; ?>
        </select>
    </div>
    <div class="col-auto">
        <input type="text" name="key" class="form-control form-control-sm" placeholder="Search key…" value="<?= htmlspecialchars($filter_key) ?>" style="width:200px">
    </div>
    <div class="col-auto">
        <input type="date" name="from" class="form-control form-control-sm" value="<?= htmlspecialchars($filter_from) ?>">
    </div>
    <div class="col-auto">
        <input type="date" name="to" class="form-control form-control-sm" value="<?= htmlspecialchars($filter_to) ?>">
    </div>
    <div class="col-auto">
        <button class="btn btn-sm btn-outline-secondary">Filter</button>
        <a href="logs.php" class="btn btn-sm btn-outline-secondary">Reset</a>
    </div>
    <div class="col-auto ms-auto text-muted small align-self-center"><?= number_format($total_rows) ?> entries</div>
</form>

<div class="card">
    <div class="card-body p-0">
        <table class="table table-striped mb-0 small">
            <thead><tr>
                <th>Time (UTC)</th><th>Key</th><th>Action</th><th>HWID</th><th>IP</th>
            </tr></thead>
            <tbody>
            <?php foreach ($logs as $log): ?>
            <tr>
                <td class="text-muted"><?= htmlspecialchars($log['created_at']) ?></td>
                <td><code><?= htmlspecialchars(mask_key($log['key_string'])) ?></code></td>
                <td><?= action_badge($log['action']) ?></td>
                <td class="text-muted" title="<?= htmlspecialchars($log['hwid'] ?? '') ?>">
                    <?= $log['hwid'] ? substr(htmlspecialchars($log['hwid']), 0, 14) . '…' : '—' ?>
                </td>
                <td class="text-muted"><?= htmlspecialchars($log['ip'] ?? '—') ?></td>
            </tr>
            <?php endforeach; ?>
            <?php if (!$logs): ?>
                <tr><td colspan="5" class="text-center text-muted py-3">No log entries found.</td></tr>
            <?php endif; ?>
            </tbody>
        </table>
    </div>
</div>

<!-- Pagination -->
<?php if ($total_pages > 1): ?>
<nav class="mt-3">
    <ul class="pagination pagination-sm justify-content-center">
        <?php for ($i = 1; $i <= $total_pages; $i++):
            $q = http_build_query(array_merge($_GET, ['page' => $i]));
        ?>
        <li class="page-item <?= $i === $page ? 'active' : '' ?>">
            <a class="page-link bg-dark border-secondary text-light" href="?<?= $q ?>"><?= $i ?></a>
        </li>
        <?php endfor; ?>
    </ul>
</nav>
<?php endif; ?>

<?php
layout_foot();

function mask_key(string $key): string {
    $parts = explode('-', $key);
    return count($parts) === 4 ? $parts[0] . '-****-****-' . $parts[3] : substr($key, 0, 6) . '****';
}

function action_badge(string $action): string {
    $map = ['validate' => 'success', 'verify' => 'primary', 'hwid_bind' => 'warning', 'denied' => 'danger'];
    $c = $map[$action] ?? 'secondary';
    return '<span class="badge bg-' . $c . '">' . htmlspecialchars($action) . '</span>';
}
