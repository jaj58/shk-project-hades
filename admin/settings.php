<?php
require_once __DIR__ . '/auth.php';
require_once __DIR__ . '/_layout.php';
require_auth();

$version_file = __DIR__ . '/../api/version.json';
$msg = $err = '';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    verify_csrf();

    $version      = trim($_POST['version'] ?? '');
    $download_url = trim($_POST['download_url'] ?? '');
    $changelog    = trim($_POST['changelog'] ?? '');
    $zip_password = trim($_POST['zip_password'] ?? '');

    // Validate version format x.y.z
    if (!preg_match('/^\d+\.\d+\.\d+$/', $version)) {
        $err = 'Version must be in x.y.z format (e.g. 1.2.0).';
    } elseif (empty($download_url)) {
        $err = 'Download URL cannot be empty.';
    } elseif (empty($zip_password)) {
        $err = 'ZIP password cannot be empty.';
    } else {
        $data = [
            'version'      => $version,
            'download_url' => $download_url,
            'changelog'    => $changelog,
            'zip_password' => $zip_password,
        ];
        if (file_put_contents($version_file, json_encode($data, JSON_PRETTY_PRINT | JSON_UNESCAPED_SLASHES)) !== false) {
            $msg = 'Settings saved successfully.';
        } else {
            $err = 'Failed to write version.json. Check file permissions.';
        }
    }
}

$data = file_exists($version_file)
    ? (json_decode(file_get_contents($version_file), true) ?? [])
    : [];

layout_head('Settings', 'settings');
flash('success', $msg);
flash('danger', $err);
?>

<div class="row">
<div class="col-md-8">
<div class="card">
    <div class="card-header">Release Settings <span class="text-muted small ms-2">(writes to api/version.json)</span></div>
    <div class="card-body">
        <form method="POST">
            <?= csrf_field() ?>
            <div class="mb-3">
                <label class="form-label">Current Version <span class="text-muted small">(x.y.z)</span></label>
                <input type="text" name="version" class="form-control"
                    value="<?= htmlspecialchars($data['version'] ?? '') ?>"
                    placeholder="1.2.0" required pattern="\d+\.\d+\.\d+">
            </div>
            <div class="mb-3">
                <label class="form-label">Download URL</label>
                <input type="url" name="download_url" class="form-control"
                    value="<?= htmlspecialchars($data['download_url'] ?? '') ?>"
                    placeholder="https://yourdomain.com/downloads/hades_1.2.0.zip" required>
                <div class="form-text text-muted">Direct URL to the password-protected ZIP file.</div>
            </div>
            <div class="mb-3">
                <label class="form-label">ZIP Password</label>
                <div class="input-group">
                    <input type="password" name="zip_password" id="zipPass" class="form-control"
                        value="<?= htmlspecialchars($data['zip_password'] ?? '') ?>" required>
                    <button type="button" class="btn btn-outline-secondary" onclick="togglePass()">Show</button>
                </div>
                <div class="form-text text-muted">Password used to encrypt the ZIP. Rotate this with each new release.</div>
            </div>
            <div class="mb-4">
                <label class="form-label">Changelog</label>
                <textarea name="changelog" class="form-control" rows="6"
                    placeholder="- Fixed bug&#10;- Added feature"><?= htmlspecialchars($data['changelog'] ?? '') ?></textarea>
            </div>
            <button type="submit" class="btn btn-gold px-4">Save Settings</button>
        </form>
    </div>
</div>
</div>

<div class="col-md-4">
    <div class="card">
        <div class="card-header">Current Live Version</div>
        <div class="card-body text-center">
            <div class="display-5 fw-bold mb-2" style="color:#c8a800">
                v<?= htmlspecialchars($data['version'] ?? '?') ?>
            </div>
            <div class="text-muted small mb-3">Served to all authenticated clients</div>
            <hr style="border-color:#333">
            <div class="text-start small text-muted">
                <strong>Changelog preview:</strong><br>
                <pre class="mt-2" style="color:#888;font-size:.75rem;white-space:pre-wrap"><?= htmlspecialchars($data['changelog'] ?? '—') ?></pre>
            </div>
        </div>
    </div>
    <div class="card mt-3">
        <div class="card-body text-center">
            <a href="upload.php" class="btn btn-outline-secondary w-100">📦 Upload New ZIP</a>
        </div>
    </div>
</div>
</div>

<script>
function togglePass() {
    const f = document.getElementById('zipPass');
    f.type = f.type === 'password' ? 'text' : 'password';
}
</script>
<?php layout_foot(); ?>
