// Decompiled with JetBrains decompiler
// Type: Kingdoms.SelectReinforcementPanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class SelectReinforcementPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton homeVillageButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton targetVillageButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage travelDirection = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton returnButton = new CustomSelfDrawPanel.CSDButton();
    private int fromVillageID = -1;
    private int toVillageID = -1;
    private int lastState = -2;
    private WorldMap.LocalArmyData m_reinforcements;
    private long selectedReinforcementID;

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
      this.Name = nameof (SelectReinforcementPanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }

    public SelectReinforcementPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.clearControls();
      CustomSelfDrawPanel.CSDImage csdImage = this.backGround.init(true, 1003);
      this.backGround.updateHeading(SK.Text("SelectArmyPanel_Reinforcements", "Reinforcements"));
      this.backGround.centerSubHeading();
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.backGround.initTravelButton(this.homeVillageButton);
      this.homeVillageButton.Position = new Point(11, 61);
      this.homeVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.homeClick), "SelectReinforcementPanel2_home_village");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.homeVillageButton);
      this.backGround.initTravelButton(this.targetVillageButton);
      this.targetVillageButton.Position = new Point(11, 119);
      this.targetVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.targetClick), "SelectReinforcementPanel2_target_village");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.targetVillageButton);
      this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
      this.travelDirection.Alpha = 0.5f;
      this.travelDirection.Position = new Point(88, 90);
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.travelDirection);
      this.returnButton.ImageNorm = (Image) GFXLibrary.mrhp_button_150x25[0];
      this.returnButton.ImageOver = (Image) GFXLibrary.mrhp_button_150x25[1];
      this.returnButton.ImageClick = (Image) GFXLibrary.mrhp_button_150x25[2];
      this.returnButton.Position = new Point(26, 155);
      this.returnButton.Text.Text = "";
      this.returnButton.TextYOffset = -3;
      this.returnButton.Text.Color = ARGBColors.Black;
      this.returnButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.returnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.returnClick), "SelectReinforcementPanel2_return");
      this.returnButton.Visible = false;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.returnButton);
    }

    public void update()
    {
      this.backGround.update();
      this.m_reinforcements = GameEngine.Instance.World.getReinforcement(this.selectedReinforcementID);
      if (this.m_reinforcements != null)
      {
        if (this.m_reinforcements.dead)
        {
          this.m_reinforcements = (WorldMap.LocalArmyData) null;
          InterfaceMgr.Instance.closeReinforcementSelectedPanel();
        }
        else
        {
          if (this.m_reinforcements.attackType != this.lastState)
          {
            this.backGround.updateTravelButton(this.homeVillageButton, this.m_reinforcements.homeVillageID);
            this.backGround.updateTravelButton(this.targetVillageButton, this.m_reinforcements.targetVillageID);
            this.fromVillageID = this.m_reinforcements.homeVillageID;
            this.toVillageID = this.m_reinforcements.targetVillageID;
            this.lastState = this.m_reinforcements.attackType;
            if (this.lastState == 20)
            {
              this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
              if (GameEngine.Instance.World.isUserVillage(this.m_reinforcements.homeVillageID))
                this.returnButton.Visible = true;
              else
                this.returnButton.Visible = false;
              this.returnButton.Text.TextDiffOnly = !GameEngine.Instance.World.isUserVillage(this.m_reinforcements.homeVillageID) ? SK.Text("SelectArmyPanel_Return", "Return") : SK.Text("SelectArmyPanel_Retrieve", "Retrieve");
            }
            else
            {
              this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[1];
              this.returnButton.Visible = false;
            }
          }
          double secsLeft = this.m_reinforcements.localEndTime - DXTimer.GetCurrentMilliseconds() / 1000.0;
          if (secsLeft < 0.0)
            this.backGround.updateSubHeading("");
          else
            this.backGround.updateSubHeading(VillageMap.createBuildTimeString((int) secsLeft));
        }
      }
      else
        InterfaceMgr.Instance.closeReinforcementSelectedPanel();
    }

    public void reinforcementSelected(long reinforcementID)
    {
      WorldMap.LocalArmyData reinforcement = GameEngine.Instance.World.getReinforcement(reinforcementID);
      if (reinforcement != null)
      {
        this.selectedReinforcementID = reinforcementID;
        this.m_reinforcements = reinforcement;
        this.lastState = -2;
        this.update();
      }
      else
      {
        InterfaceMgr.Instance.closeReinforcementSelectedPanel();
        this.m_reinforcements = (WorldMap.LocalArmyData) null;
      }
    }

    private void homeClick()
    {
      if (this.m_reinforcements == null || this.fromVillageID < 0)
        return;
      GameEngine.Instance.World.zoomToVillage(this.fromVillageID);
    }

    private void targetClick()
    {
      if (this.m_reinforcements == null || this.toVillageID < 0)
        return;
      GameEngine.Instance.World.zoomToVillage(this.toVillageID);
    }

    private void returnClick()
    {
      if (GameEngine.Instance.World.WorldEnded || this.m_reinforcements == null)
        return;
      RemoteServices.Instance.set_ReturnReinforcements_UserCallBack(new RemoteServices.ReturnReinforcements_UserCallBack(this.returnReinforcementsCallBack));
      RemoteServices.Instance.ReturnReinforcements(this.m_reinforcements.armyID);
      this.returnButton.Visible = false;
    }

    private void returnReinforcementsCallBack(ReturnReinforcements_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.armyData != null)
        GameEngine.Instance.World.addReinforcementArmy(returnData.armyData);
      if (returnData.armyData2 != null)
        GameEngine.Instance.World.addReinforcementArmy(returnData.armyData2);
      this.update();
      this.returnButton.Visible = false;
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {
    }
  }
}
