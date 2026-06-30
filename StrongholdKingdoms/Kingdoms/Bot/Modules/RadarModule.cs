using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

        // AI variants � source village has special > 0
        public const string ACTION_AI_CAPTURE = "AICapture";
        public const string ACTION_AI_RAZE = "AIRaze";
        public const string ACTION_AI_RANSACK = "AIRansack";
        public const string ACTION_AI_PILLAGE_STOCKPILE = "AIPillageStockpile";
        public const string ACTION_AI_PILLAGE_GRANARY = "AIPillageGranary";
        public const string ACTION_AI_PILLAGE_BANQUET = "AIPillageBanquet";
        public const string ACTION_AI_PILLAGE_ALE = "AIPillageAle";
        public const string ACTION_AI_PILLAGE_ARMOURY = "AIPillageArmoury";
        public const string ACTION_AI_VANDALISE = "AIVandalise";
        public const string ACTION_AI_GOLD_RAID = "AIGoldRaid";
        public const string ACTION_AI_SCOUT = "AIScout";
        public const string ACTION_AI_CAPTAIN = "AICaptain";
        public const string ACTION_AI_ATTACK_PARISH = "AIAttackParish";
        public const string ACTION_AI_ATTACK_COUNTY = "AIAttackCounty";
        public const string ACTION_AI_ATTACK_PROVINCE = "AIAttackProvince";
        public const string ACTION_AI_ATTACK_COUNTRY = "AIAttackCountry";
        public const string ACTION_AI_FORAGING = "AIForaging";

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
            ACTION_FORAGING,
            ACTION_AI_CAPTURE,
            ACTION_AI_RAZE,
            ACTION_AI_RANSACK,
            ACTION_AI_PILLAGE_STOCKPILE,
            ACTION_AI_PILLAGE_GRANARY,
            ACTION_AI_PILLAGE_BANQUET,
            ACTION_AI_PILLAGE_ALE,
            ACTION_AI_PILLAGE_ARMOURY,
            ACTION_AI_VANDALISE,
            ACTION_AI_GOLD_RAID,
            ACTION_AI_SCOUT,
            ACTION_AI_CAPTAIN,
            ACTION_AI_ATTACK_PARISH,
            ACTION_AI_ATTACK_COUNTY,
            ACTION_AI_ATTACK_PROVINCE,
            ACTION_AI_ATTACK_COUNTRY,
            ACTION_AI_FORAGING
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
        // Villages with an interdict retry cycle currently running on a background
        // thread — prevents duplicate cycles for the same village. Guarded by _interdictLock,
        // which also guards _villagesSentInterdict (now touched from background threads).
        private readonly HashSet<int> _interdictInProgress = new HashSet<int>();
        private readonly object _interdictLock = new object();
        private bool _firstScan = true;
        private Dictionary<long, WorldMap.LocalArmyData> _trackedArmies = new Dictionary<long, WorldMap.LocalArmyData>();
        private Dictionary<long, int> _armyMissingTicks = new Dictionary<long, int>();
        private Dictionary<long, WorldMap.LocalPerson> _trackedPeople = new Dictionary<long, WorldMap.LocalPerson>();
        private Dictionary<long, int> _personMissingTicks = new Dictionary<long, int>();

        // Pending army detail lookups � keyed by armyID
        private Dictionary<long, PendingArmyLookup> _pendingLookups = new Dictionary<long, PendingArmyLookup>();

        // Group member player village lookup
        private System.Threading.ManualResetEvent _groupLookupEvent = new System.Threading.ManualResetEvent(false);
        private GetOtherUserVillageIDList_ReturnType _groupLookupResult;
        // Serializes player-village lookups so the periodic auto-refresh (background
        // thread) and the manual "Refresh All" button can't clobber each other's
        // _groupLookupEvent/_groupLookupResult.
        private readonly object _groupLookupLock = new object();

        // Group member auto-refresh scheduling
        private DateTime _lastGroupRefresh = DateTime.MinValue;
        private volatile bool _groupRefreshInProgress = false;
        // Per-member lookup retries within a single refresh pass before giving up
        // (the next periodic refresh will try the member again).
        private const int GroupRefreshMaxAttempts = 3;
        private const int GroupRefreshRetryDelayMs = 2000;

        private class PendingPersonNotification
        {
            public WorldMap.LocalPerson Person;
            public int TargetVillage;
            public string ActionKey;
            public RadarActionSettings ActionSettings;
            public GroupRadarMember GroupMember;      // null = user-village path
            public GroupRadarSettings GroupSettings;  // null = user-village path
        }

        private class PendingArmyLookup
        {
            public long ArmyID;
            public WorldMap.LocalArmyData Army;
            public string ActionKey;
            public RadarSettings Settings;
            public GroupRadarMember GroupMember;  // null = user-village path
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
            lock (_interdictLock) { _villagesSentInterdict.Clear(); _interdictInProgress.Clear(); }
            _pendingLookups.Clear();
            _trackedArmies.Clear();
            _armyMissingTicks.Clear();
            _trackedPeople.Clear();
            _personMissingTicks.Clear();
            _firstScan = true;
            _lastGroupRefresh = DateTime.MinValue;
        }

        protected override void OnTick()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            RadarSettings settings = (Engine != null && Engine.Settings != null)
                ? Engine.Settings.Radar
                : null;

            if (settings == null) return;

            // Auto-refresh group members' village lists (on start + periodically).
            MaybeAutoRefreshGroupMembers(settings);

            // Keep our GetArmyData wrapper in place each tick. retrieveArmies() (called
            // on reconnect) resets the callback to WorldMap.getArmyData — re-setting here
            // ensures we intercept every incremental server response and immediately add
            // new armies to _trackedArmies, closing the flicker window for all armies
            // (not just our own sent ones).
            RemoteServices.Instance.set_GetArmyData_UserCallBack(
                new RemoteServices.GetArmyData_UserCallBack(this.OnGetArmyDataCallback));

            SyncTrackedArmies();
            SyncTrackedPeople();

            if (settings.ForceRefreshArmies)
            {
                try { GameEngine.Instance.World.getArmiesIfNewAttacks(); }
                catch (Exception ex) { LogDebug("Force refresh failed: " + ex.Message); }
            }

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

        private void SyncTrackedArmies()
        {
            SparseArray gameArray = GameEngine.Instance.World.getArmyArray();
            if (gameArray == null) return;

            foreach (WorldMap.LocalArmyData army in gameArray)
                _trackedArmies[army.armyID] = army;

            DateTime serverNow = VillageMap.getCurrentServerTime();
            List<long> toRemove = new List<long>();

            foreach (KeyValuePair<long, WorldMap.LocalArmyData> kvp in _trackedArmies)
            {
                WorldMap.LocalArmyData army = kvp.Value;

                if (army.serverEndTime <= serverNow.AddSeconds(-30))
                {
                    toRemove.Add(army.armyID);
                    continue;
                }

                if (gameArray[army.armyID] == null)
                {
                    int misses = _armyMissingTicks.ContainsKey(army.armyID)
                        ? _armyMissingTicks[army.armyID] : 0;

                    // Only give up if the army's travel time has also passed.
                    // The game's getArmyData reconciliation can remove armies that the
                    // server hasn't yet confirmed (race between launch and the next
                    // existingArmies response) — those have a future serverEndTime and
                    // must keep being re-injected until the server catches up.
                    bool travelTimePassed = army.serverEndTime <= serverNow.AddSeconds(30);

                    if (misses >= 5 && travelTimePassed)
                    {
                        toRemove.Add(army.armyID);
                    }
                    else
                    {
                        _armyMissingTicks[army.armyID] = misses + 1;
                        try
                        {
                            army.updatePosition();
                            GameEngine.Instance.World.SetArmy(army);
                        }
                        catch { }
                    }
                }
                else
                {
                    _armyMissingTicks.Remove(army.armyID);
                }
            }

            foreach (long id in toRemove)
                _trackedArmies.Remove(id);
        }

        private void SyncTrackedPeople()
        {
            SparseArray gameArray = GameEngine.Instance.World.getPeopleArray();
            if (gameArray == null) return;

            foreach (WorldMap.LocalPerson person in gameArray)
                _trackedPeople[person.personID] = person;

            DateTime serverNow = VillageMap.getCurrentServerTime();
            List<long> toRemove = new List<long>();

            foreach (KeyValuePair<long, WorldMap.LocalPerson> kvp in _trackedPeople)
            {
                WorldMap.LocalPerson person = kvp.Value;

                if (person.serverEndTime <= serverNow.AddSeconds(-30))
                {
                    toRemove.Add(person.personID);
                    continue;
                }

                if (gameArray[person.personID] == null && !person.dying)
                {
                    int misses = _personMissingTicks.ContainsKey(person.personID)
                        ? _personMissingTicks[person.personID] : 0;

                    if (misses >= 3)
                    {
                        toRemove.Add(person.personID);
                    }
                    else
                    {
                        _personMissingTicks[person.personID] = misses + 1;
                        try { gameArray[person.personID] = (object) person; }
                        catch { }
                    }
                }
                else
                {
                    _personMissingTicks.Remove(person.personID);
                }
            }

            foreach (long id in toRemove)
                _trackedPeople.Remove(id);
        }

        private void ScanArmies(RadarSettings settings)
        {
            Dictionary<long, bool> currentIds = new Dictionary<long, bool>();
            GroupRadarSettings groupSettings = settings.GroupRadar;

            foreach (KeyValuePair<long, WorldMap.LocalArmyData> kvp in _trackedArmies)
            {
                WorldMap.LocalArmyData army = kvp.Value;
                currentIds[army.armyID] = true;

                if (_knownArmyIds.ContainsKey(army.armyID))
                    continue;

                _knownArmyIds[army.armyID] = true;

                if (_firstScan)
                    continue;

                // Determine whether this targets the user or a monitored group member
                bool targetingUser = IsTargetingUser(army.targetVillageID);
                GroupRadarMember groupMember = null;

                if (!targetingUser)
                {
                    if (groupSettings == null || !groupSettings.Enabled) continue;
                    groupMember = FindGroupMember(army.targetVillageID, groupSettings);
                    if (groupMember == null || !groupMember.Enabled) continue;
                }

                if (army.lootType >= 0)
                    continue;

                string actionKey = ClassifyArmy(army);
                if (actionKey == null) continue;

                // Track incoming attacks for repair-on-attack (user village path only)
                if (groupMember == null &&
                    actionKey != ACTION_SCOUT && actionKey != ACTION_FORAGING && actionKey != ACTION_REINFORCEMENT &&
                    actionKey != ACTION_AI_SCOUT && actionKey != ACTION_AI_FORAGING)
                {
                    _incomingAttackTargets[army.armyID] = army.targetVillageID;
                }

                RadarActionSettings actionSettings = groupMember != null
                    ? groupSettings.GetActionSettings(actionKey)
                    : settings.GetActionSettings(actionKey);

                if (!actionSettings.Monitor) continue;

                // Queue a detail lookup � notifications fire inside the callback for speed
                if (!_pendingLookups.ContainsKey(army.armyID))
                {
                    PendingArmyLookup pending = new PendingArmyLookup();
                    pending.ArmyID = army.armyID;
                    pending.Army = army;
                    pending.ActionKey = actionKey;
                    pending.Settings = settings;
                    pending.GroupMember = groupMember;
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

        // Called by the game when RetrieveAttackResult completes � fires notifications immediately
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

            // Delegate to group path if this is a group-member village lookup
            if (pending.GroupMember != null)
            {
                FireGroupNotification(pending);
                return;
            }

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
                    pending.ActionKey == ACTION_GOLD_RAID ||
                    pending.ActionKey == ACTION_AI_PILLAGE_STOCKPILE ||
                    pending.ActionKey == ACTION_AI_PILLAGE_GRANARY ||
                    pending.ActionKey == ACTION_AI_PILLAGE_BANQUET ||
                    pending.ActionKey == ACTION_AI_PILLAGE_ALE ||
                    pending.ActionKey == ACTION_AI_PILLAGE_ARMOURY ||
                    pending.ActionKey == ACTION_AI_GOLD_RAID)
                {
                    message += "\nPillage: " + pending.PillagePercent + "%";
                }

                string troopInfo = GetTroopInfo(army);
                if (troopInfo.Length > 0)
                    message += "\n" + troopInfo;

                LogWarning(message.Replace("\n", " | "));

                if (actionSettings.SystemNotify)
                    ShowSystemNotification(actionLabel, message);

                if (actionSettings.DiscordNotify && !string.IsNullOrEmpty(pending.Settings.DiscordWebhookUrl) &&
                    DiscordAllowedByIgnoreOptions(pending,
                        pending.Settings.UseIgnoreOptionsForDiscord,
                        pending.Settings.MinArmySizeForInterdict,
                        pending.Settings.MaxLandTimeHours, "Radar"))
                    DiscordNotifier.SendAsync(pending.Settings.DiscordWebhookUrl,
                        "\u26A0 " + actionLabel + " Incoming!", message, 16736352,
                        string.IsNullOrEmpty(pending.Settings.DiscordMentionTag) ? null : pending.Settings.DiscordMentionTag);

                if (actionSettings.SoundNotify)
                    RadarSoundPlayer.Play(actionSettings.SoundFile);

                if (actionSettings.AutoInterdict)
                {
                    int totalArmySize = pending.NumPeasants + pending.NumArchers +
                        pending.NumPikemen + pending.NumSwordsmen +
                        pending.NumCatapults + pending.NumCaptains;
                    int minSize = pending.Settings.MinArmySizeForInterdict;

                    if (totalArmySize < minSize)
                    {
                        LogInfo("Auto-interdict skipped for army " + pending.ArmyID +
                            ": army size " + totalArmySize + " below minimum threshold of " + minSize + ".");
                    }
                    else if (pending.Settings.MinAttacksForInterdict > 0 &&
                             !HasEnoughAttacksInWindow(army.targetVillageID,
                                 pending.Settings.MinAttacksForInterdict,
                                 pending.Settings.MinAttacksWindowSeconds))
                    {
                        LogInfo("Auto-interdict skipped for village " + army.targetVillageID +
                            ": fewer than " + pending.Settings.MinAttacksForInterdict +
                            " attacks land within " + pending.Settings.MinAttacksWindowSeconds + "s window.");
                    }
                    else if (pending.Settings.MaxLandTimeHours > 0 &&
                             !HasAttackWithinMaxLandTime(army.targetVillageID,
                                 pending.Settings.MaxLandTimeHours))
                    {
                        LogInfo("Auto-interdict skipped for village " + army.targetVillageID +
                            ": all attacks land beyond max land time of " +
                            pending.Settings.MaxLandTimeHours + "h.");
                    }
                    else
                    {
                        TryAutoInterdict(army.targetVillageID, pending.Settings);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("FireNotification error for armyID " + pending.ArmyID + ": " + ex.Message);
            }
        }

        // Notification for an army targeting a group member's village
        private void FireGroupNotification(PendingArmyLookup pending)
        {
            try
            {
                GroupRadarSettings groupSettings = pending.Settings.GroupRadar;
                GroupRadarMember member = pending.GroupMember;
                WorldMap.LocalArmyData army = pending.Army;
                RadarActionSettings actionSettings = groupSettings.GetActionSettings(pending.ActionKey);

                string sourceName = GetVillageName(army.travelFromVillageID);
                string targetName = GetVillageName(army.targetVillageID);
                string timeLeft = GetTimeLeft(army.serverEndTime);
                string actionLabel = GetActionLabel(pending.ActionKey);

                string message =  member.PlayerName + "\n" +
                    actionLabel + " detected!\n" +
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
                    pending.ActionKey == ACTION_GOLD_RAID ||
                    pending.ActionKey == ACTION_AI_PILLAGE_STOCKPILE ||
                    pending.ActionKey == ACTION_AI_PILLAGE_GRANARY ||
                    pending.ActionKey == ACTION_AI_PILLAGE_BANQUET ||
                    pending.ActionKey == ACTION_AI_PILLAGE_ALE ||
                    pending.ActionKey == ACTION_AI_PILLAGE_ARMOURY ||
                    pending.ActionKey == ACTION_AI_GOLD_RAID)
                {
                    message += "\nPillage: " + pending.PillagePercent + "%";
                }

                LogWarning("[Group] " + message.Replace("\n", " | "));

                if (actionSettings.DiscordNotify && !string.IsNullOrEmpty(groupSettings.DiscordWebhookUrl) &&
                    DiscordAllowedByIgnoreOptions(pending,
                        groupSettings.UseIgnoreOptionsForDiscord,
                        groupSettings.MinArmySize,
                        groupSettings.MaxLandTimeHours, "Group"))
                {
                    string mention = !string.IsNullOrEmpty(member.DiscordTag)
                        ? member.DiscordTag
                        : (!string.IsNullOrEmpty(groupSettings.DiscordMentionTag) ? groupSettings.DiscordMentionTag : null);
                    DiscordNotifier.SendAsync(groupSettings.DiscordWebhookUrl,
                        "⚠ " + actionLabel + " Incoming!", message, 16736352, mention);
                }
            }
            catch (Exception ex)
            {
                LogError("FireGroupNotification error for armyID " + pending.ArmyID + ": " + ex.Message);
            }
        }

        // Finds a group member who owns the given village ID
        private GroupRadarMember FindGroupMember(int villageId, GroupRadarSettings groupSettings)
        {
            if (groupSettings == null || groupSettings.Members == null) return null;
            foreach (GroupRadarMember m in groupSettings.Members)
            {
                if (m.VillageIds == null) continue;
                foreach (int vid in m.VillageIds)
                    if (vid == villageId) return m;
            }
            return null;
        }

        // Notification for a monk/scout/captain targeting a group member's village
        private void FireGroupPersonNotification(WorldMap.LocalPerson person, int targetVillage,
            string actionKey, GroupRadarMember member, GroupRadarSettings groupSettings,
            string countSuffix = "", string commandSuffix = "")
        {
            try
            {
                string sourceName = GetVillageName(person.person.homeVillageID);
                string targetName = GetVillageName(targetVillage);
                string actionLabel = GetActionLabel(actionKey);

                string message = actionLabel + commandSuffix + " detected!" + countSuffix + "\n" +
                    "From: " + sourceName + " [" + person.person.homeVillageID + "]\n" +
                    "To: " + targetName + " [" + targetVillage + "]";

                LogWarning("[Group] " + message.Replace("\n", " | "));

                if (!string.IsNullOrEmpty(groupSettings.DiscordWebhookUrl))
                {
                    RadarActionSettings actionSettings = groupSettings.GetActionSettings(actionKey);
                    if (actionSettings.DiscordNotify)
                    {
                        string mention = !string.IsNullOrEmpty(member.DiscordTag)
                            ? member.DiscordTag
                            : (!string.IsNullOrEmpty(groupSettings.DiscordMentionTag) ? groupSettings.DiscordMentionTag : null);
                        DiscordNotifier.SendAsync(groupSettings.DiscordWebhookUrl,
                            "⚠ " + actionLabel + commandSuffix + " Incoming!", message, 15105570, mention);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("FireGroupPersonNotification error: " + ex.Message);
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
            Dictionary<long, bool> currentIds = new Dictionary<long, bool>();
            List<PendingPersonNotification> pendingNotifications = new List<PendingPersonNotification>();

            foreach (KeyValuePair<long, WorldMap.LocalPerson> kvp in _trackedPeople)
            {
                WorldMap.LocalPerson person = kvp.Value;
                long personId = person.personID;
                currentIds[personId] = true;

                if (_knownPersonIds.ContainsKey(personId))
                    continue;

                _knownPersonIds[personId] = true;

                if (_firstScan)
                    continue;

                int targetVillage = person.person.targetVillageID;
                bool targetingUser = IsTargetingUser(targetVillage);
                GroupRadarMember groupMember = null;
                GroupRadarSettings groupSettings = null;

                if (!targetingUser)
                {
                    groupSettings = settings.GroupRadar;
                    if (groupSettings == null || !groupSettings.Enabled) continue;
                    groupMember = FindGroupMember(targetVillage, groupSettings);
                    if (groupMember == null || !groupMember.Enabled) continue;
                }

                string actionKey = ClassifyPerson(person, targetVillage);
                if (actionKey == null) continue;

                if (groupMember != null)
                {
                    RadarActionSettings actionSettings = groupSettings.GetActionSettings(actionKey);
                    if (!actionSettings.Monitor) continue;
                    pendingNotifications.Add(new PendingPersonNotification
                    {
                        Person = person,
                        TargetVillage = targetVillage,
                        ActionKey = actionKey,
                        ActionSettings = actionSettings,
                        GroupMember = groupMember,
                        GroupSettings = groupSettings
                    });
                    continue;
                }

                // User village path
                RadarActionSettings userActionSettings = settings.GetActionSettings(actionKey);
                if (!userActionSettings.Monitor) continue;

                pendingNotifications.Add(new PendingPersonNotification
                {
                    Person = person,
                    TargetVillage = targetVillage,
                    ActionKey = actionKey,
                    ActionSettings = userActionSettings,
                    GroupMember = null,
                    GroupSettings = null
                });
            }

            // --- Batch and dispatch notifications ---
            // Group by (source village, target village, action type, group member) so that
            // multiple monks sent in the same scan tick are condensed into one notification.
            Dictionary<string, List<PendingPersonNotification>> groups =
                new Dictionary<string, List<PendingPersonNotification>>();

            foreach (PendingPersonNotification pending in pendingNotifications)
            {
                string key = pending.Person.person.homeVillageID + "|"
                    + pending.TargetVillage + "|"
                    + pending.ActionKey + "|"
                    + (pending.GroupMember != null ? pending.GroupMember.PlayerName : "user");

                if (!groups.ContainsKey(key))
                    groups[key] = new List<PendingPersonNotification>();
                groups[key].Add(pending);
            }

            foreach (List<PendingPersonNotification> group in groups.Values)
            {
                PendingPersonNotification first = group[0];
                int count = group.Count;
                string countSuffix = count > 1 ? " (x" + count + ")" : "";

                string monkCommandLabel = GetMonkCommandLabel(first.Person.person.command);
                string commandSuffix = monkCommandLabel != null ? " - " + monkCommandLabel : "";

                if (count > 1)
                    LogDebug("Batching " + count + " " + GetActionLabel(first.ActionKey) + commandSuffix
                        + " from village [" + first.Person.person.homeVillageID + "]"
                        + " to village [" + first.TargetVillage + "] into a single notification.");

                if (first.GroupMember != null)
                {
                    FireGroupPersonNotification(first.Person, first.TargetVillage, first.ActionKey,
                        first.GroupMember, first.GroupSettings, countSuffix, commandSuffix);
                }
                else
                {
                    string sourceName = GetVillageName(first.Person.person.homeVillageID);
                    string targetName = GetVillageName(first.TargetVillage);
                    string actionLabel = GetActionLabel(first.ActionKey);

                    string message = actionLabel + commandSuffix + " detected!" + countSuffix + "\n" +
                        "From: " + sourceName + " [" + first.Person.person.homeVillageID + "]\n" +
                        "To: " + targetName + " [" + first.TargetVillage + "]";

                    LogWarning(message.Replace("\n", " | "));

                    if (first.ActionSettings.SystemNotify)
                        ShowSystemNotification(actionLabel + commandSuffix + countSuffix, message);

                    if (first.ActionSettings.DiscordNotify && !string.IsNullOrEmpty(settings.DiscordWebhookUrl))
                        DiscordNotifier.SendAsync(settings.DiscordWebhookUrl,
                            "\u26A0 " + actionLabel + commandSuffix + " Incoming!", message, 15105570,
                            string.IsNullOrEmpty(settings.DiscordMentionTag) ? null : settings.DiscordMentionTag);

                    if (first.ActionSettings.SoundNotify)
                        RadarSoundPlayer.Play(first.ActionSettings.SoundFile);

                    if (first.ActionSettings.AutoInterdict)
                        TryAutoInterdict(first.TargetVillage, settings);
                }
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

        private static bool IsAISource(int villageId)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return false;
            return GameEngine.Instance.World.getSpecial(villageId) > 0;
        }

        private string ClassifyArmy(WorldMap.LocalArmyData army)
        {
            int targetVillage = army.targetVillageID;
            VillageData vData = GameEngine.Instance.World.getVillageData(targetVillage);
            bool isAI = IsAISource(army.travelFromVillageID);

            // Reinforcements MUST be checked first — before capital checks — so that
            // friendly/liege lord troops heading for a capital are not misclassified
            // as "Attack (Parish/County/etc.)".
            // Also check army.reinforcements (server boolean) in addition to attackType 13,
            // because liege lord armies can carry reinforcements=true with a different attackType.
            if (army.attackType == 13 || army.reinforcements)
                return ACTION_REINFORCEMENT;

            // Capital-level attacks take priority
            if (vData != null)
            {
                if (vData.countryCapital)
                    return isAI ? ACTION_AI_ATTACK_COUNTRY : ACTION_ATTACK_COUNTRY;
                if (vData.provinceCapital)
                    return isAI ? ACTION_AI_ATTACK_PROVINCE : ACTION_ATTACK_PROVINCE;
                if (vData.countyCapital)
                    return isAI ? ACTION_AI_ATTACK_COUNTY : ACTION_ATTACK_COUNTY;
                if (vData.regionCapital)
                    return isAI ? ACTION_AI_ATTACK_PARISH : ACTION_ATTACK_PARISH;
            }

            // Scouts: scout-only armies
            if (army.numScouts > 0 && army.numPeasants == 0 && army.numArchers == 0 &&
                army.numPikemen == 0 && army.numSwordsmen == 0 && army.numCatapults == 0)
                return isAI ? ACTION_AI_SCOUT : ACTION_SCOUT;

            //// Foraging
            //if (army.attackType == 9)
            //    return isAI ? ACTION_AI_FORAGING : ACTION_FORAGING;

            // Granular attack types
            switch (army.attackType)
            {
                case ATTACK_TYPE_CAPTURE:
                    // Capture requires at least one captain. Zero captains means the
                    // game sent friendly/liege lord troops as a capture packet — treat
                    // as a reinforcement so the user can filter via the Monitor toggle.
                    if (army.numCaptains == 0)
                        return ACTION_REINFORCEMENT;
                    return isAI ? ACTION_AI_CAPTURE : ACTION_CAPTURE;
                case ATTACK_TYPE_PILLAGE_STOCKPILE:
                    return isAI ? ACTION_AI_PILLAGE_STOCKPILE : ACTION_PILLAGE_STOCKPILE;
                case ATTACK_TYPE_RANSACK:
                    return isAI ? ACTION_AI_RANSACK : ACTION_RANSACK;
                case ATTACK_TYPE_PILLAGE_GRANARY:
                    return isAI ? ACTION_AI_PILLAGE_GRANARY : ACTION_PILLAGE_GRANARY;
                case ATTACK_TYPE_PILLAGE_BANQUET:
                    return isAI ? ACTION_AI_PILLAGE_BANQUET : ACTION_PILLAGE_BANQUET;
                case ATTACK_TYPE_PILLAGE_ALE:
                    return isAI ? ACTION_AI_PILLAGE_ALE : ACTION_PILLAGE_ALE;
                case ATTACK_TYPE_PILLAGE_ARMOURY:
                    return isAI ? ACTION_AI_PILLAGE_ARMOURY : ACTION_PILLAGE_ARMOURY;
                case ATTACK_TYPE_RAZE:
                    return isAI ? ACTION_AI_RAZE : ACTION_RAZE;
                case ATTACK_TYPE_VANDALISE:
                    return isAI ? ACTION_AI_VANDALISE : ACTION_VANDALISE;
                case ATTACK_TYPE_GOLD_RAID:
                    return isAI ? ACTION_AI_GOLD_RAID : ACTION_GOLD_RAID;
                default:
                    // Unknown attack type with no captains — treat as reinforcement
                    if (army.numCaptains == 0)
                        return ACTION_REINFORCEMENT;
                    return isAI ? ACTION_AI_CAPTURE : ACTION_CAPTURE;
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
        // Discord notification ignore options
        // =================================================================

        // Decides whether a Discord notification for this incoming army is allowed
        // under the "use ignore options" filter (min army size + max land time). When
        // the filter is off, everything is allowed. Applies to both the user-village
        // path and the group path (min-attacks is interdict-only, never checked here).
        private bool DiscordAllowedByIgnoreOptions(PendingArmyLookup pending, bool useIgnore,
            int minArmySize, int maxLandTimeHours, string tag)
        {
            if (!useIgnore) return true;

            int totalArmySize = pending.NumPeasants + pending.NumArchers +
                pending.NumPikemen + pending.NumSwordsmen +
                pending.NumCatapults + pending.NumCaptains;

            if (totalArmySize < minArmySize)
            {
                LogDebug("[" + tag + "] Discord notify skipped for army " + pending.ArmyID +
                    ": size " + totalArmySize + " below min army size " + minArmySize + ".");
                return false;
            }

            if (maxLandTimeHours > 0 && pending.Army != null)
            {
                DateTime cutoff = VillageMap.getCurrentServerTime().AddHours(maxLandTimeHours);
                if (pending.Army.serverEndTime > cutoff)
                {
                    LogDebug("[" + tag + "] Discord notify skipped for army " + pending.ArmyID +
                        ": lands beyond max land time of " + maxLandTimeHours + "h.");
                    return false;
                }
            }

            return true;
        }

        // =================================================================
        // Auto-interdict filter helpers
        // =================================================================

        private bool HasEnoughAttacksInWindow(int targetVillageId, int minCount, int windowSeconds)
        {
            List<DateTime> arrivals = new List<DateTime>();
            foreach (WorldMap.LocalArmyData a in _trackedArmies.Values)
            {
                if (a.targetVillageID != targetVillageId) continue;
                if (GameEngine.Instance.World.isUserVillage(a.homeVillageID)) continue;
                // Skip pure scout armies
                if (a.numScouts > 0 && a.numPeasants == 0 && a.numArchers == 0 &&
                    a.numPikemen == 0 && a.numSwordsmen == 0 && a.numCatapults == 0) continue;
                arrivals.Add(a.serverEndTime);
            }

            if (arrivals.Count < minCount) return false;

            arrivals.Sort();
            TimeSpan window = TimeSpan.FromSeconds(windowSeconds);
            for (int i = 0; i <= arrivals.Count - minCount; i++)
            {
                if (arrivals[i + minCount - 1] - arrivals[i] <= window)
                    return true;
            }
            return false;
        }

        private bool HasAttackWithinMaxLandTime(int targetVillageId, int maxHours)
        {
            DateTime cutoff = VillageMap.getCurrentServerTime().AddHours(maxHours);
            foreach (WorldMap.LocalArmyData a in _trackedArmies.Values)
            {
                if (a.targetVillageID != targetVillageId) continue;
                if (GameEngine.Instance.World.isUserVillage(a.homeVillageID)) continue;
                if (a.numScouts > 0 && a.numPeasants == 0 && a.numArchers == 0 &&
                    a.numPikemen == 0 && a.numSwordsmen == 0 && a.numCatapults == 0) continue;
                if (a.serverEndTime <= cutoff)
                    return true;
            }
            return false;
        }

        // =================================================================
        // Auto-interdict
        // =================================================================

        private void TryAutoInterdict(int villageId, RadarSettings settings)
        {
            // Don't interdict capitals
            if (GameEngine.Instance.World.isCapital(villageId))
            {
                LogDebug("Auto-interdict: skipping capital village " + villageId);
                return;
            }

            lock (_interdictLock)
            {
                // A retry cycle for this village is already running.
                if (_interdictInProgress.Contains(villageId))
                {
                    LogDebug("Auto-interdict: cycle already in progress for village " + villageId);
                    return;
                }

                // Throttle: don't re-interdict the same village within 30 seconds.
                DateTime last;
                if (_villagesSentInterdict.TryGetValue(villageId, out last)
                    && (DateTime.Now - last).TotalSeconds < 30)
                {
                    LogDebug("Auto-interdict: throttled for village " + villageId);
                    return;
                }
                _villagesSentInterdict[villageId] = DateTime.Now;
                _interdictInProgress.Add(villageId);
            }

            // Run the verify+retry cycle off the tick thread so radar keeps scanning.
            Thread t = new Thread(delegate ()
            {
                try { RunAutoInterdict(villageId, settings); }
                catch (Exception ex) { LogError("Auto-interdict cycle failed: " + ex.Message); }
                finally
                {
                    lock (_interdictLock) { _interdictInProgress.Remove(villageId); }
                }
            });
            t.IsBackground = true;
            t.Name = "Radar Interdict";
            t.Start();
        }

        private void RunAutoInterdict(int villageId, RadarSettings settings)
        {
            int monks = settings.AutoInterdictMonkCount;
            if (monks <= 0) monks = 1;

            string err;
            bool ok = InterdictRunner.Run(villageId, monks, settings.AutoRecruitMonks, 5, "Radar",
                                          delegate { return false; }, out err);

            string name = GameEngine.Instance.World.getVillageName(villageId);

            if (ok)
            {
                // Restock monks back to the research cap after a successful interdict.
                if (settings.AutoRecruitMonks)
                {
                    int cap = ResearchData.ordinationResearchMonkLevels[
                        (int)GameEngine.Instance.World.UserResearchData.Research_Ordination];
                    int athome = 0;
                    int total = GameEngine.Instance.World.countVillagePeople(villageId, 4, ref athome);
                    if (cap - total > 0)
                        InterdictRunner.MakeMonksMore(villageId, cap - total);
                }
            }
            else
            {
                LogError("Auto-interdict: could not interdict " + name + " [" + villageId + "] — " + err);

                // Allow a future event to retry this village.
                lock (_interdictLock) { _villagesSentInterdict.Remove(villageId); }

                if (!string.IsNullOrEmpty(settings.DiscordWebhookUrl))
                    DiscordNotifier.SendAsync(settings.DiscordWebhookUrl,
                        "⛔ Interdict FAILED",
                        "Failed to interdict " + name + " [" + villageId + "] after 5 attempts.\nLast error: " + err,
                        15548997,
                        string.IsNullOrEmpty(settings.DiscordMentionTag) ? null : settings.DiscordMentionTag);
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
                case ACTION_AI_CAPTURE: return "AI Capture";
                case ACTION_AI_RAZE: return "AI Raze";
                case ACTION_AI_RANSACK: return "AI Ransack";
                case ACTION_AI_PILLAGE_STOCKPILE: return "AI Pillage Stockpile";
                case ACTION_AI_PILLAGE_GRANARY: return "AI Pillage Granary";
                case ACTION_AI_PILLAGE_BANQUET: return "AI Pillage Banquet";
                case ACTION_AI_PILLAGE_ALE: return "AI Pillage Ale";
                case ACTION_AI_PILLAGE_ARMOURY: return "AI Pillage Armoury";
                case ACTION_AI_VANDALISE: return "AI Vandalise";
                case ACTION_AI_GOLD_RAID: return "AI Gold Raid";
                case ACTION_AI_SCOUT: return "AI Scout";
                case ACTION_AI_CAPTAIN: return "AI Captain";
                case ACTION_AI_ATTACK_PARISH: return "AI Attack (Parish)";
                case ACTION_AI_ATTACK_COUNTY: return "AI Attack (County)";
                case ACTION_AI_ATTACK_PROVINCE: return "AI Attack (Province)";
                case ACTION_AI_ATTACK_COUNTRY: return "AI Attack (Country)";
                case ACTION_AI_FORAGING: return "AI Foraging";
                default: return actionKey;
            }
        }

        // Maps the PersonData.command value to a human-readable monk action label.
        // Values sourced from AllArmiesPanel2.cs monk command constants.
        public static string GetMonkCommandLabel(int command)
        {
            switch (command)
            {
                case 1: return "Blessing";
                case 2: return "Positive Influence";
                case 3: return "Inquisition";
                case 4: return "Interdiction";
                case 5: return "Restoration";
                case 6: return "Absolution";
                case 7: return "Excommunication";
                case 8: return "Negative Influence";
                default: return null;
            }
        }

        // =================================================================
        // Group member player village lookup
        // =================================================================

        public List<int> ResolvePlayerVillages(string playerName)
        {
            List<int> result = new List<int>();
            if (string.IsNullOrEmpty(playerName)) return result;

            // Serialize lookups — the shared event/result fields can't be reused
            // concurrently by the auto-refresh thread and the manual button.
            lock (_groupLookupLock)
            {
                _groupLookupResult = null;
                _groupLookupEvent.Reset();

                RemoteServices.Instance.set_GetOtherUserVillageIDList_UserCallBack(
                    new RemoteServices.GetOtherUserVillageIDList_UserCallBack(GroupPlayerLookupCallback));
                RemoteServices.Instance.GetOtherUserVillageIDList(playerName);

                if (!_groupLookupEvent.WaitOne(15000))
                {
                    LogWarning("[Group] Player lookup timed out for '" + playerName + "'.");
                    return result;
                }

                GetOtherUserVillageIDList_ReturnType data = _groupLookupResult;
                if (data == null || !data.Success)
                {
                    LogWarning("[Group] Player lookup failed for '" + playerName + "'.");
                    return result;
                }

                if (data.userVillageList != null)
                {
                    foreach (int vid in data.userVillageList)
                        result.Add(vid);
                }
            }

            LogInfo("[Group] Found " + result.Count + " villages for '" + playerName + "'.");
            return result;
        }

        // =================================================================
        // Group member auto-refresh
        // =================================================================

        // Decides whether a refresh is due (on start, then every N minutes) and
        // kicks one off on a background thread. Runs on the radar tick thread, so it
        // must never block — the actual server lookups happen off-thread.
        private void MaybeAutoRefreshGroupMembers(RadarSettings settings)
        {
            GroupRadarSettings g = settings.GroupRadar;
            if (g == null || !g.Enabled) return;
            if (g.Members == null || g.Members.Count == 0) return;
            if (_groupRefreshInProgress) return;

            bool needRefresh;
            if (_lastGroupRefresh == DateTime.MinValue)
            {
                // First tick since the module started ("map load").
                needRefresh = g.RefreshOnStart;
                // Establish the timer baseline now so the periodic interval is
                // measured from start even when the start-refresh is disabled.
                _lastGroupRefresh = DateTime.Now;
            }
            else
            {
                needRefresh = g.AutoRefreshIntervalMinutes > 0 &&
                    (DateTime.Now - _lastGroupRefresh).TotalMinutes >= g.AutoRefreshIntervalMinutes;
            }

            if (!needRefresh) return;

            _lastGroupRefresh = DateTime.Now;
            StartGroupRefresh(g);
        }

        private void StartGroupRefresh(GroupRadarSettings g)
        {
            _groupRefreshInProgress = true;
            List<GroupRadarMember> members = new List<GroupRadarMember>(g.Members);

            Thread t = new Thread(delegate ()
            {
                int updated = 0;
                int failed = 0;
                try
                {
                    foreach (GroupRadarMember m in members)
                    {
                        if (m == null || string.IsNullOrEmpty(m.PlayerName)) continue;

                        // Retry a failed lookup up to GroupRefreshMaxAttempts times before
                        // giving up — the next periodic refresh will try again. A short
                        // delay between attempts gives a flaky server a moment to recover.
                        List<int> villages = null;
                        for (int attempt = 1; attempt <= GroupRefreshMaxAttempts; attempt++)
                        {
                            villages = ResolvePlayerVillages(m.PlayerName);
                            if (villages != null && villages.Count > 0) break;
                            if (attempt < GroupRefreshMaxAttempts)
                            {
                                LogWarning("[Group] Lookup for '" + m.PlayerName + "' failed (attempt " +
                                           attempt + "/" + GroupRefreshMaxAttempts + "); retrying.");
                                Thread.Sleep(GroupRefreshRetryDelayMs);
                            }
                        }

                        // Only overwrite when the lookup actually returned villages — a
                        // failed/timed-out lookup must not blind the radar to a member.
                        if (villages != null && villages.Count > 0)
                        {
                            m.VillageIds = villages;
                            updated++;
                        }
                        else
                        {
                            failed++;
                            LogWarning("[Group] Giving up on '" + m.PlayerName + "' after " +
                                       GroupRefreshMaxAttempts + " attempts; keeping existing villages.");
                        }
                    }

                    try { if (Engine != null && Engine.Settings != null) Engine.Settings.Save(); }
                    catch (Exception ex) { LogWarning("[Group] Auto-refresh save failed: " + ex.Message); }

                    LogInfo("[Group] Auto-refreshed villages for " + updated + "/" +
                            members.Count + " members" +
                            (failed > 0 ? " (" + failed + " failed)" : "") + ".");
                }
                catch (Exception ex)
                {
                    LogError("[Group] Auto-refresh failed: " + ex.Message);
                }
                finally
                {
                    _groupRefreshInProgress = false;
                }
            });
            t.IsBackground = true;
            t.Name = "Radar Group Refresh";
            t.Start();
        }

        private void GroupPlayerLookupCallback(GetOtherUserVillageIDList_ReturnType returnData)
        {
            _groupLookupResult = returnData;
            _groupLookupEvent.Set();
        }

        // Wraps WorldMap.getArmyData so we can immediately sync _trackedArmies after
        // every server response — armies are tracked the instant they arrive rather than
        // waiting up to one full radar tick.
        private void OnGetArmyDataCallback(GetArmyData_ReturnType returnData)
        {
            try
            {
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                    GameEngine.Instance.World.getArmyData(returnData);
            }
            catch (Exception ex)
            {
                LogError("OnGetArmyDataCallback (world handler) error: " + ex.Message);
            }

            try
            {
                SparseArray gameArray = GameEngine.Instance?.World?.getArmyArray();
                if (gameArray != null)
                {
                    foreach (WorldMap.LocalArmyData army in gameArray)
                        if (army != null) _trackedArmies[army.armyID] = army;
                }
            }
            catch (Exception ex)
            {
                LogError("OnGetArmyDataCallback (sync) error: " + ex.Message);
            }
        }

        // Called by AutoBombModule immediately after launchArmy() so that the radar
        // has this army in its tracking dictionary before the game array can drop it.
        public void TrackArmy(WorldMap.LocalArmyData army)
        {
            if (army != null)
                _trackedArmies[army.armyID] = army;
        }

        // Returns outbound user armies targeting the given village that are still
        // within their travel window. Used by RecallAll() to recall armies that may
        // be temporarily absent from the game array due to server reconciliation.
        public List<WorldMap.LocalArmyData> GetTrackedOutboundUserArmies(int targetVillageId)
        {
            List<WorldMap.LocalArmyData> result = new List<WorldMap.LocalArmyData>();
            DateTime serverNow = VillageMap.getCurrentServerTime();
            foreach (WorldMap.LocalArmyData army in _trackedArmies.Values)
            {
                if (army == null) continue;
                if (army.targetVillageID != targetVillageId) continue;
                if (army.lootType >= 0) continue; // returning, not outbound
                if (army.serverEndTime <= serverNow) continue;
                try
                {
                    if (GameEngine.Instance.World.isUserVillage(army.travelFromVillageID))
                        result.Add(army);
                }
                catch { }
            }
            return result;
        }

        protected override void OnShutdown()
        {
            // Restore the game's own callback so army updates still work after radar stops
            try
            {
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                    RemoteServices.Instance.set_GetArmyData_UserCallBack(
                        new RemoteServices.GetArmyData_UserCallBack(
                            GameEngine.Instance.World.getArmyData));
            }
            catch { }

            _knownArmyIds.Clear();
            _knownPersonIds.Clear();
            _incomingAttackTargets.Clear();
            lock (_interdictLock) { _villagesSentInterdict.Clear(); _interdictInProgress.Clear(); }
            _pendingLookups.Clear();
            _trackedArmies.Clear();
            _armyMissingTicks.Clear();
            _trackedPeople.Clear();
            _personMissingTicks.Clear();
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
