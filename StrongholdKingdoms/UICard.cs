// Decompiled with JetBrains decompiler
// Type: UICard
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Kingdoms;
using System;
using System.Collections.Generic;
using System.Drawing;

//#nullable disable
public class UICard : CustomSelfDrawPanel.CSDControl
{
  public CustomSelfDrawPanel.CSDImage bigBaseImage;
  public CustomSelfDrawPanel.CSDImage bigFrameImage;
  public CustomSelfDrawPanel.CSDImage bigFrameExtraImage;
  public CustomSelfDrawPanel.CSDImage bigGradeImage;
  public CustomSelfDrawPanel.CSDLabel bigEffect;
  public CustomSelfDrawPanel.CSDLabel bigTitle;
  public CustomSelfDrawPanel.CSDLabel rankLabel;
  public CustomSelfDrawPanel.CSDLabel buyCardsLabel;
  public int cardCount = 1;
  public int SearchIndex;
  public int SetIndex;
  public int UserID;
  public List<int> UserIDList = new List<int>();
  public CardTypes.CardDefinition Definition;
  public BaseImage bigImage;
  public BaseImage bigFrame;
  public BaseImage bigFrameOver;
  public CustomSelfDrawPanel.CSDImage smallBase;
  public CustomSelfDrawPanel.CSDImage smallFrame;
  public CustomSelfDrawPanel.CSDLabel countLabel;
  public CustomSelfDrawPanel.CSDImage progressBar;
  public double TotalTime;
  public double RemainingTime;
  public static UICard.CardsNameComparer cardsNameComparer = new UICard.CardsNameComparer();
  public static UICard.TUTCardsNameComparer TUTcardsNameComparer = new UICard.TUTCardsNameComparer();
  public static UICard.TUT2CardsNameComparer TUT2cardsNameComparer = new UICard.TUT2CardsNameComparer();
  public static UICard.CardsNameComparerReverse cardsNameComparerReverse = new UICard.CardsNameComparerReverse();
  public static UICard.CardsIDComparer cardsIDComparer = new UICard.CardsIDComparer();
  public static UICard.CardsIDComparerReverse cardsIDComparerReverse = new UICard.CardsIDComparerReverse();
  public static UICard.CardsQuantityComparer cardsQuantityComparer = new UICard.CardsQuantityComparer();
  public static UICard.CardsQuantityComparerReverse cardsQuantityComparerReverse = new UICard.CardsQuantityComparerReverse();
  public static UICard.CardsPriceComparer cardsPriceComparer = new UICard.CardsPriceComparer();
  public static UICard.CardsPriceComparerReverse cardsPriceComparerReverse = new UICard.CardsPriceComparerReverse();

