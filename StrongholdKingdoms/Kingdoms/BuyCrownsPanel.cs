// Decompiled with JetBrains decompiler
// Type: Kingdoms.BuyCrownsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class BuyCrownsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
  {
    private IContainer components;
    private string strOrderNow = SK.Text("BuyCrownsPanel_Order_Now", "Order Now");
    private string strCrowns = SK.Text("BuyCrownsPanel_Crowns", "Crowns");
    private string PlayerCountry;
    private string PlayerCurrency;
    private string PlayerLanguage;
    private int storedAeriaPoints;
    private CustomSelfDrawPanel.UICardsButtons cardButtons;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelBottom = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelPoints = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage APImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDFill greyout = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton buyAPButton = new CustomSelfDrawPanel.CSDButton();
    private Dictionary<string, UICardPack> packControls = new Dictionary<string, UICardPack>();
    private int currentCardSection = -1;
    private static int BorderPadding = 16;
    private int ContentWidth;
    private int AvailablePanelWidth;
    private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
    private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();

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

    public BuyCrownsPanel()
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
      this.ContentWidth = this.Width - 2 * BuyCrownsPanel.BorderPadding;
      this.AvailablePanelWidth = 800;
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
      int width1 = this.Width;
      int borderPadding = BuyCrownsPanel.BorderPadding;
      int width2 = this.AvailablePanel.Width;
      this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      this.closeImage.Size = this.closeImage.Image.Size;
      this.closeImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
      this.closeImage.Position = new Point(this.Width - 14 - 17, 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeImage);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 38, new Point(this.Width - 1 - 17 - 50 + 3, 5), true);
      CustomSelfDrawPanel.CSDFill control4 = new CustomSelfDrawPanel.CSDFill();
      control4.FillColor = Color.FromArgb((int) byte.MaxValue, 130, 129, 126);
      control4.Size = new Size(this.Width - 10, 1);
      control4.Position = new Point(5, 34);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control4);
      this.greyout.FillColor = Color.FromArgb(215, 25, 25, 25);
      this.greyout.Size = new Size(this.mainBackgroundImage.Width, this.AvailablePanel.Y + this.AvailablePanel.Height);
      this.greyout.Position = new Point(0, 0);
      this.greyout.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => { }));
      CustomSelfDrawPanel.CSDImage closeGrey = new CustomSelfDrawPanel.CSDImage();
      closeGrey.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      closeGrey.Size = this.closeImage.Image.Size;
      closeGrey.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => closeGrey.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => closeGrey.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      closeGrey.Position = new Point(this.Width - 14 - 17, 10);
      this.greyout.addControl((CustomSelfDrawPanel.CSDControl) closeGrey);
      this.labelTitle.Position = new Point(27, 8);
      this.labelTitle.Size = new Size(935, 64);
      this.labelTitle.Text = SK.Text("BuyCrownsPanel_Buy_Crowns", "Buy Crowns");
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
      CustomSelfDrawPanel.UICardsButtons control5 = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) this.ParentForm);
      control5.Position = new Point(808, 37);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control5);
      this.cardButtons = control5;
      List<ProductInfo> productInfoList = new List<ProductInfo>();
      if (Program.steamActive)
      {
        this.PlayerCountry = "UK";
        this.PlayerCurrency = "GBP";
        this.PlayerLanguage = MySettings.load().LanguageIdent;
        productInfoList = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).SteamGetProductList((IAuthRequest) new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", (string) null, (string) null, (string) null, (string) null)
        {
          SteamID = Program.steamID,
          SessionID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""),
          Culture = this.PlayerLanguage,
          Currency = this.PlayerCurrency,
          Country = this.PlayerCountry
        }, (AuthEndResponseDelegate) null, (Control) this, 15000).ProductList;
      }
      else if (Program.aeriaInstall)
      {
        XmlRpcAuthProvider forEndpoint = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
        XmlRpcAuthRequest req = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", (string) null, (string) null, (string) null, (string) null);
        XmlRpcAuthResponse response = (XmlRpcAuthResponse) null;
        this.storedAeriaPoints = forEndpoint.AeriaGetBalance((IAuthRequest) req, (AuthEndResponseDelegate) null, (Control) this, 15000, ref response);
        productInfoList = response.ProductList;
        this.buyAPButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
        this.buyAPButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
        this.buyAPButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
        this.buyAPButton.Position = new Point(317, 73);
        this.buyAPButton.Text.Text = this.storedAeriaPoints.ToString();
        this.buyAPButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.buyAPButton.Text.Size = new Size(this.buyAPButton.Width / 2 - 3, this.buyAPButton.Height);
        this.buyAPButton.TextYOffset = -2;
        this.buyAPButton.Text.Color = ARGBColors.Black;
        this.buyAPButton.ImageIcon = (Image) GFXLibrary.aeriaPoints;
        this.buyAPButton.ImageIconPosition = new Point(this.buyAPButton.Width / 2 + 3, 1);
        this.buyAPButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.buyAPButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.purchaseAP));
        this.buyAPButton.CustomTooltipID = 10350;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.buyAPButton);
      }
      int y = 66;
      int num1 = 94;
      int num2 = -1;
      if (Program.aeriaInstall)
        y = 132;
      NumberFormatInfo nfi = GameEngine.NFI;
      NumberFormatInfo nfiD2 = GameEngine.NFI_D2;
      foreach (ProductInfo productInfo in productInfoList)
      {
        ++num2;
        int x1 = num1;
        if (Program.steamActive)
        {
          if (num2 > 3)
          {
            if (num2 == 4)
              y = 132;
            x1 += 350;
          }
        }
        else if (Program.aeriaInstall && num2 > 2)
        {
          if (num2 == 3)
            y = 132;
          x1 += 350;
        }
        CustomSelfDrawPanel.CSDLabel control6 = new CustomSelfDrawPanel.CSDLabel();
        CustomSelfDrawPanel.CSDLabel control7 = new CustomSelfDrawPanel.CSDLabel();
        CustomSelfDrawPanel.CSDLabel control8 = new CustomSelfDrawPanel.CSDLabel();
        CustomSelfDrawPanel.CSDLabel control9 = new CustomSelfDrawPanel.CSDLabel();
        CustomSelfDrawPanel.CSDLabel control10 = new CustomSelfDrawPanel.CSDLabel();
        CustomSelfDrawPanel.CSDImage control11 = new CustomSelfDrawPanel.CSDImage();
        CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
        CustomSelfDrawPanel.CSDImage orderbutton = new CustomSelfDrawPanel.CSDImage();
        crownsbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_crowns_normal;
        crownsbutton.Position = new Point(x1, y);
        crownsbutton.Height = crownsbutton.Image.Height;
        crownsbutton.Width = crownsbutton.Image.Width;
        crownsbutton.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => crownsbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_crowns_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => crownsbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_crowns_normal));
        crownsbutton.Tag = (object) productInfo;
        crownsbutton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) crownsbutton);
        int x2 = x1 + (crownsbutton.Width + 32);
        orderbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_greywhite_normal;
        orderbutton.Position = new Point(x2, y + 18 + 3);
        orderbutton.Height = orderbutton.Image.Height;
        orderbutton.Width = orderbutton.Image.Width;
        orderbutton.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => orderbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_greywhite_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => orderbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_greywhite_normal));
        orderbutton.Tag = (object) productInfo;
        orderbutton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
        control10.Text = this.strOrderNow;
        control10.Position = new Point(0, 0);
        control10.Width = orderbutton.Width;
        control10.Height = orderbutton.Height;
        control10.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        control10.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
        orderbutton.addControl((CustomSelfDrawPanel.CSDControl) control10);
        control10.Tag = (object) productInfo;
        control10.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
        int pointSize = 14;
        int num3 = 0;
        if (Program.mySettings.LanguageIdent == "fr")
        {
          pointSize = 13;
          num3 = -5;
        }
        control6.Text = productInfo.Strikethrough.ToString();
        control6.Position = new Point(116 + num3, 21);
        control6.Width = 300;
        control6.Height = 24;
        control6.Font = FontManager.GetFont("Arial", (float) pointSize, FontStyle.Strikeout);
        control6.Color = ARGBColors.Black;
        crownsbutton.addControl((CustomSelfDrawPanel.CSDControl) control6);
        control6.Tag = (object) productInfo;
        control6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
        control6.Size = control6.TextSizeX;
        control7.Text = productInfo.Crowns.ToString();
        control7.Position = new Point(control6.X + control6.Width, control6.Y);
        control7.Font = FontManager.GetFont("Arial", (float) pointSize, FontStyle.Bold);
        control7.Color = ARGBColors.Purple;
        control7.Width = 300;
        control7.Height = 24;
        crownsbutton.addControl((CustomSelfDrawPanel.CSDControl) control7);
        control7.Tag = (object) productInfo;
        control7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
        control7.Size = control7.TextSizeX;
        control8.Text = this.strCrowns;
        control8.Position = new Point(control7.X + control7.Width + num3, control7.Y);
        control8.Font = FontManager.GetFont("Arial", (float) pointSize, FontStyle.Bold);
        control8.Color = ARGBColors.Black;
        control8.Size = new Size(300, 24);
        crownsbutton.addControl((CustomSelfDrawPanel.CSDControl) control8);
        control8.Tag = (object) productInfo;
        control8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
        control8.Size = control8.TextSizeX;
        control9.Text = !Program.aeriaInstall ? productInfo.Currency + " " + productInfo.Cost.ToString("F", (IFormatProvider) nfiD2) : " " + ((int) productInfo.Cost).ToString("F", (IFormatProvider) nfi);
        control9.Position = new Point(control6.X, control6.Y + control6.Height + 4);
        control9.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
        control9.Color = ARGBColors.Black;
        control9.Size = new Size(300, 24);
        crownsbutton.addControl((CustomSelfDrawPanel.CSDControl) control9);
        control9.Tag = (object) productInfo;
        control9.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
        control9.Size = control9.TextSizeX;
        if (Program.aeriaInstall)
        {
          control11.Image = (Image) GFXLibrary.aeriaPoints;
          control9.Position = new Point(control6.X + 20, control6.Y + control6.Height);
          control11.Position = new Point(control6.X, control6.Y + control6.Height + 4 - 2 - 3);
          control11.Tag = (object) productInfo;
          crownsbutton.addControl((CustomSelfDrawPanel.CSDControl) control11);
        }
        y += crownsbutton.Height + 40;
      }
      this.mainBackgroundImage.invalidate();
    }

    private void productclick()
    {
      if (Program.steamActive)
      {
        ProductInfo tag = (ProductInfo) this.ClickedControl.Tag;
        XmlRpcAuthProvider forEndpoint = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfilePath);
        XmlRpcAuthRequest req = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), (string) null, (string) null, (string) null);
        req.Culture = this.PlayerLanguage;
        req.Currency = this.PlayerCurrency;
        req.Country = this.PlayerCountry;
        req.SteamID = Program.steamID;
        req.ItemID = tag.ProductID.ToString();
        InterfaceMgr.Instance.closeAllPopups();
        XmlRpcAuthResponse xmlRpcAuthResponse = forEndpoint.SteamPaymentInit((IAuthRequest) req, (AuthEndResponseDelegate) null, (Control) this, 15000);
        int? successCode = xmlRpcAuthResponse.SuccessCode;
        if ((successCode.GetValueOrDefault() != 0 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
        {
          int num = (int) MessageBox.Show(xmlRpcAuthResponse.Message);
        }
        else
          Program.forceSteamDXOverlay();
      }
      else
      {
        if (!Program.aeriaInstall)
          return;
        ProductInfo tag = (ProductInfo) this.ClickedControl.Tag;
        if (MyMessageBox.Show(SK.Text("EmptyVillagePanel_Buy_Village", "Purchase") + Environment.NewLine + Environment.NewLine + tag.Crowns.ToString() + " " + SK.Text("BuyCrownsPanel_Crowns", "Crowns") + Environment.NewLine + tag.Cost.ToString() + " Aeria Points", SK.Text("ManageCandsPanel_Confirm_Purchase_Crowns", "Confirm Crowns Purchase"), MessageBoxButtons.YesNo) != DialogResult.Yes)
          return;
        int crowns = tag.Crowns;
        XmlRpcAuthResponse xmlRpcAuthResponse = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfilePath).AeriaMakePayment((IAuthRequest) new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), (string) null, (string) null, (string) null)
        {
          ItemID = crowns.ToString(),
          OrderID = tag.Cost.ToString()
        }, (AuthEndResponseDelegate) null, (Control) this, 15000);
        int? successCode = xmlRpcAuthResponse.SuccessCode;
        if ((successCode.GetValueOrDefault() != 0 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
        {
          if (xmlRpcAuthResponse.Message[0] == '2' && xmlRpcAuthResponse.Message[1] == '0' && xmlRpcAuthResponse.Message[2] == '5')
          {
            if (MyMessageBox.Show(SK.Text("ManageCandsPanel_Purchase_Failed_Buy_Points", "You don't have enough Aeria Points for this purchase. Do you wish to purchase Aeria Points now?"), SK.Text("ManageCandsPanel_Purchase_Failed", "Purchase Failed"), MessageBoxButtons.YesNo) != DialogResult.Yes)
              return;
            this.purchaseAP();
          }
          else
          {
            int num1 = (int) MessageBox.Show(xmlRpcAuthResponse.Message);
          }
        }
        else
        {
          GameEngine.Instance.World.ProfileCrowns += tag.Crowns;
          int num2 = (int) MyMessageBox.Show(SK.Text("ManageCandsPanel_Successful_Purchase", "Your purchase has been successfully completed"), SK.Text("ManageCandsPanel_Crowns_Purchased", "Crowns Purchased"));
          this.closeClick();
        }
      }
    }

    public void purchaseAP()
    {
      new Process()
      {
        StartInfo = {
          FileName = "https://billing.aeriagames.com/"
        }
      }.Start();
    }

    public void update()
    {
    }

    public void closeClick()
    {
      InterfaceMgr.Instance.closePlayCardsWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }
  }
}
