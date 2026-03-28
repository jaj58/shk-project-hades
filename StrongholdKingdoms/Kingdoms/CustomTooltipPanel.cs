// Decompiled with JetBrains decompiler
// Type: Kingdoms.CustomTooltipPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CustomTooltipPanel : CustomSelfDrawPanel
  {
    public const int MAX_TOOLTIP_WIDTH = 350;
    private IContainer components;
    private CustomSelfDrawPanel.CSDLabel tooltipLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
    private string lastText = "";
    private int lastTooltip = -1;
    private int lastData = -1;
    private CustomSelfDrawPanel.CSDLabel cardTooltipName = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel cardTooltipDescription = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel cardTooltipEffect = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel cardTooltipTimeLeft = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage cardTooltipImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage cardTooltipImage2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage timeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel peasantsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel peasantsValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel housingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel housingValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel spareWorkersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel spareWorkersValue = new CustomSelfDrawPanel.CSDLabel();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Name = nameof (CustomTooltipPanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }

    public CustomTooltipPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void setText(string text, int tooltipID, int data, Form parent, bool force)
    {
      switch (tooltipID)
      {
        case 141:
          this.createVillagePeasant(tooltipID, data, parent, force);
          break;
        case 10000:
        case 10101:
          this.createCardTooltip(tooltipID, data, parent, force);
          break;
        default:
          if (!(this.lastText != text) && !force)
            break;
          this.lastText = text;
          this.lastTooltip = tooltipID;
          this.clearControls();
          this.tooltipLabel.Text = text;
          this.tooltipLabel.Color = ARGBColors.Black;
          this.tooltipLabel.Position = new Point(2, 2);
          this.tooltipLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
          Graphics graphics = this.CreateGraphics();
          Size size1 = graphics.MeasureString(text, this.tooltipLabel.Font, 350).ToSize();
          graphics.Dispose();
          this.tooltipLabel.Size = new Size(size1.Width + 1, size1.Height + 1);
          Size size2 = new Size(size1.Width + 4 + 1, size1.Height + 4 + 1);
          if (!size2.Equals((object) parent.Size))
            parent.Size = size2;
          this.tooltipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          this.background.Size = size2;
          this.background.Position = new Point(0, 0);
          this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
          this.background.Create((Image) GFXLibrary.cardpanel_grey_9slice_left_top, (Image) GFXLibrary.cardpanel_grey_9slice_middle_top, (Image) GFXLibrary.cardpanel_grey_9slice_right_top, (Image) GFXLibrary.cardpanel_grey_9slice_left_middle, (Image) GFXLibrary.cardpanel_grey_9slice_middle_middle, (Image) GFXLibrary.cardpanel_grey_9slice_right_middle, (Image) GFXLibrary.cardpanel_grey_9slice_left_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_middle_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_right_bottom);
          this.background.addControl((CustomSelfDrawPanel.CSDControl) this.tooltipLabel);
          this.Invalidate();
          parent.Invalidate();
          break;
      }
    }

    public void hidingTooltip()
    {
      this.lastText = "";
      this.lastTooltip = -1;
      this.lastData = -1;
    }

    public void createCardTooltip(int tooltipID, int data, Form parent, bool force)
    {
      if (this.lastTooltip != tooltipID || this.lastData != data || force)
      {
        this.lastText = "x";
        this.lastData = data;
        this.lastTooltip = tooltipID;
        parent.Size = new Size(300, 240);
        this.clearControls();
        this.background.Size = parent.Size;
        this.background.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
        this.background.Create((Image) GFXLibrary.cardpanel_grey_9slice_left_top, (Image) GFXLibrary.cardpanel_grey_9slice_middle_top, (Image) GFXLibrary.cardpanel_grey_9slice_right_top, (Image) GFXLibrary.cardpanel_grey_9slice_left_middle, (Image) GFXLibrary.cardpanel_grey_9slice_middle_middle, (Image) GFXLibrary.cardpanel_grey_9slice_right_middle, (Image) GFXLibrary.cardpanel_grey_9slice_left_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_middle_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_right_bottom);
        this.cardTooltipName.Text = CardTypes.getDescriptionFromCard(data);
        this.cardTooltipName.Color = ARGBColors.Black;
        this.cardTooltipName.Position = new Point(100, 4);
        this.cardTooltipName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.cardTooltipName.Size = new Size(190, 40);
        this.cardTooltipName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.cardTooltipName);
        this.cardTooltipDescription.Text = CardTypes.getEffectTextFromCard(data);
        this.cardTooltipDescription.Color = ARGBColors.Black;
        this.cardTooltipDescription.Position = new Point(100, 50);
        this.cardTooltipDescription.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.cardTooltipDescription.Size = new Size(190, 100);
        this.cardTooltipDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.cardTooltipDescription);
        this.cardTooltipImage.Image = (Image) GFXLibrary.Instance.getCardImageBig(data);
        GFXLibrary.Instance.closeBigCardsLoader();
        this.cardTooltipImage.Position = new Point(4, 4);
        this.cardTooltipImage.Size = new Size(92, 131);
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.cardTooltipImage);
        switch (CardTypes.getColourFromCard(data))
        {
          case 1:
            this.cardTooltipImage2.Image = (Image) GFXLibrary.BlueCardOverlayBig;
            break;
          case 2:
            this.cardTooltipImage2.Image = (Image) GFXLibrary.GreenCardOverlayBig;
            break;
          case 3:
            this.cardTooltipImage2.Image = (Image) GFXLibrary.PurpleCardOverlayBig;
            break;
          case 4:
            this.cardTooltipImage2.Image = (Image) GFXLibrary.RedCardOverlayBig;
            break;
          case 5:
            this.cardTooltipImage2.Image = (Image) GFXLibrary.YellowCardOverlayBig;
            break;
        }
        this.cardTooltipImage2.Size = this.cardTooltipImage.Size;
        this.cardTooltipImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardTooltipImage2);
        if (tooltipID == 10000)
        {
          WorldData localWorldData = GameEngine.Instance.LocalWorldData;
          DateTime currentServerTime = VillageMap.getCurrentServerTime();
          CardData userCardData = GameEngine.Instance.cardsManager.UserCardData;
          DateTime minValue = DateTime.MinValue;
          int secsLeft = 0;
          int length = userCardData.cards.Length;
          for (int index = 0; index < length; ++index)
          {
            int card = userCardData.cards[index];
            if (card == data)
            {
              TimeSpan timeSpan = userCardData.cardsExpiry[index] - currentServerTime;
              CardTypes.getCardDuration(card);
              secsLeft = (int) timeSpan.TotalSeconds;
              if (secsLeft < 0)
                secsLeft = 0;
              if (timeSpan.TotalDays > 100.0)
              {
                secsLeft = -1;
                break;
              }
              break;
            }
          }
          if (secsLeft < 0)
          {
            this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_EXPIRES", "Expires when used");
          }
          else
          {
            string buildTimeString = VillageMap.createBuildTimeString(secsLeft);
            this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_EXPIRES_IN", "Expires In") + " : " + buildTimeString;
          }
        }
        else if (CardTypes.getCardSubType(data) == 3072)
        {
          this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_INSTANT", "Instant Card");
        }
        else
        {
          int cardDuration = CardTypes.getCardDuration(data);
          if (cardDuration > 18250 || cardDuration == 0)
          {
            this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_EXPIRES", "Expires when used");
          }
          else
          {
            string buildTimeString = VillageMap.createBuildTimeString(cardDuration * 60 * 60);
            this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_DURATION", "Duration") + " : " + buildTimeString;
          }
        }
        this.timeImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_time;
        this.timeImage.Position = new Point(10, 158);
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.timeImage);
        this.cardTooltipTimeLeft.Color = ARGBColors.Black;
        this.cardTooltipTimeLeft.Position = new Point(40, 160);
        this.cardTooltipTimeLeft.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.cardTooltipTimeLeft.Size = new Size(250, 40);
        this.cardTooltipTimeLeft.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.cardTooltipTimeLeft);
        string str = "";
        double cardEffectValue = CardTypes.getCardEffectValue(data);
        NumberFormatInfo provider = (double) (int) cardEffectValue != cardEffectValue ? (CardTypes.getCardType(data) != 2061 ? GameEngine.NFI_D1 : GameEngine.NFI_D2) : GameEngine.NFI;
        if (CardTypes.addX(data))
          str = "x" + cardEffectValue.ToString("N", (IFormatProvider) provider);
        else if (CardTypes.addPlus(data))
          str = "+" + cardEffectValue.ToString("N", (IFormatProvider) provider);
        else if (cardEffectValue != 0.0)
          str = cardEffectValue.ToString("N", (IFormatProvider) provider);
        if (CardTypes.addPercent(data))
          str += "%";
        if (str.Length > 0)
        {
          switch (CardTypes.getCardType(data))
          {
            case 3008:
            case 3009:
            case 3010:
              str = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_DIPLOMACY", "50% Chance of Averting Enemy Attacks");
              break;
            case 3077:
            case 3078:
            case 3079:
              int rank = GameEngine.Instance.World.getRank();
              str = ((int) ((double) GameEngine.Instance.LocalWorldData.ranks_HonourPerLevel[rank] * cardEffectValue)).ToString("N", (IFormatProvider) GameEngine.NFI);
              break;
          }
          this.cardTooltipEffect.Text = str + " " + CustomTooltipPanel.getCardEffectString(data);
          this.cardTooltipEffect.Color = ARGBColors.Black;
          this.cardTooltipEffect.Position = new Point(10, 190);
          this.cardTooltipEffect.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
          this.cardTooltipEffect.Size = new Size(290, 60);
          this.cardTooltipEffect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          this.background.addControl((CustomSelfDrawPanel.CSDControl) this.cardTooltipEffect);
        }
        else if (tooltipID == 10101)
        {
          this.cardTooltipEffect.Text = CustomTooltipPanel.getCardEffectString(data);
          this.cardTooltipEffect.Color = ARGBColors.Black;
          this.cardTooltipEffect.Position = new Point(10, 190);
          this.cardTooltipEffect.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
          this.cardTooltipEffect.Size = new Size(290, 60);
          this.cardTooltipEffect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          this.background.addControl((CustomSelfDrawPanel.CSDControl) this.cardTooltipEffect);
        }
        else
          this.cardTooltipEffect.Text = "";
        this.Invalidate();
        parent.Invalidate();
      }
      else
      {
        WorldData localWorldData = GameEngine.Instance.LocalWorldData;
        DateTime currentServerTime = VillageMap.getCurrentServerTime();
        CardData userCardData = GameEngine.Instance.cardsManager.UserCardData;
        DateTime minValue = DateTime.MinValue;
        int secsLeft = 0;
        int length = userCardData.cards.Length;
        for (int index = 0; index < length; ++index)
        {
          int card = userCardData.cards[index];
          if (card == data)
          {
            TimeSpan timeSpan = userCardData.cardsExpiry[index] - currentServerTime;
            CardTypes.getCardDuration(card);
            secsLeft = (int) timeSpan.TotalSeconds;
            if (secsLeft < 0)
              secsLeft = 0;
            if (timeSpan.TotalDays > 100.0)
            {
              secsLeft = -1;
              break;
            }
            break;
          }
        }
        if (secsLeft < 0)
        {
          this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_EXPIRES", "Expires when used");
        }
        else
        {
          string buildTimeString = VillageMap.createBuildTimeString(secsLeft);
          this.cardTooltipTimeLeft.Text = SK.Text("TOOLTIP_CARD_EXPIRES_IN", "Expires In") + " : " + buildTimeString;
        }
      }
    }

    public void createVillagePeasant(int tooltipID, int data, Form parent, bool force)
    {
      if (this.lastTooltip != tooltipID || this.lastData != data || force)
      {
        this.lastText = "x";
        this.lastData = data;
        this.lastTooltip = tooltipID;
        Graphics graphics = this.CreateGraphics();
        Font font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        Size size1 = graphics.MeasureString(SK.Text("TOOLTIP_VILAGEMAP_TOTAL_PEASANTS", "Total Peasants"), font, 800).ToSize();
        Size size2 = graphics.MeasureString(SK.Text("TOOLTIP_VILAGEMAP_UNEMPLOYEED_PEASANTS", "Unemployed Peasants"), font, 800).ToSize();
        Size size3 = graphics.MeasureString(SK.Text("TOOLTIP_VILAGEMAP_HOUSING_CAPACITY", "Housing Capacity"), font, 800).ToSize();
        int width1 = size1.Width;
        if (size2.Width > width1)
          width1 = size2.Width;
        if (size3.Width > width1)
          width1 = size3.Width;
        int width2 = width1 + 60;
        graphics.Dispose();
        parent.Size = new Size(width2, 100);
        this.clearControls();
        this.background.Size = parent.Size;
        this.background.Position = new Point(0, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
        this.background.Create((Image) GFXLibrary.cardpanel_grey_9slice_left_top, (Image) GFXLibrary.cardpanel_grey_9slice_middle_top, (Image) GFXLibrary.cardpanel_grey_9slice_right_top, (Image) GFXLibrary.cardpanel_grey_9slice_left_middle, (Image) GFXLibrary.cardpanel_grey_9slice_middle_middle, (Image) GFXLibrary.cardpanel_grey_9slice_right_middle, (Image) GFXLibrary.cardpanel_grey_9slice_left_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_middle_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_right_bottom);
        this.peasantsLabel.Text = SK.Text("TOOLTIP_VILAGEMAP_TOTAL_PEASANTS", "Total Peasants");
        this.peasantsLabel.Color = ARGBColors.Black;
        this.peasantsLabel.Position = new Point(10, 10);
        this.peasantsLabel.Font = font;
        this.peasantsLabel.Size = new Size(width2 - 20, 30);
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.peasantsLabel);
        this.peasantsValue.Text = "0";
        this.peasantsValue.Color = ARGBColors.Black;
        this.peasantsValue.Position = new Point(10, 10);
        this.peasantsValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.peasantsValue.Size = new Size(width2 - 20, 30);
        this.peasantsValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.peasantsValue);
        this.spareWorkersLabel.Text = SK.Text("TOOLTIP_VILAGEMAP_UNEMPLOYEED_PEASANTS", "Unemployed Peasants");
        this.spareWorkersLabel.Color = ARGBColors.Black;
        this.spareWorkersLabel.Position = new Point(10, 40);
        this.spareWorkersLabel.Font = font;
        this.spareWorkersLabel.Size = new Size(width2 - 20, 30);
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.spareWorkersLabel);
        this.spareWorkersValue.Text = "0";
        this.spareWorkersValue.Color = ARGBColors.Black;
        this.spareWorkersValue.Position = new Point(10, 40);
        this.spareWorkersValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.spareWorkersValue.Size = new Size(width2 - 20, 30);
        this.spareWorkersValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.spareWorkersValue);
        this.housingLabel.Text = SK.Text("TOOLTIP_VILAGEMAP_HOUSING_CAPACITY", "Housing Capacity");
        this.housingLabel.Color = ARGBColors.Black;
        this.housingLabel.Position = new Point(10, 70);
        this.housingLabel.Font = font;
        this.housingLabel.Size = new Size(width2 - 20, 30);
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.housingLabel);
        this.housingValue.Text = "0";
        this.housingValue.Color = ARGBColors.Black;
        this.housingValue.Position = new Point(10, 70);
        this.housingValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.housingValue.Size = new Size(width2 - 20, 30);
        this.housingValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.housingValue);
        this.Invalidate();
        parent.Invalidate();
      }
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      this.peasantsValue.Text = village.m_totalPeople.ToString();
      this.spareWorkersValue.Text = village.m_spareWorkers.ToString();
      this.housingValue.Text = village.m_housingCapacity.ToString();
    }

    public void update()
    {
    }

    public static string getFullCardEffectString(int data)
    {
      string str = "";
      double cardEffectValue = CardTypes.getCardEffectValue(data);
      NumberFormatInfo provider = (double) (int) cardEffectValue != cardEffectValue ? (CardTypes.getCardType(data) != 2061 ? GameEngine.NFI_D1 : GameEngine.NFI_D2) : GameEngine.NFI;
      if (CardTypes.addX(data))
        str = "x" + cardEffectValue.ToString("N", (IFormatProvider) provider);
      else if (CardTypes.addPlus(data))
        str = "+" + cardEffectValue.ToString("N", (IFormatProvider) provider);
      else if (cardEffectValue != 0.0)
        str = cardEffectValue.ToString("N", (IFormatProvider) provider);
      if (CardTypes.addPercent(data))
        str += "%";
      string cardEffectString;
      if (str.Length > 0)
      {
        switch (CardTypes.getCardType(data))
        {
          case 3008:
          case 3009:
          case 3010:
            str = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_DIPLOMACY", "50% Chance of Averting Enemy Attacks");
            break;
          case 3077:
          case 3078:
          case 3079:
            int rank = GameEngine.Instance.World.getRank();
            str = ((int) ((double) GameEngine.Instance.LocalWorldData.ranks_HonourPerLevel[rank] * cardEffectValue)).ToString("N", (IFormatProvider) GameEngine.NFI);
            break;
        }
        cardEffectString = str + " " + CustomTooltipPanel.getCardEffectString(data);
      }
      else
        cardEffectString = CustomTooltipPanel.getCardEffectString(data);
      return cardEffectString;
    }

    public static string getCardEffectString(int card)
    {
      string cardEffectString = "";
      switch (CardTypes.getCardType(card))
      {
        case 257:
        case 258:
        case 259:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MASTER_MASON", "Castle build speed");
          break;
        case 263:
          cardEffectString = "";
          break;
        case 264:
        case 267:
        case 268:
          cardEffectString = "";
          break;
        case 265:
        case 269:
        case 270:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SURPRISE_ATTACK", "Knights Charge");
          break;
        case 266:
        case 271:
        case 272:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_THE_LAST_STAND", "Knights Charge");
          break;
        case 513:
        case 514:
        case 515:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ORCHARD_MANAGEMENT", "Increase in Apple Production");
          break;
        case 516:
        case 517:
        case 518:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MILK_MAIDS", "Increase in Cheese Production");
          break;
        case 519:
        case 520:
        case 521:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PIG_BREEDING", "Increase in Meat Production");
          break;
        case 522:
        case 523:
        case 524:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_HARVESTING", "Increase in Bread Production");
          break;
        case 525:
        case 526:
        case 527:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_VETERAN_FARMER", "Increase in All Food Production");
          break;
        case 528:
        case 529:
        case 530:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CROPPING", "Increase in Vegetable Production");
          break;
        case 531:
        case 532:
        case 533:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FISHING", "Increase in Fish Production");
          break;
        case 534:
        case 535:
        case 536:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SIMPLE_FEAST", "Increase in Popularity from Rations");
          break;
        case 537:
        case 538:
        case 539:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_HOPS_TENDING", "Increase in Ale Production");
          break;
        case 540:
        case 541:
        case 542:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BAR_KEEPING", "Increase in Popularity from Ale Consumption");
          break;
        case 769:
        case 770:
        case 771:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_WOODSMAN_SHIP", "Increase in Wood Production");
          break;
        case 772:
        case 773:
        case 774:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_STONE_CRAFT", "Increase in Stone Production");
          break;
        case 775:
        case 776:
        case 777:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IRON_SMELTING", "Increase in Iron Production");
          break;
        case 778:
        case 779:
        case 780:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PITCH_EXTRACTION", "Increase in Pitch Production");
          break;
        case 781:
        case 782:
        case 783:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_HAULAGE", "Increase in All Raw Material Production");
          break;
        case 1025:
        case 1026:
        case 1027:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BODKIN_CASTING", "Increase in Bow Production");
          break;
        case 1028:
        case 1029:
        case 1030:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PIKE_CRAFT", "Increase in Pike Production");
          break;
        case 1031:
        case 1032:
        case 1033:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SWORD_CRAFT", "Increase in Sword Production");
          break;
        case 1034:
        case 1035:
        case 1036:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ARMOUR_WORKING", "Increase in Armour Production");
          break;
        case 1037:
        case 1038:
        case 1039:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SIEGE_ENGINEERS", "Increase in Catapult Production");
          break;
        case 1281:
        case 1282:
        case 1283:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_LAVISH_BANQUETING", "Increase in Honour When Holding a Banquet");
          break;
        case 1284:
        case 1285:
        case 1286:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_DEER_STALKING", "Increase in Venison Production");
          break;
        case 1287:
        case 1288:
        case 1289:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FURNITURE_MAKING", "Increase in Furniture Production");
          break;
        case 1290:
        case 1291:
        case 1292:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_METAL_CRAFTS", "Increase in Metalware Production");
          break;
        case 1293:
        case 1294:
        case 1295:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_TAILORING", "Increase in Clothes Production");
          break;
        case 1296:
        case 1297:
        case 1298:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_VINTNERS", "Increase in Wine Production");
          break;
        case 1299:
        case 1300:
        case 1301:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SALT_WORKING", "Increase in Salt Production");
          break;
        case 1302:
        case 1303:
        case 1304:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CULINARY_SKILLS", "Increase in Spice Production");
          break;
        case 1305:
        case 1306:
        case 1307:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FINE_ATTIRE", "Increase in Silk Production");
          break;
        case 1537:
        case 1538:
        case 1539:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CARTERS", "Increase in Merchant Speed");
          break;
        case 1541:
        case 1542:
        case 1543:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_TRADE_CARAVANS", "Increase in Merchant Carrying Capacity");
          break;
        case 1800:
        case 1801:
        case 1802:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CONSTRUCTION_TECHNIQUES", "Increase in Village Building Build Speed");
          break;
        case 2049:
        case 2050:
        case 2051:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_POLITICS", "Increase in Monk Votes");
          break;
        case 2052:
        case 2053:
        case 2054:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_WEDDINGS", "Increase in Blessing Duration");
          break;
        case 2055:
        case 2056:
        case 2057:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_INQUISITION_TECHNIQUES", "Increase in Inquisition Duration");
          break;
        case 2058:
        case 2059:
        case 2060:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_HEALING", "Increase in Healing");
          break;
        case 2061:
        case 2062:
        case 2063:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PROTECTION", "Increase in duration of interdiction given by monks");
          break;
        case 2064:
        case 2065:
        case 2066:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXCOMMUNICATION", "Increase in Excommunication Duration");
          break;
        case 2067:
        case 2068:
        case 2069:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ABSOLUTION", "Increase in Excommunication Duration Reduction");
          break;
        case 2070:
        case 2071:
        case 2072:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ENVOY", "Increase in Monks Speed");
          break;
        case 2305:
        case 2306:
        case 2307:
        case 2691:
        case 2692:
        case 2693:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_HORSE_BREEDING", "Increase in Scout Speed");
          break;
        case 2308:
        case 2309:
        case 2310:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_SCAVENGING", "Increase in Scout Carrying Capacity");
          break;
        case 2561:
        case 2562:
        case 2563:
        case 2694:
        case 2695:
        case 2696:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_DISCIPLINE", "Increase in Army Speed");
          break;
        case 2564:
        case 2565:
        case 2566:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_RECRUITMENT", "Cost of Troop Recruitment");
          break;
        case 2689:
        case 2690:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SCOUTING_RANGE", "Increase in your Honour Range");
          break;
        case 2817:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_HOUSEKEEPING", "Increase in House Capacity");
          break;
        case 2818:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_GRANARIES", "Increase in Granary Capacity");
          break;
        case 2819:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_STOCKPILING", "Increase in Stockpile Capacity");
          break;
        case 2820:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_CELLARING", "Increase in Inn Capacity");
          break;
        case 2821:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_ARMOURIES", "Increase in Armoury Capacity");
          break;
        case 2822:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXPANDED_KEEP_STORAGE", "Increase in Village Hall Capacity");
          break;
        case 2823:
        case 2824:
        case 2825:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_CULTURE", "Increase in Popularity To Honour Multiplier");
          break;
        case 2826:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FAIRER_JUSTICE", "Reduction in Negative Popularity from Justice Buildings");
          break;
        case 2827:
        case 2828:
        case 2829:
        case 2830:
          cardEffectString = "????";
          break;
        case 2831:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FESTIVAL", "Popularity Boost");
          break;
        case 2881:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_COMPLETED_CONTRACT", "Trade Completed Immediately");
          break;
        case 2882:
        case 2883:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_COMPLETED_CONTRACT", "Trades Completed Immediately");
          break;
        case 2887:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_COMPLETED_DELIVERY", "Delivery Completed Immediately");
          break;
        case 2888:
        case 2889:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ADVANCED_COMPLETED_DELIVERY", "Deliveries Completed Immediately");
          break;
        case 2945:
        case 2946:
        case 2947:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_GUARD_HOUSES", "Extra Spaces for Troops in your Castle");
          break;
        case 2948:
        case 2949:
        case 2950:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_WOODEN_DEFENCES", "Stronger Wooden Castle Structures");
          break;
        case 2951:
        case 2952:
        case 2953:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_STONE_WALLS", "Stronger Stone Walls");
          break;
        case 2954:
        case 2955:
        case 2956:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_STONE_STRUCTURES", "Stronger Stone Structures");
          break;
        case 2957:
        case 2958:
        case 2959:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_MOATS", "Deeper Moats");
          break;
        case 2960:
        case 2961:
        case 2962:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_PITS", "More Damage from Pits");
          break;
        case 2963:
        case 2964:
        case 2965:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IMPROVED_OIL_POTS", "Extra Range of Oil Pots");
          break;
        case 2966:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_KNIGHTS_CHARGE", "Increase in Knights Speed");
          break;
        case 2967:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXPERT_TURRETS", "Turret Firing Speed");
          break;
        case 2968:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXPERT_TUNNELLING", "Troops From a Tunnel");
          break;
        case 2969:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXPERT_BALLISTAE", "Ballistae Firing Speed");
          break;
        case 2970:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SUPER_TAX", "Extra Tax Band");
          break;
        case 2971:
        case 2972:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SUPER_TAX_ADVANCED", "Extra Tax Bands");
          break;
        case 2973:
        case 2974:
        case 2975:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_EXTRA_TAX", "Increase in Tax Collected");
          break;
        case 3031:
        case 3032:
        case 3033:
        case 3034:
        case 3035:
        case 3036:
        case 3037:
        case 3038:
        case 3039:
        case 3040:
        case 3041:
        case 3042:
        case 3043:
        case 3044:
        case 3045:
        case 3046:
        case 3047:
        case 3048:
        case 3049:
        case 3050:
        case 3051:
        case 3052:
        case 3053:
        case 3055:
        case 3056:
        case 3057:
        case 3058:
        case 3059:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_INSTANT_BUILDING", "Instant building available");
          break;
        case 3073:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_RESEARCH_POINT", "Research Point Added");
          break;
        case 3074:
        case 3075:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_RESEARCH_POINTS_2", "Research Points Added");
          break;
        case 3076:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FLAG", "Flag Added To Parish");
          break;
        case 3077:
        case 3078:
        case 3079:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BASIC_CHIVALRY", "Honour (Based on your Current Rank)");
          break;
        case 3080:
        case 3081:
        case 3082:
        case 3083:
        case 3084:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_GOLD_HORDE", "Gold Added To Your Treasury");
          break;
        case 3085:
        case 3086:
        case 3087:
        case 3088:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_APPLE_HAUL", "Apples Added To Your Granary");
          break;
        case 3089:
        case 3090:
        case 3091:
        case 3092:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CHEESE_HAUL", "Cheese Added To Your Granary");
          break;
        case 3093:
        case 3094:
        case 3095:
        case 3096:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MEAT_HAUL", "Meat Added To Your Granary");
          break;
        case 3097:
        case 3098:
        case 3099:
        case 3100:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BREAD_HAUL", "Bread Added To Your Granary");
          break;
        case 3101:
        case 3102:
        case 3103:
        case 3104:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_VEGETABLES_HAUL", "Vegetables Added To Your Granary");
          break;
        case 3105:
        case 3106:
        case 3107:
        case 3108:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FISH_HAUL", "Fish Added To Your Granary");
          break;
        case 3109:
        case 3110:
        case 3111:
        case 3112:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ALE_HAUL", "Ale Added To Your Inn");
          break;
        case 3113:
        case 3114:
        case 3115:
        case 3116:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_WOOD_HAUL", "Wood Added To Your Stockpile");
          break;
        case 3117:
        case 3118:
        case 3119:
        case 3120:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_STONE_HAUL", "Stone Added To Your Stockpile");
          break;
        case 3121:
        case 3122:
        case 3123:
        case 3124:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_IRON_HAUL", "Iron Added To Your Stockpile");
          break;
        case 3125:
        case 3126:
        case 3127:
        case 3128:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PITCH_HAUL", "Pitch Added To Your Stockpile");
          break;
        case 3129:
        case 3130:
        case 3131:
        case 3132:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_VENISON_HAUL", "Venison Added To Your Keep");
          break;
        case 3133:
        case 3134:
        case 3135:
        case 3136:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_FURNITURE_HAUL", "Furniture Added To Your Keep");
          break;
        case 3137:
        case 3138:
        case 3139:
        case 3140:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_METALWARE_HAUL", "Metalware Added To Your Keep");
          break;
        case 3141:
        case 3142:
        case 3143:
        case 3144:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CLOTHES_HAUL", "Clothes Added To Your Keep");
          break;
        case 3145:
        case 3146:
        case 3147:
        case 3148:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_WINE_HAUL", "Wine Added To Your Keep");
          break;
        case 3149:
        case 3150:
        case 3151:
        case 3152:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SALT_HAUL", "Salt Added To Your Keep");
          break;
        case 3153:
        case 3154:
        case 3155:
        case 3156:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SPICES_HAUL", "Spices Added To Your Keep");
          break;
        case 3157:
        case 3158:
        case 3159:
        case 3160:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SILK_HAUL", "Silk Added To Your Keep");
          break;
        case 3161:
        case 3162:
        case 3163:
        case 3164:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_BOWS_HAUL", "Bows Added To Your Armoury");
          break;
        case 3165:
        case 3166:
        case 3167:
        case 3168:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PIKES_HAUL", "Pikes Added To Your Armoury");
          break;
        case 3169:
        case 3170:
        case 3171:
        case 3172:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ARMOUR_HAUL", "Armour Added To Your Armoury");
          break;
        case 3173:
        case 3174:
        case 3175:
        case 3176:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_SWORDS_HAUL", "Swords Added To Your Armoury");
          break;
        case 3177:
        case 3178:
        case 3179:
        case 3180:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CATAPULTS_HAUL", "Catapults Added To Your Armoury");
          break;
        case 3201:
        case 3202:
        case 3203:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_CASTLE_DESIGNER", "Hours of ongoing Castle Construction Completed");
          break;
        case 3249:
        case 3250:
        case 3251:
        case 3252:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_ACADEMIC_STUDY", "Hours of Research Completed");
          break;
        case 3264:
        case 3265:
        case 3266:
        case 3267:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_PEASANTS_SMALL", "Peasants");
          break;
        case 3268:
        case 3269:
        case 3270:
        case 3271:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_ARCHERS_SMALL", "Archers");
          break;
        case 3272:
        case 3273:
        case 3274:
        case 3275:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_PIKEMEN_SMALL", "Pikemen");
          break;
        case 3276:
        case 3277:
        case 3278:
        case 3279:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_SWORDSMEN_SMALL", "Swordsmen");
          break;
        case 3280:
        case 3281:
        case 3282:
        case 3283:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_CATAPULTS_SMALL", "Catapults");
          break;
        case 3284:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_WALL_CONSTRUCTION_TEAM", "Hours of Castle Wall Construction Completed");
          break;
        case 3285:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MOAT_DIGGING_TEAM", "Hours of Moat Construction Completed");
          break;
        case 3286:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_PIT_CONSTRUCTION_TEAM", "Hours of Castle Pits Construction Completed");
          break;
        case 3287:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_SCOUTS_SMALL", "Scout");
          break;
        case 3288:
        case 3289:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_SCOUTS_MEDIUM", "Scouts");
          break;
        case 3290:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_MONKS_SMALL", "Monk");
          break;
        case 3291:
        case 3292:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_MONKS_MEDIUM", "Monks");
          break;
        case 3293:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_MERCHANTS_SMALL", "Merchant");
          break;
        case 3294:
        case 3295:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_MERCENARIES_MERCHANTS_MEDIUM", "Merchants");
          break;
        case 3296:
        case 3297:
        case 3298:
          cardEffectString = SK.Text("TOOLTIP_CARD_EFFECT_EXPLANATION_CARDTYPE_VILLAGERS_SMALL", "Villagers");
          break;
      }
      return cardEffectString;
    }

    public static string getCardDurationString(bool cardInPlay, int cardID)
    {
      string cardDurationString;
      if (cardInPlay)
      {
        WorldData localWorldData = GameEngine.Instance.LocalWorldData;
        DateTime currentServerTime = VillageMap.getCurrentServerTime();
        CardData userCardData = GameEngine.Instance.cardsManager.UserCardData;
        DateTime minValue = DateTime.MinValue;
        int secsLeft = 0;
        int length = userCardData.cards.Length;
        for (int index = 0; index < length; ++index)
        {
          int card = userCardData.cards[index];
          if (card == cardID)
          {
            TimeSpan timeSpan = userCardData.cardsExpiry[index] - currentServerTime;
            CardTypes.getCardDuration(card);
            secsLeft = (int) timeSpan.TotalSeconds;
            if (secsLeft < 0)
              secsLeft = 0;
            if (timeSpan.TotalDays > 100.0)
            {
              secsLeft = -1;
              break;
            }
            break;
          }
        }
        if (secsLeft < 0)
        {
          cardDurationString = SK.Text("TOOLTIP_CARD_EXPIRES", "Expires when used");
        }
        else
        {
          string buildTimeString = VillageMap.createBuildTimeString(secsLeft);
          cardDurationString = SK.Text("TOOLTIP_CARD_EXPIRES_IN", "Expires In") + " : " + buildTimeString;
        }
      }
      else if (CardTypes.getCardSubType(cardID) == 3072)
      {
        cardDurationString = SK.Text("TOOLTIP_CARD_INSTANT", "Instant Card");
      }
      else
      {
        int cardDuration = CardTypes.getCardDuration(cardID);
        if (cardDuration > 18250 || cardDuration == 0)
        {
          cardDurationString = SK.Text("TOOLTIP_CARD_EXPIRES", "Expires when used");
        }
        else
        {
          string buildTimeString = VillageMap.createBuildTimeString(cardDuration * 60 * 60);
          cardDurationString = SK.Text("TOOLTIP_CARD_DURATION", "Duration") + " : " + buildTimeString;
        }
      }
      return cardDurationString;
    }
  }
}
