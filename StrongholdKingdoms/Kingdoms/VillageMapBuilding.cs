// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageMapBuilding
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class VillageMapBuilding
  {
    public long buildingID;
    public int buildingType;
    public Point buildingLocation;
    public DateTime completionTime;
    public bool complete;
    public bool localComplete = true;
    public bool showFullConstructionText;
    public bool goTransparent;
    public bool completeRequestSent;
    public bool serverDeleting;
    public DateTime deletionTime;
    public DateTime lastCalcTime;
    public double calcRate;
    public double serverCalcRate;
    public double lastDataLevel;
    public double serverJourneyTime;
    public double tripCalcRate;
    public Point storageLocation;
    public bool gotEmployee;
    public bool buildingActive;
    public bool highlighted;
    public SpriteWrapper baseSprite;
    public SpriteWrapper shadowSprite;
    public SpriteWrapper animSprite;
    public SpriteWrapper symbolSprite;
    public SpriteWrapper productionSprite;
    public SpriteWrapper extraAnimSprite1;
    public SpriteWrapper extraAnimSprite2;
    public VillageMapBuildingStockpileExtension stockpileExtension;
    public VillageMapBuildingGranaryExtension granaryExtension;
    public VillageMapBuildingInnExtension innExtension;
    public bool open;
    public bool lastOpenState;
    public VillageMapPerson worker;
    public VillageMapPerson secondaryWorker;
    public int productionState;
    public int productionGFXCounter;
    public float productionGFXVelocity = 0.5f;
    public double productionTime;
    public double journeyTime;
    public double journeyTime2;
    public bool workerNeedsReInitializing;
    public bool weaponContinuance;
    public int data1;
    public int data2;
    public int[] capitalResourceLevels;
    public int randState = -1;
    public static readonly int[] goods48Levels = new int[14]
    {
      0,
      0,
      48,
      288,
      1248,
      3648,
      13248,
      37248,
      133248,
      373248,
      1333248,
      3733248,
      13333248,
      133333248
    };
    public static readonly int[] goods16Levels = new int[15]
    {
      0,
      0,
      16,
      96,
      416,
      1216,
      4416,
      12416,
      44416,
      124416,
      444416,
      1244416,
      4444416,
      12444416,
      124444416
    };
    public static readonly int[] goodsDividers = new int[19]
    {
      1,
      1,
      5,
      20,
      50,
      200,
      500,
      2000,
      5000,
      20000,
      50000,
      200000,
      500000,
      200000,
      5000000,
      2000000,
      5000000,
      20000000,
      50000000
    };
    public static readonly int[] woodPileOrder = new int[16]
    {
      6,
      3,
      7,
      10,
      1,
      4,
      8,
      11,
      13,
      0,
      2,
      5,
      9,
      12,
      14,
      15
    };
    public static readonly int[] stonePileOrder = new int[16]
    {
      0,
      1,
      2,
      4,
      3,
      7,
      11,
      8,
      5,
      6,
      10,
      13,
      15,
      14,
      12,
      9
    };
    public static readonly int[] ironPileOrder = new int[16]
    {
      15,
      11,
      13,
      14,
      4,
      7,
      8,
      10,
      12,
      0,
      1,
      2,
      3,
      5,
      6,
      9
    };
    public static readonly int[] pitchPileOrder = new int[16]
    {
      9,
      5,
      8,
      12,
      2,
      4,
      7,
      11,
      14,
      0,
      1,
      3,
      6,
      10,
      13,
      15
    };
    public static readonly int[] meatPileOrder = new int[4]
    {
      9,
      8,
      1,
      0
    };
    public static readonly int[] vegPileOrder = new int[3]
    {
      5,
      6,
      7
    };
    public static readonly int[] applesPileOrder = new int[3]
    {
      2,
      3,
      4
    };
    public static readonly int[] fishPileOrder = new int[3]
    {
      10,
      11,
      12
    };
    public static readonly int[] cheesePileOrder = new int[4]
    {
      15,
      16,
      19,
      20
    };
    public static readonly int[] breadPileOrder = new int[4]
    {
      13,
      14,
      17,
      18
    };
    private bool[] pilesUsed = new bool[16];

    public bool isComplete() => this.complete && this.localComplete;

    public bool isDeleting() => this.serverDeleting;

    public void createFromReturnData(VillageBuildingReturnData serverBuild)
    {
      this.completeRequestSent = false;
      this.buildingID = serverBuild.buildingID;
      this.buildingLocation = (Point) serverBuild.buildingLocation;
      this.buildingType = serverBuild.buildingType;
      this.serverCalcRate = this.calcRate = serverBuild.calcRate;
      this.serverJourneyTime = serverBuild.journeyTime;
      this.completionTime = serverBuild.completionTime;
      this.lastCalcTime = serverBuild.lastCalcTime;
      this.lastDataLevel = serverBuild.lastDataLevel;
      this.gotEmployee = serverBuild.gotEmployee;
      this.buildingActive = serverBuild.active;
      this.localComplete = true;
      this.deletionTime = serverBuild.deletionTime;
      this.serverDeleting = serverBuild.deleting;
      this.capitalResourceLevels = serverBuild.capitalResourceLevels;
      if (this.baseSprite == null)
        return;
      this.baseSprite.clearText();
      this.baseSprite.clearSecondText();
    }

    public void initStorageBuilding(GraphicsMgr gfx, VillageMap vm)
    {
      if (this.buildingType == 2)
        this.updateStockpile(gfx, vm);
      if (this.buildingType == 3)
        this.updateGranary(gfx, vm);
      if (this.buildingType != 35)
        return;
      this.updateInn(gfx, vm);
    }

    public void updateStockpile(GraphicsMgr gfx, VillageMap vm)
    {
      if (this.baseSprite == null)
        return;
      if (this.stockpileExtension == null)
      {
        this.stockpileExtension = new VillageMapBuildingStockpileExtension();
        for (int cellID = 0; cellID < 16; ++cellID)
        {
          this.stockpileExtension.cell[cellID] = new SpriteWrapper();
          this.stockpileExtension.cell[cellID].Visible = false;
          this.stockpileExtension.cell[cellID].PosX = (float) (VillageMapBuildingStockpileExtension.stockpileLayout[cellID * 2] - 96);
          this.stockpileExtension.cell[cellID].PosY = (float) (VillageMapBuildingStockpileExtension.stockpileLayout[cellID * 2 + 1] - 43);
          this.baseSprite.AddChild(this.stockpileExtension.cell[cellID]);
          this.stockpileExtension.showGood(gfx, cellID, -1, 0);
        }
      }
      for (int cellID = 0; cellID < 16; ++cellID)
        this.stockpileExtension.showGood(gfx, cellID, -1, 0);
      VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
      if (vm.getStockpileLevels(levels))
      {
        int index1 = levels.woodLevel <= 13333248.0 ? (levels.woodLevel <= 3733248.0 ? (levels.woodLevel <= 1333248.0 ? (levels.woodLevel <= 373248.0 ? (levels.woodLevel <= 133248.0 ? (levels.woodLevel <= 37248.0 ? (levels.woodLevel <= 13248.0 ? (levels.woodLevel <= 3648.0 ? (levels.woodLevel <= 1248.0 ? (levels.woodLevel <= 288.0 ? (levels.woodLevel <= 48.0 ? (levels.woodLevel <= 0.0 ? 0 : 1) : 2) : 3) : 4) : 5) : 6) : 7) : 8) : 9) : 10) : 11) : 12;
        int index2 = levels.stoneLevel <= 13333248.0 ? (levels.stoneLevel <= 3733248.0 ? (levels.stoneLevel <= 1333248.0 ? (levels.stoneLevel <= 373248.0 ? (levels.stoneLevel <= 133248.0 ? (levels.stoneLevel <= 37248.0 ? (levels.stoneLevel <= 13248.0 ? (levels.stoneLevel <= 3648.0 ? (levels.stoneLevel <= 1248.0 ? (levels.stoneLevel <= 288.0 ? (levels.stoneLevel <= 48.0 ? (levels.stoneLevel <= 0.0 ? 0 : 1) : 2) : 3) : 4) : 5) : 6) : 7) : 8) : 9) : 10) : 11) : 12;
        int index3 = levels.ironLevel <= 13333248.0 ? (levels.ironLevel <= 3733248.0 ? (levels.ironLevel <= 1333248.0 ? (levels.ironLevel <= 373248.0 ? (levels.ironLevel <= 133248.0 ? (levels.ironLevel <= 37248.0 ? (levels.ironLevel <= 13248.0 ? (levels.ironLevel <= 3648.0 ? (levels.ironLevel <= 1248.0 ? (levels.ironLevel <= 288.0 ? (levels.ironLevel <= 48.0 ? (levels.ironLevel <= 0.0 ? 0 : 1) : 2) : 3) : 4) : 5) : 6) : 7) : 8) : 9) : 10) : 11) : 12;
        int index4 = levels.pitchLevel <= 4444416.0 ? (levels.pitchLevel <= 1244416.0 ? (levels.pitchLevel <= 444416.0 ? (levels.pitchLevel <= 124416.0 ? (levels.pitchLevel <= 44416.0 ? (levels.pitchLevel <= 12416.0 ? (levels.pitchLevel <= 4416.0 ? (levels.pitchLevel <= 1216.0 ? (levels.pitchLevel <= 416.0 ? (levels.pitchLevel <= 96.0 ? (levels.pitchLevel <= 16.0 ? (levels.pitchLevel <= 0.0 ? 0 : 1) : 2) : 3) : 4) : 5) : 6) : 7) : 8) : 9) : 10) : 11) : 12;
        for (int index5 = 0; index5 < 16; ++index5)
          this.pilesUsed[index5] = false;
        int num1 = index1 + index2 + index3 + index4;
        if (num1 > 16)
        {
          int num2 = 16;
          int num3 = 0;
          if (index1 >= 1)
            ++num3;
          if (index2 >= 1)
            ++num3;
          if (index3 >= 1)
            ++num3;
          if (index4 >= 1)
            ++num3;
          int num4 = num2 - num3;
          double num5 = (double) num4 / (double) (num1 - num3);
          VillageMapBuilding.PileOrderSort[] pileOrderSortArray = new VillageMapBuilding.PileOrderSort[4];
          int num6 = 0;
          if (index1 > 1)
          {
            VillageMapBuilding.PileOrderSort pileOrderSort = new VillageMapBuilding.PileOrderSort();
            pileOrderSort.origPiles = pileOrderSort.numPiles = (double) (index1 - 1);
            pileOrderSort.type = 0;
            pileOrderSortArray[num6++] = pileOrderSort;
          }
          if (index2 > 1)
          {
            VillageMapBuilding.PileOrderSort pileOrderSort = new VillageMapBuilding.PileOrderSort();
            pileOrderSort.origPiles = pileOrderSort.numPiles = (double) (index2 - 1);
            pileOrderSort.type = 3;
            pileOrderSortArray[num6++] = pileOrderSort;
          }
          if (index3 > 1)
          {
            VillageMapBuilding.PileOrderSort pileOrderSort = new VillageMapBuilding.PileOrderSort();
            pileOrderSort.origPiles = pileOrderSort.numPiles = (double) (index3 - 1);
            pileOrderSort.type = 4;
            pileOrderSortArray[num6++] = pileOrderSort;
          }
          if (index4 > 1)
          {
            VillageMapBuilding.PileOrderSort pileOrderSort = new VillageMapBuilding.PileOrderSort();
            pileOrderSort.origPiles = pileOrderSort.numPiles = (double) (index4 - 1);
            pileOrderSort.type = 5;
            pileOrderSortArray[num6++] = pileOrderSort;
          }
          if (num6 > 1)
          {
            for (int index6 = 0; index6 < num6 - 1; ++index6)
            {
              for (int index7 = 0; index7 < num6 - 1; ++index7)
              {
                if (pileOrderSortArray[index7].numPiles < pileOrderSortArray[index7 + 1].numPiles)
                {
                  VillageMapBuilding.PileOrderSort pileOrderSort = pileOrderSortArray[index7];
                  pileOrderSortArray[index7] = pileOrderSortArray[index7 + 1];
                  pileOrderSortArray[index7 + 1] = pileOrderSort;
                }
              }
            }
          }
          int num7 = 0;
          for (int index8 = 0; index8 < num6; ++index8)
          {
            pileOrderSortArray[index8].numPiles = Math.Floor(pileOrderSortArray[index8].numPiles * num5);
            num7 += (int) pileOrderSortArray[index8].numPiles;
          }
          if (num7 < num4)
          {
            int num8 = num4 - num7;
            int num9 = 0;
            while (num8 > 0)
            {
              int index9 = num9 % num6;
              if (pileOrderSortArray[index9].numPiles < pileOrderSortArray[index9].origPiles)
              {
                ++pileOrderSortArray[index9].numPiles;
                --num8;
              }
              ++num9;
            }
          }
          if (index1 >= 1)
            index1 = 1;
          if (index2 >= 1)
            index2 = 1;
          if (index3 >= 1)
            index3 = 1;
          if (index4 >= 1)
            index4 = 1;
          for (int index10 = 0; index10 < num6; ++index10)
          {
            int numPiles = (int) pileOrderSortArray[index10].numPiles;
            switch (pileOrderSortArray[index10].type)
            {
              case 0:
                index1 += numPiles;
                break;
              case 3:
                index2 += numPiles;
                break;
              case 4:
                index3 += numPiles;
                break;
              case 5:
                index4 += numPiles;
                break;
            }
          }
          if (index1 + index2 + index3 + index4 != 16)
            index1 = 0;
        }
        int num10 = 0;
        for (int index11 = 0; index11 < index1; ++index11)
        {
          int cellID = VillageMapBuilding.woodPileOrder[num10++];
          this.pilesUsed[cellID] = true;
          if (index11 != index1 - 1)
          {
            this.stockpileExtension.showGood(gfx, cellID, 6, 48);
          }
          else
          {
            int val1 = ((int) levels.woodLevel - VillageMapBuilding.goods48Levels[index1]) / VillageMapBuilding.goodsDividers[index1];
            this.stockpileExtension.showGood(gfx, cellID, 6, Math.Min(val1, 48));
          }
        }
        int num11 = 0;
        for (int index12 = 0; index12 < index3; ++index12)
        {
          int cellID;
          do
          {
            cellID = VillageMapBuilding.ironPileOrder[num11++];
          }
          while (this.pilesUsed[cellID]);
          this.pilesUsed[cellID] = true;
          if (index12 != index3 - 1)
          {
            this.stockpileExtension.showGood(gfx, cellID, 8, 48);
          }
          else
          {
            int val1 = ((int) levels.ironLevel - VillageMapBuilding.goods48Levels[index3]) / VillageMapBuilding.goodsDividers[index3];
            this.stockpileExtension.showGood(gfx, cellID, 8, Math.Min(val1, 48));
          }
        }
        int num12 = 0;
        for (int index13 = 0; index13 < index2; ++index13)
        {
          int cellID;
          do
          {
            cellID = VillageMapBuilding.stonePileOrder[num12++];
          }
          while (this.pilesUsed[cellID]);
          this.pilesUsed[cellID] = true;
          if (index13 != index2 - 1)
          {
            this.stockpileExtension.showGood(gfx, cellID, 7, 48);
          }
          else
          {
            int val1 = ((int) levels.stoneLevel - VillageMapBuilding.goods48Levels[index2]) / VillageMapBuilding.goodsDividers[index2];
            this.stockpileExtension.showGood(gfx, cellID, 7, Math.Min(val1, 48));
          }
        }
        int num13 = 0;
        for (int index14 = 0; index14 < index4; ++index14)
        {
          int cellID;
          do
          {
            cellID = VillageMapBuilding.pitchPileOrder[num13++];
          }
          while (this.pilesUsed[cellID]);
          this.pilesUsed[cellID] = true;
          if (index14 != index4 - 1)
          {
            this.stockpileExtension.showGood(gfx, cellID, 9, 16);
          }
          else
          {
            int val1 = ((int) levels.pitchLevel - VillageMapBuilding.goods16Levels[index4]) / VillageMapBuilding.goodsDividers[index4];
            this.stockpileExtension.showGood(gfx, cellID, 9, Math.Min(val1, 16));
          }
        }
      }
      else
      {
        for (int cellID = 0; cellID < 16; ++cellID)
          this.stockpileExtension.showGood(gfx, cellID, -1, 0);
      }
    }

    public void updateGranary(GraphicsMgr gfx, VillageMap vm)
    {
      if (this.baseSprite == null)
        return;
      if (this.granaryExtension == null)
      {
        this.granaryExtension = new VillageMapBuildingGranaryExtension();
        for (int cellID = 0; cellID < 21; ++cellID)
        {
          this.granaryExtension.cell[cellID] = new SpriteWrapper();
          this.granaryExtension.cell[cellID].Visible = false;
          this.granaryExtension.cell[cellID].PosX = (float) (5 + VillageMapBuildingGranaryExtension.granaryLayout[cellID * 2]);
          this.granaryExtension.cell[cellID].PosY = (float) (VillageMapBuildingGranaryExtension.granaryLayout[cellID * 2 + 1] - 33);
          this.baseSprite.AddChild(this.granaryExtension.cell[cellID]);
          this.granaryExtension.showGood(gfx, cellID, -1, 0);
        }
      }
      for (int cellID = 0; cellID < 21; ++cellID)
        this.granaryExtension.showGood(gfx, cellID, -1, 0);
      VillageMap.GranaryLevels levels = new VillageMap.GranaryLevels();
      if (!vm.getGranaryLevels(levels))
        return;
      if (vm.granaryOpenCount == 0)
        this.open = false;
      if (levels.applesLevel + levels.breadLevel + levels.cheeseLevel + levels.fishLevel + levels.meatLevel + levels.vegLevel <= 0.0 || vm.granaryOpenCount <= 0)
        return;
      this.open = true;
      int index1 = levels.meatLevel <= 416.0 ? (levels.meatLevel <= 96.0 ? (levels.meatLevel <= 16.0 ? (levels.meatLevel <= 0.0 ? 0 : 1) : 2) : 3) : 4;
      int index2 = levels.vegLevel <= 96.0 ? (levels.vegLevel <= 16.0 ? (levels.vegLevel <= 0.0 ? 0 : 1) : 2) : 3;
      int index3 = levels.cheeseLevel <= 416.0 ? (levels.cheeseLevel <= 96.0 ? (levels.cheeseLevel <= 16.0 ? (levels.cheeseLevel <= 0.0 ? 0 : 1) : 2) : 3) : 4;
      int index4 = levels.applesLevel <= 96.0 ? (levels.applesLevel <= 16.0 ? (levels.applesLevel <= 0.0 ? 0 : 1) : 2) : 3;
      int index5 = levels.fishLevel <= 96.0 ? (levels.fishLevel <= 16.0 ? (levels.fishLevel <= 0.0 ? 0 : 1) : 2) : 3;
      int index6 = levels.breadLevel <= 832.0 ? (levels.breadLevel <= 192.0 ? (levels.breadLevel <= 32.0 ? (levels.breadLevel <= 0.0 ? 0 : 1) : 2) : 3) : 4;
      for (int index7 = 0; index7 < index1; ++index7)
      {
        int cellID = VillageMapBuilding.meatPileOrder[index7];
        if (index7 != index1 - 1)
        {
          this.granaryExtension.showGood(gfx, cellID, 16, 16);
        }
        else
        {
          int val1 = ((int) levels.meatLevel - VillageMapBuilding.goods16Levels[index1]) / VillageMapBuilding.goodsDividers[index1];
          this.granaryExtension.showGood(gfx, cellID, 16, Math.Min(val1, 16));
        }
      }
      for (int index8 = 0; index8 < index2; ++index8)
      {
        int cellID = VillageMapBuilding.vegPileOrder[index8];
        if (index8 != index2 - 1)
        {
          this.granaryExtension.showGood(gfx, cellID, 15, 16);
        }
        else
        {
          int val1 = ((int) levels.vegLevel - VillageMapBuilding.goods16Levels[index2]) / VillageMapBuilding.goodsDividers[index2];
          this.granaryExtension.showGood(gfx, cellID, 15, Math.Min(val1, 16));
        }
      }
      for (int index9 = 0; index9 < index3; ++index9)
      {
        int cellID = VillageMapBuilding.cheesePileOrder[index9];
        if (index9 != index3 - 1)
        {
          this.granaryExtension.showGood(gfx, cellID, 17, 16);
        }
        else
        {
          int val1 = ((int) levels.cheeseLevel - VillageMapBuilding.goods16Levels[index3]) / VillageMapBuilding.goodsDividers[index3];
          this.granaryExtension.showGood(gfx, cellID, 17, Math.Min(val1, 16));
        }
      }
      for (int index10 = 0; index10 < index4; ++index10)
      {
        int cellID = VillageMapBuilding.applesPileOrder[index10];
        if (index10 != index4 - 1)
        {
          this.granaryExtension.showGood(gfx, cellID, 13, 16);
        }
        else
        {
          int val1 = ((int) levels.applesLevel - VillageMapBuilding.goods16Levels[index4]) / VillageMapBuilding.goodsDividers[index4];
          this.granaryExtension.showGood(gfx, cellID, 13, Math.Min(val1, 16));
        }
      }
      for (int index11 = 0; index11 < index5; ++index11)
      {
        int cellID = VillageMapBuilding.fishPileOrder[index11];
        if (index11 != index5 - 1)
        {
          this.granaryExtension.showGood(gfx, cellID, 18, 16);
        }
        else
        {
          int val1 = ((int) levels.fishLevel - VillageMapBuilding.goods16Levels[index5]) / VillageMapBuilding.goodsDividers[index5];
          this.granaryExtension.showGood(gfx, cellID, 18, Math.Min(val1, 16));
        }
      }
      for (int index12 = 0; index12 < index6; ++index12)
      {
        int cellID = VillageMapBuilding.breadPileOrder[index12];
        if (index12 != index6 - 1)
        {
          this.granaryExtension.showGood(gfx, cellID, 14, 32);
        }
        else
        {
          int val1 = ((int) levels.breadLevel - VillageMapBuilding.goods16Levels[index6] * 2) / VillageMapBuilding.goodsDividers[index6];
          this.granaryExtension.showGood(gfx, cellID, 14, Math.Min(val1, 32));
        }
      }
    }

    public void updateInn(GraphicsMgr gfx, VillageMap vm)
    {
      if (this.baseSprite == null)
        return;
      if (this.innExtension == null)
      {
        this.innExtension = new VillageMapBuildingInnExtension();
        for (int cellID = 0; cellID < 3; ++cellID)
        {
          this.innExtension.cell[cellID] = new SpriteWrapper();
          this.innExtension.cell[cellID].Visible = false;
          this.innExtension.cell[cellID].PosX = (float) (VillageMapBuildingInnExtension.innLayout[cellID * 2] - 80);
          this.innExtension.cell[cellID].PosY = (float) (VillageMapBuildingInnExtension.innLayout[cellID * 2 + 1] - 44);
          this.baseSprite.AddChild(this.innExtension.cell[cellID]);
          this.innExtension.showGood(gfx, cellID, -1, 0);
        }
      }
      for (int cellID = 0; cellID < 3; ++cellID)
        this.innExtension.showGood(gfx, cellID, -1, 0);
      VillageMap.InnLevels levels = new VillageMap.InnLevels();
      if (!vm.getInnLevels(levels))
        return;
      if (levels.aleLevel == 0.0)
      {
        if (vm.m_effectiveAleRationsLevel > 0.0)
          this.open = true;
        else
          this.open = false;
      }
      else
      {
        this.open = true;
        int index = levels.aleLevel <= 416.0 ? (levels.aleLevel <= 96.0 ? (levels.aleLevel <= 16.0 ? (levels.aleLevel <= 0.0 ? 0 : 1) : 2) : 3) : 4;
        if (index > 3)
          index = 3;
        for (int cellID = 0; cellID < index; ++cellID)
        {
          if (cellID != index - 1)
          {
            this.innExtension.showGood(gfx, cellID, 12, 16);
          }
          else
          {
            int val1 = ((int) levels.aleLevel - VillageMapBuilding.goods16Levels[index]) / VillageMapBuilding.goodsDividers[index];
            this.innExtension.showGood(gfx, cellID, 12, Math.Min(val1, 16));
          }
        }
      }
    }

    public bool updateConstructionGFX(
      double localBaseTime,
      DateTime serverBaseTime,
      bool initialUpdate,
      VillageMap vm)
    {
      if (this.baseSprite == null)
        return false;
      if (this.serverDeleting)
      {
        double num = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0 - 1.5;
        DateTime dateTime = serverBaseTime.AddSeconds(num);
        if (!this.complete && dateTime.CompareTo(this.completionTime) >= 0)
          this.complete = true;
        TimeSpan timeSpan = this.deletionTime - dateTime;
        int secsLeft = (int) (timeSpan.TotalSeconds - 0.5);
        if (timeSpan.TotalDays > 10.0)
          secsLeft = 9999999;
        if (secsLeft > 0 && secsLeft < 10000000)
        {
          if (!vm.ViewOnly)
            this.baseSprite.attachText(VillageMap.createBuildTimeString(secsLeft), new Point(0, -50), ARGBColors.White, true, true);
        }
        else
        {
          this.baseSprite.clearText();
          this.baseSprite.clearSecondText();
          if (secsLeft <= 0)
            return true;
        }
        this.baseSprite.ColorToUse = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 128, 128);
        if (this.animSprite != null)
          this.animSprite.ColorToUse = this.baseSprite.ColorToUse;
        if (this.extraAnimSprite1 != null)
          this.extraAnimSprite1.ColorToUse = this.baseSprite.ColorToUse;
        if (this.extraAnimSprite2 != null)
          this.extraAnimSprite2.ColorToUse = this.baseSprite.ColorToUse;
        return false;
      }
      if (this.complete)
        return false;
      bool flag = false;
      double num1 = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
      if (initialUpdate)
        num1 = 0.0;
      double num2 = num1 - 3.0;
      DateTime dateTime1 = serverBaseTime.AddSeconds(num2);
      if (dateTime1.CompareTo(this.completionTime) < 0)
        flag = true;
      if (this.buildingType == 0)
        flag = false;
      if (flag)
      {
        if (!this.highlighted)
        {
          this.baseSprite.ColorToUse = Color.FromArgb(128, 128, 128, 128);
          if (this.animSprite != null)
            this.animSprite.ColorToUse = Color.FromArgb(128, 128, 128, 128);
          if (this.extraAnimSprite1 != null)
            this.extraAnimSprite1.ColorToUse = Color.FromArgb(128, 128, 128, 128);
          if (this.extraAnimSprite2 != null)
            this.extraAnimSprite2.ColorToUse = Color.FromArgb(128, 128, 128, 128);
        }
        int secsLeft1 = (int) ((this.completionTime - dateTime1).TotalSeconds - 0.5);
        if (secsLeft1 > 0 && secsLeft1 < 10000000)
        {
          int secsLeft2 = secsLeft1;
          int queuePosition;
          int secsLeft3 = vm.updateConstructionDisplayTime(secsLeft1, this.completionTime, out queuePosition);
          Color col = queuePosition == 1 ? ARGBColors.White : ARGBColors.WhiteSmoke;
          if (!vm.ViewOnly)
          {
            string text = VillageMap.createBuildTimeString(secsLeft3);
            if (secsLeft3 != secsLeft2 && this.showFullConstructionText)
            {
              this.showFullConstructionText = false;
              text = text + Environment.NewLine + "(" + VillageMap.createBuildTimeString(secsLeft2) + ")";
            }
            this.baseSprite.attachText(text, new Point(0, -40), ARGBColors.White, true, true);
            if (queuePosition > 0)
              this.baseSprite.attachSecondText(queuePosition.ToString(), new Point(0, -55), col, true, true);
            else
              this.baseSprite.clearSecondText();
          }
        }
        else
        {
          this.baseSprite.clearText();
          this.baseSprite.clearSecondText();
        }
      }
      else
      {
        Color white = ARGBColors.White;
        this.baseSprite.ColorToUse = white;
        if (this.animSprite != null)
          this.animSprite.ColorToUse = white;
        if (this.extraAnimSprite1 != null)
          this.extraAnimSprite1.ColorToUse = white;
        if (this.extraAnimSprite2 != null)
          this.extraAnimSprite2.ColorToUse = white;
        this.complete = true;
        if (!initialUpdate)
        {
          this.localComplete = false;
          return true;
        }
        this.baseSprite.clearText();
        this.baseSprite.clearSecondText();
      }
      return false;
    }

    public void updateSymbolGFX()
    {
      this.symbolSprite.Visible = false;
      this.symbolSprite.SpriteNo = 58;
      if (!VillageBuildingsData.buildingRequiresWorker(this.buildingType) || !this.complete)
        return;
      if (!this.buildingActive)
      {
        this.symbolSprite.initAnim(59, 11, 1, 100);
        this.symbolSprite.Visible = true;
      }
      else
      {
        if (this.gotEmployee)
          return;
        this.symbolSprite.Visible = true;
      }
    }

    public void initProductionGFX()
    {
    }

    public int getProductionSpriteNo(int buildingType)
    {
      switch (buildingType)
      {
        case 6:
          return 122;
        case 7:
          return 117;
        case 8:
          return 107;
        case 9:
          return 113;
        case 12:
          return 95;
        case 13:
          return 96;
        case 14:
          return 99;
        case 15:
          return 119;
        case 16:
          return 108;
        case 17:
          return 101;
        case 18:
          return 103;
        case 19:
          return 102;
        case 21:
          return 105;
        case 22:
          return 120;
        case 23:
          return 114;
        case 24:
          return 116;
        case 25:
          return 115;
        case 26:
          return 109;
        case 28:
          return 112;
        case 29:
          return 98;
        case 30:
          return 118;
        case 31:
          return 97;
        case 32:
          return 100;
        case 33:
          return 121;
        default:
          return 123;
      }
    }

    public void updateProductionGFX(bool reset)
    {
      if (!Program.mySettings.ShowProductionInfo && this.productionSprite.Visible)
        this.productionSprite.Visible = false;
      else if (reset)
      {
        this.productionSprite.SpriteNo = this.getProductionSpriteNo(this.buildingType);
        this.productionSprite.Visible = true;
        this.productionSprite.PosX = 0.0f;
        this.productionSprite.PosY = -50f;
        this.productionSprite.ColorToUse = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        this.productionGFXCounter = 0;
        double payloadSize = GameEngine.Instance.LocalWorldData.getPayloadSize(this.buildingType);
        double num = CardTypes.adjustPayloadSize(GameEngine.Instance.cardsManager.UserCardData, payloadSize, this.buildingType) - payloadSize;
        if (num > 0.99)
        {
          this.productionSprite.attachText(payloadSize.ToString(), new Point(-15, 15), Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), true, true);
          this.productionSprite.attachSecondText("(+" + num.ToString() + ")", new Point(10, 15), Color.FromArgb(0, 150, (int) byte.MaxValue, 180), true, true);
        }
        else
          this.productionSprite.attachText(payloadSize.ToString(), new Point(0, 15), ARGBColors.White, true, true);
      }
      else
      {
        if (this.productionGFXCounter <= 50)
        {
          this.productionSprite.PosY -= 0.5f;
          this.productionSprite.changeAlpha(80);
          this.productionSprite.changeTextAlpha(80);
          this.productionSprite.changeSecondTextAlpha(80);
        }
        else
        {
          this.productionSprite.PosY -= 2f;
          this.productionSprite.changeAlpha(-10);
          this.productionSprite.changeTextAlpha(-10);
          this.productionSprite.changeSecondTextAlpha(-10);
        }
        ++this.productionGFXCounter;
      }
    }

    public bool Visible
    {
      set
      {
        if (this.shadowSprite != null)
          this.shadowSprite.Visible = value;
        if (this.baseSprite != null)
          this.baseSprite.Visible = value;
        if (this.worker == null)
          return;
        this.worker.Visible = value;
      }
    }

    public class PileOrderSort
    {
      public int type;
      public double numPiles;
      public double origPiles;
    }
  }
}
