// Decompiled with JetBrains decompiler
// Type: Kingdoms.ContestPrizeList
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System.Collections.Generic;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class ContestPrizeList : CustomSelfDrawPanel.CSDControl
  {
    private CustomSelfDrawPanel.CSDVertScrollBar prizesScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea prizesScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl prizesWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDExtendingPanel prizesInset = new CustomSelfDrawPanel.CSDExtendingPanel();
    private int headerHeight;

    public void init(
      ContestPrizeDefinition prize,
      CustomSelfDrawPanel.CSDControl parentControl,
      int marginX,
      int marginY)
    {
      this.init(prize, parentControl, marginX, marginY, 0);
    }

    public void init(
      ContestPrizeDefinition prize,
      CustomSelfDrawPanel.CSDControl parentControl,
      int marginX,
      int marginY,
      int header)
    {
      ContestPrizeContent content = prize.Content;
      this.clearControls();
      List<ContestPrizeCell> contestPrizeCellList = new List<ContestPrizeCell>();
      this.Size = new Size(parentControl.Width - marginX * 2, parentControl.Height - marginY * 2);
      this.Position = new Point(marginX, marginY);
      this.headerHeight = header;
      if (content.Gold > 0)
      {
        ContestPrizeCell contestPrizeCell = new ContestPrizeCell();
        contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeGold;
        contestPrizeCell.Quantity.Text = content.Gold.ToString();
        contestPrizeCell.init();
        contestPrizeCellList.Add(contestPrizeCell);
      }
      if (content.Honour > 0)
      {
        ContestPrizeCell contestPrizeCell = new ContestPrizeCell();
        contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeHonour;
        contestPrizeCell.Quantity.Text = content.Honour.ToString();
        contestPrizeCell.init();
        contestPrizeCellList.Add(contestPrizeCell);
      }
      if (content.FaithPoints > 0)
      {
        ContestPrizeCell contestPrizeCell = new ContestPrizeCell();
        contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeFaith;
        contestPrizeCell.Quantity.Text = content.FaithPoints.ToString();
        contestPrizeCell.init();
        contestPrizeCellList.Add(contestPrizeCell);
      }
      if (content.RepPoints > 0)
      {
        ContestPrizeCell contestPrizeCell = new ContestPrizeCell();
        contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeReputation;
        contestPrizeCell.Quantity.Text = content.RepPoints.ToString();
        contestPrizeCell.init();
        contestPrizeCellList.Add(contestPrizeCell);
      }
      foreach (ContestPrizeCardDefinition card in content.Cards)
      {
        CardPrizeCell cardPrizeCell = new CardPrizeCell();
        cardPrizeCell.Icon.Image = (Image) GFXLibrary.prizeBlank;
        cardPrizeCell.init();
        cardPrizeCell.Quantity.Text = card.Amount.ToString();
        cardPrizeCell.SetCardImage(card);
        contestPrizeCellList.Add((ContestPrizeCell) cardPrizeCell);
      }
      foreach (ContestPrizeTokenDefinition token in content.Tokens)
      {
        ContestPrizeCell contestPrizeCell = new ContestPrizeCell();
        switch (token.TokenType)
        {
          case 4112:
            contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizePremium7;
            break;
          case 4113:
            contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizePremium2;
            break;
          case 4114:
            contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizePremium30;
            break;
          default:
            contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizePremium;
            break;
        }
        contestPrizeCell.init();
        contestPrizeCell.Quantity.Text = token.Amount.ToString();
        contestPrizeCellList.Add(contestPrizeCell);
      }
      for (int index = 0; index < content.WheelSpins.Count; ++index)
      {
        if (content.WheelSpins[index] > 0)
        {
          ContestPrizeCell contestPrizeCell = new ContestPrizeCell();
          switch (index)
          {
            case 1:
              contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeSpin2;
              break;
            case 2:
              contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeSpin3;
              break;
            case 3:
              contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeSpin4;
              break;
            case 4:
              contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeSpin5;
              break;
            default:
              contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeSpin1;
              break;
          }
          contestPrizeCell.init();
          contestPrizeCell.Quantity.Text = content.WheelSpins[index].ToString();
          contestPrizeCellList.Add(contestPrizeCell);
        }
      }
      if (content.ShieldCharges.Count > 0)
      {
        ContestPrizeCell contestPrizeCell = new ContestPrizeCell();
        contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeShield;
        int num = 0;
        foreach (int shieldCharge in content.ShieldCharges)
          num += shieldCharge;
        if (num > 0)
        {
          contestPrizeCell.Quantity.Text = content.ShieldCharges.Count.ToString();
          contestPrizeCell.init();
          contestPrizeCellList.Add(contestPrizeCell);
        }
      }
      foreach (ContestPrizePackDefinition pack in content.Packs)
      {
        CustomPrizeCell customPrizeCell = new CustomPrizeCell();
        CardTypes.CardOffer cardOffer = GameEngine.Instance.cardPackManager.GetCardOffer(pack.OfferID);
        customPrizeCell.init();
        customPrizeCell.SetImage(GameEngine.Instance.cardPackManager.getCardPackOverImage(cardOffer.Category), 0.7);
        customPrizeCell.Quantity.Text = pack.Amount.ToString();
        contestPrizeCellList.Add((ContestPrizeCell) customPrizeCell);
      }
      int count = contestPrizeCellList.Count;
      if (count <= 0)
        return;
      this.prizesScrollArea.Position = new Point(0, this.headerHeight);
      this.prizesScrollArea.Size = new Size(this.Width - 24, this.Height - marginY - this.headerHeight);
      this.prizesScrollArea.ClipRect = new Rectangle(Point.Empty, this.prizesScrollArea.Size);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.prizesScrollArea);
      this.prizesInset.Size = this.prizesScrollArea.Size;
      this.prizesInset.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      this.prizesInset.Position = new Point(0, this.headerHeight);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.prizesInset);
      int y = 0;
      for (int index = 0; index < count; ++index)
      {
        contestPrizeCellList[index].Width = this.prizesScrollArea.Width * 2 / 3;
        contestPrizeCellList[index].Height = this.prizesScrollArea.Width * 2 / 9;
        contestPrizeCellList[index].resize();
        contestPrizeCellList[index].Position = new Point(this.prizesScrollArea.Width / 2 - contestPrizeCellList[index].Width / 2, y);
        this.prizesScrollArea.addControl((CustomSelfDrawPanel.CSDControl) contestPrizeCellList[index]);
        contestPrizeCellList[index].invalidate();
        y += contestPrizeCellList[index].Height + 3;
      }
      if (y <= this.Height - 3 - header)
        return;
      this.prizesScrollArea.Height = y - 3;
      this.prizesScrollBar.Position = new Point(this.prizesScrollArea.Width - 24, this.headerHeight);
      this.prizesScrollBar.Size = new Size(24, this.prizesScrollArea.ClipRect.Height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.prizesScrollBar);
      this.prizesScrollBar.Value = 0;
      this.prizesScrollBar.NumVisibleLines = this.prizesScrollArea.ClipRect.Height;
      this.prizesScrollBar.Max = y - this.prizesScrollArea.ClipRect.Height;
      this.prizesScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
      this.prizesScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.prizesScrollBarMoved));
      this.prizesScrollArea.invalidate();
      this.prizesScrollBar.invalidate();
    }

    private void prizesScrollBarMoved()
    {
      int y = this.prizesScrollBar.Value;
      this.prizesScrollArea.Position = new Point(this.prizesScrollArea.X, -y + this.headerHeight);
      this.prizesScrollArea.ClipRect = new Rectangle(this.prizesScrollArea.ClipRect.X, y, this.prizesScrollArea.ClipRect.Width, this.prizesScrollArea.ClipRect.Height);
      this.prizesScrollArea.invalidate();
      this.prizesScrollBar.invalidate();
      this.prizesInset.invalidate();
      this.invalidate();
    }
  }
}
