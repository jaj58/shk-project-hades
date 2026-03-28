// Decompiled with JetBrains decompiler
// Type: Kingdoms.StatsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class StatsPanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int MIN_TEXT_LENGTH_FOR_SEARCH = 4;
    private DockableControl dockableControl;
    private IContainer components;
    private TextBox searchInput;
    private Panel focusPanel;
    public static int NUM_VISIBLE_LINES = 9;
    private bool initialTextInTextbox = true;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel categoryLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel categoryDescription = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage mainInsetTopTopImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mainInsetTopMiddleImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mainInsetTopBottomImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mainInsetMidImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mainInsetBottomImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDVertExtendingPanel secondInsetImage = new CustomSelfDrawPanel.CSDVertExtendingPanel();
    private CustomSelfDrawPanel.CSDImage searchInsetImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton searchButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel fixedIconBar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel scrollIconBar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDButton fixedButton1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton fixedButton2 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton fixedButton3 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton fixedButton4 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton fixedButton5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton fixedButton7 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage fixedBarImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton variButton1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButton2 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButton3 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButton4 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButton5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButton6 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButton7 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButton8 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButton9 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButton10 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButtonLeft = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton variButtonRight = new CustomSelfDrawPanel.CSDButton();
    private StatsPanel.StatsEntry mainEntry1 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry2 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry3 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry4 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry5 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry6 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry7 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry8 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry9 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry10 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry11 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry12 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry13 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry14 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry15 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry16 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry17 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry18 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry19 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry mainEntry20 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry topEntry1 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry topEntry2 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry topEntry3 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry topEntry4 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry topEntry5 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry topEntry6 = new StatsPanel.StatsEntry();
    private StatsPanel.StatsEntry topEntry7 = new StatsPanel.StatsEntry();
    private StatsPanel.SelfStatsEntry selfEntry1 = new StatsPanel.SelfStatsEntry();
    private StatsPanel.SelfStatsEntry selfEntry2 = new StatsPanel.SelfStatsEntry();
    private StatsPanel.SelfStatsEntry selfEntry3 = new StatsPanel.SelfStatsEntry();
    private StatsPanel.SelfStatsEntry selfEntry4 = new StatsPanel.SelfStatsEntry();
    private StatsPanel.SelfStatsEntry selfEntry5 = new StatsPanel.SelfStatsEntry();
    private StatsPanel.SelfStatsEntry selfEntry6 = new StatsPanel.SelfStatsEntry();
    private StatsPanel.SelfStatsEntry selfEntry7 = new StatsPanel.SelfStatsEntry();
    private CustomSelfDrawPanel.CSDButton topButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton upButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton downButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton bottomButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton clearSearchButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel updateLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel bestRankingsLabel = new CustomSelfDrawPanel.CSDLabel();
    private int categoryScrollPos;
    private int currentCategory = -1;
    private int currentUserLine = -10000;
    private int numExtraTopLines;
    private int numVariCats = 8;
    private bool inSearchResults;
    private int searchLocation;
    private LeaderBoardSearchResults m_results = new LeaderBoardSearchResults();

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
      this.searchInput = new TextBox();
      this.focusPanel = new Panel();
      this.SuspendLayout();
      this.searchInput.BackColor = Color.FromArgb(140, 153, 161);
      this.searchInput.BorderStyle = BorderStyle.None;
      this.searchInput.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.searchInput.Location = new Point(653, 15);
      this.searchInput.MaxLength = 40;
      this.searchInput.Multiline = true;
      this.searchInput.Name = "searchInput";
      this.searchInput.Size = new Size(200, 22);
      this.searchInput.TabIndex = 100;
      this.searchInput.Text = "Search";
      this.searchInput.WordWrap = false;
      this.searchInput.KeyPress += new KeyPressEventHandler(this.searchInput_KeyPress);
      this.searchInput.Enter += new EventHandler(this.searchInput_Enter);
      this.focusPanel.BackColor = ARGBColors.Transparent;
      this.focusPanel.ForeColor = ARGBColors.Transparent;
      this.focusPanel.Location = new Point(988, 3);
      this.focusPanel.Name = "focusPanel";
      this.focusPanel.Size = new Size(1, 1);
      this.focusPanel.TabIndex = 1;
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.focusPanel);
      this.Controls.Add((Control) this.searchInput);
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 594);
      this.Name = nameof (StatsPanel);
      this.Size = new Size(992, 594);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public StatsPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.searchInput.Font = FontManager.GetFont("Microsoft Sans Serif", 12f, FontStyle.Regular);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      this.clearControls();
      this.numVariCats = 8;
      if (this.currentCategory == 18)
      {
        this.currentCategory = -1;
        this.categoryScrollPos = 0;
      }
      this.focusPanel.Focus();
      if (!resized)
      {
        this.currentUserLine = -10000;
        this.initialTextInTextbox = true;
        this.searchInput.Text = SK.Text("Stats_Seaarch", "Search");
        this.inSearchResults = false;
      }
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.categoryLabel.Text = "[" + SK.Text("Stats_Category", "Category") + "]";
      this.categoryLabel.Color = ARGBColors.White;
      this.categoryLabel.DropShadowColor = ARGBColors.Black;
      this.categoryLabel.Position = new Point(35, 11);
      this.categoryLabel.Size = new Size(300, 35);
      this.categoryLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.categoryLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.categoryLabel);
      this.categoryDescription.Text = "[" + SK.Text("Stats_Description", "Description") + "]";
      this.categoryDescription.Color = ARGBColors.White;
      this.categoryDescription.DropShadowColor = ARGBColors.Black;
      if (Program.mySettings.LanguageIdent == "pl" || Program.mySettings.LanguageIdent == "it")
      {
        this.categoryDescription.Position = new Point(100, 3);
        this.categoryDescription.Size = new Size(360, 50);
        this.categoryDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      }
      else if (Program.mySettings.LanguageIdent == "pt")
      {
        this.categoryDescription.Position = new Point(100, 3);
        this.categoryDescription.Size = new Size(300, 50);
        this.categoryDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      }
      else
      {
        this.categoryDescription.Position = new Point(100, 18);
        this.categoryDescription.Size = new Size(500, 30);
        this.categoryDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      }
      this.categoryDescription.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.categoryDescription);
      int num1 = this.Height - 594;
      this.mainInsetTopTopImage.Image = (Image) GFXLibrary.int_statsscreen_maininset_top_top;
      this.mainInsetTopTopImage.Position = new Point(30, 103);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainInsetTopTopImage);
      this.mainInsetTopMiddleImage.Image = (Image) GFXLibrary.int_statsscreen_maininset_top_middle;
      this.mainInsetTopMiddleImage.Position = new Point(30, 143);
      this.mainInsetTopMiddleImage.Size = new Size(this.mainInsetTopMiddleImage.Image.Width, 90 + num1 / 2);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainInsetTopMiddleImage);
      this.mainInsetTopBottomImage.Image = (Image) GFXLibrary.int_statsscreen_maininset_top_bottom;
      this.mainInsetTopBottomImage.Position = new Point(30, 233 + num1 / 2);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainInsetTopBottomImage);
      this.mainInsetMidImage.Image = (Image) GFXLibrary.int_statsscreen_maininset_middle;
      this.mainInsetMidImage.Position = new Point(30, 308 + num1 / 2);
      this.mainInsetMidImage.Size = new Size(this.mainInsetMidImage.Image.Width, 222 + num1 / 2);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainInsetMidImage);
      this.mainInsetBottomImage.Image = (Image) GFXLibrary.int_statsscreen_maininset_bottom;
      this.mainInsetBottomImage.Position = new Point(30, 530 + num1 / 2 * 2);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainInsetBottomImage);
      this.searchInsetImage.Image = (Image) GFXLibrary.int_statsscreen_search_inset;
      this.searchInsetImage.Position = new Point(638, 9);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.searchInsetImage);
      this.searchButton.ImageNorm = (Image) GFXLibrary.int_statsscreen_search_button_normal;
      this.searchButton.ImageOver = (Image) GFXLibrary.int_statsscreen_search_button_over;
      this.searchButton.ImageClick = (Image) GFXLibrary.int_statsscreen_search_button_pushed;
      this.searchButton.Position = new Point(244, 4);
      this.searchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchClicked), "StatsPanel_search");
      this.searchInsetImage.addControl((CustomSelfDrawPanel.CSDControl) this.searchButton);
      this.clearSearchButton.ImageNorm = (Image) GFXLibrary.int_statsscreen_search_clear_button_normal;
      this.clearSearchButton.ImageOver = (Image) GFXLibrary.int_statsscreen_search_clear_button_over;
      this.clearSearchButton.ImageClick = (Image) GFXLibrary.int_statsscreen_search_clear_button_pushed;
      this.clearSearchButton.Position = new Point(241 - this.clearSearchButton.ImageNorm.Size.Width, 4);
      this.clearSearchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clearSearchClicked), "StatsPanel_clear_search");
      this.clearSearchButton.Visible = this.inSearchResults;
      this.searchInsetImage.addControl((CustomSelfDrawPanel.CSDControl) this.clearSearchButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 28, new Point(598, 8));
      this.fixedIconBar.Position = new Point(37, 57);
      this.fixedIconBar.Size = new Size(314, 1);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.fixedIconBar);
      this.fixedIconBar.Create((Image) GFXLibrary.int_statsscreen_iconbar_left, (Image) GFXLibrary.int_statsscreen_iconbar_middle, (Image) GFXLibrary.int_statsscreen_iconbar_right);
      this.scrollIconBar.Position = new Point(375, 57);
      this.scrollIconBar.Size = new Size(572, 1);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollIconBar);
      this.scrollIconBar.Create((Image) GFXLibrary.int_statsscreen_iconbar_left, (Image) GFXLibrary.int_statsscreen_iconbar_middle, (Image) GFXLibrary.int_statsscreen_iconbar_right);
      this.fixedButton1.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.fixedButton1.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.fixedButton1.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.fixedButton1.Position = new Point(-14, -7);
      this.fixedButton1.Data = -1;
      this.fixedButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.fixedButton1.CustomTooltipID = 1300;
      this.fixedButton1.CustomTooltipData = -1;
      this.fixedIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.fixedButton1);
      this.fixedButton2.ImageNorm = (Image) GFXLibrary.catagory_icons_rank_normal;
      this.fixedButton2.ImageOver = (Image) GFXLibrary.catagory_icons_rank_over;
      this.fixedButton2.ImageClick = (Image) GFXLibrary.catagory_icons_rank_pushed;
      this.fixedButton2.Position = new Point(46, -7);
      this.fixedButton2.Data = -5;
      this.fixedButton2.CustomTooltipID = 1300;
      this.fixedButton2.CustomTooltipData = -5;
      this.fixedButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.fixedIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.fixedButton2);
      this.fixedButton3.ImageNorm = (Image) GFXLibrary.catagory_icons_villages_normal;
      this.fixedButton3.ImageOver = (Image) GFXLibrary.catagory_icons_villages_over;
      this.fixedButton3.ImageClick = (Image) GFXLibrary.catagory_icons_villages_pushed;
      this.fixedButton3.Position = new Point(106, -7);
      this.fixedButton3.Data = -6;
      this.fixedButton3.CustomTooltipID = 1300;
      this.fixedButton3.CustomTooltipData = -6;
      this.fixedButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.fixedIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.fixedButton3);
      this.fixedButton4.ImageNorm = (Image) GFXLibrary.catagory_icons_factions_normal;
      this.fixedButton4.ImageOver = (Image) GFXLibrary.catagory_icons_factions_over;
      this.fixedButton4.ImageClick = (Image) GFXLibrary.catagory_icons_factions_pushed;
      this.fixedButton4.Position = new Point(166, -7);
      this.fixedButton4.Data = -2;
      this.fixedButton4.CustomTooltipID = 1300;
      this.fixedButton4.CustomTooltipData = -2;
      this.fixedButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.fixedIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.fixedButton4);
      this.fixedButton5.ImageNorm = (Image) GFXLibrary.catagory_icons_houses_normal;
      this.fixedButton5.ImageOver = (Image) GFXLibrary.catagory_icons_houses_over;
      this.fixedButton5.ImageClick = (Image) GFXLibrary.catagory_icons_houses_pushed;
      this.fixedButton5.Position = new Point(226, -7);
      this.fixedButton5.Data = -3;
      this.fixedButton5.CustomTooltipID = 1300;
      this.fixedButton5.CustomTooltipData = -3;
      this.fixedButton5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.fixedIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.fixedButton5);
      this.fixedButton7.ImageNorm = (Image) GFXLibrary.catagory_icons_parishflags_normal;
      this.fixedButton7.ImageOver = (Image) GFXLibrary.catagory_icons_parishflags_over;
      this.fixedButton7.ImageClick = (Image) GFXLibrary.catagory_icons_parishflags_pushed;
      this.fixedButton7.Position = new Point(286, -7);
      this.fixedButton7.Data = -4;
      this.fixedButton7.CustomTooltipID = 1300;
      this.fixedButton7.CustomTooltipData = -4;
      this.fixedButton7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.fixedIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.fixedButton7);
      this.variButton1.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.variButton1.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.variButton1.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.variButton1.Position = new Point(38, -7);
      this.variButton1.Data = 0;
      this.variButton1.CustomTooltipID = 1300;
      this.variButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButton1);
      this.variButton2.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.variButton2.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.variButton2.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.variButton2.Position = new Point(88, -7);
      this.variButton2.Data = 1;
      this.variButton2.CustomTooltipID = 1300;
      this.variButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButton2);
      this.variButton3.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.variButton3.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.variButton3.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.variButton3.Position = new Point(138, -7);
      this.variButton3.Data = 2;
      this.variButton3.CustomTooltipID = 1300;
      this.variButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButton3);
      this.variButton4.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.variButton4.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.variButton4.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.variButton4.Position = new Point(188, -7);
      this.variButton4.Data = 3;
      this.variButton4.CustomTooltipID = 1300;
      this.variButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButton4);
      this.variButton5.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.variButton5.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.variButton5.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.variButton5.Position = new Point(238, -7);
      this.variButton5.Data = 4;
      this.variButton5.CustomTooltipID = 1300;
      this.variButton5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButton5);
      this.variButton6.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.variButton6.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.variButton6.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.variButton6.Position = new Point(288, -7);
      this.variButton6.Data = 5;
      this.variButton6.CustomTooltipID = 1300;
      this.variButton6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButton6);
      this.variButton7.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.variButton7.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.variButton7.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.variButton7.Position = new Point(338, -7);
      this.variButton7.Data = 6;
      this.variButton7.CustomTooltipID = 1300;
      this.variButton7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButton7);
      this.variButton8.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.variButton8.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.variButton8.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.variButton8.Position = new Point(388, -7);
      this.variButton8.Data = 7;
      this.variButton8.CustomTooltipID = 1300;
      this.variButton8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButton8);
      this.variButton9.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.variButton9.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.variButton9.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.variButton9.Position = new Point(438, -7);
      this.variButton9.Data = 8;
      this.variButton9.CustomTooltipID = 1300;
      this.variButton9.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButton9);
      this.variButton10.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.variButton10.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.variButton10.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.variButton10.Position = new Point(488, -7);
      this.variButton10.Data = 9;
      this.variButton10.CustomTooltipID = 1300;
      this.variButton10.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButton10);
      this.variButtonLeft.ImageNorm = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_left_normal;
      this.variButtonLeft.ImageOver = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_left_over;
      this.variButtonLeft.ImageClick = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_left_pressed;
      this.variButtonLeft.Position = new Point(6, -10);
      this.variButtonLeft.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryLeftClicked), "StatsPanel_category_left");
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButtonLeft);
      this.variButtonRight.ImageNorm = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_right_normal;
      this.variButtonRight.ImageOver = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_right_over;
      this.variButtonRight.ImageClick = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_right_pressed;
      this.variButtonRight.Position = new Point(538, -10);
      this.variButtonRight.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryRightClicked), "StatsPanel_category_right");
      this.scrollIconBar.addControl((CustomSelfDrawPanel.CSDControl) this.variButtonRight);
      this.mainEntry1.Position = new Point(39, 114);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry1);
      this.mainEntry2.Position = new Point(39, 163);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry2);
      this.mainEntry3.Position = new Point(39, 212);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry3);
      this.mainEntry4.Position = new Point(39, 261);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry4);
      this.mainEntry5.Position = new Point(39, 310);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry5);
      this.mainEntry6.Position = new Point(39, 359);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry6);
      this.mainEntry7.Position = new Point(39, 408);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry7);
      this.mainEntry8.Position = new Point(39, 457);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry8);
      this.mainEntry9.Position = new Point(39, 506);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry9);
      this.mainEntry10.Position = new Point(39, 555);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry10);
      this.mainEntry11.Position = new Point(39, 604);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry11);
      this.mainEntry12.Position = new Point(39, 653);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry12);
      this.mainEntry13.Position = new Point(39, 702);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry13);
      this.mainEntry14.Position = new Point(39, 751);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry14);
      this.mainEntry15.Position = new Point(39, 800);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry15);
      this.mainEntry16.Position = new Point(39, 849);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry16);
      this.mainEntry17.Position = new Point(39, 898);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry17);
      this.mainEntry18.Position = new Point(39, 947);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry18);
      this.mainEntry19.Position = new Point(39, 996);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry19);
      this.mainEntry20.Position = new Point(39, 1045);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainEntry20);
      this.topEntry1.Position = new Point(528, 114);
      this.topEntry1.setAsTopEntry();
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.topEntry1);
      this.topEntry2.Position = new Point(528, 163);
      this.topEntry2.setAsTopEntry();
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.topEntry2);
      this.topEntry3.Position = new Point(528, 212);
      this.topEntry3.setAsTopEntry();
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.topEntry3);
      this.topEntry4.Position = new Point(528, 261);
      this.topEntry4.setAsTopEntry();
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.topEntry4);
      this.topEntry5.Position = new Point(528, 310);
      this.topEntry5.setAsTopEntry();
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.topEntry5);
      this.topEntry6.Position = new Point(528, 359);
      this.topEntry6.setAsTopEntry();
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.topEntry6);
      this.topEntry7.Position = new Point(528, 408);
      this.topEntry7.setAsTopEntry();
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.topEntry7);
      StatsPanel.NUM_VISIBLE_LINES = 9 + num1 / 49;
      if (StatsPanel.NUM_VISIBLE_LINES > 20)
        StatsPanel.NUM_VISIBLE_LINES = 20;
      this.numExtraTopLines = num1 / 2 / 49;
      if (!resized)
      {
        this.mainEntry1.init(this.currentCategory, -1000, 0);
        this.mainEntry2.init(this.currentCategory, -1001, 1);
        this.mainEntry3.init(this.currentCategory, -1002, 2);
        this.mainEntry4.init(this.currentCategory, -1003, 3);
        this.mainEntry5.init(this.currentCategory, -1004, 4);
        this.mainEntry6.init(this.currentCategory, -1005, 5);
        this.mainEntry7.init(this.currentCategory, -1006, 6);
        this.mainEntry8.init(this.currentCategory, -1007, 7);
        this.mainEntry9.init(this.currentCategory, -1008, 8);
        if (StatsPanel.NUM_VISIBLE_LINES >= 10)
          this.mainEntry10.init(this.currentCategory, -1009, 9);
        if (StatsPanel.NUM_VISIBLE_LINES >= 11)
          this.mainEntry11.init(this.currentCategory, -1009, 10);
        if (StatsPanel.NUM_VISIBLE_LINES >= 12)
          this.mainEntry12.init(this.currentCategory, -1009, 11);
        if (StatsPanel.NUM_VISIBLE_LINES >= 13)
          this.mainEntry13.init(this.currentCategory, -1009, 12);
        if (StatsPanel.NUM_VISIBLE_LINES >= 14)
          this.mainEntry14.init(this.currentCategory, -1009, 13);
        if (StatsPanel.NUM_VISIBLE_LINES >= 15)
          this.mainEntry15.init(this.currentCategory, -1009, 14);
        if (StatsPanel.NUM_VISIBLE_LINES >= 16)
          this.mainEntry16.init(this.currentCategory, -1009, 15);
        if (StatsPanel.NUM_VISIBLE_LINES >= 17)
          this.mainEntry17.init(this.currentCategory, -1009, 16);
        if (StatsPanel.NUM_VISIBLE_LINES >= 18)
          this.mainEntry18.init(this.currentCategory, -1009, 17);
        if (StatsPanel.NUM_VISIBLE_LINES >= 19)
          this.mainEntry19.init(this.currentCategory, -1009, 18);
        if (StatsPanel.NUM_VISIBLE_LINES >= 20)
          this.mainEntry20.init(this.currentCategory, -1009, 19);
        this.topEntry1.init(this.currentCategory, 1, 0);
        this.topEntry2.init(this.currentCategory, 2, 1);
        this.topEntry3.init(this.currentCategory, 3, 2);
        if (this.numExtraTopLines > 0)
          this.topEntry4.init(this.currentCategory, 4, 3);
        if (this.numExtraTopLines > 1)
          this.topEntry5.init(this.currentCategory, 5, 4);
        if (this.numExtraTopLines > 2)
          this.topEntry6.init(this.currentCategory, 6, 5);
        if (this.numExtraTopLines > 3)
          this.topEntry7.init(this.currentCategory, 7, 6);
      }
      else
        this.updateEntries();
      int num2 = num1 / 2;
      this.secondInsetImage.Position = new Point(552, 316 + num2);
      this.secondInsetImage.Size = new Size(1, 248 + num1 / 2);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.secondInsetImage);
      this.secondInsetImage.Create((Image) GFXLibrary.int_statsscreen_secondinset_top, (Image) GFXLibrary.int_statsscreen_secondinset_middle, (Image) GFXLibrary.int_statsscreen_secondinset_bottom);
      this.selfEntry1.Position = new Point(572, 338 + num2);
      this.selfEntry1.init(0, this);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selfEntry1);
      this.selfEntry2.Position = new Point(572, 408 + num2);
      this.selfEntry2.init(1, this);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selfEntry2);
      this.selfEntry3.Position = new Point(572, 478 + num2);
      this.selfEntry3.init(2, this);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selfEntry3);
      this.selfEntry4.Position = new Point(572, 548 + num2);
      this.selfEntry4.init(3, this);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selfEntry4);
      this.selfEntry5.Position = new Point(572, 618 + num2);
      this.selfEntry5.init(4, this);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selfEntry5);
      this.selfEntry6.Position = new Point(572, 688 + num2);
      this.selfEntry6.init(5, this);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selfEntry6);
      this.selfEntry7.Position = new Point(572, 758 + num2);
      this.selfEntry7.init(6, this);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selfEntry7);
      int num3 = num1 / 2 / 70;
      this.selfEntry4.Visible = num3 >= 1;
      this.selfEntry5.Visible = num3 >= 2;
      this.selfEntry6.Visible = num3 >= 3;
      this.selfEntry7.Visible = num3 >= 4;
      this.topButton.ImageNorm = (Image) GFXLibrary.page_top_norrmal;
      this.topButton.ImageOver = (Image) GFXLibrary.page_top_over;
      this.topButton.ImageClick = (Image) GFXLibrary.page_top_pushed;
      this.topButton.Position = new Point(464, 114);
      this.topButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scrollToTopClicked), "StatsPanel_scroll_top");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.topButton);
      this.upButton.ImageNorm = (Image) GFXLibrary.page_up_normal;
      this.upButton.ImageOver = (Image) GFXLibrary.page_up_over;
      this.upButton.ImageClick = (Image) GFXLibrary.page_up_pushed;
      this.upButton.Position = new Point(464, this.topButton.Position.Y + 2 + this.topButton.ImageNorm.Height);
      this.upButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scrollUpClicked), "StatsPanel_scroll_up");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.upButton);
      this.bottomButton.ImageNorm = (Image) GFXLibrary.page_bottom_normal;
      this.bottomButton.ImageOver = (Image) GFXLibrary.page_bottom_over;
      this.bottomButton.ImageClick = (Image) GFXLibrary.page_bottom_pushed;
      this.bottomButton.Position = new Point(464, this.Height - 42 - this.bottomButton.ImageNorm.Height);
      this.bottomButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scrollToBottomClicked), "StatsPanel_scroll_bottom");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.bottomButton);
      this.downButton.ImageNorm = (Image) GFXLibrary.page_down_normal;
      this.downButton.ImageOver = (Image) GFXLibrary.page_down_over;
      this.downButton.ImageClick = (Image) GFXLibrary.page_down_pushed;
      this.downButton.Position = new Point(464, this.bottomButton.Position.Y - 2 - this.downButton.ImageNorm.Height);
      this.downButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scrollDownClicked), "StatsPanel_scroll_down");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.downButton);
      this.updateLabel.Text = "";
      this.updateLabel.Color = ARGBColors.Black;
      this.updateLabel.Position = new Point(50, this.Height - 22);
      this.updateLabel.Size = new Size(500, 25);
      this.updateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.updateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.updateLabel);
      this.bestRankingsLabel.Text = SK.Text("Stats_Best_Ranking", "Your Best Rankings");
      this.bestRankingsLabel.Color = ARGBColors.White;
      this.bestRankingsLabel.Position = new Point(570, 292 + num1 / 2);
      this.bestRankingsLabel.Size = new Size(300, 25);
      this.bestRankingsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.bestRankingsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.bestRankingsLabel);
      this.updateVariIcons();
      if (resized)
        return;
      this.newCategory();
    }

    private void updateVariIcons()
    {
      this.fixedButton1.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
      this.fixedButton1.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
      this.fixedButton1.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
      this.fixedButton2.ImageNorm = (Image) GFXLibrary.catagory_icons_rank_normal;
      this.fixedButton2.ImageOver = (Image) GFXLibrary.catagory_icons_rank_over;
      this.fixedButton2.ImageClick = (Image) GFXLibrary.catagory_icons_rank_pushed;
      this.fixedButton3.ImageNorm = (Image) GFXLibrary.catagory_icons_villages_normal;
      this.fixedButton3.ImageOver = (Image) GFXLibrary.catagory_icons_villages_over;
      this.fixedButton3.ImageClick = (Image) GFXLibrary.catagory_icons_villages_pushed;
      this.fixedButton4.ImageNorm = (Image) GFXLibrary.catagory_icons_factions_normal;
      this.fixedButton4.ImageOver = (Image) GFXLibrary.catagory_icons_factions_over;
      this.fixedButton4.ImageClick = (Image) GFXLibrary.catagory_icons_factions_pushed;
      this.fixedButton5.ImageNorm = (Image) GFXLibrary.catagory_icons_houses_normal;
      this.fixedButton5.ImageOver = (Image) GFXLibrary.catagory_icons_houses_over;
      this.fixedButton5.ImageClick = (Image) GFXLibrary.catagory_icons_houses_pushed;
      this.fixedButton7.ImageNorm = (Image) GFXLibrary.catagory_icons_parishflags_normal;
      this.fixedButton7.ImageOver = (Image) GFXLibrary.catagory_icons_parishflags_over;
      this.fixedButton7.ImageClick = (Image) GFXLibrary.catagory_icons_parishflags_pushed;
      for (int i = 0; i < 10; ++i)
        this.setButtonGFX(this.getVariButton(i), i + this.categoryScrollPos);
      if (this.currentCategory < 0)
      {
        switch (this.currentCategory)
        {
          case -6:
            this.fixedButton3.ImageNorm = (Image) GFXLibrary.catagory_icons_villages_pushed;
            this.fixedButton3.ImageOver = (Image) GFXLibrary.catagory_icons_villages_pushed;
            this.fixedButton3.ImageClick = (Image) GFXLibrary.catagory_icons_villages_pushed;
            break;
          case -5:
            this.fixedButton2.ImageNorm = (Image) GFXLibrary.catagory_icons_rank_pushed;
            this.fixedButton2.ImageOver = (Image) GFXLibrary.catagory_icons_rank_pushed;
            this.fixedButton2.ImageClick = (Image) GFXLibrary.catagory_icons_rank_pushed;
            break;
          case -4:
            this.fixedButton7.ImageNorm = (Image) GFXLibrary.catagory_icons_parishflags_pushed;
            this.fixedButton7.ImageOver = (Image) GFXLibrary.catagory_icons_parishflags_pushed;
            this.fixedButton7.ImageClick = (Image) GFXLibrary.catagory_icons_parishflags_pushed;
            break;
          case -3:
            this.fixedButton5.ImageNorm = (Image) GFXLibrary.catagory_icons_houses_pushed;
            this.fixedButton5.ImageOver = (Image) GFXLibrary.catagory_icons_houses_pushed;
            this.fixedButton5.ImageClick = (Image) GFXLibrary.catagory_icons_houses_pushed;
            break;
          case -2:
            this.fixedButton4.ImageNorm = (Image) GFXLibrary.catagory_icons_factions_pushed;
            this.fixedButton4.ImageOver = (Image) GFXLibrary.catagory_icons_factions_pushed;
            this.fixedButton4.ImageClick = (Image) GFXLibrary.catagory_icons_factions_pushed;
            break;
          case -1:
            this.fixedButton1.ImageNorm = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.fixedButton1.ImageOver = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.fixedButton1.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            break;
        }
      }
      if (this.categoryScrollPos <= 0)
        this.variButtonLeft.Enabled = false;
      else
        this.variButtonLeft.Enabled = true;
      if (this.categoryScrollPos >= this.numVariCats)
        this.variButtonRight.Enabled = false;
      else
        this.variButtonRight.Enabled = true;
    }

    private CustomSelfDrawPanel.CSDButton getVariButton(int i)
    {
      switch (i)
      {
        case 0:
          return this.variButton1;
        case 1:
          return this.variButton2;
        case 2:
          return this.variButton3;
        case 3:
          return this.variButton4;
        case 4:
          return this.variButton5;
        case 5:
          return this.variButton6;
        case 6:
          return this.variButton7;
        case 7:
          return this.variButton8;
        case 8:
          return this.variButton9;
        case 9:
          return this.variButton10;
        default:
          return this.variButton1;
      }
    }

    public void setButtonGFX(CustomSelfDrawPanel.CSDButton button, int position)
    {
      int type = this.mapVariButtonPositionToType(position);
      button.CustomTooltipData = type;
      BaseImage baseImage1 = (BaseImage) null;
      BaseImage baseImage2 = (BaseImage) null;
      BaseImage baseImage3 = (BaseImage) null;
      button.Enabled = true;
      switch (type)
      {
        case 0:
          baseImage1 = GFXLibrary.catagory_icons_pillager_normal;
          baseImage2 = GFXLibrary.catagory_icons_pillager_over;
          baseImage3 = GFXLibrary.catagory_icons_pillager_pushed;
          break;
        case 1:
          baseImage1 = GFXLibrary.catagory_icons_defender_normal;
          baseImage2 = GFXLibrary.catagory_icons_defender_over;
          baseImage3 = GFXLibrary.catagory_icons_defender_pushed;
          break;
        case 2:
          baseImage1 = GFXLibrary.catagory_icons_destroyer_normal;
          baseImage2 = GFXLibrary.catagory_icons_destroyer_over;
          baseImage3 = GFXLibrary.catagory_icons_destroyer_pushed;
          break;
        case 3:
          baseImage1 = GFXLibrary.catagory_icons_wolfbane_normal;
          baseImage2 = GFXLibrary.catagory_icons_wolfbane_over;
          baseImage3 = GFXLibrary.catagory_icons_wolfbane_pushed;
          break;
        case 4:
          baseImage1 = GFXLibrary.catagory_icons_banditslayer_normal;
          baseImage2 = GFXLibrary.catagory_icons_banditslayer_over;
          baseImage3 = GFXLibrary.catagory_icons_banditslayer_pushed;
          break;
        case 5:
          baseImage1 = GFXLibrary.catagory_icons_aikiller_normal;
          baseImage2 = GFXLibrary.catagory_icons_aikiller_over;
          baseImage3 = GFXLibrary.catagory_icons_aikiller_pushed;
          break;
        case 6:
          baseImage1 = GFXLibrary.catagory_icons_merchant_normal;
          baseImage2 = GFXLibrary.catagory_icons_merchant_over;
          baseImage3 = GFXLibrary.catagory_icons_merchant_pushed;
          break;
        case 7:
          baseImage1 = GFXLibrary.catagory_icons_forger_normal;
          baseImage2 = GFXLibrary.catagory_icons_forger_over;
          baseImage3 = GFXLibrary.catagory_icons_forger_pushed;
          break;
        case 8:
          baseImage1 = GFXLibrary.catagory_icons_worker_normal;
          baseImage2 = GFXLibrary.catagory_icons_worker_over;
          baseImage3 = GFXLibrary.catagory_icons_worker_pushed;
          break;
        case 9:
          baseImage1 = GFXLibrary.catagory_icons_farmer_normal;
          baseImage2 = GFXLibrary.catagory_icons_farmer_over;
          baseImage3 = GFXLibrary.catagory_icons_farmer_pushed;
          break;
        case 10:
          baseImage1 = GFXLibrary.catagory_icons_brewer_normal;
          baseImage2 = GFXLibrary.catagory_icons_brewer_over;
          baseImage3 = GFXLibrary.catagory_icons_brewer_pushed;
          break;
        case 11:
          baseImage1 = GFXLibrary.catagory_icons_blacksmith_normal;
          baseImage2 = GFXLibrary.catagory_icons_blacksmith_over;
          baseImage3 = GFXLibrary.catagory_icons_blacksmith_pushed;
          break;
        case 12:
          baseImage1 = GFXLibrary.catagory_icons_banquet_normal;
          baseImage2 = GFXLibrary.catagory_icons_banquet_over;
          baseImage3 = GFXLibrary.catagory_icons_banquet_pushed;
          break;
        case 13:
          baseImage1 = GFXLibrary.catagory_icons_achiever_normal;
          baseImage2 = GFXLibrary.catagory_icons_achiever_over;
          baseImage3 = GFXLibrary.catagory_icons_achiever_pushed;
          break;
        case 14:
          baseImage1 = GFXLibrary.catagory_icons_donator_normal;
          baseImage2 = GFXLibrary.catagory_icons_donator_over;
          baseImage3 = GFXLibrary.catagory_icons_donator_pushed;
          break;
        case 15:
          baseImage1 = GFXLibrary.catagory_icons_capture_normal;
          baseImage2 = GFXLibrary.catagory_icons_capture_over;
          baseImage3 = GFXLibrary.catagory_icons_capture_pushed;
          break;
        case 16:
          baseImage1 = GFXLibrary.catagory_icons_raze_normal;
          baseImage2 = GFXLibrary.catagory_icons_raze_over;
          baseImage3 = GFXLibrary.catagory_icons_raze_pushed;
          break;
        case 17:
          baseImage1 = GFXLibrary.catagory_icons_glory_normal;
          baseImage2 = GFXLibrary.catagory_icons_glory_over;
          baseImage3 = GFXLibrary.catagory_icons_glory_pushed;
          break;
        case 18:
          baseImage1 = GFXLibrary.catagory_icons_killstreak_normal;
          baseImage2 = GFXLibrary.catagory_icons_killstreak_over;
          baseImage3 = GFXLibrary.catagory_icons_killstreak_pushed;
          break;
      }
      if (type == this.currentCategory)
      {
        button.ImageNorm = (Image) baseImage3;
        button.ImageOver = (Image) baseImage3;
        button.ImageClick = (Image) baseImage3;
      }
      else
      {
        button.ImageNorm = (Image) baseImage1;
        button.ImageOver = (Image) baseImage2;
        button.ImageClick = (Image) baseImage3;
      }
      this.categoryLabel.Text = StatsPanel.getCategoryTitle(this.currentCategory);
      this.categoryDescription.Text = StatsPanel.getCategoryDescription(this.currentCategory);
      Graphics graphics = this.CreateGraphics();
      Size size = graphics.MeasureString(this.categoryLabel.Text, this.categoryLabel.Font, 100000).ToSize();
      graphics.Dispose();
      this.categoryDescription.Position = new Point(this.categoryLabel.X + size.Width + 5, this.categoryDescription.Y);
    }

    public void categoryClicked()
    {
      if (this.ClickedControl == null)
        return;
      int data = this.ClickedControl.Data;
      int currentCategory = this.currentCategory;
      this.currentCategory = data >= 0 ? this.mapVariButtonPositionToType(data + this.categoryScrollPos) : data;
      if (currentCategory == this.currentCategory)
        return;
      GameEngine.Instance.playInterfaceSound("StatsPanel_category_changed");
      this.updateVariIcons();
      this.newCategory();
    }

    public void changeCategory(int category)
    {
      if (category == this.currentCategory)
        return;
      this.currentCategory = category;
      this.updateVariIcons();
      this.newCategory();
    }

    public void categoryLeftClicked()
    {
      if (this.categoryScrollPos <= 0)
        return;
      --this.categoryScrollPos;
      this.updateVariIcons();
    }

    public void categoryRightClicked()
    {
      if (this.categoryScrollPos >= this.numVariCats)
        return;
      ++this.categoryScrollPos;
      this.updateVariIcons();
    }

    public int mapVariButtonPositionToType(int position) => position;

    public void update()
    {
      if (this.currentUserLine == -1000 && GameEngine.Instance.World.isLeaderboardCategoryPopulated(this.currentCategory))
      {
        int leaderboardEntries = GameEngine.Instance.World.getMaxLeaderboardEntries(this.currentCategory);
        this.currentUserLine = GameEngine.Instance.World.findSelfInLeaderboard(this.currentCategory) - StatsPanel.NUM_VISIBLE_LINES / 2;
        if (this.currentUserLine > leaderboardEntries - (StatsPanel.NUM_VISIBLE_LINES - 1))
          this.currentUserLine = leaderboardEntries - (StatsPanel.NUM_VISIBLE_LINES - 1);
        if (this.currentUserLine < 1)
          this.currentUserLine = 1;
        this.updateEntries();
      }
      this.mainEntry1.update();
      this.mainEntry2.update();
      this.mainEntry3.update();
      this.mainEntry4.update();
      this.mainEntry5.update();
      this.mainEntry6.update();
      this.mainEntry7.update();
      this.mainEntry8.update();
      this.mainEntry9.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 10)
        this.mainEntry10.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 11)
        this.mainEntry11.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 12)
        this.mainEntry12.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 13)
        this.mainEntry13.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 14)
        this.mainEntry14.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 15)
        this.mainEntry15.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 16)
        this.mainEntry16.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 17)
        this.mainEntry17.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 18)
        this.mainEntry18.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 19)
        this.mainEntry19.update();
      if (StatsPanel.NUM_VISIBLE_LINES >= 20)
        this.mainEntry20.update();
      this.topEntry1.update();
      this.topEntry2.update();
      this.topEntry3.update();
      if (this.numExtraTopLines > 0)
        this.topEntry4.update();
      if (this.numExtraTopLines > 1)
        this.topEntry5.update();
      if (this.numExtraTopLines > 2)
        this.topEntry6.update();
      if (this.numExtraTopLines > 3)
        this.topEntry7.update();
      if (this.initialTextInTextbox || this.searchInput.Text.Length < 4 || this.currentCategory == -2 || this.currentCategory == -3 || this.currentCategory == -4 || GameEngine.Instance.World.downloadingLeaderboard())
      {
        if (this.currentCategory == -4 || this.currentCategory == -3 || this.currentCategory == -2 || GameEngine.Instance.World.downloadingLeaderboard())
          this.searchButton.Enabled = false;
        else if (this.initialTextInTextbox || this.searchInput.Text.Length == 0)
          this.searchButton.Enabled = true;
        else
          this.searchButton.Enabled = false;
      }
      else
        this.searchButton.Enabled = true;
      this.clearSearchButton.Visible = this.inSearchResults;
      if (GameEngine.Instance.World.downloadingLeaderboard())
      {
        this.topButton.Enabled = false;
        this.upButton.Enabled = false;
        this.downButton.Enabled = false;
        this.bottomButton.Enabled = false;
      }
      else
      {
        this.topButton.Enabled = true;
        this.upButton.Enabled = true;
        this.downButton.Enabled = true;
        this.bottomButton.Enabled = true;
      }
      DateTime leaderboardUpdate = GameEngine.Instance.World.getLastLeaderboardUpdate();
      if (leaderboardUpdate == DateTime.MinValue)
      {
        this.updateLabel.Visible = false;
      }
      else
      {
        this.updateLabel.Visible = true;
        this.updateLabel.Text = "(" + SK.Text("Stats_Last_Updated", "last updated") + "   :   " + leaderboardUpdate.ToShortDateString() + ":" + leaderboardUpdate.ToShortTimeString() + ")";
      }
      if (!GameEngine.Instance.World.areSelfStandingsDirty())
        return;
      this.selfEntry1.init(0, this);
      this.selfEntry2.init(1, this);
      this.selfEntry3.init(2, this);
      this.selfEntry4.init(3, this);
      this.selfEntry5.init(4, this);
      this.selfEntry6.init(5, this);
      this.selfEntry7.init(6, this);
    }

    public void newCategory()
    {
      this.inSearchResults = false;
      if (GameEngine.Instance.World.isLeaderboardCategoryPopulated(this.currentCategory))
      {
        this.currentUserLine = GameEngine.Instance.World.findSelfInLeaderboard(this.currentCategory) - StatsPanel.NUM_VISIBLE_LINES / 2;
        int leaderboardEntries = GameEngine.Instance.World.getMaxLeaderboardEntries(this.currentCategory);
        if (this.currentUserLine > leaderboardEntries - (StatsPanel.NUM_VISIBLE_LINES - 1))
          this.currentUserLine = leaderboardEntries - (StatsPanel.NUM_VISIBLE_LINES - 1);
        if (this.currentUserLine < 1)
          this.currentUserLine = 1;
      }
      else
        this.currentUserLine = -1000;
      this.updateEntries();
    }

    public void updateEntries()
    {
      if (!this.inSearchResults)
      {
        this.mainEntry1.init(this.currentCategory, this.currentUserLine, 0);
        this.mainEntry2.init(this.currentCategory, this.currentUserLine + 1, 1);
        this.mainEntry3.init(this.currentCategory, this.currentUserLine + 2, 2);
        this.mainEntry4.init(this.currentCategory, this.currentUserLine + 3, 3);
        this.mainEntry5.init(this.currentCategory, this.currentUserLine + 4, 4);
        this.mainEntry6.init(this.currentCategory, this.currentUserLine + 5, 5);
        this.mainEntry7.init(this.currentCategory, this.currentUserLine + 6, 6);
        this.mainEntry8.init(this.currentCategory, this.currentUserLine + 7, 7);
        this.mainEntry9.init(this.currentCategory, this.currentUserLine + 8, 8);
        if (StatsPanel.NUM_VISIBLE_LINES >= 10)
          this.mainEntry10.init(this.currentCategory, this.currentUserLine + 9, 9);
        if (StatsPanel.NUM_VISIBLE_LINES >= 11)
          this.mainEntry11.init(this.currentCategory, this.currentUserLine + 10, 10);
        if (StatsPanel.NUM_VISIBLE_LINES >= 12)
          this.mainEntry12.init(this.currentCategory, this.currentUserLine + 11, 11);
        if (StatsPanel.NUM_VISIBLE_LINES >= 13)
          this.mainEntry13.init(this.currentCategory, this.currentUserLine + 12, 12);
        if (StatsPanel.NUM_VISIBLE_LINES >= 14)
          this.mainEntry14.init(this.currentCategory, this.currentUserLine + 13, 13);
        if (StatsPanel.NUM_VISIBLE_LINES >= 15)
          this.mainEntry15.init(this.currentCategory, this.currentUserLine + 14, 14);
        if (StatsPanel.NUM_VISIBLE_LINES >= 16)
          this.mainEntry16.init(this.currentCategory, this.currentUserLine + 15, 15);
        if (StatsPanel.NUM_VISIBLE_LINES >= 17)
          this.mainEntry17.init(this.currentCategory, this.currentUserLine + 16, 16);
        if (StatsPanel.NUM_VISIBLE_LINES >= 18)
          this.mainEntry18.init(this.currentCategory, this.currentUserLine + 17, 17);
        if (StatsPanel.NUM_VISIBLE_LINES >= 19)
          this.mainEntry19.init(this.currentCategory, this.currentUserLine + 18, 18);
        if (StatsPanel.NUM_VISIBLE_LINES >= 20)
          this.mainEntry20.init(this.currentCategory, this.currentUserLine + 19, 19);
      }
      else
      {
        this.mainEntry1.init(this.currentCategory, this.getSearchEntry(this.searchLocation), 0);
        this.mainEntry2.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 1), 1);
        this.mainEntry3.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 2), 2);
        this.mainEntry4.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 3), 3);
        this.mainEntry5.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 4), 4);
        this.mainEntry6.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 5), 5);
        this.mainEntry7.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 6), 6);
        this.mainEntry8.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 7), 7);
        this.mainEntry9.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 8), 8);
        if (StatsPanel.NUM_VISIBLE_LINES >= 10)
          this.mainEntry10.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 9), 9);
        if (StatsPanel.NUM_VISIBLE_LINES >= 11)
          this.mainEntry11.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 10), 10);
        if (StatsPanel.NUM_VISIBLE_LINES >= 12)
          this.mainEntry12.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 11), 11);
        if (StatsPanel.NUM_VISIBLE_LINES >= 13)
          this.mainEntry13.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 12), 12);
        if (StatsPanel.NUM_VISIBLE_LINES >= 14)
          this.mainEntry14.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 13), 13);
        if (StatsPanel.NUM_VISIBLE_LINES >= 15)
          this.mainEntry15.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 14), 14);
        if (StatsPanel.NUM_VISIBLE_LINES >= 16)
          this.mainEntry16.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 15), 15);
        if (StatsPanel.NUM_VISIBLE_LINES >= 17)
          this.mainEntry17.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 16), 16);
        if (StatsPanel.NUM_VISIBLE_LINES >= 18)
          this.mainEntry18.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 17), 17);
        if (StatsPanel.NUM_VISIBLE_LINES >= 19)
          this.mainEntry19.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 18), 18);
        if (StatsPanel.NUM_VISIBLE_LINES >= 20)
          this.mainEntry20.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 19), 19);
      }
      this.topEntry1.init(this.currentCategory, 1, 0);
      this.topEntry2.init(this.currentCategory, 2, 1);
      this.topEntry3.init(this.currentCategory, 3, 2);
      if (this.numExtraTopLines > 0)
        this.topEntry4.init(this.currentCategory, 4, 3);
      if (this.numExtraTopLines > 1)
        this.topEntry5.init(this.currentCategory, 5, 4);
      if (this.numExtraTopLines > 2)
        this.topEntry6.init(this.currentCategory, 6, 5);
      if (this.numExtraTopLines <= 3)
        return;
      this.topEntry7.init(this.currentCategory, 7, 6);
    }

    public void scrollToTopClicked()
    {
      if (GameEngine.Instance.World.downloadingLeaderboard())
        return;
      if (!this.inSearchResults)
      {
        if (this.currentUserLine == 1)
          return;
        this.currentUserLine = 1;
        this.updateEntries();
      }
      else
      {
        if (this.searchLocation == 0)
          return;
        this.searchLocation = 0;
        this.updateEntries();
      }
    }

    public void scrollUpClicked()
    {
      if (GameEngine.Instance.World.downloadingLeaderboard())
        return;
      if (!this.inSearchResults)
      {
        int position = this.currentUserLine - StatsPanel.NUM_VISIBLE_LINES;
        if (position < 1)
          position = 1;
        if (position == this.currentUserLine)
          return;
        this.currentUserLine = position;
        this.updateEntries();
        if (GameEngine.Instance.World.downloadingLeaderboard() || position == 1)
          return;
        GameEngine.Instance.World.leaderboardLookHigher(this.currentCategory, position, StatsPanel.NUM_VISIBLE_LINES);
      }
      else
      {
        int num = this.searchLocation - StatsPanel.NUM_VISIBLE_LINES;
        if (num < 0)
          num = 0;
        if (num == this.searchLocation)
          return;
        this.searchLocation = num;
        this.updateEntries();
      }
    }

    public void scrollDownClicked()
    {
      if (GameEngine.Instance.World.downloadingLeaderboard())
        return;
      if (!this.inSearchResults)
      {
        int leaderboardEntries = GameEngine.Instance.World.getMaxLeaderboardEntries(this.currentCategory);
        int position = this.currentUserLine + StatsPanel.NUM_VISIBLE_LINES;
        if (position > leaderboardEntries - (StatsPanel.NUM_VISIBLE_LINES - 1))
          position = leaderboardEntries - (StatsPanel.NUM_VISIBLE_LINES - 1);
        if (position < 1)
          position = 1;
        if (position == this.currentUserLine)
          return;
        this.currentUserLine = position;
        this.updateEntries();
        if (GameEngine.Instance.World.downloadingLeaderboard())
          return;
        GameEngine.Instance.World.leaderboardLookLower(this.currentCategory, position, StatsPanel.NUM_VISIBLE_LINES);
      }
      else
      {
        int num1 = this.getMaxSearchResults() - 1;
        int num2 = this.searchLocation + StatsPanel.NUM_VISIBLE_LINES;
        if (num2 > num1 - StatsPanel.NUM_VISIBLE_LINES)
        {
          num2 = num1 - (StatsPanel.NUM_VISIBLE_LINES - 1);
          if (num2 < 0)
            num2 = 0;
        }
        if (num2 == this.searchLocation)
          return;
        this.searchLocation = num2;
        this.updateEntries();
      }
    }

    public void scrollToBottomClicked()
    {
      if (GameEngine.Instance.World.downloadingLeaderboard())
        return;
      if (!this.inSearchResults)
      {
        int leaderboardEntries = GameEngine.Instance.World.getMaxLeaderboardEntries(this.currentCategory);
        if (leaderboardEntries - (StatsPanel.NUM_VISIBLE_LINES - 1) == this.currentUserLine)
          return;
        this.currentUserLine = leaderboardEntries - (StatsPanel.NUM_VISIBLE_LINES - 1);
        if (this.currentUserLine > 1)
          this.updateEntries();
        else
          this.currentUserLine = 1;
      }
      else
      {
        int num = this.getMaxSearchResults() - 1;
        if (num - (StatsPanel.NUM_VISIBLE_LINES - 1) == this.searchLocation)
          return;
        this.searchLocation = num - (StatsPanel.NUM_VISIBLE_LINES - 1);
        if (this.searchLocation < 0)
          this.searchLocation = 0;
        this.updateEntries();
      }
    }

    public static string getCategoryTitle(int category)
    {
      switch (category)
      {
        case -6:
          return SK.Text("STATS_CATEGORY_TITLE_NUMVILLAGES", "Villages");
        case -5:
          return SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank");
        case -4:
          return SK.Text("STATS_CATEGORY_TITLE_PARISH_FLAGS", "Parish Flags");
        case -3:
          return SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House");
        case -2:
          return SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction");
        case -1:
          return SK.Text("STATS_CATEGORY_TITLE_POINTS", "Points");
        case 0:
          return SK.Text("STATS_CATEGORY_TITLE_PILLAGER", "Pillager");
        case 1:
          return SK.Text("STATS_CATEGORY_TITLE_DEFENDER", "Defender");
        case 2:
          return SK.Text("STATS_CATEGORY_TITLE_DESTROYER", "Destroyer");
        case 3:
          return SK.Text("STATS_CATEGORY_TITLE_WOLFSBANE", "Wolfs Bane");
        case 4:
          return SK.Text("STATS_CATEGORY_TITLE_BANDIT_KILLER", "Bandit Killer");
        case 5:
          return SK.Text("STATS_CATEGORY_TITLE_AI_KILLER", "AI Killer");
        case 6:
          return SK.Text("STATS_CATEGORY_TITLE_MERCHANT", "Merchant");
        case 7:
          return SK.Text("STATS_CATEGORY_TITLE_FORAGER", "Forager");
        case 8:
          return SK.Text("STATS_CATEGORY_TITLE_WORKER", "Worker");
        case 9:
          return SK.Text("STATS_CATEGORY_TITLE_FARMER", "Farmer");
        case 10:
          return SK.Text("STATS_CATEGORY_TITLE_BREWER", "Brewer");
        case 11:
          return SK.Text("STATS_CATEGORY_TITLE_WEAPONSMITH", "Weaponsmith");
        case 12:
          return SK.Text("STATS_CATEGORY_TITLE_BANQUETTER", "Banquetter");
        case 13:
          return SK.Text("STATS_CATEGORY_TITLE_QUESTER", "Quester");
        case 14:
          return SK.Text("STATS_CATEGORY_TITLE_DONATER", "Donator");
        case 15:
          return SK.Text("STATS_CATEGORY_TITLE_CAPTURE", "Conqueror");
        case 16:
          return SK.Text("STATS_CATEGORY_TITLE_RAZE", "Annihilator");
        case 17:
          return SK.Text("STATS_CATEGORY_TITLE_GLORY", "Glory Hunter");
        case 18:
          return SK.Text("STATS_CATEGORY_TITLE_KILL_STREAK", "Crusader");
        default:
          return "";
      }
    }

    public static string getCategoryDescription(int category)
    {
      switch (category)
      {
        case -6:
          return SK.Text("Stats_Most_Villages_Owned", "Most Villages owned");
        case -5:
          return SK.Text("Stats_Highest_Rank", "Highest Rank");
        case -4:
          return SK.Text("Stats_Most_Parish_Flags", "Most Parish Flags");
        case -3:
          return SK.Text("Stats_Most_House_Points", "Most House Points");
        case -2:
          return SK.Text("Stats_Most_Faction_Points", "Most Faction Points");
        case -1:
          return SK.Text("Stats_Most_Points", "Most points");
        case 0:
          return SK.Text("Stats_Most_Pillaged", "Most goods pillaged from others");
        case 1:
          return SK.Text("Stats_Most_Invaders_Killed", "Most invading troops killed at the castle walls");
        case 2:
          return SK.Text("Stats_Most_Ransacked", "Most buildings ransacked in someone else's castle");
        case 3:
          return SK.Text("Stats_Most_Wolves_Killed", "Most wolves killed");
        case 4:
          return SK.Text("Stats_Most_Bandits_Killed", "Most bandits killed");
        case 5:
          return SK.Text("Stats_Most_AI_Killed", "Most AI Troops Killed");
        case 6:
          return SK.Text("Stats_Most_Goods_Traded", "Most goods traded");
        case 7:
          return SK.Text("Stats_Most_Goods_Scouted", "Most goods scouted from the map");
        case 8:
          return SK.Text("Stats_Most_Stockpike_Goods", "Most stockpile goods produced (updated daily)");
        case 9:
          return SK.Text("Stats_Most_Food_Produced", "Most foods produced (updated daily)");
        case 10:
          return SK.Text("Stats_Most_Ale_Produced", "Most Ale produced (updated daily)");
        case 11:
          return SK.Text("Stats_Most_Weapons_Produced", "Most weapons produced (updated daily)");
        case 12:
          return SK.Text("Stats_Most_Banquetting_Oroduced", "Most banqueting goods produced (updated daily)");
        case 13:
          return SK.Text("Stats_Most_Quests_Completed", "Most Quests Completed");
        case 14:
          return SK.Text("Stats_Most_Dontations", "Most Capital Donations (as 'Packets')");
        case 15:
          return SK.Text("Stats_Most_Captures", "Most Villages Captured");
        case 16:
          return SK.Text("Stats_Most_Razes", "Most Villages Razed");
        case 17:
          return SK.Text("Stats_Most_Glory", "Most Glory Gained");
        case 18:
          return SK.Text("Stats_Most_KillStreak", "Longest Kill Streak");
        default:
          return "";
      }
    }

    private void searchInput_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      this.searchClicked();
      e.Handled = true;
    }

    private void searchInput_Enter(object sender, EventArgs e)
    {
      if (!this.initialTextInTextbox)
        return;
      this.initialTextInTextbox = false;
      this.searchInput.Text = "";
    }

    public void searchClicked()
    {
      if (GameEngine.Instance.World.downloadingLeaderboard())
        return;
      if (this.searchInput.Text.Length >= 4 && this.currentCategory != -3 && this.currentCategory != -4 && !this.initialTextInTextbox)
      {
        GameEngine.Instance.World.leaderboardSearch(this.currentCategory, this.searchInput.Text);
      }
      else
      {
        if (!this.initialTextInTextbox && this.searchInput.Text.Length != 0)
          return;
        this.inSearchResults = false;
        this.updateEntries();
      }
    }

    public void searchComplete(LeaderBoardSearchResults results)
    {
      this.m_results = results;
      this.inSearchResults = true;
      this.searchLocation = 0;
      this.currentUserLine = 1;
      this.updateEntries();
    }

    private int getSearchEntry(int entryID)
    {
      return entryID >= this.m_results.entries.Count ? -999999 : this.m_results.entries[entryID];
    }

    private int getMaxSearchResults() => this.m_results.entries.Count;

    private void clearSearchClicked()
    {
      this.clearSearchButton.Visible = false;
      this.inSearchResults = false;
      this.updateEntries();
    }

    public class StatsEntry : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel positionLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel valueLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
      private int m_category = -1000;
      private int m_position = -1000;
      private int m_screenLine;
      private int m_entryID = -1;
      private bool m_validData;
      private bool topEntry;

      public void init(int category, int leaderboardPosition, int screenLocation)
      {
        this.m_category = category;
        this.m_position = leaderboardPosition;
        this.m_screenLine = screenLocation;
        this.m_validData = false;
        LeaderBoardEntryData leaderBoardEntryData;
        if (leaderboardPosition != -999999)
        {
          leaderBoardEntryData = GameEngine.Instance.World.getLeaderboardEntry(category, leaderboardPosition, StatsPanel.NUM_VISIBLE_LINES);
          if (leaderBoardEntryData != null)
            this.m_validData = true;
        }
        else
        {
          leaderBoardEntryData = new LeaderBoardEntryData();
          leaderBoardEntryData.dummy = true;
          this.m_validData = true;
        }
        this.clearControls();
        this.backgroundImage.Image = (screenLocation & 1) != 0 ? (Image) GFXLibrary.int_statsscreen_listbar_lighter : (Image) GFXLibrary.int_statsscreen_listbar_darker;
        this.backgroundImage.Position = new Point(0, 0);
        this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.houseImage.Image = (Image) GFXLibrary.getHouseFlag(1);
        this.houseImage.Position = new Point(68, 0);
        this.houseImage.Visible = false;
        this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.houseImage.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseImage);
        this.playerName.Text = SK.Text("Stats_Getting_Data", "Getting Data");
        this.playerName.Color = ARGBColors.Black;
        this.playerName.Position = new Point(129, 0);
        this.playerName.Size = new Size(188, this.backgroundImage.Height);
        this.playerName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.playerName.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.playerName);
        this.positionLabel.Text = "0";
        this.positionLabel.Color = ARGBColors.Black;
        this.positionLabel.Position = new Point(6, 0);
        this.positionLabel.Size = new Size(56, this.backgroundImage.Height);
        this.positionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.positionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.positionLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.positionLabel.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.positionLabel);
        this.valueLabel.Text = "0";
        this.valueLabel.Color = ARGBColors.Black;
        this.valueLabel.Position = new Point(9, 0);
        this.valueLabel.Size = new Size(380, this.backgroundImage.Height);
        this.valueLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.valueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.valueLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.valueLabel.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.valueLabel);
        int playerID = -1;
        if (leaderBoardEntryData == null)
          return;
        if (!leaderBoardEntryData.dummy)
        {
          this.playerName.Color = ARGBColors.Black;
          this.valueLabel.Color = ARGBColors.Black;
          this.positionLabel.Color = ARGBColors.Black;
          if (this.m_category >= 0 || this.m_category == -1 || this.m_category == -5 || this.m_category == -6)
          {
            if (leaderBoardEntryData.entryID == RemoteServices.Instance.UserID)
            {
              this.playerName.Color = ARGBColors.White;
              this.valueLabel.Color = ARGBColors.White;
              this.positionLabel.Color = ARGBColors.White;
            }
            playerID = leaderBoardEntryData.entryID;
          }
          else if (this.m_category == -2)
          {
            if (leaderBoardEntryData.entryID == RemoteServices.Instance.UserFactionID)
            {
              this.playerName.Color = ARGBColors.White;
              this.valueLabel.Color = ARGBColors.White;
              this.positionLabel.Color = ARGBColors.White;
            }
          }
          else if (this.m_category == -3)
          {
            if (leaderBoardEntryData.entryID == GameEngine.Instance.World.getHouse(RemoteServices.Instance.UserFactionID))
            {
              this.playerName.Color = ARGBColors.White;
              this.valueLabel.Color = ARGBColors.White;
              this.positionLabel.Color = ARGBColors.White;
            }
          }
          else if (this.m_category == -4)
          {
            foreach (int userVillageId in GameEngine.Instance.World.getUserVillageIDList())
            {
              if (GameEngine.Instance.World.isCapital(userVillageId))
              {
                if (GameEngine.Instance.World.isRegionCapital(userVillageId))
                {
                  int parishFromVillageId = GameEngine.Instance.World.getParishFromVillageID(userVillageId);
                  if (leaderBoardEntryData.entryID == parishFromVillageId)
                  {
                    this.playerName.Color = ARGBColors.White;
                    this.valueLabel.Color = ARGBColors.White;
                    this.positionLabel.Color = ARGBColors.White;
                    break;
                  }
                }
              }
              else
              {
                int parishFromVillageId = GameEngine.Instance.World.getParishFromVillageID(userVillageId);
                if (leaderBoardEntryData.entryID == parishFromVillageId)
                {
                  this.playerName.Color = ARGBColors.White;
                  this.valueLabel.Color = ARGBColors.White;
                  this.positionLabel.Color = ARGBColors.White;
                  break;
                }
              }
            }
          }
          this.playerName.Text = leaderBoardEntryData.name;
          NumberFormatInfo nfi = GameEngine.NFI;
          if (this.m_category != -5)
          {
            this.valueLabel.Text = leaderBoardEntryData.value.ToString("N", (IFormatProvider) nfi);
            this.valueLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
          }
          else
          {
            int rank = leaderBoardEntryData.value / 100;
            int rankSubLevel = leaderBoardEntryData.value % 100;
            if (rank >= 22)
            {
              rank = 22;
              rankSubLevel = leaderBoardEntryData.value - 2200;
            }
            this.valueLabel.Text = Rankings.getRankingName(GameEngine.Instance.LocalWorldData, rank, rankSubLevel, leaderBoardEntryData.male);
            this.valueLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            if (Program.mySettings.LanguageIdent == "it")
            {
              this.valueLabel.Position = new Point(269, 0);
              this.valueLabel.Size = new Size(120, this.backgroundImage.Height);
            }
          }
          this.m_entryID = leaderBoardEntryData.entryID;
          this.positionLabel.Text = leaderBoardEntryData.standing.ToString("N", (IFormatProvider) nfi);
          if (leaderBoardEntryData.house > 0)
          {
            this.houseImage.Visible = true;
            this.houseImage.Image = (Image) GFXLibrary.getHouseFlag(leaderBoardEntryData.house);
          }
          if (playerID < 0 || !this.topEntry)
            return;
          this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(playerID, 25, 28);
          if (this.shieldImage.Image == null)
            return;
          this.shieldImage.Position = new Point(16, 8);
          this.shieldImage.Visible = true;
          this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
          this.shieldImage.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
        }
        else
        {
          this.playerName.Text = "";
          this.valueLabel.Text = "";
          this.positionLabel.Text = "";
          this.houseImage.Visible = false;
        }
      }

      public void setAsTopEntry() => this.topEntry = true;

      public void update()
      {
        if (this.m_validData || GameEngine.Instance.World.getLeaderboardEntry(this.m_category, this.m_position, StatsPanel.NUM_VISIBLE_LINES) == null)
          return;
        this.init(this.m_category, this.m_position, this.m_screenLine);
      }

      public void lineClicked()
      {
        GameEngine.Instance.playInterfaceSound("StatsPanel_entry_clicked");
        switch (this.m_category)
        {
          case -4:
            break;
          case -3:
            if (this.m_entryID < 0)
              break;
            InterfaceMgr.Instance.showHousePanel(this.m_entryID);
            break;
          case -2:
            if (this.m_entryID < 0)
              break;
            InterfaceMgr.Instance.showFactionPanel(this.m_entryID);
            break;
          default:
            if (this.m_entryID < 0)
              break;
            InterfaceMgr.Instance.changeTab(0);
            InterfaceMgr.Instance.showUserInfoScreen(new WorldMap.CachedUserInfo()
            {
              userID = this.m_entryID
            });
            break;
        }
      }

      public void lineRightClicked()
      {
        if (this.csd == null || this.csd.ClickedControl == null || this.csd.ClickedControl.Parent == null)
          return;
        CustomSelfDrawPanel.CSDControl parent = this.csd.ClickedControl.Parent;
        while (parent != null && parent.GetType() != typeof (StatsPanel.StatsEntry))
          parent = parent.Parent;
        if (parent == null)
          return;
        GameEngine.Instance.playInterfaceSound("StatsPanel_entry_clicked");
        Clipboard.SetText(((StatsPanel.StatsEntry) parent).playerName.Text);
      }
    }

    public class SelfStatsEntry : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel categoryLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel amountLine = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage sectionImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel changeLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage changeImage = new CustomSelfDrawPanel.CSDImage();
      private int m_position = -1000;
      private int m_category = -1000;
      private StatsPanel m_parent;

      public void init(int row, StatsPanel parent)
      {
        this.m_parent = parent;
        this.m_position = row;
        LeaderBoardSelfRankings leaderboardBestRanking = GameEngine.Instance.World.getLeaderboardBestRanking(row);
        this.clearControls();
        this.m_category = -1000;
        if (leaderboardBestRanking == null || leaderboardBestRanking.value == 0)
          return;
        this.backgroundImage.Image = (row & 1) != 0 ? (Image) GFXLibrary.int_statsscreen_secondinset_bar_lighter : (Image) GFXLibrary.int_statsscreen_secondinset_bar_darker;
        this.m_category = leaderboardBestRanking.category;
        this.backgroundImage.Position = new Point(0, 0);
        this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        NumberFormatInfo nfi = GameEngine.NFI;
        this.amountLine.Text = GameEngine.Instance.World.GetLeaderboardCategoryScore(leaderboardBestRanking.category, leaderboardBestRanking.value, GameEngine.Instance.LocalWorldData);
        this.sectionImage.Image = (Image) GFXLibrary.GetLeaderboardCategoryIcon(leaderboardBestRanking.category);
        this.sectionImage.Position = new Point(15, 14);
        this.sectionImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.sectionImage);
        if (leaderboardBestRanking.oldPlace != leaderboardBestRanking.place)
        {
          string str;
          if (leaderboardBestRanking.oldPlace >= leaderboardBestRanking.place)
          {
            str = (leaderboardBestRanking.oldPlace - leaderboardBestRanking.place).ToString();
            this.changeImage.Image = (Image) GFXLibrary.arrow_up;
          }
          else
          {
            str = (leaderboardBestRanking.place - leaderboardBestRanking.oldPlace).ToString();
            this.changeImage.Image = (Image) GFXLibrary.arrow_down;
          }
          this.changeImage.Position = new Point(333, 12);
          this.changeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.changeImage);
          this.changeLabel.Text = str;
          this.changeLabel.Color = ARGBColors.White;
          this.changeLabel.Position = new Point(0, 12);
          this.changeLabel.Size = new Size(333, 25);
          this.changeLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
          this.changeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
          this.changeLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.changeLabel);
        }
        this.categoryLabel.Text = StatsPanel.getCategoryTitle(leaderboardBestRanking.category) + " - " + SK.Text("Stats_Ranked", "Ranked") + " " + leaderboardBestRanking.place.ToString("N", (IFormatProvider) nfi);
        this.categoryLabel.Color = ARGBColors.White;
        this.categoryLabel.Position = new Point(76, 12);
        this.categoryLabel.Size = new Size(275, 25);
        this.categoryLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.categoryLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.categoryLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.categoryLabel);
        this.amountLine.Color = ARGBColors.White;
        this.amountLine.Position = new Point(76, 37);
        this.amountLine.Size = new Size(275, this.backgroundImage.Height);
        this.amountLine.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.amountLine.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.amountLine.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.amountLine);
        this.invalidate();
      }

      public void update()
      {
      }

      public void lineClicked()
      {
        if (this.m_category == -1000 || this.m_parent == null)
          return;
        GameEngine.Instance.playInterfaceSound("StatsPanel_entry_clicked");
        this.m_parent.changeCategory(this.m_category);
      }
    }
  }
}
