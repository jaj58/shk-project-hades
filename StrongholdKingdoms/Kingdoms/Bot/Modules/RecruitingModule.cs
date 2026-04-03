using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class RecruitingModule : BotModuleBase
    {
        public static readonly string[] AllUnitKeys = new string[]
        {
            "Peasants",
            "Archers",
            "Pikemen",
            "Swordsmen",
            "Catapults",
            "Captains",
            "Scouts",
            "Monks",
            "Traders"
        };

        public static readonly string[] DisbandOptions = new string[]
        {
            "Peasants",
            "Traders",
            "Scouts",
            "Monks"
        };

        public static int GetTroopTypeId(string unitKey)
        {
            switch (unitKey)
            {
                case "Peasants": return 70;
                case "Archers": return 72;
                case "Pikemen": return 73;
                case "Swordsmen": return 71;
                case "Catapults": return 74;
                case "Captains": return 100;
                case "Scouts": return 76;
                case "Traders": return -5;
                default: return -1;
            }
        }

        public static bool IsPeopleType(string unitKey)
        {
            return unitKey == "Monks";
        }

        public static int GetPeopleTypeId(string unitKey)
        {
            if (unitKey == "Monks") return 4;
            return -1;
        }

        public static int GetDisbandTypeId(string unitKey)
        {
            switch (unitKey)
            {
                case "Peasants": return 70;
                case "Archers": return 72;
                case "Pikemen": return 73;
                case "Swordsmen": return 71;
                case "Catapults": return 74;
                case "Captains": return 100;
                case "Scouts": return 4;
                case "Monks": return 1;
                case "Traders": return 2;
                default: return -1;
            }
        }

        public static bool IsDisbandPeople(string unitKey)
        {
            return unitKey == "Monks" || unitKey == "Traders" || unitKey == "Scouts";
        }

        public static int GetCurrentCount(VillageMap village, string unitKey)
        {
            if (village == null) return 0;
            switch (unitKey)
            {
                case "Peasants": return village.m_numPeasants + village.LocallyMade_Peasants;
                case "Archers": return village.m_numArchers + village.LocallyMade_Archers;
                case "Pikemen": return village.m_numPikemen + village.LocallyMade_Pikemen;
                case "Swordsmen": return village.m_numSwordsmen + village.LocallyMade_Swordsmen;
                case "Catapults": return village.m_numCatapults + village.LocallyMade_Catapults;
                case "Captains": return village.m_numCaptains + village.LocallyMade_Captains;
                case "Scouts": return village.m_numScouts + village.LocallyMade_Scouts;
                case "Monks": return village.calcTotalMonksAtHome();
                case "Traders": return village.calcTotalTradersAtHome();
                default: return 0;
            }
        }

        public static int GetUnitSize(string unitKey)
        {
            if (GameEngine.Instance == null) return 1;
            WorldData wd = GameEngine.Instance.LocalWorldData;
            if (wd == null) return 1;
            switch (unitKey)
            {
                case "Monks": return wd.UnitSize_Priests;
                case "Traders": return wd.UnitSize_Trader;
                case "Scouts": return wd.UnitSize_Scout;
                default: return 1;
            }
        }

        public static int GetSpareUnitSpace(VillageMap village)
        {
            if (village == null || GameEngine.Instance == null) return 0;
            int capacity = GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
            int used = village.calcUnitUsages();
            // Account for locally queued troops not yet confirmed by server
            int locallyMade = village.LocallyMade_Peasants + village.LocallyMade_Archers +
                              village.LocallyMade_Pikemen + village.LocallyMade_Swordsmen +
                              village.LocallyMade_Catapults + village.LocallyMade_Captains +
                              village.LocallyMade_Scouts * GameEngine.Instance.LocalWorldData.UnitSize_Scout;
            return Math.Max(capacity - used - locallyMade, 0);
        }

        public static int GetSparePeasants(VillageMap village)
        {
            if (village == null) return 0;
            return Math.Max(village.m_spareWorkers, 0);
        }

        public static int GetCommandResearchArmyLimit()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return 0;
            int commandLevel = (int)GameEngine.Instance.World.userResearchData.Research_Command;
            return ResearchData.commandResearchTroopLevels[commandLevel];
        }

        private DateTime _lastFullCycle = DateTime.MinValue;
        private int _currentVillageIndex;
        private List<int> _villageQueue = new List<int>();
        private DateTime _lastVillageAction = DateTime.MinValue;

        // Vassal state
        private enum VassalState { Idle, WaitingForArmyInfo, Done }
        private VassalState _vassalState = VassalState.Idle;
        private int _currentVassalIndex;
        private List<int> _vassalQueue = new List<int>();
        private int _pendingVassalVillageId = -1;
        private GetVassalArmyInfo_ReturnType _pendingVassalArmyInfo;
        private DateTime _lastVassalAction = DateTime.MinValue;
        private bool _vassalLoadRequested;

        public override string ModuleName
        {
            get { return "Recruiting"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(3); }
        }

        private RecruitingSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.Recruiting;
                return null;
            }
        }

        protected override void OnInitialize()
        {
            _currentVillageIndex = 0;
            _villageQueue.Clear();
        }

        protected override void OnTick()
        {
            RecruitingSettings settings = Settings;
            if (settings == null) return;

            if (_villageQueue.Count == 0 ||
                _currentVillageIndex >= _villageQueue.Count)
            {
                // Village queue exhausted — process vassals before starting a new cycle
                if (_vassalState != VassalState.Done)
                {
                    ProcessVassalsTick(settings);
                    return;
                }

                if ((DateTime.Now - _lastFullCycle).TotalSeconds < settings.CycleIntervalSeconds)
                    return;

                RefreshVillageQueue();
                RefreshVassalQueue();
                _currentVillageIndex = 0;
                _vassalState = VassalState.Idle;
                _lastFullCycle = DateTime.Now;

                if (_villageQueue.Count == 0 && _vassalQueue.Count == 0)
                {
                    LogDebug("No villages or vassals to recruit in.");
                    _vassalState = VassalState.Done;
                    return;
                }

                if (_villageQueue.Count == 0)
                {
                    // No villages, skip straight to vassals
                    ProcessVassalsTick(settings);
                    return;
                }
            }

            if ((DateTime.Now - _lastVillageAction).TotalMilliseconds < settings.DelayBetweenVillagesMs)
                return;

            int villageId = _villageQueue[_currentVillageIndex];
            _currentVillageIndex++;
            _lastVillageAction = DateTime.Now;

            ProcessVillage(villageId, settings);
        }

        private void RefreshVillageQueue()
        {
            _villageQueue.Clear();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            foreach (WorldMap.UserVillageData uvd in villages)
            {
                int id = uvd.villageID;
                RecruitingSettings settings = Settings;
                if (settings != null && settings.IsVillageEnabled(id))
                    _villageQueue.Add(id);
            }
        }

        private void ProcessVillage(int villageId, RecruitingSettings settings)
        {
            VillageMap village = GameEngine.Instance.getVillage(villageId);
            if (village == null)
            {
                LogDebug("Village " + villageId + " not loaded, skipping.");
                return;
            }

            VillageRecruitSettings villageSetting = settings.GetVillageSettings(villageId);
            if (villageSetting == null) return;

            int sparePeasants = GetSparePeasants(village);
            int spareUnitSpace = GetSpareUnitSpace(village);
            int totalTroops = village.calcTotalTroops();
            int maxArmySize = GetCommandResearchArmyLimit();
            int spareArmySlots = Math.Max(maxArmySize - totalTroops, 0);

            LogDebug("Village " + villageId + ": peasants=" + sparePeasants +
                     ", unitSpace=" + spareUnitSpace +
                     ", troops=" + totalTroops + "/" + maxArmySize);

            if (sparePeasants <= 0)
            {
                LogDebug("Village " + villageId + " has no spare peasants, skipping.");
                return;
            }

            if (spareUnitSpace <= 0)
            {
                LogDebug("Village " + villageId + " has no spare unit space, skipping.");
                return;
            }

            if (spareArmySlots <= 0)
            {
                LogDebug("Village " + villageId + " at max army size (" + maxArmySize + "), skipping.");
                return;
            }

            List<UnitRecruitEntry> sorted = new List<UnitRecruitEntry>(villageSetting.Units);
            sorted.Sort(delegate(UnitRecruitEntry a, UnitRecruitEntry b)
            {
                return a.Priority.CompareTo(b.Priority);
            });

            foreach (UnitRecruitEntry entry in sorted)
            {
                if (entry.TargetCount <= 0) continue;

                int current = GetCurrentCount(village, entry.UnitKey);
                int needed = entry.TargetCount - current;
                if (needed <= 0) continue;

                int unitSize = GetUnitSize(entry.UnitKey);
                int maxBySpace = spareUnitSpace / unitSize;
                int maxByPeasants = sparePeasants;
                int maxByArmy = spareArmySlots;
                int canRecruit = Math.Min(needed, Math.Min(maxBySpace, Math.Min(maxByPeasants, maxByArmy)));

                if (canRecruit <= 0)
                {
                    LogDebug("Village " + villageId + ": want " + needed + " " + entry.UnitKey +
                             " but limited (space=" + maxBySpace + ", peasants=" + maxByPeasants +
                             ", armySlots=" + maxByArmy + ")");
                    continue;
                }

                try
                {
                    if (IsPeopleType(entry.UnitKey))
                    {
                        int monkAmount = Math.Min(canRecruit, 4);
                        village.makePeople(1000 + monkAmount);
                        LogInfo("Village " + villageId + ": recruiting " + monkAmount + " " + entry.UnitKey +
                                " (have " + current + "/" + entry.TargetCount + ")");
                    }
                    else if (entry.UnitKey == "Traders")
                    {
                        int traderAmount = Math.Min(canRecruit, 4);
                        village.makeTroops(-5, traderAmount, true);
                        LogInfo("Village " + villageId + ": recruiting " + traderAmount + " " + entry.UnitKey +
                                " (have " + current + "/" + entry.TargetCount + ")");
                    }
                    else
                    {
                        int troopType = GetTroopTypeId(entry.UnitKey);
                        village.makeTroops(troopType, canRecruit, true);
                        LogInfo("Village " + villageId + ": recruiting " + canRecruit + " " + entry.UnitKey +
                                " (have " + current + "/" + entry.TargetCount + ")");
                    }
                }
                catch (Exception ex)
                {
                    LogError("Village " + villageId + ": failed to recruit " + entry.UnitKey + ": " + ex.Message);
                }

                break;
            }
        }

        private void RefreshVassalQueue()
        {
            _vassalQueue.Clear();
            _currentVassalIndex = 0;
            _pendingVassalVillageId = -1;
            _pendingVassalArmyInfo = null;
            _vassalLoadRequested = false;

            if (GameEngine.Instance == null || GameEngine.Instance.vassalsManager == null)
                return;

            RecruitingSettings settings = Settings;
            if (settings == null) return;

            VassalRecruitingSettings vassalSettings = settings.VassalRecruiting;
            if (vassalSettings == null) return;

            VassalInfo[] vassals = GameEngine.Instance.vassalsManager.GetVassals();
            if (vassals == null || vassals.Length == 0)
            {
                if (!_vassalLoadRequested)
                {
                    _vassalLoadRequested = true;
                    LogDebug("Vassal cache empty, requesting load from server...");
                    RemoteServices.Instance.set_VassalInfo_UserCallBack(
                        new RemoteServices.VassalInfo_UserCallBack(OnVassalInfoCallback));
                    RemoteServices.Instance.VassalInfo(-1);
                }
                return;
            }

            foreach (VassalInfo vi in vassals)
            {
                if (vi.villageID < 0) continue;
                if (vassalSettings.IsVassalEnabled(vi.villageID))
                    _vassalQueue.Add(vi.villageID);
            }

            if (_vassalQueue.Count > 0)
                LogDebug("Vassal queue: " + _vassalQueue.Count + " enabled vassals.");
        }

        private void OnVassalInfoCallback(VassalInfo_ReturnType returnData)
        {
            _vassalLoadRequested = false;
            if (returnData.Success && GameEngine.Instance != null && GameEngine.Instance.vassalsManager != null)
            {
                GameEngine.Instance.vassalsManager.importVassals(returnData.liegeLordInfo, returnData.vassals);
                GameEngine.Instance.vassalsManager.importVassalRequests(returnData.requestsYouveMade, returnData.requestsOfYou);
                GameEngine.Instance.World.updateUserVassals();
                LogDebug("Vassal cache loaded from server: " +
                    (returnData.vassals != null ? returnData.vassals.Length.ToString() : "0") + " vassals.");
            }
            else
            {
                LogWarning("Failed to load vassal info from server.");
            }
        }

        private void ProcessVassalsTick(RecruitingSettings settings)
        {
            if (_vassalQueue.Count == 0)
            {
                _vassalState = VassalState.Done;
                return;
            }

            VassalRecruitingSettings vassalSettings = settings.VassalRecruiting;
            if (vassalSettings == null)
            {
                _vassalState = VassalState.Done;
                return;
            }

            if (_vassalState == VassalState.WaitingForArmyInfo)
            {
                if (_pendingVassalArmyInfo == null)
                    return; // still waiting for callback

                ProcessVassalArmyInfo(_pendingVassalArmyInfo, vassalSettings);
                _pendingVassalArmyInfo = null;
                _pendingVassalVillageId = -1;
                _vassalState = VassalState.Idle;
                _currentVassalIndex++;
                _lastVassalAction = DateTime.Now;
            }

            if (_vassalState == VassalState.Idle)
            {
                if (_currentVassalIndex >= _vassalQueue.Count)
                {
                    _vassalState = VassalState.Done;
                    return;
                }

                if ((DateTime.Now - _lastVassalAction).TotalMilliseconds < settings.DelayBetweenVillagesMs)
                    return;

                int vassalVillageId = _vassalQueue[_currentVassalIndex];
                RequestVassalArmyInfo(vassalVillageId);
            }
        }

        private void RequestVassalArmyInfo(int vassalVillageId)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
            {
                _currentVassalIndex++;
                return;
            }

            VillageData vd = GameEngine.Instance.World.getVillageData(vassalVillageId);
            if (vd == null || vd.connecter < 0)
            {
                LogDebug("Vassal " + vassalVillageId + ": no connecter (liege lord) found, skipping.");
                _currentVassalIndex++;
                return;
            }

            int connecter = vd.connecter;
            VillageMap liegeLordVillage = GameEngine.Instance.getVillage(connecter);
            if (liegeLordVillage == null)
            {
                LogDebug("Vassal " + vassalVillageId + ": liege lord village " + connecter + " not loaded, skipping.");
                _currentVassalIndex++;
                return;
            }

            int totalTroops = liegeLordVillage.m_numPeasants + liegeLordVillage.m_numArchers +
                              liegeLordVillage.m_numPikemen + liegeLordVillage.m_numSwordsmen +
                              liegeLordVillage.m_numCatapults;
            if (totalTroops <= 0)
            {
                LogDebug("Vassal " + vassalVillageId + ": liege lord village " + connecter + " has no troops, skipping.");
                _currentVassalIndex++;
                return;
            }

            _pendingVassalVillageId = vassalVillageId;
            _pendingVassalArmyInfo = null;
            _vassalState = VassalState.WaitingForArmyInfo;

            try
            {
                RemoteServices.Instance.set_GetVassalArmyInfo_UserCallBack(
                    new RemoteServices.GetVassalArmyInfo_UserCallBack(OnGetVassalArmyInfoCallback));
                RemoteServices.Instance.GetVassalArmyInfo(vassalVillageId, 0, -1);
                LogDebug("Vassal " + vassalVillageId + ": requesting army info...");
            }
            catch (Exception ex)
            {
                LogError("Vassal " + vassalVillageId + ": failed to request army info: " + ex.Message);
                _vassalState = VassalState.Idle;
                _currentVassalIndex++;
            }
        }

        private void OnGetVassalArmyInfoCallback(GetVassalArmyInfo_ReturnType returnData)
        {
            _pendingVassalArmyInfo = returnData;
        }

        private void ProcessVassalArmyInfo(GetVassalArmyInfo_ReturnType returnData, VassalRecruitingSettings vassalSettings)
        {
            int vassalVillageId = _pendingVassalVillageId;

            if (returnData == null || !returnData.Success)
            {
                LogWarning("Vassal " + vassalVillageId + ": army info request failed.");
                return;
            }

            if (returnData.vassalVillageID != vassalVillageId)
            {
                LogWarning("Vassal " + vassalVillageId + ": got army info for wrong vassal (" + returnData.vassalVillageID + ").");
                return;
            }

            VillageData vd = GameEngine.Instance.World.getVillageData(vassalVillageId);
            if (vd == null || vd.connecter < 0) return;

            int connecter = vd.connecter;
            VillageMap liegeLordVillage = GameEngine.Instance.getVillage(connecter);
            if (liegeLordVillage == null) return;

            VassalVillageRecruitSettings vs = vassalSettings.GetVassalSettings(vassalVillageId);
            if (vs == null) return;

            // Calculate current troops at vassal (stationed + attacking + enroute)
            int vassalPeasants = returnData.numStationedTroops_Peasants + returnData.numAttackingTroops_Peasants + returnData.numEnrouteTroops_Peasants;
            int vassalArchers = returnData.numStationedTroops_Archers + returnData.numAttackingTroops_Archers + returnData.numEnrouteTroops_Archers;
            int vassalPikemen = returnData.numStationedTroops_Pikemen + returnData.numAttackingTroops_Pikemen + returnData.numEnrouteTroops_Pikemen;
            int vassalSwordsmen = returnData.numStationedTroops_Swordsmen + returnData.numAttackingTroops_Swordsmen + returnData.numEnrouteTroops_Swordsmen;
            int vassalCatapults = returnData.numStationedTroops_Catapults + returnData.numAttackingTroops_Catapults + returnData.numEnrouteTroops_Catapults;

            LogDebug("Vassal " + vassalVillageId + " has: " +
                vassalPeasants + " Peasants, " + vassalArchers + " Archers, " +
                vassalPikemen + " Pikemen, " + vassalSwordsmen + " Swordsmen, " +
                vassalCatapults + " Catapults");

            // Calculate deficits per unit type
            VassalUnitRecruitEntry peasantEntry = vs.GetEntry("Peasants");
            VassalUnitRecruitEntry archerEntry = vs.GetEntry("Archers");
            VassalUnitRecruitEntry pikeEntry = vs.GetEntry("Pikemen");
            VassalUnitRecruitEntry swordEntry = vs.GetEntry("Swordsmen");
            VassalUnitRecruitEntry catEntry = vs.GetEntry("Catapults");

            int needPeasants = Math.Max(peasantEntry.TargetCount - vassalPeasants, 0);
            int needArchers = Math.Max(archerEntry.TargetCount - vassalArchers, 0);
            int needPikemen = Math.Max(pikeEntry.TargetCount - vassalPikemen, 0);
            int needSwordsmen = Math.Max(swordEntry.TargetCount - vassalSwordsmen, 0);
            int needCatapults = Math.Max(catEntry.TargetCount - vassalCatapults, 0);

            // Cap by what liege lord village actually has
            int sendPeasants = Math.Min(needPeasants, liegeLordVillage.m_numPeasants);
            int sendArchers = Math.Min(needArchers, liegeLordVillage.m_numArchers);
            int sendPikemen = Math.Min(needPikemen, liegeLordVillage.m_numPikemen);
            int sendSwordsmen = Math.Min(needSwordsmen, liegeLordVillage.m_numSwordsmen);
            int sendCatapults = Math.Min(needCatapults, liegeLordVillage.m_numCatapults);

            int totalSend = sendPeasants + sendArchers + sendPikemen + sendSwordsmen + sendCatapults;
            if (totalSend <= 0)
            {
                LogDebug("Vassal " + vassalVillageId + ": no troops needed or available.");
                return;
            }

            // Check vassal space (command research limit)
            int vassalMaxTroops = ResearchData.commandResearchTroopLevels[
                (int)GameEngine.Instance.World.userResearchData.Research_Command];
            int vassalSpace = vassalMaxTroops - returnData.TotalTroops;
            if (vassalSpace <= 0)
            {
                LogDebug("Vassal " + vassalVillageId + ": no space at vassal (max=" + vassalMaxTroops + ", has=" + returnData.TotalTroops + ").");
                return;
            }

            if (totalSend > vassalSpace)
            {
                LogDebug("Vassal " + vassalVillageId + ": capping send to " + vassalSpace + " (space limit).");
                // Scale down proportionally
                double scale = (double)vassalSpace / totalSend;
                sendPeasants = (int)(sendPeasants * scale);
                sendArchers = (int)(sendArchers * scale);
                sendPikemen = (int)(sendPikemen * scale);
                sendSwordsmen = (int)(sendSwordsmen * scale);
                sendCatapults = (int)(sendCatapults * scale);
                totalSend = sendPeasants + sendArchers + sendPikemen + sendSwordsmen + sendCatapults;
            }

            // Check minimum troops to send
            int minToSend = vassalSettings.MinTroopsToSend;
            if (totalSend < minToSend && vassalSpace >= minToSend)
            {
                LogDebug("Vassal " + vassalVillageId + ": only " + totalSend + " troops ready, need at least " + minToSend + ". Waiting.");
                return;
            }

            if (totalSend <= 0) return;

            try
            {
                RemoteServices.Instance.SendTroopsToVassal(connecter, vassalVillageId,
                    sendPeasants, sendArchers, sendPikemen, sendSwordsmen, sendCatapults);
                LogInfo("Vassal " + vassalVillageId + ": sent " +
                    sendPeasants + " Peasants, " + sendArchers + " Archers, " +
                    sendPikemen + " Pikemen, " + sendSwordsmen + " Swordsmen, " +
                    sendCatapults + " Catapults from village " + connecter);
            }
            catch (Exception ex)
            {
                LogError("Vassal " + vassalVillageId + ": failed to send troops: " + ex.Message);
            }
        }

        public void DisbandAll(string unitKey)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            foreach (WorldMap.UserVillageData uvd in villages)
            {
                int id = uvd.villageID;
                VillageMap village = GameEngine.Instance.getVillage(id);
                if (village == null) continue;

                int count = GetCurrentCount(village, unitKey);
                if (count <= 0) continue;

                try
                {
                    if (IsDisbandPeople(unitKey))
                    {
                        int disbandType = GetDisbandTypeId(unitKey);
                        village.disbandPeople(disbandType, count);
                    }
                    else
                    {
                        int disbandType = GetDisbandTypeId(unitKey);
                        village.disbandTroops(disbandType, count);
                    }
                    LogInfo("Disbanded " + count + " " + unitKey + " in village " + id);
                }
                catch (Exception ex)
                {
                    LogError("Failed to disband " + unitKey + " in village " + id + ": " + ex.Message);
                }
            }
        }

        protected override void OnShutdown()
        {
            _villageQueue.Clear();
            _currentVillageIndex = 0;
            _vassalQueue.Clear();
            _currentVassalIndex = 0;
            _vassalState = VassalState.Idle;
            _pendingVassalVillageId = -1;
            _pendingVassalArmyInfo = null;
            _vassalLoadRequested = false;
        }
    }
}
