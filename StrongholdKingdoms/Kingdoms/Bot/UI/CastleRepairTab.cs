using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonTypes;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    internal class CastleRepairTab : Panel
    {
        private static readonly Color BgDark = Color.FromArgb(24, 24, 32);
        private static readonly Color BgCard = Color.FromArgb(40, 42, 54);
        private static readonly Color BgInput = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPrimary = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSecondary = Color.FromArgb(160, 165, 180);
        private static readonly Color Accent = Color.FromArgb(80, 160, 255);
        private static readonly Color AccentDim = Color.FromArgb(50, 100, 180);
        private static readonly Color Success = Color.FromArgb(80, 200, 120);
        private static readonly Color ErrorCol = Color.FromArgb(240, 80, 80);
        private static readonly Color Border = Color.FromArgb(55, 58, 72);

        private CheckBox _enabledCheck;
        private Label _statusLabel;
        private NumericUpDown _intervalInput;
        private NumericUpDown _delayInput;
        private CheckBox _repairOnAttackCheck;
        private Button _repairAllNowBtn;
        private Button _refreshBtn;
        private Panel _villageListPanel;
        private Timer _refreshTimer;
        private List<CastleRepairVillageRow> _villageRows = new List<CastleRepairVillageRow>();

        public CastleRepairTab()
        {
            this.BackColor = BgDark;
            this.Dock = DockStyle.Fill;
            BuildUI();

            _refreshTimer = new Timer();
            _refreshTimer.Interval = 2000;
            _refreshTimer.Tick += delegate { UpdateStatusDisplay(); };
            _refreshTimer.Start();
        }

        private void BuildUI()
        {
            // Settings panel
            Panel settingsPanel = new Panel();
            settingsPanel.Dock = DockStyle.Top;
            settingsPanel.Height = 100;
            settingsPanel.BackColor = BgCard;
            settingsPanel.Padding = new Padding(16, 12, 16, 8);

            _enabledCheck = new CheckBox();
            _enabledCheck.Text = "Module Enabled";
            _enabledCheck.Checked = true;
            _enabledCheck.FlatStyle = FlatStyle.Flat;
            _enabledCheck.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            _enabledCheck.ForeColor = TextPrimary;
            _enabledCheck.AutoSize = true;
            _enabledCheck.Location = new Point(16, 10);
            settingsPanel.Controls.Add(_enabledCheck);

            _statusLabel = new Label();
            _statusLabel.Text = "ENABLED";
            _statusLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            _statusLabel.ForeColor = Success;
            _statusLabel.AutoSize = true;
            _statusLabel.Location = new Point(200, 13);
            settingsPanel.Controls.Add(_statusLabel);

            Label intervalLabel = MakeLabel("Cycle interval (s):", 16, 42);
            settingsPanel.Controls.Add(intervalLabel);

            _intervalInput = MakeNumeric(160, 40, 80, 10, 3600, 120);
            settingsPanel.Controls.Add(_intervalInput);

            Label delayLabel = MakeLabel("Delay per village (ms):", 260, 42);
            settingsPanel.Controls.Add(delayLabel);

            _delayInput = MakeNumeric(430, 40, 80, 1000, 30000, 5000);
            _delayInput.Increment = 500;
            settingsPanel.Controls.Add(_delayInput);

            _repairOnAttackCheck = new CheckBox();
            _repairOnAttackCheck.Text = "Repair on attack";
            _repairOnAttackCheck.Checked = true;
            _repairOnAttackCheck.FlatStyle = FlatStyle.Flat;
            _repairOnAttackCheck.Font = new Font("Segoe UI", 8.5f);
            _repairOnAttackCheck.ForeColor = TextPrimary;
            _repairOnAttackCheck.AutoSize = true;
            _repairOnAttackCheck.Location = new Point(530, 40);
            settingsPanel.Controls.Add(_repairOnAttackCheck);

            _repairAllNowBtn = MakeButton("Repair All Now", ErrorCol, new Point(16, 70), new Size(120, 24));
            _repairAllNowBtn.Click += delegate { RepairAllNowClick(); };
            settingsPanel.Controls.Add(_repairAllNowBtn);

            _refreshBtn = MakeButton("Refresh Villages", Accent, new Point(150, 70), new Size(120, 24));
            _refreshBtn.Click += delegate { BuildVillageList(); };
            settingsPanel.Controls.Add(_refreshBtn);

            this.Controls.Add(settingsPanel);

            // Separator
            Panel sep = new Panel();
            sep.Dock = DockStyle.Top;
            sep.Height = 1;
            sep.BackColor = Border;
            this.Controls.Add(sep);

            // Column header
            Panel colHeader = new Panel();
            colHeader.Dock = DockStyle.Top;
            colHeader.Height = 22;
            colHeader.BackColor = Color.FromArgb(30, 32, 40);
            colHeader.Padding = new Padding(16, 0, 16, 0);

            colHeader.Controls.Add(MakeHeaderLabel("Village", 16));
            colHeader.Controls.Add(MakeHeaderLabel("Infra", 260));
            colHeader.Controls.Add(MakeHeaderLabel("Troops", 310));
            colHeader.Controls.Add(MakeHeaderLabel("Source", 370));
            colHeader.Controls.Add(MakeHeaderLabel("Infra Preset", 460));
            colHeader.Controls.Add(MakeHeaderLabel("Troop Preset", 620));
            this.Controls.Add(colHeader);

            // Village list
            _villageListPanel = new Panel();
            _villageListPanel.Dock = DockStyle.Fill;
            _villageListPanel.AutoScroll = true;
            _villageListPanel.BackColor = BgDark;
            this.Controls.Add(_villageListPanel);

            // Fix z-order: settings on top, then sep, then header, then list fills rest
            this.Controls.SetChildIndex(_villageListPanel, 0);
            this.Controls.SetChildIndex(colHeader, 1);
            this.Controls.SetChildIndex(sep, 2);
            this.Controls.SetChildIndex(settingsPanel, 3);
        }

        public void LoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            CastleRepairSettings s = BotEngine.Instance.Settings.CastleRepair;
            _enabledCheck.Checked = s.Enabled;
            _intervalInput.Value = Math.Max(_intervalInput.Minimum,
                Math.Min(_intervalInput.Maximum, s.IntervalSeconds));
            _delayInput.Value = Math.Max(_delayInput.Minimum,
                Math.Min(_delayInput.Maximum, s.DelayBetweenVillagesMs));
            _repairOnAttackCheck.Checked = s.RepairOnAttack;

            foreach (CastleRepairVillageRow row in _villageRows)
                row.LoadFromSettings(s);
        }

        public void WriteToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            CastleRepairSettings s = BotEngine.Instance.Settings.CastleRepair;
            s.Enabled = _enabledCheck.Checked;
            s.IntervalSeconds = (int)_intervalInput.Value;
            s.DelayBetweenVillagesMs = (int)_delayInput.Value;
            s.RepairOnAttack = _repairOnAttackCheck.Checked;

            foreach (CastleRepairVillageRow row in _villageRows)
                row.WriteToSettings(s);

            foreach (IBotModule m in BotEngine.Instance.Modules)
            {
                if (m is CastleRepairModule)
                    m.Enabled = s.Enabled;
            }
        }

        public void BuildVillageList()
        {
            _villageListPanel.SuspendLayout();
            foreach (CastleRepairVillageRow row in _villageRows)
            {
                _villageListPanel.Controls.Remove(row);
                row.Dispose();
            }
            _villageRows.Clear();

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
            {
                _villageListPanel.ResumeLayout();
                return;
            }

            List<int> ids = GameEngine.Instance.World.getUserVillageIDList();
            if (ids == null)
            {
                _villageListPanel.ResumeLayout();
                return;
            }

            CastleRepairSettings settings = null;
            if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                settings = BotEngine.Instance.Settings.CastleRepair;

            // Gather preset names
            List<string> infraPresets = CastleRepairModule.GetPresetNames(PresetType.INFRASTRUCTURE);
            List<string> troopPresets = CastleRepairModule.GetPresetNames(PresetType.TROOP_DEFEND);

            bool alternate = false;
            for (int i = ids.Count - 1; i >= 0; i--)
            {
                int id = ids[i];
                string name = GetVillageName(id);
                VillageCastleRepairSettings vs = settings != null
                    ? settings.GetVillageSettings(id)
                    : new VillageCastleRepairSettings();

                CastleRepairVillageRow row = new CastleRepairVillageRow(
                    id, name, vs, infraPresets, troopPresets, alternate);
                row.Dock = DockStyle.Top;
                _villageListPanel.Controls.Add(row);
                _villageRows.Add(row);
                alternate = !alternate;
            }
            _villageListPanel.ResumeLayout();
        }

        private void RepairAllNowClick()
        {
            CastleRepairModule module = null;
            if (BotEngine.Instance != null)
            {
                foreach (IBotModule m in BotEngine.Instance.Modules)
                {
                    module = m as CastleRepairModule;
                    if (module != null) break;
                }
            }
            if (module != null)
            {
                WriteToSettings();
                module.RepairAllNow();
            }
            else
            {
                BotLogger.Log("Castle Repair", BotLogLevel.Warning, "Module not found.");
            }
        }

        private void UpdateStatusDisplay()
        {
            if (_enabledCheck == null) return;
            bool enabled = _enabledCheck.Checked;
            _statusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _statusLabel.ForeColor = enabled ? Success : ErrorCol;
        }

        private static string GetVillageName(int villageId)
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                return GameEngine.Instance.World.getVillageName(villageId);
            return "Village " + villageId;
        }

        private static Label MakeLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 8.5f);
            lbl.ForeColor = Color.FromArgb(160, 165, 180);
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            return lbl;
        }

        private static Label MakeHeaderLabel(string text, int x)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(160, 165, 180);
            lbl.AutoSize = true;
            lbl.Location = new Point(x, 4);
            return lbl;
        }

        private static NumericUpDown MakeNumeric(int x, int y, int w, int min, int max, int val)
        {
            NumericUpDown nud = new NumericUpDown();
            nud.BackColor = Color.FromArgb(50, 52, 64);
            nud.ForeColor = Color.FromArgb(230, 230, 240);
            nud.BorderStyle = BorderStyle.FixedSingle;
            nud.Location = new Point(x, y);
            nud.Size = new Size(w, 22);
            nud.Minimum = min;
            nud.Maximum = max;
            nud.Value = Math.Max(min, Math.Min(max, val));
            return nud;
        }

        private static Button MakeButton(string text, Color bg, Point loc, Size size)
        {
            Button btn = new Button();
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            btn.Text = text;
            btn.Cursor = Cursors.Hand;
            btn.Location = loc;
            btn.Size = size;
            return btn;
        }
    }

    internal class CastleRepairVillageRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPrimary = Color.FromArgb(230, 230, 240);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);

        private int _villageId;
        private CheckBox _infraCheck;
        private CheckBox _troopsCheck;
        private ComboBox _sourceCombo;
        private ComboBox _infraPresetCombo;
        private ComboBox _troopPresetCombo;

        public int VillageId { get { return _villageId; } }

        public CastleRepairVillageRow(int villageId, string villageName,
            VillageCastleRepairSettings settings,
            List<string> infraPresets, List<string> troopPresets, bool alternate)
        {
            _villageId = villageId;
            this.Height = 28;
            this.BackColor = alternate ? BgOdd : BgEven;

            Label nameLabel = new Label();
            nameLabel.Text = villageName + " [" + villageId + "]";
            nameLabel.Font = new Font("Segoe UI", 8f);
            nameLabel.ForeColor = TextPrimary;
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(16, 5);
            this.Controls.Add(nameLabel);

            _infraCheck = MakeCheck(265, settings.RepairInfrastructure);
            this.Controls.Add(_infraCheck);

            _troopsCheck = MakeCheck(320, settings.RepairTroops);
            this.Controls.Add(_troopsCheck);

            _sourceCombo = MakeCombo(365, 85);
            _sourceCombo.Items.AddRange(new object[] { "Local", "Cloud" });
            _sourceCombo.SelectedItem = settings.LayoutSource;
            if (_sourceCombo.SelectedIndex < 0) _sourceCombo.SelectedIndex = 0;
            this.Controls.Add(_sourceCombo);

            _infraPresetCombo = MakeCombo(455, 155);
            _infraPresetCombo.Items.Add("(none)");
            foreach (string name in infraPresets)
                _infraPresetCombo.Items.Add(name);
            if (!string.IsNullOrEmpty(settings.InfrastructurePresetName))
            {
                int idx = _infraPresetCombo.Items.IndexOf(settings.InfrastructurePresetName);
                _infraPresetCombo.SelectedIndex = idx >= 0 ? idx : 0;
            }
            else
            {
                _infraPresetCombo.SelectedIndex = 0;
            }
            this.Controls.Add(_infraPresetCombo);

            _troopPresetCombo = MakeCombo(615, 155);
            _troopPresetCombo.Items.Add("(none)");
            foreach (string name in troopPresets)
                _troopPresetCombo.Items.Add(name);
            if (!string.IsNullOrEmpty(settings.TroopPresetName))
            {
                int idx = _troopPresetCombo.Items.IndexOf(settings.TroopPresetName);
                _troopPresetCombo.SelectedIndex = idx >= 0 ? idx : 0;
            }
            else
            {
                _troopPresetCombo.SelectedIndex = 0;
            }
            this.Controls.Add(_troopPresetCombo);
        }

        public void LoadFromSettings(CastleRepairSettings settings)
        {
            VillageCastleRepairSettings vs = settings.GetVillageSettings(_villageId);
            _infraCheck.Checked = vs.RepairInfrastructure;
            _troopsCheck.Checked = vs.RepairTroops;
            _sourceCombo.SelectedItem = vs.LayoutSource;
            if (_sourceCombo.SelectedIndex < 0) _sourceCombo.SelectedIndex = 0;
            SelectPreset(_infraPresetCombo, vs.InfrastructurePresetName);
            SelectPreset(_troopPresetCombo, vs.TroopPresetName);
        }

        public void WriteToSettings(CastleRepairSettings settings)
        {
            VillageCastleRepairSettings vs = settings.GetVillageSettings(_villageId);
            vs.RepairInfrastructure = _infraCheck.Checked;
            vs.RepairTroops = _troopsCheck.Checked;
            vs.LayoutSource = _sourceCombo.SelectedItem as string ?? "Local";
            string infra = _infraPresetCombo.SelectedItem as string;
            vs.InfrastructurePresetName = (infra == "(none)") ? "" : (infra ?? "");
            string troop = _troopPresetCombo.SelectedItem as string;
            vs.TroopPresetName = (troop == "(none)") ? "" : (troop ?? "");
        }

        private static void SelectPreset(ComboBox combo, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                combo.SelectedIndex = 0;
                return;
            }
            int idx = combo.Items.IndexOf(name);
            combo.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private static CheckBox MakeCheck(int x, bool isChecked)
        {
            CheckBox cb = new CheckBox();
            cb.Checked = isChecked;
            cb.AutoSize = true;
            cb.Location = new Point(x, 5);
            cb.FlatStyle = FlatStyle.Flat;
            cb.ForeColor = TextPrimary;
            return cb;
        }

        private static ComboBox MakeCombo(int x, int w)
        {
            ComboBox cb = new ComboBox();
            cb.BackColor = InputBg;
            cb.ForeColor = TextPrimary;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.FlatStyle = FlatStyle.Flat;
            cb.Font = new Font("Segoe UI", 7.5f);
            cb.Location = new Point(x, 2);
            cb.Size = new Size(w, 20);
            return cb;
        }
    }
}
