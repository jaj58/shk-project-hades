// Decompiled with JetBrains decompiler
// Type: Kingdoms.Banqueting
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;

//#nullable disable
namespace Kingdoms
{
  public class Banqueting
  {
    public VillageMap m_village;
    private VillageMap.TownHallLevels oldLevels;
    public int[] resourceLevels = new int[8];
    public int[,] banquetLevels = new int[8, 8];

    public Banqueting(VillageMap village) => this.m_village = village;

    public int researchLevel
    {
      get => (int) GameEngine.Instance.World.UserResearchData.Research_Craftsmanship;
    }

    public void updateLevels(bool force)
    {
      VillageMap.TownHallLevels levels = new VillageMap.TownHallLevels();
      this.m_village.getTownHallLevels(levels);
      if (force || this.oldLevels == null)
      {
        this.oldLevels = levels;
      }
      else
      {
        bool flag = false;
        if (levels.venisonLevel != this.oldLevels.venisonLevel)
          flag = true;
        if (levels.furnitureLevel != this.oldLevels.furnitureLevel)
          flag = true;
        if (levels.metalwareLevel != this.oldLevels.metalwareLevel)
          flag = true;
        if (levels.clothesLevel != this.oldLevels.clothesLevel)
          flag = true;
        if (levels.wineLevel != this.oldLevels.wineLevel)
          flag = true;
        if (levels.saltLevel != this.oldLevels.saltLevel)
          flag = true;
        if (levels.spicesLevel != this.oldLevels.spicesLevel)
          flag = true;
        if (levels.silkLevel != this.oldLevels.silkLevel)
          flag = true;
        if (!flag)
          return;
        this.oldLevels = levels;
      }
      this.resourceLevels[0] = (int) levels.venisonLevel;
      this.resourceLevels[1] = (int) levels.furnitureLevel;
      this.resourceLevels[2] = (int) levels.metalwareLevel;
      this.resourceLevels[3] = (int) levels.clothesLevel;
      this.resourceLevels[4] = (int) levels.wineLevel;
      this.resourceLevels[5] = (int) levels.saltLevel;
      this.resourceLevels[6] = (int) levels.spicesLevel;
      this.resourceLevels[7] = (int) levels.silkLevel;
      for (int researchLevel = this.researchLevel; researchLevel < 8; ++researchLevel)
        this.resourceLevels[researchLevel] = 0;
      int[] numArray1 = new int[8];
      for (int index1 = 0; index1 < 8; ++index1)
      {
        numArray1[index1] = this.resourceLevels[index1];
        for (int index2 = 0; index2 < 8; ++index2)
          this.banquetLevels[index2, index1] = 0;
      }
      for (int index3 = 0; index3 < 8; ++index3)
      {
        int num1 = 2000000000;
        for (int index4 = 0; index4 < 8; ++index4)
        {
          if (numArray1[index4] > 0 && numArray1[index4] < num1)
            num1 = numArray1[index4];
        }
        if (num1 == 2000000000)
          break;
        int num2 = 0;
        int[] numArray2 = new int[8];
        for (int index5 = 0; index5 < 8; ++index5)
        {
          numArray2[index5] = 0;
          if (numArray1[index5] >= num1)
          {
            ++num2;
            numArray1[index5] -= num1;
            numArray2[index5] = num1;
          }
        }
        for (int index6 = 0; index6 < 8; ++index6)
          this.banquetLevels[num2 - 1, index6] = numArray2[index6];
      }
    }

    public int getBanquetSize(Banqueting.Level level)
    {
      int index1 = (int) level;
      int banquetSize = 0;
      for (int index2 = 0; index2 < 8; ++index2)
      {
        if (this.banquetLevels[index1, index2] > banquetSize)
          banquetSize = this.banquetLevels[index1, index2];
      }
      return banquetSize;
    }

    public static string getBanquetName(Banqueting.Level banquetLevel)
    {
      switch (banquetLevel)
      {
        case Banqueting.Level.HUMBLE:
          return SK.Text("BanquetScreen_Size_1", "Humble");
        case Banqueting.Level.MODEST:
          return SK.Text("BanquetScreen_Size_2", "Modest");
        case Banqueting.Level.FINE:
          return SK.Text("BanquetScreen_Size_3", "Fine");
        case Banqueting.Level.IMPRESSIVE:
          return SK.Text("BanquetScreen_Size_4", "Impressive");
        case Banqueting.Level.GRAND:
          return SK.Text("BanquetScreen_Size_5", "Grand");
        case Banqueting.Level.MAGNIFICENT:
          return SK.Text("BanquetScreen_Size_6", "Magnificent");
        case Banqueting.Level.MAJESTIC:
          return SK.Text("BanquetScreen_Size_7", "Majestic");
        case Banqueting.Level.EXQUISITE:
          return SK.Text("BanquetScreen_Size_8", "Exquisite");
        default:
          throw new ArgumentException("Invalid banquet type");
      }
    }

    public int getBanquetHonour(Banqueting.Level level)
    {
      return CardTypes.getBanquetHonourValue(this.getBanquetSize(level) * Banqueting.getHonourMultiplier(level), GameEngine.Instance.cardsManager.UserCardData);
    }

    public static int getHonourMultiplier(Banqueting.Level level)
    {
      int honourMultiplier = 1;
      switch (level)
      {
        case Banqueting.Level.HUMBLE:
          honourMultiplier = 1;
          break;
        case Banqueting.Level.MODEST:
          honourMultiplier = 4;
          break;
        case Banqueting.Level.FINE:
          honourMultiplier = 9;
          break;
        case Banqueting.Level.IMPRESSIVE:
          honourMultiplier = 16;
          break;
        case Banqueting.Level.GRAND:
          honourMultiplier = 35;
          break;
        case Banqueting.Level.MAGNIFICENT:
          honourMultiplier = 60;
          break;
        case Banqueting.Level.MAJESTIC:
          honourMultiplier = 98;
          break;
        case Banqueting.Level.EXQUISITE:
          honourMultiplier = 160;
          break;
      }
      if (GameEngine.Instance.World.ThirdAgeWorld)
        honourMultiplier *= 10;
      else if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        honourMultiplier *= 2;
      return honourMultiplier;
    }

    public bool holdBanquet(int mode)
    {
      bool flag = false;
      for (int index = 0; index < 8; ++index)
      {
        if (this.banquetLevels[mode, index] > 0)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        VillageMap village = GameEngine.Instance.Village;
        if (village != null)
          RemoteServices.Instance.VillageHoldBanquet(village.VillageID, this.banquetLevels[mode, 0], this.banquetLevels[mode, 1], this.banquetLevels[mode, 2], this.banquetLevels[mode, 3], this.banquetLevels[mode, 4], this.banquetLevels[mode, 5], this.banquetLevels[mode, 6], this.banquetLevels[mode, 7]);
      }
      return flag;
    }

    public int getTotalAvailableHonour()
    {
      int totalAvailableHonour = 0;
      for (int index = 0; index < 7; ++index)
        totalAvailableHonour += this.getBanquetHonour((Banqueting.Level) index);
      return totalAvailableHonour;
    }

    public void banquetCallback(VillageHoldBanquet_ReturnType returnData)
    {
      if (HoldBanquetPanel.Instance == null)
        return;
      HoldBanquetPanel.Instance.updateLevels(true);
    }

    public enum Level
    {
      HUMBLE,
      MODEST,
      FINE,
      IMPRESSIVE,
      GRAND,
      MAGNIFICENT,
      MAJESTIC,
      EXQUISITE,
    }
  }
}
