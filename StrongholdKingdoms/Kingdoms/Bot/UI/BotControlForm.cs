using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonTypes;
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

        // Recruiting runtime state
        private Timer _rcRefreshTimer;
        private List<RecruitVillagePanel> _rcVillagePanels = 	new List<RecruitVillagePanel>();
        private List<RecruitVillagePanel> _rcCapitalPanels = new List<RecruitVillagePanel>();

        // Castle Repair runtime state
        private Timer _crRefreshTimer;
        private List<CastleRepairVillageRow> _crVillageRows = new List<CastleRepairVillageRow>();
        private int _crLastPresetCount = -1;

        // Trade runtime state
        private Timer _trRefreshTimer;
        private int _trSelectedVillageId = -1;
        private ListBox _trVillageListBox;
        private CheckBox _trVillageTradingCheck;
        private TradeResourceGrid _trResourceGrid;
        private ListBox _trMarketsListBox;
        private Label _trMarketCountLabel;
        private List<TradeRouteRow> _trRouteRows = new List<TradeRouteRow>();
        // Player Routes tab
        private List<PlayerTradeRouteRow> _trPlayerRouteRows = new List<PlayerTradeRouteRow>();
        private int _trSelectedRouteIndex = -1;

        // Vassals tab runtime state
        private List<VassalVillagePanel> _vaVassalPanels = new List<VassalVillagePanel>();

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
                WireUpRecruitingTab();
                WireUpCastleRepairTab();
                WireUpVassalsTab();
                WireUpTradeTab();
                SubscribeToLog();
                RefreshStatus();
                ReplayExistingLogs();

                VsLoadFromSettings();
                RdLoadFromSettings();
                RdBuildActionRows();
                RcLoadFromSettings();
                CrLoadFromSettings();
                VaLoadFromSettings();
                TrLoadFromSettings();
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
            _vsSelectVillagesBtn.Click += delegate { VsToggleVillages(); };
            _vsSelectCapitalsBtn.Click += delegate { VsToggleCapitals(); };

            _vsEnabledCheck.CheckedChanged += delegate { VsPushToSettings(); };
            _vsIntervalInput.ValueChanged += delegate { VsPushToSettings(); };
            _vsDelayInput.ValueChanged += delegate { VsPushToSettings(); };

            _vsRefreshTimer = new Timer();
            _vsRefreshTimer.Interval = 2000;
            _vsRefreshTimer.Tick += delegate { VsUpdateStatusDisplay(); };
            _vsRefreshTimer.Start();
        }

        private void VsPushToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            VillageSyncSettings s = BotEngine.Instance.Settings.VillageSync;
            s.Enabled = _vsEnabledCheck.Checked;
            s.IntervalSeconds = (int)_vsIntervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_vsDelayInput.Value;

            foreach (IBotModule module in BotEngine.Instance.Modules)
            {
                if (module is VillageSyncModule)
                    module.Enabled = s.Enabled;
            }
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
                _vsVillageListPanel.ResumeLayout(false);
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
                if (wa != wb) return wa.CompareTo(wb);
                string na = VsGetVillageName(a);
                string nb = VsGetVillageName(b);
                return string.Compare(na, nb, StringComparison.OrdinalIgnoreCase);
            });

            bool alternate = false;
            // Add in reverse so DockStyle.Top stacks them in the correct order
            for (int i = allIds.Count - 1; i >= 0; i--)
            {
                int id = allIds[i];
                bool enabled = settings == null || settings.IsVillageEnabled(id);
                string name = VsGetVillageName(id);
                string typeLabel = VillageSyncModule.GetVillageTypeLabel(id);

                VillageRow row = new VillageRow(id, name, typeLabel, enabled, (allIds.Count - 1 - i) % 2 != 0);
                row.Dock = DockStyle.Top;
                _vsVillageListPanel.Controls.Add(row);
                _vsVillageRows.Add(row);
            }

            _vsVillageListPanel.ResumeLayout(false);
            _vsVillageListPanel.PerformLayout();
        }

        private void VsSetAllChecked(bool isChecked)
        {
            foreach (VillageRow row in _vsVillageRows)
                row.IsChecked = isChecked;
        }

        private void VsToggleVillages()
        {
            bool allChecked = true;
            foreach (VillageRow row in _vsVillageRows)
            {
                if (row.TypeLabel == "Village" && !row.IsChecked)
                {
                    allChecked = false;
                    break;
                }
            }

            foreach (VillageRow row in _vsVillageRows)
            {
                if (row.TypeLabel == "Village")
                    row.IsChecked = !allChecked;
            }
        }

        private void VsToggleCapitals()
        {
            bool allChecked = true;
            foreach (VillageRow row in _vsVillageRows)
            {
                bool isCapital = row.TypeLabel == "Parish"
                    || row.TypeLabel == "County"
                    || row.TypeLabel == "Province"
                    || row.TypeLabel == "Country";

                if (isCapital && !row.IsChecked)
                {
                    allChecked = false;
                    break;
                }
            }

            foreach (VillageRow row in _vsVillageRows)
            {
                bool isCapital = row.TypeLabel == "Parish"
                    || row.TypeLabel == "County"
                    || row.TypeLabel == "Province"
                    || row.TypeLabel == "Country";

                if (isCapital)
                    row.IsChecked = !allChecked;
            }
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

            _rdEnabledCheck.CheckedChanged += delegate { RdPushToSettings(); };
            _rdScanIntervalInput.ValueChanged += delegate { RdPushToSettings(); };
            _rdWebhookInput.TextChanged += delegate { RdPushToSettings(); };
            _rdInterdictMonkCountInput.ValueChanged += delegate { RdPushToSettings(); };
            _rdAutoRecruitMonksCheck.CheckedChanged += delegate { RdPushToSettings(); };
            _rdMinArmySizeInput.ValueChanged += delegate { RdPushToSettings(); };

            _rdRefreshTimer = new Timer();
            _rdRefreshTimer.Interval = 2000;
            _rdRefreshTimer.Tick += delegate { RdUpdateStatusDisplay(); };
            _rdRefreshTimer.Start();
        }

        private void RdPushToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            RadarSettings s = BotEngine.Instance.Settings.Radar;
            s.Enabled = _rdEnabledCheck.Checked;
            s.ScanIntervalSeconds = (int)_rdScanIntervalInput.Value;
            s.DiscordWebhookUrl = _rdWebhookInput.Text.Trim();
            s.AutoInterdictMonkCount = (int)_rdInterdictMonkCountInput.Value;
            s.AutoRecruitMonks = _rdAutoRecruitMonksCheck.Checked;
            s.MinArmySizeForInterdict = (int)_rdMinArmySizeInput.Value;

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is RadarModule)
                    m.Enabled = s.Enabled;
            }
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
            _rdAutoRecruitMonksCheck.Checked = s.AutoRecruitMonks;
            _rdMinArmySizeInput.Value = Math.Max(_rdMinArmySizeInput.Minimum,
                Math.Min(_rdMinArmySizeInput.Maximum, s.MinArmySizeForInterdict));

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
            s.AutoRecruitMonks = _rdAutoRecruitMonksCheck.Checked;
            s.MinArmySizeForInterdict = (int)_rdMinArmySizeInput.Value;

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

            string[] keys = RadarModule.AllActionKeys;
            // Add in reverse for correct DockStyle.Top stacking
            for (int i = keys.Length - 1; i >= 0; i--)
            {
                string label = RadarModule.GetActionLabel(keys[i]);
                RadarActionSettings actionSettings = settings != null
                    ? settings.GetActionSettings(keys[i])
                    : new RadarActionSettings();

                ActionRow row = new ActionRow(keys[i], label, actionSettings, (keys.Length - 1 - i) % 2 != 0);
                row.Dock = DockStyle.Top;
                _rdActionListPanel.Controls.Add(row);
                _rdActionRows.Add(row);
            }

            _rdActionListPanel.ResumeLayout(false);
            _rdActionListPanel.PerformLayout();
        }

        private void RdUpdateStatusDisplay()
        {
            if (_rdEnabledCheck == null) return;
            bool enabled = _rdEnabledCheck.Checked;
            _rdStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _rdStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
        }

        // =====================================================================
        // Recruiting tab runtime
        // =====================================================================

        private void WireUpRecruitingTab()
        {
            _rcRefreshBtn.Click += delegate { RcBuildVillageList(); };

            _rcDisbandCombo.Items.Clear();
            foreach (string option in RecruitingModule.DisbandOptions)
                _rcDisbandCombo.Items.Add("Disband All " + option);
            if (_rcDisbandCombo.Items.Count > 0)
                _rcDisbandCombo.SelectedIndex = 0;

            _rcDisbandBtn.Click += delegate { RcDisbandClick(); };

            _rcEnabledCheck.CheckedChanged += delegate { RcPushToSettings(); };
            _rcIntervalInput.ValueChanged += delegate { RcPushToSettings(); };
            _rcDelayInput.ValueChanged += delegate { RcPushToSettings(); };

            // Villages tab: add toolbar above column header
            RcBuildColumnHeaders(_rcColHeaderVillages, RecruitingModule.AllUnitKeys);
            Panel villageToolbar = RcMakeToolbarPanel();
            Button copyVillagesBtn = RcMakeCopyButton();
            copyVillagesBtn.Click += delegate { RcCopySettingsClick(CopyRecruitSettingsForm.CopyMode.Villages); };
            villageToolbar.Controls.Add(copyVillagesBtn);
            _rcVillagesTab.Controls.Add(villageToolbar);

            // Capitals tab: add toolbar above column header, use capital-only unit keys
            RcBuildColumnHeaders(_rcColHeaderCapitals, RecruitingModule.CapitalUnitKeys);
            Panel capitalToolbar = RcMakeToolbarPanel();
            Button copyCapitalsBtn = RcMakeCopyButton();
            copyCapitalsBtn.Click += delegate { RcCopySettingsClick(CopyRecruitSettingsForm.CopyMode.Capitals); };
            capitalToolbar.Controls.Add(copyCapitalsBtn);
            _rcCapitalsTab.Controls.Add(capitalToolbar);

            _rcRefreshTimer = new Timer();
            _rcRefreshTimer.Interval = 2000;
            _rcRefreshTimer.Tick += delegate { RcUpdateStatusDisplay(); };
            _rcRefreshTimer.Start();
        }

        private static Panel RcMakeToolbarPanel()
        {
            Panel bar = new Panel();
            bar.Dock = DockStyle.Top;
            bar.Height = 28;
            bar.BackColor = Color.FromArgb(36, 38, 48);
            return bar;
        }

        private static Button RcMakeCopyButton()
        {
            Button btn = new Button();
            btn.Text = "Copy Settings";
            btn.BackColor = Color.FromArgb(60, 80, 140);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            btn.Size = new Size(90, 22);
            btn.Location = new Point(8, 3);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private void RcCopySettingsClick(CopyRecruitSettingsForm.CopyMode mode)
        {
            // Save current panels to settings first
            RcWriteToSettings();

            CopyRecruitSettingsForm form = new CopyRecruitSettingsForm(mode);
            form.ShowDialog(this);

            if (form.Copied)
                RcBuildVillageList();
        }

        private void RcBuildColumnHeaders(Panel headerPanel, string[] unitKeys)
        {
            Label villageHdr = new Label();
            villageHdr.Text = "Village";
            villageHdr.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            villageHdr.ForeColor = TextSec;
            villageHdr.AutoSize = true;
            villageHdr.Location = new Point(8, 5);
            headerPanel.Controls.Add(villageHdr);

            for (int i = 0; i < unitKeys.Length; i++)
            {
                int x = RecruitVillagePanel.VillageNameWidth + (i * RecruitVillagePanel.UnitColWidth);

                Label nameHdr = new Label();
                nameHdr.Text = unitKeys[i];
                nameHdr.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
                nameHdr.ForeColor = TextSec;
                nameHdr.AutoSize = true;
                nameHdr.Location = new Point(x, 5);
                headerPanel.Controls.Add(nameHdr);

                Label priHdr = new Label();
                priHdr.Text = "Priority";
                priHdr.Font = new Font("Segoe UI", 6.5f, FontStyle.Bold);
                priHdr.ForeColor = Color.FromArgb(120, 125, 140);
                priHdr.AutoSize = true;
                priHdr.Location = new Point(x + 52, 5);
                headerPanel.Controls.Add(priHdr);
            }
        }

        private void RcPushToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            RecruitingSettings s = BotEngine.Instance.Settings.Recruiting;
            s.Enabled = _rcEnabledCheck.Checked;
            s.CycleIntervalSeconds = (int)_rcIntervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_rcDelayInput.Value;

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is RecruitingModule)
                    m.Enabled = s.Enabled;
            }
        }

        private void RcLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            RecruitingSettings s = BotEngine.Instance.Settings.Recruiting;
            _rcEnabledCheck.Checked = s.Enabled;
            _rcIntervalInput.Value = Math.Max(_rcIntervalInput.Minimum,
                Math.Min(_rcIntervalInput.Maximum, s.CycleIntervalSeconds));
            _rcDelayInput.Value = Math.Max(_rcDelayInput.Minimum,
                Math.Min(_rcDelayInput.Maximum, s.DelayBetweenVillagesMs));

            RcBuildVillageList();
        }

        private void RcWriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            RecruitingSettings s = BotEngine.Instance.Settings.Recruiting;
            s.Enabled = _rcEnabledCheck.Checked;
            s.CycleIntervalSeconds = (int)_rcIntervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_rcDelayInput.Value;

            foreach (RecruitVillagePanel panel in _rcVillagePanels)
                panel.WriteToSettings(s);
            foreach (RecruitVillagePanel panel in _rcCapitalPanels)
                panel.WriteToSettings(s);

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is RecruitingModule)
                    m.Enabled = s.Enabled;
            }
        }

        private void RcBuildVillageList()
        {
            _rcVillageListPanel.SuspendLayout();
            _rcCapitalsListPanel.SuspendLayout();

            foreach (RecruitVillagePanel panel in _rcVillagePanels)
            {
                _rcVillageListPanel.Controls.Remove(panel);
                panel.Dispose();
            }
            _rcVillagePanels.Clear();

            foreach (RecruitVillagePanel panel in _rcCapitalPanels)
            {
                _rcCapitalsListPanel.Controls.Remove(panel);
                panel.Dispose();
            }
            _rcCapitalPanels.Clear();

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
            {
                _rcVillageListPanel.ResumeLayout(false);
                _rcCapitalsListPanel.ResumeLayout(false);
                return;
            }

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null)
            {
                _rcVillageListPanel.ResumeLayout(false);
                _rcCapitalsListPanel.ResumeLayout(false);
                return;
            }

            RecruitingSettings settings = null;
            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                settings = BotEngine.Instance.Settings.Recruiting;

            // Separate into villages and capitals first, then add in reverse for correct dock order
            List<RecruitVillagePanel> villagePanels = new List<RecruitVillagePanel>();
            List<RecruitVillagePanel> capitalPanels = new List<RecruitVillagePanel>();

            for (int i = 0; i < villages.Count; i++)
            {
                int id = villages[i].villageID;
                string name = RcGetVillageName(id);
                string typeLabel = VillageSyncModule.GetVillageTypeLabel(id);
                VillageRecruitSettings vs = settings != null
                    ? settings.GetVillageSettings(id)
                    : null;

                bool isCapital = typeLabel == "Parish"
                    || typeLabel == "County"
                    || typeLabel == "Province"
                    || typeLabel == "Country";

                if (isCapital)
                    capitalPanels.Add(new RecruitVillagePanel(id, name, vs, capitalPanels.Count % 2 != 0, RecruitingModule.CapitalUnitKeys));
                else
                    villagePanels.Add(new RecruitVillagePanel(id, name, vs, villagePanels.Count % 2 != 0));
            }

            // Add in reverse for correct DockStyle.Top stacking
            for (int i = villagePanels.Count - 1; i >= 0; i--)
            {
                villagePanels[i].Dock = DockStyle.Top;
                _rcVillageListPanel.Controls.Add(villagePanels[i]);
            }
            _rcVillagePanels = villagePanels;

            for (int i = capitalPanels.Count - 1; i >= 0; i--)
            {
                capitalPanels[i].Dock = DockStyle.Top;
                _rcCapitalsListPanel.Controls.Add(capitalPanels[i]);
            }
            _rcCapitalPanels = capitalPanels;

            _rcVillageListPanel.ResumeLayout(false);
            _rcVillageListPanel.PerformLayout();
            _rcCapitalsListPanel.ResumeLayout(false);
            _rcCapitalsListPanel.PerformLayout();
        }

        private void RcDisbandClick()
        {
            if (_rcDisbandCombo.SelectedIndex < 0)
                return;

            if (_rcDisbandCombo.SelectedIndex >= RecruitingModule.DisbandOptions.Length)
                return;

            string unitKey = RecruitingModule.DisbandOptions[_rcDisbandCombo.SelectedIndex];

            DialogResult result = MessageBox.Show(
                "Are you sure you want to disband all " + unitKey + " in every village?",
                "Confirm Disband",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            RecruitingModule module = null;
            if (BotEngine.Instance != null)
            {
                foreach (IBotModule m in BotEngine.Instance.Modules)
                {
                    module = m as RecruitingModule;
                    if (module != null) break;
                }
            }

            if (module != null)
            {
                module.DisbandAll(unitKey);
                BotLogger.Log("Recruiting", BotLogLevel.Info,
                    "Disband all " + unitKey + " requested by user.");
            }
        }

        private void RcUpdateStatusDisplay()
        {
            if (_rcEnabledCheck == null) return;
            bool enabled = _rcEnabledCheck.Checked;
            _rcStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _rcStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
        }

        private static string RcGetVillageName(int villageId)
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                return GameEngine.Instance.World.getVillageName(villageId);
            return "Village " + villageId;
        }

        // =====================================================================
        // Castle Repair tab runtime
        // =====================================================================

        private void WireUpCastleRepairTab()
        {
            _crRefreshBtn.Click += delegate { CrPopulateVillageList(); };
            _crRepairAllBtn.Click += delegate { CrRepairAllNow(); };

            _crEnabledCheck.CheckedChanged += delegate { CrPushToSettings(); };
            _crIntervalInput.ValueChanged += delegate { CrPushToSettings(); };
            _crDelayInput.ValueChanged += delegate { CrPushToSettings(); };
            _crRepairOnAttackCheck.CheckedChanged += delegate { CrPushToSettings(); };

            CrBuildColumnHeaders();

            // Wire Copy Settings button (defined in Designer)
            _crCopySettingsBtn.Click += delegate { CrCopySettingsClick(); };

            _crRefreshTimer = new Timer();
            _crRefreshTimer.Interval = 2000;
            _crRefreshTimer.Tick += delegate { CrUpdateStatusDisplay(); };
            _crRefreshTimer.Start();
        }

        private void CrCopySettingsClick()
        {
            CrWriteToSettings();

            CopyCastleRepairSettingsForm form = new CopyCastleRepairSettingsForm();
            form.ShowDialog(this);

            if (form.Copied)
            {
                CrLoadFromSettings();
                CrPopulateVillageList();
            }
        }

        private void CrBuildColumnHeaders()
        {
            Label hdrName = new Label();
            hdrName.Text = "Village";
            hdrName.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            hdrName.ForeColor = TextSec;
            hdrName.AutoSize = true;
            hdrName.Location = new Point(8, 5);
            _crColHeader.Controls.Add(hdrName);

            Label hdrType = new Label();
            hdrType.Text = "Type";
            hdrType.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            hdrType.ForeColor = TextSec;
            hdrType.AutoSize = true;
            hdrType.Location = new Point(130, 5);
            _crColHeader.Controls.Add(hdrType);

            Label hdrInfra = new Label();
            hdrInfra.Text = "Infra";
            hdrInfra.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            hdrInfra.ForeColor = TextSec;
            hdrInfra.AutoSize = true;
            hdrInfra.Location = new Point(200, 5);
            _crColHeader.Controls.Add(hdrInfra);

            Label hdrTroops = new Label();
            hdrTroops.Text = "Troops";
            hdrTroops.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            hdrTroops.ForeColor = TextSec;
            hdrTroops.AutoSize = true;
            hdrTroops.Location = new Point(270, 5);
            _crColHeader.Controls.Add(hdrTroops);

            Label hdrInfraPreset = new Label();
            hdrInfraPreset.Text = "Infra Preset";
            hdrInfraPreset.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            hdrInfraPreset.ForeColor = TextSec;
            hdrInfraPreset.AutoSize = true;
            hdrInfraPreset.Location = new Point(320, 5);
            _crColHeader.Controls.Add(hdrInfraPreset);

            Label hdrTroopPreset = new Label();
            hdrTroopPreset.Text = "Troop Preset";
            hdrTroopPreset.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            hdrTroopPreset.ForeColor = TextSec;
            hdrTroopPreset.AutoSize = true;
            hdrTroopPreset.Location = new Point(520, 5);
            _crColHeader.Controls.Add(hdrTroopPreset);
        }

        private void CrRepairAllNow()
        {
            if (BotEngine.Instance == null) return;

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                CastleRepairModule crm = m as CastleRepairModule;
                if (crm != null)
                {
                    CrWriteToSettings();
                    crm.RepairAllNow();
                    break;
                }
            }
        }

        private void CrPushToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            CastleRepairSettings s = BotEngine.Instance.Settings.CastleRepair;
            s.Enabled = _crEnabledCheck.Checked;
            s.IntervalSeconds = (int)_crIntervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_crDelayInput.Value;
            s.RepairOnAttack = _crRepairOnAttackCheck.Checked;

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is CastleRepairModule)
                    m.Enabled = s.Enabled;
            }
        }

        private void CrLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            CastleRepairSettings s = BotEngine.Instance.Settings.CastleRepair;
            _crEnabledCheck.Checked = s.Enabled;
            _crIntervalInput.Value = Math.Max(_crIntervalInput.Minimum,
                Math.Min(_crIntervalInput.Maximum, s.IntervalSeconds));
            _crDelayInput.Value = Math.Max(_crDelayInput.Minimum,
                Math.Min(_crDelayInput.Maximum, s.DelayBetweenVillagesMs));
            _crRepairOnAttackCheck.Checked = s.RepairOnAttack;

            CrPopulateVillageList();
        }

        private void CrWriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            CastleRepairSettings s = BotEngine.Instance.Settings.CastleRepair;
            s.Enabled = _crEnabledCheck.Checked;
            s.IntervalSeconds = (int)_crIntervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_crDelayInput.Value;
            s.RepairOnAttack = _crRepairOnAttackCheck.Checked;

            foreach (CastleRepairVillageRow row in _crVillageRows)
                row.WriteToSettings(s);

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is CastleRepairModule)
                    m.Enabled = s.Enabled;
            }
        }

        private void CrPopulateVillageList()
        {
            _crVillageListPanel.SuspendLayout();
            foreach (CastleRepairVillageRow row in _crVillageRows)
            {
                _crVillageListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _crVillageRows.Clear();

            CastleRepairModule repairModule = null;
            if (BotEngine.Instance != null)
            {
                foreach (IBotModule m in BotEngine.Instance.Modules)
                {
                    repairModule = m as CastleRepairModule;
                    if (repairModule != null) break;
                }
            }

            if (repairModule == null)
            {
                _crVillageListPanel.ResumeLayout();
                return;
            }

            List<int> allIds = repairModule.GetAllKnownVillageIds();
            CastleRepairSettings settings = (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                ? BotEngine.Instance.Settings.CastleRepair
                : null;

            List<string> infraPresets = CastleRepairModule.GetPresetNames(PresetType.INFRASTRUCTURE);
            List<string> troopPresets = CastleRepairModule.GetPresetNames(PresetType.TROOP_DEFEND);

            allIds.Sort(delegate(int a, int b)
            {
                int wa = CrGetSortWeight(a);
                int wb = CrGetSortWeight(b);
                if (wa != wb) return wa.CompareTo(wb);
                string na = CrGetVillageName(a);
                string nb = CrGetVillageName(b);
                return string.Compare(na, nb, StringComparison.OrdinalIgnoreCase);
            });

            // Build all rows first
            List<CastleRepairVillageRow> rows = new List<CastleRepairVillageRow>();
            for (int i = 0; i < allIds.Count; i++)
            {
                int id = allIds[i];
                string name = CrGetVillageName(id);
                string typeLabel = VillageSyncModule.GetVillageTypeLabel(id);
                VillageCastleRepairSettings vs = settings != null
                    ? settings.GetVillageSettings(id)
                    : null;

                CastleRepairVillageRow row = new CastleRepairVillageRow(
                    id, name, typeLabel, vs, infraPresets, troopPresets, i % 2 != 0);
                row.Dock = DockStyle.Top;
                rows.Add(row);
            }

            // Add in reverse for correct DockStyle.Top stacking
            for (int i = rows.Count - 1; i >= 0; i--)
                _crVillageListPanel.Controls.Add(rows[i]);

            _crVillageRows = rows;

            _crVillageListPanel.ResumeLayout(false);
            _crVillageListPanel.PerformLayout();
        }

        private void CrUpdateStatusDisplay()
        {
            if (_crEnabledCheck == null) return;

            bool enabled = _crEnabledCheck.Checked;
            _crStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _crStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;

            // Auto-refresh combo boxes when cloud presets become available
            try
            {
                int currentCount = PresetManager.Instance.m_presets != null
                    ? PresetManager.Instance.m_presets.Count : 0;
                if (currentCount != _crLastPresetCount && _crLastPresetCount >= 0)
                {
                    _crLastPresetCount = currentCount;
                    CrPopulateVillageList();
                }
                else if (_crLastPresetCount < 0)
                {
                    _crLastPresetCount = currentCount;
                }
            }
            catch { }
        }

        private static string CrGetVillageName(int villageId)
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                return GameEngine.Instance.World.getVillageName(villageId);
            return "Village " + villageId;
        }

        private static int CrGetSortWeight(int villageId)
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
        // Vassals tab runtime
        // =====================================================================

        private void WireUpVassalsTab()
        {
            _vaRefreshBtn.Click += delegate { VaRequestVassalLoad(); };
            _vaMinTroopsInput.ValueChanged += delegate { VaPushMinTroops(); };

            // Copy Settings button next to Refresh List in vassal settings panel
            Button copyVassalsBtn = new Button();
            copyVassalsBtn.Text = "Copy Settings";
            copyVassalsBtn.BackColor = Color.FromArgb(60, 80, 140);
            copyVassalsBtn.ForeColor = Color.White;
            copyVassalsBtn.FlatStyle = FlatStyle.Flat;
            copyVassalsBtn.FlatAppearance.BorderSize = 0;
            copyVassalsBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            copyVassalsBtn.Size = new Size(100, 24);
            copyVassalsBtn.Location = new Point(_vaRefreshBtn.Right + 10, _vaRefreshBtn.Top);
            copyVassalsBtn.Cursor = Cursors.Hand;
            copyVassalsBtn.Click += delegate { VaCopySettingsClick(); };
            _vaSettingsPanel.Controls.Add(copyVassalsBtn);
        }

        private void VaCopySettingsClick()
        {
            // Save current panels to settings first
            VaWriteToSettings();

            CopyRecruitSettingsForm form = new CopyRecruitSettingsForm(CopyRecruitSettingsForm.CopyMode.Vassals);
            form.ShowDialog(this);

            if (form.Copied)
                VaBuildVassalList();
        }

        private void VaRequestVassalLoad()
        {
            if (GameEngine.Instance == null)
                return;

            _vaRefreshBtn.Enabled = false;
            _vaRefreshBtn.Text = "Loading...";

            RemoteServices.Instance.set_VassalInfo_UserCallBack(
                new RemoteServices.VassalInfo_UserCallBack(VaVassalInfoCallback));
            RemoteServices.Instance.VassalInfo(-1);
        }

        private void VaVassalInfoCallback(VassalInfo_ReturnType returnData)
        {
            if (returnData.Success && GameEngine.Instance != null && GameEngine.Instance.vassalsManager != null)
            {
                GameEngine.Instance.vassalsManager.importVassals(returnData.liegeLordInfo, returnData.vassals);
                GameEngine.Instance.vassalsManager.importVassalRequests(returnData.requestsYouveMade, returnData.requestsOfYou);
                GameEngine.Instance.World.updateUserVassals();
            }

            if (_vaRefreshBtn.InvokeRequired)
            {
                _vaRefreshBtn.BeginInvoke(new MethodInvoker(delegate
                {
                    _vaRefreshBtn.Enabled = true;
                    _vaRefreshBtn.Text = "Refresh List";
                    VaBuildVassalList();
                }));
            }
            else
            {
                _vaRefreshBtn.Enabled = true;
                _vaRefreshBtn.Text = "Refresh List";
                VaBuildVassalList();
            }
        }

        private void VaPushMinTroops()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            BotEngine.Instance.Settings.Recruiting.VassalRecruiting.MinTroopsToSend = (int)_vaMinTroopsInput.Value;
        }

        private void VaLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            VassalRecruitingSettings s = BotEngine.Instance.Settings.Recruiting.VassalRecruiting;
            _vaMinTroopsInput.Value = Math.Max(_vaMinTroopsInput.Minimum,
                Math.Min(_vaMinTroopsInput.Maximum, s.MinTroopsToSend));

            VaRequestVassalLoad();
        }

        private void VaWriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            VassalRecruitingSettings s = BotEngine.Instance.Settings.Recruiting.VassalRecruiting;
            s.MinTroopsToSend = (int)_vaMinTroopsInput.Value;

            foreach (VassalVillagePanel panel in _vaVassalPanels)
                panel.WriteToSettings(s);
        }

        private void VaBuildVassalList()
        {
            _vaVassalListPanel.SuspendLayout();

            foreach (VassalVillagePanel panel in _vaVassalPanels)
            {
                _vaVassalListPanel.Controls.Remove(panel);
                panel.Dispose();
            }
            _vaVassalPanels.Clear();

            if (GameEngine.Instance == null || GameEngine.Instance.vassalsManager == null)
            {
                _vaVassalListPanel.ResumeLayout(false);
                _vaVassalListPanel.PerformLayout();
                return;
            }

            VassalRecruitingSettings settings = (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                ? BotEngine.Instance.Settings.Recruiting.VassalRecruiting
                : null;

            CommonTypes.VassalInfo[] vassals = GameEngine.Instance.vassalsManager.GetVassals();
            if (vassals == null || vassals.Length == 0)
            {
                _vaVassalListPanel.ResumeLayout(false);
                _vaVassalListPanel.PerformLayout();
                return;
            }

            List<VassalVillagePanel> panels = new List<VassalVillagePanel>();
            for (int i = 0; i < vassals.Length; i++)
            {
                CommonTypes.VassalInfo vi = vassals[i];
                int vid = vi.villageID;
                string name = VaGetVassalName(vid, vi.vassalPlayerName);

                VassalVillageRecruitSettings vs = settings != null
                    ? settings.GetVassalSettings(vid)
                    : null;

                VassalVillagePanel panel = new VassalVillagePanel(vid, name, vs, i % 2 != 0);
                panel.Dock = DockStyle.Top;
                panels.Add(panel);
            }

            for (int i = panels.Count - 1; i >= 0; i--)
                _vaVassalListPanel.Controls.Add(panels[i]);

            _vaVassalPanels = panels;

            _vaVassalListPanel.ResumeLayout(false);
            _vaVassalListPanel.PerformLayout();
        }

        private static string VaGetVassalName(int villageId, string playerName)
        {
            string villageName = "Village " + villageId;
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                villageName = GameEngine.Instance.World.getVillageName(villageId);

            if (!string.IsNullOrEmpty(playerName))
                return villageName + " (" + playerName + ")";
            return villageName;
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

            string tabName = "";
            if (_tabControl.SelectedTab == _villageSyncPage)
            {
                VsWriteToSettings();
                tabName = "Village Sync";
            }
            else if (_tabControl.SelectedTab == _radarPage)
            {
                RdWriteToSettings();
                tabName = "Radar";
            }
            else if (_tabControl.SelectedTab == _recruitingPage)
            {
                RcWriteToSettings();
                VaWriteToSettings();
                tabName = "Recruiting";
            }
            else if (_tabControl.SelectedTab == _crPage)
            {
                CrWriteToSettings();
                tabName = "Castle Repair";
            }
            else if (_tabControl.SelectedTab == _tradePage)
            {
                TrWriteToSettings();
                tabName = "Trade";
            }

            BotEngine.Instance.SaveSettings();
            BotLogger.Log("UI", BotLogLevel.Info, tabName + " settings saved.");
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            if (BotEngine.Instance == null) return;

            BotEngine.Instance.ReloadSettings();

            string tabName = "";
            if (_tabControl.SelectedTab == _villageSyncPage)
            {
                VsLoadFromSettings();
                tabName = "Village Sync";
            }
            else if (_tabControl.SelectedTab == _radarPage)
            {
                RdLoadFromSettings();
                tabName = "Radar";
            }
            else if (_tabControl.SelectedTab == _recruitingPage)
            {
                RcLoadFromSettings();
                VaLoadFromSettings();
                tabName = "Recruiting";
            }
            else if (_tabControl.SelectedTab == _crPage)
            {
                CrLoadFromSettings();
                tabName = "Castle Repair";
            }
            else if (_tabControl.SelectedTab == _tradePage)
            {
                TrLoadFromSettings();
                tabName = "Trade";
            }

            RefreshStatus();
            BotLogger.Log("UI", BotLogLevel.Info, tabName + " settings reloaded from disk.");
        }

        private void ClearLogBtn_Click(object sender, EventArgs e)
        {
            _logBox.Clear();
            BotLogger.Clear();
        }

        // =====================================================================
        // Trade tab runtime
        // =====================================================================

        private void WireUpTradeTab()
        {
            // Build the Markets sub-tab layout programmatically
            TrBuildMarketsTabLayout();
            // Build the Routes sub-tab layout programmatically
            TrBuildRoutesTabLayout();
            // Build the Player Routes sub-tab
            TrBuildPlayerRoutesTab();

            _trAddMarketsBtn.Click += delegate { TrAddMarketsClick(); };
            _trMarketRefreshBtn.Click += delegate { TrRefreshMarkets(); };

            _trRefreshTimer = new Timer();
            _trRefreshTimer.Interval = 2000;
            _trRefreshTimer.Tick += delegate { TrUpdateStatusDisplay(); };
            _trRefreshTimer.Start();
        }

        private void TrBuildRoutesTabLayout()
        {
            // Remove default controls and rebuild with proper layout
            _trRoutesTab.Controls.Clear();

            // Top button bar
            Panel btnBar = new Panel();
            btnBar.Dock = DockStyle.Top;
            btnBar.Height = 32;
            btnBar.BackColor = Color.FromArgb(36, 38, 48);

            _trAddRouteBtn.Location = new Point(8, 4);
            btnBar.Controls.Add(_trAddRouteBtn);
            _trAddRouteBtn.Click += delegate { TrAddRouteClick(); };

            _trDeleteRouteBtn.Location = new Point(118, 4);
            btnBar.Controls.Add(_trDeleteRouteBtn);
            _trDeleteRouteBtn.Click += delegate { TrDeleteRouteClick(); };

            _trRefreshRoutesBtn.Location = new Point(238, 4);
            btnBar.Controls.Add(_trRefreshRoutesBtn);
            _trRefreshRoutesBtn.Click += delegate { TrBuildRoutesList(); };

            Button editBtn = new Button();
            editBtn.Text = "Edit Route";
            editBtn.BackColor = Color.FromArgb(50, 100, 180);
            editBtn.ForeColor = Color.White;
            editBtn.FlatStyle = FlatStyle.Flat;
            editBtn.FlatAppearance.BorderSize = 0;
            editBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            editBtn.Size = new Size(100, 24);
            editBtn.Location = new Point(348, 4);
            editBtn.Cursor = Cursors.Hand;
            editBtn.Click += delegate { TrEditRouteClick(); };
            btnBar.Controls.Add(editBtn);

            // Column header
            Panel routeColHdr = new Panel();
            routeColHdr.Dock = DockStyle.Top;
            routeColHdr.Height = 22;
            routeColHdr.BackColor = Color.FromArgb(36, 38, 50);
            string[] routeCols = new string[] { "", "Name", "From", "To", "Resources", "Keep Min", "Merch", "Send Max", "Dist" };
            int[] routeColX = new int[] { 6, 28, 164, 254, 344, 510, 566, 612, 674 };
            for (int i = 0; i < routeCols.Length; i++)
            {
                Label cl = new Label();
                cl.Text = routeCols[i];
                cl.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
                cl.ForeColor = Color.FromArgb(160, 165, 180);
                cl.AutoSize = true;
                cl.Location = new Point(routeColX[i], 4);
                routeColHdr.Controls.Add(cl);
            }

            // Routes list panel fills the rest
            _trRoutesListPanel.Dock = DockStyle.Fill;

            _trRoutesTab.Controls.Add(_trRoutesListPanel);
            _trRoutesTab.Controls.Add(routeColHdr);
            _trRoutesTab.Controls.Add(btnBar);
        }

        private void TrBuildMarketsTabLayout()
        {
            // Remove the default panels from the markets tab and rebuild
            _trMarketsTab.Controls.Clear();

            // Top bar: refresh + add markets + distance
            Panel topBar = new Panel();
            topBar.Dock = DockStyle.Top;
            topBar.Height = 34;
            topBar.BackColor = Color.FromArgb(36, 38, 48);
            topBar.Controls.Add(_trMarketRefreshBtn);
            _trMarketRefreshBtn.Location = new Point(8, 5);
            topBar.Controls.Add(_trMarketDistanceLabel);
            _trMarketDistanceLabel.Location = new Point(130, 9);
            topBar.Controls.Add(_trMarketDistanceInput);
            _trMarketDistanceInput.Location = new Point(230, 6);
            topBar.Controls.Add(_trAddMarketsBtn);
            _trAddMarketsBtn.Location = new Point(300, 5);

            // "Should this village trade?" checkbox + market count
            _trVillageTradingCheck = new CheckBox();
            _trVillageTradingCheck.Text = "Should this village trade?";
            _trVillageTradingCheck.FlatStyle = FlatStyle.Flat;
            _trVillageTradingCheck.Font = new Font("Segoe UI", 8.5f);
            _trVillageTradingCheck.ForeColor = Color.FromArgb(230, 230, 240);
            _trVillageTradingCheck.AutoSize = true;
            _trVillageTradingCheck.Location = new Point(430, 9);
            topBar.Controls.Add(_trVillageTradingCheck);

            _trMarketCountLabel = new Label();
            _trMarketCountLabel.Text = "Total Markets: 0";
            _trMarketCountLabel.Font = new Font("Segoe UI", 8f);
            _trMarketCountLabel.ForeColor = Color.FromArgb(160, 165, 180);
            _trMarketCountLabel.AutoSize = true;
            _trMarketCountLabel.Location = new Point(640, 10);
            topBar.Controls.Add(_trMarketCountLabel);

            Button copyBtn = new Button();
            copyBtn.Text = "Copy Settings";
            copyBtn.BackColor = Color.FromArgb(60, 80, 140);
            copyBtn.ForeColor = Color.White;
            copyBtn.FlatStyle = FlatStyle.Flat;
            copyBtn.FlatAppearance.BorderSize = 0;
            copyBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            copyBtn.Size = new Size(100, 24);
            copyBtn.Location = new Point(760, 5);
            copyBtn.Cursor = Cursors.Hand;
            copyBtn.Click += delegate { TrCopySettingsClick(); };
            topBar.Controls.Add(copyBtn);

            // Left panel: village list
            Panel leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Width = 180;
            leftPanel.BackColor = Color.FromArgb(28, 30, 38);
            leftPanel.Padding = new Padding(4, 4, 4, 4);

            _trVillageListBox = new ListBox();
            _trVillageListBox.Dock = DockStyle.Fill;
            _trVillageListBox.BackColor = Color.FromArgb(36, 38, 48);
            _trVillageListBox.ForeColor = Color.FromArgb(230, 230, 240);
            _trVillageListBox.Font = new Font("Segoe UI", 8f);
            _trVillageListBox.BorderStyle = BorderStyle.FixedSingle;
            _trVillageListBox.SelectedIndexChanged += delegate { TrOnVillageSelected(); };
            leftPanel.Controls.Add(_trVillageListBox);

            // Right panel: markets IDs list
            Panel rightPanel = new Panel();
            rightPanel.Dock = DockStyle.Right;
            rightPanel.Width = 110;
            rightPanel.BackColor = Color.FromArgb(28, 30, 38);
            rightPanel.Padding = new Padding(2, 0, 2, 2);

            Label marketsHdr = new Label();
            marketsHdr.Text = "Markets:";
            marketsHdr.Dock = DockStyle.Top;
            marketsHdr.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            marketsHdr.ForeColor = Color.FromArgb(160, 165, 180);
            marketsHdr.Height = 16;

            _trMarketsListBox = new ListBox();
            _trMarketsListBox.Dock = DockStyle.Fill;
            _trMarketsListBox.BackColor = Color.FromArgb(36, 38, 48);
            _trMarketsListBox.ForeColor = Color.FromArgb(230, 230, 240);
            _trMarketsListBox.Font = new Font("Segoe UI", 7f);
            _trMarketsListBox.BorderStyle = BorderStyle.FixedSingle;
            _trMarketsListBox.IntegralHeight = false;
            rightPanel.Controls.Add(_trMarketsListBox);
            rightPanel.Controls.Add(marketsHdr);

            // Center: resource grid
            _trResourceGrid = new TradeResourceGrid();
            _trResourceGrid.Dock = DockStyle.Fill;

            _trMarketsTab.Controls.Add(_trResourceGrid);
            _trMarketsTab.Controls.Add(rightPanel);
            _trMarketsTab.Controls.Add(leftPanel);
            _trMarketsTab.Controls.Add(topBar);
        }

        private void TrBuildPlayerRoutesTab()
        {
            _trPlayerRoutesTab.Controls.Clear();

            // Top button bar
            Panel btnBar = new Panel();
            btnBar.Dock = DockStyle.Top;
            btnBar.Height = 32;
            btnBar.BackColor = Color.FromArgb(36, 38, 48);

            Button addBtn = new Button();
            addBtn.Text = "Add Route";
            addBtn.BackColor = Color.FromArgb(80, 160, 255);
            addBtn.ForeColor = Color.White;
            addBtn.FlatStyle = FlatStyle.Flat;
            addBtn.FlatAppearance.BorderSize = 0;
            addBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            addBtn.Size = new Size(100, 24);
            addBtn.Location = new Point(8, 4);
            addBtn.Cursor = Cursors.Hand;
            addBtn.Click += delegate { TrAddPlayerRouteClick(); };
            btnBar.Controls.Add(addBtn);

            Button deleteBtn = new Button();
            deleteBtn.Text = "Delete Route";
            deleteBtn.BackColor = Color.FromArgb(200, 60, 60);
            deleteBtn.ForeColor = Color.White;
            deleteBtn.FlatStyle = FlatStyle.Flat;
            deleteBtn.FlatAppearance.BorderSize = 0;
            deleteBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            deleteBtn.Size = new Size(100, 24);
            deleteBtn.Location = new Point(118, 4);
            deleteBtn.Cursor = Cursors.Hand;
            deleteBtn.Click += delegate { TrDeletePlayerRouteClick(); };
            btnBar.Controls.Add(deleteBtn);

            Button refreshBtn = new Button();
            refreshBtn.Text = "Refresh Routes";
            refreshBtn.BackColor = Color.FromArgb(50, 100, 180);
            refreshBtn.ForeColor = Color.White;
            refreshBtn.FlatStyle = FlatStyle.Flat;
            refreshBtn.FlatAppearance.BorderSize = 0;
            refreshBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            refreshBtn.Size = new Size(110, 24);
            refreshBtn.Location = new Point(228, 4);
            refreshBtn.Cursor = Cursors.Hand;
            refreshBtn.Click += delegate { TrBuildPlayerRoutesList(); };
            btnBar.Controls.Add(refreshBtn);

            Button editBtn = new Button();
            editBtn.Text = "Edit Route";
            editBtn.BackColor = Color.FromArgb(50, 100, 180);
            editBtn.ForeColor = Color.White;
            editBtn.FlatStyle = FlatStyle.Flat;
            editBtn.FlatAppearance.BorderSize = 0;
            editBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            editBtn.Size = new Size(100, 24);
            editBtn.Location = new Point(348, 4);
            editBtn.Cursor = Cursors.Hand;
            editBtn.Click += delegate { TrEditPlayerRouteClick(); };
            btnBar.Controls.Add(editBtn);

            Button resetBtn = new Button();
            resetBtn.Text = "Reset Progress";
            resetBtn.BackColor = Color.FromArgb(180, 130, 50);
            resetBtn.ForeColor = Color.White;
            resetBtn.FlatStyle = FlatStyle.Flat;
            resetBtn.FlatAppearance.BorderSize = 0;
            resetBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            resetBtn.Size = new Size(110, 24);
            resetBtn.Location = new Point(458, 4);
            resetBtn.Cursor = Cursors.Hand;
            resetBtn.Click += delegate { TrResetPlayerRouteClick(); };
            btnBar.Controls.Add(resetBtn);

            // Column header
            Panel colHdr = new Panel();
            colHdr.Dock = DockStyle.Top;
            colHdr.Height = 22;
            colHdr.BackColor = Color.FromArgb(36, 38, 50);
            string[] cols = new string[] { "", "Name", "From", "Target ID", "Resources", "Progress", "Keep Min", "Max Merch" };
            int[] colX = new int[] { 6, 28, 164, 264, 344, 520, 660, 730 };
            for (int i = 0; i < cols.Length; i++)
            {
                Label cl = new Label();
                cl.Text = cols[i];
                cl.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
                cl.ForeColor = Color.FromArgb(160, 165, 180);
                cl.AutoSize = true;
                cl.Location = new Point(colX[i], 4);
                colHdr.Controls.Add(cl);
            }

            _trPlayerRoutesListPanel.Dock = DockStyle.Fill;

            _trPlayerRoutesTab.Controls.Add(_trPlayerRoutesListPanel);
            _trPlayerRoutesTab.Controls.Add(colHdr);
            _trPlayerRoutesTab.Controls.Add(btnBar);
        }

        private void TrBuildPlayerRoutesList()
        {
            _trPlayerRoutesListPanel.SuspendLayout();
            foreach (PlayerTradeRouteRow row in _trPlayerRouteRows)
            {
                _trPlayerRoutesListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _trPlayerRouteRows.Clear();

            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
            {
                _trPlayerRoutesListPanel.ResumeLayout();
                return;
            }

            TradeSettings s = BotEngine.Instance.Settings.Trade;
            for (int i = s.PlayerRoutes.Count - 1; i >= 0; i--)
            {
                PlayerTradeRouteRow row = new PlayerTradeRouteRow(s.PlayerRoutes[i], i % 2 != 0);
                row.Dock = DockStyle.Top;
                _trPlayerRoutesListPanel.Controls.Add(row);
                _trPlayerRouteRows.Add(row);
            }

            _trPlayerRoutesListPanel.ResumeLayout();
        }

        private void TrAddPlayerRouteClick()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            PlayerTradeRouteSettings route = new PlayerTradeRouteSettings();
            route.Name = "Player Route " + (BotEngine.Instance.Settings.Trade.PlayerRoutes.Count + 1);

            PlayerTradeRouteEditorForm form = new PlayerTradeRouteEditorForm(route, "Add Player Route");
            form.ShowDialog(this);

            if (form.Saved)
            {
                BotEngine.Instance.Settings.Trade.PlayerRoutes.Add(route);
                TrBuildPlayerRoutesList();
            }
        }

        private void TrEditPlayerRouteClick()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            PlayerTradeRouteRow selected = null;
            foreach (PlayerTradeRouteRow row in _trPlayerRouteRows)
            {
                if (row.IsSelected) { selected = row; break; }
            }
            if (selected == null) return;

            PlayerTradeRouteEditorForm form = new PlayerTradeRouteEditorForm(selected.Route, "Edit Player Route");
            form.ShowDialog(this);

            if (form.Saved)
                TrBuildPlayerRoutesList();
        }

        private void TrDeletePlayerRouteClick()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            PlayerTradeRouteRow selected = null;
            foreach (PlayerTradeRouteRow row in _trPlayerRouteRows)
            {
                if (row.IsSelected) { selected = row; break; }
            }
            if (selected == null) return;

            BotEngine.Instance.Settings.Trade.PlayerRoutes.Remove(selected.Route);
            TrBuildPlayerRoutesList();
        }

        private void TrResetPlayerRouteClick()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            PlayerTradeRouteRow selected = null;
            foreach (PlayerTradeRouteRow row in _trPlayerRouteRows)
            {
                if (row.IsSelected) { selected = row; break; }
            }
            if (selected == null) return;

            selected.Route.ResetProgress();
            selected.Route.Enabled = true;
            TrBuildPlayerRoutesList();
            BotLogger.Log("Trade", BotLogLevel.Info,
                "Player route '" + selected.Route.Name + "' progress reset and re-enabled.");
        }

        private void TrLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            TradeSettings s = BotEngine.Instance.Settings.Trade;
            _trEnabledCheck.Checked = s.Enabled;
            _trIntervalInput.Value = Math.Max(_trIntervalInput.Minimum,
                Math.Min(_trIntervalInput.Maximum, s.CycleIntervalSeconds));
            _trDelayInput.Value = Math.Max(_trDelayInput.Minimum,
                Math.Min(_trDelayInput.Maximum, s.DelayBetweenVillagesMs));
            _trMerchantsPerTradeInput.Value = Math.Max(_trMerchantsPerTradeInput.Minimum,
                Math.Min(_trMerchantsPerTradeInput.Maximum, s.MerchantsPerTrade));
            _trTradeLimitInput.Value = Math.Max(_trTradeLimitInput.Minimum,
                Math.Min(_trTradeLimitInput.Maximum, s.MerchantsTradeLimit));
            _trExchangeLimitInput.Value = Math.Max(_trExchangeLimitInput.Minimum,
                Math.Min(_trExchangeLimitInput.Maximum, s.MerchantsExchangeLimit));
            _trAutoHireCheck.Checked = s.AutoHireMerchants;
            _trAutoHireLimitInput.Value = Math.Max(_trAutoHireLimitInput.Minimum,
                Math.Min(_trAutoHireLimitInput.Maximum, s.AutoHireMerchantsLimit));
            _trIgnoreTransactionsCheck.Checked = s.IgnoreCurrentTransactions;
            _trPrioritiseMarketsCheck.Checked = s.PrioritiseMarkets;

            TrRefreshMarkets();
            TrBuildRoutesList();
            TrBuildPlayerRoutesList();
        }

        private void TrWriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            TradeSettings s = BotEngine.Instance.Settings.Trade;
            s.Enabled = _trEnabledCheck.Checked;
            s.CycleIntervalSeconds = (int)_trIntervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_trDelayInput.Value;
            s.MerchantsPerTrade = (int)_trMerchantsPerTradeInput.Value;
            s.MerchantsTradeLimit = (int)_trTradeLimitInput.Value;
            s.MerchantsExchangeLimit = (int)_trExchangeLimitInput.Value;
            s.AutoHireMerchants = _trAutoHireCheck.Checked;
            s.AutoHireMerchantsLimit = (int)_trAutoHireLimitInput.Value;
            s.IgnoreCurrentTransactions = _trIgnoreTransactionsCheck.Checked;
            s.PrioritiseMarkets = _trPrioritiseMarketsCheck.Checked;

            // Save currently displayed village's resource grid
            TrSaveCurrentVillage();

            foreach (TradeRouteRow row in _trRouteRows)
                row.WriteToSettings(s);

            foreach (PlayerTradeRouteRow row in _trPlayerRouteRows)
                row.WriteToSettings(s);

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is TradeModule)
                    m.Enabled = s.Enabled;
            }
        }

        private void TrRefreshMarkets()
        {
            _trVillageListBox.Items.Clear();
            _trSelectedVillageId = -1;

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<int> ids = GameEngine.Instance.World.getUserVillageIDList();
            if (ids == null) return;

            foreach (int id in ids)
            {
                string name = TrGetVillageName(id);
                _trVillageListBox.Items.Add(new VillageItem(id, "[" + id + "] " + name));
            }

            if (_trVillageListBox.Items.Count > 0)
                _trVillageListBox.SelectedIndex = 0;
        }

        private void TrOnVillageSelected()
        {
            // Save previous village first
            TrSaveCurrentVillage();

            VillageItem item = _trVillageListBox.SelectedItem as VillageItem;
            if (item == null)
            {
                _trSelectedVillageId = -1;
                return;
            }

            _trSelectedVillageId = item.VillageId;

            TradeSettings settings = null;
            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                settings = BotEngine.Instance.Settings.Trade;
            if (settings == null) return;

            VillageMarketTradeInfo info = settings.GetVillageMarketInfo(_trSelectedVillageId);
            _trVillageTradingCheck.Checked = info.IsTrading;
            _trResourceGrid.LoadVillage(info);

            // Populate markets list
            _trMarketsListBox.Items.Clear();
            foreach (int marketId in info.MarketTargets)
                _trMarketsListBox.Items.Add(marketId);
            _trMarketCountLabel.Text = "Total Markets: " + info.MarketTargets.Count;
        }

        private void TrSaveCurrentVillage()
        {
            if (_trSelectedVillageId == -1) return;
            TradeSettings settings = null;
            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                settings = BotEngine.Instance.Settings.Trade;
            if (settings == null) return;

            VillageMarketTradeInfo info = settings.GetVillageMarketInfo(_trSelectedVillageId);
            info.IsTrading = _trVillageTradingCheck.Checked;
            _trResourceGrid.WriteToInfo(info);
        }

        private void TrAddMarketsClick()
        {
            TrSaveCurrentVillage();

            TradeModule module = null;
            if (BotEngine.Instance != null)
            {
                foreach (IBotModule m in BotEngine.Instance.Modules)
                {
                    module = m as TradeModule;
                    if (module != null) break;
                }
            }

            if (module != null)
            {
                double distance = (double)_trMarketDistanceInput.Value;
                module.AddMarketsForAllVillages(distance);
                BotLogger.Log("Trade", BotLogLevel.Info, "Markets found within distance " + distance);
            }

            // Re-select current village to refresh markets list
            TrOnVillageSelected();
        }

        private void TrCopySettingsClick()
        {
            // Save current village's grid first so the source data is up to date
            TrSaveCurrentVillage();

            CopyMarketSettingsForm form = new CopyMarketSettingsForm();
            form.ShowDialog(this);

            if (form.Copied)
            {
                // Refresh the current village view to reflect any changes
                TrOnVillageSelected();
            }
        }

        private void TrBuildRoutesList()
        {
            _trRoutesListPanel.SuspendLayout();
            foreach (TradeRouteRow row in _trRouteRows)
            {
                _trRoutesListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _trRouteRows.Clear();
            _trSelectedRouteIndex = -1;

            TradeSettings settings = null;
            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                settings = BotEngine.Instance.Settings.Trade;

            if (settings == null || settings.Routes.Count == 0)
            {
                _trRoutesListPanel.ResumeLayout();
                return;
            }

            bool alternate = false;
            for (int i = settings.Routes.Count - 1; i >= 0; i--)
            {
                TradeRouteSettings route = settings.Routes[i];
                TradeRouteRow row = new TradeRouteRow(i, route, alternate);
                row.Dock = DockStyle.Top;
                int idx = i;
                row.Click += delegate { TrSelectRoute(idx); };
                // Also handle clicks on child labels
                foreach (Control c in row.Controls)
                {
                    if (c is Label)
                    {
                        int capturedIdx = idx;
                        c.Click += delegate { TrSelectRoute(capturedIdx); };
                    }
                }
                _trRoutesListPanel.Controls.Add(row);
                _trRouteRows.Add(row);
                alternate = !alternate;
            }
            _trRoutesListPanel.ResumeLayout();
        }

        private void TrSelectRoute(int routeIndex)
        {
            _trSelectedRouteIndex = routeIndex;
            foreach (TradeRouteRow row in _trRouteRows)
                row.Selected = (row.RouteIndex == routeIndex);
        }

        private void TrAddRouteClick()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            TradeRouteSettings newRoute = new TradeRouteSettings();
            newRoute.Name = "Route " + (BotEngine.Instance.Settings.Trade.Routes.Count + 1);
            newRoute.Enabled = true;

            TradeRouteEditorForm editor = new TradeRouteEditorForm(newRoute, "New Route");
            editor.ShowDialog(this);

            if (editor.Saved)
            {
                BotEngine.Instance.Settings.Trade.Routes.Add(newRoute);
                TrBuildRoutesList();
                BotLogger.Log("Trade", BotLogLevel.Info, "New trade route added: " + newRoute.Name);
            }
        }

        private void TrEditRouteClick()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            TradeSettings s = BotEngine.Instance.Settings.Trade;
            if (_trSelectedRouteIndex < 0 || _trSelectedRouteIndex >= s.Routes.Count)
            {
                BotLogger.Log("Trade", BotLogLevel.Warning, "Select a route to edit.");
                return;
            }

            TradeRouteSettings route = s.Routes[_trSelectedRouteIndex];
            TradeRouteEditorForm editor = new TradeRouteEditorForm(route, "Edit Route - " + route.Name);
            editor.ShowDialog(this);

            if (editor.Saved)
            {
                TrBuildRoutesList();
                BotLogger.Log("Trade", BotLogLevel.Info, "Route updated: " + route.Name);
            }
        }

        private void TrDeleteRouteClick()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            TradeSettings s = BotEngine.Instance.Settings.Trade;
            if (_trSelectedRouteIndex < 0 || _trSelectedRouteIndex >= s.Routes.Count)
            {
                BotLogger.Log("Trade", BotLogLevel.Warning, "Select a route to delete.");
                return;
            }

            string name = s.Routes[_trSelectedRouteIndex].Name;
            s.Routes.RemoveAt(_trSelectedRouteIndex);
            _trSelectedRouteIndex = -1;
            TrBuildRoutesList();
            BotLogger.Log("Trade", BotLogLevel.Info, "Deleted trade route: " + name);
        }

        private void TrUpdateStatusDisplay()
        {
            if (_trEnabledCheck == null) return;
            bool enabled = _trEnabledCheck.Checked;
            _trStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _trStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
        }

        private static string TrGetVillageName(int villageId)
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                return GameEngine.Instance.World.getVillageName(villageId);
            return "Village " + villageId;
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
