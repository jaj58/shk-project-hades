<?php
// Set these as Railway environment variables — not in this file.
// Generate a hash: php -r "echo password_hash('your_password', PASSWORD_DEFAULT);"
define('ADMIN_USER',        getenv('ADMIN_USER')        ?: 'admin');
define('ADMIN_PASS_HASH',   getenv('ADMIN_PASS_HASH')   ?: '');
define('ADMIN_SESSION_KEY', getenv('ADMIN_SESSION_KEY') ?: 'hades_admin_authed');
