// Decompiled with JetBrains decompiler
// Type: Kingdoms.HouseInfoPanel
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
  public class HouseInfoPanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int PANEL_ID = 52;
    public static HouseInfoPanel instance = (HouseInfoPanel) null;
    public static int SelectedHouse = -1;
    private int m_houseLeaderFactionID = -1;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel houseNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel houseMottoLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel data1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data1LabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data2LabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data3LabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data4Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data4LabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data5Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data5LabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data6Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data6LabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data7Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel data7LabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel diplomacyLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton diplomacyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel factionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel membershipVoteLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel leadershipVoteLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDImage backImage1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage backImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage6 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage gloryImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton leaveHouseButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel lastVisitLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton sendProclamationButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel nextProclamationLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
    private MyMessageBoxPopUp PopUpRef;
    private int houseIDRef;
    private bool inHouseVote;
    private bool houseVisitSent;
    private List<HouseInfoPanel.HouseLine> lineList = new List<HouseInfoPanel.HouseLine>();
    private CustomSelfDrawPanel.CSDFill greyOverlay = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel diplomacyHeaderImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel diplomacyBackgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDButton diplomacyNeutralButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton diplomacyAllyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton diplomacyEnemyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton diplomacyCancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel diplomacyHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel diplomacyFactionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel diplomacyCurrentLabelHeader = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel diplomacyCurrentLabel = new CustomSelfDrawPanel.CSDLabel();
    private DockableControl dockableControl;
    private IContainer components;

    public HouseInfoPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      NumberFormatInfo nfi = GameEngine.NFI;
      HouseInfoPanel.instance = this;
      this.inHouseVote = false;
      this.clearControls();
      GameEngine.Instance.houseManager.UpdateGloryPoints(new HouseManager.HouseInfoUpdatedCallback(this.Refresh));
      this.sidebar.addSideBar(8, (CustomSelfDrawPanel) this);
      HouseData houseData;
      try
      {
        houseData = GameEngine.Instance.World.HouseInfo[HouseInfoPanel.SelectedHouse];
        this.m_houseLeaderFactionID = houseData.leadingFactionID;
      }
      catch (Exception ex)
      {
        houseData = new HouseData();
        this.m_houseLeaderFactionID = -1;
      }
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(this.Width - 200, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width - 200, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.backImage1.Image = (Image) GFXLibrary.faction_tanback;
      this.backImage1.Position = new Point(this.mainBackgroundImage.Size.Width - this.backImage1.Size.Width - 25, 12);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backImage1);
      this.backImage2.Image = (Image) GFXLibrary.faction_title_band;
      this.backImage2.Position = new Point(20, 20);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backImage2);
      this.barImage1.Image = (Image) GFXLibrary.faction_bar_tan_1_heavier;
      this.barImage1.Position = new Point(201, 70);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.barImage1);
      this.barImage2.Image = (Image) GFXLibrary.faction_bar_tan_1_lighter;
      this.barImage2.Position = new Point(201, 94);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.barImage2);
      this.barImage3.Image = (Image) GFXLibrary.faction_bar_tan_1_heavier;
      this.barImage3.Position = new Point(201, 118);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.barImage3);
      this.barImage4.Image = (Image) GFXLibrary.faction_bar_tan_2_heavier;
      this.barImage4.Position = new Point(460, 70);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.barImage4);
      this.barImage5.Image = (Image) GFXLibrary.faction_bar_tan_2_lighter;
      this.barImage5.Position = new Point(460, 94);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.barImage5);
      this.barImage6.Image = (Image) GFXLibrary.faction_bar_tan_2_heavier;
      this.barImage6.Position = new Point(460, 118);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.barImage6);
      this.houseNameLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + HouseInfoPanel.SelectedHouse.ToString();
      this.houseNameLabel.Color = ARGBColors.Black;
      this.houseNameLabel.Position = new Point(205, 10);
      this.houseNameLabel.Size = new Size(600, 40);
      this.houseNameLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Regular);
      this.houseNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseNameLabel);
      this.houseMottoLabel.Text = "\"" + CustomTooltipManager.getHouseMotto(HouseInfoPanel.SelectedHouse) + "\"";
      this.houseMottoLabel.Color = ARGBColors.Black;
      this.houseMottoLabel.Position = new Point(205, 41);
      this.houseMottoLabel.Size = new Size(600, 40);
      this.houseMottoLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.houseMottoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseMottoLabel);
      this.houseImage.Image = (Image) GFXLibrary.getHouseCircleLargeImage(HouseInfoPanel.SelectedHouse - 1);
      this.houseImage.Position = new Point(32, 24);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseImage);
      this.data1Label.Text = SK.Text("GENERIC_Factions", "Factions");
      this.data1Label.Color = ARGBColors.Black;
      this.data1Label.Position = new Point(210, 73);
      this.data1Label.Size = new Size(600, 40);
      this.data1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data1Label);
      this.data1LabelValue.Text = HouseInfoPanel.SelectedHouse != 0 ? houseData.numFactions.ToString("N", (IFormatProvider) nfi) : GameEngine.Instance.World.countHouseFactions(HouseInfoPanel.SelectedHouse).ToString("N", (IFormatProvider) nfi);
      this.data1LabelValue.Color = ARGBColors.Black;
      this.data1LabelValue.Position = new Point(200, 73);
      this.data1LabelValue.Size = new Size(230, 40);
      this.data1LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data1LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data1LabelValue);
      this.data2Label.Text = SK.Text("FactionInvites_Total_Points", "Total Points");
      this.data2Label.Color = ARGBColors.Black;
      this.data2Label.Position = new Point(210, 97);
      this.data2Label.Size = new Size(600, 40);
      this.data2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data2Label);
      this.data2LabelValue.Text = houseData.points.ToString("N", (IFormatProvider) nfi);
      this.data2LabelValue.Color = ARGBColors.Black;
      this.data2LabelValue.Position = new Point(200, 97);
      this.data2LabelValue.Size = new Size(230, 40);
      this.data2LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data2LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data2LabelValue);
      this.data3Label.Text = SK.Text("FactionInvites_Members", "Members");
      this.data3Label.Color = ARGBColors.Black;
      this.data3Label.Position = new Point(210, 121);
      this.data3Label.Size = new Size(600, 40);
      this.data3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data3Label);
      this.data3LabelValue.Text = GameEngine.Instance.World.countHouseMembers(HouseInfoPanel.SelectedHouse).ToString("N", (IFormatProvider) nfi);
      this.data3LabelValue.Color = ARGBColors.Black;
      this.data3LabelValue.Position = new Point(200, 121);
      this.data3LabelValue.Size = new Size(230, 40);
      this.data3LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data3LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data3LabelValue);
      this.data4Label.Text = SK.Text("FactionInvites_Marshall", "Marshall");
      this.data4Label.Color = ARGBColors.Black;
      this.data4Label.Position = new Point(467, 73);
      this.data4Label.Size = new Size(600, 40);
      this.data4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data4Label);
      this.data4LabelValue.Text = houseData.leaderUserName;
      this.data4LabelValue.Color = ARGBColors.Black;
      this.data4LabelValue.Position = new Point(517, 73);
      this.data4LabelValue.Size = new Size(230, 40);
      this.data4LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data4LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data4LabelValue);
      this.data5Label.Text = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction");
      this.data5Label.Color = ARGBColors.Black;
      this.data5Label.Position = new Point(467, 97);
      this.data5Label.Size = new Size(600, 40);
      this.data5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data5Label);
      this.data5LabelValue.Text = "";
      if (houseData.leadingFactionID >= 0)
      {
        FactionData faction = GameEngine.Instance.World.getFaction(houseData.leadingFactionID);
        if (faction != null)
          this.data5LabelValue.Text = faction.factionName;
      }
      this.data5LabelValue.Color = ARGBColors.Black;
      this.data5LabelValue.Position = new Point(517, 75);
      this.data5LabelValue.Size = new Size(230, 60);
      this.data5LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data5LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data5LabelValue);
      int gloryRank = GameEngine.Instance.World.getGloryRank(HouseInfoPanel.SelectedHouse);
      if (gloryRank >= 0)
      {
        this.gloryImage.Image = (Image) GFXLibrary.glory_frame;
        this.gloryImage.Position = new Point(490, 10);
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.gloryImage);
        this.data6Label.Text = SK.Text("FactionInvites_Glory_Rank", "Glory Rank");
        this.data6Label.Color = ARGBColors.Black;
        this.data6Label.Position = new Point(505, 27);
        this.data6Label.Size = new Size(600, 40);
        this.data6Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
        this.data6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data6Label);
        this.data6LabelValue.Text = (gloryRank + 1).ToString("N", (IFormatProvider) nfi);
        this.data6LabelValue.Color = ARGBColors.Black;
        this.data6LabelValue.Position = new Point(694, 27);
        this.data6LabelValue.Size = new Size(29, 40);
        this.data6LabelValue.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
        this.data6LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data6LabelValue);
      }
      this.data7Label.Text = SK.Text("FactionInvites_Glory Victories", "Glory Victories");
      this.data7Label.Color = ARGBColors.Black;
      this.data7Label.Position = new Point(467, 121);
      this.data7Label.Size = new Size(600, 40);
      this.data7Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data7Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data7Label);
      this.data7LabelValue.Text = houseData.numVictories.ToString("N", (IFormatProvider) nfi);
      this.data7LabelValue.Color = ARGBColors.Black;
      this.data7LabelValue.Position = new Point(517, 121);
      this.data7LabelValue.Size = new Size(230, 40);
      this.data7LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.data7LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.data7LabelValue);
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23 - 200, 28);
      this.headerLabelsImage.Position = new Point(25, 159);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider1Image.Position = new Point(290, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider1Image);
      this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider2Image.Position = new Point(440, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
      this.factionLabel.Text = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction");
      this.factionLabel.Color = ARGBColors.Black;
      this.factionLabel.Position = new Point(9, -2);
      this.factionLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.factionLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.factionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionLabel);
      this.pointsLabel.Text = SK.Text("FactionsPanel_Points", "Points");
      this.pointsLabel.Color = ARGBColors.Black;
      this.pointsLabel.Position = new Point(295, -2);
      this.pointsLabel.Size = new Size(140, this.headerLabelsImage.Height);
      this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + HouseInfoPanel.SelectedHouse.ToString());
      FactionData yourFaction = GameEngine.Instance.World.YourFaction;
      int index = 0;
      if (yourFaction != null)
        index = yourFaction.houseID;
      bool flag1 = false;
      int yourFactionRank = GameEngine.Instance.World.getYourFactionRank();
      if (index != 0 && index == HouseInfoPanel.SelectedHouse)
      {
        this.leaderVisited();
        if (GameEngine.Instance.World.HouseVoteInfo != null && GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID > 0)
        {
          int appliedToHouseId = GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID;
        }
        if (yourFactionRank == 1 && !GameEngine.Instance.World.WorldEnded)
        {
          this.leaveHouseButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
          this.leaveHouseButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
          this.leaveHouseButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
          this.leaveHouseButton.Position = new Point(560, height - 30);
          this.leaveHouseButton.Text.Text = SK.Text("FactionsPanel_Leave_House", "Leave House");
          this.leaveHouseButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          this.leaveHouseButton.TextYOffset = -3;
          this.leaveHouseButton.Text.Color = ARGBColors.Black;
          this.leaveHouseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.leaveHouseClick), "HouseInfoPanel_leave");
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.leaveHouseButton);
          flag1 = true;
        }
        if (yourFactionRank == 1 || yourFactionRank == 2)
        {
          TimeSpan timeSpan = VillageMap.getCurrentServerTime() - yourFaction.lastHouseDate;
          string str;
          if (timeSpan.TotalDays < 1.0 && VillageMap.getCurrentServerTime().Day == yourFaction.lastHouseDate.Day)
            str = SK.Text("FactionsPanel_Today", "Today");
          else if (timeSpan.TotalDays < 2.0 && VillageMap.getCurrentServerTime().Day == yourFaction.lastHouseDate.AddDays(1.0).Day)
          {
            str = SK.Text("FactionsPanel_Yesterday", "Yesterday");
          }
          else
          {
            int num = (int) timeSpan.TotalDays;
            if (num < 2)
              num = 2;
            str = num.ToString() + " " + SK.Text("FactionsPanel_X_Days_Ago", "Days ago");
          }
          this.lastVisitLabel.Text = SK.Text("FactionsPanel_Last_General_Visit", "Last General Visit") + " : " + str;
          this.lastVisitLabel.Color = ARGBColors.Black;
          this.lastVisitLabel.Position = new Point(10, height - 25);
          this.lastVisitLabel.Size = new Size(400, 40);
          this.lastVisitLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
          this.lastVisitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.lastVisitLabel);
          flag1 = true;
        }
        this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
        this.divider3Image.Position = new Point(610, 0);
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
        this.membershipVoteLabel.Text = SK.Text("FactionsPanel_Membership_Vote", "Membership Vote");
        this.membershipVoteLabel.Color = ARGBColors.Black;
        this.membershipVoteLabel.Position = new Point(445, -2);
        this.membershipVoteLabel.Size = new Size(160, this.headerLabelsImage.Height);
        this.membershipVoteLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.membershipVoteLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.membershipVoteLabel);
        this.leadershipVoteLabel.Text = SK.Text("FactionsPanel_Leadership_Vote", "Leadership Vote");
        this.leadershipVoteLabel.Color = ARGBColors.Black;
        this.leadershipVoteLabel.Position = new Point(595, -2);
        this.leadershipVoteLabel.Size = new Size(160, this.headerLabelsImage.Height);
        this.leadershipVoteLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.leadershipVoteLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.leadershipVoteLabel);
        if (GameEngine.Instance.World.getYourHouseRank() == 10 && yourFactionRank == 1)
        {
          this.sendProclamationButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
          this.sendProclamationButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
          this.sendProclamationButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
          this.sendProclamationButton.Position = new Point(330, height - 30);
          this.sendProclamationButton.Text.Text = SK.Text("Capitials_Proclamation", "Send Proclamation");
          this.sendProclamationButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          this.sendProclamationButton.TextYOffset = -3;
          this.sendProclamationButton.Text.Color = ARGBColors.Black;
          this.sendProclamationButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendProclamation), "HouseInfoPanel_sendProclamation");
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.sendProclamationButton);
          DateTime proclomationDate = GameEngine.Instance.World.HouseInfo[index].lastProclomationDate;
          TimeSpan timeSpan1 = VillageMap.getCurrentServerTime() - proclomationDate;
          this.nextProclamationLabel.Text = SK.Text("FactionsPanel_Next_Proclamation_Time", "Next proclamation available in") + " : ";
          this.nextProclamationLabel.Color = ARGBColors.White;
          this.nextProclamationLabel.Position = new Point(330, height - 32);
          this.nextProclamationLabel.Size = new Size(160, this.headerLabelsImage.Height);
          this.nextProclamationLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          this.nextProclamationLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.nextProclamationLabel);
          if (timeSpan1.TotalDays >= 1.0)
          {
            this.sendProclamationButton.Enabled = true;
            this.sendProclamationButton.Visible = true;
            this.nextProclamationLabel.Visible = false;
          }
          else
          {
            TimeSpan timeSpan2 = TimeSpan.FromDays(1.0) - timeSpan1;
            this.sendProclamationButton.Enabled = false;
            this.sendProclamationButton.Visible = false;
            this.nextProclamationLabel.Text += TimeSpan.FromSeconds((double) (timeSpan2.Hours * 60 * 60 + timeSpan2.Minutes * 60 + timeSpan2.Seconds)).ToString();
            this.nextProclamationLabel.Visible = true;
          }
        }
      }
      else if (index > 0)
      {
        bool flag2 = false;
        if (yourFactionRank == 1 && GameEngine.Instance.World.getYourHouseRank() == 10)
          flag2 = true;
        if (flag2)
        {
          this.diplomacyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
          this.diplomacyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
          this.diplomacyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
          this.diplomacyButton.Position = new Point(559, height - 30);
          this.diplomacyButton.Text.Text = "";
          this.diplomacyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.diplomacyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          this.diplomacyButton.TextYOffset = -3;
          this.diplomacyButton.Text.Color = ARGBColors.Black;
          this.diplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.diplomacyClicked), "HouseInfoPanel_diplomacy");
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyButton);
          flag1 = true;
        }
        else
        {
          this.diplomacyLabel.Text = "";
          this.diplomacyLabel.Color = ARGBColors.Black;
          this.diplomacyLabel.Position = new Point(520, height - 25);
          this.diplomacyLabel.Size = new Size(240, 40);
          this.diplomacyLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
          this.diplomacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyLabel);
          flag1 = true;
        }
      }
      this.wallScrollArea.Position = new Point(25, 188);
      if (flag1)
      {
        this.wallScrollArea.Size = new Size(705, height - 240);
        this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(705, height - 240));
      }
      else
      {
        this.wallScrollArea.Size = new Size(705, height - 188);
        this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(705, height - 188));
      }
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      int num1 = this.wallScrollBar.Value;
      this.wallScrollBar.Position = new Point(733, 188);
      if (flag1)
        this.wallScrollBar.Size = new Size(24, height - 240);
      else
        this.wallScrollBar.Size = new Size(24, height - 188);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      int num2 = resized ? 1 : 0;
      this.addFactions();
    }

    public void updateRelationshipText()
    {
      int house = GameEngine.Instance.World.getHouse(RemoteServices.Instance.UserFactionID);
      if (RemoteServices.Instance.UserFactionID < 0 || house == 0 || house == HouseInfoPanel.SelectedHouse)
      {
        this.diplomacyButton.Visible = false;
        this.diplomacyLabel.Visible = false;
      }
      else
      {
        string str = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy") + " : ";
        this.diplomacyButton.Visible = true;
        this.diplomacyLabel.Visible = true;
        int num = 0;
        if (HouseInfoPanel.SelectedHouse >= 0)
          num = GameEngine.Instance.World.getYourHouseRelation(HouseInfoPanel.SelectedHouse);
        if (num == 0)
          str += SK.Text("GENERIC_Neutral", "Neutral");
        else if (num > 0)
          str += SK.Text("GENERIC_Ally", "Ally");
        else if (num < 0)
          str += SK.Text("GENERIC_Enemy", "Enemy");
        this.diplomacyLabel.Text = str;
        this.diplomacyButton.Text.Text = str;
      }
    }

    public new void Refresh() => this.init(false);

    public void update() => this.sidebar.update();

    private void wallScrollBarMoved()
    {
      int y = this.wallScrollBar.Value;
      this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 188 - y);
      this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
    }

    private void mouseWheelMoved(int delta)
    {
      if (!this.wallScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.wallScrollBar.scrollDown(40);
      }
      else
      {
        if (delta <= 0)
          return;
        this.wallScrollBar.scrollUp(40);
      }
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    private void leaveHouseClick()
    {
      if (GameEngine.Instance.World.YourFaction == null)
        return;
      int houseId = GameEngine.Instance.World.YourFaction.houseID;
      if (houseId <= 0)
        return;
      this.leaveHouseConfirmationPopup(houseId);
    }

    public void leaveHouseConfirmationPopup(int houseID)
    {
      this.houseIDRef = houseID;
      if (MyMessageBox.Show(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FactionsPanel_Leave_House", "Leave House"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.leaveHouseConfirmed();
    }

    private void leaveHouseConfirmed()
    {
      GameEngine.Instance.houseManager.LeaveHouse(this.houseIDRef, new HouseManager.HouseInfoUpdatedCallback(this.Refresh));
    }

    public void houseVote(int targetFaction, bool application, bool vote)
    {
      GameEngine.Instance.houseManager.houseVote(targetFaction, application, vote, new HouseManager.HouseInfoUpdatedCallback(this.Refresh));
    }

    public void houseVoteHouseLeader(int targetFaction)
    {
      if (this.inHouseVote)
        return;
      this.inHouseVote = true;
      GameEngine.Instance.houseManager.houseVoteHouseLeader(targetFaction, new HouseManager.HouseInfoUpdatedCallback(this.houseVoteHouseLeaderCallback));
    }

    public void houseVoteHouseLeaderCallback()
    {
      this.inHouseVote = false;
      this.Refresh();
    }

    private void leaderVisited()
    {
      if (GameEngine.Instance.World.getYourFactionRank() != 1 || this.houseVisitSent)
        return;
      this.houseVisitSent = true;
      RemoteServices.Instance.TouchHouseVisitDate(RemoteServices.Instance.UserFactionID);
    }

    public void logout() => this.houseVisitSent = false;

    public void addFactions()
    {
      this.wallScrollArea.clearControls();
      int num1 = 0;
      this.lineList.Clear();
      FactionData yourFaction = GameEngine.Instance.World.YourFaction;
      int num2 = 0;
      int num3 = 0;
      int num4 = -1;
      if (yourFaction != null)
      {
        num2 = yourFaction.houseID;
        num4 = yourFaction.houseLeaderVote;
      }
      if (num2 != 0 && num2 == HouseInfoPanel.SelectedHouse)
      {
        if (GameEngine.Instance.World.HouseVoteInfo != null && GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID > 0)
        {
          int appliedToHouseId = GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID;
        }
        num3 = GameEngine.Instance.World.getYourFactionRank();
      }
      int position1 = 0;
      FactionData[] houseFactions = GameEngine.Instance.World.getHouseFactions(HouseInfoPanel.SelectedHouse);
      HouseVoteData houseVoteInfo = GameEngine.Instance.World.HouseVoteInfo;
      if (num4 < 0)
        num4 = this.m_houseLeaderFactionID;
      bool flag = false;
      foreach (FactionData factionData in houseFactions)
      {
        if (num4 == factionData.factionID)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        num4 = this.m_houseLeaderFactionID;
      foreach (FactionData factionData in houseFactions)
      {
        if (factionData.active && factionData.numMembers != 0)
        {
          HouseInfoPanel.HouseLine control = new HouseInfoPanel.HouseLine();
          if (num1 != 0)
            num1 += 5;
          control.Position = new Point(0, num1);
          control.init(factionData, position1, this);
          if (houseVoteInfo != null && (num3 == 1 || num3 == 2) && num2 == HouseInfoPanel.SelectedHouse)
          {
            bool vote = false;
            if (houseVoteInfo.contains(factionData.factionID, ref vote))
            {
              int numPos = 0;
              int numNeg = 0;
              if (houseVoteInfo.voteTotals != null)
              {
                for (int index = 0; index < houseVoteInfo.voteTotals.Length / 3; ++index)
                {
                  if (houseVoteInfo.voteTotals[index, 0] == factionData.factionID)
                  {
                    numPos = houseVoteInfo.voteTotals[index, 1];
                    numNeg = houseVoteInfo.voteTotals[index, 2];
                    break;
                  }
                }
              }
              control.extendVote(vote, numPos, numNeg, num3 == 1);
            }
          }
          if ((num3 == 1 || num3 == 2) && num2 == HouseInfoPanel.SelectedHouse)
          {
            bool vote = false;
            if (num4 == factionData.factionID)
              vote = true;
            control.extendLeader(vote, num3 == 1);
          }
          this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          num1 += control.Height;
          this.lineList.Add(control);
          ++position1;
        }
      }
      if (num2 == HouseInfoPanel.SelectedHouse && houseVoteInfo != null && houseVoteInfo.applications != null && houseVoteInfo.applications.Length > 0 && (num3 == 1 || num3 == 2))
      {
        HouseInfoPanel.HouseLine control1 = new HouseInfoPanel.HouseLine();
        control1.Position = new Point(0, num1);
        control1.applicationLine();
        this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control1);
        num1 += control1.Height;
        this.lineList.Add(control1);
        int position2 = position1 + 1;
        foreach (int application in houseVoteInfo.applications)
        {
          FactionData faction = GameEngine.Instance.World.getFaction(application);
          bool vote = false;
          if (faction != null && houseVoteInfo.contains(faction.factionID, ref vote))
          {
            int numPos = 0;
            int numNeg = 0;
            if (houseVoteInfo.voteTotals != null)
            {
              for (int index = 0; index < houseVoteInfo.voteTotals.Length / 3; ++index)
              {
                if (houseVoteInfo.voteTotals[index, 0] == faction.factionID)
                {
                  numPos = houseVoteInfo.voteTotals[index, 1];
                  numNeg = houseVoteInfo.voteTotals[index, 2];
                  break;
                }
              }
            }
            HouseInfoPanel.HouseLine control2 = new HouseInfoPanel.HouseLine();
            if (num1 != 0)
              num1 += 5;
            control2.Position = new Point(0, num1);
            control2.init(faction, position2, this);
            control2.extendVote(vote, numPos, numNeg, num3 == 1);
            control2.setAsApplication();
            this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control2);
            num1 += control2.Height;
            this.lineList.Add(control2);
            ++position2;
          }
        }
      }
      this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, num1);
      if (num1 < this.wallScrollBar.Height)
      {
        this.wallScrollBar.Visible = false;
      }
      else
      {
        this.wallScrollBar.Visible = true;
        this.wallScrollBar.NumVisibleLines = this.wallScrollBar.Height;
        this.wallScrollBar.Max = num1 - this.wallScrollBar.Height;
      }
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
      this.updateRelationshipText();
      this.update();
      this.Invalidate();
    }

    private void diplomacyClicked() => this.addDiplomacyOverlay();

    public void addDiplomacyOverlay()
    {
      this.removeOverlay();
      this.greyOverlay.Size = this.Size;
      this.greyOverlay.FillColor = Color.FromArgb(128, 0, 0, 0);
      this.greyOverlay.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => { }));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.greyOverlay);
      this.diplomacyHeaderImage.Size = new Size(400, 40);
      this.diplomacyHeaderImage.Position = new Point((this.Width - 200 - 400) / 2, 100);
      this.greyOverlay.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyHeaderImage);
      this.diplomacyHeaderImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
      this.diplomacyBackgroundImage.Size = new Size(400, 300);
      this.diplomacyBackgroundImage.Position = new Point((this.Width - 200 - 400) / 2, 140);
      this.greyOverlay.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyBackgroundImage);
      this.diplomacyBackgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
      this.diplomacyHeadingLabel.Text = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy");
      this.diplomacyHeadingLabel.Color = ARGBColors.White;
      this.diplomacyHeadingLabel.Position = new Point(0, 0);
      this.diplomacyHeadingLabel.Size = this.diplomacyHeaderImage.Size;
      this.diplomacyHeadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.diplomacyHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.diplomacyHeaderImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyHeadingLabel);
      this.diplomacyFactionLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + HouseInfoPanel.SelectedHouse.ToString();
      this.diplomacyFactionLabel.Color = ARGBColors.Black;
      this.diplomacyFactionLabel.Position = new Point(0, 8);
      this.diplomacyFactionLabel.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
      this.diplomacyFactionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.diplomacyFactionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyFactionLabel);
      this.diplomacyCurrentLabelHeader.Text = SK.Text("GENERIC_Current_Relationship", "Current Relationship");
      this.diplomacyCurrentLabelHeader.Color = ARGBColors.Black;
      this.diplomacyCurrentLabelHeader.Position = new Point(0, 40);
      this.diplomacyCurrentLabelHeader.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
      this.diplomacyCurrentLabelHeader.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.diplomacyCurrentLabelHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyCurrentLabelHeader);
      string str = "";
      int yourHouseRelation = GameEngine.Instance.World.getYourHouseRelation(HouseInfoPanel.SelectedHouse);
      if (yourHouseRelation == 0)
        str += SK.Text("GENERIC_Neutral", "Neutral");
      else if (yourHouseRelation > 0)
        str += SK.Text("GENERIC_Ally", "Ally");
      else if (yourHouseRelation < 0)
        str += SK.Text("GENERIC_Enemy", "Enemy");
      this.diplomacyCurrentLabel.Text = str;
      this.diplomacyCurrentLabel.Color = ARGBColors.Black;
      this.diplomacyCurrentLabel.Position = new Point(0, 65);
      this.diplomacyCurrentLabel.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
      this.diplomacyCurrentLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.diplomacyCurrentLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyCurrentLabel);
      this.diplomacyNeutralButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.diplomacyNeutralButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.diplomacyNeutralButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.diplomacyNeutralButton.Position = new Point(95, 100);
      this.diplomacyNeutralButton.Text.Text = SK.Text("FactionsDiplomacy_Set_as_neutral", "Set As Neutral");
      this.diplomacyNeutralButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.diplomacyNeutralButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.diplomacyNeutralButton.TextYOffset = -3;
      this.diplomacyNeutralButton.Text.Color = ARGBColors.Black;
      this.diplomacyNeutralButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnBreakAlliance_Click), "HouseInfoPanel_neutral");
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyNeutralButton);
      this.diplomacyAllyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.diplomacyAllyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.diplomacyAllyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.diplomacyAllyButton.Position = new Point(95, 150);
      this.diplomacyAllyButton.Text.Text = SK.Text("FactionsDiplomacy_Set_as_ally", "Set As Ally");
      this.diplomacyAllyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.diplomacyAllyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.diplomacyAllyButton.TextYOffset = -3;
      this.diplomacyAllyButton.Text.Color = ARGBColors.Black;
      this.diplomacyAllyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAlly_Click), "HouseInfoPanel_ally");
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyAllyButton);
      this.diplomacyEnemyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.diplomacyEnemyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.diplomacyEnemyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.diplomacyEnemyButton.Position = new Point(95, 200);
      this.diplomacyEnemyButton.Text.Text = SK.Text("FactionsDiplomacy_Set_as_enemy", "Set As Enemy");
      this.diplomacyEnemyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.diplomacyEnemyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.diplomacyEnemyButton.TextYOffset = -3;
      this.diplomacyEnemyButton.Text.Color = ARGBColors.Black;
      this.diplomacyEnemyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnMakeEnemy_Click), "HouseInfoPanel_enemy");
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyEnemyButton);
      this.diplomacyCancelButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.diplomacyCancelButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.diplomacyCancelButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.diplomacyCancelButton.Position = new Point(130, 250);
      this.diplomacyCancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.diplomacyCancelButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.diplomacyCancelButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.diplomacyCancelButton.TextYOffset = -3;
      this.diplomacyCancelButton.Text.Color = ARGBColors.Black;
      this.diplomacyCancelButton.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => this.removeOverlay()), "HouseInfoPanel_cancel");
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyCancelButton);
      this.diplomacyEnemyButton.Enabled = true;
      this.diplomacyAllyButton.Enabled = true;
      this.diplomacyNeutralButton.Enabled = true;
    }

    public void removeOverlay()
    {
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.greyOverlay);
      this.greyOverlay.clearControls();
      this.Invalidate();
    }

    private void btnAlly_Click()
    {
      this.diplomacyEnemyButton.Enabled = false;
      this.diplomacyAllyButton.Enabled = false;
      this.diplomacyNeutralButton.Enabled = false;
      RemoteServices.Instance.set_CreateHouseRelationship_UserCallBack(new RemoteServices.CreateHouseRelationship_UserCallBack(this.createHouseRelationshipCallback));
      RemoteServices.Instance.CreateHouseRelationship(HouseInfoPanel.SelectedHouse, 1);
    }

    private void btnMakeEnemy_Click()
    {
      this.diplomacyEnemyButton.Enabled = false;
      this.diplomacyAllyButton.Enabled = false;
      this.diplomacyNeutralButton.Enabled = false;
      RemoteServices.Instance.set_CreateHouseRelationship_UserCallBack(new RemoteServices.CreateHouseRelationship_UserCallBack(this.createHouseRelationshipCallback));
      RemoteServices.Instance.CreateHouseRelationship(HouseInfoPanel.SelectedHouse, -1);
    }

    private void btnBreakAlliance_Click()
    {
      this.diplomacyEnemyButton.Enabled = false;
      this.diplomacyAllyButton.Enabled = false;
      this.diplomacyNeutralButton.Enabled = false;
      RemoteServices.Instance.set_CreateHouseRelationship_UserCallBack(new RemoteServices.CreateHouseRelationship_UserCallBack(this.createHouseRelationshipCallback));
      RemoteServices.Instance.CreateHouseRelationship(HouseInfoPanel.SelectedHouse, 0);
    }

    private void createHouseRelationshipCallback(CreateHouseRelationship_ReturnType returnData)
    {
      if (returnData.Success)
      {
        GameEngine.Instance.World.HouseAllies = returnData.yourHouseAllies;
        GameEngine.Instance.World.HouseEnemies = returnData.yourHouseEnemies;
        this.init(false);
      }
      else
      {
        this.diplomacyEnemyButton.Enabled = true;
        this.diplomacyAllyButton.Enabled = true;
        this.diplomacyNeutralButton.Enabled = true;
      }
    }

    private void sendProclamation()
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_send_mail");
      InterfaceMgr.Instance.getMainTabBar().selectDummyTabFast(21);
      InterfaceMgr.Instance.sendProclamation(2, GameEngine.Instance.World.YourHouse);
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
      this.clearControls();
      this.closing();
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
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (HouseInfoPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public class HouseLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel factionName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel numPlayersLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage allianceImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
      private int m_position = -1000;
      private FactionData m_factionData;
      private bool m_application;
      private HouseInfoPanel m_parent;
      private CustomSelfDrawPanel.CSDLabel posLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel newLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton posVote = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton negVote = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton leaderVote = new CustomSelfDrawPanel.CSDButton();

      public void init(FactionData factionData, int position, HouseInfoPanel parent)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_factionData = factionData;
        this.m_application = false;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(60, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.flagImage.createFromFlagData(factionData.flagData);
        this.flagImage.Position = new Point(0, 0);
        this.flagImage.Scale = 0.25;
        this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.flagImage);
        NumberFormatInfo nfi = GameEngine.NFI;
        this.factionName.Text = factionData.factionName;
        this.factionName.Color = ARGBColors.Black;
        this.factionName.Position = new Point(9, 0);
        this.factionName.Size = new Size(220, this.backgroundImage.Height);
        this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.factionName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionName);
        this.pointsLabel.Text = factionData.points.ToString("N", (IFormatProvider) nfi);
        this.pointsLabel.Color = ARGBColors.Black;
        this.pointsLabel.Position = new Point(235, 0);
        this.pointsLabel.Size = new Size(100, this.backgroundImage.Height);
        this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.pointsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
        this.invalidate();
      }

      public void applicationLine()
      {
        this.clearControls();
        this.factionName.Text = SK.Text("HouseFactionsLine_Application", "Applications");
        this.factionName.Color = ARGBColors.Black;
        this.factionName.Position = new Point(9, 2);
        this.factionName.Size = new Size(GFXLibrary.lineitem_strip_02_light.Width, GFXLibrary.lineitem_strip_02_light.Height);
        this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.factionName);
        this.Size = GFXLibrary.lineitem_strip_02_light.Size;
      }

      public void extendVote(bool vote, int numPos, int numNeg, bool leader)
      {
        if (GameEngine.Instance.World.WorldEnded)
          return;
        NumberFormatInfo nfi = GameEngine.NFI;
        this.posLabel.Text = numPos.ToString("N", (IFormatProvider) nfi);
        this.posLabel.Color = ARGBColors.Black;
        this.posLabel.Position = new Point(329, 0);
        this.posLabel.Size = new Size(100, this.backgroundImage.Height);
        this.posLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.posLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.posLabel);
        this.newLabel.Text = numNeg.ToString("N", (IFormatProvider) nfi);
        this.newLabel.Color = ARGBColors.Black;
        this.newLabel.Position = new Point(389, 0);
        this.newLabel.Size = new Size(100, this.backgroundImage.Height);
        this.newLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.newLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.newLabel);
        if (!vote)
        {
          this.posVote.ImageNorm = (Image) GFXLibrary.radio_green[2];
          this.posVote.ImageOver = (Image) GFXLibrary.radio_green[1];
          this.posVote.ImageClick = (Image) GFXLibrary.radio_green[1];
          this.posVote.Position = new Point(434, 7);
          if (leader)
          {
            this.posVote.Active = true;
            this.posVote.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.posVoteClicked), "HouseInfoPanel_");
          }
          else
            this.posVote.Active = false;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.posVote);
          this.negVote.ImageNorm = (Image) GFXLibrary.radio_green[0];
          this.negVote.ImageOver = (Image) GFXLibrary.radio_green[0];
          this.negVote.ImageClick = (Image) GFXLibrary.radio_green[0];
          this.negVote.Active = false;
          this.negVote.Position = new Point(494, 7);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.negVote);
        }
        else
        {
          this.posVote.ImageNorm = (Image) GFXLibrary.radio_green[0];
          this.posVote.ImageOver = (Image) GFXLibrary.radio_green[0];
          this.posVote.ImageClick = (Image) GFXLibrary.radio_green[0];
          this.posVote.Active = false;
          this.posVote.Position = new Point(434, 7);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.posVote);
          this.negVote.ImageNorm = (Image) GFXLibrary.radio_green[2];
          this.negVote.ImageOver = (Image) GFXLibrary.radio_green[1];
          this.negVote.ImageClick = (Image) GFXLibrary.radio_green[1];
          this.negVote.Position = new Point(494, 7);
          if (leader)
          {
            this.negVote.Active = true;
            this.negVote.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.negVoteClicked), "HouseInfoPanel_");
          }
          else
            this.negVote.Active = false;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.negVote);
        }
      }

      public void extendLeader(bool vote, bool leader)
      {
        if (!vote)
        {
          this.leaderVote.ImageNorm = (Image) GFXLibrary.radio_green[2];
          this.leaderVote.ImageOver = (Image) GFXLibrary.radio_green[1];
          this.leaderVote.ImageClick = (Image) GFXLibrary.radio_green[1];
          this.leaderVote.Position = new Point(598, 7);
          if (leader)
          {
            this.leaderVote.Active = true;
            this.leaderVote.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.leaderClicked), "HouseInfoPanel_");
          }
          else
            this.leaderVote.Active = false;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.leaderVote);
        }
        else
        {
          this.leaderVote.ImageNorm = (Image) GFXLibrary.radio_green[0];
          this.leaderVote.ImageOver = (Image) GFXLibrary.radio_green[0];
          this.leaderVote.ImageClick = (Image) GFXLibrary.radio_green[0];
          this.leaderVote.Active = false;
          this.leaderVote.Position = new Point(598, 7);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.leaderVote);
        }
      }

      public void setAsApplication() => this.m_application = true;

      public void update()
      {
      }

      public void clickedLine()
      {
        GameEngine.Instance.playInterfaceSound("HouseInfoPanel_faction");
        InterfaceMgr.Instance.showFactionPanel(this.m_factionData.factionID);
      }

      public void posVoteClicked()
      {
        this.posVote.Active = false;
        this.m_parent.houseVote(this.m_factionData.factionID, this.m_application, true);
      }

      public void negVoteClicked()
      {
        this.negVote.Active = false;
        this.m_parent.houseVote(this.m_factionData.factionID, this.m_application, false);
      }

      public void leaderClicked()
      {
        this.m_parent.houseVoteHouseLeader(this.m_factionData.factionID);
      }
    }
  }
}
