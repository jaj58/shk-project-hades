using System;
using System.Collections;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class CastleRepairModule : BotModuleBase
    {
        private DateTime _lastFullCycle = DateTime.MinValue;
        private int _currentVillageIndex;
        private List<int> _villageQueue = new List<int>();
        private DateTime _lastVillageAction = DateTime.MinValue;
        private Dictionary<int, bool> _pendingRepairOnAttack = new Dictionary<int, bool>();
        private Dictionary<long, int> _trackedAttacks = new Dictionary<long, int>();
        private bool _firstScan = true;
        private bool _cycleInProgress = false;

        // State machine steps matching the proven CastleRepairService sequence:
        //   switch village -> download -> wait castle -> initial commit (refresh damage) ->
        //   wait commit -> autorepair if damaged -> delay -> restore infra -> commit ->
        //   wait commit -> delay -> restore troops -> commit -> wait commit -> done
        private enum RepairStep
        {
            Idle,
            SwitchVillage,
            WaitForCastle,
            InitialCommit,
            WaitInitialCommit,
            WaitAfterInitialCommit,
            AutoRepair,
            WaitAfterAutoRepair,
            RestoreInfrastructure,
            CommitInfra,
            WaitCommitInfra,
            WaitAfterInfra,
            RestoreTroops,
            CommitTroops,
            WaitCommitTroops,
            IncrementalInfraPlace,
            IncrementalInfraWait,
            IncrementalTroopsPlace,
            IncrementalTroopsWait,
            Done
        }

        private RepairStep _step = RepairStep.Idle;
        private int _activeVillageId;
        private VillageCastleRepairSettings _activeSettings;
        private DateTime _stepStarted = DateTime.MinValue;

        // Incremental placement fallback — when a batch commit is rejected by the
        // server, the parsed layout is replayed element-by-element so the buildable
        // pieces still get placed. These hold the layout being replayed.
        private List<CampCastleElement> _incrInfraList;
        private List<CampCastleElementLL> _incrTroopList;
        private int _incrementalIndex;
        private int _incrementalPlaced;
        private const int StepTimeoutMs = 15000;
        private const int ShortDelayMs = 1000;
        private const int LongDelayMs = 2000;

        // =================================================================
        // Memorise state machine — snapshot current castle layout to local
        // .cas files so the repair module can restore from them later.
        // =================================================================

        private enum MemoriseType { Infrastructure, Troops }
        private enum MemoriseStep { Idle, SwitchVillage, WaitForCastle, Memorise }

        private MemoriseStep _memoriseStep = MemoriseStep.Idle;
        private MemoriseType _memoriseType;
        private List<int> _memoriseQueue = new List<int>();
        private int _memoriseIndex;
        private int _memoriseVillageId;
        private int _memorisedCount;
        private DateTime _memoriseStepStarted = DateTime.MinValue;

        public override string ModuleName
        {
            get { return "Castle Repair"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromMilliseconds(300); }
        }

        // Keep ticking while disabled if a manual action is in flight: an active
        // repair (_step), a memorise run (_memoriseStep), or a queued Repair-All
        // (_cycleInProgress). Passive scheduled/attack repairs still require Enabled
        // (gated inside OnTick), so a disabled+idle module does no work.
        public override bool HasPendingWork
        {
            get
            {
                return _step != RepairStep.Idle
                    || _memoriseStep != MemoriseStep.Idle
                    || _cycleInProgress;
            }
        }

        private CastleRepairSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.CastleRepair;
                return null;
            }
        }

        protected override void OnInitialize()
        {
            _currentVillageIndex = 0;
            _villageQueue.Clear();
            _pendingRepairOnAttack.Clear();
            _trackedAttacks.Clear();
            _firstScan = true;
            _step = RepairStep.Idle;
            _cycleInProgress = false;
            _memoriseStep = MemoriseStep.Idle;
            _memoriseQueue.Clear();
        }

        public List<int> GetAllKnownVillageIds()
        {
            List<int> result = new List<int>();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return result;
            try
            {
                List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
                if (villages != null)
                {
                    foreach (WorldMap.UserVillageData v in villages)
                        result.Add(v.villageID);
                }
            }
            catch { }
            return result;
        }

        // =================================================================
        // Tick — drives the repair and memorise state machines
        // =================================================================

        protected override void OnTick()
        {
            CastleRepairSettings settings = Settings;
            if (settings == null) return;

            // Repair state machine takes priority
            if (_step != RepairStep.Idle)
            {
                RunRepairStateMachine();
                // Clear popup suppression the moment the machine reaches Idle — this
                // catches every exit path (Done, per-step abort, exception, timeout).
                if (_step == RepairStep.Idle)
                    EndPopupSuppression();
                return;
            }

            // Memorise state machine runs while no repair is active
            if (_memoriseStep != MemoriseStep.Idle)
            {
                RunMemoriseStateMachine();
                return;
            }

            // Scan for AI attacks — passive work, only when the module is enabled.
            if (Enabled && settings.RepairOnAttack)
            {
                ScanForArrivedAttacks(settings);
                ProcessPendingAttackRepairs(settings);
                if (_step != RepairStep.Idle) return;
            }

            // Normal scheduled cycle
            if (_villageQueue.Count == 0 || _currentVillageIndex >= _villageQueue.Count)
            {
                // Nothing queued to process. Starting a new periodic cycle is passive
                // work, so a disabled module (only ticking for on-demand work) stops here.
                if (!Enabled)
                    return;

                if ((DateTime.Now - _lastFullCycle).TotalSeconds < settings.IntervalSeconds)
                    return;

                RefreshVillageQueue(settings);
                _currentVillageIndex = 0;

                if (_villageQueue.Count == 0)
                    return;

                _cycleInProgress = true;
                LogInfo("Starting scheduled repair cycle: " + _villageQueue.Count + " village(s) queued.");
            }

            if ((DateTime.Now - _lastVillageAction).TotalMilliseconds < settings.DelayBetweenVillagesMs)
                return;

            int villageId = _villageQueue[_currentVillageIndex];
            _currentVillageIndex++;
            _lastVillageAction = DateTime.Now;

            VillageCastleRepairSettings vs = settings.GetVillageSettings(villageId);
            BeginRepairVillage(villageId, vs, false);
        }

        private void RefreshVillageQueue(CastleRepairSettings settings)
        {
            _villageQueue.Clear();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            foreach (WorldMap.UserVillageData uvd in villages)
            {
                int id = uvd.villageID;
                VillageCastleRepairSettings vs = settings.GetVillageSettings(id);
                if (vs.RepairInfrastructure || vs.RepairTroops)
                    _villageQueue.Add(id);
            }
        }

        // =================================================================
        // Attack detection — only AI attacks (getSpecial != 0)
        // =================================================================

        private void ScanForArrivedAttacks(CastleRepairSettings settings)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            SparseArray armyArray = GameEngine.Instance.World.getArmyArray();
            if (armyArray == null) return;

            Dictionary<long, int> currentAttacks = new Dictionary<long, int>();

            foreach (WorldMap.LocalArmyData army in armyArray)
            {
                if (!IsTargetingUser(army.targetVillageID)) continue;

                // Skip scouts, foraging, reinforcements, captains
                if (army.attackType == 11 || army.attackType == 9 ||
                    army.attackType == 12 || army.attackType == 13)
                    continue;

                // Only AI attacks — getSpecial != 0 means AI village
                try
                {
                    if (GameEngine.Instance.World.getSpecial(army.homeVillageID) == 0)
                        continue;
                }
                catch { continue; }

                currentAttacks[army.armyID] = army.targetVillageID;
            }

            if (!_firstScan)
            {
                foreach (long armyId in _trackedAttacks.Keys)
                {
                    if (!currentAttacks.ContainsKey(armyId))
                    {
                        int villageId = _trackedAttacks[armyId];
                        if (!_pendingRepairOnAttack.ContainsKey(villageId))
                        {
                            LogInfo("AI attack arrived at village " + villageId + ", queuing repair.");
                            _pendingRepairOnAttack[villageId] = true;
                        }
                    }
                }
            }
            else
            {
                _firstScan = false;
            }

            _trackedAttacks = currentAttacks;
        }

        private static bool IsTargetingUser(int villageId)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return false;

            List<WorldMap.UserVillageData> userVillages = GameEngine.Instance.World.getUserVillageList();
            if (userVillages == null) return false;

            foreach (WorldMap.UserVillageData uvd in userVillages)
            {
                if (uvd.villageID == villageId) return true;
            }
            return false;
        }

        private void ProcessPendingAttackRepairs(CastleRepairSettings settings)
        {
            if (_pendingRepairOnAttack.Count == 0) return;
            if (!settings.RepairOnAttack) { _pendingRepairOnAttack.Clear(); return; }

            List<int> toProcess = new List<int>(_pendingRepairOnAttack.Keys);

            foreach (int villageId in toProcess)
            {
                VillageCastleRepairSettings vs = settings.GetVillageSettings(villageId);
                if (vs.RepairInfrastructure || vs.RepairTroops)
                {
                    // Remove only this village from pending so remaining attacks are
                    // processed on subsequent ticks after this repair completes.
                    _pendingRepairOnAttack.Remove(villageId);
                    // Remove from the normal queue to prevent a second repair when the
                    // scheduled cycle reaches this village.
                    RemoveFromNormalQueue(villageId);
                    _lastVillageAction = DateTime.Now;
                    BeginRepairVillage(villageId, vs, true);
                    return;
                }
            }
        }

        // Removes a village from the not-yet-processed portion of the normal queue.
        // Only searches from _currentVillageIndex onward so already-processed slots
        // are not affected and list indices remain valid.
        private void RemoveFromNormalQueue(int villageId)
        {
            for (int i = _currentVillageIndex; i < _villageQueue.Count; i++)
            {
                if (_villageQueue[i] == villageId)
                {
                    _villageQueue.RemoveAt(i);
                    return;
                }
            }
        }

        // =================================================================
        // Public — called from UI
        // =================================================================

        public void RepairAllNow()
        {
            CastleRepairSettings settings = Settings;
            if (settings == null) return;
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            _villageQueue.Clear();
            foreach (WorldMap.UserVillageData uvd in villages)
            {
                int id = uvd.villageID;
                VillageCastleRepairSettings vs = settings.GetVillageSettings(id);
                if (vs.RepairInfrastructure || vs.RepairTroops)
                    _villageQueue.Add(id);
            }
            _currentVillageIndex = 0;
            _cycleInProgress = true;
            _lastFullCycle = DateTime.Now;
            _lastVillageAction = DateTime.MinValue;
            LogInfo("Repair all now: queued " + _villageQueue.Count + " village(s).");
        }

        public void MemoriseAllInfrastructure()
        {
            // Unconditional entry log — a click must always produce a log line so a
            // silent failure is impossible to miss.
            LogInfo("Memorise infra requested.");
            if (_step != RepairStep.Idle || _memoriseStep != MemoriseStep.Idle)
            {
                LogWarning("Cannot memorise: a repair or memorise operation is already in progress.");
                return;
            }
            StartMemoriseAll(MemoriseType.Infrastructure);
        }

        public void MemoriseAllTroops()
        {
            LogInfo("Memorise troops requested.");
            if (_step != RepairStep.Idle || _memoriseStep != MemoriseStep.Idle)
            {
                LogWarning("Cannot memorise: a repair or memorise operation is already in progress.");
                return;
            }
            StartMemoriseAll(MemoriseType.Troops);
        }

        private void StartMemoriseAll(MemoriseType type)
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                {
                    LogWarning("Cannot memorise: game world not ready.");
                    return;
                }

                List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
                if (villages == null || villages.Count == 0)
                {
                    LogWarning("Cannot memorise: no villages found.");
                    return;
                }

                _memoriseQueue.Clear();
                foreach (WorldMap.UserVillageData uvd in villages)
                    _memoriseQueue.Add(uvd.villageID);

                _memoriseType = type;
                _memoriseIndex = 0;
                _memorisedCount = 0;
                _memoriseVillageId = _memoriseQueue[0];
                _memoriseStep = MemoriseStep.SwitchVillage;
                _memoriseStepStarted = DateTime.Now;

                string typeName = type == MemoriseType.Infrastructure ? "infrastructure" : "troop";
                LogInfo("Memorising " + typeName + " layouts for " + _memoriseQueue.Count + " village(s)...");
            }
            catch (Exception ex)
            {
                LogError("Memorise start failed: " + ex.Message);
            }
        }

        // =================================================================
        // Repair state machine — the core repair sequence
        // =================================================================

        // =================================================================
        // Popup suppression — reuse the game's MyMessageBox.Suppress flag so the
        // blocking modal dialogs the server/UI fire during a repair don't freeze
        // the game. The suppressed text is logged instead of shown.
        // =================================================================

        private void BeginPopupSuppression()
        {
            MyMessageBox.Suppress = true;
            MyMessageBox.SuppressedHandler = delegate(string msg, string title)
            {
                LogWarning("Suppressed game popup ["
                    + (string.IsNullOrEmpty(title) ? "?" : title) + "]: " + msg);
            };
        }

        private void EndPopupSuppression()
        {
            MyMessageBox.Suppress = false;
            MyMessageBox.SuppressedHandler = null;
        }

        private void BeginRepairVillage(int villageId, VillageCastleRepairSettings vs, bool attackTriggered)
        {
            _activeVillageId = villageId;
            _activeSettings = vs;
            _step = RepairStep.SwitchVillage;
            _stepStarted = DateTime.Now;
            _incrInfraList = null;
            _incrTroopList = null;

            // Suppress the game's blocking modal popups (e.g. "research skill not
            // available", "cannot place there", "Castle Placement Error") for the
            // duration of this repair — they otherwise freeze the whole game. The
            // text is redirected to the bot log instead. Cleared in OnTick when the
            // state machine returns to Idle (and in OnShutdown as a safety net).
            BeginPopupSuppression();

            string infraDesc = vs.RepairInfrastructure
                ? "infra=" + (string.IsNullOrEmpty(vs.InfrastructurePresetName) || vs.InfrastructurePresetName == "Local"
                    ? "Local" : "'" + vs.InfrastructurePresetName + "'")
                : "infra=skip";
            string troopsDesc = vs.RepairTroops
                ? "troops=" + (string.IsNullOrEmpty(vs.TroopPresetName) || vs.TroopPresetName == "Local"
                    ? "Local" : "'" + vs.TroopPresetName + "'")
                : "troops=skip";
            string trigger = attackTriggered ? " [attack-triggered]" : "";

            LogInfo("Repairing village " + villageId + ": " + infraDesc + ", " + troopsDesc + trigger);
        }

        // Stamps _lastFullCycle when the last village in the normal cycle finishes
        // (regardless of whether it completed cleanly or was aborted/timed out).
        private void StampCycleCompleteIfDone()
        {
            if (!_cycleInProgress || _currentVillageIndex < _villageQueue.Count)
                return;

            _lastFullCycle = DateTime.Now;
            _cycleInProgress = false;
            CastleRepairSettings s = Settings;
            int nextSecs = s != null ? s.IntervalSeconds : 0;
            LogInfo("Repair cycle complete. Next scheduled cycle in " + nextSecs + "s.");
        }

        private void RunRepairStateMachine()
        {
            if ((DateTime.Now - _stepStarted).TotalMilliseconds > StepTimeoutMs)
            {
                LogWarning("Step " + _step + " timed out for village " + _activeVillageId + ", aborting.");
                _step = RepairStep.Idle;
                StampCycleCompleteIfDone();
                return;
            }

            try
            {
                switch (_step)
                {
                    case RepairStep.SwitchVillage:
                        StepSwitchVillage();
                        break;
                    case RepairStep.WaitForCastle:
                        StepWaitForCastle();
                        break;
                    case RepairStep.InitialCommit:
                        StepDoCommit(RepairStep.WaitInitialCommit);
                        break;
                    case RepairStep.WaitInitialCommit:
                        StepWaitForCommit(RepairStep.WaitAfterInitialCommit);
                        break;
                    case RepairStep.WaitAfterInitialCommit:
                        StepWaitDelay(RepairStep.AutoRepair, ShortDelayMs);
                        break;
                    case RepairStep.AutoRepair:
                        StepAutoRepair();
                        break;
                    case RepairStep.WaitAfterAutoRepair:
                        StepWaitDelay(RepairStep.RestoreInfrastructure, LongDelayMs);
                        break;
                    case RepairStep.RestoreInfrastructure:
                        StepRestoreInfrastructure();
                        break;
                    case RepairStep.CommitInfra:
                        StepDoCommit(RepairStep.WaitCommitInfra);
                        break;
                    case RepairStep.WaitCommitInfra:
                        StepWaitCommitInfra();
                        break;
                    case RepairStep.WaitAfterInfra:
                        StepWaitDelay(RepairStep.RestoreTroops, ShortDelayMs);
                        break;
                    case RepairStep.RestoreTroops:
                        StepRestoreTroops();
                        break;
                    case RepairStep.CommitTroops:
                        StepDoCommit(RepairStep.WaitCommitTroops);
                        break;
                    case RepairStep.WaitCommitTroops:
                        StepWaitCommitTroops();
                        break;
                    case RepairStep.IncrementalInfraPlace:
                        StepIncrementalInfraPlace();
                        break;
                    case RepairStep.IncrementalInfraWait:
                        StepIncrementalInfraWait();
                        break;
                    case RepairStep.IncrementalTroopsPlace:
                        StepIncrementalTroopsPlace();
                        break;
                    case RepairStep.IncrementalTroopsWait:
                        StepIncrementalTroopsWait();
                        break;
                    case RepairStep.Done:
                        LogInfo("Repair complete for village " + _activeVillageId);
                        _step = RepairStep.Idle;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogError("Repair error at " + _step + " for village " + _activeVillageId + ": " + ex.Message);
                _step = RepairStep.Idle;
            }

            // Any transition to Idle (Done, abort, or exception) — stamp cycle end if last village.
            if (_step == RepairStep.Idle)
                StampCycleCompleteIfDone();
        }

        // --- Individual repair steps ---

        private void StepSwitchVillage()
        {
            LogDebug("Switching to village " + _activeVillageId);
            InterfaceMgr.Instance.setVillageNameBar(_activeVillageId);
            GameEngine.Instance.downloadCurrentVillage();
            _step = RepairStep.WaitForCastle;
            _stepStarted = DateTime.Now;
        }

        private void StepWaitForCastle()
        {
            if (GameEngine.Instance.Castle == null)
                return;
            if (GameEngine.Instance.Castle.VillageID != _activeVillageId)
                return;

            LogDebug("Castle loaded for village " + _activeVillageId);
            // Commit first to refresh damage state from server,
            // matching proven code: "Updating Castle to see the damage"
            _step = RepairStep.InitialCommit;
            _stepStarted = DateTime.Now;
        }

        private void StepAutoRepair()
        {
            if (GameEngine.Instance.Castle == null)
            {
                LogWarning("Village " + _activeVillageId + ": castle not loaded at auto-repair, aborting.");
                _step = RepairStep.Idle;
                return;
            }

            if (GameEngine.Instance.Castle.castleDamaged)
            {
                LogInfo("Village " + _activeVillageId + ": castle is damaged — applying auto-repair.");
                GameEngine.Instance.Castle.autoRepairCastle();
                _step = RepairStep.WaitAfterAutoRepair;
                _stepStarted = DateTime.Now;
            }
            else
            {
                LogInfo("Village " + _activeVillageId + ": no castle damage detected, skipping auto-repair.");
                _step = RepairStep.RestoreInfrastructure;
                _stepStarted = DateTime.Now;
            }
        }

        private void StepRestoreInfrastructure()
        {
            if (!_activeSettings.RepairInfrastructure)
            {
                _step = RepairStep.RestoreTroops;
                _stepStarted = DateTime.Now;
                return;
            }

            if (GameEngine.Instance.Castle == null)
            {
                LogWarning("Village " + _activeVillageId + ": castle not loaded at infra restore, aborting.");
                _step = RepairStep.Idle;
                return;
            }

            string presetName = _activeSettings.InfrastructurePresetName;

            if (string.IsNullOrEmpty(presetName) || presetName == "Local")
            {
                LogInfo("Village " + _activeVillageId + ": restoring infrastructure from local layout.");
                _incrInfraList = GameEngine.Instance.Castle.parseInfrastructureSaveList();
            }
            else
            {
                CastleMapPreset preset = FindPreset(presetName, PresetType.INFRASTRUCTURE);
                if (preset != null)
                {
                    LogInfo("Village " + _activeVillageId + ": restoring infrastructure from preset '" + presetName + "'.");
                    _incrInfraList = GameEngine.Instance.Castle.parseInfrastructurePresetList(preset);
                }
                else
                {
                    LogWarning("Village " + _activeVillageId + ": infrastructure preset '" + presetName + "' not found, skipping.");
                    _step = RepairStep.RestoreTroops;
                    _stepStarted = DateTime.Now;
                    return;
                }
            }

            if (_incrInfraList == null || _incrInfraList.Count == 0)
            {
                LogWarning("Village " + _activeVillageId + ": no saved infrastructure layout, skipping.");
                _step = RepairStep.RestoreTroops;
                _stepStarted = DateTime.Now;
                return;
            }

            // Fast path: place the whole layout locally and commit as one batch.
            GameEngine.Instance.Castle.placeInfrastructure(_incrInfraList);
            _step = RepairStep.CommitInfra;
            _stepStarted = DateTime.Now;
        }

        private void StepRestoreTroops()
        {
            if (!_activeSettings.RepairTroops)
            {
                _step = RepairStep.Done;
                _stepStarted = DateTime.Now;
                return;
            }

            if (GameEngine.Instance.Castle == null)
            {
                LogWarning("Village " + _activeVillageId + ": castle not loaded at troop restore, aborting.");
                _step = RepairStep.Idle;
                return;
            }

            string presetName = _activeSettings.TroopPresetName;

            if (string.IsNullOrEmpty(presetName) || presetName == "Local")
            {
                LogInfo("Village " + _activeVillageId + ": restoring troops from local layout.");
                _incrTroopList = GameEngine.Instance.Castle.parseTroopsSaveList();
            }
            else
            {
                CastleMapPreset preset = FindPreset(presetName, PresetType.TROOP_DEFEND);
                if (preset != null)
                {
                    LogInfo("Village " + _activeVillageId + ": restoring troops from preset '" + presetName + "'.");
                    _incrTroopList = GameEngine.Instance.Castle.parseTroopsPresetList(preset);
                }
                else
                {
                    LogWarning("Village " + _activeVillageId + ": troop preset '" + presetName + "' not found, skipping.");
                    _step = RepairStep.Done;
                    _stepStarted = DateTime.Now;
                    return;
                }
            }

            if (_incrTroopList == null || _incrTroopList.Count == 0)
            {
                LogWarning("Village " + _activeVillageId + ": no saved troop layout, skipping.");
                _step = RepairStep.Done;
                _stepStarted = DateTime.Now;
                return;
            }

            // Fast path: place the whole layout locally and commit as one batch.
            GameEngine.Instance.Castle.placeTroops(_incrTroopList);
            _step = RepairStep.CommitTroops;
            _stepStarted = DateTime.Now;
        }

        private void StepDoCommit(RepairStep nextWaitStep)
        {
            if (GameEngine.Instance.Castle == null)
            {
                LogWarning("Village " + _activeVillageId + ": castle not loaded at commit, aborting.");
                _step = RepairStep.Idle;
                return;
            }

            LogDebug("Committing castle for village " + _activeVillageId);
            GameEngine.Instance.Castle.commitCastle();
            _step = nextWaitStep;
            _stepStarted = DateTime.Now;
        }

        private void StepWaitForCommit(RepairStep nextStep)
        {
            if (InterfaceMgr.Instance.WaitingForCallback)
                return;

            LogDebug("Commit complete for village " + _activeVillageId);
            _step = nextStep;
            _stepStarted = DateTime.Now;
        }

        // =================================================================
        // Incremental placement fallback — when the server rejects the whole
        // infrastructure/troop batch (e.g. a cloud preset contains an unresearched
        // or unaffordable piece), replay the layout element-by-element so every
        // buildable piece still lands. Only runs on the batch-reject path.
        // =================================================================

        private void StepWaitCommitInfra()
        {
            if (InterfaceMgr.Instance.WaitingForCallback)
                return;

            if (GameEngine.Instance.Castle != null
                && !GameEngine.Instance.Castle.LastListCommitSuccess
                && _incrInfraList != null && _incrInfraList.Count > 0)
            {
                LogWarning("Village " + _activeVillageId + ": infrastructure batch rejected ("
                    + GameEngine.Instance.Castle.LastListCommitError + ") — placing individually.");
                GameEngine.Instance.Castle.removeUncommittedElements();
                _incrementalIndex = 0;
                _incrementalPlaced = 0;
                _step = RepairStep.IncrementalInfraPlace;
                _stepStarted = DateTime.Now;
                return;
            }

            _step = RepairStep.WaitAfterInfra;
            _stepStarted = DateTime.Now;
        }

        private void StepIncrementalInfraPlace()
        {
            if (GameEngine.Instance.Castle == null)
            {
                LogWarning("Village " + _activeVillageId + ": castle not loaded during incremental infra, aborting.");
                _step = RepairStep.Idle;
                return;
            }

            if (_incrInfraList == null || _incrementalIndex >= _incrInfraList.Count)
            {
                LogInfo("Village " + _activeVillageId + ": placed " + _incrementalPlaced + "/"
                    + (_incrInfraList == null ? 0 : _incrInfraList.Count) + " infrastructure element(s) individually.");
                _step = RepairStep.WaitAfterInfra;
                _stepStarted = DateTime.Now;
                return;
            }

            CampCastleElement e = _incrInfraList[_incrementalIndex];
            if (GameEngine.Instance.Castle.placeSingleInfrastructure(e))
            {
                GameEngine.Instance.Castle.commitCastle();
                _step = RepairStep.IncrementalInfraWait;
                _stepStarted = DateTime.Now;
            }
            else
            {
                // Couldn't place locally (blocked tile / cap) — skip and move on.
                _incrementalIndex++;
                _stepStarted = DateTime.Now;
            }
        }

        private void StepIncrementalInfraWait()
        {
            if (InterfaceMgr.Instance.WaitingForCallback)
                return;

            if (GameEngine.Instance.Castle != null && GameEngine.Instance.Castle.LastListCommitSuccess)
            {
                _incrementalPlaced++;
            }
            else if (GameEngine.Instance.Castle != null)
            {
                CampCastleElement e = _incrInfraList[_incrementalIndex];
                LogWarning("Village " + _activeVillageId + ": infra element type " + (int) e.elementType
                    + " at " + (int) e.xPos + "," + (int) e.yPos + " rejected ("
                    + GameEngine.Instance.Castle.LastListCommitError + ").");
                GameEngine.Instance.Castle.removeUncommittedElements();
            }

            _incrementalIndex++;
            _step = RepairStep.IncrementalInfraPlace;
            _stepStarted = DateTime.Now;
        }

        private void StepWaitCommitTroops()
        {
            if (InterfaceMgr.Instance.WaitingForCallback)
                return;

            if (GameEngine.Instance.Castle != null
                && !GameEngine.Instance.Castle.LastListCommitSuccess
                && _incrTroopList != null && _incrTroopList.Count > 0)
            {
                LogWarning("Village " + _activeVillageId + ": troop batch rejected ("
                    + GameEngine.Instance.Castle.LastListCommitError + ") — placing individually.");
                GameEngine.Instance.Castle.removeUncommittedElements();
                _incrementalIndex = 0;
                _incrementalPlaced = 0;
                _step = RepairStep.IncrementalTroopsPlace;
                _stepStarted = DateTime.Now;
                return;
            }

            _step = RepairStep.Done;
            _stepStarted = DateTime.Now;
        }

        private void StepIncrementalTroopsPlace()
        {
            if (GameEngine.Instance.Castle == null)
            {
                LogWarning("Village " + _activeVillageId + ": castle not loaded during incremental troops, aborting.");
                _step = RepairStep.Idle;
                return;
            }

            if (_incrTroopList == null || _incrementalIndex >= _incrTroopList.Count)
            {
                LogInfo("Village " + _activeVillageId + ": placed " + _incrementalPlaced + "/"
                    + (_incrTroopList == null ? 0 : _incrTroopList.Count) + " troop element(s) individually.");
                _step = RepairStep.Done;
                _stepStarted = DateTime.Now;
                return;
            }

            CampCastleElementLL e = _incrTroopList[_incrementalIndex];
            if (GameEngine.Instance.Castle.placeSingleTroop(e))
            {
                GameEngine.Instance.Castle.commitCastle();
                _step = RepairStep.IncrementalTroopsWait;
                _stepStarted = DateTime.Now;
            }
            else
            {
                _incrementalIndex++;
                _stepStarted = DateTime.Now;
            }
        }

        private void StepIncrementalTroopsWait()
        {
            if (InterfaceMgr.Instance.WaitingForCallback)
                return;

            if (GameEngine.Instance.Castle != null && GameEngine.Instance.Castle.LastListCommitSuccess)
            {
                _incrementalPlaced++;
            }
            else if (GameEngine.Instance.Castle != null)
            {
                CampCastleElementLL e = _incrTroopList[_incrementalIndex];
                LogWarning("Village " + _activeVillageId + ": troop element type " + (int) e.elementType
                    + " at " + (int) e.xPos + "," + (int) e.yPos + " rejected ("
                    + GameEngine.Instance.Castle.LastListCommitError + ").");
                GameEngine.Instance.Castle.removeUncommittedElements();
            }

            _incrementalIndex++;
            _step = RepairStep.IncrementalTroopsPlace;
            _stepStarted = DateTime.Now;
        }

        private void StepWaitDelay(RepairStep nextStep, int delayMs)
        {
            if ((DateTime.Now - _stepStarted).TotalMilliseconds < delayMs)
                return;

            _step = nextStep;
            _stepStarted = DateTime.Now;
        }

        // =================================================================
        // Memorise state machine — switch to each village and snapshot layout
        // =================================================================

        private void RunMemoriseStateMachine()
        {
            if ((DateTime.Now - _memoriseStepStarted).TotalMilliseconds > StepTimeoutMs)
            {
                LogWarning("Memorise timed out for village " + _memoriseVillageId + ", skipping.");
                AdvanceMemoriseQueue();
                return;
            }

            try
            {
                switch (_memoriseStep)
                {
                    case MemoriseStep.SwitchVillage:
                        LogDebug("Memorise: switching to village " + _memoriseVillageId);
                        InterfaceMgr.Instance.setVillageNameBar(_memoriseVillageId);
                        GameEngine.Instance.downloadCurrentVillage();
                        _memoriseStep = MemoriseStep.WaitForCastle;
                        _memoriseStepStarted = DateTime.Now;
                        break;

                    case MemoriseStep.WaitForCastle:
                        if (GameEngine.Instance.Castle == null) return;
                        if (GameEngine.Instance.Castle.VillageID != _memoriseVillageId) return;
                        _memoriseStep = MemoriseStep.Memorise;
                        _memoriseStepStarted = DateTime.Now;
                        break;

                    case MemoriseStep.Memorise:
                        if (GameEngine.Instance.Castle == null)
                        {
                            LogWarning("Village " + _memoriseVillageId + ": castle not loaded, skipping memorise.");
                            AdvanceMemoriseQueue();
                            return;
                        }

                        if (_memoriseType == MemoriseType.Infrastructure)
                        {
                            GameEngine.Instance.Castle.memoriseInfrastructure();
                            LogInfo("Village " + _memoriseVillageId + ": infrastructure layout memorised.");
                        }
                        else
                        {
                            GameEngine.Instance.Castle.memoriseTroops();
                            LogInfo("Village " + _memoriseVillageId + ": troop layout memorised.");
                        }
                        _memorisedCount++;
                        AdvanceMemoriseQueue();
                        break;
                }
            }
            catch (Exception ex)
            {
                LogError("Memorise error at village " + _memoriseVillageId + ": " + ex.Message);
                AdvanceMemoriseQueue();
            }
        }

        private void AdvanceMemoriseQueue()
        {
            _memoriseIndex++;
            if (_memoriseIndex >= _memoriseQueue.Count)
            {
                string typeName = _memoriseType == MemoriseType.Infrastructure ? "infrastructure" : "troop";
                LogInfo("Memorise complete: " + _memorisedCount + "/" + _memoriseQueue.Count
                    + " " + typeName + " layout(s) saved to local files.");
                _memoriseStep = MemoriseStep.Idle;
                _memoriseQueue.Clear();
                return;
            }

            _memoriseVillageId = _memoriseQueue[_memoriseIndex];
            _memoriseStep = MemoriseStep.SwitchVillage;
            _memoriseStepStarted = DateTime.Now;
        }

        // =================================================================
        // Preset lookup — PresetManager.Instance.m_presets directly
        // =================================================================

        private static void EnsurePresetsLoaded()
        {
            try
            {
                PresetManager.Instance.LoadPresetsFromFile();
            }
            catch { }
        }

        private static CastleMapPreset FindPreset(string name, PresetType type)
        {
            if (string.IsNullOrEmpty(name) || name == "Local") return null;

            EnsurePresetsLoaded();

            try
            {
                List<CastleMapPreset> presets = PresetManager.Instance.m_presets;
                if (presets == null) return null;
                foreach (CastleMapPreset p in presets)
                {
                    if (p.Type == type && p.Name == name)
                        return p;
                }
            }
            catch { }
            return null;
        }

        public void NotifyAttackLanded(int villageId)
        {
            if (!_pendingRepairOnAttack.ContainsKey(villageId))
            {
                LogInfo("Radar notified attack landed at village " + villageId + ", queuing repair.");
                _pendingRepairOnAttack[villageId] = true;
            }
        }

        public static List<string> GetPresetNames(PresetType type)
        {
            List<string> names = new List<string>();
            names.Add("Local");

            EnsurePresetsLoaded();

            try
            {
                List<CastleMapPreset> presets = PresetManager.Instance.m_presets;
                if (presets != null)
                {
                    foreach (CastleMapPreset p in presets)
                    {
                        if (p.Type == type && !string.IsNullOrEmpty(p.Name))
                            names.Add(p.Name);
                    }
                }
            }
            catch { }
            return names;
        }

        protected override void OnShutdown()
        {
            _villageQueue.Clear();
            _currentVillageIndex = 0;
            _pendingRepairOnAttack.Clear();
            _trackedAttacks.Clear();
            _step = RepairStep.Idle;
            _cycleInProgress = false;
            _memoriseStep = MemoriseStep.Idle;
            _memoriseQueue.Clear();
            // Safety net: never leave popup suppression stuck on if the module stops
            // mid-repair.
            EndPopupSuppression();
        }
    }
}
