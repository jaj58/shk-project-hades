<?php
require_once __DIR__ . '/auth.php';
require_once __DIR__ . '/db.php';
require_once __DIR__ . '/_layout.php';
require_auth();

$pdo = get_db();
$msg = $err = '';

// --- Handle POST actions ---
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    verify_csrf();
    $action = $_POST['form_action'] ?? '';

    if ($action === 'add_key') {
        $user_id = (int)($_POST['user_id'] ?? 0);
        $days    = (int)($_POST['days'] ?? 30);
        $custom  = trim($_POST['custom_until'] ?? '');

        if ($user_id > 0) {
            $valid_until = $custom
                ? date('Y-m-d H:i:s', strtotime($custom . ' 23:59:59'))
                : date('Y-m-d H:i:s', strtotime("+{$days} days"));

            $key_string = generate_key();
            try {
                $pdo->prepare(
                    'INSERT INTO license_keys (key_string, user_id, valid_from, valid_until)
                     VALUES (?, ?, NOW(), ?)'
                )->execute([$key_string, $user_id, $valid_until]);
                $msg = "Key created: <code>{$key_string}</code>";
            } catch (PDOException $e) {
                $err = 'Failed to create key: ' . $e->getMessage();
            }
        } else {
            $err = 'Please select a user.';
        }
    }

    if ($action === 'toggle_active') {
        $id  = (int)($_POST['key_id'] ?? 0);
        $val = (int)($_POST['new_val'] ?? 0);
        $pdo->prepare('UPDATE license_keys SET is_active = ? WHERE id = ?')->execute([$val, $id]);
        $msg = 'Key ' . ($val ? 'activated' : 'deactivated') . '.';
    }

    if ($action === 'reset_hwid') {
        $id = (int)($_POST['key_id'] ?? 0);
        $pdo->prepare('UPDATE license_keys SET hwid = NULL WHERE id = ?')->execute([$id]);
        $msg = 'HWID reset. The key can now be bound to a new machine.';
    }

    if ($action === 'edit_expiry') {
        $id    = (int)($_POST['key_id'] ?? 0);
        $until = trim($_POST['valid_until'] ?? '');
        if ($id && $until) {
            $pdo->prepare('UPDATE license_keys SET valid_until = ? WHERE id = ?')
                ->execute([date('Y-m-d H:i:s', strtotime($until)), $id]);
            $msg = 'Expiry updated.';
        }
    }

    if ($action === 'delete_key') {
        $id = (int)($_POST['key_id'] ?? 0);
        $pdo->prepare('DELETE FROM license_keys WHERE id = ?')->execute([$id]);
        $msg = 'Key deleted.';
    }
}

// --- Filters ---
$filter_user   = (int)($_GET['user_id'] ?? 0);
$filter_status = $_GET['status'] ?? '';

$where  = [];
$params = [];

if ($filter_user > 0) {
    $where[]  = 'k.user_id = ?';
    $params[] = $filter_user;
}
if ($filter_status === 'active') {
    $where[] = 'k.is_active = 1 AND k.valid_until >= NOW()';
} elseif ($filter_status === 'expired') {
    $where[] = 'k.valid_until < NOW()';
} elseif ($filter_status === 'disabled') {
    $where[] = 'k.is_active = 0';
}

$sql = "SELECT k.*, u.username,
               (SELECT MAX(l.created_at) FROM license_log l WHERE l.key_id = k.id) AS last_seen
        FROM license_keys k
        JOIN users u ON u.id = k.user_id"
    . ($where ? ' WHERE ' . implode(' AND ', $where) : '')
    . " ORDER BY k.created_at DESC";

$keys = $pdo->prepare($sql);
$keys->execute($params);
$keys = $keys->fetchAll();

$users_list = $pdo->query('SELECT id, username FROM users ORDER BY username')->fetchAll();

layout_head('Licenses', 'licenses');
flash('success', $msg);
flash('danger', $err);
?>

