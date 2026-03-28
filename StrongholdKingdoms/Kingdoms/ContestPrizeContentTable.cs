// Decompiled with JetBrains decompiler
// Type: Kingdoms.ContestPrizeContentTable
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
  public class ContestPrizeContentTable : CustomSelfDrawPanel.CSDControl
  {
    public void init(ContestPrizeDefinition def, CustomSelfDrawPanel.CSDControl parentControl)
    {
      this.init(def.Content, parentControl, 30, 30);
    }

    public void init(
      ContestPrizeContent content,
      CustomSelfDrawPanel.CSDControl parentControl,
      int marginX,
      int marginY)
    {
      this.clearControls();
      List<ContestPrizeCell> contestPrizeCellList = new List<ContestPrizeCell>();
      this.Size = new Size(parentControl.Width - marginX * 2, parentControl.Height * 3 / 4 - 50);
      this.Position = new Point(marginX, parentControl.Height / 4 + 10);
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
      if (content.Cards.Count > 0 && content.Cards[0].Amount > 0)
      {
        CardPrizeCell cardPrizeCell = new CardPrizeCell();
        cardPrizeCell.Icon.Image = (Image) GFXLibrary.prizeBlank;
        cardPrizeCell.init();
        cardPrizeCell.Quantity.Text = content.Cards[0].Amount.ToString();
        cardPrizeCell.SetCardImage(content.Cards[0]);
        contestPrizeCellList.Add((ContestPrizeCell) cardPrizeCell);
      }
      if (content.Tokens.Count > 0)
      {
        ContestPrizeCell contestPrizeCell = new ContestPrizeCell();
        contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeTokens;
        int num = 0;
        foreach (ContestPrizeTokenDefinition token in content.Tokens)
          num += token.Amount;
        if (num > 0)
        {
          contestPrizeCell.Quantity.Text = num.ToString();
          contestPrizeCell.init();
          contestPrizeCellList.Add(contestPrizeCell);
        }
      }
      if (content.WheelSpins.Count > 0)
      {
        ContestPrizeCell contestPrizeCell = new ContestPrizeCell();
        contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeWheelspins;
        int num = 0;
        foreach (int wheelSpin in content.WheelSpins)
          num += wheelSpin;
        if (num > 0)
        {
          contestPrizeCell.Quantity.Text = num.ToString();
          contestPrizeCell.init();
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
      if (content.Packs.Count > 0)
      {
        ContestPrizeCell contestPrizeCell = new ContestPrizeCell();
        contestPrizeCell.Icon.Image = (Image) GFXLibrary.prizeCardPack;
        int num = 0;
        foreach (ContestPrizePackDefinition pack in content.Packs)
          num += pack.Amount;
        if (num > 0)
        {
          contestPrizeCell.Quantity.Text = num.ToString();
          contestPrizeCell.init();
          contestPrizeCellList.Add(contestPrizeCell);
        }
      }
      int count = contestPrizeCellList.Count;
      int num1 = 1;
      if (count > 6)
        num1 = 3;
      else if (count > 3)
        num1 = 2;
      int[] numArray = new int[3];
      switch (count)
      {
        case 1:
          numArray[0] = 1;
          numArray[1] = 0;
          numArray[2] = 0;
          break;
        case 2:
          numArray[0] = 2;
          numArray[1] = 0;
          numArray[2] = 0;
          break;
        case 3:
          numArray[0] = 3;
          numArray[1] = 0;
          numArray[2] = 0;
          break;
        case 4:
          numArray[0] = 2;
          numArray[1] = 2;
          numArray[2] = 0;
          break;
        case 5:
          numArray[0] = 3;
          numArray[1] = 2;
          numArray[2] = 0;
          break;
        case 6:
          numArray[0] = 3;
          numArray[1] = 3;
          numArray[2] = 0;
          break;
        case 7:
          numArray[0] = 3;
          numArray[1] = 2;
          numArray[2] = 2;
          break;
        case 8:
          numArray[0] = 3;
          numArray[1] = 3;
          numArray[2] = 2;
          break;
        case 9:
          numArray[0] = 3;
          numArray[1] = 3;
          numArray[2] = 3;
          break;
      }
      int index1 = 0;
      for (int index2 = 0; index2 < num1; ++index2)
      {
        for (int index3 = 0; index3 < numArray[index2]; ++index3)
        {
          contestPrizeCellList[index1].Width = this.Width / numArray[index2];
          contestPrizeCellList[index1].Height = this.Height / num1;
          contestPrizeCellList[index1].Position = new Point(this.Width / numArray[index2] * index3, this.Height / num1 * index2);
          contestPrizeCellList[index1].resize();
          this.addControl((CustomSelfDrawPanel.CSDControl) contestPrizeCellList[index1]);
          contestPrizeCellList[index1].invalidate();
          ++index1;
        }
      }
      this.invalidate();
    }

    public void init(
      int gold,
      int faith,
      int honour,
      int rep,
      int cardID,
      int cardCount,
      int packCount,
      int tokenCount,
      int spinCount,
      int shieldCount,
      CustomSelfDrawPanel.CSDControl parentControl)
    {
      this.init(new ContestPrizeContent()
      {
        Gold = gold,
        FaithPoints = faith,
        Honour = honour,
        RepPoints = rep,
        Cards = {
          new ContestPrizeCardDefinition()
          {
            Amount = cardCount,
            Name = CardTypes.getDescriptionFromCard(cardID),
            ID = cardID
          }
        },
        Packs = {
          new ContestPrizePackDefinition() { Amount = packCount }
        },
        Tokens = {
          new ContestPrizeTokenDefinition() { Amount = tokenCount }
        },
        WheelSpins = {
          spinCount
        },
        ShieldCharges = {
          shieldCount
        }
      }, parentControl, 15, 25);
    }
  }
}
