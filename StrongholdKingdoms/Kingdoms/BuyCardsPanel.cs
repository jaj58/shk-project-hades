// Decompiled with JetBrains decompiler
// Type: Kingdoms.BuyCardsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class BuyCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
  {
    private CustomSelfDrawPanel.UICardsButtons cardButtons;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private List<UICardOffer> OfferList;
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelBottom = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDFill greyout = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
    private Dictionary<string, UICardPack> packControls = new Dictionary<string, UICardPack>();
    private int currentCardSection = -1;
    private bool waitingResponse;
    private UICard lastRequestCard;
    private int selectedVillage;
    private static int BorderPadding = 16;
    private int ContentWidth;
    private int AvailablePanelWidth;
    private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
    private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
    private int packWidth;
    private int packX;
    private int offerX;
    private UICardOffer lastoffer;
    private UICard[] revealCards = new UICard[50];
    private int numRevealCards;
    private static Image packimage = (Image) null;
    private static Image offerimage = (Image) null;
    private MyMessageBoxPopUp confirmBuyCardPopUp;
    private int diamondAnimFrame;
    private DateTime diamondAnimStartTime = DateTime.Now;
    private PreValidateCardToBePlayed_ReturnType returnDataRef;
    private IContainer components;

    public BuyCardsPanel()
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
      this.ContentWidth = this.Width - 2 * BuyCardsPanel.BorderPadding;
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
      this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, 375);
      this.AvailablePanel.Position = new Point(8, this.Height - 8 - 550);
      this.AvailablePanel.Alpha = 0.8f;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanel);
      this.AvailablePanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
      int width1 = this.Width;
      int borderPadding = BuyCardsPanel.BorderPadding;
      int width2 = this.AvailablePanel.Width;
      this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      this.closeImage.Size = this.closeImage.Image.Size;
      this.closeImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
      this.closeImage.Position = new Point(this.Width - 14 - 17, 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeImage);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 39, new Point(this.Width - 1 - 17 - 50 + 3, 5), true);
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
      closeGrey.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.CloseGrey), "BuyCardsPanel_close_overlay");
      closeGrey.Position = new Point(this.Width - 14 - 17, 10);
      this.greyout.addControl((CustomSelfDrawPanel.CSDControl) closeGrey);
      CustomSelfDrawPanel.UICardsButtons control5 = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) this.ParentForm);
      control5.Position = new Point(808, 37);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control5);
      this.cardButtons = control5;
      this.labelTitle.Position = new Point(27, 8);
      this.labelTitle.Size = new Size(935, 64);
      this.labelTitle.Text = SK.Text("BuyCardsPanel_Buy_and_Open_Packs", "Buy and Open Card Packs: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
      this.labelBottom.Position = new Point(27, this.AvailablePanel.Y + this.AvailablePanel.Height + 4);
      this.labelBottom.Size = new Size(800, 64);
      this.labelBottom.Text = SK.Text("BuyCardsPanel_Click_To_Open", "Click on a pack to open it");
      this.labelBottom.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelBottom.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
      this.labelBottom.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelBottom);
      this.packWidth = 100;
      this.packX = this.AvailablePanel.X + BuyCardsPanel.BorderPadding;
      this.offerX = this.packX + this.packWidth - 16;
      this.GetOffercontrolList();
      this.AddOfferControls();
      this.UpdatePacks();
    }

    public void OpenGrey()
    {
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.greyout);
      this.cardButtons.Available = false;
      this.mainBackgroundImage.invalidate();
    }

    public void CloseGrey()
    {
      this.mainBackgroundImage.removeControl((CustomSelfDrawPanel.CSDControl) this.greyout);
      this.cardButtons.Available = true;
      this.mainBackgroundImage.invalidate();
      this.numRevealCards = 0;
    }

    public void UpdatePacks()
    {
      if (BuyCardsPanel.packimage == null)
      {
        BuyCardsPanel.packimage = (Image) new Bitmap(this.packWidth, 136);
        using (Graphics graphics = Graphics.FromImage(BuyCardsPanel.packimage))
          graphics.FillRectangle(Brushes.Green, 0, 0, BuyCardsPanel.packimage.Width, BuyCardsPanel.packimage.Height);
      }
      foreach (CustomSelfDrawPanel.CSDControl control in this.packControls.Values)
        this.mainBackgroundImage.removeControl(control);
      this.packControls.Clear();
      this.AvailablePanelContent.invalidate();
      int num1 = this.packWidth;
      int num2 = 0;
      foreach (CardTypes.UserCardPack userCardPack in GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Values)
      {
        if (!this.packControls.ContainsKey(GameEngine.Instance.cardPackManager.ProfileCardOffers[userCardPack.OfferID].Category) && userCardPack.Count > 0)
          ++num2;
      }
      if (num2 >= 8)
        num1 = 75;
      foreach (CardTypes.UserCardPack userCardPack in GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Values)
      {
        if (!this.packControls.ContainsKey(GameEngine.Instance.cardPackManager.ProfileCardOffers[userCardPack.OfferID].Category) && userCardPack.Count > 0)
        {
          UICardPack packControl = new UICardPack()
          {
            baseImage = new CustomSelfDrawPanel.CSDImage()
          };
          packControl.baseImage.Image = BuyCardsPanel.packimage;
          packControl.baseImage.Size = BuyCardsPanel.packimage.Size;
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          string empty3 = string.Empty;
          string category = GameEngine.Instance.cardPackManager.ProfileCardOffers[userCardPack.OfferID].Category;
          packControl.OfferID = userCardPack.OfferID;
          int cardPackTooltipId = GameEngine.Instance.cardPackManager.getCardPackTooltipID(category);
          string localizedStringId = GameEngine.Instance.cardPackManager.getCardPackLocalizedStringID(category);
          packControl.nameText = SK.Text(localizedStringId);
          packControl.baseImage.Image = (Image) GameEngine.Instance.cardPackManager.getCardPackBaseImage(category);
          packControl.overImage.Image = (Image) GameEngine.Instance.cardPackManager.getCardPackOverImage(category);
          packControl.addControl((CustomSelfDrawPanel.CSDControl) packControl.baseImage);
          packControl.addControl((CustomSelfDrawPanel.CSDControl) packControl.overImage);
          packControl.baseImage.Visible = true;
          packControl.overImage.Visible = false;
          packControl.CustomTooltipID = cardPackTooltipId;
          packControl.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
          {
            packControl.baseImage.Visible = false;
            packControl.overImage.Visible = true;
          }), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
          {
            packControl.baseImage.Visible = true;
            packControl.overImage.Visible = false;
          }));
          packControl.Size = packControl.baseImage.Size;
          if (this.packControls.Count > 0)
            packControl.Position = new Point(this.AvailablePanel.X + (num1 + 4) * this.packControls.Count, this.Height - 4 - packControl.Height);
          else
            packControl.Position = new Point(this.AvailablePanel.X, this.Height - 4 - packControl.Height);
          packControl.ClickArea = new Rectangle(8, 0, 75, packControl.Height);
          packControl.PackIDs.Add(userCardPack.OfferID);
          this.packControls.Add(GameEngine.Instance.cardPackManager.ProfileCardOffers[userCardPack.OfferID].Category, packControl);
        }
        else if (userCardPack.Count > 0)
          this.packControls[GameEngine.Instance.cardPackManager.ProfileCardOffers[userCardPack.OfferID].Category].PackIDs.Add(userCardPack.OfferID);
      }
      foreach (UICardPack control1 in this.packControls.Values)
      {
        CustomSelfDrawPanel.CSDImage control2 = new CustomSelfDrawPanel.CSDImage();
        control2.Image = (Image) GFXLibrary.cardpanel_pack_open_circle;
        control2.Size = control2.Image.Size;
        control2.Position = new Point(control1.Width - control2.Width - 4, control1.Height - control2.Height - control2.Height / 2);
        control1.addControl((CustomSelfDrawPanel.CSDControl) control2);
        control1.nameLabel = new CustomSelfDrawPanel.CSDLabel();
        int num3 = 0;
        foreach (CardTypes.UserCardPack userCardPack in GameEngine.Instance.cardPackManager.ProfileUserCardPacks.Values)
        {
          if (control1.PackIDs.Contains(userCardPack.OfferID))
            num3 += userCardPack.Count;
        }
        control1.nameLabel.Text = num3.ToString();
        control1.nameLabel.Position = new Point(control2.X - 2 - 50, control2.Y - 2);
        control1.nameLabel.Size = new Size(control2.Size.Width + 100, control2.Size.Height);
        control1.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        control1.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        control1.nameLabel.Color = ARGBColors.Black;
        control1.addControl((CustomSelfDrawPanel.CSDControl) control1.nameLabel);
        if (control1.PackIDs.Count > 0)
        {
          control1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.OpenPack));
          this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control1);
        }
      }
      this.mainBackgroundImage.invalidate();
    }

    public void GetOffercontrolList()
    {
      if (BuyCardsPanel.offerimage == null)
      {
        BuyCardsPanel.offerimage = (Image) new Bitmap(180, 150);
        using (Graphics graphics = Graphics.FromImage(BuyCardsPanel.offerimage))
          graphics.FillRectangle(Brushes.Green, 0, 0, BuyCardsPanel.offerimage.Width, BuyCardsPanel.offerimage.Height);
      }
      this.OfferList = new List<UICardOffer>();
      foreach (CardTypes.CardOffer cardOffer in GameEngine.Instance.cardPackManager.ProfileCardOffers.Values)
      {
        if (cardOffer.Buyable == 1)
        {
          if (cardOffer.Category == "PLATINUM")
          {
            cardOffer.Buyable = 0;
          }
          else
          {
            UICardOffer off = new UICardOffer();
            off.Offer = cardOffer;
            off.baseImage = new CustomSelfDrawPanel.CSDImage();
            off.baseImage.Position = new Point(0, 20);
            off.packImage = new CustomSelfDrawPanel.CSDImage();
            off.packImage.Position = new Point(10, -7);
            off.packOverImage = new CustomSelfDrawPanel.CSDImage();
            off.packOverImage.Position = new Point(10, -7);
            off.crownImage = new CustomSelfDrawPanel.CSDImage();
            off.crownImage.Position = new Point(330, 16);
            string key1 = string.Empty;
            string key2 = string.Empty;
            string ID = string.Empty;
            string defaultText = string.Empty;
            switch (cardOffer.Category)
            {
              case "FARMING":
                key1 = "card_pack_food_standard_normal";
                key2 = "card_pack_food_standard_over";
                ID = "CARD_OFFERS_Food_Pack";
                defaultText = "Food Pack";
                break;
              case "CASTLE":
                key1 = "card_pack_castle_standard_normal";
                key2 = "card_pack_castle_standard_over";
                ID = "CARD_OFFERS_Castle_Pack";
                defaultText = "Castle Pack";
                break;
              case "DEFENSE":
              case "DEFENCE":
                key1 = "card_pack_defence_standard_normal";
                key2 = "card_pack_defence_standard_over";
                ID = "CARD_OFFERS_Defense_Pack";
                defaultText = "Defence Pack";
                break;
              case "RANDOM":
                key1 = "card_pack_random_standard_normal";
                key2 = "card_pack_random_standard_over";
                ID = "CARD_OFFERS_Random_Pack";
                defaultText = "Random Pack";
                break;
              case "INDUSTRY":
                key1 = "card_pack_Industry_standard_normal";
                key2 = "card_pack_Industry_standard_over";
                ID = "CARD_OFFERS_Industry_Pack";
                defaultText = "Industry Pack";
                break;
              case "RESEARCH":
                key1 = "card_pack_research_silver_normal";
                key2 = "card_pack_research_silver_over";
                ID = "CARD_OFFERS_Industry_Pack";
                defaultText = "Industry Pack";
                break;
              case "ARMY":
                key1 = "card_pack_army_standard_normal";
                key2 = "card_pack_army_standard_over";
                ID = "CARD_OFFERS_Army_Pack";
                defaultText = "Army Pack";
                break;
              case "SUPERFARMING":
                key1 = "card_pack_food_silver_normal";
                key2 = "card_pack_food_silver_over";
                ID = "CARD_OFFERS_Super_Food_Pack";
                defaultText = "Super Food Pack";
                break;
              case "SUPERDEFENSE":
              case "SUPERDEFENCE":
                key1 = "card_pack_defence_silver_normal";
                key2 = "card_pack_defence_silver_over";
                ID = "CARD_OFFERS_Super_Defense_Pack";
                defaultText = "Super Defence Pack";
                break;
              case "SUPERRANDOM":
                key1 = "card_pack_random_silver_normal";
                key2 = "card_pack_random_silver_over";
                ID = "CARD_OFFERS_Super_Random_Pack";
                defaultText = "Super Random Pack";
                CustomSelfDrawPanel.CSDButton control1 = new CustomSelfDrawPanel.CSDButton();
                control1.ImageNorm = (Image) GFXLibrary.mrhp_button_more_info_solid[0];
                control1.ImageOver = (Image) GFXLibrary.mrhp_button_more_info_solid[1];
                control1.MoveOnClick = true;
                control1.Position = new Point(270, 100);
                control1.Text.Text = SK.Text("UserInfo_MoreInfo", "More Info");
                control1.Text.Font = !(Program.mySettings.LanguageIdent == "it") ? FontManager.GetFont("Arial", 8f, FontStyle.Bold) : FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                control1.TextYOffset = -3;
                control1.Text.Position = new Point(-3, 0);
                control1.Text.Color = ARGBColors.Black;
                control1.Text.DropShadowColor = Color.FromArgb(60, 90, 100);
                control1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.moreSuperClicked));
                off.addControl((CustomSelfDrawPanel.CSDControl) control1);
                break;
              case "SUPERINDUSTRY":
                key1 = "card_pack_Industry_silver_normal";
                key2 = "card_pack_Industry_silver_over";
                ID = "CARD_OFFERS_Super_Industry_Pack";
                defaultText = "Super Industry Pack";
                break;
              case "SUPERARMY":
                key1 = "card_pack_army_silver_normal";
                key2 = "card_pack_army_silver_over";
                ID = "CARD_OFFERS_Super_Army_Pack";
                defaultText = "Super Army Pack";
                break;
              case "ULTIMATEFARMING":
                key1 = "card_pack_food_gold_normal";
                key2 = "card_pack_food_gold_over";
                ID = "CARD_OFFERS_Ultimate_Food_Pack";
                defaultText = "Ultimate Food Pack";
                break;
              case "ULTIMATEDEFENSE":
              case "ULTIMATEDEFENCE":
                key1 = "card_pack_defence_gold_normal";
                key2 = "card_pack_defence_gold_over";
                ID = "CARD_OFFERS_Ultimate_Defense_Pack";
                defaultText = "Ultimate Defence Pack";
                break;
              case "ULTIMATERANDOM":
                key1 = "card_pack_random_gold_normal";
                key2 = "card_pack_random_gold_over";
                ID = "CARD_OFFERS_Ultimate_Random_Pack";
                defaultText = "Ultimate Random Pack";
                CustomSelfDrawPanel.CSDButton control2 = new CustomSelfDrawPanel.CSDButton();
                control2.ImageNorm = (Image) GFXLibrary.mrhp_button_more_info_solid[0];
                control2.ImageOver = (Image) GFXLibrary.mrhp_button_more_info_solid[1];
                control2.MoveOnClick = true;
                control2.Position = new Point(270, 100);
                control2.Text.Text = SK.Text("UserInfo_MoreInfo", "More Info");
                control2.Text.Font = !(Program.mySettings.LanguageIdent == "it") ? FontManager.GetFont("Arial", 8f, FontStyle.Bold) : FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                control2.TextYOffset = -3;
                control2.Text.Position = new Point(-3, 0);
                control2.Text.Color = ARGBColors.Black;
                control2.Text.DropShadowColor = Color.FromArgb(60, 90, 100);
                control2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.moreUltimateClicked));
                off.addControl((CustomSelfDrawPanel.CSDControl) control2);
                break;
              case "ULTIMATEINDUSTRY":
                key1 = "card_pack_Industry_gold_normal";
                key2 = "card_pack_Industry_gold_over";
                ID = "CARD_OFFERS_Ultimate_Industry_Pack";
                defaultText = "Ultimate Industry Pack";
                break;
              case "ULTIMATEARMY":
                key1 = "card_pack_army_gold_normal";
                key2 = "card_pack_army_gold_over";
                ID = "CARD_OFFERS_Ultimate_Army_Pack";
                defaultText = "Ultimate Army Pack";
                break;
              case "PLATINUM":
                key1 = "card_pack_army_gold_normal";
                key2 = "card_pack_army_gold_over";
                ID = "CARD_OFFERS_Platinum_Pack";
                defaultText = "Platinum Pack";
                break;
            }
            off.baseImage.Image = (Image) GFXLibrary.card_offer_background;
            if (GFXLibrary.CardPackImages == null)
              UniversalDebugLog.Log("CARDPACK IMAGES IS NULL");
            UniversalDebugLog.Log("Num packimages: " + (object) GFXLibrary.CardPackImages.Count);
            off.packImage.Image = (Image) GFXLibrary.CardPackImages[key1];
            off.packOverImage.Image = (Image) GFXLibrary.CardPackImages[key2];
            string str = SK.Text(ID, defaultText);
            off.crownImage.Image = (Image) GFXLibrary.card_offer_pieces[2];
            off.packImage.Visible = true;
            off.packOverImage.Visible = false;
            off.baseImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
            {
              off.packImage.Visible = false;
              off.packOverImage.Visible = true;
              off.baseImage.Image = (Image) GFXLibrary.card_offer_background_over;
            }), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
            {
              off.packImage.Visible = true;
              off.packOverImage.Visible = false;
              off.baseImage.Image = (Image) GFXLibrary.card_offer_background;
            }));
            off.nameLabel = new CustomSelfDrawPanel.CSDLabel();
            off.nameLabel.Position = new Point(94, 29);
            off.nameLabel.Text = str;
            off.nameLabel.Size = new Size(300, 30);
            off.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            off.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            off.nameLabel.Color = ARGBColors.Black;
            off.descLabel = new CustomSelfDrawPanel.CSDLabel();
            off.descLabel.Position = new Point(94, 46);
            off.descLabel.Text = SK.Text(ID + "_desc");
            off.descLabel.Size = new Size(245, 45);
            off.descLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            off.descLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            off.descLabel.Color = ARGBColors.Black;
            off.cardLabel = new CustomSelfDrawPanel.CSDLabel();
            off.cardLabel.Position = new Point(191, 59);
            off.cardLabel.Text = SK.Text("BUY_CARDS_5_per_pack", "5 Cards per Pack");
            off.cardLabel.Size = new Size(200, 30);
            off.cardLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            off.cardLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            off.cardLabel.Color = ARGBColors.Black;
            off.costLabel = new CustomSelfDrawPanel.CSDLabel();
            off.costLabel.Position = new Point(306, 28);
            off.costLabel.Text = cardOffer.CrownCost.ToString();
            off.costLabel.Size = new Size(40, 30);
            off.costLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            off.costLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            off.costLabel.Color = ARGBColors.Black;
            off.addControl((CustomSelfDrawPanel.CSDControl) off.baseImage);
            off.addControl((CustomSelfDrawPanel.CSDControl) off.packImage);
            off.addControl((CustomSelfDrawPanel.CSDControl) off.packOverImage);
            off.addControl((CustomSelfDrawPanel.CSDControl) off.nameLabel);
            off.addControl((CustomSelfDrawPanel.CSDControl) off.descLabel);
            off.addControl((CustomSelfDrawPanel.CSDControl) off.crownImage);
            off.addControl((CustomSelfDrawPanel.CSDControl) off.cardLabel);
            off.addControl((CustomSelfDrawPanel.CSDControl) off.costLabel);
            off.baseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.OfferClicked));
            off.Size = new Size(off.baseImage.Size.Width, 140);
            this.OfferList.Add(off);
          }
        }
      }
    }

    public void moreSuperClicked() => this.showMorePopup(0);

    public void moreUltimateClicked() => this.showMorePopup(1);

    public void showMorePopup(int mode) => GameEngine.Instance.openSuperPackInfo(mode);

    public void AddOfferControls()
    {
      this.OfferList.Sort((Comparison<UICardOffer>) ((first, next) => first.Offer.Sequence.CompareTo(next.Offer.Sequence)));
      int num = 100;
      int height = 0;
      for (int index = 0; index < this.OfferList.Count; ++index)
      {
        UICardOffer offer = this.OfferList[index];
        offer.Position = new Point((index & 1) * 330, 5 + num * index);
        this.AvailablePanelContent.addControl((CustomSelfDrawPanel.CSDControl) offer);
        height = offer.Position.Y + offer.Height + 4;
      }
      this.AvailablePanelContent.Position = new Point(BuyCardsPanel.BorderPadding, 0);
      this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
      this.AvailablePanelContent.ClipRect = new Rectangle(0, 0, this.AvailablePanel.Width - BuyCardsPanel.BorderPadding, this.AvailablePanel.Height);
      this.AvailablePanel.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanelContent);
      if (height < this.AvailablePanelContent.ClipRect.Height)
        height = this.AvailablePanelContent.ClipRect.Height;
      this.scrollbarAvailable.Position = new Point(this.AvailablePanel.Width - BuyCardsPanel.BorderPadding - BuyCardsPanel.BorderPadding / 2, this.AvailablePanel.Y + BuyCardsPanel.BorderPadding / 2);
      this.scrollbarAvailable.Size = new Size(BuyCardsPanel.BorderPadding, this.AvailablePanel.Height - BuyCardsPanel.BorderPadding);
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
      this.mainBackgroundImage.addControl(new CustomSelfDrawPanel.CSDControl()
      {
        Position = new Point(0, 0),
        Size = this.Size
      });
      this.mainBackgroundImage.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelHandler));
    }

    private void mouseWheelHandler(int delta)
    {
      if (delta > 0)
      {
        if (this.scrollbarAvailable.Value - delta * 15 > 0)
          this.scrollbarAvailable.Value += delta * -15;
        else
          this.scrollbarAvailable.Value = 0;
        this.AvailableContentScroll();
      }
      else
      {
        if (delta >= 0)
          return;
        if (this.scrollbarAvailable.Value - delta * 15 < this.scrollbarAvailable.Max)
          this.scrollbarAvailable.Value += delta * -15;
        else
          this.scrollbarAvailable.Value = this.scrollbarAvailable.Max;
        this.AvailableContentScroll();
      }
    }

    private void AvailableContentScroll()
    {
      int y = this.scrollbarAvailable.Value;
      this.AvailablePanelContent.Position = new Point(this.AvailablePanelContent.Position.X, -y);
      this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanelContent.ClipRect.Height);
      this.AvailablePanelContent.invalidate();
      this.AvailablePanel.invalidate();
    }

    public void OfferClicked() => this.doOfferClicked(true);

    public void doOfferClicked(bool initialClick)
    {
      if (GameEngine.Instance.World.WorldEnded)
        return;
      UICardOffer parent = (UICardOffer) this.ClickedControl.Parent;
      if (initialClick && parent.Offer.CrownCost > GameEngine.Instance.World.ProfileCrowns)
      {
        BuyCrownsPopup buyCrownsPopup = new BuyCrownsPopup();
        buyCrownsPopup.init(parent.Offer.CrownCost - GameEngine.Instance.World.ProfileCrowns, this.ParentForm);
        buyCrownsPopup.Show((IWin32Window) this.ParentForm);
      }
      else if (initialClick && Program.mySettings.BuyMultipleCardPacks)
      {
        GameEngine.Instance.playInterfaceSound("BuyCardsPanel_open_offer_open_confirmation");
        this.PanelActive = false;
        this.waitingResponse = false;
        InterfaceMgr.Instance.openConfirmBuyOfferPopup(parent, new ConfirmBuyOfferPanel.CardClickPlayDelegate(this.doOfferClicked));
      }
      else
      {
        if (initialClick)
        {
          GameEngine.Instance.playInterfaceSound("BuyCardsPanel_open_offer");
          InterfaceMgr.Instance.BuyOfferMultiple = 0;
        }
        if (parent.Offer.CrownCost > GameEngine.Instance.World.ProfileCrowns)
        {
          BuyCrownsPopup buyCrownsPopup = new BuyCrownsPopup();
          buyCrownsPopup.init(parent.Offer.CrownCost - GameEngine.Instance.World.ProfileCrowns, this.ParentForm);
          buyCrownsPopup.Show((IWin32Window) this.ParentForm);
        }
        else
        {
          if (InterfaceMgr.Instance.BuyOfferMultiple == 0)
            InterfaceMgr.Instance.BuyOfferMultiple = 1;
          string ID = string.Empty;
          switch (parent.Offer.Category)
          {
            case "FARMING":
              ID = "CARD_OFFERS_Food_Pack";
              break;
            case "CASTLE":
              ID = "CARD_OFFERS_Castle_Pack";
              break;
            case "DEFENSE":
            case "DEFENCE":
              ID = "CARD_OFFERS_Defense_Pack";
              break;
            case "RANDOM":
              ID = "CARD_OFFERS_Random_Pack";
              break;
            case "INDUSTRY":
              ID = "CARD_OFFERS_Industry_Pack";
              break;
            case "RESEARCH":
              ID = "CARD_OFFERS_Industry_Pack";
              break;
            case "ARMY":
              ID = "CARD_OFFERS_Army_Pack";
              break;
            case "SUPERFARMING":
              ID = "CARD_OFFERS_Super_Food_Pack";
              break;
            case "SUPERDEFENSE":
            case "SUPERDEFENCE":
              ID = "CARD_OFFERS_Super_Defense_Pack";
              break;
            case "SUPERRANDOM":
              ID = "CARD_OFFERS_Super_Random_Pack";
              break;
            case "SUPERINDUSTRY":
              ID = "CARD_OFFERS_Super_Industry_Pack";
              break;
            case "SUPERARMY":
              ID = "CARD_OFFERS_Super_Army_Pack";
              break;
            case "ULTIMATEFARMING":
              ID = "CARD_OFFERS_Ultimate_Food_Pack";
              break;
            case "ULTIMATEDEFENSE":
            case "ULTIMATEDEFENCE":
              ID = "CARD_OFFERS_Ultimate_Defense_Pack";
              break;
            case "ULTIMATERANDOM":
              ID = "CARD_OFFERS_Ultimate_Random_Pack";
              break;
            case "ULTIMATEINDUSTRY":
              ID = "CARD_OFFERS_Ultimate_Industry_Pack";
              break;
            case "ULTIMATEARMY":
              ID = "CARD_OFFERS_Ultimate_Army_Pack";
              break;
            case "PLATINUM":
              ID = "CARD_OFFERS_Platinum_Pack";
              break;
          }
          string str = SK.Text(ID);
          if (MyMessageBox.Show(InterfaceMgr.Instance.BuyOfferMultiple.ToString() + " x " + str + Environment.NewLine + SK.Text("BuyCardsPanel_Crowns_Cost", "Crowns Cost") + " : " + (parent.Offer.CrownCost * InterfaceMgr.Instance.BuyOfferMultiple).ToString(), SK.Text("BuyCardsPanel_Confirm_Purchase", "Confirm Purchase"), MessageBoxButtons.OKCancel) != DialogResult.OK)
            return;
          this.lastoffer = parent;
          GameEngine.Instance.cardPackManager.PurchasePack(parent.Offer, new CardsEndResponseUIDelegate(this.buyPackCallback), (Control) this);
          this.labelTitle.Text = SK.Text("BuyCardsPanel_Buy_and_Open_Packs", "Buy and Open Card Packs: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
        }
      }
    }

    private void BuyPackAfterConfirmation()
    {
      try
      {
        UICardOffer lastoffer = this.lastoffer;
        GameEngine.Instance.cardPackManager.PurchasePack(lastoffer.Offer, new CardsEndResponseUIDelegate(this.buyPackCallback), (Control) this);
        this.labelTitle.Text = SK.Text("BuyCardsPanel_Buy_and_Open_Packs", "Buy and Open Card Packs: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
        this.confirmBuyCardPopUp.Close();
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log("Exception " + ex.ToString());
      }
    }

    private void buyPackCallback(ICardsResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        this.UpdatePacks();
      }
      else
      {
        this.labelTitle.Text = SK.Text("BuyCardsPanel_Buy_and_Open_Packs", "Buy and Open Card Packs: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
        int num = (int) MyMessageBox.Show(response.Message, SK.Text("GENERIC_Error", "Error"));
      }
    }

    private void ClosePopUp()
    {
      if (this.confirmBuyCardPopUp == null)
        return;
      if (this.confirmBuyCardPopUp.Created)
        this.confirmBuyCardPopUp.Close();
      this.confirmBuyCardPopUp = (MyMessageBoxPopUp) null;
    }

    public void OpenPack() => this.doOpenPack(true);

    public void doOpenPack(bool initialClick)
    {
      if (GameEngine.Instance.World.WorldEnded || GameEngine.Instance.cardPackManager.openingPack)
        return;
      UICardPack clickedControl = (UICardPack) this.ClickedControl;
      if (initialClick && Program.mySettings.OpenMultipleCardPacks)
      {
        GameEngine.Instance.playInterfaceSound("BuyCardsPanel_open_pack_open_confirmation");
        this.PanelActive = false;
        this.waitingResponse = false;
        InterfaceMgr.Instance.openConfirmOpenPackPopup(clickedControl, new ConfirmOpenPackPanel.CardClickPlayDelegate(this.doOpenPack));
      }
      else
      {
        if (initialClick)
        {
          GameEngine.Instance.playInterfaceSound("BuyCardsPanel_open_pack");
          InterfaceMgr.Instance.OpenPackMultiple = 0;
        }
        if (GameEngine.Instance.cardPackManager.TryOpenPack(clickedControl.OfferID, new CardsEndResponseUIDelegate(this.PackOpened), (Control) this))
          return;
        int num = (int) MyMessageBox.Show(SK.Text("BuyCardsPanel_No_More_Available", "You have no more packs of that type available."), SK.Text("GENERIC_Error", "Error"));
      }
    }

    public void PackOpened(ICardsResponse response)
    {
      if (response.SuccessCode.Value == 1)
      {
        try
        {
          this.CloseGrey();
          for (int index = 0; index < 50; ++index)
          {
            if (this.revealCards[index] != null)
            {
              this.revealCards[index].clearControls();
              this.greyout.removeControl((CustomSelfDrawPanel.CSDControl) this.revealCards[index]);
            }
          }
          bool flag = false;
          int index1 = 0;
          List<UICard> uiCardList = new List<UICard>();
          string[] strArray1 = response.Strings.Split(";".ToCharArray());
          int length = strArray1.Length;
          int num = -10 * (length / 5 - 1);
          foreach (string str in strArray1)
          {
            string[] strArray2 = str.Split(",".ToCharArray());
            if (strArray2.Length == 2)
            {
              GameEngine.Instance.cardsManager.ProfileCards.Add(Convert.ToInt32(strArray2[0].Trim()), CardTypes.getCardDefinitionFromString(strArray2[1].Trim()));
              UICard uiCard = BuyCardsPanel.makeUICard(CardTypes.getCardDefinitionFromString(strArray2[1].Trim()), Convert.ToInt32(strArray2[0].Trim()), GameEngine.Instance.World.getRank() + 1);
              uiCard.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardClickPlayTrueFromClickFalseValidate));
              uiCardList.Add(uiCard);
            }
          }
          uiCardList.Sort((IComparer<UICard>) UICard.cardsNameComparer);
          foreach (UICard uiCard in uiCardList)
          {
            this.revealCards[index1] = uiCard;
            this.revealCards[index1].Position = new Point(15 + index1 % 5 * (200 - length / 5) + index1 / 5 * 5, 95 + num + 20 * (index1 / 5));
            this.greyout.addControl((CustomSelfDrawPanel.CSDControl) this.revealCards[index1]);
            switch (this.revealCards[index1].Definition.cardGrade)
            {
              case 524288:
              case 1048576:
              case 2097152:
              case 4194304:
                flag = true;
                break;
            }
            ++index1;
          }
          GFXLibrary.Instance.closeBigCardsLoader();
          this.numRevealCards = index1;
          if (flag)
            GameEngine.Instance.playInterfaceSound("BuyCardsPanel_found_rare_card");
          this.OpenGrey();
          this.UpdatePacks();
        }
        catch (Exception ex)
        {
          int num = (int) MyMessageBox.Show(ex.Message, SK.Text("GENERIC_Error", "Error"));
        }
      }
      else
      {
        int num = (int) MyMessageBox.Show(response.Message, SK.Text("BuyCardsPanel_Could_Not_Open_Pack", "Could not open pack."));
        this.UpdatePacks();
      }
    }

    public void update()
    {
      this.diamondAnimFrame = (int) ((DateTime.Now - this.diamondAnimStartTime).TotalMilliseconds / 33.0);
      for (int index = 0; index < this.numRevealCards; ++index)
      {
        UICard revealCard = this.revealCards[index];
        if (revealCard.Definition.cardGrade == 524288)
        {
          revealCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_diamond_anim.Length];
          revealCard.bigGradeImage.invalidateXtra();
        }
        else if (revealCard.Definition.cardGrade == 1048576)
        {
          revealCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond2_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_diamond2_anim.Length];
          revealCard.bigGradeImage.invalidateXtra();
        }
        else if (revealCard.Definition.cardGrade == 2097152)
        {
          revealCard.bigGradeImage.Image = (Image) GFXLibrary.card_diamond3_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_diamond3_anim.Length];
          revealCard.bigGradeImage.invalidateXtra();
        }
        else if (revealCard.Definition.cardGrade == 262144)
        {
          revealCard.bigGradeImage.Image = (Image) GFXLibrary.card_gold_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_gold_anim.Length];
          revealCard.bigGradeImage.invalidateXtra();
        }
        else if (revealCard.Definition.cardGrade == 4194304)
        {
          revealCard.bigGradeImage.Image = (Image) GFXLibrary.card_sapphire_anim[this.diamondAnimFrame / 1 % GFXLibrary.card_sapphire_anim.Length];
          revealCard.bigGradeImage.invalidateXtra();
        }
      }
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.closePlayCardsWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    public static UICard makeUICard(CardTypes.CardDefinition def, int userid, int playerRank)
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

    private void cardClickPlayTrueFromClickFalseValidate() => this.doCardClickPlay(true, false);

    private void cardClickPlayFalseFromClickTrueValidate() => this.doCardClickPlay(false, true);

    private void doCardClickPlay(bool fromClick, bool fromValidate)
    {
      if (GameEngine.Instance.World.WorldEnded || this.waitingResponse || this.ClickedControl.GetType() != typeof (UICard) && fromClick)
        return;
      UICard uiCard = !fromClick ? this.lastRequestCard : (UICard) this.ClickedControl;
      this.lastRequestCard = uiCard;
      this.waitingResponse = true;
      XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
      this.selectedVillage = -1;
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      if (!GameEngine.Instance.World.isCapital(selectedMenuVillage))
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
      else if (fromClick && Program.mySettings.ConfirmPlayCard)
      {
        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_open_confirmation");
        this.PanelActive = false;
        this.waitingResponse = false;
        InterfaceMgr.Instance.openConfirmPlayCardPopup(uiCard.Definition, new ConfirmPlayCardPanel.CardClickPlayDelegate(this.doCardClickPlay));
      }
      else if (!fromValidate && CardTypes.cardNeedsValidation(CardTypes.getCardType(uiCard.Definition.id)))
      {
        this.validateCardPossible(CardTypes.getCardType(uiCard.Definition.id), this.selectedVillage);
      }
      else
      {
        if (InterfaceMgr.Instance.getCardWindow() != null)
          CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.getCardWindow());
        if (fromClick)
          GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card");
        forEndpoint.PlayUserCard((ICardsRequest) new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), uiCard.UserIDList[0].ToString(), this.selectedVillage.ToString(), RemoteServices.Instance.ProfileWorldID.ToString()), new CardsEndResponseDelegate(this.CardPlayed), (Control) this);
        try
        {
          GameEngine.Instance.cardsManager.removeProfileCard(uiCard.UserIDList[0]);
          uiCard.Visible = false;
        }
        catch (Exception ex)
        {
          int num4 = (int) MyMessageBox.Show(ex.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
        }
        this.greyout.invalidate();
      }
    }

    private void CardPlayed(ICardsProvider provider, ICardsResponse response)
    {
      if (!response.SuccessCode.HasValue || response.SuccessCode.Value != 1)
      {
        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_failed");
        int num1 = (int) MyMessageBox.Show(CardsManager.translateCardError(response.Message, this.lastRequestCard.Definition.id), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
        try
        {
          GameEngine.Instance.cardsManager.addProfileCard(this.lastRequestCard.UserID, CardTypes.getStringFromCard(this.lastRequestCard.Definition.id));
          this.lastRequestCard.Visible = true;
          this.greyout.invalidate();
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
        this.greyout.removeControl((CustomSelfDrawPanel.CSDControl) this.lastRequestCard);
        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_success");
        GameEngine.Instance.cardsManager.ProfileCardsSet.Remove(this.lastRequestCard.UserID);
        GameEngine.Instance.cardsManager.CardPlayed(this.lastRequestCard.Definition.cardCategory, this.lastRequestCard.Definition.id, this.selectedVillage);
        GameEngine.Instance.cardsManager.addRecentCard(this.lastRequestCard.Definition.id);
      }
      this.waitingResponse = false;
    }

    public void validateCardPossible(int cardType, int villageID)
    {
      RemoteServices.Instance.set_PreValidateCardToBePlayed_UserCallBack(new RemoteServices.PreValidateCardToBePlayed_UserCallBack(this.preValidateCardToBePlayedCallBack));
      RemoteServices.Instance.PreValidateCardToBePlayed(cardType, villageID);
    }

    private void ContinuePreValidateCardToBePlayed()
    {
      PreValidateCardToBePlayed_ReturnType returnDataRef = this.returnDataRef;
      if (returnDataRef.canPlayFully)
        this.doCardClickPlay(false, true);
      else if (returnDataRef.canPlayPartially)
      {
        string str = "";
        switch (returnDataRef.cardType)
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
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_5", "Amount of Food gained will be") + " : " + returnDataRef.numCanPlay.ToString();
            break;
          case 3109:
          case 3110:
          case 3111:
          case 3112:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_6", "Amount of Ale gained will be") + " : " + returnDataRef.numCanPlay.ToString();
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
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_7", "Amount of Resources gained will be") + " : " + returnDataRef.numCanPlay.ToString();
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
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_8", "Amount of Honour Goods gained will be") + " : " + returnDataRef.numCanPlay.ToString();
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
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_9", "Number of Weapons gained will be") + " : " + returnDataRef.numCanPlay.ToString();
            break;
          case 3169:
          case 3170:
          case 3171:
          case 3172:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_10", "Amount of Armour gained will be") + " : " + returnDataRef.numCanPlay.ToString();
            break;
          case 3177:
          case 3178:
          case 3179:
          case 3180:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_21", "Number of Catapults gained will be") + " : " + returnDataRef.numCanPlay.ToString();
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
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_1", "Number of Troops that can be recruited") + " : " + returnDataRef.numCanPlay.ToString();
            break;
          case 3287:
          case 3288:
          case 3289:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_2", "Number of Scouts that can be recruited") + " : " + returnDataRef.numCanPlay.ToString();
            break;
          case 3290:
          case 3291:
          case 3292:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_3", "Number of Monks that can be recruited") + " : " + returnDataRef.numCanPlay.ToString();
            break;
          case 3293:
          case 3294:
          case 3295:
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_4", "Number of Merchants that can be recruited") + " : " + returnDataRef.numCanPlay.ToString();
            break;
        }
        if (MyMessageBox.Show(str + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo) != DialogResult.Yes)
          return;
        this.doCardClickPlay(false, true);
      }
      else if (returnDataRef.otherErrorCode != 0)
      {
        if (returnDataRef.otherErrorCode == -2)
        {
          int num1 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnDataRef.cardType, 5), SK.Text("GENERIC_Error", "Error"));
        }
        else
        {
          if (returnDataRef.otherErrorCode != -3)
            return;
          GameEngine.Instance.displayedVillageLost(returnDataRef.villageID, true);
        }
      }
      else
      {
        switch (returnDataRef.cardType)
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
            int num2 = (int) MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_101", "Not enough space in the Granary."), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3109:
          case 3110:
          case 3111:
          case 3112:
            int num3 = (int) MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_102", "Not enough space in the Inn."), SK.Text("GENERIC_Error", "Error"));
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
            int num4 = (int) MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_103", "Not enough space on the Stockpile."), SK.Text("GENERIC_Error", "Error"));
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
            int num5 = (int) MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_104", "Not enough space in the Village Hall."), SK.Text("GENERIC_Error", "Error"));
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
            int num6 = (int) MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_105", "Not enough space in the Armoury."), SK.Text("GENERIC_Error", "Error"));
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
            int num7 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnDataRef.cardType, 1), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3287:
          case 3288:
          case 3289:
            int num8 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnDataRef.cardType, 2), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3290:
          case 3291:
          case 3292:
            int num9 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnDataRef.cardType, 3), SK.Text("GENERIC_Error", "Error"));
            break;
          case 3293:
          case 3294:
          case 3295:
            int num10 = (int) MyMessageBox.Show(CardsManager.translateCardError("", returnDataRef.cardType, 4), SK.Text("GENERIC_Error", "Error"));
            break;
        }
      }
    }

    public void preValidateCardToBePlayedCallBack(PreValidateCardToBePlayed_ReturnType returnData)
    {
      this.waitingResponse = false;
      if (!returnData.Success)
        return;
      this.returnDataRef = returnData;
      if (CardTypes.isMercenaryTroopCardType(returnData.cardType) && returnData.otherErrorCode == 9999)
      {
        switch (MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_UNIT_SPACE", "There is not enough unit space to accomodate these troops. If troops are dispatched from this village some may be lost upon their return.") + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo))
        {
          case DialogResult.Yes:
            this.ContinuePreValidateCardToBePlayed();
            break;
        }
      }
      else
        this.ContinuePreValidateCardToBePlayed();
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
