// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionMyFactionPanel
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
  public class FactionMyFactionPanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int PANEL_ID = 42;
    private DockableControl dockableControl;
    private IContainer components;
    public static FactionMyFactionPanel instance = (FactionMyFactionPanel) null;
    private static int m_selectedFaction = -1;
    private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDExtendingPanel wallInfoImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel playerNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel villagesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel factionNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel factionMottoLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel houseLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel membersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel membersLabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pointsHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pointsHeaderLabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rankHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rankHeaderLabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDFactionFlagImage flagimage = new CustomSelfDrawPanel.CSDFactionFlagImage();
    private CustomSelfDrawPanel.CSDLabel diplomacyLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton diplomacyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDImage backImage1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage backImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
    private List<FactionMyFactionPanel.FactionMemberLine> lineList = new List<FactionMyFactionPanel.FactionMemberLine>();
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
    private bool diplomacyOverlayVisible;

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
      this.Name = nameof (FactionMyFactionPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public static int SelectedFaction
    {
      set => FactionMyFactionPanel.m_selectedFaction = value;
      get => FactionMyFactionPanel.m_selectedFaction;
    }

    public FactionMyFactionPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      FactionMyFactionPanel.instance = this;
      this.clearControls();
      NumberFormatInfo nfi = GameEngine.NFI;
      this.sidebar.addSideBar(1, (CustomSelfDrawPanel) this);
      FactionData factionData = GameEngine.Instance.World.getFaction(FactionMyFactionPanel.m_selectedFaction) ?? new FactionData();
      this.greyOverlay.Size = this.Size;
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
      this.barImage1.Position = new Point(276, 70);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.barImage1);
      this.barImage2.Image = (Image) GFXLibrary.faction_bar_tan_1_lighter;
      this.barImage2.Position = new Point(276, 94);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.barImage2);
      this.barImage3.Image = (Image) GFXLibrary.faction_bar_tan_1_heavier;
      this.barImage3.Position = new Point(276, 118);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.barImage3);
      this.factionNameLabel.Text = factionData.factionName;
      this.factionNameLabel.Color = ARGBColors.Black;
      this.factionNameLabel.Position = new Point(205, 10);
      this.factionNameLabel.Size = new Size(600, 40);
      this.factionNameLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Regular);
      this.factionNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionNameLabel);
      this.factionMottoLabel.Text = "\"" + factionData.factionMotto + "\"";
      this.factionMottoLabel.Color = ARGBColors.Black;
      this.factionMottoLabel.Position = new Point(205, 41);
      this.factionMottoLabel.Size = new Size(600, 40);
      this.factionMottoLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.factionMottoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionMottoLabel);
      if (factionData.houseID > 0)
      {
        this.houseLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + factionData.houseID.ToString();
        this.houseLabel.Color = ARGBColors.Black;
        this.houseLabel.Position = new Point(575, 110);
        this.houseLabel.Size = new Size(200, 50);
        this.houseLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.houseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.houseLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "FactionMyFactionPanel_house");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseLabel);
        this.houseImage.Image = (Image) GFXLibrary.getHouseCircleLargeImage(factionData.houseID - 1);
        this.houseImage.Position = new Point(675 - this.houseImage.Image.Width / 2, 65 - this.houseImage.Image.Height / 2 + 8);
        this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "FactionMyFactionPanel_house");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseImage);
      }
      this.membersLabel.Text = SK.Text("FactionInvites_Members", "Members");
      this.membersLabel.Color = ARGBColors.Black;
      this.membersLabel.Position = new Point(284, 73);
      this.membersLabel.Size = new Size(600, 40);
      this.membersLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.membersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.membersLabel);
      this.membersLabelValue.Text = factionData.numMembers.ToString();
      this.membersLabelValue.Color = ARGBColors.Black;
      this.membersLabelValue.Position = new Point(30, 73);
      this.membersLabelValue.Size = new Size(482, 40);
      this.membersLabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.membersLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.membersLabelValue);
      this.rankHeaderLabel.Text = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank");
      this.rankHeaderLabel.Color = ARGBColors.Black;
      this.rankHeaderLabel.Position = new Point(284, 121);
      this.rankHeaderLabel.Size = new Size(600, 40);
      this.rankHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.rankHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.rankHeaderLabel);
      this.rankHeaderLabelValue.Text = (GameEngine.Instance.World.getFactionRank(FactionMyFactionPanel.m_selectedFaction) + 1).ToString("N", (IFormatProvider) nfi);
      this.rankHeaderLabelValue.Color = ARGBColors.Black;
      this.rankHeaderLabelValue.Position = new Point(30, 121);
      this.rankHeaderLabelValue.Size = new Size(482, 40);
      this.rankHeaderLabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.rankHeaderLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.rankHeaderLabelValue);
      this.pointsHeaderLabel.Text = SK.Text("FactionsPanel_Points", "Points");
      this.pointsHeaderLabel.Color = ARGBColors.Black;
      this.pointsHeaderLabel.Position = new Point(284, 97);
      this.pointsHeaderLabel.Size = new Size(600, 40);
      this.pointsHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.pointsHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsHeaderLabel);
      this.pointsHeaderLabelValue.Text = factionData.points.ToString("N", (IFormatProvider) nfi);
      this.pointsHeaderLabelValue.Color = ARGBColors.Black;
      this.pointsHeaderLabelValue.Position = new Point(30, 97);
      this.pointsHeaderLabelValue.Size = new Size(482, 40);
      this.pointsHeaderLabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.pointsHeaderLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsHeaderLabelValue);
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
      this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider3Image.Position = new Point(610, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
      this.playerNameLabel.Text = SK.Text("UserInfoPanel_", "Player Name");
      this.playerNameLabel.Color = ARGBColors.Black;
      this.playerNameLabel.Position = new Point(9, -2);
      this.playerNameLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.playerNameLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.playerNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.playerNameLabel);
      this.pointsLabel.Text = SK.Text("FactionsPanel_Points", "Points");
      this.pointsLabel.Color = ARGBColors.Black;
      this.pointsLabel.Position = new Point(295, -2);
      this.pointsLabel.Size = new Size(140, this.headerLabelsImage.Height);
      this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
      this.rankLabel.Text = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank");
      this.rankLabel.Color = ARGBColors.Black;
      this.rankLabel.Position = new Point(445, -2);
      this.rankLabel.Size = new Size(223, this.headerLabelsImage.Height);
      this.rankLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.rankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.rankLabel);
      this.villagesLabel.Text = SK.Text("UserInfoPanel_Villages", "Villages");
      this.villagesLabel.Color = ARGBColors.Black;
      this.villagesLabel.Position = new Point(615, -2);
      this.villagesLabel.Size = new Size(110, this.headerLabelsImage.Height);
      this.villagesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.villagesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.villagesLabel);
      this.flagimage.createFromFlagData(factionData.flagData);
      this.flagimage.Position = new Point(35, 6);
      this.flagimage.Scale = 0.5;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.flagimage);
      if (factionData.factionID == RemoteServices.Instance.UserFactionID)
        InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_My_Faction_Details", "My Faction Details"));
      else
        InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_Faction_Details", "Faction Details"));
      if (RemoteServices.Instance.UserFactionID < 0 && GameEngine.Instance.World.alreadyGotFactionApplication(factionData.factionID))
      {
        this.diplomacyLabel.Text = SK.Text("FactionInvites_Application Pending", "Application Pending");
        this.diplomacyLabel.Color = ARGBColors.Black;
        this.diplomacyLabel.Position = new Point(24, 126);
        this.diplomacyLabel.Size = new Size(240, 40);
        this.diplomacyLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.diplomacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyLabel);
      }
      else if (RemoteServices.Instance.UserFactionID < 0 && factionData.openForApplications && !GameEngine.Instance.World.alreadyGotFactionApplication(factionData.factionID) && !GameEngine.Instance.World.WorldEnded)
      {
        if (GameEngine.Instance.World.getRank() >= 6)
        {
          this.diplomacyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
          this.diplomacyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
          this.diplomacyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
          this.diplomacyButton.Position = new Point(24, 126);
          this.diplomacyButton.Text.Text = SK.Text("FactionInvites_Apply", "Apply To Join");
          this.diplomacyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.diplomacyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          this.diplomacyButton.TextYOffset = -3;
          this.diplomacyButton.Text.Color = ARGBColors.Black;
          this.diplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.applyClicked), "FactionMyFactionPanel_diplomacy");
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyButton);
        }
      }
      else if (factionData.factionID != RemoteServices.Instance.UserFactionID && !GameEngine.Instance.World.WorldEnded)
      {
        if (GameEngine.Instance.World.getYourFactionRank() == 1)
        {
          this.diplomacyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
          this.diplomacyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
          this.diplomacyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
          this.diplomacyButton.Position = new Point(24, 126);
          this.diplomacyButton.Text.Text = "";
          this.diplomacyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.diplomacyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          this.diplomacyButton.TextYOffset = -3;
          this.diplomacyButton.Text.Color = ARGBColors.Black;
          this.diplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.diplomacyClicked), "FactionMyFactionPanel_diplomacy");
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyButton);
        }
        else
        {
          this.diplomacyLabel.Text = "";
          this.diplomacyLabel.Color = ARGBColors.Black;
          this.diplomacyLabel.Position = new Point(24, 126);
          this.diplomacyLabel.Size = new Size(240, 40);
          this.diplomacyLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
          this.diplomacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyLabel);
        }
      }
      this.wallScrollArea.Position = new Point(25, 188);
      this.wallScrollArea.Size = new Size(705, height - 38 - 150);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(705, height - 38 - 150));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      int num = this.wallScrollBar.Value;
      this.wallScrollBar.Position = new Point(733, 188);
      this.wallScrollBar.Size = new Size(24, height - 38 - 150);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      bool uptodate = false;
      FactionMemberData[] factionMemberDataArray = GameEngine.Instance.World.getFactionMemberData(FactionMyFactionPanel.m_selectedFaction, ref uptodate);
      if (!resized)
      {
        factionMemberDataArray = this.addAIWorldFactionMemberData(factionMemberDataArray, factionData.factionID, ref uptodate);
        if (!uptodate)
        {
          RemoteServices.Instance.set_GetViewFactionData_UserCallBack(new RemoteServices.GetViewFactionData_UserCallBack(this.getViewFactionDataCallback));
          RemoteServices.Instance.GetViewFactionData(FactionMyFactionPanel.m_selectedFaction);
        }
        this.diplomacyOverlayVisible = false;
      }
      this.addPlayers(factionMemberDataArray);
      if (!resized || !this.diplomacyOverlayVisible)
        return;
      this.addDiplomacyOverlay();
    }

    private FactionMemberData[] addAIWorldFactionMemberData(
      FactionMemberData[] memberData,
      int factionID,
      ref bool uptodate)
    {
      if (GameEngine.Instance.LocalWorldData.AIWorld)
      {
        switch (factionID)
        {
          case 1:
          case 2:
          case 3:
            List<FactionMemberData> factionMemberDataList = new List<FactionMemberData>();
            FactionMemberData factionMemberData1 = new FactionMemberData();
            switch (factionID - 1)
            {
              case 0:
                factionMemberData1.userID = 1;
                factionMemberData1.userName = "The Rat";
                factionMemberData1.status = 1;
                factionMemberData1.numVillages = GameEngine.Instance.World.countRatsCastles();
                break;
              case 1:
                factionMemberData1.userID = 2;
                factionMemberData1.userName = "The Snake";
                factionMemberData1.status = 1;
                factionMemberData1.numVillages = GameEngine.Instance.World.countSnakesCastles();
                break;
              case 2:
                factionMemberData1.userID = 3;
                factionMemberData1.userName = "The Pig";
                factionMemberData1.status = 1;
                factionMemberData1.numVillages = GameEngine.Instance.World.countPigsCastles();
                break;
              case 3:
                factionMemberData1.userID = 4;
                factionMemberData1.userName = "The Wolf";
                factionMemberData1.status = 1;
                factionMemberData1.numVillages = GameEngine.Instance.World.countWolfsCastles();
                break;
            }
            factionMemberDataList.Add(factionMemberData1);
            if (GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld && factionID == 4)
            {
              foreach (FactionMemberData factionMemberData2 in memberData)
              {
                if (factionMemberData2.userID > 4)
                  factionMemberDataList.Add(factionMemberData2);
              }
            }
            else
              uptodate = true;
            memberData = factionMemberDataList.ToArray();
            break;
          case 4:
            if (memberData == null)
              return (FactionMemberData[]) null;
            goto case 1;
        }
      }
      return memberData;
    }

    public void update() => this.sidebar.update();

    public void logout()
    {
    }

    public void updateRelationshipText()
    {
      if (RemoteServices.Instance.UserFactionID < 0 || FactionMyFactionPanel.m_selectedFaction == RemoteServices.Instance.UserFactionID)
      {
        FactionData faction = GameEngine.Instance.World.getFaction(FactionMyFactionPanel.m_selectedFaction);
        if (RemoteServices.Instance.UserFactionID < 0 && faction != null && GameEngine.Instance.World.alreadyGotFactionApplication(faction.factionID))
        {
          this.diplomacyButton.Visible = false;
          this.diplomacyLabel.Visible = true;
        }
        else if (RemoteServices.Instance.UserFactionID < 0 && faction != null && faction.openForApplications && !GameEngine.Instance.World.alreadyGotFactionApplication(faction.factionID))
        {
          if (GameEngine.Instance.World.getRank() >= 6)
            this.diplomacyButton.Visible = true;
          else
            this.diplomacyButton.Visible = false;
          this.diplomacyLabel.Visible = false;
        }
        else
        {
          this.diplomacyButton.Visible = false;
          this.diplomacyLabel.Visible = false;
        }
      }
      else
      {
        string str = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy") + " : ";
        this.diplomacyButton.Visible = true;
        this.diplomacyLabel.Visible = true;
        int num = 0;
        if (FactionMyFactionPanel.m_selectedFaction >= 0)
          num = GameEngine.Instance.World.getYourFactionRelation(FactionMyFactionPanel.m_selectedFaction);
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

    public void houseClicked()
    {
      FactionData faction = GameEngine.Instance.World.getFaction(FactionMyFactionPanel.m_selectedFaction);
      if (faction == null || faction.houseID <= 0)
        return;
      InterfaceMgr.Instance.showHousePanel(faction.houseID);
    }

    private void getViewFactionDataCallback(GetViewFactionData_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      NumberFormatInfo nfi = GameEngine.NFI;
      if (returnData.factionData != null)
      {
        bool uptodate = true;
        FactionMemberData[] fmd = this.addAIWorldFactionMemberData(returnData.members, returnData.factionData.factionID, ref uptodate);
        this.addPlayers(fmd);
        if (GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld && returnData.factionData.factionID == 4)
        {
          returnData.factionData.numMembers = fmd.Length;
          returnData.factionData.flagData = 941809835;
          returnData.factionData.houseRank = 10;
        }
        GameEngine.Instance.World.setFactionMemberData(returnData.factionData.factionID, returnData.members);
        GameEngine.Instance.World.setFactionData(returnData.factionData);
      }
      GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
      GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
    }

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

    public void addPlayers(FactionMemberData[] fmd)
    {
      this.wallScrollArea.clearControls();
      int num1 = 0;
      this.lineList.Clear();
      int position = 0;
      if (fmd != null)
      {
        if (FactionMyFactionPanel.m_selectedFaction != RemoteServices.Instance.UserFactionID)
        {
          for (int index = 0; index < 3; ++index)
          {
            int num2 = 1;
            if (index == 1)
              num2 = 2;
            else if (index == 2)
              num2 = 0;
            foreach (FactionMemberData factionData in fmd)
            {
              if (factionData.status == num2)
              {
                FactionMyFactionPanel.FactionMemberLine control = new FactionMyFactionPanel.FactionMemberLine();
                if (num1 != 0)
                  num1 += 5;
                control.Position = new Point(0, num1);
                control.init(factionData, position, this, false);
                this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
                num1 += control.Height;
                this.lineList.Add(control);
                ++position;
              }
            }
          }
        }
        else
        {
          for (int index = 0; index < 3; ++index)
          {
            int num3 = 1;
            if (index == 1)
              num3 = 2;
            else if (index == 2)
              num3 = 0;
            else if (index == 3)
              num3 = -1;
            foreach (FactionMemberData factionData in fmd)
            {
              if (factionData.status == num3)
              {
                FactionMyFactionPanel.FactionMemberLine control = new FactionMyFactionPanel.FactionMemberLine();
                if (num1 != 0)
                  num1 += 5;
                control.Position = new Point(0, num1);
                control.init(factionData, position, this, true);
                this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
                num1 += control.Height;
                this.lineList.Add(control);
                ++position;
              }
            }
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

    public void applyClicked()
    {
      this.diplomacyButton.Enabled = false;
      GameEngine.Instance.factionManager.ApplyToFaction(FactionMyFactionPanel.SelectedFaction, new FactionManager.FactionInfoUpdatedCallback(this.factionApplicationCallback));
    }

    public void factionApplicationCallback(bool success)
    {
      this.diplomacyButton.Enabled = true;
      if (!success)
        return;
      this.diplomacyButton.Visible = false;
      this.diplomacyLabel.Text = SK.Text("FactionInvites_Application Pending", "Application Pending");
      this.diplomacyLabel.Color = ARGBColors.Black;
      this.diplomacyLabel.Position = new Point(24, 126);
      this.diplomacyLabel.Size = new Size(240, 40);
      this.diplomacyLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.diplomacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyLabel);
      this.diplomacyLabel.Visible = true;
      int num = (int) MyMessageBox.Show(SK.Text("FactionInvites_Have_Applied", "You have now applied to join a faction.  Click on the Invites tab to view your current applications."), SK.Text("FactionInvites_Faction_Application", "Faction Application"));
    }

    private void diplomacyClicked() => this.addDiplomacyOverlay();

    public void addDiplomacyOverlay()
    {
      FactionData faction = GameEngine.Instance.World.getFaction(FactionMyFactionPanel.m_selectedFaction);
      if (faction == null)
        return;
      this.removeOverlay();
      this.diplomacyOverlayVisible = true;
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
      this.diplomacyFactionLabel.Text = faction.factionName;
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
      int yourFactionRelation = GameEngine.Instance.World.getYourFactionRelation(FactionMyFactionPanel.m_selectedFaction);
      if (yourFactionRelation == 0)
        str += SK.Text("GENERIC_Neutral", "Neutral");
      else if (yourFactionRelation > 0)
        str += SK.Text("GENERIC_Ally", "Ally");
      else if (yourFactionRelation < 0)
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
      this.diplomacyNeutralButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnBreakAlliance_Click), "FactionMyFactionPanel_neutral_clicked");
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
      this.diplomacyAllyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAlly_Click), "FactionMyFactionPanel_ally_clicked");
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
      this.diplomacyEnemyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnMakeEnemy_Click), "FactionMyFactionPanel_enemy_clicked");
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
      this.diplomacyCancelButton.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => this.removeOverlay()), "FactionMyFactionPanel_dipomacy_close");
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
      this.diplomacyOverlayVisible = false;
    }

    private void btnAlly_Click()
    {
      this.diplomacyEnemyButton.Enabled = false;
      this.diplomacyAllyButton.Enabled = false;
      this.diplomacyNeutralButton.Enabled = false;
      RemoteServices.Instance.set_CreateFactionRelationship_UserCallBack(new RemoteServices.CreateFactionRelationship_UserCallBack(this.createFactionRelationshipCallback));
      RemoteServices.Instance.CreateFactionRelationship(FactionMyFactionPanel.m_selectedFaction, 1);
    }

    private void btnMakeEnemy_Click()
    {
      this.diplomacyEnemyButton.Enabled = false;
      this.diplomacyAllyButton.Enabled = false;
      this.diplomacyNeutralButton.Enabled = false;
      RemoteServices.Instance.set_CreateFactionRelationship_UserCallBack(new RemoteServices.CreateFactionRelationship_UserCallBack(this.createFactionRelationshipCallback));
      RemoteServices.Instance.CreateFactionRelationship(FactionMyFactionPanel.m_selectedFaction, -1);
    }

    private void btnBreakAlliance_Click()
    {
      this.diplomacyEnemyButton.Enabled = false;
      this.diplomacyAllyButton.Enabled = false;
      this.diplomacyNeutralButton.Enabled = false;
      RemoteServices.Instance.set_CreateFactionRelationship_UserCallBack(new RemoteServices.CreateFactionRelationship_UserCallBack(this.createFactionRelationshipCallback));
      RemoteServices.Instance.CreateFactionRelationship(FactionMyFactionPanel.m_selectedFaction, 0);
    }

    private void createFactionRelationshipCallback(CreateFactionRelationship_ReturnType returnData)
    {
      if (returnData.Success)
      {
        GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
        GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
        this.diplomacyOverlayVisible = false;
        this.init(false);
      }
      else
      {
        this.diplomacyEnemyButton.Enabled = true;
        this.diplomacyAllyButton.Enabled = true;
        this.diplomacyNeutralButton.Enabled = true;
      }
    }

    public class FactionMemberLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage officerImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel rankName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel villageLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton declineButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDImage onlineImage = new CustomSelfDrawPanel.CSDImage();
      private int m_position = -1000;
      private FactionMemberData m_factionMemberData;
      private FactionMyFactionPanel m_parent;

      public void init(
        FactionMemberData factionData,
        int position,
        FactionMyFactionPanel parent,
        bool ownFaction)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_factionMemberData = factionData;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        NumberFormatInfo nfi = GameEngine.NFI;
        if (factionData.status == 1 || factionData.status == 2)
        {
          if (factionData.status == 1)
          {
            this.officerImage.Image = (Image) GFXLibrary.faction_leaders[1];
            this.officerImage.CustomTooltipID = 2305;
          }
          else
          {
            this.officerImage.Image = (Image) GFXLibrary.faction_leaders[0];
            this.officerImage.CustomTooltipID = 2306;
          }
          this.officerImage.Position = new Point(9, 2);
          this.officerImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.officerImage);
        }
        this.playerName.Text = factionData.userName;
        this.playerName.Color = factionData.status == -1 ? ARGBColors.DarkRed : ARGBColors.Black;
        this.playerName.Position = new Point(69, 0);
        this.playerName.Size = new Size(250, this.backgroundImage.Height);
        this.playerName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.playerName);
        if (factionData.status != -1)
        {
          this.pointsLabel.Text = factionData.totalPoints.ToString("N", (IFormatProvider) nfi);
          this.pointsLabel.Color = ARGBColors.Black;
          this.pointsLabel.Position = new Point(300, 0);
          this.pointsLabel.Size = new Size(85, this.backgroundImage.Height);
          this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
          this.pointsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
          this.rankName.Text = Rankings.getRankingName(factionData.rank, factionData.male);
          this.rankName.Color = ARGBColors.Black;
          this.rankName.Position = new Point(450, 0);
          this.rankName.Size = new Size(150, this.backgroundImage.Height);
          this.rankName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          this.rankName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          this.rankName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.rankName);
          this.villageLabel.Text = factionData.numVillages.ToString("N", (IFormatProvider) nfi);
          this.villageLabel.Color = ARGBColors.Black;
          this.villageLabel.Position = new Point(620, 0);
          this.villageLabel.Size = new Size(55, this.backgroundImage.Height);
          this.villageLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          this.villageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
          this.villageLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageLabel);
          if (factionData.online)
          {
            this.onlineImage.Image = (Image) GFXLibrary.radio_green[0];
            this.onlineImage.Position = new Point(280, 5);
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.onlineImage);
          }
        }
        else
        {
          this.pointsLabel.Text = SK.Text("FactionsInvites_Invite_Pending", "Invitation Pending");
          this.pointsLabel.Color = ARGBColors.DarkRed;
          this.pointsLabel.Position = new Point(300, 0);
          this.pointsLabel.Size = new Size(500, this.backgroundImage.Height);
          this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          this.pointsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
          switch (GameEngine.Instance.World.getYourFactionRank())
          {
            case 1:
            case 2:
              this.declineButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
              this.declineButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
              this.declineButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
              this.declineButton.Position = new Point(560, 0);
              this.declineButton.Text.Text = SK.Text("FactionMemberLine_Cancel_Invite", "Cancel Invite");
              this.declineButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              this.declineButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
              this.declineButton.TextYOffset = -3;
              this.declineButton.Text.Color = ARGBColors.Black;
              this.declineButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.declineClicked), "FactionMyFactionPanel_declined_clicked");
              this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.declineButton);
              break;
          }
        }
        this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(factionData.userID, 25, 28);
        if (this.shieldImage.Image != null)
        {
          this.shieldImage.Position = new Point(39, 1);
          this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.shieldImage.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
        }
        this.invalidate();
      }

      public void update()
      {
      }

      public void clickedLine()
      {
        if (this.m_factionMemberData.userID < 0)
          return;
        GameEngine.Instance.playInterfaceSound("FactionMyFactionPanel_user_clicked");
        InterfaceMgr.Instance.showUserInfoScreen(new WorldMap.CachedUserInfo()
        {
          userID = this.m_factionMemberData.userID
        });
      }

      public void declineClicked()
      {
        this.declineButton.Enabled = false;
        RemoteServices.Instance.set_FactionWithdrawInvite_UserCallBack(new RemoteServices.FactionWithdrawInvite_UserCallBack(this.factionWithdrawInviteCallback));
        RemoteServices.Instance.FactionWithdrawInvite(this.m_factionMemberData.userID);
      }

      public void factionWithdrawInviteCallback(FactionWithdrawInvite_ReturnType returnData)
      {
        this.declineButton.Enabled = true;
        if (returnData.members == null)
          return;
        GameEngine.Instance.World.FactionMembers = returnData.members;
        this.m_parent.init(false);
      }
    }
  }
}
