<?php
// Shared layout helper — included by each admin page.
// Call layout_head($title) then layout_foot() around page content.

function layout_head(string $title, string $active = ''): void {
    $pages = [
        'dashboard' => ['Dashboard',  'dashboard.php', '📊'],
        'users'     => ['Users',      'users.php',     '👥'],
        'licenses'  => ['Licenses',   'licenses.php',  '🔑'],
        'logs'      => ['Logs',       'logs.php',      '📋'],
        'settings'  => ['Settings',   'settings.php',  '⚙'],
        'upload'    => ['Upload',     'upload.php',    '📦'],
    ];
    ?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Hades Admin — <?= htmlspecialchars($title) ?></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        body { background: #0f0f0f; color: #e8e8e8; }
        .sidebar { background: #111; border-right: 1px solid #2a2a2a; min-height: 100vh; width: 210px; flex-shrink: 0; }
        .sidebar .brand { color: #c8a800; font-weight: 700; letter-spacing: 1px; border-bottom: 1px solid #2a2a2a; }
        .sidebar .nav-link { color: #bbb; padding: .55rem 1rem; border-radius: 6px; margin: 2px 8px; }
        .sidebar .nav-link:hover, .sidebar .nav-link.active { background: #1e1e1e; color: #fff; }
        .sidebar .nav-link.active { border-left: 3px solid #c8a800; color: #c8a800; }
        .main-content { flex: 1; padding: 2rem; }
        .card { background: #1a1a1a; border: 1px solid #2a2a2a; color: #e8e8e8; }
        .card-header { background: #161616; border-bottom: 1px solid #2a2a2a; color: #fff; font-weight: 600; }
        .table { --bs-table-bg: transparent; --bs-table-striped-bg: #1e1e1e; color: #e8e8e8; }
        .table thead th { border-color: #333; color: #c8a800; font-size: .8rem; text-transform: uppercase; letter-spacing: .5px; }
        .table td, .table th { border-color: #2a2a2a; }
        .text-muted { color: #aaa !important; }
        .badge-active   { background: #0d6832; }
        .badge-expired  { background: #7a1a1a; }
        .badge-disabled { background: #555; color: #ddd; }
        .form-control, .form-select { background: #111; border-color: #333; color: #e8e8e8; }
        .form-control:focus, .form-select:focus { background: #111; border-color: #c8a800; color: #fff; box-shadow: none; }
        .form-control::placeholder { color: #666; }
        .form-text { color: #999 !important; }
        .form-label { color: #ddd; }
        .btn-gold { background: #c8a800; border: none; color: #000; font-weight: 600; }
        .btn-gold:hover { background: #e0bd00; color: #000; }
        .stat-card .number { font-size: 2rem; font-weight: 700; color: #c8a800; }
        .stat-card .text-muted { color: #bbb !important; }
        code { color: #c8a800; }
        pre { color: #ccc; }
        small, .small { color: #bbb; }
    </style>
</head>
<body>
<div class="d-flex">
    <nav class="sidebar d-flex flex-column py-3">
        <div class="brand px-3 pb-3 mb-2 fs-5">⚔ HADES ADMIN</div>
        <ul class="nav flex-column">
            <?php foreach ($pages as $key => [$label, $href, $icon]): ?>
            <li class="nav-item">
                <a class="nav-link <?= $active === $key ? 'active' : '' ?>" href="<?= $href ?>">
                    <?= $icon ?> <?= $label ?>
                </a>
            </li>
            <?php endforeach; ?>
        </ul>
        <div class="mt-auto px-3 pt-3 border-top" style="border-color:#2a2a2a!important">
            <a href="auth.php?logout=1" class="btn btn-sm btn-outline-secondary w-100">Sign out</a>
        </div>
    </nav>
    <div class="main-content">
        <h4 class="mb-4" style="color:#c8a800"><?= htmlspecialchars($title) ?></h4>
    <?php
}

function layout_foot(): void {
    ?>
    </div><!-- main-content -->
</div><!-- d-flex -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
    <?php
}

function flash(string $type, string $msg): void {
    if ($msg) {
        echo '<div class="alert alert-' . htmlspecialchars($type) . ' alert-dismissible fade show" role="alert">'
            . htmlspecialchars($msg)
            . '<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>';
    }
}
