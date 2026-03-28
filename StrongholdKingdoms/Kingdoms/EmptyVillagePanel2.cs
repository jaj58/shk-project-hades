// Decompiled with JetBrains decompiler
// Type: Kingdoms.EmptyVillagePanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class EmptyVillagePanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private CustomSelfDrawPanel.MRHP_Background backGround_AI = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton attackButton_AI = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton scoutButton_AI = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleButton_AI = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel treasureCastleTimeoutLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.MRHP_Background backGround_Enemy = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.MRHP_Background backGround_Resources = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton scoutButton_Resources = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.MRHP_Background backGround_Charter = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDLabel charterLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage goldImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage honourImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel goldLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel honourLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel travelTimeDescLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel travelTimeLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage travelImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage seaConditionsImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel invasionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.WikiLinkControl wikiLink;
    private CustomSelfDrawPanel.CSDButton buyVillageButton = new CustomSelfDrawPanel.CSDButton();
    private bool wasTall = true;
    private bool wasAiShort;
    private double storedPreCardDistance;
    private double lastDist = -1.0;
    private int m_selectedVillage = -1;
    private bool special;
    private WorldMap.SpecialVillageCache lastData;
    private DockableControl dockableControl;
    private IContainer components;

    public EmptyVillagePanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init(int villageID)
    {
      if (GameEngine.Instance.LocalWorldData.AIWorld)
      {
        switch (GameEngine.Instance.World.getSpecial(villageID))
        {
          case 7:
          case 9:
          case 11:
          case 13:
            this.wasAiShort = true;
            this.Size = new Size(199, 213);
            break;
          default:
            this.wasAiShort = false;
            this.Size = new Size(199, 273);
            break;
        }
      }
      else if (this.wasAiShort)
      {
        this.wasAiShort = false;
        this.Size = new Size(199, 273);
      }
      this.wasTall = this.isTallTreasureChestPanel(villageID);
      int num = 0;
      if (this.wasTall)
        num = 60;
      this.clearControls();
      CustomSelfDrawPanel.CSDImage csdImage1 = this.backGround_AI.init(this.wasTall, 10000);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround_AI);
      this.attackButton_AI = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.ATTACK);
      this.attackButton_AI.Position = new Point(64, 79 + num);
      this.attackButton_AI.CustomTooltipID = 2411;
      this.attackButton_AI.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAttack_Click), "EmptyVillagePanel2_attack");
      csdImage1.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton_AI);
      this.scoutButton_AI = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.SCOUT);
      this.scoutButton_AI.Position = new Point(99, 79 + num);
      this.scoutButton_AI.CustomTooltipID = 2412;
      this.scoutButton_AI.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnScout_Click), "EmptyVillagePanel2_scout");
      csdImage1.addControl((CustomSelfDrawPanel.CSDControl) this.scoutButton_AI);
      this.castleButton_AI = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.LAST_REPORT);
      this.castleButton_AI.Position = new Point(80, 47 + num);
      this.castleButton_AI.CustomTooltipID = 2445;
      this.castleButton_AI.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "EmptyVillagePanel2_castle");
      csdImage1.addControl((CustomSelfDrawPanel.CSDControl) this.castleButton_AI);
      this.treasureCastleTimeoutLabel.Text = "";
      this.treasureCastleTimeoutLabel.Color = ARGBColors.Black;
      this.treasureCastleTimeoutLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.treasureCastleTimeoutLabel.Position = new Point(10, 50);
      this.treasureCastleTimeoutLabel.Size = new Size(csdImage1.Width - 20, 80);
      this.treasureCastleTimeoutLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.treasureCastleTimeoutLabel.Visible = false;
      csdImage1.addControl((CustomSelfDrawPanel.CSDControl) this.treasureCastleTimeoutLabel);
      if (!this.wasTall)
        csdImage1.Image = (Image) GFXLibrary.mrhp_world_panel_132;
      this.backGround_Enemy.init(false, 10000);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround_Enemy);
      this.backGround_Enemy.hideBackground();
      this.invasionLabel.Text = "";
      this.invasionLabel.Color = ARGBColors.Black;
      this.invasionLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.invasionLabel.Position = new Point(57, 33);
      this.invasionLabel.Size = new Size(this.backGround_Enemy.Width - 20, 80);
      this.invasionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.invasionLabel.Visible = false;
      this.backGround_Enemy.addControl((CustomSelfDrawPanel.CSDControl) this.invasionLabel);
      CustomSelfDrawPanel.CSDImage csdImage2 = this.backGround_Resources.init(false, 10000);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround_Resources);
      this.scoutButton_Resources = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.SCOUT);
      this.scoutButton_Resources.Position = new Point(80, 49);
      this.scoutButton_Resources.Enabled = false;
      this.scoutButton_Resources.CustomTooltipID = 2443;
      this.scoutButton_Resources.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnScout_Click), "EmptyVillagePanel2_scout_stash");
      csdImage2.addControl((CustomSelfDrawPanel.CSDControl) this.scoutButton_Resources);
      CustomSelfDrawPanel.CSDImage csdImage3 = this.backGround_Charter.init(true, 10000);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround_Charter);
      this.charterLabel.Text = SK.Text("EmptyVillagePanel_Cost", "Cost to found this village");
      this.charterLabel.Color = ARGBColors.Black;
      this.charterLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.charterLabel.Position = new Point(0, 42);
      this.charterLabel.Size = new Size(csdImage3.Width, 40);
      this.charterLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      csdImage3.addControl((CustomSelfDrawPanel.CSDControl) this.charterLabel);
      this.goldImage.Image = (Image) GFXLibrary.com_32_money;
      this.goldImage.Position = new Point(105, 58);
      csdImage3.addControl((CustomSelfDrawPanel.CSDControl) this.goldImage);
      this.goldLabel.Text = "0,000,000";
      this.goldLabel.Color = ARGBColors.Black;
      this.goldLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.goldLabel.Position = new Point(0, 66);
      this.goldLabel.Size = new Size(100, 40);
      this.goldLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      csdImage3.addControl((CustomSelfDrawPanel.CSDControl) this.goldLabel);
      this.honourImage.Image = (Image) GFXLibrary.com_32_honour;
      this.honourImage.Position = new Point(105, 98);
      csdImage3.addControl((CustomSelfDrawPanel.CSDControl) this.honourImage);
      this.honourLabel.Text = "0,000,000";
      this.honourLabel.Color = ARGBColors.Black;
      this.honourLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.honourLabel.Position = new Point(0, 106);
      this.honourLabel.Size = new Size(100, 40);
      this.honourLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      csdImage3.addControl((CustomSelfDrawPanel.CSDControl) this.honourLabel);
      this.travelTimeDescLabel.Text = SK.Text("EmptyVillagePanel_TravelTime", "Time to reach this Charter");
      this.travelTimeDescLabel.Color = ARGBColors.Black;
      this.travelTimeDescLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.travelTimeDescLabel.Position = new Point(0, 148);
      this.travelTimeDescLabel.Size = new Size(csdImage3.Width, 40);
      this.travelTimeDescLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      csdImage3.addControl((CustomSelfDrawPanel.CSDControl) this.travelTimeDescLabel);
      this.travelTimeLabel.Text = "0:00";
      this.travelTimeLabel.Color = ARGBColors.Black;
      this.travelTimeLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.travelTimeLabel.Position = new Point(0, 177);
      this.travelTimeLabel.Size = new Size(100, 40);
      this.travelTimeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      csdImage3.addControl((CustomSelfDrawPanel.CSDControl) this.travelTimeLabel);
      this.travelImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[32];
      this.travelImage.Position = new Point(105, 161);
      csdImage3.addControl((CustomSelfDrawPanel.CSDControl) this.travelImage);
      this.seaConditionsImage.Image = (Image) GFXLibrary.sea_conditions[0];
      this.seaConditionsImage.Position = new Point(165, 183);
      this.seaConditionsImage.Visible = false;
      this.seaConditionsImage.Scale = 0.89999997615814209;
      csdImage3.addControl((CustomSelfDrawPanel.CSDControl) this.seaConditionsImage);
      this.buyVillageButton.ImageNorm = (Image) GFXLibrary.mrhp_button_150x25[0];
      this.buyVillageButton.ImageOver = (Image) GFXLibrary.mrhp_button_150x25[1];
      this.buyVillageButton.ImageClick = (Image) GFXLibrary.mrhp_button_150x25[2];
      this.buyVillageButton.Position = new Point(26, 215);
      this.buyVillageButton.Text.Text = SK.Text("EmptyVillagePanel_Buy_Village", "Purchase");
      this.buyVillageButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.buyVillageButton.TextYOffset = -3;
      this.buyVillageButton.Text.Color = ARGBColors.Black;
      this.buyVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnBuyVillage_Click), "EmptyVillagePanel2_buy_village");
      csdImage3.addControl((CustomSelfDrawPanel.CSDControl) this.buyVillageButton);
    }

    public void update()
    {
      this.backGround_AI.update();
      this.backGround_Charter.update();
      this.backGround_Enemy.update();
      this.backGround_Resources.update();
      this.buyVillageButton.CustomTooltipID = 0;
      if (GameEngine.Instance.World.canUserOwnMoreVillages() && GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()) && !GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.getSelectedMenuVillage()))
      {
        if (!this.buyVillageButton.Active)
          this.buyVillageButton.invalidate();
        this.buyVillageButton.Active = true;
        this.buyVillageButton.Alpha = 1f;
      }
      else
      {
        if (this.buyVillageButton.Active)
          this.buyVillageButton.invalidate();
        this.buyVillageButton.Active = false;
        this.buyVillageButton.Alpha = 0.2f;
        if (!GameEngine.Instance.World.canUserOwnMoreVillages())
        {
          if (GameEngine.Instance.World.numVillagesAllowed() <= 1 && GameEngine.Instance.World.getRank() + 1 < 12)
            this.buyVillageButton.CustomTooltipID = 2506;
          else
            this.buyVillageButton.CustomTooltipID = 2504;
        }
        else if (!GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()) || GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.getSelectedMenuVillage()))
          this.buyVillageButton.CustomTooltipID = 2505;
      }
      this.updateTreasureCastleTimeout();
      if (this.special)
      {
        WorldMap.SpecialVillageCache specialVillageData = GameEngine.Instance.World.getSpecialVillageData(this.m_selectedVillage, false);
        if (this.lastData != specialVillageData)
          this.updateSpecialData(specialVillageData);
      }
      double distance = this.storedPreCardDistance * CardTypes.getArmySpeed(GameEngine.Instance.cardsManager.UserCardData);
      double secsLeft = GameEngine.Instance.World.adjustIfIslandTravel(distance, InterfaceMgr.Instance.getSelectedMenuVillage(), this.m_selectedVillage);
      if (this.lastDist != secsLeft)
      {
        this.lastDist = secsLeft;
        this.travelTimeLabel.Text = VillageMap.createBuildTimeString((int) secsLeft);
        this.travelTimeLabel.CustomTooltipID = 20000;
        this.travelTimeLabel.CustomTooltipData = (int) secsLeft;
        int index = GameEngine.Instance.World.SpecialSeaConditionsData + 4;
        if (index < 0)
          index = 0;
        else if (index >= 9)
          index = 8;
        this.seaConditionsImage.Image = (Image) GFXLibrary.sea_conditions[index];
        this.seaConditionsImage.CustomTooltipID = 23000 + index;
      }
      if (!GameEngine.Instance.World.WorldEnded)
        return;
      this.buyVillageButton.Visible = false;
    }

    private bool isTallTreasureChestPanel(int villageID)
    {
      return GameEngine.Instance.World.isSpecial(villageID) && GameEngine.Instance.World.isAttackableSpecial(villageID) && SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(villageID)) && (VillageMap.getCurrentServerTime() - GameEngine.Instance.World.getLastTreasureCastleAttackTime()).TotalSeconds < (double) WorldMap.TreasureCastle_AttackGap;
    }

    private void updateTreasureCastleTimeout()
    {
      if (!GameEngine.Instance.World.isSpecial(this.m_selectedVillage) || !GameEngine.Instance.World.isAttackableSpecial(this.m_selectedVillage))
        return;
      TimeSpan timeSpan = VillageMap.getCurrentServerTime() - GameEngine.Instance.World.getLastTreasureCastleAttackTime();
      int treasureCastleAttackGap = WorldMap.TreasureCastle_AttackGap;
      if (timeSpan.TotalSeconds < (double) treasureCastleAttackGap)
      {
        this.treasureCastleTimeoutLabel.TextDiffOnly = SK.Text("EmptyVillage_NextAttackAvailable", "Next Attack Available in") + " " + VillageMap.createBuildTimeString(treasureCastleAttackGap - (int) timeSpan.TotalSeconds);
      }
      else
      {
        this.treasureCastleTimeoutLabel.TextDiffOnly = "";
        if (!this.treasureCastleTimeoutLabel.Visible || GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
          return;
        this.attackButton_AI.Enabled = true;
      }
    }

    public void updateEmptyVillageText(int selectedVillage)
    {
      bool flag1 = false;
      bool flag2 = false;
      if (GameEngine.Instance.World.isSpecial(selectedVillage) && GameEngine.Instance.World.isAttackableSpecial(selectedVillage))
      {
        bool flag3 = this.isTallTreasureChestPanel(selectedVillage);
        if (flag3 != this.wasTall)
        {
          this.init(selectedVillage);
          flag1 = true;
        }
        flag2 = flag3;
      }
      if (!flag1 && GameEngine.Instance.LocalWorldData.AIWorld)
      {
        bool flag4;
        switch (GameEngine.Instance.World.getSpecial(selectedVillage))
        {
          case 7:
          case 9:
          case 11:
          case 13:
            flag4 = true;
            break;
          default:
            flag4 = false;
            break;
        }
        if (flag4 != this.wasAiShort)
          this.init(selectedVillage);
      }
      NumberFormatInfo nfi = GameEngine.NFI;
      this.m_selectedVillage = selectedVillage;
      this.buyVillageButton.Enabled = true;
      this.attackButton_AI.Enabled = true;
      this.scoutButton_AI.Enabled = true;
      this.scoutButton_Resources.Enabled = true;
      this.treasureCastleTimeoutLabel.Visible = false;
      this.backGround_AI.Visible = false;
      this.backGround_Enemy.Visible = false;
      this.backGround_Resources.Visible = false;
      this.backGround_Charter.Visible = false;
      this.special = false;
      this.invasionLabel.Visible = false;
      this.seaConditionsImage.Visible = false;
      this.backGround_AI.removeWikiLink(this.wikiLink);
      this.wikiLink = (CustomSelfDrawPanel.WikiLinkControl) null;
      int special = GameEngine.Instance.World.getSpecial(selectedVillage);
      if (SpecialVillageTypes.IS_ROYAL_TOWER(special))
        this.wikiLink = this.backGround_AI.addWikiLink(54);
      else if (SpecialVillageTypes.IS_TREASURE_CASTLE(special))
        this.wikiLink = this.backGround_AI.addWikiLink(49);
      else if (special == 15 || special == 17)
        this.wikiLink = this.backGround_AI.addWikiLink(50);
      if (GameEngine.Instance.World.isSpecial(selectedVillage))
      {
        bool flag5 = true;
        this.special = true;
        if (!GameEngine.Instance.World.isAttackableSpecial(selectedVillage))
        {
          if (special >= 100 && special <= 199)
          {
            this.backGround_Resources.Visible = true;
            this.backGround_Resources.updateHeading(GameEngine.Instance.World.getVillageNameOrType(selectedVillage));
            this.backGround_Resources.updatePanelTypeFromVillageID(selectedVillage);
            this.scoutButton_Resources.Enabled = false;
          }
          else
          {
            this.backGround_Enemy.Visible = true;
            if (special == 30)
            {
              switch (GameEngine.Instance.World.getAIInvasionMarkerState(selectedVillage))
              {
                case 0:
                  this.backGround_Enemy.updateHeading(SK.Text("Invasion_None", "No Invasion Sighted"));
                  break;
                case 1:
                  this.backGround_Enemy.updateHeading(SK.Text("Invasion_Planned", "Invasion Sighted"));
                  DateTime nextAiInvasionDate = GameEngine.Instance.World.getNextAIInvasionDate(selectedVillage);
                  if (nextAiInvasionDate != DateTime.MinValue)
                  {
                    TimeSpan timeSpan = nextAiInvasionDate - VillageMap.getCurrentServerTime();
                    this.invasionLabel.Visible = true;
                    this.invasionLabel.Text = VillageMap.createBuildTimeString((int) timeSpan.TotalSeconds);
                    break;
                  }
                  break;
                case 2:
                  this.backGround_Enemy.updateHeading(SK.Text("Invasion_Inprogress", "Invasion In Progress"));
                  break;
              }
            }
            else
              this.backGround_Enemy.updateHeading(GameEngine.Instance.World.getVillageNameOrType(selectedVillage));
            this.backGround_Enemy.updatePanelTypeFromVillageID(selectedVillage);
          }
        }
        else
        {
          this.backGround_AI.Visible = true;
          this.backGround_AI.updateHeading(GameEngine.Instance.World.getVillageNameOrType(selectedVillage));
          this.backGround_AI.updatePanelTypeFromVillageID(selectedVillage);
          if (SpecialVillageTypes.IS_TREASURE_CASTLE(special))
          {
            if (GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
              this.attackButton_AI.Enabled = false;
            if (flag2)
            {
              this.updateTreasureCastleTimeout();
              this.treasureCastleTimeoutLabel.Visible = true;
              this.attackButton_AI.Enabled = false;
            }
          }
          else if (SpecialVillageTypes.IS_ROYAL_TOWER(special) && GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
            this.attackButton_AI.Enabled = false;
          if (GameEngine.Instance.World.isHeretic())
          {
            switch (special)
            {
              case 7:
              case 9:
              case 11:
              case 13:
                this.attackButton_AI.Enabled = false;
                this.scoutButton_AI.Enabled = false;
                flag5 = false;
                break;
            }
          }
        }
        if (!GameEngine.Instance.World.isScoutableSpecial(selectedVillage) || GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage) || !flag5)
        {
          this.scoutButton_AI.Enabled = false;
          this.scoutButton_Resources.Enabled = false;
        }
        else
        {
          this.scoutButton_AI.Enabled = true;
          this.scoutButton_Resources.Enabled = true;
        }
      }
      else
      {
        this.backGround_Charter.Visible = true;
        this.backGround_Charter.updateHeading(SK.Text("EmptyVillagePanel_Available_Village", "New Village Charter"));
        this.backGround_Charter.updatePanelTypeFromVillageID(selectedVillage);
        this.backGround_Charter.stretchBackground();
        this.Parent.Invalidate();
        double num1 = GameEngine.Instance.LocalWorldData.villageGoldCost * (GameEngine.Instance.World.calcVillageDistance(InterfaceMgr.Instance.getSelectedMenuVillage(), selectedVillage) * GameEngine.Instance.LocalWorldData.villageCostDistanceMultiplier + 1.0);
        int numOwnedVillages = GameEngine.Instance.World.numVillagesOwned();
        this.goldLabel.Text = ((int) (double) ((int) num1 * numOwnedVillages)).ToString("N", (IFormatProvider) nfi);
        WorldData localWorldData = GameEngine.Instance.LocalWorldData;
        Point villageLocation1 = GameEngine.Instance.World.getVillageLocation(InterfaceMgr.Instance.OwnSelectedVillage);
        Point villageLocation2 = GameEngine.Instance.World.getVillageLocation(selectedVillage);
        double num2 = Math.Sqrt((double) ((villageLocation1.X - villageLocation2.X) * (villageLocation1.X - villageLocation2.X) + (villageLocation1.Y - villageLocation2.Y) * (villageLocation1.Y - villageLocation2.Y))) * (localWorldData.CaptainsMoveSpeed * localWorldData.gamePlaySpeed * ResearchData.CaptainTimes[(int) GameEngine.Instance.World.UserResearchData.Research_Courtiers]);
        this.storedPreCardDistance = num2;
        double distance = num2 * CardTypes.getArmySpeed(GameEngine.Instance.cardsManager.UserCardData);
        double secsLeft = GameEngine.Instance.World.adjustIfIslandTravel(distance, InterfaceMgr.Instance.OwnSelectedVillage, selectedVillage);
        this.lastDist = secsLeft;
        this.travelTimeLabel.Text = VillageMap.createBuildTimeString((int) secsLeft);
        this.travelTimeLabel.CustomTooltipID = 20000;
        this.travelTimeLabel.CustomTooltipData = (int) secsLeft;
        int num3 = 0;
        if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
        {
          num3 = ResearchData.getVillageBuyHonourCost(numOwnedVillages);
          if (num3 > 0 && GameEngine.Instance.World.FourthAgeWorld && numOwnedVillages < GameEngine.Instance.World.MostAge4Villages)
            num3 = 0;
        }
        if (GameEngine.Instance.World.isIslandTravel(InterfaceMgr.Instance.OwnSelectedVillage, selectedVillage))
        {
          int index = GameEngine.Instance.World.SpecialSeaConditionsData + 4;
          if (index < 0)
            index = 0;
          else if (index >= 9)
            index = 8;
          this.seaConditionsImage.Visible = true;
          this.seaConditionsImage.Image = (Image) GFXLibrary.sea_conditions[index];
          this.seaConditionsImage.CustomTooltipID = 23000 + index;
        }
        if (num3 > 0)
        {
          this.honourImage.Visible = true;
          this.honourLabel.Visible = true;
          this.honourLabel.Text = num3.ToString("N", (IFormatProvider) nfi);
        }
        else
        {
          this.honourImage.Visible = false;
          this.honourLabel.Visible = false;
        }
      }
    }

    public void updateSpecialData(WorldMap.SpecialVillageCache specialData)
    {
      string subHeading = "";
      this.lastData = specialData;
      if (specialData != null && specialData.resourceType > 0 && specialData.resourceLevel > 0)
      {
        NumberFormatInfo nfi = GameEngine.NFI;
        subHeading = specialData.resourceLevel.ToString("N", (IFormatProvider) nfi);
      }
      if (subHeading.Length > 0)
      {
        this.backGround_Resources.updateSubHeading(subHeading);
      }
      else
      {
        this.backGround_Resources.updateSubHeading("");
        this.scoutButton_Resources.Enabled = false;
      }
      if (!GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
        this.scoutButton_Resources.Enabled = true;
      else
        this.scoutButton_Resources.Enabled = false;
    }

    public void forceDisable()
    {
      this.buyVillageButton.Enabled = false;
      this.attackButton_AI.Enabled = false;
      this.scoutButton_AI.Enabled = false;
      this.scoutButton_Resources.Enabled = false;
    }

    private void btnBuyVillage_Click()
    {
      if (!this.buyVillageButton.Active)
        return;
      if (GameEngine.Instance.World.canUserOwnMoreVillages() && GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()))
      {
        int selectedVillage = InterfaceMgr.Instance.SelectedVillage;
        if (selectedVillage >= 0)
        {
          double num1 = GameEngine.Instance.LocalWorldData.villageGoldCost * (GameEngine.Instance.World.calcVillageDistance(InterfaceMgr.Instance.getSelectedMenuVillage(), selectedVillage) * GameEngine.Instance.LocalWorldData.villageCostDistanceMultiplier + 1.0);
          if (GameEngine.Instance.World.getCurrentGold() >= num1)
          {
            int num2 = 0;
            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
            {
              num2 = ResearchData.getVillageBuyHonourCost(GameEngine.Instance.World.numVillagesOwned());
              if (num2 > 0 && GameEngine.Instance.World.FourthAgeWorld && GameEngine.Instance.World.numVillagesOwned() < GameEngine.Instance.World.MostAge4Villages)
                num2 = 0;
            }
            if (num2 <= 0 || GameEngine.Instance.World.getCurrentHonour() >= (double) num2)
            {
              InterfaceMgr.Instance.openBuyVillageWindow(selectedVillage, true);
            }
            else
            {
              int num3 = (int) MyMessageBox.Show(SK.Text("EmptyVillagePanel_Not_Enough_Honour", "Not enough honour"), SK.Text("EmptyVillagePanel_Buy_Village_Error", "Buy Village Error"));
            }
          }
          else
          {
            int num4 = (int) MyMessageBox.Show(SK.Text("EmptyVillagePanel_Not_Enough_Gold", "Not enough gold"), SK.Text("EmptyVillagePanel_Buy_Village_Error", "Buy Village Error"));
          }
        }
        else
        {
          int num = (int) MyMessageBox.Show(SK.Text("EmptyVillagePanel_Not_Enough_Gold", "Not enough gold"), SK.Text("EmptyVillagePanel_Buy_Village_Error", "Buy Village Error"));
        }
      }
      else
      {
        int num5 = (int) MyMessageBox.Show(SK.Text("EmptyVillagePanel_No_More_Villages", "You cannot own more villages."), SK.Text("EmptyVillagePanel_Buy_Village_Error", "Buy Village Error"));
      }
    }

    private void btnAttack_Click()
    {
      if (InterfaceMgr.Instance.SelectedVillage < 0)
        return;
      int selectedVillage = InterfaceMgr.Instance.SelectedVillage;
      GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.OwnSelectedVillage, selectedVillage);
    }

    private void btnScout_Click()
    {
      if (InterfaceMgr.Instance.SelectedVillage < 0)
        return;
      InterfaceMgr.Instance.openScoutPopupWindow(InterfaceMgr.Instance.SelectedVillage, true);
    }

    private void castleClick()
    {
      RemoteServices.Instance.set_ViewCastle_UserCallBack(new RemoteServices.ViewCastle_UserCallBack(this.viewCastleCallback));
      RemoteServices.Instance.ViewCastle_Village(InterfaceMgr.Instance.SelectedVillage);
    }

    public void viewCastleCallback(ViewCastle_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      int special = GameEngine.Instance.World.getSpecial(InterfaceMgr.Instance.SelectedVillage);
      this.closeControl(true);
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(6);
      int villageID = returnData.villageID;
      int campMode = 0;
      switch (special)
      {
        case 3:
          campMode = 1;
          villageID = -2;
          break;
        case 5:
          campMode = 2;
          villageID = -3;
          break;
      }
      GameEngine.Instance.InitCastleView(returnData.castleMapSnapshot, returnData.castleTroopsSnapshot, returnData.keepLevel, campMode, returnData.defencesLevel, villageID, returnData.landType);
      CastleMapBattlePanel2.fromWorld();
      InterfaceMgr.Instance.castleBattleTimes(returnData.lastCastleTime, returnData.lastTroopTime);
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
      this.Name = nameof (EmptyVillagePanel2);
      this.Size = new Size(199, 273);
      this.ResumeLayout(false);
    }
  }
}
