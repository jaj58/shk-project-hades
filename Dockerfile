FROM php:8.2-apache

RUN docker-php-ext-install pdo pdo_mysql fileinfo

# Allow 128 MB uploads for release ZIPs
RUN printf "upload_max_filesize=128M\npost_max_size=140M\n" \
    > /usr/local/etc/php/conf.d/uploads.ini

COPY admin/ /var/www/html/admin/
COPY api/   /var/www/html/api/

RUN mkdir -p /var/www/html/downloads \
    && chown -R www-data:www-data /var/www/html/

EXPOSE 80
