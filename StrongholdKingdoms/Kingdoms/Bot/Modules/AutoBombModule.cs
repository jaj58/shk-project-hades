using CommonTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Kingdoms.Bot.Modules
{
    public class AutoBombModule : BotModuleBase
    {
        private volatile bool _launching;
        private volatile bool _interdictDetected;

        // Thread infrastructure
        private Thread _launchThread;
        private ManualResetEvent _cancelEvent = new ManualResetEvent(false);
        private ManualResetEvent _callbackEvent = new ManualResetEvent(false);
        private PreAttackSetup_ReturnType _callbackResult;
        private readonly object _lock = new object();

        // Fake send recall timer
        private Timer _fakeSendTimer;
        private volatile bool _fakeSendTriggered;

        public override string ModuleName
        {
            get { return "Auto Bomb"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromMilliseconds(500); }
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
        }

        protected override void OnShutdown()
        {
            StopThread();
        }

        protected override void OnTick()
        {
            // The launch logic now runs in its own thread.
            // OnTick only checks for interdict while launching.
            if (!_launching) return;

            AutoBombSettings settings = Settings;
            if (settings == null) return;

            if (settings.AutoCancelOnInterdict && !_interdictDetected)
            {
                if (CheckForInterdict(settings.TargetVillageId))
                {
                    _interdictDetected = true;
                    LogWarning("Interdict detected on target! Cancelling remaining attacks and recalling sent armies.");
                    CancelAll();
                    RecallAll();
                }
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

                int[] formationCounts = GetTroopCountsFromFormation(entry.FormationName);
                if (formationCounts == null)
                {
                    LogWarning("Stack " + entry.Stack + ": Could not load formation '" + entry.FormationName + "'. Aborting.");
                    return;
                }

                if (!HasEnoughTroops(entry, formationCounts))
                {
                    LogWarning("Stack " + entry.Stack + " (" + entry.GetSourceName() +
                        ") does not have enough troops for formation '" + entry.FormationName + "'. Aborting launch.");
                    return;
                }
            }

            _interdictDetected = false;
            _fakeSendTriggered = false;
            DisposeFakeSendTimer();
            CalculateSchedule(settings);

            // Stop any existing thread
            StopThread();

            // Start the launch thread
            _cancelEvent.Reset();
            _launching = true;
            _launchThread = new Thread(LaunchThreadProc);
            _launchThread.IsBackground = true;
            _launchThread.Name = "AutoBomb Launch";
            _launchThread.Start(settings);
            LogInfo("Launch sequence started on background thread! " + settings.PendingAttacks.Count + " attack(s) scheduled.");
        }

        public void CancelAll()
        {
            AutoBombSettings settings = Settings;
            if (settings != null)
                CancelRemainingAttacks(settings);
            StopThread();
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
        // Thread management
        // =================================================================

        private void StopThread()
        {
            _launching = false;
            _cancelEvent.Set();
            _callbackEvent.Set(); // Unblock any waiting callback
            DisposeFakeSendTimer();

            if (_launchThread != null && _launchThread.IsAlive)
            {
                _launchThread.Join(5000);
                if (_launchThread.IsAlive)
                {
                    try { _launchThread.Abort(); } catch { }
                }
            }
            _launchThread = null;
        }

        // =================================================================
        // Background launch thread
        // =================================================================

        private void LaunchThreadProc(object state)
        {
            AutoBombSettings settings = (AutoBombSettings)state;

            try
            {
                LogInfo("[Thread] Launch thread started.");

                // Sort attacks by scheduled send time (earliest first)
                List<BombAttackEntry> attacks = new List<BombAttackEntry>(settings.PendingAttacks);
                attacks.Sort(delegate(BombAttackEntry a, BombAttackEntry b)
                {
                    return a.ScheduledSendTime.CompareTo(b.ScheduledSendTime);
                });

                foreach (BombAttackEntry entry in attacks)
                {
                    if (_cancelEvent.WaitOne(0))
                    {
                        LogInfo("[Thread] Cancelled.");
                        break;
                    }

                    if (entry.Cancelled || entry.Sent) continue;

                    // Wait until PREPARE_AHEAD_SECONDS before the scheduled send time
                    double prepareAheadSeconds = 8.0;
                    DateTime prepareTime = entry.ScheduledSendTime.AddSeconds(-prepareAheadSeconds);
                    TimeSpan waitForPrepare = prepareTime - DateTime.Now;
                    if (waitForPrepare.TotalMilliseconds > 0)
                    {
                        LogInfo("[Thread] Stack " + entry.Stack + ": Waiting " +
                            FormatTimeSpan(waitForPrepare) + " before preparing.");
                        if (_cancelEvent.WaitOne((int)waitForPrepare.TotalMilliseconds))
                        {
                            LogInfo("[Thread] Cancelled during wait.");
                            break;
                        }
                    }

                    if (_cancelEvent.WaitOne(0) || entry.Cancelled) break;

                    // Phase 1: PreAttackSetup + build CastleMap
                    LogInfo("[Thread] Stack " + entry.Stack + ": Starting preparation.");
                    entry.PreparationStarted = true;
                    entry.Status = "Preparing...";

                    bool prepOk = PrepareAttackOnThread(entry, settings);
                    if (!prepOk)
                    {
                        LogError("[Thread] Stack " + entry.Stack + ": Preparation failed.");
                        continue;
                    }

                    if (_cancelEvent.WaitOne(0) || entry.Cancelled) break;

                    // Phase 2: Wait until exact send time then fire
                    TimeSpan waitForFire = entry.ScheduledSendTime - DateTime.Now;
                    if (waitForFire.TotalMilliseconds > 0)
                    {
                        LogInfo("[Thread] Stack " + entry.Stack + ": Prepared. Waiting " +
                            FormatTimeSpan(waitForFire) + " to fire.");
                        if (_cancelEvent.WaitOne((int)waitForFire.TotalMilliseconds))
                        {
                            LogInfo("[Thread] Cancelled during fire wait.");
                            break;
                        }
                    }

                    if (_cancelEvent.WaitOne(0) || entry.Cancelled) break;

                    // Fire!
                    FirePreparedAttack(entry);

                    // Small delay between attacks to avoid overwhelming the server
                    if (_cancelEvent.WaitOne(500))
                        break;
                }

                LogInfo("[Thread] Launch thread finished.");
            }
            catch (ThreadAbortException)
            {
                LogInfo("[Thread] Thread aborted.");
            }
            catch (Exception ex)
            {
                LogError("[Thread] Unhandled error: " + ex.Message);
            }
            finally
            {
                _launching = false;
            }
        }

        // =================================================================
        // Phase 1: Prepare attack (PreAttackSetup + build CastleMap)
        //          Runs on the background thread.
        // =================================================================

        private bool PrepareAttackOnThread(BombAttackEntry entry, AutoBombSettings settings)
        {
            try
            {
                if (GameEngine.Instance == null)
                {
                    entry.Status = "Error: No game engine";
                    entry.Cancelled = true;
                    return false;
                }

                // Reset the callback event
                _callbackEvent.Reset();
                _callbackResult = null;

                int parentVillageId = entry.SourceVillageId;
                RemoteServices.Instance.set_PreAttackSetup_UserCallBack(
                    new RemoteServices.PreAttackSetup_UserCallBack(PreAttackSetupCallbackBG));
                RemoteServices.Instance.PreAttackSetup(
                    parentVillageId,
                    entry.SourceVillageId,
                    entry.TargetVillageId,
                    0, 0, 0, 0, 0,
                    11,
                    0, 0);

                LogInfo("[Thread] Stack " + entry.Stack + ": Waiting for PreAttackSetup callback...");

                // Wait for the callback with a timeout (30 seconds)
                int waitIndex = WaitHandle.WaitAny(
                    new WaitHandle[] { _callbackEvent, _cancelEvent }, 30000);

                if (waitIndex == 1 || waitIndex == WaitHandle.WaitTimeout)
                {
                    if (waitIndex == WaitHandle.WaitTimeout)
                    {
                        entry.Status = "Error: Callback timeout";
                        entry.Cancelled = true;
                        LogError("[Thread] Stack " + entry.Stack + ": PreAttackSetup callback timed out.");
                    }
                    return false;
                }

                PreAttackSetup_ReturnType returnData = _callbackResult;
                if (returnData == null)
                {
                    entry.Status = "Error: No callback data";
                    entry.Cancelled = true;
                    return false;
                }

                // --- Protection / error checks ---
                if (returnData.protectedVillage)
                {
                    LogWarning("[Thread] Stack " + entry.Stack + ": Target is protected (interdiction).");
                    entry.Status = "Interdicted";

                    if (settings.AutoCancelOnInterdict)
                    {
                        _interdictDetected = true;
                        LogWarning("[Thread] Auto-cancelling remaining attacks and recalling sent armies due to interdict.");
                        CancelRemainingAttacks(settings);
                        RecallAll();
                        _cancelEvent.Set();
                    }
                    else
                    {
                        entry.Cancelled = true;
                    }
                    return false;
                }

                if (returnData.vacationVillage)
                {
                    entry.Status = "Target on vacation";
                    entry.Cancelled = true;
                    return false;
                }

                if (returnData.peaceVillage)
                {
                    entry.Status = "Target in peace";
                    entry.Cancelled = true;
                    return false;
                }

                if (returnData.peaceAttacker)
                {
                    entry.Status = "Attacker in peace";
                    entry.Cancelled = true;
                    return false;
                }

                if (returnData.protectedAttacker)
                {
                    entry.Status = "Attacker interdicted";
                    entry.Cancelled = true;
                    return false;
                }

                if (!returnData.Success)
                {
                    entry.Status = "Server error: " + returnData.m_errorCode;
                    entry.Cancelled = true;
                    LogError("[Thread] Stack " + entry.Stack + " server error: " + returnData.m_errorCode);
                    return false;
                }

                // --- Honour check ---
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
                    LogError("[Thread] Stack " + entry.Stack + ": Not enough honour to attack.");
                    return false;
                }

                // --- Determine attack type and pillage ---
                int pillageLevel = ResearchData.pillageLevels[
                    (int)GameEngine.Instance.World.UserResearchData.Research_Pillaging];
                int ransackLevel = ResearchData.ransackLevels[
                    (int)GameEngine.Instance.World.UserResearchData.Research_Ransack];

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

                // --- Build CastleMap and place formation ---
                LogInfo("[Thread] Stack " + entry.Stack + ": Building castle map.");
                CastleMap castleMap = new CastleMap(-1, GameEngine.Instance.GFX, 1);
                castleMap.SetUsingCastleTroopsOK(true);
                castleMap.importDefenderSnapshot(
                    returnData.castleMapSnapshot,
                    returnData.castleTroopsSnapshot,
                    returnData.keepLevel,
                    true,
                    returnData.landType);
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

                // Load formation
                if (string.IsNullOrEmpty(entry.FormationName))
                {
                    entry.Status = "Error: No formation";
                    entry.Cancelled = true;
                    LogError("[Thread] Stack " + entry.Stack + ": No formation specified.");
                    return false;
                }

                bool placed = false;

                // Try cloud preset first
                CastleMapPreset cloudPreset = FindCloudPreset(entry.FormationName);
                if (cloudPreset != null)
                {
                    LogInfo("[Thread] Stack " + entry.Stack + ": Applying cloud formation '" +
                        entry.FormationName + "' (" + cloudPreset.ElementCount + " elements).");

                    int[] presetCounts = PresetTroopsCount(cloudPreset);
                    if (castleMap.HasEnoughTroopsToPlace(ref presetCounts))
                    {
                        castleMap.RestoreAttackPresetBG(cloudPreset);
                        placed = true;
                    }
                    else
                    {
                        entry.Status = "Error: Not enough troops";
                        entry.Cancelled = true;
                        LogError("[Thread] Stack " + entry.Stack + ": Not enough troops for formation.");
                        return false;
                    }
                }

                // Fall back to local .cas file
                if (!placed)
                {
                    LogInfo("[Thread] Stack " + entry.Stack + ": Trying local formation '" +
                        entry.FormationName + "'.");
                    List<CastleMap.RestoreCastleElement> attackSetup =
                        castleMap.getAttackSetup(entry.FormationName);

                    if (attackSetup == null || attackSetup.Count == 0)
                    {
                        entry.Status = "Error: Formation not found";
                        entry.Cancelled = true;
                        LogError("[Thread] Stack " + entry.Stack + ": Formation '" +
                            entry.FormationName + "' not found in cloud or local.");
                        return false;
                    }

                    int[] localCounts = GetTroopsCountArray12(attackSetup);
                    if (castleMap.HasEnoughTroopsToPlace(ref localCounts))
                    {
                        castleMap.RestoreAttackSetupBG(attackSetup);
                        placed = true;
                    }
                    else
                    {
                        entry.Status = "Error: Not enough troops";
                        entry.Cancelled = true;
                        LogError("[Thread] Stack " + entry.Stack + ": Not enough troops for formation.");
                        return false;
                    }
                }

                // Verify troops were placed
                if (castleMap.attackNumPeasants == 0 &&
                    castleMap.attackNumArchers == 0 &&
                    castleMap.attackNumPikemen == 0 &&
                    castleMap.attackNumSwordsmen == 0 &&
                    castleMap.attackNumCatapults == 0 &&
                    castleMap.attackNumCaptains == 0)
                {
                    entry.Status = "Error: No troops placed";
                    entry.Cancelled = true;
                    LogError("[Thread] Stack " + entry.Stack + ": Zero troops placed.");
                    return false;
                }

                LogInfo("[Thread] Stack " + entry.Stack + ": Prepared P:" + castleMap.attackNumPeasants +
                    " A:" + castleMap.attackNumArchers + " Pk:" + castleMap.attackNumPikemen +
                    " S:" + castleMap.attackNumSwordsmen + " C:" + castleMap.attackNumCatapults +
                    " Cap:" + castleMap.attackNumCaptains);

                // Store the prepared CastleMap
                entry.PreparedCastleMap = castleMap;
                entry.PreparedAttackType = attackType;
                entry.PreparedPillagePercent = pillagePercent;
                entry.Prepared = true;
                entry.Status = "Ready (waiting to fire)";
                LogInfo("[Thread] Stack " + entry.Stack + ": Prepared and waiting for launch time.");
                return true;
            }
            catch (Exception ex)
            {
                entry.Status = "Error: " + ex.Message;
                entry.Cancelled = true;
                LogError("[Thread] Stack " + entry.Stack + " preparation failed: " + ex.Message);
                return false;
            }
        }

        private void PreAttackSetupCallbackBG(PreAttackSetup_ReturnType returnData)
        {
            LogInfo("[Thread] PreAttackSetup callback received.");
            _callbackResult = returnData;
            _callbackEvent.Set();
        }

        // =================================================================
        // Phase 2: Fire the prepared attack
        //          Runs on the background thread.
        // =================================================================

        private void FirePreparedAttack(BombAttackEntry entry)
        {
            try
            {
                CastleMap castleMap = entry.PreparedCastleMap;

                LogInfo("[Thread] Stack " + entry.Stack + ": === FIRING ===");
                LogInfo("[Thread] Stack " + entry.Stack + ": Troops P:" + castleMap.attackNumPeasants +
                    " A:" + castleMap.attackNumArchers + " Pk:" + castleMap.attackNumPikemen +
                    " S:" + castleMap.attackNumSwordsmen + " C:" + castleMap.attackNumCatapults +
                    " Cap:" + castleMap.attackNumCaptains);

                // Override attack type for special/AI villages
                int launchAttackType = entry.PreparedAttackType;
                int launchPillagePercent = entry.PreparedPillagePercent;
                VillageData targetData = GameEngine.Instance.World.getVillageData(entry.TargetVillageId);
                if (targetData != null && targetData.special != 0)
                {
                    LogInfo("[Thread] Stack " + entry.Stack + ": Target is special village (special=" +
                        targetData.special + "), overriding attackType to 11.");
                    launchAttackType = 11;
                    launchPillagePercent = 0;
                }

                LogInfo("[Thread] Stack " + entry.Stack + ": Firing army - type=" + launchAttackType +
                    " pillage=" + launchPillagePercent);

                castleMap.setupLaunchArmy(launchAttackType, launchPillagePercent, 0);
                castleMap.launchArmy(true);

                entry.Sent = true;
                entry.PreparedCastleMap = null;
                entry.Status = "Sent @ " + DateTime.Now.ToString("HH:mm:ss");
                LogInfo("[Thread] Stack " + entry.Stack + " SENT from " + entry.GetSourceName() +
                    " -> " + entry.TargetVillageId +
                    " (ETA: " + entry.EstimatedArrivalTime.ToString("HH:mm:ss") + ")");

                // Start the fake send recall timer on the first successful send
                StartFakeSendTimerIfNeeded();
            }
            catch (Exception ex)
            {
                entry.Status = "Error: " + ex.Message;
                entry.Cancelled = true;
                entry.PreparedCastleMap = null;
                LogError("[Thread] Stack " + entry.Stack + " fire failed: " + ex.Message);
            }
        }

        // =================================================================
        // Fake send: cancel & recall after 4 minutes
        // =================================================================

        private void StartFakeSendTimerIfNeeded()
        {
            AutoBombSettings settings = Settings;
            if (settings == null || !settings.FakeSendEnabled) return;
            if (_fakeSendTriggered) return; // already started
            if (_fakeSendTimer != null) return;

            _fakeSendTimer = new Timer(FakeSendTimerCallback, null,
                TimeSpan.FromMinutes(4), TimeSpan.FromMilliseconds(-1)); // one-shot
            LogInfo("[FakeSend] Recall timer started — armies will be recalled in 4 minutes.");
        }

        private void FakeSendTimerCallback(object state)
        {
            if (_fakeSendTriggered) return;
            _fakeSendTriggered = true;

            LogWarning("[FakeSend] 4 minutes elapsed — cancelling remaining attacks and recalling all armies.");
            CancelAll();
            RecallAll();
        }

        private void DisposeFakeSendTimer()
        {
            if (_fakeSendTimer != null)
            {
                _fakeSendTimer.Dispose();
                _fakeSendTimer = null;
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
                entry.PreparationStarted = false;
                entry.Prepared = false;
                entry.PreparedCastleMap = null;
                entry.PreparedAttackType = 0;
                entry.PreparedPillagePercent = 0;
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
                CastleMapPreset cloudPreset = FindCloudPreset(formationName);
                if (cloudPreset != null)
                    return PresetTroopsCount(cloudPreset);

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

        private bool HasEnoughTroops(BombAttackEntry entry, int[] formationCounts)
        {
            if (formationCounts == null) return false;

            string[] names = { "Peasants", "Archers", "Pikemen", "Swordsmen", "Catapults", "Captains" };
            int[] have = { entry.NumPeasants, entry.NumArchers, entry.NumPikemen,
                           entry.NumSwordsmen, entry.NumCatapults, entry.NumCaptains };

            bool enough = true;
            List<string> shortages = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                if (have[i] < formationCounts[i])
                {
                    shortages.Add(names[i] + ": have " + have[i] + " need " + formationCounts[i]);
                    enough = false;
                }
            }

            if (!enough)
            {
                LogWarning("Stack " + entry.Stack + " (" + entry.GetSourceName() +
                    ") troop check — Formation requires: P=" + formationCounts[0] +
                    " A=" + formationCounts[1] + " Pk=" + formationCounts[2] +
                    " S=" + formationCounts[3] + " C=" + formationCounts[4] +
                    " Cap=" + formationCounts[5] +
                    " | Village has: P=" + have[0] + " A=" + have[1] +
                    " Pk=" + have[2] + " S=" + have[3] + " C=" + have[4] +
                    " Cap=" + have[5] +
                    " | Missing: " + string.Join(", ", shortages.ToArray()));
            }

            return enough;
        }

        // =================================================================
        // Interdict detection
        // =================================================================

        private bool CheckForInterdict(int targetVillageId)
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                    return false;

                return GameEngine.Instance.World.isVillageInterdictProtected(targetVillageId);
            }
            catch { }
            return false;
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
