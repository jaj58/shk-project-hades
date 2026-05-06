<?php
/**
 * Hades Bot Updater API
 *
 * All endpoints accept POST (application/x-www-form-urlencoded).
 *
 * Actions:
 *   validate_license  key, hwid  → first-use binding / re-validation
 *   verify_client     key, hwid  → lightweight check on every client launch
 *   latest_version    key        → version info + download URL + zip password
 */

header('Content-Type: application/json');
header('Cache-Control: no-store, no-cache, must-revalidate');

require_once __DIR__ . '/db_config.php';

// ---------- Bootstrap -------------------------------------------------------

if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    respond_error('Only POST requests are accepted.', 405);
}

$action = isset($_POST['action']) ? trim($_POST['action']) : '';
$key    = isset($_POST['key'])    ? trim($_POST['key'])    : '';
$hwid   = isset($_POST['hwid'])   ? trim($_POST['hwid'])   : '';

if ($action === '') {
    respond_error('Missing required field: action.', 400);
}
if ($key === '') {
    respond_error('Missing required field: key.', 400);
}

// ---------- Routing ---------------------------------------------------------

switch ($action) {
    case 'validate_license':
        if ($hwid === '') respond_error('Missing required field: hwid.', 400);
        handle_validate_license($key, $hwid);
        break;

    case 'verify_client':
        if ($hwid === '') respond_error('Missing required field: hwid.', 400);
        handle_verify_client($key, $hwid);
        break;

    case 'latest_version':
        handle_latest_version($key);
        break;

    default:
        respond_error('Unknown action: ' . htmlspecialchars($action), 400);
}

// ---------- Handlers --------------------------------------------------------

function handle_validate_license($key, $hwid) {
    $pdo = get_db();
    $row = get_key_row($pdo, $key);

    if (!$row) {
        log_action_anon($pdo, $key, 'denied', $hwid);
        respond_ok(['valid' => false, 'message' => 'Invalid license key.']);
    }
    if (!$row['is_active']) {
        log_action($pdo, $row['id'], 'denied', $hwid);
        respond_ok(['valid' => false, 'message' => 'License key is disabled.']);
    }
    if (strtotime($row['valid_until']) < time()) {
        log_action($pdo, $row['id'], 'denied', $hwid);
        respond_ok(['valid' => false, 'message' => 'License key has expired.']);
    }

    if ($row['hwid'] === null) {
        // First use — bind HWID
        $stmt = $pdo->prepare('UPDATE license_keys SET hwid = ? WHERE id = ?');
        $stmt->execute([$hwid, $row['id']]);
        log_action($pdo, $row['id'], 'hwid_bind', $hwid);
        respond_ok(['valid' => true, 'message' => 'License activated and bound to this machine.']);
    }

    if ($row['hwid'] !== $hwid) {
        log_action($pdo, $row['id'], 'denied', $hwid);
        respond_ok(['valid' => false, 'message' => 'This key is registered to a different machine. Contact support to reset your HWID.']);
    }

    log_action($pdo, $row['id'], 'validate', $hwid);
    respond_ok(['valid' => true, 'message' => 'License validated.']);
}

function handle_verify_client($key, $hwid) {
    $pdo = get_db();
    $row = get_key_row($pdo, $key);

    if (!$row) {
        log_action_anon($pdo, $key, 'denied', $hwid);
        respond_ok(['allowed' => false, 'message' => 'Invalid license key.', 'version' => '']);
    }
    if (!$row['is_active']) {
        log_action($pdo, $row['id'], 'denied', $hwid);
        respond_ok(['allowed' => false, 'message' => 'License key is disabled.', 'version' => '']);
    }
    if (strtotime($row['valid_until']) < time()) {
        log_action($pdo, $row['id'], 'denied', $hwid);
        respond_ok(['allowed' => false, 'message' => 'License key has expired.', 'version' => '']);
    }
    if ($row['hwid'] !== null && $row['hwid'] !== $hwid) {
        log_action($pdo, $row['id'], 'denied', $hwid);
        respond_ok(['allowed' => false, 'message' => 'This key is registered to a different machine.', 'version' => '']);
    }

    // Unbound key — allow but don't bind here (binding is validate_license's job)
    log_action($pdo, $row['id'], 'verify', $hwid);

    $version = get_current_version();
    respond_ok(['allowed' => true, 'message' => 'OK', 'version' => $version]);
}

function handle_latest_version($key) {
    $pdo = get_db();
    $row = get_key_row($pdo, $key);

    if (!$row || !$row['is_active'] || strtotime($row['valid_until']) < time()) {
        respond_error('License key not authorized.', 403);
    }

    $version_file = __DIR__ . '/version.json';
    if (!file_exists($version_file)) {
        respond_error('Version info not found on server.', 500);
    }

    $data = json_decode(file_get_contents($version_file), true);
    if (!$data || !isset($data['version'], $data['download_url'], $data['zip_password'])) {
        respond_error('Malformed version.json on server.', 500);
    }

    respond_ok([
        'version'      => $data['version'],
        'download_url' => $data['download_url'],
        'changelog'    => isset($data['changelog']) ? $data['changelog'] : '',
        'zip_password' => $data['zip_password'],
    ]);
}

// ---------- Helpers ---------------------------------------------------------

function get_db() {
    static $pdo = null;
    if ($pdo === null) {
        $dsn = 'mysql:host=' . DB_HOST . ';dbname=' . DB_NAME . ';charset=' . DB_CHARSET;
        try {
            $pdo = new PDO($dsn, DB_USER, DB_PASS, [
                PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
                PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
                PDO::ATTR_EMULATE_PREPARES   => false,
            ]);
        } catch (PDOException $e) {
            respond_error('Database connection failed.', 500);
        }
    }
    return $pdo;
}

function get_key_row($pdo, $key_string) {
    $stmt = $pdo->prepare(
        'SELECT k.id, k.hwid, k.is_active, k.valid_until
         FROM license_keys k
         WHERE k.key_string = ?
         LIMIT 1'
    );
    $stmt->execute([$key_string]);
    return $stmt->fetch() ?: null;
}

function get_current_version() {
    $f = __DIR__ . '/version.json';
    if (!file_exists($f)) return '';
    $d = json_decode(file_get_contents($f), true);
    return isset($d['version']) ? $d['version'] : '';
}

function log_action($pdo, $key_id, $action, $hwid) {
    $ip = $_SERVER['REMOTE_ADDR'] ?? '';
    $stmt = $pdo->prepare(
        'INSERT INTO license_log (key_id, action, hwid, ip) VALUES (?, ?, ?, ?)'
    );
    $stmt->execute([$key_id, $action, $hwid ?: null, $ip ?: null]);
}

function log_action_anon($pdo, $key_string, $action, $hwid) {
    // Key doesn't exist in DB — log with key_id = 0 not possible due to FK.
    // Skip logging for unknown keys to avoid FK violation.
    // Optionally log to a separate table or file here.
}

function respond_ok($data) {
    echo json_encode(array_merge(['ok' => true], $data));
    exit;
}

function respond_error($message, $code = 400) {
    http_response_code($code);
    echo json_encode(['ok' => false, 'error' => $message]);
    exit;
}
