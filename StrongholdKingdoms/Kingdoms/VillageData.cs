// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageData
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;

//#nullable disable
namespace Kingdoms
{
  public class VillageData
  {
    public int id = -1;
    public short x = -1;
    public short y = -1;
    public short countyID = -1;
    public short regionID = -1;
    public bool regionCapital;
    public bool countyCapital;
    public bool provinceCapital;
    public bool countryCapital;
    public bool visible;
    public byte villageInfo;
    private VillageDataExtendedInfo m_extendedInfo;
    public bool vacationMode;
    public int factionID = 1;
    public int userID = 1;
    public int connecter = -1;
    public int special;
    public int userVillageID = -1;
    public string m_villageName = "";
    public short villageTerrain;
    public bool whiteName;
    public bool whiteFlags;
    public bool userRelatedVillage;
    public bool notDuplicate = true;
    public short numFlags;
    public WorldMap.VillageRolloverInfo rolloverInfo;

    public DateTime interdictionTime
    {
      get => this.m_extendedInfo == null ? DateTime.MinValue : this.m_extendedInfo.interdictionTime;
      set
      {
        if (!(value != DateTime.MinValue) && (this.m_extendedInfo == null || !(value == DateTime.MinValue)))
          return;
        if (this.m_extendedInfo == null)
          this.m_extendedInfo = new VillageDataExtendedInfo();
        this.m_extendedInfo.interdictionTime = value;
      }
    }

    public DateTime peaceTime
    {
      get => this.m_extendedInfo == null ? DateTime.MinValue : this.m_extendedInfo.peaceTime;
      set
      {
        if (!(value != DateTime.MinValue) && (this.m_extendedInfo == null || !(value == DateTime.MinValue)))
          return;
        if (this.m_extendedInfo == null)
          this.m_extendedInfo = new VillageDataExtendedInfo();
        this.m_extendedInfo.peaceTime = value;
      }
    }

    public DateTime excommunicationTime
    {
      get
      {
        return this.m_extendedInfo == null ? DateTime.MinValue : this.m_extendedInfo.excommunicationTime;
      }
      set
      {
        if (!(value != DateTime.MinValue) && (this.m_extendedInfo == null || !(value == DateTime.MinValue)))
          return;
        if (this.m_extendedInfo == null)
          this.m_extendedInfo = new VillageDataExtendedInfo();
        this.m_extendedInfo.excommunicationTime = value;
      }
    }

    public string villageName
    {
      get
      {
        return Program.mySettings.viewVillageIDs && !this.Capital || Program.mySettings.viewCapitalIDs && this.Capital ? "[" + this.id.ToString() + "] " + this.m_villageName : this.m_villageName;
      }
      set => this.m_villageName = value;
    }

    public bool Capital
    {
      get => this.regionCapital | this.countyCapital | this.provinceCapital | this.countryCapital;
    }

    public bool isAICastle
    {
      get => this.special == 7 || this.special == 11 || this.special == 9 || this.special == 13;
    }
  }
}
