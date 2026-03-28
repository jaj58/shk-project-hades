// Decompiled with JetBrains decompiler
// Type: Kingdoms.CastleMapRendering
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class CastleMapRendering
  {
    private const double RAD2DEG = 57.2957795;
    public SpriteWrapper backgroundSprite;
    public GraphicsMgr gfx;
    public int pulse;
    public int pulseValue;
    private int[] archerAttackWall = new int[12]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      5,
      5,
      4,
      3,
      2,
      1
    };
    private int[] archerAttackUnit = new int[6]
    {
      0,
      9,
      10,
      11,
      10,
      9
    };
    private int[] archerAttackMoat = new int[16]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15
    };
    private int[] archerBlocked = new int[8]
    {
      0,
      0,
      1,
      1,
      2,
      2,
      1,
      1
    };
    private int[] archerDyingArrow = new int[24]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
    };
    private int[] archerDyingNormal = new int[32]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      26,
      27,
      28,
      29,
      30,
      31
    };
    private int[] archerAttackFiringStraight = new int[28]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      7,
      8,
      8,
      9,
      9,
      10,
      10,
      11,
      12,
      13,
      13,
      13,
      13,
      14,
      15,
      16,
      16,
      16,
      16,
      16
    };
    private int[] archerAttackFiringDown = new int[28]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      25,
      25,
      25,
      26,
      27,
      28,
      28,
      28,
      28,
      28
    };
    private int[] archerAttackFiringUp = new int[28]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      29,
      30,
      31,
      32,
      33,
      34,
      35,
      36,
      37,
      37,
      37,
      37,
      38,
      39,
      40,
      40,
      40,
      40,
      40
    };
    private int[] pikemanAttackJab = new int[15]
    {
      0,
      1,
      2,
      3,
      3,
      3,
      3,
      2,
      2,
      1,
      1,
      0,
      0,
      0,
      0
    };
    private int[] pikemanAttackJabQuick = new int[7]
    {
      1,
      2,
      3,
      3,
      2,
      1,
      0
    };
    private int[] pikemanAttackChop = new int[15]
    {
      4,
      5,
      5,
      6,
      6,
      7,
      7,
      7,
      7,
      7,
      7,
      2,
      1,
      0,
      0
    };
    private int[] pikemanIdle = new int[38]
    {
      0,
      0,
      0,
      1,
      1,
      1,
      2,
      2,
      2,
      3,
      3,
      3,
      2,
      2,
      2,
      1,
      1,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    };
    private int[] pikemanBlocked = new int[8]
    {
      0,
      0,
      1,
      1,
      2,
      2,
      1,
      1
    };
    private int[] pikemanAttackMoat = new int[15]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14
    };
    private int[] pikemanDyingArrow = new int[24]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
    };
    private int[] pikemanDyingNormal = new int[24]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
    };
    private int[] swordsmanAttackWall = new int[15]
    {
      0,
      1,
      2,
      3,
      3,
      4,
      5,
      6,
      7,
      8,
      8,
      8,
      9,
      10,
      11
    };
    private int[] swordsmanAttackUnit = new int[14]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      6,
      6,
      7,
      8,
      9,
      10,
      11
    };
    private int[] swordsmanAttackMoat = new int[15]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14
    };
    private int[] swordsmanBlocked = new int[8]
    {
      0,
      0,
      1,
      1,
      2,
      2,
      1,
      1
    };
    private int[] swordsmanDyingArrow = new int[24]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
    };
    private int[] swordsmanDyingNormal = new int[24]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
    };
    private int[] captainAttackWall = new int[57]
    {
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      8,
      9,
      10,
      10,
      10,
      10,
      11,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      23,
      23,
      24,
      25,
      26,
      27,
      28,
      29,
      30,
      31,
      32,
      33,
      34,
      35,
      36,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      23,
      23,
      24
    };
    private int[] captainAttackUnit = new int[57]
    {
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      8,
      9,
      10,
      10,
      10,
      10,
      11,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      23,
      23,
      24,
      25,
      26,
      27,
      28,
      29,
      30,
      31,
      32,
      33,
      34,
      35,
      36,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      23,
      23,
      24
    };
    private int[] captainAttackMoat = new int[57]
    {
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      8,
      9,
      10,
      10,
      10,
      10,
      11,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      23,
      23,
      24,
      25,
      26,
      27,
      28,
      29,
      30,
      31,
      32,
      33,
      34,
      35,
      36,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      23,
      23,
      24
    };
    private int[] captainDyingAnim = new int[28]
    {
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      26,
      27,
      28
    };
    private int[] captainIdle = new int[73]
    {
      1,
      1,
      1,
      1,
      1,
      2,
      2,
      2,
      2,
      2,
      3,
      3,
      3,
      3,
      3,
      3,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      4,
      5,
      5,
      5,
      5,
      5,
      6,
      6,
      6,
      6,
      6,
      6,
      5,
      5,
      5,
      5,
      5,
      5,
      5,
      3,
      3,
      3,
      3,
      3,
      3,
      2,
      2,
      2,
      2,
      2
    };
    private int[] wolfAttackUnit = new int[28]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      26,
      27
    };
    private int[] wolfDyingArrow = new int[24]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
    };
    private int[] wolfDyingNormal = new int[8]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7
    };
    private int[] knightAttackUnit = new int[12]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11
    };
    private int[] knightHorseIdle = new int[100]
    {
      0,
      1,
      2,
      2,
      2,
      2,
      2,
      2,
      2,
      2,
      2,
      2,
      2,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      1,
      2,
      2,
      2,
      2,
      2,
      2,
      2,
      2,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      1,
      2,
      2,
      2,
      2,
      2,
      1,
      0,
      0,
      0,
      0,
      1,
      2,
      2,
      2,
      1,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      1,
      2,
      2,
      2,
      2,
      2,
      1,
      0,
      0,
      0,
      0
    };
    private int[] knightDyingArrow = new int[24]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
    };
    private int[] knightDyingNormal = new int[24]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
    };
    private int[] peasantAttack = new int[16]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15
    };
    private int[] peasantIdle = new int[18]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      8,
      7,
      6,
      5,
      4,
      3,
      2,
      1
    };
    private int[] peasantBlocked = new int[18]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      8,
      7,
      6,
      5,
      4,
      3,
      2,
      1
    };
    private int[] peasantAttackMoat = new int[15]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14
    };
    private int[] peasantDyingArrow = new int[24]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
    };
    private int[] peasantDyingNormal = new int[24]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23
    };
    private int[] catapultAnim = new int[28]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      26,
      27
    };
    private int[] fireStart = new int[7]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6
    };
    private int[] fireLoop = new int[35]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      26,
      27,
      28,
      29,
      30,
      31,
      32,
      33,
      34
    };
    private int[] fireEnd = new int[7]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6
    };
    private int[] dyingOnFire = new int[39]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      26,
      27,
      28,
      29,
      30,
      31,
      32,
      33,
      34,
      35,
      36,
      37,
      38
    };
    private int[] captainBattleCryAnim = new int[9]
    {
      335,
      334,
      333,
      332,
      331,
      332,
      333,
      334,
      335
    };
    private List<CastleMapRendering.RockChip> rockChips = new List<CastleMapRendering.RockChip>();
    private Random chipRand = new Random();
    private List<CastleMapRendering.RockSmoke> rockSmoke = new List<CastleMapRendering.RockSmoke>();
    private bool isCollapsed;
    private static bool invalidateWallsOnCollapseChange;
    private int elementsDrawn;
    public int[] moatSurroundTests = new int[16]
    {
      -1,
      -1,
      0,
      -1,
      1,
      -1,
      -1,
      0,
      1,
      0,
      -1,
      1,
      0,
      1,
      1,
      1
    };
    public int[] moatSurroundLogic = new int[424]
    {
      268,
      2,
      0,
      2,
      1,
      0,
      1,
      1,
      2,
      269,
      1,
      1,
      2,
      1,
      0,
      2,
      0,
      2,
      270,
      2,
      1,
      1,
      0,
      1,
      2,
      0,
      2,
      271,
      2,
      0,
      2,
      0,
      1,
      2,
      1,
      1,
      272,
      1,
      1,
      2,
      1,
      0,
      1,
      1,
      2,
      273,
      1,
      1,
      1,
      1,
      1,
      2,
      0,
      2,
      274,
      2,
      1,
      1,
      0,
      1,
      2,
      1,
      1,
      275,
      2,
      0,
      2,
      1,
      1,
      1,
      1,
      1,
      276,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      277,
      1,
      1,
      1,
      1,
      1,
      0,
      1,
      1,
      278,
      0,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      279,
      1,
      1,
      0,
      1,
      1,
      1,
      1,
      1,
      280,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      0,
      281,
      2,
      1,
      2,
      0,
      0,
      2,
      1,
      2,
      282,
      2,
      0,
      2,
      1,
      1,
      2,
      0,
      2,
      283,
      0,
      1,
      0,
      1,
      1,
      0,
      1,
      0,
      284,
      0,
      1,
      2,
      1,
      0,
      2,
      0,
      2,
      285,
      2,
      0,
      2,
      0,
      1,
      2,
      1,
      0,
      286,
      2,
      1,
      0,
      0,
      1,
      2,
      0,
      2,
      287,
      2,
      0,
      2,
      1,
      0,
      0,
      1,
      2,
      288,
      2,
      0,
      2,
      0,
      0,
      2,
      0,
      2,
      289,
      0,
      1,
      2,
      1,
      0,
      0,
      1,
      2,
      290,
      2,
      0,
      2,
      1,
      1,
      0,
      1,
      0,
      291,
      0,
      1,
      0,
      1,
      1,
      2,
      0,
      2,
      292,
      2,
      1,
      0,
      0,
      1,
      2,
      1,
      0,
      293,
      2,
      0,
      2,
      1,
      0,
      2,
      0,
      2,
      294,
      2,
      0,
      2,
      0,
      0,
      2,
      1,
      2,
      295,
      2,
      1,
      2,
      0,
      0,
      2,
      0,
      2,
      296,
      2,
      0,
      2,
      0,
      1,
      2,
      0,
      2,
      297,
      2,
      0,
      2,
      1,
      1,
      1,
      1,
      0,
      298,
      0,
      1,
      2,
      1,
      0,
      1,
      1,
      2,
      299,
      1,
      1,
      0,
      1,
      1,
      2,
      0,
      2,
      300,
      1,
      1,
      2,
      1,
      0,
      0,
      1,
      2,
      301,
      0,
      1,
      1,
      1,
      1,
      2,
      0,
      2,
      302,
      2,
      1,
      1,
      0,
      1,
      2,
      1,
      0,
      303,
      2,
      1,
      0,
      0,
      1,
      2,
      1,
      1,
      304,
      2,
      0,
      2,
      1,
      1,
      0,
      1,
      1,
      305,
      1,
      1,
      0,
      1,
      1,
      1,
      1,
      0,
      306,
      1,
      1,
      1,
      1,
      1,
      0,
      1,
      0,
      307,
      0,
      1,
      1,
      1,
      1,
      0,
      1,
      1,
      308,
      0,
      1,
      0,
      1,
      1,
      1,
      1,
      1,
      309,
      1,
      1,
      0,
      1,
      1,
      0,
      1,
      1,
      310,
      0,
      1,
      1,
      1,
      1,
      1,
      1,
      0,
      311,
      0,
      1,
      0,
      1,
      1,
      0,
      1,
      1,
      312,
      0,
      1,
      0,
      1,
      1,
      1,
      1,
      0,
      313,
      1,
      1,
      0,
      1,
      1,
      0,
      1,
      0,
      314,
      0,
      1,
      1,
      1,
      1,
      0,
      1,
      0,
      -1
    };

    public CastleMapRendering(GraphicsMgr gfx)
    {
      this.gfx = gfx;
      this.backgroundSprite = new SpriteWrapper();
    }

    public void drawDyingTroops(CastleMap m_castleMap)
    {
      if (m_castleMap.castleCombat.Paused)
        return;
      foreach (BattleTroop dyingTroop in m_castleMap.castleCombat.getDyingTroops())
      {
        int xPos = (int) dyingTroop.xPos;
        int yPos = (int) dyingTroop.yPos;
        int textureID = -1;
        int num1 = -1;
        int spriteNo = 0;
        int num2 = -1;
        int num3 = 32;
        int num4 = (int) dyingTroop.elementType;
        if (dyingTroop.dyingOnFire)
        {
          textureID = GFXLibrary.Instance.ManOnFireTexID;
          num4 = 0;
          spriteNo = (dyingTroop.facing + 6 & 7) + this.dyingOnFire[Math.Min(dyingTroop.animFrame, this.dyingOnFire.Length - 1)] * 8;
          num3 = 39;
        }
        else
        {
          switch (dyingTroop.elementType)
          {
            case 70:
              textureID = GFXLibrary.Instance.PeasantAnimTexID;
              spriteNo = !dyingTroop.dyingArrowAttack ? 152 + this.peasantDyingNormal[Math.Min(dyingTroop.animFrame, this.peasantDyingNormal.Length - 1)] : 128 + this.peasantDyingArrow[Math.Min(dyingTroop.animFrame, this.peasantDyingArrow.Length - 1)];
              break;
            case 71:
              textureID = GFXLibrary.Instance.SwordsmanAnimTexID;
              spriteNo = !dyingTroop.dyingArrowAttack ? 352 + this.swordsmanDyingNormal[Math.Min(dyingTroop.animFrame, this.swordsmanDyingNormal.Length - 1)] : 400 + this.swordsmanDyingArrow[Math.Min(dyingTroop.animFrame, this.swordsmanDyingArrow.Length - 1)];
              break;
            case 72:
              textureID = GFXLibrary.Instance.ArcherAnimTexID;
              spriteNo = !dyingTroop.dyingArrowAttack ? 464 + this.archerDyingNormal[Math.Min(dyingTroop.animFrame, this.archerDyingNormal.Length - 1)] : 496 + this.archerDyingArrow[Math.Min(dyingTroop.animFrame, this.archerDyingArrow.Length - 1)];
              break;
            case 73:
              textureID = GFXLibrary.Instance.PikemanAnimTexID;
              spriteNo = !dyingTroop.dyingArrowAttack ? 192 + this.pikemanDyingNormal[Math.Min(dyingTroop.animFrame, this.pikemanDyingNormal.Length - 1)] : 216 + this.pikemanDyingArrow[Math.Min(dyingTroop.animFrame, this.pikemanDyingArrow.Length - 1)];
              break;
            case 75:
              textureID = GFXLibrary.Instance.OilPotAnimTexID;
              break;
            case 77:
              textureID = GFXLibrary.Instance.WolfAnimTexID;
              spriteNo = !dyingTroop.dyingArrowAttack ? 464 + this.wolfDyingNormal[Math.Min(dyingTroop.animFrame, this.wolfDyingNormal.Length - 1)] : 440 + this.wolfDyingArrow[Math.Min(dyingTroop.animFrame, this.wolfDyingArrow.Length - 1)];
              break;
            case 78:
              textureID = GFXLibrary.Instance.KnightAnimTexID;
              num1 = GFXLibrary.Instance.KnightTopAnimTexID;
              if (dyingTroop.dyingArrowAttack)
              {
                spriteNo = 176 + this.knightDyingArrow[Math.Min(dyingTroop.animFrame, this.knightDyingArrow.Length - 1)];
                num2 = 248 + this.knightDyingArrow[Math.Min(dyingTroop.animFrame, this.knightDyingArrow.Length - 1)];
                break;
              }
              spriteNo = 152 + this.knightDyingNormal[Math.Min(dyingTroop.animFrame, this.knightDyingNormal.Length - 1)];
              num2 = 224 + this.knightDyingNormal[Math.Min(dyingTroop.animFrame, this.knightDyingNormal.Length - 1)];
              break;
            case 85:
              textureID = GFXLibrary.Instance.CaptainAnimTexID;
              spriteNo = 575 + this.captainDyingAnim[Math.Min(dyingTroop.animFrame, this.captainDyingAnim.Length - 1)];
              break;
            case 90:
              textureID = GFXLibrary.Instance.PeasantRedAnimTexID;
              spriteNo = !dyingTroop.dyingArrowAttack ? 152 + this.peasantDyingNormal[Math.Min(dyingTroop.animFrame, this.peasantDyingNormal.Length - 1)] : 128 + this.peasantDyingArrow[Math.Min(dyingTroop.animFrame, this.peasantDyingArrow.Length - 1)];
              break;
            case 91:
              textureID = GFXLibrary.Instance.SwordsmanRedAnimTexID;
              spriteNo = !dyingTroop.dyingArrowAttack ? 352 + this.swordsmanDyingNormal[Math.Min(dyingTroop.animFrame, this.swordsmanDyingNormal.Length - 1)] : 400 + this.swordsmanDyingArrow[Math.Min(dyingTroop.animFrame, this.swordsmanDyingArrow.Length - 1)];
              break;
            case 92:
              textureID = GFXLibrary.Instance.ArcherRedAnimTexID;
              spriteNo = !dyingTroop.dyingArrowAttack ? 464 + this.archerDyingNormal[Math.Min(dyingTroop.animFrame, this.archerDyingNormal.Length - 1)] : 496 + this.archerDyingArrow[Math.Min(dyingTroop.animFrame, this.archerDyingArrow.Length - 1)];
              break;
            case 93:
              textureID = GFXLibrary.Instance.PikemanRedAnimTexID;
              spriteNo = !dyingTroop.dyingArrowAttack ? 192 + this.pikemanDyingNormal[Math.Min(dyingTroop.animFrame, this.pikemanDyingNormal.Length - 1)] : 216 + this.pikemanDyingArrow[Math.Min(dyingTroop.animFrame, this.pikemanDyingArrow.Length - 1)];
              break;
            case 94:
              textureID = GFXLibrary.Instance.CatapultAnimTexID;
              break;
            case 100:
            case 101:
            case 102:
            case 103:
            case 104:
            case 105:
            case 106:
            case 107:
              textureID = GFXLibrary.Instance.CaptainAnimRedTexID;
              spriteNo = 575 + this.captainDyingAnim[Math.Min(dyingTroop.animFrame, this.captainDyingAnim.Length - 1)];
              break;
          }
        }
        int num5 = 1;
        if (num1 >= 0)
          num5 = 2;
        for (int index = 0; index < num5; ++index)
        {
          if (index == 1)
          {
            textureID = num1;
            spriteNo = num2;
          }
          SpriteWrapper nextExtraSprite = m_castleMap.getNextExtraSprite(textureID, spriteNo);
          nextExtraSprite.TroopType = num4;
          Point point1 = CastleMap.castleUnitSpritePoint[xPos, yPos];
          if (dyingTroop.moving)
          {
            Point point2 = CastleMap.castleUnitSpritePoint[dyingTroop.otherX, dyingTroop.otherY];
            float moveRatio = dyingTroop.getMoveRatio();
            point1.X = (int) ((double) (point1.X - point2.X) * (double) moveRatio + (double) point2.X);
            point1.Y = (int) ((double) (point1.Y - point2.Y) * (double) moveRatio + (double) point2.Y);
          }
          int num6 = CastleMap.displayCollapsed || m_castleMap.battleMode && CastleMap.AlwaysCollapsedWallsInBattles ? (int) m_castleMap.castleLayout.collapsedHeightMap[xPos, yPos] : (int) m_castleMap.castleLayout.fullHeightMap[xPos, yPos];
          point1.Y -= num6;
          nextExtraSprite.Visible = true;
          nextExtraSprite.Center = dyingTroop.elementType != (byte) 78 ? new PointF(50f, 66f) : new PointF(75f, 100f);
          nextExtraSprite.PosX = (float) point1.X;
          nextExtraSprite.PosY = (float) point1.Y;
          if (dyingTroop.animFrame > num3)
          {
            int alpha = (num3 + 16 - dyingTroop.animFrame) * 16;
            if (alpha < 16)
              alpha = 16;
            else if (alpha > (int) byte.MaxValue)
              alpha = (int) byte.MaxValue;
            nextExtraSprite.ColorToUse = Color.FromArgb(alpha, ARGBColors.White);
          }
          if (CastleMap.castleSpriteGrid[xPos, yPos] != null)
          {
            if (CastleMap.castleSpriteGrid[xPos, yPos].Visible)
            {
              SpriteWrapper spriteWrapper = CastleMap.castleSpriteGrid[xPos, yPos];
              spriteWrapper.TroopType = 0;
              nextExtraSprite.PosX -= spriteWrapper.PosX;
              nextExtraSprite.PosY -= spriteWrapper.PosY;
              spriteWrapper.DrawChildrenWithParent = true;
              spriteWrapper.AddChild(nextExtraSprite);
            }
            else
            {
              long elem = m_castleMap.castleLayout.elemMap[xPos, yPos];
              if (elem >= 0L || elem == -1L)
              {
                CastleElement infrastructureElement = (CastleElement) CastleMap.activeCastleInfrastructureElements[elem];
                if (infrastructureElement != null)
                {
                  SpriteWrapper spriteWrapper = CastleMap.castleSpriteGrid[(int) infrastructureElement.xPos, (int) infrastructureElement.yPos];
                  spriteWrapper.TroopType = 0;
                  if (spriteWrapper.Visible)
                  {
                    nextExtraSprite.PosX -= spriteWrapper.PosX;
                    nextExtraSprite.PosY -= spriteWrapper.PosY;
                    spriteWrapper.DrawChildrenWithParent = true;
                    spriteWrapper.AddChild(nextExtraSprite);
                  }
                  else
                    this.backgroundSprite.AddChild(nextExtraSprite, 2);
                }
                else
                  this.backgroundSprite.AddChild(nextExtraSprite, 2);
              }
              else
                this.backgroundSprite.AddChild(nextExtraSprite, 2 + index);
            }
          }
          else
            this.backgroundSprite.AddChild(nextExtraSprite, 2 + index);
        }
      }
    }

    public void drawArrows(CastleMap m_castleMap)
    {
      foreach (BattleArrow arrow in m_castleMap.castleCombat.getArrows())
      {
        int textureID;
        int x;
        if (arrow.bolt)
        {
          textureID = GFXLibrary.Instance.Missile2TexID;
          x = 50;
        }
        else
        {
          textureID = GFXLibrary.Instance.MissileTexID;
          x = 28;
        }
        for (int index = 0; index < arrow.trail.Length; ++index)
        {
          if (arrow.trail[index] != null && arrow.trail[index].visible)
          {
            SpriteWrapper nextExtraSprite = m_castleMap.getNextExtraSprite(textureID, arrow.trail[index].gfx + arrow.trail[index].tilt * 16);
            PointF pointF = new PointF((float) x, (float) (x + 27 + arrow.trail[index].height));
            nextExtraSprite.Center = pointF;
            nextExtraSprite.PosX = arrow.trail[index].pos.X;
            nextExtraSprite.PosY = arrow.trail[index].pos.Y;
            nextExtraSprite.ColorToUse = Color.FromArgb((int) byte.MaxValue - (int) byte.MaxValue * index / arrow.trail.Length, ARGBColors.White);
            this.backgroundSprite.AddChild(nextExtraSprite, 2);
          }
        }
        Point point1 = CastleMap.castleUnitSpritePoint[arrow.startX, arrow.startY];
        Point point2 = CastleMap.castleUnitSpritePoint[arrow.targetX, arrow.targetY];
        float num1 = (float) arrow.travelledDist / (float) arrow.fullDist;
        int startHeight;
        int targetHeight;
        int num2;
        if (CastleMap.displayCollapsed || m_castleMap.battleMode && CastleMap.AlwaysCollapsedWallsInBattles)
        {
          startHeight = !arrow.turretArrow ? (int) m_castleMap.castleLayout.collapsedHeightMap[arrow.startX, arrow.startY] : arrow.turrentCollapsedHeight;
          targetHeight = (int) m_castleMap.castleLayout.collapsedHeightMap[arrow.targetX, arrow.targetY];
          if (arrow.tilt < 0)
            arrow.tilt = this.getArrowTiltSpriteID(arrow, startHeight, targetHeight);
          num2 = arrow.tilt;
        }
        else
        {
          startHeight = !arrow.turretArrow ? (int) m_castleMap.castleLayout.fullHeightMap[arrow.startX, arrow.startY] : arrow.turretFullHeight;
          targetHeight = (int) m_castleMap.castleLayout.fullHeightMap[arrow.targetX, arrow.targetY];
          if (arrow.tiltHigh < 0)
            arrow.tiltHigh = this.getArrowTiltSpriteID(arrow, startHeight, targetHeight);
          num2 = arrow.tiltHigh;
        }
        point2.X = (int) ((double) (point2.X - point1.X) * (double) num1 + (double) point1.X);
        point2.Y = (int) ((double) (point2.Y - point1.Y) * (double) num1 + (double) point1.Y);
        int num3 = (int) ((double) (targetHeight - startHeight) * (double) num1 + (double) startHeight);
        SpriteWrapper nextExtraSprite1 = m_castleMap.getNextExtraSprite(textureID, arrow.gfxDirc + num2 * 16);
        PointF pointF1 = new PointF((float) x, (float) (x + 27 + num3));
        nextExtraSprite1.Center = pointF1;
        nextExtraSprite1.PosX = (float) point2.X;
        nextExtraSprite1.PosY = (float) point2.Y;
        this.backgroundSprite.AddChild(nextExtraSprite1, 2);
        if (!m_castleMap.castleCombat.Paused && (!arrow.bolt || (m_castleMap.castleCombat.TickValue & 1) == 0) && arrow.trail[0] != null)
        {
          for (int index = arrow.trail.Length - 1; index > 0; --index)
          {
            arrow.trail[index].pos = arrow.trail[index - 1].pos;
            arrow.trail[index].height = arrow.trail[index - 1].height;
            arrow.trail[index].visible = arrow.trail[index - 1].visible;
            arrow.trail[index].tilt = arrow.trail[index - 1].tilt;
            arrow.trail[index].gfx = arrow.trail[index - 1].gfx;
          }
          arrow.trail[0].pos = new PointF((float) point2.X, (float) point2.Y);
          arrow.trail[0].height = num3;
          arrow.trail[0].gfx = arrow.gfxDirc;
          arrow.trail[0].tilt = num2;
          arrow.trail[0].visible = true;
        }
      }
    }

    public void drawRocks(CastleMap m_castleMap)
    {
      foreach (RockMissile rock in m_castleMap.castleCombat.getRocks())
      {
        if (rock.firingDelay <= 0)
        {
          for (int index = 0; index < rock.trail.Length; ++index)
          {
            if (rock.trail[index].visible)
            {
              SpriteWrapper child = !rock.bombard ? m_castleMap.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 144) : m_castleMap.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 152);
              PointF pointF = new PointF(28f, (float) (28 + rock.trail[index].height));
              child.Center = pointF;
              child.PosX = rock.trail[index].pos.X;
              child.PosY = rock.trail[index].pos.Y;
              child.ColorToUse = Color.FromArgb((int) byte.MaxValue - (int) byte.MaxValue * index / rock.trail.Length, ARGBColors.White);
              this.backgroundSprite.AddChild(child, 2);
            }
          }
          Point point1 = CastleMap.castleUnitSpritePoint[rock.startX, rock.startY];
          Point point2 = CastleMap.castleUnitSpritePoint[rock.targX, rock.targY];
          int height = (int) rock.height;
          double num = rock.distTravelled / rock.journeyLength;
          point2.X = (int) ((double) (point2.X - point1.X) * num + (double) point1.X);
          point2.Y = (int) ((double) (point2.Y - point1.Y) * num + (double) point1.Y);
          SpriteWrapper child1 = !rock.bombard ? m_castleMap.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 144 + m_castleMap.castleCombat.TickValue / 3 % 8) : m_castleMap.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 152);
          PointF pointF1 = new PointF(28f, (float) (28 + height));
          child1.Center = pointF1;
          child1.PosX = (float) point2.X;
          child1.PosY = (float) point2.Y;
          this.backgroundSprite.AddChild(child1, 2);
          SpriteWrapper child2 = !rock.bombard ? m_castleMap.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 144 + m_castleMap.castleCombat.TickValue / 3 % 8) : m_castleMap.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 152);
          PointF pointF2 = new PointF(28f, 28f);
          child2.Center = pointF2;
          child2.PosX = (float) point2.X;
          child2.PosY = (float) point2.Y;
          child2.ColorToUse = Color.FromArgb(64, ARGBColors.Black);
          child2.Scale = 0.85f;
          this.backgroundSprite.AddChild(child2);
          if (!m_castleMap.castleCombat.Paused)
          {
            for (int index = rock.trail.Length - 1; index > 0; --index)
            {
              rock.trail[index].pos = rock.trail[index - 1].pos;
              rock.trail[index].height = rock.trail[index - 1].height;
              rock.trail[index].visible = rock.trail[index - 1].visible;
            }
            rock.trail[0].pos = new PointF((float) point2.X, (float) point2.Y);
            rock.trail[0].height = height;
            rock.trail[0].visible = true;
          }
        }
      }
    }

    public void drawCastleLoop(
      bool collapsed,
      ref bool completed,
      ref DateTime completeTime,
      DateTime curTime,
      CastleMap m_castleMap)
    {
      this.isCollapsed = collapsed;
      Random random = new Random();
      List<CastleElement> castleElementList = new List<CastleElement>();
      foreach (CastleElement element in m_castleMap.elements)
      {
        if (random.Next(2) == 0 || castleElementList.Count == 0)
          castleElementList.Add(element);
        else
          castleElementList.Insert(0, element);
      }
      m_castleMap.numSmelter = 0;
      foreach (CastleElement element in castleElementList)
      {
        int xPos = (int) element.xPos;
        int yPos = (int) element.yPos;
        Color white = ARGBColors.White;
        if (element.elementType < (byte) 69)
        {
          SpriteWrapper spriteWrapper = CastleMap.castleSpriteGrid[xPos, yPos];
          if (spriteWrapper != null && m_castleMap.debugDisplayMode <= 0)
          {
            int SpriteNo = this.initCastleSprite(spriteWrapper, (int) element.elementType, xPos, yPos, collapsed, element, m_castleMap);
            if (SpriteNo >= 0)
            {
              CastleMap.activeCastleInfrastructureElements[element.elementID] = (object) element;
              bool flag = false;
              int texID = GFXLibrary.Instance.CastleSpritesTexID;
              switch (element.elementType)
              {
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                  texID = GFXLibrary.Instance.FreeCardIconsID;
                  flag = true;
                  break;
              }
              if (!flag || !m_castleMap.attackerSetupForest || yPos >= 33)
              {
                spriteWrapper.reInitializeSpecial(texID, SpriteNo);
                if (spriteWrapper.Visible)
                  this.backgroundSprite.AddChild(spriteWrapper, 2);
                if (m_castleMap.displayType == 0)
                {
                  if (element.completionTime > curTime && !m_castleMap.InBuilderMode)
                    spriteWrapper.ColorToUse = Color.FromArgb(128, ARGBColors.White);
                }
                else if (m_castleMap.displayType != 1 && m_castleMap.displayType == 2 && element.completionTime > curTime)
                  spriteWrapper.Visible = false;
                if (element.completionTime > completeTime)
                {
                  completeTime = element.completionTime;
                  completed = false;
                }
                Color baseColor = spriteWrapper.ColorToUse;
                int num1 = (int) ((double) element.damage * 10.0);
                int num2 = CastleCombat.GetInfrastructureMaxDamage(GameEngine.Instance.LocalWorldData, (int) element.elementType, m_castleMap.getDefenderDefenceResearch(), m_castleMap.getLandType()) * 10;
                int num3 = 192 - num1 * 192 / num2;
                if (num3 < 0)
                  num3 = 0;
                if (num1 != 0)
                {
                  if (element.elementType != (byte) 36 || !m_castleMap.battleMode)
                  {
                    m_castleMap.castleDamaged = true;
                    baseColor = element.elementType == (byte) 35 ? Color.FromArgb((int) baseColor.A, num3, (int) byte.MaxValue, num3) : Color.FromArgb((int) baseColor.A, (int) byte.MaxValue, num3, num3);
                  }
                }
                else if (m_castleMap.InBuilderMode && element.elementID >= -1L)
                  baseColor = Color.FromArgb((int) byte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue);
                spriteWrapper.ColorToUse = baseColor;
                if (!m_castleMap.battleMode && element.elementID == m_castleMap.deletingHighlightElementID && (!m_castleMap.InBuilderMode || element.elementID < -1L))
                  spriteWrapper.ColorToUse = Color.FromArgb((int) sbyte.MaxValue, 32, 32, 32);
                if (m_castleMap.isDeletingThisElement(element.elementID))
                  spriteWrapper.ColorToUse = Color.FromArgb((int) sbyte.MaxValue, (int) byte.MaxValue, 32, 32);
                Rectangle sourceRect;
                PointF cornerOffset;
                SizeF realSize;
                switch (element.elementType)
                {
                  case 1:
                  case 2:
                  case 3:
                  case 4:
                  case 5:
                  case 6:
                  case 7:
                  case 8:
                  case 9:
                  case 10:
                    int num4 = (int) element.elementType;
                    if (CastleMap.FakeKeep >= 0)
                      num4 = CastleMap.FakeKeep;
                    if (CastleMap.displayCollapsed || m_castleMap.battleMode && CastleMap.AlwaysCollapsedWallsInBattles)
                    {
                      if (m_castleMap.displayType == 1)
                      {
                        int spriteID = 0;
                        if (m_castleMap.campMode == 0)
                          spriteID = 413 + num4 - 1;
                        else if (m_castleMap.campMode == 1)
                          spriteID = 449;
                        else if (m_castleMap.campMode == 2)
                          spriteID = 444;
                        SpriteWrapper nextExtraSprite = m_castleMap.getNextExtraSprite(spriteID);
                        PointF pointF = new PointF(96f, 0.0f);
                        int TagID = 1;
                        this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref TagID).GetSpriteXYdata(TagID, nextExtraSprite.SpriteNo, out sourceRect, out cornerOffset, out realSize);
                        pointF.Y = (float) (int) realSize.Height;
                        nextExtraSprite.Center = pointF;
                        nextExtraSprite.PosY = 40f;
                        nextExtraSprite.ColorToUse = Color.FromArgb(160, baseColor);
                        spriteWrapper.DrawChildrenWithParent = true;
                        spriteWrapper.AddChildAsLast(nextExtraSprite);
                        continue;
                      }
                      continue;
                    }
                    int spriteID1 = 0;
                    if (m_castleMap.campMode == 0)
                      spriteID1 = 258 + num4 - 1;
                    else if (m_castleMap.campMode == 1)
                      spriteID1 = 450;
                    else if (m_castleMap.campMode == 2)
                      spriteID1 = 445;
                    SpriteWrapper nextExtraSprite1 = m_castleMap.getNextExtraSprite(spriteID1);
                    PointF pointF1 = new PointF(96f, 0.0f);
                    int TagID1 = 1;
                    this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref TagID1).GetSpriteXYdata(TagID1, nextExtraSprite1.SpriteNo, out sourceRect, out cornerOffset, out realSize);
                    pointF1.Y = (float) (int) realSize.Height;
                    nextExtraSprite1.Center = pointF1;
                    nextExtraSprite1.PosY = 40f;
                    nextExtraSprite1.ColorToUse = Color.FromArgb(160, baseColor);
                    spriteWrapper.DrawChildrenWithParent = true;
                    spriteWrapper.AddChildAsLast(nextExtraSprite1);
                    continue;
                  case 11:
                    if (!CastleMap.displayCollapsed && (!m_castleMap.battleMode || !CastleMap.AlwaysCollapsedWallsInBattles) && (m_castleMap.displayType != 0 || element.completionTime <= curTime))
                    {
                      SpriteWrapper nextExtraSprite2 = m_castleMap.getNextExtraSprite(43);
                      nextExtraSprite2.Center = new PointF(32f, 42f);
                      nextExtraSprite2.PosY = -121f;
                      nextExtraSprite2.ColorToUse = baseColor;
                      spriteWrapper.DrawChildrenWithParent = true;
                      spriteWrapper.AddChildAsLast(nextExtraSprite2);
                      continue;
                    }
                    continue;
                  case 12:
                    if (!CastleMap.displayCollapsed && (!m_castleMap.battleMode || !CastleMap.AlwaysCollapsedWallsInBattles) && (m_castleMap.displayType != 0 || element.completionTime <= curTime))
                    {
                      SpriteWrapper nextExtraSprite3 = m_castleMap.getNextExtraSprite(44);
                      nextExtraSprite3.Center = new PointF(48f, 57f);
                      nextExtraSprite3.PosY = -120f;
                      nextExtraSprite3.ColorToUse = baseColor;
                      spriteWrapper.DrawChildrenWithParent = true;
                      spriteWrapper.AddChildAsLast(nextExtraSprite3);
                      continue;
                    }
                    continue;
                  case 13:
                    if (!CastleMap.displayCollapsed && (!m_castleMap.battleMode || !CastleMap.AlwaysCollapsedWallsInBattles) && (m_castleMap.displayType != 0 || element.completionTime <= curTime))
                    {
                      SpriteWrapper nextExtraSprite4 = m_castleMap.getNextExtraSprite(45);
                      nextExtraSprite4.Center = new PointF(64f, 66f);
                      nextExtraSprite4.PosY = -120f;
                      nextExtraSprite4.ColorToUse = baseColor;
                      spriteWrapper.DrawChildrenWithParent = true;
                      spriteWrapper.AddChildAsLast(nextExtraSprite4);
                      continue;
                    }
                    continue;
                  case 14:
                    if (!CastleMap.displayCollapsed && (!m_castleMap.battleMode || !CastleMap.AlwaysCollapsedWallsInBattles) && (m_castleMap.displayType != 0 || element.completionTime <= curTime))
                    {
                      SpriteWrapper nextExtraSprite5 = m_castleMap.getNextExtraSprite(46);
                      nextExtraSprite5.Center = new PointF(80f, 74f);
                      nextExtraSprite5.PosY = -124f;
                      nextExtraSprite5.ColorToUse = baseColor;
                      spriteWrapper.DrawChildrenWithParent = true;
                      spriteWrapper.AddChildAsLast(nextExtraSprite5);
                      continue;
                    }
                    continue;
                  case 31:
                    if (element.completionTime <= curTime)
                    {
                      ++m_castleMap.numGuardHouses;
                      continue;
                    }
                    continue;
                  case 32:
                    if (element.completionTime <= curTime)
                    {
                      ++m_castleMap.numSmelter;
                      continue;
                    }
                    continue;
                  case 37:
                  case 38:
                    if (m_castleMap.battleMode)
                    {
                      if (CastleMap.displayCollapsed || m_castleMap.battleMode && CastleMap.AlwaysCollapsedWallsInBattles)
                      {
                        SpriteWrapper child = element.elementType != (byte) 37 ? m_castleMap.getNextExtraSprite(428) : m_castleMap.getNextExtraSprite(432);
                        PointF pointF2 = new PointF(64f, 0.0f);
                        int TagID2 = 1;
                        this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref TagID2).GetSpriteXYdata(TagID2, child.SpriteNo, out sourceRect, out cornerOffset, out realSize);
                        pointF2.Y = (float) ((int) realSize.Height - 9);
                        child.Center = pointF2;
                        child.PosY = 24f;
                        child.ColorToUse = Color.FromArgb(160, baseColor);
                        spriteWrapper.DrawChildrenWithParent = true;
                        spriteWrapper.AddChildAsLast(child);
                        continue;
                      }
                      SpriteWrapper child1 = element.elementType != (byte) 37 ? m_castleMap.getNextExtraSprite(427) : m_castleMap.getNextExtraSprite(431);
                      PointF pointF3 = new PointF(64f, 0.0f);
                      int TagID3 = 1;
                      this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref TagID3).GetSpriteXYdata(TagID3, child1.SpriteNo, out sourceRect, out cornerOffset, out realSize);
                      pointF3.Y = (float) ((int) realSize.Height - 9);
                      child1.Center = pointF3;
                      child1.PosY = 24f;
                      child1.ColorToUse = Color.FromArgb(160, baseColor);
                      spriteWrapper.DrawChildrenWithParent = true;
                      spriteWrapper.AddChildAsLast(child1);
                      continue;
                    }
                    continue;
                  case 39:
                  case 40:
                    if (m_castleMap.battleMode)
                    {
                      if (CastleMap.displayCollapsed || m_castleMap.battleMode && CastleMap.AlwaysCollapsedWallsInBattles)
                      {
                        SpriteWrapper child = element.elementType != (byte) 39 ? m_castleMap.getNextExtraSprite(454) : m_castleMap.getNextExtraSprite(458);
                        PointF pointF4 = new PointF(64f, 0.0f);
                        int TagID4 = 1;
                        this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref TagID4).GetSpriteXYdata(TagID4, child.SpriteNo, out sourceRect, out cornerOffset, out realSize);
                        pointF4.Y = (float) ((int) realSize.Height - 9);
                        child.Center = pointF4;
                        child.PosY = 24f;
                        child.ColorToUse = Color.FromArgb(160, baseColor);
                        spriteWrapper.DrawChildrenWithParent = true;
                        spriteWrapper.AddChildAsLast(child);
                        continue;
                      }
                      SpriteWrapper child2 = element.elementType != (byte) 39 ? m_castleMap.getNextExtraSprite(453) : m_castleMap.getNextExtraSprite(457);
                      PointF pointF5 = new PointF(64f, 0.0f);
                      int TagID5 = 1;
                      this.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref TagID5).GetSpriteXYdata(TagID5, child2.SpriteNo, out sourceRect, out cornerOffset, out realSize);
                      pointF5.Y = (float) ((int) realSize.Height - 9);
                      child2.Center = pointF5;
                      child2.PosY = 24f;
                      child2.ColorToUse = Color.FromArgb(160, baseColor);
                      spriteWrapper.DrawChildrenWithParent = true;
                      spriteWrapper.AddChildAsLast(child2);
                      continue;
                    }
                    continue;
                  case 42:
                    SpriteWrapper nextExtraSprite6;
                    if (m_castleMap.battleMode)
                    {
                      BattleBuilding battleBuilding = (BattleBuilding) element;
                      int spriteNo = (battleBuilding.facing + 6 & 7) + battleBuilding.animFrame * 8;
                      nextExtraSprite6 = m_castleMap.getNextExtraSprite(GFXLibrary.Instance.BallistaTexID, spriteNo);
                    }
                    else
                      nextExtraSprite6 = m_castleMap.getNextExtraSprite(GFXLibrary.Instance.BallistaTexID, 0);
                    nextExtraSprite6.PosY = !collapsed ? -100f : -16f;
                    PointF pointF6 = new PointF(65f, 65f);
                    int TagID6 = 1;
                    this.gfx.getSpriteLoader(GFXLibrary.Instance.BallistaTexID, ref TagID6).GetSpriteXYdata(TagID6, nextExtraSprite6.SpriteNo, out sourceRect, out cornerOffset, out realSize);
                    nextExtraSprite6.Center = pointF6;
                    nextExtraSprite6.ColorToUse = baseColor;
                    spriteWrapper.DrawChildrenWithParent = true;
                    spriteWrapper.AddChild(nextExtraSprite6);
                    continue;
                  default:
                    continue;
                }
              }
            }
          }
        }
      }
    }

    public void doFireList(CastleMap m_castleMap)
    {
      foreach (BattleFire fire in m_castleMap.castleCombat.getFireList())
      {
        Point point = CastleMap.castleUnitSpritePoint[fire.xPos, fire.yPos];
        int num = CastleMap.displayCollapsed || m_castleMap.battleMode && CastleMap.AlwaysCollapsedWallsInBattles ? (int) m_castleMap.castleLayout.collapsedHeightMap[fire.xPos, fire.yPos] : (int) m_castleMap.castleLayout.fullHeightMap[fire.xPos, fire.yPos];
        int spriteNo = 0;
        if (fire.state == 0)
          spriteNo = this.fireStart[fire.animFrame];
        else if (fire.state == 1)
          spriteNo = 7 + this.fireLoop[(fire.animFrame + fire.randValue) % this.fireLoop.Length];
        else if (fire.state == 2)
          spriteNo = 41 + this.fireEnd[fire.animFrame];
        SpriteWrapper nextExtraSprite = m_castleMap.getNextExtraSprite(GFXLibrary.Instance.FireTexID, spriteNo);
        PointF pointF = new PointF(50f, (float) (83 + num));
        nextExtraSprite.Center = pointF;
        nextExtraSprite.PosX = (float) point.X;
        nextExtraSprite.PosY = (float) point.Y;
        this.backgroundSprite.AddChild(nextExtraSprite, 2);
      }
    }

    public void drawTroops(CastleMap m_castleMap)
    {
      foreach (CastleElement element in m_castleMap.elements)
      {
        if (element.elementType >= (byte) 69)
        {
          int xPos = (int) element.xPos;
          int yPos = (int) element.yPos;
          int texID = -1;
          int textureID = -1;
          int num1 = (int) element.elementType;
          bool flag1 = element.reinforcement || element.vassalReinforcements;
          bool flag2 = element.elementType < (byte) 90;
          switch (element.elementType)
          {
            case 70:
              texID = element.reinforcement || element.vassalReinforcements ? GFXLibrary.Instance.PeasantGreenAnimTexID : GFXLibrary.Instance.PeasantAnimTexID;
              break;
            case 71:
              texID = element.reinforcement || element.vassalReinforcements ? GFXLibrary.Instance.SwordsmanGreenAnimTexID : GFXLibrary.Instance.SwordsmanAnimTexID;
              break;
            case 72:
              texID = element.reinforcement || element.vassalReinforcements ? GFXLibrary.Instance.ArcherGreenAnimTexID : GFXLibrary.Instance.ArcherAnimTexID;
              break;
            case 73:
              texID = element.reinforcement || element.vassalReinforcements ? GFXLibrary.Instance.PikemanGreenAnimTexID : GFXLibrary.Instance.PikemanAnimTexID;
              break;
            case 75:
              texID = GFXLibrary.Instance.CastleSpritesTexID;
              ++m_castleMap.numPots;
              break;
            case 77:
              texID = GFXLibrary.Instance.WolfAnimTexID;
              break;
            case 78:
              texID = GFXLibrary.Instance.KnightAnimTexID;
              textureID = GFXLibrary.Instance.KnightTopAnimTexID;
              break;
            case 85:
              texID = GFXLibrary.Instance.CaptainAnimTexID;
              break;
            case 90:
              texID = GFXLibrary.Instance.PeasantRedAnimTexID;
              break;
            case 91:
              texID = GFXLibrary.Instance.SwordsmanRedAnimTexID;
              break;
            case 92:
              texID = GFXLibrary.Instance.ArcherRedAnimTexID;
              break;
            case 93:
              texID = GFXLibrary.Instance.PikemanRedAnimTexID;
              break;
            case 94:
              texID = GFXLibrary.Instance.CatapultAnimTexID;
              break;
            case 100:
            case 101:
            case 102:
            case 103:
            case 104:
            case 105:
            case 106:
            case 107:
              texID = GFXLibrary.Instance.CaptainAnimRedTexID;
              break;
          }
          if (texID >= 0)
          {
            BattleTroop battleTroop = (BattleTroop) null;
            if (m_castleMap.battleMode)
              battleTroop = (BattleTroop) element;
            PointF pointF = new PointF(18f, 28f);
            int spriteNo1 = 0;
            int SpriteNo;
            if (element.elementType == (byte) 75)
            {
              SpriteNo = 396;
              if (m_castleMap.battleMode && battleTroop.pouring)
              {
                texID = GFXLibrary.Instance.OilPotAnimTexID;
                SpriteNo = (battleTroop.facing + 6 & 7) + battleTroop.animFrame * 8;
                pointF = new PointF(48f, 54f);
              }
            }
            else if (!m_castleMap.battleMode)
            {
              SpriteNo = xPos >= yPos ? (117 - xPos >= yPos ? (!flag2 ? 2 : 6) : (!flag2 ? 4 : 0)) : (117 - xPos >= yPos ? (!flag2 ? 0 : 4) : (!flag2 ? 6 : 2));
            }
            else
            {
              SpriteNo = battleTroop.facing + 6 & 7;
              spriteNo1 = SpriteNo;
              if (battleTroop.moving)
              {
                spriteNo1 = (SpriteNo += (battleTroop.animFrame + battleTroop.walkAnimOffset) % 16 * 8);
                if (battleTroop.pillageCarry)
                {
                  switch (battleTroop.elementType)
                  {
                    case 90:
                      texID = GFXLibrary.Instance.PeasantCarryAnimTexID;
                      break;
                    case 91:
                      texID = GFXLibrary.Instance.SwordsmanCarryAnimTexID;
                      break;
                    case 92:
                      texID = GFXLibrary.Instance.ArcherCarryAnimTexID;
                      break;
                    case 93:
                      texID = GFXLibrary.Instance.PikemanCarryAnimTexID;
                      break;
                  }
                  num1 = 0;
                }
              }
              else if (battleTroop.attackingEnemy)
              {
                if (battleTroop.blockedClock > 0)
                {
                  switch (battleTroop.elementType)
                  {
                    case 70:
                      SpriteNo += 208 + this.peasantBlocked[Math.Min(battleTroop.animFrame, this.peasantBlocked.Length - 1)] * 8;
                      texID = GFXLibrary.Instance.Peasant2AnimTexID;
                      break;
                    case 71:
                    case 91:
                      SpriteNo += 424 + this.swordsmanBlocked[battleTroop.animFrame % this.swordsmanBlocked.Length] * 8;
                      break;
                    case 72:
                    case 92:
                      SpriteNo += 616 + this.archerBlocked[battleTroop.animFrame % this.archerBlocked.Length] * 8;
                      break;
                    case 73:
                    case 93:
                      SpriteNo += 448 + this.pikemanBlocked[battleTroop.animFrame % this.pikemanBlocked.Length] * 8;
                      break;
                    case 90:
                      SpriteNo += 208 + this.peasantBlocked[Math.Min(battleTroop.animFrame, this.peasantBlocked.Length - 1)] * 8;
                      texID = GFXLibrary.Instance.Peasant2RedAnimTexID;
                      break;
                  }
                }
                else
                {
                  switch (battleTroop.elementType)
                  {
                    case 70:
                      SpriteNo += this.peasantAttack[battleTroop.animFrame % this.peasantAttack.Length] * 8;
                      texID = GFXLibrary.Instance.Peasant2AnimTexID;
                      break;
                    case 71:
                    case 91:
                      SpriteNo += 128 + this.swordsmanAttackUnit[battleTroop.animFrame % this.swordsmanAttackUnit.Length] * 8;
                      break;
                    case 72:
                    case 92:
                      SpriteNo += 520 + this.archerAttackUnit[battleTroop.animFrame % this.archerAttackUnit.Length] * 8;
                      break;
                    case 73:
                    case 93:
                      SpriteNo += 264 + this.pikemanAttackJab[battleTroop.animFrame % this.pikemanAttackJab.Length] * 8;
                      break;
                    case 77:
                      SpriteNo += 128 + this.wolfAttackUnit[battleTroop.animFrame % this.wolfAttackUnit.Length] * 8;
                      break;
                    case 78:
                      SpriteNo += 128;
                      spriteNo1 += 128 + this.knightAttackUnit[battleTroop.animFrame % this.knightAttackUnit.Length] * 8;
                      break;
                    case 85:
                    case 100:
                    case 101:
                    case 102:
                    case 103:
                    case 104:
                    case 105:
                    case 106:
                    case 107:
                      SpriteNo += 287 + this.captainAttackUnit[battleTroop.animFrame % this.captainAttackUnit.Length] * 8;
                      break;
                    case 90:
                      SpriteNo += this.peasantAttack[battleTroop.animFrame % this.peasantAttack.Length] * 8;
                      texID = GFXLibrary.Instance.Peasant2RedAnimTexID;
                      break;
                  }
                }
              }
              else if (battleTroop.attackingMoat)
              {
                switch (battleTroop.elementType)
                {
                  case 70:
                    SpriteNo += 288 + this.peasantAttackMoat[battleTroop.animFrame % this.peasantAttackMoat.Length] * 8;
                    texID = GFXLibrary.Instance.Peasant2AnimTexID;
                    break;
                  case 71:
                  case 91:
                    SpriteNo += 456 + this.swordsmanAttackMoat[battleTroop.animFrame % this.swordsmanAttackMoat.Length] * 8;
                    break;
                  case 72:
                    SpriteNo += this.archerAttackMoat[battleTroop.animFrame % this.archerAttackMoat.Length] * 8;
                    texID = GFXLibrary.Instance.Archer2AnimTexID;
                    break;
                  case 73:
                  case 93:
                    SpriteNo += 328 + this.pikemanAttackMoat[battleTroop.animFrame % this.pikemanAttackMoat.Length] * 8;
                    break;
                  case 85:
                  case 100:
                  case 101:
                  case 102:
                  case 103:
                  case 104:
                  case 105:
                  case 106:
                  case 107:
                    SpriteNo += 287 + this.captainAttackMoat[battleTroop.animFrame % this.captainAttackMoat.Length] * 8;
                    break;
                  case 90:
                    SpriteNo += 288 + this.peasantAttackMoat[battleTroop.animFrame % this.peasantAttackMoat.Length] * 8;
                    texID = GFXLibrary.Instance.Peasant2RedAnimTexID;
                    break;
                  case 92:
                    SpriteNo += this.archerAttackMoat[battleTroop.animFrame % this.archerAttackMoat.Length] * 8;
                    texID = GFXLibrary.Instance.Archer2RedAnimTexID;
                    break;
                }
              }
              else if (battleTroop.attackingIntrastructure)
              {
                switch (battleTroop.elementType)
                {
                  case 70:
                    SpriteNo += this.peasantAttack[battleTroop.animFrame % this.peasantAttack.Length] * 8;
                    texID = GFXLibrary.Instance.Peasant2AnimTexID;
                    break;
                  case 71:
                  case 91:
                    SpriteNo += 128 + this.swordsmanAttackWall[battleTroop.animFrame % this.swordsmanAttackWall.Length] * 8;
                    break;
                  case 72:
                  case 92:
                    SpriteNo += 520 + this.archerAttackWall[battleTroop.animFrame % this.archerAttackWall.Length] * 8;
                    break;
                  case 73:
                  case 93:
                    SpriteNo += 264 + this.pikemanAttackChop[battleTroop.animFrame % this.pikemanAttackChop.Length] * 8;
                    break;
                  case 85:
                  case 100:
                  case 101:
                  case 102:
                  case 103:
                  case 104:
                  case 105:
                  case 106:
                  case 107:
                    SpriteNo += 287 + this.captainAttackWall[battleTroop.animFrame % this.captainAttackWall.Length] * 8;
                    break;
                  case 90:
                    SpriteNo += this.peasantAttack[battleTroop.animFrame % this.peasantAttack.Length] * 8;
                    texID = GFXLibrary.Instance.Peasant2RedAnimTexID;
                    break;
                }
              }
              else if (battleTroop.firingRock)
              {
                if (battleTroop.animFrame < this.catapultAnim.Length)
                  SpriteNo += this.catapultAnim[battleTroop.animFrame % this.catapultAnim.Length] * 8;
              }
              else if (battleTroop.shootingArrow)
              {
                if (CastleMap.displayCollapsed || m_castleMap.battleMode && CastleMap.AlwaysCollapsedWallsInBattles)
                {
                  if (battleTroop.animFrame < this.archerAttackFiringStraight.Length)
                    SpriteNo += 136 + this.archerAttackFiringStraight[battleTroop.animFrame % this.archerAttackFiringStraight.Length] * 8;
                  else
                    SpriteNo += 640;
                }
                else
                {
                  if (battleTroop.arrowTilt < 0)
                  {
                    int fullHeight1 = (int) m_castleMap.castleLayout.fullHeightMap[battleTroop.arrow.startX, battleTroop.arrow.startY];
                    int fullHeight2 = (int) m_castleMap.castleLayout.fullHeightMap[battleTroop.arrow.targetX, battleTroop.arrow.targetY];
                    battleTroop.arrowTilt = this.getArrowTiltSpriteID(battleTroop.arrow, fullHeight1, fullHeight2);
                  }
                  if (battleTroop.arrowTilt < 3)
                  {
                    if (battleTroop.animFrame < this.archerAttackFiringDown.Length)
                      SpriteNo += 136 + this.archerAttackFiringDown[battleTroop.animFrame % this.archerAttackFiringDown.Length] * 8;
                    else
                      SpriteNo += 640;
                  }
                  else if (battleTroop.arrowTilt < 6)
                  {
                    if (battleTroop.animFrame < this.archerAttackFiringStraight.Length)
                      SpriteNo += 136 + this.archerAttackFiringStraight[battleTroop.animFrame % this.archerAttackFiringStraight.Length] * 8;
                    else
                      SpriteNo += 640;
                  }
                  else if (battleTroop.animFrame < this.archerAttackFiringUp.Length)
                    SpriteNo += 136 + this.archerAttackFiringUp[battleTroop.animFrame % this.archerAttackFiringUp.Length] * 8;
                  else
                    SpriteNo += 640;
                }
              }
              else if (battleTroop.pillageCarry)
              {
                switch (battleTroop.elementType)
                {
                  case 90:
                    texID = GFXLibrary.Instance.PeasantCarryAnimTexID;
                    break;
                  case 91:
                    texID = GFXLibrary.Instance.SwordsmanCarryAnimTexID;
                    break;
                  case 92:
                    texID = GFXLibrary.Instance.ArcherCarryAnimTexID;
                    break;
                  case 93:
                    texID = GFXLibrary.Instance.PikemanCarryAnimTexID;
                    break;
                }
              }
              else
              {
                switch (battleTroop.elementType)
                {
                  case 70:
                    SpriteNo += 128 + this.peasantIdle[battleTroop.animFrame % this.peasantIdle.Length] * 8;
                    texID = GFXLibrary.Instance.Peasant2AnimTexID;
                    break;
                  case 71:
                  case 91:
                    SpriteNo += 448;
                    break;
                  case 72:
                  case 92:
                    SpriteNo += 640;
                    break;
                  case 73:
                    if (battleTroop.blockClock >= GameEngine.Instance.LocalWorldData.Castle_Pikeman_BlockRechargeTime)
                    {
                      SpriteNo += 264;
                      break;
                    }
                    SpriteNo += 128 + this.pikemanIdle[battleTroop.animFrame % this.pikemanIdle.Length] * 8;
                    break;
                  case 77:
                    SpriteNo += 424;
                    break;
                  case 78:
                    SpriteNo += 128 + this.knightHorseIdle[battleTroop.animFrame % this.knightHorseIdle.Length] * 8;
                    spriteNo1 = spriteNo1;
                    break;
                  case 85:
                  case 100:
                  case 101:
                  case 102:
                  case 103:
                  case 104:
                  case 105:
                  case 106:
                  case 107:
                    SpriteNo += 128 + (this.captainIdle[battleTroop.animFrame / 2 % this.captainIdle.Length] - 1) * 8;
                    break;
                  case 90:
                    SpriteNo += 128 + this.peasantIdle[battleTroop.animFrame % this.peasantIdle.Length] * 8;
                    texID = GFXLibrary.Instance.Peasant2RedAnimTexID;
                    break;
                  case 93:
                    SpriteNo += 128 + this.pikemanIdle[battleTroop.animFrame % this.pikemanIdle.Length] * 8;
                    break;
                }
              }
            }
            SpriteWrapper child = CastleMap.castleDefenderSpriteGrid[xPos, yPos] ?? CastleMap.castleAttackerSpriteGrid[xPos, yPos];
            child.TroopType = num1;
            child.GreenTroop = flag1;
            int num2 = 1;
            if (textureID >= 0)
              num2 = 2;
            SpriteWrapper spriteWrapper1 = (SpriteWrapper) null;
            for (int index = 0; index < num2; ++index)
            {
              if (textureID >= 0 && index == 1)
              {
                child = m_castleMap.getNextExtraSprite(textureID, spriteNo1);
                child.TroopType = 0;
                texID = textureID;
                SpriteNo = spriteNo1;
              }
              else
                child.Initialize(this.gfx, texID, SpriteNo);
              Point pos = CastleMap.castleUnitSpritePoint[xPos, yPos];
              if (m_castleMap.battleMode && battleTroop.moving)
              {
                Point point = CastleMap.castleUnitSpritePoint[battleTroop.otherX, battleTroop.otherY];
                float moveRatio = battleTroop.getMoveRatio();
                pos.X = (int) ((double) (pos.X - point.X) * (double) moveRatio + (double) point.X);
                pos.Y = (int) ((double) (pos.Y - point.Y) * (double) moveRatio + (double) point.Y);
              }
              int num3 = CastleMap.displayCollapsed || m_castleMap.battleMode && CastleMap.AlwaysCollapsedWallsInBattles ? (int) m_castleMap.castleLayout.collapsedHeightMap[xPos, yPos] : (int) m_castleMap.castleLayout.fullHeightMap[xPos, yPos];
              pos.Y -= num3;
              child.PosX = (float) pos.X;
              child.PosY = (float) pos.Y;
              child.Visible = true;
              child.Center = element.elementType != (byte) 75 ? (element.elementType != (byte) 94 ? (element.elementType != (byte) 78 ? (element.elementType >= (byte) 85 && element.elementType <= (byte) 89 || element.elementType >= (byte) 100 && element.elementType <= (byte) 109 ? new PointF(65f, 82f) : new PointF(50f, 66f)) : new PointF(75f, 100f)) : new PointF(93f, 100f)) : pointF;
              m_castleMap.getNextClickArea().addUnit(pos, element.elementID);
              child.ColorToUse = element.elementID != m_castleMap.troopMovingElemID || !m_castleMap.troopMovingMode ? (element.elementID != m_castleMap.selectedCatapult || m_castleMap.selectedCatapult == -1L ? (element.elementID != m_castleMap.troopSelected || m_castleMap.troopSelected == -1L ? (!m_castleMap.m_lassoLeftHeldDown ? (!m_castleMap.m_lassoMade ? ARGBColors.White : (m_castleMap.m_lassoElements.Contains(element.elementID) ? Color.FromArgb((int) byte.MaxValue, this.pulseValue, this.pulseValue, this.pulseValue) : Color.FromArgb(128, ARGBColors.White))) : (m_castleMap.m_lassoElements.Contains(element.elementID) ? Color.FromArgb((int) byte.MaxValue, this.pulseValue, this.pulseValue, this.pulseValue) : Color.FromArgb(160, ARGBColors.White))) : Color.FromArgb(192, ARGBColors.Red)) : Color.FromArgb(192, ARGBColors.Red)) : Color.FromArgb(128, ARGBColors.White);
              if (!m_castleMap.battleMode && element.elementID == m_castleMap.deletingHighlightElementID)
                child.ColorToUse = Color.FromArgb((int) sbyte.MaxValue, 32, 32, 32);
              if (CastleMap.castleSpriteGrid[xPos, yPos] != null)
              {
                if (CastleMap.castleSpriteGrid[xPos, yPos].Visible)
                {
                  SpriteWrapper spriteWrapper2 = CastleMap.castleSpriteGrid[xPos, yPos];
                  child.PosX -= spriteWrapper2.PosX;
                  child.PosY -= spriteWrapper2.PosY;
                  spriteWrapper2.DrawChildrenWithParent = true;
                  spriteWrapper2.AddChild(child);
                }
                else
                {
                  long elem = m_castleMap.castleLayout.elemMap[xPos, yPos];
                  if (elem != -2L || elem == -1L)
                  {
                    CastleElement infrastructureElement = (CastleElement) CastleMap.activeCastleInfrastructureElements[elem];
                    if (infrastructureElement != null)
                    {
                      SpriteWrapper spriteWrapper3 = CastleMap.castleSpriteGrid[(int) infrastructureElement.xPos, (int) infrastructureElement.yPos];
                      if (spriteWrapper3.Visible)
                      {
                        child.PosX -= spriteWrapper3.PosX;
                        child.PosY -= spriteWrapper3.PosY;
                        spriteWrapper3.DrawChildrenWithParent = true;
                        spriteWrapper3.AddChild(child);
                      }
                      else
                        this.backgroundSprite.AddChild(child, 2);
                    }
                    else
                      this.backgroundSprite.AddChild(child, 2);
                  }
                  else if (spriteWrapper1 != null)
                  {
                    child.PosX -= spriteWrapper1.PosX;
                    child.PosY -= spriteWrapper1.PosY;
                    spriteWrapper1.DrawChildrenWithParent = true;
                    spriteWrapper1.AddChild(child, 2);
                  }
                  else
                    this.backgroundSprite.AddChild(child, 2 + index);
                }
              }
              else if (spriteWrapper1 != null)
              {
                child.PosX -= spriteWrapper1.PosX;
                child.PosY -= spriteWrapper1.PosY;
                spriteWrapper1.DrawChildrenWithParent = true;
                spriteWrapper1.AddChild(child, 2);
              }
              else
                this.backgroundSprite.AddChild(child, 2 + index);
              if (m_castleMap.battleMode && m_castleMap.battleModeMousePos.X != 1000 && battleTroop.elementType != (byte) 75 && (m_castleMap.battleModeMousePos.X - pos.X) * (m_castleMap.battleModeMousePos.X - pos.X) + (m_castleMap.battleModeMousePos.Y - (pos.Y - 15)) * (m_castleMap.battleModeMousePos.Y - (pos.Y - 15)) < 22500)
              {
                int damage = (int) battleTroop.damage;
                int unitMaxDamage = m_castleMap.castleCombat.getUnitMaxDamage((int) battleTroop.elementType);
                int maxDamageNumLevels = m_castleMap.castleCombat.getUnitMaxDamageNumLevels((int) battleTroop.elementType);
                int num4 = damage * maxDamageNumLevels / unitMaxDamage;
                if (num4 >= maxDamageNumLevels)
                  num4 = maxDamageNumLevels - 1;
                int num5;
                switch (maxDamageNumLevels)
                {
                  case 12:
                    num5 = 11;
                    break;
                  case 13:
                    num5 = 23;
                    break;
                  case 14:
                    num5 = 36;
                    break;
                  case 15:
                    num5 = 50;
                    break;
                  case 16:
                    num5 = 65;
                    break;
                  case 17:
                    num5 = 81;
                    break;
                  case 18:
                    num5 = 98;
                    break;
                  case 19:
                    num5 = 116;
                    break;
                  case 20:
                    num5 = 135;
                    break;
                  case 21:
                    num5 = 155;
                    break;
                  default:
                    num5 = 0;
                    break;
                }
                SpriteWrapper nextExtraSprite = m_castleMap.getNextExtraSprite(GFXLibrary.Instance.HpsBarsTexID, num5 + (maxDamageNumLevels - 1) - num4);
                nextExtraSprite.Center = new PointF(11f, 2f);
                nextExtraSprite.PosX = 0.0f;
                nextExtraSprite.PosY = -40f;
                child.DrawChildrenWithParent = true;
                child.AddChild(nextExtraSprite);
              }
              if (m_castleMap.battleMode && battleTroop.captainsBonusDamageClock > 0)
              {
                int num6 = 900 - battleTroop.captainsBonusDamageClock;
                if (num6 >= 22)
                {
                  if (num6 > 878)
                  {
                    num6 = 900 - num6;
                  }
                  else
                  {
                    int num7 = (num6 - 22) % 44;
                    if (num7 >= 23)
                      num7 = 45 - num7;
                    num6 = num7 + 22;
                  }
                }
                if (num6 < 0)
                  num6 = 0;
                else if (num6 >= 45)
                  num6 = 44;
                int spriteNo2 = num6 + 45;
                SpriteWrapper nextExtraSprite = m_castleMap.getNextExtraSprite(GFXLibrary.Instance.ArmyAnimsTexID, spriteNo2);
                nextExtraSprite.Center = new PointF(30f, 32f);
                nextExtraSprite.PosX = 0.0f;
                nextExtraSprite.PosY = -40f;
                child.DrawChildrenWithParent = true;
                child.AddChild(nextExtraSprite);
              }
              if (m_castleMap.battleMode && battleTroop.captainsHealAnimClock > 0)
              {
                int spriteNo3 = (450 - battleTroop.captainsHealAnimClock) % 45;
                if (spriteNo3 < 0)
                  spriteNo3 = 0;
                else if (spriteNo3 >= 45)
                  spriteNo3 = 44;
                SpriteWrapper nextExtraSprite = m_castleMap.getNextExtraSprite(GFXLibrary.Instance.ArmyAnimsTexID, spriteNo3);
                nextExtraSprite.Center = new PointF(35f, 32f);
                nextExtraSprite.PosX = 0.0f;
                nextExtraSprite.PosY = -50f;
                child.DrawChildrenWithParent = true;
                child.AddChild(nextExtraSprite);
              }
              spriteWrapper1 = child;
            }
          }
          switch (element.elementType)
          {
            case 70:
              if (!element.reinforcement && !element.vassalReinforcements)
              {
                ++m_castleMap.numPlacedDefenderPeasants;
                continue;
              }
              if (element.reinforcement)
              {
                ++m_castleMap.numPlacedReinforceDefenderPeasants;
                continue;
              }
              ++m_castleMap.numPlacedVassalReinforceDefenderPeasants;
              continue;
            case 71:
              if (!element.reinforcement && !element.vassalReinforcements)
              {
                ++m_castleMap.numPlacedDefenderSwordsmen;
                continue;
              }
              if (element.reinforcement)
              {
                ++m_castleMap.numPlacedReinforceDefenderSwordsmen;
                continue;
              }
              ++m_castleMap.numPlacedVassalReinforceDefenderSwordsmen;
              continue;
            case 72:
              if (!element.reinforcement && !element.vassalReinforcements)
              {
                ++m_castleMap.numPlacedDefenderArchers;
                continue;
              }
              if (element.reinforcement)
              {
                ++m_castleMap.numPlacedReinforceDefenderArchers;
                continue;
              }
              ++m_castleMap.numPlacedVassalReinforceDefenderArchers;
              continue;
            case 73:
              if (!element.reinforcement && !element.vassalReinforcements)
              {
                ++m_castleMap.numPlacedDefenderPikemen;
                continue;
              }
              if (element.reinforcement)
              {
                ++m_castleMap.numPlacedReinforceDefenderPikemen;
                continue;
              }
              ++m_castleMap.numPlacedVassalReinforceDefenderPikemen;
              continue;
            case 77:
              ++m_castleMap.numPlacedReinforceDefenderSwordsmen;
              continue;
            case 85:
              ++m_castleMap.numPlacedDefenderCaptains;
              continue;
            case 90:
              ++m_castleMap.attackNumPeasants;
              continue;
            case 91:
              ++m_castleMap.attackNumSwordsmen;
              continue;
            case 92:
              ++m_castleMap.attackNumArchers;
              continue;
            case 93:
              ++m_castleMap.attackNumPikemen;
              continue;
            case 94:
              ++m_castleMap.attackNumCatapults;
              continue;
            case 100:
            case 101:
            case 102:
            case 103:
            case 104:
            case 105:
            case 106:
            case 107:
              ++m_castleMap.attackNumCaptains;
              continue;
            default:
              continue;
          }
        }
      }
    }

    public int initCastleSprite(
      SpriteWrapper sprite,
      int elementType,
      int gx,
      int gy,
      bool collapsed,
      CastleElement element,
      CastleMap m_castleMap)
    {
      int textureID = GFXLibrary.Instance.CastleSpritesTexID;
      sprite.Visible = true;
      sprite.ColorToUse = ARGBColors.White;
      PointF pointF = new PointF(16f, 0.0f);
      float num1 = 8f;
      int spriteTagOfset = 0;
      switch (elementType)
      {
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
        case 8:
        case 9:
        case 10:
          pointF.X = 96f;
          num1 = 40f;
          if (collapsed)
          {
            if (m_castleMap.campMode == 0)
            {
              spriteTagOfset = 224;
              if (m_castleMap.displayType == 1)
                spriteTagOfset = 234;
            }
            else if (m_castleMap.campMode == 1)
            {
              spriteTagOfset = 446;
              if (m_castleMap.displayType == 1)
                spriteTagOfset = 447;
            }
            else if (m_castleMap.campMode == 2)
            {
              spriteTagOfset = 441;
              if (m_castleMap.displayType == 1)
                spriteTagOfset = 442;
            }
          }
          else if (m_castleMap.campMode == 0)
            spriteTagOfset = 244;
          else if (m_castleMap.campMode == 1)
            spriteTagOfset = 448;
          else if (m_castleMap.campMode == 2)
            spriteTagOfset = 443;
          if (m_castleMap.campMode == 0)
          {
            if (CastleMap.FakeKeep >= 0)
            {
              spriteTagOfset += CastleMap.FakeKeep - 1;
              break;
            }
            spriteTagOfset += elementType - 1;
            break;
          }
          break;
        case 11:
          pointF.X = 32f;
          spriteTagOfset = !collapsed ? 218 : 214;
          break;
        case 12:
          pointF.X = 48f;
          num1 = 24f;
          spriteTagOfset = !collapsed ? 219 : 215;
          break;
        case 13:
          pointF.X = 64f;
          num1 = 24f;
          spriteTagOfset = !collapsed ? 220 : 216;
          break;
        case 14:
          pointF.X = 80f;
          num1 = 40f;
          spriteTagOfset = !collapsed ? 221 : 217;
          break;
        case 21:
          pointF.X = 32f;
          spriteTagOfset = !collapsed ? 223 : 222;
          break;
        case 31:
          pointF.X = 101f;
          int defenderDefenceResearch = m_castleMap.getDefenderDefenceResearch();
          if (defenderDefenceResearch < 4)
          {
            spriteTagOfset = !collapsed ? 433 : 434;
            num1 = 95f;
            break;
          }
          if (defenderDefenceResearch < 8)
          {
            spriteTagOfset = !collapsed ? 435 : 436;
            num1 = 74f;
            break;
          }
          if (defenderDefenceResearch < 10)
          {
            spriteTagOfset = !collapsed ? 437 : 438;
            num1 = 74f;
            break;
          }
          spriteTagOfset = !collapsed ? 439 : 440;
          num1 = 74f;
          break;
        case 32:
          pointF.X = 64f;
          num1 = 24f;
          spriteTagOfset = !collapsed ? 210 : 211;
          break;
        case 33:
          int num2 = (gx + gy) % 8;
          spriteTagOfset = !collapsed ? num2 + 154 : num2 + 162;
          break;
        case 34:
          if (collapsed)
          {
            spriteTagOfset = (gx + gy) % 8 + 35;
            break;
          }
          spriteTagOfset = !m_castleMap.isInNorthSouthWall(gx, gy) ? (!m_castleMap.isInEastWestWall(gx, gy) ? (!m_castleMap.isSouthEndWall(gx, gy) ? (!m_castleMap.isEastEndWall(gx, gy) ? 34 : 32) : 33) : ((gx & 15) != 15 ? 31 - (gx & 15) : 0)) : 16 - (gy & 15);
          break;
        case 35:
          if (gx < 33 || gy < 33 || gx >= 85 || gy >= 85)
          {
            spriteTagOfset = 276;
            break;
          }
          spriteTagOfset = 276;
          int num3 = 0;
          for (; this.moatSurroundLogic[num3 * 9] > 0; ++num3)
          {
            bool flag = false;
            for (int index = 0; index < 8; ++index)
            {
              int num4 = (int) m_castleMap.castleLayout.map[gx + this.moatSurroundTests[index * 2], gy + this.moatSurroundTests[index * 2 + 1]];
              int num5 = this.moatSurroundLogic[num3 * 9 + 1 + index];
              if (num5 != 2)
              {
                if (num4 == 35)
                {
                  if (num5 == 0)
                  {
                    flag = true;
                    break;
                  }
                }
                else if (num5 == 1)
                {
                  flag = true;
                  break;
                }
              }
            }
            if (!flag)
            {
              spriteTagOfset = this.moatSurroundLogic[num3 * 9];
              int num6 = gx ^ gy ^ gx / 10 ^ gy / 20;
              switch (spriteTagOfset)
              {
                case 272:
                  if (num6 % 3 != 2)
                  {
                    spriteTagOfset = 397 + num6 % 3;
                    goto label_95;
                  }
                  else
                    goto label_95;
                case 273:
                  if (num6 % 3 != 2)
                  {
                    spriteTagOfset = 399 + num6 % 3;
                    goto label_95;
                  }
                  else
                    goto label_95;
                case 274:
                  if (num6 % 3 != 2)
                  {
                    spriteTagOfset = 401 + num6 % 3;
                    goto label_95;
                  }
                  else
                    goto label_95;
                case 275:
                  if (num6 % 3 != 2)
                  {
                    spriteTagOfset = 403 + num6 % 3;
                    goto label_95;
                  }
                  else
                    goto label_95;
                case 276:
                  if (num6 % 9 != 8)
                  {
                    spriteTagOfset = 405 + num6 % 9;
                    goto label_95;
                  }
                  else
                    goto label_95;
                default:
                  goto label_95;
              }
            }
          }
          break;
        case 36:
          int num7 = (gx ^ gy) % 8;
          spriteTagOfset = 388 + num7;
          if (element != null && m_castleMap.battleMode)
          {
            BattleBuilding battleBuilding = (BattleBuilding) element;
            if (battleBuilding.visible)
            {
              if (battleBuilding.openPit)
                spriteTagOfset -= 8;
              if (battleBuilding.animating)
              {
                int num8 = battleBuilding.animFrame;
                if (m_castleMap.castleCombat.TickValue < battleBuilding.endingTick)
                {
                  if (num8 > 3)
                    num8 = 3;
                  SpriteWrapper nextExtraSprite = m_castleMap.getNextExtraSprite(GFXLibrary.Instance.AnimKillingPitsTexID, num8 + num7 * 4);
                  nextExtraSprite.Center = new PointF(50f, 50f);
                  nextExtraSprite.PosX = 0.0f;
                  nextExtraSprite.PosY = 0.0f;
                  sprite.AddChild(nextExtraSprite);
                  break;
                }
                break;
              }
              break;
            }
            sprite.Visible = false;
            return -1;
          }
          break;
        case 37:
          pointF.X = 64f;
          num1 = 33f;
          spriteTagOfset = !collapsed ? 429 : 430;
          break;
        case 38:
          pointF.X = 64f;
          num1 = 33f;
          spriteTagOfset = !collapsed ? 425 : 426;
          break;
        case 39:
          pointF.X = 64f;
          num1 = 33f;
          spriteTagOfset = !collapsed ? 455 : 456;
          break;
        case 40:
          pointF.X = 64f;
          num1 = 33f;
          spriteTagOfset = !collapsed ? 451 : 452;
          break;
        case 41:
          pointF.X = 80f;
          num1 = 16f;
          spriteTagOfset = !collapsed ? 460 : 463;
          break;
        case 42:
          pointF.X = 80f;
          num1 = 32f;
          spriteTagOfset = !collapsed ? 459 : 462;
          break;
        case 43:
          pointF.X = 91f;
          num1 = 16f;
          spriteTagOfset = !collapsed ? 461 : 464;
          if (element != null && m_castleMap.battleMode)
          {
            if (!((BattleBuilding) element).visible)
            {
              sprite.Visible = false;
              return -1;
            }
            break;
          }
          if (m_castleMap.attackerSetupMode)
          {
            sprite.Visible = false;
            return -1;
          }
          break;
        case 44:
          pointF.X = 80f;
          num1 = 32f;
          spriteTagOfset = !collapsed ? 468 : 469;
          break;
        case 45:
          spriteTagOfset = 470;
          pointF.X = 24f;
          num1 = 8f;
          break;
        case 46:
          spriteTagOfset = 471;
          pointF.X = 24f;
          num1 = 8f;
          break;
        case 51:
          textureID = GFXLibrary.Instance.FreeCardIconsID;
          spriteTagOfset = !collapsed ? 7 : 11;
          break;
        case 52:
          textureID = GFXLibrary.Instance.FreeCardIconsID;
          spriteTagOfset = !collapsed ? 8 : 12;
          break;
        case 53:
          textureID = GFXLibrary.Instance.FreeCardIconsID;
          spriteTagOfset = !collapsed ? 9 : 13;
          break;
        case 54:
          textureID = GFXLibrary.Instance.FreeCardIconsID;
          spriteTagOfset = !collapsed ? 10 : 14;
          break;
        case 55:
          textureID = GFXLibrary.Instance.FreeCardIconsID;
          pointF.X = 32f;
          spriteTagOfset = !collapsed ? 15 : 17;
          break;
        case 56:
          textureID = GFXLibrary.Instance.FreeCardIconsID;
          pointF.X = 32f;
          spriteTagOfset = !collapsed ? 16 : 18;
          break;
        case 57:
          textureID = GFXLibrary.Instance.FreeCardIconsID;
          pointF.X = 48f;
          num1 = 24f;
          spriteTagOfset = !collapsed ? 19 : 20;
          break;
      }
label_95:
      int TagID = 1;
      SizeF realSize;
      this.gfx.getSpriteLoader(textureID, ref TagID).GetSpriteXYdata(TagID, spriteTagOfset, out Rectangle _, out PointF _, out realSize);
      pointF.Y = (float) (int) realSize.Height - num1;
      sprite.Center = pointF;
      return spriteTagOfset;
    }

    private int getArrowTiltSpriteID(BattleArrow arrow, int startHeight, int targetHeight)
    {
      double x = (double) (arrow.fullDist / 8 * 24);
      double num = Math.Atan2((double) (targetHeight - startHeight), x) * 57.2957795;
      return num >= -50.0 ? (num >= -35.0 ? (num >= -17.0 ? (num >= -5.0 ? (num >= 5.0 ? (num >= 20.0 ? (num >= 35.0 ? (num >= 50.0 ? 8 : 7) : 6) : 5) : 4) : 3) : 2) : 1) : 0;
    }

    public void initRockchips(CastleMap m_castleMap)
    {
      m_castleMap.castleCombat.setRockCallback(new CastleCombat.RockChipCallback(this.rockChipCallback));
    }

    public void rockChipCallback(RockMissile rock)
    {
      int num = this.chipRand.Next(12, 18);
      for (int index = 0; index < num; ++index)
        this.addRockChips(rock.targX, rock.targY, rock.bombard);
      Point point = CastleMap.castleUnitSpritePoint[rock.targX, rock.targY];
      this.addRockSmoke((float) point.X, (float) point.Y, rock.bombard);
    }

    private void addRockChips(int posX, int posY, bool black)
    {
      CastleMapRendering.RockChip rockChip = new CastleMapRendering.RockChip();
      Point point = CastleMap.castleUnitSpritePoint[posX, posY];
      rockChip.xPos = (float) point.X;
      rockChip.yPos = (float) point.Y;
      rockChip.height = 1f;
      rockChip.vVelocity = (float) ((100 + this.chipRand.Next(25)) / 5);
      rockChip.gravityValue = 2.55112f;
      rockChip.dx = (float) this.chipRand.Next(-50, 50);
      rockChip.dy = (float) this.chipRand.Next(-50, 50);
      float num = (float) Math.Sqrt((double) rockChip.dx * (double) rockChip.dx + (double) rockChip.dy * (double) rockChip.dy) / 1.3f;
      rockChip.dx /= num;
      rockChip.dy /= num;
      rockChip.image = this.chipRand.Next(8);
      rockChip.black = black;
      this.rockChips.Add(rockChip);
    }

    public void updateRocks()
    {
      List<CastleMapRendering.RockChip> rockChipList = new List<CastleMapRendering.RockChip>();
      foreach (CastleMapRendering.RockChip rockChip in this.rockChips)
      {
        rockChip.height += rockChip.vVelocity;
        rockChip.vVelocity -= rockChip.gravityValue;
        rockChip.xPos += rockChip.dx;
        rockChip.yPos += rockChip.dy;
        if ((double) rockChip.height <= 0.0)
          rockChipList.Add(rockChip);
      }
      foreach (CastleMapRendering.RockChip rockChip in rockChipList)
        this.rockChips.Remove(rockChip);
      this.updateRockSmoke();
    }

    public void drawRockChips(CastleMap m_castleMap)
    {
      foreach (CastleMapRendering.RockChip rockChip in this.rockChips)
      {
        SpriteWrapper nextExtraSprite = m_castleMap.getNextExtraSprite(GFXLibrary.Instance.MissileTexID, 144 + rockChip.image);
        PointF pointF = new PointF(28f, 28f + rockChip.height);
        nextExtraSprite.Center = pointF;
        nextExtraSprite.PosX = rockChip.xPos;
        nextExtraSprite.PosY = rockChip.yPos;
        nextExtraSprite.Scale = 0.4f;
        nextExtraSprite.ColorToUse = !rockChip.black ? ARGBColors.White : Color.FromArgb((int) byte.MaxValue, 64, 64, 64);
        this.backgroundSprite.AddChild(nextExtraSprite, 2);
      }
      this.drawSmoke(m_castleMap);
    }

    private void addRockSmoke(float xPos, float yPos, bool black)
    {
      this.rockSmoke.Add(new CastleMapRendering.RockSmoke()
      {
        xPos = xPos,
        yPos = yPos,
        animFrame = 2,
        black = black
      });
    }

    private void updateRockSmoke()
    {
      List<CastleMapRendering.RockSmoke> rockSmokeList = new List<CastleMapRendering.RockSmoke>();
      foreach (CastleMapRendering.RockSmoke rockSmoke in this.rockSmoke)
      {
        ++rockSmoke.animFrame;
        if (rockSmoke.animFrame >= 20)
          rockSmokeList.Add(rockSmoke);
      }
      foreach (CastleMapRendering.RockSmoke rockSmoke in rockSmokeList)
        this.rockSmoke.Remove(rockSmoke);
    }

    private void drawSmoke(CastleMap m_castleMap)
    {
      foreach (CastleMapRendering.RockSmoke rockSmoke in this.rockSmoke)
      {
        SpriteWrapper nextExtraSprite = m_castleMap.getNextExtraSprite(GFXLibrary.Instance.Smoke1TexID, rockSmoke.animFrame / 2);
        nextExtraSprite.Center = new PointF(50f, 75f);
        nextExtraSprite.PosX = rockSmoke.xPos;
        nextExtraSprite.PosY = rockSmoke.yPos;
        nextExtraSprite.Scale = 1.5f;
        if (rockSmoke.black)
        {
          nextExtraSprite.ColorToUse = Color.FromArgb((int) byte.MaxValue, 64, 64, 64);
          nextExtraSprite.ScaleX = 2.3f;
        }
        else
          nextExtraSprite.ColorToUse = ARGBColors.White;
        this.backgroundSprite.AddChild(nextExtraSprite, 2);
      }
    }

    public void drawCatapultLine(CastleMap.CatapultLine line)
    {
      this.gfx.startThickLine(Color.FromArgb(0, (int) byte.MaxValue, 0), 3f);
      this.gfx.setThickLineRadius(1f);
      Point point1 = CastleMap.castleUnitSpritePoint[line.startX, line.startY];
      Point point2 = CastleMap.castleUnitSpritePoint[line.endX, line.endY];
      this.gfx.addThickLinePoint((float) point1.X + this.backgroundSprite.DrawPos.X, (float) point1.Y + this.backgroundSprite.DrawPos.Y);
      this.gfx.addThickLinePoint((float) point2.X + this.backgroundSprite.DrawPos.X, (float) point2.Y + this.backgroundSprite.DrawPos.Y);
      this.gfx.drawThickLines(true);
    }

    public class RockChip
    {
      public float xPos;
      public float yPos;
      public float dx;
      public float dy;
      public float height;
      public float vVelocity;
      public float gravityValue;
      public int image;
      public bool black;
    }

    public class RockSmoke
    {
      public float xPos;
      public float yPos;
      public int animFrame;
      public bool black;
    }
  }
}
