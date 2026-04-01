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
                SubscribeToLog();
                RefreshStatus();
                ReplayExistingLogs();

                VsLoadFromSettings();
                RdLoadFromSettings();
                RdBuildActionRows();
                RcLoadFromSettings();
                CrLoadFromSettings();
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
                if (wa != wb) return wb.CompareTo(wa);
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

            RcBuildColumnHeaders(_rcColHeaderVillages);
            RcBuildColumnHeaders(_rcColHeaderCapitals);

            _rcRefreshTimer = new Timer();
            _rcRefreshTimer.Interval = 2000;
            _rcRefreshTimer.Tick += delegate { RcUpdateStatusDisplay(); };
            _rcRefreshTimer.Start();
        }

        private void RcBuildColumnHeaders(Panel headerPanel)
        {
            Label villageHdr = new Label();
            villageHdr.Text = "Village";
            villageHdr.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            villageHdr.ForeColor = TextSec;
            villageHdr.AutoSize = true;
            villageHdr.Location = new Point(8, 5);
            headerPanel.Controls.Add(villageHdr);

            string[] unitKeys = RecruitingModule.AllUnitKeys;
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
                    capitalPanels.Add(new RecruitVillagePanel(id, name, vs, capitalPanels.Count % 2 != 0));
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

            _crRefreshTimer = new Timer();
            _crRefreshTimer.Interval = 2000;
            _crRefreshTimer.Tick += delegate { CrUpdateStatusDisplay(); };
            _crRefreshTimer.Start();
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
                if (wa != wb) return wb.CompareTo(wa);
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
                tabName = "Recruiting";
            }
            else if (_tabControl.SelectedTab == _crPage)
            {
                CrWriteToSettings();
                tabName = "Castle Repair";
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
                tabName = "Recruiting";
            }
            else if (_tabControl.SelectedTab == _crPage)
            {
                CrLoadFromSettings();
                tabName = "Castle Repair";
            }

            RefreshStatus();
            BotLogger.Log("UI", BotLogLevel.Info, tabName + " settings reloaded from disk.");
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
