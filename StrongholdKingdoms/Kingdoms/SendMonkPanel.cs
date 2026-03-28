// Decompiled with JetBrains decompiler
// Type: Kingdoms.SendMonkPanel
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
  public class SendMonkPanel : CustomSelfDrawPanel
  {
    private IContainer components;
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
    private CustomSelfDrawPanel.CSDImage influenceIndent = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel villageActionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel numLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel timeLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel tooltipLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel costLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel costValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton launchButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDTrackBar sliderImage = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDButton actionButton1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton2 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton3 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton4 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton6 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton actionButton7 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea scrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDButton closeInfluenceButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton positiveButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton negativeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel influenceHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage seaConditionsImage = new CustomSelfDrawPanel.CSDImage();
    private int m_selectedVillage = -1;
    private int m_ownVillage = -1;
    private bool targetCapital;
    private bool excommunicated;
    private int targetUserRank = -1;
    private DateTime excommunicationTime = DateTime.MinValue;
    private int voteCap = 100000;
    private int votedUser = -1;
    private int currentPointsCost;
    private bool positiveInfluence = true;
    private List<ParishMember> parishMembers = new List<ParishMember>();
    private int maxMonks;
    private double storedPreCardDistance;
    private int lastMax = -1;
    private bool sliderEnabled;
    private bool launchAllowed;
    private int currentCommand = -1;
    private bool inLaunch;
    private DateTime lastLaunchTime = DateTime.MinValue;
    private List<SendMonkPanel.MonkVoteLine> lineList = new List<SendMonkPanel.MonkVoteLine>();
    private SendMonkPanel.ParishMemberComparer parishMemberComparer = new SendMonkPanel.ParishMemberComparer();

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

    public SendMonkPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int villageID)
    {
      this.m_selectedVillage = villageID;
      this.m_ownVillage = InterfaceMgr.Instance.OwnSelectedVillage;
      this.clearControls();
      int y1 = 39;
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
      this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), this.Size);
      this.mainBackgroundImage.Position = new Point(0, y1);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundBottomEdge.Image = (Image) GFXLibrary.popup_border_bottom;
      this.backgroundBottomEdge.Position = new Point(0, this.Height - 2);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundBottomEdge);
      this.backgroundRightEdge.Image = (Image) GFXLibrary.popup_border_rhs;
      this.backgroundRightEdge.Position = new Point(this.Width - 2, y1);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundRightEdge);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(659, 5);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "SendMonkPanel_close");
      this.titleImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.titleImage, 35, new Point(609, 5));
      this.cardbar.Position = new Point(0, 4);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardbar);
      this.cardbar.init(8);
      this.gfxImage.Image = (Image) GFXLibrary.illustration_monks;
      this.gfxImage.Position = new Point(25, 77);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.gfxImage);
      this.sliderImage.Position = new Point(37, 304);
      this.sliderImage.Margin = new Rectangle(32, 63, 32, 25);
      this.sliderImage.Value = 0;
      this.sliderImage.Max = 0;
      this.sliderImage.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.sliderImage);
      this.sliderImage.Create((Image) GFXLibrary.monk_screen_slider, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar);
      this.arrowImage.Image = (Image) GFXLibrary.scout_screen_arrowbox;
      this.arrowImage.Position = new Point(219, 304);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.arrowImage);
      this.buttonIndentImage.Image = (Image) GFXLibrary.monk_screen_buttongroup_inset;
      this.buttonIndentImage.Position = new Point(503, 77);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.buttonIndentImage);
      this.influenceIndent.Image = (Image) GFXLibrary.monk_screen_playerlist_inset;
      this.influenceIndent.Position = new Point(25, 77);
      this.influenceIndent.Visible = false;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.influenceIndent);
      this.villageActionLabel.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
      this.villageActionLabel.Color = ARGBColors.White;
      this.villageActionLabel.DropShadowColor = ARGBColors.Black;
      this.villageActionLabel.Position = new Point(36, 243);
      this.villageActionLabel.Size = new Size(430, 30);
      this.villageActionLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.villageActionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageActionLabel);
      this.tooltipLabel.Text = "";
      this.tooltipLabel.Color = ARGBColors.White;
      this.tooltipLabel.DropShadowColor = ARGBColors.Black;
      this.tooltipLabel.Position = new Point(36, 270);
      this.tooltipLabel.Size = new Size(430, 32);
      this.tooltipLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tooltipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tooltipLabel);
      this.costLabel.Text = SK.Text("SendMonksPanel_Faith_Points_Cost", "Faith Points Cost");
      this.costLabel.Color = ARGBColors.White;
      this.costLabel.DropShadowColor = ARGBColors.Black;
      this.costLabel.Position = new Point(452, 358);
      this.costLabel.Size = new Size(180, 32);
      this.costLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.costLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.costLabel);
      this.costValueLabel.Text = "0";
      this.costValueLabel.Color = Color.FromArgb(18, (int) byte.MaxValue, 0);
      this.costValueLabel.DropShadowColor = ARGBColors.Black;
      this.costValueLabel.Position = new Point(635, 358);
      this.costValueLabel.Size = new Size(60, 32);
      this.costValueLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.costValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.costValueLabel);
      this.numLabel.Text = "";
      this.numLabel.Color = ARGBColors.White;
      this.numLabel.DropShadowColor = ARGBColors.Black;
      this.numLabel.Position = new Point(63, 23);
      this.numLabel.Size = new Size(59, 24);
      this.numLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.numLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.sliderImage.addControl((CustomSelfDrawPanel.CSDControl) this.numLabel);
      this.timeLabel.Text = "00:00:00";
      this.timeLabel.Color = ARGBColors.White;
      this.timeLabel.DropShadowColor = ARGBColors.Black;
      this.timeLabel.Position = new Point(-28, 23);
      this.timeLabel.Size = new Size(191, 24);
      this.timeLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.timeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.arrowImage.addControl((CustomSelfDrawPanel.CSDControl) this.timeLabel);
      this.updateButtons(-1);
      this.actionButton1.Position = new Point(48, 4);
      this.actionButton1.Data = 2;
      this.actionButton1.CustomTooltipID = 2000;
      this.actionButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_influence");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton1);
      this.actionButton2.Position = new Point(14, 62);
      this.actionButton2.Data = 4;
      this.actionButton2.CustomTooltipID = 2003;
      this.actionButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_interdicts");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton2);
      this.actionButton3.Position = new Point(88, 62);
      this.actionButton3.Data = 5;
      this.actionButton3.CustomTooltipID = 2004;
      this.actionButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_restoration");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton3);
      this.actionButton4.Position = new Point(14, 129);
      this.actionButton4.Data = 1;
      this.actionButton4.CustomTooltipID = 2001;
      this.actionButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_blessing");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton4);
      this.actionButton5.Position = new Point(88, 129);
      this.actionButton5.Data = 3;
      this.actionButton5.CustomTooltipID = 2002;
      this.actionButton5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_inquistion");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton5);
      this.actionButton6.Position = new Point(14, 196);
      this.actionButton6.Data = 6;
      this.actionButton6.CustomTooltipID = 2005;
      this.actionButton6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_absolution");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton6);
      this.actionButton7.Position = new Point(88, 196);
      this.actionButton7.Data = 7;
      this.actionButton7.CustomTooltipID = 2006;
      this.actionButton7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_excommunication");
      this.buttonIndentImage.addControl((CustomSelfDrawPanel.CSDControl) this.actionButton7);
      int index1;
      switch (GameEngine.Instance.World.getSpecial(villageID))
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
          index1 = 29;
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
      this.scrollArea.Position = new Point(25, 36);
      this.scrollArea.Size = new Size(385, 300);
      this.scrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(385, 300));
      this.influenceIndent.addControl((CustomSelfDrawPanel.CSDControl) this.scrollArea);
      this.mouseWheelOverlay.Position = this.scrollArea.Position;
      this.mouseWheelOverlay.Size = this.scrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.influenceIndent.addControl(this.mouseWheelOverlay);
      this.scrollBar.Position = new Point(423, 47);
      this.scrollBar.Size = new Size(32, 288);
      this.influenceIndent.addControl((CustomSelfDrawPanel.CSDControl) this.scrollBar);
      this.scrollBar.Value = 0;
      this.scrollBar.Max = 0;
      this.scrollBar.NumVisibleLines = 300;
      this.scrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.scroll_thumb_top, (Image) GFXLibrary.scroll_thumb_mid, (Image) GFXLibrary.scroll_thumb_bottom);
      this.scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.scrollBarMoved));
      this.closeInfluenceButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeInfluenceButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeInfluenceButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeInfluenceButton.Position = new Point(415, 1);
      this.closeInfluenceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeInfluenceClick), "SendMonkPanel_close_influence");
      this.influenceIndent.addControl((CustomSelfDrawPanel.CSDControl) this.closeInfluenceButton);
      this.positiveButton.ImageNorm = (Image) GFXLibrary.monk_screen_button_array[0];
      this.positiveButton.ImageOver = (Image) GFXLibrary.monk_screen_button_array[2];
      this.positiveButton.ImageClick = (Image) GFXLibrary.monk_screen_button_array[4];
      this.positiveButton.Position = new Point(350, 6);
      this.positiveButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.positiveClick), "SendMonkPanel_influence_positive");
      this.influenceIndent.addControl((CustomSelfDrawPanel.CSDControl) this.positiveButton);
      this.negativeButton.ImageNorm = (Image) GFXLibrary.monk_screen_button_array[1];
      this.negativeButton.ImageOver = (Image) GFXLibrary.monk_screen_button_array[3];
      this.negativeButton.ImageClick = (Image) GFXLibrary.monk_screen_button_array[5];
      this.negativeButton.Position = new Point(380, 6);
      this.negativeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.negativeClick), "SendMonkPanel_influence_negative");
      this.influenceIndent.addControl((CustomSelfDrawPanel.CSDControl) this.negativeButton);
      this.influenceHeaderLabel.Text = SK.Text("SendMonksPanel_Select_positive", "Select Player to Positively Influence");
      this.influenceHeaderLabel.Color = ARGBColors.White;
      this.influenceHeaderLabel.DropShadowColor = ARGBColors.Black;
      this.influenceHeaderLabel.Position = new Point(15, 4);
      this.influenceHeaderLabel.Size = new Size(338, 28);
      this.influenceHeaderLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.influenceHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.influenceIndent.addControl((CustomSelfDrawPanel.CSDControl) this.influenceHeaderLabel);
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      Point villageLocation1 = GameEngine.Instance.World.getVillageLocation(InterfaceMgr.Instance.OwnSelectedVillage);
      Point villageLocation2 = GameEngine.Instance.World.getVillageLocation(villageID);
      int x1 = villageLocation1.X;
      int y2 = villageLocation1.Y;
      int x2 = villageLocation2.X;
      int y3 = villageLocation2.Y;
      double time = Math.Sqrt((double) ((x1 - x2) * (x1 - x2) + (y2 - y3) * (y2 - y3))) * (GameEngine.Instance.LocalWorldData.PriestMoveSpeed * GameEngine.Instance.LocalWorldData.gamePlaySpeed);
      double num = GameEngine.Instance.World.UserResearchData.adjustPriestTimes(time);
      this.storedPreCardDistance = num;
      double distance = num * CardTypes.adjustMonkSpeed(GameEngine.Instance.cardsManager.UserCardData);
      double secsLeft = GameEngine.Instance.World.adjustIfIslandTravel(distance, this.m_ownVillage, this.m_selectedVillage);
      this.timeLabel.Text = VillageMap.createBuildTimeString((int) secsLeft);
      this.timeLabel.CustomTooltipID = 20000;
      this.timeLabel.CustomTooltipData = (int) secsLeft;
      this.launchButton.ImageNorm = (Image) GFXLibrary.button_with_inset_normal;
      this.launchButton.ImageOver = (Image) GFXLibrary.button_with_inset_over;
      this.launchButton.ImageClick = (Image) GFXLibrary.button_with_inset_pushed;
      this.launchButton.Position = new Point(520, 377);
      this.launchButton.Text.Text = SK.Text("ScoutPopup_Go", "Go");
      this.launchButton.Text.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
      this.launchButton.TextYOffset = 1;
      this.launchButton.Text.Color = ARGBColors.Black;
      this.launchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.launch), "SendMonkPanel_launch");
      this.launchButton.Enabled = false;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.launchButton);
      this.targetCapital = false;
      bool flag1 = GameEngine.Instance.World.isCapital(this.m_selectedVillage);
      bool flag2 = false;
      if (flag1)
      {
        this.targetCapital = true;
        if (GameEngine.Instance.World.isRegionCapital(this.m_selectedVillage))
          flag2 = true;
      }
      if (flag1)
      {
        if (GameEngine.Instance.World.UserResearchData.Research_Confirmation > (byte) 0 && flag2)
          this.actionButton5.Enabled = true;
        else
          this.actionButton5.Enabled = false;
        if (GameEngine.Instance.World.UserResearchData.Research_Marriage > (byte) 0 && flag2)
          this.actionButton4.Enabled = true;
        else
          this.actionButton4.Enabled = false;
        if (GameEngine.Instance.World.UserResearchData.Research_Baptism > (byte) 0 && flag2)
          this.actionButton3.Enabled = true;
        else
          this.actionButton3.Enabled = false;
        if (GameEngine.Instance.World.UserResearchData.Research_Ordination > (byte) 0 && (flag2 || (GameEngine.Instance.World.SecondAgeWorld || GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1) && GameEngine.Instance.World.isCountyCapital(this.m_selectedVillage)))
          this.actionButton1.Enabled = true;
        else
          this.actionButton1.Enabled = false;
        this.actionButton6.Enabled = false;
        this.actionButton7.Enabled = false;
      }
      else
      {
        this.actionButton5.Enabled = false;
        this.actionButton4.Enabled = false;
        this.actionButton3.Enabled = false;
        this.actionButton1.Enabled = false;
        if (GameEngine.Instance.World.UserResearchData.Research_Confession > (byte) 0 && this.m_ownVillage != this.m_selectedVillage)
          this.actionButton6.Enabled = true;
        else
          this.actionButton6.Enabled = false;
        if (GameEngine.Instance.World.UserResearchData.Research_ExtremeUnction > (byte) 0 && this.m_ownVillage != this.m_selectedVillage)
          this.actionButton7.Enabled = true;
        else
          this.actionButton7.Enabled = false;
      }
      if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        this.actionButton2.Enabled = false;
      else if (GameEngine.Instance.World.UserResearchData.Research_Eucharist > (byte) 0)
        this.actionButton2.Enabled = true;
      else
        this.actionButton2.Enabled = false;
      this.titleImage.Image = (Image) GFXLibrary.popup_title_bar;
      this.titleImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.titleImage);
      this.titleLabel.Text = SK.Text("GENERIC_Send_Monks", "Send Monks");
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
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "SendMonkPanel_close");
      this.titleImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      if (GameEngine.Instance.World.isIslandTravel(this.m_selectedVillage, this.m_ownVillage))
      {
        int index2 = GameEngine.Instance.World.SpecialSeaConditionsData + 4;
        if (index2 < 0)
          index2 = 0;
        else if (index2 >= 9)
          index2 = 8;
        this.seaConditionsImage.Image = (Image) GFXLibrary.sea_conditions[index2];
        this.seaConditionsImage.Position = new Point(269, 360);
        this.seaConditionsImage.CustomTooltipID = 23000 + index2;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.seaConditionsImage);
      }
      RemoteServices.Instance.set_GetExcommunicationStatus_UserCallBack(new RemoteServices.GetExcommunicationStatus_UserCallBack(this.getExcommunicationStatusCallback));
      RemoteServices.Instance.GetExcommunicationStatus(this.m_ownVillage, this.m_selectedVillage);
      if (flag1)
      {
        if (GameEngine.Instance.World.isRegionCapital(this.m_selectedVillage))
        {
          RemoteServices.Instance.set_GetParishMembersList_UserCallBack(new RemoteServices.GetParishMembersList_UserCallBack(this.getParishMembersListCallback));
          RemoteServices.Instance.GetParishMembersList(this.m_selectedVillage);
        }
        else if (GameEngine.Instance.World.isCountyCapital(this.m_selectedVillage))
        {
          RemoteServices.Instance.set_GetCountyElectionInfo_UserCallBack(new RemoteServices.GetCountyElectionInfo_UserCallBack(this.getCountyElectionInfoCallback));
          RemoteServices.Instance.GetCountyElectionInfo(this.m_selectedVillage);
        }
      }
      if (GameEngine.Instance.getVillage(this.m_ownVillage) != null)
        this.onVillageLoadUpdate(this.m_ownVillage, true);
      else
        GameEngine.Instance.downloadCurrentVillage();
    }

    public void onVillageLoadUpdate(int villageID, bool initial)
    {
      if (this.inLaunch || this.m_ownVillage != villageID || GameEngine.Instance.getVillage(this.m_ownVillage) == null)
        return;
      int athome = 0;
      GameEngine.Instance.World.countVillagePeople(this.m_ownVillage, 4, ref athome);
      if (!GameEngine.Instance.World.userResearchData.canCreateMonks())
        athome = 0;
      this.maxMonks = athome;
      if (initial)
      {
        if (athome > 0)
        {
          if (!this.excommunicated)
          {
            this.launchButton.Enabled = true;
            this.launchAllowed = true;
          }
          else
            this.launchButton.Enabled = false;
          this.sliderImage.Max = athome - 1;
          this.sliderImage.Value = 0;
          this.sliderEnabled = true;
        }
        else
        {
          this.sliderImage.Value = 0;
          this.sliderImage.Max = 0;
          this.sliderEnabled = false;
          this.launchButton.Enabled = false;
        }
        this.Invalidate();
        this.tracksMoved();
      }
      else if (athome != this.lastMax)
      {
        if (athome > this.lastMax)
        {
          this.sliderImage.Max = athome - 1;
          if (this.lastMax <= 0)
            this.sliderImage.Value = athome - 1;
        }
        else if (this.sliderImage.Value + 1 > athome)
        {
          this.sliderImage.Value = athome - 1;
          this.sliderImage.Max = athome - 1;
        }
        else
          this.sliderImage.Max = athome - 1;
        if (athome == 0 || this.excommunicated)
        {
          this.launchButton.Enabled = false;
        }
        else
        {
          this.launchButton.Enabled = true;
          this.launchAllowed = true;
        }
        this.sliderEnabled = this.launchButton.Enabled;
        this.Invalidate();
        this.tracksMoved();
      }
      this.lastMax = athome;
      this.addPlayers();
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
      this.actionButton1.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[0];
      this.actionButton1.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[7];
      this.actionButton2.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[1];
      this.actionButton2.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[8];
      this.actionButton3.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[2];
      this.actionButton3.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[9];
      this.actionButton4.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[3];
      this.actionButton4.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[10];
      this.actionButton5.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[4];
      this.actionButton5.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[11];
      this.actionButton6.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[5];
      this.actionButton6.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[12];
      this.actionButton7.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[6];
      this.actionButton7.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[13];
      bool visible = this.influenceIndent.Visible;
      this.influenceIndent.Visible = false;
      this.gfxImage.Visible = true;
      this.sliderImage.Visible = true;
      this.arrowImage.Visible = true;
      this.tooltipLabel.Visible = true;
      this.villageActionLabel.Visible = true;
      switch (type)
      {
        case 1:
          this.actionButton4.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[17];
          this.actionButton4.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[24];
          this.villageActionLabel.Text = SK.Text("VillageMapPanel_Blessing", "Blessing") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
          break;
        case 2:
        case 8:
          this.actionButton1.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[14];
          this.actionButton1.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[21];
          if (this.currentCommand != 2 && this.currentCommand != 8 || !visible)
          {
            this.influenceIndent.Visible = true;
            this.gfxImage.Visible = false;
            this.sliderImage.Visible = false;
            this.arrowImage.Visible = false;
            this.tooltipLabel.Visible = false;
            this.villageActionLabel.Visible = false;
          }
          if (this.positiveInfluence)
          {
            this.villageActionLabel.Text = SK.Text("SendMonksPanel_Positive_Influence", "Positive Influence") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
            this.influenceHeaderLabel.Text = SK.Text("SendMonksPanel_Select_positive", "Select Player to Positively Influence");
          }
          else
          {
            this.villageActionLabel.Text = SK.Text("SendMonksPanel_Negative_Influencs", "Negative Influence") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
            this.influenceHeaderLabel.Text = SK.Text("SendMonksPanel_Select_negative", "Select Player to Negatively Influence");
          }
          int num1 = this.sliderImage.Value + 1;
          if (this.maxMonks == 0)
            num1 = 0;
          int num2 = CardTypes.getInfluenceMultipier(GameEngine.Instance.cardsManager.UserCardData) * num1;
          CustomSelfDrawPanel.CSDLabel influenceHeaderLabel = this.influenceHeaderLabel;
          influenceHeaderLabel.Text = influenceHeaderLabel.Text + " (" + num2.ToString() + " ";
          if (num2 != 1)
            this.influenceHeaderLabel.Text += SK.Text("SendMonksPanel_X_Votes", "votes");
          else
            this.influenceHeaderLabel.Text += SK.Text("SendMonksPanel_X_Vote", "vote");
          this.influenceHeaderLabel.Text += ")";
          break;
        case 3:
          this.actionButton5.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[18];
          this.actionButton5.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[25];
          this.villageActionLabel.Text = SK.Text("VillageMapPanel_Inquisition", "Inquisition") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
          break;
        case 4:
          this.actionButton2.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[15];
          this.actionButton2.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[22];
          this.villageActionLabel.Text = SK.Text("SendMonksPanel_Interdiction", "Interdiction") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
          break;
        case 5:
          this.actionButton3.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[16];
          this.actionButton3.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[23];
          this.villageActionLabel.Text = SK.Text("SendMonksPanel_Restoration", "Restoration") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
          break;
        case 6:
          this.actionButton6.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[19];
          this.actionButton6.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[26];
          this.villageActionLabel.Text = SK.Text("SendMonksPanel_Absolution", "Absolution") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
          break;
        case 7:
          this.actionButton7.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[20];
          this.actionButton7.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[27];
          this.villageActionLabel.Text = SK.Text("SendMonksPanel_Excommnunication", "Excommunication") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
          break;
      }
      this.updatePointsCost();
    }

    private void updatePointsCost()
    {
      int num1 = 0;
      int num2 = this.sliderImage.Value + 1;
      if (this.maxMonks == 0)
        num2 = 0;
      NumberFormatInfo nfi = GameEngine.NFI;
      switch (this.currentCommand)
      {
        case 1:
          num1 = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Blessings;
          if (!this.excommunicated)
          {
            int index = (int) GameEngine.Instance.World.UserResearchData.Research_Marriage;
            if (index < 1)
              index = 1;
            double num3 = (double) ResearchData.blessingTimes[index] * CardTypes.getBlessingMultipier(GameEngine.Instance.cardsManager.UserCardData);
            this.tooltipLabel.Text = SK.Text("SendMonksPanel_Increase_Popularity", "Increase Popularity within the Parish by :") + num2.ToString() + " (" + SK.Text("TOOLTIP_CARD_DURATION", "Duration") + " : " + num3.ToString("N", (IFormatProvider) nfi) + " " + SK.Text("ResearchEffect_X_Hours", "hours") + ")";
            break;
          }
          break;
        case 2:
        case 8:
          num1 = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Influence;
          if (GameEngine.Instance.World.isCountyCapital(this.m_selectedVillage))
            num1 *= 2;
          if (!this.excommunicated)
          {
            int num4 = CardTypes.getInfluenceMultipier(GameEngine.Instance.cardsManager.UserCardData) * num2;
            if (num4 != 1)
            {
              this.tooltipLabel.Text = SK.Text("SendMonksPanel_Send_Influence", "Influence Election by :") + " " + num4.ToString() + " " + SK.Text("SendMonksPanel_X_Votes", "votes");
              break;
            }
            this.tooltipLabel.Text = SK.Text("SendMonksPanel_Send_Influence", "Influence Election by :") + " " + num4.ToString() + " " + SK.Text("SendMonksPanel_X_Vote", "vote");
            break;
          }
          break;
        case 3:
          num1 = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Inquisition;
          if (!this.excommunicated)
          {
            int index = (int) GameEngine.Instance.World.UserResearchData.Research_Confirmation;
            if (index < 1)
              index = 1;
            double num5 = (double) ResearchData.confirmationTimes[index] * CardTypes.getInquisitionMultipier(GameEngine.Instance.cardsManager.UserCardData);
            this.tooltipLabel.Text = SK.Text("SendMonksPanel_Descrease_Popularity", "Decrease Popularity within the Parish by :") + num2.ToString() + " (" + SK.Text("TOOLTIP_CARD_DURATION", "Duration") + " : " + num5.ToString("N", (IFormatProvider) nfi) + " " + SK.Text("ResearchEffect_X_Hours", "hours") + ")";
            break;
          }
          break;
        case 4:
          int pointsCostInterdicts = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Interdicts;
          if (!this.excommunicated)
          {
            int currentLevel = num2 * 4;
            int num6 = CardTypes.adjustInterdictionLevel(GameEngine.Instance.cardsManager.UserCardData, currentLevel);
            this.tooltipLabel.Text = SK.Text("SendMonksPanel_Protect", "Protect the Village from attack for :") + " " + num6.ToString() + " " + SK.Text("ResearchEffect_X_Hours", "hours");
          }
          num1 = !this.targetCapital ? TradingCalcs.adjustInterdictionCostByTargetRank(pointsCostInterdicts, this.targetUserRank, GameEngine.Instance.World.SecondAgeWorld) : pointsCostInterdicts * 10;
          break;
        case 5:
          num1 = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Restoration;
          if (!this.excommunicated)
          {
            int index = (int) GameEngine.Instance.World.UserResearchData.Research_Baptism;
            if (index < 1)
              index = 1;
            int currentLevel = num2 * ResearchData.baptismRestoreAmount[index];
            int num7 = CardTypes.adjustRestorationLevel(GameEngine.Instance.cardsManager.UserCardData, currentLevel);
            this.tooltipLabel.Text = SK.Text("SendMonksPanel_Remove_Disease", "Points of Disease healed :") + " " + num7.ToString();
            break;
          }
          break;
        case 6:
          num1 = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Absolution;
          if (!this.excommunicated)
          {
            int index = (int) GameEngine.Instance.World.UserResearchData.Research_Confession;
            if (index < 1)
              index = 1;
            double currentLevel = (double) (ResearchData.confessionTimes[index] * num2);
            double num8 = CardTypes.adjustAbsolutionLevel(GameEngine.Instance.cardsManager.UserCardData, currentLevel);
            this.tooltipLabel.Text = SK.Text("SendMonksPanel_Reduce_Excommunication", "Reduce Excommunication Time in Village by :") + " " + num8.ToString("N", (IFormatProvider) nfi) + " " + SK.Text("ResearchEffect_X_Hours", "hours");
            break;
          }
          break;
        case 7:
          num1 = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Excommunication;
          if (!this.excommunicated)
          {
            int index = (int) GameEngine.Instance.World.UserResearchData.Research_ExtremeUnction;
            if (index < 1)
              index = 1;
            double currentLevel = (double) (ResearchData.extremeUnctionTimes[index] * num2);
            double num9 = CardTypes.adjustExcommunicationLevel(GameEngine.Instance.cardsManager.UserCardData, currentLevel);
            this.tooltipLabel.Text = SK.Text("SendMonksPanel_Remove_Powers", "Remove Church powers from the Village for :") + " " + num9.ToString("N", (IFormatProvider) nfi) + " " + SK.Text("ResearchEffect_X_Hours", "hours");
            break;
          }
          break;
      }
      int num10 = num1 * num2;
      this.currentPointsCost = num10;
      this.costValueLabel.Text = num10.ToString();
      if ((double) num10 <= GameEngine.Instance.World.getCurrentFaithPoints())
      {
        this.costValueLabel.Color = Color.FromArgb(18, (int) byte.MaxValue, 0);
        if (this.launchAllowed && num10 > 0 && !this.excommunicated)
          this.launchButton.Enabled = true;
        else
          this.launchButton.Enabled = false;
      }
      else
      {
        this.costValueLabel.Color = Color.FromArgb(252, 0, 12);
        this.launchButton.Enabled = false;
      }
    }

    public void update()
    {
      this.cardbar.update();
      this.onVillageLoadUpdate(this.m_ownVillage, false);
      this.numLabel.Text = this.numLabel.Text;
      if (this.excommunicated)
      {
        int secsLeft = (int) (this.excommunicationTime - VillageMap.getCurrentServerTime()).TotalSeconds;
        if (secsLeft < -5)
        {
          this.excommunicated = false;
          this.init(this.m_selectedVillage);
        }
        else
        {
          if (secsLeft < 0)
            secsLeft = 0;
          this.tooltipLabel.Text = SK.Text("SendMonksPanel_You_Are_Excommunicated", "You are Excommunicated, you cannot issue any commands.") + " " + SK.Text("SendMonksPanel_Excommunication_Expires_in", "Excommunication Expires in") + " : " + VillageMap.createBuildTimeString(secsLeft);
        }
      }
      double distance = this.storedPreCardDistance * CardTypes.adjustMonkSpeed(GameEngine.Instance.cardsManager.UserCardData);
      double secsLeft1 = GameEngine.Instance.World.adjustIfIslandTravel(distance, this.m_ownVillage, this.m_selectedVillage);
      if ((int) secsLeft1 == this.timeLabel.CustomTooltipData)
        return;
      this.timeLabel.Text = VillageMap.createBuildTimeString((int) secsLeft1);
      this.timeLabel.CustomTooltipData = (int) secsLeft1;
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
      this.numLabel.Text = !this.sliderEnabled ? "0" : (this.sliderImage.Value + 1).ToString();
      this.updatePointsCost();
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_selectedVillage, false, true, false, false);
      InterfaceMgr.Instance.closeSendMonkWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    private void launch()
    {
      if (!this.sliderEnabled || this.inLaunch && (DateTime.Now - this.lastLaunchTime).TotalSeconds < 20.0)
        return;
      this.inLaunch = true;
      this.lastLaunchTime = DateTime.Now;
      if (this.sendMonks())
      {
        this.launchButton.Enabled = false;
        this.closeButton.Enabled = false;
        CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, this.ParentForm);
      }
      else
        this.inLaunch = false;
    }

    public bool sendMonks()
    {
      int number = this.sliderImage.Value + 1;
      if (number <= 0)
        return false;
      int data = -1;
      if (this.currentCommand == 2 && this.votedUser < 0)
        return false;
      if (this.currentCommand == 2)
      {
        if (!this.positiveInfluence)
          this.currentCommand = 8;
        data = this.votedUser;
      }
      if (this.currentCommand == 2)
      {
        foreach (ParishMember parishMember in this.parishMembers)
        {
          if (parishMember.userID == this.votedUser)
          {
            if (parishMember.numVotesReceived + number > this.voteCap)
            {
              MessageBoxButtons buts = MessageBoxButtons.YesNo;
              if (MyMessageBox.Show(SK.Text("SendMonksPanel_Are_You_Sure_positive", "Are you sure? This Positive Influence may waste monks."), SK.Text("SendMonksPanel_Confirm_Influence", "Confirm Influence"), buts) != DialogResult.Yes)
                return false;
              break;
            }
            break;
          }
        }
      }
      else if (this.currentCommand == 8)
      {
        foreach (ParishMember parishMember in this.parishMembers)
        {
          if (parishMember.userID == this.votedUser)
          {
            if (parishMember.numVotesReceived - number < 0)
            {
              MessageBoxButtons buts = MessageBoxButtons.YesNo;
              if (MyMessageBox.Show(SK.Text("SendMonksPanel_Are_You_Sure_Negative", "Are you sure? This Negative Influence may waste monks."), SK.Text("SendMonksPanel_Confirm_Influence", "Confirm Influence"), buts) != DialogResult.Yes)
                return false;
              break;
            }
            break;
          }
        }
      }
      RemoteServices.Instance.set_SendPeople_UserCallBack(new RemoteServices.SendPeople_UserCallBack(this.sendPeopleCallback));
      RemoteServices.Instance.SendPeople(this.m_ownVillage, this.m_selectedVillage, 4, number, this.currentCommand, data);
      AllVillagesPanel.travellersChanged();
      return true;
    }

    public void sendPeopleCallback(SendPeople_ReturnType returnData)
    {
      try
      {
        if (returnData.Success)
        {
          GameEngine.Instance.World.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
          GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
          InterfaceMgr.Instance.getMainTabBar().changeTab(9);
          InterfaceMgr.Instance.getMainTabBar().changeTab(0);
          InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_selectedVillage, false, true, false, false);
          InterfaceMgr.Instance.closeMonksPanel();
        }
        else
        {
          CursorManager.SetCursor(CursorManager.CursorType.Default, this.ParentForm);
          if (returnData.m_errorCode == ErrorCodes.ErrorCode.PEOPLE_INTERDICT_RANK_TOO_HIGH)
          {
            int num = (int) MyMessageBox.Show(SK.Text("SendMonksPanel_Rank_Too_High", "The Target Village Rank is too high."), SK.Text("GENERIC_Error", "Error"));
          }
          this.inLaunch = false;
          this.closeButton.Enabled = true;
          this.updatePointsCost();
        }
      }
      catch (Exception ex)
      {
      }
    }

    public void getExcommunicationStatusCallback(GetExcommunicationStatus_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.targetUserRank = returnData.targetUserRank;
      this.excommunicated = returnData.excommunicated;
      this.excommunicationTime = returnData.excommunicationTime;
      if (!this.excommunicated)
        return;
      this.launchButton.Enabled = false;
      this.updateButtons(-1);
      this.tooltipLabel.Text = SK.Text("SendMonksPanel_You_Are_Excommunicated", "You are Excommunicated, you cannot issue any commands.") + " " + SK.Text("SendMonksPanel_Excommunication_Expires_in", "Excommunication Expires in") + " :";
    }

    public void getParishMembersListCallback(GetParishMembersList_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (this.parishMembers == null)
        this.parishMembers = new List<ParishMember>();
      else
        this.parishMembers.Clear();
      if (returnData.parishMembers != null)
        this.parishMembers.AddRange((IEnumerable<ParishMember>) returnData.parishMembers);
      this.parishMembers.Sort((IComparer<ParishMember>) this.parishMemberComparer);
      if (this.parishMembers.Count > 0)
        this.votedUser = this.parishMembers[0].userID;
      this.voteCap = returnData.voteCap;
      this.addPlayers();
    }

    public void getCountyElectionInfoCallback(GetCountyElectionInfo_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (this.parishMembers == null)
        this.parishMembers = new List<ParishMember>();
      else
        this.parishMembers.Clear();
      if (returnData.countyMembers != null)
        this.parishMembers.AddRange((IEnumerable<ParishMember>) returnData.countyMembers);
      this.parishMembers.Sort((IComparer<ParishMember>) this.parishMemberComparer);
      if (this.parishMembers.Count > 0)
        this.votedUser = this.parishMembers[0].userID;
      this.voteCap = returnData.voteCap;
      this.addPlayers();
    }

    public void addPlayers()
    {
      this.scrollArea.clearControls();
      this.lineList.Clear();
      int y = 0;
      this.scrollBar.Visible = false;
      if (this.parishMembers != null)
      {
        foreach (ParishMember parishMember in this.parishMembers)
        {
          if (y != 0)
          {
            CustomSelfDrawPanel.CSDLine control = new CustomSelfDrawPanel.CSDLine();
            control.Position = new Point(0, y - 1);
            control.LineColor = Color.FromArgb(60, 60, 60);
            control.Size = new Size(385, 0);
            this.scrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          }
          SendMonkPanel.MonkVoteLine control1 = new SendMonkPanel.MonkVoteLine();
          control1.Position = new Point(0, y);
          control1.init(parishMember.userName, parishMember.userID, parishMember.rank, parishMember.points, true, parishMember.numSpareVotes, parishMember.numVotesReceived, parishMember.factionID, this.votedUser, this);
          this.scrollArea.addControl((CustomSelfDrawPanel.CSDControl) control1);
          y += control1.Height;
          this.lineList.Add(control1);
        }
        if (y > 300)
        {
          this.scrollBar.Visible = true;
          this.scrollBar.Max = y - 300;
        }
      }
      this.scrollArea.invalidate();
      this.influenceIndent.invalidate();
    }

    private void scrollBarMoved()
    {
      int y = this.scrollBar.Value;
      this.scrollArea.Position = new Point(this.scrollArea.X, 36 - y);
      this.scrollArea.ClipRect = new Rectangle(this.scrollArea.ClipRect.X, y, this.scrollArea.ClipRect.Width, this.scrollArea.ClipRect.Height);
      this.scrollArea.invalidate();
      this.influenceIndent.invalidate();
    }

    private void mouseWheelMoved(int delta)
    {
      if (delta < 0)
      {
        this.scrollBar.scrollDown(6);
      }
      else
      {
        if (delta <= 0)
          return;
        this.scrollBar.scrollUp(6);
      }
    }

    private void positiveClick()
    {
      this.positiveInfluence = true;
      this.villageActionLabel.Text = SK.Text("SendMonksPanel_Positive_Influence", "Positive Influence") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
      this.influenceHeaderLabel.Text = SK.Text("SendMonksPanel_Select_positive", "Select Player to Positively Influence");
      int num1 = this.sliderImage.Value + 1;
      if (this.maxMonks == 0)
        num1 = 0;
      int num2 = CardTypes.getInfluenceMultipier(GameEngine.Instance.cardsManager.UserCardData) * num1;
      CustomSelfDrawPanel.CSDLabel influenceHeaderLabel = this.influenceHeaderLabel;
      influenceHeaderLabel.Text = influenceHeaderLabel.Text + " (" + num2.ToString() + " ";
      if (num2 != 1)
        this.influenceHeaderLabel.Text += SK.Text("SendMonksPanel_X_Votes", "votes");
      else
        this.influenceHeaderLabel.Text += SK.Text("SendMonksPanel_X_Vote", "vote");
      this.influenceHeaderLabel.Text += ")";
    }

    private void negativeClick()
    {
      this.positiveInfluence = false;
      this.villageActionLabel.Text = SK.Text("SendMonksPanel_Negative_Influence", "Negative Influence") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
      this.influenceHeaderLabel.Text = SK.Text("SendMonksPanel_Select_negative", "Select Player to Negatively Influence");
      int num1 = this.sliderImage.Value + 1;
      if (this.maxMonks == 0)
        num1 = 0;
      int num2 = CardTypes.getInfluenceMultipier(GameEngine.Instance.cardsManager.UserCardData) * num1;
      CustomSelfDrawPanel.CSDLabel influenceHeaderLabel = this.influenceHeaderLabel;
      influenceHeaderLabel.Text = influenceHeaderLabel.Text + " (" + num2.ToString() + " ";
      if (num2 != 1)
        this.influenceHeaderLabel.Text += SK.Text("SendMonksPanel_X_Votes", "votes");
      else
        this.influenceHeaderLabel.Text += SK.Text("SendMonksPanel_X_Vote", "vote");
      this.influenceHeaderLabel.Text += ")";
    }

    private void closeInfluenceClick() => this.updateButtons(this.currentCommand);

    public void radioClicked(int clickedUserID)
    {
      this.votedUser = clickedUserID;
      foreach (SendMonkPanel.MonkVoteLine line in this.lineList)
        line.setState(this.votedUser);
    }

    public class ParishMemberComparer : IComparer<ParishMember>
    {
      public int Compare(ParishMember x, ParishMember y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null || x.numVotesReceived < y.numVotesReceived)
          return 1;
        return x.numVotesReceived > y.numVotesReceived ? -1 : 0;
      }
    }

    public class MonkVoteLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel factionLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel votesLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton radioButton = new CustomSelfDrawPanel.CSDButton();
      private SendMonkPanel m_parent;
      private int m_userID = -1;
      private int m_factionID = -1;
      private bool m_votingAllowed;

      public void init(
        string playerName,
        int userID,
        int rank,
        int points,
        bool votingAllowed,
        int numSpareVotes,
        int numReceivedVotes,
        int factionID,
        int votedUser,
        SendMonkPanel parent)
      {
        this.Size = new Size(385, 25);
        this.m_parent = parent;
        this.m_userID = userID;
        this.m_factionID = factionID;
        this.m_votingAllowed = votingAllowed;
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.radioClicked));
        if (votedUser != userID)
        {
          this.radioButton.ImageNorm = (Image) GFXLibrary.radio_green[2];
          this.radioButton.ImageOver = (Image) GFXLibrary.radio_green[1];
          this.radioButton.ImageClick = (Image) GFXLibrary.radio_green[1];
          this.radioButton.Active = true;
        }
        else
        {
          this.radioButton.ImageNorm = (Image) GFXLibrary.radio_green[0];
          this.radioButton.ImageOver = (Image) GFXLibrary.radio_green[0];
          this.radioButton.ImageClick = (Image) GFXLibrary.radio_green[0];
          this.radioButton.Active = false;
        }
        this.radioButton.Position = new Point(0, 2);
        this.radioButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.radioClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.radioButton);
        this.nameLabel.Text = "";
        this.nameLabel.Color = ARGBColors.White;
        this.nameLabel.Position = new Point(20, 0);
        this.nameLabel.Size = new Size(175, 25);
        this.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.nameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.radioClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.nameLabel);
        this.factionLabel.Color = ARGBColors.White;
        this.factionLabel.Position = new Point(200, 0);
        this.factionLabel.Size = new Size(150, 25);
        this.factionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.factionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.factionLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.radioClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.factionLabel);
        this.votesLabel.Color = ARGBColors.White;
        this.votesLabel.Position = new Point(350, 0);
        this.votesLabel.Size = new Size(35, 25);
        this.votesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.votesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.votesLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.radioClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.votesLabel);
        this.nameLabel.Text = playerName;
        NumberFormatInfo nfi = GameEngine.NFI;
        this.votesLabel.Text = numReceivedVotes.ToString("N", (IFormatProvider) nfi);
        if (factionID >= 0)
        {
          FactionData faction = GameEngine.Instance.World.getFaction(factionID);
          this.factionLabel.Text = faction == null ? "" : faction.factionNameAbrv;
        }
        else
          this.factionLabel.Text = "";
        this.invalidate();
      }

      public void update()
      {
      }

      public void setState(int selectedUserID)
      {
        if (selectedUserID != this.m_userID)
        {
          this.radioButton.ImageNorm = (Image) GFXLibrary.radio_green[2];
          this.radioButton.ImageOver = (Image) GFXLibrary.radio_green[1];
          this.radioButton.ImageClick = (Image) GFXLibrary.radio_green[1];
          this.radioButton.Active = true;
        }
        else
        {
          this.radioButton.ImageNorm = (Image) GFXLibrary.radio_green[0];
          this.radioButton.ImageOver = (Image) GFXLibrary.radio_green[0];
          this.radioButton.ImageClick = (Image) GFXLibrary.radio_green[0];
          this.radioButton.Active = false;
        }
      }

      public void radioClicked()
      {
        if (!this.radioButton.Active || this.m_parent == null)
          return;
        GameEngine.Instance.playInterfaceSound("SendMonkPanel_select_village");
        this.m_parent.radioClicked(this.m_userID);
      }

      public void lineClicked()
      {
      }
    }
  }
}
