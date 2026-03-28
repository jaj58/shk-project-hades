// Decompiled with JetBrains decompiler
// Type: Kingdoms.OtherVillagePanel2
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
  public class OtherVillagePanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton reinforceButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton monkButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton vassalButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton renameButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton leftButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton rightButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel lblProtected = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblProtectionType = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage backImage;
    private int selectedProtection;
    private int numInfos;
    private int m_selectedVillage = -1;
    private MyMessageBoxPopUp PopUpRef;

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
      this.Name = nameof (OtherVillagePanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }

    public OtherVillagePanel2()
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
      this.tradeButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.TRADE);
      this.tradeButton.Position = new Point(10, 142);
      this.tradeButton.CustomTooltipID = 2410;
      this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendResources), "OtherVillagePanel2_trade");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.tradeButton);
      this.attackButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.ATTACK);
      this.attackButton.Position = new Point(45, 142);
      this.attackButton.CustomTooltipID = 2411;
      this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendTroops), "OtherVillagePanel2_attack");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton);
      this.scoutButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.SCOUT);
      this.scoutButton.Position = new Point(80, 142);
      this.scoutButton.CustomTooltipID = 2412;
      this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendScouts), "OtherVillagePanel2_scout");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.scoutButton);
      this.reinforceButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.REINFORCE);
      this.reinforceButton.Position = new Point(115, 142);
      this.reinforceButton.CustomTooltipID = 2413;
      this.reinforceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendReinforcements), "OtherVillagePanel2_reinforce");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.reinforceButton);
      this.monkButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.MONK);
      this.monkButton.Position = new Point(150, 142);
      this.monkButton.CustomTooltipID = 2414;
      this.monkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendMonks), "OtherVillagePanel2_sendmonks");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.monkButton);
      this.vassalButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.VASSAL);
      this.vassalButton.Position = new Point(115, 112);
      this.vassalButton.CustomTooltipID = 2446;
      this.vassalButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.vassalClick), "OtherVillagePanel2_make_vassal");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.vassalButton);
      this.castleButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.LAST_REPORT);
      this.castleButton.Position = new Point(64, 112);
      this.castleButton.CustomTooltipID = 2445;
      this.castleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "OtherVillagePanel2_view_castle");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleButton);
      if (RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator)
      {
        this.renameButton.ImageNorm = (Image) GFXLibrary.faction_pen;
        this.renameButton.OverBrighten = true;
        this.renameButton.MoveOnClick = true;
        this.renameButton.Position = new Point(139, 57);
        this.renameButton.CustomTooltipID = 10390;
        this.renameButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resetNameClick));
        this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.renameButton);
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
      this.leftButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoLeft), "OtherVillagePanel2_protection_left");
      this.leftButton.Visible = false;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.leftButton);
      this.rightButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_right_norm;
      this.rightButton.ImageOver = (Image) GFXLibrary.r_arrow_small_right_over;
      this.rightButton.Position = new Point(170, 50);
      this.rightButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoRight), "OtherVillagePanel2_protection_right");
      this.rightButton.Visible = false;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.rightButton);
      this.updateSize();
      this.numInfos = 0;
      this.selectedProtection = 0;
    }

    private void updateSize()
    {
      bool visible = this.lblProtectionType.Visible;
      int num1 = 0;
      int num2 = 0;
      if (!visible)
      {
        this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_132;
        num1 = -63;
        num2 = -4;
      }
      else
        this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_192;
      this.tradeButton.Position = new Point(10, 142 + num1);
      this.attackButton.Position = new Point(45, 142 + num1);
      this.scoutButton.Position = new Point(80, 142 + num1);
      this.reinforceButton.Position = new Point(115, 142 + num1);
      this.monkButton.Position = new Point(150, 142 + num1);
      this.vassalButton.Position = new Point(96, 112 + num1 + num2);
      this.castleButton.Position = new Point(64, 112 + num1 + num2);
      this.renameButton.Position = new Point(149, 112 + num1 + num2);
      this.backGround.invalidate();
    }

    public void update()
    {
      this.backGround.update();
      int[] numArray = new int[3];
      TimeSpan[] timeSpanArray = new TimeSpan[3];
      int numInfos = this.numInfos;
      this.numInfos = 0;
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
        }
        timeSpanArray[this.numInfos] = timeSpan2;
        numArray[this.numInfos] = 2;
        ++this.numInfos;
      }
      if (GameEngine.Instance.World.isVillageVacationProtected(this.m_selectedVillage))
      {
        num = 3;
        numArray[this.numInfos] = 3;
        ++this.numInfos;
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
        case 3:
          this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked", "Cannot be attacked");
          this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Vacation", "Vacation Mode");
          this.lblProtectionType.Visible = true;
          break;
        default:
          GameEngine.Instance.World.getVillageFaction(InterfaceMgr.Instance.OwnSelectedVillage);
          GameEngine.Instance.World.getVillageFaction(this.m_selectedVillage);
          if (GameEngine.Instance.World.isUserVillage(this.m_selectedVillage))
          {
            this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Attack_Own_Village", "Cannot attack your own village");
            this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Your_Village", "Your Village");
            this.lblProtectionType.Visible = true;
            break;
          }
          this.lblProtected.TextDiffOnly = "";
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

    public void updateOtherVillageText(int selectedVillage)
    {
      this.m_selectedVillage = selectedVillage;
      this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(selectedVillage));
      this.backGround.updatePanelTypeFromVillageID(selectedVillage);
      this.backGround.setActionFromVillage(InterfaceMgr.Instance.getSelectedMenuVillage(), selectedVillage);
      if (GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
      {
        this.tradeButton.Enabled = false;
        this.vassalButton.Enabled = false;
        this.monkButton.Enabled = false;
        this.scoutButton.Enabled = false;
        if (!GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()))
        {
          this.attackButton.Enabled = false;
          this.reinforceButton.Enabled = false;
        }
        else
        {
          this.attackButton.Enabled = true;
          this.reinforceButton.Enabled = true;
        }
      }
      else
      {
        this.attackButton.Enabled = true;
        this.reinforceButton.Enabled = true;
        this.tradeButton.Enabled = true;
        this.scoutButton.Enabled = true;
        this.monkButton.Enabled = true;
        if (GameEngine.Instance.World.numVassalsAllowed() > GameEngine.Instance.World.countVassals())
          this.vassalButton.Enabled = true;
        else
          this.vassalButton.Enabled = false;
      }
      if ((this.attackButton.Enabled || this.scoutButton.Enabled || this.vassalButton.Enabled) && GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld)
      {
        VillageData villageData = GameEngine.Instance.World.getVillageData(selectedVillage);
        if (villageData != null)
        {
          if (GameEngine.Instance.World.isHeretic())
          {
            this.vassalButton.Enabled = false;
            if (villageData.factionID == 4)
            {
              this.attackButton.Enabled = false;
              this.scoutButton.Enabled = false;
            }
          }
          else if (villageData.factionID != 4)
          {
            this.attackButton.Enabled = false;
            this.scoutButton.Enabled = false;
          }
        }
      }
      this.updateSize();
      this.update();
    }

    public void forceDisable()
    {
      this.attackButton.Enabled = false;
      this.vassalButton.Enabled = false;
      this.reinforceButton.Enabled = false;
      this.scoutButton.Enabled = false;
      this.monkButton.Enabled = false;
      this.tradeButton.Enabled = false;
    }

    private void sendResources()
    {
      if (this.m_selectedVillage < 0)
        return;
      InterfaceMgr.Instance.openTradeMode(this.m_selectedVillage, false);
    }

    private void sendTroops()
    {
      if (this.m_selectedVillage < 0)
        return;
      GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.OwnSelectedVillage, this.m_selectedVillage);
    }

    private void sendScouts()
    {
      if (this.m_selectedVillage < 0)
        return;
      InterfaceMgr.Instance.openScoutPopupWindow(this.m_selectedVillage, true);
    }

    private void sendMonks()
    {
      if (this.m_selectedVillage < 0)
        return;
      InterfaceMgr.Instance.openSendMonkWindow(this.m_selectedVillage);
    }

    private void resetNameClick()
    {
      if (MyMessageBox.Show(SK.Text("Mod_Reset_Default", "Are you sure you want to reset the village name to its default?"), SK.Text("Mod_Confirm", "Confirm"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.ResetName();
    }

    private void ResetName()
    {
      RemoteServices.Instance.VillageResetName(InterfaceMgr.Instance.SelectedVillage);
    }

    private void castleClick()
    {
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

    private void vassalClick()
    {
      if (GameEngine.Instance.World.WorldEnded || this.m_selectedVillage < 0)
        return;
      GameEngine.Instance.SkipVillageTab();
      InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      InterfaceMgr.Instance.setVillageTabSubMode(8);
      InterfaceMgr.Instance.resetVillageReportPanelData();
      InterfaceMgr.Instance.selectVassalTarget(this.m_selectedVillage);
    }

    private void sendReinforcements()
    {
      if (GameEngine.Instance.World.WorldEnded || this.m_selectedVillage < 0)
        return;
      if (!GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
      {
        GameEngine.Instance.SkipVillageTab();
        InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      }
      else
        InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.setVillageTabSubMode(6);
      InterfaceMgr.Instance.getVillageTabBar().changeTabGfxOnly(9);
      InterfaceMgr.Instance.setReinforcementVillage(this.m_selectedVillage);
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
  }
}
