using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Kingdoms.Bot.UI
{
    // =========================================================================
    // Markets tab: resource grid for selected village
    // =========================================================================

    internal class TradeResourceGrid : Panel
    {
        private static readonly Color HeaderBg = Color.FromArgb(36, 38, 50);
        private static readonly Color RowEven = Color.FromArgb(30, 32, 40);
        private static readonly Color RowOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);

        private TradeResourceRow[] _rows;

        private Panel _headerPanel;
        private const int RowHeight = 24;
        private const int HeaderHeight = 22;

        public TradeResourceGrid()
        {
            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(24, 24, 32);

            _headerPanel = new Panel();
            _headerPanel.Location = new Point(0, 0);
            _headerPanel.Size = new Size(480, HeaderHeight);
            _headerPanel.BackColor = HeaderBg;

            int[] colX = new int[] { 12, 100, 150, 210, 280, 340, 400 };
            string[] colT = new string[] { "Type", "Sell", "Min", "Sell Limit", "Buy", "Max", "Buy Limit" };
            for (int i = 0; i < colT.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = colT[i];
                lbl.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
                lbl.ForeColor = TextSec;
                lbl.AutoSize = true;
                lbl.Location = new Point(colX[i], 4);
                _headerPanel.Controls.Add(lbl);
            }
            this.Controls.Add(_headerPanel);
        }

        public void LoadVillage(VillageMarketTradeInfo info)
        {
            BuildRows(info);
        }

        private void BuildRows(VillageMarketTradeInfo info)
        {
            // Remove old rows
            if (_rows != null)
            {
                for (int i = 0; i < _rows.Length; i++)
                {
                    this.Controls.Remove(_rows[i]);
                    _rows[i].Dispose();
                }
            }

            byte[] ids = TradeModuleConstants.TradeTypeIds;
            _rows = new TradeResourceRow[ids.Length];

            for (int i = 0; i < ids.Length; i++)
            {
                TradeTypeEntry entry = info != null ? info.GetTradeType(ids[i]) : null;
                _rows[i] = new TradeResourceRow(ids[i], entry, i);
                _rows[i].Location = new Point(0, HeaderHeight + i * RowHeight);
                _rows[i].Size = new Size(480, RowHeight);
                this.Controls.Add(_rows[i]);
            }
        }

        public void WriteToInfo(VillageMarketTradeInfo info)
        {
            if (_rows == null || info == null) return;
            for (int i = 0; i < _rows.Length; i++)
                _rows[i].WriteToEntry(info);
        }
    }

    internal class TradeResourceRow : Panel
    {
        private static readonly Color RowEven = Color.FromArgb(30, 32, 40);
        private static readonly Color RowOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);

        private byte _resourceId;
        private CheckBox _sellCheck;
        private NumericUpDown _minSellPrice;
        private NumericUpDown _sellLimit;
        private CheckBox _buyCheck;
        private NumericUpDown _maxBuyPrice;
        private NumericUpDown _buyLimit;

        public TradeResourceRow(byte resourceId, TradeTypeEntry entry, int index)
        {
            _resourceId = resourceId;
            this.BackColor = index % 2 == 0 ? RowEven : RowOdd;

            Label nameLabel = new Label();
            nameLabel.Text = TradeModuleConstants.GetResourceName((int)resourceId);
            nameLabel.Font = new Font("Segoe UI", 8f);
            nameLabel.ForeColor = TextPri;
            nameLabel.Location = new Point(12, 3);
            nameLabel.Size = new Size(85, 18);
            this.Controls.Add(nameLabel);

            _sellCheck = MakeCheck(104, entry != null && entry.Sell);
            this.Controls.Add(_sellCheck);

            _minSellPrice = MakeNumeric(148, 0, 999, entry != null ? entry.MinSellPrice : 0, 54);
            this.Controls.Add(_minSellPrice);

            _sellLimit = MakeNumeric(210, 0, 99999, entry != null ? entry.SellLimit : 0, 62);
            this.Controls.Add(_sellLimit);

            _buyCheck = MakeCheck(284, entry != null && entry.Buy);
            this.Controls.Add(_buyCheck);

            _maxBuyPrice = MakeNumeric(338, 0, 999, entry != null ? entry.MaxBuyPrice : 150, 54);
            this.Controls.Add(_maxBuyPrice);

            _buyLimit = MakeNumeric(400, 0, 99999, entry != null ? entry.BuyLimit : 0, 62);
            this.Controls.Add(_buyLimit);
        }

        public void WriteToEntry(VillageMarketTradeInfo info)
        {
            TradeTypeEntry entry = info.GetTradeType(_resourceId);
            entry.Sell = _sellCheck.Checked;
            entry.MinSellPrice = (int)_minSellPrice.Value;
            entry.SellLimit = (int)_sellLimit.Value;
            entry.Buy = _buyCheck.Checked;
            entry.MaxBuyPrice = (int)_maxBuyPrice.Value;
            entry.BuyLimit = (int)_buyLimit.Value;
        }

        private static CheckBox MakeCheck(int x, bool val)
        {
            CheckBox cb = new CheckBox();
            cb.FlatStyle = FlatStyle.Flat;
            cb.AutoSize = true;
            cb.Location = new Point(x, 3);
            cb.Checked = val;
            return cb;
        }

        private static NumericUpDown MakeNumeric(int x, int min, int max, int value, int w)
        {
            NumericUpDown nud = new NumericUpDown();
            nud.BackColor = InputBg;
            nud.ForeColor = Color.FromArgb(230, 230, 240);
            nud.BorderStyle = BorderStyle.FixedSingle;
            nud.Font = new Font("Segoe UI", 7.5f);
            nud.Location = new Point(x, 2);
            nud.Size = new Size(w, 20);
            nud.Maximum = max;
            nud.Minimum = min;
            nud.Value = Math.Max(min, Math.Min(max, value));
            return nud;
        }
    }

    // =========================================================================
    // Routes tab: grid row per route
    // =========================================================================

    internal class TradeRouteRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);

        private int _routeIndex;
        private CheckBox _enabledCheck;
        private Label _nameLabel;
        private Label _fromLabel;
        private Label _toLabel;
        private Label _resourceLabel;
        private Label _keepMinLabel;
        private Label _maxMerchLabel;
        private Label _sendMaxLabel;
        private Label _distLabel;
        private bool _selected;

        public int RouteIndex { get { return _routeIndex; } }
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                this.BackColor = _selected
                    ? Color.FromArgb(50, 70, 110)
                    : (_routeIndex % 2 == 0 ? BgEven : BgOdd);
            }
        }

        public TradeRouteRow(int routeIndex, TradeRouteSettings route, bool alternate)
        {
            _routeIndex = routeIndex;
            this.BackColor = alternate ? BgOdd : BgEven;
            this.Height = 26;
            this.Cursor = Cursors.Hand;

            _enabledCheck = new CheckBox();
            _enabledCheck.FlatStyle = FlatStyle.Flat;
            _enabledCheck.AutoSize = true;
            _enabledCheck.Location = new Point(6, 4);
            _enabledCheck.Checked = route.Enabled;
            this.Controls.Add(_enabledCheck);

            _nameLabel = MakeLabel(route.Name, 28, 130, FontStyle.Bold);
            this.Controls.Add(_nameLabel);

            string fromText = route.FromVillages.Count + " village(s)";
            _fromLabel = MakeLabel(fromText, 164, 85, FontStyle.Regular);
            this.Controls.Add(_fromLabel);

            string toText = route.ToVillages.Count + " village(s)";
            _toLabel = MakeLabel(toText, 254, 85, FontStyle.Regular);
            this.Controls.Add(_toLabel);

            string resText = BuildResourceSummary(route.Resources);
            _resourceLabel = MakeLabel(resText, 344, 160, FontStyle.Regular);
            this.Controls.Add(_resourceLabel);

            _keepMinLabel = MakeLabel(route.KeepMinimum.ToString(), 510, 50, FontStyle.Regular);
            this.Controls.Add(_keepMinLabel);

            _maxMerchLabel = MakeLabel(route.MaxMerchantsPerTransaction.ToString(), 566, 40, FontStyle.Regular);
            this.Controls.Add(_maxMerchLabel);

            _sendMaxLabel = MakeLabel(route.SendMaximum.ToString(), 612, 55, FontStyle.Regular);
            this.Controls.Add(_sendMaxLabel);

            string dist = route.IsDistanceLimited ? route.DistanceLimit.ToString() : "Off";
            _distLabel = MakeLabel(dist, 674, 50, FontStyle.Regular);
            this.Controls.Add(_distLabel);
        }

        public void WriteToSettings(TradeSettings settings)
        {
            if (_routeIndex < 0 || _routeIndex >= settings.Routes.Count)
                return;
            settings.Routes[_routeIndex].Enabled = _enabledCheck.Checked;
        }

        public void Refresh(TradeRouteSettings route)
        {
            _enabledCheck.Checked = route.Enabled;
            _nameLabel.Text = route.Name;
            _fromLabel.Text = route.FromVillages.Count + " village(s)";
            _toLabel.Text = route.ToVillages.Count + " village(s)";
            _resourceLabel.Text = BuildResourceSummary(route.Resources);
            _keepMinLabel.Text = route.KeepMinimum.ToString();
            _maxMerchLabel.Text = route.MaxMerchantsPerTransaction.ToString();
            _sendMaxLabel.Text = route.SendMaximum.ToString();
            _distLabel.Text = route.IsDistanceLimited ? route.DistanceLimit.ToString() : "Off";
        }

        private static string BuildResourceSummary(List<int> resources)
        {
            if (resources.Count == 0) return "(none)";
            string s = "";
            for (int i = 0; i < resources.Count && i < 3; i++)
            {
                if (i > 0) s += ", ";
                s += TradeModuleConstants.GetResourceName(resources[i]);
            }
            if (resources.Count > 3) s += "...";
            return s;
        }

        private Label MakeLabel(string text, int x, int w, FontStyle style)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 7.5f, style);
            lbl.ForeColor = TextPri;
            lbl.Location = new Point(x, 5);
            lbl.Size = new Size(w, 16);
            return lbl;
        }
    }

    // =========================================================================
    // Route Editor popup form
    // =========================================================================

    internal class TradeRouteEditorForm : Form
    {
        private static readonly Color FormBg = Color.FromArgb(28, 30, 38);
        private static readonly Color PanelBg = Color.FromArgb(36, 38, 48);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color AccentCol = Color.FromArgb(80, 160, 255);

        private TextBox _nameInput;
        private CheckBox _enabledCheck;
        private ListBox _fromList;
        private ListBox _toList;
        private CheckedListBox _resourceList;
        private NumericUpDown _keepMinInput;
        private NumericUpDown _maxMerchantsInput;
        private NumericUpDown _sendMaxInput;
        private CheckBox _distLimitCheck;
        private NumericUpDown _distLimitInput;
        private Button _saveBtn;
        private Button _cancelBtn;

        private TradeRouteSettings _route;
        private bool _saved;

        public bool Saved { get { return _saved; } }

        public TradeRouteEditorForm(TradeRouteSettings route, string title)
        {
            _route = route;
            _saved = false;

            this.Text = title;
            this.BackColor = FormBg;
            this.ForeColor = TextPri;
            this.Font = new Font("Segoe UI", 9f);
            this.ClientSize = new Size(720, 440);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;

            BuildUI();
            PopulateFromRoute();
        }

        private void BuildUI()
        {
            // Route name + enabled
            Label nameLbl = MakeLabel("Route Name:", 14, 14);
            this.Controls.Add(nameLbl);
            _nameInput = new TextBox();
            _nameInput.BackColor = InputBg;
            _nameInput.ForeColor = TextPri;
            _nameInput.BorderStyle = BorderStyle.FixedSingle;
            _nameInput.Font = new Font("Segoe UI", 9f);
            _nameInput.Location = new Point(110, 12);
            _nameInput.Size = new Size(180, 22);
            this.Controls.Add(_nameInput);

            _enabledCheck = new CheckBox();
            _enabledCheck.Text = "Enabled";
            _enabledCheck.FlatStyle = FlatStyle.Flat;
            _enabledCheck.ForeColor = TextPri;
            _enabledCheck.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            _enabledCheck.Location = new Point(310, 12);
            _enabledCheck.AutoSize = true;
            this.Controls.Add(_enabledCheck);

            // From villages
            Label fromLbl = MakeLabel("From Villages:", 14, 44);
            this.Controls.Add(fromLbl);
            _fromList = new ListBox();
            _fromList.BackColor = InputBg;
            _fromList.ForeColor = TextPri;
            _fromList.Font = new Font("Segoe UI", 8f);
            _fromList.BorderStyle = BorderStyle.FixedSingle;
            _fromList.Location = new Point(14, 64);
            _fromList.Size = new Size(210, 200);
            _fromList.SelectionMode = SelectionMode.MultiExtended;
            this.Controls.Add(_fromList);

            // To villages
            Label toLbl = MakeLabel("To Villages:", 240, 44);
            this.Controls.Add(toLbl);
            _toList = new ListBox();
            _toList.BackColor = InputBg;
            _toList.ForeColor = TextPri;
            _toList.Font = new Font("Segoe UI", 8f);
            _toList.BorderStyle = BorderStyle.FixedSingle;
            _toList.Location = new Point(240, 64);
            _toList.Size = new Size(210, 200);
            _toList.SelectionMode = SelectionMode.MultiExtended;
            this.Controls.Add(_toList);

            // Resources
            Label resLbl = MakeLabel("Resources:", 466, 44);
            this.Controls.Add(resLbl);
            _resourceList = new CheckedListBox();
            _resourceList.BackColor = InputBg;
            _resourceList.ForeColor = TextPri;
            _resourceList.Font = new Font("Segoe UI", 8f);
            _resourceList.BorderStyle = BorderStyle.FixedSingle;
            _resourceList.Location = new Point(466, 64);
            _resourceList.Size = new Size(240, 200);
            _resourceList.CheckOnClick = true;
            this.Controls.Add(_resourceList);

            // Settings row
            int sy = 280;
            Label keepLbl = MakeLabel("Keep minimum in source:", 14, sy);
            this.Controls.Add(keepLbl);
            _keepMinInput = MakeNumeric(210, sy - 2, 0, 99999, 0);
            this.Controls.Add(_keepMinInput);

            Label maxMerLbl = MakeLabel("Max merchants per transaction:", 290, sy);
            this.Controls.Add(maxMerLbl);
            _maxMerchantsInput = MakeNumeric(510, sy - 2, 1, 500, 5);
            this.Controls.Add(_maxMerchantsInput);

            sy += 32;
            Label sendLbl = MakeLabel("Max amount to store in target:", 14, sy);
            this.Controls.Add(sendLbl);
            _sendMaxInput = MakeNumeric(210, sy - 2, 0, 99999, 5000);
            this.Controls.Add(_sendMaxInput);

            _distLimitCheck = new CheckBox();
            _distLimitCheck.Text = "Limit distance:";
            _distLimitCheck.FlatStyle = FlatStyle.Flat;
            _distLimitCheck.ForeColor = TextPri;
            _distLimitCheck.Font = new Font("Segoe UI", 8.5f);
            _distLimitCheck.Location = new Point(290, sy);
            _distLimitCheck.AutoSize = true;
            this.Controls.Add(_distLimitCheck);

            _distLimitInput = MakeNumeric(420, sy - 2, 1, 9999, 100);
            this.Controls.Add(_distLimitInput);

            // Buttons
            int by = 390;
            _saveBtn = MakeButton("Save", AccentCol, 500, by);
            _saveBtn.Click += delegate { SaveAndClose(); };
            this.Controls.Add(_saveBtn);

            _cancelBtn = MakeButton("Cancel", Color.FromArgb(70, 72, 84), 610, by);
            _cancelBtn.Click += delegate { this.Close(); };
            this.Controls.Add(_cancelBtn);
        }

        private void PopulateFromRoute()
        {
            _nameInput.Text = _route.Name;
            _enabledCheck.Checked = _route.Enabled;
            _keepMinInput.Value = Clamp(_keepMinInput, _route.KeepMinimum);
            _maxMerchantsInput.Value = Clamp(_maxMerchantsInput, _route.MaxMerchantsPerTransaction);
            _sendMaxInput.Value = Clamp(_sendMaxInput, _route.SendMaximum);
            _distLimitCheck.Checked = _route.IsDistanceLimited;
            _distLimitInput.Value = Clamp(_distLimitInput, _route.DistanceLimit);

            // Populate village lists
            List<int> userVillages = GetUserVillageIds();
            Dictionary<int, string> villageNames = GetVillageNames(userVillages);

            foreach (int id in userVillages)
            {
                string display = "[" + id + "] " + villageNames[id];
                _fromList.Items.Add(new VillageItem(id, display));
                _toList.Items.Add(new VillageItem(id, display));
            }

            // Select current from/to villages
            for (int i = 0; i < _fromList.Items.Count; i++)
            {
                VillageItem item = (VillageItem)_fromList.Items[i];
                if (_route.FromVillages.Contains(item.VillageId))
                    _fromList.SetSelected(i, true);
            }
            for (int i = 0; i < _toList.Items.Count; i++)
            {
                VillageItem item = (VillageItem)_toList.Items[i];
                if (_route.ToVillages.Contains(item.VillageId))
                    _toList.SetSelected(i, true);
            }

            // Populate resource list
            byte[] resIds = TradeModuleConstants.TradeTypeIds;
            for (int i = 0; i < resIds.Length; i++)
            {
                string name = TradeModuleConstants.GetResourceName((int)resIds[i]);
                _resourceList.Items.Add(new ResourceItem(resIds[i], name));
                if (_route.Resources.Contains((int)resIds[i]))
                    _resourceList.SetItemChecked(i, true);
            }
        }

        private void SaveAndClose()
        {
            _route.Name = _nameInput.Text;
            _route.Enabled = _enabledCheck.Checked;
            _route.KeepMinimum = (int)_keepMinInput.Value;
            _route.MaxMerchantsPerTransaction = (int)_maxMerchantsInput.Value;
            _route.SendMaximum = (int)_sendMaxInput.Value;
            _route.IsDistanceLimited = _distLimitCheck.Checked;
            _route.DistanceLimit = (int)_distLimitInput.Value;

            _route.FromVillages.Clear();
            foreach (object sel in _fromList.SelectedItems)
            {
                VillageItem item = sel as VillageItem;
                if (item != null)
                    _route.FromVillages.Add(item.VillageId);
            }

            _route.ToVillages.Clear();
            foreach (object sel in _toList.SelectedItems)
            {
                VillageItem item = sel as VillageItem;
                if (item != null)
                    _route.ToVillages.Add(item.VillageId);
            }

            _route.Resources.Clear();
            for (int i = 0; i < _resourceList.Items.Count; i++)
            {
                if (_resourceList.GetItemChecked(i))
                {
                    ResourceItem item = (ResourceItem)_resourceList.Items[i];
                    _route.Resources.Add((int)item.ResourceId);
                }
            }

            _saved = true;
            this.Close();
        }

        private static List<int> GetUserVillageIds()
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
            {
                List<int> ids = GameEngine.Instance.World.getUserVillageIDList();
                if (ids != null) return ids;
            }
            return new List<int>();
        }

        private static Dictionary<int, string> GetVillageNames(List<int> ids)
        {
            Dictionary<int, string> names = new Dictionary<int, string>();
            foreach (int id in ids)
            {
                string name = "Village " + id;
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                    name = GameEngine.Instance.World.getVillageName(id);
                names[id] = name;
            }
            return names;
        }

        private static decimal Clamp(NumericUpDown nud, int val)
        {
            if (val < (int)nud.Minimum) return nud.Minimum;
            if (val > (int)nud.Maximum) return nud.Maximum;
            return val;
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

        private static NumericUpDown MakeNumeric(int x, int y, int min, int max, int val)
        {
            NumericUpDown nud = new NumericUpDown();
            nud.BackColor = Color.FromArgb(50, 52, 64);
            nud.ForeColor = Color.FromArgb(230, 230, 240);
            nud.BorderStyle = BorderStyle.FixedSingle;
            nud.Font = new Font("Segoe UI", 8.5f);
            nud.Location = new Point(x, y);
            nud.Size = new Size(70, 22);
            nud.Maximum = max;
            nud.Minimum = min;
            nud.Value = Math.Max(min, Math.Min(max, val));
            return nud;
        }

        private static Button MakeButton(string text, Color bg, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.Size = new Size(100, 30);
            btn.Location = new Point(x, y);
            btn.Cursor = Cursors.Hand;
            return btn;
        }
    }

    internal class VillageItem
    {
        public int VillageId;
        public string Display;
        public VillageItem(int id, string display) { VillageId = id; Display = display; }
        public override string ToString() { return Display; }
    }

    internal class ResourceItem
    {
        public byte ResourceId;
        public string Display;
        public ResourceItem(byte id, string display) { ResourceId = id; Display = display; }
        public override string ToString() { return Display; }
    }

    // =========================================================================
    // Kept for back-compat: TradeMarketVillageRow is no longer used but 
    // referenced from BotControlForm - we alias the type.
    // =========================================================================
    internal class TradeMarketVillageRow : Panel
    {
        public int VillageId;
        public TradeMarketVillageRow() { }
        public void WriteToSettings(TradeSettings s) { }
    }

    // =========================================================================
    // Copy Market Settings popup
    // =========================================================================

    internal class CopyMarketSettingsForm : Form
    {
        private static readonly Color FormBg = Color.FromArgb(28, 30, 38);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color AccentCol = Color.FromArgb(80, 160, 255);

        private ComboBox _sourceCombo;
        private CheckedListBox _targetList;
        private CheckBox _copyMarketsCheck;
        private Button _copyBtn;
        private Button _cancelBtn;
        private Button _selectAllBtn;
        private Button _selectNoneBtn;
        private Label _statusLabel;

        private bool _copied;
        public bool Copied { get { return _copied; } }

        public CopyMarketSettingsForm()
        {
            _copied = false;
            this.Text = "Copy Market Settings";
            this.BackColor = FormBg;
            this.ForeColor = TextPri;
            this.Font = new Font("Segoe UI", 9f);
            this.ClientSize = new Size(440, 420);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;

            BuildUI();
            PopulateVillages();
        }

        private void BuildUI()
        {
            Label srcLbl = MakeLabel("Copy from:", 14, 14);
            this.Controls.Add(srcLbl);

            _sourceCombo = new ComboBox();
            _sourceCombo.BackColor = InputBg;
            _sourceCombo.ForeColor = TextPri;
            _sourceCombo.FlatStyle = FlatStyle.Flat;
            _sourceCombo.Font = new Font("Segoe UI", 9f);
            _sourceCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _sourceCombo.Location = new Point(14, 34);
            _sourceCombo.Size = new Size(410, 24);
            this.Controls.Add(_sourceCombo);

            Label tgtLbl = MakeLabel("Copy to (select one or more):", 14, 66);
            this.Controls.Add(tgtLbl);

            _targetList = new CheckedListBox();
            _targetList.BackColor = InputBg;
            _targetList.ForeColor = TextPri;
            _targetList.Font = new Font("Segoe UI", 8.5f);
            _targetList.BorderStyle = BorderStyle.FixedSingle;
            _targetList.CheckOnClick = true;
            _targetList.Location = new Point(14, 86);
            _targetList.Size = new Size(410, 240);
            this.Controls.Add(_targetList);

            _selectAllBtn = MakeSmallButton("Select All", 14, 332);
            _selectAllBtn.Click += delegate
            {
                for (int i = 0; i < _targetList.Items.Count; i++)
                    _targetList.SetItemChecked(i, true);
            };
            this.Controls.Add(_selectAllBtn);

            _selectNoneBtn = MakeSmallButton("Select None", 100, 332);
            _selectNoneBtn.Click += delegate
            {
                for (int i = 0; i < _targetList.Items.Count; i++)
                    _targetList.SetItemChecked(i, false);
            };
            this.Controls.Add(_selectNoneBtn);

            _copyMarketsCheck = new CheckBox();
            _copyMarketsCheck.Text = "Also copy market targets list";
            _copyMarketsCheck.FlatStyle = FlatStyle.Flat;
            _copyMarketsCheck.ForeColor = TextPri;
            _copyMarketsCheck.Font = new Font("Segoe UI", 8.5f);
            _copyMarketsCheck.Location = new Point(210, 334);
            _copyMarketsCheck.AutoSize = true;
            _copyMarketsCheck.Checked = true;
            this.Controls.Add(_copyMarketsCheck);

            _statusLabel = new Label();
            _statusLabel.Text = "";
            _statusLabel.Font = new Font("Segoe UI", 8.5f);
            _statusLabel.ForeColor = Color.FromArgb(100, 220, 100);
            _statusLabel.AutoSize = true;
            _statusLabel.Location = new Point(14, 366);
            this.Controls.Add(_statusLabel);

            _copyBtn = MakeButton("Copy", AccentCol, 230, 380);
            _copyBtn.Click += delegate { DoCopy(); };
            this.Controls.Add(_copyBtn);

            _cancelBtn = MakeButton("Close", Color.FromArgb(70, 72, 84), 340, 380);
            _cancelBtn.Click += delegate { this.Close(); };
            this.Controls.Add(_cancelBtn);
        }

        private void PopulateVillages()
        {
            List<int> ids = GetUserVillageIds();
            Dictionary<int, string> names = GetVillageNames(ids);

            foreach (int id in ids)
            {
                string display = "[" + id + "] " + names[id];
                _sourceCombo.Items.Add(new VillageItem(id, display));
                _targetList.Items.Add(new VillageItem(id, display));
            }

            if (_sourceCombo.Items.Count > 0)
                _sourceCombo.SelectedIndex = 0;
        }

        private void DoCopy()
        {
            VillageItem sourceItem = _sourceCombo.SelectedItem as VillageItem;
            if (sourceItem == null)
            {
                _statusLabel.ForeColor = Color.FromArgb(255, 100, 100);
                _statusLabel.Text = "Select a source village.";
                return;
            }

            List<int> targetIds = new List<int>();
            for (int i = 0; i < _targetList.Items.Count; i++)
            {
                if (_targetList.GetItemChecked(i))
                {
                    VillageItem item = (VillageItem)_targetList.Items[i];
                    if (item.VillageId != sourceItem.VillageId)
                        targetIds.Add(item.VillageId);
                }
            }

            if (targetIds.Count == 0)
            {
                _statusLabel.ForeColor = Color.FromArgb(255, 100, 100);
                _statusLabel.Text = "Select at least one target village.";
                return;
            }

            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            TradeSettings s = BotEngine.Instance.Settings.Trade;
            VillageMarketTradeInfo source = s.GetVillageMarketInfo(sourceItem.VillageId);

            int count = 0;
            foreach (int targetId in targetIds)
            {
                VillageMarketTradeInfo target = s.GetVillageMarketInfo(targetId);
                // Copy trade type settings
                target.IsTrading = source.IsTrading;
                target.TradeTypes.Clear();
                foreach (TradeTypeEntry e in source.TradeTypes)
                    target.TradeTypes.Add(e.Clone());

                // Optionally copy market targets
                if (_copyMarketsCheck.Checked)
                {
                    target.MarketTargets.Clear();
                    target.MarketTargets.AddRange(source.MarketTargets);
                }
                count++;
            }

            _copied = true;
            _statusLabel.ForeColor = Color.FromArgb(100, 220, 100);
            _statusLabel.Text = "Copied settings to " + count + " village(s).";
            BotLogger.Log("Trade", BotLogLevel.Info,
                "Copied market settings from " + sourceItem.Display + " to " + count + " village(s).");
        }

        private static List<int> GetUserVillageIds()
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
            {
                List<int> ids = GameEngine.Instance.World.getUserVillageIDList();
                if (ids != null) return ids;
            }
            return new List<int>();
        }

        private static Dictionary<int, string> GetVillageNames(List<int> ids)
        {
            Dictionary<int, string> names = new Dictionary<int, string>();
            foreach (int id in ids)
            {
                string name = "Village " + id;
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                    name = GameEngine.Instance.World.getVillageName(id);
                names[id] = name;
            }
            return names;
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

        private static Button MakeButton(string text, Color bg, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.Size = new Size(100, 30);
            btn.Location = new Point(x, y);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private static Button MakeSmallButton(string text, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.BackColor = Color.FromArgb(50, 52, 64);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 8f);
            btn.Size = new Size(80, 24);
            btn.Location = new Point(x, y);
            btn.Cursor = Cursors.Hand;
            return btn;
        }
    }

    // =========================================================================
    // Player Trade Route Row (summary display in the list)
    // =========================================================================

    internal class PlayerTradeRouteRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color BgSelected = Color.FromArgb(50, 55, 75);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color ProgressGreen = Color.FromArgb(80, 200, 120);
        private static readonly Color ProgressYellow = Color.FromArgb(220, 180, 60);

        private PlayerTradeRouteSettings _route;
        private CheckBox _enabledCheck;
        private bool _isSelected;
        private Color _normalBg;

        public PlayerTradeRouteSettings Route { get { return _route; } }
        public bool IsSelected { get { return _isSelected; } }

        public PlayerTradeRouteRow(PlayerTradeRouteSettings route, bool alternate)
        {
            _route = route;
            _normalBg = alternate ? BgOdd : BgEven;
            this.Height = 24;
            this.BackColor = _normalBg;
            this.Cursor = Cursors.Hand;
            this.Click += delegate { Select(); };

            _enabledCheck = new CheckBox();
            _enabledCheck.Checked = route.Enabled;
            _enabledCheck.AutoSize = true;
            _enabledCheck.FlatStyle = FlatStyle.Flat;
            _enabledCheck.Location = new Point(6, 3);
            _enabledCheck.CheckedChanged += delegate { _route.Enabled = _enabledCheck.Checked; };
            this.Controls.Add(_enabledCheck);

            AddLabel(route.Name, 28, 130, TextPri);

            string fromText = route.FromVillages.Count + " village(s)";
            AddLabel(fromText, 164, 96, TextSec);

            string targetText = route.TargetVillageId > 0 ? route.TargetVillageId.ToString() : "(none)";
            AddLabel(targetText, 264, 76, TextSec);

            string resText = route.Resources.Count + " type(s)";
            AddLabel(resText, 344, 170, TextSec);

            // Progress
            int totalAmount = 0;
            int totalSent = 0;
            foreach (PlayerTradeResourceEntry e in route.Resources)
            {
                totalAmount += e.TotalAmount;
                totalSent += e.AmountSent;
            }
            string progressText = totalSent + " / " + totalAmount;
            Color progressColor = (totalAmount > 0 && totalSent >= totalAmount) ? ProgressGreen : ProgressYellow;
            Label progressLabel = AddLabel(progressText, 520, 130, progressColor);
            if (route.IsComplete())
                progressLabel.Text += " \u2713";

            AddLabel(route.KeepMinimum.ToString(), 660, 60, TextSec);
            AddLabel(route.MaxMerchantsPerTransaction.ToString(), 730, 50, TextSec);
        }

        private Label AddLabel(string text, int x, int width, Color color)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 7.5f);
            lbl.ForeColor = color;
            lbl.Location = new Point(x, 4);
            lbl.Size = new Size(width, 16);
            lbl.Click += delegate { Select(); };
            this.Controls.Add(lbl);
            return lbl;
        }

        private void Select()
        {
            _isSelected = !_isSelected;
            this.BackColor = _isSelected ? BgSelected : _normalBg;
        }

        public void WriteToSettings(TradeSettings s)
        {
            _route.Enabled = _enabledCheck.Checked;
        }
    }

    // =========================================================================
    // Player Trade Route Editor Form
    // =========================================================================

    internal class PlayerTradeRouteEditorForm : Form
    {
        private static readonly Color FormBg = Color.FromArgb(28, 30, 38);
        private static readonly Color PanelBg = Color.FromArgb(36, 38, 48);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color AccentCol = Color.FromArgb(80, 160, 255);

        private TextBox _nameInput;
        private CheckBox _enabledCheck;
        private ListBox _fromList;
        private NumericUpDown _targetIdInput;
        private NumericUpDown _keepMinInput;
        private NumericUpDown _maxMerchantsInput;
        private Panel _resourcePanel;
        private List<PlayerResourceRow> _resourceRows = new List<PlayerResourceRow>();
        private Button _saveBtn;
        private Button _cancelBtn;

        private PlayerTradeRouteSettings _route;
        private bool _saved;
        public bool Saved { get { return _saved; } }

        public PlayerTradeRouteEditorForm(PlayerTradeRouteSettings route, string title)
        {
            _route = route;
            _saved = false;

            this.Text = title;
            this.BackColor = FormBg;
            this.ForeColor = TextPri;
            this.Font = new Font("Segoe UI", 9f);
            this.ClientSize = new Size(750, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;

            BuildUI();
            PopulateFromRoute();
        }

        private void BuildUI()
        {
            // Route name + enabled
            Label nameLbl = MakeLabel("Route Name:", 14, 14);
            this.Controls.Add(nameLbl);
            _nameInput = new TextBox();
            _nameInput.BackColor = InputBg;
            _nameInput.ForeColor = TextPri;
            _nameInput.BorderStyle = BorderStyle.FixedSingle;
            _nameInput.Font = new Font("Segoe UI", 9f);
            _nameInput.Location = new Point(110, 12);
            _nameInput.Size = new Size(180, 22);
            this.Controls.Add(_nameInput);

            _enabledCheck = new CheckBox();
            _enabledCheck.Text = "Enabled";
            _enabledCheck.FlatStyle = FlatStyle.Flat;
            _enabledCheck.ForeColor = TextPri;
            _enabledCheck.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            _enabledCheck.Location = new Point(310, 12);
            _enabledCheck.AutoSize = true;
            this.Controls.Add(_enabledCheck);

            int y = 44;

            // From villages
            Label fromLbl = MakeLabel("From Villages:", 14, y);
            this.Controls.Add(fromLbl);
            _fromList = new ListBox();
            _fromList.BackColor = InputBg;
            _fromList.ForeColor = TextPri;
            _fromList.Font = new Font("Segoe UI", 8f);
            _fromList.BorderStyle = BorderStyle.FixedSingle;
            _fromList.SelectionMode = SelectionMode.MultiExtended;
            _fromList.Location = new Point(14, y + 18);
            _fromList.Size = new Size(220, 120);
            this.Controls.Add(_fromList);

            // Target village ID
            Label targetLbl = MakeLabel("Target Village ID:", 250, y);
            this.Controls.Add(targetLbl);
            _targetIdInput = new NumericUpDown();
            _targetIdInput.BackColor = InputBg;
            _targetIdInput.ForeColor = TextPri;
            _targetIdInput.BorderStyle = BorderStyle.FixedSingle;
            _targetIdInput.Font = new Font("Segoe UI", 9f);
            _targetIdInput.Location = new Point(250, y + 18);
            _targetIdInput.Size = new Size(120, 22);
            _targetIdInput.Maximum = 999999;
            _targetIdInput.Minimum = 0;
            this.Controls.Add(_targetIdInput);

            // Keep minimum
            Label keepLbl = MakeLabel("Keep Min:", 250, y + 50);
            this.Controls.Add(keepLbl);
            _keepMinInput = new NumericUpDown();
            _keepMinInput.BackColor = InputBg;
            _keepMinInput.ForeColor = TextPri;
            _keepMinInput.BorderStyle = BorderStyle.FixedSingle;
            _keepMinInput.Font = new Font("Segoe UI", 9f);
            _keepMinInput.Location = new Point(250, y + 68);
            _keepMinInput.Size = new Size(120, 22);
            _keepMinInput.Maximum = 999999;
            _keepMinInput.Minimum = 0;
            this.Controls.Add(_keepMinInput);

            // Max merchants per transaction
            Label maxMerchLbl = MakeLabel("Max Merchants:", 390, y);
            this.Controls.Add(maxMerchLbl);
            _maxMerchantsInput = new NumericUpDown();
            _maxMerchantsInput.BackColor = InputBg;
            _maxMerchantsInput.ForeColor = TextPri;
            _maxMerchantsInput.BorderStyle = BorderStyle.FixedSingle;
            _maxMerchantsInput.Font = new Font("Segoe UI", 9f);
            _maxMerchantsInput.Location = new Point(390, y + 18);
            _maxMerchantsInput.Size = new Size(80, 22);
            _maxMerchantsInput.Maximum = 500;
            _maxMerchantsInput.Minimum = 1;
            _maxMerchantsInput.Value = 50;
            this.Controls.Add(_maxMerchantsInput);

            // Resources section
            int resY = 190;
            Label resLbl = MakeLabel("Resources (select amount to send for each — 0 = skip):", 14, resY);
            this.Controls.Add(resLbl);

            // Resource header
            Panel resHeader = new Panel();
            resHeader.Location = new Point(14, resY + 18);
            resHeader.Size = new Size(720, 18);
            resHeader.BackColor = Color.FromArgb(36, 38, 50);
            Label hdrName = new Label();
            hdrName.Text = "Resource";
            hdrName.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            hdrName.ForeColor = TextSec;
            hdrName.AutoSize = true;
            hdrName.Location = new Point(4, 2);
            resHeader.Controls.Add(hdrName);
            Label hdrTotal = new Label();
            hdrTotal.Text = "Total Amount";
            hdrTotal.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            hdrTotal.ForeColor = TextSec;
            hdrTotal.AutoSize = true;
            hdrTotal.Location = new Point(140, 2);
            resHeader.Controls.Add(hdrTotal);
            Label hdrSent = new Label();
            hdrSent.Text = "Sent So Far";
            hdrSent.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            hdrSent.ForeColor = TextSec;
            hdrSent.AutoSize = true;
            hdrSent.Location = new Point(260, 2);
            resHeader.Controls.Add(hdrSent);
            this.Controls.Add(resHeader);

            _resourcePanel = new Panel();
            _resourcePanel.Location = new Point(14, resY + 36);
            _resourcePanel.Size = new Size(720, 240);
            _resourcePanel.AutoScroll = true;
            _resourcePanel.BackColor = Color.FromArgb(28, 30, 38);
            this.Controls.Add(_resourcePanel);

            int by = 480;
            _saveBtn = MakeButton("Save", AccentCol, 540, by);
            _saveBtn.Click += delegate { SaveAndClose(); };
            this.Controls.Add(_saveBtn);

            _cancelBtn = MakeButton("Cancel", Color.FromArgb(70, 72, 84), 650, by);
            _cancelBtn.Click += delegate { this.Close(); };
            this.Controls.Add(_cancelBtn);
        }

        private void PopulateFromRoute()
        {
            _nameInput.Text = _route.Name;
            _enabledCheck.Checked = _route.Enabled;
            _targetIdInput.Value = Math.Max(0, Math.Min(999999, _route.TargetVillageId));
            _keepMinInput.Value = Math.Max(0, Math.Min(999999, _route.KeepMinimum));
            _maxMerchantsInput.Value = Math.Max(1, Math.Min(500, _route.MaxMerchantsPerTransaction));

            // Populate village list
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
            {
                List<int> ids = GameEngine.Instance.World.getUserVillageIDList();
                if (ids != null)
                {
                    foreach (int id in ids)
                    {
                        string name = GameEngine.Instance.World.getVillageName(id);
                        _fromList.Items.Add(new VillageItem(id, "[" + id + "] " + name));
                    }
                }
            }

            for (int i = 0; i < _fromList.Items.Count; i++)
            {
                VillageItem item = (VillageItem)_fromList.Items[i];
                if (_route.FromVillages.Contains(item.VillageId))
                    _fromList.SetSelected(i, true);
            }

            // Populate resources
            byte[] resIds = TradeModuleConstants.TradeTypeIds;
            int rowY = 0;
            for (int idx = 0; idx < resIds.Length; idx++)
            {
                int resId = (int)resIds[idx];
                string resName = TradeModuleConstants.GetResourceName(resId);
                PlayerTradeResourceEntry existing = _route.GetResourceEntry(resId);
                int total = existing != null ? existing.TotalAmount : 0;
                int sent = existing != null ? existing.AmountSent : 0;

                PlayerResourceRow row = new PlayerResourceRow(resId, resName, total, sent, idx);
                row.Location = new Point(0, rowY);
                row.Size = new Size(700, 22);
                _resourcePanel.Controls.Add(row);
                _resourceRows.Add(row);
                rowY += 22;
            }
        }

        private void SaveAndClose()
        {
            _route.Name = _nameInput.Text;
            _route.Enabled = _enabledCheck.Checked;
            _route.TargetVillageId = (int)_targetIdInput.Value;
            _route.KeepMinimum = (int)_keepMinInput.Value;
            _route.MaxMerchantsPerTransaction = (int)_maxMerchantsInput.Value;

            _route.FromVillages.Clear();
            foreach (object sel in _fromList.SelectedItems)
            {
                VillageItem item = sel as VillageItem;
                if (item != null)
                    _route.FromVillages.Add(item.VillageId);
            }

            _route.Resources.Clear();
            foreach (PlayerResourceRow row in _resourceRows)
            {
                int total = row.TotalAmount;
                if (total > 0)
                {
                    PlayerTradeResourceEntry entry = new PlayerTradeResourceEntry();
                    entry.ResourceId = row.ResourceId;
                    entry.TotalAmount = total;
                    entry.AmountSent = row.AmountSent;
                    _route.Resources.Add(entry);
                }
            }

            _saved = true;
            this.Close();
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

        private static Button MakeButton(string text, Color bg, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.Size = new Size(100, 30);
            btn.Location = new Point(x, y);
            btn.Cursor = Cursors.Hand;
            return btn;
        }
    }

    // =========================================================================
    // Player Resource Row (inside the editor form)
    // =========================================================================

    internal class PlayerResourceRow : Panel
    {
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color RowEven = Color.FromArgb(30, 32, 40);
        private static readonly Color RowOdd = Color.FromArgb(36, 38, 48);

        private int _resourceId;
        private NumericUpDown _totalInput;
        private int _amountSent;

        public int ResourceId { get { return _resourceId; } }
        public int TotalAmount { get { return (int)_totalInput.Value; } }
        public int AmountSent { get { return _amountSent; } }

        public PlayerResourceRow(int resourceId, string resourceName, int total, int sent, int index)
        {
            _resourceId = resourceId;
            _amountSent = sent;
            this.BackColor = index % 2 == 0 ? RowEven : RowOdd;

            Label nameLabel = new Label();
            nameLabel.Text = resourceName;
            nameLabel.Font = new Font("Segoe UI", 8f);
            nameLabel.ForeColor = TextPri;
            nameLabel.Location = new Point(4, 2);
            nameLabel.Size = new Size(130, 18);
            this.Controls.Add(nameLabel);

            _totalInput = new NumericUpDown();
            _totalInput.BackColor = InputBg;
            _totalInput.ForeColor = TextPri;
            _totalInput.BorderStyle = BorderStyle.FixedSingle;
            _totalInput.Font = new Font("Segoe UI", 8f);
            _totalInput.Location = new Point(140, 1);
            _totalInput.Size = new Size(100, 20);
            _totalInput.Maximum = 999999;
            _totalInput.Minimum = 0;
            _totalInput.Value = Math.Max(0, Math.Min(999999, total));
            this.Controls.Add(_totalInput);

            Label sentLabel = new Label();
            sentLabel.Text = sent.ToString();
            sentLabel.Font = new Font("Segoe UI", 8f);
            sentLabel.ForeColor = sent >= total && total > 0
                ? Color.FromArgb(80, 200, 120)
                : TextSec;
            sentLabel.Location = new Point(260, 2);
            sentLabel.Size = new Size(80, 18);
            this.Controls.Add(sentLabel);

            if (total > 0)
            {
                int remaining = Math.Max(0, total - sent);
                Label remLabel = new Label();
                remLabel.Text = "(" + remaining + " remaining)";
                remLabel.Font = new Font("Segoe UI", 7f);
                remLabel.ForeColor = TextSec;
                remLabel.Location = new Point(340, 3);
                remLabel.AutoSize = true;
                this.Controls.Add(remLabel);
            }
        }
    }
}
