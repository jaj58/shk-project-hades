-- Migration: Goods Sender covers every good (resources, food, weapons), not only banquet goods.
-- Run once via TablePlus or the Railway MySQL shell, BEFORE deploying the new banquet_api.php.
-- Safe on a live database: widens columns and adds nullable / defaulted ones. Mirrored in api/schema.sql.
--
-- Per-good JSON arrays follow BQ_GOODS in api/banquet_planner.php. Banquet goods keep
-- indices 0..7, so rows and shipments written before this migration stay valid.
-- caps_json / storage_json stay NULL for villages reported by older bots: the planner then
-- uses has_hall / hall_cap for banquet goods and blocks the other categories for them.

ALTER TABLE bq_players
    MODIFY carry_json VARCHAR(512) NOT NULL DEFAULT '[]',
    ADD COLUMN client_json VARCHAR(512) NULL AFTER carry_json;

ALTER TABLE bq_villages
    MODIFY levels_json    VARCHAR(512) NOT NULL DEFAULT '[]',
    MODIFY prod_json      VARCHAR(512) NOT NULL DEFAULT '[]',
    MODIFY buildings_json VARCHAR(512) NOT NULL DEFAULT '[]',
    ADD COLUMN caps_json      VARCHAR(512) NULL AFTER buildings_json,
    ADD COLUMN storage_json   VARCHAR(128) NULL AFTER caps_json,
    ADD COLUMN merchants_home INT NOT NULL DEFAULT 0 AFTER merchants_free;
