// Decompiled with JetBrains decompiler
// Type: Kingdoms.WorldMapFilter
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;

//#nullable disable
namespace Kingdoms
{
  public class WorldMapFilter
  {
    public const int MAPFILTER_OFF = 0;
    public const int MAPFILTER_PRESET_YOUR_FACTION = 1;
    public const int MAPFILTER_PRESET_YOUR_HOUSE = 2;
    public const int MAPFILTER_PRESET_FORAGING = 3;
    public const int MAPFILTER_PRESET_TRADERS = 4;
    public const int MAPFILTER_PRESET_MARKETS = 5;
    public const int MAPFILTER_PRESET_ATTACKS = 6;
    public const int MAPFILTER_PRESET_OPEN_FACTIONS = 7;
    public const int MAPFILTER_PRESET_AI_ONLY = 8;
    public const int MAPFILTER_PRESET_ATTACK_FROM_VASSAL = 9;
    public const int MAPFILTER_PRESET_COUNTY_CAPITALS = 10;
    public const int MAPFILTER_PRESET_HIDE_ALL = 11;
    public const int MAPFILTER_CUSTOM = 10000;
    private bool filterActive;
    private int filterMode;
    private bool filterAlwaysShowYourVillages = true;
    private bool filterShowHouseSymbols = true;
    private bool filterShowFactionSymbols = true;
    private bool filterShowUserSymbols = true;

    public bool FilterActive
    {
      get => this.filterActive;
      set => this.filterActive = value;
    }

    public int FilterMode => this.filterMode;

    public bool FilterAlwaysShowYourVillages
    {
      get => this.filterAlwaysShowYourVillages;
      set => this.filterAlwaysShowYourVillages = value;
    }

    public bool FilterShowHouseSymbols
    {
      get => this.filterShowHouseSymbols;
      set => this.filterShowHouseSymbols = value;
    }

    public bool FilterShowFactionSymbols
    {
      get => this.filterShowFactionSymbols;
      set => this.filterShowFactionSymbols = value;
    }

    public bool FilterShowUserSymbols
    {
      get => this.filterShowUserSymbols;
      set => this.filterShowUserSymbols = value;
    }

    public void setFilterMode(int mode)
    {
      if (mode == 0)
      {
        this.FilterActive = false;
      }
      else
      {
        this.FilterActive = true;
        this.filterMode = mode;
      }
    }

    private bool visibleUnderAIFilter(VillageData village)
    {
      switch (village.special)
      {
        case 3:
        case 5:
        case 7:
        case 9:
        case 11:
        case 13:
        case 15:
        case 17:
          return true;
        default:
          return SpecialVillageTypes.IS_TREASURE_CASTLE(village.special) || SpecialVillageTypes.IS_ROYAL_TOWER(village.special);
      }
    }

    public bool showVillage(VillageData village)
    {
      if (!this.FilterActive || InterfaceMgr.Instance.WorldMapMode != 0 || this.filterAlwaysShowYourVillages && village.userID == RemoteServices.Instance.UserID)
        return true;
      switch (this.filterMode)
      {
        case 1:
          if (village.userID < 0)
            return false;
          if (village.userID == RemoteServices.Instance.UserID)
            return true;
          int userFactionId1 = RemoteServices.Instance.UserFactionID;
          return userFactionId1 >= 0 && village.factionID >= 0 && village.factionID == userFactionId1;
        case 2:
          if (village.userID < 0)
            return false;
          if (village.userID == RemoteServices.Instance.UserID)
            return true;
          int userFactionId2 = RemoteServices.Instance.UserFactionID;
          if (userFactionId2 < 0 || village.factionID < 0)
            return false;
          if (village.factionID == userFactionId2)
            return true;
          FactionData faction1 = GameEngine.Instance.World.getFaction(userFactionId2);
          FactionData faction2 = GameEngine.Instance.World.getFaction(village.factionID);
          return faction1 != null && faction2 != null && faction1.houseID == faction2.houseID && faction1.houseID != 0;
        case 3:
          if (GameEngine.Instance.World.isForagingSpecial(village.id) || GameEngine.Instance.World.isForagingVillage(village.id))
            return true;
          break;
        case 4:
        case 5:
          if (GameEngine.Instance.World.isVillageTrading(village.id) || village.Capital || GameEngine.Instance.World.isVillageMarketTrading(village.id))
            return true;
          break;
        case 6:
          if (GameEngine.Instance.World.isVillageInvolvedInAttacks(village.id))
            return true;
          break;
        case 7:
          if (village.userID < 0)
            return false;
          if (village.userID == RemoteServices.Instance.UserID)
            return true;
          if (RemoteServices.Instance.UserFactionID >= 0 || village.factionID < 0)
            return false;
          FactionData faction3 = GameEngine.Instance.World.getFaction(village.factionID);
          return faction3 != null && faction3.openForApplications;
        case 8:
          return GameEngine.Instance.World.isVillageInvolvedInAIAttacks(village.id) || this.visibleUnderAIFilter(village);
        case 9:
          if (this.visibleUnderAIFilter(village) || !village.Capital && village.userID > 0)
            return true;
          break;
        case 10:
          if (village.countyCapital)
            return true;
          break;
        case 11:
          return false;
      }
      return false;
    }

