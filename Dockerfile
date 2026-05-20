FROM php:8.2-apache

# Fix MPM conflict: disable event and worker, enable prefork (required for mod_php)
# Using semicolons so each command runs regardless of the previous result
RUN a2dismod mpm_event mpm_worker 2>/dev/null; a2enmod mpm_prefork

RUN docker-php-ext-install pdo pdo_mysql fileinfo

# Allow 128 MB uploads for release ZIPs
RUN printf "upload_max_filesize=128M\npost_max_size=140M\n" \
    > /usr/local/etc/php/conf.d/uploads.ini

COPY admin/ /var/www/html/admin/
COPY api/   /var/www/html/api/

RUN mkdir -p /var/www/html/downloads \
    && chown -R www-data:www-data /var/www/html/

EXPOSE 80
