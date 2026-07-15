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
        [System.Xml.Serialization.XmlIgnore]
        private int _userId;
        [System.Xml.Serialization.XmlIgnore]
        private int _worldId;

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
        public BanquetSettings Banquet = new BanquetSettings();
        public DefenderSettings Defender = new DefenderSettings();
        public MonkSettings Monk = new MonkSettings();
        public AttackerSettings Attacker = new AttackerSettings();
        public TimingToolSettings TimingTool = new TimingToolSettings();

        // XmlSerializer generates and compiles a dynamic serialization assembly the first time
        // it is constructed for a given type. For a complex type like BotSettings this takes
        // several seconds in .NET Framework. Caching it statically means we pay that cost once
        // on first use (typically game startup) rather than on every world switch.
        private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(BotSettings));

        private static string GetGlobalSettingsPath()
        {
            return Path.Combine(GameEngine.getSettingsPath(true), "bot_settings.xml");
        }

        // Legacy world-only file from before player-scoping. Read-only default source —
        // never written to, so it stays an untouched backup after accounts fork off it.
        private static string GetLegacyWorldSettingsPath(int worldId)
        {
            return Path.Combine(GameEngine.getSettingsPath(true), "bot_settings_" + worldId + ".xml");
        }

        private static string GetPlayerSettingsPath(int userId, int worldId)
        {
            return Path.Combine(GameEngine.getSettingsPath(true),
                "bot_settings_" + userId + "_" + worldId + ".xml");
        }

        private static BotSettings DeserializeFrom(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        BotSettings settings = (BotSettings)_serializer.Deserialize(fs);
                        if (settings != null)
                            return settings;
                    }
                }
            }
            catch (Exception ex)
            {
                BotLogger.Log("BotSettings", BotLogLevel.Error, "Failed to load settings from '" + path + "': " + ex.Message);
            }
            return null;
        }

        public static BotSettings Load(int userId, int worldId)
        {
            // Pre-login / no identity → install-global file (license check path)
            if (userId <= 0 || worldId <= 0)
            {
                BotSettings g = DeserializeFrom(GetGlobalSettingsPath());
                if (g == null) g = new BotSettings();
                g._userId = userId;
                g._worldId = worldId;
                return g;
            }

            // Authoritative player-specific file
            BotSettings s = DeserializeFrom(GetPlayerSettingsPath(userId, worldId));
            if (s == null)
            {
                // Read-through: adopt the legacy world file as DEFAULTS (never written back).
                // The first Save() forks to the player-specific path automatically.
                s = DeserializeFrom(GetLegacyWorldSettingsPath(worldId));
                if (s == null)
                {
                    // Truly new — seed LicenseKey from the install-global file.
                    s = new BotSettings();
                    BotSettings global = DeserializeFrom(GetGlobalSettingsPath());
                    if (global != null) s.LicenseKey = global.LicenseKey;
                }
            }
            s._userId = userId;
            s._worldId = worldId;
            return s;
        }

        private void SerializeTo(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    _serializer.Serialize(fs, this);
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

        public void Save()
        {
            // Write to the player-specific file once we have a full identity; otherwise the
            // install-global file. This guarantees the legacy world file is never overwritten.
            string path = (_userId > 0 && _worldId > 0)
                ? GetPlayerSettingsPath(_userId, _worldId)
                : GetGlobalSettingsPath();
            SerializeTo(path);
        }
    }

    [Serializable]
    public class VillageSyncSettings
    {
        public bool Enabled = true;
        public int IntervalSeconds = 120;
        public int DelayBetweenVillagesMs = 3000;
        // When the Trade module hits a "not enough resources" rejection, the
        // village's local resource count was stale — force a full re-download of
        // just that village so the next cycle works from authoritative data.
        public bool AutoRefreshOnStaleError = true;
        // Periodically force a full (authoritative) re-download of every enabled
        // village, on top of the lightweight background sync. 0 = disabled.
        public int ForceRedownloadIntervalMinutes = 10;
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
        // When true, the min-army-size and max-land-time thresholds above also filter
        // Discord notifications (an event below the size threshold or landing beyond the
        // max land time is not sent to Discord). Min-attacks is interdict-only and never
        // affects notifications.
        public bool UseIgnoreOptionsForDiscord = false;
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
        public bool SoundNotify = false;
        public string SoundFile = "";
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
        // Auto-refresh each member's village list (the "Refresh All" action) without
        // having to click the button. RefreshOnStart re-resolves once when the radar
        // module starts (i.e. on map load); AutoRefreshIntervalMinutes re-resolves
        // periodically thereafter (0 = periodic disabled, start-refresh only).
        public bool RefreshOnStart = true;
        public int AutoRefreshIntervalMinutes = 60;
        // Discord-notification ignore options for group radar (notify-only, no interdict).
        // When UseIgnoreOptionsForDiscord is true, an incoming army below MinArmySize, or
        // landing beyond MaxLandTimeHours, is not sent to the group webhook. (0 = off.)
        public bool UseIgnoreOptionsForDiscord = false;
        public int MinArmySize = 0;
        public int MaxLandTimeHours = 0;
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
        // When enabled, disband units whose live count exceeds the per-village target down to it
        // (target 0 => disband all of that type). Special = Traders/Scouts/Monks; Troops = the
        // combat types. Both default OFF — disbanding is destructive, so it's strictly opt-in.
        public bool AutoDisbandSpecial = false;
        public bool AutoDisbandTroops = false;
        // Protect Captains from AutoDisbandTroops. Captains are expensive and rarely given a
        // target in the recruit grid (so they'd default to 0 = disband all), so this guards them
        // by default. Only relevant when AutoDisbandTroops is on.
        public bool AutoDisbandIgnoreCaptains = true;
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
        public int DisableAfterMinutes = 0;          // 0 = feature off; auto-disable module this many minutes after it was enabled
        public bool DisbandTradersOnDisable = false; // when auto-disabled, wait for traders to return home then disband them all
        public bool AutoSavePlayerRouteProgress = true;
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
    public class BuilderCategoryPref
    {
        public string Key = "";
        public bool Enabled = true;
    }

    [Serializable]
    public class VillageBuilderSettings
    {
        public bool Enabled;
        public int CycleIntervalSeconds = 30;
        public int DelayBetweenVillagesMs = 5000;
        public bool WaitForResources = true;
        public List<VillageBuildLayout> Layouts = new List<VillageBuildLayout>();

        // Ordered category priority for the builder (index 0 = highest). Unchecked
        // categories build last in layout order. Normalized against the module's
        // category catalog on use, so missing/unknown keys are harmless.
        public List<BuilderCategoryPref> CategoryPriority = new List<BuilderCategoryPref>();

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
        public string Label = "";      // village name or custom label for display
        public string OwnerName = "";  // owning player at the time the entry was added/validated
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
        // The target village this player's posted travel times were computed against.
        [System.Xml.Serialization.XmlIgnore]
        public int TravelTarget;
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
        // Launch timing mode: 0 = stack delay (arrival spacing by stack number),
        // 1 = manual per-village ± delay (stack spacing ignored; each row's delay shifts
        // its send: positive = sends earlier than exact = lands first).
        public int DelayMode = 0;
        public bool FakeSendEnabled;
        public bool TargetQueueEnabled;
        // Force-refresh each attacking village ~5s before prepare to reduce server callback errors
        public bool PreRefreshVillages = true;
        public bool IncludeVassals;
        // Automatically play the correct speed card at T-3s before each attack send
        public bool PlayCards = false;
        // Cancel a wrong active speed card before playing the desired one
        public bool AutoCancelWrongCard = false;
        // If a village can't fully fill its formation, send a partial attack with whatever
        // troops it has instead of failing/cancelling the batch.
        public bool SendWithMissingTroops = false;
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
        public bool ManualCancel;
        [System.Xml.Serialization.XmlIgnore]
        public int InterdictCount;
        [System.Xml.Serialization.XmlIgnore]
        public double ServerClockOffsetSeconds;
    }

    // =========================================================================
    // Timing Tool Settings
    // =========================================================================

    [Serializable]
    public class TimingToolSettings
    {
        public int TargetVillageId;
        public List<int> SourceVillages = new List<int>();
        public int MinNormalAttacks;
        public int MinCaptainAttacks;
        // Speed-card cardType ints to try: 1=x2, 2=x4, 3=x6, 5=x3, 6=x5.
        // cardType 0 (no card) is always tried in addition, regardless of this list.
        public List<int> AllowedCards = new List<int>();
        public int MaxAttackWindowSeconds = 60;
        // When enabled a village may supply (1 captain + 1 normal) OR (2 normal) attacks.
        // When disabled a village supplies at most one attack (captain OR normal).
        public bool VassalSending;
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
        public bool ShowOtherTraderInfo = false;
        public bool WorldMapParishBuildingCount = false;
        public bool ShowUserScreenInfo = false;
        public bool MapAttackTypeIcons = false;
        // 0 = Off (animated multi-stage zoom), 1 = Fast Village Switch (instant, keep zoom),
        // 2 = Auto Zoom when switching (instant, snap to max zoom)
        public int MapSwitchMode = 0;
        public bool ShowActiveEnemyCards = false;
        public bool ShowAllAttackTimes = false;
    }

    // =========================================================================
    // Auto Tab Settings
    // =========================================================================

    [Serializable]
    public class AutoSettings
    {
        public bool Enabled = false;
        public int CardCheckIntervalSeconds = 30;    // how often AutoCardModule scans production goods
        public int ModuleCheckIntervalSeconds = 60;  // how often AutoModuleSchedulerModule evaluates schedules
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
        public int TierIndex = 0;        // 0=smallest (x3), 1=mid (x5), 2=largest (x10)
        public int TargetCount = 1;
        public int PlayedCount = 0;
        public int StartDelayMinutes = 0;
        public DateTime ScheduledStartTime = DateTime.MinValue;

        [System.Xml.Serialization.XmlIgnore]
        public int LastPlayedInstanceId = 0;
        [System.Xml.Serialization.XmlIgnore]
        public int PreviousTargetCount = -1;
        // Set true once we've observed the played card become active — guards against counting a
        // card as "expired" during the brief window before UserCardData refreshes after a play.
        [System.Xml.Serialization.XmlIgnore]
        public bool ConfirmedActive = false;
        // Server time the current card was played, for the play-confirmation grace period.
        [System.Xml.Serialization.XmlIgnore]
        public DateTime PlayAttemptTime = DateTime.MinValue;
    }

    [Serializable]
    public class ModuleScheduleSettings
    {
        public string ModuleName = "";
        public bool[] HourlySchedule = new bool[24];
        public bool AutoDisableEnabled = false;
        public bool PlayCardOnStart = false;
        public bool ReplayCardOnExpiry = true;
        // Multi-select card list — replaces the old single/dual CardDefId fields
        public List<int> CardDefIds = new List<int>();
        // Legacy single/dual fields kept as XmlIgnore so old XML round-trips safely
        [System.Xml.Serialization.XmlIgnore] public int CardDefId  = 0;
        [System.Xml.Serialization.XmlIgnore] public int CardDefId2 = 0;
        public bool WasAutoStarted = false;
        public bool ManuallyDisabledDuringWindow = false;
        // Runtime-only tracking of active card instance IDs (not persisted)
        [System.Xml.Serialization.XmlIgnore]
        public List<int> LastPlayedCardInstanceIds = new List<int>();
        // Server time of the last card play, used to grace-gate replays against UserCardData lag.
        [System.Xml.Serialization.XmlIgnore]
        public DateTime LastCardPlayTime = DateTime.MinValue;
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
        public bool WaitForFreeSpace = true;   // skip/shrink sends when the village store is near cap
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
        public bool ScoutingEnabled = false;
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

    [Serializable]
    public class BanquetSettings
    {
        public bool Enabled = false;
        public int CycleIntervalSeconds = 300;
        public int DelayBetweenVillagesMs = 1500;
        public List<VillageBanquetSettings> Villages = new List<VillageBanquetSettings>();

        public VillageBanquetSettings GetVillageSettings(int villageId)
        {
            foreach (VillageBanquetSettings v in Villages)
                if (v.VillageId == villageId) return v;
            VillageBanquetSettings newV = new VillageBanquetSettings { VillageId = villageId };
            Villages.Add(newV);
            return newV;
        }
    }

    [Serializable]
    public class VillageBanquetSettings
    {
        public int VillageId;
        // Indices 0-7: Venison, Furniture, Metalware, Clothes, Wine, Salt, Spices, Silk
        public List<int> EnabledGoods = new List<int>();
    }

    // =========================================================================
    // Defender Module Settings
    // =========================================================================

    [Serializable]
    public class DefenderSettings
    {
        public bool Enabled = false;
        public int SpamDurationSeconds = 20;
        public int TargetVillageId = 0;
        public int KnightsCardDefId = 0;       // 0=None, 265=SA(2), 269=SA(5), 270=SA(12)
        public int LastStandCardDefId = 0;     // 0=None, 266=LS(5), 271=LS(10), 272=LS(20)
        public bool SpamDesperateDefence = false;
        public bool AutoRepair = true;
        public bool RestoreTroops = true;
        public bool RestoreInfrastructure = false;
    }

    // =========================================================================
    // Monk Module Settings
    // =========================================================================

    public enum MonkCommand
    {
        Blessing        = 1,   // parishes only
        Inquisition     = 3,   // parishes only
        Interdiction    = 4,   // villages + parish/county capitals
        Restoration     = 5,   // parishes only (heals disease)
        Absolution      = 6,   // villages only (removes excommunication)
        Excommunication = 7,   // villages only
        // TODO: verify the correct integer for influence/envoy monks
        Influence       = 2,
    }

    public enum MonkStopCondition
    {
        QuestCompletion = 0,   // sends until the active quest for this command is satisfied
        SendXMonksEach  = 1,   // sends ExtraParameter monks to each target then stops
        RunOnCondition  = 2,   // maintains a condition (hours of interdict, disease level, etc.)
    }

    [Serializable]
    public class MonkProgressEntry
    {
        public int TargetId;
        public int MonksSent;
    }

    [Serializable]
    public class MonkRouteSettings
    {
        public string Name = "";
        public bool Enabled = true;
        public MonkCommand Command = MonkCommand.Blessing;
        public List<int> FromVillages = new List<int>();
        public List<int> ToTargets = new List<int>();
        public MonkStopCondition StopCondition = MonkStopCondition.SendXMonksEach;
        // ExtraParameter meaning:
        //   SendXMonksEach            — monks to send per target
        //   RunOnCondition/Interdiction     — maintain at least X hours of interdict
        //   RunOnCondition/Restoration      — heal until disease <= X (0 = fully healed)
        //   RunOnCondition/Blessing         — maintain X blessing level (1–100)
        //   RunOnCondition/Inquisition      — maintain X inquisition level (1–100)
        //   RunOnCondition/Absolution       — absolve if target has > X hours of excomm
        //   RunOnCondition/Excommunication  — excommunicate until target has > X hours
        //   QuestCompletion           — unused (0)
        public int ExtraParameter = 5;
        public bool IsDistanceLimited;
        public int DistanceLimit = 100;
        // Influence-specific settings
        public bool InfluencePositive = true;
        public int InfluenceTargetUserId = -1;
        // Per-target progress for SendXMonksEach — persisted so it survives restarts
        public List<MonkProgressEntry> Progress = new List<MonkProgressEntry>();

        public int GetProgress(int targetId)
        {
            foreach (MonkProgressEntry e in Progress)
                if (e.TargetId == targetId) return e.MonksSent;
            return 0;
        }

        public void AddProgress(int targetId, int amount)
        {
            foreach (MonkProgressEntry e in Progress)
            {
                if (e.TargetId == targetId) { e.MonksSent += amount; return; }
            }
            Progress.Add(new MonkProgressEntry { TargetId = targetId, MonksSent = amount });
        }

        public void ResetProgress()
        {
            Progress.Clear();
        }

        public int GetTotalProgress()
        {
            int total = 0;
            foreach (MonkProgressEntry e in Progress)
                total += e.MonksSent;
            return total;
        }

        /// <summary>Returns a concise progress string for display in the route list row.</summary>
        public string GetProgressSummary()
        {
            int total = GetTotalProgress();
            if (StopCondition == MonkStopCondition.SendXMonksEach)
            {
                // Always show "sent / needed" so the target is visible even before anything runs
                if (ToTargets.Count == 0) return "-";
                int cap = ExtraParameter * ToTargets.Count;
                return total + " / " + cap;
            }
            // QuestCompletion / RunOnCondition — show total monks dispatched
            return total == 0 ? "-" : total + " sent";
        }

        public MonkRouteSettings Clone()
        {
            MonkRouteSettings r = new MonkRouteSettings();
            r.Name = this.Name + " (Copy)";
            r.Enabled = false;
            r.Command = this.Command;
            r.FromVillages = new List<int>(this.FromVillages);
            r.ToTargets = new List<int>(this.ToTargets);
            r.StopCondition = this.StopCondition;
            r.ExtraParameter = this.ExtraParameter;
            r.IsDistanceLimited = this.IsDistanceLimited;
            r.DistanceLimit = this.DistanceLimit;
            r.InfluencePositive = this.InfluencePositive;
            r.InfluenceTargetUserId = this.InfluenceTargetUserId;
            return r;
        }
    }

    [Serializable]
    public class MonkSettings
    {
        public bool Enabled = false;
        public int CycleIntervalSeconds = 120;
        public int DelayBetweenRoutesMs = 2000;
        public int MonksToKeep = 0;
        // 0 = disabled; 1-8 = recruit monks at each from-village up to this count
        // before sending. Capped by ordination research level at runtime.
        public int AutoRecruitMonks = 0;
        public List<MonkRouteSettings> Routes = new List<MonkRouteSettings>();
    }

    [Serializable]
    public class AttackerSettings
    {
        public bool Enabled = false;
        public int CycleIntervalSeconds = 30;

        // World-map button visibility
        public bool ShowAttackButton = false;
        public bool ShowMonksButton = false;
        public bool ForceMode = false; // true = immediate attack; false = queue

        // Monk send quantities
        public int AbsMonkCount = 1;
        public int ExcomMonkCount = 1;

        // Per-target-type formation profiles.
        // Attack type integers: 1=Capture, 2=Pillage, 3=Ransack, 9=Raze, 11=Vandalise, 12=GoldRaid
        public string DistrictFormationName = "";
        public int DistrictAttackType = 11;
        public int DistrictPillagePercent = 25; // only used when DistrictAttackType == GoldRaid; valid range 1-50

        public string AiFormationName = "";
        public int AiAttackType = 11; // forced to Vandalise — AI/Special targets only support Vandalise
        public int AiPillagePercent = 0; // unused — kept for settings compatibility

        public string EnemyFormationName = "";
        public int EnemyAttackType = 11;
        public int EnemyPillagePercent = 0;
    }
}
