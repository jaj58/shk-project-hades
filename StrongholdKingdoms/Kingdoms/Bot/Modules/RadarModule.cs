using System;
using System.Collections;
using System.Collections.Generic;

namespace Kingdoms.Bot.Modules
{
    public class RadarModule : BotModuleBase
    {
        public const string ACTION_ATTACK = "Attack";
        public const string ACTION_SCOUT = "Scout";
        public const string ACTION_CAPTAIN = "Captain";
        public const string ACTION_MONK = "Monk";
        public const string ACTION_MONK_PARISH = "MonkParish";
        public const string ACTION_MONK_COUNTY = "MonkCounty";
        public const string ACTION_ATTACK_PARISH = "AttackParish";
        public const string ACTION_ATTACK_COUNTY = "AttackCounty";
        public const string ACTION_ATTACK_PROVINCE = "AttackProvince";
        public const string ACTION_ATTACK_COUNTRY = "AttackCountry";
        public const string ACTION_REINFORCEMENT = "Reinforcement";
        public const string ACTION_FORAGING = "Foraging";

        public static readonly string[] AllActionKeys = new string[]
        {
            ACTION_ATTACK,
            ACTION_SCOUT,
            ACTION_CAPTAIN,
            ACTION_MONK,
            ACTION_MONK_PARISH,
            ACTION_MONK_COUNTY,
            ACTION_ATTACK_PARISH,
            ACTION_ATTACK_COUNTY,
            ACTION_ATTACK_PROVINCE,
            ACTION_ATTACK_COUNTRY,
            ACTION_REINFORCEMENT,
            ACTION_FORAGING
        };

        private Dictionary<long, bool> _knownArmyIds = new Dictionary<long, bool>();
        private Dictionary<long, bool> _knownPersonIds = new Dictionary<long, bool>();
        private bool _firstScan = true;

        public override string ModuleName
        {
            get { return "Radar"; }
        }

        public override TimeSpan Interval
        {
            get
            {
                int secs = 10;
                if (Engine != null && Engine.Settings != null)
                    secs = Engine.Settings.Radar.ScanIntervalSeconds;
                if (secs < 3) secs = 3;
                return TimeSpan.FromSeconds(secs);
            }
        }

        protected override void OnInitialize()
        {
            _knownArmyIds.Clear();
            _knownPersonIds.Clear();
            _firstScan = true;
        }

        protected override void OnTick()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            RadarSettings settings = (Engine != null && Engine.Settings != null)
                ? Engine.Settings.Radar
                : null;

            if (settings == null) return;

            ScanArmies(settings);
            ScanPeople(settings);

            if (_firstScan)
            {
                _firstScan = false;
                LogInfo("Initial scan complete. Tracking " + _knownArmyIds.Count +
                        " armies and " + _knownPersonIds.Count + " people.");
            }
        }

        private void ScanArmies(RadarSettings settings)
        {
            SparseArray armyArray = GameEngine.Instance.World.getArmyArray();
            if (armyArray == null) return;

            Dictionary<long, bool> currentIds = new Dictionary<long, bool>();

            foreach (WorldMap.LocalArmyData army in armyArray)
            {
                currentIds[army.armyID] = true;

                if (_knownArmyIds.ContainsKey(army.armyID))
                    continue;

                _knownArmyIds[army.armyID] = true;

                if (_firstScan)
                    continue;

                // Only care about armies targeting user's villages
                if (!IsTargetingUser(army.targetVillageID))
                    continue;

                // Returning armies are not threats
                if (army.lootType >= 0)
                    continue;

                string actionKey = ClassifyArmy(army);
                if (actionKey == null) continue;

                RadarActionSettings actionSettings = settings.GetActionSettings(actionKey);
                if (!actionSettings.Monitor) continue;

                string sourceName = GetVillageName(army.travelFromVillageID);
                string targetName = GetVillageName(army.targetVillageID);
                string timeLeft = GetTimeLeft(army.serverEndTime);
                string actionLabel = GetActionLabel(actionKey);

                string message = actionLabel + " detected!\n" +
                    "From: " + sourceName + " [" + army.travelFromVillageID + "]\n" +
                    "To: " + targetName + " [" + army.targetVillageID + "]\n" +
                    "ETA: " + timeLeft;

                string troopInfo = GetTroopInfo(army);
                if (troopInfo.Length > 0)
                    message += "\n" + troopInfo;

                LogWarning(message.Replace("\n", " | "));

                if (actionSettings.SystemNotify)
                    ShowSystemNotification(actionLabel, message);

                if (actionSettings.DiscordNotify && !string.IsNullOrEmpty(settings.DiscordWebhookUrl))
                    DiscordNotifier.SendAsync(settings.DiscordWebhookUrl,
                        "\u26A0 " + actionLabel + " Incoming!", message, 16736352);

                if (actionSettings.AutoInterdict && settings.AutoInterdictMonkCount > 0)
                    TryAutoInterdict(army.targetVillageID, settings);
            }

            // Clean up stale IDs
            List<long> staleIds = new List<long>();
            foreach (long id in _knownArmyIds.Keys)
            {
                if (!currentIds.ContainsKey(id))
                    staleIds.Add(id);
            }
            foreach (long id in staleIds)
                _knownArmyIds.Remove(id);
        }

