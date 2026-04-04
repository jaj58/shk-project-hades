using System;
using System.Collections;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class RadarModule : BotModuleBase
    {
        // Action keys for radar event categories
        public const string ACTION_CAPTURE = "Capture";
        public const string ACTION_RAZE = "Raze";
        public const string ACTION_RANSACK = "Ransack";
        public const string ACTION_PILLAGE_STOCKPILE = "PillageStockpile";
        public const string ACTION_PILLAGE_GRANARY = "PillageGranary";
        public const string ACTION_PILLAGE_BANQUET = "PillageBanquet";
        public const string ACTION_PILLAGE_ALE = "PillageAle";
        public const string ACTION_PILLAGE_ARMOURY = "PillageArmoury";
        public const string ACTION_VANDALISE = "Vandalise";
        public const string ACTION_GOLD_RAID = "GoldRaid";
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
            ACTION_CAPTURE,
            ACTION_RAZE,
            ACTION_RANSACK,
            ACTION_PILLAGE_STOCKPILE,
            ACTION_PILLAGE_GRANARY,
            ACTION_PILLAGE_BANQUET,
            ACTION_PILLAGE_ALE,
            ACTION_PILLAGE_ARMOURY,
            ACTION_VANDALISE,
            ACTION_GOLD_RAID,
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

        // Attack type constants matching the game
        private const int ATTACK_TYPE_CAPTURE = 1;
        private const int ATTACK_TYPE_PILLAGE_STOCKPILE = 2;
        private const int ATTACK_TYPE_RANSACK = 3;
        private const int ATTACK_TYPE_PILLAGE_GRANARY = 4;
        private const int ATTACK_TYPE_PILLAGE_BANQUET = 5;
        private const int ATTACK_TYPE_PILLAGE_ALE = 6;
        private const int ATTACK_TYPE_PILLAGE_ARMOURY = 7;
        private const int ATTACK_TYPE_RAZE = 9;
        private const int ATTACK_TYPE_VANDALISE = 11;
        private const int ATTACK_TYPE_GOLD_RAID = 12;

        private Dictionary<long, bool> _knownArmyIds = new Dictionary<long, bool>();
        private Dictionary<long, bool> _knownPersonIds = new Dictionary<long, bool>();
        private Dictionary<long, int> _incomingAttackTargets = new Dictionary<long, int>();
        private Dictionary<int, DateTime> _villagesSentInterdict = new Dictionary<int, DateTime>();
        private bool _firstScan = true;

        // Pending army detail lookups — keyed by armyID
        private Dictionary<long, PendingArmyLookup> _pendingLookups = new Dictionary<long, PendingArmyLookup>();

        private class PendingArmyLookup
        {
            public long ArmyID;
            public WorldMap.LocalArmyData Army;
            public string ActionKey;
            public RadarSettings Settings;
            public DateTime RequestTime;
            public bool Completed;
            public bool Notified;
            public int NumPeasants;
            public int NumArchers;
            public int NumPikemen;
            public int NumSwordsmen;
            public int NumCatapults;
            public int NumCaptains;
            public int PillagePercent;
            public int LootType;
            public double LootLevel;
        }

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
            _incomingAttackTargets.Clear();
            _villagesSentInterdict.Clear();
            _pendingLookups.Clear();
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

            ProcessPendingLookups(settings);
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

                if (!IsTargetingUser(army.targetVillageID))
                    continue;

                if (army.lootType >= 0)
                    continue;

                string actionKey = ClassifyArmy(army);
                if (actionKey == null) continue;

                // Track incoming attacks for repair-on-attack (any offensive action)
                if (actionKey != ACTION_SCOUT && actionKey != ACTION_FORAGING && actionKey != ACTION_REINFORCEMENT)
                    _incomingAttackTargets[army.armyID] = army.targetVillageID;

                RadarActionSettings actionSettings = settings.GetActionSettings(actionKey);
                if (!actionSettings.Monitor) continue;

                // Queue a detail lookup — notifications fire inside the callback for speed
                if (!_pendingLookups.ContainsKey(army.armyID))
                {
                    PendingArmyLookup pending = new PendingArmyLookup();
                    pending.ArmyID = army.armyID;
                    pending.Army = army;
                    pending.ActionKey = actionKey;
                    pending.Settings = settings;
                    pending.RequestTime = DateTime.Now;
                    pending.Completed = false;
                    pending.Notified = false;
                    _pendingLookups[army.armyID] = pending;

                    try
                    {
                        RemoteServices.Instance.set_RetrieveAttackResult_UserCallBack(
                            new RemoteServices.RetrieveAttackResult_UserCallBack(this.RetrieveAttackResultCallback));
                        RemoteServices.Instance.RetrieveAttackResult(
                            army.armyID, GameEngine.Instance.World.StoredVillageFactionPos);
                    }
                    catch (Exception ex)
                    {
                        LogWarning("Failed to request army details for " + army.armyID + ": " + ex.Message);
                        pending.Completed = true;
                    }
                }
            }

            // Detect landed attacks: tracked armies that disappeared
            List<long> landedIds = new List<long>();
            foreach (long armyId in _incomingAttackTargets.Keys)
            {
                if (!currentIds.ContainsKey(armyId))
                    landedIds.Add(armyId);
            }
            foreach (long armyId in landedIds)
            {
                int targetVillageId = _incomingAttackTargets[armyId];
                _incomingAttackTargets.Remove(armyId);
                NotifyCastleRepairModule(targetVillageId);
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

        // Called by the game when RetrieveAttackResult completes — fires notifications immediately
        private void RetrieveAttackResultCallback(RetrieveAttackResult_ReturnType returnData)
        {
            try
            {
                if (returnData == null || !returnData.Success)
                {
                    foreach (PendingArmyLookup p in _pendingLookups.Values)
                    {
                        if (!p.Completed)
                        {
                            p.Completed = true;
                            FireNotification(p);
                            break;
                        }
                    }
                    return;
                }

                if (returnData.armyData != null)
                {
                    long armyId = returnData.armyData.armyID;
                    if (_pendingLookups.ContainsKey(armyId))
                    {
                        PendingArmyLookup pending = _pendingLookups[armyId];
                        pending.NumPeasants = returnData.armyData.numPeasants;
                        pending.NumArchers = returnData.armyData.numArchers;
                        pending.NumPikemen = returnData.armyData.numPikemen;
                        pending.NumSwordsmen = returnData.armyData.numSwordsmen;
                        pending.NumCatapults = returnData.armyData.numCatapults;
                        pending.NumCaptains = returnData.armyData.numCaptains;
                        pending.PillagePercent = returnData.armyData.pillagePercent;
                        pending.LootType = returnData.armyData.lootType;
                        pending.LootLevel = returnData.armyData.lootLevel;
                        pending.Completed = true;
                        LogDebug("Received detailed army data for armyID " + armyId);
                        FireNotification(pending);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("RetrieveAttackResult callback error: " + ex.Message);
            }
        }

        // Sends the actual notification for a completed lookup
        private void FireNotification(PendingArmyLookup pending)
        {
            if (pending.Notified) return;
            pending.Notified = true;

            try
            {
                WorldMap.LocalArmyData army = pending.Army;
                RadarActionSettings actionSettings = pending.Settings.GetActionSettings(pending.ActionKey);

                string sourceName = GetVillageName(army.travelFromVillageID);
                string targetName = GetVillageName(army.targetVillageID);
                string timeLeft = GetTimeLeft(army.serverEndTime);
                string actionLabel = GetActionLabel(pending.ActionKey);

                string message = actionLabel + " detected!\n" +
                    "From: " + sourceName + " [" + army.travelFromVillageID + "]\n" +
                    "To: " + targetName + " [" + army.targetVillageID + "]\n" +
                    "ETA: " + timeLeft + "\n" +
                    "P:" + pending.NumPeasants +
                    " , Arch:" + pending.NumArchers +
                    " , Pike:" + pending.NumPikemen +
                    " , Swords:" + pending.NumSwordsmen +
                    " , Cats:" + pending.NumCatapults +
                    " , Caps:" + pending.NumCaptains;

                if (pending.ActionKey == ACTION_PILLAGE_STOCKPILE ||
                    pending.ActionKey == ACTION_PILLAGE_GRANARY ||
                    pending.ActionKey == ACTION_PILLAGE_BANQUET ||
                    pending.ActionKey == ACTION_PILLAGE_ALE ||
                    pending.ActionKey == ACTION_PILLAGE_ARMOURY ||
                    pending.ActionKey == ACTION_GOLD_RAID)
                {
                    message += "\nPillage: " + pending.PillagePercent + "%";
                }

                string troopInfo = GetTroopInfo(army);
                if (troopInfo.Length > 0)
                    message += "\n" + troopInfo;

                LogWarning(message.Replace("\n", " | "));

                if (actionSettings.SystemNotify)
                    ShowSystemNotification(actionLabel, message);

                if (actionSettings.DiscordNotify && !string.IsNullOrEmpty(pending.Settings.DiscordWebhookUrl))
                    DiscordNotifier.SendAsync(pending.Settings.DiscordWebhookUrl,
                        "\u26A0 " + actionLabel + " Incoming!", message, 16736352);

                if (actionSettings.AutoInterdict)
                {
                    int totalArmySize = pending.NumPeasants + pending.NumArchers +
                        pending.NumPikemen + pending.NumSwordsmen +
                        pending.NumCatapults + pending.NumCaptains;
                    int minSize = pending.Settings.MinArmySizeForInterdict;

                    if (totalArmySize >= minSize)
                    {
                        TryAutoInterdict(army.targetVillageID, pending.Settings);
                    }
                    else
                    {
                        LogInfo("Auto-interdict skipped for army " + pending.ArmyID +
                            ": army size " + totalArmySize + " below minimum threshold of " + minSize + ".");
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("FireNotification error for armyID " + pending.ArmyID + ": " + ex.Message);
            }
        }

        // Fallback: process any lookups that timed out without a callback
        private void ProcessPendingLookups(RadarSettings settings)
        {
            if (_pendingLookups.Count == 0) return;

            List<long> toRemove = new List<long>();

            foreach (KeyValuePair<long, PendingArmyLookup> kvp in _pendingLookups)
            {
                PendingArmyLookup pending = kvp.Value;

                if (!pending.Completed && (DateTime.Now - pending.RequestTime).TotalSeconds > 5)
                {
                    LogDebug("Army detail lookup timed out for armyID " + pending.ArmyID + ", proceeding with basic info.");
                    pending.Completed = true;
                    FireNotification(pending);
                }

                if (pending.Completed && pending.Notified)
                    toRemove.Add(kvp.Key);
            }

            foreach (long id in toRemove)
                _pendingLookups.Remove(id);
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

                if (actionSettings.AutoInterdict)
                    TryAutoInterdict(targetVillage, settings);
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

            // Capital-level attacks take priority
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

            // Scouts: scout-only armies
            if (army.numScouts > 0 && army.numPeasants == 0 && army.numArchers == 0 &&
                army.numPikemen == 0 && army.numSwordsmen == 0 && army.numCatapults == 0)
                return ACTION_SCOUT;

            //// Foraging
            //if (army.attackType == 9)
            //    return ACTION_FORAGING;

            // Reinforcements
            if (army.attackType == 13)
                return ACTION_REINFORCEMENT;

            // Granular attack types
            switch (army.attackType)
            {
                case ATTACK_TYPE_CAPTURE:
                    return ACTION_CAPTURE;
                case ATTACK_TYPE_PILLAGE_STOCKPILE:
                    return ACTION_PILLAGE_STOCKPILE;
                case ATTACK_TYPE_RANSACK:
                    return ACTION_RANSACK;
                case ATTACK_TYPE_PILLAGE_GRANARY:
                    return ACTION_PILLAGE_GRANARY;
                case ATTACK_TYPE_PILLAGE_BANQUET:
                    return ACTION_PILLAGE_BANQUET;
                case ATTACK_TYPE_PILLAGE_ALE:
                    return ACTION_PILLAGE_ALE;
                case ATTACK_TYPE_PILLAGE_ARMOURY:
                    return ACTION_PILLAGE_ARMOURY;
                case ATTACK_TYPE_RAZE:
                    return ACTION_RAZE;
                case ATTACK_TYPE_VANDALISE:
                    return ACTION_VANDALISE;
                case ATTACK_TYPE_GOLD_RAID:
                    return ACTION_GOLD_RAID;
                default:
                    return ACTION_CAPTURE;
            }
        }

        private string ClassifyPerson(WorldMap.LocalPerson person, int targetVillage)
        {
            if (person.person.personType != 4)
                return null;

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

        // =================================================================
        // Auto-interdict
        // =================================================================

        private void TryAutoInterdict(int villageId, RadarSettings settings)
        {
            int numberOfMonks = settings.AutoInterdictMonkCount;
            if (numberOfMonks <= 0) numberOfMonks = 1;

            // Don't interdict capitals
            if (GameEngine.Instance.World.isCapital(villageId))
            {
                LogDebug("Auto-interdict: skipping capital village " + villageId);
                return;
            }

            // Throttle: don't re-interdict same village within 30 seconds
            if (_villagesSentInterdict.ContainsKey(villageId))
            {
                if ((DateTime.Now - _villagesSentInterdict[villageId]).TotalSeconds < 30)
                {
                    LogDebug("Auto-interdict: throttled for village " + villageId);
                    return;
                }
                _villagesSentInterdict[villageId] = DateTime.Now;
            }
            else
            {
                _villagesSentInterdict[villageId] = DateTime.Now;
            }

            VillageMap village = GameEngine.Instance.getVillage(villageId);
            if (village == null)
            {
                LogWarning("Auto-interdict: village " + villageId + " not loaded.");
                return;
            }

            // Check ordination research (required to have monks at all)
            if (GameEngine.Instance.World.UserResearchData.Research_Ordination == 0)
            {
                LogWarning("Auto-interdict: Ordination not researched. Cannot use monks.");
                return;
            }

            // Check eucharist research (required for interdict)
            if (GameEngine.Instance.World.UserResearchData.Research_Eucharist <= 0)
            {
                LogWarning("Auto-interdict: Eucharist not researched. Cannot interdict.");
                return;
            }

            // Check excommunication
            if (GameEngine.Instance.World.isVillageExcommunicated(villageId))
            {
                LogWarning("Auto-interdict: village " + villageId + " is excommunicated.");
                return;
            }

            // Check faith points cost
            try
            {
                int costPerMonk = TradingCalcs.adjustInterdictionCostByTargetRank(
                    GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Interdicts,
                    GameEngine.Instance.World.getRank(),
                    GameEngine.Instance.World.SecondAgeWorld);
                int totalCost = costPerMonk * numberOfMonks;
                double currentFaith = GameEngine.Instance.World.getCurrentFaithPoints();
                if (totalCost > currentFaith)
                {
                    LogWarning("Auto-interdict: not enough faith points (" + currentFaith + " < " + totalCost + ").");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogWarning("Auto-interdict: faith point check failed: " + ex.Message);
            }

            // Count monks at home
            int athome = village.calcTotalMonksAtHome();
            LogDebug("Auto-interdict: village " + villageId + " has " + athome + " monks at home.");

            // If not enough monks, try to recruit
            if (athome < numberOfMonks && settings.AutoRecruitMonks)
            {
                LogInfo("Auto-interdict: recruiting " + (numberOfMonks - athome) + " monk(s) for village " + villageId);
                int made = MakeMonks(villageId, numberOfMonks - athome, village);
                if (made > 0)
                    athome = village.calcTotalMonksAtHome();
            }

            if (athome <= 0)
            {
                LogWarning("Auto-interdict: no monks available at village " + villageId + ".");
                return;
            }

            if (athome < numberOfMonks)
                numberOfMonks = athome;

            try
            {
                LogInfo("Auto-interdict: sending " + numberOfMonks + " monk(s) to interdict village " + villageId);
                RemoteServices.Instance.set_SendPeople_UserCallBack(
                    new RemoteServices.SendPeople_UserCallBack(SendPeopleCallback));
                RemoteServices.Instance.SendPeople(villageId, villageId, 4, numberOfMonks, 4, -1);
            }
            catch (Exception ex)
            {
                LogError("Auto-interdict failed: " + ex.Message);
            }
        }

        private int MakeMonks(int villageId, int numberOfMonks, VillageMap village)
        {
            try
            {
                if (numberOfMonks <= 0) return 0;

                // Max monks from ordination research
                int researchMonkLevel = ResearchData.ordinationResearchMonkLevels[
                    (int)GameEngine.Instance.World.UserResearchData.Research_Ordination];
                numberOfMonks = Math.Min(numberOfMonks, researchMonkLevel);

                // How many total monks already exist for this village
                int athome = 0;
                int totalMonks = GameEngine.Instance.World.countVillagePeople(villageId, 4, ref athome);
                int canMake = researchMonkLevel - totalMonks;
                numberOfMonks = Math.Min(numberOfMonks, canMake);
                if (numberOfMonks <= 0)
                {
                    LogDebug("MakeMonks: village " + villageId + " already at monk limit.");
                    return 0;
                }

                // Check spare workers
                int spareWorkers = village.m_spareWorkers;
                if (numberOfMonks > spareWorkers)
                {
                    LogDebug("MakeMonks: only " + spareWorkers + " spare workers at village " + villageId);
                    numberOfMonks = spareWorkers;
                }

                // Check unit capacity
                int unitCapacity = GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
                int currentUsage = village.calcUnitUsages();
                int unitSizeMonk = GameEngine.Instance.LocalWorldData.UnitSize_Priests;
                int neededSpace = numberOfMonks * unitSizeMonk;
                int availableSpace = unitCapacity - currentUsage;
                if (availableSpace < neededSpace)
                {
                    numberOfMonks = availableSpace / unitSizeMonk;
                    LogDebug("MakeMonks: unit capacity limited, can make " + numberOfMonks + " monk(s).");
                }

                if (numberOfMonks <= 0) return 0;

                LogInfo("MakeMonks: recruiting " + numberOfMonks + " monk(s) at village " + villageId);
                if (numberOfMonks == 1)
                    village.makePeople(4);
                else
                    village.makePeople(1000 + numberOfMonks);

                return numberOfMonks;
            }
            catch (Exception ex)
            {
                LogError("MakeMonks failed: " + ex.Message);
                return 0;
            }
        }

        private void SendPeopleCallback(SendPeople_ReturnType returnData)
        {
            try
            {
                if (!returnData.Success)
                {
                    BotLogger.Log("Radar", BotLogLevel.Warning,
                        "Interdict failed for village " +
                        GameEngine.Instance.World.getVillageName(returnData.targetVillageID) +
                        ": " + CommonTypes.ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID));
                    if (_villagesSentInterdict.ContainsKey(returnData.targetVillageID))
                        _villagesSentInterdict.Remove(returnData.targetVillageID);
                }
                else
                {
                    BotLogger.Log("Radar", BotLogLevel.Info,
                        "Successful interdict at village " +
                        GameEngine.Instance.World.getVillageName(returnData.targetVillageID));
                    GameEngine.Instance.World.importOrphanedPeople(
                        returnData.people, returnData.currentTime, -2);
                    GameEngine.Instance.World.setFaithPointsData(
                        returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);

                    // Auto re-recruit monks after interdict
                    if (Engine != null && Engine.Settings != null &&
                        Engine.Settings.Radar.AutoRecruitMonks)
                    {
                        int monkLimit = ResearchData.ordinationResearchMonkLevels[
                            (int)GameEngine.Instance.World.UserResearchData.Research_Ordination];
                        int athome = 0;
                        int totalMonks = GameEngine.Instance.World.countVillagePeople(
                            returnData.targetVillageID, 4, ref athome);
                        int toRecruit = monkLimit - totalMonks;
                        if (toRecruit > 0)
                        {
                            VillageMap village = GameEngine.Instance.getVillage(returnData.targetVillageID);
                            if (village != null)
                                MakeMonks(returnData.targetVillageID, toRecruit, village);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BotLogger.Log("Radar", BotLogLevel.Error,
                    "SendPeople callback error: " + ex.Message);
            }
        }

        // =================================================================
        // UI helpers
        // =================================================================

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
                case ACTION_CAPTURE: return "Capture";
                case ACTION_RAZE: return "Raze";
                case ACTION_RANSACK: return "Ransack";
                case ACTION_PILLAGE_STOCKPILE: return "Pillage Stockpile";
                case ACTION_PILLAGE_GRANARY: return "Pillage Granary";
                case ACTION_PILLAGE_BANQUET: return "Pillage Banquet";
                case ACTION_PILLAGE_ALE: return "Pillage Ale";
                case ACTION_PILLAGE_ARMOURY: return "Pillage Armoury";
                case ACTION_VANDALISE: return "Vandalise";
                case ACTION_GOLD_RAID: return "Gold Raid";
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
            _incomingAttackTargets.Clear();
            _villagesSentInterdict.Clear();
            _pendingLookups.Clear();
        }

        private void NotifyCastleRepairModule(int villageId)
        {
            if (Engine == null) return;
            foreach (IBotModule module in Engine.Modules)
            {
                CastleRepairModule crModule = module as CastleRepairModule;
                if (crModule != null && crModule.Enabled)
                {
                    crModule.NotifyAttackLanded(villageId);
                    break;
                }
            }
        }
    }
}
