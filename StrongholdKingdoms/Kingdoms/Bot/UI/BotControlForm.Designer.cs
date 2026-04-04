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
                if (_rcRefreshTimer != null)
                {
                    _rcRefreshTimer.Stop();
                    _rcRefreshTimer.Dispose();
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
            this._vsVillageListPanel = new System.Windows.Forms.Panel();
            this._vsColHeader = new System.Windows.Forms.Panel();
            this._vsHdrSync = new System.Windows.Forms.Label();
            this._vsHdrName = new System.Windows.Forms.Label();
            this._vsHdrType = new System.Windows.Forms.Label();
            this._vsHdrId = new System.Windows.Forms.Label();
            this._vsListHeader = new System.Windows.Forms.Label();
            this._vsSeparator = new System.Windows.Forms.Panel();
            this._vsSettingsPanel = new System.Windows.Forms.Panel();
            this._vsButtonBar = new System.Windows.Forms.Panel();
            this._vsRefreshBtn = new System.Windows.Forms.Button();
            this._vsSelectAllBtn = new System.Windows.Forms.Button();
            this._vsDeselectAllBtn = new System.Windows.Forms.Button();
            this._vsSelectVillagesBtn = new System.Windows.Forms.Button();
            this._vsSelectCapitalsBtn = new System.Windows.Forms.Button();
            this._vsDelayInput = new System.Windows.Forms.NumericUpDown();
            this._vsDelayLabel = new System.Windows.Forms.Label();
            this._vsIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._vsIntervalLabel = new System.Windows.Forms.Label();
            this._vsLastRunLabel = new System.Windows.Forms.Label();
            this._vsStatusLabel = new System.Windows.Forms.Label();
            this._vsEnabledCheck = new System.Windows.Forms.CheckBox();
            this._radarPage = new System.Windows.Forms.TabPage();
            this._rdActionListPanel = new System.Windows.Forms.Panel();
            this._rdColHeader = new System.Windows.Forms.Panel();
            this._rdColActionType = new System.Windows.Forms.Label();
            this._rdColMonitor = new System.Windows.Forms.Label();
            this._rdColSystemNotify = new System.Windows.Forms.Label();
            this._rdColDiscordNotify = new System.Windows.Forms.Label();
            this._rdColAutoInterdict = new System.Windows.Forms.Label();
            this._rdSeparator = new System.Windows.Forms.Panel();
            this._rdSettingsPanel = new System.Windows.Forms.Panel();
            this._rdMinArmySizeInput = new System.Windows.Forms.NumericUpDown();
            this._rdMinArmySizeLabel = new System.Windows.Forms.Label();
            this._rdAutoRecruitMonksCheck = new System.Windows.Forms.CheckBox();
            this._rdTestDiscordBtn = new System.Windows.Forms.Button();
            this._rdHintLabel = new System.Windows.Forms.Label();
            this._rdInterdictMonkCountInput = new System.Windows.Forms.NumericUpDown();
            this._rdInterdictLabel = new System.Windows.Forms.Label();
            this._rdWebhookInput = new System.Windows.Forms.TextBox();
            this._rdWebhookLabel = new System.Windows.Forms.Label();
            this._rdScanIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._rdScanIntervalLabel = new System.Windows.Forms.Label();
            this._rdStatusLabel = new System.Windows.Forms.Label();
            this._rdEnabledCheck = new System.Windows.Forms.CheckBox();
            this._recruitingPage = new System.Windows.Forms.TabPage();
            this._rcSubTabs = new System.Windows.Forms.TabControl();
            this._rcVillagesTab = new System.Windows.Forms.TabPage();
            this._rcVillageListPanel = new System.Windows.Forms.Panel();
            this._rcColHeaderVillages = new System.Windows.Forms.Panel();
            this._rcCapitalsTab = new System.Windows.Forms.TabPage();
            this._rcCapitalsListPanel = new System.Windows.Forms.Panel();
            this._rcColHeaderCapitals = new System.Windows.Forms.Panel();
            this._rcVassalsTab = new System.Windows.Forms.TabPage();
            this._vaVassalListPanel = new System.Windows.Forms.Panel();
            this._vaColHeader = new System.Windows.Forms.Panel();
            this._vaColTgt = new System.Windows.Forms.Label();
            this._vaColPri = new System.Windows.Forms.Label();
            this._vaColPeasants = new System.Windows.Forms.Label();
            this._vaColArchers = new System.Windows.Forms.Label();
            this._vaColPikemen = new System.Windows.Forms.Label();
            this._vaColSwordsmen = new System.Windows.Forms.Label();
            this._vaColCatapults = new System.Windows.Forms.Label();
            this._vaSeparator = new System.Windows.Forms.Panel();
            this._vaSettingsPanel = new System.Windows.Forms.Panel();
            this._vaRefreshBtn = new System.Windows.Forms.Button();
            this._vaMinTroopsInput = new System.Windows.Forms.NumericUpDown();
            this._vaMinTroopsLabel = new System.Windows.Forms.Label();
            this._rcSeparator = new System.Windows.Forms.Panel();
            this._rcSettingsPanel = new System.Windows.Forms.Panel();
            this._rcDisbandBtn = new System.Windows.Forms.Button();
            this._rcDisbandCombo = new System.Windows.Forms.ComboBox();
            this._rcRefreshBtn = new System.Windows.Forms.Button();
            this._rcDelayInput = new System.Windows.Forms.NumericUpDown();
            this._rcDelayLabel = new System.Windows.Forms.Label();
            this._rcIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._rcIntervalLabel = new System.Windows.Forms.Label();
            this._rcStatusLabel = new System.Windows.Forms.Label();
            this._rcEnabledCheck = new System.Windows.Forms.CheckBox();
            this._crPage = new System.Windows.Forms.TabPage();
            this._crVillageListPanel = new System.Windows.Forms.Panel();
            this._crColHeader = new System.Windows.Forms.Panel();
            this._crSeparator = new System.Windows.Forms.Panel();
            this._crSettingsPanel = new System.Windows.Forms.Panel();
            this._crRefreshBtn = new System.Windows.Forms.Button();
            this._crCopySettingsBtn = new System.Windows.Forms.Button();
            this._crRepairAllBtn = new System.Windows.Forms.Button();
            this._crRepairOnAttackCheck = new System.Windows.Forms.CheckBox();
            this._crDelayInput = new System.Windows.Forms.NumericUpDown();
            this._crDelayLabel = new System.Windows.Forms.Label();
            this._crIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._crIntervalLabel = new System.Windows.Forms.Label();
            this._crStatusLabel = new System.Windows.Forms.Label();
            this._crEnabledCheck = new System.Windows.Forms.CheckBox();
            this._tradePage = new System.Windows.Forms.TabPage();
            this._trSubTabs = new System.Windows.Forms.TabControl();
            this._trMarketsTab = new System.Windows.Forms.TabPage();
            this._trMarketVillageListPanel = new System.Windows.Forms.Panel();
            this._trAddMarketsBtn = new System.Windows.Forms.Button();
            this._trMarketDistanceInput = new System.Windows.Forms.NumericUpDown();
            this._trMarketDistanceLabel = new System.Windows.Forms.Label();
            this._trMarketRefreshBtn = new System.Windows.Forms.Button();
            this._trRoutesTab = new System.Windows.Forms.TabPage();
            this._trPlayerRoutesTab = new System.Windows.Forms.TabPage();
            this._trPlayerRoutesListPanel = new System.Windows.Forms.Panel();
            this._trRoutesListPanel = new System.Windows.Forms.Panel();
            this._trRefreshRoutesBtn = new System.Windows.Forms.Button();
            this._trDeleteRouteBtn = new System.Windows.Forms.Button();
            this._trAddRouteBtn = new System.Windows.Forms.Button();
            this._trSettingsPanel = new System.Windows.Forms.Panel();
            this._trPrioritiseMarketsCheck = new System.Windows.Forms.CheckBox();
            this._trIgnoreTransactionsCheck = new System.Windows.Forms.CheckBox();
            this._trAutoHireLimitInput = new System.Windows.Forms.NumericUpDown();
            this._trAutoHireLimitLabel = new System.Windows.Forms.Label();
            this._trAutoHireCheck = new System.Windows.Forms.CheckBox();
            this._trExchangeLimitInput = new System.Windows.Forms.NumericUpDown();
            this._trExchangeLimitLabel = new System.Windows.Forms.Label();
            this._trTradeLimitInput = new System.Windows.Forms.NumericUpDown();
            this._trTradeLimitLabel = new System.Windows.Forms.Label();
            this._trMerchantsPerTradeInput = new System.Windows.Forms.NumericUpDown();
            this._trMerchantsPerTradeLabel = new System.Windows.Forms.Label();
            this._trDelayInput = new System.Windows.Forms.NumericUpDown();
            this._trDelayLabel = new System.Windows.Forms.Label();
            this._trIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._trIntervalLabel = new System.Windows.Forms.Label();
            this._trStatusLabel = new System.Windows.Forms.Label();
            this._trEnabledCheck = new System.Windows.Forms.CheckBox();
            this._logPanel = new System.Windows.Forms.Panel();
            this._logBox = new System.Windows.Forms.RichTextBox();
            this._logHeader = new System.Windows.Forms.Label();
            this._trSettingsTab = new System.Windows.Forms.TabPage();
            this._footerPanel.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._mainSplit.Panel1.SuspendLayout();
            this._mainSplit.Panel2.SuspendLayout();
            this._mainSplit.SuspendLayout();
            this._tabControl.SuspendLayout();
            this._villageSyncPage.SuspendLayout();
            this._vsColHeader.SuspendLayout();
            this._vsSettingsPanel.SuspendLayout();
            this._vsButtonBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._vsDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._vsIntervalInput)).BeginInit();
            this._radarPage.SuspendLayout();
            this._rdColHeader.SuspendLayout();
            this._rdSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._rdMinArmySizeInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rdInterdictMonkCountInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rdScanIntervalInput)).BeginInit();
            this._recruitingPage.SuspendLayout();
            this._rcSubTabs.SuspendLayout();
            this._rcVillagesTab.SuspendLayout();
            this._rcCapitalsTab.SuspendLayout();
            this._rcVassalsTab.SuspendLayout();
            this._vaColHeader.SuspendLayout();
            this._vaSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._vaMinTroopsInput)).BeginInit();
            this._rcSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._rcDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rcIntervalInput)).BeginInit();
            this._crPage.SuspendLayout();
            this._crSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._crDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._crIntervalInput)).BeginInit();
            this._tradePage.SuspendLayout();
            this._trSubTabs.SuspendLayout();
            this._trMarketsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._trMarketDistanceInput)).BeginInit();
            this._trRoutesTab.SuspendLayout();
            this._trPlayerRoutesTab.SuspendLayout();
            this._trSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._trAutoHireLimitInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._trExchangeLimitInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._trTradeLimitInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._trMerchantsPerTradeInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._trDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._trIntervalInput)).BeginInit();
            this._logPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _footerPanel
            // 
            this._footerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this._footerPanel.Controls.Add(this._versionLabel);
            this._footerPanel.Controls.Add(this._clearLogBtn);
            this._footerPanel.Controls.Add(this._loadBtn);
            this._footerPanel.Controls.Add(this._saveBtn);
            this._footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footerPanel.Location = new System.Drawing.Point(0, 856);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this._footerPanel.Size = new System.Drawing.Size(1150, 44);
            this._footerPanel.TabIndex = 0;
            // 
            // _versionLabel
            // 
            this._versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._versionLabel.AutoSize = true;
            this._versionLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._versionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._versionLabel.Location = new System.Drawing.Point(954, 14);
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(36, 13);
            this._versionLabel.TabIndex = 3;
            this._versionLabel.Text = "v1.0.0";
            // 
            // _clearLogBtn
            // 
            this._clearLogBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
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
            this._loadBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
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
            this._saveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
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
            this._footerSep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._footerSep.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footerSep.Location = new System.Drawing.Point(0, 855);
            this._footerSep.Name = "_footerSep";
            this._footerSep.Size = new System.Drawing.Size(1150, 1);
            this._footerSep.TabIndex = 1;
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this._headerPanel.Controls.Add(this._masterToggleBtn);
            this._headerPanel.Controls.Add(this._statusLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1150, 56);
            this._headerPanel.TabIndex = 2;
            // 
            // _masterToggleBtn
            // 
            this._masterToggleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._masterToggleBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._masterToggleBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._masterToggleBtn.FlatAppearance.BorderSize = 0;
            this._masterToggleBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._masterToggleBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._masterToggleBtn.ForeColor = System.Drawing.Color.White;
            this._masterToggleBtn.Location = new System.Drawing.Point(864, 10);
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
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
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
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._titleLabel.Location = new System.Drawing.Point(16, 12);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(168, 28);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "PROJECT HADES";
            // 
            // _headerSep
            // 
            this._headerSep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._headerSep.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerSep.Location = new System.Drawing.Point(0, 56);
            this._headerSep.Name = "_headerSep";
            this._headerSep.Size = new System.Drawing.Size(1150, 1);
            this._headerSep.TabIndex = 3;
            // 
            // _mainSplit
            // 
            this._mainSplit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainSplit.Location = new System.Drawing.Point(0, 57);
            this._mainSplit.Name = "_mainSplit";
            this._mainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _mainSplit.Panel1
            // 
            this._mainSplit.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._mainSplit.Panel1.Controls.Add(this._tabControl);
            // 
            // _mainSplit.Panel2
            // 
            this._mainSplit.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._mainSplit.Panel2.Controls.Add(this._logPanel);
            this._mainSplit.Size = new System.Drawing.Size(1150, 798);
            this._mainSplit.SplitterDistance = 525;
            this._mainSplit.SplitterWidth = 3;
            this._mainSplit.TabIndex = 4;
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this._villageSyncPage);
            this._tabControl.Controls.Add(this._radarPage);
            this._tabControl.Controls.Add(this._recruitingPage);
            this._tabControl.Controls.Add(this._crPage);
            this._tabControl.Controls.Add(this._tradePage);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(1150, 525);
            this._tabControl.TabIndex = 0;
            // 
            // _villageSyncPage
            // 
            this._villageSyncPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._villageSyncPage.Controls.Add(this._vsVillageListPanel);
            this._villageSyncPage.Controls.Add(this._vsColHeader);
            this._villageSyncPage.Controls.Add(this._vsListHeader);
            this._villageSyncPage.Controls.Add(this._vsSeparator);
            this._villageSyncPage.Controls.Add(this._vsSettingsPanel);
            this._villageSyncPage.Location = new System.Drawing.Point(4, 24);
            this._villageSyncPage.Name = "_villageSyncPage";
            this._villageSyncPage.Size = new System.Drawing.Size(1142, 497);
            this._villageSyncPage.TabIndex = 0;
            this._villageSyncPage.Text = "Village Sync";
            // 
            // _vsVillageListPanel
            // 
            this._vsVillageListPanel.AutoScroll = true;
            this._vsVillageListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._vsVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vsVillageListPanel.Location = new System.Drawing.Point(0, 149);
            this._vsVillageListPanel.Name = "_vsVillageListPanel";
            this._vsVillageListPanel.Size = new System.Drawing.Size(1142, 348);
            this._vsVillageListPanel.TabIndex = 4;
            // 
            // _vsColHeader
            // 
            this._vsColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this._vsColHeader.Controls.Add(this._vsHdrSync);
            this._vsColHeader.Controls.Add(this._vsHdrName);
            this._vsColHeader.Controls.Add(this._vsHdrType);
            this._vsColHeader.Controls.Add(this._vsHdrId);
            this._vsColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._vsColHeader.Location = new System.Drawing.Point(0, 127);
            this._vsColHeader.Name = "_vsColHeader";
            this._vsColHeader.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this._vsColHeader.Size = new System.Drawing.Size(1142, 22);
            this._vsColHeader.TabIndex = 3;
            // 
            // _vsHdrSync
            // 
            this._vsHdrSync.AutoSize = true;
            this._vsHdrSync.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._vsHdrSync.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vsHdrSync.Location = new System.Drawing.Point(32, 4);
            this._vsHdrSync.Name = "_vsHdrSync";
            this._vsHdrSync.Size = new System.Drawing.Size(27, 12);
            this._vsHdrSync.TabIndex = 0;
            this._vsHdrSync.Text = "Sync";
            // 
            // _vsHdrName
            // 
            this._vsHdrName.AutoSize = true;
            this._vsHdrName.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._vsHdrName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vsHdrName.Location = new System.Drawing.Point(76, 4);
            this._vsHdrName.Name = "_vsHdrName";
            this._vsHdrName.Size = new System.Drawing.Size(67, 12);
            this._vsHdrName.TabIndex = 1;
            this._vsHdrName.Text = "Village Name";
            // 
            // _vsHdrType
            // 
            this._vsHdrType.AutoSize = true;
            this._vsHdrType.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._vsHdrType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vsHdrType.Location = new System.Drawing.Point(376, 4);
            this._vsHdrType.Name = "_vsHdrType";
            this._vsHdrType.Size = new System.Drawing.Size(27, 12);
            this._vsHdrType.TabIndex = 2;
            this._vsHdrType.Text = "Type";
            // 
            // _vsHdrId
            // 
            this._vsHdrId.AutoSize = true;
            this._vsHdrId.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._vsHdrId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vsHdrId.Location = new System.Drawing.Point(476, 4);
            this._vsHdrId.Name = "_vsHdrId";
            this._vsHdrId.Size = new System.Drawing.Size(15, 12);
            this._vsHdrId.TabIndex = 3;
            this._vsHdrId.Text = "ID";
            // 
            // _vsListHeader
            // 
            this._vsListHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._vsListHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._vsListHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._vsListHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vsListHeader.Location = new System.Drawing.Point(0, 101);
            this._vsListHeader.Name = "_vsListHeader";
            this._vsListHeader.Padding = new System.Windows.Forms.Padding(16, 6, 0, 0);
            this._vsListHeader.Size = new System.Drawing.Size(1142, 26);
            this._vsListHeader.TabIndex = 2;
            this._vsListHeader.Text = "VILLAGES";
            // 
            // _vsSeparator
            // 
            this._vsSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._vsSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._vsSeparator.Location = new System.Drawing.Point(0, 100);
            this._vsSeparator.Name = "_vsSeparator";
            this._vsSeparator.Size = new System.Drawing.Size(1142, 1);
            this._vsSeparator.TabIndex = 1;
            // 
            // _vsSettingsPanel
            // 
            this._vsSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
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
            this._vsSettingsPanel.Size = new System.Drawing.Size(1142, 100);
            this._vsSettingsPanel.TabIndex = 0;
            // 
            // _vsButtonBar
            // 
            this._vsButtonBar.Controls.Add(this._vsRefreshBtn);
            this._vsButtonBar.Controls.Add(this._vsSelectAllBtn);
            this._vsButtonBar.Controls.Add(this._vsDeselectAllBtn);
            this._vsButtonBar.Controls.Add(this._vsSelectVillagesBtn);
            this._vsButtonBar.Controls.Add(this._vsSelectCapitalsBtn);
            this._vsButtonBar.Location = new System.Drawing.Point(16, 70);
            this._vsButtonBar.Name = "_vsButtonBar";
            this._vsButtonBar.Size = new System.Drawing.Size(700, 28);
            this._vsButtonBar.TabIndex = 7;
            // 
            // _vsRefreshBtn
            // 
            this._vsRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
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
            this._vsSelectAllBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
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
            this._vsDeselectAllBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
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
            // _vsSelectVillagesBtn
            // 
            this._vsSelectVillagesBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this._vsSelectVillagesBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._vsSelectVillagesBtn.FlatAppearance.BorderSize = 0;
            this._vsSelectVillagesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._vsSelectVillagesBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._vsSelectVillagesBtn.ForeColor = System.Drawing.Color.White;
            this._vsSelectVillagesBtn.Location = new System.Drawing.Point(340, 0);
            this._vsSelectVillagesBtn.Name = "_vsSelectVillagesBtn";
            this._vsSelectVillagesBtn.Size = new System.Drawing.Size(120, 24);
            this._vsSelectVillagesBtn.TabIndex = 3;
            this._vsSelectVillagesBtn.Text = "Villages Only";
            this._vsSelectVillagesBtn.UseVisualStyleBackColor = false;
            // 
            // _vsSelectCapitalsBtn
            // 
            this._vsSelectCapitalsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this._vsSelectCapitalsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._vsSelectCapitalsBtn.FlatAppearance.BorderSize = 0;
            this._vsSelectCapitalsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._vsSelectCapitalsBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._vsSelectCapitalsBtn.ForeColor = System.Drawing.Color.White;
            this._vsSelectCapitalsBtn.Location = new System.Drawing.Point(470, 0);
            this._vsSelectCapitalsBtn.Name = "_vsSelectCapitalsBtn";
            this._vsSelectCapitalsBtn.Size = new System.Drawing.Size(120, 24);
            this._vsSelectCapitalsBtn.TabIndex = 4;
            this._vsSelectCapitalsBtn.Text = "Capitals Only";
            this._vsSelectCapitalsBtn.UseVisualStyleBackColor = false;
            // 
            // _vsDelayInput
            // 
            this._vsDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._vsDelayInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._vsDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._vsDelayInput.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._vsDelayInput.Location = new System.Drawing.Point(498, 42);
            this._vsDelayInput.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this._vsDelayInput.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._vsDelayInput.Name = "_vsDelayInput";
            this._vsDelayInput.Size = new System.Drawing.Size(80, 23);
            this._vsDelayInput.TabIndex = 6;
            this._vsDelayInput.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // _vsDelayLabel
            // 
            this._vsDelayLabel.AutoSize = true;
            this._vsDelayLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._vsDelayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vsDelayLabel.Location = new System.Drawing.Point(306, 56);
            this._vsDelayLabel.Name = "_vsDelayLabel";
            this._vsDelayLabel.Size = new System.Drawing.Size(156, 15);
            this._vsDelayLabel.TabIndex = 5;
            this._vsDelayLabel.Text = "Delay between villages (ms):";
            // 
            // _vsIntervalInput
            // 
            this._vsIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._vsIntervalInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._vsIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._vsIntervalInput.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._vsIntervalInput.Location = new System.Drawing.Point(190, 42);
            this._vsIntervalInput.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this._vsIntervalInput.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._vsIntervalInput.Name = "_vsIntervalInput";
            this._vsIntervalInput.Size = new System.Drawing.Size(80, 23);
            this._vsIntervalInput.TabIndex = 4;
            this._vsIntervalInput.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // _vsIntervalLabel
            // 
            this._vsIntervalLabel.AutoSize = true;
            this._vsIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._vsIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vsIntervalLabel.Location = new System.Drawing.Point(32, 56);
            this._vsIntervalLabel.Name = "_vsIntervalLabel";
            this._vsIntervalLabel.Size = new System.Drawing.Size(135, 15);
            this._vsIntervalLabel.TabIndex = 3;
            this._vsIntervalLabel.Text = "Cycle interval (seconds):";
            // 
            // _vsLastRunLabel
            // 
            this._vsLastRunLabel.AutoSize = true;
            this._vsLastRunLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._vsLastRunLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vsLastRunLabel.Location = new System.Drawing.Point(316, 27);
            this._vsLastRunLabel.Name = "_vsLastRunLabel";
            this._vsLastRunLabel.Size = new System.Drawing.Size(82, 13);
            this._vsLastRunLabel.TabIndex = 2;
            this._vsLastRunLabel.Text = "Last run: never";
            // 
            // _vsStatusLabel
            // 
            this._vsStatusLabel.AutoSize = true;
            this._vsStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._vsStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(200)))), ((int)(((byte)(120)))));
            this._vsStatusLabel.Location = new System.Drawing.Point(216, 27);
            this._vsStatusLabel.Name = "_vsStatusLabel";
            this._vsStatusLabel.Size = new System.Drawing.Size(57, 13);
            this._vsStatusLabel.TabIndex = 1;
            this._vsStatusLabel.Text = "ENABLED";
            // 
            // _vsEnabledCheck
            // 
            this._vsEnabledCheck.AutoSize = true;
            this._vsEnabledCheck.Checked = true;
            this._vsEnabledCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._vsEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._vsEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._vsEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._vsEnabledCheck.Location = new System.Drawing.Point(32, 24);
            this._vsEnabledCheck.Name = "_vsEnabledCheck";
            this._vsEnabledCheck.Size = new System.Drawing.Size(133, 23);
            this._vsEnabledCheck.TabIndex = 0;
            this._vsEnabledCheck.Text = "Module Enabled";
            // 
            // _radarPage
            // 
            this._radarPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._radarPage.Controls.Add(this._rdActionListPanel);
            this._radarPage.Controls.Add(this._rdColHeader);
            this._radarPage.Controls.Add(this._rdSeparator);
            this._radarPage.Controls.Add(this._rdSettingsPanel);
            this._radarPage.Location = new System.Drawing.Point(4, 24);
            this._radarPage.Name = "_radarPage";
            this._radarPage.Size = new System.Drawing.Size(952, 392);
            this._radarPage.TabIndex = 1;
            this._radarPage.Text = "Radar";
            // 
            // _rdActionListPanel
            // 
            this._rdActionListPanel.AutoScroll = true;
            this._rdActionListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._rdActionListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rdActionListPanel.Location = new System.Drawing.Point(0, 175);
            this._rdActionListPanel.Name = "_rdActionListPanel";
            this._rdActionListPanel.Size = new System.Drawing.Size(952, 217);
            this._rdActionListPanel.TabIndex = 3;
            // 
            // _rdColHeader
            // 
            this._rdColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this._rdColHeader.Controls.Add(this._rdColActionType);
            this._rdColHeader.Controls.Add(this._rdColMonitor);
            this._rdColHeader.Controls.Add(this._rdColSystemNotify);
            this._rdColHeader.Controls.Add(this._rdColDiscordNotify);
            this._rdColHeader.Controls.Add(this._rdColAutoInterdict);
            this._rdColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._rdColHeader.Location = new System.Drawing.Point(0, 151);
            this._rdColHeader.Name = "_rdColHeader";
            this._rdColHeader.Size = new System.Drawing.Size(952, 24);
            this._rdColHeader.TabIndex = 2;
            // 
            // _rdColActionType
            // 
            this._rdColActionType.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColActionType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdColActionType.Location = new System.Drawing.Point(16, 2);
            this._rdColActionType.Name = "_rdColActionType";
            this._rdColActionType.Size = new System.Drawing.Size(180, 20);
            this._rdColActionType.TabIndex = 0;
            this._rdColActionType.Text = "Action Type";
            // 
            // _rdColMonitor
            // 
            this._rdColMonitor.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColMonitor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdColMonitor.Location = new System.Drawing.Point(200, 2);
            this._rdColMonitor.Name = "_rdColMonitor";
            this._rdColMonitor.Size = new System.Drawing.Size(60, 20);
            this._rdColMonitor.TabIndex = 1;
            this._rdColMonitor.Text = "Monitor";
            // 
            // _rdColSystemNotify
            // 
            this._rdColSystemNotify.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColSystemNotify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdColSystemNotify.Location = new System.Drawing.Point(270, 2);
            this._rdColSystemNotify.Name = "_rdColSystemNotify";
            this._rdColSystemNotify.Size = new System.Drawing.Size(60, 20);
            this._rdColSystemNotify.TabIndex = 2;
            this._rdColSystemNotify.Text = "System\r\nNotify";
            // 
            // _rdColDiscordNotify
            // 
            this._rdColDiscordNotify.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColDiscordNotify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdColDiscordNotify.Location = new System.Drawing.Point(340, 2);
            this._rdColDiscordNotify.Name = "_rdColDiscordNotify";
            this._rdColDiscordNotify.Size = new System.Drawing.Size(60, 20);
            this._rdColDiscordNotify.TabIndex = 3;
            this._rdColDiscordNotify.Text = "Discord\r\nNotify";
            // 
            // _rdColAutoInterdict
            // 
            this._rdColAutoInterdict.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColAutoInterdict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdColAutoInterdict.Location = new System.Drawing.Point(410, 2);
            this._rdColAutoInterdict.Name = "_rdColAutoInterdict";
            this._rdColAutoInterdict.Size = new System.Drawing.Size(60, 20);
            this._rdColAutoInterdict.TabIndex = 4;
            this._rdColAutoInterdict.Text = "Auto\r\nInterdict";
            // 
            // _rdSeparator
            // 
            this._rdSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._rdSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._rdSeparator.Location = new System.Drawing.Point(0, 150);
            this._rdSeparator.Name = "_rdSeparator";
            this._rdSeparator.Size = new System.Drawing.Size(952, 1);
            this._rdSeparator.TabIndex = 1;
            // 
            // _rdSettingsPanel
            // 
            this._rdSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._rdSettingsPanel.Controls.Add(this._rdMinArmySizeInput);
            this._rdSettingsPanel.Controls.Add(this._rdMinArmySizeLabel);
            this._rdSettingsPanel.Controls.Add(this._rdAutoRecruitMonksCheck);
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
            this._rdSettingsPanel.Size = new System.Drawing.Size(952, 150);
            this._rdSettingsPanel.TabIndex = 0;
            // 
            // _rdMinArmySizeInput
            // 
            this._rdMinArmySizeInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rdMinArmySizeInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdMinArmySizeInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rdMinArmySizeInput.Location = new System.Drawing.Point(530, 70);
            this._rdMinArmySizeInput.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._rdMinArmySizeInput.Name = "_rdMinArmySizeInput";
            this._rdMinArmySizeInput.Size = new System.Drawing.Size(60, 23);
            this._rdMinArmySizeInput.TabIndex = 21;
            this._rdMinArmySizeInput.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // _rdMinArmySizeLabel
            // 
            this._rdMinArmySizeLabel.AutoSize = true;
            this._rdMinArmySizeLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdMinArmySizeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdMinArmySizeLabel.Location = new System.Drawing.Point(416, 84);
            this._rdMinArmySizeLabel.Name = "_rdMinArmySizeLabel";
            this._rdMinArmySizeLabel.Size = new System.Drawing.Size(115, 15);
            this._rdMinArmySizeLabel.TabIndex = 20;
            this._rdMinArmySizeLabel.Text = "Min army size for ID:";
            // 
            // _rdAutoRecruitMonksCheck
            // 
            this._rdAutoRecruitMonksCheck.AutoSize = true;
            this._rdAutoRecruitMonksCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rdAutoRecruitMonksCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdAutoRecruitMonksCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdAutoRecruitMonksCheck.Location = new System.Drawing.Point(236, 84);
            this._rdAutoRecruitMonksCheck.Name = "_rdAutoRecruitMonksCheck";
            this._rdAutoRecruitMonksCheck.Size = new System.Drawing.Size(127, 19);
            this._rdAutoRecruitMonksCheck.TabIndex = 8;
            this._rdAutoRecruitMonksCheck.Text = "Auto-recruit monks";
            // 
            // _rdTestDiscordBtn
            // 
            this._rdTestDiscordBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._rdTestDiscordBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._rdTestDiscordBtn.FlatAppearance.BorderSize = 0;
            this._rdTestDiscordBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rdTestDiscordBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._rdTestDiscordBtn.ForeColor = System.Drawing.Color.White;
            this._rdTestDiscordBtn.Location = new System.Drawing.Point(660, 38);
            this._rdTestDiscordBtn.Name = "_rdTestDiscordBtn";
            this._rdTestDiscordBtn.Size = new System.Drawing.Size(110, 24);
            this._rdTestDiscordBtn.TabIndex = 10;
            this._rdTestDiscordBtn.Text = "Test Webhook";
            this._rdTestDiscordBtn.UseVisualStyleBackColor = false;
            // 
            // _rdHintLabel
            // 
            this._rdHintLabel.AutoSize = true;
            this._rdHintLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);
            this._rdHintLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdHintLabel.Location = new System.Drawing.Point(32, 112);
            this._rdHintLabel.Name = "_rdHintLabel";
            this._rdHintLabel.Size = new System.Drawing.Size(379, 12);
            this._rdHintLabel.TabIndex = 9;
            this._rdHintLabel.Text = "Configure which actions to monitor below. Tick columns to enable per-action featu" +
    "res.";
            // 
            // _rdInterdictMonkCountInput
            // 
            this._rdInterdictMonkCountInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rdInterdictMonkCountInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdInterdictMonkCountInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rdInterdictMonkCountInput.Location = new System.Drawing.Point(150, 70);
            this._rdInterdictMonkCountInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._rdInterdictMonkCountInput.Name = "_rdInterdictMonkCountInput";
            this._rdInterdictMonkCountInput.Size = new System.Drawing.Size(55, 23);
            this._rdInterdictMonkCountInput.TabIndex = 7;
            this._rdInterdictMonkCountInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _rdInterdictLabel
            // 
            this._rdInterdictLabel.AutoSize = true;
            this._rdInterdictLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdInterdictLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdInterdictLabel.Location = new System.Drawing.Point(32, 84);
            this._rdInterdictLabel.Name = "_rdInterdictLabel";
            this._rdInterdictLabel.Size = new System.Drawing.Size(124, 15);
            this._rdInterdictLabel.TabIndex = 6;
            this._rdInterdictLabel.Text = "Auto-interdict monks:";
            // 
            // _rdWebhookInput
            // 
            this._rdWebhookInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rdWebhookInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdWebhookInput.Font = new System.Drawing.Font("Consolas", 8.5F);
            this._rdWebhookInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rdWebhookInput.Location = new System.Drawing.Point(170, 40);
            this._rdWebhookInput.Name = "_rdWebhookInput";
            this._rdWebhookInput.Size = new System.Drawing.Size(480, 21);
            this._rdWebhookInput.TabIndex = 5;
            // 
            // _rdWebhookLabel
            // 
            this._rdWebhookLabel.AutoSize = true;
            this._rdWebhookLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdWebhookLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdWebhookLabel.Location = new System.Drawing.Point(32, 54);
            this._rdWebhookLabel.Name = "_rdWebhookLabel";
            this._rdWebhookLabel.Size = new System.Drawing.Size(128, 15);
            this._rdWebhookLabel.TabIndex = 4;
            this._rdWebhookLabel.Text = "Discord Webhook URL:";
            // 
            // _rdScanIntervalInput
            // 
            this._rdScanIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rdScanIntervalInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdScanIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rdScanIntervalInput.Location = new System.Drawing.Point(370, 11);
            this._rdScanIntervalInput.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this._rdScanIntervalInput.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this._rdScanIntervalInput.Name = "_rdScanIntervalInput";
            this._rdScanIntervalInput.Size = new System.Drawing.Size(60, 23);
            this._rdScanIntervalInput.TabIndex = 3;
            this._rdScanIntervalInput.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _rdScanIntervalLabel
            // 
            this._rdScanIntervalLabel.AutoSize = true;
            this._rdScanIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdScanIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdScanIntervalLabel.Location = new System.Drawing.Point(276, 25);
            this._rdScanIntervalLabel.Name = "_rdScanIntervalLabel";
            this._rdScanIntervalLabel.Size = new System.Drawing.Size(93, 15);
            this._rdScanIntervalLabel.TabIndex = 2;
            this._rdScanIntervalLabel.Text = "Scan interval (s):";
            // 
            // _rdStatusLabel
            // 
            this._rdStatusLabel.AutoSize = true;
            this._rdStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._rdStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(200)))), ((int)(((byte)(120)))));
            this._rdStatusLabel.Location = new System.Drawing.Point(176, 25);
            this._rdStatusLabel.Name = "_rdStatusLabel";
            this._rdStatusLabel.Size = new System.Drawing.Size(57, 13);
            this._rdStatusLabel.TabIndex = 1;
            this._rdStatusLabel.Text = "ENABLED";
            // 
            // _rdEnabledCheck
            // 
            this._rdEnabledCheck.AutoSize = true;
            this._rdEnabledCheck.Checked = true;
            this._rdEnabledCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._rdEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rdEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._rdEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rdEnabledCheck.Location = new System.Drawing.Point(32, 22);
            this._rdEnabledCheck.Name = "_rdEnabledCheck";
            this._rdEnabledCheck.Size = new System.Drawing.Size(133, 23);
            this._rdEnabledCheck.TabIndex = 0;
            this._rdEnabledCheck.Text = "Module Enabled";
            // 
            // _recruitingPage
            // 
            this._recruitingPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._recruitingPage.Controls.Add(this._rcSubTabs);
            this._recruitingPage.Controls.Add(this._rcSeparator);
            this._recruitingPage.Controls.Add(this._rcSettingsPanel);
            this._recruitingPage.Location = new System.Drawing.Point(4, 24);
            this._recruitingPage.Name = "_recruitingPage";
            this._recruitingPage.Size = new System.Drawing.Size(1142, 497);
            this._recruitingPage.TabIndex = 2;
            this._recruitingPage.Text = "Recruiting";
            // 
            // _rcSubTabs
            // 
            this._rcSubTabs.Controls.Add(this._rcVillagesTab);
            this._rcSubTabs.Controls.Add(this._rcCapitalsTab);
            this._rcSubTabs.Controls.Add(this._rcVassalsTab);
            this._rcSubTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rcSubTabs.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._rcSubTabs.Location = new System.Drawing.Point(0, 101);
            this._rcSubTabs.Name = "_rcSubTabs";
            this._rcSubTabs.SelectedIndex = 0;
            this._rcSubTabs.Size = new System.Drawing.Size(1142, 396);
            this._rcSubTabs.TabIndex = 2;
            // 
            // _rcVillagesTab
            // 
            this._rcVillagesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._rcVillagesTab.Controls.Add(this._rcVillageListPanel);
            this._rcVillagesTab.Controls.Add(this._rcColHeaderVillages);
            this._rcVillagesTab.Location = new System.Drawing.Point(4, 22);
            this._rcVillagesTab.Name = "_rcVillagesTab";
            this._rcVillagesTab.Size = new System.Drawing.Size(1134, 370);
            this._rcVillagesTab.TabIndex = 0;
            this._rcVillagesTab.Text = "Villages";
            // 
            // _rcVillageListPanel
            // 
            this._rcVillageListPanel.AutoScroll = true;
            this._rcVillageListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._rcVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rcVillageListPanel.Location = new System.Drawing.Point(0, 24);
            this._rcVillageListPanel.Name = "_rcVillageListPanel";
            this._rcVillageListPanel.Size = new System.Drawing.Size(1134, 346);
            this._rcVillageListPanel.TabIndex = 1;
            // 
            // _rcColHeaderVillages
            // 
            this._rcColHeaderVillages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this._rcColHeaderVillages.Dock = System.Windows.Forms.DockStyle.Top;
            this._rcColHeaderVillages.Location = new System.Drawing.Point(0, 0);
            this._rcColHeaderVillages.Name = "_rcColHeaderVillages";
            this._rcColHeaderVillages.Size = new System.Drawing.Size(1134, 24);
            this._rcColHeaderVillages.TabIndex = 0;
            // 
            // _rcCapitalsTab
            // 
            this._rcCapitalsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._rcCapitalsTab.Controls.Add(this._rcCapitalsListPanel);
            this._rcCapitalsTab.Controls.Add(this._rcColHeaderCapitals);
            this._rcCapitalsTab.Location = new System.Drawing.Point(4, 22);
            this._rcCapitalsTab.Name = "_rcCapitalsTab";
            this._rcCapitalsTab.Size = new System.Drawing.Size(804, 225);
            this._rcCapitalsTab.TabIndex = 1;
            this._rcCapitalsTab.Text = "Capitals";
            // 
            // _rcCapitalsListPanel
            // 
            this._rcCapitalsListPanel.AutoScroll = true;
            this._rcCapitalsListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._rcCapitalsListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rcCapitalsListPanel.Location = new System.Drawing.Point(0, 24);
            this._rcCapitalsListPanel.Name = "_rcCapitalsListPanel";
            this._rcCapitalsListPanel.Size = new System.Drawing.Size(804, 201);
            this._rcCapitalsListPanel.TabIndex = 1;
            // 
            // _rcColHeaderCapitals
            // 
            this._rcColHeaderCapitals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this._rcColHeaderCapitals.Dock = System.Windows.Forms.DockStyle.Top;
            this._rcColHeaderCapitals.Location = new System.Drawing.Point(0, 0);
            this._rcColHeaderCapitals.Name = "_rcColHeaderCapitals";
            this._rcColHeaderCapitals.Size = new System.Drawing.Size(804, 24);
            this._rcColHeaderCapitals.TabIndex = 0;
            // 
            // _rcVassalsTab
            // 
            this._rcVassalsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._rcVassalsTab.Controls.Add(this._vaVassalListPanel);
            this._rcVassalsTab.Controls.Add(this._vaColHeader);
            this._rcVassalsTab.Controls.Add(this._vaSeparator);
            this._rcVassalsTab.Controls.Add(this._vaSettingsPanel);
            this._rcVassalsTab.Location = new System.Drawing.Point(4, 22);
            this._rcVassalsTab.Name = "_rcVassalsTab";
            this._rcVassalsTab.Size = new System.Drawing.Size(804, 225);
            this._rcVassalsTab.TabIndex = 2;
            this._rcVassalsTab.Text = "Vassals";
            // 
            // _vaVassalListPanel
            // 
            this._vaVassalListPanel.AutoScroll = true;
            this._vaVassalListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._vaVassalListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vaVassalListPanel.Location = new System.Drawing.Point(0, 61);
            this._vaVassalListPanel.Name = "_vaVassalListPanel";
            this._vaVassalListPanel.Size = new System.Drawing.Size(804, 164);
            this._vaVassalListPanel.TabIndex = 2;
            // 
            // _vaColHeader
            // 
            this._vaColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this._vaColHeader.Controls.Add(this._vaColTgt);
            this._vaColHeader.Controls.Add(this._vaColPri);
            this._vaColHeader.Controls.Add(this._vaColPeasants);
            this._vaColHeader.Controls.Add(this._vaColArchers);
            this._vaColHeader.Controls.Add(this._vaColPikemen);
            this._vaColHeader.Controls.Add(this._vaColSwordsmen);
            this._vaColHeader.Controls.Add(this._vaColCatapults);
            this._vaColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._vaColHeader.Location = new System.Drawing.Point(0, 37);
            this._vaColHeader.Name = "_vaColHeader";
            this._vaColHeader.Size = new System.Drawing.Size(804, 24);
            this._vaColHeader.TabIndex = 2;
            // 
            // _vaColTgt
            // 
            this._vaColTgt.AutoSize = true;
            this._vaColTgt.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._vaColTgt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vaColTgt.Location = new System.Drawing.Point(8, 5);
            this._vaColTgt.Name = "_vaColTgt";
            this._vaColTgt.Size = new System.Drawing.Size(32, 12);
            this._vaColTgt.TabIndex = 0;
            this._vaColTgt.Text = "Vassal";
            // 
            // _vaColPri
            // 
            this._vaColPri.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._vaColPri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vaColPri.Location = new System.Drawing.Point(170, 5);
            this._vaColPri.Name = "_vaColPri";
            this._vaColPri.Size = new System.Drawing.Size(0, 0);
            this._vaColPri.TabIndex = 1;
            this._vaColPri.Visible = false;
            // 
            // _vaColPeasants
            // 
            this._vaColPeasants.AutoSize = true;
            this._vaColPeasants.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._vaColPeasants.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vaColPeasants.Location = new System.Drawing.Point(200, 5);
            this._vaColPeasants.Name = "_vaColPeasants";
            this._vaColPeasants.Size = new System.Drawing.Size(72, 12);
            this._vaColPeasants.TabIndex = 2;
            this._vaColPeasants.Text = "Peasants  T / P";
            // 
            // _vaColArchers
            // 
            this._vaColArchers.AutoSize = true;
            this._vaColArchers.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._vaColArchers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vaColArchers.Location = new System.Drawing.Point(318, 5);
            this._vaColArchers.Name = "_vaColArchers";
            this._vaColArchers.Size = new System.Drawing.Size(68, 12);
            this._vaColArchers.TabIndex = 3;
            this._vaColArchers.Text = "Archers  T / P";
            // 
            // _vaColPikemen
            // 
            this._vaColPikemen.AutoSize = true;
            this._vaColPikemen.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._vaColPikemen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vaColPikemen.Location = new System.Drawing.Point(436, 5);
            this._vaColPikemen.Name = "_vaColPikemen";
            this._vaColPikemen.Size = new System.Drawing.Size(73, 12);
            this._vaColPikemen.TabIndex = 4;
            this._vaColPikemen.Text = "Pikemen  T / P";
            // 
            // _vaColSwordsmen
            // 
            this._vaColSwordsmen.AutoSize = true;
            this._vaColSwordsmen.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._vaColSwordsmen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vaColSwordsmen.Location = new System.Drawing.Point(554, 5);
            this._vaColSwordsmen.Name = "_vaColSwordsmen";
            this._vaColSwordsmen.Size = new System.Drawing.Size(67, 12);
            this._vaColSwordsmen.TabIndex = 5;
            this._vaColSwordsmen.Text = "Swords  T / P";
            // 
            // _vaColCatapults
            // 
            this._vaColCatapults.AutoSize = true;
            this._vaColCatapults.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._vaColCatapults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vaColCatapults.Location = new System.Drawing.Point(672, 5);
            this._vaColCatapults.Name = "_vaColCatapults";
            this._vaColCatapults.Size = new System.Drawing.Size(76, 12);
            this._vaColCatapults.TabIndex = 6;
            this._vaColCatapults.Text = "Catapults  T / P";
            // 
            // _vaSeparator
            // 
            this._vaSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._vaSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._vaSeparator.Location = new System.Drawing.Point(0, 36);
            this._vaSeparator.Name = "_vaSeparator";
            this._vaSeparator.Size = new System.Drawing.Size(804, 1);
            this._vaSeparator.TabIndex = 1;
            // 
            // _vaSettingsPanel
            // 
            this._vaSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._vaSettingsPanel.Controls.Add(this._vaRefreshBtn);
            this._vaSettingsPanel.Controls.Add(this._vaMinTroopsInput);
            this._vaSettingsPanel.Controls.Add(this._vaMinTroopsLabel);
            this._vaSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._vaSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._vaSettingsPanel.Name = "_vaSettingsPanel";
            this._vaSettingsPanel.Padding = new System.Windows.Forms.Padding(16, 6, 16, 6);
            this._vaSettingsPanel.Size = new System.Drawing.Size(804, 36);
            this._vaSettingsPanel.TabIndex = 0;
            // 
            // _vaRefreshBtn
            // 
            this._vaRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._vaRefreshBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._vaRefreshBtn.FlatAppearance.BorderSize = 0;
            this._vaRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._vaRefreshBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._vaRefreshBtn.ForeColor = System.Drawing.Color.White;
            this._vaRefreshBtn.Location = new System.Drawing.Point(220, 5);
            this._vaRefreshBtn.Name = "_vaRefreshBtn";
            this._vaRefreshBtn.Size = new System.Drawing.Size(100, 24);
            this._vaRefreshBtn.TabIndex = 2;
            this._vaRefreshBtn.Text = "Refresh List";
            this._vaRefreshBtn.UseVisualStyleBackColor = false;
            // 
            // _vaMinTroopsInput
            // 
            this._vaMinTroopsInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._vaMinTroopsInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._vaMinTroopsInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._vaMinTroopsInput.Location = new System.Drawing.Point(140, 6);
            this._vaMinTroopsInput.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._vaMinTroopsInput.Name = "_vaMinTroopsInput";
            this._vaMinTroopsInput.Size = new System.Drawing.Size(60, 22);
            this._vaMinTroopsInput.TabIndex = 1;
            this._vaMinTroopsInput.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // _vaMinTroopsLabel
            // 
            this._vaMinTroopsLabel.AutoSize = true;
            this._vaMinTroopsLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._vaMinTroopsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vaMinTroopsLabel.Location = new System.Drawing.Point(32, 15);
            this._vaMinTroopsLabel.Name = "_vaMinTroopsLabel";
            this._vaMinTroopsLabel.Size = new System.Drawing.Size(113, 15);
            this._vaMinTroopsLabel.TabIndex = 0;
            this._vaMinTroopsLabel.Text = "Min. troops to send:";
            // 
            // _rcSeparator
            // 
            this._rcSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(62)))), ((int)(((byte)(74)))));
            this._rcSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._rcSeparator.Location = new System.Drawing.Point(0, 100);
            this._rcSeparator.Name = "_rcSeparator";
            this._rcSeparator.Size = new System.Drawing.Size(1142, 1);
            this._rcSeparator.TabIndex = 1;
            // 
            // _rcSettingsPanel
            // 
            this._rcSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._rcSettingsPanel.Controls.Add(this._rcDisbandBtn);
            this._rcSettingsPanel.Controls.Add(this._rcDisbandCombo);
            this._rcSettingsPanel.Controls.Add(this._rcRefreshBtn);
            this._rcSettingsPanel.Controls.Add(this._rcDelayInput);
            this._rcSettingsPanel.Controls.Add(this._rcDelayLabel);
            this._rcSettingsPanel.Controls.Add(this._rcIntervalInput);
            this._rcSettingsPanel.Controls.Add(this._rcIntervalLabel);
            this._rcSettingsPanel.Controls.Add(this._rcStatusLabel);
            this._rcSettingsPanel.Controls.Add(this._rcEnabledCheck);
            this._rcSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._rcSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._rcSettingsPanel.Name = "_rcSettingsPanel";
            this._rcSettingsPanel.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this._rcSettingsPanel.Size = new System.Drawing.Size(1142, 100);
            this._rcSettingsPanel.TabIndex = 0;
            // 
            // _rcDisbandBtn
            // 
            this._rcDisbandBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._rcDisbandBtn.FlatAppearance.BorderSize = 0;
            this._rcDisbandBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rcDisbandBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._rcDisbandBtn.ForeColor = System.Drawing.Color.White;
            this._rcDisbandBtn.Location = new System.Drawing.Point(665, 70);
            this._rcDisbandBtn.Name = "_rcDisbandBtn";
            this._rcDisbandBtn.Size = new System.Drawing.Size(70, 24);
            this._rcDisbandBtn.TabIndex = 8;
            this._rcDisbandBtn.Text = "Go";
            this._rcDisbandBtn.UseVisualStyleBackColor = false;
            // 
            // _rcDisbandCombo
            // 
            this._rcDisbandCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rcDisbandCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._rcDisbandCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rcDisbandCombo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._rcDisbandCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rcDisbandCombo.Location = new System.Drawing.Point(498, 70);
            this._rcDisbandCombo.Name = "_rcDisbandCombo";
            this._rcDisbandCombo.Size = new System.Drawing.Size(160, 21);
            this._rcDisbandCombo.TabIndex = 7;
            // 
            // _rcRefreshBtn
            // 
            this._rcRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._rcRefreshBtn.FlatAppearance.BorderSize = 0;
            this._rcRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rcRefreshBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._rcRefreshBtn.ForeColor = System.Drawing.Color.White;
            this._rcRefreshBtn.Location = new System.Drawing.Point(16, 70);
            this._rcRefreshBtn.Name = "_rcRefreshBtn";
            this._rcRefreshBtn.Size = new System.Drawing.Size(100, 24);
            this._rcRefreshBtn.TabIndex = 6;
            this._rcRefreshBtn.Text = "Refresh List";
            this._rcRefreshBtn.UseVisualStyleBackColor = false;
            // 
            // _rcDelayInput
            // 
            this._rcDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rcDelayInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rcDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rcDelayInput.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._rcDelayInput.Location = new System.Drawing.Point(498, 40);
            this._rcDelayInput.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this._rcDelayInput.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._rcDelayInput.Name = "_rcDelayInput";
            this._rcDelayInput.Size = new System.Drawing.Size(80, 23);
            this._rcDelayInput.TabIndex = 5;
            this._rcDelayInput.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // _rcDelayLabel
            // 
            this._rcDelayLabel.AutoSize = true;
            this._rcDelayLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rcDelayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rcDelayLabel.Location = new System.Drawing.Point(290, 42);
            this._rcDelayLabel.Name = "_rcDelayLabel";
            this._rcDelayLabel.Size = new System.Drawing.Size(156, 15);
            this._rcDelayLabel.TabIndex = 4;
            this._rcDelayLabel.Text = "Delay between villages (ms):";
            // 
            // _rcIntervalInput
            // 
            this._rcIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rcIntervalInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rcIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rcIntervalInput.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._rcIntervalInput.Location = new System.Drawing.Point(190, 40);
            this._rcIntervalInput.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this._rcIntervalInput.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._rcIntervalInput.Name = "_rcIntervalInput";
            this._rcIntervalInput.Size = new System.Drawing.Size(80, 23);
            this._rcIntervalInput.TabIndex = 3;
            this._rcIntervalInput.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // _rcIntervalLabel
            // 
            this._rcIntervalLabel.AutoSize = true;
            this._rcIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rcIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rcIntervalLabel.Location = new System.Drawing.Point(16, 42);
            this._rcIntervalLabel.Name = "_rcIntervalLabel";
            this._rcIntervalLabel.Size = new System.Drawing.Size(135, 15);
            this._rcIntervalLabel.TabIndex = 2;
            this._rcIntervalLabel.Text = "Cycle interval (seconds):";
            // 
            // _rcStatusLabel
            // 
            this._rcStatusLabel.AutoSize = true;
            this._rcStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._rcStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(200)))), ((int)(((byte)(120)))));
            this._rcStatusLabel.Location = new System.Drawing.Point(110, 14);
            this._rcStatusLabel.Name = "_rcStatusLabel";
            this._rcStatusLabel.Size = new System.Drawing.Size(59, 15);
            this._rcStatusLabel.TabIndex = 1;
            this._rcStatusLabel.Text = "ENABLED";
            // 
            // _rcEnabledCheck
            // 
            this._rcEnabledCheck.AutoSize = true;
            this._rcEnabledCheck.Checked = true;
            this._rcEnabledCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._rcEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rcEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._rcEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rcEnabledCheck.Location = new System.Drawing.Point(16, 12);
            this._rcEnabledCheck.Name = "_rcEnabledCheck";
            this._rcEnabledCheck.Size = new System.Drawing.Size(133, 23);
            this._rcEnabledCheck.TabIndex = 0;
            this._rcEnabledCheck.Text = "Module Enabled";
            // 
            // _crPage
            // 
            this._crPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._crPage.Controls.Add(this._crVillageListPanel);
            this._crPage.Controls.Add(this._crColHeader);
            this._crPage.Controls.Add(this._crSeparator);
            this._crPage.Controls.Add(this._crSettingsPanel);
            this._crPage.Location = new System.Drawing.Point(4, 24);
            this._crPage.Name = "_crPage";
            this._crPage.Size = new System.Drawing.Size(1142, 497);
            this._crPage.TabIndex = 3;
            this._crPage.Text = "Castle Repair";
            // 
            // _crVillageListPanel
            // 
            this._crVillageListPanel.AutoScroll = true;
            this._crVillageListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._crVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._crVillageListPanel.Location = new System.Drawing.Point(0, 175);
            this._crVillageListPanel.Name = "_crVillageListPanel";
            this._crVillageListPanel.Size = new System.Drawing.Size(1142, 322);
            this._crVillageListPanel.TabIndex = 3;
            // 
            // _crColHeader
            // 
            this._crColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this._crColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._crColHeader.Location = new System.Drawing.Point(0, 151);
            this._crColHeader.Name = "_crColHeader";
            this._crColHeader.Size = new System.Drawing.Size(1142, 24);
            this._crColHeader.TabIndex = 2;
            // 
            // _crSeparator
            // 
            this._crSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._crSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._crSeparator.Location = new System.Drawing.Point(0, 150);
            this._crSeparator.Name = "_crSeparator";
            this._crSeparator.Size = new System.Drawing.Size(1142, 1);
            this._crSeparator.TabIndex = 1;
            // 
            // _crSettingsPanel
            // 
            this._crSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._crSettingsPanel.Controls.Add(this._crCopySettingsBtn);
            this._crSettingsPanel.Controls.Add(this._crRefreshBtn);
            this._crSettingsPanel.Controls.Add(this._crRepairAllBtn);
            this._crSettingsPanel.Controls.Add(this._crRepairOnAttackCheck);
            this._crSettingsPanel.Controls.Add(this._crDelayInput);
            this._crSettingsPanel.Controls.Add(this._crDelayLabel);
            this._crSettingsPanel.Controls.Add(this._crIntervalInput);
            this._crSettingsPanel.Controls.Add(this._crIntervalLabel);
            this._crSettingsPanel.Controls.Add(this._crStatusLabel);
            this._crSettingsPanel.Controls.Add(this._crEnabledCheck);
            this._crSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._crSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._crSettingsPanel.Name = "_crSettingsPanel";
            this._crSettingsPanel.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this._crSettingsPanel.Size = new System.Drawing.Size(1142, 150);
            this._crSettingsPanel.TabIndex = 0;
            // 
            // _crRefreshBtn
            // 
            this._crRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._crRefreshBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._crRefreshBtn.FlatAppearance.BorderSize = 0;
            this._crRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._crRefreshBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._crRefreshBtn.ForeColor = System.Drawing.Color.White;
            this._crRefreshBtn.Location = new System.Drawing.Point(604, 38);
            this._crRefreshBtn.Name = "_crRefreshBtn";
            this._crRefreshBtn.Size = new System.Drawing.Size(100, 24);
            this._crRefreshBtn.TabIndex = 9;
            this._crRefreshBtn.Text = "Refresh List";
            this._crRefreshBtn.UseVisualStyleBackColor = false;
            // 
            // _crCopySettingsBtn
            // 
            this._crCopySettingsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(140)))));
            this._crCopySettingsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._crCopySettingsBtn.FlatAppearance.BorderSize = 0;
            this._crCopySettingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._crCopySettingsBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._crCopySettingsBtn.ForeColor = System.Drawing.Color.White;
            this._crCopySettingsBtn.Location = new System.Drawing.Point(716, 38);
            this._crCopySettingsBtn.Name = "_crCopySettingsBtn";
            this._crCopySettingsBtn.Size = new System.Drawing.Size(100, 24);
            this._crCopySettingsBtn.TabIndex = 10;
            this._crCopySettingsBtn.Text = "Copy Settings";
            this._crCopySettingsBtn.UseVisualStyleBackColor = false;
            // 
            // _crRepairAllBtn
            // 
            this._crRepairAllBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this._crRepairAllBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._crRepairAllBtn.FlatAppearance.BorderSize = 0;
            this._crRepairAllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._crRepairAllBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._crRepairAllBtn.ForeColor = System.Drawing.Color.White;
            this._crRepairAllBtn.Location = new System.Drawing.Point(604, 68);
            this._crRepairAllBtn.Name = "_crRepairAllBtn";
            this._crRepairAllBtn.Size = new System.Drawing.Size(100, 24);
            this._crRepairAllBtn.TabIndex = 8;
            this._crRepairAllBtn.Text = "Repair All";
            this._crRepairAllBtn.UseVisualStyleBackColor = false;
            // 
            // _crRepairOnAttackCheck
            // 
            this._crRepairOnAttackCheck.AutoSize = true;
            this._crRepairOnAttackCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._crRepairOnAttackCheck.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._crRepairOnAttackCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._crRepairOnAttackCheck.Location = new System.Drawing.Point(16, 70);
            this._crRepairOnAttackCheck.Name = "_crRepairOnAttackCheck";
            this._crRepairOnAttackCheck.Size = new System.Drawing.Size(141, 19);
            this._crRepairOnAttackCheck.TabIndex = 7;
            this._crRepairOnAttackCheck.Text = "Repair on Attack/Spy";
            this._crRepairOnAttackCheck.UseVisualStyleBackColor = true;
            // 
            // _crDelayInput
            // 
            this._crDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._crDelayInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._crDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._crDelayInput.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._crDelayInput.Location = new System.Drawing.Point(498, 40);
            this._crDelayInput.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this._crDelayInput.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._crDelayInput.Name = "_crDelayInput";
            this._crDelayInput.Size = new System.Drawing.Size(80, 23);
            this._crDelayInput.TabIndex = 6;
            this._crDelayInput.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // _crDelayLabel
            // 
            this._crDelayLabel.AutoSize = true;
            this._crDelayLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._crDelayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._crDelayLabel.Location = new System.Drawing.Point(290, 42);
            this._crDelayLabel.Name = "_crDelayLabel";
            this._crDelayLabel.Size = new System.Drawing.Size(156, 15);
            this._crDelayLabel.TabIndex = 5;
            this._crDelayLabel.Text = "Delay between villages (ms):";
            // 
            // _crIntervalInput
            // 
            this._crIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._crIntervalInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._crIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._crIntervalInput.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._crIntervalInput.Location = new System.Drawing.Point(190, 40);
            this._crIntervalInput.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this._crIntervalInput.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._crIntervalInput.Name = "_crIntervalInput";
            this._crIntervalInput.Size = new System.Drawing.Size(80, 23);
            this._crIntervalInput.TabIndex = 4;
            this._crIntervalInput.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // _crIntervalLabel
            // 
            this._crIntervalLabel.AutoSize = true;
            this._crIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._crIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._crIntervalLabel.Location = new System.Drawing.Point(16, 42);
            this._crIntervalLabel.Name = "_crIntervalLabel";
            this._crIntervalLabel.Size = new System.Drawing.Size(135, 15);
            this._crIntervalLabel.TabIndex = 2;
            this._crIntervalLabel.Text = "Cycle interval (seconds):";
            // 
            // _crStatusLabel
            // 
            this._crStatusLabel.AutoSize = true;
            this._crStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._crStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(200)))), ((int)(((byte)(120)))));
            this._crStatusLabel.Location = new System.Drawing.Point(200, 13);
            this._crStatusLabel.Name = "_crStatusLabel";
            this._crStatusLabel.Size = new System.Drawing.Size(57, 13);
            this._crStatusLabel.TabIndex = 1;
            this._crStatusLabel.Text = "ENABLED";
            // 
            // _crEnabledCheck
            // 
            this._crEnabledCheck.AutoSize = true;
            this._crEnabledCheck.Checked = true;
            this._crEnabledCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._crEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._crEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._crEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._crEnabledCheck.Location = new System.Drawing.Point(16, 10);
            this._crEnabledCheck.Name = "_crEnabledCheck";
            this._crEnabledCheck.Size = new System.Drawing.Size(133, 23);
            this._crEnabledCheck.TabIndex = 0;
            this._crEnabledCheck.Text = "Module Enabled";
            // 
            // _tradePage
            // 
            this._tradePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._tradePage.Controls.Add(this._trSubTabs);
            this._tradePage.Controls.Add(this._trSettingsPanel);
            this._tradePage.Location = new System.Drawing.Point(4, 24);
            this._tradePage.Name = "_tradePage";
            this._tradePage.Size = new System.Drawing.Size(1142, 497);
            this._tradePage.TabIndex = 4;
            this._tradePage.Text = "Trade";
            // 
            // _trSubTabs
            // 
            this._trSubTabs.Controls.Add(this._trMarketsTab);
            this._trSubTabs.Controls.Add(this._trRoutesTab);
            this._trSubTabs.Controls.Add(this._trPlayerRoutesTab);
            this._trSubTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._trSubTabs.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._trSubTabs.Location = new System.Drawing.Point(0, 100);
            this._trSubTabs.Name = "_trSubTabs";
            this._trSubTabs.SelectedIndex = 0;
            this._trSubTabs.Size = new System.Drawing.Size(1142, 397);
            this._trSubTabs.TabIndex = 1;
            // 
            // _trMarketsTab
            // 
            this._trMarketsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._trMarketsTab.Controls.Add(this._trMarketVillageListPanel);
            this._trMarketsTab.Controls.Add(this._trAddMarketsBtn);
            this._trMarketsTab.Controls.Add(this._trMarketDistanceInput);
            this._trMarketsTab.Controls.Add(this._trMarketDistanceLabel);
            this._trMarketsTab.Controls.Add(this._trMarketRefreshBtn);
            this._trMarketsTab.Location = new System.Drawing.Point(4, 22);
            this._trMarketsTab.Name = "_trMarketsTab";
            this._trMarketsTab.Size = new System.Drawing.Size(1134, 371);
            this._trMarketsTab.TabIndex = 0;
            this._trMarketsTab.Text = "Markets";
            // 
            // _trMarketVillageListPanel
            // 
            this._trMarketVillageListPanel.AutoScroll = true;
            this._trMarketVillageListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._trMarketVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._trMarketVillageListPanel.Location = new System.Drawing.Point(0, 0);
            this._trMarketVillageListPanel.Name = "_trMarketVillageListPanel";
            this._trMarketVillageListPanel.Size = new System.Drawing.Size(1134, 371);
            this._trMarketVillageListPanel.TabIndex = 4;
            // 
            // _trAddMarketsBtn
            // 
            this._trAddMarketsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this._trAddMarketsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._trAddMarketsBtn.FlatAppearance.BorderSize = 0;
            this._trAddMarketsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trAddMarketsBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._trAddMarketsBtn.ForeColor = System.Drawing.Color.White;
            this._trAddMarketsBtn.Location = new System.Drawing.Point(295, 6);
            this._trAddMarketsBtn.Name = "_trAddMarketsBtn";
            this._trAddMarketsBtn.Size = new System.Drawing.Size(110, 24);
            this._trAddMarketsBtn.TabIndex = 3;
            this._trAddMarketsBtn.Text = "Find Markets";
            this._trAddMarketsBtn.UseVisualStyleBackColor = false;
            // 
            // _trMarketDistanceInput
            // 
            this._trMarketDistanceInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._trMarketDistanceInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._trMarketDistanceInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trMarketDistanceInput.Location = new System.Drawing.Point(225, 8);
            this._trMarketDistanceInput.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._trMarketDistanceInput.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._trMarketDistanceInput.Name = "_trMarketDistanceInput";
            this._trMarketDistanceInput.Size = new System.Drawing.Size(60, 22);
            this._trMarketDistanceInput.TabIndex = 2;
            this._trMarketDistanceInput.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // _trMarketDistanceLabel
            // 
            this._trMarketDistanceLabel.AutoSize = true;
            this._trMarketDistanceLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trMarketDistanceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._trMarketDistanceLabel.Location = new System.Drawing.Point(130, 10);
            this._trMarketDistanceLabel.Name = "_trMarketDistanceLabel";
            this._trMarketDistanceLabel.Size = new System.Drawing.Size(94, 15);
            this._trMarketDistanceLabel.TabIndex = 1;
            this._trMarketDistanceLabel.Text = "Market distance:";
            // 
            // _trMarketRefreshBtn
            // 
            this._trMarketRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._trMarketRefreshBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._trMarketRefreshBtn.FlatAppearance.BorderSize = 0;
            this._trMarketRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trMarketRefreshBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._trMarketRefreshBtn.ForeColor = System.Drawing.Color.White;
            this._trMarketRefreshBtn.Location = new System.Drawing.Point(8, 6);
            this._trMarketRefreshBtn.Name = "_trMarketRefreshBtn";
            this._trMarketRefreshBtn.Size = new System.Drawing.Size(110, 24);
            this._trMarketRefreshBtn.TabIndex = 0;
            this._trMarketRefreshBtn.Text = "Refresh Villages";
            this._trMarketRefreshBtn.UseVisualStyleBackColor = false;
            // 
            // _trRoutesTab
            // 
            this._trRoutesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._trRoutesTab.Controls.Add(this._trRoutesListPanel);
            this._trRoutesTab.Controls.Add(this._trRefreshRoutesBtn);
            this._trRoutesTab.Controls.Add(this._trDeleteRouteBtn);
            this._trRoutesTab.Controls.Add(this._trAddRouteBtn);
            this._trRoutesTab.Location = new System.Drawing.Point(4, 22);
            this._trRoutesTab.Name = "_trRoutesTab";
            this._trRoutesTab.Size = new System.Drawing.Size(944, 266);
            this._trRoutesTab.TabIndex = 1;
            this._trRoutesTab.Text = "Village Routes";
            // 
            // _trPlayerRoutesTab
            // 
            this._trPlayerRoutesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._trPlayerRoutesTab.Controls.Add(this._trPlayerRoutesListPanel);
            this._trPlayerRoutesTab.Location = new System.Drawing.Point(4, 22);
            this._trPlayerRoutesTab.Name = "_trPlayerRoutesTab";
            this._trPlayerRoutesTab.Size = new System.Drawing.Size(944, 266);
            this._trPlayerRoutesTab.TabIndex = 2;
            this._trPlayerRoutesTab.Text = "Player Routes";
            // 
            // _trPlayerRoutesListPanel
            // 
            this._trPlayerRoutesListPanel.AutoScroll = true;
            this._trPlayerRoutesListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._trPlayerRoutesListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._trPlayerRoutesListPanel.Location = new System.Drawing.Point(0, 0);
            this._trPlayerRoutesListPanel.Name = "_trPlayerRoutesListPanel";
            this._trPlayerRoutesListPanel.Size = new System.Drawing.Size(944, 266);
            this._trPlayerRoutesListPanel.TabIndex = 0;
            // 
            // _trRoutesListPanel
            // 
            this._trRoutesListPanel.AutoScroll = true;
            this._trRoutesListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._trRoutesListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._trRoutesListPanel.Location = new System.Drawing.Point(0, 0);
            this._trRoutesListPanel.Name = "_trRoutesListPanel";
            this._trRoutesListPanel.Size = new System.Drawing.Size(944, 266);
            this._trRoutesListPanel.TabIndex = 3;
            // 
            // _trRefreshRoutesBtn
            // 
            this._trRefreshRoutesBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this._trRefreshRoutesBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._trRefreshRoutesBtn.FlatAppearance.BorderSize = 0;
            this._trRefreshRoutesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trRefreshRoutesBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._trRefreshRoutesBtn.ForeColor = System.Drawing.Color.White;
            this._trRefreshRoutesBtn.Location = new System.Drawing.Point(238, 4);
            this._trRefreshRoutesBtn.Name = "_trRefreshRoutesBtn";
            this._trRefreshRoutesBtn.Size = new System.Drawing.Size(100, 24);
            this._trRefreshRoutesBtn.TabIndex = 2;
            this._trRefreshRoutesBtn.Text = "Refresh";
            this._trRefreshRoutesBtn.UseVisualStyleBackColor = false;
            // 
            // _trDeleteRouteBtn
            // 
            this._trDeleteRouteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._trDeleteRouteBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._trDeleteRouteBtn.FlatAppearance.BorderSize = 0;
            this._trDeleteRouteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trDeleteRouteBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._trDeleteRouteBtn.ForeColor = System.Drawing.Color.White;
            this._trDeleteRouteBtn.Location = new System.Drawing.Point(118, 4);
            this._trDeleteRouteBtn.Name = "_trDeleteRouteBtn";
            this._trDeleteRouteBtn.Size = new System.Drawing.Size(110, 24);
            this._trDeleteRouteBtn.TabIndex = 1;
            this._trDeleteRouteBtn.Text = "Delete Route";
            this._trDeleteRouteBtn.UseVisualStyleBackColor = false;
            // 
            // _trAddRouteBtn
            // 
            this._trAddRouteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._trAddRouteBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._trAddRouteBtn.FlatAppearance.BorderSize = 0;
            this._trAddRouteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trAddRouteBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._trAddRouteBtn.ForeColor = System.Drawing.Color.White;
            this._trAddRouteBtn.Location = new System.Drawing.Point(8, 4);
            this._trAddRouteBtn.Name = "_trAddRouteBtn";
            this._trAddRouteBtn.Size = new System.Drawing.Size(100, 24);
            this._trAddRouteBtn.TabIndex = 0;
            this._trAddRouteBtn.Text = "Add Route";
            this._trAddRouteBtn.UseVisualStyleBackColor = false;
            // 
            // _trSettingsPanel
            // 
            this._trSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._trSettingsPanel.Controls.Add(this._trPrioritiseMarketsCheck);
            this._trSettingsPanel.Controls.Add(this._trIgnoreTransactionsCheck);
            this._trSettingsPanel.Controls.Add(this._trAutoHireLimitInput);
            this._trSettingsPanel.Controls.Add(this._trAutoHireLimitLabel);
            this._trSettingsPanel.Controls.Add(this._trAutoHireCheck);
            this._trSettingsPanel.Controls.Add(this._trExchangeLimitInput);
            this._trSettingsPanel.Controls.Add(this._trExchangeLimitLabel);
            this._trSettingsPanel.Controls.Add(this._trTradeLimitInput);
            this._trSettingsPanel.Controls.Add(this._trTradeLimitLabel);
            this._trSettingsPanel.Controls.Add(this._trMerchantsPerTradeInput);
            this._trSettingsPanel.Controls.Add(this._trMerchantsPerTradeLabel);
            this._trSettingsPanel.Controls.Add(this._trDelayInput);
            this._trSettingsPanel.Controls.Add(this._trDelayLabel);
            this._trSettingsPanel.Controls.Add(this._trIntervalInput);
            this._trSettingsPanel.Controls.Add(this._trIntervalLabel);
            this._trSettingsPanel.Controls.Add(this._trStatusLabel);
            this._trSettingsPanel.Controls.Add(this._trEnabledCheck);
            this._trSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._trSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._trSettingsPanel.Name = "_trSettingsPanel";
            this._trSettingsPanel.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this._trSettingsPanel.Size = new System.Drawing.Size(1142, 100);
            this._trSettingsPanel.TabIndex = 0;
            // 
            // _trPrioritiseMarketsCheck
            // 
            this._trPrioritiseMarketsCheck.AutoSize = true;
            this._trPrioritiseMarketsCheck.Checked = true;
            this._trPrioritiseMarketsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._trPrioritiseMarketsCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trPrioritiseMarketsCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trPrioritiseMarketsCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trPrioritiseMarketsCheck.Location = new System.Drawing.Point(500, 70);
            this._trPrioritiseMarketsCheck.Name = "_trPrioritiseMarketsCheck";
            this._trPrioritiseMarketsCheck.Size = new System.Drawing.Size(114, 19);
            this._trPrioritiseMarketsCheck.TabIndex = 16;
            this._trPrioritiseMarketsCheck.Text = "Prioritise Markets";
            // 
            // _trIgnoreTransactionsCheck
            // 
            this._trIgnoreTransactionsCheck.AutoSize = true;
            this._trIgnoreTransactionsCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trIgnoreTransactionsCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trIgnoreTransactionsCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trIgnoreTransactionsCheck.Location = new System.Drawing.Point(310, 70);
            this._trIgnoreTransactionsCheck.Name = "_trIgnoreTransactionsCheck";
            this._trIgnoreTransactionsCheck.Size = new System.Drawing.Size(165, 19);
            this._trIgnoreTransactionsCheck.TabIndex = 15;
            this._trIgnoreTransactionsCheck.Text = "Ignore current transactions";
            // 
            // _trAutoHireLimitInput
            // 
            this._trAutoHireLimitInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._trAutoHireLimitInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._trAutoHireLimitInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trAutoHireLimitInput.Location = new System.Drawing.Point(235, 70);
            this._trAutoHireLimitInput.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._trAutoHireLimitInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._trAutoHireLimitInput.Name = "_trAutoHireLimitInput";
            this._trAutoHireLimitInput.Size = new System.Drawing.Size(50, 23);
            this._trAutoHireLimitInput.TabIndex = 14;
            this._trAutoHireLimitInput.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // _trAutoHireLimitLabel
            // 
            this._trAutoHireLimitLabel.AutoSize = true;
            this._trAutoHireLimitLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trAutoHireLimitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._trAutoHireLimitLabel.Location = new System.Drawing.Point(170, 72);
            this._trAutoHireLimitLabel.Name = "_trAutoHireLimitLabel";
            this._trAutoHireLimitLabel.Size = new System.Drawing.Size(59, 15);
            this._trAutoHireLimitLabel.TabIndex = 13;
            this._trAutoHireLimitLabel.Text = "Hire limit:";
            // 
            // _trAutoHireCheck
            // 
            this._trAutoHireCheck.AutoSize = true;
            this._trAutoHireCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trAutoHireCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trAutoHireCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trAutoHireCheck.Location = new System.Drawing.Point(16, 70);
            this._trAutoHireCheck.Name = "_trAutoHireCheck";
            this._trAutoHireCheck.Size = new System.Drawing.Size(133, 19);
            this._trAutoHireCheck.TabIndex = 12;
            this._trAutoHireCheck.Text = "Auto-hire merchants";
            // 
            // _trExchangeLimitInput
            // 
            this._trExchangeLimitInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._trExchangeLimitInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._trExchangeLimitInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trExchangeLimitInput.Location = new System.Drawing.Point(735, 40);
            this._trExchangeLimitInput.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._trExchangeLimitInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._trExchangeLimitInput.Name = "_trExchangeLimitInput";
            this._trExchangeLimitInput.Size = new System.Drawing.Size(50, 23);
            this._trExchangeLimitInput.TabIndex = 11;
            this._trExchangeLimitInput.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // _trExchangeLimitLabel
            // 
            this._trExchangeLimitLabel.AutoSize = true;
            this._trExchangeLimitLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trExchangeLimitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._trExchangeLimitLabel.Location = new System.Drawing.Point(645, 42);
            this._trExchangeLimitLabel.Name = "_trExchangeLimitLabel";
            this._trExchangeLimitLabel.Size = new System.Drawing.Size(88, 15);
            this._trExchangeLimitLabel.TabIndex = 10;
            this._trExchangeLimitLabel.Text = "Exchange limit:";
            // 
            // _trTradeLimitInput
            // 
            this._trTradeLimitInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._trTradeLimitInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._trTradeLimitInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trTradeLimitInput.Location = new System.Drawing.Point(585, 40);
            this._trTradeLimitInput.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._trTradeLimitInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._trTradeLimitInput.Name = "_trTradeLimitInput";
            this._trTradeLimitInput.Size = new System.Drawing.Size(50, 23);
            this._trTradeLimitInput.TabIndex = 9;
            this._trTradeLimitInput.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // _trTradeLimitLabel
            // 
            this._trTradeLimitLabel.AutoSize = true;
            this._trTradeLimitLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trTradeLimitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._trTradeLimitLabel.Location = new System.Drawing.Point(500, 42);
            this._trTradeLimitLabel.Name = "_trTradeLimitLabel";
            this._trTradeLimitLabel.Size = new System.Drawing.Size(74, 15);
            this._trTradeLimitLabel.TabIndex = 8;
            this._trTradeLimitLabel.Text = "Market limit:";
            // 
            // _trMerchantsPerTradeInput
            // 
            this._trMerchantsPerTradeInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._trMerchantsPerTradeInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._trMerchantsPerTradeInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trMerchantsPerTradeInput.Location = new System.Drawing.Point(440, 40);
            this._trMerchantsPerTradeInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._trMerchantsPerTradeInput.Name = "_trMerchantsPerTradeInput";
            this._trMerchantsPerTradeInput.Size = new System.Drawing.Size(50, 23);
            this._trMerchantsPerTradeInput.TabIndex = 7;
            this._trMerchantsPerTradeInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _trMerchantsPerTradeLabel
            // 
            this._trMerchantsPerTradeLabel.AutoSize = true;
            this._trMerchantsPerTradeLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trMerchantsPerTradeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._trMerchantsPerTradeLabel.Location = new System.Drawing.Point(345, 42);
            this._trMerchantsPerTradeLabel.Name = "_trMerchantsPerTradeLabel";
            this._trMerchantsPerTradeLabel.Size = new System.Drawing.Size(122, 15);
            this._trMerchantsPerTradeLabel.TabIndex = 6;
            this._trMerchantsPerTradeLabel.Text = "Min merchants/trade:";
            // 
            // _trDelayInput
            // 
            this._trDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._trDelayInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._trDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trDelayInput.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._trDelayInput.Location = new System.Drawing.Point(275, 40);
            this._trDelayInput.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this._trDelayInput.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._trDelayInput.Name = "_trDelayInput";
            this._trDelayInput.Size = new System.Drawing.Size(60, 23);
            this._trDelayInput.TabIndex = 5;
            this._trDelayInput.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // _trDelayLabel
            // 
            this._trDelayLabel.AutoSize = true;
            this._trDelayLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trDelayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._trDelayLabel.Location = new System.Drawing.Point(200, 42);
            this._trDelayLabel.Name = "_trDelayLabel";
            this._trDelayLabel.Size = new System.Drawing.Size(66, 15);
            this._trDelayLabel.TabIndex = 4;
            this._trDelayLabel.Text = "Delay (ms):";
            // 
            // _trIntervalInput
            // 
            this._trIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._trIntervalInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._trIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trIntervalInput.Location = new System.Drawing.Point(130, 40);
            this._trIntervalInput.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this._trIntervalInput.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._trIntervalInput.Name = "_trIntervalInput";
            this._trIntervalInput.Size = new System.Drawing.Size(60, 23);
            this._trIntervalInput.TabIndex = 3;
            this._trIntervalInput.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // _trIntervalLabel
            // 
            this._trIntervalLabel.AutoSize = true;
            this._trIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._trIntervalLabel.Location = new System.Drawing.Point(16, 42);
            this._trIntervalLabel.Name = "_trIntervalLabel";
            this._trIntervalLabel.Size = new System.Drawing.Size(97, 15);
            this._trIntervalLabel.TabIndex = 2;
            this._trIntervalLabel.Text = "Cycle interval (s):";
            // 
            // _trStatusLabel
            // 
            this._trStatusLabel.AutoSize = true;
            this._trStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._trStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(200)))), ((int)(((byte)(120)))));
            this._trStatusLabel.Location = new System.Drawing.Point(200, 13);
            this._trStatusLabel.Name = "_trStatusLabel";
            this._trStatusLabel.Size = new System.Drawing.Size(57, 13);
            this._trStatusLabel.TabIndex = 1;
            this._trStatusLabel.Text = "ENABLED";
            // 
            // _trEnabledCheck
            // 
            this._trEnabledCheck.AutoSize = true;
            this._trEnabledCheck.Checked = true;
            this._trEnabledCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._trEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._trEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trEnabledCheck.Location = new System.Drawing.Point(16, 10);
            this._trEnabledCheck.Name = "_trEnabledCheck";
            this._trEnabledCheck.Size = new System.Drawing.Size(133, 23);
            this._trEnabledCheck.TabIndex = 0;
            this._trEnabledCheck.Text = "Module Enabled";
            // 
            // _logPanel
            // 
            this._logPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._logPanel.Controls.Add(this._logBox);
            this._logPanel.Controls.Add(this._logHeader);
            this._logPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logPanel.Location = new System.Drawing.Point(0, 0);
            this._logPanel.Name = "_logPanel";
            this._logPanel.Size = new System.Drawing.Size(1150, 270);
            this._logPanel.TabIndex = 0;
            // 
            // _logBox
            // 
            this._logBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(24)))));
            this._logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logBox.Font = new System.Drawing.Font("Consolas", 8.5F);
            this._logBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this._logBox.Location = new System.Drawing.Point(0, 24);
            this._logBox.Name = "_logBox";
            this._logBox.ReadOnly = true;
            this._logBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._logBox.Size = new System.Drawing.Size(1150, 246);
            this._logBox.TabIndex = 1;
            this._logBox.Text = "";
            // 
            // _logHeader
            // 
            this._logHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this._logHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._logHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._logHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._logHeader.Location = new System.Drawing.Point(0, 0);
            this._logHeader.Name = "_logHeader";
            this._logHeader.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this._logHeader.Size = new System.Drawing.Size(1150, 24);
            this._logHeader.TabIndex = 0;
            this._logHeader.Text = "Log Output";
            this._logHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _trSettingsTab
            // 
            this._trSettingsTab.Location = new System.Drawing.Point(0, 0);
            this._trSettingsTab.Name = "_trSettingsTab";
            this._trSettingsTab.Size = new System.Drawing.Size(200, 100);
            this._trSettingsTab.TabIndex = 0;
            // 
            // BotControlForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1150, 900);
            this.Controls.Add(this._mainSplit);
            this.Controls.Add(this._headerSep);
            this.Controls.Add(this._headerPanel);
            this.Controls.Add(this._footerSep);
            this.Controls.Add(this._footerPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
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
            this._vsColHeader.ResumeLayout(false);
            this._vsColHeader.PerformLayout();
            this._vsSettingsPanel.ResumeLayout(false);
            this._vsSettingsPanel.PerformLayout();
            this._vsButtonBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._vsDelayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._vsIntervalInput)).EndInit();
            this._radarPage.ResumeLayout(false);
            this._rdColHeader.ResumeLayout(false);
            this._rdSettingsPanel.ResumeLayout(false);
            this._rdSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._rdMinArmySizeInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rdInterdictMonkCountInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rdScanIntervalInput)).EndInit();
            this._recruitingPage.ResumeLayout(false);
            this._rcSubTabs.ResumeLayout(false);
            this._rcVillagesTab.ResumeLayout(false);
            this._rcCapitalsTab.ResumeLayout(false);
            this._rcVassalsTab.ResumeLayout(false);
            this._vaColHeader.ResumeLayout(false);
            this._vaColHeader.PerformLayout();
            this._vaSettingsPanel.ResumeLayout(false);
            this._vaSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._vaMinTroopsInput)).EndInit();
            this._rcSettingsPanel.ResumeLayout(false);
            this._rcSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._rcDelayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rcIntervalInput)).EndInit();
            this._crPage.ResumeLayout(false);
            this._crSettingsPanel.ResumeLayout(false);
            this._crSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._crDelayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._crIntervalInput)).EndInit();
            this._tradePage.ResumeLayout(false);
            this._trSubTabs.ResumeLayout(false);
            this._trMarketsTab.ResumeLayout(false);
            this._trMarketsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._trMarketDistanceInput)).EndInit();
            this._trRoutesTab.ResumeLayout(false);
            this._trPlayerRoutesTab.ResumeLayout(false);
            this._trSettingsPanel.ResumeLayout(false);
            this._trSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._trAutoHireLimitInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._trExchangeLimitInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._trTradeLimitInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._trMerchantsPerTradeInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._trDelayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._trIntervalInput)).EndInit();
            this._logPanel.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage _recruitingPage;
        private System.Windows.Forms.TabPage _crPage;
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
        private System.Windows.Forms.Button _vsSelectVillagesBtn;
        private System.Windows.Forms.Button _vsSelectCapitalsBtn;
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
        private System.Windows.Forms.CheckBox _rdAutoRecruitMonksCheck;
        private System.Windows.Forms.Label _rdMinArmySizeLabel;
        private System.Windows.Forms.NumericUpDown _rdMinArmySizeInput;
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
        // Recruiting tab controls
        private System.Windows.Forms.Panel _rcSettingsPanel;
        private System.Windows.Forms.CheckBox _rcEnabledCheck;
        private System.Windows.Forms.Label _rcStatusLabel;
        private System.Windows.Forms.Label _rcIntervalLabel;
        private System.Windows.Forms.NumericUpDown _rcIntervalInput;
        private System.Windows.Forms.Label _rcDelayLabel;
        private System.Windows.Forms.NumericUpDown _rcDelayInput;
        private System.Windows.Forms.Button _rcRefreshBtn;
        private System.Windows.Forms.ComboBox _rcDisbandCombo;
        private System.Windows.Forms.Button _rcDisbandBtn;
        private System.Windows.Forms.Panel _rcSeparator;
        private System.Windows.Forms.TabControl _rcSubTabs;
        private System.Windows.Forms.TabPage _rcVillagesTab;
        private System.Windows.Forms.TabPage _rcCapitalsTab;
        private System.Windows.Forms.TabPage _rcVassalsTab;
        private System.Windows.Forms.Panel _rcColHeaderVillages;
        private System.Windows.Forms.Panel _rcColHeaderCapitals;
        private System.Windows.Forms.Panel _rcVillageListPanel;
        private System.Windows.Forms.Panel _rcCapitalsListPanel;
        // Vassals sub-tab controls
        private System.Windows.Forms.Panel _vaSettingsPanel;
        private System.Windows.Forms.Label _vaMinTroopsLabel;
        private System.Windows.Forms.NumericUpDown _vaMinTroopsInput;
        private System.Windows.Forms.Button _vaRefreshBtn;
        private System.Windows.Forms.Panel _vaSeparator;
        private System.Windows.Forms.Panel _vaColHeader;
        private System.Windows.Forms.Label _vaColTgt;
        private System.Windows.Forms.Label _vaColPri;
        private System.Windows.Forms.Label _vaColPeasants;
        private System.Windows.Forms.Label _vaColArchers;
        private System.Windows.Forms.Label _vaColPikemen;
        private System.Windows.Forms.Label _vaColSwordsmen;
        private System.Windows.Forms.Label _vaColCatapults;
        private System.Windows.Forms.Panel _vaVassalListPanel;
        // Castle Repair tab controls
        private System.Windows.Forms.Panel _crSettingsPanel;
        private System.Windows.Forms.CheckBox _crEnabledCheck;
        private System.Windows.Forms.Label _crStatusLabel;
        private System.Windows.Forms.Label _crIntervalLabel;
        private System.Windows.Forms.NumericUpDown _crIntervalInput;
        private System.Windows.Forms.Label _crDelayLabel;
        private System.Windows.Forms.NumericUpDown _crDelayInput;
        private System.Windows.Forms.CheckBox _crRepairOnAttackCheck;
        private System.Windows.Forms.Button _crRepairAllBtn;
        private System.Windows.Forms.Button _crRefreshBtn;
        private System.Windows.Forms.Button _crCopySettingsBtn;
        private System.Windows.Forms.Panel _crSeparator;
        private System.Windows.Forms.Panel _crColHeader;
        private System.Windows.Forms.Panel _crVillageListPanel;
        // Trade tab controls
        private System.Windows.Forms.TabPage _tradePage;
        private System.Windows.Forms.TabControl _trSubTabs;
        private System.Windows.Forms.TabPage _trMarketsTab;
        private System.Windows.Forms.TabPage _trRoutesTab;
        private System.Windows.Forms.TabPage _trPlayerRoutesTab;
        private System.Windows.Forms.Panel _trPlayerRoutesListPanel;
        private System.Windows.Forms.TabPage _trSettingsTab;
        private System.Windows.Forms.Panel _trSettingsPanel;
        private System.Windows.Forms.CheckBox _trEnabledCheck;
        private System.Windows.Forms.Label _trStatusLabel;
        private System.Windows.Forms.Label _trIntervalLabel;
        private System.Windows.Forms.NumericUpDown _trIntervalInput;
        private System.Windows.Forms.Label _trDelayLabel;
        private System.Windows.Forms.NumericUpDown _trDelayInput;
        private System.Windows.Forms.Label _trMerchantsPerTradeLabel;
        private System.Windows.Forms.NumericUpDown _trMerchantsPerTradeInput;
        private System.Windows.Forms.Label _trTradeLimitLabel;
        private System.Windows.Forms.NumericUpDown _trTradeLimitInput;
        private System.Windows.Forms.Label _trExchangeLimitLabel;
        private System.Windows.Forms.NumericUpDown _trExchangeLimitInput;
        private System.Windows.Forms.CheckBox _trAutoHireCheck;
        private System.Windows.Forms.Label _trAutoHireLimitLabel;
        private System.Windows.Forms.NumericUpDown _trAutoHireLimitInput;
        private System.Windows.Forms.CheckBox _trIgnoreTransactionsCheck;
        private System.Windows.Forms.CheckBox _trPrioritiseMarketsCheck;
        private System.Windows.Forms.Panel _trMarketVillageListPanel;
        private System.Windows.Forms.Button _trMarketRefreshBtn;
        private System.Windows.Forms.Button _trAddMarketsBtn;
        private System.Windows.Forms.Label _trMarketDistanceLabel;
        private System.Windows.Forms.NumericUpDown _trMarketDistanceInput;
        private System.Windows.Forms.Panel _trRoutesListPanel;
        private System.Windows.Forms.Button _trAddRouteBtn;
        private System.Windows.Forms.Button _trDeleteRouteBtn;
        private System.Windows.Forms.Button _trRefreshRoutesBtn;
    }
}
