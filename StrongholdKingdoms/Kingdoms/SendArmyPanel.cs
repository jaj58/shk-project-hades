// Decompiled with JetBrains decompiler
// Type: Kingdoms.SendArmyPanel
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
  public class SendArmyPanel : CustomSelfDrawPanel
  {
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CardBarGDI cardbar = new CardBarGDI();
    private CustomSelfDrawPanel.CSDImage titleImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage backgroundRightEdge = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage backgroundBottomEdge = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage gfxImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage arrowImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage targetImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage buttonIndentImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel villageActionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel timeLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel tooltipLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel needCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton launchButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDTrackBar sliderImage = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDLabel sliderValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel sliderHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton sliderButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton_Vandalise = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton_Pillage = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton_Ransack = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton_Raze = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton_Capture = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton_GoldRaid = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel targetVillageLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton targetVillageFavourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel errorLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel honourPenaltyLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel honourPenaltyValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel captureCostLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel captureCostValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage honourPenaltyImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage captureCostImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage seaConditionsImage = new CustomSelfDrawPanel.CSDImage();
    private int m_selectedPenalty;
    private int m_captureHonourCost;
    private CastleMapAttackerSetupPanel m_parent;
    private bool m_captureAllowed = true;
    private bool capitalToCapital;
    private bool toCapital;
    private int m_toVillage = -1;
    private int m_fromVillage = -1;
    private int m_travelFromVillage = -1;
    private BattleHonourData m_battleHonourData;
    private int currentPillageType;
    private bool noCaptain;
    private int maxPillageValue = 1;
    private int maxRansackValue = 1;
    private int maxGoldRaidValue = 1;
    private double storedPreCardDistance;
    private int m_lastSeaConditions = -1;
    private int currentCommand = -1;
    private DateTime lastLaunchTime = DateTime.MinValue;
    private int attackTypeRef;
    private int pillageValueRef;
    private IContainer components;

    public SendArmyPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(
      int parentFromVillage,
      int fromVillageID,
      int toVillageID,
      string villageName,
      double distance,
      BattleHonourData honourData,
      bool gotCaptain,
      CastleMapAttackerSetupPanel parent)
    {
      this.m_fromVillage = parentFromVillage;
      this.m_toVillage = toVillageID;
      this.m_travelFromVillage = fromVillageID;
      this.m_parent = parent;
      this.m_battleHonourData = honourData;
      this.m_selectedPenalty = 0;
      this.toCapital = false;
      this.m_captureHonourCost = 0;
      this.m_lastSeaConditions = -1;
      this.clearControls();
      int y = 39;
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
      this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), this.Size);
      this.mainBackgroundImage.Position = new Point(0, y);
      this.mainBackgroundImage.Size = new Size(this.Size.Width, this.Size.Height - y);
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundBottomEdge.Image = (Image) GFXLibrary.popup_border_bottom;
      this.backgroundBottomEdge.Position = new Point(0, this.Height - 2);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundBottomEdge);
      this.backgroundRightEdge.Image = (Image) GFXLibrary.popup_border_rhs;
      this.backgroundRightEdge.Position = new Point(this.Width - 2, y);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundRightEdge);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(659, 5);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "SendArmyPanel_close");
      this.titleImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.titleImage, 33, new Point(609, 5));
      this.cardbar.Position = new Point(0, 4);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardbar);
      this.cardbar.init(6);
      this.gfxImage.Image = (Image) GFXLibrary.send_army_illustration;
      this.gfxImage.Position = new Point(25, 77);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.gfxImage);
      this.targetVillageLabel.Text = villageName;
      this.targetVillageLabel.Color = ARGBColors.White;
      this.targetVillageLabel.DropShadowColor = ARGBColors.Black;
      this.targetVillageLabel.Position = new Point(5, 10);
      this.targetVillageLabel.Size = new Size(this.gfxImage.Width - 10 - 14 - 20, 32);
      this.targetVillageLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.targetVillageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.gfxImage.addControl((CustomSelfDrawPanel.CSDControl) this.targetVillageLabel);
      if (AttackTargetsPanel.isFavourite(toVillageID))
      {
        this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_1;
        this.targetVillageFavourite.CustomTooltipID = 2107;
      }
      else
      {
        this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_3;
        this.targetVillageFavourite.CustomTooltipID = 2018;
      }
      this.targetVillageFavourite.OverBrighten = true;
      this.targetVillageFavourite.Position = new Point(this.gfxImage.Width - 20 - 16, 10);
      this.targetVillageFavourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.targetVillageFavourite.Data = 0;
      this.gfxImage.addControl((CustomSelfDrawPanel.CSDControl) this.targetVillageFavourite);
      this.sliderImage.Position = new Point(273, 304);
      this.sliderImage.Margin = new Rectangle(90, 70, 19, 25);
      this.sliderImage.Value = 0;
      this.sliderImage.Max = 10;
      this.sliderImage.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.sliderImage);
      this.sliderImage.Create((Image) GFXLibrary.send_army_slider, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar);
      this.sliderValueLabel.Text = "100%";
      this.sliderValueLabel.Color = ARGBColors.White;
      this.sliderValueLabel.DropShadowColor = ARGBColors.Black;
      this.sliderValueLabel.Position = new Point(11, 65);
      this.sliderValueLabel.Size = new Size(64, 32);
      this.sliderValueLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.sliderValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.sliderImage.addControl((CustomSelfDrawPanel.CSDControl) this.sliderValueLabel);
      this.sliderHeaderLabel.Text = "";
      this.sliderHeaderLabel.Color = ARGBColors.White;
      this.sliderHeaderLabel.DropShadowColor = ARGBColors.Black;
      this.sliderHeaderLabel.Position = new Point(63, 15);
      this.sliderHeaderLabel.Size = new Size(135, 32);
      this.sliderHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.sliderHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.sliderImage.addControl((CustomSelfDrawPanel.CSDControl) this.sliderHeaderLabel);
      this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[24];
      this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[24];
      this.sliderButton.Position = new Point(-5, -8);
      this.sliderButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sliderClick), "SendArmyPanel_change_type");
      this.sliderImage.addControl((CustomSelfDrawPanel.CSDControl) this.sliderButton);
      this.arrowImage.Image = (Image) GFXLibrary.send_army_timer;
      this.arrowImage.Position = new Point(33, 304);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.arrowImage);
      this.buttonIndentImage.Image = (Image) GFXLibrary.monk_screen_buttongroup_inset;
      this.buttonIndentImage.Position = new Point(503, 77);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.buttonIndentImage);
      this.villageActionLabel.Text = "";
      this.villageActionLabel.Color = ARGBColors.White;
      this.villageActionLabel.DropShadowColor = ARGBColors.Black;
      this.villageActionLabel.Position = new Point(31, 243);
      this.villageActionLabel.Size = new Size(340, 30);
      this.villageActionLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.villageActionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageActionLabel);
      this.tooltipLabel.Text = "";
      this.tooltipLabel.Color = ARGBColors.White;
      this.tooltipLabel.DropShadowColor = ARGBColors.Black;
      this.tooltipLabel.Position = new Point(31, 266);
      this.tooltipLabel.Size = new Size(340, 60);
      this.tooltipLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tooltipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tooltipLabel);
      this.honourPenaltyLabel.Text = SK.Text("LaunchAttackPopup_Honour_Penalty", "Honour Penalty");
      this.honourPenaltyLabel.Color = ARGBColors.White;
      this.honourPenaltyLabel.DropShadowColor = ARGBColors.Black;
      this.honourPenaltyLabel.Position = new Point(270, 247);
      this.honourPenaltyLabel.Size = new Size(180, 60);
      this.honourPenaltyLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.honourPenaltyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.honourPenaltyLabel);
      this.honourPenaltyValueLabel.Text = "0,000,000";
      this.honourPenaltyValueLabel.Color = Color.FromArgb(18, (int) byte.MaxValue, 0);
      this.honourPenaltyValueLabel.DropShadowColor = ARGBColors.Black;
      this.honourPenaltyValueLabel.Position = new Point(270, 267);
      this.honourPenaltyValueLabel.Size = new Size(180, 60);
      this.honourPenaltyValueLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.honourPenaltyValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.honourPenaltyValueLabel);
      this.honourPenaltyImage.Image = (Image) GFXLibrary.com_32_honour;
      this.honourPenaltyImage.Position = new Point(450, 247);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.honourPenaltyImage);
      this.captureCostLabel.Text = SK.Text("LaunchAttackPopup_Honour_Capture", "Capture Cost");
      this.captureCostLabel.Color = ARGBColors.White;
      this.captureCostLabel.DropShadowColor = ARGBColors.Black;
      this.captureCostLabel.Position = new Point(270, 287);
      this.captureCostLabel.Size = new Size(180, 60);
      this.captureCostLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.captureCostLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.captureCostLabel);
      this.captureCostValueLabel.Text = "10,000,000";
      this.captureCostValueLabel.Color = Color.FromArgb(18, (int) byte.MaxValue, 0);
      this.captureCostValueLabel.DropShadowColor = ARGBColors.Black;
      this.captureCostValueLabel.Position = new Point(270, 307);
      this.captureCostValueLabel.Size = new Size(180, 60);
      this.captureCostValueLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.captureCostValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.captureCostValueLabel);
      this.captureCostImage.Image = (Image) GFXLibrary.com_32_honour;
      this.captureCostImage.Position = new Point(450, 287 + y);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.captureCostImage);
      this.needCaptainLabel.Text = SK.Text("LaunchAttackPopup_Need_Captain", "Need Captain");
      this.needCaptainLabel.Color = ARGBColors.White;
      this.needCaptainLabel.DropShadowColor = ARGBColors.Black;
      this.needCaptainLabel.Position = new Point(500, 358);
      this.needCaptainLabel.Size = new Size(180, 32);
      this.needCaptainLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.needCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.needCaptainLabel.Visible = false;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.needCaptainLabel);
      this.storedPreCardDistance = distance;
      distance = GameEngine.Instance.World.adjustIfIslandTravel(distance, this.m_travelFromVillage, this.m_toVillage);
      distance *= CardTypes.getArmySpeed(GameEngine.Instance.cardsManager.UserCardData);
      this.timeLabel.Text = VillageMap.createBuildTimeString((int) distance);
      this.timeLabel.Color = ARGBColors.White;
      this.timeLabel.DropShadowColor = ARGBColors.Black;
      this.timeLabel.Position = new Point(0, 23);
      this.timeLabel.Size = new Size(191, 24);
      this.timeLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.timeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.arrowImage.addControl((CustomSelfDrawPanel.CSDControl) this.timeLabel);
      this.errorLabel.Text = "Error Message Here";
      this.errorLabel.Color = ARGBColors.White;
      this.errorLabel.DropShadowColor = ARGBColors.Black;
      this.errorLabel.Position = new Point(0, 411);
      this.errorLabel.Size = new Size(this.mainBackgroundImage.Width, 32);
      this.errorLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.errorLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.errorLabel);
      this.actionButton_GoldRaid.Enabled = false;
      bool flag1 = true;
      bool flag2 = true;
      this.updateButtons(-1);
      this.actionButton_Vandalise.Position = new Point(10, 12);
      this.actionButton_Vandalise.Data = 0;
      this.actionButton_Vandalise.CustomTooltipID = 2100;
      this.actionButton_Vandalise.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_vandalise");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton_Vandalise);
      this.actionButton_Pillage.Position = new Point(84, 12);
      this.actionButton_Pillage.Data = 1;
      this.actionButton_Pillage.CustomTooltipID = 2102;
      this.actionButton_Pillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_pillage");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton_Pillage);
      this.actionButton_Ransack.Position = new Point(10, 99);
      this.actionButton_Ransack.Data = 2;
      this.actionButton_Ransack.CustomTooltipID = 2103;
      this.actionButton_Ransack.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_ransack");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton_Ransack);
      this.actionButton_Raze.Position = new Point(84, 99);
      this.actionButton_Raze.Data = 3;
      this.actionButton_Raze.CustomTooltipID = 2104;
      this.actionButton_Raze.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_raze");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton_Raze);
      this.actionButton_Capture.Position = new Point(10, 186);
      this.actionButton_Capture.Data = 4;
      this.actionButton_Capture.CustomTooltipID = 2101;
      this.actionButton_Capture.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_capture");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton_Capture);
      this.actionButton_GoldRaid.Position = new Point(84, 186);
      this.actionButton_GoldRaid.Data = 5;
      this.actionButton_GoldRaid.CustomTooltipID = 2105;
      this.actionButton_GoldRaid.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_gold_raid");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton_GoldRaid);
      int special = GameEngine.Instance.World.getSpecial(toVillageID);
      int index1;
      switch (special)
      {
        case 3:
        case 4:
          index1 = 24;
          break;
        case 5:
        case 6:
          index1 = 25;
          break;
        case 7:
        case 8:
        case 9:
        case 10:
        case 11:
        case 12:
        case 13:
        case 14:
          index1 = 28;
          break;
        case 15:
        case 16:
        case 17:
        case 18:
          index1 = 53;
          break;
        case 40:
        case 41:
        case 42:
        case 43:
        case 44:
        case 45:
        case 46:
        case 47:
        case 48:
        case 49:
        case 50:
          index1 = 54;
          break;
        case 51:
        case 52:
        case 53:
        case 54:
        case 55:
        case 56:
        case 57:
        case 58:
        case 59:
        case 60:
          index1 = 55;
          break;
        case 61:
        case 62:
        case 63:
        case 64:
        case 65:
        case 66:
        case 67:
        case 68:
        case 69:
        case 70:
          index1 = 56;
          break;
        case 71:
        case 72:
        case 73:
        case 74:
        case 75:
        case 76:
        case 77:
        case 78:
        case 79:
        case 80:
          index1 = 57;
          break;
        case 81:
        case 82:
        case 83:
        case 84:
        case 85:
        case 86:
        case 87:
        case 88:
        case 89:
        case 90:
          index1 = 58;
          break;
        case 200:
        case 201:
        case 202:
        case 203:
        case 204:
        case 205:
        case 206:
        case 207:
        case 208:
        case 209:
        case 210:
        case 211:
        case 212:
        case 213:
        case 214:
        case 215:
        case 216:
        case 217:
        case 218:
        case 219:
        case 220:
          index1 = 65;
          break;
        default:
          index1 = !GameEngine.Instance.World.isRegionCapital(toVillageID) ? (!GameEngine.Instance.World.isCountyCapital(toVillageID) ? (!GameEngine.Instance.World.isProvinceCapital(toVillageID) ? (!GameEngine.Instance.World.isCountryCapital(toVillageID) ? GameEngine.Instance.World.getVillageSize(toVillageID) : 52) : 51) : 50) : 49;
          break;
      }
      this.targetImage.Image = (Image) GFXLibrary.scout_screen_icons[index1];
      this.targetImage.Position = new Point(143, 15);
      this.arrowImage.addControl((CustomSelfDrawPanel.CSDControl) this.targetImage);
      this.maxPillageValue = ResearchData.pillageLevels[(int) GameEngine.Instance.World.UserResearchData.Research_Pillaging];
      this.maxRansackValue = ResearchData.ransackLevels[(int) GameEngine.Instance.World.UserResearchData.Research_Ransack];
      this.maxGoldRaidValue = 50;
      this.launchButton.ImageNorm = (Image) GFXLibrary.button_with_inset_normal;
      this.launchButton.ImageOver = (Image) GFXLibrary.button_with_inset_over;
      this.launchButton.ImageClick = (Image) GFXLibrary.button_with_inset_pushed;
      this.launchButton.Position = new Point(520, 377);
      this.launchButton.Text.Text = SK.Text("ScoutPopup_Go", "Go");
      this.launchButton.Text.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
      this.launchButton.TextYOffset = 1;
      this.launchButton.Text.Color = ARGBColors.Black;
      this.launchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.launch), "SendArmyPanel_launch");
      this.launchButton.Enabled = false;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.launchButton);
      bool flag3 = false;
      int rank = GameEngine.Instance.World.getRank();
      if (GameEngine.Instance.World.isCapital(fromVillageID) && GameEngine.Instance.World.isSpecial(toVillageID) && (SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(toVillageID)) || SpecialVillageTypes.IS_ROYAL_TOWER(GameEngine.Instance.World.getSpecial(toVillageID))))
      {
        flag1 = false;
        this.actionButton_Capture.Visible = false;
        this.actionButton_Pillage.Visible = false;
        this.actionButton_Ransack.Visible = false;
        flag2 = false;
        this.actionButton_Raze.Visible = false;
        this.actionButton_GoldRaid.Visible = false;
        this.launchButton.Enabled = false;
        flag3 = true;
      }
      else if (GameEngine.Instance.World.isCapital(toVillageID))
      {
        this.toCapital = true;
        this.actionButton_Capture.Enabled = false;
        this.actionButton_Pillage.Enabled = false;
        this.actionButton_Ransack.Enabled = false;
        this.actionButton_Raze.Enabled = false;
        flag2 = false;
        if (GameEngine.Instance.World.isCapital(fromVillageID))
        {
          this.capitalToCapital = true;
          this.actionButton_GoldRaid.Enabled = true;
        }
        else
          this.actionButton_GoldRaid.Enabled = false;
      }
      else
      {
        if (SpecialVillageTypes.IS_ROYAL_TOWER(GameEngine.Instance.World.getSpecial(toVillageID)))
          this.actionButton_Capture.Enabled = true;
        else if (GameEngine.Instance.World.canUserOwnMoreVillages() && !GameEngine.Instance.World.isUserVillage(toVillageID))
        {
          this.actionButton_Capture.Enabled = true;
          NumberFormatInfo nfi = GameEngine.NFI;
          if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
          {
            this.m_captureHonourCost = ResearchData.getVillageBuyHonourCost(GameEngine.Instance.World.numVillagesOwned());
            if (this.m_captureHonourCost > 0 && GameEngine.Instance.World.FourthAgeWorld && GameEngine.Instance.World.numVillagesOwned() < GameEngine.Instance.World.MostAge4Villages)
              this.m_captureHonourCost = 0;
          }
          this.captureCostValueLabel.Text = this.m_captureHonourCost.ToString("N", (IFormatProvider) nfi);
        }
        else
          this.actionButton_Capture.Enabled = false;
        if (GameEngine.Instance.World.getCurrentHonour() > 0.0 && (GameEngine.Instance.World.getVillageUserID(toVillageID) >= 0 || GameEngine.Instance.LocalWorldData.AIWorld && GameEngine.Instance.World.isSpecialAIPlayer(toVillageID)) && rank >= GameEngine.Instance.LocalWorldData.RazeMinRank - 1)
        {
          if (!GameEngine.Instance.LocalWorldData.EraWorld || GameEngine.Instance.LocalWorldData.AIWorld)
            this.actionButton_Raze.Enabled = true;
          else if (honourData != null)
          {
            if (honourData.defenderRank < 8)
              this.actionButton_Raze.Enabled = false;
            else
              this.actionButton_Raze.Enabled = true;
          }
          else
            this.actionButton_Raze.Enabled = true;
        }
        else
          this.actionButton_Raze.Enabled = false;
        if (GameEngine.Instance.World.isCapital(fromVillageID))
        {
          flag1 = false;
          this.actionButton_Capture.Visible = false;
          this.actionButton_Pillage.Visible = false;
          this.actionButton_Ransack.Visible = false;
          flag2 = false;
          this.actionButton_Raze.Visible = false;
          this.actionButton_GoldRaid.Visible = false;
          this.launchButton.Enabled = true;
          this.actionButton_Vandalise.CustomTooltipID = 2106;
          this.updateButtons(0);
        }
        else if (GameEngine.Instance.LocalWorldData.AIWorld && GameEngine.Instance.World.isSpecialAIPlayer(toVillageID))
        {
          this.actionButton_Pillage.Visible = false;
          this.actionButton_Ransack.Visible = false;
          this.actionButton_GoldRaid.Visible = false;
          this.actionButton_Raze.Visible = false;
          this.actionButton_Vandalise.CustomTooltipID = 2106;
          this.actionButton_Capture.Position = new Point(84, 12);
        }
        else if (!GameEngine.Instance.World.isSpecial(toVillageID) && GameEngine.Instance.World.getVillageUserID(toVillageID) >= 0)
        {
          if (GameEngine.Instance.World.UserResearchData.Research_Ransack == (byte) 0)
            this.actionButton_Ransack.Enabled = false;
          else
            this.actionButton_Ransack.Enabled = true;
        }
        else if (SpecialVillageTypes.IS_ROYAL_TOWER(GameEngine.Instance.World.getSpecial(toVillageID)))
        {
          this.actionButton_Pillage.Visible = false;
          this.actionButton_Ransack.Visible = false;
          flag2 = false;
          this.actionButton_Raze.Visible = false;
          this.actionButton_GoldRaid.Visible = false;
          this.actionButton_Vandalise.Visible = false;
          if (!gotCaptain)
            this.noCaptain = true;
          this.updateButtons(4);
        }
        else
        {
          flag1 = false;
          this.actionButton_Capture.Visible = false;
          this.actionButton_Pillage.Visible = false;
          this.actionButton_Ransack.Visible = false;
          flag2 = false;
          this.actionButton_Raze.Visible = false;
          this.actionButton_GoldRaid.Visible = false;
          this.launchButton.Enabled = true;
          this.actionButton_Vandalise.CustomTooltipID = 2106;
          this.updateButtons(0);
        }
        if (parentFromVillage != fromVillageID)
        {
          flag1 = false;
          this.actionButton_Capture.Visible = false;
          flag2 = false;
          this.actionButton_Raze.Visible = false;
        }
      }
      if ((flag2 || flag1 && !this.capitalToCapital) && !gotCaptain)
        this.noCaptain = true;
      this.titleImage.Image = (Image) GFXLibrary.popup_title_bar;
      this.titleImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.titleImage);
      this.titleLabel.Text = SK.Text("GENERIC_Launch_Attack", "Launch Attack");
      this.titleLabel.Color = ARGBColors.White;
      this.titleLabel.DropShadowColor = ARGBColors.Black;
      this.titleLabel.Position = new Point(20, 5);
      this.titleLabel.Size = new Size(this.Width, 32);
      this.titleLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
      this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.titleImage.addControl((CustomSelfDrawPanel.CSDControl) this.titleLabel);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(659, 5);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "SendArmyPanel_close");
      this.titleImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      if (flag3)
      {
        this.errorLabel.Visible = true;
        this.errorLabel.Text = !SpecialVillageTypes.IS_ROYAL_TOWER(GameEngine.Instance.World.getSpecial(toVillageID)) ? SK.Text("LaunchAttackPopup_Not_Attack_TC_From_Capitals", "You cannot attack Treasure Castles from Capitals.") : SK.Text("LaunchAttackPopup_Not_Attack_RT_From_Capitals", "You cannot attack Royal Towers from Capitals.");
      }
      else
        this.errorLabel.Text = special < 100 || special > 199 ? (special != 5 ? (special != 3 ? (special == 7 || special == 9 || special == 11 || special == 13 ? SK.Text("LaunchAttackPopup_No_Honour_Out_Of_Range_AI", "No Honour will be received, the AI Castle is out of range.") : SK.Text("LaunchAttackPopup_No_Honour_Out_Of_Range_Village", "No Honour will be received, the village is out of range.")) : SK.Text("LaunchAttackPopup_No_Honour_Out_Of_Range_Bandit", "No Honour will be received, the Bandit Camp is out of range.")) : SK.Text("LaunchAttackPopup_No_Honour_Out_Of_Range_Wolf", "No Honour will be received, the Wolf Lair is out of range.")) : SK.Text("LaunchAttackPopup_No_Honour_Out_Of_Range_Stash", "No Honour will be received, the stash is out of range.");
      this.errorLabel.Visible = GameEngine.Instance.World.isScoutHonourOutOfRange(fromVillageID, toVillageID);
      if (special == 15 || special == 17 || SpecialVillageTypes.IS_TREASURE_CASTLE(special))
      {
        this.errorLabel.Visible = true;
        this.errorLabel.Text = SK.Text("LaunchAttackPopup_Paladin_No_Honour", "No honour will be received for destroying this type of AI castle");
      }
      if (!GameEngine.Instance.World.isIslandTravel(this.m_travelFromVillage, this.m_toVillage))
        return;
      int index2 = GameEngine.Instance.World.SpecialSeaConditionsData + 4;
      if (index2 < 0)
        index2 = 0;
      else if (index2 >= 9)
        index2 = 8;
      this.m_lastSeaConditions = index2;
      this.seaConditionsImage.Image = (Image) GFXLibrary.sea_conditions[index2];
      this.seaConditionsImage.Position = new Point(97, 358);
      this.seaConditionsImage.CustomTooltipID = 23000 + index2;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.seaConditionsImage);
    }

    public void changeCommand()
    {
      if (this.ClickedControl == null)
        return;
      this.updateButtons(this.ClickedControl.Data);
    }

    public void updateButtons(int type)
    {
      this.currentCommand = type;
      this.actionButton_Vandalise.ImageNorm = (Image) GFXLibrary.send_army_buttons[1];
      this.actionButton_Vandalise.ImageOver = (Image) GFXLibrary.send_army_buttons[7];
      this.actionButton_Pillage.ImageNorm = (Image) GFXLibrary.send_army_buttons[5];
      this.actionButton_Pillage.ImageOver = (Image) GFXLibrary.send_army_buttons[11];
      this.actionButton_Ransack.ImageNorm = (Image) GFXLibrary.send_army_buttons[2];
      this.actionButton_Ransack.ImageOver = (Image) GFXLibrary.send_army_buttons[8];
      this.actionButton_Raze.ImageNorm = (Image) GFXLibrary.send_army_buttons[4];
      this.actionButton_Raze.ImageOver = (Image) GFXLibrary.send_army_buttons[10];
      this.actionButton_Capture.ImageNorm = (Image) GFXLibrary.send_army_buttons[3];
      this.actionButton_Capture.ImageOver = (Image) GFXLibrary.send_army_buttons[9];
      this.actionButton_GoldRaid.ImageNorm = (Image) GFXLibrary.send_army_buttons[0];
      this.actionButton_GoldRaid.ImageOver = (Image) GFXLibrary.send_army_buttons[6];
      this.gfxImage.Visible = true;
      this.m_selectedPenalty = 0;
      this.sliderImage.Visible = false;
      this.arrowImage.Visible = true;
      this.tooltipLabel.Visible = true;
      this.villageActionLabel.Visible = true;
      this.needCaptainLabel.Visible = false;
      this.honourPenaltyImage.Visible = false;
      this.honourPenaltyLabel.Visible = false;
      this.honourPenaltyValueLabel.Visible = false;
      this.captureCostImage.Visible = false;
      this.captureCostLabel.Visible = false;
      this.captureCostValueLabel.Visible = false;
      switch (type)
      {
        case 0:
          this.actionButton_Vandalise.ImageNorm = (Image) GFXLibrary.send_army_buttons[13];
          this.actionButton_Vandalise.ImageOver = (Image) GFXLibrary.send_army_buttons[19];
          this.launchButton.Enabled = true;
          this.villageActionLabel.Text = this.actionButton_Vandalise.CustomTooltipID != 2100 ? SK.Text("GENERIC_Attack", "Attack") : SK.Text("LaunchAttackPopup_Vandalise", "Vandalise");
          if (SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(this.m_toVillage)))
          {
            this.villageActionLabel.Text = SK.Text("LaunchAttackPopup_Attack_tooltip_treasure_castle", "Attack a Treasure Castle.");
            this.tooltipLabel.Size = new Size(640, 60);
            this.buttonIndentImage.Visible = false;
            this.gfxImage.Position = new Point(120, 77);
            this.tooltipLabel.Text = SK.Text("CastleMap_TC_Message", "Treasure chests are below ground and cannot be seen by troops until they are on an immediately adjacent tile, otherwise they march to the keep as normal.");
          }
          else
            this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Attack_tooltip", "Attack an enemy castle.");
          int penalty1 = 0;
          if (!GameEngine.Instance.World.isCapital(this.m_fromVillage))
          {
            if (this.m_battleHonourData != null)
            {
              this.m_battleHonourData.attackType = 11;
              penalty1 = CastlesCommon.calcBattleHonourCost(this.m_battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset, GameEngine.Instance.LocalWorldData.EraWorld);
            }
            if (penalty1 > 0)
            {
              this.showHonourPenalty(penalty1);
              break;
            }
            break;
          }
          break;
        case 1:
          this.actionButton_Pillage.ImageNorm = (Image) GFXLibrary.send_army_buttons[17];
          this.actionButton_Pillage.ImageOver = (Image) GFXLibrary.send_army_buttons[23];
          this.sliderImage.Visible = true;
          this.launchButton.Enabled = true;
          this.sliderImage.Value = 0;
          this.sliderImage.Max = this.maxPillageValue - 1;
          this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Stockpile", "Stockpile");
          this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[24];
          this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[30];
          this.currentPillageType = 0;
          this.villageActionLabel.Text = SK.Text("GENERIC_Pillage", "Pillage");
          this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Pillage_tooltip", "Steal resources from an enemy.");
          int penalty2 = 0;
          if (!GameEngine.Instance.World.isCapital(this.m_fromVillage))
          {
            if (this.m_battleHonourData != null)
            {
              this.m_battleHonourData.attackType = 2;
              penalty2 = CastlesCommon.calcBattleHonourCost(this.m_battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset, GameEngine.Instance.LocalWorldData.EraWorld);
            }
            if (penalty2 > 0)
            {
              this.showHonourPenalty(penalty2);
              break;
            }
            break;
          }
          break;
        case 2:
          this.actionButton_Ransack.ImageNorm = (Image) GFXLibrary.send_army_buttons[14];
          this.actionButton_Ransack.ImageOver = (Image) GFXLibrary.send_army_buttons[20];
          this.sliderImage.Visible = true;
          this.launchButton.Enabled = true;
          this.sliderImage.Value = 0;
          this.sliderImage.Max = this.maxRansackValue - 1;
          this.sliderHeaderLabel.Text = SK.Text("LaunchAttackPopup_Max_Buildings", "Max Buildings");
          this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[29];
          this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[29];
          this.villageActionLabel.Text = SK.Text("GENERIC_Ransack", "Ransack");
          this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Ransack_tooltip", "Destroy enemy village buildings.");
          int penalty3 = 0;
          if (!GameEngine.Instance.World.isCapital(this.m_fromVillage))
          {
            if (this.m_battleHonourData != null)
            {
              this.m_battleHonourData.attackType = 3;
              penalty3 = CastlesCommon.calcBattleHonourCost(this.m_battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset, GameEngine.Instance.LocalWorldData.EraWorld);
            }
            if (penalty3 > 0)
            {
              this.showHonourPenalty(penalty3);
              break;
            }
            break;
          }
          break;
        case 3:
          this.actionButton_Raze.ImageNorm = (Image) GFXLibrary.send_army_buttons[16];
          this.actionButton_Raze.ImageOver = (Image) GFXLibrary.send_army_buttons[22];
          if (this.noCaptain)
            this.launchButton.Enabled = false;
          else
            this.launchButton.Enabled = true;
          this.needCaptainLabel.Visible = this.noCaptain;
          this.villageActionLabel.Text = SK.Text("GENERIC_Raze", "Raze");
          this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Raze_tooltip", "Completely destroy target.");
          int penalty4 = 0;
          if (!GameEngine.Instance.World.isCapital(this.m_fromVillage))
          {
            if (this.m_battleHonourData != null)
            {
              this.m_battleHonourData.attackType = 9;
              penalty4 = CastlesCommon.calcBattleHonourCost(this.m_battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset, GameEngine.Instance.LocalWorldData.EraWorld);
            }
            if (penalty4 > 0)
            {
              this.showHonourPenalty(penalty4);
              break;
            }
            break;
          }
          break;
        case 4:
          this.actionButton_Capture.ImageNorm = (Image) GFXLibrary.send_army_buttons[15];
          this.actionButton_Capture.ImageOver = (Image) GFXLibrary.send_army_buttons[21];
          if (this.noCaptain)
            this.launchButton.Enabled = false;
          else
            this.launchButton.Enabled = true;
          this.needCaptainLabel.Visible = this.noCaptain;
          this.villageActionLabel.Text = SK.Text("GENERIC_Capture", "Capture");
          if (SpecialVillageTypes.IS_ROYAL_TOWER(GameEngine.Instance.World.getSpecial(this.m_toVillage)))
          {
            this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Capture_tooltip_RT", "Capture Royal Tower.");
            this.tooltipLabel.Size = new Size(640, 60);
            this.buttonIndentImage.Visible = false;
            this.gfxImage.Position = new Point(120, 77);
          }
          else
            this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Capture_tooltip", "Captures enemy village.");
          if (this.m_captureHonourCost > 0)
          {
            this.captureCostLabel.Visible = true;
            this.captureCostValueLabel.Visible = true;
            this.captureCostImage.Visible = true;
          }
          if (!GameEngine.Instance.World.isCapital(this.m_toVillage) && !this.capitalToCapital)
          {
            int penalty5 = 0;
            if (!GameEngine.Instance.World.isCapital(this.m_fromVillage))
            {
              if (this.m_battleHonourData != null)
              {
                this.m_battleHonourData.attackType = !this.m_captureAllowed ? 11 : 1;
                penalty5 = CastlesCommon.calcBattleHonourCost(this.m_battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset, GameEngine.Instance.LocalWorldData.EraWorld);
              }
              if (penalty5 > 0 || this.m_captureHonourCost > 0)
              {
                this.showHonourPenalty(penalty5, this.m_captureHonourCost);
                break;
              }
              break;
            }
            break;
          }
          break;
        case 5:
          this.actionButton_GoldRaid.ImageNorm = (Image) GFXLibrary.send_army_buttons[12];
          this.actionButton_GoldRaid.ImageOver = (Image) GFXLibrary.send_army_buttons[18];
          this.sliderImage.Visible = true;
          this.launchButton.Enabled = true;
          this.sliderImage.Value = 0;
          this.sliderImage.Max = this.maxGoldRaidValue - 1;
          this.sliderHeaderLabel.Text = SK.Text("GENERIC_Gold", "Gold");
          this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[28];
          this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[28];
          this.villageActionLabel.Text = SK.Text("GENERIC_Gold_Raid", "Gold Raid");
          this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Goldraid_tooltip", "Steals gold from capital.");
          break;
      }
      this.tracksMoved();
    }

    private void showHonourPenalty(int penalty)
    {
      if (this.toCapital)
        return;
      NumberFormatInfo nfi = GameEngine.NFI;
      this.honourPenaltyValueLabel.Text = penalty.ToString("N", (IFormatProvider) nfi);
      this.m_selectedPenalty = penalty;
      this.honourPenaltyValueLabel.Visible = true;
      this.honourPenaltyLabel.Visible = true;
      this.honourPenaltyImage.Visible = true;
      this.honourPenaltyValueLabel.Color = Color.FromArgb(18, (int) byte.MaxValue, 0);
      if (penalty <= 0 || GameEngine.Instance.World.getCurrentHonour() > 0.0)
        return;
      this.launchButton.Enabled = false;
      this.honourPenaltyValueLabel.Color = Color.FromArgb((int) byte.MaxValue, 18, 0);
    }

    private void showHonourPenalty(int penalty, int captureCost)
    {
      if (this.toCapital)
        return;
      NumberFormatInfo nfi = GameEngine.NFI;
      this.honourPenaltyValueLabel.Text = penalty.ToString("N", (IFormatProvider) nfi);
      this.m_selectedPenalty = penalty;
      this.honourPenaltyValueLabel.Visible = true;
      this.honourPenaltyLabel.Visible = true;
      this.honourPenaltyImage.Visible = true;
      this.honourPenaltyValueLabel.Color = Color.FromArgb(18, (int) byte.MaxValue, 0);
      this.captureCostValueLabel.Color = Color.FromArgb(18, (int) byte.MaxValue, 0);
      if ((penalty <= 0 || GameEngine.Instance.World.getCurrentHonour() > 0.0) && (captureCost <= 0 || GameEngine.Instance.World.getCurrentHonour() >= (double) captureCost))
        return;
      this.launchButton.Enabled = false;
      this.honourPenaltyValueLabel.Color = Color.FromArgb((int) byte.MaxValue, 18, 0);
      this.captureCostValueLabel.Color = Color.FromArgb((int) byte.MaxValue, 18, 0);
    }

    public void update()
    {
      this.cardbar.update();
      double distance = this.storedPreCardDistance * CardTypes.getArmySpeed(GameEngine.Instance.cardsManager.UserCardData);
      this.timeLabel.TextDiffOnly = VillageMap.createBuildTimeString((int) GameEngine.Instance.World.adjustIfIslandTravel(distance, this.m_travelFromVillage, this.m_toVillage));
      if (this.m_lastSeaConditions == -1)
        return;
      int index = GameEngine.Instance.World.SpecialSeaConditionsData + 4;
      if (index < 0)
        index = 0;
      else if (index >= 9)
        index = 8;
      if (this.m_lastSeaConditions == index)
        return;
      this.m_lastSeaConditions = index;
      this.seaConditionsImage.Image = (Image) GFXLibrary.sea_conditions[index];
      this.seaConditionsImage.CustomTooltipID = 23000 + index;
    }

    private void tracksMoved()
    {
      if (this.currentCommand == 2)
        this.sliderValueLabel.Text = (this.sliderImage.Value + 1).ToString();
      else
        this.sliderValueLabel.Text = (this.sliderImage.Value + 1).ToString() + "%";
    }

    private void sliderClick()
    {
      if (this.currentCommand != 1)
        return;
      ++this.currentPillageType;
      if (this.currentPillageType >= 5)
        this.currentPillageType = 0;
      if (this.currentPillageType == 0)
      {
        this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Stockpile", "Stockpile");
        this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[24];
        this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[30];
      }
      else if (this.currentPillageType == 1)
      {
        this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Granary", "Granary");
        this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[25];
        this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[31];
      }
      else if (this.currentPillageType == 2)
      {
        this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Inn", "Inn");
        this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[34];
        this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[35];
      }
      else if (this.currentPillageType == 3)
      {
        this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Village_Hall", "Village Hall");
        this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[27];
        this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[33];
      }
      else
      {
        if (this.currentPillageType != 4)
          return;
        this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Armoury", "Armoury");
        this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[26];
        this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[32];
      }
    }

    private void villageFavouriteClicked()
    {
      if (AttackTargetsPanel.isFavourite(this.m_toVillage))
      {
        AttackTargetsPanel.removeFavourite(this.m_toVillage);
        this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_3;
        this.targetVillageFavourite.CustomTooltipID = 2018;
      }
      else
      {
        AttackTargetsPanel.addFavourite(this.m_toVillage);
        this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_1;
        this.targetVillageFavourite.CustomTooltipID = 2107;
      }
    }

    private void closeClick()
    {
      GameEngine.Instance.EnableMouseClicks();
      InterfaceMgr.Instance.closeLaunchAttackPopup();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    private void launch()
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      if (this.currentCommand == 4)
      {
        if (this.m_captureAllowed)
        {
          num1 = 1;
          num3 = this.m_captureHonourCost;
        }
        else
          num1 = 11;
      }
      if (this.currentCommand == 5 && this.capitalToCapital)
      {
        num1 = 12;
        num2 = this.sliderImage.Value + 1;
      }
      if (this.currentCommand == 3)
        num1 = 9;
      if (this.currentCommand == 1)
      {
        switch (this.currentPillageType)
        {
          case 1:
            num1 = 4;
            break;
          case 2:
            num1 = 6;
            break;
          case 3:
            num1 = 5;
            break;
          case 4:
            num1 = 7;
            break;
          default:
            num1 = 2;
            break;
        }
        num2 = this.sliderImage.Value + 1;
      }
      if (this.currentCommand == 2)
      {
        num1 = 3;
        num2 = this.sliderImage.Value + 1;
      }
      if (this.currentCommand == 0)
        num1 = 11;
      this.attackTypeRef = num1;
      this.pillageValueRef = num2;
      if (this.m_selectedPenalty > 0 || num3 > 0)
      {
        MessageBoxButtons buts = MessageBoxButtons.YesNo;
        NumberFormatInfo nfi = GameEngine.NFI;
        if (MyMessageBox.Show(SK.Text("LaunchAttackPopup_Penalty_Warning", "This attack will cost you an Honour Penalty.") + Environment.NewLine + SK.Text("GENERIC_Honour_Cost", "Honour Cost") + " : " + (this.m_selectedPenalty + num3).ToString("N", (IFormatProvider) nfi) + Environment.NewLine + SK.Text("LaunchAttackPopup_Continue", "Continue?"), SK.Text("LaunchAttackPopup_Confirm_Attack", "Confirm Attack"), buts) != DialogResult.Yes)
          return;
      }
      this.Attack();
    }

    private void Attack()
    {
      GameEngine.Instance.CastleAttackerSetup.setupLaunchArmy(this.attackTypeRef, this.pillageValueRef, 0);
      GameEngine.Instance.CastleAttackerSetup.launchArmy();
      this.m_parent.launched();
      GameEngine.Instance.EnableMouseClicks();
      InterfaceMgr.Instance.closeLaunchAttackPopup();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }
  }
}
