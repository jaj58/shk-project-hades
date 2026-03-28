// Decompiled with JetBrains decompiler
// Type: Kingdoms.HouseListPanel
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
  public class HouseListPanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int PANEL_ID = 51;
    public static HouseListPanel instance;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel houseLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel playersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDArea houseSortArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea factionsSortArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea pointsSortArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton factionInfoButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton gloryInfoButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
    private int sortMethod = -1;
    private int pageMode;
    private List<HouseListPanel.HouseLine> lineList = new List<HouseListPanel.HouseLine>();
    private HouseListPanel.NamePosComparer namePosComparer = new HouseListPanel.NamePosComparer();
    private HouseListPanel.NameNegComparer nameNegComparer = new HouseListPanel.NameNegComparer();
    private HouseListPanel.PlayersPosComparer playersPosComparer = new HouseListPanel.PlayersPosComparer();
    private HouseListPanel.PlayersNegComparer playersNegComparer = new HouseListPanel.PlayersNegComparer();
    private HouseListPanel.PointsPosComparer pointsPosComparer = new HouseListPanel.PointsPosComparer();
    private HouseListPanel.PointsNegComparer pointsNegComparer = new HouseListPanel.PointsNegComparer();
    private HouseListPanel.GloryPosComparer gloryPosComparer = new HouseListPanel.GloryPosComparer();
    private HouseListPanel.GloryNegComparer gloryNegComparer = new HouseListPanel.GloryNegComparer();
    private DockableControl dockableControl;
    private IContainer components;

    public HouseListPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      HouseListPanel.instance = this;
      this.clearControls();
      if (GameEngine.Instance.World.testGloryPointsUpdate())
      {
        RemoteServices.Instance.set_GetHouseGloryPoints_UserCallBack(new RemoteServices.GetHouseGloryPoints_UserCallBack(this.GetHouseGloryPointsCallBack));
        RemoteServices.Instance.GetHouseGloryPoints();
      }
      this.sidebar.addSideBar(7, (CustomSelfDrawPanel) this);
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(this.Width - 200, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width - 200, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23 - 200, 28);
      this.headerLabelsImage.Position = new Point(25, 39);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider1Image.Position = new Point(250, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider1Image);
      this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider2Image.Position = new Point(400, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
      this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider3Image.Position = new Point(560, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
      this.houseLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House");
      this.houseLabel.Color = ARGBColors.Black;
      this.houseLabel.Position = new Point(9, -2);
      this.houseLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.houseLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.houseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseLabel);
      this.houseSortArea.Position = new Point(0, 0);
      this.houseSortArea.Size = new Size(250, this.headerLabelsImage.Height);
      this.houseSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortNameClick), "HouseListPanel_sort_house");
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseSortArea);
      this.playersLabel.Text = this.pageMode != 0 ? SK.Text("FactionInvites_Glory_Rank", "Glory Rank") : SK.Text("GENERIC_Factions", "Factions");
      this.playersLabel.Color = ARGBColors.Black;
      this.playersLabel.Position = new Point((int) byte.MaxValue, -2);
      this.playersLabel.Size = new Size(130, this.headerLabelsImage.Height);
      this.playersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.playersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.playersLabel);
      this.factionsSortArea.Position = new Point(250, 0);
      this.factionsSortArea.Size = new Size(150, this.headerLabelsImage.Height);
      this.factionsSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortFactionsClick), "HouseListPanel_sort_faction");
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionsSortArea);
      this.pointsLabel.Text = this.pageMode != 0 ? SK.Text("FactionInvites_Glory_Points", "Glory Points") : SK.Text("FactionsPanel_Points", "Points");
      this.pointsLabel.Color = ARGBColors.Black;
      this.pointsLabel.Position = new Point(405, -2);
      this.pointsLabel.Size = new Size(160, this.headerLabelsImage.Height);
      this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
      this.pointsSortArea.Position = new Point(400, 0);
      this.pointsSortArea.Size = new Size(160, this.headerLabelsImage.Height);
      this.pointsSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortPointsClick), "HouseListPanel_sort_points");
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsSortArea);
      this.membershipLabel.Text = SK.Text("FactionInvites_Membership", "Membership");
      this.membershipLabel.Color = ARGBColors.Black;
      this.membershipLabel.Position = new Point(565, -2);
      this.membershipLabel.Size = new Size(175, this.headerLabelsImage.Height);
      this.membershipLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.membershipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.membershipLabel);
      this.factionInfoButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.factionInfoButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.factionInfoButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.factionInfoButton.Position = new Point(100, 5);
      this.factionInfoButton.Text.Text = SK.Text("HouseInfoPanel_Faction_Info", "Faction Info");
      this.factionInfoButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.factionInfoButton.TextYOffset = -3;
      if (this.pageMode == 0)
      {
        this.factionInfoButton.Text.Color = ARGBColors.White;
        this.factionInfoButton.Text.DropShadowColor = ARGBColors.Black;
      }
      else
      {
        this.factionInfoButton.Text.Color = ARGBColors.Black;
        this.factionInfoButton.Text.clearDropShadow();
      }
      this.factionInfoButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionInfoClick), "HouseInfoPanel_leave");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionInfoButton);
      this.gloryInfoButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.gloryInfoButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.gloryInfoButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.gloryInfoButton.Position = new Point(470, 5);
      this.gloryInfoButton.Text.Text = SK.Text("HouseInfoPanel_Glory_Info", "Glory Info");
      this.gloryInfoButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.gloryInfoButton.TextYOffset = -3;
      if (this.pageMode == 1)
      {
        this.gloryInfoButton.Text.Color = ARGBColors.White;
        this.gloryInfoButton.Text.DropShadowColor = ARGBColors.Black;
      }
      else
      {
        this.gloryInfoButton.Text.Color = ARGBColors.Black;
        this.gloryInfoButton.Text.clearDropShadow();
      }
      this.gloryInfoButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.gloryInfoClick), "HouseInfoPanel_leave");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.gloryInfoButton);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("HouseInfo_All_Houses", "All Houses"));
      this.wallScrollArea.Position = new Point(25, 68);
      this.wallScrollArea.Size = new Size(705, height - 38 - 30);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(705, height - 38 - 30));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      int num1 = this.wallScrollBar.Value;
      this.wallScrollBar.Visible = false;
      this.wallScrollBar.Position = new Point(733, 68);
      this.wallScrollBar.Size = new Size(24, height - 38 - 30);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      int num2 = resized ? 1 : 0;
      this.addFactions();
    }

    public void update() => this.sidebar.update();

    public void logout()
    {
      this.sortMethod = -1;
      this.pageMode = 0;
    }

    public void GetHouseGloryPointsCallBack(GetHouseGloryPoints_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      GameEngine.Instance.World.HouseGloryPoints = returnData.gloryPoints;
      GameEngine.Instance.World.HouseGloryRoundData = returnData.gloryRoundData;
    }

    private void factionInfoClick()
    {
      this.pageMode = 0;
      this.init(true);
    }

    private void gloryInfoClick()
    {
      this.pageMode = 1;
      this.init(true);
    }

    private void wallScrollBarMoved()
    {
      int y = this.wallScrollBar.Value;
      this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 68 - y);
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

    public void addFactions()
    {
      int yourFactionRank = GameEngine.Instance.World.getYourFactionRank();
      FactionData yourFaction = GameEngine.Instance.World.YourFaction;
      int yourHouseID = 0;
      if (yourFaction != null)
        yourHouseID = yourFaction.houseID;
      int appliedToHouse = 0;
      if (GameEngine.Instance.World.HouseVoteInfo != null && GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID > 0)
        appliedToHouse = GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID;
      this.wallScrollArea.clearControls();
      int num = 0;
      this.lineList.Clear();
      int position = 0;
      HouseData[] houseInfo = GameEngine.Instance.World.HouseInfo;
      List<HouseData> houseDataList = new List<HouseData>();
      foreach (HouseData houseData in houseInfo)
        houseDataList.Add(houseData);
      switch (this.sortMethod)
      {
        case 0:
          houseDataList.Sort((IComparer<HouseData>) this.namePosComparer);
          break;
        case 1:
          houseDataList.Sort((IComparer<HouseData>) this.nameNegComparer);
          break;
        case 2:
          if (this.pageMode == 0)
          {
            houseDataList.Sort((IComparer<HouseData>) this.playersPosComparer);
            break;
          }
          houseDataList.Sort((IComparer<HouseData>) this.gloryPosComparer);
          break;
        case 3:
          if (this.pageMode == 0)
          {
            houseDataList.Sort((IComparer<HouseData>) this.playersNegComparer);
            break;
          }
          houseDataList.Sort((IComparer<HouseData>) this.gloryNegComparer);
          break;
        case 4:
          if (this.pageMode == 0)
          {
            houseDataList.Sort((IComparer<HouseData>) this.pointsPosComparer);
            break;
          }
          houseDataList.Sort((IComparer<HouseData>) this.gloryPosComparer);
          break;
        case 5:
          if (this.pageMode == 0)
          {
            houseDataList.Sort((IComparer<HouseData>) this.pointsNegComparer);
            break;
          }
          houseDataList.Sort((IComparer<HouseData>) this.gloryNegComparer);
          break;
      }
      foreach (HouseData houseData in houseDataList)
      {
        HouseListPanel.HouseLine control = new HouseListPanel.HouseLine();
        if (num != 0)
          num += 5;
        control.Position = new Point(0, num);
        control.init(houseData, yourHouseID, yourFactionRank, appliedToHouse, position, this, this.pageMode == 1);
        this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
        this.lineList.Add(control);
        ++position;
      }
      this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, num);
      if (num < this.wallScrollBar.Height)
      {
        this.wallScrollBar.Visible = false;
      }
      else
      {
        this.wallScrollBar.Visible = true;
        this.wallScrollBar.NumVisibleLines = this.wallScrollBar.Height;
        this.wallScrollBar.Max = num - this.wallScrollBar.Height;
      }
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
      this.update();
      this.Invalidate();
    }

    public void selfJoinHouse(int houseID)
    {
      RemoteServices.Instance.set_SelfJoinHouse_UserCallBack(new RemoteServices.SelfJoinHouse_UserCallBack(this.selfJoinHouseCallback));
      RemoteServices.Instance.SelfJoinHouse(RemoteServices.Instance.UserFactionID, houseID, GameEngine.Instance.World.StoredFactionChangesPos);
    }

    public void selfJoinHouseCallback(SelfJoinHouse_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.factionsList != null)
          GameEngine.Instance.World.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
        GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
        GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
        if (returnData.yourFaction != null && returnData.yourFaction.houseID > 0)
          InterfaceMgr.Instance.showHousePanel(returnData.yourFaction.houseID);
        else
          this.init(false);
      }
      else
      {
        if (returnData.m_errorCode != ErrorCodes.ErrorCode.HOUSE_FULL && returnData.m_errorCode != ErrorCodes.ErrorCode.HOUSE_FACTION_NEEDS_5_MEMBERS)
          return;
        if (returnData.m_errorCode == ErrorCodes.ErrorCode.HOUSE_FACTION_NEEDS_5_MEMBERS && GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        {
          int num1 = (int) MyMessageBox.Show(SK.Text("ERRORCODE_HOUSE_FACTION_NEEDS_10_MEMBERS", "Your faction needs 10 members to join a house."), SK.Text("FactionsPanel_House_Join_Error", "House Join Error"));
        }
        else
        {
          int num2 = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode), SK.Text("FactionsPanel_House_Join_Error", "House Join Error"));
        }
      }
    }

    private void sortNameClick()
    {
      this.sortMethod = this.sortMethod != 0 ? 0 : 1;
      this.addFactions();
    }

    private void sortFactionsClick()
    {
      this.sortMethod = this.sortMethod != 2 ? 2 : 3;
      this.addFactions();
    }

    private void sortPointsClick()
    {
      if (this.pageMode == 0)
      {
        this.sortMethod = this.sortMethod != 4 ? 4 : 5;
        this.addFactions();
      }
      else
        this.sortFactionsClick();
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
      this.Name = nameof (HouseListPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public class HouseLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel houseName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel houseMotto = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel numPlayersLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton joinButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDImage allianceImage = new CustomSelfDrawPanel.CSDImage();
      private int m_position = -1000;
      private bool m_applied;
      private HouseData m_houseData;
      private HouseListPanel m_parent;
      private MyMessageBoxPopUp PopUpRef;

      public void init(
        HouseData houseData,
        int yourHouseID,
        int yourRank,
        int appliedToHouse,
        int position,
        HouseListPanel parent,
        bool gloryMode)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_houseData = houseData;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.backgroundImage.Size = new Size(this.backgroundImage.Size.Width, 51);
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        if (houseData.houseID > 0)
        {
          this.houseImage.Image = (Image) GFXLibrary.getHouseCircleMediumImage(houseData.houseID - 1);
          this.houseImage.Position = new Point(5, 0);
          this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.houseImage.CustomTooltipID = 2308;
          this.houseImage.CustomTooltipData = houseData.houseID;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseImage);
        }
        NumberFormatInfo nfi = GameEngine.NFI;
        Color color = ARGBColors.Black;
        if (houseData.houseID == yourHouseID)
          color = ARGBColors.Yellow;
        this.houseName.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + houseData.houseID.ToString();
        this.houseName.Color = color;
        this.houseName.Position = new Point(64, 5);
        this.houseName.Size = new Size(280, this.backgroundImage.Height);
        this.houseName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.houseName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.houseName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseName);
        this.houseMotto.Text = "\"" + CustomTooltipManager.getHouseMotto(houseData.houseID) + "\"";
        this.houseMotto.Color = color;
        this.houseMotto.Position = new Point(64, 30);
        this.houseMotto.Size = new Size(280, this.backgroundImage.Height);
        this.houseMotto.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.houseMotto.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.houseMotto.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseMotto);
        int num = -1;
        if (!gloryMode)
        {
          this.numPlayersLabel.Text = houseData.houseID != 0 ? houseData.numFactions.ToString("N", (IFormatProvider) nfi) : GameEngine.Instance.World.countHouseFactions(0).ToString("N", (IFormatProvider) nfi);
        }
        else
        {
          int gloryRank = GameEngine.Instance.World.getGloryRank(houseData.houseID);
          if (houseData.houseID == 0 || gloryRank < 0)
          {
            this.numPlayersLabel.Text = "";
          }
          else
          {
            this.numPlayersLabel.Text = (gloryRank + 1).ToString("N", (IFormatProvider) nfi);
            num = GameEngine.Instance.World.getGloryPoints(houseData.houseID);
          }
        }
        this.numPlayersLabel.Color = ARGBColors.Black;
        this.numPlayersLabel.Position = new Point(235, 0);
        this.numPlayersLabel.Size = new Size(100, this.backgroundImage.Height);
        this.numPlayersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.numPlayersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.numPlayersLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.numPlayersLabel);
        this.pointsLabel.Text = gloryMode ? (num < 0 ? "" : num.ToString("N", (IFormatProvider) nfi)) : houseData.points.ToString("N", (IFormatProvider) nfi);
        this.pointsLabel.Color = ARGBColors.Black;
        this.pointsLabel.Position = new Point(410, 0);
        this.pointsLabel.Size = new Size(100, this.backgroundImage.Height);
        this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.pointsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
        if (houseData.houseID > 0)
        {
          this.membershipLabel.Color = ARGBColors.Black;
          this.membershipLabel.Position = new Point(570, 3);
          this.membershipLabel.Size = new Size(130, this.backgroundImage.Height);
          this.membershipLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          this.membershipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
          this.membershipLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.membershipLabel);
          this.joinButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
          this.joinButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
          this.joinButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
          this.joinButton.Position = new Point(567, 24);
          string str;
          this.joinButton.Text.Text = str = SK.Text("HouseInfoLine_Join", "Join");
          this.joinButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.joinButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          this.joinButton.TextYOffset = -3;
          this.joinButton.Text.Color = ARGBColors.Black;
          this.joinButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.joinClicked), "HouseListPanel_join");
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.joinButton);
          this.joinButton.Visible = false;
          if (yourHouseID == 0 && appliedToHouse > 0)
          {
            if (houseData.houseID == appliedToHouse)
            {
              str = SK.Text("HouseInfoLine_Applied", "Applied");
              this.joinButton.Visible = true;
              this.m_applied = true;
              if (yourRank != 1)
                this.joinButton.Enabled = false;
            }
          }
          else if (houseData.numFactions < GameEngine.Instance.LocalWorldData.Houses_MaxFactions && yourHouseID == 0 && yourRank == 1 && houseData.houseID != 0)
          {
            if (houseData.numFactions >= GameEngine.Instance.LocalWorldData.Houses_SelfJoinLimit)
            {
              str = SK.Text("HouseInfoLine_Apply", "Apply");
              this.joinButton.Visible = true;
            }
            else
            {
              str = SK.Text("HouseInfoLine_Join", "Join");
              this.joinButton.Visible = true;
            }
          }
          if (houseData.houseID == 10 && GameEngine.Instance.LocalWorldData.AIWorld)
          {
            this.membershipLabel.Text = SK.Text("FactionInvites_Membership_closed", "Closed");
            this.joinButton.Visible = false;
          }
          else
            this.membershipLabel.Text = houseData.numFactions >= GameEngine.Instance.LocalWorldData.Houses_MaxFactions ? SK.Text("FactionInvites_Membership_closed", "Closed") : SK.Text("FactionInvites_Membership_open", "Open");
          this.joinButton.Text.Text = str;
          if (!this.joinButton.Visible)
          {
            this.membershipLabel.Position = new Point(570, 0);
            this.membershipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          }
        }
        int yourHouseRelation = GameEngine.Instance.World.getYourHouseRelation(houseData.houseID);
        if (yourHouseRelation != 0)
        {
          if (yourHouseRelation > 0)
          {
            this.allianceImage.Image = (Image) GFXLibrary.faction_relationships[0];
            this.allianceImage.CustomTooltipID = 2303;
          }
          else
          {
            this.allianceImage.Image = (Image) GFXLibrary.faction_relationships[2];
            this.allianceImage.CustomTooltipID = 2304;
          }
          this.allianceImage.Position = new Point(238, 12);
          this.allianceImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.allianceImage);
        }
        if (GameEngine.Instance.World.WorldEnded)
          this.joinButton.Visible = false;
        this.invalidate();
      }

      public void update()
      {
      }

      public void clickedLine()
      {
        GameEngine.Instance.playInterfaceSound("HouseListPanel_faction");
        if (this.m_houseData.houseID <= 0)
          return;
        InterfaceMgr.Instance.showHousePanel(this.m_houseData.houseID);
      }

      private void joinClicked()
      {
        if (this.m_parent == null)
          return;
        if (!this.m_applied)
        {
          this.m_parent.selfJoinHouse(this.m_houseData.houseID);
        }
        else
        {
          MessageBoxButtons buts = MessageBoxButtons.YesNo;
          if (MyMessageBox.Show(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FactionInvite_Cancel_Application", "Cancel Application"), buts) != DialogResult.Yes)
            return;
          this.Join();
        }
      }

      private void Join() => this.m_parent.selfJoinHouse(-1);
    }

    public class NamePosComparer : IComparer<HouseData>
    {
      public int Compare(HouseData x, HouseData y)
      {
        return x == null ? (y == null ? 0 : -1) : (y == null ? 1 : x.houseID.CompareTo(y.houseID));
      }
    }

    public class NameNegComparer : IComparer<HouseData>
    {
      public int Compare(HouseData y, HouseData x)
      {
        return x == null ? (y == null ? 0 : -1) : (y == null ? 1 : x.houseID.CompareTo(y.houseID));
      }
    }

    public class PlayersPosComparer : IComparer<HouseData>
    {
      public int Compare(HouseData x, HouseData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.numFactions > y.numFactions)
          return -1;
        return x.numFactions < y.numFactions ? 1 : x.houseID.CompareTo(y.houseID);
      }
    }

    public class PlayersNegComparer : IComparer<HouseData>
    {
      public int Compare(HouseData y, HouseData x)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.numFactions > y.numFactions)
          return -1;
        return x.numFactions < y.numFactions ? 1 : x.houseID.CompareTo(y.houseID);
      }
    }

    public class PointsPosComparer : IComparer<HouseData>
    {
      public int Compare(HouseData x, HouseData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.points > y.points)
          return -1;
        return x.points < y.points ? 1 : x.houseID.CompareTo(y.houseID);
      }
    }

    public class PointsNegComparer : IComparer<HouseData>
    {
      public int Compare(HouseData y, HouseData x)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.points > y.points)
          return -1;
        return x.points < y.points ? 1 : x.houseID.CompareTo(y.houseID);
      }
    }

    public class GloryPosComparer : IComparer<HouseData>
    {
      public int Compare(HouseData x, HouseData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.loser != y.loser)
          return x.loser ? 1 : -1;
        if (x.loser)
          return x.houseID.CompareTo(y.houseID);
        int num1 = GameEngine.Instance.World.getGloryPoints(x.houseID);
        int num2 = GameEngine.Instance.World.getGloryPoints(y.houseID);
        if (x.houseID == 0)
          num1 = -1;
        if (y.houseID == 0)
          num2 = -1;
        if (num1 > num2)
          return -1;
        return num1 < num2 ? 1 : x.houseID.CompareTo(y.houseID);
      }
    }

    public class GloryNegComparer : IComparer<HouseData>
    {
      public int Compare(HouseData y, HouseData x)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.loser != y.loser)
          return x.loser ? 1 : -1;
        if (x.loser)
          return x.houseID.CompareTo(y.houseID);
        int num1 = GameEngine.Instance.World.getGloryPoints(x.houseID);
        int num2 = GameEngine.Instance.World.getGloryPoints(y.houseID);
        if (x.houseID == 0)
          num1 = -1;
        if (y.houseID == 0)
          num2 = -1;
        if (num1 > num2)
          return -1;
        return num1 < num2 ? 1 : x.houseID.CompareTo(y.houseID);
      }
    }
  }
}
