// Decompiled with JetBrains decompiler
// Type: Kingdoms.UserInfoScreen3
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using StatTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class UserInfoScreen3 : CustomSelfDrawPanel, IDockableControl
  {
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel mainBackgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDArea mainHeaderArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea mainBodyArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel headerLabel2 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton adminButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage backgroundLeft = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage backgroundCentre = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage backgroundRight = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage avatarImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
    private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage houseImageShadow = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage flagImageShadow = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel houseLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel factionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton inviteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage positionImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel standingLabelLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton achievementsButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel achievementsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton questsButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel questsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel villageLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel regionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar outgoingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea outgoingScrollArea = new CustomSelfDrawPanel.CSDArea();
    private WorldMap.CachedUserInfo m_userInfo;
    private int m_userID = -1;
    private int m_houseID;
    private CustomSelfDrawPanel.CSDButton editButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton editAvatarButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton diplomacyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton repairButton = new CustomSelfDrawPanel.CSDButton();
    private static Image lastCreatedAvatar = (Image) null;
    public static UserInfoScreen3.VillageComparer villageComparer = new UserInfoScreen3.VillageComparer();
    private List<UserInfoScreen3.VillageLine> lineList = new List<UserInfoScreen3.VillageLine>();
    private CustomSelfDrawPanel.CSDButton diplomacyNeutralButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton diplomacyAllyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton diplomacyEnemyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel diplomacyCurrentLabelHeader = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel diplomacyCurrentLabel = new CustomSelfDrawPanel.CSDLabel();
    private bool diplomacyOverlayVisible;
    private CustomSelfDrawPanel.CSDFill greyOverlay = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel diplomacyHeaderImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel diplomacyBackgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel diplomacyHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel diplomacyFactionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton diplomacyCancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel statusHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton allyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton neutralButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton enemyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel markerHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton[] markerButtons = new CustomSelfDrawPanel.CSDButton[GFXLibrary.custom_player_marker.Length];
    private CustomSelfDrawPanel.CSDButton updateButton = new CustomSelfDrawPanel.CSDButton();
    private bool isAlly;
    private bool isEnemy;
    private bool relationshipChanged;
    private int currentMarkerType;
    private bool pendingMarkerUpdate;
    private bool pendingRelationshipUpdate;
    private UserInfoScreen3.VillageLine currentRepairCastle;
    private int repairCount;
    private DockableControl dockableControl;
    private IContainer components;

    public UserInfoScreen3()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(WorldMap.CachedUserInfo userInfo)
    {
      foreach (UserInfoScreen3.VillageLine line in this.lineList)
        line.resetRepairStatus();
      this.clearControls();
      NumberFormatInfo nfi = GameEngine.NFI;
      this.m_houseID = 0;
      if (userInfo == null)
      {
        userInfo = new WorldMap.CachedUserInfo();
        userInfo.userID = this.m_userID;
      }
      this.m_userID = userInfo.userID;
      WorldMap.VillageRolloverInfo villageInfo = (WorldMap.VillageRolloverInfo) null;
      GameEngine.Instance.World.retrieveUserData(-1, userInfo.userID, ref villageInfo, ref userInfo, true, true);
      this.m_userInfo = userInfo;
      this.mainBackgroundImage.Size = new Size(this.Width, this.Height - 40);
      this.mainBackgroundImage.Position = new Point(0, 40);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.mainBackgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
      this.mainHeaderArea.Position = new Point(0, -40);
      this.mainHeaderArea.Size = new Size(992, 45);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainHeaderArea);
      this.headerImage.Size = new Size(this.Width, 40);
      this.headerImage.Position = new Point(0, 0);
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage);
      this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
      this.positionImage.Image = (Image) GFXLibrary.char_position[0];
      this.positionImage.Position = new Point(9, 7);
      this.positionImage.Visible = false;
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.positionImage);
      this.headerLabel.Text = userInfo == null ? "" : userInfo.userName;
      this.headerLabel.Color = ARGBColors.White;
      this.headerLabel.DropShadowColor = ARGBColors.Black;
      this.headerLabel.Position = new Point(39, 10);
      this.headerLabel.Size = new Size(500, 50);
      this.headerLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabel);
      Size textSizeX = this.headerLabel.TextSizeX;
      this.headerLabel2.Text = "";
      this.headerLabel2.Color = Color.FromArgb(173, 195, 208);
      this.headerLabel2.DropShadowColor = ARGBColors.Black;
      this.headerLabel2.Position = new Point(this.headerLabel.Position.X + textSizeX.Width + 5, 12);
      this.headerLabel2.Size = new Size(700, 28);
      this.headerLabel2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.headerLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabel2);
      this.standingLabelLabel.Text = userInfo == null || userInfo.standing < 0 ? "" : SK.Text("UserInfoScreen_Rank", "Rank") + " : " + userInfo.standing.ToString("N", (IFormatProvider) nfi);
      this.standingLabelLabel.Color = Color.FromArgb(173, 195, 208);
      this.standingLabelLabel.DropShadowColor = ARGBColors.Black;
      this.standingLabelLabel.Position = new Point(650, 12);
      this.standingLabelLabel.Size = new Size(700, 28);
      this.standingLabelLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.standingLabelLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.standingLabelLabel);
      this.pointsLabel.Text = userInfo == null ? "" : SK.Text("GENERIC_Points", "Points") + " : " + userInfo.points.ToString("N", (IFormatProvider) nfi);
      this.pointsLabel.Color = Color.FromArgb(173, 195, 208);
      this.pointsLabel.DropShadowColor = ARGBColors.Black;
      this.pointsLabel.Position = new Point(775, 12);
      this.pointsLabel.Size = new Size(700, 28);
      this.pointsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(948, 4);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "UserInfo2_close");
      this.closeButton.CustomTooltipID = 502;
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      if (userInfo != null && userInfo.avatarData != null)
      {
        this.backgroundLeft.Image = (Image) GFXLibrary.char_portraite_shadow;
        this.backgroundLeft.Position = new Point(5, 0);
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundLeft);
      }
      this.backgroundRight.Image = (Image) GFXLibrary.char_villagelist_inset;
      this.backgroundRight.Position = new Point(this.Width - 7 - GFXLibrary.char_villagelist_inset.Width, 1);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundRight);
      this.backgroundCentre.Image = (Image) GFXLibrary.char_shieldcomp_back;
      this.backgroundCentre.Position = new Point(299, 1);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundCentre);
      if (userInfo != null)
      {
        this.nameLabel.Text = userInfo.userName;
        this.nameLabel.Color = ARGBColors.Black;
        this.nameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.nameLabel.Position = new Point(11, 30);
        this.nameLabel.Size = new Size(180, 45);
        this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.nameLabel);
        this.rankLabel.TextDiffOnly = userInfo.avatarData == null ? Rankings.getRankingName(userInfo.rank) : Rankings.getRankingName(userInfo.rank, userInfo.avatarData.male);
        this.rankLabel.Color = ARGBColors.Black;
        this.rankLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.rankLabel.Position = new Point(11, 61);
        this.rankLabel.Size = new Size(180, 20);
        this.rankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.rankLabel);
        if (userInfo.avatarData != null)
        {
          if (UserInfoScreen3.lastCreatedAvatar != null)
            UserInfoScreen3.lastCreatedAvatar.Dispose();
          UserInfoScreen3.lastCreatedAvatar = this.avatarImage.Image = (Image) Avatar.CreateAvatar(userInfo.avatarData, ARGBColors.Transparent);
          this.avatarImage.Position = new Point(73, 22);
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.avatarImage);
        }
        this.shieldImage.Image = GameEngine.Instance.World.getWorldShieldOrBlank(userInfo.userID, 140, 156);
        if (this.shieldImage.Image != null)
        {
          this.shieldImage.Position = new Point(24, 102);
          if (userInfo.userID == RemoteServices.Instance.UserID)
          {
            this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editClicked), "UserInfo2_edit_shield_shield_clicked");
            this.shieldImage.CustomTooltipID = 4015;
          }
          else
          {
            this.shieldImage.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
            this.shieldImage.CustomTooltipID = 0;
          }
          this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
        }
        if (userInfo.factionID >= 0)
        {
          FactionData faction = GameEngine.Instance.World.getFaction(userInfo.factionID);
          if (faction != null)
          {
            this.flagImageShadow.Image = (Image) GFXLibrary.char_shadow_faction;
            this.flagImageShadow.Position = new Point(130, 259);
            this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.flagImageShadow);
            this.flagImage.createFromFlagData(faction.flagData);
            this.flagImage.CustomTooltipData = faction.factionID;
            this.flagImage.Position = new Point(128, 257);
            this.flagImage.Scale = 0.25;
            this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClicked), "UserInfoPanel2_faction_flag");
            this.flagImage.CustomTooltipID = 2501;
            this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.flagImage);
            this.factionLabel.Text = faction.factionNameAbrv;
            this.factionLabel.Color = ARGBColors.Black;
            this.factionLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.factionLabel.Position = new Point(11, 311);
            this.factionLabel.Size = new Size(180, 20);
            this.factionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.factionLabel.CustomTooltipID = 2501;
            this.factionLabel.CustomTooltipData = faction.factionID;
            this.factionLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClicked), "UserInfoPanel2_faction_flag");
            this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.factionLabel);
            if (faction.houseID > 0)
            {
              this.houseImageShadow.Image = (Image) GFXLibrary.char_shadow_house;
              this.houseImageShadow.Position = new Point(10, 247);
              this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.houseImageShadow);
              this.houseImage.Image = this.houseImage.Image = (Image) GFXLibrary.getHouseCircleMediumImage(faction.houseID - 1);
              this.houseImage.CustomTooltipData = faction.houseID;
              this.houseImage.Position = new Point(10, 247);
              this.houseImage.CustomTooltipID = 2307;
              this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "UserInfoPanel2_house");
              this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.houseImage);
              this.houseLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + faction.houseID.ToString();
              this.houseLabel.Color = ARGBColors.Black;
              this.houseLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
              this.houseLabel.Position = new Point(11, 285);
              this.houseLabel.Size = new Size(180, 20);
              this.houseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
              this.houseLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "UserInfoPanel2_house");
              this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.houseLabel);
              this.m_houseID = faction.houseID;
            }
          }
        }
        int yourFactionRank = GameEngine.Instance.World.getYourFactionRank();
        if (GameEngine.Instance.World.YourFaction != null && userInfo != null && userInfo.userID != RemoteServices.Instance.UserID && GameEngine.Instance.World.FactionMembers != null && yourFactionRank > 0)
        {
          this.inviteButton.ImageNorm = (Image) GFXLibrary.char_but_invite[0];
          this.inviteButton.ImageOver = (Image) GFXLibrary.char_but_invite[1];
          this.inviteButton.Position = new Point(62, 346);
          this.inviteButton.MoveOnClick = true;
          this.inviteButton.Text.Text = SK.Text("UserInfoScreen_InviteToFaction", "Invite To Faction");
          this.inviteButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
          this.inviteButton.Text.Position = new Point(3, 26);
          this.inviteButton.Text.Size = new Size(70, 27);
          this.inviteButton.TextYOffset = 0;
          this.inviteButton.Text.Color = ARGBColors.Black;
          this.inviteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.inviteToFactionClicked), "UserInfoPanel2_invite_to_faction_clicked");
          this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.inviteButton);
        }
        this.mailButton.ImageNorm = (Image) GFXLibrary.char_but_mail[0];
        this.mailButton.ImageOver = (Image) GFXLibrary.char_but_mail[1];
        this.mailButton.Position = new Point(62, 417);
        this.mailButton.MoveOnClick = true;
        this.mailButton.Text.Text = SK.Text("User_Send_A_Message", "Send a Message");
        this.mailButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
        this.mailButton.Text.Position = new Point(3, 26);
        this.mailButton.Text.Size = new Size(70, 27);
        this.mailButton.TextYOffset = 0;
        this.mailButton.Text.Color = ARGBColors.Black;
        this.mailButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendMailClicked), "UserInfoPanel2_send_mail_clicked");
        this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.mailButton);
        if (GameEngine.Instance.LocalWorldData.IsHereticEUAIWorld && userInfo.userID <= 4)
          this.mailButton.Visible = false;
        else
          this.mailButton.Visible = true;
        this.diplomacyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
        this.diplomacyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
        this.diplomacyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
        this.diplomacyButton.Position = new Point(635, 509);
        if (userInfo.userID == RemoteServices.Instance.UserID)
        {
          this.diplomacyButton.Text.Text = SK.Text("User_Manage_Relations", "Manage Diplomacy");
          this.diplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.manageDiplomacyClicked), "FactionMyFactionPanel_diplomacy");
        }
        else
        {
          string str = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy") + " : ";
          int userRelationship = GameEngine.Instance.World.getUserRelationship(userInfo.userID);
          if (userRelationship == 0)
            str += SK.Text("GENERIC_Neutral", "Neutral");
          else if (userRelationship > 0)
            str += SK.Text("GENERIC_Ally", "Ally");
          else if (userRelationship < 0)
            str += SK.Text("GENERIC_Enemy", "Enemy");
          this.diplomacyButton.Text.Text = str;
          this.diplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addDiplomacyOverlay), "FactionMyFactionPanel_diplomacy");
        }
        this.diplomacyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.diplomacyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
        this.diplomacyButton.TextYOffset = -3;
        this.diplomacyButton.Text.Color = ARGBColors.Black;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyButton);
      }
      if (RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator)
      {
        this.adminButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
        this.adminButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
        this.adminButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
        this.adminButton.Position = new Point(72, 481);
        this.adminButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.adminClick));
        this.adminButton.CustomTooltipID = 3101;
        this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.adminButton);
      }
      if (userInfo != null && this.m_userInfo.userID == RemoteServices.Instance.UserID)
      {
        this.editButton.ImageNorm = (Image) GFXLibrary.mrhp_button_more_info;
        this.editButton.ImageOver = (Image) GFXLibrary.mrhp_button_more_info_over;
        this.editButton.MoveOnClick = true;
        this.editButton.Position = new Point(57, 79);
        this.editButton.Text.Text = SK.Text("User_Edit", "Edit");
        this.editButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
        this.editButton.TextYOffset = -3;
        this.editButton.Text.Color = Color.FromArgb(233, 231, 213);
        this.editButton.Text.Position = new Point(-3, 0);
        this.editButton.Text.DropShadowColor = ARGBColors.Black;
        this.editButton.CustomTooltipID = 4015;
        this.editButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editClicked), "UserInfo2_edit_shield_button_clicked");
        this.backgroundCentre.addControl((CustomSelfDrawPanel.CSDControl) this.editButton);
        this.editAvatarButton.ImageNorm = (Image) GFXLibrary.mrhp_button_more_info;
        this.editAvatarButton.ImageOver = (Image) GFXLibrary.mrhp_button_more_info_over;
        this.editAvatarButton.MoveOnClick = true;
        this.editAvatarButton.Position = new Point(106, 499);
        this.editAvatarButton.Text.Text = SK.Text("User_Edit", "Edit");
        this.editAvatarButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
        this.editAvatarButton.TextYOffset = -3;
        this.editAvatarButton.Text.Color = Color.FromArgb(233, 231, 213);
        this.editAvatarButton.Text.Position = new Point(-3, 0);
        this.editAvatarButton.Text.DropShadowColor = ARGBColors.Black;
        this.editAvatarButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editAvatarClicked), "UserInfo2_edit_avatar_clicked");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.editAvatarButton);
      }
      this.achievementsButton.ImageNorm = (Image) GFXLibrary.char_but_achievement[0];
      this.achievementsButton.ImageOver = (Image) GFXLibrary.char_but_achievement[1];
      this.achievementsButton.ImageClick = (Image) GFXLibrary.char_but_achievement[2];
      this.achievementsButton.Position = new Point(584, 11);
      this.achievementsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.achievementsClicked));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.achievementsButton);
      this.achievementsLabel.Text = userInfo == null || userInfo.achievements == null ? "" : SK.Text("GENERIC_Achievements", "Achievements") + " : " + userInfo.achievements.Count.ToString("N", (IFormatProvider) nfi);
      this.achievementsLabel.Color = ARGBColors.White;
      this.achievementsLabel.DropShadowColor = ARGBColors.Black;
      this.achievementsLabel.Position = new Point(624, 17);
      this.achievementsLabel.Size = new Size(300, 28);
      this.achievementsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.achievementsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.achievementsLabel);
      this.questsButton.ImageNorm = (Image) GFXLibrary.char_but_quest[0];
      this.questsButton.ImageOver = (Image) GFXLibrary.char_but_quest[1];
      this.questsButton.ImageClick = (Image) GFXLibrary.char_but_quest[2];
      if (userInfo == null || userInfo.completedQuests == null)
      {
        this.questsButton.ImageOver = (Image) GFXLibrary.char_but_quest[0];
        this.questsButton.ImageClick = (Image) GFXLibrary.char_but_quest[0];
        this.questsButton.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
      }
      else
        this.questsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.questsClicked), "UserInfo2_quests_clicked");
      this.questsButton.Position = new Point(584, 48);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questsButton);
      this.questsLabel.Text = userInfo == null ? "" : SK.Text("User_Quests_Complete", "Quests Completed") + " : " + userInfo.numQuests.ToString("N", (IFormatProvider) nfi);
      this.questsLabel.Color = ARGBColors.White;
      this.questsLabel.DropShadowColor = ARGBColors.Black;
      this.questsLabel.Position = new Point(624, 54);
      this.questsLabel.Size = new Size(300, 28);
      this.questsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.questsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questsLabel);
      this.headerLabelsImage.Size = new Size(400, 28);
      this.headerLabelsImage.Position = new Point(89, 98);
      this.backgroundRight.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider1Image.Position = new Point(218, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider1Image);
      this.villageLabel.Text = SK.Text("GENERIC_Village", "Village");
      this.villageLabel.Color = ARGBColors.Black;
      this.villageLabel.Position = new Point(20, -3);
      this.villageLabel.Size = new Size(208, this.headerLabelsImage.Height);
      this.villageLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.villageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageLabel);
      this.regionLabel.Text = SK.Text("Users_Region", "Region");
      this.regionLabel.Color = ARGBColors.Black;
      this.regionLabel.Position = new Point(222, -3);
      this.regionLabel.Size = new Size(223, this.headerLabelsImage.Height);
      this.regionLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.regionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.regionLabel);
      if (userInfo != null && userInfo.villages != null)
        this.addVillages(userInfo.villages, userInfo.avatarData.male);
      this.Invalidate();
    }

    public void update()
    {
      WorldMap.CachedUserInfo storedUserInfo = GameEngine.Instance.World.getStoredUserInfo(this.m_userID);
      if (this.m_userInfo == storedUserInfo)
        return;
      this.init(storedUserInfo);
    }

    private void addVillages(int[] villages, bool isMale)
    {
      int num1 = 0;
      int villageID1 = -1;
      this.outgoingScrollArea.Position = new Point(98, 133);
      this.outgoingScrollArea.Size = new Size(360, 360);
      this.outgoingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(360, 360));
      this.backgroundRight.addControl((CustomSelfDrawPanel.CSDControl) this.outgoingScrollArea);
      int num2 = this.outgoingScrollBar.Value;
      this.outgoingScrollBar.Position = new Point(463, 133);
      this.outgoingScrollBar.Size = new Size(24, 360);
      this.backgroundRight.addControl((CustomSelfDrawPanel.CSDControl) this.outgoingScrollBar);
      this.outgoingScrollBar.Value = 0;
      this.outgoingScrollBar.Max = 100;
      this.outgoingScrollBar.NumVisibleLines = 25;
      this.outgoingScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.outgoingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      List<int> intList = new List<int>((IEnumerable<int>) villages);
      intList.Sort((IComparer<int>) UserInfoScreen3.villageComparer);
      this.outgoingScrollArea.clearControls();
      int num3 = 0;
      for (int index = 0; index < intList.Count; ++index)
      {
        int villageID2 = intList[index];
        UserInfoScreen3.VillageLine control = new UserInfoScreen3.VillageLine();
        if (num3 != 0)
          num3 += 5;
        control.Position = new Point(0, num3);
        control.init(villageID2, this, index);
        this.outgoingScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num3 += control.Height;
        this.lineList.Add(control);
        VillageData villageData = GameEngine.Instance.World.getVillageData(villageID2);
        if (villageData != null && villageData.Capital)
        {
          int num4 = 0;
          if (villageData.regionCapital)
            num4 = 1;
          if (villageData.countyCapital)
            num4 = 2;
          if (villageData.provinceCapital)
            num4 = 3;
          if (villageData.countryCapital)
            num4 = 4;
          if (num4 > num1)
          {
            num1 = num4;
            villageID1 = villageID2;
          }
        }
      }
      this.outgoingScrollArea.Size = new Size(this.outgoingScrollArea.Width, num3);
      if (num3 < this.outgoingScrollBar.Height)
      {
        this.outgoingScrollBar.Visible = false;
      }
      else
      {
        this.outgoingScrollBar.Visible = true;
        this.outgoingScrollBar.NumVisibleLines = this.outgoingScrollBar.Height;
        this.outgoingScrollBar.Max = num3 - this.outgoingScrollBar.Height;
      }
      this.outgoingScrollArea.invalidate();
      this.outgoingScrollBar.invalidate();
      if (num1 <= 0)
        return;
      this.positionImage.Image = (Image) GFXLibrary.char_position[num1 - 1];
      this.positionImage.Visible = true;
      string str = "";
      switch (num1)
      {
        case 1:
          str = SK.Text("ParishWallPanel_Steward", "Steward") + " - " + GameEngine.Instance.World.getVillageName(villageID1);
          break;
        case 2:
          str = SK.Text("ParishWallPanel_Sheriff", "Sheriff") + " - " + GameEngine.Instance.World.getCountyName(GameEngine.Instance.World.getCountyFromVillageID(villageID1));
          break;
        case 3:
          str = SK.Text("ParishWallPanel_Governor", "Governor") + " - " + GameEngine.Instance.World.getProvinceName(GameEngine.Instance.World.getProvinceFromVillageID(villageID1));
          break;
        case 4:
          str = (!isMale ? SK.Text("ParishWallPanel_Queen", "Queen") : SK.Text("ParishWallPanel_King", "King")) + " - " + GameEngine.Instance.World.getCountryName(GameEngine.Instance.World.getCountryFromVillageID(villageID1));
          break;
      }
      this.headerLabel2.Text = str;
    }

    private void wallScrollBarMoved()
    {
      int y = this.outgoingScrollBar.Value;
      this.outgoingScrollArea.Position = new Point(this.outgoingScrollArea.X, 133 - y);
      this.outgoingScrollArea.ClipRect = new Rectangle(this.outgoingScrollArea.ClipRect.X, y, this.outgoingScrollArea.ClipRect.Width, this.outgoingScrollArea.ClipRect.Height);
      this.outgoingScrollArea.invalidate();
      this.outgoingScrollBar.invalidate();
    }

    private void closeClick()
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_close");
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
      InterfaceMgr.Instance.closeParishPanel();
      InterfaceMgr.Instance.getMainTabBar().changeTab(0);
    }

    private void adminClick()
    {
      if (this.m_userInfo == null)
        return;
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_close");
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
      InterfaceMgr.Instance.closeParishPanel();
      InterfaceMgr.Instance.getMainTabBar().changeTab(0);
      InterfaceMgr.Instance.showUserInfoScreenAdmin(this.m_userInfo);
    }

    private void editClicked()
    {
      Process.Start(URLs.shieldDesignerURL + "?webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.LanguageIdent.ToLower());
    }

    private void editAvatarClicked()
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_edit_avatar");
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(10);
    }

    public static int getInt32FromString(string text)
    {
      if (text.Length == 0)
        return 0;
      try
      {
        return Convert.ToInt32(text);
      }
      catch (Exception ex)
      {
      }
      return 0;
    }

    public void inviteToFactionClicked()
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_faction_invite");
      InterfaceMgr.Instance.clearControls();
      if (this.m_userInfo == null)
        return;
      InterfaceMgr.Instance.inviteToFaction(this.m_userInfo.userName);
    }

    private void achievementsClicked()
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_achievements");
      if (this.m_userInfo == null)
        return;
      InterfaceMgr.Instance.openAchievements(this.m_userInfo.achievements);
    }

    private void questsClicked()
    {
      if (this.m_userInfo == null)
        return;
      InterfaceMgr.Instance.openNewQuestsCompletedPopup(this.m_userInfo.completedQuests);
    }

    private void sendMailClicked()
    {
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_send_mail");
      if (this.m_userInfo == null)
        return;
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(21);
      InterfaceMgr.Instance.mailTo(this.m_userInfo.userID, this.m_userInfo.userName);
    }

    private void factionClicked()
    {
      if (this.m_userInfo == null || this.m_userInfo.factionID < 0)
        return;
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_faction");
      InterfaceMgr.Instance.closeParishPanel();
      InterfaceMgr.Instance.showFactionPanel(this.m_userInfo.factionID);
    }

    private void houseClicked()
    {
      if (this.m_userInfo == null || this.m_houseID <= 0)
        return;
      InterfaceMgr.Instance.closeParishPanel();
      InterfaceMgr.Instance.showHousePanel(this.m_houseID);
    }

    private void manageDiplomacyClicked() => InterfaceMgr.Instance.setVillageTabSubMode(60, false);

    public void addDiplomacyOverlay()
    {
      if (this.m_userInfo == null)
        return;
      this.removeOverlay();
      this.diplomacyOverlayVisible = true;
      this.greyOverlay.Position = new Point(0, -this.mainHeaderArea.Height);
      this.greyOverlay.Size = this.Size;
      this.greyOverlay.FillColor = Color.FromArgb(128, 0, 0, 0);
      this.greyOverlay.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => { }));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.greyOverlay);
      this.diplomacyHeaderImage.Size = new Size(500, 40);
      this.diplomacyHeaderImage.Position = new Point((this.Width - 500) / 2, 100);
      this.greyOverlay.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyHeaderImage);
      this.diplomacyHeaderImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
      this.diplomacyBackgroundImage.Size = new Size(500, 300);
      this.diplomacyBackgroundImage.Position = new Point((this.Width - 500) / 2, 140);
      this.greyOverlay.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyBackgroundImage);
      this.diplomacyBackgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
      this.diplomacyHeadingLabel.Text = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy");
      this.diplomacyHeadingLabel.Color = ARGBColors.White;
      this.diplomacyHeadingLabel.Position = new Point(0, 0);
      this.diplomacyHeadingLabel.Size = this.diplomacyHeaderImage.Size;
      this.diplomacyHeadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.diplomacyHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.diplomacyHeaderImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyHeadingLabel);
      this.diplomacyFactionLabel.Text = this.m_userInfo.userName;
      this.diplomacyFactionLabel.Color = ARGBColors.Black;
      this.diplomacyFactionLabel.Position = new Point(0, 8);
      this.diplomacyFactionLabel.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
      this.diplomacyFactionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.diplomacyFactionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.diplomacyFactionLabel);
      this.statusHeaderLabel.Text = SK.Text("GENERIC_Current_Relationship", "Current Relationship");
      this.statusHeaderLabel.Color = ARGBColors.Black;
      this.statusHeaderLabel.Position = new Point(0, 40);
      this.statusHeaderLabel.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
      this.statusHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.statusHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.statusHeaderLabel);
      this.allyButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.allyButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.allyButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.allyButton.setSizeToImage();
      this.allyButton.Position = new Point(this.diplomacyBackgroundImage.Width / 6 - this.allyButton.Width / 2, this.statusHeaderLabel.Rectangle.Bottom + 5);
      this.allyButton.Text.Text = SK.Text("GENERIC_Ally", "Ally");
      this.allyButton.Text.Color = ARGBColors.Green;
      this.allyButton.Text.Font = this.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.allyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.onAllyClick));
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.allyButton);
      this.neutralButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.neutralButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.neutralButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.neutralButton.setSizeToImage();
      this.neutralButton.Position = new Point(this.diplomacyBackgroundImage.Width / 2 - this.neutralButton.Width / 2, this.statusHeaderLabel.Rectangle.Bottom + 5);
      this.neutralButton.Text.Text = SK.Text("GENERIC_Neutral", "Neutral");
      this.neutralButton.Text.Color = ARGBColors.Black;
      this.neutralButton.Text.Font = this.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.neutralButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.onNeutralClick));
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.neutralButton);
      this.enemyButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.enemyButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.enemyButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.enemyButton.setSizeToImage();
      this.enemyButton.Position = new Point(this.diplomacyBackgroundImage.Width * 5 / 6 - this.enemyButton.Width / 2, this.statusHeaderLabel.Rectangle.Bottom + 5);
      this.enemyButton.Text.Text = SK.Text("GENERIC_Enemy", "Enemy");
      this.enemyButton.Text.Color = ARGBColors.Red;
      this.enemyButton.Text.Font = this.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.enemyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.onEnemyClick));
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.enemyButton);
      UserRelationship relationshipData = GameEngine.Instance.World.getUserRelationshipData(this.m_userInfo.userID);
      if (relationshipData == null)
      {
        this.neutralButton.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
        this.neutralButton.ImageOver = (Image) GFXLibrary.button_132_over_gold;
        this.neutralButton.ImageClick = (Image) GFXLibrary.button_132_in_gold;
        this.isAlly = false;
        this.isEnemy = false;
      }
      else if (relationshipData.friendly)
      {
        this.allyButton.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
        this.allyButton.ImageOver = (Image) GFXLibrary.button_132_over_gold;
        this.allyButton.ImageClick = (Image) GFXLibrary.button_132_in_gold;
        this.isAlly = true;
        this.isEnemy = false;
      }
      else
      {
        this.enemyButton.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
        this.enemyButton.ImageOver = (Image) GFXLibrary.button_132_over_gold;
        this.enemyButton.ImageClick = (Image) GFXLibrary.button_132_in_gold;
        this.isAlly = false;
        this.isEnemy = true;
      }
      this.markerHeaderLabel.Text = SK.Text("GENERIC_Player_Marker", "Assign Player Marker");
      this.markerHeaderLabel.Color = ARGBColors.Black;
      this.markerHeaderLabel.Position = new Point(0, this.neutralButton.Rectangle.Bottom + 20);
      this.markerHeaderLabel.Size = new Size(this.diplomacyBackgroundImage.Width, 20);
      this.markerHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.markerHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.markerHeaderLabel);
      UserMarker userMarker = GameEngine.Instance.World.getUserMarker(this.m_userInfo.userID);
      this.currentMarkerType = 0;
      for (int index = 0; index < GFXLibrary.custom_player_marker.Length; ++index)
      {
        this.markerButtons[index] = new CustomSelfDrawPanel.CSDButton();
        if (userMarker != null && userMarker.markerType == index + 1)
        {
          this.markerButtons[index].ImageNorm = (Image) GFXLibrary.custom_player_marker_selected[index];
          this.markerButtons[index].ImageOver = (Image) GFXLibrary.custom_player_marker_selected[index];
          this.markerButtons[index].ImageClick = (Image) GFXLibrary.custom_player_marker_selected[index];
          this.markerButtons[index].setSizeToImage();
          this.currentMarkerType = userMarker.markerType;
        }
        else
        {
          this.markerButtons[index].ImageNorm = (Image) GFXLibrary.custom_player_marker[index];
          this.markerButtons[index].ImageOver = (Image) GFXLibrary.custom_player_marker[index];
          this.markerButtons[index].ImageClick = (Image) GFXLibrary.custom_player_marker[index];
          this.markerButtons[index].setSizeToImage();
        }
        this.markerButtons[index].Position = new Point(this.diplomacyBackgroundImage.Width * (index % 4 + 1) / 5 - this.markerButtons[index].Width / 2, this.markerHeaderLabel.Rectangle.Bottom + 10 + index / 4 * (this.markerButtons[index].Height + 5));
        this.markerButtons[index].Data = index;
        this.markerButtons[index].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.onMarkerClick));
        this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.markerButtons[index]);
      }
      this.updateButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.updateButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.updateButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.updateButton.setSizeToImage();
      this.updateButton.Position = new Point(this.diplomacyBackgroundImage.Width / 2 - this.updateButton.Width / 2, this.diplomacyBackgroundImage.Height - this.updateButton.Height - 10);
      this.updateButton.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.updateButton.Text.Color = ARGBColors.Black;
      this.updateButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.updateRelationship));
      this.diplomacyBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.updateButton);
      this.relationshipChanged = false;
      this.pendingMarkerUpdate = false;
      this.pendingRelationshipUpdate = false;
    }

    private void onMarkerClick()
    {
      if (this.pendingMarkerUpdate)
        return;
      CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) this.ClickedControl;
      int data = clickedControl.Data;
      if (data + 1 == this.currentMarkerType)
      {
        clickedControl.ImageNorm = (Image) GFXLibrary.custom_player_marker[data];
        clickedControl.ImageOver = (Image) GFXLibrary.custom_player_marker[data];
        clickedControl.ImageClick = (Image) GFXLibrary.custom_player_marker[data];
        this.currentMarkerType = 0;
      }
      else
      {
        for (int index = 0; index < this.markerButtons.Length; ++index)
        {
          if (index == data)
          {
            this.markerButtons[index].ImageNorm = (Image) GFXLibrary.custom_player_marker_selected[index];
            this.markerButtons[index].ImageOver = (Image) GFXLibrary.custom_player_marker_selected[index];
            this.markerButtons[index].ImageClick = (Image) GFXLibrary.custom_player_marker_selected[index];
            this.currentMarkerType = index + 1;
          }
          else
          {
            this.markerButtons[index].ImageNorm = (Image) GFXLibrary.custom_player_marker[index];
            this.markerButtons[index].ImageOver = (Image) GFXLibrary.custom_player_marker[index];
            this.markerButtons[index].ImageClick = (Image) GFXLibrary.custom_player_marker[index];
          }
        }
      }
      this.Invalidate();
    }

    private void onAllyClick()
    {
      if (this.pendingRelationshipUpdate || this.isAlly)
        return;
      this.isAlly = true;
      this.isEnemy = false;
      this.allyButton.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
      this.allyButton.ImageOver = (Image) GFXLibrary.button_132_over_gold;
      this.allyButton.ImageClick = (Image) GFXLibrary.button_132_in_gold;
      this.enemyButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.enemyButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.enemyButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.neutralButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.neutralButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.neutralButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.relationshipChanged = true;
    }

    private void onNeutralClick()
    {
      if (this.pendingRelationshipUpdate || !this.isAlly && !this.isEnemy)
        return;
      this.isAlly = false;
      this.isEnemy = false;
      this.neutralButton.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
      this.neutralButton.ImageOver = (Image) GFXLibrary.button_132_over_gold;
      this.neutralButton.ImageClick = (Image) GFXLibrary.button_132_in_gold;
      this.allyButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.allyButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.allyButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.enemyButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.enemyButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.enemyButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.relationshipChanged = true;
    }

    private void onEnemyClick()
    {
      if (this.pendingRelationshipUpdate || this.isEnemy)
        return;
      this.isAlly = false;
      this.isEnemy = true;
      this.enemyButton.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
      this.enemyButton.ImageOver = (Image) GFXLibrary.button_132_over_gold;
      this.enemyButton.ImageClick = (Image) GFXLibrary.button_132_in_gold;
      this.allyButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.allyButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.allyButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.neutralButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.neutralButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.neutralButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.relationshipChanged = true;
    }

    public void removeOverlay()
    {
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.greyOverlay);
      this.greyOverlay.clearControls();
      this.Invalidate();
      this.diplomacyOverlayVisible = false;
      RemoteServices.Instance.set_CreateUserRelationship_UserCallBack((RemoteServices.CreateUserRelationship_UserCallBack) null);
      RemoteServices.Instance.set_SetUserMarker_UserCallBack((RemoteServices.SetUserMarker_UserCallBack) null);
    }

    public void updateRelationship()
    {
      UserMarker userMarker = GameEngine.Instance.World.getUserMarker(this.m_userInfo.userID);
      if (userMarker == null || userMarker.markerType != this.currentMarkerType)
      {
        GameEngine.Instance.World.setUserMarker(this.m_userInfo.userID, this.currentMarkerType, this.m_userInfo.userName);
        RemoteServices.Instance.set_SetUserMarker_UserCallBack(new RemoteServices.SetUserMarker_UserCallBack(this.updateMarkerCallback));
        RemoteServices.Instance.SetUserMarker(this.m_userInfo.userID, this.currentMarkerType);
        this.pendingMarkerUpdate = true;
      }
      if (this.relationshipChanged)
      {
        if (this.isAlly)
        {
          GameEngine.Instance.World.setUserRelationship(this.m_userInfo.userID, 1, this.m_userInfo.userName);
          RemoteServices.Instance.CreateUserRelationship(this.m_userInfo.userID, 1);
        }
        else if (this.isEnemy)
        {
          GameEngine.Instance.World.setUserRelationship(this.m_userInfo.userID, -1, this.m_userInfo.userName);
          RemoteServices.Instance.CreateUserRelationship(this.m_userInfo.userID, -1);
        }
        else
        {
          GameEngine.Instance.World.setUserRelationship(this.m_userInfo.userID, 0, this.m_userInfo.userName);
          RemoteServices.Instance.CreateUserRelationship(this.m_userInfo.userID, 0);
        }
        RemoteServices.Instance.set_CreateUserRelationship_UserCallBack(new RemoteServices.CreateUserRelationship_UserCallBack(this.updateRelationshipCallback));
        this.pendingRelationshipUpdate = true;
      }
      this.removeOverlay();
    }

    private void updateMarkerCallback(SetUserMarker_ReturnType returnData)
    {
      this.pendingMarkerUpdate = false;
    }

    private void updateRelationshipCallback(CreateUserRelationship_ReturnType returnData)
    {
      this.pendingRelationshipUpdate = false;
    }

    private void btnAlly_Click()
    {
      this.diplomacyEnemyButton.Enabled = false;
      this.diplomacyAllyButton.Enabled = false;
      this.diplomacyNeutralButton.Enabled = false;
      if (GameEngine.Instance.World.UserRelations.Count == 0)
        StatTrackingClient.Instance().ActivateTrigger(11, (object) 0);
      GameEngine.Instance.World.setUserRelationship(this.m_userInfo.userID, 1, this.m_userInfo.userName);
      RemoteServices.Instance.set_CreateUserRelationship_UserCallBack(new RemoteServices.CreateUserRelationship_UserCallBack(this.createUserRelationshipCallback));
      RemoteServices.Instance.CreateUserRelationship(this.m_userInfo.userID, 1);
    }

    private void btnMakeEnemy_Click()
    {
      this.diplomacyEnemyButton.Enabled = false;
      this.diplomacyAllyButton.Enabled = false;
      this.diplomacyNeutralButton.Enabled = false;
      if (GameEngine.Instance.World.UserRelations.Count == 0)
        StatTrackingClient.Instance().ActivateTrigger(11, (object) 0);
      GameEngine.Instance.World.setUserRelationship(this.m_userInfo.userID, -1, this.m_userInfo.userName);
      RemoteServices.Instance.set_CreateUserRelationship_UserCallBack(new RemoteServices.CreateUserRelationship_UserCallBack(this.createUserRelationshipCallback));
      RemoteServices.Instance.CreateUserRelationship(this.m_userInfo.userID, -1);
    }

    private void btnBreakAlliance_Click()
    {
      this.diplomacyEnemyButton.Enabled = false;
      this.diplomacyAllyButton.Enabled = false;
      this.diplomacyNeutralButton.Enabled = false;
      GameEngine.Instance.World.setUserRelationship(this.m_userInfo.userID, 0, this.m_userInfo.userName);
      RemoteServices.Instance.set_CreateUserRelationship_UserCallBack(new RemoteServices.CreateUserRelationship_UserCallBack(this.createUserRelationshipCallback));
      RemoteServices.Instance.CreateUserRelationship(this.m_userInfo.userID, 0);
    }

    private void createUserRelationshipCallback(CreateUserRelationship_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.diplomacyOverlayVisible = false;
        this.init(this.m_userInfo);
      }
      else
      {
        this.diplomacyEnemyButton.Enabled = true;
        this.diplomacyAllyButton.Enabled = true;
        this.diplomacyNeutralButton.Enabled = true;
      }
    }

    private void repairAllClicked()
    {
      this.repairCount = 0;
      this.repairButton.Enabled = false;
      this.startNextRepair();
    }

    private void startNextRepair()
    {
      this.currentRepairCastle = (UserInfoScreen3.VillageLine) null;
      foreach (UserInfoScreen3.VillageLine line in this.lineList)
      {
        if (line.RepairAvailable)
        {
          this.currentRepairCastle = line;
          this.startRepair();
          return;
        }
      }
      this.repairButton.Enabled = true;
      foreach (UserInfoScreen3.VillageLine line in this.lineList)
        line.resetRepairStatus();
    }

    private void startRepair()
    {
      if (this.currentRepairCastle == null)
        return;
      this.currentRepairCastle.startRepair();
      RemoteServices.Instance.set_AutoRepairCastle_UserCallBack(new RemoteServices.AutoRepairCastle_UserCallBack(this.repairCallback));
      RemoteServices.Instance.AutoRepairCastle(this.currentRepairCastle.villageID);
    }

    private void repairCallback(AutoRepairCastle_ReturnType returnData)
    {
      if (returnData.Success)
      {
        VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
        if (village != null)
        {
          village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
          if (returnData.elements != null)
            GameEngine.Instance.Castle.importElements(returnData.elements);
        }
        bool flag = false;
        if (returnData.elements != null)
        {
          foreach (CastleElement element in returnData.elements)
          {
            if ((double) element.damage > 0.0)
            {
              flag = true;
              break;
            }
          }
        }
        this.currentRepairCastle.completeRepair(!flag);
        if (!flag)
          ++this.repairCount;
        this.Invalidate();
      }
      this.startNextRepair();
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
      this.MaximumSize = new Size(992, 590);
      this.MinimumSize = new Size(992, 590);
      this.Name = nameof (UserInfoScreen3);
      this.Size = new Size(992, 590);
      this.ResumeLayout(false);
    }

    public class VillageComparer : IComparer<int>
    {
      public int Compare(int x, int y)
      {
        VillageData villageData1 = GameEngine.Instance.World.getVillageData(x);
        VillageData villageData2 = GameEngine.Instance.World.getVillageData(y);
        if (villageData1 == null)
          return villageData2 == null ? 0 : -1;
        if (villageData2 == null || villageData1.Capital && !villageData2.Capital)
          return 1;
        if (!villageData1.Capital && villageData2.Capital)
          return -1;
        if (villageData1.Capital && villageData2.Capital)
        {
          int num1 = 0;
          int num2 = 0;
          if (villageData1.countyCapital)
            num1 = 1;
          else if (villageData1.provinceCapital)
            num1 = 2;
          else if (villageData1.countryCapital)
            num1 = 3;
          if (villageData2.countyCapital)
            num2 = 1;
          else if (villageData2.provinceCapital)
            num2 = 2;
          else if (villageData2.countryCapital)
            num2 = 3;
          if (num1 < num2)
            return -1;
          if (num2 < num1)
            return 1;
        }
        return villageData1.villageName.CompareTo(villageData2.villageName);
      }
    }

    public class VillageLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage villageImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel villageNameLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel regionLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage repairStatusImage = new CustomSelfDrawPanel.CSDImage();
      private UserInfoScreen3 m_parent;
      private int m_villageID = -1;
      public bool RepairAvailable = true;

      public int villageID => this.m_villageID;

      public void init(int villageID, UserInfoScreen3 parent, int position)
      {
        this.m_villageID = villageID;
        this.m_parent = parent;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.char_line_02 : (Image) GFXLibrary.char_line_01;
        this.backgroundImage.Position = new Point(0, 5);
        this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.Size = new Size(360, 30);
        this.villageNameLabel.Text = GameEngine.Instance.World.getVillageName(villageID);
        this.villageNameLabel.Color = ARGBColors.Black;
        this.villageNameLabel.RolloverColor = ARGBColors.White;
        this.villageNameLabel.Position = new Point(50, -10);
        this.villageNameLabel.Size = new Size(160, this.backgroundImage.Height + 20);
        this.villageNameLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.villageNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.villageNameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageNameLabel);
        this.regionLabel.Color = ARGBColors.Black;
        this.regionLabel.RolloverColor = ARGBColors.White;
        this.regionLabel.Position = new Point(220, -10);
        this.regionLabel.Size = new Size(140, this.backgroundImage.Height + 20);
        this.regionLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.regionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.regionLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.regionLabel);
        if (GameEngine.Instance.World.isCapital(villageID))
        {
          int num = 0;
          if (GameEngine.Instance.World.isRegionCapital(villageID))
          {
            num = 0;
            int villageCounty = GameEngine.Instance.World.getVillageCounty(villageID);
            this.regionLabel.Text = GameEngine.Instance.World.getCountyName(villageCounty);
          }
          else if (GameEngine.Instance.World.isCountyCapital(villageID))
          {
            num = 1;
            int villageCounty = GameEngine.Instance.World.getVillageCounty(villageID);
            this.regionLabel.Text = GameEngine.Instance.World.getCountyName(villageCounty);
          }
          else if (GameEngine.Instance.World.isProvinceCapital(villageID))
          {
            num = 2;
            int provinceFromVillageId = GameEngine.Instance.World.getProvinceFromVillageID(villageID);
            this.regionLabel.Text = GameEngine.Instance.World.getProvinceName(provinceFromVillageId);
          }
          else if (GameEngine.Instance.World.isCountryCapital(villageID))
          {
            num = 3;
            int countryFromVillageId = GameEngine.Instance.World.getCountryFromVillageID(villageID);
            this.regionLabel.Text = GameEngine.Instance.World.getCountryName(countryFromVillageId);
          }
          this.villageImage.Image = (Image) GFXLibrary.char_position[num + 4];
          this.villageImage.Position = new Point(10, -4);
          this.villageImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageImage);
        }
        else
        {
          int villageSize = GameEngine.Instance.World.getVillageSize(villageID);
          this.villageImage.Image = (Image) GFXLibrary.char_village_icons[villageSize];
          this.villageImage.Position = new Point(-5, -18);
          this.villageImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageImage);
          int villageCounty = GameEngine.Instance.World.getVillageCounty(villageID);
          this.regionLabel.Text = GameEngine.Instance.World.getCountyName(villageCounty);
          if (!GameEngine.Instance.World.isUserVillage(villageID))
            return;
          this.repairStatusImage.Position = new Point(this.Width * 5 / 6, this.Height / 2 - 16);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.repairStatusImage);
        }
      }

      public bool update(double localTime) => true;

      private void lineClicked()
      {
        if (this.m_villageID < 0)
          return;
        if (RemoteServices.Instance.Admin && GameEngine.shiftPressed)
        {
          AGUR agur = new AGUR();
          agur.init(this.m_villageID);
          agur.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
        }
        else
        {
          GameEngine.Instance.playInterfaceSound("UserinfoScreenLine_village");
          Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.m_villageID);
          GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
          InterfaceMgr.Instance.closeParishPanel();
          InterfaceMgr.Instance.getMainTabBar().changeTab(0);
          GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
          InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_villageID, false, true, true, false);
        }
      }

      public void startRepair()
      {
        this.RepairAvailable = false;
        this.villageNameLabel.Color = ARGBColors.Green;
      }

      public void completeRepair(bool success)
      {
        int num = success ? 1 : 0;
        this.repairStatusImage.setSizeToImage();
        this.villageNameLabel.Color = ARGBColors.Black;
      }

      public void resetRepairStatus()
      {
        this.RepairAvailable = true;
        this.villageNameLabel.Color = ARGBColors.Black;
      }
    }
  }
}
