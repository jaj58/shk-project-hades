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
    private CustomSelfDrawPanel.CSDImage attackTypeIcon = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel attackTypeLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pillageLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage[] troopIcons = new CustomSelfDrawPanel.CSDImage[6];
    private CustomSelfDrawPanel.CSDLabel[] troopLabels = new CustomSelfDrawPanel.CSDLabel[6];
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
      this.backGround.stretchBackground();
      this.backGround.Size = new Size(199, 320);
      csdImage.Size = new Size(csdImage.Width, csdImage.Height + 47);
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

      // ~5px below the target village button (ends ~y=177) — halved gap
      this.attackTypeIcon.Position = new Point(8, 182);
      this.attackTypeIcon.Size = new Size(23, 23);   // 18 × 1.3
      this.attackTypeIcon.Visible = false;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.attackTypeIcon);

      this.attackTypeLabel.Position = new Point(35, 183);
      this.attackTypeLabel.Size = new Size(148, 20);
      this.attackTypeLabel.Font = FontManager.GetFont("Arial", 9.5f, FontStyle.Bold);   // 8 × 1.2
      this.attackTypeLabel.Color = ARGBColors.Black;
      this.attackTypeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.attackTypeLabel.Visible = false;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.attackTypeLabel);

      this.pillageLabel.Position = new Point(8, 207);
      this.pillageLabel.Size = new Size(177, 16);
      this.pillageLabel.Font = FontManager.GetFont("Arial", 8.5f, FontStyle.Regular);   // 7 × 1.2
      this.pillageLabel.Color = ARGBColors.Black;
      this.pillageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.pillageLabel.Visible = false;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.pillageLabel);

      // 6 troop types (no scouts), 3 per row × 2 rows
      // Peasants, Archers, Pikemen / Swordsmen, Catapults, Captains
      Image[] unitImages = new Image[]
      {
        (Image) GFXLibrary.barracks_unit_peasant,
        (Image) GFXLibrary.barracks_unit_archer,
        (Image) GFXLibrary.barracks_unit_pikemen,
        (Image) GFXLibrary.barracks_unit_swordsman,
        (Image) GFXLibrary.barracks_unit_catapult,
        (Image) GFXLibrary.barracks_unit_captain,
      };
      const int iconW = 31;   // 28 × 1.1
      const int iconH = 33;   // 30 × 1.1
      int[] colX = new int[] { 4, 65, 126 };   // 3 cols × 61px in ~185px
      int[] rowY = new int[] { 228, 265 };

      for (int i = 0; i < 6; i++)
      {
        int x = colX[i % 3];
        int y = rowY[i / 3];

        this.troopIcons[i] = new CustomSelfDrawPanel.CSDImage();
        this.troopIcons[i].Image = unitImages[i];
        this.troopIcons[i].Size = new Size(iconW, iconH);
        this.troopIcons[i].Position = new Point(x, y);
        this.troopIcons[i].Visible = false;
        csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.troopIcons[i]);

        this.troopLabels[i] = new CustomSelfDrawPanel.CSDLabel();
        this.troopLabels[i].Position = new Point(x + iconW + 2, y + 9);
        this.troopLabels[i].Size = new Size(24, 16);
        this.troopLabels[i].Font = FontManager.GetFont("Arial", 9.5f, FontStyle.Regular);   // 8 × 1.2
        this.troopLabels[i].Color = ARGBColors.Black;
        this.troopLabels[i].Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.troopLabels[i].Visible = false;
        csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.troopLabels[i]);
      }
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
            this.updateAttackInfo();
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
        this.updateAttackInfo();
        this.fetchAttackDetails(armyID);
      }
      else
      {
        InterfaceMgr.Instance.closeArmySelectedPanel();
        this.m_army = (WorldMap.LocalArmyData) null;
        this.hideAttackInfo();
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

    private void updateAttackInfo()
    {
      if (this.m_army == null) { hideAttackInfo(); return; }

      bool isScoutsOnly = this.m_army.isScouts();
      this.attackTypeIcon.Visible = false;
      this.attackTypeLabel.Visible = !isScoutsOnly;

      if (!isScoutsOnly)
      {
        this.attackTypeLabel.Text = GetAttackTypeName(this.m_army.attackType);
        Image icon = GetAttackTypeIcon(this.m_army.attackType);
        if (icon != null)
        {
          this.attackTypeIcon.Image = icon;
          this.attackTypeIcon.Size = new Size(23, 23);
        }
        this.attackTypeIcon.Visible = icon != null;
      }

      int[] counts = new int[]
      {
        this.m_army.numPeasants, this.m_army.numArchers, this.m_army.numPikemen,
        this.m_army.numSwordsmen, this.m_army.numCatapults, this.m_army.numCaptains,
      };
      for (int i = 0; i < 6; i++)
      {
        this.troopIcons[i].Visible = true;
        this.troopLabels[i].Visible = true;
        this.troopLabels[i].Text = counts[i].ToString();
        this.troopIcons[i].Alpha = counts[i] > 0 ? 1.0f : 0.3f;
        this.troopLabels[i].Color = counts[i] > 0 ? ARGBColors.Black : Color.FromArgb(140, 100, 90, 80);
      }
    }

    private void hideAttackInfo()
    {
      this.attackTypeIcon.Visible = false;
      this.attackTypeLabel.Visible = false;
      this.pillageLabel.Visible = false;
      for (int i = 0; i < 6; i++)
      {
        if (this.troopIcons[i] != null) this.troopIcons[i].Visible = false;
        if (this.troopLabels[i] != null) this.troopLabels[i].Visible = false;
      }
    }

    private void fetchAttackDetails(long armyID)
    {
      RemoteServices.Instance.set_RetrieveAttackResult_UserCallBack(
        new RemoteServices.RetrieveAttackResult_UserCallBack(this.attackDetailCallback));
      RemoteServices.Instance.RetrieveAttackResult(armyID, GameEngine.Instance.World.StoredVillageFactionPos);
    }

    private void attackDetailCallback(RetrieveAttackResult_ReturnType returnData)
    {
      RemoteServices.Instance.set_RetrieveAttackResult_UserCallBack(
        new RemoteServices.RetrieveAttackResult_UserCallBack(
          GameEngine.Instance.World.retrieveAttackResultCallback));
      GameEngine.Instance.World.retrieveAttackResultCallback(returnData);

      if (!returnData.Success || returnData.armyData == null) return;
      if (returnData.armyData.armyID != this.selectedArmyID) return;

      int[] serverCounts = new int[]
      {
        returnData.armyData.numPeasants, returnData.armyData.numArchers,
        returnData.armyData.numPikemen,  returnData.armyData.numSwordsmen,
        returnData.armyData.numCatapults, returnData.armyData.numCaptains,
      };
      for (int i = 0; i < 6; i++)
      {
        this.troopIcons[i].Visible = true;
        this.troopLabels[i].Visible = true;
        this.troopLabels[i].Text = serverCounts[i].ToString();
        this.troopIcons[i].Alpha = serverCounts[i] > 0 ? 1.0f : 0.3f;
        this.troopLabels[i].Color = serverCounts[i] > 0 ? ARGBColors.Black : Color.FromArgb(140, 100, 90, 80);
      }

      int pct = returnData.armyData.pillagePercent;
      if (pct > 0 && this.m_army != null && !this.m_army.isScouts())
      {
        this.pillageLabel.Text = pct + "%";
        this.pillageLabel.Visible = true;
      }

      this.Invalidate();
    }

    private static string GetAttackTypeName(int attackType)
    {
      switch (attackType)
      {
        case 1:  return SK.Text("AttackType_Capture",          "Capture");
        case 2:  return SK.Text("AttackType_PillageStockpile", "Pillage Stockpile");
        case 3:  return SK.Text("AttackType_Ransack",          "Ransack");
        case 4:  return SK.Text("AttackType_PillageGranary",   "Pillage Granary");
        case 5:  return SK.Text("AttackType_PillageBanquet",   "Pillage Banquet");
        case 6:  return SK.Text("AttackType_PillageAle",       "Pillage Ale");
        case 7:  return SK.Text("AttackType_PillageArmoury",   "Pillage Armoury");
        case 9:  return SK.Text("AttackType_Raze",             "Raze");
        case 11: return SK.Text("AttackType_Vandalise",        "Vandalise");
        case 12: return SK.Text("AttackType_GoldRaid",         "Gold Raid");
        case 17: return SK.Text("GENERIC_Invasion",            "Invasion");
        case 30: return SK.Text("AttackType_VassalSupport",    "Vassal Support");
        case 31: return SK.Text("AttackType_CapitalSupport",   "Capital Support");
        default: return "";
      }
    }

    private static Image GetAttackTypeIcon(int attackType)
    {
      switch (attackType)
      {
        case 1:                         return (Image) GFXLibrary.send_army_buttons[3];
        case 2: case 4: case 5:
        case 6: case 7:                 return (Image) GFXLibrary.send_army_buttons[5];
        case 3:                         return (Image) GFXLibrary.send_army_buttons[2];
        case 9:                         return (Image) GFXLibrary.send_army_buttons[4];
        case 11:                        return (Image) GFXLibrary.send_army_buttons[1];
        case 12:                        return (Image) GFXLibrary.send_army_buttons[0];
        default:                        return null;
      }
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
      this.Size = new Size(199, 320);
      this.ResumeLayout(false);
    }
  }
}
