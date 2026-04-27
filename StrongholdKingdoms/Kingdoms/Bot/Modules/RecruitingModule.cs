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

        public static readonly string[] CapitalUnitKeys = new string[]
        {
            "Peasants",
            "Archers",
            "Pikemen",
            "Swordsmen",
            "Catapults"
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
                case "Monks": return village.calcTotalMonksAtHome() + village.LocallyMadeMonks;
                case "Traders": return village.calcTotalTradersAtHome() + village.LocallyMade_Traders;
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
            // Account for locally queued troops not yet confirmed by server.
            // Each unit type costs its UnitSize slots (most cost 1, Scouts/Monks/Traders vary).
            WorldData wd = GameEngine.Instance.LocalWorldData;
            int locallyMade = village.LocallyMade_Peasants + village.LocallyMade_Archers +
                              village.LocallyMade_Pikemen + village.LocallyMade_Swordsmen +
                              village.LocallyMade_Catapults + village.LocallyMade_Captains +
                              village.LocallyMade_Scouts * wd.UnitSize_Scout +
                              village.LocallyMadeMonks * wd.UnitSize_Priests +
                              village.LocallyMade_Traders * wd.UnitSize_Trader;
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
                // Village queue exhausted � process vassals before starting a new cycle
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

            // Capitals use a completely different gold source, cost formula, army cap, and
            // research tree — route them to their own method rather than bolting on conditionals.
            if (GameEngine.Instance.World != null && GameEngine.Instance.World.isCapital(villageId))
            {
                ProcessCapital(villageId, settings, village);
                return;
            }

            VillageRecruitSettings villageSetting = settings.GetVillageSettings(villageId);
            if (villageSetting == null) return;

            // Sum of locally queued army troops (not yet server-confirmed).
            // These consume spare workers and army slots immediately on queue, so subtract
            // them from both limits now — the old code handled this by decrementing local
            // variables within a single pass; our tick-per-type design must do it upfront.
            int locallyMadeArmyTroops = village.LocallyMade_Peasants + village.LocallyMade_Archers +
                                        village.LocallyMade_Pikemen + village.LocallyMade_Swordsmen +
                                        village.LocallyMade_Catapults + village.LocallyMade_Captains;

            int sparePeasants  = Math.Max(GetSparePeasants(village) - locallyMadeArmyTroops, 0);
            int spareUnitSpace = GetSpareUnitSpace(village);
            int maxArmySize    = GetCommandResearchArmyLimit();
            int totalTroops    = village.calcTotalTroops() + locallyMadeArmyTroops;
            int spareArmySlots = Math.Max(maxArmySize - totalTroops, 0);

            LogDebug("Village " + villageId + ": peasants=" + sparePeasants +
                     " (locallyMadeArmy=" + locallyMadeArmyTroops + ")" +
                     ", unitSpace=" + spareUnitSpace +
                     ", troops=" + totalTroops + "/" + maxArmySize);

            // Only bail out on unit space — non-army types (Monks/Traders/Scouts) can still
            // be recruited even when peasants are 0 or the army is full.
            if (spareUnitSpace <= 0)
            {
                LogDebug("Village " + villageId + " has no spare unit space, skipping.");
                return;
            }

            int currentGold = (int)GameEngine.Instance.World.getCurrentGold();

            // Read armoury stock levels for weapon-capped troop types.
            // Armour is shared between Pikemen and Swordsmen — whichever type recruits
            // first on a given tick reduces the pool; the server reflects updated levels
            // on the next poll, so cross-tick depletion is handled naturally.
            VillageMap.ArmouryLevels armoury = new VillageMap.ArmouryLevels();
            village.getArmouryLevels(armoury);
            int bowsAvail      = (int)armoury.bowsLevel;
            int pikesAvail     = (int)armoury.pikesLevel;
            int swordsAvail    = (int)armoury.swordsLevel;
            int armourAvail    = (int)armoury.armourLevel;
            int catapultsAvail = (int)armoury.catapultsLevel;

            List<UnitRecruitEntry> sorted = new List<UnitRecruitEntry>(villageSetting.Units);
            sorted.Sort(delegate(UnitRecruitEntry a, UnitRecruitEntry b)
            {
                return a.Priority.CompareTo(b.Priority);
            });

            foreach (UnitRecruitEntry entry in sorted)
            {
                if (entry.TargetCount <= 0) continue;

                int current = GetCurrentCount(village, entry.UnitKey);
                int needed  = entry.TargetCount - current;
                if (needed <= 0) continue;

                // Monks/Traders/Scouts are people/special types: no research gate, no gold
                // cost, no spare-worker consumption, don't count toward army size limit.
                bool isArmyUnit = (entry.UnitKey != "Monks" &&
                                   entry.UnitKey != "Traders" &&
                                   entry.UnitKey != "Scouts");
                int canRecruit;

                if (isArmyUnit)
                {
                    // Research prerequisite
                    if (!IsResearchDoneForVillage(entry.UnitKey))
                    {
                        LogDebug("Village " + villageId + ": " + entry.UnitKey + " not yet researched, skipping.");
                        continue;
                    }

                    int unitSize   = GetUnitSize(entry.UnitKey);
                    int maxBySpace = spareUnitSpace / unitSize;
                    canRecruit = Math.Min(needed, Math.Min(maxBySpace,
                                     Math.Min(sparePeasants, spareArmySlots)));

                    // Armoury caps
                    switch (entry.UnitKey)
                    {
                        case "Archers":
                            canRecruit = Math.Min(canRecruit, bowsAvail);
                            if (canRecruit < needed)
                                LogDebug("Village " + villageId + ": bows limit Archers to " + canRecruit);
                            break;
                        case "Pikemen":
                            canRecruit = Math.Min(canRecruit, Math.Min(pikesAvail, armourAvail));
                            if (canRecruit < needed)
                                LogDebug("Village " + villageId + ": pikes/armour limit Pikemen to " + canRecruit);
                            break;
                        case "Swordsmen":
                            canRecruit = Math.Min(canRecruit, Math.Min(swordsAvail, armourAvail));
                            if (canRecruit < needed)
                                LogDebug("Village " + villageId + ": swords/armour limit Swordsmen to " + canRecruit);
                            break;
                        case "Catapults":
                            canRecruit = Math.Min(canRecruit, catapultsAvail);
                            if (canRecruit < needed)
                                LogDebug("Village " + villageId + ": catapult parts limit Catapults to " + canRecruit);
                            break;
                    }

                    // Gold cap — captains use a separate scaling cost (increases with each captain made)
                    if (entry.UnitKey == "Captains")
                    {
                        int captainCost = GameEngine.Instance.LocalWorldData.CaptainGoldCost *
                                          GameEngine.Instance.World.getNumMadeCaptains();
                        if (currentGold < captainCost)
                        {
                            LogDebug("Village " + villageId + ": not enough gold for Captain" +
                                     " (need " + captainCost + ", have " + currentGold + ").");
                            continue;
                        }
                        canRecruit = 1; // always hire one captain at a time
                    }
                    else
                    {
                        int baseCost     = GetBarracksCost(entry.UnitKey);
                        int adjustedCost = CardTypes.adjustTroopCost(
                            GameEngine.Instance.cardsManager.UserCardData, baseCost);
                        if (adjustedCost > 0 && currentGold < canRecruit * adjustedCost)
                        {
                            canRecruit = currentGold / adjustedCost;
                            LogDebug("Village " + villageId + ": gold limits " +
                                     entry.UnitKey + " to " + canRecruit);
                        }
                    }

                    if (canRecruit <= 0)
                    {
                        LogDebug("Village " + villageId + ": want " + needed + " " + entry.UnitKey +
                                 " but limited to 0 (space/peasants/army/armoury/gold).");
                        continue;
                    }

                    try
                    {
                        village.makeTroops(GetTroopTypeId(entry.UnitKey), canRecruit, true);
                        LogInfo("Village " + villageId + ": recruiting " + canRecruit + " " + entry.UnitKey +
                                " (have " + current + "/" + entry.TargetCount + ")");
                    }
                    catch (Exception ex)
                    {
                        LogError("Village " + villageId + ": failed to recruit " + entry.UnitKey + ": " + ex.Message);
                    }
                }
                else
                {
                    // Non-army types: only constrained by unit space
                    int unitSize   = GetUnitSize(entry.UnitKey);
                    int maxBySpace = spareUnitSpace / unitSize;
                    canRecruit = Math.Min(needed, maxBySpace);
                    if (canRecruit <= 0)
                    {
                        LogDebug("Village " + villageId + ": want " + needed + " " + entry.UnitKey +
                                 " but limited by unit space (max=" + maxBySpace + ")");
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
                            village.makeTroops(GetTroopTypeId(entry.UnitKey), canRecruit, true);
                            LogInfo("Village " + villageId + ": recruiting " + canRecruit + " " + entry.UnitKey +
                                    " (have " + current + "/" + entry.TargetCount + ")");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError("Village " + villageId + ": failed to recruit " + entry.UnitKey + ": " + ex.Message);
                    }
                }

                break; // one type per tick; next tick picks up the next deficit
            }
        }

        /// <summary>
        /// Recruits mercenary troops into a capital village.
        /// Capitals differ from regular villages in four key ways:
        ///   1. Gold comes from m_capitalGold, not the player's regular gold pool
        ///   2. Cost = barracks base cost × MercenaryCostMultiplier (÷2 on 6th age worlds)
        ///   3. No spare-worker requirement (mercenaries bypass the peasant pool)
        ///   4. Army cap = (parishCapitalResearchData.Research_Command + 1) × 25
        ///   5. Research is checked against the capital's own research tree
        /// </summary>
        private void ProcessCapital(int villageId, RecruitingSettings settings, VillageMap village)
        {
            VillageRecruitSettings villageSetting = settings.GetVillageSettings(villageId);
            if (villageSetting == null)
            {
                LogDebug("Capital " + villageId + ": no settings configured, skipping.");
                return;
            }

            int capitalGold = (int)village.m_capitalGold;
            if (capitalGold <= 0)
            {
                LogDebug("Capital " + villageId + ": no capital gold available, skipping.");
                return;
            }

            // Capital army limit from parish research — not the global command research
            int maxArmySize = ((int)village.m_parishCapitalResearchData.Research_Command + 1) * 25;
            int locallyMadeArmyTroops = village.LocallyMade_Peasants + village.LocallyMade_Archers +
                                        village.LocallyMade_Pikemen + village.LocallyMade_Swordsmen +
                                        village.LocallyMade_Catapults + village.LocallyMade_Captains;
            int totalTroops    = village.calcTotalTroops() + locallyMadeArmyTroops;
            int spareArmySlots = Math.Max(maxArmySize - totalTroops, 0);

            LogDebug("Capital " + villageId + ": capitalGold=" + capitalGold +
                     ", troops=" + totalTroops + "/" + maxArmySize);

            if (spareArmySlots <= 0)
            {
                LogDebug("Capital " + villageId + " at max army size (" + maxArmySize + "), skipping.");
                return;
            }

            WorldData wd          = GameEngine.Instance.LocalWorldData;
            int costMultiplier    = wd.MercenaryCostMultiplier;
            if (GameEngine.Instance.World.SixthAgeWorld)
                costMultiplier /= 2;

            // Capitals have their own research tree (parish/county/province research)
            ResearchData capitalResearch = GameEngine.Instance.World.GetResearchDataForCurrentVillage();

            List<UnitRecruitEntry> sorted = new List<UnitRecruitEntry>(villageSetting.Units);
            sorted.Sort(delegate(UnitRecruitEntry a, UnitRecruitEntry b)
            {
                return a.Priority.CompareTo(b.Priority);
            });

            foreach (UnitRecruitEntry entry in sorted)
            {
                // Capitals only take the 5 standard barracks types — no Captains (barracks-only),
                // no Scouts/Monks/Traders (don't apply to capitals)
                if (Array.IndexOf(CapitalUnitKeys, entry.UnitKey) < 0) continue;
                if (entry.TargetCount <= 0) continue;

                if (!IsResearchDoneForCapital(entry.UnitKey, capitalResearch))
                {
                    LogDebug("Capital " + villageId + ": " + entry.UnitKey + " not yet researched, skipping.");
                    continue;
                }

                int current = GetCurrentCount(village, entry.UnitKey);
                int needed  = entry.TargetCount - current;
                if (needed <= 0) continue;

                // Mercenary cost (no armoury check — capitals buy troops directly)
                int baseCost = GetBarracksCost(entry.UnitKey);
                int mercCost = baseCost * costMultiplier;

                int canRecruit = Math.Min(needed, spareArmySlots);
                if (mercCost > 0 && capitalGold < canRecruit * mercCost)
                {
                    canRecruit = capitalGold / mercCost;
                    LogDebug("Capital " + villageId + ": capital gold limits " + entry.UnitKey + " to " + canRecruit);
                }

                if (canRecruit <= 0)
                {
                    LogDebug("Capital " + villageId + ": want " + needed + " " + entry.UnitKey +
                             " but limited (armySlots=" + spareArmySlots +
                             ", capitalGold=" + capitalGold + ")");
                    continue;
                }

                try
                {
                    village.makeTroops(GetTroopTypeId(entry.UnitKey), canRecruit, true);
                    LogInfo("Capital " + villageId + ": recruiting " + canRecruit + " " + entry.UnitKey +
                            " (have " + current + "/" + entry.TargetCount + ")" +
                            " for " + (canRecruit * mercCost) + " capital gold");
                }
                catch (Exception ex)
                {
                    LogError("Capital " + villageId + ": failed to recruit " + entry.UnitKey + ": " + ex.Message);
                }

                break; // one type per tick
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

        /// <summary>Returns the base barracks gold cost for a unit type (before card adjustments).</summary>
        private static int GetBarracksCost(string unitKey)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.LocalWorldData == null) return 0;
            WorldData wd = GameEngine.Instance.LocalWorldData;
            switch (unitKey)
            {
                case "Peasants":  return wd.Barracks_GoldCost_Peasant;
                case "Archers":   return wd.Barracks_GoldCost_Archer;
                case "Pikemen":   return wd.Barracks_GoldCost_Pikeman;
                case "Swordsmen": return wd.Barracks_GoldCost_Swordsman;
                case "Catapults": return wd.Barracks_GoldCost_Catapult;
                default: return 0; // Captains handled separately; Scouts/Monks/Traders have no barracks cost
            }
        }

        /// <summary>
        /// Returns true if the player has researched the prerequisite for the given unit type
        /// in a regular village (uses global user research data).
        /// </summary>
        private static bool IsResearchDoneForVillage(string unitKey)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return false;
            ResearchData r = GameEngine.Instance.World.UserResearchData;
            if (r == null) return false;
            switch (unitKey)
            {
                case "Peasants":  return r.Research_Conscription != 0;
                case "Archers":   return r.Research_LongBow != 0;
                case "Pikemen":   return r.Research_Pike != 0;
                case "Swordsmen": return r.Research_Sword != 0;
                case "Catapults": return r.Research_Catapult != 0;
                case "Captains":  return r.Research_Captains != 0;
                default: return true; // Scouts/Monks/Traders have no research gate
            }
        }

        /// <summary>
        /// Returns true if the given research data (a capital's own research tree) satisfies
        /// the prerequisite for the given unit type.
        /// </summary>
        private static bool IsResearchDoneForCapital(string unitKey, ResearchData r)
        {
            if (r == null) return false;
            switch (unitKey)
            {
                case "Peasants":  return r.Research_Conscription != 0;
                case "Archers":   return r.Research_LongBow != 0;
                case "Pikemen":   return r.Research_Pike != 0;
                case "Swordsmen": return r.Research_Sword != 0;
                case "Catapults": return r.Research_Catapult != 0;
                default: return true;
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
