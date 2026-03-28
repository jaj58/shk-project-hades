// Decompiled with JetBrains decompiler
// Type: Kingdoms.InterfaceMgr
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

//#nullable disable
namespace Kingdoms
{
  public class InterfaceMgr
  {
    private const int MRHP_POS = 6;
    private const int OVERLAY_CONTEXT_HEIGHT = 28;
    public static int UIScale = 1;
    public static bool mouseDownOnDraggable = false;
    private DateTime lastStopDrawTime = DateTime.MinValue;
    public bool ignoreStopDraw;
    private static InterfaceMgr instance = (InterfaceMgr) null;
    private MainWindow parentMainWindow;
    private Form parentForm;
    private Size m_expandedMainSize;
    private int worldMapMode;
    private int stockExchangeBuyingVillage = -1;
    private int attackTargetHomeVillage = -1;
    private int vassalSelectHomeVillage = -1;
    private int monkSelectHomeVillage = -1;
    private int userInfoRefreshCountdown;
    private bool doUserInfoUpdate;
    private UserInfoPanel2 userInfoPanel = new UserInfoPanel2();
    private EmptyVillagePanel2 emptyVillagePanel = new EmptyVillagePanel2();
    private OwnVillagePanel2 ownVillagePanel = new OwnVillagePanel2();
    private OtherVillagePanel2 otherVillagePanel = new OtherVillagePanel2();
    private ParishCapitalVillagePanel2 parishCapitalVillagePanel = new ParishCapitalVillagePanel2();
    private CountyCapitalVillagePanel2 countyCapitalVillagePanel = new CountyCapitalVillagePanel2();
    private ProvinceCapitalVillagePanel2 provinceCapitalVillagePanel = new ProvinceCapitalVillagePanel2();
    private CountryCapitalVillagePanel2 countryCapitalVillagePanel = new CountryCapitalVillagePanel2();
    private OwnParishCapitalPanel2 ownParishCapitalPanel = new OwnParishCapitalPanel2();
    private OwnCountyCapitalPanel2 ownCountyCapitalPanel = new OwnCountyCapitalPanel2();
    private OwnProvinceCapitalPanel2 ownProvinceCapitalPanel = new OwnProvinceCapitalPanel2();
    private OwnCountryCapitalPanel2 ownCountryCapitalPanel = new OwnCountryCapitalPanel2();
    private VassalVillagePanel2 vassalVillagePanel = new VassalVillagePanel2();
    private VassalAttackVillagePanel2 vassalAttackVillagePanel = new VassalAttackVillagePanel2();
    private int m_reallySelectedVillage = -1;
    private int m_touchscreenSelectedVillage = -1;
    private int m_ownSelectedVillage = -1;
    private int m_selectedVassalVillage = -1;
    private int lastViewedVillage = -1;
    private SelectArmyPanel2 selectArmyPanel = new SelectArmyPanel2();
    public long MapSelectedArmy = -1;
    public long MapSelectedReinforcement = -1;
    private SelectReinforcementPanel2 selectReinforcementPanel = new SelectReinforcementPanel2();
    private TradeWithPanel2 tradeWithPanel = new TradeWithPanel2();
    private StockExchangeSidePanel2 stockExchangeSidePanel = new StockExchangeSidePanel2();
    private AttackTargetSidePanel2 attackTargetSidePanel = new AttackTargetSidePanel2();
    private ScoutTargetSidePanel2 scoutTargetSidePanel = new ScoutTargetSidePanel2();
    private TraderInfoPanel2 traderInfoPanel = new TraderInfoPanel2();
    public long MapSelectedTrader = -1;
    private PersonInfoPanel2 personInfoPanel = new PersonInfoPanel2();
    public long MapSelectedPerson = -1;
    private ReinforcementTargetSidePanel2 reinforcementTargetSidePanel = new ReinforcementTargetSidePanel2();
    private int courtierHomeVillage = -1;
    private VassalSelectSidePanel2 vassalSelectSidePanel = new VassalSelectSidePanel2();
    private MonkTargetSidePanel2 monkTargetSidePanel = new MonkTargetSidePanel2();
    private VillageMapPanel villageMapPanel = new VillageMapPanel();
    private VillageInfoBar2 villageInfoBar = new VillageInfoBar2();
    private VillageReportBackgroundPanel villageReportBackgroundPanel = new VillageReportBackgroundPanel();
    private int lastVillageTab = -1;
    private bool firstVillageBackgroundCall = true;
    private CastleMapPanel castleMapPanel = new CastleMapPanel();
    private CastleInfoBar2 castleInfoBar = new CastleInfoBar2();
    public bool WaitingForCallback;
    private CastleMapAttackerSetupPanel castleMapAttackerSetupPanel = new CastleMapAttackerSetupPanel();
    private CastleMapBattlePanel2 castleMapBattlePanel = new CastleMapBattlePanel2();
    private UserInfoScreen userInfoScreen = new UserInfoScreen();
    private MainWindowPanel mainWindowPanel = new MainWindowPanel();
    public int SelectedResearchCategory;
    private ResearchPanel researchPanel = new ResearchPanel();
    private MailScreenPanel mailScreenManager = new MailScreenPanel();
    private ChatScreenManager chatScreenManager = new ChatScreenManager();
    private MapFilterSelectPanel mapFilterSelectPanel = new MapFilterSelectPanel();
    private MapFilterPanel2 mapFilterPanel = new MapFilterPanel2();
    public int ChildVillageID = -1;
    private int m_selectedMenuVillage = -1;
    private int m_forcedMenuVillage = -1;
    public string CurrentVillageName = "";
    public string CurrentVillageDescription = "";
    public bool cameraCentredOnVillage = true;
    private DateTime timeChangedToMode1 = DateTime.MinValue;
    private int lastTimeChangedMode = -1;
    private SendArmyWindow m_launchAttackPopup;
    private bool launchAttackPopupClosing;
    private CreatePopupWindow m_createPopupWindow;
    private bool createPopupWindowClosing;
    private WorldSelectPopupWindow m_worldSelectPopupWindow;
    private bool worldSelectPopupWindowClosing;
    private BPPopupWindow m_BPPopupWindow;
    private bool BPPopupWindowClosing;
    private VacationCancelPopupWindow m_VacationCancelPopupWindow;
    private bool VacationCancelPopupWindowClosing;
    private UpdatedTOSPopupWindow m_UpdatedTOSPopupWindow;
    private bool UpdatedTOSPopupWindowClosing;
    private PlayCardsWindow m_playCardsWindow;
    private bool playCardsWindowClosing;
    private MenuPopup m_currentMenuPopup;
    private DateTime m_menuPopupClosedLastTime = DateTime.MinValue;
    private CustomTooltip m_currentCustomTooltip;
    private TutorialWindow m_currentTutorialWindow;
    private AchievementPopup m_achievementPopup;
    private List<int> nextAchievementIDs = new List<int>();
    private ScoutPopupWindow m_scoutPopupWindow;
    private bool scoutPopupWindowClosing;
    private SendMonkWindow m_sendMonkWindow;
    private bool sendMonkWindowClosing;
    private BuyVillagePopupWindow m_buyVillageWindow;
    private bool buyVillageWindowClosing;
    private ConnectionErrorWindow m_connectionErrorWindow;
    private bool connectionErrorWindowClosing;
    private GreyOutWindow m_greyOutWindow;
    private bool m_greyLogin;
    private DonatePopup m_currentDonatePopup;
    private LogoutOptionsWindow2 m_logoutOptionsWindow;
    private bool logoutWindowClosing;
    private ReportCapturePopup m_reportCapturePopup;
    private bool reportCaptureWindowClosing;
    private NewQuestRewardPopup m_newQuestRewardPopup;
    private bool newQuestRewardPopupClosing;
    private NewQuestsCompletedWindow newQuestsCompletedWindow;
    private GloryVictoryWindow gloryVictoryWindow;
    private AdvancedCastleOptionsPopup m_advancedCastleOptionsPopup;
    private bool advancedCastleOptionsPopupClosing;
    private FormationPopup m_formationPopup;
    private bool formationPopupClosing;
    private PresetPopup m_presetPopup;
    private bool presetPopupClosing;
    private AttackTargetsPopup m_AttackTargetsPopup;
    private bool AttackTargetsPopupClosing;
    private FreeCardsPopup m_freeCardsPopup;
    private bool freeCardsPopupClosing;
    private WheelPopup m_WheelPopup;
    private bool WheelPopupClosing;
    private WheelSelectPopup m_WheelSelectPopup;
    private bool WheelSelectPopupClosing;
    private DominationWindow dominationWindow;
    private AdvicePopup advicePopup;
    public int OpenPackMultiple;
    private ConfirmOpenPackPopup m_confirmOpenPackPopup;
    private bool confirmOpenPackPopupClosing;
    public int BuyOfferMultiple;
    private ConfirmBuyOfferPopup m_confirmBuyOfferPopup;
    private bool confirmBuyOfferPopupClosing;
    private ConfirmPlayCardPopup m_confirmPlayCardPopup;
    private bool confirmPlayCardPopupClosing;
    private TutorialArrowWindow m_currentTutorialArrowWindow;
    private MedalsPopupWindow medalsPopupPanel;
    public List<int> newAchievements = new List<int>();
    private int m_floatingInputValue;
    private string m_floatingInputString = "";
    private InterfaceMgr.FloatingValueSent sendDelegate;
    private InterfaceMgr.FloatingTextSent sendTextDelegate;
    private CardBarDX cardBarDX = new CardBarDX();
    private PlaybackBarDX playbackBarDX = new PlaybackBarDX();
    public bool playbackEnabled;

    public bool isOverDXScreen(Point mousepos)
    {
      return (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE || GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD || GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE) && InterfaceMgr.Instance.getDXBasePanel().ClientRectangle.Contains(mousepos);
    }

    public void StopDrawing()
    {
    }

    public void StartDrawing()
    {
    }

    public static InterfaceMgr Instance
    {
      get
      {
        if (InterfaceMgr.instance == null)
          InterfaceMgr.instance = new InterfaceMgr();
        return InterfaceMgr.instance;
      }
    }

    public MainWindow ParentMainWindow => this.parentMainWindow;

    public Form ParentForm => this.parentForm;

    public Form ChatForm => this.chatScreenManager.ChatForm();

    public bool isDXVisible()
    {
      return this.parentMainWindow != null && this.parentMainWindow.getDXBasePanel() != null && this.parentMainWindow.getDXBasePanel().Visible;
    }

    public void reactiveMainWindow()
    {
      if (this.parentForm == null)
        return;
      this.parentForm.TopMost = true;
      this.parentForm.Focus();
      this.parentForm.BringToFront();
      this.parentForm.TopMost = false;
    }

    public void registerForm(Form parent, MainWindow newParentMainWindow)
    {
      this.parentForm = parent;
      this.parentMainWindow = newParentMainWindow;
    }

    public void initInterfaces()
    {
      this.m_expandedMainSize = this.parentMainWindow.getDXBasePanel().Size;
      this.m_expandedMainSize.Width += this.parentMainWindow.getMainRightHandPanel().Size.Width;
    }

    public void Close()
    {
      this.villageReportBackgroundPanel.clearAllReports();
      this.mailScreenManager.clearAllMail();
      this.chatScreenManager.close(true, true);
      this.clearControls();
      this.parentMainWindow = (MainWindow) null;
      this.parentForm = (Form) null;
    }

    public void logout()
    {
      this.mailScreenManager.logout();
      this.mailScreenManager.close(true);
      this.chatScreenManager.close(true, true);
      this.chatScreenManager.logout();
      this.villageReportBackgroundPanel.clearAllReports();
      this.mailScreenManager.clearAllMail();
      this.villageReportBackgroundPanel.logout();
      this.clearControls();
      this.closeAllPopups();
      RemoteServices.Instance.UserID = -1;
      GameEngine.Instance.World.resetTutorialInfo();
      GameEngine.Instance.World.LastUpdatedCrowns = DateTime.Now.AddHours(-1.0);
      this.castleMapPanel.castleCommitReturn();
      this.m_ownSelectedVillage = -1;
      this.m_selectedVassalVillage = -1;
      this.m_selectedMenuVillage = -1;
      CustomSelfDrawPanel.FactionPanelSideBar.logout();
      PlayCardsWindow.logout();
      TutorialPanel.logout();
      PostTutorialWindow.close();
      VideoWindow.ClosePopup();
      this.nextAchievementIDs.Clear();
      GameEngine.Instance.World.clearPlaybackData();
    }

    public Rectangle getWindowRect()
    {
      return new Rectangle(this.parentForm.Location, this.parentForm.Size);
    }

    public DXPanel getDXBasePanel() => this.parentMainWindow.getDXBasePanel();

    public MainRightHandPanel getMainRightHandPanel()
    {
      return this.parentMainWindow.getMainRightHandPanel();
    }

    public MainTabBar2 getMainTabBar() => this.parentMainWindow.getMainTabBar();

    public VillageTabBar2 getVillageTabBar() => this.parentMainWindow.getVillageTabBar();

    public FactionTabBar2 getFactionTabBar() => this.parentMainWindow.getFactionTabBar();

    public TopLeftMenu2 getTopLeftMenu() => this.parentMainWindow.getTopLeftMenu();

    public TopRightMenu getTopRightMenu() => this.parentMainWindow.getTopRightMenu();

    public MainMenuBar2 getMainMenuBar() => this.parentMainWindow.getMainMenuBar();

    public void setUserName(string userName) => this.getTopLeftMenu().setUserName(userName);

    public void setRank(int rank) => this.getTopLeftMenu().setRank(rank);

    public void changeTab(int tabID) => this.getMainTabBar().changeTab(tabID);

    public void showVillageTabBar() => this.getTopRightMenu().showVillageTab(true);

    public void showFactionTabBar() => this.getTopRightMenu().showFactionTabBar(true);

    public void clearControls() => this.clearControls(true, true, true, true);

    public void clearControlsBetweenPolitics() => this.clearControls(false, true, false, true);

    public void clearControlsLeaveRightHandPanel() => this.clearControls(true, true, true, false);

    public void clearControls(
      bool removeMainWindowPanel,
      bool removeVillageReportBackground,
      bool removePolitics,
      bool removeRightHandPanel)
    {
      this.doUserInfoUpdate = false;
      if (removeRightHandPanel)
      {
        this.userInfoPanel.closeControl(true);
        this.closeTraderInfoPanel();
        this.closeArmySelectedPanel();
        this.closeSelectedVillagePanel();
        this.closePersonInfoPanel();
        this.closeReinforcementSelectedPanel();
        this.getMainRightHandPanel().Controls.Clear();
      }
      this.mapFilterPanel.closeControl(true);
      this.researchPanel.closeControl(true);
      this.mailScreenManager.closeControl(true);
      this.chatScreenManager.closeControl(true);
      this.userInfoScreen.closeControl(true);
      this.mapFilterSelectPanel.closeControl(true);
      this.lastVillageTab = -1;
      this.villageReportBackgroundPanel.showPanel(-1);
      this.closeSendMonkWindow();
      this.m_selectedVassalVillage = -1;
      if (removeVillageReportBackground)
        this.villageReportBackgroundPanel.closeControl(true);
      if (removeMainWindowPanel)
      {
        this.getDXBasePanel().Controls.Remove((Control) this.mainWindowPanel);
        this.parentForm.Controls.Remove((Control) this.mainWindowPanel);
      }
      this.villageInfoBar.hide();
      this.castleInfoBar.hide();
      this.closeVillageTab();
      this.closeCastleTab();
      CapitalHelpBox.closeHelpBox();
      this.closeMedalsPopup();
      this.closeNewQuestsCompletedPopup();
      this.closeGloryVictoryWindowPopup();
    }

    public int WorldMapMode => this.worldMapMode;

    public void initWorldTab()
    {
      this.getTopRightMenu().showVillageTab(false);
      this.getTopRightMenu().showFactionTabBar(false);
      this.worldMapMode = 0;
      this.showDXWindow(true);
      this.showDXCardBar(9);
      this.userInfoPanel.initProperties(true, "User Village Info", (ContainerControl) this.parentMainWindow.getMainRightHandPanel());
      this.showMapFilterSelectPanel(true, true);
    }

    public void initWorldTab_tradingVillageSelect()
    {
      this.worldMapMode = 1;
      this.showDXWindow(true);
      this.showDXCardBar(9);
      this.displayTradeWithPanel();
      this.setTradeWithVillage(-1);
      this.showMapFilterSelectPanel(false, true);
    }

    public int StockExchangeBuyingVillage
    {
      get => this.stockExchangeBuyingVillage;
      set => this.stockExchangeBuyingVillage = value;
    }

    public void initWorldTab_stockExchangeSelect()
    {
      this.worldMapMode = 2;
      this.stockExchangeBuyingVillage = -1;
      this.showDXWindow(true);
      this.showDXCardBar(9);
      this.displayStockExchangeSidepanel();
      this.setStockExchangeSidePanelVillage(-1);
      this.showMapFilterSelectPanel(false, true);
    }

    public int AttackTargetHomeVillage
    {
      get => this.attackTargetHomeVillage;
      set => this.attackTargetHomeVillage = value;
    }

    public void initWorldTab_attackTargetSelect()
    {
      this.worldMapMode = 3;
      this.attackTargetHomeVillage = -1;
      this.showDXWindow(true);
      this.showDXCardBar(9);
      this.displayAttackTargetSidepanel();
      this.setAttackTargetSidePanelVillage(-1);
      this.showMapFilterSelectPanel(false, true);
    }

    public void initWorldTab_scoutTargetSelect()
    {
      this.worldMapMode = 4;
      this.attackTargetHomeVillage = -1;
      this.showDXWindow(true);
      this.showDXCardBar(9);
      this.displayScoutTargetSidepanel();
      this.setScoutTargetSidePanelVillage(-1);
      this.showMapFilterSelectPanel(false, true);
    }

    public int VassalSelectHomeVillage
    {
      get => this.vassalSelectHomeVillage;
      set => this.vassalSelectHomeVillage = value;
    }

    public void initWorldTab_vassalSelect()
    {
      this.worldMapMode = 7;
      this.vassalSelectHomeVillage = -1;
      this.showDXWindow(true);
      this.showDXCardBar(9);
      this.displayVassalSelectSidePanel();
      this.setVassalSelectSidePanelVillage(-1);
      this.showMapFilterSelectPanel(false, true);
    }

    public int MonkSelectHomeVillage
    {
      get => this.monkSelectHomeVillage;
      set => this.monkSelectHomeVillage = value;
    }

    public void initWorldTab_monkSelect()
    {
      this.worldMapMode = 9;
      this.monkSelectHomeVillage = -1;
      this.showDXWindow(true);
      this.showDXCardBar(9);
      this.displayMonkSelectSidePanel();
      this.setMonkSelectSidePanelVillage(-1);
      this.showMapFilterSelectPanel(false, true);
    }

    public void closeMonksPanel()
    {
    }

    public bool isUserInfoVisible() => this.userInfoPanel.isVisible();

