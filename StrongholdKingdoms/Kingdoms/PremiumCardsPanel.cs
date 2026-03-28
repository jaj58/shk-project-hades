// Decompiled with JetBrains decompiler
// Type: Kingdoms.PremiumCardsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PremiumCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
  {
    private CustomSelfDrawPanel.CSDHorzProgressBar ExpiryBar;
    private CustomSelfDrawPanel.UICardsButtons cardsButtons;
    private Image premiumTokenImage;
    private CustomSelfDrawPanel.CSDFill TimerOuter;
    private CustomSelfDrawPanel.CSDFill TimerInner;
    private List<CustomSelfDrawPanel.CSDImage> PremiumTokens = new List<CustomSelfDrawPanel.CSDImage>();
    private CustomSelfDrawPanel.CSDLabel PremiumTokensLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage PremiumInplayImage = new CustomSelfDrawPanel.CSDImage();
    private Size InnerBarDimentions = new Size(196, 12);
    private Size OuterBardimentions = new Size(200, 16);
    private CustomSelfDrawPanel.CSDLabel expiryLabel;
    private double currentExpirySeconds;
    private double maxExpirySeconds = 604800.0;
    private double expiryDays;
    private double expiryHours;
    private double expiryMinutes;
    private int lastminute;
    private static int expiryBarMax = 200;
    private int expiryBarCurrent;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private List<UICard> UICardList = new List<UICard>();
    private List<UICard> UICardListInplay = new List<UICard>();
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDFill greyout = new CustomSelfDrawPanel.CSDFill();
    private int currentCardSection = -1;
    private static int BorderPadding = 16;
    private int ContentWidth;
    private int AvailablePanelWidth;
    private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
    private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage InplayPanelContent = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollbarInplay = new CustomSelfDrawPanel.CSDVertScrollBar();
    private Bitmap greenbar = new Bitmap(29, 3);
    private bool inSend;
    private MyMessageBoxPopUp playPremiumPopup;
    private MyMessageBoxPopUp extendPremiumPopUp;
    private MyMessageBoxPopUp buyTokenPopUp;
    private bool buying;
    private IContainer components;

    public PremiumCardsPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int cardSection)
    {
      this.inSend = false;
      this.currentCardSection = cardSection;
      this.clearControls();
      this.mainBackgroundImage.Image = GFXLibrary.dummy;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.ContentWidth = this.Width - 2 * PremiumCardsPanel.BorderPadding;
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
      this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, 385);
      this.AvailablePanel.Position = new Point(8, this.Height - 8 - 385);
      this.AvailablePanel.Alpha = 0.8f;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanel);
      this.AvailablePanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
      int width1 = this.Width;
      int borderPadding = PremiumCardsPanel.BorderPadding;
      int width2 = this.AvailablePanel.Width;
      this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      this.closeImage.Size = this.closeImage.Image.Size;
      this.closeImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
      this.closeImage.Position = new Point(this.Width - 14 - 17, 10);
      this.closeImage.CustomTooltipID = 10100;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeImage);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 37, new Point(this.Width - 1 - 17 - 50 + 3, 5), true);
      CustomSelfDrawPanel.CSDFill control4 = new CustomSelfDrawPanel.CSDFill();
      control4.FillColor = Color.FromArgb((int) byte.MaxValue, 130, 129, 126);
      control4.Size = new Size(this.Width - 10, 1);
      control4.Position = new Point(5, 34);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control4);
      this.greyout.FillColor = Color.FromArgb(215, 25, 25, 25);
      this.greyout.Size = new Size(this.mainBackgroundImage.Width, this.mainBackgroundImage.Height);
      this.greyout.Position = new Point(0, 0);
      this.cardsButtons = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) this.ParentForm);
      this.cardsButtons.Position = new Point(808, 37);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardsButtons);
      this.labelTitle.Position = new Point(27, 8);
      this.labelTitle.Size = new Size(935, 64);
      this.labelTitle.Text = SK.Text("PremiumCardsPanel_Buy_and_Open_Packs", "Buy and Play Premium Tokens: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
      if (GameEngine.Instance.cardsManager.UserCardData.premiumCard == 4112)
        this.maxExpirySeconds = 604800.0;
      else if (GameEngine.Instance.cardsManager.UserCardData.premiumCard == 4114)
        this.maxExpirySeconds = 2592000.0;
      else if (GameEngine.Instance.cardsManager.UserCardData.premiumCard == 4113)
        this.maxExpirySeconds = 172800.0;
      else if (GameEngine.Instance.cardsManager.UserCardData.premiumCard == 4116)
        this.maxExpirySeconds = 0.0;
      this.currentExpirySeconds = GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
      this.expiryDays = this.currentExpirySeconds / 86400.0;
      this.expiryHours = this.currentExpirySeconds % 86400.0 / 3600.0;
      this.expiryMinutes = this.currentExpirySeconds % 3600.0 / 60.0;
      this.expiryBarCurrent = this.maxExpirySeconds <= 0.0 ? -1 : Convert.ToInt32(Math.Floor(this.currentExpirySeconds / this.maxExpirySeconds * (double) PremiumCardsPanel.expiryBarMax));
      this.premiumTokenImage = GameEngine.Instance.cardsManager.UserCardData.premiumCard <= 0 ? (Image) GFXLibrary.PremiumTokens[4112][0] : (Image) GFXLibrary.PremiumTokens[GameEngine.Instance.cardsManager.UserCardData.premiumCard][0];
      this.PremiumInplayImage.Visible = false;
      this.PremiumInplayImage.Image = this.premiumTokenImage;
      this.PremiumInplayImage.Size = this.premiumTokenImage.Size;
      this.PremiumInplayImage.Position = new Point(this.AvailablePanel.X + this.AvailablePanel.Width - 32 - this.PremiumInplayImage.Width, this.cardsButtons.Y + 8);
      this.PremiumInplayImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
      {
        if (GameEngine.Instance.cardsManager.UserCardData.premiumCard <= 0)
          return;
        this.PremiumInplayImage.Image = (Image) GFXLibrary.PremiumTokens[GameEngine.Instance.cardsManager.UserCardData.premiumCard][1];
      }), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
      {
        if (GameEngine.Instance.cardsManager.UserCardData.premiumCard <= 0)
          return;
        this.PremiumInplayImage.Image = (Image) GFXLibrary.PremiumTokens[GameEngine.Instance.cardsManager.UserCardData.premiumCard][0];
      }));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.PremiumInplayImage);
      if (this.expiryBarCurrent >= 0)
      {
        this.TimerOuter = new CustomSelfDrawPanel.CSDFill();
        this.TimerInner = new CustomSelfDrawPanel.CSDFill();
      }
      else
      {
        this.TimerInner = (CustomSelfDrawPanel.CSDFill) null;
        this.TimerOuter = (CustomSelfDrawPanel.CSDFill) null;
      }
      this.PremiumTokensLabel = new CustomSelfDrawPanel.CSDLabel();
      this.PremiumTokensLabel.Position = new Point(this.AvailablePanel.X + 32, this.AvailablePanel.Y - 24);
      this.PremiumTokensLabel.Size = new Size(450, 32);
      this.PremiumTokensLabel.Text = SK.Text("PremiumCardsPanel_Current_Tokens", "Current Premium Tokens") + " : " + GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Count.ToString() + (GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Count > 0 ? " (" + SK.Text("PremiumCardsPanel_Click_To_Play", "click one to play") + ")" : "");
      this.PremiumTokensLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.PremiumTokensLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.PremiumTokensLabel.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.PremiumTokensLabel);
      CustomSelfDrawPanel.CSDImage csdImage = new CustomSelfDrawPanel.CSDImage();
      BaseImage cardpanelPremiumAd = GFXLibrary.cardpanel_premium_ad;
      csdImage.Image = (Image) cardpanelPremiumAd;
      csdImage.Size = cardpanelPremiumAd.Size;
      csdImage.Position = new Point(0, 0);
      CustomSelfDrawPanel.CSDImage PremiumAdvert7 = new CustomSelfDrawPanel.CSDImage();
      CustomSelfDrawPanel.CSDImage PremiumAdvert30 = new CustomSelfDrawPanel.CSDImage();
      BaseImage AdImage7 = GFXLibrary.premiumAdvert7;
      BaseImage AdImage7_over = GFXLibrary.premiumAdvert7_over;
      BaseImage AdImage30 = GFXLibrary.premiumAdvert30;
      BaseImage AdImage30_over = GFXLibrary.premiumAdvert30_over;
      PremiumAdvert7.Image = (Image) AdImage7;
      PremiumAdvert7.Size = AdImage7.Size;
      PremiumAdvert7.Position = new Point(0, 0);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) PremiumAdvert7);
      PremiumAdvert30.Image = (Image) AdImage30;
      PremiumAdvert30.Size = AdImage30.Size;
      PremiumAdvert30.Position = new Point(363, 0);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) PremiumAdvert30);
      PremiumAdvert7.Data = 4112;
      PremiumAdvert30.Data = 4114;
      PremiumAdvert7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickedOffer), "PremiumCardsPanel_buy_premium");
      PremiumAdvert30.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickedOffer), "PremiumCardsPanel_buy_premium");
      PremiumAdvert7.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => PremiumAdvert7.Image = (Image) AdImage7_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => PremiumAdvert7.Image = (Image) AdImage7));
      PremiumAdvert30.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => PremiumAdvert30.Image = (Image) AdImage30_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => PremiumAdvert30.Image = (Image) AdImage30));
      csdImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickedOffer), "PremiumCardsPanel_buy_premium");
      csdImage.Data = 4112;
      CustomSelfDrawPanel.CSDLabel control5 = new CustomSelfDrawPanel.CSDLabel();
      control5.Position = new Point(0, csdImage.Height + 8);
      control5.Size = new Size(600, 32);
      control5.Text = SK.Text("PremiumCardsPanel_Benefits", "Premium Benefits");
      control5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control5.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      control5.Color = ARGBColors.Gold;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control5);
      CustomSelfDrawPanel.CSDLabel control6 = new CustomSelfDrawPanel.CSDLabel();
      control6.Position = new Point(0, csdImage.Height + 8 + control5.Height + 4);
      control6.Size = new Size(600, 30);
      control6.Text = SK.Text("PremiumCardsPanel_Building_Queue", "Building Queue");
      control6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control6.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control6.Color = ARGBColors.Goldenrod;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control6);
      CustomSelfDrawPanel.CSDLabel control7 = new CustomSelfDrawPanel.CSDLabel();
      control7.Position = new Point(110, control6.Y + control6.Height + 4 - 11);
      control7.Size = new Size(590, 50);
      control7.Text = SK.Text("PremiumCardsPanel_Buildings_Queue_Info", "This allows up to 5 buildings to be queued for construction in the village. You can also move all buildings within your village screen.");
      control7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control7.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control7.Color = ARGBColors.White;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control7);
      CustomSelfDrawPanel.CSDImage control8 = new CustomSelfDrawPanel.CSDImage();
      control8.Image = (Image) GFXLibrary.premiumIcons[0];
      control8.Position = new Point(4, control6.Y + control6.Height + 4 - 10);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control8);
      CustomSelfDrawPanel.CSDLabel control9 = new CustomSelfDrawPanel.CSDLabel();
      control9.Position = new Point(0, control7.Y + control7.Height + 4);
      control9.Size = new Size(600, 30);
      control9.Text = SK.Text("PremiumCardsPanel_Research_Queue", "Research Queue");
      control9.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control9.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control9.Color = ARGBColors.Goldenrod;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control9);
      CustomSelfDrawPanel.CSDLabel control10 = new CustomSelfDrawPanel.CSDLabel();
      control10.Position = new Point(110, control9.Y + control9.Height + 4 - 11);
      control10.Size = new Size(590, 50);
      control10.Text = SK.Text("PremiumCardsPanel_Research_Queue_Info", "This allows up to 5 researches to be queued in the research screen.");
      control10.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control10.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control10.Color = ARGBColors.White;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control10);
      CustomSelfDrawPanel.CSDImage control11 = new CustomSelfDrawPanel.CSDImage();
      control11.Image = (Image) GFXLibrary.premiumIcons[1];
      control11.Position = new Point(4, control9.Y + control9.Height + 4 - 10);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control11);
      CustomSelfDrawPanel.CSDLabel control12 = new CustomSelfDrawPanel.CSDLabel();
      control12.Position = new Point(0, control10.Y + control10.Height + 4);
      control12.Size = new Size(600, 30);
      control12.Text = SK.Text("PremiumCardsPanel_Auto_Trading", "Auto Trading");
      control12.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control12.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control12.Color = ARGBColors.Goldenrod;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control12);
      CustomSelfDrawPanel.CSDLabel control13 = new CustomSelfDrawPanel.CSDLabel();
      control13.Position = new Point(110, control12.Y + control12.Height + 4 - 11);
      control13.Size = new Size(590, 50);
      control13.Text = SK.Text("PremiumCardsPanel_Auto_Trading_Info", "This allows the trade one type of good to the parish capitals market automatically while you are logged out.") + " (" + SK.Text("PremiumCardsPanel_Auto_Extra", "Activates once every 2 to 4 hours.") + ")";
      control13.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control13.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control13.Color = ARGBColors.White;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control13);
      CustomSelfDrawPanel.CSDImage control14 = new CustomSelfDrawPanel.CSDImage();
      control14.Image = (Image) GFXLibrary.premiumIcons[2];
      control14.Position = new Point(4, control12.Y + control12.Height + 4 - 10);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control14);
      CustomSelfDrawPanel.CSDLabel control15 = new CustomSelfDrawPanel.CSDLabel();
      control15.Position = new Point(0, control13.Y + control13.Height + 4);
      control15.Size = new Size(600, 30);
      control15.Text = SK.Text("PremiumCardsPanel_Auto_Scouting", "Auto Scouting");
      control15.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control15.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control15.Color = ARGBColors.Goldenrod;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control15);
      CustomSelfDrawPanel.CSDLabel control16 = new CustomSelfDrawPanel.CSDLabel();
      control16.Position = new Point(110, control15.Y + control15.Height + 4 - 11);
      control16.Size = new Size(590, 50);
      control16.Text = SK.Text("PremiumCardsPanel_Auto_Scouting_Info", "This will send out all available scouts to stashes within the parish the village is located, automatically while you are logged out.") + " (" + SK.Text("PremiumCardsPanel_Auto_Extra", "Activates once every 2 to 4 hours.") + ")";
      control16.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control16.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control16.Color = ARGBColors.White;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control16);
      CustomSelfDrawPanel.CSDImage control17 = new CustomSelfDrawPanel.CSDImage();
      control17.Image = (Image) GFXLibrary.premiumIcons[3];
      control17.Position = new Point(4, control15.Y + control15.Height + 4 - 10);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control17);
      CustomSelfDrawPanel.CSDLabel control18 = new CustomSelfDrawPanel.CSDLabel();
      control18.Position = new Point(0, control16.Y + control16.Height + 4);
      control18.Size = new Size(600, 30);
      control18.Text = SK.Text("PremiumCardsPanel_Auto_Attacking", "Auto Attacking");
      control18.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control18.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control18.Color = ARGBColors.Goldenrod;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control18);
      CustomSelfDrawPanel.CSDLabel control19 = new CustomSelfDrawPanel.CSDLabel();
      control19.Position = new Point(110, control18.Y + control18.Height + 4 - 11);
      control19.Size = new Size(590, 50);
      control19.Text = SK.Text("PremiumCardsPanel_Auto_Attacking_Info", "This will send out attacks to chosen targets automatically while you are logged out.") + " (" + SK.Text("PremiumCardsPanel_Auto_Extra", "Activates once every 2 to 4 hours.") + ")";
      control19.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control19.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control19.Color = ARGBColors.White;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control19);
      CustomSelfDrawPanel.CSDImage control20 = new CustomSelfDrawPanel.CSDImage();
      control20.Image = (Image) GFXLibrary.premiumIcons[4];
      control20.Position = new Point(4, control18.Y + control18.Height + 4 - 10);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control20);
      CustomSelfDrawPanel.CSDLabel control21 = new CustomSelfDrawPanel.CSDLabel();
      control21.Position = new Point(0, control19.Y + control19.Height + 4);
      control21.Size = new Size(600, 30);
      control21.Text = SK.Text("PremiumCardsPanel_Auto_Recruit", "Auto Recruit");
      control21.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control21.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control21.Color = ARGBColors.Goldenrod;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control21);
      CustomSelfDrawPanel.CSDLabel control22 = new CustomSelfDrawPanel.CSDLabel();
      control22.Position = new Point(110, control21.Y + control21.Height + 4 - 11);
      control22.Size = new Size(590, 50);
      control22.Text = SK.Text("PremiumCardsPanel_Auto_Recruit_Info", "This will automatically conscript idle peasants to your army.") + " (" + SK.Text("PremiumCardsPanel_Auto_Extra", "Activates once every 2 to 4 hours.") + ")";
      control22.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control22.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control22.Color = ARGBColors.White;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control22);
      CustomSelfDrawPanel.CSDImage control23 = new CustomSelfDrawPanel.CSDImage();
      control23.Image = (Image) GFXLibrary.premiumIcons[5];
      control23.Position = new Point(4, control21.Y + control21.Height + 4 - 10);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control23);
      CustomSelfDrawPanel.CSDLabel control24 = new CustomSelfDrawPanel.CSDLabel();
      control24.Position = new Point(0, control22.Y + control22.Height + 4);
      control24.Size = new Size(600, 30);
      control24.Text = SK.Text("PremiumCardsPanel_Village_Overview", "Village Overview");
      control24.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control24.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control24.Color = ARGBColors.Goldenrod;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control24);
      CustomSelfDrawPanel.CSDLabel control25 = new CustomSelfDrawPanel.CSDLabel();
      control25.Position = new Point(110, control24.Y + control24.Height + 4 - 11);
      control25.Size = new Size(590, 50);
      control25.Text = SK.Text("PremiumCardsPanel_Village_Overview_Info", "This allows players to keep track of essential information on all their villages, such as income from taxes, housing capacity, popularity and more.");
      control25.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control25.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control25.Color = ARGBColors.White;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control25);
      CustomSelfDrawPanel.CSDImage control26 = new CustomSelfDrawPanel.CSDImage();
      control26.Image = (Image) GFXLibrary.premiumIcons[6];
      control26.Position = new Point(4, control24.Y + control24.Height + 4 - 10);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control26);
      CustomSelfDrawPanel.CSDLabel control27 = new CustomSelfDrawPanel.CSDLabel();
      control27.Position = new Point(0, control25.Y + control25.Height + 4);
      control27.Size = new Size(600, 30);
      control27.Text = SK.Text("PremiumCardsPanel_Vacation_Mode", "Vacation Mode");
      control27.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control27.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control27.Color = ARGBColors.Goldenrod;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control27);
      CustomSelfDrawPanel.CSDLabel control28 = new CustomSelfDrawPanel.CSDLabel();
      control28.Position = new Point(110, control27.Y + control27.Height + 4 - 11);
      control28.Size = new Size(590, 50);
      control28.Text = SK.Text("PremiumCardsPanel_Vacation_Mode_Info", "This allows players to protect their villages from attack for up to 15 days.");
      control28.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control28.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control28.Color = ARGBColors.White;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control28);
      CustomSelfDrawPanel.CSDImage control29 = new CustomSelfDrawPanel.CSDImage();
      control29.Image = (Image) GFXLibrary.premiumIcons[7];
      control29.Position = new Point(4, control27.Y + control27.Height + 4 - 10);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control29);
      CustomSelfDrawPanel.CSDLabel control30 = new CustomSelfDrawPanel.CSDLabel();
      control30.Position = new Point(0, control28.Y + control28.Height + 4);
      control30.Size = new Size(600, 30);
      control30.Text = SK.Text("PremiumCardsPanel_AdvancedTrading", "Advanced Trading Option");
      control30.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control30.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control30.Color = ARGBColors.Goldenrod;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control30);
      CustomSelfDrawPanel.CSDLabel control31 = new CustomSelfDrawPanel.CSDLabel();
      control31.Position = new Point(110, control30.Y + control30.Height + 4 - 11);
      control31.Size = new Size(590, 50);
      control31.Text = SK.Text("PremiumCardsPanel_AdvancedTrading_Info", "This allows players to find the best prices for goods in nearby Markets.");
      control31.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      control31.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control31.Color = ARGBColors.White;
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control31);
      CustomSelfDrawPanel.CSDImage control32 = new CustomSelfDrawPanel.CSDImage();
      control32.Image = (Image) GFXLibrary.premiumIcons[8];
      control32.Position = new Point(4, control30.Y + control30.Height + 4 - 10);
      this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) control32);
      int height = control31.Y + control31.Height + 6;
      this.AvailablePanelContent.Position = new Point(PremiumCardsPanel.BorderPadding, PremiumCardsPanel.BorderPadding);
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
      this.AvailablePanelContent.ClipRect = new Rectangle(0, 0, this.AvailablePanel.Width - PremiumCardsPanel.BorderPadding, this.AvailablePanel.Height - PremiumCardsPanel.BorderPadding * 2);
      this.AvailablePanel.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanelContent);
      if (height < this.AvailablePanelContent.ClipRect.Height)
        height = this.AvailablePanelContent.ClipRect.Height;
      this.scrollbarAvailable.Position = new Point(this.AvailablePanel.Width - PremiumCardsPanel.BorderPadding - PremiumCardsPanel.BorderPadding / 2, this.AvailablePanel.Y + PremiumCardsPanel.BorderPadding / 2);
      this.scrollbarAvailable.Size = new Size(PremiumCardsPanel.BorderPadding, this.AvailablePanel.Height - PremiumCardsPanel.BorderPadding);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollbarAvailable);
      this.scrollbarAvailable.Value = 0;
      this.scrollbarAvailable.StepSize = 200;
      this.scrollbarAvailable.Max = this.AvailablePanelContent.Height - this.AvailablePanelContent.ClipRect.Height;
      this.scrollbarAvailable.NumVisibleLines = this.AvailablePanelContent.ClipRect.Height;
      this.scrollbarAvailable.OffsetTL = new Point(1, 5);
      this.scrollbarAvailable.OffsetBR = new Point(0, -10);
      this.scrollbarAvailable.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.AvailableContentScroll));
      this.scrollbarAvailable.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.cardpanel_scroll_thumb_top, (Image) GFXLibrary.cardpanel_scroll_thumb_mid, (Image) GFXLibrary.cardpanel_scroll_thumb_botom);
      if (height <= this.AvailablePanelContent.ClipRect.Height)
        this.scrollbarAvailable.Visible = false;
      this.expiryLabel = new CustomSelfDrawPanel.CSDLabel();
      if (this.TimerInner != null)
      {
        this.ExpiryBar = new CustomSelfDrawPanel.CSDHorzProgressBar();
        this.ExpiryBar.Size = new Size(170, 0);
        this.ExpiryBar.Position = new Point(this.PremiumInplayImage.X - 13, this.PremiumInplayImage.Y + this.PremiumInplayImage.Height);
        this.ExpiryBar.Create((Image) GFXLibrary.cardpanel_prem_timer_back_left, (Image) GFXLibrary.cardpanel_prem_timer_back_mid, (Image) GFXLibrary.cardpanel_prem_timer_back_right, (Image) GFXLibrary.cardpanel_prem_timer_fill_left, (Image) GFXLibrary.cardpanel_prem_timer_fill_mid, (Image) GFXLibrary.cardpanel_prem_timer_fill_right);
        this.ExpiryBar.setValues(this.currentExpirySeconds, this.maxExpirySeconds);
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.ExpiryBar);
        this.expiryLabel.Position = new Point(this.ExpiryBar.X, this.ExpiryBar.Y + this.ExpiryBar.Height);
        this.expiryLabel.Size = new Size(this.ExpiryBar.Width, 16);
      }
      else
      {
        this.expiryLabel.Position = new Point(this.PremiumInplayImage.X - 13, this.PremiumInplayImage.Y + this.PremiumInplayImage.Height);
        this.expiryLabel.Size = new Size(170, 16);
      }
      this.expiryLabel.Visible = false;
      this.expiryLabel.Text = Math.Floor(this.expiryDays).ToString().PadLeft(2, '0') + ":" + Math.Floor(this.expiryHours).ToString().PadLeft(2, '0') + ":" + Math.Floor(this.expiryMinutes).ToString().PadLeft(2, '0') + " (" + SK.Text("PremiumCardsPanel_Day_Hour_Minute", "dd:hh:mm") + ")";
      this.expiryLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.expiryLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.expiryLabel.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.expiryLabel);
      this.UpdatePremiumTokens();
      this.UpdateExpiry();
    }

    private void ClickedToken()
    {
      if (GameEngine.Instance.World.WorldEnded || this.inSend)
        return;
      int data = this.ClickedControl.Data;
      int type = GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens[data].Type;
      DateTime currentServerTime = VillageMap.getCurrentServerTime();
      if (GameEngine.Instance.premiumTokenManager.PremiumInPlay)
      {
        this.currentExpirySeconds = GameEngine.Instance.cardsManager.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
        currentServerTime.AddSeconds(this.currentExpirySeconds);
        if (GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens[data].Type == 4113 && GameEngine.Instance.cardsManager.UserCardData.premiumCard == 4113)
        {
          int num = (int) MyMessageBox.Show(SK.Text("PremiumCardsPanel_Already_In_Play_2_2", "You cannot extend a 2 day Premium Token using another 2 day Premium Token."), SK.Text("GENERIC_Error", "Error"));
          return;
        }
        if (MyMessageBox.Show(SK.Text("PremiumCardsPanel_ExtendToken", "You currently have a Premium Token in play, do you wish to extend this by playing another Token?"), SK.Text("PremiumCardsPanel_ExtendWarning", "Extend Premium Token"), MessageBoxButtons.YesNo) != DialogResult.Yes)
          return;
      }
      else
      {
        string str = "";
        switch (GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens[data].Type)
        {
          case 4112:
            str = SK.Text("PremiumCardsPanel_7day", "7 Day Premium Token");
            break;
          case 4113:
            str = SK.Text("TOOLTIPS_QUEST_REWARD_PREMIUM_CARD", "2 Day Premium Token");
            break;
          case 4114:
            str = SK.Text("PremiumCardsPanel_30day", "30 Day Premium Token");
            break;
        }
        if (MyMessageBox.Show(str + Environment.NewLine + Environment.NewLine + SK.Text("PremiumCardsPanel_PlayToken", "You are about to play this Premium Token. This Premium Token will only affect the current game world.") + Environment.NewLine + Environment.NewLine + SK.Text("PremiumCardsPanel_PlayToken2", "Are you sure you wish to play this Token?"), SK.Text("PremiumCardsPanel_PlayToken_Header", "Play Premium Token"), MessageBoxButtons.YesNo) != DialogResult.Yes)
          return;
      }
      this.inSend = true;
      CardTypes.PremiumToken userToken = GameEngine.Instance.premiumTokenManager.getUserToken(data);
      GameEngine.Instance.premiumTokenManager.PlayToken(userToken, new CardsEndResponseDelegate(this.PlayedToken), (Control) this);
      if (InterfaceMgr.Instance.getCardWindow() != null)
        CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.getCardWindow());
      this.UpdatePremiumTokens();
      this.UpdateExpiry();
    }

    private void ExtendOrPlayPremiumToken()
    {
      try
      {
        this.inSend = true;
        int data = this.ClickedControl.Data;
        CardTypes.PremiumToken userToken = GameEngine.Instance.premiumTokenManager.getUserToken(data);
        GameEngine.Instance.premiumTokenManager.PlayToken(userToken, new CardsEndResponseDelegate(this.PlayedToken), (Control) this);
        if (InterfaceMgr.Instance.getCardWindow() != null)
          CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.getCardWindow());
        this.UpdatePremiumTokens();
        this.UpdateExpiry();
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log(ex.ToString());
      }
    }

    private void PlayPremiumToken()
    {
      this.ExtendOrPlayPremiumToken();
      this.playPremiumPopup.Close();
    }

    private void ClosePlayPremiumPopUp()
    {
      if (this.playPremiumPopup == null)
        return;
      if (this.playPremiumPopup.Created)
        this.playPremiumPopup.Close();
      this.playPremiumPopup = (MyMessageBoxPopUp) null;
    }

    private void ExtendPremiumToken()
    {
      this.ExtendOrPlayPremiumToken();
      this.extendPremiumPopUp.Close();
    }

    private void CloseExtendPremiumPopUp()
    {
      if (this.extendPremiumPopUp == null)
        return;
      if (this.extendPremiumPopUp.Created)
        this.extendPremiumPopUp.Close();
      this.extendPremiumPopUp = (MyMessageBoxPopUp) null;
    }

    private void BoughtTokenPopUp()
    {
      try
      {
        GameEngine.Instance.premiumTokenManager.buyToken(this.ClickedControl.Data, new CardsEndResponseDelegate(this.BoughtOffer), (Control) this);
        this.labelTitle.Text = SK.Text("PremiumCardsPanel_Buy_and_Open_Packs", "Buy and Play Premium Tokens: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log(ex.ToString());
      }
      this.CloseBuyTokenPopUp();
    }

    private void CloseBuyTokenPopUp()
    {
      if (this.buyTokenPopUp == null)
        return;
      if (this.buyTokenPopUp.Created)
        this.buyTokenPopUp.Close();
      this.buyTokenPopUp = (MyMessageBoxPopUp) null;
    }

    private void PlayedToken(ICardsProvider provider, ICardsResponse response)
    {
      this.inSend = false;
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 1 : (!successCode.HasValue ? 1 : 0)) != 0)
      {
        int num = (int) MyMessageBox.Show(CardsManager.translateCardError(response.Message, 0), SK.Text("GENERIC_Error", "Error"));
      }
      this.UpdatePremiumTokens();
      this.UpdateExpiry();
      if (InterfaceMgr.Instance.getCardWindow() == null)
        return;
      CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.getCardWindow());
    }

    private void ClickedOffer()
    {
      if (GameEngine.Instance.World.WorldEnded)
        return;
      int num = 30;
      if (this.ClickedControl.Data == 4112)
        num = 30;
      else if (this.ClickedControl.Data == 4114)
        num = 100;
      if (GameEngine.Instance.World.ProfileCrowns < num)
      {
        BuyCrownsPopup buyCrownsPopup = new BuyCrownsPopup();
        buyCrownsPopup.init(num - GameEngine.Instance.World.ProfileCrowns, this.ParentForm);
        buyCrownsPopup.Show((IWin32Window) this.ParentForm);
      }
      else
      {
        if (this.buying)
          return;
        this.buying = true;
        string txtMessage = "";
        if (this.ClickedControl.Data == 4112)
          txtMessage = SK.Text("PremiumCardsPanel_7Day_Premium", "Buy one 7-Day Premium Token for 30 Crowns?  To activate the Premium Token you must click on it to set it into play on the game world.") + Environment.NewLine;
        else if (this.ClickedControl.Data == 4114)
          txtMessage = SK.Text("PremiumCardsPanel_30Day_Premium", "Buy one 30-Day Premium Token for 100 Crowns?  To activate the Premium Token you must click on it to set it into play on the game world.") + Environment.NewLine;
        if (MyMessageBox.Show(txtMessage, SK.Text("BuyCardsPanel_Confirm_Purchase", "Confirm Purchase"), MessageBoxButtons.OKCancel) == DialogResult.OK)
        {
          this.BoughtTokenPopUp();
          this.labelTitle.Text = SK.Text("PremiumCardsPanel_Buy_and_Open_Packs", "Buy and Play Premium Tokens: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
        }
        else
          this.buying = false;
      }
    }

    private void BoughtOffer(ICardsProvider provider, ICardsResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 1 : (!successCode.HasValue ? 1 : 0)) != 0)
      {
        int num = (int) MyMessageBox.Show(response.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
        this.labelTitle.Text = SK.Text("PremiumCardsPanel_Buy_and_Open_Packs", "Buy and Play Premium Tokens: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
      }
      else
        this.UpdatePremiumTokens();
      this.buying = false;
    }

    private void AvailableContentScroll()
    {
      int y = this.scrollbarAvailable.Value;
      this.AvailablePanelContent.Position = new Point(this.AvailablePanelContent.Position.X, PremiumCardsPanel.BorderPadding - y);
      this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanelContent.ClipRect.Height);
      this.AvailablePanelContent.invalidate();
      this.AvailablePanel.invalidate();
    }

    public void UpdatePremiumTokens()
    {
      foreach (CustomSelfDrawPanel.CSDControl premiumToken in this.PremiumTokens)
        this.mainBackgroundImage.removeControl(premiumToken);
      this.PremiumTokens.Clear();
      int num1 = 45;
      int num2 = 0;
      foreach (CardTypes.PremiumToken premiumToken in GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Values)
      {
        if ((premiumToken.Type & 4112) == 4112 && GFXLibrary.PremiumTokens.ContainsKey(premiumToken.Type))
          ++num2;
      }
      if (num2 > 24)
        num1 = 15;
      else if (num2 > 16)
        num1 = 20;
      else if (num2 > 11)
        num1 = 30;
      int num3 = 0;
      foreach (CardTypes.PremiumToken premiumToken in GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Values)
      {
        if ((premiumToken.Type & 4112) == 4112 && GFXLibrary.PremiumTokens.ContainsKey(premiumToken.Type))
        {
          CustomSelfDrawPanel.CSDImage im = new CustomSelfDrawPanel.CSDImage();
          im.Data = premiumToken.UserPremiumTokenID;
          im.Position = new Point(this.AvailablePanel.X + 32 + num3 * num1, this.cardsButtons.Y + 8);
          im.Size = this.premiumTokenImage.Size;
          BaseImage normalImage = GFXLibrary.PremiumTokens[premiumToken.Type][0];
          BaseImage overImage = GFXLibrary.PremiumTokens[premiumToken.Type][1];
          im.Image = (Image) normalImage;
          im.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => im.Image = (Image) overImage), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => im.Image = (Image) normalImage));
          this.PremiumTokens.Add(im);
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) im);
          im.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickedToken), "PremiumCardsPanel_play_token");
          ++num3;
          if (num3 == 32)
            break;
        }
      }
      this.mainBackgroundImage.invalidate();
      this.PremiumTokensLabel.Text = SK.Text("PremiumCardsPanel_Current_Tokens", "Current Premium Tokens") + " : " + GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Count.ToString() + (GameEngine.Instance.premiumTokenManager.ProfilePremiumTokens.Count > 0 ? " (" + SK.Text("PremiumCardsPanel_Click_To_Play", "click one to play") + ")" : "");
    }

    public void UpdateExpiry()
    {
      if (GameEngine.Instance.premiumTokenManager.PremiumInPlay && GameEngine.Instance.premiumTokenManager.ExpiryTimeSpan.TotalSeconds > 0.0 && GameEngine.Instance.cardsManager.UserCardData.premiumCard > 0)
      {
        if (GameEngine.Instance.cardsManager.UserCardData.premiumCard == 4112)
          this.maxExpirySeconds = 604800.0;
        else if (GameEngine.Instance.cardsManager.UserCardData.premiumCard == 4114)
          this.maxExpirySeconds = 2592000.0;
        else if (GameEngine.Instance.cardsManager.UserCardData.premiumCard == 4113)
          this.maxExpirySeconds = 172800.0;
        this.PremiumInplayImage.Image = GameEngine.Instance.cardsManager.UserCardData.premiumCard <= 0 ? (Image) GFXLibrary.PremiumTokens[4112][0] : (Image) GFXLibrary.PremiumTokens[GameEngine.Instance.cardsManager.UserCardData.premiumCard][0];
        this.PremiumInplayImage.Visible = true;
        if (this.ExpiryBar != null)
          this.ExpiryBar.Visible = true;
        this.expiryLabel.Visible = true;
        this.currentExpirySeconds = GameEngine.Instance.premiumTokenManager.ExpiryTimeSpan.TotalSeconds;
        this.expiryDays = this.currentExpirySeconds / 86400.0;
        this.expiryHours = this.currentExpirySeconds % 86400.0 / 3600.0;
        this.expiryMinutes = this.currentExpirySeconds % 3600.0 / 60.0;
        if (this.TimerInner != null)
          this.ExpiryBar.setValues(this.currentExpirySeconds, this.maxExpirySeconds);
        this.expiryLabel.Text = Math.Floor(this.expiryDays).ToString().PadLeft(2, '0') + ":" + Math.Floor(this.expiryHours).ToString().PadLeft(2, '0') + ":" + Math.Floor(this.expiryMinutes).ToString().PadLeft(2, '0') + " (" + SK.Text("PremiumCardsPanel_Day_Hour_Minute", "dd:hh:mm") + ")";
        this.lastminute = VillageMap.getCurrentServerTime().Minute;
      }
      else
      {
        this.PremiumInplayImage.Visible = false;
        this.expiryLabel.Visible = false;
        if (this.ExpiryBar == null)
          return;
        this.ExpiryBar.Visible = false;
      }
    }

    public void update()
    {
      if (this.lastminute == VillageMap.getCurrentServerTime().Minute)
        return;
      this.UpdateExpiry();
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.closePlayCardsWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

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
  }
}
