// Decompiled with JetBrains decompiler
// Type: Kingdoms.StockExchangePanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using StatTracking;
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
  public class StockExchangePanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int CLOSEST_SEARCH_NUMBER = 20;
    private DockableControl dockableControl;
    private IContainer components;
    public static StockExchangePanel instance = (StockExchangePanel) null;
    private static List<WorldMap.VillageNameItem> exchangeHistory = new List<WorldMap.VillageNameItem>();
    private static List<WorldMap.VillageNameItem> exchangeFavourites = new List<WorldMap.VillageNameItem>();
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage stockExchangeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newTradingButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel leftWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel midWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel infoWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel buyHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage buyHeadingImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDExtendingPanel buyWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel buySubWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel buyNumber = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel buyCostLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel buyCostValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel buyMin = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel buyMax = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton buyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDTrackBar buyTrack = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDLabel buyTaxLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel sellHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage sellHeadingImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDExtendingPanel sellWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel sellSubWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel sellNumber = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel sellCostLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel sellCostValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel sellMin = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel sellMax = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton sellButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDTrackBar sellTrack = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDLabel localHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel storedHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel BuyPriceHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDExtendingPanel lightArea1 = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel lightArea2 = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel lightArea3 = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel exchangeNameBar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel exchangeNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton exchangeArrowButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel deliveryTimeArea = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel deliveryTimeAreaLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton tabButton1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tabButton2 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tabButton3 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton tabButton4 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel fourthAgeMessage = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage highlightLine1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage highlightLine2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage highlightLine3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage highlightLine4 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage highlightLine5 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage highlightLine6 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage highlightLine7 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage highlightLine8 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton selectRow1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton selectRow2 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton selectRow3 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton selectRow4 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton selectRow5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton selectRow6 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton selectRow7 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton selectRow8 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel localLabel1 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localLabel2 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localLabel3 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localLabel4 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localLabel5 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localLabel6 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localLabel7 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel localLabel8 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel storedLabel1 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel storedLabel2 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel storedLabel3 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel storedLabel4 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel storedLabel5 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel storedLabel6 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel storedLabel7 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel storedLabel8 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel priceLabel1 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel priceLabel2 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel priceLabel3 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel priceLabel4 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel priceLabel5 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel priceLabel6 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel priceLabel7 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel priceLabel8 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton lowestPriceRow1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton lowestPriceRow2 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton lowestPriceRow3 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton lowestPriceRow4 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton lowestPriceRow5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton lowestPriceRow6 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton lowestPriceRow7 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton lowestPriceRow8 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton highestPriceRow1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton highestPriceRow2 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton highestPriceRow3 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton highestPriceRow4 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton highestPriceRow5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton highestPriceRow6 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton highestPriceRow7 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton highestPriceRow8 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel traderCapacityLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel traderCapacityValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel tradersAvailableLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel tradersAvailableValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage traderIconImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage villageSelectPanel = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage villageSelectPanelHeader = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel villageSelectPanelLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage2 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage3 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage4 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage6 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage7 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage8 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage9 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage10 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage11 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage12 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage13 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage14 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage15 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage16 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage17 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage1Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage2Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage3Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage4Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage5Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage6Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage7Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage8Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage9Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage10Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage11Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage12Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage13Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage14Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage15Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage16Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage17Delete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage1Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage2Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage3Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage4Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage5Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage6Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage7Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage8Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage9Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage10Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage11Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage12Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage13Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage14Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage15Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage16Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton villageSelectVillage17Favourite = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton worldMapButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDCheckBox advancedOptions = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDExtendingPanel noResearchWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel noResearchText = new CustomSelfDrawPanel.CSDLabel();
    private CardBarGDI cardbar = new CardBarGDI();
    private bool lastPremiumType;
    private CustomSelfDrawPanel.CSDImage seaConditionsImage = new CustomSelfDrawPanel.CSDImage();
    private int lastSeaConditions = -1;
    private int selectedStockExchange = -1;
    private int lastTab = -1;
    private int currentResource = -1;
    private int currentResourcePacketSize = 1;
    private int currentResourcePacketSizeREAL = 1;
    private int BACKUP_resource = -1;
    private int BACKUP_sellLevel;
    private int BACKUP_buyLevel;
    private int lastHighlightResource = -1;
    private int numTraders;
    private int numFreeTraders;
    private List<int> closeCapitalsToTest = new List<int>();
    private DateTime lastTradeTime = DateTime.MinValue;
    public SparseArray stockExchanges = new SparseArray();

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
      this.Name = nameof (StockExchangePanel);
      this.MaximumSize = new Size(992, 566);
      this.MinimumSize = new Size(992, 566);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public static List<WorldMap.VillageNameItem> ExchangeHistory
    {
      get => StockExchangePanel.exchangeHistory;
    }

    public static List<WorldMap.VillageNameItem> ExchangeFavourites
    {
      get => StockExchangePanel.exchangeFavourites;
    }

    public static void addHistory(List<GenericVillageHistoryData> newData)
    {
      StockExchangePanel.exchangeHistory.Clear();
      if (newData == null)
        return;
      foreach (GenericVillageHistoryData villageHistoryData in newData)
      {
        WorldMap.VillageNameItem villageNameItem = new WorldMap.VillageNameItem();
        if (GameEngine.Instance.World.isCapital(villageHistoryData.villageID))
        {
          villageNameItem.villageID = villageHistoryData.villageID;
          StockExchangePanel.exchangeHistory.Add(villageNameItem);
        }
      }
    }

    public static void addFavourites(List<GenericVillageHistoryData> newData)
    {
      StockExchangePanel.exchangeFavourites.Clear();
      if (newData == null)
        return;
      foreach (GenericVillageHistoryData villageHistoryData in newData)
      {
        WorldMap.VillageNameItem villageNameItem = new WorldMap.VillageNameItem();
        if (GameEngine.Instance.World.isCapital(villageHistoryData.villageID))
        {
          villageNameItem.villageID = villageHistoryData.villageID;
          StockExchangePanel.exchangeFavourites.Add(villageNameItem);
        }
      }
    }

    public StockExchangePanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init()
    {
      this.lastPremiumType = GameEngine.Instance.World.isAccountPremium();
      StockExchangePanel.instance = this;
      this.clearControls();
      this.lastSeaConditions = -1;
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.mainBackgroundArea.Position = new Point(0, 0);
      this.mainBackgroundArea.Size = new Size(992, 566);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("TRADE_Stock_Exchange", "Stock Exchange"));
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(948, 10);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "StockExchangePanel_close");
      this.closeButton.CustomTooltipID = 800;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea, 4, new Point(898, 10));
      this.midWindow.Size = new Size(293, 449);
      this.midWindow.Position = new Point(349, 74);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.midWindow);
      this.midWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
      this.leftWindow.Size = new Size(335, 449);
      this.leftWindow.Position = new Point(19, 74);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.leftWindow);
      this.leftWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
      this.newTradingButton.ImageNorm = (Image) GFXLibrary.se_tabs[2];
      this.newTradingButton.ImageOver = (Image) GFXLibrary.se_tabs[3];
      this.newTradingButton.ImageClick = (Image) GFXLibrary.se_tabs[3];
      this.newTradingButton.Position = new Point(20, -17);
      this.newTradingButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tradingClick), "StockExchangePanel_trading");
      this.newTradingButton.ClickArea = new Rectangle(0, 0, 95, 25);
      this.newTradingButton.CustomTooltipID = 807;
      this.midWindow.addControl((CustomSelfDrawPanel.CSDControl) this.newTradingButton);
      this.lightArea1.Size = new Size(97, 329);
      this.lightArea1.Position = new Point(216, 102);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.lightArea1);
      this.lightArea1.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
      this.localHeadingLabel.Text = SK.Text("TRADE_Local", "Local");
      this.localHeadingLabel.Color = Color.FromArgb(196, 161, 85);
      this.localHeadingLabel.Position = new Point(0, -35);
      this.localHeadingLabel.Size = new Size(97, 30);
      this.localHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.localHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
      this.lightArea1.addControl((CustomSelfDrawPanel.CSDControl) this.localHeadingLabel);
      this.lightArea2.Size = new Size(97, 329);
      this.lightArea2.Position = new Point(21, 102);
      this.midWindow.addControl((CustomSelfDrawPanel.CSDControl) this.lightArea2);
      this.lightArea2.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
      this.storedHeadingLabel.Text = SK.Text("TRADE_At_Exchange", "At Exchange");
      this.storedHeadingLabel.Color = Color.FromArgb(196, 161, 85);
      this.storedHeadingLabel.Position = new Point(0, -35);
      this.storedHeadingLabel.Size = new Size(97, 30);
      this.storedHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.storedHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
      this.lightArea2.addControl((CustomSelfDrawPanel.CSDControl) this.storedHeadingLabel);
      this.lightArea3.Size = new Size(77, 329);
      this.lightArea3.Position = new Point(129, 102);
      this.midWindow.addControl((CustomSelfDrawPanel.CSDControl) this.lightArea3);
      this.lightArea3.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
      this.BuyPriceHeadingLabel.Text = SK.Text("TRADE_Price", "Price");
      this.BuyPriceHeadingLabel.Color = Color.FromArgb(196, 161, 85);
      this.BuyPriceHeadingLabel.Position = new Point(0, -35);
      this.BuyPriceHeadingLabel.Size = new Size(77, 30);
      this.BuyPriceHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.BuyPriceHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
      this.lightArea3.addControl((CustomSelfDrawPanel.CSDControl) this.BuyPriceHeadingLabel);
      this.exchangeNameBar.Size = new Size(270, 31);
      this.exchangeNameBar.Position = new Point(11, 9);
      this.exchangeNameBar.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exchangeArrowClick));
      this.midWindow.addControl((CustomSelfDrawPanel.CSDControl) this.exchangeNameBar);
      this.exchangeNameBar.Create((Image) GFXLibrary.int_lineitem_inset_left, (Image) GFXLibrary.int_lineitem_inset_middle, (Image) GFXLibrary.int_lineitem_inset_right);
      this.exchangeNameLabel.Text = SK.Text("TRADE_Selected_Exchange", "Select Exchange");
      this.exchangeNameLabel.Color = Color.FromArgb(196, 161, 85);
      this.exchangeNameLabel.Position = new Point(17, 7);
      this.exchangeNameLabel.Size = new Size(this.exchangeNameBar.Size.Width - 17 - 20, this.exchangeNameBar.Size.Height - 13);
      this.exchangeNameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.exchangeNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.exchangeNameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exchangeArrowClick));
      this.exchangeNameBar.addControl((CustomSelfDrawPanel.CSDControl) this.exchangeNameLabel);
      this.exchangeArrowButton.ImageNorm = (Image) GFXLibrary.int_button_droparrow_normal;
      this.exchangeArrowButton.ImageOver = (Image) GFXLibrary.int_button_droparrow_over;
      this.exchangeArrowButton.ImageClick = (Image) GFXLibrary.int_button_droparrow_down;
      this.exchangeArrowButton.Position = new Point(246, 7);
      this.exchangeArrowButton.MoveOnClick = false;
      this.exchangeArrowButton.Data = 0;
      this.exchangeArrowButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exchangeArrowClick));
      this.exchangeNameBar.addControl((CustomSelfDrawPanel.CSDControl) this.exchangeArrowButton);
      this.deliveryTimeArea.Size = new Size(258, 32);
      this.deliveryTimeArea.Position = new Point(16, 43);
      this.midWindow.addControl((CustomSelfDrawPanel.CSDControl) this.deliveryTimeArea);
      this.deliveryTimeArea.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
      this.deliveryTimeAreaLabel.Text = SK.Text("TRADE_Delivery_Time", "Delivery Time") + ":   88m 44s";
      this.deliveryTimeAreaLabel.Color = ARGBColors.Black;
      this.deliveryTimeAreaLabel.Position = new Point(0, 0);
      this.deliveryTimeAreaLabel.Size = this.deliveryTimeArea.Size;
      this.deliveryTimeAreaLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.deliveryTimeAreaLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.deliveryTimeArea.addControl((CustomSelfDrawPanel.CSDControl) this.deliveryTimeAreaLabel);
      this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_normal;
      this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_over;
      this.tabButton1.Position = new Point(2, -13);
      this.tabButton1.MoveOnClick = false;
      this.tabButton1.Data = 1;
      this.tabButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "StockExchangePanel_resource_tab");
      this.tabButton1.CustomTooltipID = 802;
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.tabButton1);
      this.tabButton2.ImageNorm = (Image) GFXLibrary.int_storage_tab_02_normal;
      this.tabButton2.ImageOver = (Image) GFXLibrary.int_storage_tab_02_over;
      this.tabButton2.Position = new Point(83, -13);
      this.tabButton2.MoveOnClick = false;
      this.tabButton2.Data = 2;
      this.tabButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "StockExchangePanel_food_tab");
      this.tabButton2.CustomTooltipID = 803;
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.tabButton2);
      this.tabButton3.ImageNorm = (Image) GFXLibrary.int_storage_tab_03_normal;
      this.tabButton3.ImageOver = (Image) GFXLibrary.int_storage_tab_03_over;
      this.tabButton3.Position = new Point(161, -13);
      this.tabButton3.MoveOnClick = false;
      this.tabButton3.Data = 3;
      this.tabButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "StockExchangePanel_weapons_tab");
      this.tabButton3.CustomTooltipID = 804;
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.tabButton3);
      this.tabButton4.ImageNorm = (Image) GFXLibrary.int_storage_tab_04_normal;
      this.tabButton4.ImageOver = (Image) GFXLibrary.int_storage_tab_04_over;
      this.tabButton4.Position = new Point(239, -13);
      this.tabButton4.MoveOnClick = false;
      this.tabButton4.Data = 4;
      this.tabButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "StockExchangePanel_banquetting_tab");
      this.tabButton4.CustomTooltipID = 805;
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.tabButton4);
      this.buyWindow.Size = new Size(336, 145);
      this.buyWindow.Position = new Point(637, 74);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.buyWindow);
      this.buyWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
      this.buySubWindow.Size = new Size(147, 50);
      this.buySubWindow.Position = new Point(178, 32);
      this.buyWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buySubWindow);
      this.buySubWindow.Create((Image) GFXLibrary.int_insetpanel_b_top_left, (Image) GFXLibrary.int_insetpanel_b_middle_top, (Image) GFXLibrary.int_insetpanel_b_top_right, (Image) GFXLibrary.int_insetpanel_b_middle_left, (Image) GFXLibrary.int_insetpanel_b_middle, (Image) GFXLibrary.int_insetpanel_b_middle_right, (Image) GFXLibrary.int_insetpanel_b_bottom_left, (Image) GFXLibrary.int_insetpanel_b_middle_bottom, (Image) GFXLibrary.int_insetpanel_b_bottom_right);
      this.buyHeadingLabel.Text = SK.Text("TRADE_Buy", "Buy") + " ";
      this.buyHeadingLabel.Color = ARGBColors.Black;
      this.buyHeadingLabel.Position = new Point(90, -30);
      this.buyHeadingLabel.Size = new Size(246, 30);
      this.buyHeadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.buyHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
      this.buyWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buyHeadingLabel);
      this.buyHeadingImage.Image = (Image) null;
      this.buyHeadingImage.Position = new Point(5, -50);
      this.buyWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buyHeadingImage);
      this.buyTaxLabel.Text = SK.Text("CapitalTradePanel_25_Tax", "+25% Tax");
      this.buyTaxLabel.Color = Color.FromArgb(196, 161, 85);
      this.buyTaxLabel.Position = new Point(21, 108);
      this.buyTaxLabel.Size = new Size(74, 30);
      this.buyTaxLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.buyTaxLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.buyWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buyTaxLabel);
      this.buyCostLabel.Text = SK.Text("CapitalTradePanel_Cost", "Cost") + ":";
      this.buyCostLabel.Color = Color.FromArgb(196, 161, 85);
      this.buyCostLabel.Position = new Point(0, 13);
      this.buyCostLabel.Size = new Size(74, 30);
      this.buyCostLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.buyCostLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.buySubWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buyCostLabel);
      this.buyNumber.Text = "0";
      this.buyNumber.Color = Color.FromArgb(196, 161, 85);
      this.buyNumber.Position = new Point(63, -4);
      this.buyNumber.Size = new Size(70, 30);
      this.buyNumber.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.buyNumber.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.buySubWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buyNumber);
      this.buyCostValue.Text = "0";
      this.buyCostValue.Color = Color.FromArgb(196, 161, 85);
      this.buyCostValue.Position = new Point(63, 13);
      this.buyCostValue.Size = new Size(70, 30);
      this.buyCostValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.buyCostValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.buySubWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buyCostValue);
      this.buyButton.Position = new Point(177, 94);
      this.buyButton.Size = new Size(153, 38);
      this.buyButton.Text.Text = SK.Text("CapitalTradePanel_Buy", "Buy");
      this.buyButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.buyButton.TextYOffset = -1;
      this.buyButton.Text.Color = ARGBColors.Black;
      this.buyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buyClick), "StockExchangePanel_buy");
      this.buyWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buyButton);
      this.buyButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.buyButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.buyTrack.Position = new Point(21, 41);
      this.buyTrack.Margin = new Rectangle(3, -1, 1, 0);
      this.buyTrack.Value = 0;
      this.buyTrack.Max = 1;
      this.buyTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
      this.buyWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buyTrack);
      this.buyTrack.Create((Image) GFXLibrary.int_slidebar_ruler, (Image) GFXLibrary.int_slidebar_thumb_middle_normal, (Image) GFXLibrary.int_slidebar_thumb_left_normal, (Image) GFXLibrary.int_slidebar_thumb_right_normal, (Image) GFXLibrary.int_slidebar_thumb_middle_in, (Image) GFXLibrary.int_slidebar_thumb_middle_over);
      this.buyMin.Text = "0";
      this.buyMin.Color = ARGBColors.Black;
      this.buyMin.Position = new Point(-2, 74);
      this.buyMin.Size = new Size(50, 30);
      this.buyMin.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      this.buyMin.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.buyWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buyMin);
      this.buyMax.Text = "0";
      this.buyMax.Color = ARGBColors.Black;
      this.buyMax.Position = new Point(126, 74);
      this.buyMax.Size = new Size(50, 30);
      this.buyMax.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      this.buyMax.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.buyWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buyMax);
      this.sellWindow.Size = new Size(336, 145);
      this.sellWindow.Position = new Point(637, 272);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.sellWindow);
      this.sellWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
      this.sellSubWindow.Size = new Size(147, 50);
      this.sellSubWindow.Position = new Point(178, 32);
      this.sellWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sellSubWindow);
      this.sellSubWindow.Create((Image) GFXLibrary.int_insetpanel_b_top_left, (Image) GFXLibrary.int_insetpanel_b_middle_top, (Image) GFXLibrary.int_insetpanel_b_top_right, (Image) GFXLibrary.int_insetpanel_b_middle_left, (Image) GFXLibrary.int_insetpanel_b_middle, (Image) GFXLibrary.int_insetpanel_b_middle_right, (Image) GFXLibrary.int_insetpanel_b_bottom_left, (Image) GFXLibrary.int_insetpanel_b_middle_bottom, (Image) GFXLibrary.int_insetpanel_b_bottom_right);
      this.sellHeadingLabel.Text = SK.Text("CapitalTradePanel_Sell", "Sell") + " ";
      this.sellHeadingLabel.Color = ARGBColors.Black;
      this.sellHeadingLabel.Position = new Point(90, -30);
      this.sellHeadingLabel.Size = new Size(246, 30);
      this.sellHeadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.sellHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
      this.sellWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sellHeadingLabel);
      this.sellHeadingImage.Image = (Image) null;
      this.sellHeadingImage.Position = new Point(5, -50);
      this.sellWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sellHeadingImage);
      this.sellCostLabel.Text = SK.Text("TRADE_Income", "Income") + ":";
      this.sellCostLabel.Color = Color.FromArgb(196, 161, 85);
      this.sellCostLabel.Position = new Point(-10, 13);
      this.sellCostLabel.Size = new Size(84, 30);
      this.sellCostLabel.Font = !(Program.mySettings.LanguageIdent == "de") ? FontManager.GetFont("Arial", 10f, FontStyle.Bold) : FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.sellCostLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.sellSubWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sellCostLabel);
      this.sellNumber.Text = "0";
      this.sellNumber.Color = Color.FromArgb(196, 161, 85);
      this.sellNumber.Position = new Point(63, -4);
      this.sellNumber.Size = new Size(70, 30);
      this.sellNumber.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.sellNumber.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.sellSubWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sellNumber);
      this.sellCostValue.Text = "0";
      this.sellCostValue.Color = Color.FromArgb(196, 161, 85);
      this.sellCostValue.Position = new Point(63, 13);
      this.sellCostValue.Size = new Size(70, 30);
      this.sellCostValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.sellCostValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.sellSubWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sellCostValue);
      this.sellButton.Position = new Point(177, 94);
      this.sellButton.Size = new Size(153, 38);
      this.sellButton.Text.Text = SK.Text("CapitalTradePanel_Sell", "Sell");
      this.sellButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.sellButton.TextYOffset = -1;
      this.sellButton.Text.Color = ARGBColors.Black;
      this.sellButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sellClick), "StockExchangePanel_sell");
      this.sellWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sellButton);
      this.sellButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.sellButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.sellTrack.Position = new Point(21, 41);
      this.sellTrack.Margin = new Rectangle(3, -1, 1, 0);
      this.sellTrack.Value = 0;
      this.sellTrack.Max = 1;
      this.sellTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
      this.sellWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sellTrack);
      this.sellTrack.Create((Image) GFXLibrary.int_slidebar_ruler, (Image) GFXLibrary.int_slidebar_thumb_middle_normal, (Image) GFXLibrary.int_slidebar_thumb_left_normal, (Image) GFXLibrary.int_slidebar_thumb_right_normal, (Image) GFXLibrary.int_slidebar_thumb_middle_in, (Image) GFXLibrary.int_slidebar_thumb_middle_over);
      this.sellMin.Text = "0";
      this.sellMin.Color = ARGBColors.Black;
      this.sellMin.Position = new Point(-2, 74);
      this.sellMin.Size = new Size(50, 30);
      this.sellMin.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      this.sellMin.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.sellWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sellMin);
      this.sellMax.Text = "0";
      this.sellMax.Color = ARGBColors.Black;
      this.sellMax.Position = new Point(126, 74);
      this.sellMax.Size = new Size(50, 30);
      this.sellMax.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
      this.sellMax.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.sellWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sellMax);
      this.fourthAgeMessage.Text = SK.Text("TRADE_NO_WEAPONS_4TH_AGE", "Weapons cannot be bought or sold in this Age.");
      this.fourthAgeMessage.Color = Color.FromArgb(196, 161, 85);
      this.fourthAgeMessage.Position = new Point(16, 111);
      this.fourthAgeMessage.Size = new Size(300, 100);
      this.fourthAgeMessage.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.fourthAgeMessage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.fourthAgeMessage.Visible = false;
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.fourthAgeMessage);
      this.highlightLine1.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine1.Position = new Point(153, 111);
      this.highlightLine1.Size = new Size(465, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine1);
      this.highlightLine2.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine2.Position = new Point(153, 151);
      this.highlightLine2.Size = new Size(465, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine2);
      this.highlightLine3.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine3.Position = new Point(153, 191);
      this.highlightLine3.Size = new Size(465, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine3);
      this.highlightLine4.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine4.Position = new Point(153, 231);
      this.highlightLine4.Size = new Size(465, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine4);
      this.highlightLine5.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine5.Position = new Point(153, 271);
      this.highlightLine5.Size = new Size(465, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine5);
      this.highlightLine6.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine6.Position = new Point(153, 311);
      this.highlightLine6.Size = new Size(465, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine6);
      this.highlightLine7.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine7.Position = new Point(153, 351);
      this.highlightLine7.Size = new Size(465, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine7);
      this.highlightLine8.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine8.Position = new Point(153, 391);
      this.highlightLine8.Size = new Size(465, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine8);
      this.selectRow1.Position = new Point(-134, -3);
      this.selectRow1.Size = new Size(191, 38);
      this.selectRow1.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.selectRow1.Text.Position = new Point(91, 0);
      this.selectRow1.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.selectRow1.TextYOffset = -1;
      this.selectRow1.Text.Color = ARGBColors.Black;
      this.selectRow1.ImageIconPosition = new Point(46, -3);
      this.selectRow1.createSubText("0");
      this.selectRow1.Text2.Size = new Size(46, this.selectRow1.Text2.Size.Height);
      this.selectRow1.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.selectRow1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
      this.highlightLine1.addControl((CustomSelfDrawPanel.CSDControl) this.selectRow1);
      this.selectRow1.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.selectRow1.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.selectRow2.Position = new Point(-134, -3);
      this.selectRow2.Size = new Size(191, 38);
      this.selectRow2.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.selectRow2.Text.Position = new Point(91, 0);
      this.selectRow2.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.selectRow2.TextYOffset = -1;
      this.selectRow2.Text.Color = ARGBColors.Black;
      this.selectRow2.ImageIconPosition = new Point(46, -3);
      this.selectRow2.createSubText("0");
      this.selectRow2.Text2.Size = new Size(46, this.selectRow2.Text2.Size.Height);
      this.selectRow2.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.selectRow2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
      this.highlightLine2.addControl((CustomSelfDrawPanel.CSDControl) this.selectRow2);
      this.selectRow2.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.selectRow2.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.selectRow3.Position = new Point(-134, -3);
      this.selectRow3.Size = new Size(191, 38);
      this.selectRow3.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.selectRow3.Text.Position = new Point(91, 0);
      this.selectRow3.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.selectRow3.TextYOffset = -1;
      this.selectRow3.Text.Color = ARGBColors.Black;
      this.selectRow3.ImageIconPosition = new Point(46, -3);
      this.selectRow3.createSubText("0");
      this.selectRow3.Text2.Size = new Size(46, this.selectRow3.Text2.Size.Height);
      this.selectRow3.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.selectRow3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
      this.highlightLine3.addControl((CustomSelfDrawPanel.CSDControl) this.selectRow3);
      this.selectRow3.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.selectRow3.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.selectRow4.Position = new Point(-134, -3);
      this.selectRow4.Size = new Size(191, 38);
      this.selectRow4.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.selectRow4.Text.Position = new Point(91, 0);
      this.selectRow4.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.selectRow4.TextYOffset = -1;
      this.selectRow4.Text.Color = ARGBColors.Black;
      this.selectRow4.ImageIconPosition = new Point(46, -3);
      this.selectRow4.createSubText("0");
      this.selectRow4.Text2.Size = new Size(46, this.selectRow4.Text2.Size.Height);
      this.selectRow4.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.selectRow4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
      this.highlightLine4.addControl((CustomSelfDrawPanel.CSDControl) this.selectRow4);
      this.selectRow4.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.selectRow4.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.selectRow5.Position = new Point(-134, -3);
      this.selectRow5.Size = new Size(191, 38);
      this.selectRow5.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.selectRow5.Text.Position = new Point(91, 0);
      this.selectRow5.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.selectRow5.TextYOffset = -1;
      this.selectRow5.Text.Color = ARGBColors.Black;
      this.selectRow5.ImageIconPosition = new Point(46, -3);
      this.selectRow5.createSubText("0");
      this.selectRow5.Text2.Size = new Size(46, this.selectRow5.Text2.Size.Height);
      this.selectRow5.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.selectRow5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
      this.highlightLine5.addControl((CustomSelfDrawPanel.CSDControl) this.selectRow5);
      this.selectRow5.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.selectRow5.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.selectRow6.Position = new Point(-134, -3);
      this.selectRow6.Size = new Size(191, 38);
      this.selectRow6.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.selectRow6.Text.Position = new Point(91, 0);
      this.selectRow6.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.selectRow6.TextYOffset = -1;
      this.selectRow6.Text.Color = ARGBColors.Black;
      this.selectRow6.ImageIconPosition = new Point(46, -3);
      this.selectRow6.createSubText("0");
      this.selectRow6.Text2.Size = new Size(46, this.selectRow6.Text2.Size.Height);
      this.selectRow6.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.selectRow6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
      this.highlightLine6.addControl((CustomSelfDrawPanel.CSDControl) this.selectRow6);
      this.selectRow6.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.selectRow6.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.selectRow7.Position = new Point(-134, -3);
      this.selectRow7.Size = new Size(191, 38);
      this.selectRow7.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.selectRow7.Text.Position = new Point(91, 0);
      this.selectRow7.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.selectRow7.TextYOffset = -1;
      this.selectRow7.Text.Color = ARGBColors.Black;
      this.selectRow7.ImageIconPosition = new Point(46, -3);
      this.selectRow7.createSubText("0");
      this.selectRow7.Text2.Size = new Size(46, this.selectRow7.Text2.Size.Height);
      this.selectRow7.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.selectRow7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
      this.highlightLine7.addControl((CustomSelfDrawPanel.CSDControl) this.selectRow7);
      this.selectRow7.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.selectRow7.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.selectRow8.Position = new Point(-134, -3);
      this.selectRow8.Size = new Size(191, 38);
      this.selectRow8.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.selectRow8.Text.Position = new Point(91, 0);
      this.selectRow8.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.selectRow8.TextYOffset = -1;
      this.selectRow8.Text.Color = ARGBColors.Black;
      this.selectRow8.ImageIconPosition = new Point(46, -3);
      this.selectRow8.createSubText("0");
      this.selectRow8.Text2.Size = new Size(46, this.selectRow8.Text2.Size.Height);
      this.selectRow8.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.selectRow8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
      this.highlightLine8.addControl((CustomSelfDrawPanel.CSDControl) this.selectRow8);
      this.selectRow8.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.selectRow8.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.localLabel1.Text = "0";
      this.localLabel1.Color = ARGBColors.Black;
      this.localLabel1.Position = new Point(63, 1);
      this.localLabel1.Size = new Size(97, 31);
      this.localLabel1.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.localLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine1.addControl((CustomSelfDrawPanel.CSDControl) this.localLabel1);
      this.localLabel2.Text = "0";
      this.localLabel2.Color = ARGBColors.Black;
      this.localLabel2.Position = new Point(63, 1);
      this.localLabel2.Size = new Size(97, 31);
      this.localLabel2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.localLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine2.addControl((CustomSelfDrawPanel.CSDControl) this.localLabel2);
      this.localLabel3.Text = "0";
      this.localLabel3.Color = ARGBColors.Black;
      this.localLabel3.Position = new Point(63, 1);
      this.localLabel3.Size = new Size(97, 31);
      this.localLabel3.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.localLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine3.addControl((CustomSelfDrawPanel.CSDControl) this.localLabel3);
      this.localLabel4.Text = "0";
      this.localLabel4.Color = ARGBColors.Black;
      this.localLabel4.Position = new Point(63, 1);
      this.localLabel4.Size = new Size(97, 31);
      this.localLabel4.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.localLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine4.addControl((CustomSelfDrawPanel.CSDControl) this.localLabel4);
      this.localLabel5.Text = "0";
      this.localLabel5.Color = ARGBColors.Black;
      this.localLabel5.Position = new Point(63, 1);
      this.localLabel5.Size = new Size(97, 31);
      this.localLabel5.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.localLabel5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine5.addControl((CustomSelfDrawPanel.CSDControl) this.localLabel5);
      this.localLabel6.Text = "0";
      this.localLabel6.Color = ARGBColors.Black;
      this.localLabel6.Position = new Point(63, 1);
      this.localLabel6.Size = new Size(97, 31);
      this.localLabel6.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.localLabel6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine6.addControl((CustomSelfDrawPanel.CSDControl) this.localLabel6);
      this.localLabel7.Text = "0";
      this.localLabel7.Color = ARGBColors.Black;
      this.localLabel7.Position = new Point(63, 1);
      this.localLabel7.Size = new Size(97, 31);
      this.localLabel7.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.localLabel7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine7.addControl((CustomSelfDrawPanel.CSDControl) this.localLabel7);
      this.localLabel8.Text = "0";
      this.localLabel8.Color = ARGBColors.Black;
      this.localLabel8.Position = new Point(63, 1);
      this.localLabel8.Size = new Size(97, 31);
      this.localLabel8.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.localLabel8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine8.addControl((CustomSelfDrawPanel.CSDControl) this.localLabel8);
      this.storedLabel1.Text = "0";
      this.storedLabel1.Color = ARGBColors.Black;
      this.storedLabel1.Position = new Point(198, 1);
      this.storedLabel1.Size = new Size(97, 31);
      this.storedLabel1.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.storedLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine1.addControl((CustomSelfDrawPanel.CSDControl) this.storedLabel1);
      this.storedLabel2.Text = "0";
      this.storedLabel2.Color = ARGBColors.Black;
      this.storedLabel2.Position = new Point(198, 1);
      this.storedLabel2.Size = new Size(97, 31);
      this.storedLabel2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.storedLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine2.addControl((CustomSelfDrawPanel.CSDControl) this.storedLabel2);
      this.storedLabel3.Text = "0";
      this.storedLabel3.Color = ARGBColors.Black;
      this.storedLabel3.Position = new Point(198, 1);
      this.storedLabel3.Size = new Size(97, 31);
      this.storedLabel3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.storedLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine3.addControl((CustomSelfDrawPanel.CSDControl) this.storedLabel3);
      this.storedLabel4.Text = "0";
      this.storedLabel4.Color = ARGBColors.Black;
      this.storedLabel4.Position = new Point(198, 1);
      this.storedLabel4.Size = new Size(97, 31);
      this.storedLabel4.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.storedLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine4.addControl((CustomSelfDrawPanel.CSDControl) this.storedLabel4);
      this.storedLabel5.Text = "0";
      this.storedLabel5.Color = ARGBColors.Black;
      this.storedLabel5.Position = new Point(198, 1);
      this.storedLabel5.Size = new Size(97, 31);
      this.storedLabel5.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.storedLabel5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine5.addControl((CustomSelfDrawPanel.CSDControl) this.storedLabel5);
      this.storedLabel6.Text = "0";
      this.storedLabel6.Color = ARGBColors.Black;
      this.storedLabel6.Position = new Point(198, 1);
      this.storedLabel6.Size = new Size(97, 31);
      this.storedLabel6.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.storedLabel6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine6.addControl((CustomSelfDrawPanel.CSDControl) this.storedLabel6);
      this.storedLabel7.Text = "0";
      this.storedLabel7.Color = ARGBColors.Black;
      this.storedLabel7.Position = new Point(198, 1);
      this.storedLabel7.Size = new Size(97, 31);
      this.storedLabel7.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.storedLabel7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine7.addControl((CustomSelfDrawPanel.CSDControl) this.storedLabel7);
      this.storedLabel8.Text = "0";
      this.storedLabel8.Color = ARGBColors.Black;
      this.storedLabel8.Position = new Point(198, 1);
      this.storedLabel8.Size = new Size(97, 31);
      this.storedLabel8.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.storedLabel8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine8.addControl((CustomSelfDrawPanel.CSDControl) this.storedLabel8);
      this.priceLabel1.Text = "0";
      this.priceLabel1.Color = ARGBColors.Black;
      this.priceLabel1.Position = new Point(306, 1);
      this.priceLabel1.Size = new Size(77, 31);
      this.priceLabel1.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.priceLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine1.addControl((CustomSelfDrawPanel.CSDControl) this.priceLabel1);
      this.priceLabel2.Text = "0";
      this.priceLabel2.Color = ARGBColors.Black;
      this.priceLabel2.Position = new Point(306, 1);
      this.priceLabel2.Size = new Size(77, 31);
      this.priceLabel2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.priceLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine2.addControl((CustomSelfDrawPanel.CSDControl) this.priceLabel2);
      this.priceLabel3.Text = "0";
      this.priceLabel3.Color = ARGBColors.Black;
      this.priceLabel3.Position = new Point(306, 1);
      this.priceLabel3.Size = new Size(77, 31);
      this.priceLabel3.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.priceLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine3.addControl((CustomSelfDrawPanel.CSDControl) this.priceLabel3);
      this.priceLabel4.Text = "0";
      this.priceLabel4.Color = ARGBColors.Black;
      this.priceLabel4.Position = new Point(306, 1);
      this.priceLabel4.Size = new Size(77, 31);
      this.priceLabel4.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.priceLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine4.addControl((CustomSelfDrawPanel.CSDControl) this.priceLabel4);
      this.priceLabel5.Text = "0";
      this.priceLabel5.Color = ARGBColors.Black;
      this.priceLabel5.Position = new Point(306, 1);
      this.priceLabel5.Size = new Size(77, 31);
      this.priceLabel5.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.priceLabel5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine5.addControl((CustomSelfDrawPanel.CSDControl) this.priceLabel5);
      this.priceLabel6.Text = "0";
      this.priceLabel6.Color = ARGBColors.Black;
      this.priceLabel6.Position = new Point(306, 1);
      this.priceLabel6.Size = new Size(77, 31);
      this.priceLabel6.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.priceLabel6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine6.addControl((CustomSelfDrawPanel.CSDControl) this.priceLabel6);
      this.priceLabel7.Text = "0";
      this.priceLabel7.Color = ARGBColors.Black;
      this.priceLabel7.Position = new Point(306, 1);
      this.priceLabel7.Size = new Size(77, 31);
      this.priceLabel7.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.priceLabel7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine7.addControl((CustomSelfDrawPanel.CSDControl) this.priceLabel7);
      this.priceLabel8.Text = "0";
      this.priceLabel8.Color = ARGBColors.Black;
      this.priceLabel8.Position = new Point(306, 1);
      this.priceLabel8.Size = new Size(77, 31);
      this.priceLabel8.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.priceLabel8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.highlightLine8.addControl((CustomSelfDrawPanel.CSDControl) this.priceLabel8);
      this.highestPriceRow1.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
      this.highestPriceRow1.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
      this.highestPriceRow1.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
      this.highestPriceRow1.Position = new Point(389, -2);
      this.highestPriceRow1.Data = 0;
      this.highestPriceRow1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
      this.highestPriceRow1.CustomTooltipID = 814;
      this.highestPriceRow1.Active = GameEngine.Instance.World.isAccountPremium();
      this.highestPriceRow1.Alpha = this.highestPriceRow1.Active ? 1f : 0.5f;
      this.highlightLine1.addControl((CustomSelfDrawPanel.CSDControl) this.highestPriceRow1);
      this.highestPriceRow2.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
      this.highestPriceRow2.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
      this.highestPriceRow2.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
      this.highestPriceRow2.Position = new Point(389, -2);
      this.highestPriceRow2.Data = 1;
      this.highestPriceRow2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
      this.highestPriceRow2.CustomTooltipID = 814;
      this.highestPriceRow2.Active = GameEngine.Instance.World.isAccountPremium();
      this.highestPriceRow2.Alpha = this.highestPriceRow2.Active ? 1f : 0.5f;
      this.highlightLine2.addControl((CustomSelfDrawPanel.CSDControl) this.highestPriceRow2);
      this.highestPriceRow3.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
      this.highestPriceRow3.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
      this.highestPriceRow3.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
      this.highestPriceRow3.Position = new Point(389, -2);
      this.highestPriceRow3.Data = 2;
      this.highestPriceRow3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
      this.highestPriceRow3.CustomTooltipID = 814;
      this.highestPriceRow3.Active = GameEngine.Instance.World.isAccountPremium();
      this.highestPriceRow3.Alpha = this.highestPriceRow3.Active ? 1f : 0.5f;
      this.highlightLine3.addControl((CustomSelfDrawPanel.CSDControl) this.highestPriceRow3);
      this.highestPriceRow4.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
      this.highestPriceRow4.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
      this.highestPriceRow4.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
      this.highestPriceRow4.Position = new Point(389, -2);
      this.highestPriceRow4.Data = 3;
      this.highestPriceRow4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
      this.highestPriceRow4.CustomTooltipID = 814;
      this.highestPriceRow4.Active = GameEngine.Instance.World.isAccountPremium();
      this.highestPriceRow4.Alpha = this.highestPriceRow4.Active ? 1f : 0.5f;
      this.highlightLine4.addControl((CustomSelfDrawPanel.CSDControl) this.highestPriceRow4);
      this.highestPriceRow5.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
      this.highestPriceRow5.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
      this.highestPriceRow5.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
      this.highestPriceRow5.Position = new Point(389, -2);
      this.highestPriceRow5.Data = 4;
      this.highestPriceRow5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
      this.highestPriceRow5.CustomTooltipID = 814;
      this.highestPriceRow5.Active = GameEngine.Instance.World.isAccountPremium();
      this.highestPriceRow5.Alpha = this.highestPriceRow5.Active ? 1f : 0.5f;
      this.highlightLine5.addControl((CustomSelfDrawPanel.CSDControl) this.highestPriceRow5);
      this.highestPriceRow6.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
      this.highestPriceRow6.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
      this.highestPriceRow6.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
      this.highestPriceRow6.Position = new Point(389, -2);
      this.highestPriceRow6.Data = 5;
      this.highestPriceRow6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
      this.highestPriceRow6.CustomTooltipID = 814;
      this.highestPriceRow6.Active = GameEngine.Instance.World.isAccountPremium();
      this.highestPriceRow6.Alpha = this.highestPriceRow6.Active ? 1f : 0.5f;
      this.highlightLine6.addControl((CustomSelfDrawPanel.CSDControl) this.highestPriceRow6);
      this.highestPriceRow7.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
      this.highestPriceRow7.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
      this.highestPriceRow7.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
      this.highestPriceRow7.Position = new Point(389, -2);
      this.highestPriceRow7.Data = 6;
      this.highestPriceRow7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
      this.highestPriceRow7.CustomTooltipID = 814;
      this.highestPriceRow7.Active = GameEngine.Instance.World.isAccountPremium();
      this.highestPriceRow7.Alpha = this.highestPriceRow7.Active ? 1f : 0.5f;
      this.highlightLine7.addControl((CustomSelfDrawPanel.CSDControl) this.highestPriceRow7);
      this.highestPriceRow8.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
      this.highestPriceRow8.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
      this.highestPriceRow8.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
      this.highestPriceRow8.Position = new Point(389, -2);
      this.highestPriceRow8.Data = 7;
      this.highestPriceRow8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
      this.highestPriceRow8.CustomTooltipID = 814;
      this.highestPriceRow8.Active = GameEngine.Instance.World.isAccountPremium();
      this.highestPriceRow8.Alpha = this.highestPriceRow8.Active ? 1f : 0.5f;
      this.highlightLine8.addControl((CustomSelfDrawPanel.CSDControl) this.highestPriceRow8);
      this.lowestPriceRow1.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
      this.lowestPriceRow1.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
      this.lowestPriceRow1.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
      this.lowestPriceRow1.Position = new Point(425, -2);
      this.lowestPriceRow1.Data = 0;
      this.lowestPriceRow1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
      this.lowestPriceRow1.CustomTooltipID = 815;
      this.lowestPriceRow1.Active = GameEngine.Instance.World.isAccountPremium();
      this.lowestPriceRow1.Alpha = this.highestPriceRow1.Active ? 1f : 0.5f;
      this.highlightLine1.addControl((CustomSelfDrawPanel.CSDControl) this.lowestPriceRow1);
      this.lowestPriceRow2.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
      this.lowestPriceRow2.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
      this.lowestPriceRow2.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
      this.lowestPriceRow2.Position = new Point(425, -2);
      this.lowestPriceRow2.Data = 1;
      this.lowestPriceRow2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
      this.lowestPriceRow2.CustomTooltipID = 815;
      this.lowestPriceRow2.Active = GameEngine.Instance.World.isAccountPremium();
      this.lowestPriceRow2.Alpha = this.lowestPriceRow2.Active ? 1f : 0.5f;
      this.highlightLine2.addControl((CustomSelfDrawPanel.CSDControl) this.lowestPriceRow2);
      this.lowestPriceRow3.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
      this.lowestPriceRow3.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
      this.lowestPriceRow3.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
      this.lowestPriceRow3.Position = new Point(425, -2);
      this.lowestPriceRow3.Data = 2;
      this.lowestPriceRow3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
      this.lowestPriceRow3.CustomTooltipID = 815;
      this.lowestPriceRow3.Active = GameEngine.Instance.World.isAccountPremium();
      this.lowestPriceRow3.Alpha = this.lowestPriceRow3.Active ? 1f : 0.5f;
      this.highlightLine3.addControl((CustomSelfDrawPanel.CSDControl) this.lowestPriceRow3);
      this.lowestPriceRow4.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
      this.lowestPriceRow4.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
      this.lowestPriceRow4.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
      this.lowestPriceRow4.Position = new Point(425, -2);
      this.lowestPriceRow4.Data = 3;
      this.lowestPriceRow4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
      this.lowestPriceRow4.CustomTooltipID = 815;
      this.lowestPriceRow4.Active = GameEngine.Instance.World.isAccountPremium();
      this.lowestPriceRow4.Alpha = this.lowestPriceRow4.Active ? 1f : 0.5f;
      this.highlightLine4.addControl((CustomSelfDrawPanel.CSDControl) this.lowestPriceRow4);
      this.lowestPriceRow5.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
      this.lowestPriceRow5.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
      this.lowestPriceRow5.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
      this.lowestPriceRow5.Position = new Point(425, -2);
      this.lowestPriceRow5.Data = 4;
      this.lowestPriceRow5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
      this.lowestPriceRow5.CustomTooltipID = 815;
      this.lowestPriceRow5.Active = GameEngine.Instance.World.isAccountPremium();
      this.lowestPriceRow5.Alpha = this.lowestPriceRow5.Active ? 1f : 0.5f;
      this.highlightLine5.addControl((CustomSelfDrawPanel.CSDControl) this.lowestPriceRow5);
      this.lowestPriceRow6.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
      this.lowestPriceRow6.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
      this.lowestPriceRow6.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
      this.lowestPriceRow6.Position = new Point(425, -2);
      this.lowestPriceRow6.Data = 5;
      this.lowestPriceRow6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
      this.lowestPriceRow6.CustomTooltipID = 815;
      this.lowestPriceRow6.Active = GameEngine.Instance.World.isAccountPremium();
      this.lowestPriceRow6.Alpha = this.lowestPriceRow6.Active ? 1f : 0.5f;
      this.highlightLine6.addControl((CustomSelfDrawPanel.CSDControl) this.lowestPriceRow6);
      this.lowestPriceRow7.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
      this.lowestPriceRow7.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
      this.lowestPriceRow7.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
      this.lowestPriceRow7.Position = new Point(425, -2);
      this.lowestPriceRow7.Data = 6;
      this.lowestPriceRow7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
      this.lowestPriceRow7.CustomTooltipID = 815;
      this.lowestPriceRow7.Active = GameEngine.Instance.World.isAccountPremium();
      this.lowestPriceRow7.Alpha = this.lowestPriceRow7.Active ? 1f : 0.5f;
      this.highlightLine7.addControl((CustomSelfDrawPanel.CSDControl) this.lowestPriceRow7);
      this.lowestPriceRow8.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
      this.lowestPriceRow8.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
      this.lowestPriceRow8.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
      this.lowestPriceRow8.Position = new Point(425, -2);
      this.lowestPriceRow8.Data = 7;
      this.lowestPriceRow8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
      this.lowestPriceRow8.CustomTooltipID = 815;
      this.lowestPriceRow8.Active = GameEngine.Instance.World.isAccountPremium();
      this.lowestPriceRow8.Alpha = this.lowestPriceRow8.Active ? 1f : 0.5f;
      this.highlightLine8.addControl((CustomSelfDrawPanel.CSDControl) this.lowestPriceRow8);
      this.infoWindow.Size = new Size(336, 65);
      this.infoWindow.Position = new Point(637, 459);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.infoWindow);
      this.infoWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
      this.traderCapacityLabel.Text = SK.Text("MarketTradeScreen_Merchant_Capacity", "Merchant Capacity");
      this.traderCapacityLabel.Color = Color.FromArgb(196, 161, 85);
      this.traderCapacityLabel.Position = new Point(105, -1);
      this.traderCapacityLabel.Size = new Size(231, 30);
      this.traderCapacityLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.traderCapacityLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
      this.infoWindow.addControl((CustomSelfDrawPanel.CSDControl) this.traderCapacityLabel);
      this.traderCapacityValue.Text = "0";
      this.traderCapacityValue.Color = Color.FromArgb(196, 161, 85);
      this.traderCapacityValue.Position = new Point(232, -1);
      this.traderCapacityValue.Size = new Size(80, 30);
      this.traderCapacityValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.traderCapacityValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.infoWindow.addControl((CustomSelfDrawPanel.CSDControl) this.traderCapacityValue);
      this.tradersAvailableLabel.Text = SK.Text("MarketTradeScreen_Merchant_Available", "Merchants Available");
      this.tradersAvailableLabel.Color = Color.FromArgb(196, 161, 85);
      this.tradersAvailableLabel.Position = new Point(105, 18);
      this.tradersAvailableLabel.Size = new Size(231, 30);
      this.tradersAvailableLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.tradersAvailableLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
      this.infoWindow.addControl((CustomSelfDrawPanel.CSDControl) this.tradersAvailableLabel);
      this.tradersAvailableValue.Text = "0";
      this.tradersAvailableValue.Color = Color.FromArgb(196, 161, 85);
      this.tradersAvailableValue.Position = new Point(232, 18);
      this.tradersAvailableValue.Size = new Size(80, 30);
      this.tradersAvailableValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.tradersAvailableValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
      this.infoWindow.addControl((CustomSelfDrawPanel.CSDControl) this.tradersAvailableValue);
      this.traderIconImage.Image = (Image) GFXLibrary.int_icon_trader;
      this.traderIconImage.Position = new Point(16, -26);
      this.infoWindow.addControl((CustomSelfDrawPanel.CSDControl) this.traderIconImage);
      this.villageSelectPanel.Image = (Image) GFXLibrary.int_villagelist_panel;
      this.villageSelectPanel.Position = new Point(356, 109);
      this.villageSelectPanel.Visible = false;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectPanel);
      this.villageSelectPanelHeader.Image = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectPanelHeader.Position = new Point(3, 3);
      this.villageSelectPanelHeader.Size = new Size(this.villageSelectPanel.Width - 14, this.villageSelectPanelHeader.Image.Height);
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectPanelHeader);
      this.villageSelectPanelLabel.Text = SK.Text("MarketTradeScreen_Recent_Exchanges", "Recent Exchanges");
      this.villageSelectPanelLabel.Color = ARGBColors.Black;
      this.villageSelectPanelLabel.Position = new Point(5, -1);
      this.villageSelectPanelLabel.Size = new Size(this.villageSelectPanelHeader.Size.Width - 10, this.villageSelectPanelHeader.Size.Height);
      this.villageSelectPanelLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectPanelLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectPanelHeader.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectPanelLabel);
      this.villageSelectVillage1.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage1.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage1.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage1.ImageNorm = (Image) null;
      this.villageSelectVillage1.Position = new Point(20, 21);
      this.villageSelectVillage1.Text.Text = "Village 1";
      this.villageSelectVillage1.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage1.Text.Position = new Point(5, 0);
      this.villageSelectVillage1.Text.Size = new Size(this.villageSelectVillage1.Width - 10, this.villageSelectVillage1.Height);
      this.villageSelectVillage1.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage1.TextYOffset = 0;
      this.villageSelectVillage1.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage1.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage1);
      this.villageSelectVillage1Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage1Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage1Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage1Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage1Delete.Position = new Point((int) byte.MaxValue, 21);
      this.villageSelectVillage1Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage1Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage1Delete);
      this.villageSelectVillage1Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage1Favourite.OverBrighten = true;
      this.villageSelectVillage1Favourite.Position = new Point(1, 19);
      this.villageSelectVillage1Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage1Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage1Favourite);
      this.villageSelectVillage2.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage2.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage2.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage2.ImageNorm = (Image) null;
      this.villageSelectVillage2.Position = new Point(20, 39);
      this.villageSelectVillage2.Text.Text = "Village 2";
      this.villageSelectVillage2.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage2.Text.Position = new Point(5, 0);
      this.villageSelectVillage2.Text.Size = new Size(this.villageSelectVillage2.Width - 10, this.villageSelectVillage2.Height);
      this.villageSelectVillage2.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage2.TextYOffset = 0;
      this.villageSelectVillage2.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage2.Data = 1;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage2);
      this.villageSelectVillage2Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage2Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage2Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage2Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage2Delete.Position = new Point((int) byte.MaxValue, 39);
      this.villageSelectVillage2Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage2Delete.Data = 1;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage2Delete);
      this.villageSelectVillage2Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage2Favourite.OverBrighten = true;
      this.villageSelectVillage2Favourite.Position = new Point(1, 37);
      this.villageSelectVillage2Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage2Favourite.Data = 1;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage2Favourite);
      this.villageSelectVillage3.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage3.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage3.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage3.ImageNorm = (Image) null;
      this.villageSelectVillage3.Position = new Point(20, 57);
      this.villageSelectVillage3.Text.Text = "Village 3";
      this.villageSelectVillage3.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage3.Text.Position = new Point(5, 0);
      this.villageSelectVillage3.Text.Size = new Size(this.villageSelectVillage3.Width - 10, this.villageSelectVillage3.Height);
      this.villageSelectVillage3.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage3.TextYOffset = 0;
      this.villageSelectVillage3.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage3.Data = 2;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage3);
      this.villageSelectVillage3Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage3Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage3Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage3Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage3Delete.Position = new Point((int) byte.MaxValue, 57);
      this.villageSelectVillage3Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage3Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage3Delete);
      this.villageSelectVillage3Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage3Favourite.OverBrighten = true;
      this.villageSelectVillage3Favourite.Position = new Point(1, 55);
      this.villageSelectVillage3Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage3Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage3Favourite);
      this.villageSelectVillage4.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage4.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage4.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage4.ImageNorm = (Image) null;
      this.villageSelectVillage4.Position = new Point(20, 75);
      this.villageSelectVillage4.Text.Text = "Village 4";
      this.villageSelectVillage4.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage4.Text.Position = new Point(5, 0);
      this.villageSelectVillage4.Text.Size = new Size(this.villageSelectVillage4.Width - 10, this.villageSelectVillage4.Height);
      this.villageSelectVillage4.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage4.TextYOffset = 0;
      this.villageSelectVillage4.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage4.Data = 3;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage4);
      this.villageSelectVillage4Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage4Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage4Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage4Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage4Delete.Position = new Point((int) byte.MaxValue, 75);
      this.villageSelectVillage4Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage4Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage4Delete);
      this.villageSelectVillage4Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage4Favourite.OverBrighten = true;
      this.villageSelectVillage4Favourite.Position = new Point(1, 73);
      this.villageSelectVillage4Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage4Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage4Favourite);
      this.villageSelectVillage5.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage5.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage5.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage5.ImageNorm = (Image) null;
      this.villageSelectVillage5.Position = new Point(20, 93);
      this.villageSelectVillage5.Text.Text = "Village 5";
      this.villageSelectVillage5.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage5.Text.Position = new Point(5, 0);
      this.villageSelectVillage5.Text.Size = new Size(this.villageSelectVillage5.Width - 10, this.villageSelectVillage5.Height);
      this.villageSelectVillage5.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage5.TextYOffset = 0;
      this.villageSelectVillage5.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage5.Data = 4;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage5);
      this.villageSelectVillage5Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage5Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage5Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage5Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage5Delete.Position = new Point((int) byte.MaxValue, 93);
      this.villageSelectVillage5Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage5Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage5Delete);
      this.villageSelectVillage5Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage5Favourite.OverBrighten = true;
      this.villageSelectVillage5Favourite.Position = new Point(1, 91);
      this.villageSelectVillage5Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage5Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage5Favourite);
      this.villageSelectVillage6.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage6.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage6.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage6.ImageNorm = (Image) null;
      this.villageSelectVillage6.Position = new Point(20, 111);
      this.villageSelectVillage6.Text.Text = "Village 6";
      this.villageSelectVillage6.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage6.Text.Position = new Point(5, 0);
      this.villageSelectVillage6.Text.Size = new Size(this.villageSelectVillage6.Width - 10, this.villageSelectVillage6.Height);
      this.villageSelectVillage6.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage6.TextYOffset = 0;
      this.villageSelectVillage6.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage6.Data = 5;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage6);
      this.villageSelectVillage6Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage6Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage6Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage6Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage6Delete.Position = new Point((int) byte.MaxValue, 111);
      this.villageSelectVillage6Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage6Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage6Delete);
      this.villageSelectVillage6Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage6Favourite.OverBrighten = true;
      this.villageSelectVillage6Favourite.Position = new Point(1, 109);
      this.villageSelectVillage6Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage6Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage6Favourite);
      this.villageSelectVillage7.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage7.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage7.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage7.ImageNorm = (Image) null;
      this.villageSelectVillage7.Position = new Point(20, 129);
      this.villageSelectVillage7.Text.Text = "Village 7";
      this.villageSelectVillage7.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage7.Text.Position = new Point(5, 0);
      this.villageSelectVillage7.Text.Size = new Size(this.villageSelectVillage7.Width - 10, this.villageSelectVillage7.Height);
      this.villageSelectVillage7.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage7.TextYOffset = 0;
      this.villageSelectVillage7.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage7.Data = 6;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage7);
      this.villageSelectVillage7Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage7Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage7Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage7Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage7Delete.Position = new Point((int) byte.MaxValue, 129);
      this.villageSelectVillage7Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage7Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage7Delete);
      this.villageSelectVillage7Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage7Favourite.OverBrighten = true;
      this.villageSelectVillage7Favourite.Position = new Point(1, (int) sbyte.MaxValue);
      this.villageSelectVillage7Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage7Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage7Favourite);
      this.villageSelectVillage8.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage8.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage8.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage8.ImageNorm = (Image) null;
      this.villageSelectVillage8.Position = new Point(20, 147);
      this.villageSelectVillage8.Text.Text = "Village 8";
      this.villageSelectVillage8.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage8.Text.Position = new Point(5, 0);
      this.villageSelectVillage8.Text.Size = new Size(this.villageSelectVillage8.Width - 10, this.villageSelectVillage8.Height);
      this.villageSelectVillage8.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage8.TextYOffset = 0;
      this.villageSelectVillage8.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage8.Data = 7;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage8);
      this.villageSelectVillage8Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage8Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage8Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage8Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage8Delete.Position = new Point((int) byte.MaxValue, 147);
      this.villageSelectVillage8Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage8Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage8Delete);
      this.villageSelectVillage8Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage8Favourite.OverBrighten = true;
      this.villageSelectVillage8Favourite.Position = new Point(1, 145);
      this.villageSelectVillage8Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage8Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage8Favourite);
      this.villageSelectVillage9.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage9.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage9.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage9.ImageNorm = (Image) null;
      this.villageSelectVillage9.Position = new Point(20, 165);
      this.villageSelectVillage9.Text.Text = "Village 9";
      this.villageSelectVillage9.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage9.Text.Position = new Point(5, 0);
      this.villageSelectVillage9.Text.Size = new Size(this.villageSelectVillage9.Width - 10, this.villageSelectVillage9.Height);
      this.villageSelectVillage9.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage9.TextYOffset = 0;
      this.villageSelectVillage9.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage9.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage9.Data = 8;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage9);
      this.villageSelectVillage9Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage9Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage9Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage9Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage9Delete.Position = new Point((int) byte.MaxValue, 165);
      this.villageSelectVillage9Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage9Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage9Delete);
      this.villageSelectVillage9Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage9Favourite.OverBrighten = true;
      this.villageSelectVillage9Favourite.Position = new Point(1, 163);
      this.villageSelectVillage9Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage9Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage9Favourite);
      this.villageSelectVillage10.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage10.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage10.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage10.ImageNorm = (Image) null;
      this.villageSelectVillage10.Position = new Point(20, 183);
      this.villageSelectVillage10.Text.Text = "Village 10";
      this.villageSelectVillage10.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage10.Text.Position = new Point(5, 0);
      this.villageSelectVillage10.Text.Size = new Size(this.villageSelectVillage1.Width - 10, this.villageSelectVillage1.Height);
      this.villageSelectVillage10.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage10.TextYOffset = 0;
      this.villageSelectVillage10.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage10.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage10.Data = 9;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage10);
      this.villageSelectVillage10Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage10Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage10Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage10Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage10Delete.Position = new Point((int) byte.MaxValue, 183);
      this.villageSelectVillage10Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage10Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage10Delete);
      this.villageSelectVillage10Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage10Favourite.OverBrighten = true;
      this.villageSelectVillage10Favourite.Position = new Point(1, 181);
      this.villageSelectVillage10Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage10Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage10Favourite);
      this.villageSelectVillage11.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage11.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage11.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage11.ImageNorm = (Image) null;
      this.villageSelectVillage11.Position = new Point(20, 201);
      this.villageSelectVillage11.Text.Text = "Village 11";
      this.villageSelectVillage11.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage11.Text.Position = new Point(5, 0);
      this.villageSelectVillage11.Text.Size = new Size(this.villageSelectVillage1.Width - 10, this.villageSelectVillage1.Height);
      this.villageSelectVillage11.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage11.TextYOffset = 0;
      this.villageSelectVillage11.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage11.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage11.Data = 10;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage11);
      this.villageSelectVillage11Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage11Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage11Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage11Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage11Delete.Position = new Point((int) byte.MaxValue, 201);
      this.villageSelectVillage11Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage11Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage11Delete);
      this.villageSelectVillage11Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage11Favourite.OverBrighten = true;
      this.villageSelectVillage11Favourite.Position = new Point(1, 199);
      this.villageSelectVillage11Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage11Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage11Favourite);
      this.villageSelectVillage12.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage12.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage12.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage12.ImageNorm = (Image) null;
      this.villageSelectVillage12.Position = new Point(20, 219);
      this.villageSelectVillage12.Text.Text = "Village 12";
      this.villageSelectVillage12.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage12.Text.Position = new Point(5, 0);
      this.villageSelectVillage12.Text.Size = new Size(this.villageSelectVillage1.Width - 10, this.villageSelectVillage1.Height);
      this.villageSelectVillage12.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage12.TextYOffset = 0;
      this.villageSelectVillage12.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage12.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage12.Data = 11;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage12);
      this.villageSelectVillage12Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage12Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage12Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage12Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage12Delete.Position = new Point((int) byte.MaxValue, 219);
      this.villageSelectVillage12Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage12Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage12Delete);
      this.villageSelectVillage12Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage12Favourite.OverBrighten = true;
      this.villageSelectVillage12Favourite.Position = new Point(1, 217);
      this.villageSelectVillage12Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage12Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage12Favourite);
      this.villageSelectVillage13.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage13.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage13.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage13.ImageNorm = (Image) null;
      this.villageSelectVillage13.Position = new Point(20, 237);
      this.villageSelectVillage13.Text.Text = "Village 13";
      this.villageSelectVillage13.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage13.Text.Position = new Point(5, 0);
      this.villageSelectVillage13.Text.Size = new Size(this.villageSelectVillage1.Width - 10, this.villageSelectVillage1.Height);
      this.villageSelectVillage13.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage13.TextYOffset = 0;
      this.villageSelectVillage13.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage13.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage13.Data = 12;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage13);
      this.villageSelectVillage13Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage13Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage13Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage13Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage13Delete.Position = new Point((int) byte.MaxValue, 237);
      this.villageSelectVillage13Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage13Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage13Delete);
      this.villageSelectVillage13Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage13Favourite.OverBrighten = true;
      this.villageSelectVillage13Favourite.Position = new Point(1, 235);
      this.villageSelectVillage13Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage13Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage13Favourite);
      this.villageSelectVillage14.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage14.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage14.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage14.ImageNorm = (Image) null;
      this.villageSelectVillage14.Position = new Point(20, (int) byte.MaxValue);
      this.villageSelectVillage14.Text.Text = "Village 14";
      this.villageSelectVillage14.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage14.Text.Position = new Point(5, 0);
      this.villageSelectVillage14.Text.Size = new Size(this.villageSelectVillage1.Width - 10, this.villageSelectVillage1.Height);
      this.villageSelectVillage14.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage14.TextYOffset = 0;
      this.villageSelectVillage14.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage14.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage14.Data = 13;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage14);
      this.villageSelectVillage14Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage14Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage14Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage14Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage14Delete.Position = new Point((int) byte.MaxValue, (int) byte.MaxValue);
      this.villageSelectVillage14Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage14Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage14Delete);
      this.villageSelectVillage14Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage14Favourite.OverBrighten = true;
      this.villageSelectVillage14Favourite.Position = new Point(1, 253);
      this.villageSelectVillage14Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage14Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage14Favourite);
      this.villageSelectVillage15.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage15.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage15.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage15.ImageNorm = (Image) null;
      this.villageSelectVillage15.Position = new Point(20, 273);
      this.villageSelectVillage15.Text.Text = "Village 15";
      this.villageSelectVillage15.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage15.Text.Position = new Point(5, 0);
      this.villageSelectVillage15.Text.Size = new Size(this.villageSelectVillage1.Width - 10, this.villageSelectVillage1.Height);
      this.villageSelectVillage15.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage15.TextYOffset = 0;
      this.villageSelectVillage15.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage15.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage15.Data = 14;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage15);
      this.villageSelectVillage15Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage15Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage15Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage15Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage15Delete.Position = new Point((int) byte.MaxValue, 273);
      this.villageSelectVillage15Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage15Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage15Delete);
      this.villageSelectVillage15Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage15Favourite.OverBrighten = true;
      this.villageSelectVillage15Favourite.Position = new Point(1, 271);
      this.villageSelectVillage15Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage15Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage15Favourite);
      this.villageSelectVillage16.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage16.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage16.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage16.ImageNorm = (Image) null;
      this.villageSelectVillage16.Position = new Point(20, 291);
      this.villageSelectVillage16.Text.Text = "Village 16";
      this.villageSelectVillage16.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage16.Text.Position = new Point(5, 0);
      this.villageSelectVillage16.Text.Size = new Size(this.villageSelectVillage1.Width - 10, this.villageSelectVillage1.Height);
      this.villageSelectVillage16.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage16.TextYOffset = 0;
      this.villageSelectVillage16.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage16.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage16.Data = 15;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage16);
      this.villageSelectVillage16Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage16Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage16Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage16Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage16Delete.Position = new Point((int) byte.MaxValue, 291);
      this.villageSelectVillage16Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage16Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage16Delete);
      this.villageSelectVillage16Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage16Favourite.OverBrighten = true;
      this.villageSelectVillage16Favourite.Position = new Point(1, 289);
      this.villageSelectVillage16Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage16Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage16Favourite);
      this.villageSelectVillage17.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage17.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage17.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
      this.villageSelectVillage17.ImageNorm = (Image) null;
      this.villageSelectVillage17.Position = new Point(20, 309);
      this.villageSelectVillage17.Text.Text = "Village 17";
      this.villageSelectVillage17.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.villageSelectVillage17.Text.Position = new Point(5, 0);
      this.villageSelectVillage17.Text.Size = new Size(this.villageSelectVillage1.Width - 10, this.villageSelectVillage1.Height);
      this.villageSelectVillage17.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.villageSelectVillage17.TextYOffset = 0;
      this.villageSelectVillage17.Text.Color = Color.FromArgb(196, 161, 85);
      this.villageSelectVillage17.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
      this.villageSelectVillage17.Data = 16;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage17);
      this.villageSelectVillage17Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
      this.villageSelectVillage17Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
      this.villageSelectVillage17Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
      this.villageSelectVillage17Delete.Size = new Size(GFXLibrary.trashcan_normal.Width * 3 / 4, GFXLibrary.trashcan_normal.Height * 3 / 4);
      this.villageSelectVillage17Delete.Position = new Point((int) byte.MaxValue, 309);
      this.villageSelectVillage17Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
      this.villageSelectVillage17Delete.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage17Delete);
      this.villageSelectVillage17Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
      this.villageSelectVillage17Favourite.OverBrighten = true;
      this.villageSelectVillage17Favourite.Position = new Point(1, 307);
      this.villageSelectVillage17Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
      this.villageSelectVillage17Favourite.Data = 0;
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.villageSelectVillage17Favourite);
      this.worldMapButton.ImageNorm = (Image) GFXLibrary.int_button_findonmap_normal;
      this.worldMapButton.ImageOver = (Image) GFXLibrary.int_button_findonmap_over;
      this.worldMapButton.ImageClick = (Image) GFXLibrary.int_button_findonmap_in;
      this.worldMapButton.Position = new Point(56, 344);
      this.worldMapButton.Text.Text = SK.Text("MarketTradeScreen_Find_On_Map", "Find on map");
      this.worldMapButton.TextYOffset = -5;
      this.worldMapButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.worldMapButton.Text.Color = ARGBColors.Black;
      this.worldMapButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.findOnWorldClicked), "StockExchangePanel_find_on_map");
      this.villageSelectPanel.addControl((CustomSelfDrawPanel.CSDControl) this.worldMapButton);
      if (GameEngine.Instance.World.UserResearchData.Research_Merchant_Guilds == (byte) 0)
      {
        this.leftWindow.Visible = false;
        this.midWindow.Visible = false;
        this.buyWindow.Visible = false;
        this.sellWindow.Visible = false;
        this.infoWindow.Visible = false;
        this.noResearchWindow.Size = new Size(739, 150);
        this.noResearchWindow.Position = new Point(126, (this.Height - 150) / 2);
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.noResearchWindow);
        this.noResearchWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
        this.noResearchText.Text = SK.Text("Trade_Need_Research", "You don't currently have the required 'Merchant Guilds' research level to trade with other villages and exchanges. To begin trading you must research 'Merchant Guilds', place a Market in your village and recruit at least one Merchant.");
        this.noResearchText.Color = Color.FromArgb(224, 203, 146);
        this.noResearchText.Position = new Point(20, 0);
        this.noResearchText.Size = new Size(this.noResearchWindow.Width - 40, this.noResearchWindow.Height);
        this.noResearchText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        this.noResearchText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.noResearchWindow.addControl((CustomSelfDrawPanel.CSDControl) this.noResearchText);
        this.currentResource = -1;
      }
      else
      {
        this.leftWindow.Visible = true;
        this.midWindow.Visible = true;
        this.buyWindow.Visible = true;
        this.sellWindow.Visible = true;
        this.infoWindow.Visible = true;
      }
      this.advancedOptions.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
      this.advancedOptions.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
      this.advancedOptions.Position = new Point(20, 450);
      this.advancedOptions.Checked = Program.mySettings.AdvancedTrading;
      this.advancedOptions.CBLabel.Text = SK.Text("StockExchangePanel_advanced_options", "Show Advanced Trade Options");
      this.advancedOptions.CBLabel.Color = ARGBColors.Black;
      this.advancedOptions.CBLabel.Position = new Point(20, -1);
      this.advancedOptions.CBLabel.Size = new Size(this.midWindow.Width, 35);
      this.advancedOptions.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.advancedOptions.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.advancedToggle));
      this.midWindow.addControl((CustomSelfDrawPanel.CSDControl) this.advancedOptions);
      this.cardbar.Position = new Point(0, 0);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.cardbar);
      this.cardbar.init(1);
      this.updateExchangeHistory();
      this.seaConditionsImage.Image = (Image) GFXLibrary.sea_conditions[0];
      this.seaConditionsImage.Position = new Point(328, 112);
      this.seaConditionsImage.CustomTooltipID = 23000;
      this.seaConditionsImage.Visible = false;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.seaConditionsImage);
      this.lastTab = -1;
      this.manageTabs(1);
      this.updateDeliveryTime(-1);
      if (this.selectedStockExchange >= 0)
      {
        this.resetBackupData();
        this.selectStockExchange(this.selectedStockExchange);
        this.selectHighlightLine(0);
      }
      this.updateAdvancedOptions();
      this.update();
    }

    public void update()
    {
      if (this.currentResource >= 0)
      {
        this.currentResourcePacketSize = GameEngine.Instance.LocalWorldData.traderCarryingLevels[this.currentResource];
        this.currentResourcePacketSizeREAL = this.currentResourcePacketSize;
        this.currentResourcePacketSize = CardTypes.adjustTraderCarryLevels(GameEngine.Instance.cardsManager.UserCardData, this.currentResourcePacketSize);
      }
      this.updateValues();
      this.cardbar.update();
      this.updateDeliveryTime(this.selectedStockExchange);
      if (this.lastPremiumType == GameEngine.Instance.World.isAccountPremium())
        return;
      this.lastPremiumType = GameEngine.Instance.World.isAccountPremium();
      this.updateAdvancedOptions();
    }

    private void updateAdvancedOptions()
    {
      if (GameEngine.Instance.World.isAccountPremium() && Program.mySettings.AdvancedTrading)
      {
        for (int row = 0; row < 8; ++row)
        {
          CustomSelfDrawPanel.CSDButton rowHighestButton = this.getRowHighestButton(row);
          CustomSelfDrawPanel.CSDButton rowLowestButton = this.getRowLowestButton(row);
          CustomSelfDrawPanel.CSDLabel rowStored = this.getRowStored(row);
          CustomSelfDrawPanel.CSDLabel rowPrice = this.getRowPrice(row);
          if (rowPrice.Text.Length > 0)
          {
            rowHighestButton.Visible = true;
            rowLowestButton.Visible = true;
          }
          else
          {
            rowHighestButton.Visible = false;
            rowLowestButton.Visible = false;
          }
          rowStored.Position = new Point(198, 1);
          rowPrice.Position = new Point(306, 1);
        }
        this.lightArea2.Position = new Point(21, 102);
        this.lightArea3.Position = new Point(129, 102);
      }
      else
      {
        for (int row = 0; row < 8; ++row)
        {
          this.getRowHighestButton(row).Visible = false;
          this.getRowLowestButton(row).Visible = false;
          this.getRowStored(row).Position = new Point(230, 1);
          this.getRowPrice(row).Position = new Point(338, 1);
        }
        this.lightArea2.Position = new Point(53, 102);
        this.lightArea3.Position = new Point(161, 102);
      }
      this.advancedOptions.Visible = GameEngine.Instance.World.isAccountPremium();
      this.mainBackgroundImage.invalidate();
    }

    private void advancedToggle()
    {
      if (this.advancedOptions.Checked)
        StatTrackingClient.Instance().ActivateTrigger(10, (object) 0);
      Program.mySettings.AdvancedTrading = this.advancedOptions.Checked;
      this.updateAdvancedOptions();
    }

    private void tabClicked()
    {
      if (this.ClickedControl == null)
        return;
      CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) this.ClickedControl;
      if (clickedControl.Data == this.lastTab)
        return;
      this.manageTabs(clickedControl.Data);
    }

    public void logout() => this.selectedStockExchange = -1;

    private void manageTabs(int tabID)
    {
      if (tabID == this.lastTab)
        return;
      this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_normal;
      this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_over;
      this.tabButton2.ImageNorm = (Image) GFXLibrary.int_storage_tab_02_normal;
      this.tabButton2.ImageOver = (Image) GFXLibrary.int_storage_tab_02_over;
      this.tabButton3.ImageNorm = (Image) GFXLibrary.int_storage_tab_03_normal;
      this.tabButton3.ImageOver = (Image) GFXLibrary.int_storage_tab_03_over;
      this.tabButton4.ImageNorm = (Image) GFXLibrary.int_storage_tab_04_normal;
      this.tabButton4.ImageOver = (Image) GFXLibrary.int_storage_tab_04_over;
      this.fourthAgeMessage.Visible = false;
      this.localHeadingLabel.Visible = true;
      this.lightArea1.Visible = true;
      switch (tabID)
      {
        case 1:
          this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_selected;
          this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_selected;
          this.selectHighlightLine(0);
          this.initStockpileTab();
          this.selectHighlightLine(0);
          break;
        case 2:
          this.tabButton2.ImageNorm = (Image) GFXLibrary.int_storage_tab_02_selected;
          this.tabButton2.ImageOver = (Image) GFXLibrary.int_storage_tab_02_selected;
          this.selectHighlightLine(0);
          this.initGranaryTab();
          this.selectHighlightLine(0);
          break;
        case 3:
          this.tabButton3.ImageNorm = (Image) GFXLibrary.int_storage_tab_03_selected;
          this.tabButton3.ImageOver = (Image) GFXLibrary.int_storage_tab_03_selected;
          this.selectHighlightLine(0);
          this.initArmouryTab();
          this.selectHighlightLine(0);
          break;
        case 4:
          this.tabButton4.ImageNorm = (Image) GFXLibrary.int_storage_tab_04_selected;
          this.tabButton4.ImageOver = (Image) GFXLibrary.int_storage_tab_04_selected;
          this.selectHighlightLine(0);
          this.initHallTab();
          this.selectHighlightLine(0);
          break;
      }
      this.lastTab = tabID;
      this.Invalidate();
    }

    private void selectHighlightLine(int line)
    {
      this.highlightLine1.Image = (Image) null;
      this.highlightLine2.Image = (Image) null;
      this.highlightLine3.Image = (Image) null;
      this.highlightLine4.Image = (Image) null;
      this.highlightLine5.Image = (Image) null;
      this.highlightLine6.Image = (Image) null;
      this.highlightLine7.Image = (Image) null;
      this.highlightLine8.Image = (Image) null;
      this.currentResource = this.getRowButton(line).Data;
      CustomSelfDrawPanel.CSDImage rowHighlight = this.getRowHighlight(line);
      rowHighlight.Image = (Image) GFXLibrary.int_white_highlight_bar;
      rowHighlight.Size = new Size(465, 31);
      this.currentResourcePacketSize = GameEngine.Instance.LocalWorldData.traderCarryingLevels[this.currentResource];
      this.currentResourcePacketSizeREAL = this.currentResourcePacketSize;
      this.currentResourcePacketSize = CardTypes.adjustTraderCarryLevels(GameEngine.Instance.cardsManager.UserCardData, this.currentResourcePacketSize);
      this.buyHeadingLabel.Text = SK.Text("CapitalTradePanel_Buy", "Buy") + " : " + VillageBuildingsData.getResourceNames(this.currentResource);
      this.sellHeadingLabel.Text = SK.Text("CapitalTradePanel_Sell", "Sell") + " : " + VillageBuildingsData.getResourceNames(this.currentResource);
      this.buyHeadingImage.Image = (Image) GFXLibrary.getCommodity64DSImage(this.currentResource);
      this.sellHeadingImage.Image = (Image) GFXLibrary.getCommodity64DSImage(this.currentResource);
      this.buyTrack.Max = 50000;
      this.sellTrack.Max = 50000;
      if (this.lastHighlightResource != this.currentResource)
      {
        this.lastHighlightResource = this.currentResource;
        this.buyTrack.Value = 50000;
        this.sellTrack.Value = 50000;
      }
      this.showBuySellWindow();
    }

    private void initStockpileTab()
    {
      this.highlightLine1.Visible = true;
      this.highlightLine2.Visible = true;
      this.highlightLine3.Visible = true;
      this.highlightLine4.Visible = true;
      this.highlightLine5.Visible = false;
      this.highlightLine6.Visible = false;
      this.highlightLine7.Visible = false;
      this.highlightLine8.Visible = false;
      this.setRowInfo(0, 6);
      this.setRowInfo(1, 7);
      this.setRowInfo(2, 8);
      this.setRowInfo(3, 9);
    }

    private void initGranaryTab()
    {
      this.highlightLine1.Visible = true;
      this.highlightLine2.Visible = true;
      this.highlightLine3.Visible = true;
      this.highlightLine4.Visible = true;
      this.highlightLine5.Visible = true;
      this.highlightLine6.Visible = true;
      this.highlightLine7.Visible = true;
      this.highlightLine8.Visible = false;
      this.setRowInfo(0, 13);
      this.setRowInfo(1, 17);
      this.setRowInfo(2, 16);
      this.setRowInfo(3, 14);
      this.setRowInfo(4, 15);
      this.setRowInfo(5, 18);
      this.setRowInfo(6, 12);
    }

    private void initArmouryTab()
    {
      if (!GameEngine.Instance.LocalWorldData.EraWorld && GameEngine.Instance.World.FourthAgeWorld && !GameEngine.Instance.World.SixthAgeWorld)
      {
        this.highlightLine1.Visible = false;
        this.highlightLine2.Visible = false;
        this.highlightLine3.Visible = false;
        this.highlightLine4.Visible = false;
        this.highlightLine5.Visible = false;
        this.highlightLine6.Visible = false;
        this.highlightLine7.Visible = false;
        this.highlightLine8.Visible = false;
        this.fourthAgeMessage.Visible = true;
        this.localHeadingLabel.Visible = false;
        this.lightArea1.Visible = false;
      }
      else
      {
        this.highlightLine1.Visible = true;
        this.highlightLine2.Visible = true;
        this.highlightLine3.Visible = true;
        this.highlightLine4.Visible = true;
        this.highlightLine5.Visible = true;
        this.highlightLine6.Visible = false;
        this.highlightLine7.Visible = false;
        this.highlightLine8.Visible = false;
        this.setRowInfo(0, 29);
        this.setRowInfo(1, 28);
        this.setRowInfo(2, 31);
        this.setRowInfo(3, 30);
        this.setRowInfo(4, 32);
      }
    }

    private void initHallTab()
    {
      this.highlightLine1.Visible = true;
      this.highlightLine2.Visible = true;
      this.highlightLine3.Visible = true;
      this.highlightLine4.Visible = true;
      this.highlightLine5.Visible = true;
      this.highlightLine6.Visible = true;
      this.highlightLine7.Visible = true;
      this.highlightLine8.Visible = true;
      this.setRowInfo(0, 22);
      this.setRowInfo(1, 21);
      this.setRowInfo(2, 26);
      this.setRowInfo(3, 19);
      this.setRowInfo(4, 33);
      this.setRowInfo(5, 23);
      this.setRowInfo(6, 24);
      this.setRowInfo(7, 25);
    }

    private void rowClicked()
    {
      if (this.ClickedControl == null)
        return;
      CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) this.ClickedControl;
      if (clickedControl.Data == this.currentResource)
        return;
      this.buyTrack.Max = 50000;
      this.buyTrack.Value = 50000;
      this.sellTrack.Max = 50000;
      this.sellTrack.Value = 50000;
      GameEngine.Instance.playInterfaceSound("StockExchangePanel_resource_clicked");
      this.selectHighlightLine(this.getLineFromResource(clickedControl.Data));
      this.Invalidate();
    }

    private void setRowInfo(int line, int resource)
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      CustomSelfDrawPanel.CSDButton rowButton = this.getRowButton(line);
      rowButton.ImageIcon = (Image) GFXLibrary.getCommodity32DSImage(resource);
      rowButton.Text.Text = VillageBuildingsData.getResourceNames(resource);
      rowButton.Data = resource;
      int traderCarryingLevel = GameEngine.Instance.LocalWorldData.traderCarryingLevels[resource];
      int num = CardTypes.adjustTraderCarryLevels(GameEngine.Instance.cardsManager.UserCardData, traderCarryingLevel);
      rowButton.Text2.Text = num.ToString("N", (IFormatProvider) nfi);
      this.getRowHighestButton(line).Data = resource;
      this.getRowLowestButton(line).Data = resource;
      if (Program.mySettings.LanguageIdent == "pt")
      {
        if (resource == 22 || resource == 26)
        {
          rowButton.Size = new Size(191, 38);
          rowButton.UseTextSize = true;
          rowButton.Text.Size = new Size(100, 38);
          rowButton.Text.Position = new Point(91, 0);
        }
        else
        {
          rowButton.Size = new Size(191, 38);
          rowButton.UseTextSize = false;
          rowButton.Text.Size = rowButton.Size;
          rowButton.Text.Position = new Point(91, 0);
        }
      }
      else
      {
        rowButton.Size = new Size(191, 38);
        rowButton.UseTextSize = false;
        rowButton.Text.Size = rowButton.Size;
        rowButton.Text.Position = new Point(91, 0);
      }
    }

    private int getLineFromResource(int resource)
    {
      for (int row = 0; row < 8; ++row)
      {
        if (this.getRowButton(row).Data == resource)
          return row;
      }
      return 0;
    }

    private void setRowValues(int row, int localValue, int stockLevel, int priceValue)
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      CustomSelfDrawPanel.CSDLabel rowLocal = this.getRowLocal(row);
      rowLocal.Text = "";
      if (localValue >= 0)
        rowLocal.Text = localValue.ToString("N", (IFormatProvider) nfi);
      CustomSelfDrawPanel.CSDLabel rowStored = this.getRowStored(row);
      rowStored.Text = "";
      if (stockLevel >= 0)
        rowStored.Text = stockLevel.ToString("N", (IFormatProvider) nfi);
      CustomSelfDrawPanel.CSDLabel rowPrice = this.getRowPrice(row);
      rowPrice.Text = "";
      if (priceValue >= 0)
        rowPrice.Text = priceValue.ToString("N", (IFormatProvider) nfi);
      if (!GameEngine.Instance.World.isAccountPremium() || !Program.mySettings.AdvancedTrading)
        return;
      this.getRowHighestButton(row).Visible = priceValue >= 0;
      this.getRowLowestButton(row).Visible = priceValue >= 0;
    }

    private CustomSelfDrawPanel.CSDButton getRowButton(int row)
    {
      switch (row)
      {
        case 0:
          return this.selectRow1;
        case 1:
          return this.selectRow2;
        case 2:
          return this.selectRow3;
        case 3:
          return this.selectRow4;
        case 4:
          return this.selectRow5;
        case 5:
          return this.selectRow6;
        case 6:
          return this.selectRow7;
        case 7:
          return this.selectRow8;
        default:
          return (CustomSelfDrawPanel.CSDButton) null;
      }
    }

    private CustomSelfDrawPanel.CSDImage getRowHighlight(int row)
    {
      switch (row)
      {
        case 0:
          return this.highlightLine1;
        case 1:
          return this.highlightLine2;
        case 2:
          return this.highlightLine3;
        case 3:
          return this.highlightLine4;
        case 4:
          return this.highlightLine5;
        case 5:
          return this.highlightLine6;
        case 6:
          return this.highlightLine7;
        case 7:
          return this.highlightLine8;
        default:
          return (CustomSelfDrawPanel.CSDImage) null;
      }
    }

    private CustomSelfDrawPanel.CSDLabel getRowLocal(int row)
    {
      switch (row)
      {
        case 0:
          return this.localLabel1;
        case 1:
          return this.localLabel2;
        case 2:
          return this.localLabel3;
        case 3:
          return this.localLabel4;
        case 4:
          return this.localLabel5;
        case 5:
          return this.localLabel6;
        case 6:
          return this.localLabel7;
        case 7:
          return this.localLabel8;
        default:
          return (CustomSelfDrawPanel.CSDLabel) null;
      }
    }

    private CustomSelfDrawPanel.CSDLabel getRowStored(int row)
    {
      switch (row)
      {
        case 0:
          return this.storedLabel1;
        case 1:
          return this.storedLabel2;
        case 2:
          return this.storedLabel3;
        case 3:
          return this.storedLabel4;
        case 4:
          return this.storedLabel5;
        case 5:
          return this.storedLabel6;
        case 6:
          return this.storedLabel7;
        case 7:
          return this.storedLabel8;
        default:
          return (CustomSelfDrawPanel.CSDLabel) null;
      }
    }

    private CustomSelfDrawPanel.CSDLabel getRowPrice(int row)
    {
      switch (row)
      {
        case 0:
          return this.priceLabel1;
        case 1:
          return this.priceLabel2;
        case 2:
          return this.priceLabel3;
        case 3:
          return this.priceLabel4;
        case 4:
          return this.priceLabel5;
        case 5:
          return this.priceLabel6;
        case 6:
          return this.priceLabel7;
        case 7:
          return this.priceLabel8;
        default:
          return (CustomSelfDrawPanel.CSDLabel) null;
      }
    }

    private CustomSelfDrawPanel.CSDButton getRowHighestButton(int row)
    {
      switch (row)
      {
        case 0:
          return this.highestPriceRow1;
        case 1:
          return this.highestPriceRow2;
        case 2:
          return this.highestPriceRow3;
        case 3:
          return this.highestPriceRow4;
        case 4:
          return this.highestPriceRow5;
        case 5:
          return this.highestPriceRow6;
        case 6:
          return this.highestPriceRow7;
        case 7:
          return this.highestPriceRow8;
        default:
          return (CustomSelfDrawPanel.CSDButton) null;
      }
    }

    private CustomSelfDrawPanel.CSDButton getRowLowestButton(int row)
    {
      switch (row)
      {
        case 0:
          return this.lowestPriceRow1;
        case 1:
          return this.lowestPriceRow2;
        case 2:
          return this.lowestPriceRow3;
        case 3:
          return this.lowestPriceRow4;
        case 4:
          return this.lowestPriceRow5;
        case 5:
          return this.lowestPriceRow6;
        case 6:
          return this.lowestPriceRow7;
        case 7:
          return this.lowestPriceRow8;
        default:
          return (CustomSelfDrawPanel.CSDButton) null;
      }
    }

    private CustomSelfDrawPanel.CSDButton getVillageHistory(int line)
    {
      switch (line)
      {
        case 0:
          return this.villageSelectVillage1;
        case 1:
          return this.villageSelectVillage2;
        case 2:
          return this.villageSelectVillage3;
        case 3:
          return this.villageSelectVillage4;
        case 4:
          return this.villageSelectVillage5;
        case 5:
          return this.villageSelectVillage6;
        case 6:
          return this.villageSelectVillage7;
        case 7:
          return this.villageSelectVillage8;
        case 8:
          return this.villageSelectVillage9;
        case 9:
          return this.villageSelectVillage10;
        case 10:
          return this.villageSelectVillage11;
        case 11:
          return this.villageSelectVillage12;
        case 12:
          return this.villageSelectVillage13;
        case 13:
          return this.villageSelectVillage14;
        case 14:
          return this.villageSelectVillage15;
        case 15:
          return this.villageSelectVillage16;
        case 16:
          return this.villageSelectVillage17;
        default:
          return (CustomSelfDrawPanel.CSDButton) null;
      }
    }

    private CustomSelfDrawPanel.CSDButton getVillageHistoryFavourite(int line)
    {
      switch (line)
      {
        case 0:
          return this.villageSelectVillage1Favourite;
        case 1:
          return this.villageSelectVillage2Favourite;
        case 2:
          return this.villageSelectVillage3Favourite;
        case 3:
          return this.villageSelectVillage4Favourite;
        case 4:
          return this.villageSelectVillage5Favourite;
        case 5:
          return this.villageSelectVillage6Favourite;
        case 6:
          return this.villageSelectVillage7Favourite;
        case 7:
          return this.villageSelectVillage8Favourite;
        case 8:
          return this.villageSelectVillage9Favourite;
        case 9:
          return this.villageSelectVillage10Favourite;
        case 10:
          return this.villageSelectVillage11Favourite;
        case 11:
          return this.villageSelectVillage12Favourite;
        case 12:
          return this.villageSelectVillage13Favourite;
        case 13:
          return this.villageSelectVillage14Favourite;
        case 14:
          return this.villageSelectVillage15Favourite;
        case 15:
          return this.villageSelectVillage16Favourite;
        case 16:
          return this.villageSelectVillage17Favourite;
        default:
          return (CustomSelfDrawPanel.CSDButton) null;
      }
    }

    private CustomSelfDrawPanel.CSDButton getVillageHistoryDelete(int line)
    {
      switch (line)
      {
        case 0:
          return this.villageSelectVillage1Delete;
        case 1:
          return this.villageSelectVillage2Delete;
        case 2:
          return this.villageSelectVillage3Delete;
        case 3:
          return this.villageSelectVillage4Delete;
        case 4:
          return this.villageSelectVillage5Delete;
        case 5:
          return this.villageSelectVillage6Delete;
        case 6:
          return this.villageSelectVillage7Delete;
        case 7:
          return this.villageSelectVillage8Delete;
        case 8:
          return this.villageSelectVillage9Delete;
        case 9:
          return this.villageSelectVillage10Delete;
        case 10:
          return this.villageSelectVillage11Delete;
        case 11:
          return this.villageSelectVillage12Delete;
        case 12:
          return this.villageSelectVillage13Delete;
        case 13:
          return this.villageSelectVillage14Delete;
        case 14:
          return this.villageSelectVillage15Delete;
        case 15:
          return this.villageSelectVillage16Delete;
        case 16:
          return this.villageSelectVillage17Delete;
        default:
          return (CustomSelfDrawPanel.CSDButton) null;
      }
    }

    private void highestPricedClicked()
    {
      if (!GameEngine.Instance.World.isAccountPremium())
        return;
      int data = this.ClickedControl.Data;
      int num1 = 100000000;
      int villageID = this.selectedStockExchange;
      int num2 = 1000000000;
      int villageID1 = this.selectedStockExchange;
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
        villageID1 = village.VillageID;
      foreach (int num3 in this.closeCapitalsToTest)
      {
        if (this.stockExchanges[num3] != null)
        {
          int level = ((StockExchangePanel.StockExchangeInfo) this.stockExchanges[num3]).getLevel(data);
          if (level < num2)
          {
            num2 = level;
            villageID = num3;
            num1 = GameEngine.Instance.World.getSquareDistance(villageID1, num3);
          }
          else if (level == num2)
          {
            int squareDistance = GameEngine.Instance.World.getSquareDistance(villageID1, num3);
            if (squareDistance < num1)
            {
              villageID = num3;
              num1 = squareDistance;
            }
          }
        }
      }
      this.BACKUP_resource = data;
      this.selectStockExchange(villageID);
    }

    private void lowestPricedClicked()
    {
      if (!GameEngine.Instance.World.isAccountPremium())
        return;
      int data = this.ClickedControl.Data;
      int num1 = 100000000;
      int villageID = this.selectedStockExchange;
      int num2 = -1;
      int villageID1 = this.selectedStockExchange;
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
        villageID1 = village.VillageID;
      foreach (int num3 in this.closeCapitalsToTest)
      {
        if (this.stockExchanges[num3] != null)
        {
          int level = ((StockExchangePanel.StockExchangeInfo) this.stockExchanges[num3]).getLevel(data);
          if (level > num2)
          {
            num2 = level;
            villageID = num3;
            num1 = GameEngine.Instance.World.getSquareDistance(villageID1, num3);
          }
          else if (level == num2)
          {
            int squareDistance = GameEngine.Instance.World.getSquareDistance(villageID1, num3);
            if (squareDistance < num1)
            {
              villageID = num3;
              num1 = squareDistance;
            }
          }
        }
      }
      this.BACKUP_resource = data;
      this.selectStockExchange(villageID);
    }

    private void exchangeArrowClick()
    {
      if (this.exchangeArrowButton.Data == 0)
      {
        GameEngine.Instance.playInterfaceSound("StockExchangePanel_village_list_open");
        this.showVillagePanel(true);
      }
      else
      {
        GameEngine.Instance.playInterfaceSound("StockExchangePanel_village_list_close");
        this.showVillagePanel(false);
      }
    }

    private void villageClicked()
    {
      if (this.ClickedControl == null)
        return;
      GameEngine.Instance.playInterfaceSound("StockExchangePanel_village_clicked");
      CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) this.ClickedControl;
      this.BACKUP_resource = this.currentResource;
      this.BACKUP_buyLevel = this.buyTrack.Value;
      this.BACKUP_sellLevel = this.sellTrack.Value;
      this.selectStockExchange(clickedControl.Data);
      this.showVillagePanel(false);
    }

    private void villageRecentDeleteClicked()
    {
      if (this.ClickedControl == null)
        return;
      int data = this.ClickedControl.Data;
      RemoteServices.Instance.UpdateVillageFavourites(6, data);
      foreach (WorldMap.VillageNameItem villageNameItem in StockExchangePanel.exchangeHistory)
      {
        if (villageNameItem.villageID == data)
        {
          StockExchangePanel.exchangeHistory.Remove(villageNameItem);
          this.updateExchangeHistory();
          break;
        }
      }
    }

    private void villageFavouriteClicked()
    {
      if (this.ClickedControl == null)
        return;
      CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) this.ClickedControl;
      int data = clickedControl.Data;
      bool flag = false;
      foreach (WorldMap.VillageNameItem exchangeFavourite in StockExchangePanel.exchangeFavourites)
      {
        if (exchangeFavourite.villageID == data)
        {
          flag = true;
          StockExchangePanel.exchangeFavourites.Remove(exchangeFavourite);
          break;
        }
      }
      if (flag)
      {
        RemoteServices.Instance.UpdateVillageFavourites(3, data);
        clickedControl.ImageNorm = (Image) GFXLibrary.star_market_2;
        for (int line = 0; line < 17; ++line)
        {
          CustomSelfDrawPanel.CSDButton villageHistoryDelete = this.getVillageHistoryDelete(line);
          if (villageHistoryDelete.Data == clickedControl.Data)
          {
            villageHistoryDelete.Visible = true;
            break;
          }
        }
      }
      else
      {
        RemoteServices.Instance.UpdateVillageFavourites(2, data);
        StockExchangePanel.exchangeFavourites.Add(new WorldMap.VillageNameItem()
        {
          villageID = data,
          villageName = GameEngine.Instance.World.getExchangeName(data)
        });
        clickedControl.ImageNorm = (Image) GFXLibrary.star_market_1;
        for (int line = 0; line < 17; ++line)
        {
          CustomSelfDrawPanel.CSDButton villageHistoryDelete = this.getVillageHistoryDelete(line);
          if (villageHistoryDelete.Data == clickedControl.Data)
          {
            villageHistoryDelete.Visible = false;
            break;
          }
        }
      }
    }

    private void showVillagePanel(bool show)
    {
      this.villageSelectPanel.Visible = show;
      if (show)
      {
        this.exchangeArrowButton.ImageNorm = (Image) GFXLibrary.int_button_droparrow_up_normal;
        this.exchangeArrowButton.ImageOver = (Image) GFXLibrary.int_button_droparrow_up_over;
        this.exchangeArrowButton.ImageClick = (Image) GFXLibrary.int_button_droparrow_up_down;
        this.exchangeArrowButton.Data = 1;
        this.updateExchangeHistory();
      }
      else
      {
        this.exchangeArrowButton.ImageNorm = (Image) GFXLibrary.int_button_droparrow_normal;
        this.exchangeArrowButton.ImageOver = (Image) GFXLibrary.int_button_droparrow_over;
        this.exchangeArrowButton.ImageClick = (Image) GFXLibrary.int_button_droparrow_down;
        this.exchangeArrowButton.Data = 0;
      }
    }

    public void updateValues()
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
      {
        StockExchangePanel.StockExchangeInfo stockExchangeInfo = (StockExchangePanel.StockExchangeInfo) null;
        if (this.selectedStockExchange >= 0 && this.stockExchanges[this.selectedStockExchange] != null)
        {
          stockExchangeInfo = (StockExchangePanel.StockExchangeInfo) this.stockExchanges[this.selectedStockExchange];
          this.updateDeliveryTime(this.selectedStockExchange);
        }
        WorldData localWorldData = GameEngine.Instance.LocalWorldData;
        switch (this.lastTab)
        {
          case 1:
            VillageMap.StockpileLevels levels1 = new VillageMap.StockpileLevels();
            village.getStockpileLevels(levels1);
            if (stockExchangeInfo == null)
            {
              this.setRowValues(0, (int) levels1.woodLevel, -1, -1);
              this.setRowValues(1, (int) levels1.stoneLevel, -1, -1);
              this.setRowValues(2, (int) levels1.ironLevel, -1, -1);
              this.setRowValues(3, (int) levels1.pitchLevel, -1, -1);
              break;
            }
            this.setRowValues(0, (int) levels1.woodLevel, stockExchangeInfo.woodLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(6), 6));
            this.setRowValues(1, (int) levels1.stoneLevel, stockExchangeInfo.stoneLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(7), 7));
            this.setRowValues(2, (int) levels1.ironLevel, stockExchangeInfo.ironLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(8), 8));
            this.setRowValues(3, (int) levels1.pitchLevel, stockExchangeInfo.pitchLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(9), 9));
            break;
          case 2:
            VillageMap.GranaryLevels levels2 = new VillageMap.GranaryLevels();
            village.getGranaryLevels(levels2);
            VillageMap.InnLevels levels3 = new VillageMap.InnLevels();
            village.getInnLevels(levels3);
            if (stockExchangeInfo == null)
            {
              this.setRowValues(0, (int) levels2.applesLevel, -1, -1);
              this.setRowValues(1, (int) levels2.cheeseLevel, -1, -1);
              this.setRowValues(2, (int) levels2.meatLevel, -1, -1);
              this.setRowValues(3, (int) levels2.breadLevel, -1, -1);
              this.setRowValues(4, (int) levels2.vegLevel, -1, -1);
              this.setRowValues(5, (int) levels2.fishLevel, -1, -1);
              this.setRowValues(6, (int) levels3.aleLevel, -1, -1);
              break;
            }
            this.setRowValues(0, (int) levels2.applesLevel, stockExchangeInfo.applesLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(13), 13));
            this.setRowValues(1, (int) levels2.cheeseLevel, stockExchangeInfo.cheeseLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(17), 17));
            this.setRowValues(2, (int) levels2.meatLevel, stockExchangeInfo.meatLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(16), 16));
            this.setRowValues(3, (int) levels2.breadLevel, stockExchangeInfo.breadLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(14), 14));
            this.setRowValues(4, (int) levels2.vegLevel, stockExchangeInfo.vegLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(15), 15));
            this.setRowValues(5, (int) levels2.fishLevel, stockExchangeInfo.fishLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(18), 18));
            this.setRowValues(6, (int) levels3.aleLevel, stockExchangeInfo.aleLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(12), 12));
            break;
          case 3:
            VillageMap.ArmouryLevels levels4 = new VillageMap.ArmouryLevels();
            village.getArmouryLevels(levels4);
            if (stockExchangeInfo == null)
            {
              this.setRowValues(0, (int) levels4.bowsLevel, -1, -1);
              this.setRowValues(1, (int) levels4.pikesLevel, -1, -1);
              this.setRowValues(2, (int) levels4.armourLevel, -1, -1);
              this.setRowValues(3, (int) levels4.swordsLevel, -1, -1);
              this.setRowValues(4, (int) levels4.catapultsLevel, -1, -1);
              break;
            }
            this.setRowValues(0, (int) levels4.bowsLevel, stockExchangeInfo.bowsLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(29), 29));
            this.setRowValues(1, (int) levels4.pikesLevel, stockExchangeInfo.pikesLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(28), 28));
            this.setRowValues(2, (int) levels4.armourLevel, stockExchangeInfo.armourLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(31), 31));
            this.setRowValues(3, (int) levels4.swordsLevel, stockExchangeInfo.swordsLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(30), 30));
            this.setRowValues(4, (int) levels4.catapultsLevel, stockExchangeInfo.catapultsLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(32), 32));
            break;
          case 4:
            VillageMap.TownHallLevels levels5 = new VillageMap.TownHallLevels();
            village.getTownHallLevels(levels5);
            if (stockExchangeInfo == null)
            {
              this.setRowValues(0, (int) levels5.venisonLevel, -1, -1);
              this.setRowValues(1, (int) levels5.furnitureLevel, -1, -1);
              this.setRowValues(2, (int) levels5.metalwareLevel, -1, -1);
              this.setRowValues(3, (int) levels5.clothesLevel, -1, -1);
              this.setRowValues(4, (int) levels5.wineLevel, -1, -1);
              this.setRowValues(5, (int) levels5.saltLevel, -1, -1);
              this.setRowValues(6, (int) levels5.spicesLevel, -1, -1);
              this.setRowValues(7, (int) levels5.silkLevel, -1, -1);
              break;
            }
            this.setRowValues(0, (int) levels5.venisonLevel, stockExchangeInfo.venisonLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(22), 22));
            this.setRowValues(1, (int) levels5.furnitureLevel, stockExchangeInfo.furnitureLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(21), 21));
            this.setRowValues(2, (int) levels5.metalwareLevel, stockExchangeInfo.metalwareLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(26), 26));
            this.setRowValues(3, (int) levels5.clothesLevel, stockExchangeInfo.clothesLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(19), 19));
            this.setRowValues(4, (int) levels5.wineLevel, stockExchangeInfo.wineLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(33), 33));
            this.setRowValues(5, (int) levels5.saltLevel, stockExchangeInfo.saltLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(23), 23));
            this.setRowValues(6, (int) levels5.spicesLevel, stockExchangeInfo.spicesLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(24), 24));
            this.setRowValues(7, (int) levels5.silkLevel, stockExchangeInfo.silkLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(25), 25));
            break;
        }
        this.numTraders = village.numTraders();
        this.numFreeTraders = village.numFreeTraders();
        if (this.numFreeTraders > this.numTraders)
          village.refreshTraderNumbers();
        this.tradersAvailableValue.Text = this.numFreeTraders.ToString() + "/" + this.numTraders.ToString();
        this.traderCapacityValue.Text = (this.currentResourcePacketSize * this.numFreeTraders).ToString("N", (IFormatProvider) nfi);
      }
      else
      {
        for (int row = 0; row < 8; ++row)
          this.setRowValues(row, -1, -1, -1);
        this.tradersAvailableValue.Text = "0/0";
        this.traderCapacityValue.Text = "0";
      }
      this.showBuySellWindow();
    }

    public void updateExchangeHistory()
    {
      for (int line = 0; line < 17; ++line)
      {
        this.getVillageHistory(line).Visible = false;
        this.getVillageHistoryFavourite(line).Visible = false;
        this.getVillageHistoryDelete(line).Visible = false;
      }
      int num;
      for (num = 0; num < 17 && num < StockExchangePanel.exchangeFavourites.Count; ++num)
      {
        WorldMap.VillageNameItem exchangeFavourite = StockExchangePanel.exchangeFavourites[num];
        CustomSelfDrawPanel.CSDButton villageHistory = this.getVillageHistory(num);
        villageHistory.Visible = true;
        villageHistory.Text.Text = GameEngine.Instance.World.getExchangeName(exchangeFavourite.villageID);
        villageHistory.Data = exchangeFavourite.villageID;
        CustomSelfDrawPanel.CSDButton historyFavourite = this.getVillageHistoryFavourite(num);
        historyFavourite.ImageNorm = (Image) GFXLibrary.star_market_1;
        historyFavourite.Visible = true;
        historyFavourite.Data = exchangeFavourite.villageID;
        historyFavourite.CustomTooltipID = 808;
        this.getVillageHistoryDelete(num).Data = exchangeFavourite.villageID;
      }
      for (int index = 0; num < 17 && index < StockExchangePanel.exchangeHistory.Count; ++index)
      {
        WorldMap.VillageNameItem villageNameItem = StockExchangePanel.exchangeHistory[index];
        bool flag = false;
        foreach (WorldMap.VillageNameItem exchangeFavourite in StockExchangePanel.exchangeFavourites)
        {
          if (exchangeFavourite.villageID == villageNameItem.villageID)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          CustomSelfDrawPanel.CSDButton villageHistory = this.getVillageHistory(num);
          villageHistory.Visible = true;
          villageHistory.Text.Text = GameEngine.Instance.World.getExchangeName(villageNameItem.villageID);
          villageHistory.Data = villageNameItem.villageID;
          CustomSelfDrawPanel.CSDButton historyFavourite = this.getVillageHistoryFavourite(num);
          historyFavourite.ImageNorm = (Image) GFXLibrary.star_market_2;
          historyFavourite.Visible = true;
          historyFavourite.Data = villageNameItem.villageID;
          historyFavourite.CustomTooltipID = 809;
          CustomSelfDrawPanel.CSDButton villageHistoryDelete = this.getVillageHistoryDelete(num);
          villageHistoryDelete.Visible = true;
          villageHistoryDelete.Data = villageNameItem.villageID;
          villageHistoryDelete.CustomTooltipID = 810;
          ++num;
        }
      }
    }

    private void updateDeliveryTime(int villageID)
    {
      VillageMap village = GameEngine.Instance.Village;
      if (villageID >= 0 && village != null)
      {
        WorldData localWorldData = GameEngine.Instance.LocalWorldData;
        Point villageLocation1 = GameEngine.Instance.World.getVillageLocation(village.VillageID);
        Point villageLocation2 = GameEngine.Instance.World.getVillageLocation(villageID);
        int x1 = villageLocation1.X;
        int y1 = villageLocation1.Y;
        int villageRegion1 = GameEngine.Instance.World.getVillageRegion(village.VillageID);
        int x2 = villageLocation2.X;
        int y2 = villageLocation2.Y;
        int villageRegion2 = GameEngine.Instance.World.getVillageRegion(villageID);
        double time = villageRegion1 == villageRegion2 ? localWorldData.traderStockExchangeSameRegionTime : Math.Sqrt((double) ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))) * (localWorldData.traderMoveSpeed * localWorldData.gamePlaySpeed);
        double distance1 = GameEngine.Instance.World.UserResearchData.adjustTradeTimes(time) * CardTypes.cards_adjustTradeTimes(GameEngine.Instance.cardsManager.UserCardData);
        double distance2 = CardTypes.cards_adjustTradeTimesCompleteContract(GameEngine.Instance.cardsManager.UserCardData, distance1);
        string buildTimeString = VillageMap.createBuildTimeString((int) GameEngine.Instance.World.adjustIfIslandTravel(distance2, village.VillageID, villageID));
        this.deliveryTimeAreaLabel.TextDiffOnly = SK.Text("TRADE_Delivery_Time", "Delivery Time") + ":  " + buildTimeString;
        if (GameEngine.Instance.World.isIslandTravel(village.VillageID, villageID))
        {
          this.seaConditionsImage.Visible = true;
          int index = GameEngine.Instance.World.SpecialSeaConditionsData + 4;
          if (index < 0)
            index = 0;
          else if (index >= 9)
            index = 8;
          if (this.lastSeaConditions == index)
            return;
          this.lastSeaConditions = index;
          this.seaConditionsImage.Image = (Image) GFXLibrary.sea_conditions[index];
          this.seaConditionsImage.CustomTooltipID = 23000 + index;
        }
        else
          this.seaConditionsImage.Visible = false;
      }
      else
      {
        this.deliveryTimeAreaLabel.TextDiffOnly = SK.Text("TRADE_Delivery_Time", "Delivery Time") + ":  ";
        this.seaConditionsImage.Visible = false;
        this.lastSeaConditions = -1;
      }
    }

    private void showBuySellWindow()
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      bool visible1 = this.buyWindow.Visible;
      bool visible2 = this.sellWindow.Visible;
      this.buyWindow.Visible = false;
      this.sellWindow.Visible = false;
      if (GameEngine.Instance.World.UserResearchData.Research_Merchant_Guilds == (byte) 0 || GameEngine.Instance.World.WorldEnded)
        return;
      VillageMap village = GameEngine.Instance.Village;
      if (village != null && this.currentResource >= 0 && this.selectedStockExchange >= 0 && this.stockExchanges[this.selectedStockExchange] != null && this.currentResource > 0)
      {
        WorldData localWorldData = GameEngine.Instance.LocalWorldData;
        StockExchangePanel.StockExchangeInfo stockExchange = (StockExchangePanel.StockExchangeInfo) this.stockExchanges[this.selectedStockExchange];
        int resourceLevel = (int) village.getResourceLevel(this.currentResource);
        int level = stockExchange.getLevel(this.currentResource);
        int num1 = this.numFreeTraders;
        int num2 = TradingCalcs.calcGoldCost(localWorldData, level, this.currentResource, level - this.buyTrack.Value * this.currentResourcePacketSize);
        int num3 = TradingCalcs.calcGoldCost(localWorldData, level, this.currentResource, level + this.sellTrack.Value * this.currentResourcePacketSize);
        if (resourceLevel >= this.currentResourcePacketSize)
        {
          this.sellWindow.Visible = true;
          int num4 = resourceLevel / this.currentResourcePacketSize;
          if (num4 > num1)
            num4 = num1;
          int max = this.sellTrack.Max;
          if (num4 > max)
            this.sellTrack.Max = num4;
          else if (num4 < max)
          {
            if (this.sellTrack.Value > num4)
              this.sellTrack.Value = num4;
            this.sellTrack.Max = num4;
          }
          int buyCost = num3;
          this.sellCostValue.Text = (TradingCalcs.calcSellCost(localWorldData, buyCost) * this.sellTrack.Value * this.currentResourcePacketSize / this.currentResourcePacketSizeREAL).ToString("N", (IFormatProvider) nfi);
          this.sellNumber.Text = (this.sellTrack.Value * this.currentResourcePacketSize).ToString("N", (IFormatProvider) nfi);
          this.sellMax.Text = (this.sellTrack.Max * this.currentResourcePacketSize).ToString("N", (IFormatProvider) nfi);
          this.sellHeadingImage.invalidate();
        }
        if (level >= this.currentResourcePacketSize)
        {
          this.buyWindow.Visible = true;
          int currentGold = (int) GameEngine.Instance.World.getCurrentGold();
          int num5 = level / this.currentResourcePacketSize;
          int num6 = currentGold / num2;
          if (num1 > num6)
            num1 = num6;
          if (num5 > num1)
            num5 = num1;
          int max = this.buyTrack.Max;
          if (num5 > max)
            this.buyTrack.Max = num5;
          else if (num5 < max)
          {
            if (this.buyTrack.Value > num5)
              this.buyTrack.Value = num5;
            this.buyTrack.Max = num5;
          }
          this.buyCostValue.Text = (this.buyTrack.Value * (num2 * this.currentResourcePacketSize / this.currentResourcePacketSizeREAL)).ToString("N", (IFormatProvider) nfi);
          this.buyNumber.Text = (this.buyTrack.Value * this.currentResourcePacketSize).ToString("N", (IFormatProvider) nfi);
          this.buyMax.Text = (this.buyTrack.Max * this.currentResourcePacketSize).ToString("N", (IFormatProvider) nfi);
          this.buyHeadingImage.invalidate();
        }
      }
      this.stockExchangeImage.Alpha = this.buyWindow.Visible || this.sellWindow.Visible ? 0.15f : 1f;
      this.validateBuySellButtons();
      if (visible1 == this.buyWindow.Visible && visible2 == this.sellWindow.Visible)
        return;
      this.mainBackgroundImage.invalidate();
    }

    private void validateBuySellButtons()
    {
      if (this.buyWindow.Visible && this.buyTrack.Value > 0)
        this.buyButton.Enabled = true;
      else
        this.buyButton.Enabled = false;
      if (this.sellWindow.Visible && this.sellTrack.Value > 0)
        this.sellButton.Enabled = true;
      else
        this.sellButton.Enabled = false;
    }

    private void findOnWorldClicked()
    {
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      this.BACKUP_resource = this.currentResource;
      this.BACKUP_sellLevel = this.sellTrack.Value;
      this.BACKUP_buyLevel = this.buyTrack.Value;
      GameEngine.Instance.World.zoomToVillage(village.VillageID);
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(4);
      InterfaceMgr.Instance.StockExchangeBuyingVillage = village.VillageID;
    }

    private void tracksMoved()
    {
      this.showBuySellWindow();
      this.buyWindow.invalidate();
      this.sellWindow.invalidate();
    }

    public void resetBackupData()
    {
      this.BACKUP_resource = -1;
      this.BACKUP_sellLevel = 50000;
      this.BACKUP_buyLevel = 50000;
    }

    public void selectStockExchange(int villageID)
    {
      if (villageID == -2)
        villageID = this.selectedStockExchange;
      if (villageID < 0)
      {
        this.selectedStockExchange = -1;
      }
      else
      {
        this.selectedStockExchange = villageID;
        bool flag1 = true;
        if (GameEngine.Instance.World.isAccountPremium())
        {
          flag1 = false;
          int num = villageID;
          VillageMap village = GameEngine.Instance.Village;
          if (village != null)
            num = village.VillageID;
          List<StockExchangePanel.ClosestCapitalSortItem> closestCapitalSortItemList = new List<StockExchangePanel.ClosestCapitalSortItem>();
          foreach (int capital in GameEngine.Instance.World.getCapitalList())
          {
            if (capital != villageID)
            {
              int squareDistance = GameEngine.Instance.World.getSquareDistance(num, capital);
              if (squareDistance < 40000 && GameEngine.Instance.World.allowExchangeTrade(capital, num))
                closestCapitalSortItemList.Add(new StockExchangePanel.ClosestCapitalSortItem()
                {
                  distance = squareDistance,
                  villageID = capital
                });
            }
          }
          this.closeCapitalsToTest.Clear();
          this.closeCapitalsToTest.Add(villageID);
          closestCapitalSortItemList.Sort((Comparison<StockExchangePanel.ClosestCapitalSortItem>) ((a, b) => a.distance.CompareTo(b.distance)));
          if (closestCapitalSortItemList.Count > 20)
            closestCapitalSortItemList.RemoveRange(20, closestCapitalSortItemList.Count - 20);
          List<int> intList = new List<int>();
          foreach (StockExchangePanel.ClosestCapitalSortItem closestCapitalSortItem in closestCapitalSortItemList)
          {
            this.closeCapitalsToTest.Add(closestCapitalSortItem.villageID);
            bool flag2 = true;
            if (this.stockExchanges[closestCapitalSortItem.villageID] != null && (DateTime.Now - ((StockExchangePanel.StockExchangeInfo) this.stockExchanges[closestCapitalSortItem.villageID]).lastTime).TotalMinutes < 1.0)
              flag2 = false;
            if (flag2)
              intList.Add(closestCapitalSortItem.villageID);
          }
          if (intList.Count > 0)
          {
            RemoteServices.Instance.set_GetStockExchangeData_UserCallBack(new RemoteServices.GetStockExchangeData_UserCallBack(this.getStockExchangeDataCallback));
            RemoteServices.Instance.GetStockExchangePremiumData(villageID, intList.ToArray());
          }
          else
            flag1 = true;
        }
        if (flag1)
        {
          bool flag3 = true;
          if (this.stockExchanges[this.selectedStockExchange] != null && (DateTime.Now - ((StockExchangePanel.StockExchangeInfo) this.stockExchanges[this.selectedStockExchange]).lastTime).TotalMinutes < 1.0)
            flag3 = false;
          if (flag3)
          {
            RemoteServices.Instance.set_GetStockExchangeData_UserCallBack(new RemoteServices.GetStockExchangeData_UserCallBack(this.getStockExchangeDataCallback));
            RemoteServices.Instance.GetStockExchangeData(villageID, true);
          }
        }
        this.updateDeliveryTime(villageID);
        this.exchangeNameLabel.Text = GameEngine.Instance.World.getExchangeName(villageID);
        this.currentResource = this.BACKUP_resource;
        if (this.BACKUP_resource >= 0)
        {
          this.lastTab = -1;
          switch (this.BACKUP_resource)
          {
            case 6:
            case 7:
            case 8:
            case 9:
              this.manageTabs(1);
              break;
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
              this.manageTabs(2);
              break;
            case 19:
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26:
            case 33:
              this.manageTabs(4);
              break;
            case 28:
            case 29:
            case 30:
            case 31:
            case 32:
              this.manageTabs(3);
              break;
          }
          this.selectHighlightLine(this.getLineFromResource(this.BACKUP_resource));
          this.buyTrack.Max = 50000;
          this.buyTrack.Value = this.BACKUP_buyLevel;
          this.sellTrack.Max = 50000;
          this.sellTrack.Value = this.BACKUP_sellLevel;
        }
        this.updateValues();
      }
    }

    public void closeClick() => InterfaceMgr.Instance.setVillageTabSubMode(-1);

    private void BuyClickConinue()
    {
      GameEngine.Instance.Village.stockExchangeTrade(this.selectedStockExchange, this.currentResource, this.buyTrack.Value * this.currentResourcePacketSize, true);
      this.addVillageToHistory(this.selectedStockExchange);
      string tag = "";
      switch (this.currentResource)
      {
        case 6:
          tag = "MarketResource_Wood";
          break;
        case 7:
          tag = "MarketResource_Stone";
          break;
        case 8:
          tag = "MarketResource_Iron";
          break;
        case 9:
          tag = "MarketResource_Pitch";
          break;
        case 12:
          tag = "MarketResource_Ale";
          break;
        case 13:
          tag = "MarketResource_Apples";
          break;
        case 14:
          tag = "MarketResource_Bread";
          break;
        case 15:
          tag = "MarketResource_Veg";
          break;
        case 16:
          tag = "MarketResource_Meat";
          break;
        case 17:
          tag = "MarketResource_Cheese";
          break;
        case 18:
          tag = "MarketResource_Fish";
          break;
        case 19:
          tag = "MarketResource_Clothes";
          break;
        case 21:
          tag = "MarketResource_Furniture";
          break;
        case 22:
          tag = "MarketResource_Venison";
          break;
        case 23:
          tag = "MarketResource_Salt";
          break;
        case 24:
          tag = "MarketResource_Spices";
          break;
        case 25:
          tag = "MarketResource_Salt";
          break;
        case 26:
          tag = "MarketResource_Metalware";
          break;
        case 28:
          tag = "MarketResource_Pikes";
          break;
        case 29:
          tag = "MarketResource_Bows";
          break;
        case 30:
          tag = "MarketResource_Swords";
          break;
        case 31:
          tag = "MarketResource_Armour";
          break;
        case 32:
          tag = "MarketResource_Catapults";
          break;
        case 33:
          tag = "MarketResource_Wine";
          break;
      }
      Sound.playDelayedInterfaceSound(tag, 100);
    }

    private void buyClick()
    {
      DateTime now = DateTime.Now;
      if ((now - this.lastTradeTime).TotalSeconds < 3.0)
        return;
      this.lastTradeTime = now;
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      this.dirtyStockExchangeInfo(this.selectedStockExchange);
      double num1 = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, this.currentResource, GameEngine.Instance.cardsManager.UserCardData, false) - village.getResourceLevel(this.currentResource);
      int num2 = this.buyTrack.Value * this.currentResourcePacketSize;
      if (num1 < Convert.ToDouble(num2))
      {
        if (MyMessageBox.Show(SK.Text("Stock_Exchange_Space_Warning", "You do not have enough space to store all of the goods. Do you wish to continue with the trade? You will receive :") + " " + Convert.ToInt32(num1).ToString(), SK.Text("Stock_Exchange_Space_Warning_Title", "Insufficient Storage Space"), MessageBoxButtons.YesNo) == DialogResult.No)
          return;
        this.BuyClickConinue();
      }
      else
        this.BuyClickConinue();
    }

    private void sellClick()
    {
      DateTime now = DateTime.Now;
      if ((now - this.lastTradeTime).TotalSeconds < 3.0)
        return;
      this.lastTradeTime = now;
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      this.dirtyStockExchangeInfo(this.selectedStockExchange);
      village.stockExchangeTrade(this.selectedStockExchange, this.currentResource, this.sellTrack.Value * this.currentResourcePacketSize, false);
      this.addVillageToHistory(this.selectedStockExchange);
      string tag = "";
      switch (this.currentResource)
      {
        case 6:
          tag = "MarketResource_Wood";
          break;
        case 7:
          tag = "MarketResource_Stone";
          break;
        case 8:
          tag = "MarketResource_Iron";
          break;
        case 9:
          tag = "MarketResource_Pitch";
          break;
        case 12:
          tag = "MarketResource_Ale";
          break;
        case 13:
          tag = "MarketResource_Apples";
          break;
        case 14:
          tag = "MarketResource_Bread";
          break;
        case 15:
          tag = "MarketResource_Veg";
          break;
        case 16:
          tag = "MarketResource_Meat";
          break;
        case 17:
          tag = "MarketResource_Cheese";
          break;
        case 18:
          tag = "MarketResource_Fish";
          break;
        case 19:
          tag = "MarketResource_Clothes";
          break;
        case 21:
          tag = "MarketResource_Furniture";
          break;
        case 22:
          tag = "MarketResource_Venison";
          break;
        case 23:
          tag = "MarketResource_Salt";
          break;
        case 24:
          tag = "MarketResource_Spices";
          break;
        case 25:
          tag = "MarketResource_Salt";
          break;
        case 26:
          tag = "MarketResource_Metalware";
          break;
        case 28:
          tag = "MarketResource_Pikes";
          break;
        case 29:
          tag = "MarketResource_Bows";
          break;
        case 30:
          tag = "MarketResource_Swords";
          break;
        case 31:
          tag = "MarketResource_Armour";
          break;
        case 32:
          tag = "MarketResource_Catapults";
          break;
        case 33:
          tag = "MarketResource_Wine";
          break;
      }
      Sound.playDelayedInterfaceSound(tag, 100);
    }

    private void addVillageToHistory(int villageID)
    {
      bool flag = false;
      foreach (WorldMap.VillageNameItem villageNameItem in StockExchangePanel.exchangeHistory)
      {
        if (villageNameItem.villageID == villageID)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      StockExchangePanel.exchangeHistory.Add(new WorldMap.VillageNameItem()
      {
        villageID = villageID,
        villageName = GameEngine.Instance.World.getExchangeName(villageID)
      });
      this.updateExchangeHistory();
    }

    public void getStockExchangeDataCallback(GetStockExchangeData_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.stockExchanges[returnData.villageID] = (object) new StockExchangePanel.StockExchangeInfo()
      {
        lastTime = DateTime.Now,
        villageID = returnData.villageID,
        woodLevel = returnData.woodLevel,
        stoneLevel = returnData.stoneLevel,
        ironLevel = returnData.ironLevel,
        pitchLevel = returnData.pitchLevel,
        aleLevel = returnData.aleLevel,
        applesLevel = returnData.applesLevel,
        breadLevel = returnData.breadLevel,
        meatLevel = returnData.meatLevel,
        cheeseLevel = returnData.cheeseLevel,
        vegLevel = returnData.vegLevel,
        fishLevel = returnData.fishLevel,
        bowsLevel = returnData.bowsLevel,
        pikesLevel = returnData.pikesLevel,
        swordsLevel = returnData.swordsLevel,
        armourLevel = returnData.armourLevel,
        catapultsLevel = returnData.catapultsLevel,
        furnitureLevel = returnData.furnitureLevel,
        clothesLevel = returnData.clothesLevel,
        saltLevel = returnData.saltLevel,
        venisonLevel = returnData.venisonLevel,
        silkLevel = returnData.silkLevel,
        spicesLevel = returnData.spicesLevel,
        metalwareLevel = returnData.metalwareLevel,
        wineLevel = returnData.wineLevel
      };
      if (returnData.otherVillages != null)
      {
        foreach (GetStockExchangeData_ReturnType otherVillage in returnData.otherVillages)
          this.stockExchanges[otherVillage.villageID] = (object) new StockExchangePanel.StockExchangeInfo()
          {
            lastTime = DateTime.Now.AddMinutes(1.0),
            villageID = otherVillage.villageID,
            woodLevel = otherVillage.woodLevel,
            stoneLevel = otherVillage.stoneLevel,
            ironLevel = otherVillage.ironLevel,
            pitchLevel = otherVillage.pitchLevel,
            aleLevel = otherVillage.aleLevel,
            applesLevel = otherVillage.applesLevel,
            breadLevel = otherVillage.breadLevel,
            meatLevel = otherVillage.meatLevel,
            cheeseLevel = otherVillage.cheeseLevel,
            vegLevel = otherVillage.vegLevel,
            fishLevel = otherVillage.fishLevel,
            bowsLevel = otherVillage.bowsLevel,
            pikesLevel = otherVillage.pikesLevel,
            swordsLevel = otherVillage.swordsLevel,
            armourLevel = otherVillage.armourLevel,
            catapultsLevel = otherVillage.catapultsLevel,
            furnitureLevel = otherVillage.furnitureLevel,
            clothesLevel = otherVillage.clothesLevel,
            saltLevel = otherVillage.saltLevel,
            venisonLevel = otherVillage.venisonLevel,
            silkLevel = otherVillage.silkLevel,
            spicesLevel = otherVillage.spicesLevel,
            metalwareLevel = otherVillage.metalwareLevel,
            wineLevel = otherVillage.wineLevel
          };
      }
      this.selectHighlightLine(this.getLineFromResource(this.currentResource));
      this.updateValues();
    }

    public void dirtyStockExchangeInfo(int selectedStockExchange)
    {
      if (this.stockExchanges[selectedStockExchange] == null)
        return;
      ((StockExchangePanel.StockExchangeInfo) this.stockExchanges[selectedStockExchange]).lastTime = DateTime.MinValue;
    }

    public void tradingClick() => InterfaceMgr.Instance.setVillageTabSubMode(2);

    public void newVillageLoaded() => this.mainBackgroundImage.invalidate();

    public class ClosestCapitalSortItem
    {
      public int villageID = -1;
      public int distance;
    }

    public class StockExchangeInfo
    {
      public DateTime lastTime = DateTime.MinValue;
      public int villageID = -1;
      public int woodLevel;
      public int stoneLevel;
      public int ironLevel;
      public int pitchLevel;
      public int aleLevel;
      public int applesLevel;
      public int breadLevel;
      public int meatLevel;
      public int cheeseLevel;
      public int vegLevel;
      public int fishLevel;
      public int bowsLevel;
      public int pikesLevel;
      public int swordsLevel;
      public int armourLevel;
      public int catapultsLevel;
      public int furnitureLevel;
      public int clothesLevel;
      public int saltLevel;
      public int venisonLevel;
      public int silkLevel;
      public int spicesLevel;
      public int metalwareLevel;
      public int wineLevel;

      public int getLevel(int resource)
      {
        switch (resource)
        {
          case 6:
            return this.woodLevel;
          case 7:
            return this.stoneLevel;
          case 8:
            return this.ironLevel;
          case 9:
            return this.pitchLevel;
          case 12:
            return this.aleLevel;
          case 13:
            return this.applesLevel;
          case 14:
            return this.breadLevel;
          case 15:
            return this.vegLevel;
          case 16:
            return this.meatLevel;
          case 17:
            return this.cheeseLevel;
          case 18:
            return this.fishLevel;
          case 19:
            return this.clothesLevel;
          case 21:
            return this.furnitureLevel;
          case 22:
            return this.venisonLevel;
          case 23:
            return this.saltLevel;
          case 24:
            return this.spicesLevel;
          case 25:
            return this.silkLevel;
          case 26:
            return this.metalwareLevel;
          case 28:
            return this.pikesLevel;
          case 29:
            return this.bowsLevel;
          case 30:
            return this.swordsLevel;
          case 31:
            return this.armourLevel;
          case 32:
            return this.catapultsLevel;
          case 33:
            return this.wineLevel;
          default:
            return 0;
        }
      }

      public int getFakeLevel(int resource) => this.getLevel(resource);
    }
  }
}
