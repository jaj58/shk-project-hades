// Decompiled with JetBrains decompiler
// Type: Kingdoms.ParishCapitalVillagePanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ParishCapitalVillagePanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton reinforceButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton monkButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel lblProtected = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblProtectionType = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton leftButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton rightButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel lblPlagueValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton mapEdit = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage backImage;
    private int lastPlague = -100;
    private int selectedProtection;
    private int numInfos;
    private int m_selectedVillage = -1;

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
      this.Name = nameof (ParishCapitalVillagePanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }

    public ParishCapitalVillagePanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.clearControls();
      this.backImage = this.backGround.init(true, 10000);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backImage);
      this.tradeButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.TRADE);
      this.tradeButton.Position = new Point(10, 49);
      this.tradeButton.Enabled = false;
      this.tradeButton.CustomTooltipID = 2410;
      this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnTradeWith_Click), "ParishCapitalVillagePanel2_trade");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.tradeButton);
      this.attackButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.ATTACK);
      this.attackButton.Position = new Point(45, 49);
      this.attackButton.Enabled = false;
      this.attackButton.CustomTooltipID = 2411;
      this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAttack_Click), "ParishCapitalVillagePanel2_attack");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton);
      this.scoutButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.SCOUT);
      this.scoutButton.Position = new Point(80, 49);
      this.scoutButton.Enabled = false;
      this.scoutButton.CustomTooltipID = 2412;
      this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnScout_Click), "ParishCapitalVillagePanel2_scout");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.scoutButton);
      this.reinforceButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.REINFORCE);
      this.reinforceButton.Position = new Point(115, 49);
      this.reinforceButton.Enabled = false;
      this.reinforceButton.CustomTooltipID = 2413;
      this.reinforceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnSendTroops_Click), "ParishCapitalVillagePanel2_reinforce");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.reinforceButton);
      this.monkButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.MONK);
      this.monkButton.Position = new Point(150, 49);
      this.monkButton.Enabled = false;
      this.monkButton.CustomTooltipID = 2414;
      this.monkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnSendMonks_Click), "ParishCapitalVillagePanel2_sendmonks");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.monkButton);
      this.castleButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.LAST_REPORT);
      this.castleButton.Position = new Point(82, 112);
      this.castleButton.CustomTooltipID = 2445;
      this.castleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "ParishCapitalVillagePanel2_view_castle");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleButton);
      if (GameEngine.Instance.World.MapEditing)
      {
        this.mapEdit.ImageNorm = (Image) GFXLibrary.faction_pen;
        this.mapEdit.ImageOver = (Image) GFXLibrary.faction_pen;
        this.mapEdit.ImageClick = (Image) GFXLibrary.faction_pen;
        this.mapEdit.MoveOnClick = true;
        this.mapEdit.OverBrighten = true;
        this.mapEdit.Position = new Point(168, 112);
        this.mapEdit.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mapEditClicked));
        this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.mapEdit);
      }
      this.lblProtectionType.Text = "";
      this.lblProtectionType.Color = ARGBColors.Black;
      this.lblProtectionType.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.lblProtectionType.Position = new Point(0, 38);
      this.lblProtectionType.Size = new Size(this.backImage.Width, 23);
      this.lblProtectionType.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblProtectionType);
      this.lblProtected.Text = "";
      this.lblProtected.Color = ARGBColors.Black;
      this.lblProtected.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.lblProtected.Position = new Point(6, 48);
      this.lblProtected.Size = new Size(this.backImage.Width - 12, 74);
      this.lblProtected.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblProtected);
      this.leftButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_left_norm;
      this.leftButton.ImageOver = (Image) GFXLibrary.r_arrow_small_left_over;
      this.leftButton.Position = new Point(5, 50);
      this.leftButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoLeft), "ParishCapitalVillagePanel2_protection_left");
      this.leftButton.Visible = false;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.leftButton);
      this.rightButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_right_norm;
      this.rightButton.ImageOver = (Image) GFXLibrary.r_arrow_small_right_over;
      this.rightButton.Position = new Point(170, 50);
      this.rightButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoRight), "ParishCapitalVillagePanel2_protection_right");
      this.rightButton.Visible = false;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.rightButton);
      this.lblPlagueValue.Text = "";
      this.lblPlagueValue.Color = ARGBColors.Black;
      this.lblPlagueValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.lblPlagueValue.Position = new Point(82, 10);
      this.lblPlagueValue.Size = new Size(48, 22);
      this.lblPlagueValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.lblPlagueValue);
      this.lastPlague = -100;
      this.numInfos = 0;
      this.selectedProtection = 0;
    }

    private void updateSize()
    {
      bool visible = this.lblProtectionType.Visible;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      if (!visible)
      {
        this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_132;
        num1 = -63;
        num2 = -4;
      }
      else
        this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_192;
      if (!this.castleButton.Visible)
        num3 = -15;
      this.tradeButton.Position = new Point(10, 142 + num1 + num3);
      this.attackButton.Position = new Point(45, 142 + num1 + num3);
      this.scoutButton.Position = new Point(80, 142 + num1 + num3);
      this.reinforceButton.Position = new Point(115, 142 + num1 + num3);
      this.monkButton.Position = new Point(150, 142 + num1 + num3);
      this.castleButton.Position = new Point(82, 112 + num1 + num2);
      this.mapEdit.Position = new Point(168, 112 + num1 + num2);
      this.backGround.invalidate();
    }

    public void update()
    {
      this.backGround.update();
      int[] numArray = new int[3];
      TimeSpan[] timeSpanArray = new TimeSpan[3];
      int numInfos = this.numInfos;
      this.numInfos = 0;
      int parishPlagueLevel = GameEngine.Instance.World.getParishPlagueLevel(this.m_selectedVillage);
      if (parishPlagueLevel != this.lastPlague)
      {
        if (parishPlagueLevel <= 0)
        {
          this.backGround.updatePanelType(1500);
          this.lblPlagueValue.TextDiffOnly = "";
        }
        else if (this.lastPlague <= 0)
        {
          this.backGround.updatePanelType(1504);
          this.lblPlagueValue.TextDiffOnly = parishPlagueLevel.ToString();
        }
        this.backGround.setTooltipData(parishPlagueLevel);
        this.lastPlague = parishPlagueLevel;
      }
      bool visible = this.lblProtectionType.Visible;
      int num = 0;
      TimeSpan timeSpan1 = new TimeSpan();
      if (GameEngine.Instance.World.isVillageInterdictProtected(this.m_selectedVillage))
      {
        timeSpan1 = GameEngine.Instance.World.getInterdictTime(this.m_selectedVillage) - VillageMap.getCurrentServerTime();
        num = 1;
        timeSpanArray[this.numInfos] = timeSpan1;
        numArray[this.numInfos] = num;
        ++this.numInfos;
      }
      if (GameEngine.Instance.World.isVillagePeaceTimeProtected(this.m_selectedVillage))
      {
        TimeSpan timeSpan2 = GameEngine.Instance.World.getPeaceTime(this.m_selectedVillage) - VillageMap.getCurrentServerTime();
        if (timeSpan2 > timeSpan1)
        {
          timeSpan1 = timeSpan2;
          num = 2;
          timeSpanArray[this.numInfos] = timeSpan1;
          numArray[this.numInfos] = num;
          ++this.numInfos;
        }
      }
      if (this.numInfos > 0)
      {
        if (this.selectedProtection < this.numInfos)
        {
          num = numArray[this.numInfos - 1 - this.selectedProtection];
          timeSpan1 = timeSpanArray[this.numInfos - 1 - this.selectedProtection];
        }
        else
          this.selectedProtection = 0;
      }
      switch (num)
      {
        case 1:
          string buildTimeStringFull1 = VillageMap.createBuildTimeStringFull((int) timeSpan1.TotalSeconds);
          this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + buildTimeStringFull1;
          this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Interdict", "Interdict");
          this.lblProtectionType.Visible = true;
          break;
        case 2:
          string buildTimeStringFull2 = VillageMap.createBuildTimeStringFull((int) timeSpan1.TotalSeconds);
          this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + buildTimeStringFull2;
          this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Peace", "Peace");
          this.lblProtectionType.Visible = true;
          break;
        default:
          this.lblProtected.TextDiffOnly = "";
          this.lblProtectionType.TextDiffOnly = "";
          this.lblProtectionType.Visible = false;
          break;
      }
      if (visible != this.lblProtectionType.Visible)
      {
        this.updateSize();
        if (!visible)
          this.selectedProtection = 0;
      }
      if (numInfos == this.numInfos)
        return;
      if (this.numInfos >= 2)
      {
        this.leftButton.Visible = true;
        this.rightButton.Visible = true;
      }
      else
      {
        this.leftButton.Visible = false;
        this.rightButton.Visible = false;
      }
    }

    public void updateParishCapitalVillageText(int selectedVillage, int ownVillage)
    {
      bool flag = true;
      this.m_selectedVillage = selectedVillage;
      this.lastPlague = -100;
      this.lblPlagueValue.TextDiffOnly = "";
      this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(selectedVillage));
      this.backGround.updatePanelTypeFromVillageID(selectedVillage);
      this.backGround.setActionFromVillage(ownVillage, selectedVillage);
      if (ownVillage < 0 || !GameEngine.Instance.World.isUserVillage(ownVillage))
      {
        this.scoutButton.Enabled = false;
        this.tradeButton.Enabled = false;
        this.attackButton.Enabled = false;
        this.monkButton.Enabled = false;
        this.reinforceButton.Enabled = false;
      }
      else
      {
        this.scoutButton.Enabled = true;
        this.tradeButton.Enabled = true;
        this.attackButton.Enabled = true;
        this.monkButton.Enabled = true;
        this.reinforceButton.Enabled = true;
        if (GameEngine.Instance.World.isCapital(ownVillage))
        {
          this.scoutButton.Enabled = false;
          this.tradeButton.Enabled = false;
          this.reinforceButton.Enabled = false;
          this.monkButton.Enabled = false;
          flag = false;
        }
        else
          this.scoutButton.Enabled = true;
        if (selectedVillage < 0 || ownVillage < 0)
        {
          this.tradeButton.Enabled = false;
        }
        else
        {
          if (!GameEngine.Instance.World.allowExchangeTrade(selectedVillage, ownVillage))
            flag = false;
          if (flag)
            this.tradeButton.Enabled = true;
          else
            this.tradeButton.Enabled = false;
        }
      }
      if ((this.attackButton.Enabled || this.scoutButton.Enabled) && GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld)
      {
        VillageData villageData = GameEngine.Instance.World.getVillageData(selectedVillage);
        if (villageData != null)
        {
          if (GameEngine.Instance.World.isHeretic())
          {
            if (villageData.factionID >= 1 && villageData.factionID <= 4)
            {
              this.attackButton.Enabled = false;
              this.scoutButton.Enabled = false;
            }
          }
          else if (villageData.factionID < 1 || villageData.factionID > 4)
          {
            this.attackButton.Enabled = false;
            this.scoutButton.Enabled = false;
          }
        }
      }
      if (!GameEngine.Instance.World.doesUserHaveVillageInParishByCapital(this.m_selectedVillage))
        this.castleButton.Visible = true;
      else
        this.castleButton.Visible = false;
      this.updateSize();
      this.update();
    }

    private void btnTradeWith_Click()
    {
      if (this.m_selectedVillage < 0)
        return;
      InterfaceMgr.Instance.selectStockExchange(-1);
      GameEngine.Instance.SkipVillageTab();
      InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      InterfaceMgr.Instance.setVillageTabSubMode(3);
      InterfaceMgr.Instance.resetVillageReportPanelData();
      InterfaceMgr.Instance.selectStockExchange(this.m_selectedVillage);
    }

    private void btnSendCourtiers_Click()
    {
      if (this.m_selectedVillage < 0)
        return;
      InterfaceMgr.Instance.showParishPanel(0);
    }

    private void btnAttack_Click()
    {
      GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.OwnSelectedVillage, this.m_selectedVillage);
    }

    private void btnSendTroops_Click()
    {
      GameEngine.Instance.SkipVillageTab();
      InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      InterfaceMgr.Instance.setCapitalSendTargetVillage(this.m_selectedVillage);
      InterfaceMgr.Instance.setVillageTabSubMode(17);
    }

    private void btnScout_Click()
    {
      if (this.m_selectedVillage < 0)
        return;
      InterfaceMgr.Instance.openScoutPopupWindow(this.m_selectedVillage, true);
    }

    private void btnSendMonks_Click()
    {
      if (this.m_selectedVillage < 0)
        return;
      InterfaceMgr.Instance.openSendMonkWindow(this.m_selectedVillage);
    }

    private void castleClick()
    {
      if (GameEngine.Instance.World.doesUserHaveVillageInParishByCapital(this.m_selectedVillage))
        return;
      RemoteServices.Instance.set_ViewCastle_UserCallBack(new RemoteServices.ViewCastle_UserCallBack(this.viewCastleCallback));
      RemoteServices.Instance.ViewCastle_Village(this.m_selectedVillage);
    }

    public void viewCastleCallback(ViewCastle_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.closeControl(true);
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(6);
      GameEngine.Instance.InitCastleView(returnData.castleMapSnapshot, returnData.castleTroopsSnapshot, returnData.keepLevel, 0, returnData.defencesLevel, returnData.villageID, returnData.landType);
      CastleMapBattlePanel2.fromWorld();
      InterfaceMgr.Instance.castleBattleTimes(returnData.lastCastleTime, returnData.lastTroopTime);
    }

    private void infoLeft()
    {
      --this.selectedProtection;
      if (this.selectedProtection >= 0)
        return;
      this.selectedProtection = this.numInfos - 1;
    }

    private void infoRight()
    {
      ++this.selectedProtection;
      if (this.selectedProtection < this.numInfos)
        return;
      this.selectedProtection = 0;
    }

    private void mapEditClicked()
    {
      RenameVillagePopup renameVillagePopup = new RenameVillagePopup();
      renameVillagePopup.setParishVillageID(this.m_selectedVillage, GameEngine.Instance.World.getVillageName(this.m_selectedVillage));
      renameVillagePopup.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
    }
  }
}
