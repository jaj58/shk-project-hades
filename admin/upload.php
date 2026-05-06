<?php
require_once __DIR__ . '/auth.php';
require_once __DIR__ . '/_layout.php';
require_auth();

$downloads_dir = __DIR__ . '/../downloads/';
$version_file  = __DIR__ . '/../api/version.json';
$msg = $err = '';

// Max upload: 128 MB
$max_bytes = 128 * 1024 * 1024;

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    verify_csrf();

    if (!isset($_FILES['zipfile']) || $_FILES['zipfile']['error'] !== UPLOAD_ERR_OK) {
        $err = 'Upload error: ' . upload_error_msg($_FILES['zipfile']['error'] ?? -1);
    } else {
        $tmp  = $_FILES['zipfile']['tmp_name'];
        $size = $_FILES['zipfile']['size'];
        $name = basename($_FILES['zipfile']['name']);

        // Whitelist .zip extension
        if (!preg_match('/\.zip$/i', $name)) {
            $err = 'Only .zip files are allowed.';
        } elseif ($size > $max_bytes) {
            $err = 'File exceeds maximum size (128 MB).';
        } elseif (mime_content_type($tmp) !== 'application/zip') {
            $err = 'File does not appear to be a valid ZIP archive.';
        } else {
            // Sanitise filename and add timestamp to avoid collisions
            $safe_name = preg_replace('/[^a-zA-Z0-9._-]/', '_', $name);
            $dest = $downloads_dir . $safe_name;

            if (!is_dir($downloads_dir)) mkdir($downloads_dir, 0755, true);

            if (move_uploaded_file($tmp, $dest)) {
                // Optionally auto-update version.json download_url
                if (!empty($_POST['update_url']) && !empty($_POST['base_url'])) {
                    $base = rtrim(trim($_POST['base_url']), '/');
                    $url  = $base . '/downloads/' . $safe_name;

                    $vdata = file_exists($version_file)
                        ? (json_decode(file_get_contents($version_file), true) ?? [])
                        : [];
                    $vdata['download_url'] = $url;
                    file_put_contents($version_file, json_encode($vdata, JSON_PRETTY_PRINT | JSON_UNESCAPED_SLASHES));

                    $msg = "Uploaded <code>{$safe_name}</code> and updated download URL.";
                } else {
                    $msg = "Uploaded <code>{$safe_name}</code>. Remember to update the download URL in Settings.";
                }
            } else {
                $err = 'Failed to move uploaded file. Check directory permissions.';
            }
        }
    }
}

// Handle delete
if (isset($_GET['delete'])) {
    verify_csrf_get();
    $del = basename($_GET['delete']);
    $path = $downloads_dir . $del;
    if (file_exists($path) && preg_match('/\.zip$/i', $del)) {
        unlink($path);
        $msg = "Deleted $del.";
    }
}

// List uploaded files
$files = [];
if (is_dir($downloads_dir)) {
    foreach (glob($downloads_dir . '*.zip') as $f) {
        $files[] = [
            'name'    => basename($f),
            'size'    => filesize($f),
            'mtime'   => filemtime($f),
        ];
    }
    usort($files, fn($a, $b) => $b['mtime'] - $a['mtime']);
}

layout_head('Upload', 'upload');
flash('success', $msg);
flash('danger', $err);
?>

<div class="row g-3">
<div class="col-md-6">
<div class="card">
    <div class="card-header">Upload New Release ZIP</div>
    <div class="card-body">
        <form method="POST" enctype="multipart/form-data">
            <?= csrf_field() ?>
            <input type="hidden" name="MAX_FILE_SIZE" value="<?= $max_bytes ?>">
            <div class="mb-3">
                <label class="form-label">ZIP File <span class="text-muted small">(max 128 MB)</span></label>
                <input type="file" name="zipfile" class="form-control" accept=".zip" required>
            </div>
            <div class="mb-3">
                <div class="form-check">
                    <input type="checkbox" class="form-check-input" id="autoUrl" name="update_url" value="1" checked>
                    <label class="form-check-label" for="autoUrl">Auto-update download URL in Settings</label>
                </div>
            </div>
            <div class="mb-4" id="baseUrlRow">
                <label class="form-label">Your site base URL</label>
                <input type="url" name="base_url" class="form-control"
                    placeholder="https://yourdomain.com" value="<?= htmlspecialchars($_POST['base_url'] ?? '') ?>">
                <div class="form-text text-muted">Used to build the download URL automatically.</div>
            </div>
            <button type="submit" class="btn btn-gold">Upload</button>
        </form>
    </div>
</div>
</div>

<div class="col-md-6">
<div class="card">
    <div class="card-header">Uploaded Files</div>
    <div class="card-body p-0">
        <table class="table table-striped mb-0 small">
            <thead><tr><th>Filename</th><th>Size</th><th>Uploaded</th><th></th></tr></thead>
            <tbody>
            <?php foreach ($files as $f): ?>
            <tr>
                <td><code><?= htmlspecialchars($f['name']) ?></code></td>
                <td class="text-muted"><?= format_bytes($f['size']) ?></td>
                <td class="text-muted"><?= date('Y-m-d H:i', $f['mtime']) ?></td>
                <td>
                    <a href="?delete=<?= urlencode($f['name']) ?>&csrf_token=<?= urlencode(csrf_token()) ?>"
                       class="btn btn-sm btn-outline-danger"
                       onclick="return confirm('Delete <?= htmlspecialchars($f['name']) ?>?')">Delete</a>
                </td>
            </tr>
            <?php endforeach; ?>
            <?php if (!$files): ?>
                <tr><td colspan="4" class="text-center text-muted py-3">No files uploaded yet.</td></tr>
            <?php endif; ?>
            </tbody>
        </table>
    </div>
</div>
</div>
</div>

<script>
document.getElementById('autoUrl').addEventListener('change', function() {
    document.getElementById('baseUrlRow').style.display = this.checked ? '' : 'none';
});
</script>
<?php
layout_foot();

function format_bytes(int $bytes): string {
    if ($bytes >= 1048576) return round($bytes / 1048576, 1) . ' MB';
    if ($bytes >= 1024)    return round($bytes / 1024, 1) . ' KB';
    return $bytes . ' B';
}

function upload_error_msg(int $code): string {
    return [
        UPLOAD_ERR_INI_SIZE   => 'File exceeds server upload_max_filesize.',
        UPLOAD_ERR_FORM_SIZE  => 'File exceeds form MAX_FILE_SIZE.',
        UPLOAD_ERR_PARTIAL    => 'File was only partially uploaded.',
        UPLOAD_ERR_NO_FILE    => 'No file was uploaded.',
        UPLOAD_ERR_NO_TMP_DIR => 'Missing temp directory.',
        UPLOAD_ERR_CANT_WRITE => 'Failed to write file to disk.',
    ][$code] ?? "Unknown error (code $code).";
}

function verify_csrf_get(): void {
    if (empty($_GET['csrf_token']) || !hash_equals(csrf_token(), $_GET['csrf_token'])) {
        http_response_code(403); die('CSRF mismatch.');
    }
}