<div class="d-flex justify-content-between align-items-center mb-3">
    <form class="d-flex gap-2" method="GET">
        <select name="user_id" class="form-select form-select-sm" style="width:180px">
            <option value="">All Users</option>
            <?php foreach ($users_list as $u): ?>
            <option value="<?= $u['id'] ?>" <?= $filter_user == $u['id'] ? 'selected' : '' ?>><?= htmlspecialchars($u['username']) ?></option>
            <?php endforeach; ?>
        </select>
        <select name="status" class="form-select form-select-sm" style="width:140px">
            <option value="">All Status</option>
            <option value="active"   <?= $filter_status === 'active'   ? 'selected' : '' ?>>Active</option>
            <option value="expired"  <?= $filter_status === 'expired'  ? 'selected' : '' ?>>Expired</option>
            <option value="disabled" <?= $filter_status === 'disabled' ? 'selected' : '' ?>>Disabled</option>
        </select>
        <button class="btn btn-sm btn-outline-secondary">Filter</button>
        <a href="licenses.php" class="btn btn-sm btn-outline-secondary">Reset</a>
    </form>
    <button class="btn btn-gold" data-bs-toggle="modal" data-bs-target="#addModal">+ Add Key</button>
</div>

<div class="card">
    <div class="card-body p-0">
        <table class="table table-striped mb-0 small">
            <thead><tr>
                <th>Key</th><th>User</th><th>Valid Until</th><th>Status</th><th>HWID</th><th>Last Seen</th><th>Actions</th>
            </tr></thead>
            <tbody>
            <?php foreach ($keys as $k):
                $expired  = strtotime($k['valid_until']) < time();
                $disabled = !$k['is_active'];
                $status_badge = $disabled
                    ? '<span class="badge badge-disabled">Disabled</span>'
                    : ($expired
                        ? '<span class="badge badge-expired">Expired</span>'
                        : '<span class="badge badge-active">Active</span>');
            ?>
            <tr>
                <td><code><?= htmlspecialchars($k['key_string']) ?></code></td>
                <td><?= htmlspecialchars($k['username']) ?></td>
                <td class="<?= $expired ? 'text-danger' : 'text-muted' ?>"><?= htmlspecialchars(substr($k['valid_until'], 0, 10)) ?></td>
                <td><?= $status_badge ?></td>
                <td class="text-muted">
                    <?php if ($k['hwid']): ?>
                        <span title="<?= htmlspecialchars($k['hwid']) ?>"><?= substr(htmlspecialchars($k['hwid']), 0, 10) ?>…</span>
                    <?php else: ?>
                        <span class="text-warning">Unbound</span>
                    <?php endif; ?>
                </td>
                <td class="text-muted"><?= $k['last_seen'] ? htmlspecialchars(substr($k['last_seen'], 0, 16)) : '—' ?></td>
                <td>
                    <div class="d-flex gap-1 flex-wrap">
                        <!-- Toggle active -->
                        <form method="POST" class="d-inline">
                            <?= csrf_field() ?>
                            <input type="hidden" name="form_action" value="toggle_active">
                            <input type="hidden" name="key_id" value="<?= $k['id'] ?>">
                            <input type="hidden" name="new_val" value="<?= $k['is_active'] ? 0 : 1 ?>">
                            <button class="btn btn-sm <?= $k['is_active'] ? 'btn-outline-warning' : 'btn-outline-success' ?>">
                                <?= $k['is_active'] ? 'Disable' : 'Enable' ?>
                            </button>
                        </form>
                        <!-- Reset HWID -->
                        <?php if ($k['hwid']): ?>
                        <form method="POST" class="d-inline" onsubmit="return confirm('Reset HWID for this key?')">
                            <?= csrf_field() ?>
                            <input type="hidden" name="form_action" value="reset_hwid">
                            <input type="hidden" name="key_id" value="<?= $k['id'] ?>">
                            <button class="btn btn-sm btn-outline-info">Reset HWID</button>
                        </form>
                        <?php endif; ?>
                        <!-- Edit expiry -->
                        <button class="btn btn-sm btn-outline-secondary"
                            onclick="openExpiry(<?= $k['id'] ?>, '<?= substr($k['valid_until'], 0, 10) ?>')">
                            Expiry
                        </button>
                        <!-- Delete -->
                        <form method="POST" class="d-inline" onsubmit="return confirm('Permanently delete this key?')">
                            <?= csrf_field() ?>
                            <input type="hidden" name="form_action" value="delete_key">
                            <input type="hidden" name="key_id" value="<?= $k['id'] ?>">
                            <button class="btn btn-sm btn-outline-danger">Delete</button>
                        </form>
                    </div>
                </td>
            </tr>
            <?php endforeach; ?>
            <?php if (!$keys): ?>
                <tr><td colspan="7" class="text-center text-muted py-3">No license keys found.</td></tr>
            <?php endif; ?>
            </tbody>
        </table>
    </div>
