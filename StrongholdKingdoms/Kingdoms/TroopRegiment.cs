// Decompiled with JetBrains decompiler
// Type: Kingdoms.TroopRegiment
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class TroopRegiment
  {
    public int regimentID;
    private List<CastleElement> m_troops;
    public int TroopType;
    private Point m_centerPoint;

    public TroopRegiment.Stance GetStance()
    {
      int num1 = 0;
      int num2 = 0;
      foreach (CastleElement troop in this.m_troops)
      {
        if (troop.aggressiveDefender)
          ++num1;
        else
          ++num2;
      }
      if (num1 > 0 && num2 == 0)
        return TroopRegiment.Stance.AGGRESSIVE;
      return num2 > 0 && num1 == 0 ? TroopRegiment.Stance.DEFENSIVE : TroopRegiment.Stance.MIXED;
    }

    public TroopRegiment(int RegimentID)
    {
      this.regimentID = RegimentID;
      this.m_troops = new List<CastleElement>();
    }

    public void AddTroop(CastleElement element)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      this.m_troops.Add(element);
      this.TroopType = (int) element.elementType;
      this.onChange();
    }

    public void RemoveOne()
    {
      this.m_troops.RemoveAt(this.m_troops.Count - 1);
      this.onChange();
    }

    public CastleElement[] GetTroops()
    {
      return this.m_troops == null ? (CastleElement[]) null : this.m_troops.ToArray();
    }

    public void onChange() => this.m_centerPoint = this.calculateCenterPoint(this.m_troops);

    public Point calculateCenterPoint(List<CastleElement> elements)
    {
      Point centerPoint = new Point();
      foreach (CastleElement element in elements)
      {
        centerPoint.X += (int) element.xPos;
        centerPoint.Y += (int) element.yPos;
      }
      centerPoint.X /= this.m_troops.Count;
      centerPoint.Y /= this.m_troops.Count;
      return centerPoint;
    }

    public Point CenterPoint => this.m_centerPoint;

    public List<TroopRegiment> divideByTowers(CastleMap map)
    {
      Dictionary<long, TroopRegiment> dictionary = new Dictionary<long, TroopRegiment>();
      int num = 0;
      foreach (CastleElement troop in this.GetTroops())
      {
        long key = map.getTowerIDAtPosition((int) troop.xPos, (int) troop.yPos);
        if (key < 0L)
          key = 0L;
        if (!dictionary.ContainsKey(key))
          dictionary.Add(key, new TroopRegiment(num++));
        dictionary[key].AddTroop(troop);
      }
      List<TroopRegiment> troopRegimentList = new List<TroopRegiment>();
      foreach (KeyValuePair<long, TroopRegiment> keyValuePair in dictionary)
        troopRegimentList.Add(keyValuePair.Value);
      return troopRegimentList;
    }

    public List<TroopRegiment> divideByAdjacency()
    {
      List<TroopRegiment> troopRegimentList = new List<TroopRegiment>();
      List<CastleElement> open = new List<CastleElement>();
      List<CastleElement> troops = new List<CastleElement>((IEnumerable<CastleElement>) this.GetTroops());
      while (troops.Count > 0)
      {
        TroopRegiment troopRegiment = new TroopRegiment(0);
        troopRegiment.AddTroop(troops[0]);
        open.Add(troops[0]);
        troops.RemoveAt(0);
        while (open.Count > 0)
        {
          List<CastleElement> adjacent = this.getAdjacent(open, troops);
          open.Clear();
          foreach (CastleElement element in adjacent)
          {
            open.Add(element);
            troopRegiment.AddTroop(element);
            troops.Remove(element);
          }
        }
        troopRegimentList.Add(troopRegiment);
      }
      return troopRegimentList;
    }

    private List<CastleElement> getAdjacent(List<CastleElement> open, List<CastleElement> troops)
    {
      Point a = new Point();
      Point b = new Point();
      List<CastleElement> adjacent = new List<CastleElement>();
      foreach (CastleElement castleElement1 in open)
      {
        foreach (CastleElement castleElement2 in troops.ToArray())
        {
          a.X = (int) castleElement1.xPos;
          a.Y = (int) castleElement1.yPos;
          b.X = (int) castleElement2.xPos;
          b.Y = (int) castleElement2.yPos;
          if (this.isAdjacent(a, b))
          {
            adjacent.Add(castleElement2);
            troops.Remove(castleElement2);
          }
        }
      }
      return adjacent;
    }

    private bool isAdjacent(Point a, Point b)
    {
      return a.X >= b.X - 1 && a.X <= b.X + 1 && a.Y >= b.Y - 1 && a.Y <= b.Y + 1;
    }

    public enum Stance
    {
      AGGRESSIVE,
      DEFENSIVE,
      MIXED,
    }
  }
}