    public int showVillage(int villageID)
    {
      if (!this.FilterActive || InterfaceMgr.Instance.WorldMapMode != 0)
        return villageID;
      VillageData villageData = GameEngine.Instance.World.getVillageData(villageID);
      return villageData == null || !this.showVillage(villageData) ? -1 : villageID;
    }

    public bool showArmy(WorldMap.LocalArmyData army)
    {
      if (!this.FilterActive || InterfaceMgr.Instance.WorldMapMode != 0)
        return true;
      switch (this.filterMode)
      {
        case 3:
          if (GameEngine.Instance.World.isForagingArmy(army.armyID))
            return true;
          break;
        case 6:
          if (GameEngine.Instance.World.isAttackingArmy(army.armyID))
            return true;
          break;
        case 8:
          if (army.lootType < 0)
          {
            VillageData villageData = GameEngine.Instance.World.getVillageData(army.targetVillageID);
            if (villageData != null)
            {
              switch (villageData.special)
              {
                case 3:
                case 5:
                case 7:
                case 9:
                case 11:
                case 13:
                case 15:
                case 17:
                  return true;
                default:
                  if (SpecialVillageTypes.IS_TREASURE_CASTLE(villageData.special) || SpecialVillageTypes.IS_ROYAL_TOWER(villageData.special))
                    return true;
                  break;
              }
            }
            else
              break;
          }
          else
            break;
          break;
      }
      return false;
    }

    public bool showReinforcements(WorldMap.LocalArmyData army)
    {
      return !this.FilterActive || InterfaceMgr.Instance.WorldMapMode != 0;
    }

    public long showReinforcements(long reinfID)
    {
      if (!this.FilterActive || InterfaceMgr.Instance.WorldMapMode != 0)
        return reinfID;
      WorldMap.LocalArmyData reinforcement = GameEngine.Instance.World.getReinforcement(reinfID);
      return reinforcement == null || !this.showReinforcements(reinforcement) ? -1L : reinfID;
    }

    public bool showPeople(WorldMap.LocalPerson person)
    {
      return !this.FilterActive || InterfaceMgr.Instance.WorldMapMode != 0;
    }

    public long showPeople(long personID)
    {
      if (!this.FilterActive || InterfaceMgr.Instance.WorldMapMode != 0)
        return personID;
      WorldMap.LocalPerson person = GameEngine.Instance.World.getPerson(personID);
      return person == null || !this.showPeople(person) ? -1L : personID;
    }

    public bool showTrader(WorldMap.LocalTrader trader)
    {
      if (!this.FilterActive || InterfaceMgr.Instance.WorldMapMode != 0)
        return true;
      switch (this.filterMode)
      {
        case 4:
        case 5:
          if (trader.trader.traderState == 1 || trader.trader.traderState == 2 || trader.trader.traderState > 2 && trader.trader.traderState <= 6)
            return true;
          break;
      }
      return false;
    }

    public long showTrader(long traderID)
    {
      if (!this.FilterActive || InterfaceMgr.Instance.WorldMapMode != 0)
        return traderID;
      WorldMap.LocalTrader trader = GameEngine.Instance.World.getTrader(traderID);
      return trader == null || !this.showTrader(trader) ? -1L : traderID;
    }
  }
}