    public void showUserInfo()
    {
      if (!this.userInfoPanel.isVisible())
      {
        this.userInfoPanel.display(false, (ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 182);
        this.userInfoPanel.init();
        this.userInfoRefreshCountdown = 5;
      }
      this.userInfoPanel.SendToBack();
      if (this.userInfoPanel.Parent == null || this.userInfoRefreshCountdown <= 0)
        return;
      --this.userInfoRefreshCountdown;
      if (this.userInfoRefreshCountdown <= 0)
        return;
      foreach (Control control in (ArrangedElementCollection) this.userInfoPanel.Parent.Controls)
      {
        if (control != this.userInfoPanel)
          control.Invalidate();
      }
    }

    public void clearAndCloseUserInfo()
    {
      this.lastViewedVillage = -1;
      this.closeUserInfo();
    }

    public void closeUserInfo() => this.userInfoPanel.closeControl(true);

    public void userInfoUpdate()
    {
      GameEngine.Instance.World.monitorCachedVillageUserInfo();
      WorldMap.VillageRolloverInfo villageInfo = (WorldMap.VillageRolloverInfo) null;
      WorldMap.CachedUserInfo userInfo = (WorldMap.CachedUserInfo) null;
      GameEngine.Instance.World.retrieveUserData(this.lastViewedVillage, -1, ref villageInfo, ref userInfo, false, false);
      if (userInfo != null)
      {
        this.showUserInfo();
        this.userInfoPanel.updateVillageInfo(villageInfo, userInfo);
      }
      else
        this.closeUserInfo();
    }

    public void worldTabUpdate(bool special)
    {
      if (this.attackTargetSidePanel.isVisible())
        this.attackTargetSidePanel.update();
      if (this.monkTargetSidePanel.isVisible())
        this.monkTargetSidePanel.update();
      if (this.reinforcementTargetSidePanel.isVisible())
        this.reinforcementTargetSidePanel.update();
      if (this.scoutTargetSidePanel.isVisible())
        this.scoutTargetSidePanel.update();
      if (this.stockExchangeSidePanel.isVisible())
        this.stockExchangeSidePanel.update();
      if (this.tradeWithPanel.isVisible())
        this.tradeWithPanel.update();
      if (this.vassalAttackVillagePanel.isVisible())
        this.vassalAttackVillagePanel.update();
      if (this.vassalSelectSidePanel.isVisible())
        this.vassalSelectSidePanel.update();
      if (this.emptyVillagePanel.isVisible())
        this.emptyVillagePanel.update();
      if (this.selectArmyPanel.isVisible())
        this.selectArmyPanel.update();
      if (this.selectReinforcementPanel.isVisible())
        this.selectReinforcementPanel.update();
      if (this.userInfoScreen.isVisible())
        this.userInfoScreen.update();
      if (this.doUserInfoUpdate)
        this.userInfoUpdate();
      if (this.parishCapitalVillagePanel.isVisible())
        this.parishCapitalVillagePanel.update();
      if (this.countyCapitalVillagePanel.isVisible())
        this.countyCapitalVillagePanel.update();
      if (this.countryCapitalVillagePanel.isVisible())
        this.countryCapitalVillagePanel.update();
      if (this.provinceCapitalVillagePanel.isVisible())
        this.provinceCapitalVillagePanel.update();
      if (this.ownParishCapitalPanel.isVisible())
        this.ownParishCapitalPanel.update();
      if (this.ownCountyCapitalPanel.isVisible())
        this.ownCountyCapitalPanel.update();
      if (this.ownProvinceCapitalPanel.isVisible())
        this.ownProvinceCapitalPanel.update();
      if (this.ownCountryCapitalPanel.isVisible())
        this.ownCountryCapitalPanel.update();
      if (this.ownVillagePanel.isVisible())
        this.ownVillagePanel.update();
      if (this.otherVillagePanel.isVisible())
        this.otherVillagePanel.update();
      if (this.vassalVillagePanel.isVisible())
        this.vassalVillagePanel.update();
      if (!this.mapFilterPanel.isVisible())
        return;
      this.mapFilterPanel.update();
    }

    public void deselectVillage()
    {
      if (this.SelectedVillage <= 0)
        return;
      this.SelectedVillage = -1;
    }

    public int SelectedVillage
    {
      get => this.m_reallySelectedVillage;
      set
      {
        this.m_reallySelectedVillage = value;
        GameEngine.Instance.World.createTributeLinesList(this.m_reallySelectedVillage);
      }
    }

    public int OwnSelectedVillage => this.m_ownSelectedVillage;

    public int SelectedVassalVillage => this.m_selectedVassalVillage;

    public void displaySelectedVillagePanel(
      int villageID,
      bool doubleClick,
      bool doShowUserInfo,
      bool forceSelfClick,
      bool forceInactiveNonPlayer)
    {
      UniversalDebugLog.Log("clicked on village! " + (object) villageID);
      this.showMapFilterSelectPanel(true, true, true, false);
      this.clearRightHandPanel_Special();
      this.doUserInfoUpdate = false;
      this.userInfoRefreshCountdown = 5;
      bool flag1 = this.emptyVillagePanel.isVisible();
      bool flag2 = this.ownVillagePanel.isVisible();
      bool flag3 = this.ownParishCapitalPanel.isVisible();
      bool flag4 = this.ownCountyCapitalPanel.isVisible();
      bool flag5 = this.ownProvinceCapitalPanel.isVisible();
      bool flag6 = this.ownCountryCapitalPanel.isVisible();
      bool flag7 = this.otherVillagePanel.isVisible();
      bool flag8 = this.parishCapitalVillagePanel.isVisible();
      bool flag9 = this.countyCapitalVillagePanel.isVisible();
      bool flag10 = this.provinceCapitalVillagePanel.isVisible();
      bool flag11 = this.countryCapitalVillagePanel.isVisible();
      bool flag12 = this.vassalVillagePanel.isVisible();
      bool flag13 = this.vassalAttackVillagePanel.isVisible();
      bool flag14 = false;
      bool flag15 = false;
      bool flag16 = false;
      bool flag17 = false;
      bool flag18 = false;
      bool flag19 = false;
      bool flag20 = false;
      bool flag21 = false;
      bool flag22 = false;
      bool flag23 = false;
      bool flag24 = false;
      bool flag25 = false;
      bool flag26 = false;
      WorldMap.SpecialVillageCache specialData = (WorldMap.SpecialVillageCache) null;
      if (!forceSelfClick && villageID == this.getSelectedMenuVillage())
        forceSelfClick = true;
      this.m_forcedMenuVillage = -1;
      bool flag27 = false;
      if (RemoteServices.Instance.Admin && GameEngine.shiftPressed && GameEngine.Instance.World.isCapital(villageID))
        flag27 = true;
      int y = 0;
      if ((forceSelfClick || doubleClick) && (GameEngine.Instance.World.isUserVillage(villageID) || flag27) && !forceInactiveNonPlayer)
      {
        this.setVillageNameBar(villageID);
        if (!GameEngine.Instance.World.isCapital(villageID))
        {
          GameEngine.Instance.MovedFromVillageID = villageID;
          flag15 = true;
          if (!this.ownVillagePanel.isVisible())
          {
            this.ownVillagePanel.initProperties(true, "", (ContainerControl) null);
            this.ownVillagePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
            this.ownVillagePanel.init();
          }
          this.ownVillagePanel.updateOwnVillageText(villageID);
          if (doubleClick)
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
        }
        else if (GameEngine.Instance.World.isRegionCapital(villageID))
        {
          flag16 = true;
          if (!this.ownParishCapitalPanel.isVisible())
          {
            this.ownParishCapitalPanel.initProperties(true, "", (ContainerControl) null);
            this.ownParishCapitalPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
            this.ownParishCapitalPanel.init();
          }
          this.ownParishCapitalPanel.updateOwnVillageText(villageID);
          if (doubleClick)
          {
            if (flag27)
              this.m_forcedMenuVillage = villageID;
            this.m_reallySelectedVillage = villageID;
            InterfaceMgr.Instance.getMainTabBar().changeTab(2);
          }
        }
        else if (GameEngine.Instance.World.isCountyCapital(villageID))
        {
          flag17 = true;
          if (!this.ownCountyCapitalPanel.isVisible())
          {
            this.ownCountyCapitalPanel.initProperties(true, "", (ContainerControl) null);
            this.ownCountyCapitalPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
            this.ownCountyCapitalPanel.init();
          }
          this.ownCountyCapitalPanel.updateOwnVillageText(villageID);
          if (doubleClick)
          {
            if (flag27)
              this.m_forcedMenuVillage = villageID;
            this.m_reallySelectedVillage = villageID;
            InterfaceMgr.Instance.getMainTabBar().changeTab(2);
          }
        }
        else if (GameEngine.Instance.World.isProvinceCapital(villageID))
        {
          flag18 = true;
          if (!this.ownProvinceCapitalPanel.isVisible())
          {
            this.ownProvinceCapitalPanel.initProperties(true, "", (ContainerControl) null);
            this.ownProvinceCapitalPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
            this.ownProvinceCapitalPanel.init();
          }
          this.ownProvinceCapitalPanel.updateOwnVillageText(villageID);
          if (doubleClick)
          {
            if (flag27)
              this.m_forcedMenuVillage = villageID;
            this.m_reallySelectedVillage = villageID;
            InterfaceMgr.Instance.getMainTabBar().changeTab(2);
          }
        }
        else if (GameEngine.Instance.World.isCountryCapital(villageID))
        {
          flag19 = true;
          if (!this.ownCountryCapitalPanel.isVisible())
          {
            this.ownCountryCapitalPanel.initProperties(true, "", (ContainerControl) null);
            this.ownCountryCapitalPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
            this.ownCountryCapitalPanel.init();
          }
          this.ownCountryCapitalPanel.updateOwnVillageText(villageID);
          if (doubleClick)
          {
            if (flag27)
              this.m_forcedMenuVillage = villageID;
            this.m_reallySelectedVillage = villageID;
            InterfaceMgr.Instance.getMainTabBar().changeTab(2);
          }
        }
        this.m_selectedVassalVillage = -1;
      }
      else if (GameEngine.Instance.World.isCapital(villageID) && this.m_selectedVassalVillage < 0)
      {
        if (GameEngine.Instance.World.isRegionCapital(villageID))
        {
          flag21 = true;
          if (!this.parishCapitalVillagePanel.isVisible())
          {
            this.parishCapitalVillagePanel.initProperties(true, "", (ContainerControl) null);
            this.parishCapitalVillagePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
            this.parishCapitalVillagePanel.init();
          }
          this.parishCapitalVillagePanel.updateParishCapitalVillageText(villageID, this.OwnSelectedVillage);
          if ((forceSelfClick || doubleClick) && GameEngine.Instance.World.isUserRelatedVillage(villageID))
          {
            this.m_ownSelectedVillage = -1;
            this.setVillageNameBar(villageID);
            GameEngine.Instance.MovedFromVillageID = villageID;
            this.parishCapitalVillagePanel.updateParishCapitalVillageText(villageID, this.OwnSelectedVillage);
            if (doubleClick)
            {
              this.m_reallySelectedVillage = villageID;
              InterfaceMgr.Instance.getMainTabBar().changeTab(2);
            }
          }
        }
        else if (GameEngine.Instance.World.isCountyCapital(villageID))
        {
          flag22 = true;
          if (!this.countyCapitalVillagePanel.isVisible())
          {
            this.countyCapitalVillagePanel.initProperties(true, "", (ContainerControl) null);
            this.countyCapitalVillagePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
            this.countyCapitalVillagePanel.init();
          }
          this.countyCapitalVillagePanel.updateCountyCapitalVillageText(villageID, this.OwnSelectedVillage);
          if ((forceSelfClick || doubleClick) && GameEngine.Instance.World.isUserRelatedVillage(villageID))
          {
            this.m_ownSelectedVillage = -1;
            this.setVillageNameBar(villageID);
            GameEngine.Instance.MovedFromVillageID = villageID;
            this.countyCapitalVillagePanel.updateCountyCapitalVillageText(villageID, this.OwnSelectedVillage);
            if (doubleClick)
            {
              this.m_reallySelectedVillage = villageID;
              InterfaceMgr.Instance.getMainTabBar().changeTab(2);
            }
          }
        }
        else if (GameEngine.Instance.World.isProvinceCapital(villageID))
        {
          flag23 = true;
          if (!this.provinceCapitalVillagePanel.isVisible())
          {
            this.provinceCapitalVillagePanel.initProperties(true, "", (ContainerControl) null);
            this.provinceCapitalVillagePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
            this.provinceCapitalVillagePanel.init();
          }
          this.provinceCapitalVillagePanel.updateProvinceCapitalVillageText(villageID, this.OwnSelectedVillage);
          if ((forceSelfClick || doubleClick) && GameEngine.Instance.World.isUserRelatedVillage(villageID))
          {
            this.m_ownSelectedVillage = -1;
            this.setVillageNameBar(villageID);
            GameEngine.Instance.MovedFromVillageID = villageID;
            this.provinceCapitalVillagePanel.updateProvinceCapitalVillageText(villageID, this.OwnSelectedVillage);
            if (doubleClick)
            {
              this.m_reallySelectedVillage = villageID;
              InterfaceMgr.Instance.getMainTabBar().changeTab(2);
            }
          }
        }
        else if (GameEngine.Instance.World.isCountryCapital(villageID))
        {
          flag24 = true;
          if (!this.countryCapitalVillagePanel.isVisible())
          {
            this.countryCapitalVillagePanel.initProperties(true, "", (ContainerControl) null);
            this.countryCapitalVillagePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
            this.countryCapitalVillagePanel.init();
          }
          this.countryCapitalVillagePanel.updateCountryCapitalVillageText(villageID, this.OwnSelectedVillage);
          if ((forceSelfClick || doubleClick) && GameEngine.Instance.World.isUserRelatedVillage(villageID))
          {
            this.m_ownSelectedVillage = -1;
            this.setVillageNameBar(villageID);
            GameEngine.Instance.MovedFromVillageID = villageID;
            this.countryCapitalVillagePanel.updateCountryCapitalVillageText(villageID, this.OwnSelectedVillage);
            if (doubleClick)
            {
              this.m_reallySelectedVillage = villageID;
              InterfaceMgr.Instance.getMainTabBar().changeTab(2);
            }
          }
        }
        this.m_reallySelectedVillage = villageID;
      }
      else
      {
        int num = GameEngine.Instance.World.getVillageUserID(villageID);
        if (GameEngine.Instance.LocalWorldData.AIWorld)
        {
          switch (num)
          {
            case 1:
            case 2:
            case 3:
            case 4:
              num = -1;
              break;
          }
        }
        if (num < 0)
        {
          if (!forceInactiveNonPlayer && this.m_selectedVassalVillage >= 0)
          {
            flag26 = true;
            if (!this.vassalAttackVillagePanel.isVisible())
            {
              this.vassalAttackVillagePanel.initProperties(true, "", (ContainerControl) null);
              this.vassalAttackVillagePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
              this.vassalAttackVillagePanel.init(villageID);
            }
            this.vassalAttackVillagePanel.updateOtherVillageText(villageID);
          }
          else
          {
            bool flag28 = false;
            if (GameEngine.Instance.World.isSpecial(villageID))
            {
              flag28 = true;
              specialData = GameEngine.Instance.World.getSpecialVillageData(villageID, true);
            }
            flag14 = true;
            if (!this.emptyVillagePanel.isVisible())
            {
              this.emptyVillagePanel.initProperties(true, "", (ContainerControl) null);
              this.emptyVillagePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
              this.emptyVillagePanel.init(villageID);
            }
            this.emptyVillagePanel.updateEmptyVillageText(villageID);
            this.emptyVillagePanel.updateSpecialData(specialData);
            if (forceInactiveNonPlayer && flag28)
              this.emptyVillagePanel.forceDisable();
          }
        }
        else if (!flag15)
        {
          if (!forceInactiveNonPlayer && GameEngine.Instance.World.isVassal(this.m_ownSelectedVillage, villageID))
          {
            flag25 = true;
            if (!this.vassalVillagePanel.isVisible())
            {
              this.vassalVillagePanel.initProperties(true, "", (ContainerControl) null);
              this.vassalVillagePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
              this.vassalVillagePanel.init();
            }
            this.vassalVillagePanel.updateVassalVillageText(villageID);
            if (this.m_selectedVassalVillage != villageID)
              this.m_selectedVassalVillage = -1;
          }
          else if (!forceInactiveNonPlayer && this.m_selectedVassalVillage >= 0)
          {
            flag26 = true;
            if (!this.vassalAttackVillagePanel.isVisible())
            {
              this.vassalAttackVillagePanel.initProperties(true, "", (ContainerControl) null);
              this.vassalAttackVillagePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
              this.vassalAttackVillagePanel.init(villageID);
            }
            this.vassalAttackVillagePanel.updateOtherVillageText(villageID);
          }
          else
          {
            flag20 = true;
            if (!this.otherVillagePanel.isVisible())
            {
              this.otherVillagePanel.initProperties(true, "", (ContainerControl) null);
              this.otherVillagePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
              this.otherVillagePanel.init();
            }
            this.otherVillagePanel.updateOtherVillageText(villageID);
            if (forceInactiveNonPlayer)
              this.otherVillagePanel.forceDisable();
          }
        }
      }
      if (GameEngine.Instance.World.isSpecial(villageID))
      {
        if (GameEngine.Instance.LocalWorldData.AIWorld)
        {
          switch (GameEngine.Instance.World.getSpecial(villageID))
          {
            case 7:
            case 9:
            case 11:
            case 13:
              break;
            default:
              doShowUserInfo = false;
              this.closeUserInfo();
              break;
          }
        }
        else
        {
          doShowUserInfo = false;
          this.closeUserInfo();
        }
      }
      if (doShowUserInfo)
      {
        this.doUserInfoUpdate = true;
        WorldMap.VillageRolloverInfo villageInfo = (WorldMap.VillageRolloverInfo) null;
        WorldMap.CachedUserInfo userInfo = (WorldMap.CachedUserInfo) null;
        if (villageID != this.lastViewedVillage)
        {
          this.lastViewedVillage = villageID;
          GameEngine.Instance.World.retrieveUserData(villageID, -1, ref villageInfo, ref userInfo, true, false);
        }
        else
          GameEngine.Instance.World.retrieveUserData(villageID, -1, ref villageInfo, ref userInfo, false, false);
        if (userInfo != null)
        {
          this.showUserInfo();
          this.userInfoPanel.updateVillageInfo(villageInfo, userInfo);
        }
        else
          this.closeUserInfo();
      }
      else
        GameEngine.Instance.World.clearCachedVillageUserInfo();
      if (flag1 && !flag14)
        this.emptyVillagePanel.closeControl(true);
      if (flag7 && !flag20)
        this.otherVillagePanel.closeControl(true);
      if (flag2 && !flag15)
        this.ownVillagePanel.closeControl(true);
      if (flag3 && !flag16)
        this.ownParishCapitalPanel.closeControl(true);
      if (flag4 && !flag17)
        this.ownCountyCapitalPanel.closeControl(true);
      if (flag5 && !flag18)
        this.ownProvinceCapitalPanel.closeControl(true);
      if (flag6 && !flag19)
        this.ownCountryCapitalPanel.closeControl(true);
      if (flag8 && !flag21)
        this.parishCapitalVillagePanel.closeControl(true);
      if (flag9 && !flag22)
        this.countyCapitalVillagePanel.closeControl(true);
      if (flag10 && !flag23)
        this.provinceCapitalVillagePanel.closeControl(true);
      if (flag11 && !flag24)
        this.countryCapitalVillagePanel.closeControl(true);
      if (flag12 && !flag25)
        this.vassalVillagePanel.closeControl(true);
      if (flag13 && !flag26)
        this.vassalAttackVillagePanel.closeControl(true);
      this.m_reallySelectedVillage = villageID;
      GameEngine.Instance.World.createTributeLinesList(villageID);
    }

    public void setVassalAttackMode(int vassalVillageID)
    {
      this.m_selectedVassalVillage = vassalVillageID;
    }

    public void selectVillage(int villageID) => this.m_reallySelectedVillage = villageID;

    public void closeSelectedVillagePanel()
    {
      this.closeUserInfo();
      this.emptyVillagePanel.closeControl(true);
      this.ownVillagePanel.closeControl(true);
      this.ownParishCapitalPanel.closeControl(true);
      this.ownCountyCapitalPanel.closeControl(true);
      this.ownProvinceCapitalPanel.closeControl(true);
      this.ownCountryCapitalPanel.closeControl(true);
      this.otherVillagePanel.closeControl(true);
      this.parishCapitalVillagePanel.closeControl(true);
      this.countyCapitalVillagePanel.closeControl(true);
      this.provinceCapitalVillagePanel.closeControl(true);
      this.countryCapitalVillagePanel.closeControl(true);
      this.vassalVillagePanel.closeControl(true);
      this.vassalAttackVillagePanel.closeControl(true);
      this.monkTargetSidePanel.closeControl(true);
      this.attackTargetSidePanel.closeControl(true);
      this.reinforcementTargetSidePanel.closeControl(true);
      this.scoutTargetSidePanel.closeControl(true);
      this.selectArmyPanel.closeControl(true);
      this.selectReinforcementPanel.closeControl(true);
      this.traderInfoPanel.closeControl(true);
      this.tradeWithPanel.closeControl(true);
      this.vassalAttackVillagePanel.closeControl(true);
      this.vassalSelectSidePanel.closeControl(true);
      this.m_reallySelectedVillage = -1;
    }

    public void closeSelectedVillagePanelButNotSelect()
    {
      this.closeUserInfo();
      this.emptyVillagePanel.closeControl(true);
      this.ownVillagePanel.closeControl(true);
      this.ownParishCapitalPanel.closeControl(true);
      this.ownCountyCapitalPanel.closeControl(true);
      this.ownProvinceCapitalPanel.closeControl(true);
      this.ownCountryCapitalPanel.closeControl(true);
      this.otherVillagePanel.closeControl(true);
      this.parishCapitalVillagePanel.closeControl(true);
      this.countyCapitalVillagePanel.closeControl(true);
      this.provinceCapitalVillagePanel.closeControl(true);
      this.countryCapitalVillagePanel.closeControl(true);
      this.vassalVillagePanel.closeControl(true);
      this.vassalAttackVillagePanel.closeControl(true);
      this.selectArmyPanel.closeControl(true);
      this.selectReinforcementPanel.closeControl(true);
      this.traderInfoPanel.closeControl(true);
      this.vassalAttackVillagePanel.closeControl(true);
    }

    public void clearRightHandPanel_Special()
    {
      this.vassalVillagePanel.closeControl(true);
      this.vassalAttackVillagePanel.closeControl(true);
      this.monkTargetSidePanel.closeControl(true);
      this.attackTargetSidePanel.closeControl(true);
      this.reinforcementTargetSidePanel.closeControl(true);
      this.scoutTargetSidePanel.closeControl(true);
      this.selectArmyPanel.closeControl(true);
      this.selectReinforcementPanel.closeControl(true);
      this.traderInfoPanel.closeControl(true);
      this.tradeWithPanel.closeControl(true);
      this.vassalAttackVillagePanel.closeControl(true);
      this.vassalSelectSidePanel.closeControl(true);
      this.mapFilterPanel.closeControl(true);
    }

    public void displayArmySelectPanel(long armyID)
    {
      int y = 0;
      this.MapSelectedArmy = armyID;
      if (!this.selectArmyPanel.isVisible())
      {
        this.selectArmyPanel.initProperties(true, "", (ContainerControl) null);
        this.selectArmyPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, y);
        this.selectArmyPanel.init();
      }
      this.selectArmyPanel.armySelected(armyID);
    }

