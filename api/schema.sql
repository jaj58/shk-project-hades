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

-- ── Goods Sender, formerly Banquet Good Sender (see migrations/add_banquet_sender_tables.sql
--    and migrations/expand_sender_all_goods.sql) ──
CREATE TABLE IF NOT EXISTS bq_groups (
    id              INT AUTO_INCREMENT PRIMARY KEY,
    key_hash        CHAR(64)     NOT NULL UNIQUE,   -- sha256 of the shared group key
    settings_json   TEXT         NOT NULL,
    game_offset_sec INT          NOT NULL DEFAULT 0, -- game clock minus API clock, from the latest sync
    created_at      INT UNSIGNED NOT NULL,
    updated_at      INT UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS bq_players (
    group_id        INT          NOT NULL,
    user_id         INT          NOT NULL,          -- game user ID
    name            VARCHAR(64)  NOT NULL DEFAULT '',
    world           VARCHAR(64)  NOT NULL DEFAULT '',
    craftsmanship   TINYINT      NOT NULL DEFAULT 0, -- number of banquet goods unlocked (0..8)
    sec_per_tile    DOUBLE       NOT NULL DEFAULT 0, -- merchant travel seconds per map tile (research + cards)
    carry_json      VARCHAR(512) NOT NULL DEFAULT '[]', -- per-good merchant carry level, BQ_GOODS order
    client_json     VARCHAR(512) NULL,              -- bot-side per category: {enabled, reserve}
    cards_json      TEXT         NULL,              -- cards in play / owned, informational
    client_version  VARCHAR(32)  NOT NULL DEFAULT '',
    last_seen       INT UNSIGNED NOT NULL,
    PRIMARY KEY (group_id, user_id),
    FOREIGN KEY (group_id) REFERENCES bq_groups(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS bq_villages (
    group_id           INT          NOT NULL,
    village_id         INT          NOT NULL,
    user_id            INT          NOT NULL,
    name               VARCHAR(64)  NOT NULL DEFAULT '',
    x                  INT          NOT NULL DEFAULT 0,
    y                  INT          NOT NULL DEFAULT 0,
    has_hall           TINYINT(1)   NOT NULL DEFAULT 0,
    hall_cap           INT          NOT NULL DEFAULT 0, -- real cap incl. research + active card
    levels_json        VARCHAR(512) NOT NULL DEFAULT '[]', -- BQ_GOODS order, client-extrapolated at levels_at_game
    prod_json          VARCHAR(512) NOT NULL DEFAULT '[]', -- BQ_GOODS order, production per day
    buildings_json     VARCHAR(512) NOT NULL DEFAULT '[]', -- BQ_GOODS order, finished production buildings
    caps_json          VARCHAR(512) NULL,           -- BQ_GOODS order, storage cap incl. research + cards (NULL = old bot)
    storage_json       VARCHAR(128) NULL,           -- {hall,stockpile,granary,inn,armoury}: building present (NULL = old bot)
    levels_at_game     INT UNSIGNED NOT NULL DEFAULT 0,
    snapshot_at_game   INT UNSIGNED NOT NULL DEFAULT 0, -- last server download of this village
    merchants_free     INT          NOT NULL DEFAULT 0, -- at home minus the client's (banquet) reserve
    merchants_home     INT          NOT NULL DEFAULT 0, -- at home; per-category reserves come from client_json
    reported_at        INT UNSIGNED NOT NULL,
    PRIMARY KEY (group_id, village_id),
    INDEX idx_bq_villages_user (group_id, user_id),
    FOREIGN KEY (group_id) REFERENCES bq_groups(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- One row per planned or observed shipment. Lifecycle:
--   leased    -> order handed to the sender, reserved against the target until lease_until
--   in_flight -> confirmed sent (or adopted from the sender's trader list); counts at the target
--   settled   -> the target reported a village download taken >= eta + settle buffer
--   failed / cancelled / expired -> never counted again
CREATE TABLE IF NOT EXISTS bq_shipments (
    id              BIGINT AUTO_INCREMENT PRIMARY KEY,
    group_id        INT          NOT NULL,
    status          VARCHAR(16)  NOT NULL,
    source          VARCHAR(16)  NOT NULL DEFAULT 'planner', -- planner | adopted
    from_user_id    INT          NOT NULL,
    from_village_id INT          NOT NULL,
    to_user_id      INT          NOT NULL,
    to_village_id   INT          NOT NULL,
    good            TINYINT      NOT NULL,          -- index into BQ_GOODS (0..7 banquet)
    amount          INT          NOT NULL,
    merchants       INT          NOT NULL DEFAULT 0,
    trader_id       BIGINT       NULL,
    eta_game        INT UNSIGNED NULL,
    lease_until     INT UNSIGNED NULL,
    error           VARCHAR(255) NULL,
    created_at      INT UNSIGNED NOT NULL,
    updated_at      INT UNSIGNED NOT NULL,
    UNIQUE KEY uq_bq_trader (group_id, trader_id),
    INDEX idx_bq_ship_status (group_id, status),
    INDEX idx_bq_ship_target (group_id, to_village_id),
    FOREIGN KEY (group_id) REFERENCES bq_groups(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
