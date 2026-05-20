#!/bin/sh
# Fix permissions on the mounted volume at startup — Railway mounts volumes
# as root, overriding any chown done at build time.
chown -R www-data:www-data /var/www/html/downloads
chmod 755 /var/www/html/downloads

# Start PHP-FPM in the background, then Nginx in the foreground
php-fpm -D
exec nginx -g 'daemon off;'
