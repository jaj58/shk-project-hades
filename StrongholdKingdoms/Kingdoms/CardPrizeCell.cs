// Decompiled with JetBrains decompiler
// Type: Kingdoms.CardPrizeCell
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class CardPrizeCell : ContestPrizeCell
  {
    private UICard newCard;

    public override void init()
    {
      this.clearControls();
      this.Quantity.Color = ARGBColors.Black;
      this.Quantity.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.Quantity.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.Icon);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.Quantity);
    }

    public void SetCardImage(ContestPrizeCardDefinition def)
    {
      this.newCard = BuyCardsPanel.makeUICard(CardTypes.getCardDefinition(def.ID), 0, GameEngine.Instance.World.getRank() + 1);
      this.newCard.ScaleAll(0.25 * (double) InterfaceMgr.UIScale);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.newCard);
    }

    public override void resize()
    {
      base.resize();
      this.newCard.Position = new Point(this.Width / 4 - this.newCard.Width / 2, this.Icon.Height / 2 - this.newCard.Height / 2);
      this.newCard.invalidate();
      this.Quantity.invalidate();
    }
  }
}
