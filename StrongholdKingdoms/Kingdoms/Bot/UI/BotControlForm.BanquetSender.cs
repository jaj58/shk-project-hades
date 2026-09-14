using System;
using System.Drawing;
using System.Windows.Forms;
using Kingdoms.Bot;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Banquet Sender tab — connection settings and live status for BanquetSenderModule.
    /// What gets sent where is configured for the whole group on the website
    /// (api/banquet/); this tab only holds per-account settings.
    /// Built in code like the Timing Tool tab, and inserted right after the Banquet tab.
    /// </summary>
    public partial class BotControlForm
    {
        private TabPage _bsPage;
        private CheckBox _bsEnabledCheck;
        private Label _bsStatusLabel;
        private TextBox _bsApiUrlBox;
        private TextBox _bsKeyBox;
        private CheckBox _bsShowKeyCheck;
        private NumericUpDown _bsSyncIntervalInput;
        private NumericUpDown _bsReserveInput;
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
            _bsPage.Text = "Banquet Sender";
            _bsPage.BackColor = TtBg;
            // Start at the real page size so the anchored activity list doesn't stretch by
            // the difference from TabPage's 200x100 default when the page is inserted.
            _bsPage.Size = _bqPage.Size;

            _bsEnabledCheck = new CheckBox();
            _bsEnabledCheck.Text = "Enabled";
            _bsEnabledCheck.ForeColor = TtText;
            _bsEnabledCheck.FlatStyle = FlatStyle.Flat;
            _bsEnabledCheck.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            _bsEnabledCheck.Location = new Point(10, 10);
            _bsEnabledCheck.Size = new Size(90, 22);
            _bsPage.Controls.Add(_bsEnabledCheck);

            _bsStatusLabel = TtMakeLabel("DISABLED", 104, 13, 90);
            _bsStatusLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            _bsPage.Controls.Add(_bsStatusLabel);

            Label intro = TtMakeLabel("Shares banquet goods between players in a group. Choose what to fill " +
                "(auto-fill every village or focus players) on the group website — every player uses the same key.",
                200, 13, 900);
            intro.ForeColor = Color.FromArgb(160, 165, 180);
            _bsPage.Controls.Add(intro);

            _bsPage.Controls.Add(TtMakeLabel("API URL:", 10, 46, 80));
            _bsApiUrlBox = TtMakeTextBox(94, 44, 460);
            _bsPage.Controls.Add(_bsApiUrlBox);
            Label urlHint = TtMakeLabel("e.g. https://your-site/api/banquet_api.php", 562, 46, 300);
            urlHint.ForeColor = Color.FromArgb(120, 125, 140);
            _bsPage.Controls.Add(urlHint);

            _bsPage.Controls.Add(TtMakeLabel("Group key:", 10, 76, 80));
            _bsKeyBox = TtMakeTextBox(94, 74, 260);
            _bsKeyBox.UseSystemPasswordChar = true;
            _bsPage.Controls.Add(_bsKeyBox);

            _bsShowKeyCheck = new CheckBox();
            _bsShowKeyCheck.Text = "Show";
            _bsShowKeyCheck.ForeColor = TtText;
            _bsShowKeyCheck.FlatStyle = FlatStyle.Flat;
            _bsShowKeyCheck.Font = new Font("Segoe UI", 8f);
            _bsShowKeyCheck.Location = new Point(362, 75);
            _bsShowKeyCheck.Size = new Size(60, 20);
            _bsShowKeyCheck.CheckedChanged += delegate { _bsKeyBox.UseSystemPasswordChar = !_bsShowKeyCheck.Checked; };
            _bsPage.Controls.Add(_bsShowKeyCheck);

            _bsPage.Controls.Add(TtMakeLabel("Sync every (s):", 10, 106, 90));
            _bsSyncIntervalInput = TtMakeNumeric(104, 104, 10, 300, 20, 60);
            _bsPage.Controls.Add(_bsSyncIntervalInput);

            _bsPage.Controls.Add(TtMakeLabel("Merchants to keep per village:", 180, 106, 170));
            _bsReserveInput = TtMakeNumeric(354, 104, 0, 1000, 0, 60);
            _bsPage.Controls.Add(_bsReserveInput);

            _bsRefreshCheck = new CheckBox();
            _bsRefreshCheck.Text = "Re-download my villages when goods have arrived";
            _bsRefreshCheck.ForeColor = TtText;
            _bsRefreshCheck.FlatStyle = FlatStyle.Flat;
            _bsRefreshCheck.Font = new Font("Segoe UI", 8f);
            _bsRefreshCheck.Location = new Point(430, 105);
            _bsRefreshCheck.Size = new Size(320, 20);
            _bsPage.Controls.Add(_bsRefreshCheck);

            Button syncBtn = TtMakeButton("Sync Now", 10, 138, 90);
            syncBtn.Click += delegate { BanquetSenderModule m = BsModule; if (m != null) m.SyncNow(); };
            _bsPage.Controls.Add(syncBtn);

            Button siteBtn = TtMakeButton("Open Group Website", 106, 138, 150);
            siteBtn.Click += delegate { BsOpenWebsite(); };
            _bsPage.Controls.Add(siteBtn);

            // ---- Live status ----
            int sy = 178;
            _bsConnectionLabel = TtMakeLabel("", 10, sy, 1100);
            _bsConnectionLabel.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            _bsPage.Controls.Add(_bsConnectionLabel);
            _bsModeLabel = TtMakeLabel("", 10, sy + 22, 540);
            _bsPage.Controls.Add(_bsModeLabel);
            _bsGroupLabel = TtMakeLabel("", 560, sy + 22, 540);
            _bsPage.Controls.Add(_bsGroupLabel);
            _bsQueueLabel = TtMakeLabel("", 10, sy + 42, 540);
            _bsPage.Controls.Add(_bsQueueLabel);
            _bsSentLabel = TtMakeLabel("", 560, sy + 42, 540);
            _bsPage.Controls.Add(_bsSentLabel);
            _bsCurrentLabel = TtMakeLabel("", 10, sy + 62, 1100);
            _bsCurrentLabel.ForeColor = Color.FromArgb(220, 180, 60);
            _bsPage.Controls.Add(_bsCurrentLabel);

            _bsPage.Controls.Add(TtMakeLabel("Activity (this session):", 10, sy + 88, 200));
            _bsActivityList = new ListBox();
            _bsActivityList.BackColor = TtInputBg;
            _bsActivityList.ForeColor = TtText;
            _bsActivityList.BorderStyle = BorderStyle.FixedSingle;
            _bsActivityList.Font = new Font("Consolas", 8.5f);
            _bsActivityList.Location = new Point(10, sy + 106);
            _bsActivityList.Size = new Size(1120, 200);
            _bsActivityList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            _bsActivityList.IntegralHeight = false;
            _bsPage.Controls.Add(_bsActivityList);

            // Live-but-not-saved, like the other tabs: edits apply to the running module
            // immediately; the Save Settings button persists them.
            _bsEnabledCheck.CheckedChanged += delegate { BsWriteToSettings(); };
            _bsApiUrlBox.TextChanged += delegate { BsWriteToSettings(); };
            _bsKeyBox.TextChanged += delegate { BsWriteToSettings(); };
            _bsSyncIntervalInput.ValueChanged += delegate { BsWriteToSettings(); };
            _bsReserveInput.ValueChanged += delegate { BsWriteToSettings(); };
            _bsRefreshCheck.CheckedChanged += delegate { BsWriteToSettings(); };

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
                if (s.MerchantsReserve > _bsReserveInput.Maximum) _bsReserveInput.Maximum = s.MerchantsReserve;
                _bsReserveInput.Value = Math.Max(0, s.MerchantsReserve);
                _bsRefreshCheck.Checked = s.RefreshArrivedVillages;
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
            s.MerchantsReserve = (int)_bsReserveInput.Value;
            s.RefreshArrivedVillages = _bsRefreshCheck.Checked;

            BanquetSenderModule m = BsModule;
            if (m != null) m.Enabled = s.Enabled;
            BsUpdateStatusDisplay();
        }

        private void BsOpenWebsite()
        {
            BanquetSenderSettings s = BsSettings;
            string site = BanquetSenderModule.WebsiteUrlFor(s != null ? s.ApiUrl : "");
            if (string.IsNullOrEmpty(site))
            {
                MessageBox.Show(this, "Set the API URL first.", "Banquet Sender", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // The key goes in the fragment: browsers never send it to the server, and the
            // page removes it from the address bar after reading it.
            if (!string.IsNullOrEmpty(s.GroupKey)) site += "#key=" + Uri.EscapeDataString(s.GroupKey);
            try { System.Diagnostics.Process.Start(site); }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not open the browser: " + ex.Message, "Banquet Sender",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // Only repaint the (potentially long) activity list while the tab is visible.
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

            string mode;
            switch (st.Mode)
            {
                case "autofill": mode = "Auto-fill every village"; break;
                case "focus": mode = "Focus on selected players / villages"; break;
                case "off": mode = "Off (nothing is being sent)"; break;
                default: mode = "—"; break;
            }
            _bsModeLabel.Text = "Group mode: " + mode;
            _bsGroupLabel.Text = "Players online: " + st.PlayersOnline + "    Shipments on the way (group): " + st.InFlight;
            _bsQueueLabel.Text = "My open orders: " + st.OpenOrders;
            _bsSentLabel.Text = "Sent this session: " + st.SentCount + " shipments, " + st.SentAmount.ToString("N0") + " goods";
            _bsCurrentLabel.Text = st.Current;

            if (_bsActivityList.Items.Count != st.Activity.Count ||
                (st.Activity.Count > 0 && (string)_bsActivityList.Items[0] != st.Activity[0]))
            {
                _bsActivityList.BeginUpdate();
                _bsActivityList.Items.Clear();
                foreach (string line in st.Activity) _bsActivityList.Items.Add(line);
                _bsActivityList.EndUpdate();
            }
        }
    }
}
