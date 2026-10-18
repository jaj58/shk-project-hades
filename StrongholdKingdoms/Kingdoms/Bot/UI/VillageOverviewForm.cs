using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Village Overview - every one of the player's villages in one sortable, filterable
    /// grid. A bot version of the game's premium-token overview that works without a
    /// token, shows a lot more, and keeps refreshing while it's open.
    ///
    /// Opened from the bot button left of the premium overview button on the top menu bar
    /// (MainMenuBar2), or from the bot window's footer. Data comes from
    /// VillageOverviewCache; this class only lays it out.
    ///
    /// Everything runs on the game's UI thread, inside the main loop's DoEvents, so each
    /// timer tick does a bounded slice of work: at most RowsPerTick rows rebuilt and at most
    /// one request sent. Painting is the expensive part - a DataGridView takes the best part
    /// of 100ms to repaint a screenful even double-buffered - so the grid is never
    /// invalidated wholesale on a timer. Once a second the on-screen cells are compared
    /// with what was last painted and only the ones whose text or colour changed are
    /// repainted; full repaints only follow the user sorting, filtering or scrolling, or
    /// the set of rows actually changing.
    /// </summary>
    internal class VillageOverviewForm : MyFormBase
    {
        private static readonly Color FormBg = Color.FromArgb(28, 30, 38);
        private static readonly Color StripBg = Color.FromArgb(36, 38, 50);
        private static readonly Color ListBg = Color.FromArgb(24, 24, 32);
        private static readonly Color RowBg = Color.FromArgb(30, 32, 40);
        private static readonly Color RowAltBg = Color.FromArgb(36, 38, 48);
        private static readonly Color TotalsBg = Color.FromArgb(44, 48, 64);
        private static readonly Color SelectBg = Color.FromArgb(60, 64, 84);
        private static readonly Color GridLine = Color.FromArgb(55, 58, 72);
        private static readonly Color ButtonBg = Color.FromArgb(50, 52, 64);
        private static readonly Color ToggleOnBg = Color.FromArgb(52, 84, 132);
        private static readonly Color ButtonBorder = Color.FromArgb(70, 74, 90);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);

        private const int TitleBarHeight = 32;
        private const int FrameInset = 5;      // leaves MyFormBase's resize border clickable
        private const int FooterHeight = 22;

        private const int TickMs = 250;
        private const int RowsPerTick = 25;
        private const int WorldPassTicks = 8;  // every 2 s
        private const int SecondTicks = 4;
        private const int MaxOwnInFlight = 2;  // the router allows 4 in total; leave room
        private const double SlowRebuildMs = 15.0;

        private static VillageOverviewForm _instance;

        private Panel _body;
        private FlowLayoutPanel _headerStrip;
        private Panel _footerStrip;
        private Label _footerLabel;
        private readonly CheckBox[] _groupToggles = new CheckBox[5];
        private TextBox _filterBox;
        private ComboBox _typeBox;
        private CheckBox _incomingOnly;
        private CheckBox _downloadedOnly;
        private NumericUpDown _autoMinutes;
        private Button _refreshButton;
        private Button _copyButton;
        private OverviewGrid _grid;
        private ContextMenuStrip _rowMenu;
        private ContextMenuStrip _headerMenu;
        private ToolStripMenuItem _redownloadItem;
        private Timer _timer;
        private Font _boldFont;

        private readonly List<OverviewColumn> _columns = VillageOverviewColumns.All;
        private readonly Dictionary<string, bool> _hidden = new Dictionary<string, bool>();
        private readonly bool[] _groupOn = new bool[5];

        private readonly List<VillageOverviewRow> _rows = new List<VillageOverviewRow>();
        private readonly Dictionary<int, VillageOverviewRow> _rowsById = new Dictionary<int, VillageOverviewRow>();
        private readonly Dictionary<int, bool> _capitals = new Dictionary<int, bool>();
        private List<int> _villageIds = new List<int>();
        private List<VillageOverviewRow> _view = new List<VillageOverviewRow>();
        private readonly Dictionary<int, int> _viewIndex = new Dictionary<int, int>();
        private double[] _totals = new double[0];

        // What each on-screen cell was last painted with, keyed by village id (-1 for the
        // totals row), so the once-a-second pass can repaint only cells that changed.
        private class ShownRow
        {
            public string[] Text;
            public int[] Colour;
        }
        private readonly Dictionary<int, ShownRow> _shown = new Dictionary<int, ShownRow>();
        private const int TotalsKey = -1;

        private VillageOverviewCache.WorldPass _pass;
        private readonly Queue<int> _rebuildQueue = new Queue<int>();
        private readonly Dictionary<int, bool> _queued = new Dictionary<int, bool>();
        private int _tick;
        private DateTime _now = DateTime.MinValue;
        private bool _resortWhenDrained = true;
        private bool _sweepDone = true;
        private DateTime _forcedSweepFrom = DateTime.MinValue;
        private DateTime _lastSlowLog = DateTime.MinValue;
        private int _menuVillageId = -1;
        private bool _testMode;
        private bool _loading;

        private string _sortKey = "name";
        private bool _sortDesc;

        /// <summary>Opens the window, or brings it to the front if it's already up.</summary>
        public static void ShowInstance()
        {
            try
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new VillageOverviewForm();

                if (!_instance.Visible)
                {
                    Form parent = InterfaceMgr.Instance != null ? InterfaceMgr.Instance.ParentForm : null;
                    if (parent != null)
                        _instance.Show(parent);
                    else
                        _instance.Show();
                }

                if (_instance.WindowState == FormWindowState.Minimized)
                    _instance.WindowState = FormWindowState.Normal;
                _instance.BringToFront();
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageOverviewForm.ShowInstance failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Closes the window if it is open. Called on a world reinit, where every village id
        /// on screen is about to become meaningless.
        /// </summary>
        public static void CloseInstance()
        {
            if (_instance == null || _instance.IsDisposed) return;
            try { _instance.Close(); }
            catch (Exception) { }
            _instance = null;
        }

        private VillageOverviewForm()
        {
            this.ShowClose = true;
            this.Resizable = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.DoubleBuffered = true;
            this.BackColor = FormBg;
            this.MinimumSize = new Size(640, 320);
            this.ClientSize = new Size(1150, 620);
            this.Title = "Village Overview";
            this.setGradient(FormBg, Color.FromArgb(20, 20, 28));

            _boldFont = new Font("Segoe UI", 8.25f, FontStyle.Bold);

            BuildUI();
            LoadSettings();
            ApplyColumnVisibility();
            ApplyView(true);

            _timer = new Timer();
            _timer.Interval = TickMs;
            _timer.Tick += new EventHandler(this.TimerTick);
            _timer.Start();
        }

        // =================================================================
        // Layout
        // =================================================================

        private void BuildUI()
        {
            this.SuspendLayout();

            _body = new Panel();
            _body.BackColor = FormBg;
            _body.Location = new Point(FrameInset, TitleBarHeight);
            _body.Size = new Size(this.ClientSize.Width - (FrameInset * 2),
                                  this.ClientSize.Height - TitleBarHeight - FrameInset);
            _body.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(_body);

            _footerStrip = new Panel();
            _footerStrip.Dock = DockStyle.Bottom;
            _footerStrip.Height = FooterHeight;
            _footerStrip.BackColor = StripBg;

            _footerLabel = new Label();
            _footerLabel.AutoSize = false;
            _footerLabel.Dock = DockStyle.Fill;
            _footerLabel.Font = new Font("Segoe UI", 7.5f);
            _footerLabel.ForeColor = VillageOverviewColumns.TextDim;
            _footerLabel.TextAlign = ContentAlignment.MiddleLeft;
            _footerLabel.Padding = new Padding(8, 0, 8, 0);
            _footerStrip.Controls.Add(_footerLabel);
            _body.Controls.Add(_footerStrip);

            // Wraps onto a second line when the window is narrow rather than clipping.
            _headerStrip = new FlowLayoutPanel();
            _headerStrip.Dock = DockStyle.Top;
            _headerStrip.AutoSize = true;
            _headerStrip.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _headerStrip.WrapContents = true;
            _headerStrip.BackColor = StripBg;
            _headerStrip.Padding = new Padding(4, 3, 4, 3);

            for (int g = 1; g <= 4; g++)
            {
                CheckBox toggle = MakeToggle(VillageOverviewColumns.GroupName((OverviewGroup)g));
                toggle.Tag = g;
                toggle.CheckedChanged += new EventHandler(this.GroupToggleChanged);
                _groupToggles[g] = toggle;
                _headerStrip.Controls.Add(toggle);
            }

            _headerStrip.Controls.Add(MakeLabel("Find", 10));
            _filterBox = new TextBox();
            _filterBox.Width = 120;
            _filterBox.Font = new Font("Segoe UI", 8.25f);
            _filterBox.BackColor = RowBg;
            _filterBox.ForeColor = VillageOverviewColumns.TextPri;
            _filterBox.BorderStyle = BorderStyle.FixedSingle;
            _filterBox.Margin = new Padding(3, 4, 3, 3);
            _filterBox.TextChanged += new EventHandler(this.FilterChanged);
            _headerStrip.Controls.Add(_filterBox);

            _typeBox = new ComboBox();
            _typeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _typeBox.FlatStyle = FlatStyle.Flat;
            _typeBox.Font = new Font("Segoe UI", 8.25f);
            _typeBox.BackColor = RowBg;
            _typeBox.ForeColor = VillageOverviewColumns.TextPri;
            _typeBox.Width = 84;
            _typeBox.Margin = new Padding(3, 4, 3, 3);
            _typeBox.Items.Add("Villages");
            _typeBox.Items.Add("Capitals");
            _typeBox.Items.Add("All");
            _typeBox.SelectedIndex = 0;
            _typeBox.SelectedIndexChanged += new EventHandler(this.FilterChanged);
            _headerStrip.Controls.Add(_typeBox);

            _incomingOnly = MakeCheck("Under attack");
            _incomingOnly.CheckedChanged += new EventHandler(this.FilterChanged);
            _headerStrip.Controls.Add(_incomingOnly);

            _downloadedOnly = MakeCheck("Downloaded");
            _downloadedOnly.CheckedChanged += new EventHandler(this.FilterChanged);
            _headerStrip.Controls.Add(_downloadedOnly);

            _headerStrip.Controls.Add(MakeLabel("Auto-refresh (min)", 10));
            _autoMinutes = new NumericUpDown();
            // Range before value - setting a value outside the default 0-100 first would
            // clamp it (the trap that once wiped the Trade settings).
            _autoMinutes.Minimum = 0;
            _autoMinutes.Maximum = 60;
            _autoMinutes.Value = 5;
            _autoMinutes.Width = 44;
            _autoMinutes.Font = new Font("Segoe UI", 8.25f);
            _autoMinutes.BackColor = RowBg;
            _autoMinutes.ForeColor = VillageOverviewColumns.TextPri;
            _autoMinutes.BorderStyle = BorderStyle.FixedSingle;
            _autoMinutes.Margin = new Padding(3, 4, 3, 3);
            _headerStrip.Controls.Add(_autoMinutes);

            _refreshButton = MakeButton("Refresh all", 80);
            _refreshButton.Click += new EventHandler(this.RefreshClick);
            _headerStrip.Controls.Add(_refreshButton);

            _copyButton = MakeButton("Copy table", 76);
            _copyButton.Click += new EventHandler(this.CopyClick);
            _headerStrip.Controls.Add(_copyButton);

            _body.Controls.Add(_headerStrip);

            BuildGrid();
            BuildMenus();

            // Docking is applied from the back of the z-order forwards, so the Fill grid
            // has to sit in front of the two strips or it would claim the whole body.
            _body.Controls.Add(_grid);
            _grid.BringToFront();

            this.ResumeLayout(false);
        }

        private void BuildGrid()
        {
            _grid = new OverviewGrid();
            _grid.Dock = DockStyle.Fill;
            _grid.VirtualMode = true;
            _grid.ReadOnly = true;
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.AllowUserToResizeRows = false;
            _grid.AllowUserToOrderColumns = true;
            _grid.MultiSelect = false;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.RowHeadersVisible = false;
            _grid.BorderStyle = BorderStyle.None;
            _grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            _grid.BackgroundColor = ListBg;
            _grid.GridColor = GridLine;
            _grid.EnableHeadersVisualStyles = false;
            _grid.ShowCellToolTips = true;
            _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _grid.ColumnHeadersHeight = 26;
            _grid.RowTemplate.Height = 20;
            _grid.Font = new Font("Segoe UI", 8.25f);

            _grid.DefaultCellStyle.BackColor = RowBg;
            _grid.DefaultCellStyle.ForeColor = VillageOverviewColumns.TextPri;
            _grid.DefaultCellStyle.SelectionBackColor = SelectBg;
            _grid.DefaultCellStyle.SelectionForeColor = VillageOverviewColumns.TextPri;
            _grid.AlternatingRowsDefaultCellStyle.BackColor = RowAltBg;
            _grid.ColumnHeadersDefaultCellStyle.BackColor = StripBg;
            _grid.ColumnHeadersDefaultCellStyle.ForeColor = TextSec;
            _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = StripBg;
            _grid.ColumnHeadersDefaultCellStyle.Font = _boldFont;
            _grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            for (int i = 0; i < _columns.Count; i++)
            {
                OverviewColumn def = _columns[i];
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = def.Key;
                col.HeaderText = def.Header;
                col.ToolTipText = def.HeaderTip ?? "";
                col.Width = def.Width;
                col.MinimumWidth = 28;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
                col.DefaultCellStyle.Alignment = def.LeftAlign
                    ? DataGridViewContentAlignment.MiddleLeft
                    : DataGridViewContentAlignment.MiddleRight;
                _grid.Columns.Add(col);
            }
            _grid.Columns[0].Frozen = true;

            _grid.CellValueNeeded += new DataGridViewCellValueEventHandler(this.GridCellValueNeeded);
            _grid.CellFormatting += new DataGridViewCellFormattingEventHandler(this.GridCellFormatting);
            _grid.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(this.GridCellToolTipTextNeeded);
            _grid.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.GridColumnHeaderMouseClick);
            _grid.CellMouseDown += new DataGridViewCellMouseEventHandler(this.GridCellMouseDown);
            _grid.CellDoubleClick += new DataGridViewCellEventHandler(this.GridCellDoubleClick);
        }

        private void BuildMenus()
        {
            _rowMenu = new ContextMenuStrip();
            _rowMenu.Items.Add("Show on map", null, delegate { ShowOnMap(_menuVillageId); });
            _rowMenu.Items.Add("Open castle", null, delegate { OpenCastle(_menuVillageId); });
            _rowMenu.Items.Add("Village Info window", null, delegate { VillageStatsForm.ShowFor(_menuVillageId); });
            _rowMenu.Items.Add("Village Radar window", null, delegate { VillageRadarForm.ShowFor(_menuVillageId); });
            _rowMenu.Items.Add(new ToolStripSeparator());
            _rowMenu.Items.Add("Refresh this village", null, delegate { RefreshVillage(_menuVillageId); });
            _redownloadItem = new ToolStripMenuItem("Full re-download (Village Sync)", null,
                delegate { RequestRedownload(_menuVillageId); });
            _redownloadItem.ToolTipText = "Queued in Village Sync, which switches to the village to download it. Runs while the bot is on.";
            _rowMenu.Items.Add(_redownloadItem);
            _rowMenu.Items.Add(new ToolStripSeparator());
            _rowMenu.Items.Add("Copy village ID", null, delegate { CopyText(_menuVillageId.ToString()); });

            _headerMenu = new ContextMenuStrip();
        }

        private CheckBox MakeToggle(string text)
        {
            CheckBox toggle = new CheckBox();
            toggle.Appearance = Appearance.Button;
            toggle.Text = text;
            toggle.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            toggle.FlatStyle = FlatStyle.Flat;
            toggle.FlatAppearance.BorderColor = ButtonBorder;
            toggle.FlatAppearance.CheckedBackColor = ToggleOnBg;
            toggle.BackColor = ButtonBg;
            toggle.ForeColor = VillageOverviewColumns.TextPri;
            toggle.Margin = new Padding(3, 3, 3, 3);
            toggle.TextAlign = ContentAlignment.MiddleCenter;
            toggle.UseMnemonic = false;   // "Units & incoming" - keep the ampersand
            // AutoSize under-measures a flat button-style checkbox and clips the text, so
            // size it from the text instead.
            toggle.AutoSize = false;
            toggle.Size = new Size(TextRenderer.MeasureText(text, toggle.Font).Width + 18, 24);
            return toggle;
        }

        private CheckBox MakeCheck(string text)
        {
            CheckBox check = new CheckBox();
            check.Text = text;
            check.AutoSize = true;
            check.Font = new Font("Segoe UI", 8f);
            check.ForeColor = VillageOverviewColumns.TextPri;
            check.Margin = new Padding(6, 6, 3, 3);
            return check;
        }

        private Label MakeLabel(string text, int leftGap)
        {
            Label label = new Label();
            label.Text = text;
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 8f);
            label.ForeColor = TextSec;
            label.Margin = new Padding(leftGap, 8, 0, 3);
            return label;
        }

        private Button MakeButton(string text, int width)
        {
            Button button = new Button();
            button.Text = text;
            button.Font = new Font("Segoe UI", 8f);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = ButtonBorder;
            button.BackColor = ButtonBg;
            button.ForeColor = VillageOverviewColumns.TextPri;
            button.Size = new Size(width, 24);
            button.Margin = new Padding(6, 3, 3, 3);
            return button;
        }

        // =================================================================
        // Settings
        // =================================================================

        private static VillageOverviewSettings Settings
        {
            get
            {
                try
                {
                    if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return null;
                    if (BotEngine.Instance.Settings.VillageOverview == null)
                        BotEngine.Instance.Settings.VillageOverview = new VillageOverviewSettings();
                    return BotEngine.Instance.Settings.VillageOverview;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private void LoadSettings()
        {
            _loading = true;
            try
            {
                VillageOverviewSettings s = Settings ?? new VillageOverviewSettings();

                if (!s.DefaultsApplied)
                {
                    for (int i = 0; i < _columns.Count; i++)
                    {
                        if (_columns[i].HiddenByDefault && !s.HiddenColumns.Contains(_columns[i].Key))
                            s.HiddenColumns.Add(_columns[i].Key);
                    }
                    s.DefaultsApplied = true;
                }

                _hidden.Clear();
                for (int i = 0; i < s.HiddenColumns.Count; i++)
                    _hidden[s.HiddenColumns[i]] = true;

                _groupOn[(int)OverviewGroup.Info] = true;
                _groupOn[(int)OverviewGroup.Resources] = s.ShowResources;
                _groupOn[(int)OverviewGroup.Military] = s.ShowMilitary;
                _groupOn[(int)OverviewGroup.Health] = s.ShowVillage;
                _groupOn[(int)OverviewGroup.Units] = s.ShowUnits;
                for (int g = 1; g <= 4; g++)
                    _groupToggles[g].Checked = _groupOn[g];

                _typeBox.SelectedIndex = Math.Max(0, Math.Min(2, s.TypeFilter));
                _incomingOnly.Checked = s.IncomingOnly;
                _downloadedOnly.Checked = s.DownloadedOnly;
                _autoMinutes.Value = Math.Max((int)_autoMinutes.Minimum, Math.Min((int)_autoMinutes.Maximum, s.AutoRefreshMinutes));

                if (FindColumn(s.SortKey) >= 0) _sortKey = s.SortKey;
                _sortDesc = s.SortDescending;

                if (s.WindowWidth >= this.MinimumSize.Width && s.WindowHeight >= this.MinimumSize.Height)
                {
                    Rectangle bounds = new Rectangle(s.WindowX, s.WindowY, s.WindowWidth, s.WindowHeight);
                    if (IsOnScreen(bounds))
                    {
                        this.StartPosition = FormStartPosition.Manual;
                        this.Bounds = bounds;
                    }
                }
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageOverviewForm: loading settings failed: " + ex.Message);
            }
            finally
            {
                _loading = false;
            }
        }

        private void SaveSettings()
        {
            try
            {
                VillageOverviewSettings s = Settings;
                if (s == null) return;

                s.HiddenColumns = new List<string>(_hidden.Keys);
                s.ShowResources = _groupOn[(int)OverviewGroup.Resources];
                s.ShowMilitary = _groupOn[(int)OverviewGroup.Military];
                s.ShowVillage = _groupOn[(int)OverviewGroup.Health];
                s.ShowUnits = _groupOn[(int)OverviewGroup.Units];
                s.TypeFilter = _typeBox.SelectedIndex;
                s.IncomingOnly = _incomingOnly.Checked;
                s.DownloadedOnly = _downloadedOnly.Checked;
                s.AutoRefreshMinutes = (int)_autoMinutes.Value;
                s.SortKey = _sortKey;
                s.SortDescending = _sortDesc;

                Rectangle bounds = this.WindowState == FormWindowState.Normal ? this.Bounds : this.RestoreBounds;
                s.WindowX = bounds.X;
                s.WindowY = bounds.Y;
                s.WindowWidth = bounds.Width;
                s.WindowHeight = bounds.Height;

                BotEngine.Instance.Settings.Save();
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageOverviewForm: saving settings failed: " + ex.Message);
            }
        }

        private static bool IsOnScreen(Rectangle bounds)
        {
            Rectangle titleBar = new Rectangle(bounds.X, bounds.Y, bounds.Width, TitleBarHeight);
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.IntersectsWith(titleBar)) return true;
            }
            return false;
        }

        // =================================================================
        // Ticking
        // =================================================================

        private void TimerTick(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            try
            {
                _tick++;
                _now = VillageMap.getCurrentServerTime();

                if (!_testMode && GameEngine.Instance != null && GameEngine.Instance.World != null)
                {
                    // Every row is rebuilt every couple of seconds, a slice per tick - that
                    // picks up new replies, armies, traders and live stock alike.
                    if (_pass == null || _tick % WorldPassTicks == 0)
                    {
                        _pass = VillageOverviewCache.BuildWorldPass();
                        RefreshVillageList();
                        EnqueueAll();
                    }

                    PumpRequests();
                    RebuildSome();
                }

                if (_tick % SecondTicks == 0)
                {
                    RepaintChangedCells();
                    UpdateFooter();
                }
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageOverviewForm tick failed: " + ex.Message);
            }
        }

        private void RefreshVillageList()
        {
            List<int> capitals = new List<int>();
            List<int> ids = VillageOverviewCache.GetVillageIds(capitals);

            bool changed = ids.Count != _villageIds.Count;
            for (int i = 0; !changed && i < ids.Count; i++)
                changed = ids[i] != _villageIds[i];
            if (!changed) return;

            _villageIds = ids;
            _capitals.Clear();
            for (int i = 0; i < capitals.Count; i++)
                _capitals[capitals[i]] = true;

            Dictionary<int, VillageOverviewRow> old = new Dictionary<int, VillageOverviewRow>(_rowsById);
            _rows.Clear();
            _rowsById.Clear();
            for (int i = 0; i < ids.Count; i++)
            {
                VillageOverviewRow row;
                if (!old.TryGetValue(ids[i], out row))
                {
                    // Built straight away rather than in the next slices, so the window
                    // never opens on a grid of blank rows. A one-off cost when it opens.
                    row = new VillageOverviewRow(ids[i]);
                    VillageOverviewCache.BuildRow(row, _capitals.ContainsKey(ids[i]), _pass);
                }
                _rows.Add(row);
                _rowsById[ids[i]] = row;
            }

            ApplyView(true);
        }

        private void EnqueueAll()
        {
            for (int i = 0; i < _rows.Count; i++)
            {
                int id = _rows[i].VillageId;
                if (_queued.ContainsKey(id)) continue;
                _queued[id] = true;
                _rebuildQueue.Enqueue(id);
            }
        }

        private void PumpRequests()
        {
            int minutes = (int)_autoMinutes.Value;
            bool forced = _forcedSweepFrom != DateTime.MinValue;
            if (minutes <= 0 && !forced) return;

            DateTime staleBefore = minutes > 0 ? _now - TimeSpan.FromMinutes(minutes) : DateTime.MinValue;
            if (forced && _forcedSweepFrom > staleBefore) staleBefore = _forcedSweepFrom;

            int waiting = VillageOverviewCache.PumpRequests(_villageIds, staleBefore, MaxOwnInFlight);
            VillageOverviewCache.PumpPremium(TimeSpan.FromMinutes(Math.Max(1, minutes)), false);

            // A sweep is over once nothing is waiting or out. That's when the order is
            // allowed to change under the user - not on every reply.
            bool done = waiting == 0 && VillageOverviewCache.PendingCount == 0;
            if (done && !_sweepDone)
            {
                _resortWhenDrained = true;
                _forcedSweepFrom = DateTime.MinValue;
            }
            _sweepDone = done;
        }

        private void RebuildSome()
        {
            if (_rebuildQueue.Count == 0 || _pass == null) return;

            Stopwatch watch = Stopwatch.StartNew();
            int built = 0;

            while (_rebuildQueue.Count > 0 && built < RowsPerTick)
            {
                int id = _rebuildQueue.Dequeue();
                _queued.Remove(id);

                VillageOverviewRow row;
                if (!_rowsById.TryGetValue(id, out row)) continue;

                // No repaint here - RepaintChangedCells picks up whatever this changed.
                VillageOverviewCache.BuildRow(row, _capitals.ContainsKey(id), _pass);
                built++;
            }

            if (_rebuildQueue.Count == 0)
            {
                if (_resortWhenDrained || FilterDependsOnValues())
                {
                    _resortWhenDrained = false;
                    ApplyView(false);
                }
                else
                {
                    RecomputeTotals();
                }
            }

            watch.Stop();
            if (watch.Elapsed.TotalMilliseconds > SlowRebuildMs && (DateTime.Now - _lastSlowLog).TotalSeconds > 60)
            {
                _lastSlowLog = DateTime.Now;
                UniversalDebugLog.Log("VillageOverviewForm: rebuilding " + built + " rows took "
                    + (int)watch.Elapsed.TotalMilliseconds + "ms");
            }
        }

        private bool FilterDependsOnValues()
        {
            return _incomingOnly.Checked || _downloadedOnly.Checked;
        }

        // Repaints just the on-screen cells whose text or colour differs from what they were
        // last painted with. Countdowns mostly change once a minute and live stock levels
        // every few seconds, so this is usually a handful of cells.
        private void RepaintChangedCells()
        {
            int rowCount = _grid.RowCount;
            if (rowCount == 0 || !_grid.Visible) return;

            List<int> cols = new List<int>();
            for (int c = 0; c < _grid.Columns.Count; c++)
            {
                if (_grid.Columns[c].Visible && _grid.Columns[c].Displayed) cols.Add(c);
            }
            if (cols.Count == 0) return;

            int first = _grid.FirstDisplayedScrollingRowIndex;
            if (first < 1) first = 1;
            int last = Math.Min(rowCount - 1, first + _grid.DisplayedRowCount(true));

            CheckRow(0, cols);
            for (int r = first; r <= last; r++)
                CheckRow(r, cols);
        }

        private void CheckRow(int gridRow, List<int> cols)
        {
            ShownRow shown;
            if (!_shown.TryGetValue(KeyForRow(gridRow), out shown)) return;

            for (int i = 0; i < cols.Count; i++)
            {
                int c = cols[i];
                if (shown.Text[c] == null) continue;   // never painted - nothing stale to fix
                if (shown.Text[c] != CellText(gridRow, c) || shown.Colour[c] != CellColour(gridRow, c).ToArgb())
                    _grid.InvalidateCell(c, gridRow);
            }
        }

        private int KeyForRow(int gridRow)
        {
            if (gridRow == 0) return TotalsKey;
            int index = gridRow - 1;
            return index >= 0 && index < _view.Count ? _view[index].VillageId : int.MinValue;
        }

        private ShownRow ShownFor(int gridRow)
        {
            int key = KeyForRow(gridRow);
            if (key == int.MinValue) return null;

            ShownRow shown;
            if (!_shown.TryGetValue(key, out shown))
            {
                shown = new ShownRow();
                shown.Text = new string[_columns.Count];
                shown.Colour = new int[_columns.Count];
                _shown[key] = shown;
            }
            return shown;
        }

        private string CellText(int gridRow, int c)
        {
            if (gridRow == 0)
            {
                if (c == 0) return "Total (" + _view.Count + ")";
                if (c < _totals.Length && _columns[c].Summable)
                    return VillageOverviewColumns.FormatNumber(_totals[c]);
                return "";
            }

            int index = gridRow - 1;
            if (index < 0 || index >= _view.Count) return "";
            return _columns[c].FormatValue(_view[index], _now);
        }

        private Color CellColour(int gridRow, int c)
        {
            if (gridRow == 0) return VillageOverviewColumns.TextPri;

            int index = gridRow - 1;
            if (index < 0 || index >= _view.Count) return VillageOverviewColumns.TextPri;

            OverviewColumn col = _columns[c];
            if (col.Colour != null) return col.Colour(_view[index], _now);
            if (col.Value != null && double.IsNaN(col.Value(_view[index]))) return VillageOverviewColumns.TextDim;
            return VillageOverviewColumns.TextPri;
        }

        private void UpdateFooter()
        {
            int live = 0, snapshot = 0, premium = 0, none = 0;
            for (int i = 0; i < _view.Count; i++)
            {
                switch (_view[i].Source)
                {
                    case OverviewSource.Live: live++; break;
                    case OverviewSource.Snapshot: snapshot++; break;
                    case OverviewSource.Premium: premium++; break;
                    default: none++; break;
                }
            }

            StringBuilder text = new StringBuilder();
            text.Append(_view.Count).Append(_view.Count == 1 ? " village" : " villages");
            text.Append("   ·   ").Append(live).Append(" live, ").Append(snapshot).Append(" from server");
            if (premium > 0) text.Append(", ").Append(premium).Append(" premium only");
            if (none > 0) text.Append(", ").Append(none).Append(" waiting");

            int ours = VillageOverviewCache.PendingCount;
            int inFlight, queued;
            VillageResourceRouter.GetCounts(out inFlight, out queued);
            text.Append("   ·   requests: ").Append(ours).Append(" ours, ")
                .Append(inFlight).Append(" on the wire, ").Append(queued).Append(" queued");

            bool isPremium = false;
            try
            {
                isPremium = GameEngine.Instance != null && GameEngine.Instance.World != null
                    && GameEngine.Instance.World.isAccountPremium();
            }
            catch (Exception) { }

            if (isPremium)
            {
                DateTime premiumTime = VillageOverviewCache.PremiumTime;
                text.Append("   ·   premium token: ");
                text.Append(premiumTime == DateTime.MinValue ? "waiting for the batch"
                    : "batch " + VillageOverviewColumns.FormatSpan((_now - premiumTime).TotalSeconds) + " old");
            }

            if ((int)_autoMinutes.Value == 0) text.Append("   ·   auto-refresh off");

            _footerLabel.Text = text.ToString();
        }

        // =================================================================
        // Filtering, sorting and totals
        // =================================================================

        // Refilters and resorts. force is for the user's own changes (sort, filter); the
        // automatic calls pass false, and then a view that comes out in the same order is
        // left alone rather than repainting the whole grid.
        private void ApplyView(bool force)
        {
            string find = _filterBox.Text.Trim();
            int type = _typeBox.SelectedIndex;

            List<VillageOverviewRow> view = new List<VillageOverviewRow>();
            for (int i = 0; i < _rows.Count; i++)
            {
                VillageOverviewRow row = _rows[i];
                if (type == 0 && row.IsCapital) continue;
                if (type == 1 && !row.IsCapital) continue;
                if (_incomingOnly.Checked && !(row.Incoming > 0)) continue;
                if (_downloadedOnly.Checked && !row.Downloaded) continue;
                if (find.Length > 0
                    && row.Name.IndexOf(find, StringComparison.OrdinalIgnoreCase) < 0
                    && row.Parish.IndexOf(find, StringComparison.OrdinalIgnoreCase) < 0
                    && row.VillageId.ToString() != find)
                    continue;
                view.Add(row);
            }

            int sortIndex = FindColumn(_sortKey);
            if (sortIndex < 0) sortIndex = 0;
            OverviewColumn sortCol = _columns[sortIndex];
            bool desc = _sortDesc;
            view.Sort(delegate(VillageOverviewRow a, VillageOverviewRow b) { return CompareRows(sortCol, desc, a, b); });

            if (!force && SameOrder(view, _view))
            {
                RecomputeTotals();
                return;
            }

            for (int i = 0; i < _grid.Columns.Count; i++)
            {
                SortOrder glyph = i == sortIndex
                    ? (desc ? SortOrder.Descending : SortOrder.Ascending)
                    : SortOrder.None;
                if (_grid.Columns[i].HeaderCell.SortGlyphDirection != glyph)
                    _grid.Columns[i].HeaderCell.SortGlyphDirection = glyph;
            }

            int selectedId = SelectedVillageId();
            int firstRow = -1;
            try { firstRow = _grid.FirstDisplayedScrollingRowIndex; }
            catch (Exception) { }

            _view = view;
            _viewIndex.Clear();
            for (int i = 0; i < _view.Count; i++)
                _viewIndex[_view[i].VillageId] = i;
            RecomputeTotals();
            _shown.Clear();

            if (_grid.RowCount != _view.Count + 1)
                _grid.RowCount = _view.Count + 1;
            _grid.Rows[0].Frozen = true;

            int selectedIndex;
            if (selectedId >= 0 && _viewIndex.TryGetValue(selectedId, out selectedIndex))
            {
                try { _grid.CurrentCell = _grid.Rows[selectedIndex + 1].Cells[0]; }
                catch (Exception) { }
            }
            else
            {
                _grid.ClearSelection();
            }

            if (firstRow > 0 && firstRow < _grid.RowCount)
            {
                try { _grid.FirstDisplayedScrollingRowIndex = firstRow; }
                catch (Exception) { }
            }

            _grid.Invalidate();
            UpdateFooter();
        }

        private static bool SameOrder(List<VillageOverviewRow> a, List<VillageOverviewRow> b)
        {
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].VillageId != b[i].VillageId) return false;
            }
            return true;
        }

        private static int CompareRows(OverviewColumn col, bool desc, VillageOverviewRow a, VillageOverviewRow b)
        {
            int cmp;
            if (col.SortText != null)
            {
                cmp = string.Compare(col.SortText(a), col.SortText(b), StringComparison.OrdinalIgnoreCase);
                if (desc) cmp = -cmp;
            }
            else if (col.Value != null)
            {
                double va = col.Value(a);
                double vb = col.Value(b);
                bool na = double.IsNaN(va), nb = double.IsNaN(vb);
                // Unknowns go to the bottom whichever way the column is sorted.
                if (na && nb) cmp = 0;
                else if (na) return 1;
                else if (nb) return -1;
                else
                {
                    cmp = va.CompareTo(vb);
                    if (desc) cmp = -cmp;
                }
            }
            else
            {
                cmp = 0;
            }

            if (cmp != 0) return cmp;
            cmp = string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
            if (cmp != 0) return cmp;
            return a.VillageId.CompareTo(b.VillageId);
        }

        private void RecomputeTotals()
        {
            if (_totals.Length != _columns.Count) _totals = new double[_columns.Count];

            for (int c = 0; c < _columns.Count; c++)
            {
                OverviewColumn col = _columns[c];
                double total = double.NaN;
                if (col.Summable && col.Value != null)
                {
                    for (int i = 0; i < _view.Count; i++)
                        total = VillageOverviewRow.Sum(total, col.Value(_view[i]));
                }
                _totals[c] = total;
            }
        }

        private int FindColumn(string key)
        {
            if (string.IsNullOrEmpty(key)) return -1;
            for (int i = 0; i < _columns.Count; i++)
            {
                if (_columns[i].Key == key) return i;
            }
            return -1;
        }

        private void ApplyColumnVisibility()
        {
            for (int i = 0; i < _columns.Count; i++)
            {
                OverviewColumn def = _columns[i];
                bool visible = (def.Group == OverviewGroup.Info || _groupOn[(int)def.Group])
                    && (i == 0 || !_hidden.ContainsKey(def.Key));
                if (_grid.Columns[i].Visible != visible)
                    _grid.Columns[i].Visible = visible;
            }
        }

        private int SelectedVillageId()
        {
            try
            {
                if (_grid.CurrentCell == null) return -1;
                int index = _grid.CurrentCell.RowIndex - 1;
                if (index < 0 || index >= _view.Count) return -1;
                return _view[index].VillageId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        // =================================================================
        // Grid events
        // =================================================================

        private void GridCellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.ColumnIndex >= _columns.Count || e.RowIndex < 0) return;

            string text = CellText(e.RowIndex, e.ColumnIndex);
            e.Value = text;

            ShownRow shown = ShownFor(e.RowIndex);
            if (shown != null) shown.Text[e.ColumnIndex] = text;
        }

        private void GridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.ColumnIndex >= _columns.Count || e.RowIndex < 0) return;

            Color colour = CellColour(e.RowIndex, e.ColumnIndex);
            ShownRow shown = ShownFor(e.RowIndex);
            if (shown != null) shown.Colour[e.ColumnIndex] = colour.ToArgb();

            if (e.RowIndex == 0)
            {
                e.CellStyle.Font = _boldFont;
                e.CellStyle.BackColor = TotalsBg;
                e.CellStyle.SelectionBackColor = TotalsBg;
            }

            e.CellStyle.ForeColor = colour;
            e.CellStyle.SelectionForeColor = colour;
        }

        private void GridCellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.ColumnIndex >= _columns.Count) return;
            if (e.RowIndex < 0)
            {
                e.ToolTipText = _columns[e.ColumnIndex].HeaderTip ?? "";
                return;
            }

            int index = e.RowIndex - 1;
            if (index < 0 || index >= _view.Count) return;

            OverviewColumn col = _columns[e.ColumnIndex];
            if (col.CellTip != null)
                e.ToolTipText = col.CellTip(_view[index], _now) ?? "";
        }

        private void GridColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.ColumnIndex >= _columns.Count) return;

            if (e.Button == MouseButtons.Right)
            {
                ShowHeaderMenu();
                return;
            }

            if (e.Button != MouseButtons.Left) return;

            string key = _columns[e.ColumnIndex].Key;
            if (key == _sortKey)
            {
                _sortDesc = !_sortDesc;
            }
            else
            {
                _sortKey = key;
                // Text reads naturally A-Z, numbers are usually wanted biggest first.
                _sortDesc = _columns[e.ColumnIndex].SortText == null;
            }
            ApplyView(true);
        }

        private void GridCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            int index = e.RowIndex - 1;
            if (index < 0 || index >= _view.Count) return;

            _menuVillageId = _view[index].VillageId;
            try { _grid.CurrentCell = _grid.Rows[e.RowIndex].Cells[Math.Max(0, e.ColumnIndex)]; }
            catch (Exception) { }

            VillageSyncModule sync = null;
            try { if (BotEngine.Instance != null) sync = BotEngine.Instance.GetModule<VillageSyncModule>(); }
            catch (Exception) { }
            _redownloadItem.Enabled = sync != null;

            _rowMenu.Show(Cursor.Position);
        }

        private void GridCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex - 1;
            if (index < 0 || index >= _view.Count) return;
            ShowOnMap(_view[index].VillageId);
        }

        private void ShowHeaderMenu()
        {
            _headerMenu.Items.Clear();

            for (int g = 0; g <= 4; g++)
            {
                if (!_groupOn[g]) continue;

                ToolStripMenuItem title = new ToolStripMenuItem(
                    VillageOverviewColumns.GroupName((OverviewGroup)g).Replace("&", "&&"));
                title.Enabled = false;
                _headerMenu.Items.Add(title);

                for (int i = 1; i < _columns.Count; i++)
                {
                    if ((int)_columns[i].Group != g) continue;
                    string key = _columns[i].Key;
                    ToolStripMenuItem item = new ToolStripMenuItem("    " + _columns[i].Header);
                    item.Checked = !_hidden.ContainsKey(key);
                    item.Click += delegate
                    {
                        if (_hidden.ContainsKey(key)) _hidden.Remove(key);
                        else _hidden[key] = true;
                        ApplyColumnVisibility();
                    };
                    _headerMenu.Items.Add(item);
                }
            }

            _headerMenu.Items.Add(new ToolStripSeparator());
            _headerMenu.Items.Add("Show all columns", null, delegate
            {
                _hidden.Clear();
                ApplyColumnVisibility();
            });

            _headerMenu.Show(Cursor.Position);
        }

        // =================================================================
        // Header strip
        // =================================================================

        private void GroupToggleChanged(object sender, EventArgs e)
        {
            CheckBox toggle = sender as CheckBox;
            if (toggle == null || !(toggle.Tag is int)) return;
            _groupOn[(int)toggle.Tag] = toggle.Checked;
            if (_loading) return;
            ApplyColumnVisibility();
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            if (_loading) return;
            ApplyView(true);
        }

        private void RefreshClick(object sender, EventArgs e)
        {
            // Everything older than now counts as stale until the sweep finishes.
            _forcedSweepFrom = VillageMap.getCurrentServerTime();
            _sweepDone = false;
            VillageOverviewCache.PumpPremium(TimeSpan.Zero, true);
            _pass = null;
        }

        private void CopyClick(object sender, EventArgs e)
        {
            List<int> order = new List<int>();
            for (int i = 0; i < _grid.Columns.Count; i++)
            {
                if (_grid.Columns[i].Visible) order.Add(i);
            }
            order.Sort(delegate(int a, int b) { return _grid.Columns[a].DisplayIndex.CompareTo(_grid.Columns[b].DisplayIndex); });

            StringBuilder text = new StringBuilder();
            for (int i = 0; i < order.Count; i++)
            {
                if (i > 0) text.Append('\t');
                text.Append(_columns[order[i]].Header);
            }
            text.Append("\r\n");

            for (int r = 0; r < _view.Count; r++)
            {
                for (int i = 0; i < order.Count; i++)
                {
                    if (i > 0) text.Append('\t');
                    string value = _columns[order[i]].FormatValue(_view[r], _now);
                    text.Append(value == VillageOverviewColumns.Dash ? "" : value);
                }
                text.Append("\r\n");
            }

            CopyText(text.ToString());
        }

        // =================================================================
        // Row actions
        // =================================================================

        // The game's own overview row click (AllVillagesPanel.VillageOverviewLine.clickedLine).
        private static void ShowOnMap(int villageId)
        {
            if (villageId < 0) return;
            try
            {
                Point location = GameEngine.Instance.World.getVillageLocation(villageId);
                GameEngine.Instance.playInterfaceSound("UserinfoScreenLine_village");
                GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
                InterfaceMgr.Instance.closeParishPanel();
                InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                GameEngine.Instance.World.startMultiStageZoom(10000.0, (double)location.X, (double)location.Y);
                InterfaceMgr.Instance.displaySelectedVillagePanel(villageId, false, true, true, false);
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageOverviewForm: show on map failed: " + ex.Message);
            }
        }

        // The game's own overview keep-icon click (VillageOverviewLine.clickedCastle).
        private static void OpenCastle(int villageId)
        {
            if (villageId < 0) return;
            try
            {
                InterfaceMgr.Instance.closeParishPanel();
                InterfaceMgr.Instance.displaySelectedVillagePanel(villageId, false, true, true, false);
                GameEngine.Instance.SkipVillageTab();
                InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                InterfaceMgr.Instance.getVillageTabBar().changeTab(1);
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageOverviewForm: open castle failed: " + ex.Message);
            }
        }

        private void RefreshVillage(int villageId)
        {
            if (villageId < 0) return;
            VillageOverviewCache.Request(villageId);
            _sweepDone = false;
        }

        private static void RequestRedownload(int villageId)
        {
            if (villageId < 0) return;
            try
            {
                VillageSyncModule sync = BotEngine.Instance != null ? BotEngine.Instance.GetModule<VillageSyncModule>() : null;
                if (sync != null) sync.RequestForceRedownload(villageId);
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageOverviewForm: re-download request failed: " + ex.Message);
            }
        }

        private static void CopyText(string text)
        {
            // The clipboard can be locked by another process; a failed copy must not take
            // the window down.
            try { Clipboard.SetText(text); }
            catch (Exception) { }
        }

        // =================================================================
        // Testing
        // =================================================================

        // Used by the headless screenshot harness (via reflection) to show made-up rows
        // without a game running. Stops the tick from touching the game.
        private void SetRowsForTest(List<VillageOverviewRow> rows)
        {
            _testMode = true;
            _rows.Clear();
            _rowsById.Clear();
            for (int i = 0; i < rows.Count; i++)
            {
                _rows.Add(rows[i]);
                _rowsById[rows[i].VillageId] = rows[i];
            }
            _now = VillageMap.getCurrentServerTime();
            ApplyView(true);
        }

        private void SetGroupsForTest(bool resources, bool military, bool village, bool units)
        {
            _groupToggles[1].Checked = resources;
            _groupToggles[2].Checked = military;
            _groupToggles[3].Checked = village;
            _groupToggles[4].Checked = units;
        }

        // =================================================================
        // Closing
        // =================================================================

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }

            VillageOverviewCache.CancelAll();
            if (!_testMode) SaveSettings();

            if (_instance == this)
                _instance = null;

            base.OnFormClosed(e);
        }

        /// <summary>A DataGridView that paints through a back buffer, so updates don't flicker.</summary>
        private class OverviewGrid : DataGridView
        {
            public OverviewGrid()
            {
                this.DoubleBuffered = true;
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            }
        }
    }
}
