using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Kingdoms.Bot;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Sender tab (formerly Banquet Sender) — connection settings and live status for
    /// BanquetSenderModule, which shares banquet goods, resources, food and weapons.
    /// Sub-tabs: Connection (URL, key, sync, every category's activity) and one per category
    /// (switch it on/off for this account, merchants to keep, that category's activity).
    /// What gets sent where is configured for the whole group on the website (api/banquet/).
    /// Built in code like the Timing Tool tab, and inserted right after the Banquet tab.
    /// </summary>
    public partial class BotControlForm
    {
        private TabPage _bsPage;
        private TabControl _bsTabs;
        private CheckBox _bsEnabledCheck;
        private Label _bsStatusLabel;
        private TextBox _bsApiUrlBox;
        private TextBox _bsKeyBox;
        private CheckBox _bsShowKeyCheck;
        private NumericUpDown _bsSyncIntervalInput;
        private CheckBox _bsRefreshCheck;
        private Label _bsConnectionLabel;
        private Label _bsModeLabel;
        private Label _bsGroupLabel;
        private Label _bsQueueLabel;
        private Label _bsSentLabel;
        private Label _bsCurrentLabel;
        private ListBox _bsActivityList;
        private Timer _bsRefreshTimer;
        private bool _bsLoading;
        // The settings object the controls were last loaded from. Writes are refused until a
        // load has happened (the form can open before login), and a new object (world switch)
        // triggers a reload instead of being overwritten with the previous world's values.
        private BanquetSenderSettings _bsLoadedFrom;

        /// <summary>The controls of one category's sub-tab.</summary>
        private class BsCategoryTab
        {
            public string Category;
            public TabPage Page;
            public CheckBox EnabledCheck;
            public NumericUpDown ReserveInput;
            public Label ModeLabel;
            public Label QueueLabel;
            public Label SentLabel;
            public Label CurrentLabel;
            public ListBox ActivityList;
        }

        private readonly List<BsCategoryTab> _bsCategoryTabs = new List<BsCategoryTab>();

        private BanquetSenderSettings BsSettings
        {
            get
            {
                return BotEngine.Instance != null && BotEngine.Instance.Settings != null
                    ? BotEngine.Instance.Settings.BanquetSender : null;
            }
        }

        private static BanquetSenderModule BsModule
        {
            get { return BotEngine.Instance != null ? BotEngine.Instance.GetModule<BanquetSenderModule>() : null; }
        }

        private void WireUpBanquetSenderTab()
        {
            _bsPage = new TabPage();
            _bsPage.Text = "Sender";
            _bsPage.BackColor = TtBg;
            // Start at the real page size so anchored lists don't stretch by the difference
            // from TabPage's 200x100 default when the page is inserted.
            _bsPage.Size = _bqPage.Size;

            _bsTabs = new TabControl();
            _bsTabs.Font = new Font("Segoe UI", 9f);
            _bsTabs.Size = _bsPage.Size;
            Size subSize = new Size(Math.Max(200, _bsPage.Width - 8), Math.Max(100, _bsPage.Height - 26));

            TabPage connPage = new TabPage("Connection");
            connPage.BackColor = TtBg;
            connPage.Size = subSize;
            BsBuildConnectionTab(connPage);
            _bsTabs.TabPages.Add(connPage);

            for (int i = 0; i < SenderGoods.Categories.Length; i++)
            {
                BsCategoryTab tab = BsBuildCategoryTab(SenderGoods.Categories[i], SenderGoods.CategoryNames[i], subSize);
                _bsCategoryTabs.Add(tab);
                _bsTabs.TabPages.Add(tab.Page);
            }
            _bsTabs.Dock = DockStyle.Fill;
            _bsTabs.SelectedIndexChanged += delegate { try { BsUpdateStatusDisplay(); } catch { } };
            _bsPage.Controls.Add(_bsTabs);

            // TabPages.Insert is silently ignored until the TabControl has a window handle
            // (WinForms quirk), and this runs from the constructor — force the handle first.
            IntPtr tabHandle = _tabControl.Handle;
            int bqIndex = _tabControl.TabPages.IndexOf(_bqPage);
            if (bqIndex >= 0) _tabControl.TabPages.Insert(bqIndex + 1, _bsPage);
            else _tabControl.TabPages.Add(_bsPage);

            _bsRefreshTimer = new Timer();
            _bsRefreshTimer.Interval = 1500;
            _bsRefreshTimer.Tick += delegate { try { BsUpdateStatusDisplay(); } catch { } };
            _bsRefreshTimer.Start();
        }

        private void BsBuildConnectionTab(TabPage page)
        {
            _bsEnabledCheck = BsMakeCheck("Enabled", 10, 10, 90);
            _bsEnabledCheck.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            page.Controls.Add(_bsEnabledCheck);

            _bsStatusLabel = TtMakeLabel("DISABLED", 104, 13, 90);
            _bsStatusLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            page.Controls.Add(_bsStatusLabel);

            Label intro = TtMakeLabel("Shares banquet goods, resources, food and weapons in a group (one key for everyone). " +
                "Switch each on in its tab; the website decides what is sent.", 200, 13, 920);
            intro.ForeColor = Color.FromArgb(160, 165, 180);
            page.Controls.Add(intro);

            page.Controls.Add(TtMakeLabel("API URL:", 10, 46, 80));
            _bsApiUrlBox = TtMakeTextBox(94, 44, 460);
            page.Controls.Add(_bsApiUrlBox);
            Label urlHint = TtMakeLabel("e.g. https://your-site/api/banquet_api.php", 562, 46, 300);
            urlHint.ForeColor = Color.FromArgb(120, 125, 140);
            page.Controls.Add(urlHint);

            page.Controls.Add(TtMakeLabel("Group key:", 10, 76, 80));
            _bsKeyBox = TtMakeTextBox(94, 74, 260);
            _bsKeyBox.UseSystemPasswordChar = true;
            page.Controls.Add(_bsKeyBox);

            _bsShowKeyCheck = BsMakeCheck("Show", 362, 75, 60);
            _bsShowKeyCheck.CheckedChanged += delegate { _bsKeyBox.UseSystemPasswordChar = !_bsShowKeyCheck.Checked; };
            page.Controls.Add(_bsShowKeyCheck);

            page.Controls.Add(TtMakeLabel("Sync every (s):", 10, 106, 90));
            _bsSyncIntervalInput = TtMakeNumeric(104, 104, 10, 300, 20, 60);
            page.Controls.Add(_bsSyncIntervalInput);

            _bsRefreshCheck = BsMakeCheck("Re-download my villages when goods have arrived", 180, 105, 320);
            page.Controls.Add(_bsRefreshCheck);

            Button syncBtn = TtMakeButton("Sync Now", 10, 138, 90);
            syncBtn.Click += delegate { BanquetSenderModule m = BsModule; if (m != null) m.SyncNow(); };
            page.Controls.Add(syncBtn);

            Button siteBtn = TtMakeButton("Open Group Website", 106, 138, 150);
            siteBtn.Click += delegate { BsOpenWebsite(null); };
            page.Controls.Add(siteBtn);

            // ---- Live status ----
            int sy = 178;
            _bsConnectionLabel = TtMakeLabel("", 10, sy, 1100);
            _bsConnectionLabel.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            page.Controls.Add(_bsConnectionLabel);
            _bsModeLabel = TtMakeLabel("", 10, sy + 22, 540);
            page.Controls.Add(_bsModeLabel);
            _bsGroupLabel = TtMakeLabel("", 560, sy + 22, 540);
            page.Controls.Add(_bsGroupLabel);
            _bsQueueLabel = TtMakeLabel("", 10, sy + 42, 540);
            page.Controls.Add(_bsQueueLabel);
            _bsSentLabel = TtMakeLabel("", 560, sy + 42, 540);
            page.Controls.Add(_bsSentLabel);
            _bsCurrentLabel = TtMakeLabel("", 10, sy + 62, 1100);
            _bsCurrentLabel.ForeColor = Color.FromArgb(220, 180, 60);
            page.Controls.Add(_bsCurrentLabel);

            page.Controls.Add(TtMakeLabel("Activity, every category (this session):", 10, sy + 88, 300));
            _bsActivityList = BsMakeActivityList(page, sy + 106);

            // Live-but-not-saved, like the other tabs: edits apply to the running module
            // immediately; the Save Settings button persists them.
            _bsEnabledCheck.CheckedChanged += delegate { BsWriteToSettings(); };
            _bsApiUrlBox.TextChanged += delegate { BsWriteToSettings(); };
            _bsKeyBox.TextChanged += delegate { BsWriteToSettings(); };
            _bsSyncIntervalInput.ValueChanged += delegate { BsWriteToSettings(); };
            _bsRefreshCheck.CheckedChanged += delegate { BsWriteToSettings(); };
        }

        private BsCategoryTab BsBuildCategoryTab(string category, string title, Size size)
        {
            BsCategoryTab t = new BsCategoryTab();
            t.Category = category;
            t.Page = new TabPage(title);
            t.Page.BackColor = TtBg;
            t.Page.Size = size;

            t.EnabledCheck = BsMakeCheck("Share " + title.ToLower() + " with the group", 10, 10, 280);
            t.EnabledCheck.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            t.Page.Controls.Add(t.EnabledCheck);

            List<string> names = new List<string>();
            for (int g = 0; g < SenderGoods.Count; g++)
                if (SenderGoods.Category(g) == category) names.Add(SenderGoods.Name(g));
            Label goods = TtMakeLabel(string.Join(", ", names.ToArray()), 300, 13, 700);
            goods.ForeColor = Color.FromArgb(160, 165, 180);
            t.Page.Controls.Add(goods);

            Label hint = TtMakeLabel("Off: this account neither gives nor receives " + title.ToLower() +
                ". On: the group website decides what is sent (its " + title + " tab).", 10, 38, 900);
            hint.ForeColor = Color.FromArgb(120, 125, 140);
            t.Page.Controls.Add(hint);

            t.Page.Controls.Add(TtMakeLabel("Merchants to keep per village:", 10, 68, 170));
            t.ReserveInput = TtMakeNumeric(184, 66, 0, 1000, 0, 60);
            t.Page.Controls.Add(t.ReserveInput);
            Label reserveHint = TtMakeLabel("never used for " + title.ToLower() + " (e.g. leave some for the Trade module)", 254, 68, 400);
            reserveHint.ForeColor = Color.FromArgb(120, 125, 140);
            t.Page.Controls.Add(reserveHint);

            Button siteBtn = TtMakeButton("Open " + title + " on the Website", 10, 100, 220);
            siteBtn.Click += delegate { BsOpenWebsite(category); };
            t.Page.Controls.Add(siteBtn);

            int sy = 140;
            t.ModeLabel = TtMakeLabel("", 10, sy, 540);
            t.Page.Controls.Add(t.ModeLabel);
            t.QueueLabel = TtMakeLabel("", 560, sy, 540);
            t.Page.Controls.Add(t.QueueLabel);
            t.SentLabel = TtMakeLabel("", 10, sy + 20, 540);
            t.Page.Controls.Add(t.SentLabel);
            t.CurrentLabel = TtMakeLabel("", 10, sy + 40, 1100);
            t.CurrentLabel.ForeColor = Color.FromArgb(220, 180, 60);
            t.Page.Controls.Add(t.CurrentLabel);

            t.Page.Controls.Add(TtMakeLabel(title + " activity (this session):", 10, sy + 66, 300));
            t.ActivityList = BsMakeActivityList(t.Page, sy + 84);

            t.EnabledCheck.CheckedChanged += delegate { BsWriteToSettings(); };
            t.ReserveInput.ValueChanged += delegate { BsWriteToSettings(); };
            return t;
        }

        private CheckBox BsMakeCheck(string text, int x, int y, int w)
        {
            CheckBox c = new CheckBox();
            c.Text = text;
            c.ForeColor = TtText;
            c.FlatStyle = FlatStyle.Flat;
            c.Font = new Font("Segoe UI", 8f);
            c.Location = new Point(x, y);
            c.Size = new Size(w, 22);
            return c;
        }

        private ListBox BsMakeActivityList(TabPage page, int y)
        {
            ListBox list = new ListBox();
            list.BackColor = TtInputBg;
            list.ForeColor = TtText;
            list.BorderStyle = BorderStyle.FixedSingle;
            list.Font = new Font("Consolas", 8.5f);
            list.Location = new Point(10, y);
            list.Size = new Size(Math.Max(200, page.Width - 20), Math.Max(60, page.Height - y - 10));
            list.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            list.IntegralHeight = false;
            page.Controls.Add(list);
            return list;
        }

        private void BsLoadFromSettings()
        {
            BanquetSenderSettings s = BsSettings;
            if (s == null) return;
            _bsLoading = true;
            try
            {
                _bsEnabledCheck.Checked = s.Enabled;
                _bsApiUrlBox.Text = s.ApiUrl ?? "";
                _bsKeyBox.Text = s.GroupKey ?? "";
                _bsSyncIntervalInput.Value = Math.Max(_bsSyncIntervalInput.Minimum,
                    Math.Min(_bsSyncIntervalInput.Maximum, s.SyncIntervalSeconds));
                _bsRefreshCheck.Checked = s.RefreshArrivedVillages;
                foreach (BsCategoryTab t in _bsCategoryTabs)
                {
                    t.EnabledCheck.Checked = s.IsCategoryEnabled(t.Category);
                    int reserve = Math.Max(0, s.GetCategoryReserve(t.Category));
                    // Raise the maximum rather than clamping a saved value down (and losing it).
                    if (reserve > t.ReserveInput.Maximum) t.ReserveInput.Maximum = reserve;
                    t.ReserveInput.Value = reserve;
                }
                _bsLoadedFrom = s;
            }
            finally { _bsLoading = false; }
            BsUpdateStatusDisplay();
        }

        private void BsWriteToSettings()
        {
            if (_bsLoading) return;
            BanquetSenderSettings s = BsSettings;
            if (s == null || s != _bsLoadedFrom) return;

            s.Enabled = _bsEnabledCheck.Checked;
            s.ApiUrl = _bsApiUrlBox.Text.Trim();
            s.GroupKey = _bsKeyBox.Text.Trim();
            s.SyncIntervalSeconds = (int)_bsSyncIntervalInput.Value;
            s.RefreshArrivedVillages = _bsRefreshCheck.Checked;
            bool categoryChanged = false;
            foreach (BsCategoryTab t in _bsCategoryTabs)
            {
                if (s.IsCategoryEnabled(t.Category) != t.EnabledCheck.Checked ||
                    s.GetCategoryReserve(t.Category) != (int)t.ReserveInput.Value)
                    categoryChanged = true;
                s.SetCategoryEnabled(t.Category, t.EnabledCheck.Checked);
                s.SetCategoryReserve(t.Category, (int)t.ReserveInput.Value);
            }

            BanquetSenderModule m = BsModule;
            if (m != null)
            {
                m.Enabled = s.Enabled;
                // Tell the API straight away so it stops (or starts) planning for this account.
                if (categoryChanged) m.SyncNow();
            }
            BsUpdateStatusDisplay();
        }

        private void BsOpenWebsite(string category)
        {
            BanquetSenderSettings s = BsSettings;
            string site = BanquetSenderModule.WebsiteUrlFor(s != null ? s.ApiUrl : "");
            if (string.IsNullOrEmpty(site))
            {
                MessageBox.Show(this, "Set the API URL first (Connection tab).", "Sender", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // The key goes in the fragment: browsers never send it to the server, and the
            // page removes it from the address bar after reading it.
            string fragment = "";
            if (!string.IsNullOrEmpty(s.GroupKey)) fragment += "key=" + Uri.EscapeDataString(s.GroupKey);
            if (!string.IsNullOrEmpty(category)) fragment += (fragment.Length > 0 ? "&" : "") + "tab=" + category;
            if (fragment.Length > 0) site += "#" + fragment;
            try { System.Diagnostics.Process.Start(site); }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not open the browser: " + ex.Message, "Sender",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static string BsModeText(string mode)
        {
            switch (mode)
            {
                case "autofill": return "Auto-fill every village";
                case "focus": return "Focus on selected players / villages";
                case "off": return "Off (nothing is being sent)";
                case "": return "— (update the API to use this category)";
                default: return "—";
            }
        }

        private void BsUpdateStatusDisplay()
        {
            if (_bsPage == null) return;
            if (BsSettings != null && BsSettings != _bsLoadedFrom) BsLoadFromSettings();
            bool enabled = _bsEnabledCheck.Checked;
            _bsStatusLabel.Text = enabled ? "ENABLED" : "DISABLED";
            _bsStatusLabel.ForeColor = enabled ? SuccessCol : ErrorCol;

            BanquetSenderModule m = BsModule;
            if (m == null)
            {
                _bsConnectionLabel.Text = "Not running — log into a world first.";
                _bsConnectionLabel.ForeColor = TtText;
                return;
            }
            // Only repaint the (potentially long) activity lists while the tab is visible.
            if (_tabControl.SelectedTab != _bsPage) return;

            BanquetSenderModule.Status st = m.GetStatus();
            if (!st.Configured)
            {
                _bsConnectionLabel.Text = "Set the API URL and a group key (6+ characters) to connect.";
                _bsConnectionLabel.ForeColor = WarningCol;
            }
            else if (!enabled)
            {
                _bsConnectionLabel.Text = "Disabled.";
                _bsConnectionLabel.ForeColor = TtText;
            }
            else if (!string.IsNullOrEmpty(st.LastError))
            {
                _bsConnectionLabel.Text = "Sync error: " + st.LastError;
                _bsConnectionLabel.ForeColor = ErrorCol;
            }
            else if (st.LastSyncOk == DateTime.MinValue)
            {
                _bsConnectionLabel.Text = "Connecting…";
                _bsConnectionLabel.ForeColor = WarningCol;
            }
            else
            {
                _bsConnectionLabel.Text = "Connected — last sync " + (int)(DateTime.Now - st.LastSyncOk).TotalSeconds + "s ago.";
                _bsConnectionLabel.ForeColor = SuccessCol;
            }

            BanquetSenderSettings settings = BsSettings;
            List<string> modes = new List<string>();
            for (int i = 0; i < SenderGoods.Categories.Length; i++)
            {
                string c = SenderGoods.Categories[i];
                string mode = st.Category(c).Mode;
                bool on = settings != null && settings.IsCategoryEnabled(c);
                modes.Add(SenderGoods.CategoryNames[i] + ": " + (!on ? "off here" : mode == "autofill" ? "auto-fill"
                    : mode == "focus" ? "focus" : mode == "off" ? "off" : "—"));
            }
            _bsModeLabel.Text = "Group modes — " + string.Join(" · ", modes.ToArray());
            _bsGroupLabel.Text = "Players online: " + st.PlayersOnline + "    Shipments on the way (group): " + st.InFlight;
            _bsQueueLabel.Text = "My open orders: " + st.OpenOrders;
            _bsSentLabel.Text = "Sent this session: " + st.SentCount + " shipments, " + st.SentAmount.ToString("N0") + " goods";
            _bsCurrentLabel.Text = st.Current;

            TabPage sub = _bsTabs.SelectedTab;
            if (sub != null && sub == _bsTabs.TabPages[0])
            {
                BsFillActivity(_bsActivityList, st.Activity, null);
                return;
            }
            foreach (BsCategoryTab t in _bsCategoryTabs)
            {
                if (t.Page != sub) continue;
                BanquetSenderModule.CategoryStatus cs = st.Category(t.Category);
                bool on = settings != null && settings.IsCategoryEnabled(t.Category);
                t.ModeLabel.Text = "Group mode: " + BsModeText(cs.Mode) + (on ? "" : "   (switched off for this account)");
                t.ModeLabel.ForeColor = on ? TtText : WarningCol;
                t.QueueLabel.Text = "My open orders: " + cs.OpenOrders;
                t.SentLabel.Text = "Sent this session: " + cs.SentCount + " shipments, " + cs.SentAmount.ToString("N0") + " goods";
                t.CurrentLabel.Text = st.CurrentCategory == t.Category ? st.Current : "";
                BsFillActivity(t.ActivityList, st.Activity, t.Category);
            }
        }

        /// <summary>Show the activity lines of one category (null = all, tagged with their category).</summary>
        private static void BsFillActivity(ListBox list, List<BanquetSenderModule.ActivityLine> activity, string category)
        {
            List<string> lines = new List<string>();
            foreach (BanquetSenderModule.ActivityLine a in activity)
            {
                if (category == null)
                {
                    // "HH:mm:ss  text" -> "HH:mm:ss  [Food] text"
                    string tag = a.Category.Length > 0 ? "[" + SenderGoods.CategoryName(a.Category) + "] " : "";
                    lines.Add(a.Text.Length > 10 ? a.Text.Substring(0, 10) + tag + a.Text.Substring(10) : tag + a.Text);
                }
                else if (a.Category == category)
                    lines.Add(a.Text);
            }
            if (list.Items.Count == lines.Count && (lines.Count == 0 || (string)list.Items[0] == lines[0])) return;
            list.BeginUpdate();
            list.Items.Clear();
            foreach (string line in lines) list.Items.Add(line);
            list.EndUpdate();
        }
    }
}
