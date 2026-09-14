# Auto Bomb Multi — API Setup Guide

## Requirements
- PHP 7.4 or later
- The `api/` directory must be writable by the web server (so `state.json` can be created)

---

## Option A: Local LAN (XAMPP)

Use this when all players are on the same network (e.g. a local game session).

1. Download and install [XAMPP](https://www.apachefriends.org/) (free).
2. Copy the `api/` folder to `C:\xampp\htdocs\shk-multi\` (or any subfolder of `htdocs`).
3. Start the XAMPP control panel and click **Start** next to **Apache**.
4. Test by opening `http://localhost/shk-multi/multi_bomb_api.php` in a browser — you should see a JSON error (expected, since it requires POST).
5. Find your local IP address (run `ipconfig` in cmd, look for IPv4 under your network adapter).
6. Other players on the same network use `http://192.168.x.x/shk-multi/multi_bomb_api.php` as the API URL.

---

## Option B: Shared Hosting (Players on Different Networks)

Use this for coordinating with players across the internet.

1. Sign up for any cheap PHP shared hosting (e.g. Hostinger, InfinityFree, 000webhost).
2. Upload the `api/` folder contents to a directory on your hosting (e.g. `public_html/shk-multi/`).
3. Make sure the directory is writable:
   - Via your hosting file manager or FTP, right-click the folder and set permissions to **755** or **775**.
   - If `state.json` fails to create, try **777** (less secure but fine for private use).
4. Your API URL will be something like `https://yourdomain.com/shk-multi/multi_bomb_api.php`.

---

## Setting a Session Key (Recommended)

To prevent unauthorised access, create a file named `session_key.txt` in the same directory as `multi_bomb_api.php`, containing a shared password:

```
mysecretpassword123
```

All players must enter this same value in the **Session Key** field of the Auto Bomb Multi tab.

If `session_key.txt` does not exist or is empty, the API accepts all requests without checking.

---

## Resetting State

If a session gets stuck, the coordinator can click **Reset Session** in the Auto Bomb Multi tab, or you can manually delete `state.json` from the server — the API will recreate it on the next request.

---

# Banquet Sender — API Setup

Shares banquet goods between players. Unlike Auto Bomb Multi it keeps its data in
MySQL (the same `hades_bot` database as the licence system), so the record of goods
on the road survives redeploys.

1. Run `migrations/add_banquet_sender_tables.sql` once against the database
   (TablePlus or the Railway MySQL shell). It only creates the `bq_*` tables.
2. Deploy as usual — `api/banquet_api.php`, `api/banquet_planner.php` and the
   website in `api/banquet/` ship with the rest of `api/`.
3. Pick a group key (6+ characters) and give it to every player. In the bot's
   **Banquet Sender** tab each player sets:
   - API URL: `https://<your-domain>/api/banquet_api.php`
   - Group key: the shared key
   - Enabled
4. Open `https://<your-domain>/api/banquet/` (or **Open Group Website** in the tab)
   and choose **Auto-fill** or **Focus**. Anyone with the key can change settings.

Tests (no production access needed):

```
php tests/banquet_planner_test.php                        # planner maths, no DB
php tests/banquet_api_integration.php http://host/api/banquet_api.php   # needs a DB with the tables
```
