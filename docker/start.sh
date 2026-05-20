#!/bin/sh
# Start PHP-FPM in the background, then Nginx in the foreground
php-fpm -D
exec nginx -g 'daemon off;'
