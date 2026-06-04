using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CommonTypes;
using Kingdoms.Bot;
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
        private bool _rdLoading;

        // Group Radar runtime state (controls created programmatically)
        private List<GroupActionRow> _grpActionRows = new List<GroupActionRow>();
        private bool _grpLoading;
        private int _grpSelectedMemberIndex = -1;
        private CheckBox _grpEnabledCheck;
        private TextBox _grpWebhookInput;
        private TextBox _grpMentionTagInput;
        private Button _grpTestWebhookBtn;
        private TextBox _grpPlayerNameInput;
        private Button _grpAddMemberBtn;
        private Button _grpRefreshAllBtn;
        private ListBox _grpMemberList;
        private Button _grpRemoveMemberBtn;
        private Label _grpMemberNameLabel;
        private TextBox _grpMemberTagInput;
        private Label _grpMemberTagLabel;
        private Label _grpMemberVillagesLabel;
        private Button _grpRefreshVillagesBtn;
        private Panel _grpActionListPanel;
        private Panel _grpColHeader;


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

        // Village Builder tab
        private List<BuildingListRow> _bldBuildingRows = new List<BuildingListRow>();
        private int _bldSelectedVillageId = -1;
        private Timer _bldRefreshTimer;

        // Auto Bomb tab
        private List<BombArmyRow> _abArmyRows = new List<BombArmyRow>();
        private List<PendingBombRow> _abPendingRows = new List<PendingBombRow>();
        private Timer _abRefreshTimer;

        // Auto Bomb Multi tab
        private List<MultiBombVillageRow> _abmVillageRows = new List<MultiBombVillageRow>();
        private List<MultiPendingRow> _abmPendingRows = new List<MultiPendingRow>();
        private Timer _abmRefreshTimer;
        private bool _abmLastIsCoordinator = false;

        // ── DPI helper ────────────────────────────────────────────────────────
        // Scales a design-time pixel value (authored at 96 DPI) to the current
        // device DPI.  Use for any control sizes/positions set in code rather
        // than the designer (the designer controls are auto-scaled by WinForms).
        private int Dp(int designPx)
        {
            using (var g = CreateGraphics())
                return (int)Math.Round(designPx * g.DpiX / 96f);
        }

        // Per-tab loading guards — prevent settings writes while controls are being populated from settings
        private bool _vsLoading;
        private bool _rcLoading;
        private bool _crLoading;
        private bool _trLoading;
        private bool _bldLoading;
        private bool _ppLoading;
        private bool _bqLoading;
        private bool _miscLoading;
        private bool _mkLoading;

        // Misc tab — no runtime state needed (settings only)

        // Scout tab runtime state
        private int _scSelectedVillageId = -1;
        private bool _scLoading;
        private bool _scDragging;
        private object _scDragItem;
        private int _scDragFromIndex = -1;
        private Timer _scRefreshTimer;

        // Popularity tab runtime state
        private Timer _ppRefreshTimer;

        // Banquet tab runtime state
        private List<BanquetVillageRow> _bqVillageRows = new List<BanquetVillageRow>();

        // Monk tab runtime state
        private List<MonkRouteRow> _mkRouteRows = new List<MonkRouteRow>();
        private int _mkSelectedRouteIndex = -1;
        private Timer _mkSyncTimer;

        // Auto tab runtime state. The control fields (_autoProd*/_autoModule* panels, interval
        // inputs and server-time label) are declared in BotControlForm.Designer.cs.
        private Timer _autoRefreshTimer;
        private List<AutoProdRow> _autoProdRows = new List<AutoProdRow>();
        private List<AutoModuleRow> _autoModuleRows = new List<AutoModuleRow>();
        private bool _autoLoading;
        private List<PopularityVillageRow> _ppVillageRows = new List<PopularityVillageRow>();

        private int _trSelectedRouteIndex = -1;

        // Vassals tab runtime state
        private List<VassalVillagePanel> _vaVassalPanels = new List<VassalVillagePanel>();

        public static void ShowInstance()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new BotControlForm();

                // Clamp the initial size to the available working area on whatever
                // screen the form will appear on (CenterScreen positioning is set in
                // the designer, so we target the primary screen's working area).
                Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
                int w = Math.Min(_instance.Width,  workingArea.Width);
                int h = Math.Min(_instance.Height, workingArea.Height);
                if (w != _instance.Width || h != _instance.Height)
                    _instance.Size = new System.Drawing.Size(w, h);
            }

            if (!_instance.Visible)
                _instance.Show();

            _instance.BringToFront();
        }

        /// <summary>
        /// Closes the current instance (if any) and returns whether it was visible,
        /// so callers can reopen it after a world reinit.
        /// </summary>
        public static bool CloseInstance()
        {
            bool wasVisible = _instance != null && !_instance.IsDisposed && _instance.Visible;
            if (_instance != null && !_instance.IsDisposed)
            {
                _instance.Close();
                _instance = null;
            }
            return wasVisible;
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

            string pv = Application.ProductVersion ?? "0.0.0";
            if (pv.StartsWith("dev-", StringComparison.OrdinalIgnoreCase))
                _versionLabel.Text = pv; // e.g. "dev-abc1234"
            else
            {
                try { var v = new Version(pv); _versionLabel.Text = $"v{v.Major}.{v.Minor}.{v.Build}"; }
                catch { _versionLabel.Text = "v" + pv; }
            }

            if (!IsDesignTime)
            {
                WireUpVillageSyncTab();
                WireUpRadarTab();
                WireUpRecruitingTab();
                WireUpCastleRepairTab();
                WireUpVassalsTab();
                WireUpTradeTab();
                WireUpBuilderTab();
                WireUpAutoBombTab();
                WireUpTargetQueueTab();
                WireUpAutoBombMultiTab();
                WireUpMiscTab();
                WireUpPopularityTab();
                WireUpBanquetTab();
                WireUpAutoTab();
                WireUpScoutTab();
                WireUpDefenderTab();
                WireUpMonkTab();
                SubscribeToLog();
                RefreshStatus();
                ReplayExistingLogs();

                VsLoadFromSettings();
                RdLoadFromSettings();
                RdBuildActionRows();
                GrpLoadFromSettings();
                GrpBuildActionRows();
                RcLoadFromSettings();
                CrLoadFromSettings();
                VaLoadFromSettings();
                TrLoadFromSettings();
                BldLoadFromSettings();
                AbLoadFromSettings();
                AbmLoadFromSettings();
                MiscLoadFromSettings();
                PpLoadFromSettings();
                BqLoadFromSettings();
                AutoLoadFromSettings();
                ScLoadFromSettings();
                DfLoadFromSettings();
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
            _vsRefreshTimer.Tick += delegate { try { VsUpdateStatusDisplay(); } catch { } };
            _vsRefreshTimer.Start();
        }

        private void VsPushToSettings()
        {
            if (_vsLoading) return;
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
            _vsLoading = true;
            try
            {
                VillageSyncSettings s = BotEngine.Instance.Settings.VillageSync;
                _vsEnabledCheck.Checked = s.Enabled;
                _vsIntervalInput.Value = Math.Max(_vsIntervalInput.Minimum,
                    Math.Min(_vsIntervalInput.Maximum, s.IntervalSeconds));
                _vsDelayInput.Value = Math.Max(_vsDelayInput.Minimum,
                    Math.Min(_vsDelayInput.Maximum, s.DelayBetweenVillagesMs));

                VsPopulateVillageList();
            }
            finally { _vsLoading = false; }
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
                string mention = _rdMentionTagInput.Text.Trim();
                DiscordNotifier.SendAsync(url, "\u2705 Webhook Test",
                    "Project Hades Radar is connected!", 5025616,
                    string.IsNullOrEmpty(mention) ? null : mention);
                BotLogger.Log("Radar", BotLogLevel.Info, "Test webhook sent.");
            };

            _rdEnabledCheck.CheckedChanged += delegate { RdPushToSettings(); };
            _rdScanIntervalInput.ValueChanged += delegate { RdPushToSettings(); };
            _rdWebhookInput.TextChanged += delegate { RdPushToSettings(); };
            _rdMentionTagInput.TextChanged += delegate { RdPushToSettings(); };
            _rdInterdictMonkCountInput.ValueChanged += delegate { RdPushToSettings(); };
            _rdAutoRecruitMonksCheck.CheckedChanged += delegate { RdPushToSettings(); };
            _rdMinArmySizeInput.ValueChanged += delegate { RdPushToSettings(); };
            _rdMinAttacksInput.ValueChanged += delegate { RdPushToSettings(); };
            _rdMinAttacksWindowInput.ValueChanged += delegate { RdPushToSettings(); };
            _rdMaxLandTimeInput.ValueChanged += delegate { RdPushToSettings(); };
            _rdForceRefreshCheck.CheckedChanged += delegate { RdPushToSettings(); };

            _rdRefreshTimer = new Timer();
            _rdRefreshTimer.Interval = 2000;
            _rdRefreshTimer.Tick += delegate { try { RdUpdateStatusDisplay(); } catch { } };
            _rdRefreshTimer.Start();

            BuildInnerRadarTabs();
        }

        private void RdPushToSettings()
        {
            if (_rdLoading) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            RadarSettings s = BotEngine.Instance.Settings.Radar;
            s.Enabled = _rdEnabledCheck.Checked;
            s.ScanIntervalSeconds = (int)_rdScanIntervalInput.Value;
            s.DiscordWebhookUrl = _rdWebhookInput.Text.Trim();
            s.DiscordMentionTag = _rdMentionTagInput.Text.Trim();
            s.AutoInterdictMonkCount = (int)_rdInterdictMonkCountInput.Value;
            s.AutoRecruitMonks = _rdAutoRecruitMonksCheck.Checked;
            s.MinArmySizeForInterdict = (int)_rdMinArmySizeInput.Value;
            s.MinAttacksForInterdict = (int)_rdMinAttacksInput.Value;
            s.MinAttacksWindowSeconds = (int)_rdMinAttacksWindowInput.Value;
            s.MaxLandTimeHours = (int)_rdMaxLandTimeInput.Value;
            s.ForceRefreshArmies = _rdForceRefreshCheck.Checked;

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

            _rdLoading = true;
            try
            {
                RadarSettings s = BotEngine.Instance.Settings.Radar;
                _rdEnabledCheck.Checked = s.Enabled;
                _rdScanIntervalInput.Value = Math.Max(_rdScanIntervalInput.Minimum,
                    Math.Min(_rdScanIntervalInput.Maximum, s.ScanIntervalSeconds));
                _rdWebhookInput.Text = s.DiscordWebhookUrl ?? "";
                _rdMentionTagInput.Text = s.DiscordMentionTag ?? "";
                _rdInterdictMonkCountInput.Value = Math.Max(_rdInterdictMonkCountInput.Minimum,
                    Math.Min(_rdInterdictMonkCountInput.Maximum, s.AutoInterdictMonkCount));
                _rdAutoRecruitMonksCheck.Checked = s.AutoRecruitMonks;
                _rdMinArmySizeInput.Value = Math.Max(_rdMinArmySizeInput.Minimum,
                    Math.Min(_rdMinArmySizeInput.Maximum, s.MinArmySizeForInterdict));
                _rdMinAttacksInput.Value = Math.Max(_rdMinAttacksInput.Minimum,
                    Math.Min(_rdMinAttacksInput.Maximum, s.MinAttacksForInterdict));
                _rdMinAttacksWindowInput.Value = Math.Max(_rdMinAttacksWindowInput.Minimum,
                    Math.Min(_rdMinAttacksWindowInput.Maximum, s.MinAttacksWindowSeconds));
                _rdMaxLandTimeInput.Value = Math.Max(_rdMaxLandTimeInput.Minimum,
                    Math.Min(_rdMaxLandTimeInput.Maximum, s.MaxLandTimeHours));
                _rdForceRefreshCheck.Checked = s.ForceRefreshArmies;

                foreach (ActionRow row in _rdActionRows)
                {
                    RadarActionSettings actionSettings = s.GetActionSettings(row.ActionKey);
                    row.SetValues(actionSettings);
                }
            }
            finally
            {
                _rdLoading = false;
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
            s.DiscordMentionTag = _rdMentionTagInput.Text.Trim();
            s.AutoInterdictMonkCount = (int)_rdInterdictMonkCountInput.Value;
            s.AutoRecruitMonks = _rdAutoRecruitMonksCheck.Checked;
            s.MinArmySizeForInterdict = (int)_rdMinArmySizeInput.Value;
            s.MinAttacksForInterdict = (int)_rdMinAttacksInput.Value;
            s.MinAttacksWindowSeconds = (int)_rdMinAttacksWindowInput.Value;
            s.MaxLandTimeHours = (int)_rdMaxLandTimeInput.Value;
            s.ForceRefreshArmies = _rdForceRefreshCheck.Checked;

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

        // =====================================================================
        // Radar – inner tab (Radar / Group sub-tabs)
        // =====================================================================

        private void BuildInnerRadarTabs()
        {
            // Create inner TabControl that fills the radar page
            TabControl innerTab = new TabControl();
            innerTab.Dock = DockStyle.Fill;
            innerTab.Font = new Font("Segoe UI", 9f);

            // ---- Radar sub-tab (house existing controls) ----
            TabPage radarSub = new TabPage("Radar");
            radarSub.BackColor = _radarPage.BackColor;

            // Reparent existing controls into the Radar sub-tab
            // (assigning to a new Controls collection removes from old parent automatically)
            radarSub.Controls.Add(_rdActionListPanel);
            radarSub.Controls.Add(_rdColHeader);
            radarSub.Controls.Add(_rdSeparator);
            radarSub.Controls.Add(_rdSettingsPanel);

            // ---- Group sub-tab ----
            TabPage groupPage = new TabPage("Group");
            groupPage.BackColor = _radarPage.BackColor;
            BuildGroupTabContent(groupPage);

            innerTab.TabPages.Add(radarSub);
            innerTab.TabPages.Add(groupPage);

            _radarPage.Controls.Clear();
            _radarPage.Controls.Add(innerTab);
        }

        private void BuildGroupTabContent(TabPage page)
        {
            Color bgDark   = Color.FromArgb(24, 24, 32);
            Color bgMed    = Color.FromArgb(40, 42, 54);
            Color bgLight  = Color.FromArgb(50, 52, 64);
            Color textPri  = Color.FromArgb(230, 230, 240);
            Color textSec  = Color.FromArgb(160, 165, 180);
            Font  fNorm    = new Font("Segoe UI", 9f);
            Font  fSmall   = new Font("Segoe UI", 8.5f);
            Font  fBold    = new Font("Segoe UI", 9f, FontStyle.Bold);

            // ---- Settings panel (top) ----
            Panel settingsPanel = new Panel();
            settingsPanel.Dock = DockStyle.Top;
            settingsPanel.BackColor = bgMed;
            settingsPanel.Padding = new Padding(16, 10, 16, 8);
            settingsPanel.Height = 80;

            _grpEnabledCheck = new CheckBox();
            _grpEnabledCheck.Text = "Enable Group Radar";
            _grpEnabledCheck.Font = fBold;
            _grpEnabledCheck.ForeColor = textPri;
            _grpEnabledCheck.Location = new Point(16, 12);
            _grpEnabledCheck.AutoSize = true;

            Label webhookLabel = new Label();
            webhookLabel.Text = "Group Webhook URL:";
            webhookLabel.Font = fSmall;
            webhookLabel.ForeColor = textSec;
            webhookLabel.Location = new Point(16, 44);
            webhookLabel.AutoSize = true;

            _grpWebhookInput = new TextBox();
            _grpWebhookInput.Location = new Point(150, 41);
            _grpWebhookInput.Size = new Size(460, 23);
            _grpWebhookInput.BackColor = bgLight;
            _grpWebhookInput.ForeColor = textPri;
            _grpWebhookInput.BorderStyle = BorderStyle.FixedSingle;
            _grpWebhookInput.Font = fNorm;

            _grpTestWebhookBtn = new Button();
            _grpTestWebhookBtn.Text = "Test";
            _grpTestWebhookBtn.Location = new Point(618, 40);
            _grpTestWebhookBtn.Size = new Size(60, 24);
            _grpTestWebhookBtn.BackColor = Color.FromArgb(60, 63, 80);
            _grpTestWebhookBtn.ForeColor = textPri;
            _grpTestWebhookBtn.FlatStyle = FlatStyle.Flat;
            _grpTestWebhookBtn.Font = fSmall;

            Label mentionLabel = new Label();
            mentionLabel.Text = "Default Mention:";
            mentionLabel.Font = fSmall;
            mentionLabel.ForeColor = textSec;
            mentionLabel.Location = new Point(690, 44);
            mentionLabel.AutoSize = true;

            _grpMentionTagInput = new TextBox();
            _grpMentionTagInput.Location = new Point(800, 41);
            _grpMentionTagInput.Size = new Size(180, 23);
            _grpMentionTagInput.BackColor = bgLight;
            _grpMentionTagInput.ForeColor = textPri;
            _grpMentionTagInput.BorderStyle = BorderStyle.FixedSingle;
            _grpMentionTagInput.Font = fNorm;

            settingsPanel.Controls.AddRange(new Control[] {
                _grpEnabledCheck, webhookLabel, _grpWebhookInput,
                _grpTestWebhookBtn, mentionLabel, _grpMentionTagInput
            });

            // ---- Separator ----
            Panel sep = new Panel();
            sep.Dock = DockStyle.Top;
            sep.BackColor = Color.FromArgb(55, 58, 72);
            sep.Height = 1;

            // ---- Left member panel ----
            Panel memberPanel = new Panel();
            memberPanel.Dock = DockStyle.Left;
            memberPanel.Width = 290;
            memberPanel.BackColor = Color.FromArgb(30, 32, 42);
            memberPanel.Padding = new Padding(10, 8, 10, 8);

            Label addLabel = new Label();
            addLabel.Text = "Add Player:";
            addLabel.Font = fSmall;
            addLabel.ForeColor = textSec;
            addLabel.Location = new Point(10, 10);
            addLabel.AutoSize = true;

            _grpPlayerNameInput = new TextBox();
            _grpPlayerNameInput.Location = new Point(10, 28);
            _grpPlayerNameInput.Size = new Size(160, 23);
            _grpPlayerNameInput.BackColor = bgLight;
            _grpPlayerNameInput.ForeColor = textPri;
            _grpPlayerNameInput.BorderStyle = BorderStyle.FixedSingle;
            _grpPlayerNameInput.Font = fNorm;

            _grpAddMemberBtn = new Button();
            _grpAddMemberBtn.Text = "Add";
            _grpAddMemberBtn.Location = new Point(175, 27);
            _grpAddMemberBtn.Size = new Size(50, 25);
            _grpAddMemberBtn.BackColor = Color.FromArgb(60, 63, 80);
            _grpAddMemberBtn.ForeColor = textPri;
            _grpAddMemberBtn.FlatStyle = FlatStyle.Flat;
            _grpAddMemberBtn.Font = fSmall;

            _grpRefreshAllBtn = new Button();
            _grpRefreshAllBtn.Text = "Refresh All";
            _grpRefreshAllBtn.Location = new Point(230, 27);
            _grpRefreshAllBtn.Size = new Size(50, 25);
            _grpRefreshAllBtn.BackColor = Color.FromArgb(60, 63, 80);
            _grpRefreshAllBtn.ForeColor = textPri;
            _grpRefreshAllBtn.FlatStyle = FlatStyle.Flat;
            _grpRefreshAllBtn.Font = new Font("Segoe UI", 7.5f);

            Label membersLabel = new Label();
            membersLabel.Text = "Members:";
            membersLabel.Font = fSmall;
            membersLabel.ForeColor = textSec;
            membersLabel.Location = new Point(10, 60);
            membersLabel.AutoSize = true;

            _grpMemberList = new ListBox();
            _grpMemberList.Location = new Point(10, 78);
            _grpMemberList.Size = new Size(270, 160);
            _grpMemberList.BackColor = bgLight;
            _grpMemberList.ForeColor = textPri;
            _grpMemberList.BorderStyle = BorderStyle.FixedSingle;
            _grpMemberList.Font = fNorm;

            _grpRemoveMemberBtn = new Button();
            _grpRemoveMemberBtn.Text = "Remove Selected";
            _grpRemoveMemberBtn.Location = new Point(10, 245);
            _grpRemoveMemberBtn.Size = new Size(130, 25);
            _grpRemoveMemberBtn.BackColor = Color.FromArgb(80, 40, 40);
            _grpRemoveMemberBtn.ForeColor = textPri;
            _grpRemoveMemberBtn.FlatStyle = FlatStyle.Flat;
            _grpRemoveMemberBtn.Font = fSmall;

            // Selected member detail area
            _grpMemberNameLabel = new Label();
            _grpMemberNameLabel.Text = "No member selected";
            _grpMemberNameLabel.Font = fBold;
            _grpMemberNameLabel.ForeColor = textPri;
            _grpMemberNameLabel.Location = new Point(10, 280);
            _grpMemberNameLabel.Size = new Size(270, 20);

            _grpMemberTagLabel = new Label();
            _grpMemberTagLabel.Text = "Discord Tag:";
            _grpMemberTagLabel.Font = fSmall;
            _grpMemberTagLabel.ForeColor = textSec;
            _grpMemberTagLabel.Location = new Point(10, 308);
            _grpMemberTagLabel.AutoSize = true;

            _grpMemberTagInput = new TextBox();
            _grpMemberTagInput.Location = new Point(10, 325);
            _grpMemberTagInput.Size = new Size(200, 23);
            _grpMemberTagInput.BackColor = bgLight;
            _grpMemberTagInput.ForeColor = textPri;
            _grpMemberTagInput.BorderStyle = BorderStyle.FixedSingle;
            _grpMemberTagInput.Font = fNorm;

            _grpMemberVillagesLabel = new Label();
            _grpMemberVillagesLabel.Text = "Villages: none";
            _grpMemberVillagesLabel.Font = fSmall;
            _grpMemberVillagesLabel.ForeColor = textSec;
            _grpMemberVillagesLabel.Location = new Point(10, 358);
            _grpMemberVillagesLabel.Size = new Size(270, 35);

            _grpRefreshVillagesBtn = new Button();
            _grpRefreshVillagesBtn.Text = "Refresh Villages";
            _grpRefreshVillagesBtn.Location = new Point(10, 398);
            _grpRefreshVillagesBtn.Size = new Size(120, 25);
            _grpRefreshVillagesBtn.BackColor = Color.FromArgb(60, 63, 80);
            _grpRefreshVillagesBtn.ForeColor = textPri;
            _grpRefreshVillagesBtn.FlatStyle = FlatStyle.Flat;
            _grpRefreshVillagesBtn.Font = fSmall;

            memberPanel.Controls.AddRange(new Control[] {
                addLabel, _grpPlayerNameInput, _grpAddMemberBtn, _grpRefreshAllBtn,
                membersLabel, _grpMemberList, _grpRemoveMemberBtn,
                _grpMemberNameLabel, _grpMemberTagLabel, _grpMemberTagInput,
                _grpMemberVillagesLabel, _grpRefreshVillagesBtn
            });

            // ---- Column header for action rows ----
            _grpColHeader = new Panel();
            _grpColHeader.Dock = DockStyle.Top;
            _grpColHeader.BackColor = Color.FromArgb(30, 32, 40);
            _grpColHeader.Height = 24;

            Label colAction = new Label();
            colAction.Text = "Action Type";
            colAction.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            colAction.ForeColor = textSec;
            colAction.Location = new Point(16, 2);
            colAction.Size = new Size(180, 20);

            Label colMonitor = new Label();
            colMonitor.Text = "Monitor";
            colMonitor.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            colMonitor.ForeColor = textSec;
            colMonitor.Location = new Point(200, 2);
            colMonitor.Size = new Size(60, 20);

            Label colDiscord = new Label();
            colDiscord.Text = "Discord\r\nNotify";
            colDiscord.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            colDiscord.ForeColor = textSec;
            colDiscord.Location = new Point(270, 2);
            colDiscord.Size = new Size(60, 20);

            _grpColHeader.Controls.AddRange(new Control[] { colAction, colMonitor, colDiscord });

            // ---- Action list panel (fills remaining space) ----
            _grpActionListPanel = new Panel();
            _grpActionListPanel.Dock = DockStyle.Fill;
            _grpActionListPanel.BackColor = bgDark;
            _grpActionListPanel.AutoScroll = true;

            // ---- Right content area (fills to the right of member panel) ----
            Panel rightPanel = new Panel();
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.BackColor = bgDark;
            rightPanel.Controls.Add(_grpActionListPanel);
            rightPanel.Controls.Add(_grpColHeader);

            // Assemble: member panel on left, right panel fills rest
            page.Controls.Add(rightPanel);
            page.Controls.Add(memberPanel);
            page.Controls.Add(sep);
            page.Controls.Add(settingsPanel);

            WireUpGroupTab();
        }

        private void WireUpGroupTab()
        {
            _grpEnabledCheck.CheckedChanged += delegate { GrpPushToSettings(); };
            _grpWebhookInput.TextChanged += delegate { GrpPushToSettings(); };
            _grpMentionTagInput.TextChanged += delegate { GrpPushToSettings(); };

            _grpTestWebhookBtn.Click += delegate
            {
                string url = _grpWebhookInput.Text.Trim();
                if (string.IsNullOrEmpty(url)) { BotLogger.Log("Group Radar", BotLogLevel.Warning, "No webhook URL set."); return; }
                string mention = _grpMentionTagInput.Text.Trim();
                DiscordNotifier.SendAsync(url, "✅ Group Webhook Test",
                    "Project Hades Group Radar is connected!", 5025616,
                    string.IsNullOrEmpty(mention) ? null : mention);
                BotLogger.Log("Group Radar", BotLogLevel.Info, "Test webhook sent.");
            };

            _grpAddMemberBtn.Click += delegate { GrpAddMember(); };
            _grpRemoveMemberBtn.Click += delegate { GrpRemoveMember(); };
            _grpRefreshAllBtn.Click += delegate { GrpRefreshAll(); };
            _grpRefreshVillagesBtn.Click += delegate { GrpRefreshSelectedMemberVillages(); };

            _grpMemberList.SelectedIndexChanged += delegate { GrpOnMemberSelected(); };

            _grpMemberTagInput.TextChanged += delegate
            {
                if (_grpLoading) return;
                GroupRadarMember m = GrpGetSelectedMember();
                if (m != null)
                    m.DiscordTag = _grpMemberTagInput.Text.Trim();
            };
        }

        private void GrpLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            if (_grpEnabledCheck == null) return;

            _grpLoading = true;
            try
            {
                GroupRadarSettings s = BotEngine.Instance.Settings.Radar.GroupRadar;
                _grpEnabledCheck.Checked = s.Enabled;
                _grpWebhookInput.Text = s.DiscordWebhookUrl ?? "";
                _grpMentionTagInput.Text = s.DiscordMentionTag ?? "";
                GrpPopulateMemberList();
            }
            finally
            {
                _grpLoading = false;
            }
        }

        private void GrpPopulateMemberList()
        {
            if (_grpMemberList == null) return;
            _grpMemberList.Items.Clear();
            GroupRadarSettings s = BotEngine.Instance?.Settings?.Radar?.GroupRadar;
            if (s == null) return;
            foreach (GroupRadarMember m in s.Members)
                _grpMemberList.Items.Add((m.Enabled ? "[✓] " : "[ ] ") + m.PlayerName);

            // Restore selection
            if (_grpSelectedMemberIndex >= 0 && _grpSelectedMemberIndex < _grpMemberList.Items.Count)
                _grpMemberList.SelectedIndex = _grpSelectedMemberIndex;
            else
                GrpShowMemberDetail(null);
        }

        private void GrpOnMemberSelected()
        {
            _grpSelectedMemberIndex = _grpMemberList.SelectedIndex;
            GrpShowMemberDetail(GrpGetSelectedMember());
        }

        private void GrpShowMemberDetail(GroupRadarMember m)
        {
            _grpLoading = true;
            try
            {
                if (m == null)
                {
                    _grpMemberNameLabel.Text = "No member selected";
                    _grpMemberTagInput.Text = "";
                    _grpMemberTagInput.Enabled = false;
                    _grpMemberVillagesLabel.Text = "Villages: none";
                    _grpRefreshVillagesBtn.Enabled = false;
                    _grpRemoveMemberBtn.Enabled = false;
                }
                else
                {
                    _grpMemberNameLabel.Text = m.PlayerName;
                    _grpMemberTagInput.Text = m.DiscordTag ?? "";
                    _grpMemberTagInput.Enabled = true;
                    int count = m.VillageIds != null ? m.VillageIds.Count : 0;
                    _grpMemberVillagesLabel.Text = "Villages: " + count + " found";
                    _grpRefreshVillagesBtn.Enabled = true;
                    _grpRemoveMemberBtn.Enabled = true;
                }
            }
            finally
            {
                _grpLoading = false;
            }
        }

        private GroupRadarMember GrpGetSelectedMember()
        {
            if (_grpSelectedMemberIndex < 0) return null;
            GroupRadarSettings s = BotEngine.Instance?.Settings?.Radar?.GroupRadar;
            if (s == null || _grpSelectedMemberIndex >= s.Members.Count) return null;
            return s.Members[_grpSelectedMemberIndex];
        }

        private void GrpPushToSettings()
        {
            if (_grpLoading) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            GroupRadarSettings s = BotEngine.Instance.Settings.Radar.GroupRadar;
            s.Enabled = _grpEnabledCheck.Checked;
            s.DiscordWebhookUrl = _grpWebhookInput.Text.Trim();
            s.DiscordMentionTag = _grpMentionTagInput.Text.Trim();
        }

        private void GrpWriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            GroupRadarSettings s = BotEngine.Instance.Settings.Radar.GroupRadar;
            s.Enabled = _grpEnabledCheck.Checked;
            s.DiscordWebhookUrl = _grpWebhookInput.Text.Trim();
            s.DiscordMentionTag = _grpMentionTagInput.Text.Trim();
            foreach (GroupActionRow row in _grpActionRows)
                row.WriteToSettings();
        }

        private void GrpSaveToSettings()
        {
            BotEngine.Instance?.Settings?.Save();
        }

        private void GrpBuildActionRows()
        {
            if (_grpActionListPanel == null) return;
            _grpActionListPanel.SuspendLayout();
            foreach (GroupActionRow row in _grpActionRows)
            {
                _grpActionListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _grpActionRows.Clear();

            GroupRadarSettings settings = BotEngine.Instance?.Settings?.Radar?.GroupRadar;

            string[] keys = RadarModule.AllActionKeys;
            for (int i = keys.Length - 1; i >= 0; i--)
            {
                string label = RadarModule.GetActionLabel(keys[i]);
                RadarActionSettings actionSettings = settings != null
                    ? settings.GetActionSettings(keys[i])
                    : new RadarActionSettings();

                GroupActionRow row = new GroupActionRow(keys[i], label, actionSettings, (keys.Length - 1 - i) % 2 != 0);
                row.Dock = DockStyle.Top;
                _grpActionListPanel.Controls.Add(row);
                _grpActionRows.Add(row);
            }

            _grpActionListPanel.ResumeLayout(false);
            _grpActionListPanel.PerformLayout();
        }

        private void GrpAddMember()
        {
            string name = _grpPlayerNameInput.Text.Trim();
            if (string.IsNullOrEmpty(name)) return;

            GroupRadarSettings s = BotEngine.Instance?.Settings?.Radar?.GroupRadar;
            if (s == null) return;

            // Check duplicate
            foreach (GroupRadarMember existing in s.Members)
            {
                if (string.Equals(existing.PlayerName, name, StringComparison.OrdinalIgnoreCase))
                {
                    BotLogger.Log("Group Radar", BotLogLevel.Warning, "'" + name + "' is already in the list.");
                    return;
                }
            }

            _grpAddMemberBtn.Enabled = false;
            _grpAddMemberBtn.Text = "...";
            _grpPlayerNameInput.Enabled = false;

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                List<int> villages = ResolveGroupPlayerVillages(name);
                if (this.IsDisposed) return;
                this.BeginInvoke(new Action(delegate
                {
                    GroupRadarMember m = new GroupRadarMember();
                    m.PlayerName = name;
                    m.VillageIds = villages;
                    s.Members.Add(m);
                    GrpSaveToSettings();
                    _grpPlayerNameInput.Text = "";
                    _grpPlayerNameInput.Enabled = true;
                    _grpAddMemberBtn.Enabled = true;
                    _grpAddMemberBtn.Text = "Add";
                    GrpPopulateMemberList();
                    BotLogger.Log("Group Radar", BotLogLevel.Info,
                        "Added '" + name + "' with " + villages.Count + " villages.");
                }));
            });
        }

        private void GrpRemoveMember()
        {
            GroupRadarMember m = GrpGetSelectedMember();
            if (m == null) return;
            GroupRadarSettings s = BotEngine.Instance?.Settings?.Radar?.GroupRadar;
            if (s == null) return;
            s.Members.Remove(m);
            _grpSelectedMemberIndex = -1;
            GrpSaveToSettings();
            GrpPopulateMemberList();
        }

        private void GrpRefreshSelectedMemberVillages()
        {
            GroupRadarMember m = GrpGetSelectedMember();
            if (m == null) return;

            _grpRefreshVillagesBtn.Enabled = false;
            _grpRefreshVillagesBtn.Text = "...";
            string name = m.PlayerName;

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                List<int> villages = ResolveGroupPlayerVillages(name);
                if (this.IsDisposed) return;
                this.BeginInvoke(new Action(delegate
                {
                    m.VillageIds = villages;
                    GrpSaveToSettings();
                    _grpRefreshVillagesBtn.Enabled = true;
                    _grpRefreshVillagesBtn.Text = "Refresh Villages";
                    GrpShowMemberDetail(m);
                    BotLogger.Log("Group Radar", BotLogLevel.Info,
                        "Refreshed '" + name + "': " + villages.Count + " villages.");
                }));
            });
        }

        private void GrpRefreshAll()
        {
            GroupRadarSettings s = BotEngine.Instance?.Settings?.Radar?.GroupRadar;
            if (s == null || s.Members.Count == 0) return;

            _grpRefreshAllBtn.Enabled = false;
            _grpRefreshAllBtn.Text = "...";

            List<GroupRadarMember> members = new List<GroupRadarMember>(s.Members);
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                foreach (GroupRadarMember m in members)
                {
                    List<int> villages = ResolveGroupPlayerVillages(m.PlayerName);
                    m.VillageIds = villages;
                }
                if (this.IsDisposed) return;
                this.BeginInvoke(new Action(delegate
                {
                    GrpSaveToSettings();
                    _grpRefreshAllBtn.Enabled = true;
                    _grpRefreshAllBtn.Text = "Refresh All";
                    GrpPopulateMemberList();
                    BotLogger.Log("Group Radar", BotLogLevel.Info, "Refreshed all group members.");
                }));
            });
        }

        private List<int> ResolveGroupPlayerVillages(string playerName)
        {
            // Route through the RadarModule which has the server lookup method
            if (BotEngine.Instance == null) return new List<int>();
            foreach (IBotModule module in BotEngine.Instance.Modules)
            {
                RadarModule rm = module as RadarModule;
                if (rm != null)
                    return rm.ResolvePlayerVillages(playerName);
            }
            return new List<int>();
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
            _rcRefreshTimer.Tick += delegate { try { RcUpdateStatusDisplay(); } catch { } };
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
            if (_rcLoading) return;
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
            _rcLoading = true;
            try
            {
                RecruitingSettings s = BotEngine.Instance.Settings.Recruiting;
                _rcEnabledCheck.Checked = s.Enabled;
                _rcIntervalInput.Value = Math.Max(_rcIntervalInput.Minimum,
                    Math.Min(_rcIntervalInput.Maximum, s.CycleIntervalSeconds));
                _rcDelayInput.Value = Math.Max(_rcDelayInput.Minimum,
                    Math.Min(_rcDelayInput.Maximum, s.DelayBetweenVillagesMs));

                RcBuildVillageList();
            }
            finally { _rcLoading = false; }
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
            _crRefreshTimer.Tick += delegate { try { CrUpdateStatusDisplay(); } catch { } };
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
            if (_crLoading) return;
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
            _crLoading = true;
            try
            {
                CastleRepairSettings s = BotEngine.Instance.Settings.CastleRepair;
                _crEnabledCheck.Checked = s.Enabled;
                _crIntervalInput.Value = Math.Max(_crIntervalInput.Minimum,
                    Math.Min(_crIntervalInput.Maximum, s.IntervalSeconds));
                _crDelayInput.Value = Math.Max(_crDelayInput.Minimum,
                    Math.Min(_crDelayInput.Maximum, s.DelayBetweenVillagesMs));
                _crRepairOnAttackCheck.Checked = s.RepairOnAttack;

                CrPopulateVillageList();
            }
            finally { _crLoading = false; }
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
                _logBox.ReadOnly = false;
                _logBox.SelectionStart = 0;
                _logBox.SelectionLength = _logBox.GetFirstCharIndexFromLine(500);
                _logBox.SelectedText = "";
                _logBox.ReadOnly = true;
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
                GrpWriteToSettings();
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
            else if (_tabControl.SelectedTab == _builderPage)
            {
                BldWriteToSettings();
                tabName = "Village Builder";
            }
            else if (_tabControl.SelectedTab == _bombPage)
            {
                AbWriteToSettings();
                tabName = "Auto Bomb";
            }
            else if (_tabControl.SelectedTab == _bombMultiPage)
            {
                AbmWriteToSettings();
                tabName = "Auto Bomb Multi";
            }
            else if (_tabControl.SelectedTab == _popularityPage)
            {
                PpWriteToSettings();
                tabName = "Popularity";
            }
            else if (_tabControl.SelectedTab == _scoutPage)
            {
                ScPushGlobalSettings();
                tabName = "Scout";
            }
            else if (_tabControl.SelectedTab == _miscPage)
            {
                MiscWriteToSettings();
                tabName = "Misc";
            }
            else if (_tabControl.SelectedTab == _autoPage)
            {
                AutoWriteToSettings();
                tabName = "Auto";
            }
            else if (_tabControl.SelectedTab == _bqPage)
            {
                BqWriteToSettings();
                tabName = "Banquet";
            }
            else if (_tabControl.SelectedTab == _defenderPage)
            {
                DfWriteToSettings();
                tabName = "Defender";
            }
            else if (_tabControl.SelectedTab == _mkPage)
            {
                MkWriteToSettings();
                tabName = "Monk";
            }

            BotEngine.Instance.ApplySettings();
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
            else if (_tabControl.SelectedTab == _builderPage)
            {
                BldLoadFromSettings();
                tabName = "Village Builder";
            }
            else if (_tabControl.SelectedTab == _bombPage)
            {
                AbLoadFromSettings();
                tabName = "Auto Bomb";
            }
            else if (_tabControl.SelectedTab == _bombMultiPage)
            {
                AbmLoadFromSettings();
                tabName = "Auto Bomb Multi";
            }
            else if (_tabControl.SelectedTab == _miscPage)
            {
                MiscLoadFromSettings();
                tabName = "Misc";
            }
            else if (_tabControl.SelectedTab == _popularityPage)
            {
                PpLoadFromSettings();
                tabName = "Popularity";
            }
            else if (_tabControl.SelectedTab == _scoutPage)
            {
                ScLoadFromSettings();
                tabName = "Scout";
            }
            else if (_tabControl.SelectedTab == _autoPage)
            {
                AutoLoadFromSettings();
                tabName = "Auto";
            }
            else if (_tabControl.SelectedTab == _bqPage)
            {
                BqLoadFromSettings();
                tabName = "Banquet";
            }
            else if (_tabControl.SelectedTab == _defenderPage)
            {
                DfLoadFromSettings();
                tabName = "Defender";
            }
            else if (_tabControl.SelectedTab == _mkPage)
            {
                MkLoadFromSettings();
                tabName = "Monk";
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
            // Build the Stats sub-tab
            TrBuildStatsTab();

            _trEnabledCheck.CheckedChanged += delegate { TrWriteToSettings(); };
            _trAddMarketsBtn.Click += delegate { TrAddMarketsClick(); };
            _trMarketRefreshBtn.Click += delegate { TrRefreshMarkets(); };

            _trRefreshTimer = new Timer();
            _trRefreshTimer.Interval = 2000;
            _trRefreshTimer.Tick += delegate { try { TrUpdateStatusDisplay(); } catch { } };
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

            Button dupRouteBtn = new Button();
            dupRouteBtn.Text = "Duplicate Route";
            dupRouteBtn.BackColor = Color.FromArgb(80, 160, 80);
            dupRouteBtn.ForeColor = Color.White;
            dupRouteBtn.FlatStyle = FlatStyle.Flat;
            dupRouteBtn.FlatAppearance.BorderSize = 0;
            dupRouteBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            dupRouteBtn.Size = new Size(120, 24);
            dupRouteBtn.Location = new Point(458, 4);
            dupRouteBtn.Cursor = Cursors.Hand;
            dupRouteBtn.Click += delegate { TrDuplicateRouteClick(); };
            btnBar.Controls.Add(dupRouteBtn);

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

            Button dupBtn = new Button();
            dupBtn.Text = "Duplicate Route";
            dupBtn.BackColor = Color.FromArgb(80, 160, 80);
            dupBtn.ForeColor = Color.White;
            dupBtn.FlatStyle = FlatStyle.Flat;
            dupBtn.FlatAppearance.BorderSize = 0;
            dupBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            dupBtn.Size = new Size(120, 24);
            dupBtn.Location = new Point(578, 4);
            dupBtn.Cursor = Cursors.Hand;
            dupBtn.Click += delegate { TrDuplicatePlayerRouteClick(); };
            btnBar.Controls.Add(dupBtn);

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

        private void TrDuplicatePlayerRouteClick()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            PlayerTradeRouteRow selected = null;
            foreach (PlayerTradeRouteRow row in _trPlayerRouteRows)
                if (row.IsSelected) { selected = row; break; }
            if (selected == null) return;

            PlayerTradeRouteSettings clone = selected.Route.Clone();
            PlayerTradeRouteEditorForm form = new PlayerTradeRouteEditorForm(clone, "Duplicate Player Route");
            form.ShowDialog(this);
            if (form.Saved)
            {
                BotEngine.Instance.Settings.Trade.PlayerRoutes.Add(clone);
                TrBuildPlayerRoutesList();
                BotLogger.Log("Trade", BotLogLevel.Info,
                    "Player route '" + clone.Name + "' duplicated from '" + selected.Route.Name + "'.");
            }
        }

        // ── Stats tab ────────────────────────────────────────────────────────────

        private ListView _trStatsListView;
        private Label _trStatsSessionLabel;
        private Label _trStatsDurationLabel;
        private Label _trStatsStartGoldLabel;
        private Label _trStatsCurrentGoldLabel;
        private Label _trStatsGoldDeltaLabel;

        private void TrBuildStatsTab()
        {
            _trStatsTab.Controls.Clear();

            // ── Info bar ──────────────────────────────────────────────────────────
            Panel infoBar = new Panel();
            infoBar.Dock = DockStyle.Top;
            infoBar.Height = 70;
            infoBar.BackColor = Color.FromArgb(36, 38, 48);
            infoBar.Padding = new Padding(8, 6, 8, 6);

            _trStatsSessionLabel     = MakeStatsLabel("Session start: —",       8,  8);
            _trStatsDurationLabel    = MakeStatsLabel("Duration: —",             8, 28);
            _trStatsStartGoldLabel   = MakeStatsLabel("Starting gold: —",      340,  8);
            _trStatsCurrentGoldLabel = MakeStatsLabel("Current gold: —",       340, 28);
            _trStatsGoldDeltaLabel   = MakeStatsLabel("Gold change: —",        340, 48);
            infoBar.Controls.Add(_trStatsSessionLabel);
            infoBar.Controls.Add(_trStatsDurationLabel);
            infoBar.Controls.Add(_trStatsStartGoldLabel);
            infoBar.Controls.Add(_trStatsCurrentGoldLabel);
            infoBar.Controls.Add(_trStatsGoldDeltaLabel);

            // ── Button bar ────────────────────────────────────────────────────────
            Panel btnBar = new Panel();
            btnBar.Dock = DockStyle.Top;
            btnBar.Height = 32;
            btnBar.BackColor = Color.FromArgb(30, 32, 42);

            Button refreshBtn = new Button();
            refreshBtn.Text = "Refresh Stats";
            refreshBtn.BackColor = Color.FromArgb(60, 100, 160);
            refreshBtn.ForeColor = Color.White;
            refreshBtn.FlatStyle = FlatStyle.Flat;
            refreshBtn.FlatAppearance.BorderSize = 0;
            refreshBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            refreshBtn.Size = new Size(110, 24);
            refreshBtn.Location = new Point(8, 4);
            refreshBtn.Cursor = Cursors.Hand;
            refreshBtn.Click += delegate { TrRefreshStats(); };
            btnBar.Controls.Add(refreshBtn);

            Button resetBtn = new Button();
            resetBtn.Text = "Reset Stats";
            resetBtn.BackColor = Color.FromArgb(160, 60, 60);
            resetBtn.ForeColor = Color.White;
            resetBtn.FlatStyle = FlatStyle.Flat;
            resetBtn.FlatAppearance.BorderSize = 0;
            resetBtn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            resetBtn.Size = new Size(100, 24);
            resetBtn.Location = new Point(126, 4);
            resetBtn.Cursor = Cursors.Hand;
            resetBtn.Click += delegate { TrResetStatsClick(); };
            btnBar.Controls.Add(resetBtn);

            // ── Column header row ─────────────────────────────────────────────────
            Panel headerRow = new Panel();
            headerRow.Dock = DockStyle.Top;
            headerRow.Height = 22;
            headerRow.BackColor = Color.FromArgb(44, 46, 58);
            headerRow.Controls.Add(MakeStatsHeaderLabel("Resource",          8,   3));
            headerRow.Controls.Add(MakeStatsHeaderLabel("Sold (units)",    220,   3));
            headerRow.Controls.Add(MakeStatsHeaderLabel("Bought (units)",  360,   3));
            headerRow.Controls.Add(MakeStatsHeaderLabel("Sent to Villages", 500,  3));

            // ── Main ListView ─────────────────────────────────────────────────────
            _trStatsListView = new ListView();
            _trStatsListView.Dock = DockStyle.Fill;
            _trStatsListView.View = View.Details;
            _trStatsListView.FullRowSelect = true;
            _trStatsListView.GridLines = false;
            _trStatsListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            _trStatsListView.BackColor = Color.FromArgb(24, 24, 32);
            _trStatsListView.ForeColor = Color.FromArgb(220, 220, 235);
            _trStatsListView.Font = new Font("Segoe UI", 8.5f);
            _trStatsListView.BorderStyle = BorderStyle.None;
            _trStatsListView.Columns.Add("Resource",          200);
            _trStatsListView.Columns.Add("Sold (units)",      130);
            _trStatsListView.Columns.Add("Bought (units)",    130);
            _trStatsListView.Columns.Add("Sent to Villages",  130);

            // Add in reverse dock order so Fill goes to bottom
            _trStatsTab.Controls.Add(_trStatsListView);
            _trStatsTab.Controls.Add(headerRow);
            _trStatsTab.Controls.Add(btnBar);
            _trStatsTab.Controls.Add(infoBar);
        }

        private Label MakeStatsLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 8.5f);
            lbl.ForeColor = Color.FromArgb(200, 200, 215);
            lbl.BackColor = Color.Transparent;
            lbl.Location = new Point(x, y);
            lbl.Text = text;
            return lbl;
        }

        private Label MakeStatsHeaderLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(170, 170, 190);
            lbl.BackColor = Color.Transparent;
            lbl.Location = new Point(x, y);
            lbl.Text = text;
            return lbl;
        }

        private void TrRefreshStats()
        {
            if (_trStatsListView == null) return;
            TradeModule module = GetTradeModule();

            if (module == null)
            {
                _trStatsSessionLabel.Text     = "Session start: (module not running)";
                _trStatsDurationLabel.Text    = "Duration: —";
                _trStatsStartGoldLabel.Text   = "Starting gold: —";
                _trStatsCurrentGoldLabel.Text = "Current gold: —";
                _trStatsGoldDeltaLabel.Text   = "Gold change: —";
                _trStatsListView.Items.Clear();
                return;
            }

            TradeModule.TradeSessionStats stats = module.GetStats();

            _trStatsSessionLabel.Text  = "Session start: " + stats.SessionStart.ToString("HH:mm:ss dd/MM/yyyy");
            TimeSpan dur = DateTime.Now - stats.SessionStart;
            _trStatsDurationLabel.Text = "Duration: " + (int)dur.TotalHours + "h " + dur.Minutes + "m " + dur.Seconds + "s";
            _trStatsStartGoldLabel.Text = "Starting gold: " + stats.SessionStartGold.ToString("N0");

            long currentGold = 0;
            try { currentGold = (long)GameEngine.Instance.World.getCurrentGold(); } catch { }
            _trStatsCurrentGoldLabel.Text = "Current gold: " + currentGold.ToString("N0");

            long delta = currentGold - stats.SessionStartGold;
            string sign = delta >= 0 ? "+" : "";
            _trStatsGoldDeltaLabel.Text   = "Gold change: " + sign + delta.ToString("N0");
            _trStatsGoldDeltaLabel.ForeColor = delta >= 0
                ? Color.FromArgb(100, 220, 100)
                : Color.FromArgb(220, 100, 100);

            _trStatsListView.Items.Clear();

            System.Collections.Generic.HashSet<int> allIds =
                new System.Collections.Generic.HashSet<int>(stats.SoldByResource.Keys);
            foreach (int k in stats.BoughtByResource.Keys) allIds.Add(k);
            foreach (int k in stats.SentByResource.Keys)   allIds.Add(k);

            if (allIds.Count == 0)
            {
                _trStatsListView.Items.Add(new ListViewItem(
                    new string[] { "No trades recorded yet.", "", "", "" }));
                return;
            }

            System.Collections.Generic.List<ListViewItem> items =
                new System.Collections.Generic.List<ListViewItem>();
            foreach (int id in allIds)
            {
                long sold   = stats.SoldByResource.ContainsKey(id)   ? stats.SoldByResource[id]   : 0;
                long bought = stats.BoughtByResource.ContainsKey(id) ? stats.BoughtByResource[id] : 0;
                long sent   = stats.SentByResource.ContainsKey(id)   ? stats.SentByResource[id]   : 0;
                string name = TradeModuleConstants.GetResourceName(id);
                items.Add(new ListViewItem(new string[] {
                    name,
                    sold.ToString("N0"),
                    bought.ToString("N0"),
                    sent.ToString("N0")
                }));
            }
            items.Sort((a, b) =>
            {
                long ta = ParseN0(a.SubItems[1].Text) + ParseN0(a.SubItems[2].Text) + ParseN0(a.SubItems[3].Text);
                long tb = ParseN0(b.SubItems[1].Text) + ParseN0(b.SubItems[2].Text) + ParseN0(b.SubItems[3].Text);
                return tb.CompareTo(ta);
            });
            _trStatsListView.Items.AddRange(items.ToArray());
        }

        private static long ParseN0(string s)
        {
            long v;
            long.TryParse(s.Replace(",", "").Replace(" ", ""), out v);
            return v;
        }

        private void _trSubTabs_Selected(object sender, System.Windows.Forms.TabControlEventArgs e)
        {
            if (_trSubTabs.SelectedTab == _trStatsTab)
                TrRefreshStats();
        }

        private void TrResetStatsClick()
        {
            TradeModule module = GetTradeModule();
            if (module != null) { module.GetStats().Reset(); TrRefreshStats(); }
        }

        // ── End stats tab ─────────────────────────────────────────────────────────

        private void TrLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;
            _trLoading = true;
            try
            {
            TradeSettings s = BotEngine.Instance.Settings.Trade;
            _trPriorityCombo.SelectedIndex = (int)s.Priority;
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
            _trDisableOnCardExpiryCheck.Checked = s.DisableOnTradeCardExpiry;
            _trAutoSaveRouteProgressCheck.Checked = s.AutoSavePlayerRouteProgress;

            TrRefreshMarkets();
            TrBuildRoutesList();
            TrBuildPlayerRoutesList();
            }
            finally { _trLoading = false; }
        }

        private void TrWriteToSettings()
        {
            if (_trLoading) return;
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
            if (_trPriorityCombo.SelectedIndex >= 0)
                s.Priority = (TradePriority)_trPriorityCombo.SelectedIndex;
            s.DisableOnTradeCardExpiry = _trDisableOnCardExpiryCheck.Checked;
            s.AutoSavePlayerRouteProgress = _trAutoSaveRouteProgressCheck.Checked;

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

        private void TrReloadCurrentVillageGrid()
        {
            if (_trSelectedVillageId == -1) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            VillageMarketTradeInfo info = BotEngine.Instance.Settings.Trade.GetVillageMarketInfo(_trSelectedVillageId);
            _trVillageTradingCheck.Checked = info.IsTrading;
            _trResourceGrid.LoadVillage(info);
            _trMarketsListBox.Items.Clear();
            foreach (int marketId in info.MarketTargets)
                _trMarketsListBox.Items.Add(marketId);
            _trMarketCountLabel.Text = "Total Markets: " + info.MarketTargets.Count;
        }

        private TradeModule GetTradeModule()
        {
            if (BotEngine.Instance == null) return null;
            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                TradeModule t = m as TradeModule;
                if (t != null) return t;
            }
            return null;
        }

        private void TrAddMarketsClick()
        {
            TrSaveCurrentVillage();

            TradeModule module = GetTradeModule();
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
                // Reload grid from settings (no save — avoids overwriting the just-copied data)
                TrReloadCurrentVillageGrid();
                TrRefreshMarkets();
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

        private void TrDuplicateRouteClick()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            TradeSettings s = BotEngine.Instance.Settings.Trade;
            if (_trSelectedRouteIndex < 0 || _trSelectedRouteIndex >= s.Routes.Count)
            {
                BotLogger.Log("Trade", BotLogLevel.Warning, "Select a route to duplicate.");
                return;
            }

            TradeRouteSettings clone = s.Routes[_trSelectedRouteIndex].Clone();
            TradeRouteEditorForm editor = new TradeRouteEditorForm(clone, "Duplicate Route - " + s.Routes[_trSelectedRouteIndex].Name);
            editor.ShowDialog(this);
            if (editor.Saved)
            {
                string originalName = s.Routes[_trSelectedRouteIndex].Name;
                s.Routes.Add(clone);
                TrBuildRoutesList();
                BotLogger.Log("Trade", BotLogLevel.Info,
                    "Trade route '" + clone.Name + "' duplicated from '" + originalName + "'.");
            }
        }

        private void TrUpdateStatusDisplay()
        {
            if (_trEnabledCheck == null) return;

            // Sync checkbox with module state (module may have been disabled by card expiry)
            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
            {
                bool moduleEnabled = BotEngine.Instance.Settings.Trade.Enabled;
                if (_trEnabledCheck.Checked != moduleEnabled)
                    _trEnabledCheck.Checked = moduleEnabled;
            }

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

        // =====================================================================
        // Village Builder tab runtime
        // =====================================================================

        private void WireUpBuilderTab()
        {
            // Wire events for Designer-defined controls
            _bldEnabledCheck.CheckedChanged += delegate { BldWriteToSettings(); };
            _bldVillageCombo.SelectedIndexChanged += delegate { BldVillageSelected(); };
            _bldVillageEnabledCheck.CheckedChanged += delegate { BldVillageEnabledChanged(); };
            _bldCopySettingsBtn.Click += delegate { BldCopySettingsClick(); };
            _bldImportFileBtn.Click += delegate { BldImportFromFile(); };
            _bldRefreshStateBtn.Click += delegate { BldRefreshState(); };
            _bldExportFileBtn.Click += delegate { BldExportToFile(); };
            _bldClearLayoutBtn.Click += delegate { BldClearLayout(); };

            // Build column header labels dynamically
            string[] cols = new string[] { "Building", "Type", "Status", "X", "Y", "Done" };
            int[] colX = new int[] { 8, 216, 264, 392, 436, 480 };
            for (int i = 0; i < cols.Length; i++)
            {
                Label cl = new Label();
                cl.Text = cols[i];
                cl.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
                cl.ForeColor = Color.FromArgb(160, 165, 180);
                cl.AutoSize = true;
                cl.Location = new Point(colX[i], 4);
                _bldColHeader.Controls.Add(cl);
            }

            // Refresh timer
            _bldRefreshTimer = new Timer();
            _bldRefreshTimer.Interval = 2000;
            _bldRefreshTimer.Tick += delegate { try { BldUpdateStatusDisplay(); } catch { } };
            _bldRefreshTimer.Start();
        }

        private void BldLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;
            _bldLoading = true;
            try
            {
                VillageBuilderSettings s = BotEngine.Instance.Settings.VillageBuilder;
                _bldEnabledCheck.Checked = s.Enabled;
                _bldIntervalInput.Value = Math.Max(_bldIntervalInput.Minimum,
                    Math.Min(_bldIntervalInput.Maximum, s.CycleIntervalSeconds));
                _bldDelayInput.Value = Math.Max(_bldDelayInput.Minimum,
                    Math.Min(_bldDelayInput.Maximum, s.DelayBetweenVillagesMs));
                _bldWaitForResourcesCheck.Checked = s.WaitForResources;

                BldPopulateVillageCombo();
            }
            finally { _bldLoading = false; }
        }

        private void BldWriteToSettings()
        {
            if (_bldLoading) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            VillageBuilderSettings s = BotEngine.Instance.Settings.VillageBuilder;
            s.Enabled = _bldEnabledCheck.Checked;
            s.CycleIntervalSeconds = (int)_bldIntervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_bldDelayInput.Value;
            s.WaitForResources = _bldWaitForResourcesCheck.Checked;

            // Save current village enabled state
            if (_bldSelectedVillageId > 0)
            {
                VillageBuildLayout layout = s.GetOrCreateLayout(_bldSelectedVillageId);
                layout.Enabled = _bldVillageEnabledCheck.Checked;
            }

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is VillageBuilderModule)
                    m.Enabled = s.Enabled;
            }
        }

        private void BldPopulateVillageCombo()
        {
            _bldVillageCombo.Items.Clear();
            _bldSelectedVillageId = -1;

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<int> ids = GameEngine.Instance.World.getUserVillageIDList();
            if (ids == null) return;

            foreach (int id in ids)
            {
                string name = GameEngine.Instance.World.getVillageName(id);
                _bldVillageCombo.Items.Add(new BldComboItem(id, "[" + id + "] " + name));
            }

            if (_bldVillageCombo.Items.Count > 0)
                _bldVillageCombo.SelectedIndex = 0;
        }

        private void BldVillageSelected()
        {
            BldComboItem item = _bldVillageCombo.SelectedItem as BldComboItem;
            if (item == null) return;

            _bldSelectedVillageId = item.VillageId;

            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
            {
                VillageBuildLayout layout = BotEngine.Instance.Settings.VillageBuilder.GetLayout(_bldSelectedVillageId);
                _bldVillageEnabledCheck.Checked = layout != null && layout.Enabled;

                // Auto-load current village buildings if no layout exists yet
                if (layout == null || layout.Buildings.Count == 0)
                {
                    BldAutoLoadFromVillage();
                }
            }

            BldRefreshBuildingList();
        }

        private void BldVillageEnabledChanged()
        {
            if (_bldSelectedVillageId <= 0) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            VillageBuildLayout layout = BotEngine.Instance.Settings.VillageBuilder.GetOrCreateLayout(_bldSelectedVillageId);
            layout.Enabled = _bldVillageEnabledCheck.Checked;
        }

        private void BldRefreshBuildingList()
        {
            _bldBuildingListPanel.SuspendLayout();
            foreach (BuildingListRow row in _bldBuildingRows)
            {
                _bldBuildingListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _bldBuildingRows.Clear();

            if (_bldSelectedVillageId <= 0 || BotEngine.Instance == null || BotEngine.Instance.Settings == null)
            {
                _bldBuildingListPanel.ResumeLayout();
                return;
            }

            VillageBuildLayout layout = BotEngine.Instance.Settings.VillageBuilder.GetLayout(_bldSelectedVillageId);
            if (layout == null || layout.Buildings.Count == 0)
            {
                _bldBuildingListPanel.ResumeLayout();
                return;
            }

            for (int i = layout.Buildings.Count - 1; i >= 0; i--)
            {
                BuildingListRow row = new BuildingListRow(layout.Buildings[i], i);
                row.Dock = DockStyle.Top;
                _bldBuildingListPanel.Controls.Add(row);
                _bldBuildingRows.Add(row);
            }

            _bldBuildingListPanel.ResumeLayout();
        }

        private void BldImportFromFile()
        {
            if (_bldSelectedVillageId <= 0) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Layout files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.Title = "Import Village Layout";
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                List<BuildingEntry> buildings = VillageBuilderModule.ImportLayoutFromFile(dlg.FileName);
                if (buildings.Count == 0)
                {
                    BotLogger.Log("Village Builder", BotLogLevel.Warning, "No buildings found in file.");
                    return;
                }

                VillageBuildLayout layout = BotEngine.Instance.Settings.VillageBuilder.GetOrCreateLayout(_bldSelectedVillageId);
                layout.Buildings.Clear();
                layout.Buildings.AddRange(buildings);

                BotLogger.Log("Village Builder", BotLogLevel.Info,
                    "Imported " + buildings.Count + " buildings from file for village " + _bldSelectedVillageId + ".");

                BldRefreshBuildingList();
            }
            catch (Exception ex)
            {
                BotLogger.Log("Village Builder", BotLogLevel.Error, "Import failed: " + ex.Message);
            }
        }

        private void BldAutoLoadFromVillage()
        {
            if (_bldSelectedVillageId <= 0) return;
            if (GameEngine.Instance == null) return;

            VillageMap village = GameEngine.Instance.getVillage(_bldSelectedVillageId);
            if (village == null) return;

            List<BuildingEntry> buildings = VillageBuilderModule.ImportFromVillage(village);
            if (buildings.Count == 0) return;

            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            VillageBuildLayout layout = BotEngine.Instance.Settings.VillageBuilder.GetOrCreateLayout(_bldSelectedVillageId);
            layout.Buildings.Clear();
            layout.Buildings.AddRange(buildings);

            // Mark all as already placed since they exist in the village
            foreach (BuildingEntry entry in layout.Buildings)
            {
                entry.Placed = true;
                entry.Status = "Already built";
            }

            BotLogger.Log("Village Builder", BotLogLevel.Info,
                "Auto-loaded " + buildings.Count + " buildings from village " + _bldSelectedVillageId + ".");
        }

        private void BldRefreshState()
        {
            if (_bldSelectedVillageId <= 0) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            VillageBuildLayout layout = BotEngine.Instance.Settings.VillageBuilder.GetLayout(_bldSelectedVillageId);
            if (layout == null || layout.Buildings.Count == 0) return;

            VillageMap village = GameEngine.Instance != null
                ? GameEngine.Instance.getVillage(_bldSelectedVillageId) : null;

            if (village == null)
            {
                BotLogger.Log("Village Builder", BotLogLevel.Warning, "Village not loaded, cannot refresh state.");
                return;
            }

            int updated = 0;
            foreach (BuildingEntry entry in layout.Buildings)
            {
                bool wasPlaced = entry.Placed;
                bool exists = false;
                bool constructing = false;

                try
                {
                    foreach (VillageMapBuilding b in village.Buildings)
                    {
                        if (b.buildingType == entry.BuildingType &&
                            b.buildingLocation.X == entry.X &&
                            b.buildingLocation.Y == entry.Y)
                        {
                            if (b.complete)
                                exists = true;
                            else
                                constructing = true;
                            break;
                        }
                    }
                }
                catch { }

                if (exists)
                {
                    entry.Placed = true;
                    entry.Status = "Already built";
                }
                else if (constructing)
                {
                    entry.Status = "Constructing";
                }
                else if (!entry.Placed)
                {
                    entry.Status = "Pending";
                }

                if (entry.Placed != wasPlaced)
                    updated++;
            }

            BotLogger.Log("Village Builder", BotLogLevel.Info,
                "Refreshed building state for village " + _bldSelectedVillageId +
                ". " + updated + " status change(s).");

            BldRefreshBuildingList();
        }

        private void BldExportToFile()
        {
            if (_bldSelectedVillageId <= 0) return;
            if (GameEngine.Instance == null) return;

            VillageMap village = GameEngine.Instance.getVillage(_bldSelectedVillageId);
            if (village == null)
            {
                BotLogger.Log("Village Builder", BotLogLevel.Warning, "Village not loaded.");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Layout files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.Title = "Export Village Layout";
            dlg.FileName = "village_" + _bldSelectedVillageId + ".txt";
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                VillageBuilderModule.ExportLayoutToFile(dlg.FileName, village);
                BotLogger.Log("Village Builder", BotLogLevel.Info,
                    "Exported village layout to " + dlg.FileName);
            }
            catch (Exception ex)
            {
                BotLogger.Log("Village Builder", BotLogLevel.Error, "Export failed: " + ex.Message);
            }
        }

        private void BldClearLayout()
        {
            if (_bldSelectedVillageId <= 0) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            VillageBuildLayout layout = BotEngine.Instance.Settings.VillageBuilder.GetLayout(_bldSelectedVillageId);
            if (layout != null)
            {
                layout.Buildings.Clear();
                BotLogger.Log("Village Builder", BotLogLevel.Info, "Cleared layout for village " + _bldSelectedVillageId + ".");
            }

            BldRefreshBuildingList();
        }

        private void BldCopySettingsClick()
        {
            BldWriteToSettings();
            CopyBuilderSettingsForm form = new CopyBuilderSettingsForm();
            form.ShowDialog(this);
            if (form.Copied)
                BldRefreshBuildingList();
        }

        private void BldUpdateStatusDisplay()
        {
            if (_bldEnabledCheck == null) return;

            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
            {
                bool moduleEnabled = BotEngine.Instance.Settings.VillageBuilder.Enabled;
                if (_bldEnabledCheck.Checked != moduleEnabled)
                    _bldEnabledCheck.Checked = moduleEnabled;
            }

            bool enabled = _bldEnabledCheck.Checked;
            _bldStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _bldStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
        }

        private class BldComboItem
        {
            public int VillageId;
            public string Display;
            public BldComboItem(int id, string display) { VillageId = id; Display = display; }
            public override string ToString() { return Display; }
        }

        // =====================================================================
        // Auto Bomb tab runtime
        // =====================================================================

        private void WireUpAutoBombTab()
        {
            // Wire events for Designer-defined controls
            _abLoadArmiesBtn.Click += delegate { AbLoadArmies(); };
            _abSelectAllBtn.Click += delegate { AbSetAllSelected(true); };
            _abDeselectAllBtn.Click += delegate { AbSetAllSelected(false); };
            _abSubmitBtn.Click += delegate { AbSubmitToQueue(); };
            _abLaunchBtn.Click += delegate { AbLaunch(); };
            _abCancelAllBtn.Click += delegate { AbCancelAll(); };
            _abClearQueueBtn.Click += delegate { AbClearQueue(); };

            // Build setup column header labels dynamically
            string[] setupCols = new string[] { "", "Village", "Time", "Card", "Cap?", "Formation", "P", "Arch", "Pike", "Sw", "Cat", "Cap", "Stack", "Type" };
            int[] setupColX = new int[] { 4, 24, 168, 246, 314, 334, 514, 548, 582, 616, 650, 680, 712, 756 };
            for (int i = 0; i < setupCols.Length; i++)
            {
                Label cl = new Label();
                cl.Text = setupCols[i];
                cl.Font = new Font("Segoe UI", 6.5f, FontStyle.Bold);
                cl.ForeColor = Color.FromArgb(160, 165, 180);
                cl.AutoSize = true;
                cl.Location = new Point(setupColX[i], 3);
                _abSetupColHeader.Controls.Add(cl);
            }

            // Build pending column header labels dynamically
            string[] pendCols = new string[] { "Stack", "Village", "Target", "Travel", "Send Time", "Arrival", "Formation", "Type", "Status" };
            int[] pendColX = new int[] { 8, 42, 186, 250, 334, 408, 482, 566, 640 };
            for (int i = 0; i < pendCols.Length; i++)
            {
                Label cl = new Label();
                cl.Text = pendCols[i];
                cl.Font = new Font("Segoe UI", 6.5f, FontStyle.Bold);
                cl.ForeColor = Color.FromArgb(160, 165, 180);
                cl.AutoSize = true;
                cl.Location = new Point(pendColX[i], 3);
                _abPendingColHeader.Controls.Add(cl);
            }

            // Refresh timer
            _abRefreshTimer = new Timer();
            _abRefreshTimer.Interval = 1000;
            _abRefreshTimer.Tick += delegate { try { AbUpdateDisplay(); } catch { } };
            _abRefreshTimer.Start();
        }

        private void WireUpTargetQueueTab()
        {
            _abQueueAddIdBtn.Click += delegate { AbQueueAddId(); };
            _abQueueLookupBtn.Click += delegate { AbQueueLookupPlayer(); };
            _abQueueRemoveBtn.Click += delegate { AbQueueRemoveSelected(); };
            _abQueueClearBtn.Click += delegate { AbQueueClear(); };
            _abQueueSaveBtn.Click += delegate { AbQueueSaveToFile(); };
            _abQueueLoadBtn.Click += delegate { AbQueueLoadFromFile(); };
            _abQueueResetBtn.Click += delegate { AbQueueReset(); };
            _abQueueAddSelectedVillageBtn.Click += delegate { AbQueueAddSelectedVillage(); };
            _abQueueAddSelectedPlayerBtn.Click += delegate { AbQueueAddSelectedPlayer(); };
        }

        private void WireUpAutoBombMultiTab()
        {
            // ── Connection panel labels (dynamic — not in designer) ────────────
            Label apiLbl = new Label();
            apiLbl.Text = "API URL:";
            apiLbl.Font = new Font("Segoe UI", 7.5f);
            apiLbl.ForeColor = TextSec;
            apiLbl.AutoSize = true;
            apiLbl.Location = new Point(6, 10);
            _abmConnPanel.Controls.Add(apiLbl);

            Label keyLbl = new Label();
            keyLbl.Text = "Key:";
            keyLbl.Font = new Font("Segoe UI", 7.5f);
            keyLbl.ForeColor = TextSec;
            keyLbl.AutoSize = true;
            keyLbl.Location = new Point(328, 10);
            _abmConnPanel.Controls.Add(keyLbl);

            // ── Coordinator panel labels ──────────────────────────────────────
            Label tgtLbl = new Label();
            tgtLbl.Text = "Target VID:";
            tgtLbl.Font = new Font("Segoe UI", 7.5f);
            tgtLbl.ForeColor = TextSec;
            tgtLbl.AutoSize = true;
            tgtLbl.Location = new Point(6, 11);
            _abmCtrlPanel.Controls.Add(tgtLbl);

            Label sdLbl = new Label();
            sdLbl.Text = "Stack Delay:";
            sdLbl.Font = new Font("Segoe UI", 7.5f);
            sdLbl.ForeColor = TextSec;
            sdLbl.AutoSize = true;
            sdLbl.Location = new Point(168, 11);
            _abmCtrlPanel.Controls.Add(sdLbl);

            // ── Village column headers ────────────────────────────────────────
            string[] vCols = { "", "Village", "Travel", "Card", "Cap?", "Formation", "P", "Arch", "Pike", "Sw", "Cat", "Cap", "Stack", "Type", "Status" };
            int[]    vColX = { 4, 26, 202, 282, 350, 370, 554, 588, 622, 656, 690, 722, 754, 798, 874 };
            for (int i = 0; i < vCols.Length; i++)
            {
                Label cl = new Label();
                cl.Text = vCols[i];
                cl.Font = new Font("Segoe UI", 6.5f, FontStyle.Bold);
                cl.ForeColor = TextSec;
                cl.AutoSize = true;
                cl.Location = new Point(vColX[i], 2);
                _abmVillageColHeader.Controls.Add(cl);
            }

            // ── Pending column headers ────────────────────────────────────────
            string[] pCols = { "Stack", "Player", "Village", "Target", "Travel", "Send Time", "Arrival", "Formation", "Type", "Status" };
            int[]    pColX = { 6, 38, 152, 276, 340, 422, 494, 566, 650, 722 };
            for (int i = 0; i < pCols.Length; i++)
            {
                Label cl = new Label();
                cl.Text = pCols[i];
                cl.Font = new Font("Segoe UI", 6.5f, FontStyle.Bold);
                cl.ForeColor = TextSec;
                cl.AutoSize = true;
                cl.Location = new Point(pColX[i], 2);
                _abmPendingColHeader.Controls.Add(cl);
            }

            // ── Event wiring ──────────────────────────────────────────────────
            _abmConnectBtn.Click               += delegate { AbmDoConnect(); };
            _abmDisconnectBtn.Click            += delegate { AbmDoDisconnect(); };
            _abmPushConfigBtn.Click            += delegate { AbmDoPushConfig(); };
            _abmPrepareBtn.Click               += delegate { AbmDoPrepare(); };
            _abmLaunchBtn.Click                += delegate { AbmDoLaunch(); };
            _abmCancelBtn.Click                += delegate { AbmDoCancel(); };
            _abmResetBtn.Click                 += delegate { AbmDoReset(); };
            _abmTakeCoordBtn.Click             += delegate { AbmDoTakeCoordinator(); };
            _abmQueueEnabledCheck.CheckedChanged += delegate
            {
                AutoBombMultiSettings s = AbmSettings;
                if (s != null && _abmQueueEnabledCheck.Enabled)
                    s.TargetQueueEnabled = _abmQueueEnabledCheck.Checked;
            };
            _abmPreRefreshCheck.CheckedChanged += delegate
            {
                AutoBombMultiSettings s = AbmSettings;
                if (s != null)
                    s.PreRefreshVillages = _abmPreRefreshCheck.Checked;
            };
            _abmIncludeVassalsCheck.CheckedChanged += delegate
            {
                AutoBombMultiSettings s = AbmSettings;
                if (s != null)
                    s.IncludeVassals = _abmIncludeVassalsCheck.Checked;
            };
            _abmPlayCardsCheck.CheckedChanged += delegate
            {
                AutoBombMultiSettings s = AbmSettings;
                if (s != null)
                    s.PlayCards = _abmPlayCardsCheck.Checked;
            };
            _abmAutoCancelCardCheck.CheckedChanged += delegate
            {
                AutoBombMultiSettings s = AbmSettings;
                if (s != null)
                    s.AutoCancelWrongCard = _abmAutoCancelCardCheck.Checked;
            };
            _abmQueueAddIdBtn.Click             += delegate { AbmQueueAddId(); };
            _abmQueueLookupBtn.Click            += delegate { AbmQueueLookupPlayer(); };
            _abmQueueAddSelectedVillageBtn.Click += delegate { AbmQueueAddSelectedVillage(); };
            _abmQueueAddSelectedPlayerBtn.Click  += delegate { AbmQueueAddSelectedPlayer(); };
            _abmQueueRemoveBtn.Click            += delegate { AbmQueueRemove(); };
            _abmQueueClearBtn.Click             += delegate { AbmQueueClear(); };
            _abmQueueSaveBtn.Click              += delegate { AbmQueueSave(); };
            _abmQueueLoadBtn.Click              += delegate { AbmQueueLoad(); };
            _abmQueueResetBtn.Click             += delegate { AbmQueueReset(); };

            // ── Refresh timer ─────────────────────────────────────────────────
            _abmRefreshTimer = new Timer();
            _abmRefreshTimer.Interval = 1500;
            _abmRefreshTimer.Tick += delegate { try { AbmRefreshDisplay(); } catch { } };
            _abmRefreshTimer.Start();
        }

        // ── Multi-bomb load/save ──────────────────────────────────────────────

        private void AbmLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            AutoBombMultiSettings s = BotEngine.Instance.Settings.AutoBombMulti;

            _abmApiUrlBox.Text       = s.ApiUrl ?? "";
            _abmSessionKeyBox.Text   = s.SessionKey ?? "";
            _abmAutoInterdictCheck.Checked = s.AutoCancelOnInterdict;
            _abmFakeSendCheck.Checked      = s.FakeSendEnabled;
            _abmStackDelayInput.Value = Math.Max(_abmStackDelayInput.Minimum,
                Math.Min(_abmStackDelayInput.Maximum, s.StackDelaySeconds));
            _abmPreRefreshCheck.Checked      = s.PreRefreshVillages;
            _abmIncludeVassalsCheck.Checked  = s.IncludeVassals;
            _abmPlayCardsCheck.Checked       = s.PlayCards;
            _abmAutoCancelCardCheck.Checked  = s.AutoCancelWrongCard;
            _abmQueueEnabledCheck.Checked    = s.TargetQueueEnabled;
            AbmRefreshQueueList(s);
        }

        private void AbmWriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            AutoBombMultiSettings s = BotEngine.Instance.Settings.AutoBombMulti;

            s.ApiUrl               = _abmApiUrlBox.Text.Trim();
            s.SessionKey           = _abmSessionKeyBox.Text;
            s.AutoCancelOnInterdict = _abmAutoInterdictCheck.Checked;
            s.FakeSendEnabled      = _abmFakeSendCheck.Checked;
            s.StackDelaySeconds    = (int)_abmStackDelayInput.Value;
            s.PreRefreshVillages   = _abmPreRefreshCheck.Checked;
            s.IncludeVassals       = _abmIncludeVassalsCheck.Checked;
            s.PlayCards            = _abmPlayCardsCheck.Checked;
            s.AutoCancelWrongCard  = _abmAutoCancelCardCheck.Checked;
            s.TargetQueueEnabled   = _abmQueueEnabledCheck.Checked;
        }

        // ── Multi-bomb per-village setup persistence ─────────────────────────

        private string AbmSetupFilePath()
        {
            string key = _abmSessionKeyBox.Text.Trim();
            if (string.IsNullOrEmpty(key)) key = "default";
            // Sanitise: keep alphanumerics and a few safe chars
            var sb = new System.Text.StringBuilder();
            foreach (char c in key)
                if (char.IsLetterOrDigit(c) || c == '_' || c == '-') sb.Append(c);
                else sb.Append('_');
            string safe = sb.Length > 0 ? sb.ToString() : "default";
            string dir = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SHKBot");
            System.IO.Directory.CreateDirectory(dir);
            return System.IO.Path.Combine(dir, "abm_setup_" + safe + ".txt");
        }

        private void AbmSaveSetup()
        {
            if (_abmVillageRows.Count == 0) return;
            try
            {
                var lines = new System.Text.StringBuilder();
                lines.AppendLine("# ABM Setup " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                foreach (MultiBombVillageRow row in _abmVillageRows)
                {
                    // villageId|formation|cardIndex|useCaptains|stack|attackTypeIndex
                    int attackTypeIndex = 0;
                    int at = row.SelectedAttackType;
                    if (at == 9) attackTypeIndex = 1;
                    else if (at == 1) attackTypeIndex = 2;

                    lines.AppendLine(string.Join("|", new string[]
                    {
                        row.SourceVillageId.ToString(),
                        row.SelectedFormation,
                        row.SelectedCardType.ToString(),
                        row.UseCaptains ? "1" : "0",
                        row.StackOrder.ToString(),
                        attackTypeIndex.ToString()
                    }));
                }
                System.IO.File.WriteAllText(AbmSetupFilePath(), lines.ToString(),
                    System.Text.Encoding.UTF8);
            }
            catch { }
        }

        private void AbmLoadSetup()
        {
            if (_abmVillageRows.Count == 0) return;
            string path = AbmSetupFilePath();
            if (!System.IO.File.Exists(path)) return;
            try
            {
                // Build lookup: villageId → config line parts
                var map = new Dictionary<int, string[]>();
                foreach (string raw in System.IO.File.ReadAllLines(path, System.Text.Encoding.UTF8))
                {
                    string line = raw.Trim();
                    if (line.StartsWith("#") || string.IsNullOrEmpty(line)) continue;
                    string[] parts = line.Split('|');
                    if (parts.Length < 6) continue;
                    int vid;
                    if (int.TryParse(parts[0], out vid))
                        map[vid] = parts;
                }

                foreach (MultiBombVillageRow row in _abmVillageRows)
                {
                    string[] p;
                    if (!map.TryGetValue(row.SourceVillageId, out p)) continue;
                    string formation = p[1];
                    int cardIndex, stack, attackTypeIndex;
                    bool useCaptains;
                    int.TryParse(p[2], out cardIndex);
                    useCaptains = p[3] == "1";
                    int.TryParse(p[4], out stack);
                    int.TryParse(p[5], out attackTypeIndex);
                    row.ApplyConfig(formation, cardIndex, useCaptains, stack, attackTypeIndex);
                }
            }
            catch { }
        }

        // ── Multi-bomb settings/module accessors ──────────────────────────────

        private AutoBombMultiSettings AbmSettings
        {
            get
            {
                if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                    return BotEngine.Instance.Settings.AutoBombMulti;
                return null;
            }
        }

        private AutoBombMultiModule AbmModule
        {
            get
            {
                if (BotEngine.Instance == null) return null;
                foreach (IBotModule m in BotEngine.Instance.Modules)
                {
                    AutoBombMultiModule mm = m as AutoBombMultiModule;
                    if (mm != null) return mm;
                }
                return null;
            }
        }

        // ── Multi-bomb actions ────────────────────────────────────────────────

        private void AbmDoConnect()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null) return;
            s.ApiUrl     = _abmApiUrlBox.Text.Trim();
            s.SessionKey = _abmSessionKeyBox.Text;
            s.AutoCancelOnInterdict = _abmAutoInterdictCheck.Checked;
            s.StackDelaySeconds = (int)_abmStackDelayInput.Value;
            s.FakeSendEnabled = _abmFakeSendCheck.Checked;
            s.PreRefreshVillages = _abmPreRefreshCheck.Checked;
            s.IncludeVassals = _abmIncludeVassalsCheck.Checked;
            s.PlayCards = _abmPlayCardsCheck.Checked;
            s.AutoCancelWrongCard = _abmAutoCancelCardCheck.Checked;

            AutoBombMultiModule mod = AbmModule;
            if (mod != null)
            {
                mod.Enabled = true;
                BotEngine.Instance.Settings.Save();
            }
        }

        private void AbmDoDisconnect()
        {
            AutoBombMultiModule mod = AbmModule;
            if (mod != null) mod.Enabled = false;
        }

        private void AbmDoPushConfig()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null || !s.IsCoordinator) return;

            int targetVid;
            if (!int.TryParse(_abmTargetVidBox.Text.Trim(), out targetVid) || targetVid <= 0)
            {
                MessageBox.Show("Enter a valid target village ID.", "Auto Bomb Multi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var attacks = new List<MultiAttackConfigEntry>();
            foreach (MultiBombVillageRow row in _abmVillageRows)
            {
                if (!row.Selected) continue;

                // Always recalculate travel time fresh from game world using the current
                // target VID — the stored row value may be 0 if the village was registered
                // before a target was set.
                double baseTravel = AutoBombModule.CalculateBaseTravelTime(
                    row.SourceVillageId, targetVid, row.UseCaptains);
                double effectiveTravel = AutoBombModule.ApplyCardSpeed(baseTravel, row.SelectedCardType);

                attacks.Add(new MultiAttackConfigEntry
                {
                    SourcePlayerName  = row.OwnerPlayerName,
                    SourceVillageId   = row.SourceVillageId,
                    ParentVillageId   = row.ParentVillageId,
                    IsVassal          = row.IsVassal,
                    FormationName     = row.SelectedFormation == "None" ? "" : row.SelectedFormation,
                    Stack             = row.StackOrder,
                    CardType          = row.SelectedCardType,
                    CaptainsOnly      = row.UseCaptains,
                    AttackType        = row.SelectedAttackType,
                    TravelTimeSeconds = effectiveTravel,
                    Selected          = true,
                });
            }

            AutoBombMultiModule mod = AbmModule;
            if (mod != null)
            {
                mod.PushAttackConfig(targetVid, attacks);

                // Update TargetVillageId locally so heartbeat re-sends correct travel times
                AutoBombMultiSettings s2 = AbmSettings;
                if (s2 != null) s2.TargetVillageId = targetVid;

                // Update travel time labels in-place — don't rebuild rows (would lose UI selections)
                foreach (MultiBombVillageRow row in _abmVillageRows)
                {
                    double baseTravel = AutoBombModule.CalculateBaseTravelTime(
                        row.SourceVillageId, targetVid, false);
                    row.UpdateTravelTime(baseTravel);
                }

                AbmSaveSetup();
            }
        }

        private void AbmDoPrepare()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null || !s.IsCoordinator)
            {
                MessageBox.Show("Only the coordinator can trigger preparation.", "Auto Bomb Multi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (s.TargetVillageId <= 0)
            {
                MessageBox.Show("Push the attack config first to set a target.", "Auto Bomb Multi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AutoBombMultiModule mod = AbmModule;
            if (mod != null) mod.TriggerPrepare();
        }

        private void AbmDoLaunch()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null || !s.IsCoordinator)
            {
                MessageBox.Show("Only the coordinator can start the timer.", "Auto Bomb Multi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (s.TargetVillageId <= 0)
            {
                MessageBox.Show("Push the attack config first to set a target.", "Auto Bomb Multi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AutoBombMultiModule mod = AbmModule;
            if (mod != null) mod.StartTimer();
        }

        private void AbmDoCancel()
        {
            AutoBombMultiModule mod = AbmModule;
            if (mod != null) mod.CancelAll();
        }

        private void AbmDoReset()
        {
            if (MessageBox.Show("Reset the session? This will clear all connected players and attack configs.",
                "Auto Bomb Multi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            AutoBombMultiModule mod = AbmModule;
            if (mod != null) mod.ResetSession();
        }

        private void AbmDoTakeCoordinator()
        {
            AutoBombMultiModule mod = AbmModule;
            if (mod != null) mod.TakeCoordinator();
        }

        // ── Multi-bomb queue actions ──────────────────────────────────────────

        private void AbmQueueAddId()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null) return;
            int vid = (int)_abmQueueVidInput.Value;
            if (vid <= 0) return;

            TargetQueueEntry entry = new TargetQueueEntry();
            entry.VillageId = vid;
            try
            {
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                {
                    string name = GameEngine.Instance.World.getVillageName(vid);
                    if (!string.IsNullOrEmpty(name)) entry.Label = name;
                }
            }
            catch { }

            s.TargetQueue.Add(entry);
            AbmRefreshQueueList(s);
            _abmQueueVidInput.Value = 0;
        }

        private void AbmQueueLookupPlayer()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null) return;
            string playerName = _abmQueuePlayerNameBox.Text.Trim();
            if (string.IsNullOrEmpty(playerName)) return;

            _abmQueueLookupBtn.Enabled = false;
            _abmQueueLookupBtn.Text = "Looking up...";

            System.Threading.Thread t = new System.Threading.Thread(delegate()
            {
                AutoBombModule abMod = null;
                if (BotEngine.Instance != null)
                    foreach (IBotModule m in BotEngine.Instance.Modules)
                        if (m is AutoBombModule) { abMod = (AutoBombModule)m; break; }

                List<int> villages = abMod != null
                    ? abMod.ResolvePlayerVillages(playerName)
                    : new List<int>();

                this.BeginInvoke((MethodInvoker)delegate
                {
                    AutoBombMultiSettings s2 = AbmSettings;
                    if (s2 != null)
                    {
                        foreach (int vid in villages)
                        {
                            if (GameEngine.Instance != null && GameEngine.Instance.World != null
                                && GameEngine.Instance.World.isCapital(vid)) continue;
                            TargetQueueEntry e = new TargetQueueEntry();
                            e.VillageId = vid;
                            e.Label = playerName;
                            s2.TargetQueue.Add(e);
                        }
                        AbmRefreshQueueList(s2);
                    }
                    _abmQueueLookupBtn.Enabled = true;
                    _abmQueueLookupBtn.Text = "Lookup Player";
                });
            });
            t.IsBackground = true;
            t.Name = "AbmQueueLookup";
            t.Start();
        }

        private void AbmQueueAddSelectedVillage()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null || GameEngine.Instance == null || GameEngine.Instance.World == null) return;
            int vid = GameEngine.Instance.World.LastClickedVillage;
            if (vid <= 0) return;
            TargetQueueEntry entry = new TargetQueueEntry();
            entry.VillageId = vid;
            try
            {
                string name = GameEngine.Instance.World.getVillageName(vid);
                if (!string.IsNullOrEmpty(name)) entry.Label = name;
            }
            catch { }
            s.TargetQueue.Add(entry);
            AbmRefreshQueueList(s);
        }

        private void AbmQueueAddSelectedPlayer()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null || GameEngine.Instance == null || GameEngine.Instance.World == null) return;
            int vid = GameEngine.Instance.World.LastClickedVillage;
            if (vid <= 0) return;
            int userId = GameEngine.Instance.World.getVillageUserID(vid);
            if (userId < 0) return;

            string playerName = null;
            try
            {
                WorldMap.CachedUserInfo info = GameEngine.Instance.World.getStoredUserInfo(userId);
                if (info != null && !string.IsNullOrEmpty(info.userName))
                    playerName = info.userName;
            }
            catch { }

            if (string.IsNullOrEmpty(playerName)) return;
            _abmQueuePlayerNameBox.Text = playerName;
            AbmQueueLookupPlayer();
        }

        private void AbmQueueRemove()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null || _abmQueueListBox.SelectedIndex < 0) return;
            int idx = _abmQueueListBox.SelectedIndex;
            if (idx < s.TargetQueue.Count)
            {
                s.TargetQueue.RemoveAt(idx);
                AbmRefreshQueueList(s);
            }
        }

        private void AbmQueueClear()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null) return;
            s.TargetQueue.Clear();
            AbmRefreshQueueList(s);
        }

        private void AbmQueueSave()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null || s.TargetQueue.Count == 0) return;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Target List (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.DefaultExt = "txt";
            dlg.Title = "Save Target Queue";
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (StreamWriter w = new StreamWriter(dlg.FileName))
                {
                    w.WriteLine("# Auto Bomb Multi Target Queue");
                    foreach (TargetQueueEntry e in s.TargetQueue)
                        w.WriteLine(e.VillageId + "," + e.Label);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save failed: " + ex.Message, "Auto Bomb Multi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbmQueueLoad()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null) return;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Target List (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.Title = "Load Target Queue";
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (StreamReader r = new StreamReader(dlg.FileName))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length == 0 || line.StartsWith("#")) continue;
                        string[] parts = line.Split(new char[] { ',' }, 2);
                        int vid;
                        if (!int.TryParse(parts[0].Trim(), out vid) || vid <= 0) continue;
                        TargetQueueEntry e = new TargetQueueEntry();
                        e.VillageId = vid;
                        if (parts.Length > 1) e.Label = parts[1].Trim();
                        s.TargetQueue.Add(e);
                    }
                }
                AbmRefreshQueueList(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load failed: " + ex.Message, "Auto Bomb Multi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbmQueueReset()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null) return;
            foreach (TargetQueueEntry e in s.TargetQueue) e.Completed = false;
            AbmRefreshQueueList(s);
        }

        private void AbmRefreshQueueList(AutoBombMultiSettings s)
        {
            _abmQueueListBox.Items.Clear();
            if (s == null) return;
            int completed = 0;
            for (int i = 0; i < s.TargetQueue.Count; i++)
            {
                TargetQueueEntry e = s.TargetQueue[i];
                string prefix = e.Completed ? "[done] " : "[ " + (i + 1) + " ] ";
                string label = e.VillageId.ToString();
                if (!string.IsNullOrEmpty(e.Label)) label += "  (" + e.Label + ")";
                _abmQueueListBox.Items.Add(prefix + label);
                if (e.Completed) completed++;
            }
            string status = s.TargetQueue.Count > 0
                ? completed + " / " + s.TargetQueue.Count + " targets completed"
                : "No targets in queue";
            status += "  |  Interdicts: " + s.InterdictCount;
            _abmQueueStatusLabel.Text = status;
        }

        // ── Multi-bomb refresh ────────────────────────────────────────────────

        private void AbmRefreshDisplay()
        {
            AutoBombMultiSettings s = AbmSettings;
            if (s == null) return;

            string stateText = s.SessionState ?? "idle";
            string coordText = "";
            foreach (MultiPlayerInfo pi in s.ConnectedPlayers)
                if (pi.IsCoordinator) { coordText = pi.PlayerName; break; }

            bool isCoord = s.IsCoordinator;
            string statusStr = "Session: " + stateText;
            if (!string.IsNullOrEmpty(coordText)) statusStr += " | Coordinator: " + coordText;
            statusStr += " | Players: " + s.ConnectedPlayers.Count;
            if (s.InterdictDetected) statusStr += " | ⚠ INTERDICT";

            bool modEnabled = AbmModule != null && AbmModule.Enabled;
            _abmConnStatusLabel.Text = statusStr;
            _abmConnStatusLabel.ForeColor = modEnabled
                ? (s.InterdictDetected ? Color.OrangeRed : AccentCol)
                : TextSec;

            bool coordControls = isCoord && modEnabled;
            _abmTargetVidBox.Enabled        = coordControls;
            _abmStackDelayInput.Enabled     = coordControls;
            _abmFakeSendCheck.Enabled       = coordControls;
            _abmAutoInterdictCheck.Enabled  = coordControls;
            _abmPreRefreshCheck.Enabled      = modEnabled;
            _abmIncludeVassalsCheck.Enabled  = modEnabled;
            _abmPlayCardsCheck.Enabled       = modEnabled;
            _abmAutoCancelCardCheck.Enabled  = modEnabled;
            _abmPushConfigBtn.Enabled       = coordControls;
            _abmPrepareBtn.Enabled          = coordControls;
            _abmLaunchBtn.Enabled           = coordControls && (stateText == "configured" || stateText == "prepared" || stateText == "preparing");
            _abmCancelBtn.Enabled           = modEnabled;
            _abmResetBtn.Enabled            = coordControls;
            _abmTakeCoordBtn.Enabled        = modEnabled && !isCoord;

            bool queueCoord = coordControls;
            _abmQueueAddIdBtn.Enabled              = queueCoord;
            _abmQueueLookupBtn.Enabled             = queueCoord;
            _abmQueueAddSelectedVillageBtn.Enabled = queueCoord;
            _abmQueueAddSelectedPlayerBtn.Enabled  = queueCoord;
            _abmQueueRemoveBtn.Enabled             = queueCoord;
            _abmQueueClearBtn.Enabled              = queueCoord;
            _abmQueueSaveBtn.Enabled               = true;
            _abmQueueLoadBtn.Enabled               = queueCoord;
            _abmQueueResetBtn.Enabled              = queueCoord;
            _abmQueueEnabledCheck.Enabled          = queueCoord;
            if (_abmQueueEnabledCheck.Enabled) _abmQueueEnabledCheck.Checked = s.TargetQueueEnabled;

            if (s.TargetVillageId > 0 && string.IsNullOrEmpty(_abmTargetVidBox.Text))
                _abmTargetVidBox.Text = s.TargetVillageId.ToString();

            _abmCoordStatusLabel.Text = "Session: " + stateText + (isCoord ? " [Coord]" : " [Player]");

            AbmRefreshVillageRows(s, isCoord);
            AbmRefreshPendingRows(s);
            AbmRefreshQueueList(s);
        }

        private void AbmRefreshVillageRows(AutoBombMultiSettings settings, bool isCoordinator)
        {
            int totalVillages = 0;
            foreach (MultiPlayerInfo pi in settings.ConnectedPlayers)
                totalVillages += pi.Villages.Count;

            bool coordChanged = (isCoordinator != _abmLastIsCoordinator);
            _abmLastIsCoordinator = isCoordinator;

            if (totalVillages != _abmVillageRows.Count || coordChanged)
                AbmRebuildVillageRows(settings, isCoordinator);
            else
            {
                foreach (MultiBombVillageRow row in _abmVillageRows)
                    foreach (MultiPlayerInfo pi in settings.ConnectedPlayers)
                        foreach (MultiVillageInfo vi in pi.Villages)
                            if (vi.VillageId == row.SourceVillageId)
                                row.SetStatus(vi.AttackStatus);
            }
        }

        private void AbmRebuildVillageRows(AutoBombMultiSettings settings, bool isCoordinator)
        {
            _abmVillageRows.Clear();

            // Dispose old controls before clearing to release GDI handles immediately.
            // With 40+ villages per player this can be 800+ controls; without disposal
            // the handles accumulate and cause lag or crashes.
            _abmVillageListPanel.SuspendLayout();
            foreach (Control c in _abmVillageListPanel.Controls)
                c.Dispose();
            _abmVillageListPanel.Controls.Clear();

            List<string> formationNames = AutoBombModule.GetFormationNames();
            int y = 0, idx = 0;

            string localPlayerName = AutoBombMultiModule.GetLocalPlayerName();

            foreach (MultiPlayerInfo pi in settings.ConnectedPlayers)
            {
                Label playerHeader = new Label();
                playerHeader.BackColor = Color.FromArgb(20, 30, 50);
                playerHeader.ForeColor = pi.IsCoordinator
                    ? Color.FromArgb(100, 200, 255)
                    : Color.FromArgb(180, 200, 230);
                playerHeader.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
                playerHeader.Text = "  " + pi.PlayerName + (pi.IsCoordinator ? " [Coordinator]" : "");
                playerHeader.Location = new Point(0, y);
                playerHeader.Size = new Size(1100, 18);
                _abmVillageListPanel.Controls.Add(playerHeader);
                y += 18;

                bool isLocal = (pi.PlayerName == localPlayerName);

                foreach (MultiVillageInfo vi in pi.Villages)
                {
                    var row = new MultiBombVillageRow(
                        pi.PlayerName, vi.VillageId, vi.VillageName,
                        vi.TravelTimeArmy, vi.TravelTimeCaptain,
                        vi.NumPeasants, vi.NumArchers, vi.NumPikemen,
                        vi.NumSwordsmen, vi.NumCatapults, vi.NumCaptains,
                        formationNames, isLocal, idx, isCoordinator,
                        vi.IsVassal, vi.ParentVillageId);

                    row.Location = new Point(0, y);
                    row.Width = _abmVillageListPanel.Width > 0 ? _abmVillageListPanel.Width : 1100;
                    row.SetStatus(vi.AttackStatus);
                    _abmVillageListPanel.Controls.Add(row);
                    _abmVillageRows.Add(row);
                    y += 24;
                    idx++;
                }
            }

            _abmVillageListPanel.AutoScrollMinSize = new Size(0, y);
            _abmVillageListPanel.ResumeLayout(false);

            // Restore per-village formation/card/stack/type from last saved setup
            AbmLoadSetup();
        }

        private void AbmRefreshPendingRows(AutoBombMultiSettings settings)
        {
            int expected = 0;
            foreach (MultiPlayerInfo pi in settings.ConnectedPlayers)
                foreach (MultiVillageInfo vi in pi.Villages)
                    if (!string.IsNullOrEmpty(vi.AttackStatus)) expected++;

            if (expected != _abmPendingRows.Count)
                AbmRebuildPendingRows(settings);
        }

        private void AbmRebuildPendingRows(AutoBombMultiSettings settings)
        {
            _abmPendingListPanel.SuspendLayout();
            foreach (MultiPendingRow row in _abmPendingRows)
            {
                _abmPendingListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _abmPendingRows.Clear();
            int y = 0, idx = 0;

            foreach (MultiPlayerInfo pi in settings.ConnectedPlayers)
            {
                foreach (MultiVillageInfo vi in pi.Villages)
                {
                    if (string.IsNullOrEmpty(vi.AttackStatus)) continue;

                    var row = new MultiPendingRow(
                        pi.PlayerName, vi.VillageId, vi.VillageName,
                        settings.TargetVillageId,
                        vi.TravelTimeArmy, DateTime.MaxValue, DateTime.MaxValue,
                        vi.FormationName, vi.AttackType, vi.Stack,
                        vi.AttackStatus, idx);

                    row.Location = new Point(0, y);
                    row.Width = _abmPendingListPanel.Width > 0 ? _abmPendingListPanel.Width : 1100;
                    _abmPendingListPanel.Controls.Add(row);
                    _abmPendingRows.Add(row);
                    y += 22;
                    idx++;
                }
            }
            _abmPendingListPanel.AutoScrollMinSize = new Size(0, y);
            _abmPendingListPanel.ResumeLayout(false);
        }

        private void AbQueueAddId()
        {
            int vid = (int)_abQueueVillageIdInput.Value;
            if (vid <= 0) return;

            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;

            TargetQueueEntry entry = new TargetQueueEntry();
            entry.VillageId = vid;

            // Try to get village name for label
            try
            {
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                {
                    string name = GameEngine.Instance.World.getVillageName(vid);
                    if (!string.IsNullOrEmpty(name))
                        entry.Label = name;
                }
            }
            catch { }

            s.TargetQueue.Add(entry);
            AbRefreshTargetQueue();
            _abQueueVillageIdInput.Value = 0;
        }

        private void AbQueueLookupPlayer()
        {
            string playerName = _abQueuePlayerNameInput.Text.Trim();
            if (string.IsNullOrEmpty(playerName))
            {
                BotLogger.Log("Auto Bomb", BotLogLevel.Warning, "Enter a player name first.");
                return;
            }

            _abQueueLookupBtn.Enabled = false;
            _abQueueLookupBtn.Text = "Looking up...";

            System.Threading.Thread lookupThread = new System.Threading.Thread(delegate()
            {
                AutoBombModule module = null;
                foreach (IBotModule m in BotEngine.Instance.Modules)
                {
                    if (m is AutoBombModule)
                    {
                        module = (AutoBombModule)m;
                        break;
                    }
                }

                List<int> villages = new List<int>();
                if (module != null)
                    villages = module.ResolvePlayerVillages(playerName);

                this.BeginInvoke((MethodInvoker)delegate
                {
                    if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                    {
                        AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
                        int skippedCapitals = 0;
                        foreach (int vid in villages)
                        {
                            // Skip capitals (counties, provinces, countries) — only add normal villages
                            if (GameEngine.Instance != null && GameEngine.Instance.World != null &&
                                GameEngine.Instance.World.isCapital(vid))
                            {
                                skippedCapitals++;
                                continue;
                            }
                            TargetQueueEntry entry = new TargetQueueEntry();
                            entry.VillageId = vid;
                            entry.Label = playerName;
                            s.TargetQueue.Add(entry);
                        }
                        AbRefreshTargetQueue();

                        int added = villages.Count - skippedCapitals;
                        if (added > 0)
                        {
                            string msg = "Added " + added + " villages for player '" + playerName + "'.";
                            if (skippedCapitals > 0)
                                msg += " (" + skippedCapitals + " capital(s) skipped)";
                            BotLogger.Log("Auto Bomb", BotLogLevel.Info, msg);
                        }
                        else
                        {
                            BotLogger.Log("Auto Bomb", BotLogLevel.Warning,
                                "No non-capital villages found for player '" + playerName + "'.");
                        }
                    }
                    else
                    {
                        BotLogger.Log("Auto Bomb", BotLogLevel.Warning,
                            "No villages found for player '" + playerName + "'.");
                    }

                    _abQueueLookupBtn.Enabled = true;
                    _abQueueLookupBtn.Text = "Lookup Player";
                });
            });
            lookupThread.IsBackground = true;
            lookupThread.Name = "PlayerLookup";
            lookupThread.Start();
        }

        private void AbQueueRemoveSelected()
        {
            if (_abQueueListBox.SelectedIndex < 0) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
            int idx = _abQueueListBox.SelectedIndex;
            if (idx < s.TargetQueue.Count)
            {
                s.TargetQueue.RemoveAt(idx);
                AbRefreshTargetQueue();
            }
        }

        private void AbQueueClear()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            BotEngine.Instance.Settings.AutoBomb.TargetQueue.Clear();
            AbRefreshTargetQueue();
        }

        private void AbQueueReset()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            foreach (TargetQueueEntry qe in BotEngine.Instance.Settings.AutoBomb.TargetQueue)
                qe.Completed = false;
            AbRefreshTargetQueue();
        }

        private void AbQueueAddSelectedVillage()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            int vid = GameEngine.Instance.World.LastClickedVillage;
            if (vid <= 0)
            {
                BotLogger.Log("Auto Bomb", BotLogLevel.Warning, "Click on a village on the map first.");
                return;
            }

            AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
            TargetQueueEntry entry = new TargetQueueEntry();
            entry.VillageId = vid;

            try
            {
                string name = GameEngine.Instance.World.getVillageName(vid);
                if (!string.IsNullOrEmpty(name))
                    entry.Label = name;
            }
            catch { }

            s.TargetQueue.Add(entry);
            AbRefreshTargetQueue();
            BotLogger.Log("Auto Bomb", BotLogLevel.Info,
                "Added village " + vid + " (" + entry.Label + ") to target queue.");
        }

        private void AbQueueAddSelectedPlayer()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            int vid = GameEngine.Instance.World.LastClickedVillage;
            if (vid <= 0)
            {
                BotLogger.Log("Auto Bomb", BotLogLevel.Warning, "Click on a village on the map first.");
                return;
            }

            int userID = GameEngine.Instance.World.getVillageUserID(vid);
            if (userID < 0)
            {
                BotLogger.Log("Auto Bomb", BotLogLevel.Warning, "Selected village has no owner.");
                return;
            }

            // Try to get player name from cached user info
            string playerName = null;
            try
            {
                WorldMap.CachedUserInfo info = GameEngine.Instance.World.getStoredUserInfo(userID);
                if (info != null && !string.IsNullOrEmpty(info.userName))
                    playerName = info.userName;
            }
            catch { }

            if (string.IsNullOrEmpty(playerName))
            {
                BotLogger.Log("Auto Bomb", BotLogLevel.Warning,
                    "Could not resolve player name for village " + vid +
                    ". Try hovering over the village first to cache the user info, or use the player name lookup instead.");
                return;
            }

            // Use the lookup on a background thread
            _abQueueAddSelectedPlayerBtn.Enabled = false;
            _abQueueAddSelectedPlayerBtn.Text = "Looking up...";
            string capturedName = playerName;

            System.Threading.Thread lookupThread = new System.Threading.Thread(delegate()
            {
                AutoBombModule module = null;
                foreach (IBotModule m in BotEngine.Instance.Modules)
                {
                    if (m is AutoBombModule)
                    {
                        module = (AutoBombModule)m;
                        break;
                    }
                }

                List<int> villages = new List<int>();
                if (module != null)
                    villages = module.ResolvePlayerVillages(capturedName);

                this.BeginInvoke((MethodInvoker)delegate
                {
                    if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                    {
                        AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
                        int skippedCapitals = 0;
                        foreach (int v in villages)
                        {
                            // Skip capitals (counties, provinces, countries) — only add normal villages
                            if (GameEngine.Instance != null && GameEngine.Instance.World != null &&
                                GameEngine.Instance.World.isCapital(v))
                            {
                                skippedCapitals++;
                                continue;
                            }
                            TargetQueueEntry entry = new TargetQueueEntry();
                            entry.VillageId = v;
                            entry.Label = capturedName;
                            s.TargetQueue.Add(entry);
                        }
                        AbRefreshTargetQueue();

                        int added = villages.Count - skippedCapitals;
                        if (added > 0)
                        {
                            string msg = "Added " + added + " villages for player '" + capturedName + "'.";
                            if (skippedCapitals > 0)
                                msg += " (" + skippedCapitals + " capital(s) skipped)";
                            BotLogger.Log("Auto Bomb", BotLogLevel.Info, msg);
                        }
                        else
                        {
                            BotLogger.Log("Auto Bomb", BotLogLevel.Warning,
                                "No non-capital villages found for player '" + capturedName + "'.");
                        }
                    }
                    else
                    {
                        BotLogger.Log("Auto Bomb", BotLogLevel.Warning,
                            "No villages found for player '" + capturedName + "'.");
                    }

                    _abQueueAddSelectedPlayerBtn.Enabled = true;
                    _abQueueAddSelectedPlayerBtn.Text = "Add Selected Player to Queue";
                });
            });
            lookupThread.IsBackground = true;
            lookupThread.Name = "PlayerLookup";
            lookupThread.Start();
        }

        private void AbQueueSaveToFile()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
            if (s.TargetQueue.Count == 0) return;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Target List (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.DefaultExt = "txt";
            dlg.Title = "Save Target Queue";
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (StreamWriter w = new StreamWriter(dlg.FileName))
                {
                    w.WriteLine("# Auto Bomb Target Queue");
                    foreach (TargetQueueEntry entry in s.TargetQueue)
                        w.WriteLine(entry.VillageId + "," + entry.Label);
                }
                BotLogger.Log("Auto Bomb", BotLogLevel.Info,
                    "Saved " + s.TargetQueue.Count + " targets to " + dlg.FileName);
            }
            catch (Exception ex)
            {
                BotLogger.Log("Auto Bomb", BotLogLevel.Error, "Save failed: " + ex.Message);
            }
        }

        private void AbQueueLoadFromFile()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Target List (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.Title = "Load Target Queue";
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
                int count = 0;
                using (StreamReader r = new StreamReader(dlg.FileName))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length == 0 || line.StartsWith("#")) continue;

                        string[] parts = line.Split(new char[] { ',' }, 2);
                        int vid;
                        if (!int.TryParse(parts[0].Trim(), out vid) || vid <= 0) continue;

                        TargetQueueEntry entry = new TargetQueueEntry();
                        entry.VillageId = vid;
                        if (parts.Length > 1)
                            entry.Label = parts[1].Trim();
                        s.TargetQueue.Add(entry);
                        count++;
                    }
                }
                AbRefreshTargetQueue();
                BotLogger.Log("Auto Bomb", BotLogLevel.Info,
                    "Loaded " + count + " targets from " + dlg.FileName);
            }
            catch (Exception ex)
            {
                BotLogger.Log("Auto Bomb", BotLogLevel.Error, "Load failed: " + ex.Message);
            }
        }

        private void AbRefreshTargetQueue()
        {
            _abQueueListBox.Items.Clear();

            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;

            int completed = 0;
            int total = s.TargetQueue.Count;
            for (int i = 0; i < total; i++)
            {
                TargetQueueEntry entry = s.TargetQueue[i];
                string prefix = entry.Completed ? "[done] " : "[ " + (i + 1) + " ] ";
                string label = entry.VillageId.ToString();
                if (!string.IsNullOrEmpty(entry.Label))
                    label += "  (" + entry.Label + ")";
                _abQueueListBox.Items.Add(prefix + label);
                if (entry.Completed) completed++;
            }

            string statusText = total > 0
                ? completed + " / " + total + " targets completed"
                : "No targets in queue";
            statusText += "  |  Interdicts: " + s.InterdictCount;
            _abQueueStatusLabel.Text = statusText;
        }

        private void AbLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
            _abEnabledCheck.Checked = s.Enabled;
            _abTargetInput.Value = Math.Max(_abTargetInput.Minimum,
                Math.Min(_abTargetInput.Maximum, s.TargetVillageId));
            _abAutoCancelCheck.Checked = s.AutoCancelOnInterdict;
            _abFakeSendCheck.Checked = s.FakeSendEnabled;
            _abStackDelayInput.Value = Math.Max(_abStackDelayInput.Minimum,
                Math.Min(_abStackDelayInput.Maximum, s.StackDelaySeconds));

            AbRefreshPendingList();
            _abQueueEnabledCheck.Checked = s.TargetQueueEnabled;
            AbRefreshTargetQueue();
        }

        private void AbWriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
            s.Enabled = _abEnabledCheck.Checked;
            s.TargetVillageId = (int)_abTargetInput.Value;
            s.AutoCancelOnInterdict = _abAutoCancelCheck.Checked;
            s.FakeSendEnabled = _abFakeSendCheck.Checked;
            s.StackDelaySeconds = (int)_abStackDelayInput.Value;
            s.TargetQueueEnabled = _abQueueEnabledCheck.Checked;

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is AutoBombModule)
                    m.Enabled = s.Enabled;
            }
        }

        private void AbLoadArmies()
        {
            int targetId = (int)_abTargetInput.Value;
            if (targetId <= 0)
            {
                BotLogger.Log("Auto Bomb", BotLogLevel.Warning, "Set a target village ID first.");
                return;
            }

            // Save current row configs before clearing
            AbSaveArmyConfigs();

            _abArmyListPanel.SuspendLayout();
            foreach (BombArmyRow row in _abArmyRows)
            {
                _abArmyListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _abArmyRows.Clear();

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
            {
                _abArmyListPanel.ResumeLayout();
                return;
            }

            List<string> formations = AutoBombModule.GetFormationNames();
            List<int> villageIds = GameEngine.Instance.World.getUserVillageIDList();
            if (villageIds == null) { _abArmyListPanel.ResumeLayout(); return; }

            // Build a lookup from saved configs
            Dictionary<int, SavedArmyConfig> savedLookup = new Dictionary<int, SavedArmyConfig>();
            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
            {
                foreach (SavedArmyConfig cfg in BotEngine.Instance.Settings.AutoBomb.SavedConfigs)
                    savedLookup[cfg.SourceVillageId] = cfg;
            }

            int index = 0;
            for (int vi = villageIds.Count - 1; vi >= 0; vi--)
            {
                int vid = villageIds[vi];
                bool isCapital = GameEngine.Instance.World.isCapital(vid);

                if (isCapital && !_abLoadCapitals.Checked) continue;
                if (!isCapital && !_abLoadVillages.Checked) continue;

                VillageMap village = GameEngine.Instance.getVillage(vid);
                if (village == null) continue;

                int peasants = 0, archers = 0, pikemen = 0, swordsmen = 0, captains = 0;
                village.getVillageTroops(ref peasants, ref archers, ref pikemen, ref swordsmen, ref captains);
                int catapults = village.m_numCatapults;

                int totalTroops = peasants + archers + pikemen + swordsmen + catapults + captains;
                if (totalTroops == 0) continue;

                double baseTravelArmy = AutoBombModule.CalculateBaseTravelTime(vid, targetId, false);
                double baseTravelCaptain = AutoBombModule.CalculateBaseTravelTime(vid, targetId, true);
                string villageName = GameEngine.Instance.World.getVillageName(vid);

                BombArmyRow row = new BombArmyRow(vid, targetId, villageName,
                    baseTravelArmy, baseTravelCaptain,
                    peasants, archers, pikemen, swordsmen, catapults, captains,
                    formations, index);
                row.Dock = DockStyle.Top;

                // Restore saved config for this village
                SavedArmyConfig saved;
                if (savedLookup.TryGetValue(vid, out saved))
                    row.ApplySavedConfig(saved);

                _abArmyListPanel.Controls.Add(row);
                _abArmyRows.Add(row);
                index++;
            }

            _abArmyListPanel.ResumeLayout();
            BotLogger.Log("Auto Bomb", BotLogLevel.Info,
                "Loaded " + _abArmyRows.Count + " armies targeting village " + targetId +
                " (" + savedLookup.Count + " configs restored).");
        }

        private void AbSaveArmyConfigs()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;
            if (_abArmyRows.Count == 0)
                return;

            AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
            // Update existing configs and add new ones (keyed by source village)
            Dictionary<int, SavedArmyConfig> lookup = new Dictionary<int, SavedArmyConfig>();
            foreach (SavedArmyConfig existing in s.SavedConfigs)
                lookup[existing.SourceVillageId] = existing;

            foreach (BombArmyRow row in _abArmyRows)
            {
                SavedArmyConfig cfg = row.ToSavedConfig();
                lookup[cfg.SourceVillageId] = cfg;
            }

            s.SavedConfigs = new List<SavedArmyConfig>(lookup.Values);
        }

        private void AbSetAllSelected(bool selected)
        {
            foreach (BombArmyRow row in _abArmyRows)
                row.Selected = selected;
        }

        private void AbSubmitToQueue()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            int targetId = (int)_abTargetInput.Value;
            if (targetId <= 0)
            {
                BotLogger.Log("Auto Bomb", BotLogLevel.Warning, "Set a target village ID first.");
                return;
            }

            AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
            s.TargetVillageId = targetId;
            s.PendingAttacks.Clear();

            int count = 0;
            foreach (BombArmyRow row in _abArmyRows)
            {
                if (!row.Selected) continue;

                BombAttackEntry entry = new BombAttackEntry();
                entry.SourceVillageId = row.SourceVillageId;
                entry.TargetVillageId = targetId;
                entry.TravelTimeSeconds = row.EffectiveTravelTime;
                entry.AttackType = row.SelectedAttackType;
                entry.FormationName = row.SelectedFormation == "None" ? "" : row.SelectedFormation;
                entry.Stack = row.StackOrder;
                entry.NumPeasants = row.NumPeasants;
                entry.NumArchers = row.NumArchers;
                entry.NumPikemen = row.NumPikemen;
                entry.NumSwordsmen = row.NumSwordsmen;
                entry.NumCatapults = row.NumCatapults;
                entry.NumCaptains = row.NumCaptains;
                entry.CaptainsOnly = row.UseCaptains;
                entry.CardType = row.SelectedCardType;
                entry.Status = "Queued";
                s.PendingAttacks.Add(entry);
                count++;
            }

            BotLogger.Log("Auto Bomb", BotLogLevel.Info,
                "Submitted " + count + " attack(s) to queue. Switch to Pending Attacks tab to launch.");

            AbRefreshPendingList();
            _bombSubTabs.SelectedTab = _bombPendingTab;
        }

        private void AbLaunch()
        {
            AbSaveArmyConfigs();
            AbWriteToSettings();

            // Ensure bot engine is enabled so ticks fire
            if (BotEngine.Instance.Settings != null)
                BotEngine.Instance.Settings.BotEnabled = true;

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                AutoBombModule bomb = m as AutoBombModule;
                if (bomb != null)
                {
                    // Force-enable the module for the launch sequence
                    m.Enabled = true;
                    bomb.StartLaunch();
                    return;
                }
            }
        }

        private void AbCancelAll()
        {
            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                AutoBombModule bomb = m as AutoBombModule;
                if (bomb != null)
                {
                    bomb.CancelAll();
                    AbRefreshPendingList();
                    return;
                }
            }
        }

        private void AbClearQueue()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            BotEngine.Instance.Settings.AutoBomb.PendingAttacks.Clear();
            AbRefreshPendingList();
            BotLogger.Log("Auto Bomb", BotLogLevel.Info, "Queue cleared.");
        }

        private void AbRefreshPendingList()
        {
            _abPendingListPanel.SuspendLayout();
            foreach (PendingBombRow row in _abPendingRows)
            {
                _abPendingListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _abPendingRows.Clear();

            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
            {
                _abPendingListPanel.ResumeLayout();
                return;
            }

            List<BombAttackEntry> attacks = BotEngine.Instance.Settings.AutoBomb.PendingAttacks;
            // Sort by stack for display
            List<BombAttackEntry> sorted = new List<BombAttackEntry>(attacks);
            sorted.Sort(delegate(BombAttackEntry a, BombAttackEntry b) { return a.Stack.CompareTo(b.Stack); });

            for (int i = sorted.Count - 1; i >= 0; i--)
            {
                PendingBombRow row = new PendingBombRow(sorted[i], i);
                row.Dock = DockStyle.Top;
                _abPendingListPanel.Controls.Add(row);
                _abPendingRows.Add(row);
            }

            _abPendingListPanel.ResumeLayout();
        }

        private void AbUpdateDisplay()
        {
            if (_abEnabledCheck == null) return;

            // Update status label
            bool isLaunching = false;
            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                AutoBombModule bomb = m as AutoBombModule;
                if (bomb != null)
                {
                    isLaunching = bomb.IsLaunching;
                    break;
                }
            }

            if (isLaunching)
            {
                _abStatusLabel.Text = "LAUNCHING";
                _abStatusLabel.ForeColor = Color.FromArgb(255, 120, 50);
            }
            else
            {
                bool enabled = _abEnabledCheck.Checked;
                _abStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
                _abStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
            }

            // Refresh pending row statuses
            foreach (PendingBombRow row in _abPendingRows)
                row.RefreshStatus();

            // Update queue status label (completed/interdict counts) from live settings
            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
            {
                AutoBombSettings s = BotEngine.Instance.Settings.AutoBomb;
                int completed = 0;
                int total = s.TargetQueue.Count;
                foreach (TargetQueueEntry qe in s.TargetQueue)
                {
                    if (qe.Completed) completed++;
                }

                string statusText = total > 0
                    ? completed + " / " + total + " targets completed"
                    : "No targets in queue";
                statusText += "  |  Interdicts: " + s.InterdictCount;
                _abQueueStatusLabel.Text = statusText;

                // Refresh listbox items if completed count changed
                // (check by comparing label text to avoid full rebuild every second)
                int listCompleted = 0;
                foreach (object item in _abQueueListBox.Items)
                {
                    if (item.ToString().StartsWith("[done]")) listCompleted++;
                }
                if (listCompleted != completed)
                    AbRefreshTargetQueue();
            }
        }

        // =====================================================================
        // Misc tab
        // =====================================================================

        private void WireUpMiscTab()
        {
            _miscCollectFreeCardsCheck.CheckedChanged += delegate { MiscWriteToSettings(); };
            _miscDisableCannotPlayCardCheck.CheckedChanged += delegate { MiscWriteToSettings(); };
            _miscShowOtherTraderInfoCheck.CheckedChanged += delegate { MiscWriteToSettings(); };
            _miscWorldMapParishBuildingCountCheck.CheckedChanged += delegate { MiscWriteToSettings(); };
            _miscShowUserScreenInfoCheck.CheckedChanged += delegate { MiscWriteToSettings(); };
            _miscMapAttackTypeIconsCheck.CheckedChanged += delegate { MiscWriteToSettings(); };
            MiscRefreshSaleInfo();
        }

        private void MiscLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;
            _miscLoading = true;
            try
            {
                MiscSettings s = BotEngine.Instance.Settings.Misc;
                _miscCollectFreeCardsCheck.Checked = s.CollectFreeCards;
                _miscDisableCannotPlayCardCheck.Checked = s.DisableCannotPlayCardPopup;
                _miscShowOtherTraderInfoCheck.Checked = s.ShowOtherTraderInfo;
                _miscWorldMapParishBuildingCountCheck.Checked = s.WorldMapParishBuildingCount;
                _miscShowUserScreenInfoCheck.Checked = s.ShowUserScreenInfo;
                _miscMapAttackTypeIconsCheck.Checked = s.MapAttackTypeIcons;
            }
            finally { _miscLoading = false; }
        }

        private void MiscWriteToSettings()
        {
            if (_miscLoading) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;
            MiscSettings s = BotEngine.Instance.Settings.Misc;
            s.CollectFreeCards = _miscCollectFreeCardsCheck.Checked;
            s.DisableCannotPlayCardPopup = _miscDisableCannotPlayCardCheck.Checked;
            s.ShowOtherTraderInfo = _miscShowOtherTraderInfoCheck.Checked;
            s.WorldMapParishBuildingCount = _miscWorldMapParishBuildingCountCheck.Checked;
            s.ShowUserScreenInfo = _miscShowUserScreenInfoCheck.Checked;
            s.MapAttackTypeIcons = _miscMapAttackTypeIconsCheck.Checked;
            // Live only — consistent with every other tab. Persistence happens via the
            // Save Settings button; Load Settings reverts to the last saved snapshot.
        }

        private void MiscRefreshSaleInfo()
        {
            var world = GameEngine.Instance?.World;
            if (world == null)
            {
                _miscSalePctValue.Text = "N/A";
                _miscSaleStartValue.Text = "N/A (not logged in)";
                _miscSaleEndValue.Text = "N/A (not logged in)";
                return;
            }
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            _miscSalePctValue.Text = world.salePercentage + "%";
            _miscSaleStartValue.Text = epoch.AddSeconds(world.saleStartTime).ToLocalTime().ToString();
            _miscSaleEndValue.Text = epoch.AddSeconds(world.saleEndTime).ToLocalTime().ToString();
        }

        // =====================================================================
        // Popularity tab
        // =====================================================================

        private void WireUpPopularityTab()
        {
            // Wire events to Designer-defined controls
            _ppEnabledCheck.CheckedChanged += delegate { PpWriteToSettings(); };
            _ppIntervalInput.ValueChanged += delegate { PpWriteToSettings(); };
            _ppDelayInput.ValueChanged += delegate { PpWriteToSettings(); };
            _ppRefreshBtn.Click += delegate { PpPopulateVillageList(); };
            _ppRunNowBtn.Click += delegate { PpRunNow(); };
            _ppCopySettingsBtn.Click += delegate { PpCopySettingsClick(); };

            // Add column header labels dynamically
            string[] colNames = { "Village", "Mode" };
            int[] colXs = { 8, 220 };
            for (int i = 0; i < colNames.Length; i++)
            {
                Label cl = new Label();
                cl.Text = colNames[i];
                cl.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
                cl.ForeColor = TextSec;
                cl.AutoSize = true;
                cl.Location = new Point(colXs[i], 4);
                _ppColHeader.Controls.Add(cl);
            }

            _ppRefreshTimer = new Timer();
            _ppRefreshTimer.Interval = 2000;
            _ppRefreshTimer.Tick += delegate { try { PpUpdateStatusDisplay(); } catch { } };
            _ppRefreshTimer.Start();
        }

        private void PpLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;
            _ppLoading = true;
            try
            {
                PopularitySettings s = BotEngine.Instance.Settings.Popularity;
                _ppEnabledCheck.Checked = s.Enabled;
                _ppIntervalInput.Value = Math.Max(_ppIntervalInput.Minimum,
                    Math.Min(_ppIntervalInput.Maximum, s.CycleIntervalSeconds));
                _ppDelayInput.Value = Math.Max(_ppDelayInput.Minimum,
                    Math.Min(_ppDelayInput.Maximum, s.DelayBetweenVillagesMs));

                PpPopulateVillageList();
            }
            finally { _ppLoading = false; }
        }

        private void PpWriteToSettings()
        {
            if (_ppLoading) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            PopularitySettings s = BotEngine.Instance.Settings.Popularity;
            s.Enabled = _ppEnabledCheck.Checked;
            s.CycleIntervalSeconds = (int)_ppIntervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_ppDelayInput.Value;

            foreach (IBotModule module in BotEngine.Instance.Modules)
            {
                if (module is Modules.PopularityModule)
                    module.Enabled = s.Enabled;
            }

            PpUpdateStatusDisplay();
        }

        private void PpPopulateVillageList()
        {
            _ppVillageListPanel.SuspendLayout();
            foreach (PopularityVillageRow row in _ppVillageRows)
            {
                _ppVillageListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _ppVillageRows.Clear();

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
            {
                _ppVillageListPanel.ResumeLayout();
                return;
            }

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null)
            {
                _ppVillageListPanel.ResumeLayout();
                return;
            }

            PopularitySettings settings = (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                ? BotEngine.Instance.Settings.Popularity
                : null;

            int y = 2;
            foreach (WorldMap.UserVillageData uvd in villages)
            {
                if (GameEngine.Instance.World.isCapital(uvd.villageID))
                    continue;

                VillagePopularitySettings vs = settings != null
                    ? settings.GetVillageSettings(uvd.villageID)
                    : new VillagePopularitySettings { VillageId = uvd.villageID };

                PopularityVillageRow row = new PopularityVillageRow(uvd, vs);
                row.Location = new Point(0, y);
                row.Width = _ppVillageListPanel.ClientSize.Width;
                row.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                row.ModeChanged += PpOnVillageModeChanged;
                _ppVillageListPanel.Controls.Add(row);
                _ppVillageRows.Add(row);
                y += row.Height + 1;
            }

            _ppVillageListPanel.ResumeLayout();
        }

        private void PpOnVillageModeChanged(int villageId, PopularityMode mode)
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            PopularitySettings s = BotEngine.Instance.Settings.Popularity;
            VillagePopularitySettings vs = s.GetVillageSettings(villageId);
            vs.Mode = mode;
        }

        private void PpRunNow()
        {
            if (BotEngine.Instance == null) return;
            foreach (IBotModule module in BotEngine.Instance.Modules)
            {
                Modules.PopularityModule pm = module as Modules.PopularityModule;
                if (pm != null)
                {
                    pm.RunNow();
                    break;
                }
            }
        }

        private void PpCopySettingsClick()
        {
            CopyPopularitySettingsForm form = new CopyPopularitySettingsForm();
            form.ShowDialog(this);
            if (form.Copied)
                PpPopulateVillageList();
        }

        private void PpUpdateStatusDisplay()
        {
            bool enabled = _ppEnabledCheck.Checked;
            _ppStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _ppStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
        }

        // =====================================================================
        // Banquet tab
        // =====================================================================

        private void WireUpBanquetTab()
        {
            _bqEnabledCheck.CheckedChanged += delegate { BqWriteToSettings(); };
            _bqIntervalInput.ValueChanged += delegate { BqWriteToSettings(); };
            _bqDelayInput.ValueChanged += delegate { BqWriteToSettings(); };
            _bqRefreshBtn.Click += delegate { BqPopulateVillageList(); };
            _bqRunNowBtn.Click += delegate { BqRunNow(); };
            _bqCopySettingsBtn.Click += delegate { BqCopySettingsClick(); };

            int[] colXs = { 8, 212, 302, 392, 482, 572, 660, 750, 838 };
            string[] colNames = { "Village" };
            Label vl = new Label();
            vl.Text = colNames[0];
            vl.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            vl.ForeColor = TextSec;
            vl.AutoSize = true;
            vl.Location = new Point(colXs[0], 4);
            _bqColHeader.Controls.Add(vl);

            for (int i = 0; i < Modules.BanquetModule.GoodNames.Length; i++)
            {
                Label cl = new Label();
                cl.Text = Modules.BanquetModule.GoodNames[i];
                cl.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
                cl.ForeColor = TextSec;
                cl.AutoSize = true;
                cl.Location = new Point(colXs[i + 1], 4);
                _bqColHeader.Controls.Add(cl);
            }
        }

        private void BqLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            _bqLoading = true;
            try
            {
                BanquetSettings s = BotEngine.Instance.Settings.Banquet;
                _bqEnabledCheck.Checked = s.Enabled;
                _bqIntervalInput.Value = Math.Max(_bqIntervalInput.Minimum,
                    Math.Min(_bqIntervalInput.Maximum, s.CycleIntervalSeconds));
                _bqDelayInput.Value = Math.Max(_bqDelayInput.Minimum,
                    Math.Min(_bqDelayInput.Maximum, s.DelayBetweenVillagesMs));

                BqPopulateVillageList();
            }
            finally { _bqLoading = false; }
        }

        private void BqWriteToSettings()
        {
            if (_bqLoading) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            BanquetSettings s = BotEngine.Instance.Settings.Banquet;
            s.Enabled = _bqEnabledCheck.Checked;
            s.CycleIntervalSeconds = (int)_bqIntervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_bqDelayInput.Value;

            foreach (IBotModule module in BotEngine.Instance.Modules)
            {
                if (module is Modules.BanquetModule)
                    module.Enabled = s.Enabled;
            }

            BqUpdateStatusDisplay();
        }

        private void BqPopulateVillageList()
        {
            _bqVillageListPanel.SuspendLayout();
            foreach (BanquetVillageRow row in _bqVillageRows)
            {
                _bqVillageListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _bqVillageRows.Clear();

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
            {
                _bqVillageListPanel.ResumeLayout();
                return;
            }

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null)
            {
                _bqVillageListPanel.ResumeLayout();
                return;
            }

            BanquetSettings settings = (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                ? BotEngine.Instance.Settings.Banquet
                : null;

            int researchLevel = 0;
            try { researchLevel = (int)GameEngine.Instance.World.UserResearchData.Research_Craftsmanship; } catch { }

            int y = 2;
            bool alternate = false;
            foreach (WorldMap.UserVillageData uvd in villages)
            {
                // Only show regular villages — banquets cannot be held in parish/county/etc. capitals
                if (GameEngine.Instance.World.isCapital(uvd.villageID)) continue;

                VillageBanquetSettings vs = settings != null
                    ? settings.GetVillageSettings(uvd.villageID)
                    : new VillageBanquetSettings { VillageId = uvd.villageID };

                BanquetVillageRow row = new BanquetVillageRow(uvd, vs, researchLevel, alternate);
                row.Location = new Point(0, y);
                row.Width = _bqVillageListPanel.ClientSize.Width;
                row.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                row.GoodToggled += BqOnGoodToggled;
                _bqVillageListPanel.Controls.Add(row);
                _bqVillageRows.Add(row);
                y += row.Height + 1;
                alternate = !alternate;
            }

            _bqVillageListPanel.ResumeLayout();
        }

        private void BqOnGoodToggled(int villageId, int goodIdx, bool enabled)
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            VillageBanquetSettings vs = BotEngine.Instance.Settings.Banquet.GetVillageSettings(villageId);
            if (enabled)
            {
                if (!vs.EnabledGoods.Contains(goodIdx))
                    vs.EnabledGoods.Add(goodIdx);
            }
            else
            {
                vs.EnabledGoods.Remove(goodIdx);
            }
        }

        private void BqCopySettingsClick()
        {
            CopyBanquetSettingsForm form = new CopyBanquetSettingsForm();
            form.ShowDialog(this);
            if (form.Copied)
                BqPopulateVillageList();
        }

        private void BqRunNow()
        {
            if (BotEngine.Instance == null) return;
            foreach (IBotModule module in BotEngine.Instance.Modules)
            {
                Modules.BanquetModule bm = module as Modules.BanquetModule;
                if (bm != null) { bm.RunNow(); break; }
            }
        }

        private void BqUpdateStatusDisplay()
        {
            bool enabled = _bqEnabledCheck.Checked;
            _bqStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _bqStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
        }

        // =====================================================================
        // Auto tab
        // =====================================================================

        private void WireUpAutoTab()
        {
            BuildAutoTabUI();

            _autoRefreshTimer = new Timer();
            _autoRefreshTimer.Interval = 30000;
            _autoRefreshTimer.Tick += delegate { try { AutoUpdateServerTime(); } catch { } };
            _autoRefreshTimer.Start();
        }

        private void BuildAutoTabUI()
        {
            // The inner tab control and the Production / Modules sub-tab pages are declared in the
            // Designer (_autoInnerTabs / _autoProdTab / _autoModuleTab). Here we only populate their
            // data-driven content (settings sections, headers and the catalog/module-driven rows).
            _autoPage.SuspendLayout();

            BuildProductionSubTab();
            // Reset scroll to top when the tab is entered. BeginInvoke defers it until AFTER
            // WinForms' focus-into-view pass, which is what otherwise pushes the first row off.
            _autoProdTab.Enter += delegate { AutoResetScroll(_autoProdScrollPanel); };

            BuildModulesSubTab();
            _autoModuleTab.Enter += delegate { AutoResetScroll(_autoModuleScrollPanel); };

            _autoPage.ResumeLayout(false);
        }

        // Resets a scrollable rows panel back to the top. Deferred via BeginInvoke so it runs after
        // WinForms scrolls a freshly-focused child into view (which is what hides the first row).
        private void AutoResetScroll(Panel scrollPanel)
        {
            if (scrollPanel == null) return;
            if (IsHandleCreated)
                BeginInvoke((Action)(delegate
                {
                    if (scrollPanel != null)
                        scrollPanel.AutoScrollPosition = new System.Drawing.Point(0, 0);
                }));
            else
                scrollPanel.AutoScrollPosition = new System.Drawing.Point(0, 0);
        }

        private void BuildProductionSubTab()
        {
            // The settings/header/scroll panels, the interval input and all captions are declared in
            // the Designer. Here we only wire the interval input and build the catalog-driven rows.
            _autoCardIntervalInput.ValueChanged += delegate { if (!_autoLoading) AutoWriteToSettings(); };

            // Build rows
            _autoProdRows.Clear();
            int y = 4;
            string lastSection = null;
            foreach (ProductionGoodDef def in ProductionCardCatalog.Goods)
            {
                if (def.Section != lastSection)
                {
                    lastSection = def.Section;
                    Label sectionLabel = new Label();
                    sectionLabel.Text = "── " + def.Section + " ──";
                    sectionLabel.Location = new Point(6, y + 4);
                    sectionLabel.AutoSize = true;
                    sectionLabel.ForeColor = AccentCol;
                    sectionLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
                    _autoProdScrollPanel.Controls.Add(sectionLabel);
                    y += 22;
                }

                AutoProdRow row = BuildProdRow(def, y);
                _autoProdScrollPanel.Controls.Add(row.RowPanel);
                _autoProdRows.Add(row);
                y += 28;
            }
        }

        private AutoProdRow BuildProdRow(ProductionGoodDef def, int y)
        {
            AutoProdRow row = new AutoProdRow();
            row.GoodKey = def.GoodKey;

            Panel panel = new Panel();
            panel.Location = new Point(0, y);
            panel.Size = new Size(1100, 26);
            panel.BackColor = Color.FromArgb(30, 30, 42);
            row.RowPanel = panel;

            // Enabled checkbox — persist + apply immediately on toggle (other tabs behave the same)
            CheckBox enabled = new CheckBox();
            enabled.Location = new Point(6, 4);
            enabled.Size = new Size(18, 18);
            enabled.BackColor = Color.Transparent;
            row.EnabledCheck = enabled;
            enabled.CheckedChanged += delegate { if (!_autoLoading) AutoWriteToSettings(); };
            panel.Controls.Add(enabled);

            // Good name
            Label nameLabel = new Label();
            nameLabel.Text = def.GoodKey;
            nameLabel.Location = new Point(28, 5);
            nameLabel.Size = new Size(80, 16);
            nameLabel.ForeColor = TextPri;
            nameLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            panel.Controls.Add(nameLabel);

            // Tier dropdown
            ComboBox tierCombo = new ComboBox();
            tierCombo.Location = new Point(115, 3);
            tierCombo.Size = new Size(88, 20);
            tierCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            tierCombo.BackColor = Color.FromArgb(40, 40, 55);
            tierCombo.ForeColor = TextPri;
            tierCombo.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            foreach (string t in def.Tiers) tierCombo.Items.Add(t);
            if (tierCombo.Items.Count > 0) tierCombo.SelectedIndex = 0;
            row.TierCombo = tierCombo;
            tierCombo.SelectedIndexChanged += delegate { if (!_autoLoading) AutoWriteToSettings(); };
            panel.Controls.Add(tierCombo);

            // Target count
            Label targetLbl = new Label();
            targetLbl.Text = "Target:";
            targetLbl.Location = new Point(213, 5);
            targetLbl.AutoSize = true;
            targetLbl.ForeColor = TextSec;
            targetLbl.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            panel.Controls.Add(targetLbl);

            NumericUpDown targetInput = new NumericUpDown();
            targetInput.Location = new Point(258, 3);
            targetInput.Size = new Size(48, 20);
            targetInput.Minimum = 1;
            targetInput.Maximum = 100;
            targetInput.Value = 1;
            targetInput.BackColor = Color.FromArgb(40, 40, 55);
            targetInput.ForeColor = TextPri;
            targetInput.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            row.TargetInput = targetInput;
            targetInput.ValueChanged += delegate { if (!_autoLoading) AutoWriteToSettings(); };
            panel.Controls.Add(targetInput);

            // Delay
            Label delayLbl = new Label();
            delayLbl.Text = "Start in:";
            delayLbl.Location = new Point(316, 5);
            delayLbl.AutoSize = true;
            delayLbl.ForeColor = TextSec;
            delayLbl.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            panel.Controls.Add(delayLbl);

            NumericUpDown delayH = new NumericUpDown();
            delayH.Location = new Point(365, 3);
            delayH.Size = new Size(40, 20);
            delayH.Minimum = 0;
            delayH.Maximum = 72;
            delayH.Value = 0;
            delayH.BackColor = Color.FromArgb(40, 40, 55);
            delayH.ForeColor = TextPri;
            delayH.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            row.DelayHInput = delayH;
            delayH.ValueChanged += delegate { if (!_autoLoading) AutoWriteToSettings(); };
            panel.Controls.Add(delayH);

            Label hLbl = new Label();
            hLbl.Text = "h";
            hLbl.Location = new Point(408, 5);
            hLbl.AutoSize = true;
            hLbl.ForeColor = TextSec;
            hLbl.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            panel.Controls.Add(hLbl);

            NumericUpDown delayM = new NumericUpDown();
            delayM.Location = new Point(420, 3);
            delayM.Size = new Size(40, 20);
            delayM.Minimum = 0;
            delayM.Maximum = 59;
            delayM.Value = 0;
            delayM.BackColor = Color.FromArgb(40, 40, 55);
            delayM.ForeColor = TextPri;
            delayM.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            row.DelayMInput = delayM;
            delayM.ValueChanged += delegate { if (!_autoLoading) AutoWriteToSettings(); };
            panel.Controls.Add(delayM);

            Label mLbl = new Label();
            mLbl.Text = "m";
            mLbl.Location = new Point(463, 5);
            mLbl.AutoSize = true;
            mLbl.ForeColor = TextSec;
            mLbl.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            panel.Controls.Add(mLbl);

            // Progress
            Label progressLabel = new Label();
            progressLabel.Text = "0/1";
            progressLabel.Location = new Point(480, 5);
            progressLabel.Size = new Size(60, 16);
            progressLabel.ForeColor = TextSec;
            progressLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            row.ProgressLabel = progressLabel;
            panel.Controls.Add(progressLabel);

            // Reset button
            Button resetBtn = new Button();
            resetBtn.Text = "Reset";
            resetBtn.Location = new Point(550, 3);
            resetBtn.Size = new Size(48, 20);
            resetBtn.FlatStyle = FlatStyle.Flat;
            resetBtn.BackColor = Color.FromArgb(45, 35, 35);
            resetBtn.ForeColor = TextSec;
            resetBtn.Font = new System.Drawing.Font("Segoe UI", 7F);
            resetBtn.FlatAppearance.BorderColor = Color.FromArgb(80, 50, 50);
            AutoProdRow capturedRow = row;
            resetBtn.Click += delegate { AutoResetProgress(capturedRow.GoodKey); };
            panel.Controls.Add(resetBtn);

            return row;
        }

        private void BuildModulesSubTab()
        {
            // Panels, interval input, server-time label and the named column captions are Designer
            // controls. Here we wire the interval input, populate the 24 hour labels and build rows.
            _autoModuleIntervalInput.ValueChanged += delegate { if (!_autoLoading) AutoWriteToSettings(); };
            AutoUpdateServerTime();

            // Hour labels (two rows: 0-11 top, 12-23 bottom) — generated, so kept in code
            for (int h = 0; h < 12; h++)
            {
                Label lbl = new Label();
                lbl.Text = h.ToString("00");
                lbl.Location = new Point(120 + h * 32, 4);
                lbl.Size = new Size(30, 12);
                lbl.ForeColor = TextSec;
                lbl.Font = new System.Drawing.Font("Segoe UI", 6.5F);
                lbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                _autoModuleHeaderPanel.Controls.Add(lbl);
            }
            for (int h = 12; h < 24; h++)
            {
                Label lbl = new Label();
                lbl.Text = h.ToString("00");
                lbl.Location = new Point(120 + (h - 12) * 32, 20);
                lbl.Size = new Size(30, 12);
                lbl.ForeColor = TextSec;
                lbl.Font = new System.Drawing.Font("Segoe UI", 6.5F);
                lbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                _autoModuleHeaderPanel.Controls.Add(lbl);
            }

            // Build module rows
            _autoModuleRows.Clear();
            string[] moduleNames  = { "Trade", "Recruiting", "VillageBuilder", "CastleRepair", "Popularity", "Scout" };
            string[] moduleLabels = { "Trade", "Recruiting", "Village Builder", "Castle Repair", "Popularity", "Scout" };
            int y = 4;
            for (int i = 0; i < moduleNames.Length; i++)
            {
                AutoModuleRow row = BuildModuleRow(moduleNames[i], moduleLabels[i], y);
                _autoModuleScrollPanel.Controls.Add(row.RowPanel);
                _autoModuleRows.Add(row);
                y += 70;   // 62px row + 8px gap
            }
        }

        private AutoModuleRow BuildModuleRow(string moduleName, string displayName, int y)
        {
            AutoModuleRow row = new AutoModuleRow();
            row.ModuleName = moduleName;

            Panel panel = new Panel();
            panel.Location = new Point(0, y);
            panel.Size = new Size(1100, 62);   // taller row to show ~3 card items
            panel.BackColor = Color.FromArgb(30, 30, 42);
            row.RowPanel = panel;

            // Module name
            Label nameLabel = new Label();
            nameLabel.Text = displayName;
            nameLabel.Location = new Point(8, 14);
            nameLabel.Size = new Size(105, 16);
            nameLabel.ForeColor = TextPri;
            nameLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            panel.Controls.Add(nameLabel);

            // Hour checkboxes — two rows of 12 (0-11 top, 12-23 bottom)
            for (int h = 0; h < 24; h++)
            {
                CheckBox cb = new CheckBox();
                int col = h < 12 ? h : h - 12;
                int rowY = h < 12 ? 3 : 24;
                cb.Location = new Point(120 + col * 32, rowY);
                cb.Size = new Size(20, 18);
                cb.BackColor = Color.Transparent;
                row.HourChecks[h] = cb;
                panel.Controls.Add(cb);
            }

            // Multi-select CheckedListBox — any number of cards from inventory
            CheckedListBox clb = new CheckedListBox();
            clb.Location = new Point(510, 4);
            clb.Size = new Size(280, 54);
            clb.BackColor = Color.FromArgb(40, 40, 55);
            clb.ForeColor = TextPri;
            clb.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            clb.CheckOnClick = true;
            clb.BorderStyle = BorderStyle.FixedSingle;
            row.CardsListBox = clb;
            // ItemCheck fires before CheckedItems updates, so defer the write until the new state settles
            clb.ItemCheck += delegate
            {
                if (_autoLoading) return;
                if (IsHandleCreated)
                    BeginInvoke((Action)(delegate { if (!_autoLoading) AutoWriteToSettings(); }));
            };
            panel.Controls.Add(clb);

            // Re-play card on expiry checkbox
            CheckBox replayCard = new CheckBox();
            replayCard.Text = "Re-play";
            replayCard.Location = new Point(810, 20);
            replayCard.Size = new Size(78, 18);
            replayCard.BackColor = Color.Transparent;
            replayCard.ForeColor = TextPri;
            replayCard.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            replayCard.Checked = true;
            row.ReplayCardCheck = replayCard;
            replayCard.CheckedChanged += delegate { if (!_autoLoading) AutoWriteToSettings(); };
            panel.Controls.Add(replayCard);

            // Auto-disable checkbox
            CheckBox autoOff = new CheckBox();
            autoOff.Text = "Auto-disable";
            autoOff.Location = new Point(900, 20);
            autoOff.Size = new Size(95, 18);
            autoOff.BackColor = Color.Transparent;
            autoOff.ForeColor = TextPri;
            autoOff.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            row.AutoDisableCheck = autoOff;
            autoOff.CheckedChanged += delegate { if (!_autoLoading) AutoWriteToSettings(); };
            panel.Controls.Add(autoOff);

            return row;
        }

        private Button MakeDarkButton(string text)
        {
            Button b = new Button();
            b.Text = text;
            b.FlatStyle = FlatStyle.Flat;
            b.BackColor = Color.FromArgb(45, 55, 80);
            b.ForeColor = TextPri;
            b.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            b.Size = new Size(175, 24);
            b.FlatAppearance.BorderColor = Color.FromArgb(60, 90, 130);
            return b;
        }

        private void AutoUpdateServerTime()
        {
            if (_autoServerTimeLabel == null) return;
            try
            {
                DateTime st = VillageMap.getCurrentServerTime();
                _autoServerTimeLabel.Text = "Server time: " + st.ToString("HH:mm:ss");
            }
            catch
            {
                _autoServerTimeLabel.Text = "Server time: unavailable";
            }
        }

        private void AutoLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            AutoSettings s = BotEngine.Instance.Settings.Auto;

            _autoLoading = true;   // suppress change handlers while we populate controls
            try
            {

            // Interval inputs
            if (_autoCardIntervalInput != null)
                _autoCardIntervalInput.Value = Math.Max(_autoCardIntervalInput.Minimum,
                    Math.Min(_autoCardIntervalInput.Maximum, s.CardCheckIntervalSeconds));
            if (_autoModuleIntervalInput != null)
                _autoModuleIntervalInput.Value = Math.Max(_autoModuleIntervalInput.Minimum,
                    Math.Min(_autoModuleIntervalInput.Maximum, s.ModuleCheckIntervalSeconds));

            // Populate production rows from settings
            foreach (AutoProdRow row in _autoProdRows)
            {
                ProductionCardSettings p = s.GetProduction(row.GoodKey);
                row.EnabledCheck.Checked = p.Enabled;
                if (row.TierCombo.Items.Count > p.TierIndex)
                    row.TierCombo.SelectedIndex = p.TierIndex;
                row.TargetInput.Value = Math.Max(row.TargetInput.Minimum, Math.Min(row.TargetInput.Maximum, p.TargetCount));
                row.DelayHInput.Value = p.StartDelayMinutes / 60;
                row.DelayMInput.Value = p.StartDelayMinutes % 60;
                row.ProgressLabel.Text = p.PlayedCount + "/" + p.TargetCount;
            }

            // Populate module rows from settings
            foreach (AutoModuleRow row in _autoModuleRows)
            {
                ModuleScheduleSettings m = s.GetModuleSchedule(row.ModuleName);
                for (int h = 0; h < 24; h++)
                    row.HourChecks[h].Checked = m.HourlySchedule != null && h < m.HourlySchedule.Length && m.HourlySchedule[h];
                row.ReplayCardCheck.Checked = m.ReplayCardOnExpiry;
                row.AutoDisableCheck.Checked = m.AutoDisableEnabled;
                AutoPopulateModuleCardsListBox(row.ModuleName, row.CardsListBox, m.CardDefIds);
            }

            // Scroll reset is handled by moduleTab.Enter in BuildAutoTabUI — nothing to do here.
            }
            finally { _autoLoading = false; }
        }

        // Populates a module's card checklist from the curated ModuleCardCatalog (only the cards
        // relevant to that module, with friendly names), rather than every card the player owns.
        private void AutoPopulateModuleCardsListBox(string moduleName, CheckedListBox clb, System.Collections.Generic.List<int> selectedDefIds)
        {
            clb.Items.Clear();
            foreach (ModuleCardDef card in ModuleCardCatalog.GetCards(moduleName))
            {
                bool isChecked = selectedDefIds != null && selectedDefIds.Contains(card.DefId);
                clb.Items.Add(new AutoCardOption { DefId = card.DefId, Name = card.Name }, isChecked);
            }
        }

        private void AutoWriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            AutoSettings s = BotEngine.Instance.Settings.Auto;

            if (_autoCardIntervalInput != null)
                s.CardCheckIntervalSeconds = (int)_autoCardIntervalInput.Value;
            if (_autoModuleIntervalInput != null)
                s.ModuleCheckIntervalSeconds = (int)_autoModuleIntervalInput.Value;

            foreach (AutoProdRow row in _autoProdRows)
            {
                ProductionCardSettings p = s.GetProduction(row.GoodKey);
                bool wasEnabled = p.Enabled;
                bool nowEnabled = row.EnabledCheck.Checked;
                int newTarget = (int)row.TargetInput.Value;
                int newTier = row.TierCombo.SelectedIndex;
                int newDelay = (int)row.DelayHInput.Value * 60 + (int)row.DelayMInput.Value;

                // If target count changed, reset progress
                if (p.TargetCount != newTarget)
                {
                    p.PlayedCount = 0;
                    p.LastPlayedInstanceId = 0;
                    p.ScheduledStartTime = DateTime.MinValue;
                    p.PreviousTargetCount = -1;
                }

                // If re-enabled, reset scheduled start time so delay is re-applied
                if (!wasEnabled && nowEnabled)
                {
                    p.PlayedCount = 0;
                    p.LastPlayedInstanceId = 0;
                    p.ScheduledStartTime = DateTime.MinValue;
                }

                p.Enabled = nowEnabled;
                p.TierIndex = newTier;
                p.TargetCount = newTarget;
                p.StartDelayMinutes = newDelay;

                // Update progress display
                row.ProgressLabel.Text = p.PlayedCount + "/" + p.TargetCount;
            }

            foreach (AutoModuleRow row in _autoModuleRows)
            {
                ModuleScheduleSettings m = s.GetModuleSchedule(row.ModuleName);
                if (m.HourlySchedule == null || m.HourlySchedule.Length != 24)
                    m.HourlySchedule = new bool[24];
                for (int h = 0; h < 24; h++)
                    m.HourlySchedule[h] = row.HourChecks[h].Checked;
                m.ReplayCardOnExpiry = row.ReplayCardCheck.Checked;
                m.AutoDisableEnabled = row.AutoDisableCheck.Checked;

                m.CardDefIds.Clear();
                foreach (AutoCardOption opt in row.CardsListBox.CheckedItems)
                    if (opt != null && opt.DefId != 0)
                        m.CardDefIds.Add(opt.DefId);
                m.PlayCardOnStart = m.CardDefIds.Count > 0;
            }

            // Informational aggregate: is anything configured/active.
            bool anyActive = false;
            foreach (ProductionCardSettings p in s.ProductionCards)
                if (p.Enabled) { anyActive = true; break; }
            if (!anyActive)
                foreach (ModuleScheduleSettings m in s.ModuleSchedules)
                    foreach (bool h in m.HourlySchedule)
                        if (h) { anyActive = true; break; }
            s.Enabled = anyActive;

            // LIVE ONLY: changes are written to the in-memory settings object, which the Auto
            // modules read on every tick — so they take effect immediately. Persistence to disk
            // happens ONLY via the explicit Save Settings button; Load Settings reverts to the
            // last saved snapshot. (Deliberately no ApplySettings() here — it would re-sync every
            // module's Enabled from settings and could override the scheduler's runtime toggles.)
        }

        private void AutoResetProgress(string goodKey)
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            AutoSettings s = BotEngine.Instance.Settings.Auto;
            ProductionCardSettings p = s.GetProduction(goodKey);
            p.PlayedCount = 0;
            p.LastPlayedInstanceId = 0;
            p.ScheduledStartTime = DateTime.MinValue;
            p.PreviousTargetCount = -1;
            // Live only — not persisted until the user clicks Save Settings.

            foreach (AutoProdRow row in _autoProdRows)
            {
                if (row.GoodKey == goodKey)
                {
                    row.ProgressLabel.Text = "0/" + p.TargetCount;
                    break;
                }
            }
            BotLogger.Log("Auto", BotLogLevel.Info, goodKey + " card progress reset.");
        }

        private void WireUpScoutTab()
        {
            // ── Wire events ──────────────────────────────────────────────────

            _scEnabledCheck.CheckedChanged += delegate { ScPushGlobalSettings(); };
            _scIntervalInput.ValueChanged += delegate { ScPushGlobalSettings(); };
            _scMaxTimeInput.ValueChanged += delegate { ScPushGlobalSettings(); };
            _scAutoHireInput.ValueChanged += delegate { ScPushGlobalSettings(); };
            _scDelayInput.ValueChanged += delegate { ScPushGlobalSettings(); };
            _scDisableOnCardExpiryCheck.CheckedChanged += delegate { ScPushGlobalSettings(); };
            _scPriorityResourceRadio.CheckedChanged += delegate { ScPushGlobalSettings(); };
            _scPriorityRangeRadio.CheckedChanged += delegate { ScPushGlobalSettings(); };
            _scSendOneScoutCheck.CheckedChanged += delegate { ScPushGlobalSettings(); };
            _scSendOneOnNewCheck.CheckedChanged += delegate { ScPushGlobalSettings(); };

            _scVillageListBox.SelectedIndexChanged += delegate { ScOnVillageSelected(); };
            _scVillageEnabledCheck.CheckedChanged += delegate { ScSaveCurrentVillage(); };

            _scMoveToIgnoreBtn.Click += delegate { ScMoveSelectedToIgnore(); };
            _scMoveToScoutBtn.Click += delegate { ScMoveSelectedToScout(); };
            _scMoveUpBtn.Click += delegate { ScMoveScoutListItem(-1); };
            _scMoveDownBtn.Click += delegate { ScMoveScoutListItem(1); };

            _scScoutList.DoubleClick += delegate { ScMoveSelectedToIgnore(); };
            _scIgnoreList.DoubleClick += delegate { ScMoveSelectedToScout(); };

            _scCopySettingsBtn.Click += delegate { ScCopySettingsToAll(); };

            // Drag-to-reorder within each list
            _scScoutList.MouseDown += ScListMouseDown;
            _scScoutList.MouseMove += ScListMouseMove;
            _scScoutList.MouseUp += ScListMouseUp;
            _scIgnoreList.MouseDown += ScListMouseDown;
            _scIgnoreList.MouseMove += ScListMouseMove;
            _scIgnoreList.MouseUp += ScListMouseUp;

            // Auto-refresh: repopulate village list when world data becomes available,
            // and keep the status label current.
            _scRefreshTimer = new Timer();
            _scRefreshTimer.Interval = 2000;
            _scRefreshTimer.Tick += delegate
            {
                try
                {
                    ScUpdateStatusLabel();
                    if (_scVillageListBox.Items.Count == 0)
                        ScPopulateVillageList();
                }
                catch { }
            };
            _scRefreshTimer.Start();
        }

        private void ScLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            _scLoading = true;
            try
            {
                ScoutSettings s = BotEngine.Instance.Settings.Scout;
                // Read enabled from the live module if available — the auto-scheduler may have
                // enabled it after the settings were last loaded, so settings alone can be stale.
                ScoutModule liveScout = BotEngine.Instance.GetModule<ScoutModule>();
                _scEnabledCheck.Checked = liveScout != null ? liveScout.Enabled : s.Enabled;
                _scIntervalInput.Value = Math.Max(_scIntervalInput.Minimum, Math.Min(_scIntervalInput.Maximum, s.CycleIntervalSeconds));
                _scMaxTimeInput.Value = Math.Max(_scMaxTimeInput.Minimum, Math.Min(_scMaxTimeInput.Maximum, s.MaxScoutTimeSeconds));
                _scAutoHireInput.Value = Math.Max(0, Math.Min(8, s.AutoHireScouts));
                _scDelayInput.Value = Math.Max(_scDelayInput.Minimum, Math.Min(_scDelayInput.Maximum, s.DelayBetweenSendsMs));
                _scDisableOnCardExpiryCheck.Checked = s.DisableOnScoutCardExpiry;
                _scPriorityResourceRadio.Checked = s.Priority == ScoutPriority.ResourcePriority;
                _scPriorityRangeRadio.Checked = s.Priority == ScoutPriority.RangePriority;
                _scSendOneScoutCheck.Checked = s.SendOneScout;
                _scSendOneOnNewCheck.Checked = s.SendOneOnNewStash;
                ScUpdateStatusLabel();
                ScPopulateVillageList();
            }
            finally
            {
                _scLoading = false;
            }
        }

        private void ScPushGlobalSettings()
        {
            if (_scLoading) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            ScoutSettings s = BotEngine.Instance.Settings.Scout;
            s.Enabled = _scEnabledCheck.Checked;
            s.CycleIntervalSeconds = (int)_scIntervalInput.Value;
            s.MaxScoutTimeSeconds = (int)_scMaxTimeInput.Value;
            s.AutoHireScouts = (int)_scAutoHireInput.Value;
            s.DelayBetweenSendsMs = (int)_scDelayInput.Value;
            s.DisableOnScoutCardExpiry = _scDisableOnCardExpiryCheck.Checked;
            s.Priority = _scPriorityResourceRadio.Checked ? ScoutPriority.ResourcePriority : ScoutPriority.RangePriority;
            s.SendOneScout = _scSendOneScoutCheck.Checked;
            s.SendOneOnNewStash = _scSendOneOnNewCheck.Checked;

            foreach (IBotModule module in BotEngine.Instance.Modules)
            {
                if (module is Modules.ScoutModule)
                    module.Enabled = s.Enabled;
            }

            ScUpdateStatusLabel();
        }

        private void ScUpdateStatusLabel()
        {
            bool enabled = _scEnabledCheck.Checked;
            _scStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _scStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
        }

        private void ScPopulateVillageList()
        {
            _scVillageListBox.Items.Clear();
            _scSelectedVillageId = -1;

            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            foreach (WorldMap.UserVillageData uvd in villages)
            {
                string name = "";
                try { name = GameEngine.Instance.World.getVillageName(uvd.villageID); } catch { }
                if (string.IsNullOrEmpty(name)) name = "Village";
                _scVillageListBox.Items.Add(new ScoutVillageItem(uvd.villageID, name + " (" + uvd.villageID + ")"));
            }

            if (_scVillageListBox.Items.Count > 0)
                _scVillageListBox.SelectedIndex = 0;
        }

        private void ScOnVillageSelected()
        {
            if (_scLoading || _scDragging) return;

            ScoutVillageItem item = _scVillageListBox.SelectedItem as ScoutVillageItem;
            if (item == null)
            {
                _scSelectedVillageId = -1;
                return;
            }

            _scSelectedVillageId = item.VillageId;

            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            _scLoading = true;
            try
            {
                VillageScoutSettings vs = BotEngine.Instance.Settings.Scout.GetVillageSettings(_scSelectedVillageId);
                _scVillageEnabledCheck.Checked = vs.ScoutingEnabled;

                _scScoutList.Items.Clear();
                foreach (int type in vs.ResourceTypesToScout)
                    _scScoutList.Items.Add(new ScoutResourceItem(type, ScGetResourceTypeName(type)));

                _scIgnoreList.Items.Clear();
                foreach (int type in vs.ResourceTypesToIgnore)
                    _scIgnoreList.Items.Add(new ScoutResourceItem(type, ScGetResourceTypeName(type)));
            }
            finally
            {
                _scLoading = false;
            }
        }

        private void ScSaveCurrentVillage()
        {
            if (_scLoading) return;
            if (_scSelectedVillageId < 0) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            VillageScoutSettings vs = BotEngine.Instance.Settings.Scout.GetVillageSettings(_scSelectedVillageId);
            vs.ScoutingEnabled = _scVillageEnabledCheck.Checked;

            vs.ResourceTypesToScout.Clear();
            foreach (object item in _scScoutList.Items)
            {
                ScoutResourceItem ri = item as ScoutResourceItem;
                if (ri != null) vs.ResourceTypesToScout.Add(ri.ResourceType);
            }

            vs.ResourceTypesToIgnore.Clear();
            foreach (object item in _scIgnoreList.Items)
            {
                ScoutResourceItem ri = item as ScoutResourceItem;
                if (ri != null) vs.ResourceTypesToIgnore.Add(ri.ResourceType);
            }
        }

        private void ScCopySettingsToAll()
        {
            if (_scSelectedVillageId < 0) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            // Persist whatever is currently shown in the UI first
            ScSaveCurrentVillage();

            ScoutSettings settings = BotEngine.Instance.Settings.Scout;
            VillageScoutSettings source = settings.GetVillageSettings(_scSelectedVillageId);

            List<WorldMap.UserVillageData> villages = null;
            try
            {
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                    villages = GameEngine.Instance.World.getUserVillageList();
            }
            catch { }

            if (villages == null) return;

            int count = 0;
            foreach (WorldMap.UserVillageData uvd in villages)
            {
                if (uvd.villageID == _scSelectedVillageId) continue;
                VillageScoutSettings dest = settings.GetVillageSettings(uvd.villageID);
                dest.ScoutingEnabled = source.ScoutingEnabled;
                dest.ResourceTypesToScout = new List<int>(source.ResourceTypesToScout);
                dest.ResourceTypesToIgnore = new List<int>(source.ResourceTypesToIgnore);
                count++;
            }

            BotLogger.Log("Scout", BotLogLevel.Info,
                "Copied settings from village " + _scSelectedVillageId + " to " + count + " other village(s).");
        }

        private void ScListMouseDown(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int idx = lb.IndexFromPoint(e.Location);
            if (idx < 0) { _scDragItem = null; _scDragFromIndex = -1; return; }
            _scDragItem = lb.Items[idx];
            _scDragFromIndex = idx;
        }

        private void ScListMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || _scDragItem == null) return;
            ListBox lb = (ListBox)sender;
            int idx = lb.IndexFromPoint(e.Location);
            if (idx < 0 || idx == _scDragFromIndex) return;

            _scDragging = true;
            lb.Items.RemoveAt(_scDragFromIndex);
            lb.Items.Insert(idx, _scDragItem);
            lb.SelectedIndex = idx;
            _scDragFromIndex = idx;
            ScSaveCurrentVillage();
        }

        private void ScListMouseUp(object sender, MouseEventArgs e)
        {
            _scDragging = false;
            _scDragItem = null;
            _scDragFromIndex = -1;
        }

        private void ScMoveSelectedToIgnore()
        {
            ScoutResourceItem item = _scScoutList.SelectedItem as ScoutResourceItem;
            if (item == null) return;
            int idx = _scScoutList.SelectedIndex;
            _scScoutList.Items.Remove(item);
            _scIgnoreList.Items.Add(item);
            _scIgnoreList.SelectedItem = item;
            if (idx < _scScoutList.Items.Count) _scScoutList.SelectedIndex = idx;
            ScSaveCurrentVillage();
        }

        private void ScMoveSelectedToScout()
        {
            ScoutResourceItem item = _scIgnoreList.SelectedItem as ScoutResourceItem;
            if (item == null) return;
            int idx = _scIgnoreList.SelectedIndex;
            _scIgnoreList.Items.Remove(item);
            _scScoutList.Items.Add(item);
            _scScoutList.SelectedItem = item;
            if (idx < _scIgnoreList.Items.Count) _scIgnoreList.SelectedIndex = idx;
            ScSaveCurrentVillage();
        }

        private void ScMoveScoutListItem(int direction)
        {
            int idx = _scScoutList.SelectedIndex;
            if (idx < 0) return;
            int newIdx = idx + direction;
            if (newIdx < 0 || newIdx >= _scScoutList.Items.Count) return;
            object item = _scScoutList.Items[idx];
            _scScoutList.Items.RemoveAt(idx);
            _scScoutList.Items.Insert(newIdx, item);
            _scScoutList.SelectedIndex = newIdx;
            ScSaveCurrentVillage();
        }

        private static string ScGetResourceTypeName(int type)
        {
            if (type == 100) return "New Stash";
            try
            {
                if (type > 100 && type <= 133)
                {
                    string name = VillageBuildingsData.getResourceNames(type - 100);
                    if (!string.IsNullOrEmpty(name)) return name;
                }
            }
            catch { }
            return "Type " + type;
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

    internal class PopularityVillageRow : Panel
    {
        public event Action<int, PopularityMode> ModeChanged;
        private readonly int _villageId;

        public PopularityVillageRow(WorldMap.UserVillageData uvd, VillagePopularitySettings vs)
        {
            _villageId = uvd.villageID;
            Height = 28;
            BackColor = Color.FromArgb(30, 30, 42);

            Label nameLabel = new Label();
            string vname = "";
            try { vname = GameEngine.Instance.World.getVillageName(uvd.villageID); } catch { }
            nameLabel.Text = (string.IsNullOrEmpty(vname) ? "Village" : vname) + " (" + uvd.villageID + ")";
            nameLabel.ForeColor = Color.FromArgb(230, 230, 240);
            nameLabel.AutoSize = false;
            nameLabel.Width = 210;
            nameLabel.Location = new Point(8, 6);
            Controls.Add(nameLabel);

            ComboBox modeCombo = new ComboBox();
            modeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            modeCombo.Items.Add("Disabled");
            modeCombo.Items.Add("Max Popularity");
            modeCombo.Items.Add("Max Gold");
            modeCombo.Items.Add("Auto");
            modeCombo.SelectedIndex = (int)vs.Mode;
            modeCombo.Width = 130;
            modeCombo.Location = new Point(220, 4);
            modeCombo.BackColor = Color.FromArgb(40, 40, 55);
            modeCombo.ForeColor = Color.FromArgb(230, 230, 240);
            ComboBox capturedCombo = modeCombo;
            capturedCombo.SelectedIndexChanged += delegate
            {
                if (ModeChanged != null)
                    ModeChanged(_villageId, (PopularityMode)capturedCombo.SelectedIndex);
            };
            Controls.Add(modeCombo);
        }
    }

    // =========================================================================
    // Auto tab row helpers
    // =========================================================================

    class AutoProdRow
    {
        public string GoodKey;
        public CheckBox EnabledCheck;
        public ComboBox TierCombo;
        public NumericUpDown TargetInput;
        public NumericUpDown DelayHInput;
        public NumericUpDown DelayMInput;
        public Label ProgressLabel;
        public Panel RowPanel;
    }

    class AutoModuleRow
    {
        public string ModuleName;
        public CheckBox[] HourChecks = new CheckBox[24];
        public CheckedListBox CardsListBox;  // multi-select: any number of cards to play
        public CheckBox ReplayCardCheck;
        public CheckBox AutoDisableCheck;
        public Panel RowPanel;
    }

    class AutoCardOption
    {
        public int DefId;
        public string Name;
        public override string ToString() { return Name; }
    }
    internal class BanquetVillageRow : Panel
    {
        public event Action<int, int, bool> GoodToggled;
        private readonly int _villageId;

        private static readonly int[] ColXs = { 212, 302, 392, 482, 572, 660, 750, 838 };

        public BanquetVillageRow(
            WorldMap.UserVillageData uvd,
            VillageBanquetSettings vs,
            int researchLevel,
            bool alternate)
        {
            _villageId = uvd.villageID;
            Height = 28;
            BackColor = alternate
                ? Color.FromArgb(36, 38, 48)
                : Color.FromArgb(30, 32, 40);

            string vname = "";
            try { vname = GameEngine.Instance.World.getVillageName(uvd.villageID); } catch { }
            Label nameLabel = new Label();
            nameLabel.Text = (string.IsNullOrEmpty(vname) ? "Village" : vname) + " (" + uvd.villageID + ")";
            nameLabel.ForeColor = Color.FromArgb(230, 230, 240);
            nameLabel.AutoSize = false;
            nameLabel.Width = 200;
            nameLabel.Location = new Point(8, 6);
            Controls.Add(nameLabel);

            for (int i = 0; i < Modules.BanquetModule.GoodNames.Length; i++)
            {
                bool unlocked = i < researchLevel;
                CheckBox cb = new CheckBox();
                cb.AutoSize = true;
                cb.Location = new Point(ColXs[i], 6);
                cb.BackColor = Color.Transparent;
                cb.Checked = vs.EnabledGoods.Contains(i);
                cb.Enabled = unlocked;
                cb.ForeColor = unlocked
                    ? Color.FromArgb(230, 230, 240)
                    : Color.FromArgb(80, 85, 100);

                int capturedIdx = i;
                cb.CheckedChanged += delegate
                {
                    if (GoodToggled != null)
                        GoodToggled(_villageId, capturedIdx, cb.Checked);
                };
                Controls.Add(cb);
            }
        }
    }

    internal class ScoutVillageItem
    {
        public readonly int VillageId;
        private readonly string _display;

        public ScoutVillageItem(int villageId, string display)
        {
            VillageId = villageId;
            _display = display;
        }

        public override string ToString() { return _display; }
    }

    internal class ScoutResourceItem
    {
        public readonly int ResourceType;
        private readonly string _display;

        public ScoutResourceItem(int resourceType, string display)
        {
            ResourceType = resourceType;
            _display = display;
        }

        public override string ToString() { return _display; }
    }

    // =====================================================================
    // Defender tab
    // =====================================================================

    public partial class BotControlForm
    {
        private static readonly Color DfBgDark = Color.FromArgb(24, 24, 32);
        private static readonly Color DfBgCard = Color.FromArgb(40, 42, 54);
        private static readonly Color DfBgInput = Color.FromArgb(50, 52, 64);
        private static readonly Color DfTextPrimary = Color.FromArgb(230, 230, 240);
        private static readonly Color DfTextSecondary = Color.FromArgb(160, 165, 180);
        private static readonly Color DfAccent = Color.FromArgb(80, 160, 255);
        private static readonly Color DfSuccess = Color.FromArgb(80, 200, 120);
        private static readonly Color DfError = Color.FromArgb(240, 80, 80);
        private static readonly Color DfWarning = Color.FromArgb(220, 160, 40);
        private static readonly Color DfBorder = Color.FromArgb(55, 58, 72);

        private bool _dfLoading;
        private Timer _dfRefreshTimer;

        private void WireUpDefenderTab()
        {
            DfInitComboItems();

            _dfEnabledCheck.CheckedChanged += delegate { DfWriteToSettings(); };
            _dfDurationInput.ValueChanged += delegate { DfWriteToSettings(); };
            _dfKnightsCombo.SelectedIndexChanged += delegate { DfWriteToSettings(); };
            _dfLastStandCombo.SelectedIndexChanged += delegate { DfWriteToSettings(); };
            _dfDesperateCheck.CheckedChanged += delegate { DfWriteToSettings(); };
            _dfAutoRepairCheck.CheckedChanged += delegate { DfWriteToSettings(); };
            _dfRestoreTroopsCheck.CheckedChanged += delegate { DfWriteToSettings(); };
            _dfRestoreInfraCheck.CheckedChanged += delegate { DfWriteToSettings(); };

            _dfRefreshTimer = new Timer();
            _dfRefreshTimer.Interval = 500;
            _dfRefreshTimer.Tick += delegate
            {
                try { DfUpdateCountdown(); }
                catch { /* swallow — timer must not propagate exceptions */ }
            };
            _dfRefreshTimer.Start();


            _dfVillageRefreshBtn.Click += delegate { DfPopulateVillages(); };
            _dfStartBtn.Click += delegate { DfStartSpam(); };
            _dfStopBtn.Click += delegate { DfStopSpam(); };
        }

        private void DfInitComboItems()
        {
            // Populate the knights and last stand card combo boxes.
            // All other controls are initialized in the designer.
            _dfKnightsCombo.Items.Add(new DfCardItem("None", 0));
            _dfKnightsCombo.Items.Add(new DfCardItem("Surprise Attack (2)", 265));
            _dfKnightsCombo.Items.Add(new DfCardItem("Surprise Attack (5)", 269));
            _dfKnightsCombo.Items.Add(new DfCardItem("Surprise Attack (12)", 270));
            _dfKnightsCombo.SelectedIndex = 0;

            _dfLastStandCombo.Items.Add(new DfCardItem("None", 0));
            _dfLastStandCombo.Items.Add(new DfCardItem("Last Stand (5)", 266));
            _dfLastStandCombo.Items.Add(new DfCardItem("Last Stand (10)", 271));
            _dfLastStandCombo.Items.Add(new DfCardItem("Last Stand (20)", 272));
            _dfLastStandCombo.SelectedIndex = 0;

            DfPopulateVillages();
        }

        private void DfPopulateVillages()
        {
            int currentId = 0;
            DfVillageItem current = _dfVillageCombo.SelectedItem as DfVillageItem;
            if (current != null) currentId = current.VillageId;

            _dfVillageCombo.Items.Clear();

            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;

            List<int> ids = GameEngine.Instance.World.getUserVillageIDList();
            if (ids == null) return;

            DfVillageItem toSelect = null;
            foreach (int id in ids)
            {
                string name = GameEngine.Instance.World.getVillageName(id);
                DfVillageItem item = new DfVillageItem(id, "[" + id + "] " + name);
                _dfVillageCombo.Items.Add(item);
                if (id == currentId) toSelect = item;
            }

            if (toSelect != null)
                _dfVillageCombo.SelectedItem = toSelect;
            else if (_dfVillageCombo.Items.Count > 0)
                _dfVillageCombo.SelectedIndex = 0;
        }

        private void DfStartSpam()
        {
            DfWriteToSettings();
            Kingdoms.Bot.Modules.DefenderModule mod = GetDefenderModule();
            if (mod == null) return;

            DfVillageItem item = _dfVillageCombo.SelectedItem as DfVillageItem;
            if (item == null)
            {
                BotLogger.Log("Defender", BotLogLevel.Warning, "No target village selected.");
                return;
            }

            int duration = (int)_dfDurationInput.Value;
            mod.StartSpam(duration, item.VillageId);
        }

        private void DfStopSpam()
        {
            Kingdoms.Bot.Modules.DefenderModule mod = GetDefenderModule();
            if (mod != null) mod.StopSpam();
        }

        private void DfUpdateCountdown()
        {
            if (_dfCountdownLabel == null) return;

            bool enabled = _dfEnabledCheck != null && _dfEnabledCheck.Checked;
            if (_dfStatusLabel != null)
            {
                _dfStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
                _dfStatusLabel.ForeColor = enabled ? DfSuccess : DfError;
            }

            Kingdoms.Bot.Modules.DefenderModule mod = GetDefenderModule();
            if (mod == null) { _dfCountdownLabel.Text = "--"; return; }

            if (mod.IsSpamActive)
            {
                int secs = mod.SecondsRemaining;
                _dfCountdownLabel.Text = secs + "s";
                _dfCountdownLabel.ForeColor = DfWarning;
            }
            else
            {
                _dfCountdownLabel.Text = "--";
                _dfCountdownLabel.ForeColor = DfAccent;
            }
        }

        private void DfLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            _dfLoading = true;
            try
            {
            DefenderSettings s = BotEngine.Instance.Settings.Defender;

            _dfEnabledCheck.Checked = s.Enabled;
            _dfDurationInput.Value = Math.Max(_dfDurationInput.Minimum,
                Math.Min(_dfDurationInput.Maximum, s.SpamDurationSeconds));
            _dfDesperateCheck.Checked = s.SpamDesperateDefence;
            _dfAutoRepairCheck.Checked = s.AutoRepair;
            _dfRestoreTroopsCheck.Checked = s.RestoreTroops;
            _dfRestoreInfraCheck.Checked = s.RestoreInfrastructure;

            DfSelectComboByDefId(_dfKnightsCombo, s.KnightsCardDefId);
            DfSelectComboByDefId(_dfLastStandCombo, s.LastStandCardDefId);

            if (s.TargetVillageId > 0)
            {
                foreach (object item in _dfVillageCombo.Items)
                {
                    DfVillageItem vi = item as DfVillageItem;
                    if (vi != null && vi.VillageId == s.TargetVillageId)
                    {
                        _dfVillageCombo.SelectedItem = vi;
                        break;
                    }
                }
            }
            }
            finally { _dfLoading = false; }
        }

        private void DfWriteToSettings()
        {
            if (_dfLoading) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            DefenderSettings s = BotEngine.Instance.Settings.Defender;

            s.Enabled = _dfEnabledCheck.Checked;
            s.SpamDurationSeconds = (int)_dfDurationInput.Value;
            s.SpamDesperateDefence = _dfDesperateCheck.Checked;
            s.AutoRepair = _dfAutoRepairCheck.Checked;
            s.RestoreTroops = _dfRestoreTroopsCheck.Checked;
            s.RestoreInfrastructure = _dfRestoreInfraCheck.Checked;

            DfCardItem ki = _dfKnightsCombo.SelectedItem as DfCardItem;
            s.KnightsCardDefId = ki != null ? ki.DefId : 0;

            DfCardItem li = _dfLastStandCombo.SelectedItem as DfCardItem;
            s.LastStandCardDefId = li != null ? li.DefId : 0;

            DfVillageItem vi = _dfVillageCombo.SelectedItem as DfVillageItem;
            s.TargetVillageId = vi != null ? vi.VillageId : 0;

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is Kingdoms.Bot.Modules.DefenderModule)
                    m.Enabled = s.Enabled;
            }
        }

        private static void DfSelectComboByDefId(ComboBox combo, int defId)
        {
            foreach (object item in combo.Items)
            {
                DfCardItem ci = item as DfCardItem;
                if (ci != null && ci.DefId == defId)
                {
                    combo.SelectedItem = item;
                    return;
                }
            }
            if (combo.Items.Count > 0) combo.SelectedIndex = 0;
        }

        private Kingdoms.Bot.Modules.DefenderModule GetDefenderModule()
        {
            if (BotEngine.Instance == null) return null;
            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                Kingdoms.Bot.Modules.DefenderModule mod = m as Kingdoms.Bot.Modules.DefenderModule;
                if (mod != null) return mod;
            }
            return null;
        }

        private class DfCardItem
        {
            public readonly int DefId;
            private readonly string _label;
            public DfCardItem(string label, int defId) { _label = label; DefId = defId; }
            public override string ToString() { return _label; }
        }

        private class DfVillageItem
        {
            public readonly int VillageId;
            private readonly string _label;
            public DfVillageItem(int id, string label) { VillageId = id; _label = label; }
            public override string ToString() { return _label; }
        }

        // =====================================================================
        // Monk tab
        // =====================================================================

        private void WireUpMonkTab()
        {
            _mkEnabledCheck.CheckedChanged += delegate { MkWriteToSettings(); };
            _mkIntervalInput.ValueChanged  += delegate { MkWriteToSettings(); };
            _mkDelayInput.ValueChanged     += delegate { MkWriteToSettings(); };
            _mkMonksToKeepInput.ValueChanged  += delegate { MkWriteToSettings(); };
            _mkAutoRecruitInput.ValueChanged   += delegate { MkWriteToSettings(); };

            _mkRefreshBtn.Click    += delegate { MkPopulateRouteList(); };
            _mkRunNowBtn.Click     += delegate { MkRunNow(); };
            _mkAddRouteBtn.Click    += delegate { MkAddRoute(); };
            _mkEditRouteBtn.Click   += delegate { MkEditSelected(); };
            _mkDeleteRouteBtn.Click += delegate { MkDeleteSelected(); };

            // Duplicate and Reset Progress — added programmatically to match Trade tab style
            Button dupBtn = new Button();
            dupBtn.Text      = "Duplicate";
            dupBtn.BackColor = Color.FromArgb(40, 75, 40);
            dupBtn.ForeColor = Color.FromArgb(230, 230, 240);
            dupBtn.FlatStyle = FlatStyle.Flat;
            dupBtn.Font      = new Font("Segoe UI", 8.5f);
            dupBtn.Size      = new Size(90, 26);
            dupBtn.Location  = new Point(270, 4);
            dupBtn.UseVisualStyleBackColor = false;
            dupBtn.Click += delegate { MkDuplicateSelected(); };
            _mkRouteButtonPanel.Controls.Add(dupBtn);

            Button resetBtn = new Button();
            resetBtn.Text      = "Reset Progress";
            resetBtn.BackColor = Color.FromArgb(90, 65, 20);
            resetBtn.ForeColor = Color.FromArgb(230, 230, 240);
            resetBtn.FlatStyle = FlatStyle.Flat;
            resetBtn.Font      = new Font("Segoe UI", 8.5f);
            resetBtn.Size      = new Size(115, 26);
            resetBtn.Location  = new Point(368, 4);
            resetBtn.UseVisualStyleBackColor = false;
            resetBtn.Click += delegate { MkResetSelectedProgress(); };
            _mkRouteButtonPanel.Controls.Add(resetBtn);

            // Populate the static column-header panel (defined in Designer so docking order is correct)
            int[] colXs    = { 28, 174, 270, 340, 410, 546, 630 };
            string[] colNames = { "Route Name", "Command", "From", "To", "Stop Condition", "Param", "Progress" };
            for (int i = 0; i < colNames.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text      = colNames[i];
                lbl.Font      = new Font("Segoe UI", 7f, FontStyle.Bold);
                lbl.ForeColor = TextSec;
                lbl.AutoSize  = true;
                lbl.Location  = new Point(colXs[i], 4);
                _mkColHeader.Controls.Add(lbl);
            }

            // Periodic sync: reflects module-side route.Enabled changes (e.g. auto-disable
            // on quest complete) back to the UI checkboxes without a full list rebuild.
            _mkSyncTimer = new Timer();
            _mkSyncTimer.Interval = 1500;
            _mkSyncTimer.Tick += delegate { try { MkSyncRouteStates(); } catch { } };
            _mkSyncTimer.Start();

            MkLoadFromSettings();
        }

        private void MkLoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            _mkLoading = true;
            try
            {
                MonkSettings s = BotEngine.Instance.Settings.Monk;
                _mkEnabledCheck.Checked = s.Enabled;
                _mkIntervalInput.Value  = Math.Max(_mkIntervalInput.Minimum,
                    Math.Min(_mkIntervalInput.Maximum, s.CycleIntervalSeconds));
                _mkDelayInput.Value     = Math.Max(_mkDelayInput.Minimum,
                    Math.Min(_mkDelayInput.Maximum, s.DelayBetweenRoutesMs));
                _mkMonksToKeepInput.Value = Math.Max(_mkMonksToKeepInput.Minimum,
                    Math.Min(_mkMonksToKeepInput.Maximum, s.MonksToKeep));
                _mkAutoRecruitInput.Value = Math.Max(_mkAutoRecruitInput.Minimum,
                    Math.Min(_mkAutoRecruitInput.Maximum, s.AutoRecruitMonks));

                MkPopulateRouteList();
                MkUpdateStatusDisplay();
            }
            finally { _mkLoading = false; }
        }

        private void MkWriteToSettings()
        {
            if (_mkLoading) return;
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            MonkSettings s = BotEngine.Instance.Settings.Monk;
            s.Enabled               = _mkEnabledCheck.Checked;
            s.CycleIntervalSeconds  = (int)_mkIntervalInput.Value;
            s.DelayBetweenRoutesMs  = (int)_mkDelayInput.Value;
            s.MonksToKeep           = (int)_mkMonksToKeepInput.Value;
            s.AutoRecruitMonks      = (int)_mkAutoRecruitInput.Value;

            foreach (IBotModule module in BotEngine.Instance.Modules)
            {
                if (module is Modules.MonkModule)
                    module.Enabled = s.Enabled;
            }

            MkUpdateStatusDisplay();
        }

        private void MkPopulateRouteList()
        {
            _mkRouteListPanel.SuspendLayout();
            foreach (MonkRouteRow row in _mkRouteRows)
            {
                _mkRouteListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _mkRouteRows.Clear();
            _mkSelectedRouteIndex = -1;

            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
            {
                _mkRouteListPanel.ResumeLayout();
                return;
            }

            MonkSettings settings = BotEngine.Instance.Settings.Monk;
            int y = 2;
            bool alternate = false;
            for (int i = 0; i < settings.Routes.Count; i++)
            {
                MonkRouteSettings route = settings.Routes[i];
                MonkRouteRow row = new MonkRouteRow(i, route, alternate);
                row.Location = new Point(0, y);
                row.Width    = _mkRouteListPanel.ClientSize.Width;
                row.Anchor   = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                int capturedIdx = i;
                row.EnabledToggled     += (idx, en) => MkOnEnabledToggled(idx, en);
                row.SelectionRequested += (idx) => MkSelectRoute(idx);
                row.DeleteRequested    += (idx) => MkDeleteRoute(idx);

                _mkRouteListPanel.Controls.Add(row);
                _mkRouteRows.Add(row);
                y += row.Height + 1;
                alternate = !alternate;
            }

            _mkRouteListPanel.ResumeLayout();
        }

        private void MkSelectRoute(int idx)
        {
            _mkSelectedRouteIndex = idx;
            for (int i = 0; i < _mkRouteRows.Count; i++)
                _mkRouteRows[i].Selected = (i == idx);
        }

        // Returns the index of the first selected route row, or -1 if none.
        private int MkGetSelectedIndex()
        {
            for (int i = 0; i < _mkRouteRows.Count; i++)
                if (_mkRouteRows[i].Selected) return i;
            return -1;
        }

        private void MkEditSelected()
        {
            int idx = MkGetSelectedIndex();
            if (idx >= 0) MkEditRoute(idx);
        }

        private void MkDeleteSelected()
        {
            int idx = MkGetSelectedIndex();
            if (idx >= 0) MkDeleteRoute(idx);
        }

        private void MkDuplicateSelected()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            int idx = MkGetSelectedIndex();
            if (idx < 0) return;
            MonkSettings s = BotEngine.Instance.Settings.Monk;
            if (idx >= s.Routes.Count) return;
            MonkRouteSettings clone = s.Routes[idx].Clone();
            s.Routes.Add(clone);
            MkPopulateRouteList();
        }

        private void MkResetSelectedProgress()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            int idx = MkGetSelectedIndex();
            if (idx < 0) return;
            MonkSettings s = BotEngine.Instance.Settings.Monk;
            if (idx >= s.Routes.Count) return;
            MonkRouteSettings route = s.Routes[idx];
            route.ResetProgress();
            // Re-enable route in case it was auto-disabled on completion
            route.Enabled = true;
            MkPopulateRouteList();
        }

        private void MkOnEnabledToggled(int idx, bool enabled)
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            MonkSettings s = BotEngine.Instance.Settings.Monk;
            if (idx >= 0 && idx < s.Routes.Count)
                s.Routes[idx].Enabled = enabled;
        }

        private void MkAddRoute()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;

            MonkRouteSettings newRoute = new MonkRouteSettings();
            newRoute.Name = "New Monk Route";
            MonkRouteEditorForm editor = new MonkRouteEditorForm(newRoute, "Add Monk Route");
            editor.ShowDialog(this);

            if (editor.Saved)
            {
                BotEngine.Instance.Settings.Monk.Routes.Add(newRoute);
                MkPopulateRouteList();
            }
        }

        private void MkEditRoute(int idx)
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            MonkSettings s = BotEngine.Instance.Settings.Monk;
            if (idx < 0 || idx >= s.Routes.Count) return;

            MonkRouteEditorForm editor = new MonkRouteEditorForm(s.Routes[idx], "Edit Monk Route");
            editor.ShowDialog(this);

            if (editor.Saved)
                MkPopulateRouteList();
        }

        private void MkDeleteRoute(int idx)
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            MonkSettings s = BotEngine.Instance.Settings.Monk;
            if (idx < 0 || idx >= s.Routes.Count) return;

            if (MessageBox.Show(this,
                "Delete route \"" + s.Routes[idx].Name + "\"?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                s.Routes.RemoveAt(idx);
                MkPopulateRouteList();
            }
        }

        private void MkRunNow()
        {
            if (BotEngine.Instance == null) return;
            foreach (IBotModule module in BotEngine.Instance.Modules)
            {
                Modules.MonkModule mm = module as Modules.MonkModule;
                if (mm != null) { mm.RunNow(); break; }
            }
        }

        private void MkUpdateStatusDisplay()
        {
            bool enabled = _mkEnabledCheck.Checked;
            _mkStatusLabel.Text      = enabled ? "ENABLED" : "DISABLED";
            _mkStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
        }

        // Lightweight periodic sync: updates each route row's enabled checkbox to match
        // the live settings value, so module-side auto-disables (quest done, not researched)
        // are reflected in the UI without rebuilding the whole list.
        private void MkSyncRouteStates()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return;
            MonkSettings s = BotEngine.Instance.Settings.Monk;
            for (int i = 0; i < _mkRouteRows.Count && i < s.Routes.Count; i++)
                _mkRouteRows[i].SyncState(s.Routes[i]);
        }
    }
}
