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
                if (_ppRefreshTimer != null)
                {
                    _ppRefreshTimer.Stop();
                    _ppRefreshTimer.Dispose();
                }
                if (_autoRefreshTimer != null)
                {
                    _autoRefreshTimer.Stop();
                    _autoRefreshTimer.Dispose();
                }
                if (_scRefreshTimer != null)
                {
                    _scRefreshTimer.Stop();
                    _scRefreshTimer.Dispose();
                }
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._vsVillageListPanelPlaceholder = new System.Windows.Forms.Label();
            this._rdActionListPanelPlaceholder = new System.Windows.Forms.Label();
            this._rcVillageListPanelPlaceholder = new System.Windows.Forms.Label();
            this._rcCapitalsListPanelPlaceholder = new System.Windows.Forms.Label();
            this._crVillageListPanelPlaceholder = new System.Windows.Forms.Label();
            this._bldBuildingListPanelPlaceholder = new System.Windows.Forms.Label();
            this._ppVillageListPanelPlaceholder = new System.Windows.Forms.Label();
            this._bqVillageListPanelPlaceholder = new System.Windows.Forms.Label();
            this._mkRouteListPanelPlaceholder = new System.Windows.Forms.Label();
            this._autoProdScrollPanelPlaceholder = new System.Windows.Forms.Label();
            this._autoModuleScrollPanelPlaceholder = new System.Windows.Forms.Label();
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
            this._rdMinAttacksInput = new System.Windows.Forms.NumericUpDown();
            this._rdMinAttacksLabel = new System.Windows.Forms.Label();
            this._rdMinAttacksWindowInput = new System.Windows.Forms.NumericUpDown();
            this._rdMinAttacksWindowLabel = new System.Windows.Forms.Label();
            this._rdMinAttacksWindowUnitLabel = new System.Windows.Forms.Label();
            this._rdMaxLandTimeInput = new System.Windows.Forms.NumericUpDown();
            this._rdMaxLandTimeLabel = new System.Windows.Forms.Label();
            this._rdAutoRecruitMonksCheck = new System.Windows.Forms.CheckBox();
            this._rdTestDiscordBtn = new System.Windows.Forms.Button();
            this._rdTestSoundBtn = new System.Windows.Forms.Button();
            this._rdStopSoundBtn = new System.Windows.Forms.Button();
            this._rdColSound = new System.Windows.Forms.Label();
            this._rdHintLabel = new System.Windows.Forms.Label();
            this._rdInterdictMonkCountInput = new System.Windows.Forms.NumericUpDown();
            this._rdInterdictLabel = new System.Windows.Forms.Label();
            this._rdWebhookInput = new System.Windows.Forms.TextBox();
            this._rdWebhookLabel = new System.Windows.Forms.Label();
            this._rdMentionTagInput = new System.Windows.Forms.TextBox();
            this._rdMentionTagLabel = new System.Windows.Forms.Label();
            this._rdForceRefreshCheck = new System.Windows.Forms.CheckBox();
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
            this._crCopySettingsBtn = new System.Windows.Forms.Button();
            this._crRefreshBtn = new System.Windows.Forms.Button();
            this._crRepairAllBtn = new System.Windows.Forms.Button();
            this._crMemoriseInfraBtn = new System.Windows.Forms.Button();
            this._crMemoriseTroopsBtn = new System.Windows.Forms.Button();
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
            this._trRoutesListPanel = new System.Windows.Forms.Panel();
            this._trRefreshRoutesBtn = new System.Windows.Forms.Button();
            this._trDeleteRouteBtn = new System.Windows.Forms.Button();
            this._trAddRouteBtn = new System.Windows.Forms.Button();
            this._trPlayerRoutesTab = new System.Windows.Forms.TabPage();
            this._trPlayerRoutesListPanel = new System.Windows.Forms.Panel();
            this._trStatsTab = new System.Windows.Forms.TabPage();
            this._trSettingsPanel = new System.Windows.Forms.Panel();
            this._trDisableOnCardExpiryCheck = new System.Windows.Forms.CheckBox();
            this._trAutoSaveRouteProgressCheck = new System.Windows.Forms.CheckBox();
            this._trPriorityCombo = new System.Windows.Forms.ComboBox();
            this._trPriorityLabel = new System.Windows.Forms.Label();
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
            this._builderPage = new System.Windows.Forms.TabPage();
            this._bldBuildingListPanel = new System.Windows.Forms.Panel();
            this._bldColHeader = new System.Windows.Forms.Panel();
            this._bldNavPanel = new System.Windows.Forms.Panel();
            this._bldClearLayoutBtn = new System.Windows.Forms.Button();
            this._bldExportFileBtn = new System.Windows.Forms.Button();
            this._bldRefreshStateBtn = new System.Windows.Forms.Button();
            this._bldImportFileBtn = new System.Windows.Forms.Button();
            this._bldVillageEnabledCheck = new System.Windows.Forms.CheckBox();
            this._bldVillageCombo = new System.Windows.Forms.ComboBox();
            this._bldSettingsPanel = new System.Windows.Forms.Panel();
            this._bldCopySettingsBtn = new System.Windows.Forms.Button();
            this._bldWaitForResourcesCheck = new System.Windows.Forms.CheckBox();
            this._bldDelayInput = new System.Windows.Forms.NumericUpDown();
            this._bldDelayLabel = new System.Windows.Forms.Label();
            this._bldIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._bldIntervalLabel = new System.Windows.Forms.Label();
            this._bldStatusLabel = new System.Windows.Forms.Label();
            this._bldEnabledCheck = new System.Windows.Forms.CheckBox();
            this._bombPage = new System.Windows.Forms.TabPage();
            this._bombSubTabs = new System.Windows.Forms.TabControl();
            this._bombSetupTab = new System.Windows.Forms.TabPage();
            this._abArmyListPanel = new System.Windows.Forms.Panel();
            this._abSetupColHeader = new System.Windows.Forms.Panel();
            this._abSettingsPanel = new System.Windows.Forms.Panel();
            this._abSubmitBtn = new System.Windows.Forms.Button();
            this._abDeselectAllBtn = new System.Windows.Forms.Button();
            this._abSelectAllBtn = new System.Windows.Forms.Button();
            this._abLoadArmiesBtn = new System.Windows.Forms.Button();
            this._abLoadCapitals = new System.Windows.Forms.CheckBox();
            this._abLoadVillages = new System.Windows.Forms.CheckBox();
            this._abStackDelayInput = new System.Windows.Forms.NumericUpDown();
            this._abStackDelayLabel = new System.Windows.Forms.Label();
            this._abFakeSendCheck = new System.Windows.Forms.CheckBox();
            this._abAutoCancelCheck = new System.Windows.Forms.CheckBox();
            this._abTargetInput = new System.Windows.Forms.NumericUpDown();
            this._abTargetLabel = new System.Windows.Forms.Label();
            this._abStatusLabel = new System.Windows.Forms.Label();
            this._abEnabledCheck = new System.Windows.Forms.CheckBox();
            this._bombPendingTab = new System.Windows.Forms.TabPage();
            this._abPendingListPanel = new System.Windows.Forms.Panel();
            this._abPendingColHeader = new System.Windows.Forms.Panel();
            this._abPendingSettingsPanel = new System.Windows.Forms.Panel();
            this._abClearQueueBtn = new System.Windows.Forms.Button();
            this._abCancelAllBtn = new System.Windows.Forms.Button();
            this._abLaunchBtn = new System.Windows.Forms.Button();
            this._bombTargetQueueTab = new System.Windows.Forms.TabPage();
            this._abQueueListBox = new System.Windows.Forms.ListBox();
            this._abQueueSettingsPanel = new System.Windows.Forms.Panel();
            this._abQueueAddSelectedPlayerBtn = new System.Windows.Forms.Button();
            this._abQueueAddSelectedVillageBtn = new System.Windows.Forms.Button();
            this._abQueueResetBtn = new System.Windows.Forms.Button();
            this._abQueueLoadBtn = new System.Windows.Forms.Button();
            this._abQueueSaveBtn = new System.Windows.Forms.Button();
            this._abQueueClearBtn = new System.Windows.Forms.Button();
            this._abQueueRemoveBtn = new System.Windows.Forms.Button();
            this._abQueueLookupBtn = new System.Windows.Forms.Button();
            this._abQueuePlayerNameInput = new System.Windows.Forms.TextBox();
            this._abQueueAddIdBtn = new System.Windows.Forms.Button();
            this._abQueueVillageIdInput = new System.Windows.Forms.NumericUpDown();
            this._abQueueStatusLabel = new System.Windows.Forms.Label();
            this._abQueueEnabledCheck = new System.Windows.Forms.CheckBox();
            this._bombMultiPage = new System.Windows.Forms.TabPage();
            this._abmSubTabs = new System.Windows.Forms.TabControl();
            this._abmPlayersTab = new System.Windows.Forms.TabPage();
            this._abmPendingTab = new System.Windows.Forms.TabPage();
            this._abmQueueTab = new System.Windows.Forms.TabPage();
            this._abmConnPanel = new System.Windows.Forms.Panel();
            this._abmCtrlPanel = new System.Windows.Forms.Panel();
            this._abmVillageListPanel = new System.Windows.Forms.Panel();
            this._abmVillageColHeader = new System.Windows.Forms.Panel();
            this._abmPendingListPanel = new System.Windows.Forms.Panel();
            this._abmPendingColHeader = new System.Windows.Forms.Panel();
            this._abmQueueListBox = new System.Windows.Forms.ListBox();
            this._abmQueueSettingsPanel = new System.Windows.Forms.Panel();
            this._abmApiUrlBox = new System.Windows.Forms.TextBox();
            this._abmSessionKeyBox = new System.Windows.Forms.TextBox();
            this._abmConnectBtn = new System.Windows.Forms.Button();
            this._abmDisconnectBtn = new System.Windows.Forms.Button();
            this._abmConnStatusLabel = new System.Windows.Forms.Label();
            this._abmTargetVidBox = new System.Windows.Forms.TextBox();
            this._abmStackDelayInput = new System.Windows.Forms.NumericUpDown();
            this._abmFakeSendCheck = new System.Windows.Forms.CheckBox();
            this._abmAutoInterdictCheck = new System.Windows.Forms.CheckBox();
            this._abmPushConfigBtn = new System.Windows.Forms.Button();
            this._abmPrepareBtn = new System.Windows.Forms.Button();
            this._abmLaunchBtn = new System.Windows.Forms.Button();
            this._abmCancelBtn = new System.Windows.Forms.Button();
            this._abmForceRecallBtn = new System.Windows.Forms.Button();
            this._abmDelayModeCombo = new System.Windows.Forms.ComboBox();
            this._abmResetBtn = new System.Windows.Forms.Button();
            this._abmTakeCoordBtn = new System.Windows.Forms.Button();
            this._abmCoordStatusLabel = new System.Windows.Forms.Label();
            this._abmPreRefreshCheck = new System.Windows.Forms.CheckBox();
            this._abmIncludeVassalsCheck = new System.Windows.Forms.CheckBox();
            this._abmPlayCardsCheck = new System.Windows.Forms.CheckBox();
            this._abmAutoCancelCardCheck = new System.Windows.Forms.CheckBox();
            this._abmSendPartialCheck = new System.Windows.Forms.CheckBox();
            this._abmSelectAllBtn = new System.Windows.Forms.Button();
            this._abmDeselectAllBtn = new System.Windows.Forms.Button();
            this._abmQueueEnabledCheck = new System.Windows.Forms.CheckBox();
            this._abmQueueVidInput = new System.Windows.Forms.NumericUpDown();
            this._abmQueueAddIdBtn = new System.Windows.Forms.Button();
            this._abmQueuePlayerNameBox = new System.Windows.Forms.TextBox();
            this._abmQueueLookupBtn = new System.Windows.Forms.Button();
            this._abmQueueAddSelectedVillageBtn = new System.Windows.Forms.Button();
            this._abmQueueAddSelectedPlayerBtn = new System.Windows.Forms.Button();
            this._abmQueueRemoveBtn = new System.Windows.Forms.Button();
            this._abmQueueClearBtn = new System.Windows.Forms.Button();
            this._abmQueueSaveBtn = new System.Windows.Forms.Button();
            this._abmQueueLoadBtn = new System.Windows.Forms.Button();
            this._abmQueueRefreshBtn = new System.Windows.Forms.Button();
            this._abmQueueResetBtn = new System.Windows.Forms.Button();
            this._abmQueueStatusLabel = new System.Windows.Forms.Label();
            this._popularityPage = new System.Windows.Forms.TabPage();
            this._ppVillageListPanel = new System.Windows.Forms.Panel();
            this._ppColHeader = new System.Windows.Forms.Panel();
            this._ppSeparator = new System.Windows.Forms.Panel();
            this._ppSettingsPanel = new System.Windows.Forms.Panel();
            this._ppRunNowBtn = new System.Windows.Forms.Button();
            this._ppCopySettingsBtn = new System.Windows.Forms.Button();
            this._ppRefreshBtn = new System.Windows.Forms.Button();
            this._ppDelayInput = new System.Windows.Forms.NumericUpDown();
            this._ppDelayLabel = new System.Windows.Forms.Label();
            this._ppIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._ppIntervalLabel = new System.Windows.Forms.Label();
            this._ppStatusLabel = new System.Windows.Forms.Label();
            this._ppEnabledCheck = new System.Windows.Forms.CheckBox();
            this._bqPage = new System.Windows.Forms.TabPage();
            this._defenderPage = new System.Windows.Forms.TabPage();
            // Defender tab child controls
            this._dfSettingsPanel = new System.Windows.Forms.Panel();
            this._dfEnabledCheck = new System.Windows.Forms.CheckBox();
            this._dfStatusLabel = new System.Windows.Forms.Label();
            this._dfDurationLabel = new System.Windows.Forms.Label();
            this._dfDurationInput = new System.Windows.Forms.NumericUpDown();
            this._dfVillageLabel = new System.Windows.Forms.Label();
            this._dfVillageCombo = new System.Windows.Forms.ComboBox();
            this._dfVillageRefreshBtn = new System.Windows.Forms.Button();
            this._dfStartBtn = new System.Windows.Forms.Button();
            this._dfStopBtn = new System.Windows.Forms.Button();
            this._dfCountdownPrefixLabel = new System.Windows.Forms.Label();
            this._dfCountdownLabel = new System.Windows.Forms.Label();
            this._dfSep1 = new System.Windows.Forms.Panel();
            this._dfCardsPanel = new System.Windows.Forms.Panel();
            this._dfCardsTitle = new System.Windows.Forms.Label();
            this._dfKnightsLabel = new System.Windows.Forms.Label();
            this._dfKnightsCombo = new System.Windows.Forms.ComboBox();
            this._dfLastStandLabel = new System.Windows.Forms.Label();
            this._dfLastStandCombo = new System.Windows.Forms.ComboBox();
            this._dfDesperateCheck = new System.Windows.Forms.CheckBox();
            this._dfSep2 = new System.Windows.Forms.Panel();
            this._dfActionsPanel = new System.Windows.Forms.Panel();
            this._dfActionsTitle = new System.Windows.Forms.Label();
            this._dfAutoRepairCheck = new System.Windows.Forms.CheckBox();
            this._dfRestoreTroopsCheck = new System.Windows.Forms.CheckBox();
            this._dfRestoreInfraCheck = new System.Windows.Forms.CheckBox();
            this._mkPage = new System.Windows.Forms.TabPage();
            this._mkSettingsPanel = new System.Windows.Forms.Panel();
            this._mkColHeader = new System.Windows.Forms.Panel();
            this._mkRouteListPanel = new System.Windows.Forms.Panel();
            this._mkRouteButtonPanel = new System.Windows.Forms.Panel();
            this._mkEnabledCheck = new System.Windows.Forms.CheckBox();
            this._mkStatusLabel = new System.Windows.Forms.Label();
            this._mkIntervalLabel = new System.Windows.Forms.Label();
            this._mkIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._mkDelayLabel = new System.Windows.Forms.Label();
            this._mkDelayInput = new System.Windows.Forms.NumericUpDown();
            this._mkKeepLabel = new System.Windows.Forms.Label();
            this._mkMonksToKeepInput = new System.Windows.Forms.NumericUpDown();
            this._mkAutoRecruitLabel = new System.Windows.Forms.Label();
            this._mkAutoRecruitInput = new System.Windows.Forms.NumericUpDown();
            this._mkRefreshBtn = new System.Windows.Forms.Button();
            this._mkRunNowBtn = new System.Windows.Forms.Button();
            this._mkAddRouteBtn = new System.Windows.Forms.Button();
            this._mkEditRouteBtn = new System.Windows.Forms.Button();
            this._mkDeleteRouteBtn = new System.Windows.Forms.Button();
            this._bqVillageListPanel = new System.Windows.Forms.Panel();
            this._bqColHeader = new System.Windows.Forms.Panel();
            this._bqSeparator = new System.Windows.Forms.Panel();
            this._bqSettingsPanel = new System.Windows.Forms.Panel();
            this._bqCopySettingsBtn = new System.Windows.Forms.Button();
            this._bqRunNowBtn = new System.Windows.Forms.Button();
            this._bqRefreshBtn = new System.Windows.Forms.Button();
            this._bqDelayInput = new System.Windows.Forms.NumericUpDown();
            this._bqDelayLabel = new System.Windows.Forms.Label();
            this._bqIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._bqIntervalLabel = new System.Windows.Forms.Label();
            this._bqStatusLabel = new System.Windows.Forms.Label();
            this._bqEnabledCheck = new System.Windows.Forms.CheckBox();
            this._scoutPage = new System.Windows.Forms.TabPage();
            this._scSettingsPanel = new System.Windows.Forms.Panel();
            this._scVillagePanel = new System.Windows.Forms.Panel();
            this._scDivider = new System.Windows.Forms.Panel();
            this._scContentPanel = new System.Windows.Forms.Panel();
            this._scSeparator = new System.Windows.Forms.Panel();
            // Scout tab child controls
            this._scEnabledCheck = new System.Windows.Forms.CheckBox();
            this._scStatusLabel = new System.Windows.Forms.Label();
            this._scIntervalLabel = new System.Windows.Forms.Label();
            this._scIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._scMaxTimeLabel = new System.Windows.Forms.Label();
            this._scMaxTimeInput = new System.Windows.Forms.NumericUpDown();
            this._scAutoHireLabel = new System.Windows.Forms.Label();
            this._scAutoHireInput = new System.Windows.Forms.NumericUpDown();
            this._scDelayLabel = new System.Windows.Forms.Label();
            this._scDelayInput = new System.Windows.Forms.NumericUpDown();
            this._scDisableOnCardExpiryCheck = new System.Windows.Forms.CheckBox();
            this._scPriorityLabel = new System.Windows.Forms.Label();
            this._scPriorityResourceRadio = new System.Windows.Forms.RadioButton();
            this._scPriorityRangeRadio = new System.Windows.Forms.RadioButton();
            this._scSendOneScoutCheck = new System.Windows.Forms.CheckBox();
            this._scSendOneOnNewCheck = new System.Windows.Forms.CheckBox();
            this._scVillageListBox = new System.Windows.Forms.ListBox();
            this._scVillageHeaderLabel = new System.Windows.Forms.Label();
            this._scVillageEnabledCheck = new System.Windows.Forms.CheckBox();
            this._scScoutListLabel = new System.Windows.Forms.Label();
            this._scIgnoreListLabel = new System.Windows.Forms.Label();
            this._scScoutList = new System.Windows.Forms.ListBox();
            this._scIgnoreList = new System.Windows.Forms.ListBox();
            this._scMoveUpBtn = new System.Windows.Forms.Button();
            this._scMoveDownBtn = new System.Windows.Forms.Button();
            this._scMoveToIgnoreBtn = new System.Windows.Forms.Button();
            this._scMoveToScoutBtn = new System.Windows.Forms.Button();
            this._scCopySettingsBtn = new System.Windows.Forms.Button();
            this._miscPage = new System.Windows.Forms.TabPage();
            this._miscSettingsPanel = new System.Windows.Forms.Panel();
            this._autoPage = new System.Windows.Forms.TabPage();
            this._autoInnerTabs = new System.Windows.Forms.TabControl();
            this._autoProdTab = new System.Windows.Forms.TabPage();
            this._autoModuleTab = new System.Windows.Forms.TabPage();
            this._autoProdSettingsPanel = new System.Windows.Forms.Panel();
            this._autoProdHeaderPanel = new System.Windows.Forms.Panel();
            this._autoProdScrollPanel = new System.Windows.Forms.Panel();
            this._autoCardIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._autoModuleSettingsPanel = new System.Windows.Forms.Panel();
            this._autoModuleHeaderPanel = new System.Windows.Forms.Panel();
            this._autoModuleScrollPanel = new System.Windows.Forms.Panel();
            this._autoModuleIntervalInput = new System.Windows.Forms.NumericUpDown();
            this._autoServerTimeLabel = new System.Windows.Forms.Label();
            this._autoProdSettingsTitle = new System.Windows.Forms.Label();
            this._autoProdIntervalLabel = new System.Windows.Forms.Label();
            this._autoProdSecondsLabel = new System.Windows.Forms.Label();
            this._autoProdSettingsSep = new System.Windows.Forms.Panel();
            this._autoProdColGood = new System.Windows.Forms.Label();
            this._autoProdColTier = new System.Windows.Forms.Label();
            this._autoProdColTarget = new System.Windows.Forms.Label();
            this._autoProdColDelay = new System.Windows.Forms.Label();
            this._autoProdColProgress = new System.Windows.Forms.Label();
            this._autoModuleSettingsTitle = new System.Windows.Forms.Label();
            this._autoModuleIntervalLabel = new System.Windows.Forms.Label();
            this._autoModuleSecondsLabel = new System.Windows.Forms.Label();
            this._autoModuleSettingsSep = new System.Windows.Forms.Panel();
            this._autoModuleColModule = new System.Windows.Forms.Label();
            this._autoModuleColCards = new System.Windows.Forms.Label();
            this._autoModuleColReplay = new System.Windows.Forms.Label();
            this._autoModuleColAutoOff = new System.Windows.Forms.Label();
            this._miscCollectFreeCardsCheck = new System.Windows.Forms.CheckBox();
            this._miscDisableCannotPlayCardCheck = new System.Windows.Forms.CheckBox();
            this._miscShowOtherTraderInfoCheck = new System.Windows.Forms.CheckBox();
            this._miscWorldMapParishBuildingCountCheck = new System.Windows.Forms.CheckBox();
            this._miscShowUserScreenInfoCheck = new System.Windows.Forms.CheckBox();
            this._miscMapAttackTypeIconsCheck = new System.Windows.Forms.CheckBox();
            this._miscSaleHeaderLabel = new System.Windows.Forms.Label();
            this._miscSalePctLabel = new System.Windows.Forms.Label();
            this._miscSalePctValue = new System.Windows.Forms.Label();
            this._miscSaleStartLabel = new System.Windows.Forms.Label();
            this._miscSaleStartValue = new System.Windows.Forms.Label();
            this._miscSaleEndLabel = new System.Windows.Forms.Label();
            this._miscSaleEndValue = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this._rdMinAttacksInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rdMinAttacksWindowInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rdMaxLandTimeInput)).BeginInit();
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
            this._builderPage.SuspendLayout();
            this._bldNavPanel.SuspendLayout();
            this._bldSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bldDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bldIntervalInput)).BeginInit();
            this._bombPage.SuspendLayout();
            this._bombSubTabs.SuspendLayout();
            this._bombSetupTab.SuspendLayout();
            this._abSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._abStackDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._abTargetInput)).BeginInit();
            this._bombPendingTab.SuspendLayout();
            this._abPendingSettingsPanel.SuspendLayout();
            this._bombTargetQueueTab.SuspendLayout();
            this._abQueueSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._abQueueVillageIdInput)).BeginInit();
            this._bombMultiPage.SuspendLayout();
            this._abmSubTabs.SuspendLayout();
            this._abmPlayersTab.SuspendLayout();
            this._abmPendingTab.SuspendLayout();
            this._abmQueueTab.SuspendLayout();
            this._abmConnPanel.SuspendLayout();
            this._abmCtrlPanel.SuspendLayout();
            this._abmQueueSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._abmStackDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._abmQueueVidInput)).BeginInit();
            this._bqPage.SuspendLayout();
            this._defenderPage.SuspendLayout();
            this._dfSettingsPanel.SuspendLayout();
            this._dfCardsPanel.SuspendLayout();
            this._dfActionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dfDurationInput)).BeginInit();
            this._mkPage.SuspendLayout();
            this._mkColHeader.SuspendLayout();
            this._mkSettingsPanel.SuspendLayout();
            this._mkRouteButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._mkIntervalInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._mkDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._mkMonksToKeepInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._mkAutoRecruitInput)).BeginInit();
            this._bqSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bqDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._bqIntervalInput)).BeginInit();
            this._popularityPage.SuspendLayout();
            this._ppSettingsPanel.SuspendLayout();
            this._scoutPage.SuspendLayout();
            this._scSettingsPanel.SuspendLayout();
            this._scVillagePanel.SuspendLayout();
            this._scContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._scIntervalInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._scMaxTimeInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._scAutoHireInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._scDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ppDelayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ppIntervalInput)).BeginInit();
            this._miscPage.SuspendLayout();
            this._miscSettingsPanel.SuspendLayout();
            this._autoPage.SuspendLayout();
            this._autoInnerTabs.SuspendLayout();
            this._autoProdTab.SuspendLayout();
            this._autoProdSettingsPanel.SuspendLayout();
            this._autoModuleTab.SuspendLayout();
            this._autoModuleSettingsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._autoCardIntervalInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._autoModuleIntervalInput)).BeginInit();
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
            this._tabControl.Controls.Add(this._builderPage);
            this._tabControl.Controls.Add(this._bombPage);
            this._tabControl.Controls.Add(this._bombMultiPage);
            this._tabControl.Controls.Add(this._popularityPage);
            this._tabControl.Controls.Add(this._scoutPage);
            this._tabControl.Controls.Add(this._miscPage);
            this._tabControl.Controls.Add(this._autoPage);
            this._tabControl.Controls.Add(this._bqPage);
            this._tabControl.Controls.Add(this._defenderPage);
            this._tabControl.Controls.Add(this._mkPage);
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
            this._vsVillageListPanel.Controls.Add(this._vsVillageListPanelPlaceholder);
            this._vsVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vsVillageListPanel.Location = new System.Drawing.Point(0, 149);
            this._vsVillageListPanel.Name = "_vsVillageListPanel";
            this._vsVillageListPanel.Size = new System.Drawing.Size(1142, 348);
            this._vsVillageListPanel.TabIndex = 4;
            //
            // _vsVillageListPanelPlaceholder
            //
            this._vsVillageListPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vsVillageListPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._vsVillageListPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._vsVillageListPanelPlaceholder.Name = "_vsVillageListPanelPlaceholder";
            this._vsVillageListPanelPlaceholder.TabIndex = 99;
            this._vsVillageListPanelPlaceholder.Text = "〈 Village rows — one per owned village, populated when world loads 〉";
            this._vsVillageListPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._vsVillageListPanelPlaceholder.Visible = false;
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
            this._vsColHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this._vsColHeader.Size = new System.Drawing.Size(1142, 22);
            this._vsColHeader.TabIndex = 3;
            // 
            // _vsHdrSync
            // 
            this._vsHdrSync.AutoSize = true;
            this._vsHdrSync.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._vsHdrSync.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._vsHdrSync.Location = new System.Drawing.Point(20, 4);
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
            this._vsHdrName.Location = new System.Drawing.Point(60, 4);
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
            this._vsHdrType.Location = new System.Drawing.Point(360, 4);
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
            this._vsHdrId.Location = new System.Drawing.Point(460, 4);
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
            this._vsSettingsPanel.Size = new System.Drawing.Size(1142, 126);
            this._vsSettingsPanel.TabIndex = 0;
            // 
            // _vsButtonBar
            // 
            this._vsButtonBar.Controls.Add(this._vsRefreshBtn);
            this._vsButtonBar.Controls.Add(this._vsSelectAllBtn);
            this._vsButtonBar.Controls.Add(this._vsDeselectAllBtn);
            this._vsButtonBar.Controls.Add(this._vsSelectVillagesBtn);
            this._vsButtonBar.Controls.Add(this._vsSelectCapitalsBtn);
            this._vsButtonBar.Location = new System.Drawing.Point(16, 78);
            this._vsButtonBar.Name = "_vsButtonBar";
            this._vsButtonBar.Size = new System.Drawing.Size(700, 38);
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
            this._vsRefreshBtn.Size = new System.Drawing.Size(100, 34);
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
            this._vsSelectAllBtn.Size = new System.Drawing.Size(100, 34);
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
            this._vsDeselectAllBtn.Size = new System.Drawing.Size(100, 34);
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
            this._vsSelectVillagesBtn.Size = new System.Drawing.Size(120, 34);
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
            this._vsSelectCapitalsBtn.Size = new System.Drawing.Size(120, 34);
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
            this._vsDelayInput.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this._vsDelayInput.Location = new System.Drawing.Point(680, 45);
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
            this._vsDelayLabel.Location = new System.Drawing.Point(440, 46);
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
            this._vsIntervalInput.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this._vsIntervalInput.Location = new System.Drawing.Point(240, 45);
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
            this._vsIntervalLabel.Location = new System.Drawing.Point(32, 46);
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
            this._vsLastRunLabel.Location = new System.Drawing.Point(390, 17);
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
            this._vsStatusLabel.Location = new System.Drawing.Point(216, 17);
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
            this._vsEnabledCheck.Location = new System.Drawing.Point(32, 12);
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
            this._radarPage.Size = new System.Drawing.Size(1142, 497);
            this._radarPage.TabIndex = 1;
            this._radarPage.Text = "Radar";
            // 
            // _rdActionListPanel
            //
            this._rdActionListPanel.AutoScroll = true;
            this._rdActionListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._rdActionListPanel.Controls.Add(this._rdActionListPanelPlaceholder);
            this._rdActionListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rdActionListPanel.Location = new System.Drawing.Point(0, 200);
            this._rdActionListPanel.Name = "_rdActionListPanel";
            this._rdActionListPanel.Size = new System.Drawing.Size(1142, 297);
            this._rdActionListPanel.TabIndex = 3;
            //
            // _rdActionListPanelPlaceholder
            //
            this._rdActionListPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rdActionListPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._rdActionListPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._rdActionListPanelPlaceholder.Name = "_rdActionListPanelPlaceholder";
            this._rdActionListPanelPlaceholder.TabIndex = 99;
            this._rdActionListPanelPlaceholder.Text = "〈 Radar action rows — one per alert type (Attack, Raze, Interdict…) 〉";
            this._rdActionListPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._rdActionListPanelPlaceholder.Visible = false;
            //
            // _rdColHeader
            // 
            this._rdColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this._rdColHeader.Controls.Add(this._rdColActionType);
            this._rdColHeader.Controls.Add(this._rdColMonitor);
            this._rdColHeader.Controls.Add(this._rdColSystemNotify);
            this._rdColHeader.Controls.Add(this._rdColDiscordNotify);
            this._rdColHeader.Controls.Add(this._rdColSound);
            this._rdColHeader.Controls.Add(this._rdColAutoInterdict);
            this._rdColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._rdColHeader.Location = new System.Drawing.Point(0, 176);
            this._rdColHeader.Name = "_rdColHeader";
            this._rdColHeader.Size = new System.Drawing.Size(1142, 34);
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
            this._rdColSystemNotify.Size = new System.Drawing.Size(60, 30);
            this._rdColSystemNotify.TabIndex = 2;
            this._rdColSystemNotify.Text = "System\r\nNotify";
            // 
            // _rdColDiscordNotify
            // 
            this._rdColDiscordNotify.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColDiscordNotify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdColDiscordNotify.Location = new System.Drawing.Point(340, 2);
            this._rdColDiscordNotify.Name = "_rdColDiscordNotify";
            this._rdColDiscordNotify.Size = new System.Drawing.Size(60, 30);
            this._rdColDiscordNotify.TabIndex = 3;
            this._rdColDiscordNotify.Text = "Discord\r\nNotify";
            //
            // _rdColSound
            //
            this._rdColSound.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColSound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdColSound.Location = new System.Drawing.Point(410, 2);
            this._rdColSound.Name = "_rdColSound";
            this._rdColSound.Size = new System.Drawing.Size(70, 30);
            this._rdColSound.TabIndex = 6;
            this._rdColSound.Text = "Sound\r\nNotify";
            //
            // _rdColAutoInterdict
            //
            this._rdColAutoInterdict.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._rdColAutoInterdict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdColAutoInterdict.Location = new System.Drawing.Point(610, 2);
            this._rdColAutoInterdict.Name = "_rdColAutoInterdict";
            this._rdColAutoInterdict.Size = new System.Drawing.Size(60, 30);
            this._rdColAutoInterdict.TabIndex = 4;
            this._rdColAutoInterdict.Text = "Auto\r\nInterdict";
            // 
            // _rdSeparator
            // 
            this._rdSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._rdSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._rdSeparator.Location = new System.Drawing.Point(0, 175);
            this._rdSeparator.Name = "_rdSeparator";
            this._rdSeparator.Size = new System.Drawing.Size(1142, 1);
            this._rdSeparator.TabIndex = 1;
            // 
            // _rdSettingsPanel
            // 
            this._rdSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._rdSettingsPanel.Controls.Add(this._rdMinArmySizeInput);
            this._rdSettingsPanel.Controls.Add(this._rdMinArmySizeLabel);
            this._rdSettingsPanel.Controls.Add(this._rdMinAttacksInput);
            this._rdSettingsPanel.Controls.Add(this._rdMinAttacksLabel);
            this._rdSettingsPanel.Controls.Add(this._rdMinAttacksWindowInput);
            this._rdSettingsPanel.Controls.Add(this._rdMinAttacksWindowLabel);
            this._rdSettingsPanel.Controls.Add(this._rdMinAttacksWindowUnitLabel);
            this._rdSettingsPanel.Controls.Add(this._rdMaxLandTimeInput);
            this._rdSettingsPanel.Controls.Add(this._rdMaxLandTimeLabel);
            this._rdSettingsPanel.Controls.Add(this._rdAutoRecruitMonksCheck);
            this._rdSettingsPanel.Controls.Add(this._rdTestDiscordBtn);
            this._rdSettingsPanel.Controls.Add(this._rdTestSoundBtn);
            this._rdSettingsPanel.Controls.Add(this._rdStopSoundBtn);
            this._rdSettingsPanel.Controls.Add(this._rdHintLabel);
            this._rdSettingsPanel.Controls.Add(this._rdInterdictMonkCountInput);
            this._rdSettingsPanel.Controls.Add(this._rdInterdictLabel);
            this._rdSettingsPanel.Controls.Add(this._rdWebhookInput);
            this._rdSettingsPanel.Controls.Add(this._rdWebhookLabel);
            this._rdSettingsPanel.Controls.Add(this._rdMentionTagInput);
            this._rdSettingsPanel.Controls.Add(this._rdMentionTagLabel);
            this._rdSettingsPanel.Controls.Add(this._rdForceRefreshCheck);
            this._rdSettingsPanel.Controls.Add(this._rdScanIntervalInput);
            this._rdSettingsPanel.Controls.Add(this._rdScanIntervalLabel);
            this._rdSettingsPanel.Controls.Add(this._rdStatusLabel);
            this._rdSettingsPanel.Controls.Add(this._rdEnabledCheck);
            this._rdSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._rdSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._rdSettingsPanel.Name = "_rdSettingsPanel";
            this._rdSettingsPanel.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this._rdSettingsPanel.Size = new System.Drawing.Size(1142, 175);
            this._rdSettingsPanel.TabIndex = 0;
            // 
            // _rdMinArmySizeInput
            // 
            this._rdMinArmySizeInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rdMinArmySizeInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdMinArmySizeInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rdMinArmySizeInput.Location = new System.Drawing.Point(537, 93);
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
            this._rdMinArmySizeLabel.Location = new System.Drawing.Point(416, 96);
            this._rdMinArmySizeLabel.Name = "_rdMinArmySizeLabel";
            this._rdMinArmySizeLabel.Size = new System.Drawing.Size(115, 15);
            this._rdMinArmySizeLabel.TabIndex = 20;
            this._rdMinArmySizeLabel.Text = "Min army size for ID:";
            // 
            // _rdMinAttacksInput
            // 
            this._rdMinAttacksInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rdMinAttacksInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdMinAttacksInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rdMinAttacksInput.Location = new System.Drawing.Point(157, 122);
            this._rdMinAttacksInput.Name = "_rdMinAttacksInput";
            this._rdMinAttacksInput.Size = new System.Drawing.Size(55, 23);
            this._rdMinAttacksInput.TabIndex = 40;
            // 
            // _rdMinAttacksLabel
            // 
            this._rdMinAttacksLabel.AutoSize = true;
            this._rdMinAttacksLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdMinAttacksLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdMinAttacksLabel.Location = new System.Drawing.Point(8, 125);
            this._rdMinAttacksLabel.Name = "_rdMinAttacksLabel";
            this._rdMinAttacksLabel.Size = new System.Drawing.Size(143, 15);
            this._rdMinAttacksLabel.TabIndex = 41;
            this._rdMinAttacksLabel.Text = "Min attacks for ID (0=off):";
            // 
            // _rdMinAttacksWindowInput
            // 
            this._rdMinAttacksWindowInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rdMinAttacksWindowInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdMinAttacksWindowInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rdMinAttacksWindowInput.Location = new System.Drawing.Point(262, 122);
            this._rdMinAttacksWindowInput.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this._rdMinAttacksWindowInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._rdMinAttacksWindowInput.Name = "_rdMinAttacksWindowInput";
            this._rdMinAttacksWindowInput.Size = new System.Drawing.Size(55, 23);
            this._rdMinAttacksWindowInput.TabIndex = 42;
            this._rdMinAttacksWindowInput.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // _rdMinAttacksWindowLabel
            // 
            this._rdMinAttacksWindowLabel.AutoSize = true;
            this._rdMinAttacksWindowLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdMinAttacksWindowLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdMinAttacksWindowLabel.Location = new System.Drawing.Point(216, 125);
            this._rdMinAttacksWindowLabel.Name = "_rdMinAttacksWindowLabel";
            this._rdMinAttacksWindowLabel.Size = new System.Drawing.Size(40, 15);
            this._rdMinAttacksWindowLabel.TabIndex = 43;
            this._rdMinAttacksWindowLabel.Text = "within";
            // 
            // _rdMinAttacksWindowUnitLabel
            // 
            this._rdMinAttacksWindowUnitLabel.AutoSize = true;
            this._rdMinAttacksWindowUnitLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdMinAttacksWindowUnitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdMinAttacksWindowUnitLabel.Location = new System.Drawing.Point(322, 125);
            this._rdMinAttacksWindowUnitLabel.Name = "_rdMinAttacksWindowUnitLabel";
            this._rdMinAttacksWindowUnitLabel.Size = new System.Drawing.Size(12, 15);
            this._rdMinAttacksWindowUnitLabel.TabIndex = 44;
            this._rdMinAttacksWindowUnitLabel.Text = "s";
            // 
            // _rdMaxLandTimeInput
            // 
            this._rdMaxLandTimeInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rdMaxLandTimeInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdMaxLandTimeInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rdMaxLandTimeInput.Location = new System.Drawing.Point(567, 122);
            this._rdMaxLandTimeInput.Maximum = new decimal(new int[] {
            168,
            0,
            0,
            0});
            this._rdMaxLandTimeInput.Name = "_rdMaxLandTimeInput";
            this._rdMaxLandTimeInput.Size = new System.Drawing.Size(55, 23);
            this._rdMaxLandTimeInput.TabIndex = 45;
            // 
            // _rdMaxLandTimeLabel
            // 
            this._rdMaxLandTimeLabel.AutoSize = true;
            this._rdMaxLandTimeLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdMaxLandTimeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdMaxLandTimeLabel.Location = new System.Drawing.Point(416, 125);
            this._rdMaxLandTimeLabel.Name = "_rdMaxLandTimeLabel";
            this._rdMaxLandTimeLabel.Size = new System.Drawing.Size(145, 15);
            this._rdMaxLandTimeLabel.TabIndex = 46;
            this._rdMaxLandTimeLabel.Text = "Max land time hrs (0=off):";
            // 
            // _rdAutoRecruitMonksCheck
            // 
            this._rdAutoRecruitMonksCheck.AutoSize = true;
            this._rdAutoRecruitMonksCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rdAutoRecruitMonksCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdAutoRecruitMonksCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdAutoRecruitMonksCheck.Location = new System.Drawing.Point(202, 96);
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
            // _rdTestSoundBtn
            //
            this._rdTestSoundBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._rdTestSoundBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._rdTestSoundBtn.FlatAppearance.BorderSize = 0;
            this._rdTestSoundBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rdTestSoundBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._rdTestSoundBtn.ForeColor = System.Drawing.Color.White;
            this._rdTestSoundBtn.Location = new System.Drawing.Point(660, 65);
            this._rdTestSoundBtn.Name = "_rdTestSoundBtn";
            this._rdTestSoundBtn.Size = new System.Drawing.Size(110, 24);
            this._rdTestSoundBtn.TabIndex = 11;
            this._rdTestSoundBtn.Text = "Test Sound";
            this._rdTestSoundBtn.UseVisualStyleBackColor = false;
            //
            // _rdStopSoundBtn
            //
            this._rdStopSoundBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._rdStopSoundBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._rdStopSoundBtn.FlatAppearance.BorderSize = 0;
            this._rdStopSoundBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._rdStopSoundBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._rdStopSoundBtn.ForeColor = System.Drawing.Color.White;
            this._rdStopSoundBtn.Location = new System.Drawing.Point(780, 65);
            this._rdStopSoundBtn.Name = "_rdStopSoundBtn";
            this._rdStopSoundBtn.Size = new System.Drawing.Size(110, 24);
            this._rdStopSoundBtn.TabIndex = 12;
            this._rdStopSoundBtn.Text = "Stop Sound";
            this._rdStopSoundBtn.UseVisualStyleBackColor = false;
            // 
            // _rdHintLabel
            // 
            this._rdHintLabel.AutoSize = true;
            this._rdHintLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic);
            this._rdHintLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdHintLabel.Location = new System.Drawing.Point(32, 150);
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
            this._rdInterdictMonkCountInput.Location = new System.Drawing.Point(135, 93);
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
            this._rdInterdictLabel.Location = new System.Drawing.Point(8, 96);
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
            this._rdWebhookInput.Location = new System.Drawing.Point(142, 37);
            this._rdWebhookInput.Name = "_rdWebhookInput";
            this._rdWebhookInput.Size = new System.Drawing.Size(480, 21);
            this._rdWebhookInput.TabIndex = 5;
            // 
            // _rdWebhookLabel
            // 
            this._rdWebhookLabel.AutoSize = true;
            this._rdWebhookLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdWebhookLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdWebhookLabel.Location = new System.Drawing.Point(8, 38);
            this._rdWebhookLabel.Name = "_rdWebhookLabel";
            this._rdWebhookLabel.Size = new System.Drawing.Size(128, 15);
            this._rdWebhookLabel.TabIndex = 4;
            this._rdWebhookLabel.Text = "Discord Webhook URL:";
            // 
            // _rdMentionTagInput
            // 
            this._rdMentionTagInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._rdMentionTagInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._rdMentionTagInput.Font = new System.Drawing.Font("Consolas", 8.5F);
            this._rdMentionTagInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._rdMentionTagInput.Location = new System.Drawing.Point(142, 64);
            this._rdMentionTagInput.Name = "_rdMentionTagInput";
            this._rdMentionTagInput.Size = new System.Drawing.Size(220, 21);
            this._rdMentionTagInput.TabIndex = 31;
            // 
            // _rdMentionTagLabel
            // 
            this._rdMentionTagLabel.AutoSize = true;
            this._rdMentionTagLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdMentionTagLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdMentionTagLabel.Location = new System.Drawing.Point(8, 67);
            this._rdMentionTagLabel.Name = "_rdMentionTagLabel";
            this._rdMentionTagLabel.Size = new System.Drawing.Size(71, 15);
            this._rdMentionTagLabel.TabIndex = 30;
            this._rdMentionTagLabel.Text = "Discord Tag:";
            // 
            // _rdForceRefreshCheck
            // 
            this._rdForceRefreshCheck.AutoSize = true;
            this._rdForceRefreshCheck.Checked = true;
            this._rdForceRefreshCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._rdForceRefreshCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._rdForceRefreshCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._rdForceRefreshCheck.Location = new System.Drawing.Point(450, 12);
            this._rdForceRefreshCheck.Name = "_rdForceRefreshCheck";
            this._rdForceRefreshCheck.Size = new System.Drawing.Size(187, 19);
            this._rdForceRefreshCheck.TabIndex = 30;
            this._rdForceRefreshCheck.Text = "Force refresh armies each scan";
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
            this._rdScanIntervalLabel.Location = new System.Drawing.Point(259, 14);
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
            this._rdStatusLabel.Location = new System.Drawing.Point(147, 14);
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
            this._rdEnabledCheck.Location = new System.Drawing.Point(8, 8);
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
            this._rcVillageListPanel.Controls.Add(this._rcVillageListPanelPlaceholder);
            this._rcVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rcVillageListPanel.Location = new System.Drawing.Point(0, 24);
            this._rcVillageListPanel.Name = "_rcVillageListPanel";
            this._rcVillageListPanel.Size = new System.Drawing.Size(1134, 346);
            this._rcVillageListPanel.TabIndex = 1;
            //
            // _rcVillageListPanelPlaceholder
            //
            this._rcVillageListPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rcVillageListPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._rcVillageListPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._rcVillageListPanelPlaceholder.Name = "_rcVillageListPanelPlaceholder";
            this._rcVillageListPanelPlaceholder.TabIndex = 99;
            this._rcVillageListPanelPlaceholder.Text = "〈 Village recruit panels — one per village, populated when world loads 〉";
            this._rcVillageListPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._rcVillageListPanelPlaceholder.Visible = false;
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
            this._rcCapitalsTab.Size = new System.Drawing.Size(1134, 370);
            this._rcCapitalsTab.TabIndex = 1;
            this._rcCapitalsTab.Text = "Capitals";
            // 
            // _rcCapitalsListPanel
            //
            this._rcCapitalsListPanel.AutoScroll = true;
            this._rcCapitalsListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._rcCapitalsListPanel.Controls.Add(this._rcCapitalsListPanelPlaceholder);
            this._rcCapitalsListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rcCapitalsListPanel.Location = new System.Drawing.Point(0, 24);
            this._rcCapitalsListPanel.Name = "_rcCapitalsListPanel";
            this._rcCapitalsListPanel.Size = new System.Drawing.Size(1134, 346);
            this._rcCapitalsListPanel.TabIndex = 1;
            //
            // _rcCapitalsListPanelPlaceholder
            //
            this._rcCapitalsListPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rcCapitalsListPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._rcCapitalsListPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._rcCapitalsListPanelPlaceholder.Name = "_rcCapitalsListPanelPlaceholder";
            this._rcCapitalsListPanelPlaceholder.TabIndex = 99;
            this._rcCapitalsListPanelPlaceholder.Text = "〈 Capital recruit panels — one per capital, populated when world loads 〉";
            this._rcCapitalsListPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._rcCapitalsListPanelPlaceholder.Visible = false;
            //
            // _rcColHeaderCapitals
            // 
            this._rcColHeaderCapitals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(44)))));
            this._rcColHeaderCapitals.Dock = System.Windows.Forms.DockStyle.Top;
            this._rcColHeaderCapitals.Location = new System.Drawing.Point(0, 0);
            this._rcColHeaderCapitals.Name = "_rcColHeaderCapitals";
            this._rcColHeaderCapitals.Size = new System.Drawing.Size(1134, 24);
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
            this._rcVassalsTab.Size = new System.Drawing.Size(1134, 370);
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
            this._vaVassalListPanel.Size = new System.Drawing.Size(1134, 309);
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
            this._vaColHeader.Size = new System.Drawing.Size(1134, 24);
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
            this._vaSeparator.Size = new System.Drawing.Size(1134, 1);
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
            this._vaSettingsPanel.Size = new System.Drawing.Size(1134, 36);
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
            this._crVillageListPanel.Controls.Add(this._crVillageListPanelPlaceholder);
            this._crVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._crVillageListPanel.Location = new System.Drawing.Point(0, 175);
            this._crVillageListPanel.Name = "_crVillageListPanel";
            this._crVillageListPanel.Size = new System.Drawing.Size(1142, 322);
            this._crVillageListPanel.TabIndex = 3;
            //
            // _crVillageListPanelPlaceholder
            //
            this._crVillageListPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._crVillageListPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._crVillageListPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._crVillageListPanelPlaceholder.Name = "_crVillageListPanelPlaceholder";
            this._crVillageListPanelPlaceholder.TabIndex = 99;
            this._crVillageListPanelPlaceholder.Text = "〈 Castle repair rows — one per village, populated when world loads 〉";
            this._crVillageListPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._crVillageListPanelPlaceholder.Visible = false;
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
            this._crSettingsPanel.Controls.Add(this._crMemoriseInfraBtn);
            this._crSettingsPanel.Controls.Add(this._crMemoriseTroopsBtn);
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
            // _crMemoriseInfraBtn
            //
            this._crMemoriseInfraBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(130)))), ((int)(((byte)(80)))));
            this._crMemoriseInfraBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._crMemoriseInfraBtn.FlatAppearance.BorderSize = 0;
            this._crMemoriseInfraBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._crMemoriseInfraBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._crMemoriseInfraBtn.ForeColor = System.Drawing.Color.White;
            this._crMemoriseInfraBtn.Location = new System.Drawing.Point(604, 98);
            this._crMemoriseInfraBtn.Name = "_crMemoriseInfraBtn";
            this._crMemoriseInfraBtn.Size = new System.Drawing.Size(115, 24);
            this._crMemoriseInfraBtn.TabIndex = 11;
            this._crMemoriseInfraBtn.Text = "Memorise Infra";
            this._crMemoriseInfraBtn.UseVisualStyleBackColor = false;
            //
            // _crMemoriseTroopsBtn
            //
            this._crMemoriseTroopsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(130)))), ((int)(((byte)(80)))));
            this._crMemoriseTroopsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._crMemoriseTroopsBtn.FlatAppearance.BorderSize = 0;
            this._crMemoriseTroopsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._crMemoriseTroopsBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._crMemoriseTroopsBtn.ForeColor = System.Drawing.Color.White;
            this._crMemoriseTroopsBtn.Location = new System.Drawing.Point(731, 98);
            this._crMemoriseTroopsBtn.Name = "_crMemoriseTroopsBtn";
            this._crMemoriseTroopsBtn.Size = new System.Drawing.Size(115, 24);
            this._crMemoriseTroopsBtn.TabIndex = 12;
            this._crMemoriseTroopsBtn.Text = "Memorise Troops";
            this._crMemoriseTroopsBtn.UseVisualStyleBackColor = false;
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
            this._trSubTabs.Controls.Add(this._trStatsTab);
            this._trSubTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this._trSubTabs_Selected);
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
            this._trRoutesTab.Size = new System.Drawing.Size(1134, 371);
            this._trRoutesTab.TabIndex = 1;
            this._trRoutesTab.Text = "Village Routes";
            // 
            // _trRoutesListPanel
            // 
            this._trRoutesListPanel.AutoScroll = true;
            this._trRoutesListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._trRoutesListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._trRoutesListPanel.Location = new System.Drawing.Point(0, 0);
            this._trRoutesListPanel.Name = "_trRoutesListPanel";
            this._trRoutesListPanel.Size = new System.Drawing.Size(1134, 371);
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
            // _trPlayerRoutesTab
            // 
            this._trPlayerRoutesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._trPlayerRoutesTab.Controls.Add(this._trPlayerRoutesListPanel);
            this._trPlayerRoutesTab.Location = new System.Drawing.Point(4, 22);
            this._trPlayerRoutesTab.Name = "_trPlayerRoutesTab";
            this._trPlayerRoutesTab.Size = new System.Drawing.Size(1134, 371);
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
            this._trPlayerRoutesListPanel.Size = new System.Drawing.Size(1134, 371);
            this._trPlayerRoutesListPanel.TabIndex = 0;
            //
            // _trStatsTab
            //
            this._trStatsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._trStatsTab.Name = "_trStatsTab";
            this._trStatsTab.Text = "Stats";
            this._trStatsTab.TabIndex = 3;
            //
            // _trSettingsPanel
            //
            this._trSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._trSettingsPanel.Controls.Add(this._trAutoSaveRouteProgressCheck);
            this._trSettingsPanel.Controls.Add(this._trDisableOnCardExpiryCheck);
            this._trSettingsPanel.Controls.Add(this._trPriorityCombo);
            this._trSettingsPanel.Controls.Add(this._trPriorityLabel);
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
            // _trAutoSaveRouteProgressCheck
            //
            this._trAutoSaveRouteProgressCheck.AutoSize = true;
            this._trAutoSaveRouteProgressCheck.Checked = true;
            this._trAutoSaveRouteProgressCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._trAutoSaveRouteProgressCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trAutoSaveRouteProgressCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trAutoSaveRouteProgressCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trAutoSaveRouteProgressCheck.Location = new System.Drawing.Point(928, 70);
            this._trAutoSaveRouteProgressCheck.Name = "_trAutoSaveRouteProgressCheck";
            this._trAutoSaveRouteProgressCheck.TabIndex = 18;
            this._trAutoSaveRouteProgressCheck.Text = "Auto-save route progress";
            //
            // _trDisableOnCardExpiryCheck
            //
            this._trDisableOnCardExpiryCheck.AutoSize = true;
            this._trDisableOnCardExpiryCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trDisableOnCardExpiryCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trDisableOnCardExpiryCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trDisableOnCardExpiryCheck.Location = new System.Drawing.Point(775, 70);
            this._trDisableOnCardExpiryCheck.Name = "_trDisableOnCardExpiryCheck";
            this._trDisableOnCardExpiryCheck.Size = new System.Drawing.Size(139, 19);
            this._trDisableOnCardExpiryCheck.TabIndex = 17;
            this._trDisableOnCardExpiryCheck.Text = "Disable on card expiry";
            //
            // _trPriorityLabel
            //
            this._trPriorityLabel.AutoSize = true;
            this._trPriorityLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trPriorityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this._trPriorityLabel.Location = new System.Drawing.Point(490, 72);
            this._trPriorityLabel.Name = "_trPriorityLabel";
            this._trPriorityLabel.Text = "Priority:";
            //
            // _trPriorityCombo
            //
            this._trPriorityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._trPriorityCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._trPriorityCombo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._trPriorityCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._trPriorityCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._trPriorityCombo.Location = new System.Drawing.Point(545, 68);
            this._trPriorityCombo.Name = "_trPriorityCombo";
            this._trPriorityCombo.Size = new System.Drawing.Size(220, 22);
            this._trPriorityCombo.TabIndex = 16;
            this._trPriorityCombo.Items.AddRange(new object[] {
                "Market Priority (Sell then Buy)",
                "Market Priority (Buy then Sell)",
                "Village Route Priority",
                "Player Route Priority"});
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
            // _builderPage
            // 
            this._builderPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._builderPage.Controls.Add(this._bldBuildingListPanel);
            this._builderPage.Controls.Add(this._bldColHeader);
            this._builderPage.Controls.Add(this._bldNavPanel);
            this._builderPage.Controls.Add(this._bldSettingsPanel);
            this._builderPage.Location = new System.Drawing.Point(4, 24);
            this._builderPage.Name = "_builderPage";
            this._builderPage.Size = new System.Drawing.Size(1142, 497);
            this._builderPage.TabIndex = 5;
            this._builderPage.Text = "Village Builder";
            // 
            // _bldBuildingListPanel
            //
            this._bldBuildingListPanel.AutoScroll = true;
            this._bldBuildingListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._bldBuildingListPanel.Controls.Add(this._bldBuildingListPanelPlaceholder);
            this._bldBuildingListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bldBuildingListPanel.Location = new System.Drawing.Point(0, 158);
            this._bldBuildingListPanel.Name = "_bldBuildingListPanel";
            this._bldBuildingListPanel.Size = new System.Drawing.Size(1142, 339);
            this._bldBuildingListPanel.TabIndex = 3;
            //
            // _bldBuildingListPanelPlaceholder
            //
            this._bldBuildingListPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bldBuildingListPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._bldBuildingListPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._bldBuildingListPanelPlaceholder.Name = "_bldBuildingListPanelPlaceholder";
            this._bldBuildingListPanelPlaceholder.TabIndex = 99;
            this._bldBuildingListPanelPlaceholder.Text = "〈 Building queue rows — populated from selected village layout 〉";
            this._bldBuildingListPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._bldBuildingListPanelPlaceholder.Visible = false;
            //
            // _bldColHeader
            // 
            this._bldColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(38)))), ((int)(((byte)(50)))));
            this._bldColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._bldColHeader.Location = new System.Drawing.Point(0, 136);
            this._bldColHeader.Name = "_bldColHeader";
            this._bldColHeader.Size = new System.Drawing.Size(1142, 22);
            this._bldColHeader.TabIndex = 2;
            // 
            // _bldNavPanel
            // 
            this._bldNavPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this._bldNavPanel.Controls.Add(this._bldClearLayoutBtn);
            this._bldNavPanel.Controls.Add(this._bldExportFileBtn);
            this._bldNavPanel.Controls.Add(this._bldRefreshStateBtn);
            this._bldNavPanel.Controls.Add(this._bldImportFileBtn);
            this._bldNavPanel.Controls.Add(this._bldVillageEnabledCheck);
            this._bldNavPanel.Controls.Add(this._bldVillageCombo);
            this._bldNavPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._bldNavPanel.Location = new System.Drawing.Point(0, 100);
            this._bldNavPanel.Name = "_bldNavPanel";
            this._bldNavPanel.Size = new System.Drawing.Size(1142, 36);
            this._bldNavPanel.TabIndex = 1;
            // 
            // _bldClearLayoutBtn
            // 
            this._bldClearLayoutBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._bldClearLayoutBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._bldClearLayoutBtn.FlatAppearance.BorderSize = 0;
            this._bldClearLayoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bldClearLayoutBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._bldClearLayoutBtn.ForeColor = System.Drawing.Color.White;
            this._bldClearLayoutBtn.Location = new System.Drawing.Point(890, 6);
            this._bldClearLayoutBtn.Name = "_bldClearLayoutBtn";
            this._bldClearLayoutBtn.Size = new System.Drawing.Size(100, 24);
            this._bldClearLayoutBtn.TabIndex = 5;
            this._bldClearLayoutBtn.Text = "Clear Layout";
            this._bldClearLayoutBtn.UseVisualStyleBackColor = false;
            // 
            // _bldExportFileBtn
            // 
            this._bldExportFileBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this._bldExportFileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._bldExportFileBtn.FlatAppearance.BorderSize = 0;
            this._bldExportFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bldExportFileBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._bldExportFileBtn.ForeColor = System.Drawing.Color.White;
            this._bldExportFileBtn.Location = new System.Drawing.Point(770, 6);
            this._bldExportFileBtn.Name = "_bldExportFileBtn";
            this._bldExportFileBtn.Size = new System.Drawing.Size(110, 24);
            this._bldExportFileBtn.TabIndex = 4;
            this._bldExportFileBtn.Text = "Export To File";
            this._bldExportFileBtn.UseVisualStyleBackColor = false;
            // 
            // _bldRefreshStateBtn
            // 
            this._bldRefreshStateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this._bldRefreshStateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._bldRefreshStateBtn.FlatAppearance.BorderSize = 0;
            this._bldRefreshStateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bldRefreshStateBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._bldRefreshStateBtn.ForeColor = System.Drawing.Color.White;
            this._bldRefreshStateBtn.Location = new System.Drawing.Point(650, 6);
            this._bldRefreshStateBtn.Name = "_bldRefreshStateBtn";
            this._bldRefreshStateBtn.Size = new System.Drawing.Size(110, 24);
            this._bldRefreshStateBtn.TabIndex = 3;
            this._bldRefreshStateBtn.Text = "Refresh State";
            this._bldRefreshStateBtn.UseVisualStyleBackColor = false;
            // 
            // _bldImportFileBtn
            // 
            this._bldImportFileBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._bldImportFileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._bldImportFileBtn.FlatAppearance.BorderSize = 0;
            this._bldImportFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bldImportFileBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._bldImportFileBtn.ForeColor = System.Drawing.Color.White;
            this._bldImportFileBtn.Location = new System.Drawing.Point(520, 6);
            this._bldImportFileBtn.Name = "_bldImportFileBtn";
            this._bldImportFileBtn.Size = new System.Drawing.Size(120, 24);
            this._bldImportFileBtn.TabIndex = 2;
            this._bldImportFileBtn.Text = "Import From File";
            this._bldImportFileBtn.UseVisualStyleBackColor = false;
            // 
            // _bldVillageEnabledCheck
            // 
            this._bldVillageEnabledCheck.AutoSize = true;
            this._bldVillageEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bldVillageEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._bldVillageEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bldVillageEnabledCheck.Location = new System.Drawing.Point(330, 8);
            this._bldVillageEnabledCheck.Name = "_bldVillageEnabledCheck";
            this._bldVillageEnabledCheck.Size = new System.Drawing.Size(113, 19);
            this._bldVillageEnabledCheck.TabIndex = 1;
            this._bldVillageEnabledCheck.Text = "Build this village";
            // 
            // _bldVillageCombo
            // 
            this._bldVillageCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._bldVillageCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._bldVillageCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bldVillageCombo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._bldVillageCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bldVillageCombo.Location = new System.Drawing.Point(16, 6);
            this._bldVillageCombo.Name = "_bldVillageCombo";
            this._bldVillageCombo.Size = new System.Drawing.Size(300, 23);
            this._bldVillageCombo.TabIndex = 0;
            // 
            // _bldSettingsPanel
            // 
            this._bldSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._bldSettingsPanel.Controls.Add(this._bldCopySettingsBtn);
            this._bldSettingsPanel.Controls.Add(this._bldWaitForResourcesCheck);
            this._bldSettingsPanel.Controls.Add(this._bldDelayInput);
            this._bldSettingsPanel.Controls.Add(this._bldDelayLabel);
            this._bldSettingsPanel.Controls.Add(this._bldIntervalInput);
            this._bldSettingsPanel.Controls.Add(this._bldIntervalLabel);
            this._bldSettingsPanel.Controls.Add(this._bldStatusLabel);
            this._bldSettingsPanel.Controls.Add(this._bldEnabledCheck);
            this._bldSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._bldSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._bldSettingsPanel.Name = "_bldSettingsPanel";
            this._bldSettingsPanel.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this._bldSettingsPanel.Size = new System.Drawing.Size(1142, 100);
            this._bldSettingsPanel.TabIndex = 0;
            // 
            // _bldCopySettingsBtn
            // 
            this._bldCopySettingsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(80)))), ((int)(((byte)(140)))));
            this._bldCopySettingsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._bldCopySettingsBtn.FlatAppearance.BorderSize = 0;
            this._bldCopySettingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bldCopySettingsBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._bldCopySettingsBtn.ForeColor = System.Drawing.Color.White;
            this._bldCopySettingsBtn.Location = new System.Drawing.Point(16, 70);
            this._bldCopySettingsBtn.Name = "_bldCopySettingsBtn";
            this._bldCopySettingsBtn.Size = new System.Drawing.Size(110, 24);
            this._bldCopySettingsBtn.TabIndex = 7;
            this._bldCopySettingsBtn.Text = "Copy Settings";
            this._bldCopySettingsBtn.UseVisualStyleBackColor = false;
            // 
            // _bldWaitForResourcesCheck
            // 
            this._bldWaitForResourcesCheck.AutoSize = true;
            this._bldWaitForResourcesCheck.Checked = true;
            this._bldWaitForResourcesCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._bldWaitForResourcesCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bldWaitForResourcesCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._bldWaitForResourcesCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bldWaitForResourcesCheck.Location = new System.Drawing.Point(350, 40);
            this._bldWaitForResourcesCheck.Name = "_bldWaitForResourcesCheck";
            this._bldWaitForResourcesCheck.Size = new System.Drawing.Size(118, 19);
            this._bldWaitForResourcesCheck.TabIndex = 6;
            this._bldWaitForResourcesCheck.Text = "Wait for resources";
            // 
            // _bldDelayInput
            // 
            this._bldDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._bldDelayInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._bldDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bldDelayInput.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._bldDelayInput.Location = new System.Drawing.Point(260, 40);
            this._bldDelayInput.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this._bldDelayInput.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._bldDelayInput.Name = "_bldDelayInput";
            this._bldDelayInput.Size = new System.Drawing.Size(70, 23);
            this._bldDelayInput.TabIndex = 5;
            this._bldDelayInput.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // _bldDelayLabel
            // 
            this._bldDelayLabel.AutoSize = true;
            this._bldDelayLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._bldDelayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._bldDelayLabel.Location = new System.Drawing.Point(180, 42);
            this._bldDelayLabel.Name = "_bldDelayLabel";
            this._bldDelayLabel.Size = new System.Drawing.Size(66, 15);
            this._bldDelayLabel.TabIndex = 4;
            this._bldDelayLabel.Text = "Delay (ms):";
            // 
            // _bldIntervalInput
            // 
            this._bldIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._bldIntervalInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._bldIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bldIntervalInput.Location = new System.Drawing.Point(100, 40);
            this._bldIntervalInput.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this._bldIntervalInput.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._bldIntervalInput.Name = "_bldIntervalInput";
            this._bldIntervalInput.Size = new System.Drawing.Size(60, 23);
            this._bldIntervalInput.TabIndex = 3;
            this._bldIntervalInput.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // _bldIntervalLabel
            // 
            this._bldIntervalLabel.AutoSize = true;
            this._bldIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._bldIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._bldIntervalLabel.Location = new System.Drawing.Point(16, 42);
            this._bldIntervalLabel.Name = "_bldIntervalLabel";
            this._bldIntervalLabel.Size = new System.Drawing.Size(65, 15);
            this._bldIntervalLabel.TabIndex = 2;
            this._bldIntervalLabel.Text = "Interval (s):";
            // 
            // _bldStatusLabel
            // 
            this._bldStatusLabel.AutoSize = true;
            this._bldStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._bldStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._bldStatusLabel.Location = new System.Drawing.Point(220, 14);
            this._bldStatusLabel.Name = "_bldStatusLabel";
            this._bldStatusLabel.Size = new System.Drawing.Size(64, 15);
            this._bldStatusLabel.TabIndex = 1;
            this._bldStatusLabel.Text = "DISABLED";
            // 
            // _bldEnabledCheck
            // 
            this._bldEnabledCheck.AutoSize = true;
            this._bldEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bldEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._bldEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bldEnabledCheck.Location = new System.Drawing.Point(16, 12);
            this._bldEnabledCheck.Name = "_bldEnabledCheck";
            this._bldEnabledCheck.Size = new System.Drawing.Size(171, 23);
            this._bldEnabledCheck.TabIndex = 0;
            this._bldEnabledCheck.Text = "Enable Village Builder";
            // 
            // _bombPage
            // 
            this._bombPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._bombPage.Controls.Add(this._bombSubTabs);
            this._bombPage.Location = new System.Drawing.Point(4, 24);
            this._bombPage.Name = "_bombPage";
            this._bombPage.Size = new System.Drawing.Size(1142, 497);
            this._bombPage.TabIndex = 6;
            this._bombPage.Text = "Auto Bomb";
            // 
            // _bombSubTabs
            // 
            this._bombSubTabs.Controls.Add(this._bombSetupTab);
            this._bombSubTabs.Controls.Add(this._bombPendingTab);
            this._bombSubTabs.Controls.Add(this._bombTargetQueueTab);
            this._bombSubTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bombSubTabs.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._bombSubTabs.Location = new System.Drawing.Point(0, 0);
            this._bombSubTabs.Name = "_bombSubTabs";
            this._bombSubTabs.SelectedIndex = 0;
            this._bombSubTabs.Size = new System.Drawing.Size(1142, 497);
            this._bombSubTabs.TabIndex = 0;
            // 
            // _bombSetupTab
            // 
            this._bombSetupTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._bombSetupTab.Controls.Add(this._abArmyListPanel);
            this._bombSetupTab.Controls.Add(this._abSetupColHeader);
            this._bombSetupTab.Controls.Add(this._abSettingsPanel);
            this._bombSetupTab.Location = new System.Drawing.Point(4, 22);
            this._bombSetupTab.Name = "_bombSetupTab";
            this._bombSetupTab.Size = new System.Drawing.Size(1134, 471);
            this._bombSetupTab.TabIndex = 0;
            this._bombSetupTab.Text = "Player Auto Bomb";
            // 
            // _abArmyListPanel
            // 
            this._abArmyListPanel.AutoScroll = true;
            this._abArmyListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._abArmyListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._abArmyListPanel.Location = new System.Drawing.Point(0, 120);
            this._abArmyListPanel.Name = "_abArmyListPanel";
            this._abArmyListPanel.Size = new System.Drawing.Size(1134, 351);
            this._abArmyListPanel.TabIndex = 2;
            // 
            // _abSetupColHeader
            // 
            this._abSetupColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(38)))), ((int)(((byte)(50)))));
            this._abSetupColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._abSetupColHeader.Location = new System.Drawing.Point(0, 100);
            this._abSetupColHeader.Name = "_abSetupColHeader";
            this._abSetupColHeader.Size = new System.Drawing.Size(1134, 20);
            this._abSetupColHeader.TabIndex = 1;
            // 
            // _abSettingsPanel
            // 
            this._abSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._abSettingsPanel.Controls.Add(this._abSubmitBtn);
            this._abSettingsPanel.Controls.Add(this._abDeselectAllBtn);
            this._abSettingsPanel.Controls.Add(this._abSelectAllBtn);
            this._abSettingsPanel.Controls.Add(this._abLoadArmiesBtn);
            this._abSettingsPanel.Controls.Add(this._abLoadCapitals);
            this._abSettingsPanel.Controls.Add(this._abLoadVillages);
            this._abSettingsPanel.Controls.Add(this._abStackDelayInput);
            this._abSettingsPanel.Controls.Add(this._abStackDelayLabel);
            this._abSettingsPanel.Controls.Add(this._abFakeSendCheck);
            this._abSettingsPanel.Controls.Add(this._abAutoCancelCheck);
            this._abSettingsPanel.Controls.Add(this._abTargetInput);
            this._abSettingsPanel.Controls.Add(this._abTargetLabel);
            this._abSettingsPanel.Controls.Add(this._abStatusLabel);
            this._abSettingsPanel.Controls.Add(this._abEnabledCheck);
            this._abSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._abSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._abSettingsPanel.Name = "_abSettingsPanel";
            this._abSettingsPanel.Padding = new System.Windows.Forms.Padding(12, 10, 12, 6);
            this._abSettingsPanel.Size = new System.Drawing.Size(1134, 100);
            this._abSettingsPanel.TabIndex = 0;
            // 
            // _abSubmitBtn
            // 
            this._abSubmitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(170)))), ((int)(((byte)(80)))));
            this._abSubmitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._abSubmitBtn.FlatAppearance.BorderSize = 0;
            this._abSubmitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abSubmitBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._abSubmitBtn.ForeColor = System.Drawing.Color.White;
            this._abSubmitBtn.Location = new System.Drawing.Point(530, 66);
            this._abSubmitBtn.Name = "_abSubmitBtn";
            this._abSubmitBtn.Size = new System.Drawing.Size(130, 24);
            this._abSubmitBtn.TabIndex = 12;
            this._abSubmitBtn.Text = "Submit to Queue";
            this._abSubmitBtn.UseVisualStyleBackColor = false;
            // 
            // _abDeselectAllBtn
            // 
            this._abDeselectAllBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abDeselectAllBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._abDeselectAllBtn.FlatAppearance.BorderSize = 0;
            this._abDeselectAllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abDeselectAllBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._abDeselectAllBtn.ForeColor = System.Drawing.Color.White;
            this._abDeselectAllBtn.Location = new System.Drawing.Point(426, 66);
            this._abDeselectAllBtn.Name = "_abDeselectAllBtn";
            this._abDeselectAllBtn.Size = new System.Drawing.Size(90, 24);
            this._abDeselectAllBtn.TabIndex = 11;
            this._abDeselectAllBtn.Text = "Deselect All";
            this._abDeselectAllBtn.UseVisualStyleBackColor = false;
            // 
            // _abSelectAllBtn
            // 
            this._abSelectAllBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abSelectAllBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._abSelectAllBtn.FlatAppearance.BorderSize = 0;
            this._abSelectAllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abSelectAllBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._abSelectAllBtn.ForeColor = System.Drawing.Color.White;
            this._abSelectAllBtn.Location = new System.Drawing.Point(340, 66);
            this._abSelectAllBtn.Name = "_abSelectAllBtn";
            this._abSelectAllBtn.Size = new System.Drawing.Size(80, 24);
            this._abSelectAllBtn.TabIndex = 10;
            this._abSelectAllBtn.Text = "Select All";
            this._abSelectAllBtn.UseVisualStyleBackColor = false;
            // 
            // _abLoadArmiesBtn
            // 
            this._abLoadArmiesBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._abLoadArmiesBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._abLoadArmiesBtn.FlatAppearance.BorderSize = 0;
            this._abLoadArmiesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abLoadArmiesBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._abLoadArmiesBtn.ForeColor = System.Drawing.Color.White;
            this._abLoadArmiesBtn.Location = new System.Drawing.Point(180, 66);
            this._abLoadArmiesBtn.Name = "_abLoadArmiesBtn";
            this._abLoadArmiesBtn.Size = new System.Drawing.Size(140, 24);
            this._abLoadArmiesBtn.TabIndex = 9;
            this._abLoadArmiesBtn.Text = "Load/Reload Armies";
            this._abLoadArmiesBtn.UseVisualStyleBackColor = false;
            // 
            // _abLoadCapitals
            // 
            this._abLoadCapitals.AutoSize = true;
            this._abLoadCapitals.Checked = true;
            this._abLoadCapitals.CheckState = System.Windows.Forms.CheckState.Checked;
            this._abLoadCapitals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abLoadCapitals.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._abLoadCapitals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abLoadCapitals.Location = new System.Drawing.Point(90, 68);
            this._abLoadCapitals.Name = "_abLoadCapitals";
            this._abLoadCapitals.Size = new System.Drawing.Size(65, 19);
            this._abLoadCapitals.TabIndex = 8;
            this._abLoadCapitals.Text = "Capitals";
            // 
            // _abLoadVillages
            // 
            this._abLoadVillages.AutoSize = true;
            this._abLoadVillages.Checked = true;
            this._abLoadVillages.CheckState = System.Windows.Forms.CheckState.Checked;
            this._abLoadVillages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abLoadVillages.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._abLoadVillages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abLoadVillages.Location = new System.Drawing.Point(12, 68);
            this._abLoadVillages.Name = "_abLoadVillages";
            this._abLoadVillages.Size = new System.Drawing.Size(63, 19);
            this._abLoadVillages.TabIndex = 7;
            this._abLoadVillages.Text = "Villages";
            // 
            // _abStackDelayInput
            // 
            this._abStackDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abStackDelayInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._abStackDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abStackDelayInput.Location = new System.Drawing.Point(530, 38);
            this._abStackDelayInput.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._abStackDelayInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._abStackDelayInput.Name = "_abStackDelayInput";
            this._abStackDelayInput.Size = new System.Drawing.Size(50, 23);
            this._abStackDelayInput.TabIndex = 6;
            this._abStackDelayInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _abStackDelayLabel
            // 
            this._abStackDelayLabel.AutoSize = true;
            this._abStackDelayLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._abStackDelayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._abStackDelayLabel.Location = new System.Drawing.Point(420, 40);
            this._abStackDelayLabel.Name = "_abStackDelayLabel";
            this._abStackDelayLabel.Size = new System.Drawing.Size(85, 15);
            this._abStackDelayLabel.TabIndex = 5;
            this._abStackDelayLabel.Text = "Stack delay (s):";
            // 
            // _abFakeSendCheck
            // 
            this._abFakeSendCheck.AutoSize = true;
            this._abFakeSendCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abFakeSendCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._abFakeSendCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abFakeSendCheck.Location = new System.Drawing.Point(600, 38);
            this._abFakeSendCheck.Name = "_abFakeSendCheck";
            this._abFakeSendCheck.Size = new System.Drawing.Size(197, 19);
            this._abFakeSendCheck.TabIndex = 7;
            this._abFakeSendCheck.Text = "Fake send, cancel after 4 minutes";
            // 
            // _abAutoCancelCheck
            // 
            this._abAutoCancelCheck.AutoSize = true;
            this._abAutoCancelCheck.Checked = true;
            this._abAutoCancelCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._abAutoCancelCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abAutoCancelCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._abAutoCancelCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abAutoCancelCheck.Location = new System.Drawing.Point(230, 38);
            this._abAutoCancelCheck.Name = "_abAutoCancelCheck";
            this._abAutoCancelCheck.Size = new System.Drawing.Size(152, 19);
            this._abAutoCancelCheck.TabIndex = 4;
            this._abAutoCancelCheck.Text = "Auto-cancel on interdict";
            // 
            // _abTargetInput
            // 
            this._abTargetInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abTargetInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._abTargetInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abTargetInput.Location = new System.Drawing.Point(130, 38);
            this._abTargetInput.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this._abTargetInput.Name = "_abTargetInput";
            this._abTargetInput.Size = new System.Drawing.Size(80, 23);
            this._abTargetInput.TabIndex = 3;
            // 
            // _abTargetLabel
            // 
            this._abTargetLabel.AutoSize = true;
            this._abTargetLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._abTargetLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._abTargetLabel.Location = new System.Drawing.Point(12, 40);
            this._abTargetLabel.Name = "_abTargetLabel";
            this._abTargetLabel.Size = new System.Drawing.Size(94, 15);
            this._abTargetLabel.TabIndex = 2;
            this._abTargetLabel.Text = "Target Village ID:";
            // 
            // _abStatusLabel
            // 
            this._abStatusLabel.AutoSize = true;
            this._abStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._abStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._abStatusLabel.Location = new System.Drawing.Point(200, 12);
            this._abStatusLabel.Name = "_abStatusLabel";
            this._abStatusLabel.Size = new System.Drawing.Size(64, 15);
            this._abStatusLabel.TabIndex = 1;
            this._abStatusLabel.Text = "DISABLED";
            // 
            // _abEnabledCheck
            // 
            this._abEnabledCheck.AutoSize = true;
            this._abEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._abEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abEnabledCheck.Location = new System.Drawing.Point(12, 10);
            this._abEnabledCheck.Name = "_abEnabledCheck";
            this._abEnabledCheck.Size = new System.Drawing.Size(149, 23);
            this._abEnabledCheck.TabIndex = 0;
            this._abEnabledCheck.Text = "Enable Auto Bomb";
            // 
            // _bombPendingTab
            // 
            this._bombPendingTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._bombPendingTab.Controls.Add(this._abPendingListPanel);
            this._bombPendingTab.Controls.Add(this._abPendingColHeader);
            this._bombPendingTab.Controls.Add(this._abPendingSettingsPanel);
            this._bombPendingTab.Location = new System.Drawing.Point(4, 22);
            this._bombPendingTab.Name = "_bombPendingTab";
            this._bombPendingTab.Size = new System.Drawing.Size(1134, 471);
            this._bombPendingTab.TabIndex = 1;
            this._bombPendingTab.Text = "Pending Attacks";
            // 
            // _abPendingListPanel
            // 
            this._abPendingListPanel.AutoScroll = true;
            this._abPendingListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._abPendingListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._abPendingListPanel.Location = new System.Drawing.Point(0, 64);
            this._abPendingListPanel.Name = "_abPendingListPanel";
            this._abPendingListPanel.Size = new System.Drawing.Size(1134, 407);
            this._abPendingListPanel.TabIndex = 2;
            // 
            // _abPendingColHeader
            // 
            this._abPendingColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(38)))), ((int)(((byte)(50)))));
            this._abPendingColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._abPendingColHeader.Location = new System.Drawing.Point(0, 44);
            this._abPendingColHeader.Name = "_abPendingColHeader";
            this._abPendingColHeader.Size = new System.Drawing.Size(1134, 20);
            this._abPendingColHeader.TabIndex = 1;
            // 
            // _abPendingSettingsPanel
            // 
            this._abPendingSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._abPendingSettingsPanel.Controls.Add(this._abClearQueueBtn);
            this._abPendingSettingsPanel.Controls.Add(this._abCancelAllBtn);
            this._abPendingSettingsPanel.Controls.Add(this._abLaunchBtn);
            this._abPendingSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._abPendingSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._abPendingSettingsPanel.Name = "_abPendingSettingsPanel";
            this._abPendingSettingsPanel.Size = new System.Drawing.Size(1134, 44);
            this._abPendingSettingsPanel.TabIndex = 0;
            // 
            // _abClearQueueBtn
            // 
            this._abClearQueueBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(72)))), ((int)(((byte)(84)))));
            this._abClearQueueBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._abClearQueueBtn.FlatAppearance.BorderSize = 0;
            this._abClearQueueBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abClearQueueBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._abClearQueueBtn.ForeColor = System.Drawing.Color.White;
            this._abClearQueueBtn.Location = new System.Drawing.Point(242, 10);
            this._abClearQueueBtn.Name = "_abClearQueueBtn";
            this._abClearQueueBtn.Size = new System.Drawing.Size(100, 28);
            this._abClearQueueBtn.TabIndex = 2;
            this._abClearQueueBtn.Text = "Clear Queue";
            this._abClearQueueBtn.UseVisualStyleBackColor = false;
            // 
            // _abCancelAllBtn
            // 
            this._abCancelAllBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(130)))), ((int)(((byte)(50)))));
            this._abCancelAllBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._abCancelAllBtn.FlatAppearance.BorderSize = 0;
            this._abCancelAllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abCancelAllBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._abCancelAllBtn.ForeColor = System.Drawing.Color.White;
            this._abCancelAllBtn.Location = new System.Drawing.Point(132, 10);
            this._abCancelAllBtn.Name = "_abCancelAllBtn";
            this._abCancelAllBtn.Size = new System.Drawing.Size(100, 28);
            this._abCancelAllBtn.TabIndex = 1;
            this._abCancelAllBtn.Text = "Cancel All";
            this._abCancelAllBtn.UseVisualStyleBackColor = false;
            // 
            // _abLaunchBtn
            // 
            this._abLaunchBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this._abLaunchBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._abLaunchBtn.FlatAppearance.BorderSize = 0;
            this._abLaunchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abLaunchBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._abLaunchBtn.ForeColor = System.Drawing.Color.White;
            this._abLaunchBtn.Location = new System.Drawing.Point(12, 10);
            this._abLaunchBtn.Name = "_abLaunchBtn";
            this._abLaunchBtn.Size = new System.Drawing.Size(110, 28);
            this._abLaunchBtn.TabIndex = 0;
            this._abLaunchBtn.Text = "LAUNCH";
            this._abLaunchBtn.UseVisualStyleBackColor = false;
            // 
            // _bombTargetQueueTab
            // 
            this._bombTargetQueueTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._bombTargetQueueTab.Controls.Add(this._abQueueListBox);
            this._bombTargetQueueTab.Controls.Add(this._abQueueSettingsPanel);
            this._bombTargetQueueTab.Location = new System.Drawing.Point(4, 22);
            this._bombTargetQueueTab.Name = "_bombTargetQueueTab";
            this._bombTargetQueueTab.Size = new System.Drawing.Size(1134, 471);
            this._bombTargetQueueTab.TabIndex = 2;
            this._bombTargetQueueTab.Text = "Target Queue";
            // 
            // _abQueueListBox
            // 
            this._abQueueListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this._abQueueListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._abQueueListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._abQueueListBox.Font = new System.Drawing.Font("Consolas", 9.5F);
            this._abQueueListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueListBox.ItemHeight = 15;
            this._abQueueListBox.Location = new System.Drawing.Point(0, 104);
            this._abQueueListBox.Name = "_abQueueListBox";
            this._abQueueListBox.Size = new System.Drawing.Size(1134, 367);
            this._abQueueListBox.TabIndex = 1;
            // 
            // _abQueueSettingsPanel
            // 
            this._abQueueSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._abQueueSettingsPanel.Controls.Add(this._abQueueAddSelectedPlayerBtn);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueAddSelectedVillageBtn);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueResetBtn);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueLoadBtn);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueSaveBtn);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueClearBtn);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueRemoveBtn);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueLookupBtn);
            this._abQueueSettingsPanel.Controls.Add(this._abQueuePlayerNameInput);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueAddIdBtn);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueVillageIdInput);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueStatusLabel);
            this._abQueueSettingsPanel.Controls.Add(this._abQueueEnabledCheck);
            this._abQueueSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._abQueueSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._abQueueSettingsPanel.Name = "_abQueueSettingsPanel";
            this._abQueueSettingsPanel.Padding = new System.Windows.Forms.Padding(12, 10, 12, 6);
            this._abQueueSettingsPanel.Size = new System.Drawing.Size(1134, 104);
            this._abQueueSettingsPanel.TabIndex = 0;
            // 
            // _abQueueAddSelectedPlayerBtn
            // 
            this._abQueueAddSelectedPlayerBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abQueueAddSelectedPlayerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abQueueAddSelectedPlayerBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abQueueAddSelectedPlayerBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueAddSelectedPlayerBtn.Location = new System.Drawing.Point(195, 72);
            this._abQueueAddSelectedPlayerBtn.Name = "_abQueueAddSelectedPlayerBtn";
            this._abQueueAddSelectedPlayerBtn.Size = new System.Drawing.Size(195, 23);
            this._abQueueAddSelectedPlayerBtn.TabIndex = 12;
            this._abQueueAddSelectedPlayerBtn.Text = "Add Selected Player to Queue";
            this._abQueueAddSelectedPlayerBtn.UseVisualStyleBackColor = false;
            // 
            // _abQueueAddSelectedVillageBtn
            // 
            this._abQueueAddSelectedVillageBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abQueueAddSelectedVillageBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abQueueAddSelectedVillageBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abQueueAddSelectedVillageBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueAddSelectedVillageBtn.Location = new System.Drawing.Point(12, 72);
            this._abQueueAddSelectedVillageBtn.Name = "_abQueueAddSelectedVillageBtn";
            this._abQueueAddSelectedVillageBtn.Size = new System.Drawing.Size(175, 23);
            this._abQueueAddSelectedVillageBtn.TabIndex = 11;
            this._abQueueAddSelectedVillageBtn.Text = "Add Selected Village to Queue";
            this._abQueueAddSelectedVillageBtn.UseVisualStyleBackColor = false;
            // 
            // _abQueueResetBtn
            // 
            this._abQueueResetBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abQueueResetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abQueueResetBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abQueueResetBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueResetBtn.Location = new System.Drawing.Point(698, 44);
            this._abQueueResetBtn.Name = "_abQueueResetBtn";
            this._abQueueResetBtn.Size = new System.Drawing.Size(55, 23);
            this._abQueueResetBtn.TabIndex = 10;
            this._abQueueResetBtn.Text = "Reset";
            this._abQueueResetBtn.UseVisualStyleBackColor = false;
            // 
            // _abQueueLoadBtn
            // 
            this._abQueueLoadBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abQueueLoadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abQueueLoadBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abQueueLoadBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueLoadBtn.Location = new System.Drawing.Point(629, 44);
            this._abQueueLoadBtn.Name = "_abQueueLoadBtn";
            this._abQueueLoadBtn.Size = new System.Drawing.Size(65, 23);
            this._abQueueLoadBtn.TabIndex = 9;
            this._abQueueLoadBtn.Text = "Load List";
            this._abQueueLoadBtn.UseVisualStyleBackColor = false;
            // 
            // _abQueueSaveBtn
            // 
            this._abQueueSaveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abQueueSaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abQueueSaveBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abQueueSaveBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueSaveBtn.Location = new System.Drawing.Point(560, 44);
            this._abQueueSaveBtn.Name = "_abQueueSaveBtn";
            this._abQueueSaveBtn.Size = new System.Drawing.Size(65, 23);
            this._abQueueSaveBtn.TabIndex = 8;
            this._abQueueSaveBtn.Text = "Save List";
            this._abQueueSaveBtn.UseVisualStyleBackColor = false;
            // 
            // _abQueueClearBtn
            // 
            this._abQueueClearBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abQueueClearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abQueueClearBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abQueueClearBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueClearBtn.Location = new System.Drawing.Point(489, 44);
            this._abQueueClearBtn.Name = "_abQueueClearBtn";
            this._abQueueClearBtn.Size = new System.Drawing.Size(55, 23);
            this._abQueueClearBtn.TabIndex = 7;
            this._abQueueClearBtn.Text = "Clear";
            this._abQueueClearBtn.UseVisualStyleBackColor = false;
            // 
            // _abQueueRemoveBtn
            // 
            this._abQueueRemoveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abQueueRemoveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abQueueRemoveBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abQueueRemoveBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueRemoveBtn.Location = new System.Drawing.Point(420, 44);
            this._abQueueRemoveBtn.Name = "_abQueueRemoveBtn";
            this._abQueueRemoveBtn.Size = new System.Drawing.Size(65, 23);
            this._abQueueRemoveBtn.TabIndex = 6;
            this._abQueueRemoveBtn.Text = "Remove";
            this._abQueueRemoveBtn.UseVisualStyleBackColor = false;
            // 
            // _abQueueLookupBtn
            // 
            this._abQueueLookupBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abQueueLookupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abQueueLookupBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abQueueLookupBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueLookupBtn.Location = new System.Drawing.Point(309, 44);
            this._abQueueLookupBtn.Name = "_abQueueLookupBtn";
            this._abQueueLookupBtn.Size = new System.Drawing.Size(95, 23);
            this._abQueueLookupBtn.TabIndex = 5;
            this._abQueueLookupBtn.Text = "Lookup Player";
            this._abQueueLookupBtn.UseVisualStyleBackColor = false;
            // 
            // _abQueuePlayerNameInput
            // 
            this._abQueuePlayerNameInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abQueuePlayerNameInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._abQueuePlayerNameInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueuePlayerNameInput.Location = new System.Drawing.Point(165, 44);
            this._abQueuePlayerNameInput.Name = "_abQueuePlayerNameInput";
            this._abQueuePlayerNameInput.Size = new System.Drawing.Size(140, 23);
            this._abQueuePlayerNameInput.TabIndex = 4;
            // 
            // _abQueueAddIdBtn
            // 
            this._abQueueAddIdBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abQueueAddIdBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abQueueAddIdBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abQueueAddIdBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueAddIdBtn.Location = new System.Drawing.Point(96, 44);
            this._abQueueAddIdBtn.Name = "_abQueueAddIdBtn";
            this._abQueueAddIdBtn.Size = new System.Drawing.Size(55, 23);
            this._abQueueAddIdBtn.TabIndex = 3;
            this._abQueueAddIdBtn.Text = "Add ID";
            this._abQueueAddIdBtn.UseVisualStyleBackColor = false;
            // 
            // _abQueueVillageIdInput
            // 
            this._abQueueVillageIdInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abQueueVillageIdInput.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._abQueueVillageIdInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueVillageIdInput.Location = new System.Drawing.Point(12, 44);
            this._abQueueVillageIdInput.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this._abQueueVillageIdInput.Name = "_abQueueVillageIdInput";
            this._abQueueVillageIdInput.Size = new System.Drawing.Size(80, 23);
            this._abQueueVillageIdInput.TabIndex = 2;
            // 
            // _abQueueStatusLabel
            // 
            this._abQueueStatusLabel.AutoSize = true;
            this._abQueueStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._abQueueStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(200)))), ((int)(((byte)(120)))));
            this._abQueueStatusLabel.Location = new System.Drawing.Point(250, 12);
            this._abQueueStatusLabel.Name = "_abQueueStatusLabel";
            this._abQueueStatusLabel.Size = new System.Drawing.Size(0, 15);
            this._abQueueStatusLabel.TabIndex = 1;
            // 
            // _abQueueEnabledCheck
            // 
            this._abQueueEnabledCheck.AutoSize = true;
            this._abQueueEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abQueueEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._abQueueEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abQueueEnabledCheck.Location = new System.Drawing.Point(12, 10);
            this._abQueueEnabledCheck.Name = "_abQueueEnabledCheck";
            this._abQueueEnabledCheck.Size = new System.Drawing.Size(216, 23);
            this._abQueueEnabledCheck.TabIndex = 0;
            this._abQueueEnabledCheck.Text = "Enable Auto-Advance Queue";
            //
            // _bombMultiPage
            //
            this._bombMultiPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._bombMultiPage.Controls.Add(this._abmSubTabs);
            this._bombMultiPage.Controls.Add(this._abmConnPanel);
            this._bombMultiPage.Location = new System.Drawing.Point(4, 24);
            this._bombMultiPage.Name = "_bombMultiPage";
            this._bombMultiPage.Size = new System.Drawing.Size(1142, 497);
            this._bombMultiPage.TabIndex = 7;
            this._bombMultiPage.Text = "Auto Bomb Multi";
            //
            // _abmConnPanel
            //
            this._abmConnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(30)))));
            this._abmConnPanel.Controls.Add(this._abmConnStatusLabel);
            this._abmConnPanel.Controls.Add(this._abmDisconnectBtn);
            this._abmConnPanel.Controls.Add(this._abmConnectBtn);
            this._abmConnPanel.Controls.Add(this._abmSessionKeyBox);
            this._abmConnPanel.Controls.Add(this._abmApiUrlBox);
            this._abmConnPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._abmConnPanel.Location = new System.Drawing.Point(0, 0);
            this._abmConnPanel.Name = "_abmConnPanel";
            this._abmConnPanel.Size = new System.Drawing.Size(1142, 30);
            this._abmConnPanel.TabIndex = 1;
            //
            // _abmApiUrlBox
            //
            this._abmApiUrlBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abmApiUrlBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._abmApiUrlBox.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmApiUrlBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmApiUrlBox.Location = new System.Drawing.Point(60, 4);
            this._abmApiUrlBox.Name = "_abmApiUrlBox";
            this._abmApiUrlBox.Size = new System.Drawing.Size(260, 20);
            this._abmApiUrlBox.TabIndex = 0;
            //
            // _abmSessionKeyBox
            //
            this._abmSessionKeyBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abmSessionKeyBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._abmSessionKeyBox.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmSessionKeyBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmSessionKeyBox.Location = new System.Drawing.Point(360, 4);
            this._abmSessionKeyBox.Name = "_abmSessionKeyBox";
            this._abmSessionKeyBox.PasswordChar = '●';
            this._abmSessionKeyBox.Size = new System.Drawing.Size(100, 20);
            this._abmSessionKeyBox.TabIndex = 1;
            //
            // _abmConnectBtn
            //
            this._abmConnectBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(100)))), ((int)(((byte)(60)))));
            this._abmConnectBtn.FlatAppearance.BorderSize = 0;
            this._abmConnectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmConnectBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmConnectBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmConnectBtn.Location = new System.Drawing.Point(464, 3);
            this._abmConnectBtn.Name = "_abmConnectBtn";
            this._abmConnectBtn.Size = new System.Drawing.Size(70, 22);
            this._abmConnectBtn.TabIndex = 2;
            this._abmConnectBtn.Text = "Connect";
            this._abmConnectBtn.UseVisualStyleBackColor = false;
            //
            // _abmDisconnectBtn
            //
            this._abmDisconnectBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._abmDisconnectBtn.FlatAppearance.BorderSize = 0;
            this._abmDisconnectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmDisconnectBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmDisconnectBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmDisconnectBtn.Location = new System.Drawing.Point(538, 3);
            this._abmDisconnectBtn.Name = "_abmDisconnectBtn";
            this._abmDisconnectBtn.Size = new System.Drawing.Size(84, 22);
            this._abmDisconnectBtn.TabIndex = 3;
            this._abmDisconnectBtn.Text = "Disconnect";
            this._abmDisconnectBtn.UseVisualStyleBackColor = false;
            //
            // _abmConnStatusLabel
            //
            this._abmConnStatusLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._abmConnStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._abmConnStatusLabel.Location = new System.Drawing.Point(626, 7);
            this._abmConnStatusLabel.Name = "_abmConnStatusLabel";
            this._abmConnStatusLabel.Size = new System.Drawing.Size(450, 16);
            this._abmConnStatusLabel.TabIndex = 4;
            this._abmConnStatusLabel.Text = "Not connected";
            //
            // _abmSubTabs
            //
            this._abmSubTabs.Controls.Add(this._abmPlayersTab);
            this._abmSubTabs.Controls.Add(this._abmPendingTab);
            this._abmSubTabs.Controls.Add(this._abmQueueTab);
            this._abmSubTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._abmSubTabs.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._abmSubTabs.Location = new System.Drawing.Point(0, 30);
            this._abmSubTabs.Name = "_abmSubTabs";
            this._abmSubTabs.SelectedIndex = 0;
            this._abmSubTabs.Size = new System.Drawing.Size(1142, 467);
            this._abmSubTabs.TabIndex = 0;
            //
            // _abmPlayersTab
            //
            this._abmPlayersTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._abmPlayersTab.Controls.Add(this._abmVillageListPanel);
            this._abmPlayersTab.Controls.Add(this._abmVillageColHeader);
            this._abmPlayersTab.Controls.Add(this._abmCtrlPanel);
            this._abmPlayersTab.Location = new System.Drawing.Point(4, 22);
            this._abmPlayersTab.Name = "_abmPlayersTab";
            this._abmPlayersTab.Size = new System.Drawing.Size(1134, 441);
            this._abmPlayersTab.TabIndex = 0;
            this._abmPlayersTab.Text = "Players & Villages";
            //
            // _abmCtrlPanel
            //
            this._abmCtrlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(30)))));
            this._abmCtrlPanel.Controls.Add(this._abmCoordStatusLabel);
            this._abmCtrlPanel.Controls.Add(this._abmTakeCoordBtn);
            this._abmCtrlPanel.Controls.Add(this._abmResetBtn);
            this._abmCtrlPanel.Controls.Add(this._abmForceRecallBtn);
            this._abmCtrlPanel.Controls.Add(this._abmCancelBtn);
            this._abmCtrlPanel.Controls.Add(this._abmLaunchBtn);
            this._abmCtrlPanel.Controls.Add(this._abmPrepareBtn);
            this._abmCtrlPanel.Controls.Add(this._abmPushConfigBtn);
            this._abmCtrlPanel.Controls.Add(this._abmAutoInterdictCheck);
            this._abmCtrlPanel.Controls.Add(this._abmFakeSendCheck);
            this._abmCtrlPanel.Controls.Add(this._abmDelayModeCombo);
            this._abmCtrlPanel.Controls.Add(this._abmStackDelayInput);
            this._abmCtrlPanel.Controls.Add(this._abmTargetVidBox);
            this._abmCtrlPanel.Controls.Add(this._abmPreRefreshCheck);
            this._abmCtrlPanel.Controls.Add(this._abmIncludeVassalsCheck);
            this._abmCtrlPanel.Controls.Add(this._abmPlayCardsCheck);
            this._abmCtrlPanel.Controls.Add(this._abmAutoCancelCardCheck);
            this._abmCtrlPanel.Controls.Add(this._abmSendPartialCheck);
            this._abmCtrlPanel.Controls.Add(this._abmSelectAllBtn);
            this._abmCtrlPanel.Controls.Add(this._abmDeselectAllBtn);
            this._abmCtrlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._abmCtrlPanel.Location = new System.Drawing.Point(0, 0);
            this._abmCtrlPanel.Name = "_abmCtrlPanel";
            this._abmCtrlPanel.Size = new System.Drawing.Size(1134, 54);
            this._abmCtrlPanel.TabIndex = 2;
            //
            // _abmTargetVidBox
            //
            this._abmTargetVidBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abmTargetVidBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._abmTargetVidBox.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmTargetVidBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmTargetVidBox.Location = new System.Drawing.Point(84, 5);
            this._abmTargetVidBox.Name = "_abmTargetVidBox";
            this._abmTargetVidBox.Size = new System.Drawing.Size(80, 20);
            this._abmTargetVidBox.TabIndex = 0;
            //
            // _abmStackDelayInput
            //
            this._abmStackDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abmStackDelayInput.Font = new System.Drawing.Font("Segoe UI", 7F);
            this._abmStackDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmStackDelayInput.Location = new System.Drawing.Point(244, 5);
            this._abmStackDelayInput.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            this._abmStackDelayInput.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this._abmStackDelayInput.Name = "_abmStackDelayInput";
            this._abmStackDelayInput.Size = new System.Drawing.Size(44, 20);
            this._abmStackDelayInput.TabIndex = 1;
            this._abmStackDelayInput.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // _abmDelayModeCombo
            //
            this._abmDelayModeCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abmDelayModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._abmDelayModeCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmDelayModeCombo.Font = new System.Drawing.Font("Segoe UI", 7F);
            this._abmDelayModeCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmDelayModeCombo.Items.AddRange(new object[] { "Stack Dly", "Manual ±" });
            this._abmDelayModeCombo.Location = new System.Drawing.Point(168, 4);
            this._abmDelayModeCombo.Name = "_abmDelayModeCombo";
            this._abmDelayModeCombo.Size = new System.Drawing.Size(72, 21);
            this._abmDelayModeCombo.TabIndex = 12;
            //
            // _abmFakeSendCheck
            //
            this._abmFakeSendCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmFakeSendCheck.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmFakeSendCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmFakeSendCheck.Location = new System.Drawing.Point(292, 7);
            this._abmFakeSendCheck.Name = "_abmFakeSendCheck";
            this._abmFakeSendCheck.Size = new System.Drawing.Size(80, 18);
            this._abmFakeSendCheck.TabIndex = 2;
            this._abmFakeSendCheck.Text = "Fake Send";
            //
            // _abmAutoInterdictCheck
            //
            this._abmAutoInterdictCheck.Checked = true;
            this._abmAutoInterdictCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._abmAutoInterdictCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmAutoInterdictCheck.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmAutoInterdictCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmAutoInterdictCheck.Location = new System.Drawing.Point(376, 7);
            this._abmAutoInterdictCheck.Name = "_abmAutoInterdictCheck";
            this._abmAutoInterdictCheck.Size = new System.Drawing.Size(140, 18);
            this._abmAutoInterdictCheck.TabIndex = 3;
            this._abmAutoInterdictCheck.Text = "Auto Cancel Interdict";
            //
            // _abmPreRefreshCheck
            //
            this._abmPreRefreshCheck.Checked = true;
            this._abmPreRefreshCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._abmPreRefreshCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmPreRefreshCheck.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmPreRefreshCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmPreRefreshCheck.Location = new System.Drawing.Point(6, 34);
            this._abmPreRefreshCheck.Name = "_abmPreRefreshCheck";
            this._abmPreRefreshCheck.Size = new System.Drawing.Size(180, 18);
            this._abmPreRefreshCheck.TabIndex = 11;
            this._abmPreRefreshCheck.Text = "Pre-refresh villages before prepare";
            //
            // _abmIncludeVassalsCheck
            //
            this._abmIncludeVassalsCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmIncludeVassalsCheck.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmIncludeVassalsCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmIncludeVassalsCheck.Location = new System.Drawing.Point(190, 34);
            this._abmIncludeVassalsCheck.Name = "_abmIncludeVassalsCheck";
            this._abmIncludeVassalsCheck.Size = new System.Drawing.Size(130, 18);
            this._abmIncludeVassalsCheck.TabIndex = 12;
            this._abmIncludeVassalsCheck.Text = "Include Vassals";
            //
            // _abmPlayCardsCheck
            //
            this._abmPlayCardsCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmPlayCardsCheck.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmPlayCardsCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmPlayCardsCheck.Location = new System.Drawing.Point(324, 34);
            this._abmPlayCardsCheck.Name = "_abmPlayCardsCheck";
            this._abmPlayCardsCheck.Size = new System.Drawing.Size(80, 18);
            this._abmPlayCardsCheck.TabIndex = 13;
            this._abmPlayCardsCheck.Text = "Play Cards";
            //
            // _abmAutoCancelCardCheck
            //
            this._abmAutoCancelCardCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmAutoCancelCardCheck.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmAutoCancelCardCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmAutoCancelCardCheck.Location = new System.Drawing.Point(408, 34);
            this._abmAutoCancelCardCheck.Name = "_abmAutoCancelCardCheck";
            this._abmAutoCancelCardCheck.Size = new System.Drawing.Size(138, 18);
            this._abmAutoCancelCardCheck.TabIndex = 14;
            this._abmAutoCancelCardCheck.Text = "Auto Cancel Wrong Card";
            //
            // _abmSendPartialCheck
            //
            this._abmSendPartialCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmSendPartialCheck.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmSendPartialCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmSendPartialCheck.Location = new System.Drawing.Point(686, 34);
            this._abmSendPartialCheck.Name = "_abmSendPartialCheck";
            this._abmSendPartialCheck.Size = new System.Drawing.Size(230, 18);
            this._abmSendPartialCheck.TabIndex = 16;
            this._abmSendPartialCheck.Text = "Send even if missing troops (partial)";
            //
            // _abmSelectAllBtn
            //
            this._abmSelectAllBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this._abmSelectAllBtn.FlatAppearance.BorderSize = 0;
            this._abmSelectAllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmSelectAllBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmSelectAllBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(200)))));
            this._abmSelectAllBtn.Location = new System.Drawing.Point(556, 33);
            this._abmSelectAllBtn.Name = "_abmSelectAllBtn";
            this._abmSelectAllBtn.Size = new System.Drawing.Size(54, 18);
            this._abmSelectAllBtn.TabIndex = 14;
            this._abmSelectAllBtn.Text = "Sel All";
            this._abmSelectAllBtn.UseVisualStyleBackColor = false;
            //
            // _abmDeselectAllBtn
            //
            this._abmDeselectAllBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._abmDeselectAllBtn.FlatAppearance.BorderSize = 0;
            this._abmDeselectAllBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmDeselectAllBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmDeselectAllBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this._abmDeselectAllBtn.Location = new System.Drawing.Point(614, 33);
            this._abmDeselectAllBtn.Name = "_abmDeselectAllBtn";
            this._abmDeselectAllBtn.Size = new System.Drawing.Size(62, 18);
            this._abmDeselectAllBtn.TabIndex = 15;
            this._abmDeselectAllBtn.Text = "Desel All";
            this._abmDeselectAllBtn.UseVisualStyleBackColor = false;
            //
            // _abmPushConfigBtn
            //
            this._abmPushConfigBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this._abmPushConfigBtn.FlatAppearance.BorderSize = 0;
            this._abmPushConfigBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmPushConfigBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmPushConfigBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmPushConfigBtn.Location = new System.Drawing.Point(520, 4);
            this._abmPushConfigBtn.Name = "_abmPushConfigBtn";
            this._abmPushConfigBtn.Size = new System.Drawing.Size(86, 22);
            this._abmPushConfigBtn.TabIndex = 4;
            this._abmPushConfigBtn.Text = "Push Config";
            this._abmPushConfigBtn.UseVisualStyleBackColor = false;
            //
            // _abmPrepareBtn
            //
            this._abmPrepareBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this._abmPrepareBtn.FlatAppearance.BorderSize = 0;
            this._abmPrepareBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmPrepareBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmPrepareBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmPrepareBtn.Location = new System.Drawing.Point(610, 4);
            this._abmPrepareBtn.Name = "_abmPrepareBtn";
            this._abmPrepareBtn.Size = new System.Drawing.Size(70, 22);
            this._abmPrepareBtn.TabIndex = 5;
            this._abmPrepareBtn.Text = "Prepare";
            this._abmPrepareBtn.UseVisualStyleBackColor = false;
            //
            // _abmLaunchBtn
            //
            this._abmLaunchBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this._abmLaunchBtn.FlatAppearance.BorderSize = 0;
            this._abmLaunchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmLaunchBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmLaunchBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmLaunchBtn.Location = new System.Drawing.Point(684, 4);
            this._abmLaunchBtn.Name = "_abmLaunchBtn";
            this._abmLaunchBtn.Size = new System.Drawing.Size(66, 22);
            this._abmLaunchBtn.TabIndex = 6;
            this._abmLaunchBtn.Text = "Launch";
            this._abmLaunchBtn.UseVisualStyleBackColor = false;
            //
            // _abmCancelBtn
            //
            this._abmCancelBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._abmCancelBtn.FlatAppearance.BorderSize = 0;
            this._abmCancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmCancelBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmCancelBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmCancelBtn.Location = new System.Drawing.Point(754, 4);
            this._abmCancelBtn.Name = "_abmCancelBtn";
            this._abmCancelBtn.Size = new System.Drawing.Size(60, 22);
            this._abmCancelBtn.TabIndex = 7;
            this._abmCancelBtn.Text = "Cancel";
            this._abmCancelBtn.UseVisualStyleBackColor = false;
            //
            // _abmForceRecallBtn
            //
            this._abmForceRecallBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(80)))), ((int)(((byte)(20)))));
            this._abmForceRecallBtn.FlatAppearance.BorderSize = 0;
            this._abmForceRecallBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmForceRecallBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmForceRecallBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmForceRecallBtn.Location = new System.Drawing.Point(818, 4);
            this._abmForceRecallBtn.Name = "_abmForceRecallBtn";
            this._abmForceRecallBtn.Size = new System.Drawing.Size(64, 22);
            this._abmForceRecallBtn.TabIndex = 11;
            this._abmForceRecallBtn.Text = "Recall All";
            this._abmForceRecallBtn.UseVisualStyleBackColor = false;
            //
            // _abmResetBtn
            //
            this._abmResetBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(60)))), ((int)(((byte)(20)))));
            this._abmResetBtn.FlatAppearance.BorderSize = 0;
            this._abmResetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmResetBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmResetBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmResetBtn.Location = new System.Drawing.Point(886, 4);
            this._abmResetBtn.Name = "_abmResetBtn";
            this._abmResetBtn.Size = new System.Drawing.Size(96, 22);
            this._abmResetBtn.TabIndex = 8;
            this._abmResetBtn.Text = "Reset Session";
            this._abmResetBtn.UseVisualStyleBackColor = false;
            //
            // _abmTakeCoordBtn
            //
            this._abmTakeCoordBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(110)))));
            this._abmTakeCoordBtn.FlatAppearance.BorderSize = 0;
            this._abmTakeCoordBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmTakeCoordBtn.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this._abmTakeCoordBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmTakeCoordBtn.Location = new System.Drawing.Point(986, 4);
            this._abmTakeCoordBtn.Name = "_abmTakeCoordBtn";
            this._abmTakeCoordBtn.Size = new System.Drawing.Size(118, 22);
            this._abmTakeCoordBtn.TabIndex = 9;
            this._abmTakeCoordBtn.Text = "Take Coordinator";
            this._abmTakeCoordBtn.UseVisualStyleBackColor = false;
            //
            // _abmCoordStatusLabel
            //
            this._abmCoordStatusLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._abmCoordStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._abmCoordStatusLabel.Location = new System.Drawing.Point(1108, 8);
            this._abmCoordStatusLabel.Name = "_abmCoordStatusLabel";
            this._abmCoordStatusLabel.Size = new System.Drawing.Size(86, 16);
            this._abmCoordStatusLabel.TabIndex = 10;
            this._abmCoordStatusLabel.Text = "Session: idle";
            //
            // _abmVillageColHeader
            //
            this._abmVillageColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(20)))), ((int)(((byte)(28)))));
            this._abmVillageColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._abmVillageColHeader.Location = new System.Drawing.Point(0, 32);
            this._abmVillageColHeader.Name = "_abmVillageColHeader";
            this._abmVillageColHeader.Size = new System.Drawing.Size(1134, 18);
            this._abmVillageColHeader.TabIndex = 1;
            //
            // _abmVillageListPanel
            //
            this._abmVillageListPanel.AutoScroll = true;
            this._abmVillageListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._abmVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._abmVillageListPanel.Location = new System.Drawing.Point(0, 50);
            this._abmVillageListPanel.Name = "_abmVillageListPanel";
            this._abmVillageListPanel.Size = new System.Drawing.Size(1134, 391);
            this._abmVillageListPanel.TabIndex = 0;
            //
            // _abmPendingTab
            //
            this._abmPendingTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._abmPendingTab.Controls.Add(this._abmPendingListPanel);
            this._abmPendingTab.Controls.Add(this._abmPendingColHeader);
            this._abmPendingTab.Location = new System.Drawing.Point(4, 22);
            this._abmPendingTab.Name = "_abmPendingTab";
            this._abmPendingTab.Size = new System.Drawing.Size(1134, 441);
            this._abmPendingTab.TabIndex = 1;
            this._abmPendingTab.Text = "Pending Attacks";
            //
            // _abmPendingColHeader
            //
            this._abmPendingColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(20)))), ((int)(((byte)(28)))));
            this._abmPendingColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._abmPendingColHeader.Location = new System.Drawing.Point(0, 0);
            this._abmPendingColHeader.Name = "_abmPendingColHeader";
            this._abmPendingColHeader.Size = new System.Drawing.Size(1134, 18);
            this._abmPendingColHeader.TabIndex = 1;
            //
            // _abmPendingListPanel
            //
            this._abmPendingListPanel.AutoScroll = true;
            this._abmPendingListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._abmPendingListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._abmPendingListPanel.Location = new System.Drawing.Point(0, 18);
            this._abmPendingListPanel.Name = "_abmPendingListPanel";
            this._abmPendingListPanel.Size = new System.Drawing.Size(1134, 423);
            this._abmPendingListPanel.TabIndex = 0;
            //
            // _abmQueueTab
            //
            this._abmQueueTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._abmQueueTab.Controls.Add(this._abmQueueListBox);
            this._abmQueueTab.Controls.Add(this._abmQueueSettingsPanel);
            this._abmQueueTab.Location = new System.Drawing.Point(4, 22);
            this._abmQueueTab.Name = "_abmQueueTab";
            this._abmQueueTab.Size = new System.Drawing.Size(1134, 441);
            this._abmQueueTab.TabIndex = 2;
            this._abmQueueTab.Text = "Target Queue";
            //
            // _abmQueueSettingsPanel
            //
            this._abmQueueSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueStatusLabel);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueResetBtn);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueRefreshBtn);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueLoadBtn);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueSaveBtn);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueClearBtn);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueRemoveBtn);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueAddSelectedPlayerBtn);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueAddSelectedVillageBtn);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueLookupBtn);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueuePlayerNameBox);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueAddIdBtn);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueVidInput);
            this._abmQueueSettingsPanel.Controls.Add(this._abmQueueEnabledCheck);
            this._abmQueueSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._abmQueueSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._abmQueueSettingsPanel.Name = "_abmQueueSettingsPanel";
            this._abmQueueSettingsPanel.Padding = new System.Windows.Forms.Padding(12, 10, 12, 6);
            this._abmQueueSettingsPanel.Size = new System.Drawing.Size(1134, 104);
            this._abmQueueSettingsPanel.TabIndex = 1;
            //
            // _abmQueueEnabledCheck
            //
            this._abmQueueEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueEnabledCheck.Location = new System.Drawing.Point(12, 10);
            this._abmQueueEnabledCheck.Name = "_abmQueueEnabledCheck";
            this._abmQueueEnabledCheck.Size = new System.Drawing.Size(110, 20);
            this._abmQueueEnabledCheck.TabIndex = 0;
            this._abmQueueEnabledCheck.Text = "Queue Enabled";
            //
            // _abmQueueVidInput
            //
            this._abmQueueVidInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abmQueueVidInput.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueVidInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueVidInput.Location = new System.Drawing.Point(130, 10);
            this._abmQueueVidInput.Maximum = new decimal(new int[] { 2147483647, 0, 0, 0 });
            this._abmQueueVidInput.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this._abmQueueVidInput.Name = "_abmQueueVidInput";
            this._abmQueueVidInput.Size = new System.Drawing.Size(100, 22);
            this._abmQueueVidInput.TabIndex = 1;
            //
            // _abmQueueAddIdBtn
            //
            this._abmQueueAddIdBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abmQueueAddIdBtn.FlatAppearance.BorderSize = 0;
            this._abmQueueAddIdBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueAddIdBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueAddIdBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueAddIdBtn.Location = new System.Drawing.Point(234, 10);
            this._abmQueueAddIdBtn.Name = "_abmQueueAddIdBtn";
            this._abmQueueAddIdBtn.Size = new System.Drawing.Size(80, 22);
            this._abmQueueAddIdBtn.TabIndex = 2;
            this._abmQueueAddIdBtn.Text = "Add by ID";
            this._abmQueueAddIdBtn.UseVisualStyleBackColor = false;
            //
            // _abmQueuePlayerNameBox
            //
            this._abmQueuePlayerNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._abmQueuePlayerNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._abmQueuePlayerNameBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueuePlayerNameBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueuePlayerNameBox.Location = new System.Drawing.Point(318, 10);
            this._abmQueuePlayerNameBox.Name = "_abmQueuePlayerNameBox";
            this._abmQueuePlayerNameBox.Size = new System.Drawing.Size(160, 22);
            this._abmQueuePlayerNameBox.TabIndex = 3;
            //
            // _abmQueueLookupBtn
            //
            this._abmQueueLookupBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abmQueueLookupBtn.FlatAppearance.BorderSize = 0;
            this._abmQueueLookupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueLookupBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueLookupBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueLookupBtn.Location = new System.Drawing.Point(482, 10);
            this._abmQueueLookupBtn.Name = "_abmQueueLookupBtn";
            this._abmQueueLookupBtn.Size = new System.Drawing.Size(100, 22);
            this._abmQueueLookupBtn.TabIndex = 4;
            this._abmQueueLookupBtn.Text = "Lookup Player";
            this._abmQueueLookupBtn.UseVisualStyleBackColor = false;
            //
            // _abmQueueStatusLabel
            //
            this._abmQueueStatusLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._abmQueueStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._abmQueueStatusLabel.Location = new System.Drawing.Point(586, 13);
            this._abmQueueStatusLabel.Name = "_abmQueueStatusLabel";
            this._abmQueueStatusLabel.Size = new System.Drawing.Size(300, 16);
            this._abmQueueStatusLabel.TabIndex = 5;
            this._abmQueueStatusLabel.Text = "No targets in queue";
            //
            // _abmQueueAddSelectedVillageBtn
            //
            this._abmQueueAddSelectedVillageBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abmQueueAddSelectedVillageBtn.FlatAppearance.BorderSize = 0;
            this._abmQueueAddSelectedVillageBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueAddSelectedVillageBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueAddSelectedVillageBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueAddSelectedVillageBtn.Location = new System.Drawing.Point(12, 44);
            this._abmQueueAddSelectedVillageBtn.Name = "_abmQueueAddSelectedVillageBtn";
            this._abmQueueAddSelectedVillageBtn.Size = new System.Drawing.Size(160, 22);
            this._abmQueueAddSelectedVillageBtn.TabIndex = 6;
            this._abmQueueAddSelectedVillageBtn.Text = "Add Selected Village";
            this._abmQueueAddSelectedVillageBtn.UseVisualStyleBackColor = false;
            //
            // _abmQueueAddSelectedPlayerBtn
            //
            this._abmQueueAddSelectedPlayerBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abmQueueAddSelectedPlayerBtn.FlatAppearance.BorderSize = 0;
            this._abmQueueAddSelectedPlayerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueAddSelectedPlayerBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueAddSelectedPlayerBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueAddSelectedPlayerBtn.Location = new System.Drawing.Point(176, 44);
            this._abmQueueAddSelectedPlayerBtn.Name = "_abmQueueAddSelectedPlayerBtn";
            this._abmQueueAddSelectedPlayerBtn.Size = new System.Drawing.Size(158, 22);
            this._abmQueueAddSelectedPlayerBtn.TabIndex = 7;
            this._abmQueueAddSelectedPlayerBtn.Text = "Add Selected Player";
            this._abmQueueAddSelectedPlayerBtn.UseVisualStyleBackColor = false;
            //
            // _abmQueueRemoveBtn
            //
            this._abmQueueRemoveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abmQueueRemoveBtn.FlatAppearance.BorderSize = 0;
            this._abmQueueRemoveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueRemoveBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueRemoveBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueRemoveBtn.Location = new System.Drawing.Point(338, 44);
            this._abmQueueRemoveBtn.Name = "_abmQueueRemoveBtn";
            this._abmQueueRemoveBtn.Size = new System.Drawing.Size(72, 22);
            this._abmQueueRemoveBtn.TabIndex = 8;
            this._abmQueueRemoveBtn.Text = "Remove";
            this._abmQueueRemoveBtn.UseVisualStyleBackColor = false;
            //
            // _abmQueueClearBtn
            //
            this._abmQueueClearBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._abmQueueClearBtn.FlatAppearance.BorderSize = 0;
            this._abmQueueClearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueClearBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueClearBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueClearBtn.Location = new System.Drawing.Point(414, 44);
            this._abmQueueClearBtn.Name = "_abmQueueClearBtn";
            this._abmQueueClearBtn.Size = new System.Drawing.Size(60, 22);
            this._abmQueueClearBtn.TabIndex = 9;
            this._abmQueueClearBtn.Text = "Clear";
            this._abmQueueClearBtn.UseVisualStyleBackColor = false;
            //
            // _abmQueueSaveBtn
            //
            this._abmQueueSaveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abmQueueSaveBtn.FlatAppearance.BorderSize = 0;
            this._abmQueueSaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueSaveBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueSaveBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueSaveBtn.Location = new System.Drawing.Point(478, 44);
            this._abmQueueSaveBtn.Name = "_abmQueueSaveBtn";
            this._abmQueueSaveBtn.Size = new System.Drawing.Size(56, 22);
            this._abmQueueSaveBtn.TabIndex = 10;
            this._abmQueueSaveBtn.Text = "Save";
            this._abmQueueSaveBtn.UseVisualStyleBackColor = false;
            //
            // _abmQueueLoadBtn
            //
            this._abmQueueLoadBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._abmQueueLoadBtn.FlatAppearance.BorderSize = 0;
            this._abmQueueLoadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueLoadBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueLoadBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueLoadBtn.Location = new System.Drawing.Point(538, 44);
            this._abmQueueLoadBtn.Name = "_abmQueueLoadBtn";
            this._abmQueueLoadBtn.Size = new System.Drawing.Size(56, 22);
            this._abmQueueLoadBtn.TabIndex = 11;
            this._abmQueueLoadBtn.Text = "Load";
            this._abmQueueLoadBtn.UseVisualStyleBackColor = false;
            //
            // _abmQueueRefreshBtn
            //
            this._abmQueueRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(70)))));
            this._abmQueueRefreshBtn.FlatAppearance.BorderSize = 0;
            this._abmQueueRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueRefreshBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueRefreshBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueRefreshBtn.Location = new System.Drawing.Point(598, 44);
            this._abmQueueRefreshBtn.Name = "_abmQueueRefreshBtn";
            this._abmQueueRefreshBtn.Size = new System.Drawing.Size(60, 22);
            this._abmQueueRefreshBtn.TabIndex = 12;
            this._abmQueueRefreshBtn.Text = "Refresh";
            this._abmQueueRefreshBtn.UseVisualStyleBackColor = false;
            //
            // _abmQueueResetBtn
            //
            this._abmQueueResetBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(60)))), ((int)(((byte)(20)))));
            this._abmQueueResetBtn.FlatAppearance.BorderSize = 0;
            this._abmQueueResetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._abmQueueResetBtn.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._abmQueueResetBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueResetBtn.Location = new System.Drawing.Point(598, 44);
            this._abmQueueResetBtn.Name = "_abmQueueResetBtn";
            this._abmQueueResetBtn.Size = new System.Drawing.Size(86, 22);
            this._abmQueueResetBtn.TabIndex = 12;
            this._abmQueueResetBtn.Text = "Reset Done";
            this._abmQueueResetBtn.UseVisualStyleBackColor = false;
            //
            // _abmQueueListBox
            //
            this._abmQueueListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this._abmQueueListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._abmQueueListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._abmQueueListBox.Font = new System.Drawing.Font("Consolas", 9.5F);
            this._abmQueueListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._abmQueueListBox.FormattingEnabled = true;
            this._abmQueueListBox.ItemHeight = 15;
            this._abmQueueListBox.Location = new System.Drawing.Point(0, 104);
            this._abmQueueListBox.Name = "_abmQueueListBox";
            this._abmQueueListBox.Size = new System.Drawing.Size(1134, 337);
            this._abmQueueListBox.TabIndex = 0;
            // 
            // _popularityPage
            // 
            this._popularityPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._popularityPage.Controls.Add(this._ppVillageListPanel);
            this._popularityPage.Controls.Add(this._ppColHeader);
            this._popularityPage.Controls.Add(this._ppSeparator);
            this._popularityPage.Controls.Add(this._ppSettingsPanel);
            this._popularityPage.Location = new System.Drawing.Point(4, 24);
            this._popularityPage.Name = "_popularityPage";
            this._popularityPage.Size = new System.Drawing.Size(1142, 497);
            this._popularityPage.TabIndex = 7;
            this._popularityPage.Text = "Popularity";
            // 
            // _ppVillageListPanel
            //
            this._ppVillageListPanel.AutoScroll = true;
            this._ppVillageListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._ppVillageListPanel.Controls.Add(this._ppVillageListPanelPlaceholder);
            this._ppVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ppVillageListPanel.Location = new System.Drawing.Point(0, 22);
            this._ppVillageListPanel.Name = "_ppVillageListPanel";
            this._ppVillageListPanel.Size = new System.Drawing.Size(1142, 384);
            this._ppVillageListPanel.TabIndex = 2;
            //
            // _ppVillageListPanelPlaceholder
            //
            this._ppVillageListPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ppVillageListPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._ppVillageListPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._ppVillageListPanelPlaceholder.Name = "_ppVillageListPanelPlaceholder";
            this._ppVillageListPanelPlaceholder.TabIndex = 99;
            this._ppVillageListPanelPlaceholder.Text = "〈 Village popularity rows — one per village, populated when world loads 〉";
            this._ppVillageListPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._ppVillageListPanelPlaceholder.Visible = false;
            //
            // _ppColHeader
            // 
            this._ppColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this._ppColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._ppColHeader.Location = new System.Drawing.Point(0, 0);
            this._ppColHeader.Name = "_ppColHeader";
            this._ppColHeader.Size = new System.Drawing.Size(1142, 22);
            this._ppColHeader.TabIndex = 1;
            // 
            // _ppSeparator
            // 
            this._ppSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this._ppSeparator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._ppSeparator.Location = new System.Drawing.Point(0, 406);
            this._ppSeparator.Name = "_ppSeparator";
            this._ppSeparator.Size = new System.Drawing.Size(1142, 1);
            this._ppSeparator.TabIndex = 3;
            // 
            // _ppSettingsPanel
            // 
            this._ppSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this._ppSettingsPanel.Controls.Add(this._ppRunNowBtn);
            this._ppSettingsPanel.Controls.Add(this._ppCopySettingsBtn);
            this._ppSettingsPanel.Controls.Add(this._ppRefreshBtn);
            this._ppSettingsPanel.Controls.Add(this._ppDelayInput);
            this._ppSettingsPanel.Controls.Add(this._ppDelayLabel);
            this._ppSettingsPanel.Controls.Add(this._ppIntervalInput);
            this._ppSettingsPanel.Controls.Add(this._ppIntervalLabel);
            this._ppSettingsPanel.Controls.Add(this._ppStatusLabel);
            this._ppSettingsPanel.Controls.Add(this._ppEnabledCheck);
            this._ppSettingsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._ppSettingsPanel.Location = new System.Drawing.Point(0, 407);
            this._ppSettingsPanel.Name = "_ppSettingsPanel";
            this._ppSettingsPanel.Size = new System.Drawing.Size(1142, 90);
            this._ppSettingsPanel.TabIndex = 0;
            // 
            // _ppRunNowBtn
            // 
            this._ppRunNowBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(80)))), ((int)(((byte)(50)))));
            this._ppRunNowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._ppRunNowBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._ppRunNowBtn.Location = new System.Drawing.Point(431, 39);
            this._ppRunNowBtn.Name = "_ppRunNowBtn";
            this._ppRunNowBtn.Size = new System.Drawing.Size(80, 26);
            this._ppRunNowBtn.TabIndex = 7;
            this._ppRunNowBtn.Text = "Run Now";
            this._ppRunNowBtn.UseVisualStyleBackColor = false;
            //
            // _ppCopySettingsBtn
            //
            this._ppCopySettingsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(80)))));
            this._ppCopySettingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._ppCopySettingsBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._ppCopySettingsBtn.Location = new System.Drawing.Point(521, 39);
            this._ppCopySettingsBtn.Name = "_ppCopySettingsBtn";
            this._ppCopySettingsBtn.Size = new System.Drawing.Size(100, 26);
            this._ppCopySettingsBtn.TabIndex = 8;
            this._ppCopySettingsBtn.Text = "Copy Settings";
            this._ppCopySettingsBtn.UseVisualStyleBackColor = false;
            //
            // _ppRefreshBtn
            // 
            this._ppRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this._ppRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._ppRefreshBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._ppRefreshBtn.Location = new System.Drawing.Point(346, 39);
            this._ppRefreshBtn.Name = "_ppRefreshBtn";
            this._ppRefreshBtn.Size = new System.Drawing.Size(75, 26);
            this._ppRefreshBtn.TabIndex = 6;
            this._ppRefreshBtn.Text = "Refresh";
            this._ppRefreshBtn.UseVisualStyleBackColor = false;
            // 
            // _ppDelayInput
            // 
            this._ppDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._ppDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._ppDelayInput.Location = new System.Drawing.Point(256, 41);
            this._ppDelayInput.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this._ppDelayInput.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._ppDelayInput.Name = "_ppDelayInput";
            this._ppDelayInput.Size = new System.Drawing.Size(75, 23);
            this._ppDelayInput.TabIndex = 5;
            this._ppDelayInput.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // _ppDelayLabel
            // 
            this._ppDelayLabel.AutoSize = true;
            this._ppDelayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._ppDelayLabel.Location = new System.Drawing.Point(176, 44);
            this._ppDelayLabel.Name = "_ppDelayLabel";
            this._ppDelayLabel.Size = new System.Drawing.Size(68, 15);
            this._ppDelayLabel.TabIndex = 4;
            this._ppDelayLabel.Text = "Delay (ms):";
            // 
            // _ppIntervalInput
            // 
            this._ppIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._ppIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._ppIntervalInput.Location = new System.Drawing.Point(96, 41);
            this._ppIntervalInput.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this._ppIntervalInput.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._ppIntervalInput.Name = "_ppIntervalInput";
            this._ppIntervalInput.Size = new System.Drawing.Size(65, 23);
            this._ppIntervalInput.TabIndex = 3;
            this._ppIntervalInput.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // _ppIntervalLabel
            // 
            this._ppIntervalLabel.AutoSize = true;
            this._ppIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._ppIntervalLabel.Location = new System.Drawing.Point(16, 44);
            this._ppIntervalLabel.Name = "_ppIntervalLabel";
            this._ppIntervalLabel.Size = new System.Drawing.Size(70, 15);
            this._ppIntervalLabel.TabIndex = 2;
            this._ppIntervalLabel.Text = "Interval (s):";
            // 
            // _ppStatusLabel
            // 
            this._ppStatusLabel.AutoSize = true;
            this._ppStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._ppStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._ppStatusLabel.Location = new System.Drawing.Point(186, 16);
            this._ppStatusLabel.Name = "_ppStatusLabel";
            this._ppStatusLabel.Size = new System.Drawing.Size(59, 13);
            this._ppStatusLabel.TabIndex = 1;
            this._ppStatusLabel.Text = "DISABLED";
            // 
            // _ppEnabledCheck
            // 
            this._ppEnabledCheck.AutoSize = true;
            this._ppEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._ppEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._ppEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._ppEnabledCheck.Location = new System.Drawing.Point(16, 12);
            this._ppEnabledCheck.Name = "_ppEnabledCheck";
            this._ppEnabledCheck.Size = new System.Drawing.Size(143, 23);
            this._ppEnabledCheck.TabIndex = 0;
            this._ppEnabledCheck.Text = "Enable Popularity";
            //
            // _bqPage
            //
            this._bqPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._bqPage.Controls.Add(this._bqVillageListPanel);
            this._bqPage.Controls.Add(this._bqColHeader);
            this._bqPage.Controls.Add(this._bqSeparator);
            this._bqPage.Controls.Add(this._bqSettingsPanel);
            // NOTE: Add order is intentional — last-added Top panel docks to the very top first.
            // SettingsPanel (Top, last) → very top; Separator (Top) → just below it;
            // ColHeader (Top) → below separator; VillageListPanel (Fill) → remaining area.
            this._bqPage.Location = new System.Drawing.Point(4, 24);
            this._bqPage.Name = "_bqPage";
            this._bqPage.Size = new System.Drawing.Size(1142, 497);
            this._bqPage.TabIndex = 12;
            this._bqPage.Text = "Banquet";
            //
            // _defenderPage
            //
            this._defenderPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._defenderPage.Controls.Add(this._dfActionsPanel);
            this._defenderPage.Controls.Add(this._dfSep2);
            this._defenderPage.Controls.Add(this._dfCardsPanel);
            this._defenderPage.Controls.Add(this._dfSep1);
            this._defenderPage.Controls.Add(this._dfSettingsPanel);
            this._defenderPage.Location = new System.Drawing.Point(4, 24);
            this._defenderPage.Name = "_defenderPage";
            this._defenderPage.Size = new System.Drawing.Size(1142, 497);
            this._defenderPage.TabIndex = 13;
            this._defenderPage.Text = "Defender";
            //
            // _dfSettingsPanel
            //
            this._dfSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._dfSettingsPanel.Controls.Add(this._dfEnabledCheck);
            this._dfSettingsPanel.Controls.Add(this._dfStatusLabel);
            this._dfSettingsPanel.Controls.Add(this._dfDurationLabel);
            this._dfSettingsPanel.Controls.Add(this._dfDurationInput);
            this._dfSettingsPanel.Controls.Add(this._dfVillageLabel);
            this._dfSettingsPanel.Controls.Add(this._dfVillageCombo);
            this._dfSettingsPanel.Controls.Add(this._dfVillageRefreshBtn);
            this._dfSettingsPanel.Controls.Add(this._dfStartBtn);
            this._dfSettingsPanel.Controls.Add(this._dfStopBtn);
            this._dfSettingsPanel.Controls.Add(this._dfCountdownPrefixLabel);
            this._dfSettingsPanel.Controls.Add(this._dfCountdownLabel);
            this._dfSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._dfSettingsPanel.Height = 108;
            this._dfSettingsPanel.Name = "_dfSettingsPanel";
            this._dfSettingsPanel.Padding = new System.Windows.Forms.Padding(16, 10, 16, 8);
            this._dfSettingsPanel.Size = new System.Drawing.Size(1142, 108);
            this._dfSettingsPanel.TabIndex = 0;
            //
            // _dfEnabledCheck
            //
            this._dfEnabledCheck.AutoSize = true;
            this._dfEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._dfEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfEnabledCheck.Location = new System.Drawing.Point(16, 10);
            this._dfEnabledCheck.Name = "_dfEnabledCheck";
            this._dfEnabledCheck.Size = new System.Drawing.Size(130, 19);
            this._dfEnabledCheck.TabIndex = 0;
            this._dfEnabledCheck.Text = "Module Enabled";
            //
            // _dfStatusLabel
            //
            this._dfStatusLabel.AutoSize = true;
            this._dfStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._dfStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._dfStatusLabel.Location = new System.Drawing.Point(180, 13);
            this._dfStatusLabel.Name = "_dfStatusLabel";
            this._dfStatusLabel.Size = new System.Drawing.Size(60, 13);
            this._dfStatusLabel.TabIndex = 1;
            this._dfStatusLabel.Text = "DISABLED";
            //
            // _dfDurationLabel
            //
            this._dfDurationLabel.AutoSize = true;
            this._dfDurationLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._dfDurationLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._dfDurationLabel.Location = new System.Drawing.Point(16, 38);
            this._dfDurationLabel.Name = "_dfDurationLabel";
            this._dfDurationLabel.Size = new System.Drawing.Size(75, 15);
            this._dfDurationLabel.TabIndex = 2;
            this._dfDurationLabel.Text = "Duration (s):";
            //
            // _dfDurationInput
            //
            this._dfDurationInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._dfDurationInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._dfDurationInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfDurationInput.Location = new System.Drawing.Point(110, 36);
            this._dfDurationInput.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            this._dfDurationInput.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            this._dfDurationInput.Name = "_dfDurationInput";
            this._dfDurationInput.Size = new System.Drawing.Size(60, 22);
            this._dfDurationInput.TabIndex = 3;
            this._dfDurationInput.Value = new decimal(new int[] { 60, 0, 0, 0 });
            //
            // _dfVillageLabel
            //
            this._dfVillageLabel.AutoSize = true;
            this._dfVillageLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._dfVillageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._dfVillageLabel.Location = new System.Drawing.Point(190, 38);
            this._dfVillageLabel.Name = "_dfVillageLabel";
            this._dfVillageLabel.Size = new System.Drawing.Size(95, 15);
            this._dfVillageLabel.TabIndex = 4;
            this._dfVillageLabel.Text = "Target village:";
            //
            // _dfVillageCombo
            //
            this._dfVillageCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._dfVillageCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._dfVillageCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfVillageCombo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._dfVillageCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfVillageCombo.Location = new System.Drawing.Point(300, 35);
            this._dfVillageCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this._dfVillageCombo.Name = "_dfVillageCombo";
            this._dfVillageCombo.Size = new System.Drawing.Size(300, 22);
            this._dfVillageCombo.TabIndex = 5;
            //
            // _dfVillageRefreshBtn
            //
            this._dfVillageRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._dfVillageRefreshBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._dfVillageRefreshBtn.FlatAppearance.BorderSize = 0;
            this._dfVillageRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfVillageRefreshBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._dfVillageRefreshBtn.ForeColor = System.Drawing.Color.White;
            this._dfVillageRefreshBtn.Location = new System.Drawing.Point(608, 34);
            this._dfVillageRefreshBtn.Name = "_dfVillageRefreshBtn";
            this._dfVillageRefreshBtn.Size = new System.Drawing.Size(70, 22);
            this._dfVillageRefreshBtn.TabIndex = 6;
            this._dfVillageRefreshBtn.Text = "Refresh";
            //
            // _dfStartBtn
            //
            this._dfStartBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(200)))), ((int)(((byte)(120)))));
            this._dfStartBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._dfStartBtn.FlatAppearance.BorderSize = 0;
            this._dfStartBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfStartBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._dfStartBtn.ForeColor = System.Drawing.Color.White;
            this._dfStartBtn.Location = new System.Drawing.Point(16, 70);
            this._dfStartBtn.Name = "_dfStartBtn";
            this._dfStartBtn.Size = new System.Drawing.Size(100, 26);
            this._dfStartBtn.TabIndex = 7;
            this._dfStartBtn.Text = "Start Spam";
            //
            // _dfStopBtn
            //
            this._dfStopBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._dfStopBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this._dfStopBtn.FlatAppearance.BorderSize = 0;
            this._dfStopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfStopBtn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._dfStopBtn.ForeColor = System.Drawing.Color.White;
            this._dfStopBtn.Location = new System.Drawing.Point(126, 70);
            this._dfStopBtn.Name = "_dfStopBtn";
            this._dfStopBtn.Size = new System.Drawing.Size(70, 26);
            this._dfStopBtn.TabIndex = 8;
            this._dfStopBtn.Text = "Stop";
            //
            // _dfCountdownPrefixLabel
            //
            this._dfCountdownPrefixLabel.AutoSize = true;
            this._dfCountdownPrefixLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._dfCountdownPrefixLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._dfCountdownPrefixLabel.Location = new System.Drawing.Point(210, 75);
            this._dfCountdownPrefixLabel.Name = "_dfCountdownPrefixLabel";
            this._dfCountdownPrefixLabel.Size = new System.Drawing.Size(68, 15);
            this._dfCountdownPrefixLabel.TabIndex = 9;
            this._dfCountdownPrefixLabel.Text = "Countdown:";
            //
            // _dfCountdownLabel
            //
            this._dfCountdownLabel.AutoSize = true;
            this._dfCountdownLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._dfCountdownLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._dfCountdownLabel.Location = new System.Drawing.Point(290, 75);
            this._dfCountdownLabel.Name = "_dfCountdownLabel";
            this._dfCountdownLabel.Size = new System.Drawing.Size(20, 15);
            this._dfCountdownLabel.TabIndex = 10;
            this._dfCountdownLabel.Text = "--";
            //
            // _dfSep1
            //
            this._dfSep1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._dfSep1.Dock = System.Windows.Forms.DockStyle.Top;
            this._dfSep1.Height = 1;
            this._dfSep1.Name = "_dfSep1";
            this._dfSep1.Size = new System.Drawing.Size(1142, 1);
            this._dfSep1.TabIndex = 1;
            //
            // _dfCardsPanel
            //
            this._dfCardsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._dfCardsPanel.Controls.Add(this._dfCardsTitle);
            this._dfCardsPanel.Controls.Add(this._dfKnightsLabel);
            this._dfCardsPanel.Controls.Add(this._dfKnightsCombo);
            this._dfCardsPanel.Controls.Add(this._dfLastStandLabel);
            this._dfCardsPanel.Controls.Add(this._dfLastStandCombo);
            this._dfCardsPanel.Controls.Add(this._dfDesperateCheck);
            this._dfCardsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._dfCardsPanel.Height = 96;
            this._dfCardsPanel.Name = "_dfCardsPanel";
            this._dfCardsPanel.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this._dfCardsPanel.Size = new System.Drawing.Size(1142, 96);
            this._dfCardsPanel.TabIndex = 2;
            //
            // _dfCardsTitle
            //
            this._dfCardsTitle.AutoSize = true;
            this._dfCardsTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._dfCardsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfCardsTitle.Location = new System.Drawing.Point(16, 8);
            this._dfCardsTitle.Name = "_dfCardsTitle";
            this._dfCardsTitle.Size = new System.Drawing.Size(140, 15);
            this._dfCardsTitle.TabIndex = 0;
            this._dfCardsTitle.Text = "Cards (Spam Cards)";
            //
            // _dfKnightsLabel
            //
            this._dfKnightsLabel.AutoSize = true;
            this._dfKnightsLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._dfKnightsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._dfKnightsLabel.Location = new System.Drawing.Point(16, 34);
            this._dfKnightsLabel.Name = "_dfKnightsLabel";
            this._dfKnightsLabel.Size = new System.Drawing.Size(85, 15);
            this._dfKnightsLabel.TabIndex = 1;
            this._dfKnightsLabel.Text = "Knights card:";
            //
            // _dfKnightsCombo
            //
            this._dfKnightsCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._dfKnightsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._dfKnightsCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfKnightsCombo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._dfKnightsCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfKnightsCombo.Location = new System.Drawing.Point(120, 31);
            this._dfKnightsCombo.Name = "_dfKnightsCombo";
            this._dfKnightsCombo.Size = new System.Drawing.Size(200, 22);
            this._dfKnightsCombo.TabIndex = 2;
            //
            // _dfLastStandLabel
            //
            this._dfLastStandLabel.AutoSize = true;
            this._dfLastStandLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._dfLastStandLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._dfLastStandLabel.Location = new System.Drawing.Point(340, 34);
            this._dfLastStandLabel.Name = "_dfLastStandLabel";
            this._dfLastStandLabel.Size = new System.Drawing.Size(100, 15);
            this._dfLastStandLabel.TabIndex = 3;
            this._dfLastStandLabel.Text = "Last Stand card:";
            //
            // _dfLastStandCombo
            //
            this._dfLastStandCombo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(64)))));
            this._dfLastStandCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._dfLastStandCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfLastStandCombo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._dfLastStandCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfLastStandCombo.Location = new System.Drawing.Point(450, 31);
            this._dfLastStandCombo.Name = "_dfLastStandCombo";
            this._dfLastStandCombo.Size = new System.Drawing.Size(200, 22);
            this._dfLastStandCombo.TabIndex = 4;
            //
            // _dfDesperateCheck
            //
            this._dfDesperateCheck.AutoSize = true;
            this._dfDesperateCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfDesperateCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._dfDesperateCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfDesperateCheck.Location = new System.Drawing.Point(16, 64);
            this._dfDesperateCheck.Name = "_dfDesperateCheck";
            this._dfDesperateCheck.Size = new System.Drawing.Size(260, 19);
            this._dfDesperateCheck.TabIndex = 5;
            this._dfDesperateCheck.Text = "Spam Desperate Defence (card 263)";
            //
            // _dfSep2
            //
            this._dfSep2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(72)))));
            this._dfSep2.Dock = System.Windows.Forms.DockStyle.Top;
            this._dfSep2.Height = 1;
            this._dfSep2.Name = "_dfSep2";
            this._dfSep2.Size = new System.Drawing.Size(1142, 1);
            this._dfSep2.TabIndex = 3;
            //
            // _dfActionsPanel
            //
            this._dfActionsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this._dfActionsPanel.Controls.Add(this._dfActionsTitle);
            this._dfActionsPanel.Controls.Add(this._dfAutoRepairCheck);
            this._dfActionsPanel.Controls.Add(this._dfRestoreTroopsCheck);
            this._dfActionsPanel.Controls.Add(this._dfRestoreInfraCheck);
            this._dfActionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._dfActionsPanel.Height = 72;
            this._dfActionsPanel.Name = "_dfActionsPanel";
            this._dfActionsPanel.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this._dfActionsPanel.Size = new System.Drawing.Size(1142, 72);
            this._dfActionsPanel.TabIndex = 4;
            //
            // _dfActionsTitle
            //
            this._dfActionsTitle.AutoSize = true;
            this._dfActionsTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._dfActionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfActionsTitle.Location = new System.Drawing.Point(16, 8);
            this._dfActionsTitle.Name = "_dfActionsTitle";
            this._dfActionsTitle.Size = new System.Drawing.Size(290, 15);
            this._dfActionsTitle.TabIndex = 0;
            this._dfActionsTitle.Text = "Castle Actions (applied to target village)";
            //
            // _dfAutoRepairCheck
            //
            this._dfAutoRepairCheck.AutoSize = true;
            this._dfAutoRepairCheck.Checked = true;
            this._dfAutoRepairCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._dfAutoRepairCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfAutoRepairCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._dfAutoRepairCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfAutoRepairCheck.Location = new System.Drawing.Point(16, 38);
            this._dfAutoRepairCheck.Name = "_dfAutoRepairCheck";
            this._dfAutoRepairCheck.Size = new System.Drawing.Size(100, 19);
            this._dfAutoRepairCheck.TabIndex = 1;
            this._dfAutoRepairCheck.Text = "Auto Repair";
            //
            // _dfRestoreTroopsCheck
            //
            this._dfRestoreTroopsCheck.AutoSize = true;
            this._dfRestoreTroopsCheck.Checked = true;
            this._dfRestoreTroopsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this._dfRestoreTroopsCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfRestoreTroopsCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._dfRestoreTroopsCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfRestoreTroopsCheck.Location = new System.Drawing.Point(140, 38);
            this._dfRestoreTroopsCheck.Name = "_dfRestoreTroopsCheck";
            this._dfRestoreTroopsCheck.Size = new System.Drawing.Size(210, 19);
            this._dfRestoreTroopsCheck.TabIndex = 2;
            this._dfRestoreTroopsCheck.Text = "Restore Troops (local layout)";
            //
            // _dfRestoreInfraCheck
            //
            this._dfRestoreInfraCheck.AutoSize = true;
            this._dfRestoreInfraCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._dfRestoreInfraCheck.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._dfRestoreInfraCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._dfRestoreInfraCheck.Location = new System.Drawing.Point(360, 38);
            this._dfRestoreInfraCheck.Name = "_dfRestoreInfraCheck";
            this._dfRestoreInfraCheck.Size = new System.Drawing.Size(270, 19);
            this._dfRestoreInfraCheck.TabIndex = 3;
            this._dfRestoreInfraCheck.Text = "Restore Infrastructure (local layout)";
            //
            // _mkPage
            //
            this._mkPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            // Add order is intentional: last-added Top panel docks to the very top first.
            // RouteListPanel (Fill) → remaining area; RouteButtonPanel (Bottom) → very bottom;
            // ColHeader (Top) → below separator; SettingsPanel (Top, last) → very top.
            this._mkPage.Controls.Add(this._mkRouteListPanel);
            this._mkPage.Controls.Add(this._mkRouteButtonPanel);
            this._mkPage.Controls.Add(this._mkColHeader);
            this._mkPage.Controls.Add(this._mkSettingsPanel);
            this._mkPage.Location = new System.Drawing.Point(4, 24);
            this._mkPage.Name = "_mkPage";
            this._mkPage.Size = new System.Drawing.Size(1142, 497);
            this._mkPage.TabIndex = 14;
            this._mkPage.Text = "Monks";
            //
            // _mkSettingsPanel
            //
            this._mkSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this._mkSettingsPanel.Controls.Add(this._mkEnabledCheck);
            this._mkSettingsPanel.Controls.Add(this._mkStatusLabel);
            this._mkSettingsPanel.Controls.Add(this._mkIntervalLabel);
            this._mkSettingsPanel.Controls.Add(this._mkIntervalInput);
            this._mkSettingsPanel.Controls.Add(this._mkDelayLabel);
            this._mkSettingsPanel.Controls.Add(this._mkDelayInput);
            this._mkSettingsPanel.Controls.Add(this._mkKeepLabel);
            this._mkSettingsPanel.Controls.Add(this._mkMonksToKeepInput);
            this._mkSettingsPanel.Controls.Add(this._mkAutoRecruitLabel);
            this._mkSettingsPanel.Controls.Add(this._mkAutoRecruitInput);
            this._mkSettingsPanel.Controls.Add(this._mkRefreshBtn);
            this._mkSettingsPanel.Controls.Add(this._mkRunNowBtn);
            this._mkSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._mkSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._mkSettingsPanel.Name = "_mkSettingsPanel";
            this._mkSettingsPanel.Size = new System.Drawing.Size(1142, 68);
            this._mkSettingsPanel.TabIndex = 0;
            //
            // _mkEnabledCheck
            //
            this._mkEnabledCheck.AutoSize = true;
            this._mkEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._mkEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._mkEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._mkEnabledCheck.Location = new System.Drawing.Point(16, 12);
            this._mkEnabledCheck.Name = "_mkEnabledCheck";
            this._mkEnabledCheck.Size = new System.Drawing.Size(130, 23);
            this._mkEnabledCheck.TabIndex = 0;
            this._mkEnabledCheck.Text = "Enable Monks";
            //
            // _mkStatusLabel
            //
            this._mkStatusLabel.AutoSize = true;
            this._mkStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._mkStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._mkStatusLabel.Location = new System.Drawing.Point(160, 16);
            this._mkStatusLabel.Name = "_mkStatusLabel";
            this._mkStatusLabel.Size = new System.Drawing.Size(59, 13);
            this._mkStatusLabel.TabIndex = 1;
            this._mkStatusLabel.Text = "DISABLED";
            //
            // _mkIntervalLabel
            //
            this._mkIntervalLabel.AutoSize = true;
            this._mkIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._mkIntervalLabel.Location = new System.Drawing.Point(16, 44);
            this._mkIntervalLabel.Name = "_mkIntervalLabel";
            this._mkIntervalLabel.Size = new System.Drawing.Size(70, 13);
            this._mkIntervalLabel.TabIndex = 2;
            this._mkIntervalLabel.Text = "Interval (s):";
            //
            // _mkIntervalInput
            //
            this._mkIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._mkIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._mkIntervalInput.Location = new System.Drawing.Point(92, 41);
            this._mkIntervalInput.Maximum = new decimal(new int[] { 86400, 0, 0, 0 });
            this._mkIntervalInput.Minimum = new decimal(new int[] { 30, 0, 0, 0 });
            this._mkIntervalInput.Name = "_mkIntervalInput";
            this._mkIntervalInput.Size = new System.Drawing.Size(65, 23);
            this._mkIntervalInput.TabIndex = 3;
            this._mkIntervalInput.Value = new decimal(new int[] { 120, 0, 0, 0 });
            //
            // _mkDelayLabel
            //
            this._mkDelayLabel.AutoSize = true;
            this._mkDelayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._mkDelayLabel.Location = new System.Drawing.Point(174, 44);
            this._mkDelayLabel.Name = "_mkDelayLabel";
            this._mkDelayLabel.Size = new System.Drawing.Size(65, 13);
            this._mkDelayLabel.TabIndex = 4;
            this._mkDelayLabel.Text = "Delay (ms):";
            //
            // _mkDelayInput
            //
            this._mkDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._mkDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._mkDelayInput.Location = new System.Drawing.Point(246, 41);
            this._mkDelayInput.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            this._mkDelayInput.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this._mkDelayInput.Name = "_mkDelayInput";
            this._mkDelayInput.Size = new System.Drawing.Size(75, 23);
            this._mkDelayInput.TabIndex = 5;
            this._mkDelayInput.Value = new decimal(new int[] { 2000, 0, 0, 0 });
            //
            // _mkKeepLabel
            //
            this._mkKeepLabel.AutoSize = true;
            this._mkKeepLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._mkKeepLabel.Location = new System.Drawing.Point(338, 44);
            this._mkKeepLabel.Name = "_mkKeepLabel";
            this._mkKeepLabel.Size = new System.Drawing.Size(75, 13);
            this._mkKeepLabel.TabIndex = 6;
            this._mkKeepLabel.Text = "Keep monks:";
            //
            // _mkMonksToKeepInput
            //
            this._mkMonksToKeepInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._mkMonksToKeepInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._mkMonksToKeepInput.Location = new System.Drawing.Point(420, 41);
            this._mkMonksToKeepInput.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this._mkMonksToKeepInput.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this._mkMonksToKeepInput.Name = "_mkMonksToKeepInput";
            this._mkMonksToKeepInput.Size = new System.Drawing.Size(55, 23);
            this._mkMonksToKeepInput.TabIndex = 7;
            this._mkMonksToKeepInput.Value = new decimal(new int[] { 0, 0, 0, 0 });
            //
            // _mkRefreshBtn
            //
            this._mkRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this._mkRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._mkRefreshBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._mkRefreshBtn.Location = new System.Drawing.Point(492, 39);
            this._mkRefreshBtn.Name = "_mkRefreshBtn";
            this._mkRefreshBtn.Size = new System.Drawing.Size(75, 26);
            this._mkRefreshBtn.TabIndex = 8;
            this._mkRefreshBtn.Text = "Refresh";
            this._mkRefreshBtn.UseVisualStyleBackColor = false;
            //
            // _mkRunNowBtn
            //
            this._mkRunNowBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(80)))), ((int)(((byte)(50)))));
            this._mkRunNowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._mkRunNowBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._mkRunNowBtn.Location = new System.Drawing.Point(578, 39);
            this._mkRunNowBtn.Name = "_mkRunNowBtn";
            this._mkRunNowBtn.Size = new System.Drawing.Size(80, 26);
            this._mkRunNowBtn.TabIndex = 9;
            this._mkRunNowBtn.Text = "Run Now";
            this._mkRunNowBtn.UseVisualStyleBackColor = false;
            //
            // _mkAutoRecruitLabel
            //
            this._mkAutoRecruitLabel.AutoSize = true;
            this._mkAutoRecruitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._mkAutoRecruitLabel.Location = new System.Drawing.Point(668, 44);
            this._mkAutoRecruitLabel.Name = "_mkAutoRecruitLabel";
            this._mkAutoRecruitLabel.TabIndex = 10;
            this._mkAutoRecruitLabel.Text = "Auto recruit:";
            //
            // _mkAutoRecruitInput
            //
            this._mkAutoRecruitInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._mkAutoRecruitInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._mkAutoRecruitInput.Location = new System.Drawing.Point(753, 41);
            this._mkAutoRecruitInput.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            this._mkAutoRecruitInput.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this._mkAutoRecruitInput.Name = "_mkAutoRecruitInput";
            this._mkAutoRecruitInput.Size = new System.Drawing.Size(45, 23);
            this._mkAutoRecruitInput.TabIndex = 11;
            this._mkAutoRecruitInput.Value = new decimal(new int[] { 0, 0, 0, 0 });
            //
            // _mkColHeader
            //
            this._mkColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this._mkColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._mkColHeader.Location = new System.Drawing.Point(0, 0);
            this._mkColHeader.Name = "_mkColHeader";
            this._mkColHeader.Size = new System.Drawing.Size(1142, 22);
            this._mkColHeader.TabIndex = 3;
            //
            // _mkRouteListPanel
            //
            this._mkRouteListPanel.AutoScroll = true;
            this._mkRouteListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._mkRouteListPanel.Controls.Add(this._mkRouteListPanelPlaceholder);
            this._mkRouteListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mkRouteListPanel.Location = new System.Drawing.Point(0, 68);
            this._mkRouteListPanel.Name = "_mkRouteListPanel";
            this._mkRouteListPanel.Size = new System.Drawing.Size(1142, 395);
            this._mkRouteListPanel.TabIndex = 1;
            //
            // _mkRouteListPanelPlaceholder
            //
            this._mkRouteListPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mkRouteListPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._mkRouteListPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._mkRouteListPanelPlaceholder.Name = "_mkRouteListPanelPlaceholder";
            this._mkRouteListPanelPlaceholder.TabIndex = 99;
            this._mkRouteListPanelPlaceholder.Text = "〈 Monk route rows — one per route saved in settings 〉";
            this._mkRouteListPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._mkRouteListPanelPlaceholder.Visible = false;
            //
            // _mkRouteButtonPanel
            //
            this._mkRouteButtonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this._mkRouteButtonPanel.Controls.Add(this._mkAddRouteBtn);
            this._mkRouteButtonPanel.Controls.Add(this._mkEditRouteBtn);
            this._mkRouteButtonPanel.Controls.Add(this._mkDeleteRouteBtn);
            this._mkRouteButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._mkRouteButtonPanel.Location = new System.Drawing.Point(0, 463);
            this._mkRouteButtonPanel.Name = "_mkRouteButtonPanel";
            this._mkRouteButtonPanel.Size = new System.Drawing.Size(1142, 34);
            this._mkRouteButtonPanel.TabIndex = 2;
            //
            // _mkAddRouteBtn
            //
            this._mkAddRouteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this._mkAddRouteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._mkAddRouteBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._mkAddRouteBtn.Location = new System.Drawing.Point(8, 4);
            this._mkAddRouteBtn.Name = "_mkAddRouteBtn";
            this._mkAddRouteBtn.Size = new System.Drawing.Size(90, 26);
            this._mkAddRouteBtn.TabIndex = 0;
            this._mkAddRouteBtn.Text = "Add Route";
            this._mkAddRouteBtn.UseVisualStyleBackColor = false;
            //
            // _mkEditRouteBtn
            //
            this._mkEditRouteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(80)))));
            this._mkEditRouteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._mkEditRouteBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._mkEditRouteBtn.Location = new System.Drawing.Point(106, 4);
            this._mkEditRouteBtn.Name = "_mkEditRouteBtn";
            this._mkEditRouteBtn.Size = new System.Drawing.Size(75, 26);
            this._mkEditRouteBtn.TabIndex = 1;
            this._mkEditRouteBtn.Text = "Edit";
            this._mkEditRouteBtn.UseVisualStyleBackColor = false;
            //
            // _mkDeleteRouteBtn
            //
            this._mkDeleteRouteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._mkDeleteRouteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._mkDeleteRouteBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._mkDeleteRouteBtn.Location = new System.Drawing.Point(188, 4);
            this._mkDeleteRouteBtn.Name = "_mkDeleteRouteBtn";
            this._mkDeleteRouteBtn.Size = new System.Drawing.Size(75, 26);
            this._mkDeleteRouteBtn.TabIndex = 2;
            this._mkDeleteRouteBtn.Text = "Delete";
            this._mkDeleteRouteBtn.UseVisualStyleBackColor = false;
            //
            // _bqVillageListPanel
            //
            this._bqVillageListPanel.AutoScroll = true;
            this._bqVillageListPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._bqVillageListPanel.Controls.Add(this._bqVillageListPanelPlaceholder);
            this._bqVillageListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bqVillageListPanel.Location = new System.Drawing.Point(0, 22);
            this._bqVillageListPanel.Name = "_bqVillageListPanel";
            this._bqVillageListPanel.Size = new System.Drawing.Size(1142, 384);
            this._bqVillageListPanel.TabIndex = 2;
            //
            // _bqVillageListPanelPlaceholder
            //
            this._bqVillageListPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bqVillageListPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._bqVillageListPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._bqVillageListPanelPlaceholder.Name = "_bqVillageListPanelPlaceholder";
            this._bqVillageListPanelPlaceholder.TabIndex = 99;
            this._bqVillageListPanelPlaceholder.Text = "〈 Village banquet rows — one per village, populated when world loads 〉";
            this._bqVillageListPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._bqVillageListPanelPlaceholder.Visible = false;
            //
            // _bqColHeader
            //
            this._bqColHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this._bqColHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._bqColHeader.Location = new System.Drawing.Point(0, 0);
            this._bqColHeader.Name = "_bqColHeader";
            this._bqColHeader.Size = new System.Drawing.Size(1142, 22);
            this._bqColHeader.TabIndex = 1;
            //
            // _bqSeparator
            //
            this._bqSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this._bqSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._bqSeparator.Location = new System.Drawing.Point(0, 90);
            this._bqSeparator.Name = "_bqSeparator";
            this._bqSeparator.Size = new System.Drawing.Size(1142, 1);
            this._bqSeparator.TabIndex = 3;
            //
            // _bqSettingsPanel
            //
            this._bqSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this._bqSettingsPanel.Controls.Add(this._bqCopySettingsBtn);
            this._bqSettingsPanel.Controls.Add(this._bqRunNowBtn);
            this._bqSettingsPanel.Controls.Add(this._bqRefreshBtn);
            this._bqSettingsPanel.Controls.Add(this._bqDelayInput);
            this._bqSettingsPanel.Controls.Add(this._bqDelayLabel);
            this._bqSettingsPanel.Controls.Add(this._bqIntervalInput);
            this._bqSettingsPanel.Controls.Add(this._bqIntervalLabel);
            this._bqSettingsPanel.Controls.Add(this._bqStatusLabel);
            this._bqSettingsPanel.Controls.Add(this._bqEnabledCheck);
            this._bqSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._bqSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._bqSettingsPanel.Name = "_bqSettingsPanel";
            this._bqSettingsPanel.Size = new System.Drawing.Size(1142, 90);
            this._bqSettingsPanel.TabIndex = 0;
            //
            // _bqCopySettingsBtn
            //
            this._bqCopySettingsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(80)))));
            this._bqCopySettingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bqCopySettingsBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bqCopySettingsBtn.Location = new System.Drawing.Point(521, 39);
            this._bqCopySettingsBtn.Name = "_bqCopySettingsBtn";
            this._bqCopySettingsBtn.Size = new System.Drawing.Size(100, 26);
            this._bqCopySettingsBtn.TabIndex = 8;
            this._bqCopySettingsBtn.Text = "Copy Settings";
            this._bqCopySettingsBtn.UseVisualStyleBackColor = false;
            //
            // _bqRunNowBtn
            //
            this._bqRunNowBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(80)))), ((int)(((byte)(50)))));
            this._bqRunNowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bqRunNowBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bqRunNowBtn.Location = new System.Drawing.Point(431, 39);
            this._bqRunNowBtn.Name = "_bqRunNowBtn";
            this._bqRunNowBtn.Size = new System.Drawing.Size(80, 26);
            this._bqRunNowBtn.TabIndex = 7;
            this._bqRunNowBtn.Text = "Run Now";
            this._bqRunNowBtn.UseVisualStyleBackColor = false;
            //
            // _bqRefreshBtn
            //
            this._bqRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this._bqRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bqRefreshBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bqRefreshBtn.Location = new System.Drawing.Point(346, 39);
            this._bqRefreshBtn.Name = "_bqRefreshBtn";
            this._bqRefreshBtn.Size = new System.Drawing.Size(75, 26);
            this._bqRefreshBtn.TabIndex = 6;
            this._bqRefreshBtn.Text = "Refresh";
            this._bqRefreshBtn.UseVisualStyleBackColor = false;
            //
            // _bqDelayInput
            //
            this._bqDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._bqDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bqDelayInput.Location = new System.Drawing.Point(256, 41);
            this._bqDelayInput.Maximum = new decimal(new int[] { 30000, 0, 0, 0 });
            this._bqDelayInput.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this._bqDelayInput.Name = "_bqDelayInput";
            this._bqDelayInput.Size = new System.Drawing.Size(75, 23);
            this._bqDelayInput.TabIndex = 5;
            this._bqDelayInput.Value = new decimal(new int[] { 1500, 0, 0, 0 });
            //
            // _bqDelayLabel
            //
            this._bqDelayLabel.AutoSize = true;
            this._bqDelayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._bqDelayLabel.Location = new System.Drawing.Point(176, 44);
            this._bqDelayLabel.Name = "_bqDelayLabel";
            this._bqDelayLabel.Size = new System.Drawing.Size(68, 15);
            this._bqDelayLabel.TabIndex = 4;
            this._bqDelayLabel.Text = "Delay (ms):";
            //
            // _bqIntervalInput
            //
            this._bqIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._bqIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bqIntervalInput.Location = new System.Drawing.Point(96, 41);
            this._bqIntervalInput.Maximum = new decimal(new int[] { 86400, 0, 0, 0 });
            this._bqIntervalInput.Minimum = new decimal(new int[] { 30, 0, 0, 0 });
            this._bqIntervalInput.Name = "_bqIntervalInput";
            this._bqIntervalInput.Size = new System.Drawing.Size(65, 23);
            this._bqIntervalInput.TabIndex = 3;
            this._bqIntervalInput.Value = new decimal(new int[] { 300, 0, 0, 0 });
            //
            // _bqIntervalLabel
            //
            this._bqIntervalLabel.AutoSize = true;
            this._bqIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._bqIntervalLabel.Location = new System.Drawing.Point(16, 44);
            this._bqIntervalLabel.Name = "_bqIntervalLabel";
            this._bqIntervalLabel.Size = new System.Drawing.Size(70, 15);
            this._bqIntervalLabel.TabIndex = 2;
            this._bqIntervalLabel.Text = "Interval (s):";
            //
            // _bqStatusLabel
            //
            this._bqStatusLabel.AutoSize = true;
            this._bqStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._bqStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._bqStatusLabel.Location = new System.Drawing.Point(186, 16);
            this._bqStatusLabel.Name = "_bqStatusLabel";
            this._bqStatusLabel.Size = new System.Drawing.Size(59, 13);
            this._bqStatusLabel.TabIndex = 1;
            this._bqStatusLabel.Text = "DISABLED";
            //
            // _bqEnabledCheck
            //
            this._bqEnabledCheck.AutoSize = true;
            this._bqEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._bqEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._bqEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._bqEnabledCheck.Location = new System.Drawing.Point(16, 12);
            this._bqEnabledCheck.Name = "_bqEnabledCheck";
            this._bqEnabledCheck.Size = new System.Drawing.Size(143, 23);
            this._bqEnabledCheck.TabIndex = 0;
            this._bqEnabledCheck.Text = "Enable Banquet";
            //
            // _scoutPage
            //
            this._scoutPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._scoutPage.Controls.Add(this._scContentPanel);
            this._scoutPage.Controls.Add(this._scDivider);
            this._scoutPage.Controls.Add(this._scVillagePanel);
            this._scoutPage.Controls.Add(this._scSeparator);
            this._scoutPage.Controls.Add(this._scSettingsPanel);
            // NOTE: Add order is intentional — last-added is docked first in WinForms.
            // _scSettingsPanel (Top, last) docks to very top; _scSeparator (Top) docks just below it;
            // then Left panels fill the remaining area; _scContentPanel (Fill) takes the rest.
            this._scoutPage.Location = new System.Drawing.Point(4, 24);
            this._scoutPage.Name = "_scoutPage";
            this._scoutPage.Size = new System.Drawing.Size(1142, 497);
            this._scoutPage.TabIndex = 9;
            this._scoutPage.Text = "Scout";
            //
            // _scSettingsPanel  (docked Top — last added so docks to the very top first)
            //
            this._scSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this._scSettingsPanel.Controls.Add(this._scEnabledCheck);
            this._scSettingsPanel.Controls.Add(this._scStatusLabel);
            this._scSettingsPanel.Controls.Add(this._scIntervalLabel);
            this._scSettingsPanel.Controls.Add(this._scIntervalInput);
            this._scSettingsPanel.Controls.Add(this._scMaxTimeLabel);
            this._scSettingsPanel.Controls.Add(this._scMaxTimeInput);
            this._scSettingsPanel.Controls.Add(this._scAutoHireLabel);
            this._scSettingsPanel.Controls.Add(this._scAutoHireInput);
            this._scSettingsPanel.Controls.Add(this._scDelayLabel);
            this._scSettingsPanel.Controls.Add(this._scDelayInput);
            this._scSettingsPanel.Controls.Add(this._scDisableOnCardExpiryCheck);
            this._scSettingsPanel.Controls.Add(this._scPriorityLabel);
            this._scSettingsPanel.Controls.Add(this._scPriorityResourceRadio);
            this._scSettingsPanel.Controls.Add(this._scPriorityRangeRadio);
            this._scSettingsPanel.Controls.Add(this._scSendOneScoutCheck);
            this._scSettingsPanel.Controls.Add(this._scSendOneOnNewCheck);
            this._scSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._scSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._scSettingsPanel.Name = "_scSettingsPanel";
            this._scSettingsPanel.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this._scSettingsPanel.Size = new System.Drawing.Size(1142, 90);
            this._scSettingsPanel.TabIndex = 0;
            //
            // _scEnabledCheck
            //
            this._scEnabledCheck.AutoSize = true;
            this._scEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._scEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scEnabledCheck.Location = new System.Drawing.Point(8, 8);
            this._scEnabledCheck.Name = "_scEnabledCheck";
            this._scEnabledCheck.Size = new System.Drawing.Size(130, 23);
            this._scEnabledCheck.TabIndex = 0;
            this._scEnabledCheck.Text = "Enable Scout";
            //
            // _scStatusLabel
            //
            this._scStatusLabel.AutoSize = true;
            this._scStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._scStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this._scStatusLabel.Location = new System.Drawing.Point(160, 10);
            this._scStatusLabel.Name = "_scStatusLabel";
            this._scStatusLabel.Size = new System.Drawing.Size(60, 15);
            this._scStatusLabel.TabIndex = 1;
            this._scStatusLabel.Text = "DISABLED";
            //
            // _scIntervalLabel
            //
            this._scIntervalLabel.AutoSize = true;
            this._scIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._scIntervalLabel.Location = new System.Drawing.Point(290, 10);
            this._scIntervalLabel.Name = "_scIntervalLabel";
            this._scIntervalLabel.Size = new System.Drawing.Size(120, 13);
            this._scIntervalLabel.TabIndex = 2;
            this._scIntervalLabel.Text = "Cycle interval (sec):";
            //
            // _scIntervalInput
            //
            this._scIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._scIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scIntervalInput.Location = new System.Drawing.Point(450, 7);
            this._scIntervalInput.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            this._scIntervalInput.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this._scIntervalInput.Name = "_scIntervalInput";
            this._scIntervalInput.Size = new System.Drawing.Size(70, 23);
            this._scIntervalInput.TabIndex = 3;
            this._scIntervalInput.Value = new decimal(new int[] { 60, 0, 0, 0 });
            //
            // _scMaxTimeLabel
            //
            this._scMaxTimeLabel.AutoSize = true;
            this._scMaxTimeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._scMaxTimeLabel.Location = new System.Drawing.Point(8, 39);
            this._scMaxTimeLabel.Name = "_scMaxTimeLabel";
            this._scMaxTimeLabel.Size = new System.Drawing.Size(130, 13);
            this._scMaxTimeLabel.TabIndex = 4;
            this._scMaxTimeLabel.Text = "Max scout time (sec):";
            //
            // _scMaxTimeInput
            //
            this._scMaxTimeInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._scMaxTimeInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scMaxTimeInput.Location = new System.Drawing.Point(160, 36);
            this._scMaxTimeInput.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            this._scMaxTimeInput.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this._scMaxTimeInput.Name = "_scMaxTimeInput";
            this._scMaxTimeInput.Size = new System.Drawing.Size(70, 23);
            this._scMaxTimeInput.TabIndex = 5;
            this._scMaxTimeInput.Value = new decimal(new int[] { 1200, 0, 0, 0 });
            //
            // _scAutoHireLabel
            //
            this._scAutoHireLabel.AutoSize = true;
            this._scAutoHireLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._scAutoHireLabel.Location = new System.Drawing.Point(244, 39);
            this._scAutoHireLabel.Name = "_scAutoHireLabel";
            this._scAutoHireLabel.Size = new System.Drawing.Size(155, 13);
            this._scAutoHireLabel.TabIndex = 6;
            this._scAutoHireLabel.Text = "Auto hire scouts (0=off):";
            //
            // _scAutoHireInput
            //
            this._scAutoHireInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._scAutoHireInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scAutoHireInput.Location = new System.Drawing.Point(420, 36);
            this._scAutoHireInput.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            this._scAutoHireInput.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this._scAutoHireInput.Name = "_scAutoHireInput";
            this._scAutoHireInput.Size = new System.Drawing.Size(55, 23);
            this._scAutoHireInput.TabIndex = 7;
            this._scAutoHireInput.Value = new decimal(new int[] { 0, 0, 0, 0 });
            //
            // _scDelayLabel
            //
            this._scDelayLabel.AutoSize = true;
            this._scDelayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._scDelayLabel.Location = new System.Drawing.Point(490, 39);
            this._scDelayLabel.Name = "_scDelayLabel";
            this._scDelayLabel.Size = new System.Drawing.Size(170, 13);
            this._scDelayLabel.TabIndex = 8;
            this._scDelayLabel.Text = "Delay between sends (ms):";
            //
            // _scDelayInput
            //
            this._scDelayInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._scDelayInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scDelayInput.Location = new System.Drawing.Point(695, 36);
            this._scDelayInput.Maximum = new decimal(new int[] { 60000, 0, 0, 0 });
            this._scDelayInput.Minimum = new decimal(new int[] { 500, 0, 0, 0 });
            this._scDelayInput.Name = "_scDelayInput";
            this._scDelayInput.Size = new System.Drawing.Size(75, 23);
            this._scDelayInput.TabIndex = 9;
            this._scDelayInput.Value = new decimal(new int[] { 3000, 0, 0, 0 });
            //
            // _scDisableOnCardExpiryCheck
            //
            this._scDisableOnCardExpiryCheck.AutoSize = true;
            this._scDisableOnCardExpiryCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scDisableOnCardExpiryCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scDisableOnCardExpiryCheck.Location = new System.Drawing.Point(790, 36);
            this._scDisableOnCardExpiryCheck.Name = "_scDisableOnCardExpiryCheck";
            this._scDisableOnCardExpiryCheck.Size = new System.Drawing.Size(210, 19);
            this._scDisableOnCardExpiryCheck.TabIndex = 10;
            this._scDisableOnCardExpiryCheck.Text = "Disable on scout card expiry";
            //
            // _scPriorityLabel
            //
            this._scPriorityLabel.AutoSize = true;
            this._scPriorityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._scPriorityLabel.Location = new System.Drawing.Point(8, 64);
            this._scPriorityLabel.Name = "_scPriorityLabel";
            this._scPriorityLabel.Size = new System.Drawing.Size(75, 13);
            this._scPriorityLabel.TabIndex = 11;
            this._scPriorityLabel.Text = "Scout order:";
            //
            // _scPriorityResourceRadio
            //
            this._scPriorityResourceRadio.AutoSize = true;
            this._scPriorityResourceRadio.Checked = true;
            this._scPriorityResourceRadio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scPriorityResourceRadio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scPriorityResourceRadio.Location = new System.Drawing.Point(90, 62);
            this._scPriorityResourceRadio.Name = "_scPriorityResourceRadio";
            this._scPriorityResourceRadio.Size = new System.Drawing.Size(230, 19);
            this._scPriorityResourceRadio.TabIndex = 12;
            this._scPriorityResourceRadio.TabStop = true;
            this._scPriorityResourceRadio.Text = "Resource Priority (type order in list)";
            //
            // _scPriorityRangeRadio
            //
            this._scPriorityRangeRadio.AutoSize = true;
            this._scPriorityRangeRadio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scPriorityRangeRadio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scPriorityRangeRadio.Location = new System.Drawing.Point(330, 62);
            this._scPriorityRangeRadio.Name = "_scPriorityRangeRadio";
            this._scPriorityRangeRadio.Size = new System.Drawing.Size(200, 19);
            this._scPriorityRangeRadio.TabIndex = 13;
            this._scPriorityRangeRadio.Text = "Range Priority (nearest first)";
            //
            // _scSendOneScoutCheck
            //
            this._scSendOneScoutCheck.AutoSize = true;
            this._scSendOneScoutCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scSendOneScoutCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scSendOneScoutCheck.Location = new System.Drawing.Point(570, 62);
            this._scSendOneScoutCheck.Name = "_scSendOneScoutCheck";
            this._scSendOneScoutCheck.Size = new System.Drawing.Size(280, 19);
            this._scSendOneScoutCheck.TabIndex = 14;
            this._scSendOneScoutCheck.Text = "Send 1 scout per stash (ignore stash size)";
            //
            // _scSendOneOnNewCheck
            //
            this._scSendOneOnNewCheck.AutoSize = true;
            this._scSendOneOnNewCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scSendOneOnNewCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scSendOneOnNewCheck.Location = new System.Drawing.Point(850, 62);
            this._scSendOneOnNewCheck.Name = "_scSendOneOnNewCheck";
            this._scSendOneOnNewCheck.Size = new System.Drawing.Size(300, 19);
            this._scSendOneOnNewCheck.TabIndex = 15;
            this._scSendOneOnNewCheck.Text = "Send 1 scout on New Stash (to discover type)";
            //
            // _scSeparator  (docked Top — docks just below _scSettingsPanel)
            //
            this._scSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this._scSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this._scSeparator.Location = new System.Drawing.Point(0, 90);
            this._scSeparator.Name = "_scSeparator";
            this._scSeparator.Size = new System.Drawing.Size(1142, 1);
            this._scSeparator.TabIndex = 4;
            //
            // _scVillagePanel  (docked Left, fills height below settings+separator)
            //
            this._scVillagePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(38)))));
            this._scVillagePanel.Controls.Add(this._scVillageListBox);
            this._scVillagePanel.Controls.Add(this._scVillageHeaderLabel);
            this._scVillagePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this._scVillagePanel.Location = new System.Drawing.Point(0, 91);
            this._scVillagePanel.Name = "_scVillagePanel";
            this._scVillagePanel.Size = new System.Drawing.Size(220, 406);
            this._scVillagePanel.TabIndex = 1;
            //
            // _scVillageListBox
            //
            this._scVillageListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this._scVillageListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._scVillageListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._scVillageListBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._scVillageListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scVillageListBox.Name = "_scVillageListBox";
            this._scVillageListBox.Size = new System.Drawing.Size(220, 384);
            this._scVillageListBox.TabIndex = 0;
            //
            // _scVillageHeaderLabel
            //
            this._scVillageHeaderLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this._scVillageHeaderLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._scVillageHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._scVillageHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._scVillageHeaderLabel.Location = new System.Drawing.Point(0, 0);
            this._scVillageHeaderLabel.Name = "_scVillageHeaderLabel";
            this._scVillageHeaderLabel.Padding = new System.Windows.Forms.Padding(6, 4, 0, 0);
            this._scVillageHeaderLabel.Size = new System.Drawing.Size(220, 22);
            this._scVillageHeaderLabel.TabIndex = 1;
            this._scVillageHeaderLabel.Text = "Villages";
            //
            // _scDivider
            //
            this._scDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this._scDivider.Dock = System.Windows.Forms.DockStyle.Left;
            this._scDivider.Location = new System.Drawing.Point(220, 91);
            this._scDivider.Name = "_scDivider";
            this._scDivider.Size = new System.Drawing.Size(2, 406);
            this._scDivider.TabIndex = 2;
            //
            // _scContentPanel  (Fill — takes everything remaining)
            //
            this._scContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._scContentPanel.Controls.Add(this._scVillageEnabledCheck);
            this._scContentPanel.Controls.Add(this._scScoutListLabel);
            this._scContentPanel.Controls.Add(this._scIgnoreListLabel);
            this._scContentPanel.Controls.Add(this._scScoutList);
            this._scContentPanel.Controls.Add(this._scIgnoreList);
            this._scContentPanel.Controls.Add(this._scMoveUpBtn);
            this._scContentPanel.Controls.Add(this._scMoveDownBtn);
            this._scContentPanel.Controls.Add(this._scMoveToIgnoreBtn);
            this._scContentPanel.Controls.Add(this._scMoveToScoutBtn);
            this._scContentPanel.Controls.Add(this._scCopySettingsBtn);
            this._scContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._scContentPanel.Location = new System.Drawing.Point(222, 91);
            this._scContentPanel.Name = "_scContentPanel";
            this._scContentPanel.Size = new System.Drawing.Size(920, 406);
            this._scContentPanel.TabIndex = 3;
            //
            // _scVillageEnabledCheck
            //
            this._scVillageEnabledCheck.AutoSize = true;
            this._scVillageEnabledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scVillageEnabledCheck.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._scVillageEnabledCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scVillageEnabledCheck.Location = new System.Drawing.Point(10, 8);
            this._scVillageEnabledCheck.Name = "_scVillageEnabledCheck";
            this._scVillageEnabledCheck.Size = new System.Drawing.Size(160, 23);
            this._scVillageEnabledCheck.TabIndex = 0;
            this._scVillageEnabledCheck.Text = "Scout this village";
            //
            // _scScoutListLabel
            //
            this._scScoutListLabel.AutoSize = true;
            this._scScoutListLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._scScoutListLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._scScoutListLabel.Location = new System.Drawing.Point(10, 36);
            this._scScoutListLabel.Name = "_scScoutListLabel";
            this._scScoutListLabel.Size = new System.Drawing.Size(130, 15);
            this._scScoutListLabel.TabIndex = 1;
            this._scScoutListLabel.Text = "Resources to Scout";
            //
            // _scIgnoreListLabel
            //
            this._scIgnoreListLabel.AutoSize = true;
            this._scIgnoreListLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._scIgnoreListLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._scIgnoreListLabel.Location = new System.Drawing.Point(420, 36);
            this._scIgnoreListLabel.Name = "_scIgnoreListLabel";
            this._scIgnoreListLabel.Size = new System.Drawing.Size(135, 15);
            this._scIgnoreListLabel.TabIndex = 2;
            this._scIgnoreListLabel.Text = "Resources to Ignore";
            //
            // _scScoutList
            //
            this._scScoutList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this._scScoutList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._scScoutList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scScoutList.Location = new System.Drawing.Point(10, 56);
            this._scScoutList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this._scScoutList.Name = "_scScoutList";
            this._scScoutList.Size = new System.Drawing.Size(290, 310);
            this._scScoutList.TabIndex = 3;
            //
            // _scIgnoreList
            //
            this._scIgnoreList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this._scIgnoreList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._scIgnoreList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scIgnoreList.Location = new System.Drawing.Point(420, 56);
            this._scIgnoreList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this._scIgnoreList.Name = "_scIgnoreList";
            this._scIgnoreList.Size = new System.Drawing.Size(290, 310);
            this._scIgnoreList.TabIndex = 4;
            //
            // _scMoveUpBtn
            //
            this._scMoveUpBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this._scMoveUpBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scMoveUpBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scMoveUpBtn.Location = new System.Drawing.Point(308, 56);
            this._scMoveUpBtn.Name = "_scMoveUpBtn";
            this._scMoveUpBtn.Size = new System.Drawing.Size(30, 26);
            this._scMoveUpBtn.TabIndex = 5;
            this._scMoveUpBtn.Text = "▲";
            //
            // _scMoveDownBtn
            //
            this._scMoveDownBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this._scMoveDownBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scMoveDownBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scMoveDownBtn.Location = new System.Drawing.Point(308, 86);
            this._scMoveDownBtn.Name = "_scMoveDownBtn";
            this._scMoveDownBtn.Size = new System.Drawing.Size(30, 26);
            this._scMoveDownBtn.TabIndex = 6;
            this._scMoveDownBtn.Text = "▼";
            //
            // _scMoveToIgnoreBtn
            //
            this._scMoveToIgnoreBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this._scMoveToIgnoreBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scMoveToIgnoreBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scMoveToIgnoreBtn.Location = new System.Drawing.Point(353, 180);
            this._scMoveToIgnoreBtn.Name = "_scMoveToIgnoreBtn";
            this._scMoveToIgnoreBtn.Size = new System.Drawing.Size(50, 26);
            this._scMoveToIgnoreBtn.TabIndex = 7;
            this._scMoveToIgnoreBtn.Text = ">>";
            //
            // _scMoveToScoutBtn
            //
            this._scMoveToScoutBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this._scMoveToScoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scMoveToScoutBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scMoveToScoutBtn.Location = new System.Drawing.Point(353, 214);
            this._scMoveToScoutBtn.Name = "_scMoveToScoutBtn";
            this._scMoveToScoutBtn.Size = new System.Drawing.Size(50, 26);
            this._scMoveToScoutBtn.TabIndex = 8;
            this._scMoveToScoutBtn.Text = "<<";
            //
            // _scCopySettingsBtn
            //
            this._scCopySettingsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(50)))));
            this._scCopySettingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scCopySettingsBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._scCopySettingsBtn.Location = new System.Drawing.Point(10, 378);
            this._scCopySettingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._scCopySettingsBtn.Name = "_scCopySettingsBtn";
            this._scCopySettingsBtn.Size = new System.Drawing.Size(160, 26);
            this._scCopySettingsBtn.TabIndex = 9;
            this._scCopySettingsBtn.Text = "Copy to all villages";
            //
            // _miscPage
            //
            this._miscPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._miscPage.Controls.Add(this._miscSettingsPanel);
            this._miscPage.Location = new System.Drawing.Point(4, 24);
            this._miscPage.Name = "_miscPage";
            this._miscPage.Size = new System.Drawing.Size(1142, 497);
            this._miscPage.TabIndex = 8;
            this._miscPage.Text = "Misc";
            // 
            // _miscSettingsPanel
            // 
            this._miscSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this._miscSettingsPanel.Controls.Add(this._miscSaleEndValue);
            this._miscSettingsPanel.Controls.Add(this._miscSaleEndLabel);
            this._miscSettingsPanel.Controls.Add(this._miscSaleStartValue);
            this._miscSettingsPanel.Controls.Add(this._miscSaleStartLabel);
            this._miscSettingsPanel.Controls.Add(this._miscSalePctValue);
            this._miscSettingsPanel.Controls.Add(this._miscSalePctLabel);
            this._miscSettingsPanel.Controls.Add(this._miscSaleHeaderLabel);
            this._miscSettingsPanel.Controls.Add(this._miscCollectFreeCardsCheck);
            this._miscSettingsPanel.Controls.Add(this._miscDisableCannotPlayCardCheck);
            this._miscSettingsPanel.Controls.Add(this._miscShowOtherTraderInfoCheck);
            this._miscSettingsPanel.Controls.Add(this._miscWorldMapParishBuildingCountCheck);
            this._miscSettingsPanel.Controls.Add(this._miscShowUserScreenInfoCheck);
            this._miscSettingsPanel.Controls.Add(this._miscMapAttackTypeIconsCheck);
            this._miscSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._miscSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._miscSettingsPanel.Name = "_miscSettingsPanel";
            this._miscSettingsPanel.Padding = new System.Windows.Forms.Padding(16);
            this._miscSettingsPanel.Size = new System.Drawing.Size(1142, 226);
            this._miscSettingsPanel.TabIndex = 0;
            // 
            // _miscCollectFreeCardsCheck
            // 
            this._miscCollectFreeCardsCheck.AutoSize = true;
            this._miscCollectFreeCardsCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._miscCollectFreeCardsCheck.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._miscCollectFreeCardsCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._miscCollectFreeCardsCheck.Location = new System.Drawing.Point(16, 20);
            this._miscCollectFreeCardsCheck.Name = "_miscCollectFreeCardsCheck";
            this._miscCollectFreeCardsCheck.Size = new System.Drawing.Size(214, 23);
            this._miscCollectFreeCardsCheck.TabIndex = 0;
            this._miscCollectFreeCardsCheck.Text = "Collect free cards automatically";
            //
            // _miscDisableCannotPlayCardCheck
            //
            this._miscDisableCannotPlayCardCheck.AutoSize = true;
            this._miscDisableCannotPlayCardCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._miscDisableCannotPlayCardCheck.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._miscDisableCannotPlayCardCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._miscDisableCannotPlayCardCheck.Location = new System.Drawing.Point(16, 46);
            this._miscDisableCannotPlayCardCheck.Name = "_miscDisableCannotPlayCardCheck";
            this._miscDisableCannotPlayCardCheck.Size = new System.Drawing.Size(214, 23);
            this._miscDisableCannotPlayCardCheck.TabIndex = 2;
            this._miscDisableCannotPlayCardCheck.Text = "Disable can't play card popup";
            //
            // _miscShowOtherTraderInfoCheck
            //
            this._miscShowOtherTraderInfoCheck.AutoSize = true;
            this._miscShowOtherTraderInfoCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._miscShowOtherTraderInfoCheck.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._miscShowOtherTraderInfoCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._miscShowOtherTraderInfoCheck.Location = new System.Drawing.Point(16, 72);
            this._miscShowOtherTraderInfoCheck.Name = "_miscShowOtherTraderInfoCheck";
            this._miscShowOtherTraderInfoCheck.Size = new System.Drawing.Size(214, 23);
            this._miscShowOtherTraderInfoCheck.TabIndex = 3;
            this._miscShowOtherTraderInfoCheck.Text = "Show Other Trader Info";
            //
            // _miscWorldMapParishBuildingCountCheck
            //
            this._miscWorldMapParishBuildingCountCheck.AutoSize = true;
            this._miscWorldMapParishBuildingCountCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._miscWorldMapParishBuildingCountCheck.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._miscWorldMapParishBuildingCountCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._miscWorldMapParishBuildingCountCheck.Location = new System.Drawing.Point(16, 98);
            this._miscWorldMapParishBuildingCountCheck.Name = "_miscWorldMapParishBuildingCountCheck";
            this._miscWorldMapParishBuildingCountCheck.Size = new System.Drawing.Size(214, 23);
            this._miscWorldMapParishBuildingCountCheck.TabIndex = 4;
            this._miscWorldMapParishBuildingCountCheck.Text = "World Map Parish Building Count";
            //
            // _miscShowUserScreenInfoCheck
            //
            this._miscShowUserScreenInfoCheck.AutoSize = true;
            this._miscShowUserScreenInfoCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._miscShowUserScreenInfoCheck.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._miscShowUserScreenInfoCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._miscShowUserScreenInfoCheck.Location = new System.Drawing.Point(16, 124);
            this._miscShowUserScreenInfoCheck.Name = "_miscShowUserScreenInfoCheck";
            this._miscShowUserScreenInfoCheck.Size = new System.Drawing.Size(214, 23);
            this._miscShowUserScreenInfoCheck.TabIndex = 5;
            this._miscShowUserScreenInfoCheck.Text = "Show User Screen Info";
            //
            // _miscMapAttackTypeIconsCheck
            //
            this._miscMapAttackTypeIconsCheck.AutoSize = true;
            this._miscMapAttackTypeIconsCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._miscMapAttackTypeIconsCheck.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._miscMapAttackTypeIconsCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._miscMapAttackTypeIconsCheck.Location = new System.Drawing.Point(16, 150);
            this._miscMapAttackTypeIconsCheck.Name = "_miscMapAttackTypeIconsCheck";
            this._miscMapAttackTypeIconsCheck.Size = new System.Drawing.Size(214, 23);
            this._miscMapAttackTypeIconsCheck.TabIndex = 6;
            this._miscMapAttackTypeIconsCheck.Text = "Map Attack Type Icons";
            //
            // _miscSaleHeaderLabel
            //
            this._miscSaleHeaderLabel.AutoSize = true;
            this._miscSaleHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._miscSaleHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._miscSaleHeaderLabel.Location = new System.Drawing.Point(300, 20);
            this._miscSaleHeaderLabel.Name = "_miscSaleHeaderLabel";
            this._miscSaleHeaderLabel.Text = "Sale Info";
            //
            // _miscSalePctLabel
            //
            this._miscSalePctLabel.AutoSize = true;
            this._miscSalePctLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._miscSalePctLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._miscSalePctLabel.Location = new System.Drawing.Point(300, 46);
            this._miscSalePctLabel.Name = "_miscSalePctLabel";
            this._miscSalePctLabel.Text = "Sale %:";
            //
            // _miscSalePctValue
            //
            this._miscSalePctValue.AutoSize = true;
            this._miscSalePctValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._miscSalePctValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._miscSalePctValue.Location = new System.Drawing.Point(380, 46);
            this._miscSalePctValue.Name = "_miscSalePctValue";
            this._miscSalePctValue.Text = "—";
            //
            // _miscSaleStartLabel
            //
            this._miscSaleStartLabel.AutoSize = true;
            this._miscSaleStartLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._miscSaleStartLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._miscSaleStartLabel.Location = new System.Drawing.Point(300, 72);
            this._miscSaleStartLabel.Name = "_miscSaleStartLabel";
            this._miscSaleStartLabel.Text = "Start:";
            //
            // _miscSaleStartValue
            //
            this._miscSaleStartValue.AutoSize = true;
            this._miscSaleStartValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._miscSaleStartValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._miscSaleStartValue.Location = new System.Drawing.Point(380, 72);
            this._miscSaleStartValue.Name = "_miscSaleStartValue";
            this._miscSaleStartValue.Text = "—";
            //
            // _miscSaleEndLabel
            //
            this._miscSaleEndLabel.AutoSize = true;
            this._miscSaleEndLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._miscSaleEndLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._miscSaleEndLabel.Location = new System.Drawing.Point(300, 98);
            this._miscSaleEndLabel.Name = "_miscSaleEndLabel";
            this._miscSaleEndLabel.Text = "End:";
            //
            // _miscSaleEndValue
            //
            this._miscSaleEndValue.AutoSize = true;
            this._miscSaleEndValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._miscSaleEndValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._miscSaleEndValue.Location = new System.Drawing.Point(380, 98);
            this._miscSaleEndValue.Name = "_miscSaleEndValue";
            this._miscSaleEndValue.Text = "—";
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
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
            ((System.ComponentModel.ISupportInitialize)(this._rdMinAttacksInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rdMinAttacksWindowInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rdMaxLandTimeInput)).EndInit();
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
            this._builderPage.ResumeLayout(false);
            this._bldNavPanel.ResumeLayout(false);
            this._bldNavPanel.PerformLayout();
            this._bldSettingsPanel.ResumeLayout(false);
            this._bldSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bldDelayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bldIntervalInput)).EndInit();
            this._bombPage.ResumeLayout(false);
            this._bombSubTabs.ResumeLayout(false);
            this._bombSetupTab.ResumeLayout(false);
            this._abSettingsPanel.ResumeLayout(false);
            this._abSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._abStackDelayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._abTargetInput)).EndInit();
            this._bombPendingTab.ResumeLayout(false);
            this._abPendingSettingsPanel.ResumeLayout(false);
            this._bombTargetQueueTab.ResumeLayout(false);
            this._abQueueSettingsPanel.ResumeLayout(false);
            this._abQueueSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._abQueueVillageIdInput)).EndInit();
            this._bombMultiPage.ResumeLayout(false);
            this._abmSubTabs.ResumeLayout(false);
            this._abmPlayersTab.ResumeLayout(false);
            this._abmPendingTab.ResumeLayout(false);
            this._abmQueueTab.ResumeLayout(false);
            this._abmConnPanel.ResumeLayout(false);
            this._abmCtrlPanel.ResumeLayout(false);
            this._abmQueueSettingsPanel.ResumeLayout(false);
            this._abmQueueSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._abmStackDelayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._abmQueueVidInput)).EndInit();
            this._bqPage.ResumeLayout(false);
            this._defenderPage.ResumeLayout(false);
            this._dfSettingsPanel.ResumeLayout(false);
            this._dfSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dfDurationInput)).EndInit();
            this._dfCardsPanel.ResumeLayout(false);
            this._dfCardsPanel.PerformLayout();
            this._dfActionsPanel.ResumeLayout(false);
            this._dfActionsPanel.PerformLayout();
            this._mkPage.ResumeLayout(false);
            this._mkColHeader.ResumeLayout(false);
            this._mkSettingsPanel.ResumeLayout(false);
            this._mkSettingsPanel.PerformLayout();
            this._mkRouteButtonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._mkIntervalInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._mkDelayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._mkMonksToKeepInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._mkAutoRecruitInput)).EndInit();
            this._bqSettingsPanel.ResumeLayout(false);
            this._bqSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bqDelayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._bqIntervalInput)).EndInit();
            this._popularityPage.ResumeLayout(false);
            this._ppSettingsPanel.ResumeLayout(false);
            this._ppSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._ppDelayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._ppIntervalInput)).EndInit();
            this._scoutPage.ResumeLayout(false);
            this._scSettingsPanel.ResumeLayout(false);
            this._scSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._scIntervalInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._scMaxTimeInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._scAutoHireInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._scDelayInput)).EndInit();
            this._scVillagePanel.ResumeLayout(false);
            this._scContentPanel.ResumeLayout(false);
            this._miscPage.ResumeLayout(false);
            this._miscSettingsPanel.ResumeLayout(false);
            this._miscSettingsPanel.PerformLayout();
            //
            // _autoPage
            //
            this._autoPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._autoPage.Controls.Add(this._autoInnerTabs);
            this._autoPage.Location = new System.Drawing.Point(4, 24);
            this._autoPage.Name = "_autoPage";
            this._autoPage.Size = new System.Drawing.Size(1142, 497);
            this._autoPage.TabIndex = 9;
            this._autoPage.Text = "Auto";
            //
            // _autoInnerTabs
            //
            this._autoInnerTabs.Controls.Add(this._autoProdTab);
            this._autoInnerTabs.Controls.Add(this._autoModuleTab);
            this._autoInnerTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._autoInnerTabs.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._autoInnerTabs.Location = new System.Drawing.Point(0, 0);
            this._autoInnerTabs.Name = "_autoInnerTabs";
            this._autoInnerTabs.SelectedIndex = 0;
            this._autoInnerTabs.Size = new System.Drawing.Size(1142, 497);
            this._autoInnerTabs.TabIndex = 0;
            //
            // _autoProdTab
            //
            this._autoProdTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._autoProdTab.Controls.Add(this._autoProdScrollPanel);
            this._autoProdTab.Controls.Add(this._autoProdHeaderPanel);
            this._autoProdTab.Controls.Add(this._autoProdSettingsPanel);
            this._autoProdTab.Location = new System.Drawing.Point(4, 26);
            this._autoProdTab.Name = "_autoProdTab";
            this._autoProdTab.Size = new System.Drawing.Size(1134, 467);
            this._autoProdTab.TabIndex = 0;
            this._autoProdTab.Text = "Production";
            //
            // _autoProdScrollPanel
            //
            this._autoProdScrollPanel.AutoScroll = true;
            this._autoProdScrollPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._autoProdScrollPanel.Controls.Add(this._autoProdScrollPanelPlaceholder);
            this._autoProdScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._autoProdScrollPanel.Location = new System.Drawing.Point(0, 60);
            this._autoProdScrollPanel.Name = "_autoProdScrollPanel";
            this._autoProdScrollPanel.Size = new System.Drawing.Size(1134, 407);
            this._autoProdScrollPanel.TabIndex = 2;
            //
            // _autoProdScrollPanelPlaceholder
            //
            this._autoProdScrollPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._autoProdScrollPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._autoProdScrollPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._autoProdScrollPanelPlaceholder.Name = "_autoProdScrollPanelPlaceholder";
            this._autoProdScrollPanelPlaceholder.TabIndex = 99;
            this._autoProdScrollPanelPlaceholder.Text = "〈 Production card rows — one per tradeable good (Wood, Stone, Iron…) 〉";
            this._autoProdScrollPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._autoProdScrollPanelPlaceholder.Visible = false;
            //
            // _autoProdHeaderPanel
            //
            this._autoProdHeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this._autoProdHeaderPanel.Controls.Add(this._autoProdColGood);
            this._autoProdHeaderPanel.Controls.Add(this._autoProdColTier);
            this._autoProdHeaderPanel.Controls.Add(this._autoProdColTarget);
            this._autoProdHeaderPanel.Controls.Add(this._autoProdColDelay);
            this._autoProdHeaderPanel.Controls.Add(this._autoProdColProgress);
            this._autoProdHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._autoProdHeaderPanel.Location = new System.Drawing.Point(0, 36);
            this._autoProdHeaderPanel.Name = "_autoProdHeaderPanel";
            this._autoProdHeaderPanel.Size = new System.Drawing.Size(1134, 24);
            this._autoProdHeaderPanel.TabIndex = 1;
            //
            // _autoProdColGood
            //
            this._autoProdColGood.AutoSize = true;
            this._autoProdColGood.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoProdColGood.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoProdColGood.Location = new System.Drawing.Point(95, 4);
            this._autoProdColGood.Name = "_autoProdColGood";
            this._autoProdColGood.Size = new System.Drawing.Size(31, 13);
            this._autoProdColGood.TabIndex = 0;
            this._autoProdColGood.Text = "Good";
            //
            // _autoProdColTier
            //
            this._autoProdColTier.AutoSize = true;
            this._autoProdColTier.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoProdColTier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoProdColTier.Location = new System.Drawing.Point(215, 4);
            this._autoProdColTier.Name = "_autoProdColTier";
            this._autoProdColTier.Size = new System.Drawing.Size(24, 13);
            this._autoProdColTier.TabIndex = 1;
            this._autoProdColTier.Text = "Tier";
            //
            // _autoProdColTarget
            //
            this._autoProdColTarget.AutoSize = true;
            this._autoProdColTarget.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoProdColTarget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoProdColTarget.Location = new System.Drawing.Point(310, 4);
            this._autoProdColTarget.Name = "_autoProdColTarget";
            this._autoProdColTarget.Size = new System.Drawing.Size(38, 13);
            this._autoProdColTarget.TabIndex = 2;
            this._autoProdColTarget.Text = "Target";
            //
            // _autoProdColDelay
            //
            this._autoProdColDelay.AutoSize = true;
            this._autoProdColDelay.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoProdColDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoProdColDelay.Location = new System.Drawing.Point(430, 4);
            this._autoProdColDelay.Name = "_autoProdColDelay";
            this._autoProdColDelay.Size = new System.Drawing.Size(56, 13);
            this._autoProdColDelay.TabIndex = 3;
            this._autoProdColDelay.Text = "Start Delay";
            //
            // _autoProdColProgress
            //
            this._autoProdColProgress.AutoSize = true;
            this._autoProdColProgress.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoProdColProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoProdColProgress.Location = new System.Drawing.Point(540, 4);
            this._autoProdColProgress.Name = "_autoProdColProgress";
            this._autoProdColProgress.Size = new System.Drawing.Size(49, 13);
            this._autoProdColProgress.TabIndex = 4;
            this._autoProdColProgress.Text = "Progress";
            //
            // _autoProdSettingsPanel
            //
            this._autoProdSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(42)))));
            this._autoProdSettingsPanel.Controls.Add(this._autoProdSettingsTitle);
            this._autoProdSettingsPanel.Controls.Add(this._autoProdIntervalLabel);
            this._autoProdSettingsPanel.Controls.Add(this._autoCardIntervalInput);
            this._autoProdSettingsPanel.Controls.Add(this._autoProdSecondsLabel);
            this._autoProdSettingsPanel.Controls.Add(this._autoProdSettingsSep);
            this._autoProdSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._autoProdSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._autoProdSettingsPanel.Name = "_autoProdSettingsPanel";
            this._autoProdSettingsPanel.Size = new System.Drawing.Size(1134, 36);
            this._autoProdSettingsPanel.TabIndex = 0;
            //
            // _autoProdSettingsTitle
            //
            this._autoProdSettingsTitle.AutoSize = true;
            this._autoProdSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._autoProdSettingsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._autoProdSettingsTitle.Location = new System.Drawing.Point(8, 6);
            this._autoProdSettingsTitle.Name = "_autoProdSettingsTitle";
            this._autoProdSettingsTitle.Size = new System.Drawing.Size(46, 13);
            this._autoProdSettingsTitle.TabIndex = 1;
            this._autoProdSettingsTitle.Text = "Settings";
            //
            // _autoProdIntervalLabel
            //
            this._autoProdIntervalLabel.AutoSize = true;
            this._autoProdIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoProdIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoProdIntervalLabel.Location = new System.Drawing.Point(90, 8);
            this._autoProdIntervalLabel.Name = "_autoProdIntervalLabel";
            this._autoProdIntervalLabel.Size = new System.Drawing.Size(141, 13);
            this._autoProdIntervalLabel.TabIndex = 2;
            this._autoProdIntervalLabel.Text = "Check production cards every";
            //
            // _autoProdSecondsLabel
            //
            this._autoProdSecondsLabel.AutoSize = true;
            this._autoProdSecondsLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoProdSecondsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoProdSecondsLabel.Location = new System.Drawing.Point(356, 8);
            this._autoProdSecondsLabel.Name = "_autoProdSecondsLabel";
            this._autoProdSecondsLabel.Size = new System.Drawing.Size(46, 13);
            this._autoProdSecondsLabel.TabIndex = 3;
            this._autoProdSecondsLabel.Text = "seconds";
            //
            // _autoProdSettingsSep
            //
            this._autoProdSettingsSep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this._autoProdSettingsSep.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._autoProdSettingsSep.Location = new System.Drawing.Point(0, 35);
            this._autoProdSettingsSep.Name = "_autoProdSettingsSep";
            this._autoProdSettingsSep.Size = new System.Drawing.Size(1134, 1);
            this._autoProdSettingsSep.TabIndex = 4;
            //
            // _autoCardIntervalInput
            //
            this._autoCardIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._autoCardIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._autoCardIntervalInput.Location = new System.Drawing.Point(290, 6);
            this._autoCardIntervalInput.Maximum = new decimal(new int[] { 3600, 0, 0, 0});
            this._autoCardIntervalInput.Minimum = new decimal(new int[] { 5, 0, 0, 0});
            this._autoCardIntervalInput.Name = "_autoCardIntervalInput";
            this._autoCardIntervalInput.Size = new System.Drawing.Size(60, 20);
            this._autoCardIntervalInput.TabIndex = 0;
            this._autoCardIntervalInput.Value = new decimal(new int[] { 30, 0, 0, 0});
            //
            // _autoModuleTab
            //
            this._autoModuleTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._autoModuleTab.Controls.Add(this._autoModuleScrollPanel);
            this._autoModuleTab.Controls.Add(this._autoModuleHeaderPanel);
            this._autoModuleTab.Controls.Add(this._autoModuleSettingsPanel);
            this._autoModuleTab.Location = new System.Drawing.Point(4, 26);
            this._autoModuleTab.Name = "_autoModuleTab";
            this._autoModuleTab.Size = new System.Drawing.Size(1134, 467);
            this._autoModuleTab.TabIndex = 1;
            this._autoModuleTab.Text = "Modules";
            //
            // _autoModuleScrollPanel
            //
            this._autoModuleScrollPanel.AutoScroll = true;
            this._autoModuleScrollPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(32)))));
            this._autoModuleScrollPanel.Controls.Add(this._autoModuleScrollPanelPlaceholder);
            this._autoModuleScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._autoModuleScrollPanel.Location = new System.Drawing.Point(0, 90);
            this._autoModuleScrollPanel.Name = "_autoModuleScrollPanel";
            this._autoModuleScrollPanel.Size = new System.Drawing.Size(1134, 377);
            this._autoModuleScrollPanel.TabIndex = 2;
            //
            // _autoModuleScrollPanelPlaceholder
            //
            this._autoModuleScrollPanelPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._autoModuleScrollPanelPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._autoModuleScrollPanelPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(95)))));
            this._autoModuleScrollPanelPlaceholder.Name = "_autoModuleScrollPanelPlaceholder";
            this._autoModuleScrollPanelPlaceholder.TabIndex = 99;
            this._autoModuleScrollPanelPlaceholder.Text = "〈 Module schedule rows — one per bot module 〉";
            this._autoModuleScrollPanelPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._autoModuleScrollPanelPlaceholder.Visible = false;
            //
            // _autoModuleHeaderPanel
            //
            this._autoModuleHeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this._autoModuleHeaderPanel.Controls.Add(this._autoModuleColModule);
            this._autoModuleHeaderPanel.Controls.Add(this._autoModuleColCards);
            this._autoModuleHeaderPanel.Controls.Add(this._autoModuleColReplay);
            this._autoModuleHeaderPanel.Controls.Add(this._autoModuleColAutoOff);
            this._autoModuleHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._autoModuleHeaderPanel.Location = new System.Drawing.Point(0, 54);
            this._autoModuleHeaderPanel.Name = "_autoModuleHeaderPanel";
            this._autoModuleHeaderPanel.Size = new System.Drawing.Size(1134, 36);
            this._autoModuleHeaderPanel.TabIndex = 1;
            //
            // _autoModuleColModule
            //
            this._autoModuleColModule.AutoSize = true;
            this._autoModuleColModule.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoModuleColModule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoModuleColModule.Location = new System.Drawing.Point(8, 4);
            this._autoModuleColModule.Name = "_autoModuleColModule";
            this._autoModuleColModule.Size = new System.Drawing.Size(44, 13);
            this._autoModuleColModule.TabIndex = 0;
            this._autoModuleColModule.Text = "Module";
            //
            // _autoModuleColCards
            //
            this._autoModuleColCards.AutoSize = true;
            this._autoModuleColCards.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoModuleColCards.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoModuleColCards.Location = new System.Drawing.Point(510, 4);
            this._autoModuleColCards.Name = "_autoModuleColCards";
            this._autoModuleColCards.Size = new System.Drawing.Size(168, 13);
            this._autoModuleColCards.TabIndex = 1;
            this._autoModuleColCards.Text = "Cards to play (check all you want)";
            //
            // _autoModuleColReplay
            //
            this._autoModuleColReplay.AutoSize = true;
            this._autoModuleColReplay.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoModuleColReplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoModuleColReplay.Location = new System.Drawing.Point(810, 4);
            this._autoModuleColReplay.Name = "_autoModuleColReplay";
            this._autoModuleColReplay.Size = new System.Drawing.Size(44, 13);
            this._autoModuleColReplay.TabIndex = 2;
            this._autoModuleColReplay.Text = "Re-play";
            //
            // _autoModuleColAutoOff
            //
            this._autoModuleColAutoOff.AutoSize = true;
            this._autoModuleColAutoOff.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoModuleColAutoOff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoModuleColAutoOff.Location = new System.Drawing.Point(900, 4);
            this._autoModuleColAutoOff.Name = "_autoModuleColAutoOff";
            this._autoModuleColAutoOff.Size = new System.Drawing.Size(46, 13);
            this._autoModuleColAutoOff.TabIndex = 3;
            this._autoModuleColAutoOff.Text = "Auto-off";
            //
            // _autoModuleSettingsPanel
            //
            this._autoModuleSettingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(42)))));
            this._autoModuleSettingsPanel.Controls.Add(this._autoModuleSettingsTitle);
            this._autoModuleSettingsPanel.Controls.Add(this._autoModuleIntervalLabel);
            this._autoModuleSettingsPanel.Controls.Add(this._autoServerTimeLabel);
            this._autoModuleSettingsPanel.Controls.Add(this._autoModuleIntervalInput);
            this._autoModuleSettingsPanel.Controls.Add(this._autoModuleSecondsLabel);
            this._autoModuleSettingsPanel.Controls.Add(this._autoModuleSettingsSep);
            this._autoModuleSettingsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._autoModuleSettingsPanel.Location = new System.Drawing.Point(0, 0);
            this._autoModuleSettingsPanel.Name = "_autoModuleSettingsPanel";
            this._autoModuleSettingsPanel.Size = new System.Drawing.Size(1134, 54);
            this._autoModuleSettingsPanel.TabIndex = 0;
            //
            // _autoModuleSettingsTitle
            //
            this._autoModuleSettingsTitle.AutoSize = true;
            this._autoModuleSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this._autoModuleSettingsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this._autoModuleSettingsTitle.Location = new System.Drawing.Point(8, 6);
            this._autoModuleSettingsTitle.Name = "_autoModuleSettingsTitle";
            this._autoModuleSettingsTitle.Size = new System.Drawing.Size(46, 13);
            this._autoModuleSettingsTitle.TabIndex = 3;
            this._autoModuleSettingsTitle.Text = "Settings";
            //
            // _autoModuleIntervalLabel
            //
            this._autoModuleIntervalLabel.AutoSize = true;
            this._autoModuleIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoModuleIntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoModuleIntervalLabel.Location = new System.Drawing.Point(90, 8);
            this._autoModuleIntervalLabel.Name = "_autoModuleIntervalLabel";
            this._autoModuleIntervalLabel.Size = new System.Drawing.Size(146, 13);
            this._autoModuleIntervalLabel.TabIndex = 4;
            this._autoModuleIntervalLabel.Text = "Check module schedules every";
            //
            // _autoModuleSecondsLabel
            //
            this._autoModuleSecondsLabel.AutoSize = true;
            this._autoModuleSecondsLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._autoModuleSecondsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoModuleSecondsLabel.Location = new System.Drawing.Point(356, 8);
            this._autoModuleSecondsLabel.Name = "_autoModuleSecondsLabel";
            this._autoModuleSecondsLabel.Size = new System.Drawing.Size(46, 13);
            this._autoModuleSecondsLabel.TabIndex = 5;
            this._autoModuleSecondsLabel.Text = "seconds";
            //
            // _autoModuleSettingsSep
            //
            this._autoModuleSettingsSep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this._autoModuleSettingsSep.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._autoModuleSettingsSep.Location = new System.Drawing.Point(0, 53);
            this._autoModuleSettingsSep.Name = "_autoModuleSettingsSep";
            this._autoModuleSettingsSep.Size = new System.Drawing.Size(1134, 1);
            this._autoModuleSettingsSep.TabIndex = 6;
            //
            // _autoModuleIntervalInput
            //
            this._autoModuleIntervalInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(55)))));
            this._autoModuleIntervalInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this._autoModuleIntervalInput.Location = new System.Drawing.Point(290, 6);
            this._autoModuleIntervalInput.Maximum = new decimal(new int[] { 3600, 0, 0, 0});
            this._autoModuleIntervalInput.Minimum = new decimal(new int[] { 10, 0, 0, 0});
            this._autoModuleIntervalInput.Name = "_autoModuleIntervalInput";
            this._autoModuleIntervalInput.Size = new System.Drawing.Size(60, 20);
            this._autoModuleIntervalInput.TabIndex = 0;
            this._autoModuleIntervalInput.Value = new decimal(new int[] { 60, 0, 0, 0});
            //
            // _autoServerTimeLabel
            //
            this._autoServerTimeLabel.AutoSize = true;
            this._autoServerTimeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this._autoServerTimeLabel.Location = new System.Drawing.Point(8, 32);
            this._autoServerTimeLabel.Name = "_autoServerTimeLabel";
            this._autoServerTimeLabel.Size = new System.Drawing.Size(100, 13);
            this._autoServerTimeLabel.TabIndex = 2;
            this._autoServerTimeLabel.Text = "Server time: --:--:--";
            this._autoPage.ResumeLayout(false);
            this._autoInnerTabs.ResumeLayout(false);
            this._autoProdTab.ResumeLayout(false);
            this._autoProdSettingsPanel.ResumeLayout(false);
            this._autoProdSettingsPanel.PerformLayout();
            this._autoModuleTab.ResumeLayout(false);
            this._autoModuleSettingsPanel.ResumeLayout(false);
            this._autoModuleSettingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._autoCardIntervalInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._autoModuleIntervalInput)).EndInit();
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
        private System.Windows.Forms.Label _rdMentionTagLabel;
        private System.Windows.Forms.TextBox _rdMentionTagInput;
        private System.Windows.Forms.Label _rdInterdictLabel;
        private System.Windows.Forms.NumericUpDown _rdInterdictMonkCountInput;
        private System.Windows.Forms.CheckBox _rdAutoRecruitMonksCheck;
        private System.Windows.Forms.Label _rdMinArmySizeLabel;
        private System.Windows.Forms.NumericUpDown _rdMinArmySizeInput;
        private System.Windows.Forms.NumericUpDown _rdMinAttacksInput;
        private System.Windows.Forms.Label _rdMinAttacksLabel;
        private System.Windows.Forms.NumericUpDown _rdMinAttacksWindowInput;
        private System.Windows.Forms.Label _rdMinAttacksWindowLabel;
        private System.Windows.Forms.Label _rdMinAttacksWindowUnitLabel;
        private System.Windows.Forms.NumericUpDown _rdMaxLandTimeInput;
        private System.Windows.Forms.Label _rdMaxLandTimeLabel;
        private System.Windows.Forms.CheckBox _rdForceRefreshCheck;
        private System.Windows.Forms.Label _rdHintLabel;
        private System.Windows.Forms.Button _rdTestDiscordBtn;
        private System.Windows.Forms.Button _rdTestSoundBtn;
        private System.Windows.Forms.Button _rdStopSoundBtn;
        private System.Windows.Forms.Panel _rdSeparator;
        private System.Windows.Forms.Panel _rdColHeader;
        private System.Windows.Forms.Label _rdColActionType;
        private System.Windows.Forms.Label _rdColMonitor;
        private System.Windows.Forms.Label _rdColSystemNotify;
        private System.Windows.Forms.Label _rdColDiscordNotify;
        private System.Windows.Forms.Label _rdColSound;
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
        private System.Windows.Forms.Button _crMemoriseInfraBtn;
        private System.Windows.Forms.Button _crMemoriseTroopsBtn;
        private System.Windows.Forms.Button _crRefreshBtn;
        private System.Windows.Forms.Button _crCopySettingsBtn;
        private System.Windows.Forms.Panel _crSeparator;
        private System.Windows.Forms.Panel _crColHeader;
        private System.Windows.Forms.Panel _crVillageListPanel;
        // Trade tab controls
        private System.Windows.Forms.TabPage _tradePage;
        private System.Windows.Forms.TabPage _builderPage;
        private System.Windows.Forms.Panel _bldSettingsPanel;
        private System.Windows.Forms.CheckBox _bldEnabledCheck;
        private System.Windows.Forms.Label _bldStatusLabel;
        private System.Windows.Forms.Label _bldIntervalLabel;
        private System.Windows.Forms.NumericUpDown _bldIntervalInput;
        private System.Windows.Forms.Label _bldDelayLabel;
        private System.Windows.Forms.NumericUpDown _bldDelayInput;
        private System.Windows.Forms.CheckBox _bldWaitForResourcesCheck;
        private System.Windows.Forms.Button _bldCopySettingsBtn;
        private System.Windows.Forms.Panel _bldNavPanel;
        private System.Windows.Forms.ComboBox _bldVillageCombo;
        private System.Windows.Forms.CheckBox _bldVillageEnabledCheck;
        private System.Windows.Forms.Button _bldImportFileBtn;
        private System.Windows.Forms.Button _bldRefreshStateBtn;
        private System.Windows.Forms.Button _bldExportFileBtn;
        private System.Windows.Forms.Button _bldClearLayoutBtn;
        private System.Windows.Forms.Panel _bldColHeader;
        private System.Windows.Forms.Panel _bldBuildingListPanel;
        private System.Windows.Forms.TabPage _bombPage;
        private System.Windows.Forms.TabControl _bombSubTabs;
        private System.Windows.Forms.TabPage _bombSetupTab;
        private System.Windows.Forms.TabPage _bombPendingTab;
        private System.Windows.Forms.TabPage _bombMultiPage;
        private System.Windows.Forms.TabControl _abmSubTabs;
        private System.Windows.Forms.TabPage _abmPlayersTab;
        private System.Windows.Forms.TabPage _abmPendingTab;
        private System.Windows.Forms.TabPage _abmQueueTab;
        private System.Windows.Forms.Panel _abmConnPanel;
        private System.Windows.Forms.Panel _abmCtrlPanel;
        private System.Windows.Forms.Panel _abmVillageListPanel;
        private System.Windows.Forms.Panel _abmVillageColHeader;
        private System.Windows.Forms.Panel _abmPendingListPanel;
        private System.Windows.Forms.Panel _abmPendingColHeader;
        private System.Windows.Forms.ListBox _abmQueueListBox;
        private System.Windows.Forms.Panel _abmQueueSettingsPanel;
        private System.Windows.Forms.TextBox _abmApiUrlBox;
        private System.Windows.Forms.TextBox _abmSessionKeyBox;
        private System.Windows.Forms.Button _abmConnectBtn;
        private System.Windows.Forms.Button _abmDisconnectBtn;
        private System.Windows.Forms.Label _abmConnStatusLabel;
        private System.Windows.Forms.TextBox _abmTargetVidBox;
        private System.Windows.Forms.NumericUpDown _abmStackDelayInput;
        private System.Windows.Forms.CheckBox _abmFakeSendCheck;
        private System.Windows.Forms.CheckBox _abmAutoInterdictCheck;
        private System.Windows.Forms.CheckBox _abmPreRefreshCheck;
        private System.Windows.Forms.CheckBox _abmIncludeVassalsCheck;
        private System.Windows.Forms.CheckBox _abmPlayCardsCheck;
        private System.Windows.Forms.CheckBox _abmAutoCancelCardCheck;
        private System.Windows.Forms.CheckBox _abmSendPartialCheck;
        private System.Windows.Forms.Button _abmSelectAllBtn;
        private System.Windows.Forms.Button _abmDeselectAllBtn;
        private System.Windows.Forms.Button _abmPushConfigBtn;
        private System.Windows.Forms.Button _abmPrepareBtn;
        private System.Windows.Forms.Button _abmLaunchBtn;
        private System.Windows.Forms.Button _abmCancelBtn;
        private System.Windows.Forms.Button _abmForceRecallBtn;
        private System.Windows.Forms.ComboBox _abmDelayModeCombo;
        private System.Windows.Forms.Button _abmResetBtn;
        private System.Windows.Forms.Button _abmTakeCoordBtn;
        private System.Windows.Forms.Label _abmCoordStatusLabel;
        private System.Windows.Forms.CheckBox _abmQueueEnabledCheck;
        private System.Windows.Forms.NumericUpDown _abmQueueVidInput;
        private System.Windows.Forms.Button _abmQueueAddIdBtn;
        private System.Windows.Forms.TextBox _abmQueuePlayerNameBox;
        private System.Windows.Forms.Button _abmQueueLookupBtn;
        private System.Windows.Forms.Button _abmQueueAddSelectedVillageBtn;
        private System.Windows.Forms.Button _abmQueueAddSelectedPlayerBtn;
        private System.Windows.Forms.Button _abmQueueRemoveBtn;
        private System.Windows.Forms.Button _abmQueueClearBtn;
        private System.Windows.Forms.Button _abmQueueSaveBtn;
        private System.Windows.Forms.Button _abmQueueLoadBtn;
        private System.Windows.Forms.Button _abmQueueRefreshBtn;
        private System.Windows.Forms.Button _abmQueueResetBtn;
        private System.Windows.Forms.Label _abmQueueStatusLabel;
        private System.Windows.Forms.Panel _abSettingsPanel;
        private System.Windows.Forms.CheckBox _abEnabledCheck;
        private System.Windows.Forms.Label _abStatusLabel;
        private System.Windows.Forms.Label _abTargetLabel;
        private System.Windows.Forms.NumericUpDown _abTargetInput;
        private System.Windows.Forms.CheckBox _abAutoCancelCheck;
        private System.Windows.Forms.CheckBox _abFakeSendCheck;
        private System.Windows.Forms.Label _abStackDelayLabel;
        private System.Windows.Forms.NumericUpDown _abStackDelayInput;
        private System.Windows.Forms.CheckBox _abLoadVillages;
        private System.Windows.Forms.CheckBox _abLoadCapitals;
        private System.Windows.Forms.Button _abLoadArmiesBtn;
        private System.Windows.Forms.Button _abSelectAllBtn;
        private System.Windows.Forms.Button _abDeselectAllBtn;
        private System.Windows.Forms.Button _abSubmitBtn;
        private System.Windows.Forms.Panel _abSetupColHeader;
        private System.Windows.Forms.Panel _abArmyListPanel;
        private System.Windows.Forms.Panel _abPendingSettingsPanel;
        private System.Windows.Forms.Button _abLaunchBtn;
        private System.Windows.Forms.Button _abCancelAllBtn;
        private System.Windows.Forms.Button _abClearQueueBtn;
        private System.Windows.Forms.Panel _abPendingColHeader;
        private System.Windows.Forms.Panel _abPendingListPanel;
        private System.Windows.Forms.TabPage _bombTargetQueueTab;
        private System.Windows.Forms.Panel _abQueueSettingsPanel;
        private System.Windows.Forms.CheckBox _abQueueEnabledCheck;
        private System.Windows.Forms.NumericUpDown _abQueueVillageIdInput;
        private System.Windows.Forms.Button _abQueueAddIdBtn;
        private System.Windows.Forms.TextBox _abQueuePlayerNameInput;
        private System.Windows.Forms.Button _abQueueLookupBtn;
        private System.Windows.Forms.Button _abQueueRemoveBtn;
        private System.Windows.Forms.Button _abQueueClearBtn;
        private System.Windows.Forms.Button _abQueueSaveBtn;
        private System.Windows.Forms.Button _abQueueLoadBtn;
        private System.Windows.Forms.Button _abQueueResetBtn;
        private System.Windows.Forms.Button _abQueueAddSelectedVillageBtn;
        private System.Windows.Forms.Button _abQueueAddSelectedPlayerBtn;
        private System.Windows.Forms.Label _abQueueStatusLabel;
        private System.Windows.Forms.ListBox _abQueueListBox;
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
        private System.Windows.Forms.ComboBox _trPriorityCombo;
        private System.Windows.Forms.Label _trPriorityLabel;
        private System.Windows.Forms.TabPage _trStatsTab;
        private System.Windows.Forms.CheckBox _trDisableOnCardExpiryCheck;
        private System.Windows.Forms.CheckBox _trAutoSaveRouteProgressCheck;
        private System.Windows.Forms.Panel _trMarketVillageListPanel;
        private System.Windows.Forms.Button _trMarketRefreshBtn;
        private System.Windows.Forms.Button _trAddMarketsBtn;
        private System.Windows.Forms.Label _trMarketDistanceLabel;
        private System.Windows.Forms.NumericUpDown _trMarketDistanceInput;
        private System.Windows.Forms.Panel _trRoutesListPanel;
        private System.Windows.Forms.Button _trAddRouteBtn;
        private System.Windows.Forms.Button _trDeleteRouteBtn;
        private System.Windows.Forms.Button _trRefreshRoutesBtn;
        // Misc tab controls
        private System.Windows.Forms.TabPage _miscPage;
        private System.Windows.Forms.Panel _miscSettingsPanel;
        private System.Windows.Forms.CheckBox _miscCollectFreeCardsCheck;
        private System.Windows.Forms.CheckBox _miscDisableCannotPlayCardCheck;
        private System.Windows.Forms.CheckBox _miscShowOtherTraderInfoCheck;
        private System.Windows.Forms.CheckBox _miscWorldMapParishBuildingCountCheck;
        private System.Windows.Forms.CheckBox _miscShowUserScreenInfoCheck;
        private System.Windows.Forms.CheckBox _miscMapAttackTypeIconsCheck;
        private System.Windows.Forms.Label _miscSaleHeaderLabel;
        private System.Windows.Forms.Label _miscSalePctLabel;
        private System.Windows.Forms.Label _miscSalePctValue;
        private System.Windows.Forms.Label _miscSaleStartLabel;
        private System.Windows.Forms.Label _miscSaleStartValue;
        private System.Windows.Forms.Label _miscSaleEndLabel;
        private System.Windows.Forms.Label _miscSaleEndValue;
        // Popularity tab controls
        private System.Windows.Forms.TabPage _popularityPage;
        private System.Windows.Forms.TabPage _autoPage;
        // Auto tab scaffolding (inner sub-tabs). The settings/header/scroll panels and all rows
        // are data-driven and populated at runtime in BuildAutoTabUI / BuildProductionSubTab /
        // BuildModulesSubTab.
        private System.Windows.Forms.TabControl _autoInnerTabs;
        private System.Windows.Forms.TabPage _autoProdTab;
        private System.Windows.Forms.TabPage _autoModuleTab;
        // Production sub-tab sections (rows added at runtime)
        private System.Windows.Forms.Panel _autoProdSettingsPanel;
        private System.Windows.Forms.Panel _autoProdHeaderPanel;
        private System.Windows.Forms.Panel _autoProdScrollPanel;
        private System.Windows.Forms.NumericUpDown _autoCardIntervalInput;
        // Modules sub-tab sections (rows added at runtime)
        private System.Windows.Forms.Panel _autoModuleSettingsPanel;
        private System.Windows.Forms.Panel _autoModuleHeaderPanel;
        private System.Windows.Forms.Panel _autoModuleScrollPanel;
        private System.Windows.Forms.NumericUpDown _autoModuleIntervalInput;
        private System.Windows.Forms.Label _autoServerTimeLabel;
        // Auto tab static captions
        private System.Windows.Forms.Label _autoProdSettingsTitle;
        private System.Windows.Forms.Label _autoProdIntervalLabel;
        private System.Windows.Forms.Label _autoProdSecondsLabel;
        private System.Windows.Forms.Panel _autoProdSettingsSep;
        private System.Windows.Forms.Label _autoProdColGood;
        private System.Windows.Forms.Label _autoProdColTier;
        private System.Windows.Forms.Label _autoProdColTarget;
        private System.Windows.Forms.Label _autoProdColDelay;
        private System.Windows.Forms.Label _autoProdColProgress;
        private System.Windows.Forms.Label _autoModuleSettingsTitle;
        private System.Windows.Forms.Label _autoModuleIntervalLabel;
        private System.Windows.Forms.Label _autoModuleSecondsLabel;
        private System.Windows.Forms.Panel _autoModuleSettingsSep;
        private System.Windows.Forms.Label _autoModuleColModule;
        private System.Windows.Forms.Label _autoModuleColCards;
        private System.Windows.Forms.Label _autoModuleColReplay;
        private System.Windows.Forms.Label _autoModuleColAutoOff;
        private System.Windows.Forms.Panel _ppVillageListPanel;
        private System.Windows.Forms.Panel _ppColHeader;
        private System.Windows.Forms.Panel _ppSeparator;
        private System.Windows.Forms.Panel _ppSettingsPanel;
        private System.Windows.Forms.CheckBox _ppEnabledCheck;
        private System.Windows.Forms.Label _ppStatusLabel;
        private System.Windows.Forms.Label _ppIntervalLabel;
        private System.Windows.Forms.NumericUpDown _ppIntervalInput;
        private System.Windows.Forms.Label _ppDelayLabel;
        private System.Windows.Forms.NumericUpDown _ppDelayInput;
        private System.Windows.Forms.Button _ppRefreshBtn;
        private System.Windows.Forms.Button _ppRunNowBtn;
        private System.Windows.Forms.Button _ppCopySettingsBtn;
        // Banquet tab controls
        private System.Windows.Forms.TabPage _bqPage;
        private System.Windows.Forms.Panel _bqVillageListPanel;
        private System.Windows.Forms.Panel _bqColHeader;
        private System.Windows.Forms.Panel _bqSeparator;
        private System.Windows.Forms.Panel _bqSettingsPanel;
        private System.Windows.Forms.CheckBox _bqEnabledCheck;
        private System.Windows.Forms.Label _bqStatusLabel;
        private System.Windows.Forms.Label _bqIntervalLabel;
        private System.Windows.Forms.NumericUpDown _bqIntervalInput;
        private System.Windows.Forms.Label _bqDelayLabel;
        private System.Windows.Forms.NumericUpDown _bqDelayInput;
        private System.Windows.Forms.Button _bqRefreshBtn;
        private System.Windows.Forms.Button _bqRunNowBtn;
        private System.Windows.Forms.Button _bqCopySettingsBtn;
        // Scout tab controls
        private System.Windows.Forms.TabPage _scoutPage;
        private System.Windows.Forms.Panel _scSettingsPanel;
        private System.Windows.Forms.Panel _scSeparator;
        private System.Windows.Forms.Panel _scVillagePanel;
        private System.Windows.Forms.Panel _scDivider;
        private System.Windows.Forms.Panel _scContentPanel;
        private System.Windows.Forms.CheckBox _scEnabledCheck;
        private System.Windows.Forms.Label _scStatusLabel;
        private System.Windows.Forms.Label _scIntervalLabel;
        private System.Windows.Forms.NumericUpDown _scIntervalInput;
        private System.Windows.Forms.Label _scMaxTimeLabel;
        private System.Windows.Forms.NumericUpDown _scMaxTimeInput;
        private System.Windows.Forms.Label _scAutoHireLabel;
        private System.Windows.Forms.NumericUpDown _scAutoHireInput;
        private System.Windows.Forms.Label _scDelayLabel;
        private System.Windows.Forms.NumericUpDown _scDelayInput;
        private System.Windows.Forms.CheckBox _scDisableOnCardExpiryCheck;
        private System.Windows.Forms.Label _scPriorityLabel;
        private System.Windows.Forms.RadioButton _scPriorityResourceRadio;
        private System.Windows.Forms.RadioButton _scPriorityRangeRadio;
        private System.Windows.Forms.CheckBox _scSendOneScoutCheck;
        private System.Windows.Forms.CheckBox _scSendOneOnNewCheck;
        private System.Windows.Forms.ListBox _scVillageListBox;
        private System.Windows.Forms.Label _scVillageHeaderLabel;
        private System.Windows.Forms.CheckBox _scVillageEnabledCheck;
        private System.Windows.Forms.Label _scScoutListLabel;
        private System.Windows.Forms.Label _scIgnoreListLabel;
        private System.Windows.Forms.ListBox _scScoutList;
        private System.Windows.Forms.ListBox _scIgnoreList;
        private System.Windows.Forms.Button _scMoveUpBtn;
        private System.Windows.Forms.Button _scMoveDownBtn;
        private System.Windows.Forms.Button _scMoveToIgnoreBtn;
        private System.Windows.Forms.Button _scMoveToScoutBtn;
        private System.Windows.Forms.Button _scCopySettingsBtn;
        // Defender tab controls
        private System.Windows.Forms.TabPage _defenderPage;
        private System.Windows.Forms.Panel _dfSettingsPanel;
        private System.Windows.Forms.CheckBox _dfEnabledCheck;
        private System.Windows.Forms.Label _dfStatusLabel;
        private System.Windows.Forms.Label _dfDurationLabel;
        private System.Windows.Forms.NumericUpDown _dfDurationInput;
        private System.Windows.Forms.Label _dfVillageLabel;
        private System.Windows.Forms.ComboBox _dfVillageCombo;
        private System.Windows.Forms.Button _dfVillageRefreshBtn;
        private System.Windows.Forms.Button _dfStartBtn;
        private System.Windows.Forms.Button _dfStopBtn;
        private System.Windows.Forms.Label _dfCountdownPrefixLabel;
        private System.Windows.Forms.Label _dfCountdownLabel;
        private System.Windows.Forms.Panel _dfSep1;
        private System.Windows.Forms.Panel _dfCardsPanel;
        private System.Windows.Forms.Label _dfCardsTitle;
        private System.Windows.Forms.Label _dfKnightsLabel;
        private System.Windows.Forms.ComboBox _dfKnightsCombo;
        private System.Windows.Forms.Label _dfLastStandLabel;
        private System.Windows.Forms.ComboBox _dfLastStandCombo;
        private System.Windows.Forms.CheckBox _dfDesperateCheck;
        private System.Windows.Forms.Panel _dfSep2;
        private System.Windows.Forms.Panel _dfActionsPanel;
        private System.Windows.Forms.Label _dfActionsTitle;
        private System.Windows.Forms.CheckBox _dfAutoRepairCheck;
        private System.Windows.Forms.CheckBox _dfRestoreTroopsCheck;
        private System.Windows.Forms.CheckBox _dfRestoreInfraCheck;
        // Monk tab controls
        private System.Windows.Forms.TabPage _mkPage;
        private System.Windows.Forms.Panel _mkSettingsPanel;
        private System.Windows.Forms.Panel _mkColHeader;
        private System.Windows.Forms.Panel _mkRouteListPanel;
        private System.Windows.Forms.Panel _mkRouteButtonPanel;
        private System.Windows.Forms.CheckBox _mkEnabledCheck;
        private System.Windows.Forms.Label _mkStatusLabel;
        private System.Windows.Forms.Label _mkIntervalLabel;
        private System.Windows.Forms.NumericUpDown _mkIntervalInput;
        private System.Windows.Forms.Label _mkDelayLabel;
        private System.Windows.Forms.NumericUpDown _mkDelayInput;
        private System.Windows.Forms.Label _mkKeepLabel;
        private System.Windows.Forms.NumericUpDown _mkMonksToKeepInput;
        private System.Windows.Forms.Label _mkAutoRecruitLabel;
        private System.Windows.Forms.NumericUpDown _mkAutoRecruitInput;
        private System.Windows.Forms.Button _mkRefreshBtn;
        private System.Windows.Forms.Button _mkRunNowBtn;
        private System.Windows.Forms.Button _mkAddRouteBtn;
        private System.Windows.Forms.Button _mkEditRouteBtn;
        private System.Windows.Forms.Button _mkDeleteRouteBtn;
        // Designer-only placeholder labels (Visible = false at runtime)
        private System.Windows.Forms.Label _vsVillageListPanelPlaceholder;
        private System.Windows.Forms.Label _rdActionListPanelPlaceholder;
        private System.Windows.Forms.Label _rcVillageListPanelPlaceholder;
        private System.Windows.Forms.Label _rcCapitalsListPanelPlaceholder;
        private System.Windows.Forms.Label _crVillageListPanelPlaceholder;
        private System.Windows.Forms.Label _bldBuildingListPanelPlaceholder;
        private System.Windows.Forms.Label _ppVillageListPanelPlaceholder;
        private System.Windows.Forms.Label _bqVillageListPanelPlaceholder;
        private System.Windows.Forms.Label _mkRouteListPanelPlaceholder;
        private System.Windows.Forms.Label _autoProdScrollPanelPlaceholder;
        private System.Windows.Forms.Label _autoModuleScrollPanelPlaceholder;
    }
}
