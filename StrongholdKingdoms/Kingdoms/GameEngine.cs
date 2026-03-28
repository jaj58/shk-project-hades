// Decompiled with JetBrains decompiler
// Type: Kingdoms.GameEngine
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using StatTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class GameEngine
  {
    private const long FRAME_TIME = 33;
    private const int WM_LBUTTONDOWN = 513;
    private const int WM_LBUTTONUP = 514;
    private const int WM_LBUTTONDBLCLK = 515;
    private const int WM_MOUSEMOVE = 512;
    private const int WM_RBUTTONDOWN = 516;
    private const int WM_RBUTTONUP = 517;
    private const int WM_RBUTTONDBLCLK = 518;
    private const int WM_MBUTTONDOWN = 519;
    private const int WM_MBUTTONUP = 520;
    private const int WM_MBUTTONDBLCLK = 521;
    private const int WH_KEYBOARD_LL = 13;
    private const int WH_MOUSE_LL = 7;
    private const int WM_KEYDOWN = 256;
    private const int WM_KEYUP = 257;
    public static GameEngine Instance = (GameEngine) null;
    private GraphicsMgr gfx;
    private SparseArray villages = new SparseArray();
    private VillageMap village;
    private SparseArray castles = new SparseArray();
    private CastleMapRendering m_castleMapRendering;
    private CastleMap castle;
    private CastleMap castle_AttackerSetup;
    private CastleMap castle_Battle;
    private CastleMap castle_Preview;
    private WorldMap world = new WorldMap();
    private WorldData worldData;
    private readonly PremiumTokenManager m_premiumTokenManager = new PremiumTokenManager();
    private readonly CardsManager m_cardsManager = new CardsManager();
    private readonly CardPackManager m_cardPackManager = new CardPackManager();
    private readonly MonksManager m_monksManager = new MonksManager();
    private readonly VassalsManager m_vassalsManager = new VassalsManager();
    private readonly HouseManager m_houseManager = new HouseManager();
    private readonly FactionManager m_factionManager = new FactionManager();
    private Audio audio;
    private WorldMapTypes worldMapTypesData;
    private System.Threading.Timer m_tickTimer;
    private bool ticked;
    private bool firstCall = true;
    private bool m_doReLogin;
    private int currentResolution = -1;
    private int maxResolution = -1;
    private int newResolution = -1;
    private bool windowActive;
    private double lastFullTickTime;
    private double lastFullTickRegisterTime;
    private int tickCount;
    public static bool IsTOSLocked = false;
    private GameEngine.GameDisplays gameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
    private GameEngine.GameDisplaySubModes gameDisplayModeSubMode;
    private static NumberFormatInfo nfi = (NumberFormatInfo) null;
    private static NumberFormatInfo nfi_decimal = (NumberFormatInfo) null;
    private static NumberFormatInfo nfi_decimal1 = (NumberFormatInfo) null;
    private static NumberFormatInfo nfi_decimal2 = (NumberFormatInfo) null;
    private FontDesc dxFont1 = new FontDesc();
    private FontDesc dxFont2 = new FontDesc();
    private Thread m_WorkerThread;
    private bool gfxLoaded;
    private bool m_cancelLoading;
    private ProfileLoginWindow m_loginWindow;
    private bool forceTriggerFullTick;
    private LostVillageWindow lostVillagePopup;
    private WorldsEndWindow worldsEndPopup;
    private NewSelectVillageAreaWindow noVillagePopup;
    public NewAutoSelectVillageWindow noAutoVillagePopup;
    public int tryingToJoinCounty = -2;
    private bool pendingUserVillageZoom;
    public int clockFrame;
    public int clockMode;
    private long lastFrameTime;
    public int villageToAbandon = -1;
    public bool villageHasBeenDownloaded;
    private DateTime lastSoundClear = DateTime.MinValue;
    public bool finaliseResize;
    private int incomingAttacks;
    private long lastHighestArmyIDSeen = -1;
    public bool NewArmiesSeen;
    private bool shownLostVillage;
    private DateTime serverDowntime = DateTime.MinValue;
    private bool warning5;
    private bool warning15;
    private bool warning30;
    private bool warning60;
    private bool warning4H;
    private bool warning12H;
    private bool warning24H;
    private bool serverOffline;
    private ServerDowntimePopup m_downtimePopup;
    private int previousTabID = -1;
    private int lastTabID = -1;
    private bool m_firstDrawReports = true;
    private bool m_firstDrawRank = true;
    private bool m_firstDrawResearch = true;
    private bool m_firstDrawDummy = true;
    private bool m_firstDrawMail = true;
    private bool m_firstDrawArmy = true;
    private bool m_firstDrawFactions = true;
    private bool m_firstDrawLeaderboard = true;
    private bool m_firstDrawQuest = true;
    public int movedFromVillageID = -1;
    public int movedFromVillageIDNonCapital = -1;
    private int lastVillageTabID = -1;
    private bool skipVillageTab;
    private int nextFactionPage = -1;
    private int lastFactionTabID = -1;
    private int sentParentVillageID = -1;
    private int sentAttackingVillageID = -1;
    private int sentTargetVillageID = -1;
    private int pendingErrorCode = -1;
    private bool appClose;
    public string connectionErrorString = "";
    public bool forcingLogout;
    private bool quitGame;
    private int lastLoadedVillage = -1;
    public GameEngine.ConvertVillageDelegate convertVillageCallback;
    public int lastLoadedCastle = -1;
    private DebugPopup dPop;
    private LoginHistoryPopup m_loginHistoryPop;
    public bool stopInterfaceSounds;
    private static bool updatedPermissions = false;
    private GameEngine.MouseClickMessageFilter Filter;
    private string[] badWords;
    private static Censor staticCensor = (Censor) null;
    private static GameEngine.LowLevelKeyboardProc _proc = new GameEngine.LowLevelKeyboardProc(GameEngine.HookCallback);
    private static IntPtr _hookID = IntPtr.Zero;
    private static bool keyboardHookedInstalled = false;
    public static bool StopKeyTrap = false;
    private static int lastKeyPressed = 0;
    public static bool scrollUp = false;
    public static bool scrollDown = false;
    public static bool scrollLeft = false;
    public static bool scrollRight = false;
    public static bool shiftPressed = false;
    public static bool shiftPressedAlways = false;
    public static bool tabPressed = false;
    public static bool tabReleased = false;
    public static bool enterPressed = false;
    public static bool f11Pressed = false;
    public Point lastMouseMovePosition;
    public DateTime lastMouseMoveTime = DateTime.Now;
    private static string userPath = (string) null;
    private static string userPathBase = (string) null;
    private static string langPath = (string) null;

    public GraphicsMgr GFX => this.gfx;

    public VillageMap Village => this.village;

    public CastleMapRendering castleMapRendering
    {
      get
      {
        if (this.m_castleMapRendering == null)
          this.m_castleMapRendering = new CastleMapRendering(this.gfx);
        return this.m_castleMapRendering;
      }
    }

    public CastleMap Castle => this.castle;

    public CastleMap CastleAttackerSetup => this.castle_AttackerSetup;

    public CastleMap CastleBattle => this.castle_Battle;

    public CastleMap CastlePreview => this.castle_Preview;

    public WorldMap World => this.world;

    public WorldData LocalWorldData => this.worldData;

    public PremiumTokenManager premiumTokenManager => this.m_premiumTokenManager;

    public CardsManager cardsManager => this.m_cardsManager;

    public CardPackManager cardPackManager => this.m_cardPackManager;

    public MonksManager monksManager => this.m_monksManager;

    public VassalsManager vassalsManager => this.m_vassalsManager;

    public HouseManager houseManager => this.m_houseManager;

    public FactionManager factionManager => this.m_factionManager;

    public Audio AudioEngine => this.audio;

    public WorldMapTypes WorldMapTypesData => this.worldMapTypesData;

    public int NewResolution => this.newResolution;

    public int CurrentResolution => InterfaceMgr.Instance.ParentForm.Height < 960 ? 0 : 1;

    public int MaxResolution => this.maxResolution;

    public bool WindowActive
    {
      get => this.windowActive;
      set
      {
        this.windowActive = value;
        this.gfx.WindowActive = value;
      }
    }

    public GameEngine.GameDisplays GameDisplayMode
    {
      get => this.gameDisplayMode;
      set
      {
        if (Sound.isPlayingEnvironmental(19) && value != GameEngine.GameDisplays.DISPLAY_WORLD)
          Sound.stopVillageEnvironmental();
        this.gameDisplayMode = value;
        if (value != GameEngine.GameDisplays.DISPLAY_WORLD || InterfaceMgr.Instance.ParentForm == null || !InterfaceMgr.Instance.ParentForm.Visible)
          return;
        Sound.playVillageEnvironmental(19);
      }
    }

    public GameEngine.GameDisplaySubModes GameDisplayModeSubMode => this.gameDisplayModeSubMode;

    public static NumberFormatInfo NFI => GameEngine.nfi;

    public static NumberFormatInfo NFI_D => GameEngine.nfi_decimal;

    public static NumberFormatInfo NFI_D1 => GameEngine.nfi_decimal1;

    public static NumberFormatInfo NFI_D2 => GameEngine.nfi_decimal2;

    public GameEngine() => GameEngine.Instance = this;

    public void loadThread()
    {
      InterfaceMgr.Instance.showDXCardBar(0);
      if (!this.gfxLoaded)
      {
        UVSpriteLoader.loadUVX("assets\\uvx.resources");
        GFXLibrary.Instance.loadGFX(this.gfx);
        UVSpriteLoader.closeUVX();
        this.gfxLoaded = true;
      }
      else
        this.gfx.reloadGFX();
    }

    public bool cancelLoading() => this.m_cancelLoading;

    public void killLoadThread()
    {
      this.m_cancelLoading = true;
      while (!this.gfxLoaded)
      {
        Thread.Sleep(10);
        Program.DoEvents();
      }
    }

    public bool isStillLoading() => !GFXLibrary.Instance.worldMapLoaded;

    public bool Initialise(GraphicsMgr mgr, int maxRes, int curRes)
    {
      if (Program.mySettings.LanguageIdent == "de")
      {
        GameEngine.nfi = new CultureInfo("de-DE", false).NumberFormat;
        GameEngine.nfi.NumberDecimalDigits = 0;
        GameEngine.nfi_decimal = new CultureInfo("de-DE", false).NumberFormat;
        GameEngine.nfi_decimal1 = new CultureInfo("de-DE", false).NumberFormat;
        GameEngine.nfi_decimal1.NumberDecimalDigits = 1;
        GameEngine.nfi_decimal2 = new CultureInfo("de-DE", false).NumberFormat;
        GameEngine.nfi_decimal2.NumberDecimalDigits = 2;
      }
      else if (Program.mySettings.LanguageIdent == "fr")
      {
        GameEngine.nfi = new CultureInfo("fr-FR", false).NumberFormat;
        GameEngine.nfi.NumberDecimalDigits = 0;
        GameEngine.nfi_decimal = new CultureInfo("fr-FR", false).NumberFormat;
        GameEngine.nfi_decimal1 = new CultureInfo("fr-FR", false).NumberFormat;
        GameEngine.nfi_decimal1.NumberDecimalDigits = 1;
        GameEngine.nfi_decimal2 = new CultureInfo("fr-FR", false).NumberFormat;
        GameEngine.nfi_decimal2.NumberDecimalDigits = 2;
      }
      else if (Program.mySettings.LanguageIdent == "ru")
      {
        GameEngine.nfi = new CultureInfo("ru-RU", false).NumberFormat;
        GameEngine.nfi.NumberDecimalDigits = 0;
        GameEngine.nfi_decimal = new CultureInfo("ru-RU", false).NumberFormat;
        GameEngine.nfi_decimal1 = new CultureInfo("ru-RU", false).NumberFormat;
        GameEngine.nfi_decimal1.NumberDecimalDigits = 1;
        GameEngine.nfi_decimal2 = new CultureInfo("ru-RU", false).NumberFormat;
        GameEngine.nfi_decimal2.NumberDecimalDigits = 2;
      }
      else if (Program.mySettings.LanguageIdent == "es")
      {
        GameEngine.nfi = new CultureInfo("es-ES", false).NumberFormat;
        GameEngine.nfi.NumberDecimalDigits = 0;
        GameEngine.nfi_decimal = new CultureInfo("es-ES", false).NumberFormat;
        GameEngine.nfi_decimal1 = new CultureInfo("es-ES", false).NumberFormat;
        GameEngine.nfi_decimal1.NumberDecimalDigits = 1;
        GameEngine.nfi_decimal2 = new CultureInfo("es-ES", false).NumberFormat;
        GameEngine.nfi_decimal2.NumberDecimalDigits = 2;
      }
      else if (Program.mySettings.LanguageIdent == "pl")
      {
        GameEngine.nfi = new CultureInfo("pl-PL", false).NumberFormat;
        GameEngine.nfi.NumberDecimalDigits = 0;
        GameEngine.nfi_decimal = new CultureInfo("pl-PL", false).NumberFormat;
        GameEngine.nfi_decimal1 = new CultureInfo("pl-PL", false).NumberFormat;
        GameEngine.nfi_decimal1.NumberDecimalDigits = 1;
        GameEngine.nfi_decimal2 = new CultureInfo("pl-PL", false).NumberFormat;
        GameEngine.nfi_decimal2.NumberDecimalDigits = 2;
      }
      else if (Program.mySettings.LanguageIdent == "it")
      {
        GameEngine.nfi = new CultureInfo("it-IT", false).NumberFormat;
        GameEngine.nfi.NumberDecimalDigits = 0;
        GameEngine.nfi_decimal = new CultureInfo("it-IT", false).NumberFormat;
        GameEngine.nfi_decimal1 = new CultureInfo("it-IT", false).NumberFormat;
        GameEngine.nfi_decimal1.NumberDecimalDigits = 1;
        GameEngine.nfi_decimal2 = new CultureInfo("it-IT", false).NumberFormat;
        GameEngine.nfi_decimal2.NumberDecimalDigits = 2;
      }
      else if (Program.mySettings.LanguageIdent == "tr")
      {
        GameEngine.nfi = new CultureInfo("tr-TR", false).NumberFormat;
        GameEngine.nfi.NumberDecimalDigits = 0;
        GameEngine.nfi_decimal = new CultureInfo("tr-TR", false).NumberFormat;
        GameEngine.nfi_decimal1 = new CultureInfo("tr-TR", false).NumberFormat;
        GameEngine.nfi_decimal1.NumberDecimalDigits = 1;
        GameEngine.nfi_decimal2 = new CultureInfo("tr-TR", false).NumberFormat;
        GameEngine.nfi_decimal2.NumberDecimalDigits = 2;
      }
      else if (Program.mySettings.LanguageIdent == "pt")
      {
        GameEngine.nfi = new CultureInfo("pt-BR", false).NumberFormat;
        GameEngine.nfi.NumberDecimalDigits = 0;
        GameEngine.nfi_decimal = new CultureInfo("pt-BR", false).NumberFormat;
        GameEngine.nfi_decimal1 = new CultureInfo("pt-BR", false).NumberFormat;
        GameEngine.nfi_decimal1.NumberDecimalDigits = 1;
        GameEngine.nfi_decimal2 = new CultureInfo("pt-BR", false).NumberFormat;
        GameEngine.nfi_decimal2.NumberDecimalDigits = 2;
      }
      else
      {
        GameEngine.nfi = new CultureInfo(CultureInfo.CurrentCulture.Name, false).NumberFormat;
        GameEngine.nfi.NumberDecimalDigits = 0;
        GameEngine.nfi_decimal = new CultureInfo(CultureInfo.CurrentCulture.Name, false).NumberFormat;
        GameEngine.nfi_decimal1 = new CultureInfo(CultureInfo.CurrentCulture.Name, false).NumberFormat;
        GameEngine.nfi_decimal1.NumberDecimalDigits = 1;
        GameEngine.nfi_decimal2 = new CultureInfo(CultureInfo.CurrentCulture.Name, false).NumberFormat;
        GameEngine.nfi_decimal2.NumberDecimalDigits = 2;
      }
      NewQuests.loadCSV();
      this.maxResolution = maxRes;
      this.currentResolution = curRes;
      this.gfx = mgr;
      this.m_doReLogin = false;
      this.villageToAbandon = -1;
      if (this.firstCall)
        this.m_tickTimer = new System.Threading.Timer(new TimerCallback(this.TimerCallbackFunction), (object) null, 33, 33);
      if (this.gfx.InitControl((Control) InterfaceMgr.Instance.getDXBasePanel(), Program.mySettings.AAMode) != null)
      {
        GameEngine.displayDirectXError();
        return false;
      }
      if (this.gfx.calcedAAMode > 0)
        Program.mySettings.AAMode = this.gfx.calcedAAMode;
      this.dxFont1.Family = "Tahoma";
      this.dxFont1.Height = 18;
      this.dxFont2.Family = "Arial";
      this.dxFont2.Weight = FontDesc.Weighting.Normal;
      this.dxFont2.Height = 18;
      this.gfx.Initialize();
      this.gfx.initRenderCallback(new GraphicsMgr.RenderCallback(this.render));
      this.gfx.initFont(this.dxFont1, this.dxFont2);
      this.m_WorkerThread = new Thread(new ThreadStart(this.loadThread));
      this.m_WorkerThread.Name = "Loader";
      GFXLibrary.Instance.loadResources();
      InterfaceMgr.Instance.mapPanelCreates();
      this.m_WorkerThread.Start();
      if (this.firstCall)
        Thread.Sleep(100);
      this.worldMapTypesData = new WorldMapTypes();
      if (this.firstCall)
      {
        this.audio = new Audio();
        this.audio.initAudio();
        VillageMap.loadVillageSounds();
        Sound.createPlayLists();
      }
      RemoteServices.Instance.set_CommonData_UserCallBack(new RemoteServices.CommonData_UserCallBack(this.remoteConnectionCommonHandler));
      InterfaceMgr.Instance.initInterfaces();
      OptionsPopup.registerCallback(new OptionsPopup.ResolutionChangeCallback(this.resolutionButtonChange));
      this.world.registerWorldZoomCallback(new WorldMap.WorldZoomCallback(this.worldZoomChange));
      this.world.capZoom(0.0);
      this.world.initFW();
      InterfaceMgr.Instance.getMainTabBar().registerTabChangeCallback(new MainTabBar2.TabChangeCallback(this.mainTabChange));
      InterfaceMgr.Instance.getVillageTabBar().registerTabChangeCallback(new VillageTabBar2.TabChangeCallback(this.villageTabChange));
      InterfaceMgr.Instance.getFactionTabBar().registerTabChangeCallback(new FactionTabBar2.TabChangeCallback(this.factionTabChange));
      RemoteServices.Instance.set_GetVillageBuildingsList_UserCallBack(new RemoteServices.GetVillageBuildingsList_UserCallBack(this.getVillageBuildingListCallBack));
      this.gfx.BGColor = WorldMap.SEACOLOR;
      this.lastTabID = -1;
      InterfaceMgr.Instance.ignoreStopDraw = true;
      DXPanel.skipPaint = true;
      this.mainTabChange(0);
      DXPanel.skipPaint = false;
      InterfaceMgr.Instance.ignoreStopDraw = false;
      this.firstCall = false;
      this.lastFullTickRegisterTime = this.lastFullTickTime = DXTimer.GetCurrentMilliseconds();
      return true;
    }

    public void resumeCommonRemote()
    {
      RemoteServices.Instance.set_CommonData_UserCallBack(new RemoteServices.CommonData_UserCallBack(this.remoteConnectionCommonHandler));
    }

    public void lateStart()
    {
      InterfaceMgr.Instance.setUserName(RemoteServices.Instance.UserName);
      this.world.setCurrentZoom((float) this.World.WorldZoom);
      this.world.setScreenSize(InterfaceMgr.Instance.getDXBasePanel().Width, InterfaceMgr.Instance.getDXBasePanel().Height);
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      InterfaceMgr.Instance.getMainTabBar().changeTab(9);
      InterfaceMgr.Instance.getMainTabBar().changeTab(0);
      this.world.startGameZoom(selectedMenuVillage);
      InterfaceMgr.Instance.getTopLeftMenu().init();
      InterfaceMgr.Instance.getTopRightMenu().init();
      if (this.LocalWorldData.Alternate_Ruleset != 1)
        return;
      InterfaceMgr.Instance.showDominationWindow();
    }

    public void showConnectingPopup()
    {
    }

    public void enableConnectingPopup()
    {
    }

    public void enableConnectingPopup2()
    {
      if (this.m_loginWindow == null)
        return;
      this.m_loginWindow.selfClose();
      this.m_loginWindow.Close();
    }

    public void updateConnectingPopup()
    {
      if (this.m_loginWindow == null)
        return;
      this.m_loginWindow.update();
    }

    public bool waitForConnectingPopupToClose()
    {
      return this.m_loginWindow != null && this.m_loginWindow.Created;
    }

    public void setProfileLogin(ProfileLoginWindow loginWindow) => this.m_loginWindow = loginWindow;

    public ProfileLoginWindow getLoginWindow()
    {
      return this.m_loginWindow != null && this.m_loginWindow.Created ? this.m_loginWindow : (ProfileLoginWindow) null;
    }

    private void TimerCallbackFunction(object o)
    {
    }

    public void forceFullTick() => this.forceTriggerFullTick = true;

    public bool isSelectNewVillageVisible()
    {
      return (this.noVillagePopup != null || this.noAutoVillagePopup != null) && (this.noVillagePopup == null || this.noVillagePopup.Created && this.noVillagePopup.Visible) && (this.noAutoVillagePopup == null || this.noAutoVillagePopup.Created && this.noAutoVillagePopup.Visible);
    }

    public void run()
    {
      long num1 = this.lastFrameTime;
      long millisecondsLong = DXTimer.GetCurrentMillisecondsLong();
      if (num1 == 0L)
        num1 = millisecondsLong - 33L;
      long num2 = millisecondsLong - num1;
      if (num2 >= 33L)
      {
        this.ticked = true;
        if (num2 >= 49L)
          this.lastFrameTime = millisecondsLong;
        else
          this.lastFrameTime += 33L;
        InterfaceMgr.Instance.getDXBasePanel().AllowDraw();
        InterfaceMgr.Instance.getDXBasePanel().Invalidate();
        Program.DoEvents();
      }
      if (this.villageToAbandon >= 0)
      {
        int villageToAbandon = this.villageToAbandon;
        this.villageHasBeenDownloaded = false;
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(1);
        for (int index1 = 0; index1 < 210; ++index1)
        {
          Thread.Sleep(33);
          Program.DoEvents();
          RemoteServices.Instance.processData();
          if (this.villageHasBeenDownloaded)
          {
            this.villageToAbandon = -1;
            for (int index2 = 0; index2 < 10; ++index2)
            {
              Thread.Sleep(33);
              this.run();
              RemoteServices.Instance.processData();
            }
            break;
          }
        }
        int num3 = 0;
        VillageMap village = GameEngine.Instance.getVillage(villageToAbandon);
        if (village != null)
          num3 = village.countBuildings();
        MessageBoxButtons buts = MessageBoxButtons.YesNo;
        DialogResult dialogResult;
        if (num3 <= 0)
          dialogResult = MyMessageBox.Show(SK.Text("Abandon_Message", "You are about to Abandon this village. You will lose ownership of this village and once abandoned it can not be reversed.") + Environment.NewLine + SK.Text("BuyVillagePopup_Are_You_REALLY_Sure", "Are you REALLY Sure you want to do this and that you have selected the correct village?") + Environment.NewLine + Environment.NewLine + SK.Text("BuyVillagePopup_To_Be_Abandon", "The Village to be abandoned is : ") + Environment.NewLine + Environment.NewLine + GameEngine.Instance.World.getVillageName(villageToAbandon) + Environment.NewLine + Environment.NewLine + ".", SK.Text("MENU_Abandon_Warning", "Warning! : Abandon") + " : " + GameEngine.Instance.World.getVillageName(villageToAbandon) + "?", buts, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0);
        else
          dialogResult = MyMessageBox.Show(SK.Text("Abandon_Message", "You are about to Abandon this village. You will lose ownership of this village and once abandoned it can not be reversed.") + Environment.NewLine + SK.Text("BuyVillagePopup_Are_You_REALLY_Sure", "Are you REALLY Sure you want to do this and that you have selected the correct village?") + Environment.NewLine + Environment.NewLine + SK.Text("BuyVillagePopup_To_Be_Abandon", "The Village to be abandoned is : ") + Environment.NewLine + Environment.NewLine + GameEngine.Instance.World.getVillageName(villageToAbandon) + Environment.NewLine + Environment.NewLine + "." + SK.Text("BuyVillagePopup_Num_Buildings", "The number of buildings in this village : ") + num3.ToString() + Environment.NewLine + Environment.NewLine + ".", SK.Text("MENU_Abandon_Warning", "Warning! : Abandon") + " : " + GameEngine.Instance.World.getVillageName(villageToAbandon) + "?", buts, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0);
        if (dialogResult == DialogResult.Yes)
        {
          RemoteServices.Instance.set_VillageRename_UserCallBack(new RemoteServices.VillageRename_UserCallBack(MainMenuBar2.VillageRenameCallback));
          RemoteServices.Instance.VillageAbandon(villageToAbandon);
        }
        this.villageToAbandon = -1;
      }
      if (this.ticked)
      {
        if (this.lastSoundClear == DateTime.MinValue)
          this.lastSoundClear = DateTime.Now;
        else if ((DateTime.Now - this.lastSoundClear).TotalMinutes > 5.0)
        {
          this.lastSoundClear = DateTime.Now;
          this.AudioEngine.unloadUnplayingSounds();
        }
        Program.steam_run();
        Program.arc_run();
        this.audio.update();
        Form activeForm = Form.ActiveForm;
        if (activeForm != InterfaceMgr.Instance.ParentForm && (activeForm != InterfaceMgr.Instance.ChatForm || InterfaceMgr.Instance.ChatForm == null))
        {
          GameEngine.scrollLeft = false;
          GameEngine.scrollUp = false;
          GameEngine.scrollRight = false;
          GameEngine.scrollDown = false;
          GameEngine.Instance.GFX.keyControlled = false;
          GameEngine.shiftPressed = false;
          GameEngine.tabPressed = false;
        }
        this.gfx.RenderList.clearLayers();
        this.ticked = false;
        ++this.tickCount;
        this.cardsManager.postcardPlayUpdate();
        if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE)
        {
          if (this.village != null)
            this.village.Update(true);
          if (this.castle != null)
            this.castle.Update(false);
          InterfaceMgr.Instance.runVillageInterface();
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
        {
          if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT)
          {
            if (this.village != null)
              this.village.Update(false);
            if (this.castle != null)
              this.castle.Update(true);
            InterfaceMgr.Instance.runCastleInterface();
          }
          else if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
          {
            if (this.castle_AttackerSetup != null)
              this.castle_AttackerSetup.Update(true);
          }
          else if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_BATTLE)
          {
            if (this.castle_Battle != null)
              this.castle_Battle.BattleUpdateManager(true);
          }
          else if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW && this.castle_Preview != null)
            this.castle_Preview.Update(true);
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD)
        {
          bool special = false;
          this.world.Update();
          if (this.world.ZoomChange != 0.0)
          {
            this.world.changeZoom((float) this.world.ZoomChange);
            if (this.world.ZoomChange <= 0.0)
              this.world.centreMap(false);
          }
          if (this.tickCount % 10 == 0)
          {
            InterfaceMgr.Instance.updateTraderInfo();
            InterfaceMgr.Instance.updatePersonInfo();
            this.World.updateLocalVillagesFromFactions();
            InterfaceMgr.Instance.ensureInfoTabCleared();
            this.World.monitorAIInvasionActivity();
          }
          double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
          double callHomeRate = this.LocalWorldData.callHomeRate;
          int gameActivityMode = InterfaceMgr.Instance.getGameActivityMode();
          switch (gameActivityMode)
          {
            case 1:
              callHomeRate *= 2.0;
              break;
            case 2:
              callHomeRate *= 4.0;
              break;
            case 3:
              callHomeRate *= 6.0;
              break;
            case 4:
              callHomeRate *= 14.0;
              break;
            case 5:
              callHomeRate *= 20.0;
              break;
          }
          this.clockMode = gameActivityMode;
          this.clockFrame = (int) ((currentMilliseconds - this.lastFullTickTime) * 64.0 / (callHomeRate * 1000.0));
          if (this.clockFrame >= 63)
            this.clockFrame = 63;
          if (currentMilliseconds - this.lastFullTickTime > callHomeRate * 1000.0 || this.forceTriggerFullTick)
          {
            this.clockFrame = 0;
            this.forceTriggerFullTick = false;
            if (currentMilliseconds - this.lastFullTickRegisterTime > 240000.0)
            {
              this.lastFullTickTime = currentMilliseconds;
              this.lastFullTickRegisterTime = currentMilliseconds;
              this.World.doFullTick(true, gameActivityMode);
            }
            else if (!InterfaceMgr.Instance.isGameMinimised())
            {
              this.lastFullTickTime = currentMilliseconds;
              this.World.doFullTick(false, gameActivityMode);
            }
            special = true;
          }
          InterfaceMgr.Instance.worldTabUpdate(special);
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_REPORTS)
        {
          if (this.tickCount % 10 == 0)
            InterfaceMgr.Instance.updateVillageReports();
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_LEADERBOARD)
        {
          if (this.tickCount % 10 == 0)
            InterfaceMgr.Instance.updateVillageReports();
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CONTESTS_LEADERBOARD)
        {
          if (this.tickCount % 10 == 0)
            InterfaceMgr.Instance.updateVillageReports();
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_USER_INFO)
        {
          if (this.tickCount % 10 == 0)
            InterfaceMgr.Instance.updateVillageReports();
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_ALL_VILLAGES)
        {
          if (this.tickCount % 10 == 0)
            InterfaceMgr.Instance.updateVillageReports();
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_RANKINGS)
          InterfaceMgr.Instance.updateVillageReports();
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_FACTIONS)
        {
          if (this.tickCount % 10 == 0)
            InterfaceMgr.Instance.updateVillageReports();
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_QUESTS)
          InterfaceMgr.Instance.updateVillageReports();
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_ARMIES)
        {
          if (this.tickCount % 10 == 0)
          {
            double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
            double callHomeRate = this.LocalWorldData.callHomeRate;
            int gameActivityMode = InterfaceMgr.Instance.getGameActivityMode();
            switch (gameActivityMode)
            {
              case 1:
                callHomeRate *= 2.0;
                break;
              case 2:
                callHomeRate *= 4.0;
                break;
              case 3:
                callHomeRate *= 6.0;
                break;
              case 4:
                callHomeRate *= 14.0;
                break;
              case 5:
                callHomeRate *= 20.0;
                break;
            }
            if (currentMilliseconds - this.lastFullTickTime > callHomeRate * 1000.0)
            {
              this.lastFullTickTime = currentMilliseconds;
              this.World.doFullTick(false, gameActivityMode);
            }
            InterfaceMgr.Instance.updateVillageReports();
          }
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_RESEARCH)
          InterfaceMgr.Instance.updateResearch(this.tickCount % 10 == 0);
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_AVATAR_EDITOR)
          InterfaceMgr.Instance.updateVillageReports();
        if (this.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_QUESTS && this.tickCount % 60 == 35)
          NewQuestsPanel.handleClientSideQuestReporting(true);
        if (this.forceTriggerFullTick && this.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_WORLD)
        {
          double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
          this.forceTriggerFullTick = false;
          this.lastFullTickTime = currentMilliseconds;
          this.World.doFullTick(false, 3);
        }
        if (this.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_WORLD && this.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_ARMIES && !InterfaceMgr.Instance.isGameMinimised())
        {
          double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
          double callHomeRate = this.LocalWorldData.callHomeRate;
          switch (InterfaceMgr.Instance.getGameActivityMode())
          {
            case 0:
              callHomeRate *= 3.0;
              break;
            case 1:
              callHomeRate *= 4.0;
              break;
            case 2:
              callHomeRate *= 7.0;
              break;
            case 3:
              callHomeRate *= 10.0;
              break;
            case 4:
              callHomeRate *= 19.0;
              break;
            case 5:
              callHomeRate *= 25.0;
              break;
          }
          if (currentMilliseconds - this.lastFullTickTime > callHomeRate * 1000.0)
          {
            this.lastFullTickTime = currentMilliseconds;
            this.World.getArmiesIfNewAttacks();
          }
        }
        if (this.tickCount % 10 == 0)
        {
          if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
          {
            int currentGold = (int) this.world.getCurrentGold();
            if (this.Castle != null && !this.World.isCapital(this.Castle.VillageID))
            {
              VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
              this.Castle.adjustLevels(ref levels, ref currentGold);
              InterfaceMgr.Instance.setGold((double) currentGold);
            }
          }
          else
            InterfaceMgr.Instance.setGold(this.world.getCurrentGold());
          InterfaceMgr.Instance.setHonour(this.world.getCurrentHonour(), this.world.getRank());
          InterfaceMgr.Instance.setFaithPoints(this.world.getCurrentFaithPoints());
          InterfaceMgr.Instance.setPoints(this.world.getCurrentPoints());
          InterfaceMgr.Instance.setServerTime(VillageMap.getCurrentServerTime(), this.World.getGameDay());
          if (RemoteServices.Instance.queueEmpty())
            InterfaceMgr.Instance.setConnectionLight(false);
          else
            InterfaceMgr.Instance.setConnectionLight(true);
          long highestAttackingArmy = -1;
          this.incomingAttacks = this.World.countIncomingAttacks(ref highestAttackingArmy);
          if (highestAttackingArmy > this.lastHighestArmyIDSeen)
          {
            this.NewArmiesSeen = true;
            this.lastHighestArmyIDSeen = highestAttackingArmy;
          }
          InterfaceMgr.Instance.getMainTabBar().incomingAttacks(this.incomingAttacks, highestAttackingArmy);
          InterfaceMgr.Instance.getMainTabBar().updateResearchTime(this.World.UserResearchData);
          foreach (VillageMap village in this.villages)
            village?.makeTroopsUpdate();
          this.World.updateArmyRetrievalData();
          this.monitorDownTime();
          Sound.monitorMusic();
        }
        InterfaceMgr.Instance.mailUpdate();
        InterfaceMgr.Instance.chatUpdate();
        InterfaceMgr.Instance.getTopLeftMenu().update();
        InterfaceMgr.Instance.getMainTabBar().update();
        InterfaceMgr.Instance.updatePopups();
        this.World.updateResearch(false);
        int num4 = Program.mySettings.MapScreenNoUpdate ? 1 : 0;
        if (!this.World.WorldEnded)
        {
          this.World.updateArmies();
          this.World.updateTraders();
          this.World.updatePeople();
        }
        else
          this.World.updateFW();
        PizzazzPopupWindow.updatePizzazz();
        InterfaceMgr.Instance.runTutorialWindow();
        MouseInputState inputState = new MouseInputState();
        inputState.getInput();
        this.manageInput(inputState);
        if (this.tickCount % 1800 == 0)
        {
          GC.Collect();
          GC.WaitForPendingFinalizers();
        }
        if (this.tickCount % 120 == 0)
          this.World.runClientAchievementTests();
        if (this.worldsEndPopup == null && this.noVillagePopup == null && this.noAutoVillagePopup == null && this.lostVillagePopup == null)
        {
          if (this.World.WorldEnded && !this.World.WorldEnded_message)
          {
            this.World.WorldEnded_message = true;
            GameEngine.Instance.openWorldsEnd();
            RemoteServices.Instance.Show2ndAgeMessage = false;
            RemoteServices.Instance.Show3rdAgeMessage = false;
            RemoteServices.Instance.Show4thAgeMessage = false;
            RemoteServices.Instance.Show5thAgeMessage = false;
            RemoteServices.Instance.Show6thAgeMessage = false;
            RemoteServices.Instance.Show7thAgeMessage = false;
          }
          else if (RemoteServices.Instance.Show7thAgeMessage)
          {
            GameEngine.Instance.openLostVillage(7);
            RemoteServices.Instance.Show2ndAgeMessage = false;
            RemoteServices.Instance.Show3rdAgeMessage = false;
            RemoteServices.Instance.Show4thAgeMessage = false;
            RemoteServices.Instance.Show5thAgeMessage = false;
            RemoteServices.Instance.Show6thAgeMessage = false;
          }
          else if (RemoteServices.Instance.Show6thAgeMessage)
          {
            GameEngine.Instance.openLostVillage(6);
            RemoteServices.Instance.Show2ndAgeMessage = false;
            RemoteServices.Instance.Show3rdAgeMessage = false;
            RemoteServices.Instance.Show4thAgeMessage = false;
            RemoteServices.Instance.Show5thAgeMessage = false;
          }
          else if (RemoteServices.Instance.Show5thAgeMessage)
          {
            GameEngine.Instance.openLostVillage(5);
            RemoteServices.Instance.Show2ndAgeMessage = false;
            RemoteServices.Instance.Show3rdAgeMessage = false;
            RemoteServices.Instance.Show4thAgeMessage = false;
          }
          else if (RemoteServices.Instance.Show4thAgeMessage)
          {
            GameEngine.Instance.openLostVillage(4);
            RemoteServices.Instance.Show2ndAgeMessage = false;
            RemoteServices.Instance.Show3rdAgeMessage = false;
          }
          else if (RemoteServices.Instance.Show3rdAgeMessage)
          {
            GameEngine.Instance.openLostVillage(3);
            RemoteServices.Instance.Show2ndAgeMessage = false;
          }
          else if (RemoteServices.Instance.Show2ndAgeMessage)
            GameEngine.Instance.openLostVillage(2);
          else if (GameEngine.Instance.World.showGloryResults)
          {
            this.houseManager.UpdateGloryPoints(new HouseManager.HouseInfoUpdatedCallback(InterfaceMgr.Instance.openGloryVictoryPopup), true);
            GameEngine.Instance.World.showGloryResults = false;
          }
          if (!this.World.isRetrievingUserVillages() && !LoggingOutPopup.loggingOut && this.World.numVillagesOwned() == 0 && !this.World.WorldEnded)
            this.World.updateLastAttackerInfo();
        }
        else if (this.noVillagePopup != null)
          this.noVillagePopup.update();
        else if (this.lostVillagePopup != null)
          this.lostVillagePopup.update();
        else if (this.worldsEndPopup != null)
          this.worldsEndPopup.update();
        else if (this.noAutoVillagePopup != null)
          this.noAutoVillagePopup.update();
        TutorialWindow.runTutorial();
        this.debugPopupRun();
        this.loginHistoryRun();
        if (this.pendingUserVillageZoom)
        {
          int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
          if (selectedMenuVillage >= 0)
          {
            this.pendingUserVillageZoom = false;
            VillageData villageData = this.world.getVillageData(selectedMenuVillage);
            if (villageData != null)
              this.world.startMultiStageZoom(10000.0, (double) villageData.x, (double) villageData.y);
          }
        }
      }
      if (Program.ShowSeasonalFX && SnowSystem.getInstance().snowTexture != null)
        SnowSystem.getInstance().update(this.gfx);
      if (!this.finaliseResize)
        return;
      try
      {
        this.finaliseResize = false;
        if (InterfaceMgr.Instance.ParentForm == null)
          return;
        ((MainWindow) InterfaceMgr.Instance.ParentForm).finaliseResize();
      }
      catch (Exception ex)
      {
      }
    }

    public int IncomingArmies => this.incomingAttacks;

    public void closeNoVillagePopup(bool pendingVillage)
    {
      if (this.noVillagePopup != null)
      {
        InterfaceMgr.Instance.ParentForm.Enabled = true;
        this.noVillagePopup.closePopup();
        this.noVillagePopup.Close();
        this.noVillagePopup = (NewSelectVillageAreaWindow) null;
        InterfaceMgr.Instance.closeGreyOut();
      }
      if (this.lostVillagePopup != null)
      {
        if (this.lostVillagePopup.isCardsPopup())
        {
          this.lostVillagePopup.closePopup();
          this.lostVillagePopup.Close();
          this.lostVillagePopup = (LostVillageWindow) null;
        }
        else
        {
          InterfaceMgr.Instance.ParentForm.Enabled = true;
          this.lostVillagePopup.closePopup();
          this.lostVillagePopup.Close();
          this.lostVillagePopup = (LostVillageWindow) null;
          InterfaceMgr.Instance.closeGreyOut();
        }
      }
      if (this.worldsEndPopup != null)
      {
        InterfaceMgr.Instance.ParentForm.Enabled = true;
        this.worldsEndPopup.closePopup();
        this.worldsEndPopup.Close();
        this.worldsEndPopup = (WorldsEndWindow) null;
        InterfaceMgr.Instance.closeGreyOut();
      }
      if (this.noAutoVillagePopup != null)
      {
        InterfaceMgr.Instance.ParentForm.Enabled = true;
        this.noAutoVillagePopup.closePopup();
        this.noAutoVillagePopup.Close();
        this.noAutoVillagePopup = (NewAutoSelectVillageWindow) null;
        InterfaceMgr.Instance.closeGreyOut();
      }
      if (!pendingVillage)
        return;
      this.pendingUserVillageZoom = true;
    }

    public void openAdvancedSelectVillage()
    {
      InterfaceMgr.Instance.openGreyOutWindow(false);
      InterfaceMgr.Instance.ParentForm.Enabled = false;
      this.noVillagePopup = new NewSelectVillageAreaWindow();
      this.noVillagePopup.init(this.tryingToJoinCounty);
      this.noVillagePopup.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
    }

    public void openLostVillage(int age)
    {
      if (InterfaceMgr.Instance.ParentForm.WindowState == FormWindowState.Minimized)
      {
        InterfaceMgr.Instance.ParentForm.WindowState = FormWindowState.Normal;
        for (int index = 0; index < 10; ++index)
        {
          Thread.Sleep(100);
          Application.DoEvents();
        }
      }
      InterfaceMgr.Instance.openGreyOutWindow(false);
      InterfaceMgr.Instance.ParentForm.Enabled = false;
      this.lostVillagePopup = new LostVillageWindow();
      this.lostVillagePopup.init(age, -1);
      this.lostVillagePopup.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
    }

    public void openWorldsEnd()
    {
      if (InterfaceMgr.Instance.ParentForm.WindowState == FormWindowState.Minimized)
      {
        InterfaceMgr.Instance.ParentForm.WindowState = FormWindowState.Normal;
        for (int index = 0; index < 10; ++index)
        {
          Thread.Sleep(100);
          Application.DoEvents();
        }
      }
      InterfaceMgr.Instance.openGreyOutWindow(false);
      InterfaceMgr.Instance.ParentForm.Enabled = false;
      this.worldsEndPopup = new WorldsEndWindow();
      this.worldsEndPopup.init();
      this.worldsEndPopup.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
    }

    public void openSimpleSelectVillage()
    {
      InterfaceMgr.Instance.openGreyOutWindow(false);
      InterfaceMgr.Instance.ParentForm.Enabled = false;
      this.noAutoVillagePopup = new NewAutoSelectVillageWindow();
      this.noAutoVillagePopup.init(this.tryingToJoinCounty);
      this.noAutoVillagePopup.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
    }

    public void openSuperPackInfo(int mode)
    {
      this.lostVillagePopup = new LostVillageWindow();
      this.lostVillagePopup.init(0, mode);
      this.lostVillagePopup.Show((IWin32Window) InterfaceMgr.Instance.getCardWindow());
    }

    public void OnPaintCallback()
    {
      bool flag = false;
      if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD)
        flag = true;
      if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE && InterfaceMgr.Instance.updateVillageReports())
        flag = true;
      if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
        flag = true;
      if (!flag || !InterfaceMgr.Instance.ParentForm.Created)
        return;
      bool renderContent = true;
      if (Program.steamInstall)
        renderContent = !Program.steamOverlayActive;
      if (this.gfx.render(renderContent))
        return;
      this.newResolution = this.currentResolution;
      if (InterfaceMgr.Instance.ParentForm == null)
        return;
      int num = (int) MyMessageBox.Show(SK.Text("GameEngine_Generic_Error", "An error has occurred and Stronghold Kingdoms will now close."), "DirectX");
      InterfaceMgr.Instance.ParentForm.Close();
    }

    public void setServerDownTime(DateTime downtime)
    {
      if (!(downtime != this.serverDowntime))
        return;
      if (downtime == DateTime.MinValue)
      {
        this.clearServerDowntime();
      }
      else
      {
        this.clearServerDowntime();
        this.serverDowntime = downtime;
        TimeSpan timeSpan = this.serverDowntime - VillageMap.getCurrentServerTime();
        if (timeSpan.TotalMinutes < 780.0)
          this.warning24H = true;
        if (timeSpan.TotalMinutes < 300.0)
          this.warning12H = true;
        if (timeSpan.TotalMinutes < 120.0)
          this.warning4H = true;
        if (timeSpan.TotalMinutes < 35.0)
          this.warning60 = true;
        if (timeSpan.TotalMinutes < 19.0)
          this.warning30 = true;
        if (timeSpan.TotalMinutes >= 8.0)
          return;
        this.warning15 = true;
      }
    }

    private void monitorDownTime()
    {
      if (this.serverDowntime != DateTime.MinValue)
      {
        TimeSpan timeSpan = this.serverDowntime - VillageMap.getCurrentServerTime();
        if (timeSpan.TotalMinutes < 1440.5 && !this.warning24H)
        {
          this.warning24H = true;
          this.showDowntimeWarning(timeSpan.TotalMinutes);
          return;
        }
        if (timeSpan.TotalMinutes < 720.5 && !this.warning12H)
        {
          this.warning12H = true;
          this.showDowntimeWarning(timeSpan.TotalMinutes);
          return;
        }
        if (timeSpan.TotalMinutes < 240.5 && !this.warning4H)
        {
          this.warning4H = true;
          this.showDowntimeWarning(timeSpan.TotalMinutes);
          return;
        }
        if (timeSpan.TotalMinutes < 60.5 && !this.warning60)
        {
          this.warning60 = true;
          this.showDowntimeWarning(timeSpan.TotalMinutes);
          return;
        }
        if (timeSpan.TotalMinutes < 30.5 && !this.warning30)
        {
          this.warning30 = true;
          this.showDowntimeWarning(timeSpan.TotalMinutes);
          return;
        }
        if (timeSpan.TotalMinutes < 15.5 && !this.warning15)
        {
          this.warning15 = true;
          this.showDowntimeWarning(timeSpan.TotalMinutes);
          return;
        }
        if (timeSpan.TotalMinutes < 5.5 && !this.warning5)
        {
          this.warning5 = true;
          this.showDowntimeWarning(timeSpan.TotalMinutes);
          return;
        }
        if (timeSpan.TotalMinutes < 0.0 && !this.serverOffline)
        {
          this.serverOffline = true;
          this.clearDowntimePopup();
          this.sessionExpired(3);
          return;
        }
      }
      int alternateRuleset = this.LocalWorldData.Alternate_Ruleset;
    }

    public TimeSpan getDominationTimeLeft()
    {
      DateTime currentServerTime = VillageMap.getCurrentServerTime();
      return new TimeSpan(92, 0, 0, 0) - (new TimeSpan(GameEngine.Instance.World.getGameDay(), currentServerTime.Hour, currentServerTime.Minute, currentServerTime.Second) - new TimeSpan(14, 0, 0));
    }

    private void showDowntimeWarning(double minutes)
    {
      if (minutes < 0.0)
        return;
      this.clearDowntimePopup();
      this.m_downtimePopup = new ServerDowntimePopup();
      this.m_downtimePopup.show((int) minutes);
    }

    public void clearDowntimePopup()
    {
      if (this.m_downtimePopup == null)
        return;
      if (this.m_downtimePopup.Created && this.m_downtimePopup.Visible)
        this.m_downtimePopup.Close();
      this.m_downtimePopup = (ServerDowntimePopup) null;
    }

    public void clearServerDowntime()
    {
      this.serverDowntime = DateTime.MinValue;
      this.warning5 = false;
      this.warning15 = false;
      this.warning30 = false;
      this.warning60 = false;
      this.serverOffline = false;
    }

    public void render()
    {
      this.gfx.drawOverLayTexture = InterfaceMgr.Instance.allowDrawCircles();
      InterfaceMgr.Instance.updateDXCardBar();
      if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD)
      {
        this.world.drawVillageTree(this.gfx);
        this.gfx.drawPlaybackTexture = InterfaceMgr.Instance.playbackEnabled;
        if (this.gfx.drawPlaybackTexture)
          InterfaceMgr.Instance.updateDXPlaybackBar();
      }
      else
        this.gfx.drawPlaybackTexture = false;
      this.gfx.RenderList.render(this.gfx);
      if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
      {
        if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP && this.castle_AttackerSetup != null)
        {
          this.castle_AttackerSetup.drawCatapultLines();
          this.castle_AttackerSetup.drawLasso();
        }
        if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT && this.castle != null)
          this.castle.drawLasso();
      }
      if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE && this.village != null)
        this.village.drawProductionArrow();
      if (!Program.ShowSeasonalFX || SnowSystem.getInstance().snowTexture == null)
        return;
      this.gfx.beginSprites();
      SnowSystem.getInstance().render(this.gfx);
      this.gfx.endSprites();
    }

    public void manageInput(MouseInputState inputState)
    {
      InterfaceMgr.Instance.runTooltips();
      if (!inputState.leftdown && (GameEngine.scrollLeft || GameEngine.scrollRight || GameEngine.scrollUp || GameEngine.scrollDown))
      {
        if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE)
        {
          if (GameEngine.scrollLeft)
            this.village.Camera.Drag(new Point(10, 0));
          if (GameEngine.scrollRight)
            this.village.Camera.Drag(new Point(-10, 0));
          if (GameEngine.scrollUp)
            this.village.Camera.Drag(new Point(0, 10));
          if (GameEngine.scrollDown)
            this.village.Camera.Drag(new Point(0, -10));
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD)
        {
          if (GameEngine.scrollLeft)
            this.world.moveMap(0.0 - 10.0 / this.world.WorldScale, 0.0);
          if (GameEngine.scrollRight)
            this.world.moveMap(10.0 / this.world.WorldScale, 0.0);
          if (GameEngine.scrollUp)
            this.world.moveMap(0.0, 0.0 - 10.0 / this.world.WorldScale);
          if (GameEngine.scrollDown)
            this.world.moveMap(0.0, 10.0 / this.world.WorldScale);
        }
        else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
        {
          CastleMap castleMap = this.castle;
          if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
            castleMap = this.castle_AttackerSetup;
          else if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_BATTLE)
            castleMap = this.castle_Battle;
          else if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW)
            castleMap = this.castle_Preview;
          if (castleMap != null)
          {
            if (GameEngine.scrollLeft)
              castleMap.moveMap(10, 0);
            if (GameEngine.scrollRight)
              castleMap.moveMap(-10, 0);
            if (GameEngine.scrollUp)
              castleMap.moveMap(0, 10);
            if (GameEngine.scrollDown)
              castleMap.moveMap(0, -10);
          }
        }
      }
      if (!this.WindowActive)
        return;
      if (GameEngine.f11Pressed)
      {
        GameEngine.f11Pressed = false;
        if (InterfaceMgr.Instance.ParentForm == null || !InterfaceMgr.Instance.ParentForm.Visible)
          return;
        if (InterfaceMgr.Instance.ParentForm.FormBorderStyle == FormBorderStyle.Sizable)
          InterfaceMgr.Instance.ParentForm.FormBorderStyle = FormBorderStyle.None;
        else if (InterfaceMgr.Instance.ParentForm.WindowState == FormWindowState.Maximized)
        {
          InterfaceMgr.Instance.ParentForm.Visible = false;
          InterfaceMgr.Instance.ParentForm.WindowState = FormWindowState.Normal;
          Program.DoEvents();
          Thread.Sleep(100);
          InterfaceMgr.Instance.ParentForm.FormBorderStyle = FormBorderStyle.Sizable;
          Program.DoEvents();
          InterfaceMgr.Instance.ParentForm.WindowState = FormWindowState.Maximized;
          InterfaceMgr.Instance.ParentForm.Visible = true;
          Program.DoEvents();
          InterfaceMgr.Instance.ParentForm.Invalidate();
        }
        else
          InterfaceMgr.Instance.ParentForm.FormBorderStyle = FormBorderStyle.Sizable;
      }
      else
      {
        if (InterfaceMgr.Instance.getDXBasePanel().Visible)
        {
          if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE)
            new VillageInputHandler(this.village).handleInput(inputState);
          else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD)
            new WorldMapInputHandler(this.world).handleInput(inputState);
          else if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
          {
            CastleMap castlemap = this.castle;
            if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
              castlemap = this.castle_AttackerSetup;
            else if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_BATTLE)
              castlemap = this.castle_Battle;
            else if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW)
              castlemap = this.castle_Preview;
            new CastleMouseInputHandler(castlemap, this.gameDisplayModeSubMode).handleInput(inputState);
          }
        }
        this.gfx.clearInput();
      }
    }

    public void resolutionButtonChange(int newRes)
    {
      if (newRes > this.maxResolution)
        return;
      this.newResolution = newRes;
    }

    public bool resolutionChange() => this.newResolution != -1;

    public void worldZoomChange(double worldZoom, bool redraw)
    {
      this.world.WorldZoom = worldZoom;
      if (!redraw)
        return;
      this.run();
    }

    public int MovedFromVillageID
    {
      get => this.movedFromVillageID;
      set
      {
        this.movedFromVillageID = value;
        if (value == -1 || this.world.isCapital(value))
          return;
        this.movedFromVillageIDNonCapital = value;
      }
    }

    public void ResetVillageIfChangedFromCapital()
    {
      if (this.MovedFromVillageID < 0)
        return;
      if (InterfaceMgr.Instance.getMainTabBar().getCurrentTab() == 0)
        this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
      InterfaceMgr.Instance.selectUserVillage(this.MovedFromVillageID, false);
    }

    public void forceResetVillageIfChangedFromCapital()
    {
      if (!this.World.isCapital(InterfaceMgr.Instance.getSelectedMenuVillage()))
        return;
      if (this.MovedFromVillageID >= 0 && !this.World.isCapital(this.MovedFromVillageID))
        InterfaceMgr.Instance.selectUserVillage(this.MovedFromVillageID, false);
      else if (this.movedFromVillageIDNonCapital >= 0 && !this.World.isCapital(this.movedFromVillageIDNonCapital))
      {
        InterfaceMgr.Instance.selectUserVillage(this.movedFromVillageIDNonCapital, false);
      }
      else
      {
        int movedFromVillageId = this.MovedFromVillageID;
        List<int> userVillageIdList = this.World.getUserVillageIDList();
        if (userVillageIdList.Count > 0)
          InterfaceMgr.Instance.selectUserVillage(userVillageIdList[0], false);
        this.MovedFromVillageID = movedFromVillageId;
      }
    }

    public void externalMainTabChange(int tabID) => this.lastTabID = tabID;

    public void mainTabChange(int tabID)
    {
      if (this.lastTabID == tabID && tabID != 9)
        return;
      if (this.lastTabID == 0 && tabID != 0 && this.World.playbackActive())
        this.World.stopPlayback();
      if (tabID == 1 && InterfaceMgr.Instance.getSelectedMenuVillage() < 0)
      {
        InterfaceMgr.Instance.StopDrawing();
        InterfaceMgr.Instance.getMainTabBar().changeTab(this.lastTabID);
        InterfaceMgr.Instance.StartDrawing();
      }
      else
      {
        InterfaceMgr.Instance.StopDrawing();
        this.previousTabID = this.lastTabID;
        this.lastTabID = tabID;
        InterfaceMgr.Instance.clearControls();
        this.gameDisplayModeSubMode = GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT;
        StatTrackingClient.Instance().ActivateTrigger(1, (object) tabID);
        if (tabID != 1)
          this.lastLoadedVillage = -1;
        switch (tabID)
        {
          case 0:
            this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
            this.gfx.BGColor = WorldMap.SEACOLOR;
            InterfaceMgr.Instance.initWorldTab();
            InterfaceMgr.Instance.selectCurrentUserVillage();
            break;
          case 1:
            this.lastVillageTabID = -1;
            InterfaceMgr.Instance.showVillageTabBar();
            InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(0);
            break;
          case 2:
            int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
            if (InterfaceMgr.Instance.isSelectedVillageACapital())
            {
              this.lastVillageTabID = -1;
              InterfaceMgr.Instance.showVillageTabBar();
              InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(5);
              this.MovedFromVillageID = selectedMenuVillage;
              break;
            }
            int parishCapital = this.World.getParishCapital(this.World.getParishFromVillageID(selectedMenuVillage));
            if (parishCapital >= 0)
            {
              this.lastVillageTabID = -1;
              InterfaceMgr.Instance.showVillageTabBar();
              InterfaceMgr.Instance.selectUserVillage(parishCapital, false);
              InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(5);
              this.MovedFromVillageID = selectedMenuVillage;
              break;
            }
            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            break;
          case 3:
            this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_RESEARCH;
            InterfaceMgr.Instance.addMainWindow(this.m_firstDrawResearch, true);
            this.m_firstDrawResearch = false;
            InterfaceMgr.Instance.initResearchTab();
            break;
          case 4:
            this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_RANKINGS;
            InterfaceMgr.Instance.addMainWindow(this.m_firstDrawRank, true);
            this.m_firstDrawRank = false;
            InterfaceMgr.Instance.initRankingsTab();
            break;
          case 5:
            this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_QUESTS;
            InterfaceMgr.Instance.addMainWindow(this.m_firstDrawQuest, true);
            this.m_firstDrawQuest = false;
            InterfaceMgr.Instance.initQuestsTab();
            break;
          case 6:
            this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_ARMIES;
            InterfaceMgr.Instance.addMainWindow(this.m_firstDrawArmy, true);
            this.m_firstDrawArmy = false;
            InterfaceMgr.Instance.initAllArmiesTab();
            break;
          case 7:
            this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_REPORTS;
            InterfaceMgr.Instance.addMainWindow(this.m_firstDrawReports, false);
            this.m_firstDrawReports = false;
            InterfaceMgr.Instance.initReportTab();
            break;
          case 8:
            this.lastVillageTabID = -1;
            this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_FACTIONS;
            InterfaceMgr.Instance.showFactionTabBar();
            if (this.nextFactionPage < 0 || this.nextFactionPage == 65)
            {
              InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(0);
              break;
            }
            if (this.nextFactionPage == 52 || this.nextFactionPage == 51)
            {
              InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(2);
              break;
            }
            InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(1);
            break;
          case 9:
            int dummyMode = MainTabBar2.DummyMode;
            MainTabBar2.LastDummyMode = dummyMode;
            MainTabBar2.DummyMode = 0;
            switch (dummyMode)
            {
              case -14:
              case 14:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
                this.gfx.BGColor = WorldMap.SEACOLOR;
                InterfaceMgr.Instance.initWorldTab_monkSelect();
                break;
              case -13:
              case 13:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
                this.gfx.BGColor = WorldMap.SEACOLOR;
                InterfaceMgr.Instance.initWorldTab_vassalSelect();
                break;
              case -11:
              case 11:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
                this.gfx.BGColor = WorldMap.SEACOLOR;
                InterfaceMgr.Instance.initWorldTab_courtierTargetSelect();
                break;
              case -7:
              case 7:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
                this.gfx.BGColor = WorldMap.SEACOLOR;
                InterfaceMgr.Instance.initWorldTab_scoutTargetSelect();
                break;
              case -5:
              case 5:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
                this.gfx.BGColor = WorldMap.SEACOLOR;
                InterfaceMgr.Instance.initWorldTab_attackTargetSelect();
                break;
              case -4:
              case 4:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
                this.gfx.BGColor = WorldMap.SEACOLOR;
                InterfaceMgr.Instance.initWorldTab_stockExchangeSelect();
                break;
              case -3:
              case 3:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
                this.gfx.BGColor = WorldMap.SEACOLOR;
                InterfaceMgr.Instance.initWorldTab_tradingVillageSelect();
                break;
              case 1:
              case 2:
              case 6:
                break;
              case 10:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_AVATAR_EDITOR;
                InterfaceMgr.Instance.getTopRightMenu().showVillageTab(false);
                InterfaceMgr.Instance.addMainWindow(this.m_firstDrawDummy, true);
                this.m_firstDrawDummy = false;
                InterfaceMgr.Instance.setVillageTabSubMode(10);
                break;
              case 21:
                if (InterfaceMgr.Instance.isMailDocked())
                {
                  this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_MAIL;
                  InterfaceMgr.Instance.addMainWindow(this.m_firstDrawMail, true);
                  this.m_firstDrawMail = false;
                }
                InterfaceMgr.Instance.initMailSubTab(0);
                break;
              case 22:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_LEADERBOARD;
                InterfaceMgr.Instance.addMainWindow(this.m_firstDrawLeaderboard, true);
                this.m_firstDrawLeaderboard = false;
                InterfaceMgr.Instance.initReportsLeaderboard();
                break;
              case 30:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_CONTESTS_LEADERBOARD;
                InterfaceMgr.Instance.addMainWindow(this.m_firstDrawLeaderboard, true);
                this.m_firstDrawLeaderboard = false;
                InterfaceMgr.Instance.initContestsLeaderboard();
                break;
              case 31:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_CONTESTS_HISTORY;
                InterfaceMgr.Instance.addMainWindow(this.m_firstDrawLeaderboard, true);
                this.m_firstDrawLeaderboard = false;
                InterfaceMgr.Instance.initContestHistory();
                break;
              case 50:
                this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_VILLAGE;
                this.gfx.BGColor = ARGBColors.Black;
                InterfaceMgr.Instance.initVillageTabView();
                break;
              case 60:
                InterfaceMgr.Instance.setVillageTabSubMode(60);
                break;
              case 100:
                this.gfx.BGColor = ARGBColors.Black;
                InterfaceMgr.Instance.showAllVillagesScreen();
                break;
              default:
                InterfaceMgr.Instance.addMainWindow(this.m_firstDrawDummy, true);
                this.m_firstDrawDummy = false;
                break;
            }
            break;
        }
        InterfaceMgr.Instance.StartDrawing();
      }
    }

    public void forceVillageTabUpdate() => this.lastVillageTabID = -1;

    public void SkipVillageTab() => this.skipVillageTab = true;

    public void villageTabChange(int tabID)
    {
      if (this.lastVillageTabID == tabID && tabID != 9 && this.gameDisplayModeSubMode != GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW)
        return;
      InterfaceMgr.Instance.StopDrawing();
      InterfaceMgr.Instance.getVillageTabBar().updateShownTabs();
      this.lastVillageTabID = tabID;
      if (tabID <= 1)
        InterfaceMgr.Instance.clearControls();
      else
        InterfaceMgr.Instance.clearControls(false, false, true, true);
      this.gameDisplayModeSubMode = GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT;
      if (InterfaceMgr.Instance.getSelectedMenuVillage() != this.lastLoadedVillage)
        this.downloadCurrentVillage();
      switch (tabID)
      {
        case 0:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_VILLAGE;
          this.gfx.BGColor = ARGBColors.Black;
          if (!this.skipVillageTab)
            InterfaceMgr.Instance.initVillageTab();
          this.getVillage(InterfaceMgr.Instance.getSelectedMenuVillage())?.playEnvironmentalSounds();
          this.skipVillageTab = false;
          break;
        case 1:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_CASTLE;
          StatTrackingClient.Instance().ActivateTrigger(2, (object) null);
          this.gfx.BGColor = ARGBColors.Black;
          InterfaceMgr.Instance.initCastleTab();
          this.World.handleQuestObjectiveHappening(10004);
          break;
        case 2:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_VILLAGE;
          InterfaceMgr.Instance.initVillageTabTabBarsOnly();
          if (!InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(5);
            break;
          }
          InterfaceMgr.Instance.setVillageTabSubMode(1005);
          break;
        case 3:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_VILLAGE;
          InterfaceMgr.Instance.initVillageTabTabBarsOnly();
          if (!InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(3);
            break;
          }
          InterfaceMgr.Instance.setVillageTabSubMode(1003);
          break;
        case 4:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_VILLAGE;
          InterfaceMgr.Instance.initVillageTabTabBarsOnly();
          if (!InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(4);
            break;
          }
          InterfaceMgr.Instance.setVillageTabSubMode(1004);
          break;
        case 5:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_VILLAGE;
          InterfaceMgr.Instance.initVillageTabTabBarsOnly();
          if (!InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(18);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageAParishCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1008);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageACountyCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1108);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageAProvinceCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1208);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageACountryCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1308);
            break;
          }
          break;
        case 6:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_VILLAGE;
          InterfaceMgr.Instance.initVillageTabTabBarsOnly();
          if (!InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageAParishCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1006);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageACountyCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1106);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageAProvinceCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1206);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageACountryCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1306);
            break;
          }
          break;
        case 7:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_VILLAGE;
          InterfaceMgr.Instance.initVillageTabTabBarsOnly();
          if (!InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(8);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageAParishCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1007);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageACountyCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1107);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageAProvinceCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1207);
            break;
          }
          if (InterfaceMgr.Instance.isSelectedVillageACountryCapital())
          {
            InterfaceMgr.Instance.setVillageTabSubMode(1307);
            break;
          }
          break;
        case 8:
          InterfaceMgr.Instance.initVillageTabTabBarsOnly();
          break;
      }
      InterfaceMgr.Instance.StartDrawing();
    }

    public void setNextFactionPage(int pageID) => this.nextFactionPage = pageID;

    public void forceFactionTabChange() => this.lastFactionTabID = -1;

    public void factionTabChange(int tabID)
    {
      if (this.lastFactionTabID == tabID && tabID != 9)
        return;
      InterfaceMgr.Instance.StopDrawing();
      InterfaceMgr.Instance.getFactionTabBar().updateShownTabs();
      this.lastFactionTabID = tabID;
      InterfaceMgr.Instance.clearControls();
      this.gameDisplayModeSubMode = GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT;
      switch (tabID)
      {
        case 0:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_FACTIONS;
          InterfaceMgr.Instance.addMainWindow(this.m_firstDrawFactions, true);
          this.m_firstDrawFactions = false;
          if (this.nextFactionPage == 65)
          {
            InterfaceMgr.Instance.setVillageTabSubMode(this.nextFactionPage, false);
            break;
          }
          InterfaceMgr.Instance.initGloryTab();
          break;
        case 1:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_FACTIONS;
          if (this.nextFactionPage >= 0 && this.nextFactionPage != 999)
            InterfaceMgr.Instance.setVillageTabSubMode(this.nextFactionPage, false);
          else if (RemoteServices.Instance.UserFactionID >= 0)
            InterfaceMgr.Instance.showFactionPanel(RemoteServices.Instance.UserFactionID);
          else
            InterfaceMgr.Instance.setVillageTabSubMode(41, false);
          this.nextFactionPage = -1;
          break;
        case 2:
          this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_FACTIONS;
          if (this.nextFactionPage >= 0)
            InterfaceMgr.Instance.setVillageTabSubMode(this.nextFactionPage, false);
          else
            InterfaceMgr.Instance.setVillageTabSubMode(51, false);
          this.nextFactionPage = -1;
          break;
      }
      InterfaceMgr.Instance.StartDrawing();
    }

    public void politicsTabChange(int tabID)
    {
    }

    public void preAttackSetup(int parentVillageID, int attackingVillageID, int targetVillageID)
    {
      RemoteServices.Instance.set_PreAttackSetup_UserCallBack(new RemoteServices.PreAttackSetup_UserCallBack(this.preAttackSetupCallback));
      RemoteServices.Instance.PreAttackSetup(parentVillageID, attackingVillageID, targetVillageID, 0, 0, 0, 0, 0, 0, 0, 0);
    }

    public void preAttackSetupCallback(PreAttackSetup_ReturnType returnData)
    {
      if (returnData.protectedVillage)
      {
        int num = (int) MyMessageBox.Show(SK.Text("GameEngine_Protected_Interdiction", "This village is protected from attack by an Interdiction."), SK.Text("GENERIC_Village_Protected", "Village Protected"));
        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
      }
      else if (returnData.vacationVillage)
      {
        int num = (int) MyMessageBox.Show(SK.Text("GameEngine_Protected_Vacation", "This village is protected from attack by Vacation Mode."), SK.Text("GENERIC_Village_Protected", "Village Protected"));
        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
      }
      else if (returnData.vassalVacation)
      {
        int num = (int) MyMessageBox.Show(SK.Text("GameEngine_Vassal_Vacation", "Your vassal is in Vacation Mode and you cannot attack from here."), SK.Text("GENERIC_Cannot_Attack_Target", "Cannot Attack Target"));
        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
      }
      else if (returnData.peaceVillage)
      {
        if (!this.world.isCapital(returnData.targetVillage))
        {
          int num1 = (int) MyMessageBox.Show(SK.Text("GameEngine_Protected_Peacetime", "This village is within Peace Time and cannot be attacked."), SK.Text("GENERIC_Village_Protected", "Village Protected"));
        }
        else
        {
          int num2 = (int) MyMessageBox.Show(SK.Text("GameEngine_Protected_Peacetime_Capital", "This capital is within peace time and cannot be attacked."), SK.Text("GENERIC_Capital_Protected", "Capital Protected"));
        }
        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
      }
      else if (returnData.peaceAttacker)
      {
        if (returnData.parentAttackingVillage != returnData.attackingVillage)
        {
          int num = (int) MyMessageBox.Show(SK.Text("GameEngine_Cannot_Attack_PeaceTime", "You are within Peace Time and cannot attack from this village."), SK.Text("GENERIC_Village_Protected", "Village Protected"));
          InterfaceMgr.Instance.getMainTabBar().changeTab(9);
          InterfaceMgr.Instance.getMainTabBar().changeTab(0);
        }
        else
        {
          MessageBoxButtons buts = MessageBoxButtons.YesNo;
          if (MyMessageBox.Show(SK.Text("GameEngine_Currently_Peacetime", "You are currently Peace Time protected") + "\n" + SK.Text("GameEngine_CancelProtection", "Do you wish to cancel this protection?"), SK.Text("GENERIC_Village_Protected", "Village Protected"), buts) == DialogResult.Yes)
          {
            this.sentParentVillageID = returnData.parentAttackingVillage;
            this.sentAttackingVillageID = returnData.attackingVillage;
            this.sentTargetVillageID = returnData.targetVillage;
            RemoteServices.Instance.set_CancelInterdiction_UserCallBack(new RemoteServices.CancelInterdiction_UserCallBack(this.cancelInterdictionCallback));
            RemoteServices.Instance.CancelInterdiction(returnData.attackingVillage);
          }
          else
          {
            InterfaceMgr.Instance.getMainTabBar().changeTab(9);
            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
          }
        }
      }
      else if (returnData.protectedAttacker)
      {
        if (returnData.parentAttackingVillage != returnData.attackingVillage)
        {
          int num = (int) MyMessageBox.Show(SK.Text("GameEngine_Currently_Interdited_Vassal", "Your vassal is protected by Interdiction and you cannot attack from this village."), SK.Text("GameEngine_Currently_Interdited_protected", "Your Vassal is Protected"));
          InterfaceMgr.Instance.getMainTabBar().changeTab(9);
          InterfaceMgr.Instance.getMainTabBar().changeTab(0);
        }
        else
        {
          MessageBoxButtons buts = MessageBoxButtons.YesNo;
          if (MyMessageBox.Show(SK.Text("GameEngine_Currently_Interdited", "You are currently Interdiction protected") + "\n" + SK.Text("GameEngine_CancelProtection", "Do you wish to cancel this protection?"), SK.Text("GENERIC_Protected", "You Are Protected"), buts) == DialogResult.Yes)
          {
            this.sentParentVillageID = returnData.parentAttackingVillage;
            this.sentAttackingVillageID = returnData.attackingVillage;
            this.sentTargetVillageID = returnData.targetVillage;
            RemoteServices.Instance.set_CancelInterdiction_UserCallBack(new RemoteServices.CancelInterdiction_UserCallBack(this.cancelInterdictionCallback));
            if (this.LocalWorldData.AIWorld)
              RemoteServices.Instance.CancelInterdiction(-returnData.attackingVillage);
            else
              RemoteServices.Instance.CancelInterdiction(returnData.attackingVillage);
          }
          else
          {
            InterfaceMgr.Instance.getMainTabBar().changeTab(9);
            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
          }
        }
      }
      else if (returnData.Success)
      {
        int num3 = 0;
        if (returnData.battleHonourData != null)
        {
          returnData.battleHonourData.attackType = 11;
          if (!GameEngine.Instance.World.isCapital(returnData.parentAttackingVillage))
            num3 = CastlesCommon.calcBattleHonourCost(returnData.battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset, GameEngine.Instance.LocalWorldData.EraWorld);
        }
        if (num3 > 0 && this.World.getCurrentHonour() <= 0.0)
        {
          int num4 = (int) MyMessageBox.Show(SK.Text("GameEngine_Require_Honour_To_Attack", "You require honour to attack this target."), SK.Text("GENERIC_Attack_Error", "Attack Error"));
        }
        else
        {
          this.InitCastleAttackSetup(returnData.castleMapSnapshot, returnData.castleTroopsSnapshot, returnData.keepLevel, returnData.numPeasants, returnData.numArchers, returnData.numPikemen, returnData.numSwordsmen, returnData.numCatapults, returnData.attackingVillage, returnData.targetVillage, returnData.attackType, returnData.pillagePercent, returnData.captainsCommand, returnData.parentAttackingVillage, returnData.numPeasantsInCastle, returnData.numArchersInCastle, returnData.numPikemenInCastle, returnData.numSwordsmenInCastle, returnData.targetUserID, returnData.targetUserName, returnData.battleHonourData, returnData.numCaptainsInCastle, returnData.numCaptains, returnData.landType, returnData.capitalAttackRate);
          InterfaceMgr.Instance.setCastleViewTimes(returnData.lastCastleTime, returnData.castleMapSnapshot != null, returnData.lastTroopTime, returnData.castleTroopsSnapshot != null);
        }
      }
      else
      {
        switch (returnData.m_errorCode)
        {
          case ErrorCodes.ErrorCode.ATTACKING_NOT_ENOUGH_TROOPS:
          case ErrorCodes.ErrorCode.ATTACKING_INVALID_TARGET:
            int num5 = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("GENERIC_Attack_Error", "Attack Error"));
            break;
        }
      }
    }

    public void cancelInterdictionCallback(CancelInterdiction_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      RemoteServices.Instance.set_PreAttackSetup_UserCallBack(new RemoteServices.PreAttackSetup_UserCallBack(this.preAttackSetupCallback));
      RemoteServices.Instance.PreAttackSetup(this.sentParentVillageID, this.sentAttackingVillageID, this.sentTargetVillageID, 0, 0, 0, 0, 0, 0, 0, 0);
    }

    public void InitCastleAttackSetup()
    {
      InterfaceMgr.Instance.clearControls();
      if (this.castle_AttackerSetup == null)
        this.castle_AttackerSetup = new CastleMap(-1, this.gfx, 1);
      this.castle_AttackerSetup.castleShown(false);
      this.castle_AttackerSetup.reInitGFX();
      if (this.castle == null)
        this.castle_AttackerSetup.importDefenderSnapshot((byte[]) null, (byte[]) null, 0, false, 0);
      else
        this.castle_AttackerSetup.importDefenderSnapshot(this.castle.generateCastleMapSnapshot(), this.castle.generateCastleTroopsSnapshot(), 0, false, 0);
      this.castle_AttackerSetup.initFakeSetup();
      this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_CASTLE;
      this.gameDisplayModeSubMode = GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP;
      this.gfx.BGColor = ARGBColors.Black;
      InterfaceMgr.Instance.initCastleAttackerSetupTab();
    }

    public void InitCastleAttackSetup(
      byte[] castleMap,
      byte[] defenderMap,
      int keepLevel,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int attackingVillage,
      int targetVillage,
      int attackType,
      int pillagePercent,
      int captainsCommand,
      int parentOfAttackingVillage,
      int numPeasantsInCastle,
      int numArchersInCastle,
      int numPikemenInCastle,
      int numSwordsmenInCastle,
      int targetUserID,
      string targetUserName,
      BattleHonourData honourData,
      int numCaptainsInCastle,
      int numCaptains,
      int landType,
      double capitalAttackRate)
    {
      try
      {
        InterfaceMgr.Instance.clearControls();
        if (this.castle_AttackerSetup == null)
          this.castle_AttackerSetup = new CastleMap(-1, this.gfx, 1);
        this.castle_AttackerSetup.castleShown(false);
        this.castle_AttackerSetup.reInitGFX();
        int mode = 0;
        switch (this.World.getSpecial(targetVillage))
        {
          case 3:
            mode = 1;
            break;
          case 5:
            mode = 2;
            break;
        }
        this.castle_AttackerSetup.setCampMode(mode);
        this.castle_AttackerSetup.importDefenderSnapshot(castleMap, defenderMap, keepLevel, true, landType);
        this.castle_AttackerSetup.initRealSetup(attackingVillage, targetVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, attackType, pillagePercent, captainsCommand, parentOfAttackingVillage, numPeasantsInCastle, numArchersInCastle, numPikemenInCastle, numSwordsmenInCastle, targetUserID, targetUserName, honourData, numCaptainsInCastle, numCaptains, capitalAttackRate);
        this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_CASTLE;
        this.gameDisplayModeSubMode = GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP;
        this.gfx.BGColor = ARGBColors.Black;
        InterfaceMgr.Instance.initCastleAttackerSetupTab();
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log(ex.ToString() + " values = " + (object) castleMap + (object) defenderMap + (object) keepLevel + (object) numPeasants + (object) numArchers + (object) numPikemen + (object) numSwordsmen + (object) numCatapults + (object) attackingVillage + (object) targetVillage + (object) attackType + (object) pillagePercent + (object) captainsCommand + (object) parentOfAttackingVillage + (object) numPeasantsInCastle + (object) numArchersInCastle);
      }
    }

    public void InitCastlePreview(CastleMapPreset preset)
    {
      InterfaceMgr.Instance.clearControls();
      if (this.castle_Preview == null)
        this.castle_Preview = new CastleMap(-1, this.gfx, 4);
      this.castle_Preview.castleShown(false);
      this.castle_Preview.reInitGFX();
      List<CastleElement> newElements = new List<CastleElement>();
      foreach (CastleMapPreset.CastleElementInfo castleElementInfo in preset.BasicData)
        newElements.Add(new CastleElement()
        {
          elementType = castleElementInfo.elementType,
          xPos = castleElementInfo.xPos,
          yPos = castleElementInfo.yPos,
          reinforcement = castleElementInfo.reinforcement,
          aggressiveDefender = false,
          completionTime = DateTime.MinValue,
          damage = 0.0f,
          elementID = -1L
        });
      this.castle_Preview.importElements(newElements);
      this.castle_Preview.initFakeSetup();
      this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_CASTLE;
      this.gameDisplayModeSubMode = GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW;
      this.gfx.BGColor = ARGBColors.Black;
    }

    public void InitBattle(int keepType, int fakeDefensiveMode)
    {
      CastleResearchData defenderResearchData = new CastleResearchData();
      CastleResearchData attackerResearchData = new CastleResearchData();
      switch (fakeDefensiveMode)
      {
        case 0:
          defenderResearchData.catapult = 0;
          defenderResearchData.conscription = 0;
          defenderResearchData.longBow = 0;
          defenderResearchData.pike = 0;
          defenderResearchData.sword = 0;
          keepType = 1;
          break;
        case 1:
          defenderResearchData.defences = 4;
          keepType = 3;
          defenderResearchData.catapult = 2;
          defenderResearchData.conscription = 2;
          defenderResearchData.longBow = 2;
          defenderResearchData.pike = 2;
          defenderResearchData.sword = 2;
          break;
        case 2:
          defenderResearchData.defences = 8;
          defenderResearchData.sallyForth = 2;
          keepType = 5;
          defenderResearchData.catapult = 4;
          defenderResearchData.conscription = 4;
          defenderResearchData.longBow = 4;
          defenderResearchData.pike = 4;
          defenderResearchData.sword = 4;
          break;
        case 3:
          defenderResearchData.defences = 10;
          defenderResearchData.sallyForth = 4;
          keepType = 10;
          defenderResearchData.catapult = 6;
          defenderResearchData.conscription = 6;
          defenderResearchData.longBow = 6;
          defenderResearchData.pike = 6;
          defenderResearchData.sword = 6;
          defenderResearchData.tunnel = 6;
          break;
      }
      attackerResearchData.defences = (int) this.World.UserResearchData.Research_Defences;
      attackerResearchData.catapult = (int) this.World.UserResearchData.Research_Catapult;
      attackerResearchData.sword = (int) this.World.UserResearchData.Research_Sword;
      attackerResearchData.pike = (int) this.World.UserResearchData.Research_Pike;
      attackerResearchData.longBow = (int) this.World.UserResearchData.Research_LongBow;
      attackerResearchData.conscription = (int) this.World.UserResearchData.Research_Conscription;
      attackerResearchData.sallyForth = (int) this.World.UserResearchData.Research_SallyForth;
      attackerResearchData.vaults = (int) this.World.UserResearchData.Research_Vaults;
      InterfaceMgr.Instance.clearControls();
      this.castle_Battle = new CastleMap(-1, this.gfx, 3);
      this.castle_Battle.castleShown(false);
      this.castle_Battle.reInitGFX();
      this.castle_Battle.setCampMode(0);
      if (keepType < 0)
        keepType = 1;
      this.castle_Battle.launchBattle(this.castle_AttackerSetup.generateCastleMapSnapshot(), (byte[]) null, this.castle_AttackerSetup.generateCastleTroopsSnapshot(), (byte[]) null, keepType, defenderResearchData, attackerResearchData, 0, -1, -1, -1, 0, false, false);
      this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_CASTLE;
      this.gameDisplayModeSubMode = GameEngine.GameDisplaySubModes.SUBMODE_BATTLE;
      this.gfx.BGColor = ARGBColors.Black;
      InterfaceMgr.Instance.initCastleBattleTab(true, -1, false);
    }

    public void InitBattle(GetReport_ReturnType reportData, ViewBattle_ReturnType battleData)
    {
      int campMode = 0;
      if (reportData.reportType == (short) 24)
        campMode = 1;
      else if (reportData.reportType == (short) 25)
        campMode = 2;
      int pillageInfo = -1;
      int ransackCount = -1;
      int raidCount = -1;
      switch (reportData.genericData30)
      {
        case 2:
        case 4:
        case 5:
        case 6:
        case 7:
          pillageInfo = reportData.genericData31;
          if (pillageInfo > 9999)
          {
            pillageInfo -= 10000;
            break;
          }
          break;
        case 3:
          ransackCount = reportData.genericData31;
          if (ransackCount > 9999)
          {
            ransackCount -= 10000;
            break;
          }
          break;
        case 12:
          raidCount = reportData.genericData31;
          if (raidCount > 9999)
          {
            raidCount -= 10000;
            break;
          }
          break;
      }
      this.InitBattle(battleData.castleMapSnapshot, battleData.damageMapSnapshot, battleData.castleTroopsSnapshot, battleData.attackMapSnapshot, battleData.keepLevel, battleData.defenderResearchData, battleData.attackerResearchData, campMode, pillageInfo, ransackCount, raidCount, reportData.genericData30, reportData.defendingVillage, reportData, battleData.landType);
    }

    public void InitBattle(
      byte[] compressedCastleMap,
      byte[] compressedCastleDamageMap,
      byte[] compressedDefenderMap,
      byte[] compressedAttackerMap,
      int keepType,
      CastleResearchData defenderResearchData,
      CastleResearchData attackerResearchData,
      int campMode,
      int pillageInfo,
      int ransackCount,
      int raidCount,
      int attackType,
      int villageID,
      GetReport_ReturnType reportReturnData,
      int landType)
    {
      InterfaceMgr.Instance.clearControls();
      this.castle_Battle = new CastleMap(villageID, this.gfx, 3);
      this.castle_Battle.castleShown(false);
      this.castle_Battle.reInitGFX();
      this.castle_Battle.setCampMode(campMode);
      bool oldReport = false;
      if (reportReturnData != null && reportReturnData.reportTime < CastlesCommon.PRE_FOREST_CHANGE_DATE)
        oldReport = true;
      this.castle_Battle.setReportData(reportReturnData);
      this.castle_Battle.launchBattle(compressedCastleMap, compressedCastleDamageMap, compressedDefenderMap, compressedAttackerMap, keepType, defenderResearchData, attackerResearchData, campMode, pillageInfo, ransackCount, raidCount, landType, false, oldReport);
      this.castle_Battle.returnToReports();
      this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_CASTLE;
      this.gameDisplayModeSubMode = GameEngine.GameDisplaySubModes.SUBMODE_BATTLE;
      this.gfx.BGColor = ARGBColors.Black;
      bool AIAttack = true;
      if (reportReturnData != null)
        AIAttack = this.World.isSpecial(reportReturnData.attackingVillage);
      InterfaceMgr.Instance.initCastleBattleTab(true, attackType, AIAttack);
    }

    public void InitCastleView(
      byte[] compressedCastleMap,
      byte[] compressedDefenderMap,
      int keepType,
      int campMode,
      int defencesResearch,
      int villageID,
      int landType)
    {
      InterfaceMgr.Instance.clearControls();
      this.castle_Battle = new CastleMap(-1, this.gfx, 3);
      this.castle_Battle.castleShown(false);
      this.castle_Battle.reInitGFX();
      this.castle_Battle.setCampMode(campMode);
      this.castle_Battle.clearTempAttackers();
      this.castle_Battle.launchBattle(compressedCastleMap, (byte[]) null, compressedDefenderMap, (byte[]) null, keepType, new CastleResearchData()
      {
        defences = defencesResearch
      }, new CastleResearchData(), campMode, -1, -1, -1, landType, true, false);
      this.castle_Battle.returnToReports();
      this.castle_Battle.setRealBattleMode(false);
      this.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_CASTLE;
      this.gameDisplayModeSubMode = GameEngine.GameDisplaySubModes.SUBMODE_BATTLE;
      this.gfx.BGColor = ARGBColors.Black;
      InterfaceMgr.Instance.initCastleBattleTab(false, villageID, false);
    }

    public void windowClosing()
    {
      foreach (VillageMap village in this.villages)
        village.dispose();
      this.villages.Clear();
      foreach (CastleMap castle in this.castles)
        castle.dispose();
      this.castles.Clear();
      this.cardsManager.UserCardData = (CardData) null;
      InterfaceMgr.Instance.ignoreStopDraw = true;
      InterfaceMgr.Instance.logout();
      InterfaceMgr.Instance.changeTab(0);
      InterfaceMgr.Instance.ignoreStopDraw = false;
      this.World.clearParishChat();
      this.World.resetLeaderboards();
      this.World.logout();
      this.newResolution = -1;
      this.nextFactionPage = -1;
      this.villageToAbandon = -1;
      WorldsEndPanel.logout();
      if (this.noVillagePopup != null)
        this.noVillagePopup.closing = true;
      if (this.lostVillagePopup != null)
        this.lostVillagePopup.closing = true;
      if (this.worldsEndPopup != null)
        this.worldsEndPopup.closing = true;
      if (this.noAutoVillagePopup != null)
        this.noAutoVillagePopup.closing = true;
      this.closeNoVillagePopup(false);
      this.noVillagePopup = (NewSelectVillageAreaWindow) null;
      this.lostVillagePopup = (LostVillageWindow) null;
      this.worldsEndPopup = (WorldsEndWindow) null;
      this.noAutoVillagePopup = (NewAutoSelectVillageWindow) null;
    }

    public void setPendingSessionExpiredStat(int errorNo)
    {
      this.pendingErrorCode = errorNo;
      if (errorNo == -1)
        return;
      GameEngine.Instance.World.downloadingCounter = -100;
      this.forceRelogin();
    }

    public bool pendingError()
    {
      if (this.pendingErrorCode == -1)
        return false;
      this.sessionExpired(this.pendingErrorCode);
      return true;
    }

    public void appClosing() => this.appClose = true;

    public void initWorldData(WorldData newWorldData) => this.worldData = newWorldData;

    public void sessionExpired(int errorNo)
    {
      string connectionErrorTitle = GameEngine.GetConnectionErrorTitle(errorNo);
      string txtMessage = GameEngine.GetConnectionErrorMessage(errorNo);
      UniversalDebugLog.Log("Got session expired error " + (object) errorNo);
      bool flag = false;
      if (errorNo == 11)
      {
        errorNo = 1;
        flag = true;
      }
      if (errorNo == 1 && InterfaceMgr.Instance.ParentForm != null && !this.appClose)
      {
        if (InterfaceMgr.Instance.isConnectionErrorWindow() || this.forcingLogout)
          return;
        if (!flag)
        {
          InterfaceMgr.Instance.closeAllPopups();
          InterfaceMgr.Instance.getMainTabBar().changeTab(0);
        }
        InterfaceMgr.Instance.openConnectionErrorWindow();
      }
      else
      {
        InterfaceMgr.Instance.closeAllPopups();
        InterfaceMgr.Instance.chatClose();
        if (InterfaceMgr.Instance.ParentForm != null && !this.appClose)
        {
          if (errorNo >= 0)
          {
            if (errorNo == 0 && this.LocalWorldData.Alternate_Ruleset == 1 && this.getDominationTimeLeft().TotalMinutes < 5.0)
            {
              GameEngine.Instance.openLostVillage(1000);
              while (this.lostVillagePopup != null && this.lostVillagePopup.Visible)
              {
                Thread.Sleep(100);
                Application.DoEvents();
              }
            }
            if (errorNo == 1)
              txtMessage = txtMessage + "\n\n" + this.connectionErrorString;
            int num = (int) MyMessageBox.Show(txtMessage, connectionErrorTitle);
          }
          this.m_doReLogin = true;
        }
        this.World.invalidateWorldData();
        if (this.dPop != null && this.dPop.Created)
          this.dPop.Close();
        if (this.m_loginHistoryPop != null && this.m_loginHistoryPop.Created)
          this.m_loginHistoryPop.Close();
        this.pendingErrorCode = -1;
      }
    }

    public static string GetConnectionErrorTitle(int errorNo)
    {
      switch (errorNo)
      {
        case 0:
          return SK.Text("GameEngine_Session_Lost", "Session Lost");
        case 1:
        case 11:
          return SK.Text("ConnectioError_Title", "Problem with Connection to Server");
        case 3:
          return SK.Text("ServerDowntime_Scheduled_DownTime", "Scheduled Server Maintenance");
        default:
          return SK.Text("GENERIC_Connection_Error", "Connection Error");
      }
    }

    public static string GetConnectionErrorMessage(int errorNo)
    {
      switch (errorNo)
      {
        case 0:
          return SK.Text("GameEngine_Session_Lost_Message", "Your current session has been lost, please login again.");
        case 1:
        case 11:
          return SK.Text("GameEngine_Cannot_Access_Server", "Cannot access Server.");
        case 2:
          return SK.Text("GameEngine_Connection_Timed_Out", "Your connection has timed out.");
        case 3:
          return SK.Text("ServerDowntime_Scheduled_DownTime", "Scheduled Server Maintenance");
        default:
          return "";
      }
    }

    public void forceLogout()
    {
      this.forcingLogout = true;
      InterfaceMgr.Instance.chatClose();
      this.m_doReLogin = true;
      this.World.invalidateWorldData();
      if (this.dPop != null && this.dPop.Created)
        this.dPop.Close();
      if (this.m_loginHistoryPop != null && this.m_loginHistoryPop.Created)
        this.m_loginHistoryPop.Close();
      this.pendingErrorCode = -1;
      InterfaceMgr.Instance.closeAllPopups();
    }

    public void chatSessionExpired(int errorNo)
    {
    }

    public void forceRelogin() => this.m_doReLogin = true;

    public bool loginCancelled() => this.m_doReLogin;

    public bool reLogin()
    {
      bool doReLogin = this.m_doReLogin;
      this.m_doReLogin = false;
      return doReLogin;
    }

    public void FlagQuitGame() => this.quitGame = true;

    public bool quitting() => this.quitGame;

    public static void displayDirectXError()
    {
      int num = (int) MessageBox.Show(SK.Text("GameEngine_DX_problem", "There is a problem with DirectX, please contact Support."), SK.Text("GameEngine_DX_Error", "DirectX Error"));
      Application.Exit();
    }

    public void remoteConnectionCommonHandler(Common_ReturnData returnData)
    {
      InterfaceMgr.Instance.getMainTabBar().newReports(returnData.NewReports);
      InterfaceMgr.Instance.getMainTabBar().newMail(returnData.NewMail);
      if (returnData.NewMail)
        InterfaceMgr.Instance.mailPopupNewMail();
      InterfaceMgr.Instance.getMainTabBar().newPoliticsPost(returnData.NewPoliticsForumPost);
      if (returnData.NewIngameMessage)
      {
        RemoteServices.Instance.set_GetIngameMessage_UserCallBack(new RemoteServices.GetIngameMessage_UserCallBack(this.getIngameMessageCallback));
        RemoteServices.Instance.GetIngameMessage();
      }
      if (!returnData.NoVillages)
        return;
      InterfaceMgr.Instance.getMainTabBar().changeTab(9);
      InterfaceMgr.Instance.getMainTabBar().changeTab(0);
    }

    public void getIngameMessageCallback(GetIngameMessage_ReturnType returnData)
    {
      if (!returnData.Success || returnData.message.Length <= 0)
        return;
      AdminInfoPopup.setMessage(returnData.message);
      AdminInfoPopup.showMessage();
    }

    public void forceDownloadCurrentVillage()
    {
      this.villages[InterfaceMgr.Instance.getSelectedMenuVillage()] = (object) null;
      this.downloadCurrentVillage();
    }

    public void downloadCurrentVillage()
    {
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      InterfaceMgr.Instance.villageChanged(selectedMenuVillage);
      InterfaceMgr.Instance.castleChanged();
      this.castle = (CastleMap) null;
      this.village = (VillageMap) null;
      this.lastLoadedVillage = selectedMenuVillage;
      bool needParishPeople = false;
      if (this.villages[selectedMenuVillage] != null && this.castles[selectedMenuVillage] != null)
      {
        this.village = (VillageMap) this.villages[selectedMenuVillage];
        this.castle = (CastleMap) this.castles[selectedMenuVillage];
        this.village.Camera.Drag(new Point(0, 0));
        this.castle.moveMap(0, 0);
        this.village.ViewOnly = false;
        if (this.World.isCapital(selectedMenuVillage) && this.village.needParishPeople())
          needParishPeople = true;
        if ((DateTime.Now - this.village.lastDownloadedTime).TotalMinutes < 5.0)
        {
          VillageMap.loadVillageBuildingsGFX2();
          this.village.loadBackgroundImage();
          this.village.reAddBuildingsToMap();
          this.village.updateConstructionOnCachedLoad();
          this.castle.reInitGFX();
          CastleMap.CreateMode = false;
          InterfaceMgr.Instance.villageDownloaded(selectedMenuVillage);
          this.castle.castleShown(true);
          return;
        }
      }
      else if (this.World.isCapital(selectedMenuVillage))
        needParishPeople = true;
      RemoteServices.Instance.GetVillageBuildingsList(selectedMenuVillage, true, needParishPeople);
      VillageMap.loadVillageBuildingsGFX2();
      if (this.village != null)
        this.village.loadBackgroundImage();
      if (this.castle != null)
        this.castle.reInitGFX();
      CastleMap.CreateMode = false;
      if (this.convertVillageCallback == null)
        return;
      this.convertVillageCallback();
    }

    public void flushVillages()
    {
      foreach (VillageMap village in this.villages)
      {
        if (village != null)
          village.lastDownloadedTime = DateTime.MinValue;
      }
    }

    public void flushVillage(int villageID)
    {
      if (this.villages[villageID] == null)
        return;
      VillageMap village = (VillageMap) this.villages[villageID];
      if (village == null)
        return;
      village.lastDownloadedTime = DateTime.MinValue;
    }

    public void getVillageBuildingListCallBack(GetVillageBuildingsList_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.existingArmies != null)
          this.World.updateExistingArmies(returnData.existingArmies);
        if (InterfaceMgr.Instance.getSelectedMenuVillage() == returnData.villageID)
        {
          int villageId = returnData.villageID;
          if (this.villages[villageId] == null)
          {
            VillageMap villageMap = new VillageMap(returnData.mapID, returnData.mapVariant, returnData.mapType, villageId, this.gfx);
            this.villages[villageId] = (object) villageMap;
          }
          bool flag = false;
          VillageMap village = (VillageMap) this.villages[villageId];
          if (villageId == InterfaceMgr.Instance.getSelectedMenuVillage() || returnData.viewOnly)
          {
            this.village = village;
            flag = true;
          }
          village.resetMapType(returnData.mapID, returnData.mapVariant, returnData.mapType);
          if (flag)
          {
            village.loadBackgroundImage();
            village.reInitGFX(this.gfx);
          }
          village.ViewOnly = returnData.viewOnly;
          village.ViewHonour = returnData.viewHonour;
          village.lastDownloadedTime = DateTime.Now;
          if (returnData.parishTaxInfo != null)
            village.importParishTaxPeople(returnData.parishTaxInfo, returnData.currentTime);
          VillageMap.setServerTime(returnData.currentTime);
          if (returnData.fullUpdate)
            village.initClickMask();
          village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
          village.importVillageBuildings(returnData.villageBuildings, returnData.fullUpdate);
          if (!returnData.viewOnly)
            village.importTraders(returnData.traders, returnData.currentTime);
          DXPanel.skipPaint = true;
          if (this.lastVillageTabID == 0)
            village.playEnvironmentalSounds();
          InterfaceMgr.Instance.villageDownloaded(returnData.villageID);
          if (!returnData.viewOnly)
            this.getCastleCallBack(returnData);
          if (returnData.viewOnly)
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(50);
        }
        if (returnData.viewOnly)
          return;
        this.World.importOrphanedPeople(returnData.people, returnData.currentTime, returnData.villageID);
      }
      else
      {
        if (returnData.m_errorCode != ErrorCodes.ErrorCode.VILLAGE_BUILDINGS_NO_LONGER_OWNER || returnData.viewOnly)
          return;
        this.displayedVillageLost(returnData.villageID, true);
      }
    }

    public VillageMap getVillage(int villageID)
    {
      if (villageID < 0)
        return (VillageMap) null;
      return this.villages[villageID] == null ? (VillageMap) null : (VillageMap) this.villages[villageID];
    }

    public void displayedVillageLost(int villageID, bool popup)
    {
      InterfaceMgr.Instance.closeVillageTab();
      InterfaceMgr.Instance.closeCastleTab();
      this.world.updateWorldMapOwnership();
      if (popup)
      {
        int num = (int) MyMessageBox.Show(SK.Text("GameEngine_Lost_Control_Of_Village", "You have lost control of this village!"), SK.Text("GENERIC_Error", "Error"));
      }
      if (this.villages[villageID] != null)
      {
        this.villages[villageID] = (object) null;
        this.village = (VillageMap) null;
      }
      InterfaceMgr.Instance.getMainTabBar().changeTab(9);
      InterfaceMgr.Instance.getMainTabBar().changeTab(0);
    }

    public void downloadCurrentCastle()
    {
    }

    public void getCastleCallBack(GetVillageBuildingsList_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      int villageId = returnData.villageID;
      if (this.castles[villageId] == null)
      {
        CastleMap castleMap = new CastleMap(villageId, this.gfx, 0);
        this.castles[villageId] = (object) castleMap;
      }
      CastleMap castle = (CastleMap) this.castles[villageId];
      if (villageId == InterfaceMgr.Instance.getSelectedMenuVillage())
        this.castle = castle;
      CastleMap.setServerTime(returnData.currentTime);
      castle.importElements(returnData.elements);
      castle.castleShown(true);
    }

    public void toggleDebugPopup()
    {
      if (this.dPop == null)
      {
        this.dPop = new DebugPopup();
        this.dPop.Show();
      }
      else if (this.dPop.Created)
      {
        this.dPop.Close();
        this.dPop = (DebugPopup) null;
      }
      else
      {
        this.dPop = new DebugPopup();
        this.dPop.Show();
      }
    }

    private void debugPopupRun()
    {
      if (this.dPop == null || !this.dPop.Created)
        return;
      this.dPop.run();
    }

    public void showLoginHistory()
    {
      if (this.m_loginHistoryPop != null)
      {
        if (this.m_loginHistoryPop.Created)
          this.m_loginHistoryPop.Close();
        this.m_loginHistoryPop = (LoginHistoryPopup) null;
      }
      this.m_loginHistoryPop = new LoginHistoryPopup();
      this.m_loginHistoryPop.Show();
    }

    private void loginHistoryRun()
    {
      if (this.m_loginHistoryPop == null || !this.m_loginHistoryPop.Created)
        return;
      this.m_loginHistoryPop.update();
    }

    public void startResizeWindow() => InterfaceMgr.Instance.mainWindowStartResize();

    public void resizeWindow()
    {
      if (this.gfx != null)
        this.gfx.resizeWindow();
      InterfaceMgr.Instance.mainWindowResize();
      switch (this.lastTabID)
      {
        case 0:
          if (this.World != null)
          {
            this.World.moveMap(0.0, 0.0);
            break;
          }
          break;
        case 1:
          if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE)
          {
            InterfaceMgr.Instance.villageMapResizeWindow();
            if (this.village != null)
            {
              this.village.Camera.Drag(new Point(0, 0));
              this.Village.createSurroundSprites();
              this.gfx.RenderList.clearLayers();
              this.village.justDrawSprites();
              break;
            }
            break;
          }
          if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
          {
            InterfaceMgr.Instance.castleMapResizeWindow();
            if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT)
            {
              if (this.castle != null)
              {
                this.castle.moveMap(0, 0);
                this.castle.createSurroundSprites();
                this.gfx.RenderList.clearLayers();
                this.castle.justDrawSprites();
                this.castle.recalcCastleLayout();
                break;
              }
              break;
            }
            if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
            {
              if (this.castle_AttackerSetup != null)
              {
                this.castle_AttackerSetup.moveMap(0, 0);
                this.castle_AttackerSetup.createSurroundSprites();
                this.gfx.RenderList.clearLayers();
                this.castle_AttackerSetup.justDrawSprites();
                break;
              }
              break;
            }
            if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_BATTLE)
            {
              if (this.castle_Battle != null)
              {
                this.castle_Battle.moveMap(0, 0);
                this.castle_Battle.createSurroundSprites();
                this.gfx.RenderList.clearLayers();
                this.castle_Battle.justDrawSprites();
                break;
              }
              break;
            }
            if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW && this.castle_Preview != null)
            {
              this.castle_Preview.moveMap(0, 0);
              this.castle_Preview.createSurroundSprites();
              this.gfx.RenderList.clearLayers();
              this.castle_Preview.justDrawSprites();
              break;
            }
            break;
          }
          break;
        case 9:
          if (this.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
          {
            InterfaceMgr.Instance.castleMapResizeWindow();
            if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_DEFAULT)
            {
              if (this.castle != null)
              {
                this.castle.moveMap(0, 0);
                this.castle.createSurroundSprites();
                this.gfx.RenderList.clearLayers();
                this.castle.justDrawSprites();
                this.castle.recalcCastleLayout();
                break;
              }
              break;
            }
            if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
            {
              if (this.castle_AttackerSetup != null)
              {
                this.castle_AttackerSetup.moveMap(0, 0);
                this.castle_AttackerSetup.createSurroundSprites();
                this.gfx.RenderList.clearLayers();
                this.castle_AttackerSetup.justDrawSprites();
                break;
              }
              break;
            }
            if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_BATTLE)
            {
              if (this.castle_Battle != null)
              {
                this.castle_Battle.moveMap(0, 0);
                this.castle_Battle.createSurroundSprites();
                this.gfx.RenderList.clearLayers();
                this.castle_Battle.justDrawSprites();
                break;
              }
              break;
            }
            if (this.gameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_PREVIEW && this.castle_Preview != null)
            {
              this.castle_Preview.moveMap(0, 0);
              this.castle_Preview.createSurroundSprites();
              this.gfx.RenderList.clearLayers();
              this.castle_Preview.justDrawSprites();
              break;
            }
            break;
          }
          break;
      }
      InterfaceMgr.Instance.getDXBasePanel().Invalidate();
    }

    public void playInterfaceSound(string soundTag) => this.playInterfaceSound(soundTag, true);

    public void playInterfaceSound(string soundTag, bool overwritePlayingSound)
    {
      if (soundTag.Trim().Length == 0 || this.AudioEngine == null || this.stopInterfaceSounds || !Sound.SFXActive || !overwritePlayingSound && this.AudioEngine.isSoundPlaying(soundTag))
        return;
      this.AudioEngine.playInterfaceSound(soundTag);
    }

    public static void updateFolderPermissions(string path)
    {
      if (GameEngine.updatedPermissions)
        return;
      GameEngine.updatedPermissions = true;
      try
      {
        string Account = (new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, (SecurityIdentifier) null).Translate(typeof (NTAccount)) as NTAccount).ToString();
        GameEngine.AddDirectorySecurity(path, Account, FileSystemRights.FullControl, AccessControlType.Allow);
      }
      catch (Exception ex)
      {
      }
    }

    public static void AddDirectorySecurity(
      string FileName,
      string Account,
      FileSystemRights Rights,
      AccessControlType ControlType)
    {
      DirectoryInfo directoryInfo = new DirectoryInfo(FileName);
      DirectorySecurity accessControl = directoryInfo.GetAccessControl();
      accessControl.AddAccessRule(new FileSystemAccessRule(Account, Rights, InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, ControlType));
      accessControl.AddAccessRule(new FileSystemAccessRule(Account, Rights, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, ControlType));
      accessControl.AddAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));
      directoryInfo.SetAccessControl(accessControl);
    }

    public void DisableMouseClicks()
    {
      if (this.Filter != null)
        return;
      this.Filter = new GameEngine.MouseClickMessageFilter();
      Application.AddMessageFilter((IMessageFilter) this.Filter);
    }

    public void EnableMouseClicks()
    {
      if (this.Filter == null)
        return;
      Application.RemoveMessageFilter((IMessageFilter) this.Filter);
      this.Filter = (GameEngine.MouseClickMessageFilter) null;
    }

    public void initCensorText(string[] words)
    {
      List<string> stringList = new List<string>();
      foreach (string word in words)
      {
        if (word.ToLower() != "niger")
          stringList.Add(word);
      }
      this.badWords = stringList.ToArray();
    }

    public string censorString(string text)
    {
      if (this.badWords == null)
        return text;
      if (GameEngine.staticCensor == null)
        GameEngine.staticCensor = new Censor((IEnumerable<string>) this.badWords);
      return GameEngine.staticCensor.CensorText(text);
    }

    public void installKeyboardHook()
    {
      this.uninstallKeyboardHook();
      GameEngine._hookID = GameEngine.SetHook(GameEngine._proc, 13);
      GameEngine.keyboardHookedInstalled = true;
    }

    public void uninstallKeyboardHook()
    {
      if (!GameEngine.keyboardHookedInstalled)
        return;
      GameEngine.UnhookWindowsHookEx(GameEngine._hookID);
      GameEngine.keyboardHookedInstalled = false;
    }

    private static IntPtr SetHook(GameEngine.LowLevelKeyboardProc proc, int type)
    {
      using (Process currentProcess = Process.GetCurrentProcess())
      {
        using (ProcessModule mainModule = currentProcess.MainModule)
          return GameEngine.SetWindowsHookEx(type, proc, GameEngine.GetModuleHandle(mainModule.ModuleName), 0U);
      }
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
      if (GameEngine.Instance.GFX == null)
        return GameEngine.CallNextHookEx(GameEngine._hookID, nCode, wParam, lParam);
      if (nCode >= 0 && wParam == (IntPtr) 256)
      {
        GameEngine.Instance.lastMouseMoveTime = DateTime.Now;
        int num = Marshal.ReadInt32(lParam);
        switch (num)
        {
          case 160:
          case 161:
            GameEngine.shiftPressedAlways = true;
            break;
        }
        GameEngine.lastKeyPressed = num;
        Form activeForm = Form.ActiveForm;
        bool flag1 = false;
        if (!GameEngine.StopKeyTrap && InterfaceMgr.Instance.ParentForm != null && (activeForm == InterfaceMgr.Instance.ParentForm || activeForm == InterfaceMgr.Instance.ChatForm && activeForm != null))
        {
          bool flag2 = false;
          if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_MAIL || GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE || GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE || GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_WORLD)
            flag2 = true;
          if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE && InterfaceMgr.Instance.isTextInputScreenActive())
          {
            switch (num)
            {
              case 9:
                GameEngine.tabPressed = true;
                break;
              case 160:
              case 161:
                GameEngine.shiftPressed = true;
                break;
            }
            return GameEngine.CallNextHookEx(GameEngine._hookID, nCode, wParam, lParam);
          }
          if (activeForm == InterfaceMgr.Instance.ChatForm)
            flag1 = true;
          switch (num)
          {
            case 9:
              if (!flag1)
              {
                GameEngine.tabPressed = true;
                break;
              }
              break;
            case 13:
              if (!flag1)
              {
                GameEngine.enterPressed = true;
                break;
              }
              break;
            case 37:
              if (flag2)
              {
                if (!flag1)
                  GameEngine.scrollLeft = true;
                if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_MAIL && !flag1)
                  return (IntPtr) 1;
                break;
              }
              break;
            case 38:
              if (flag2)
              {
                if (!flag1)
                  GameEngine.scrollUp = true;
                if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_MAIL && !flag1)
                  return (IntPtr) 1;
                break;
              }
              break;
            case 39:
              if (flag2)
              {
                if (!flag1)
                  GameEngine.scrollRight = true;
                if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_MAIL && !flag1)
                  return (IntPtr) 1;
                break;
              }
              break;
            case 40:
              if (flag2)
              {
                if (!flag1)
                  GameEngine.scrollDown = true;
                if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_MAIL && !flag1)
                  return (IntPtr) 1;
                break;
              }
              break;
            case 122:
              GameEngine.f11Pressed = true;
              break;
            case 160:
            case 161:
              GameEngine.shiftPressed = true;
              break;
            case 162:
            case 163:
              GameEngine.Instance.GFX.keyControlled = true;
              break;
          }
        }
      }
      else
      {
        GameEngine.lastKeyPressed = 0;
        if (nCode >= 0 && wParam == (IntPtr) 257)
        {
          switch (Marshal.ReadInt32(lParam))
          {
            case 9:
              GameEngine.tabPressed = false;
              GameEngine.tabReleased = true;
              break;
            case 37:
              GameEngine.scrollLeft = false;
              break;
            case 38:
              GameEngine.scrollUp = false;
              break;
            case 39:
              GameEngine.scrollRight = false;
              break;
            case 40:
              GameEngine.scrollDown = false;
              break;
            case 160:
            case 161:
              GameEngine.shiftPressed = false;
              GameEngine.shiftPressedAlways = false;
              break;
            case 162:
            case 163:
              GameEngine.Instance.GFX.keyControlled = false;
              break;
          }
        }
      }
      return GameEngine.CallNextHookEx(GameEngine._hookID, nCode, wParam, lParam);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(
      int idHook,
      GameEngine.LowLevelKeyboardProc lpfn,
      IntPtr hMod,
      uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(
      IntPtr hhk,
      int nCode,
      IntPtr wParam,
      IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    public static string getCachePath()
    {
      string path = Path.Combine(GameEngine.getSettingsPath(false), "BrowserCache");
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      return path;
    }

    public static string getSettingsPath(bool createFolder)
    {
      if (GameEngine.userPath == null)
      {
        FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        GameEngine.userPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        GameEngine.userPath += "\\";
        GameEngine.userPath += versionInfo.CompanyName;
        GameEngine.userPathBase = GameEngine.userPath;
        GameEngine.userPath += "\\";
        GameEngine.userPath += versionInfo.ProductName;
      }
      try
      {
        if (Directory.Exists(GameEngine.userPath) || !createFolder)
        {
          GameEngine.updateFolderPermissions(GameEngine.userPath);
          return GameEngine.userPath;
        }
        Directory.CreateDirectory(GameEngine.userPathBase);
        Directory.CreateDirectory(GameEngine.userPath);
        GameEngine.updateFolderPermissions(GameEngine.userPath);
      }
      catch (Exception ex)
      {
      }
      return GameEngine.userPath;
    }

    public static string getLangsPath()
    {
      if (GameEngine.langPath == null)
      {
        GameEngine.langPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Stronghold Kingdoms\\";
        try
        {
          Directory.CreateDirectory(GameEngine.langPath);
        }
        catch (Exception ex)
        {
        }
      }
      return GameEngine.langPath;
    }

    public enum GameDisplays
    {
      DISPLAY_VILLAGE,
      DISPLAY_WORLD,
      DISPLAY_REPORTS,
      DISPLAY_CASTLE,
      DISPLAY_RANKINGS,
      DISPLAY_RESEARCH,
      DISPLAY_ARMIES,
      DISPLAY_MAIL,
      DISPLAY_ELECTIONS,
      DISPLAY_POLITICS_VOTE,
      DISPLAY_POLITICS_FORUM,
      DISPLAY_AVATAR_EDITOR,
      DISPLAY_FACTIONS,
      DISPLAY_WEB,
      DISPLAY_LEADERBOARD,
      DISPLAY_QUESTS,
      DISPLAY_TEMP_DUMMY,
      DISPLAY_USER_INFO,
      DISPLAY_ALL_VILLAGES,
      DISPLAY_CONTESTS_LEADERBOARD,
      DISPLAY_CONTESTS_HISTORY,
    }

    public enum GameDisplaySubModes
    {
      SUBMODE_DEFAULT,
      SUBMODE_CASTLE_ATTACKER_SETUP,
      SUBMODE_BATTLE,
      SUBMODE_PREVIEW,
    }

    public delegate void ConvertVillageDelegate();

    public class MouseClickMessageFilter : IMessageFilter
    {
      public bool PreFilterMessage(ref Message m) => false;
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private delegate IntPtr MessageTextProc(int nCode, IntPtr wParam, IntPtr lParam);
  }
}
