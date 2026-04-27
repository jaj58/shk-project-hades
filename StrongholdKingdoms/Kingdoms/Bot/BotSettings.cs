using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CommonTypes;

namespace Kingdoms.Bot
{
    [Serializable]
    public class BotSettings
    {
        [System.Xml.Serialization.XmlIgnore]
        public bool BotEnabled;
        public VillageSyncSettings VillageSync = new VillageSyncSettings();
        public RadarSettings Radar = new RadarSettings();
        public RecruitingSettings Recruiting = new RecruitingSettings();
        public CastleRepairSettings CastleRepair = new CastleRepairSettings();
        public TradeSettings Trade = new TradeSettings();
        public VillageBuilderSettings VillageBuilder = new VillageBuilderSettings();
        public AutoBombSettings AutoBomb = new AutoBombSettings();

        private static string GetSettingsFilePath()
        {
            string dir = GameEngine.getSettingsPath(true);
            return Path.Combine(dir, "bot_settings.xml");
        }

        public static BotSettings Load()
        {
            string path = GetSettingsFilePath();
            try
            {
                if (File.Exists(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(BotSettings));
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        BotSettings settings = (BotSettings)serializer.Deserialize(fs);
                        if (settings != null)
                            return settings;
                    }
                }
            }
            catch (Exception ex)
            {
                BotLogger.Log("BotSettings", BotLogLevel.Error, "Failed to load settings: " + ex.Message);
            }
            return new BotSettings();
        }

