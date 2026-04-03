using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

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
}
