// Decompiled with JetBrains decompiler
// Type: Kingdoms.CountryVotePanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CountryVotePanel : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private Panel focusPanel;
    public static CountryVotePanel instance;
    private SparseArray countryList = new SparseArray();
    private int voteCap = 100000;
    private List<ParishMember> countryMembers = new List<ParishMember>();
    private int currentCountry = -1;
    private DateTime nextElectionTime = DateTime.MinValue;
    private DateTime lastProclamationTime = DateTime.MinValue;
    private int electedLeaderID = -1;
    private string electedLeaderName = "";
    private int currentLeaderID = -1;
    private bool currentLeaderMale;
    private string currentLeaderName = "";
    private bool votingAllowed;
    private int m_userIDOnCurrent = -1;
    private int m_currentVillage = -1;
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage illustrationImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel stewardLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel votesAvailableLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel votesAvailableLabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel voteCapLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel voteCapLabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel voteLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel eligibleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel FactionsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel votesReceivedLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDExtendingPanel wallInfoImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton proclamationButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel proclamationLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private List<WallInfo> wallList = new List<WallInfo>();
    private List<CountryVotePanel.VoteLine> lineList = new List<CountryVotePanel.VoteLine>();
    private CountryVotePanel.ParishMemberComparer parishMemberComparer = new CountryVotePanel.ParishMemberComparer();

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
      this.focusPanel = new Panel();
      this.SuspendLayout();
      this.focusPanel.BackColor = ARGBColors.Transparent;
      this.focusPanel.ForeColor = ARGBColors.Transparent;
      this.focusPanel.Location = new Point(988, 3);
      this.focusPanel.Name = "focusPanel";
      this.focusPanel.Size = new Size(1, 1);
      this.focusPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.focusPanel);
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (CountryVotePanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public CountryVotePanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.focusPanel.Focus();
    }

    public void init(bool resized)
    {
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      this.m_currentVillage = selectedMenuVillage;
      int countryFromVillageId = GameEngine.Instance.World.getCountryFromVillageID(selectedMenuVillage);
      int height = this.Height;
      CountryVotePanel.instance = this;
      this.clearControls();
      this.headerImage.Size = new Size(this.Width, 40);
      this.headerImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage);
      this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
      this.backgroundImage.Size = new Size(this.Width, height - 40);
      this.backgroundImage.Position = new Point(0, 40);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.backgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.backgroundImage, 15, new Point(this.Width - 44, 3));
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23, 28);
      this.headerLabelsImage.Position = new Point(25, 129);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider1Image.Position = new Point(95, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider1Image);
      this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider2Image.Position = new Point(366, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
      this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider3Image.Position = new Point(627, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
      this.parishNameLabel.Text = GameEngine.Instance.World.getVillageName(this.m_currentVillage) + " (" + GameEngine.Instance.World.getCountryName(countryFromVillageId) + ")";
      this.parishNameLabel.Color = ARGBColors.White;
      this.parishNameLabel.DropShadowColor = ARGBColors.Black;
      this.parishNameLabel.Position = new Point(20, 0);
      this.parishNameLabel.Size = new Size(this.Width - 40, 40);
      this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.parishNameLabel);
      this.illustrationImage.Image = (Image) GFXLibrary.parishwall_village_illlustration_04;
      this.illustrationImage.Position = new Point(17, 5);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.illustrationImage);
      this.stewardLabel.Text = "";
      this.stewardLabel.Color = ARGBColors.Black;
      this.stewardLabel.Position = new Point(5, 5);
      this.stewardLabel.Size = new Size(this.illustrationImage.Width - 6, 30);
      this.stewardLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.stewardLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.illustrationImage.addControl((CustomSelfDrawPanel.CSDControl) this.stewardLabel);
      this.proclamationButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.proclamationButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.proclamationButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.proclamationButton.Position = new Point(this.Width - 220, 7);
      this.proclamationButton.Text.Text = SK.Text("Capitials_Proclamation", "Send Proclamation");
      this.proclamationButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.proclamationButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.proclamationButton.TextYOffset = -3;
      this.proclamationButton.Text.Color = ARGBColors.Black;
      this.proclamationButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendProclamation));
      this.proclamationButton.CustomTooltipID = 4203;
      this.proclamationButton.Visible = false;
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.proclamationButton);
      this.proclamationLabel.Text = "";
      this.proclamationLabel.Color = ARGBColors.White;
      this.proclamationLabel.DropShadowColor = ARGBColors.Black;
      this.proclamationLabel.Position = new Point(20, 0);
      this.proclamationLabel.Size = new Size(this.Width - 40 - 220, 40);
      this.proclamationLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.proclamationLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.proclamationLabel.Visible = false;
      this.headerImage.addControl((CustomSelfDrawPanel.CSDControl) this.proclamationLabel);
      this.wallInfoImage.Size = new Size(440, 85);
      this.wallInfoImage.Position = new Point(460, 20);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallInfoImage);
      this.wallInfoImage.Create((Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_right);
      this.wallScrollArea.Position = new Point(25, 158);
      this.wallScrollArea.Size = new Size(915, height - 212);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, height - 212));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      int num = this.wallScrollBar.Value;
      this.wallScrollBar.Position = new Point(943, 158);
      this.wallScrollBar.Size = new Size(24, height - 212);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.votesAvailableLabel.Text = SK.Text("GENERIC_Votes_Available", "Votes Available") + " :";
      this.votesAvailableLabel.Color = ARGBColors.Black;
      this.votesAvailableLabel.Position = new Point(31, 27);
      this.votesAvailableLabel.Size = new Size(300, 40);
      this.votesAvailableLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.votesAvailableLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.wallInfoImage.addControl((CustomSelfDrawPanel.CSDControl) this.votesAvailableLabel);
      this.votesAvailableLabelValue.Text = "0";
      this.votesAvailableLabelValue.Color = ARGBColors.Black;
      this.votesAvailableLabelValue.Position = new Point(307, 27);
      this.votesAvailableLabelValue.Size = new Size(100, 40);
      this.votesAvailableLabelValue.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.votesAvailableLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.votesAvailableLabelValue.Visible = true;
      this.wallInfoImage.addControl((CustomSelfDrawPanel.CSDControl) this.votesAvailableLabelValue);
      this.voteCapLabel.Visible = false;
      if (GameEngine.Instance.World.SecondAgeWorld || GameEngine.Instance.LocalWorldData.EraWorld || GameEngine.Instance.LocalWorldData.AIWorld)
      {
        this.votesAvailableLabel.Position = new Point(31, 12);
        this.votesAvailableLabelValue.Position = new Point(307, 12);
        this.voteCapLabel.Text = SK.Text("ParishPanel_Current_Vote_cap", "Current Vote Cap") + " :";
        this.voteCapLabel.Color = ARGBColors.Black;
        this.voteCapLabel.Position = new Point(31, 42);
        this.voteCapLabel.Size = new Size(300, 40);
        this.voteCapLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
        this.voteCapLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.voteCapLabel.Visible = true;
        this.wallInfoImage.addControl((CustomSelfDrawPanel.CSDControl) this.voteCapLabel);
        this.voteCapLabelValue.Text = "0";
        this.voteCapLabelValue.Color = ARGBColors.Black;
        this.voteCapLabelValue.Position = new Point(307, 42);
        this.voteCapLabelValue.Size = new Size(100, 40);
        this.voteCapLabelValue.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
        this.voteCapLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
        this.voteCapLabelValue.Visible = true;
        this.wallInfoImage.addControl((CustomSelfDrawPanel.CSDControl) this.voteCapLabelValue);
      }
      this.voteLabel.Text = SK.Text("GENERIC_Vote", "Vote");
      this.voteLabel.Color = ARGBColors.Black;
      this.voteLabel.Position = new Point(15, -2);
      this.voteLabel.Size = new Size(81, this.headerLabelsImage.Height);
      this.voteLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.voteLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.voteLabel);
      this.eligibleLabel.Text = SK.Text("GENERIC_Eligible_Candidates", "Eligible Candidates");
      this.eligibleLabel.Color = ARGBColors.Black;
      this.eligibleLabel.Position = new Point(106, -2);
      this.eligibleLabel.Size = new Size(250, this.headerLabelsImage.Height);
      this.eligibleLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.eligibleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.eligibleLabel);
      this.FactionsLabel.Text = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction");
      this.FactionsLabel.Color = ARGBColors.Black;
      this.FactionsLabel.Position = new Point(376, -2);
      this.FactionsLabel.Size = new Size(247, this.headerLabelsImage.Height);
      this.FactionsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.FactionsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.FactionsLabel);
      this.votesReceivedLabel.Text = SK.Text("GENERIC_Votes_Received", "Votes Received");
      this.votesReceivedLabel.Color = ARGBColors.Black;
      this.votesReceivedLabel.Position = new Point(635, -2);
      this.votesReceivedLabel.Size = new Size(300, this.headerLabelsImage.Height);
      this.votesReceivedLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.votesReceivedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.votesReceivedLabel);
      if (!resized)
      {
        CountryVotePanel.StoredCountryInfo country = (CountryVotePanel.StoredCountryInfo) this.countryList[countryFromVillageId];
        bool flag = false;
        if (country == null || (DateTime.Now - country.m_lastUpdateTime).TotalMinutes > 2.0 || country.lastReturnData == null)
          flag = true;
        this.m_currentVillage = selectedMenuVillage;
        if (this.currentCountry != countryFromVillageId)
        {
          this.countryMembers.Clear();
          this.currentLeaderID = -1;
          this.electedLeaderID = -1;
          this.currentLeaderName = "";
          this.electedLeaderName = "";
          this.m_userIDOnCurrent = -1;
        }
        this.currentCountry = countryFromVillageId;
        if (flag)
        {
          RemoteServices.Instance.set_GetCountryElectionInfo_UserCallBack(new RemoteServices.GetCountryElectionInfo_UserCallBack(this.getCountryElectionInfoCallback));
          RemoteServices.Instance.GetCountryElectionInfo(this.m_currentVillage);
        }
        this.nextElectionTime = DateTime.MinValue;
        this.votingAllowed = false;
        this.addPlayers();
        if (flag)
          return;
        this.getCountryElectionInfoCallback(country.lastReturnData);
      }
      else
        this.addPlayers();
    }

    public void update()
    {
      if (!this.proclamationLabel.Visible)
        return;
      TimeSpan timeSpan = VillageMap.getCurrentServerTime() - this.lastProclamationTime;
      if (timeSpan.TotalDays >= 3.0)
      {
        this.proclamationLabel.Visible = false;
        this.proclamationButton.Enabled = true;
      }
      else
        this.proclamationLabel.Text = SK.Text("Proclamations_time_to_go", "Time before next Proclamation : ") + VillageMap.createBuildTimeString(259200 - (int) timeSpan.TotalSeconds);
    }

    public void logout()
    {
    }

    private void createParishWall(WallInfo[] wallInfos)
    {
    }

    public void updateWallArea()
    {
    }

    private void wallScrollBarMoved()
    {
      int y = this.wallScrollBar.Value;
      this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 158 - y);
      this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    public void getCountryElectionInfoCallback(GetCountryElectionInfo_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      CountryVotePanel.StoredCountryInfo storedCountryInfo = (CountryVotePanel.StoredCountryInfo) this.countryList[returnData.countryID];
      if (storedCountryInfo == null)
      {
        storedCountryInfo = new CountryVotePanel.StoredCountryInfo();
        this.countryList[returnData.countryID] = (object) storedCountryInfo;
      }
      storedCountryInfo.m_lastUpdateTime = DateTime.Now;
      storedCountryInfo.lastReturnData = returnData;
      if (this.currentCountry != returnData.countryID)
        return;
      this.votingAllowed = returnData.votingAllowed;
      if (this.countryMembers == null)
        this.countryMembers = new List<ParishMember>();
      else
        this.countryMembers.Clear();
      if (returnData.countryMembers != null)
      {
        this.countryMembers.AddRange((IEnumerable<ParishMember>) returnData.countryMembers);
        int num = 0;
        foreach (ParishMember countryMember in this.countryMembers)
        {
          if (countryMember.userID == RemoteServices.Instance.UserID)
          {
            num = countryMember.numSpareVotes;
            break;
          }
        }
        this.votesAvailableLabelValue.Text = num.ToString();
      }
      else
      {
        this.votesAvailableLabel.Text = SK.Text("CountryPanel_All_Provinces_Needed", "All Provinces need to be active before an election is held");
        this.votesAvailableLabel.Position = new Point(31, 12);
        this.votesAvailableLabel.Size = new Size(400, 100);
        this.votesAvailableLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
        this.votesAvailableLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.votesAvailableLabelValue.Visible = false;
        this.wallInfoImage.invalidate();
      }
      this.m_userIDOnCurrent = -1;
      this.electedLeaderID = returnData.leaderID;
      this.electedLeaderName = returnData.leaderName;
      this.lastProclamationTime = returnData.lastProclamation;
      this.currentLeaderID = returnData.leaderID;
      this.currentLeaderName = returnData.leaderName;
      this.currentLeaderMale = returnData.leaderMale;
      this.voteCap = returnData.voteCap;
      if (GameEngine.Instance.LocalWorldData.AIWorld && this.voteCap < 100000 && !this.voteCapLabel.Visible)
      {
        this.votesAvailableLabel.Position = new Point(31, 12);
        this.votesAvailableLabelValue.Position = new Point(307, 12);
        this.voteCapLabel.Text = SK.Text("ParishPanel_Current_Vote_cap", "Current Vote Cap") + " :";
        this.voteCapLabel.Color = ARGBColors.Black;
        this.voteCapLabel.Position = new Point(31, 42);
        this.voteCapLabel.Size = new Size(300, 40);
        this.voteCapLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
        this.voteCapLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.voteCapLabel.Visible = true;
        this.wallInfoImage.addControl((CustomSelfDrawPanel.CSDControl) this.voteCapLabel);
        this.voteCapLabelValue.Text = "0";
        this.voteCapLabelValue.Color = ARGBColors.Black;
        this.voteCapLabelValue.Position = new Point(307, 42);
        this.voteCapLabelValue.Size = new Size(100, 40);
        this.voteCapLabelValue.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
        this.voteCapLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
        this.voteCapLabelValue.Visible = true;
        this.wallInfoImage.addControl((CustomSelfDrawPanel.CSDControl) this.voteCapLabelValue);
        this.wallInfoImage.invalidate();
      }
      this.voteCapLabelValue.Text = returnData.voteCap.ToString();
      this.addPlayers();
    }

    public void addPlayers()
    {
      this.wallScrollArea.clearControls();
      int num = 0;
      this.lineList.Clear();
      int yourVotes = 0;
      if (this.countryMembers != null)
      {
        foreach (ParishMember countryMember in this.countryMembers)
        {
          if (countryMember.userID == RemoteServices.Instance.UserID)
          {
            yourVotes = countryMember.numSpareVotes;
            break;
          }
        }
        this.countryMembers.Sort((IComparer<ParishMember>) this.parishMemberComparer);
        int position = 0;
        foreach (ParishMember countryMember in this.countryMembers)
        {
          CountryVotePanel.VoteLine control = new CountryVotePanel.VoteLine();
          if (num != 0)
            num += 5;
          control.Position = new Point(0, num);
          int numReceivedVotes = countryMember.numVotesReceived;
          if (numReceivedVotes > this.voteCap)
            numReceivedVotes = this.voteCap;
          control.init(countryMember.userName, countryMember.userID, countryMember.rank, countryMember.points, this.votingAllowed, countryMember.numSpareVotes, numReceivedVotes, countryMember.areaID, countryMember.factionID, yourVotes, position, this);
          this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          num += control.Height;
          this.lineList.Add(control);
          ++position;
        }
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
      this.stewardLabel.Text = !this.currentLeaderMale ? SK.Text("ParishWallPanel_Queen", "Queen") + " : " + this.currentLeaderName : SK.Text("ParishWallPanel_King", "King") + " : " + this.currentLeaderName;
      this.m_userIDOnCurrent = this.currentLeaderID;
      TimeSpan timeSpan = VillageMap.getCurrentServerTime() - this.lastProclamationTime;
      if (this.currentLeaderID == RemoteServices.Instance.UserID)
      {
        this.proclamationButton.Visible = true;
        if (timeSpan.TotalDays >= 3.0)
        {
          this.proclamationButton.Enabled = true;
          this.proclamationLabel.Visible = false;
        }
        else
        {
          this.proclamationButton.Enabled = false;
          this.proclamationLabel.Visible = true;
        }
      }
      else
      {
        this.proclamationButton.Visible = false;
        this.proclamationLabel.Visible = false;
      }
      this.update();
    }

    public void voteChanged(int userID)
    {
      RemoteServices.Instance.set_MakeCountryVote_UserCallBack(new RemoteServices.MakeCountryVote_UserCallBack(this.makeCountryVoteCallback));
      RemoteServices.Instance.MakeCountryVote(this.m_currentVillage, userID);
    }

    private void makeCountryVoteCallback(MakeCountryVote_ReturnType returnData)
    {
      if (!returnData.Success || returnData.returnData == null)
        return;
      this.getCountryElectionInfoCallback(returnData.returnData);
      GameEngine.Instance.forceFullTick();
    }

    private void sendProclamation()
    {
      CountryVotePanel.StoredCountryInfo country = (CountryVotePanel.StoredCountryInfo) this.countryList[this.currentCountry];
      if (country != null)
        country.m_lastUpdateTime = DateTime.MinValue;
      GameEngine.Instance.playInterfaceSound("UserInfoScreen_send_mail");
      InterfaceMgr.Instance.getMainTabBar().selectDummyTabFast(21);
      InterfaceMgr.Instance.sendProclamation(7, GameEngine.Instance.World.getCountryFromVillageID(this.m_currentVillage));
    }

    public class StoredCountryInfo
    {
      public GetCountryElectionInfo_ReturnType lastReturnData;
      public DateTime m_lastUpdateTime = DateTime.MinValue;
    }

    public class ParishMemberComparer : IComparer<ParishMember>
    {
      public int Compare(ParishMember x, ParishMember y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null || x.numVotesReceived < y.numVotesReceived)
          return 1;
        return x.numVotesReceived > y.numVotesReceived ? -1 : x.userName.CompareTo(y.userName);
      }
    }

    public class VoteLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel personName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel votesLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDButton voteButton = new CustomSelfDrawPanel.CSDButton();
      private CustomSelfDrawPanel.CSDLabel factionName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
      private int m_position = -1000;
      private int m_userID = -1;
      private int m_factionID = -1;
      private CountryVotePanel m_parent;

      public void init(
        string playerName,
        int userID,
        int rank,
        int points,
        bool votingAllowed,
        int numSpareVotes,
        int numReceivedVotes,
        int parishID,
        int factionID,
        int yourVotes,
        int position,
        CountryVotePanel parent)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_userID = userID;
        this.m_factionID = factionID;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.voteButton.ImageNorm = (Image) GFXLibrary.parishwall_button_vote_checked_normal;
        this.voteButton.ImageOver = (Image) GFXLibrary.parishwall_button_vote_checked_over;
        this.voteButton.Position = new Point(8, 4);
        this.voteButton.Text.Text = SK.Text("GENERIC_Vote", "Vote");
        this.voteButton.Text.Color = ARGBColors.Black;
        this.voteButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.voteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked), "CountryVotePanel_vote");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.voteButton);
        if (yourVotes > 0 && !GameEngine.Instance.World.WorldEnded)
          this.voteButton.Enabled = true;
        else
          this.voteButton.Enabled = false;
        NumberFormatInfo nfi = GameEngine.NFI;
        int num = 0;
        if (factionID >= 0)
        {
          FactionData faction = GameEngine.Instance.World.getFaction(factionID);
          if (faction != null)
          {
            this.factionName.Text = faction.factionNameAbrv;
            int houseId = faction.houseID;
            if (houseId > 0)
            {
              this.houseImage.Image = (Image) GFXLibrary.getHouseFlagSmall(houseId);
              this.houseImage.Position = new Point(377, 5);
              this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick));
              this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseImage);
              num = 32;
            }
          }
          else
            this.factionName.Text = "";
        }
        else
          this.factionName.Text = "";
        this.factionName.Color = ARGBColors.Black;
        this.factionName.Position = new Point(377 + num, 0);
        this.factionName.Size = new Size(210, this.backgroundImage.Height);
        this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        if (factionID >= 0)
          this.factionName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionName);
        this.personName.Text = playerName;
        this.personName.Color = ARGBColors.Black;
        this.personName.Position = new Point(136, 0);
        this.personName.Size = new Size(225, this.backgroundImage.Height);
        this.personName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.personName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.personName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerClick), "CountryVotePanel_user_clicked");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.personName);
        this.votesLabel.Text = numReceivedVotes.ToString("N", (IFormatProvider) nfi);
        this.votesLabel.Color = ARGBColors.Black;
        this.votesLabel.Position = new Point(635, 0);
        this.votesLabel.Size = new Size(150, this.backgroundImage.Height);
        this.votesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.votesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.votesLabel);
        this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(userID, 25, 28);
        if (this.shieldImage.Image != null)
        {
          this.shieldImage.Position = new Point(106, 1);
          this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerClick));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
        }
        this.invalidate();
      }

      public void update()
      {
      }

      public void lineClicked()
      {
        if (this.m_parent == null)
          return;
        this.voteButton.Enabled = false;
        this.m_parent.voteChanged(this.m_userID);
      }

      private void playerClick()
      {
        if (this.m_userID < 0)
          return;
        InterfaceMgr.Instance.showUserInfoScreen(new WorldMap.CachedUserInfo()
        {
          userID = this.m_userID
        });
      }

      private void factionClick()
      {
        if (this.m_factionID < 0)
          return;
        GameEngine.Instance.playInterfaceSound("CountryVotePanel_faction");
        InterfaceMgr.Instance.showFactionPanel(this.m_factionID);
      }
    }
  }
}
