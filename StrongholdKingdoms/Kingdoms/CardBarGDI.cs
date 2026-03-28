// Decompiled with JetBrains decompiler
// Type: Kingdoms.CardBarGDI
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CardBarGDI : CustomSelfDrawPanel.CSDControl
  {
    private const int BASE_CIRCLE_XPOS = 10;
    private const int BASE_CIRCLE_STRIDE = 53;
    private const int BASE_HEIGHT = 162;
    private CustomSelfDrawPanel.CSDLabel mainText = new CustomSelfDrawPanel.CSDLabel();
    private List<CardBarGDI.CardCircle> cardCircles = new List<CardBarGDI.CardCircle>();
    private CustomSelfDrawPanel.CSDImage circleCards = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel circleCardsText = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage suggestedExpand = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage suggestedCollapse = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage suggestedNext = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage suggestedPrev = new CustomSelfDrawPanel.CSDImage();
    private int cardTextTimer;
    private int numCardCirclesVisible = 10;
    private bool showExtras;
    private int lastAvailableToPlay = -1;
    public int currentCardSection = -1;
    public bool Dirty;
    private List<CardBarGDI.DisplayCardInfo> displayedCards = new List<CardBarGDI.DisplayCardInfo>();
    private List<CardBarGDI.DisplayCardInfo> newDisplayedCards = new List<CardBarGDI.DisplayCardInfo>();
    private List<UICard> suggestedCards = new List<UICard>();
    private List<UICard> suggestedDisplayedCards = new List<UICard>();
    private Dictionary<int, int> suggestedCardCounts = new Dictionary<int, int>();
    private bool animationComplete = true;
    private bool suggestedCardsValid;
    private int BASE_CARD_POS = 140;
    private bool suggestedPrevActive;
    private UICard clickedCard;
    private UICard animatedCard;
    private int animationCounter = 10;
    private PreValidateCardToBePlayed_ReturnType returnDataRef;
    private bool waitingResponse;
    private int selectedVillage = -1;

    public void init(int cardSection) => this.init(cardSection, 10, true, 14, 0, 0);

    public void init(
      int cardSection,
      int numVisible,
      bool extras,
      int cardsPerRow,
      int xExtra,
      int yExtra)
    {
      this.numCardCirclesVisible = numVisible;
      this.showExtras = extras;
      this.clearControls();
      this.currentCardSection = cardSection;
      if (numVisible == 10 && this.showExtras)
        this.Size = new Size(980, 162);
      else
        this.Size = new Size(800, 625);
      if (this.showExtras)
      {
        this.mainText.Color = ARGBColors.White;
        this.mainText.DropShadowColor = ARGBColors.Black;
        this.mainText.Position = new Point(10, -50);
        this.mainText.Size = new Size(980, 162);
        this.mainText.Text = "";
        this.mainText.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
        this.mainText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.cardTextTimer = 210;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.mainText);
      }
      int x = 10;
      int y = 5;
      this.cardCircles.Clear();
      for (int index = 0; index < this.numCardCirclesVisible; ++index)
      {
        CardBarGDI.CardCircle control = new CardBarGDI.CardCircle();
        control.init(this.currentCardSection, extras);
        control.Position = new Point(x, y);
        this.addControl((CustomSelfDrawPanel.CSDControl) control);
        this.cardCircles.Add(control);
        if ((index + 1) % cardsPerRow == 0)
        {
          x = 10;
          y += 162 + yExtra;
        }
        else
          x += 53 + xExtra;
      }
      if (this.showExtras)
      {
        this.circleCards.Position = new Point(x, 1);
        this.circleCards.Image = (Image) GFXLibrary.card_circles_card;
        this.circleCards.Data = -1;
        this.circleCards.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.circleClicked));
        this.circleCards.CustomTooltipID = 10001;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.circleCards);
        this.circleCardsText.Color = ARGBColors.White;
        this.circleCardsText.DropShadowColor = ARGBColors.Black;
        this.circleCardsText.Position = new Point(0, 25);
        this.circleCardsText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
        this.circleCardsText.Size = new Size(this.circleCards.Width - 1, 38);
        this.circleCardsText.Text = "5";
        this.circleCardsText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.circleCards.addControl((CustomSelfDrawPanel.CSDControl) this.circleCardsText);
        int num = x + 53;
        this.suggestedExpand.Image = (Image) GFXLibrary.cardbar_expand[0];
        this.suggestedExpand.Position = new Point(53 * this.displayedCards.Count + 16 + this.circleCards.Width, 18);
        this.suggestedExpand.Data = 0;
        this.suggestedExpand.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickExpand));
        this.suggestedExpand.CustomTooltipID = 10002;
        this.suggestedExpand.Visible = true;
        this.suggestedExpand.Enabled = true;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.suggestedExpand);
        this.suggestedCollapse.Image = (Image) GFXLibrary.cardbar_collapse[0];
        this.suggestedCollapse.Position = new Point(16 + this.circleCards.Width, 18);
        this.suggestedCollapse.Data = 0;
        this.suggestedCollapse.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickCollapse));
        this.suggestedCollapse.CustomTooltipID = 10003;
        this.suggestedCollapse.Visible = false;
        this.suggestedCollapse.Enabled = false;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.suggestedCollapse);
        this.suggestedNext.Image = (Image) GFXLibrary.cardbar_right[1];
        this.suggestedNext.Position = new Point(57 + this.circleCards.Width + 495, 18);
        this.suggestedNext.Data = 0;
        this.suggestedNext.Visible = false;
        this.suggestedNext.Enabled = false;
        this.suggestedNext.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickGoRight));
        this.suggestedNext.CustomTooltipID = 10004;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.suggestedNext);
        this.suggestedPrev.Image = (Image) GFXLibrary.cardbar_left[1];
        this.suggestedPrev.Position = new Point(57 + this.circleCards.Width, 18);
        this.suggestedPrev.Data = 0;
        this.suggestedPrev.Visible = false;
        this.suggestedPrev.Enabled = false;
        this.suggestedPrev.Alpha = 0.5f;
        this.suggestedPrev.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickGoLeft));
        this.suggestedPrev.CustomTooltipID = 10005;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.suggestedPrev);
      }
      this.refresh();
      if (this.lastAvailableToPlay != 0)
        return;
      this.mainText.Text = SK.Text("CardBarGDI_Click_To_Buy", "Click to Buy Cards");
    }

    public void refresh()
    {
      this.displayedCards.Clear();
      this.newDisplayedCards.Clear();
      this.suggestedCards.Clear();
      this.suggestedCardsValid = false;
      this.suggestedCardCounts.Clear();
      foreach (CustomSelfDrawPanel.CSDControl suggestedDisplayedCard in this.suggestedDisplayedCards)
        this.removeControl(suggestedDisplayedCard);
      this.suggestedDisplayedCards.Clear();
      this.suggestedExpand.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickExpand));
      this.lastAvailableToPlay = -1;
      this.update();
      PlayCardsPanel.disableCardsInPlay(this.suggestedDisplayedCards);
    }

    public void flagAsRendered() => this.Dirty = false;

    private bool equalCards(
      List<CardBarGDI.DisplayCardInfo> list1,
      List<CardBarGDI.DisplayCardInfo> list2)
    {
      if (list1.Count != list2.Count)
        return false;
      int count = list1.Count;
      for (int index = 0; index < count; ++index)
      {
        if (!list1[index].equals(list2[index]))
          return false;
      }
      return true;
    }

    public bool update()
    {
      this.newDisplayedCards.Clear();
      CardData userCardData = GameEngine.Instance.cardsManager.UserCardData;
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      DateTime currentServerTime = VillageMap.getCurrentServerTime();
      int length = userCardData.cards.Length;
      for (int index = 0; index < length; ++index)
      {
        int card = userCardData.cards[index];
        int cardCategory = CardTypes.getCardCategory(card);
        if (cardCategory == this.currentCardSection || this.currentCardSection == 0 || this.currentCardSection == 9 && (cardCategory == 6 || cardCategory == 7))
        {
          TimeSpan timeSpan = userCardData.cardsExpiry[index] - currentServerTime;
          int cardDuration = CardTypes.getCardDuration(card);
          int num1 = (int) timeSpan.TotalMinutes;
          if (num1 < 0)
            num1 = 0;
          int num2 = num1 * 64 / (cardDuration * 60);
          if (num2 < 0)
            num2 = 0;
          else if (num2 >= 64)
            num2 = 63;
          if (timeSpan.TotalDays > 100.0)
            num2 = 64;
          this.newDisplayedCards.Add(new CardBarGDI.DisplayCardInfo()
          {
            card = card,
            currentFrame = num2,
            effect = CardTypes.getCardEffectValue(card),
            imageID = this.getCardImage(card) - 1
          });
        }
      }
      int num3 = GameEngine.Instance.cardsManager.countPlayableCardsInCardSection(this.currentCardSection);
      bool flag = false;
      if (num3 != this.lastAvailableToPlay)
        flag = true;
      if (!this.equalCards(this.displayedCards, this.newDisplayedCards))
      {
        flag = true;
        this.displayedCards.Clear();
        foreach (CardBarGDI.DisplayCardInfo newDisplayedCard in this.newDisplayedCards)
          this.displayedCards.Add(newDisplayedCard);
      }
      if (this.showExtras && this.cardTextTimer > 0)
      {
        --this.cardTextTimer;
        if (this.cardTextTimer <= 0)
        {
          this.mainText.Visible = false;
          flag = true;
        }
        else if (this.cardTextTimer < 10)
        {
          this.mainText.Color = Color.FromArgb(this.cardTextTimer * 25, this.cardTextTimer * 25, this.cardTextTimer * 25, this.cardTextTimer * 25);
          this.mainText.DropShadowColor = Color.FromArgb(this.cardTextTimer * 25, 0, 0, 0);
          flag = true;
        }
      }
      if (this.showExtras && !this.suggestedCardsValid)
      {
        GameEngine.Instance.cardsManager.searchProfileCards(new CardTypes.CardDefinition()
        {
          cardCategory = this.currentCardSection
        }, "meta", GameEngine.Instance.cardsManager.lastUserCardNameFilter);
        int playerRank = GameEngine.Instance.World.getRank() + 1;
        foreach (int num4 in GameEngine.Instance.cardsManager.ProfileCardsSearch)
        {
          if (GameEngine.Instance.cardsManager.ProfileCards[num4].cardRank <= playerRank && GameEngine.Instance.cardsManager.ProfileCards[num4].id != 3076)
          {
            if (this.suggestedCardCounts.ContainsKey(GameEngine.Instance.cardsManager.ProfileCards[num4].id))
            {
              foreach (UICard suggestedCard in this.suggestedCards)
              {
                if (suggestedCard.Definition.id == GameEngine.Instance.cardsManager.ProfileCards[num4].id)
                {
                  suggestedCard.UserIDList.Add(num4);
                  ++suggestedCard.cardCount;
                  suggestedCard.countLabel.Text = suggestedCard.cardCount.ToString();
                }
              }
            }
            else
            {
              this.suggestedCardCounts.Add(GameEngine.Instance.cardsManager.ProfileCards[num4].id, 1);
              this.suggestedCards.Add(this.makeUICard(GameEngine.Instance.cardsManager.ProfileCards[num4], num4, playerRank));
            }
          }
        }
        this.suggestedExpand.Visible = this.suggestedExpand.Enabled = this.suggestedCards.Count != 0;
        this.suggestedCardsValid = true;
      }
      if (this.showExtras && !this.animationComplete)
      {
        this.animationComplete = true;
        if (!this.suggestedExpand.Visible)
        {
          for (int index = 0; index < this.suggestedDisplayedCards.Count; ++index)
          {
            if (this.suggestedDisplayedCards[index].Position.X < this.BASE_CARD_POS + 47 * index)
            {
              this.animationComplete = false;
              this.suggestedDisplayedCards[index].X = Math.Min(this.suggestedDisplayedCards[index].Position.X + 70, this.BASE_CARD_POS + 47 * index);
            }
          }
        }
        this.Dirty = true;
        this.invalidate();
      }
      if (this.showExtras && this.animatedCard != null && this.animationCounter < 30)
      {
        int num5 = (this.animationCounter % 10 + 11) * 12;
        this.animatedCard.Hilight(Color.FromArgb(num5, num5, num5));
        ++this.animationCounter;
        if (this.animationCounter == 10)
          this.animatedCard.Y -= 2;
        this.Dirty = true;
        this.invalidate();
      }
      else if (this.showExtras && this.animatedCard != null)
      {
        this.animatedCard.Hilight(ARGBColors.White);
        this.animatedCard = (UICard) null;
        this.Dirty = true;
        this.invalidate();
      }
      if (this.showExtras && this.suggestedNext.Data > 0)
      {
        --this.suggestedNext.Data;
        if (this.suggestedNext.Data == 0)
        {
          this.suggestedNext.Image = (Image) GFXLibrary.cardbar_right[1];
          this.Dirty = true;
          this.invalidate();
        }
      }
      if (this.showExtras && this.suggestedPrev.Data > 0)
      {
        --this.suggestedPrev.Data;
        if (this.suggestedPrev.Data == 0)
        {
          this.suggestedPrev.Image = (Image) GFXLibrary.cardbar_left[1];
          this.Dirty = true;
          this.invalidate();
        }
      }
      if (flag)
      {
        this.Dirty = true;
        this.lastAvailableToPlay = num3;
        int num6 = this.displayedCards.Count;
        if (num6 > this.numCardCirclesVisible)
          num6 = this.numCardCirclesVisible;
        this.circleCardsText.Text = num3.ToString();
        if (this.suggestedDisplayedCards.Count != 0)
        {
          this.invalidate();
          return true;
        }
        for (int index = 0; index < this.numCardCirclesVisible; ++index)
          this.getCircle(index).Visible = false;
        for (int index = 0; index < num6; ++index)
        {
          CardBarGDI.CardCircle circle = this.getCircle(index);
          CardBarGDI.DisplayCardInfo displayedCard = this.displayedCards[index];
          circle.Image = GFXLibrary.card_circles_timer[displayedCard.currentFrame];
          circle.Visible = true;
          circle.FXImage = GFXLibrary.card_circles_icons[displayedCard.imageID];
          circle.scaleFXImage(displayedCard.imageID == 33);
          NumberFormatInfo provider = (double) (int) displayedCard.effect != displayedCard.effect ? GameEngine.NFI_D1 : GameEngine.NFI;
          string str = !CardTypes.addX(displayedCard.card) ? (!CardTypes.addPlus(displayedCard.card) ? (displayedCard.effect == 0.0 ? "" : displayedCard.effect.ToString("N", (IFormatProvider) provider)) : "+" + displayedCard.effect.ToString("N", (IFormatProvider) provider)) : "x" + displayedCard.effect.ToString("N", (IFormatProvider) provider);
          if (CardTypes.addPercent(displayedCard.card))
            str += "%";
          circle.Text = str;
          circle.CustomTooltipID = 10000;
          circle.CustomTooltipData = displayedCard.card;
        }
        if (this.showExtras)
        {
          this.circleCards.X = 10 + 53 * num6;
          this.suggestedExpand.X = 53 * num6 + 16 + this.circleCards.Width;
          this.mainText.X = this.circleCards.X + 53 + 5;
        }
        this.invalidate();
      }
      return flag;
    }

    public void circleClicked()
    {
      GameEngine.Instance.playInterfaceSound("WorldMap_cards_opened_from_map");
      InterfaceMgr.Instance.openPlayCardsWindow(this.currentCardSection);
    }

    public void clickExpand()
    {
      for (int index = 0; index < this.numCardCirclesVisible; ++index)
        this.cardCircles[index].Visible = false;
      this.circleCards.Position = new Point(10, 1);
      foreach (UICard suggestedCard in this.suggestedCards)
      {
        if (this.suggestedDisplayedCards.Count < 10)
        {
          this.suggestedDisplayedCards.Add(suggestedCard);
          suggestedCard.Position = new Point(this.BASE_CARD_POS, 1);
          this.addControl((CustomSelfDrawPanel.CSDControl) suggestedCard);
        }
      }
      this.suggestedExpand.Visible = this.suggestedExpand.Enabled = false;
      this.suggestedNext.Enabled = this.suggestedNext.Visible = this.suggestedCards.Count > 10;
      this.suggestedPrev.Visible = true;
      this.suggestedPrev.Enabled = false;
      this.suggestedPrev.Alpha = 0.5f;
      this.suggestedCollapse.Enabled = this.suggestedCollapse.Visible = true;
      this.animationComplete = false;
      this.Dirty = true;
      this.invalidate();
    }

    public void clickCollapse()
    {
      for (int index = 0; index < this.displayedCards.Count && index < this.numCardCirclesVisible; ++index)
        this.cardCircles[index].Visible = true;
      foreach (CustomSelfDrawPanel.CSDControl suggestedDisplayedCard in this.suggestedDisplayedCards)
        this.removeControl(suggestedDisplayedCard);
      this.suggestedDisplayedCards.Clear();
      this.suggestedExpand.Enabled = this.suggestedExpand.Visible = true;
      this.suggestedNext.Enabled = this.suggestedNext.Visible = false;
      this.suggestedPrev.Enabled = this.suggestedPrev.Visible = false;
      this.suggestedCollapse.Enabled = this.suggestedCollapse.Visible = false;
      this.animationComplete = false;
      this.refresh();
      this.Dirty = true;
      this.invalidate();
    }

    public void clickGoRight()
    {
      int num = this.suggestedCards.IndexOf(this.suggestedDisplayedCards[this.suggestedDisplayedCards.Count - 1]) + 1;
      foreach (CustomSelfDrawPanel.CSDControl suggestedDisplayedCard in this.suggestedDisplayedCards)
        this.removeControl(suggestedDisplayedCard);
      this.suggestedDisplayedCards.Clear();
      for (int index = num; index < this.suggestedCards.Count && this.suggestedDisplayedCards.Count != 10; ++index)
      {
        this.suggestedDisplayedCards.Add(this.suggestedCards[index]);
        this.suggestedCards[index].Position = new Point(this.BASE_CARD_POS, 1);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.suggestedCards[index]);
      }
      this.suggestedNext.Enabled = this.suggestedNext.Visible = this.suggestedDisplayedCards[this.suggestedDisplayedCards.Count - 1] != this.suggestedCards[this.suggestedCards.Count - 1];
      this.suggestedNext.Data = 5;
      this.suggestedNext.Image = (Image) GFXLibrary.cardbar_right[2];
      this.animationComplete = false;
      this.suggestedPrev.Enabled = true;
      this.suggestedPrev.Alpha = 1f;
      this.suggestedPrev.CustomTooltipID = 10005;
      this.Dirty = true;
      this.invalidate();
    }

    public void clickGoLeft()
    {
      if (this.suggestedDisplayedCards[0] == this.suggestedCards[0])
        return;
      int num = this.suggestedCards.IndexOf(this.suggestedDisplayedCards[0]) - 10;
      foreach (CustomSelfDrawPanel.CSDControl suggestedDisplayedCard in this.suggestedDisplayedCards)
        this.removeControl(suggestedDisplayedCard);
      this.suggestedDisplayedCards.Clear();
      for (int index = num; index < this.suggestedCards.Count && this.suggestedDisplayedCards.Count != 10; ++index)
      {
        this.suggestedDisplayedCards.Add(this.suggestedCards[index]);
        this.suggestedCards[index].Position = new Point(this.BASE_CARD_POS, 1);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.suggestedCards[index]);
      }
      this.suggestedNext.Enabled = this.suggestedNext.Visible = this.suggestedDisplayedCards[this.suggestedDisplayedCards.Count - 1] != this.suggestedCards[this.suggestedCards.Count - 1];
      this.suggestedPrev.Data = 5;
      this.suggestedPrev.Image = (Image) GFXLibrary.cardbar_left[2];
      if (this.suggestedDisplayedCards[0] == this.suggestedCards[0])
      {
        this.suggestedPrev.Enabled = false;
        this.suggestedPrev.Alpha = 0.5f;
      }
      this.animationComplete = false;
      this.Dirty = true;
      this.invalidate();
    }

    private CardBarGDI.CardCircle getCircle(int index)
    {
      if (index < this.cardCircles.Count)
        return this.cardCircles[index];
      if (this.cardCircles.Count == 0)
        this.cardCircles.Add(new CardBarGDI.CardCircle());
      return this.cardCircles[0];
    }

    private int getCardImage(int card)
    {
      card = CardTypes.getCardType(card);
      switch (card)
      {
        case 257:
        case 258:
        case 259:
          return 40;
        case 263:
          return 57;
        case 264:
        case 267:
        case 268:
          return 94;
        case 265:
        case 269:
        case 270:
          return 35;
        case 266:
        case 271:
        case 272:
          return 58;
        case 513:
        case 514:
        case 515:
          return 3;
        case 516:
        case 517:
        case 518:
          return 8;
        case 519:
        case 520:
        case 521:
          return 14;
        case 522:
        case 523:
        case 524:
          return 6;
        case 525:
        case 526:
        case 527:
          return 11;
        case 528:
        case 529:
        case 530:
          return 25;
        case 531:
        case 532:
        case 533:
          return 10;
        case 534:
        case 535:
        case 536:
          return 50;
        case 537:
        case 538:
        case 539:
          return 2;
        case 540:
        case 541:
        case 542:
          return 51;
        case 769:
        case 770:
        case 771:
          return 28;
        case 772:
        case 773:
        case 774:
          return 23;
        case 775:
        case 776:
        case 777:
          return 13;
        case 778:
        case 779:
        case 780:
          return 19;
        case 781:
        case 782:
        case 783:
          return 52;
        case 1025:
        case 1026:
        case 1027:
          return 5;
        case 1028:
        case 1029:
        case 1030:
          return 18;
        case 1031:
        case 1032:
        case 1033:
          return 24;
        case 1034:
        case 1035:
        case 1036:
          return 4;
        case 1037:
        case 1038:
        case 1039:
          return 7;
        case 1281:
        case 1282:
        case 1283:
          return 1;
        case 1284:
        case 1285:
        case 1286:
          return 26;
        case 1287:
        case 1288:
        case 1289:
          return 12;
        case 1290:
        case 1291:
        case 1292:
          return 15;
        case 1293:
        case 1294:
        case 1295:
          return 9;
        case 1296:
        case 1297:
        case 1298:
          return 27;
        case 1299:
        case 1300:
        case 1301:
          return 20;
        case 1302:
        case 1303:
        case 1304:
          return 22;
        case 1305:
        case 1306:
        case 1307:
          return 21;
        case 1537:
        case 1538:
        case 1539:
          return 34;
        case 1541:
        case 1542:
        case 1543:
          return 89;
        case 1800:
        case 1801:
        case 1802:
          return 93;
        case 2049:
        case 2050:
        case 2051:
          return 32;
        case 2052:
        case 2053:
        case 2054:
          return 39;
        case 2055:
        case 2056:
        case 2057:
          return 86;
        case 2058:
        case 2059:
        case 2060:
          return 87;
        case 2061:
        case 2062:
        case 2063:
          return 59;
        case 2064:
        case 2065:
        case 2066:
          return 102;
        case 2067:
        case 2068:
        case 2069:
          return 101;
        case 2070:
        case 2071:
        case 2072:
          return 32;
        case 2305:
        case 2306:
        case 2307:
          return 33;
        case 2308:
          return 98;
        case 2309:
          return 99;
        case 2310:
          return 100;
        case 2311:
        case 2312:
        case 2313:
          return 36;
        case 2561:
        case 2562:
        case 2563:
          return 95;
        case 2564:
        case 2565:
        case 2566:
          return 96;
        case 2567:
        case 2568:
        case 2569:
          return 97;
        case 2689:
        case 2690:
          return 53;
        case 2691:
        case 2692:
        case 2693:
          return 33;
        case 2694:
        case 2695:
        case 2696:
          return 95;
        case 2817:
          return 37;
        case 2818:
          return 38;
        case 2819:
          return 41;
        case 2820:
          return 56;
        case 2821:
          return 88;
        case 2822:
          return 42;
        case 2823:
        case 2824:
        case 2825:
          return 1;
        case 2826:
          return 103;
        case 2881:
        case 2882:
        case 2883:
          return 34;
        case 2887:
        case 2888:
        case 2889:
          return 106;
        case 2945:
        case 2946:
        case 2947:
          return 43;
        case 2948:
        case 2949:
        case 2950:
          return 44;
        case 2951:
        case 2952:
        case 2953:
          return 45;
        case 2954:
        case 2955:
        case 2956:
          return 46;
        case 2957:
        case 2958:
        case 2959:
          return 47;
        case 2960:
        case 2961:
        case 2962:
          return 48;
        case 2963:
        case 2964:
        case 2965:
          return 49;
        case 2966:
          return 54;
        case 2967:
          return 55;
        case 2968:
          return 92;
        case 2969:
          return 91;
        case 2970:
        case 2971:
        case 2972:
          return 90;
        case 2973:
        case 2974:
        case 2975:
          return 16;
        case 3031:
          return 60;
        case 3032:
          return 61;
        case 3033:
          return 62;
        case 3034:
          return 63;
        case 3035:
          return 64;
        case 3036:
          return 65;
        case 3038:
          return 105;
        case 3039:
          return 66;
        case 3040:
          return 67;
        case 3041:
          return 68;
        case 3042:
          return 69;
        case 3043:
          return 70;
        case 3044:
          return 71;
        case 3045:
          return 72;
        case 3046:
          return 73;
        case 3047:
          return 74;
        case 3048:
          return 75;
        case 3049:
          return 76;
        case 3050:
          return 77;
        case 3051:
          return 78;
        case 3052:
          return 79;
        case 3053:
          return 80;
        case 3055:
          return 81;
        case 3056:
          return 82;
        case 3057:
          return 83;
        case 3058:
          return 84;
        case 3059:
          return 85;
        case 3076:
          return 104;
        default:
          return 29;
      }
    }

    public void toggleActive(bool value)
    {
      float factor = 1f;
      if (!value)
      {
        factor = 0.5f;
        this.suggestedPrevActive = this.suggestedPrev.Enabled;
        this.suggestedPrev.Enabled = false;
        this.suggestedPrev.Alpha = 0.5f;
      }
      else
      {
        this.suggestedPrev.Enabled = this.suggestedPrevActive;
        this.suggestedPrev.Alpha = this.suggestedPrevActive ? 1f : 0.5f;
      }
      this.circleCards.Alpha = this.suggestedExpand.Alpha = this.suggestedCollapse.Alpha = this.suggestedNext.Alpha = factor;
      foreach (UICard suggestedDisplayedCard in this.suggestedDisplayedCards)
      {
        suggestedDisplayedCard.Enabled = value;
        suggestedDisplayedCard.setAlpha(factor);
      }
      foreach (CardBarGDI.CardCircle cardCircle in this.cardCircles)
      {
        cardCircle.Enabled = value;
        cardCircle.setAlpha(factor);
      }
      this.circleCards.Enabled = this.suggestedExpand.Enabled = this.suggestedCollapse.Enabled = this.suggestedNext.Enabled = value;
      this.invalidate();
      this.Dirty = true;
    }

    public UICard makeUICard(CardTypes.CardDefinition def, int userid, int playerRank)
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
      uiCard.bigGradeImage.Alpha = 0.0f;
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
          uiCard.bigFrameExtraImage.Alpha = 0.0f;
          uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigFrameExtraImage);
          break;
        case 524288:
        case 1048576:
        case 2097152:
          uiCard.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
          uiCard.bigFrameExtraImage.Position = new Point(0, 0);
          uiCard.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_diamond;
          uiCard.bigFrameExtraImage.Alpha = 0.0f;
          uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigFrameExtraImage);
          break;
        case 4194304:
          uiCard.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
          uiCard.bigFrameExtraImage.Position = new Point(0, 0);
          uiCard.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_sapphire;
          uiCard.bigFrameExtraImage.Alpha = 0.0f;
          uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigFrameExtraImage);
          break;
      }
      uiCard.bigGradeImage.Size = uiCard.bigGradeImage.Image.Size;
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) uiCard.bigGradeImage);
      uiCard.bigTitle = new CustomSelfDrawPanel.CSDLabel();
      uiCard.bigTitle.Text = "";
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
      control1.Position = new Point(0, Convert.ToInt32((double) uiCard.Height * 0.72));
      control1.Size = new Size(uiCard.Width, uiCard.Height / 4);
      control1.Text = "1";
      control1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control1.Font = FontManager.GetFont("Arial", 36f, FontStyle.Bold);
      control1.Color = ARGBColors.White;
      control1.DropShadowColor = ARGBColors.Black;
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) control1);
      uiCard.countLabel = control1;
      CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
      control2.Text = "";
      uiCard.addControl((CustomSelfDrawPanel.CSDControl) control2);
      uiCard.rankLabel = control2;
      uiCard.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickPlayDelegate));
      uiCard.ScaleAll(0.25 * (double) InterfaceMgr.UIScale);
      return uiCard;
    }

    private void clickPlayDelegate() => this.clickPlay(true, false);

    private void clickPlay(bool fromClick, bool fromValidate)
    {
      if (this.waitingResponse || GameEngine.Instance.World.WorldEnded)
        return;
      if (CustomSelfDrawPanel.StaticClickedControl != null && fromClick)
        this.clickedCard = (UICard) CustomSelfDrawPanel.StaticClickedControl;
      if (this.clickedCard == null)
        return;
      this.waitingResponse = true;
      XmlRpcCardsProvider forEndpoint = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
      UICard clickedCard = this.clickedCard;
      this.selectedVillage = -1;
      int selectedMenuVillage = InterfaceMgr.Instance.getSelectedMenuVillage();
      if (!GameEngine.Instance.World.isCapital(selectedMenuVillage) || CardTypes.getCardType(clickedCard.Definition.id) == 3076)
        this.selectedVillage = selectedMenuVillage;
      int num1 = GameEngine.Instance.World.getRank() + 1;
      if (clickedCard.Definition.cardRank > num1)
      {
        int num2 = (int) MyMessageBox.Show(SK.Text("BuyCardsPanel_Rank_Too_low", "Your rank is too low to play this card.") + Environment.NewLine + SK.Text("BuyCardsPanel_Current_Rank", "Current Rank") + " : " + num1.ToString() + Environment.NewLine + SK.Text("BuyCardsPanel_Required_Rank", "Required Rank") + " : " + clickedCard.Definition.cardRank.ToString(), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
        this.waitingResponse = false;
      }
      else if ((clickedCard.Definition.id == 3109 || clickedCard.Definition.id == 3110 || clickedCard.Definition.id == 3111 || clickedCard.Definition.id == 3112) && GameEngine.Instance.Village != null && GameEngine.Instance.Village.countBuildingType(35) == 0)
      {
        int num3 = (int) MyMessageBox.Show(SK.Text("PlayCard_No_Inn_Available", "An inn must be built at the current village before this card can be played."));
        this.waitingResponse = false;
      }
      else if (fromClick && Program.mySettings.ConfirmPlayCard)
      {
        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_open_confirmation");
        this.waitingResponse = false;
        InterfaceMgr.Instance.openConfirmPlayCardPopup(clickedCard.Definition, new ConfirmPlayCardPanel.CardClickPlayDelegate(this.clickPlay));
      }
      else
      {
        if (!fromValidate)
        {
          if (CardTypes.cardNeedsValidation(CardTypes.getCardType(clickedCard.Definition.id)))
          {
            this.validateCardPossible(CardTypes.getCardType(clickedCard.Definition.id), this.selectedVillage);
            return;
          }
        }
        try
        {
          if (fromClick)
            GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card");
          this.animationCounter = 0;
          this.animatedCard = this.clickedCard;
          this.animatedCard.Y += 2;
          forEndpoint.PlayUserCard((ICardsRequest) new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), clickedCard.UserIDList[0].ToString(), this.selectedVillage.ToString(), RemoteServices.Instance.ProfileWorldID.ToString()), new CardsEndResponseDelegate(this.cardPlayed), (Control) InterfaceMgr.Instance.getDXBasePanel());
          GameEngine.Instance.cardsManager.removeProfileCard(clickedCard.UserIDList[0]);
        }
        catch (Exception ex)
        {
        }
      }
    }

    public void validateCardPossible(int cardType, int villageID)
    {
      RemoteServices.Instance.set_PreValidateCardToBePlayed_UserCallBack(new RemoteServices.PreValidateCardToBePlayed_UserCallBack(this.preValidateCardToBePlayedCallBack));
      RemoteServices.Instance.PreValidateCardToBePlayed(cardType, villageID);
    }

    public void cardClickPlayFalseFromClickTrueValidate() => this.clickPlay(false, true);

    private void ContinuePreValidateCardToBePlayed()
    {
      PreValidateCardToBePlayed_ReturnType returnDataRef = this.returnDataRef;
      if (returnDataRef.canPlayFully)
        this.clickPlay(false, true);
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
            str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_11", "Number of Catapults gained will be") + " : " + returnDataRef.numCanPlay.ToString();
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
        if (MyMessageBox.Show(str + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo) == DialogResult.Yes)
          this.clickPlay(false, true);
        else
          this.clickedCard.Hilight(ARGBColors.White);
      }
      else
      {
        this.clickedCard.Hilight(ARGBColors.White);
        if (returnDataRef.otherErrorCode != 0)
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
    }

    public void preValidateCardToBePlayedCallBack(PreValidateCardToBePlayed_ReturnType returnData)
    {
      this.waitingResponse = false;
      this.returnDataRef = returnData;
      if (!returnData.Success)
        return;
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

    private void cardPlayed(ICardsProvider provider, ICardsResponse response)
    {
      if (!response.SuccessCode.HasValue || response.SuccessCode.Value != 1)
      {
        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_failed");
        int num1 = (int) MyMessageBox.Show(CardsManager.translateCardError(response.Message, this.clickedCard.Definition.id), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
        try
        {
          GameEngine.Instance.cardsManager.addProfileCard(this.clickedCard.UserIDList[0], CardTypes.getStringFromCard(this.clickedCard.Definition.id));
        }
        catch (Exception ex)
        {
          int num2 = (int) MyMessageBox.Show(ex.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
        }
      }
      else
      {
        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_success");
        GameEngine.Instance.cardsManager.ProfileCardsSet.Remove(this.clickedCard.UserIDList[0]);
        GameEngine.Instance.cardsManager.addRecentCard(this.clickedCard.Definition.id);
        if (this.clickedCard.UserIDList.Count > 0)
          this.clickedCard.UserID = this.clickedCard.UserIDList[0];
        GameEngine.Instance.cardsManager.CardPlayed(this.clickedCard.Definition.cardCategory, this.clickedCard.Definition.id, this.selectedVillage);
        if (this.clickedCard.cardCount > 1)
        {
          this.clickedCard.UserIDList.Remove(this.clickedCard.UserID);
          this.clickedCard.UserID = this.clickedCard.UserIDList[0];
          --this.clickedCard.cardCount;
          this.clickedCard.countLabel.Text = this.clickedCard.cardCount.ToString();
        }
        else
        {
          this.clickedCard.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
          CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
          control.Position = new Point(0, 0);
          control.Size = this.clickedCard.bigImage.Size;
          control.Image = (Image) GFXLibrary.CardBackBig;
          control.setScale(0.25);
          this.clickedCard.CustomTooltipID = 0;
          this.clickedCard.CustomTooltipData = -1;
          this.clickedCard.addControl((CustomSelfDrawPanel.CSDControl) control);
        }
      }
      this.Dirty = true;
      this.invalidate();
      this.clickedCard = (UICard) null;
      this.waitingResponse = false;
    }

    private class DisplayCardInfo
    {
      public int card;
      public int currentFrame = -1;
      public int imageID = -1;
      public double effect;

      public bool equals(CardBarGDI.DisplayCardInfo dci)
      {
        return dci.card == this.card && dci.currentFrame == this.currentFrame && dci.imageID == this.imageID && dci.effect == this.effect;
      }
    }

    public class CardCircle : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage circle1 = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage circle1SubImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel circle1Text = new CustomSelfDrawPanel.CSDLabel();
      private int m_cardSection;
      private MyMessageBoxPopUp cancelCardPopUp;

      public void init(int cardSection, bool extras)
      {
        this.m_cardSection = cardSection;
        this.circle1.Position = new Point(0, 0);
        this.circle1.Image = (Image) GFXLibrary.card_circles_timer[0];
        this.circle1.Data = 10;
        if (extras)
          this.circle1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.circleClicked));
        else
          this.circle1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.circleClickedCancel));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.circle1);
        this.circle1SubImage.Position = new Point(-6, 0);
        this.circle1SubImage.Image = (Image) GFXLibrary.popularityFace;
        this.circle1.addControl((CustomSelfDrawPanel.CSDControl) this.circle1SubImage);
        this.circle1Text.Color = ARGBColors.White;
        this.circle1Text.DropShadowColor = ARGBColors.Black;
        this.circle1Text.Position = new Point(0, 20);
        this.circle1Text.Size = new Size(this.circle1.Width - 5, 31);
        this.circle1Text.Text = "??";
        this.circle1Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
        this.circle1Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.circle1.addControl((CustomSelfDrawPanel.CSDControl) this.circle1Text);
        this.Size = this.circle1.Size;
      }

      public BaseImage Image
      {
        set => this.circle1.Image = (Image) value;
      }

      public BaseImage FXImage
      {
        set => this.circle1SubImage.Image = (Image) value;
      }

      public void scaleFXImage(bool state)
      {
        if (!state)
        {
          this.circle1SubImage.Scale = 1.0;
          this.circle1SubImage.Position = new Point(-6, 0);
        }
        else
        {
          this.circle1SubImage.Scale = 0.75;
          this.circle1SubImage.Position = new Point(4, 10);
        }
      }

      public string Text
      {
        set => this.circle1Text.Text = value;
      }

      public void setAlpha(float value)
      {
        this.circle1.Alpha = value;
        this.circle1SubImage.Alpha = value;
      }

      public void circleClicked()
      {
        GameEngine.Instance.playInterfaceSound("WorldMap_cards_opened_from_map");
        InterfaceMgr.Instance.openPlayCardsWindow(this.m_cardSection);
      }

      public void circleClickedCancel()
      {
        int customTooltipData = this.CustomTooltipData;
        GameEngine.Instance.playInterfaceSound("WorldMap_cards_opened_from_map");
        if (MyMessageBox.Show(CardTypes.getDescriptionFromCard(customTooltipData) + Environment.NewLine + Environment.NewLine + SK.Text("ViewCards_Cancel_Card_1", "Are you sure you wish to cancel this card?") + Environment.NewLine + Environment.NewLine + SK.Text("ViewCards_Cancel_Card_2", "If you cancel this card, the effect of the card will end and you will lose the card.") + Environment.NewLine + Environment.NewLine, SK.Text("ViewCards_Cancel_Card", "Cancel Card in Play"), MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, 0) != DialogResult.Yes)
          return;
        this.cancelCardSharedFunction(customTooltipData);
      }

      private void CancelCardClicked()
      {
        this.cancelCardSharedFunction(this.CustomTooltipData);
        this.cancelCardPopUp.Close();
      }

      private void ClosePopUp()
      {
        if (this.cancelCardPopUp == null)
          return;
        if (this.cancelCardPopUp.Created)
          this.cancelCardPopUp.Close();
        this.cancelCardPopUp = (MyMessageBoxPopUp) null;
      }

      private void cancelCardSharedFunction(int cardType)
      {
        if (GameEngine.Instance.World.WorldEnded)
          return;
        RemoteServices.Instance.set_CancelCard_UserCallBack(new RemoteServices.CancelCard_UserCallBack(this.cancelCardCallback));
        RemoteServices.Instance.CancelCard(cardType);
      }

      private void cancelCardCallback(CancelCard_ReturnType returnData)
      {
        if (!returnData.Success || returnData.m_cardData == null)
          return;
        GameEngine.Instance.cardsManager.UserCardData = returnData.m_cardData;
      }
    }
  }
}
