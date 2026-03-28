// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionOfficersPanel
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
  public class FactionOfficersPanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int PANEL_ID = 46;
    public static FactionOfficersPanel instance;
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
    private CustomSelfDrawPanel.CSDLabel leadershipVoteLabel = new CustomSelfDrawPanel.CSDLabel();
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
    private CustomSelfDrawPanel.CSDButton inviteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDFactionFlagImage flagimage = new CustomSelfDrawPanel.CSDFactionFlagImage();
    private CustomSelfDrawPanel.CSDButton editButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDImage backImage1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage backImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage barImage3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton applicationButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel applicationsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
    private List<FactionOfficersPanel.FactionMemberLineOfficer> lineList = new List<FactionOfficersPanel.FactionMemberLineOfficer>();
    private string invitedUserName = "";
    private DockableControl dockableControl;
    private IContainer components;
    private TextBox tbInviteName;

    public FactionOfficersPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      FactionOfficersPanel.instance = this;
      this.clearControls();
      NumberFormatInfo nfi = GameEngine.NFI;
      this.sidebar.addSideBar(3, (CustomSelfDrawPanel) this);
      FactionData factionData = GameEngine.Instance.World.YourFaction ?? new FactionData();
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
      int yourFactionRank = GameEngine.Instance.World.getYourFactionRank();
      this.factionMottoLabel.Text = "\"" + factionData.factionMotto + "\"";
      this.factionMottoLabel.Color = ARGBColors.Black;
      if (yourFactionRank == 1)
        this.factionMottoLabel.Position = new Point(230, 41);
      else
        this.factionMottoLabel.Position = new Point(205, 41);
      this.factionMottoLabel.Size = new Size(600, 40);
      this.factionMottoLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.factionMottoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionMottoLabel);
      this.applicationButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.applicationButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.applicationButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.applicationButton.Position = new Point(24, 126);
      this.applicationButton.Text.Text = !factionData.openForApplications ? SK.Text("FactionInvites_Not_Accepting_App", "Not Accepting") : SK.Text("FactionInvites_Accepting_Apps", "Accepting");
      this.applicationButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.applicationButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.applicationButton.TextYOffset = -3;
      this.applicationButton.Text.Color = ARGBColors.Black;
      this.applicationButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.setApplicationModeClicked));
      this.applicationButton.Enabled = true;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.applicationButton);
      this.applicationsLabel.Text = SK.Text("FactionInvites_Applications", "Applications");
      this.applicationsLabel.Color = ARGBColors.Black;
      this.applicationsLabel.Position = new Point(24, 96);
      this.applicationsLabel.Size = this.applicationButton.Size;
      this.applicationsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.applicationsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.applicationsLabel);
      if (yourFactionRank == 1)
      {
        this.editButton.ImageNorm = (Image) GFXLibrary.faction_pen;
        this.editButton.ImageOver = (Image) GFXLibrary.faction_pen;
        this.editButton.ImageClick = (Image) GFXLibrary.faction_pen;
        this.editButton.Position = new Point(205, 41);
        this.editButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editClicked), "FactionOfficersPanel_edit");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.editButton);
      }
      if (factionData.houseID > 0)
      {
        this.houseLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + factionData.houseID.ToString();
        this.houseLabel.Color = ARGBColors.Black;
        this.houseLabel.Position = new Point(575, 110);
        this.houseLabel.Size = new Size(200, 50);
        this.houseLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.houseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.houseLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "FactionOfficersPanel_house");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseLabel);
        this.houseImage.Image = (Image) GFXLibrary.getHouseCircleLargeImage(factionData.houseID - 1);
        this.houseImage.Position = new Point(675 - this.houseImage.Image.Width / 2, 65 - this.houseImage.Image.Height / 2 + 8);
        this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "FactionOfficersPanel_house");
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
      this.rankHeaderLabelValue.Text = (GameEngine.Instance.World.getYourFactionRank() + 1).ToString("N", (IFormatProvider) nfi);
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
      this.playerNameLabel.Text = SK.Text("UserInfoPanel_", "Player Name");
      this.playerNameLabel.Color = ARGBColors.Black;
      this.playerNameLabel.Position = new Point(9, -2);
      this.playerNameLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.playerNameLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.playerNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.playerNameLabel);
      this.leadershipVoteLabel.Text = SK.Text("FactionsPanel_Leadership_Vote", "Leadership Vote");
      this.leadershipVoteLabel.Color = ARGBColors.Black;
      this.leadershipVoteLabel.Position = new Point(444, -2);
      this.leadershipVoteLabel.Size = new Size(300, this.headerLabelsImage.Height);
      this.leadershipVoteLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.leadershipVoteLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.leadershipVoteLabel);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_Faction_Officers", "Faction Officers"));
      this.inviteButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      this.inviteButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      this.inviteButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      this.inviteButton.Position = new Point(20, height - 30);
      this.inviteButton.Text.Text = SK.Text("FactionsPanel_Invite_User", "Invite User");
      this.inviteButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.inviteButton.TextYOffset = -3;
      this.inviteButton.Text.Color = ARGBColors.Black;
      this.inviteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.inviteClick), "FactionOfficersPanel_invite");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.inviteButton);
      this.wallScrollArea.Position = new Point(25, 188);
      this.wallScrollArea.Size = new Size(705, height - 50 - 150 - 40);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(705, height - 50 - 150 - 40));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      this.flagimage.createFromFlagData(factionData.flagData);
      this.flagimage.Position = new Point(35, 6);
      this.flagimage.Scale = 0.5;
      this.flagimage.ClickArea = new Rectangle(0, 0, GFXLibrary.factionFlags[0].Width / 2, GFXLibrary.factionFlags[0].Height / 2);
      if (yourFactionRank == 1)
        this.flagimage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editClicked), "FactionOfficersPanel_edit");
      else
        this.flagimage.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.flagimage);
      int num = this.wallScrollBar.Value;
      this.wallScrollBar.Visible = false;
      this.wallScrollBar.Position = new Point(733, 188);
      this.wallScrollBar.Size = new Size(24, height - 50 - 150 - 40);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      bool uptodate = false;
      FactionMemberData[] factionMemberData = GameEngine.Instance.World.getFactionMemberData(factionData.factionID, ref uptodate);
      if (!resized && !uptodate)
      {
        RemoteServices.Instance.set_GetViewFactionData_UserCallBack(new RemoteServices.GetViewFactionData_UserCallBack(this.getViewFactionDataCallback));
        RemoteServices.Instance.GetViewFactionData(factionData.factionID);
      }
      this.addPlayers(factionMemberData);
    }

    private void getViewFactionDataCallback(GetViewFactionData_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      NumberFormatInfo nfi = GameEngine.NFI;
      this.addPlayers(returnData.members);
      if (returnData.factionData != null)
        GameEngine.Instance.World.setFactionMemberData(returnData.factionData.factionID, returnData.members);
      GameEngine.Instance.World.setFactionData(returnData.factionData);
      GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
      GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
    }

    public void update()
    {
      this.sidebar.update();
      if (this.tbInviteName.Text.Length == 0)
        this.inviteButton.Enabled = false;
      else
        this.inviteButton.Enabled = true;
    }

    public void logout()
    {
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

    public void editClicked() => InterfaceMgr.Instance.showEditFactionPanel();

    public void houseClicked()
    {
      FactionData yourFaction = GameEngine.Instance.World.YourFaction;
      if (yourFaction == null || yourFaction.houseID <= 0)
        return;
      InterfaceMgr.Instance.showHousePanel(yourFaction.houseID);
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
        int numOfficers = 0;
        foreach (FactionMemberData factionMemberData in fmd)
        {
          if (factionMemberData.status == 2)
            ++numOfficers;
        }
        for (int index = 0; index < 5; ++index)
        {
          int num2 = 1;
          if (index == 1)
            num2 = 2;
          else if (index == 2)
            num2 = 0;
          else if (index == 3)
            num2 = -1;
          else if (index == 4)
            num2 = -3;
          foreach (FactionMemberData factionData in fmd)
          {
            if (factionData.status == num2)
            {
              FactionOfficersPanel.FactionMemberLineOfficer control = new FactionOfficersPanel.FactionMemberLineOfficer();
              if (num1 != 0)
                num1 += 5;
              control.Position = new Point(0, num1);
              control.init(factionData, position, this, true, numOfficers);
              this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
              num1 += control.Height;
              this.lineList.Add(control);
              ++position;
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
      this.update();
      this.Invalidate();
    }

    public void inviteClick()
    {
      if (this.tbInviteName.Text.Length <= 0)
        return;
      this.inviteToFaction(this.tbInviteName.Text);
    }

    public void setApplicationModeClicked()
    {
      FactionData factionData = GameEngine.Instance.World.YourFaction ?? new FactionData();
      this.applicationButton.Enabled = false;
      RemoteServices.Instance.set_FactionApplicationProcessing_UserCallBack(new RemoteServices.FactionApplicationProcessing_UserCallBack(this.factionApplicationProcessingCallback));
      RemoteServices.Instance.FactionApplicationSetMode(!factionData.openForApplications);
    }

    public void factionApplicationProcessingCallback(
      FactionApplicationProcessing_ReturnType returnData)
    {
      this.applicationButton.Enabled = true;
      if (returnData.members == null)
        return;
      GameEngine.Instance.World.FactionMembers = returnData.members;
      GameEngine.Instance.World.YourFaction = returnData.yourFaction;
      this.init(false);
    }

    public void inviteToFaction(string username)
    {
      this.invitedUserName = username;
      RemoteServices.Instance.set_FactionSendInvite_UserCallBack(new RemoteServices.FactionSendInvite_UserCallBack(this.factionSendInviteCallback));
      RemoteServices.Instance.FactionSendInvite(username);
    }

    public void factionSendInviteCallback(FactionSendInvite_ReturnType returnData)
    {
      if (returnData.members != null)
      {
        GameEngine.Instance.World.FactionMembers = returnData.members;
        this.addPlayers(GameEngine.Instance.World.FactionMembers);
      }
      if (returnData.Success)
      {
        int num = (int) MyMessageBox.Show(SK.Text("FactionsPanel_Invited", "Player Successfully Invited") + Environment.NewLine + Environment.NewLine + this.invitedUserName, SK.Text("FactionsPanel_Invited_Header", "Player Invited"));
        this.tbInviteName.Text = "";
        this.inviteButton.Enabled = false;
      }
      else
      {
        switch (returnData.m_errorCode)
        {
          case ErrorCodes.ErrorCode.FACTION_ALREADY_IN_FACTION:
            int num1 = (int) MyMessageBox.Show(SK.Text("FactionsPanel_Already_In_Faction", "This user is already in this faction."), SK.Text("FactionsPanel_Invite_Error", "Invite Error"));
            break;
          case ErrorCodes.ErrorCode.FACTION_INVITE_ALREADY_EXISTS:
            int num2 = (int) MyMessageBox.Show(SK.Text("FactionsPanel_Already_Has_Invite", "This User already has an invite."), SK.Text("FactionsPanel_Invite_Error", "Invite Error"));
            break;
          case ErrorCodes.ErrorCode.FACTION_FULL:
            int num3 = (int) MyMessageBox.Show(SK.Text("FactionsPanel_Faction_Full", "The Faction is full."), SK.Text("FactionsPanel_Invite_Error", "Invite Error"));
            break;
          case ErrorCodes.ErrorCode.FACTION_UNKNOWN_USER:
            int num4 = (int) MyMessageBox.Show(SK.Text("FactionsPanel_Unknown_User", "Unknown User"), SK.Text("FactionsPanel_Invite_Error", "Invite Error"));
            break;
          case ErrorCodes.ErrorCode.FACTION_INVITEE_TOO_LOW:
            int num5 = (int) MyMessageBox.Show(SK.Text("FactionsPanel_Rank_Too_Low", "User's rank too low"), SK.Text("FactionsPanel_Invite_Error", "Invite Error"));
            break;
        }
      }
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
      this.tbInviteName = new TextBox();
      this.SuspendLayout();
      this.tbInviteName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.tbInviteName.Location = new Point(188, 539);
      this.tbInviteName.Name = "tbInviteName";
      this.tbInviteName.Size = new Size(245, 20);
      this.tbInviteName.TabIndex = 7;
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.tbInviteName);
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (FactionOfficersPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public class FactionMemberLineOfficer : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage officerImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton promoteButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton dismissButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDCheckBox voteCheck = new CustomSelfDrawPanel.CSDCheckBox();
      private CustomSelfDrawPanel.CSDLabel pendingLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton acceptButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton declineButton = new CustomSelfDrawPanel.CSDButton();
      private int m_position = -1000;
      private FactionMemberData m_factionMemberData;
      private FactionOfficersPanel m_parent;

      public void init(
        FactionMemberData factionData,
        int position,
        FactionOfficersPanel parent,
        bool ownFaction,
        int numOfficers)
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
        this.playerName.Color = ARGBColors.Black;
        this.playerName.Position = new Point(39, 0);
        this.playerName.Size = new Size(280, this.backgroundImage.Height);
        this.playerName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.playerName);
        if (factionData.status == -1)
        {
          this.pendingLabel.Text = SK.Text("FactionsInvites_Invite_Pending", "Invitation Pending");
          this.pendingLabel.Color = ARGBColors.DarkRed;
          this.pendingLabel.Position = new Point(300, 0);
          this.pendingLabel.Size = new Size(500, this.backgroundImage.Height);
          this.pendingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          this.pendingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          this.pendingLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pendingLabel);
          switch (GameEngine.Instance.World.getYourFactionRank())
          {
            case 1:
            case 2:
              if (!GameEngine.Instance.World.WorldEnded)
              {
                this.declineButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.declineButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.declineButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.declineButton.Position = new Point(525, 0);
                this.declineButton.Text.Text = SK.Text("FactionMemberLine_Cancel_Invite", "Cancel Invite");
                this.declineButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.declineButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.declineButton.TextYOffset = -3;
                this.declineButton.Text.Color = ARGBColors.Black;
                this.declineButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.declineClicked), "FactionOfficersPanel_decline");
                this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.declineButton);
                break;
              }
              break;
          }
        }
        else if (factionData.status == -3)
        {
          this.pendingLabel.Text = SK.Text("FactionsInvites_Application", "Application");
          this.pendingLabel.Color = ARGBColors.DarkRed;
          this.pendingLabel.Position = new Point(270, 0);
          this.pendingLabel.Size = new Size(500, this.backgroundImage.Height);
          this.pendingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          this.pendingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          this.pendingLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pendingLabel);
          switch (GameEngine.Instance.World.getYourFactionRank())
          {
            case 1:
            case 2:
              if (!GameEngine.Instance.World.WorldEnded)
              {
                this.acceptButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.acceptButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.acceptButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.acceptButton.Position = new Point(370, 0);
                this.acceptButton.Text.Text = SK.Text("FactionInviteLine_Accept", "Accept");
                this.acceptButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.acceptButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.acceptButton.TextYOffset = -3;
                this.acceptButton.Text.Color = ARGBColors.Black;
                this.acceptButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.acceptAppClicked), "FactionOfficersPanel_decline");
                this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.acceptButton);
                this.declineButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.declineButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.declineButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.declineButton.Position = new Point(525, 0);
                this.declineButton.Text.Text = SK.Text("FactionInviteLine_Decline", "Decline");
                this.declineButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.declineButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.declineButton.TextYOffset = -3;
                this.declineButton.Text.Color = ARGBColors.Black;
                this.declineButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.declineAppClicked), "FactionOfficersPanel_decline");
                this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.declineButton);
                break;
              }
              break;
          }
        }
        else
        {
          int yourFactionRank = GameEngine.Instance.World.getYourFactionRank();
          if (factionData.status != 1)
          {
            if (yourFactionRank == 1 && !GameEngine.Instance.World.WorldEnded)
            {
              this.promoteButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
              this.promoteButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
              this.promoteButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
              this.promoteButton.Position = new Point(300, 0);
              this.promoteButton.Text.Text = SK.Text("FactionMemberLine_Cancel_Invite", "Cancel Invite");
              if (factionData.status == 0)
              {
                this.promoteButton.Text.Text = SK.Text("FactionMemberLine_Promote_To_Officer", "Promote To Officer");
                if (numOfficers >= GameEngine.Instance.LocalWorldData.Faction_MaxSergeants)
                  this.promoteButton.Enabled = false;
              }
              else
                this.promoteButton.Text.Text = SK.Text("FactionMemberLine_Demote_To_Commoner", "Demote To Commoner");
              this.promoteButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              this.promoteButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
              this.promoteButton.TextYOffset = -3;
              this.promoteButton.Text.Color = ARGBColors.Black;
              this.promoteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.promoteClicked), "FactionOfficersPanel_promote");
              this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.promoteButton);
            }
            if (factionData.status == 0 && !GameEngine.Instance.World.WorldEnded)
            {
              this.dismissButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
              this.dismissButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
              this.dismissButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
              this.dismissButton.Position = new Point(525, 0);
              this.dismissButton.Text.Text = SK.Text("FactionMemberLine_Dismiss", "Dismiss");
              this.dismissButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              this.dismissButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
              this.dismissButton.TextYOffset = -3;
              this.dismissButton.Text.Color = ARGBColors.Black;
              this.dismissButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.dismissMember), "FactionOfficersPanel_dismiss");
              this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.dismissButton);
            }
          }
          if ((factionData.status == 1 || factionData.status == 2) && !GameEngine.Instance.World.WorldEnded)
          {
            this.voteCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
            this.voteCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
            this.voteCheck.Position = new Point(585, 5);
            if (factionData.userID == GameEngine.Instance.World.YourFactionVote || GameEngine.Instance.World.YourFactionVote == -1 && factionData.status == 1)
            {
              this.voteCheck.Checked = true;
            }
            else
            {
              this.voteCheck.Checked = false;
              this.voteCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
            }
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.voteCheck);
          }
        }
        this.invalidate();
      }

      public void update()
      {
      }

      public void clickedLine()
      {
        GameEngine.Instance.playInterfaceSound("FactionOfficersPanel_user_clicked");
        InterfaceMgr.Instance.showUserInfoScreen(new WorldMap.CachedUserInfo()
        {
          userID = this.m_factionMemberData.userID
        });
      }

      public void checkToggled()
      {
        if (!this.voteCheck.Checked)
          return;
        this.voteLeaderChange(this.m_factionMemberData.userID);
      }

      private void voteLeaderChange(int userID)
      {
        RemoteServices.Instance.set_FactionLeadershipVote_UserCallBack(new RemoteServices.FactionLeadershipVote_UserCallBack(this.factionLeadershipVoteCallback));
        RemoteServices.Instance.FactionLeadershipVote(RemoteServices.Instance.UserFactionID, userID);
      }

      public void factionLeadershipVoteCallback(FactionLeadershipVote_ReturnType returnData)
      {
        if (!returnData.Success)
          return;
        GameEngine.Instance.World.YourFactionVote = returnData.yourLeaderVote;
        if (returnData.leaderChanged)
        {
          RemoteServices.Instance.UserFactionID = returnData.yourFaction.factionID;
          GameEngine.Instance.World.YourFaction = returnData.yourFaction;
          GameEngine.Instance.World.FactionMembers = returnData.members;
          GameEngine.Instance.World.FactionInvites = returnData.invites;
          GameEngine.Instance.World.FactionApplications = returnData.applications;
        }
        this.m_parent.init(false);
      }

      public void promoteClicked()
      {
        this.promoteButton.Enabled = false;
        if (this.m_factionMemberData.status == 0)
          this.changeRank(2);
        else
          this.changeRank(0);
      }

      public void changeRank(int rank)
      {
        RemoteServices.Instance.set_FactionChangeMemberStatus_UserCallBack(new RemoteServices.FactionChangeMemberStatus_UserCallBack(this.factionChangeMemberStatusCallback));
        RemoteServices.Instance.FactionChangeMemberStatus(this.m_factionMemberData.userID, rank);
      }

      public void dismissMember()
      {
        this.dismissButton.Enabled = false;
        RemoteServices.Instance.set_FactionChangeMemberStatus_UserCallBack(new RemoteServices.FactionChangeMemberStatus_UserCallBack(this.factionChangeMemberStatusCallback));
        RemoteServices.Instance.FactionChangeMemberStatus(this.m_factionMemberData.userID, -2);
      }

      public void factionChangeMemberStatusCallback(FactionChangeMemberStatus_ReturnType returnData)
      {
        if (returnData.Success)
        {
          GameEngine.Instance.World.FactionMembers = returnData.members;
          GameEngine.Instance.World.YourFaction = returnData.yourFaction;
          this.m_parent.init(false);
        }
        else
        {
          this.promoteButton.Enabled = true;
          this.dismissButton.Enabled = true;
        }
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

      public void declineAppClicked()
      {
        this.declineButton.Enabled = false;
        RemoteServices.Instance.set_FactionApplicationProcessing_UserCallBack(new RemoteServices.FactionApplicationProcessing_UserCallBack(this.factionApplicationProcessingCallback));
        RemoteServices.Instance.FactionApplicationReject(this.m_factionMemberData.userID);
      }

      public void acceptAppClicked()
      {
        this.declineButton.Enabled = false;
        RemoteServices.Instance.set_FactionApplicationProcessing_UserCallBack(new RemoteServices.FactionApplicationProcessing_UserCallBack(this.factionApplicationProcessingCallback));
        RemoteServices.Instance.FactionApplicationAccept(this.m_factionMemberData.userID);
      }

      public void factionApplicationProcessingCallback(
        FactionApplicationProcessing_ReturnType returnData)
      {
        if (returnData.m_errorCode == ErrorCodes.ErrorCode.FACTION_FULL)
        {
          int num = (int) MyMessageBox.Show(SK.Text("FactionsPanel_Faction_Full", "The Faction is full."), SK.Text("GENERIC_Error", "Error"));
        }
        this.declineButton.Enabled = true;
        if (returnData.members == null)
          return;
        GameEngine.Instance.World.FactionMembers = returnData.members;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
        this.m_parent.init(false);
      }
    }
  }
}
