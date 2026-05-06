<?php
if (session_status() === PHP_SESSION_NONE) session_start();
require_once __DIR__ . '/auth.php';

// Already logged in — go to dashboard
if (!empty($_SESSION[ADMIN_SESSION_KEY])) {
    header('Location: dashboard.php');
    exit;
}

$error = $_SESSION['login_error'] ?? '';
unset($_SESSION['login_error']);
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Hades Admin — Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        body { background: #0f0f0f; }
        .login-card { max-width: 380px; margin: 10vh auto; }
        .card { background: #1a1a1a; border: 1px solid #333; }
        .card-header { background: #111; border-bottom: 1px solid #333; }
        h5 { color: #c8a800; letter-spacing: 1px; }
        label { color: #aaa; }
    </style>
</head>
<body>
<div class="login-card">
    <div class="card shadow-lg">
        <div class="card-header py-3 text-center">
            <h5 class="mb-0">⚔ HADES ADMIN</h5>
        </div>
        <div class="card-body p-4">
            <?php if ($error): ?>
                <div class="alert alert-danger py-2"><?= htmlspecialchars($error) ?></div>
            <?php endif; ?>
            <form method="POST" action="auth.php">
                <input type="hidden" name="login" value="1">
                <div class="mb-3">
                    <label class="form-label">Username</label>
                    <input type="text" name="username" class="form-control bg-dark text-light border-secondary" required autofocus>
                </div>
                <div class="mb-3">
                    <label class="form-label">Password</label>
                    <input type="password" name="password" class="form-control bg-dark text-light border-secondary" required>
                </div>
                <button type="submit" class="btn btn-warning w-100">Sign In</button>
            </form>
        </div>
    </div>
</div>
</body>
</html>