</div>

<!-- Add Key Modal -->
<div class="modal fade" id="addModal" tabindex="-1">
  <div class="modal-dialog">
    <div class="modal-content bg-dark text-light border-secondary">
      <div class="modal-header border-secondary">
        <h5 class="modal-title">Add License Key</h5>
        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
      </div>
      <form method="POST">
        <?= csrf_field() ?>
        <input type="hidden" name="form_action" value="add_key">
        <div class="modal-body">
          <div class="mb-3">
            <label class="form-label">Assign to User</label>
            <select name="user_id" class="form-select" required>
              <option value="">Select user…</option>
              <?php foreach ($users_list as $u): ?>
              <option value="<?= $u['id'] ?>"><?= htmlspecialchars($u['username']) ?></option>
              <?php endforeach; ?>
            </select>
          </div>
          <div class="mb-3">
            <label class="form-label">Validity Period</label>
            <select name="days" class="form-select" id="daysSelect" onchange="toggleCustomDate(this)">
              <option value="30">30 days</option>
              <option value="90">90 days</option>
              <option value="180">180 days</option>
              <option value="365">1 year</option>
              <option value="0">Custom date…</option>
            </select>
          </div>
          <div class="mb-3 d-none" id="customDateRow">
            <label class="form-label">Expire on</label>
            <input type="date" name="custom_until" class="form-control">
          </div>
        </div>
        <div class="modal-footer border-secondary">
          <button class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
          <button class="btn btn-gold">Generate Key</button>
        </div>
      </form>
    </div>
  </div>
</div>

<!-- Edit Expiry Modal -->
<div class="modal fade" id="expiryModal" tabindex="-1">
  <div class="modal-dialog modal-sm">
    <div class="modal-content bg-dark text-light border-secondary">
      <div class="modal-header border-secondary">
        <h5 class="modal-title">Edit Expiry</h5>
        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
      </div>
      <form method="POST">
        <?= csrf_field() ?>
        <input type="hidden" name="form_action" value="edit_expiry">
        <input type="hidden" name="key_id" id="expiry_key_id">
        <div class="modal-body">
          <label class="form-label">New expiry date</label>
          <input type="date" name="valid_until" id="expiry_date" class="form-control" required>
        </div>
        <div class="modal-footer border-secondary">
          <button class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
          <button class="btn btn-gold">Save</button>
        </div>
      </form>
    </div>
  </div>
</div>

<script>
function toggleCustomDate(sel) {
    document.getElementById('customDateRow').classList.toggle('d-none', sel.value !== '0');
}
function openExpiry(id, date) {
    document.getElementById('expiry_key_id').value = id;
    document.getElementById('expiry_date').value = date;
    new bootstrap.Modal(document.getElementById('expiryModal')).show();
}
</script>
<?php
layout_foot();

function generate_key(): string {
    $a = strtoupper(bin2hex(random_bytes(3)));
    $b = strtoupper(bin2hex(random_bytes(3)));
    $c = strtoupper(bin2hex(random_bytes(3)));
    return "HADES-{$a}-{$b}-{$c}";
}