        private void ScanPeople(RadarSettings settings)
        {
            SparseArray peopleArray = GameEngine.Instance.World.getPeopleArray();
            if (peopleArray == null) return;

            Dictionary<long, bool> currentIds = new Dictionary<long, bool>();

            foreach (WorldMap.LocalPerson person in peopleArray)
            {
                long personId = person.person.personID;
                currentIds[personId] = true;

                if (_knownPersonIds.ContainsKey(personId))
                    continue;

                _knownPersonIds[personId] = true;

                if (_firstScan)
                    continue;

                int targetVillage = person.person.targetVillageID;
                if (!IsTargetingUser(targetVillage))
                    continue;

                // Monks heading toward user
                string actionKey = ClassifyPerson(person, targetVillage);
                if (actionKey == null) continue;

                RadarActionSettings actionSettings = settings.GetActionSettings(actionKey);
                if (!actionSettings.Monitor) continue;

                string sourceName = GetVillageName(person.person.homeVillageID);
                string targetName = GetVillageName(targetVillage);
                string actionLabel = GetActionLabel(actionKey);

                string message = actionLabel + " detected!\n" +
                    "From: " + sourceName + " [" + person.person.homeVillageID + "]\n" +
                    "To: " + targetName + " [" + targetVillage + "]";

                LogWarning(message.Replace("\n", " | "));

                if (actionSettings.SystemNotify)
                    ShowSystemNotification(actionLabel, message);

                if (actionSettings.DiscordNotify && !string.IsNullOrEmpty(settings.DiscordWebhookUrl))
                    DiscordNotifier.SendAsync(settings.DiscordWebhookUrl,
                        "\u26A0 " + actionLabel + " Incoming!", message, 15105570);
            }

            List<long> staleIds = new List<long>();
            foreach (long id in _knownPersonIds.Keys)
            {
                if (!currentIds.ContainsKey(id))
                    staleIds.Add(id);
            }
            foreach (long id in staleIds)
                _knownPersonIds.Remove(id);
        }

        private bool IsTargetingUser(int villageId)
        {
            if (villageId < 0) return false;
            return GameEngine.Instance.World.isUserVillage(villageId);
        }

        private string ClassifyArmy(WorldMap.LocalArmyData army)
        {
            int targetVillage = army.targetVillageID;
            VillageData vData = GameEngine.Instance.World.getVillageData(targetVillage);

            // Scout-only armies (attackType 11 = scout)
            if (army.attackType == 11)
                return ACTION_SCOUT;

            // Captain-only (attackType 12 = captain)
            if (army.attackType == 12)
                return ACTION_CAPTAIN;

            // Foraging (attackType 9)
            if (army.attackType == 9)
                return ACTION_FORAGING;

            // Reinforcements (attackType 13 = tutorial/reinforcement marker)
            if (army.attackType == 13)
                return ACTION_REINFORCEMENT;

            // Capital-level attacks
            if (vData != null)
            {
                if (vData.countryCapital)
                    return ACTION_ATTACK_COUNTRY;
                if (vData.provinceCapital)
                    return ACTION_ATTACK_PROVINCE;
                if (vData.countyCapital)
                    return ACTION_ATTACK_COUNTY;
                if (vData.regionCapital)
                    return ACTION_ATTACK_PARISH;
            }

            // Generic attack (attackType 1=normal, 2=pillage, 3=ransack, 4=raze, 5=vandalise, 6..7=siege)
            return ACTION_ATTACK;
        }

        private string ClassifyPerson(WorldMap.LocalPerson person, int targetVillage)
        {
            if (person.person.personType != 4)
                return null; // Only monks

            VillageData vData = GameEngine.Instance.World.getVillageData(targetVillage);
            if (vData != null)
            {
                if (vData.countyCapital || vData.provinceCapital || vData.countryCapital)
                    return ACTION_MONK_COUNTY;
                if (vData.regionCapital)
                    return ACTION_MONK_PARISH;
            }

            return ACTION_MONK;
        }