        public void Save()
        {
            string path = GetSettingsFilePath();
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BotSettings));
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(fs, this);
                }
            }
            catch (Exception ex)
            {
                BotLogger.Log("BotSettings", BotLogLevel.Error, "Failed to save settings: " + ex.Message);
            }
        }
    }

    [Serializable]
    public class VillageSyncSettings
    {
        public bool Enabled = true;
        public int IntervalSeconds = 120;
        public int DelayBetweenVillagesMs = 3000;
        public List<int> ExcludedVillageIds = new List<int>();

        public bool IsVillageEnabled(int villageId)
        {
            return !ExcludedVillageIds.Contains(villageId);
        }

        public void SetVillageEnabled(int villageId, bool enabled)
        {
            if (enabled)
                ExcludedVillageIds.Remove(villageId);
            else if (!ExcludedVillageIds.Contains(villageId))
                ExcludedVillageIds.Add(villageId);
        }
    }

    [Serializable]
    public class RadarSettings
    {
        public bool Enabled = true;
        public int ScanIntervalSeconds = 10;
        public string DiscordWebhookUrl = "";
        public int AutoInterdictMonkCount = 1;
        public bool AutoRecruitMonks = false;
        public int MinArmySizeForInterdict = 100;
        public int MinAttacksForInterdict = 0;
        public int MinAttacksWindowSeconds = 20;
        public int MaxLandTimeHours = 0;
        public bool ForceRefreshArmies = true;
        public List<RadarActionSettings> Actions = new List<RadarActionSettings>();

        public RadarActionSettings GetActionSettings(string actionKey)
        {
            foreach (RadarActionSettings a in Actions)
            {
                if (a.ActionKey == actionKey) return a;
            }
            RadarActionSettings newAction = new RadarActionSettings();
            newAction.ActionKey = actionKey;
            Actions.Add(newAction);
            return newAction;
        }
    }

    [Serializable]
    public class RadarActionSettings
    {
        public string ActionKey = "";
        public bool Monitor = true;
        public bool SystemNotify = true;
        public bool DiscordNotify = false;
        public bool AutoInterdict = false;
    }

    [Serializable]
    public class RecruitingSettings
    {
        public bool Enabled = true;
        public int CycleIntervalSeconds = 60;
        public int DelayBetweenVillagesMs = 3000;
        public List<int> ExcludedVillageIds = new List<int>();
        public List<VillageRecruitSettings> Villages = new List<VillageRecruitSettings>();
        public VassalRecruitingSettings VassalRecruiting = new VassalRecruitingSettings();

        public bool IsVillageEnabled(int villageId)
        {
            return !ExcludedVillageIds.Contains(villageId);
        }

        public void SetVillageEnabled(int villageId, bool enabled)
        {
            if (enabled)
                ExcludedVillageIds.Remove(villageId);
            else if (!ExcludedVillageIds.Contains(villageId))
                ExcludedVillageIds.Add(villageId);
        }

        public VillageRecruitSettings GetVillageSettings(int villageId)
        {
            foreach (VillageRecruitSettings v in Villages)
            {
                if (v.VillageId == villageId) return v;
            }
            VillageRecruitSettings newV = new VillageRecruitSettings();
            newV.VillageId = villageId;
            newV.InitDefaults();
            Villages.Add(newV);
            return newV;
        }
    }

    [Serializable]
    public class VillageRecruitSettings
    {
        public int VillageId;
        public List<UnitRecruitEntry> Units = new List<UnitRecruitEntry>();

        public void InitDefaults()
        {
            if (Units.Count > 0) return;
            string[] keys = new string[]
            {
                "Peasants", "Archers", "Pikemen", "Swordsmen",
                "Catapults", "Captains", "Scouts", "Monks", "Traders"
            };
            for (int i = 0; i < keys.Length; i++)
            {
                UnitRecruitEntry entry = new UnitRecruitEntry();
                entry.UnitKey = keys[i];
                entry.TargetCount = 0;
                entry.Priority = i + 1;
                Units.Add(entry);
            }
        }

        public UnitRecruitEntry GetEntry(string unitKey)
        {
            foreach (UnitRecruitEntry e in Units)
            {
                if (e.UnitKey == unitKey) return e;
            }
            UnitRecruitEntry newE = new UnitRecruitEntry();
            newE.UnitKey = unitKey;
            newE.TargetCount = 0;
            newE.Priority = Units.Count + 1;
            Units.Add(newE);
            return newE;
        }
    }

    [Serializable]
    public class UnitRecruitEntry
    {
        public string UnitKey = "";
        public int TargetCount;
        public int Priority = 1;
    }

    [Serializable]
    public class CastleRepairSettings
    {
        public bool Enabled;
        public int IntervalSeconds = 300;
        public int DelayBetweenVillagesMs = 5000;
        public bool RepairOnAttack;
        public List<VillageCastleRepairSettings> Villages = new List<VillageCastleRepairSettings>();

        public VillageCastleRepairSettings GetVillageSettings(int villageId)
        {
            foreach (VillageCastleRepairSettings v in Villages)
            {
                if (v.VillageId == villageId) return v;
            }
            VillageCastleRepairSettings newV = new VillageCastleRepairSettings();
            newV.VillageId = villageId;
            Villages.Add(newV);
            return newV;
        }
    }

    [Serializable]
    public class VillageCastleRepairSettings
    {
        public int VillageId;
        public bool RepairInfrastructure = true;
        public bool RepairTroops = true;
        public string LayoutSource = "Local";
        public string InfrastructurePresetName = "";
        public string TroopPresetName = "";
    }

    [Serializable]
    public class VassalRecruitingSettings
    {
        public int MinTroopsToSend = 1;
        public List<VassalVillageRecruitSettings> Vassals = new List<VassalVillageRecruitSettings>();

        public VassalVillageRecruitSettings GetVassalSettings(int vassalVillageId)
        {
            foreach (VassalVillageRecruitSettings v in Vassals)
            {
                if (v.VassalVillageId == vassalVillageId) return v;
            }
            VassalVillageRecruitSettings newV = new VassalVillageRecruitSettings();
            newV.VassalVillageId = vassalVillageId;
            newV.InitDefaults();
            Vassals.Add(newV);
            return newV;
        }

        public bool IsVassalEnabled(int vassalVillageId)
        {
            foreach (VassalVillageRecruitSettings v in Vassals)
            {
                if (v.VassalVillageId == vassalVillageId) return v.Enabled;
            }
            return false;
        }
    }

    [Serializable]
    public class VassalVillageRecruitSettings
    {
        public int VassalVillageId;
        public bool Enabled;
        public List<VassalUnitRecruitEntry> Units = new List<VassalUnitRecruitEntry>();

        public static readonly string[] VassalUnitKeys = new string[]
        {
            "Peasants", "Archers", "Pikemen", "Swordsmen", "Catapults"
        };

        public void InitDefaults()
        {
            if (Units.Count > 0) return;
            for (int i = 0; i < VassalUnitKeys.Length; i++)
            {
                VassalUnitRecruitEntry entry = new VassalUnitRecruitEntry();
                entry.UnitKey = VassalUnitKeys[i];
                entry.TargetCount = 0;
                entry.Priority = i + 1;
                Units.Add(entry);
            }
        }

        public VassalUnitRecruitEntry GetEntry(string unitKey)
        {
            foreach (VassalUnitRecruitEntry e in Units)
            {
                if (e.UnitKey == unitKey) return e;
            }
            VassalUnitRecruitEntry newE = new VassalUnitRecruitEntry();
            newE.UnitKey = unitKey;
            newE.TargetCount = 0;
            newE.Priority = Units.Count + 1;
            Units.Add(newE);
            return newE;
        }
    }

    [Serializable]
    public class VassalUnitRecruitEntry
    {
        public string UnitKey = "";
        public int TargetCount;
        public int Priority = 1;
    }

    [Serializable]
    public class TradeSettings
    {
        public bool Enabled = true;
        public int CycleIntervalSeconds = 120;
        public int DelayBetweenVillagesMs = 3000;
        public int MerchantsPerTrade = 1;
        public int MerchantsTradeLimit = 50;
        public int MerchantsExchangeLimit = 50;
        public bool AutoHireMerchants = false;
        public int AutoHireMerchantsLimit = 50;
        public bool IgnoreCurrentTransactions = false;
        public bool PrioritiseMarkets = true; // true = markets first, false = village routes first
        public bool DisableOnTradeCardExpiry = false;
        public List<VillageMarketTradeInfo> VillageMarketSettings = new List<VillageMarketTradeInfo>();
        public List<TradeRouteSettings> Routes = new List<TradeRouteSettings>();
        public List<PlayerTradeRouteSettings> PlayerRoutes = new List<PlayerTradeRouteSettings>();

        public VillageMarketTradeInfo GetVillageMarketInfo(int villageId)
        {
            foreach (VillageMarketTradeInfo v in VillageMarketSettings)
            {
                if (v.VillageId == villageId) return v;
            }
            VillageMarketTradeInfo newV = new VillageMarketTradeInfo();
            newV.VillageId = villageId;
            newV.InitDefaults();
            VillageMarketSettings.Add(newV);
            return newV;
        }
    }

    [Serializable]
    public class VillageMarketTradeInfo
    {
        public int VillageId;
        public bool IsTrading;
        public List<TradeTypeEntry> TradeTypes = new List<TradeTypeEntry>();
        public List<int> MarketTargets = new List<int>();

        public void InitDefaults()
        {
            if (TradeTypes.Count > 0) return;
            byte[] resourceIds = TradeModuleConstants.TradeTypeIds;
            for (int i = 0; i < resourceIds.Length; i++)
            {
                TradeTypeEntry entry = new TradeTypeEntry();
                entry.ResourceId = resourceIds[i];
                TradeTypes.Add(entry);
            }
        }

        public TradeTypeEntry GetTradeType(byte resourceId)
        {
            foreach (TradeTypeEntry e in TradeTypes)
            {
                if (e.ResourceId == resourceId) return e;
            }
            TradeTypeEntry newE = new TradeTypeEntry();
            newE.ResourceId = resourceId;
            TradeTypes.Add(newE);
            return newE;
        }

        public void CopySettingsFrom(VillageMarketTradeInfo source)
        {
            this.IsTrading = source.IsTrading;
            this.TradeTypes.Clear();
            foreach (TradeTypeEntry e in source.TradeTypes)
                this.TradeTypes.Add(e.Clone());
            this.MarketTargets.Clear();
            this.MarketTargets.AddRange(source.MarketTargets);
        }
    }

    [Serializable]
    public class TradeTypeEntry
    {
        public byte ResourceId;
        public bool Sell;
        public int MinSellPrice;
        public int SellLimit;
        public bool Buy;
        public int MaxBuyPrice = 150;
        public int BuyLimit;

        public TradeTypeEntry Clone()
        {
            TradeTypeEntry c = new TradeTypeEntry();
            c.ResourceId = this.ResourceId;
            c.Sell = this.Sell;
            c.MinSellPrice = this.MinSellPrice;
            c.SellLimit = this.SellLimit;
            c.Buy = this.Buy;
            c.MaxBuyPrice = this.MaxBuyPrice;
            c.BuyLimit = this.BuyLimit;
            return c;
        }
    }

    [Serializable]
    public class TradeRouteSettings
    {
        public string Name = "";
        public bool Enabled;
        public List<int> FromVillages = new List<int>();
        public List<int> ToVillages = new List<int>();
        public List<int> Resources = new List<int>();
        public int KeepMinimum;
        public int MaxMerchantsPerTransaction = 5;
        public int SendMaximum = 5000;
        public bool IsDistanceLimited;
        public int DistanceLimit = 100;
    }

    [Serializable]
    public class PlayerTradeRouteSettings
    {
        public string Name = "";
        public bool Enabled;
        public List<int> FromVillages = new List<int>();
        public int TargetVillageId;
        public List<PlayerTradeResourceEntry> Resources = new List<PlayerTradeResourceEntry>();
        public int KeepMinimum;
        public int MaxMerchantsPerTransaction = 50;

        public PlayerTradeResourceEntry GetResourceEntry(int resourceId)
        {
            foreach (PlayerTradeResourceEntry e in Resources)
            {
                if (e.ResourceId == resourceId) return e;
            }
            return null;
        }

        public bool IsComplete()
        {
            foreach (PlayerTradeResourceEntry e in Resources)
            {
                if (e.TotalAmount > 0 && e.AmountSent < e.TotalAmount) return false;
            }
            return Resources.Count > 0;
        }

        public void ResetProgress()
        {
            foreach (PlayerTradeResourceEntry e in Resources)
                e.AmountSent = 0;
        }
    }

    [Serializable]
    public class PlayerTradeResourceEntry
    {
        public int ResourceId;
        public int TotalAmount;
        public int AmountSent;

        public int Remaining
        {
            get { return Math.Max(0, TotalAmount - AmountSent); }
        }
    }

    public static class TradeModuleConstants
    {
        public static readonly byte[] TradeTypeIds = new byte[]
        {
            6, 7, 8, 9,         // Wood, Stone, Iron, Pitch
            12, 13, 14, 15,     // Ale, Apples, Bread, Veg
            16, 17, 18, 22,     // Meat, Cheese, Fish, Venison
            21, 26, 19, 33,     // Furniture, Metalware, Clothes, Wine
            23, 24, 25,         // Salt, Spices, Silk
            29, 28, 31, 30, 32  // Bows, Pikes, Armour, Swords, Catapults
        };

        public static readonly byte[] WeaponsTypeIds = new byte[]
        {
            29, 28, 31, 30, 32
        };

        public static bool IsWeapon(byte resourceId)
        {
            for (int i = 0; i < WeaponsTypeIds.Length; i++)
            {
                if (WeaponsTypeIds[i] == resourceId) return true;
            }
            return false;
        }

        public static string GetResourceName(int resourceId)
        {
            try
            {
                return VillageBuildingsData.getResourceNames(resourceId);
            }
            catch
            {
                return "Resource " + resourceId;
            }
        }
    }

    [Serializable]
    public class VillageBuilderSettings
    {
        public bool Enabled;
        public int CycleIntervalSeconds = 30;
        public int DelayBetweenVillagesMs = 5000;
        public bool WaitForResources = true;
        public List<VillageBuildLayout> Layouts = new List<VillageBuildLayout>();

        public VillageBuildLayout GetLayout(int villageId)
        {
            foreach (VillageBuildLayout l in Layouts)
            {
                if (l.VillageId == villageId) return l;
            }
            return null;
        }

        public VillageBuildLayout GetOrCreateLayout(int villageId)
        {
            VillageBuildLayout l = GetLayout(villageId);
            if (l == null)
            {
                l = new VillageBuildLayout();
                l.VillageId = villageId;
                Layouts.Add(l);
            }
            return l;
        }
    }

    [Serializable]
    public class VillageBuildLayout
    {
        public int VillageId;
        public bool Enabled;
        public List<BuildingEntry> Buildings = new List<BuildingEntry>();

        public void CopyBuildingsFrom(VillageBuildLayout source)
        {
            Buildings.Clear();
            foreach (BuildingEntry e in source.Buildings)
            {
                BuildingEntry copy = new BuildingEntry();
                copy.BuildingType = e.BuildingType;
                copy.X = e.X;
                copy.Y = e.Y;
                copy.Placed = false;
                copy.Status = "";
                Buildings.Add(copy);
            }
        }
    }

    [Serializable]
    public class BuildingEntry
    {
        public int BuildingType;
        public int X;
        public int Y;
        public bool Placed;
        [System.Xml.Serialization.XmlIgnore]
        public string Status = "";
    }

    // =========================================================================
    // Auto Bomb Settings
    // =========================================================================

    [Serializable]
    public class SavedArmyConfig
    {
        public int SourceVillageId;
        public string FormationName = "";
        public int Stack = 1;
        public int CardType;       // 0=None, 1=x2 Basic, 2=x4 Advanced, 3=x6 Expert
        public bool CaptainsOnly;
        public int AttackType = 11; // 11=Vandalise, 9=Raze, 1=Capture
        public bool Selected = true;
    }

    [Serializable]
    public class TargetQueueEntry
    {
        public int VillageId;
        public string Label = "";      // player name or custom label for display
        public bool Completed;
    }

    [Serializable]
    public class AutoBombSettings
    {
        public bool Enabled;
        public int TargetVillageId;
        public bool AutoCancelOnInterdict = true;
        public int StackDelaySeconds = 1;
        public bool FakeSendEnabled;
        public bool TargetQueueEnabled;
        public List<BombAttackEntry> PendingAttacks = new List<BombAttackEntry>();
        public List<SavedArmyConfig> SavedConfigs = new List<SavedArmyConfig>();
        public List<TargetQueueEntry> TargetQueue = new List<TargetQueueEntry>();
        public int InterdictCount;
    }

    [Serializable]
    public class BombAttackEntry
    {
        public int SourceVillageId;
        public int TargetVillageId;
        public int AttackType; // 3=Vandalise, 9=Raze, 1=Capture
        public string FormationName = "";
        public int Stack = 1;
        public double TravelTimeSeconds; // pre-calculated travel time to target
        public int NumPeasants;
        public int NumArchers;
        public int NumPikemen;
        public int NumSwordsmen;
        public int NumCatapults;
        public int NumCaptains;
        public bool CaptainsOnly;
        public int CardType; // 0=None, 1=x2 Basic, 2=x4 Advanced, 3=x6 Expert

        // Runtime state (not serialized)
        [System.Xml.Serialization.XmlIgnore]
        public string Status = "Queued";
        [System.Xml.Serialization.XmlIgnore]
        public DateTime ScheduledSendTime = DateTime.MaxValue;
        [System.Xml.Serialization.XmlIgnore]
        public DateTime EstimatedArrivalTime = DateTime.MaxValue;
        [System.Xml.Serialization.XmlIgnore]
        public bool Sent;
        [System.Xml.Serialization.XmlIgnore]
        public bool Cancelled;
        [System.Xml.Serialization.XmlIgnore]
        public bool PreparationStarted;
        [System.Xml.Serialization.XmlIgnore]
        public bool Prepared;
        [System.Xml.Serialization.XmlIgnore]
        public CastleMap PreparedCastleMap;
        [System.Xml.Serialization.XmlIgnore]
        public int PreparedAttackType;
        [System.Xml.Serialization.XmlIgnore]
        public int PreparedPillagePercent;

        public string GetSourceName()
        {
            try
            {
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                    return GameEngine.Instance.World.getVillageName(SourceVillageId);
            }
            catch { }
            return "Village " + SourceVillageId;
        }

        public static string GetAttackTypeName(int attackType)
        {
            switch (attackType)
            {
                case 3: return "Vandalise";
                case 9: return "Raze";
                case 1: return "Capture";
                default: return "Attack " + attackType;
            }
        }
    }
}
