// Decompiled with JetBrains decompiler
// Type: Kingdoms.ProfileLoginWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Gecko;
using Gecko.Events;
using StatTracking;
using Stronghold.AuthClient;
using Stronghold.ShieldClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ProfileLoginWindow : Form
  {
    public static bool NewWorldsAvailable = false;
    private string strOnline = SK.Text("WORLD_Online", "Online");
    private string strOffline = SK.Text("WORLD_Offline", "Offline");
    private string strWorldEnded = SK.Text("WorldEnded", "This World has ended.");
    private string strJoin = SK.Text("WORLD_Join", "Join");
    private string strClosed = SK.Text("FactionInvites_Membership_closed", "Closed");
    private string strPlay = SK.Text("WORLD_Play", "Play");
    private string strSelect = SK.Text("WORLD_Select", "Select World");
    private string strEmailAddress = SK.Text("LOGIN_Email", "Email Address");
    private string strPassword = SK.Text("LOGIN_Password", "Password");
    private string strLogin = SK.Text("LOGIN_Login", "Login");
    private string strLogout = SK.Text("LogoutPanel_Logout", "Logout");
    private string strNews = SK.Text("LOGIN_News", "News");
    private string strCreateAccount = SK.Text("LOGIN_CreateAccount", "Create Account");
    private string strOptions = SK.Text("MENU_Settings", "Settings");
    private string strAccountDetails = SK.Text("LOGIN_AccountDetails", "Account Details");
    private string strGenericLoginError = SK.Text("LOGIN_GenericLoginError", "Login Failed: Please check that your email and password are entered correctly.");
    private string strGenericLoginErrorConnection = SK.Text("LOGIN_GenericLoginErrorConnection", "Login Failed: There is a problem connecting to the Login Server.");
    private string strForgottenPassword = SK.Text("LOGIN_ForgottenPassword", "Forgotten Password");
    private string strExit = SK.Text("GENERIC_Exit", "Exit");
    private string strCancel = SK.Text("GENERIC_Cancel", "Cancel");
    private string AdminGUID;
    private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
    private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);
    private Color WebButtonblue = Color.FromArgb(85, 145, 203);
    private Color WebButtonRed = Color.FromArgb(160, 0, 0);
    private Color WebButtonYellow = Color.FromArgb(225, 225, 0);
    private Color WebButtonGrey = Color.FromArgb(225, 225, 225);
    private int WebButtonWidth = 60;
    private int WebButtonheight = 22;
    private int WebButtonRadius = 10;
    private Image joinImage;
    private Image joinImageOver;
    private Image playImage;
    private Image playImageOver;
    private Image loginImage;
    private Image logoutImage;
    private Image loginImageOver;
    private Image closedImage;
    private Image closedImageOver;
    private Image logoutImageOver;
    private Image newsImage;
    private Image newsImageOver;
    private Image accountImage;
    private Image accountImageOver;
    private Image exitImage;
    private Image exitImageOver;
    private Image cancelImage;
    private Image cancelImageOver;
    private Image forgottenImage;
    private Image forgottenImageOver;
    private Image selectImage;
    private Image selectImageOver;
    private Image createAccountImage;
    private Image createAccountImageOver;
    private Image createAccountImage2;
    private Image createAccountImageOver2;
    private Image optionsImage;
    private Image optionsImageOver;
    private int worldControlWidth = 45;
    private int worldControlHeight = 24;
    private string serverAddr = string.Empty;
    private bool ignoreEmailChange;
    public static Dictionary<string, LocalizationLanguage> LanguageList;
    public static Dictionary<string, string> ShieldURL = new Dictionary<string, string>();
    public static Dictionary<string, Image> ShieldImage = new Dictionary<string, Image>();
    public CustomSelfDrawPanel LoginPanelControls_LoggedOut;
    public TextBox txtEmail;
    public CustomSelfDrawPanel.CSDLabel lblEmailSteam;
    public CustomSelfDrawPanel.CSDLabel lblEmail;
    public TextBox txtPassword;
    public CustomSelfDrawPanel.CSDLabel lblPassword;
    public CustomSelfDrawPanel.CSDImage btnLogin;
    public CustomSelfDrawPanel.CSDButton btnLoginFB;
    public CustomSelfDrawPanel.CSDLabel lblLoginError;
    public CustomSelfDrawPanel.CSDLabel lblConnectionInfo;
    public CustomSelfDrawPanel LoginPanelControls_LoggedIn;
    public CustomSelfDrawPanel.CSDImage btnClientLogout;
    public CustomSelfDrawPanel.CSDImage btnShieldDesigner;
    public CustomSelfDrawPanel.CSDImage lblUsername;
    public CustomSelfDrawPanel.CSDCheckBox chkAutoLogin;
    public CustomSelfDrawPanel.CSDLabel lblNewWorlds;
    public CustomSelfDrawPanel WorldsPanelcontrols_LoggedOut;
    public CustomSelfDrawPanel.CSDLabel lblWorldsOfflineError;
    public List<CustomSelfDrawPanel.CSDControl> loggedOutWorldControls;
    public CustomSelfDrawPanel WorldsPanelcontrols_LoggedIn;
    public CustomSelfDrawPanel.CSDLabel lblWorldsOnlineError;
    public List<CustomSelfDrawPanel.CSDControl> loggedInWorldControls;
    public CustomSelfDrawPanel BrowserTabsControls;
    public CustomSelfDrawPanel LoginPanelControls_Feedback;
    public CustomSelfDrawPanel.CSDImage exitButton;
    public CustomSelfDrawPanel.CSDImage cancelButton;
    public CustomSelfDrawPanel.CSDLabel lblRetrieving;
    public CustomSelfDrawPanel.CSDLabel lblVersion;
    public CustomSelfDrawPanel.CSDLine feedbackLine;
    public CustomSelfDrawPanel.CSDFill feedbackProgress;
    public CustomSelfDrawPanel.CSDArea feedbackProgressArea;
    private List<CustomSelfDrawPanel.CSDControl> allButtons;
    public CustomSelfDrawPanel.CSDLabel tandcLabel;
    public CustomSelfDrawPanel.CSDLabel gameRulesLabel;
    public CustomSelfDrawPanel.CSDLabel supportLabel;
    public CustomSelfDrawPanel.CSDLabel forumLabel;
    public Uri URL_landingPage;
    public Uri URL_gameWorldPage;
    public CustomSelfDrawPanel.CSDImage GreyoutWorlds;
    public CustomSelfDrawPanel.CSDImage GreyoutLogin;
    public CustomSelfDrawPanel.CSDImage GreyoutTabs;
    public CustomSelfDrawPanel.CSDButton imgFacebook = new CustomSelfDrawPanel.CSDButton();
    public CustomSelfDrawPanel.CSDButton imgTwitter = new CustomSelfDrawPanel.CSDButton();
    public CustomSelfDrawPanel.CSDButton imgYoutube = new CustomSelfDrawPanel.CSDButton();
    public CustomSelfDrawPanel.CSDButton imgSHKRu = new CustomSelfDrawPanel.CSDButton();
    private static bool successfulAutoLogin = false;
    public static string AeriaToken = string.Empty;
    /// <summary>Overrides the MAC address reported at login. Set by AdvancedLoginForm.</summary>
    public static string OverrideMAC;
    private string FacebookToken = "";
    private bool delayedCreateUserOpen;
    private bool emailOptInPopup;
    private int lastWorldLoggedIn = -1;
    public static int LastNumberOfWorldsPlaying = 0;
    public static bool inSpecialWorld = false;
    public static string specialWorldName = "";
    private static WebBrowser tempBrowser = (WebBrowser) null;
    private static string bp2_currentGuid = "";
    private int bp2_loginMode;
    private bool tempFacebookLogin;
    private bool specialFacebookLogin;
    private static bool certPolicyCreated = false;
    public static bool LoggedInViaFacebook = false;
    public static bool httpLogin = false;
    public static string gfEmail = "";
    public static string gfPW = "";
    private static string bp2_logoutURL = "";
    private string defaultWindowTitle = "";
    public string initialisedLanguage = "";
    public bool UserEntryMode = true;
    public bool connectingCancelled;
    public static string storedUserLoginEmail = "";
    private bool loginButtonActive = true;
    private bool selfClosing;
    public static string LastSelectedSupportCulture = Program.mySettings.LanguageIdent.ToLower();
    private static string lastLoadedEmail = "";
    private static string lastLoadedEmail2 = "";
    private bool emailOptInState;
    private bool OpenTOSPanel;
    private bool OpenTOSPanelIsOpen;
    public static bool CloseTOSPanel = false;
    private DateTime lastLogoutClicked = DateTime.MinValue;
    private int lastCount = -1;
    private int PlayerGameworldCount;
    public static List<WorldInfo> WorldList;
    private IContainer components;
    private Button btnExit;
    private KingdomsBrowserGecko geckoWebBrowser1;
    private Panel panel1;
    private Label label1;
    private NoDrawPanel pnlTabs;
    private NoDrawPanel pnlFeedback;
    private NoDrawPanel pnlWorlds;
    private NoDrawPanel pnlLogin;
    private KingdomsBrowserGecko browserServerNews;
    private Panel panel2;

    [DllImport("kernel32.dll")]
    private static extern ushort GetSystemDefaultLangID();

    public Image LoginImage
    {
      get
      {
        if (this.loginImage == null)
          this.loginImage = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strLogin, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.loginImage;
      }
    }

    public Image LoginImageOver
    {
      get
      {
        if (this.loginImageOver == null)
          this.loginImageOver = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strLogin, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.loginImageOver;
      }
    }

    public Image LogoutImage
    {
      get
      {
        if (this.logoutImage == null)
          this.logoutImage = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strLogout, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.logoutImage;
      }
    }

    public Image LogoutImageOver
    {
      get
      {
        if (this.logoutImageOver == null)
          this.logoutImageOver = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strLogout, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.logoutImageOver;
      }
    }

    public Image JoinImage
    {
      get
      {
        if (this.joinImage == null)
          this.joinImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strJoin, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.joinImage;
      }
    }

    public Image JoinImageOver
    {
      get
      {
        if (this.joinImageOver == null)
          this.joinImageOver = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strJoin, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.joinImageOver;
      }
    }

    public Image PlayImage
    {
      get
      {
        if (this.playImage == null)
          this.playImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strPlay, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.playImage;
      }
    }

    public Image PlayImageOver
    {
      get
      {
        if (this.playImageOver == null)
          this.playImageOver = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strPlay, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.playImageOver;
      }
    }

    public Image ClosedImage
    {
      get
      {
        if (this.closedImage == null)
          this.closedImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strClosed, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRed, this.WebButtonRadius);
        return this.closedImage;
      }
    }

    public Image SelectImage
    {
      get
      {
        if (this.selectImage == null)
          this.selectImage = WebStyleButtonImage.Generate(this.WebButtonWidth * 2 + 100, this.WebButtonheight, this.strSelect, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.selectImage;
      }
    }

    public Image SelectImageOver
    {
      get
      {
        if (this.selectImageOver == null)
          this.selectImageOver = WebStyleButtonImage.Generate(this.WebButtonWidth * 2 + 100, this.WebButtonheight, this.strSelect, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.selectImageOver;
      }
    }

    public Image NewsImage
    {
      get
      {
        if (this.newsImage == null)
          this.newsImage = WebStyleButtonImage.Generate(80, this.WebButtonheight, this.strNews, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.newsImage;
      }
    }

    public Image NewsImageOver
    {
      get
      {
        if (this.newsImageOver == null)
          this.newsImageOver = WebStyleButtonImage.Generate(80, this.WebButtonheight, this.strNews, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.newsImageOver;
      }
    }

    public Image AccountImage
    {
      get
      {
        if (this.accountImage == null)
          this.accountImage = WebStyleButtonImage.Generate(130, this.WebButtonheight, this.strAccountDetails, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.accountImage;
      }
    }

    public Image AccountImageOver
    {
      get
      {
        if (this.accountImageOver == null)
          this.accountImageOver = WebStyleButtonImage.Generate(130, this.WebButtonheight, this.strAccountDetails, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.accountImageOver;
      }
    }

    public Image CreateAccountImage
    {
      get
      {
        if (this.createAccountImage == null)
          this.createAccountImage = WebStyleButtonImage.Generate(250, this.WebButtonheight, this.strCreateAccount, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.createAccountImage;
      }
    }

    public Image CreateAccountImageOver
    {
      get
      {
        if (this.createAccountImageOver == null)
          this.createAccountImageOver = WebStyleButtonImage.Generate(250, this.WebButtonheight, this.strCreateAccount, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.createAccountImageOver;
      }
    }

    public Image CreateAccountImage2
    {
      get
      {
        if (this.createAccountImage2 == null)
          this.createAccountImage2 = WebStyleButtonImage.Generate(250, this.WebButtonheight, "StrongholdKingdoms.com", this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.createAccountImage2;
      }
    }

    public Image CreateAccountImageOver2
    {
      get
      {
        if (this.createAccountImageOver2 == null)
          this.createAccountImageOver2 = WebStyleButtonImage.Generate(250, this.WebButtonheight, "StrongholdKingdoms.com", this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.createAccountImageOver2;
      }
    }

    public Image OptionsImage
    {
      get
      {
        if (this.optionsImage == null)
          this.optionsImage = WebStyleButtonImage.Generate(250, this.WebButtonheight, this.strOptions, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.optionsImage;
      }
    }

    public Image OptionsImageOver
    {
      get
      {
        if (this.optionsImageOver == null)
          this.optionsImageOver = WebStyleButtonImage.Generate(250, this.WebButtonheight, this.strOptions, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.optionsImageOver;
      }
    }

    public Image ForgottenImage
    {
      get
      {
        if (this.forgottenImage == null)
          this.forgottenImage = WebStyleButtonImage.Generate(150, this.WebButtonheight, this.strForgottenPassword, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.forgottenImage;
      }
    }

    public Image ForgottenImageOver
    {
      get
      {
        if (this.forgottenImageOver == null)
          this.forgottenImageOver = WebStyleButtonImage.Generate(150, this.WebButtonheight, this.strForgottenPassword, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.forgottenImageOver;
      }
    }

    public Image ExitImage
    {
      get
      {
        if (this.exitImage == null)
          this.exitImage = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strExit, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.exitImage;
      }
    }

    public Image ExitImageOver
    {
      get
      {
        if (this.exitImageOver == null)
          this.exitImageOver = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strExit, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.exitImageOver;
      }
    }

    public Image CancelImage
    {
      get
      {
        if (this.cancelImage == null)
          this.cancelImage = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strCancel, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return this.cancelImage;
      }
    }

    public Image CancelImageOver
    {
      get
      {
        if (this.cancelImageOver == null)
          this.cancelImageOver = WebStyleButtonImage.Generate(100, this.WebButtonheight, this.strCancel, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return this.cancelImageOver;
      }
    }

    public CustomSelfDrawPanel.CSDImage MakeGreyoutImage(Control ctrl)
    {
      return this.MakeGreyoutImage(ctrl.Width, ctrl.Height);
    }

    public CustomSelfDrawPanel.CSDImage MakeGreyoutImage(int width, int height)
    {
      Image image = (Image) new Bitmap(width, height);
      Brush brush = (Brush) new SolidBrush(Color.FromArgb(125, 100, 100, 100));
      using (Graphics graphics = Graphics.FromImage(image))
        graphics.FillRectangle(brush, 0, 0, width, height);
      brush.Dispose();
      CustomSelfDrawPanel.CSDImage csdImage = new CustomSelfDrawPanel.CSDImage();
      csdImage.Height = image.Height;
      csdImage.Width = image.Width;
      csdImage.X = 0;
      csdImage.Y = 0;
      csdImage.Image = image;
      return csdImage;
    }

    public void SetSteamEmail(string text)
    {
      this.txtEmail.Text = text;
      this.lblEmailSteam.Text = text;
      this.lblEmail.Visible = true;
      this.lblPassword.Visible = false;
      this.txtPassword.Visible = false;
    }

    public void SetNonSteamEmail(string text, string password)
    {
      this.txtEmail.Text = text;
      this.txtPassword.Text = password;
      this.lblEmail.Visible = true;
    }

    public void AddControls()
    {
      this.pnlWorlds.Controls.Clear();
      this.pnlLogin.Controls.Clear();
      this.pnlFeedback.Controls.Clear();
      this.pnlTabs.Controls.Clear();
      this.allButtons = new List<CustomSelfDrawPanel.CSDControl>();
      this.txtEmail = new TextBox();
      this.lblEmailSteam = new CustomSelfDrawPanel.CSDLabel();
      this.lblEmail = new CustomSelfDrawPanel.CSDLabel();
      this.lblEmail.Text = Program.gamersFirstInstall || Program.arcInstall ? "" : (Program.bigpointInstall ? SK.Text("Login_BigPoint_username", "Stronghold Kingdoms Username") : this.strEmailAddress);
      this.lblEmail.Width = 300;
      this.lblEmail.Height = 18;
      this.txtPassword = new TextBox();
      this.lblPassword = new CustomSelfDrawPanel.CSDLabel();
      this.lblPassword.Text = Program.gamersFirstInstall || Program.arcInstall ? "" : (Program.bigpointInstall ? SK.Text("Login_BigPoint_Password", "Your Bigpoint Password") : this.strPassword);
      this.lblPassword.Width = 300;
      this.lblPassword.Height = 18;
      this.LoginPanelControls_LoggedOut = new CustomSelfDrawPanel();
      this.LoginPanelControls_LoggedOut.AutoScaleMode = AutoScaleMode.None;
      this.LoginPanelControls_LoggedOut.forceStyle();
      this.btnLogin = new CustomSelfDrawPanel.CSDImage();
      this.allButtons.Add((CustomSelfDrawPanel.CSDControl) this.btnLogin);
      this.btnLogin.Image = this.LoginImage;
      this.btnLogin.Width = this.btnLogin.Image.Width;
      this.btnLogin.Height = this.btnLogin.Image.Height;
      this.btnLogin.Enabled = false;
      this.lblLoginError = new CustomSelfDrawPanel.CSDLabel();
      this.lblLoginError.Color = ARGBColors.Red;
      this.lblLoginError.Visible = false;
      this.lblLoginError.Text = "ERROR:";
      this.lblLoginError.Width = this.pnlLogin.Width;
      this.lblEmail.Position = new Point(4, this.pnlTabs.Height - 29);
      this.txtEmail.Location = new Point(4, this.lblEmail.Y + this.lblEmail.Height);
      this.lblEmailSteam.Position = new Point(4, this.lblEmail.Y + this.lblEmail.Height);
      this.lblPassword.Position = new Point(4, this.txtEmail.Bottom + 2);
      this.txtPassword.Location = new Point(4, this.lblPassword.Y + this.lblPassword.Height);
      this.txtPassword.Width = this.pnlLogin.Width - 8;
      this.txtEmail.Width = this.pnlLogin.Width - 8;
      this.lblEmailSteam.Width = this.pnlLogin.Width - 8;
      this.lblEmailSteam.Height = this.lblEmail.Height;
      this.txtPassword.PasswordChar = '*';

      // ── Connection info label (MAC + IP) — opens AdvancedLoginForm on click ──
      CustomSelfDrawPanel.CSDLabel connLabel = new CustomSelfDrawPanel.CSDLabel();
      connLabel.Visible = true;
      connLabel.Width   = 300;
      connLabel.Height  = 60;
      connLabel.Text    = GetConnectionInfo(GetMacAddress());
      connLabel.Position = new Point(4, this.txtPassword.Bottom + 2);
      this.lblConnectionInfo = connLabel;
      this.lblConnectionInfo.setClickDelegate(
          (CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate)(() =>
              new Bot.UI.AdvancedLoginForm(this.txtEmail, this.txtPassword, this.lblConnectionInfo, this.GetMacAddress).Show()),
          "ProfileLoginWindow_login");
      try
      {
        if (Properties.Settings.Default.ShowAdvancedLoginOptions)
          new Bot.UI.AdvancedLoginForm(this.txtEmail, this.txtPassword, this.lblConnectionInfo, this.GetMacAddress).Show();
      }
      catch { }
      this.LoginPanelControls_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.lblConnectionInfo);
      // ─────────────────────────────────────────────────────────────────────────

      this.btnLogin.Position = new Point(4, this.lblConnectionInfo.Y + this.lblConnectionInfo.Height + 4);
      this.lblLoginError.Position = new Point(4, this.btnLogin.Y + this.btnLogin.Height);
      this.btnLogin.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnLogin_Click), "ProfileLoginWindow_login");
      this.btnLogin.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.loginOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.loginOut));
      this.txtEmail.TextChanged += new EventHandler(this.txtLoginField_Validate_email);
      this.txtPassword.TextChanged += new EventHandler(this.txtLoginField_Validate);
      this.txtEmail.KeyPress += new KeyPressEventHandler(this.txtEmail_KeyPress);
      this.txtPassword.KeyPress += new KeyPressEventHandler(this.txtEmail_KeyPress);
      this.LoginPanelControls_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.lblEmail);
      this.LoginPanelControls_LoggedOut.Controls.Add((Control) this.txtEmail);
      this.LoginPanelControls_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.lblEmailSteam);
      if (Program.steamInstall && Program.steamActive && Program.kingdomsAccountFound)
      {
        this.txtEmail.Visible = false;
        this.lblEmailSteam.Visible = true;
        this.lblEmailSteam.Text = Program.steamEmail;
      }
      else if (Program.steamInstall && Program.steamActive && !Program.kingdomsAccountFound)
      {
        this.txtEmail.Visible = false;
        this.lblEmailSteam.Visible = true;
        this.delayedCreateUserOpen = true;
      }
      else if (Program.aeriaInstall || Program.bigpointInstall)
      {
        this.txtEmail.Visible = false;
        this.lblEmail.Visible = false;
        this.txtPassword.Visible = false;
        this.lblPassword.Visible = false;
        this.btnLogin.Visible = false;
      }
      else if (Program.winStore && !Program.mySettings.hasLoggedIn())
        this.delayedCreateUserOpen = true;
      if (Program.bigpointPartnerInstall)
      {
        this.txtEmail.Visible = false;
        this.lblEmail.Visible = false;
        this.txtPassword.Visible = false;
        this.lblPassword.Visible = false;
        this.btnLogin.Visible = true;
        this.btnLoginFB.Visible = false;
        this.btnLogin.Position = new Point(84, this.txtPassword.Bottom + 4 - 30);
        this.bp2_loginMode = 0;
      }
      this.LoginPanelControls_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.lblPassword);
      this.LoginPanelControls_LoggedOut.Controls.Add((Control) this.txtPassword);
      this.LoginPanelControls_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.btnLogin);
      this.LoginPanelControls_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.lblLoginError);
      this.LoginPanelControls_LoggedOut.Size = this.pnlLogin.Size;
      this.LoginPanelControls_LoggedOut.Visible = true;
      this.pnlLogin.Controls.Add((Control) this.LoginPanelControls_LoggedOut);
      this.LoginPanelControls_LoggedIn = new CustomSelfDrawPanel();
      this.LoginPanelControls_LoggedIn.AutoScaleMode = AutoScaleMode.None;
      this.LoginPanelControls_LoggedIn.forceStyle();
      this.LoginPanelControls_LoggedIn.Size = this.pnlLogin.Size;
      this.btnClientLogout = new CustomSelfDrawPanel.CSDImage();
      this.allButtons.Add((CustomSelfDrawPanel.CSDControl) this.btnClientLogout);
      this.btnClientLogout.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnClientLogout_Click), "ProfileLoginWindow_logout");
      this.btnClientLogout.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.logoutOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.logoutOut));
      this.btnClientLogout.Image = this.LogoutImage;
      this.btnClientLogout.Width = this.btnClientLogout.Image.Width;
      this.btnClientLogout.Height = this.btnClientLogout.Image.Height;
      if (!Program.steamActive && !Program.aeriaInstall && !Program.gamersFirstInstall && !Program.arcInstall && !Program.bigpointInstall)
        this.LoginPanelControls_LoggedIn.addControl((CustomSelfDrawPanel.CSDControl) this.btnClientLogout);
      this.pnlLogin.Controls.Add((Control) this.LoginPanelControls_LoggedIn);
      this.btnShieldDesigner = new CustomSelfDrawPanel.CSDImage();
      this.allButtons.Add((CustomSelfDrawPanel.CSDControl) this.btnShieldDesigner);
      this.lblUsername = new CustomSelfDrawPanel.CSDImage();
      this.LoginPanelControls_LoggedIn.addControl((CustomSelfDrawPanel.CSDControl) this.btnShieldDesigner);
      this.LoginPanelControls_LoggedIn.addControl((CustomSelfDrawPanel.CSDControl) this.lblUsername);
      this.btnShieldDesigner.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.LoadShieldDesigner), "ProfileLoginWindow_shield_designer");
      this.btnClientLogout.Y = this.pnlTabs.Height - 29;
      this.btnClientLogout.X = this.LoginPanelControls_LoggedIn.Width - this.btnClientLogout.Width - 4;
      this.lblUsername.X = 4;
      this.lblUsername.Y = this.btnClientLogout.Y;
      this.WorldsPanelcontrols_LoggedOut = new CustomSelfDrawPanel();
      this.WorldsPanelcontrols_LoggedOut.AutoScaleMode = AutoScaleMode.None;
      this.WorldsPanelcontrols_LoggedOut.forceStyle();
      this.WorldsPanelcontrols_LoggedOut.Size = this.pnlWorlds.Size;
      this.lblWorldsOfflineError = new CustomSelfDrawPanel.CSDLabel();
      this.lblWorldsOfflineError.Color = ARGBColors.Red;
      this.WorldsPanelcontrols_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.lblWorldsOfflineError);
      this.WorldsPanelcontrols_LoggedOut.Visible = false;
      this.pnlWorlds.Controls.Add((Control) this.WorldsPanelcontrols_LoggedOut);
      this.WorldsPanelcontrols_LoggedIn = new CustomSelfDrawPanel();
      this.WorldsPanelcontrols_LoggedIn.forceStyle();
      this.WorldsPanelcontrols_LoggedIn.AutoScaleMode = AutoScaleMode.None;
      this.WorldsPanelcontrols_LoggedIn.Size = this.pnlWorlds.Size;
      this.lblWorldsOnlineError = new CustomSelfDrawPanel.CSDLabel();
      this.lblWorldsOnlineError.Color = ARGBColors.Red;
      this.WorldsPanelcontrols_LoggedIn.addControl((CustomSelfDrawPanel.CSDControl) this.lblWorldsOnlineError);
      this.WorldsPanelcontrols_LoggedIn.Visible = false;
      this.pnlWorlds.Controls.Add((Control) this.WorldsPanelcontrols_LoggedIn);
      this.BrowserTabsControls = new CustomSelfDrawPanel();
      this.BrowserTabsControls.forceStyle();
      this.BrowserTabsControls.AutoScaleMode = AutoScaleMode.None;
      this.BrowserTabsControls.Size = this.pnlTabs.Size;
      this.pnlTabs.Controls.Add((Control) this.BrowserTabsControls);
      this.btnExit.GotFocus += new EventHandler(this.btnExit_GotFocus);
      if (Program.mySettings.Username.Trim().Length > 0)
      {
        this.ignoreEmailChange = true;
        this.txtEmail.Text = Program.mySettings.Username;
        this.ignoreEmailChange = false;
        if (Program.steamActive)
        {
          this.lblEmailSteam.Text = Program.steamEmail;
          this.txtPassword.Visible = false;
          this.lblPassword.Visible = false;
          this.btnLogin.Visible = false;
        }
        this.txtPassword.Focus();
      }
      else
        this.txtEmail.Focus();
      this.LoginPanelControls_Feedback = new CustomSelfDrawPanel();
      this.LoginPanelControls_Feedback.forceStyle();
      this.LoginPanelControls_Feedback.Location = new Point(0, 0);
      this.LoginPanelControls_Feedback.AutoScaleMode = AutoScaleMode.None;
      this.LoginPanelControls_Feedback.Size = this.pnlFeedback.Size;
      this.feedbackProgressArea = new CustomSelfDrawPanel.CSDArea();
      this.feedbackProgressArea.Size = this.LoginPanelControls_Feedback.Size;
      this.LoginPanelControls_Feedback.addControl((CustomSelfDrawPanel.CSDControl) this.feedbackProgressArea);
      this.feedbackProgress = new CustomSelfDrawPanel.CSDFill();
      this.feedbackProgress.FillColor = Color.FromArgb((int) byte.MaxValue, 182, 0);
      this.feedbackProgress.Size = new Size(0, this.pnlFeedback.Height);
      this.feedbackProgressArea.addControl((CustomSelfDrawPanel.CSDControl) this.feedbackProgress);
      this.feedbackLine = new CustomSelfDrawPanel.CSDLine();
      this.feedbackLine.Position = new Point(0, 0);
      this.feedbackLine.Size = new Size(this.pnlFeedback.Width, 0);
      this.feedbackLine.LineColor = ARGBColors.Black;
      this.feedbackProgressArea.addControl((CustomSelfDrawPanel.CSDControl) this.feedbackLine);
      this.exitButton = new CustomSelfDrawPanel.CSDImage();
      this.allButtons.Add((CustomSelfDrawPanel.CSDControl) this.exitButton);
      this.exitButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnExit_Click), "ProfileLoginWindow_exit");
      this.exitButton.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.exitOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.exitOut));
      this.exitButton.Image = this.ExitImage;
      this.exitButton.Width = this.exitButton.Image.Width;
      this.exitButton.Height = this.exitButton.Image.Height;
      this.exitButton.Position = new Point(823, 5);
      this.feedbackProgressArea.addControl((CustomSelfDrawPanel.CSDControl) this.exitButton);
      this.cancelButton = new CustomSelfDrawPanel.CSDImage();
      this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick), "ProfileLoginWindow_cancel");
      this.cancelButton.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.cancelOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.cancelOut));
      this.cancelButton.Image = this.CancelImage;
      this.cancelButton.Width = this.cancelButton.Image.Width;
      this.cancelButton.Height = this.cancelButton.Image.Height;
      this.cancelButton.Position = new Point(4, 5);
      this.cancelButton.Visible = false;
      this.feedbackProgressArea.addControl((CustomSelfDrawPanel.CSDControl) this.cancelButton);
      this.lblRetrieving = new CustomSelfDrawPanel.CSDLabel();
      this.lblRetrieving.Text = "";
      this.lblRetrieving.Position = new Point(112, 10);
      this.lblRetrieving.Size = new Size(600, 20);
      this.lblRetrieving.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblRetrieving.Color = ARGBColors.Black;
      this.lblRetrieving.Visible = false;
      this.Text = this.defaultWindowTitle;
      this.feedbackProgressArea.addControl((CustomSelfDrawPanel.CSDControl) this.lblRetrieving);
      this.lblVersion = new CustomSelfDrawPanel.CSDLabel();
      this.lblVersion.Text = "";
      this.lblVersion.Position = new Point(640, 10);
      this.lblVersion.Size = new Size(168, 20);
      this.lblVersion.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.lblVersion.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblVersion.Color = ARGBColors.Black;
      this.lblVersion.Visible = false;
      this.feedbackProgressArea.addControl((CustomSelfDrawPanel.CSDControl) this.lblVersion);
      this.tandcLabel = new CustomSelfDrawPanel.CSDLabel();
      this.tandcLabel.Text = SK.Text("TOUCH_Z_TermsOfUse", "Terms of Use");
      this.tandcLabel.Size = new Size(270, 15);
      this.tandcLabel.Position = new Point(40, 10);
      this.tandcLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.tandcLabel.Color = ARGBColors.Black;
      this.tandcLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tcClicked));
      this.tandcLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.tandcLabel.Color = ARGBColors.Red), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.tandcLabel.Color = ARGBColors.Black));
      this.tandcLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.tandcLabel.Visible = true;
      this.feedbackProgressArea.addControl((CustomSelfDrawPanel.CSDControl) this.tandcLabel);
      this.gameRulesLabel = new CustomSelfDrawPanel.CSDLabel();
      this.gameRulesLabel.Text = SK.Text("MENU_Game_Rules", "Game Rules");
      this.gameRulesLabel.Size = new Size(300, 15);
      if (Program.mySettings.languageIdent == "de")
        this.gameRulesLabel.Position = new Point(253, 10);
      else
        this.gameRulesLabel.Position = new Point(205, 10);
      this.gameRulesLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.gameRulesLabel.Color = ARGBColors.Black;
      this.gameRulesLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.gameRulesClicked));
      this.gameRulesLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.gameRulesLabel.Color = ARGBColors.Red), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.gameRulesLabel.Color = ARGBColors.Black));
      this.gameRulesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.gameRulesLabel.Visible = true;
      this.feedbackProgressArea.addControl((CustomSelfDrawPanel.CSDControl) this.gameRulesLabel);
      this.forumLabel = new CustomSelfDrawPanel.CSDLabel();
      this.forumLabel.Text = SK.Text("MENU_Privacy", "Privacy Policy");
      this.forumLabel.Size = new Size(300, 15);
      this.forumLabel.Position = new Point(370, 10);
      this.forumLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.forumLabel.Color = ARGBColors.Black;
      this.forumLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumClicked));
      this.forumLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.forumLabel.Color = ARGBColors.Red), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.forumLabel.Color = ARGBColors.Black));
      this.forumLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.forumLabel.Visible = true;
      this.feedbackProgressArea.addControl((CustomSelfDrawPanel.CSDControl) this.forumLabel);
      this.supportLabel = new CustomSelfDrawPanel.CSDLabel();
      this.supportLabel.Text = SK.Text("MENU_Support", "Support");
      this.supportLabel.Size = new Size(100, 15);
      this.supportLabel.Position = new Point(535, 10);
      this.supportLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.supportLabel.Color = ARGBColors.Black;
      this.supportLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.supportClicked));
      this.supportLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.supportLabel.Color = ARGBColors.Red), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.supportLabel.Color = ARGBColors.Black));
      this.supportLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.supportLabel.Visible = true;
      this.feedbackProgressArea.addControl((CustomSelfDrawPanel.CSDControl) this.supportLabel);
      this.pnlFeedback.Controls.Add((Control) this.LoginPanelControls_Feedback);
      this.GreyoutWorlds = this.MakeGreyoutImage((Control) this.pnlWorlds);
      this.GreyoutLogin = this.MakeGreyoutImage((Control) this.pnlLogin);
      this.GreyoutTabs = this.MakeGreyoutImage((Control) this.pnlFeedback);
      if (Program.steamActive)
      {
        this.lblEmail.Visible = false;
        this.lblPassword.Visible = false;
        this.txtPassword.Visible = false;
      }
      if (Program.steamInstall && Program.steamActive && Program.kingdomsAccountFound && !ProfileLoginWindow.successfulAutoLogin)
        this.btnLogin_Click();
      if (!Program.gamersFirstInstall && !Program.arcInstall)
        return;
      this.lblEmail.Visible = false;
      this.lblPassword.Visible = false;
      this.txtPassword.Visible = false;
      this.txtEmail.Visible = false;
      this.btnLogin.Visible = false;
      Program.mySettings.AutoLogin = false;
      if (ProfileLoginWindow.successfulAutoLogin)
        return;
      this.btnLogin_Click();
    }

    private void statusPageClicked()
    {
      try
      {
        new Process()
        {
          StartInfo = {
            FileName = ("https://slogin.strongholdkingdoms.com/status.php?lang=" + Program.mySettings.languageIdent)
          }
        }.Start();
      }
      catch (Exception ex)
      {
      }
    }

    private void tcClicked()
    {
      try
      {
        new Process()
        {
          StartInfo = {
            FileName = URLs.TermsAndConditions
          }
        }.Start();
      }
      catch (Exception ex)
      {
      }
    }

    private void gameRulesClicked()
    {
      try
      {
        new Process()
        {
          StartInfo = {
            FileName = URLs.IPSharingPage
          }
        }.Start();
      }
      catch (Exception ex)
      {
      }
    }

    private void forumClicked()
    {
      try
      {
        new Process()
        {
          StartInfo = {
            FileName = URLs.PrivacyPolicy
          }
        }.Start();
      }
      catch (Exception ex)
      {
      }
    }

    private void supportClicked()
    {
      try
      {
        Process process = new Process();
        if (RemoteServices.Instance.UserGuid == Guid.Empty)
          process.StartInfo.FileName = URLs.Supportpage;
        else
          process.StartInfo.FileName = "https://login.strongholdkingdoms.com/support/?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.languageIdent;
        process.Start();
      }
      catch (Exception ex)
      {
      }
    }

    public void openAeriaPopup() => this.openAeriaPopup(false);

    public void openAeriaPopup(bool logout)
    {
      string str1 = "https://www.aeriagames.com/dialog/oauth?response_type=code&client_id=";
      string str2;
      switch (Program.mySettings.LanguageIdent.ToLower())
      {
        case "fr":
          str2 = str1 + "c855f84df02e095dfc674de414ac912005048e61c&state=login_fr&lang=fr";
          break;
        case "de":
          str2 = str1 + "8ab4b5d461753ddf4bca8ace798ec5a705048e5a8&state=login_de&lang=de";
          break;
        case "ru":
          str2 = str1 + "46a58fca1d92ee6eba5245668660db0a05048e6b1&state=login_ru&lang=ru";
          break;
        case "es":
          str2 = str1 + "34f89db79f7699370f70b91ba0f93960051143200&state=login_es&lang=es";
          break;
        default:
          str2 = str1 + "bcccf8ff68ac2d79fa9dd659332cf83405048e30a&state=login&lang=en";
          break;
      }
      AeriaWindow.ShowAeriaLogin(str2 + "&theme=api_ignite&redirect_uri=https://login.strongholdkingdoms.com/aeria/login.php", "", this, (AeriaEventHandler) ((sender, e) => ProfileLoginWindow.AeriaToken = e.token));
    }

    public void aeriaClose() => this.btnExit_Click();

    public void aeriaLogin(string userGUID, string sessionGUID)
    {
      this.txtEmail.Text = userGUID;
      this.txtPassword.Text = sessionGUID;
      this.btnLogin_Click();
    }

    public void openBigPointPopup() => this.openBigPointPopup(false);

    public void openBigPointPopup(bool logout)
    {
      BigPointWindow.ShowBigPointLogin("https://login.strongholdkingdoms.com/bigpoint/iframelogin.php?lang=" + Program.mySettings.LanguageIdent.ToLower(), "", this, (BigPointEventHandler) ((sender, e) => ProfileLoginWindow.AeriaToken = e.token));
    }

    public void bigpointClose() => this.btnExit_Click();

    public void bigpointLogin(string userGUID, string sessionGUID)
    {
      this.txtEmail.Text = userGUID;
      this.txtPassword.Text = sessionGUID;
      this.btnLogin_Click();
    }

    public void openFacebookPopup()
    {
    }

    public void FacebookClose()
    {
    }

    public void FacebookLogin(string userGUID, string sessionGUID)
    {
    }

    private void btnExit_GotFocus(object sender, EventArgs e) => this.txtPassword.Focus();

    private void txtEmail_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Return)
        return;
      if (this.btnLogin.Enabled)
      {
        GameEngine.Instance.playInterfaceSound("ProfileLoginWindow_login");
        this.btnLogin_Click();
      }
      e.Handled = true;
    }

    private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      if (this.btnLogin.Enabled)
      {
        GameEngine.Instance.playInterfaceSound("ProfileLoginWindow_login");
        this.btnLogin_Click();
      }
      e.Handled = true;
    }

    public void SetUsername()
    {
      this.lblUsername.Image = WebStyleButtonImage.GenerateLabel(200, 25, RemoteServices.Instance.UserName, ARGBColors.Black, ARGBColors.Transparent);
      this.lblUsername.Width = 200;
      this.lblUsername.Height = 25;
    }

    public void SetShieldImage()
    {
      this.btnShieldDesigner.Image = ProfileLoginWindow.ShieldImage["profile"];
      this.btnShieldDesigner.Width = this.btnShieldDesigner.Image.Width;
      this.btnShieldDesigner.Height = this.btnShieldDesigner.Image.Height;
      this.btnShieldDesigner.X = (this.pnlLogin.Width - this.btnShieldDesigner.Width) / 2;
      this.btnShieldDesigner.Y = this.pnlTabs.Bottom + 8;
      this.btnShieldDesigner.CustomTooltipID = 4015;
    }

    public void LoadShieldImages()
    {
      foreach (string key in ProfileLoginWindow.ShieldURL.Keys)
      {
        try
        {
          if (key == "profile")
          {
            if (ProfileLoginWindow.ShieldURL[key].ToLowerInvariant() != "/shield/render/profile_coa_placeholder.png")
            {
              char[] chArray = new char[2]{ '_', '.' };
              string[] strArray = ProfileLoginWindow.ShieldURL[key].ToLowerInvariant().Split(chArray);
              GameEngine.Instance.World.downloadPlayerShield(strArray[1], new ShieldFactory.AsyncDelegate(this.shieldDownloaded));
              ProfileLoginWindow.ShieldImage[key] = GFXLibrary.dummy;
            }
            else
              ProfileLoginWindow.ShieldImage[key] = GameEngine.Instance.World.getDummyShield(140, 156);
          }
          else
            ProfileLoginWindow.ShieldImage[key] = GameEngine.Instance.World.getDummyShield(140, 156);
        }
        catch (Exception ex)
        {
          ProfileLoginWindow.ShieldImage[key] = GameEngine.Instance.World.getDummyShield(140, 156);
        }
      }
    }

    private void shieldDownloaded()
    {
      if (ShieldFactory.LastErrorString.Length <= 0)
        ProfileLoginWindow.ShieldImage["profile"] = GameEngine.Instance.World.getPlayerShieldImage(140, 156);
      if (ProfileLoginWindow.ShieldImage["profile"] == null)
        ProfileLoginWindow.ShieldImage["profile"] = GameEngine.Instance.World.getDummyShield(140, 156);
      if (this.btnShieldDesigner == null)
        return;
      this.SetShieldImage();
      this.btnShieldDesigner.invalidate();
    }

    private void RespCallback(IAsyncResult ar)
    {
      try
      {
        Image image = Image.FromStream(((ProfileLoginWindow.RequestState) ar.AsyncState).req.EndGetResponse(ar).GetResponseStream());
        ProfileLoginWindow.ShieldImage["profile"] = image;
      }
      catch (Exception ex)
      {
        ProfileLoginWindow.ShieldImage["profile"] = (Image) GFXLibrary.LoginShieldPlaceholder;
      }
      if (this.btnShieldDesigner == null)
        return;
      this.SetShieldImage();
      this.btnShieldDesigner.invalidate();
    }

    public void LoadShieldDesigner()
    {
      Process.Start(URLs.shieldDesignerURL + "?webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.LanguageIdent.ToLower());
    }

    public void loginOver() => this.btnLogin.Image = this.LoginImageOver;

    public void loginOut() => this.btnLogin.Image = this.LoginImage;

    public void logoutOver() => this.btnClientLogout.Image = this.LogoutImageOver;

    public void logoutOut() => this.btnClientLogout.Image = this.LogoutImage;

    public void exitOver() => this.exitButton.Image = this.ExitImageOver;

    public void exitOut() => this.exitButton.Image = this.ExitImage;

    public void cancelOver() => this.cancelButton.Image = this.CancelImageOver;

    public void cancelOut() => this.cancelButton.Image = this.CancelImage;

    public void ShowTabs()
    {
      this.BrowserTabsControls.clearControls();
      bool flag = this.isPlayerLoggedIn();
      int num1 = 8;
      int num2 = this.BrowserTabsControls.Height - 29;
      CustomSelfDrawPanel.CSDImage btnNews = new CustomSelfDrawPanel.CSDImage();
      this.allButtons.Add((CustomSelfDrawPanel.CSDControl) btnNews);
      btnNews.Image = this.NewsImage;
      btnNews.Height = btnNews.Image.Height;
      btnNews.Width = btnNews.Image.Width;
      btnNews.X = num1;
      btnNews.Y = num2;
      this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) btnNews);
      int num3 = num1 + (btnNews.Width + 2);
      btnNews.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnNews_Click), "ProfileLoginWindow_news");
      btnNews.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => btnNews.Image = this.NewsImageOver), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => btnNews.Image = this.NewsImage));
      if (!Program.bigpointPartnerInstall)
      {
        if (Program.mySettings.LanguageIdent == "ru")
        {
          this.imgSHKRu.ImageNorm = (Image) GFXLibrary.vk;
          this.imgSHKRu.OverBrighten = true;
          this.imgSHKRu.Size = new Size(32, 32);
          this.imgSHKRu.Position = new Point(268, 5);
          this.imgSHKRu.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.shkruClick));
          this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) this.imgSHKRu);
          this.imgTwitter.ImageNorm = (Image) GFXLibrary.twitter;
          this.imgTwitter.OverBrighten = true;
          this.imgTwitter.Size = new Size(32, 32);
          this.imgTwitter.Position = new Point(305, 5);
          this.imgTwitter.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.twitterClick));
          this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) this.imgTwitter);
          this.imgYoutube.ImageNorm = (Image) GFXLibrary.youtube;
          this.imgYoutube.OverBrighten = true;
          this.imgYoutube.Position = new Point(342, 5);
          this.imgYoutube.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.youtubeClick));
          this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) this.imgYoutube);
        }
        else
        {
          this.imgTwitter.ImageNorm = (Image) GFXLibrary.twitter;
          this.imgTwitter.OverBrighten = true;
          this.imgTwitter.Size = new Size(32, 32);
          this.imgTwitter.Position = new Point(268, 5);
          this.imgTwitter.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.twitterClick));
          this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) this.imgTwitter);
          this.imgFacebook.ImageNorm = (Image) GFXLibrary.facebook;
          this.imgFacebook.OverBrighten = true;
          this.imgFacebook.Size = new Size(32, 32);
          this.imgFacebook.Position = new Point(305, 5);
          this.imgFacebook.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.facebookClick));
          this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) this.imgFacebook);
          this.imgYoutube.ImageNorm = (Image) GFXLibrary.youtube;
          this.imgYoutube.OverBrighten = true;
          this.imgYoutube.Position = new Point(342, 5);
          this.imgYoutube.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.youtubeClick));
          this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) this.imgYoutube);
        }
      }
      int num4;
      if (flag)
      {
        CustomSelfDrawPanel.CSDImage btnAccountDetails = new CustomSelfDrawPanel.CSDImage();
        this.allButtons.Add((CustomSelfDrawPanel.CSDControl) btnAccountDetails);
        btnAccountDetails.Image = this.AccountImage;
        btnAccountDetails.Height = btnAccountDetails.Image.Height;
        btnAccountDetails.Width = btnAccountDetails.Image.Width;
        btnAccountDetails.X = num3;
        btnAccountDetails.Y = num2;
        this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) btnAccountDetails);
        num4 = num3 + (btnAccountDetails.Width + 2);
        btnAccountDetails.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAccountDetails_Click), "ProfileLoginWindow_account_details");
        btnAccountDetails.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => btnAccountDetails.Image = this.AccountImageOver), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => btnAccountDetails.Image = this.AccountImage));
        CustomSelfDrawPanel.CSDImage btnCreateAccount = new CustomSelfDrawPanel.CSDImage();
        this.allButtons.Add((CustomSelfDrawPanel.CSDControl) btnCreateAccount);
        btnCreateAccount.Image = this.OptionsImage;
        btnCreateAccount.X = 400;
        btnCreateAccount.Height = btnCreateAccount.Image.Height;
        btnCreateAccount.Width = btnCreateAccount.Image.Width;
        btnCreateAccount.Y = num2;
        this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) btnCreateAccount);
        btnCreateAccount.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.options_Click), "ProfileLoginWindow_create_account");
        btnCreateAccount.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => btnCreateAccount.Image = this.OptionsImageOver), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => btnCreateAccount.Image = this.OptionsImage));
      }
      else
      {
        if (!Program.steamActive && !Program.aeriaInstall && !Program.gamersFirstInstall && !Program.arcInstall && !Program.bigpointInstall && !Program.bigpointPartnerInstall)
        {
          CustomSelfDrawPanel.CSDImage btnCreateAccount = new CustomSelfDrawPanel.CSDImage();
          this.allButtons.Add((CustomSelfDrawPanel.CSDControl) btnCreateAccount);
          btnCreateAccount.Image = Program.mySettings.hasLoggedIn() || Program.bigpointInstall || Program.aeriaInstall || Program.bigpointPartnerInstall ? this.CreateAccountImage2 : this.CreateAccountImage;
          btnCreateAccount.X = 400;
          btnCreateAccount.Height = btnCreateAccount.Image.Height;
          btnCreateAccount.Width = btnCreateAccount.Image.Width;
          btnCreateAccount.Y = num2;
          this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) btnCreateAccount);
          btnCreateAccount.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnCreateAccount_Click), "ProfileLoginWindow_create_account");
          btnCreateAccount.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
          {
            if (!Program.mySettings.hasLoggedIn() && !Program.bigpointInstall && !Program.aeriaInstall && !Program.bigpointPartnerInstall)
              btnCreateAccount.Image = this.CreateAccountImageOver;
            else
              btnCreateAccount.Image = this.CreateAccountImageOver2;
          }), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
          {
            if (!Program.mySettings.hasLoggedIn() && !Program.bigpointInstall && !Program.aeriaInstall && !Program.bigpointPartnerInstall)
              btnCreateAccount.Image = this.CreateAccountImage;
            else
              btnCreateAccount.Image = this.CreateAccountImage2;
          }));
        }
        if (Program.bigpointInstall || Program.aeriaInstall || Program.gamersFirstInstall || Program.bigpointPartnerInstall || Program.arcInstall || Program.steamActive)
          return;
        CustomSelfDrawPanel.CSDImage btnForgottenPassword = new CustomSelfDrawPanel.CSDImage();
        this.allButtons.Add((CustomSelfDrawPanel.CSDControl) btnForgottenPassword);
        btnForgottenPassword.Image = this.ForgottenImage;
        btnForgottenPassword.Height = btnForgottenPassword.Image.Height;
        btnForgottenPassword.Width = btnForgottenPassword.Image.Width;
        btnForgottenPassword.X = num3;
        btnForgottenPassword.Y = num2;
        this.BrowserTabsControls.addControl((CustomSelfDrawPanel.CSDControl) btnForgottenPassword);
        num4 = num3 + (btnForgottenPassword.Width + 2);
        btnForgottenPassword.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnForgotten_Click), "ProfileLoginWindow_forgotten_password");
        btnForgottenPassword.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => btnForgottenPassword.Image = this.ForgottenImageOver), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => btnForgottenPassword.Image = this.forgottenImage));
      }
    }

    private void btnAccountDetails_Click()
    {
      this.EnablePanels(false);
      this.geckoWebBrowser1.Navigate(new Uri(URLs.AccountMainPage), (IDictionary<string, string>) new Dictionary<string, string>()
      {
        {
          "webtoken",
          RemoteServices.Instance.WebToken
        },
        {
          "ClientLanguage",
          Program.mySettings.LanguageIdent.ToLower()
        },
        {
          "NewLoginScreen",
          "1"
        }
      });
    }

    private void options_Click()
    {
      GameEngine.Instance.playInterfaceSound("Options_open");
      OptionsPopup.openSettingsLogin();
    }

    private void btnCreateAccount_Click()
    {
      if (!Program.mySettings.hasLoggedIn() && !Program.bigpointInstall && !Program.aeriaInstall && !Program.bigpointPartnerInstall)
        this.ShowCreateUserForm();
      else if (Program.mySettings.LanguageIdent == "de")
        new Process()
        {
          StartInfo = {
            FileName = "https://de.strongholdkingdoms.com/"
          }
        }.Start();
      else if (Program.mySettings.LanguageIdent == "fr")
        new Process()
        {
          StartInfo = {
            FileName = "https://fr.strongholdkingdoms.com/"
          }
        }.Start();
      else if (Program.mySettings.LanguageIdent == "ru")
        new Process()
        {
          StartInfo = {
            FileName = "https://ru.strongholdkingdoms.com"
          }
        }.Start();
      else if (Program.mySettings.LanguageIdent == "es")
        new Process()
        {
          StartInfo = {
            FileName = "https://es.strongholdkingdoms.com"
          }
        }.Start();
      else if (Program.mySettings.LanguageIdent == "pl")
        new Process()
        {
          StartInfo = {
            FileName = "https://pl.strongholdkingdoms.com"
          }
        }.Start();
      else if (Program.mySettings.LanguageIdent == "tr")
        new Process()
        {
          StartInfo = {
            FileName = "https://tr.strongholdkingdoms.com"
          }
        }.Start();
      else if (Program.mySettings.LanguageIdent == "it")
        new Process()
        {
          StartInfo = {
            FileName = "https://it.strongholdkingdoms.com"
          }
        }.Start();
      else if (Program.mySettings.LanguageIdent == "pt")
        new Process()
        {
          StartInfo = {
            FileName = "https://pt.strongholdkingdoms.com"
          }
        }.Start();
      else
        new Process()
        {
          StartInfo = {
            FileName = "https://www.strongholdkingdoms.com"
          }
        }.Start();
    }

    private void btnForgotten_Click()
    {
      this.EnablePanels(false);
      this.geckoWebBrowser1.Navigate(URLs.ForgottenPasswordLink + "?lang=" + Program.mySettings.LanguageIdent.ToLower());
    }

    private void btnNews_Click()
    {
      this.EnablePanels(false);
      this.geckoWebBrowser1.Navigate(URLs.NewsMainPage);
    }

    public void ShowCreateUserForm() => InterfaceMgr.Instance.openCreatePopupWindow();

    public void ShowWorldSelect() => InterfaceMgr.Instance.openWorldSelectPopupWindow();

    public void EnablePanels(bool enabled)
    {
      if (this.lblRetrieving == null)
        return;
      if (this.lblRetrieving.Visible)
        enabled = false;
      this.pnlWorlds.Enabled = enabled;
      this.pnlLogin.Enabled = enabled;
      this.pnlTabs.Enabled = enabled;
      this.btnLogin.Enabled = enabled;
      this.btnClientLogout.Enabled = enabled;
      foreach (CustomSelfDrawPanel.CSDControl allButton in this.allButtons)
      {
        if (allButton != null)
          allButton.Enabled = enabled;
      }
      if (this.isPlayerLoggedIn())
      {
        this.WorldsPanelcontrols_LoggedIn.removeControl((CustomSelfDrawPanel.CSDControl) this.GreyoutWorlds);
        this.LoginPanelControls_LoggedIn.removeControl((CustomSelfDrawPanel.CSDControl) this.GreyoutLogin);
        if (!enabled)
        {
          this.WorldsPanelcontrols_LoggedIn.addControl((CustomSelfDrawPanel.CSDControl) this.GreyoutWorlds);
          this.LoginPanelControls_LoggedIn.addControl((CustomSelfDrawPanel.CSDControl) this.GreyoutLogin);
        }
        this.WorldsPanelcontrols_LoggedIn.Invalidate();
        this.LoginPanelControls_LoggedIn.Invalidate();
      }
      else
      {
        this.WorldsPanelcontrols_LoggedOut.removeControl((CustomSelfDrawPanel.CSDControl) this.GreyoutWorlds);
        this.LoginPanelControls_LoggedOut.removeControl((CustomSelfDrawPanel.CSDControl) this.GreyoutLogin);
        if (!enabled)
        {
          this.WorldsPanelcontrols_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.GreyoutWorlds);
          this.LoginPanelControls_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.GreyoutLogin);
        }
        this.WorldsPanelcontrols_LoggedOut.Invalidate();
        this.LoginPanelControls_LoggedOut.Invalidate();
      }
      this.pnlWorlds.Invalidate();
      this.pnlFeedback.Invalidate();
      this.pnlTabs.Invalidate();
      this.pnlLogin.Invalidate();
    }

    public void JoinGameworld(int? playing, int? firstworld, int? worldid, string worldName)
    {
      if (GameEngine.IsTOSLocked)
        return;
      if (playing.HasValue)
      {
        int? nullable = playing;
        if ((nullable.GetValueOrDefault() != 0 ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
          ++ProfileLoginWindow.LastNumberOfWorldsPlaying;
      }
      Program.mySettings.LastWorldID = worldid.Value;
      this.lastWorldLoggedIn = Program.mySettings.LastWorldID;
      Program.mySettings.Save();
      this.lblRetrieving.Text = SK.Text("ProfileLogin_Connecting", "Connecting To : ") + worldName;
      this.lblRetrieving.Visible = true;
      this.tandcLabel.Visible = false;
      this.gameRulesLabel.Visible = false;
      this.forumLabel.Visible = false;
      this.supportLabel.Visible = false;
      this.feedbackProgress.Size = new Size(Math.Min(this.pnlFeedback.Width / 11, this.pnlFeedback.Width), this.pnlFeedback.Height);
      this.feedbackProgressArea.invalidate();
      this.Text = this.defaultWindowTitle + " - " + this.lblRetrieving.Text;
      this.cancelButton.Visible = true;
      XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).ChooseWorld((IAuthRequest) new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), "", "", "", worldid, firstworld, playing)
      {
        Culture = (Program.syslang + ";" + Program.mySettings.LanguageIdent.ToLower())
      }, new AuthEndResponseDelegate(this.JoinGameworldCallback), (Control) this);
    }

    private void JoinGameworldCallback(IAuthProvider sender, IAuthResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        foreach (WorldInfo world in ProfileLoginWindow.WorldList)
        {
          if (world.KingdomsWorldID == this.lastWorldLoggedIn)
          {
            if (!world.Playing)
              world.Playing = true;
            GameEngine.Instance.World.contestStartTime = world.ContestStartTime;
            GameEngine.Instance.World.contestEndTime = world.ContestEndTime;
            GameEngine.Instance.World.contestName = world.ContestName;
            GameEngine.Instance.World.contestDescription = world.ContestDescription;
            GameEngine.Instance.World.contestID = world.ContestID;
          }
        }
        string admin = string.Empty;
        if (this.AdminGUID != null && this.AdminGUID.Length > 0)
          admin = this.AdminGUID;
        if (!Program.kingdomsAccountFound)
          this.LoginBeta(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), this.txtEmail.Text, admin);
        else
          this.LoginBeta(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), Program.steamEmail, admin);
      }
      else
      {
        this.lblRetrieving.Visible = false;
        this.tandcLabel.Visible = true;
        this.gameRulesLabel.Visible = true;
        if (Program.bigpointInstall || Program.bigpointPartnerInstall)
          this.forumLabel.Visible = false;
        else
          this.forumLabel.Visible = true;
        this.supportLabel.Visible = true;
        this.feedbackProgress.Size = new Size(Math.Min(this.pnlFeedback.Width / 11, this.pnlFeedback.Width), this.pnlFeedback.Height);
        this.feedbackProgressArea.invalidate();
        this.Text = this.defaultWindowTitle;
        this.cancelButton.Visible = false;
        int num = (int) MessageBox.Show("ERROR: " + response.Message);
        this.EnablePanels(true);
      }
    }

    public void BuildOnlineWorldList(List<WorldInfo> list)
    {
      if (this.loggedInWorldControls != null && this.loggedInWorldControls.Count > 0)
      {
        foreach (CustomSelfDrawPanel.CSDControl loggedInWorldControl in this.loggedInWorldControls)
          this.WorldsPanelcontrols_LoggedIn.removeControl(loggedInWorldControl);
        this.loggedInWorldControls.Clear();
      }
      else if (this.loggedInWorldControls == null)
        this.loggedInWorldControls = new List<CustomSelfDrawPanel.CSDControl>();
      int num1 = Program.mySettings.LastWorldID;
      string str1 = SK.Text("LOGIN_LastWorld", "Last World Played");
      int num2 = 0;
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].Playing)
          ++num2;
      }
      int num3 = -1;
      if (num2 == 0)
      {
        if (this.chkAutoLogin != null)
          this.chkAutoLogin.Checked = false;
        Program.mySettings.AutoLogin = false;
        str1 = SK.Text("LOGIN_Recommended_Server", "Recommended World");
        bool flag1 = false;
        int num4 = 0;
        int num5 = num1;
        for (int index = 0; index < list.Count; ++index)
        {
          if (list[index].Supportculture == Program.mySettings.LanguageIdent)
            flag1 = true;
          if (list[index].NewWorld)
          {
            ++num4;
            num5 = list[index].KingdomsWorldID;
            if (list[index].KingdomsWorldID >= 700 && list[index].KingdomsWorldID < 799 || list[index].KingdomsWorldID >= 1200 && list[index].KingdomsWorldID < 1299)
              num3 = list[index].KingdomsWorldID;
          }
        }
        if (num4 == 1)
        {
          num1 = num5;
        }
        else
        {
          bool flag2 = false;
          string str2 = Program.mySettings.LanguageIdent;
          if (!flag1)
            str2 = "en";
          bool flag3 = false;
          int num6 = -1;
          switch (str2)
          {
            case "en":
              uint num7 = 0;
              try
              {
                num7 = (uint) ProfileLoginWindow.GetSystemDefaultLangID();
              }
              catch (Exception ex)
              {
              }
              if (num7 == 1033U)
              {
                for (int index = 0; index < list.Count; ++index)
                {
                  if (list[index].Supportculture == str2 && list[index].KingdomsWorldID >= 900 && list[index].KingdomsWorldID < 1000 && list[index].NewWorld && list[index].Online && list[index].AvailableToJoin)
                  {
                    flag3 = true;
                    num1 = list[index].KingdomsWorldID;
                    flag2 = true;
                    break;
                  }
                }
                break;
              }
              break;
            case "es":
              uint num8 = 0;
              try
              {
                num8 = (uint) ProfileLoginWindow.GetSystemDefaultLangID();
              }
              catch (Exception ex)
              {
              }
              switch (num8)
              {
                case 2058:
                case 4106:
                case 5130:
                case 6154:
                case 7178:
                case 8202:
                case 9226:
                case 10250:
                case 11274:
                case 12298:
                case 13322:
                case 14346:
                case 15370:
                case 16394:
                case 17418:
                case 18442:
                case 19466:
                case 20490:
                case 21514:
                  for (int index = 0; index < list.Count; ++index)
                  {
                    if (list[index].Supportculture == "pt")
                    {
                      str2 = "pt";
                      break;
                    }
                  }
                  break;
              }
              break;
            case "pt":
              uint num9 = 0;
              try
              {
                num9 = (uint) ProfileLoginWindow.GetSystemDefaultLangID();
              }
              catch (Exception ex)
              {
              }
              if (num9 == 2070U)
              {
                str2 = "es";
                break;
              }
              bool flag4 = false;
              for (int index = 0; index < list.Count; ++index)
              {
                if (list[index].Supportculture == "pt")
                {
                  flag4 = true;
                  break;
                }
              }
              if (!flag4)
              {
                str2 = "es";
                break;
              }
              break;
          }
          if (!flag3)
          {
            bool flag5 = false;
            for (int index = 0; index < list.Count; ++index)
            {
              if (list[index].Supportculture == str2 && (str2 != "en" || list[index].KingdomsWorldID < 200))
              {
                if (list[index].KingdomsWorldID > num6 && (list[index].Online || !flag2) && list[index].AvailableToJoin)
                {
                  num1 = list[index].KingdomsWorldID;
                  num6 = list[index].KingdomsWorldID;
                  if (list[index].Online)
                    flag2 = true;
                }
                if (list[index].NewWorld && list[index].Online && list[index].AvailableToJoin)
                {
                  num1 = list[index].KingdomsWorldID;
                  flag5 = true;
                  break;
                }
              }
            }
            if (!flag5 && num3 >= 0)
              num1 = num3;
          }
        }
      }
      ProfileLoginWindow.LastNumberOfWorldsPlaying = num2;
      DateTime dateTime = new DateTime(2017, 1, 24, 15, 0, 0);
      CustomSelfDrawPanel.CSDLabel statusLinkLabel = new CustomSelfDrawPanel.CSDLabel();
      statusLinkLabel.Text = SK.Text("LOGIN_WORLDS_STATUS_PAGE", "Live Status Webpage");
      statusLinkLabel.Color = ARGBColors.Black;
      statusLinkLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      statusLinkLabel.Position = new Point(0, 175);
      statusLinkLabel.Size = new Size(this.WorldsPanelcontrols_LoggedOut.Width, 25);
      statusLinkLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      statusLinkLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.statusPageClicked));
      statusLinkLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => statusLinkLabel.Color = ARGBColors.Red), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => statusLinkLabel.Color = ARGBColors.Black));
      this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) statusLinkLabel);
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].KingdomsWorldID == num1 && (num2 == 0 || list[index].Playing))
        {
          CustomSelfDrawPanel.CSDLabel csdLabel1 = new CustomSelfDrawPanel.CSDLabel();
          CustomSelfDrawPanel.CSDImage csdImage1 = new CustomSelfDrawPanel.CSDImage();
          CustomSelfDrawPanel.CSDImage csdImage2 = new CustomSelfDrawPanel.CSDImage();
          CustomSelfDrawPanel.CSDLabel csdLabel2 = new CustomSelfDrawPanel.CSDLabel();
          CustomSelfDrawPanel.CSDLabel csdLabel3 = new CustomSelfDrawPanel.CSDLabel();
          csdLabel3.Text = str1;
          csdLabel3.Position = new Point(0, 10);
          csdLabel3.Size = new Size(this.WorldsPanelcontrols_LoggedIn.Width, 60);
          csdLabel3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          csdLabel3.Color = ARGBColors.Black;
          csdLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdLabel3);
          int num10 = 70;
          csdImage1.Y = num10;
          csdImage2.Y = num10;
          csdLabel1.Y = num10;
          csdLabel2.Y = num10;
          csdLabel1.Width = this.worldControlWidth + 8 + 8 + 8;
          csdLabel1.Height = this.worldControlHeight;
          csdLabel2.Width = this.worldControlWidth - 8;
          csdLabel2.Height = this.worldControlHeight;
          switch (list[index].Supportculture)
          {
            case "en":
              csdImage1.CustomTooltipID = 4001;
              break;
            case "de":
              csdImage1.CustomTooltipID = 4002;
              break;
            case "fr":
              csdImage1.CustomTooltipID = 4003;
              break;
            case "ru":
              csdImage1.CustomTooltipID = 4004;
              break;
            case "es":
              csdImage1.CustomTooltipID = 4016;
              break;
            case "pl":
              csdImage1.CustomTooltipID = 4020;
              break;
            case "tr":
              csdImage1.CustomTooltipID = 4023;
              break;
            case "it":
              csdImage1.CustomTooltipID = 4027;
              break;
            case "pt":
              csdImage1.CustomTooltipID = 4035;
              break;
            case "eu":
              csdImage1.CustomTooltipID = 4031;
              break;
            case "zh":
              csdImage1.CustomTooltipID = 4046;
              break;
          }
          switch (list[index].MapCulture)
          {
            case "en":
              csdImage2.CustomTooltipID = 4005;
              break;
            case "de":
              csdImage2.CustomTooltipID = 4006;
              break;
            case "fr":
              csdImage2.CustomTooltipID = 4007;
              break;
            case "ru":
              csdImage2.CustomTooltipID = 4008;
              break;
            case "es":
              csdImage2.CustomTooltipID = 4017;
              break;
            case "pl":
              csdImage2.CustomTooltipID = 4021;
              break;
            case "tr":
              csdImage2.CustomTooltipID = 4024;
              break;
            case "it":
              csdImage2.CustomTooltipID = 4028;
              break;
            case "us":
              csdImage2.CustomTooltipID = 4030;
              break;
            case "eu":
              csdImage2.CustomTooltipID = 4032;
              break;
            case "pt":
              csdImage2.CustomTooltipID = 4036;
              break;
            case "zh":
              csdImage2.CustomTooltipID = 4047;
              break;
            case "kg":
              csdImage2.CustomTooltipID = 4049;
              break;
            case "jp":
              csdImage2.CustomTooltipID = 4050;
              break;
            case "hy":
              csdImage2.CustomTooltipID = 4051;
              break;
            case "vk":
              csdImage2.CustomTooltipID = 4052;
              break;
            case "gd":
              csdImage2.CustomTooltipID = 4053;
              break;
            case "cru":
              csdImage2.CustomTooltipID = 4054;
              break;
            case "sparta":
              csdImage2.CustomTooltipID = 4055;
              break;
          }
          csdLabel1.Text = ProfileLoginWindow.getWorldShortDesc(list[index]);
          csdImage1.Image = !(list[index].Supportculture == "ph") ? (Image) GFXLibrary.getLoginWorldFlag(list[index].Supportculture) : (Image) GFXLibrary.getLoginWorldFlag("wd");
          csdImage1.Width = csdImage1.Image.Width;
          csdImage1.Height = csdImage1.Image.Height;
          csdImage2.Image = (Image) GFXLibrary.getLoginWorldMap(list[index].MapCulture);
          csdImage2.Width = csdImage2.Image.Width;
          csdImage2.Height = csdImage2.Image.Height;
          csdLabel1.X = 2;
          csdImage1.X = csdLabel1.X + csdLabel1.Width + 8 - 6;
          csdImage2.X = csdImage1.X + csdImage1.Width + 8;
          csdLabel2.X = csdImage2.X + csdImage2.Width + 8;
          if (list[index].Online)
          {
            csdLabel2.Text = this.strOnline;
            csdLabel2.Color = ARGBColors.Green;
            CustomSelfDrawPanel.CSDImage csdImage3 = new CustomSelfDrawPanel.CSDImage();
            this.allButtons.Add((CustomSelfDrawPanel.CSDControl) csdImage3);
            csdImage3.Width = this.worldControlWidth;
            csdImage3.Height = this.worldControlHeight;
            csdImage3.Y = num10;
            csdImage3.Tag = (object) list[index];
            csdImage3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnWorldAction_Click), "ProfileLoginWindow_enter_world");
            csdImage3.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.btnWorldAction_mouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.btnWorldAction_mouseOut));
            if (list[index].Playing)
              csdImage3.Image = this.PlayImage;
            else if (list[index].AvailableToJoin)
            {
              csdImage3.Image = this.JoinImage;
            }
            else
            {
              csdImage3.Image = this.ClosedImage;
              csdImage3.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
              csdImage3.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null, (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null);
            }
            csdImage3.Width = csdImage3.Image.Width;
            csdImage3.Height = csdImage3.Image.Height;
            csdImage3.X = this.pnlWorlds.Width - 4 - csdImage3.Width;
            this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage3);
            csdLabel2.CustomTooltipID = 4010;
          }
          else
          {
            if (list[index].KingdomsWorldID == 2502 && DateTime.UtcNow > dateTime)
            {
              csdLabel2.Text = this.strWorldEnded;
              csdLabel2.Width = 128;
            }
            else
            {
              csdLabel2.Text = this.strOffline;
              csdLabel2.Color = ARGBColors.Red;
            }
            csdLabel2.CustomTooltipID = 4009;
          }
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage1);
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage2);
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdLabel1);
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdLabel2);
          break;
        }
      }
      CustomSelfDrawPanel.CSDButton csdButton = new CustomSelfDrawPanel.CSDButton();
      csdButton.ImageNorm = this.SelectImage;
      csdButton.ImageOver = this.SelectImageOver;
      csdButton.Position = new Point(23, 120);
      csdButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ShowWorldSelect), "ProfileLoginWindow_show_worlds");
      this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdButton);
      if (ProfileLoginWindow.NewWorldsAvailable)
      {
        CustomSelfDrawPanel.CSDLabel csdLabel = new CustomSelfDrawPanel.CSDLabel();
        csdLabel.Text = SK.Text("LOGIN_New_Worlds", "A New World is available!");
        csdLabel.Color = ARGBColors.Green;
        csdLabel.Position = new Point(0, 155);
        csdLabel.Size = new Size(this.WorldsPanelcontrols_LoggedIn.Width, 60);
        csdLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        csdLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdLabel);
      }
      foreach (CustomSelfDrawPanel.CSDControl loggedInWorldControl in this.loggedInWorldControls)
        this.WorldsPanelcontrols_LoggedIn.addControl(loggedInWorldControl);
      this.WorldsPanelcontrols_LoggedIn.Invalidate(true);
    }

    private void btnWorldAction_mouseOver()
    {
      if (((WorldInfo) this.WorldsPanelcontrols_LoggedIn.OverControl.Tag).Playing)
        ((CustomSelfDrawPanel.CSDImage) this.WorldsPanelcontrols_LoggedIn.OverControl).Image = this.PlayImageOver;
      else
        ((CustomSelfDrawPanel.CSDImage) this.WorldsPanelcontrols_LoggedIn.OverControl).Image = this.JoinImageOver;
    }

    private void btnWorldAction_mouseOut()
    {
      if (((WorldInfo) this.WorldsPanelcontrols_LoggedIn.OverControl.Tag).Playing)
        ((CustomSelfDrawPanel.CSDImage) this.WorldsPanelcontrols_LoggedIn.OverControl).Image = this.PlayImage;
      else
        ((CustomSelfDrawPanel.CSDImage) this.WorldsPanelcontrols_LoggedIn.OverControl).Image = this.JoinImage;
    }

    private void btnWorldAction_Click()
    {
      WorldInfo tag = (WorldInfo) this.WorldsPanelcontrols_LoggedIn.ClickedControl.Tag;
      if (!tag.Online)
        return;
      this.serverAddr = tag.HostExt;
      Program.WorldName = ProfileLoginWindow.getWorldShortDesc(tag);
      RemoteServices.Instance.ProfileWorldID = tag.KingdomsWorldID;
      this.EnablePanels(false);
      this.JoinGameworld(new int?(tag.Playing ? 1 : 0), new int?(this.PlayerGameworldCount > 0 ? 0 : 1), new int?(tag.KingdomsWorldID), ProfileLoginWindow.getWorldShortDesc(tag));
    }

    public void btnWorldAction_Click(WorldInfo i)
    {
      if (!i.Online)
        return;
      this.serverAddr = i.HostExt;
      Program.WorldName = ProfileLoginWindow.getWorldShortDesc(i);
      RemoteServices.Instance.ProfileWorldID = i.KingdomsWorldID;
      this.EnablePanels(false);
      this.JoinGameworld(new int?(i.Playing ? 1 : 0), new int?(this.PlayerGameworldCount > 0 ? 0 : 1), new int?(i.KingdomsWorldID), ProfileLoginWindow.getWorldShortDesc(i));
    }

    public void BuildOfflineWorldList(List<WorldInfo> list)
    {
      if (this.loggedOutWorldControls != null && this.loggedOutWorldControls.Count > 0)
      {
        foreach (CustomSelfDrawPanel.CSDControl loggedOutWorldControl in this.loggedOutWorldControls)
          this.WorldsPanelcontrols_LoggedOut.removeControl(loggedOutWorldControl);
        this.loggedOutWorldControls.Clear();
      }
      else if (this.loggedOutWorldControls == null)
        this.loggedOutWorldControls = new List<CustomSelfDrawPanel.CSDControl>();
      int num = 0;
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].Online)
          ++num;
      }
      Color color = ARGBColors.Green;
      string str;
      if (num == 0)
      {
        str = SK.Text("LOGIN_ALL_OFFLINE", "All Worlds Offline");
        color = ARGBColors.Red;
      }
      else if (num == list.Count)
      {
        str = SK.Text("LOGIN_ALL_ONLINE", "All Worlds Online");
      }
      else
      {
        str = SK.Text("LOGIN_WORLDS_ONLINE", "Worlds Online : ") + num.ToString() + " / " + list.Count.ToString();
        color = ARGBColors.Black;
      }
      CustomSelfDrawPanel.CSDLabel csdLabel = new CustomSelfDrawPanel.CSDLabel();
      csdLabel.Text = str;
      csdLabel.Color = color;
      csdLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      csdLabel.Position = new Point(0, 0);
      csdLabel.Size = new Size(this.WorldsPanelcontrols_LoggedOut.Width, 80);
      csdLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.loggedOutWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdLabel);
      CustomSelfDrawPanel.CSDLabel statusLinkLabel = new CustomSelfDrawPanel.CSDLabel();
      statusLinkLabel.Text = SK.Text("LOGIN_WORLDS_STATUS_PAGE", "Live Status Webpage");
      statusLinkLabel.Color = ARGBColors.Black;
      statusLinkLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      statusLinkLabel.Position = new Point(0, 20);
      statusLinkLabel.Size = new Size(this.WorldsPanelcontrols_LoggedOut.Width, 25);
      statusLinkLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      statusLinkLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.statusPageClicked));
      statusLinkLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => statusLinkLabel.Color = ARGBColors.Red), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => statusLinkLabel.Color = ARGBColors.Black));
      this.loggedOutWorldControls.Add((CustomSelfDrawPanel.CSDControl) statusLinkLabel);
      foreach (CustomSelfDrawPanel.CSDControl loggedOutWorldControl in this.loggedOutWorldControls)
        this.WorldsPanelcontrols_LoggedOut.addControl(loggedOutWorldControl);
      this.WorldsPanelcontrols_LoggedOut.Invalidate(true);
    }

    public void GetOfflineWorlds()
    {
      XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).GetWorlds((IAuthRequest) new XmlRpcAuthRequest("", "", "", "", "", "", "", ""), new AuthEndResponseDelegate(this.GetOfflineWorldsCallback), (Control) this);
    }

    private void GetOfflineWorldsCallback(IAuthProvider sender, IAuthResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 1 : (!successCode.HasValue ? 1 : 0)) != 0)
      {
        this.throwClientErrorConnection(this.lblLoginError, response.Message);
      }
      else
      {
        ProfileLoginWindow.WorldList = ((XmlRpcAuthResponse) response).WorldList;
        ProfileLoginWindow.LanguageList = new Dictionary<string, LocalizationLanguage>();
        foreach (WorldInfo world in ProfileLoginWindow.WorldList)
        {
          if (world.KingdomsWorldID >= 1600 && world.KingdomsWorldID < 1700)
          {
            world.Supportculture = "en";
            world.MapCulture = "jp";
          }
          if (world.KingdomsWorldID >= 1700 && world.KingdomsWorldID < 1800)
          {
            world.Supportculture = "en";
            world.MapCulture = "hy";
          }
        }
        foreach (WorldInfo world in ProfileLoginWindow.WorldList)
        {
          if (!ProfileLoginWindow.LanguageList.ContainsKey(world.Supportculture))
            ProfileLoginWindow.LanguageList.Add(world.Supportculture, new LocalizationLanguage()
            {
              CultureCode = world.Supportculture
            });
        }
        if (Program.mySettings.NumWorldsCount < 0)
        {
          Program.mySettings.NumWorldsCount = ProfileLoginWindow.WorldList.Count;
          Program.mySettings.NumWorldsLastChanged = DateTime.MinValue;
          Program.mySettings.Save();
        }
        else if (Program.mySettings.NumWorldsCount != ProfileLoginWindow.WorldList.Count)
        {
          Program.mySettings.NumWorldsCount = ProfileLoginWindow.WorldList.Count;
          Program.mySettings.NumWorldsLastChanged = DateTime.Now;
          Program.mySettings.Save();
          ProfileLoginWindow.NewWorldsAvailable = true;
        }
        else if ((DateTime.Now - Program.mySettings.NumWorldsLastChanged).TotalDays < 7.0)
          ProfileLoginWindow.NewWorldsAvailable = true;
        if (DateTime.Now < new DateTime(2012, 2, 18))
          ProfileLoginWindow.NewWorldsAvailable = false;
        List<WorldInfo> bySupportCulture = this.GetWorldsBySupportCulture("");
        this.BuildOfflineWorldList(bySupportCulture);
        if (ProfileLoginWindow.NewWorldsAvailable)
        {
          if (this.lblNewWorlds == null)
            this.lblNewWorlds = new CustomSelfDrawPanel.CSDLabel();
          this.lblNewWorlds.Text = SK.Text("LOGIN_New_Worlds", "A New World is available!");
          this.lblNewWorlds.Color = ARGBColors.Green;
          this.lblNewWorlds.Position = new Point(10, this.LoginPanelControls_LoggedOut.Height - 70);
          this.lblNewWorlds.Size = new Size(this.LoginPanelControls_LoggedOut.Width - 20, 40);
          this.lblNewWorlds.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
          this.lblNewWorlds.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
          this.LoginPanelControls_LoggedOut.removeControl((CustomSelfDrawPanel.CSDControl) this.lblNewWorlds);
          this.LoginPanelControls_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.lblNewWorlds);
        }
        if (this.chkAutoLogin != null)
          this.chkAutoLogin.Visible = false;
        if (Program.mySettings.LastWorldID < 0)
          return;
        string str = "";
        foreach (WorldInfo world in bySupportCulture)
        {
          if (world.KingdomsWorldID == Program.mySettings.LastWorldID)
          {
            str = ProfileLoginWindow.getWorldShortDesc(world);
            break;
          }
        }
        if (str.Length <= 0)
          return;
        if (this.chkAutoLogin != null)
          this.LoginPanelControls_LoggedOut.removeControl((CustomSelfDrawPanel.CSDControl) this.chkAutoLogin);
        this.chkAutoLogin = new CustomSelfDrawPanel.CSDCheckBox();
        this.chkAutoLogin.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
        this.chkAutoLogin.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
        this.chkAutoLogin.Position = new Point(10, this.LoginPanelControls_LoggedOut.Height - 90);
        this.chkAutoLogin.Checked = Program.mySettings.AutoLogin;
        this.chkAutoLogin.CBLabel.Text = SK.Text("LOGIN_Auto_Load", "Auto Connect to : ") + str;
        this.chkAutoLogin.CBLabel.Color = ARGBColors.Black;
        this.chkAutoLogin.CBLabel.Position = new Point(20, -1);
        this.chkAutoLogin.CBLabel.Size = new Size(this.LoginPanelControls_LoggedOut.Width, 25);
        this.chkAutoLogin.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        this.chkAutoLogin.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.autoLoadToggled));
        this.chkAutoLogin.Visible = true;
        this.LoginPanelControls_LoggedOut.addControl((CustomSelfDrawPanel.CSDControl) this.chkAutoLogin);
        this.LoginPanelControls_LoggedOut.Invalidate();
      }
    }

    public void GetOnlineWorlds()
    {
      XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).GetWorlds((IAuthRequest) new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", "", "", "", ""), new AuthEndResponseDelegate(this.GetOnlineWorldsCallback), (Control) this);
    }

    private void GetOnlineWorldsCallback(IAuthProvider sender, IAuthResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 1 : (!successCode.HasValue ? 1 : 0)) != 0)
      {
        this.throwClientErrorConnection(this.lblLoginError, response.Message);
      }
      else
      {
        ProfileLoginWindow.WorldList = ((XmlRpcAuthResponse) response).WorldList;
        foreach (WorldInfo world in ProfileLoginWindow.WorldList)
        {
          if (world.KingdomsWorldID >= 1600 && world.KingdomsWorldID < 1700)
          {
            world.Supportculture = "en";
            world.MapCulture = "jp";
          }
          if (world.KingdomsWorldID >= 1700 && world.KingdomsWorldID < 1800)
          {
            world.Supportculture = "en";
            world.MapCulture = "hy";
          }
        }
        this.processVacationModeInfo(ProfileLoginWindow.WorldList);
        this.ShowOnlinePanels();
      }
    }

    private void processVacationModeInfo(List<WorldInfo> worldList)
    {
      ProfileLoginWindow.inSpecialWorld = false;
      ProfileLoginWindow.specialWorldName = "";
      foreach (WorldInfo world in worldList)
      {
        if (ProfileLoginWindow.isSpecialWorld(world.KingdomsWorldID) && world.Playing)
        {
          ProfileLoginWindow.inSpecialWorld = true;
          ProfileLoginWindow.specialWorldName = ProfileLoginWindow.getWorldShortDesc(world);
          break;
        }
      }
    }

    private void autoLoadToggled() => Program.mySettings.AutoLogin = this.chkAutoLogin.Checked;

    private void btnClientLogout_Click()
    {
      if (Program.bigpointPartnerInstall)
      {
        this.forumLabel.Visible = false;
        if (ProfileLoginWindow.bp2_logoutURL != null && ProfileLoginWindow.bp2_logoutURL.Length > 0)
          Process.Start(ProfileLoginWindow.bp2_logoutURL);
        RemoteServices.Instance.UserGuid = Guid.Empty;
        RemoteServices.Instance.SessionGuid = Guid.Empty;
        this.AdminGUID = (string) null;
        this.bp2_loginMode = 0;
        this.PlayerGameworldCount = 0;
        this.GetOfflineWorlds();
        this.RefreshControls();
        this.geckoWebBrowser1.Navigate(URLs.NewsMainPage);
        this.EnablePanels(true);
      }
      else
      {
        XmlRpcAuthProvider forEndpoint = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
        XmlRpcAuthRequest req = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), "", "", "");
        this.clearErrorMessages();
        forEndpoint.clientLogout((IAuthRequest) req, new AuthEndResponseDelegate(this.ClientLogoutCallback), (Control) this);
        this.EnablePanels(false);
        this.geckoWebBrowser1.Enabled = false;
        if (ProfileLoginWindow.LoggedInViaFacebook)
        {
          if (ProfileLoginWindow.tempBrowser == null)
          {
            ProfileLoginWindow.tempBrowser = new WebBrowser();
            ProfileLoginWindow.tempBrowser.Location = new Point(1000, 1000);
          }
          ProfileLoginWindow.tempBrowser.Navigate("https://login.strongholdkingdoms.com/facebook/logout.php?access_token=" + Program.mySettings.facebookaccesstoken);
          Thread.Sleep(1000);
          Program.mySettings.facebookaccesstoken = "";
        }
        ProfileLoginWindow.LoggedInViaFacebook = false;
        if (Program.aeriaInstall)
          this.openAeriaPopup(true);
        if (Program.bigpointInstall)
          this.openBigPointPopup(true);
        int num = Program.bigpointPartnerInstall ? 1 : 0;
      }
    }

    private void ClientLogoutCallback(IAuthProvider sender, IAuthResponse response)
    {
      RemoteServices.Instance.UserGuid = Guid.Empty;
      RemoteServices.Instance.SessionGuid = Guid.Empty;
      this.AdminGUID = (string) null;
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 1 : (!successCode.HasValue ? 1 : 0)) != 0)
        this.throwClientErrorConnection(this.lblLoginError, response.Message);
      this.PlayerGameworldCount = 0;
      this.GetOfflineWorlds();
      this.RefreshControls();
      this.geckoWebBrowser1.Navigate(URLs.NewsMainPage);
      this.EnablePanels(true);
      if (!Program.bigpointInstall && !Program.bigpointPartnerInstall)
        return;
      this.forumLabel.Visible = false;
    }

    public bool isPlayerLoggedIn()
    {
      Guid userGuid = RemoteServices.Instance.UserGuid;
      return RemoteServices.Instance.UserGuid != Guid.Empty;
    }

    public void RefreshControls()
    {
      bool flag = this.isPlayerLoggedIn();
      this.LoginPanelControls_LoggedIn.Visible = flag;
      this.WorldsPanelcontrols_LoggedIn.Visible = flag;
      this.LoginPanelControls_LoggedOut.Visible = !flag;
      this.WorldsPanelcontrols_LoggedOut.Visible = !flag;
      this.ShowTabs();
    }

    private void txtLoginField_Validate_email(object sender, EventArgs e)
    {
      this.btnLogin.Enabled = this.txtEmail.TextLength > 0 && this.txtPassword.TextLength > 0;
      if (this.chkAutoLogin == null || this.ignoreEmailChange)
        return;
      this.chkAutoLogin.Checked = false;
      Program.mySettings.AutoLogin = false;
    }

    private void txtLoginField_Validate(object sender, EventArgs e)
    {
      this.btnLogin.Enabled = this.txtEmail.TextLength > 0 && this.txtPassword.TextLength > 0;
    }

    public void clearErrorMessages() => this.lblLoginError.Visible = false;

    public void throwClientError(CustomSelfDrawPanel.CSDLabel l, string message)
    {
      l.Visible = true;
      l.Text = this.strGenericLoginError;
      l.X = 4;
      l.Y = this.LoginPanelControls_LoggedOut.Height - 30;
      l.Width = this.LoginPanelControls_LoggedOut.Width - 8;
      l.Height = 60;
      this.EnablePanels(true);
    }

    public void throwClientErrorConnection(CustomSelfDrawPanel.CSDLabel l, string message)
    {
      l.Visible = true;
      l.Text = this.strGenericLoginErrorConnection;
      l.X = 4;
      l.Y = this.LoginPanelControls_LoggedOut.Height - 30;
      l.Width = this.LoginPanelControls_LoggedOut.Width - 8;
      l.Height = 60;
      this.EnablePanels(true);
    }

    public void BP2_Closed()
    {
      this.btnLogin.Enabled = true;
      this.bp2_loginMode = 0;
      this.EnablePanels(true);
    }

    public void bp2_autoLoginAttempt()
    {
      this.bp2_loginMode = 1;
      this.btnLogin_Click();
    }

    public void bp2_manualLoginAttempt()
    {
      this.bp2_loginMode = 2;
      this.btnLogin_Click();
    }

    public void btnLogin_Click()
    {
      if (Program.bigpointPartnerInstall && this.bp2_loginMode == 0)
      {
        ProfileLoginWindow.bp2_currentGuid = Guid.NewGuid().ToString().Replace("-", "");
        Process.Start("https://api.bigpoint.com/oauth/authorize?response_type=code&client_id=strongholdkingdoms&redirect_uri=https://login.strongholdkingdoms.com/bigpoint/2/oauth2/authorized.php&state=" + ProfileLoginWindow.bp2_currentGuid);
        InterfaceMgr.Instance.openBPPopupWindow(this);
        this.btnLogin.Enabled = false;
      }
      else
      {
        ProfileLoginWindow.LoggedInViaFacebook = false;
        XmlRpcAuthProvider forEndpoint;
        if (Control.ModifierKeys != Keys.Shift)
        {
          ProfileLoginWindow.httpLogin = false;
          ShieldFactory.HTTP_only = false;
          if (!ProfileLoginWindow.certPolicyCreated)
          {
            ServicePointManager.ServerCertificateValidationCallback += (RemoteCertificateValidationCallback) ((sender, certificate, chain, sslPolicyErrors) =>
            {
              if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
              bool flag = true;
              string str1 = "The server could not be validated for the following reason(s):\r\n";
              if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) == SslPolicyErrors.RemoteCertificateNotAvailable)
              {
                str1 += "\r\n    -The server did not present a certificate.\r\n";
                flag = false;
              }
              else
              {
                if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch)
                {
                  str1 += "\r\n    -The certificate name does not match the authenticated name.\r\n";
                  flag = false;
                }
                if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors)
                {
                  foreach (X509ChainStatus chainStatu in chain.ChainStatus)
                  {
                    if (chainStatu.Status == X509ChainStatusFlags.RevocationStatusUnknown || chainStatu.Status == X509ChainStatusFlags.OfflineRevocation)
                    {
                      if (chainStatu.Status != X509ChainStatusFlags.NoError)
                      {
                        str1 = str1 + "\r\n    -" + chainStatu.StatusInformation;
                        flag = false;
                      }
                    }
                    else
                      break;
                  }
                }
              }
              if (!flag)
              {
                string str2 = str1 + "\r\nDo you wish to override the security check?";
                flag = true;
              }
              return flag;
            });
            ProfileLoginWindow.certPolicyCreated = true;
          }
          forEndpoint = XmlRpcAuthProvider.CreateForEndpoint("https", URLs.ProfileServerAddressLogin, "443", URLs.ProfilePath);
        }
        else
        {
          forEndpoint = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
          ProfileLoginWindow.httpLogin = true;
          ShieldFactory.HTTP_only = true;
        }
        XmlRpcAuthRequest req;
        if (Program.arcInstall)
          req = new XmlRpcAuthRequest("", Program.arcUsername, this.txtEmail.Text, this.txtPassword.Text, "", "", "", "");
        else if (!Program.kingdomsAccountFound || !Program.steamActive)
        {
          req = new XmlRpcAuthRequest("", this.txtEmail.Text, this.txtEmail.Text, this.txtPassword.Text, "", "", "", "");
        }
        else
        {
          req = new XmlRpcAuthRequest("", Program.steamEmail, Program.steamEmail, this.txtPassword.Text, "", "", "", "");
          Program.Steam_getTicket();
          string str = BitConverter.ToString(Program.steam_SessionTicket).Replace("-", "");
          req.SteamID = str;
        }
        try
        {
          if (Directory.Exists(Rot13.Transform("ObgFrggvatf")))
            req.OrderID += "u";
        }
        catch (Exception ex)
        {
        }
        if (Program.gamersFirstInstall)
        {
          req.SteamID = "gamersfirst";
          req.Password = Program.gamersFirstTokenMD5;
        }
        if (Program.arcInstall)
        {
          req.SteamID = "arc";
          if (Program.arcToken.Length < 5)
          {
            Thread.Sleep(2000);
            Program.arcToken = Program.getNewArcToken();
          }
          ProfileLoginWindow.AeriaToken = Program.arcToken;
        }
        if (Program.bigpointInstall)
          req.SteamID = "bp";
        if (!this.tempFacebookLogin)
        {
          this.FacebookToken = "";
          this.specialFacebookLogin = false;
        }
        if (Program.bigpointPartnerInstall)
        {
          req.SteamID = "bp2";
          ProfileLoginWindow.AeriaToken = ProfileLoginWindow.bp2_currentGuid;
        }
        req.AeriaToken = ProfileLoginWindow.AeriaToken;
        req.Platform = this.GetMacAddress();
        req.Culture = Program.syslang + ";" + Program.mySettings.LanguageIdent.ToLower();
        this.clearErrorMessages();
        this.EnablePanels(false);
        forEndpoint.clientLogin((IAuthRequest) req, new AuthEndResponseDelegate(this.ClientLoginCallback), (Control) this);
      }
    }

    private void ClientLoginCallback(IAuthProvider sender, IAuthResponse response)
    {
      int? successCode1 = response.SuccessCode;
      if ((successCode1.GetValueOrDefault() != 1 ? 1 : (!successCode1.HasValue ? 1 : 0)) != 0)
      {
        if (Program.bigpointPartnerInstall)
        {
          if (this.bp2_loginMode == 1)
          {
            BPPopupWindow bpPopupWindow = InterfaceMgr.Instance.getBPPopupWindow();
            if (bpPopupWindow == null)
              return;
            bpPopupWindow.attempt1Failed();
            this.EnablePanels(true);
            return;
          }
          if (this.bp2_loginMode == 2)
          {
            this.bp2_loginMode = 0;
            this.btnLogin.Enabled = true;
            InterfaceMgr.Instance.closeBPPopupWindow();
          }
        }
        if (this.FacebookToken == "")
        {
          GameEngine.Instance.playInterfaceSound("ProfileLoginWindow_login_failed");
          if (!(response.Message.ToLower() == "the provided password is incorrect.") && !(response.Message.ToLower() == "the specified user doesn't exist."))
          {
            int? successCode2 = response.SuccessCode;
            if ((successCode2.GetValueOrDefault() < 100 ? 0 : (successCode2.HasValue ? 1 : 0)) != 0)
            {
              int? successCode3 = response.SuccessCode;
              if ((successCode3.GetValueOrDefault() > 105 ? 0 : (successCode3.HasValue ? 1 : 0)) != 0)
                goto label_11;
            }
            this.throwClientErrorConnection(this.lblLoginError, response.Message);
            goto label_13;
          }
label_11:
          this.throwClientError(this.lblLoginError, response.Message);
label_13:
          if (Program.aeriaInstall)
            this.openAeriaPopup();
          if (Program.bigpointInstall)
            this.openBigPointPopup();
        }
        this.specialFacebookLogin = false;
      }
      else
      {
        ProfileLoginWindow.successfulAutoLogin = true;
        GameEngine.IsTOSLocked = !response.HasAcceptedNewTermsPolicy;
        GameEngine.Instance.playInterfaceSound("ProfileLoginWindow_login_success");
        if (Program.bigpointPartnerInstall)
        {
          ProfileLoginWindow.bp2_logoutURL = response.Password;
          InterfaceMgr.Instance.closeBPPopupWindow();
        }
        if (!(this.FacebookToken != ""))
          Program.mySettings.facebookaccesstoken = "";
        this.specialFacebookLogin = false;
        if (Program.gamersFirstInstall && response.Username.Length == 0)
        {
          this.EnablePanels(true);
          ProfileLoginWindow.gfEmail = this.txtEmail.Text;
          ProfileLoginWindow.gfPW = this.txtPassword.Text;
          this.delayedCreateUserOpen = true;
        }
        else if (Program.arcInstall && response.Username.Length == 0)
        {
          this.EnablePanels(true);
          ProfileLoginWindow.gfEmail = this.txtEmail.Text;
          ProfileLoginWindow.gfPW = this.txtPassword.Text;
          this.delayedCreateUserOpen = true;
        }
        else
        {
          Program.mySettings.Username = Program.aeriaInstall || Program.gamersFirstInstall || Program.arcInstall || Program.bigpointInstall ? response.Username : (Program.kingdomsAccountFound ? Program.steamEmail : this.txtEmail.Text);
          Program.mySettings.HasLoggedIn = true;
          ProfileLoginWindow.WorldList = ((XmlRpcAuthResponse) response).WorldList;
          foreach (WorldInfo world in ProfileLoginWindow.WorldList)
          {
            if (world.KingdomsWorldID >= 1600 && world.KingdomsWorldID < 1700)
            {
              world.Supportculture = "en";
              world.MapCulture = "jp";
            }
            if (world.KingdomsWorldID >= 1700 && world.KingdomsWorldID < 1800)
            {
              world.Supportculture = "en";
              world.MapCulture = "hy";
            }
          }
          this.processVacationModeInfo(ProfileLoginWindow.WorldList);
          ProfileLoginWindow.ShieldURL = ((XmlRpcAuthResponse) response).Shields;
          RemoteServices.Instance.UserGuid = new Guid(response.UserGUID);
          RemoteServices.Instance.SessionGuid = new Guid(response.SessionID);
          RemoteServices.Instance.WebToken = response.WebToken;
          RemoteServices.Instance.UserName = response.Username;
          if (!response.HasAcceptedNewTermsPolicy)
            this.OpenTOSPanel = true;
          string str = ((XmlRpcAuthResponse) response).SpecialURL;
          if (((XmlRpcAuthResponse) response).hasUnviewedOffers)
            str = URLs.AccountOffersPage;
          if (str.Length > 0)
            this.geckoWebBrowser1.Navigate(str + "?lang=" + Program.mySettings.LanguageIdent.ToLower() + "&culture=" + Program.mySettings.LanguageIdent.ToLower() + "&webtoken=" + response.WebToken);
          bool flag = false;
          if (response.OnVacation.HasValue)
          {
            int? onVacation = response.OnVacation;
            if ((onVacation.GetValueOrDefault() != 0 ? 1 : (!onVacation.HasValue ? 1 : 0)) != 0)
              flag = true;
          }
          if ((flag || GameEngine.IsTOSLocked) && this.chkAutoLogin != null && this.chkAutoLogin.Checked)
            this.chkAutoLogin.Checked = false;
          GameEngine.Instance.World.FacebookFreePack = false;
          if (response.FacebookFreePack.HasValue)
          {
            int? facebookFreePack = response.FacebookFreePack;
            if ((facebookFreePack.GetValueOrDefault() != 0 ? 1 : (!facebookFreePack.HasValue ? 1 : 0)) != 0)
              GameEngine.Instance.World.FacebookFreePack = true;
          }
          if (Program.bigpointInstall || Program.bigpointPartnerInstall)
            this.forumLabel.Visible = true;
          this.ShowOnlinePanels();
          GameEngine.Instance.World.NumVacationsAvailable = 2 - response.VacationsTaken.Value;
          GameEngine.Instance.World.VacationNot30Days = false;
          if (response.VacationPossible.HasValue)
          {
            int? vacationPossible1 = response.VacationPossible;
            if ((vacationPossible1.GetValueOrDefault() > 0 ? 0 : (vacationPossible1.HasValue ? 1 : 0)) != 0)
            {
              GameEngine.Instance.World.NumVacationsAvailable = 0;
              int? vacationPossible2 = response.VacationPossible;
              if ((vacationPossible2.GetValueOrDefault() != -1 ? 0 : (vacationPossible2.HasValue ? 1 : 0)) != 0)
                GameEngine.Instance.World.VacationNot30Days = true;
            }
          }
          else
            GameEngine.Instance.World.NumVacationsAvailable = 0;
          if (flag)
          {
            if (this.AdminGUID != null)
            {
              if (this.AdminGUID.Length != 0)
                goto label_64;
            }
            try
            {
              int secondsLeft = response.VacationSecondsLeft.Value;
              int secondsLeftToCancel = response.VacationSecondsToCancel.Value;
              bool canCancel = false;
              if (response.CancelVacation.HasValue)
              {
                int? cancelVacation = response.CancelVacation;
                if ((cancelVacation.GetValueOrDefault() != 0 ? 1 : (!cancelVacation.HasValue ? 1 : 0)) != 0)
                  canCancel = true;
              }
              InterfaceMgr.Instance.openVacationCancelPopupWindow(secondsLeft, secondsLeftToCancel, canCancel);
              return;
            }
            catch (Exception ex)
            {
            }
          }
label_64:
          if (response.RequiresOptInCheck.HasValue)
          {
            int? requiresOptInCheck = response.RequiresOptInCheck;
            int num = requiresOptInCheck.GetValueOrDefault() <= 0 ? 0 : (requiresOptInCheck.HasValue ? 1 : 0);
          }
          StatTrackingClient.Instance().Init("http://shk-data.strongholdkingdoms.com", RemoteServices.Instance.UserGuid.ToString());
          GameEngine.Instance.World.saleStartTime = response.SaleStartTime.Value;
          GameEngine.Instance.World.saleEndTime = response.SaleEndTime.Value;
          GameEngine.Instance.World.salePercentage = response.SalePercentage.Value;
          if (ProfileLoginWindow.httpLogin)
            PresetManager.Instance.SetPresetURL(response.PresetURL.Replace("https:", "http:"));
          else
            PresetManager.Instance.SetPresetURL(response.PresetURL.Replace("http:", "https:"));
        }
      }
    }

    /// <summary>
    /// Returns the connection info string shown on the login screen label.
    /// Fetches the public IP (3-second timeout); falls back to "unknown" if offline.
    /// </summary>
    internal static string GetConnectionInfo(string macAddress)
    {
      return "PHBot:\nMAC: " + macAddress + "\nIP:  " + GetPublicIP() + "\nClick to configure";
    }

    private static string GetPublicIP()
    {
      try
      {
        HttpWebRequest req = (HttpWebRequest) WebRequest.Create("http://checkip.amazonaws.com/");
        req.AutomaticDecompression = DecompressionMethods.GZip;
        req.Timeout = 3000;
        using (HttpWebResponse resp = (HttpWebResponse) req.GetResponse())
        using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
          return reader.ReadToEnd().Trim();
      }
      catch { return "unknown"; }
    }

    private string GetMacAddress()
    {
      if (!string.IsNullOrEmpty(OverrideMAC))
        return OverrideMAC;
      string macAddress = "";
      try
      {
        long num = -1;
        foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
          string str = networkInterface.GetPhysicalAddress().ToString();
          bool flag = true;
          switch (str.ToUpper())
          {
            case "020054554E01":
            case "00000000000000E0":
            case "005345000000":
            case "005056C00001":
            case "7A7900000000":
            case "002637BD3942":
            case "020054746872":
            case "582C80139263":
            case "00093BF01A40":
            case "00A0C6000000":
            case "000000000000":
            case "0000000000000000":
              flag = false;
              break;
          }
          if (flag && networkInterface.Speed > num && !string.IsNullOrEmpty(str) && str.Length >= 12)
          {
            num = networkInterface.Speed;
            macAddress = str;
          }
        }
      }
      catch (Exception ex)
      {
      }
      return macAddress;
    }

    public void ShowOnlinePanels()
    {
      this.LoadShieldImages();
      ProfileLoginWindow.LanguageList = new Dictionary<string, LocalizationLanguage>();
      foreach (WorldInfo world in ProfileLoginWindow.WorldList)
      {
        if (!ProfileLoginWindow.LanguageList.ContainsKey(world.Supportculture))
          ProfileLoginWindow.LanguageList.Add(world.Supportculture, new LocalizationLanguage()
          {
            CultureCode = world.Supportculture
          });
      }
      this.SetUsername();
      this.SetShieldImage();
      List<WorldInfo> bySupportCulture = this.GetWorldsBySupportCulture("");
      this.BuildOnlineWorldList(bySupportCulture);
      this.RefreshControls();
      this.EnablePanels(true);
      this.WorldsPanelcontrols_LoggedIn.Invalidate();
      if (this.chkAutoLogin == null || !this.chkAutoLogin.Checked)
        return;
      foreach (WorldInfo i in bySupportCulture)
      {
        if (i.KingdomsWorldID == Program.mySettings.LastWorldID && i.Playing)
        {
          this.btnWorldAction_Click(i);
          break;
        }
      }
    }

    public void cancelVacationMode()
    {
      XmlRpcCardsResponse rpcCardsResponse = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, "/services/cardserver.php").cancelVacation((ICardsRequest) new XmlRpcCardsRequest()
      {
        UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", ""),
        SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "")
      }, (CardsEndResponseDelegate) null, (Control) null, 30000);
      if (!rpcCardsResponse.SuccessCode.HasValue)
        return;
      int? successCode = rpcCardsResponse.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 1 : (!successCode.HasValue ? 1 : 0)) != 0)
        return;
      InterfaceMgr.Instance.closeVacationCancelPopupWindow();
    }

    public void CertOverrideFunction(object s, CertOverrideEventArgs e)
    {
      e.OverrideResult = CertOverride.Mismatch | CertOverride.Time | CertOverride.Untrusted;
      e.Temporary = true;
      e.Handled = true;
    }

    public ProfileLoginWindow()
    {
      this.InitializeComponent();
      this.geckoWebBrowser1.NoDefaultContextMenu = true;
      this.browserServerNews.NoDefaultContextMenu = true;
      CertOverrideService.GetService().ValidityOverride -= new EventHandler<CertOverrideEventArgs>(this.CertOverrideFunction);
      CertOverrideService.GetService().ValidityOverride += new EventHandler<CertOverrideEventArgs>(this.CertOverrideFunction);
      this.label1.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.Text = "Stronghold Kingdoms";
      this.defaultWindowTitle = this.Text;
    }

    private void ProfileLoginWindow_Load(object sender, EventArgs e)
    {
      GeckoPreferences.User["general.useragent.override"] = (object) this.Text;
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      System.Windows.Forms.Screen primaryScreen = System.Windows.Forms.Screen.PrimaryScreen;
      Point location = primaryScreen.WorkingArea.Location;
      location.X += (primaryScreen.WorkingArea.Width - this.Size.Width) / 2;
      location.Y += (primaryScreen.WorkingArea.Height - this.Size.Height) / 2;
      this.Location = location;
      RemoteServices.Instance.Admin = false;
      RemoteServices.Instance.MapEditor = false;
      RemoteServices.Instance.Moderator = false;
      this.lastLogoutClicked = DateTime.MinValue;
    }

    public void initialNavigate()
    {
      this.URL_landingPage = new Uri(URLs.NewsMainPage);
      if (RemoteServices.Instance.UserGuid == Guid.Empty)
      {
        this.geckoWebBrowser1.Navigate(this.URL_landingPage);
        this.browserServerNews.Navigate(URLs.ServerNewsFeed);
        if (Program.aeriaInstall)
          this.openAeriaPopup();
        if (Program.bigpointInstall)
          this.openBigPointPopup();
        this.loginButtonActive = true;
      }
      else
      {
        this.geckoWebBrowser1.Navigate(this.URL_landingPage);
        this.browserServerNews.Navigate(URLs.ServerNewsFeed);
        this.loginButtonActive = true;
      }
    }

    public void init()
    {
      this.OpenTOSPanel = false;
      GameEngine.Instance.forcingLogout = false;
      this.delayedCreateUserOpen = false;
      this.emailOptInPopup = false;
      this.strOnline = SK.Text("WORLD_Online", "Online");
      this.strOffline = SK.Text("WORLD_Offline", "Offline");
      this.strWorldEnded = SK.Text("WorldEnded", "This World has ended.");
      this.strJoin = SK.Text("WORLD_Join", "Join");
      this.strClosed = SK.Text("FactionInvites_Membership_closed", "Closed");
      this.strPlay = SK.Text("WORLD_Play", "Play");
      this.strSelect = SK.Text("WORLD_Select", "Select World");
      this.strEmailAddress = SK.Text("LOGIN_Email", "Email Address");
      this.strPassword = SK.Text("LOGIN_Password", "Password");
      this.strLogin = SK.Text("LOGIN_Login", "Login");
      this.strLogout = SK.Text("LogoutPanel_Logout", "Logout");
      this.strNews = SK.Text("LOGIN_News", "News");
      this.strCreateAccount = SK.Text("LOGIN_CreateAccount", "Create Account");
      this.strOptions = SK.Text("MENU_Settings", "Settings");
      this.strAccountDetails = SK.Text("LOGIN_AccountDetails", "Account Details");
      this.strGenericLoginError = SK.Text("LOGIN_GenericLoginError", "Login Failed: Please check that your email and password are entered correctly.");
      this.strGenericLoginErrorConnection = SK.Text("LOGIN_GenericLoginErrorConnection", "Login Failed: There is a problem connecting to the Login Server.");
      this.strForgottenPassword = SK.Text("LOGIN_ForgottenPassword", "Forgotten Password");
      this.strExit = SK.Text("GENERIC_Exit", "Exit");
      this.strCancel = SK.Text("GENERIC_Cancel", "Cancel");
      this.initialisedLanguage = Program.mySettings.LanguageIdent;
      this.UserEntryMode = true;
      this.btnExit.Text = SK.Text("GENERIC_Exit", "Exit");
      this.label1.Text = SK.Text("ProfileLoginWindow_Connecting", "Connecting to Login Server...");
      this.Focus();
      this.BringToFront();
      this.Activate();
      this.TopMost = true;
      this.browserServerNews.Visible = false;
      this.geckoWebBrowser1.Visible = false;
      Program.DoEvents();
      Thread.Sleep(100);
      Program.DoEvents();
      this.geckoWebBrowser1.Visible = true;
      this.browserServerNews.Visible = true;
      this.Focus();
      this.BringToFront();
      this.TopMost = false;
      this.Activate();
      this.BringToFront();
      this.ShowInTaskbar = false;
      this.AddControls();
      Program.DoEvents();
      this.ShowInTaskbar = true;
      if (this.isPlayerLoggedIn())
        this.GetOnlineWorlds();
      else
        this.GetOfflineWorlds();
      this.RefreshControls();
      string[] strArray = Regex.Split(Application.StartupPath, "\\\\");
      if (strArray.Length > 0)
      {
        this.lblVersion.Visible = true;
        this.lblVersion.Text = SK.Text("ProfileLoginWindow_Version", "Version") + " : " + strArray[strArray.Length - 1];
      }
      this.initialNavigate();
    }

    private void tbLoginUser_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      e.Handled = true;
    }

    private void tbLoginPassword_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      e.Handled = true;
    }

    private void LoginBeta(string userguid, string sessionguid, string email, string admin)
    {
      GameEngine.Instance.World.isBigpointAccount = false;
      this.connectingCancelled = false;
      ProfileLoginWindow.storedUserLoginEmail = email;
      IAuthProvider forEndpoint = (IAuthProvider) XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
      IAuthRequest req = (IAuthRequest) new XmlRpcAuthRequest(userguid, "", "", "", (string) null, "", "Kingdoms Client v0.xx", admin);
      req.SessionID = sessionguid;
      req.Admin = admin;
      req.WorldID = new int?(this.lastWorldLoggedIn);
      forEndpoint.LoginBetaUser(req, new AuthEndResponseDelegate(this.LoginCallback), (Control) this);
      Program.mySettings.Username = ProfileLoginWindow.storedUserLoginEmail;
      Program.mySettings.Save();
    }

    private void LoginCallback(IAuthProvider sender, IAuthResponse response)
    {
      if (this.connectingCancelled)
      {
        this.openAfterCancel();
        this.manageLoginButton();
      }
      else if (response.SuccessCode.HasValue && response.SuccessCode.Value == 1)
      {
        RemoteServices.Instance.BoxUser = response.PremiumBox.HasValue && response.PremiumBox.Value == 1;
        GameEngine.Instance.World.ProfileCrowns = response.Crowns.GetValueOrDefault();
        GameEngine.Instance.premiumTokenManager.ProfilePremiumCards = response.PremiumCards.GetValueOrDefault();
        GameEngine.Instance.World.ProfileCardpoints = response.Cardpoints.GetValueOrDefault();
        GameEngine.Instance.World.ProfileNumFriends = response.Friends.GetValueOrDefault();
        GameEngine.Instance.World.isBigpointAccount = response.isBigPoint;
        RemoteServices.Instance.MapEditor = false;
        GameEngine.Instance.World.MapEditing = false;
        GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Clear();
        foreach (int key in response.Tokens.Keys)
          GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Add(key, response.Tokens[key]);
        GameEngine.Instance.cardsManager.ProfileCards.Clear();
        foreach (int key in response.Cards.Keys)
          GameEngine.Instance.cardsManager.addProfileCard(key, response.Cards[key]);
        GameEngine.Instance.cardPackManager.ProfileCardOffers.Clear();
        foreach (int key in response.CardOffers.Keys)
          GameEngine.Instance.cardPackManager.ProfileCardOffers.Add(key, response.CardOffers[key]);
        GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Clear();
        foreach (int key in response.UserCardPacks.Keys)
          GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Add(key, response.UserCardPacks[key]);
        bool[] flagArray1 = new bool[10];
        bool[] flagArray2 = flagArray1;
        int num1;
        if (((XmlRpcAuthResponse) response).VeteranLevel1.HasValue)
        {
          int? veteranLevel1 = ((XmlRpcAuthResponse) response).VeteranLevel1;
          num1 = veteranLevel1.GetValueOrDefault() != 1 ? 0 : (veteranLevel1.HasValue ? 1 : 0);
        }
        else
          num1 = 0;
        flagArray2[0] = num1 != 0;
        bool[] flagArray3 = flagArray1;
        int num2;
        if (((XmlRpcAuthResponse) response).VeteranLevel2.HasValue)
        {
          int? veteranLevel2 = ((XmlRpcAuthResponse) response).VeteranLevel2;
          num2 = veteranLevel2.GetValueOrDefault() != 1 ? 0 : (veteranLevel2.HasValue ? 1 : 0);
        }
        else
          num2 = 0;
        flagArray3[1] = num2 != 0;
        bool[] flagArray4 = flagArray1;
        int num3;
        if (((XmlRpcAuthResponse) response).VeteranLevel3.HasValue)
        {
          int? veteranLevel3 = ((XmlRpcAuthResponse) response).VeteranLevel3;
          num3 = veteranLevel3.GetValueOrDefault() != 1 ? 0 : (veteranLevel3.HasValue ? 1 : 0);
        }
        else
          num3 = 0;
        flagArray4[2] = num3 != 0;
        bool[] flagArray5 = flagArray1;
        int num4;
        if (((XmlRpcAuthResponse) response).VeteranLevel4.HasValue)
        {
          int? veteranLevel4 = ((XmlRpcAuthResponse) response).VeteranLevel4;
          num4 = veteranLevel4.GetValueOrDefault() != 1 ? 0 : (veteranLevel4.HasValue ? 1 : 0);
        }
        else
          num4 = 0;
        flagArray5[3] = num4 != 0;
        bool[] flagArray6 = flagArray1;
        int num5;
        if (((XmlRpcAuthResponse) response).VeteranLevel5.HasValue)
        {
          int? veteranLevel5 = ((XmlRpcAuthResponse) response).VeteranLevel5;
          num5 = veteranLevel5.GetValueOrDefault() != 1 ? 0 : (veteranLevel5.HasValue ? 1 : 0);
        }
        else
          num5 = 0;
        flagArray6[4] = num5 != 0;
        bool[] flagArray7 = flagArray1;
        int num6;
        if (((XmlRpcAuthResponse) response).VeteranLevel6.HasValue)
        {
          int? veteranLevel6 = ((XmlRpcAuthResponse) response).VeteranLevel6;
          num6 = veteranLevel6.GetValueOrDefault() != 1 ? 0 : (veteranLevel6.HasValue ? 1 : 0);
        }
        else
          num6 = 0;
        flagArray7[5] = num6 != 0;
        bool[] flagArray8 = flagArray1;
        int num7;
        if (((XmlRpcAuthResponse) response).VeteranLevel7.HasValue)
        {
          int? veteranLevel7 = ((XmlRpcAuthResponse) response).VeteranLevel7;
          num7 = veteranLevel7.GetValueOrDefault() != 1 ? 0 : (veteranLevel7.HasValue ? 1 : 0);
        }
        else
          num7 = 0;
        flagArray8[6] = num7 != 0;
        bool[] flagArray9 = flagArray1;
        int num8;
        if (((XmlRpcAuthResponse) response).VeteranLevel8.HasValue)
        {
          int? veteranLevel8 = ((XmlRpcAuthResponse) response).VeteranLevel8;
          num8 = veteranLevel8.GetValueOrDefault() != 1 ? 0 : (veteranLevel8.HasValue ? 1 : 0);
        }
        else
          num8 = 0;
        flagArray9[7] = num8 != 0;
        bool[] flagArray10 = flagArray1;
        int num9;
        if (((XmlRpcAuthResponse) response).VeteranLevel9.HasValue)
        {
          int? veteranLevel9 = ((XmlRpcAuthResponse) response).VeteranLevel9;
          num9 = veteranLevel9.GetValueOrDefault() != 1 ? 0 : (veteranLevel9.HasValue ? 1 : 0);
        }
        else
          num9 = 0;
        flagArray10[8] = num9 != 0;
        bool[] flagArray11 = flagArray1;
        int num10;
        if (((XmlRpcAuthResponse) response).VeteranLevel10.HasValue)
        {
          int? veteranLevel10 = ((XmlRpcAuthResponse) response).VeteranLevel10;
          num10 = veteranLevel10.GetValueOrDefault() != 1 ? 0 : (veteranLevel10.HasValue ? 1 : 0);
        }
        else
          num10 = 0;
        flagArray11[9] = num10 != 0;
        bool[] stages = flagArray1;
        if (((XmlRpcAuthResponse) response).VeteranLevel6.HasValue)
        {
          int? veteranLevel6 = ((XmlRpcAuthResponse) response).VeteranLevel6;
          if ((veteranLevel6.GetValueOrDefault() != 2 ? 0 : (veteranLevel6.HasValue ? 1 : 0)) != 0)
          {
            GameEngine.Instance.World.InviteSystemNotImplemented = true;
            goto label_57;
          }
        }
        GameEngine.Instance.World.InviteSystemNotImplemented = false;
label_57:
        if (((XmlRpcAuthResponse) response).VeteranCurrentLevel.HasValue && ((XmlRpcAuthResponse) response).VeteranSecondsLeft.HasValue)
          GameEngine.Instance.World.importFreeCardData(((XmlRpcAuthResponse) response).VeteranCurrentLevel.Value, stages, DateTime.Now.AddSeconds((double) ((XmlRpcAuthResponse) response).VeteranSecondsLeft.Value), DateTime.Now);
        GameEngine.Instance.cardsManager.calcAvailableCategories();
        FreeCardsData.veteranFreeCardsDuration = response.FreeCardDurations;
        RemoteServices.Instance.UserGuid = new Guid(response.UserGUID);
        RemoteServices.Instance.SessionGuid = new Guid(response.SessionID);
        URLs.GameRPCAddress = this.serverAddr;
        URLs.ChatRPCAddress = this.serverAddr;
        RemoteServices.Instance.init(URLs.GameRPC);
        RemoteServices.Instance.set_LoginUserGuid_UserCallBack(new RemoteServices.LoginUserGuid_UserCallBack(this.loginUserGuid));
        RemoteServices.Instance.LoginUserGuid(ProfileLoginWindow.storedUserLoginEmail, RemoteServices.Instance.UserGuid, RemoteServices.Instance.SessionGuid);
        this.Cursor = Cursors.WaitCursor;
      }
      else if (response.Message == "On Vacation.")
      {
        int num = (int) MyMessageBox.Show(SK.Text("Login_Vacation_Error", "Your Account is currently in Vacation Mode and you are not able to log into this game world at this time."), SK.Text("ProfileLoginWindow_Login_Error", "Login Error"));
        this.btnExit_Click();
      }
      else
      {
        int num = (int) MyMessageBox.Show(response.Message, SK.Text("ProfileLoginWindow_Login_Error", "Login Error"));
        this.openAfterCancel();
        this.manageLoginButton();
      }
    }

    private void RequestGameWorlds(string SessionID, string UserID)
    {
      this.loginButtonActive = false;
      IDictionary<string, string> postVars = (IDictionary<string, string>) new Dictionary<string, string>();
      postVars.Add(new KeyValuePair<string, string>("uid", UserID));
      postVars.Add(new KeyValuePair<string, string>("sid", SessionID));
      this.geckoWebBrowser1.Navigate(this.URL_gameWorldPage, postVars);
    }

    public void selfClose() => this.selfClosing = true;

    private void btnExit_Click(object sender, EventArgs e)
    {
      RemoteServices.Instance.UserID = -1;
      this.selfClosing = true;
      this.Close();
    }

    public void btnExit_Click()
    {
      RemoteServices.Instance.UserID = -1;
      this.selfClosing = true;
      this.Close();
    }

    private void geckoWebBrowser1_ClientFeedback(object sender, EventArgs e)
    {
      this.EnablePanels(true);
      this.geckoWebBrowser1.Enabled = true;
      bool flag1 = false;
      bool flag2 = false;
      string sessionguid = string.Empty;
      string userguid = string.Empty;
      string email = string.Empty;
      string admin1 = string.Empty;
      string str1 = string.Empty;
      string admin2 = string.Empty;
      string guid = string.Empty;
      string sessionid = string.Empty;
      string str2 = string.Empty;
      string str3 = "";
      foreach (string key in (IEnumerable<string>) this.geckoWebBrowser1.PageValues.Keys)
      {
        str3 = str3 + key + " : " + this.geckoWebBrowser1.PageValues[key] + " ";
        if (key != "")
        {
          string pageValue1 = this.geckoWebBrowser1.PageValues[key];
          if (key.ToLowerInvariant() == "errorcode" || key.ToLowerInvariant() == "errorCode")
          {
            if (this.getInt32FromString(pageValue1) != 1)
              flag2 = true;
          }
          else if (key.Trim().ToLowerInvariant() == "switchadmin")
            admin2 = pageValue1;
          else if (key.Trim().ToLowerInvariant() == "switchuser")
            guid = pageValue1;
          else if (key.Trim().ToLowerInvariant() == "switchsession")
            sessionid = pageValue1;
          else if (key.Trim().ToLowerInvariant() == "switchemail")
            str2 = pageValue1;
          else if (key.ToLowerInvariant() == "server" || key.ToLowerInvariant() == " server")
          {
            flag1 = true;
            this.serverAddr = pageValue1;
          }
          else if (key.Trim().ToLowerInvariant() == "email")
            email = pageValue1;
          else if (key.Trim().ToLowerInvariant() == "sessionguid")
            sessionguid = pageValue1;
          else if (key.Trim().ToLowerInvariant() == "userguid")
            userguid = pageValue1;
          else if (key.Trim().ToLowerInvariant() == "adminguid")
            admin1 = pageValue1;
          else if (key.Trim().ToLowerInvariant() == "worldname")
            str1 = pageValue1.Replace('+', ' ');
          else if (key.Trim().ToLowerInvariant() == "openlink")
            Process.Start("https://" + pageValue1.Replace("%2F", "/"));
          else if (key.Trim().ToLowerInvariant() == "selectedworldid")
          {
            string pageValue2 = this.geckoWebBrowser1.PageValues[key];
            int num;
            try
            {
              num = int.Parse(pageValue2, (IFormatProvider) CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
              num = -1;
            }
            RemoteServices.Instance.ProfileWorldID = num;
          }
        }
      }
      if (admin2.Length > 0 && guid.Length > 0 && sessionid.Length > 0)
      {
        this.geckoWebBrowser1.Document.Cookie = "";
        this.AdminGUID = admin2;
        XmlRpcAuthProvider forEndpoint = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
        XmlRpcAuthRequest req = new XmlRpcAuthRequest(guid, str2, str2, "", sessionid, "", "", admin2);
        this.clearErrorMessages();
        forEndpoint.clientLogin((IAuthRequest) req, new AuthEndResponseDelegate(this.ClientLoginCallback), (Control) this);
        this.EnablePanels(false);
      }
      if (RemoteServices.Instance.ProfileWorldID == 0 || RemoteServices.Instance.ProfileWorldID == -1)
        RemoteServices.Instance.ProfileWorldID = 5;
      if (flag1 && !flag2 && this.serverAddr.Length > 0)
      {
        if (str1 != string.Empty)
          Program.WorldName = str1;
        this.LoginBeta(userguid, sessionguid, email, admin1);
      }
      else
      {
        if (!this.geckoWebBrowser1.Url.AbsoluteUri.ToLowerInvariant().Contains("gotogameworld"))
          return;
        if (str3.Length > 0)
        {
          int num1 = (int) MyMessageBox.Show(SK.Text("ProfileLoginWindow_Connection_Problem", "There was a problem logging in, please check that Kingdoms is not blocked by your firewall or proxy.") + Environment.NewLine + str3);
        }
        else
        {
          int num2 = (int) MyMessageBox.Show(SK.Text("ProfileLoginWindow_Connection_Problem", "There was a problem logging in, please check that Kingdoms is not blocked by your firewall or proxy.") + Environment.NewLine + SK.Text("ProfileLoginWindow_No_Cookies_Written", "No cookies written!"));
        }
        this.RequestGameWorlds(RemoteServices.Instance.SessionGuid.ToString("N"), RemoteServices.Instance.UserGuid.ToString("N"));
      }
    }

    private void kingdomsBrowserIE1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
    {
    }

    public int getInt32FromString(string text)
    {
      if (text.Length == 0)
        return 0;
      try
      {
        return Convert.ToInt32(text);
      }
      catch (Exception ex)
      {
      }
      return 0;
    }

    private void loginUserGuid(LoginUserGuid_ReturnType returnData)
    {
      if (this.connectingCancelled)
      {
        this.openAfterCancel();
        this.manageLoginButton();
      }
      else
      {
        this.Cursor = Cursors.Default;
        if (returnData.Success && returnData.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.OK)
        {
          RemoteServices.Instance.UserID = returnData.m_userID;
          if (returnData.m_requiresVerification)
          {
            RemoteServices.Instance.RequiresVerification = true;
          }
          else
          {
            RemoteServices.Instance.RequiresVerification = false;
            ProfileLoginWindow.installLoginInfo(returnData);
          }
          RemoteServices.Instance.UserName = returnData.m_userName;
          RemoteServices.Instance.RealName = returnData.m_realName;
          this.UserEntryMode = false;
          this.lblRetrieving.Visible = true;
          this.tandcLabel.Visible = false;
          this.gameRulesLabel.Visible = false;
          this.forumLabel.Visible = false;
          this.supportLabel.Visible = false;
          this.feedbackProgress.Size = new Size(Math.Min(this.pnlFeedback.Width / 11, this.pnlFeedback.Width), this.pnlFeedback.Height);
          this.feedbackProgressArea.invalidate();
          this.cancelButton.Visible = true;
          this.lastCount = -1;
          GameEngine.Instance.cardsManager.addRecentCardsFromServer(returnData.m_RecentCards);
        }
        else
        {
          int num = (int) MyMessageBox.Show(returnData.m_errorCode != CommonTypes.ErrorCodes.ErrorCode.VACATION_MODE_LOGIN_NOT_ALLOWED ? (returnData.m_errorCode != CommonTypes.ErrorCodes.ErrorCode.LOGIN_SERVER_INACTIVE ? (returnData.m_errorCode != CommonTypes.ErrorCodes.ErrorCode.LOGIN_SQL_CONNECTION_ISSUE ? SK.Text("ProfileLoginWindow_Login_User_Failed", "Login User Failed") + Environment.NewLine + CommonTypes.ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID) : SK.Text("ProfileLoginWindow_Connection_Problems", "Connection problems, please try again a little later.")) : returnData.m_maintenanceMessage) : SK.Text("ProfileLoginWindow_Vacation_Mode_Active", "You cannot login, Vacation Mode is still active."), SK.Text("ProfileLoginWindow_Login_User_Error", "Login User Error"));
          if (returnData.m_errorCode == CommonTypes.ErrorCodes.ErrorCode.LOGIN_NEW_VERSION)
          {
            RemoteServices.Instance.UserID = -1;
            this.selfClosing = true;
            this.Close();
          }
          else
          {
            this.openAfterCancel();
            this.manageLoginButton();
          }
        }
      }
    }

    public List<WorldInfo> GetAllPlayedWorlds()
    {
      List<WorldInfo> allPlayedWorlds = new List<WorldInfo>();
      foreach (WorldInfo world in ProfileLoginWindow.WorldList)
      {
        if (world.Playing)
          allPlayedWorlds.Add(world);
      }
      return allPlayedWorlds;
    }

    public List<WorldInfo> GetNonPlayedBySupportCulture(string culture)
    {
      List<WorldInfo> bySupportCulture = new List<WorldInfo>();
      foreach (WorldInfo world in ProfileLoginWindow.WorldList)
      {
        if (!world.Playing && (world.Supportculture == culture || culture == "" || world.Supportculture == "eu" && culture != "pt" && culture != "wd" || world.Supportculture == "pt" && culture == "es" || world.Supportculture == "wd" || world.Supportculture == "kg" || world.Supportculture == "vk" || world.Supportculture == "gd" || world.Supportculture == "cru" || world.Supportculture == "sp" || world.Supportculture == "ph" || world.Supportculture == "ch" || world.Supportculture == "zh" || world.KingdomsWorldID >= 2500 && world.KingdomsWorldID <= 2599))
          bySupportCulture.Add(world);
      }
      if (culture != "")
        ProfileLoginWindow.LastSelectedSupportCulture = culture;
      return bySupportCulture;
    }

    public List<WorldInfo> GetWorldsBySupportCulture(string culture)
    {
      return this.GetWorldsBySupportCulture(culture, true, 0);
    }

    public List<WorldInfo> GetWorldsBySupportCulture(
      string culture,
      bool includeOwn,
      int specialMode)
    {
      if (specialMode == 2)
        culture = "";
      List<WorldInfo> bySupportCulture = new List<WorldInfo>();
      foreach (WorldInfo world in ProfileLoginWindow.WorldList)
      {
        bool flag1 = ProfileLoginWindow.isSpecialWorld(world.KingdomsWorldID);
        bool flag2 = ProfileLoginWindow.isAIWorld(world.KingdomsWorldID);
        bool flag3 = false;
        if (specialMode == 0)
          flag3 = true;
        else if (specialMode == -1 && !flag1 && !flag2)
          flag3 = true;
        else if (specialMode == 1 && flag1)
          flag3 = true;
        else if (specialMode == 2 && flag2)
          flag3 = true;
        if (flag3 && (world.Supportculture == culture || world.Playing && includeOwn || culture == "" || world.Supportculture == "eu" && culture != "pt" && culture != "wd" || world.Supportculture == "pt" && culture == "es" || (world.Supportculture == "wd" || world.Supportculture == "kg" || world.Supportculture == "vk" || world.Supportculture == "gd" || world.Supportculture == "cru" || world.Supportculture == "sp") && culture != "eu"))
        {
          bySupportCulture.Add(world);
          if (world.Playing)
            ++this.PlayerGameworldCount;
        }
      }
      bySupportCulture.Sort((Comparison<WorldInfo>) ((a, b) =>
      {
        if ((a.Supportculture == culture || a.Supportculture == "kg") && b.Supportculture != culture && b.Supportculture != "kg")
          return -1;
        if ((b.Supportculture == culture || b.Supportculture == "kg") && a.Supportculture != culture && a.Supportculture != "kg")
          return 1;
        if ((a.Supportculture == culture || a.Supportculture == "wd") && b.Supportculture != culture && b.Supportculture != "wd")
          return -1;
        if ((b.Supportculture == culture || b.Supportculture == "wd") && a.Supportculture != culture && a.Supportculture != "wd")
          return 1;
        if ((a.Supportculture == culture || a.Supportculture == "eu") && b.Supportculture != culture && b.Supportculture != "eu")
          return -1;
        if ((b.Supportculture == culture || b.Supportculture == "eu") && a.Supportculture != culture && a.Supportculture != "eu")
          return 1;
        if ((a.Supportculture == culture || a.Supportculture == "vk") && b.Supportculture != culture && b.Supportculture != "vk")
          return -1;
        if ((b.Supportculture == culture || b.Supportculture == "vk") && a.Supportculture != culture && a.Supportculture != "vk")
          return 1;
        if ((a.Supportculture == culture || a.Supportculture == "gd") && b.Supportculture != culture && b.Supportculture != "gd")
          return -1;
        if ((b.Supportculture == culture || b.Supportculture == "gd") && a.Supportculture != culture && a.Supportculture != "gd")
          return 1;
        if ((a.Supportculture == culture || a.Supportculture == "cru") && b.Supportculture != culture && b.Supportculture != "cru")
          return -1;
        if ((b.Supportculture == culture || b.Supportculture == "cru") && a.Supportculture != culture && a.Supportculture != "cru")
          return 1;
        if ((a.Supportculture == culture || a.Supportculture == "sp") && b.Supportculture != culture && b.Supportculture != "sp")
          return -1;
        if ((b.Supportculture == culture || b.Supportculture == "sp") && a.Supportculture != culture && a.Supportculture != "sp")
          return 1;
        if (a.Playing != b.Playing)
          return b.Playing.CompareTo(a.Playing);
        if (a.Supportculture != b.Supportculture)
        {
          if (a.Supportculture == culture)
            return -1;
          if (b.Supportculture == culture)
            return 1;
          if (a.Supportculture == "eu")
            return -1;
          if (b.Supportculture == "eu")
            return 1;
        }
        return a.KingdomsWorldID.CompareTo(b.KingdomsWorldID);
      }));
      if (culture != "")
        ProfileLoginWindow.LastSelectedSupportCulture = culture;
      return bySupportCulture;
    }

    public static bool isSpecialWorld(int worldID) => false;

    public static bool isAIWorld(int worldID)
    {
      return worldID >= 2500 && worldID < 2599 || worldID >= 3500 && worldID < 3599 || worldID >= 1300 && worldID < 1399 || worldID >= 1200 && worldID < 1299 || worldID >= 1400 && worldID < 1499;
    }

    public static void installLoginInfo(LoginUserGuid_ReturnType returnData)
    {
      GameEngine.Instance.World.registerWorldIdentifier(returnData.m_worldID);
      GameEngine.Instance.World.initWorldMap(returnData.m_worldMapType);
      GameEngine.Instance.World.downloadWorldShields(returnData.m_worldID);
      StatTrackingClient.Instance().ActivateTrigger(8, (object) returnData.m_worldID);
      StatTrackingClient.Instance().ActivateTrigger(14, (object) ProfileLoginWindow.LastNumberOfWorldsPlaying);
      VillageMap.setServerTime(returnData.m_currentTime);
      RemoteServices.Instance.SessionID = returnData.m_sessionID;
      RemoteServices.Instance.WorldGUID = new Guid(returnData.m_worldIdentity);
      RemoteServices.Instance.UserFactionID = returnData.m_userFactionID;
      RemoteServices.Instance.Admin = returnData.m_admin;
      RemoteServices.Instance.Moderator = returnData.m_moderator;
      AdminInfoPopup.setMessage(returnData.m_adminMessage);
      RemoteServices.Instance.ShowAdminMessage = returnData.m_showAdminMessage;
      RemoteServices.Instance.Show2ndAgeMessage = returnData.m_show2ndAgeMessage;
      RemoteServices.Instance.Show3rdAgeMessage = returnData.m_show3rdAgeMessage;
      RemoteServices.Instance.Show4thAgeMessage = returnData.m_show4thAgeMessage;
      RemoteServices.Instance.Show5thAgeMessage = returnData.m_show5thAgeMessage;
      RemoteServices.Instance.ReportFilters = returnData.m_reportFilters;
      GameEngine.Instance.initWorldData(returnData.m_worldData);
      if (returnData.m_villageLayoutData != null)
        VillageMap.villageLayout = returnData.m_villageLayoutData;
      if (returnData.m_villageBuildingData != null)
        VillageMap.villageBuildingData = returnData.m_villageBuildingData;
      MarketTransferPanel.addHistory(returnData.m_tradeVillageHistory);
      StockExchangePanel.addHistory(returnData.m_stockExchangeHistory);
      AttackTargetsPanel.addHistory(returnData.m_attackTargetHistory);
      MarketTransferPanel.addFavourites(returnData.m_tradeVillageFavourites);
      StockExchangePanel.addFavourites(returnData.m_stockExchangeFavourites);
      AttackTargetsPanel.addFavourites(returnData.m_attackTargetFavourites);
      if (returnData.FlushCache)
        GameEngine.Instance.World.flushCaches();
      GameEngine.Instance.World.setResearchData(returnData.m_researchData);
      GameEngine.Instance.World.setWorldStartDate(returnData.m_gameStartDate);
      GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
      GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
      RemoteServices.Instance.LoginLeaderInfo = returnData.m_leaderInfo;
      RemoteServices.Instance.UserAvatar = returnData.m_avatarData;
      InterfaceMgr.Instance.processAchievements(returnData.m_achievements);
      GameEngine.Instance.cardsManager.UserCardData = returnData.m_cardData;
      InterfaceMgr.Instance.clearStoredMail();
      GameEngine.Instance.World.resetParishTextReadID();
      RemoteServices.Instance.UserOptions = returnData.m_gameOptions;
      GameEngine.Instance.World.HouseGloryPoints = returnData.m_gloryPoints;
      GameEngine.Instance.World.HouseGloryRoundData = returnData.gloryRoundData;
      GameEngine.Instance.World.setTutorialInfo(returnData.m_questsAndTutorialInfo);
      GameEngine.Instance.World.MostAge4Villages = returnData.mostAge4Villages;
      GameEngine.Instance.World.setTickets(0, returnData.wheel_Treasure1Tickets);
      GameEngine.Instance.World.setTickets(1, returnData.wheel_Treasure2Tickets);
      GameEngine.Instance.World.setTickets(2, returnData.wheel_Treasure3Tickets);
      GameEngine.Instance.World.setTickets(3, returnData.wheel_Treasure4Tickets);
      GameEngine.Instance.World.setTickets(4, returnData.wheel_Treasure5Tickets);
      if (returnData.m_questCompleted != -1)
        GameEngine.Instance.World.addCompletedQuestObjectives(returnData.m_questCompleted);
      GameEngine.Instance.World.setLastTreasureCastleAttackTime(returnData.lastTreasureCastleAttackTime);
      GameEngine.Instance.initCensorText(returnData.words);
      InterfaceMgr.Instance.getMainMenuBar().setAdmin();
      InterfaceMgr.Instance.chatLogin();
      GameEngine.Instance.tryingToJoinCounty = returnData.tryingToJoinCounty;
      GameEngine.Instance.setServerDownTime(returnData.serverDownTime);
      GameEngine.Instance.World.setNewQuestData(returnData.m_newQuestsData);
      GameEngine.Instance.World.setTickets(-1, returnData.m_numQuestTickets);
      NewQuestsPanel.handleClientSideQuestReporting(false);
      GameEngine.Instance.World.lastAttacker = returnData.m_lastAttacker;
      GameEngine.Instance.World.newPlayer = returnData.m_newPlayer;
      GameEngine.Instance.World.lastAttackerLastUpdate = DateTime.Now;
      if (returnData.m_worldData != null && returnData.m_worldData.AIWorld)
      {
        if (returnData.m_secondAgeWorld)
          GameEngine.Instance.World.WorldEnded = true;
        GameEngine.Instance.World.SecondAgeWorld = false;
        GameEngine.Instance.World.ThirdAgeWorld = false;
        GameEngine.Instance.World.FourthAgeWorld = false;
        GameEngine.Instance.World.FifthAgeWorld = false;
        GameEngine.Instance.World.WorldEnded = false;
        GameEngine.Instance.World.SeventhAgeWorld = false;
        GameEngine.Instance.World.SixthAgeWorld = false;
      }
      else
      {
        GameEngine.Instance.World.SecondAgeWorld = returnData.m_secondAgeWorld;
        GameEngine.Instance.World.ThirdAgeWorld = returnData.m_thirdAgeWorld;
        GameEngine.Instance.World.FourthAgeWorld = returnData.m_fourthAgeWorld;
        GameEngine.Instance.World.FifthAgeWorld = returnData.m_fifthAgeWorld;
        GameEngine.Instance.World.WorldEnded = false;
        GameEngine.Instance.World.SeventhAgeWorld = false;
        GameEngine.Instance.World.SixthAgeWorld = false;
        RemoteServices.Instance.Show7thAgeMessage = false;
        RemoteServices.Instance.Show6thAgeMessage = false;
        if (GameEngine.Instance.World.FifthAgeWorld)
        {
          GameEngine.Instance.World.FourthAgeWorld = true;
          GameEngine.Instance.World.ThirdAgeWorld = true;
          GameEngine.Instance.World.SecondAgeWorld = true;
          if (!returnData.m_fourthAgeWorld)
            GameEngine.Instance.World.SixthAgeWorld = true;
          if (!returnData.m_thirdAgeWorld)
            GameEngine.Instance.World.SeventhAgeWorld = true;
          if (!returnData.m_secondAgeWorld)
            GameEngine.Instance.World.WorldEnded = true;
        }
      }
      if (GameEngine.Instance.World.SeventhAgeWorld && returnData.m_show3rdAgeMessage)
      {
        RemoteServices.Instance.Show3rdAgeMessage = false;
        RemoteServices.Instance.Show7thAgeMessage = true;
      }
      if (GameEngine.Instance.World.SixthAgeWorld && returnData.m_show2ndAgeMessage)
      {
        RemoteServices.Instance.Show2ndAgeMessage = false;
        RemoteServices.Instance.Show6thAgeMessage = true;
      }
      GameEngine.Instance.World.UserRelations = returnData.m_userRelationships;
      GameEngine.Instance.World.UserMarkers = returnData.m_userMarkers;
      if (!GameEngine.Instance.World.isIslandWorld())
      {
        WorldMap.TreasureCastle_AttackGap = returnData.m_treasureCastle_AttackGap;
      }
      else
      {
        GameEngine.Instance.World.SpecialSeaConditionsData = returnData.m_treasureCastle_AttackGap;
        WorldMap.TreasureCastle_AttackGap = 86400;
      }
      InterfaceMgr.Instance.chatSetBan(returnData.m_chatBanned);
      GameEngine.Instance.World.pendingPrizes = returnData.m_PendingPrizes == null ? new List<int>() : new List<int>((IEnumerable<int>) returnData.m_PendingPrizes);
      if (ProfileLoginWindow.lastLoadedEmail.Length == 0)
        ProfileLoginWindow.lastLoadedEmail = ProfileLoginWindow.storedUserLoginEmail;
      else if (ProfileLoginWindow.lastLoadedEmail.ToLowerInvariant() != ProfileLoginWindow.storedUserLoginEmail.ToLowerInvariant())
      {
        int level = 0;
        if (ProfileLoginWindow.lastLoadedEmail2.Length == 0)
          ProfileLoginWindow.lastLoadedEmail2 = ProfileLoginWindow.storedUserLoginEmail;
        else
          level = 1;
        GameEngine.Instance.World.maybeMultiAccount(level);
      }
      int num = returnData.m_showGloryResults ? 1 : 0;
      GameEngine.Instance.World.showGloryResults = returnData.m_showGloryResults;
      GameEngine.Instance.World.RetrievePreviousContestIDs();
      GameEngine.Instance.cardsManager.ResetPremiumOffers();
      GameEngine.Instance.cardsManager.RetrievePremiumOffers();
    }

    private void manageLoginButton()
    {
      int num = this.loginButtonActive ? 1 : 0;
    }

    private void tbLoginUser_TextChanged(object sender, EventArgs e) => this.manageLoginButton();

    private void tbLoginPassword_TextChanged(object sender, EventArgs e)
    {
      this.manageLoginButton();
    }

    private void geckoWebBrowser1_DocumentCompleted(object sender, EventArgs e)
    {
    }

    private void geckoWebBrowser1_StatusTextChanged(object sender, EventArgs e)
    {
      string statusText = this.geckoWebBrowser1.StatusText;
    }

    public void SetEmailOptInState(bool state) => this.emailOptInState = state;

    public void update()
    {
      if (this.OpenTOSPanel)
      {
        this.OpenTOSPanel = false;
        this.OpenTOSPanelIsOpen = true;
        ProfileLoginWindow.CloseTOSPanel = false;
        InterfaceMgr.Instance.openUpdatedTOSPopupWindow();
      }
      if (this.OpenTOSPanelIsOpen && ProfileLoginWindow.CloseTOSPanel)
      {
        this.OpenTOSPanelIsOpen = false;
        ProfileLoginWindow.CloseTOSPanel = false;
        UpdatedTOSPopupWindow.close();
      }
      if (this.initialisedLanguage != Program.mySettings.LanguageIdent)
      {
        if (this.joinImage != null)
          this.joinImage.Dispose();
        if (this.joinImageOver != null)
          this.joinImageOver.Dispose();
        if (this.playImage != null)
          this.playImage.Dispose();
        if (this.playImageOver != null)
          this.playImageOver.Dispose();
        if (this.loginImage != null)
          this.loginImage.Dispose();
        if (this.logoutImage != null)
          this.logoutImage.Dispose();
        if (this.loginImageOver != null)
          this.loginImageOver.Dispose();
        if (this.closedImage != null)
          this.closedImage.Dispose();
        if (this.closedImageOver != null)
          this.closedImageOver.Dispose();
        if (this.logoutImageOver != null)
          this.logoutImageOver.Dispose();
        if (this.newsImage != null)
          this.newsImage.Dispose();
        if (this.newsImageOver != null)
          this.newsImageOver.Dispose();
        if (this.accountImage != null)
          this.accountImage.Dispose();
        if (this.accountImageOver != null)
          this.accountImageOver.Dispose();
        if (this.exitImage != null)
          this.exitImage.Dispose();
        if (this.exitImageOver != null)
          this.exitImageOver.Dispose();
        if (this.cancelImage != null)
          this.cancelImage.Dispose();
        if (this.cancelImageOver != null)
          this.cancelImageOver.Dispose();
        if (this.forgottenImage != null)
          this.forgottenImage.Dispose();
        if (this.forgottenImageOver != null)
          this.forgottenImageOver.Dispose();
        if (this.selectImage != null)
          this.selectImage.Dispose();
        if (this.selectImageOver != null)
          this.selectImageOver.Dispose();
        if (this.createAccountImage != null)
          this.createAccountImage.Dispose();
        if (this.createAccountImageOver != null)
          this.createAccountImageOver.Dispose();
        if (this.createAccountImage2 != null)
          this.createAccountImage2.Dispose();
        if (this.createAccountImageOver2 != null)
          this.createAccountImageOver2.Dispose();
        if (this.optionsImage != null)
          this.optionsImage.Dispose();
        if (this.optionsImageOver != null)
          this.optionsImageOver.Dispose();
        this.joinImage = this.joinImageOver = this.playImage = this.playImageOver = this.loginImage = this.logoutImage = this.loginImageOver = this.closedImage = this.closedImageOver = this.logoutImageOver = this.newsImage = this.newsImageOver = this.accountImage = this.accountImageOver = this.exitImage = this.exitImageOver = this.cancelImage = this.cancelImageOver = this.forgottenImage = this.forgottenImageOver = this.selectImage = this.selectImageOver = this.createAccountImage = this.createAccountImageOver = this.createAccountImage2 = this.createAccountImageOver2 = this.optionsImage = this.optionsImageOver = (Image) null;
        if (WorldSelectPopupPanel.closeImage != null)
          WorldSelectPopupPanel.closeImage.Dispose();
        if (WorldSelectPopupPanel.closeImageOver != null)
          WorldSelectPopupPanel.closeImageOver.Dispose();
        if (WorldSelectPopupPanel.selectImageSelected != null)
          WorldSelectPopupPanel.selectImageSelected.Dispose();
        if (WorldSelectPopupPanel.selectImage != null)
          WorldSelectPopupPanel.selectImage.Dispose();
        if (WorldSelectPopupPanel.selectImageOver != null)
          WorldSelectPopupPanel.selectImageOver.Dispose();
        if (WorldSelectPopupPanel.selectSpecialImage != null)
          WorldSelectPopupPanel.selectSpecialImage.Dispose();
        if (WorldSelectPopupPanel.selectSpecialImageSelected != null)
          WorldSelectPopupPanel.selectSpecialImageSelected.Dispose();
        if (WorldSelectPopupPanel.selectSpecialImageOver != null)
          WorldSelectPopupPanel.selectSpecialImageOver.Dispose();
        if (WorldSelectPopupPanel.selectAIImage != null)
          WorldSelectPopupPanel.selectAIImage.Dispose();
        if (WorldSelectPopupPanel.selectAIImageSelected != null)
          WorldSelectPopupPanel.selectAIImageSelected.Dispose();
        if (WorldSelectPopupPanel.selectAIImageOver != null)
          WorldSelectPopupPanel.selectAIImageOver.Dispose();
        if (WorldSelectPopupPanel.joinImage != null)
          WorldSelectPopupPanel.joinImage.Dispose();
        if (WorldSelectPopupPanel.joinImageOver != null)
          WorldSelectPopupPanel.joinImageOver.Dispose();
        if (WorldSelectPopupPanel.playImage != null)
          WorldSelectPopupPanel.playImage.Dispose();
        if (WorldSelectPopupPanel.playImageOver != null)
          WorldSelectPopupPanel.playImageOver.Dispose();
        if (WorldSelectPopupPanel.closedImage != null)
          WorldSelectPopupPanel.closedImage.Dispose();
        // ISSUE: variable of the null type
       // __Null local1;
        WorldSelectPopupPanel.closedImage = (Image) (null);
        WorldSelectPopupPanel.playImageOver = (Image) null;
        WorldSelectPopupPanel.playImage = (Image) null;
        WorldSelectPopupPanel.joinImageOver = (Image) null;
        WorldSelectPopupPanel.joinImage = (Image) null;
        WorldSelectPopupPanel.selectAIImageOver = (Image) null;
        WorldSelectPopupPanel.selectAIImageSelected = (Image) null;
        WorldSelectPopupPanel.selectAIImage = (Image) null;
        WorldSelectPopupPanel.selectSpecialImageOver = (Image) null;
        WorldSelectPopupPanel.selectSpecialImageSelected = (Image) null;
        WorldSelectPopupPanel.selectSpecialImage = (Image) null;
        WorldSelectPopupPanel.selectImageOver = (Image) null;
        WorldSelectPopupPanel.selectImage = (Image) null;
        WorldSelectPopupPanel.selectImageSelected = (Image) null;
        WorldSelectPopupPanel.closeImageOver = (Image) null;
        WorldSelectPopupPanel.closeImage = (Image) null;
        if (BPPopupPanel.closeImage != null)
          BPPopupPanel.closeImage.Dispose();
        if (BPPopupPanel.closeImageOver != null)
          BPPopupPanel.closeImageOver.Dispose();
        if (BPPopupPanel.completeImage != null)
          BPPopupPanel.completeImage.Dispose();
        if (BPPopupPanel.completeImageOver != null)
          BPPopupPanel.completeImageOver.Dispose();
        // ISSUE: variable of the null type
       // __Null null;
        BPPopupPanel.completeImageOver = (Image) ( null);
        BPPopupPanel.completeImage = (Image) null;
        BPPopupPanel.closeImageOver = (Image) null;
        BPPopupPanel.closeImage = (Image) null;
        if (CreatePopupPanel.nextImage != null)
          CreatePopupPanel.nextImage.Dispose();
        if (CreatePopupPanel.nextImageOver != null)
          CreatePopupPanel.nextImageOver.Dispose();
        if (CreatePopupPanel.headerImage != null)
          CreatePopupPanel.headerImage.Dispose();
        if (CreatePopupPanel.headerTransferImage != null)
          CreatePopupPanel.headerTransferImage.Dispose();
        if (CreatePopupPanel.closeImage != null)
          CreatePopupPanel.closeImage.Dispose();
        if (CreatePopupPanel.closeImageOver != null)
          CreatePopupPanel.closeImageOver.Dispose();
        if (CreatePopupPanel.transferImage != null)
          CreatePopupPanel.transferImage.Dispose();
        if (CreatePopupPanel.transferImageOver != null)
          CreatePopupPanel.transferImageOver.Dispose();
        // ISSUE: variable of the null type
       // __Null null;
        CreatePopupPanel.transferImageOver = (Image) ( null);
        CreatePopupPanel.transferImage = (Image) null;
        CreatePopupPanel.closeImageOver = (Image) null;
        CreatePopupPanel.closeImage = (Image) null;
        CreatePopupPanel.headerTransferImage = (Image) null;
        CreatePopupPanel.headerImage = (Image) null;
        CreatePopupPanel.nextImageOver = (Image) null;
        CreatePopupPanel.nextImage = (Image) null;
        if (VacationCancelPopupPanel.cancelImageOver != null)
          VacationCancelPopupPanel.cancelImageOver.Dispose();
        if (VacationCancelPopupPanel.closeImage != null)
          VacationCancelPopupPanel.closeImage.Dispose();
        if (VacationCancelPopupPanel.closeImageOver != null)
          VacationCancelPopupPanel.closeImageOver.Dispose();
        if (VacationCancelPopupPanel.headerImage != null)
          VacationCancelPopupPanel.headerImage.Dispose();
        if (VacationCancelPopupPanel.cancelImage != null)
          VacationCancelPopupPanel.cancelImage.Dispose();
        // ISSUE: variable of the null type
       // __Null local4;
        VacationCancelPopupPanel.cancelImageOver = (Image) (null);
        VacationCancelPopupPanel.cancelImage = (Image) null;
        VacationCancelPopupPanel.headerImage = (Image)null;
        VacationCancelPopupPanel.closeImageOver = (Image)null;
        VacationCancelPopupPanel.closeImage = (Image) null;
        if (this.chkAutoLogin != null)
          this.chkAutoLogin.Checked = false;
        this.init();
        if (this.chkAutoLogin != null)
          this.chkAutoLogin.Checked = false;
        this.Invalidate();
      }
      else if (this.delayedCreateUserOpen)
      {
        this.delayedCreateUserOpen = false;
        this.ShowCreateUserForm();
      }
      else
      {
        if (this.emailOptInPopup)
        {
          this.emailOptInPopup = false;
          int num1 = (int) new EmailOptInPopup()
          {
            m_Parent = this
          }.ShowDialog((IWin32Window) Program.profileLogin);
          int num2 = 0;
          if (this.emailOptInState)
            num2 = 1;
          XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).SetEmailOptIn((IAuthRequest) new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), "", "", "", new int?(0), new int?(0), new int?(num2)), new AuthEndResponseDelegate(this.EmailOptInCallback), (Control) this);
        }
        InterfaceMgr.Instance.runTooltips();
        if (this.lastLogoutClicked != DateTime.MinValue && (DateTime.Now - this.lastLogoutClicked).TotalSeconds > 60.0)
          this.lastLogoutClicked = DateTime.MinValue;
        int downloadingCounter = GameEngine.Instance.World.downloadingCounter;
        if (downloadingCounter != this.lastCount)
        {
          string str = SK.Text("ConnectingPopup_Retrieving_Data", "Retrieving Data From Server.");
          for (int index = 0; index < downloadingCounter; ++index)
            str += ".";
          this.lblRetrieving.Text = str;
          if (this.lblRetrieving.Visible)
            this.Text = this.defaultWindowTitle + " - " + this.lblRetrieving.Text;
          this.lastCount = downloadingCounter;
          if (this.lblRetrieving.Visible)
            this.feedbackProgress.Size = new Size(Math.Min(this.pnlFeedback.Width * (downloadingCounter + 2) / 12, this.pnlFeedback.Width), this.pnlFeedback.Height);
          else
            this.feedbackProgress.Size = new Size(0, this.pnlFeedback.Height);
          this.feedbackProgressArea.invalidate();
        }
        if (!InterfaceMgr.Instance.isCreatePopup() && !InterfaceMgr.Instance.isVacationCancelPopupWindow() && !InterfaceMgr.Instance.isBPPopup() && !InterfaceMgr.Instance.isWorldSelectPopup())
          return;
        InterfaceMgr.Instance.updatePopups();
      }
    }

    private void EmailOptInCallback(IAuthProvider sender, IAuthResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) == 0)
        ;
    }

    private void cancelClick()
    {
      this.Cursor = Cursors.Default;
      this.connectingCancelled = true;
      GameEngine.Instance.forceRelogin();
      GameEngine.Instance.sessionExpired(-1);
      this.feedbackProgressArea.invalidate();
      while (GameEngine.Instance.World.isWorkerThreadAlive())
      {
        Thread.Sleep(200);
        Program.DoEvents();
      }
    }

    public void openAfterCancel()
    {
      this.lblRetrieving.Visible = false;
      this.tandcLabel.Visible = true;
      this.gameRulesLabel.Visible = true;
      this.forumLabel.Visible = true;
      this.supportLabel.Visible = true;
      this.feedbackProgress.Size = new Size(0, this.pnlFeedback.Height);
      this.Text = this.defaultWindowTitle;
      this.cancelButton.Visible = false;
      this.feedbackProgressArea.invalidate();
      this.UserEntryMode = true;
      this.EnablePanels(true);
    }

    private void ProfileLoginWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing || this.selfClosing)
        return;
      GameEngine.Instance.appClosing();
      RemoteServices.Instance.UserID = -1;
    }

    private void ProfileLoginWindow_LocationChanged(object sender, EventArgs e)
    {
      InterfaceMgr.Instance.moveGreyOutWindow();
      InterfaceMgr.Instance.moveCreatePopupWindow();
    }

    private void shkruClick()
    {
      new Process()
      {
        StartInfo = {
          FileName = "www.vk.com/shkru"
        }
      }.Start();
    }

    private void facebookClick()
    {
      new Process()
      {
        StartInfo = {
          FileName = URLs.FacebookURL
        }
      }.Start();
    }

    private void youtubeClick()
    {
      new Process()
      {
        StartInfo = {
          FileName = URLs.YoutubeURL
        }
      }.Start();
    }

    private void twitterClick()
    {
      new Process()
      {
        StartInfo = {
          FileName = URLs.TwitterURL
        }
      }.Start();
    }

    private void ProfileLoginWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (this.LoginPanelControls_Feedback != null)
        this.LoginPanelControls_Feedback.PanelActive = false;
      if (this.LoginPanelControls_LoggedIn != null)
        this.LoginPanelControls_LoggedIn.PanelActive = false;
      if (this.LoginPanelControls_LoggedOut != null)
        this.LoginPanelControls_LoggedOut.PanelActive = false;
      if (this.LoginPanelControls_Feedback != null)
        this.LoginPanelControls_Feedback.PanelActive = false;
      if (this.BrowserTabsControls != null)
        this.BrowserTabsControls.PanelActive = false;
      if (this.WorldsPanelcontrols_LoggedIn != null)
        this.WorldsPanelcontrols_LoggedIn.PanelActive = false;
      if (this.WorldsPanelcontrols_LoggedOut != null)
        this.WorldsPanelcontrols_LoggedOut.PanelActive = false;
      if (this.joinImage != null)
        this.joinImage.Dispose();
      if (this.joinImageOver != null)
        this.joinImageOver.Dispose();
      if (this.playImage != null)
        this.playImage.Dispose();
      if (this.playImageOver != null)
        this.playImageOver.Dispose();
      if (this.loginImage != null)
        this.loginImage.Dispose();
      if (this.logoutImage != null)
        this.logoutImage.Dispose();
      if (this.loginImageOver != null)
        this.loginImageOver.Dispose();
      if (this.closedImage != null)
        this.closedImage.Dispose();
      if (this.closedImageOver != null)
        this.closedImageOver.Dispose();
      if (this.logoutImageOver != null)
        this.logoutImageOver.Dispose();
      if (this.newsImage != null)
        this.newsImage.Dispose();
      if (this.newsImageOver != null)
        this.newsImageOver.Dispose();
      if (this.accountImage != null)
        this.accountImage.Dispose();
      if (this.accountImageOver != null)
        this.accountImageOver.Dispose();
      if (this.exitImage != null)
        this.exitImage.Dispose();
      if (this.exitImageOver != null)
        this.exitImageOver.Dispose();
      if (this.cancelImage != null)
        this.cancelImage.Dispose();
      if (this.cancelImageOver != null)
        this.cancelImageOver.Dispose();
      if (this.forgottenImage != null)
        this.forgottenImage.Dispose();
      if (this.forgottenImageOver != null)
        this.forgottenImageOver.Dispose();
      if (this.selectImage != null)
        this.selectImage.Dispose();
      if (this.selectImageOver != null)
        this.selectImageOver.Dispose();
      if (this.createAccountImage != null)
        this.createAccountImage.Dispose();
      if (this.createAccountImageOver != null)
        this.createAccountImageOver.Dispose();
      if (this.createAccountImage2 != null)
        this.createAccountImage2.Dispose();
      if (this.createAccountImageOver2 != null)
        this.createAccountImageOver2.Dispose();
      if (this.optionsImage != null)
        this.optionsImage.Dispose();
      if (this.optionsImageOver != null)
        this.optionsImageOver.Dispose();
      if (this.GreyoutLogin != null && this.GreyoutLogin.Image != null)
      {
        this.GreyoutLogin.Image.Dispose();
        this.GreyoutLogin.Image = (Image) null;
      }
      if (this.GreyoutTabs != null && this.GreyoutTabs.Image != null)
      {
        this.GreyoutTabs.Image.Dispose();
        this.GreyoutTabs.Image = (Image) null;
      }
      if (this.GreyoutWorlds == null || this.GreyoutWorlds.Image == null)
        return;
      this.GreyoutWorlds.Image.Dispose();
      this.GreyoutWorlds.Image = (Image) null;
    }

    public static string getWorldShortDesc(WorldInfo world)
    {
      return world.Supportculture == "ru" && world.MapCulture == "ru" ? world.ShortDesc.Replace("*", "").Replace("World", "Мир") : world.ShortDesc.Replace("*", "");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProfileLoginWindow));
      this.panel1 = new Panel();
      this.browserServerNews = new KingdomsBrowserGecko();
      this.pnlTabs = new NoDrawPanel();
      this.pnlFeedback = new NoDrawPanel();
      this.panel2 = new Panel();
      this.btnExit = new Button();
      this.geckoWebBrowser1 = new KingdomsBrowserGecko();
      this.label1 = new Label();
      this.pnlWorlds = new NoDrawPanel();
      this.pnlLogin = new NoDrawPanel();
      this.panel1.SuspendLayout();
      this.pnlFeedback.SuspendLayout();
      this.SuspendLayout();
      this.panel1.BackColor = ARGBColors.White;
      this.panel1.Controls.Add((Control) this.browserServerNews);
      this.panel1.Controls.Add((Control) this.pnlTabs);
      this.panel1.Controls.Add((Control) this.pnlFeedback);
      this.panel1.Controls.Add((Control) this.geckoWebBrowser1);
      this.panel1.Controls.Add((Control) this.label1);
      this.panel1.Dock = DockStyle.Left;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(930, 632);
      this.panel1.TabIndex = 280;
      this.browserServerNews.Location = new Point(658, 439);
      this.browserServerNews.MinimumSize = new Size(20, 20);
      this.browserServerNews.Name = "browserServerNews";
      this.browserServerNews.Size = new Size(270, 161);
      this.browserServerNews.TabIndex = 283;
      this.browserServerNews.TabStop = false;
      this.pnlTabs.Location = new Point(0, 0);
      this.pnlTabs.MaximumSize = new Size(658, 39);
      this.pnlTabs.MinimumSize = new Size(658, 39);
      this.pnlTabs.Name = "pnlTabs";
      this.pnlTabs.Size = new Size(658, 39);
      this.pnlTabs.TabIndex = 281;
      this.pnlFeedback.Controls.Add((Control) this.panel2);
      this.pnlFeedback.Controls.Add((Control) this.btnExit);
      this.pnlFeedback.Location = new Point(0, 600);
      this.pnlFeedback.MaximumSize = new Size(930, 32);
      this.pnlFeedback.MinimumSize = new Size(930, 32);
      this.pnlFeedback.Name = "pnlFeedback";
      this.pnlFeedback.Size = new Size(930, 32);
      this.pnlFeedback.TabIndex = 282;
      this.panel2.BackColor = ARGBColors.Black;
      this.panel2.Location = new Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(930, 1);
      this.panel2.TabIndex = 274;
      this.btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnExit.Location = new Point(841, 5);
      this.btnExit.Name = "btnExit";
      this.btnExit.Size = new Size(76, 23);
      this.btnExit.TabIndex = 269;
      this.btnExit.Text = "Exit";
      this.btnExit.UseVisualStyleBackColor = true;
      this.btnExit.Visible = false;
      this.btnExit.Click += new EventHandler(this.btnExit_Click);
      this.geckoWebBrowser1.Location = new Point(0, 39);
      this.geckoWebBrowser1.MinimumSize = new Size(20, 20);
      this.geckoWebBrowser1.Name = "geckoWebBrowser1";
      this.geckoWebBrowser1.Size = new Size(658, 561);
      this.geckoWebBrowser1.TabIndex = 279;
      this.geckoWebBrowser1.TabStop = false;
      this.geckoWebBrowser1.ClientFeedback += new ClientFedbackEventHandler(this.geckoWebBrowser1_ClientFeedback);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label1.ForeColor = ARGBColors.DarkRed;
      this.label1.Location = new Point(7, 50);
      this.label1.Name = "label1";
      this.label1.Size = new Size(219, 17);
      this.label1.TabIndex = 0;
      this.label1.Text = "Connecting to News Server...";
      this.pnlWorlds.BackColor = ARGBColors.White;
      this.pnlWorlds.Location = new Point(658, 232);
      this.pnlWorlds.MaximumSize = new Size(270, 207);
      this.pnlWorlds.MinimumSize = new Size(270, 207);
      this.pnlWorlds.Name = "pnlWorlds";
      this.pnlWorlds.Size = new Size(270, 207);
      this.pnlWorlds.TabIndex = 282;
      this.pnlLogin.BackColor = ARGBColors.White;
      this.pnlLogin.Location = new Point(658, 0);
      this.pnlLogin.MaximumSize = new Size(270, 232);
      this.pnlLogin.MinimumSize = new Size(270, 232);
      this.pnlLogin.Name = "pnlLogin";
      this.pnlLogin.Size = new Size(270, 232);
      this.pnlLogin.TabIndex = 283;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.White;
      this.ClientSize = new Size(928, 632);
      this.Controls.Add((Control) this.pnlWorlds);
      this.Controls.Add((Control) this.pnlLogin);
      this.Controls.Add((Control) this.panel1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ProfileLoginWindow);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (ProfileLoginWindow);
      this.Load += new EventHandler(this.ProfileLoginWindow_Load);
      this.FormClosed += new FormClosedEventHandler(this.ProfileLoginWindow_FormClosed);
      this.FormClosing += new FormClosingEventHandler(this.ProfileLoginWindow_FormClosing);
      this.LocationChanged += new EventHandler(this.ProfileLoginWindow_LocationChanged);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.pnlFeedback.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private class RequestState
    {
      public WebRequest req;
    }
  }
}
