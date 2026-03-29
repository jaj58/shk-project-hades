using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Kingdoms.Bot
{
    [Serializable]
    public class BotSettings
    {
        public bool BotEnabled;
        public VillageSyncSettings VillageSync = new VillageSyncSettings();
        public RadarSettings Radar = new RadarSettings();

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
        public int AutoInterdictMonkCount = 5;
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
}
