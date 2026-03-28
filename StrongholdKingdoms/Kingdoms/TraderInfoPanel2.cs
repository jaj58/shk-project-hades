// Decompiled with JetBrains decompiler
// Type: Kingdoms.TraderInfoPanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class TraderInfoPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton homeVillageButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton targetVillageButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage travelDirection = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage resourceImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel resourceAmountLabel = new CustomSelfDrawPanel.CSDLabel();
    private WorldMap.LocalTrader m_trader;
    private int lastState = -1;

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
      this.Name = nameof (TraderInfoPanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }

    public TraderInfoPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.clearControls();
      CustomSelfDrawPanel.CSDImage csdImage = this.backGround.init(true, 1004);
      this.backGround.updateHeading(SK.Text("SelectArmyPanel_Trader", "Trader"));
      this.backGround.centerSubHeading();
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.backGround.initTravelButton(this.homeVillageButton);
      this.homeVillageButton.Position = new Point(11, 61);
      this.homeVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.homeClick), "TraderInfoPanel2_home_village");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.homeVillageButton);
      this.backGround.initTravelButton(this.targetVillageButton);
      this.targetVillageButton.Position = new Point(11, 119);
      this.targetVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.targetClick), "TraderInfoPanel2_target_village");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.targetVillageButton);
      this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
      this.travelDirection.Position = new Point(88, 90);
      this.travelDirection.Alpha = 0.5f;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.travelDirection);
      this.resourceImage.Image = GFXLibrary.dummy;
      this.resourceImage.Position = new Point(45, 144);
      this.resourceImage.Visible = false;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.resourceImage);
      this.resourceAmountLabel.Text = "";
      this.resourceAmountLabel.Color = ARGBColors.Black;
      this.resourceAmountLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.resourceAmountLabel.Position = new Point(90, 158);
      this.resourceAmountLabel.Size = new Size(168, 23);
      this.resourceAmountLabel.Visible = false;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.resourceAmountLabel);
    }

    public void setTrader(long traderID)
    {
      WorldMap.LocalTrader trader = GameEngine.Instance.World.getTrader(traderID);
      if (trader != null)
      {
        this.m_trader = trader;
        this.lastState = -1;
        this.update();
      }
      else
        InterfaceMgr.Instance.closeTraderInfoPanel();
    }

    public void update()
    {
      this.backGround.update();
      if (this.m_trader == null)
        return;
      if (this.m_trader.trader.traderState != this.lastState)
      {
        this.lastState = this.m_trader.trader.traderState;
        this.backGround.updateTravelButton(this.homeVillageButton, this.m_trader.trader.homeVillageID);
        this.backGround.updateTravelButton(this.targetVillageButton, this.m_trader.trader.targetVillageID);
        this.resourceImage.Visible = false;
        this.resourceAmountLabel.Visible = false;
        if (this.lastState == 0)
        {
          InterfaceMgr.Instance.closeTraderInfoPanel();
          return;
        }
        if (this.lastState == 1 || this.lastState == 3 || this.lastState == 6)
        {
          this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Trading", "Trading"));
          if (GameEngine.Instance.World.isUserVillage(this.m_trader.trader.homeVillageID))
          {
            this.resourceImage.Image = (Image) GFXLibrary.getCommodity32DSImage(this.m_trader.trader.resource);
            this.resourceImage.Visible = true;
            NumberFormatInfo nfi = GameEngine.NFI;
            this.resourceAmountLabel.TextDiffOnly = GameEngine.Instance.World.getTradingAmount(this.m_trader.traderID).ToString("N", (IFormatProvider) nfi);
            this.resourceAmountLabel.Visible = true;
          }
          this.travelDirection.Image = this.lastState != 6 ? (Image) GFXLibrary.mrhp_travelling_arrows[0] : (Image) GFXLibrary.mrhp_travelling_arrows[1];
        }
        else if (this.lastState == 2 || this.lastState == 4)
        {
          this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Returning", "Returning"));
          this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[1];
        }
        else if (this.lastState == 5)
        {
          this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Collecting", "Collecting"));
          this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
        }
      }
      double secsLeft = this.m_trader.localEndTime - DXTimer.GetCurrentMilliseconds() / 1000.0;
      if (secsLeft < 0.0)
        secsLeft = 0.0;
      this.backGround.updateSubHeading(VillageMap.createBuildTimeString((int) secsLeft));
    }

    private void homeClick()
    {
      if (this.m_trader == null)
        return;
      GameEngine.Instance.World.zoomToVillage(this.m_trader.trader.homeVillageID);
    }

    private void targetClick()
    {
      if (this.m_trader == null)
        return;
      GameEngine.Instance.World.zoomToVillage(this.m_trader.trader.targetVillageID);
    }
  }
}
