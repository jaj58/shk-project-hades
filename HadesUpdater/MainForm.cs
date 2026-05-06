using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;

namespace HadesUpdater
{
    public partial class MainForm : Form
    {
        private UpdaterSettings _settings;
        private UpdaterClient   _client;
        private BackgroundWorker _worker;

        public MainForm()
        {
            InitializeComponent();
            _settings = UpdaterSettings.Load();
            _client   = new UpdaterClient();
        }

        // ------------------------------------------------------------------ //
        //  Form load                                                           //
        // ------------------------------------------------------------------ //

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_settings.IsConfigured)
            {
                // Pre-fill from saved settings — make fields read-only
                txtLicenseKey.Text     = _settings.LicenseKey;
                txtLicenseKey.ReadOnly = true;
                txtInstallDir.Text     = _settings.InstallDir;
                txtInstallDir.ReadOnly = true;
                btnBrowse.Enabled      = false;
                btnAction.Text         = "Check for Update";
                SetStatus("Ready — click 'Check for Update' to continue.");
            }
            else
            {
                btnAction.Text = "Install";
                SetStatus("Enter your license key and install directory to get started.");
            }
        }

        // ------------------------------------------------------------------ //
        //  UI events                                                           //
        // ------------------------------------------------------------------ //

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select the folder where Hades Bot should be installed.";
                dlg.ShowNewFolderButton = true;
                if (!string.IsNullOrWhiteSpace(txtInstallDir.Text))
                    dlg.SelectedPath = txtInstallDir.Text;

                if (dlg.ShowDialog() == DialogResult.OK)
                    txtInstallDir.Text = dlg.SelectedPath;
            }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            if (_worker != null && _worker.IsBusy) return;

            string key       = txtLicenseKey.Text.Trim();
            string installDir = txtInstallDir.Text.Trim();

            if (string.IsNullOrWhiteSpace(key))
            {
                MessageBox.Show("Please enter your license key.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!_settings.IsConfigured && string.IsNullOrWhiteSpace(installDir))
            {
                MessageBox.Show("Please select an install directory.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetUIBusy(true);
            progressBar.Value = 0;

            _worker = new BackgroundWorker { WorkerReportsProgress = true };
            _worker.DoWork             += Worker_DoWork;
            _worker.ProgressChanged    += Worker_ProgressChanged;
            _worker.RunWorkerCompleted += Worker_Completed;
            _worker.RunWorkerAsync(new WorkerArgs
            {
                Key        = key,
                InstallDir = string.IsNullOrWhiteSpace(installDir) ? _settings.InstallDir : installDir,
            });
        }

        private void lnkChange_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Unlock fields so the user can enter a new key / directory
            txtLicenseKey.ReadOnly = false;
            txtInstallDir.ReadOnly = false;
            btnBrowse.Enabled      = true;
            btnAction.Text         = "Install";
            txtLicenseKey.Focus();
        }

        // ------------------------------------------------------------------ //
        //  BackgroundWorker                                                    //
        // ------------------------------------------------------------------ //

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var args   = (WorkerArgs)e.Argument;
            var worker = (BackgroundWorker)sender;

            // 1. Generate HWID
            worker.ReportProgress(0, "Generating hardware ID…");
            string hwid = HwidHelper.GetHwid();

            // 2. Validate license key
            worker.ReportProgress(5, "Validating license key…");
            string validationMsg;
            if (!_client.ValidateLicense(args.Key, hwid, out validationMsg))
                throw new Exception(validationMsg ?? "License key validation failed.");

            // 3. Fetch latest version info (includes download URL + ZIP password)
            worker.ReportProgress(12, "Fetching version information…");
            VersionInfo version = _client.GetLatestVersion(args.Key);
            if (version == null)
                throw new Exception("Could not retrieve version information from server.");

            // 4. Check if already up to date (use our own tracked version, not the exe's file version)
            string installedVersion = _settings.InstalledVersion;
            if (!string.IsNullOrEmpty(installedVersion) &&
                !Program.IsNewerVersion(version.Version, installedVersion))
            {
                e.Result = new WorkerResult { AlreadyUpToDate = true, Version = version.Version };
                return;
            }

            // 5. Download to temp path
            string zipPath = Path.Combine(Path.GetTempPath(), "hades_update.zip");
            worker.ReportProgress(15, "Downloading v" + version.Version + "…");

            _client.DownloadFile(version.DownloadUrl, zipPath, pct =>
                worker.ReportProgress(15 + (int)(pct * 0.72), "Downloading… " + pct + "%"));

            // 6. Extract (password used in-memory only, never stored)
            worker.ReportProgress(88, "Extracting files…");
            Directory.CreateDirectory(args.InstallDir);
            ExtractZip(zipPath, args.InstallDir, version.ZipPassword);
            version.ZipPassword = null; // clear from memory ASAP

            // 7. Save settings + cleanup
            worker.ReportProgress(97, "Saving settings…");
            _settings.LicenseKey       = args.Key;
            _settings.InstallDir       = args.InstallDir;
            _settings.InstalledVersion = version.Version;
            _settings.Save();

            try { File.Delete(zipPath); } catch { }

            e.Result = new WorkerResult
            {
                AlreadyUpToDate = false,
                Version         = version.Version,
                Changelog       = version.Changelog,
            };
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = Math.Min(e.ProgressPercentage, 100);
            if (e.UserState is string msg)
                SetStatus(msg);
        }

        private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            SetUIBusy(false);

            if (e.Error != null)
            {
                progressBar.Value = 0;
                SetStatus("Error: " + e.Error.Message);
                MessageBox.Show(e.Error.Message, "Update Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = (WorkerResult)e.Result;
            progressBar.Value = 100;

            if (result.AlreadyUpToDate)
            {
                SetStatus("Already up to date (v" + result.Version + ").");
                if (!string.IsNullOrEmpty(Program.RelaunchPath))
                    RelaunchClient();
                else
                    MessageBox.Show("You already have the latest version (v" + result.Version + ").",
                        "Up to Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SetStatus("Update complete — v" + result.Version + " installed.");

            string msg = "Hades Bot v" + result.Version + " installed successfully!";
            if (!string.IsNullOrWhiteSpace(result.Changelog))
                msg += "\n\nChangelog:\n" + result.Changelog;

            MessageBox.Show(msg, "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (!string.IsNullOrEmpty(Program.RelaunchPath))
                RelaunchClient();
            else
                Application.Exit();
        }

        // ------------------------------------------------------------------ //
        //  Helpers                                                             //
        // ------------------------------------------------------------------ //

        private static void ExtractZip(string zipPath, string destDir, string password)
        {
            string canonDest = Path.GetFullPath(destDir).TrimEnd(
                Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                + Path.DirectorySeparatorChar;

            using (FileStream fs = File.OpenRead(zipPath))
            using (ZipFile zip = new ZipFile(fs))
            {
                if (!string.IsNullOrEmpty(password))
                    zip.Password = password;

                foreach (ZipEntry entry in zip)
                {
                    if (!entry.IsFile) continue;

                    string dest = Path.GetFullPath(Path.Combine(destDir, entry.Name));

                    // Zip-slip guard
                    if (!dest.StartsWith(canonDest, StringComparison.OrdinalIgnoreCase))
                        continue;

                    string dir = Path.GetDirectoryName(dest);
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    using (Stream entryStream = zip.GetInputStream(entry))
                    using (FileStream outStream = File.Create(dest))
                        entryStream.CopyTo(outStream);
                }
            }
        }

        private void RelaunchClient()
        {
            try
            {
                if (!string.IsNullOrEmpty(Program.RelaunchPath) &&
                    File.Exists(Program.RelaunchPath))
                {
                    Process.Start(Program.RelaunchPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not relaunch the client: " + ex.Message,
                    "Relaunch Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Application.Exit();
        }

        private void SetStatus(string text)
        {
            lblStatus.Text = text;
        }

        private void SetUIBusy(bool busy)
        {
            btnAction.Enabled   = !busy;
            txtLicenseKey.Enabled = !busy;
            if (!_settings.IsConfigured)
            {
                txtInstallDir.Enabled = !busy;
                btnBrowse.Enabled     = !busy;
            }
        }

        // ------------------------------------------------------------------ //
        //  Inner types                                                         //
        // ------------------------------------------------------------------ //

        private class WorkerArgs
        {
            public string Key        { get; set; }
            public string InstallDir { get; set; }
        }

        private class WorkerResult
        {
            public bool   AlreadyUpToDate { get; set; }
            public string Version         { get; set; }
            public string Changelog       { get; set; }
        }
    }
}
