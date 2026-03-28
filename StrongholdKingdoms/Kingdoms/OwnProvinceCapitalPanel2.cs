// Decompiled with JetBrains decompiler
// Type: Kingdoms.OwnProvinceCapitalPanel2
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
  public class OwnProvinceCapitalPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton resourcesButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton troopsButton = new CustomSelfDrawPanel.CSDButton();
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
      this.Name = nameof (OwnProvinceCapitalPanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }

    public OwnProvinceCapitalPanel2()
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
      this.tradeButton.Position = new Point(80, 142);
      this.tradeButton.CustomTooltipID = 2441;
      this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tradeClick), "OwnProvinceCapitalPanel2_trade");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.tradeButton);
      this.villageButton.ImageNorm = (Image) GFXLibrary.int_world_icon_village;
      this.villageButton.OverBrighten = true;
      this.villageButton.MoveOnClick = true;
      this.villageButton.Position = new Point(29, 112);
      this.villageButton.CustomTooltipID = 2437;
      this.villageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClick), "OwnProvinceCapitalPanel2_view_village");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageButton);
      this.castleButton.ImageNorm = (Image) GFXLibrary.int_world_icon_castle;
      this.castleButton.OverBrighten = true;
      this.castleButton.MoveOnClick = true;
      this.castleButton.Position = new Point(64, 112);
      this.castleButton.CustomTooltipID = 2438;
      this.castleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "OwnProvinceCapitalPanel2_view_castle");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleButton);
      this.resourcesButton.ImageNorm = (Image) GFXLibrary.int_world_icon_resource;
      this.resourcesButton.OverBrighten = true;
      this.resourcesButton.MoveOnClick = true;
      this.resourcesButton.Position = new Point(99, 112);
      this.resourcesButton.CustomTooltipID = 2439;
      this.resourcesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourcesClick), "OwnProvinceCapitalPanel2_view_resources");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.resourcesButton);
      this.troopsButton.ImageNorm = (Image) GFXLibrary.int_world_icon_troops;
      this.troopsButton.OverBrighten = true;
      this.troopsButton.MoveOnClick = true;
      this.troopsButton.Position = new Point(134, 112);
      this.troopsButton.CustomTooltipID = 2442;
      this.troopsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopsClick), "OwnProvinceCapitalPanel2_make_troops");
      this.backImage.addControl((CustomSelfDrawPanel.CSDControl) this.troopsButton);
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
      this.tradeButton.Position = new Point(80, 142 + num1);
      this.villageButton.Position = new Point(29, 112 + num1 + num2);
      this.castleButton.Position = new Point(64, 112 + num1 + num2);
      this.resourcesButton.Position = new Point(99, 112 + num1 + num2);
      this.troopsButton.Position = new Point(134, 112 + num1 + num2);
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

    public void updateOwnVillageText(int selectedVillage)
    {
      this.m_selectedVillage = selectedVillage;
      this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(selectedVillage));
      this.backGround.updatePanelTypeFromVillageID(selectedVillage);
      this.backGround.setActionFromVillage(selectedVillage, -1);
      this.updateSize();
      this.update();
    }

    private void villageClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.getVillageTabBar().changeTab(0);
    }

    private void castleClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.getVillageTabBar().changeTab(1);
    }

    private void resourcesClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.setVillageTabSubMode(1005);
    }

    private void troopsClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.setVillageTabSubMode(1004);
    }

    private void tradeClick()
    {
      InterfaceMgr.Instance.getMainTabBar().changeTab(2);
      InterfaceMgr.Instance.getVillageTabBar().changeTab(3);
    }
  }
}
