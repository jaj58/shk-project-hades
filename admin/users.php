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

    if ($action === 'add_user') {
        $username = trim($_POST['username'] ?? '');
        $email    = trim($_POST['email'] ?? '');
        if ($username && $email && filter_var($email, FILTER_VALIDATE_EMAIL)) {
            try {
                $pdo->prepare('INSERT INTO users (username, email) VALUES (?, ?)')->execute([$username, $email]);
                $msg = "User '$username' created.";
            } catch (PDOException $e) {
                $err = 'Username or email already exists.';
            }
        } else {
            $err = 'Invalid username or email address.';
        }
    }

    if ($action === 'edit_user') {
        $id    = (int)($_POST['user_id'] ?? 0);
        $uname = trim($_POST['username'] ?? '');
        $email = trim($_POST['email'] ?? '');
        if ($id && $uname && $email && filter_var($email, FILTER_VALIDATE_EMAIL)) {
            try {
                $pdo->prepare('UPDATE users SET username=?, email=? WHERE id=?')->execute([$uname, $email, $id]);
                $msg = 'User updated.';
            } catch (PDOException $e) {
                $err = 'Username or email already in use.';
            }
        }
    }

    if ($action === 'delete_user') {
        $id = (int)($_POST['user_id'] ?? 0);
        $count = $pdo->prepare('SELECT COUNT(*) FROM license_keys WHERE user_id = ?');
        $count->execute([$id]);
        if ($count->fetchColumn() > 0) {
            $err = 'Cannot delete user with existing license keys. Deactivate or delete their keys first.';
        } else {
            $pdo->prepare('DELETE FROM users WHERE id = ?')->execute([$id]);
            $msg = 'User deleted.';
        }
    }
}

$users = $pdo->query("
    SELECT u.id, u.username, u.email, u.created_at,
           COUNT(k.id) AS key_count
    FROM users u
    LEFT JOIN license_keys k ON k.user_id = u.id
    GROUP BY u.id
    ORDER BY u.created_at DESC
")->fetchAll();

layout_head('Users', 'users');
flash('success', $msg);
flash('danger', $err);
?>

<div class="d-flex justify-content-end mb-3">
    <button class="btn btn-gold" data-bs-toggle="modal" data-bs-target="#addModal">+ Add User</button>
</div>

<div class="card">
    <div class="card-body p-0">
        <table class="table table-striped mb-0">
            <thead><tr>
                <th>#</th><th>Username</th><th>Email</th><th>Keys</th><th>Created</th><th>Actions</th>
            </tr></thead>
            <tbody>
            <?php foreach ($users as $u): ?>
            <tr>
                <td class="text-muted"><?= $u['id'] ?></td>
                <td><?= htmlspecialchars($u['username']) ?></td>
                <td class="text-muted"><?= htmlspecialchars($u['email']) ?></td>
                <td><span class="badge bg-secondary"><?= $u['key_count'] ?></span></td>
                <td class="text-muted small"><?= htmlspecialchars($u['created_at']) ?></td>
                <td>
                    <button class="btn btn-sm btn-outline-secondary"
                        onclick="openEdit(<?= $u['id'] ?>, '<?= htmlspecialchars(addslashes($u['username'])) ?>', '<?= htmlspecialchars(addslashes($u['email'])) ?>')">
                        Edit
                    </button>
                    <form method="POST" class="d-inline" onsubmit="return confirm('Delete user <?= htmlspecialchars($u['username']) ?>?')">
                        <?= csrf_field() ?>
                        <input type="hidden" name="form_action" value="delete_user">
                        <input type="hidden" name="user_id" value="<?= $u['id'] ?>">
                        <button class="btn btn-sm btn-outline-danger">Delete</button>
                    </form>
                </td>
            </tr>
            <?php endforeach; ?>
            <?php if (!$users): ?>
                <tr><td colspan="6" class="text-center text-muted py-3">No users yet.</td></tr>
            <?php endif; ?>
            </tbody>
        </table>
    </div>
</div>

<!-- Add User Modal -->
<div class="modal fade" id="addModal" tabindex="-1">
  <div class="modal-dialog">
    <div class="modal-content bg-dark text-light border-secondary">
      <div class="modal-header border-secondary">
        <h5 class="modal-title">Add User</h5>
        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
      </div>
      <form method="POST">
        <?= csrf_field() ?>
        <input type="hidden" name="form_action" value="add_user">
        <div class="modal-body">
          <div class="mb-3">
            <label class="form-label">Username</label>
            <input type="text" name="username" class="form-control" required>
          </div>
          <div class="mb-3">
            <label class="form-label">Email</label>
            <input type="email" name="email" class="form-control" required>
          </div>
        </div>
        <div class="modal-footer border-secondary">
          <button class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
          <button class="btn btn-gold">Create User</button>
        </div>
      </form>
    </div>
  </div>
</div>

<!-- Edit User Modal -->
<div class="modal fade" id="editModal" tabindex="-1">
  <div class="modal-dialog">
    <div class="modal-content bg-dark text-light border-secondary">
      <div class="modal-header border-secondary">
        <h5 class="modal-title">Edit User</h5>
        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
      </div>
      <form method="POST">
        <?= csrf_field() ?>
        <input type="hidden" name="form_action" value="edit_user">
        <input type="hidden" name="user_id" id="edit_user_id">
        <div class="modal-body">
          <div class="mb-3">
            <label class="form-label">Username</label>
            <input type="text" name="username" id="edit_username" class="form-control" required>
          </div>
          <div class="mb-3">
            <label class="form-label">Email</label>
            <input type="email" name="email" id="edit_email" class="form-control" required>
          </div>
        </div>
        <div class="modal-footer border-secondary">
          <button class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
          <button class="btn btn-gold">Save Changes</button>
        </div>
      </form>
    </div>
  </div>
</div>

<script>
function openEdit(id, username, email) {
    document.getElementById('edit_user_id').value = id;
    document.getElementById('edit_username').value = username;
    document.getElementById('edit_email').value = email;
    new bootstrap.Modal(document.getElementById('editModal')).show();
}
</script>
<?php layout_foot(); ?>
