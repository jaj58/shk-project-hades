// Decompiled with JetBrains decompiler
// Type: Kingdoms.PlayCardsPanel
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
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PlayCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
  {
    private IContainer components;
    private DateTime lastUpdatedProgressBars = DateTime.Now.AddSeconds(30.0);
    private DateTime lastTickCall = DateTime.Now.AddSeconds(-60.0);
    private DateTime lastRefresh = DateTime.Now;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private List<UICard> UICardList = new List<UICard>();
    private List<UICard> UICardListInplay = new List<UICard>();
    private CustomSelfDrawPanel.CSDButton searchButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton clearSearchButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private UICard LastMouseoverCard;
    private float bigCardAlphaTarget = 1f;
    private float bigCardAlpha;
    private static float fadeStep = 0.1f;
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
    private int currentCardSection = -1;
    private string sectionName;
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
    private CustomSelfDrawPanel.CSDButton sortByRarity = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton compressButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton expandButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollbarInplay = new CustomSelfDrawPanel.CSDVertScrollBar();
    private UICard lastRequestCard;
    private bool waitingResponse;
    private bool usingRecentFilter;
    private Bitmap greenbar = new Bitmap(29, 3);
    private int selectedVillage;
    private List<CustomSelfDrawPanel.CSDButton> FilterButtons = new List<CustomSelfDrawPanel.CSDButton>();
    private bool compressedCards;
    private List<UICard> dummyCards = new List<UICard>();
    private int lastRequestUserID;
    private int diamondAnimFrame;
    private DateTime diamondAnimStartTime = DateTime.Now;

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

    public PlayCardsPanel()
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
      this.ContentWidth = this.Width - 2 * PlayCardsPanel.BorderPadding;
      this.AvailablePanelWidth = 800;
      this.InplayPanelWidth = this.ContentWidth - PlayCardsPanel.BorderPadding - this.AvailablePanelWidth;
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
      this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, 550);
      this.AvailablePanel.Position = new Point(8, this.Height - 8 - 550);
      this.AvailablePanel.Alpha = 0.8f;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanel);
      this.AvailablePanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
      this.sortBack.Image = (Image) GFXLibrary.sort_back;
      this.sortBack.Position = new Point(12, this.AvailablePanel.Height - 37);
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
      this.sortByName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByNameClicked), "PlayCardsPanel_sort_by_name");
      this.sortBack.addControl((CustomSelfDrawPanel.CSDControl) this.sortByName);
      this.sortByType.ImageNorm = (Image) GFXLibrary.sort_normal;
      this.sortByType.ImageOver = (Image) GFXLibrary.sort_over;
      this.sortByType.ImageClick = (Image) GFXLibrary.sort_in;
      this.sortByType.Position = new Point(228, 4);
      this.sortByType.Text.Text = SK.Text("Card_Sorting_Type", "Sort By Type");
      this.sortByType.Text.Color = ARGBColors.White;
      this.sortByType.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.sortByType.TextYOffset = -1;
      this.sortByType.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByTypeClicked), "PlayCardsPanel_sort_by_type");
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
      this.sortByRarity.ImageNorm = (Image) GFXLibrary.sort_normal;
      this.sortByRarity.ImageOver = (Image) GFXLibrary.sort_over;
      this.sortByRarity.ImageClick = (Image) GFXLibrary.sort_in;
      this.sortByRarity.Position = new Point(368, 4);
      this.sortByRarity.Text.Text = SK.Text("Card_Sorting_Rarity", "Sort By Rarity");
      this.sortByRarity.Text.Color = ARGBColors.White;
      this.sortByRarity.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.sortByRarity.TextYOffset = -1;
      this.sortByRarity.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByRarityClicked), "PlayCardsPanel_sort_by_rarity");
      this.compressButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_minus_norm;
      this.compressButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_minus_over;
      this.compressButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_minus_in;
      this.compressButton.Position = new Point(677, 16);
      this.compressButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.compressClicked), "PlayCardsPanel_compress_cards");
      this.sortBack.addControl((CustomSelfDrawPanel.CSDControl) this.compressButton);
      this.expandButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_plus_norm;
      this.expandButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_plus_over;
      this.expandButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_plus_in;
      this.expandButton.Position = new Point(677, -2);
      this.expandButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.expandClicked), "PlayCardsPanel_expand_cards");
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
      int width1 = this.Width;
      int borderPadding = PlayCardsPanel.BorderPadding;
      int width2 = this.AvailablePanel.Width;
      this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      this.closeImage.Size = this.closeImage.Image.Size;
      this.closeImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
      this.closeImage.Position = new Point(this.Width - 14 - 17, 10);
      this.closeImage.CustomTooltipID = 10100;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeImage);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 25, new Point(this.Width - 1 - 17 - 50 + 3, 5), true);
      CustomSelfDrawPanel.CSDFill control4 = new CustomSelfDrawPanel.CSDFill();
      control4.FillColor = Color.FromArgb((int) byte.MaxValue, 130, 129, 126);
      control4.Size = new Size(this.Width - 10, 1);
      control4.Position = new Point(5, 34);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control4);
      CustomSelfDrawPanel.UICardsButtons control5 = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) this.ParentForm);
      control5.Position = new Point(808, 37);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control5);
      control5.offersButton.Visible = GameEngine.Instance.cardsManager.PremiumOfferAvailable();
      control5.inviteButton.Visible = !GameEngine.Instance.cardsManager.PremiumOfferAvailable();
      this.labelTitle.Position = new Point(27, 8);
      this.labelTitle.Size = new Size(600, 64);
      this.labelTitle.Text = "";
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
      if (cardSection != 0)
      {
        CustomSelfDrawPanel.CSDButton control6 = new CustomSelfDrawPanel.CSDButton();
        control6.ImageNorm = (Image) GFXLibrary.button_cards_all_normal;
        control6.ImageOver = (Image) GFXLibrary.button_cards_all_over;
        control6.ImageClick = (Image) GFXLibrary.button_cards_all_over;
        control6.Position = new Point(390, 0);
        control6.Text.Text = SK.Text("PlayCardsPanel_All_Your_Cards", "All Your Cards");
        control6.TextYOffset = -3;
        control6.Text.Color = ARGBColors.Black;
        control6.Text.Size = new Size(control6.Size.Width - 45, control6.Size.Height);
        control6.Text.Position = new Point(45, 0);
        control6.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        control6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showAllCardsClick), "PlayCardsPanel_show_all_cards");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control6);
      }
      CustomSelfDrawPanel.CSDButton control7 = new CustomSelfDrawPanel.CSDButton();
      control7.ImageNorm = (Image) GFXLibrary.button_cards_in_play_normal;
      control7.ImageOver = (Image) GFXLibrary.button_cards_in_play_over;
      control7.ImageClick = (Image) GFXLibrary.button_cards_in_play_over;
      control7.Position = new Point(570, 0);
      control7.Text.Text = SK.Text("PlayCardsPanel_Cards_In_Play", "Cards In Play");
      control7.TextYOffset = -3;
      control7.Text.Color = ARGBColors.Black;
      control7.Text.Size = new Size(control7.Size.Width - 30, control7.Size.Height);
      control7.Text.Position = new Point(30, 0);
      control7.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      control7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showCardsInPlay), "PlayCardsPanel_cards_in_play");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control7);
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
      CardTypes.CardDefinition filter = new CardTypes.CardDefinition();
      filter.cardCategory = cardSection;
      if (GameEngine.Instance.World.getTutorialStage() == 8 || GameEngine.Instance.World.getTutorialStage() == 12)
        filter.rewardcard = true;
      filter.rewardcard = true;
      GameEngine.Instance.cardsManager.searchProfileCards(filter, "", ((PlayCardsWindow) this.ParentForm).getNameSearchText());
      this.sectionName = cardSection >= 15 ? Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(CardFilters.getName2(cardSection).ToLower()) : CardSections.getName(cardSection);
      this.labelTitle.Text = this.sectionName + " : " + GameEngine.Instance.cardsManager.ProfileCardsSearch.Count.ToString();
      this.GetCardsAvailable(false);
      this.RenderCards();
      this.mainBackgroundImage.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelHandler));
      if (cardSection == 0)
        this.InitFilters();
      if (cardSection == 0)
        ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = !this.searchButton.Visible;
      else
        ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = false;
      this.Invalidate();
    }

    private void InitFilters()
    {
      foreach (CustomSelfDrawPanel.CSDControl filterButton in this.FilterButtons)
        this.mainBackgroundImage.removeControl(filterButton);
      this.FilterButtons.Clear();
      int currentFilter1 = 0;
      if (this.usingRecentFilter)
        currentFilter1 = 1048576;
      else if (GameEngine.Instance.cardsManager.lastUserCardSearchCriteria.cardRank != 0)
        currentFilter1 = 2097152;
      else if (GameEngine.Instance.cardsManager.lastUserCardSearchCriteria != null)
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
      if (GameEngine.Instance.cardsManager.NewCategoriesAvailable_Parish)
      {
        if ((currentFilter1 & 524288) != 0)
          this.addFilterButton(524288, GFXLibrary.CardFilters_Parish, num37++, currentFilter1);
        else
          this.addFilterButton(524288, GFXLibrary.CardFilters_Parish, num37++, currentFilter1);
      }
      BaseImage buttonsRecentNormal = GFXLibrary.card_type_buttons_recent_normal;
      BaseImage buttonsRecentOver = GFXLibrary.card_type_buttons_recent_over;
      BaseImage typeButtonsRecentIn = GFXLibrary.card_type_buttons_recent_in;
      int index51 = num37;
      int num45 = index51 + 1;
      int currentFilter52 = currentFilter1;
      this.addFilterButton(1048576, buttonsRecentNormal, buttonsRecentOver, typeButtonsRecentIn, index51, currentFilter52);
      BaseImage[] cardFiltersPlayable = GFXLibrary.CardFilters_Playable;
      int index52 = num45;
      int num46 = index52 + 1;
      int currentFilter53 = currentFilter1;
      this.addFilterButton(2097152, cardFiltersPlayable, index52, currentFilter53);
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
        control.Position = new Point(this.AvailablePanel.X + this.AvailablePanel.Width - 84, this.AvailablePanel.Y + 8 + index * 24);
      }
      else
      {
        control.ImageNorm = (Image) normalImage;
        control.ImageOver = (Image) overImage;
        control.ImageClick = (Image) clickedImage;
        control.Data = category;
        control.CustomTooltipData = category;
        control.CustomTooltipID = 10105;
        control.Position = new Point(this.AvailablePanel.X + this.AvailablePanel.Width - 84, this.AvailablePanel.Y + 8 + index * 24);
        control.ClipRect = new Rectangle(0, 6, 51, 22);
        control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.NewFilterClick), "PlayCardsPanel_filter");
      }
      this.FilterButtons.Add(control);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control);
    }

    public void NewFilterClick()
    {
      CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) this.ClickedControl;
      if (!this.waitingResponse)
      {
        CardTypes.CardDefinition filter = new CardTypes.CardDefinition();
        int data = clickedControl.Data;
        this.usingRecentFilter = data == 1048576;
        if (data == 2097152)
          filter.cardRank = GameEngine.Instance.World.getRank() + 1;
        else
          filter.newCardCategoryFilter = data;
        GameEngine.Instance.cardsManager.searchProfileCards(filter, "", ((PlayCardsWindow) this.ParentForm).getNameSearchText());
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
        this.InitFilters();
      }
      this.labelTitle.Text = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(CardFilters.getName2(clickedControl.CustomTooltipData).ToLower()) + " : " + GameEngine.Instance.cardsManager.countCardsInCategory(clickedControl.CustomTooltipData).ToString();
      if (this.usingRecentFilter)
      {
        this.sortBack.Visible = false;
        this.GetCardsRecent();
        ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = false;
        this.searchButton.Visible = false;
      }
      else
        this.GetCardsAvailable(true);
      this.clearSearchButton.Visible = ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible;
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
      this.scrollbarAvailable.Value = 0;
      this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
      this.AvailableContentScroll();
      this.Invalidate();
    }

    public void FilterClick()
    {
      if (!this.waitingResponse)
      {
        int data = this.ClickedControl.Data;
        CardTypes.CardDefinition filter = new CardTypes.CardDefinition();
        if (data != 999)
          filter.cardFilter = data;
        else
          filter.cardColour = 2;
        GameEngine.Instance.cardsManager.searchProfileCards(filter, "", ((PlayCardsWindow) this.ParentForm).getNameSearchText());
      }
      this.GetCardsAvailable(true);
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
      this.scrollbarAvailable.Value = 0;
      this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
      this.AvailableContentScroll();
      this.Invalidate();
    }

    public void searchClicked()
    {
      this.searchButton.Visible = false;
      this.clearSearchButton.Visible = true;
      ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = true;
      ((PlayCardsWindow) this.ParentForm).tbSearchBox.Focus();
      this.handleSearchTextChanged();
    }

    public void forceSearch()
    {
      this.searchButton.Visible = false;
      this.clearSearchButton.Visible = true;
      ((PlayCardsWindow) this.ParentForm).tbSearchBox.Visible = true;
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
      GameEngine.Instance.cardsManager.searchProfileCardsRedoLast(((PlayCardsWindow) this.ParentForm).getNameSearchText());
      this.GetCardsAvailable(false);
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
      this.scrollbarAvailable.Value = 0;
      this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
      this.AvailableContentScroll();
      this.Invalidate();
    }

    private void GetCardsAvailable(bool redosearch)
    {
      if (redosearch)
        GameEngine.Instance.cardsManager.searchProfileCardsRedoLast();
      Dictionary<int, int> dictionary1 = new Dictionary<int, int>();
      foreach (int key1 in GameEngine.Instance.cardsManager.ProfileCardsSearch)
      {
        int id = GameEngine.Instance.cardsManager.ProfileCards[key1].id;
        if (dictionary1.ContainsKey(id))
        {
          Dictionary<int, int> dictionary2;
          int key2;
          (dictionary2 = dictionary1)[key2 = id] = dictionary2[key2] + 1;
        }
        else
          dictionary1.Add(id, 1);
      }
      foreach (UICard uiCard in this.UICardList)
      {
        uiCard.clearControls();
        if (uiCard.Parent != null)
          uiCard.Parent.removeControl((CustomSelfDrawPanel.CSDControl) uiCard);
      }
      this.UICardList.Clear();
      int num = GameEngine.Instance.World.getRank() + 1;
      foreach (int key in GameEngine.Instance.cardsManager.ProfileCardsSearch)
      {
        int id = GameEngine.Instance.cardsManager.ProfileCards[key].id;
        try
        {
          if (dictionary1.ContainsKey(id))
          {
            UICard uiCard = new UICard();
            uiCard.cardCount = dictionary1[id];
            uiCard.UserID = key;
            uiCard.UserIDList.Add(key);
            uiCard.Definition = GameEngine.Instance.cardsManager.ProfileCards[key];
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
            if (num < uiCard.Definition.cardRank)
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
            control2.Text = uiCard.Definition.cardRank.ToString();
            control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            control2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            control2.Color = ARGBColors.White;
            control2.DropShadowColor = ARGBColors.Black;
            uiCard.addControl((CustomSelfDrawPanel.CSDControl) control2);
            uiCard.rankLabel = control2;
            uiCard.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardClickPlay));
            uiCard.ScaleAll(0.95);
            this.UICardList.Add(uiCard);
            dictionary1.Remove(id);
            if (num < uiCard.Definition.cardRank)
              uiCard.Hilight(ARGBColors.Gray);
            else
              uiCard.Hilight(ARGBColors.White);
          }
          else
          {
            foreach (UICard uiCard in this.UICardList)
            {
              if (uiCard.Definition.id == id)
              {
                uiCard.UserIDList.Add(key);
                break;
              }
            }
          }
        }
        catch (Exception ex)
        {
          UniversalDebugLog.Log("EXCEPTION " + ex.ToString());
        }
      }
      GFXLibrary.Instance.closeBigCardsLoader();
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
      if (GameEngine.Instance.World.getTutorialStage() == 8)
        list.Sort((IComparer<UICard>) UICard.TUT2cardsNameComparer);
      else if (GameEngine.Instance.World.getTutorialStage() == 12)
        list.Sort((IComparer<UICard>) UICard.TUTcardsNameComparer);
      else if (this.sortByMode == 0)
        list.Sort((IComparer<UICard>) UICard.cardsNameComparer);
      else if (this.sortByMode == 1)
        list.Sort((IComparer<UICard>) UICard.cardsIDComparer);
      else if (this.sortByMode == 2)
        list.Sort((IComparer<UICard>) UICard.cardsNameComparerReverse);
      else if (this.sortByMode == 3)
        list.Sort((IComparer<UICard>) UICard.cardsIDComparerReverse);
      else if (this.sortByMode == 7)
        list.Sort((IComparer<UICard>) UICard.cardsQuantityComparer);
      else if (this.sortByMode == 8)
        list.Sort((IComparer<UICard>) UICard.cardsQuantityComparerReverse);
      int num1 = GameEngine.Instance.World.getRank() + 1;
      content.clearDirectControlsOnly();
      foreach (CustomSelfDrawPanel.CSDControl dummyCard in this.dummyCards)
        dummyCard.clearControls();
      this.dummyCards.Clear();
      int num2 = 0;
      int x1 = 16;
      int y1 = 0;
      if (this.currentCardSection == 0)
        x1 = 0;
      if (numArray != null)
      {
        this.sortBack.Visible = false;
        this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, this.AvailablePanelContent.ClipRect.Y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanel.Height - PlayCardsPanel.BorderPadding * 2);
        int num3 = 0;
        int num4 = -1;
        for (int index1 = 0; index1 < numArray.Length; index1 += 3)
        {
          if (numArray[index1 + 2] != num4)
          {
            int num5 = numArray[index1];
            int x2 = numArray[index1 + 1] * 178;
            int y2 = (numArray[index1 + 2] - num3) * 237;
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
            if (!flag1 && (cardDefinition.cardRank <= 0 || cardDefinition.cardRarity <= 0 || cardDefinition.available != 1) && x2 == 0)
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
            if (y2 + 237 > num2)
              num2 = y2 + 237;
            if (flag1)
            {
              control1.Position = new Point(x2, y2);
              content.addControl((CustomSelfDrawPanel.CSDControl) control1);
              if (num1 < control1.Definition.cardRank || control1.UserIDList.Count == 0)
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
            else if (cardDefinition.cardRank > 0 && cardDefinition.cardRarity > 0 && cardDefinition.available == 1)
            {
              UICard control2 = BuyCardsPanel.makeUICard(cardDefinition, RemoteServices.Instance.UserID, 10000);
              control2.Position = new Point(x2, y2);
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
              if (cardDefinition.cardPoints > 0)
              {
                control4.Text = SK.Text("CARDS_GetCard", "Get Card");
                control4.Data = cardDefinition.id;
                control4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.linkToBuy));
              }
              else
                control4.Text = SK.Text("CARDS_No_Cards", "No Cards");
              control4.Position = new Point(x2 + 3, y2 + 5);
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
        if (!this.usingRecentFilter)
          this.sortBack.Visible = true;
        this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, this.AvailablePanelContent.ClipRect.Y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanel.Height - PlayCardsPanel.BorderPadding * 2 - 24);
        int num6 = 0;
        int num7 = 0;
        foreach (UICard control5 in list)
        {
          control5.Position = new Point(x1, y1);
          content.addControl((CustomSelfDrawPanel.CSDControl) control5);
          num7 = y1;
          if (x1 > width)
          {
            x1 = 16;
            if (this.currentCardSection == 0)
              x1 = 0;
            if (!this.compressedCards)
              y1 += control5.Height + 8;
            else
              y1 += 58;
          }
          else
            x1 += control5.Width + 12;
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
          if (num1 < control5.Definition.cardRank || control5.UserIDList.Count == 0)
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
      PlayCardsPanel.disableCardsInPlay(this.UICardList);
      content.invalidate();
      return num2;
    }

    private void linkToBuy()
    {
      if (this.ClickedControl == null)
        return;
      CustomSelfDrawPanel.CSDLabel clickedControl = (CustomSelfDrawPanel.CSDLabel) this.ClickedControl;
      if (this.usingRecentFilter)
        ((PlayCardsWindow) this.ParentForm).SwitchToManageAndFilter(((UICard) this.ClickedControl.Parent).Definition.newCardCategoryFilter, clickedControl.Data);
      else
        ((PlayCardsWindow) this.ParentForm).SwitchToManageAndFilter(GameEngine.Instance.cardsManager.lastUserCardSearchCriteria.newCardCategoryFilter, clickedControl.Data);
    }

    private void mouseWheelHandler(int delta)
    {
      if ((delta <= 0 || this.scrollbarAvailable.Value - delta * 15 <= 0) && (delta >= 0 || this.scrollbarAvailable.Value - delta * 15 >= this.scrollbarAvailable.Max) || this.waitingResponse)
        return;
      this.scrollbarAvailable.Value += delta * -15;
      this.AvailableContentScroll();
    }

    public void UpdateScrollbar(
      CustomSelfDrawPanel.CSDVertScrollBar bar,
      CustomSelfDrawPanel.CSDImage content)
    {
      bar.Visible = content.Height > content.ClipRect.Height;
      bar.Max = content.Height - content.ClipRect.Height;
      bar.NumVisibleLines = content.ClipRect.Height;
    }

    public void RenderCards()
    {
      int height1 = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
      this.AvailablePanelContent.Position = new Point(PlayCardsPanel.BorderPadding, PlayCardsPanel.BorderPadding);
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height1);
      this.AvailablePanelContent.ClipRect = new Rectangle(0, 0, this.AvailablePanel.Width - PlayCardsPanel.BorderPadding, this.AvailablePanel.Height - PlayCardsPanel.BorderPadding * 2 - 24);
      this.AvailablePanel.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanelContent);
      if (height1 < this.AvailablePanelContent.ClipRect.Height)
        height1 = this.AvailablePanelContent.ClipRect.Height;
      this.scrollbarAvailable.Position = new Point(this.AvailablePanel.Width - PlayCardsPanel.BorderPadding - PlayCardsPanel.BorderPadding / 2, this.AvailablePanel.Y + PlayCardsPanel.BorderPadding / 2);
      this.scrollbarAvailable.Size = new Size(PlayCardsPanel.BorderPadding, this.AvailablePanel.Height - PlayCardsPanel.BorderPadding - 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollbarAvailable);
      this.scrollbarAvailable.Value = 0;
      this.scrollbarAvailable.StepSize = 200;
      this.scrollbarAvailable.Max = this.AvailablePanelContent.Height - this.AvailablePanelContent.ClipRect.Height;
      this.scrollbarAvailable.NumVisibleLines = this.AvailablePanelContent.ClipRect.Height;
      this.scrollbarAvailable.OffsetTL = new Point(1, 5);
      this.scrollbarAvailable.OffsetBR = new Point(0, -10);
      this.scrollbarAvailable.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.AvailableContentScroll));
      this.scrollbarAvailable.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.cardpanel_scroll_thumb_top, (Image) GFXLibrary.cardpanel_scroll_thumb_mid, (Image) GFXLibrary.cardpanel_scroll_thumb_botom);
      if (height1 <= this.AvailablePanelContent.ClipRect.Height)
        this.scrollbarAvailable.Visible = false;
      else
        this.scrollbarAvailable.Visible = true;
      int height2 = this.AvailablePanelContent.Height;
      int height3 = this.AvailablePanelContent.ClipRect.Height;
    }

    private void AvailableContentScroll()
    {
      int y = this.scrollbarAvailable.Value;
      this.AvailablePanelContent.Position = new Point(this.AvailablePanelContent.Position.X, PlayCardsPanel.BorderPadding - y);
      this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanelContent.ClipRect.Height);
      this.AvailablePanelContent.invalidate();
      this.AvailablePanel.invalidate();
    }

    private void InplayContentScroll()
    {
      int y = this.scrollbarInplay.Value;
      this.InplayPanelContent.Position = new Point(this.InplayPanelContent.Position.X, PlayCardsPanel.BorderPadding - y);
      this.InplayPanelContent.ClipRect = new Rectangle(this.InplayPanelContent.ClipRect.X, y, this.InplayPanelContent.ClipRect.Width, this.InplayPanelContent.ClipRect.Height);
      this.InplayPanelContent.invalidate();
      this.AvailablePanel.invalidate();
    }

    private void cardClickPlay() => this.doCardClickPlay(true, false);

    private void doCardClickPlay(bool fromClick, bool fromValidate)
    {
      if (GameEngine.Instance.World.WorldEnded)
        return;
      try
      {
        if (this.waitingResponse)
          return;
        if (this.ClickedControl.GetType() == typeof (UICard) || !fromClick)
        {
          UICard uiCard = !fromClick ? this.lastRequestCard : (UICard) this.ClickedControl;
          this.lastRequestCard = uiCard;
          this.waitingResponse = true;
          XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
          this.selectedVillage = -1;
          int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
          if (!GameEngine.Instance.World.isCapital(selectedMenuVillage) || CardTypes.getCardType(uiCard.Definition.id) == 3076)
            this.selectedVillage = selectedMenuVillage;
          int num1 = GameEngine.Instance.World.getRank() + 1;
          if (this.lastRequestCard.Definition.cardRank > num1)
          {
            int num2 = (int) MyMessageBox.Show(SK.Text("BuyCardsPanel_Rank_Too_low", "Your rank is too low to play this card.") + Environment.NewLine + SK.Text("BuyCardsPanel_Current_Rank", "Current Rank") + " : " + num1.ToString() + Environment.NewLine + SK.Text("BuyCardsPanel_Required_Rank", "Required Rank") + " : " + this.lastRequestCard.Definition.cardRank.ToString(), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
            this.waitingResponse = false;
          }
          else if ((this.lastRequestCard.Definition.id == 3109 || this.lastRequestCard.Definition.id == 3110 || this.lastRequestCard.Definition.id == 3111 || this.lastRequestCard.Definition.id == 3112) && GameEngine.Instance.Village != null && GameEngine.Instance.Village.countBuildingType(35) == 0)
          {
            int num3 = (int) MyMessageBox.Show(SK.Text("PlayCard_No_Inn_Available", "An inn must be built at the current village before this card can be played."));
            this.waitingResponse = false;
          }
          else
          {
            if (fromClick && Program.mySettings.ConfirmPlayCard)
            {
              GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_open_confirmation");
              this.PanelActive = false;
              this.waitingResponse = false;
              InterfaceMgr.Instance.openConfirmPlayCardPopup(uiCard.Definition, new ConfirmPlayCardPanel.CardClickPlayDelegate(this.doCardClickPlay));
              return;
            }
            if (!fromValidate)
            {
              if (CardTypes.cardNeedsValidation(CardTypes.getCardType(uiCard.Definition.id)))
              {
                this.validateCardPossible(CardTypes.getCardType(uiCard.Definition.id), this.selectedVillage);
                return;
              }
            }
            try
            {
              if (InterfaceMgr.Instance.getCardWindow() != null)
                CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.getCardWindow());
              if (fromClick)
                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card");
              forEndpoint.PlayUserCard((ICardsRequest) new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), uiCard.UserIDList[0].ToString(), this.selectedVillage.ToString(), RemoteServices.Instance.ProfileWorldID.ToString()), new CardsEndResponseDelegate(this.CardPlayed), (Control) this);
              GameEngine.Instance.cardsManager.removeProfileCard(uiCard.UserIDList[0]);
              if (this.lastRequestCard.cardCount > 1 || this.usingRecentFilter)
              {
                this.lastRequestUserID = uiCard.UserIDList[0];
                this.lastRequestCard.UserIDList.Remove(uiCard.UserIDList[0]);
                --this.lastRequestCard.cardCount;
                if (this.lastRequestCard.cardCount > 1)
                {
                  this.lastRequestCard.countLabel.Text = this.lastRequestCard.cardCount.ToString();
                  this.lastRequestCard.countLabel.Font = this.lastRequestCard.cardCount < 100 ? FontManager.GetFont("Arial", 18f, FontStyle.Bold) : FontManager.GetFont("Arial", 16f, FontStyle.Bold);
                }
                else
                {
                  this.lastRequestCard.countLabel.Text = "";
                  if (this.usingRecentFilter && this.lastRequestCard.cardCount < 1)
                  {
                    this.lastRequestCard.buyCardsLabel.Visible = true;
                    this.lastRequestCard.Hilight(ARGBColors.Gray);
                    this.lastRequestCard.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
                    this.lastRequestCard.buyCardsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.linkToBuy));
                  }
                }
                this.AvailablePanelContent.invalidate();
                if (this.usingRecentFilter)
                  this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
              }
              else
              {
                this.UICardList.Remove(this.lastRequestCard);
                this.lastRequestUserID = uiCard.UserIDList[0];
                this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
                this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
              }
            }
            catch (Exception ex)
            {
              int num4 = (int) MyMessageBox.Show(ex.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
            }
          }
        }
        if (this.usingRecentFilter)
          this.labelTitle.Text = SK.Text("CARDFILTER_RECENT2", "Recent") + " : " + GameEngine.Instance.cardsManager.countCardsInCategory(1048576).ToString();
        else
          this.labelTitle.Text = this.sectionName + " : " + GameEngine.Instance.cardsManager.ProfileCardsSearch.Count.ToString();
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log(ex.ToString());
      }
    }

    public void validateCardPossible(int cardType, int villageID)
    {
      RemoteServices.Instance.set_PreValidateCardToBePlayed_UserCallBack(new RemoteServices.PreValidateCardToBePlayed_UserCallBack(this.preValidateCardToBePlayedCallBack));
      RemoteServices.Instance.PreValidateCardToBePlayed(cardType, villageID);
    }

    public void preValidateCardToBePlayedCallBack(PreValidateCardToBePlayed_ReturnType returnData)
    {
      this.waitingResponse = false;
      if (returnData.Success)
      {
        if (CardTypes.isMercenaryTroopCardType(returnData.cardType) && returnData.otherErrorCode == 9999 && MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_UNIT_SPACE", "There is not enough unit space to accomodate these troops. If troops are dispatched from this village some may be lost upon their return.") + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo) == DialogResult.No)
          return;
        if (returnData.canPlayFully)
        {
          this.doCardClickPlay(false, true);
          return;
        }
        if (returnData.canPlayPartially)
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
          if (MyMessageBox.Show(str + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo) == DialogResult.Yes)
          {
            this.doCardClickPlay(false, true);
            return;
          }
        }
        else if (returnData.otherErrorCode != 0)
        {
          if (returnData.otherErrorCode == -2)
          {
            int num1 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnData.cardType, 5), SK.Text("GENERIC_Error", "Error"));
          }
          else if (returnData.otherErrorCode == -3)
            GameEngine.Instance.displayedVillageLost(returnData.villageID, true);
        }
        else
        {
          MyMessageBoxPopUp myMessageBoxPopUp = new MyMessageBoxPopUp();
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
      UniversalDebugLog.Log("Failed");
    }

    public void CardPlayed(ICardsProvider provider, ICardsResponse response)
    {
      if (!response.SuccessCode.HasValue || response.SuccessCode.Value != 1)
      {
        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_failed");
        int num1 = (int) MyMessageBox.Show(CardsManager.translateCardError(response.Message, this.lastRequestCard.Definition.id), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
        try
        {
          GameEngine.Instance.cardsManager.addProfileCard(this.lastRequestUserID, CardTypes.getStringFromCard(this.lastRequestCard.Definition.id));
          this.GetCardsAvailable(true);
          this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
          this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
        }
        catch (Exception ex)
        {
          int num2 = (int) MyMessageBox.Show(ex.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
        }
        if (InterfaceMgr.Instance.getCardWindow() != null)
          CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.getCardWindow());
      }
      else
      {
        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_success");
        GameEngine.Instance.cardsManager.ProfileCardsSet.Remove(this.lastRequestUserID);
        GameEngine.Instance.cardsManager.addRecentCard(this.lastRequestCard.Definition.id);
        if (this.lastRequestCard.UserIDList.Count > 0)
          this.lastRequestCard.UserID = this.lastRequestCard.UserIDList[0];
        if (CardTypes.getBasicUniqueCardType(this.lastRequestCard.Definition.id) != -1)
        {
          PlayCardsPanel.disableCardsInPlay(CardTypes.getBasicUniqueCardType(this.lastRequestCard.Definition.id), this.UICardList);
          this.AvailablePanelContent.invalidate();
        }
        GameEngine.Instance.cardsManager.CardPlayed(this.lastRequestCard.Definition.cardCategory, this.lastRequestCard.Definition.id, this.selectedVillage);
        StatTrackingClient.Instance().ActivateTrigger(15, (object) this.usingRecentFilter);
                StatTrackingClient.Instance().ActivateTrigger(17, (object)(((PlayCardsWindow)this.ParentForm).getNameSearchText().Length <= 0 ? 0 : (this.clearSearchButton.Visible ? 1 : 0)));
                StatTrackingClient.Instance().ActivateTrigger(6, (object) this.lastRequestCard.Definition.id);
        if (GameEngine.Instance.World.getTutorialStage() == 8 || GameEngine.Instance.World.getTutorialStage() == 12)
        {
          InterfaceMgr.Instance.closePlayCardsWindow();
          InterfaceMgr.Instance.ParentForm.TopMost = true;
          InterfaceMgr.Instance.ParentForm.TopMost = false;
        }
      }
      this.waitingResponse = false;
      if (this.usingRecentFilter)
        this.labelTitle.Text = SK.Text("CardPanel_Recent", "Recently Played") + " : " + GameEngine.Instance.cardsManager.countCardsInCategory(1048576).ToString();
      else
        this.labelTitle.Text = this.sectionName + " : " + GameEngine.Instance.cardsManager.ProfileCardsSearch.Count.ToString();
    }

    private void cardMouseOver()
    {
      if (this.OverControl.GetType() != typeof (UICard))
        return;
      UICard overControl = (UICard) this.OverControl;
      overControl.Hilight(ARGBColors.White);
      this.LastMouseoverCard = overControl;
    }

    private void cardMouseLeave()
    {
      if (this.LastMouseoverCard == null)
        return;
      this.LastMouseoverCard.Hilight(ARGBColors.LightGray);
    }

    private void showAllCardsClick()
    {
      ((PlayCardsWindow) this.ParentForm).SetCardSection(0);
      this.init(0);
      this.Invalidate();
    }

    private void showCardsInPlay() => ((PlayCardsWindow) this.ParentForm).SwitchPanel(8);

    public void update()
    {
      int num = (int) ((DateTime.Now - this.diamondAnimStartTime).TotalMilliseconds / 33.0);
      foreach (UICard uiCard in this.UICardList)
      {
        if (this.diamondAnimFrame != num)
        {
          BaseImage baseImage = (BaseImage) null;
          if (uiCard.Definition.cardGrade == 524288)
            baseImage = GFXLibrary.card_diamond_anim[num / 1 % GFXLibrary.card_diamond_anim.Length];
          else if (uiCard.Definition.cardGrade == 2097152)
            baseImage = GFXLibrary.card_diamond3_anim[num / 1 % GFXLibrary.card_diamond3_anim.Length];
          else if (uiCard.Definition.cardGrade == 1048576)
            baseImage = GFXLibrary.card_diamond2_anim[num / 1 % GFXLibrary.card_diamond2_anim.Length];
          else if (uiCard.Definition.cardGrade == 262144)
            baseImage = GFXLibrary.card_gold_anim[num / 1 % GFXLibrary.card_gold_anim.Length];
          else if (uiCard.Definition.cardGrade == 4194304)
            baseImage = GFXLibrary.card_sapphire_anim[num / 1 % GFXLibrary.card_sapphire_anim.Length];
          if (baseImage != null)
          {
            uiCard.bigGradeImage.Image = (Image) baseImage;
            uiCard.bigGradeImage.invalidateXtra();
          }
        }
      }
      this.diamondAnimFrame = num;
    }

    private void UpdateAlpha()
    {
      if ((double) this.bigCardAlpha == (double) this.bigCardAlphaTarget)
        return;
      if ((double) this.bigCardAlpha < (double) this.bigCardAlphaTarget)
      {
        this.bigCardAlpha += PlayCardsPanel.fadeStep;
        if ((double) this.bigCardAlpha > (double) this.bigCardAlphaTarget)
          this.bigCardAlpha = this.bigCardAlphaTarget;
      }
      else
      {
        this.bigCardAlpha -= PlayCardsPanel.fadeStep;
        if ((double) this.bigCardAlpha < (double) this.bigCardAlphaTarget)
          this.bigCardAlpha = this.bigCardAlphaTarget;
      }
      this.SetBigCardAlpha(this.bigCardAlpha);
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.closePlayCardsWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    private void SetBigCardAlpha(float alpha)
    {
    }

    private void ShowBigCard(UICard card)
    {
    }

    private void HideBigCard()
    {
    }

    public void navigateTest() => this.Navigate(2);

    private void Navigate(int panelType)
    {
      ((PlayCardsWindow) this.ParentForm).SwitchPanel(panelType);
    }

    private void sortByNameClicked()
    {
      this.sortByMode = this.sortByMode == 0 ? 2 : 0;
      this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
      this.sortByName.Alpha = 0.5f;
      this.sortByType.Alpha = 1f;
      this.sortByQuantity.Alpha = 0.5f;
    }

    private void sortByTypeClicked()
    {
      this.sortByMode = this.sortByMode == 1 ? 3 : 1;
      this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
      this.sortByName.Alpha = 1f;
      this.sortByType.Alpha = 0.5f;
      this.sortByQuantity.Alpha = 0.5f;
    }

    private void sortByQuantityClicked()
    {
      this.sortByMode = this.sortByMode == 7 ? 8 : 7;
      this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
      this.sortByName.Alpha = 0.5f;
      this.sortByType.Alpha = 0.5f;
      this.sortByQuantity.Alpha = 1f;
    }

    private void sortByRarityClicked()
    {
    }

    private void compressClicked()
    {
      if (this.compressedCards)
        return;
      this.compressedCards = true;
      this.scrollbarAvailable.Value = 0;
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
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
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500));
      this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
      this.AvailableContentScroll();
      this.Invalidate();
    }

    private void GetCardsRecent()
    {
      this.UICardList.Clear();
      List<UICard> uiCardList = new List<UICard>();
      int playerRank = GameEngine.Instance.World.getRank() + 1;
      foreach (CardTypes.CardDefinition card in CardTypes.cardList)
      {
        if (GameEngine.Instance.cardsManager.recentCards.Contains(card.id))
        {
          List<int> userid = new List<int>();
          foreach (int key in GameEngine.Instance.cardsManager.ProfileCards.Keys)
          {
            if (GameEngine.Instance.cardsManager.ProfileCards[key].id == card.id)
              userid.Add(key);
          }
          uiCardList.Add(this.makeUICard(card, userid, playerRank));
        }
      }
      foreach (int recentCard in GameEngine.Instance.cardsManager.recentCards)
      {
        foreach (UICard uiCard in uiCardList)
        {
          if (uiCard.Definition.id == recentCard)
            this.UICardList.Add(uiCard);
        }
      }
    }

    private UICard makeUICard(CardTypes.CardDefinition def, List<int> userid, int playerRank)
    {
      UICard uiCard = new UICard()
      {
        cardCount = userid.Count
      };
      uiCard.UserID = uiCard.cardCount <= 0 ? -1 : userid[0];
      foreach (int num in userid)
        uiCard.UserIDList.Add(num);
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
      control1.Text = uiCard.cardCount <= 1 ? "" : uiCard.cardCount.ToString();
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
      control2.Text = "";
      control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      control2.Color = color;
      control2.DropShadowColor = ARGBColors.Black;
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) control2);
      uiCard.rankLabel = control2;
      if (def.cardPoints > 0)
      {
        CustomSelfDrawPanel.CSDLabel control3 = new CustomSelfDrawPanel.CSDLabel();
        control3.Text = SK.Text("CARDS_GetCard", "Get Card");
        control3.Position = new Point(0, 0);
        control3.Size = new Size(157, 217);
        control3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        control3.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        control3.Color = ARGBColors.White;
        control3.Data = def.id;
        control3.Visible = uiCard.cardCount == 0;
        uiCard.buyCardsLabel = control3;
        uiCard.addControl((CustomSelfDrawPanel.CSDControl) control3);
        if (uiCard.cardCount == 0)
          uiCard.buyCardsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.linkToBuy));
      }
      if (uiCard.cardCount == 0)
      {
        uiCard.Hilight(ARGBColors.Gray);
      }
      else
      {
        uiCard.Hilight(ARGBColors.White);
        uiCard.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardClickPlay));
      }
      uiCard.ScaleAll(0.95);
      return uiCard;
    }

    public static void disableCardsInPlay(List<UICard> cardList)
    {
      CardData userCardData = GameEngine.Instance.cardsManager.UserCardData;
      List<int> intList = new List<int>();
      int length = userCardData.cards.Length;
      for (int index = 0; index < length; ++index)
      {
        int basicUniqueCardType = CardTypes.getBasicUniqueCardType(CardTypes.getCardType(userCardData.cards[index]));
        if (!intList.Contains(basicUniqueCardType) && basicUniqueCardType != -1)
          intList.Add(basicUniqueCardType);
      }
      foreach (UICard card in cardList)
      {
        if (card.Enabled && intList.Contains(CardTypes.getBasicUniqueCardType(card.Definition.id)))
          card.Hilight(ARGBColors.Gray);
      }
    }

    public static void disableCardsInPlay(int basicType, List<UICard> cardList)
    {
      foreach (UICard card in cardList)
      {
        if (card.Enabled && basicType == CardTypes.getBasicUniqueCardType(card.Definition.id))
          card.Hilight(ARGBColors.Gray);
      }
    }
  }
}
