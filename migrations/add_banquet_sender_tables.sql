-- Migration: Banquet Good Sender tables
-- Run once via TablePlus or the Railway MySQL shell. Safe on a live database —
-- only creates new tables. Mirrored in api/schema.sql.
--
-- All *_at / eta columns ending in "_game" hold GAME SERVER time as unix seconds
-- (what the client reads from VillageMap / MarketTraderData). Columns without that
-- suffix hold API (PHP) time as unix seconds. The two clocks are never compared
-- directly — see bq_game_now() in api/banquet_planner.php.

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
    carry_json      VARCHAR(128) NOT NULL DEFAULT '[]', -- per-good merchant carry level, 8 entries
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
    levels_json        VARCHAR(128) NOT NULL DEFAULT '[]', -- 8 goods, client-extrapolated at levels_at_game
    prod_json          VARCHAR(128) NOT NULL DEFAULT '[]', -- 8 goods, production per day
    buildings_json     VARCHAR(64)  NOT NULL DEFAULT '[]', -- 8 goods, finished production buildings
    levels_at_game     INT UNSIGNED NOT NULL DEFAULT 0,
    snapshot_at_game   INT UNSIGNED NOT NULL DEFAULT 0, -- last server download of this village
    merchants_free     INT          NOT NULL DEFAULT 0, -- at home minus the client's Trade reserve
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
    good            TINYINT      NOT NULL,          -- 0..7, see BQ_GOOD_NAMES
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
