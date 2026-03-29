namespace Kingdoms.Bot.UI
{
    partial class BotControlForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_vsRefreshTimer != null)
                {
                    _vsRefreshTimer.Stop();
                    _vsRefreshTimer.Dispose();
                }
                if (_rdRefreshTimer != null)
                {
                    _rdRefreshTimer.Stop();
                    _rdRefreshTimer.Dispose();
                }
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._footerPanel = new System.Windows.Forms.Panel();
            this._versionLabel = new System.Windows.Forms.Label();
            this._clearLogBtn = new System.Windows.Forms.Button();
            this._loadBtn = new System.Windows.Forms.Button();
            this._saveBtn = new System.Windows.Forms.Button();
            this._footerSep = new System.Windows.Forms.Panel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._masterToggleBtn = new System.Windows.Forms.Button();
            this._statusLabel = new System.Windows.Forms.Label();
            this._titleLabel = new System.Windows.Forms.Label();
            this._headerSep = new System.Windows.Forms.Panel();
            this._mainSplit = new System.Windows.Forms.SplitContainer();
            this._tabControl = new System.Windows.Forms.TabControl();
            this._villageSyncPage = new System.Windows.Forms.TabPage();
            this._radarPage = new System.Windows.Forms.TabPage();
            this._logPanel = new System.Windows.Forms.Panel();
            this._logBox = new System.Windows.Forms.RichTextBox();
            this._logHeader = new System.Windows.Forms.Label();
            // Village Sync tab controls
            this._vsSettingsPanel = new System.Windows.Forms.Panel();
            this._vsEnabledCheck = new System.Windows.Forms.CheckBox();
            this._vsStatusLabel = new System.Windows.Forms.Label();
            this._vsLastRunLabel = new System.Windows.Forms.Label();
            this._vsIntervalLabel = new System.Windows.Forms.Label();
            this._vsIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._vsDelayLabel = new System.Windows.Forms.Label();
            this._vsDelayInput = new System.Windows.Forms.NumericUpDown();
            this._vsButtonBar = new System.Windows.Forms.Panel();
            this._vsRefreshBtn = new System.Windows.Forms.Button();
            this._vsSelectAllBtn = new System.Windows.Forms.Button();
            this._vsDeselectAllBtn = new System.Windows.Forms.Button();
            this._vsSeparator = new System.Windows.Forms.Panel();
            this._vsListHeader = new System.Windows.Forms.Label();
            this._vsColHeader = new System.Windows.Forms.Panel();
            this._vsHdrSync = new System.Windows.Forms.Label();
            this._vsHdrName = new System.Windows.Forms.Label();
            this._vsHdrType = new System.Windows.Forms.Label();
            this._vsHdrId = new System.Windows.Forms.Label();
            this._vsVillageListPanel = new System.Windows.Forms.Panel();
            // Radar tab controls
            this._rdSettingsPanel = new System.Windows.Forms.Panel();
            this._rdEnabledCheck = new System.Windows.Forms.CheckBox();
            this._rdStatusLabel = new System.Windows.Forms.Label();
            this._rdScanIntervalLabel = new System.Windows.Forms.Label();
            this._rdScanIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._rdWebhookLabel = new System.Windows.Forms.Label();
            this._rdWebhookInput = new System.Windows.Forms.TextBox();
            this._rdInterdictLabel = new System.Windows.Forms.Label();
            this._rdInterdictMonkCountInput = new System.Windows.Forms.NumericUpDown();
            this._rdHintLabel = new System.Windows.Forms.Label();
            this._rdTestDiscordBtn = new System.Windows.Forms.Button();
            this._rdSeparator = new System.Windows.Forms.Panel();
            this._rdColHeader = new System.Windows.Forms.Panel();
            this._rdColActionType = new System.Windows.Forms.Label();
            this._rdColMonitor = new System.Windows.Forms.Label();
            this._rdColSystemNotify = new System.Windows.Forms.Label();
            this._rdColDiscordNotify = new System.Windows.Forms.Label();
            this._rdColAutoInterdict = new System.Windows.Forms.Label();
            this._rdActionListPanel = new System.Windows.Forms.Panel();
            // SuspendLayout
            this._footerPanel.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._mainSplit.Panel1.SuspendLayout();
            this._mainSplit.Panel2.SuspendLayout();
            this._mainSplit.SuspendLayout();
            this._tabControl.SuspendLayout();
            this._villageSyncPage.SuspendLayout();
            this._radarPage.SuspendLayout();
            this._logPanel.SuspendLayout();
            this._vsSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._vsIntervalInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._vsDelayInput)).BeginInit();
            this._vsButtonBar.SuspendLayout();
            this._vsColHeader.SuspendLayout();
            this._rdSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._rdScanIntervalInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rdInterdictMonkCountInput)).BeginInit();
            this._rdColHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // _footerPanel
            // 
            this._footerPanel.BackColor = System.Drawing.Color.FromArgb(32, 34, 44);
            this._footerPanel.Controls.Add(this._versionLabel);
            this._footerPanel.Controls.Add(this._clearLogBtn);
            this._footerPanel.Controls.Add(this._loadBtn);
            this._footerPanel.Controls.Add(this._saveBtn);
            this._footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footerPanel.Location = new System.Drawing.Point(0, 634);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this._footerPanel.Size = new System.Drawing.Size(820, 44);
            this._footerPanel.TabIndex = 0;
            // 
            // _versionLabel
            // 
            this._versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._versionLabel.AutoSize = true;
            this._versionLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._versionLabel.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._versionLabel.Location = new System.Drawing.Point(764, 14);
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(36, 13);
            this._versionLabel.TabIndex = 3;
            this._versionLabel.Text = "v1.0.0";
            // 
            // _clearLogBtn
            // 
            this._clearLogBtn.BackColor = System.Drawing.Color.FromArgb(40, 42, 54);
            this._clearLogBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._clearLogBtn.FlatAppearance.BorderSize = 0;
            this._clearLogBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearLogBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._clearLogBtn.ForeColor = System.Drawing.Color.White;
            this._clearLogBtn.Location = new System.Drawing.Point(248, 7);
            this._clearLogBtn.Name = "_clearLogBtn";
            this._clearLogBtn.Size = new System.Drawing.Size(90, 30);
            this._clearLogBtn.TabIndex = 2;
            this._clearLogBtn.Text = "Clear Log";
            this._clearLogBtn.UseVisualStyleBackColor = false;
            this._clearLogBtn.Click += new System.EventHandler(this.ClearLogBtn_Click);
            // 
            // _loadBtn
            // 
            this._loadBtn.BackColor = System.Drawing.Color.FromArgb(40, 42, 54);
            this._loadBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._loadBtn.FlatAppearance.BorderSize = 0;
            this._loadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._loadBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._loadBtn.ForeColor = System.Drawing.Color.White;
            this._loadBtn.Location = new System.Drawing.Point(130, 7);
            this._loadBtn.Name = "_loadBtn";
            this._loadBtn.Size = new System.Drawing.Size(110, 30);
            this._loadBtn.TabIndex = 1;
            this._loadBtn.Text = "Load Settings";
            this._loadBtn.UseVisualStyleBackColor = false;
            this._loadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // _saveBtn
            // 
            this._saveBtn.BackColor = System.Drawing.Color.FromArgb(50, 100, 180);
            this._saveBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._saveBtn.FlatAppearance.BorderSize = 0;
            this._saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._saveBtn.ForeColor = System.Drawing.Color.White;
            this._saveBtn.Location = new System.Drawing.Point(12, 7);
            this._saveBtn.Name = "_saveBtn";
            this._saveBtn.Size = new System.Drawing.Size(110, 30);
            this._saveBtn.TabIndex = 0;
            this._saveBtn.Text = "Save Settings";
            this._saveBtn.UseVisualStyleBackColor = false;
            this._saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // _footerSep
            // 
            this._footerSep.BackColor = System.Drawing.Color.FromArgb(55, 58, 72);
            this._footerSep.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footerSep.Location = new System.Drawing.Point(0, 633);
            this._footerSep.Name = "_footerSep";
            this._footerSep.Size = new System.Drawing.Size(820, 1);
            this._footerSep.TabIndex = 1;
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.FromArgb(32, 34, 44);
            this._headerPanel.Controls.Add(this._masterToggleBtn);
            this._headerPanel.Controls.Add(this._statusLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(820, 56);
            this._headerPanel.TabIndex = 2;
            // 
            // _masterToggleBtn
            // 
            this._masterToggleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._masterToggleBtn.BackColor = System.Drawing.Color.FromArgb(80, 160, 255);
            this._masterToggleBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._masterToggleBtn.FlatAppearance.BorderSize = 0;
            this._masterToggleBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._masterToggleBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._masterToggleBtn.ForeColor = System.Drawing.Color.White;
            this._masterToggleBtn.Location = new System.Drawing.Point(674, 10);
            this._masterToggleBtn.Name = "_masterToggleBtn";
            this._masterToggleBtn.Size = new System.Drawing.Size(130, 36);
            this._masterToggleBtn.TabIndex = 2;
            this._masterToggleBtn.Text = "START BOT";
            this._masterToggleBtn.UseVisualStyleBackColor = false;
            this._masterToggleBtn.Click += new System.EventHandler(this.MasterToggle_Click);
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(240, 80, 80);
            this._statusLabel.Location = new System.Drawing.Point(220, 18);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(59, 15);
            this._statusLabel.TabIndex = 1;
            this._statusLabel.Text = "STOPPED";
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = true;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(80, 160, 255);
            this._titleLabel.Location = new System.Drawing.Point(16, 12);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(168, 28);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "PROJECT HADES";
            // 
            // _headerSep
            // 
            this._headerSep.BackColor = System.Drawing.Color.FromArgb(55, 58, 72);
            this._headerSep.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerSep.Location = new System.Drawing.Point(0, 56);
            this._headerSep.Name = "_headerSep";
            this._headerSep.Size = new System.Drawing.Size(820, 1);
            this._headerSep.TabIndex = 3;
            // 
            // _mainSplit
            // 
            this._mainSplit.BackColor = System.Drawing.Color.FromArgb(55, 58, 72);
            this._mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainSplit.Location = new System.Drawing.Point(0, 57);
            this._mainSplit.Name = "_mainSplit";
            this._mainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _mainSplit.Panel1
            // 
            this._mainSplit.Panel1.BackColor = System.Drawing.Color.FromArgb(24, 24, 32);
            this._mainSplit.Panel1.Controls.Add(this._tabControl);
            // 
            // _mainSplit.Panel2
            // 
            this._mainSplit.Panel2.BackColor = System.Drawing.Color.FromArgb(24, 24, 32);
            this._mainSplit.Panel2.Controls.Add(this._logPanel);
            this._mainSplit.Size = new System.Drawing.Size(820, 576);
            this._mainSplit.SplitterDistance = 380;
            this._mainSplit.SplitterWidth = 3;
            this._mainSplit.TabIndex = 4;
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this._villageSyncPage);
            this._tabControl.Controls.Add(this._radarPage);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(820, 380);
            this._tabControl.TabIndex = 0;
            // 
            // _villageSyncPage
            // 
            this._villageSyncPage.BackColor = System.Drawing.Color.FromArgb(24, 24, 32);
            this._villageSyncPage.Controls.Add(this._vsVillageListPanel);
            this._villageSyncPage.Controls.Add(this._vsColHeader);
            this._villageSyncPage.Controls.Add(this._vsListHeader);
            this._villageSyncPage.Controls.Add(this._vsSeparator);
            this._villageSyncPage.Controls.Add(this._vsSettingsPanel);
            this._villageSyncPage.Location = new System.Drawing.Point(4, 24);
            this._villageSyncPage.Name = "_villageSyncPage";
            this._villageSyncPage.Size = new System.Drawing.Size(812, 352);
            this._villageSyncPage.TabIndex = 0;
            this._villageSyncPage.Text = "Village Sync";
            // 
            // _vsSettingsPanel
            // 
            this._vsSettingsPanel.BackColor = System.Drawing.Color.FromArgb(40, 42, 54);
            this._vsSettingsPanel.Controls.Add(this._vsButtonBar);
            this._vsSettingsPanel.Controls.Add(this._vsDelayInput);
            this._vsSettingsPanel.Controls.Add(this._vsDelayLabel);
            this._vsSettingsPanel.Controls.Add(this._vsIntervalInput);
            this._vsSettingsPanel.Controls.Add(this._vsIntervalLabel);
            this._vsSettingsPanel.Controls.Add(this._vsLastRunLabel);
            this._vsSettingsPanel.Controls.Add(this._vsStatusLabel);
            this._vsSettingsPanel.Controls.Add(this._vsEnabledCheck);
            this._vsSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._vsSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._vsSettingsPanel.Name = "_vsSettingsPanel";
            this._vsSettingsPanel.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this._vsSettingsPanel.Size = new System.Drawing.Size(812, 100);
            this._vsSettingsPanel.TabIndex = 0;
            // 
            // _vsEnabledCheck
            // 
            this._vsEnabledCheck.AutoSize = true;
            this._vsEnabledCheck.Checked = true;
            this._vsEnabledCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._vsEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._vsEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._vsEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(230, 230, 240);
            this._vsEnabledCheck.Location = new System.Drawing.Point(16, 12);
            this._vsEnabledCheck.Name = "_vsEnabledCheck";
            this._vsEnabledCheck.Size = new System.Drawing.Size(136, 23);
            this._vsEnabledCheck.TabIndex = 0;
            this._vsEnabledCheck.Text = "Module Enabled";
            // 
            // _vsStatusLabel
            // 
            this._vsStatusLabel.AutoSize = true;
            this._vsStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._vsStatusLabel.ForeColor = System.Drawing.Color.FromArgb(80, 200, 120);
            this._vsStatusLabel.Location = new System.Drawing.Point(200, 15);
            this._vsStatusLabel.Name = "_vsStatusLabel";
            this._vsStatusLabel.Size = new System.Drawing.Size(56, 13);
            this._vsStatusLabel.TabIndex = 1;
            this._vsStatusLabel.Text = "ENABLED";
            // 
            // _vsLastRunLabel
            // 
            this._vsLastRunLabel.AutoSize = true;
            this._vsLastRunLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._vsLastRunLabel.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._vsLastRunLabel.Location = new System.Drawing.Point(300, 15);
            this._vsLastRunLabel.Name = "_vsLastRunLabel";
            this._vsLastRunLabel.Size = new System.Drawing.Size(80, 13);
            this._vsLastRunLabel.TabIndex = 2;
            this._vsLastRunLabel.Text = "Last run: never";
            // 
            // _vsIntervalLabel
            // 
            this._vsIntervalLabel.AutoSize = true;
            this._vsIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._vsIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._vsIntervalLabel.Location = new System.Drawing.Point(16, 44);
            this._vsIntervalLabel.Name = "_vsIntervalLabel";
            this._vsIntervalLabel.Size = new System.Drawing.Size(137, 15);
            this._vsIntervalLabel.TabIndex = 3;
            this._vsIntervalLabel.Text = "Cycle interval (seconds):";
            // 
            // _vsIntervalInput
            // 
            this._vsIntervalInput.BackColor = System.Drawing.Color.FromArgb(50, 52, 64);
            this._vsIntervalInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._vsIntervalInput.ForeColor = System.Drawing.Color.FromArgb(230, 230, 240);
            this._vsIntervalInput.Increment = new decimal(10);
            this._vsIntervalInput.Location = new System.Drawing.Point(190, 42);
            this._vsIntervalInput.Maximum = new decimal(3600);
            this._vsIntervalInput.Minimum = new decimal(30);
            this._vsIntervalInput.Name = "_vsIntervalInput";
            this._vsIntervalInput.Size = new System.Drawing.Size(80, 22);
            this._vsIntervalInput.TabIndex = 4;
            this._vsIntervalInput.Value = new decimal(120);
            // 
            // _vsDelayLabel
            // 
            this._vsDelayLabel.AutoSize = true;
            this._vsDelayLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._vsDelayLabel.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._vsDelayLabel.Location = new System.Drawing.Point(290, 44);
            this._vsDelayLabel.Name = "_vsDelayLabel";
            this._vsDelayLabel.Size = new System.Drawing.Size(161, 15);
            this._vsDelayLabel.TabIndex = 5;
            this._vsDelayLabel.Text = "Delay between villages (ms):";
            // 
            // _vsDelayInput
            // 
            this._vsDelayInput.BackColor = System.Drawing.Color.FromArgb(50, 52, 64);
            this._vsDelayInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._vsDelayInput.ForeColor = System.Drawing.Color.FromArgb(230, 230, 240);
            this._vsDelayInput.Increment = new decimal(500);
            this._vsDelayInput.Location = new System.Drawing.Point(498, 42);
            this._vsDelayInput.Maximum = new decimal(30000);
            this._vsDelayInput.Minimum = new decimal(1000);
            this._vsDelayInput.Name = "_vsDelayInput";
            this._vsDelayInput.Size = new System.Drawing.Size(80, 22);
            this._vsDelayInput.TabIndex = 6;
            this._vsDelayInput.Value = new decimal(3000);
            // 
            // _vsButtonBar
            // 
            this._vsButtonBar.Controls.Add(this._vsRefreshBtn);
            this._vsButtonBar.Controls.Add(this._vsSelectAllBtn);
            this._vsButtonBar.Controls.Add(this._vsDeselectAllBtn);
            this._vsButtonBar.Location = new System.Drawing.Point(16, 70);
            this._vsButtonBar.Name = "_vsButtonBar";
            this._vsButtonBar.Size = new System.Drawing.Size(700, 28);
            this._vsButtonBar.TabIndex = 7;
            // 
            // _vsRefreshBtn
            // 
            this._vsRefreshBtn.BackColor = System.Drawing.Color.FromArgb(80, 160, 255);
            this._vsRefreshBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._vsRefreshBtn.FlatAppearance.BorderSize = 0;
            this._vsRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._vsRefreshBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._vsRefreshBtn.ForeColor = System.Drawing.Color.White;
            this._vsRefreshBtn.Location = new System.Drawing.Point(0, 0);
            this._vsRefreshBtn.Name = "_vsRefreshBtn";
            this._vsRefreshBtn.Size = new System.Drawing.Size(100, 24);
            this._vsRefreshBtn.TabIndex = 0;
            this._vsRefreshBtn.Text = "Refresh List";
            this._vsRefreshBtn.UseVisualStyleBackColor = false;
            // 
            // _vsSelectAllBtn
            // 
            this._vsSelectAllBtn.BackColor = System.Drawing.Color.FromArgb(50, 100, 180);
            this._vsSelectAllBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._vsSelectAllBtn.FlatAppearance.BorderSize = 0;
            this._vsSelectAllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._vsSelectAllBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._vsSelectAllBtn.ForeColor = System.Drawing.Color.White;
            this._vsSelectAllBtn.Location = new System.Drawing.Point(110, 0);
            this._vsSelectAllBtn.Name = "_vsSelectAllBtn";
            this._vsSelectAllBtn.Size = new System.Drawing.Size(100, 24);
            this._vsSelectAllBtn.TabIndex = 1;
            this._vsSelectAllBtn.Text = "Select All";
            this._vsSelectAllBtn.UseVisualStyleBackColor = false;
            // 
            // _vsDeselectAllBtn
            // 
            this._vsDeselectAllBtn.BackColor = System.Drawing.Color.FromArgb(50, 100, 180);
            this._vsDeselectAllBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._vsDeselectAllBtn.FlatAppearance.BorderSize = 0;
            this._vsDeselectAllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._vsDeselectAllBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._vsDeselectAllBtn.ForeColor = System.Drawing.Color.White;
            this._vsDeselectAllBtn.Location = new System.Drawing.Point(220, 0);
            this._vsDeselectAllBtn.Name = "_vsDeselectAllBtn";
            this._vsDeselectAllBtn.Size = new System.Drawing.Size(100, 24);
            this._vsDeselectAllBtn.TabIndex = 2;
            this._vsDeselectAllBtn.Text = "Deselect All";
            this._vsDeselectAllBtn.UseVisualStyleBackColor = false;
            // 
            // _vsSeparator
            // 
            this._vsSeparator.BackColor = System.Drawing.Color.FromArgb(55, 58, 72);
            this._vsSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._vsSeparator.Location = new System.Drawing.Point(0, 100);
            this._vsSeparator.Name = "_vsSeparator";
            this._vsSeparator.Size = new System.Drawing.Size(812, 1);
            this._vsSeparator.TabIndex = 1;
            // 
            // _vsListHeader
            // 
            this._vsListHeader.BackColor = System.Drawing.Color.FromArgb(24, 24, 32);
            this._vsListHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._vsListHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._vsListHeader.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._vsListHeader.Location = new System.Drawing.Point(0, 101);
            this._vsListHeader.Name = "_vsListHeader";
            this._vsListHeader.Padding = new System.Windows.Forms.Padding(16, 6, 0, 0);
            this._vsListHeader.Size = new System.Drawing.Size(812, 26);
            this._vsListHeader.TabIndex = 2;
            this._vsListHeader.Text = "VILLAGES";
            // 
            // _vsColHeader
            // 
            this._vsColHeader.BackColor = System.Drawing.Color.FromArgb(30, 32, 40);
            this._vsColHeader.Controls.Add(this._vsHdrSync);
            this._vsColHeader.Controls.Add(this._vsHdrName);
            this._vsColHeader.Controls.Add(this._vsHdrType);
            this._vsColHeader.Controls.Add(this._vsHdrId);
            this._vsColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._vsColHeader.Location = new System.Drawing.Point(0, 127);
            this._vsColHeader.Name = "_vsColHeader";
            this._vsColHeader.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this._vsColHeader.Size = new System.Drawing.Size(812, 22);
            this._vsColHeader.TabIndex = 3;
            // 
            // _vsHdrSync
            // 
            this._vsHdrSync.AutoSize = true;
            this._vsHdrSync.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._vsHdrSync.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._vsHdrSync.Location = new System.Drawing.Point(16, 4);
            this._vsHdrSync.Name = "_vsHdrSync";
            this._vsHdrSync.Size = new System.Drawing.Size(29, 12);
            this._vsHdrSync.TabIndex = 0;
            this._vsHdrSync.Text = "Sync";
            // 
            // _vsHdrName
            // 
            this._vsHdrName.AutoSize = true;
            this._vsHdrName.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._vsHdrName.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._vsHdrName.Location = new System.Drawing.Point(60, 4);
            this._vsHdrName.Name = "_vsHdrName";
            this._vsHdrName.Size = new System.Drawing.Size(72, 12);
            this._vsHdrName.TabIndex = 1;
            this._vsHdrName.Text = "Village Name";
            // 
            // _vsHdrType
            // 
            this._vsHdrType.AutoSize = true;
            this._vsHdrType.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._vsHdrType.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._vsHdrType.Location = new System.Drawing.Point(360, 4);
            this._vsHdrType.Name = "_vsHdrType";
            this._vsHdrType.Size = new System.Drawing.Size(29, 12);
            this._vsHdrType.TabIndex = 2;
            this._vsHdrType.Text = "Type";
            // 
            // _vsHdrId
            // 
            this._vsHdrId.AutoSize = true;
            this._vsHdrId.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._vsHdrId.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._vsHdrId.Location = new System.Drawing.Point(460, 4);
            this._vsHdrId.Name = "_vsHdrId";
            this._vsHdrId.Size = new System.Drawing.Size(14, 12);
            this._vsHdrId.TabIndex = 3;
            this._vsHdrId.Text = "ID";
            // 
            // _vsVillageListPanel
            // 
            this._vsVillageListPanel.AutoScroll = true;
            this._vsVillageListPanel.BackColor = System.Drawing.Color.FromArgb(24, 24, 32);
            this._vsVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vsVillageListPanel.Location = new System.Drawing.Point(0, 149);
            this._vsVillageListPanel.Name = "_vsVillageListPanel";
            this._vsVillageListPanel.Size = new System.Drawing.Size(812, 203);
            this._vsVillageListPanel.TabIndex = 4;
            // 
            // _radarPage
            // 
            this._radarPage.BackColor = System.Drawing.Color.FromArgb(24, 24, 32);
            this._radarPage.Controls.Add(this._rdActionListPanel);
            this._radarPage.Controls.Add(this._rdColHeader);
            this._radarPage.Controls.Add(this._rdSeparator);
            this._radarPage.Controls.Add(this._rdSettingsPanel);
            this._radarPage.Location = new System.Drawing.Point(4, 24);
            this._radarPage.Name = "_radarPage";
            this._radarPage.Size = new System.Drawing.Size(812, 352);
            this._radarPage.TabIndex = 1;
            this._radarPage.Text = "Radar";
            // 
            // _rdSettingsPanel
            // 
            this._rdSettingsPanel.BackColor = System.Drawing.Color.FromArgb(40, 42, 54);
            this._rdSettingsPanel.Controls.Add(this._rdTestDiscordBtn);
            this._rdSettingsPanel.Controls.Add(this._rdHintLabel);
            this._rdSettingsPanel.Controls.Add(this._rdInterdictMonkCountInput);
            this._rdSettingsPanel.Controls.Add(this._rdInterdictLabel);
            this._rdSettingsPanel.Controls.Add(this._rdWebhookInput);
            this._rdSettingsPanel.Controls.Add(this._rdWebhookLabel);
            this._rdSettingsPanel.Controls.Add(this._rdScanIntervalInput);
            this._rdSettingsPanel.Controls.Add(this._rdScanIntervalLabel);
            this._rdSettingsPanel.Controls.Add(this._rdStatusLabel);
            this._rdSettingsPanel.Controls.Add(this._rdEnabledCheck);
            this._rdSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._rdSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._rdSettingsPanel.Name = "_rdSettingsPanel";
            this._rdSettingsPanel.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this._rdSettingsPanel.Size = new System.Drawing.Size(812, 150);
            this._rdSettingsPanel.TabIndex = 0;
            // 
            // _rdEnabledCheck
            // 
            this._rdEnabledCheck.AutoSize = true;
            this._rdEnabledCheck.Checked = true;
            this._rdEnabledCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._rdEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rdEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._rdEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(230, 230, 240);
            this._rdEnabledCheck.Location = new System.Drawing.Point(16, 10);
            this._rdEnabledCheck.Name = "_rdEnabledCheck";
            this._rdEnabledCheck.Size = new System.Drawing.Size(136, 23);
            this._rdEnabledCheck.TabIndex = 0;
            this._rdEnabledCheck.Text = "Module Enabled";
            // 
            // _rdStatusLabel
            // 
            this._rdStatusLabel.AutoSize = true;
            this._rdStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._rdStatusLabel.ForeColor = System.Drawing.Color.FromArgb(80, 200, 120);
            this._rdStatusLabel.Location = new System.Drawing.Point(200, 13);
            this._rdStatusLabel.Name = "_rdStatusLabel";
            this._rdStatusLabel.Size = new System.Drawing.Size(56, 13);
            this._rdStatusLabel.TabIndex = 1;
            this._rdStatusLabel.Text = "ENABLED";
            // 
            // _rdScanIntervalLabel
            // 
            this._rdScanIntervalLabel.AutoSize = true;
            this._rdScanIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdScanIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._rdScanIntervalLabel.Location = new System.Drawing.Point(310, 13);
            this._rdScanIntervalLabel.Name = "_rdScanIntervalLabel";
            this._rdScanIntervalLabel.Size = new System.Drawing.Size(96, 15);
            this._rdScanIntervalLabel.TabIndex = 2;
            this._rdScanIntervalLabel.Text = "Scan interval (s):";
            // 
            // _rdScanIntervalInput
            // 
            this._rdScanIntervalInput.BackColor = System.Drawing.Color.FromArgb(50, 52, 64);
            this._rdScanIntervalInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdScanIntervalInput.ForeColor = System.Drawing.Color.FromArgb(230, 230, 240);
            this._rdScanIntervalInput.Location = new System.Drawing.Point(440, 11);
            this._rdScanIntervalInput.Maximum = new decimal(120);
            this._rdScanIntervalInput.Minimum = new decimal(3);
            this._rdScanIntervalInput.Name = "_rdScanIntervalInput";
            this._rdScanIntervalInput.Size = new System.Drawing.Size(60, 22);
            this._rdScanIntervalInput.TabIndex = 3;
            this._rdScanIntervalInput.Value = new decimal(10);
            // 
            // _rdWebhookLabel
            // 
            this._rdWebhookLabel.AutoSize = true;
            this._rdWebhookLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdWebhookLabel.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._rdWebhookLabel.Location = new System.Drawing.Point(16, 42);
            this._rdWebhookLabel.Name = "_rdWebhookLabel";
            this._rdWebhookLabel.Size = new System.Drawing.Size(128, 15);
            this._rdWebhookLabel.TabIndex = 4;
            this._rdWebhookLabel.Text = "Discord Webhook URL:";
            // 
            // _rdWebhookInput
            // 
            this._rdWebhookInput.BackColor = System.Drawing.Color.FromArgb(50, 52, 64);
            this._rdWebhookInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdWebhookInput.Font = new System.Drawing.Font("Consolas", 8.5F);
            this._rdWebhookInput.ForeColor = System.Drawing.Color.FromArgb(230, 230, 240);
            this._rdWebhookInput.Location = new System.Drawing.Point(170, 40);
            this._rdWebhookInput.Name = "_rdWebhookInput";
            this._rdWebhookInput.Size = new System.Drawing.Size(540, 21);
            this._rdWebhookInput.TabIndex = 5;
            // 
            // _rdInterdictLabel
            // 
            this._rdInterdictLabel.AutoSize = true;
            this._rdInterdictLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdInterdictLabel.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._rdInterdictLabel.Location = new System.Drawing.Point(16, 72);
            this._rdInterdictLabel.Name = "_rdInterdictLabel";
            this._rdInterdictLabel.Size = new System.Drawing.Size(124, 15);
            this._rdInterdictLabel.TabIndex = 6;
            this._rdInterdictLabel.Text = "Auto-interdict monks:";
            // 
            // _rdInterdictMonkCountInput
            // 
            this._rdInterdictMonkCountInput.BackColor = System.Drawing.Color.FromArgb(50, 52, 64);
            this._rdInterdictMonkCountInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdInterdictMonkCountInput.ForeColor = System.Drawing.Color.FromArgb(230, 230, 240);
            this._rdInterdictMonkCountInput.Location = new System.Drawing.Point(170, 70);
            this._rdInterdictMonkCountInput.Maximum = new decimal(100);
            this._rdInterdictMonkCountInput.Minimum = new decimal(1);
            this._rdInterdictMonkCountInput.Name = "_rdInterdictMonkCountInput";
            this._rdInterdictMonkCountInput.Size = new System.Drawing.Size(60, 22);
            this._rdInterdictMonkCountInput.TabIndex = 7;
            this._rdInterdictMonkCountInput.Value = new decimal(5);
            // 
            // _rdHintLabel
            // 
            this._rdHintLabel.AutoSize = true;
            this._rdHintLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);
            this._rdHintLabel.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._rdHintLabel.Location = new System.Drawing.Point(16, 100);
            this._rdHintLabel.Name = "_rdHintLabel";
            this._rdHintLabel.Size = new System.Drawing.Size(384, 12);
            this._rdHintLabel.TabIndex = 8;
            this._rdHintLabel.Text = "Configure which actions to monitor below. Tick columns to enable per-action features.";
            // 
            // _rdTestDiscordBtn
            // 
            this._rdTestDiscordBtn.BackColor = System.Drawing.Color.FromArgb(80, 160, 255);
            this._rdTestDiscordBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._rdTestDiscordBtn.FlatAppearance.BorderSize = 0;
            this._rdTestDiscordBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rdTestDiscordBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._rdTestDiscordBtn.ForeColor = System.Drawing.Color.White;
            this._rdTestDiscordBtn.Location = new System.Drawing.Point(540, 68);
            this._rdTestDiscordBtn.Name = "_rdTestDiscordBtn";
            this._rdTestDiscordBtn.Size = new System.Drawing.Size(110, 24);
            this._rdTestDiscordBtn.TabIndex = 9;
            this._rdTestDiscordBtn.Text = "Test Webhook";
            this._rdTestDiscordBtn.UseVisualStyleBackColor = false;
            // 
            // _rdSeparator
            // 
            this._rdSeparator.BackColor = System.Drawing.Color.FromArgb(55, 58, 72);
            this._rdSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._rdSeparator.Location = new System.Drawing.Point(0, 150);
            this._rdSeparator.Name = "_rdSeparator";
            this._rdSeparator.Size = new System.Drawing.Size(812, 1);
            this._rdSeparator.TabIndex = 1;
            // 
            // _rdColHeader
            // 
            this._rdColHeader.BackColor = System.Drawing.Color.FromArgb(30, 32, 40);
            this._rdColHeader.Controls.Add(this._rdColActionType);
            this._rdColHeader.Controls.Add(this._rdColMonitor);
            this._rdColHeader.Controls.Add(this._rdColSystemNotify);
            this._rdColHeader.Controls.Add(this._rdColDiscordNotify);
            this._rdColHeader.Controls.Add(this._rdColAutoInterdict);
            this._rdColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._rdColHeader.Location = new System.Drawing.Point(0, 151);
            this._rdColHeader.Name = "_rdColHeader";
            this._rdColHeader.Size = new System.Drawing.Size(812, 24);
            this._rdColHeader.TabIndex = 2;
            // 
            // _rdColActionType
            // 
            this._rdColActionType.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColActionType.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._rdColActionType.Location = new System.Drawing.Point(16, 2);
            this._rdColActionType.Name = "_rdColActionType";
            this._rdColActionType.Size = new System.Drawing.Size(180, 20);
            this._rdColActionType.TabIndex = 0;
            this._rdColActionType.Text = "Action Type";
            // 
            // _rdColMonitor
            // 
            this._rdColMonitor.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColMonitor.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._rdColMonitor.Location = new System.Drawing.Point(200, 2);
            this._rdColMonitor.Name = "_rdColMonitor";
            this._rdColMonitor.Size = new System.Drawing.Size(60, 20);
            this._rdColMonitor.TabIndex = 1;
            this._rdColMonitor.Text = "Monitor";
            // 
            // _rdColSystemNotify
            // 
            this._rdColSystemNotify.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColSystemNotify.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._rdColSystemNotify.Location = new System.Drawing.Point(270, 2);
            this._rdColSystemNotify.Name = "_rdColSystemNotify";
            this._rdColSystemNotify.Size = new System.Drawing.Size(60, 20);
            this._rdColSystemNotify.TabIndex = 2;
            this._rdColSystemNotify.Text = "System\r\nNotify";
            // 
            // _rdColDiscordNotify
            // 
            this._rdColDiscordNotify.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColDiscordNotify.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._rdColDiscordNotify.Location = new System.Drawing.Point(340, 2);
            this._rdColDiscordNotify.Name = "_rdColDiscordNotify";
            this._rdColDiscordNotify.Size = new System.Drawing.Size(60, 20);
            this._rdColDiscordNotify.TabIndex = 3;
            this._rdColDiscordNotify.Text = "Discord\r\nNotify";
            // 
            // _rdColAutoInterdict
            // 
            this._rdColAutoInterdict.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColAutoInterdict.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._rdColAutoInterdict.Location = new System.Drawing.Point(410, 2);
            this._rdColAutoInterdict.Name = "_rdColAutoInterdict";
            this._rdColAutoInterdict.Size = new System.Drawing.Size(60, 20);
            this._rdColAutoInterdict.TabIndex = 4;
            this._rdColAutoInterdict.Text = "Auto\r\nInterdict";
            // 
            // _rdActionListPanel
            // 
            this._rdActionListPanel.AutoScroll = true;
            this._rdActionListPanel.BackColor = System.Drawing.Color.FromArgb(24, 24, 32);
            this._rdActionListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rdActionListPanel.Location = new System.Drawing.Point(0, 175);
            this._rdActionListPanel.Name = "_rdActionListPanel";
            this._rdActionListPanel.Size = new System.Drawing.Size(812, 177);
            this._rdActionListPanel.TabIndex = 3;
            // 
            // _logPanel
            // 
            this._logPanel.BackColor = System.Drawing.Color.FromArgb(24, 24, 32);
            this._logPanel.Controls.Add(this._logBox);
            this._logPanel.Controls.Add(this._logHeader);
            this._logPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logPanel.Location = new System.Drawing.Point(0, 0);
            this._logPanel.Name = "_logPanel";
            this._logPanel.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this._logPanel.Size = new System.Drawing.Size(820, 193);
            this._logPanel.TabIndex = 0;
            // 
            // _logBox
            // 
            this._logBox.BackColor = System.Drawing.Color.FromArgb(18, 18, 24);
            this._logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logBox.Font = new System.Drawing.Font("Consolas", 8.5F);
            this._logBox.ForeColor = System.Drawing.Color.FromArgb(230, 230, 240);
            this._logBox.Location = new System.Drawing.Point(8, 26);
            this._logBox.Name = "_logBox";
            this._logBox.ReadOnly = true;
            this._logBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._logBox.Size = new System.Drawing.Size(804, 163);
            this._logBox.TabIndex = 1;
            this._logBox.Text = "";
            this._logBox.WordWrap = false;
            // 
            // _logHeader
            // 
            this._logHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._logHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._logHeader.ForeColor = System.Drawing.Color.FromArgb(160, 165, 180);
            this._logHeader.Location = new System.Drawing.Point(8, 4);
            this._logHeader.Name = "_logHeader";
            this._logHeader.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
            this._logHeader.Size = new System.Drawing.Size(804, 22);
            this._logHeader.TabIndex = 0;
            this._logHeader.Text = "LOG";
            // 
            // BotControlForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(24, 24, 32);
            this.ClientSize = new System.Drawing.Size(820, 678);
            this.Controls.Add(this._mainSplit);
            this.Controls.Add(this._headerSep);
            this.Controls.Add(this._headerPanel);
            this.Controls.Add(this._footerSep);
            this.Controls.Add(this._footerPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(230, 230, 240);
            this.MinimumSize = new System.Drawing.Size(700, 560);
            this.Name = "BotControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Hades - Bot Control";
            this._footerPanel.ResumeLayout(false);
            this._footerPanel.PerformLayout();
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._mainSplit.Panel1.ResumeLayout(false);
            this._mainSplit.Panel2.ResumeLayout(false);
            this._mainSplit.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this._villageSyncPage.ResumeLayout(false);
            this._radarPage.ResumeLayout(false);
            this._logPanel.ResumeLayout(false);
            this._vsSettingsPanel.ResumeLayout(false);
            this._vsSettingsPanel.PerformLayout();
            this._vsButtonBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._vsIntervalInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._vsDelayInput)).EndInit();
            this._vsColHeader.ResumeLayout(false);
            this._vsColHeader.PerformLayout();
            this._rdSettingsPanel.ResumeLayout(false);
            this._rdSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._rdScanIntervalInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rdInterdictMonkCountInput)).EndInit();
            this._rdColHeader.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // Form controls
        private System.Windows.Forms.Panel _footerPanel;
        private System.Windows.Forms.Button _saveBtn;
        private System.Windows.Forms.Button _loadBtn;
        private System.Windows.Forms.Button _clearLogBtn;
        private System.Windows.Forms.Label _versionLabel;
        private System.Windows.Forms.Panel _footerSep;
        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Label _titleLabel;
        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.Button _masterToggleBtn;
        private System.Windows.Forms.Panel _headerSep;
        private System.Windows.Forms.SplitContainer _mainSplit;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _villageSyncPage;
        private System.Windows.Forms.TabPage _radarPage;
        private System.Windows.Forms.Panel _logPanel;
        private System.Windows.Forms.RichTextBox _logBox;
        private System.Windows.Forms.Label _logHeader;
        // Village Sync tab controls
        private System.Windows.Forms.Panel _vsSettingsPanel;
        private System.Windows.Forms.CheckBox _vsEnabledCheck;
        private System.Windows.Forms.Label _vsStatusLabel;
        private System.Windows.Forms.Label _vsLastRunLabel;
        private System.Windows.Forms.Label _vsIntervalLabel;
        private System.Windows.Forms.NumericUpDown _vsIntervalInput;
        private System.Windows.Forms.Label _vsDelayLabel;
        private System.Windows.Forms.NumericUpDown _vsDelayInput;
        private System.Windows.Forms.Panel _vsButtonBar;
        private System.Windows.Forms.Button _vsRefreshBtn;
        private System.Windows.Forms.Button _vsSelectAllBtn;
        private System.Windows.Forms.Button _vsDeselectAllBtn;
        private System.Windows.Forms.Panel _vsSeparator;
        private System.Windows.Forms.Label _vsListHeader;
        private System.Windows.Forms.Panel _vsColHeader;
        private System.Windows.Forms.Label _vsHdrSync;
        private System.Windows.Forms.Label _vsHdrName;
        private System.Windows.Forms.Label _vsHdrType;
        private System.Windows.Forms.Label _vsHdrId;
        private System.Windows.Forms.Panel _vsVillageListPanel;
        // Radar tab controls
        private System.Windows.Forms.Panel _rdSettingsPanel;
        private System.Windows.Forms.CheckBox _rdEnabledCheck;
        private System.Windows.Forms.Label _rdStatusLabel;
        private System.Windows.Forms.Label _rdScanIntervalLabel;
        private System.Windows.Forms.NumericUpDown _rdScanIntervalInput;
        private System.Windows.Forms.Label _rdWebhookLabel;
        private System.Windows.Forms.TextBox _rdWebhookInput;
        private System.Windows.Forms.Label _rdInterdictLabel;
        private System.Windows.Forms.NumericUpDown _rdInterdictMonkCountInput;
        private System.Windows.Forms.Label _rdHintLabel;
        private System.Windows.Forms.Button _rdTestDiscordBtn;
        private System.Windows.Forms.Panel _rdSeparator;
        private System.Windows.Forms.Panel _rdColHeader;
        private System.Windows.Forms.Label _rdColActionType;
        private System.Windows.Forms.Label _rdColMonitor;
        private System.Windows.Forms.Label _rdColSystemNotify;
        private System.Windows.Forms.Label _rdColDiscordNotify;
        private System.Windows.Forms.Label _rdColAutoInterdict;
        private System.Windows.Forms.Panel _rdActionListPanel;
    }
}
