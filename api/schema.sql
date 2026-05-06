-- Hades Bot License System Schema
-- Run once on the web host: mysql -u root -p hades_bot < schema.sql
-- Or create the database first: CREATE DATABASE hades_bot; USE hades_bot;

CREATE TABLE IF NOT EXISTS users (
    id         INT AUTO_INCREMENT PRIMARY KEY,
    username   VARCHAR(64)  NOT NULL UNIQUE,
    email      VARCHAR(255) NOT NULL UNIQUE,
    created_at DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS license_keys (
    id          INT AUTO_INCREMENT PRIMARY KEY,
    key_string  VARCHAR(64)  NOT NULL UNIQUE,
    user_id     INT          NOT NULL,
    valid_from  DATETIME     NOT NULL,
    valid_until DATETIME     NOT NULL,
    hwid        VARCHAR(64)  DEFAULT NULL,
    is_active   TINYINT(1)   NOT NULL DEFAULT 1,
    created_at  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS license_log (
    id         INT AUTO_INCREMENT PRIMARY KEY,
    key_id     INT         NOT NULL,
    action     VARCHAR(32) NOT NULL,
    hwid       VARCHAR(64) DEFAULT NULL,
    ip         VARCHAR(45) DEFAULT NULL,
    created_at DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (key_id) REFERENCES license_keys(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Index for faster log queries
CREATE INDEX idx_log_key_id    ON license_log(key_id);
CREATE INDEX idx_log_created   ON license_log(created_at);
CREATE INDEX idx_key_user      ON license_keys(user_id);
CREATE INDEX idx_key_active    ON license_keys(is_active);