        private void TryAutoInterdict(int targetVillageId, RadarSettings settings)
        {
            if (settings.AutoInterdictMonkCount <= 0)
                return;

            if (!GameEngine.Instance.World.isUserVillage(targetVillageId))
            {
                LogWarning("Auto-interdict: target village [" + targetVillageId + "] is not owned by user.");
                return;
            }

            try
            {
                int monkCount = settings.AutoInterdictMonkCount;
                // command 4 = interdict, data = -1
                RemoteServices.Instance.SendPeople(targetVillageId, targetVillageId, 4, monkCount, 4, -1);
                LogInfo("Auto-interdict sent " + monkCount + " monks from [" + targetVillageId +
                        "] to [" + targetVillageId + "]");
            }
            catch (Exception ex)
            {
                LogError("Auto-interdict failed: " + ex.Message);
            }
        }

        private static void ShowSystemNotification(string title, string body)
        {
            try
            {
                System.Windows.Forms.NotifyIcon notify = new System.Windows.Forms.NotifyIcon();
                notify.Icon = System.Drawing.SystemIcons.Warning;
                notify.Visible = true;
                notify.BalloonTipTitle = "Hades Radar: " + title;
                notify.BalloonTipText = body.Replace("\n", " | ");
                notify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
                notify.ShowBalloonTip(5000);
                System.Windows.Forms.Timer disposeTimer = new System.Windows.Forms.Timer();
                disposeTimer.Interval = 6000;
                disposeTimer.Tick += delegate
                {
                    notify.Visible = false;
                    notify.Dispose();
                    disposeTimer.Stop();
                    disposeTimer.Dispose();
                };
                disposeTimer.Start();
            }
            catch
            {
            }
        }

        private static string GetVillageName(int villageId)
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                return GameEngine.Instance.World.getVillageName(villageId);
            return "Unknown (" + villageId + ")";
        }

        private static string GetTimeLeft(DateTime arrivalTime)
        {
            TimeSpan remaining = arrivalTime - VillageMap.getCurrentServerTime();
            if (remaining.TotalSeconds < 1)
                return "Arrived";
            if (remaining.TotalHours >= 1)
                return ((int)remaining.TotalHours) + "h " + remaining.Minutes + "m " + remaining.Seconds + "s";
            if (remaining.TotalMinutes >= 1)
                return remaining.Minutes + "m " + remaining.Seconds + "s";
            return remaining.Seconds + "s";
        }

        private static string GetTroopInfo(WorldMap.LocalArmyData army)
        {
            List<string> parts = new List<string>();
            if (army.numPeasants > 0) parts.Add("Peasants:" + army.numPeasants);
            if (army.numArchers > 0) parts.Add("Archers:" + army.numArchers);
            if (army.numPikemen > 0) parts.Add("Pikemen:" + army.numPikemen);
            if (army.numSwordsmen > 0) parts.Add("Swords:" + army.numSwordsmen);
            if (army.numCatapults > 0) parts.Add("Catapults:" + army.numCatapults);
            if (army.numCaptains > 0) parts.Add("Captains:" + army.numCaptains);
            if (army.numScouts > 0) parts.Add("Scouts:" + army.numScouts);
            if (parts.Count == 0) return "";
            return string.Join(" | ", parts.ToArray());
        }

        public static string GetActionLabel(string actionKey)
        {
            switch (actionKey)
            {
                case ACTION_ATTACK: return "Attack";
                case ACTION_SCOUT: return "Scout";
                case ACTION_CAPTAIN: return "Captain";
                case ACTION_MONK: return "Monk";
                case ACTION_MONK_PARISH: return "Monk (Parish)";
                case ACTION_MONK_COUNTY: return "Monk (County+)";
                case ACTION_ATTACK_PARISH: return "Attack (Parish)";
                case ACTION_ATTACK_COUNTY: return "Attack (County)";
                case ACTION_ATTACK_PROVINCE: return "Attack (Province)";
                case ACTION_ATTACK_COUNTRY: return "Attack (Country)";
                case ACTION_REINFORCEMENT: return "Reinforcement";
                case ACTION_FORAGING: return "Foraging";
                default: return actionKey;
            }
        }

        protected override void OnShutdown()
        {
            _knownArmyIds.Clear();
            _knownPersonIds.Clear();
        }
    }
}
