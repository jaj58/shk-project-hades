// Decompiled with JetBrains decompiler
// Type: Kingdoms.ContestHistoryPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ContestHistoryPanel : CustomSelfDrawPanel
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.CSDLabel panelHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel tierLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel endDateLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel tierBar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDButton activeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel leaderboardInset = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDButton topButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton upButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton downButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton bottomButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton nextContestButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton prevContestButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton nextTierButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton prevTierButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel playerRankHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel playerRankValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage leftSupport = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rightSupport = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage leftTrumpet = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage rightTrumpet = new CustomSelfDrawPanel.CSDImage();
    private bool initialised;
    private int[] contestIDs;
    private int visibleContestIndex = -1;
    private List<ContestCachedData> contestData = new List<ContestCachedData>();

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

    public ContestHistoryPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      this.clearControls();
      CustomSelfDrawPanel.CSDExtendingPanel control = new CustomSelfDrawPanel.CSDExtendingPanel();
      control.Position = new Point(0, 0);
      control.Size = this.Size;
      control.Create((Image) GFXLibrary._9sclice_generic_top_left, (Image) GFXLibrary._9sclice_generic_top_mid, (Image) GFXLibrary._9sclice_generic_top_right, (Image) GFXLibrary._9sclice_generic_mid_left, (Image) GFXLibrary._9sclice_generic_mid_mid, (Image) GFXLibrary._9sclice_generic_mid_right, (Image) GFXLibrary._9sclice_generic_bottom_left, (Image) GFXLibrary._9sclice_generic_bottom_mid, (Image) GFXLibrary._9sclice_generic_bottom_right);
      this.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.panelHeaderLabel.Text = SK.Text("Tourneys_Past", "Past Tourneys");
      this.panelHeaderLabel.Color = ARGBColors.Black;
      this.panelHeaderLabel.Size = new Size(this.Width, 50);
      this.panelHeaderLabel.Position = new Point(0, 20);
      this.panelHeaderLabel.Font = FontManager.GetFont("Arial", 24f, FontStyle.Bold);
      this.panelHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.panelHeaderLabel);
      this.leaderboardInset.Size = new Size(this.Width / 2 - 20, this.Height - 300);
      this.leaderboardInset.Position = new Point(this.Width / 2 - this.leaderboardInset.Width / 2, 200);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.leaderboardInset);
      this.leaderboardInset.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
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
      this.titleLabel.Text = "";
      this.titleLabel.Color = ARGBColors.Black;
      this.titleLabel.Size = new Size(control.Width, 160);
      this.titleLabel.Position = new Point(0, 80);
      this.titleLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.titleLabel);
      this.tierBar.Size = new Size(control.Width / 4, GFXLibrary.int_statsscreen_iconbar_left.Height);
      this.tierBar.Position = new Point(control.Width / 2 - this.tierBar.Width / 2, this.leaderboardInset.Y - this.tierBar.Height - 5);
      this.tierBar.Create((Image) GFXLibrary.contestTitleLeft, (Image) GFXLibrary.contestTitleMid, (Image) GFXLibrary.contestTitleRight);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.tierBar);
      this.tierLabel.Color = ARGBColors.Black;
      this.tierLabel.Size = this.tierBar.Size;
      this.tierLabel.Position = new Point(this.tierBar.X, this.tierBar.Y + 2);
      this.tierLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.tierLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.tierLabel);
      this.playerRankHeaderLabel.Text = SK.Text("Event_Final_Position", "Your final position") + ": ";
      this.playerRankHeaderLabel.Color = ARGBColors.Black;
      this.playerRankHeaderLabel.Size = new Size(this.leaderboardInset.Width, 25);
      this.playerRankHeaderLabel.Position = new Point(this.leaderboardInset.X, this.leaderboardInset.Rectangle.Bottom + 5);
      this.playerRankHeaderLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.playerRankHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.playerRankHeaderLabel);
      this.playerRankValueLabel.Color = ARGBColors.Black;
      this.playerRankValueLabel.Size = new Size(this.leaderboardInset.Width, 30);
      this.playerRankValueLabel.Position = new Point(this.playerRankHeaderLabel.X, this.playerRankHeaderLabel.Rectangle.Bottom + 10);
      this.playerRankValueLabel.Font = FontManager.GetFont("Arial", 15f, FontStyle.Bold);
      this.playerRankValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      control.addControl((CustomSelfDrawPanel.CSDControl) this.playerRankValueLabel);
      this.prevTierButton.ImageNorm = (Image) GFXLibrary.contestArrowLeft;
      this.prevTierButton.ImageOver = (Image) GFXLibrary.contestArrowLeft;
      this.prevTierButton.ImageClick = (Image) GFXLibrary.contestArrowLeft;
      this.prevTierButton.setSizeToImage();
      this.prevTierButton.Position = new Point(this.tierBar.X - this.prevTierButton.Width - 2, this.tierBar.Y + this.tierBar.Height / 2 - this.prevTierButton.Height / 2);
      this.prevTierButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ViewPrevTier), "Contest_history_prev_tier");
      control.addControl((CustomSelfDrawPanel.CSDControl) this.prevTierButton);
      this.nextTierButton.ImageNorm = (Image) GFXLibrary.contestArrowRight;
      this.nextTierButton.ImageOver = (Image) GFXLibrary.contestArrowRight;
      this.nextTierButton.ImageClick = (Image) GFXLibrary.contestArrowRight;
      this.nextTierButton.setSizeToImage();
      this.nextTierButton.Position = new Point(this.tierBar.Rectangle.Right + 2, this.tierBar.Y + this.tierBar.Height / 2 - this.nextTierButton.Height / 2);
      this.nextTierButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ViewNextTier), "Contest_history_next_tier");
      control.addControl((CustomSelfDrawPanel.CSDControl) this.nextTierButton);
      this.prevContestButton.ImageNorm = (Image) GFXLibrary.contestArrowLeft;
      this.prevContestButton.ImageOver = (Image) GFXLibrary.contestArrowLeft;
      this.prevContestButton.ImageClick = (Image) GFXLibrary.contestArrowLeft;
      this.prevContestButton.setSizeToImage();
      this.prevContestButton.Position = new Point(this.leaderboardInset.X - this.prevContestButton.Width - 2, this.titleLabel.Y);
      this.prevContestButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ViewPrevContest), "Contest_history_prev_contest");
      control.addControl((CustomSelfDrawPanel.CSDControl) this.prevContestButton);
      this.nextContestButton.ImageNorm = (Image) GFXLibrary.contestArrowRight;
      this.nextContestButton.ImageOver = (Image) GFXLibrary.contestArrowRight;
      this.nextContestButton.ImageClick = (Image) GFXLibrary.contestArrowRight;
      this.nextContestButton.setSizeToImage();
      this.nextContestButton.Position = new Point(this.leaderboardInset.Rectangle.Right, this.titleLabel.Y);
      this.nextContestButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ViewNextContest), "Contest_history_next_contest");
      control.addControl((CustomSelfDrawPanel.CSDControl) this.nextContestButton);
      this.leftSupport.Image = (Image) GFXLibrary.contestSupportHorse;
      this.leftSupport.setSizeToImage();
      this.leftSupport.Position = new Point(this.leaderboardInset.X - this.leftSupport.Width, this.leaderboardInset.Y + this.leaderboardInset.Height / 2 - this.leftSupport.Height / 2 + 50);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.leftSupport);
      this.rightSupport.Image = (Image) GFXLibrary.contestSupportLion;
      this.rightSupport.setSizeToImage();
      this.rightSupport.Position = new Point(this.leaderboardInset.Rectangle.Right, this.leaderboardInset.Y + this.leaderboardInset.Height / 2 - this.rightSupport.Height / 2 + 50);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.rightSupport);
      this.leftTrumpet.Image = (Image) GFXLibrary.contestTrumpetLeft;
      this.leftTrumpet.setSizeToImage();
      this.leftTrumpet.Position = new Point(10, 10);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.leftTrumpet);
      this.rightTrumpet.Image = (Image) GFXLibrary.contestTrumpetRight;
      this.rightTrumpet.setSizeToImage();
      this.rightTrumpet.Position = new Point(this.Width - this.rightTrumpet.Width - 10, 10);
      control.addControl((CustomSelfDrawPanel.CSDControl) this.rightTrumpet);
      if (GameEngine.Instance.World.contestID > 0)
      {
        DateTime dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestStartTime);
        DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestEndTime);
        if (dateTime1 <= VillageMap.getCurrentServerTime() && dateTime2 > VillageMap.getCurrentServerTime())
        {
          this.activeButton = new CustomSelfDrawPanel.CSDButton();
          this.activeButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
          this.activeButton.ImageOver = (Image) GFXLibrary.button_132_over;
          this.activeButton.ImageClick = (Image) GFXLibrary.button_132_in;
          this.activeButton.setSizeToImage();
          this.activeButton.Text.Text = SK.Text("Tourneys_Active", "Active Tourney");
          this.activeButton.Position = new Point(this.Width - this.activeButton.Width - 30, this.Height - this.activeButton.Height - 30);
          this.activeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showActiveContest));
          control.addControl((CustomSelfDrawPanel.CSDControl) this.activeButton);
        }
      }
      if (!resized && this.contestData.Count == 0)
      {
        this.initialised = false;
        this.contestData = new List<ContestCachedData>();
        this.RetrieveIDList();
        this.nextTierButton.Enabled = false;
        this.prevTierButton.Enabled = false;
        this.nextContestButton.Enabled = false;
        this.prevContestButton.Enabled = false;
        this.prevContestButton.Visible = false;
        this.nextContestButton.Visible = false;
        this.prevTierButton.Visible = false;
        this.nextTierButton.Visible = false;
        this.tierBar.Visible = false;
        this.playerRankHeaderLabel.Visible = false;
      }
      else
      {
        if (this.visibleContest == null || this.visibleContestIndex <= -1)
          return;
        this.visibleContest.visibleLineCount = this.potentialLineCount;
        this.titleLabel.Text = this.visibleContest.name;
        this.PopulateLeaderboard(true);
        this.UpdatePlayerInfo(true);
      }
    }

    public void update()
    {
    }

    private void showActiveContest() => InterfaceMgr.Instance.getMainTabBar().selectDummyTab(30);

    private void LeaderboardScrollUp()
    {
      if (this.visibleContest == null)
        return;
      this.visibleContest.ScrollUp();
    }

    private void LeaderboardScrollTop()
    {
      if (this.visibleContest == null)
        return;
      this.visibleContest.ScrollToTop();
    }

    private void LeaderboardScrollDown()
    {
      if (this.visibleContest == null)
        return;
      this.visibleContest.ScrollDown();
    }

    private void LeaderboardScrollBottom()
    {
      if (this.visibleContest == null)
        return;
      this.visibleContest.ScrollToBottom();
    }

    private void ViewNextTier()
    {
      if (this.visibleContestIndex < 0)
        return;
      this.DisableButtons();
      this.visibleContest.NextTier();
    }

    private void ViewPrevTier()
    {
      if (this.visibleContestIndex < 0)
        return;
      this.DisableButtons();
      this.visibleContest.PrevTier();
    }

    private void ViewNextContest()
    {
      if (this.visibleContestIndex < 0 || this.visibleContestIndex <= 0)
        return;
      --this.visibleContestIndex;
      if (this.visibleContest == null)
        return;
      this.DisableButtons();
      this.visibleContest.SetAsVisible();
    }

    private void ViewPrevContest()
    {
      if (this.visibleContestIndex < 0 || this.visibleContestIndex >= this.contestIDs.Length - 1)
        return;
      ++this.visibleContestIndex;
      if (this.visibleContest == null)
        return;
      this.DisableButtons();
      this.visibleContest.SetAsVisible();
    }

    private void DisableButtons()
    {
      this.nextTierButton.Enabled = false;
      this.prevTierButton.Enabled = false;
      this.nextContestButton.Enabled = false;
      this.prevContestButton.Enabled = false;
    }

    private void UpdateButtons()
    {
      this.nextTierButton.Enabled = this.visibleContest.visibleTier < 3;
      this.prevTierButton.Enabled = this.visibleContest.visibleTier > 1;
      this.nextContestButton.Enabled = this.visibleContestIndex > 0 && this.contestIDs[this.visibleContestIndex - 1] > 0;
      this.prevContestButton.Enabled = this.visibleContestIndex < this.contestIDs.Length - 1 && this.contestIDs[this.visibleContestIndex + 1] > 0;
    }

    private ContestCachedData visibleContest
    {
      get
      {
        return this.contestData.Count == 0 ? (ContestCachedData) null : this.contestData[this.visibleContestIndex];
      }
    }

    private int potentialLineCount
    {
      get
      {
        return (int) Math.Floor((double) this.leaderboardInset.Height / (double) (GFXLibrary.lineitem_strip_02_dark.Height * 5 / 4));
      }
    }

    private int actualLineCount
    {
      get => Math.Min(this.potentialLineCount, this.visibleContest.activeLeaderboard.Length);
    }

    private void RetrieveIDList()
    {
      this.contestData.Clear();
      RemoteServices.Instance.set_GetContestHistoryIDs_UserCallBack(new RemoteServices.GetContestHistoryIDs_UserCallBack(this.RetrieveIDListCallback));
      RemoteServices.Instance.GetContestHistoryIDs();
    }

    private void RetrieveIDListCallback(GetContestHistoryIDs_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.initialised = true;
      this.contestIDs = returnData.contestIDs;
      if (this.contestIDs.Length <= 0)
        return;
      for (int index = 0; index < this.contestIDs.Length; ++index)
      {
        if (this.contestIDs[index] != 0)
          this.contestData.Add(new ContestCachedData()
          {
            leaderboardCallback = new ContestCachedData.ContestCacheCallbackDelegate(this.PopulateLeaderboard),
            metaDataCallback = new ContestCachedData.ContestCacheCallbackDelegate(this.UpdateContestName),
            userDataCallback = new ContestCachedData.ContestCacheCallbackDelegate(this.UpdatePlayerInfo),
            ID = this.contestIDs[index],
            visibleLineCount = this.potentialLineCount
          });
      }
      if (this.contestData.Count > 0)
      {
        this.visibleContestIndex = 0;
        this.visibleContest.SetAsVisible();
        this.prevContestButton.Visible = true;
        this.nextContestButton.Visible = true;
        this.prevTierButton.Visible = true;
        this.nextTierButton.Visible = true;
        this.tierBar.Visible = true;
        this.playerRankHeaderLabel.Visible = true;
        this.UpdateButtons();
      }
      else
      {
        this.prevContestButton.Visible = false;
        this.nextContestButton.Visible = false;
        this.prevTierButton.Visible = false;
        this.nextTierButton.Visible = false;
        this.tierBar.Visible = false;
        this.playerRankHeaderLabel.Visible = false;
      }
    }

    private void PopulateLeaderboard(bool success)
    {
      if (!this.initialised)
        return;
      this.UpdateButtons();
      if (!success)
        return;
      this.ClearLeaderboard();
      int num = (int) Math.Floor((double) (this.leaderboardInset.Height - GFXLibrary.lineitem_strip_02_dark.Height * 4 / 3 * this.potentialLineCount) / (double) (this.potentialLineCount + 1));
      int y = num;
      for (int index = 0; index < this.actualLineCount; ++index)
      {
        ContestRankLine control = new ContestRankLine();
        control.init(this.visibleContest.activeLeaderboard[index], (CustomSelfDrawPanel.CSDControl) this.leaderboardInset, index % 2 == 1);
        control.Position = new Point(0, y);
        this.leaderboardInset.addControl((CustomSelfDrawPanel.CSDControl) control);
        y += control.Height + num;
      }
      this.leaderboardInset.invalidate();
      switch (this.visibleContest.visibleTier)
      {
        case 1:
          this.tierLabel.Text = SK.Text("TOUCH_Z_Commoners", "Commoners");
          break;
        case 2:
          this.tierLabel.Text = SK.Text("TOUCH_Z_Gentry", "Gentry");
          break;
        case 3:
          this.tierLabel.Text = SK.Text("TOUCH_Z_Nobility", "Nobility");
          break;
      }
      this.tierLabel.CustomTooltipID = 25003;
      this.tierLabel.CustomTooltipData = this.visibleContest.visibleTier;
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

    private void UpdatePlayerInfo(bool success)
    {
      if (!this.initialised)
        return;
      if (success && this.visibleContest.userRankBand > 0)
      {
        this.playerRankHeaderLabel.Text = SK.Text("Event_Final_Position", "Your final position") + ": ";
        this.playerRankValueLabel.Text = this.visibleContest.userPosition.ToString() + " (";
        switch (this.visibleContest.userRankBand)
        {
          case 1:
            this.playerRankValueLabel.Text += SK.Text("TOUCH_Z_Commoners", "Commoners");
            break;
          case 2:
            this.playerRankValueLabel.Text += SK.Text("TOUCH_Z_Gentry", "Gentry");
            break;
          case 3:
            this.playerRankValueLabel.Text += SK.Text("TOUCH_Z_Nobility", "Nobility");
            break;
        }
        this.playerRankValueLabel.Text += ")";
        this.playerRankValueLabel.CustomTooltipID = 25003;
        this.playerRankValueLabel.CustomTooltipData = this.visibleContest.userRankBand;
      }
      else
      {
        this.playerRankHeaderLabel.Text = "";
        this.playerRankValueLabel.Text = "";
      }
    }

    private void UpdateContestName(bool success)
    {
      if (!this.initialised)
        return;
      this.titleLabel.Text = this.visibleContest.name;
      this.UpdateButtons();
    }

    public void logout()
    {
      this.contestData.Clear();
      this.contestIDs = (int[]) null;
    }
  }
}