  public UICard()
  {
    this.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.MouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.MouseOut));
    this.ClipVisible = true;
  }

  public void MouseOver()
  {
    if (this.bigFrameImage == null || this.bigFrame == null || this.bigFrameOver == null)
      return;
    this.bigFrameImage.Image = (Image) this.bigFrameOver;
  }

  public void MouseOut()
  {
    if (this.bigFrameImage == null || this.bigFrame == null || this.bigFrameOver == null)
      return;
    this.bigFrameImage.Image = (Image) this.bigFrame;
  }

  public void Hilight(Color c)
  {
    if (this.bigBaseImage != null)
      this.bigBaseImage.Colorise = c;
    if (this.bigFrameImage != null)
      this.bigFrameImage.Colorise = c;
    if (this.bigFrameExtraImage != null)
      this.bigFrameExtraImage.Colorise = c;
    this.invalidate();
  }

  public void ScaleAll(double factor)
  {
    this.Size = new Size(Convert.ToInt32(Math.Floor((double) this.bigFrame.Width * factor)), Convert.ToInt32(Math.Floor((double) this.bigFrame.Height * factor)));
    this.bigBaseImage.Scale = factor;
    this.bigFrameImage.Scale = factor;
    this.bigGradeImage.Scale = factor;
    this.bigEffect.Scale = factor;
    this.bigTitle.Scale = factor;
    this.rankLabel.Scale = factor;
    this.countLabel.Scale = factor;
    if (this.bigFrameExtraImage == null)
      return;
    this.bigFrameExtraImage.Scale = factor;
  }

  public void setAlpha(float factor) => this.bigBaseImage.Alpha = factor;

  public void setProgress(double secondspassed)
  {
    this.RemainingTime -= secondspassed;
    this.progressBar.ClipRect = new Rectangle(0, 0, Convert.ToInt32(Math.Floor(this.RemainingTime / this.TotalTime * (double) this.progressBar.ClipRect.Width)), this.progressBar.ClipRect.Height);
  }

  public override void clearControls()
  {
    base.clearControls();
    this.bigFrame = (BaseImage) null;
    this.bigFrameImage = (CustomSelfDrawPanel.CSDImage) null;
    this.bigFrameOver = (BaseImage) null;
  }

  public class CardsNameComparer : IComparer<UICard>
  {
    public int Compare(UICard x, UICard y)
    {
      return x == null || x.Definition == null ? (y == null || y.Definition == null ? 0 : -1) : (y == null || y.Definition == null ? 1 : CardTypes.getDescriptionFromCard(x.Definition.id).ToLower().CompareTo(CardTypes.getDescriptionFromCard(y.Definition.id).ToLower()));
    }
  }

  public class TUTCardsNameComparer : IComparer<UICard>
  {
    public int Compare(UICard x, UICard y)
    {
      if (x == null || x.Definition == null)
        return y == null || y.Definition == null ? 0 : -1;
      if (y == null || y.Definition == null)
        return 1;
      string str = CardTypes.getDescriptionFromCard(x.Definition.id).ToLower();
      string strB = CardTypes.getDescriptionFromCard(y.Definition.id).ToLower();
      if (CardTypes.getCardType(x.Definition.id) == 3201)
        str = "00000";
      if (CardTypes.getCardType(y.Definition.id) == 3201)
        strB = "00000";
      return str.CompareTo(strB);
    }
  }

  public class TUT2CardsNameComparer : IComparer<UICard>
  {
    public int Compare(UICard x, UICard y)
    {
      if (x == null || x.Definition == null)
        return y == null || y.Definition == null ? 0 : -1;
      if (y == null || y.Definition == null)
        return 1;
      string str = CardTypes.getDescriptionFromCard(x.Definition.id).ToLower();
      string strB = CardTypes.getDescriptionFromCard(y.Definition.id).ToLower();
      if (CardTypes.getCardType(x.Definition.id) == 769)
        str = "00000";
      if (CardTypes.getCardType(y.Definition.id) == 769)
        strB = "00000";
      return str.CompareTo(strB);
    }
  }

  public class CardsNameComparerReverse : IComparer<UICard>
  {
    public int Compare(UICard y, UICard x)
    {
      return x == null || x.Definition == null ? (y == null || y.Definition == null ? 0 : -1) : (y == null || y.Definition == null ? 1 : CardTypes.getDescriptionFromCard(x.Definition.id).ToLower().CompareTo(CardTypes.getDescriptionFromCard(y.Definition.id).ToLower()));
    }
  }

  public class CardsIDComparer : IComparer<UICard>
  {
    public int Compare(UICard x, UICard y)
    {
      if (x == null || x.Definition == null)
        return y == null || y.Definition == null ? 0 : -1;
      if (y == null || y.Definition == null)
        return 1;
      int id1 = x.Definition.id;
      int id2 = y.Definition.id;
      int cardType1 = CardTypes.getCardType(id1);
      int cardType2 = CardTypes.getCardType(id2);
      if (cardType1 <= 272)
        cardType1 += 2656;
      if (cardType2 <= 272)
        cardType2 += 2656;
      if (cardType1 < cardType2)
        return -1;
      if (cardType1 > cardType2)
        return 1;
      if (id1 < id2)
        return -1;
      return id1 > id2 ? 1 : 0;
    }
  }

  public class CardsIDComparerReverse : IComparer<UICard>
  {
    public int Compare(UICard y, UICard x)
    {
      if (x == null || x.Definition == null)
        return y == null || y.Definition == null ? 0 : -1;
      if (y == null || y.Definition == null)
        return 1;
      int id1 = x.Definition.id;
      int id2 = y.Definition.id;
      int cardType1 = CardTypes.getCardType(id1);
      int cardType2 = CardTypes.getCardType(id2);
      if (cardType1 <= 272)
        cardType1 += 2656;
      if (cardType2 <= 272)
        cardType2 += 2656;
      if (cardType1 < cardType2)
        return -1;
      if (cardType1 > cardType2)
        return 1;
      if (id1 < id2)
        return -1;
      return id1 > id2 ? 1 : 0;
    }
  }

  public class CardsQuantityComparer : IComparer<UICard>
  {
    public int Compare(UICard x, UICard y)
    {
      if (x == null || x.Definition == null)
        return y == null || y.Definition == null ? 0 : -1;
      if (y == null || y.Definition == null)
        return 1;
      if (x.cardCount > y.cardCount)
        return -1;
      if (x.cardCount < y.cardCount)
        return 0;
      int id1 = x.Definition.id;
      int id2 = y.Definition.id;
      int cardType1 = CardTypes.getCardType(id1);
      int cardType2 = CardTypes.getCardType(id2);
      if (cardType1 <= 272)
        cardType1 += 2656;
      if (cardType2 <= 272)
        cardType2 += 2656;
      if (cardType1 < cardType2)
        return -1;
      if (cardType1 > cardType2)
        return 1;
      if (id1 < id2)
        return -1;
      return id1 > id2 ? 1 : 0;
    }
  }

  public class CardsQuantityComparerReverse : IComparer<UICard>
  {
    public int Compare(UICard y, UICard x)
    {
      if (x == null || x.Definition == null)
        return y == null || y.Definition == null ? 0 : -1;
      if (y == null || y.Definition == null)
        return 1;
      if (x.cardCount > y.cardCount)
        return -1;
      if (x.cardCount < y.cardCount)
        return 0;
      int id1 = x.Definition.id;
      int id2 = y.Definition.id;
      int cardType1 = CardTypes.getCardType(id1);
      int cardType2 = CardTypes.getCardType(id2);
      if (cardType1 <= 272)
        cardType1 += 2656;
      if (cardType2 <= 272)
        cardType2 += 2656;
      if (cardType1 < cardType2)
        return -1;
      if (cardType1 > cardType2)
        return 1;
      if (id1 < id2)
        return -1;
      return id1 > id2 ? 1 : 0;
    }
  }

  public class CardsPriceComparer : IComparer<UICard>
  {
    public int Compare(UICard x, UICard y)
    {
      if (x == null || x.Definition == null)
        return y == null || y.Definition == null ? 0 : -1;
      if (y == null || y.Definition == null)
        return 1;
      if (x.Definition.cardPoints > y.Definition.cardPoints)
        return -1;
      if (x.Definition.cardPoints < y.Definition.cardPoints)
        return 0;
      int id1 = x.Definition.id;
      int id2 = y.Definition.id;
      int cardType1 = CardTypes.getCardType(id1);
      int cardType2 = CardTypes.getCardType(id2);
      if (cardType1 <= 272)
        cardType1 += 2656;
      if (cardType2 <= 272)
        cardType2 += 2656;
      if (cardType1 < cardType2)
        return -1;
      if (cardType1 > cardType2)
        return 1;
      if (id1 < id2)
        return -1;
      return id1 > id2 ? 1 : 0;
    }
  }

  public class CardsPriceComparerReverse : IComparer<UICard>
  {
    public int Compare(UICard y, UICard x)
    {
      if (x == null || x.Definition == null)
        return y == null || y.Definition == null ? 0 : -1;
      if (y == null || y.Definition == null)
        return 1;
      if (x.Definition.cardPoints > y.Definition.cardPoints)
        return -1;
      if (x.Definition.cardPoints < y.Definition.cardPoints)
        return 0;
      int id1 = x.Definition.id;
      int id2 = y.Definition.id;
      int cardType1 = CardTypes.getCardType(id1);
      int cardType2 = CardTypes.getCardType(id2);
      if (cardType1 <= 272)
        cardType1 += 2656;
      if (cardType2 <= 272)
        cardType2 += 2656;
      if (cardType1 < cardType2)
        return -1;
      if (cardType1 > cardType2)
        return 1;
      if (id1 < id2)
        return -1;
      return id1 > id2 ? 1 : 0;
    }
  }
}