    public void closeArmySelectedPanel()
    {
      this.selectArmyPanel.closeControl(true);
      this.MapSelectedArmy = -1L;
    }

    public void selectTutorialArmy()
    {
      long tutorialArmyId = GameEngine.Instance.World.getTutorialArmyID();
      if (tutorialArmyId < 0L)
        return;
      this.closeFilterPanel();
      this.closeSelectedVillagePanel();
      this.closeTraderInfoPanel();
      this.closeReinforcementSelectedPanel();
      this.closePersonInfoPanel();
      this.clearAndCloseUserInfo();
      this.displayArmySelectPanel(tutorialArmyId);
    }

    public void displayReinforcementSelectPanel(long armyID)
    {
      this.MapSelectedReinforcement = armyID;
      if (!this.selectReinforcementPanel.isVisible())
      {
        this.selectReinforcementPanel.initProperties(true, "", (ContainerControl) null);
        this.selectReinforcementPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
        this.selectReinforcementPanel.init();
      }
      this.selectReinforcementPanel.reinforcementSelected(armyID);
    }

    public void closeReinforcementSelectedPanel()
    {
      this.selectReinforcementPanel.closeControl(true);
      this.MapSelectedReinforcement = -1L;
    }

    public void displayTradeWithPanel()
    {
      this.tradeWithPanel.initProperties(true, "", (ContainerControl) null);
      this.tradeWithPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
      this.tradeWithPanel.init();
    }

    public void setTradeWithVillage(int villageID)
    {
      this.tradeWithPanel.setTradeWithVillage(villageID);
    }

    public void tradeWithResume(int villageID, bool keepInfo)
    {
      this.villageReportBackgroundPanel.tradeWithResume(villageID, keepInfo);
    }

    public void displayStockExchangeSidepanel()
    {
      this.stockExchangeSidePanel.initProperties(true, "", (ContainerControl) null);
      this.stockExchangeSidePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
      this.stockExchangeSidePanel.init();
    }

    public void setStockExchangeSidePanelVillage(int villageID)
    {
      this.stockExchangeSidePanel.setStockExchange(villageID);
    }

    public void selectStockExchange(int villageID)
    {
      this.villageReportBackgroundPanel.selectExchange(villageID);
    }

    public void displayAttackTargetSidepanel()
    {
      this.attackTargetSidePanel.initProperties(true, "", (ContainerControl) null);
      this.attackTargetSidePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
      this.attackTargetSidePanel.init();
    }

    public void setAttackTargetSidePanelVillage(int villageID)
    {
      this.attackTargetSidePanel.setTarget(villageID);
    }

    public void selectAttackTarget(int villageID)
    {
      this.villageReportBackgroundPanel.selectAttackTarget(villageID);
    }

    public void setVassalTargetVillage(int villageID, int targetVillageID)
    {
      this.villageReportBackgroundPanel.setVassalTargetVillage(villageID, targetVillageID);
    }

    public void setReinforcementVillage(int villageID)
    {
      this.villageReportBackgroundPanel.setReinforcementVillage(villageID);
    }

    public void displayScoutTargetSidepanel()
    {
      this.scoutTargetSidePanel.initProperties(true, "", (ContainerControl) null);
      this.scoutTargetSidePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
      this.scoutTargetSidePanel.init();
    }

    public void setScoutTargetSidePanelVillage(int villageID)
    {
      this.scoutTargetSidePanel.setTarget(villageID);
    }

    public void selectScoutTarget(int villageID)
    {
      this.villageReportBackgroundPanel.selectScoutsTarget(villageID);
    }

    public void displayTraderInfoPanel(long traderID)
    {
      this.MapSelectedTrader = traderID;
      if (!this.traderInfoPanel.isVisible())
      {
        this.traderInfoPanel.initProperties(true, "", (ContainerControl) null);
        this.traderInfoPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
        this.traderInfoPanel.init();
      }
      this.traderInfoPanel.setTrader(traderID);
    }

    public void closeTraderInfoPanel()
    {
      this.traderInfoPanel.closeControl(true);
      this.MapSelectedTrader = -1L;
    }

    public void updateTraderInfo()
    {
      if (!this.traderInfoPanel.isVisible())
        return;
      this.traderInfoPanel.update();
    }

    public void displayPersonInfoPanel(long personID)
    {
      this.MapSelectedPerson = personID;
      if (!this.personInfoPanel.isVisible())
      {
        this.personInfoPanel.initProperties(true, "", (ContainerControl) null);
        this.personInfoPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
        this.personInfoPanel.init();
      }
      this.personInfoPanel.setPerson(personID);
    }

    public void closePersonInfoPanel()
    {
      this.personInfoPanel.closeControl(true);
      this.MapSelectedPerson = -1L;
    }

    public void updatePersonInfo()
    {
      if (!this.personInfoPanel.isVisible())
        return;
      this.personInfoPanel.update();
    }

    public void initWorldTab_courtierTargetSelect()
    {
      this.worldMapMode = 5;
      this.courtierHomeVillage = -1;
      this.showDXWindow(true);
      this.displayReinforcementTargetSidepanel();
      this.setReinforcementTargetSidePanelVillage(-1);
      this.showMapFilterSelectPanel(false, true);
    }

    public void displayReinforcementTargetSidepanel()
    {
      this.reinforcementTargetSidePanel.initProperties(true, "", (ContainerControl) null);
      this.reinforcementTargetSidePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
      this.reinforcementTargetSidePanel.init();
    }

    public void setReinforcementTargetSidePanelVillage(int villageID)
    {
      this.reinforcementTargetSidePanel.setReinforcementTarget(villageID);
    }

    public int CourtierHomeVillage
    {
      get => this.courtierHomeVillage;
      set => this.courtierHomeVillage = value;
    }

    public void displayVassalSelectSidePanel()
    {
      this.vassalSelectSidePanel.initProperties(true, "", (ContainerControl) null);
      this.vassalSelectSidePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
      this.vassalSelectSidePanel.init();
    }

    public void setVassalSelectSidePanelVillage(int villageID)
    {
      this.vassalSelectSidePanel.setTarget(villageID);
    }

    public void selectVassalTarget(int villageID)
    {
      this.villageReportBackgroundPanel.selectVassalVillage(villageID);
    }

    public void displayMonkSelectSidePanel()
    {
      this.monkTargetSidePanel.initProperties(true, "", (ContainerControl) null);
      this.monkTargetSidePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
      this.monkTargetSidePanel.init();
    }

    public void setMonkSelectSidePanelVillage(int villageID)
    {
      this.monkTargetSidePanel.setTarget(villageID);
    }

    public void setVassalArmiesVillage(int villageID)
    {
      this.villageReportBackgroundPanel.setVassalArmiesVillage(villageID);
    }

    public bool wasShowingVassalSendScreen() => this.lastVillageTab == 15;

    public void setVillageInfoBar(VillageInfoBar2 infoBar, CastleInfoBar2 cInfoBar)
    {
      this.villageInfoBar = infoBar;
      this.castleInfoBar = cInfoBar;
    }

    public void initVillageTabTabBarsOnly()
    {
      this.getTopRightMenu().showVillageTab(true);
      this.getTopRightMenu().showFactionTabBar(false);
    }

    public void initVillageTab()
    {
      this.getTopRightMenu().showVillageTab(true);
      this.getTopRightMenu().showFactionTabBar(false);
      this.showDXWindow(false);
      this.showDXCardBar(10);
      this.villageMapPanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
      this.villageMapPanel.initProperties(true, "VillageBuildings", (ContainerControl) null);
      this.villageMapPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 6, 5);
      this.initVillageTab_Quick();
      if (GameEngine.Instance.World.getTutorialStage() == 100)
        GameEngine.Instance.World.advanceTutorialOLD();
      if (GameEngine.Instance.Village == null)
        return;
      GameEngine.Instance.Village.createSurroundSprites();
      GameEngine.Instance.Village.Camera.Drag(new Point(0, 0));
    }

    public void initVillageTab_Quick()
    {
      this.updateVillageInfoBar();
      if (GameEngine.Instance.Village != null)
        this.villageMapPanel.showAsVillage(!this.isSelectedVillageACapital(GameEngine.Instance.Village.VillageID));
      VillageMap.closePopups();
      this.villageMapPanel.showNewInterface();
      this.villageMapPanel.showExtras();
    }

    public void updateVillageInfoBar()
    {
      if (this.getVillageTabBar().getCurrentTab() == 0)
      {
        bool flag = GameEngine.Instance.World.isCapital(this.getSelectedMenuVillage());
        if (this.villageInfoBar.isVisible() && this.getVillageTabBar().lastVillageCapital == flag)
          return;
        this.villageInfoBar.show();
        this.villageInfoBar.removeHeading();
      }
      else
      {
        if (this.getVillageTabBar().getCurrentTab() != 3 || !GameEngine.Instance.World.isCapital(this.getSelectedMenuVillage()) || this.villageInfoBar.isVisible() && this.getVillageTabBar().lastVillageCapital)
          return;
        this.villageInfoBar.show();
        this.villageInfoBar.removeHeading();
      }
    }

    public void ensureInfoTabCleared()
    {
      if (!this.villageInfoBar.isVisible())
        return;
      this.villageInfoBar.hide();
    }

