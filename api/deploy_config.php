<?php
/**
 * Deploy configuration.
 * Set DEPLOY_TOKEN as a Railway environment variable (not in this file).
 *
 * Generate a token (PowerShell):
 *   -join ((65..90+97..122+48..57) | Get-Random -Count 48 | % {[char]$_})
 */
define('DEPLOY_TOKEN', getenv('DEPLOY_TOKEN') ?: '');
