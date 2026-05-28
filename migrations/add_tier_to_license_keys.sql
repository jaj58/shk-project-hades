-- Migration: Add tier column to license_keys
-- Run once via TablePlus or the Railway MySQL shell.
-- Safe to run on a live database — adds a column with a default value.

ALTER TABLE license_keys
  ADD COLUMN tier ENUM('standard', 'dev') NOT NULL DEFAULT 'standard'
  AFTER is_active;
