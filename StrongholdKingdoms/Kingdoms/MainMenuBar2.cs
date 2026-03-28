// Decompiled with JetBrains decompiler
// Type: Kingdoms.MainMenuBar2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MainMenuBar2 : CustomSelfDrawPanel.CSDControl
  {
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton btnHelpMenu = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnFileMenu = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnAdminMenu = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnCombat = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnLogOut = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnMyAccount = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDFill pnlLoadLight = new CustomSelfDrawPanel.CSDFill();
    private MenuMailPanel MenuButtonsPanel = new MenuMailPanel();
    private SaveFileDialog SaveCSVFileDialog;
    private SaveFileDialog SavePNGFileDialog;
    private static bool castleCopyMode;
    private bool fixCommandSent;
    private bool nextPlaybackCountries;
    private LogoutOptionsWindow2 logoutPopup;

    public static bool CastleCopyMode
    {
      get => MainMenuBar2.castleCopyMode && RemoteServices.Instance.Admin;
    }

    public MainMenuBar2()
    {
      this.SaveCSVFileDialog = new SaveFileDialog();
      this.SaveCSVFileDialog.DefaultExt = "csv";
      this.SaveCSVFileDialog.Filter = "CSV (*.csv)|*.csv";
      this.SaveCSVFileDialog.Title = "Save Stats .csv File";
      this.SavePNGFileDialog = new SaveFileDialog();
      this.SavePNGFileDialog.DefaultExt = "png";
      this.SavePNGFileDialog.Filter = "PNG (*.png)|*.png";
      this.SavePNGFileDialog.Title = "Save Avatar .png File";
      if (RemoteServices.Instance.Admin)
        this.btnAdminMenu.Visible = true;
      else
        this.btnAdminMenu.Visible = false;
      this.btnCombat.Visible = false;
    }

    public void init2()
    {
      this.MenuButtonsPanel.init();
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.menu_Background;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(463, 29);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.btnAdminMenu.FillRectOverColor = Color.FromArgb(32, 0, 0, 0);
      this.btnAdminMenu.Position = new Point(79, 1);
      this.btnAdminMenu.Size = new Size(45, 23);
      this.btnAdminMenu.Text.Text = "Admin";
      this.btnAdminMenu.Text.Size = this.btnAdminMenu.Size;
      this.btnAdminMenu.Text.Color = ARGBColors.Black;
      this.btnAdminMenu.TextYOffset = -1;
      this.btnAdminMenu.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.btnAdminMenu.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnAdminMenu.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.adminOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.genericLeave));
      this.btnAdminMenu.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAdminMenu_Click));
      this.btnAdminMenu.Visible = RemoteServices.Instance.Admin;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnAdminMenu);
      this.btnCombat.FillRectOverColor = Color.FromArgb(32, 0, 0, 0);
      this.btnCombat.Position = new Point(19, 1);
      this.btnCombat.Size = new Size(54, 23);
      this.btnCombat.Text.Text = "Combat";
      this.btnCombat.Text.Size = this.btnCombat.Size;
      this.btnCombat.Text.Color = ARGBColors.Black;
      this.btnCombat.TextYOffset = -1;
      this.btnCombat.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.btnCombat.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnCombat.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.combatOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.genericLeave));
      this.btnCombat.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnCombat_Click));
      this.btnCombat.Visible = RemoteServices.Instance.Admin;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnCombat);
      this.pnlLoadLight.Position = new Point(459, 0);
      this.pnlLoadLight.Size = new Size(4, 4);
      this.pnlLoadLight.Visible = false;
      this.pnlLoadLight.FillColor = ARGBColors.Green;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pnlLoadLight);
      this.MenuButtonsPanel.Position = new Point(10, 0);
      this.MenuButtonsPanel.Size = new Size(150, 24);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.MenuButtonsPanel);
      this.MenuButtonsPanel.init();
      this.btnHelpMenu.FillRectOverColor = Color.FromArgb(32, 0, 0, 0);
      this.btnHelpMenu.Position = new Point(323, 1);
      this.btnHelpMenu.Size = new Size(62, 23);
      this.btnHelpMenu.Text.Text = SK.Text("MENU_Help", "Help");
      this.btnHelpMenu.Text.Size = this.btnHelpMenu.Size;
      this.btnHelpMenu.Text.Color = ARGBColors.Black;
      this.btnHelpMenu.TextYOffset = -1;
      this.btnHelpMenu.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.btnHelpMenu.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnHelpMenu.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.helpOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.genericLeave));
      this.btnHelpMenu.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnHelp_Click));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnHelpMenu);
      this.btnLogOut.FillRectOverColor = Color.FromArgb(32, 0, 0, 0);
      this.btnLogOut.Position = new Point(391, 1);
      this.btnLogOut.Size = new Size(62, 23);
      this.btnLogOut.Text.Text = SK.Text("GENERIC_Log_Out", "Log Out");
      this.btnLogOut.Text.Size = this.btnLogOut.Size;
      this.btnLogOut.Text.Color = ARGBColors.Black;
      this.btnLogOut.TextYOffset = -1;
      this.btnLogOut.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.btnLogOut.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnLogOut.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnLogOut_Click));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnLogOut);
      this.btnFileMenu.FillRectOverColor = Color.FromArgb(32, 0, 0, 0);
      this.btnFileMenu.Position = new Point(246, 1);
      this.btnFileMenu.Size = new Size(78, 23);
      this.btnFileMenu.Text.Text = SK.Text("MENU_Settings", "Settings");
      this.btnFileMenu.Text.Size = this.btnFileMenu.Size;
      this.btnFileMenu.Text.Color = ARGBColors.Black;
      this.btnFileMenu.TextYOffset = -1;
      this.btnFileMenu.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.btnFileMenu.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnFileMenu.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.fileOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.genericLeave));
      this.btnFileMenu.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.button2_Click));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnFileMenu);
      this.btnMyAccount.FillRectOverColor = Color.FromArgb(32, 0, 0, 0);
      this.btnMyAccount.Position = new Point(166, 1);
      this.btnMyAccount.Size = new Size(78, 23);
      this.btnMyAccount.Text.Text = SK.Text("MENU_My_Account", "My Account");
      this.btnMyAccount.Text.Size = this.btnMyAccount.Size;
      this.btnMyAccount.Text.Color = ARGBColors.Black;
      this.btnMyAccount.TextYOffset = -1;
      this.btnMyAccount.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.btnMyAccount.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.btnMyAccount.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.accountOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.genericLeave));
      this.btnMyAccount.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnMyAccount_Click));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.btnMyAccount);
      this.resize();
    }

    private void helpOver()
    {
      if (!MenuPopup.isAMenuVisible())
        return;
      this.btnHelp_Click(false);
    }

    private void fileOver()
    {
      if (!MenuPopup.isAMenuVisible())
        return;
      this.button2_Click(false);
    }

    private void accountOver()
    {
      if (!MenuPopup.isAMenuVisible())
        return;
      this.btnMyAccount_Click(false);
    }

    private void adminOver()
    {
      if (!MenuPopup.isAMenuVisible())
        return;
      this.btnAdminMenu_Click();
    }

    private void combatOver()
    {
      if (!MenuPopup.isAMenuVisible())
        return;
      this.btnCombat_Click();
    }

    private void genericLeave()
    {
    }

    public void setAdmin()
    {
      if (RemoteServices.Instance.Admin)
      {
        this.btnAdminMenu.Visible = true;
      }
      else
      {
        this.btnAdminMenu.Visible = false;
        this.btnCombat.Visible = false;
      }
    }

    private void button2_Click() => this.button2_Click(true);

    private void button2_Click(bool sfx)
    {
      if (sfx)
        GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_settings");
      MenuPopup menuPopup = new MenuPopup();
      Point screen = this.csd.PointToScreen(this.btnFileMenu.Position);
      menuPopup.setPosition(screen.X, screen.Y + 24);
      menuPopup.setCallBack(new MenuPopup.MenuCallback(this.menu1Callback));
      menuPopup.addMenuItem(SK.Text("MENU_Settings", "Settings"), 1);
      if (!GameEngine.Instance.World.WorldEnded)
      {
        menuPopup.addMenuItem(SK.Text("MENU_Edit_Avatar", "Edit Avatar"), 5);
        menuPopup.addMenuItem(SK.Text("User_Manage_Relations", "Manage Diplomacy"), 300);
      }
      int ownSelectedVillage = InterfaceMgr.Instance.OwnSelectedVillage;
      if (ownSelectedVillage >= 0 && !GameEngine.Instance.World.isCapital(ownSelectedVillage) && GameEngine.Instance.World.isUserVillage(ownSelectedVillage) && !GameEngine.Instance.World.WorldEnded)
      {
        menuPopup.addBar();
        menuPopup.addMenuItem(SK.Text("MENU_Rename_Current_Village", "Rename Current Village"), 9);
        CustomSelfDrawPanel.CSDControl csdControl1 = (CustomSelfDrawPanel.CSDControl) menuPopup.addMenuItem(SK.Text("MENU_Convert_Current_Village", "Convert Current Village"), 12);
        CustomSelfDrawPanel.CSDControl csdControl2 = (CustomSelfDrawPanel.CSDControl) menuPopup.addMenuItem(SK.Text("MENU_Abandon_Current_Village", "Abandon Current Village"), 11);
        csdControl1.CustomTooltipID = 1200;
        csdControl2.CustomTooltipID = 1201;
      }
      menuPopup.showMenu();
    }

    private void btnHelp_Click() => this.btnHelp_Click(true);

    private void btnHelp_Click(bool sfx)
    {
      if (sfx)
        GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_help");
      MenuPopup menuPopup = new MenuPopup();
      Point screen = this.csd.PointToScreen(this.btnHelpMenu.Position);
      menuPopup.setPosition(screen.X, screen.Y + 24);
      menuPopup.setCallBack(new MenuPopup.MenuCallback(this.menu1Callback));
      menuPopup.addMenuItem(SK.Text("MENU_SHK_Help", "Stronghold Kingdoms Help"), 108);
      menuPopup.addMenuItem(SK.Text("MENU_Game_Rules", "Game Rules"), 109);
      menuPopup.addMenuItem(SK.Text("TOUCH_Z_TermsOfUse", "Terms of Use"), 151);
      menuPopup.addMenuItem(SK.Text("MENU_Privacy", "Privacy Policy"), 152);
      menuPopup.addBar();
      menuPopup.addMenuItem(SK.Text("MENU_Show_Admin_Message", "Show Admin Message"), 103);
      menuPopup.addBar();
      if (GameEngine.Instance.World.isTutorialResumable())
        menuPopup.addMenuItem(SK.Text("Options_Resume_Tutorial", "Resume Tutorial"), 1109);
      menuPopup.addMenuItem(SK.Text("Options_Player_Guide", "Player Guide"), 1201);
      menuPopup.addBar();
      menuPopup.addMenuItem(SK.Text("MENU_About_Stronghold Kingdoms", "About Stronghold Kingdoms"), 102);
      menuPopup.showMenu();
    }

    private void btnMyAccount_Click() => this.btnMyAccount_Click(true);

    private void btnMyAccount_Click(bool sfx)
    {
      if (sfx)
        GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_myaccount");
      MenuPopup menuPopup = new MenuPopup();
      Point screen = this.csd.PointToScreen(this.btnMyAccount.Position);
      menuPopup.setPosition(screen.X, screen.Y + 24);
      menuPopup.setCallBack(new MenuPopup.MenuCallback(this.menu1Callback));
      menuPopup.addMenuItem(SK.Text("MENU_Account_Information", "Account Information"), 21001);
      if (!GameEngine.Instance.World.isBigpointAccount && !Program.bigpointInstall && !Program.aeriaInstall && !Program.bigpointPartnerInstall)
        menuPopup.addMenuItem(SK.Text("MENU_Invite_A_Friend", "Invite a Friend"), 21002);
      menuPopup.addMenuItem(SK.Text("MENU_Redeem_Offer_Code", "Redeem Offer Code"), 21003);
      if (!GameEngine.Instance.World.WorldEnded)
      {
        menuPopup.addBar();
        if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
          menuPopup.addMenuItem(SK.Text("MENU_VacationMode", "Vacation Mode Options"), 21009);
      }
      menuPopup.showMenu();
    }

    private void menu1Callback(int id)
    {
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_item_selected");
      this.fixCommandSent = false;
      switch (id)
      {
        case 1:
          GameEngine.Instance.playInterfaceSound("Options_open");
          OptionsPopup.openSettings();
          break;
        case 2:
          InterfaceMgr.Instance.openLogoutWindow(true);
          break;
        case 5:
          InterfaceMgr.Instance.getMainTabBar().selectDummyTab(10);
          break;
        case 9:
          if (RemoteServices.Instance.Admin && GameEngine.Instance.World.DrawDebugVillageNames)
          {
            int selectedVillage = InterfaceMgr.Instance.SelectedVillage;
            RenameVillagePopup renameVillagePopup = new RenameVillagePopup();
            renameVillagePopup.setVillageID(selectedVillage, GameEngine.Instance.World.getVillageNameOnly(selectedVillage));
            renameVillagePopup.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
            break;
          }
          int ownSelectedVillage1 = InterfaceMgr.Instance.OwnSelectedVillage;
          if (ownSelectedVillage1 >= 0 && !GameEngine.Instance.World.isCapital(ownSelectedVillage1) && GameEngine.Instance.World.isUserVillage(ownSelectedVillage1))
          {
            RenameVillagePopup renameVillagePopup = new RenameVillagePopup();
            renameVillagePopup.setVillageID(ownSelectedVillage1, GameEngine.Instance.World.getVillageNameOnly(ownSelectedVillage1));
            renameVillagePopup.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
            break;
          }
          int num1 = (int) MyMessageBox.Show(SK.Text("MENU_Cannot_Rename", "You cannot rename this village."), SK.Text("MENU_Rename_Error", "Rename Error"));
          break;
        case 11:
          int ownSelectedVillage2 = InterfaceMgr.Instance.OwnSelectedVillage;
          if (ownSelectedVillage2 >= 0 && !GameEngine.Instance.World.isCapital(ownSelectedVillage2) && GameEngine.Instance.World.isUserVillage(ownSelectedVillage2))
          {
            GameEngine.Instance.villageToAbandon = ownSelectedVillage2;
            break;
          }
          int num2 = (int) MyMessageBox.Show(SK.Text("MENU_Cannot_Abandon", "You cannot abandon this village."), SK.Text("GENERIC_Error", "Error"));
          break;
        case 12:
          int ownSelectedVillage3 = InterfaceMgr.Instance.OwnSelectedVillage;
          if (ownSelectedVillage3 < 0 || GameEngine.Instance.World.isCapital(ownSelectedVillage3) || !GameEngine.Instance.World.isUserVillage(ownSelectedVillage3))
            break;
          InterfaceMgr.Instance.changeTab(1);
          VillageMap village = GameEngine.Instance.getVillage(ownSelectedVillage3);
          if (village != null && village.m_nextMapTypeChange > VillageMap.getCurrentServerTime())
          {
            TimeSpan timeSpan = village.m_nextMapTypeChange - VillageMap.getCurrentServerTime();
            string str;
            if (timeSpan.Days > 0)
              str = string.Format("{0:D2} " + SK.Text("MENU_days", "days") + ", {1:D2} " + SK.Text("MENU_hours_short", "hrs") + ", {2:D2} " + SK.Text("MENU_minutes_short", "mins"), (object) timeSpan.Days, (object) timeSpan.Hours, (object) timeSpan.Minutes);
            else
              str = string.Format("{0:D1} " + SK.Text("MENU_hours_short", "hrs") + ", {1:D2} " + SK.Text("MENU_minutes_short", "mins"), (object) timeSpan.Hours, (object) timeSpan.Minutes);
            int num3 = (int) MyMessageBox.Show(SK.Text("MENU_Cannot_Change_Type", "You cannot change this Village's Type for") + " : " + str, SK.Text("MENU_Change_Type_Error", "Change Village Type Error"));
            break;
          }
          InterfaceMgr.Instance.openBuyVillageWindow(ownSelectedVillage3, false);
          break;
        case 101:
          new Process()
          {
            StartInfo = {
              FileName = "readme.txt"
            }
          }.Start();
          break;
        case 102:
          AboutPopup aboutPopup = new AboutPopup();
          aboutPopup.init();
          aboutPopup.Show();
          break;
        case 103:
          AdminInfoPopup.showMessage();
          break;
        case 105:
          try
          {
            new Process()
            {
              StartInfo = {
                FileName = URLs.FireflyHomepage
              }
            }.Start();
            break;
          }
          catch (Exception ex)
          {
            break;
          }
        case 107:
          try
          {
            new Process()
            {
              StartInfo = {
                FileName = URLs.ForumHomepage
              }
            }.Start();
            break;
          }
          catch (Exception ex)
          {
            break;
          }
        case 108:
          try
          {
            new Process()
            {
              StartInfo = {
                FileName = URLs.WikiPage
              }
            }.Start();
            break;
          }
          catch (Exception ex)
          {
            break;
          }
        case 109:
          try
          {
            new Process()
            {
              StartInfo = {
                FileName = URLs.IPSharingPage
              }
            }.Start();
            break;
          }
          catch (Exception ex)
          {
            break;
          }
        case 121:
          try
          {
            new Process()
            {
              StartInfo = {
                FileName = URLs.WikiPage
              }
            }.Start();
            break;
          }
          catch (Exception ex)
          {
            break;
          }
        case 151:
          try
          {
            new Process()
            {
              StartInfo = {
                FileName = URLs.TermsAndConditions
              }
            }.Start();
            break;
          }
          catch (Exception ex)
          {
            break;
          }
        case 152:
          try
          {
            new Process()
            {
              StartInfo = {
                FileName = URLs.PrivacyPolicy
              }
            }.Start();
            break;
          }
          catch (Exception ex)
          {
            break;
          }
        case 201:
          AdminInfoPopup.showAdminEdit();
          break;
        case 202:
          this.nextPlaybackCountries = true;
          if (GameEngine.Instance.World.gotPlaybackData())
          {
            GameEngine.Instance.World.playbackCountries();
            InterfaceMgr.Instance.togglePlaybackBarDXActive(true);
            break;
          }
          this.retrieveGameStats();
          break;
        case 203:
          this.retrieveGameInfo();
          break;
        case 209:
          if (MyMessageBox.Show("This call is not entirely 'game friendly'. Only use sparingly and at quiet game times and make sure no one else is using them same function!", "Admin Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0) != DialogResult.Yes)
            break;
          this.fixCommandSent = true;
          RemoteServices.Instance.set_CompleteVillageCastle_UserCallBack(new RemoteServices.CompleteVillageCastle_UserCallBack(this.CompleteVillageCastleCallBack));
          RemoteServices.Instance.CompleteVillageCastle(InterfaceMgr.Instance.getSelectedMenuVillage(), 15);
          break;
        case 221:
          this.createIngameMessage();
          break;
        case 223:
          this.clearIngameMessage();
          break;
        case 231:
          GameEngine.Instance.World.DrawDebugNames = !GameEngine.Instance.World.DrawDebugNames;
          GameEngine.Instance.World.DrawDebugVillageNames = false;
          break;
        case 232:
          GameEngine.Instance.World.DrawDebugVillageNames = !GameEngine.Instance.World.DrawDebugVillageNames;
          GameEngine.Instance.World.DrawDebugNames = false;
          break;
        case 300:
          InterfaceMgr.Instance.getMainTabBar().selectDummyTab(60);
          break;
        case 1001:
          InterfaceMgr.Instance.ParentForm.Close();
          break;
        case 1101:
          InterfaceMgr.Instance.getMainTabBar().selectDummyTab(2);
          GameEngine.Instance.InitCastleAttackSetup();
          break;
        case 1102:
          GameEngine.Instance.SkipVillageTab();
          InterfaceMgr.Instance.getMainTabBar().changeTab(1);
          InterfaceMgr.Instance.getVillageTabBar().changeTab(1);
          CastleMap.CreateMode = true;
          break;
        case 1109:
          GameEngine.Instance.playInterfaceSound("Options_resume_tutorial");
          GameEngine.Instance.World.resumeTutorial();
          break;
        case 1110:
        case 1111:
        case 1112:
        case 1113:
        case 1114:
        case 1115:
        case 1116:
        case 1117:
        case 1118:
        case 1119:
          CastleMap.FakeKeep = id - 1110 + 1;
          break;
        case 1120:
        case 1121:
        case 1122:
        case 1123:
          CastleMap.FakeDefensiveMode = id - 1120;
          break;
        case 1201:
          PostTutorialWindow.CreatePostTutorialWindow(false);
          break;
        case 2219:
          if (GameEngine.Instance.World.MapEditing)
          {
            int villageID = GameEngine.Instance.World.lastClickedVillage();
            if (GameEngine.Instance.World.isCountyCapital(villageID) && !GameEngine.Instance.World.isVillageVisible(villageID))
              RemoteServices.Instance.CompleteVillageCastle(villageID, 21);
            GameEngine.Instance.World.MapEditing = false;
            break;
          }
          GameEngine.Instance.World.MapEditing = true;
          break;
        case 9202:
          this.nextPlaybackCountries = false;
          if (GameEngine.Instance.World.gotPlaybackData())
          {
            GameEngine.Instance.World.playbackProvinces();
            InterfaceMgr.Instance.togglePlaybackBarDXActive(true);
            break;
          }
          this.retrieveGameStats();
          break;
        case 9203:
          GameEngine.Instance.World.stopPlayback();
          InterfaceMgr.Instance.togglePlaybackBarDXActive(false);
          break;
        case 10799:
          try
          {
            string str = "http://login.strongholdkingdoms.com/support/?webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.languageIdent;
            new Process() { StartInfo = { FileName = str } }.Start();
            break;
          }
          catch (Exception ex)
          {
            break;
          }
        case 20999:
          MainMenuBar2.castleCopyMode = true;
          break;
        case 21001:
          string fileName1 = URLs.AccountInfoURL + "?webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
          try
          {
            Process.Start(fileName1);
            break;
          }
          catch (Exception ex)
          {
            int num4 = (int) MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
            break;
          }
        case 21002:
          string fileName2 = URLs.InviteAFriendURL + "?webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
          try
          {
            Process.Start(fileName2);
            break;
          }
          catch (Exception ex)
          {
            int num5 = (int) MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
            break;
          }
        case 21003:
          string fileName3 = URLs.AccountInfoURL + "?section=codes&webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
          try
          {
            Process.Start(fileName3);
            break;
          }
          catch (Exception ex)
          {
            int num6 = (int) MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
            break;
          }
        case 21009:
          CreateVacationWindow.showVacationMode();
          break;
        case 40001:
          if (!GameEngine.Instance.World.WorldEnded)
          {
            if (GameEngine.Instance.LocalWorldData.EraWorld)
              break;
            GloryRoundData houseGloryRoundData = GameEngine.Instance.World.HouseGloryRoundData;
            WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo();
            WorldMap.VillageRolloverInfo villageInfo = (WorldMap.VillageRolloverInfo) null;
            userInfo.userID = houseGloryRoundData.marshallUserID;
            GameEngine.Instance.World.retrieveUserData(-1, houseGloryRoundData.marshallUserID, ref villageInfo, ref userInfo, true, true);
            break;
          }
          RemoteServices.Instance.set_EndOfTheWorldStats_UserCallBack(new RemoteServices.EndOfTheWorldStats_UserCallBack(this.endOfTheWorldCallback));
          RemoteServices.Instance.EndOfTheWorldStats();
          break;
        case 40002:
          if (!GameEngine.Instance.World.WorldEnded)
          {
            if (GameEngine.Instance.LocalWorldData.EraWorld)
              break;
            GloryRoundData houseGloryRoundData = GameEngine.Instance.World.HouseGloryRoundData;
            WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo();
            WorldMap.VillageRolloverInfo villageInfo = (WorldMap.VillageRolloverInfo) null;
            userInfo.userID = houseGloryRoundData.marshallUserID;
            GameEngine.Instance.World.retrieveUserData(-1, houseGloryRoundData.marshallUserID, ref villageInfo, ref userInfo, true, true);
            if (userInfo == null)
              break;
            this.SavePNGFileDialog.Title = "Map Data Export";
            if (this.SavePNGFileDialog.ShowDialog() != DialogResult.OK)
              break;
            Avatar.CreateExportAvatar(userInfo, this.SavePNGFileDialog.FileName);
            break;
          }
          if (WorldsEndPanel.cachedData == null || WorldsEndPanel.cachedData.globalData == null || WorldsEndPanel.cachedData.globalData.winningUser <= 0)
            break;
          WorldMap.CachedUserInfo userInfo1 = new WorldMap.CachedUserInfo();
          WorldMap.VillageRolloverInfo villageInfo1 = (WorldMap.VillageRolloverInfo) null;
          userInfo1.userID = WorldsEndPanel.cachedData.globalData.winningUser;
          GameEngine.Instance.World.retrieveUserData(-1, WorldsEndPanel.cachedData.globalData.winningUser, ref villageInfo1, ref userInfo1, true, true);
          if (userInfo1 == null)
            break;
          this.SavePNGFileDialog.Title = "Map Data Export";
          if (this.SavePNGFileDialog.ShowDialog() != DialogResult.OK)
            break;
          Avatar.CreateExportAvatar(userInfo1, this.SavePNGFileDialog.FileName);
          break;
      }
    }

    private void btnAdminMenu_Click()
    {
      MenuPopup menuPopup = new MenuPopup();
      Point screen = this.csd.PointToScreen(this.btnAdminMenu.Position);
      menuPopup.setPosition(screen.X, screen.Y + 24);
      menuPopup.setCallBack(new MenuPopup.MenuCallback(this.menu1Callback));
      menuPopup.addMenuItem("Edit Admin Message", 201);
      menuPopup.addMenuItem("Retrieve Game Info", 203);
      menuPopup.addBar();
      menuPopup.addMenuItem("Country Playback (Admins Only)", 202);
      menuPopup.addMenuItem("Province Playback (Admins Only)", 9202);
      menuPopup.addMenuItem("Stop Playback (Admins Only)", 9203);
      menuPopup.addBar();
      menuPopup.addMenuItem("Fix Lost Units (CAREFUL!)", 209);
      menuPopup.addMenuItem("Castle Copy Mode", 20999);
      if (!GameEngine.Instance.World.MapEditing)
      {
        menuPopup.addMenuItem("Open County - Select Capital", 2219);
      }
      else
      {
        int villageID = GameEngine.Instance.World.lastClickedVillage();
        if (GameEngine.Instance.World.isCountyCapital(villageID) && !GameEngine.Instance.World.isVillageVisible(villageID))
        {
          int countyFromVillageId = GameEngine.Instance.World.getCountyFromVillageID(villageID);
          menuPopup.addMenuItem("Open County : " + GameEngine.Instance.World.getCountyName(countyFromVillageId), 2219);
        }
        else
          menuPopup.addMenuItem("Open County : NONE SELECTED", 2219);
      }
      menuPopup.addBar();
      menuPopup.addMenuItem("Prep Winners Avatar", 40001);
      menuPopup.addMenuItem("Export Winners Avatar", 40002);
      menuPopup.addBar();
      menuPopup.addMenuItem("Toggle Village IDs", 231);
      menuPopup.addMenuItem("Toggle Village Names", 232);
      menuPopup.addBar();
      menuPopup.addMenuItem("Create Ingame Message", 221);
      menuPopup.addMenuItem("Remove Ingame Message", 223);
      menuPopup.showMenu();
    }

    private void CompleteVillageCastleCallBack(CompleteVillageCastle_ReturnType returnData)
    {
      if (returnData.Success && returnData.cardData >= 0)
        GameEngine.Instance.cardsManager.addProfileCard(returnData.cardData, CardTypes.getStringFromCard(3080));
      if (returnData.Success && this.fixCommandSent)
      {
        int num = (int) MyMessageBox.Show("Armies : " + returnData.armies.ToString() + Environment.NewLine + "Monks : " + returnData.monks.ToString() + Environment.NewLine + "Traders : " + returnData.traders.ToString() + Environment.NewLine + "Cards : " + returnData.cards.ToString() + Environment.NewLine, "Fixes");
      }
      this.fixCommandSent = false;
    }

    private void btnMail_Click(object sender, EventArgs e)
    {
    }

    private void btnMail_MouseEnter(object sender, EventArgs e)
    {
      if (!MenuPopup.isAMenuVisible())
        return;
      this.btnMail_Click((object) null, (EventArgs) null);
    }

    private void btnCombat_Click()
    {
    }

    public void setLoadingLight(bool loading) => this.pnlLoadLight.Visible = loading;

    public void retrieveGameStats()
    {
      RemoteServices.Instance.set_RetrieveStats_UserCallBack(new RemoteServices.RetrieveStats_UserCallBack(this.retrieveStatsCallback));
      RemoteServices.Instance.RetrieveStats();
    }

    public void retrieveStatsCallback(RetrieveStats_ReturnType returnData)
    {
      if (!returnData.Success || returnData.mapHistory == null)
        return;
      GameEngine.Instance.World.setPlaybackData(returnData.mapHistory, returnData.worldStartTime);
      if (this.nextPlaybackCountries)
        GameEngine.Instance.World.playbackCountries();
      else
        GameEngine.Instance.World.playbackProvinces();
    }

    public void retrieveGameInfo()
    {
      RemoteServices.Instance.set_GetAdminStats_UserCallBack(new RemoteServices.GetAdminStats_UserCallBack(this.getAdminStatsCallback));
      RemoteServices.Instance.GetAdminStats();
    }

    public void getAdminStatsCallback(GetAdminStats_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      AdminStatsPopup adminStatsPopup = new AdminStatsPopup();
      adminStatsPopup.init(returnData);
      adminStatsPopup.Show();
    }

    public void shutdownMessage()
    {
    }

    public void createIngameMessage() => new AdminIngameMessage().Show();

    public void clearIngameMessage() => RemoteServices.Instance.SetAdminMessage("", 1000);

    public static void VillageRenameCallback(VillageRename_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (!returnData.abandoned)
          return;
        GameEngine.Instance.World.newPlayer = false;
        GameEngine.Instance.World.lastAttacker = RemoteServices.Instance.UserName;
        GameEngine.Instance.World.lastAttackerLastUpdate = DateTime.Now;
        GameEngine.Instance.flushVillages();
        GameEngine.Instance.forceFullTick();
      }
      else
      {
        if (returnData.m_errorCode == ErrorCodes.ErrorCode.ABANDONED_TOO_SOON)
        {
          int num1 = (int) MyMessageBox.Show(SK.Text("MENU_Abandon_Once_Week", "You can only abandon your village once a week"), SK.Text("MENU_Abandon_Village_Error", "Abandon Village Error"));
        }
        if (returnData.m_errorCode != ErrorCodes.ErrorCode.CANT_ABANDON_WITH_INCOMING_ATTACKS)
          return;
        int num2 = (int) MyMessageBox.Show(SK.Text("MENU_Abandon_Incoming", "You cannot abandon your village while you have incoming attacks"), SK.Text("MENU_Abandon_Village_Error", "Abandon Village Error"));
      }
    }

    public void setServerTime(DateTime serverTime, int gameDay)
    {
      int playbackDay = GameEngine.Instance.World.getPlaybackDay();
      if (playbackDay >= 0)
      {
        InterfaceMgr.Instance.getTopLeftMenu().setServerTime(playbackDay.ToString());
      }
      else
      {
        bool flag = false;
        if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        {
          TimeSpan dominationTimeLeft = GameEngine.Instance.getDominationTimeLeft();
          if (dominationTimeLeft.TotalHours < 15.0)
            flag = true;
          int totalSeconds = (int) dominationTimeLeft.TotalSeconds;
          int num1 = totalSeconds % 60;
          int num2 = totalSeconds / 60 % 60;
          int num3 = totalSeconds / 3600 % 24;
          int num4 = totalSeconds / 86400;
          if (dominationTimeLeft.TotalHours >= 24.0)
          {
            string str1 = num4.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + ":";
            string str2 = num3 != 0 ? (num3 >= 10 ? str1 + num3.ToString() + ":" : str1 + "0" + num3.ToString() + ":") : str1 + "00:";
            string str3 = num2 != 0 ? (num2 >= 10 ? str2 + num2.ToString() + ":" : str2 + "0" + num2.ToString() + ":") : str2 + "00:";
            string text = num1 != 0 ? (num1 >= 10 ? str3 + num1.ToString() : str3 + "0" + num1.ToString()) : str3 + "00";
            if (flag)
              InterfaceMgr.Instance.getTopLeftMenu().setServerTime(SK.Text("Dom_Time_Left", "Time Remaining") + " " + text);
            InterfaceMgr.Instance.updateDominationWindow(text);
          }
          else
          {
            string str4 = "";
            string str5 = num3 != 0 ? (num3 >= 10 ? str4 + num3.ToString() + ":" : str4 + "0" + num3.ToString() + ":") : str4 + "00:";
            string str6 = num2 != 0 ? (num2 >= 10 ? str5 + num2.ToString() + ":" : str5 + "0" + num2.ToString() + ":") : str5 + "00:";
            string text = num1 != 0 ? (num1 >= 10 ? str6 + num1.ToString() : str6 + "0" + num1.ToString()) : str6 + "00";
            if (flag)
              InterfaceMgr.Instance.getTopLeftMenu().setServerTime(SK.Text("Dom_Time_Left", "Time Remaining") + " " + text);
            InterfaceMgr.Instance.updateDominationWindow(text);
          }
        }
        if (flag)
          return;
        InterfaceMgr.Instance.getTopLeftMenu().setServerTime(SK.Text("MENU_Day_X", "Day") + " " + gameDay.ToString() + " - " + serverTime.ToLongTimeString());
      }
    }

    private void btnLogOut_Click()
    {
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_logout");
      if (this.logoutPopup != null && this.logoutPopup.Created)
      {
        this.logoutPopup.Close();
        this.logoutPopup = (LogoutOptionsWindow2) null;
      }
      this.logoutPopup = InterfaceMgr.Instance.openLogoutWindow(true);
    }

    public void newMail(bool newMail) => this.MenuButtonsPanel.newMail(newMail);

    public void setMailAlpha(double alpha) => this.MenuButtonsPanel.setMailAlpha(alpha);

    public void setContestLeaderboardButtonVisible(bool visible)
    {
      this.MenuButtonsPanel.setContestLeaderboardButtonVisible(visible);
    }

    public void resize()
    {
      this.mainBackgroundImage.Size = new Size(this.Size.Width, 29);
      this.btnHelpMenu.Position = new Point(this.Width - 140, 1);
      this.btnLogOut.Position = new Point(this.Width - 72, 1);
      this.btnFileMenu.Position = new Point(this.Width - 217, 1);
      this.btnMyAccount.Position = new Point(this.Width - 297, 1);
      this.pnlLoadLight.Position = new Point(this.Width - 4, 0);
      this.MenuButtonsPanel.Position = new Point(this.Width - 464, 0);
    }

    private void endOfTheWorldCallback(EndOfTheWorldStats_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      WorldsEndPanel.cachedData = returnData;
    }
  }
}
