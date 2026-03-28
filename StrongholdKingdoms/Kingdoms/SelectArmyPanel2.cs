// Decompiled with JetBrains decompiler
// Type: Kingdoms.SelectArmyPanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class SelectArmyPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton homeVillageButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton targetVillageButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage travelDirection = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton returnButton = new CustomSelfDrawPanel.CSDButton();
    private bool forceReturnOff;
    private int fromVillageID = -1;
    private int toVillageID = -1;
    private int lastState = -2;
    private WorldMap.LocalArmyData m_army;
    private long selectedArmyID;
    private DockableControl dockableControl;
    private IContainer components;

    public SelectArmyPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.clearControls();
      CustomSelfDrawPanel.CSDImage csdImage = this.backGround.init(true, 1000);
      this.backGround.centerSubHeading();
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.backGround.initTravelButton(this.homeVillageButton);
      this.homeVillageButton.Position = new Point(11, 61);
      this.homeVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.homeClick), "SelectArmyPanel2_home_village");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.homeVillageButton);
      this.backGround.initTravelButton(this.targetVillageButton);
      this.targetVillageButton.Position = new Point(11, 119);
      this.targetVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.targetClick), "SelectArmyPanel2_target_village");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.targetVillageButton);
      this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
      this.travelDirection.Position = new Point(88, 90);
      this.travelDirection.Alpha = 0.5f;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.travelDirection);
      this.returnButton.ImageNorm = (Image) GFXLibrary.mrhp_button_150x25[0];
      this.returnButton.ImageOver = (Image) GFXLibrary.mrhp_button_150x25[1];
      this.returnButton.ImageClick = (Image) GFXLibrary.mrhp_button_150x25[2];
      this.returnButton.Position = new Point(26, 155);
      this.returnButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.returnButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.returnButton.TextYOffset = -3;
      this.returnButton.Text.Color = ARGBColors.Black;
      this.returnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick), "SelectArmyPanel2_cancel");
      this.returnButton.Visible = false;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.returnButton);
      this.forceReturnOff = false;
    }

    public void update()
    {
      this.backGround.update();
      this.m_army = GameEngine.Instance.World.getArmy(this.selectedArmyID);
      if (this.m_army != null)
      {
        if (this.m_army.dead)
        {
          this.m_army = (WorldMap.LocalArmyData) null;
          InterfaceMgr.Instance.closeArmySelectedPanel();
        }
        else
        {
          double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
          double secsLeft = this.m_army.localEndTime - num;
          if (secsLeft < 0.0)
            secsLeft = 0.0;
          string buildTimeString = VillageMap.createBuildTimeString((int) secsLeft);
          if (!GameEngine.Instance.World.isUserVillage(this.m_army.homeVillageID) || this.m_army.localStartTime + (double) (GameEngine.Instance.LocalWorldData.AttackCancelDuration * 60) < num && !this.m_army.isScouts() && !this.targetIsAI(this.m_army.targetVillageID) || this.m_army.lootType >= 0 || this.forceReturnOff)
            this.returnButton.Visible = false;
          else
            this.returnButton.Visible = true;
          if (this.m_army.lootType != this.lastState)
          {
            bool flag1 = false;
            if (this.m_army.attackType == 30 || this.m_army.attackType == 31)
            {
              this.backGround.updateHeading(SK.Text("SelectArmyPanel_Troops", "Troops"));
              this.backGround.updatePanelType(1000);
            }
            else if (this.m_army.attackType == 17)
            {
              this.backGround.updateHeading(SK.Text("GENERIC_Invasion", "Invasion"));
              this.backGround.updatePanelType(1000);
            }
            else if (!this.m_army.isScouts())
            {
              this.backGround.updateHeading(SK.Text("SelectArmyPanel_Army", "Army"));
              this.backGround.updatePanelType(1000);
            }
            else
            {
              flag1 = true;
              this.backGround.updatePanelType(1002);
              this.backGround.updateHeading(SK.Text("SelectArmyPanel_Scouts", "Scouts"));
            }
            this.lastState = this.m_army.lootType;
            if (this.lastState < 0)
            {
              this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
              if (this.m_army.attackType == 30 || this.m_army.attackType == 31)
                this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Stationing", "Stationing"));
              else if (!flag1)
              {
                if (GameEngine.Instance.LocalWorldData.AIWorld && this.m_army.attackType == 17)
                {
                  bool flag2 = false;
                  int special1 = GameEngine.Instance.World.getVillageData(this.m_army.travelFromVillageID).special;
                  int special2 = GameEngine.Instance.World.getVillageData(this.m_army.targetVillageID).special;
                  if (special1 == 30)
                  {
                    switch (special2)
                    {
                      case 7:
                      case 8:
                      case 9:
                      case 10:
                      case 11:
                      case 12:
                      case 13:
                      case 14:
                        this.backGround.updatePanelText(SK.Text("BARRACKS_Reinforcing", "Reinforcing"));
                        flag2 = true;
                        break;
                    }
                  }
                  if (!flag2)
                    this.backGround.updatePanelText(SK.Text("GENERIC_Attacking", "Attacking"));
                }
                else
                  this.backGround.updatePanelText(SK.Text("GENERIC_Attacking", "Attacking"));
              }
              else
                this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Scouting", "Scouting"));
              if (this.m_army.attackType != 13)
                this.backGround.updateTravelButton(this.homeVillageButton, this.m_army.travelFromVillageID);
              else
                this.backGround.updateTravelButton(this.homeVillageButton, SK.Text("SelectArmyPanel_Tutorial", "Tutorial"));
              this.backGround.updateTravelButton(this.targetVillageButton, this.m_army.targetVillageID);
              if (flag1 && this.homeVillageButton.Text.Text.Length == 0)
                this.backGround.updateTravelButton(this.targetVillageButton, SK.Text("GENERIC_Unknown", "Unknown"));
              this.fromVillageID = this.m_army.travelFromVillageID;
              this.toVillageID = this.m_army.targetVillageID;
            }
            else
            {
              this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[1];
              this.backGround.updatePanelText(SK.Text("SelectArmyPanel_Returning", "Returning"));
              if (this.m_army.attackType != 13)
                this.backGround.updateTravelButton(this.homeVillageButton, this.m_army.travelFromVillageID);
              else
                this.backGround.updateTravelButton(this.homeVillageButton, SK.Text("SelectArmyPanel_Tutorial", "Tutorial"));
              this.backGround.updateTravelButton(this.targetVillageButton, this.m_army.targetVillageID);
              if (flag1 && this.homeVillageButton.Text.Text.Length == 0)
                this.backGround.updateTravelButton(this.targetVillageButton, SK.Text("GENERIC_Unknown", "Unknown"));
              this.fromVillageID = this.m_army.travelFromVillageID;
              this.toVillageID = this.m_army.targetVillageID;
              this.returnButton.Visible = false;
            }
          }
          this.backGround.updateSubHeading(buildTimeString);
        }
      }
      else
        InterfaceMgr.Instance.closeArmySelectedPanel();
    }

    private bool targetIsAI(int villageID) => GameEngine.Instance.World.getSpecial(villageID) != 0;

    public void armySelected(long armyID)
    {
      WorldMap.LocalArmyData army = GameEngine.Instance.World.getArmy(armyID);
      if (army != null)
      {
        this.selectedArmyID = armyID;
        this.m_army = army;
        this.lastState = -2;
        this.update();
      }
      else
      {
        InterfaceMgr.Instance.closeArmySelectedPanel();
        this.m_army = (WorldMap.LocalArmyData) null;
      }
    }

    private void homeClick()
    {
      if (this.m_army == null || this.fromVillageID < 0)
        return;
      GameEngine.Instance.World.zoomToVillage(this.fromVillageID);
    }

    private void targetClick()
    {
      if (this.m_army == null || this.toVillageID < 0)
        return;
      GameEngine.Instance.World.zoomToVillage(this.toVillageID);
    }

    private void cancelClick()
    {
      if (GameEngine.Instance.World.WorldEnded || this.m_army == null)
        return;
      this.returnButton.Visible = false;
      this.forceReturnOff = true;
      RemoteServices.Instance.set_CancelCastleAttack_UserCallBack(new RemoteServices.CancelCastleAttack_UserCallBack(this.cancelCastleAttackCallBack));
      RemoteServices.Instance.CancelCastleAttack(this.m_army.armyID);
    }

    private void cancelCastleAttackCallBack(CancelCastleAttack_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.armyData != null)
        {
          ArmyReturnData[] armyReturnData = new ArmyReturnData[1]
          {
            returnData.armyData
          };
          GameEngine.Instance.World.doGetArmyData((IEnumerable<ArmyReturnData>) armyReturnData, (IEnumerable<ArmyReturnData>) null, false);
          GameEngine.Instance.World.addExistingArmy(returnData.armyData.armyID);
          GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
          GameEngine.Instance.World.deleteArmy(returnData.oldArmyID);
          if (SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(returnData.armyData.targetVillageID)))
            GameEngine.Instance.World.setLastTreasureCastleAttackTime(DateTime.MinValue);
        }
        this.update();
        this.returnButton.Visible = false;
      }
      else
        this.forceReturnOff = false;
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {
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
      this.Name = nameof (SelectArmyPanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }
  }
}
