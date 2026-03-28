// Decompiled with JetBrains decompiler
// Type: Kingdoms.CastleMapBattlePanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CastleMapBattlePanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private static CastleMapBattlePanel2 Instance = (CastleMapBattlePanel2) null;
    private CustomSelfDrawPanel.CSDArea backgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage backPanelImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel attackTypeLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pillageClockLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton pauseButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton speedButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton heightButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel viewCastleHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel viewCastleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel viewTroopsHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel viewTroopsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel reportHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDColorBar reportBar = new CustomSelfDrawPanel.CSDColorBar();
    private CustomSelfDrawPanel.CSDColorBar pillageBar = new CustomSelfDrawPanel.CSDColorBar();
    private CustomSelfDrawPanel.CSDLabel reportClockLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton adminExportAllButton = new CustomSelfDrawPanel.CSDButton();
    public static bool fromReports = true;
    private static bool paused = false;
    private static bool fast = false;
    private static bool high = false;
    private bool resultsMode;
    private int m_attackType = -1;
    private bool m_aiAttack;
    private bool m_attackerVictory;
    private BattleTroopNumbers m_startingTroops;
    private BattleTroopNumbers m_endingTroops;
    private int m_villageID = -1;
    private GetReport_ReturnType m_reportReturnData;
    private BattleResultPopup battleResultPopup;
    private DockableControl dockableControl;
    private IContainer components;

    public CastleMapBattlePanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
      CastleMapBattlePanel2.Instance = this;
    }

    public void create() => this.initCastlePlacePanel();

    public void initCastlePlacePanel()
    {
      this.backgroundArea.Position = new Point(0, 0);
      this.backgroundArea.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundArea);
      this.backPanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_A;
      this.backPanelImage.Position = new Point(0, 0);
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.backPanelImage);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(153, 6);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CastleMapBattlePanel2_close");
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      this.attackTypeLabel.Color = ARGBColors.Black;
      this.attackTypeLabel.Position = new Point(0, 33);
      this.attackTypeLabel.Size = new Size(this.backPanelImage.Width - 2, 24);
      this.attackTypeLabel.Text = SK.Text("GENERIC_Attack_Type", "Attack Type");
      this.attackTypeLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.attackTypeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackTypeLabel);
      this.pillageClockLabel.Color = ARGBColors.Black;
      this.pillageClockLabel.Position = new Point(0, 30);
      this.pillageClockLabel.Size = new Size(this.backPanelImage.Width, 80);
      this.pillageClockLabel.Text = "0";
      this.pillageClockLabel.Visible = false;
      this.pillageClockLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.pillageClockLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.pillageClockLabel);
      this.pillageBar.setImages((Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right, (Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right);
      this.pillageBar.SetMargin(2, 2, 2, 3);
      this.pillageBar.Number = 0.0;
      this.pillageBar.MaxValue = 1.0;
      this.pillageBar.Visible = false;
      this.pillageBar.Position = new Point(21, 85);
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.pillageBar);
      this.pauseButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.pauseButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.pauseButton.Position = new Point(21, 195);
      this.pauseButton.Text.Text = SK.Text("CastleMapBattle_Pause", "Pause");
      this.pauseButton.TextYOffset = 0;
      this.pauseButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.pauseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.togglePauseClick), "CastleMapBattlePanel2_pause");
      this.backPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.pauseButton);
      this.speedButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.speedButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.speedButton.Position = new Point(21, 245);
      this.speedButton.Text.Text = SK.Text("CastleMapBattle_Fast", "Fast");
      this.speedButton.TextYOffset = 0;
      this.speedButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.speedButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleSpeedClick), "CastleMapBattlePanel2_speed");
      this.backPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.speedButton);
      this.heightButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_viewmode_normal;
      this.heightButton.ImageOver = (Image) GFXLibrary.r_building_miltary_viewmode_over;
      this.heightButton.ImageClick = (Image) GFXLibrary.r_building_miltary_viewmode_pushed;
      this.heightButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleHeightClick));
      this.heightButton.Position = new Point(58, 295);
      this.backPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.heightButton);
      this.adminExportAllButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.adminExportAllButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.adminExportAllButton.Position = new Point(21, 175);
      this.adminExportAllButton.Text.Text = "Export";
      this.adminExportAllButton.TextYOffset = 0;
      this.adminExportAllButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.adminExportAllButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exportClick));
      this.adminExportAllButton.Visible = false;
      this.backPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.adminExportAllButton);
      this.viewCastleHeadingLabel.Color = ARGBColors.Black;
      this.viewCastleHeadingLabel.Position = new Point(0, 73);
      this.viewCastleHeadingLabel.Size = new Size(this.backPanelImage.Width - 2, 44);
      this.viewCastleHeadingLabel.Text = SK.Text("CastleMapBattle_Castle_Last_Update", "Castle Last Update");
      this.viewCastleHeadingLabel.Visible = false;
      this.viewCastleHeadingLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.viewCastleHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.viewCastleHeadingLabel);
      this.viewCastleLabel.Color = ARGBColors.Black;
      this.viewCastleLabel.Position = new Point(0, 108);
      this.viewCastleLabel.Size = new Size(this.backPanelImage.Width - 2, 24);
      this.viewCastleLabel.Text = "...";
      this.viewCastleLabel.Visible = false;
      this.viewCastleLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.viewCastleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.viewCastleLabel);
      this.viewTroopsHeadingLabel.Color = ARGBColors.Black;
      this.viewTroopsHeadingLabel.Position = new Point(0, 133);
      this.viewTroopsHeadingLabel.Size = new Size(this.backPanelImage.Width - 2, 44);
      this.viewTroopsHeadingLabel.Text = SK.Text("CastleMapBattle_Troops_Last_Update", "Troops Last Update");
      this.viewTroopsHeadingLabel.Visible = false;
      this.viewTroopsHeadingLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.viewTroopsHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.viewTroopsHeadingLabel);
      this.viewTroopsLabel.Color = ARGBColors.Black;
      this.viewTroopsLabel.Position = new Point(0, 168);
      this.viewTroopsLabel.Size = new Size(this.backPanelImage.Width - 2, 24);
      this.viewTroopsLabel.Text = "...";
      this.viewTroopsLabel.Visible = false;
      this.viewTroopsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.viewTroopsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.viewTroopsLabel);
      this.reportHeadingLabel.Color = ARGBColors.Black;
      this.reportHeadingLabel.Position = new Point(20, 101);
      this.reportHeadingLabel.Size = new Size(this.backPanelImage.Width - 2 - 40, 34);
      this.reportHeadingLabel.Text = SK.Text("CastleMapBattle_Report_Unavailable", "Report Unavailable To Attacker");
      this.reportHeadingLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.reportHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.reportHeadingLabel);
      this.reportClockLabel.Color = ARGBColors.Black;
      this.reportClockLabel.Position = new Point(0, 107);
      this.reportClockLabel.Size = new Size(this.backPanelImage.Width, 80);
      this.reportClockLabel.Text = "0";
      this.reportClockLabel.Visible = false;
      this.reportClockLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.reportClockLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.reportClockLabel);
      this.reportBar.setImages((Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right, (Image) GFXLibrary.barracks_fillbar_back, (Image) GFXLibrary.barracks_fillbar_fill_left, (Image) GFXLibrary.barracks_fillbar_fill_mid, (Image) GFXLibrary.barracks_fillbar_fill_right);
      this.reportBar.SetMargin(2, 2, 2, 3);
      this.reportBar.Number = 0.0;
      this.reportBar.MaxValue = 1.0;
      this.reportBar.Visible = false;
      this.reportBar.Position = new Point(21, 160);
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.reportBar);
    }

    public void battleMode(bool realBattle, int attackType, bool aiAttack)
    {
      this.m_aiAttack = aiAttack;
      this.m_attackType = attackType;
      CastleMapBattlePanel2.fromReports = true;
      CastleMapBattlePanel2.paused = false;
      CastleMapBattlePanel2.fast = false;
      CastleMapBattlePanel2.high = false;
      this.resultsMode = false;
      this.updateButtons();
      this.speedButton.Visible = realBattle;
      this.pauseButton.Visible = realBattle;
      if (realBattle)
      {
        this.attackTypeLabel.Text = CastlesCommon.getAttackTypeLabel(attackType);
        if (this.attackTypeLabel.Text.Length == 0)
          this.attackTypeLabel.Text = SK.Text("GENERIC_Attacking", "Attacking");
        if (MainMenuBar2.CastleCopyMode)
        {
          this.adminExportAllButton.Visible = true;
          CastleMapBattlePanel2.paused = true;
          this.pauseButton.Text.Text = SK.Text("CastleMapBattle_Resume", "Resume");
          GameEngine.Instance.CastleBattle.pauseBattle();
        }
      }
      else if (attackType >= 0)
      {
        this.attackTypeLabel.Text = GameEngine.Instance.World.getVillageName(attackType);
      }
      else
      {
        switch (attackType - -12)
        {
          case 0:
            this.attackTypeLabel.Text = SK.Text("CommonDataTypes_Royal_Tower", "Royal Tower");
            break;
          case 1:
            this.attackTypeLabel.Text = SK.Text("GENERIC_Treasure_Castle", "Treasure Castle");
            break;
          case 2:
            this.attackTypeLabel.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
            break;
          case 3:
            this.attackTypeLabel.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
            break;
          case 4:
            this.attackTypeLabel.Text = SK.Text("GENERIC_Wolfs_Castle", "Wolf's Castle");
            break;
          case 5:
            this.attackTypeLabel.Text = SK.Text("GENERIC_Pigs_Castle", "Pig's Castle");
            break;
          case 6:
            this.attackTypeLabel.Text = SK.Text("GENERIC_Snakes_Castle", "Snake's Castle");
            break;
          case 7:
            this.attackTypeLabel.Text = SK.Text("GENERIC_Rats_Castle", "Rat's Castle");
            break;
          case 8:
            this.attackTypeLabel.Text = SK.Text("GENERIC_An_Empty_Village", "An empty village");
            break;
          case 9:
            this.attackTypeLabel.Text = SK.Text("GENERIC_Wolf_Camp", "Wolf Lair");
            break;
          case 10:
            this.attackTypeLabel.Text = SK.Text("GENERIC_Bandit_Camp", "Bandit Camp");
            break;
          default:
            this.attackTypeLabel.Text = "";
            break;
        }
      }
      this.viewCastleHeadingLabel.Visible = !realBattle;
      this.viewCastleLabel.Visible = !realBattle;
      this.viewTroopsHeadingLabel.Visible = !realBattle;
      this.viewTroopsLabel.Visible = !realBattle;
      if (realBattle)
        return;
      this.pillageClockLabel.Visible = false;
      this.pillageBar.Visible = false;
      this.reportBar.Visible = false;
      this.reportClockLabel.Visible = false;
      this.reportHeadingLabel.Visible = false;
    }

    public void setTimes(DateTime castleTime, DateTime troopTime)
    {
      this.reportHeadingLabel.Visible = false;
      this.pillageBar.Visible = false;
      this.pillageClockLabel.Visible = false;
      if (castleTime == DateTime.MaxValue)
      {
        this.viewCastleLabel.Visible = false;
        this.viewCastleHeadingLabel.Visible = false;
      }
      else
        this.viewCastleLabel.Text = !(castleTime == DateTime.MinValue) ? castleTime.ToShortDateString() + ":" + castleTime.ToShortTimeString() : SK.Text("CastleMapBattle_None_Available", "None Available");
      if (troopTime == DateTime.MaxValue)
      {
        this.viewTroopsLabel.Visible = false;
        this.viewTroopsHeadingLabel.Visible = false;
      }
      else if (troopTime == DateTime.MinValue)
        this.viewTroopsLabel.Text = SK.Text("CastleMapBattle_None_Available", "None Available");
      else
        this.viewTroopsLabel.Text = troopTime.ToShortDateString() + ":" + troopTime.ToShortTimeString();
    }

    public void setPillageClock(int pillageClock, int pillageClockMax)
    {
      if (pillageClock < 0)
      {
        this.pillageBar.Visible = false;
        this.pillageClockLabel.Visible = false;
      }
      else
      {
        this.pillageBar.Visible = true;
        this.pillageClockLabel.Visible = true;
        this.pillageBar.Number = (double) pillageClock;
        this.pillageClockLabel.Text = (pillageClock / 10).ToString();
        this.pillageBar.MaxValue = (double) pillageClockMax;
      }
    }

    public void setCastleReportClock(int reportClock, int reportClockMax)
    {
      if (this.m_aiAttack)
      {
        this.reportBar.Visible = false;
        this.reportClockLabel.Visible = false;
        this.reportHeadingLabel.Visible = false;
      }
      else
      {
        this.reportHeadingLabel.Visible = true;
        if (reportClock < 0)
        {
          this.reportBar.Visible = false;
          this.reportClockLabel.Visible = false;
          this.reportHeadingLabel.Text = SK.Text("CastleMapBattle_Report_Available", "Report Available To Attacker");
        }
        else
        {
          this.reportHeadingLabel.Text = SK.Text("CastleMapBattle_Report_Unavailable", "Report Unavailable To Attacker");
          this.reportBar.Visible = true;
          this.reportClockLabel.Visible = true;
          this.reportBar.Number = (double) reportClock;
          this.reportClockLabel.Text = (reportClock / 10).ToString();
          this.reportBar.MaxValue = (double) reportClockMax;
        }
      }
    }

    public static void fromWorld() => CastleMapBattlePanel2.Instance.fromWorldInst();

    private void fromWorldInst()
    {
      CastleMapBattlePanel2.fromReports = false;
      CastleMapBattlePanel2.paused = false;
      CastleMapBattlePanel2.fast = false;
      CastleMapBattlePanel2.high = false;
      this.resultsMode = false;
      this.updateButtons();
    }

    private void updateButtons()
    {
      if (!this.resultsMode)
      {
        this.pauseButton.Text.Text = !CastleMapBattlePanel2.paused ? SK.Text("CastleMapBattle_Pause", "Pause") : SK.Text("CastleMapBattle_Resume", "Resume");
        if (!CastleMapBattlePanel2.fast)
          this.speedButton.Text.Text = SK.Text("CastleMapBattle_Fast_Speed", "Fast Speed");
        else
          this.speedButton.Text.Text = SK.Text("CastleMapBattle_Normal_Speed", "Normal Speed");
      }
      else
        this.pauseButton.Text.Text = SK.Text("CastleMapBattle_View_Report", "View Report");
    }

    private void togglePauseClick()
    {
      if (!this.resultsMode)
      {
        CastleMapBattlePanel2.paused = !CastleMapBattlePanel2.paused;
        if (GameEngine.Instance.CastleBattle != null)
        {
          if (CastleMapBattlePanel2.paused)
            GameEngine.Instance.CastleBattle.pauseBattle();
          else
            GameEngine.Instance.CastleBattle.unpauseBattle();
        }
      }
      else
        this.ShowViewBattleResults(this.m_attackerVictory, this.m_startingTroops, this.m_endingTroops, this.m_villageID, this.m_reportReturnData);
      this.updateButtons();
    }

    private void toggleSpeedClick()
    {
      if (!this.resultsMode)
      {
        CastleMapBattlePanel2.fast = !CastleMapBattlePanel2.fast;
        if (GameEngine.Instance.CastleBattle != null)
          GameEngine.Instance.CastleBattle.setFastPlayback(CastleMapBattlePanel2.fast);
      }
      this.updateButtons();
    }

    private void toggleHeightClick()
    {
      CastleMapBattlePanel2.high = !CastleMapBattlePanel2.high;
      if (GameEngine.Instance.CastleBattle != null)
      {
        GameEngine.Instance.CastleBattle.toggleHeight();
        if (CastleMapBattlePanel2.high)
          GameEngine.Instance.playInterfaceSound("CastleMapBattlePanel2_height_high");
        else
          GameEngine.Instance.playInterfaceSound("CastleMapBattlePanel2_height_low");
      }
      this.updateButtons();
    }

    private void closeClick()
    {
      if (CastleMapBattlePanel2.fromReports)
        InterfaceMgr.Instance.getMainTabBar().changeTab(7);
      else
        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
    }

    public void closePopup(bool exit, bool tutorial)
    {
      if (exit)
      {
        this.closeClick();
      }
      else
      {
        this.resultsMode = true;
        this.speedButton.Visible = false;
        this.updateButtons();
      }
      if (!tutorial)
        return;
      PostTutorialWindow.CreatePostTutorialWindow(true);
    }

    public void ShowViewBattleResults(
      bool attackerVictory,
      BattleTroopNumbers startingTroops,
      BattleTroopNumbers endingTroops,
      int villageID,
      GetReport_ReturnType reportReturnData)
    {
      this.m_attackerVictory = attackerVictory;
      this.m_startingTroops = startingTroops;
      this.m_endingTroops = endingTroops;
      this.m_villageID = villageID;
      this.m_reportReturnData = reportReturnData;
      if (this.battleResultPopup != null)
      {
        if (this.battleResultPopup.Created)
          this.battleResultPopup.Close();
        this.battleResultPopup = (BattleResultPopup) null;
      }
      this.battleResultPopup = new BattleResultPopup();
      this.battleResultPopup.init(attackerVictory, startingTroops, endingTroops, this.m_attackType, villageID, reportReturnData, this);
      if (attackerVictory)
      {
        if (GameEngine.Instance.World.isUserVillage(villageID))
          Sound.playBattleEndDefeatMusic();
        else
          Sound.playBattleEndVictoryMusic();
      }
      else if (GameEngine.Instance.World.isUserVillage(villageID))
        Sound.playBattleEndVictoryMusic();
      else
        Sound.playBattleEndDefeatMusic();
      Form parentForm = InterfaceMgr.Instance.ParentForm;
      Size size = parentForm.Size;
      size.Width -= this.battleResultPopup.Width;
      size.Height -= this.battleResultPopup.Height;
      Point location = parentForm.Location;
      this.battleResultPopup.Location = new Point(location.X + size.Width / 2, location.Y + size.Height / 2);
      this.battleResultPopup.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
    }

    private void exportClick()
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.DefaultExt = "";
      saveFileDialog.Filter = "All Save Types (*.*)|*.*";
      saveFileDialog.Title = "Save Castle and attackers Data";
      if (saveFileDialog.ShowDialog() != DialogResult.OK)
        return;
      GameEngine.Instance.CastleBattle.DEBUG_SaveCastleMap(saveFileDialog.FileName + ".cmap");
      GameEngine.Instance.CastleBattle.DEBUG_SaveAIWorldSetup(saveFileDialog.FileName + ".txt");
      GameEngine.Instance.CastleBattle.saveCamp(saveFileDialog.FileName + ".camp");
      GameEngine.Instance.CastleBattle.memoriseAttackSetup("Export_" + Path.GetFileNameWithoutExtension(saveFileDialog.FileName));
    }

    public void initProperties(bool dockable, string title, ContainerControl parent)
    {
      this.dockableControl.initProperties(dockable, title, parent);
    }

    public void display(ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(parent, x, y);
    }

    public void display(bool asPopup, ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(asPopup, parent, x, y);
    }

    public void controlDockToggle() => this.dockableControl.controlDockToggle();

    public void closeControl(bool includePopups)
    {
      this.dockableControl.closeControl(includePopups);
    }

    public bool isVisible() => this.dockableControl.isVisible();

    public bool isPopup() => this.dockableControl.isPopup();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Transparent;
      this.Name = nameof (CastleMapBattlePanel2);
      this.Size = new Size(196, 566);
      this.ResumeLayout(false);
    }
  }
}
