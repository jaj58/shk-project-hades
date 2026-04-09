using CommonTypes;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Kingdoms.Bot.Modules
{
    public class AutoBombModule : BotModuleBase
    {
        private bool _launching;
        private bool _interdictDetected;
        private BombAttackEntry _currentEntry;
        private bool _waitingForCallback;

        public override string ModuleName
        {
            get { return "Auto Bomb"; }
        }

        public override TimeSpan Interval
        {
            get
            {
                if (_launching) return TimeSpan.Zero;
                return TimeSpan.FromMilliseconds(500);
            }
        }

        private AutoBombSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.AutoBomb;
                return null;
            }
        }

        public bool IsLaunching
        {
            get { return _launching; }
        }

        public bool InterdictDetected
        {
            get { return _interdictDetected; }
        }

        protected override void OnInitialize()
        {
            _launching = false;
            _interdictDetected = false;
            _waitingForCallback = false;
            _currentEntry = null;
        }

        protected override void OnTick()
        {
            if (!_launching) return;
            if (_waitingForCallback) return;

            AutoBombSettings settings = Settings;
            if (settings == null) return;

            if (settings.PendingAttacks.Count == 0)
            {
                _launching = false;
                LogInfo("No attacks in queue.");
                return;
            }

            DateTime now = DateTime.Now;
            bool allDone = true;

            foreach (BombAttackEntry entry in settings.PendingAttacks)
            {
                if (entry.Cancelled || entry.Sent)
                    continue;

                allDone = false;

                if (entry.ScheduledSendTime <= now)
                {
                    BeginSendAttack(entry, settings);
                    return;
                }
            }

            if (allDone)
            {
                _launching = false;
                LogInfo("All attacks have been sent!");
            }
        }

        // =================================================================
        // Public methods called by UI
        // =================================================================

        public void StartLaunch()
        {
            AutoBombSettings settings = Settings;
            if (settings == null || settings.PendingAttacks.Count == 0)
            {
                LogWarning("No attacks to launch.");
                return;
            }

            foreach (BombAttackEntry entry in settings.PendingAttacks)
            {
                if (string.IsNullOrEmpty(entry.FormationName))
                {
                    LogWarning("Stack " + entry.Stack + " (" + entry.GetSourceName() + ") has no formation set. Aborting.");
                    return;
                }

                // TODO?
                //int[] localCounts = GetTroopsCountArray12(attackSetup);
                // if (castleMap.HasEnoughTroopsToPlace(ref localCounts))

                int[] formationCounts = GetTroopCountsFromFormation(entry.FormationName);
                if (formationCounts == null)
                {
                    LogWarning("Stack " + entry.Stack + ": Could not load formation '" + entry.FormationName + "'. Aborting.");
                    return;
                }

                if (!HasEnoughTroops(entry, formationCounts))
                {
                    LogWarning("Stack " + entry.Stack + " (" + entry.GetSourceName() +
                        ") does not have enough troops for formation '" + entry.FormationName + "'. Aborting.");
                    return;
                }
            }

            _interdictDetected = false;
            _waitingForCallback = false;
            _currentEntry = null;
            CalculateSchedule(settings);
            _launching = true;
            LogInfo("Launch sequence started! " + settings.PendingAttacks.Count + " attack(s) scheduled.");
        }

        public void CancelAll()
        {
            AutoBombSettings settings = Settings;
            if (settings != null)
                CancelRemainingAttacks(settings);
            _launching = false;
            _waitingForCallback = false;
            _currentEntry = null;
            LogInfo("All attacks cancelled.");
        }

        public void RecallAll()
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;

                AutoBombSettings settings = Settings;
                if (settings == null) return;

                RemoteServices.Instance.set_CancelCastleAttack_UserCallBack(null);
                int recalled = 0;
                foreach (WorldMap.LocalArmyData army in GameEngine.Instance.World.getArmyArray())
                {
                    if (army.targetVillageID == settings.TargetVillageId &&
                        GameEngine.Instance.World.isUserVillage(army.travelFromVillageID))
                    {
                        RemoteServices.Instance.CancelCastleAttack(army.armyID);
                        recalled++;
                    }
                }
                LogInfo("Recalled " + recalled + " army/armies.");
            }
            catch (Exception ex)
            {
                LogError("Recall error: " + ex.Message);
            }
        }

        // =================================================================
        // Schedule calculation
        // =================================================================

        private void CalculateSchedule(AutoBombSettings settings)
        {
            List<BombAttackEntry> attacks = settings.PendingAttacks;
            if (attacks.Count == 0) return;

            int stackDelay = settings.StackDelaySeconds;
            if (stackDelay < 1) stackDelay = 1;

            attacks.Sort(delegate(BombAttackEntry a, BombAttackEntry b)
            {
                return b.Stack.CompareTo(a.Stack);
            });

            DateTime now = DateTime.Now;

            foreach (BombAttackEntry entry in attacks)
            {
                entry.Sent = false;
                entry.Cancelled = false;
                entry.Status = "Scheduled";
            }

            double baseArrival = 0;
            foreach (BombAttackEntry entry in attacks)
            {
                double needed = entry.TravelTimeSeconds - (entry.Stack - 1) * stackDelay;
                if (needed > baseArrival)
                    baseArrival = needed;
            }

            baseArrival += 5.0;

            foreach (BombAttackEntry entry in attacks)
            {
                double arrivalOffset = (entry.Stack - 1) * stackDelay;
                double targetArrival = baseArrival + arrivalOffset;
                double sendOffset = targetArrival - entry.TravelTimeSeconds;

                entry.ScheduledSendTime = now.AddSeconds(sendOffset);
                entry.EstimatedArrivalTime = now.AddSeconds(targetArrival);
            }

            foreach (BombAttackEntry entry in attacks)
            {
                TimeSpan sendIn = entry.ScheduledSendTime - now;
                string sendStr = sendIn.TotalSeconds > 0
                    ? FormatTimeSpan(sendIn) + " from now"
                    : "NOW";
                LogInfo("Stack " + entry.Stack + " (" + entry.GetSourceName() + "): send " +
                    sendStr + ", ETA " + entry.EstimatedArrivalTime.ToString("HH:mm:ss"));
            }
        }

        // =================================================================
        // Attack execution
        // =================================================================

        private void BeginSendAttack(BombAttackEntry entry, AutoBombSettings settings)
        {
            LogInfo(ModuleName + ": Launching stack " + entry.Stack + " from " + entry.GetSourceName() +
                " to " + entry.TargetVillageId);    
            try
            {
                if (GameEngine.Instance == null)
                {
                    entry.Status = "Error: No game engine";
                    entry.Cancelled = true;
                    return;
                }

                _currentEntry = entry;
                _waitingForCallback = true;
                entry.Status = "Connecting...";

                int parentVillageId = entry.SourceVillageId;
                LogInfo("Stack " + entry.Stack + ": Initiating PreAttackSetup for " +
                    entry.GetSourceName() + " -> " + entry.TargetVillageId);
                RemoteServices.Instance.set_PreAttackSetup_UserCallBack(
                    new RemoteServices.PreAttackSetup_UserCallBack(PreAttackSetupCallbackBG));
                RemoteServices.Instance.PreAttackSetup(
                    parentVillageId,
                    entry.SourceVillageId,
                    entry.TargetVillageId,
                    0, 0, 0, 0, 0,
                    0,
                    0, 0);
            }
            catch (Exception ex)
            {
                entry.Status = "Error: " + ex.Message;
                entry.Cancelled = true;
                _waitingForCallback = false;
                _currentEntry = null;
                LogError("Stack " + entry.Stack + " PreAttackSetup failed: " + ex.Message);
            }
        }

        private void PreAttackSetupCallbackBG(PreAttackSetup_ReturnType returnData)
        {
            LogInfo("PreAttackSetup callback received for stack " + (_currentEntry != null ? _currentEntry.Stack.ToString() : "null"));

            BombAttackEntry entry = _currentEntry;
            _waitingForCallback = false;
            _currentEntry = null;

            if (entry == null || entry.Cancelled)
                return;

            try
            {
                // --- Protection / error checks (matches reference PreAttackSetupCallbackBG) ---
                if (returnData.protectedVillage)
                {
                    LogWarning("Stack " + entry.Stack + ": Target is protected (interdiction).");
                    entry.Status = "Interdicted";

                    AutoBombSettings settings = Settings;
                    if (settings != null && settings.AutoCancelOnInterdict)
                    {
                        _interdictDetected = true;
                        LogWarning("Auto-cancelling remaining attacks due to interdict.");
                        CancelRemainingAttacks(settings);
                        _launching = false;
                    }
                    else
                    {
                        entry.Cancelled = true;
                    }
                    return;
                }

                if (returnData.vacationVillage)
                {
                    entry.Status = "Target on vacation";
                    entry.Cancelled = true;
                    return;
                }

                if (returnData.peaceVillage)
                {
                    entry.Status = "Target in peace";
                    entry.Cancelled = true;
                    return;
                }

                if (returnData.peaceAttacker)
                {
                    entry.Status = "Attacker in peace";
                    entry.Cancelled = true;
                    return;
                }

                if (returnData.protectedAttacker)
                {
                    entry.Status = "Attacker interdicted";
                    entry.Cancelled = true;
                    return;
                }

                if (!returnData.Success)
                {
                    entry.Status = "Server error: " + returnData.m_errorCode;
                    entry.Cancelled = true;
                    LogError("Stack " + entry.Stack + " server error: " + returnData.m_errorCode);
                    return;
                }

                // --- Honour check (matches reference PreAttackSetupCallbackBG) ---
                int honourCost = 0;
                if (returnData.battleHonourData != null)
                {
                    returnData.battleHonourData.attackType = 11;
                    if (!GameEngine.Instance.World.isCapital(returnData.parentAttackingVillage))
                        honourCost = CastlesCommon.calcBattleHonourCost(
                            returnData.battleHonourData,
                            GameEngine.Instance.LocalWorldData.Alternate_Ruleset,
                            GameEngine.Instance.LocalWorldData.EraWorld);
                }
                if (honourCost > 0 && GameEngine.Instance.World.getCurrentHonour() <= 0.0)
                {
                    entry.Status = "Error: Need honour";
                    entry.Cancelled = true;
                    LogError("Stack " + entry.Stack + ": Not enough honour to attack.");
                    return;
                }

                // --- InitCastleAttackSetup (matches reference) ---
                int pillageLevel = ResearchData.pillageLevels[
                    (int)GameEngine.Instance.World.UserResearchData.Research_Pillaging];
                int ransackLevel = ResearchData.ransackLevels[
                    (int)GameEngine.Instance.World.UserResearchData.Research_Ransack];

                // Default attack type is 11 (normal); override for player villages
                int attackType = 11;
                int pillagePercent = 0;

                if (GameEngine.Instance.World.IsPlayerVillage(returnData.targetVillage))
                {
                    attackType = entry.AttackType;
                    if (attackType == 2 || attackType == 4 || attackType == 5 ||
                        attackType == 6 || attackType == 7)
                    {
                        pillagePercent = pillageLevel;
                    }
                    else if (attackType == 3)
                    {
                        pillagePercent = ransackLevel;
                    }
                }

                // Build CastleMap (matches reference: new CastleMap(-1, GFX, 1))
                LogInfo("Stack " + entry.Stack + ": Initializing castle map for attack.");
                CastleMap castleMap = new CastleMap(-1, GameEngine.Instance.GFX, 1);
                castleMap.SetUsingCastleTroopsOK(true);
                LogInfo("Stack " + entry.Stack + ": Importing defender snapshot.");
                // BG versions avoid sprite grid access that causes IndexOutOfRangeException
                castleMap.importDefenderSnapshot(
                    returnData.castleMapSnapshot,
                    returnData.castleTroopsSnapshot,
                    returnData.keepLevel,
                    true,
                    returnData.landType);
                LogInfo("Stack " + entry.Stack + ": Initializing attack setup.");
                castleMap.initRealSetup(
                    returnData.attackingVillage,
                    returnData.targetVillage,
                    returnData.numPeasants,
                    returnData.numArchers,
                    returnData.numPikemen,
                    returnData.numSwordsmen,
                    returnData.numCatapults,
                    attackType,
                    pillagePercent,
                    returnData.captainsCommand,
                    returnData.parentAttackingVillage,
                    returnData.numPeasantsInCastle,
                    returnData.numArchersInCastle,
                    returnData.numPikemenInCastle,
                    returnData.numSwordsmenInCastle,
                    returnData.targetUserID,
                    returnData.targetUserName,
                    returnData.battleHonourData,
                    returnData.numCaptainsInCastle,
                    returnData.numCaptains,
                    returnData.capitalAttackRate);

                // Load formation (matches reference: cloud preset first, then local)
                if (string.IsNullOrEmpty(entry.FormationName))
                {
                    entry.Status = "Error: No formation";
                    entry.Cancelled = true;
                    LogError("Stack " + entry.Stack + ": No formation specified.");
                    return;
                }

                bool placed = false;

                // Try cloud preset (matches reference mFormationsCloudPresets path)
                CastleMapPreset cloudPreset = FindCloudPreset(entry.FormationName);
                if (cloudPreset != null)
                {
                    LogInfo("Stack " + entry.Stack + ": Applying cloud formation '" +
                        entry.FormationName + "' (" + cloudPreset.ElementCount + " elements).");

                    int[] presetCounts = PresetTroopsCount(cloudPreset);
                    if (castleMap.HasEnoughTroopsToPlace(ref presetCounts))
                    {
                        LogInfo("Stack " + entry.Stack + ": Enough troops for cloud preset, placing formation.");
                        castleMap.RestoreAttackPresetBG(cloudPreset);
                        placed = true;
                    }
                    else
                    {
                        entry.Status = "Error: Not enough troops";
                        entry.Cancelled = true;
                        LogError("Stack " + entry.Stack + ": Not enough troops for formation.");
                        return;
                    }
                }


                // Fall back to local .cas file (matches reference mFormationsLocalPresets path)
                if (!placed)
                {
                    LogInfo("Stack " + entry.Stack + ": Cloud formation not found, trying local file '" +
                        entry.FormationName + ".cas'.");
                    List<CastleMap.RestoreCastleElement> attackSetup =
                        castleMap.getAttackSetup(entry.FormationName);

                    if (attackSetup == null || attackSetup.Count == 0)
                    {
                        entry.Status = "Error: Formation not found";
                        entry.Cancelled = true;
                        LogError("Stack " + entry.Stack + ": Formation '" +
                            entry.FormationName + "' not found in cloud or local.");
                        return;
                    }

                    LogInfo("Stack " + entry.Stack + ": Applying local formation '" +
                        entry.FormationName + "' (" + attackSetup.Count + " elements).");

                    int[] localCounts = GetTroopsCountArray12(attackSetup);
                    if (castleMap.HasEnoughTroopsToPlace(ref localCounts))
                    {
                        LogInfo("Stack " + entry.Stack + ": Enough troops for local formation, placing formation.");
                        castleMap.RestoreAttackSetupBG(attackSetup);
                        placed = true;
                    }
                    else
                    {
                        entry.Status = "Error: Not enough troops";
                        entry.Cancelled = true;
                        LogError("Stack " + entry.Stack + ": Not enough troops for formation.");
                        return;
                    }
                }

                // Verify placement
                if (castleMap.attackNumPeasants == 0 &&
                    castleMap.attackNumArchers == 0 &&
                    castleMap.attackNumPikemen == 0 &&
                    castleMap.attackNumSwordsmen == 0 &&
                    castleMap.attackNumCatapults == 0 &&
                    castleMap.attackNumCaptains == 0)
                {
                    entry.Status = "Error: No troops placed";
                    entry.Cancelled = true;
                    LogError("Stack " + entry.Stack + ": Zero troops placed.");
                    return;
                }

                LogInfo("Stack " + entry.Stack + ": Placed P:" + castleMap.attackNumPeasants +
                    " A:" + castleMap.attackNumArchers + " Pk:" + castleMap.attackNumPikemen +
                    " S:" + castleMap.attackNumSwordsmen + " C:" + castleMap.attackNumCatapults +
                    " Cap:" + castleMap.attackNumCaptains);

                // Set launch parameters and fire (matches reference: setupLaunchArmy then launchArmy)
                castleMap.setupLaunchArmy(attackType, pillagePercent, 0);

                // Verify the troop map has actual attacker data
                byte[] debugTroopMap = castleMap.castleLayout.createAttackerMapArray();
                int troopBytes = 0;
                for (int i = 0; i < debugTroopMap.Length; i++)
                {
                    if (debugTroopMap[i] != 0) troopBytes++;
                }
                byte[] debugCompressed = CastlesCommon.compressCastleData(debugTroopMap);

                // Count attacker elements in the elements list
                int attackerElementCount = 0;
                foreach (CastleElement elem in castleMap.elements)
                {
                    if (elem.elementType >= 90 && elem.elementType <= 94)
                        attackerElementCount++;
                    else if (elem.elementType >= 100 && elem.elementType <= 107)
                        attackerElementCount++;
                }
                LogInfo("Stack " + entry.Stack + ": TroopMap has " + troopBytes +
                    " non-zero bytes, compressed=" + debugCompressed.Length + " bytes" +
                    " elements=" + castleMap.elements.Count +
                    " attackerElements=" + attackerElementCount);

                LogInfo("Stack " + entry.Stack + ": Launching army - type=" + attackType +
                    " pillage=" + pillagePercent +
                    " parent=" + castleMap.ParentOfAttackingVillage +
                    " source=" + castleMap.VillageID +
                    " target=" + castleMap.attackRealTargetVillage);
                castleMap.launchArmy(true);

                entry.Sent = true;
                entry.Status = "Sent @ " + DateTime.Now.ToString("HH:mm:ss");
                LogInfo("Stack " + entry.Stack + " SENT from " + entry.GetSourceName() +
                    " -> " + entry.TargetVillageId +
                    " (ETA: " + entry.EstimatedArrivalTime.ToString("HH:mm:ss") + ")");
            }
            catch (Exception ex)
            {
                entry.Status = "Error: " + ex.Message;
                entry.Cancelled = true;
                LogError("Stack " + entry.Stack + " launch failed: " + ex.Message);
            }
        }

        // =================================================================
        // Formation loading
        // =================================================================

        private static CastleMapPreset FindCloudPreset(string formationName)
        {
            try
            {
                List<CastleMapPreset> presets =
                    PresetManager.Instance.GetPresets(PresetType.TROOP_ATTACK);
                if (presets == null) return null;

                foreach (CastleMapPreset preset in presets)
                {
                    if (preset.Name == formationName)
                        return preset;
                }
            }
            catch { }
            return null;
        }

        // =================================================================
        // Troop counting and validation
        // =================================================================

        private static int[] CountTroopsInFormation(List<CastleMap.RestoreCastleElement> elements)
        {
            // [0]=peasants, [1]=archers, [2]=pikemen, [3]=swordsmen, [4]=catapults, [5]=captains
            int[] counts = new int[6];
            foreach (CastleMap.RestoreCastleElement rce in elements)
            {
                switch (rce.elementType)
                {
                    case 90: counts[0]++; break;
                    case 92: counts[1]++; break;
                    case 93: counts[2]++; break;
                    case 91: counts[3]++; break;
                    case 94: counts[4]++; break;
                    case 100: case 101: case 102: case 103:
                    case 104: case 105: case 106: case 107:
                        counts[5]++; break;
                }
            }
            return counts;
        }

        public static int[] PresetTroopsCount(CastleMapPreset preset)
        {
            List<CastleMap.RestoreCastleElement> list = new List<CastleMap.RestoreCastleElement>();
            string[] strArray1 = preset.Data.Split(' ');
            int num1 = 0;
            for (int index1 = 0; index1 < preset.ElementCount; ++index1)
            {
                CastleMap.RestoreCastleElement restoreCastleElement1 = new CastleMap.RestoreCastleElement();
                CastleMap.RestoreCastleElement restoreCastleElement2 = restoreCastleElement1;
                string[] strArray2 = strArray1;
                int num2 = num1;
                int num3 = num2 + 1;
                int index2 = num2;
                int num4 = (int)Convert.ToByte(strArray2[index2]);
                restoreCastleElement2.xPos = (byte)num4;
                CastleMap.RestoreCastleElement restoreCastleElement3 = restoreCastleElement1;
                string[] strArray3 = strArray1;
                int num5 = num3;
                int num6 = num5 + 1;
                int index3 = num5;
                int num7 = (int)Convert.ToByte(strArray3[index3]);
                restoreCastleElement3.yPos = (byte)num7;
                CastleMap.RestoreCastleElement restoreCastleElement4 = restoreCastleElement1;
                string[] strArray4 = strArray1;
                int num8 = num6;
                num1 = num8 + 1;
                int index4 = num8;
                int num9 = (int)Convert.ToByte(strArray4[index4]);
                restoreCastleElement4.elementType = (byte)num9;
                if (restoreCastleElement1.elementType == (byte)94)
                {
                    CastleMap.RestoreCastleElement restoreCastleElement5 = restoreCastleElement1;
                    string[] strArray5 = strArray1;
                    int num10 = num1;
                    int num11 = num10 + 1;
                    int index5 = num10;
                    int num12 = (int)Convert.ToByte(strArray5[index5]);
                    restoreCastleElement5.targXPos = (byte)num12;
                    CastleMap.RestoreCastleElement restoreCastleElement6 = restoreCastleElement1;
                    string[] strArray6 = strArray1;
                    int num13 = num11;
                    num1 = num13 + 1;
                    int index6 = num13;
                    int num14 = (int)Convert.ToByte(strArray6[index6]);
                    restoreCastleElement6.targYPos = (byte)num14;
                }
                if (restoreCastleElement1.elementType >= (byte)100 && restoreCastleElement1.elementType < (byte)109)
                {
                    restoreCastleElement1.delay = Convert.ToByte(strArray1[num1++]);
                    if (restoreCastleElement1.elementType == (byte)102 || restoreCastleElement1.elementType == (byte)103)
                    {
                        CastleMap.RestoreCastleElement restoreCastleElement5 = restoreCastleElement1;
                        string[] strArray5 = strArray1;
                        int num10 = num1;
                        int num11 = num10 + 1;
                        int index5 = num10;
                        int num12 = (int)Convert.ToByte(strArray5[index5]);
                        restoreCastleElement5.targXPos = (byte)num12;
                        CastleMap.RestoreCastleElement restoreCastleElement6 = restoreCastleElement1;
                        string[] strArray6 = strArray1;
                        int num13 = num11;
                        num1 = num13 + 1;
                        int index6 = num13;
                        int num14 = (int)Convert.ToByte(strArray6[index6]);
                        restoreCastleElement6.targYPos = (byte)num14;
                    }
                    bool flag = false;
                    int researchTactics = (int)GameEngine.Instance.World.UserResearchData.Research_Tactics;
                    switch (restoreCastleElement1.elementType)
                    {
                        case 100:
                            flag = true;
                            break;
                        case 101:
                            if (researchTactics > 0)
                            {
                                flag = true;
                                break;
                            }
                            break;
                        case 102:
                            if (researchTactics > 1)
                            {
                                flag = true;
                                break;
                            }
                            break;
                        case 103:
                            if (researchTactics > 3)
                            {
                                flag = true;
                                break;
                            }
                            break;
                        case 104:
                            if (researchTactics > 2)
                            {
                                flag = true;
                                break;
                            }
                            break;
                    }
                    if (!flag)
                        continue;
                }
                list.Add(restoreCastleElement1);
            }
            return GetTroopsCountArray12(list);
        }

        public static int[] GetTroopsCountArray12(List<CastleMap.RestoreCastleElement> list)
        {
            int[] numArray = new int[12];
            foreach (CastleMap.RestoreCastleElement restoreCastleElement in list)
            {
                switch (restoreCastleElement.elementType)
                {
                    case 70:
                    case 90:
                        ++numArray[0];
                        continue;
                    case 71:
                    case 91:
                        ++numArray[3];
                        continue;
                    case 72:
                    case 92:
                        ++numArray[1];
                        continue;
                    case 73:
                    case 93:
                        ++numArray[2];
                        continue;
                    case 74:
                    case 94:
                        ++numArray[4];
                        continue;
                    case 85:
                    case 100:
                    case 101:
                    case 102:
                    case 103:
                    case 104:
                    case 105:
                    case 106:
                    case 107:
                        ++numArray[5];
                        continue;
                    default:
                        continue;
                }
            }
            return numArray;
        }

        public static int[] GetTroopCountsFromFormation(string formationName)
        {
            try
            {
                // Try cloud preset first
                CastleMapPreset cloudPreset = FindCloudPreset(formationName);
                if (cloudPreset != null)
                    return PresetTroopsCount(cloudPreset);

                // Fall back to local file
                CastleMap tempMap = new CastleMap(-1, GameEngine.Instance.GFX, 1);
                List<CastleMap.RestoreCastleElement> elements =
                    tempMap.getAttackSetup(formationName);
                if (elements == null || elements.Count == 0)
                    return null;
                return CountTroopsInFormation(elements);
            }
            catch
            {
                return null;
            }
        }

        private static bool HasEnoughTroops(BombAttackEntry entry, int[] formationCounts)
        {
            if (formationCounts == null) return false;
            if (entry.NumPeasants < formationCounts[0]) return false;
            if (entry.NumArchers < formationCounts[1]) return false;
            if (entry.NumPikemen < formationCounts[2]) return false;
            if (entry.NumSwordsmen < formationCounts[3]) return false;
            if (entry.NumCatapults < formationCounts[4]) return false;
            if (entry.NumCaptains < formationCounts[5]) return false;
            return true;
        }

        // =================================================================
        // Helpers
        // =================================================================

        private void CancelRemainingAttacks(AutoBombSettings settings)
        {
            foreach (BombAttackEntry entry in settings.PendingAttacks)
            {
                if (!entry.Sent && !entry.Cancelled)
                {
                    entry.Cancelled = true;
                    entry.Status = "Cancelled";
                }
            }
        }

        // =================================================================
        // Travel time calculation (static, used by UI)
        // =================================================================

        public static double CalculateBaseTravelTime(int sourceVillageId, int targetVillageId,
            bool captainsOnly)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return 0;

            WorldData wd = GameEngine.Instance.LocalWorldData;
            Point src = GameEngine.Instance.World.getVillageLocation(sourceVillageId);
            Point tgt = GameEngine.Instance.World.getVillageLocation(targetVillageId);

            double dx = src.X - tgt.X;
            double dy = src.Y - tgt.Y;
            double dist = Math.Sqrt(dx * dx + dy * dy);

            double travelTime;
            bool isCapital = GameEngine.Instance.World.isCapital(sourceVillageId);

            if (isCapital)
            {
                travelTime = dist * (wd.armyMoveSpeed * wd.gamePlaySpeed);
            }
            else if (captainsOnly)
            {
                travelTime = dist * (wd.CaptainsMoveSpeed * wd.gamePlaySpeed *
                    ResearchData.CaptainTimes[
                        (int)GameEngine.Instance.World.UserResearchData.Research_Courtiers]);
            }
            else
            {
                travelTime = dist * (wd.armyMoveSpeed * wd.gamePlaySpeed *
                    ResearchData.ArmyTimes[
                        (int)GameEngine.Instance.World.UserResearchData.Research_ForcedMarch]);
            }

            travelTime = GameEngine.Instance.World.adjustIfIslandTravel(
                travelTime, sourceVillageId, targetVillageId);

            return travelTime;
        }

        public static double ApplyCardSpeed(double baseTime, int cardType)
        {
            switch (cardType)
            {
                case 1: return baseTime * 0.5;
                case 2: return baseTime * 0.25;
                case 3: return baseTime / 6.0;
                default: return baseTime;
            }
        }

        public static double GetCurrentCardModifier()
        {
            try
            {
                if (GameEngine.Instance != null && GameEngine.Instance.cardsManager != null)
                    return CardTypes.getArmySpeed(GameEngine.Instance.cardsManager.UserCardData);
            }
            catch { }
            return 1.0;
        }

        public static List<string> GetFormationNames()
        {
            List<string> names = new List<string>();
            try
            {
                // Cloud presets
                List<CastleMapPreset> cloudPresets =
                    PresetManager.Instance.GetPresets(PresetType.TROOP_ATTACK);
                if (cloudPresets != null)
                {
                    foreach (CastleMapPreset preset in cloudPresets)
                    {
                        if (!string.IsNullOrEmpty(preset.Name))
                            names.Add(preset.Name);
                    }
                }
            }
            catch { }
            try
            {
                // Local .cas files
                string dir = GameEngine.getSettingsPath(true);
                string[] files = Directory.GetFiles(dir, "AttackSetup_*.cas");
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
                    string name = fileName.Replace("AttackSetup_", "");
                    if (name.Length > 0 && !names.Contains(name))
                        names.Add(name);
                }
            }
            catch { }
            return names;
        }

        public static string FormatTimeSpan(TimeSpan ts)
        {
            if (ts.TotalHours >= 1)
                return string.Format("{0}h {1}m {2}s",
                    (int)ts.TotalHours, ts.Minutes, ts.Seconds);
            if (ts.TotalMinutes >= 1)
                return string.Format("{0}m {1}s", (int)ts.TotalMinutes, ts.Seconds);
            return string.Format("{0}s", (int)ts.TotalSeconds);
        }
    }
}
