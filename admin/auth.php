<?php
if (session_status() === PHP_SESSION_NONE) session_start();
require_once __DIR__ . '/admin_config.php';

// Handle login POST
if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['login'])) {
    if (
        isset($_POST['username'], $_POST['password']) &&
        $_POST['username'] === ADMIN_USER &&
        password_verify($_POST['password'], ADMIN_PASS_HASH)
    ) {
        session_regenerate_id(true);
        $_SESSION[ADMIN_SESSION_KEY] = true;
        header('Location: dashboard.php');
        exit;
    }
    $_SESSION['login_error'] = 'Invalid username or password.';
    header('Location: index.php');
    exit;
}

// Handle logout
if (isset($_GET['logout'])) {
    session_destroy();
    header('Location: index.php');
    exit;
}

// Guard — redirect to login if not authenticated
function require_auth(): void {
    if (session_status() === PHP_SESSION_NONE) session_start();
    if (empty($_SESSION[ADMIN_SESSION_KEY])) {
        header('Location: index.php');
        exit;
    }
}

// CSRF helpers
function csrf_token(): string {
    if (empty($_SESSION['csrf_token'])) {
        $_SESSION['csrf_token'] = bin2hex(random_bytes(32));
    }
    return $_SESSION['csrf_token'];
}

function csrf_field(): string {
    return '<input type="hidden" name="csrf_token" value="' . htmlspecialchars(csrf_token()) . '">';
}

function verify_csrf(): void {
    if (
        !isset($_POST['csrf_token']) ||
        !hash_equals(csrf_token(), $_POST['csrf_token'])
    ) {
        http_response_code(403);
        die('CSRF token mismatch.');
    }
}
