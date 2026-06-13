using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Kingdoms.Bot;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Timing Tool tab — a coordinated-attack window planner. The operator sets a target, a list
    /// of source villages, and requirements; the tool computes max-speed travel times for every
    /// source/card/attack-type combination and finds the source selections whose attacks land in
    /// the tightest possible window. Results are browsable and pushable into Auto Bomb Multi.
    ///
    /// The search math is reused from AutoBombModule (CalculateBaseTravelTimeMaxResearch /
    /// ApplyCardSpeed); the combinatorial search lives in the GameEngine-free TimingSearch class.
    /// </summary>
    public partial class BotControlForm
    {
        // Theme colors (match the other tabs).
        private static readonly Color TtBg       = Color.FromArgb(24, 24, 32);
        private static readonly Color TtText     = Color.FromArgb(230, 230, 240);
        private static readonly Color TtInputBg  = Color.FromArgb(50, 52, 64);
        private static readonly Color TtBtnBg    = Color.FromArgb(60, 64, 84);

        private TabPage _timingPage;
        private TextBox _ttTargetVidBox;
        private ListBox _ttSourceListBox;
        private TextBox _ttAddIdsBox;
        private Button  _ttAddIdsBtn;
        private Button  _ttAddMapBtn;
        private Button  _ttRemoveBtn;
        private Button  _ttClearBtn;
        private NumericUpDown _ttMinNormalInput;
        private NumericUpDown _ttMinCaptainInput;
        private NumericUpDown _ttMaxWindowInput;
        private CheckBox _ttVassalSendingCheck;
        private Button   _ttSearchBtn;
        private Label    _ttStatusLabel;
        private Label    _ttResultIndexLabel;
        private Button   _ttPrevBtn;
        private Button   _ttNextBtn;
        private Button   _ttPushBtn;
        private FlowLayoutPanel _ttResultPanel;

        // cardType int -> checkbox. x2=1, x4=2, x6=3, x3=5, x5=6.
        private Dictionary<int, CheckBox> _ttCardChecks;

        private bool _ttLoading;
        private List<TimingSearch.TtSetup> _ttResults = new List<TimingSearch.TtSetup>();
        private int _ttResultIdx;

        private TimingToolSettings TtSettings
        {
            get
            {
                return BotEngine.Instance != null && BotEngine.Instance.Settings != null
                    ? BotEngine.Instance.Settings.TimingTool : null;
            }
        }

        // =====================================================================
        // Tab construction
        // =====================================================================

        private void WireUpTimingTab()
        {
            _timingPage = new TabPage();
            _timingPage.Text = "Timing Tool";
            _timingPage.BackColor = TtBg;

            // ---- Inputs (left column) ----
            _timingPage.Controls.Add(TtMakeLabel("Target Village ID:", 8, 12, 110));
            _ttTargetVidBox = TtMakeTextBox(122, 10, 90);
            _timingPage.Controls.Add(_ttTargetVidBox);

            _timingPage.Controls.Add(TtMakeLabel("Source Villages:", 8, 42, 150));
            _ttSourceListBox = new ListBox();
            _ttSourceListBox.BackColor = TtInputBg;
            _ttSourceListBox.ForeColor = TtText;
            _ttSourceListBox.BorderStyle = BorderStyle.FixedSingle;
            _ttSourceListBox.Font = new Font("Segoe UI", 8f);
            _ttSourceListBox.Location = new Point(8, 60);
            _ttSourceListBox.Size = new Size(248, 210);
            _timingPage.Controls.Add(_ttSourceListBox);

            _ttRemoveBtn = TtMakeButton("Remove", 8, 276, 76);
            _ttRemoveBtn.Click += delegate { TtRemoveSelected(); };
            _timingPage.Controls.Add(_ttRemoveBtn);

            _ttClearBtn = TtMakeButton("Clear", 90, 276, 76);
            _ttClearBtn.Click += delegate { TtClearSources(); };
            _timingPage.Controls.Add(_ttClearBtn);

            _timingPage.Controls.Add(TtMakeLabel("Add IDs (comma/space separated):", 8, 308, 250));
            _ttAddIdsBox = new TextBox();
            _ttAddIdsBox.Multiline = true;
            _ttAddIdsBox.BackColor = TtInputBg;
            _ttAddIdsBox.ForeColor = TtText;
            _ttAddIdsBox.BorderStyle = BorderStyle.FixedSingle;
            _ttAddIdsBox.Font = new Font("Segoe UI", 8f);
            _ttAddIdsBox.Location = new Point(8, 326);
            _ttAddIdsBox.Size = new Size(180, 48);
            _timingPage.Controls.Add(_ttAddIdsBox);

            _ttAddIdsBtn = TtMakeButton("Add IDs", 194, 326, 62);
            _ttAddIdsBtn.Click += delegate { TtAddTypedIds(); };
            _timingPage.Controls.Add(_ttAddIdsBtn);

            _ttAddMapBtn = TtMakeButton("Add Selected From Map", 8, 380, 180);
            _ttAddMapBtn.Click += delegate { TtAddFromMap(); };
            _timingPage.Controls.Add(_ttAddMapBtn);

            // ---- Search settings (middle column) ----
            int mx = 290;
            _timingPage.Controls.Add(TtMakeLabel("Min Normal Attacks:", mx, 12, 130));
            _ttMinNormalInput = TtMakeNumeric(mx + 134, 10, 0, 50, 0, 60);
            _timingPage.Controls.Add(_ttMinNormalInput);

            _timingPage.Controls.Add(TtMakeLabel("Min Captain Attacks:", mx, 42, 130));
            _ttMinCaptainInput = TtMakeNumeric(mx + 134, 40, 0, 50, 0, 60);
            _timingPage.Controls.Add(_ttMinCaptainInput);

            _timingPage.Controls.Add(TtMakeLabel("Cards to use:", mx, 72, 130));
            _ttCardChecks = new Dictionary<int, CheckBox>();
            int cx = mx;
            // Display order x2,x3,x4,x5,x6 -> cardType ints 1,5,2,6,3.
            int[] cardTypes = new int[] { 1, 5, 2, 6, 3 };
            string[] cardLabels = new string[] { "x2", "x3", "x4", "x5", "x6" };
            for (int i = 0; i < cardTypes.Length; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Text = cardLabels[i];
                cb.ForeColor = TtText;
                cb.FlatStyle = FlatStyle.Flat;
                cb.Font = new Font("Segoe UI", 8f);
                cb.Location = new Point(cx, 92);
                cb.Size = new Size(46, 20);
                cb.CheckedChanged += delegate { TtWriteToSettings(); };
                _ttCardChecks[cardTypes[i]] = cb;
                _timingPage.Controls.Add(cb);
                cx += 48;
            }

            _timingPage.Controls.Add(TtMakeLabel("Max Window (s):", mx, 122, 130));
            _ttMaxWindowInput = TtMakeNumeric(mx + 134, 120, 1, 86400, 60, 70);
            _timingPage.Controls.Add(_ttMaxWindowInput);

            _ttVassalSendingCheck = new CheckBox();
            _ttVassalSendingCheck.Text = "Vassal Sending (allow 1 captain + 1 normal, or 2 normal, per village)";
            _ttVassalSendingCheck.ForeColor = TtText;
            _ttVassalSendingCheck.FlatStyle = FlatStyle.Flat;
            _ttVassalSendingCheck.Font = new Font("Segoe UI", 8f);
            _ttVassalSendingCheck.Location = new Point(mx, 152);
            _ttVassalSendingCheck.Size = new Size(440, 20);
            _ttVassalSendingCheck.CheckedChanged += delegate { TtWriteToSettings(); };
            _timingPage.Controls.Add(_ttVassalSendingCheck);

            _ttSearchBtn = TtMakeButton("Search", mx, 182, 120);
            _ttSearchBtn.Height = 28;
            _ttSearchBtn.Click += delegate { TtSearch(); };
            _timingPage.Controls.Add(_ttSearchBtn);

            _ttStatusLabel = TtMakeLabel("", mx, 218, 500);
            _ttStatusLabel.ForeColor = Color.FromArgb(220, 180, 60);
            _timingPage.Controls.Add(_ttStatusLabel);

            // ---- Results browser ----
            _ttResultIndexLabel = TtMakeLabel("No results", mx, 244, 500);
            _ttResultIndexLabel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            _timingPage.Controls.Add(_ttResultIndexLabel);

            _ttPrevBtn = TtMakeButton("◀ Prev", mx, 268, 76);
            _ttPrevBtn.Click += delegate { TtStep(-1); };
            _timingPage.Controls.Add(_ttPrevBtn);

            _ttNextBtn = TtMakeButton("Next ▶", mx + 82, 268, 76);
            _ttNextBtn.Click += delegate { TtStep(1); };
            _timingPage.Controls.Add(_ttNextBtn);

            _ttPushBtn = TtMakeButton("Push to Auto Bomb Multi", mx + 170, 268, 200);
            _ttPushBtn.Click += delegate { TtPushToAbm(); };
            _timingPage.Controls.Add(_ttPushBtn);

            _ttResultPanel = new FlowLayoutPanel();
            _ttResultPanel.FlowDirection = FlowDirection.TopDown;
            _ttResultPanel.WrapContents = false;
            _ttResultPanel.AutoScroll = true;
            _ttResultPanel.BackColor = TtInputBg;
            _ttResultPanel.BorderStyle = BorderStyle.FixedSingle;
            _ttResultPanel.Location = new Point(mx, 300);
            _ttResultPanel.Size = new Size(560, 180);
            _timingPage.Controls.Add(_ttResultPanel);

            // Persistence wiring.
            _ttTargetVidBox.TextChanged += delegate { TtWriteToSettings(); };
            _ttMinNormalInput.ValueChanged += delegate { TtWriteToSettings(); };
            _ttMinCaptainInput.ValueChanged += delegate { TtWriteToSettings(); };
            _ttMaxWindowInput.ValueChanged += delegate { TtWriteToSettings(); };

            _tabControl.Controls.Add(_timingPage);
            TtUpdateResultButtons();
        }

        // =====================================================================
        // Styled control helpers
        // =====================================================================

        private Label TtMakeLabel(string text, int x, int y, int w)
        {
            Label l = new Label();
            l.Text = text;
            l.ForeColor = TtText;
            l.Font = new Font("Segoe UI", 8f);
            l.Location = new Point(x, y);
            l.Size = new Size(w, 18);
            l.AutoSize = false;
            return l;
        }

        private TextBox TtMakeTextBox(int x, int y, int w)
        {
            TextBox t = new TextBox();
            t.BackColor = TtInputBg;
            t.ForeColor = TtText;
            t.BorderStyle = BorderStyle.FixedSingle;
            t.Font = new Font("Segoe UI", 8f);
            t.Location = new Point(x, y);
            t.Size = new Size(w, 20);
            return t;
        }

        private Button TtMakeButton(string text, int x, int y, int w)
        {
            Button b = new Button();
            b.Text = text;
            b.BackColor = TtBtnBg;
            b.ForeColor = TtText;
            b.FlatStyle = FlatStyle.Flat;
            b.Font = new Font("Segoe UI", 8f);
            b.Location = new Point(x, y);
            b.Size = new Size(w, 24);
            return b;
        }

        private NumericUpDown TtMakeNumeric(int x, int y, int min, int max, int value, int w)
        {
            NumericUpDown nud = new NumericUpDown();
            nud.BackColor = TtInputBg;
            nud.ForeColor = TtText;
            nud.BorderStyle = BorderStyle.FixedSingle;
            nud.Font = new Font("Segoe UI", 8f);
            nud.Location = new Point(x, y);
            nud.Size = new Size(w, 20);
            nud.Minimum = min;
            nud.Maximum = max;
            nud.Value = Math.Max(min, Math.Min(max, value));
            return nud;
        }

        // =====================================================================
        // Source list management
        // =====================================================================

        private void TtAddSource(int vid)
        {
            TimingToolSettings s = TtSettings;
            if (s == null || vid <= 0) return;
            if (s.SourceVillages.Contains(vid)) return;
            s.SourceVillages.Add(vid);
        }

        private void TtAddTypedIds()
        {
            TimingToolSettings s = TtSettings;
            if (s == null) return;
            string raw = _ttAddIdsBox.Text;
            if (string.IsNullOrEmpty(raw)) return;
            string[] parts = raw.Split(new char[] { ',', ' ', '\t', '\r', '\n', ';' },
                StringSplitOptions.RemoveEmptyEntries);
            foreach (string p in parts)
            {
                int vid;
                if (int.TryParse(p.Trim(), out vid)) TtAddSource(vid);
            }
            _ttAddIdsBox.Text = "";
            TtRefreshSourceList();
            SaveSettingsSafe();
        }

        private void TtAddFromMap()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
            int vid = GameEngine.Instance.World.LastClickedVillage;
            if (vid <= 0) return;
            TtAddSource(vid);
            TtRefreshSourceList();
            SaveSettingsSafe();
        }

        private void TtRemoveSelected()
        {
            TimingToolSettings s = TtSettings;
            if (s == null) return;
            int idx = _ttSourceListBox.SelectedIndex;
            if (idx < 0 || idx >= s.SourceVillages.Count) return;
            s.SourceVillages.RemoveAt(idx);
            TtRefreshSourceList();
            SaveSettingsSafe();
        }

        private void TtClearSources()
        {
            TimingToolSettings s = TtSettings;
            if (s == null) return;
            s.SourceVillages.Clear();
            TtRefreshSourceList();
            SaveSettingsSafe();
        }

        private void TtRefreshSourceList()
        {
            TimingToolSettings s = TtSettings;
            _ttSourceListBox.BeginUpdate();
            _ttSourceListBox.Items.Clear();
            if (s != null)
            {
                foreach (int vid in s.SourceVillages)
                    _ttSourceListBox.Items.Add(vid + " — " + TtVillageName(vid));
            }
            _ttSourceListBox.EndUpdate();
        }

        private string TtVillageName(int vid)
        {
            try
            {
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                {
                    string name = GameEngine.Instance.World.getVillageName(vid);
                    if (!string.IsNullOrEmpty(name)) return name;
                }
            }
            catch { }
            return "?";
        }

        // =====================================================================
        // Settings load / write
        // =====================================================================

        private void TtLoadFromSettings()
        {
            TimingToolSettings s = TtSettings;
            if (s == null) return;
            _ttLoading = true;
            try
            {
                _ttTargetVidBox.Text = s.TargetVillageId > 0 ? s.TargetVillageId.ToString() : "";
                _ttMinNormalInput.Value = Clamp(s.MinNormalAttacks, _ttMinNormalInput);
                _ttMinCaptainInput.Value = Clamp(s.MinCaptainAttacks, _ttMinCaptainInput);
                _ttMaxWindowInput.Value = Clamp(s.MaxAttackWindowSeconds, _ttMaxWindowInput);
                _ttVassalSendingCheck.Checked = s.VassalSending;
                foreach (KeyValuePair<int, CheckBox> kv in _ttCardChecks)
                    kv.Value.Checked = s.AllowedCards.Contains(kv.Key);
                TtRefreshSourceList();
            }
            finally { _ttLoading = false; }
        }

        private decimal Clamp(int value, NumericUpDown nud)
        {
            return Math.Max(nud.Minimum, Math.Min(nud.Maximum, value));
        }

        private void TtWriteToSettings()
        {
            if (_ttLoading) return;
            TimingToolSettings s = TtSettings;
            if (s == null) return;

            int vid;
            s.TargetVillageId = int.TryParse(_ttTargetVidBox.Text.Trim(), out vid) ? vid : 0;
            s.MinNormalAttacks = (int)_ttMinNormalInput.Value;
            s.MinCaptainAttacks = (int)_ttMinCaptainInput.Value;
            s.MaxAttackWindowSeconds = (int)_ttMaxWindowInput.Value;
            s.VassalSending = _ttVassalSendingCheck.Checked;
            s.AllowedCards.Clear();
            foreach (KeyValuePair<int, CheckBox> kv in _ttCardChecks)
                if (kv.Value.Checked) s.AllowedCards.Add(kv.Key);

            SaveSettingsSafe();
        }

        private void SaveSettingsSafe()
        {
            try
            {
                if (BotEngine.Instance != null && BotEngine.Instance.Settings != null)
                    BotEngine.Instance.Settings.Save();
            }
            catch { }
        }

        // =====================================================================
        // Search
        // =====================================================================

        private void TtSearch()
        {
            _ttResults = new List<TimingSearch.TtSetup>();
            _ttResultIdx = 0;

            TimingToolSettings s = TtSettings;
            if (s == null) { TtSetStatus("Settings unavailable."); TtRenderResult(); return; }

            int target;
            if (!int.TryParse(_ttTargetVidBox.Text.Trim(), out target) || target <= 0)
            { TtSetStatus("Enter a valid target village ID."); TtRenderResult(); return; }

            if (s.SourceVillages.Count == 0)
            { TtSetStatus("Add at least one source village."); TtRenderResult(); return; }

            int minCap = (int)_ttMinCaptainInput.Value;
            int minNorm = (int)_ttMinNormalInput.Value;
            if (minCap + minNorm <= 0)
            { TtSetStatus("Set at least one required attack (normal or captain)."); TtRenderResult(); return; }

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
            { TtSetStatus("Game world not loaded."); TtRenderResult(); return; }

            // Allowed cards (from settings) plus 0 (no card) always.
            List<int> allowed = new List<int>();
            allowed.Add(0);
            foreach (int c in s.AllowedCards) if (!allowed.Contains(c)) allowed.Add(c);

            // Build candidates.
            List<TimingSearch.Candidate> candidates = new List<TimingSearch.Candidate>();
            foreach (int v in s.SourceVillages)
            {
                double baseN, baseC;
                try
                {
                    baseN = AutoBombModule.CalculateBaseTravelTimeMaxResearch(v, target, false);
                    baseC = AutoBombModule.CalculateBaseTravelTimeMaxResearch(v, target, true);
                }
                catch { continue; }

                for (int type = 0; type < 2; type++)
                {
                    bool isCaptain = (type == 1);
                    double bse = isCaptain ? baseC : baseN;
                    if (bse <= 0) continue;
                    foreach (int c in allowed)
                    {
                        double t = AutoBombModule.ApplyCardSpeed(bse, c);
                        candidates.Add(new TimingSearch.Candidate(v, isCaptain, c, t));
                    }
                }
            }

            if (candidates.Count == 0)
            { TtSetStatus("Could not resolve travel times for the source villages."); TtRenderResult(); return; }

            double maxWindow = (double)_ttMaxWindowInput.Value;
            bool vassalOn = _ttVassalSendingCheck.Checked;

            _ttResults = TimingSearch.FindSetups(candidates, minCap, minNorm, maxWindow, vassalOn);

            if (_ttResults.Count == 0)
                TtSetStatus("No setup meets the requirements within the " + maxWindow + "s window. "
                    + "Try a larger window, more cards, fewer required attacks, or more source villages.");
            else
                TtSetStatus(_ttResults.Count + " setup(s) found.");

            TtRenderResult();
        }

        private void TtSetStatus(string text)
        {
            if (_ttStatusLabel != null) _ttStatusLabel.Text = text;
        }

        private void TtStep(int delta)
        {
            if (_ttResults.Count == 0) return;
            _ttResultIdx += delta;
            if (_ttResultIdx < 0) _ttResultIdx = 0;
            if (_ttResultIdx >= _ttResults.Count) _ttResultIdx = _ttResults.Count - 1;
            TtRenderResult();
        }

        private void TtRenderResult()
        {
            _ttResultPanel.SuspendLayout();
            _ttResultPanel.Controls.Clear();

            if (_ttResults.Count == 0)
            {
                _ttResultIndexLabel.Text = "No results";
            }
            else
            {
                TimingSearch.TtSetup setup = _ttResults[_ttResultIdx];
                _ttResultIndexLabel.Text = "Setup " + (_ttResultIdx + 1) + " / " + _ttResults.Count
                    + "  —  window " + AutoBombModule.FormatTimeSpan(TimeSpan.FromSeconds(setup.Window))
                    + " (" + setup.Window.ToString("0") + "s)";

                int capCount = 0, normCount = 0;
                foreach (TimingSearch.TtAttack a in setup.Attacks)
                    if (a.IsCaptain) capCount++; else normCount++;

                _ttResultPanel.Controls.Add(TtResultLine(
                    capCount + " captain + " + normCount + " normal attack(s):", true));

                foreach (TimingSearch.TtAttack a in setup.Attacks)
                {
                    string line = "  [" + a.VillageId + "] " + TtVillageName(a.VillageId)
                        + "   —   " + (a.IsCaptain ? "Captain" : "Normal")
                        + "   —   " + TtCardLabel(a.CardType)
                        + "   —   " + AutoBombModule.FormatTimeSpan(TimeSpan.FromSeconds(a.Time));
                    _ttResultPanel.Controls.Add(TtResultLine(line, false));
                }
            }

            _ttResultPanel.ResumeLayout();
            TtUpdateResultButtons();
        }

        private Label TtResultLine(string text, bool bold)
        {
            Label l = new Label();
            l.Text = text;
            l.ForeColor = bold ? Color.FromArgb(100, 180, 255) : TtText;
            l.Font = new Font("Consolas", 8.5f, bold ? FontStyle.Bold : FontStyle.Regular);
            l.AutoSize = true;
            l.Margin = new Padding(2, 1, 2, 1);
            return l;
        }

        private string TtCardLabel(int cardType)
        {
            switch (cardType)
            {
                case 1: return "x2";
                case 2: return "x4";
                case 3: return "x6";
                case 5: return "x3";
                case 6: return "x5";
                default: return "No card";
            }
        }

        private void TtUpdateResultButtons()
        {
            bool any = _ttResults.Count > 0;
            _ttPrevBtn.Enabled = any && _ttResultIdx > 0;
            _ttNextBtn.Enabled = any && _ttResultIdx < _ttResults.Count - 1;
            _ttPushBtn.Enabled = any;
        }

        // =====================================================================
        // Push to Auto Bomb Multi
        // =====================================================================

        private void TtPushToAbm()
        {
            if (_ttResults.Count == 0) return;
            TimingSearch.TtSetup setup = _ttResults[_ttResultIdx];

            AutoBombMultiSettings abm = AbmSettings;
            if (abm == null) { TtSetStatus("Auto Bomb Multi settings unavailable."); return; }

            int target;
            if (!int.TryParse(_ttTargetVidBox.Text.Trim(), out target) || target <= 0) return;

            // Switch to the ABM tab and set its target so its rows recalc against the same target.
            if (_bombMultiPage != null) _tabControl.SelectedTab = _bombMultiPage;
            if (_abmTargetVidBox != null) _abmTargetVidBox.Text = target.ToString();

            string localPlayer = AutoBombMultiModule.GetLocalPlayerName();
            MultiPlayerInfo owner = new MultiPlayerInfo();
            owner.PlayerName = localPlayer;

            // A village can appear once as a "player" entry and once as a "vassal" entry in ABM
            // (deduped by village + IsVassal). For a second attack from the same village (vassal
            // sending), we add it as the vassal entry. Track which slot each village has used.
            HashSet<int> usedPlayerSlot = new HashSet<int>();

            int added = 0;
            foreach (TimingSearch.TtAttack a in setup.Attacks)
            {
                bool asVassal = usedPlayerSlot.Contains(a.VillageId);
                usedPlayerSlot.Add(a.VillageId);

                MultiVillageInfo vi = TtBuildLocalVillageInfo(a.VillageId, target, asVassal);
                AbmAddVillageRow(abm, owner, vi, abm.IsCoordinator);

                // The row we just added is the last one in the list — configure it.
                if (_abmVillageRows.Count > 0)
                {
                    MultiBombVillageRow row = _abmVillageRows[_abmVillageRows.Count - 1];
                    // cardIndex == cardType int (ABM combo order matches), attackType 0 = Vandalise.
                    row.ApplyConfig("", a.CardType, a.IsCaptain, 1, 0);
                    row.Selected = true;
                    AbmRecalcRowTravel(row);
                }
                added++;
            }

            AbmRepositionVillageRows();
            AbmSaveSetup();
            TtSetStatus("Pushed " + added + " attack(s) to Auto Bomb Multi.");
        }

        private MultiVillageInfo TtBuildLocalVillageInfo(int villageId, int target, bool asVassal)
        {
            MultiVillageInfo vi = new MultiVillageInfo();
            vi.VillageId = villageId;
            vi.VillageName = TtVillageName(villageId);
            vi.IsVassal = asVassal;
            vi.ParentVillageId = asVassal ? villageId : 0;
            try
            {
                vi.TravelTimeArmy = AutoBombModule.CalculateBaseTravelTimeMaxResearch(villageId, target, false);
                vi.TravelTimeCaptain = AutoBombModule.CalculateBaseTravelTimeMaxResearch(villageId, target, true);
            }
            catch { }
            return vi;
        }
    }
}
