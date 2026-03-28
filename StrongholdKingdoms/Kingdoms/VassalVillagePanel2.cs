// Decompiled with JetBrains decompiler
// Type: Kingdoms.VassalVillagePanel2
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
  public class VassalVillagePanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDArea drawArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton reinforceButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton vassalButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel lblProtected = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblProtectionType = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton leftButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton rightButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage backImage;
    private bool attackMode;
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
      this.Name = nameof (VassalVillagePanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }

    public VassalVillagePanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.attackMode = false;
      this.clearControls();
      this.backImage = this.backGround.init(true, 10000);
      this.drawArea.Size = this.backImage.Size;
      this.drawArea.Position = new Point(0, 0);
      this.drawArea.Visible = true;
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.drawArea);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.tradeButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.TRADE);
      this.tradeButton.Position = new Point(29, 142);
      this.tradeButton.CustomTooltipID = 2410;
      this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendResources), "VassalVillagePanel2_trade");
      this.drawArea.addControl((CustomSelfDrawPanel.CSDControl) this.tradeButton);
      this.attackButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.ATTACK);
      this.attackButton.Position = new Point(64, 142);
      this.attackButton.CustomTooltipID = 2453;
      this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendTroops), "VassalVillagePanel2_attack_from");
      this.drawArea.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton);
      this.reinforceButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.REINFORCE);
      this.reinforceButton.Position = new Point(99, 142);
      this.reinforceButton.CustomTooltipID = 2451;
      this.reinforceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendReinforcements), "VassalVillagePanel2_manage_troops");
      this.drawArea.addControl((CustomSelfDrawPanel.CSDControl) this.reinforceButton);
      this.vassalButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.VASSAL);
      this.vassalButton.Position = new Point(134, 142);
      this.vassalButton.CustomTooltipID = 2452;
      this.vassalButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.vassalClick), "VassalVillagePanel2_manage_vassals");
      this.drawArea.addControl((CustomSelfDrawPanel.CSDControl) this.vassalButton);
      this.castleButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.LAST_REPORT);
      this.castleButton.Position = new Point(80, 112);
      this.castleButton.CustomTooltipID = 2445;
      this.castleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "VassalVillagePanel2_view_castle_report");
      this.drawArea.addControl((CustomSelfDrawPanel.CSDControl) this.castleButton);
      this.lblProtectionType.Text = "";
      this.lblProtectionType.Color = ARGBColors.Black;
      this.lblProtectionType.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.lblProtectionType.Position = new Point(0, 38);
      this.lblProtectionType.Size = new Size(this.backImage.Width, 23);
      this.lblProtectionType.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.drawArea.addControl((CustomSelfDrawPanel.CSDControl) this.lblProtectionType);
      this.lblProtected.Text = "";
      this.lblProtected.Color = ARGBColors.Black;
      this.lblProtected.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.lblProtected.Position = new Point(6, 48);
      this.lblProtected.Size = new Size(this.backImage.Width - 12, 74);
      this.lblProtected.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.drawArea.addControl((CustomSelfDrawPanel.CSDControl) this.lblProtected);
      this.leftButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_left_norm;
      this.leftButton.ImageOver = (Image) GFXLibrary.r_arrow_small_left_over;
      this.leftButton.Position = new Point(5, 50);
      this.leftButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoLeft), "VassalVillagePanel2_protection_left");
      this.leftButton.Visible = false;
      this.drawArea.addControl((CustomSelfDrawPanel.CSDControl) this.leftButton);
      this.rightButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_right_norm;
      this.rightButton.ImageOver = (Image) GFXLibrary.r_arrow_small_right_over;
      this.rightButton.Position = new Point(170, 50);
      this.rightButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoRight), "VassalVillagePanel2_protection_right");
      this.rightButton.Visible = false;
      this.drawArea.addControl((CustomSelfDrawPanel.CSDControl) this.rightButton);
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
      this.tradeButton.Position = new Point(29, 142 + num1);
      this.attackButton.Position = new Point(64, 142 + num1);
      this.reinforceButton.Position = new Point(99, 142 + num1);
      this.vassalButton.Position = new Point(134, 142 + num1);
      this.castleButton.Position = new Point(80, 112 + num1 + num2);
      this.backGround.invalidate();
    }

    public void update()
    {
      this.backGround.update();
      if (this.attackMode && InterfaceMgr.Instance.SelectedVassalVillage < 0)
      {
        this.backImage.Size = this.backImage.Image.Size;
        this.drawArea.Visible = true;
        this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(this.m_selectedVillage));
      }
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
          string buildTimeString1 = VillageMap.createBuildTimeString((int) timeSpan1.TotalSeconds);
          this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + buildTimeString1;
          this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Interdict", "Interdict");
          this.lblProtectionType.Visible = true;
          break;
        case 2:
          string buildTimeString2 = VillageMap.createBuildTimeString((int) timeSpan1.TotalSeconds);
          this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + buildTimeString2;
          this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Peace", "Peace");
          this.lblProtectionType.Visible = true;
          break;
        case 3:
          this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked", "Cannot be attacked");
          this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Vacation", "Vacation Mode");
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

    public void updateVassalVillageText(int selectedVillage)
    {
      this.attackMode = false;
      this.m_selectedVillage = selectedVillage;
      this.backGround.updateHeading(GameEngine.Instance.World.getVillageNameOrType(selectedVillage));
      this.backGround.updatePanelTypeFromVillageID(selectedVillage);
      this.backGround.setActionFromVillage(InterfaceMgr.Instance.getSelectedMenuVillage(), selectedVillage);
      this.updateSize();
      this.update();
    }

    private void sendResources()
    {
      if (this.m_selectedVillage < 0)
        return;
      InterfaceMgr.Instance.openTradeMode(this.m_selectedVillage, false);
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

    private void sendTroops()
    {
      InterfaceMgr.Instance.setVassalAttackMode(this.m_selectedVillage);
      this.attackMode = true;
      this.backImage.Size = new Size(1, 1);
      this.drawArea.Visible = false;
      this.backGround.updateHeading(SK.Text("VassalVillagePanel_Attack_From_Here", "Attack From Here"));
    }

    private void vassalClick()
    {
      GameEngine.Instance.SkipVillageTab();
      InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      InterfaceMgr.Instance.setVillageTabSubMode(8);
    }

    private void sendReinforcements()
    {
      GameEngine.Instance.SkipVillageTab();
      InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      InterfaceMgr.Instance.setVassalArmiesVillage(this.m_selectedVillage);
      InterfaceMgr.Instance.setVillageTabSubMode(15);
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
