using CommonTypes;
using System;
using System.Linq;

namespace Kingdoms
{
  // Holds the defender's active cards while an attack-report replay is being watched,
  // so CardBarGDI renders them instead of the viewer's own cards.
  internal static class EnemyCardOverlay
  {
    public static CardData Data { get; private set; }

    // Build the overlay from a defender research snapshot's active card slots.
    public static void ShowFrom(CastleResearchData rd)
    {
      if (rd == null)
      {
        Clear();
        return;
      }
      int[] rawIds = new int[]
      {
        rd.cardInPlay1, rd.cardInPlay2, rd.cardInPlay3, rd.cardInPlay4, rd.cardInPlay5,
        rd.cardInPlay6, rd.cardInPlay7, rd.cardInPlay8, rd.cardInPlay9, rd.cardInPlay10,
      };
      int[] activeIds = rawIds.Where(id => id > 0).ToArray();
      Data = new CardData()
      {
        cards = activeIds,
        cardsExpiry = Enumerable.Repeat(DateTime.MaxValue, activeIds.Length).ToArray(),
      };
    }

    public static void Clear() => Data = null;
  }
}
