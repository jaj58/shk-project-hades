// Decompiled with JetBrains decompiler
// Type: Kingdoms.ResearchPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ResearchPanel : CustomSelfDrawPanel, IDockableControl
  {
    private const int topMoveDownOffset = 55;
    private const int MAX_RESEARCH_LINES = 30;
    private const int tileSizeX = 150;
    private const int tileSizeY = 110;
    private const int tileBorderX = 40;
    private const int tileBorderY = 40;
    private const int NUM_INDUSTRY_ROWS = 21;
    private const int NUM_INDUSTRY_COLUMNS = 18;
    private const int NUM_MILITARY_ROWS = 24;
    private const int NUM_MILITARY_COLUMNS = 20;
    private const int NUM_FARMING_ROWS = 13;
    private const int NUM_FARMING_COLUMNS = 17;
    private const int NUM_EDUCATION_ROWS = 27;
    private const int NUM_EDUCATION_COLUMNS = 26;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage currentResearchBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage currentResearchBackgroundImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage currentResearchImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage currentResearchingBarImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel currentResearchText = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton currentResearchCancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel currentResearchInfoBox = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel currentResearchInfoBoxHeadingText = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel currentResearchInfoBoxRow1Text = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel currentResearchInfoBoxRow2Text = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel currentResearchInfoBoxRow3Text = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDArea queuedResearchArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage queuedResearchImage1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage queuedResearchImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage queuedResearchImage3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage queuedResearchImage4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage queuedResearchImage5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage queuedResearchImage6 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage queuedResearchImage7 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton queuedResearchButton1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton queuedResearchButton2 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton queuedResearchButton3 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton queuedResearchButton4 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton queuedResearchButton5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton queuedResearchButton6 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton queuedResearchButton7 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel queuedResearchNoPremiumText = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel timeInfoBox = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDHorzProgressBar timeProgressBar = new CustomSelfDrawPanel.CSDHorzProgressBar();
    private CustomSelfDrawPanel.CSDLabel timeProgressText = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel buyPointInfoBox = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage buyPointGold = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel buyPointText = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton buyPointButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel pointsInfoBox = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel pointsText = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage scrollPanelImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDDragPanel dragOverlay = new CustomSelfDrawPanel.CSDDragPanel();
    private CustomSelfDrawPanel.CSDControl dragOverlay2 = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDImage scrollPanelTopLeftOverlay = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage scrollPanelTopMiddleOverlay = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage scrollPanelTopRightOverlay = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage scrollPanelLeftOverlay = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage scrollPanelRightOverlay = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage scrollPanelBottomLeftOverlay = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage scrollPanelBottomMiddleOverlay = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage scrollPanelBottomRightOverlay = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton tab1Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tab2Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tab3Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tab4Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tab5Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tabModeTreeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tabModeListButton = new CustomSelfDrawPanel.CSDButton();
    private CardBarGDI cardbar = new CardBarGDI();
    private int lastResearchTab = -1;
    private CustomSelfDrawPanel.CSDButton[] startResearchButtons = new CustomSelfDrawPanel.CSDButton[30];
    private CustomSelfDrawPanel.CSDImage[] startResearchImages = new CustomSelfDrawPanel.CSDImage[30];
    private CustomSelfDrawPanel.CSDLabel[] startResearchHeader = new CustomSelfDrawPanel.CSDLabel[30];
    private CustomSelfDrawPanel.CSDLabel[] startResearchText1 = new CustomSelfDrawPanel.CSDLabel[30];
    private CustomSelfDrawPanel.CSDLabel[] startResearchText2 = new CustomSelfDrawPanel.CSDLabel[30];
    private CustomSelfDrawPanel.CSDImage[] startResearchDotsBack = new CustomSelfDrawPanel.CSDImage[30];
    private CustomSelfDrawPanel.CSDImage[] startResearchDots = new CustomSelfDrawPanel.CSDImage[30];
    private CustomSelfDrawPanel.CSDImage[] startResearchDotsYellow = new CustomSelfDrawPanel.CSDImage[30];
    private CustomSelfDrawPanel.CSDImage[] startResearchOpenBackground = new CustomSelfDrawPanel.CSDImage[30];
    private CustomSelfDrawPanel.CSDImage[] startResearchOpenResearch = new CustomSelfDrawPanel.CSDImage[30];
    private CustomSelfDrawPanel.CSDImage[] startResearchOpenBuilding = new CustomSelfDrawPanel.CSDImage[30];
    private CustomSelfDrawPanel.CSDImage[] startResearchOpenResearchOverlay = new CustomSelfDrawPanel.CSDImage[30];
    private CustomSelfDrawPanel.CSDLabel[] startResearchOpenResearchOverlayLabel = new CustomSelfDrawPanel.CSDLabel[30];
    private CustomSelfDrawPanel.CSDImage[] startResearchShield = new CustomSelfDrawPanel.CSDImage[30];
    private CustomSelfDrawPanel.CSDLabel[] startResearchShieldNumber = new CustomSelfDrawPanel.CSDLabel[30];
    private CustomSelfDrawPanel.CSDVertScrollBar startResearchScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDLabel startResearchHeaderMain = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel startResearchHeaderResearchOpen = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel startResearchHeaderBuildingOpen = new CustomSelfDrawPanel.CSDLabel();
    public static int TUTORIAL_artsTabPos = -10000;
    private int lastTab;
    private int tabType;
    private MyMessageBoxPopUp cancelResearchPopup;
    private bool forceUpdate;
    private int tooltipToShow = -1;
    private ResearchData lastData;
    private ResearchData lastDataQueued;
    private bool researchAllowed;
    private int selectedQueueSlot = -1;
    private int lastScrollXPos;
    private int lastScrollYPos;
    private Size realScrollImageSize = new Size();
    private double m_windowScale = 1.0;
    private int m_windowScaleNotch;
    private double[] windowScalingValues = new double[2]
    {
      1.0,
      0.5
    };
    private int[] industryLayout = new int[42]
    {
      -1,
      0,
      0,
      1,
      1,
      1,
      4,
      1,
      3,
      5,
      2,
      2,
      6,
      1,
      38,
      9,
      39,
      8,
      5,
      7,
      69,
      6,
      7,
      5,
      9,
      4,
      8,
      3,
      63,
      2,
      13,
      1,
      14,
      6,
      15,
      5,
      16,
      4,
      17,
      3,
      18,
      2
    };
    private int[] farmingLayout = new int[26]
    {
      -2,
      0,
      66,
      1,
      64,
      1,
      65,
      1,
      68,
      1,
      12,
      8,
      10,
      7,
      71,
      5,
      70,
      3,
      67,
      2,
      61,
      1,
      11,
      6,
      62,
      3
    };
    private int[] militaryLayout = new int[48]
    {
      -3,
      0,
      19,
      1,
      84,
      7,
      20,
      6,
      57,
      5,
      21,
      4,
      22,
      3,
      23,
      2,
      24,
      1,
      26,
      6,
      27,
      5,
      28,
      4,
      29,
      3,
      30,
      2,
      75,
      1,
      76,
      3,
      37,
      2,
      78,
      1,
      85,
      2,
      86,
      1,
      74,
      1,
      25,
      2,
      82,
      4,
      73,
      3
    };
    private int[] educationLayout = new int[54]
    {
      -4,
      0,
      32,
      1,
      33,
      4,
      34,
      3,
      35,
      6,
      36,
      5,
      40,
      2,
      42,
      8,
      43,
      7,
      44,
      6,
      41,
      5,
      45,
      4,
      46,
      3,
      59,
      1,
      48,
      5,
      58,
      6,
      49,
      4,
      50,
      17,
      51,
      16,
      53,
      14,
      54,
      13,
      56,
      12,
      55,
      11,
      52,
      9,
      72,
      10,
      47,
      3,
      60,
      2
    };
    private int[] educationLayout2 = new int[52]
    {
      -4,
      0,
      32,
      1,
      33,
      4,
      34,
      3,
      35,
      6,
      36,
      5,
      40,
      2,
      42,
      8,
      43,
      7,
      44,
      6,
      41,
      5,
      45,
      4,
      46,
      3,
      59,
      1,
      48,
      5,
      58,
      6,
      49,
      4,
      50,
      17,
      51,
      16,
      54,
      13,
      56,
      12,
      55,
      11,
      52,
      9,
      72,
      10,
      47,
      3,
      60,
      2
    };
    private bool rowsCreated;
    private CustomSelfDrawPanel.CSDImage[][] industryRows = new CustomSelfDrawPanel.CSDImage[21][];
    private CustomSelfDrawPanel.CSDImage[][] militaryRows = new CustomSelfDrawPanel.CSDImage[24][];
    private CustomSelfDrawPanel.CSDImage[][] farmingRows = new CustomSelfDrawPanel.CSDImage[13][];
    private CustomSelfDrawPanel.CSDImage[][] educationRows = new CustomSelfDrawPanel.CSDImage[27][];
    private CustomSelfDrawPanel.CSDImage[][] educationRows2 = new CustomSelfDrawPanel.CSDImage[26][];
    private List<CustomSelfDrawPanel.CSDImage> imageCache = new List<CustomSelfDrawPanel.CSDImage>();
    private int curImageID;
    private List<CustomSelfDrawPanel.CSDLabel> labelCache = new List<CustomSelfDrawPanel.CSDLabel>();
    private int curLabelID;
    private DockableControl dockableControl;
    private IContainer components;

    public ResearchPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.NoDrawBackground = true;
    }

    public void init()
    {
      this.clearControls();
      this.Size = InterfaceMgr.Instance.getMainWindowSize();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_001;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Tile = true;
      this.mainBackgroundImage.ClipRect = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
      this.mainBackgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.backgroundClick));
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 17, new Point(this.Width - 44, 3));
      int num1 = 42;
      this.currentResearchBackgroundImage.Image = (Image) GFXLibrary.ill_back_bline_0000;
      this.currentResearchBackgroundImage.Position = new Point(19, 2 + num1 + 20);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchBackgroundImage);
      this.currentResearchBackgroundImage2.Image = (Image) GFXLibrary.research_ill_none;
      this.currentResearchBackgroundImage2.Position = new Point(19, 2 + num1 + 20);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchBackgroundImage2);
      this.currentResearchImage.Position = new Point(4, 8);
      this.currentResearchImage.Visible = false;
      this.currentResearchBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchImage);
      this.currentResearchingBarImage.Image = (Image) GFXLibrary.ill_back_green_textback;
      this.currentResearchingBarImage.Position = new Point(4, 68);
      this.currentResearchBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchingBarImage);
      this.currentResearchText.Text = "";
      this.currentResearchText.Color = ARGBColors.Black;
      this.currentResearchText.Position = new Point(0, 0);
      this.currentResearchText.Size = new Size(this.currentResearchingBarImage.Width, this.currentResearchingBarImage.Height);
      this.currentResearchText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.currentResearchText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.currentResearchingBarImage.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchText);
      this.currentResearchCancelButton.ImageNorm = (Image) GFXLibrary.techtree_button_normal;
      this.currentResearchCancelButton.ImageOver = (Image) GFXLibrary.techtree_button_over;
      this.currentResearchCancelButton.ImageClick = (Image) GFXLibrary.techtree_button_in;
      this.currentResearchCancelButton.Position = new Point(17, 123 + num1);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchCancelButton);
      this.currentResearchCancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.currentResearchCancelButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.currentResearchCancelButton.TextYOffset = 1;
      this.currentResearchCancelButton.Text.Color = ARGBColors.Black;
      this.currentResearchCancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelResearchClick), "ResearchPanel_cancel");
      this.currentResearchInfoBox.Size = new Size(this.Width - 450 + 2 + 15 + 15, 91);
      this.currentResearchInfoBox.Position = new Point(179, 27 + num1);
      this.currentResearchInfoBox.Create((Image) GFXLibrary.tech_tree_inset_tall_left, (Image) GFXLibrary.tech_tree_inset_tall_mid, (Image) GFXLibrary.tech_tree_inset_tall_right);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchInfoBox);
      this.currentResearchInfoBoxHeadingText.Text = "";
      this.currentResearchInfoBoxHeadingText.Color = Color.FromArgb(254, 230, 192);
      this.currentResearchInfoBoxHeadingText.Position = new Point(20, 8);
      this.currentResearchInfoBoxHeadingText.Size = new Size(this.currentResearchInfoBox.Width - 40, this.currentResearchingBarImage.Height);
      this.currentResearchInfoBoxHeadingText.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.currentResearchInfoBoxHeadingText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.currentResearchInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchInfoBoxHeadingText);
      this.currentResearchInfoBoxRow1Text.Text = "Row of Text 1";
      this.currentResearchInfoBoxRow1Text.Color = Color.FromArgb(254, 230, 192);
      this.currentResearchInfoBoxRow1Text.Position = new Point(20, 30);
      this.currentResearchInfoBoxRow1Text.Size = new Size(this.currentResearchInfoBox.Width - 40, this.currentResearchingBarImage.Height);
      this.currentResearchInfoBoxRow1Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.currentResearchInfoBoxRow1Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.currentResearchInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchInfoBoxRow1Text);
      this.currentResearchInfoBoxRow2Text.Text = "Row of Text 2";
      this.currentResearchInfoBoxRow2Text.Color = Color.FromArgb(254, 230, 192);
      this.currentResearchInfoBoxRow2Text.Position = new Point(20, 49);
      this.currentResearchInfoBoxRow2Text.Size = new Size(this.currentResearchInfoBox.Width - 40, this.currentResearchingBarImage.Height);
      this.currentResearchInfoBoxRow2Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.currentResearchInfoBoxRow2Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.currentResearchInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchInfoBoxRow2Text);
      this.currentResearchInfoBoxRow3Text.Text = "Row of Text 3";
      this.currentResearchInfoBoxRow3Text.Color = Color.FromArgb(254, 230, 192);
      this.currentResearchInfoBoxRow3Text.Position = new Point(20, 66);
      this.currentResearchInfoBoxRow3Text.Size = new Size(this.currentResearchInfoBox.Width - 40, this.currentResearchingBarImage.Height);
      this.currentResearchInfoBoxRow3Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.currentResearchInfoBoxRow3Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.currentResearchInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.currentResearchInfoBoxRow3Text);
      this.queuedResearchArea.Position = new Point(5, 56);
      int x1 = 5;
      this.queuedResearchArea.Size = new Size(500, 50);
      this.currentResearchInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchArea);
      this.queuedResearchImage1.Image = (Image) GFXLibrary.research_ill_wine_production;
      this.queuedResearchImage1.Size = new Size(this.queuedResearchImage1.Size.Width / 2, this.queuedResearchImage1.Size.Height / 2);
      this.queuedResearchImage1.Position = new Point(x1, 5);
      this.queuedResearchImage1.Visible = false;
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchImage1);
      this.queuedResearchImage2.Image = (Image) GFXLibrary.research_ill_wine_production;
      this.queuedResearchImage2.Size = new Size(this.queuedResearchImage2.Size.Width / 2, this.queuedResearchImage2.Size.Height / 2);
      this.queuedResearchImage2.Position = new Point(x1 + 81, 5);
      this.queuedResearchImage2.Visible = false;
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchImage2);
      this.queuedResearchImage3.Image = (Image) GFXLibrary.research_ill_wine_production;
      this.queuedResearchImage3.Size = new Size(this.queuedResearchImage3.Size.Width / 2, this.queuedResearchImage3.Size.Height / 2);
      this.queuedResearchImage3.Position = new Point(x1 + 162, 5);
      this.queuedResearchImage3.Visible = false;
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchImage3);
      this.queuedResearchImage4.Image = (Image) GFXLibrary.research_ill_wine_production;
      this.queuedResearchImage4.Size = new Size(this.queuedResearchImage4.Size.Width / 2, this.queuedResearchImage4.Size.Height / 2);
      this.queuedResearchImage4.Position = new Point(x1 + 243, 5);
      this.queuedResearchImage4.Visible = false;
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchImage4);
      this.queuedResearchImage5.Image = (Image) GFXLibrary.research_ill_wine_production;
      this.queuedResearchImage5.Size = new Size(this.queuedResearchImage5.Size.Width / 2, this.queuedResearchImage5.Size.Height / 2);
      this.queuedResearchImage5.Position = new Point(x1 + 324, 5);
      this.queuedResearchImage5.Visible = false;
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchImage5);
      this.queuedResearchButton1.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
      this.queuedResearchButton1.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
      this.queuedResearchButton1.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
      this.queuedResearchButton1.Position = new Point(x1 - 1, 4);
      this.queuedResearchButton1.Data = 0;
      this.queuedResearchButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchButton1);
      this.queuedResearchButton2.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
      this.queuedResearchButton2.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
      this.queuedResearchButton2.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
      this.queuedResearchButton2.Position = new Point(x1 - 1 + 81, 4);
      this.queuedResearchButton2.Data = 1;
      this.queuedResearchButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchButton2);
      this.queuedResearchButton3.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
      this.queuedResearchButton3.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
      this.queuedResearchButton3.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
      this.queuedResearchButton3.Position = new Point(x1 - 1 + 162, 4);
      this.queuedResearchButton3.Data = 2;
      this.queuedResearchButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchButton3);
      this.queuedResearchButton4.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
      this.queuedResearchButton4.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
      this.queuedResearchButton4.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
      this.queuedResearchButton4.Position = new Point(x1 - 1 + 243, 4);
      this.queuedResearchButton4.Data = 3;
      this.queuedResearchButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchButton4);
      this.queuedResearchButton5.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
      this.queuedResearchButton5.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
      this.queuedResearchButton5.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
      this.queuedResearchButton5.Position = new Point(x1 - 1 + 324, 4);
      this.queuedResearchButton5.Data = 4;
      this.queuedResearchButton5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchButton5);
      this.queuedResearchNoPremiumText.Text = SK.Text("Research_Queue_Premium", "Research Queue requires a Premium Token");
      this.queuedResearchNoPremiumText.Color = Color.FromArgb(254, 230, 192);
      int width = 148 + (this.Width - 992);
      if (width < 175)
      {
        width = Program.mySettings.LanguageIdent == "de" || Program.mySettings.LanguageIdent == "fr" ? 184 : 175;
        this.queuedResearchNoPremiumText.Position = new Point(399, -10);
      }
      else
        this.queuedResearchNoPremiumText.Position = new Point(409, -10);
      this.queuedResearchNoPremiumText.Size = new Size(width, 58);
      this.queuedResearchNoPremiumText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.queuedResearchNoPremiumText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.queuedResearchArea.addControl((CustomSelfDrawPanel.CSDControl) this.queuedResearchNoPremiumText);
      this.timeInfoBox.Size = new Size(this.Width - 450 + 2 + 15 + 15, 35);
      this.timeInfoBox.Position = new Point(179, 125 + num1);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.timeInfoBox);
      this.timeInfoBox.Create((Image) GFXLibrary.tech_tree_inset_left, (Image) GFXLibrary.tech_tree_inset_mid, (Image) GFXLibrary.tech_tree_inset_right);
      this.timeProgressBar.Size = new Size(this.timeInfoBox.Size.Width - 14, 22);
      this.timeProgressBar.Position = new Point(7, 7);
      this.timeInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.timeProgressBar);
      this.timeProgressBar.Offset = new Point(5, 3);
      this.timeProgressBar.Create((Image) GFXLibrary.tech_tree_progbar_olive_left, (Image) GFXLibrary.tech_tree_progbar_olive_mid, (Image) GFXLibrary.tech_tree_progbar_olive_right, (Image) GFXLibrary.tech_tree_progbar_green_left, (Image) GFXLibrary.tech_tree_progbar_green_mid, (Image) GFXLibrary.tech_tree_progbar_green_right);
      this.timeProgressText.Text = "";
      this.timeProgressText.Color = ARGBColors.Black;
      this.timeProgressText.Position = new Point(0, 0);
      this.timeProgressText.Size = new Size(this.timeInfoBox.Width, this.timeInfoBox.Height);
      this.timeProgressText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.timeProgressText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.timeInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.timeProgressText);
      this.buyPointInfoBox.Size = new Size(208, 91);
      this.buyPointInfoBox.Position = new Point(this.Width - 244 + 15, 27 + num1);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.buyPointInfoBox);
      this.buyPointInfoBox.Create((Image) GFXLibrary.tech_tree_inset_tall_left, (Image) GFXLibrary.tech_tree_inset_tall_mid, (Image) GFXLibrary.tech_tree_inset_tall_right);
      this.buyPointInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.buyPointButton);
      this.buyPointGold.Image = (Image) GFXLibrary.com_32_money;
      this.buyPointGold.Position = new Point(48, 8);
      this.buyPointInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.buyPointGold);
      this.buyPointText.Text = "";
      this.buyPointText.Color = Color.FromArgb(254, 230, 192);
      this.buyPointText.Position = new Point(97, -6);
      this.buyPointText.Size = new Size(100, 60);
      this.buyPointText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.buyPointText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.buyPointInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.buyPointText);
      this.buyPointButton.ImageNorm = (Image) GFXLibrary.techtree_button_normal;
      this.buyPointButton.ImageOver = (Image) GFXLibrary.techtree_button_over;
      this.buyPointButton.ImageClick = (Image) GFXLibrary.techtree_button_in;
      this.buyPointButton.Position = new Point(26, 44);
      this.buyPointButton.Text.Text = SK.Text("Research_Buy_Point", "Buy Point");
      this.buyPointButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.buyPointButton.TextYOffset = 1;
      this.buyPointButton.Text.Color = ARGBColors.Black;
      this.buyPointButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buyPointClick), "ResearchPanel_buy_point");
      this.pointsInfoBox.Size = new Size(208, 35);
      this.pointsInfoBox.Position = new Point(this.Width - 244 + 15, 125 + num1);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsInfoBox);
      this.pointsInfoBox.Create((Image) GFXLibrary.tech_tree_inset_left, (Image) GFXLibrary.tech_tree_inset_mid, (Image) GFXLibrary.tech_tree_inset_right);
      this.pointsText.Color = Color.FromArgb(254, 230, 192);
      this.pointsText.Text = "";
      this.pointsText.Position = new Point(0, 0);
      this.pointsText.Size = new Size(this.pointsInfoBox.Width, this.pointsInfoBox.Height);
      this.pointsText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.pointsText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.pointsInfoBox.addControl((CustomSelfDrawPanel.CSDControl) this.pointsText);
      this.scrollPanelImage.Image = (Image) GFXLibrary.body_background_002;
      this.scrollPanelImage.Tile = true;
      this.scrollPanelImage.Position = new Point(20, 242);
      this.scrollPanelImage.Size = new Size(this.Width - 40, this.Height - 205 - 55);
      this.scrollPanelImage.ClipRect = new Rectangle(new Point(0, 0), new Size(this.Width - 40, this.Height - 205 - 55));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollPanelImage);
      this.dragOverlay.Position = this.scrollPanelImage.Position;
      this.dragOverlay.Size = this.scrollPanelImage.Size;
      this.dragOverlay.Visible = true;
      this.dragOverlay.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.windowDragged));
      this.dragOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.dragWindowMouseWheel));
      this.dragOverlay.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.dragWindowMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.dragWindowMouseLeave));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.dragOverlay);
      this.dragOverlay2.Position = this.scrollPanelImage.Position;
      this.dragOverlay2.Size = this.scrollPanelImage.Size;
      this.dragOverlay2.Visible = false;
      this.dragOverlay2.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.listWindowMouseWheel));
      this.mainBackgroundImage.addControl(this.dragOverlay2);
      int y = 242;
      int x2 = 20;
      int num2 = this.Width - 20;
      int num3 = this.Height - 205 - 55 + y;
      this.scrollPanelTopLeftOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_topleft;
      this.scrollPanelTopLeftOverlay.Position = new Point(x2, y);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollPanelTopLeftOverlay);
      this.scrollPanelTopRightOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_topright;
      this.scrollPanelTopRightOverlay.Position = new Point(num2 - this.scrollPanelTopRightOverlay.Image.Width, y);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollPanelTopRightOverlay);
      this.scrollPanelTopMiddleOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_top;
      this.scrollPanelTopMiddleOverlay.Position = new Point(x2 + this.scrollPanelTopLeftOverlay.Image.Width, y);
      this.scrollPanelTopMiddleOverlay.Size = new Size(num2 - x2 - this.scrollPanelTopRightOverlay.Image.Width - this.scrollPanelTopLeftOverlay.Image.Width, this.scrollPanelTopMiddleOverlay.Image.Height);
      this.scrollPanelTopMiddleOverlay.ClipRect = new Rectangle(0, 0, this.scrollPanelTopMiddleOverlay.Size.Width, this.scrollPanelTopMiddleOverlay.Size.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollPanelTopMiddleOverlay);
      this.scrollPanelBottomLeftOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_bottomleft;
      this.scrollPanelBottomLeftOverlay.Position = new Point(x2, num3 - this.scrollPanelBottomLeftOverlay.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollPanelBottomLeftOverlay);
      this.scrollPanelLeftOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_left;
      this.scrollPanelLeftOverlay.Position = new Point(x2, y + this.scrollPanelTopLeftOverlay.Image.Height);
      this.scrollPanelLeftOverlay.Size = new Size(this.scrollPanelLeftOverlay.Image.Width, num3 - y - this.scrollPanelTopLeftOverlay.Image.Height - this.scrollPanelBottomLeftOverlay.Image.Height);
      this.scrollPanelLeftOverlay.ClipRect = new Rectangle(0, 0, this.scrollPanelLeftOverlay.Size.Width, this.scrollPanelLeftOverlay.Size.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollPanelLeftOverlay);
      this.scrollPanelBottomRightOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_bottomright;
      this.scrollPanelBottomRightOverlay.Position = new Point(num2 - this.scrollPanelBottomRightOverlay.Image.Width, num3 - this.scrollPanelBottomRightOverlay.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollPanelBottomRightOverlay);
      this.scrollPanelRightOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_right;
      this.scrollPanelRightOverlay.Position = new Point(num2 - this.scrollPanelRightOverlay.Image.Width, y + this.scrollPanelTopRightOverlay.Image.Height);
      this.scrollPanelRightOverlay.Size = new Size(this.scrollPanelRightOverlay.Image.Width, num3 - y - this.scrollPanelTopRightOverlay.Image.Height - this.scrollPanelBottomRightOverlay.Image.Height);
      this.scrollPanelRightOverlay.ClipRect = new Rectangle(0, 0, this.scrollPanelRightOverlay.Size.Width, this.scrollPanelRightOverlay.Size.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollPanelRightOverlay);
      this.scrollPanelBottomMiddleOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_bottom;
      this.scrollPanelBottomMiddleOverlay.Position = new Point(x2 + this.scrollPanelBottomLeftOverlay.Image.Width, num3 - this.scrollPanelBottomMiddleOverlay.Image.Height);
      this.scrollPanelBottomMiddleOverlay.Size = new Size(num2 - x2 - this.scrollPanelBottomRightOverlay.Image.Width - this.scrollPanelBottomLeftOverlay.Image.Width, this.scrollPanelBottomMiddleOverlay.Image.Height);
      this.scrollPanelBottomMiddleOverlay.ClipRect = new Rectangle(0, 0, this.scrollPanelBottomMiddleOverlay.Size.Width, this.scrollPanelBottomMiddleOverlay.Size.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollPanelBottomMiddleOverlay);
      this.tab1Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_01_normal;
      this.tab1Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_01_highlight;
      this.tab1Button.Text.Text = SK.Text("Research_Industry", "Industry");
      this.tab1Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab1Button.Position = new Point(x2, 216);
      this.tab1Button.TextYOffset = -13;
      this.tab1Button.Text.Color = Color.FromArgb(205, 157, 49);
      this.tab1Button.Data = 0;
      this.tab1Button.ClickArea = new Rectangle(0, 0, this.tab1Button.ImageNorm.Width, 25);
      this.tab1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "ResearchPanel_industry_tab");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tab1Button);
      this.tab2Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
      this.tab2Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
      this.tab2Button.Text.Text = SK.Text("Research_Military", "Military");
      this.tab2Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab2Button.TextYOffset = 0;
      this.tab2Button.Text.Color = Color.FromArgb(205, 157, 49);
      this.tab2Button.Position = new Point(this.tab1Button.Position.X + this.tab1Button.Width + 2, 216);
      this.tab2Button.Data = 1;
      this.tab2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "ResearchPanel_military_tab");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tab2Button);
      this.tab3Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
      this.tab3Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
      this.tab3Button.Text.Text = SK.Text("Research_Farming", "Farming");
      this.tab3Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab3Button.TextYOffset = 0;
      this.tab3Button.Position = new Point(this.tab2Button.Position.X + this.tab2Button.Width + 2, 216);
      this.tab3Button.Text.Color = Color.FromArgb(205, 157, 49);
      this.tab3Button.Data = 2;
      this.tab3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "ResearchPanel_farming_tab");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tab3Button);
      this.tab4Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
      this.tab4Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
      this.tab4Button.Text.Text = SK.Text("Research_Education", "Education");
      this.tab4Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tab4Button.TextYOffset = 0;
      this.tab4Button.Position = new Point(this.tab3Button.Position.X + this.tab3Button.Width + 2, 216);
      this.tab4Button.Text.Color = Color.FromArgb(205, 157, 49);
      this.tab4Button.Data = 3;
      this.tab4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "ResearchPanel_education_tab");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tab4Button);
      this.tabModeTreeButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_tree_normal;
      this.tabModeTreeButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_tree_normal;
      this.tabModeTreeButton.Position = new Point(num2 - this.tabModeTreeButton.Width, 216);
      this.tabModeTreeButton.Data = 1;
      this.tabModeTreeButton.ClickArea = new Rectangle(0, 0, this.tabModeTreeButton.Width, 25);
      this.tabModeTreeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabModeClicked), "ResearchPanel_tree_mode");
      this.tabModeTreeButton.CustomTooltipID = 301;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tabModeTreeButton);
      this.tabModeListButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_list_normal;
      this.tabModeListButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_list_normal;
      this.tabModeListButton.Position = new Point(this.tabModeTreeButton.Position.X - this.tabModeListButton.Width - 2, 216);
      this.tabModeListButton.Data = 0;
      this.tabModeListButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabModeClicked), "ResearchPanel_list_mode");
      this.tabModeListButton.CustomTooltipID = 300;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tabModeListButton);
      this.cardbar.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardbar);
      this.cardbar.init(12);
      this.manageTabs(this.lastTab);
      this.forceUpdate = true;
    }

    private BaseImage getBuildingGFX(int building)
    {
      switch (building)
      {
        case 6:
          return GFXLibrary.r_building_panel_bld_icon_ind_woodcutters_hut;
        case 7:
          return GFXLibrary.r_building_panel_bld_icon_ind_stone_quarry;
        case 8:
          return GFXLibrary.r_building_panel_bld_icon_ind_iron_mine;
        case 9:
          return GFXLibrary.r_building_panel_bld_icon_ind_pitch_rig;
        case 12:
          return GFXLibrary.r_building_panel_bld_icon_food_brewery;
        case 13:
          return GFXLibrary.r_building_panel_bld_icon_food_apple_orchard;
        case 14:
          return GFXLibrary.r_building_panel_bld_icon_food_bakery;
        case 15:
          return GFXLibrary.r_building_panel_bld_icon_food_vegetable_farm;
        case 16:
          return GFXLibrary.r_building_panel_bld_icon_food_pig_farm;
        case 17:
          return GFXLibrary.r_building_panel_bld_icon_food_dairy_farm;
        case 18:
          return GFXLibrary.r_building_panel_bld_icon_food_fishing_jetty;
        case 19:
          return GFXLibrary.r_building_panel_bld_icon_hon_tailers_workshop;
        case 21:
          return GFXLibrary.r_building_panel_bld_icon_hon_carpenters_workshop;
        case 22:
          return GFXLibrary.r_building_panel_bld_icon_hon_hunters_hut;
        case 23:
          return GFXLibrary.r_building_panel_bld_icon_hon_salt_pan;
        case 24:
          return GFXLibrary.r_building_panel_bld_icon_hon_spice_docs;
        case 25:
          return GFXLibrary.r_building_panel_bld_icon_hon_silk_docs;
        case 26:
          return GFXLibrary.r_building_panel_bld_icon_hon_metalworks_workshop;
        case 28:
          return GFXLibrary.r_building_panel_bld_icon_mil_pole_turner;
        case 29:
          return GFXLibrary.r_building_panel_bld_icon_mil_fletcher;
        case 30:
          return GFXLibrary.r_building_panel_bld_icon_mil_blacksmith;
        case 31:
          return GFXLibrary.r_building_panel_bld_icon_mil_armourer;
        case 32:
          return GFXLibrary.r_building_panel_bld_icon_mil_siege_workshop;
        case 33:
          return GFXLibrary.r_building_panel_bld_icon_hon_vinyard;
        case 34:
          return GFXLibrary.r_building_panel_bld_civ_rel_small_church;
        case 35:
          return GFXLibrary.r_building_panel_bld_icon_food_inn;
        case 36:
          return GFXLibrary.r_building_panel_bld_civ_rel_medium_church;
        case 37:
          return GFXLibrary.r_building_panel_bld_civ_rel_large_church;
        case 38:
        case 41:
        case 42:
        case 43:
        case 44:
        case 45:
          return GFXLibrary.r_building_panel_bld_civ_dec_small_garden_01;
        case 49:
        case 50:
        case 51:
          return GFXLibrary.r_building_panel_bld_civ_dec_large_garden_01png;
        case 54:
        case 55:
        case 56:
        case 57:
          return GFXLibrary.r_building_panel_bld_civ_dec_small_statue_01;
        case 58:
        case 59:
          return GFXLibrary.r_building_panel_bld_civ_dec_large_statue_01;
        case 60:
          return GFXLibrary.r_building_panel_bld_civ_dec_dovecote;
        case 61:
          return GFXLibrary.r_building_panel_bld_jus_stocks;
        case 62:
          return GFXLibrary.r_building_panel_bld_jus_burning_post;
        case 63:
          return GFXLibrary.r_building_panel_bld_jus_gibbet;
        case 64:
          return GFXLibrary.r_building_panel_bld_jus_stretching_rack;
        case 65:
          return GFXLibrary.r_building_panel_bld_ent_maypole;
        case 66:
          return GFXLibrary.r_building_panel_bld_ent_dancing_bear;
        case 67:
          return GFXLibrary.r_building_panel_bld_ent_theatre;
        case 68:
          return GFXLibrary.r_building_panel_bld_ent_jesters_court;
        case 69:
          return GFXLibrary.r_building_panel_bld_ent_troubadours_arbor;
        case 70:
        case 71:
        case 72:
        case 73:
          return GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_01;
        case 74:
        case 75:
          return GFXLibrary.r_building_panel_bld_civ_rel_large_shrines_01;
        case 78:
          return GFXLibrary.r_building_panel_bld_icon_ind_market;
        default:
          return (BaseImage) null;
      }
    }

    private BaseImage getCastleGFX(int castlePiece)
    {
      switch (castlePiece)
      {
        case 11:
          return GFXLibrary.r_building_miltary_lookouttower;
        case 12:
          return GFXLibrary.r_building_miltary_smalltower;
        case 13:
          return GFXLibrary.r_building_miltary_largetower;
        case 14:
          return GFXLibrary.r_building_miltary_greattower;
        case 21:
          return GFXLibrary.r_building_miltary_woodtower;
        case 31:
          return GFXLibrary.r_building_miltary_guardhouse;
        case 32:
          return GFXLibrary.r_building_miltary_smelter;
        case 33:
          return GFXLibrary.r_building_miltary_woodwall;
        case 34:
          return GFXLibrary.r_building_miltary_stonewall;
        case 35:
          return GFXLibrary.r_building_miltary_moat;
        case 36:
          return GFXLibrary.r_building_miltary_killingpits;
        case 37:
          return GFXLibrary.r_building_miltary_gatehouse;
        case 60:
          return GFXLibrary.r_bld_icon_mil_guardhouse_2;
        case 61:
          return GFXLibrary.r_bld_icon_mil_guardhouse_3;
        case 62:
          return GFXLibrary.r_bld_icon_mil_guardhouse_4;
        case 70:
          return GFXLibrary.r_building_miltary_peasent;
        case 71:
          return GFXLibrary.r_building_miltary_swordsman;
        case 72:
          return GFXLibrary.r_building_miltary_archer;
        case 73:
          return GFXLibrary.r_building_miltary_pikemen;
        case 74:
          return GFXLibrary.r_building_miltary_catapult;
        default:
          return (BaseImage) null;
      }
    }

    public bool isResearchOnEducationTab() => this.lastResearchTab == 3;

    private void initIndustryTab()
    {
      this.lastResearchTab = 0;
      this.initTab(ResearchData.industryResearchLayout);
    }

    private void initMilitaryTab()
    {
      this.lastResearchTab = 1;
      this.initTab(ResearchData.militaryResearchLayout);
    }

    private void initFarmingTab()
    {
      this.lastResearchTab = 2;
      this.initTab(ResearchData.farmingResearchLayout);
    }

    private void initEducationTab()
    {
      this.lastResearchTab = 3;
      if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
        this.initTab(ResearchData.educationResearchLayout);
      else
        this.initTab(ResearchData.educationResearchLayout2);
    }

    private void initTab(int[] researchlist)
    {
      this.queuedResearchArea.Visible = true;
      this.currentResearchInfoBox.Create((Image) GFXLibrary.research_tech_tree_inset_54_tall_left, (Image) GFXLibrary.research_tech_tree_inset_54_tall_mid, (Image) GFXLibrary.research_tech_tree_inset_54_tall_right);
      this.currentResearchInfoBoxRow2Text.Visible = false;
      this.currentResearchInfoBoxRow3Text.Visible = false;
      this.dragOverlay.Visible = false;
      this.dragOverlay2.Visible = true;
      this.scrollPanelImage.clearControls();
      this.scrollPanelImage.ClipRect = new Rectangle(new Point(0, 0), new Size(this.Width - 40, this.Height - 205 - 55));
      for (int index = 0; index < 30; ++index)
      {
        if (this.startResearchButtons[index] == null)
          this.startResearchButtons[index] = new CustomSelfDrawPanel.CSDButton();
        this.startResearchButtons[index].Visible = false;
        if (this.startResearchImages[index] == null)
          this.startResearchImages[index] = new CustomSelfDrawPanel.CSDImage();
        this.startResearchImages[index].Visible = false;
        if (this.startResearchOpenBackground[index] == null)
          this.startResearchOpenBackground[index] = new CustomSelfDrawPanel.CSDImage();
        this.startResearchOpenBackground[index].Visible = false;
        if (this.startResearchHeader[index] == null)
          this.startResearchHeader[index] = new CustomSelfDrawPanel.CSDLabel();
        this.startResearchHeader[index].Visible = false;
        if (this.startResearchText1[index] == null)
          this.startResearchText1[index] = new CustomSelfDrawPanel.CSDLabel();
        this.startResearchText1[index].Visible = false;
        if (this.startResearchText2[index] == null)
          this.startResearchText2[index] = new CustomSelfDrawPanel.CSDLabel();
        this.startResearchText2[index].Visible = false;
        if (this.startResearchDotsBack[index] == null)
          this.startResearchDotsBack[index] = new CustomSelfDrawPanel.CSDImage();
        this.startResearchDotsBack[index].Visible = false;
        if (this.startResearchDots[index] == null)
          this.startResearchDots[index] = new CustomSelfDrawPanel.CSDImage();
        this.startResearchDots[index].Visible = false;
        if (this.startResearchDotsYellow[index] == null)
          this.startResearchDotsYellow[index] = new CustomSelfDrawPanel.CSDImage();
        this.startResearchDotsYellow[index].Visible = false;
        if (this.startResearchOpenResearch[index] == null)
          this.startResearchOpenResearch[index] = new CustomSelfDrawPanel.CSDImage();
        this.startResearchOpenResearch[index].Visible = false;
        if (this.startResearchOpenResearchOverlay[index] == null)
          this.startResearchOpenResearchOverlay[index] = new CustomSelfDrawPanel.CSDImage();
        this.startResearchOpenResearchOverlay[index].Visible = false;
        if (this.startResearchOpenResearchOverlayLabel[index] == null)
          this.startResearchOpenResearchOverlayLabel[index] = new CustomSelfDrawPanel.CSDLabel();
        this.startResearchOpenResearchOverlayLabel[index].Visible = false;
        if (this.startResearchOpenBuilding[index] == null)
          this.startResearchOpenBuilding[index] = new CustomSelfDrawPanel.CSDImage();
        this.startResearchOpenBuilding[index].Visible = false;
        if (this.startResearchShield[index] == null)
          this.startResearchShield[index] = new CustomSelfDrawPanel.CSDImage();
        this.startResearchShield[index].Visible = false;
        if (this.startResearchShieldNumber[index] == null)
          this.startResearchShieldNumber[index] = new CustomSelfDrawPanel.CSDLabel();
        this.startResearchShieldNumber[index].Visible = false;
      }
      if (this.lastData == null)
        return;
      int rank = GameEngine.Instance.World.getRank();
      int rankSubLevel = GameEngine.Instance.World.getRankSubLevel();
      int rankNeeded = -1;
      int height = 44;
      int y = 34;
      int index1 = 0;
      bool special = false;
      foreach (int index2 in researchlist)
      {
        if ((int) this.lastData.research[index2] < ResearchData.getNumLevels(index2, rank, GameEngine.Instance.LocalWorldData) && this.lastDataQueued.isResearchStepOpen(index2, (int) this.lastDataQueued.research[index2], rank, rankSubLevel, ref rankNeeded, ref special, GameEngine.Instance.LocalWorldData.EraWorld))
        {
          CustomSelfDrawPanel.CSDButton startResearchButton = this.startResearchButtons[index1];
          startResearchButton.Position = new Point(20, y);
          if (this.researchAllowed || index2 != this.lastDataQueued.researchingType)
          {
            startResearchButton.ImageNorm = (Image) GFXLibrary.tech_list_but_big_normal;
            startResearchButton.ImageOver = (Image) GFXLibrary.tech_list_but_big_over;
            startResearchButton.ImageClick = (Image) GFXLibrary.tech_list_but_big_in;
          }
          else
          {
            startResearchButton.ImageNorm = (Image) GFXLibrary.tech_list_but_big_over;
            startResearchButton.ImageOver = (Image) GFXLibrary.tech_list_but_big_over;
            startResearchButton.ImageClick = (Image) GFXLibrary.tech_list_but_big_over;
          }
          startResearchButton.Data = index2;
          startResearchButton.Visible = true;
          startResearchButton.Enabled = this.researchAllowed && (int) this.lastDataQueued.research[index2] < ResearchData.getNumLevels(index2, rank, GameEngine.Instance.LocalWorldData);
          startResearchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.researchClicked), "ResearchPanel_do_research");
          this.scrollPanelImage.addControl((CustomSelfDrawPanel.CSDControl) startResearchButton);
          CustomSelfDrawPanel.CSDImage startResearchImage = this.startResearchImages[index1];
          startResearchImage.Image = (Image) GFXLibrary.getResearchIllustration(index2);
          startResearchImage.Position = new Point(7, 7);
          startResearchImage.Visible = true;
          startResearchButton.addControl((CustomSelfDrawPanel.CSDControl) startResearchImage);
          CustomSelfDrawPanel.CSDLabel control1 = this.startResearchHeader[index1];
          control1.Text = ResearchData.getResearchName(index2);
          control1.Color = ARGBColors.Black;
          control1.Position = new Point(150, 5);
          control1.Size = new Size(startResearchButton.Width - 150, 30);
          control1.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
          control1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          control1.Visible = true;
          startResearchButton.addControl((CustomSelfDrawPanel.CSDControl) control1);
          CustomSelfDrawPanel.CSDLabel control2 = this.startResearchText1[index1];
          control2.Text = ResearchData.getDescriptionText(index2, (int) this.lastDataQueued.research[index2]);
          control2.Color = ARGBColors.Black;
          control2.Size = new Size(startResearchButton.Width - 150, 30);
          control2.Position = new Point(150, 23);
          control2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
          control2.Visible = true;
          startResearchButton.addControl((CustomSelfDrawPanel.CSDControl) control2);
          CustomSelfDrawPanel.CSDLabel control3 = this.startResearchText2[index1];
          control3.Text = ResearchData.getEffectText(index2, (int) this.lastDataQueued.research[index2], GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.SixthAgeWorld, GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1, GameEngine.Instance.LocalWorldData);
          control3.Color = ARGBColors.Black;
          control3.Size = new Size(startResearchButton.Width - 150, 30);
          control3.Position = new Point(150, 53);
          control3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          control3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          control3.Visible = true;
          startResearchButton.addControl((CustomSelfDrawPanel.CSDControl) control3);
          int numLevels = ResearchData.getNumLevels(index2, rank, GameEngine.Instance.LocalWorldData);
          if (numLevels <= 16)
          {
            CustomSelfDrawPanel.CSDImage control4 = this.startResearchDotsBack[index1];
            switch (numLevels)
            {
              case 4:
                control4.Image = (Image) GFXLibrary.tech_tree_dots_black_x04;
                break;
              case 5:
                control4.Image = (Image) GFXLibrary.tech_tree_dots_black_x05;
                break;
              case 8:
                control4.Image = (Image) GFXLibrary.tech_tree_dots_black_x08;
                break;
              case 10:
                control4.Image = (Image) GFXLibrary.tech_tree_dots_black_x10;
                break;
              case 13:
                control4.Image = (Image) GFXLibrary.tech_tree_dots_black_x13;
                break;
              case 15:
                control4.Image = (Image) GFXLibrary.tech_tree_dots_black_x15;
                break;
              default:
                control4.Image = (Image) GFXLibrary.tech_tree_dots_black_x16;
                break;
            }
            control4.Position = new Point(startResearchButton.Width - 10 - control4.Image.Width, 11);
            control4.Visible = true;
            startResearchButton.addControl((CustomSelfDrawPanel.CSDControl) control4);
            int num1 = (int) this.lastDataQueued.research[index2];
            int num2 = (int) this.lastData.research[index2];
            if (num1 > 0 && num1 != num2)
            {
              CustomSelfDrawPanel.CSDImage control5 = this.startResearchDotsYellow[index1];
              control5.Image = (Image) GFXLibrary.tech_tree_dots_yellow_x16;
              control5.Position = new Point(0, 0);
              control5.ClipRect = new Rectangle(0, 0, num1 * 10 - 2, control5.Height);
              control5.Visible = true;
              control4.addControl((CustomSelfDrawPanel.CSDControl) control5);
            }
            if (num2 > 0)
            {
              CustomSelfDrawPanel.CSDImage startResearchDot = this.startResearchDots[index1];
              startResearchDot.Image = (Image) GFXLibrary.tech_tree_dots_green_x16;
              startResearchDot.Position = new Point(0, 0);
              startResearchDot.ClipRect = new Rectangle(0, 0, num2 * 10 - 2, startResearchDot.Height);
              startResearchDot.Visible = true;
              control4.addControl((CustomSelfDrawPanel.CSDControl) startResearchDot);
            }
          }
          CustomSelfDrawPanel.CSDImage control6 = this.startResearchOpenBackground[index1];
          control6.Image = (Image) GFXLibrary.tech_list_insets_X2;
          control6.Position = new Point(656, y);
          control6.Visible = true;
          this.scrollPanelImage.addControl((CustomSelfDrawPanel.CSDControl) control6);
          int openedBuilding = -1;
          int openedCastleBuilding = -1;
          int openedTroop = -1;
          int openedResearch = ResearchData.getOpenedResearch(index2, (int) this.lastDataQueued.research[index2] + 1, GameEngine.Instance.LocalWorldData.Alternate_Ruleset, ref openedBuilding, ref openedCastleBuilding, ref openedTroop);
          if (openedResearch >= 0)
          {
            CustomSelfDrawPanel.CSDImage control7 = this.startResearchOpenResearch[index1];
            control7.Image = (Image) GFXLibrary.getResearchIllustration(openedResearch);
            if (control7.Image != null)
            {
              control7.Tooltip = openedResearch * 1000;
              control7.Position = new Point(8, 7);
              control7.Visible = true;
              control6.addControl((CustomSelfDrawPanel.CSDControl) control7);
              this.lastDataQueued.isResearchStepOpen(openedResearch, 0, rank, rankSubLevel, ref rankNeeded, ref special, GameEngine.Instance.LocalWorldData.EraWorld);
              if (rankNeeded >= 0)
              {
                CustomSelfDrawPanel.CSDImage control8 = this.startResearchShield[index1];
                control8.Image = (Image) GFXLibrary.ill_shield;
                control8.Position = new Point(105, 2);
                control8.Visible = true;
                control7.addControl((CustomSelfDrawPanel.CSDControl) control8);
                CustomSelfDrawPanel.CSDLabel control9 = this.startResearchShieldNumber[index1];
                control9.Text = rankNeeded < 100 ? rankNeeded.ToString() : (rankNeeded - 100 + 1).ToString();
                control9.Color = ARGBColors.White;
                control9.Position = new Point(0, -2);
                control9.Size = control8.Size;
                control9.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                control9.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                control9.Visible = true;
                control8.addControl((CustomSelfDrawPanel.CSDControl) control9);
              }
              CustomSelfDrawPanel.CSDImage control10 = this.startResearchOpenResearchOverlay[index1];
              control10.Image = (Image) GFXLibrary.research_ill_overlay;
              control10.Position = new Point(0, 40);
              control10.Visible = true;
              control10.Alpha = 0.5f;
              control7.addControl((CustomSelfDrawPanel.CSDControl) control10);
              CustomSelfDrawPanel.CSDLabel control11 = this.startResearchOpenResearchOverlayLabel[index1];
              control11.Text = ResearchData.getResearchName(openedResearch);
              control11.Color = ARGBColors.White;
              control11.Position = new Point(0, 0);
              control11.Size = control10.Size;
              control11.Font = !(Program.mySettings.LanguageIdent == "tr") || openedResearch != 14 && openedResearch != 66 && openedResearch != 46 && openedResearch != 41 && openedResearch != 43 && openedResearch != 42 ? (!(Program.mySettings.LanguageIdent == "pl") || openedResearch != 14 && openedResearch != 37 && openedResearch != 45 && openedResearch != 50 ? (!(Program.mySettings.LanguageIdent == "it") || openedResearch != 17 && openedResearch != 67 && openedResearch != 41 ? (!(Program.mySettings.LanguageIdent == "pt") || openedResearch != 0 && openedResearch != 39 && openedResearch != 17 && openedResearch != 66 && openedResearch != 64 && openedResearch != 10 && openedResearch != 43 && openedResearch != 44 && openedResearch != 45 && openedResearch != 46 ? (!(Program.mySettings.LanguageIdent == "pt") || openedResearch != 34 && openedResearch != 42 ? (openedResearch != 45 && openedResearch != 43 || !(Program.mySettings.LanguageIdent == "de") ? FontManager.GetFont("Arial", 10f, FontStyle.Regular) : FontManager.GetFont("Arial", 9f, FontStyle.Regular)) : FontManager.GetFont("Arial", 7.5f, FontStyle.Regular)) : FontManager.GetFont("Arial", 8f, FontStyle.Regular)) : FontManager.GetFont("Arial", 8f, FontStyle.Regular)) : FontManager.GetFont("Arial", 8f, FontStyle.Regular)) : FontManager.GetFont("Arial", 8f, FontStyle.Regular);
              control11.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              control11.Visible = true;
              control10.addControl((CustomSelfDrawPanel.CSDControl) control11);
            }
          }
          if (openedBuilding > 0 || openedCastleBuilding > 0 || openedTroop > 0)
          {
            CustomSelfDrawPanel.CSDImage control12 = this.startResearchOpenBuilding[index1];
            if (openedBuilding > 0)
              control12.Image = (Image) this.getBuildingGFX(openedBuilding);
            if (openedCastleBuilding > 0)
              control12.Image = (Image) this.getCastleGFX(openedCastleBuilding);
            if (openedTroop > 0)
              control12.Image = (Image) this.getCastleGFX(openedTroop);
            if (control12.Image != null)
            {
              control12.Position = new Point(197 - control12.Image.Size.Width / 2, 42 - control12.Image.Size.Height / 2);
              control12.Visible = true;
              control6.addControl((CustomSelfDrawPanel.CSDControl) control12);
            }
          }
          if (index2 == 59)
            ResearchPanel.TUTORIAL_artsTabPos = y;
          ++index1;
          y += 80;
          height += 80;
        }
      }
      this.startResearchHeaderMain.Text = SK.Text("Research_Choose_Next", "Choose Next Research");
      this.startResearchHeaderMain.Color = ARGBColors.Black;
      this.startResearchHeaderMain.Position = new Point(183, 12);
      this.startResearchHeaderMain.Size = new Size(400, 60);
      this.startResearchHeaderMain.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.startResearchHeaderMain.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.scrollPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.startResearchHeaderMain);
      this.startResearchHeaderResearchOpen.Text = SK.Text("Research_Allows", "Allows");
      this.startResearchHeaderResearchOpen.Color = ARGBColors.Black;
      this.startResearchHeaderResearchOpen.Position = new Point(656, 12);
      this.startResearchHeaderResearchOpen.Size = new Size(200, 60);
      this.startResearchHeaderResearchOpen.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.startResearchHeaderResearchOpen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.scrollPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.startResearchHeaderResearchOpen);
      this.startResearchHeaderBuildingOpen.Text = SK.Text("Research_Opens", "Opens");
      this.startResearchHeaderBuildingOpen.Color = ARGBColors.Black;
      this.startResearchHeaderBuildingOpen.Position = new Point(813, 12);
      this.startResearchHeaderBuildingOpen.Size = new Size(200, 60);
      this.startResearchHeaderBuildingOpen.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.startResearchHeaderBuildingOpen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.scrollPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.startResearchHeaderBuildingOpen);
      int num = height;
      this.startResearchScrollBar.clearControls();
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.startResearchScrollBar);
      if (height <= this.scrollPanelImage.ClipRect.Height)
      {
        height = this.scrollPanelImage.ClipRect.Height;
        this.startResearchScrollBar.Visible = false;
      }
      else
      {
        this.startResearchScrollBar.Visible = true;
        this.startResearchScrollBar.Position = new Point(this.Width - 20 - 10 - 32, (int) byte.MaxValue);
        this.startResearchScrollBar.Size = new Size(32, this.scrollPanelImage.ClipRect.Height - 13 - 13);
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.startResearchScrollBar);
        this.startResearchScrollBar.Value = 0;
        this.startResearchScrollBar.Max = num - this.scrollPanelImage.ClipRect.Height;
        this.startResearchScrollBar.NumVisibleLines = this.scrollPanelImage.ClipRect.Height;
        this.startResearchScrollBar.OffsetTL = new Point(1, 5);
        this.startResearchScrollBar.OffsetBR = new Point(0, -10);
        this.startResearchScrollBar.Create((Image) GFXLibrary.scroll_inset_top, (Image) GFXLibrary.scroll_inset_mid, (Image) GFXLibrary.scroll_inset_bottom, (Image) GFXLibrary.scroll_thumb_top, (Image) GFXLibrary.scroll_thumb_mid, (Image) GFXLibrary.scroll_thumb_bottom);
        this.startResearchScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.scrollBarMoved));
      }
      this.scrollPanelImage.Size = new Size(this.scrollPanelImage.Size.Width, height);
      this.scrollPanelImage.Position = new Point(20, 242);
      this.scrollPanelImage.invalidate();
    }

    private void scrollBarMoved()
    {
      int y = this.startResearchScrollBar.Value;
      this.scrollPanelImage.Position = new Point(this.scrollPanelImage.Position.X, 242 - y);
      this.scrollPanelImage.ClipRect = new Rectangle(this.scrollPanelImage.ClipRect.X, y, this.scrollPanelImage.ClipRect.Width, this.scrollPanelImage.ClipRect.Height);
      this.scrollPanelImage.invalidate();
      this.scrollPanelBottomMiddleOverlay.invalidate();
      this.scrollPanelBottomLeftOverlay.invalidate();
    }

    private void listWindowMouseWheel(int delta)
    {
      if (!this.startResearchScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.startResearchScrollBar.scrollDown();
      }
      else
      {
        if (delta <= 0)
          return;
        this.startResearchScrollBar.scrollUp();
      }
    }

    private void tabClicked()
    {
      this.selectedQueueSlot = -1;
      if (this.ClickedControl == null)
        return;
      CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) this.ClickedControl;
      if (clickedControl.Data == this.lastTab)
        return;
      this.manageTabs(clickedControl.Data);
    }

    private void tabModeClicked()
    {
      this.selectedQueueSlot = -1;
      if (this.ClickedControl == null)
        return;
      CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) this.ClickedControl;
      if (clickedControl.Data == this.tabType)
        return;
      this.tabType = clickedControl.Data;
      this.manageTabs(this.lastTab);
    }

    private void manageTabs(int tab)
    {
      this.lastTab = tab;
      this.tab1Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_01_normal;
      this.tab1Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_01_highlight;
      this.tab2Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
      this.tab2Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
      this.tab3Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
      this.tab3Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
      this.tab4Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
      this.tab4Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
      this.tab5Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
      this.tab5Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
      if (this.tabType == 0)
      {
        this.tabModeListButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_list_highlight;
        this.tabModeListButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_list_highlight;
        this.tabModeTreeButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_tree_normal;
        this.tabModeTreeButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_tree_highlight;
      }
      else
      {
        this.tabModeListButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_list_normal;
        this.tabModeListButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_list_highlight;
        this.tabModeTreeButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_tree_highlight;
        this.tabModeTreeButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_tree_highlight;
      }
      this.tab1Button.Text.Color = Color.FromArgb(205, 157, 49);
      this.tab2Button.Text.Color = Color.FromArgb(205, 157, 49);
      this.tab3Button.Text.Color = Color.FromArgb(205, 157, 49);
      this.tab4Button.Text.Color = Color.FromArgb(205, 157, 49);
      this.tab5Button.Text.Color = Color.FromArgb(205, 157, 49);
      switch (tab)
      {
        case 0:
          this.tab1Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_01_highlight;
          this.tab1Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_01_highlight;
          this.tab1Button.Text.Color = ARGBColors.White;
          if (this.tabType == 0)
          {
            this.initIndustryTab();
            break;
          }
          this.initExploreTab(0);
          break;
        case 1:
          this.tab2Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_highlight;
          this.tab2Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
          this.tab2Button.Text.Color = ARGBColors.White;
          if (this.tabType == 0)
          {
            this.initMilitaryTab();
            break;
          }
          this.initExploreTab(1);
          break;
        case 2:
          this.tab3Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_highlight;
          this.tab3Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
          this.tab3Button.Text.Color = ARGBColors.White;
          if (this.tabType == 0)
          {
            this.initFarmingTab();
            break;
          }
          this.initExploreTab(2);
          break;
        case 3:
          this.tab4Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_highlight;
          this.tab4Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
          this.tab4Button.Text.Color = ARGBColors.White;
          if (this.tabType == 0)
          {
            this.initEducationTab();
            break;
          }
          this.initExploreTab(3);
          break;
        case 4:
          this.tab5Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_highlight;
          this.tab5Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
          this.tab5Button.Text.Color = ARGBColors.White;
          this.initExploreTab(0);
          break;
      }
      this.queuedResearchArea.invalidate();
      this.currentResearchInfoBox.invalidate();
      this.mainBackgroundImage.invalidate();
    }

    private void cancelResearchClick()
    {
      if (this.selectedQueueSlot >= 0 && (this.lastData == null || this.lastData.research_queueEntries == null || this.selectedQueueSlot >= this.lastData.research_queueEntries.Length))
        return;
      this.closeCancelResearchPopup();
      InterfaceMgr.Instance.openGreyOutWindow(false);
      this.cancelResearchPopup = new MyMessageBoxPopUp();
      this.cancelResearchPopup.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("Research_Cancel_Research", "Cancel Research?"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelResearch));
      this.cancelResearchPopup.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
    }

    private void closeCancelResearchPopup()
    {
      if (this.cancelResearchPopup == null)
        return;
      if (this.cancelResearchPopup.Created)
        this.cancelResearchPopup.Close();
      InterfaceMgr.Instance.closeGreyOut();
      this.cancelResearchPopup = (MyMessageBoxPopUp) null;
    }

    private void cancelResearch()
    {
      if (this.selectedQueueSlot < 0)
        GameEngine.Instance.World.doResearch(-1);
      else if (this.lastData != null && this.lastData.research_queueEntries != null && this.selectedQueueSlot < this.lastData.research_queueEntries.Length)
        GameEngine.Instance.World.CancelQueuedResearch(this.lastData.research_queueEntries[this.selectedQueueSlot], this.selectedQueueSlot);
      this.selectedQueueSlot = -1;
      InterfaceMgr.Instance.closeGreyOut();
      this.cancelResearchPopup.Close();
    }

    private void backgroundClick() => this.selectedQueueSlot = -1;

    private void buyPointClick()
    {
      this.selectedQueueSlot = -1;
      GameEngine.Instance.World.buyResearchPoint();
    }

    private void researchClicked()
    {
      this.selectedQueueSlot = -1;
      if (this.ClickedControl == null)
        return;
      int data = this.ClickedControl.Data;
      GameEngine.Instance.World.doResearch(data);
    }

    public void update(bool fullTick)
    {
      this.cardbar.update();
      this.tooltipToShow = -1;
      int data = 0;
      if (this.getToolTip(ref data))
        this.tooltipToShow = data;
      if (!fullTick || this.lastData == null)
        return;
      this.updateBasedOnResearchData(this.lastData, false);
    }

    private void applyData(ResearchData data)
    {
      int num1 = -1;
      int level = 0;
      this.researchAllowed = true;
      int researchPoints = data.research_points;
      if (data.research_queueEntries != null)
        researchPoints -= data.research_queueEntries.Length;
      if (data.researchingType >= 0)
      {
        if (!data.canDoMoreResearch(GameEngine.Instance.World.isAccountPremium()))
          this.researchAllowed = false;
        DateTime currentServerTime = VillageMap.getCurrentServerTime();
        if (this.selectedQueueSlot >= 0 && (data.research_queueEntries == null || this.selectedQueueSlot >= data.research_queueEntries.Length))
          this.selectedQueueSlot = -1;
        if (this.selectedQueueSlot < 0)
        {
          TimeSpan timeSpan1 = data.research_completionTime - currentServerTime;
          int secsLeft = (int) (timeSpan1.TotalSeconds + 0.5);
          if (secsLeft < 0)
            secsLeft = 0;
          this.timeProgressText.Text = SK.Text("Research_Completed_In", "Completed In") + " : " + VillageMap.createBuildTimeString(secsLeft);
          this.timeProgressText.Visible = true;
          if (GameEngine.Instance.World.isResearchLagging())
          {
            CustomSelfDrawPanel.CSDLabel timeProgressText = this.timeProgressText;
            timeProgressText.Text = timeProgressText.Text + " (" + SK.Text("Research_Lagging", "Research Overdue, Please wait") + ")";
          }
          TimeSpan timeSpan2 = data.calcResearchTime(data.research_pointCount - 1, GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData);
          if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
            timeSpan2 = new TimeSpan(timeSpan2.Ticks / 2L);
          int maxValue = (int) timeSpan2.TotalSeconds;
          if (maxValue < 1)
            maxValue = 1;
          if (maxValue == 30 && GameEngine.Instance.World.getTutorialStage() == 5)
            maxValue = 11;
          double num2 = timeSpan1.TotalSeconds;
          if (num2 < 0.0)
            num2 = 0.0;
          double curValue = (double) maxValue - num2;
          if (curValue < 0.0)
            curValue = 0.0;
          this.timeProgressBar.setValues(curValue, (double) maxValue);
          this.currentResearchCancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
          num1 = data.researchingType;
          level = (int) data.research[data.researchingType];
        }
        else
        {
          TimeSpan timeSpan3 = data.research_completionTime - currentServerTime;
          if (this.selectedQueueSlot > 0)
          {
            for (int index = 0; index < this.selectedQueueSlot; ++index)
            {
              TimeSpan timeSpan4 = data.calcResearchTime(data.research_pointCount + index, GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData);
              if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                timeSpan4 = new TimeSpan(timeSpan4.Ticks / 2L);
              timeSpan3 += timeSpan4;
            }
          }
          int secsLeft = (int) (timeSpan3.TotalSeconds + 0.5);
          if (secsLeft < 0)
            secsLeft = 0;
          this.timeProgressText.Text = SK.Text("Research_Starts_In", "Starts In") + " : " + VillageMap.createBuildTimeString(secsLeft);
          this.timeProgressText.Visible = true;
          this.timeProgressBar.setValues(0.0, 0.0);
          num1 = data.research_queueEntries[this.selectedQueueSlot];
          level = 0;
          this.currentResearchCancelButton.Text.Text = SK.Text("Research_Remove_From_Queue", "Remove From Queue");
        }
        this.currentResearchCancelButton.Enabled = true;
      }
      else
      {
        TimeSpan timeSpan = data.calcResearchTime(data.research_pointCount, GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData);
        if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
          timeSpan = new TimeSpan(timeSpan.Ticks / 2L);
        int totalSeconds = (int) timeSpan.TotalSeconds;
        this.currentResearchCancelButton.Enabled = false;
        this.timeProgressText.Text = SK.Text("Research_Next_Duration", "Next Research Duration") + " : " + VillageMap.createBuildTimeString(totalSeconds);
        this.timeProgressBar.setValues(0.0, 0.0);
      }
      if (this.tooltipToShow >= 0)
      {
        num1 = this.tooltipToShow / 1000;
        level = this.tooltipToShow % 1000 - 1;
      }
      if (num1 >= 0)
      {
        this.currentResearchInfoBoxHeadingText.Visible = true;
        this.currentResearchInfoBoxHeadingText.Text = ResearchData.getResearchName(num1);
        this.currentResearchText.Text = this.currentResearchInfoBoxHeadingText.Text;
        this.currentResearchText.Visible = true;
        this.currentResearchImage.Image = (Image) GFXLibrary.getResearchIllustration(num1);
        if (this.currentResearchImage.Image != null)
          this.currentResearchImage.Visible = true;
        else
          this.currentResearchImage.Visible = false;
        this.currentResearchInfoBoxRow1Text.Text = ResearchData.getDescriptionText(num1, level);
        this.currentResearchInfoBoxRow2Text.Text = "";
        this.currentResearchInfoBoxRow3Text.Text = ResearchData.getEffectText(num1, level, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.SixthAgeWorld, GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1, GameEngine.Instance.LocalWorldData);
        this.currentResearchInfoBoxRow1Text.Visible = true;
        this.currentResearchInfoBoxRow2Text.Visible = this.dragOverlay.Visible;
        this.currentResearchInfoBoxRow3Text.Visible = this.dragOverlay.Visible;
        this.currentResearchBackgroundImage.Visible = true;
        this.currentResearchBackgroundImage2.Visible = false;
        this.currentResearchText.Font = !(Program.mySettings.LanguageIdent == "tr") || num1 != 14 && num1 != 66 && num1 != 46 && num1 != 41 && num1 != 43 && num1 != 42 ? (!(Program.mySettings.LanguageIdent == "pl") || num1 != 14 && num1 != 37 && num1 != 45 && num1 != 50 ? (!(Program.mySettings.LanguageIdent == "it") || num1 != 17 && num1 != 67 && num1 != 41 ? (!(Program.mySettings.LanguageIdent == "pt") || num1 != 0 && num1 != 39 && num1 != 17 && num1 != 66 && num1 != 64 && num1 != 10 && num1 != 43 && num1 != 44 && num1 != 45 && num1 != 46 ? (!(Program.mySettings.LanguageIdent == "pt") || num1 != 34 && num1 != 42 ? FontManager.GetFont("Arial", 10f, FontStyle.Regular) : FontManager.GetFont("Arial", 7.5f, FontStyle.Regular)) : FontManager.GetFont("Arial", 8f, FontStyle.Regular)) : FontManager.GetFont("Arial", 8f, FontStyle.Regular)) : FontManager.GetFont("Arial", 8f, FontStyle.Regular)) : FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      }
      else
      {
        this.currentResearchBackgroundImage2.Visible = true;
        this.currentResearchBackgroundImage.Visible = false;
        this.currentResearchInfoBoxRow1Text.Visible = false;
        this.currentResearchInfoBoxRow2Text.Visible = false;
        this.currentResearchInfoBoxRow3Text.Visible = false;
        this.currentResearchText.Visible = false;
        this.currentResearchImage.Visible = false;
        this.currentResearchInfoBoxHeadingText.Text = SK.Text("Research_No_Current", "No current research");
      }
      this.pointsText.Text = SK.Text("Research_Research_Points", "Research Points") + " : " + researchPoints.ToString();
      NumberFormatInfo nfi = GameEngine.NFI;
      if (researchPoints <= 0)
        this.researchAllowed = false;
      double num3 = data.calcPointGoldCost(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData);
      this.buyPointText.Text = num3.ToString("N", (IFormatProvider) nfi);
      if (num3 <= GameEngine.Instance.World.getCurrentGold())
        this.buyPointButton.Enabled = true;
      else
        this.buyPointButton.Enabled = false;
      this.queuedResearchImage1.Visible = false;
      this.queuedResearchImage2.Visible = false;
      this.queuedResearchImage3.Visible = false;
      this.queuedResearchImage4.Visible = false;
      this.queuedResearchImage5.Visible = false;
      this.queuedResearchImage6.Visible = false;
      this.queuedResearchImage7.Visible = false;
      this.queuedResearchButton1.Enabled = false;
      this.queuedResearchButton2.Enabled = false;
      this.queuedResearchButton3.Enabled = false;
      this.queuedResearchButton4.Enabled = false;
      this.queuedResearchButton5.Enabled = false;
      this.queuedResearchButton6.Enabled = false;
      this.queuedResearchButton7.Enabled = false;
      if (data.research_queueEntries != null && data.research_queueEntries.Length > 0 && data.researchingType >= 0)
      {
        for (int index = 0; index < data.research_queueEntries.Length; ++index)
        {
          CustomSelfDrawPanel.CSDImage csdImage = (CustomSelfDrawPanel.CSDImage) null;
          CustomSelfDrawPanel.CSDButton csdButton = (CustomSelfDrawPanel.CSDButton) null;
          switch (index)
          {
            case 0:
              csdImage = this.queuedResearchImage1;
              csdButton = this.queuedResearchButton1;
              break;
            case 1:
              csdImage = this.queuedResearchImage2;
              csdButton = this.queuedResearchButton2;
              break;
            case 2:
              csdImage = this.queuedResearchImage3;
              csdButton = this.queuedResearchButton3;
              break;
            case 3:
              csdImage = this.queuedResearchImage4;
              csdButton = this.queuedResearchButton4;
              break;
            case 4:
              csdImage = this.queuedResearchImage5;
              csdButton = this.queuedResearchButton5;
              break;
            case 5:
              csdImage = this.queuedResearchImage6;
              csdButton = this.queuedResearchButton6;
              break;
            case 6:
              csdImage = this.queuedResearchImage7;
              csdButton = this.queuedResearchButton7;
              break;
          }
          csdImage.Visible = true;
          csdImage.Image = (Image) GFXLibrary.getResearchIllustration(data.research_queueEntries[index]);
          csdImage.Size = new Size(csdImage.Size.Width / 2, csdImage.Size.Height / 2);
          csdImage.CustomTooltipID = 302;
          csdImage.CustomTooltipData = index;
          csdButton.Enabled = true;
          csdButton.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
          csdButton.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
          csdButton.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
          csdButton.CustomTooltipID = 302;
          csdButton.CustomTooltipData = index;
        }
      }
      if (this.selectedQueueSlot >= 0)
      {
        CustomSelfDrawPanel.CSDButton csdButton = (CustomSelfDrawPanel.CSDButton) null;
        switch (this.selectedQueueSlot)
        {
          case 0:
            csdButton = this.queuedResearchButton1;
            break;
          case 1:
            csdButton = this.queuedResearchButton2;
            break;
          case 2:
            csdButton = this.queuedResearchButton3;
            break;
          case 3:
            csdButton = this.queuedResearchButton4;
            break;
          case 4:
            csdButton = this.queuedResearchButton5;
            break;
          case 5:
            csdButton = this.queuedResearchButton6;
            break;
          case 6:
            csdButton = this.queuedResearchButton7;
            break;
        }
        csdButton.ImageNorm = (Image) GFXLibrary.border_research_ill_selected_normal;
        csdButton.ImageOver = (Image) GFXLibrary.border_research_ill_selected_normal;
        csdButton.ImageClick = (Image) GFXLibrary.border_research_ill_selected_normal;
      }
      if (GameEngine.Instance.World.isAccountPremium())
        this.queuedResearchNoPremiumText.Visible = false;
      else
        this.queuedResearchNoPremiumText.Visible = true;
    }

    public void updateBasedOnResearchData(ResearchData data, bool localForce)
    {
      if (data == null)
        return;
      this.lastData = data;
      this.lastDataQueued = this.lastData;
      if (this.lastData.researchingType >= 0)
      {
        this.lastDataQueued = data.copyAndAdd(data.researchingType, false);
        if (data.research_queueEntries != null)
        {
          foreach (int researchQueueEntry in data.research_queueEntries)
            this.lastDataQueued = this.lastDataQueued.copyAndAdd(researchQueueEntry, true);
        }
      }
      this.applyData(data);
      if (this.lastData == data && !this.forceUpdate && !localForce)
        return;
      int num = this.startResearchScrollBar.Value;
      if (!this.startResearchScrollBar.Visible)
        num = 0;
      this.init();
      this.forceUpdate = false;
      this.applyData(data);
      this.startResearchScrollBar.Value = num;
      this.scrollBarMoved();
    }

    private void queuedResearchClick()
    {
      CustomSelfDrawPanel.CSDControl clickedControl = this.ClickedControl;
      if (clickedControl == null)
        return;
      this.selectedQueueSlot = clickedControl.Data;
    }

    private void ResearchPanel2_SizeChanged(object sender, EventArgs e)
    {
      this.updateBasedOnResearchData(this.lastData, true);
      this.Invalidate();
    }

    private void initExploreTab(int mode)
    {
      this.queuedResearchArea.Visible = false;
      this.currentResearchInfoBox.Create((Image) GFXLibrary.tech_tree_inset_tall_left, (Image) GFXLibrary.tech_tree_inset_tall_mid, (Image) GFXLibrary.tech_tree_inset_tall_right);
      this.scrollPanelImage.clearControls();
      this.startResearchScrollBar.clearControls();
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.startResearchScrollBar);
      this.dragOverlay.Visible = true;
      this.dragOverlay2.Visible = false;
      if (!this.rowsCreated)
      {
        this.rowsCreated = true;
        this.createRows(0, 0, 1, 1, this.industryLayout, this.industryRows, 21, 18);
        this.createRows(0, 0, 1, 1, this.farmingLayout, this.farmingRows, 13, 17);
        this.createRows(0, 0, 1, 1, this.militaryLayout, this.militaryRows, 24, 30);
        this.createRows(0, 0, 1, 1, this.educationLayout, this.educationRows, 27, 26);
        this.createRows(0, 0, 1, 1, this.educationLayout2, this.educationRows2, 26, 26);
      }
      this.resetImageCache();
      this.resetLabelCache();
      switch (mode)
      {
        case 0:
          this.scrollPanelImage.Size = new Size(2780, 2390);
          this.updateRows(0, 0, 1, 1, this.industryLayout, this.industryRows, 21);
          break;
        case 1:
          int num1 = 20;
          this.scrollPanelImage.Size = new Size((GameEngine.Instance.World.getRank() != 22 ? num1 - 2 : num1 + 5) * 150 + 80, 2720);
          this.updateRows(0, 0, 1, 1, this.militaryLayout, this.militaryRows, 24);
          break;
        case 2:
          this.scrollPanelImage.Size = new Size(2630, 1510);
          this.updateRows(0, 0, 1, 1, this.farmingLayout, this.farmingRows, 13);
          break;
        case 3:
          this.scrollPanelImage.Size = new Size(3980, 3050);
          if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
          {
            this.updateRows(0, 0, 1, 1, this.educationLayout, this.educationRows, 27);
            break;
          }
          this.updateRows(0, 0, 1, 1, this.educationLayout2, this.educationRows2, 26);
          break;
      }
      this.realScrollImageSize = this.scrollPanelImage.Size;
      this.scrollPanelImage.setChildrensScale(this.m_windowScale);
      this.scrollPanelImage.Position = new Point(20, 242);
      this.scrollPanelImage.ClipRect = new Rectangle(new Point(0, 0), new Size(this.Width - 40, this.Height - 205 - 55));
      int num2 = 0;
      int num3 = 0;
      this.lastScrollXPos = 0;
      this.lastScrollYPos = 0;
      this.scrollPanelImage.Position = new Point(20 - num2, 242 - num3);
      this.scrollPanelImage.ClipRect = new Rectangle(this.scrollPanelImage.ClipRect.X + num2, this.scrollPanelImage.ClipRect.Y + num3, this.scrollPanelImage.ClipRect.Width, this.scrollPanelImage.ClipRect.Height);
      this.dragWindowMouseWheel(0);
      this.Invalidate();
    }

    private void windowDragged()
    {
      int num1 = -this.dragOverlay.XDiff;
      int num2 = -this.dragOverlay.YDiff;
      int num3 = num1 * 2;
      int num4 = num2 * 2;
      if (this.scrollPanelImage.ClipRect.X + num3 < 0)
        num3 = -this.scrollPanelImage.ClipRect.X;
      if (this.scrollPanelImage.ClipRect.Y + num4 < 0)
        num4 = -this.scrollPanelImage.ClipRect.Y;
      double windowScale = this.m_windowScale;
      int num5 = (int) ((double) this.scrollPanelImage.Size.Width * windowScale);
      int num6 = (int) ((double) this.scrollPanelImage.Size.Height * windowScale);
      if (this.scrollPanelImage.ClipRect.X + num3 > num5 - this.scrollPanelImage.ClipRect.Width)
        num3 -= this.scrollPanelImage.ClipRect.X + num3 - (num5 - this.scrollPanelImage.ClipRect.Width);
      if (this.scrollPanelImage.ClipRect.Y + num4 > num6 - this.scrollPanelImage.ClipRect.Height)
        num4 -= this.scrollPanelImage.ClipRect.Y + num4 - (num6 - this.scrollPanelImage.ClipRect.Height);
      this.scrollPanelImage.Position = new Point(this.scrollPanelImage.Position.X - num3, this.scrollPanelImage.Position.Y - num4);
      this.scrollPanelImage.ClipRect = new Rectangle(this.scrollPanelImage.ClipRect.X + num3, this.scrollPanelImage.ClipRect.Y + num4, this.scrollPanelImage.ClipRect.Width, this.scrollPanelImage.ClipRect.Height);
      this.scrollPanelImage.invalidate();
      this.lastScrollXPos = (int) ((double) (-(this.scrollPanelImage.Position.X - 20) + this.scrollPanelImage.ClipRect.Width / 2) / windowScale);
      this.lastScrollYPos = (int) ((double) (-(this.scrollPanelImage.Position.Y - 187 - 55) + this.scrollPanelImage.ClipRect.Height / 2) / windowScale);
    }

    private void dragWindowMouseOver()
    {
      CursorManager.SetCursor(CursorManager.CursorType.Hand, InterfaceMgr.Instance.ParentForm);
    }

    private void dragWindowMouseLeave()
    {
      CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
    }

    private void dragWindowMouseWheel(int delta)
    {
      double windowScale = this.m_windowScale;
      if (delta < 0)
      {
        if (this.m_windowScaleNotch > 0)
          --this.m_windowScaleNotch;
      }
      else if (delta > 0 && this.m_windowScaleNotch < this.windowScalingValues.Length - 1)
        ++this.m_windowScaleNotch;
      this.m_windowScale = this.windowScalingValues[this.m_windowScaleNotch];
      double num1 = (double) this.realScrollImageSize.Width * this.m_windowScale;
      double num2 = (double) this.realScrollImageSize.Height * this.m_windowScale;
      if (num1 < (double) this.scrollPanelImage.ClipRect.Width)
        this.scrollPanelImage.Size = new Size((int) ((double) this.scrollPanelImage.ClipRect.Width / this.m_windowScale), this.scrollPanelImage.Size.Height);
      else
        this.scrollPanelImage.Size = new Size(this.realScrollImageSize.Width, this.scrollPanelImage.Size.Height);
      if (num2 < (double) this.scrollPanelImage.ClipRect.Height)
        this.scrollPanelImage.Size = new Size(this.scrollPanelImage.Size.Width, (int) ((double) this.scrollPanelImage.ClipRect.Height / this.m_windowScale));
      else
        this.scrollPanelImage.Size = new Size(this.scrollPanelImage.Size.Width, this.realScrollImageSize.Height);
      if (windowScale == this.m_windowScale)
        return;
      this.rescaleWindow(windowScale, this.m_windowScale);
    }

    private void rescaleWindow(double oldScale, double newScale)
    {
      this.scrollPanelImage.setChildrensScale(newScale);
      int num1 = (int) ((double) (-(this.scrollPanelImage.Position.X - 20) + this.scrollPanelImage.ClipRect.Width / 2) / oldScale);
      int num2 = (int) ((double) (-(this.scrollPanelImage.Position.Y - 187 - 55) + this.scrollPanelImage.ClipRect.Height / 2) / oldScale);
      this.lastScrollXPos = num1;
      this.lastScrollYPos = num2;
      this.scrollPanelImage.ClipRect = new Rectangle(new Point(0, 0), new Size(this.Width - 40, this.Height - 205 - 55));
      int num3 = (int) ((double) num1 * newScale) - this.scrollPanelImage.ClipRect.Width / 2;
      int num4 = (int) ((double) num2 * newScale) - this.scrollPanelImage.ClipRect.Height / 2;
      if (num3 < 0)
        num3 = 0;
      if (num4 < 0)
        num4 = 0;
      if ((double) num3 > (double) this.scrollPanelImage.Size.Width * newScale - (double) this.scrollPanelImage.ClipRect.Width)
        num3 = (int) ((double) this.scrollPanelImage.Size.Width * newScale) - this.scrollPanelImage.ClipRect.Width - 1;
      if ((double) num4 > (double) this.scrollPanelImage.Size.Height * newScale - (double) this.scrollPanelImage.ClipRect.Height)
        num3 = (int) ((double) this.scrollPanelImage.Size.Height * newScale) - this.scrollPanelImage.ClipRect.Height - 1;
      this.scrollPanelImage.Position = new Point(20 - num3, 242 - num4);
      this.scrollPanelImage.ClipRect = new Rectangle(this.scrollPanelImage.ClipRect.X + num3, this.scrollPanelImage.ClipRect.Y + num4, this.scrollPanelImage.ClipRect.Width, this.scrollPanelImage.ClipRect.Height);
      this.scrollPanelImage.invalidate();
    }

    private void createRows(
      int startColumn,
      int startRow,
      int dx,
      int dy,
      int[] layout,
      CustomSelfDrawPanel.CSDImage[][] rows,
      int numRows,
      int numColumns)
    {
      GameEngine.Instance.World.getRank();
      for (int index1 = 0; index1 < numRows; ++index1)
      {
        int researchType = layout[index1 * 2];
        int num1 = layout[index1 * 2 + 1];
        int num2 = 1;
        if (researchType >= 0)
          num2 += ResearchData.getNumLevels(researchType, 22, GameEngine.Instance.LocalWorldData);
        int y1 = (index1 * dy + startRow) * 110 + 40;
        rows[index1] = new CustomSelfDrawPanel.CSDImage[numColumns];
        for (int index2 = 0; index2 < num2; ++index2)
        {
          int x1 = ((index2 + num1) * dx + startColumn) * 150 + 40;
          CustomSelfDrawPanel.CSDImage csdImage1 = new CustomSelfDrawPanel.CSDImage();
          csdImage1.Position = new Point(x1, y1);
          rows[index1][index2 + num1] = csdImage1;
          if (index2 == 0)
          {
            csdImage1.Data = 0;
            switch (num1)
            {
              case 0:
                continue;
              case 1:
                int x2 = ((index2 - 1 + num1) * dx + startColumn) * 150 + 40;
                CustomSelfDrawPanel.CSDImage csdImage2 = new CustomSelfDrawPanel.CSDImage();
                csdImage2.Position = new Point(x2, y1);
                rows[index1][index2 - 1 + num1] = csdImage2;
                for (int index3 = index1 - 1; index3 > 0 && rows[index3][index2 - 1 + num1] == null; --index3)
                {
                  int y2 = (index3 * dy + startRow) * 110 + 40;
                  CustomSelfDrawPanel.CSDImage csdImage3 = new CustomSelfDrawPanel.CSDImage();
                  csdImage3.Position = new Point(x2, y2);
                  csdImage3.Data = 1;
                  rows[index3][index2 - 1 + num1] = csdImage3;
                }
                continue;
              default:
                for (int index4 = index1 - 1; index4 > 0 && rows[index4][index2 + num1] == null; --index4)
                {
                  int y3 = (index4 * dy + startRow) * 110 + 40;
                  CustomSelfDrawPanel.CSDImage csdImage4 = new CustomSelfDrawPanel.CSDImage();
                  csdImage4.Position = new Point(x1, y3);
                  csdImage4.Data = 1;
                  rows[index4][index2 + num1] = csdImage4;
                }
                continue;
            }
          }
          else
            csdImage1.Data = 2;
        }
      }
    }

    private void updateRows(
      int startColumn,
      int startRow,
      int dx,
      int dy,
      int[] layout,
      CustomSelfDrawPanel.CSDImage[][] rows,
      int numRows)
    {
      int rank = GameEngine.Instance.World.getRank();
      int rankSubLevel = GameEngine.Instance.World.getRankSubLevel();
      Font font1 = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      Font font2 = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      Font font3 = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      Font font4 = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
      for (int index1 = 0; index1 < numRows; ++index1)
      {
        int index2 = layout[index1 * 2];
        int num1 = layout[index1 * 2 + 1];
        int num2 = 1;
        if (index2 >= 0)
          num2 += ResearchData.getNumLevels(index2, rank, GameEngine.Instance.LocalWorldData);
        for (int index3 = 0; index3 < num2; ++index3)
        {
          CustomSelfDrawPanel.CSDImage control1 = rows[index1][index3 + num1];
          control1.clearControls();
          this.scrollPanelImage.addControl((CustomSelfDrawPanel.CSDControl) control1);
          int num3;
          if (index3 == 0)
          {
            if (num1 == 0)
            {
              control1.Image = (Image) this.getIllBack(dx, dy, 0, 0, 1, 0, -1);
              CustomSelfDrawPanel.CSDImage nextImage = this.getNextImage();
              nextImage.Position = new Point(3, 7);
              string str = "";
              num3 = index2;
              switch (num3)
              {
                case -4:
                  nextImage.Image = (Image) GFXLibrary.research_ill_education;
                  str = SK.Text("Research_Education", "Education");
                  break;
                case -3:
                  nextImage.Image = (Image) GFXLibrary.research_ill_military;
                  str = SK.Text("Research_Military", "Military");
                  break;
                case -2:
                  nextImage.Image = (Image) GFXLibrary.research_ill_farming;
                  str = SK.Text("Research_Farming", "Farming");
                  break;
                case -1:
                  nextImage.Image = (Image) GFXLibrary.research_ill_industry;
                  str = SK.Text("Research_Industry", "Industry");
                  break;
              }
              control1.addControl((CustomSelfDrawPanel.CSDControl) nextImage);
              CustomSelfDrawPanel.CSDLabel nextLabel = this.getNextLabel();
              nextLabel.Text = str;
              nextLabel.Color = ARGBColors.Black;
              nextLabel.Position = new Point(6, 71);
              nextLabel.Size = new Size(135, 30);
              nextLabel.Font = font1;
              nextLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
              control1.addControl((CustomSelfDrawPanel.CSDControl) nextLabel);
            }
            else
            {
              control1.Tooltip = index2 * 1000;
              int right = 1;
              int rankNeeded1 = -1;
              bool special1 = false;
              int rankNeeded2 = -1;
              bool special2 = false;
              bool flag1 = false;
              if (!this.lastDataQueued.isResearchStepOpen(index2, index3, rank, rankSubLevel, ref rankNeeded1, ref special1, GameEngine.Instance.LocalWorldData.EraWorld))
                right = 2;
              else if (!this.lastData.isResearchStepOpen(index2, index3, rank, rankSubLevel, ref rankNeeded2, ref special2, GameEngine.Instance.LocalWorldData.EraWorld))
                flag1 = true;
              if (num1 == 1)
              {
                bool flag2 = false;
                if (this.lastDataQueued.research[index2] > (byte) 0 && this.lastData.research[index2] == (byte) 0)
                  flag2 = true;
                control1.Image = !flag2 ? (Image) this.getIllBack(dx, dy, 0, 1, 0, right, index2) : (Image) GFXLibrary.ill_back_yline_0101;
              }
              else
                control1.Image = !flag1 ? (Image) this.getIllBack(dx, dy, 1, 0, 0, right, index2) : (Image) GFXLibrary.ill_back_yline_1100;
              CustomSelfDrawPanel.CSDImage nextImage1 = this.getNextImage();
              nextImage1.Image = (Image) GFXLibrary.getResearchIllustration(index2);
              nextImage1.Position = new Point(3, 7);
              control1.addControl((CustomSelfDrawPanel.CSDControl) nextImage1);
              if (right != 2)
              {
                CustomSelfDrawPanel.CSDImage nextImage2 = this.getNextImage();
                nextImage2.Image = flag1 ? (Image) GFXLibrary.ill_back_yellow_textback : (Image) GFXLibrary.ill_back_green_textback;
                nextImage2.Position = new Point(4, 68);
                control1.addControl((CustomSelfDrawPanel.CSDControl) nextImage2);
              }
              else if (rankNeeded1 > 0)
              {
                CustomSelfDrawPanel.CSDImage nextImage3 = this.getNextImage();
                nextImage3.Image = (Image) GFXLibrary.ill_shield;
                nextImage3.Position = new Point(105, 2);
                nextImage3.Visible = true;
                nextImage1.addControl((CustomSelfDrawPanel.CSDControl) nextImage3);
                CustomSelfDrawPanel.CSDLabel nextLabel = this.getNextLabel();
                if (rankNeeded1 >= 100)
                {
                  CustomSelfDrawPanel.CSDLabel csdLabel = nextLabel;
                  num3 = rankNeeded1 - 100 + 1;
                  string str = num3.ToString();
                  csdLabel.Text = str;
                }
                else
                  nextLabel.Text = rankNeeded1.ToString();
                nextLabel.Color = ARGBColors.White;
                nextLabel.Position = new Point(0, -2);
                nextLabel.Size = nextImage3.Size;
                nextLabel.Font = font2;
                nextLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                nextLabel.Visible = true;
                nextImage3.addControl((CustomSelfDrawPanel.CSDControl) nextLabel);
              }
              int num4 = 0;
              CustomSelfDrawPanel.CSDLabel nextLabel1 = this.getNextLabel();
              if (Program.mySettings.LanguageIdent == "tr" && (index2 == 14 || index2 == 66 || index2 == 46 || index2 == 41 || index2 == 43 || index2 == 42))
                nextLabel1.Font = font3;
              else if (Program.mySettings.LanguageIdent == "pl" && (index2 == 14 || index2 == 37 || index2 == 45 || index2 == 50))
                nextLabel1.Font = font3;
              else if (Program.mySettings.LanguageIdent == "it" && (index2 == 17 || index2 == 67 || index2 == 41))
                nextLabel1.Font = font3;
              else if (Program.mySettings.LanguageIdent == "pt" && (index2 == 0 || index2 == 39 || index2 == 17 || index2 == 66 || index2 == 64 || index2 == 10 || index2 == 43 || index2 == 44 || index2 == 45 || index2 == 46))
                nextLabel1.Font = font3;
              else if (Program.mySettings.LanguageIdent == "pt" && (index2 == 34 || index2 == 42))
              {
                nextLabel1.Font = font4;
                num4 = -5;
              }
              else
                nextLabel1.Font = index2 == 66 && Program.mySettings.LanguageIdent == "en" || (index2 == 45 || index2 == 43) && Program.mySettings.LanguageIdent == "de" ? font3 : font1;
              nextLabel1.Text = ResearchData.getResearchName(index2);
              nextLabel1.Color = ARGBColors.Black;
              nextLabel1.Position = new Point(6, 71 + num4);
              nextLabel1.Size = new Size(135, 30);
              nextLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
              control1.addControl((CustomSelfDrawPanel.CSDControl) nextLabel1);
              if (num1 == 1)
              {
                CustomSelfDrawPanel.CSDImage control2 = rows[index1][index3 - 1 + num1];
                int num5 = 0;
                if (index1 < numRows - 1 && rows[index1 + 1][index3 - 1 + num1] != null)
                  num5 = 1;
                control2.Image = num5 != 1 ? (Image) GFXLibrary.gline_1100 : (Image) GFXLibrary.gline_1110;
                this.scrollPanelImage.addControl((CustomSelfDrawPanel.CSDControl) control2);
                for (int index4 = index1 - 1; index4 > 0; --index4)
                {
                  CustomSelfDrawPanel.CSDImage control3 = rows[index4][index3 - 1 + num1];
                  if (control3 != null)
                  {
                    if (control3.Data == 1)
                    {
                      control3.Image = (Image) GFXLibrary.gline_vertical;
                      this.scrollPanelImage.addControl((CustomSelfDrawPanel.CSDControl) control3);
                    }
                    else
                      break;
                  }
                }
              }
              else
              {
                for (int index5 = index1 - 1; index5 > 0; --index5)
                {
                  CustomSelfDrawPanel.CSDImage control4 = rows[index5][index3 + num1];
                  if (control4 != null)
                  {
                    if (control4.Data == 1)
                    {
                      control4.Image = !flag1 ? (right != 1 ? (Image) GFXLibrary.bline_vertical : (Image) GFXLibrary.gline_vertical) : (Image) GFXLibrary.yline_vertical;
                      this.scrollPanelImage.addControl((CustomSelfDrawPanel.CSDControl) control4);
                    }
                    else
                      break;
                  }
                }
              }
            }
          }
          else
          {
            control1.Tooltip = index2 * 1000 + index3;
            int down = 0;
            if (index1 < numRows - 1)
            {
              CustomSelfDrawPanel.CSDImage csdImage = rows[index1 + 1][index3 + num1];
              if (csdImage != null && (csdImage.Data == 0 || csdImage.Data == 1))
                down = 1;
            }
            int right = 0;
            if (index3 != num2 - 1)
              right = 1;
            bool flag3 = false;
            bool flag4 = false;
            int rankNeeded3 = -1;
            bool special3 = false;
            int num6;
            if ((int) this.lastDataQueued.research[index2] == index3 - 1)
            {
              if ((int) this.lastData.research[index2] != index3 - 1)
                flag3 = true;
              if (this.lastDataQueued.isResearchStepOpen(index2, index3 - 1, rank, rankSubLevel, ref rankNeeded3, ref special3, GameEngine.Instance.LocalWorldData.EraWorld))
              {
                num6 = 1;
                if (right != 0)
                  right = 2;
                if (down != 0)
                  down = 2;
                if (!this.lastData.isResearchStepOpen(index2, index3 - 1, rank, rankSubLevel, ref rankNeeded3, ref special3, GameEngine.Instance.LocalWorldData.EraWorld))
                  flag3 = true;
              }
              else
              {
                num6 = 2;
                if (rankNeeded3 > 0 && special3)
                {
                  flag3 = false;
                  CustomSelfDrawPanel.CSDImage nextImage = this.getNextImage();
                  nextImage.Image = (Image) GFXLibrary.ill_shield;
                  nextImage.Position = new Point(75 - nextImage.Image.Width / 2, 7);
                  nextImage.Visible = true;
                  control1.addControl((CustomSelfDrawPanel.CSDControl) nextImage);
                  CustomSelfDrawPanel.CSDLabel nextLabel = this.getNextLabel();
                  if (rankNeeded3 >= 100)
                  {
                    CustomSelfDrawPanel.CSDLabel csdLabel = nextLabel;
                    num3 = rankNeeded3 - 100 + 1;
                    string str = num3.ToString();
                    csdLabel.Text = str;
                  }
                  else
                    nextLabel.Text = rankNeeded3.ToString();
                  nextLabel.Color = ARGBColors.White;
                  nextLabel.Position = new Point(0, -2);
                  nextLabel.Size = nextImage.Size;
                  nextLabel.Font = font2;
                  nextLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                  nextLabel.Visible = true;
                  nextImage.addControl((CustomSelfDrawPanel.CSDControl) nextLabel);
                }
              }
              int openedBuilding = -1;
              int openedCastleBuilding = -1;
              int openedTroop = -1;
              ResearchData.getOpenedResearch(index2, index3, GameEngine.Instance.LocalWorldData.Alternate_Ruleset, ref openedBuilding, ref openedCastleBuilding, ref openedTroop);
              if (openedBuilding >= 0 || openedCastleBuilding > 0 || openedTroop > 0)
              {
                CustomSelfDrawPanel.CSDImage nextImage = this.getNextImage();
                if (openedBuilding >= 0)
                  nextImage.Image = (Image) this.getBuildingGFX(openedBuilding);
                if (openedCastleBuilding >= 0)
                  nextImage.Image = (Image) this.getCastleGFX(openedCastleBuilding);
                if (openedTroop >= 0)
                  nextImage.Image = (Image) this.getCastleGFX(openedTroop);
                if (nextImage.Image != null)
                {
                  nextImage.Position = new Point(112 - nextImage.Image.Width / 2, 82 - nextImage.Image.Height / 2);
                  nextImage.Visible = true;
                  control1.addControl((CustomSelfDrawPanel.CSDControl) nextImage);
                }
              }
            }
            else if ((int) this.lastDataQueued.research[index2] < index3 - 1)
            {
              num6 = 2;
              this.lastDataQueued.isResearchStepOpen(index2, index3 - 1, rank, rankSubLevel, ref rankNeeded3, ref special3, GameEngine.Instance.LocalWorldData.EraWorld);
              if (rankNeeded3 > 0 && special3)
              {
                CustomSelfDrawPanel.CSDImage nextImage = this.getNextImage();
                nextImage.Image = (Image) GFXLibrary.ill_shield;
                nextImage.Position = new Point(75 - nextImage.Image.Width / 2, 7);
                nextImage.Visible = true;
                control1.addControl((CustomSelfDrawPanel.CSDControl) nextImage);
                CustomSelfDrawPanel.CSDLabel nextLabel = this.getNextLabel();
                if (rankNeeded3 >= 100)
                {
                  CustomSelfDrawPanel.CSDLabel csdLabel = nextLabel;
                  num3 = rankNeeded3 - 100 + 1;
                  string str = num3.ToString();
                  csdLabel.Text = str;
                }
                else
                  nextLabel.Text = rankNeeded3.ToString();
                nextLabel.Color = ARGBColors.White;
                nextLabel.Position = new Point(0, -2);
                nextLabel.Size = nextImage.Size;
                nextLabel.Font = font2;
                nextLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                nextLabel.Visible = true;
                nextImage.addControl((CustomSelfDrawPanel.CSDControl) nextLabel);
              }
              int openedBuilding = -1;
              int openedCastleBuilding = -1;
              int openedTroop = -1;
              ResearchData.getOpenedResearch(index2, index3, GameEngine.Instance.LocalWorldData.Alternate_Ruleset, ref openedBuilding, ref openedCastleBuilding, ref openedTroop);
              if (openedBuilding >= 0 || openedCastleBuilding > 0 || openedTroop > 0)
              {
                CustomSelfDrawPanel.CSDImage nextImage = this.getNextImage();
                if (openedBuilding >= 0)
                  nextImage.Image = (Image) this.getBuildingGFX(openedBuilding);
                if (openedCastleBuilding >= 0)
                  nextImage.Image = (Image) this.getCastleGFX(openedCastleBuilding);
                if (openedTroop >= 0)
                  nextImage.Image = (Image) this.getCastleGFX(openedTroop);
                if (nextImage.Image != null)
                {
                  nextImage.Position = new Point(112 - nextImage.Image.Width / 2, 82 - nextImage.Image.Height / 2);
                  nextImage.Visible = true;
                  control1.addControl((CustomSelfDrawPanel.CSDControl) nextImage);
                }
              }
            }
            else
            {
              if ((int) this.lastData.research[index2] == index3 - 1)
              {
                if (index3 > 1)
                  flag4 = true;
                flag3 = true;
              }
              else if ((int) this.lastData.research[index2] < index3 - 1)
                flag3 = true;
              num6 = 0;
              if (down != 0)
              {
                int openedBuilding = 0;
                int openedCastleBuilding = -1;
                int openedTroop = -1;
                int openedResearch = ResearchData.getOpenedResearch(index2, index3, GameEngine.Instance.LocalWorldData.Alternate_Ruleset, ref openedBuilding, ref openedCastleBuilding, ref openedTroop);
                if (openedResearch >= 0)
                {
                  int rankNeeded4 = -1;
                  bool special4 = false;
                  if (!this.lastDataQueued.isResearchStepOpen(openedResearch, 0, rank, rankSubLevel, ref rankNeeded4, ref special4, GameEngine.Instance.LocalWorldData.EraWorld))
                    down = 2;
                }
              }
              if (right != 0 && !this.lastDataQueued.isResearchStepOpen(index2, index3, rank, rankSubLevel, ref rankNeeded3, ref special3, GameEngine.Instance.LocalWorldData.EraWorld))
                right = 2;
            }
            control1.Image = !flag4 ? (flag3 ? (Image) this.getYellowCircle(dx, dy, num6, 0, 1, down, right, index2, index3 - 1) : (Image) this.getCircle(dx, dy, num6, 0, 1, down, right, index2, index3 - 1)) : (Image) this.getTransitionCircle(dx, dy, num6, 0, 1, down, right, index2, index3 - 1);
            CustomSelfDrawPanel.CSDImage nextImage4 = this.getNextImage();
            nextImage4.Image = flag3 ? (Image) this.getNumberImage(index3, 2) : (Image) this.getNumberImage(index3, num6);
            nextImage4.Position = new Point(control1.Size.Width / 2 - nextImage4.Size.Width / 2, control1.Size.Height / 2 - nextImage4.Size.Height / 2);
            control1.addControl((CustomSelfDrawPanel.CSDControl) nextImage4);
          }
        }
      }
    }

    private void resetImageCache() => this.curImageID = 0;

    private CustomSelfDrawPanel.CSDImage getNextImage()
    {
      if (this.curImageID >= this.imageCache.Count)
      {
        CustomSelfDrawPanel.CSDImage nextImage = new CustomSelfDrawPanel.CSDImage();
        this.imageCache.Add(nextImage);
        ++this.curImageID;
        return nextImage;
      }
      ++this.curImageID;
      return this.imageCache[this.curImageID - 1];
    }

    private void resetLabelCache() => this.curLabelID = 0;

    private CustomSelfDrawPanel.CSDLabel getNextLabel()
    {
      if (this.curLabelID >= this.labelCache.Count)
      {
        CustomSelfDrawPanel.CSDLabel nextLabel = new CustomSelfDrawPanel.CSDLabel();
        this.labelCache.Add(nextLabel);
        ++this.curLabelID;
        return nextLabel;
      }
      ++this.curLabelID;
      return this.labelCache[this.curLabelID - 1];
    }

    private BaseImage getIllBack(
      int dx,
      int dy,
      int up,
      int left,
      int down,
      int right,
      int research)
    {
      if (dx < 0)
      {
        int num = left;
        left = right;
        right = num;
      }
      if (dy < 0)
      {
        int num = up;
        up = down;
        down = num;
      }
      if (up != 0)
      {
        switch (left)
        {
          case 0:
            switch (right)
            {
              case 0:
                switch (down)
                {
                  case 0:
                    return up == 2 ? GFXLibrary.ill_back_bline_1000 : GFXLibrary.ill_back_gline_1000;
                  case 2:
                    return GFXLibrary.ill_back_bline_1010;
                  default:
                    if (up != 2)
                      return GFXLibrary.ill_back_gline_1010;
                    goto case 2;
                }
              case 2:
                return GFXLibrary.ill_back_bline_1100;
              default:
                if (up != 2)
                  return GFXLibrary.ill_back_gline_1100;
                goto case 2;
            }
          case 2:
            return GFXLibrary.ill_back_bline_1001;
          default:
            if (up != 2)
              return GFXLibrary.ill_back_gline_1001;
            goto case 2;
        }
      }
      else if (down != 0)
      {
        switch (left)
        {
          case 0:
            switch (right)
            {
              case 0:
                return down == 2 ? GFXLibrary.ill_back_bline_0010 : GFXLibrary.ill_back_gline_0010;
              case 2:
                return GFXLibrary.ill_back_bline_0110;
              default:
                if (down != 2)
                  return GFXLibrary.ill_back_gline_0110;
                goto case 2;
            }
          case 2:
            return GFXLibrary.ill_back_bline_0011;
          default:
            if (down != 2)
              return GFXLibrary.ill_back_gline_0011;
            goto case 2;
        }
      }
      else
      {
        if (left != 0)
          return right != 0 ? (left == 2 || right == 2 ? GFXLibrary.ill_back_bline_0101 : GFXLibrary.ill_back_gline_0101) : (left == 2 ? GFXLibrary.ill_back_bline_0001 : GFXLibrary.ill_back_gline_0001);
        if (right == 0)
          return GFXLibrary.ill_back_bline_0000;
        return right == 2 ? GFXLibrary.ill_back_bline_0100 : GFXLibrary.ill_back_gline_0100;
      }
    }

    private BaseImage getCircle(
      int dx,
      int dy,
      int mode,
      int up,
      int left,
      int down,
      int right,
      int research,
      int level)
    {
      if (dx < 0)
      {
        int num = left;
        left = right;
        right = num;
      }
      if (dy < 0)
      {
        int num = up;
        up = down;
        down = num;
      }
      switch (mode)
      {
        case 0:
          if (left == 1 && down == 1 && right == 2)
            return GFXLibrary.mix_gcf_0011_bl_0100;
          if (left == 1 && right == 1 && (up == 2 || down == 2) && down == 2)
            return GFXLibrary.mix_gcf_0101_bl_0010;
          if (left == 1 && (up == 2 || right == 2 || down == 2))
          {
            if (right == 2)
              return down == 2 ? GFXLibrary.mix_gcf_0001_bl_0110 : GFXLibrary.mix_gcf_0001_bl_0100;
            if (down == 2)
              return GFXLibrary.mix_gcf_0001_bl_0010;
          }
          return down != 0 ? (right != 0 ? GFXLibrary.gcf_0111 : GFXLibrary.gcf_0011) : (right != 0 ? GFXLibrary.gcf_0101 : GFXLibrary.gcf_0001);
        case 1:
          if (up == 0 && right == 0 && down == 2 && left == 1)
            return GFXLibrary.mix_gch_0001_bl_0010;
          if (up == 0 && right == 2 && down == 0 && left == 1)
            return GFXLibrary.mix_gch_0001_bl_0100;
          if (up == 0 && right == 2 && down == 2 && left == 1)
            return GFXLibrary.mix_gch_0001_bl_0110;
          return down != 0 ? (right != 0 ? GFXLibrary.gch_0111 : GFXLibrary.gch_0011) : (right != 0 ? GFXLibrary.gch_0101 : GFXLibrary.gch_0001);
        default:
          return down != 0 ? (right != 0 ? GFXLibrary.bcf_0111 : GFXLibrary.bcf_0011) : (right != 0 ? GFXLibrary.bcf_0101 : GFXLibrary.bcf_0001);
      }
    }

    private BaseImage getYellowCircle(
      int dx,
      int dy,
      int mode,
      int up,
      int left,
      int down,
      int right,
      int research,
      int level)
    {
      if (dx < 0)
      {
        int num = left;
        left = right;
        right = num;
      }
      if (dy < 0)
      {
        int num = up;
        up = down;
        down = num;
      }
      switch (mode)
      {
        case 0:
          if (left == 1 && down == 1 && right == 2)
            return GFXLibrary.mix_ycf_0011_bl_0100;
          if (left == 1 && right == 1 && (up == 2 || down == 2) && down == 2)
            return GFXLibrary.mix_ycf_0101_bl_0010;
          if (left == 1 && (up == 2 || right == 2 || down == 2))
          {
            if (right == 2)
              return down == 2 ? GFXLibrary.mix_ycf_0001_bl_0110 : GFXLibrary.mix_ycf_0001_bl_0100;
            if (down == 2)
              return GFXLibrary.mix_ycf_0001_bl_0010;
          }
          return down != 0 ? (right != 0 ? GFXLibrary.ycf_0111 : GFXLibrary.ycf_0011) : (right != 0 ? GFXLibrary.ycf_0101 : GFXLibrary.ycf_0001);
        case 1:
          if (up == 0 && right == 0 && down == 2 && left == 1)
            return GFXLibrary.mix_ych_0001_bl_0010;
          if (up == 0 && right == 2 && down == 0 && left == 1)
            return GFXLibrary.mix_ych_0001_bl_0100;
          return up == 0 && right == 2 && down == 2 && left == 1 ? GFXLibrary.mix_ych_0001_bl_0110 : GFXLibrary.ych_0001;
        default:
          return GFXLibrary.bcf_0001;
      }
    }

    private BaseImage getTransitionCircle(
      int dx,
      int dy,
      int mode,
      int up,
      int left,
      int down,
      int right,
      int research,
      int level)
    {
      if (dx < 0)
      {
        int num = left;
        left = right;
        right = num;
      }
      if (dy < 0)
      {
        int num = up;
        up = down;
        down = num;
      }
      if (left == 1 && down == 1 && right == 2)
        return GFXLibrary.ycf_0g1G;
      if (left == 1 && right == 1 && (up == 2 || down == 2) && down == 2)
        return GFXLibrary.ycf_01gG;
      if (left == 1 && (up == 2 || right == 2 || down == 2))
      {
        if (right == 2)
          return down == 2 ? GFXLibrary.ycf_0ggG : GFXLibrary.mix_ycf_000G_bl_0100;
        if (down == 2)
          return GFXLibrary.ycf_00gG;
      }
      return down != 0 ? (right != 0 ? GFXLibrary.ycf_011G : GFXLibrary.ycf_001G) : (right != 0 ? GFXLibrary.ycf_010G : GFXLibrary.ycf_000G);
    }

    private BaseImage getNumberImage(int number, int colour)
    {
      switch (number)
      {
        case 1:
          if (colour == 0)
            return GFXLibrary.tech_number_1_green;
          return colour == 1 ? GFXLibrary.tech_number_1_olive : GFXLibrary.tech_number_1_tan;
        case 2:
          if (colour == 0)
            return GFXLibrary.tech_number_2_green;
          return colour == 1 ? GFXLibrary.tech_number_2_olive : GFXLibrary.tech_number_2_tan;
        case 3:
          if (colour == 0)
            return GFXLibrary.tech_number_3_green;
          return colour == 1 ? GFXLibrary.tech_number_3_olive : GFXLibrary.tech_number_3_tan;
        case 4:
          if (colour == 0)
            return GFXLibrary.tech_number_4_green;
          return colour == 1 ? GFXLibrary.tech_number_4_olive : GFXLibrary.tech_number_4_tan;
        case 5:
          if (colour == 0)
            return GFXLibrary.tech_number_5_green;
          return colour == 1 ? GFXLibrary.tech_number_5_olive : GFXLibrary.tech_number_5_tan;
        case 6:
          if (colour == 0)
            return GFXLibrary.tech_number_6_green;
          return colour == 1 ? GFXLibrary.tech_number_6_olive : GFXLibrary.tech_number_6_tan;
        case 7:
          if (colour == 0)
            return GFXLibrary.tech_number_7_green;
          return colour == 1 ? GFXLibrary.tech_number_7_olive : GFXLibrary.tech_number_7_tan;
        case 8:
          if (colour == 0)
            return GFXLibrary.tech_number_8_green;
          return colour == 1 ? GFXLibrary.tech_number_8_olive : GFXLibrary.tech_number_8_tan;
        case 9:
          if (colour == 0)
            return GFXLibrary.tech_number_9_green;
          return colour == 1 ? GFXLibrary.tech_number_9_olive : GFXLibrary.tech_number_9_tan;
        case 10:
          if (colour == 0)
            return GFXLibrary.tech_number_10_green;
          return colour == 1 ? GFXLibrary.tech_number_10_olive : GFXLibrary.tech_number_10_tan;
        case 11:
          if (colour == 0)
            return GFXLibrary.tech_number_11_green;
          return colour == 1 ? GFXLibrary.tech_number_11_olive : GFXLibrary.tech_number_11_tan;
        case 12:
          if (colour == 0)
            return GFXLibrary.tech_number_12_green;
          return colour == 1 ? GFXLibrary.tech_number_12_olive : GFXLibrary.tech_number_12_tan;
        case 13:
          if (colour == 0)
            return GFXLibrary.tech_number_13_green;
          return colour == 1 ? GFXLibrary.tech_number_13_olive : GFXLibrary.tech_number_13_tan;
        case 14:
          if (colour == 0)
            return GFXLibrary.tech_number_14_green;
          return colour == 1 ? GFXLibrary.tech_number_14_olive : GFXLibrary.tech_number_14_tan;
        case 15:
          if (colour == 0)
            return GFXLibrary.tech_number_15_green;
          return colour == 1 ? GFXLibrary.tech_number_15_olive : GFXLibrary.tech_number_15_tan;
        case 16:
          if (colour == 0)
            return GFXLibrary.tech_number_16_green;
          return colour == 1 ? GFXLibrary.tech_number_16_olive : GFXLibrary.tech_number_16_tan;
        case 17:
          if (colour == 0)
            return GFXLibrary.tech_numbers[10];
          return colour == 1 ? GFXLibrary.tech_numbers[20] : GFXLibrary.tech_numbers[0];
        case 18:
          if (colour == 0)
            return GFXLibrary.tech_numbers[11];
          return colour == 1 ? GFXLibrary.tech_numbers[21] : GFXLibrary.tech_numbers[1];
        case 19:
          if (colour == 0)
            return GFXLibrary.tech_numbers[12];
          return colour == 1 ? GFXLibrary.tech_numbers[22] : GFXLibrary.tech_numbers[2];
        case 20:
          if (colour == 0)
            return GFXLibrary.tech_numbers[13];
          return colour == 1 ? GFXLibrary.tech_numbers[23] : GFXLibrary.tech_numbers[3];
        case 21:
          if (colour == 0)
            return GFXLibrary.tech_numbers[14];
          return colour == 1 ? GFXLibrary.tech_numbers[24] : GFXLibrary.tech_numbers[4];
        case 22:
          if (colour == 0)
            return GFXLibrary.tech_numbers[15];
          return colour == 1 ? GFXLibrary.tech_numbers[25] : GFXLibrary.tech_numbers[5];
        case 23:
          if (colour == 0)
            return GFXLibrary.tech_numbers[16];
          return colour == 1 ? GFXLibrary.tech_numbers[26] : GFXLibrary.tech_numbers[6];
        case 24:
          if (colour == 0)
            return GFXLibrary.tech_numbers[17];
          return colour == 1 ? GFXLibrary.tech_numbers[27] : GFXLibrary.tech_numbers[7];
        case 25:
          if (colour == 0)
            return GFXLibrary.tech_numbers[18];
          return colour == 1 ? GFXLibrary.tech_numbers[28] : GFXLibrary.tech_numbers[8];
        case 26:
          if (colour == 0)
            return GFXLibrary.tech_numbers[19];
          return colour == 1 ? GFXLibrary.tech_numbers[29] : GFXLibrary.tech_numbers[9];
        default:
          return GFXLibrary.tech_number_1_green;
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
      this.Name = nameof (ResearchPanel);
      this.SizeChanged += new EventHandler(this.ResearchPanel2_SizeChanged);
      this.ResumeLayout(false);
    }
  }
}
