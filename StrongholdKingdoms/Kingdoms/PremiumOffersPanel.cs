// Decompiled with JetBrains decompiler
// Type: Kingdoms.PremiumOffersPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Stronghold.AuthClient;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PremiumOffersPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.UICardsButtons cardsButtons;
    private int currentCardSection = -1;
    private static int BorderPadding = 16;
    private int ContentWidth;
    private int AvailablePanelWidth;
    private int InplayPanelWidth;
    private CustomSelfDrawPanel.CSDExtendingPanel InsetPanel;
    private CustomSelfDrawPanel.CSDImage OfferListPanel = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDVertScrollBar OfferListScrollbar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDImage ContentPanel = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDVertScrollBar ContentScrollbar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton purchaseButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton backButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel labelCost = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage crownsIcon = new CustomSelfDrawPanel.CSDImage();
    private int offerYPos = 5;
    private int offerItemCount;
    private PremiumOfferData selectedOffer;
    private bool bAwaitingPurchase;

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

    public PremiumOffersPanel()
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
      this.ContentWidth = this.Width - 2 * PremiumOffersPanel.BorderPadding;
      this.AvailablePanelWidth = this.ContentWidth - 150 - 40;
      this.InplayPanelWidth = this.ContentWidth - PremiumOffersPanel.BorderPadding - this.AvailablePanelWidth;
      CustomSelfDrawPanel.CSDExtendingPanel control1 = new CustomSelfDrawPanel.CSDExtendingPanel();
      control1.Size = this.Size;
      control1.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control1);
      control1.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
      CustomSelfDrawPanel.CSDFill control2 = new CustomSelfDrawPanel.CSDFill();
      control2.FillColor = Color.FromArgb((int) byte.MaxValue, 130, 129, 126);
      control2.Size = new Size(this.Width - 10, 1);
      control2.Position = new Point(5, 34);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control2);
      this.InsetPanel = new CustomSelfDrawPanel.CSDExtendingPanel();
      this.InsetPanel.Position = new Point(PremiumOffersPanel.BorderPadding, PremiumOffersPanel.BorderPadding * 2 + 10);
      this.InsetPanel.Size = new Size(this.AvailablePanelWidth, this.Height - this.InsetPanel.Position.Y - PremiumOffersPanel.BorderPadding - 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.InsetPanel);
      this.InsetPanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
      int width1 = this.Width;
      int borderPadding = PremiumOffersPanel.BorderPadding;
      int width2 = this.InsetPanel.Width;
      this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      this.closeImage.Size = this.closeImage.Image.Size;
      this.closeImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
      this.closeImage.CustomTooltipID = 10100;
      this.closeImage.Position = new Point(this.Width - 14 - 17, 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeImage);
      this.cardsButtons = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) this.ParentForm);
      this.cardsButtons.Position = new Point(808, 37);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardsButtons);
      this.InsetPanel.addControl((CustomSelfDrawPanel.CSDControl) this.OfferListPanel);
      this.InsetPanel.addControl((CustomSelfDrawPanel.CSDControl) this.ContentPanel);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.OfferListScrollbar);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.ContentScrollbar);
      this.mainBackgroundImage.addControl(new CustomSelfDrawPanel.CSDControl()
      {
        Position = new Point(0, 0),
        Size = this.Size
      });
      this.mainBackgroundImage.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelHandler));
      this.backButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.backButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.backButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.backButton.setSizeToImage();
      this.backButton.Position = new Point(this.InsetPanel.Width / 4 - this.backButton.Width / 2, this.InsetPanel.Height - 30 - this.backButton.Height / 2);
      this.backButton.Text.Text = SK.Text("FORUMS_Back", "Back");
      this.backButton.Text.Color = ARGBColors.Black;
      this.backButton.Text.Font = this.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.backButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showOfferList));
      this.InsetPanel.addControl((CustomSelfDrawPanel.CSDControl) this.backButton);
      this.labelCost.Size = new Size(this.InsetPanel.Width / 4, 64);
      this.labelCost.Position = new Point(this.InsetPanel.Width / 4, this.InsetPanel.Height - 30 - this.labelCost.Height / 2);
      this.labelCost.Text = "";
      this.labelCost.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.labelCost.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelCost.Color = ARGBColors.White;
      this.InsetPanel.addControl((CustomSelfDrawPanel.CSDControl) this.labelCost);
      this.crownsIcon.Image = (Image) GFXLibrary.card_offer_pieces[2];
      this.crownsIcon.setSizeToImage();
      this.crownsIcon.Position = new Point(this.InsetPanel.Width / 2 + PremiumOffersPanel.BorderPadding, this.InsetPanel.Height - 30 - this.crownsIcon.Height / 2);
      this.InsetPanel.addControl((CustomSelfDrawPanel.CSDControl) this.crownsIcon);
      this.purchaseButton.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
      this.purchaseButton.ImageOver = (Image) GFXLibrary.button_132_over_gold;
      this.purchaseButton.ImageClick = (Image) GFXLibrary.button_132_in_gold;
      this.purchaseButton.setSizeToImage();
      this.purchaseButton.Position = new Point(this.InsetPanel.Width * 3 / 4 - this.purchaseButton.Width / 2, this.InsetPanel.Height - 30 - this.purchaseButton.Height / 2);
      this.purchaseButton.Text.Text = SK.Text("EmptyVillagePanel_Buy_Village", "Purchase");
      this.purchaseButton.Text.Color = ARGBColors.Black;
      this.purchaseButton.Text.Font = this.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.purchaseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.confirmPurchaseOffer));
      this.InsetPanel.addControl((CustomSelfDrawPanel.CSDControl) this.purchaseButton);
      this.labelTitle.Position = new Point(27, 8);
      this.labelTitle.Size = new Size(780, 64);
      this.labelTitle.Text = "";
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
      this.showOfferList();
      this.mainBackgroundImage.invalidate();
    }

    private void mouseWheelHandler(int delta)
    {
      if (delta > 0)
      {
        if (this.OfferListPanel.Visible)
        {
          if (this.OfferListScrollbar.Value - delta * 15 > 0)
            this.OfferListScrollbar.Value += delta * -15;
          else
            this.OfferListScrollbar.Value = 0;
          this.OfferListScroll();
        }
        else
        {
          if (!this.ContentPanel.Visible)
            return;
          if (this.ContentScrollbar.Value - delta * 15 > 0)
            this.ContentScrollbar.Value += delta * -15;
          else
            this.ContentScrollbar.Value = 0;
          this.ContentScroll();
        }
      }
      else
      {
        if (delta >= 0)
          return;
        if (this.OfferListPanel.Visible)
        {
          if (this.OfferListScrollbar.Value - delta * 15 < this.OfferListScrollbar.Max)
            this.OfferListScrollbar.Value += delta * -15;
          else
            this.OfferListScrollbar.Value = this.OfferListScrollbar.Max;
          this.OfferListScroll();
        }
        else
        {
          if (!this.ContentPanel.Visible)
            return;
          if (this.ContentScrollbar.Value - delta * 15 < this.ContentScrollbar.Max)
            this.ContentScrollbar.Value += delta * -15;
          else
            this.ContentScrollbar.Value = this.ContentScrollbar.Max;
          this.ContentScroll();
        }
      }
    }

    private void OfferListScroll()
    {
      if (!this.OfferListScrollbar.Visible)
        return;
      int y = this.OfferListScrollbar.Value;
      this.OfferListPanel.Position = new Point(this.OfferListPanel.Position.X, -y);
      this.OfferListPanel.ClipRect = new Rectangle(this.OfferListPanel.ClipRect.X, y, this.OfferListPanel.ClipRect.Width, this.OfferListPanel.ClipRect.Height);
      this.OfferListPanel.invalidate();
      this.InsetPanel.invalidate();
    }

    private void ContentScroll()
    {
      if (!this.ContentScrollbar.Visible)
        return;
      int y = this.ContentScrollbar.Value;
      this.ContentPanel.Position = new Point(this.ContentPanel.Position.X, -y);
      this.ContentPanel.ClipRect = new Rectangle(this.ContentPanel.ClipRect.X, y, this.ContentPanel.ClipRect.Width, this.ContentPanel.ClipRect.Height);
      this.ContentPanel.invalidate();
      this.InsetPanel.invalidate();
    }

    public void update()
    {
    }

    private void viewClick()
    {
      if (CustomSelfDrawPanel.StaticClickedControl.Data < 0)
        ((PlayCardsWindow) InterfaceMgr.Instance.getCardWindow()).GetCrowns("&click=saleindicator");
      else
        this.showOfferContent();
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.closePlayCardsWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    private void showOfferList()
    {
      this.OfferListPanel.clearControls();
      int num = 5;
      DateTime currentServerTime = VillageMap.getCurrentServerTime();
      DateTime dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.saleStartTime);
      DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.saleEndTime);
      this.labelTitle.Text = SK.Text("TOUCH_Z_PurchaseSpecialOffer", "Purchase Special Offer");
      if (dateTime1 <= currentServerTime && dateTime2 > currentServerTime)
      {
        CustomSelfDrawPanel.CSDButton control1 = new CustomSelfDrawPanel.CSDButton();
        control1.ImageNorm = (Image) GFXLibrary.offer_sale_normal;
        control1.ImageOver = (Image) GFXLibrary.offer_sale_over;
        control1.Position = new Point(this.InsetPanel.Width / 2 - control1.ImageNorm.Width / 2, num);
        control1.Data = -1;
        control1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.viewClick));
        this.OfferListPanel.addControl((CustomSelfDrawPanel.CSDControl) control1);
        num += control1.ImageNorm.Height;
        CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
        control2.Size = new Size(control1.Width * 2 / 3, 55);
        control2.Position = new Point(control1.Width * 2 / 3 - control2.Width / 2, 20);
        control2.Text = SK.Text("TOUCH_Z_Sale", "Sale").ToUpper();
        control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        control2.Font = FontManager.GetFont("Arial", 36f, FontStyle.Bold);
        control2.Color = ARGBColors.White;
        control2.DropShadowColor = ARGBColors.DarkRed;
        control1.addControl((CustomSelfDrawPanel.CSDControl) control2);
        CustomSelfDrawPanel.CSDLabel control3 = new CustomSelfDrawPanel.CSDLabel();
        control3.Size = new Size(control1.Width * 2 / 3, 45);
        control3.Position = new Point(control1.Width * 2 / 3 - control3.Width / 2, control2.Rectangle.Bottom);
        control3.Text = GameEngine.Instance.World.salePercentage.ToString() + "%";
        control3.Text += " ";
        control3.Text += SK.Text("TOUCH_Z_ExtraCrowns", "Extra Crowns");
        control3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        control3.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
        control3.Color = ARGBColors.White;
        control1.addControl((CustomSelfDrawPanel.CSDControl) control3);
        TimeSpan timeSpan = dateTime2 - currentServerTime;
        CustomSelfDrawPanel.CSDLabel control4 = new CustomSelfDrawPanel.CSDLabel();
        control4.Size = new Size(control1.Width * 2 / 3, 25);
        control4.Position = new Point(control1.Width * 2 / 3 - control4.Width / 2, control1.Height * 3 / 4 - control4.Height / 2);
        control4.Text = VillageMap.createBuildTimeVariable((int) timeSpan.TotalSeconds);
        control4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        control4.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
        control4.Color = ARGBColors.White;
        control4.DropShadowColor = ARGBColors.Black;
        control1.addControl((CustomSelfDrawPanel.CSDControl) control4);
        control1.dataObject = (object) null;
      }
      PremiumOfferData[] premiumOffers = GameEngine.Instance.cardsManager.PremiumOffers;
      for (int index = 0; index < premiumOffers.Length; ++index)
      {
        CustomSelfDrawPanel.CSDButton control5 = new CustomSelfDrawPanel.CSDButton();
        DateTime dateTime3 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) premiumOffers[index].ExpirationTimestamp);
        if (!(currentServerTime >= dateTime3) && !premiumOffers[index].HasBeenPurchased)
        {
          if (premiumOffers[index].OfferType != 1)
          {
            control5.ImageNorm = (Image) GFXLibrary.offer_special_normal;
            control5.ImageOver = (Image) GFXLibrary.offer_special_over;
          }
          else if (premiumOffers[index].Multiplier < 5)
          {
            control5.ImageNorm = (Image) GFXLibrary.offer_bundle_1_normal;
            control5.ImageOver = (Image) GFXLibrary.offer_bundle_1_over;
          }
          else if (premiumOffers[index].Multiplier < 10)
          {
            control5.ImageNorm = (Image) GFXLibrary.offer_bundle_2_normal;
            control5.ImageOver = (Image) GFXLibrary.offer_bundle_2_over;
          }
          else
          {
            control5.ImageNorm = (Image) GFXLibrary.offer_bundle_3_normal;
            control5.ImageOver = (Image) GFXLibrary.offer_bundle_3_over;
          }
          control5.Position = new Point(this.InsetPanel.Width / 2 - control5.ImageNorm.Width / 2, num);
          control5.Data = premiumOffers[index].OfferID;
          control5.dataObject = (object) premiumOffers[index];
          string s = premiumOffers[index].Name;
          int result;
          if (premiumOffers[index].OfferType == 1 && int.TryParse(s, out result))
            s = Rankings.getRankingName(result, RemoteServices.Instance.UserAvatar.male);
          CustomSelfDrawPanel.CSDLabel control6 = new CustomSelfDrawPanel.CSDLabel();
          control6.Size = new Size(control5.Width * 2 / 3, 38);
          control6.Position = new Point(control5.Width * 2 / 3 - control6.Width / 2, 20);
          control6.Text = s;
          control6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
          control6.Font = s.Length <= 21 ? FontManager.GetFont("Arial", 24f, FontStyle.Bold) : FontManager.GetFont("Arial", 16f, FontStyle.Bold);
          control6.Color = ARGBColors.White;
          control6.DropShadowColor = ARGBColors.Black;
          control5.addControl((CustomSelfDrawPanel.CSDControl) control6);
          string description = premiumOffers[index].Description;
          CustomSelfDrawPanel.CSDLabel control7 = new CustomSelfDrawPanel.CSDLabel();
          control7.Size = new Size(control5.Width * 2 / 3 - 50, 25);
          control7.Position = new Point(control5.Width * 2 / 3 - control7.Width / 2, control6.Rectangle.Bottom);
          control7.Text = description;
          control7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
          control7.Font = description.Length <= 40 ? FontManager.GetFont("Arial", 12f, FontStyle.Bold) : FontManager.GetFont("Arial", 8f, FontStyle.Bold);
          control7.Color = ARGBColors.WhiteSmoke;
          control5.addControl((CustomSelfDrawPanel.CSDControl) control7);
          if (premiumOffers[index].Multiplier > 1)
          {
            CustomSelfDrawPanel.CSDLabel control8 = new CustomSelfDrawPanel.CSDLabel();
            control8.Size = new Size(control5.Width * 2 / 3, 25);
            control8.Position = new Point(control5.Width * 2 / 3 - control8.Width / 2, control7.Rectangle.Bottom);
            control8.Text = string.Format(" - x{0} {1}!", (object) premiumOffers[index].Multiplier, (object) SK.Text("TOUCH_Z_Value", "Value"));
            control8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            control8.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            control8.Color = ARGBColors.White;
            control8.DropShadowColor = ARGBColors.Black;
            control5.addControl((CustomSelfDrawPanel.CSDControl) control8);
          }
          TimeSpan timeSpan = dateTime3 - currentServerTime;
          CustomSelfDrawPanel.CSDLabel control9 = new CustomSelfDrawPanel.CSDLabel();
          control9.Size = new Size(control5.Width * 2 / 3, 25);
          control9.Position = new Point(control5.Width * 2 / 3 - control9.Width / 2, control5.Height * 3 / 4 - control9.Height / 2);
          control9.Text = VillageMap.createBuildTimeVariable((int) timeSpan.TotalSeconds);
          control9.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
          control9.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
          control9.Color = ARGBColors.White;
          control9.DropShadowColor = ARGBColors.Black;
          control5.addControl((CustomSelfDrawPanel.CSDControl) control9);
          control5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showOfferContent));
          this.OfferListPanel.addControl((CustomSelfDrawPanel.CSDControl) control5);
          num += control5.ImageNorm.Height;
        }
      }
      this.OfferListPanel.Position = new Point(0, 0);
      this.OfferListPanel.Size = new Size(this.InsetPanel.Width, num);
      this.OfferListPanel.ClipRect = new Rectangle(0, 0, this.InsetPanel.Width, this.InsetPanel.Height);
      if (num < this.OfferListPanel.ClipRect.Height)
        num = this.OfferListPanel.ClipRect.Height;
      this.OfferListScrollbar.Position = new Point(this.InsetPanel.Width - PremiumOffersPanel.BorderPadding - PremiumOffersPanel.BorderPadding / 2, this.InsetPanel.Y + PremiumOffersPanel.BorderPadding / 2);
      this.OfferListScrollbar.Size = new Size(PremiumOffersPanel.BorderPadding, this.InsetPanel.Height - PremiumOffersPanel.BorderPadding);
      this.OfferListScrollbar.Value = 0;
      this.OfferListScrollbar.StepSize = 200;
      this.OfferListScrollbar.Max = this.OfferListPanel.Height - this.OfferListPanel.ClipRect.Height;
      this.OfferListScrollbar.NumVisibleLines = this.OfferListPanel.ClipRect.Height;
      this.OfferListScrollbar.OffsetTL = new Point(1, 5);
      this.OfferListScrollbar.OffsetBR = new Point(0, -10);
      this.OfferListScrollbar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.OfferListScroll));
      this.OfferListScrollbar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.cardpanel_scroll_thumb_top, (Image) GFXLibrary.cardpanel_scroll_thumb_mid, (Image) GFXLibrary.cardpanel_scroll_thumb_botom);
      this.OfferListScrollbar.Visible = num > this.OfferListPanel.ClipRect.Height;
      this.OfferListPanel.Visible = true;
      this.ContentPanel.Visible = false;
      this.ContentScrollbar.Visible = false;
      this.purchaseButton.Visible = false;
      this.backButton.Visible = false;
      this.labelCost.Visible = false;
      this.crownsIcon.Visible = false;
      this.mainBackgroundImage.invalidate();
    }

    private void showOfferContent()
    {
      if (CustomSelfDrawPanel.StaticClickedControl.dataObject == null)
        return;
      this.selectedOffer = (PremiumOfferData) CustomSelfDrawPanel.StaticClickedControl.dataObject;
      PremiumOfferContentData content = this.selectedOffer.Content;
      if (this.selectedOffer.HasBeenPurchased)
        return;
      this.purchaseButton.Enabled = true;
      this.backButton.Enabled = true;
      int result;
      this.labelTitle.Text = this.selectedOffer.OfferType != 1 || !int.TryParse(this.selectedOffer.Name, out result) ? this.selectedOffer.Name : Rankings.getRankingName(result, RemoteServices.Instance.UserAvatar.male);
      this.labelTitle.Text += ": ";
      this.labelTitle.Text += (string) (object) this.selectedOffer.CrownsPrice;
      CustomSelfDrawPanel.CSDLabel labelTitle1 = this.labelTitle;
      labelTitle1.Text = labelTitle1.Text + " " + SK.Text("BuyCrownsPanel_Crowns", "Crowns");
      this.labelTitle.Text += " (";
      CustomSelfDrawPanel.CSDLabel labelTitle2 = this.labelTitle;
      labelTitle2.Text = labelTitle2.Text + SK.Text("LogoutPanel_Crowns_In_Treasury", "Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString() + ")";
      this.labelCost.Text = this.selectedOffer.CrownsPrice.ToString();
      this.ContentPanel.clearControls();
      this.offerYPos = 5;
      this.offerItemCount = 0;
      content.Cards.Sort((Comparison<ContestPrizeCardDefinition>) ((c1, c2) =>
      {
        if (c1 == null)
          return -1;
        if (c2 == null)
          return 1;
        return c2.Amount == c1.Amount ? c2.Name.CompareTo(c1.Name) : c1.Amount.CompareTo(c2.Amount);
      }));
      foreach (ContestPrizeCardDefinition card in content.Cards)
      {
        CardTypes.CardDefinition cardDefinition = CardTypes.getCardDefinition(card.ID);
        UICard ctrl = BuyCardsPanel.makeUICard(cardDefinition, 0, GameEngine.Instance.World.getRank() + 1);
        ctrl.ScaleAll(0.5);
        string text = CardTypes.getDescriptionFromCard(CardTypes.getCardTypeFromString(cardDefinition.name)) + " x" + (object) card.Amount;
        this.AddContentItem((CustomSelfDrawPanel.CSDControl) ctrl, text);
      }
      foreach (ContestPrizePackDefinition cardPack in content.CardPacks)
      {
        if (string.IsNullOrEmpty(cardPack.Name))
        {
          CardTypes.CardOffer cardOffer = GameEngine.Instance.cardPackManager.GetCardOffer(cardPack.OfferID);
          cardPack.Name = SK.Text(GameEngine.Instance.cardPackManager.getCardPackLocalizedStringID(cardOffer.Category));
        }
      }
      content.CardPacks.Sort((Comparison<ContestPrizePackDefinition>) ((c1, c2) =>
      {
        if (c1 == null)
          return -1;
        if (c2 == null)
          return 1;
        return c2.Amount == c1.Amount ? c2.Name.CompareTo(c1.Name) : c1.Amount.CompareTo(c2.Amount);
      }));
      foreach (ContestPrizePackDefinition cardPack in content.CardPacks)
      {
        CardTypes.CardOffer cardOffer = GameEngine.Instance.cardPackManager.GetCardOffer(cardPack.OfferID);
        this.AddContentItem(GameEngine.Instance.cardPackManager.getCardPackOverImage(cardOffer.Category), SK.Text(GameEngine.Instance.cardPackManager.getCardPackLocalizedStringID(cardOffer.Category)) + " x" + (object) cardPack.Amount, 1.0);
      }
      for (int index = 0; index < content.Tokens.Count; ++index)
      {
        ContestPrizeTokenDefinition token = content.Tokens[index];
        if (token.Amount > 0)
        {
          string text = "x " + (object) token.Amount + " " + SK.Text("CARDTYPE_GENERIC_PREMIUM", "Premium Token");
          this.AddContentItem(GFXLibrary.PremiumTokens[token.TokenType][0], text, 1.0);
        }
      }
      for (int index = 0; index < content.Wheelspins.Length; ++index)
      {
        int wheelspin = content.Wheelspins[index];
        if (wheelspin > 0)
        {
          string text = SK.Text("GENERIC_Wheel_Spins", "Wheel Spins");
          if (index < 5)
            text = text + " (" + SK.Text("Event_Tier", "Tier") + " " + (object) (index + 1) + ") x" + (object) wheelspin;
          switch (index)
          {
            case 0:
              this.AddContentItem(GFXLibrary.offer_wheel_spin1, text, 1.0);
              continue;
            case 1:
              this.AddContentItem(GFXLibrary.offer_wheel_spin2, text, 1.0);
              continue;
            case 2:
              this.AddContentItem(GFXLibrary.offer_wheel_spin3, text, 1.0);
              continue;
            case 3:
              this.AddContentItem(GFXLibrary.offer_wheel_spin4, text, 1.0);
              continue;
            case 4:
              this.AddContentItem(GFXLibrary.offer_wheel_spin5, text, 1.0);
              continue;
            default:
              this.AddContentItem(GFXLibrary.offer_wheel_spin, text, 1.0);
              continue;
          }
        }
      }
      if (content.Gold > 0)
      {
        string text = SK.Text("GENERIC_Gold", "Gold") + " x" + (object) content.Gold;
        this.AddContentItem(GFXLibrary.offer_gold, text, 1.0);
      }
      if (content.Honour > 0)
      {
        string text = SK.Text("GENERIC_Honour", "Honour") + " x" + (object) content.Honour;
        this.AddContentItem(GFXLibrary.offer_honour, text, 1.0);
      }
      if (content.Faith > 0)
      {
        string text = SK.Text("VillageMapPanel_Faith_Points", "Faith Points") + " x" + (object) content.Faith;
        this.AddContentItem(GFXLibrary.offer_faith, text, 1.0);
      }
      if (content.Charges.Count > 0)
      {
        string text = SK.Text("TOUCH_Z_ShieldCharges", "Shield Charges") + " x" + (object) content.Charges.Count;
        this.AddContentItem(GFXLibrary.offer_shield, text, 1.0);
      }
      this.ContentPanel.Position = new Point(0, 0);
      this.ContentPanel.Size = new Size(this.InsetPanel.Width, this.offerYPos);
      this.ContentPanel.ClipRect = new Rectangle(0, 0, this.InsetPanel.Width, this.InsetPanel.Height - 60);
      if (this.offerYPos < this.ContentPanel.ClipRect.Height)
        this.offerYPos = this.ContentPanel.ClipRect.Height;
      this.ContentScrollbar.Position = new Point(this.InsetPanel.Width - PremiumOffersPanel.BorderPadding - PremiumOffersPanel.BorderPadding / 2, this.InsetPanel.Y + PremiumOffersPanel.BorderPadding / 2);
      this.ContentScrollbar.Size = new Size(PremiumOffersPanel.BorderPadding, this.InsetPanel.Height - PremiumOffersPanel.BorderPadding * 2 - 60);
      this.ContentScrollbar.Value = 0;
      this.ContentScrollbar.StepSize = 200;
      this.ContentScrollbar.Max = this.ContentPanel.Height - this.ContentPanel.ClipRect.Height;
      this.ContentScrollbar.NumVisibleLines = this.ContentPanel.ClipRect.Height;
      this.ContentScrollbar.OffsetTL = new Point(1, 5);
      this.ContentScrollbar.OffsetBR = new Point(0, -10);
      this.ContentScrollbar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.ContentScroll));
      this.ContentScrollbar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.cardpanel_scroll_thumb_top, (Image) GFXLibrary.cardpanel_scroll_thumb_mid, (Image) GFXLibrary.cardpanel_scroll_thumb_botom);
      this.ContentScrollbar.Visible = this.offerYPos > this.ContentPanel.ClipRect.Height;
      this.ContentPanel.Visible = true;
      this.purchaseButton.Visible = true;
      this.backButton.Visible = true;
      this.labelCost.Visible = true;
      this.crownsIcon.Visible = true;
      this.OfferListPanel.Visible = false;
      this.OfferListScrollbar.Visible = false;
      this.mainBackgroundImage.invalidate();
      this.trackViewedOffer();
    }

    private void AddContentItem(BaseImage img, string text, double scale)
    {
      CustomSelfDrawPanel.CSDImage ctrl = new CustomSelfDrawPanel.CSDImage();
      ctrl.Image = (Image) img;
      ctrl.Scale = scale;
      this.AddContentItem((CustomSelfDrawPanel.CSDControl) ctrl, text);
    }

    private void AddContentItem(CustomSelfDrawPanel.CSDControl ctrl, string text)
    {
      CustomSelfDrawPanel.CSDImage control1 = new CustomSelfDrawPanel.CSDImage();
      control1.Image = (Image) GFXLibrary.card_offer_background;
      ctrl.Position = new Point(PremiumOffersPanel.BorderPadding * 2, control1.Height / 2 - ctrl.Height / 2);
      control1.addControl(ctrl);
      CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
      control2.Size = new Size(control1.Width * 2 / 3, control1.Height);
      control2.Position = new Point(control1.Width / 3, 0);
      control2.Text = text;
      control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control2.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      control2.Color = ARGBColors.Black;
      control2.DropShadowColor = ARGBColors.White;
      control1.addControl((CustomSelfDrawPanel.CSDControl) control2);
      int num = Math.Max(control1.Height, ctrl.Height);
      control1.Position = new Point(this.offerItemCount % 2 == 0 ? PremiumOffersPanel.BorderPadding : this.InsetPanel.Width - control1.Width - PremiumOffersPanel.BorderPadding * 3, this.offerYPos + num / 2 - control1.Height / 2);
      this.ContentPanel.addControl((CustomSelfDrawPanel.CSDControl) control1);
      this.offerYPos += num - PremiumOffersPanel.BorderPadding * 2;
      ++this.offerItemCount;
    }

    private void confirmPurchaseOffer()
    {
      this.purchaseButton.Enabled = false;
      this.backButton.Enabled = false;
      if (this.selectedOffer.HasBeenPurchased || this.bAwaitingPurchase)
        return;
      if (MyMessageBox.Show(SK.Text("TOUCH_Z_ConfirmationSpecialOffer", "Are you sure you want to buy this Special Offer?") + Environment.NewLine + Environment.NewLine + "(" + (object) this.selectedOffer.CrownsPrice + " " + SK.Text("BuyCrownsPanel_Crowns", "Crowns") + ")", SK.Text("BuyCardsPanel_Confirm_Purchase", "Confirm Purchase"), MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        this.purchaseOfferClick();
      }
      else
      {
        this.purchaseButton.Enabled = true;
        this.backButton.Enabled = true;
      }
    }

    private void purchaseOfferClick()
    {
      if (this.bAwaitingPurchase)
        return;
      if (this.selectedOffer.CrownsPrice > GameEngine.Instance.World.ProfileCrowns)
      {
        this.purchaseButton.Enabled = true;
        this.backButton.Enabled = true;
        BuyCrownsPopup buyCrownsPopup = new BuyCrownsPopup();
        buyCrownsPopup.init(this.selectedOffer.CrownsPrice - GameEngine.Instance.World.ProfileCrowns, this.ParentForm);
        buyCrownsPopup.Show((IWin32Window) this.ParentForm);
      }
      else
      {
        this.bAwaitingPurchase = true;
        XmlRpcPremiumOffersProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).PurchaseSpecialOffer((IPremiumOffersRequest) new XmlRpcPremiumOffersRequest()
        {
          UserGUID = RemoteServices.Instance.UserGuidProfileSite,
          SessionGUID = RemoteServices.Instance.SessionGuidProfileSite,
          WorldID = new int?(RemoteServices.Instance.ProfileWorldID),
          OfferID = new int?(this.selectedOffer.OfferID)
        }, new PremiumOffersEndResponseDelegate(this.purchaseOfferCallback), (Control) null);
      }
    }

    private void purchaseOfferCallback(
      IPremiumOffersProvider sender,
      IPremiumOffersResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        this.selectedOffer.HasBeenPurchased = true;
        this.labelTitle.Text = SK.Text("ManageCandsPanel_Successful_Purchase", "Your purchase has been successfully completed");
        GameEngine.Instance.World.ProfileCrowns -= this.selectedOffer.CrownsPrice;
        GameEngine.Instance.World.addGold((double) this.selectedOffer.Content.Gold);
        GameEngine.Instance.World.addHonour((double) this.selectedOffer.Content.Honour);
        GameEngine.Instance.World.addFaithPoints((double) this.selectedOffer.Content.Faith);
        foreach (ContestPrizeCardDefinition cardsUniqueId in ((XmlRpcPremiumOffersResponse) response).CardsUniqueIDs)
        {
          string stringFromCard = CardTypes.getStringFromCard(cardsUniqueId.ID);
          foreach (int uniqueId in cardsUniqueId.UniqueIDs)
            GameEngine.Instance.cardsManager.addProfileCard(uniqueId, stringFromCard);
        }
        foreach (ContestPrizePackDefinition cardPack in this.selectedOffer.Content.CardPacks)
          GameEngine.Instance.cardPackManager.addCardPack(cardPack.OfferID, cardPack.Amount);
        for (int level = 0; level < this.selectedOffer.Content.Wheelspins.Length; ++level)
          GameEngine.Instance.World.addTickets(level, this.selectedOffer.Content.Wheelspins[level]);
        foreach (ContestPrizeTokenDefinition token in this.selectedOffer.Content.Tokens)
        {
          foreach (int uniqueId in token.UniqueIDs)
            GameEngine.Instance.premiumTokenManager.AddToken(new CardTypes.PremiumToken()
            {
              Reward = 0,
              Type = token.TokenType,
              UserPremiumTokenID = uniqueId,
              WorldID = RemoteServices.Instance.ProfileWorldID
            });
        }
        this.purchaseButton.Enabled = false;
        this.purchaseButton.Visible = false;
        this.crownsIcon.Visible = false;
        this.labelCost.Visible = true;
        this.backButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.leavePanelClick));
      }
      else
      {
        this.purchaseButton.Enabled = true;
        int num = (int) MyMessageBox.Show(response.ErrorMessage, SK.Text("GENERIC_Error", "Error"));
        this.backButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showOfferList));
      }
      this.bAwaitingPurchase = false;
      this.backButton.Enabled = true;
      GameEngine.Instance.cardsManager.RetrievePremiumOffers();
    }

    private void trackViewedOffer()
    {
      XmlRpcPremiumOffersProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).TrackViewedOffer((IPremiumOffersRequest) new XmlRpcPremiumOffersRequest()
      {
        UserGUID = RemoteServices.Instance.UserGuidProfileSite,
        SessionGUID = RemoteServices.Instance.SessionGuidProfileSite,
        WorldID = new int?(RemoteServices.Instance.ProfileWorldID),
        OfferID = new int?(this.selectedOffer.OfferID)
      }, new PremiumOffersEndResponseDelegate(this.trackOfferViewedCallback), (Control) null);
    }

    private void trackOfferViewedCallback(
      IPremiumOffersProvider sender,
      IPremiumOffersResponse response)
    {
    }

    private void leavePanelClick()
    {
      ((PlayCardsWindow) this.ParentForm).SetCardSection(0);
      if (this.selectedOffer.Content.CardPacks.Count <= 0)
        return;
      ((PlayCardsWindow) this.ParentForm).SwitchPanel(2);
    }
  }
}
