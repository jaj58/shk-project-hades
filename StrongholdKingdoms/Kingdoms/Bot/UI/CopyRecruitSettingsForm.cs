using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Generic form for copying recruit settings from one village/vassal to others.
    /// Used for Villages tab, Capitals tab, and Vassals tab.
    /// </summary>
    internal class CopyRecruitSettingsForm : Form
    {
        public enum CopyMode { Villages, Capitals, Vassals }

        private static readonly Color FormBg = Color.FromArgb(28, 30, 38);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color AccentCol = Color.FromArgb(80, 160, 255);

        private ComboBox _sourceCombo;
        private CheckedListBox _targetList;
        private Button _copyBtn;
        private Button _cancelBtn;
        private Button _selectAllBtn;
        private Button _selectNoneBtn;
        private Label _statusLabel;

        private CopyMode _mode;
        private bool _copied;
        public bool Copied { get { return _copied; } }

        public CopyRecruitSettingsForm(CopyMode mode)
        {
            _mode = mode;
            _copied = false;

            string title;
            switch (mode)
            {
                case CopyMode.Capitals: title = "Copy Capital Recruit Settings"; break;
                case CopyMode.Vassals: title = "Copy Vassal Recruit Settings"; break;
                default: title = "Copy Village Recruit Settings"; break;
            }

            this.Text = title;
            this.BackColor = FormBg;
            this.ForeColor = TextPri;
            this.Font = new Font("Segoe UI", 9f);
            this.ClientSize = new Size(440, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;

            BuildUI();
            PopulateItems();
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
            _targetList.Size = new Size(410, 224);
            this.Controls.Add(_targetList);

            _selectAllBtn = MakeSmallButton("Select All", 14, 316);
            _selectAllBtn.Click += delegate
            {
                for (int i = 0; i < _targetList.Items.Count; i++)
                    _targetList.SetItemChecked(i, true);
            };
            this.Controls.Add(_selectAllBtn);

            _selectNoneBtn = MakeSmallButton("Select None", 100, 316);
            _selectNoneBtn.Click += delegate
            {
                for (int i = 0; i < _targetList.Items.Count; i++)
                    _targetList.SetItemChecked(i, false);
            };
            this.Controls.Add(_selectNoneBtn);

            _statusLabel = new Label();
            _statusLabel.Text = "";
            _statusLabel.Font = new Font("Segoe UI", 8.5f);
            _statusLabel.ForeColor = Color.FromArgb(100, 220, 100);
            _statusLabel.AutoSize = true;
            _statusLabel.Location = new Point(14, 348);
            this.Controls.Add(_statusLabel);

            _copyBtn = MakeButton("Copy", AccentCol, 230, 362);
            _copyBtn.Click += delegate { DoCopy(); };
            this.Controls.Add(_copyBtn);

            _cancelBtn = MakeButton("Close", Color.FromArgb(70, 72, 84), 340, 362);
            _cancelBtn.Click += delegate { this.Close(); };
            this.Controls.Add(_cancelBtn);
        }

        private void PopulateItems()
        {
            List<ItemEntry> items = GetItemList();
            foreach (ItemEntry item in items)
            {
                _sourceCombo.Items.Add(item);
                _targetList.Items.Add(item);
            }
            if (_sourceCombo.Items.Count > 0)
                _sourceCombo.SelectedIndex = 0;
        }

        private List<ItemEntry> GetItemList()
        {
            List<ItemEntry> items = new List<ItemEntry>();

            if (_mode == CopyMode.Vassals)
            {
                if (GameEngine.Instance == null || GameEngine.Instance.vassalsManager == null)
                    return items;

                CommonTypes.VassalInfo[] vassals = GameEngine.Instance.vassalsManager.GetVassals();
                if (vassals == null) return items;

                foreach (CommonTypes.VassalInfo vi in vassals)
                {
                    string name = GetVillageName(vi.villageID);
                    if (!string.IsNullOrEmpty(vi.vassalPlayerName))
                        name += " (" + vi.vassalPlayerName + ")";
                    items.Add(new ItemEntry(vi.villageID, name));
                }
            }
            else
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                    return items;

                List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
                if (villages == null) return items;

                foreach (WorldMap.UserVillageData v in villages)
                {
                    int id = v.villageID;
                    string typeLabel = Modules.VillageSyncModule.GetVillageTypeLabel(id);
                    bool isCapital = typeLabel == "Parish"
                        || typeLabel == "County"
                        || typeLabel == "Province"
                        || typeLabel == "Country";

                    if (_mode == CopyMode.Capitals && !isCapital) continue;
                    if (_mode == CopyMode.Villages && isCapital) continue;

                    string name = GetVillageName(id);
                    items.Add(new ItemEntry(id, "[" + id + "] " + name));
                }
            }

            return items;
        }

        private void DoCopy()
        {
            ItemEntry sourceItem = _sourceCombo.SelectedItem as ItemEntry;
            if (sourceItem == null)
            {
                ShowStatus("Select a source.", true);
                return;
            }

            List<int> targetIds = new List<int>();
            for (int i = 0; i < _targetList.Items.Count; i++)
            {
                if (_targetList.GetItemChecked(i))
                {
                    ItemEntry item = (ItemEntry)_targetList.Items[i];
                    if (item.Id != sourceItem.Id)
                        targetIds.Add(item.Id);
                }
            }

            if (targetIds.Count == 0)
            {
                ShowStatus("Select at least one target.", true);
                return;
            }

            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            int count;
            if (_mode == CopyMode.Vassals)
                count = CopyVassalSettings(sourceItem.Id, targetIds);
            else
                count = CopyVillageSettings(sourceItem.Id, targetIds);

            _copied = true;
            string label;
            switch (_mode)
            {
                case CopyMode.Capitals: label = "capital(s)"; break;
                case CopyMode.Vassals: label = "vassal(s)"; break;
                default: label = "village(s)"; break;
            }
            ShowStatus("Copied settings to " + count + " " + label + ".", false);
            BotLogger.Log("Recruiting", BotLogLevel.Info,
                "Copied recruit settings from " + sourceItem.Display + " to " + count + " " + label + ".");
        }

        private int CopyVillageSettings(int sourceId, List<int> targetIds)
        {
            RecruitingSettings s = BotEngine.Instance.Settings.Recruiting;
            VillageRecruitSettings source = s.GetVillageSettings(sourceId);

            // For capitals, only copy combat unit entries
            string[] keysToUse = _mode == CopyMode.Capitals
                ? Modules.RecruitingModule.CapitalUnitKeys
                : Modules.RecruitingModule.AllUnitKeys;

            int count = 0;
            foreach (int targetId in targetIds)
            {
                VillageRecruitSettings target = s.GetVillageSettings(targetId);
                foreach (string key in keysToUse)
                {
                    UnitRecruitEntry srcEntry = source.GetEntry(key);
                    UnitRecruitEntry tgtEntry = target.GetEntry(key);
                    tgtEntry.TargetCount = srcEntry.TargetCount;
                    tgtEntry.Priority = srcEntry.Priority;
                }
                count++;
            }
            return count;
        }

        private int CopyVassalSettings(int sourceId, List<int> targetIds)
        {
            VassalRecruitingSettings s = BotEngine.Instance.Settings.Recruiting.VassalRecruiting;
            VassalVillageRecruitSettings source = s.GetVassalSettings(sourceId);

            int count = 0;
            foreach (int targetId in targetIds)
            {
                VassalVillageRecruitSettings target = s.GetVassalSettings(targetId);
                target.Enabled = source.Enabled;
                target.Units.Clear();
                foreach (VassalUnitRecruitEntry e in source.Units)
                {
                    VassalUnitRecruitEntry copy = new VassalUnitRecruitEntry();
                    copy.UnitKey = e.UnitKey;
                    copy.TargetCount = e.TargetCount;
                    copy.Priority = e.Priority;
                    target.Units.Add(copy);
                }
                count++;
            }
            return count;
        }

        private void ShowStatus(string text, bool isError)
        {
            _statusLabel.ForeColor = isError
                ? Color.FromArgb(255, 100, 100)
                : Color.FromArgb(100, 220, 100);
            _statusLabel.Text = text;
        }

        private static string GetVillageName(int id)
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                return GameEngine.Instance.World.getVillageName(id);
            return "Village " + id;
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

        private class ItemEntry
        {
            public int Id;
            public string Display;
            public ItemEntry(int id, string display) { Id = id; Display = display; }
            public override string ToString() { return Display; }
        }
    }

    internal class CopyCastleRepairSettingsForm : Form
    {
        private static readonly Color FormBg = Color.FromArgb(28, 30, 38);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color AccentCol = Color.FromArgb(80, 160, 255);

        private ComboBox _sourceCombo;
        private CheckedListBox _targetList;
        private Button _copyBtn;
        private Button _cancelBtn;
        private Button _selectAllBtn;
        private Button _selectNoneBtn;
        private Label _statusLabel;

        private bool _copied;
        public bool Copied { get { return _copied; } }

        public CopyCastleRepairSettingsForm()
        {
            _copied = false;
            this.Text = "Copy Castle Repair Settings";
            this.BackColor = FormBg;
            this.ForeColor = TextPri;
            this.Font = new Font("Segoe UI", 9f);
            this.ClientSize = new Size(440, 400);
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
            _targetList.Size = new Size(410, 224);
            this.Controls.Add(_targetList);

            _selectAllBtn = MakeSmallButton("Select All", 14, 316);
            _selectAllBtn.Click += delegate
            {
                for (int i = 0; i < _targetList.Items.Count; i++)
                    _targetList.SetItemChecked(i, true);
            };
            this.Controls.Add(_selectAllBtn);

            _selectNoneBtn = MakeSmallButton("Select None", 100, 316);
            _selectNoneBtn.Click += delegate
            {
                for (int i = 0; i < _targetList.Items.Count; i++)
                    _targetList.SetItemChecked(i, false);
            };
            this.Controls.Add(_selectNoneBtn);

            _statusLabel = new Label();
            _statusLabel.Text = "";
            _statusLabel.Font = new Font("Segoe UI", 8.5f);
            _statusLabel.ForeColor = Color.FromArgb(100, 220, 100);
            _statusLabel.AutoSize = true;
            _statusLabel.Location = new Point(14, 348);
            this.Controls.Add(_statusLabel);

            _copyBtn = MakeBtn("Copy", AccentCol, 230, 362);
            _copyBtn.Click += delegate { DoCopy(); };
            this.Controls.Add(_copyBtn);

            _cancelBtn = MakeBtn("Close", Color.FromArgb(70, 72, 84), 340, 362);
            _cancelBtn.Click += delegate { this.Close(); };
            this.Controls.Add(_cancelBtn);
        }

        private void PopulateVillages()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            List<CrVillageItem> items = new List<CrVillageItem>();
            foreach (WorldMap.UserVillageData v in villages)
            {
                int id = v.villageID;
                string name = GameEngine.Instance.World.getVillageName(id);
                string typeLabel = Modules.VillageSyncModule.GetVillageTypeLabel(id);
                string display = "[" + id + "] " + name;
                if (typeLabel != "Village")
                    display += " (" + typeLabel + ")";
                items.Add(new CrVillageItem(id, display));
            }

            foreach (CrVillageItem item in items)
            {
                _sourceCombo.Items.Add(item);
                _targetList.Items.Add(item);
            }

            if (_sourceCombo.Items.Count > 0)
                _sourceCombo.SelectedIndex = 0;
        }

        private void DoCopy()
        {
            CrVillageItem sourceItem = _sourceCombo.SelectedItem as CrVillageItem;
            if (sourceItem == null)
            {
                ShowStatus("Select a source village.", true);
                return;
            }

            List<int> targetIds = new List<int>();
            for (int i = 0; i < _targetList.Items.Count; i++)
            {
                if (_targetList.GetItemChecked(i))
                {
                    CrVillageItem item = (CrVillageItem)_targetList.Items[i];
                    if (item.VillageId != sourceItem.VillageId)
                        targetIds.Add(item.VillageId);
                }
            }

            if (targetIds.Count == 0)
            {
                ShowStatus("Select at least one target village.", true);
                return;
            }

            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            CastleRepairSettings s = BotEngine.Instance.Settings.CastleRepair;
            VillageCastleRepairSettings source = s.GetVillageSettings(sourceItem.VillageId);

            int count = 0;
            foreach (int targetId in targetIds)
            {
                VillageCastleRepairSettings target = s.GetVillageSettings(targetId);
                target.RepairInfrastructure = source.RepairInfrastructure;
                target.RepairTroops = source.RepairTroops;
                target.LayoutSource = source.LayoutSource;
                target.InfrastructurePresetName = source.InfrastructurePresetName;
                target.TroopPresetName = source.TroopPresetName;
                count++;
            }

            _copied = true;
            ShowStatus("Copied settings to " + count + " village(s).", false);
            BotLogger.Log("Castle Repair", BotLogLevel.Info,
                "Copied castle repair settings from " + sourceItem.Display + " to " + count + " village(s).");
        }

        private void ShowStatus(string text, bool isError)
        {
            _statusLabel.ForeColor = isError
                ? Color.FromArgb(255, 100, 100)
                : Color.FromArgb(100, 220, 100);
            _statusLabel.Text = text;
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

        private static Button MakeBtn(string text, Color bg, int x, int y)
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

        private class CrVillageItem
        {
            public int VillageId;
            public string Display;
            public CrVillageItem(int id, string display) { VillageId = id; Display = display; }
            public override string ToString() { return Display; }
        }
    }
}
