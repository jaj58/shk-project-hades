# Project Hades — Deployment Setup Guide

Complete instructions for deploying the admin panel, API, and CI/CD pipeline from scratch.

**Stack:** PHP 8.2 + MySQL on Railway, GitHub Actions for automated client builds.

---

## Prerequisites

Before starting, make sure you have:
- Access to the private GitHub repo
- [TablePlus](https://tableplus.com) installed (free tier is enough) — for DB import
- [7-Zip](https://www.7-zip.org) installed at `C:\Program Files\7-Zip\7z.exe`
- Visual Studio with MSBuild installed (for local emergency releases)
- A domain you control (if pointing a custom domain)
- Your existing database dump (see [Database Export](#1-export-your-local-database) below)

---

## 1. One-Time Repo Setup

### Commit `4gb_patch.exe`
The GitHub Actions build pipeline needs this tool. It only needs to be done once:
```powershell
git add 4gb_patch.exe
git commit -m "Add 4gb_patch.exe for CI build pipeline"
git push
```

---

## 2. Railway Setup

### Create Account & Project
1. Go to [railway.com](https://railway.com) and sign up
2. Upgrade to the **Hobby plan** ($5/mo) — required for always-on services
3. Click **New Project → Deploy from GitHub repo**
4. Authorise Railway to access your GitHub account when prompted
5. Select `shk-project-hades`
6. Railway detects the `Dockerfile` and starts the first build automatically

### Add MySQL Database
1. Inside the project, click **+ New → Database → MySQL**
2. Railway provisions a managed MySQL instance
3. Click the MySQL service → **Variables** tab
4. Note down the values for: `MYSQLHOST`, `MYSQLPORT`, `MYSQLUSER`, `MYSQLPASSWORD`

### Generate a Deploy Token
Run this in PowerShell to generate a strong token — you'll need one for prod and one for dev:
```powershell
-join ((65..90+97..122+48..57) | Get-Random -Count 48 | % {[char]$_})
```
Run it twice and save both outputs somewhere safe.

### Set Environment Variables (Production)
Click your PHP service → **Variables** tab → add each of the following:

| Variable | Value |
|---|---|
| `DB_HOST` | Value of `MYSQLHOST` from the MySQL service |
| `DB_NAME` | `hades_bot` |
| `DB_USER` | Value of `MYSQLUSER` |
| `DB_PASS` | Value of `MYSQLPASSWORD` |
| `DB_CHARSET` | `utf8mb4` |
| `DEPLOY_TOKEN` | First generated token (prod) |
| `ZIP_PASSWORD` | Your release ZIP password |
| `ADMIN_USER` | `admin` |
| `ADMIN_PASS_HASH` | Your bcrypt hash — generate with: `php -r "echo password_hash('yourpassword', PASSWORD_DEFAULT);"` |
| `ADMIN_SESSION_KEY` | `hades_admin_authed` |

### Add Persistent Volume (for release ZIPs)
1. Click PHP service → **Volumes** tab → **Add Volume**
2. Set mount path: `/var/www/html/downloads`
3. Set size: **5 GB** (expandable later if needed)

### Set Up Dev Environment
1. In the Railway project, click the environment dropdown (top-left) → **New Environment** → name it `Development`
2. Inside the dev environment: **+ New → GitHub Repo** → select the same repo
3. In the new service settings → **Source → Branch** → set to `dev`
4. Set the same environment variables as production with these differences:
   - `DB_*` values — use the **same MySQL service** as production (shared database)
   - `DEPLOY_TOKEN` — use the **second generated token** (keep dev and prod tokens separate)
5. Add a persistent volume at the same mount path (`/var/www/html/downloads`), **1–2 GB** is enough for dev

### Point Your Custom Domain (Production Only)
1. Production PHP service → **Settings → Domains → Add Custom Domain**
2. Enter your domain (e.g. `projecthades.co.uk`)
3. Railway gives you a **CNAME record** — add it in your domain registrar's DNS settings
4. SSL is provisioned automatically via Let's Encrypt
5. DNS propagation takes anywhere from 15 minutes to 24 hours

---

## 3. Database Migration

### Export Your Local Database
Run this on your local machine (adjust username if needed):
```bash
mysqldump -u admin -p hades_bot > hades_bot_backup.sql
```
Enter your local MySQL password when prompted. This creates `hades_bot_backup.sql` with all tables and data.

### Import into Railway MySQL

**Option A — TablePlus (recommended)**
1. Open TablePlus → **New Connection → MySQL**
2. Railway → MySQL service → **Connect** tab → copy the connection details and paste into TablePlus
3. Connect, then go to **File → Import → From SQL Dump**
4. Select `hades_bot_backup.sql` and run

**Option B — Command line**
```bash
mysql -h <MYSQLHOST> -P <MYSQLPORT> -u <MYSQLUSER> -p<MYSQLPASSWORD> hades_bot < hades_bot_backup.sql
```

**Option C — Railway CLI**
```bash
npm install -g @railway/cli
railway login
railway link
railway connect mysql
```
Then inside the MySQL shell:
```sql
source hades_bot_backup.sql
```

### Verify the Import
```sql
SHOW TABLES;
SELECT COUNT(*) FROM license_keys;
SELECT COUNT(*) FROM users;
SELECT COUNT(*) FROM license_log;
```

---

## 4. GitHub Secrets Setup

In your GitHub repo → **Settings → Secrets and variables → Actions → New repository secret** — add all seven:

| Secret name | Value |
|---|---|
| `HADES_DEPLOY_URL_PROD` | `https://your-prod-domain/api/deploy_api.php` |
| `HADES_DEPLOY_URL_DEV` | `https://your-dev-railway-url.railway.app/api/deploy_api.php` |
| `HADES_DEPLOY_TOKEN_PROD` | Prod deploy token (same as Railway `DEPLOY_TOKEN` env var) |
| `HADES_DEPLOY_TOKEN_DEV` | Dev deploy token (same as Railway dev `DEPLOY_TOKEN` env var) |
| `HADES_DOWNLOAD_BASE_PROD` | `https://your-prod-domain` (no trailing slash) |
| `HADES_DOWNLOAD_BASE_DEV` | `https://your-dev-railway-url.railway.app` (no trailing slash) |
| `HADES_ZIP_PASSWORD` | Your release ZIP password |

The Railway service URLs are found in Railway → service → **Settings → Domains**.

---

## 5. Local Machine Setup

Add these to your PowerShell profile (`~\Documents\PowerShell\Microsoft.PowerShell_profile.ps1`) so `deploy.ps1` works for emergency manual releases:

```powershell
$env:HADES_DEPLOY_URL        = "https://your-prod-domain/api/deploy_api.php"
$env:HADES_DEPLOY_TOKEN      = "<prod deploy token>"
$env:HADES_ZIP_PASSWORD      = "<zip password>"
$env:HADES_DOWNLOAD_BASE_URL = "https://your-prod-domain"
```

Restart your PowerShell session after saving.

---

## 6. How Deployments Work Day-to-Day

### PHP / Admin Panel Changes
Just push to the relevant branch — Railway handles the rest:
```
git push           # → Railway redeploys automatically
```
- Push to `dev` → dev environment updates
- Push to `main` → production updates

### New Client Release (C# exe)
Push any changes inside `StrongholdKingdoms/` to trigger the full automated pipeline:
```
git push
```
GitHub Actions will automatically:
1. Increment the patch version in `AssemblyInfo.cs`
2. Build the release exe with MSBuild
3. Apply the 4GB memory patch
4. Create a password-protected ZIP
5. Upload the ZIP to the correct Railway environment's persistent volume
6. Update `api/version.json` with the new version and download URL
7. Commit `AssemblyInfo.cs` + `version.json` back to git (`[skip ci]`)
8. That commit triggers Railway to redeploy with the updated `version.json`

**Branch → Environment mapping:**
- `dev` branch → dev Railway environment
- `main` branch → production Railway environment

### Emergency Manual Release
If you need to release manually (e.g. CI is down):
```powershell
.\deploy.ps1 -Release
.\deploy.ps1 -Release -Changelog "Fixed X, improved Y"
```
Requires the env vars from step 5 to be set in your PowerShell profile.

---

## 7. Verification Checklist

Run through these after a fresh deployment to confirm everything is working:

- [ ] Railway dashboard shows a green successful build
- [ ] Admin panel loads at your Railway/custom domain URL
- [ ] Admin panel login works with your credentials
- [ ] TablePlus shows correct row counts in Railway MySQL (`license_keys`, `users`, `license_log`)
- [ ] Upload a test ZIP via the admin panel → appears in the file list
- [ ] Trigger a manual Railway redeploy → ZIP still listed (confirms volume is persisting)
- [ ] `GET /api/updater_api.php` with a valid license key returns expected JSON
- [ ] Push a test C# change to `dev` → GitHub Actions runs and succeeds → ZIP appears in dev downloads
- [ ] Merge to `main` → same pipeline runs against production
- [ ] `.\deploy.ps1 -Release` completes successfully using PowerShell profile env vars

---

## Troubleshooting

**Admin panel login fails after fresh deploy**
Your `ADMIN_PASS_HASH` Railway env var may be missing or incorrect. Generate a fresh hash:
```bash
php -r "echo password_hash('yourpassword', PASSWORD_DEFAULT);"
```
Update the env var in Railway and redeploy.

**ZIP upload fails in GitHub Actions**
- Check that `HADES_DEPLOY_TOKEN_PROD` / `HADES_DEPLOY_TOKEN_DEV` in GitHub Secrets matches the `DEPLOY_TOKEN` env var set in the corresponding Railway environment
- Check the Actions log for the HTTP response from `deploy_api.php`

**Downloads not persisting after redeploy**
Confirm the Railway Volume is mounted at `/var/www/html/downloads` — check the **Volumes** tab on the PHP service.

**Database connection error on startup**
Confirm all four `DB_*` env vars are set on the PHP service in Railway and match the MySQL service's connection details exactly.

**`4gb_patch.exe` not found in GitHub Actions**
Ensure it was committed to the repo root (`git add 4gb_patch.exe`) and is not excluded by `.gitignore`.
