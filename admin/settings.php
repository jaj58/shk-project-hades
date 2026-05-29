<?php
require_once __DIR__ . '/auth.php';
require_once __DIR__ . '/_layout.php';
require_auth();

$msg = $err = '';

function load_version_file(string $path): array {
    return file_exists($path)
        ? (json_decode(file_get_contents($path), true) ?? [])
        : [];
}

function save_version_file(string $path, array $data): bool {
    return file_put_contents($path, json_encode($data, JSON_PRETTY_PRINT | JSON_UNESCAPED_SLASHES)) !== false;
}

$prod_file = __DIR__ . '/../api/version.json';
$dev_file  = __DIR__ . '/../api/dev_version.json';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    verify_csrf();

    $type         = ($_POST['version_type'] ?? '') === 'dev' ? 'dev' : 'prod';
    $version      = trim($_POST['version'] ?? '');
    $download_url = trim($_POST['download_url'] ?? '');
    $changelog    = trim($_POST['changelog'] ?? '');
    $zip_password = trim($_POST['zip_password'] ?? '');
    $target_file  = $type === 'dev' ? $dev_file : $prod_file;
    $label        = $type === 'dev' ? 'Dev' : 'Production';

    if ($type === 'prod' && !preg_match('/^\d+\.\d+\.\d+$/', $version)) {
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
        if (save_version_file($target_file, $data)) {
            $msg = $label . ' settings saved successfully.';
        } else {
            $err = 'Failed to write ' . ($type === 'dev' ? 'dev_version.json' : 'version.json') . '. Check file permissions.';
        }
    }
}

$prod = load_version_file($prod_file);
$dev  = load_version_file($dev_file);

layout_head('Settings', 'settings');
flash('success', $msg);
flash('danger', $err);
?>

<!-- Production Settings -->
<div class="row mb-4">
<div class="col-md-8">
<div class="card">
    <div class="card-header">Production Release <span class="text-muted small ms-2">(writes to api/version.json — served to standard-tier keys)</span></div>
    <div class="card-body">
        <form method="POST">
            <?= csrf_field() ?>
            <input type="hidden" name="version_type" value="prod">
            <div class="mb-3">
                <label class="form-label">Current Version <span class="text-muted small">(x.y.z)</span></label>
                <input type="text" name="version" class="form-control"
                    value="<?= htmlspecialchars($prod['version'] ?? '') ?>"
                    placeholder="1.2.0" required pattern="\d+\.\d+\.\d+">
            </div>
            <div class="mb-3">
                <label class="form-label">Download URL</label>
                <input type="url" name="download_url" class="form-control"
                    value="<?= htmlspecialchars($prod['download_url'] ?? '') ?>"
                    placeholder="https://yourdomain.com/downloads/hades_1.2.0.zip" required>
                <div class="form-text text-muted">Direct URL to the password-protected ZIP file.</div>
            </div>
            <div class="mb-3">
                <label class="form-label">ZIP Password</label>
                <div class="input-group">
                    <input type="password" name="zip_password" id="zipPassProd" class="form-control"
                        value="<?= htmlspecialchars($prod['zip_password'] ?? '') ?>" required>
                    <button type="button" class="btn btn-outline-secondary" onclick="togglePass('zipPassProd', this)">Show</button>
                </div>
            </div>
            <div class="mb-4">
                <label class="form-label">Changelog</label>
                <textarea name="changelog" class="form-control" rows="5"
                    placeholder="- Fixed bug&#10;- Added feature"><?= htmlspecialchars($prod['changelog'] ?? '') ?></textarea>
            </div>
            <button type="submit" class="btn btn-gold px-4">Save Production Settings</button>
        </form>
    </div>
</div>
</div>

<div class="col-md-4">
    <div class="card">
        <div class="card-header">Live Production Version</div>
        <div class="card-body text-center">
            <div class="display-5 fw-bold mb-2" style="color:#c8a800">
                v<?= htmlspecialchars($prod['version'] ?? '?') ?>
            </div>
            <div class="text-muted small mb-3">Served to all standard-tier keys</div>
            <hr style="border-color:#333">
            <div class="text-start small text-muted">
                <strong>Changelog preview:</strong><br>
                <pre class="mt-2" style="color:#888;font-size:.75rem;white-space:pre-wrap"><?= htmlspecialchars($prod['changelog'] ?? '—') ?></pre>
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

<!-- Dev Settings -->
<div class="row">
<div class="col-md-8">
<div class="card" style="border-color:#c8a80055">
    <div class="card-header" style="border-color:#c8a80055">Dev Release <span class="text-muted small ms-2">(writes to api/dev_version.json — served to dev-tier keys)</span></div>
    <div class="card-body">
        <form method="POST">
            <?= csrf_field() ?>
            <input type="hidden" name="version_type" value="dev">
            <div class="mb-3">
                <label class="form-label">Dev Version Label</label>
                <input type="text" name="version" class="form-control"
                    value="<?= htmlspecialchars($dev['version'] ?? '') ?>"
                    placeholder="dev-abc1234">
                <div class="form-text text-muted">Set automatically by GitHub Actions (e.g. <code>dev-abc1234</code>). Edit here only if needed.</div>
            </div>
            <div class="mb-3">
                <label class="form-label">Download URL</label>
                <input type="url" name="download_url" class="form-control"
                    value="<?= htmlspecialchars($dev['download_url'] ?? '') ?>"
                    placeholder="https://yourdomain.com/downloads/hades_dev_latest.zip" required>
            </div>
            <div class="mb-3">
                <label class="form-label">ZIP Password</label>
                <div class="input-group">
                    <input type="password" name="zip_password" id="zipPassDev" class="form-control"
                        value="<?= htmlspecialchars($dev['zip_password'] ?? '') ?>" required>
                    <button type="button" class="btn btn-outline-secondary" onclick="togglePass('zipPassDev', this)">Show</button>
                </div>
            </div>
            <div class="mb-4">
                <label class="form-label">Changelog</label>
                <textarea name="changelog" class="form-control" rows="4"
                    placeholder="- Dev build notes"><?= htmlspecialchars($dev['changelog'] ?? '') ?></textarea>
            </div>
            <button type="submit" class="btn btn-outline-secondary px-4">Save Dev Settings</button>
        </form>
    </div>
</div>
</div>

<div class="col-md-4">
    <div class="card" style="border-color:#c8a80055">
        <div class="card-header" style="border-color:#c8a80055">Live Dev Version</div>
        <div class="card-body text-center">
            <div class="fs-4 fw-bold mb-2 text-warning">
                <?= htmlspecialchars($dev['version'] ?? '—') ?>
            </div>
            <div class="text-muted small mb-3">Served to dev-tier keys only</div>
            <hr style="border-color:#333">
            <div class="text-start small text-muted">
                <strong>Changelog preview:</strong><br>
                <pre class="mt-2" style="color:#888;font-size:.75rem;white-space:pre-wrap"><?= htmlspecialchars($dev['changelog'] ?? '—') ?></pre>
            </div>
        </div>
    </div>
</div>
</div>

<script>
function togglePass(id, btn) {
    const f = document.getElementById(id);
    f.type = f.type === 'password' ? 'text' : 'password';
    btn.textContent = f.type === 'password' ? 'Show' : 'Hide';
}
</script>
<?php layout_foot(); ?>
