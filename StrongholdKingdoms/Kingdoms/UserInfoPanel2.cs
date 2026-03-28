// Decompiled with JetBrains decompiler
// Type: Kingdoms.UserInfoPanel2
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
  public class UserInfoPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage avatarImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage avatarBackImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton moreInfo = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailToButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
    private int lastShieldUserID = -1;
    private int lastFlagData = -1;
    private int lastUserID = -1;
    private WorldMap.CachedUserInfo m_userInfo;
    public Bitmap avatarBitmap;

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
      this.BackColor = ARGBColors.Transparent;
      this.Name = nameof (UserInfoPanel2);
      this.Size = new Size(200, 378);
      this.ResumeLayout(false);
    }

    public UserInfoPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.lastShieldUserID = -1;
      this.clearControls();
      this.avatarBackImage.Image = (Image) GFXLibrary.mrhp_avatar_frame_background;
      this.avatarBackImage.Position = new Point(0, 110);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.avatarBackImage);
      this.avatarImage.Image = (Image) null;
      this.avatarImage.Visible = false;
      this.avatarImage.Position = new Point(71, 113);
      this.background.Image = (Image) GFXLibrary.mrhp_avatar_frame;
      this.background.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.avatarImage);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
      this.flagImage.createFromFlagData(0);
      this.flagImage.Position = new Point(136, 48);
      this.flagImage.Scale = 0.25;
      this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClicked), "UserInfoPanel2_faction_flag");
      this.flagImage.Visible = false;
      this.flagImage.CustomTooltipID = 2501;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.flagImage);
      this.houseImage.Image = (Image) null;
      this.houseImage.Position = new Point(15, 38);
      this.houseImage.Visible = false;
      this.houseImage.CustomTooltipID = 2307;
      this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "UserInfoPanel2_house");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.houseImage);
      this.shieldImage.Image = (Image) null;
      this.shieldImage.Position = new Point(74, 31);
      this.shieldImage.Visible = false;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
      this.nameLabel.Text = "";
      this.nameLabel.Color = ARGBColors.Black;
      this.nameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.nameLabel.Position = new Point(8, 79);
      this.nameLabel.Size = new Size(this.background.Width - 12, 45);
      this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.nameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.nameClicked), "UserInfoPanel2_name");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.nameLabel);
      this.rankLabel.Text = "";
      this.rankLabel.Color = ARGBColors.Black;
      this.rankLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.rankLabel.Position = new Point(8, 317);
      this.rankLabel.Size = new Size(this.background.Width - 12, 20);
      this.rankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.rankLabel);
      this.pointsLabel.Text = "";
      this.pointsLabel.Color = ARGBColors.Black;
      this.pointsLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      this.pointsLabel.Position = new Point(8, 330);
      this.pointsLabel.Size = new Size(this.background.Width - 12, 20);
      this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
      this.moreInfo.ImageNorm = (Image) GFXLibrary.mrhp_button_more_info_solid[0];
      this.moreInfo.ImageOver = (Image) GFXLibrary.mrhp_button_more_info_solid[1];
      this.moreInfo.Position = new Point((200 - this.moreInfo.ImageNorm.Width) / 2 + 6, 353);
      this.moreInfo.MoveOnClick = true;
      this.moreInfo.Text.Text = SK.Text("UserInfo_MoreInfo", "More Info");
      this.moreInfo.Text.Font = Program.mySettings.LanguageIdent == "it" || Program.mySettings.LanguageIdent == "tr" || Program.mySettings.LanguageIdent == "pt" ? FontManager.GetFont("Arial", 7.5f, FontStyle.Bold) : FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      this.moreInfo.TextYOffset = -3;
      this.moreInfo.Text.Position = new Point(-3, 0);
      this.moreInfo.Text.Color = ARGBColors.Black;
      this.moreInfo.Text.DropShadowColor = Color.FromArgb(60, 90, 100);
      this.moreInfo.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.nameClicked), "UserInfoPanel2_more_info");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.moreInfo);
      this.mailToButton.ImageNorm = (Image) GFXLibrary.mrhp_button_envelope[0];
      this.mailToButton.ImageOver = (Image) GFXLibrary.mrhp_button_envelope[1];
      this.mailToButton.ImageClick = (Image) GFXLibrary.mrhp_button_envelope[2];
      this.mailToButton.Position = new Point(157, 259);
      this.mailToButton.CustomTooltipID = 2502;
      this.mailToButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailToClick), "UserInfoPanel2_mail_to");
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.mailToButton);
      this.lastFlagData = -1;
    }

    public void updateVillageInfo(
      WorldMap.VillageRolloverInfo villageInfo,
      WorldMap.CachedUserInfo userInfo)
    {
      CustomTooltipManager.UserInfo = userInfo;
      this.m_userInfo = userInfo;
      this.pointsLabel.CustomTooltipID = 0;
      this.rankLabel.CustomTooltipID = 0;
      if (userInfo != null)
      {
        this.pointsLabel.CustomTooltipID = 2503;
        this.rankLabel.CustomTooltipID = 2503;
        NumberFormatInfo nfi = GameEngine.NFI;
        this.nameLabel.TextDiffOnly = userInfo.userName;
        this.rankLabel.TextDiffOnly = userInfo.avatarData == null ? Rankings.getRankingName(userInfo.rank) : Rankings.getRankingName(userInfo.rank, userInfo.avatarData.male);
        if (userInfo.userID != this.lastShieldUserID)
        {
          this.lastShieldUserID = userInfo.userID;
          this.shieldImage.Image = GameEngine.Instance.World.getWorldShieldOrBlank(userInfo.userID, 47, 54);
          if (this.shieldImage.Image != null)
          {
            GameEngine.Instance.World.showShieldUser(this.lastShieldUserID);
            this.shieldImage.Visible = true;
          }
          else
            this.shieldImage.Visible = false;
        }
        if (userInfo.factionID >= 0)
        {
          FactionData faction = GameEngine.Instance.World.getFaction(userInfo.factionID);
          if (faction != null)
          {
            if (this.lastFlagData != faction.flagData)
              this.flagImage.createFromFlagData(faction.flagData);
            this.flagImage.CustomTooltipData = faction.factionID;
            this.flagImage.Visible = true;
            if (faction.houseID > 0)
            {
              this.houseImage.Image = this.houseImage.Image = (Image) GFXLibrary.getHouseCircleMediumImage(faction.houseID - 1);
              this.houseImage.CustomTooltipData = faction.houseID;
              this.houseImage.Visible = true;
            }
            else
              this.houseImage.Visible = false;
          }
          else
          {
            this.flagImage.Visible = false;
            this.houseImage.Visible = false;
          }
        }
        else
        {
          this.flagImage.Visible = false;
          this.houseImage.Visible = false;
        }
        this.avatarImage.Image = (Image) userInfo.avatarBitmap;
        this.avatarImage.Visible = true;
        int num = userInfo.numVillages;
        if (GameEngine.Instance.LocalWorldData.AIWorld)
        {
          switch (userInfo.userID)
          {
            case 1:
              num = GameEngine.Instance.World.countRatsCastles();
              break;
            case 2:
              num = GameEngine.Instance.World.countSnakesCastles();
              break;
            case 3:
              num = GameEngine.Instance.World.countPigsCastles();
              break;
            case 4:
              num = GameEngine.Instance.World.countWolfsCastles();
              break;
          }
        }
        this.pointsLabel.TextDiffOnly = SK.Text("GENERIC_Villages", "Villages") + " : " + num.ToString("N", (IFormatProvider) nfi);
        this.lastUserID = userInfo.userID;
      }
      else
      {
        this.avatarImage.Visible = false;
        this.rankLabel.TextDiffOnly = "";
        this.nameLabel.TextDiffOnly = "";
        this.pointsLabel.TextDiffOnly = "";
      }
    }

    private void closing() => GameEngine.Instance.World.showShieldUser(-1);

    private void mailToClick()
    {
      if (this.m_userInfo == null || this.m_userInfo.userID < 0)
        return;
      int userId = this.m_userInfo.userID;
      string userName = this.m_userInfo.userName;
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(21);
      InterfaceMgr.Instance.mailTo(userId, userName);
    }

    private void nameClicked()
    {
      if (this.m_userInfo == null)
        return;
      InterfaceMgr.Instance.showUserInfoScreen(this.m_userInfo);
    }

    private void factionClicked()
    {
      if (this.m_userInfo == null || this.m_userInfo.factionID < 0)
        return;
      InterfaceMgr.Instance.showFactionPanel(this.m_userInfo.factionID);
    }

    private void houseClicked()
    {
      if (this.m_userInfo == null || this.m_userInfo.factionID < 0)
        return;
      FactionData faction = GameEngine.Instance.World.getFaction(this.m_userInfo.factionID);
      if (faction == null)
        return;
      InterfaceMgr.Instance.showHousePanel(faction.houseID);
    }
  }
}
