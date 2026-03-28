// Decompiled with JetBrains decompiler
// Type: Kingdoms.ScoutPopupPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ScoutPopupPanel : CustomSelfDrawPanel
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
    private CustomSelfDrawPanel.CSDButton targetVillageFavourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel scoutingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel numLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel timeLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel scoutHonourLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel scoutCarryingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton launchButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDTrackBar sliderImage = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDImage seaConditionsImage = new CustomSelfDrawPanel.CSDImage();
    private double storedPreCardDistance;
    private int m_selectedVillage = -1;
    private int m_ownVillage = -1;
    private int m_carryLevel;
    private int lastMax = -1;
    private bool sliderEnabled;
    private int lastSliderAmount;
    private int aiworld_Scout_ID_ownVillage = -1;
    private int aiworld_Scout_ID_selectedVillage = -1;
    private int aiworld_Scout_ID_numScouts = -1;
    private bool inLaunch;
    private DateTime lastLaunchTime = DateTime.MinValue;
    private IContainer components;

    public ScoutPopupPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int villageID, bool reset)
    {
      Color white1 = ARGBColors.White;
      Color black = ARGBColors.Black;
      Color white2 = ARGBColors.White;
      this.m_selectedVillage = villageID;
      this.m_ownVillage = InterfaceMgr.Instance.OwnSelectedVillage;
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
      int y1 = 39;
      this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), this.Size);
      this.mainBackgroundImage.Position = new Point(0, y1);
      this.mainBackgroundImage.Size = new Size(this.Size.Width, this.Size.Height - y1);
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundBottomEdge.Image = (Image) GFXLibrary.popup_border_bottom;
      this.backgroundBottomEdge.Position = new Point(0, this.Height - 2);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundBottomEdge);
      this.backgroundRightEdge.Image = (Image) GFXLibrary.popup_border_rhs;
      this.backgroundRightEdge.Position = new Point(this.Width - 2, y1);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundRightEdge);
      this.cardbar.Position = new Point(0, 4);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardbar);
      this.cardbar.init(7);
      this.gfxImage.Image = (Image) GFXLibrary.scout_screen_illustration_01;
      this.gfxImage.Position = new Point(20, 71);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.gfxImage);
      this.sliderImage.Position = new Point(44, 284);
      this.sliderImage.Margin = new Rectangle(32, 63, 32, 25);
      this.sliderImage.Value = 0;
      this.sliderImage.Max = 0;
      this.sliderImage.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.sliderImage);
      this.sliderImage.Create((Image) GFXLibrary.scout_screen_slider, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar);
      this.arrowImage.Image = (Image) GFXLibrary.scout_screen_arrowbox;
      this.arrowImage.Position = new Point(238, 284);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.arrowImage);
      this.scoutingLabel.Text = SK.Text("ScoutPopup_Scouting_Target", "Scouting") + " '" + GameEngine.Instance.World.getVillageNameOrType(villageID) + "'";
      this.scoutingLabel.Color = white1;
      this.scoutingLabel.DropShadowColor = black;
      this.scoutingLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
      this.scoutingLabel.Position = new Point(0, 243);
      this.scoutingLabel.Size = new Size(700, 30);
      this.scoutingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scoutingLabel);
      if (AttackTargetsPanel.isFavourite(this.m_selectedVillage))
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
      this.targetVillageFavourite.Position = new Point(650, 244);
      this.targetVillageFavourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.targetVillageFavourite.Data = 0;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.targetVillageFavourite);
      this.numLabel.Text = "";
      this.numLabel.Color = white1;
      this.numLabel.DropShadowColor = black;
      this.numLabel.Position = new Point(63, 23);
      this.numLabel.Size = new Size(59, 24);
      this.numLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.numLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.sliderImage.addControl((CustomSelfDrawPanel.CSDControl) this.numLabel);
      this.timeLabel.Text = "00:00:00";
      this.timeLabel.Color = white1;
      this.timeLabel.DropShadowColor = black;
      this.timeLabel.Position = new Point(-28, 23);
      this.timeLabel.Size = new Size(191, 24);
      this.timeLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.timeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.arrowImage.addControl((CustomSelfDrawPanel.CSDControl) this.timeLabel);
      int special = GameEngine.Instance.World.getSpecial(villageID);
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
        case 100:
          index1 = HolidayPeriods.xmas(VillageMap.getCurrentServerTime()) ? 59 : 29;
          break;
        case 106:
          index1 = 30;
          break;
        case 107:
          index1 = 31;
          break;
        case 108:
          index1 = 33;
          break;
        case 109:
          index1 = 32;
          break;
        case 112:
          index1 = 34;
          break;
        case 113:
          index1 = 35;
          break;
        case 114:
          index1 = 36;
          break;
        case 115:
          index1 = 41;
          break;
        case 116:
          index1 = 37;
          break;
        case 117:
          index1 = 40;
          break;
        case 118:
          index1 = 42;
          break;
        case 119:
          index1 = 45;
          break;
        case 121:
          index1 = 44;
          break;
        case 122:
          index1 = 38;
          break;
        case 123:
          index1 = 43;
          break;
        case 124:
          index1 = 46;
          break;
        case 125:
          index1 = 47;
          break;
        case 126:
          index1 = 48;
          break;
        case 128:
          index1 = 61;
          break;
        case 129:
          index1 = 60;
          break;
        case 130:
          index1 = 62;
          break;
        case 131:
          index1 = 63;
          break;
        case 132:
          index1 = 64;
          break;
        case 133:
          index1 = 39;
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
          index1 = !GameEngine.Instance.World.isRegionCapital(villageID) ? (!GameEngine.Instance.World.isCountyCapital(villageID) ? (!GameEngine.Instance.World.isProvinceCapital(villageID) ? (!GameEngine.Instance.World.isCountryCapital(villageID) ? GameEngine.Instance.World.getVillageSize(villageID) : 52) : 51) : 50) : 49;
          break;
      }
      this.targetImage.Image = (Image) GFXLibrary.scout_screen_icons[index1];
      this.targetImage.Position = new Point(181, 5);
      this.arrowImage.addControl((CustomSelfDrawPanel.CSDControl) this.targetImage);
      switch (special)
      {
        case 106:
        case 107:
        case 108:
        case 109:
        case 112:
        case 113:
        case 114:
        case 115:
        case 116:
        case 117:
        case 118:
        case 119:
        case 121:
        case 122:
        case 123:
        case 124:
        case 125:
        case 126:
        case 128:
        case 129:
        case 130:
        case 131:
        case 132:
        case 133:
          WorldMap.SpecialVillageCache specialVillageData = GameEngine.Instance.World.getSpecialVillageData(villageID, false);
          if (specialVillageData != null)
          {
            NumberFormatInfo nfi = GameEngine.NFI;
            CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel();
            control.Text = specialVillageData.resourceLevel.ToString("N", (IFormatProvider) nfi);
            control.Position = new Point(158, 85);
            control.Size = new Size(150, 20);
            control.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            control.Color = white1;
            control.DropShadowColor = black;
            control.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.arrowImage.addControl((CustomSelfDrawPanel.CSDControl) control);
            int resourceCarryLevel = GameEngine.Instance.LocalWorldData.ScoutResourceCarryLevel;
            int researchForaging = (int) GameEngine.Instance.World.UserResearchData.Research_Foraging;
            int num = CardTypes.adjustForagingLevel(GameEngine.Instance.cardsManager.UserCardData, resourceCarryLevel) * ResearchData.foragingResearch[researchForaging] / 2;
            switch (special)
            {
              case 119:
              case 121:
              case 122:
              case 123:
              case 124:
              case 125:
              case 126:
              case 128:
              case 129:
              case 130:
              case 131:
              case 132:
              case 133:
                num /= 10;
                break;
            }
            this.m_carryLevel = num;
            this.scoutCarryingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.scoutCarryingLabel.Color = white1;
            this.scoutCarryingLabel.DropShadowColor = black;
            this.scoutCarryingLabel.Text = this.m_carryLevel.ToString("N", (IFormatProvider) nfi);
            this.scoutCarryingLabel.Position = new Point(0, 90);
            this.scoutCarryingLabel.Size = new Size(this.sliderImage.Width, 20);
            this.scoutCarryingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.sliderImage.addControl((CustomSelfDrawPanel.CSDControl) this.scoutCarryingLabel);
            break;
          }
          break;
      }
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      Point villageLocation1 = GameEngine.Instance.World.getVillageLocation(InterfaceMgr.Instance.OwnSelectedVillage);
      Point villageLocation2 = GameEngine.Instance.World.getVillageLocation(villageID);
      int x1 = villageLocation1.X;
      int y2 = villageLocation1.Y;
      int x2 = villageLocation2.X;
      int y3 = villageLocation2.Y;
      double distance = Math.Sqrt((double) ((x1 - x2) * (x1 - x2) + (y2 - y3) * (y2 - y3))) * (localWorldData.ScoutsMoveSpeed * localWorldData.gamePlaySpeed * ResearchData.ScoutTimes[(int) GameEngine.Instance.World.UserResearchData.Research_Horsemanship]);
      this.storedPreCardDistance = distance;
      double secsLeft = GameEngine.Instance.World.adjustIfIslandTravel(distance, this.m_ownVillage, this.m_selectedVillage) * CardTypes.getScoutSpeed(GameEngine.Instance.cardsManager.UserCardData);
      this.timeLabel.Text = VillageMap.createBuildTimeString((int) secsLeft);
      this.timeLabel.CustomTooltipID = 20000;
      this.timeLabel.CustomTooltipData = (int) secsLeft;
      this.launchButton.ImageNorm = (Image) GFXLibrary.button_with_inset_normal;
      this.launchButton.ImageOver = (Image) GFXLibrary.button_with_inset_over;
      this.launchButton.ImageClick = (Image) GFXLibrary.button_with_inset_pushed;
      this.launchButton.Position = new Point(520, 324);
      this.launchButton.Text.Text = SK.Text("ScoutPopup_Go", "Go");
      this.launchButton.Text.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
      this.launchButton.TextYOffset = 1;
      this.launchButton.Text.Color = ARGBColors.Black;
      this.launchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.launch), "ScoutPopupPanel_launch");
      this.launchButton.Enabled = false;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.launchButton);
      this.scoutHonourLabel.Text = "";
      this.scoutHonourLabel.Visible = false;
      this.scoutHonourLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.scoutHonourLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.scoutHonourLabel.Color = white1;
      this.scoutHonourLabel.DropShadowColor = black;
      this.scoutHonourLabel.Position = new Point(0, 410);
      this.scoutHonourLabel.Size = new Size(700, 30);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scoutHonourLabel);
      this.scoutHonourLabel.Text = special < 100 || special > 199 ? (special != 5 ? (special != 3 ? (special == 7 || special == 9 || special == 11 || special == 13 ? SK.Text("ScoutPopup_No_Honour_AI_castle_Out_Of_Range", "No Honour will be received, the AI Castle is out of range.") : (special == 15 || special == 17 || SpecialVillageTypes.IS_TREASURE_CASTLE(special) ? SK.Text("LaunchAttackPopup_Paladin_No_Honour", "No honour will be received for destroying this type of AI castle") : SK.Text("ScoutPopup_No_Honour_Village_Out_Of_Range", "No Honour will be received, the village is out of range."))) : SK.Text("ScoutPopup_No_Honour_Bandit_Camp_Out_Of_Range", "No Honour will be received, the Bandit Camp is out of range.")) : SK.Text("ScoutPopup_No_Honour_Wolf_Lair_Out_Of_Range", "No Honour will be received, the Wolf Lair is out of range.")) : SK.Text("ScoutPopup_No_Honour_Stash_Out_Of_Range", "No Honour will be received, the stash is out of range.");
      this.scoutHonourLabel.Visible = GameEngine.Instance.World.isScoutHonourOutOfRange(InterfaceMgr.Instance.OwnSelectedVillage, villageID) && (special <= 100 || special > 199);
      this.titleImage.Image = (Image) GFXLibrary.popup_title_bar;
      this.titleImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.titleImage);
      this.titleLabel.Text = SK.Text("OwnVillagePanel_Send_Out_Scouts", "Send Out Scouts");
      this.titleLabel.Color = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.titleLabel.DropShadowColor = black;
      this.titleLabel.Position = new Point(20, 5);
      this.titleLabel.Size = new Size(this.Width, 32);
      this.titleLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
      this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.titleImage.addControl((CustomSelfDrawPanel.CSDControl) this.titleLabel);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "ScoutPopupPanel_close");
      this.closeButton.Position = new Point(659, 5);
      this.titleImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      if (GameEngine.Instance.World.isIslandTravel(this.m_selectedVillage, this.m_ownVillage))
      {
        int index2 = GameEngine.Instance.World.SpecialSeaConditionsData + 4;
        if (index2 < 0)
          index2 = 0;
        else if (index2 >= 9)
          index2 = 8;
        this.seaConditionsImage.Image = (Image) GFXLibrary.sea_conditions[index2];
        this.seaConditionsImage.Position = new Point(290, 340);
        this.seaConditionsImage.CustomTooltipID = 23000 + index2;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.seaConditionsImage);
      }
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.titleImage, 34, new Point(609, 5));
      if (GameEngine.Instance.getVillage(this.m_ownVillage) != null)
        this.onVillageLoadUpdate(this.m_ownVillage, true);
      else
        GameEngine.Instance.downloadCurrentVillage();
    }

    public void onVillageLoadUpdate(int villageID, bool initial)
    {
      if (this.inLaunch || this.m_ownVillage != villageID || GameEngine.Instance.getVillage(this.m_ownVillage) == null)
        return;
      VillageMap village = GameEngine.Instance.getVillage(this.m_ownVillage);
      if (initial)
      {
        if (village.m_numScouts > 0)
        {
          this.launchButton.Enabled = true;
          this.sliderImage.Max = village.m_numScouts - 1;
          this.sliderImage.Value = village.m_numScouts - 1;
          this.sliderEnabled = true;
        }
        else
        {
          this.sliderImage.Value = 0;
          this.sliderImage.Max = 0;
          this.sliderEnabled = false;
        }
        this.Invalidate();
        this.tracksMoved();
      }
      else if (village.m_numScouts != this.lastMax)
      {
        if (village.m_numScouts > this.lastMax)
        {
          this.sliderImage.Max = village.m_numScouts - 1;
          if (this.lastMax == 0)
            this.sliderImage.Value = village.m_numScouts - 1;
        }
        else if (this.sliderImage.Value + 1 > village.m_numScouts)
        {
          this.sliderImage.Value = village.m_numScouts - 1;
          this.sliderImage.Max = village.m_numScouts - 1;
        }
        else
          this.sliderImage.Max = village.m_numScouts - 1;
        if (village.m_numScouts == 0)
          this.launchButton.Enabled = false;
        else
          this.launchButton.Enabled = true;
        this.sliderEnabled = this.launchButton.Enabled;
        this.Invalidate();
        this.tracksMoved();
      }
      this.lastMax = village.m_numScouts;
    }

    public void update()
    {
      this.cardbar.update();
      this.onVillageLoadUpdate(this.m_ownVillage, false);
      this.numLabel.Text = this.numLabel.Text;
      double distance = this.storedPreCardDistance * CardTypes.getScoutSpeed(GameEngine.Instance.cardsManager.UserCardData);
      double secsLeft = GameEngine.Instance.World.adjustIfIslandTravel(distance, this.m_ownVillage, this.m_selectedVillage);
      if ((int) secsLeft == this.timeLabel.CustomTooltipData)
        return;
      this.timeLabel.Text = VillageMap.createBuildTimeString((int) secsLeft);
      this.timeLabel.CustomTooltipID = 20000;
      this.timeLabel.CustomTooltipData = (int) secsLeft;
      int index = GameEngine.Instance.World.SpecialSeaConditionsData + 4;
      if (index < 0)
        index = 0;
      else if (index >= 9)
        index = 8;
      this.seaConditionsImage.Image = (Image) GFXLibrary.sea_conditions[index];
      this.seaConditionsImage.CustomTooltipID = 23000 + index;
    }

    private void tracksMoved()
    {
      if (this.sliderEnabled)
      {
        this.numLabel.Text = (this.sliderImage.Value + 1).ToString();
        this.scoutCarryingLabel.Text = (this.m_carryLevel * (this.sliderImage.Value + 1)).ToString("N", (IFormatProvider) GameEngine.NFI);
      }
      else
      {
        this.numLabel.Text = "0";
        this.scoutCarryingLabel.Text = "";
      }
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_selectedVillage, false, true, false, false);
      InterfaceMgr.Instance.closeScoutPopupWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    private void launch()
    {
      if (!this.sliderEnabled || this.inLaunch && (DateTime.Now - this.lastLaunchTime).TotalSeconds < 20.0)
        return;
      this.inLaunch = true;
      this.lastLaunchTime = DateTime.Now;
      int numScouts = this.sliderImage.Value + 1;
      this.aiworld_Scout_ID_ownVillage = this.m_ownVillage;
      this.aiworld_Scout_ID_selectedVillage = this.m_selectedVillage;
      this.aiworld_Scout_ID_numScouts = numScouts;
      RemoteServices.Instance.set_SendScouts_UserCallBack(new RemoteServices.SendScouts_UserCallBack(this.sendScoutsCallback));
      RemoteServices.Instance.SendScouts(this.m_ownVillage, this.m_selectedVillage, numScouts);
      AllVillagesPanel.travellersChanged();
      GameEngine.Instance.getVillage(this.m_ownVillage)?.addTroops(0, 0, 0, 0, 0, -numScouts);
      this.launchButton.Enabled = false;
      this.closeButton.Enabled = false;
      CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, this.ParentForm);
    }

    public void cancelInterdictionCallback(CancelInterdiction_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.inLaunch = true;
      this.closeButton.Enabled = false;
      RemoteServices.Instance.SendScouts(this.aiworld_Scout_ID_ownVillage, this.aiworld_Scout_ID_selectedVillage, this.aiworld_Scout_ID_numScouts);
    }

    public void sendScoutsCallback(SendScouts_ReturnType returnData)
    {
      CursorManager.SetCursor(CursorManager.CursorType.Default, this.ParentForm);
      this.inLaunch = false;
      if (!returnData.Success && returnData.m_errorCode == ErrorCodes.ErrorCode.ATTACKING_VILLAGE_INTERDICT_PROTECTED)
      {
        MessageBoxButtons buts = MessageBoxButtons.YesNo;
        if (MyMessageBox.Show(SK.Text("GameEngine_Currently_Interdited", "You are currently Interdiction protected") + "\n" + SK.Text("GameEngine_CancelProtection", "Do you wish to cancel this protection?"), SK.Text("GENERIC_Protected", "You Are Protected"), buts) == DialogResult.Yes)
        {
          RemoteServices.Instance.set_CancelInterdiction_UserCallBack(new RemoteServices.CancelInterdiction_UserCallBack(this.cancelInterdictionCallback));
          RemoteServices.Instance.CancelInterdiction(-returnData.sourceVillage);
        }
        else
        {
          if (returnData.numScoutsNotTaken > 0)
            GameEngine.Instance.getVillage(returnData.sourceVillage)?.addTroops(0, 0, 0, 0, 0, returnData.numScoutsNotTaken);
          InterfaceMgr.Instance.closeScoutPopupWindow();
          InterfaceMgr.Instance.getMainTabBar().changeTab(9);
          InterfaceMgr.Instance.getMainTabBar().changeTab(0);
        }
      }
      else
      {
        this.closeButton.Enabled = true;
        if (returnData.Success)
        {
          ArmyReturnData[] armyReturnData = new ArmyReturnData[1]
          {
            returnData.armyData
          };
          GameEngine.Instance.World.doGetArmyData((IEnumerable<ArmyReturnData>) armyReturnData, (IEnumerable<ArmyReturnData>) null, false);
          GameEngine.Instance.World.addExistingArmy(returnData.armyData.armyID);
          InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_ownVillage, false, false, false, false);
          InterfaceMgr.Instance.closeScoutPopupWindow();
          if (returnData.cardData != null)
            GameEngine.Instance.cardsManager.UserCardData = returnData.cardData;
          AttackTargetsPanel.addRecent(returnData.targetVillage);
        }
        if (returnData.numScoutsNotTaken > 0)
        {
          GameEngine.Instance.getVillage(returnData.sourceVillage)?.addTroops(0, 0, 0, 0, 0, returnData.numScoutsNotTaken);
          if (!returnData.Success)
            this.launchButton.Enabled = false;
        }
        if (!returnData.Success)
          return;
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_selectedVillage, false, true, false, false);
        InterfaceMgr.Instance.closeScoutPopupWindow();
      }
    }

    private void villageFavouriteClicked()
    {
      if (AttackTargetsPanel.isFavourite(this.m_selectedVillage))
      {
        AttackTargetsPanel.removeFavourite(this.m_selectedVillage);
        this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_3;
        this.targetVillageFavourite.CustomTooltipID = 2018;
      }
      else
      {
        AttackTargetsPanel.addFavourite(this.m_selectedVillage);
        this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_1;
        this.targetVillageFavourite.CustomTooltipID = 2107;
      }
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
