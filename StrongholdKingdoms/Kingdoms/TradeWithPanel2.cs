// Decompiled with JetBrains decompiler
// Type: Kingdoms.TradeWithPanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class TradeWithPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton okButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private int selectedVillage = -1;

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
      this.Name = nameof (TradeWithPanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }

    public TradeWithPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.clearControls();
      CustomSelfDrawPanel.CSDImage csdImage = this.backGround.init(false, 10000);
      this.backGround.updatePanelText(SK.Text("TradeWithPanel_Select_Trade_Village", "Select Trade Village"));
      this.backGround.setAction(1004);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.okButton.ImageNorm = (Image) GFXLibrary.mrhp_button_check_normal;
      this.okButton.ImageOver = (Image) GFXLibrary.mrhp_button_check_over;
      this.okButton.ImageClick = (Image) GFXLibrary.mrhp_button_check_pushed;
      this.okButton.Position = new Point(102, 64);
      this.okButton.Enabled = false;
      this.okButton.CustomTooltipID = 2403;
      this.okButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectTargetClick), "TradeWithPanel2_ok");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.okButton);
      this.cancelButton.ImageNorm = (Image) GFXLibrary.mrhp_button_x_normal;
      this.cancelButton.ImageOver = (Image) GFXLibrary.mrhp_button_x_over;
      this.cancelButton.ImageClick = (Image) GFXLibrary.mrhp_button_x_pushed;
      this.cancelButton.Position = new Point(20, 64);
      this.cancelButton.CustomTooltipID = 2400;
      this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick), "TradeWithPanel2_cancel");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.cancelButton);
    }

    public void update() => this.backGround.update();

    public void setTradeWithVillage(int villageID)
    {
      this.selectedVillage = villageID;
      if (villageID < 0 || !GameEngine.Instance.World.isVillageVisible(villageID))
      {
        this.backGround.updateHeading("");
        this.backGround.updatePanelType(10000);
        this.okButton.Enabled = false;
      }
      else
      {
        this.backGround.updateHeading(GameEngine.Instance.World.getVillageNameOrType(villageID));
        this.backGround.updatePanelTypeFromVillageID(villageID);
        this.okButton.Enabled = true;
      }
    }

    private void cancelClick()
    {
      if (MainTabBar2.LastDummyMode >= 0)
        InterfaceMgr.Instance.openTradeMode(-1, true);
      else
        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
    }

    private void selectTargetClick()
    {
      GameEngine.Instance.SkipVillageTab();
      InterfaceMgr.Instance.getMainTabBar().changeTab(1);
      InterfaceMgr.Instance.tradeWithResume(-1, true);
      InterfaceMgr.Instance.setVillageTabSubMode(2);
      InterfaceMgr.Instance.tradeWithResume(this.selectedVillage, true);
    }
  }
}
