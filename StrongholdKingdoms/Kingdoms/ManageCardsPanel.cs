// Decompiled with JetBrains decompiler
// Type: Kingdoms.ManageCardsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using StatTracking;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ManageCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
  {
    private const int MAX_CASHIN_CARDS = 60;
    private IContainer components;
    private CustomSelfDrawPanel.CSDImage TabSelector = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea TabBuyArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea TabCashArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton searchButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton clearSearchButton = new CustomSelfDrawPanel.CSDButton();
    public int PanelMode;
    public int LayoutPanelMode;
    public static int PANEL_MODE_CASH = 1;
    public static int PANEL_MODE_BUY = 2;
    private CardTypes.CardDefinition CatalogFilterDefinition = new CardTypes.CardDefinition();
    private List<CustomSelfDrawPanel.CSDButton> FilterButtons = new List<CustomSelfDrawPanel.CSDButton>();
    private CustomSelfDrawPanel.CSDImageAnim[] SlotAnims = new CustomSelfDrawPanel.CSDImageAnim[5];
    private CustomSelfDrawPanel.CSDImage SlotHolder = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel LabelClickToRemove = new CustomSelfDrawPanel.CSDLabel();
    private string TextRemove = SK.Text("ManageCandsPanel_Cancel_Purchase", "Click Card to cancel purchase") + ": ";
    private string TextRemoveSet = SK.Text("ManageCandsPanel_Remove_From_Set", "Click Card to remove from set") + ": ";
    private string TextEmptySet = SK.Text("ManageCandsPanel_Make_Set", "Click on cards below to make a set of 5");
    private string TextEmptyMultiSet = SK.Text("ManageCandsPanel_Make_MultiSet", "Click on cards below to make a set of at least 5");
    private string TextIncompleteSetStart = SK.Text("ManageCandsPanel_Choose_More", "More Cards Needed") + ": ";
    private string TextCash = SK.Text("ManageCandsPanel_Cash_In_Here", "Click Here to cash in!");
    private string TextCartEmpty = SK.Text("ManageCandsPanel_Buy", "Click on cards below to buy them");
    private string TextCartFull = SK.Text("ManageCandsPanel_Confirm,", "Click Here to confirm purchase!");
    private CustomSelfDrawPanel.CSDImage DynamicPanel = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel DynamicLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage DynamicButton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel DynamicButtonLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertImageScroller[] SymbolScrollers = new CustomSelfDrawPanel.CSDVertImageScroller[5];
    private List<CustomSelfDrawPanel.CSDFloatingText> floatingLabels = new List<CustomSelfDrawPanel.CSDFloatingText>();
    public bool showingbonus;
    private DateTime lastUpdatedProgressBars = DateTime.Now.AddSeconds(30.0);
    private DateTime lastTickCall = DateTime.Now.AddSeconds(-60.0);
    private DateTime lastRefresh = DateTime.Now;
    private CustomSelfDrawPanel.CSDImage buttonCash = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage buttonBonus = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage buttonCatalog = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel labelBuyCash = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.UICardsButtons cardsButtons;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private List<UICard> UICardList = new List<UICard>();
    private List<UICard> UICardListInplay = new List<UICard>();
    private List<UICard> CardCatalog = new List<UICard>();
    private List<UICard> ShoppingCart = new List<UICard>();
    private CustomSelfDrawPanel.CSDImage[] EmptyCards = new CustomSelfDrawPanel.CSDImage[5];
    private UICard[] SetCards = new UICard[60];
    private CustomSelfDrawPanel.CSDLabel cardTitle;
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelTitlePoints = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage imageTitlePoints = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDCheckBox fastCashInCheckBox = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox buyAndPlayCheckBox = new CustomSelfDrawPanel.CSDCheckBox();
    private int currentCardSection = -1;
    private static int BorderPadding = 16;
    private int ContentWidth;
    private int AvailablePanelWidth;
    private int InplayPanelWidth;
    private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
    private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage InplayPanelContent = new CustomSelfDrawPanel.CSDImage();
    private int sortByMode = -1;
    private CustomSelfDrawPanel.CSDImage sortBack = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton sortByName = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton sortByType = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton sortByQuantity = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton compressButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton expandButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
    private Bitmap greenbar = new Bitmap(29, 3);
    private CustomSelfDrawPanel.CSDControl[] Spinners = new CustomSelfDrawPanel.CSDControl[5];
    private CustomSelfDrawPanel.CSDControl[] SpinnerInners = new CustomSelfDrawPanel.CSDControl[5];
    private Dictionary<int, int> symbolOffsets = new Dictionary<int, int>();
    private int[] SymbolTargets = new int[5];
    private bool[] spinning = new bool[5];
    private bool cashingIn;
    private bool fastCashIn;
    private bool buyingCard;
    private DateTime spinstart;
    private int spinspeed;
    private XmlRpcCardsResponse lastCashResponse;
    private string newcardnames = "";
    private int newcardcost;
    private CardTypes.CardDefinition newcarddef;
    private int NumCardsCachingIn;
    private int failedPurchaseCard = -1;
    private int failedPurchaseCost = -1;
    private bool compressedCards;
    private List<UICard> dummyCards = new List<UICard>();
    private int diamondAnimFrame;
    private DateTime diamondAnimStartTime = DateTime.Now;
    private bool playingSpinSound;
    private bool[] spinSoundStopPlayed = new bool[5];
    private int spinSoundSoundID;
    private DateTime spinSoundStopLastTime = DateTime.MinValue;
    private int autoCardUserID;
    private CardTypes.CardDefinition autoCardDef;
    private int autoCardVillageID;
    private bool waitingResponse;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }

    public ManageCardsPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int cardSection)
    {
      this.currentCardSection = cardSection;
      this.clearControls();
      this.mainBackgroundImage.Image = GFXLibrary.dummy;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.ContentWidth = this.Width - 2 * ManageCardsPanel.BorderPadding;
      this.AvailablePanelWidth = 800;
      this.InplayPanelWidth = this.ContentWidth - ManageCardsPanel.BorderPadding - this.AvailablePanelWidth;
      CustomSelfDrawPanel.CSDExtendingPanel control1 = new CustomSelfDrawPanel.CSDExtendingPanel();
      control1.Size = this.Size;
      control1.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control1);
      control1.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
      CustomSelfDrawPanel.CSDImage control2 = new CustomSelfDrawPanel.CSDImage();
      control2.Image = (Image) GFXLibrary.cardpanel_panel_gradient_top_left;
      control2.Size = GFXLibrary.cardpanel_panel_gradient_top_left.Size;
      control2.Position = new Point(0, 0);
      control1.addControl((CustomSelfDrawPanel.CSDControl) control2);
      CustomSelfDrawPanel.CSDImage control3 = new CustomSelfDrawPanel.CSDImage();
      control3.Image = (Image) GFXLibrary.cardpanel_panel_gradient_bottom_right;
      control3.Size = GFXLibrary.cardpanel_panel_gradient_bottom_right.Size;
      control3.Position = new Point(control1.Width - control3.Width - 6, control1.Height - control3.Height - 6);
      control1.addControl((CustomSelfDrawPanel.CSDControl) control3);
      this.AvailablePanel = new CustomSelfDrawPanel.CSDExtendingPanel();
      this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, 375);
      this.AvailablePanel.Position = new Point(8, this.Height - 8 - 375);
      this.AvailablePanel.Alpha = 0.8f;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanel);
      this.AvailablePanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
      int width1 = this.Width;
      int borderPadding = ManageCardsPanel.BorderPadding;
      int width2 = this.AvailablePanel.Width;
      this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      this.closeImage.Size = this.closeImage.Image.Size;
      this.closeImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
      this.closeImage.CustomTooltipID = 10100;
      this.closeImage.Position = new Point(this.Width - 14 - 17, 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeImage);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 40, new Point(this.Width - 1 - 17 - 50 + 3, 5), true);
      CustomSelfDrawPanel.CSDFill control4 = new CustomSelfDrawPanel.CSDFill();
      control4.FillColor = Color.FromArgb((int) byte.MaxValue, 130, 129, 126);
      control4.Size = new Size(this.Width - 10, 1);
      control4.Position = new Point(5, 34);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control4);
      this.cardsButtons = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) this.ParentForm);
      this.cardsButtons.Position = new Point(808, 37);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardsButtons);
      this.labelTitle.Position = new Point(27, 8);
      this.labelTitle.Size = new Size(600, 64);
      this.labelTitle.Text = "";
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
      this.labelTitlePoints.Position = new Point(27, 8);
      this.labelTitlePoints.Size = new Size(600, 64);
      this.labelTitlePoints.Text = "";
      this.labelTitlePoints.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitlePoints.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelTitlePoints.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitlePoints);
      this.imageTitlePoints.Image = (Image) GFXLibrary.cardpanel_manage_card_points_icon;
      this.imageTitlePoints.Position = new Point(400, 5);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.imageTitlePoints);
      this.searchButton.ImageNorm = (Image) GFXLibrary.int_statsscreen_search_button_normal;
      this.searchButton.ImageOver = (Image) GFXLibrary.int_statsscreen_search_button_over;
      this.searchButton.ImageClick = (Image) GFXLibrary.int_statsscreen_search_button_pushed;
      this.searchButton.Position = new Point(811, 7);
      this.searchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchClicked), "StatsPanel_search");
      this.searchButton.CustomTooltipID = 10319;
      this.searchButton.Visible = true;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.searchButton);
      this.clearSearchButton.ImageNorm = (Image) GFXLibrary.int_statsscreen_search_clear_button_normal;
      this.clearSearchButton.ImageOver = (Image) GFXLibrary.int_statsscreen_search_clear_button_over;
      this.clearSearchButton.ImageClick = (Image) GFXLibrary.int_statsscreen_search_clear_button_pushed;
      this.clearSearchButton.Position = new Point(740, 7);
      this.clearSearchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clearSearchClicked), "StatsPanel_clear_search");
      this.clearSearchButton.Visible = false;
      this.clearSearchButton.CustomTooltipID = 10320;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.clearSearchButton);
      this.addPointsData();
      CustomSelfDrawPanel.CSDLabel csdLabel = new CustomSelfDrawPanel.CSDLabel();
      csdLabel.Position = new Point(2 * ManageCardsPanel.BorderPadding + this.AvailablePanelWidth, ManageCardsPanel.BorderPadding);
      csdLabel.Size = new Size(300, 64);
      csdLabel.Text = SK.Text("ManageCandsPanel_Cards_In_Play", "Cards In Play");
      csdLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      csdLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      csdLabel.Color = ARGBColors.White;
      csdLabel.DropShadowColor = ARGBColors.Black;
      this.cardTitle = new CustomSelfDrawPanel.CSDLabel();
      this.cardTitle.Position = new Point(16, 40);
      this.cardTitle.Size = new Size(600, 64);
      this.cardTitle.Text = string.Empty;
      this.cardTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.cardTitle.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.cardTitle.Color = ARGBColors.White;
      this.cardTitle.DropShadowColor = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardTitle);
      this.labelFeedback = new CustomSelfDrawPanel.CSDLabel();
      this.labelFeedback.Position = new Point(16, 500);
      this.labelFeedback.Size = new Size(600, 64);
      this.labelFeedback.Text = "";
      this.labelFeedback.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelFeedback.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.labelFeedback.Color = ARGBColors.White;
      this.labelFeedback.DropShadowColor = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelFeedback);
      this.buttonCash = new CustomSelfDrawPanel.CSDImage();
      this.buttonBonus = new CustomSelfDrawPanel.CSDImage();
      this.buttonCatalog = new CustomSelfDrawPanel.CSDImage();
      this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_normal;
      this.buttonCash.Size = this.buttonCash.Image.Size;
      this.buttonBonus.Image = (Image) GFXLibrary.cardpanel_button_blue_normal;
      this.buttonBonus.Size = this.buttonBonus.Image.Size;
      this.buttonCatalog.Image = (Image) GFXLibrary.cardpanel_button_blue_normal;
      this.buttonCatalog.Size = this.buttonCash.Image.Size;
      this.buttonCash.Position = new Point(this.AvailablePanel.X + this.AvailablePanel.Width / 2 - this.buttonCash.Width, this.cardsButtons.Y + 4);
      this.buttonBonus.Position = new Point(this.buttonCash.X, this.buttonCash.Y);
      this.buttonCatalog.Position = new Point(this.buttonCash.X - this.buttonCash.Width, this.buttonCash.Y);
      this.buttonBonus.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonBonus.Image = (Image) GFXLibrary.cardpanel_button_blue_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonBonus.Image = (Image) GFXLibrary.cardpanel_button_blue_normal));
      this.buttonCash.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_normal));
      this.buttonCatalog.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonCatalog.Image = (Image) GFXLibrary.cardpanel_button_blue_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonCatalog.Image = (Image) GFXLibrary.cardpanel_button_blue_normal));
      this.buttonBonus.Visible = false;
      this.buttonCash.Visible = true;
      this.fastCashIn = Program.mySettings.fastCashIn;
      this.fastCashInCheckBox = new CustomSelfDrawPanel.CSDCheckBox();
      this.fastCashInCheckBox.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.fastCashInCheckBox.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.fastCashInCheckBox.Position = new Point(this.AvailablePanel.X + 590, this.cardsButtons.Y + 160);
      this.fastCashInCheckBox.Checked = this.fastCashIn;
      this.fastCashInCheckBox.CBLabel.Text = SK.Text("ManageCards_multicashin", "Multi-Cash In");
      this.fastCashInCheckBox.CBLabel.Color = ARGBColors.Black;
      this.fastCashInCheckBox.CBLabel.Position = new Point(20, -1);
      this.fastCashInCheckBox.CBLabel.Size = new Size(250, 25);
      this.fastCashInCheckBox.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.fastCashInCheckBox.setCheckChangedDelegate((CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate) (() =>
      {
        Program.mySettings.fastCashIn = this.fastCashIn = this.fastCashInCheckBox.Checked;
        this.RefreshSet();
      }));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.fastCashInCheckBox);
      this.buyAndPlayCheckBox = new CustomSelfDrawPanel.CSDCheckBox();
      this.buyAndPlayCheckBox.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.buyAndPlayCheckBox.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.buyAndPlayCheckBox.Position = new Point(this.AvailablePanel.X + 100, this.cardsButtons.Y + 92);
      this.buyAndPlayCheckBox.Checked = false;
      this.buyAndPlayCheckBox.Visible = false;
      this.buyAndPlayCheckBox.CBLabel.Text = SK.Text("ManageCards_buyAndPlay", "Play Card Immediately");
      this.buyAndPlayCheckBox.CBLabel.Color = ARGBColors.Black;
      this.buyAndPlayCheckBox.CBLabel.Position = new Point(20, -1);
      this.buyAndPlayCheckBox.CBLabel.Size = new Size(250, 25);
      this.buyAndPlayCheckBox.CBLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.buyAndPlayCheckBox.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.buyAndPlayCheckChanged));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.buyAndPlayCheckBox);
      CustomSelfDrawPanel.CSDLabel control5 = new CustomSelfDrawPanel.CSDLabel();
      control5.Position = new Point(0, -2);
      control5.Size = this.buttonCash.Size;
      control5.Text = SK.Text("ManageCandsPanel_Cash_In", "Cash In");
      control5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control5.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control5.Color = ARGBColors.Black;
      this.buttonCash.addControl((CustomSelfDrawPanel.CSDControl) control5);
      CustomSelfDrawPanel.CSDLabel control6 = new CustomSelfDrawPanel.CSDLabel();
      control6.Position = new Point(0, -2);
      control6.Size = this.buttonCash.Size;
      control6.Text = SK.Text("ManageCandsPanel_Cash_In", "Cash In");
      control6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control6.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control6.Color = ARGBColors.Black;
      this.buttonBonus.addControl((CustomSelfDrawPanel.CSDControl) control6);
      this.labelBuyCash = new CustomSelfDrawPanel.CSDLabel();
      this.labelBuyCash.Position = new Point(0, -2);
      this.labelBuyCash.Size = this.buttonCash.Size;
      this.labelBuyCash.Text = SK.Text("ManageCandsPanel_Get_Cards", "Get Cards");
      this.labelBuyCash.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.labelBuyCash.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.labelBuyCash.Color = ARGBColors.Black;
      this.LabelClickToRemove = new CustomSelfDrawPanel.CSDLabel();
      this.LabelClickToRemove.Position = new Point(this.AvailablePanel.X, this.cardsButtons.Y);
      this.LabelClickToRemove.Size = new Size(600, 18);
      this.LabelClickToRemove.Text = "";
      this.LabelClickToRemove.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.LabelClickToRemove.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.LabelClickToRemove.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.LabelClickToRemove);
      this.buttonCatalog.addControl((CustomSelfDrawPanel.CSDControl) this.labelBuyCash);
      this.buttonCatalog.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.SwitchToBuy), "ManageCardsPanel_switch_to_buy_cards");
      if (GameEngine.Instance.cardsManager.ProfileCardsSet.Count < 5)
      {
        this.buttonCash.Colorise = ARGBColors.Gray;
        this.buttonCash.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null, (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null);
        this.buttonCash.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
      }
      else
      {
        this.buttonCash.Colorise = ARGBColors.White;
        this.buttonCash.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_normal));
        this.buttonCash.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.CashClick), "ManageCardsPanel_switch_to_cash_in");
      }
      this.LayoutPanelMode = this.PanelMode = ManageCardsPanel.PANEL_MODE_CASH;
      CardTypes.CardDefinition filter = new CardTypes.CardDefinition();
      GameEngine.Instance.cardsManager.searchProfileCards(filter, "", ((PlayCardsWindow) this.ParentForm).getNameSearchText());
      ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = false;
      this.ResizeAvailable(375);
      this.GetCardsAvailable(false);
      this.RenderCards(this.UICardList);
      this.InitEmptyCards();
      this.InitDynamicPanel();
      this.RefreshSet();
      this.CatalogFilterDefinition.cardCategory = 0;
      this.CatalogFilterDefinition.cardColour = 0;
      this.InitCatalog();
      this.mainBackgroundImage.addControl(new CustomSelfDrawPanel.CSDControl()
      {
        Position = new Point(0, 0),
        Size = this.Size
      });
      this.mainBackgroundImage.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelHandler));
      this.InitFilters();
      this.InitTabs();
      this.mainBackgroundImage.invalidate();
      GameEngine.shiftPressedAlways = false;
    }

    private void InitTabs()
    {
      this.TabSelector.Image = (Image) GFXLibrary.cardpanel_manage_tabs_white_left;
      this.TabSelector.Size = this.TabSelector.Image.Size;
      this.TabSelector.Position = new Point(this.AvailablePanel.X + this.AvailablePanel.Width - this.TabSelector.Width, this.DynamicPanel.Y - this.TabSelector.Height + 6);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.TabSelector);
      this.TabCashArea.Position = new Point(79, 0);
      this.TabCashArea.Size = new Size(118, 30);
      this.TabCashArea.CustomTooltipID = 10103;
      this.TabSelector.addControl((CustomSelfDrawPanel.CSDControl) this.TabCashArea);
      this.TabBuyArea.Position = new Point(196, 0);
      this.TabBuyArea.Size = new Size(118, 30);
      this.TabBuyArea.CustomTooltipID = 10104;
      this.TabSelector.addControl((CustomSelfDrawPanel.CSDControl) this.TabBuyArea);
      this.TabSelector.ClickArea = new Rectangle(196, 0, 118, 30);
      this.TabSelector.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() =>
      {
        if (this.cashingIn || this.buyingCard)
          return;
        if (this.PanelMode == ManageCardsPanel.PANEL_MODE_CASH)
        {
          GameEngine.Instance.playInterfaceSound("ManageCardsPanel_switch_to_buy_cards");
          this.SwitchToBuy();
          this.TabSelector.Image = (Image) GFXLibrary.cardpanel_manage_tabs_white_right;
          this.TabSelector.ClickArea = new Rectangle(79, 0, 118, 30);
        }
        else
        {
          GameEngine.Instance.playInterfaceSound("ManageCardsPanel_switch_to_cash_in");
          this.SwitchToCash();
          this.TabSelector.Image = (Image) GFXLibrary.cardpanel_manage_tabs_white_left;
          this.TabSelector.ClickArea = new Rectangle(196, 0, 118, 30);
        }
      }));
    }

    private void InitFilters()
    {
      foreach (CustomSelfDrawPanel.CSDControl filterButton in this.FilterButtons)
        this.mainBackgroundImage.removeControl(filterButton);
      this.FilterButtons.Clear();
      int currentFilter1 = 0;
      if (GameEngine.Instance.cardsManager.lastUserCardSearchCriteria != null)
        currentFilter1 = GameEngine.Instance.cardsManager.lastUserCardSearchCriteria.newCardCategoryFilter;
      int num1 = 0;
      BaseImage[] cardFiltersAll = GFXLibrary.CardFilters_All;
      int index1 = num1;
      int num2 = index1 + 1;
      int currentFilter2 = currentFilter1;
      this.addFilterButton(0, cardFiltersAll, index1, currentFilter2);
      int num3;
      if ((currentFilter1 & 4096) != 0)
      {
        BaseImage[] cardFiltersFood = GFXLibrary.CardFilters_Food;
        int index2 = num2;
        int num4 = index2 + 1;
        int currentFilter3 = currentFilter1;
        this.addFilterButton(4096, cardFiltersFood, index2, currentFilter3);
        BaseImage[] cardFiltersApples = GFXLibrary.CardFilters_Apples;
        int index3 = num4;
        int num5 = index3 + 1;
        int currentFilter4 = currentFilter1;
        this.addFilterButton(4097, cardFiltersApples, index3, currentFilter4);
        BaseImage[] cardFiltersCheese = GFXLibrary.CardFilters_Cheese;
        int index4 = num5;
        int num6 = index4 + 1;
        int currentFilter5 = currentFilter1;
        this.addFilterButton(4098, cardFiltersCheese, index4, currentFilter5);
        BaseImage[] cardFiltersMeat = GFXLibrary.CardFilters_Meat;
        int index5 = num6;
        int num7 = index5 + 1;
        int currentFilter6 = currentFilter1;
        this.addFilterButton(4099, cardFiltersMeat, index5, currentFilter6);
        BaseImage[] cardFiltersBread = GFXLibrary.CardFilters_Bread;
        int index6 = num7;
        int num8 = index6 + 1;
        int currentFilter7 = currentFilter1;
        this.addFilterButton(4100, cardFiltersBread, index6, currentFilter7);
        BaseImage[] cardFiltersVeg = GFXLibrary.CardFilters_Veg;
        int index7 = num8;
        int num9 = index7 + 1;
        int currentFilter8 = currentFilter1;
        this.addFilterButton(4101, cardFiltersVeg, index7, currentFilter8);
        BaseImage[] cardFiltersFish = GFXLibrary.CardFilters_Fish;
        int index8 = num9;
        int num10 = index8 + 1;
        int currentFilter9 = currentFilter1;
        this.addFilterButton(4102, cardFiltersFish, index8, currentFilter9);
        BaseImage cardTypeButton1 = GFXLibrary.cardTypeButtons[111];
        BaseImage cardTypeButton2 = GFXLibrary.cardTypeButtons[112];
        BaseImage cardTypeButton3 = GFXLibrary.cardTypeButtons[113];
        int index9 = num10;
        num3 = index9 + 1;
        int currentFilter10 = currentFilter1;
        this.addFilterButton(4103, cardTypeButton1, cardTypeButton2, cardTypeButton3, index9, currentFilter10);
      }
      else
      {
        BaseImage[] cardFiltersFood = GFXLibrary.CardFilters_Food;
        int index10 = num2;
        num3 = index10 + 1;
        int currentFilter11 = currentFilter1;
        this.addFilterButton(4096, cardFiltersFood, index10, currentFilter11);
      }
      int num11;
      if ((currentFilter1 & 8192) != 0)
      {
        BaseImage[] filtersResources = GFXLibrary.CardFilters_Resources;
        int index11 = num3;
        int num12 = index11 + 1;
        int currentFilter12 = currentFilter1;
        this.addFilterButton(8192, filtersResources, index11, currentFilter12);
        BaseImage cardTypeButton4 = GFXLibrary.cardTypeButtons[0];
        BaseImage cardTypeButton5 = GFXLibrary.cardTypeButtons[1];
        BaseImage cardTypeButton6 = GFXLibrary.cardTypeButtons[2];
        int index12 = num12;
        int num13 = index12 + 1;
        int currentFilter13 = currentFilter1;
        this.addFilterButton(8193, cardTypeButton4, cardTypeButton5, cardTypeButton6, index12, currentFilter13);
        BaseImage cardTypeButton7 = GFXLibrary.cardTypeButtons[3];
        BaseImage cardTypeButton8 = GFXLibrary.cardTypeButtons[4];
        BaseImage cardTypeButton9 = GFXLibrary.cardTypeButtons[5];
        int index13 = num13;
        int num14 = index13 + 1;
        int currentFilter14 = currentFilter1;
        this.addFilterButton(8194, cardTypeButton7, cardTypeButton8, cardTypeButton9, index13, currentFilter14);
        BaseImage cardTypeButton10 = GFXLibrary.cardTypeButtons[6];
        BaseImage cardTypeButton11 = GFXLibrary.cardTypeButtons[7];
        BaseImage cardTypeButton12 = GFXLibrary.cardTypeButtons[8];
        int index14 = num14;
        int num15 = index14 + 1;
        int currentFilter15 = currentFilter1;
        this.addFilterButton(8195, cardTypeButton10, cardTypeButton11, cardTypeButton12, index14, currentFilter15);
        BaseImage cardTypeButton13 = GFXLibrary.cardTypeButtons[9];
        BaseImage cardTypeButton14 = GFXLibrary.cardTypeButtons[10];
        BaseImage cardTypeButton15 = GFXLibrary.cardTypeButtons[11];
        int index15 = num15;
        num11 = index15 + 1;
        int currentFilter16 = currentFilter1;
        this.addFilterButton(8196, cardTypeButton13, cardTypeButton14, cardTypeButton15, index15, currentFilter16);
      }
      else
      {
        BaseImage[] filtersResources = GFXLibrary.CardFilters_Resources;
        int index16 = num3;
        num11 = index16 + 1;
        int currentFilter17 = currentFilter1;
        this.addFilterButton(8192, filtersResources, index16, currentFilter17);
      }
      int num16;
      if ((currentFilter1 & 16384) != 0)
      {
        BaseImage[] cardFiltersHonour = GFXLibrary.CardFilters_Honour;
        int index17 = num11;
        int num17 = index17 + 1;
        int currentFilter18 = currentFilter1;
        this.addFilterButton(16384, cardFiltersHonour, index17, currentFilter18);
        BaseImage cardTypeButton16 = GFXLibrary.cardTypeButtons[12];
        BaseImage cardTypeButton17 = GFXLibrary.cardTypeButtons[13];
        BaseImage cardTypeButton18 = GFXLibrary.cardTypeButtons[14];
        int index18 = num17;
        int num18 = index18 + 1;
        int currentFilter19 = currentFilter1;
        this.addFilterButton(16385, cardTypeButton16, cardTypeButton17, cardTypeButton18, index18, currentFilter19);
        BaseImage cardTypeButton19 = GFXLibrary.cardTypeButtons[15];
        BaseImage cardTypeButton20 = GFXLibrary.cardTypeButtons[16];
        BaseImage cardTypeButton21 = GFXLibrary.cardTypeButtons[17];
        int index19 = num18;
        int num19 = index19 + 1;
        int currentFilter20 = currentFilter1;
        this.addFilterButton(16386, cardTypeButton19, cardTypeButton20, cardTypeButton21, index19, currentFilter20);
        BaseImage cardTypeButton22 = GFXLibrary.cardTypeButtons[18];
        BaseImage cardTypeButton23 = GFXLibrary.cardTypeButtons[19];
        BaseImage cardTypeButton24 = GFXLibrary.cardTypeButtons[20];
        int index20 = num19;
        int num20 = index20 + 1;
        int currentFilter21 = currentFilter1;
        this.addFilterButton(16387, cardTypeButton22, cardTypeButton23, cardTypeButton24, index20, currentFilter21);
        BaseImage cardTypeButton25 = GFXLibrary.cardTypeButtons[21];
        BaseImage cardTypeButton26 = GFXLibrary.cardTypeButtons[22];
        BaseImage cardTypeButton27 = GFXLibrary.cardTypeButtons[23];
        int index21 = num20;
        int num21 = index21 + 1;
        int currentFilter22 = currentFilter1;
        this.addFilterButton(16388, cardTypeButton25, cardTypeButton26, cardTypeButton27, index21, currentFilter22);
        BaseImage cardTypeButton28 = GFXLibrary.cardTypeButtons[24];
        BaseImage cardTypeButton29 = GFXLibrary.cardTypeButtons[25];
        BaseImage cardTypeButton30 = GFXLibrary.cardTypeButtons[26];
        int index22 = num21;
        num16 = index22 + 1;
        int currentFilter23 = currentFilter1;
        this.addFilterButton(16389, cardTypeButton28, cardTypeButton29, cardTypeButton30, index22, currentFilter23);
        if (GameEngine.Instance.cardsManager.NewCategoriesAvailable_Salt)
          this.addFilterButton(16390, GFXLibrary.cardTypeButtons[27], GFXLibrary.cardTypeButtons[28], GFXLibrary.cardTypeButtons[29], num16++, currentFilter1);
        if (GameEngine.Instance.cardsManager.NewCategoriesAvailable_Spice)
          this.addFilterButton(16391, GFXLibrary.cardTypeButtons[30], GFXLibrary.cardTypeButtons[31], GFXLibrary.cardTypeButtons[32], num16++, currentFilter1);
        if (GameEngine.Instance.cardsManager.NewCategoriesAvailable_Silk)
          this.addFilterButton(16392, GFXLibrary.cardTypeButtons[33], GFXLibrary.cardTypeButtons[34], GFXLibrary.cardTypeButtons[35], num16++, currentFilter1);
      }
      else
      {
        BaseImage[] cardFiltersHonour = GFXLibrary.CardFilters_Honour;
        int index23 = num11;
        num16 = index23 + 1;
        int currentFilter24 = currentFilter1;
        this.addFilterButton(16384, cardFiltersHonour, index23, currentFilter24);
      }
      int num22;
      if ((currentFilter1 & 32768) != 0)
      {
        BaseImage[] cardFiltersWeapons2 = GFXLibrary.CardFilters_Weapons2;
        int index24 = num16;
        int num23 = index24 + 1;
        int currentFilter25 = currentFilter1;
        this.addFilterButton(32768, cardFiltersWeapons2, index24, currentFilter25);
        BaseImage cardTypeButton31 = GFXLibrary.cardTypeButtons[36];
        BaseImage cardTypeButton32 = GFXLibrary.cardTypeButtons[37];
        BaseImage cardTypeButton33 = GFXLibrary.cardTypeButtons[38];
        int index25 = num23;
        int num24 = index25 + 1;
        int currentFilter26 = currentFilter1;
        this.addFilterButton(32769, cardTypeButton31, cardTypeButton32, cardTypeButton33, index25, currentFilter26);
        BaseImage cardTypeButton34 = GFXLibrary.cardTypeButtons[39];
        BaseImage cardTypeButton35 = GFXLibrary.cardTypeButtons[40];
        BaseImage cardTypeButton36 = GFXLibrary.cardTypeButtons[41];
        int index26 = num24;
        int num25 = index26 + 1;
        int currentFilter27 = currentFilter1;
        this.addFilterButton(32770, cardTypeButton34, cardTypeButton35, cardTypeButton36, index26, currentFilter27);
        BaseImage cardTypeButton37 = GFXLibrary.cardTypeButtons[42];
        BaseImage cardTypeButton38 = GFXLibrary.cardTypeButtons[43];
        BaseImage cardTypeButton39 = GFXLibrary.cardTypeButtons[44];
        int index27 = num25;
        int num26 = index27 + 1;
        int currentFilter28 = currentFilter1;
        this.addFilterButton(32771, cardTypeButton37, cardTypeButton38, cardTypeButton39, index27, currentFilter28);
        BaseImage cardTypeButton40 = GFXLibrary.cardTypeButtons[45];
        BaseImage cardTypeButton41 = GFXLibrary.cardTypeButtons[46];
        BaseImage cardTypeButton42 = GFXLibrary.cardTypeButtons[47];
        int index28 = num26;
        num22 = index28 + 1;
        int currentFilter29 = currentFilter1;
        this.addFilterButton(32772, cardTypeButton40, cardTypeButton41, cardTypeButton42, index28, currentFilter29);
        if (GameEngine.Instance.cardsManager.NewCategoriesAvailable_Catapults)
          this.addFilterButton(32773, GFXLibrary.cardTypeButtons[48], GFXLibrary.cardTypeButtons[49], GFXLibrary.cardTypeButtons[50], num22++, currentFilter1);
      }
      else
      {
        BaseImage[] cardFiltersWeapons2 = GFXLibrary.CardFilters_Weapons2;
        int index29 = num16;
        num22 = index29 + 1;
        int currentFilter30 = currentFilter1;
        this.addFilterButton(32768, cardFiltersWeapons2, index29, currentFilter30);
      }
      int num27;
      if ((currentFilter1 & 65536) != 0)
      {
        BaseImage[] cardFiltersCastle = GFXLibrary.CardFilters_Castle;
        int index30 = num22;
        int num28 = index30 + 1;
        int currentFilter31 = currentFilter1;
        this.addFilterButton(65536, cardFiltersCastle, index30, currentFilter31);
        BaseImage cardTypeButton43 = GFXLibrary.cardTypeButtons[51];
        BaseImage cardTypeButton44 = GFXLibrary.cardTypeButtons[52];
        BaseImage cardTypeButton45 = GFXLibrary.cardTypeButtons[53];
        int index31 = num28;
        int num29 = index31 + 1;
        int currentFilter32 = currentFilter1;
        this.addFilterButton(65537, cardTypeButton43, cardTypeButton44, cardTypeButton45, index31, currentFilter32);
        BaseImage cardTypeButton46 = GFXLibrary.cardTypeButtons[54];
        BaseImage cardTypeButton47 = GFXLibrary.cardTypeButtons[55];
        BaseImage cardTypeButton48 = GFXLibrary.cardTypeButtons[56];
        int index32 = num29;
        int num30 = index32 + 1;
        int currentFilter33 = currentFilter1;
        this.addFilterButton(65538, cardTypeButton46, cardTypeButton47, cardTypeButton48, index32, currentFilter33);
        BaseImage cardTypeButton49 = GFXLibrary.cardTypeButtons[57];
        BaseImage cardTypeButton50 = GFXLibrary.cardTypeButtons[58];
        BaseImage cardTypeButton51 = GFXLibrary.cardTypeButtons[59];
        int index33 = num30;
        int num31 = index33 + 1;
        int currentFilter34 = currentFilter1;
        this.addFilterButton(65539, cardTypeButton49, cardTypeButton50, cardTypeButton51, index33, currentFilter34);
        BaseImage cardTypeButton52 = GFXLibrary.cardTypeButtons[60];
        BaseImage cardTypeButton53 = GFXLibrary.cardTypeButtons[61];
        BaseImage cardTypeButton54 = GFXLibrary.cardTypeButtons[62];
        int index34 = num31;
        num27 = index34 + 1;
        int currentFilter35 = currentFilter1;
        this.addFilterButton(65540, cardTypeButton52, cardTypeButton53, cardTypeButton54, index34, currentFilter35);
      }
      else
      {
        BaseImage[] cardFiltersCastle = GFXLibrary.CardFilters_Castle;
        int index35 = num22;
        num27 = index35 + 1;
        int currentFilter36 = currentFilter1;
        this.addFilterButton(65536, cardFiltersCastle, index35, currentFilter36);
      }
      int num32;
      if ((currentFilter1 & 131072) != 0)
      {
        BaseImage[] cardFiltersArmy = GFXLibrary.CardFilters_Army;
        int index36 = num27;
        int num33 = index36 + 1;
        int currentFilter37 = currentFilter1;
        this.addFilterButton(131072, cardFiltersArmy, index36, currentFilter37);
        BaseImage cardTypeButton55 = GFXLibrary.cardTypeButtons[63];
        BaseImage cardTypeButton56 = GFXLibrary.cardTypeButtons[64];
        BaseImage cardTypeButton57 = GFXLibrary.cardTypeButtons[65];
        int index37 = num33;
        int num34 = index37 + 1;
        int currentFilter38 = currentFilter1;
        this.addFilterButton(131073, cardTypeButton55, cardTypeButton56, cardTypeButton57, index37, currentFilter38);
        BaseImage cardTypeButton58 = GFXLibrary.cardTypeButtons[66];
        BaseImage cardTypeButton59 = GFXLibrary.cardTypeButtons[67];
        BaseImage cardTypeButton60 = GFXLibrary.cardTypeButtons[68];
        int index38 = num34;
        int num35 = index38 + 1;
        int currentFilter39 = currentFilter1;
        this.addFilterButton(131074, cardTypeButton58, cardTypeButton59, cardTypeButton60, index38, currentFilter39);
        BaseImage cardTypeButton61 = GFXLibrary.cardTypeButtons[69];
        BaseImage cardTypeButton62 = GFXLibrary.cardTypeButtons[70];
        BaseImage cardTypeButton63 = GFXLibrary.cardTypeButtons[71];
        int index39 = num35;
        int num36 = index39 + 1;
        int currentFilter40 = currentFilter1;
        this.addFilterButton(131075, cardTypeButton61, cardTypeButton62, cardTypeButton63, index39, currentFilter40);
        BaseImage cardTypeButton64 = GFXLibrary.cardTypeButtons[72];
        BaseImage cardTypeButton65 = GFXLibrary.cardTypeButtons[73];
        BaseImage cardTypeButton66 = GFXLibrary.cardTypeButtons[74];
        int index40 = num36;
        num32 = index40 + 1;
        int currentFilter41 = currentFilter1;
        this.addFilterButton(131076, cardTypeButton64, cardTypeButton65, cardTypeButton66, index40, currentFilter41);
        if (GameEngine.Instance.cardsManager.NewCategoriesAvailable_Strategy)
          this.addFilterButton(131077, GFXLibrary.cardTypeButtons[75], GFXLibrary.cardTypeButtons[76], GFXLibrary.cardTypeButtons[77], num32++, currentFilter1);
      }
      else
      {
        BaseImage[] cardFiltersArmy = GFXLibrary.CardFilters_Army;
        int index41 = num27;
        num32 = index41 + 1;
        int currentFilter42 = currentFilter1;
        this.addFilterButton(131072, cardFiltersArmy, index41, currentFilter42);
      }
      int num37;
      if ((currentFilter1 & 262144) != 0)
      {
        BaseImage[] filtersSpecialist = GFXLibrary.CardFilters_Specialist;
        int index42 = num32;
        int num38 = index42 + 1;
        int currentFilter43 = currentFilter1;
        this.addFilterButton(262144, filtersSpecialist, index42, currentFilter43);
        BaseImage cardTypeButton67 = GFXLibrary.cardTypeButtons[78];
        BaseImage cardTypeButton68 = GFXLibrary.cardTypeButtons[79];
        BaseImage cardTypeButton69 = GFXLibrary.cardTypeButtons[80];
        int index43 = num38;
        int num39 = index43 + 1;
        int currentFilter44 = currentFilter1;
        this.addFilterButton(262145, cardTypeButton67, cardTypeButton68, cardTypeButton69, index43, currentFilter44);
        BaseImage cardTypeButton70 = GFXLibrary.cardTypeButtons[81];
        BaseImage cardTypeButton71 = GFXLibrary.cardTypeButtons[82];
        BaseImage cardTypeButton72 = GFXLibrary.cardTypeButtons[83];
        int index44 = num39;
        int num40 = index44 + 1;
        int currentFilter45 = currentFilter1;
        this.addFilterButton(262146, cardTypeButton70, cardTypeButton71, cardTypeButton72, index44, currentFilter45);
        BaseImage cardTypeButton73 = GFXLibrary.cardTypeButtons[84];
        BaseImage cardTypeButton74 = GFXLibrary.cardTypeButtons[85];
        BaseImage cardTypeButton75 = GFXLibrary.cardTypeButtons[86];
        int index45 = num40;
        int num41 = index45 + 1;
        int currentFilter46 = currentFilter1;
        this.addFilterButton(262147, cardTypeButton73, cardTypeButton74, cardTypeButton75, index45, currentFilter46);
        BaseImage cardTypeButton76 = GFXLibrary.cardTypeButtons[87];
        BaseImage cardTypeButton77 = GFXLibrary.cardTypeButtons[88];
        BaseImage cardTypeButton78 = GFXLibrary.cardTypeButtons[89];
        int index46 = num41;
        int num42 = index46 + 1;
        int currentFilter47 = currentFilter1;
        this.addFilterButton(262148, cardTypeButton76, cardTypeButton77, cardTypeButton78, index46, currentFilter47);
        BaseImage cardTypeButton79 = GFXLibrary.cardTypeButtons[90];
        BaseImage cardTypeButton80 = GFXLibrary.cardTypeButtons[91];
        BaseImage cardTypeButton81 = GFXLibrary.cardTypeButtons[92];
        int index47 = num42;
        int num43 = index47 + 1;
        int currentFilter48 = currentFilter1;
        this.addFilterButton(262149, cardTypeButton79, cardTypeButton80, cardTypeButton81, index47, currentFilter48);
        BaseImage cardTypeButton82 = GFXLibrary.cardTypeButtons[93];
        BaseImage cardTypeButton83 = GFXLibrary.cardTypeButtons[94];
        BaseImage cardTypeButton84 = GFXLibrary.cardTypeButtons[95];
        int index48 = num43;
        int num44 = index48 + 1;
        int currentFilter49 = currentFilter1;
        this.addFilterButton(262150, cardTypeButton82, cardTypeButton83, cardTypeButton84, index48, currentFilter49);
        if (GameEngine.Instance.cardsManager.NewCategoriesAvailable_Capacity)
          this.addFilterButton(262151, GFXLibrary.cardTypeButtons[96], GFXLibrary.cardTypeButtons[97], GFXLibrary.cardTypeButtons[98], num44++, currentFilter1);
        BaseImage cardTypeButton85 = GFXLibrary.cardTypeButtons[99];
        BaseImage cardTypeButton86 = GFXLibrary.cardTypeButtons[100];
        BaseImage cardTypeButton87 = GFXLibrary.cardTypeButtons[101];
        int index49 = num44;
        num37 = index49 + 1;
        int currentFilter50 = currentFilter1;
        this.addFilterButton(262152, cardTypeButton85, cardTypeButton86, cardTypeButton87, index49, currentFilter50);
      }
      else
      {
        BaseImage[] filtersSpecialist = GFXLibrary.CardFilters_Specialist;
        int index50 = num32;
        num37 = index50 + 1;
        int currentFilter51 = currentFilter1;
        this.addFilterButton(262144, filtersSpecialist, index50, currentFilter51);
      }
      if (!GameEngine.Instance.cardsManager.NewCategoriesAvailable_Parish)
        return;
      int num45;
      if ((currentFilter1 & 524288) != 0)
      {
        BaseImage[] cardFiltersParish = GFXLibrary.CardFilters_Parish;
        int index51 = num37;
        num45 = index51 + 1;
        int currentFilter52 = currentFilter1;
        this.addFilterButton(524288, cardFiltersParish, index51, currentFilter52);
      }
      else
      {
        BaseImage[] cardFiltersParish = GFXLibrary.CardFilters_Parish;
        int index52 = num37;
        num45 = index52 + 1;
        int currentFilter53 = currentFilter1;
        this.addFilterButton(524288, cardFiltersParish, index52, currentFilter53);
      }
    }

    private void addFilterButton(
      int category,
      BaseImage[] buttonImage,
      int index,
      int currentFilter)
    {
      this.addFilterButton(category, buttonImage[GFXLibrary.ButtonStateNormal], buttonImage[GFXLibrary.ButtonStateOver], buttonImage[GFXLibrary.ButtonStatePressed], index, currentFilter);
    }

    private void addFilterButton(
      int category,
      BaseImage normalImage,
      BaseImage overImage,
      BaseImage clickedImage,
      int index,
      int currentFilter)
    {
      int num1 = 23;
      int num2 = 3;
      if (GameEngine.Instance.cardsManager.NewCategoriesAvailable_FullHeight)
      {
        num1 = 21;
        num2 = 4;
      }
      CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton();
      if (currentFilter == category)
      {
        control.ImageNorm = (Image) overImage;
        control.ImageOver = (Image) overImage;
        control.ImageClick = (Image) overImage;
        control.Data = category;
        control.CustomTooltipData = category;
        control.CustomTooltipID = 10105;
        control.ClipRect = new Rectangle(0, 6, 51, 22);
        control.Position = new Point(this.AvailablePanel.X + this.AvailablePanel.Width - 84, this.AvailablePanel.Y + num2 + index * num1);
      }
      else
      {
        control.ImageNorm = (Image) normalImage;
        control.ImageOver = (Image) overImage;
        control.ImageClick = (Image) clickedImage;
        control.Data = category;
        control.CustomTooltipData = category;
        control.CustomTooltipID = 10105;
        control.Position = new Point(this.AvailablePanel.X + this.AvailablePanel.Width - 84, this.AvailablePanel.Y + num2 + index * num1);
        control.ClipRect = new Rectangle(0, 6, 51, 22);
        control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.NewFilterClick), "PlayCardsPanel_filter");
      }
      this.FilterButtons.Add(control);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control);
    }

    public void NewFilterClick()
    {
      if (this.cashingIn || this.buyingCard)
        return;
      int data = this.ClickedControl.Data;
      if (this.PanelMode == ManageCardsPanel.PANEL_MODE_CASH)
      {
        CardTypes.CardDefinition filter = new CardTypes.CardDefinition();
        filter.newCardCategoryFilter = data;
        this.CatalogFilterDefinition = new CardTypes.CardDefinition();
        this.CatalogFilterDefinition.newCardCategoryFilter = data;
        if ((data & (int) byte.MaxValue) == 0)
        {
          if (!this.searchButton.Visible && !((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible)
            this.searchButton.Visible = true;
          ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = !this.searchButton.Visible;
        }
        else
        {
          ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = false;
          this.searchButton.Visible = false;
        }
        GameEngine.Instance.cardsManager.searchProfileCards(filter, "", ((PlayCardsWindow) this.ParentForm).getNameSearchText());
        this.SwitchToCash();
      }
      else if (this.PanelMode == ManageCardsPanel.PANEL_MODE_BUY)
      {
        this.CatalogFilterDefinition = new CardTypes.CardDefinition();
        this.CatalogFilterDefinition.newCardCategoryFilter = data;
        CardTypes.CardDefinition cardDefinition = new CardTypes.CardDefinition();
        cardDefinition.newCardCategoryFilter = data;
        if ((data & (int) byte.MaxValue) == 0)
        {
          if (!this.searchButton.Visible && !((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible)
            this.searchButton.Visible = true;
          ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = !this.searchButton.Visible;
        }
        else
        {
          ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = false;
          this.searchButton.Visible = false;
        }
        GameEngine.Instance.cardsManager.lastUserCardSearchCriteria = cardDefinition;
        GameEngine.Instance.cardsManager.lastUserCardNameFilter = ((PlayCardsWindow) this.ParentForm).getNameSearchText();
        this.SwitchToBuy();
      }
      this.clearSearchButton.Visible = ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible;
    }

    private void searchClicked()
    {
      this.searchButton.Visible = false;
      this.clearSearchButton.Visible = true;
      ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = true;
      ((PlayCardsWindow) this.ParentForm).tbSearchBox.Focus();
      this.handleSearchTextChanged();
    }

    private void clearSearchClicked()
    {
      this.searchButton.Visible = true;
      this.clearSearchButton.Visible = false;
      ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = false;
      this.handleSearchTextChanged();
    }

    public void handleSearchTextChanged()
    {
      if (this.PanelMode == ManageCardsPanel.PANEL_MODE_CASH)
      {
        GameEngine.Instance.cardsManager.searchProfileCardsRedoLast(((PlayCardsWindow) this.ParentForm).getNameSearchText());
        this.SwitchToCash();
      }
      else
      {
        GameEngine.Instance.cardsManager.lastUserCardNameFilter = ((PlayCardsWindow) this.ParentForm).getNameSearchText();
        this.SwitchToBuy();
      }
    }

    private void mouseWheelHandler(int delta)
    {
      if ((delta <= 0 || this.scrollbarAvailable.Value - delta * 15 <= 0) && (delta >= 0 || this.scrollbarAvailable.Value - delta * 15 >= this.scrollbarAvailable.Max) || this.cashingIn || this.buyingCard)
        return;
      this.scrollbarAvailable.Value += delta * -15;
      this.AvailableContentScroll();
    }

    private void InitDynamicPanel()
    {
      this.DynamicPanel.Position = new Point((this.EmptyCards[4].X + this.EmptyCards[4].Width) / 2 - 6, this.EmptyCards[0].Y / 2);
      this.DynamicPanel.Size = new Size(this.cardsButtons.X - this.DynamicPanel.Position.X, this.EmptyCards[0].Height / 2);
      this.DynamicLabel = new CustomSelfDrawPanel.CSDLabel();
      this.DynamicLabel.Position = new Point(0, 0);
      this.DynamicLabel.Size = this.DynamicPanel.Size;
      this.DynamicLabel.Text = this.fastCashIn ? this.TextEmptyMultiSet : this.TextEmptySet;
      this.DynamicLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.DynamicLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.DynamicLabel.Color = ARGBColors.Black;
      this.DynamicPanel.addControl((CustomSelfDrawPanel.CSDControl) this.DynamicLabel);
      this.DynamicLabel.Visible = true;
      this.DynamicButton = new CustomSelfDrawPanel.CSDImage();
      this.DynamicButton.Image = (Image) GFXLibrary.cardpanel_cashin_normal;
      this.DynamicButton.Size = this.DynamicButton.Image.Size;
      this.DynamicButton.Position = new Point(this.DynamicPanel.Width / 2 - this.DynamicButton.Width / 2, this.DynamicPanel.Height / 2 - this.DynamicButton.Height / 2);
      this.DynamicPanel.addControl((CustomSelfDrawPanel.CSDControl) this.DynamicButton);
      this.DynamicButton.Visible = false;
      this.DynamicButton.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.DynamicButton.Image = (Image) GFXLibrary.cardpanel_cashin_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.DynamicButton.Image = (Image) GFXLibrary.cardpanel_cashin_normal));
      this.DynamicButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.CashClick), "ManageCardsPanel_switch_to_cash_in");
      this.DynamicButtonLabel = new CustomSelfDrawPanel.CSDLabel();
      this.DynamicButtonLabel.Position = new Point(119, 21);
      this.DynamicButtonLabel.Size = new Size(144, 66);
      this.DynamicButtonLabel.Text = SK.Text("ManageCandsPanel_Cash_In", "Cash In");
      this.DynamicButtonLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.DynamicButtonLabel.Font = !(Program.mySettings.LanguageIdent == "ru") ? FontManager.GetFont("Arial", 12f, FontStyle.Regular) : FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.DynamicButtonLabel.Color = ARGBColors.Black;
      this.DynamicButtonLabel.Visible = true;
      this.DynamicButton.addControl((CustomSelfDrawPanel.CSDControl) this.DynamicButtonLabel);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.DynamicPanel);
      this.InitSpinners();
    }

    private void ResizeAvailable(int height)
    {
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.scrollbarAvailable);
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanel);
      this.AvailablePanel.clearDirectControlsOnly();
      this.AvailablePanelContent.clearDirectControlsOnly();
      this.AvailablePanel = new CustomSelfDrawPanel.CSDExtendingPanel();
      this.scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
      this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, height);
      this.AvailablePanel.Position = new Point(8, this.Height - 8 - height);
      this.AvailablePanel.Alpha = 0.8f;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanel);
      this.AvailablePanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
      this.mainBackgroundImage.invalidate();
      this.sortBack.clearControls();
      this.sortBack.Image = (Image) GFXLibrary.sort_back;
      this.sortBack.Position = new Point(8, this.AvailablePanel.Height - 37);
      this.sortBack.Visible = true;
      this.AvailablePanel.addControl((CustomSelfDrawPanel.CSDControl) this.sortBack);
      this.sortByName.ImageNorm = (Image) GFXLibrary.sort_normal;
      this.sortByName.ImageOver = (Image) GFXLibrary.sort_over;
      this.sortByName.ImageClick = (Image) GFXLibrary.sort_in;
      this.sortByName.Position = new Point(7, 4);
      this.sortByName.Text.Text = SK.Text("Card_Sorting_Name", "Sort By Name");
      this.sortByName.Text.Color = ARGBColors.White;
      this.sortByName.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.sortByName.TextYOffset = -1;
      this.sortByName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByNameClicked), "ManageCardsPanel_sort_by_name");
      this.sortBack.addControl((CustomSelfDrawPanel.CSDControl) this.sortByName);
      this.sortByType.ImageNorm = (Image) GFXLibrary.sort_normal;
      this.sortByType.ImageOver = (Image) GFXLibrary.sort_over;
      this.sortByType.ImageClick = (Image) GFXLibrary.sort_in;
      this.sortByType.Position = new Point(228, 4);
      this.sortByType.Text.Text = SK.Text("Card_Sorting_Type", "Sort By Type");
      this.sortByType.Text.Color = ARGBColors.White;
      this.sortByType.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.sortByType.TextYOffset = -1;
      this.sortByType.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByTypeClicked), "ManageCardsPanel_sort_by_type");
      this.sortBack.addControl((CustomSelfDrawPanel.CSDControl) this.sortByType);
      this.sortByQuantity.ImageNorm = (Image) GFXLibrary.sort_normal;
      this.sortByQuantity.ImageOver = (Image) GFXLibrary.sort_over;
      this.sortByQuantity.ImageClick = (Image) GFXLibrary.sort_in;
      this.sortByQuantity.Position = new Point(449, 4);
      this.sortByQuantity.Text.Text = SK.Text("Card_Sorting_Quantity", "Sort By Quantity");
      this.sortByQuantity.Text.Color = ARGBColors.White;
      this.sortByQuantity.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.sortByQuantity.TextYOffset = -1;
      this.sortByQuantity.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByQuantityClicked), "PlayCardsPanel_sort_by_type");
      this.sortBack.addControl((CustomSelfDrawPanel.CSDControl) this.sortByQuantity);
      this.compressButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_minus_norm;
      this.compressButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_minus_over;
      this.compressButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_minus_in;
      this.compressButton.Position = new Point(673, 16);
      this.compressButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.compressClicked), "ManageCardsPanel_compressed_cards");
      this.sortBack.addControl((CustomSelfDrawPanel.CSDControl) this.compressButton);
      this.expandButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_plus_norm;
      this.expandButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_plus_over;
      this.expandButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_plus_in;
      this.expandButton.Position = new Point(673, -2);
      this.expandButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.expandClicked), "ManageCardsPanel_expand_cards");
      this.sortBack.addControl((CustomSelfDrawPanel.CSDControl) this.expandButton);
      if (this.sortByMode == 0 || this.sortByMode == 2)
      {
        this.sortByName.Alpha = 0.5f;
        this.sortByType.Alpha = 1f;
        this.sortByQuantity.Alpha = 0.5f;
      }
      else if (this.sortByMode == 1 || this.sortByMode == 3)
      {
        this.sortByName.Alpha = 1f;
        this.sortByType.Alpha = 0.5f;
        this.sortByQuantity.Alpha = 0.5f;
      }
      else if (this.sortByMode == 7 || this.sortByMode == 8)
      {
        this.sortByName.Alpha = 0.5f;
        this.sortByType.Alpha = 0.5f;
        this.sortByQuantity.Alpha = 1f;
      }
      else
      {
        this.sortByName.Alpha = 1f;
        this.sortByType.Alpha = 1f;
        this.sortByQuantity.Alpha = 1f;
      }
    }

    public void SwitchToBuy()
    {
      if (this.cashingIn || this.buyingCard)
        return;
      this.LayoutPanelMode = ManageCardsPanel.PANEL_MODE_BUY;
      this.InitCatalog();
      this.ResizeAvailable(375);
      this.buttonCatalog.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.SwitchToCash), "ManageCardsPanel_switch_to_cash_in");
      this.RenderCards(this.CardCatalog);
      this.labelTitle.Text = SK.Text("ManageCandsPanel_Get_New_Cards_Points", "Get New Cards: Current Card Points");
      this.addPointsData();
      if (GameEngine.Instance.World.FakeCardPoints > 0)
      {
        CustomSelfDrawPanel.CSDLabel labelTitlePoints = this.labelTitlePoints;
        labelTitlePoints.Text = labelTitlePoints.Text + " (+" + GameEngine.Instance.World.FakeCardPoints.ToString() + ")";
      }
      this.DynamicButton.Visible = false;
      this.DynamicButtonLabel.Visible = false;
      this.DynamicLabel.Visible = true;
      this.fastCashInCheckBox.Visible = false;
      if (this.failedPurchaseCard != -1)
      {
        int profileCardpoints = GameEngine.Instance.World.ProfileCardpoints;
        int failedPurchaseCost = this.failedPurchaseCost;
        foreach (UICard uiCard in this.ShoppingCart)
          failedPurchaseCost += uiCard.Definition.cardPoints;
        if (failedPurchaseCost <= profileCardpoints)
        {
          GameEngine.Instance.cardsManager.ShoppingCartCards.Add(this.failedPurchaseCard);
          this.failedPurchaseCard = -1;
          this.failedPurchaseCost = -1;
        }
      }
      this.RefreshCart();
      foreach (CustomSelfDrawPanel.CSDControl emptyCard in this.EmptyCards)
        emptyCard.Visible = false;
      foreach (CustomSelfDrawPanel.CSDControl setCard in this.SetCards)
        setCard.Visible = false;
      this.buttonCatalog.Colorise = ARGBColors.Gray;
      this.buttonCatalog.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null, (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null);
      this.buttonCatalog.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
      this.buttonCash.Colorise = ARGBColors.White;
      this.buttonCash.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_normal));
      this.buttonCash.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.SwitchToCash), "ManageCardsPanel_switch_to_cash_in");
      if (GameEngine.Instance.World.getTutorialStage() != 102)
      {
        this.InitFilters();
      }
      else
      {
        foreach (CustomSelfDrawPanel.CSDControl filterButton in this.FilterButtons)
          this.mainBackgroundImage.removeControl(filterButton);
        this.FilterButtons.Clear();
      }
      this.PanelMode = ManageCardsPanel.PANEL_MODE_BUY;
      this.buyAndPlayCheckBox.Visible = this.ShoppingCart.Count == 1 && GameEngine.Instance.World.getTutorialStage() != 102;
      this.buyAndPlayCheckBox.Checked = false;
      this.sortByQuantity.Text.Text = SK.Text("Card_Sorting_Price", "Sort By Price");
    }

    private void addPointsData()
    {
      Graphics graphics = this.CreateGraphics();
      Size size = graphics.MeasureString(this.labelTitle.Text, this.labelTitle.Font, 1000).ToSize();
      graphics.Dispose();
      this.imageTitlePoints.Position = new Point(this.labelTitle.X + size.Width + 5, 5);
      this.labelTitlePoints.Position = new Point(this.labelTitle.X + size.Width + 35, this.labelTitle.Y);
      this.labelTitlePoints.Text = GameEngine.Instance.World.ProfileCardpoints.ToString();
    }

    private void SwitchToCash()
    {
      if (this.cashingIn || this.buyingCard)
        return;
      this.LayoutPanelMode = ManageCardsPanel.PANEL_MODE_CASH;
      GameEngine.Instance.cardsManager.searchProfileCardsRedoLast();
      this.ResizeAvailable(375);
      this.GetCardsAvailable(false);
      this.RenderCards(this.UICardList);
      this.InitEmptyCards();
      this.RefreshSet();
      this.InitSpinners();
      this.buttonCatalog.Position = new Point(this.buttonCash.X - this.buttonCash.Width, this.buttonCash.Y);
      this.buttonCatalog.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.SwitchToBuy), "ManageCardsPanel_switch_to_buy_cards");
      this.labelBuyCash.Text = SK.Text("ManageCandsPanel_Get_Cards", "Get Cards");
      this.labelTitle.Text = SK.Text("ManageCandsPanel_Cash_In_Card_Points", "Cash in Cards: Current Card Points");
      this.addPointsData();
      if (GameEngine.Instance.World.FakeCardPoints > 0)
      {
        CustomSelfDrawPanel.CSDLabel labelTitlePoints = this.labelTitlePoints;
        labelTitlePoints.Text = labelTitlePoints.Text + " (+" + GameEngine.Instance.World.FakeCardPoints.ToString() + ")";
      }
      foreach (CustomSelfDrawPanel.CSDControl csdControl in this.ShoppingCart)
        csdControl.Visible = false;
      this.fastCashInCheckBox.Visible = true;
      this.buttonCash.Colorise = ARGBColors.Gray;
      this.buttonCash.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null, (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null);
      this.buttonCash.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
      this.buttonCatalog.Colorise = ARGBColors.White;
      this.buttonCatalog.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonCatalog.Image = (Image) GFXLibrary.cardpanel_button_blue_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.buttonCatalog.Image = (Image) GFXLibrary.cardpanel_button_blue_normal));
      this.buttonCatalog.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.SwitchToBuy), "ManageCardsPanel_switch_to_buy_cards");
      this.InitFilters();
      this.PanelMode = ManageCardsPanel.PANEL_MODE_CASH;
      this.buyAndPlayCheckBox.Visible = false;
      this.sortByQuantity.Text.Text = SK.Text("Card_Sorting_Quantity", "Sort By Quantity");
    }

    private void InitCatalog()
    {
      if (GameEngine.Instance.World.getTutorialStage() == 102)
      {
        if (this.CardCatalog != null)
        {
          foreach (UICard control in this.CardCatalog)
          {
            control.clearControls();
            if (control.Parent != null)
              control.Parent.removeControl((CustomSelfDrawPanel.CSDControl) control);
          }
        }
        this.CardCatalog.Clear();
        this.CardCatalog = new List<UICard>();
        UICard uiCard = this.makeUICard(CardTypes.getCardDefinition(3113), 0, GameEngine.Instance.World.getRank() + 1);
        GFXLibrary.Instance.closeBigCardsLoader();
        this.CardCatalog.Add(uiCard);
        uiCard.countLabel.Text = uiCard.Definition.cardPoints.ToString();
        if (uiCard.Definition.cardPoints >= 100)
          uiCard.countLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
        uiCard.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickCardCart), "ManageCardsPanel_purchase_card");
      }
      else
      {
        string nameSearchText = ((PlayCardsWindow) this.ParentForm).getNameSearchText();
        if (this.CardCatalog != null)
        {
          foreach (UICard control in this.CardCatalog)
          {
            control.clearControls();
            if (control.Parent != null)
              control.Parent.removeControl((CustomSelfDrawPanel.CSDControl) control);
          }
        }
        this.CardCatalog.Clear();
        this.CardCatalog = new List<UICard>();
        foreach (CardTypes.CardDefinition card in CardTypes.cardList)
        {
          if (card.cardRank > 0 && card.cardRarity > 0 && card.available == 1 && card.cardPoints > 0 && (this.CatalogFilterDefinition.cardCategory == 0 || this.CatalogFilterDefinition.cardCategory == card.cardCategory) && (this.CatalogFilterDefinition.cardColour == 0 || this.CatalogFilterDefinition.cardColour == card.cardColour) && (this.CatalogFilterDefinition.newCardCategoryFilter == 0 || CardTypes.isCardInNewCategory(card.id, this.CatalogFilterDefinition.newCardCategoryFilter)) && (nameSearchText.Length == 0 || CardTypes.containsName(card.id, nameSearchText)))
          {
            UICard uiCard = this.makeUICard(card, 0, GameEngine.Instance.World.getRank() + 1);
            this.CardCatalog.Add(uiCard);
            uiCard.countLabel.Text = uiCard.Definition.cardPoints.ToString();
            if (uiCard.Definition.cardPoints >= 100)
              uiCard.countLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            uiCard.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickCardCart), "ManageCardsPanel_purchase_card");
          }
        }
        GFXLibrary.Instance.closeBigCardsLoader();
      }
      this.CardCatalog.Sort((Comparison<UICard>) ((card1, card2) => card1.Definition.cardPoints != card2.Definition.cardPoints ? card1.Definition.cardPoints.CompareTo(card2.Definition.cardPoints) : CardTypes.getDescriptionFromCard(card1.Definition.id).CompareTo(CardTypes.getDescriptionFromCard(card2.Definition.id))));
    }

    private void ClickBuyMultiple()
    {
      if (GameEngine.Instance.World.WorldEnded || this.cashingIn || this.buyingCard)
        return;
      XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
      XmlRpcCardsRequest req = new XmlRpcCardsRequest();
      req.UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", "");
      req.SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
      req.WorldID = RemoteServices.Instance.ProfileWorldID.ToString();
      req.CardString = "";
      if (GameEngine.Instance.World.getTutorialStage() == 102)
        req.CardPoints = new int?(1);
      this.newcardcost = 0;
      for (int index = 0; index < this.ShoppingCart.Count; ++index)
      {
        req.CardString += this.ShoppingCart[index].Definition.name;
        this.newcardcost += this.ShoppingCart[index].Definition.cardPoints;
        if (index < this.ShoppingCart.Count - 1)
          req.CardString += ",";
      }
      this.newcardnames = req.CardString;
      forEndpoint.buyMultipleCards((ICardsRequest) req, new CardsEndResponseDelegate(this.MultipleCallback), (Control) this);
      this.buyingCard = true;
      this.cardsButtons.Available = false;
    }

    private void MultipleCallback(ICardsProvider provider, ICardsResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        string str = response.Strings.TrimEnd(",".ToCharArray());
        string[] strArray1 = this.newcardnames.Split(",".ToCharArray());
        string[] strArray2 = str.Split(",".ToCharArray());
        for (int index = 0; index < strArray1.Length; ++index)
        {
          int int32 = Convert.ToInt32(strArray2[strArray1.Length - 1 - index].Trim());
          GameEngine.Instance.cardsManager.ProfileCards.Add(int32, CardTypes.getCardDefinitionFromString(strArray1[index].Trim()));
          if (GameEngine.Instance.cardsManager.ProfileCards[int32].id == 3113)
            GameEngine.Instance.World.handleQuestObjectiveHappening(10007);
        }
        if (GameEngine.Instance.World.getTutorialStage() == 102)
          GameEngine.Instance.World.FakeCardPoints = 0;
        else
          GameEngine.Instance.World.ProfileCardpoints -= this.newcardcost;
        this.labelTitle.Text = SK.Text("ManageCandsPanel_Get_New_Cards_Points", "Get New Cards: Current Card Points");
        this.addPointsData();
        if (GameEngine.Instance.World.FakeCardPoints > 0)
        {
          CustomSelfDrawPanel.CSDLabel labelTitlePoints = this.labelTitlePoints;
          labelTitlePoints.Text = labelTitlePoints.Text + " (+" + GameEngine.Instance.World.FakeCardPoints.ToString() + ")";
        }
        if (GameEngine.Instance.World.getTutorialStage() == 102)
          this.closeClick();
        if (strArray2.Length == 1 && this.buyAndPlayCheckBox.Checked)
          this.autoPlayCard(Convert.ToInt32(strArray2[0].Trim()), CardTypes.getCardDefinitionFromString(strArray1[0].Trim()), true, false);
      }
      else
      {
        int num = (int) MyMessageBox.Show(response.Message, SK.Text("GENERIC_Error", "Error"));
      }
      this.cardsButtons.Available = true;
      this.buyingCard = false;
      GameEngine.Instance.cardsManager.ShoppingCartCards.Clear();
      this.RefreshCart();
      this.LabelClickToRemove.Text = "";
    }

    private void InitSpinners()
    {
      this.DynamicPanel.removeControl((CustomSelfDrawPanel.CSDControl) this.SlotHolder);
      this.SlotHolder = new CustomSelfDrawPanel.CSDImage();
      this.SlotHolder.Image = (Image) GFXLibrary.CardSlotFrame;
      this.SlotHolder.Position = new Point(this.DynamicPanel.Width / 2 - this.SlotHolder.Width / 2, this.DynamicPanel.Height / 2 - this.SlotHolder.Height / 2);
      this.SlotHolder.Size = GFXLibrary.CardSlotFrame.Size;
      this.DynamicPanel.addControl((CustomSelfDrawPanel.CSDControl) this.SlotHolder);
      this.SlotHolder.Visible = false;
      for (int index = 0; index < this.SlotAnims.Length; ++index)
      {
        this.SlotHolder.removeControl((CustomSelfDrawPanel.CSDControl) this.SlotAnims[index]);
        this.SlotAnims[index] = new CustomSelfDrawPanel.CSDImageAnim();
        this.SlotAnims[index].Position = new Point(11 + index * 61, 11);
        this.SlotAnims[index].SetFrames(GFXLibrary.CardSlotAnimFrames);
        this.SlotAnims[index].Size = GFXLibrary.CardSlotAnimFrames[0].Size;
        this.SlotAnims[index].FrameData = GFXLibrary.CardSlotAnimData;
        this.SlotAnims[index].Playing = false;
        this.SlotHolder.addControl((CustomSelfDrawPanel.CSDControl) this.SlotAnims[index]);
        this.SlotAnims[index].Visible = false;
      }
      for (int index = 0; index < this.SymbolScrollers.Length; ++index)
      {
        this.DynamicPanel.removeControl((CustomSelfDrawPanel.CSDControl) this.SymbolScrollers[index]);
        this.SymbolScrollers[index] = new CustomSelfDrawPanel.CSDVertImageScroller();
        this.SymbolScrollers[index].init(new Point(index * (GFXLibrary.cardpanel_symbol_crown.Width - 10), 0), new BaseImage[7]
        {
          GFXLibrary.cardpanel_symbol_apple,
          GFXLibrary.cardpanel_symbol_crown,
          GFXLibrary.cardpanel_symbol_hawk,
          GFXLibrary.cardpanel_symbol_jester,
          GFXLibrary.cardpanel_symbol_shield,
          GFXLibrary.cardpanel_symbol_tower,
          GFXLibrary.cardpanel_symbol_wolf
        }, new int[7]
        {
          16777216,
          1073741824,
          67108864,
          536870912,
          134217728,
          268435456,
          33554432
        });
        this.DynamicPanel.addControl((CustomSelfDrawPanel.CSDControl) this.SymbolScrollers[index]);
        this.SymbolScrollers[index].Visible = false;
      }
    }

    private void CashClick()
    {
      if (GameEngine.Instance.World.WorldEnded || this.cashingIn || this.buyingCard)
        return;
      XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
      XmlRpcCardsRequest req = new XmlRpcCardsRequest();
      req.UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", "");
      req.SessionGUID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", "");
      string str = "";
      this.NumCardsCachingIn = 0;
      foreach (UICard setCard in this.SetCards)
      {
        if (setCard.Visible)
        {
          if (str.Length > 0)
            str += ",";
          str += setCard.UserID.ToString();
          ++this.NumCardsCachingIn;
        }
      }
      req.CardString = str;
      forEndpoint.cashInCards((ICardsRequest) req, new CardsEndResponseDelegate(this.CashClickCallback), (Control) this);
      for (int index = 0; index < this.SlotAnims.Length; ++index)
      {
        this.SlotAnims[index].Visible = true;
        this.SlotAnims[index].Playing = !this.fastCashIn;
      }
      this.SlotHolder.Visible = true;
      this.cashingIn = true;
      this.lastCashResponse = (XmlRpcCardsResponse) null;
      this.cardsButtons.Available = false;
      this.spinspeed = 64;
      this.spinstart = DateTime.Now;
      if (!this.fastCashIn && !this.playingSpinSound)
      {
        this.playingSpinSound = true;
        GameEngine.Instance.playInterfaceSound("CardSpinners_spin");
        for (int index = 0; index < 5; ++index)
          this.spinSoundStopPlayed[index] = false;
        this.spinSoundSoundID = 1;
      }
      this.mainBackgroundImage.invalidate();
      this.DynamicLabel.Visible = false;
    }

    private void CashClickCallback(ICardsProvider provider, ICardsResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        StatTrackingClient.Instance().ActivateTrigger(19, (object) null);
        foreach (int profileCards in GameEngine.Instance.cardsManager.ProfileCardsSet)
          GameEngine.Instance.cardsManager.ProfileCards.Remove(profileCards);
        GameEngine.Instance.cardsManager.ProfileCardsSet.Clear();
        this.fastCashInCheckBox.Enabled = true;
        this.lastCashResponse = (XmlRpcCardsResponse) response;
        GameEngine.Instance.World.ProfileCardpoints += response.Newpoints.Value;
      }
      else
      {
        int num = (int) MyMessageBox.Show(response.Message, SK.Text("GENERIC_Error", "Error"));
        this.GetCardsAvailable(true);
        this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
        this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
        this.cashingIn = false;
        this.cardsButtons.Available = true;
        this.InitEmptyCards();
        this.RefreshSet();
        this.InitSpinners();
      }
    }

    private void ClickCardUnset()
    {
      if (this.cashingIn || this.buyingCard)
        return;
      if (GameEngine.shiftPressedAlways)
      {
        GameEngine.Instance.cardsManager.ProfileCardsSet.Clear();
      }
      else
      {
        UICard clickedControl = (UICard) this.ClickedControl;
        GameEngine.Instance.cardsManager.ProfileCardsSet.Remove(clickedControl.UserID);
      }
      this.GetCardsAvailable(true);
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
      this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
      this.RefreshSet();
      this.LabelClickToRemove.Text = "";
      if (GameEngine.Instance.cardsManager.ProfileCardsSet.Count > 5)
        this.fastCashInCheckBox.Enabled = false;
      else
        this.fastCashInCheckBox.Enabled = true;
    }

    private void ClickCardSet() => this.ClickCardSet(true);

    private void ClickCardSet(bool initialCall)
    {
      if (this.cashingIn || this.buyingCard)
        return;
      if (GameEngine.Instance.cardsManager.ProfileCardsSet.Count < 5 || GameEngine.Instance.cardsManager.ProfileCardsSet.Count < 60 && this.fastCashIn)
      {
        UICard clickedControl = (UICard) this.ClickedControl;
        if (clickedControl.cardCount > 1 && clickedControl.UserIDList.Count > 1)
        {
          int userId = clickedControl.UserIDList[0];
          if (GameEngine.Instance.cardsManager.ProfileCards[userId].rewardcard)
          {
            GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set_error");
            int num = (int) MyMessageBox.Show(SK.Text("ManageCandsPanel_Cannot_Cash_Rewards", "You cannot cash in reward cards."), SK.Text("GENERIC_Error", "Error"));
          }
          else
          {
            clickedControl.UserIDList.Remove(userId);
            --clickedControl.cardCount;
            if (clickedControl.cardCount > 1)
            {
              clickedControl.countLabel.Text = clickedControl.cardCount.ToString();
              clickedControl.countLabel.Font = clickedControl.cardCount < 100 ? FontManager.GetFont("Arial", 18f, FontStyle.Bold) : FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            }
            else
              clickedControl.countLabel.Text = "";
            this.AvailablePanelContent.invalidate();
            clickedControl.UserID = clickedControl.UserIDList[0];
            if (!GameEngine.Instance.cardsManager.ProfileCardsSet.Contains(userId))
            {
              if (initialCall)
                GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set");
              if (GameEngine.Instance.cardsManager.ProfileCardsSet.Count >= 5)
              {
                int index = GameEngine.Instance.cardsManager.ProfileCardsSet.Count % 5;
                int profileCards = GameEngine.Instance.cardsManager.ProfileCardsSet[index];
                GameEngine.Instance.cardsManager.ProfileCardsSet.Remove(profileCards);
                GameEngine.Instance.cardsManager.ProfileCardsSet.Insert(index, userId);
                GameEngine.Instance.cardsManager.ProfileCardsSet.Add(profileCards);
              }
              else
                GameEngine.Instance.cardsManager.ProfileCardsSet.Add(userId);
              if (GameEngine.shiftPressedAlways)
                this.ClickCardSet(false);
            }
            else
            {
              int num = (int) MyMessageBox.Show(SK.Text("ManageCandsPanel_Already_In_Set", "It appears that card is already in the set."), SK.Text("GENERIC_Error", "Error"));
            }
          }
        }
        else if (clickedControl.UserIDList.Count > 0 && GameEngine.Instance.cardsManager.ProfileCards[clickedControl.UserIDList[0]].rewardcard)
        {
          GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set_error");
          int num = (int) MyMessageBox.Show(SK.Text("ManageCandsPanel_Cannot_Cash_Rewards", "You cannot cash in reward cards."), SK.Text("GENERIC_Error", "Error"));
        }
        else
        {
          this.UICardList.Remove(clickedControl);
          this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
          this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
          if (clickedControl.UserIDList.Count > 0)
          {
            clickedControl.UserID = clickedControl.UserIDList[0];
            int userId = clickedControl.UserIDList[0];
            if (!GameEngine.Instance.cardsManager.ProfileCardsSet.Contains(userId))
            {
              GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set");
              if (GameEngine.Instance.cardsManager.ProfileCardsSet.Count >= 5)
              {
                int index = GameEngine.Instance.cardsManager.ProfileCardsSet.Count % 5;
                int profileCards = GameEngine.Instance.cardsManager.ProfileCardsSet[index];
                GameEngine.Instance.cardsManager.ProfileCardsSet.Remove(profileCards);
                GameEngine.Instance.cardsManager.ProfileCardsSet.Insert(index, userId);
                GameEngine.Instance.cardsManager.ProfileCardsSet.Add(profileCards);
              }
              else
                GameEngine.Instance.cardsManager.ProfileCardsSet.Add(userId);
            }
            else
            {
              GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set_error");
              int num = (int) MyMessageBox.Show(SK.Text("ManageCandsPanel_Already_In_Set", "It appears that card is already in the set."), SK.Text("GENERIC_Error", "Error"));
            }
          }
          else
          {
            GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set_error");
            int num = (int) MyMessageBox.Show(SK.Text("ManageCandsPanel_Not_Own_Card", "It appears you do not own that card."), SK.Text("GENERIC_Error", "Error"));
          }
        }
        if (GameEngine.Instance.cardsManager.ProfileCardsSet.Count > 5)
          this.fastCashInCheckBox.Enabled = false;
        else
          this.fastCashInCheckBox.Enabled = true;
        this.RefreshSet();
      }
      else
        GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set_full_error");
    }

    private void RefreshCart()
    {
      foreach (CustomSelfDrawPanel.CSDControl control in this.ShoppingCart)
        this.mainBackgroundImage.removeControl(control);
      this.ShoppingCart.Clear();
      int userid = 0;
      int num = 0;
      foreach (int shoppingCartCard in GameEngine.Instance.cardsManager.ShoppingCartCards)
      {
        UICard newcard = this.makeUICard(CardTypes.getCardDefinition(shoppingCartCard), userid, GameEngine.Instance.World.getRank() + 1);
        newcard.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
        {
          this.LabelClickToRemove.Text = this.TextRemove + CardTypes.getDescriptionFromCard(newcard.Definition.id);
          newcard.MouseOver();
        }), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
        {
          this.LabelClickToRemove.Text = "";
          newcard.MouseOut();
        }));
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) newcard);
        newcard.ScaleAll(0.5);
        newcard.Position = new Point(Convert.ToInt32(Math.Floor((double) this.EmptyCards[0].X * 0.5)) + userid * 16, Convert.ToInt32(Math.Floor((double) this.EmptyCards[0].Y * 0.5)));
        newcard.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickCardUncart), "ManageCardsPanel_un_purchase_card");
        newcard.Visible = true;
        this.ShoppingCart.Add(newcard);
        ++userid;
        num += newcard.Definition.cardPoints;
      }
      GFXLibrary.Instance.closeBigCardsLoader();
      if (this.ShoppingCart.Count == 0)
      {
        this.DynamicLabel.Color = ARGBColors.Black;
        this.DynamicLabel.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
        this.DynamicLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null, (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null);
        this.DynamicLabel.Text = this.TextCartEmpty;
        this.DynamicButton.Visible = false;
        this.DynamicButtonLabel.Visible = false;
        this.DynamicLabel.Visible = true;
      }
      else
      {
        this.DynamicLabel.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
        this.DynamicLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null, (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null);
        this.DynamicLabel.Text = "";
        this.DynamicLabel.Text = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + SK.Text("ManageCandsPanel_Cards_Points_Value", "Card Point Value") + " : " + num.ToString();
        this.DynamicButton.Visible = true;
        this.DynamicButtonLabel.Visible = true;
        this.DynamicButtonLabel.Text = SK.Text("ManageCandsPanel_Get_Cards", "Get Cards");
        this.DynamicButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickBuyMultiple), "ManageCardsPanel_get_cards");
      }
      this.buyAndPlayCheckBox.Visible = this.ShoppingCart.Count == 1 && GameEngine.Instance.World.getTutorialStage() != 102;
      this.buyAndPlayCheckBox.Checked = false;
      this.mainBackgroundImage.invalidate();
    }

    private void ClickCardUncart()
    {
      this.LabelClickToRemove.Text = "";
      UICard clickedControl = (UICard) this.ClickedControl;
      GameEngine.Instance.cardsManager.ShoppingCartCards.RemoveAt(clickedControl.UserID);
      this.RefreshCart();
    }

    private void ClickCardCart()
    {
      this.addCardToCard(((UICard) this.ClickedControl).Definition.id, true);
    }

    public void addCardToCard(int cardType, bool showMessages)
    {
      CardTypes.CardDefinition cardDefinition = CardTypes.getCardDefinition(cardType);
      int num1 = 0;
      foreach (UICard uiCard in this.ShoppingCart)
        num1 += uiCard.Definition.cardPoints;
      if (num1 + cardDefinition.cardPoints > (GameEngine.Instance.World.getTutorialStage() != 102 ? GameEngine.Instance.World.ProfileCardpoints : GameEngine.Instance.World.FakeCardPoints))
      {
        this.failedPurchaseCard = cardType;
        this.failedPurchaseCost = cardDefinition.cardPoints;
        if (showMessages)
        {
          bool flag = MyMessageBox.Show(SK.Text("ManageCandsPanel_Not_Enough_Points", "That would cost more Card Points than you currently have. Would you like to trade existing cards for more points?"), SK.Text("ManageCandsPanel_Not_Enough_Points_Heading", "Not Enough Card Points"), MessageBoxButtons.YesNo) == DialogResult.Yes;
          StatTrackingClient.Instance().ActivateTrigger(18, (object) flag);
          StatTrackingClient.Instance().ActivateTrigger(22, (object) cardType);
          if (flag)
          {
            this.SwitchToCash();
            this.TabSelector.Image = (Image) GFXLibrary.cardpanel_manage_tabs_white_left;
            this.TabSelector.ClickArea = new Rectangle(196, 0, 118, 30);
          }
        }
        if (GameEngine.Instance.World.getTutorialStage() != 102)
          return;
        GameEngine.Instance.World.handleQuestObjectiveHappening(10007);
      }
      else if (GameEngine.Instance.cardsManager.ShoppingCartCards.Count > 24)
      {
        if (!showMessages)
          return;
        int num2 = (int) MyMessageBox.Show(SK.Text("ManageCandsPanel_Cards_Limit", "You may only buy up to 25 cards at a time."), SK.Text("ManageCandsPanel_Cards_Limit_Heading", "Maximum Reached"));
      }
      else
      {
        StatTrackingClient.Instance().ActivateTrigger(20, (object) cardType);
        GameEngine.Instance.cardsManager.ShoppingCartCards.Add(cardType);
        this.RefreshCart();
      }
    }

    private void RefreshSet()
    {
      for (int index = 59; index >= 0; --index)
      {
        if (this.SetCards[index] != null)
          this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.SetCards[index]);
        if (GameEngine.Instance.cardsManager.ProfileCardsSet.Count > index)
        {
          this.SetCards[index] = this.makeUICard(GameEngine.Instance.cardsManager.ProfileCards[GameEngine.Instance.cardsManager.ProfileCardsSet[index]], GameEngine.Instance.cardsManager.ProfileCardsSet[index], GameEngine.Instance.World.getRank() + 1);
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.SetCards[index]);
          this.SetCards[index].ScaleAll(0.5);
          int x = Convert.ToInt32(Math.Floor((double) this.EmptyCards[index % 5].X * 0.5)) + index / 5;
          int y = Convert.ToInt32(Math.Floor((double) this.EmptyCards[index % 5].Y * 0.5)) - index / 5 * 2;
          this.SetCards[index].Position = new Point(x, y);
          this.SetCards[index].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickCardUnset), "ManageCardsPanel_remove_card");
          UICard deelgatecard = this.SetCards[index];
          this.SetCards[index].setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
          {
            this.LabelClickToRemove.Text = this.TextRemoveSet + CardTypes.getDescriptionFromCard(deelgatecard.Definition.id);
            deelgatecard.MouseOver();
          }), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
          {
            this.LabelClickToRemove.Text = "";
            deelgatecard.MouseOut();
          }));
          this.SetCards[index].Visible = true;
          if (index < 5)
            this.EmptyCards[index].Visible = false;
        }
        else
        {
          this.SetCards[index].Visible = false;
          if (index < 5)
            this.EmptyCards[index].Visible = true;
        }
      }
      GFXLibrary.Instance.closeBigCardsLoader();
      this.labelTitle.Text = SK.Text("ManageCandsPanel_Cash_In_Cards_Title", "Cash in Cards: Current Card Points");
      this.addPointsData();
      if (GameEngine.Instance.World.FakeCardPoints > 0)
      {
        CustomSelfDrawPanel.CSDLabel labelTitlePoints = this.labelTitlePoints;
        labelTitlePoints.Text = labelTitlePoints.Text + " (+" + GameEngine.Instance.World.FakeCardPoints.ToString() + ")";
      }
      this.DynamicLabel.Text = this.TextCash;
      this.DynamicLabel.Color = ARGBColors.Black;
      this.DynamicLabel.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
      this.DynamicLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null, (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) null);
      this.DynamicLabel.Visible = true;
      this.DynamicButton.Visible = false;
      if (GameEngine.Instance.cardsManager.ProfileCardsSet.Count == 0)
        this.DynamicLabel.Text = this.fastCashIn ? this.TextEmptyMultiSet : this.TextEmptySet;
      else if (GameEngine.Instance.cardsManager.ProfileCardsSet.Count >= 5)
      {
        this.DynamicLabel.Text = this.TextCash;
        this.DynamicLabel.Visible = false;
        this.DynamicButton.Visible = true;
        this.DynamicButtonLabel.Visible = true;
        this.DynamicButtonLabel.Text = SK.Text("ManageCandsPanel_Cash_In", "Cash In") + " (" + GameEngine.Instance.cardsManager.ProfileCardsSet.Count.ToString() + ")";
        this.DynamicButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.CashClick), "ManageCardsPanel_cash_in_cards");
      }
      else
        this.DynamicLabel.Text = this.TextIncompleteSetStart + (5 - GameEngine.Instance.cardsManager.ProfileCardsSet.Count).ToString();
      this.DynamicPanel.invalidate();
      this.mainBackgroundImage.invalidate();
    }

    private void InitEmptyCards()
    {
      for (int index = 0; index < 5; ++index)
      {
        if (this.EmptyCards[index] != null)
          this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.EmptyCards[index]);
        this.EmptyCards[index] = new CustomSelfDrawPanel.CSDImage();
        this.EmptyCards[index].Image = (Image) GFXLibrary.CardBackBig;
        this.EmptyCards[index].Size = this.EmptyCards[index].Image.Size;
        this.EmptyCards[index].Scale = 0.5;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.EmptyCards[index]);
        this.EmptyCards[index].Position = new Point(index * this.EmptyCards[index].Width + this.AvailablePanel.X + 8, (this.buttonCash.Y + this.buttonCash.Height) * 2 + 8);
        this.EmptyCards[index].Visible = true;
      }
      for (int index = 0; index < 60; ++index)
      {
        this.SetCards[index] = new UICard();
        this.SetCards[index].Visible = false;
      }
    }

    private void GetCardsAvailable(bool redosearch)
    {
      if (redosearch)
        GameEngine.Instance.cardsManager.searchProfileCardsRedoLast();
      Dictionary<int, int> dictionary1 = new Dictionary<int, int>();
      foreach (int key1 in GameEngine.Instance.cardsManager.ProfileCardsSearch)
      {
        int id = GameEngine.Instance.cardsManager.ProfileCards[key1].id;
        if (!GameEngine.Instance.cardsManager.ProfileCardsSet.Contains(key1))
        {
          if (dictionary1.ContainsKey(id))
          {
            Dictionary<int, int> dictionary2;
            int key2;
            (dictionary2 = dictionary1)[key2 = id] = dictionary2[key2] + 1;
          }
          else
            dictionary1.Add(id, 1);
        }
      }
      UICard uiCard1 = (UICard) null;
      foreach (UICard uiCard2 in this.UICardList)
      {
        uiCard2.clearControls();
        if (uiCard2.Parent != null)
          uiCard2.Parent.removeControl((CustomSelfDrawPanel.CSDControl) uiCard2);
      }
      this.UICardList.Clear();
      int num = GameEngine.Instance.World.getRank() + 1;
      foreach (int key in GameEngine.Instance.cardsManager.ProfileCardsSearch)
      {
        int id = GameEngine.Instance.cardsManager.ProfileCards[key].id;
        if (dictionary1.ContainsKey(id) && !GameEngine.Instance.cardsManager.ProfileCardsSet.Contains(key))
        {
          UICard uiCard3 = new UICard();
          uiCard3.cardCount = dictionary1[id];
          uiCard3.UserID = key;
          uiCard3.UserIDList.Add(key);
          uiCard3.Definition = GameEngine.Instance.cardsManager.ProfileCards[key];
          switch (uiCard3.Definition.cardColour)
          {
            case 1:
              uiCard3.bigFrame = GFXLibrary.BlueCardOverlayBig;
              uiCard3.bigFrameOver = GFXLibrary.BlueCardOverlayBigOver;
              break;
            case 2:
              uiCard3.bigFrame = GFXLibrary.GreenCardOverlayBig;
              uiCard3.bigFrameOver = GFXLibrary.GreenCardOverlayBigOver;
              break;
            case 3:
              uiCard3.bigFrame = GFXLibrary.PurpleCardOverlayBig;
              uiCard3.bigFrameOver = GFXLibrary.PurpleCardOverlayBigOver;
              break;
            case 4:
              uiCard3.bigFrame = GFXLibrary.RedCardOverlayBig;
              uiCard3.bigFrameOver = GFXLibrary.RedCardOverlayBigOver;
              break;
            case 5:
              uiCard3.bigFrame = GFXLibrary.YellowCardOverlayBig;
              uiCard3.bigFrameOver = GFXLibrary.YellowCardOverlayBigOver;
              break;
          }
          try
          {
            uiCard3.bigImage = GFXLibrary.Instance.getCardImageBig(uiCard3.Definition.id);
          }
          catch (Exception ex)
          {
            continue;
          }
          uiCard3.Size = uiCard3.bigFrame.Size;
          uiCard3.CustomTooltipID = 10101;
          uiCard3.CustomTooltipData = uiCard3.Definition.id;
          uiCard3.bigGradeImage = new CustomSelfDrawPanel.CSDImage();
          int grade = CardTypes.getGrade(uiCard3.Definition.cardGrade);
          switch (grade)
          {
            case 65536:
              uiCard3.bigGradeImage.Image = (Image) GFXLibrary.CardGradeBronze;
              uiCard3.bigGradeImage.Position = new Point(uiCard3.Width - uiCard3.bigGradeImage.Width, 0);
              break;
            case 131072:
              uiCard3.bigGradeImage.Image = (Image) GFXLibrary.CardGradeSilver;
              uiCard3.bigGradeImage.Position = new Point(uiCard3.Width - uiCard3.bigGradeImage.Width, 0);
              break;
            case 262144:
              uiCard3.bigGradeImage.Image = (Image) GFXLibrary.card_gold_anim[0];
              uiCard3.bigGradeImage.Position = new Point(uiCard3.Width - uiCard3.bigGradeImage.Width - 3, 0);
              break;
            case 524288:
              uiCard3.bigGradeImage.Image = (Image) GFXLibrary.card_diamond_anim[0];
              uiCard3.bigGradeImage.Position = new Point(uiCard3.Width - uiCard3.bigGradeImage.Width - 3, -2);
              break;
            case 1048576:
              uiCard3.bigGradeImage.Image = (Image) GFXLibrary.card_diamond2_anim[0];
              uiCard3.bigGradeImage.Position = new Point(uiCard3.Width - uiCard3.bigGradeImage.Width - 3, -7);
              break;
            case 2097152:
              uiCard3.bigGradeImage.Image = (Image) GFXLibrary.card_diamond3_anim[0];
              uiCard3.bigGradeImage.Position = new Point(uiCard3.Width - uiCard3.bigGradeImage.Width - 3, -10);
              break;
            case 4194304:
              uiCard3.bigGradeImage.Image = (Image) GFXLibrary.card_sapphire_anim[0];
              uiCard3.bigGradeImage.Position = new Point(uiCard3.Width - uiCard3.bigGradeImage.Width - 3, -12);
              break;
            default:
              uiCard3.bigGradeImage.Image = (Image) GFXLibrary.CardGradeBronze;
              uiCard3.bigGradeImage.Position = new Point(uiCard3.Width - uiCard3.bigGradeImage.Width, 0);
              break;
          }
          uiCard3.bigBaseImage = new CustomSelfDrawPanel.CSDImage();
          uiCard3.bigBaseImage.Position = new Point(10, 11);
          uiCard3.bigBaseImage.Size = uiCard3.bigImage.Size;
          uiCard3.bigBaseImage.Image = (Image) uiCard3.bigImage;
          uiCard3.addControl((CustomSelfDrawPanel.CSDControl) uiCard3.bigBaseImage);
          uiCard3.bigFrameImage = new CustomSelfDrawPanel.CSDImage();
          uiCard3.bigFrameImage.Position = new Point(0, 0);
          uiCard3.bigFrameImage.Size = uiCard3.bigFrame.Size;
          uiCard3.bigFrameImage.Image = (Image) uiCard3.bigFrame;
          uiCard3.addControl((CustomSelfDrawPanel.CSDControl) uiCard3.bigFrameImage);
          switch (grade)
          {
            case 262144:
              uiCard3.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
              uiCard3.bigFrameExtraImage.Position = new Point(0, 0);
              uiCard3.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_gold;
              uiCard3.addControl((CustomSelfDrawPanel.CSDControl) uiCard3.bigFrameExtraImage);
              break;
            case 524288:
            case 1048576:
            case 2097152:
              uiCard3.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
              uiCard3.bigFrameExtraImage.Position = new Point(0, 0);
              uiCard3.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_diamond;
              uiCard3.addControl((CustomSelfDrawPanel.CSDControl) uiCard3.bigFrameExtraImage);
              break;
            case 4194304:
              uiCard3.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
              uiCard3.bigFrameExtraImage.Position = new Point(0, 0);
              uiCard3.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_sapphire;
              uiCard3.addControl((CustomSelfDrawPanel.CSDControl) uiCard3.bigFrameExtraImage);
              break;
          }
          uiCard3.bigGradeImage.Size = uiCard3.bigGradeImage.Image.Size;
          uiCard3.addControl((CustomSelfDrawPanel.CSDControl) uiCard3.bigGradeImage);
          uiCard3.bigTitle = new CustomSelfDrawPanel.CSDLabel();
          uiCard3.bigTitle.Text = CardTypes.getDescriptionFromCard(uiCard3.Definition.id);
          uiCard3.bigTitle.Size = new Size(110, 48);
          uiCard3.bigTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          uiCard3.bigTitle.Font = uiCard3.Definition.id != 1801 && uiCard3.Definition.id != 1542 && uiCard3.Definition.id != 3137 && uiCard3.Definition.id != 1290 && uiCard3.Definition.id != 1541 && uiCard3.Definition.id != 1543 || !(Program.mySettings.LanguageIdent == "de") ? FontManager.GetFont("Arial", 9f, FontStyle.Bold) : FontManager.GetFont("Arial", 8f, FontStyle.Bold);
          uiCard3.bigTitle.Color = ARGBColors.White;
          uiCard3.bigTitle.DropShadowColor = ARGBColors.Black;
          uiCard3.bigTitle.Position = new Point(38, 12);
          uiCard3.addControl((CustomSelfDrawPanel.CSDControl) uiCard3.bigTitle);
          uiCard3.bigEffect = new CustomSelfDrawPanel.CSDLabel();
          uiCard3.bigEffect.Text = uiCard3.Definition.EffectText;
          uiCard3.bigEffect.Size = new Size(150, 64);
          uiCard3.bigEffect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          uiCard3.bigEffect.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
          if (Program.mySettings.LanguageIdent == "de" && CardTypes.isGermanSmallDesc(uiCard3.Definition.id))
            uiCard3.bigEffect.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
          uiCard3.bigEffect.Color = ARGBColors.White;
          uiCard3.bigEffect.DropShadowColor = ARGBColors.Black;
          uiCard3.bigEffect.Position = new Point(14, 174);
          uiCard3.addControl((CustomSelfDrawPanel.CSDControl) uiCard3.bigEffect);
          CustomSelfDrawPanel.CSDLabel control1 = new CustomSelfDrawPanel.CSDLabel();
          control1.Position = new Point(2, 2);
          control1.Size = new Size(uiCard3.Width, uiCard3.Height);
          control1.Text = "";
          control1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          control1.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
          control1.Color = ARGBColors.Yellow;
          control1.DropShadowColor = ARGBColors.Black;
          uiCard3.addControl((CustomSelfDrawPanel.CSDControl) control1);
          uiCard3.countLabel = control1;
          if (num < uiCard3.Definition.cardRank)
          {
            Color red = ARGBColors.Red;
          }
          else
          {
            Color white = ARGBColors.White;
          }
          CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
          control2.Position = new Point(150, 220);
          control2.Size = new Size(20, 13);
          control2.Text = uiCard3.Definition.cardRank.ToString();
          control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          control2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          control2.Color = ARGBColors.White;
          control2.DropShadowColor = ARGBColors.Black;
          uiCard3.addControl((CustomSelfDrawPanel.CSDControl) control2);
          uiCard3.rankLabel = control2;
          uiCard3.ScaleAll(0.95);
          this.UICardList.Add(uiCard3);
          dictionary1.Remove(id);
          uiCard1 = uiCard3;
          if (num < uiCard3.Definition.cardRank)
            uiCard3.Hilight(ARGBColors.Gray);
          else
            uiCard3.Hilight(ARGBColors.White);
          uiCard3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickCardSet));
        }
        else if (uiCard1 != null && !GameEngine.Instance.cardsManager.ProfileCardsSet.Contains(key))
          uiCard1.UserIDList.Add(key);
      }
      GFXLibrary.Instance.closeBigCardsLoader();
    }

    public void setFilter(int filterGroup)
    {
      this.CatalogFilterDefinition = new CardTypes.CardDefinition();
      this.CatalogFilterDefinition.newCardCategoryFilter = filterGroup;
      GameEngine.Instance.cardsManager.lastUserCardSearchCriteria = new CardTypes.CardDefinition()
      {
        newCardCategoryFilter = filterGroup
      };
    }

    private int RefreshCards(CustomSelfDrawPanel.CSDImage content, List<UICard> list, int width)
    {
      int[] numArray = (int[]) null;
      if (GameEngine.Instance.cardsManager.lastUserCardSearchCriteria != null)
      {
        switch (GameEngine.Instance.cardsManager.lastUserCardSearchCriteria.newCardCategoryFilter)
        {
          case 4097:
            numArray = CardTypes.newCategories_ApplesOrder;
            break;
          case 4098:
            numArray = CardTypes.newCategories_CheeseOrder;
            break;
          case 4099:
            numArray = CardTypes.newCategories_MeatOrder;
            break;
          case 4100:
            numArray = CardTypes.newCategories_BreadOrder;
            break;
          case 4101:
            numArray = CardTypes.newCategories_VegOrder;
            break;
          case 4102:
            numArray = CardTypes.newCategories_FishOrder;
            break;
          case 4103:
            numArray = CardTypes.newCategories_AleOrder;
            break;
          case 8193:
            numArray = CardTypes.newCategories_WoodOrder;
            break;
          case 8194:
            numArray = CardTypes.newCategories_StoneOrder;
            break;
          case 8195:
            numArray = CardTypes.newCategories_IronOrder;
            break;
          case 8196:
            numArray = CardTypes.newCategories_PitchOrder;
            break;
          case 16385:
            numArray = CardTypes.newCategories_VenisonOrder;
            break;
          case 16386:
            numArray = CardTypes.newCategories_FurnitureOrder;
            break;
          case 16387:
            numArray = CardTypes.newCategories_MetalwareOrder;
            break;
          case 16388:
            numArray = CardTypes.newCategories_ClothesOrder;
            break;
          case 16389:
            numArray = CardTypes.newCategories_WineOrder;
            break;
          case 16390:
            numArray = CardTypes.newCategories_SaltOrder;
            break;
          case 16391:
            numArray = CardTypes.newCategories_SpicesOrder;
            break;
          case 16392:
            numArray = CardTypes.newCategories_SilkOrder;
            break;
          case 32769:
            numArray = CardTypes.newCategories_BowsOrder;
            break;
          case 32770:
            numArray = CardTypes.newCategories_PikesOrder;
            break;
          case 32771:
            numArray = CardTypes.newCategories_ArmourOrder;
            break;
          case 32772:
            numArray = CardTypes.newCategories_SwordsOrder;
            break;
          case 32773:
            numArray = CardTypes.newCategories_CatapultsOrder;
            break;
          case 65537:
            numArray = CardTypes.newCategories_CastleConOrder;
            break;
          case 65538:
            numArray = CardTypes.newCategories_DefencesOrder;
            break;
          case 65539:
            numArray = CardTypes.newCategories_WallsOrder;
            break;
          case 65540:
            numArray = CardTypes.newCategories_KnightsOrder;
            break;
          case 131073:
            numArray = CardTypes.newCategories_ScoutingOrder;
            break;
          case 131074:
            numArray = CardTypes.newCategories_SpeedOrder;
            break;
          case 131075:
            numArray = CardTypes.newCategories_RecruitmentOrder;
            break;
          case 131076:
            numArray = CardTypes.newCategories_TroopsOrder;
            break;
          case 131077:
            numArray = CardTypes.newCategories_DiplomacyOrder;
            break;
          case 262145:
            numArray = CardTypes.newCategories_TradeOrder;
            break;
          case 262146:
            numArray = CardTypes.newCategories_ReligionOrder;
            break;
          case 262147:
            numArray = CardTypes.newCategories_HonourOrder;
            break;
          case 262148:
            numArray = CardTypes.newCategories_GoldOrder;
            break;
          case 262149:
            numArray = CardTypes.newCategories_PopOrder;
            break;
          case 262150:
            numArray = CardTypes.newCategories_ResearchOrder;
            break;
          case 262151:
            numArray = CardTypes.newCategories_CapacityOrder;
            break;
          case 262152:
            numArray = CardTypes.newCategories_ConstructionOrder;
            break;
        }
      }
      if (this.sortByMode == 0)
        list.Sort((IComparer<UICard>) UICard.cardsNameComparer);
      else if (this.sortByMode == 1)
        list.Sort((IComparer<UICard>) UICard.cardsIDComparer);
      else if (this.sortByMode == 2)
        list.Sort((IComparer<UICard>) UICard.cardsNameComparerReverse);
      else if (this.sortByMode == 3)
        list.Sort((IComparer<UICard>) UICard.cardsIDComparerReverse);
      else if (this.sortByMode == 7)
      {
        if (this.PanelMode == ManageCardsPanel.PANEL_MODE_BUY)
          list.Sort((IComparer<UICard>) UICard.cardsPriceComparer);
        else
          list.Sort((IComparer<UICard>) UICard.cardsQuantityComparer);
      }
      else if (this.sortByMode == 8)
      {
        if (this.PanelMode == ManageCardsPanel.PANEL_MODE_BUY)
          list.Sort((IComparer<UICard>) UICard.cardsPriceComparerReverse);
        else
          list.Sort((IComparer<UICard>) UICard.cardsQuantityComparerReverse);
      }
      int num1 = GameEngine.Instance.World.getRank() + 1;
      content.clearDirectControlsOnly();
      foreach (CustomSelfDrawPanel.CSDControl dummyCard in this.dummyCards)
        dummyCard.clearControls();
      this.dummyCards.Clear();
      int num2 = 0;
      if (numArray != null)
      {
        this.sortBack.Visible = false;
        this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, this.AvailablePanelContent.ClipRect.Y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanel.Height - ManageCardsPanel.BorderPadding * 2);
        int num3 = 0;
        int num4 = -1;
        for (int index1 = 0; index1 < numArray.Length; index1 += 3)
        {
          if (numArray[index1 + 2] != num4)
          {
            int num5 = numArray[index1];
            int x = numArray[index1 + 1] * 178;
            int y = (numArray[index1 + 2] - num3) * 237;
            bool flag1 = false;
            UICard control1 = (UICard) null;
            foreach (UICard uiCard in list)
            {
              if (CardTypes.getCardType(uiCard.Definition.id) == num5)
              {
                flag1 = true;
                control1 = uiCard;
              }
            }
            CardTypes.CardDefinition cardDefinition = CardTypes.getCardDefinition(num5);
            if (!flag1 && (cardDefinition.cardRank <= 0 || cardDefinition.cardRarity <= 0 || cardDefinition.available != 1 || cardDefinition.cardPoints <= 0 && this.LayoutPanelMode == ManageCardsPanel.PANEL_MODE_BUY) && x == 0)
            {
              bool flag2 = false;
              int cardType = CardTypes.getCardType(num5);
              if (cardType >= 3031 && cardType <= 3061)
              {
                for (int index2 = 0; index2 < numArray.Length; index2 += 3)
                {
                  if (numArray[index2 + 2] == numArray[index1 + 2] && num5 != numArray[index2] && CardTypes.getCardDefinition(numArray[index2]).available == 1)
                  {
                    --numArray[index2 + 1];
                    flag2 = true;
                  }
                }
              }
              if (!flag2)
              {
                ++num3;
                num4 = numArray[index1 + 2];
                continue;
              }
            }
            if (y + 237 > num2)
              num2 = y + 237;
            if (flag1)
            {
              control1.Position = new Point(x, y);
              content.addControl((CustomSelfDrawPanel.CSDControl) control1);
              if (num1 < control1.Definition.cardRank)
              {
                control1.rankLabel.Color = ARGBColors.Red;
                control1.Hilight(ARGBColors.Gray);
              }
              else
              {
                control1.rankLabel.Color = ARGBColors.White;
                control1.Hilight(ARGBColors.White);
              }
              if (control1.cardCount > 1)
              {
                control1.countLabel.Text = control1.cardCount.ToString();
                control1.countLabel.Font = control1.cardCount < 100 ? FontManager.GetFont("Arial", 18f, FontStyle.Bold) : FontManager.GetFont("Arial", 16f, FontStyle.Bold);
              }
            }
            else if (cardDefinition.cardRank > 0 && cardDefinition.cardRarity > 0 && cardDefinition.available == 1 && (cardDefinition.cardPoints > 0 || this.LayoutPanelMode != ManageCardsPanel.PANEL_MODE_BUY))
            {
              UICard control2 = BuyCardsPanel.makeUICard(cardDefinition, RemoteServices.Instance.UserID, 10000);
              control2.Position = new Point(x, y);
              content.addControl((CustomSelfDrawPanel.CSDControl) control2);
              CustomSelfDrawPanel.CSDFill control3 = new CustomSelfDrawPanel.CSDFill();
              control3.FillColor = Color.FromArgb(170, 0, 0, 0);
              control3.Alpha = 0.2f;
              control3.Position = new Point(2, 1);
              control3.Size = new Size(control2.Size.Width - 2 - 4, control2.Size.Height - 1 - 5);
              control2.addControl((CustomSelfDrawPanel.CSDControl) control3);
              this.dummyCards.Add(control2);
              control2.CustomTooltipID = 10101;
              control2.CustomTooltipData = num5;
              CustomSelfDrawPanel.CSDLabel control4 = new CustomSelfDrawPanel.CSDLabel();
              control4.Text = SK.Text("CARDS_No_Cards", "No Cards");
              control4.Position = new Point(x + 3, y + 5);
              control4.Size = new Size(157, 217);
              control4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
              control4.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
              control4.Color = ARGBColors.White;
              control4.CustomTooltipID = 10101;
              control4.CustomTooltipData = num5;
              content.addControl((CustomSelfDrawPanel.CSDControl) control4);
            }
          }
        }
      }
      else
      {
        this.sortBack.Visible = true;
        this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, this.AvailablePanelContent.ClipRect.Y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanel.Height - ManageCardsPanel.BorderPadding * 2 - 20);
        int x = 0;
        int y = 0;
        int num6 = 0;
        int num7 = 0;
        foreach (UICard control5 in list)
        {
          control5.Position = new Point(x, y);
          content.addControl((CustomSelfDrawPanel.CSDControl) control5);
          num7 = y;
          if (x > width)
          {
            x = 0;
            if (!this.compressedCards)
              y += control5.Height + 8;
            else
              y += 58;
          }
          else
            x += control5.Width + 12;
          if (this.compressedCards && num6 < list.Count - 4)
          {
            control5.ClipRect = new Rectangle(0, 0, control5.Width, 60);
            control5.bigEffect.Visible = false;
            control5.rankLabel.Visible = false;
          }
          else
          {
            control5.ClipRect = Rectangle.Empty;
            control5.bigEffect.Visible = true;
            control5.rankLabel.Visible = true;
          }
          if (this.compressedCards)
          {
            CustomSelfDrawPanel.CSDLine control6 = new CustomSelfDrawPanel.CSDLine();
            control6.Position = new Point(control5.Position.X + 3, control5.Position.Y + 1);
            control6.Size = new Size(control5.Width - 7, 0);
            control6.LineColor = Color.FromArgb(128, ARGBColors.Black);
            content.addControl((CustomSelfDrawPanel.CSDControl) control6);
          }
          ++num6;
          if (num1 < control5.Definition.cardRank)
          {
            control5.rankLabel.Color = ARGBColors.Red;
            control5.Hilight(ARGBColors.Gray);
          }
          else
          {
            control5.rankLabel.Color = ARGBColors.White;
            control5.Hilight(ARGBColors.White);
          }
          if (control5.cardCount > 1)
          {
            control5.countLabel.Text = control5.cardCount.ToString();
            control5.countLabel.Font = control5.cardCount < 100 ? FontManager.GetFont("Arial", 18f, FontStyle.Bold) : FontManager.GetFont("Arial", 16f, FontStyle.Bold);
          }
        }
        if (list.Count > 0)
          num2 = num7 + list[0].Height + 8;
      }
      content.invalidate();
      return num2;
    }

    public void UpdateScrollbar(
      CustomSelfDrawPanel.CSDVertScrollBar bar,
      CustomSelfDrawPanel.CSDImage content)
    {
      bar.Visible = content.Height > content.ClipRect.Height;
      bar.Max = content.Height - content.ClipRect.Height;
      bar.NumVisibleLines = content.ClipRect.Height;
    }

    public void RenderCards(List<UICard> list)
    {
      int height = this.RefreshCards(this.AvailablePanelContent, list, 500);
      this.AvailablePanelContent.Position = new Point(12, 8);
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
      if (this.sortBack.Visible)
        this.AvailablePanelContent.ClipRect = new Rectangle(0, 0, this.AvailablePanel.Width - ManageCardsPanel.BorderPadding, this.AvailablePanel.Height - ManageCardsPanel.BorderPadding * 2 - 20);
      else
        this.AvailablePanelContent.ClipRect = new Rectangle(0, 0, this.AvailablePanel.Width - ManageCardsPanel.BorderPadding, this.AvailablePanel.Height - ManageCardsPanel.BorderPadding * 2 + 16);
      this.AvailablePanel.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanelContent);
      if (height < this.AvailablePanelContent.ClipRect.Height)
        height = this.AvailablePanelContent.ClipRect.Height;
      this.scrollbarAvailable.Position = new Point(this.AvailablePanel.Width - ManageCardsPanel.BorderPadding - ManageCardsPanel.BorderPadding / 2, this.AvailablePanel.Y + ManageCardsPanel.BorderPadding / 2);
      this.scrollbarAvailable.Size = new Size(ManageCardsPanel.BorderPadding, this.AvailablePanel.Height - ManageCardsPanel.BorderPadding);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollbarAvailable);
      this.scrollbarAvailable.Value = 0;
      this.scrollbarAvailable.StepSize = 200;
      this.scrollbarAvailable.Max = this.AvailablePanelContent.Height - this.AvailablePanelContent.ClipRect.Height;
      this.scrollbarAvailable.NumVisibleLines = this.AvailablePanelContent.ClipRect.Height;
      this.scrollbarAvailable.OffsetTL = new Point(1, 5);
      this.scrollbarAvailable.OffsetBR = new Point(0, -10);
      this.scrollbarAvailable.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.AvailableContentScroll));
      this.scrollbarAvailable.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.cardpanel_scroll_thumb_top, (Image) GFXLibrary.cardpanel_scroll_thumb_mid, (Image) GFXLibrary.cardpanel_scroll_thumb_botom);
      if (height > this.AvailablePanelContent.ClipRect.Height)
        return;
      this.scrollbarAvailable.Visible = false;
    }

    private void AvailableContentScroll()
    {
      int y = this.scrollbarAvailable.Value;
      this.AvailablePanelContent.Position = new Point(this.AvailablePanelContent.Position.X, 8 - y);
      this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanelContent.ClipRect.Height);
      this.AvailablePanelContent.invalidate();
      this.AvailablePanel.invalidate();
    }

    public void AddFloatingText(string text)
    {
      CustomSelfDrawPanel.CSDFloatingText csdFloatingText = new CustomSelfDrawPanel.CSDFloatingText();
      int _dy = -5;
      if (this.fastCashIn)
        _dy = -1;
      csdFloatingText.init(new Point(this.EmptyCards[0].X, this.EmptyCards[0].Y), new Size(this.EmptyCards[0].Width * 5, this.EmptyCards[0].Height), ARGBColors.Yellow, ARGBColors.Black, 0, _dy, -10, text, 32, 33.0, 3000.0, DXTimer.GetCurrentMilliseconds(), (CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.floatingLabels.Add(csdFloatingText);
    }

    public void update()
    {
      double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
      foreach (CustomSelfDrawPanel.CSDFloatingText floatingLabel in this.floatingLabels)
        floatingLabel.move(currentMilliseconds);
      if (this.cashingIn)
      {
        if (this.lastCashResponse != null && (DateTime.Now - this.spinstart).TotalSeconds > 1.0 && this.spinspeed > 32)
        {
          this.spinspeed /= 2;
          this.spinstart = DateTime.Now;
        }
        for (int index = 0; index < 5; ++index)
        {
          if (this.spinspeed == 32)
          {
            if (!this.SlotAnims[index].Animate(currentMilliseconds, this.lastCashResponse.SymbolList[index]) && !this.spinSoundStopPlayed[index])
            {
              this.spinSoundStopPlayed[index] = true;
              if ((DateTime.Now - this.spinSoundStopLastTime).TotalMilliseconds > 500.0)
              {
                GameEngine.Instance.playInterfaceSound("CardSpinners_stop_" + this.spinSoundSoundID.ToString());
                this.spinSoundStopLastTime = DateTime.Now;
              }
              ++this.spinSoundSoundID;
            }
          }
          else
            this.SlotAnims[index].Animate(currentMilliseconds);
          if (!this.SlotAnims[index].Playing && this.lastCashResponse != null)
            this.SlotAnims[index].Image = (Image) GFXLibrary.CardSlotStillSymbols[this.lastCashResponse.SymbolList[index]];
        }
        this.DynamicPanel.invalidate();
        if (!this.SlotAnims[0].Playing && !this.SlotAnims[1].Playing && !this.SlotAnims[2].Playing && !this.SlotAnims[3].Playing && !this.SlotAnims[4].Playing && this.lastCashResponse != null)
        {
          GameEngine.Instance.AudioEngine.Stop("CardSpinners_spin");
          this.playingSpinSound = false;
          if (!this.showingbonus)
          {
            this.floatingLabels.Clear();
            int num = this.NumCardsCachingIn * 5;
            if (this.lastCashResponse.Newpoints.Value == num)
            {
              this.AddFloatingText("+" + this.lastCashResponse.Newpoints.Value.ToString() + " " + SK.Text("ManageCandsPanel_Card_Points", "Card Points") + "! " + SK.Text("ManageCandsPanel_No_Bonus", "No Bonus"));
              GameEngine.Instance.playInterfaceSound("CardSpinners_bonus0");
            }
            else
            {
              this.AddFloatingText("+" + this.lastCashResponse.Newpoints.Value.ToString() + " " + SK.Text("ManageCandsPanel_Card_Points", "Card Points") + "! (" + SK.Text("ManageCandsPanel_Bonus", "Bonus") + " " + (this.lastCashResponse.Newpoints.Value - num).ToString() + ")");
              GameEngine.Instance.playInterfaceSound("CardSpinners_bonus" + (this.lastCashResponse.Newpoints.Value - num).ToString());
            }
            this.labelTitle.Text = SK.Text("ManageCandsPanel_Cash_In_Cards_Title", "Cash in Cards: Current Card Points");
            this.addPointsData();
            if (GameEngine.Instance.World.FakeCardPoints > 0)
            {
              CustomSelfDrawPanel.CSDLabel labelTitlePoints = this.labelTitlePoints;
              labelTitlePoints.Text = labelTitlePoints.Text + " (+" + GameEngine.Instance.World.FakeCardPoints.ToString() + ")";
            }
            this.showingbonus = true;
          }
          else
          {
            bool flag = true;
            foreach (CustomSelfDrawPanel.CSDFloatingText floatingLabel in this.floatingLabels)
            {
              if (floatingLabel.live)
                flag = false;
            }
            if (flag)
            {
              this.GetCardsAvailable(true);
              this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
              this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
              this.RefreshSet();
              this.cashingIn = false;
              this.cardsButtons.Available = true;
              this.InitEmptyCards();
              this.InitSpinners();
              this.showingbonus = false;
              this.cashingIn = false;
              this.cardsButtons.Available = true;
              this.LabelClickToRemove.Text = "";
            }
          }
        }
      }
      this.diamondAnimFrame = (int) ((DateTime.Now - this.diamondAnimStartTime).TotalMilliseconds / 33.0);
      if (this.PanelMode == ManageCardsPanel.PANEL_MODE_CASH)
      {
        foreach (UICard uiCard in this.UICardList)
        {
          if (uiCard.Definition.cardGrade == 524288)
          {
            uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_diamond_anim.Length];
            uiCard.bigGradeImage.invalidateXtra();
          }
          else if (uiCard.Definition.cardGrade == 1048576)
          {
            uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond2_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_diamond2_anim.Length];
            uiCard.bigGradeImage.invalidateXtra();
          }
          else if (uiCard.Definition.cardGrade == 2097152)
          {
            uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond3_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_diamond3_anim.Length];
            uiCard.bigGradeImage.invalidateXtra();
          }
          else if (uiCard.Definition.cardGrade == 262144)
          {
            uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_gold_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_gold_anim.Length];
            uiCard.bigGradeImage.invalidateXtra();
          }
          else if (uiCard.Definition.cardGrade == 4194304)
          {
            uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_sapphire_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_sapphire_anim.Length];
            uiCard.bigGradeImage.invalidateXtra();
          }
        }
      }
      else
      {
        foreach (UICard uiCard in this.CardCatalog)
        {
          if (uiCard.Definition.cardGrade == 524288)
          {
            uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_diamond_anim.Length];
            uiCard.bigGradeImage.invalidateXtra();
          }
          else if (uiCard.Definition.cardGrade == 1048576)
          {
            uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond2_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_diamond2_anim.Length];
            uiCard.bigGradeImage.invalidateXtra();
          }
          else if (uiCard.Definition.cardGrade == 2097152)
          {
            uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond3_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_diamond3_anim.Length];
            uiCard.bigGradeImage.invalidateXtra();
          }
          else if (uiCard.Definition.cardGrade == 262144)
          {
            uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_gold_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_gold_anim.Length];
            uiCard.bigGradeImage.invalidateXtra();
          }
          else if (uiCard.Definition.cardGrade == 4194304)
          {
            uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_sapphire_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_sapphire_anim.Length];
            uiCard.bigGradeImage.invalidateXtra();
          }
        }
      }
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.closePlayCardsWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    public void navigateTest() => this.Navigate(2);

    private void Navigate(int panelType)
    {
      ((PlayCardsWindow) this.ParentForm).SwitchPanel(panelType);
    }

    private UICard makeUICard(CardTypes.CardDefinition def, int userid, int playerRank)
    {
      UICard uiCard = new UICard();
      uiCard.UserID = userid;
      uiCard.UserIDList.Add(userid);
      uiCard.Definition = def;
      switch (uiCard.Definition.cardColour)
      {
        case 1:
          uiCard.bigFrame = GFXLibrary.BlueCardOverlayBig;
          uiCard.bigFrameOver = GFXLibrary.BlueCardOverlayBigOver;
          break;
        case 2:
          uiCard.bigFrame = GFXLibrary.GreenCardOverlayBig;
          uiCard.bigFrameOver = GFXLibrary.GreenCardOverlayBigOver;
          break;
        case 3:
          uiCard.bigFrame = GFXLibrary.PurpleCardOverlayBig;
          uiCard.bigFrameOver = GFXLibrary.PurpleCardOverlayBigOver;
          break;
        case 4:
          uiCard.bigFrame = GFXLibrary.RedCardOverlayBig;
          uiCard.bigFrameOver = GFXLibrary.RedCardOverlayBigOver;
          break;
        case 5:
          uiCard.bigFrame = GFXLibrary.YellowCardOverlayBig;
          uiCard.bigFrameOver = GFXLibrary.YellowCardOverlayBigOver;
          break;
        default:
          uiCard.bigFrame = GFXLibrary.GreenCardOverlayBig;
          uiCard.bigFrameOver = GFXLibrary.GreenCardOverlayBigOver;
          break;
      }
      uiCard.bigImage = GFXLibrary.Instance.getCardImageBig(uiCard.Definition.id);
      uiCard.Size = uiCard.bigFrame.Size;
      uiCard.CustomTooltipID = 10101;
      uiCard.CustomTooltipData = uiCard.Definition.id;
      uiCard.bigGradeImage = new CustomSelfDrawPanel.CSDImage();
      int grade = CardTypes.getGrade(uiCard.Definition.cardGrade);
      switch (grade)
      {
        case 65536:
          uiCard.bigGradeImage.Image = (Image) GFXLibrary.CardGradeBronze;
          uiCard.bigGradeImage.Position = new Point(uiCard.Width - uiCard.bigGradeImage.Width, 0);
          break;
        case 131072:
          uiCard.bigGradeImage.Image = (Image) GFXLibrary.CardGradeSilver;
          uiCard.bigGradeImage.Position = new Point(uiCard.Width - uiCard.bigGradeImage.Width, 0);
          break;
        case 262144:
          uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_gold_anim[0];
          uiCard.bigGradeImage.Position = new Point(uiCard.Width - uiCard.bigGradeImage.Width - 3, 0);
          break;
        case 524288:
          uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond_anim[0];
          uiCard.bigGradeImage.Position = new Point(uiCard.Width - uiCard.bigGradeImage.Width - 3, -2);
          break;
        case 1048576:
          uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond2_anim[0];
          uiCard.bigGradeImage.Position = new Point(uiCard.Width - uiCard.bigGradeImage.Width - 3, -7);
          break;
        case 2097152:
          uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond3_anim[0];
          uiCard.bigGradeImage.Position = new Point(uiCard.Width - uiCard.bigGradeImage.Width - 3, -10);
          break;
        case 4194304:
          uiCard.bigGradeImage.Image = (Image) GFXLibrary.card_sapphire_anim[0];
          uiCard.bigGradeImage.Position = new Point(uiCard.Width - uiCard.bigGradeImage.Width - 3, -12);
          break;
        default:
          uiCard.bigGradeImage.Image = (Image) GFXLibrary.CardGradeBronze;
          uiCard.bigGradeImage.Position = new Point(uiCard.Width - uiCard.bigGradeImage.Width, 0);
          break;
      }
      uiCard.bigBaseImage = new CustomSelfDrawPanel.CSDImage();
      uiCard.bigBaseImage.Position = new Point(10, 11);
      uiCard.bigBaseImage.Size = uiCard.bigImage.Size;
      uiCard.bigBaseImage.Image = (Image) uiCard.bigImage;
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigBaseImage);
      uiCard.bigFrameImage = new CustomSelfDrawPanel.CSDImage();
      uiCard.bigFrameImage.Position = new Point(0, 0);
      uiCard.bigFrameImage.Size = uiCard.bigFrame.Size;
      uiCard.bigFrameImage.Image = (Image) uiCard.bigFrame;
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigFrameImage);
      switch (grade)
      {
        case 262144:
          uiCard.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
          uiCard.bigFrameExtraImage.Position = new Point(0, 0);
          uiCard.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_gold;
          uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigFrameExtraImage);
          break;
        case 524288:
        case 1048576:
        case 2097152:
          uiCard.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
          uiCard.bigFrameExtraImage.Position = new Point(0, 0);
          uiCard.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_diamond;
          uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigFrameExtraImage);
          break;
        case 4194304:
          uiCard.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
          uiCard.bigFrameExtraImage.Position = new Point(0, 0);
          uiCard.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_sapphire;
          uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigFrameExtraImage);
          break;
      }
      uiCard.bigGradeImage.Size = uiCard.bigGradeImage.Image.Size;
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigGradeImage);
      uiCard.bigTitle = new CustomSelfDrawPanel.CSDLabel();
      uiCard.bigTitle.Text = CardTypes.getDescriptionFromCard(uiCard.Definition.id);
      uiCard.bigTitle.Size = new Size(110, 48);
      uiCard.bigTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      uiCard.bigTitle.Font = uiCard.Definition.id != 1801 && uiCard.Definition.id != 1542 && uiCard.Definition.id != 3137 && uiCard.Definition.id != 1290 && uiCard.Definition.id != 1541 && uiCard.Definition.id != 1543 || !(Program.mySettings.LanguageIdent == "de") ? FontManager.GetFont("Arial", 9f, FontStyle.Bold) : FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      uiCard.bigTitle.Color = ARGBColors.White;
      uiCard.bigTitle.DropShadowColor = ARGBColors.Black;
      uiCard.bigTitle.Position = new Point(38, 12);
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigTitle);
      uiCard.bigEffect = new CustomSelfDrawPanel.CSDLabel();
      uiCard.bigEffect.Text = uiCard.Definition.EffectText;
      uiCard.bigEffect.Size = new Size(150, 64);
      uiCard.bigEffect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      uiCard.bigEffect.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      if (Program.mySettings.LanguageIdent == "de" && CardTypes.isGermanSmallDesc(uiCard.Definition.id))
        uiCard.bigEffect.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      uiCard.bigEffect.Color = ARGBColors.White;
      uiCard.bigEffect.DropShadowColor = ARGBColors.Black;
      uiCard.bigEffect.Position = new Point(14, 174);
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigEffect);
      CustomSelfDrawPanel.CSDLabel control1 = new CustomSelfDrawPanel.CSDLabel();
      control1.Position = new Point(2, 2);
      control1.Size = new Size(uiCard.Width, uiCard.Height);
      control1.Text = "";
      control1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control1.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
      control1.Color = ARGBColors.Yellow;
      control1.DropShadowColor = ARGBColors.Black;
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) control1);
      uiCard.countLabel = control1;
      Color color = playerRank >= uiCard.Definition.cardRank ? ARGBColors.White : ARGBColors.Red;
      CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
      control2.Position = new Point(150, 220);
      control2.Size = new Size(20, 13);
      control2.Text = uiCard.Definition.cardRank.ToString();
      control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      control2.Color = color;
      control2.DropShadowColor = ARGBColors.Black;
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) control2);
      uiCard.rankLabel = control2;
      if (playerRank < uiCard.Definition.cardRank)
        uiCard.Hilight(ARGBColors.Gray);
      else
        uiCard.Hilight(ARGBColors.White);
      uiCard.ScaleAll(0.95);
      return uiCard;
    }

    public bool TUTORIAL_cardsInCart() => this.ShoppingCart.Count > 0;

    private void sortByNameClicked()
    {
      this.sortByMode = this.sortByMode == 0 ? 2 : 0;
      if (this.PanelMode == ManageCardsPanel.PANEL_MODE_BUY)
        this.RefreshCards(this.AvailablePanelContent, this.CardCatalog, 500);
      else
        this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
      this.sortByName.Alpha = 0.5f;
      this.sortByType.Alpha = 1f;
      this.sortByQuantity.Alpha = 0.5f;
    }

    private void sortByTypeClicked()
    {
      this.sortByMode = this.sortByMode == 1 ? 3 : 1;
      if (this.PanelMode == ManageCardsPanel.PANEL_MODE_BUY)
        this.RefreshCards(this.AvailablePanelContent, this.CardCatalog, 500);
      else
        this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
      this.sortByName.Alpha = 1f;
      this.sortByType.Alpha = 0.5f;
      this.sortByQuantity.Alpha = 0.5f;
    }

    private void sortByQuantityClicked()
    {
      this.sortByMode = this.sortByMode == 7 ? 8 : 7;
      if (this.PanelMode == ManageCardsPanel.PANEL_MODE_BUY)
        this.RefreshCards(this.AvailablePanelContent, this.CardCatalog, 500);
      else
        this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
      this.sortByName.Alpha = 0.5f;
      this.sortByType.Alpha = 0.5f;
      this.sortByQuantity.Alpha = 1f;
    }

    private void compressClicked()
    {
      if (this.compressedCards)
        return;
      this.compressedCards = true;
      this.scrollbarAvailable.Value = 0;
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.PanelMode != ManageCardsPanel.PANEL_MODE_BUY ? this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500) : this.RefreshCards(this.AvailablePanelContent, this.CardCatalog, 500));
      this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
      this.AvailableContentScroll();
      this.Invalidate();
    }

    private void expandClicked()
    {
      if (!this.compressedCards)
        return;
      this.compressedCards = false;
      this.scrollbarAvailable.Value = 0;
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.PanelMode != ManageCardsPanel.PANEL_MODE_BUY ? this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500) : this.RefreshCards(this.AvailablePanelContent, this.CardCatalog, 500));
      this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
      this.AvailableContentScroll();
      this.Invalidate();
    }

    private void buyAndPlayCheckChanged()
    {
      if (this.buyAndPlayCheckBox.Checked)
        this.DynamicButtonLabel.Text = SK.Text("ManageCandsPanel_Get_And_Play_Card", "Get and Play Card");
      else
        this.DynamicButtonLabel.Text = SK.Text("ManageCandsPanel_Get_Cards", "Get Cards");
    }

    public void autoPlayCardDelegate(bool fromClick, bool fromValidate)
    {
      this.autoPlayCard(this.autoCardUserID, this.autoCardDef, fromClick, fromValidate);
    }

    private void autoPlayCard(
      int userID,
      CardTypes.CardDefinition def,
      bool fromClick,
      bool fromValidate)
    {
      if (GameEngine.Instance.World.WorldEnded || this.waitingResponse)
        return;
      this.waitingResponse = true;
      XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
      int villageID = -1;
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      if (!GameEngine.Instance.World.isCapital(selectedMenuVillage))
        villageID = selectedMenuVillage;
      int num1 = GameEngine.Instance.World.getRank() + 1;
      if (def.cardRank > num1)
      {
        int num2 = (int) MyMessageBox.Show(SK.Text("BuyCardsPanel_Rank_Too_low", "Your rank is too low to play this card.") + Environment.NewLine + SK.Text("BuyCardsPanel_Current_Rank", "Current Rank") + " : " + num1.ToString() + Environment.NewLine + SK.Text("BuyCardsPanel_Required_Rank", "Required Rank") + " : " + def.cardRank.ToString(), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
        this.waitingResponse = false;
      }
      else
      {
        this.autoCardUserID = userID;
        this.autoCardVillageID = villageID;
        this.autoCardDef = def;
        if (fromClick && Program.mySettings.ConfirmPlayCard)
        {
          GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_open_confirmation");
          this.PanelActive = false;
          this.waitingResponse = false;
          InterfaceMgr.Instance.openConfirmPlayCardPopup(this.autoCardDef, new ConfirmPlayCardPanel.CardClickPlayDelegate(this.autoPlayCardDelegate));
        }
        else if (!fromValidate && CardTypes.cardNeedsValidation(CardTypes.getCardType(this.autoCardDef.id)))
        {
          this.validateCardPossible(CardTypes.getCardType(this.autoCardDef.id), villageID);
        }
        else
        {
          if (InterfaceMgr.Instance.getCardWindow() != null)
            CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.getCardWindow());
          GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card");
          StatTrackingClient.Instance().ActivateTrigger(16, (object) this.buyAndPlayCheckBox.Checked);
          forEndpoint.PlayUserCard((ICardsRequest) new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), userID.ToString(), villageID.ToString(), RemoteServices.Instance.ProfileWorldID.ToString()), new CardsEndResponseDelegate(this.CardPlayed), (Control) this);
          try
          {
            GameEngine.Instance.cardsManager.removeProfileCard(userID);
          }
          catch (Exception ex)
          {
            int num3 = (int) MyMessageBox.Show(ex.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
          }
        }
      }
    }

    private void CardPlayed(ICardsProvider provider, ICardsResponse response)
    {
      if (!response.SuccessCode.HasValue || response.SuccessCode.Value != 1)
      {
        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_failed");
        int num1 = (int) MyMessageBox.Show(CardsManager.translateCardError(response.Message, this.autoCardDef.id), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
        try
        {
          GameEngine.Instance.cardsManager.addProfileCard(this.autoCardUserID, CardTypes.getStringFromCard(this.autoCardDef.id));
        }
        catch (Exception ex)
        {
          int num2 = (int) MyMessageBox.Show(ex.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
        }
        if (InterfaceMgr.Instance.getCardWindow() != null)
          CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.getCardWindow());
        StatTrackingClient.Instance().ActivateTrigger(16, (object) false);
      }
      else
      {
        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_success");
        GameEngine.Instance.cardsManager.ProfileCardsSet.Remove(this.autoCardUserID);
        GameEngine.Instance.cardsManager.CardPlayed(this.autoCardDef.cardCategory, this.autoCardDef.id, this.autoCardVillageID);
        GameEngine.Instance.cardsManager.addRecentCard(this.autoCardDef.id);
        StatTrackingClient.Instance().ActivateTrigger(6, (object) this.autoCardDef.id);
      }
      this.waitingResponse = false;
    }

    public void validateCardPossible(int cardType, int villageID)
    {
      RemoteServices.Instance.set_PreValidateCardToBePlayed_UserCallBack(new RemoteServices.PreValidateCardToBePlayed_UserCallBack(this.preValidateCardToBePlayedCallBack));
      RemoteServices.Instance.PreValidateCardToBePlayed(cardType, villageID);
    }

    public void preValidateCardToBePlayedCallBack(PreValidateCardToBePlayed_ReturnType returnData)
    {
      this.waitingResponse = false;
      if (!returnData.Success || CardTypes.isMercenaryTroopCardType(returnData.cardType) && returnData.otherErrorCode == 9999 && MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_UNIT_SPACE", "There is not enough unit space to accomodate these troops. If troops are dispatched from this village some may be lost upon their return.") + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      if (returnData.canPlayFully)
        this.autoPlayCardDelegate(false, true);
      else if (returnData.canPlayPartially)
      {
        string str = "";
        switch (returnData.cardType)
        {
          case 3085:
          case 3086:
          case 3087:
          case 3088:
          case 3089:
          case 3090:
          case 3091:
          case 3092:
          case 3093:
          case 3094:
          case 3095:
          case 3096:
          case 3097:
          case 3098:
          case 3099:
          case 3100:
          case 3101:
          case 3102:
          case 3103:
          case 3104:
          case 3105:
          case 3106:
          case 3107:
          case 3108:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_5", "Amount of Food gained will be") + " : " + returnData.numCanPlay.ToString();
            break;
          case 3109:
          case 3110:
          case 3111:
          case 3112:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_6", "Amount of Ale gained will be") + " : " + returnData.numCanPlay.ToString();
            break;
          case 3113:
          case 3114:
          case 3115:
          case 3116:
          case 3117:
          case 3118:
          case 3119:
          case 3120:
          case 3121:
          case 3122:
          case 3123:
          case 3124:
          case 3125:
          case 3126:
          case 3127:
          case 3128:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_7", "Amount of Resources gained will be") + " : " + returnData.numCanPlay.ToString();
            break;
          case 3129:
          case 3130:
          case 3131:
          case 3132:
          case 3133:
          case 3134:
          case 3135:
          case 3136:
          case 3137:
          case 3138:
          case 3139:
          case 3140:
          case 3141:
          case 3142:
          case 3143:
          case 3144:
          case 3145:
          case 3146:
          case 3147:
          case 3148:
          case 3149:
          case 3150:
          case 3151:
          case 3152:
          case 3153:
          case 3154:
          case 3155:
          case 3156:
          case 3157:
          case 3158:
          case 3159:
          case 3160:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_8", "Amount of Honour Goods gained will be") + " : " + returnData.numCanPlay.ToString();
            break;
          case 3161:
          case 3162:
          case 3163:
          case 3164:
          case 3165:
          case 3166:
          case 3167:
          case 3168:
          case 3173:
          case 3174:
          case 3175:
          case 3176:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_9", "Number of Weapons gained will be") + " : " + returnData.numCanPlay.ToString();
            break;
          case 3169:
          case 3170:
          case 3171:
          case 3172:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_10", "Amount of Armour gained will be") + " : " + returnData.numCanPlay.ToString();
            break;
          case 3177:
          case 3178:
          case 3179:
          case 3180:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_11", "Number of Catapults gained will be") + " : " + returnData.numCanPlay.ToString();
            break;
          case 3264:
          case 3265:
          case 3266:
          case 3267:
          case 3268:
          case 3269:
          case 3270:
          case 3271:
          case 3272:
          case 3273:
          case 3274:
          case 3275:
          case 3276:
          case 3277:
          case 3278:
          case 3279:
          case 3280:
          case 3281:
          case 3282:
          case 3283:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_1", "Number of Troops that can be recruited") + " : " + returnData.numCanPlay.ToString();
            break;
          case 3287:
          case 3288:
          case 3289:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_2", "Number of Scouts that can be recruited") + " : " + returnData.numCanPlay.ToString();
            break;
          case 3290:
          case 3291:
          case 3292:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_3", "Number of Monks that can be recruited") + " : " + returnData.numCanPlay.ToString();
            break;
          case 3293:
          case 3294:
          case 3295:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_4", "Number of Merchants that can be recruited") + " : " + returnData.numCanPlay.ToString();
            break;
        }
        if (MyMessageBox.Show(str + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo) != DialogResult.Yes)
          return;
        this.autoPlayCardDelegate(false, true);
      }
      else if (returnData.otherErrorCode != 0)
      {
        if (returnData.otherErrorCode == -2)
        {
          int num1 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnData.cardType, 5), SK.Text("GENERIC_Error", "Error"));
        }
        else
        {
          if (returnData.otherErrorCode != -3)
            return;
          GameEngine.Instance.displayedVillageLost(returnData.villageID, true);
        }
      }
      else
      {
        switch (returnData.cardType)
        {
          case 3085:
          case 3086:
          case 3087:
          case 3088:
          case 3089:
          case 3090:
          case 3091:
          case 3092:
          case 3093:
          case 3094:
          case 3095:
          case 3096:
          case 3097:
          case 3098:
          case 3099:
          case 3100:
          case 3101:
          case 3102:
          case 3103:
          case 3104:
          case 3105:
          case 3106:
          case 3107:
          case 3108:
            int num2 = (int) MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_101", "Not enough space in the Granary."), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3109:
          case 3110:
          case 3111:
          case 3112:
            int num3 = (int) MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_102", "Not enough space in the Inn."), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3113:
          case 3114:
          case 3115:
          case 3116:
          case 3117:
          case 3118:
          case 3119:
          case 3120:
          case 3121:
          case 3122:
          case 3123:
          case 3124:
          case 3125:
          case 3126:
          case 3127:
          case 3128:
            int num4 = (int) MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_103", "Not enough space on the Stockpile."), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3129:
          case 3130:
          case 3131:
          case 3132:
          case 3133:
          case 3134:
          case 3135:
          case 3136:
          case 3137:
          case 3138:
          case 3139:
          case 3140:
          case 3141:
          case 3142:
          case 3143:
          case 3144:
          case 3145:
          case 3146:
          case 3147:
          case 3148:
          case 3149:
          case 3150:
          case 3151:
          case 3152:
          case 3153:
          case 3154:
          case 3155:
          case 3156:
          case 3157:
          case 3158:
          case 3159:
          case 3160:
            int num5 = (int) MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_104", "Not enough space in the Village Hall."), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3161:
          case 3162:
          case 3163:
          case 3164:
          case 3165:
          case 3166:
          case 3167:
          case 3168:
          case 3169:
          case 3170:
          case 3171:
          case 3172:
          case 3173:
          case 3174:
          case 3175:
          case 3176:
          case 3177:
          case 3178:
          case 3179:
          case 3180:
            int num6 = (int) MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_105", "Not enough space in the Armoury."), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3264:
          case 3265:
          case 3266:
          case 3267:
          case 3268:
          case 3269:
          case 3270:
          case 3271:
          case 3272:
          case 3273:
          case 3274:
          case 3275:
          case 3276:
          case 3277:
          case 3278:
          case 3279:
          case 3280:
          case 3281:
          case 3282:
          case 3283:
            int num7 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnData.cardType, 1), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3287:
          case 3288:
          case 3289:
            int num8 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnData.cardType, 2), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3290:
          case 3291:
          case 3292:
            int num9 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnData.cardType, 3), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3293:
          case 3294:
          case 3295:
            int num10 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnData.cardType, 4), SK.Text("GENERIC_Error", "Error"));
            break;
        }
      }
    }
  }
}
