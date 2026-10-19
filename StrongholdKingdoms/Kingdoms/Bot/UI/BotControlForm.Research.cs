using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CommonTypes;
using Kingdoms.Bot;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Research tab — the research plan (ordered list of research + target level) and options
    /// for ResearchModule. Built in code like the Banquet Sender tab, inserted after the Village
    /// Builder tab. Plan statuses come from ResearchPlanner, so they show even while the module
    /// is off. Edits are live; the Save Settings button persists them.
    /// </summary>
    public partial class BotControlForm
    {
        private static readonly Color RsDimText = Color.FromArgb(120, 125, 140);
        private static readonly Color RsBlockedText = Color.FromArgb(220, 140, 110);
        private static readonly Color RsNextText = Color.FromArgb(220, 180, 60);

        private TabPage _rsPage;
        private CheckBox _rsEnabledCheck;
        private Label _rsStatusLabel;
        private NumericUpDown _rsIntervalInput;
        private NumericUpDown _rsSlotsInput;
        private CheckBox _rsPrereqCheck;
        private ComboBox _rsRankUpCombo;
        private CheckBox _rsBuyCheck;
        private NumericUpDown _rsMaxGoldInput;
        private NumericUpDown _rsReserveInput;
        private Label _rsInfoLabel;
        private Label _rsQueueLabel;
        private Label _rsModuleLabel;
        private TreeView _rsTree;
        private ListView _rsPlanList;
        private NumericUpDown _rsTargetInput;
        private ToolTip _rsToolTip;
        private Timer _rsRefreshTimer;
        private bool _rsLoading;
        // Set while the plan list is being filled, so ItemChecked doesn't write back.
        private bool _rsPlanUpdating;
        private string _rsTreeSignature = "";
        private string _rsPlanSignature = "";
        // The settings object the controls were last loaded from (see BanquetSender tab).
        private ResearchSettings _rsLoadedFrom;

        private ResearchSettings RsSettings
        {
            get
            {
                return BotEngine.Instance != null && BotEngine.Instance.Settings != null
                    ? BotEngine.Instance.Settings.Research : null;
            }
        }

        private static ResearchModule RsModule
        {
            get { return BotEngine.Instance != null ? BotEngine.Instance.GetModule<ResearchModule>() : null; }
        }

        // Settings the plan buttons may edit: only once the controls were loaded from them.
        private ResearchSettings RsEditableSettings
        {
            get
            {
                ResearchSettings s = RsSettings;
                return s != null && s == _rsLoadedFrom ? s : null;
            }
        }

        private void WireUpResearchTab()
        {
            _rsPage = new TabPage();
            _rsPage.Text = "Research";
            _rsPage.BackColor = TtBg;
            // Start at the real page size so anchored controls don't stretch by the difference
            // from TabPage's 200x100 default when the page is inserted.
            _rsPage.Size = _builderPage.Size;
            int pageW = _rsPage.Width;
            int pageH = _rsPage.Height;

            _rsToolTip = new ToolTip();

            _rsEnabledCheck = new CheckBox();
            _rsEnabledCheck.Text = "Enabled";
            _rsEnabledCheck.ForeColor = TtText;
            _rsEnabledCheck.FlatStyle = FlatStyle.Flat;
            _rsEnabledCheck.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            _rsEnabledCheck.Location = new Point(10, 10);
            _rsEnabledCheck.Size = new Size(90, 22);
            _rsPage.Controls.Add(_rsEnabledCheck);

            _rsStatusLabel = TtMakeLabel("DISABLED", 104, 13, 90);
            _rsStatusLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            _rsPage.Controls.Add(_rsStatusLabel);

            Label intro = TtMakeLabel("Researches the plan from the top. Entries that need a higher rank are skipped " +
                "until you rank up; missing prerequisites can be researched first.", 200, 13, 920);
            intro.ForeColor = TextSec;
            _rsPage.Controls.Add(intro);

            // ---- Options ----
            _rsPage.Controls.Add(TtMakeLabel("Check every (s):", 10, 44, 95));
            _rsIntervalInput = TtMakeNumeric(108, 42, 3, 3600, 10, 60);
            _rsPage.Controls.Add(_rsIntervalInput);

            _rsPage.Controls.Add(TtMakeLabel("Fill up to (slots):", 186, 44, 105));
            _rsSlotsInput = TtMakeNumeric(294, 42, 1, ResearchPlanner.PremiumSlotLimit, 6, 50);
            _rsPage.Controls.Add(_rsSlotsInput);
            _rsToolTip.SetToolTip(_rsSlotsInput, "Research slots to keep filled (1 in progress + queued).\n" +
                "Only with a premium token - without one the game allows 1 research at a time.");

            _rsPrereqCheck = new CheckBox();
            _rsPrereqCheck.Text = "Research missing prerequisites first";
            _rsPrereqCheck.ForeColor = TtText;
            _rsPrereqCheck.FlatStyle = FlatStyle.Flat;
            _rsPrereqCheck.Font = new Font("Segoe UI", 8f);
            _rsPrereqCheck.Location = new Point(366, 43);
            _rsPrereqCheck.Size = new Size(250, 20);
            _rsPage.Controls.Add(_rsPrereqCheck);
            _rsToolTip.SetToolTip(_rsPrereqCheck, "E.g. a plan entry for Pitch Extraction researches Tools up to 4 first.\n" +
                "Off: entries with a missing prerequisite are skipped.");

            _rsPage.Controls.Add(TtMakeLabel("Rank up:", 630, 44, 55));
            _rsRankUpCombo = new ComboBox();
            _rsRankUpCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _rsRankUpCombo.Items.Add("Off");
            _rsRankUpCombo.Items.Add("When research needs it");
            _rsRankUpCombo.Items.Add("Whenever possible");
            _rsRankUpCombo.SelectedIndex = 0;
            _rsRankUpCombo.BackColor = TtInputBg;
            _rsRankUpCombo.ForeColor = TtText;
            _rsRankUpCombo.FlatStyle = FlatStyle.Flat;
            _rsRankUpCombo.Font = new Font("Segoe UI", 8f);
            _rsRankUpCombo.Location = new Point(688, 41);
            _rsRankUpCombo.Size = new Size(170, 21);
            _rsPage.Controls.Add(_rsRankUpCombo);
            _rsToolTip.SetToolTip(_rsRankUpCombo, "Spend honour on the next rank automatically (each rank up gives a research point).\n" +
                "When research needs it: only when out of research points, or when the rest of the plan needs a higher rank.");

            _rsBuyCheck = new CheckBox();
            _rsBuyCheck.Text = "Buy research points with gold";
            _rsBuyCheck.ForeColor = TtText;
            _rsBuyCheck.FlatStyle = FlatStyle.Flat;
            _rsBuyCheck.Font = new Font("Segoe UI", 8f);
            _rsBuyCheck.Location = new Point(10, 73);
            _rsBuyCheck.Size = new Size(220, 20);
            _rsPage.Controls.Add(_rsBuyCheck);
            _rsToolTip.SetToolTip(_rsBuyCheck, "Only when out of points, something in the plan can be started and a slot is free.\n" +
                "The price goes up with every point bought.");

            _rsPage.Controls.Add(TtMakeLabel("Max gold per point:", 240, 75, 115));
            _rsMaxGoldInput = TtMakeNumeric(358, 73, 0, 1000000000, 100000, 100);
            _rsMaxGoldInput.ThousandsSeparator = true;
            _rsPage.Controls.Add(_rsMaxGoldInput);

            _rsPage.Controls.Add(TtMakeLabel("Keep at least (gold):", 476, 75, 120));
            _rsReserveInput = TtMakeNumeric(598, 73, 0, 1000000000, 0, 100);
            _rsReserveInput.ThousandsSeparator = true;
            _rsPage.Controls.Add(_rsReserveInput);

            // ---- Live game state ----
            _rsInfoLabel = TtMakeLabel("", 10, 106, pageW - 20);
            _rsInfoLabel.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            _rsInfoLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _rsPage.Controls.Add(_rsInfoLabel);
            _rsQueueLabel = TtMakeLabel("", 10, 126, pageW - 20);
            _rsQueueLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _rsPage.Controls.Add(_rsQueueLabel);
            _rsModuleLabel = TtMakeLabel("", 10, 146, pageW - 20);
            _rsModuleLabel.ForeColor = RsNextText;
            _rsModuleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _rsPage.Controls.Add(_rsModuleLabel);

            // ---- Available researches (left) ----
            const int listTop = 192;
            const int treeW = 360;
            const int btnW = 100;
            int listH = pageH - listTop - 42;
            int planX = 10 + treeW + 10;
            int btnX = pageW - btnW - 10;
            int planW = btnX - 8 - planX;

            _rsPage.Controls.Add(TtMakeLabel("Available researches (double-click to add):", 10, listTop - 18, treeW));
            _rsTree = new TreeView();
            _rsTree.BackColor = TtInputBg;
            _rsTree.ForeColor = TtText;
            _rsTree.BorderStyle = BorderStyle.FixedSingle;
            _rsTree.Font = new Font("Segoe UI", 8.5f);
            _rsTree.HideSelection = false;
            _rsTree.FullRowSelect = true;
            _rsTree.ShowLines = false;
            _rsTree.Location = new Point(10, listTop);
            _rsTree.Size = new Size(treeW, listH);
            _rsTree.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            _rsPage.Controls.Add(_rsTree);

            // ---- Plan (right) ----
            Label planLabel = TtMakeLabel("Research plan (researched from the top; untick to pause an entry):", planX, listTop - 18, 500);
            _rsPage.Controls.Add(planLabel);
            _rsPlanList = new ListView();
            _rsPlanList.View = View.Details;
            _rsPlanList.FullRowSelect = true;
            _rsPlanList.MultiSelect = false;
            _rsPlanList.HideSelection = false;
            _rsPlanList.CheckBoxes = true;
            _rsPlanList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            _rsPlanList.BackColor = TtInputBg;
            _rsPlanList.ForeColor = TtText;
            _rsPlanList.BorderStyle = BorderStyle.FixedSingle;
            _rsPlanList.Font = new Font("Segoe UI", 8.5f);
            _rsPlanList.Location = new Point(planX, listTop);
            _rsPlanList.Size = new Size(planW, listH);
            _rsPlanList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            _rsPlanList.Columns.Add("#", 44);
            _rsPlanList.Columns.Add("Research", 170);
            _rsPlanList.Columns.Add("Target", 50);
            _rsPlanList.Columns.Add("Level", 90);
            _rsPlanList.Columns.Add("Status", Math.Max(150, planW - 44 - 170 - 50 - 90 - 22));
            _rsPage.Controls.Add(_rsPlanList);

            string[] btnText = { "▲ Up", "▼ Down", "Remove", "Set target", "Clear finished", "Export…", "Import…" };
            Button[] btns = new Button[btnText.Length];
            for (int i = 0; i < btnText.Length; i++)
            {
                btns[i] = TtMakeButton(btnText[i], btnX, listTop + i * 30, btnW);
                btns[i].Anchor = AnchorStyles.Top | AnchorStyles.Right;
                _rsPage.Controls.Add(btns[i]);
            }
            btns[0].Click += delegate { RsMoveSelected(-1); };
            btns[1].Click += delegate { RsMoveSelected(1); };
            btns[2].Click += delegate { RsRemoveSelected(); };
            btns[3].Click += delegate { RsSetSelectedTarget(); };
            btns[4].Click += delegate { RsClearFinished(); };
            btns[5].Click += delegate { RsExportPlan(); };
            btns[6].Click += delegate { RsImportPlan(); };
            _rsToolTip.SetToolTip(btns[3], "Set the selected plan entry's target to the Target level below.");
            _rsToolTip.SetToolTip(btns[4], "Remove entries that have reached their target.");

            // ---- Add row (bottom) ----
            int addY = pageH - 34;
            Label targetLabel = TtMakeLabel("Target level:", 10, addY + 3, 75);
            targetLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _rsPage.Controls.Add(targetLabel);
            _rsTargetInput = TtMakeNumeric(88, addY + 1, 1, ResearchPlanner.MaxTargetLevel, 10, 50);
            _rsTargetInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _rsPage.Controls.Add(_rsTargetInput);
            Button addBtn = TtMakeButton("Add to plan ▶", 146, addY, 110);
            addBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            addBtn.Click += delegate { RsAddSelectedTreeNode(); };
            _rsPage.Controls.Add(addBtn);
            Button addCatBtn = TtMakeButton("Add category", 262, addY, 108);
            addCatBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            addCatBtn.Click += delegate { RsAddSelectedCategory(); };
            _rsPage.Controls.Add(addCatBtn);
            _rsToolTip.SetToolTip(addBtn, "Add the selected research at the Target level\n" +
                "(or update its target if it's already in the plan).");
            _rsToolTip.SetToolTip(addCatBtn, "Add every research in the selected category at its max level.");

            // ---- Events (live-but-not-saved, like the other tabs) ----
            _rsEnabledCheck.CheckedChanged += delegate { RsWriteToSettings(); };
            _rsIntervalInput.ValueChanged += delegate { RsWriteToSettings(); };
            _rsSlotsInput.ValueChanged += delegate { RsWriteToSettings(); };
            _rsPrereqCheck.CheckedChanged += delegate { RsWriteToSettings(); };
            _rsRankUpCombo.SelectedIndexChanged += delegate { RsWriteToSettings(); };
            _rsBuyCheck.CheckedChanged += delegate { RsWriteToSettings(); };
            _rsMaxGoldInput.ValueChanged += delegate { RsWriteToSettings(); };
            _rsReserveInput.ValueChanged += delegate { RsWriteToSettings(); };

            _rsTree.AfterSelect += delegate (object sender, TreeViewEventArgs e) { RsTreeSelected(e.Node); };
            _rsTree.NodeMouseDoubleClick += delegate (object sender, TreeNodeMouseClickEventArgs e)
            {
                if (e.Node != null && e.Node.Tag is int)
                    RsAddToPlan((int)e.Node.Tag, RsAbsoluteMax((int)e.Node.Tag));
            };
            _rsPlanList.ItemChecked += delegate (object sender, ItemCheckedEventArgs e) { RsPlanItemChecked(e.Item); };
            _rsPlanList.SelectedIndexChanged += delegate { RsPlanSelected(); };
            _tabControl.SelectedIndexChanged += delegate
            {
                if (_tabControl.SelectedTab == _rsPage) RsUpdateStatusDisplay(true);
            };

            // TabPages.Insert is silently ignored until the TabControl has a window handle
            // (WinForms quirk), and this runs from the constructor — force the handle first.
            IntPtr tabHandle = _tabControl.Handle;
            int builderIndex = _tabControl.TabPages.IndexOf(_builderPage);
            if (builderIndex >= 0) _tabControl.TabPages.Insert(builderIndex + 1, _rsPage);
            else _tabControl.TabPages.Add(_rsPage);

            RsRefreshTree(true);

            _rsRefreshTimer = new Timer();
            _rsRefreshTimer.Interval = 1500;
            _rsRefreshTimer.Tick += delegate { try { RsUpdateStatusDisplay(false); } catch { } };
            _rsRefreshTimer.Start();
        }

        // =====================================================================
        // Settings load / write
        // =====================================================================

        private void RsLoadFromSettings()
        {
            ResearchSettings s = RsSettings;
            if (s == null) return;
            _rsLoading = true;
            try
            {
                s.Normalize();
                _rsEnabledCheck.Checked = s.Enabled;
                RsSetNumeric(_rsIntervalInput, s.CheckIntervalSeconds);
                RsSetNumeric(_rsSlotsInput, s.MaxQueueSlots);
                _rsPrereqCheck.Checked = s.AutoPrerequisites;
                int mode = (int)s.RankUpMode;
                _rsRankUpCombo.SelectedIndex = mode >= 0 && mode < _rsRankUpCombo.Items.Count ? mode : 0;
                _rsBuyCheck.Checked = s.BuyPointsWithGold;
                RsSetNumeric(_rsMaxGoldInput, s.MaxGoldPerPoint);
                RsSetNumeric(_rsReserveInput, s.MinGoldReserve);
                _rsLoadedFrom = s;
            }
            finally { _rsLoading = false; }
            _rsPlanSignature = "";
            _rsTreeSignature = "";
            RsUpdateStatusDisplay(true);
        }

        private void RsWriteToSettings()
        {
            if (_rsLoading) return;
            ResearchSettings s = RsEditableSettings;
            if (s == null) return;

            s.Enabled = _rsEnabledCheck.Checked;
            s.CheckIntervalSeconds = (int)_rsIntervalInput.Value;
            s.MaxQueueSlots = (int)_rsSlotsInput.Value;
            s.AutoPrerequisites = _rsPrereqCheck.Checked;
            // An unset combo (-1) would serialize as an invalid enum and break saving.
            if (_rsRankUpCombo.SelectedIndex >= 0)
                s.RankUpMode = (ResearchRankUpMode)_rsRankUpCombo.SelectedIndex;
            s.BuyPointsWithGold = _rsBuyCheck.Checked;
            s.MaxGoldPerPoint = (int)_rsMaxGoldInput.Value;
            s.MinGoldReserve = (int)_rsReserveInput.Value;

            ResearchModule m = RsModule;
            if (m != null) m.Enabled = s.Enabled;
            RsUpdateStatusDisplay(true);
        }

        private static void RsSetNumeric(NumericUpDown nud, int value)
        {
            nud.Value = Math.Max(nud.Minimum, Math.Min(nud.Maximum, value));
        }

        // =====================================================================
        // Plan editing
        // =====================================================================

        private int RsAbsoluteMax(int type)
        {
            WorldData wd = GameEngine.Instance != null ? GameEngine.Instance.LocalWorldData : null;
            if (wd == null) return (int)_rsTargetInput.Value;
            try { return Math.Max(1, ResearchPlanner.AbsoluteMaxLevel(type, wd)); }
            catch { return (int)_rsTargetInput.Value; }
        }

        private void RsTreeSelected(TreeNode node)
        {
            if (node == null || !(node.Tag is int)) return;
            RsSetNumeric(_rsTargetInput, RsAbsoluteMax((int)node.Tag));
        }

        private void RsAddSelectedTreeNode()
        {
            TreeNode node = _rsTree.SelectedNode;
            if (node == null || !(node.Tag is int))
            {
                MessageBox.Show(this, "Select a research in the list on the left first.", "Research",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RsAddToPlan((int)node.Tag, (int)_rsTargetInput.Value);
        }

        private void RsAddSelectedCategory()
        {
            ResearchSettings s = RsEditableSettings;
            if (s == null) { RsNotInWorld(); return; }
            TreeNode node = _rsTree.SelectedNode;
            if (node != null && node.Tag is int) node = node.Parent;
            ResearchPlanner.Category cat = node != null ? node.Tag as ResearchPlanner.Category : null;
            if (cat == null)
            {
                MessageBox.Show(this, "Select a category (or a research in it) on the left first.", "Research",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int added = 0;
            foreach (int type in cat.Types)
            {
                if (RsPlanIndexOf(s, type) >= 0) continue;
                s.Plan.Add(new ResearchPlanEntry { ResearchType = type, TargetLevel = RsAbsoluteMax(type) });
                added++;
            }
            RsRefreshPlan(true, s.Plan.Count - 1);
            RsRefreshTree(true);
            BotLogger.Log("UI", BotLogLevel.Info, "Research plan: added " + added + " " + cat.Name + " research(es).");
        }

        private void RsAddToPlan(int type, int target)
        {
            ResearchSettings s = RsEditableSettings;
            if (s == null) { RsNotInWorld(); return; }
            target = Math.Max(1, Math.Min(ResearchPlanner.MaxTargetLevel, target));
            int index = RsPlanIndexOf(s, type);
            if (index >= 0)
            {
                s.Plan[index].TargetLevel = target;
                s.Plan[index].Enabled = true;
            }
            else
            {
                s.Plan.Add(new ResearchPlanEntry { ResearchType = type, TargetLevel = target });
                index = s.Plan.Count - 1;
            }
            RsRefreshPlan(true, index);
            RsRefreshTree(true);
        }

        private static int RsPlanIndexOf(ResearchSettings s, int type)
        {
            for (int i = 0; i < s.Plan.Count; i++)
                if (s.Plan[i].ResearchType == type) return i;
            return -1;
        }

        private int RsSelectedPlanIndex()
        {
            return _rsPlanList.SelectedIndices.Count > 0 ? _rsPlanList.SelectedIndices[0] : -1;
        }

        private void RsMoveSelected(int dir)
        {
            ResearchSettings s = RsEditableSettings;
            int i = RsSelectedPlanIndex();
            if (s == null || i < 0 || i >= s.Plan.Count) return;
            int j = i + dir;
            if (j < 0 || j >= s.Plan.Count) return;
            ResearchPlanEntry e = s.Plan[i];
            s.Plan.RemoveAt(i);
            s.Plan.Insert(j, e);
            RsRefreshPlan(true, j);
        }

        private void RsRemoveSelected()
        {
            ResearchSettings s = RsEditableSettings;
            int i = RsSelectedPlanIndex();
            if (s == null || i < 0 || i >= s.Plan.Count) return;
            s.Plan.RemoveAt(i);
            RsRefreshPlan(true, Math.Min(i, s.Plan.Count - 1));
            RsRefreshTree(true);
        }

        private void RsSetSelectedTarget()
        {
            ResearchSettings s = RsEditableSettings;
            int i = RsSelectedPlanIndex();
            if (s == null || i < 0 || i >= s.Plan.Count) return;
            s.Plan[i].TargetLevel = (int)_rsTargetInput.Value;
            RsRefreshPlan(true, i);
        }

        private void RsClearFinished()
        {
            ResearchSettings s = RsEditableSettings;
            if (s == null) { RsNotInWorld(); return; }
            ResearchPlanner.PlanEvaluation eval = RsEvaluate(s);
            if (eval == null) { RsNotInWorld(); return; }
            int removed = 0;
            for (int i = Math.Min(s.Plan.Count, eval.Entries.Length) - 1; i >= 0; i--)
            {
                if (eval.Entries[i].State != ResearchPlanner.EntryState.Done) continue;
                s.Plan.RemoveAt(i);
                removed++;
            }
            RsRefreshPlan(true, -1);
            RsRefreshTree(true);
            BotLogger.Log("UI", BotLogLevel.Info, "Research plan: removed " + removed + " finished entr" + (removed == 1 ? "y." : "ies."));
        }

        private void RsPlanItemChecked(ListViewItem item)
        {
            if (_rsPlanUpdating || item == null) return;
            ResearchSettings s = RsEditableSettings;
            ResearchPlanEntry e = item.Tag as ResearchPlanEntry;
            if (s == null || e == null || !s.Plan.Contains(e)) return;
            e.Enabled = item.Checked;
            RsRefreshPlan(false, -1);
        }

        private void RsPlanSelected()
        {
            ResearchSettings s = RsEditableSettings;
            int i = RsSelectedPlanIndex();
            if (s == null || i < 0 || i >= s.Plan.Count) return;
            RsSetNumeric(_rsTargetInput, s.Plan[i].TargetLevel);
        }

        private void RsNotInWorld()
        {
            MessageBox.Show(this, "Log into a world first - the research plan is saved per world.", "Research",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =====================================================================
        // Export / import
        // =====================================================================

        private const string RsPlanFileHeader = "#hades-research-plan v1";

        private void RsExportPlan()
        {
            ResearchSettings s = RsEditableSettings;
            if (s == null) { RsNotInWorld(); return; }
            if (s.Plan.Count == 0)
            {
                MessageBox.Show(this, "The plan is empty.", "Research", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Research Plan (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.DefaultExt = "txt";
            dlg.FileName = "research_plan.txt";
            dlg.Title = "Export Research Plan";
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                using (StreamWriter w = new StreamWriter(dlg.FileName))
                {
                    w.WriteLine(RsPlanFileHeader);
                    w.WriteLine("# researchType;targetLevel;enabled;name (name is ignored on import)");
                    foreach (ResearchPlanEntry e in s.Plan)
                        w.WriteLine(e.ResearchType + ";" + e.TargetLevel + ";" + (e.Enabled ? "1" : "0") + ";"
                            + ResearchPlanner.Name(e.ResearchType));
                }
                BotLogger.Log("UI", BotLogLevel.Info, "Research plan exported (" + s.Plan.Count + " entries) to " + dlg.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Export failed: " + ex.Message, "Research", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RsImportPlan()
        {
            ResearchSettings s = RsEditableSettings;
            if (s == null) { RsNotInWorld(); return; }

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Research Plan (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.Title = "Import Research Plan";
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            List<ResearchPlanEntry> imported = new List<ResearchPlanEntry>();
            try
            {
                using (StreamReader r = new StreamReader(dlg.FileName))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length == 0 || line.StartsWith("#")) continue;
                        string[] parts = line.Split(';');
                        int type, target;
                        if (parts.Length < 2 || !int.TryParse(parts[0].Trim(), out type)
                            || !int.TryParse(parts[1].Trim(), out target))
                            continue;
                        bool enabled = parts.Length < 3 || parts[2].Trim() != "0";
                        imported.Add(new ResearchPlanEntry { ResearchType = type, TargetLevel = target, Enabled = enabled });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Import failed: " + ex.Message, "Research", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate on a scratch copy so a bad file can't touch the current plan.
            ResearchSettings check = new ResearchSettings();
            check.Plan = imported;
            check.Normalize();
            if (check.Plan.Count == 0)
            {
                MessageBox.Show(this, "No valid research entries found in that file.", "Research",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool replace = true;
            if (s.Plan.Count > 0)
            {
                DialogResult choice = MessageBox.Show(this,
                    "Import " + check.Plan.Count + " entr" + (check.Plan.Count == 1 ? "y" : "ies") + ".\n\n" +
                    "Yes = replace the current plan\nNo = add to the end (research already in the plan keeps its current target)",
                    "Import Research Plan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (choice == DialogResult.Cancel) return;
                replace = choice == DialogResult.Yes;
            }

            if (replace) s.Plan = check.Plan;
            else s.Plan.AddRange(check.Plan);
            s.Normalize();
            RsRefreshPlan(true, -1);
            RsRefreshTree(true);
            BotLogger.Log("UI", BotLogLevel.Info, "Research plan imported from " + dlg.FileName + " (" + s.Plan.Count + " entries now).");
        }

        // =====================================================================
        // Live display
        // =====================================================================

        private ResearchPlanner.PlanEvaluation RsEvaluate(ResearchSettings s)
        {
            try
            {
                ResearchModule m = RsModule;
                return ResearchPlanner.EvaluateLive(s, m != null ? m.ParkedTypes : null);
            }
            catch
            {
                return null;
            }
        }

        private void RsUpdateStatusDisplay(bool force)
        {
            if (_rsPage == null) return;
            ResearchSettings s = RsSettings;
            if (s != null && s != _rsLoadedFrom)
            {
                RsLoadFromSettings();
                return;
            }
            bool enabled = _rsEnabledCheck.Checked;
            _rsStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _rsStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;
            if (!force && _tabControl.SelectedTab != _rsPage) return;

            ResearchPlanner.PlanEvaluation eval = s != null ? RsEvaluate(s) : null;
            RsUpdateInfo(eval);
            RsRefreshTree(false);
            RsRefreshPlan(false, -1, eval);
        }

        private void RsUpdateInfo(ResearchPlanner.PlanEvaluation eval)
        {
            ResearchModule m = RsModule;
            WorldMap world = GameEngine.Instance != null ? GameEngine.Instance.World : null;
            ResearchData data = world != null ? world.UserResearchData : null;
            WorldData wd = GameEngine.Instance != null ? GameEngine.Instance.LocalWorldData : null;
            if (m == null || data == null || wd == null || eval == null)
            {
                _rsInfoLabel.Text = "Not running - log into a world first.";
                _rsInfoLabel.ForeColor = TtText;
                _rsQueueLabel.Text = "";
                _rsModuleLabel.Text = "";
                return;
            }

            StringBuilder info = new StringBuilder();
            if (eval.Premium)
            {
                info.Append("Premium token: yes");
                try
                {
                    TimeSpan left = GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry - VillageMap.getCurrentServerTime();
                    info.Append(" (" + RsFormatSpan(left) + " left)");
                }
                catch { }
                info.Append(" - up to " + eval.SlotLimit + " slot" + (eval.SlotLimit == 1 ? "" : "s"));
            }
            else
            {
                info.Append("Premium token: no - one research at a time");
            }
            info.Append("     Research points: " + Math.Max(0, eval.SpendablePoints));
            int rank = world.getRank();
            int sub = world.getRankSubLevel();
            info.Append("     Rank " + (rank + 1) + " (level " + (sub + 1) + ")");
            double honour = world.getCurrentHonour();
            double needed = ResearchPlanner.HonourForNextRank(rank, sub, wd);
            info.Append("     Honour: " + honour.ToString("N0") + (needed >= 0 ? " / " + needed.ToString("N0") + " for next rank" : ""));
            _rsInfoLabel.Text = info.ToString();
            _rsInfoLabel.ForeColor = TtText;

            if (data.researchingType >= 0)
            {
                Dictionary<int, int> levels = new Dictionary<int, int>();
                StringBuilder q = new StringBuilder();
                q.Append("Researching: " + RsQueueStep(data, data.researchingType, levels));
                int secs = (int)(data.research_completionTime - VillageMap.getCurrentServerTime()).TotalSeconds;
                q.Append(secs > 0 ? " - " + VillageMap.createBuildTimeString(secs) + " left" : " - finishing");
                if (data.research_queueEntries != null && data.research_queueEntries.Length > 0)
                {
                    q.Append("     Queue: ");
                    for (int i = 0; i < data.research_queueEntries.Length; i++)
                    {
                        if (i > 0) q.Append(", ");
                        q.Append(RsQueueStep(data, data.research_queueEntries[i], levels));
                    }
                }
                q.Append("     (" + eval.SlotsUsed + "/" + eval.SlotLimit + " slots used)");
                _rsQueueLabel.Text = q.ToString();
            }
            else
            {
                _rsQueueLabel.Text = "Researching: nothing";
            }

            if (!m.Enabled)
                _rsModuleLabel.Text = "Module disabled" + (eval.NextPick != null
                    ? " - would research " + ResearchPlanner.Name(eval.NextPick.ResearchType) + " " + eval.NextPick.ToLevel + " next."
                    : ".");
            else
                _rsModuleLabel.Text = string.IsNullOrEmpty(m.StatusText) ? "Running." : m.StatusText;
        }

        // "Tools 4" — the level a queued research step will reach, counting earlier steps.
        private static string RsQueueStep(ResearchData data, int type, Dictionary<int, int> levels)
        {
            if (type < 0 || type >= data.research.Length) return "?";
            int level;
            if (!levels.TryGetValue(type, out level)) level = data.research[type];
            levels[type] = ++level;
            return ResearchPlanner.Name(type) + " " + level;
        }

        private static string RsFormatSpan(TimeSpan span)
        {
            if (span.TotalSeconds <= 0) return "0m";
            if (span.TotalDays >= 1) return (int)span.TotalDays + "d " + span.Hours + "h";
            if (span.TotalHours >= 1) return (int)span.TotalHours + "h " + span.Minutes + "m";
            return Math.Max(1, (int)span.TotalMinutes) + "m";
        }

        private void RsRefreshTree(bool force)
        {
            WorldMap world = GameEngine.Instance != null ? GameEngine.Instance.World : null;
            ResearchData data = world != null ? world.UserResearchData : null;
            WorldData wd = GameEngine.Instance != null ? GameEngine.Instance.LocalWorldData : null;
            if (wd == null) data = null;
            ResearchSettings s = RsSettings;
            bool alternate = wd != null && wd.Alternate_Ruleset == 1;

            int rank = world != null ? world.getRank() : 0;
            int sub = world != null ? world.getRankSubLevel() : 0;
            HashSet<int> inPlan = new HashSet<int>();
            if (s != null)
                foreach (ResearchPlanEntry e in s.Plan) inPlan.Add(e.ResearchType);

            StringBuilder sig = new StringBuilder();
            sig.Append(alternate ? "A" : "N");
            foreach (int t in inPlan) sig.Append(",p").Append(t);
            if (data != null)
            {
                sig.Append("|").Append(rank).Append(".").Append(sub).Append("|").Append(data.researchingType);
                if (data.research_queueEntries != null)
                    foreach (int t in data.research_queueEntries) sig.Append(",q").Append(t);
                sig.Append("|").Append(Convert.ToBase64String(data.research));
            }
            string signature = sig.ToString();
            if (!force && signature == _rsTreeSignature) return;
            _rsTreeSignature = signature;

            // Keep expansion and selection across rebuilds.
            HashSet<string> collapsed = new HashSet<string>();
            foreach (TreeNode cn in _rsTree.Nodes)
                if (!cn.IsExpanded) collapsed.Add(cn.Text);
            object selectedTag = _rsTree.SelectedNode != null ? _rsTree.SelectedNode.Tag : null;
            int selectedType = selectedTag is int ? (int)selectedTag : -1;
            string selectedCat = selectedTag is ResearchPlanner.Category ? ((ResearchPlanner.Category)selectedTag).Name : null;
            bool firstBuild = _rsTree.Nodes.Count == 0;

            ResearchData queued = null;
            try { if (data != null) queued = ResearchPlanner.BuildQueued(data); }
            catch { queued = null; }

            _rsTree.BeginUpdate();
            try
            {
                _rsTree.Nodes.Clear();
                TreeNode reselect = null;
                foreach (ResearchPlanner.Category cat in ResearchPlanner.PlayerTabs(alternate))
                {
                    TreeNode cn = new TreeNode(cat.Name);
                    cn.Tag = cat;
                    cn.ForeColor = AccentCol;
                    if (cat.Name == selectedCat) reselect = cn;
                    foreach (int type in cat.Types)
                    {
                        string text = ResearchPlanner.Name(type);
                        Color color = TtText;
                        if (queued != null && type < data.research.Length)
                        {
                            int current = data.research[type];
                            int queuedLevel = queued.research[type];
                            int max = ResearchPlanner.MaxLevel(type, rank, wd);
                            text += "   " + current + "/" + max;
                            if (queuedLevel > current) text += " (→" + queuedLevel + " queued)";
                            string block = ResearchPlanner.StepBlockText(queued, type, rank, sub, wd);
                            if (block == "max") { text += " - done"; color = RsDimText; }
                            else if (block.Length > 0) { text += " - " + block; color = RsBlockedText; }
                        }
                        if (inPlan.Contains(type)) text += "   [in plan]";
                        TreeNode node = new TreeNode(text);
                        node.Tag = type;
                        node.ForeColor = color;
                        cn.Nodes.Add(node);
                        if (type == selectedType) reselect = node;
                    }
                    _rsTree.Nodes.Add(cn);
                    if (firstBuild || !collapsed.Contains(cat.Name)) cn.Expand();
                }
                if (reselect != null) _rsTree.SelectedNode = reselect;
                if (firstBuild && _rsTree.Nodes.Count > 0) _rsTree.Nodes[0].EnsureVisible();
            }
            finally { _rsTree.EndUpdate(); }
        }

        private void RsRefreshPlan(bool rebuild, int selectIndex)
        {
            ResearchSettings s = RsSettings;
            RsRefreshPlan(rebuild, selectIndex, s != null ? RsEvaluate(s) : null);
        }

        private void RsRefreshPlan(bool rebuild, int selectIndex, ResearchPlanner.PlanEvaluation eval)
        {
            ResearchSettings s = RsEditableSettings;
            List<ResearchPlanEntry> plan = s != null ? s.Plan : new List<ResearchPlanEntry>();

            StringBuilder sig = new StringBuilder();
            foreach (ResearchPlanEntry e in plan) sig.Append(e.ResearchType).Append(',');
            string signature = sig.ToString();

            _rsPlanUpdating = true;
            try
            {
                if (rebuild || signature != _rsPlanSignature || _rsPlanList.Items.Count != plan.Count)
                {
                    if (selectIndex < 0) selectIndex = RsSelectedPlanIndex();
                    _rsPlanList.BeginUpdate();
                    _rsPlanList.Items.Clear();
                    foreach (ResearchPlanEntry e in plan)
                    {
                        ListViewItem item = new ListViewItem(new string[] { "", "", "", "", "" });
                        item.Tag = e;
                        item.Checked = e.Enabled;
                        _rsPlanList.Items.Add(item);
                    }
                    _rsPlanList.EndUpdate();
                    _rsPlanSignature = signature;
                    if (selectIndex >= 0 && selectIndex < _rsPlanList.Items.Count)
                    {
                        _rsPlanList.Items[selectIndex].Selected = true;
                        _rsPlanList.Items[selectIndex].Focused = true;
                        _rsPlanList.EnsureVisible(selectIndex);
                    }
                }

                for (int i = 0; i < plan.Count && i < _rsPlanList.Items.Count; i++)
                {
                    ResearchPlanEntry e = plan[i];
                    ListViewItem item = _rsPlanList.Items[i];
                    item.Tag = e;
                    if (item.Checked != e.Enabled) item.Checked = e.Enabled;

                    ResearchPlanner.EntryResult r = eval != null && i < eval.Entries.Length ? eval.Entries[i] : null;
                    string level = "-";
                    string status = s == null ? "" : "Log into a world to see the status";
                    Color color = TtText;
                    if (r != null)
                    {
                        status = r.Text;
                        if (r.State != ResearchPlanner.EntryState.Unavailable)
                            level = r.CurrentLevel + (r.QueuedLevel > r.CurrentLevel ? " (→" + r.QueuedLevel + ")" : "") + " / " + r.MaxLevel;
                        color = RsStateColor(r.State);
                    }
                    RsSetSubItem(item, 0, (i + 1).ToString());
                    RsSetSubItem(item, 1, ResearchPlanner.Name(e.ResearchType));
                    RsSetSubItem(item, 2, e.TargetLevel.ToString());
                    RsSetSubItem(item, 3, level);
                    RsSetSubItem(item, 4, status);
                    if (item.ForeColor != color) item.ForeColor = color;
                }
            }
            finally { _rsPlanUpdating = false; }
        }

        private static void RsSetSubItem(ListViewItem item, int index, string text)
        {
            if (item.SubItems[index].Text != text) item.SubItems[index].Text = text;
        }

        private static Color RsStateColor(ResearchPlanner.EntryState state)
        {
            switch (state)
            {
                case ResearchPlanner.EntryState.Done: return SuccessCol;
                case ResearchPlanner.EntryState.Queued: return AccentCol;
                case ResearchPlanner.EntryState.Next: return RsNextText;
                case ResearchPlanner.EntryState.NeedsRank:
                case ResearchPlanner.EntryState.Blocked: return RsBlockedText;
                case ResearchPlanner.EntryState.Disabled:
                case ResearchPlanner.EntryState.Unavailable: return RsDimText;
                default: return TtText;
            }
        }
    }
}
