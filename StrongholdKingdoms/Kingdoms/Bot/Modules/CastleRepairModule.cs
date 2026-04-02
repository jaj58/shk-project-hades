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
            Done
        }

        private RepairStep _step = RepairStep.Idle;
        private int _activeVillageId;
        private VillageCastleRepairSettings _activeSettings;
        private DateTime _stepStarted = DateTime.MinValue;
        private const int StepTimeoutMs = 15000;
        private const int ShortDelayMs = 1000;
        private const int LongDelayMs = 2000;

        public override string ModuleName
        {
            get { return "Castle Repair"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromMilliseconds(300); }
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
        // Tick — drives the state machine and normal cycle
        // =================================================================

        protected override void OnTick()
        {
            CastleRepairSettings settings = Settings;
            if (settings == null) return;

            // If a repair sequence is in progress, run the state machine
            if (_step != RepairStep.Idle)
            {
                RunRepairStateMachine();
                return;
            }

            // Scan for AI attacks
            if (settings.RepairOnAttack)
            {
                ScanForArrivedAttacks(settings);
                ProcessPendingAttackRepairs(settings);
                if (_step != RepairStep.Idle) return;
            }

            // Normal scheduled cycle
            if (_villageQueue.Count == 0 || _currentVillageIndex >= _villageQueue.Count)
            {
                if ((DateTime.Now - _lastFullCycle).TotalSeconds < settings.IntervalSeconds)
                    return;

                RefreshVillageQueue(settings);
                _currentVillageIndex = 0;
                _lastFullCycle = DateTime.Now;

                if (_villageQueue.Count == 0)
                    return;
            }

            if ((DateTime.Now - _lastVillageAction).TotalMilliseconds < settings.DelayBetweenVillagesMs)
                return;

            int villageId = _villageQueue[_currentVillageIndex];
            _currentVillageIndex++;
            _lastVillageAction = DateTime.Now;

            VillageCastleRepairSettings vs = settings.GetVillageSettings(villageId);
            BeginRepairVillage(villageId, vs);
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
            _pendingRepairOnAttack.Clear();

            foreach (int villageId in toProcess)
            {
                VillageCastleRepairSettings vs = settings.GetVillageSettings(villageId);
                if (vs.RepairInfrastructure || vs.RepairTroops)
                {
                    LogInfo("Repair on attack: repairing village " + villageId);
                    BeginRepairVillage(villageId, vs);
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
            _lastFullCycle = DateTime.Now;
            _lastVillageAction = DateTime.MinValue;
            LogInfo("Repair all now: queued " + _villageQueue.Count + " village(s).");
        }

        // =================================================================
        // State machine — the core repair sequence
        // =================================================================

        private void BeginRepairVillage(int villageId, VillageCastleRepairSettings vs)
        {
            _activeVillageId = villageId;
            _activeSettings = vs;
            _step = RepairStep.SwitchVillage;
            _stepStarted = DateTime.Now;
            LogInfo("Starting repair sequence for village " + villageId);
        }

        private void RunRepairStateMachine()
        {
            if ((DateTime.Now - _stepStarted).TotalMilliseconds > StepTimeoutMs)
            {
                LogWarning("Step " + _step + " timed out for village " + _activeVillageId + ", aborting.");
                _step = RepairStep.Idle;
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
                        StepWaitForCommit(RepairStep.WaitAfterInfra);
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
                        StepWaitForCommit(RepairStep.Done);
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
        }

        // --- Individual steps ---

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
                LogWarning("Castle null at autorepair for village " + _activeVillageId);
                _step = RepairStep.Idle;
                return;
            }

            if (GameEngine.Instance.Castle.castleDamaged)
            {
                LogInfo("Castle damaged, launching autorepair for village " + _activeVillageId);
                GameEngine.Instance.Castle.autoRepairCastle();
                _step = RepairStep.WaitAfterAutoRepair;
                _stepStarted = DateTime.Now;
            }
            else
            {
                LogDebug("No damage for village " + _activeVillageId);
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
                LogWarning("Castle null at infra restore for village " + _activeVillageId);
                _step = RepairStep.Idle;
                return;
            }

            string presetName = _activeSettings.InfrastructurePresetName;

            if (string.IsNullOrEmpty(presetName) || presetName == "Local")
            {
                LogInfo("Restoring local infrastructure for village " + _activeVillageId);
                GameEngine.Instance.Castle.restoreInfrastructure();
            }
            else
            {
                CastleMapPreset preset = FindPreset(presetName, PresetType.INFRASTRUCTURE);
                if (preset != null)
                {
                    LogInfo("Restoring infrastructure preset '" + presetName + "' for village " + _activeVillageId);
                    GameEngine.Instance.Castle.restoreInfrastructurePreset(preset);
                }
                else
                {
                    LogWarning("Infrastructure preset not found: " + presetName);
                    _step = RepairStep.RestoreTroops;
                    _stepStarted = DateTime.Now;
                    return;
                }
            }

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
                LogWarning("Castle null at troop restore for village " + _activeVillageId);
                _step = RepairStep.Idle;
                return;
            }

            string presetName = _activeSettings.TroopPresetName;

            if (string.IsNullOrEmpty(presetName) || presetName == "Local")
            {
                LogInfo("Restoring local troops for village " + _activeVillageId);
                GameEngine.Instance.Castle.restoreTroops();
            }
            else
            {
                CastleMapPreset preset = FindPreset(presetName, PresetType.TROOP_DEFEND);
                if (preset != null)
                {
                    LogInfo("Restoring troop preset '" + presetName + "' for village " + _activeVillageId);
                    GameEngine.Instance.Castle.restoreTroopsPreset(preset);
                }
                else
                {
                    LogWarning("Troop preset not found: " + presetName);
                    _step = RepairStep.Done;
                    _stepStarted = DateTime.Now;
                    return;
                }
            }

            _step = RepairStep.CommitTroops;
            _stepStarted = DateTime.Now;
        }

        private void StepDoCommit(RepairStep nextWaitStep)
        {
            if (GameEngine.Instance.Castle == null)
            {
                LogWarning("Castle null at commit for village " + _activeVillageId);
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

        private void StepWaitDelay(RepairStep nextStep, int delayMs)
        {
            if ((DateTime.Now - _stepStarted).TotalMilliseconds < delayMs)
                return;

            _step = nextStep;
            _stepStarted = DateTime.Now;
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
        }
    }
}
