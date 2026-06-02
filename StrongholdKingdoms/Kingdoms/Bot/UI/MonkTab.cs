using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonTypes;
using Kingdoms.Bot;

namespace Kingdoms.Bot.UI
{
    // =========================================================================
    // Route list row
    // =========================================================================

    internal class MonkRouteRow : Panel
    {
        private static readonly Color BgEven    = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd     = Color.FromArgb(36, 38, 48);
        private static readonly Color SelBg     = Color.FromArgb(50, 70, 110);
        private static readonly Color TextPri   = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec   = Color.FromArgb(160, 165, 180);

        private readonly int _routeIndex;
        private readonly CheckBox _enabledCheck;
        private readonly Label _nameLabel;
        private readonly Label _cmdLabel;
        private readonly Label _fromLabel;
        private readonly Label _toLabel;
        private readonly Label _stopLabel;
        private readonly Label _paramLabel;
        private bool _selected;

        public event Action<int, bool> EnabledToggled;  // (routeIndex, enabled)
        public event Action<int>       EditRequested;   // (routeIndex)
        public event Action<int>       DeleteRequested; // (routeIndex)

        public int  RouteIndex { get { return _routeIndex; } }
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                this.BackColor = _selected ? SelBg : (_routeIndex % 2 == 0 ? BgEven : BgOdd);
            }
        }

        public MonkRouteRow(int routeIndex, MonkRouteSettings route, bool alternate)
        {
            _routeIndex = routeIndex;
            this.BackColor = alternate ? BgOdd : BgEven;
            this.Height    = 26;
            this.Cursor    = Cursors.Hand;
            this.Click    += (s, e) => EditRequested?.Invoke(_routeIndex);

            _enabledCheck = new CheckBox();
            _enabledCheck.FlatStyle = FlatStyle.Flat;
            _enabledCheck.AutoSize  = true;
            _enabledCheck.Location  = new Point(6, 4);
            _enabledCheck.Checked   = route.Enabled;
            _enabledCheck.Click    += (s, e) => EnabledToggled?.Invoke(_routeIndex, _enabledCheck.Checked);
            this.Controls.Add(_enabledCheck);

            _nameLabel  = MkLabel(route.Name,                        28,  140, FontStyle.Bold);
            _cmdLabel   = MkLabel(route.Command.ToString(),          174,  90, FontStyle.Regular);
            _fromLabel  = MkLabel(route.FromVillages.Count + " from", 270, 65, FontStyle.Regular);
            _toLabel    = MkLabel(route.ToTargets.Count   + " to",    340, 65, FontStyle.Regular);
            _stopLabel  = MkLabel(StopCondLabel(route),               410, 130, FontStyle.Regular);
            _paramLabel = MkLabel(ParamLabel(route),                  546,  80, FontStyle.Regular);

            Button delBtn = new Button();
            delBtn.Text      = "✕";
            delBtn.FlatStyle = FlatStyle.Flat;
            delBtn.ForeColor = Color.FromArgb(200, 80, 80);
            delBtn.BackColor = Color.Transparent;
            delBtn.Font      = new Font("Segoe UI", 7.5f);
            delBtn.Size      = new Size(22, 20);
            delBtn.Location  = new Point(636, 3);
            delBtn.Click    += (s, e) => DeleteRequested?.Invoke(_routeIndex);
            this.Controls.Add(delBtn);

            this.Controls.Add(_nameLabel);
            this.Controls.Add(_cmdLabel);
            this.Controls.Add(_fromLabel);
            this.Controls.Add(_toLabel);
            this.Controls.Add(_stopLabel);
            this.Controls.Add(_paramLabel);
        }

        public void Refresh(MonkRouteSettings route)
        {
            _enabledCheck.Checked = route.Enabled;
            _nameLabel.Text       = route.Name;
            _cmdLabel.Text        = route.Command.ToString();
            _fromLabel.Text       = route.FromVillages.Count + " from";
            _toLabel.Text         = route.ToTargets.Count + " to";
            _stopLabel.Text       = StopCondLabel(route);
            _paramLabel.Text      = ParamLabel(route);
        }

        private static string StopCondLabel(MonkRouteSettings r)
        {
            switch (r.StopCondition)
            {
                case MonkStopCondition.QuestCompletion: return "Quest";
                case MonkStopCondition.SendXMonksEach:  return "Send X Each";
                case MonkStopCondition.RunOnCondition:  return "Condition";
                default: return r.StopCondition.ToString();
            }
        }

        private static string ParamLabel(MonkRouteSettings r)
        {
            if (r.StopCondition == MonkStopCondition.QuestCompletion) return "-";
            return r.ExtraParameter.ToString();
        }

        private Label MkLabel(string text, int x, int w, FontStyle style)
        {
            Label l = new Label();
            l.Text      = text;
            l.Font      = new Font("Segoe UI", 7.5f, style);
            l.ForeColor = TextPri;
            l.Location  = new Point(x, 5);
            l.Size      = new Size(w, 16);
            return l;
        }
    }

    // =========================================================================
    // Route editor popup
    // =========================================================================

    internal class MonkRouteEditorForm : Form
    {
        private static readonly Color FormBg   = Color.FromArgb(28, 30, 38);
        private static readonly Color PanelBg  = Color.FromArgb(36, 38, 48);
        private static readonly Color InputBg  = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri  = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec  = Color.FromArgb(160, 165, 180);
        private static readonly Color AccentCol = Color.FromArgb(80, 160, 255);

        // Top row controls
        private TextBox       _nameInput;
        private CheckBox      _enabledCheck;
        private ComboBox      _commandCombo;

        // From villages
        private CheckedListBox _fromList;

        // To targets
        private ListBox       _toList;
        private TextBox       _addTargetInput;
        private Button        _addTargetBtn;
        private Button        _addOwnVillageBtn;
        private Button        _removeTargetBtn;
        // Parish quick-add
        private NumericUpDown _parishRangeInput;
        private Button        _addInRangeBtn;
        private Button        _addMyParishesBtn;
        private Button        _addOwnedParishesBtn;

        // Stop condition + param
        private ComboBox      _stopCondCombo;
        private Label         _paramLabel;
        private NumericUpDown _paramInput;

        // Distance limit
        private CheckBox      _distLimitCheck;
        private NumericUpDown _distLimitInput;

        // Influence section
        private Panel         _influencePanel;
        private RadioButton   _influencePosRadio;
        private RadioButton   _influenceNegRadio;
        private NumericUpDown _influenceUserIdInput;

        // Buttons
        private Button _saveBtn;
        private Button _cancelBtn;

        private MonkRouteSettings _route;
        private bool _saved;
        public bool Saved { get { return _saved; } }

        public MonkRouteEditorForm(MonkRouteSettings route, string title)
        {
            _route = route;
            _saved = false;

            this.Text             = title;
            this.BackColor        = FormBg;
            this.ForeColor        = TextPri;
            this.Font             = new Font("Segoe UI", 9f);
            this.ClientSize       = new Size(760, 540);
            this.FormBorderStyle  = FormBorderStyle.FixedDialog;
            this.MaximizeBox      = false;
            this.MinimizeBox      = false;
            this.StartPosition    = FormStartPosition.CenterParent;
            this.ShowInTaskbar    = false;

            BuildUI();
            PopulateFromRoute();
            UpdateDynamicLabels();
        }

        private void BuildUI()
        {
            int y = 14;

            // ── Row 1: name + enabled + command ──
            this.Controls.Add(MkLabel("Route Name:", 14, y));
            _nameInput = MkTextBox(110, y - 2, 180);
            this.Controls.Add(_nameInput);

            _enabledCheck = new CheckBox();
            _enabledCheck.Text      = "Enabled";
            _enabledCheck.FlatStyle = FlatStyle.Flat;
            _enabledCheck.ForeColor = TextPri;
            _enabledCheck.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            _enabledCheck.Location  = new Point(306, y);
            _enabledCheck.AutoSize  = true;
            this.Controls.Add(_enabledCheck);

            this.Controls.Add(MkLabel("Command:", 420, y));
            _commandCombo = MkCombo(496, y - 2, 150);
            foreach (MonkCommand cmd in Enum.GetValues(typeof(MonkCommand)))
                _commandCombo.Items.Add(cmd);
            _commandCombo.SelectedIndexChanged += (s, e) => UpdateDynamicLabels();
            this.Controls.Add(_commandCombo);

            y += 36;

            // ── Row 2: from-villages + to-targets ──
            this.Controls.Add(MkLabel("From Villages (own):", 14, y));

            // Select All / None micro-buttons beside the from-list label
            Button selAllBtn = new Button();
            selAllBtn.Text      = "All";
            selAllBtn.FlatStyle = FlatStyle.Flat;
            selAllBtn.ForeColor = TextSec;
            selAllBtn.BackColor = Color.FromArgb(44, 46, 58);
            selAllBtn.Font      = new Font("Segoe UI", 7f);
            selAllBtn.Size      = new Size(32, 18);
            selAllBtn.Location  = new Point(160, y - 1);
            selAllBtn.Click    += delegate
            {
                for (int i = 0; i < _fromList.Items.Count; i++)
                    _fromList.SetItemChecked(i, true);
            };
            this.Controls.Add(selAllBtn);

            Button selNoneBtn = new Button();
            selNoneBtn.Text      = "None";
            selNoneBtn.FlatStyle = FlatStyle.Flat;
            selNoneBtn.ForeColor = TextSec;
            selNoneBtn.BackColor = Color.FromArgb(44, 46, 58);
            selNoneBtn.Font      = new Font("Segoe UI", 7f);
            selNoneBtn.Size      = new Size(38, 18);
            selNoneBtn.Location  = new Point(196, y - 1);
            selNoneBtn.Click    += delegate
            {
                for (int i = 0; i < _fromList.Items.Count; i++)
                    _fromList.SetItemChecked(i, false);
            };
            this.Controls.Add(selNoneBtn);

            this.Controls.Add(MkLabel("To Targets (IDs):", 300, y));

            y += 18;
            _fromList = new CheckedListBox();
            _fromList.BackColor     = InputBg;
            _fromList.ForeColor     = TextPri;
            _fromList.Font          = new Font("Segoe UI", 8f);
            _fromList.BorderStyle   = BorderStyle.FixedSingle;
            _fromList.Location      = new Point(14, y);
            _fromList.Size          = new Size(270, 150);
            _fromList.CheckOnClick  = true;
            this.Controls.Add(_fromList);

            _toList = new ListBox();
            _toList.BackColor   = InputBg;
            _toList.ForeColor   = TextPri;
            _toList.Font        = new Font("Segoe UI", 8f);
            _toList.BorderStyle = BorderStyle.FixedSingle;
            _toList.Location    = new Point(300, y);
            _toList.Size        = new Size(220, 150);
            this.Controls.Add(_toList);

            // Target management buttons (beside to-list)
            int bx = 530;
            _addTargetInput = MkTextBox(bx, y, 100);
            _addTargetInput.Text = "Village ID";
            _addTargetInput.ForeColor = Color.FromArgb(100, 110, 130);
            _addTargetInput.GotFocus += (s, e) =>
            {
                if (_addTargetInput.Text == "Village ID")
                { _addTargetInput.Text = ""; _addTargetInput.ForeColor = TextPri; }
            };
            _addTargetInput.LostFocus += (s, e) =>
            {
                if (string.IsNullOrEmpty(_addTargetInput.Text.Trim()))
                { _addTargetInput.Text = "Village ID"; _addTargetInput.ForeColor = Color.FromArgb(100, 110, 130); }
            };
            this.Controls.Add(_addTargetInput);

            _addTargetBtn = MkButton("Add", Color.FromArgb(50, 80, 50), bx, y + 28, 100);
            _addTargetBtn.Click += delegate { AddTargetById(); };
            this.Controls.Add(_addTargetBtn);

            _addOwnVillageBtn = MkButton("Add Own Village", Color.FromArgb(50, 55, 80), bx, y + 60, 130);
            _addOwnVillageBtn.Click += delegate { AddSelectedOwnVillage(); };
            this.Controls.Add(_addOwnVillageBtn);

            _removeTargetBtn = MkButton("Remove", Color.FromArgb(80, 40, 40), bx, y + 92, 100);
            _removeTargetBtn.Click += delegate { RemoveSelectedTarget(); };
            this.Controls.Add(_removeTargetBtn);

            // ── Parish quick-add row (sits just below the to-targets list) ──
            // All elements confined to x=300..750 (450px) so nothing clips the form edge.
            int qy = y + 156;   // ~6px gap below the 150-high to-list

            Label rangeLbl = MkLabel("Range:", 300, qy + 5);
            this.Controls.Add(rangeLbl);

            _parishRangeInput = MkNumeric(346, qy + 3, 1, 9999, 32);
            _parishRangeInput.Size = new Size(55, 22);
            this.Controls.Add(_parishRangeInput);

            // x=406 → 406+110=516
            _addInRangeBtn = MkButton("Add In Range", Color.FromArgb(45, 65, 95), 406, qy, 110);
            _addInRangeBtn.Click += delegate { AddParishesInRange((int)_parishRangeInput.Value); };
            this.Controls.Add(_addInRangeBtn);

            // x=521 → 521+105=626
            _addMyParishesBtn = MkButton("My Parishes", Color.FromArgb(50, 75, 50), 521, qy, 105);
            _addMyParishesBtn.Click += delegate { AddMyParishes(); };
            this.Controls.Add(_addMyParishesBtn);

            // x=631 → 631+119=750
            _addOwnedParishesBtn = MkButton("Owned Parishes", Color.FromArgb(75, 55, 90), 631, qy, 119);
            _addOwnedParishesBtn.Click += delegate { AddOwnedParishes(); };
            this.Controls.Add(_addOwnedParishesBtn);

            y += 196;   // 160 original + 36 for the quick-add row

            // ── Row 3: stop condition + extra parameter ──
            this.Controls.Add(MkLabel("Stop Condition:", 14, y + 2));
            _stopCondCombo = MkCombo(120, y, 160);
            _stopCondCombo.Items.Add("Quest Completion");
            _stopCondCombo.Items.Add("Send X Monks Each");
            _stopCondCombo.Items.Add("Run On Condition");
            _stopCondCombo.SelectedIndexChanged += (s, e) => UpdateDynamicLabels();
            this.Controls.Add(_stopCondCombo);

            _paramLabel = MkLabel("Monks per target:", 300, y + 2);
            this.Controls.Add(_paramLabel);

            _paramInput = MkNumeric(440, y, 0, 9999, 5);
            this.Controls.Add(_paramInput);

            y += 36;

            // ── Row 4: distance limit ──
            _distLimitCheck = new CheckBox();
            _distLimitCheck.Text      = "Limit distance:";
            _distLimitCheck.FlatStyle = FlatStyle.Flat;
            _distLimitCheck.ForeColor = TextPri;
            _distLimitCheck.Location  = new Point(14, y);
            _distLimitCheck.AutoSize  = true;
            this.Controls.Add(_distLimitCheck);

            _distLimitInput = MkNumeric(132, y, 1, 9999, 100);
            this.Controls.Add(_distLimitInput);

            y += 36;

            // ── Row 5: influence panel (conditionally visible) ──
            _influencePanel = new Panel();
            _influencePanel.BackColor = PanelBg;
            _influencePanel.Location  = new Point(14, y);
            _influencePanel.Size      = new Size(500, 36);
            _influencePanel.Visible   = false;

            _influencePosRadio = new RadioButton();
            _influencePosRadio.Text      = "Positive Influence";
            _influencePosRadio.FlatStyle = FlatStyle.Flat;
            _influencePosRadio.ForeColor = TextPri;
            _influencePosRadio.Location  = new Point(4, 8);
            _influencePosRadio.AutoSize  = true;
            _influencePosRadio.Checked   = true;
            _influencePanel.Controls.Add(_influencePosRadio);

            _influenceNegRadio = new RadioButton();
            _influenceNegRadio.Text      = "Negative Influence";
            _influenceNegRadio.FlatStyle = FlatStyle.Flat;
            _influenceNegRadio.ForeColor = TextPri;
            _influenceNegRadio.Location  = new Point(160, 8);
            _influenceNegRadio.AutoSize  = true;
            _influencePanel.Controls.Add(_influenceNegRadio);

            Label userIdLbl = MkLabel("Target User ID:", 330, 8);
            _influencePanel.Controls.Add(userIdLbl);

            _influenceUserIdInput = MkNumeric(440, 6, -1, 9999999, -1);
            _influencePanel.Controls.Add(_influenceUserIdInput);

            this.Controls.Add(_influencePanel);

            y += 44;

            // ── Buttons ──
            _saveBtn = MkButton("Save", AccentCol, this.ClientSize.Width - 196, this.ClientSize.Height - 40, 90);
            _saveBtn.Click += delegate { SaveAndClose(); };
            this.Controls.Add(_saveBtn);

            _cancelBtn = MkButton("Cancel", Color.FromArgb(70, 72, 84), this.ClientSize.Width - 100, this.ClientSize.Height - 40, 86);
            _cancelBtn.Click += delegate { this.Close(); };
            this.Controls.Add(_cancelBtn);
        }

        private void PopulateFromRoute()
        {
            _nameInput.Text       = _route.Name;
            _enabledCheck.Checked = _route.Enabled;

            // Command combo
            for (int i = 0; i < _commandCombo.Items.Count; i++)
            {
                if ((MonkCommand)_commandCombo.Items[i] == _route.Command)
                { _commandCombo.SelectedIndex = i; break; }
            }
            if (_commandCombo.SelectedIndex < 0) _commandCombo.SelectedIndex = 0;

            // Stop condition combo
            _stopCondCombo.SelectedIndex = (int)_route.StopCondition;
            _paramInput.Value = Clamp(_paramInput, _route.ExtraParameter);
            _distLimitCheck.Checked = _route.IsDistanceLimited;
            _distLimitInput.Value   = Clamp(_distLimitInput, _route.DistanceLimit);
            _influencePosRadio.Checked = _route.InfluencePositive;
            _influenceNegRadio.Checked = !_route.InfluencePositive;
            _influenceUserIdInput.Value = Clamp(_influenceUserIdInput, _route.InfluenceTargetUserId);

            // Populate from-villages from own villages
            try
            {
                List<WorldMap.UserVillageData> uvds = GameEngine.Instance.World.getUserVillageList();
                if (uvds != null)
                {
                    foreach (WorldMap.UserVillageData uvd in uvds)
                    {
                        string name = GameEngine.Instance.World.getVillageName(uvd.villageID);
                        string display = "[" + uvd.villageID + "] " + name;
                        int idx = _fromList.Items.Add(new VillageItem(uvd.villageID, display));
                        if (_route.FromVillages.Contains(uvd.villageID))
                            _fromList.SetItemChecked(idx, true);
                    }
                }
            }
            catch { }

            // Populate to-targets
            foreach (int id in _route.ToTargets)
                _toList.Items.Add(new TargetItem(id));
        }

        private void UpdateDynamicLabels()
        {
            MonkCommand cmd = _commandCombo.SelectedIndex >= 0
                ? (MonkCommand)_commandCombo.Items[_commandCombo.SelectedIndex]
                : MonkCommand.Blessing;

            MonkStopCondition cond = _stopCondCombo.SelectedIndex >= 0
                ? (MonkStopCondition)_stopCondCombo.SelectedIndex
                : MonkStopCondition.SendXMonksEach;

            bool questMode = cond == MonkStopCondition.QuestCompletion;
            _paramLabel.Visible = !questMode;
            _paramInput.Visible = !questMode;

            if (!questMode)
            {
                if (cond == MonkStopCondition.SendXMonksEach)
                {
                    _paramLabel.Text = "Monks per target:";
                }
                else
                {
                    switch (cmd)
                    {
                        case MonkCommand.Interdiction:
                            _paramLabel.Text = "Target hours of interdict:"; break;
                        case MonkCommand.Restoration:
                            _paramLabel.Text = "Max disease (0=fully heal):"; break;
                        case MonkCommand.Blessing:
                            _paramLabel.Text = "Target blessing level (1-100):"; break;
                        case MonkCommand.Inquisition:
                            _paramLabel.Text = "Target inquisition level (1-100):"; break;
                        case MonkCommand.Absolution:
                            _paramLabel.Text = "Absolve if excomm > X hours:"; break;
                        case MonkCommand.Excommunication:
                            _paramLabel.Text = "Target hours of excomm:"; break;
                        default:
                            _paramLabel.Text = "Parameter:"; break;
                    }
                }
            }

            _influencePanel.Visible = cmd == MonkCommand.Influence;
        }

        private void AddTargetById()
        {
            string text = _addTargetInput.Text.Trim();
            if (string.IsNullOrEmpty(text) || text == "Village ID") return;

            foreach (string part in text.Split(new char[]{',', ' '}, StringSplitOptions.RemoveEmptyEntries))
            {
                int id;
                if (int.TryParse(part, out id))
                {
                    bool already = false;
                    foreach (object item in _toList.Items)
                        if (((TargetItem)item).VillageId == id) { already = true; break; }
                    if (!already) _toList.Items.Add(new TargetItem(id));
                }
            }
            _addTargetInput.Clear();
        }

        private void AddSelectedOwnVillage()
        {
            foreach (int i in _fromList.CheckedIndices)
            {
                int id = ((VillageItem)_fromList.Items[i]).VillageId;
                bool already = false;
                foreach (object item in _toList.Items)
                    if (((TargetItem)item).VillageId == id) { already = true; break; }
                if (!already) _toList.Items.Add(new TargetItem(id));
            }
        }

        private void RemoveSelectedTarget()
        {
            while (_toList.SelectedIndices.Count > 0)
                _toList.Items.RemoveAt(_toList.SelectedIndices[0]);
        }

        // Adds parish capitals of all parishes within `range` tiles of any checked
        // from-village (or any own village if nothing is checked).
        private void AddParishesInRange(int range)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;

            List<int> fromIds = GetCheckedFromIds();

            int rangeSq = range * range;
            int numParishes = GameEngine.Instance.World.getNumParishes();

            for (int parishId = 0; parishId < numParishes; parishId++)
            {
                int capitalId = GameEngine.Instance.World.getParishCapital(parishId);
                if (capitalId < 0) continue;

                bool inRange = false;
                foreach (int fromId in fromIds)
                {
                    if (GameEngine.Instance.World.getSquareDistance(fromId, capitalId) <= rangeSq)
                    { inRange = true; break; }
                }
                if (!inRange) continue;

                AddToListIfAbsent(capitalId);
            }
        }

        // Adds the parish capital of every parish that contains at least one of
        // the player's own villages.
        private void AddMyParishes()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;

            List<WorldMap.UserVillageData> uvds = GameEngine.Instance.World.getUserVillageList();
            if (uvds == null) return;

            HashSet<int> seen = new HashSet<int>();
            foreach (WorldMap.UserVillageData uvd in uvds)
            {
                int parishId = GameEngine.Instance.World.getParishFromVillageID(uvd.villageID);
                if (parishId < 0 || seen.Contains(parishId)) continue;
                seen.Add(parishId);

                int capitalId = GameEngine.Instance.World.getParishCapital(parishId);
                if (capitalId >= 0)
                    AddToListIfAbsent(capitalId);
            }
        }

        // Adds every parish capital that the player owns (i.e. their UserVillageData
        // entry has parishCapital == true, meaning they hold that capital village).
        private void AddOwnedParishes()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;

            List<WorldMap.UserVillageData> uvds = GameEngine.Instance.World.getUserVillageList();
            if (uvds == null) return;

            foreach (WorldMap.UserVillageData uvd in uvds)
            {
                if (uvd.parishCapital)
                    AddToListIfAbsent(uvd.villageID);
            }
        }

        // Returns IDs of checked from-villages, falling back to all own villages.
        private List<int> GetCheckedFromIds()
        {
            List<int> ids = new List<int>();
            foreach (int i in _fromList.CheckedIndices)
                ids.Add(((VillageItem)_fromList.Items[i]).VillageId);

            if (ids.Count == 0)
            {
                try
                {
                    List<WorldMap.UserVillageData> uvds = GameEngine.Instance.World.getUserVillageList();
                    if (uvds != null)
                        foreach (WorldMap.UserVillageData uvd in uvds)
                            ids.Add(uvd.villageID);
                }
                catch { }
            }
            return ids;
        }

        // Adds `id` to the to-list only if not already present.
        private void AddToListIfAbsent(int id)
        {
            foreach (object item in _toList.Items)
                if (((TargetItem)item).VillageId == id) return;
            _toList.Items.Add(new TargetItem(id));
        }

        private void SaveAndClose()
        {
            _route.Name    = _nameInput.Text.Trim();
            _route.Enabled = _enabledCheck.Checked;

            if (_commandCombo.SelectedIndex >= 0)
                _route.Command = (MonkCommand)_commandCombo.Items[_commandCombo.SelectedIndex];

            _route.FromVillages.Clear();
            foreach (int i in _fromList.CheckedIndices)
                _route.FromVillages.Add(((VillageItem)_fromList.Items[i]).VillageId);

            _route.ToTargets.Clear();
            foreach (object item in _toList.Items)
                _route.ToTargets.Add(((TargetItem)item).VillageId);

            _route.StopCondition     = (MonkStopCondition)Math.Max(0, _stopCondCombo.SelectedIndex);
            _route.ExtraParameter    = (int)_paramInput.Value;
            _route.IsDistanceLimited = _distLimitCheck.Checked;
            _route.DistanceLimit     = (int)_distLimitInput.Value;
            _route.InfluencePositive = _influencePosRadio.Checked;
            _route.InfluenceTargetUserId = (int)_influenceUserIdInput.Value;

            _saved = true;
            this.Close();
        }

        // ── Helpers ──

        private static decimal Clamp(NumericUpDown n, decimal v)
        {
            if (v < n.Minimum) return n.Minimum;
            if (v > n.Maximum) return n.Maximum;
            return v;
        }

        private Label MkLabel(string text, int x, int y)
        {
            Label l = new Label();
            l.Text      = text;
            l.ForeColor = TextSec;
            l.Location  = new Point(x, y);
            l.AutoSize  = true;
            return l;
        }

        private TextBox MkTextBox(int x, int y, int w)
        {
            TextBox tb = new TextBox();
            tb.BackColor    = InputBg;
            tb.ForeColor    = TextPri;
            tb.BorderStyle  = BorderStyle.FixedSingle;
            tb.Font         = new Font("Segoe UI", 9f);
            tb.Location     = new Point(x, y);
            tb.Size         = new Size(w, 22);
            return tb;
        }

        private ComboBox MkCombo(int x, int y, int w)
        {
            ComboBox cb = new ComboBox();
            cb.BackColor    = InputBg;
            cb.ForeColor    = TextPri;
            cb.FlatStyle    = FlatStyle.Flat;
            cb.Font         = new Font("Segoe UI", 8.5f);
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Location     = new Point(x, y);
            cb.Size         = new Size(w, 22);
            return cb;
        }

        private NumericUpDown MkNumeric(int x, int y, decimal min, decimal max, decimal val)
        {
            NumericUpDown n = new NumericUpDown();
            n.BackColor = InputBg;
            n.ForeColor = TextPri;
            n.BorderStyle = BorderStyle.FixedSingle;
            n.Font      = new Font("Segoe UI", 8.5f);
            n.Location  = new Point(x, y);
            n.Size      = new Size(80, 22);
            n.Minimum   = min;
            n.Maximum   = max;
            n.Value     = Clamp(n, val);
            return n;
        }

        private Button MkButton(string text, Color bg, int x, int y, int w)
        {
            Button b = new Button();
            b.Text      = text;
            b.BackColor = bg;
            b.ForeColor = TextPri;
            b.FlatStyle = FlatStyle.Flat;
            b.Font      = new Font("Segoe UI", 9f);
            b.Location  = new Point(x, y);
            b.Size      = new Size(w, 28);
            b.UseVisualStyleBackColor = false;
            return b;
        }

        // ── Inner display helpers ──

        private class VillageItem
        {
            public readonly int    VillageId;
            private readonly string _display;
            public VillageItem(int id, string display) { VillageId = id; _display = display; }
            public override string ToString() { return _display; }
        }

        private class TargetItem
        {
            public readonly int VillageId;
            public TargetItem(int id) { VillageId = id; }
            public override string ToString()
            {
                try
                {
                    if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                    {
                        string name = GameEngine.Instance.World.getVillageName(VillageId);
                        if (!string.IsNullOrEmpty(name)) return "[" + VillageId + "] " + name;
                    }
                }
                catch { }
                return "[" + VillageId + "]";
            }
        }
    }
}
