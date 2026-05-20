<?php
define('DB_HOST',    getenv('DB_HOST')    ?: 'localhost');
define('DB_NAME',    getenv('DB_NAME')    ?: 'hades_bot');
define('DB_USER',    getenv('DB_USER')    ?: '');
define('DB_PASS',    getenv('DB_PASS')    ?: '');
define('DB_CHARSET', getenv('DB_CHARSET') ?: 'utf8mb4');
