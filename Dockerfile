FROM php:8.2-fpm

RUN apt-get update && apt-get install -y nginx \
    && docker-php-ext-install pdo pdo_mysql fileinfo \
    && rm -rf /var/lib/apt/lists/*

# Allow 128 MB uploads for release ZIPs
RUN printf "upload_max_filesize=128M\npost_max_size=140M\n" \
    > /usr/local/etc/php/conf.d/uploads.ini

COPY docker/nginx.conf /etc/nginx/sites-available/default
COPY admin/ /var/www/html/admin/
COPY api/   /var/www/html/api/

RUN mkdir -p /var/www/html/downloads \
    && chown -R www-data:www-data /var/www/html/

COPY docker/start.sh /start.sh
RUN chmod +x /start.sh

EXPOSE 80
CMD ["/start.sh"]
