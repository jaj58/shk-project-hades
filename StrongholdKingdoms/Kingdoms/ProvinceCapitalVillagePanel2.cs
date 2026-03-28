// Decompiled with JetBrains decompiler
// Type: Kingdoms.ProvinceCapitalVillagePanel2
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
  public class ProvinceCapitalVillagePanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton reinforceButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton monkButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel lblProtected = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblProtectionType = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage backImage;
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
      this.Name = nameof (ProvinceCapitalVillagePanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }

    public ProvinceCapitalVillagePanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.clearControls();
      this.backImage = this.backGround.init(false, 1502);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.tradeButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.TRADE);
      this.tradeButton.Position = new Point(10, 49);
      this.tradeButton.Enabled = false;
      this.tradeButton.CustomTooltipID = 2410;
      this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnTradeWith_Click), "ProvinceCapitalVillagePanel2_trade");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.tradeButton);
      this.attackButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.ATTACK);
      this.attackButton.Position = new Point(45, 49);
      this.attackButton.Enabled = false;
      this.attackButton.CustomTooltipID = 2411;
      this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAttack_Click), "ProvinceCapitalVillagePanel2_attack");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton);
      this.scoutButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.SCOUT);
      this.scoutButton.Position = new Point(80, 49);
      this.scoutButton.Enabled = false;
      this.scoutButton.CustomTooltipID = 2412;
      this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnScout_Click), "ProvinceCapitalVillagePanel2_scout");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.scoutButton);
      this.reinforceButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.REINFORCE);
      this.reinforceButton.Position = new Point(115, 49);
      this.reinforceButton.Enabled = false;
      this.reinforceButton.CustomTooltipID = 2413;
      this.reinforceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnSendTroops_Click), "ProvinceCapitalVillagePanel2_reinforce");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.reinforceButton);
      this.monkButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.MONK);
      this.monkButton.Position = new Point(150, 49);
      this.monkButton.Enabled = false;
      this.monkButton.CustomTooltipID = 2414;
      this.monkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnSendMonks_Click), "ProvinceCapitalVillagePanel2_send_monks");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.monkButton);
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
    }

    private void updateSize()
    {
      bool visible = this.lblProtectionType.Visible;
      int num = 0;
      if (!visible)
      {
        this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_102;
        num = -95;
      }
      else
        this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_192;
      this.tradeButton.Position = new Point(10, 142 + num);
      this.attackButton.Position = new Point(45, 142 + num);
      this.scoutButton.Position = new Point(80, 142 + num);
      this.reinforceButton.Position = new Point(115, 142 + num);
      this.monkButton.Position = new Point(150, 142 + num);
      this.backGround.invalidate();
    }

    public void update()
    {
      this.backGround.update();
      bool visible = this.lblProtectionType.Visible;
      int num = 0;
      TimeSpan timeSpan = new TimeSpan();
      if (GameEngine.Instance.World.isVillageInterdictProtected(this.m_selectedVillage))
      {
        timeSpan = GameEngine.Instance.World.getInterdictTime(this.m_selectedVillage) - VillageMap.getCurrentServerTime();
        num = 1;
      }
      if (num == 1)
      {
        string buildTimeStringFull = VillageMap.createBuildTimeStringFull((int) timeSpan.TotalSeconds);
        this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + buildTimeStringFull;
        this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Interdict", "Interdict");
        this.lblProtectionType.Visible = true;
      }
      else
      {
        this.lblProtected.TextDiffOnly = "";
        this.lblProtectionType.TextDiffOnly = "";
        this.lblProtectionType.Visible = false;
      }
      if (visible == this.lblProtectionType.Visible)
        return;
      this.updateSize();
    }

    public void updateProvinceCapitalVillageText(int selectedVillage, int ownVillage)
    {
      this.m_selectedVillage = selectedVillage;
      this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(selectedVillage));
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
        bool flag = true;
        if (GameEngine.Instance.World.isCapital(ownVillage))
        {
          this.scoutButton.Enabled = false;
          flag = false;
          this.monkButton.Enabled = false;
          this.reinforceButton.Enabled = false;
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
      this.updateSize();
      this.update();
    }

    private void btnTradeWith_Click()
    {
      if (this.m_selectedVillage < 0)
        return;
      InterfaceMgr.Instance.selectStockExchange(-1);
      if (!GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
      {
        GameEngine.Instance.SkipVillageTab();
        InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      }
      else
        InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.setVillageTabSubMode(3);
      InterfaceMgr.Instance.resetVillageReportPanelData();
      InterfaceMgr.Instance.selectStockExchange(this.m_selectedVillage);
    }

    private void btnSendCourtiers_Click(object sender, EventArgs e)
    {
    }

    private void btnAttack_Click()
    {
      GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.OwnSelectedVillage, this.m_selectedVillage);
    }

    private void btnSendTroops_Click()
    {
      if (!GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
      {
        GameEngine.Instance.SkipVillageTab();
        InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      }
      else
        InterfaceMgr.Instance.getMainTabBar().changeTab(2);
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
  }
}