    public void initVillageTabView()
    {
      this.getTopRightMenu().showVillageTab(false);
      this.getTopRightMenu().showFactionTabBar(false);
      this.showDXWindow(false);
      this.showDXCardBar(10);
      this.villageMapPanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
      this.villageMapPanel.initProperties(true, "VillageBuildings", (ContainerControl) null);
      this.villageMapPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 6, 5);
      this.villageInfoBar.show();
      VillageMap.closePopups();
      this.villageMapPanel.showExtras();
    }

    public void villageMapResizeWindow()
    {
      this.villageMapPanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
    }

    public void SetVillageViewMode(bool viewOnly) => this.villageMapPanel.ViewOnly = viewOnly;

    public void setVillageInfoData(
      int woodLevel,
      int clayLevel,
      int stoneLevel,
      int foodLevel,
      bool gotStockpile,
      bool gotGranary,
      int totalPeople,
      int housingCapacity,
      int spareWorkers,
      int pitchLevel,
      bool viewOnly,
      int ironLevel,
      int capitalGold,
      int villageID,
      int numFlags)
    {
      switch (GameEngine.Instance.GameDisplayMode)
      {
        case GameEngine.GameDisplays.DISPLAY_VILLAGE:
          this.villageInfoBar.setDisplayedLevels(woodLevel, stoneLevel, foodLevel, gotStockpile, gotGranary, totalPeople, housingCapacity, spareWorkers, viewOnly, capitalGold, villageID, numFlags);
          break;
        case GameEngine.GameDisplays.DISPLAY_CASTLE:
          this.castleInfoBar.setDisplayedLevels(woodLevel, stoneLevel, pitchLevel, ironLevel);
          break;
      }
    }

    public void setVillageHeading(string text)
    {
      if (!this.villageInfoBar.isVisible() || this.villageInfoBar.Parent == null)
        this.villageInfoBar.show();
      this.villageInfoBar.Visible = true;
      this.villageInfoBar.setHeading(text);
    }

    public void showVillageStats(
      int taxLevel,
      int rationsLevel,
      int aleRationsLevel,
      int popularity,
      double popularityChange,
      string timeLeftString,
      string migrationTimeString,
      double effectiveRationsLevel,
      int numFoodTypesEaten,
      double effectiveAleRationsLevel,
      double housingChangeLevel,
      double goldDayRate,
      double dailyFoodConsumption,
      int totalPeople,
      int housingCapacity,
      int numPositiveBuildings,
      int numNegativeBuildings,
      PopEventData[] popEvents,
      double dailyAleConsumption,
      DateTime curTime,
      double foodProductionRate,
      double aleProductionRate,
      int numPopularityBuildings,
      int parishTax)
    {
      this.villageMapPanel.showStats(taxLevel, rationsLevel, aleRationsLevel, popularity, popularityChange, timeLeftString, effectiveRationsLevel, numFoodTypesEaten, effectiveAleRationsLevel, housingChangeLevel);
      this.villageMapPanel.showMigration(popularity, migrationTimeString, totalPeople, housingCapacity);
      this.villageMapPanel.showGoldChange(GameEngine.Instance.World.getCurrentGold(), GameEngine.Instance.World.getCurrentGoldRate());
      this.villageMapPanel.showDayRates(goldDayRate, dailyFoodConsumption, dailyAleConsumption, foodProductionRate, aleProductionRate, parishTax);
      this.villageMapPanel.showBuildingInfo(numPositiveBuildings, numNegativeBuildings, numPopularityBuildings);
      this.villageMapPanel.showPopEvents(popEvents, curTime);
    }

    public void showVillageStats2(
      int numChurches,
      int numChapels,
      int numCathedrals,
      int numSmallGardens,
      int numLargeGardens,
      int numSmallStatues,
      int numLargeStatues,
      int numDovecotes,
      int numStocks,
      int numBurningPosts,
      int numGibbets,
      int numRacks,
      bool lastbanquetStored,
      double lastBanquetHonour,
      DateTime lastBanquetDate,
      double lastTributePayment,
      double popularityLevel,
      int capitalTaxRate,
      int parishTax,
      ParishTaxCalc[] parishTaxPeople,
      int parentCapitalTaxRate,
      int lastCapitalTaxRate,
      int parishBonus)
    {
      this.villageMapPanel.showHonour();
      this.villageMapPanel.showHonourBuildings(numChurches, numChapels, numCathedrals, numSmallGardens, numLargeGardens, numSmallStatues, numLargeStatues, numDovecotes, numStocks, numBurningPosts, numGibbets, numRacks, popularityLevel, parishBonus);
      if (lastbanquetStored)
        this.villageMapPanel.showHonourBanquet(lastBanquetHonour, lastBanquetDate);
      else
        this.villageMapPanel.showHonourBanquet(0.0, lastBanquetDate);
      this.villageMapPanel.showCapitalData(capitalTaxRate, parishTax, parishTaxPeople, parentCapitalTaxRate, lastCapitalTaxRate);
    }

    public void closeVillageTab()
    {
      if (GameEngine.Instance.Village != null)
        GameEngine.Instance.Village.leaveMap();
      VillageMap.closePopups();
    }

    public bool isVillageMapPanelOnFoodTab() => this.villageMapPanel.isVillageMapPanelOnFoodTab();

    public bool isVillageMapPanelOnIndustryTab()
    {
      return this.villageMapPanel.isVillageMapPanelOnIndustryTab();
    }

    public bool isVillageMapPanelOnPopularityBar()
    {
      return this.villageMapPanel.isVillageMapPanelOnPopularityBar();
    }

    public bool isVillageHonourTabOpen() => this.villageMapPanel.isHonourTabOpen();

    public int getVillageMapPanelHonourTabPos() => this.villageMapPanel.calcInfoTabYPos();

    public int getVillageMapPanelBuildTabPos() => this.villageMapPanel.TUTORIAL_getBuildTabYPos();

    public void showVillageBuildingInfo(
      string buildingName,
      int woodCost,
      int stoneCost,
      int clayCost,
      int goldCost,
      int flagsNeeded,
      string buildTimeString,
      int buildingType,
      int realBuildingType)
    {
      this.villageMapPanel.setBuildingInfo(buildingName, woodCost, stoneCost, clayCost, goldCost, flagsNeeded, buildTimeString, buildingType, realBuildingType);
    }

    public void updateSidepanelAfterBuildingPlaced() => this.villageMapPanel.refreshCurrentTab();

    public void clearVillageBuildingInfo() => this.villageMapPanel.clearBuildingInfo();

    public void villageChanged(int villageID)
    {
      if (GameEngine.Instance.Village != null)
        GameEngine.Instance.Village.leaveMap();
      this.villageMapPanel.showAsVillage(!this.isSelectedVillageACapital(villageID));
      this.villageMapPanel.showNewInterface();
      VillageMap.closePopups();
    }

    public bool isInBuildingPanelOpen() => this.villageMapPanel.isInBuildingPanelOpen();

    public void showInBuildingInfo(VillageMapBuilding building)
    {
      this.villageMapPanel.showInBuildingInfo(building);
    }

    public VillageMapBuilding getInBuildingBuilding()
    {
      return this.villageMapPanel.getInBuildingBuilding();
    }

    public void villageReshowAfterStockpilePlaced()
    {
      this.villageMapPanel.villageReshowAfterStockpilePlaced();
    }

    public void capitalDonateResourcesInit(int villageID, VillageMapBuilding selectedBuilding)
    {
      this.villageReportBackgroundPanel.capitalDonateResourcesInit(villageID, selectedBuilding);
    }

    public void runVillageInterface()
    {
      if (!this.villageMapPanel.isVisible())
        return;
      this.villageMapPanel.run();
    }

    public void stopIndustryEnabled() => this.villageMapPanel.stopIndustryEnabled();

    public void mapPanelCreates()
    {
      this.villageMapPanel.create();
      this.castleMapPanel.create();
      this.castleMapAttackerSetupPanel.create();
      this.castleMapBattlePanel.create();
    }

    public int LastVillageTab => this.lastVillageTab;

    public void showFactionPanel(int factionID)
    {
      if (this.getMainTabBar().getCurrentTab() != 8)
      {
        GameEngine.Instance.setNextFactionPage(42);
        FactionMyFactionPanel.SelectedFaction = factionID;
        this.getMainTabBar().changeTab(8);
      }
      else if (this.getFactionTabBar().getCurrentTab() != 1)
      {
        GameEngine.Instance.setNextFactionPage(42);
        FactionMyFactionPanel.SelectedFaction = factionID;
        this.getFactionTabBar().changeTab(1);
      }
      else
      {
        FactionMyFactionPanel.SelectedFaction = factionID;
        this.setVillageTabSubMode(42, false);
      }
    }

    public void showHousePanel(int houseID)
    {
      if (this.getMainTabBar().getCurrentTab() != 8)
      {
        GameEngine.Instance.setNextFactionPage(52);
        HouseInfoPanel.SelectedHouse = houseID;
        this.getMainTabBar().changeTab(8);
      }
      else if (this.getFactionTabBar().getCurrentTab() != 2)
      {
        GameEngine.Instance.setNextFactionPage(52);
        HouseInfoPanel.SelectedHouse = houseID;
        this.getFactionTabBar().changeTab(2);
      }
      else
      {
        HouseInfoPanel.SelectedHouse = houseID;
        this.setVillageTabSubMode(52, false);
      }
    }

    public void showRoyalTowerPanel()
    {
      if (this.getMainTabBar().getCurrentTab() != 8)
      {
        GameEngine.Instance.setNextFactionPage(65);
        this.getMainTabBar().changeTab(8);
      }
      else if (this.getFactionTabBar().getCurrentTab() != 0)
      {
        GameEngine.Instance.setNextFactionPage(65);
        this.getFactionTabBar().changeTab(0);
      }
      else
        this.setVillageTabSubMode(65, false);
    }

    public void showStartFactionPanel()
    {
      FactionStartFactionPanel.StartFaction = true;
      this.setVillageTabSubMode(47, false);
    }

    public void showEditFactionPanel()
    {
      FactionStartFactionPanel.StartFaction = false;
      this.setVillageTabSubMode(47, false);
    }

    public void showFactionForumPosts(
      long threadID,
      long forumID,
      string threadTitle,
      string forumTitle)
    {
      FactionNewForumPostsPanel.ThreadID = threadID;
      FactionNewForumPostsPanel.parentForumID = forumID;
      FactionNewForumPostsPanel.ThreadTitle = threadTitle;
      FactionNewForumPostsPanel.ForumTitle = forumTitle;
      this.setVillageTabSubMode(48, false);
    }

    public void showCapitalForumPosts(
      long threadID,
      long forumID,
      string threadTitle,
      int areaID,
      int areaType,
      string forumTitle)
    {
      CapitalForumPostsPanel.ThreadID = threadID;
      CapitalForumPostsPanel.parentForumID = forumID;
      CapitalForumPostsPanel.ThreadTitle = threadTitle;
      CapitalForumPostsPanel.ForumTitle = forumTitle;
      CapitalForumPostsPanel.areaID = areaID;
      CapitalForumPostsPanel.areaType = areaType;
      this.setVillageTabSubMode(1009, false);
    }

    public void setVillageTabSubMode(int mode) => this.setVillageTabSubMode(mode, false);

    public void setVillageTabSubMode(int mode, bool overlayTab)
    {
      this.lastVillageTab = mode;
      if (mode == -1)
      {
        this.getDXBasePanel().Controls.Remove((Control) this.mainWindowPanel);
        this.parentMainWindow.setMainWindowAreaVisible(true);
        GameEngine.Instance.forceVillageTabUpdate();
        this.getVillageTabBar().forceChangeTab(0);
      }
      else
      {
        GameEngine.Instance.forceVillageTabUpdate();
        if (mode >= 1000 && !this.isSelectedVillageACapital())
          this.initVillageTab();
        else if (mode < 1000 && this.isSelectedVillageACapital() && mode != 6 && mode != 10 && mode != 19 && mode != 20 && mode != 21 && mode != 22 && mode != 23 && mode != 24 && mode != 25 && mode != 26 && mode != 31 && mode != 32 && mode != 33 && mode != 34 && mode != 41 && mode != 42 && mode != 45 && mode != 46 && mode != 43 && mode != 44 && mode != 47 && mode != 48 && mode != 52 && mode != 51 && mode != 99 && mode != 100 && mode != 60 && mode != 65)
        {
          this.initVillageTab();
        }
        else
        {
          this.StopDrawing();
          this.addMainWindow(this.firstVillageBackgroundCall, overlayTab);
          this.firstVillageBackgroundCall = false;
          this.villageReportBackgroundPanel.initProperties(true, "Village Reports", (ContainerControl) this.mainWindowPanel);
          this.villageReportBackgroundPanel.Size = this.mainWindowPanel.Size;
          this.villageReportBackgroundPanel.display((ContainerControl) this.mainWindowPanel, 0, 0);
          this.villageReportBackgroundPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.villageReportBackgroundPanel.showPanel(mode);
          switch (mode)
          {
            case 0:
              this.getVillageTabBar().changeTabGfxOnly(5);
              break;
            case 1:
              this.getVillageTabBar().changeTabGfxOnly(6);
              break;
            case 2:
            case 1002:
              this.getVillageTabBar().changeTabGfxOnly(3);
              break;
            case 3:
            case 1003:
              this.getVillageTabBar().changeTabGfxOnly(3);
              break;
            case 4:
            case 1004:
              this.getVillageTabBar().changeTabGfxOnly(4);
              break;
            case 5:
            case 1005:
              this.getVillageTabBar().changeTabGfxOnly(2);
              GameEngine.Instance.forceVillageTabUpdate();
              break;
            case 8:
              this.getVillageTabBar().changeTabGfxOnly(7);
              break;
            case 11:
            case 12:
            case 13:
              this.getVillageTabBar().changeTabGfxOnly(6);
              break;
            case 15:
            case 16:
            case 17:
              this.getVillageTabBar().changeTabGfxOnly(9);
              break;
            case 19:
            case 20:
            case 21:
            case 23:
            case 24:
            case 30:
              this.getTopRightMenu().showVillageTab(false);
              this.getTopRightMenu().showFactionTabBar(false);
              break;
            case 22:
              this.getTopRightMenu().showVillageTab(false);
              this.getTopRightMenu().showFactionTabBar(true);
              break;
            case 25:
              this.getTopRightMenu().showVillageTab(false);
              this.getTopRightMenu().showFactionTabBar(false);
              break;
            case 26:
              this.getTopRightMenu().showVillageTab(false);
              this.getTopRightMenu().showFactionTabBar(false);
              break;
            case 31:
              this.getTopRightMenu().showVillageTab(false);
              break;
            case 32:
            case 33:
            case 34:
              this.getTopRightMenu().showVillageTab(false);
              break;
            case 41:
            case 42:
            case 43:
            case 44:
            case 45:
            case 46:
            case 47:
            case 48:
            case 51:
            case 52:
              this.getTopRightMenu().showFactionTabBar(true);
              break;
            case 60:
              this.getTopRightMenu().showVillageTab(false);
              break;
            case 1006:
            case 1106:
            case 1206:
            case 1306:
              this.getVillageTabBar().changeTabGfxOnly(6);
              break;
            case 1007:
            case 1009:
            case 1107:
            case 1207:
            case 1307:
              this.getVillageTabBar().changeTabGfxOnly(7);
              break;
            case 1008:
            case 1108:
            case 1208:
            case 1308:
              this.getVillageTabBar().changeTabGfxOnly(5);
              break;
          }
          this.StartDrawing();
        }
      }
    }

    public void villageDownloaded(int villageID)
    {
      GameEngine.Instance.villageHasBeenDownloaded = true;
      ScoutPopupWindow scoutPopupWindow = this.getScoutPopupWindow();
      if (scoutPopupWindow != null && scoutPopupWindow.Visible && scoutPopupWindow.Created)
      {
        scoutPopupWindow.villageLoaded(villageID);
      }
      else
      {
        InterfaceMgr.Instance.getVillageTabBar().updateShownTabs();
        if (this.getVillageTabBar().getCurrentTab() > 1 || !this.getVillageTabBar().Visible)
          this.villageReportBackgroundPanel.newVillageLoaded();
        else if (this.getVillageTabBar().getCurrentTab() == 1)
        {
          this.initCastleTab();
        }
        else
        {
          if (this.getVillageTabBar().getCurrentTab() != 0)
            return;
          this.initVillageTab_Quick();
          if (!this.villageReportBackgroundPanel.isTab0OverLayActive())
            return;
          this.villageReportBackgroundPanel.newVillageLoaded();
        }
      }
    }

    public bool updateVillageReports()
    {
      if (this.lastVillageTab == -1)
        return true;
      this.villageReportBackgroundPanel.update(this.lastVillageTab);
      return false;
    }

    public void resetVillageReportPanelData() => this.villageReportBackgroundPanel.resetData();

    public void setCapitalSendTargetVillage(int villageID)
    {
      this.villageReportBackgroundPanel.setCapitalSendTargetVillage(villageID);
    }

    public bool isTextInputScreenActive()
    {
      return this.villageReportBackgroundPanel.isTextInputScreenActive();
    }

    public bool deleteReport(long reportID)
    {
      return this.villageReportBackgroundPanel.queryDeleteReport(reportID);
    }

    public Point getVillageReportBackgroundLocation()
    {
      return this.villageReportBackgroundPanel.getLocation();
    }

    public void initCastleTab()
    {
      this.getTopRightMenu().showVillageTab(true);
      this.showDXWindow(false);
      this.showDXCardBar(11);
      this.castleMapPanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
      this.castleMapPanel.initProperties(true, "Castle", (ContainerControl) null);
      this.castleMapPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 6, 5);
      this.castleMapPanel.showNewInterface();
      this.castleInfoBar.show();
      this.castle_ClearSelectedTroop();
      if (GameEngine.Instance.Castle != null)
      {
        GameEngine.Instance.Castle.createSurroundSprites();
        GameEngine.Instance.Castle.recalcCastleLayout();
        GameEngine.Instance.Castle.moveMap(0, 0);
        if (GameEngine.Instance.Castle.isAnyConstructing())
          Sound.playVillageEnvironmental(18);
        else
          Sound.playVillageEnvironmental(17);
      }
      if (GameEngine.Instance.World.getTutorialStage() != 10)
        return;
      GameEngine.Instance.World.handleQuestObjectiveHappening(10004);
    }

    public void castleMapResizeWindow()
    {
    }

    public void closeCastleTab()
    {
      if (GameEngine.Instance.Castle == null)
        return;
      GameEngine.Instance.Castle.leaveMap();
    }

    public void castleChanged()
    {
      if (GameEngine.Instance.Castle == null)
        return;
      GameEngine.Instance.Castle.leaveMap();
    }

    public void runCastleInterface()
    {
      if (!this.castleMapPanel.isVisible())
        return;
      this.castleMapPanel.Run();
    }

    public void showCastlePieceInfo(
      string pieceName,
      int woodCost,
      int stoneCost,
      int ironCost,
      int pitchCost,
      int goldCost,
      string buildTimeString,
      bool rollover)
    {
      this.castleMapPanel.setCastleElementInfo(pieceName, woodCost, stoneCost, ironCost, pitchCost, goldCost, buildTimeString, rollover);
    }

    public void castleStopPlacing()
    {
      this.castleMapPanel.clearCastlePlaceInfo();
      this.castleMapPanel.stopDeleting();
    }

    public void setCastleStats(
      int numGuardHouseSpaces,
      int numPlacedArchers,
      int numPlacedPeasants,
      int numPlacedPikemen,
      int numPlacedSwordsmen,
      DateTime completeTime,
      bool completed,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numPots,
      int numSmelterPlaces,
      bool castleDamaged,
      int numPlacedReinforceArchers,
      int numPlacedReinforcePeasants,
      int numPlacedReinforcePikemen,
      int numPlacedReinforceSwordsmen,
      int numReinforcePeasants,
      int numReinforceArchers,
      int numReinforcePikemen,
      int numReinforceSwordsmen,
      int numAvailableVassalReinforceDefenderPeasants,
      int numAvailableVassalReinforceDefenderArchers,
      int numAvailableVassalReinforceDefenderPikemen,
      int numAvailableVassalReinforceDefenderSwordsmen,
      int numPlacedVassalReinforceDefenderArchers,
      int numPlacedVassalReinforceDefenderPeasants,
      int numPlacedVassalReinforceDefenderPikemen,
      int numPlacedVassalReinforceDefenderSwordsmen,
      int numPlacedCaptains,
      int numCaptains)
    {
      this.castleMapPanel.setCastleStats(numGuardHouseSpaces, numPlacedArchers, numPlacedPeasants, numPlacedPikemen, numPlacedSwordsmen, completeTime, completed, numPeasants, numArchers, numPikemen, numSwordsmen, numPots, numSmelterPlaces, castleDamaged, numPlacedReinforceArchers, numPlacedReinforcePeasants, numPlacedReinforcePikemen, numPlacedReinforceSwordsmen, numReinforcePeasants, numReinforceArchers, numReinforcePikemen, numReinforceSwordsmen, numAvailableVassalReinforceDefenderPeasants, numAvailableVassalReinforceDefenderArchers, numAvailableVassalReinforceDefenderPikemen, numAvailableVassalReinforceDefenderSwordsmen, numPlacedVassalReinforceDefenderArchers, numPlacedVassalReinforceDefenderPeasants, numPlacedVassalReinforceDefenderPikemen, numPlacedVassalReinforceDefenderSwordsmen, numPlacedCaptains, numCaptains);
    }

    public void castle_SetSelectedTroop(
      int numPeasants,
      int peasantsState,
      int numArchers,
      int archersState,
      int numPikemen,
      int pikemenState,
      int numSwordsmen,
      int swordsmenState,
      int numCaptains,
      int captainState)
    {
      this.castleMapPanel.setSelectedTroop(numPeasants, peasantsState, numArchers, archersState, numPikemen, pikemenState, numSwordsmen, swordsmenState, numCaptains, captainState);
    }

    public void castleAttack_SetSelectedTroop(
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int numCaptains,
      int captainsCommand,
      int captainsData)
    {
      this.castleMapAttackerSetupPanel.setSelectedTroop(numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numCaptains, captainsCommand, captainsData);
    }

    public void castle_ClearSelectedTroop()
    {
      this.castleMapPanel.clearSelectedTroop();
      this.castleMapAttackerSetupPanel.clearSelectedTroop();
    }

    public void refreshCastleInterface() => this.castleMapPanel.refreshInterface();

    public void castleStartBuilderMode() => this.castleMapPanel.castleStartBuilderMode();

    public void castleEndBuilderMode() => this.castleMapPanel.castleEndBuilderMode();

    public void castleCommitReturn()
    {
      this.WaitingForCallback = false;
      this.castleMapPanel.castleCommitReturn();
    }

    public bool TUTORIAL_openedWoodTab() => this.castleMapPanel.TUTORIAL_openedWoodTab();

    public void initCastleAttackerSetupTab()
    {
      this.showDXWindow(false);
      this.showDXCardBar(6);
      this.castleMapAttackerSetupPanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
      this.castleMapAttackerSetupPanel.initProperties(true, "Castle", (ContainerControl) null);
      this.castleMapAttackerSetupPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 2, 5);
      this.castleMapAttackerSetupPanel.init();
    }

    public void setCastleViewTimes(
      DateTime castleViewTime,
      bool castleAvailable,
      DateTime troopViewTime,
      bool troopAvailable)
    {
      this.castleMapAttackerSetupPanel.setTimes(castleViewTime, castleAvailable, troopViewTime, troopAvailable);
    }

    public void castleAttackShowRealAttack(bool state)
    {
      this.castleMapAttackerSetupPanel.showRealAttack(state);
    }

    public void castleAttackShowAttackReady(bool state)
    {
      this.castleMapAttackerSetupPanel.showAttackReady(state);
    }

    public void castleShowPlacedAttackers(
      int numPlacedPeasants,
      int numPlacedArchers,
      int numPlacedPikemen,
      int numPlacedSwordsmen,
      int numPlacedCatapults,
      int maxPeasants,
      int maxArchers,
      int maxPikemen,
      int maxSwordsmen,
      int maxCatapults,
      int numCaptains,
      int maxCaptains,
      int captainsCommand,
      int numPeasantsInCastle,
      int numArchersInCastle,
      int numPikemenInCastle,
      int numSwordsmenInCastle)
    {
      this.castleMapAttackerSetupPanel.setStats(numPlacedArchers, numPlacedPikemen, numPlacedSwordsmen, numPlacedPeasants, numPlacedCatapults, maxPeasants, maxArchers, maxPikemen, maxSwordsmen, maxCatapults, numCaptains, maxCaptains, captainsCommand, numPeasantsInCastle, numArchersInCastle, numPikemenInCastle, numSwordsmenInCastle);
    }

    public void initCastleBattleTab(bool realBattle, int attackType, bool AIAttack)
    {
      this.showDXWindow(false);
      this.showDXCardBar(11);
      this.castleMapBattlePanel.Height = this.parentMainWindow.getMainRightHandPanel().Height;
      this.castleMapBattlePanel.initProperties(true, "Castle", (ContainerControl) null);
      this.castleMapBattlePanel.battleMode(realBattle, attackType, AIAttack);
      this.castleMapBattlePanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 2, 5);
    }

    public void castleBattleTimes(DateTime castleTime, DateTime troopTime)
    {
      this.castleMapBattlePanel.setTimes(castleTime, troopTime);
    }

    public void setCastlePillageClock(int pillageClock, int pillageClockMax)
    {
      this.castleMapBattlePanel.setPillageClock(pillageClock, pillageClockMax);
    }

    public void setCastleReportClock(int reportClock, int reportClockMax)
    {
      this.castleMapBattlePanel.setCastleReportClock(reportClock, reportClockMax);
    }

    public void ShowViewBattleResults(
      bool attackerVictory,
      BattleTroopNumbers startingTroops,
      BattleTroopNumbers endingTroops,
      int villageID,
      GetReport_ReturnType reportReturnData)
    {
      this.castleMapBattlePanel.ShowViewBattleResults(attackerVictory, startingTroops, endingTroops, villageID, reportReturnData);
    }

    public void showParishPanel(int parishID)
    {
    }

    public void closeParishPanel()
    {
      this.clearControlsLeaveRightHandPanel();
      this.getTopRightMenu().showVillageTab(false);
      this.getTopRightMenu().showFactionTabBar(false);
      this.worldMapMode = 0;
      this.parentMainWindow.setMainAreaVisible(true);
      this.showMapFilterSelectPanel(true, true);
    }

    public void flushParishFrontPageInfo(int parishID)
    {
      this.villageReportBackgroundPanel.flushParishFrontPageInfo(parishID);
    }

    public void showUserInfoScreen(WorldMap.CachedUserInfo userInfo)
    {
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_USER_INFO;
      this.addMainWindow(false, true);
      this.setVillageTabSubMode(99, true);
      this.villageReportBackgroundPanel.userInfoScreen.init(userInfo);
    }

    public void showAllVillagesScreen()
    {
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_ALL_VILLAGES;
      this.addMainWindow(false, true);
      this.setVillageTabSubMode(100, true);
      if (GameEngine.Instance.World.isAccountPremium())
        return;
      int num = (int) MyMessageBox.Show(SK.Text("AllVillageOverview_error2", "This feature requires a premium token to be in play."), SK.Text("AllVillageOverview_error", "Premium Village Overview"));
    }

    public void showUserInfoScreenAdmin(WorldMap.CachedUserInfo userInfo)
    {
      if (this.getMainTabBar().getCurrentTab() != 0)
        this.getMainTabBar().changeTab(0);
      this.clearControlsLeaveRightHandPanel();
      this.addMainWindow(false, true);
      this.userInfoScreen.Size = this.mainWindowPanel.Size;
      this.userInfoScreen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.userInfoScreen.clear();
      this.userInfoScreen.init(userInfo);
      this.userInfoScreen.display((ContainerControl) this.mainWindowPanel, 0, 0);
    }

    public void addMainWindow(bool allowBackgroundDraw, bool overlayTabBar)
    {
      int num = 0;
      if (overlayTabBar)
        num = 28;
      Size size = new Size(this.m_expandedMainSize.Width, this.m_expandedMainSize.Height + num);
      this.getTopLeftMenu().Height = 120 - num;
      this.getTopRightMenu().Height = 120 - num;
      this.mainWindowPanel.Size = size;
      this.getTopLeftMenu().setContextBarVisible(!overlayTabBar);
      this.mainWindowPanel.Location = new Point(0, 120 - num);
      this.mainWindowPanel.BringToFront();
      this.mainWindowPanel.doDraw(allowBackgroundDraw);
      if (!this.parentForm.Controls.Contains((Control) this.mainWindowPanel))
      {
        this.parentForm.SuspendLayout();
        this.parentForm.Controls.Add((Control) this.mainWindowPanel);
        this.parentForm.ResumeLayout(false);
      }
      this.parentMainWindow.setMainAreaVisible(false);
    }

    public void addMainMiniWindow(bool firstCall) => this.addMainMiniWindow(firstCall, false);

    public void addMainMiniWindow(bool firstCall, bool overlayTabBar)
    {
      int num = 0;
      if (overlayTabBar)
        num = 28;
      this.mainWindowPanel.Size = new Size(this.parentMainWindow.getDXBasePanel().Width, this.parentForm.ClientSize.Height - 120 + num);
      this.getTopLeftMenu().Height = 120 - num;
      this.getTopRightMenu().Height = 120 - num;
      this.getTopLeftMenu().setContextBarVisible(!overlayTabBar);
      this.mainWindowPanel.Location = new Point(0, 120 - num);
      this.mainWindowPanel.doDraw(firstCall);
      if (!this.parentForm.Controls.Contains((Control) this.mainWindowPanel))
      {
        this.parentForm.SuspendLayout();
        this.parentForm.Controls.Add((Control) this.mainWindowPanel);
        this.mainWindowPanel.ResumeLayout(false);
        this.parentForm.ResumeLayout(false);
      }
      this.parentMainWindow.setMainWindowAreaVisible(false);
    }

    public void reShowDXWindow() => this.showDXWindow(!this.getTopLeftMenu().contextBarVisible());

    public void showDXWindow(bool overlayTabBar)
    {
      int num = 0;
      if (overlayTabBar)
        num = 28;
      this.parentMainWindow.setMainAreaVisible(true);
      Size size = new Size(this.parentMainWindow.getDXBasePanel().Width, this.parentForm.ClientSize.Height - 120 + num);
      this.getTopLeftMenu().Height = 120 - num;
      this.getTopRightMenu().Height = 120 - num;
      this.getMainRightHandPanel().Height = this.parentForm.ClientSize.Height - 120 + num;
      this.getMainRightHandPanel().Location = new Point(this.getMainRightHandPanel().Location.X, 120 - num);
      this.parentMainWindow.getDXBasePanel().Size = size;
      this.getTopLeftMenu().setContextBarVisible(!overlayTabBar);
      this.parentMainWindow.getDXBasePanel().Location = new Point(0, 120 - num);
      GameEngine.Instance.GFX.resizeWindow();
      if (GameEngine.Instance.World == null)
        return;
      GameEngine.Instance.World.setScreenSize(this.parentMainWindow.getDXBasePanel().Width, this.parentMainWindow.getDXBasePanel().Height);
    }

    public void mainWindowStartResize() => this.moveMenuPopup();

    public void mainWindowResize()
    {
      this.m_expandedMainSize = this.parentMainWindow.getDXBasePanel().Size;
      this.m_expandedMainSize.Height = this.parentForm.ClientSize.Height - 120;
      this.m_expandedMainSize.Width += this.parentMainWindow.getMainRightHandPanel().Size.Width;
      if (this.getTopLeftMenu().Height != 120)
        this.parentMainWindow.getDXBasePanel().Size = new Size(this.parentMainWindow.getDXBasePanel().Width, this.parentForm.ClientSize.Height - 120 + 28);
      else
        this.parentMainWindow.getDXBasePanel().Size = new Size(this.parentMainWindow.getDXBasePanel().Width, this.parentForm.ClientSize.Height - 120);
      if (this.parentMainWindow.isFullMainArea())
      {
        Size expandedMainSize = this.m_expandedMainSize;
        if (this.getTopLeftMenu().Height != 120)
          expandedMainSize.Height += 28;
        this.mainWindowPanel.Size = expandedMainSize;
      }
      else
        this.mainWindowPanel.Size = this.parentMainWindow.getDXBasePanel().Size;
      this.parentMainWindow.getMainRightHandPanel().Size = new Size(this.parentMainWindow.getMainRightHandPanel().Width, this.parentMainWindow.getDXBasePanel().Height);
      this.villageReportBackgroundPanel.screenResize();
      this.mailScreenManager.screenResize();
      this.chatScreenManager.screenResize();
      this.movePlayCardsWindow();
      this.moveLogoutWindow();
      this.moveScoutPopupWindow();
      this.moveBuyVillagePopupWindow();
      this.moveReportCaptureWindow();
      this.moveNewQuestRewardPopup();
      this.moveGreyOutWindow();
      this.moveMenuPopup();
      this.moveTutorialWindow();
      this.moveTutorialArrowWindow();
      this.moveFreeCardsPopup();
      this.moveWheelPopup();
      this.moveWheelSelectPopup();
      this.moveAdvancedCastleOptionsPopup();
      if (!this.mapFilterSelectPanel.isVisible())
        return;
      this.showMapFilterSelectPanel(true, true, true, false);
    }

    public Size getMainWindowSize() => this.mainWindowPanel.Size;

    public void reportTabSetup() => RemoteServices.Instance.getReportFolders();

    public void initReportTab()
    {
      this.getTopRightMenu().showVillageTab(false);
      this.getTopRightMenu().showFactionTabBar(false);
      this.switchReportTabs(0);
    }

    public void switchReportTabs(int tabID)
    {
      this.mainWindowPanel.Controls.Clear();
      switch (tabID)
      {
        case 0:
          this.initReportsReports();
          break;
      }
    }

    public void initReportsReports() => this.setVillageTabSubMode(21, true);

    public void updateReports()
    {
    }

    public void moveReports(string folderName)
    {
      this.villageReportBackgroundPanel.moveReports(folderName);
    }

    public void deleteReportFolder(string folderName, int mode)
    {
      this.villageReportBackgroundPanel.deleteReportFolder(folderName, mode);
    }

    public object getReportData(long reportID)
    {
      return this.villageReportBackgroundPanel.getReportData(reportID);
    }

    public void setReportData(object reportData, long reportID)
    {
      this.villageReportBackgroundPanel.setReportData(reportData, reportID);
    }

    public void setReportAlreadyRead(long reportID)
    {
      this.villageReportBackgroundPanel.setReportAlreadyRead(reportID);
    }

    public void initReportsLeaderboard() => this.setVillageTabSubMode(20, true);

    public void updateLeaderboard()
    {
    }

    public void leaderboardSearchComplete(LeaderBoardSearchResults results)
    {
      this.villageReportBackgroundPanel.leaderboardSearchComplete(results);
    }

    public void initContestsLeaderboard() => this.setVillageTabSubMode(30, true);

    public void initContestHistory() => this.setVillageTabSubMode(31, true);

    public void initResearchTab()
    {
      this.getTopRightMenu().showVillageTab(false);
      this.getTopRightMenu().showFactionTabBar(false);
      this.researchPanel.initProperties(true, "Research", (ContainerControl) this.mainWindowPanel);
      this.researchPanel.Size = this.mainWindowPanel.Size;
      this.researchPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.researchPanel.updateBasedOnResearchData(GameEngine.Instance.World.UserResearchData, true);
      this.researchPanel.display((ContainerControl) this.mainWindowPanel, 0, 0);
    }

    public void updateResearch(bool fullTick) => this.researchPanel.update(fullTick);

    public void researchDataChanged(ResearchData data)
    {
      if (data == null || !this.researchPanel.isVisible())
        return;
      this.researchPanel.updateBasedOnResearchData(data, true);
    }

    public bool isResearchOnEducationTab() => this.researchPanel.isResearchOnEducationTab();

    public void initRankingsTab()
    {
      InterfaceMgr.Instance.setVillageTabSubMode(19, true);
      RankingsPanel.setRanking(GameEngine.Instance.World.getRank(), GameEngine.Instance.World.getRankSubLevel());
      this.updateVillageReports();
    }

    public void initGloryTab()
    {
      this.getTopRightMenu().showVillageTab(false);
      this.getTopRightMenu().showFactionTabBar(true);
      InterfaceMgr.Instance.setVillageTabSubMode(22);
    }

    public void inviteToFaction(string username)
    {
      GameEngine.Instance.setNextFactionPage(46);
      this.getMainTabBar().changeTab(8);
      InterfaceMgr.Instance.setVillageTabSubMode(46, false);
      this.villageReportBackgroundPanel.inviteToFaction(username);
    }

    public void downCurrentFactionInfo()
    {
      CustomSelfDrawPanel.FactionPanelSideBar.downloadCurrentFactionInfo();
    }

    public void initAllArmiesTab() => this.setVillageTabSubMode(23, true);

    public void updateAllArmiesPanel()
    {
    }

    public void initQuestsTab()
    {
      this.setVillageTabSubMode(26, true);
      if (GameEngine.Instance.World.TutorialIsAdvancing() || GameEngine.Instance.World.getTutorialStage() != 101)
        return;
      GameEngine.Instance.World.advanceTutorialOLD();
    }

    public void reloadQuestPanel() => this.villageReportBackgroundPanel.questPanelInit();

    public void updateQuestsPanel()
    {
    }

    public void completeQuest(int quest)
    {
      this.villageReportBackgroundPanel.questPanelCompleteQuest(quest);
    }

    public bool isMailDocked() => this.mailScreenManager.isDocked();

    public bool mailScreenNeedsOpening()
    {
      return this.mailScreenManager.isDocked() || !this.mailScreenManager.isMailScreenVisible();
    }

    public void initMailSubTab(int mode)
    {
      if (this.mailScreenManager.isDocked())
      {
        this.getTopRightMenu().showVillageTab(false);
        this.getTopRightMenu().showFactionTabBar(false);
        this.mailScreenManager.Size = this.mainWindowPanel.Size;
        this.mailScreenManager.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.mailScreenManager.initProperties(true, "Main", (ContainerControl) this.mainWindowPanel);
        this.mailScreenManager.open(false, false);
        this.mailScreenManager.display((ContainerControl) this.mainWindowPanel, 0, 0);
      }
      else
        this.mailScreenManager.open(false, false);
      if (mode != 1)
        return;
      this.mailScreenManager.startWithNewMessage(-1, "");
    }

    public void refreshForMail(bool success)
    {
    }

    public void mailTo(int userID, string userName)
    {
      this.mailScreenManager.mailTo(userID, userName);
    }

    public void sendProclamation(int mailType, int areaID)
    {
      this.mailScreenManager.sendProclamation(mailType, areaID);
    }

    public void mailTo(int userID, string[] userNames)
    {
      this.mailScreenManager.mailTo(userID, userNames);
    }

    public void mailUpdate() => this.mailScreenManager.mailUpdate();

    public void clearStoredMail() => this.mailScreenManager.clearStoredMail();

    public void mailPopupNewMail() => this.mailScreenManager.mailPopupNewMail();

    public void mailScreenRePop() => this.mailScreenManager.open(false, false);

    public bool isChatDocked() => this.chatScreenManager.isDocked();

    public void initChatPanel(int startingArea, int startAreaID)
    {
      if (this.chatScreenManager.isDocked())
      {
        this.getTopRightMenu().showVillageTab(false);
        this.getTopRightMenu().showFactionTabBar(false);
        this.chatScreenManager.Size = this.mainWindowPanel.Size;
        this.chatScreenManager.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.chatScreenManager.initProperties(true, "Main", (ContainerControl) this.mainWindowPanel);
        this.chatScreenManager.open(false, false, startingArea, startAreaID);
        this.chatScreenManager.display((ContainerControl) this.mainWindowPanel, 0, 0);
      }
      else
        this.chatScreenManager.open(false, false, startingArea, startAreaID);
    }

    public void chatUpdate() => this.chatScreenManager.chatUpdate();

    public void chatLogin() => this.chatScreenManager.login();

    public void chatLogout() => this.chatScreenManager.logout();

    public void chatClose()
    {
      this.chatScreenManager.close(true, true);
      this.chatScreenManager.logout();
    }

    public void chatSetBan(bool banned) => this.chatScreenManager.setChatBan(banned);

    public void showMapFilterSelectPanel(bool show, bool showAsOpen)
    {
      this.showMapFilterSelectPanel(show, showAsOpen, false, false);
    }

    public void showMapFilterSelectPanel(
      bool show,
      bool showAsOpen,
      bool force,
      bool forceDoubleHeight)
    {
      if (!show)
      {
        this.mapFilterSelectPanel.closeControl(true);
      }
      else
      {
        int height = this.ParentForm.Height;
        bool doubleHeight = false;
        if (height >= 750 || forceDoubleHeight)
          doubleHeight = true;
        if (!this.mapFilterSelectPanel.isVisible())
        {
          this.mapFilterSelectPanel.initProperties(true, "", (ContainerControl) null);
          this.mapFilterSelectPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          if (doubleHeight)
            this.mapFilterSelectPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 11, this.parentMainWindow.getMainRightHandPanel().Height - 60);
          else
            this.mapFilterSelectPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 11, this.parentMainWindow.getMainRightHandPanel().Height - 30);
        }
        else if (force)
        {
          if (doubleHeight)
          {
            if (this.mapFilterSelectPanel.Size.Height < 40)
              this.mapFilterSelectPanel.setPosition(11, this.parentMainWindow.getMainRightHandPanel().Height - 60);
          }
          else if (this.mapFilterSelectPanel.Size.Height > 40)
            this.mapFilterSelectPanel.setPosition(11, this.parentMainWindow.getMainRightHandPanel().Height - 30);
        }
        this.mapFilterSelectPanel.init(showAsOpen, doubleHeight);
      }
    }

    public void showMapFilterPanel()
    {
      this.clearControls();
      this.mapFilterPanel.initProperties(true, "", (ContainerControl) null);
      this.mapFilterPanel.display((ContainerControl) this.parentMainWindow.getMainRightHandPanel(), 0, 0);
      this.mapFilterPanel.init();
    }

    public void closeFilterPanel()
    {
      this.mapFilterPanel.closeControl(true);
      this.showMapFilterSelectPanel(true, true);
    }

    public int getSelectedMenuVillage()
    {
      if (this.m_forcedMenuVillage >= 0 && RemoteServices.Instance.Admin)
        return this.m_forcedMenuVillage;
      if (this.m_selectedMenuVillage >= 0 && (GameEngine.Instance.World.isUserVillage(this.m_selectedMenuVillage) || GameEngine.Instance.World.isUserRelatedVillage(this.m_selectedMenuVillage)))
        return this.m_selectedMenuVillage;
      int nextUserVillage = GameEngine.Instance.World.getNextUserVillage(-1, 1);
      this.setVillageNameBar(nextUserVillage);
      GameEngine.Instance.MovedFromVillageID = nextUserVillage;
      return nextUserVillage;
    }

    public void setVillageNameBar(int villageID)
    {
      if (GameEngine.Instance.World.isUserVillage(villageID) || GameEngine.Instance.World.isUserRelatedVillage(villageID))
        this.m_ownSelectedVillage = villageID;
      GameEngine.Instance.World.createTributeLinesList(villageID);
      this.m_selectedMenuVillage = villageID;
      this.parentMainWindow.getTopRightMenu().setSelectedVillageName(GameEngine.Instance.World.getVillageName(villageID), this.isSelectedVillageACapital(villageID));
    }

    public void centerOnVillage()
    {
      if (this.m_selectedMenuVillage < 0)
        return;
      this.selectUserVillage(this.m_selectedMenuVillage, true);
    }

    public void selectedVillageNameLeft()
    {
      int nextUserVillage = GameEngine.Instance.World.getNextUserVillage(this.m_selectedMenuVillage, -1);
      if (nextUserVillage < 0)
        return;
      this.selectUserVillage(nextUserVillage, true);
    }

    public void selectedVillageNameRight()
    {
      int nextUserVillage = GameEngine.Instance.World.getNextUserVillage(this.m_selectedMenuVillage, 1);
      if (nextUserVillage < 0)
        return;
      this.selectUserVillage(nextUserVillage, true);
    }

    public void selectVillageParent()
    {
      int villageParent = GameEngine.Instance.World.getVillageParent(this.m_selectedMenuVillage);
      if (villageParent < 0)
        return;
      this.selectUserVillage(villageParent, true);
    }

    public void selectVillageChildBestGuess()
    {
      int villageFromCapital = GameEngine.Instance.World.getPlayerChildVillageFromCapital(this.m_selectedMenuVillage);
      if (villageFromCapital < 0)
        return;
      this.selectUserVillage(villageFromCapital, true);
    }

    public void validateUserVillage()
    {
      if (this.m_selectedMenuVillage < 0 || GameEngine.Instance.World.isUserVillage(this.m_selectedMenuVillage) || GameEngine.Instance.World.isUserRelatedVillage(this.m_selectedMenuVillage))
        return;
      this.getMainTabBar().changeTab(0);
      this.setupVillageName();
    }

    public void setupVillageName()
    {
      this.m_selectedMenuVillage = -1;
      int num = -1;
      do
      {
        num = GameEngine.Instance.World.getNextUserVillage(num, 1);
      }
      while (num >= 0 && GameEngine.Instance.World.isCapital(num));
      if (num < 0)
        return;
      this.setVillageNameBar(num);
      this.selectUserVillage(num, false);
    }

    public void switchToSelectedVillage()
    {
      int selectedVillage = this.SelectedVillage;
      if (selectedVillage < 0)
        return;
      this.selectUserVillage(selectedVillage, true);
      GameEngine.Instance.downloadCurrentVillage();
      this.SelectedVillage = -1;
    }

    public void selectCurrentUserVillage()
    {
      if (this.m_selectedMenuVillage < 0 || !GameEngine.Instance.World.isUserVillage(this.m_selectedMenuVillage))
        return;
      this.selectUserVillage(this.m_selectedMenuVillage, false);
    }

    public void selectUserVillage(int villageID, bool zoomIn)
    {
      int ownSelectedVillage = this.OwnSelectedVillage;
      GameEngine.Instance.MovedFromVillageID = villageID;
      if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD)
      {
        if (zoomIn)
        {
          Point villageLocation = GameEngine.Instance.World.getVillageLocation(villageID);
          GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        }
        this.displaySelectedVillagePanel(villageID, false, true, true, false);
      }
      else
      {
        this.setVillageNameBar(villageID);
        if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE)
        {
          if (this.wasShowingVassalSendScreen())
            InterfaceMgr.Instance.setVillageTabSubMode(8);
          GameEngine.Instance.downloadCurrentVillage();
        }
        else if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
        {
          if (GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP || GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW)
          {
            InterfaceMgr.Instance.getMainTabBar().changeTab(9);
            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
          }
          else
            GameEngine.Instance.downloadCurrentVillage();
        }
      }
      if (this.getMainTabBar().getCurrentTab() == 1)
      {
        if (!GameEngine.Instance.World.isCapital(villageID))
          return;
        this.getMainTabBar().changeTabGfxOnly(2);
        GameEngine.Instance.externalMainTabChange(2);
      }
      else
      {
        if (this.getMainTabBar().getCurrentTab() != 2 || GameEngine.Instance.World.isCapital(villageID))
          return;
        this.getMainTabBar().changeTabGfxOnly(1);
        GameEngine.Instance.externalMainTabChange(1);
      }
    }

    public bool isPlayerElectedLeaderOfSelectedVillage()
    {
      return GameEngine.Instance.World.isUserVillage(this.OwnSelectedVillage);
    }

    public bool isSelectedVillageACapital()
    {
      return this.m_selectedMenuVillage >= 0 && GameEngine.Instance.World.isCapital(this.m_selectedMenuVillage);
    }

    public bool isSelectedVillageAParishCapital()
    {
      return this.m_selectedMenuVillage >= 0 && GameEngine.Instance.World.isRegionCapital(this.m_selectedMenuVillage);
    }

    public bool isSelectedVillageACountyCapital()
    {
      return this.m_selectedMenuVillage >= 0 && GameEngine.Instance.World.isCountyCapital(this.m_selectedMenuVillage);
    }

    public bool isSelectedVillageAProvinceCapital()
    {
      return this.m_selectedMenuVillage >= 0 && GameEngine.Instance.World.isProvinceCapital(this.m_selectedMenuVillage);
    }

    public bool isSelectedVillageACountryCapital()
    {
      return this.m_selectedMenuVillage >= 0 && GameEngine.Instance.World.isCountryCapital(this.m_selectedMenuVillage);
    }

    public bool isSelectedVillageACapital(int villageID)
    {
      return villageID >= 0 && GameEngine.Instance.World.isCapital(villageID);
    }

    public void setCardData(CardData cardData)
    {
      this.parentMainWindow.getTopLeftMenu().setCards(cardData);
    }

    public void setGold(double newGold) => this.parentMainWindow.getTopLeftMenu().setGold(newGold);

    public void setHonour(double newHonour, int rank)
    {
      this.parentMainWindow.getTopLeftMenu().setHonour(newHonour, rank);
    }

    public void setFaithPoints(double newFaithPoints)
    {
      this.parentMainWindow.getTopLeftMenu().SetFaithPoints(newFaithPoints);
    }

    public void setPoints(int points) => this.parentMainWindow.getTopLeftMenu().setPoints(points);

    public void setServerTime(DateTime serverTime, int gameDay)
    {
      this.parentMainWindow.getMainMenuBar().setServerTime(serverTime, gameDay);
    }

    public void setConnectionLight(bool loading)
    {
      if (this.getMainMenuBar() == null)
        return;
      this.getMainMenuBar().setLoadingLight(loading);
    }

    public bool isGameMinimised()
    {
      return this.parentForm == null || this.parentForm.WindowState == FormWindowState.Minimized;
    }

    public int getGameActivityMode()
    {
      if (this.parentForm == null)
      {
        this.lastTimeChangedMode = 2;
        return 3;
      }
      if (this.parentForm.WindowState == FormWindowState.Minimized)
      {
        this.lastTimeChangedMode = 1;
        return 5;
      }
      TimeSpan timeSpan = DateTime.Now - GameEngine.Instance.lastMouseMoveTime;
      if (timeSpan.TotalSeconds > 180.0)
      {
        this.lastTimeChangedMode = 2;
        if (timeSpan.TotalMinutes > 25.0)
          return 4;
        return timeSpan.TotalMinutes > 15.0 ? 3 : 2;
      }
      Form activeForm = Form.ActiveForm;
      if (activeForm == null)
      {
        if (this.lastTimeChangedMode == 0)
          this.timeChangedToMode1 = DateTime.Now;
        if ((DateTime.Now - this.timeChangedToMode1).TotalMinutes > 10.0)
          return 2;
        this.lastTimeChangedMode = 1;
        return 1;
      }
      if (activeForm == this.parentForm)
      {
        this.lastTimeChangedMode = 0;
        return 0;
      }
      for (Form parentForm = this.parentForm.ParentForm; parentForm != null; parentForm = parentForm.ParentForm)
      {
        if (parentForm == activeForm)
        {
          this.lastTimeChangedMode = 0;
          return 0;
        }
      }
      if (this.lastTimeChangedMode == 0)
        this.timeChangedToMode1 = DateTime.Now;
      if ((DateTime.Now - this.timeChangedToMode1).TotalMinutes > 10.0)
        return 2;
      this.lastTimeChangedMode = 1;
      return 1;
    }

    public string getPlagueText(int plagueLevel)
    {
      if (plagueLevel >= 181)
        return SK.Text("InterfaceMgr_Disease_10", "The Black Death");
      if (plagueLevel >= 161)
        return SK.Text("InterfaceMgr_Disease_9", "Plague Symptoms");
      if (plagueLevel >= 141)
        return SK.Text("InterfaceMgr_Disease_8", "Mass Delirium");
      if (plagueLevel >= 121)
        return SK.Text("InterfaceMgr_Disease_7", "Raging Fevers");
      if (plagueLevel >= 101)
        return SK.Text("InterfaceMgr_Disease_6", "Flu Epidemic");
      if (plagueLevel >= 81)
        return SK.Text("InterfaceMgr_Disease_5", "Flu Symptoms");
      if (plagueLevel >= 61)
        return SK.Text("InterfaceMgr_Disease_4", "Bronchitis");
      if (plagueLevel >= 41)
        return SK.Text("InterfaceMgr_Disease_3", "Coughing");
      return plagueLevel >= 21 ? SK.Text("InterfaceMgr_Disease_2", "Colds") : SK.Text("InterfaceMgr_Disease_1", "Slight Sniffles");
    }

    public SendArmyWindow openLaunchAttackPopup()
    {
      this.openGreyOutWindow(true);
      this.closePopupWindow((Form) this.m_launchAttackPopup);
      this.m_launchAttackPopup = new SendArmyWindow();
      this.positionWindow((Form) this.m_launchAttackPopup, true, false);
      this.m_launchAttackPopup.Show((IWin32Window) this.getGreyOutWindow());
      if (InterfaceMgr.Instance.isTutorialWindowOpen())
        GameEngine.Instance.World.forceTutorialToBeShown();
      GameEngine.Instance.DisableMouseClicks();
      return this.m_launchAttackPopup;
    }

    public void closeLaunchAttackPopup()
    {
      if (this.launchAttackPopupClosing)
        return;
      this.launchAttackPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_launchAttackPopup))
        this.closeGreyOut();
      this.m_launchAttackPopup = (SendArmyWindow) null;
      this.launchAttackPopupClosing = false;
      if (this.parentForm == null)
        return;
      this.parentForm.TopMost = true;
      this.parentForm.Focus();
      this.parentForm.BringToFront();
      this.parentForm.Focus();
      this.parentForm.TopMost = false;
    }

    public CreatePopupWindow openCreatePopupWindow()
    {
      this.openGreyOutWindowLogin(true);
      this.closePopupWindow((Form) this.m_createPopupWindow);
      this.m_createPopupWindow = new CreatePopupWindow();
      this.positionWindow((Form) this.m_createPopupWindow, true, false);
      this.m_createPopupWindow.init();
      this.m_createPopupWindow.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      this.m_createPopupWindow.Location = new Point(Program.profileLogin.Location.X + (Program.profileLogin.Width - this.m_createPopupWindow.Width) / 2, Program.profileLogin.Location.Y + (Program.profileLogin.Height - this.m_createPopupWindow.Height) / 2);
      Program.profileLogin.TopMost = false;
      this.m_greyOutWindow.BringToFront();
      this.m_createPopupWindow.BringToFront();
      this.m_createPopupWindow.TopMost = true;
      this.m_createPopupWindow.Focus();
      this.m_createPopupWindow.TopMost = false;
      return this.m_createPopupWindow;
    }

    public void moveCreatePopupWindow()
    {
      Form createPopupWindow = (Form) this.m_createPopupWindow;
      if (createPopupWindow == null || !createPopupWindow.Created)
        return;
      this.m_createPopupWindow.Location = new Point(Program.profileLogin.Location.X + (Program.profileLogin.Width - this.m_createPopupWindow.Width) / 2, Program.profileLogin.Location.Y + (Program.profileLogin.Height - this.m_createPopupWindow.Height) / 2);
    }

    public void closeCreatePopupWindow()
    {
      if (this.createPopupWindowClosing)
        return;
      this.createPopupWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_createPopupWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
        this.showDXCardBar(9);
        InterfaceMgr.Instance.closeParishPanel();
      }
      this.m_createPopupWindow = (CreatePopupWindow) null;
      this.createPopupWindowClosing = false;
      Program.profileLogin.TopMost = true;
      Program.profileLogin.BringToFront();
      Program.profileLogin.TopMost = false;
    }

    public CreatePopupWindow getCreatePopupWindow() => this.m_createPopupWindow;

    public bool isCreatePopup() => this.isPopupWindowOpen((Form) this.m_createPopupWindow);

    public WorldSelectPopupWindow openWorldSelectPopupWindow()
    {
      this.openGreyOutWindowLogin(true);
      this.closePopupWindow((Form) this.m_worldSelectPopupWindow);
      this.m_worldSelectPopupWindow = new WorldSelectPopupWindow();
      this.positionWindow((Form) this.m_worldSelectPopupWindow, true, false);
      this.m_worldSelectPopupWindow.init(0, false);
      this.m_worldSelectPopupWindow.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      this.m_worldSelectPopupWindow.Location = new Point(Program.profileLogin.Location.X + (Program.profileLogin.Width - this.m_worldSelectPopupWindow.Width) / 2, Program.profileLogin.Location.Y + (Program.profileLogin.Height - this.m_worldSelectPopupWindow.Height) / 2 + 10);
      Program.profileLogin.TopMost = false;
      this.m_greyOutWindow.BringToFront();
      this.m_worldSelectPopupWindow.BringToFront();
      this.m_worldSelectPopupWindow.TopMost = true;
      this.m_worldSelectPopupWindow.Focus();
      this.m_worldSelectPopupWindow.TopMost = false;
      return this.m_worldSelectPopupWindow;
    }

    public void moveWorldSelectPopupWindow()
    {
      Form selectPopupWindow = (Form) this.m_worldSelectPopupWindow;
      if (selectPopupWindow == null || !selectPopupWindow.Created)
        return;
      this.m_worldSelectPopupWindow.Location = new Point(Program.profileLogin.Location.X + (Program.profileLogin.Width - this.m_worldSelectPopupWindow.Width) / 2, Program.profileLogin.Location.Y + (Program.profileLogin.Height - this.m_worldSelectPopupWindow.Height) / 2 + 10);
    }

    public void closeWorldSelectPopupWindow()
    {
      if (this.worldSelectPopupWindowClosing)
        return;
      this.worldSelectPopupWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_worldSelectPopupWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
        this.showDXCardBar(9);
        InterfaceMgr.Instance.closeParishPanel();
      }
      this.m_worldSelectPopupWindow = (WorldSelectPopupWindow) null;
      this.worldSelectPopupWindowClosing = false;
      Program.profileLogin.TopMost = true;
      Program.profileLogin.BringToFront();
      Program.profileLogin.TopMost = false;
    }

    public WorldSelectPopupWindow getWorldSelectPopupWindow() => this.m_worldSelectPopupWindow;

    public bool isWorldSelectPopup()
    {
      return this.isPopupWindowOpen((Form) this.m_worldSelectPopupWindow);
    }

    public BPPopupWindow openBPPopupWindow(ProfileLoginWindow parentForm)
    {
      this.openGreyOutWindowLogin(true);
      this.closePopupWindow((Form) this.m_BPPopupWindow);
      this.m_BPPopupWindow = new BPPopupWindow();
      this.positionWindow((Form) this.m_BPPopupWindow, true, false);
      this.m_BPPopupWindow.init(parentForm);
      this.m_BPPopupWindow.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      this.m_BPPopupWindow.Location = new Point(Program.profileLogin.Location.X + (Program.profileLogin.Width - this.m_BPPopupWindow.Width) / 2, Program.profileLogin.Location.Y + (Program.profileLogin.Height - this.m_BPPopupWindow.Height) / 2 + 10);
      Program.profileLogin.TopMost = false;
      this.m_greyOutWindow.BringToFront();
      this.m_BPPopupWindow.BringToFront();
      this.m_BPPopupWindow.TopMost = true;
      this.m_BPPopupWindow.Focus();
      this.m_BPPopupWindow.TopMost = false;
      return this.m_BPPopupWindow;
    }

    public void moveBPPopupWindow()
    {
      Form bpPopupWindow = (Form) this.m_BPPopupWindow;
      if (bpPopupWindow == null || !bpPopupWindow.Created)
        return;
      this.m_BPPopupWindow.Location = new Point(Program.profileLogin.Location.X + (Program.profileLogin.Width - this.m_BPPopupWindow.Width) / 2, Program.profileLogin.Location.Y + (Program.profileLogin.Height - this.m_BPPopupWindow.Height) / 2 + 10);
    }

    public void closeBPPopupWindow()
    {
      if (this.BPPopupWindowClosing)
        return;
      this.BPPopupWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_BPPopupWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
        this.showDXCardBar(9);
        InterfaceMgr.Instance.closeParishPanel();
      }
      this.m_BPPopupWindow = (BPPopupWindow) null;
      this.BPPopupWindowClosing = false;
      Program.profileLogin.TopMost = true;
      Program.profileLogin.BringToFront();
      Program.profileLogin.TopMost = false;
    }

    public BPPopupWindow getBPPopupWindow() => this.m_BPPopupWindow;

    public bool isBPPopup() => this.isPopupWindowOpen((Form) this.m_BPPopupWindow);

    public VacationCancelPopupWindow openVacationCancelPopupWindow(
      int secondsLeft,
      int secondsLeftToCancel,
      bool canCancel)
    {
      this.openGreyOutWindowLogin(true);
      this.closePopupWindow((Form) this.m_VacationCancelPopupWindow);
      this.m_VacationCancelPopupWindow = new VacationCancelPopupWindow();
      this.positionWindow((Form) this.m_VacationCancelPopupWindow, true, false);
      this.m_VacationCancelPopupWindow.init(secondsLeft, secondsLeftToCancel, canCancel);
      this.m_VacationCancelPopupWindow.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      this.m_VacationCancelPopupWindow.Location = new Point(Program.profileLogin.Location.X + (Program.profileLogin.Width - this.m_VacationCancelPopupWindow.Width) / 2, Program.profileLogin.Location.Y + (Program.profileLogin.Height - this.m_VacationCancelPopupWindow.Height) / 2);
      Program.profileLogin.TopMost = false;
      this.m_greyOutWindow.BringToFront();
      this.m_VacationCancelPopupWindow.BringToFront();
      this.m_VacationCancelPopupWindow.TopMost = true;
      this.m_VacationCancelPopupWindow.Focus();
      this.m_VacationCancelPopupWindow.TopMost = false;
      return this.m_VacationCancelPopupWindow;
    }

    public void moveVacationCancelPopupWindow()
    {
      Form cancelPopupWindow = (Form) this.m_VacationCancelPopupWindow;
      if (cancelPopupWindow == null || !cancelPopupWindow.Created)
        return;
      this.m_VacationCancelPopupWindow.Location = new Point(Program.profileLogin.Location.X + (Program.profileLogin.Width - this.m_VacationCancelPopupWindow.Width) / 2, Program.profileLogin.Location.Y + (Program.profileLogin.Height - this.m_VacationCancelPopupWindow.Height) / 2);
    }

    public void closeVacationCancelPopupWindow()
    {
      if (this.VacationCancelPopupWindowClosing)
        return;
      this.VacationCancelPopupWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_VacationCancelPopupWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
        this.showDXCardBar(9);
        InterfaceMgr.Instance.closeParishPanel();
      }
      this.m_VacationCancelPopupWindow = (VacationCancelPopupWindow) null;
      this.VacationCancelPopupWindowClosing = false;
      Program.profileLogin.TopMost = true;
      Program.profileLogin.BringToFront();
      Program.profileLogin.TopMost = false;
    }

    public VacationCancelPopupWindow getVacationCancelPopupWindow()
    {
      return this.m_VacationCancelPopupWindow;
    }

    public bool isVacationCancelPopupWindow()
    {
      return this.isPopupWindowOpen((Form) this.m_VacationCancelPopupWindow);
    }

    public UpdatedTOSPopupWindow openUpdatedTOSPopupWindow()
    {
      this.openGreyOutWindowLogin(true);
      this.closePopupWindow((Form) this.m_worldSelectPopupWindow);
      this.m_UpdatedTOSPopupWindow = new UpdatedTOSPopupWindow();
      this.positionWindow((Form) this.m_UpdatedTOSPopupWindow, true, false);
      this.m_UpdatedTOSPopupWindow.init();
      this.m_UpdatedTOSPopupWindow.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      this.m_UpdatedTOSPopupWindow.Location = new Point(Program.profileLogin.Location.X + (Program.profileLogin.Width - this.m_UpdatedTOSPopupWindow.Width) / 2, Program.profileLogin.Location.Y + (Program.profileLogin.Height - this.m_UpdatedTOSPopupWindow.Height) / 2 + 10);
      Program.profileLogin.TopMost = false;
      this.m_greyOutWindow.BringToFront();
      this.m_UpdatedTOSPopupWindow.BringToFront();
      this.m_UpdatedTOSPopupWindow.TopMost = true;
      this.m_UpdatedTOSPopupWindow.Focus();
      this.m_UpdatedTOSPopupWindow.TopMost = false;
      return this.m_UpdatedTOSPopupWindow;
    }

    public void moveUpdatedTOSPopupWindow()
    {
      Form updatedTosPopupWindow = (Form) this.m_UpdatedTOSPopupWindow;
      if (updatedTosPopupWindow == null || !updatedTosPopupWindow.Created)
        return;
      this.m_UpdatedTOSPopupWindow.Location = new Point(Program.profileLogin.Location.X + (Program.profileLogin.Width - this.m_UpdatedTOSPopupWindow.Width) / 2, Program.profileLogin.Location.Y + (Program.profileLogin.Height - this.m_UpdatedTOSPopupWindow.Height) / 2 + 10);
    }

    public void closeUpdatedTOSPopupWindow()
    {
      if (this.UpdatedTOSPopupWindowClosing)
        return;
      this.UpdatedTOSPopupWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_UpdatedTOSPopupWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
        this.showDXCardBar(9);
        InterfaceMgr.Instance.closeParishPanel();
      }
      this.m_UpdatedTOSPopupWindow = (UpdatedTOSPopupWindow) null;
      this.UpdatedTOSPopupWindowClosing = false;
      Program.profileLogin.TopMost = true;
      Program.profileLogin.BringToFront();
      Program.profileLogin.TopMost = false;
    }

    public UpdatedTOSPopupWindow getUpdatedTOSPopupWindow() => this.m_UpdatedTOSPopupWindow;

    public bool isUpdatedTOSPopupWindow()
    {
      return this.isPopupWindowOpen((Form) this.m_UpdatedTOSPopupWindow);
    }

    public PlayCardsWindow openPlayCardsWindow(int cardSection)
    {
      this.openGreyOutWindow(false);
      this.closePopupWindow((Form) this.m_playCardsWindow);
      this.m_playCardsWindow = new PlayCardsWindow();
      this.positionWindow((Form) this.m_playCardsWindow, false, false);
      this.m_playCardsWindow.init(cardSection, true);
      this.m_playCardsWindow.Show((IWin32Window) this.getGreyOutWindow());
      if (InterfaceMgr.Instance.isTutorialWindowOpen())
        GameEngine.Instance.World.forceTutorialToBeShown();
      GameEngine.Instance.DisableMouseClicks();
      return this.m_playCardsWindow;
    }

    public PlayCardsWindow openPlayCardsWindowSearch(int cardSection, string searchText)
    {
      this.openGreyOutWindow(false);
      this.closePopupWindow((Form) this.m_playCardsWindow);
      this.m_playCardsWindow = new PlayCardsWindow();
      this.positionWindow((Form) this.m_playCardsWindow, false, false);
      this.m_playCardsWindow.init(cardSection, true);
      this.m_playCardsWindow.Show((IWin32Window) this.getGreyOutWindow());
      this.m_playCardsWindow.tbSearchBox.Text = searchText;
      this.m_playCardsWindow.performSearch();
      if (InterfaceMgr.Instance.isTutorialWindowOpen())
        GameEngine.Instance.World.forceTutorialToBeShown();
      GameEngine.Instance.DisableMouseClicks();
      return this.m_playCardsWindow;
    }

    public void movePlayCardsWindow()
    {
      this.positionWindow((Form) this.m_playCardsWindow, false, true);
    }

    public void closePlayCardsWindow()
    {
      if (this.playCardsWindowClosing)
        return;
      this.playCardsWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_playCardsWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        if (this.getScoutPopupWindow() == null && this.getSendMonkWindow() == null && this.getBuyVillageWindow() == null && this.m_launchAttackPopup == null)
          this.closeGreyOut();
      }
      this.m_playCardsWindow = (PlayCardsWindow) null;
      this.playCardsWindowClosing = false;
    }

    public Form getCardWindow() => (Form) this.m_playCardsWindow;

    public bool isCardPopupOpen() => this.isPopupWindowOpen((Form) this.m_playCardsWindow);

    public void setCurrentMenuPopup(MenuPopup menuPopup) => this.m_currentMenuPopup = menuPopup;

    public void moveMenuPopup() => this.closeMenuPopup();

    public void closeMenuPopup()
    {
      MainWindow.captureCloseMenuEvent = false;
      if (this.m_currentMenuPopup == null)
        return;
      this.closePopupWindow((Form) this.m_currentMenuPopup);
      this.m_currentMenuPopup = (MenuPopup) null;
      this.m_menuPopupClosedLastTime = DateTime.Now;
    }

    public bool menuPopupClosedRecently()
    {
      return (DateTime.Now - this.m_menuPopupClosedLastTime).TotalMilliseconds < 500.0;
    }

    public MenuPopup getMenuPopup() => this.m_currentMenuPopup;

    public bool isMenuPopupOpen() => this.isPopupWindowOpen((Form) this.m_currentMenuPopup);

    public void setCurrentCustomTooltip(CustomTooltip customTooltip)
    {
      this.m_currentCustomTooltip = customTooltip;
    }

    public void moveCustomTooltip()
    {
    }

    public void closeCustomTooltip()
    {
      if (!this.isPopupWindowOpen((Form) this.m_currentCustomTooltip))
        return;
      this.m_currentCustomTooltip.closing();
      this.m_currentCustomTooltip.Hide();
    }

    public CustomTooltip getCustomTooltip() => this.m_currentCustomTooltip;

    public void runTooltips()
    {
      CustomTooltipManager.runTooltips();
      if (this.m_currentCustomTooltip == null || !this.m_currentCustomTooltip.Created || !this.m_currentCustomTooltip.Visible)
        return;
      this.m_currentCustomTooltip.updateLocation();
    }

    public void setCurrentTutorialWindow(TutorialWindow tutorialWindow)
    {
      this.m_currentTutorialWindow = tutorialWindow;
    }

    public void moveTutorialWindow()
    {
      if (this.m_currentTutorialWindow == null || !this.m_currentTutorialWindow.Created)
        return;
      this.m_currentTutorialWindow.updateLocation(0, this.ParentForm);
    }

    public void closeTutorialWindow()
    {
      if (!this.isPopupWindowOpen((Form) this.m_currentTutorialWindow))
        return;
      this.m_currentTutorialWindow.closing();
      this.m_currentTutorialWindow.Hide();
    }

    public bool isTutorialWindowOpen()
    {
      return this.isPopupWindowOpen((Form) this.m_currentTutorialWindow);
    }

    public TutorialWindow getTutorialWindow() => this.m_currentTutorialWindow;

    public void runTutorialWindow()
    {
      if (this.m_currentTutorialWindow == null)
        return;
      this.m_currentTutorialWindow.update();
    }

    public void activateAchievementPopup(int id)
    {
      if (this.m_achievementPopup != null)
      {
        if (this.m_achievementPopup.isActive())
        {
          this.nextAchievementIDs.Add(id);
          return;
        }
        this.m_achievementPopup = (AchievementPopup) null;
      }
      this.m_achievementPopup = new AchievementPopup();
      this.m_achievementPopup.activate(id);
    }

    public void moveAchievementPopup()
    {
      if (this.m_achievementPopup == null)
        return;
      this.m_achievementPopup.move();
    }

    public void closeAchievementPopup()
    {
      if (this.m_achievementPopup == null)
        return;
      this.closePopupWindow((Form) this.m_achievementPopup);
      this.m_achievementPopup = (AchievementPopup) null;
    }

    public bool isInsideAchievementPopup()
    {
      return this.m_achievementPopup != null && this.m_achievementPopup.isMouseInside();
    }

    public void openTradeMode(int villageID, bool keepInfo)
    {
      GameEngine.Instance.SkipVillageTab();
      InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      InterfaceMgr.Instance.setVillageTabSubMode(2);
      this.tradeWithResume(villageID, keepInfo);
    }

    public ScoutPopupWindow openScoutPopupWindow(int villageID, bool resetData)
    {
      if (GameEngine.Instance.World.WorldEnded)
        return (ScoutPopupWindow) null;
      this.openGreyOutWindow(true);
      this.closePopupWindow((Form) this.m_scoutPopupWindow);
      this.m_scoutPopupWindow = new ScoutPopupWindow();
      this.positionWindow((Form) this.m_scoutPopupWindow, true, false);
      this.m_scoutPopupWindow.init(villageID, resetData);
      this.m_scoutPopupWindow.Show((IWin32Window) this.getGreyOutWindow());
      if (InterfaceMgr.Instance.isTutorialWindowOpen())
        GameEngine.Instance.World.forceTutorialToBeShown();
      GameEngine.Instance.DisableMouseClicks();
      return this.m_scoutPopupWindow;
    }

    public void moveScoutPopupWindow()
    {
      this.positionWindow((Form) this.m_scoutPopupWindow, true, true);
    }

    public void closeScoutPopupWindow()
    {
      if (this.scoutPopupWindowClosing)
        return;
      this.scoutPopupWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_scoutPopupWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
        this.showDXCardBar(9);
        InterfaceMgr.Instance.closeParishPanel();
      }
      this.m_scoutPopupWindow = (ScoutPopupWindow) null;
      this.scoutPopupWindowClosing = false;
    }

    public ScoutPopupWindow getScoutPopupWindow() => this.m_scoutPopupWindow;

    public bool isScoutPopup() => this.isPopupWindowOpen((Form) this.m_scoutPopupWindow);

    public SendMonkWindow openSendMonkWindow(int villageID)
    {
      this.openGreyOutWindow(true);
      this.closePopupWindow((Form) this.m_sendMonkWindow);
      this.m_sendMonkWindow = new SendMonkWindow();
      this.positionWindow((Form) this.m_sendMonkWindow, true, false);
      this.m_sendMonkWindow.init(villageID);
      this.m_sendMonkWindow.Show((IWin32Window) this.getGreyOutWindow());
      if (InterfaceMgr.Instance.isTutorialWindowOpen())
        GameEngine.Instance.World.forceTutorialToBeShown();
      GameEngine.Instance.DisableMouseClicks();
      return this.m_sendMonkWindow;
    }

    public void moveSendMonkWindow()
    {
      this.positionWindow((Form) this.m_sendMonkWindow, true, true);
    }

    public void closeSendMonkWindow()
    {
      if (this.sendMonkWindowClosing)
        return;
      this.sendMonkWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_sendMonkWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
        this.showDXCardBar(9);
        InterfaceMgr.Instance.closeParishPanel();
      }
      this.m_sendMonkWindow = (SendMonkWindow) null;
      this.sendMonkWindowClosing = false;
    }

    public SendMonkWindow getSendMonkWindow() => this.m_sendMonkWindow;

    public bool isSendMonk() => this.isPopupWindowOpen((Form) this.m_sendMonkWindow);

    public void openBuyVillageWindow(int villageID, bool buy)
    {
      this.openGreyOutWindow(true);
      this.closePopupWindow((Form) this.m_buyVillageWindow);
      this.m_buyVillageWindow = new BuyVillagePopupWindow();
      this.positionWindow((Form) this.m_buyVillageWindow, true, false);
      this.m_buyVillageWindow.init(villageID, buy);
      this.m_buyVillageWindow.Show((IWin32Window) this.getGreyOutWindow());
      if (InterfaceMgr.Instance.isTutorialWindowOpen())
        GameEngine.Instance.World.forceTutorialToBeShown();
      GameEngine.Instance.DisableMouseClicks();
    }

    public void moveBuyVillagePopupWindow()
    {
      this.positionWindow((Form) this.m_buyVillageWindow, true, true);
    }

    public void closeBuyVillagePopupWindow()
    {
      if (this.buyVillageWindowClosing)
        return;
      this.buyVillageWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_buyVillageWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
        this.showDXCardBar(9);
      }
      this.m_buyVillageWindow = (BuyVillagePopupWindow) null;
      this.buyVillageWindowClosing = false;
    }

    public BuyVillagePopupWindow getBuyVillageWindow() => this.m_buyVillageWindow;

    public bool isBuyVillage() => this.isPopupWindowOpen((Form) this.m_buyVillageWindow);

    public ConnectionErrorWindow openConnectionErrorWindow()
    {
      UniversalDebugLog.Log("Got connection error");
      this.openGreyOutWindow(true);
      this.closePopupWindow((Form) this.m_connectionErrorWindow);
      this.m_connectionErrorWindow = new ConnectionErrorWindow();
      this.positionWindow((Form) this.m_connectionErrorWindow, false, false);
      this.m_connectionErrorWindow.init();
      this.m_connectionErrorWindow.Show((IWin32Window) this.getGreyOutWindow());
      if (InterfaceMgr.Instance.isTutorialWindowOpen())
        GameEngine.Instance.World.forceTutorialToBeShown();
      GameEngine.Instance.DisableMouseClicks();
      return this.m_connectionErrorWindow;
    }

    public void moveConnectionErrorWindow()
    {
      this.positionWindow((Form) this.m_connectionErrorWindow, true, true);
    }

    public void closeConnectionErrorWindow()
    {
      if (this.connectionErrorWindowClosing)
        return;
      this.connectionErrorWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_connectionErrorWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
        this.showDXCardBar(9);
      }
      this.m_connectionErrorWindow = (ConnectionErrorWindow) null;
      this.connectionErrorWindowClosing = false;
    }

    public ConnectionErrorWindow getConnectionErrorWindow() => this.m_connectionErrorWindow;

    public bool isConnectionErrorWindow()
    {
      return this.isPopupWindowOpen((Form) this.m_connectionErrorWindow);
    }

    public GreyOutWindow openGreyOutWindow(bool showBorder)
    {
      if (this.isPopupWindowCreated((Form) this.m_greyOutWindow))
        return this.m_greyOutWindow;
      this.m_greyLogin = false;
      this.m_greyOutWindow = new GreyOutWindow();
      Size clientSize = this.parentMainWindow.ClientSize;
      this.m_greyOutWindow.Location = this.parentMainWindow.PointToScreen(new Point(0, 0));
      this.m_greyOutWindow.Size = clientSize;
      this.m_greyOutWindow.init(showBorder);
      this.m_greyOutWindow.Show((IWin32Window) this.ParentMainWindow);
      return this.m_greyOutWindow;
    }

    public GreyOutWindow openGreyOutWindowLogin(bool showBorder)
    {
      if (this.isPopupWindowCreated((Form) this.m_greyOutWindow))
        return this.m_greyOutWindow;
      if (Program.profileLogin == null)
        return (GreyOutWindow) null;
      this.m_greyLogin = true;
      this.m_greyOutWindow = new GreyOutWindow();
      Size clientSize = Program.profileLogin.ClientSize;
      this.m_greyOutWindow.Location = Program.profileLogin.PointToScreen(new Point(0, 0));
      this.m_greyOutWindow.Size = clientSize;
      this.m_greyOutWindow.init(showBorder);
      this.m_greyOutWindow.Show((IWin32Window) Program.profileLogin);
      return this.m_greyOutWindow;
    }

    public GreyOutWindow openGreyOutWindow(bool showBorder, Form parent)
    {
      if (this.isPopupWindowCreated((Form) this.m_greyOutWindow))
        return this.m_greyOutWindow;
      this.m_greyLogin = false;
      this.m_greyOutWindow = new GreyOutWindow();
      Size clientSize = parent.ClientSize;
      this.m_greyOutWindow.Location = parent.PointToScreen(new Point(0, 0));
      this.m_greyOutWindow.Size = clientSize;
      this.m_greyOutWindow.init(showBorder);
      this.m_greyOutWindow.Show((IWin32Window) parent);
      return this.m_greyOutWindow;
    }

    public void moveGreyOutWindow()
    {
      if (!this.isPopupWindowCreated((Form) this.m_greyOutWindow))
        return;
      if (this.m_greyLogin)
      {
        if (Program.profileLogin == null)
          return;
        Size clientSize = Program.profileLogin.ClientSize;
        this.m_greyOutWindow.Location = Program.profileLogin.PointToScreen(new Point(0, 0));
        this.m_greyOutWindow.Size = clientSize;
      }
      else
      {
        Size clientSize = this.parentMainWindow.ClientSize;
        this.m_greyOutWindow.Location = this.parentMainWindow.PointToScreen(new Point(0, 0));
        this.m_greyOutWindow.Size = clientSize;
      }
    }

    public void closeGreyOut()
    {
      this.closePopupWindow((Form) this.m_greyOutWindow);
      this.m_greyOutWindow = (GreyOutWindow) null;
    }

    public GreyOutWindow getGreyOutWindow() => this.m_greyOutWindow;

    public bool isGreyOutWindow() => this.isPopupWindowOpen((Form) this.m_greyOutWindow);

    public void setCurrentDonatePopup(DonatePopup donatePopup)
    {
      this.m_currentDonatePopup = donatePopup;
    }

    public void moveDonatePopup()
    {
    }

    public void closeDonatePopup()
    {
      if (!this.isPopupWindowOpen((Form) this.m_currentDonatePopup))
        return;
      this.m_currentDonatePopup.Hide();
    }

    public bool isDonatePopupOpen() => this.isPopupWindowOpen((Form) this.m_currentDonatePopup);

    public DonatePopup getDonatePopup() => this.m_currentDonatePopup;

    public LogoutOptionsWindow2 openLogoutWindow(bool normalLogout)
    {
      return this.openLogoutWindow(normalLogout, false);
    }

    public LogoutOptionsWindow2 openLogoutWindow(bool normalLogout, bool advertOnly)
    {
      this.openGreyOutWindow(false);
      this.closePopupWindow((Form) this.m_logoutOptionsWindow);
      this.m_logoutOptionsWindow = new LogoutOptionsWindow2();
      this.positionWindow((Form) this.m_logoutOptionsWindow, false, false);
      this.m_logoutOptionsWindow.init(normalLogout, advertOnly);
      this.m_logoutOptionsWindow.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      return this.m_logoutOptionsWindow;
    }

    public void moveLogoutWindow()
    {
      this.positionWindow((Form) this.m_logoutOptionsWindow, false, true);
    }

    public void closeLogoutWindow()
    {
      if (this.logoutWindowClosing)
        return;
      this.logoutWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_logoutOptionsWindow))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
      }
      this.m_logoutOptionsWindow = (LogoutOptionsWindow2) null;
      this.logoutWindowClosing = false;
    }

    public Form getLogoutWindow() => (Form) this.m_logoutOptionsWindow;

    public bool isLogoutPopupOpen() => this.isPopupWindowOpen((Form) this.m_logoutOptionsWindow);

    public ReportCapturePopup openReportCaptureWindow(int mode)
    {
      this.openGreyOutWindow(false);
      this.closePopupWindow((Form) this.m_reportCapturePopup);
      this.m_reportCapturePopup = new ReportCapturePopup();
      this.m_reportCapturePopup.init(mode);
      this.positionWindow((Form) this.m_reportCapturePopup, false, false);
      this.m_reportCapturePopup.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      return this.m_reportCapturePopup;
    }

    public void moveReportCaptureWindow()
    {
      this.positionWindow((Form) this.m_reportCapturePopup, false, true);
    }

    public void closeReportCaptureWindow()
    {
      if (this.reportCaptureWindowClosing)
        return;
      this.reportCaptureWindowClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_reportCapturePopup))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
      }
      this.m_reportCapturePopup = (ReportCapturePopup) null;
      this.reportCaptureWindowClosing = false;
    }

    public Form getReportCaptureWindow() => (Form) this.m_reportCapturePopup;

    public bool isReportCapturePopupOpen()
    {
      return this.isPopupWindowOpen((Form) this.m_reportCapturePopup);
    }

    public NewQuestRewardPopup openNewQuestRewardPopup(
      int questID,
      int villageID,
      NewQuestsPanel parent)
    {
      this.openGreyOutWindow(false);
      this.closePopupWindow((Form) this.m_newQuestRewardPopup);
      this.m_newQuestRewardPopup = new NewQuestRewardPopup();
      this.m_newQuestRewardPopup.init(questID, villageID, parent);
      this.positionWindow((Form) this.m_newQuestRewardPopup, false, false);
      this.m_newQuestRewardPopup.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      return this.m_newQuestRewardPopup;
    }

    public void moveNewQuestRewardPopup()
    {
      this.positionWindow((Form) this.m_newQuestRewardPopup, false, true);
    }

    public void closeNewQuestRewardPopup()
    {
      if (this.newQuestRewardPopupClosing)
        return;
      this.newQuestRewardPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_newQuestRewardPopup))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
      }
      this.m_newQuestRewardPopup = (NewQuestRewardPopup) null;
      this.newQuestRewardPopupClosing = false;
    }

    public Form getNewQuestRewardWindow() => (Form) this.m_newQuestRewardPopup;

    public bool isNewQuestRewardPopupOpen()
    {
      return this.isPopupWindowOpen((Form) this.m_newQuestRewardPopup);
    }

    public void openNewQuestsCompletedPopup(List<int> completedQuests)
    {
      this.closeNewQuestsCompletedPopup();
      this.newQuestsCompletedWindow = new NewQuestsCompletedWindow();
      this.newQuestsCompletedWindow.init(this.ParentForm, completedQuests, true, (string) null, -1);
      this.newQuestsCompletedWindow.Show((IWin32Window) this.ParentForm);
    }

    public void openNewQuestFurtherTextPopup(string questTag, int questID)
    {
      List<int> quests = new List<int>();
      this.closeNewQuestsCompletedPopup();
      this.newQuestsCompletedWindow = new NewQuestsCompletedWindow();
      this.newQuestsCompletedWindow.init(this.ParentForm, quests, false, questTag, questID);
      this.newQuestsCompletedWindow.Show((IWin32Window) this.ParentForm);
    }

    public void closeNewQuestsCompletedPopup()
    {
      if (this.newQuestsCompletedWindow == null)
        return;
      this.newQuestsCompletedWindow.Close();
      this.newQuestsCompletedWindow = (NewQuestsCompletedWindow) null;
    }

    public void openGloryVictoryPopup()
    {
      this.closeGloryVictoryWindowPopup();
      this.gloryVictoryWindow = new GloryVictoryWindow();
      this.gloryVictoryWindow.init(this.ParentForm);
      this.gloryVictoryWindow.Show((IWin32Window) this.ParentForm);
    }

    public void openGloryValuesPopup()
    {
      this.closeGloryVictoryWindowPopup();
      this.gloryVictoryWindow = new GloryVictoryWindow();
      this.gloryVictoryWindow.initValues(this.ParentForm);
      this.gloryVictoryWindow.Show((IWin32Window) this.ParentForm);
    }

    public void closeGloryVictoryWindowPopup()
    {
      if (this.gloryVictoryWindow == null)
        return;
      this.gloryVictoryWindow.Close();
      this.gloryVictoryWindow = (GloryVictoryWindow) null;
    }

    public AdvancedCastleOptionsPopup openAdvancedCastleOptionsPopup(bool castleSetup)
    {
      this.openGreyOutWindow(false);
      this.closePopupWindow((Form) this.m_advancedCastleOptionsPopup);
      this.m_advancedCastleOptionsPopup = new AdvancedCastleOptionsPopup();
      this.m_advancedCastleOptionsPopup.init(castleSetup);
      this.positionWindow((Form) this.m_advancedCastleOptionsPopup, false, false);
      this.m_advancedCastleOptionsPopup.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      return this.m_advancedCastleOptionsPopup;
    }

    public void moveAdvancedCastleOptionsPopup()
    {
      this.positionWindow((Form) this.m_advancedCastleOptionsPopup, false, true);
    }

    public void closeAdvancedCastleOptionsPopup()
    {
      if (this.advancedCastleOptionsPopupClosing)
        return;
      this.advancedCastleOptionsPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_advancedCastleOptionsPopup))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
      }
      this.m_advancedCastleOptionsPopup = (AdvancedCastleOptionsPopup) null;
      this.advancedCastleOptionsPopupClosing = false;
    }

    public Form getAdvancedCastleOptionsPopup() => (Form) this.m_advancedCastleOptionsPopup;

    public bool isAdvancedCastleOptionsPopup()
    {
      return this.isPopupWindowOpen((Form) this.m_advancedCastleOptionsPopup);
    }

    public FormationPopup openFormationPopup()
    {
      this.openGreyOutWindow(false);
      this.closePopupWindow((Form) this.m_formationPopup);
      this.m_formationPopup = new FormationPopup();
      this.positionWindow((Form) this.m_formationPopup, false, false);
      this.m_formationPopup.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      return this.m_formationPopup;
    }

    public void moveFormationPopup()
    {
      this.positionWindow((Form) this.m_formationPopup, false, true);
    }

    public void closeFormationPopup()
    {
      if (this.formationPopupClosing)
        return;
      this.formationPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_formationPopup))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
      }
      this.m_formationPopup = (FormationPopup) null;
      this.formationPopupClosing = false;
    }

    public Form getFormationPopup() => (Form) this.m_formationPopup;

    public bool isFormationPopup() => this.isPopupWindowOpen((Form) this.m_formationPopup);

    public PresetPopup openPresetPopup(PresetType type)
    {
      this.openGreyOutWindow(false);
      this.closePopupWindow((Form) this.m_presetPopup);
      this.m_presetPopup = new PresetPopup(type);
      this.positionWindow((Form) this.m_presetPopup, false, false);
      this.m_presetPopup.Show((IWin32Window) this.getGreyOutWindow());
      this.m_presetPopup.Location = InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().PointToScreen(new Point(0, 0));
      GameEngine.Instance.DisableMouseClicks();
      if (!PresetManager.Instance.IsDataReady)
        PresetManager.Instance.LoadPresetsFromServer(this.m_presetPopup.GetPanel());
      return this.m_presetPopup;
    }

    public void closePresetPopup()
    {
      if (this.presetPopupClosing)
        return;
      this.presetPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_presetPopup))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
      }
      this.m_presetPopup = (PresetPopup) null;
      this.presetPopupClosing = false;
    }

    public AttackTargetsPopup openAttackTargetsPopup()
    {
      this.closePopupWindow((Form) this.m_AttackTargetsPopup);
      this.m_AttackTargetsPopup = new AttackTargetsPopup();
      this.positionWindow((Form) this.m_AttackTargetsPopup, false, false);
      this.m_AttackTargetsPopup.Show((IWin32Window) this.ParentMainWindow);
      GameEngine.Instance.DisableMouseClicks();
      return this.m_AttackTargetsPopup;
    }

    public void moveAttackTargetsPopup()
    {
      this.positionWindow((Form) this.m_AttackTargetsPopup, false, true);
    }

    public void closeAttackTargetsPopup()
    {
      if (this.AttackTargetsPopupClosing)
        return;
      this.AttackTargetsPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_AttackTargetsPopup))
        GameEngine.Instance.EnableMouseClicks();
      this.m_AttackTargetsPopup = (AttackTargetsPopup) null;
      this.AttackTargetsPopupClosing = false;
    }

    public Form getAttackTargetsPopup() => (Form) this.m_AttackTargetsPopup;

    public bool isAttackTargetsPopup() => this.isPopupWindowOpen((Form) this.m_AttackTargetsPopup);

    public FreeCardsPopup openFreeCardsPopup()
    {
      this.openGreyOutWindow(true);
      this.closePopupWindow((Form) this.m_freeCardsPopup);
      this.m_freeCardsPopup = new FreeCardsPopup();
      this.positionWindow((Form) this.m_freeCardsPopup, false, false);
      this.m_freeCardsPopup.init();
      this.m_freeCardsPopup.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      return this.m_freeCardsPopup;
    }

    public void moveFreeCardsPopup()
    {
      this.positionWindow((Form) this.m_freeCardsPopup, false, true);
    }

    public void closeFreeCardsPopup()
    {
      if (this.freeCardsPopupClosing)
        return;
      this.freeCardsPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_freeCardsPopup))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
      }
      this.m_freeCardsPopup = (FreeCardsPopup) null;
      this.freeCardsPopupClosing = false;
    }

    public FreeCardsPopup getFreeCardsPopup() => this.m_freeCardsPopup;

    public bool isFreeCardsPopup() => this.isPopupWindowOpen((Form) this.m_freeCardsPopup);

    public WheelPopup openWheelPopup(int wheelType)
    {
      this.openGreyOutWindow(true);
      this.closePopupWindow((Form) this.m_WheelPopup);
      this.m_WheelPopup = new WheelPopup();
      this.positionWindow((Form) this.m_WheelPopup, false, false);
      this.m_WheelPopup.init(wheelType);
      this.m_WheelPopup.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      return this.m_WheelPopup;
    }

    public void moveWheelPopup() => this.positionWindow((Form) this.m_WheelPopup, false, true);

    public void closeWheelPopup()
    {
      if (this.WheelPopupClosing)
        return;
      WheelPanel.ClearInstance();
      this.WheelPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_WheelPopup))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
      }
      this.m_WheelPopup = (WheelPopup) null;
      this.WheelPopupClosing = false;
    }

    public WheelPopup getWheelPopup() => this.m_WheelPopup;

    public bool isWheelPopup() => this.isPopupWindowOpen((Form) this.m_WheelPopup);

    public WheelSelectPopup openWheelSelectPopup()
    {
      this.openGreyOutWindow(true);
      this.closePopupWindow((Form) this.m_WheelSelectPopup);
      this.m_WheelSelectPopup = new WheelSelectPopup();
      this.positionWindow((Form) this.m_WheelSelectPopup, false, false);
      this.m_WheelSelectPopup.init();
      this.m_WheelSelectPopup.Show((IWin32Window) this.getGreyOutWindow());
      GameEngine.Instance.DisableMouseClicks();
      return this.m_WheelSelectPopup;
    }

    public void moveWheelSelectPopup()
    {
      this.positionWindow((Form) this.m_WheelSelectPopup, false, true);
    }

    public void closeWheelSelectPopup()
    {
      if (this.WheelSelectPopupClosing)
        return;
      WheelPanel.ClearInstance();
      this.WheelSelectPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_WheelSelectPopup))
      {
        GameEngine.Instance.EnableMouseClicks();
        this.closeGreyOut();
      }
      this.m_WheelSelectPopup = (WheelSelectPopup) null;
      this.WheelSelectPopupClosing = false;
    }

    public WheelSelectPopup getWheelSelectPopup() => this.m_WheelSelectPopup;

    public bool isWheelSelectPopup() => this.isPopupWindowOpen((Form) this.m_WheelSelectPopup);

    public void showDominationWindow()
    {
      this.dominationWindow = new DominationWindow();
      this.dominationWindow.Show((IWin32Window) this.parentMainWindow);
    }

    public void updateDominationWindow(string text)
    {
      if (this.dominationWindow == null)
        return;
      this.dominationWindow.updateText(text);
    }

    public void closeDominatonWindow()
    {
      if (this.dominationWindow == null)
        return;
      this.dominationWindow.Close();
      this.dominationWindow = (DominationWindow) null;
    }

    public void openAdvicePopupFromButton(int screenID)
    {
      this.closeAdvicePopup();
      this.advicePopup = new AdvicePopup();
      this.advicePopup.init(this.ParentForm, screenID);
      this.advicePopup.Show((IWin32Window) this.ParentForm);
      this.setAdviceViewed(screenID);
    }

    public void openAdvicePopupFirstTime(int screenID)
    {
      if (!Program.mySettings.adviceEnabled || this.adviceIsViewed(screenID))
        return;
      this.closeAdvicePopup();
      this.advicePopup = new AdvicePopup();
      this.advicePopup.init(this.ParentForm, screenID);
      this.advicePopup.Show((IWin32Window) this.ParentForm);
      this.setAdviceViewed(screenID);
    }

    public void enableAdvicePopups(bool enabled) => Program.mySettings.adviceEnabled = enabled;

    public bool adviceIsViewed(int screenID)
    {
      string[] strArray = Program.mySettings.advicePanelsViewed.Split(',');
      if (strArray.Length == 1 && string.IsNullOrEmpty(strArray[0]))
        return false;
      foreach (string s in strArray)
      {
        if (int.Parse(s) == screenID)
          return true;
      }
      return false;
    }

    public void setAdviceViewed(int screenID)
    {
      if (this.adviceIsViewed(screenID))
        return;
      if (string.IsNullOrEmpty(Program.mySettings.advicePanelsViewed))
      {
        Program.mySettings.advicePanelsViewed = screenID.ToString();
      }
      else
      {
        MySettings settings = Program.mySettings;
        settings.advicePanelsViewed = settings.advicePanelsViewed + "," + screenID.ToString();
      }
    }

    public void closeAdvicePopup()
    {
      if (this.advicePopup == null)
        return;
      this.advicePopup.Close();
      this.advicePopup = (AdvicePopup) null;
    }

    public ConfirmOpenPackPopup openConfirmOpenPackPopup(
      UICardPack pack,
      ConfirmOpenPackPanel.CardClickPlayDelegate callback)
    {
      this.closePopupWindow((Form) this.m_confirmOpenPackPopup);
      this.m_confirmOpenPackPopup = new ConfirmOpenPackPopup();
      this.positionWindow((Form) this.m_confirmOpenPackPopup, false, false);
      this.m_confirmOpenPackPopup.init(pack, callback);
      this.m_confirmOpenPackPopup.Show((IWin32Window) this.getCardWindow());
      return this.m_confirmOpenPackPopup;
    }

    public void moveConfirmOpenPackPopup()
    {
      this.positionWindow((Form) this.m_confirmOpenPackPopup, true, true);
    }

    public void closeConfirmOpenPackPopup()
    {
      if (this.confirmOpenPackPopupClosing)
        return;
      this.confirmOpenPackPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_confirmOpenPackPopup))
        ((PlayCardsWindow) this.getCardWindow()).reactivatePanel();
      this.m_confirmOpenPackPopup = (ConfirmOpenPackPopup) null;
      this.confirmOpenPackPopupClosing = false;
    }

    public ConfirmOpenPackPopup getConfirmOpenPackPopup() => this.m_confirmOpenPackPopup;

    public bool isConfirmOpenPackPopup()
    {
      return this.isPopupWindowOpen((Form) this.m_confirmOpenPackPopup);
    }

    public ConfirmBuyOfferPopup openConfirmBuyOfferPopup(
      UICardOffer offer,
      ConfirmBuyOfferPanel.CardClickPlayDelegate callback)
    {
      this.closePopupWindow((Form) this.m_confirmBuyOfferPopup);
      this.m_confirmBuyOfferPopup = new ConfirmBuyOfferPopup();
      this.positionWindow((Form) this.m_confirmBuyOfferPopup, false, false);
      this.m_confirmBuyOfferPopup.init(offer, callback);
      this.m_confirmBuyOfferPopup.Show((IWin32Window) this.getCardWindow());
      return this.m_confirmBuyOfferPopup;
    }

    public void moveConfirmBuyOfferPopup()
    {
      this.positionWindow((Form) this.m_confirmBuyOfferPopup, true, true);
    }

    public void closeConfirmBuyOfferPopup()
    {
      if (this.confirmBuyOfferPopupClosing)
        return;
      this.confirmBuyOfferPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_confirmBuyOfferPopup))
        ((PlayCardsWindow) this.getCardWindow()).reactivatePanel();
      this.m_confirmBuyOfferPopup = (ConfirmBuyOfferPopup) null;
      this.confirmBuyOfferPopupClosing = false;
    }

    public ConfirmBuyOfferPopup getConfirmBuyOfferPopup() => this.m_confirmBuyOfferPopup;

    public bool isConfirmBuyOfferPopup()
    {
      return this.isPopupWindowOpen((Form) this.m_confirmBuyOfferPopup);
    }

    public ConfirmPlayCardPopup openConfirmPlayCardPopup(
      CardTypes.CardDefinition def,
      ConfirmPlayCardPanel.CardClickPlayDelegate callback)
    {
      this.closePopupWindow((Form) this.m_confirmPlayCardPopup);
      this.m_confirmPlayCardPopup = new ConfirmPlayCardPopup();
      this.positionWindow((Form) this.m_confirmPlayCardPopup, false, false);
      this.m_confirmPlayCardPopup.init(def, callback);
      this.m_confirmPlayCardPopup.Show((IWin32Window) this.getCardWindow());
      return this.m_confirmPlayCardPopup;
    }

    public void moveConfirmPlayCardPopup()
    {
      this.positionWindow((Form) this.m_confirmPlayCardPopup, true, true);
    }

    public void closeConfirmPlayCardPopup()
    {
      if (this.confirmPlayCardPopupClosing)
        return;
      this.confirmPlayCardPopupClosing = true;
      if (this.isPopupWindowOpenAndClose((Form) this.m_confirmPlayCardPopup) && this.getCardWindow() != null)
        ((PlayCardsWindow) this.getCardWindow()).reactivatePanel();
      this.m_confirmPlayCardPopup = (ConfirmPlayCardPopup) null;
      this.confirmPlayCardPopupClosing = false;
    }

    public ConfirmPlayCardPopup getConfirmPlayCardPopup() => this.m_confirmPlayCardPopup;

    public bool isConfirmPlayCardPopup()
    {
      return this.isPopupWindowOpen((Form) this.m_confirmPlayCardPopup);
    }

    public void setCurrentTutorialArrowWindow(TutorialArrowWindow donatePopup)
    {
      this.m_currentTutorialArrowWindow = donatePopup;
    }

    public void moveTutorialArrowWindow()
    {
      if (this.m_currentTutorialArrowWindow == null)
        return;
      this.m_currentTutorialArrowWindow.move();
    }

    public void closeTutorialArrowWindow()
    {
      if (!this.isPopupWindowOpen((Form) this.m_currentTutorialArrowWindow))
        return;
      this.m_currentTutorialArrowWindow.Hide();
    }

    public bool isTutorialArrowWindowOpen()
    {
      return this.isPopupWindowOpen((Form) this.m_currentTutorialArrowWindow);
    }

    public TutorialArrowWindow getTutorialArrowWindow() => this.m_currentTutorialArrowWindow;

    public void openAchievements(List<int> achievements)
    {
      this.closeMedalsPopup();
      this.medalsPopupPanel = new MedalsPopupWindow();
      this.medalsPopupPanel.init(achievements, this.ParentForm);
      this.medalsPopupPanel.Show((IWin32Window) this.ParentForm);
    }

    public void closeMedalsPopup()
    {
      if (this.medalsPopupPanel == null)
        return;
      this.medalsPopupPanel.Close();
      this.medalsPopupPanel = (MedalsPopupWindow) null;
    }

    public void updatePopups()
    {
      if (this.isTutorialArrowWindowOpen())
        TutorialArrowWindow.updateArrow();
      bool flag = true;
      if (this.isPopupWindowCreated((Form) this.m_playCardsWindow))
      {
        this.m_playCardsWindow.update();
        Form activeForm = Form.ActiveForm;
        if (Form.ActiveForm == this.ParentForm || this.m_launchAttackPopup != null && Form.ActiveForm == this.m_launchAttackPopup)
          this.m_playCardsWindow.Focus();
        flag = false;
      }
      if (this.isPopupWindowCreated((Form) this.m_launchAttackPopup))
      {
        this.m_launchAttackPopup.update();
        if (flag)
        {
          Form activeForm = Form.ActiveForm;
          if (Form.ActiveForm == this.ParentForm)
            this.m_launchAttackPopup.Focus();
          flag = false;
        }
      }
      if (this.isPopupWindowCreated((Form) this.m_scoutPopupWindow))
      {
        this.m_scoutPopupWindow.update();
        if (flag)
        {
          Form activeForm = Form.ActiveForm;
          if (Form.ActiveForm == this.ParentForm || this.m_launchAttackPopup != null && Form.ActiveForm == this.m_scoutPopupWindow)
            this.m_scoutPopupWindow.Focus();
          flag = false;
        }
      }
      if (this.isPopupWindowCreated((Form) this.m_sendMonkWindow))
      {
        this.m_sendMonkWindow.update();
        if (flag)
        {
          Form activeForm = Form.ActiveForm;
          if (Form.ActiveForm == this.ParentForm || this.m_launchAttackPopup != null && Form.ActiveForm == this.m_sendMonkWindow)
            this.m_sendMonkWindow.Focus();
          flag = false;
        }
      }
      if (this.isPopupWindowCreated((Form) this.m_buyVillageWindow))
      {
        this.m_buyVillageWindow.update();
        if (flag)
        {
          Form activeForm = Form.ActiveForm;
          if (Form.ActiveForm == this.ParentForm)
            this.m_buyVillageWindow.Focus();
          flag = false;
        }
      }
      if (this.isPopupWindowCreated((Form) this.m_connectionErrorWindow))
      {
        this.m_connectionErrorWindow.update();
        if (flag)
        {
          Form activeForm = Form.ActiveForm;
          if (Form.ActiveForm == this.ParentForm)
            this.m_connectionErrorWindow.Focus();
        }
      }
      if (this.isPopupWindowCreated((Form) this.m_currentDonatePopup))
      {
        Form activeForm = Form.ActiveForm;
        if (Form.ActiveForm != this.m_currentDonatePopup)
          this.closeDonatePopup();
      }
      if (this.isPopupWindowCreated((Form) this.m_logoutOptionsWindow))
      {
        this.m_logoutOptionsWindow.update();
        Form activeForm = Form.ActiveForm;
        if (Form.ActiveForm == this.ParentForm)
          this.m_logoutOptionsWindow.Focus();
      }
      if (this.isPopupWindowCreated((Form) this.m_reportCapturePopup))
      {
        this.m_reportCapturePopup.update();
        Form activeForm = Form.ActiveForm;
        if (Form.ActiveForm == this.ParentForm)
          this.m_reportCapturePopup.Focus();
      }
      if (this.isPopupWindowCreated((Form) this.m_newQuestRewardPopup))
      {
        this.m_newQuestRewardPopup.update();
        Form activeForm = Form.ActiveForm;
        if (Form.ActiveForm == this.ParentForm)
          this.m_newQuestRewardPopup.Focus();
      }
      if (this.isPopupWindowCreated((Form) this.m_advancedCastleOptionsPopup))
      {
        this.m_advancedCastleOptionsPopup.update();
        Form activeForm = Form.ActiveForm;
        if (Form.ActiveForm == this.ParentForm)
          this.m_advancedCastleOptionsPopup.Focus();
      }
      if (this.isPopupWindowCreated((Form) this.m_freeCardsPopup))
      {
        this.m_freeCardsPopup.update();
        Form activeForm = Form.ActiveForm;
        if (Form.ActiveForm == this.ParentForm)
          this.m_freeCardsPopup.Focus();
      }
      if (this.isPopupWindowCreated((Form) this.m_WheelPopup))
      {
        this.m_WheelPopup.update();
        Form activeForm = Form.ActiveForm;
        if (Form.ActiveForm == this.ParentForm)
          this.m_WheelPopup.Focus();
      }
      if (this.isPopupWindowCreated((Form) this.m_createPopupWindow))
        this.m_createPopupWindow.update();
      if (this.isPopupWindowCreated((Form) this.m_VacationCancelPopupWindow))
        this.m_VacationCancelPopupWindow.update();
      if (this.isPopupWindowCreated((Form) this.m_worldSelectPopupWindow))
        this.m_worldSelectPopupWindow.update();
      if (this.isPopupWindowCreated((Form) this.m_BPPopupWindow))
        this.m_BPPopupWindow.update();
      if (this.isPopupWindowCreated((Form) this.m_currentMenuPopup))
        this.m_currentMenuPopup.update();
      if (!this.isPopupWindowCreated((Form) this.m_achievementPopup))
        return;
      this.m_achievementPopup.update();
      if (this.m_achievementPopup.isActive())
        return;
      this.m_achievementPopup.Hide();
      this.m_achievementPopup = (AchievementPopup) null;
      if (this.nextAchievementIDs.Count <= 0)
        return;
      this.m_achievementPopup = new AchievementPopup();
      this.m_achievementPopup.activate(this.nextAchievementIDs[0]);
      this.nextAchievementIDs.RemoveAt(0);
    }

    public void closeAllPopups()
    {
      this.closeLaunchAttackPopup();
      this.closePlayCardsWindow();
      this.closeMenuPopup();
      this.closeCustomTooltip();
      this.closeTutorialWindow();
      this.closeAchievementPopup();
      this.closeScoutPopupWindow();
      this.closeSendMonkWindow();
      this.closeBuyVillagePopupWindow();
      this.closeDonatePopup();
      this.closeLogoutWindow();
      this.closeReportCaptureWindow();
      this.closeNewQuestRewardPopup();
      this.closeNewQuestsCompletedPopup();
      this.closeGloryVictoryWindowPopup();
      this.closeAdvancedCastleOptionsPopup();
      this.closeFreeCardsPopup();
      this.closeWheelPopup();
      this.closeWheelSelectPopup();
      this.closeConfirmPlayCardPopup();
      this.closeTutorialArrowWindow();
      this.closeMedalsPopup();
      this.closeConnectionErrorWindow();
      this.closeDominatonWindow();
      this.closeFormationPopup();
      this.closeAttackTargetsPopup();
      this.closeAdvicePopup();
    }

    private void closePopupWindow(Form window)
    {
      if (window == null || !window.Created)
        return;
      window.Close();
    }

    private void positionWindow(CustomSelfDrawPanel window, bool dxCentre, bool needCreated)
    {
    }

    private void positionWindow(Form window, bool dxCentre, bool needCreated)
    {
      if (window == null || !window.Created && needCreated)
        return;
      if (!dxCentre)
      {
        Point location = this.parentMainWindow.Location;
        Size clientSize = this.parentMainWindow.ClientSize;
        int x = (clientSize.Width - window.Size.Width) / 2 + location.X + 4;
        int y = (clientSize.Height - window.Size.Height - 120) / 2 + 120 + location.Y + 16;
        window.Location = new Point(x, y);
      }
      else
      {
        Size size = this.parentMainWindow.getDXBasePanel().Size;
        Point screen = this.parentMainWindow.getDXBasePanel().PointToScreen(new Point(0, 0));
        int x = (size.Width - window.Size.Width) / 2 + screen.X;
        int y = (size.Height - window.Size.Height) / 2 + screen.Y;
        window.Location = new Point(x, y);
      }
    }

    private bool isPopupWindowOpen(Form window)
    {
      return window != null && window.Created && window.Visible;
    }

    private bool isPopupWindowCreated(Form window) => window != null && window.Created;

    private bool isPopupWindowOpenAndClose(Form window)
    {
      if (window == null || !window.Created)
        return false;
      window.Close();
      return true;
    }

    public void processAchievements(List<int> achievements)
    {
      if (achievements == null)
      {
        RemoteServices.Instance.UserAchievements = new List<int>();
      }
      else
      {
        List<int> intList = new List<int>();
        bool flag = false;
        foreach (int achievement in achievements)
        {
          if (achievement == -1)
          {
            flag = true;
          }
          else
          {
            if (flag)
              this.activateAchievementPopup(achievement + 1000);
            intList.Add(achievement);
          }
        }
        RemoteServices.Instance.UserAchievements = intList;
      }
    }

    public int FloatingInputValue => this.m_floatingInputValue;

    public string FloatingInputString => this.m_floatingInputString;

    public void setFloatingValueSentDelegate(InterfaceMgr.FloatingValueSent del)
    {
      this.sendDelegate = del;
    }

    public void setFloatingTextSentDelegate(InterfaceMgr.FloatingTextSent del)
    {
      this.sendTextDelegate = del;
    }

    public void closeTextInput(int inputValue)
    {
      this.m_floatingInputValue = inputValue;
      FloatingInput.close();
      if (this.sendDelegate == null)
        return;
      this.sendDelegate(this.m_floatingInputValue);
      this.sendDelegate = (InterfaceMgr.FloatingValueSent) null;
    }

    public void closeTextStringInput(string inputValue)
    {
      this.m_floatingInputString = inputValue;
      FloatingInputText.close();
      if (this.sendTextDelegate == null)
        return;
      this.sendTextDelegate(this.m_floatingInputString);
      this.sendTextDelegate = (InterfaceMgr.FloatingTextSent) null;
    }

    public void toggleDXCardBarActive(bool value) => this.cardBarDX.toggleEnabled(value);

    public void showDXCardBar(int cardSection)
    {
      if (!DXPanel.skipPaint)
        this.cardBarDX.init(cardSection);
      else
        this.cardBarDX.delayedInit(cardSection);
    }

    public void updateDXCardBar() => this.cardBarDX.update();

    public bool clickDXCardBar(Point mousePos) => this.cardBarDX.click(mousePos);

    public void mouseMoveDXCardBar(Point mousePos) => this.cardBarDX.mouseMove(mousePos);

    public bool allowDrawCircles()
    {
      if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD)
        return true;
      int selectedMenuVillage = this.getSelectedMenuVillage();
      return selectedMenuVillage >= 0 && GameEngine.Instance.World.isUserVillage(selectedMenuVillage) && !GameEngine.Instance.World.isCapital(selectedMenuVillage);
    }

    public void togglePlaybackBarDXActive(bool value)
    {
      this.playbackBarDX.toggleEnabled(value);
      this.playbackEnabled = value;
      if (!value)
        return;
      this.showDXPlaybackBar();
    }

    public void showDXPlaybackBar()
    {
      if (!DXPanel.skipPaint)
        this.playbackBarDX.init();
      else
        this.playbackBarDX.delayedInit();
    }

    public void updateDXPlaybackBar() => this.playbackBarDX.update();

    public bool clickDXPlaybackBar(Point mousePos) => this.playbackBarDX.click(mousePos);

    public bool mouseUpDXPlaybackBar(Point mousePos) => this.playbackBarDX.mouseUp(mousePos);

    public bool mouseDownDXPlaybackBar(Point mousePos) => this.playbackBarDX.mouseDown(mousePos);

    public void mouseMoveDXPlaybackBar(Point mousePos) => this.playbackBarDX.mouseMove(mousePos);

    public delegate void FloatingValueSent(int value);

    public delegate void FloatingTextSent(string text);
  }
}
