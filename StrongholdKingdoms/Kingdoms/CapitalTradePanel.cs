// Decompiled with JetBrains decompiler
// Type: Kingdoms.CapitalTradePanel
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
  public class CapitalTradePanel : CustomSelfDrawPanel, IDockableControl
  {
    public static CapitalTradePanel instance = (CapitalTradePanel) null;
    private static List<WorldMap.VillageNameItem> exchangeHistory = new List<WorldMap.VillageNameItem>();
    private static List<WorldMap.VillageNameItem> exchangeFavourites = new List<WorldMap.VillageNameItem>();
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage stockExchangeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel stockExchangeLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel leftWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel midWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel buyHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage buyHeadingImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDExtendingPanel buyWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel buySubWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel buyNumber = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel buyCostLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel buyCostValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel buyTaxLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel buyMin = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel buyMax = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton buyButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDTrackBar buyTrack = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDButton sendEditButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel localHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel storedHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel BuyPriceHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDExtendingPanel lightArea1 = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel lightArea2 = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel lightArea3 = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDButton tabButton1 = new CustomSelfDrawPanel.CSDButton();
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
    private CustomSelfDrawPanel.CSDButton worldMapButton = new CustomSelfDrawPanel.CSDButton();
    private int selectedStockExchange = -1;
    private int lastTab = -1;
    private int currentResource = -1;
    private int currentResourcePacketSize = 1;
    private int BACKUP_resource = -1;
    private int BACKUP_buyLevel;
    public SparseArray stockExchanges = new SparseArray();
    private DockableControl dockableControl;
    private IContainer components;

    public static List<WorldMap.VillageNameItem> ExchangeHistory
    {
      get => CapitalTradePanel.exchangeHistory;
    }

    public static List<WorldMap.VillageNameItem> ExchangeFavourites
    {
      get => CapitalTradePanel.exchangeFavourites;
    }

    public static void addHistory(GenericVillageHistoryData[] newData)
    {
      CapitalTradePanel.exchangeHistory.Clear();
      if (newData == null)
        return;
      foreach (GenericVillageHistoryData villageHistoryData in newData)
      {
        WorldMap.VillageNameItem villageNameItem = new WorldMap.VillageNameItem();
        if (GameEngine.Instance.World.isCapital(villageHistoryData.villageID))
        {
          villageNameItem.villageID = villageHistoryData.villageID;
          CapitalTradePanel.exchangeHistory.Add(villageNameItem);
        }
      }
    }

    public static void addFavourites(GenericVillageHistoryData[] newData)
    {
      CapitalTradePanel.exchangeFavourites.Clear();
      if (newData == null)
        return;
      foreach (GenericVillageHistoryData villageHistoryData in newData)
      {
        WorldMap.VillageNameItem villageNameItem = new WorldMap.VillageNameItem();
        if (GameEngine.Instance.World.isCapital(villageHistoryData.villageID))
        {
          villageNameItem.villageID = villageHistoryData.villageID;
          CapitalTradePanel.exchangeFavourites.Add(villageNameItem);
        }
      }
    }

    public CapitalTradePanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init()
    {
      CapitalTradePanel.instance = this;
      this.clearControls();
      int num = 70;
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.mainBackgroundArea.Position = new Point(0, 0);
      this.mainBackgroundArea.Size = new Size(992, 566);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea);
      this.stockExchangeLabel.Text = SK.Text("CapitalTradePanel_", "Purchase Goods");
      this.stockExchangeLabel.Color = Color.FromArgb(224, 203, 146);
      this.stockExchangeLabel.DropShadowColor = Color.FromArgb(74, 67, 48);
      this.stockExchangeLabel.Position = new Point(9, 9);
      this.stockExchangeLabel.Size = new Size(992, 50);
      this.stockExchangeLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.stockExchangeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.stockExchangeLabel);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(948, 10);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CapitalTradePanel_close");
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea, 12, new Point(898, 10));
      this.midWindow.Size = new Size(228, 449 - num - 150);
      this.midWindow.Position = new Point(375, 124);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.midWindow);
      this.midWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
      this.leftWindow.Size = new Size(335, 449 - num - 150);
      this.leftWindow.Position = new Point(45, 124);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.leftWindow);
      this.leftWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
      this.lightArea1.Size = new Size(97, 179);
      this.lightArea1.Position = new Point(216, 102 - num);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.lightArea1);
      this.lightArea1.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
      this.localHeadingLabel.Text = SK.Text("TRADE_Local", "Local");
      this.localHeadingLabel.Color = Color.FromArgb(196, 161, 85 - num);
      this.localHeadingLabel.Position = new Point(0, -35);
      this.localHeadingLabel.Size = new Size(97, 30);
      this.localHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.localHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
      this.lightArea1.addControl((CustomSelfDrawPanel.CSDControl) this.localHeadingLabel);
      this.lightArea2.Size = new Size(97, 179);
      this.lightArea2.Position = new Point(21, 102 - num);
      this.midWindow.addControl((CustomSelfDrawPanel.CSDControl) this.lightArea2);
      this.lightArea2.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
      this.storedHeadingLabel.Text = SK.Text("TRADE_At_Exchange", "At Exchange");
      this.storedHeadingLabel.Color = Color.FromArgb(196, 161, 85 - num);
      this.storedHeadingLabel.Position = new Point(0, -35);
      this.storedHeadingLabel.Size = new Size(97, 30);
      this.storedHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.storedHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
      this.lightArea2.addControl((CustomSelfDrawPanel.CSDControl) this.storedHeadingLabel);
      this.lightArea3.Size = new Size(77, 179);
      this.lightArea3.Position = new Point(129, 102 - num);
      this.midWindow.addControl((CustomSelfDrawPanel.CSDControl) this.lightArea3);
      this.lightArea3.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
      this.BuyPriceHeadingLabel.Text = SK.Text("CapitalTradePanel_Buy_Price", "Buy Price");
      this.BuyPriceHeadingLabel.Color = Color.FromArgb(196, 161, 85 - num);
      this.BuyPriceHeadingLabel.Position = new Point(-30, -35);
      this.BuyPriceHeadingLabel.Size = new Size(137, 30);
      this.BuyPriceHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.BuyPriceHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
      this.lightArea3.addControl((CustomSelfDrawPanel.CSDControl) this.BuyPriceHeadingLabel);
      this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_normal;
      this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_over;
      this.tabButton1.Position = new Point(2, -13);
      this.tabButton1.MoveOnClick = false;
      this.tabButton1.Data = 1;
      this.tabButton1.Visible = false;
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.tabButton1);
      this.buyWindow.Size = new Size(336, 145);
      this.buyWindow.Position = new Point(627, 166);
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.buyWindow);
      this.buyWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
      this.buySubWindow.Size = new Size(147, 50);
      this.buySubWindow.Position = new Point(178, 32);
      this.buyWindow.addControl((CustomSelfDrawPanel.CSDControl) this.buySubWindow);
      this.buySubWindow.Create((Image) GFXLibrary.int_insetpanel_b_top_left, (Image) GFXLibrary.int_insetpanel_b_middle_top, (Image) GFXLibrary.int_insetpanel_b_top_right, (Image) GFXLibrary.int_insetpanel_b_middle_left, (Image) GFXLibrary.int_insetpanel_b_middle, (Image) GFXLibrary.int_insetpanel_b_middle_right, (Image) GFXLibrary.int_insetpanel_b_bottom_left, (Image) GFXLibrary.int_insetpanel_b_middle_bottom, (Image) GFXLibrary.int_insetpanel_b_bottom_right);
      this.buyHeadingLabel.Text = SK.Text("CapitalTradePanel_Buy", "Buy") + " ";
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
      this.sendEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
      this.sendEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
      this.sendEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
      this.sendEditButton.MoveOnClick = true;
      this.sendEditButton.OverBrighten = true;
      this.sendEditButton.Position = new Point(7, 5);
      this.sendEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "CapitalTradePanel_editValue");
      this.buySubWindow.addControl((CustomSelfDrawPanel.CSDControl) this.sendEditButton);
      this.buyButton.Position = new Point(177, 94);
      this.buyButton.Size = new Size(153, 38);
      this.buyButton.Text.Text = SK.Text("CapitalTradePanel_Buy", "Buy");
      this.buyButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.buyButton.TextYOffset = -1;
      this.buyButton.Text.Color = ARGBColors.Black;
      this.buyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buyClick), "CapitalTradePanel_buy");
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
      this.highlightLine1.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine1.Position = new Point(153, 111 - num + 5);
      this.highlightLine1.Size = new Size(400, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine1);
      this.highlightLine2.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine2.Position = new Point(153, 111 - num + 5 + 40);
      this.highlightLine2.Size = new Size(400, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine2);
      this.highlightLine3.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine3.Position = new Point(153, 111 - num + 5 + 80);
      this.highlightLine3.Size = new Size(400, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine3);
      this.highlightLine4.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine4.Position = new Point(153, 111 - num + 5 + 120);
      this.highlightLine4.Size = new Size(400, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine4);
      this.highlightLine5.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine5.Position = new Point(153, 271);
      this.highlightLine5.Size = new Size(400, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine5);
      this.highlightLine6.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine6.Position = new Point(153, 311);
      this.highlightLine6.Size = new Size(400, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine6);
      this.highlightLine7.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine7.Position = new Point(153, 351);
      this.highlightLine7.Size = new Size(400, 31);
      this.leftWindow.addControl((CustomSelfDrawPanel.CSDControl) this.highlightLine7);
      this.highlightLine8.Image = (Image) GFXLibrary.int_white_highlight_bar;
      this.highlightLine8.Position = new Point(153, 391);
      this.highlightLine8.Size = new Size(400, 31);
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
      this.lastTab = -1;
      this.manageTabs(1);
      if (this.selectedStockExchange >= 0)
      {
        this.resetBackupData();
        this.selectStockExchange(this.selectedStockExchange);
        this.selectHighlightLine(0);
      }
      this.update();
    }

    public void update() => this.updateValues();

    private void tabClicked()
    {
      if (this.ClickedControl == null)
        return;
      CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) this.ClickedControl;
      if (clickedControl.Data == this.lastTab)
        return;
      this.manageTabs(clickedControl.Data);
    }

    private void manageTabs(int tabID)
    {
      if (tabID == this.lastTab)
        return;
      this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_normal;
      this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_over;
      if (tabID == 1)
      {
        this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_selected;
        this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_selected;
        this.selectHighlightLine(0);
        this.initStockpileTab();
        this.selectHighlightLine(0);
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
      rowHighlight.Size = new Size(400, 31);
      this.currentResourcePacketSize = GameEngine.Instance.LocalWorldData.traderCarryingLevels[this.currentResource];
      this.buyHeadingLabel.Text = SK.Text("CapitalTradePanel_Buy", "Buy") + " : " + VillageBuildingsData.getResourceNames(this.currentResource);
      this.buyHeadingImage.Image = (Image) GFXLibrary.getCommodity64DSImage(this.currentResource);
      this.buyTrack.Max = 50000;
      this.buyTrack.Value = 50000;
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
      this.setRowInfo(4, 12);
    }

    private void initGranaryTab()
    {
    }

    private void initArmouryTab()
    {
    }

    private void initHallTab()
    {
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
      GameEngine.Instance.playInterfaceSound("CapitalTradePanel_line_clicked");
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
      rowButton.Text2.Text = traderCarryingLevel.ToString("N", (IFormatProvider) nfi);
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
      if (priceValue < 0)
        return;
      rowPrice.Text = priceValue.ToString("N", (IFormatProvider) nfi);
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

    private CustomSelfDrawPanel.CSDButton getVillageHistory(int line)
    {
      return (CustomSelfDrawPanel.CSDButton) null;
    }

    public void updateValues()
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
      {
        if (this.selectedStockExchange < 0)
          this.selectStockExchange(village.VillageID);
        CapitalTradePanel.StockExchangeInfo stockExchangeInfo = (CapitalTradePanel.StockExchangeInfo) null;
        if (this.selectedStockExchange >= 0 && this.stockExchanges[this.selectedStockExchange] != null)
          stockExchangeInfo = (CapitalTradePanel.StockExchangeInfo) this.stockExchanges[this.selectedStockExchange];
        WorldData localWorldData = GameEngine.Instance.LocalWorldData;
        if (this.lastTab == 1)
        {
          VillageMap.StockpileLevels levels1 = new VillageMap.StockpileLevels();
          village.getStockpileLevels(levels1);
          VillageMap.InnLevels levels2 = new VillageMap.InnLevels();
          village.getInnLevels(levels2);
          if (stockExchangeInfo == null)
          {
            this.setRowValues(0, (int) levels1.woodLevel, -1, -1);
            this.setRowValues(1, (int) levels1.stoneLevel, -1, -1);
            this.setRowValues(2, (int) levels1.ironLevel, -1, -1);
            this.setRowValues(3, (int) levels1.pitchLevel, -1, -1);
          }
          else
          {
            this.setRowValues(0, (int) levels1.woodLevel, stockExchangeInfo.woodLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(6), 6));
            this.setRowValues(1, (int) levels1.stoneLevel, stockExchangeInfo.stoneLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(7), 7));
            this.setRowValues(2, (int) levels1.ironLevel, stockExchangeInfo.ironLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(8), 8));
            this.setRowValues(3, (int) levels1.pitchLevel, stockExchangeInfo.pitchLevel, TradingCalcs.calcSellCost(localWorldData, stockExchangeInfo.getFakeLevel(9), 9));
          }
        }
      }
      else
      {
        for (int row = 0; row < 8; ++row)
          this.setRowValues(row, -1, -1, -1);
      }
      this.showBuySellWindow();
    }

    private void showBuySellWindow()
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      bool visible = this.buyWindow.Visible;
      this.buyWindow.Visible = false;
      VillageMap village = GameEngine.Instance.Village;
      if (village != null && this.currentResource >= 0 && GameEngine.Instance.World.isUserVillage(village.VillageID) && this.selectedStockExchange >= 0 && this.stockExchanges[this.selectedStockExchange] != null)
      {
        WorldData localWorldData = GameEngine.Instance.LocalWorldData;
        CapitalTradePanel.StockExchangeInfo stockExchange = (CapitalTradePanel.StockExchangeInfo) this.stockExchanges[this.selectedStockExchange];
        village.getResourceLevel(this.currentResource);
        int level = stockExchange.getLevel(this.currentResource);
        int num1 = TradingCalcs.calcGoldCost(localWorldData, level, this.currentResource, level + this.buyTrack.Value);
        if (level > 0)
        {
          this.buyWindow.Visible = true;
          int currentGold = (int) GameEngine.Instance.World.getCurrentGold();
          int num2 = level;
          int num3 = currentGold / num1;
          int max = this.buyTrack.Max;
          if (num2 > max)
            this.buyTrack.Max = num2;
          else if (num2 < max)
          {
            if (this.buyTrack.Value > num2)
              this.buyTrack.Value = num2;
            this.buyTrack.Max = num2;
          }
          int num4 = this.buyTrack.Value * num1 / this.currentResourcePacketSize;
          if (num4 <= 0 && this.buyTrack.Value > 0)
            num4 = 1;
          this.buyCostValue.Text = num4.ToString("N", (IFormatProvider) nfi);
          this.buyNumber.Text = this.buyTrack.Value.ToString("N", (IFormatProvider) nfi);
          this.buyMax.Text = this.buyTrack.Max.ToString("N", (IFormatProvider) nfi);
        }
      }
      this.validateBuySellButtons();
      if (visible == this.buyWindow.Visible)
        return;
      this.mainBackgroundImage.invalidate();
    }

    private void validateBuySellButtons()
    {
      if (this.buyWindow.Visible && this.buyTrack.Value > 0)
        this.buyButton.Enabled = true;
      else
        this.buyButton.Enabled = false;
    }

    private void findOnWorldClicked()
    {
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      this.BACKUP_resource = this.currentResource;
      this.BACKUP_buyLevel = this.buyTrack.Value;
      GameEngine.Instance.World.zoomToVillage(village.VillageID);
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(4);
      InterfaceMgr.Instance.StockExchangeBuyingVillage = village.VillageID;
    }

    private void tracksMoved()
    {
      this.showBuySellWindow();
      this.buyWindow.invalidate();
    }

    public void resetBackupData()
    {
      this.BACKUP_resource = -1;
      this.BACKUP_buyLevel = 50000;
    }

    public void selectStockExchange(int villageID)
    {
      if (villageID < 0)
      {
        this.selectedStockExchange = -1;
      }
      else
      {
        this.selectedStockExchange = villageID;
        RemoteServices.Instance.set_GetStockExchangeData_UserCallBack(new RemoteServices.GetStockExchangeData_UserCallBack(this.getStockExchangeDataCallback));
        RemoteServices.Instance.GetStockExchangeData(villageID, true);
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
            case 12:
              this.manageTabs(1);
              break;
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
          this.buyTrack.Max = 500000;
          this.buyTrack.Value = this.BACKUP_buyLevel;
        }
        this.updateValues();
      }
    }

    public void closeClick() => InterfaceMgr.Instance.setVillageTabSubMode(-1);

    private void floatingValueCB(int value)
    {
      this.buyTrack.Value = value;
      this.updateValues();
    }

    private void editSendValue()
    {
      InterfaceMgr.Instance.setFloatingValueSentDelegate(new InterfaceMgr.FloatingValueSent(this.floatingValueCB));
      Point screen = InterfaceMgr.Instance.ParentForm.PointToScreen(new Point(620 + this.Location.X + 217, 254 + this.Location.Y + 120 - 50));
      FloatingInput.open(screen.X, screen.Y, this.buyTrack.Value, this.buyTrack.Max, InterfaceMgr.Instance.ParentForm);
    }

    private void buyClick()
    {
      VillageMap village = GameEngine.Instance.Village;
      village?.stockExchangeTrade(village.VillageID, this.currentResource, this.buyTrack.Value, true);
    }

    public void getStockExchangeDataCallback(GetStockExchangeData_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.stockExchanges[returnData.villageID] = (object) new CapitalTradePanel.StockExchangeInfo()
      {
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
      this.selectHighlightLine(this.getLineFromResource(this.currentResource));
      this.updateValues();
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
      this.MaximumSize = new Size(992, 566);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (CapitalTradePanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public class StockExchangeInfo
    {
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
