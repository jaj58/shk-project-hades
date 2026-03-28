// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionInvitePanel
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
  public class FactionInvitePanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int PANEL_ID = 41;
    private DockableControl dockableControl;
    private IContainer components;
    public static FactionInvitePanel instance;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel playerNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel applicationsNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDVertScrollBar appScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea appScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl appMouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private int blockYSize;
    private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
    private List<FactionInvitePanel.FactionInviteLine> lineList = new List<FactionInvitePanel.FactionInviteLine>();
    private List<FactionInvitePanel.FactionInviteLine> lineList2 = new List<FactionInvitePanel.FactionInviteLine>();

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
      this.Name = nameof (FactionInvitePanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public FactionInvitePanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      this.blockYSize = height / 2;
      FactionInvitePanel.instance = this;
      this.clearControls();
      this.sidebar.addSideBar(0, (CustomSelfDrawPanel) this);
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(this.Width - 200, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width - 200, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23 - 200, 28);
      this.headerLabelsImage.Position = new Point(25, 9);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.playerNameLabel.Text = SK.Text("FactionsPanel_Users", "Invites");
      this.playerNameLabel.Color = ARGBColors.Black;
      this.playerNameLabel.Position = new Point(9, -2);
      this.playerNameLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.playerNameLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.playerNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.playerNameLabel);
      this.headerLabelsImage2.Size = new Size(this.Width - 25 - 23 - 200, 28);
      this.headerLabelsImage2.Position = new Point(25, this.blockYSize);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage2);
      this.headerLabelsImage2.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.applicationsNameLabel.Text = SK.Text("FactionInvites_Applications", "Applications");
      this.applicationsNameLabel.Color = ARGBColors.Black;
      this.applicationsNameLabel.Position = new Point(9, -2);
      this.applicationsNameLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.applicationsNameLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.applicationsNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.applicationsNameLabel);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_Faction_Invites", "Faction Invites"));
      this.wallScrollArea.Position = new Point(25, 38);
      this.wallScrollArea.Size = new Size(705, this.blockYSize - 50);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(705, this.blockYSize - 50));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      int num1 = this.wallScrollBar.Value;
      this.wallScrollBar.Visible = false;
      this.wallScrollBar.Position = new Point(733, 38);
      this.wallScrollBar.Size = new Size(24, this.blockYSize - 50);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.appScrollArea.Position = new Point(25, 38 + this.blockYSize);
      this.appScrollArea.Size = new Size(705, this.blockYSize - 50);
      this.appScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(705, this.blockYSize - 50));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.appScrollArea);
      this.appMouseWheelOverlay.Position = this.appScrollArea.Position;
      this.appMouseWheelOverlay.Size = this.appScrollArea.Size;
      this.appMouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.appMouseWheelMoved));
      this.mainBackgroundImage.addControl(this.appMouseWheelOverlay);
      int num2 = this.appScrollBar.Value;
      this.appScrollBar.Visible = false;
      this.appScrollBar.Position = new Point(733, 38 + this.blockYSize);
      this.appScrollBar.Size = new Size(24, this.blockYSize - 50);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.appScrollBar);
      this.appScrollBar.Value = 0;
      this.appScrollBar.Max = 100;
      this.appScrollBar.NumVisibleLines = 25;
      this.appScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.appScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.appScrollBarMoved));
      if (GameEngine.Instance.World.getRank() < 6)
      {
        this.rankLabel.Text = SK.Text("FACTION_INVITE_rank", "You don't currently have the required Rank (7) to join a Faction.");
        this.rankLabel.Color = ARGBColors.Black;
        this.rankLabel.Position = new Point(0, 50);
        this.rankLabel.Size = this.wallScrollArea.Size;
        this.rankLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.rankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.rankLabel);
      }
      if (!resized)
        CustomSelfDrawPanel.FactionPanelSideBar.downloadCurrentFactionInfo();
      this.addPlayers();
    }

    public void update() => this.sidebar.update();

    public void logout()
    {
    }

    private void wallScrollBarMoved()
    {
      int y = this.wallScrollBar.Value;
      this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 38 - y);
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

    private void appScrollBarMoved()
    {
      int y = this.appScrollBar.Value;
      this.appScrollArea.Position = new Point(this.appScrollArea.X, 38 + this.blockYSize - y);
      this.appScrollArea.ClipRect = new Rectangle(this.appScrollArea.ClipRect.X, y, this.appScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
      this.appScrollArea.invalidate();
      this.appScrollBar.invalidate();
    }

    private void appMouseWheelMoved(int delta)
    {
      if (!this.appScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.appScrollBar.scrollDown(40);
      }
      else
      {
        if (delta <= 0)
          return;
        this.appScrollBar.scrollUp(40);
      }
    }

    public void closing()
    {
    }

    public void addPlayers()
    {
      this.wallScrollArea.clearControls();
      this.appScrollArea.clearControls();
      int num1 = 0;
      int num2 = 0;
      this.lineList.Clear();
      this.lineList2.Clear();
      if (GameEngine.Instance.World.getRank() >= 6)
      {
        int position1 = 0;
        FactionInviteData[] factionInvites = GameEngine.Instance.World.FactionInvites;
        if (factionInvites != null)
        {
          foreach (FactionInviteData factionInviteData in factionInvites)
          {
            FactionData faction = GameEngine.Instance.World.getFaction(factionInviteData.factionID);
            if (faction != null)
            {
              FactionInvitePanel.FactionInviteLine control = new FactionInvitePanel.FactionInviteLine();
              if (num1 != 0)
                num1 += 5;
              control.Position = new Point(0, num1);
              control.init(faction, position1, this, true);
              this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
              num1 += control.Height;
              this.lineList.Add(control);
              ++position1;
            }
          }
        }
        int position2 = 0;
        List<FactionInviteData> factionApplications = GameEngine.Instance.World.FactionApplications;
        if (factionApplications != null)
        {
          foreach (FactionInviteData factionInviteData in factionApplications)
          {
            FactionData faction = GameEngine.Instance.World.getFaction(factionInviteData.factionID);
            if (faction != null)
            {
              FactionInvitePanel.FactionInviteLine control = new FactionInvitePanel.FactionInviteLine();
              if (num2 != 0)
                num2 += 5;
              control.Position = new Point(0, num2);
              control.init(faction, position2, this, false);
              this.appScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
              num2 += control.Height;
              this.lineList2.Add(control);
              ++position2;
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
      this.appScrollArea.Size = new Size(this.appScrollArea.Width, num2);
      if (num1 < this.appScrollBar.Height)
      {
        this.appScrollBar.Visible = false;
      }
      else
      {
        this.appScrollBar.Visible = true;
        this.appScrollBar.NumVisibleLines = this.appScrollBar.Height;
        this.appScrollBar.Max = num2 - this.appScrollBar.Height;
      }
      this.appScrollArea.invalidate();
      this.appScrollBar.invalidate();
      this.update();
      this.Invalidate();
    }

    public class FactionInviteLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton acceptButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDButton declineButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
      private int m_position = -1000;
      private FactionData m_factionData;
      private FactionInvitePanel m_parent;

      public void init(
        FactionData factionData,
        int position,
        FactionInvitePanel parent,
        bool invite)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_factionData = factionData;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(60, 0);
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.flagImage.createFromFlagData(factionData.flagData);
        this.flagImage.Position = new Point(0, 0);
        this.flagImage.Scale = 0.25;
        this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.flagImage);
        NumberFormatInfo nfi = GameEngine.NFI;
        this.playerName.Text = factionData.factionName;
        this.playerName.Color = ARGBColors.Black;
        this.playerName.Position = new Point(9, 0);
        this.playerName.Size = new Size(280, this.backgroundImage.Height);
        this.playerName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.playerName);
        if (!GameEngine.Instance.World.WorldEnded)
        {
          if (invite)
          {
            if (RemoteServices.Instance.UserFactionID < 0)
            {
              this.acceptButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
              this.acceptButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
              this.acceptButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
              this.acceptButton.Position = new Point(350, 0);
              this.acceptButton.Text.Text = SK.Text("FactionInviteLine_Accept", "Accept");
              this.acceptButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              this.acceptButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
              this.acceptButton.TextYOffset = -3;
              this.acceptButton.Text.Color = ARGBColors.Black;
              this.acceptButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.acceptClicked), "FactionInvitePanel_accept_clicked");
              this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.acceptButton);
            }
            this.declineButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.declineButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.declineButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.declineButton.Position = new Point(500, 0);
            this.declineButton.Text.Text = SK.Text("FactionInviteLine_Decline", "Decline");
            this.declineButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.declineButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.declineButton.TextYOffset = -3;
            this.declineButton.Text.Color = ARGBColors.Black;
            this.declineButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.declineClicked), "FactionInvitePanel_declined_clicked");
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.declineButton);
          }
          else
          {
            this.declineButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.declineButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.declineButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.declineButton.Position = new Point(500, 0);
            this.declineButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.declineButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.declineButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.declineButton.TextYOffset = -3;
            this.declineButton.Text.Color = ARGBColors.Black;
            this.declineButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.appCancelClicked), "FactionInvitePanel_declined_clicked");
            this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.declineButton);
          }
        }
        this.invalidate();
      }

      public void update()
      {
      }

      public void clickedLine()
      {
        GameEngine.Instance.playInterfaceSound("FactionInvitePanel_faction_clicked");
        InterfaceMgr.Instance.showFactionPanel(this.m_factionData.factionID);
      }

      public void declineClicked()
      {
        this.declineButton.Enabled = false;
        this.acceptButton.Enabled = false;
        RemoteServices.Instance.set_FactionReplyToInvite_UserCallBack(new RemoteServices.FactionReplyToInvite_UserCallBack(this.factionReplyToInviteCallback));
        RemoteServices.Instance.FactionReplyToInvite(this.m_factionData.factionID, false);
      }

      public void acceptClicked()
      {
        this.declineButton.Enabled = false;
        this.acceptButton.Enabled = false;
        RemoteServices.Instance.set_FactionReplyToInvite_UserCallBack(new RemoteServices.FactionReplyToInvite_UserCallBack(this.factionReplyToInviteCallback));
        RemoteServices.Instance.FactionReplyToInvite(this.m_factionData.factionID, true);
      }

      public void factionReplyToInviteCallback(FactionReplyToInvite_ReturnType returnData)
      {
        if (returnData.m_errorCode == ErrorCodes.ErrorCode.FACTION_FULL)
        {
          int num = (int) MyMessageBox.Show(SK.Text("FactionsPanel_Faction_Full", "The Faction is full."), SK.Text("GENERIC_Error", "Error"));
        }
        if (returnData.Success)
        {
          GameEngine.Instance.World.FactionMembers = returnData.members;
          GameEngine.Instance.World.YourFaction = returnData.yourFaction;
          GameEngine.Instance.World.FactionInvites = returnData.invites;
          GameEngine.Instance.World.FactionApplications = returnData.applications;
          if (returnData.yourFaction != null)
          {
            GameEngine.Instance.World.updateYourVillageFactions(returnData.yourFaction.factionID);
            if (returnData.decline)
            {
              this.m_parent.init(false);
              return;
            }
            GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
            GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
            GameEngine.Instance.World.HouseAllies = returnData.yourHouseAllies;
            GameEngine.Instance.World.HouseEnemies = returnData.yourHouseEnemies;
          }
          else
            GameEngine.Instance.World.updateYourVillageFactions(-1);
          GameEngine.Instance.World.LastUpdatedCrowns = DateTime.MinValue;
          InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(1);
        }
        else
        {
          this.declineButton.Enabled = true;
          this.acceptButton.Enabled = true;
        }
      }

      public void appCancelClicked()
      {
        this.declineButton.Enabled = false;
        RemoteServices.Instance.set_FactionApplication_UserCallBack(new RemoteServices.FactionApplication_UserCallBack(this.factionApplicationCallback));
        RemoteServices.Instance.FactionApplicationCancel(this.m_factionData.factionID);
      }

      public void factionApplicationCallback(FactionApplication_ReturnType returnData)
      {
        if (returnData.Success)
        {
          GameEngine.Instance.World.FactionInvites = returnData.invites;
          GameEngine.Instance.World.FactionApplications = returnData.applications;
          this.m_parent.init(false);
        }
        else
          this.declineButton.Enabled = true;
      }
    }
  }
}
