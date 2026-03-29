using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    public partial class BotControlForm : Form
    {
        private static BotControlForm _instance;

        private static readonly Color SuccessCol = Color.FromArgb(80, 200, 120);
        private static readonly Color WarningCol = Color.FromArgb(255, 180, 50);
        private static readonly Color ErrorCol = Color.FromArgb(240, 80, 80);
        private static readonly Color AccentCol = Color.FromArgb(80, 160, 255);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);

        // Village Sync runtime state
        private Timer _vsRefreshTimer;
        private List<VillageRow> _vsVillageRows = new List<VillageRow>();

        // Radar runtime state
        private Timer _rdRefreshTimer;
        private List<ActionRow> _rdActionRows = new List<ActionRow>();

        public static void ShowInstance()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new BotControlForm();
            }

            if (!_instance.Visible)
                _instance.Show();

            _instance.BringToFront();
        }

        private bool IsDesignTime
        {
            get
            {
                return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                    || DesignMode;
            }
        }

        public BotControlForm()
        {
            InitializeComponent();

            if (!IsDesignTime)
            {
                WireUpVillageSyncTab();
                WireUpRadarTab();
                SubscribeToLog();
                RefreshStatus();
                ReplayExistingLogs();

                VsLoadFromSettings();
                RdLoadFromSettings();
                RdBuildActionRows();
            }
        }

        // =====================================================================
        // Village Sync tab runtime
        // =====================================================================

        private void WireUpVillageSyncTab()
        {
            _vsRefreshBtn.Click += delegate { VsPopulateVillageList(); };
            _vsSelectAllBtn.Click += delegate { VsSetAllChecked(true); };
            _vsDeselectAllBtn.Click += delegate { VsSetAllChecked(false); };

            _vsRefreshTimer = new Timer();
            _vsRefreshTimer.Interval = 2000;
            _vsRefreshTimer.Tick += delegate { VsUpdateStatusDisplay(); };
            _vsRefreshTimer.Start();
        }

        private void VsLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            VillageSyncSettings s = BotEngine.Instance.Settings.VillageSync;
            _vsEnabledCheck.Checked = s.Enabled;
            _vsIntervalInput.Value = Math.Max(_vsIntervalInput.Minimum,
                Math.Min(_vsIntervalInput.Maximum, s.IntervalSeconds));
            _vsDelayInput.Value = Math.Max(_vsDelayInput.Minimum,
                Math.Min(_vsDelayInput.Maximum, s.DelayBetweenVillagesMs));

            VsPopulateVillageList();
        }

        private void VsWriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            VillageSyncSettings s = BotEngine.Instance.Settings.VillageSync;
            s.Enabled = _vsEnabledCheck.Checked;
            s.IntervalSeconds = (int)_vsIntervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_vsDelayInput.Value;

            foreach (VillageRow row in _vsVillageRows)
            {
                s.SetVillageEnabled(row.VillageId, row.IsChecked);
            }

            foreach (IBotModule module in BotEngine.Instance.Modules)
            {
                if (module is VillageSyncModule)
                    module.Enabled = s.Enabled;
            }
        }

        private void VsPopulateVillageList()
        {
            _vsVillageListPanel.SuspendLayout();
            foreach (VillageRow row in _vsVillageRows)
            {
                _vsVillageListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _vsVillageRows.Clear();

            VillageSyncModule syncModule = null;
            if (BotEngine.Instance != null)
            {
                foreach (IBotModule m in BotEngine.Instance.Modules)
                {
                    syncModule = m as VillageSyncModule;
                    if (syncModule != null) break;
                }
            }

            if (syncModule == null)
            {
                _vsVillageListPanel.ResumeLayout();
                return;
            }

            List<int> allIds = syncModule.GetAllKnownVillageIds();
            VillageSyncSettings settings = (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                ? BotEngine.Instance.Settings.VillageSync
                : null;

            allIds.Sort(delegate(int a, int b)
            {
                int wa = VsGetSortWeight(a);
                int wb = VsGetSortWeight(b);
                if (wa != wb) return wb.CompareTo(wa);
                string na = VsGetVillageName(a);
                string nb = VsGetVillageName(b);
                return string.Compare(na, nb, StringComparison.OrdinalIgnoreCase);
            });

            bool alternate = false;
            foreach (int id in allIds)
            {
                bool enabled = settings == null || settings.IsVillageEnabled(id);
                string name = VsGetVillageName(id);
                string typeLabel = VillageSyncModule.GetVillageTypeLabel(id);

                VillageRow row = new VillageRow(id, name, typeLabel, enabled, alternate);
                row.Dock = DockStyle.Top;
                _vsVillageListPanel.Controls.Add(row);
                _vsVillageListPanel.Controls.SetChildIndex(row, 0);
                _vsVillageRows.Add(row);
                alternate = !alternate;
            }

            _vsVillageListPanel.ResumeLayout();
        }

        private void VsSetAllChecked(bool isChecked)
        {
            foreach (VillageRow row in _vsVillageRows)
                row.IsChecked = isChecked;
        }

        private void VsUpdateStatusDisplay()
        {
            if (_vsEnabledCheck == null) return;

            bool enabled = _vsEnabledCheck.Checked;
            _vsStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _vsStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;

            if (BotEngine.Instance != null)
            {
                foreach (IBotModule m in BotEngine.Instance.Modules)
                {
                    if (m is VillageSyncModule)
                    {
                        if (m.LastRun == DateTime.MinValue)
                            _vsLastRunLabel.Text = "Last run: never";
                        else
                            _vsLastRunLabel.Text = "Last run: " + m.LastRun.ToString("HH:mm:ss") +
                                                   " (" + (int)(DateTime.Now - m.LastRun).TotalSeconds + "s ago)";
                        break;
                    }
                }
            }
        }

        private static string VsGetVillageName(int villageId)
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                return GameEngine.Instance.World.getVillageName(villageId);
            return "Village " + villageId;
        }

        private static int VsGetSortWeight(int villageId)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return 0;
            VillageData data = GameEngine.Instance.World.getVillageData(villageId);
            if (data == null) return 0;
            if (data.countryCapital) return 4;
            if (data.provinceCapital) return 3;
            if (data.countyCapital) return 2;
            if (data.regionCapital) return 1;
            return 0;
        }

        // =====================================================================
        // Radar tab runtime
        // =====================================================================

        private void WireUpRadarTab()
        {
            _rdTestDiscordBtn.Click += delegate
            {
                string url = _rdWebhookInput.Text.Trim();
                if (string.IsNullOrEmpty(url))
                {
                    BotLogger.Log("Radar", BotLogLevel.Warning, "No webhook URL set.");
                    return;
                }
                DiscordNotifier.SendAsync(url, "\u2705 Webhook Test",
                    "Project Hades Radar is connected!", 5025616);
                BotLogger.Log("Radar", BotLogLevel.Info, "Test webhook sent.");
            };

            _rdRefreshTimer = new Timer();
            _rdRefreshTimer.Interval = 2000;
            _rdRefreshTimer.Tick += delegate { RdUpdateStatusDisplay(); };
            _rdRefreshTimer.Start();
        }

        private void RdLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            RadarSettings s = BotEngine.Instance.Settings.Radar;
            _rdEnabledCheck.Checked = s.Enabled;
            _rdScanIntervalInput.Value = Math.Max(_rdScanIntervalInput.Minimum,
                Math.Min(_rdScanIntervalInput.Maximum, s.ScanIntervalSeconds));
            _rdWebhookInput.Text = s.DiscordWebhookUrl ?? "";
            _rdInterdictMonkCountInput.Value = Math.Max(_rdInterdictMonkCountInput.Minimum,
                Math.Min(_rdInterdictMonkCountInput.Maximum, s.AutoInterdictMonkCount));

            foreach (ActionRow row in _rdActionRows)
            {
                RadarActionSettings actionSettings = s.GetActionSettings(row.ActionKey);
                row.SetValues(actionSettings);
            }
        }

        private void RdWriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            RadarSettings s = BotEngine.Instance.Settings.Radar;
            s.Enabled = _rdEnabledCheck.Checked;
            s.ScanIntervalSeconds = (int)_rdScanIntervalInput.Value;
            s.DiscordWebhookUrl = _rdWebhookInput.Text.Trim();
            s.AutoInterdictMonkCount = (int)_rdInterdictMonkCountInput.Value;

            foreach (ActionRow row in _rdActionRows)
            {
                RadarActionSettings actionSettings = s.GetActionSettings(row.ActionKey);
                row.GetValues(actionSettings);
            }

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is RadarModule)
                    m.Enabled = s.Enabled;
            }
        }

        private void RdBuildActionRows()
        {
            _rdActionListPanel.SuspendLayout();
            foreach (ActionRow row in _rdActionRows)
            {
                _rdActionListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _rdActionRows.Clear();

            RadarSettings settings = null;
            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                settings = BotEngine.Instance.Settings.Radar;

            bool alternate = false;
            foreach (string key in RadarModule.AllActionKeys)
            {
                string label = RadarModule.GetActionLabel(key);
                RadarActionSettings actionSettings = settings != null
                    ? settings.GetActionSettings(key)
                    : new RadarActionSettings();

                ActionRow row = new ActionRow(key, label, actionSettings, alternate);
                row.Dock = DockStyle.Top;
                _rdActionListPanel.Controls.Add(row);
                _rdActionListPanel.Controls.SetChildIndex(row, 0);
                _rdActionRows.Add(row);
                alternate = !alternate;
            }

            _rdActionListPanel.ResumeLayout();
        }

        private void RdUpdateStatusDisplay()
        {
            if (_rdEnabledCheck == null) return;
            bool enabled = _rdEnabledCheck.Checked;
            _rdStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _rdStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
        }

        // =====================================================================
        // Log
        // =====================================================================

        private void SubscribeToLog()
        {
            BotLogger.OnLogAdded += OnLogEntryAdded;
        }

        private void OnLogEntryAdded(BotLogEntry entry)
        {
            if (_logBox.InvokeRequired)
            {
                try
                {
                    _logBox.BeginInvoke(new Action<BotLogEntry>(AppendLogEntry), entry);
                }
                catch { }
                return;
            }
            AppendLogEntry(entry);
        }

        private void AppendLogEntry(BotLogEntry entry)
        {
            if (_logBox.IsDisposed) return;

            Color color;
            switch (entry.Level)
            {
                case BotLogLevel.Debug: color = TextSec; break;
                case BotLogLevel.Warning: color = WarningCol; break;
                case BotLogLevel.Error: color = ErrorCol; break;
                default: color = TextPri; break;
            }

            string ts = entry.Timestamp.ToString("HH:mm:ss");
            string lvl = entry.Level.ToString().ToUpper().PadRight(5);
            string line = ts + " [" + lvl + "] [" + entry.ModuleName + "] " + entry.Message + "\n";

            _logBox.SelectionStart = _logBox.TextLength;
            _logBox.SelectionLength = 0;
            _logBox.SelectionColor = color;
            _logBox.AppendText(line);

            if (_logBox.Lines.Length > 3000)
            {
                _logBox.SelectionStart = 0;
                _logBox.SelectionLength = _logBox.GetFirstCharIndexFromLine(500);
                _logBox.SelectedText = "";
            }
            _logBox.SelectionStart = _logBox.TextLength;
            _logBox.ScrollToCaret();
        }

        private void ReplayExistingLogs()
        {
            foreach (BotLogEntry entry in BotLogger.GetEntries())
                AppendLogEntry(entry);
        }

        // =====================================================================
        // Status
        // =====================================================================

        private void RefreshStatus()
        {
            bool running = BotEngine.Instance != null && BotEngine.Instance.IsRunning;
            _statusLabel.Text = running ? "RUNNING" : "STOPPED";
            _statusLabel.ForeColor = running ? SuccessCol : ErrorCol;
            _masterToggleBtn.Text = running ? "STOP BOT" : "START BOT";
            _masterToggleBtn.BackColor = running ? ErrorCol : AccentCol;
        }

        // =====================================================================
        // Event handlers
        // =====================================================================

        private void MasterToggle_Click(object sender, EventArgs e)
        {
            if (BotEngine.Instance == null) return;

            BotEngine.Instance.Settings.BotEnabled = !BotEngine.Instance.Settings.BotEnabled;
            RefreshStatus();

            if (BotEngine.Instance.IsRunning)
                BotLogger.Log("BotEngine", BotLogLevel.Info, "Bot started by user.");
            else
                BotLogger.Log("BotEngine", BotLogLevel.Info, "Bot stopped by user.");
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (BotEngine.Instance == null) return;

            VsWriteToSettings();
            RdWriteToSettings();

            BotEngine.Instance.SaveSettings();
            BotLogger.Log("UI", BotLogLevel.Info, "Settings saved.");
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            if (BotEngine.Instance == null) return;

            BotEngine.Instance.ReloadSettings();

            VsLoadFromSettings();
            RdLoadFromSettings();

            RefreshStatus();
            BotLogger.Log("UI", BotLogLevel.Info, "Settings reloaded from disk.");
        }

        private void ClearLogBtn_Click(object sender, EventArgs e)
        {
            _logBox.Clear();
            BotLogger.Clear();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                return;
            }
            BotLogger.OnLogAdded -= OnLogEntryAdded;
            base.OnFormClosing(e);
        }
    }
}
