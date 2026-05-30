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
        /// <summary>License key used to authenticate this install with the update server.</summary>
        public string LicenseKey = string.Empty;

        [System.Xml.Serialization.XmlIgnore]
        public bool BotEnabled;
        public VillageSyncSettings VillageSync = new VillageSyncSettings();
        public RadarSettings Radar = new RadarSettings();
        public RecruitingSettings Recruiting = new RecruitingSettings();
        public CastleRepairSettings CastleRepair = new CastleRepairSettings();
        public TradeSettings Trade = new TradeSettings();
        public VillageBuilderSettings VillageBuilder = new VillageBuilderSettings();
        public AutoBombSettings AutoBomb = new AutoBombSettings();
        public AutoBombMultiSettings AutoBombMulti = new AutoBombMultiSettings();
        public PopularitySettings Popularity = new PopularitySettings();
        public ScoutSettings Scout = new ScoutSettings();
        public MiscSettings Misc = new MiscSettings();
        public AutoSettings Auto = new AutoSettings();

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
                string msg = ex.Message;
                if (ex.InnerException != null) msg += " | Inner: " + ex.InnerException.Message;
                if (ex.InnerException?.InnerException != null) msg += " | Inner2: " + ex.InnerException.InnerException.Message;
                BotLogger.Log("BotSettings", BotLogLevel.Error, "Failed to save settings: " + msg);
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
        public string DiscordMentionTag = "";
        public GroupRadarSettings GroupRadar = new GroupRadarSettings();
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
    public class GroupRadarMember
    {
        public string PlayerName = "";
        public string DiscordTag = "";      // per-member @mention, e.g. <@123456789>
        public bool Enabled = true;
        public List<int> VillageIds = new List<int>();
    }

    [Serializable]
    public class GroupRadarSettings
    {
        public bool Enabled = false;
        public string DiscordWebhookUrl = "";
        public string DiscordMentionTag = "";   // group-level fallback mention
        public List<GroupRadarMember> Members = new List<GroupRadarMember>();
        public List<RadarActionSettings> Actions = new List<RadarActionSettings>();

        public RadarActionSettings GetActionSettings(string actionKey)
        {
            foreach (RadarActionSettings a in Actions)
                if (a.ActionKey == actionKey) return a;
            RadarActionSettings newAction = new RadarActionSettings();
            newAction.ActionKey = actionKey;
            Actions.Add(newAction);
            return newAction;
        }
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

    public enum TradePriority
    {
        MarketSellFirst = 0,   // Market first: sell before buy
        MarketBuyFirst  = 1,   // Market first: buy before sell
        VillageRoutes   = 2,   // Village routes first
        PlayerRoutes    = 3    // Player routes first
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
        public TradePriority Priority = TradePriority.MarketSellFirst;
        public bool DisableOnTradeCardExpiry = false;
        public bool TradeCardsWereActive = false;
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

        public TradeRouteSettings Clone()
        {
            TradeRouteSettings r = new TradeRouteSettings();
            r.Name = this.Name + " (Copy)";
            r.Enabled = false;
            r.FromVillages = new List<int>(this.FromVillages);
            r.ToVillages = new List<int>(this.ToVillages);
            r.Resources = new List<int>(this.Resources);
            r.KeepMinimum = this.KeepMinimum;
            r.MaxMerchantsPerTransaction = this.MaxMerchantsPerTransaction;
            r.SendMaximum = this.SendMaximum;
            r.IsDistanceLimited = this.IsDistanceLimited;
            r.DistanceLimit = this.DistanceLimit;
            return r;
        }
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

        public PlayerTradeRouteSettings Clone()
        {
            PlayerTradeRouteSettings r = new PlayerTradeRouteSettings();
            r.Name = this.Name + " (Copy)";
            r.Enabled = false;
            r.FromVillages = new List<int>(this.FromVillages);
            r.TargetVillageId = this.TargetVillageId;
            r.KeepMinimum = this.KeepMinimum;
            r.MaxMerchantsPerTransaction = this.MaxMerchantsPerTransaction;
            foreach (PlayerTradeResourceEntry e in this.Resources)
                r.Resources.Add(e.Clone());
            return r;
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

        public PlayerTradeResourceEntry Clone()
        {
            return new PlayerTradeResourceEntry
            {
                ResourceId = this.ResourceId,
                TotalAmount = this.TotalAmount,
                AmountSent = 0
            };
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
        public int ParentVillageId; // 0 = own village; lord village ID for vassal attacks
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

    // =========================================================================
    // Auto Bomb Multi Settings
    // =========================================================================

    [Serializable]
    public class MultiVillageInfo
    {
        public int VillageId;
        public string VillageName = "";
        public int NumPeasants;
        public int NumArchers;
        public int NumPikemen;
        public int NumSwordsmen;
        public int NumCatapults;
        public int NumCaptains;
        public double TravelTimeArmy;
        public double TravelTimeCaptain;
        public bool IsVassal;
        public int ParentVillageId;
        // Attack assignment set by coordinator (stored in API, mirrored here for display)
        [System.Xml.Serialization.XmlIgnore]
        public string FormationName = "";
        [System.Xml.Serialization.XmlIgnore]
        public int Stack = 1;
        [System.Xml.Serialization.XmlIgnore]
        public int CardType;
        [System.Xml.Serialization.XmlIgnore]
        public bool CaptainsOnly;
        [System.Xml.Serialization.XmlIgnore]
        public int AttackType = 11;
        [System.Xml.Serialization.XmlIgnore]
        public bool Selected = true;
        [System.Xml.Serialization.XmlIgnore]
        public string AttackStatus = ""; // queued/preparing/prepared/sent/cancelled/failed
    }

    [Serializable]
    public class MultiPlayerInfo
    {
        public string PlayerName = "";
        [System.Xml.Serialization.XmlIgnore]
        public bool IsCoordinator;
        [System.Xml.Serialization.XmlIgnore]
        public bool Ready;
        [System.Xml.Serialization.XmlIgnore]
        public List<MultiVillageInfo> Villages = new List<MultiVillageInfo>();
    }

    [Serializable]
    public class AutoBombMultiSettings
    {
        public bool Enabled;
        public string ApiUrl = "";
        public string SessionKey = "";
        public bool AutoCancelOnInterdict = true;
        public int StackDelaySeconds = 1;
        public bool FakeSendEnabled;
        public bool TargetQueueEnabled;
        // Force-refresh each attacking village ~5s before prepare to reduce server callback errors
        public bool PreRefreshVillages = true;
        public bool IncludeVassals;
        // Automatically play the correct speed card at T-3s before each attack send
        public bool PlayCards = false;
        // Cancel a wrong active speed card before playing the desired one
        public bool AutoCancelWrongCard = false;
        public List<TargetQueueEntry> TargetQueue = new List<TargetQueueEntry>();

        // Runtime state — not persisted
        [System.Xml.Serialization.XmlIgnore]
        public int TargetVillageId;
        [System.Xml.Serialization.XmlIgnore]
        public List<MultiPlayerInfo> ConnectedPlayers = new List<MultiPlayerInfo>();
        [System.Xml.Serialization.XmlIgnore]
        public string SessionState = "idle";
        [System.Xml.Serialization.XmlIgnore]
        public bool IsCoordinator;
        [System.Xml.Serialization.XmlIgnore]
        public bool InterdictDetected;
        [System.Xml.Serialization.XmlIgnore]
        public int InterdictCount;
        [System.Xml.Serialization.XmlIgnore]
        public double ServerClockOffsetSeconds;
    }

    public enum PopularityMode
    {
        Disabled = 0,
        MaxPopularity = 1,
        MaxGold = 2,
        Auto = 3
    }

    [Serializable]
    public class PopularitySettings
    {
        public bool Enabled = false;
        public int CycleIntervalSeconds = 120;
        public int DelayBetweenVillagesMs = 2000;
        public List<VillagePopularitySettings> Villages = new List<VillagePopularitySettings>();

        public VillagePopularitySettings GetVillageSettings(int villageId)
        {
            foreach (VillagePopularitySettings v in Villages)
            {
                if (v.VillageId == villageId) return v;
            }
            VillagePopularitySettings newV = new VillagePopularitySettings();
            newV.VillageId = villageId;
            Villages.Add(newV);
            return newV;
        }
    }

    [Serializable]
    public class VillagePopularitySettings
    {
        public int VillageId;
        public PopularityMode Mode = PopularityMode.Disabled;
    }

    [Serializable]
    public class MiscSettings
    {
        public bool CollectFreeCards = false;
        public bool DisableCannotPlayCardPopup = false;
    }

    // =========================================================================
    // Auto Tab Settings
    // =========================================================================

    [Serializable]
    public class AutoSettings
    {
        public bool Enabled = false;
        public List<ProductionCardSettings> ProductionCards = new List<ProductionCardSettings>();
        public List<ModuleScheduleSettings> ModuleSchedules = new List<ModuleScheduleSettings>();

        public ProductionCardSettings GetProduction(string goodKey)
        {
            foreach (ProductionCardSettings p in ProductionCards)
                if (p.GoodKey == goodKey) return p;
            ProductionCardSettings newP = new ProductionCardSettings();
            newP.GoodKey = goodKey;
            ProductionCards.Add(newP);
            return newP;
        }

        public ModuleScheduleSettings GetModuleSchedule(string moduleName)
        {
            foreach (ModuleScheduleSettings m in ModuleSchedules)
                if (m.ModuleName == moduleName) return m;
            ModuleScheduleSettings newM = new ModuleScheduleSettings();
            newM.ModuleName = moduleName;
            ModuleSchedules.Add(newM);
            return newM;
        }
    }

    [Serializable]
    public class ProductionCardSettings
    {
        public string GoodKey = "";
        public bool Enabled = false;
        public int CardFilterId = 0;     // cardFilter ID for this good type
        public int TierIndex = 0;        // 0=smallest (x3), 1=mid (x5), 2=largest (x10)
        public int TargetCount = 1;
        public int PlayedCount = 0;
        public int StartDelayMinutes = 0;
        public DateTime ScheduledStartTime = DateTime.MinValue;

        [System.Xml.Serialization.XmlIgnore]
        public int LastPlayedInstanceId = 0;
        [System.Xml.Serialization.XmlIgnore]
        public int PreviousTargetCount = -1;
    }

    [Serializable]
    public class ModuleScheduleSettings
    {
        public string ModuleName = "";
        public bool[] HourlySchedule = new bool[24];
        public bool AutoDisableEnabled = false;
        public bool PlayCardOnStart = false;
        public bool ReplayCardOnExpiry = true;  // Re-play card when it expires mid-window
        public int CardDefId = 0;
        public int CardDefId2 = 0;             // Second card (Trade: capacity; Scout: range/speed)
        public bool WasAutoStarted = false;
        public bool ManuallyDisabledDuringWindow = false;
        public int LastPlayedCardInstanceId = 0;
        public int LastPlayedCardInstanceId2 = 0; // Tracks second card instance
    }

    // =========================================================================
    // Scout Module Settings
    // =========================================================================

    public enum ScoutPriority
    {
        ResourcePriority = 0,  // type order in scout list first, distance as tiebreak
        RangePriority    = 1   // nearest stash first, regardless of type order
    }

    [Serializable]
    public class ScoutSettings
    {
        public bool Enabled = false;
        public int CycleIntervalSeconds = 60;
        public int MaxScoutTimeSeconds = 1200;
        public int AutoHireScouts = 0;          // 0 = disabled; 1-8 = target count (capped by Research_Scouts)
        public int DelayBetweenSendsMs = 3000;
        public bool DisableOnScoutCardExpiry = false;
        public bool ScoutCardsWereActive = false;
        public ScoutPriority Priority = ScoutPriority.ResourcePriority;
        public bool SendOneScout = false;
        public bool SendOneOnNewStash = true;
        public List<VillageScoutSettings> Villages = new List<VillageScoutSettings>();

        public VillageScoutSettings GetVillageSettings(int villageId)
        {
            foreach (VillageScoutSettings v in Villages)
            {
                if (v.VillageId == villageId) return v;
            }
            VillageScoutSettings newV = new VillageScoutSettings();
            newV.VillageId = villageId;
            newV.InitDefaults();
            Villages.Add(newV);
            return newV;
        }
    }

    [Serializable]
    public class VillageScoutSettings
    {
        public int VillageId;
        public bool ScoutingEnabled = true;
        public List<int> ResourceTypesToScout = new List<int>();
        public List<int> ResourceTypesToIgnore = new List<int>();

        public void InitDefaults()
        {
            if (ResourceTypesToScout.Count > 0) return;
            // Valid stash special IDs: TradeTypeIds + 100, plus 100 (new stash)
            int[] types = new int[]
            {
                100,                         // New Stash
                106, 107, 108, 109,          // Wood, Stone, Iron, Pitch
                112, 113, 114, 115,          // Ale, Apples, Bread, Vegetables
                116, 117, 118, 122,          // Meat, Cheese, Fish, Venison
                121, 126, 119, 133,          // Furniture, Metalware, Clothes, Wine
                123, 124, 125,               // Salt, Spices, Silk
                129, 128, 131, 130, 132      // Bows, Pikes, Armour, Swords, Catapults
            };
            foreach (int t in types)
                ResourceTypesToScout.Add(t);
        }
    }
}
