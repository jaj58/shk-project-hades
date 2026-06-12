using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    // =========================================================================
    // Building Row (displayed in the building list for a village)
    // =========================================================================

    internal class BuildingListRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color StatusOk = Color.FromArgb(80, 200, 120);
        private static readonly Color StatusWarn = Color.FromArgb(220, 180, 60);
        private static readonly Color StatusErr = Color.FromArgb(240, 80, 80);

        private BuildingEntry _entry;
        private Label _statusLabel;

        public BuildingEntry Entry { get { return _entry; } }

        public BuildingListRow(BuildingEntry entry, int index, int priorityRank = int.MinValue)
        {
            _entry = entry;
            this.Height = 22;
            this.BackColor = index % 2 == 0 ? BgEven : BgOdd;

            string buildingName = VillageBuilderModule.GetBuildingName(entry.BuildingType);
            if (priorityRank == VillageBuilderModule.AlwaysFirstRank)
                buildingName = "[*] " + buildingName;
            else if (priorityRank != int.MinValue)
                buildingName = "[" + (priorityRank + 1) + "] " + buildingName;

            AddLabel(buildingName, 8, 200, TextPri);
            AddLabel(entry.BuildingType.ToString(), 216, 40, TextSec);

            _statusLabel = AddLabel(GetStatusText(entry), 264, 120, GetStatusColor(entry));

            AddLabel(entry.X.ToString(), 392, 36, TextSec);
            AddLabel(entry.Y.ToString(), 436, 36, TextSec);

            if (entry.Placed)
            {
                Label checkLabel = AddLabel("\u2713", 480, 20, StatusOk);
                checkLabel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            }
        }

        public void RefreshStatus()
        {
            if (_statusLabel != null)
            {
                _statusLabel.Text = GetStatusText(_entry);
                _statusLabel.ForeColor = GetStatusColor(_entry);
            }
        }

        private static string GetStatusText(BuildingEntry entry)
        {
            if (entry.Placed) return "Placed";
            if (!string.IsNullOrEmpty(entry.Status)) return entry.Status;
            return "Pending";
        }

        private static Color GetStatusColor(BuildingEntry entry)
        {
            if (entry.Placed) return StatusOk;
            string s = entry.Status;
            if (s == "Constructing" || s == "Waiting for resources" || s == "Placing...") return StatusWarn;
            if (!string.IsNullOrEmpty(s) && s != "Pending" && s != "") return StatusErr;
            return Color.FromArgb(160, 165, 180);
        }

        private Label AddLabel(string text, int x, int width, Color color)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 7.5f);
            lbl.ForeColor = color;
            lbl.Location = new Point(x, 3);
            lbl.Size = new Size(width, 16);
            this.Controls.Add(lbl);
            return lbl;
        }
    }

    // =========================================================================
    // Copy Builder Settings Form
    // =========================================================================

    internal class CopyBuilderSettingsForm : Form
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

        public CopyBuilderSettingsForm()
        {
            _copied = false;
            this.Text = "Copy Village Builder Settings";
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

            List<int> ids = GameEngine.Instance.World.getUserVillageIDList();
            if (ids == null) return;

            foreach (int id in ids)
            {
                string name = GameEngine.Instance.World.getVillageName(id);
                BldVillageItem item = new BldVillageItem(id, "[" + id + "] " + name);
                _sourceCombo.Items.Add(item);
                _targetList.Items.Add(item);
            }

            if (_sourceCombo.Items.Count > 0)
                _sourceCombo.SelectedIndex = 0;
        }

        private void DoCopy()
        {
            BldVillageItem sourceItem = _sourceCombo.SelectedItem as BldVillageItem;
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
                    BldVillageItem item = (BldVillageItem)_targetList.Items[i];
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

            VillageBuilderSettings s = BotEngine.Instance.Settings.VillageBuilder;
            VillageBuildLayout source = s.GetOrCreateLayout(sourceItem.VillageId);

            int sourceTerrain = -1;
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                sourceTerrain = GameEngine.Instance.World.getVillageTerrainType(sourceItem.VillageId);

            int count = 0;
            int skipped = 0;
            foreach (int targetId in targetIds)
            {
                // A layout only makes sense on matching terrain — terrain-specific
                // buildings would be permanently unplaceable on the wrong type.
                if (sourceTerrain >= 0 &&
                    GameEngine.Instance.World.getVillageTerrainType(targetId) != sourceTerrain)
                {
                    skipped++;
                    continue;
                }

                VillageBuildLayout target = s.GetOrCreateLayout(targetId);
                target.CopyBuildingsFrom(source);
                target.Enabled = source.Enabled;
                count++;
            }

            _copied = count > 0;
            if (skipped > 0)
                ShowStatus("Copied to " + count + " village(s); skipped " + skipped + " with different terrain.", count == 0);
            else
                ShowStatus("Copied layout to " + count + " village(s).", false);
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

        private class BldVillageItem
        {
            public int VillageId;
            public string Display;
            public BldVillageItem(int id, string display) { VillageId = id; Display = display; }
            public override string ToString() { return Display; }
        }
    }

    // =========================================================================
    // Build Priority Form — reorderable category list (check = prioritized)
    // =========================================================================

    internal class BuilderPriorityForm : Form
    {
        private static readonly Color FormBg = Color.FromArgb(28, 30, 38);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color AccentCol = Color.FromArgb(80, 160, 255);

        private CheckedListBox _categoryList;
        private Label _contentsLabel;

        private bool _saved;
        public bool Saved { get { return _saved; } }

        public BuilderPriorityForm()
        {
            this.Text = "Build Priority";
            this.BackColor = FormBg;
            this.ForeColor = TextPri;
            this.Font = new Font("Segoe UI", 9f);
            this.ClientSize = new Size(420, 420);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;

            BuildUI();
            LoadFromSettings();
        }

        private void BuildUI()
        {
            Label pinnedTop = MakeLabel("Stockpile & Granary — always built first", 14, 12);
            pinnedTop.ForeColor = Color.FromArgb(100, 220, 100);
            this.Controls.Add(pinnedTop);

            Label orderLbl = MakeLabel("Build order (top = highest, check = prioritised):", 14, 36);
            this.Controls.Add(orderLbl);

            _categoryList = new CheckedListBox();
            _categoryList.BackColor = InputBg;
            _categoryList.ForeColor = TextPri;
            _categoryList.Font = new Font("Segoe UI", 9f);
            _categoryList.BorderStyle = BorderStyle.FixedSingle;
            _categoryList.CheckOnClick = true;
            _categoryList.IntegralHeight = false;
            _categoryList.Location = new Point(14, 56);
            _categoryList.Size = new Size(310, 160);
            _categoryList.SelectedIndexChanged += delegate { ShowCategoryContents(); };
            this.Controls.Add(_categoryList);

            Button upBtn = MakeBtn("Up", Color.FromArgb(70, 72, 84), 334, 56);
            upBtn.Size = new Size(70, 28);
            upBtn.Click += delegate { MoveSelected(-1); };
            this.Controls.Add(upBtn);

            Button downBtn = MakeBtn("Down", Color.FromArgb(70, 72, 84), 334, 90);
            downBtn.Size = new Size(70, 28);
            downBtn.Click += delegate { MoveSelected(1); };
            this.Controls.Add(downBtn);

            Label pinnedBottom = MakeLabel("Everything else — built last, in layout order", 14, 222);
            this.Controls.Add(pinnedBottom);

            _contentsLabel = new Label();
            _contentsLabel.Font = new Font("Segoe UI", 8f);
            _contentsLabel.ForeColor = TextSec;
            _contentsLabel.Location = new Point(14, 246);
            _contentsLabel.Size = new Size(392, 124);
            _contentsLabel.Text = "Select a category to see which buildings it includes.";
            this.Controls.Add(_contentsLabel);

            Button okBtn = MakeBtn("OK", AccentCol, 200, 382);
            okBtn.Click += delegate { SaveToSettings(); this.Close(); };
            this.Controls.Add(okBtn);

            Button cancelBtn = MakeBtn("Cancel", Color.FromArgb(70, 72, 84), 310, 382);
            cancelBtn.Click += delegate { this.Close(); };
            this.Controls.Add(cancelBtn);
        }

        private void LoadFromSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            List<BuilderCategoryPref> prefs = VillageBuilderModule.NormalizeCategoryPriority(
                BotEngine.Instance.Settings.VillageBuilder.CategoryPriority);

            _categoryList.Items.Clear();
            foreach (BuilderCategoryPref pref in prefs)
            {
                VillageBuilderModule.BuildCategory cat = FindCategory(pref.Key);
                if (cat != null)
                    _categoryList.Items.Add(new CategoryItem(cat), pref.Enabled);
            }

            if (_categoryList.Items.Count > 0)
                _categoryList.SelectedIndex = 0;
        }

        private void SaveToSettings()
        {
            if (BotEngine.Instance == null || BotEngine.Instance.Settings == null)
                return;

            List<BuilderCategoryPref> prefs = new List<BuilderCategoryPref>();
            for (int i = 0; i < _categoryList.Items.Count; i++)
            {
                CategoryItem item = (CategoryItem)_categoryList.Items[i];
                BuilderCategoryPref pref = new BuilderCategoryPref();
                pref.Key = item.Category.Key;
                pref.Enabled = _categoryList.GetItemChecked(i);
                prefs.Add(pref);
            }

            BotEngine.Instance.Settings.VillageBuilder.CategoryPriority = prefs;
            _saved = true;
        }

        private void MoveSelected(int direction)
        {
            int index = _categoryList.SelectedIndex;
            if (index < 0) return;
            int target = index + direction;
            if (target < 0 || target >= _categoryList.Items.Count) return;

            object item = _categoryList.Items[index];
            bool isChecked = _categoryList.GetItemChecked(index);
            _categoryList.Items.RemoveAt(index);
            _categoryList.Items.Insert(target, item);
            _categoryList.SetItemChecked(target, isChecked);
            _categoryList.SelectedIndex = target;
        }

        private void ShowCategoryContents()
        {
            CategoryItem item = _categoryList.SelectedItem as CategoryItem;
            if (item == null) return;

            List<string> names = new List<string>();
            foreach (int type in item.Category.Types)
                names.Add(VillageBuilderModule.GetBuildingName(type));
            _contentsLabel.Text = item.Category.DisplayName + " includes: " + string.Join(", ", names.ToArray());
        }

        private static VillageBuilderModule.BuildCategory FindCategory(string key)
        {
            foreach (VillageBuilderModule.BuildCategory cat in VillageBuilderModule.Categories)
                if (cat.Key == key) return cat;
            return null;
        }

        private static Label MakeLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 8.5f);
            lbl.ForeColor = TextSec;
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

        private class CategoryItem
        {
            public VillageBuilderModule.BuildCategory Category;
            public CategoryItem(VillageBuilderModule.BuildCategory cat) { Category = cat; }
            public override string ToString() { return Category.DisplayName; }
        }
    }
}
