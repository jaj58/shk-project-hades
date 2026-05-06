<?php
/**
 * Deploy API — receives file uploads from the post-build script.
 *
 * POST /api/deploy_api.php
 *   Header: X-Deploy-Token: <token>
 *   Body:   multipart/form-data
 *     file      — the file content
 *     path      — relative destination path (e.g. "api/updater_api.php")
 *
 * The deploy token is stored in deploy_config.php (not committed).
 * Only files within the allowed directories (api/, admin/) can be written.
 */

header('Content-Type: application/json');

require_once __DIR__ . '/deploy_config.php';

// ---------- Auth ------------------------------------------------------------

$token = $_SERVER['HTTP_X_DEPLOY_TOKEN'] ?? '';
if (!hash_equals(DEPLOY_TOKEN, $token)) {
    http_response_code(403);
    echo json_encode(['ok' => false, 'error' => 'Invalid deploy token.']);
    exit;
}

if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    http_response_code(405);
    echo json_encode(['ok' => false, 'error' => 'POST only.']);
    exit;
}

// ---------- Validate path ---------------------------------------------------

$rel_path = trim($_POST['path'] ?? '');
if (empty($rel_path)) {
    http_response_code(400);
    echo json_encode(['ok' => false, 'error' => 'Missing path.']);
    exit;
}

// Only allow writing inside api/, admin/, and downloads/
$allowed_prefixes = ['api/', 'admin/', 'downloads/'];
$allowed = false;
foreach ($allowed_prefixes as $prefix) {
    if (strpos($rel_path, $prefix) === 0) { $allowed = true; break; }
}
if (!$allowed) {
    http_response_code(403);
    echo json_encode(['ok' => false, 'error' => 'Path not in allowed directories.']);
    exit;
}

// Prevent path traversal
$web_root = realpath(__DIR__ . '/..');
$dest     = realpath_safe($web_root . '/' . $rel_path);
if ($dest === false || strpos($dest, $web_root . DIRECTORY_SEPARATOR) !== 0) {
    http_response_code(403);
    echo json_encode(['ok' => false, 'error' => 'Path traversal detected.']);
    exit;
}

// ---------- Write file ------------------------------------------------------

if (!isset($_FILES['file']) || $_FILES['file']['error'] !== UPLOAD_ERR_OK) {
    http_response_code(400);
    echo json_encode(['ok' => false, 'error' => 'No file uploaded.']);
    exit;
}

$dir = dirname($dest);
if (!is_dir($dir)) mkdir($dir, 0755, true);

if (!move_uploaded_file($_FILES['file']['tmp_name'], $dest)) {
    http_response_code(500);
    echo json_encode(['ok' => false, 'error' => 'Failed to write file.']);
    exit;
}

echo json_encode(['ok' => true, 'message' => 'Deployed: ' . $rel_path]);

// ---------- Helpers ---------------------------------------------------------

function realpath_safe(string $path): string|false {
    // Like realpath() but works even if the file doesn't exist yet
    $parts  = explode(DIRECTORY_SEPARATOR, str_replace(['/', '\\'], DIRECTORY_SEPARATOR, $path));
    $result = [];
    foreach ($parts as $part) {
        if ($part === '' || $part === '.') continue;
        if ($part === '..') { array_pop($result); continue; }
        $result[] = $part;
    }
    $resolved = (DIRECTORY_SEPARATOR === '\\' ? '' : DIRECTORY_SEPARATOR) . implode(DIRECTORY_SEPARATOR, $result);
    return $resolved ?: false;
}
