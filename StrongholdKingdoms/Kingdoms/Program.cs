// Decompiled with JetBrains decompiler
// Type: Kingdoms.Program
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Gecko;
using StatTracking;
using Stronghold.AuthClient;
using Stronghold.ShieldClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  internal static class Program
  {
    public static int CurrentInstallerBuild = 116;
    public static int steam_SessionTicketUserID = 0;
    public static byte[] steam_SessionTicket = (byte[]) null;
    public static bool steamActive = false;
    public static string steamID = string.Empty;
    public static string steamEmail = string.Empty;
    public static bool kingdomsAccountFound = false;
    public static string installedLangCode = "";
    public static bool steamInstall = false;
    public static bool bigpointInstall = false;
    public static bool bigpointPartnerInstall = false;
    public static bool aeriaInstall = false;
    public static bool gamersFirstInstall = false;
    public static bool gamersFirstError = false;
    public static bool arcInstall = false;
    public static bool arc_overlay_open = false;
    public static int arc_overlay_delay = 0;
    public static string arcToken = "";
    public static string arcUsername = "";
    public static bool arcLauncherStart = false;
    public static bool winStore = false;
    private static bool xmas_period = false;
    public static List<SKLang> communityLangs = new List<SKLang>();
    public static ProfileLoginWindow profileLogin = (ProfileLoginWindow) null;
    private static uint timerPeriod = 1;
    public static MySettings mySettings = (MySettings) null;
    public static string WorldName = string.Empty;
    public static string syslang = "";
    public static string gamersFirstTokenMD5 = "";
    private static uint LOAD_WITH_ALTERED_SEARCH_PATH = 8;
    private static string[] geckoFXDlls = new string[22]
    {
      "sqlite3.dll",
      "nspr4.dll",
      "softokn3.dll",
      "AccessibleMarshal.dll",
      "freebl3.dll",
      "IA2Marshal.dll",
      "ssl3.dll",
      "js3250.dll",
      "javaxpcomglue.dll",
      "nss3.dll",
      "mozctl.dll",
      "mozcrt19.dll",
      "mozctlx.dll",
      "nssdbm3.dll",
      "nssckbi.dll",
      "smime3.dll",
      "nssutil3.dll",
      "plc4.dll",
      "plds4.dll",
      "xpcom.dll",
      "xul.dll",
      "plugins\\npnul32.dll"
    };
    private static List<IntPtr> loadedDLLs = new List<IntPtr>();
    public static bool steamOverlayActive = false;
    private static bool lastOverlayState = false;
    private static bool arc_initialised = false;
    private static Form arc_form = (Form) null;
    private static bool last_arc_overlay_open = false;
    private static bool arc_delayedNextState = false;
    public static int arc_delayedManualOpening = -1;

    [DllImport("winmm.dll")]
    internal static extern uint timeBeginPeriod(uint period);

    [DllImport("winmm.dll")]
    internal static extern uint timeEndPeriod(uint period);

    [DllImport("winmm.dll")]
    private static extern int timeGetDevCaps(ref Program.TimerCaps caps, int sizeOfTimerCaps);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetProcessDPIAware();

    [DllImport("ArcWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Arc_Init(IntPtr handle);

    [DllImport("ArcWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Arc_Exit();

    [DllImport("ArcWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr Arc_GetAuthToken(IntPtr username);

    [DllImport("ArcWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Arc_Overlay(int state);

    [DllImport("ArcWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Arc_OpenURL(IntPtr url);

    [DllImport("ArcWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Arc_LaunchClient();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Steam_Init();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr Steam_GetAuthTicket();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Steam_GetAuthTicketLength();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Steam_RunCallBacks();

    [DllImport("SteamWrap3.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr Steam_User_GetPersonaName();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong Steam_User_GetLocalUserSteamID();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Steam_OverlayActive();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Steam_OpenOverlay();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Steam_IsOverlayEnabled();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Steam_MT_Clear_Response();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Steam_MT_Authorised();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong Steam_MT_OrderID();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint Steam_MT_AppID();

    [DllImport("SteamWrap3.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Steam_MT_Got_Response();

    public static bool ShowSeasonalFX
    {
      get => Program.xmas_period && Program.mySettings.SeasonalSpecialFX;
    }

    public static bool ShowSeasonalGraphics
    {
      get => Program.xmas_period && Program.mySettings.SeasonalWinterLandscape;
    }

    public static bool ShowSeasonalFXOption => Program.xmas_period;

    public static bool testMutex()
    {
      try
      {
        IpcClientChannel chnl = new IpcClientChannel();
        ChannelServices.RegisterChannel((IChannel) chnl, false);
        SHKMutex shkMutex = (SHKMutex) Activator.GetObject(typeof (SHKMutex), "ipc://SHKMutex/KingdomsRemoteObj");
        try
        {
          if (shkMutex.HelloWorld() == "Hello World")
            return true;
        }
        catch (Exception ex)
        {
        }
        ChannelServices.UnregisterChannel((IChannel) chnl);
        ChannelServices.RegisterChannel((IChannel) new IpcServerChannel("SHKMutex"), false);
        RemotingConfiguration.RegisterWellKnownServiceType(typeof (SHKMutexObject), "KingdomsRemoteObj", WellKnownObjectMode.SingleCall);
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    [STAThread]
    private static void Main(string[] args)
    {
      bool flag1 = false;
      bool flag2 = false;
      string lang = "en";
      if (args == null || args.Length < 1)
        flag1 = true;
      if (args != null && args.Length > 1)
      {
        if (args[0].ToLowerInvariant() == "-installerversion")
        {
          if (Convert.ToInt32(args[1]) < Program.CurrentInstallerBuild)
            flag2 = true;
        }
        else if (!(args[0].ToLowerInvariant() == "-installer"))
          flag1 = true;
        if (args.Length > 2)
        {
          if (args[2].Length > 0)
          {
            lang = args[2];
            switch (lang)
            {
              case "sc":
                lang = "zh";
                break;
              case "tc":
                lang = "zhhk";
                break;
            }
          }
          if (args.Length > 3 && args[3].Length > 0)
          {
            if (args[3] == "st")
              Program.steamInstall = true;
            if (args[3] == "bp")
              Program.bigpointInstall = true;
            if (args[3] == "bp2")
              Program.bigpointPartnerInstall = true;
            if (args[3] == "ws")
              Program.winStore = true;
            if (args[3] == "ae")
              Program.aeriaInstall = true;
            if (args[3] == "gf")
            {
              Program.gamersFirstInstall = true;
              if (args.Length > 4 && args[4].Length > 0)
                Program.gamersFirstTokenMD5 = args[4];
            }
            if (args[3] == "arc")
            {
              if (args.Length > 4)
              {
                Program.arcUsername = args[4];
                Program.arcInstall = true;
                if (Program.arcUsername.Length <= 0)
                  Program.arcLauncherStart = true;
              }
              else
                Program.arcLauncherStart = true;
            }
          }
        }
      }
      else
        flag2 = true;
      Program.xmas_period = HolidayPeriods.xmas(DateTime.Now);
      if (Program.arcLauncherStart)
        Program.arc_launchClient(lang);
      else if (flag1)
      {
        int num1 = (int) MessageBox.Show(SK.Text("ProgramMain_Launch_Failure1", "This is not the game exe!") + Environment.NewLine + Environment.NewLine + SK.Text("ProgramMain_Launch_Failure2", "Please run Stronghold Kingdoms in the normal manner."), SK.Text("ProgramMain_Launch_Failure", "Stronghold Kingdoms Error"), MessageBoxButtons.OK);
      }
      else if (flag2 && !Program.steamInstall)
      {
        MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
        if (MessageBox.Show(SK.Text("ProgramMain_New_nInstaller", "A new version of the Updater/Installer is needed") + Environment.NewLine + SK.Text("ProgramMain_Must_Install", "You cannot Launch Stronghold Kingdoms until this is installed") + Environment.NewLine + Environment.NewLine + SK.Text("ProgramMain_Install_Now", "Do you wish to install this now?"), SK.Text("ProgramMain_Installer_Update", "Stronghold Kingdoms Installer Update"), buttons) != DialogResult.OK)
          return;
        string path = InstallerUpdater.downloadSelfUpdater(new Uri("http://static.strongholdkingdoms.com/Kingdoms/kingdoms-setup-update-" + Program.CurrentInstallerBuild.ToString() + ".exe"));
        if (path == null || path.Length <= 0)
          return;
        InstallerUpdater.runInstaller(path);
      }
      else
      {
        bool createdNew;
        using (new Mutex(true, "Global\\StrongholdKingdoms", out createdNew))
        {
          try
          {
            OperatingSystem osVersion = Environment.OSVersion;
            if (osVersion.Platform == PlatformID.Win32NT)
            {
              if (osVersion.Version.Major >= 6)
                Program.SetProcessDPIAware();
            }
          }
          catch (Exception ex)
          {
          }
          AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
          Application.ThreadException += new ThreadExceptionEventHandler(Program.CurrentDomain_ThreadException);
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);
          Program.communityLangs = SKLocalization.scanForLanguages(GameEngine.getLangsPath());
          Program.installedLangCode = lang;
          Program.mySettings = MySettings.load();
          if (Program.mySettings.LanguageIdent.Length == 0)
            Program.mySettings.LanguageIdent = lang;
          else if (Program.mySettings.InstalledLanguageIdent != lang)
          {
            Program.mySettings.LanguageIdent = lang;
            Program.mySettings.InstalledLanguageIdent = lang;
          }
          Program.syslang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
          if (!Program.mySettings.OwnLanguageAvailableAndChecked)
          {
            string str1 = Program.syslang;
            if (str1 == "ja")
              str1 = "jp";
            if (str1 != Program.mySettings.LanguageIdent)
            {
              switch (str1)
              {
                case "en":
                  Program.mySettings.OwnLanguageAvailableAndChecked = true;
                  break;
                case "de":
                case "fr":
                case "ru":
                case "es":
                case "pl":
                case "it":
                case "tr":
                case "pt":
                case "zh":
                case "zhhk":
                case "ko":
                case "jp":
                  string str2 = SK.Text("ProgramMain_A_New_Language", "A New Language is available : ");
                  switch (str1)
                  {
                    case "de":
                      str2 += "Deutsch";
                      break;
                    case "fr":
                      str2 += "Français";
                      break;
                    case "ru":
                      str2 += "Русский";
                      break;
                    case "es":
                      str2 += "Español";
                      break;
                    case "pl":
                      str2 += "Polski";
                      break;
                    case "it":
                      str2 += "Italiano";
                      break;
                    case "tr":
                      str2 += "Türkçe";
                      break;
                    case "pt":
                      str2 += "Português do Brasil";
                      break;
                    case "zh":
                      str2 += "简体中文";
                      break;
                    case "zhhk":
                      str2 += "繁體中文";
                      break;
                    case "ko":
                      str2 += "한국어";
                      break;
                    case "jp":
                      str2 += "日本語";
                      break;
                  }
                  if (MessageBox.Show(str2 + Environment.NewLine + SK.Text("ProgramMain_Use_New_Language", "Your system settings indicate you are using this language, do you wish to play Stronghold Kingdoms in this language?"), SK.Text("ProgramMain_NewLanguageAvailable", "New Language Available"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Program.mySettings.LanguageIdent = str1;
                  Program.mySettings.OwnLanguageAvailableAndChecked = true;
                  break;
              }
            }
            else
              Program.mySettings.OwnLanguageAvailableAndChecked = true;
          }
          switch (Program.mySettings.LanguageIdent)
          {
            case "en":
            case "de":
            case "fr":
            case "ru":
            case "es":
            case "pl":
            case "pt":
            case "tr":
            case "it":
            case "zh":
            case "zhhk":
            case "ko":
            case "jp":
              SKLocalization.LoadLocalization(Application.StartupPath + "\\Localization\\", Program.mySettings.LanguageIdent);
              break;
            default:
              bool flag3 = false;
              foreach (SKLang communityLang in Program.communityLangs)
              {
                if (communityLang.id == Program.mySettings.LanguageIdent)
                {
                  SKLocalization.LoadLocalization(GameEngine.getLangsPath(), communityLang.id);
                  flag3 = SKLocalization.Instance != null && SKLocalization.Instance.valid;
                  break;
                }
              }
              if (!flag3)
              {
                Program.mySettings.LanguageIdent = "en";
                SKLocalization.LoadLocalization(Application.StartupPath + "\\Localization\\", Program.mySettings.LanguageIdent);
                break;
              }
              break;
          }
          if (Program.steamInstall)
          {
            bool flag4 = false;
            try
            {
              if (Program.Steam_Init() > 0)
              {
                Program.steamActive = true;
                Program.Steam_getTicket();
                XmlRpcAuthRequest req = new XmlRpcAuthRequest("", "", "", "", BitConverter.ToString(Program.steam_SessionTicket).Replace("-", ""), "", "", "");
                XmlRpcAuthResponse xmlRpcAuthResponse = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).AuthenticateSteamAccount((IAuthRequest) req, (AuthEndResponseDelegate) null, (Control) null, 15000);
                int? successCode = xmlRpcAuthResponse.SuccessCode;
                if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
                {
                  flag4 = true;
                  Program.steamID = xmlRpcAuthResponse.Message;
                  Program.steamEmail = xmlRpcAuthResponse.UserGUID;
                  Program.mySettings.AutoLogin = false;
                  Program.kingdomsAccountFound = Program.steamEmail.Trim().Length > 0;
                }
              }
            }
            catch (Exception ex)
            {
              Program.steamActive = false;
            }
            if (!flag4)
            {
              int num2 = (int) MessageBox.Show(SK.Text("Steam_steam_required", "Stronghold Kingdoms requires the Steam Client to be running in Online mode."), SK.Text("Steam_error", "Steam Error"));
              Application.Exit();
              return;
            }
          }
          if (Program.arcInstall && !Program.arc_login(Program.arcUsername))
            return;
          if (Program.gamersFirstInstall && Program.gamersFirstTokenMD5.Length == 0)
          {
            int num3 = (int) MessageBox.Show(SK.Text("GF_token_error", "Unable to verify your GamersFirst identity. Please try again. If this issue persists, please contact support."), SK.Text("GF_Error", "GamersFirst Error"));
            Application.Exit();
          }
          else
          {
            LoadingPanel loadingPanel1 = new LoadingPanel();
            loadingPanel1.init();
            loadingPanel1.Show();
            loadingPanel1.TopMost = true;
            loadingPanel1.BringToFront();
            loadingPanel1.Focus();
            loadingPanel1.BringToFront();
            loadingPanel1.TopMost = false;
            Xpcom.Initialize(Application.StartupPath + "\\geckofx\\xulrunner33");
            bool flag5 = Program.testMutex();
            if (!createdNew || flag5)
            {
              int num4 = (int) MessageBox.Show(SK.Text("ProgramMain_Already_Running", "Already running") + "...", "Stronghold Kingdoms");
            }
            else
            {
              bool flag6 = true;
              Program.TimerCaps caps = new Program.TimerCaps();
              Program.timeGetDevCaps(ref caps, Marshal.SizeOf((object) caps));
              Program.timerPeriod = Math.Max(caps.periodMin, 1U);
              int num5 = (int) Program.timeBeginPeriod(Program.timerPeriod);
              DXTimer.Init();
              GameEngine gameEngine;
              try
              {
                gameEngine = new GameEngine();
              }
              catch (FileNotFoundException ex)
              {
                if (ex.FileName.Contains("irectX"))
                  GameEngine.displayDirectXError();
                int num6 = (int) Program.timeEndPeriod(Program.timerPeriod);
                loadingPanel1.Close();
                return;
              }
              GraphicsMgr mgr = new GraphicsMgr();
              int maxRes = 2;
              System.Windows.Forms.Screen primaryScreen = System.Windows.Forms.Screen.PrimaryScreen;
              int width1 = primaryScreen.Bounds.Width;
              int height1 = primaryScreen.Bounds.Height;
              if (width1 < 1024 || height1 < 768)
              {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                loadingPanel1.Close();
                loadingPanel1 = (LoadingPanel) null;
                if (MessageBox.Show(SK.Text("ProgramMain_Screen_Too_Small", "Your screen resolution is too small to run Stronghold Kingdoms") + Environment.NewLine + Environment.NewLine + SK.Text("ProgramMain_Try_Anyway", "Try to anyway?"), SK.Text("ProgramMain_Error", "Error"), buttons) != DialogResult.Yes)
                {
                  int num7 = (int) Program.timeEndPeriod(Program.timerPeriod);
                  return;
                }
              }
              int num8 = width1 - 80;
              int num9 = height1 - 100;
              if (num8 < 944)
                num8 = 944;
              if (num9 < 668)
                num9 = 668;
              Program.mySettings.Save();
              CastleMap.displayCollapsed = Program.mySettings.CastleWalls;
              Form form = (Form) new MainWindow();
              form.Visible = false;
              if (Program.arcInstall)
                Program.arc_init(form);
              ((MainWindow) form).allowResizing(false);
              int width2 = num8;
              int height2 = num9;
              if (Program.mySettings.ScreenWidth > 0)
                width2 = Program.mySettings.ScreenWidth;
              if (Program.mySettings.ScreenHeight > 0)
                height2 = Program.mySettings.ScreenHeight;
              if (width2 > width1)
                width2 = width1;
              if (height2 > height1)
                height2 = height1;
              if (flag6)
              {
                form.MaximumSize = new Size(3840, 2160);
                form.ClientSize = new Size(width2, height2);
              }
              else
              {
                form.ClientSize = new Size(1000, 720);
                form.MaximumSize = new Size(1050, 760);
              }
              if (Program.mySettings.Maximize)
                form.WindowState = FormWindowState.Maximized;
              form.Text = "Stronghold Kingdoms";
              ((MainWindow) form).allowResizing(true);
              MainWindow newParentMainWindow = (MainWindow) form;
              InterfaceMgr.Instance.registerForm(form, newParentMainWindow);
              LoadingPanel loadingPanel2;
              if (!gameEngine.Initialise(mgr, maxRes, 2))
              {
                if (loadingPanel1 == null)
                  return;
                loadingPanel1.Close();
                loadingPanel2 = (LoadingPanel) null;
              }
              else
              {
                SVG_Source instance = SVG_Source.Instance;
                Sound.setMusicState(Program.mySettings.Music);
                GameEngine.Instance.AudioEngine.setMP3MasterVolume((float) Program.mySettings.MusicVolume / 100f, 0);
                Sound.setSFXState(Program.mySettings.SFX);
                Sound.setBattleSFXState(Program.mySettings.BattleSFX);
                GameEngine.Instance.AudioEngine.setSFXMasterVolume((float) Program.mySettings.SFXVolume / 100f);
                Sound.setEnvironmentalState(Program.mySettings.Environmentals);
                GameEngine.Instance.AudioEngine.setEnvironmentalMasterVolume((float) Program.mySettings.EnvironmentalVolume / 100f);
                bool flag7 = true;
                if (loadingPanel1 != null)
                {
                  loadingPanel1.Close();
                  loadingPanel2 = (LoadingPanel) null;
                }
                RemoteServices.Instance.initChannel();
                while (flag7)
                {
                  gameEngine.reLogin();
                  flag7 = false;
                  RemoteServices.Instance.UserID = -1;
                  RemoteServices.Instance.set_CommonData_UserCallBack((RemoteServices.CommonData_UserCallBack) null);
                  while (RemoteServices.Instance.UserID < 0)
                  {
                    gameEngine.installKeyboardHook();
                    GameEngine.Instance.reLogin();
                    GameEngine.Instance.clearServerDowntime();
                    Program.profileLogin = gameEngine.getLoginWindow();
                    if (Program.profileLogin == null)
                    {
                      Program.profileLogin = new ProfileLoginWindow();
                      GameEngine.Instance.setProfileLogin(Program.profileLogin);
                      Program.profileLogin.Show();
                      Program.profileLogin.init();
                    }
                    else
                      Program.profileLogin.openAfterCancel();
                    RemoteServices.Instance.clearQueues();
                    while (Program.profileLogin.Created && Program.profileLogin.UserEntryMode)
                    {
                      RemoteServices.Instance.processData();
                      Thread.Sleep(1);
                      Program.DoEvents();
                      Program.profileLogin.update();
                      StatTrackingClient.Instance().Update(0.01);
                    }
                    GameEngine.Instance.reLogin();
                    form.Text = "Stronghold Kingdoms";
                    if (Program.WorldName != string.Empty)
                      form.Text = form.Text + " - " + Program.WorldName;
                    if (RemoteServices.Instance.UserID == -1)
                    {
                      GameEngine.Instance.killLoadThread();
                      Program.mySettings.Maximize = form.WindowState == FormWindowState.Maximized;
                      form.Close();
                      Program.shutdown();
                      return;
                    }
                  }
                  gameEngine.showConnectingPopup();
                  gameEngine.World.loadLocalWorldData();
                  gameEngine.World.updateWorldMapOwnership();
                  bool flag8 = true;
                  while (flag8)
                  {
                    flag8 = false;
                    VillageMap.loadVillageBuildingsGFX();
                    while (gameEngine.isStillLoading())
                    {
                      Thread.Sleep(10);
                      Program.DoEvents();
                      RemoteServices.Instance.processData();
                      GameEngine.Instance.updateConnectingPopup();
                    }
                    GameEngine.Instance.World.initSprites(GameEngine.Instance.GFX);
                    GameEngine.Instance.resumeCommonRemote();
                    gameEngine.enableConnectingPopup();
                    while (!GameEngine.Instance.World.isDownloadComplete())
                    {
                      Thread.Sleep(10);
                      Application.DoEvents();
                      RemoteServices.Instance.processData();
                      GameEngine.Instance.updateConnectingPopup();
                      if (gameEngine.loginCancelled())
                        break;
                    }
                    if (gameEngine.pendingError())
                    {
                      gameEngine.updateConnectingPopup();
                      gameEngine.forceRelogin();
                    }
                    if (!gameEngine.loginCancelled())
                    {
                      gameEngine.World.saveFactionData();
                      gameEngine.World.saveNamesData();
                      gameEngine.enableConnectingPopup2();
                      while (gameEngine.waitForConnectingPopupToClose())
                      {
                        Thread.Sleep(10);
                        Program.DoEvents();
                        RemoteServices.Instance.processData();
                        if (gameEngine.loginCancelled())
                          break;
                      }
                    }
                    if (RemoteServices.Instance.UserID == -1)
                    {
                      GameEngine.Instance.killLoadThread();
                      Program.mySettings.Maximize = form.WindowState == FormWindowState.Maximized;
                      form.Close();
                      Program.shutdown();
                      return;
                    }
                    Sound.playMusic();
                    bool flag9 = false;
                    if (!gameEngine.reLogin())
                    {
                      InterfaceMgr.Instance.setupVillageName();
                      form.Show();
                      form.Visible = true;
                      newParentMainWindow.MainWindowLarge_SizeChanged((object) null, (EventArgs) null);
                      GameEngine.Instance.lateStart();
                      if (GameEngine.Instance.World.numVillagesOwned() > 0 && RemoteServices.Instance.ShowAdminMessage)
                        AdminInfoPopup.showMessage();
                      while (form.Created)
                      {
                        gameEngine.run();
                        if (gameEngine.reLogin())
                        {
                          form.Hide();
                          form.Visible = false;
                          gameEngine.windowClosing();
                          if (!gameEngine.quitting())
                            flag7 = true;
                          flag9 = true;
                          break;
                        }
                        StatTrackingClient.Instance().Update(0.01);
                        RemoteServices.Instance.processData();
                        if (form.Created)
                          Thread.Sleep(1);
                      }
                    }
                    else
                    {
                      flag7 = true;
                      flag9 = true;
                    }
                    if (!flag9)
                    {
                      form.Hide();
                      form.Visible = false;
                      form = (Form) null;
                      newParentMainWindow = (MainWindow) null;
                    }
                    gameEngine.World.saveFactionData();
                    gameEngine.World.saveNamesData();
                    Sound.stopMusic();
                  }
                }
                try
                {
                  if (form != null)
                    Program.mySettings.Maximize = form.WindowState == FormWindowState.Maximized;
                }
                catch (Exception ex)
                {
                }
                Program.shutdown();
              }
            }
          }
        }
      }
    }

    public static void DoEvents()
    {
      Application.DoEvents();
      CustomSelfDrawPanel.processInvalidRectCache();
    }

    private static void shutdown()
    {
      Program.mySettings.CastleWalls = CastleMap.displayCollapsed;
      StatTrackingClient.Instance().Terminate();
      Program.mySettings.Save();
      RemoteServices.Instance.LogOut(false, false, false, false, false, false, false, 0, 100, false, false, false, false, false, false, 500, 500, 500, 500, 250);
      GameEngine.Instance.uninstallKeyboardHook();
      Thread.Sleep(1000);
      Program.unloadGeckoDLLs();
      int num = (int) Program.timeEndPeriod(Program.timerPeriod);
      if (GameEngine.Instance.AudioEngine != null)
        GameEngine.Instance.AudioEngine.closeAudio();
      if (!Program.arcInstall)
        return;
      Program.Arc_Exit();
    }

    private static void CurrentDomain_UnhandledException(
      object sender,
      UnhandledExceptionEventArgs e)
    {
      Exception exceptionObject = e.ExceptionObject as Exception;
      int num = (int) MessageBox.Show(SK.Text("ProgramMain_Unexpected_Error", "There has been an unexpected error. Please forward a copy of this report to Support.") + "\n\n" + exceptionObject.Message + "\n\n" + exceptionObject.ToString(), SK.Text("ProgramMain_SK_Error", "Stronghold Kingdoms Error"));
      Application.Exit();
    }

    private static void CurrentDomain_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
      Exception exception = e.Exception;
      int num = (int) MessageBox.Show(SK.Text("ProgramMain_Unexpected_Error", "There has been an unexpected error. Please forward a copy of this report to Support.") + "\n\n" + exception.Message + "\n\n" + exception.ToString(), SK.Text("ProgramMain_SK_Error", "Stronghold Kingdoms Error"));
      Application.Exit();
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibraryEx(string dllFilePath, IntPtr hFile, uint dwFlags);

    [DllImport("kernel32.dll")]
    public static extern bool FreeLibrary(IntPtr dllPointer);

    private static void loadGeckoDLLs(string basePath)
    {
      foreach (string geckoFxDll in Program.geckoFXDlls)
      {
        IntPtr num = Program.LoadWin32Library(basePath + "\\" + geckoFxDll);
        if (num != IntPtr.Zero)
          Program.loadedDLLs.Add(num);
      }
    }

    private static void unloadGeckoDLLs()
    {
      foreach (IntPtr loadedDlL in Program.loadedDLLs)
        Program.FreeLibrary(loadedDlL);
    }

    public static IntPtr LoadWin32Library(string dllFilePath)
    {
      try
      {
        IntPtr num1 = Program.LoadLibraryEx(dllFilePath, IntPtr.Zero, Program.LOAD_WITH_ALTERED_SEARCH_PATH);
        int num2 = num1 == IntPtr.Zero ? 1 : 0;
        return num1;
      }
      catch (Exception ex)
      {
      }
      return IntPtr.Zero;
    }

    public static void OLActive()
    {
      Program.steamOverlayActive = true;
      InterfaceMgr.Instance.closeAllPopups();
      InterfaceMgr.Instance.ParentMainWindow.makeFullDX();
    }

    public static void OLInactive()
    {
      Program.steamOverlayActive = false;
      InterfaceMgr.Instance.ParentMainWindow.restoreDXSize();
    }

    public static unsafe void Steam_getTicket()
    {
      IntPtr authTicket = Program.Steam_GetAuthTicket();
      int authTicketLength = Program.Steam_GetAuthTicketLength();
      if (authTicketLength == -1)
        return;
      byte* pointer = (byte*) authTicket.ToPointer();
      Program.steam_SessionTicket = new byte[authTicketLength];
      for (int index = 0; index < authTicketLength; ++index)
        Program.steam_SessionTicket[index] = pointer[index];
    }

    public static unsafe string GetLocalUserName()
    {
      if (!Program.steamActive)
        return "";
      byte* pointer = (byte*) Program.Steam_User_GetPersonaName().ToPointer();
      int count = 0;
      while (pointer[count] != (byte) 0)
        ++count;
      byte[] bytes = new byte[count];
      for (int index = 0; pointer[index] != (byte) 0; ++index)
        bytes[index] = pointer[index];
      return Encoding.UTF8.GetString(bytes, 0, count);
    }

    public static ulong Steam_GetSteamUserID()
    {
      return Program.steamActive ? Program.Steam_User_GetLocalUserSteamID() : 0UL;
    }

    public static bool Steam_PollOverlayStatus()
    {
      return Program.steamActive && Program.Steam_OverlayActive() > 0;
    }

    public static void forceSteamDXOverlay()
    {
      if (InterfaceMgr.Instance.isDXVisible())
        return;
      InterfaceMgr.Instance.changeTab(9);
      InterfaceMgr.Instance.changeTab(0);
      Program.lastOverlayState = true;
      Program.OLActive();
    }

    public static void steam_run()
    {
      if (!Program.steamActive)
        return;
      Program.Steam_RunCallBacks();
      bool lastOverlayState = Program.lastOverlayState;
      if (Program.Steam_OverlayActive() > 0)
      {
        if (!Program.lastOverlayState)
        {
          Program.lastOverlayState = true;
          Program.OLActive();
        }
      }
      else if (Program.lastOverlayState)
      {
        Program.lastOverlayState = false;
        Program.OLInactive();
      }
      if (!Program.lastOverlayState && !lastOverlayState && GameEngine.tabPressed && GameEngine.shiftPressed && Program.Steam_IsOverlayEnabled() > 0 && !InterfaceMgr.Instance.isDXVisible())
      {
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
        Program.Steam_OpenOverlay();
      }
      if (Program.Steam_MT_Got_Response() <= 0)
        return;
      bool flag = Program.Steam_MT_Authorised() == 1;
      ulong num1 = Program.Steam_MT_OrderID();
      int num2 = (int) Program.Steam_MT_AppID();
      Program.Steam_MT_Clear_Response();
      if (!flag)
        return;
      int? successCode = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).SteamPaymentFinal((IAuthRequest) new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), (string) null, (string) null, (string) null)
      {
        OrderID = num1.ToString()
      }, (AuthEndResponseDelegate) null, (Control) null, 15000).SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        int num3 = (int) MyMessageBox.Show(SK.Text("STEAM_AUTHORIZED_OK", "Your payment was received and your crowns have been credited - Thank You!"));
      }
      else
      {
        int num4 = (int) MyMessageBox.Show(SK.Text("STEAM_AUTHORIZED_BAD", "There was a problem processing your payment through Steam, please contact support."));
      }
    }

    private static string readLocalTxt()
    {
      string str1 = "en";
      StreamReader streamReader = (StreamReader) null;
      try
      {
        streamReader = File.OpenText(Application.StartupPath + "\\local.txt");
        str1 = streamReader.ReadLine();
        streamReader.Close();
      }
      catch (Exception ex1)
      {
        try
        {
          streamReader?.Close();
        }
        catch (Exception ex2)
        {
        }
      }
      string str2 = str1.Substring(0, 2);
      if (str2 != "en" && str2 != "fr" && str2 != "de" && str2 != "ru" && str2 != "es")
        str2 = "en";
      return str2;
    }

    public static bool arc_login(string username)
    {
      if (Program.arcInstall)
      {
        Program.arcUsername = username;
        if (Program.Arc_Init(IntPtr.Zero) == 0)
        {
          int num = (int) MessageBox.Show(SK.Text("ARC_Init_Error", "Unable to initialize Arc"), SK.Text("ARC_Error", "Arc Error"));
          return false;
        }
        Program.arc_initialised = true;
        Program.arcToken = Program.arc_getTicket(username);
        if (Program.arcToken == "")
        {
          int num = (int) MessageBox.Show(SK.Text("ARC_Login_Error", "Unable to login with user") + " : " + username, SK.Text("ARC_Error", "Arc Error"));
          return false;
        }
        Program.Arc_Exit();
      }
      return true;
    }

    public static string getNewArcToken()
    {
      if (Program.arc_initialised)
        return Program.arc_getTicket(Program.arcUsername);
      if (Program.Arc_Init(IntPtr.Zero) == 0)
        return "";
      string ticket = Program.arc_getTicket(Program.arcUsername);
      Program.Arc_Exit();
      return ticket;
    }

    public static void arc_exit()
    {
      if (!Program.arcInstall)
        return;
      if (Program.arc_initialised)
        Program.Arc_Exit();
      Program.arc_initialised = false;
    }

    public static void arc_init(Form form)
    {
      if (!Program.arcInstall || Program.Arc_Init(form.Handle) == 0)
        return;
      Program.arc_form = form;
      Program.arc_initialised = true;
      Program.arc_overlay_open = false;
      Program.last_arc_overlay_open = false;
      Program.arc_overlay_delay = 0;
    }

    private static unsafe string arc_getTicket(string username)
    {
      List<char> charList1 = new List<char>();
      foreach (char ch in username)
        charList1.Add(ch);
      IntPtr authToken = Program.Arc_GetAuthToken(Marshal.UnsafeAddrOfPinnedArrayElement((Array) charList1.ToArray(), 0));
      if (!(authToken != (IntPtr) 0))
        return "";
      char* pointer = (char*) authToken.ToPointer();
      List<char> charList2 = new List<char>();
      for (int index = 0; index < 99 && pointer[index] != char.MinValue; ++index)
        charList2.Add(pointer[index]);
      return new string(charList2.ToArray());
    }

    public static void arc_openURL(string url)
    {
      List<char> charList = new List<char>();
      foreach (char ch in url)
        charList.Add(ch);
      IntPtr url1 = Marshal.UnsafeAddrOfPinnedArrayElement((Array) charList.ToArray(), 0);
      if (!InterfaceMgr.Instance.isDXVisible())
      {
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
      }
      Program.Arc_OpenURL(url1);
    }

    public static void arc_launchClient(string lang) => Program.Arc_LaunchClient();

    public static void arc_forceoverlay()
    {
      if (!InterfaceMgr.Instance.isDXVisible())
      {
        InterfaceMgr.Instance.changeTab(9);
        InterfaceMgr.Instance.changeTab(0);
      }
      InterfaceMgr.Instance.closeAllPopups();
      Program.arc_overlay_open = Program.last_arc_overlay_open = true;
      Program.arc_delayedManualOpening = 6;
      Program.arc_delayedNextState = true;
    }

    public static void arc_run()
    {
      if (!Program.arcInstall || !Program.arc_initialised || Program.arc_form == null || !Program.arc_form.Visible)
        return;
      if (Program.arc_delayedManualOpening > 0)
      {
        --Program.arc_delayedManualOpening;
        if (Program.arc_delayedManualOpening != 0)
          return;
        if (Program.arc_delayedNextState)
        {
          if (Program.arc_overlay_open)
            return;
          Program.Arc_Overlay(1);
        }
        else
        {
          if (!Program.arc_overlay_open)
            return;
          Program.Arc_Overlay(0);
        }
      }
      else if (Program.arc_overlay_delay > 0)
      {
        --Program.arc_overlay_delay;
        GameEngine.tabPressed = false;
      }
      else
      {
        if (!Program.arc_overlay_open || InterfaceMgr.Instance.isDXVisible())
          return;
        Program.arc_overlay_open = Program.last_arc_overlay_open = false;
        Program.Arc_Overlay(0);
      }
    }

    public struct TimerCaps
    {
      public uint periodMin;
      public uint periodMax;
    }
  }
}
