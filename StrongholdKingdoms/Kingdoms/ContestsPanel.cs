// Decompiled with JetBrains decompiler
// Type: Kingdoms.ContestsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ContestsPanel : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private List<ContestPrizeDefinition> m_Prizes = new List<ContestPrizeDefinition>();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel descriptionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel titleBar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage leftTrumpet = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rightTrumpet = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel leaderboardHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel prizesHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel prizeContentHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel prizeContentInsetHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel playerRankHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel playerScoreHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel playerRankValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel playerScoreValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lastUpdateLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel timeRemainingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDExtendingPanel leaderboardInset = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel prizesInset = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel prizeContentInset = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDButton closePrizeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton topButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton upButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton downButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton bottomButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton historyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton nextTierButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton prevTierButton = new CustomSelfDrawPanel.CSDButton();
    private ContestsPanel.AsyncDelegate m_PrizeRequestDel;
    private ContestsPanel.ResponseDelegate m_PrizeResponseDel;
    private ContestCachedData leaderboardData;
    private DateTime m_LastLocalUpdate = DateTime.MinValue;
    private ContestPrizeList m_PrizeContent = new ContestPrizeList();

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
      this.MinimumSize = new Size(992, 594);
      this.Name = "EventsPanel";
      this.Size = new Size(992, 594);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public ContestsPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      this.clearControls();
      if (!resized)
        Program.mySettings.LastContestViewed = GameEngine.Instance.World.contestID;
      CustomSelfDrawPanel.CSDExtendingPanel control = new CustomSelfDrawPanel.CSDExtendingPanel();
      control.Position = new Point(0, 0);
      control.Size = this.Size;
      control.Create((Image) GFXLibrary._9sclice_generic_top_left, (Image) GFXLibrary._9sclice_generic_top_mid, (Image) GFXLibrary._9sclice_generic_top_right, (Image) GFXLibrary._9sclice_generic_mid_left, (Image) GFXLibrary._9sclice_generic_mid_mid, (Image) GFXLibrary._9sclice_generic_mid_right, (Image) GFXLibrary._9sclice_generic_bottom_left, (Image) GFXLibrary._9sclice_generic_bottom_mid, (Image) GFXLibrary._9sclice_generic_bottom_right);
      this.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.leaderboardInset.Size = new Size(this.Width / 2 - 50, this.Height - 200);
      this.leaderboardInset.Position = new Point(40, 150);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.leaderboardInset);
      this.leaderboardInset.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      this.prizesInset.Size = new Size(this.Width / 2 - 50, this.Height / 4);
      this.prizesInset.Position = new Point(this.Width / 2 + 10, this.leaderboardInset.Y);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.prizesInset);
      this.prizesInset.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      this.titleBar.Size = new Size(this.Width - 240, GFXLibrary.contestTitleLeft.Height);
      this.titleBar.Position = new Point(120, 20);
      this.titleBar.Create((Image) GFXLibrary.contestTitleLeft, (Image) GFXLibrary.contestTitleMid, (Image) GFXLibrary.contestTitleRight);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.titleBar);
      this.titleLabel.Text = GameEngine.Instance.World.contestName;
      this.titleLabel.Color = ARGBColors.Black;
      this.titleLabel.Size = new Size(this.Width, GFXLibrary.contestTitleLeft.Height);
      this.titleLabel.Position = new Point(0, 20);
      this.titleLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
      this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.titleLabel);
      this.descriptionLabel.Text = GameEngine.Instance.World.contestDescription;
      this.descriptionLabel.Color = ARGBColors.Black;
      this.descriptionLabel.Size = new Size(this.Width, 20);
      this.descriptionLabel.Position = new Point(0, this.titleLabel.Rectangle.Bottom + 10);
      this.descriptionLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.descriptionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.descriptionLabel);
      this.leftTrumpet.Image = (Image) GFXLibrary.contestTrumpetLeftSmall;
      this.leftTrumpet.setSizeToImage();
      this.leftTrumpet.Position = new Point(15, 15);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.leftTrumpet);
      this.rightTrumpet.Image = (Image) GFXLibrary.contestTrumpetRightSmall;
      this.rightTrumpet.setSizeToImage();
      this.rightTrumpet.Position = new Point(this.Width - this.rightTrumpet.Width - 15, 15);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.rightTrumpet);
      int contestBand = ContestsPanel.GetContestBand(GameEngine.Instance.World.getRank());
      this.leaderboardHeaderLabel.Text = SK.Text("Event_Leaderboard_Header", "Leaderboard");
      switch (contestBand)
      {
        case 1:
          CustomSelfDrawPanel.CSDLabel leaderboardHeaderLabel1 = this.leaderboardHeaderLabel;
          leaderboardHeaderLabel1.Text = leaderboardHeaderLabel1.Text + " (" + SK.Text("TOUCH_Z_Commoners", "Commoners") + ")";
          break;
        case 2:
          CustomSelfDrawPanel.CSDLabel leaderboardHeaderLabel2 = this.leaderboardHeaderLabel;
          leaderboardHeaderLabel2.Text = leaderboardHeaderLabel2.Text + " (" + SK.Text("TOUCH_Z_Gentry", "Gentry") + ")";
          break;
        case 3:
          CustomSelfDrawPanel.CSDLabel leaderboardHeaderLabel3 = this.leaderboardHeaderLabel;
          leaderboardHeaderLabel3.Text = leaderboardHeaderLabel3.Text + " (" + SK.Text("TOUCH_Z_Nobility", "Nobility") + ")";
          break;
      }
      this.leaderboardHeaderLabel.Color = ARGBColors.Black;
      this.leaderboardHeaderLabel.Size = new Size(this.leaderboardInset.Width * 4 / 5, GFXLibrary.contestTitleLeft.Height);
      this.leaderboardHeaderLabel.Position = new Point(this.leaderboardInset.X + this.leaderboardInset.Width / 5, this.leaderboardInset.Y - this.leaderboardHeaderLabel.Height - 2);
      this.leaderboardHeaderLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.leaderboardHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.leaderboardHeaderLabel.CustomTooltipID = 25003;
      this.leaderboardHeaderLabel.CustomTooltipData = contestBand;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.leaderboardHeaderLabel);
      this.prevTierButton.ImageNorm = (Image) GFXLibrary.contestArrowLeft;
      this.prevTierButton.ImageOver = (Image) GFXLibrary.contestArrowLeft;
      this.prevTierButton.ImageClick = (Image) GFXLibrary.contestArrowLeft;
      this.prevTierButton.setSizeToImage();
      this.prevTierButton.Position = new Point(this.leaderboardInset.X, this.leaderboardHeaderLabel.Y + this.leaderboardHeaderLabel.Height / 2 - this.prevTierButton.Height / 2);
      this.prevTierButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ViewPrevTier), "Contest_prev_tier");
      control.addControl((CustomSelfDrawPanel.CSDControl) this.prevTierButton);
      this.nextTierButton.ImageNorm = (Image) GFXLibrary.contestArrowRight;
      this.nextTierButton.ImageOver = (Image) GFXLibrary.contestArrowRight;
      this.nextTierButton.ImageClick = (Image) GFXLibrary.contestArrowRight;
      this.nextTierButton.setSizeToImage();
      this.nextTierButton.Position = new Point(this.leaderboardInset.Rectangle.Right - this.prevTierButton.Width, this.leaderboardHeaderLabel.Y + this.leaderboardHeaderLabel.Height / 2 - this.nextTierButton.Height / 2);
      this.nextTierButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ViewNextTier), "Contest_next_tier");
      control.addControl((CustomSelfDrawPanel.CSDControl) this.nextTierButton);
      this.prizesHeaderLabel.Text = SK.Text("Event_Prizes_Header", "Prizes");
      this.prizesHeaderLabel.Color = ARGBColors.Black;
      this.prizesHeaderLabel.Size = new Size(this.prizesInset.Width, 30);
      this.prizesHeaderLabel.Position = new Point(this.prizesInset.X, this.prizesInset.Y - this.prizesHeaderLabel.Height - 3);
      this.prizesHeaderLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
      this.prizesHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.prizesHeaderLabel);
      this.playerRankHeaderLabel.Text = SK.Text("Event_Your_Position", "Your position") + ": ";
      this.playerRankHeaderLabel.Color = ARGBColors.Black;
      this.playerRankHeaderLabel.Size = new Size(this.Width / 4, 40);
      this.playerRankHeaderLabel.Position = new Point(this.Width / 2, this.prizesInset.Rectangle.Bottom + 20);
      this.playerRankHeaderLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.playerRankHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.playerRankHeaderLabel);
      this.playerRankValueLabel.Color = ARGBColors.Black;
      this.playerRankValueLabel.Size = new Size(this.playerRankHeaderLabel.Width, 40);
      this.playerRankValueLabel.Position = new Point(this.playerRankHeaderLabel.X, this.playerRankHeaderLabel.Rectangle.Bottom + 5);
      this.playerRankValueLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.playerRankValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.playerRankValueLabel);
      this.playerScoreHeaderLabel.Text = SK.Text("Event_Your_Score", "Your score") + ": ";
      this.playerScoreHeaderLabel.Color = ARGBColors.Black;
      this.playerScoreHeaderLabel.Size = new Size(this.Width / 4, 40);
      this.playerScoreHeaderLabel.Position = new Point(this.Width * 3 / 4, this.playerRankHeaderLabel.Y);
      this.playerScoreHeaderLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.playerScoreHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.playerScoreHeaderLabel);
      this.playerScoreValueLabel.Color = ARGBColors.Black;
      this.playerScoreValueLabel.Size = new Size(this.playerScoreHeaderLabel.Width, 40);
      this.playerScoreValueLabel.Position = new Point(this.playerScoreHeaderLabel.X, this.playerScoreHeaderLabel.Rectangle.Bottom + 5);
      this.playerScoreValueLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.playerScoreValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.playerScoreValueLabel);
      TimeSpan timeSpan = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestEndTime) - VillageMap.getCurrentServerTime();
      int num1 = (int) timeSpan.TotalSeconds / 86400;
      int num2 = (int) timeSpan.TotalSeconds / 3600 % 24;
      int num3 = (int) timeSpan.TotalSeconds / 60 % 60;
      string str = "";
      if (num1 > 0)
        str = str + num1.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + ":";
      if (num2 > 0)
      {
        if (num2 < 10)
          str += "0";
        str = str + num2.ToString() + SK.Text("VillageMap_Hour_Abbrev", "h");
      }
      if (num3 > 0)
      {
        if (num3 < 10)
          str += "0";
        str = str + num3.ToString() + SK.Text("VillageMap_Minute_Abbrev", "m");
      }
      if (!string.IsNullOrEmpty(str))
      {
        this.timeRemainingLabel.Text = SK.Text("Event_Time_Remaining", "Ends in") + " ";
        CustomSelfDrawPanel.CSDLabel timeRemainingLabel = this.timeRemainingLabel;
        timeRemainingLabel.Text = timeRemainingLabel.Text + Environment.NewLine + str;
      }
      this.timeRemainingLabel.Color = ARGBColors.Black;
      this.timeRemainingLabel.Size = new Size(this.prizesInset.Width, 50);
      this.timeRemainingLabel.Position = new Point(this.prizesInset.X, this.playerScoreValueLabel.Rectangle.Bottom);
      this.timeRemainingLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
      this.timeRemainingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.timeRemainingLabel);
      this.topButton.ImageNorm = (Image) GFXLibrary.page_top_norrmal;
      this.topButton.ImageOver = (Image) GFXLibrary.page_top_over;
      this.topButton.ImageClick = (Image) GFXLibrary.page_top_pushed;
      this.topButton.setSizeToImage();
      int x = this.leaderboardInset.Width - this.topButton.Width - 2;
      this.topButton.Position = new Point(x, 4);
      this.topButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.LeaderboardScrollTop), "StatsPanel_scroll_top");
      this.leaderboardInset.addControl((CustomSelfDrawPanel.CSDControl) this.topButton);
      this.upButton.ImageNorm = (Image) GFXLibrary.page_up_normal;
      this.upButton.ImageOver = (Image) GFXLibrary.page_up_over;
      this.upButton.ImageClick = (Image) GFXLibrary.page_up_pushed;
      this.upButton.setSizeToImage();
      this.upButton.Position = new Point(x, this.topButton.Rectangle.Bottom + 2);
      this.upButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.LeaderboardScrollUp), "StatsPanel_scroll_up");
      this.leaderboardInset.addControl((CustomSelfDrawPanel.CSDControl) this.upButton);
      this.bottomButton.ImageNorm = (Image) GFXLibrary.page_bottom_normal;
      this.bottomButton.ImageOver = (Image) GFXLibrary.page_bottom_over;
      this.bottomButton.ImageClick = (Image) GFXLibrary.page_bottom_pushed;
      this.bottomButton.setSizeToImage();
      this.bottomButton.Position = new Point(x, this.leaderboardInset.Height - this.bottomButton.Height - 2);
      this.bottomButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.LeaderboardScrollBottom), "StatsPanel_scroll_bottom");
      this.leaderboardInset.addControl((CustomSelfDrawPanel.CSDControl) this.bottomButton);
      this.downButton.ImageNorm = (Image) GFXLibrary.page_down_normal;
      this.downButton.ImageOver = (Image) GFXLibrary.page_down_over;
      this.downButton.ImageClick = (Image) GFXLibrary.page_down_pushed;
      this.downButton.setSizeToImage();
      this.downButton.Position = new Point(x, this.bottomButton.Position.Y - 2 - this.downButton.Height);
      this.downButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.LeaderboardScrollDown), "StatsPanel_scroll_down");
      this.leaderboardInset.addControl((CustomSelfDrawPanel.CSDControl) this.downButton);
      this.timeRemainingLabel.Color = ARGBColors.Black;
      this.timeRemainingLabel.Size = new Size(this.prizesInset.Width, 50);
      this.timeRemainingLabel.Position = new Point(this.prizesInset.X, this.playerScoreValueLabel.Rectangle.Bottom);
      this.timeRemainingLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
      this.timeRemainingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.timeRemainingLabel);
      this.lastUpdateLabel.Color = ARGBColors.Black;
      this.lastUpdateLabel.Size = new Size(this.leaderboardInset.Width, 20);
      this.lastUpdateLabel.Position = new Point(this.leaderboardInset.X, this.leaderboardInset.Rectangle.Bottom + 2);
      this.lastUpdateLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.lastUpdateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.lastUpdateLabel);
      this.historyButton = new CustomSelfDrawPanel.CSDButton();
      this.historyButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.historyButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.historyButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.historyButton.setSizeToImage();
      this.historyButton.Text.Text = SK.Text("Tourneys_Past", "Past Tourneys");
      this.historyButton.Position = new Point(this.Width - this.historyButton.Width - 30, this.Height - this.historyButton.Height - 30);
      this.historyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.OnHistoryClicked));
      control.addControl((CustomSelfDrawPanel.CSDControl) this.historyButton);
      this.prizeContentInset.Size = new Size(400, 400);
      this.prizeContentInset.Position = new Point(this.Width / 2 - this.prizeContentInset.Width / 2, this.Height / 2 - this.prizeContentInset.Height / 2);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.prizeContentInset);
      this.prizeContentInset.Visible = false;
      if (new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestStartTime).AddHours(1.5) > VillageMap.getCurrentServerTime())
      {
        this.timeRemainingLabel.Text = SK.Text("Tourney_Awaiting_Scores", "Tourney under way - awaiting initial scores");
        this.playerRankValueLabel.Text = "-";
        this.playerScoreValueLabel.Text = "-";
        this.leaderboardData = new ContestCachedData();
        this.leaderboardData.metaDataCallback = new ContestCachedData.ContestCacheCallbackDelegate(this.UpdateMetaData);
        this.leaderboardData.ID = GameEngine.Instance.World.contestID;
        this.leaderboardData.RetrieveMetaData();
      }
      else if (!resized)
      {
        this.leaderboardData = new ContestCachedData();
        this.leaderboardData.leaderboardCallback = new ContestCachedData.ContestCacheCallbackDelegate(this.PopulateLeaderboard);
        this.leaderboardData.userDataCallback = new ContestCachedData.ContestCacheCallbackDelegate(this.UpdateUserInfo);
        this.leaderboardData.metaDataCallback = new ContestCachedData.ContestCacheCallbackDelegate(this.UpdateMetaData);
        this.leaderboardData.ID = GameEngine.Instance.World.contestID;
        this.leaderboardData.visibleTier = contestBand;
        this.leaderboardData.visibleLineCount = this.potentialLineCount;
        this.leaderboardData.SetAsVisible();
      }
      else
      {
        if (this.leaderboardData.activeLeaderboard == null)
          return;
        this.leaderboardData.visibleLineCount = this.potentialLineCount;
        this.leaderboardData.SetAsVisible();
      }
    }

    public void update()
    {
      TimeSpan timeSpan = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestEndTime) - VillageMap.getCurrentServerTime();
      int num1 = (int) timeSpan.TotalSeconds / 86400;
      int num2 = (int) timeSpan.TotalSeconds / 3600 % 24;
      int num3 = (int) timeSpan.TotalSeconds / 60 % 60;
      string str = "";
      if (num1 > 0)
        str = str + num1.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + ":";
      if (num2 > 0)
      {
        if (num2 < 10)
          str += "0";
        str = str + num2.ToString() + SK.Text("VillageMap_Hour_Abbrev", "h");
      }
      if (num3 > 0)
      {
        if (num3 < 10)
          str += "0";
        str = str + num3.ToString() + SK.Text("VillageMap_Minute_Abbrev", "m");
      }
      if (string.IsNullOrEmpty(str))
        return;
      this.timeRemainingLabel.TextDiffOnly = SK.Text("Event_Time_Remaining", "Ends in") + " " + Environment.NewLine + str;
    }

    private void PopulateLeaderboard(bool success)
    {
      this.nextTierButton.Enabled = this.leaderboardData.visibleTier < 3;
      this.prevTierButton.Enabled = this.leaderboardData.visibleTier > 1;
      if (!success)
        return;
      this.ClearLeaderboard();
      int num = (int) Math.Floor((double) (this.leaderboardInset.Height - GFXLibrary.lineitem_strip_02_dark.Height * 4 / 3 * this.potentialLineCount) / (double) (this.potentialLineCount + 1));
      int y = num;
      for (int index = 0; index < this.actualLineCount; ++index)
      {
        ContestRankLine control = new ContestRankLine();
        control.init(this.leaderboardData.activeLeaderboard[index], (CustomSelfDrawPanel.CSDControl) this.leaderboardInset, index % 2 == 1);
        control.Position = new Point(0, y);
        this.leaderboardInset.addControl((CustomSelfDrawPanel.CSDControl) control);
        y += control.Height + num;
      }
      this.leaderboardHeaderLabel.Text = SK.Text("Event_Leaderboard_Header", "Leaderboard");
      switch (this.leaderboardData.visibleTier)
      {
        case 1:
          CustomSelfDrawPanel.CSDLabel leaderboardHeaderLabel1 = this.leaderboardHeaderLabel;
          leaderboardHeaderLabel1.Text = leaderboardHeaderLabel1.Text + " (" + SK.Text("TOUCH_Z_Commoners", "Commoners") + ")";
          break;
        case 2:
          CustomSelfDrawPanel.CSDLabel leaderboardHeaderLabel2 = this.leaderboardHeaderLabel;
          leaderboardHeaderLabel2.Text = leaderboardHeaderLabel2.Text + " (" + SK.Text("TOUCH_Z_Gentry", "Gentry") + ")";
          break;
        case 3:
          CustomSelfDrawPanel.CSDLabel leaderboardHeaderLabel3 = this.leaderboardHeaderLabel;
          leaderboardHeaderLabel3.Text = leaderboardHeaderLabel3.Text + " (" + SK.Text("TOUCH_Z_Nobility", "Nobility") + ")";
          break;
      }
      this.leaderboardHeaderLabel.CustomTooltipData = this.leaderboardData.visibleTier;
      this.Invalidate();
    }

    private void ClearLeaderboard()
    {
      List<CustomSelfDrawPanel.CSDControl> csdControlList = new List<CustomSelfDrawPanel.CSDControl>();
      foreach (CustomSelfDrawPanel.CSDControl control in this.leaderboardInset.Controls)
      {
        if (control.GetType() == typeof (ContestRankLine))
          csdControlList.Add(control);
      }
      foreach (CustomSelfDrawPanel.CSDControl control in csdControlList)
        this.leaderboardInset.removeControl(control);
    }

    private void UpdateMetaData(bool success)
    {
      if (!success)
        return;
      this.ClearPrizes();
      int y = 2;
      foreach (ContestPrizeDefinition priz in this.leaderboardData.prizes)
      {
        if (priz.RankTier + 1 == this.leaderboardData.visibleTier)
        {
          this.prizesHeaderLabel.Text = priz.Content.Name;
          ContestPrizeLine control = new ContestPrizeLine();
          control.init(priz, (CustomSelfDrawPanel.CSDControl) this.prizesInset, this);
          control.Position = new Point(2, y);
          this.prizesInset.addControl((CustomSelfDrawPanel.CSDControl) control);
          y += control.Height + 2;
        }
      }
      this.Invalidate();
    }

    private void ClearPrizes()
    {
      List<CustomSelfDrawPanel.CSDControl> csdControlList = new List<CustomSelfDrawPanel.CSDControl>();
      foreach (CustomSelfDrawPanel.CSDControl control in this.prizesInset.Controls)
      {
        if (control.GetType() == typeof (ContestPrizeLine))
          csdControlList.Add(control);
      }
      foreach (CustomSelfDrawPanel.CSDControl control in csdControlList)
        this.prizesInset.removeControl(control);
    }

    private void UpdateUserInfo(bool success)
    {
      if (success)
      {
        this.playerScoreValueLabel.Text = ((int) Math.Floor(this.leaderboardData.userScore)).ToString();
        this.playerRankValueLabel.Text = this.leaderboardData.userPosition.ToString();
      }
      if (this.leaderboardData != null && this.leaderboardData.lastUpdate != DateTime.MinValue)
      {
        TimeSpan timeSpan = VillageMap.getCurrentServerTime() - this.leaderboardData.lastUpdate;
        this.m_LastLocalUpdate = this.leaderboardData.lastUpdate;
        this.lastUpdateLabel.Text = SK.Text("Events_Last_Update", "Time since last update") + ": ";
        CustomSelfDrawPanel.CSDLabel lastUpdateLabel = this.lastUpdateLabel;
        lastUpdateLabel.Text = lastUpdateLabel.Text + ": " + Math.Floor(timeSpan.TotalMinutes).ToString() + SK.Text("VillageMap_Minute_Abbrev", "m");
      }
      else
      {
        this.lastUpdateLabel.Text = "";
        this.m_LastLocalUpdate = DateTime.MinValue;
      }
    }

    private void ViewNextTier()
    {
      if (this.leaderboardData.activeLeaderboard == null)
        return;
      this.nextTierButton.Enabled = false;
      this.prevTierButton.Enabled = false;
      this.leaderboardData.NextTier();
    }

    private void ViewPrevTier()
    {
      if (this.leaderboardData.activeLeaderboard == null)
        return;
      this.nextTierButton.Enabled = false;
      this.prevTierButton.Enabled = false;
      this.leaderboardData.PrevTier();
    }

    private void LeaderboardScrollUp()
    {
      if (this.leaderboardData.activeLeaderboard == null)
        return;
      this.leaderboardData.ScrollUp();
    }

    private void LeaderboardScrollDown()
    {
      if (this.leaderboardData.activeLeaderboard == null)
        return;
      this.leaderboardData.ScrollDown();
    }

    private void LeaderboardScrollTop()
    {
      if (this.leaderboardData.activeLeaderboard == null)
        return;
      this.leaderboardData.ScrollToTop();
    }

    private void LeaderboardScrollBottom()
    {
      if (this.leaderboardData.activeLeaderboard == null)
        return;
      this.leaderboardData.ScrollToBottom();
    }

    public void OnPrizeInfoClicked()
    {
      int data = this.ClickedControl.Data;
      foreach (ContestPrizeDefinition priz in this.leaderboardData.prizes)
      {
        if (priz.Content.ID == data)
        {
          this.prizeContentInset.clearControls();
          CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
          control.Image = (Image) GFXLibrary.castlescreen_panelback_A;
          control.Size = this.prizeContentInset.Size;
          this.prizeContentInset.addControl((CustomSelfDrawPanel.CSDControl) control);
          this.prizeContentInset.Create((Image) GFXLibrary._9sclice_generic_top_left, (Image) GFXLibrary._9sclice_generic_top_mid, (Image) GFXLibrary._9sclice_generic_top_right, (Image) GFXLibrary._9sclice_generic_mid_left, (Image) GFXLibrary._9sclice_generic_mid_mid, (Image) GFXLibrary._9sclice_generic_mid_right, (Image) GFXLibrary._9sclice_generic_bottom_left, (Image) GFXLibrary._9sclice_generic_bottom_mid, (Image) GFXLibrary._9sclice_generic_bottom_right);
          this.prizeContentInsetHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
          this.prizeContentInsetHeaderLabel.Text = priz.Content.Name;
          this.prizeContentInsetHeaderLabel.Color = ARGBColors.Black;
          this.prizeContentInsetHeaderLabel.Size = new Size(this.prizeContentInset.Width, this.prizeContentInset.Height / 4 - 20);
          this.prizeContentInsetHeaderLabel.Position = new Point(0, 20);
          this.prizeContentInsetHeaderLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
          this.prizeContentInsetHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.prizeContentInset.addControl((CustomSelfDrawPanel.CSDControl) this.prizeContentInsetHeaderLabel);
          this.closePrizeButton = new CustomSelfDrawPanel.CSDButton();
          this.closePrizeButton.ImageNorm = (Image) GFXLibrary.button_132_in_gold;
          this.closePrizeButton.ImageOver = (Image) GFXLibrary.button_132_over_gold;
          this.closePrizeButton.ImageClick = (Image) GFXLibrary.button_132_in_gold;
          this.closePrizeButton.setSizeToImage();
          this.closePrizeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
          this.closePrizeButton.Position = new Point(this.prizeContentInset.Width / 2 - this.closePrizeButton.Width / 2, this.prizeContentInset.Height - this.closePrizeButton.Height - 10);
          this.closePrizeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClosePrizeInfo));
          this.prizeContentInset.addControl((CustomSelfDrawPanel.CSDControl) this.closePrizeButton);
          this.m_PrizeContent.clearControls();
          this.prizeContentInset.Visible = true;
          this.m_PrizeContent.init(priz, (CustomSelfDrawPanel.CSDControl) this.prizeContentInset, 20, 30, this.prizeContentInsetHeaderLabel.Height);
          this.prizeContentInset.addControl((CustomSelfDrawPanel.CSDControl) this.m_PrizeContent);
          this.prizeContentInset.invalidate();
          this.Invalidate();
          break;
        }
      }
    }

    public void ClosePrizeInfo() => this.prizeContentInset.Visible = false;

    private void OnHistoryClicked()
    {
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_leaderboard");
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(31);
    }

    private int potentialLineCount
    {
      get
      {
        return (int) Math.Floor((double) this.leaderboardInset.Height / (double) (GFXLibrary.lineitem_strip_02_dark.Height * 4 / 3));
      }
    }

    private int actualLineCount
    {
      get => Math.Min(this.potentialLineCount, this.leaderboardData.activeLeaderboard.Length);
    }

    public void logout()
    {
      this.leaderboardData = (ContestCachedData) null;
      this.m_LastLocalUpdate = DateTime.MinValue;
    }

    public static int GetContestBand(int rank)
    {
      if (rank >= 18)
        return 3;
      return rank >= 12 ? 2 : 1;
    }

    private delegate void AsyncDelegate();

    private delegate void ResponseDelegate(bool success);
  }
}
