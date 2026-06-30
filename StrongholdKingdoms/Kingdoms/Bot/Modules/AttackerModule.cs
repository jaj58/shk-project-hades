using CommonTypes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Kingdoms.Bot.Modules
{
    public class AttackerPrey
    {
        public int OwnVillageId;
        public int TargetId;
        public double Distance;
    }

    public class MonkPrey
    {
        public int OwnVillageId;
        public int TargetId;
        public int Command; // 6 = Absolution, 7 = Excommunication
        public int Count;
    }

    public class AttackerModule : BotModuleBase
    {
        private const int ATTACK_PILLAGE = 2;
        private const int ATTACK_RANSACK = 3;
        private const int ATTACK_VANDALISE = 11;
        private const int ATTACK_GOLD_RAID = 12;

        private readonly object _preyLock = new object();
        private readonly Queue<AttackerPrey> _preyQueue = new Queue<AttackerPrey>();

        private readonly object _monkLock = new object();
        private readonly Queue<MonkPrey> _monkQueue = new Queue<MonkPrey>();

        private volatile bool _attacking;
        private Thread _attackThread;
        private ManualResetEvent _cancelEvent = new ManualResetEvent(false);
        private ManualResetEvent _callbackEvent = new ManualResetEvent(false);
        private PreAttackSetup_ReturnType _callbackResult;

        public override string ModuleName { get { return "Attacker"; } }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(3); }
        }

        public int PreyQueueCount
        {
            get { lock (_preyLock) return _preyQueue.Count; }
        }

        public int MonkQueueCount
        {
            get { lock (_monkLock) return _monkQueue.Count; }
        }

        public bool IsAttacking
        {
            get { return _attacking; }
        }

        public AttackerSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.Attacker;
                return null;
            }
        }

        protected override void OnInitialize()
        {
            _attacking = false;
            _cancelEvent.Reset();
        }

        protected override void OnDisable()
        {
            _cancelEvent.Set();
        }

        protected override void OnShutdown()
        {
            _cancelEvent.Set();
            _callbackEvent.Set();
            if (_attackThread != null && _attackThread.IsAlive)
                _attackThread.Join(500);
            _attackThread = null;
        }

        protected override void OnTick()
        {
            AttackerSettings s = Settings;
            if (s == null || !s.Enabled)
                return;

            MonkPrey monkPrey = null;
            lock (_monkLock)
            {
                if (_monkQueue.Count > 0)
                    monkPrey = _monkQueue.Dequeue();
            }
            if (monkPrey != null)
                SendMonkPrey(monkPrey);

            if (_attacking)
                return;

            AttackerPrey prey = null;
            lock (_preyLock)
            {
                if (_preyQueue.Count > 0)
                    prey = _preyQueue.Dequeue();
            }
            if (prey == null)
                return;

            LaunchAttackThread(prey, s);
        }

        // =================================================================
        // Public API — called by UI and village panel buttons
        // =================================================================

        public List<AttackerPrey> GetPreyList()
        {
            lock (_preyLock)
                return new List<AttackerPrey>(_preyQueue);
        }

        public void AddPrey(AttackerPrey prey)
        {
            if (prey == null) return;
            lock (_preyLock)
                _preyQueue.Enqueue(prey);
            LogInfo("Prey added: " + prey.OwnVillageId + " -> " + prey.TargetId + " (queue=" + PreyQueueCount + ")");
        }

        public void SetPreyList(IEnumerable<AttackerPrey> preys)
        {
            lock (_preyLock)
            {
                _preyQueue.Clear();
                if (preys != null)
                {
                    foreach (AttackerPrey p in preys)
                        _preyQueue.Enqueue(p);
                }
            }
            LogInfo("Prey list set: " + PreyQueueCount + " target(s).");
        }

        public void ClearPreys()
        {
            lock (_preyLock)
                _preyQueue.Clear();
            LogInfo("Prey queue cleared.");
        }

        public void RunNow()
        {
            if (Enabled)
                OnTick();
        }

        public List<MonkPrey> GetMonkList()
        {
            lock (_monkLock)
                return new List<MonkPrey>(_monkQueue);
        }

        public void AddMonkPrey(MonkPrey monk)
        {
            if (monk == null) return;
            lock (_monkLock)
                _monkQueue.Enqueue(monk);
            LogInfo("Monk action queued: " + monk.OwnVillageId + " -> " + monk.TargetId +
                " (cmd " + monk.Command + ", queue=" + MonkQueueCount + ")");
        }

        public void ClearMonkPreys()
        {
            lock (_monkLock)
                _monkQueue.Clear();
            LogInfo("Monk queue cleared.");
        }

        /// <summary>
        /// Sends monks immediately, bypassing the queue.
        /// Used by the world-map absolution/excommunication buttons when Force Mode is enabled.
        /// </summary>
        public void SendMonkNow(int ownVillageId, int targetId, int command, int count)
        {
            SendMonkPrey(new MonkPrey { OwnVillageId = ownVillageId, TargetId = targetId, Command = command, Count = count });
        }

        private void SendMonkPrey(MonkPrey monk)
        {
            try
            {
                RemoteServices.Instance.SendPeople(monk.OwnVillageId, monk.TargetId, 4, monk.Count, monk.Command, -1);
                LogInfo("Monks sent: " + monk.OwnVillageId + " -> " + monk.TargetId +
                    " (cmd " + monk.Command + ", count " + monk.Count + ")");
            }
            catch (Exception ex)
            {
                LogError("Monk send error: " + ex.Message);
            }
        }

        /// <summary>
        /// Fires an attack immediately on a background thread, bypassing the queue.
        /// Used by the world-map attack button when Force Mode is enabled.
        /// </summary>
        public void AttackNow(int ownVillageId, int targetId)
        {
            AttackerSettings s = Settings;
            if (s == null || _attacking)
                return;

            LaunchAttackThread(new AttackerPrey { OwnVillageId = ownVillageId, TargetId = targetId }, s);
        }

        private void LaunchAttackThread(AttackerPrey prey, AttackerSettings s)
        {
            _attacking = true;
            _attackThread = new Thread(() => AttackThreadProc(prey, s));
            _attackThread.IsBackground = true;
            _attackThread.Name = "Attacker";
            _attackThread.Start();
        }

        // =================================================================
        // Background attack thread
        // =================================================================

        private void AttackThreadProc(AttackerPrey prey, AttackerSettings s)
        {
            try
            {
                if (GameEngine.Instance == null)
                {
                    LogError("No game engine available.");
                    return;
                }

                _callbackEvent.Reset();
                _callbackResult = null;

                RemoteServices.Instance.set_PreAttackSetup_UserCallBack(
                    new RemoteServices.PreAttackSetup_UserCallBack(OnPreAttackSetupCallback));
                RemoteServices.Instance.PreAttackSetup(
                    prey.OwnVillageId,
                    prey.OwnVillageId,
                    prey.TargetId,
                    0, 0, 0, 0, 0,
                    11,
                    0, 0);

                int waitIndex = WaitHandle.WaitAny(new WaitHandle[] { _callbackEvent, _cancelEvent }, 30000);
                if (waitIndex != 0)
                {
                    if (waitIndex == WaitHandle.WaitTimeout)
                        LogError("PreAttackSetup callback timed out for target " + prey.TargetId + ".");
                    return;
                }

                PreAttackSetup_ReturnType ret = _callbackResult;
                if (ret == null)
                {
                    LogError("No callback data for target " + prey.TargetId + ".");
                    return;
                }

                if (ret.protectedVillage)
                {
                    LogWarning("Target " + prey.TargetId + " is protected (interdiction).");
                    return;
                }
                if (ret.vacationVillage)
                {
                    LogWarning("Target " + prey.TargetId + " is on vacation.");
                    return;
                }
                if (ret.peaceVillage)
                {
                    LogWarning("Target " + prey.TargetId + " is in peace time.");
                    return;
                }
                if (ret.peaceAttacker)
                {
                    LogWarning("Attacker " + prey.OwnVillageId + " is in peace time.");
                    return;
                }
                if (ret.protectedAttacker)
                {
                    LogWarning("Attacker " + prey.OwnVillageId + " is interdiction protected.");
                    return;
                }
                if (!ret.Success)
                {
                    LogError("Server error attacking " + prey.TargetId + ": " + ret.m_errorCode);
                    return;
                }

                int honourCost = 0;
                if (ret.battleHonourData != null)
                {
                    ret.battleHonourData.attackType = 11;
                    if (!GameEngine.Instance.World.isCapital(ret.parentAttackingVillage))
                        honourCost = CastlesCommon.calcBattleHonourCost(
                            ret.battleHonourData,
                            GameEngine.Instance.LocalWorldData.Alternate_Ruleset,
                            GameEngine.Instance.LocalWorldData.EraWorld);
                }
                if (honourCost > 0 && GameEngine.Instance.World.getCurrentHonour() <= 0.0)
                {
                    LogError("Not enough honour to attack " + prey.TargetId + ".");
                    return;
                }

                string formationName;
                int attackType;
                int pillagePercent;
                ResolveAttackProfile(ret, s, out formationName, out attackType, out pillagePercent);

                if (string.IsNullOrEmpty(formationName))
                {
                    LogWarning("No formation configured for this target type — skipping " + prey.TargetId + ".");
                    return;
                }

                CastleMap castleMap = new CastleMap(-1, GameEngine.Instance.GFX, 1);
                castleMap.SetUsingCastleTroopsOK(true);
                castleMap.importDefenderSnapshot(
                    ret.castleMapSnapshot,
                    ret.castleTroopsSnapshot,
                    ret.keepLevel,
                    true,
                    ret.landType);
                castleMap.initRealSetup(
                    ret.attackingVillage,
                    ret.targetVillage,
                    ret.numPeasants,
                    ret.numArchers,
                    ret.numPikemen,
                    ret.numSwordsmen,
                    ret.numCatapults,
                    attackType,
                    pillagePercent,
                    ret.captainsCommand,
                    ret.parentAttackingVillage,
                    ret.numPeasantsInCastle,
                    ret.numArchersInCastle,
                    ret.numPikemenInCastle,
                    ret.numSwordsmenInCastle,
                    ret.targetUserID,
                    ret.targetUserName,
                    ret.battleHonourData,
                    ret.numCaptainsInCastle,
                    ret.numCaptains,
                    ret.capitalAttackRate);

                if (!TryApplyFormation(castleMap, formationName))
                    return;

                if (castleMap.attackNumPeasants == 0 && castleMap.attackNumArchers == 0 &&
                    castleMap.attackNumPikemen == 0 && castleMap.attackNumSwordsmen == 0 &&
                    castleMap.attackNumCatapults == 0 && castleMap.attackNumCaptains == 0)
                {
                    LogError("Zero troops placed for formation '" + formationName + "' — aborting attack on " + prey.TargetId + ".");
                    return;
                }

                castleMap.setupLaunchArmy(attackType, pillagePercent, 0);
                castleMap.launchArmy(true);
                LogInfo("Attacked: " + prey.OwnVillageId + " -> " + prey.TargetId +
                    " (formation '" + formationName + "', type " + attackType + ")");
            }
            catch (Exception ex)
            {
                LogError("Attack thread error: " + ex.Message);
            }
            finally
            {
                _attacking = false;
            }
        }

        private void OnPreAttackSetupCallback(PreAttackSetup_ReturnType ret)
        {
            _callbackResult = ret;
            _callbackEvent.Set();
        }

        // =================================================================
        // Target classification
        // =================================================================

        private void ResolveAttackProfile(PreAttackSetup_ReturnType ret, AttackerSettings s,
            out string formationName, out int attackType, out int pillagePercent)
        {
            int targetId = ret.targetVillage;
            WorldMap world = GameEngine.Instance.World;

            if (world.isCapital(targetId) || world.isCountyCapital(targetId) || world.isCountryCapital(targetId))
            {
                formationName = s.DistrictFormationName;
                attackType = s.DistrictAttackType;
                // Gold raid is only valid when the attacker is also a capital.
                if (attackType == ATTACK_GOLD_RAID && !world.isCapital(ret.parentAttackingVillage))
                    attackType = ATTACK_VANDALISE;
                pillagePercent = 0;
            }
            else if (world.isSpecial(targetId) || world.isSpecialAIPlayer(targetId))
            {
                formationName = s.AiFormationName;
                attackType = s.AiAttackType;
                pillagePercent = attackType == ATTACK_PILLAGE ? s.AiPillagePercent : 0;
            }
            else
            {
                formationName = s.EnemyFormationName;
                attackType = s.EnemyAttackType;
                pillagePercent = (attackType == ATTACK_PILLAGE || attackType == ATTACK_RANSACK)
                    ? s.EnemyPillagePercent
                    : 0;
            }
        }

        private bool TryApplyFormation(CastleMap castleMap, string formationName)
        {
            CastleMapPreset cloudPreset = AutoBombModule.FindCloudPreset(formationName);
            if (cloudPreset != null)
            {
                int[] presetCounts = AutoBombModule.PresetTroopsCount(cloudPreset);
                if (castleMap.HasEnoughTroopsToPlace(ref presetCounts))
                {
                    castleMap.RestoreAttackPresetBG(cloudPreset);
                    return true;
                }
                LogWarning("Not enough troops for cloud formation '" + formationName + "'.");
                return false;
            }

            List<CastleMap.RestoreCastleElement> attackSetup = castleMap.getAttackSetup(formationName);
            if (attackSetup != null && attackSetup.Count > 0)
            {
                int[] localCounts = AutoBombModule.GetTroopsCountArray12(attackSetup);
                if (castleMap.HasEnoughTroopsToPlace(ref localCounts))
                {
                    castleMap.RestoreAttackSetupBG(attackSetup);
                    return true;
                }
                LogWarning("Not enough troops for local formation '" + formationName + "'.");
                return false;
            }

            LogWarning("Formation not found in cloud or local: '" + formationName + "'.");
            return false;
        }
    }
}
