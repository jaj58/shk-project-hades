using CommonTypes;
using Stronghold.AuthClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Kingdoms.Bot.Modules
{
    public class AutoBombMultiModule : BotModuleBase
    {
        // ── Poll thread ──────────────────────────────────────────────────────
        private Thread _pollThread;
        private ManualResetEvent _stopPollEvent = new ManualResetEvent(false);

        // ── Launch infrastructure (mirrors AutoBombModule) ───────────────────
        private volatile bool _launching;
        private volatile bool _preparing;
        private volatile bool _interdictDetected;
        private Thread _launchThread;
        private Thread _prepareThread;
        private ManualResetEvent _cancelLaunchEvent = new ManualResetEvent(false);
        private ManualResetEvent _callbackEvent = new ManualResetEvent(false);
        private PreAttackSetup_ReturnType _callbackResult;

        // Fake send recall timer
        private Timer _fakeSendTimer;
        private volatile bool _fakeSendTriggered;

        // Set when a prepare failure (non-interdict) aborts a batch so the launch
        // thread can still recall + advance the queue rather than stopping entirely.
        private volatile bool _prepareErrorCancel;

        // Current set of attacks this client is responsible for
        private readonly object _attacksLock = new object();
        private List<BombAttackEntry> _myAttacks = new List<BombAttackEntry>();
        private int _currentTargetVillageId;

        // Locally cached copy of the full attack config (all players), set when the
        // coordinator pushes config or advances the queue. Used by TryAdvanceMultiQueue
        // so we never rely on re-fetching from the API (which can return corrupt data
        // after an interdict cancel resets the attacks array server-side).
        private List<Dictionary<string, object>> _cachedAttackConfig = null;

        // Last known session state to detect transitions
        private string _lastSessionState = "";
        private string _lastProcessedLaunchId = "";
        private bool _pollStarted;

        // Army-return monitoring for target queue advancement.
        // Non-coordinators monitor their own armies after launch and report via the poll thread.
        private volatile bool _monitoringArmyReturn;
        private volatile bool _armyReturnReported;
        private HashSet<int> _sentSourceVillages = new HashSet<int>();

        // Instance IDs of logistics cards already played this batch.
        // Logistics cards are consumed on send, so their instance ID becomes invalid the moment
        // the attack fires. ProfileCards won't reflect this until the client syncs, so we track
        // played IDs locally to avoid re-playing the same (now-consumed) instance on the next village.
        private readonly HashSet<int> _playedCardInstanceIds = new HashSet<int>();

        // Flag tracking if a manual cancel (via CancelAll) is pending.
        // Used to resend cancel_attacks on each poll until the batch completes.
        private volatile bool _pendingManualCancel;

        // The earliest time the current active card will be free (i.e. consumed by the village
        // it was played for). Set to that village's scheduled send time when a card is played.
        // Used to delay the next card play when send times are very close together.
        private DateTime _cardFreeAt = DateTime.MinValue;

        // Cached result of BuildLocalVillageList. Rebuilt only when the target village changes
        // or after VillageListCacheSeconds, rather than on every heartbeat. With 200+ vassals,
        // rebuilding every 2 s during launch generates hundreds of world-map lookups per second.
        private List<Dictionary<string, object>> _cachedVillageList = null;
        private int    _cachedVillageListTargetVid = -1;
        private DateTime _cachedVillageListTime    = DateTime.MinValue;
        private const int VillageListCacheSeconds  = 60;

        // ── Sent army tracking for queue advancement ──────────────────────────
        // Track when armies were sent and when they were recalled, so we can wait
        // until all have actually returned before launching the next target.
        private class SentArmy
        {
            public int VillageId;
            public DateTime SentTime;
            public DateTime? RecalledTime;  // When we confirmed recall was issued
            public double TravelSeconds;
        }
        private List<SentArmy> _sentArmies = new List<SentArmy>();

        // Recall acknowledgements from remote players
        // Key: "player_name", Value: DateTime when they acknowledged the recall signal
        private Dictionary<string, DateTime> _recallAcknowledged = new Dictionary<string, DateTime>();

        public override string ModuleName { get { return "Auto Bomb Multi"; } }
        public override TimeSpan Interval { get { return TimeSpan.FromMilliseconds(500); } }

        public bool IsLaunching { get { return _launching; } }

        private AutoBombMultiSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.AutoBombMulti;
                return null;
            }
        }

        // ── Lifecycle ────────────────────────────────────────────────────────

        protected override void OnInitialize()
        {
            _launching = false;
            _preparing = false;
            _interdictDetected = false;
            _pollStarted = false;
        }

        protected override void OnDisable()
        {
            _pollStarted = false;
            _stopPollEvent.Set();
            _cancelLaunchEvent.Set();
            _cachedVillageList = null; // force fresh build on next connect

            AutoBombMultiSettings settings = Settings;
            if (settings != null && !string.IsNullOrEmpty(settings.ApiUrl))
            {
                try { PostAction(settings, "player_disconnect", new Dictionary<string, object>
                    { ["player_name"] = GetLocalPlayerName() }); }
                catch { }
            }
        }

        private void StartPolling()
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null) return;

            if (string.IsNullOrEmpty(settings.ApiUrl))
            {
                LogWarning("API URL is not set. Configure it in the Auto Bomb Multi tab.");
                return;
            }

            // Register with the API
            PostPlayerReady(settings);

            // Start poll thread
            _stopPollEvent.Reset();
            _pollThread = new Thread(PollThreadProc);
            _pollThread.IsBackground = true;
            _pollThread.Name = "AutoBombMulti Poll";
            _pollThread.Start();
        }

        protected override void OnShutdown()
        {
            _stopPollEvent.Set();
            _cancelLaunchEvent.Set();
            if (_pollThread != null && _pollThread.IsAlive)
                _pollThread.Join(3000);
            StopLaunchThread();
        }

        protected override void OnTick()
        {
            // Start the poll thread on the first tick after the module is enabled.
            if (!_pollStarted)
            {
                _pollStarted = true;
                StartPolling();
            }

            if (!_launching) return;
            AutoBombMultiSettings settings = Settings;
            if (settings == null) return;

            // Once any auto-recall has triggered (armies recalled and returning home),
            // interdict on the old target is irrelevant — don't abort the queue-advance wait.
            if (settings.AutoCancelOnInterdict && !_interdictDetected && !_fakeSendTriggered && !_prepareErrorCancel)
            {
                if (AutoBombModule.CheckForInterdict(_currentTargetVillageId))
                {
                    _interdictDetected = true;

                    // Before cancelling, check if any of our sent attacks are too far along to safely recall.
                    // Use 4:50 (290s) cutoff to provide buffer before the hard 5-minute limit.
                    if (!CanSafelyRecallAllMyAttacks(290))
                    {
                        LogWarning("Interdict detected but one or more sent attacks are >= 4:50 out — " +
                            "cancelling launch but NOT recalling armies (they're too far out).");
                        _cancelLaunchEvent.Set();
                        CancelRemainingLocalAttacks();
                        // Don't recall; let armies proceed to target
                    }
                    else
                    {
                        LogWarning("Interdict detected on target! Cancelling all attacks and recalling armies.");
                        _cancelLaunchEvent.Set();
                        CancelRemainingLocalAttacks();
                        RecallAll(settings);
                    }

                    // Notify API so other players also get the cancel signal.
                    // Each player will independently decide whether to recall based on their own armies.
                    try { PostAction(settings, "cancel_attacks", new Dictionary<string, object>
                        { ["player_name"] = GetLocalPlayerName(), ["reason"] = "interdict" }); }
                    catch { }
                }
            }
        }

        // ── Public methods called by UI ──────────────────────────────────────

        public void TakeCoordinator()
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null || string.IsNullOrEmpty(settings.ApiUrl)) return;
            try
            {
                var resp = PostAction(settings, "take_coordinator", new Dictionary<string, object>
                    { ["player_name"] = GetLocalPlayerName() });
                if (resp != null && true.Equals(resp["ok"]))
                {
                    settings.IsCoordinator = true;
                    ApplyStateFromResponse(settings, resp);
                    LogInfo("Coordinator role claimed.");
                }
            }
            catch (Exception ex) { LogError("TakeCoordinator failed: " + ex.Message); }
        }

        public void PushAttackConfig(int targetVillageId, List<MultiAttackConfigEntry> attacks)
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null || !settings.IsCoordinator) return;

            var attackList = new List<Dictionary<string, object>>();
            foreach (var a in attacks)
            {
                attackList.Add(new Dictionary<string, object>
                {
                    ["source_player"]     = a.SourcePlayerName,
                    ["source_village_id"] = a.SourceVillageId,
                    ["parent_village_id"] = a.ParentVillageId,
                    ["is_vassal"]         = a.IsVassal,
                    ["formation"]         = a.FormationName,
                    ["stack"]             = a.Stack,
                    ["card_type"]         = a.CardType,
                    ["captains_only"]     = a.CaptainsOnly,
                    ["attack_type"]       = a.AttackType,
                    ["travel_time_seconds"] = a.TravelTimeSeconds,
                    ["selected"]          = a.Selected,
                });
            }

            try
            {
                // Cache locally so queue advances can rebuild without re-fetching from API
                _cachedAttackConfig = new List<Dictionary<string, object>>(attackList);

                var resp = PostAction(settings, "set_attack_config", new Dictionary<string, object>
                {
                    ["player_name"]       = GetLocalPlayerName(),
                    ["target_village_id"] = targetVillageId,
                    ["attacks"]           = attackList,
                });
                // Apply state immediately so the UI reflects 'configured' without
                // waiting up to 2 s for the next poll cycle to pick it up.
                if (resp != null) ApplyStateFromResponse(settings, resp);
                LogInfo("Attack config pushed to API for target " + targetVillageId + ".");
            }
            catch (Exception ex) { LogError("PushAttackConfig failed: " + ex.Message); }
        }

        public void StartTimer()
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null || !settings.IsCoordinator) return;
            // Run on a background thread — the remote-travel handshake may wait a few seconds
            // and must not freeze the UI thread that clicked Launch.
            var t = new Thread(() => StartTimerProc(settings));
            t.IsBackground = true;
            t.Name = "AbmStartTimer";
            t.Start();
        }

        private void StartTimerProc(AutoBombMultiSettings settings)
        {
            try
            {
                string myName    = GetLocalPlayerName();
                int    targetVid = settings.TargetVillageId;
                var    source    = _cachedAttackConfig;

                // Detect whether any selected attack belongs to another player.
                bool hasRemote = false;
                if (source != null)
                    foreach (var ad in source)
                        if (GetBool(ad, "selected", true) && GetStr(ad, "source_player", "") != myName)
                            { hasRemote = true; break; }

                // With remote players, their config travel times can be stale (computed before
                // they saw this target). Wait for them to recompute, then re-push the config with
                // corrected travel times before firing the timer. The launch thread re-prepares
                // each attack just-in-time, so re-pushing here does not disturb the prepared state.
                if (hasRemote && source != null && source.Count > 0)
                {
                    _cancelLaunchEvent.Reset();
                    if (!WaitForRemoteTravelReady(settings, source, targetVid, myName, 25000))
                    {
                        LogInfo("[Launch] Cancelled during remote-travel wait — not starting timer.");
                        return;
                    }

                    var freshList = BuildQueueAttackList(settings, source, targetVid, myName);
                    _cachedAttackConfig = new List<Dictionary<string, object>>(freshList);
                    foreach (var atk in freshList)
                        LogInfo("[Launch] Travel: village=" + GetInt(atk, "source_village_id") +
                            " player=" + GetStr(atk, "source_player", "") +
                            " travel=" + (int)GetDouble(atk, "travel_time_seconds") + "s");

                    PostAction(settings, "set_attack_config", new Dictionary<string, object>
                    {
                        ["player_name"]       = myName,
                        ["target_village_id"] = targetVid,
                        ["attacks"]           = freshList,
                    });
                    // Brief settle so non-coordinators poll the corrected config before the timer.
                    if (_cancelLaunchEvent.WaitOne(2000)) return;
                }

                PostAction(settings, "start_timer", new Dictionary<string, object>
                {
                    ["player_name"]            = myName,
                    ["stack_delay_seconds"]    = settings.StackDelaySeconds,
                    ["fake_send"]              = settings.FakeSendEnabled,
                    ["auto_cancel_on_interdict"] = settings.AutoCancelOnInterdict,
                });
                LogInfo("Timer started — all clients will receive launch times on next poll.");
            }
            catch (Exception ex) { LogError("StartTimerProc failed: " + ex.Message); }
        }

        public void TriggerPrepare()
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null || !settings.IsCoordinator) return;
            try
            {
                PostAction(settings, "prepare_attacks", new Dictionary<string, object>
                    { ["player_name"] = GetLocalPlayerName() });
                LogInfo("Preparation triggered — all clients will validate attacks.");
            }
            catch (Exception ex) { LogError("TriggerPrepare failed: " + ex.Message); }
        }

        public void CancelAll()
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null) return;
            _cancelLaunchEvent.Set();
            CancelRemainingLocalAttacks();
            if (settings.FakeSendEnabled)
            {
                bool anySent = false;
                lock (_attacksLock) { foreach (var e in _myAttacks) if (e.Sent) { anySent = true; break; } }
                if (anySent) RecallAll(settings);
            }
            _pendingManualCancel = true;
            try
            {
                PostAction(settings, "cancel_attacks", new Dictionary<string, object>
                    { ["player_name"] = GetLocalPlayerName() });
            }
            catch (Exception ex) { LogError("CancelAll API call failed: " + ex.Message); }
        }

        public void ForceRecallAll()
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null || !settings.IsCoordinator) return;
            LogInfo("Force recalling all armies across all players...");
            try
            {
                PostAction(settings, "force_recall_all", new Dictionary<string, object>
                    { ["player_name"] = GetLocalPlayerName() });
            }
            catch (Exception ex) { LogError("ForceRecallAll API call failed: " + ex.Message); }
        }

        public void ResetSession()
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null || !settings.IsCoordinator) return;
            try
            {
                PostAction(settings, "reset_session", new Dictionary<string, object>
                    { ["player_name"] = GetLocalPlayerName() });
                settings.SessionState = "idle";
                settings.ConnectedPlayers.Clear();
                LogInfo("Session reset.");
            }
            catch (Exception ex) { LogError("ResetSession failed: " + ex.Message); }
        }

        public void QueueTarget(int villageId, string label)
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null || !settings.IsCoordinator) return;
            try
            {
                PostAction(settings, "queue_target", new Dictionary<string, object>
                {
                    ["player_name"] = GetLocalPlayerName(),
                    ["village_id"]  = villageId,
                    ["label"]       = label ?? "",
                });
            }
            catch (Exception ex) { LogError("QueueTarget failed: " + ex.Message); }
        }

        // ── Poll thread ──────────────────────────────────────────────────────

        private void PollThreadProc()
        {
            int pollsSinceHeartbeat = 0;
            while (!_stopPollEvent.WaitOne(0))
            {
                try { DoPoll(); }
                catch (Exception ex) { LogError("Poll error: " + ex.Message); }

                // Re-register every ~20 s so the server doesn't expire us (30 s TTL)
                pollsSinceHeartbeat++;
                if (pollsSinceHeartbeat >= 10)
                {
                    pollsSinceHeartbeat = 0;
                    AutoBombMultiSettings s = Settings;
                    if (s != null) try { PostPlayerReady(s); } catch { }
                }

                int delay = _launching ? 200 : 2000;
                _stopPollEvent.WaitOne(delay);
            }
        }

        private void DoPoll()
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null || string.IsNullOrEmpty(settings.ApiUrl)) return;

            var resp = PostAction(settings, "get_status", new Dictionary<string, object>
                { ["player_name"] = GetLocalPlayerName() });
            if (resp == null) return;

            ApplyStateFromResponse(settings, resp);

            // ── Resend pending cancel/recall signals ──────────────────────────
            // If an interdict was detected or a manual cancel is pending, resend the signal
            // every poll cycle so players who missed the previous signal will catch it.
            if ((_interdictDetected || _pendingManualCancel) && settings.IsCoordinator)
            {
                try
                {
                    string reason = _interdictDetected ? "interdict" : "";
                    PostAction(settings, "cancel_attacks", new Dictionary<string, object>
                        { ["player_name"] = GetLocalPlayerName(), ["reason"] = reason });
                }
                catch { }
            }

            // Non-coordinator army return reporting.
            // The coordinator handles its own reporting inside WaitForArmiesReturnMulti.
            // Non-coordinators report here so the coordinator's wait can unblock.
            if (_monitoringArmyReturn && !_armyReturnReported && !settings.IsCoordinator)
            {
                HashSet<int> vids = _sentSourceVillages;
                if (CheckLocalArmiesHome(vids))
                {
                    _armyReturnReported = true;
                    _monitoringArmyReturn = false;
                    LogInfo("[Queue] Local armies returned — reporting to API.");
                    try
                    {
                        PostAction(settings, "report_armies_status", new Dictionary<string, object>
                        {
                            ["player_name"] = GetLocalPlayerName(),
                            ["status"]      = "returned",
                        });
                    }
                    catch (Exception ex) { LogError("[Queue] report_armies_status failed: " + ex.Message); }
                }
            }
        }

        private void ApplyStateFromResponse(AutoBombMultiSettings settings, Dictionary<string, object> resp)
        {
            object stateDataObj;
            if (!resp.TryGetValue("state_data", out stateDataObj) || stateDataObj == null) return;

            var stateData = stateDataObj as Dictionary<string, object>;
            if (stateData == null) return;

            // ── Clock offset ─────────────────────────────────────────────────
            object serverTimeObj;
            if (resp.TryGetValue("server_time", out serverTimeObj) && serverTimeObj != null)
            {
                DateTime serverTime;
                if (DateTime.TryParse(serverTimeObj.ToString(), null,
                    System.Globalization.DateTimeStyles.RoundtripKind, out serverTime))
                {
                    settings.ServerClockOffsetSeconds = (DateTime.UtcNow - serverTime).TotalSeconds;
                }
            }

            // ── Session state ─────────────────────────────────────────────────
            string newState = GetStr(stateData, "state", "idle");
            settings.SessionState = newState;
            bool interdictDetected = GetBool(stateData, "interdict_detected");
            bool manualCancel = GetBool(stateData, "manual_cancel");
            bool forceRecall = GetBool(stateData, "force_recall_all");

            settings.InterdictDetected = interdictDetected;
            settings.ManualCancel = manualCancel;

            // ── Cancel/Recall Signal Handling ────────────────────────────────
            // When ANY cancel signal is detected (force recall, interdict, manual), acknowledge it
            // so coordinator knows all remote players received the signal
            if (forceRecall || interdictDetected || manualCancel)
            {
                RecallAll(settings);
                // Send acknowledgement
                try
                {
                    PostAction(settings, "acknowledge_recall", new Dictionary<string, object>
                    {
                        ["player_name"] = GetLocalPlayerName(),
                        ["signal_type"] = forceRecall ? "force_recall_all" :
                                         interdictDetected ? "interdict" : "manual_cancel"
                    });
                    LogInfo("Acknowledged " + (forceRecall ? "force recall" :
                           interdictDetected ? "interdict" : "manual cancel") + " signal.");
                }
                catch (Exception ex)
                {
                    LogWarning("Failed to acknowledge recall signal: " + ex.Message);
                }
            }

            // Clear pending cancel flags when session moves out of cancelled state
            if (newState != "cancelled")
            {
                _pendingManualCancel = false;
            }

            object coordObj;
            stateData.TryGetValue("coordinator", out coordObj);
            string coordinator = coordObj != null ? coordObj.ToString() : "";
            settings.IsCoordinator = (coordinator == GetLocalPlayerName());

            // ── Target village ────────────────────────────────────────────────
            int newTargetVidFromPoll = GetInt(stateData, "target_village_id");
            bool targetChanged = (newTargetVidFromPoll != settings.TargetVillageId);
            if (targetChanged)
                InvalidateVillageListCache(); // travel times depend on target — force rebuild
            settings.TargetVillageId = newTargetVidFromPoll;

            // When the target changes (e.g. queue advanced to the next target), a non-coordinator
            // must promptly recompute its villages' travel times for the new target and repost,
            // so the coordinator can read correct values before starting the bomb timer.
            if (targetChanged && !settings.IsCoordinator && newTargetVidFromPoll > 0)
            {
                try { PostPlayerReady(settings); } catch { }
            }

            // ── Log recall acknowledgements ──────────────────────────────────
            object ackObj;
            if (stateData.TryGetValue("recall_acknowledged", out ackObj) && ackObj is Dictionary<string, object>)
            {
                var acks = ackObj as Dictionary<string, object>;
                if (acks.Count > 0 && settings.IsCoordinator)
                {
                    var apiAcks = new List<string>();
                    var localAcks = new List<string>();
                    foreach (var kv in acks)
                    {
                        var ackData = kv.Value as Dictionary<string, object>;
                        string signalType = (ackData != null && ackData.ContainsKey("signal_type")) ?
                            ackData["signal_type"].ToString() : "unknown";
                        int armiesCount = 0;
                        if (ackData != null && ackData.ContainsKey("armies_recalled"))
                            int.TryParse(ackData["armies_recalled"].ToString(), out armiesCount);

                        string entry = kv.Key;
                        if (armiesCount > 0) entry += "[" + armiesCount + "]";

                        if (signalType == "local_recall")
                            localAcks.Add(entry);
                        else
                            apiAcks.Add(entry + "(" + signalType + ")");
                    }
                    if (apiAcks.Count > 0)
                        LogInfo("[Recall] API Signal acknowledged by: " + string.Join(", ", apiAcks.ToArray()));
                    if (localAcks.Count > 0)
                        LogInfo("[Recall] Local recalls executed by: " + string.Join(", ", localAcks.ToArray()));
                }
            }

            // ── Connected players ─────────────────────────────────────────────
            object playersObj;
            if (stateData.TryGetValue("players", out playersObj) && playersObj is System.Collections.ArrayList)
            {
                var playerList = new List<MultiPlayerInfo>();
                foreach (var item in (System.Collections.ArrayList)playersObj)
                {
                    var pd = item as Dictionary<string, object>;
                    if (pd == null) continue;
                    var pi = new MultiPlayerInfo();
                    pi.PlayerName    = GetStr(pd, "name", "");
                    pi.IsCoordinator = (pi.PlayerName == coordinator);
                    pi.Ready         = GetBool(pd, "ready");
                    pi.TravelTarget  = GetInt(pd, "travel_target");

                    object villagesObj;
                    if (pd.TryGetValue("villages", out villagesObj) && villagesObj is System.Collections.ArrayList)
                    {
                        foreach (var vi in (System.Collections.ArrayList)villagesObj)
                        {
                            var vd = vi as Dictionary<string, object>;
                            if (vd == null) continue;
                            pi.Villages.Add(new MultiVillageInfo
                            {
                                VillageId        = GetInt(vd, "village_id"),
                                VillageName      = GetStr(vd, "village_name", ""),
                                NumPeasants      = GetInt(vd, "peasants"),
                                NumArchers       = GetInt(vd, "archers"),
                                NumPikemen       = GetInt(vd, "pikemen"),
                                NumSwordsmen     = GetInt(vd, "swordsmen"),
                                NumCatapults     = GetInt(vd, "catapults"),
                                NumCaptains      = GetInt(vd, "captains"),
                                TravelTimeArmy   = GetDouble(vd, "travel_time_army"),
                                TravelTimeCaptain= GetDouble(vd, "travel_time_captain"),
                                IsVassal         = GetBool(vd, "is_vassal"),
                                ParentVillageId  = GetInt(vd, "parent_village_id"),
                            });
                        }
                    }
                    playerList.Add(pi);
                }
                settings.ConnectedPlayers = playerList;
            }

            // ── Apply attack statuses back to village info ────────────────────
            object attacksObj;
            if (stateData.TryGetValue("attacks", out attacksObj) && attacksObj is System.Collections.ArrayList)
            {
                var statusMap = new Dictionary<int, string>();
                foreach (var item in (System.Collections.ArrayList)attacksObj)
                {
                    var ad = item as Dictionary<string, object>;
                    if (ad == null) continue;
                    statusMap[GetInt(ad, "source_village_id")] = GetStr(ad, "status", "");
                }
                foreach (var player in settings.ConnectedPlayers)
                    foreach (var v in player.Villages)
                    {
                        string st;
                        if (statusMap.TryGetValue(v.VillageId, out st)) v.AttackStatus = st;
                    }
            }

            // ── State transition handling ─────────────────────────────────────
            if (newState != _lastSessionState)
            {
                LogInfo("Session state changed: " + _lastSessionState + " → " + newState);
                _lastSessionState = newState;

                if (newState == "preparing")
                    OnSessionPreparing(settings, stateData);
                else if (newState == "launching")
                    OnSessionLaunching(settings, stateData);
                else if (newState == "cancelled")
                    OnSessionCancelled(settings);
            }
            else if (newState == "launching" && !_launching)
            {
                // Re-trigger only when the server has started a genuinely new launch
                // (coordinator called start_timer again). The launch_id changes each time
                // start_timer is called, so if it matches what we already processed we
                // know our thread finished its batch and we should not spawn a duplicate.
                string launchId = GetStr(stateData, "launch_id", "");
                if (launchId != _lastProcessedLaunchId)
                    OnSessionLaunching(settings, stateData);
            }
        }

        private void OnSessionPreparing(AutoBombMultiSettings settings, Dictionary<string, object> stateData)
        {
            if (_preparing) return;

            string myName = GetLocalPlayerName();
            int targetVid = GetInt(stateData, "target_village_id");

            object attacksObj;
            if (!stateData.TryGetValue("attacks", out attacksObj) || !(attacksObj is System.Collections.ArrayList))
            {
                LogWarning("No attacks in API state — cannot prepare.");
                return;
            }

            var myEntries = new List<BombAttackEntry>();
            foreach (var item in (System.Collections.ArrayList)attacksObj)
            {
                var ad = item as Dictionary<string, object>;
                if (ad == null) continue;
                if (GetStr(ad, "source_player", "") != myName) continue;
                if (!GetBool(ad, "selected", true)) continue;

                myEntries.Add(new BombAttackEntry
                {
                    SourceVillageId   = GetInt(ad, "source_village_id"),
                    ParentVillageId   = GetInt(ad, "parent_village_id"),
                    TargetVillageId   = targetVid,
                    AttackType        = GetInt(ad, "attack_type", 11),
                    FormationName     = GetStr(ad, "formation", ""),
                    Stack             = GetInt(ad, "stack", 1),
                    TravelTimeSeconds = GetDouble(ad, "travel_time_seconds"),
                    CardType          = GetInt(ad, "card_type"),
                    CaptainsOnly      = GetBool(ad, "captains_only"),
                    Status            = "Validating...",
                    ScheduledSendTime = DateTime.MaxValue,
                });
            }

            if (myEntries.Count == 0)
            {
                LogInfo("No attacks assigned to this player — nothing to prepare.");
                return;
            }

            _cancelLaunchEvent.Reset();
            _preparing = true;
            _prepareThread = new Thread(PrepareThreadProc);
            _prepareThread.IsBackground = true;
            _prepareThread.Name = "AutoBombMulti Prepare";
            _prepareThread.Start(new LaunchContext { Settings = settings, Attacks = myEntries });
            LogInfo("Prepare thread started: " + myEntries.Count + " attack(s) to validate.");
        }

        private void PrepareThreadProc(object obj)
        {
            var ctx      = (LaunchContext)obj;
            var settings = ctx.Settings;
            var attacks  = ctx.Attacks;

            try
            {
                foreach (var entry in attacks)
                {
                    if (_cancelLaunchEvent.WaitOne(0)) break;

                    bool ok = PrepareAttackOnThread(entry, settings);

                    // Discard the CastleMap — validation only; launch thread re-prepares fresh.
                    entry.PreparedCastleMap = null;
                    entry.Prepared = false;

                    LogInfo("[Prepare] Village " + entry.SourceVillageId + ": prepare result=" + ok + " status='" + entry.Status + "'");

                    if (ok)
                    {
                        bool reported = PostAttackEvent(settings, "attack_validated", entry.SourceVillageId);
                        // Only mark validated after the API acknowledges — if the post failed,
                        // keep "Validating..." so the finally safety-net reports it as failed_prepare.
                        if (reported) entry.Status = "validated";
                        else LogWarning("[Prepare] Village " + entry.SourceVillageId + ": attack_validated POST failed — safety net will catch.");
                    }
                    else
                    {
                        PostAttackEvent(settings, "attack_failed_prepare", entry.SourceVillageId,
                            entry.Status);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("[Prepare thread] Unhandled exception: " + ex.Message);
            }
            finally
            {
                // Safety net: any attack still at the initial status was never reported
                // (loop broke early due to cancel/exception). Report them as cancelled
                // so the API can advance out of 'preparing' and not get stuck.
                foreach (var entry in attacks)
                {
                    if (entry.Status == "Validating...")
                    {
                        entry.Status = "cancelled";
                        PostAttackEvent(settings, "attack_failed_prepare", entry.SourceVillageId,
                            "cancelled");
                    }
                }
                _preparing = false;
                LogInfo("[Prepare thread] Validation complete.");
            }
        }

        private void OnSessionLaunching(AutoBombMultiSettings settings, Dictionary<string, object> stateData)
        {
            if (_launching) return;

            _lastProcessedLaunchId = GetStr(stateData, "launch_id", "");

            string myName = GetLocalPlayerName();

            // Build BombAttackEntry list for this player's villages only
            var myEntries = new List<BombAttackEntry>();

            object attacksObj;
            object sendTimesObj;
            stateData.TryGetValue("scheduled_send_times", out sendTimesObj);

            // In multi-player, we need send times for ALL players' villages, not just the local player.
            // Extract all "player|village" send times so that remote players' villages also get scheduled.
            var allSendTimes = new Dictionary<string, string>();
            var st = sendTimesObj as Dictionary<string, object>;
            if (st != null)
            {
                foreach (var kv in st)
                    allSendTimes[kv.Key] = kv.Value.ToString();
            }

            // For logging and matching, extract just the local player's send times
            Dictionary<int, string> sendTimes = ParseSendTimesForPlayer(sendTimesObj, myName);

            LogInfo("Launching: " + sendTimes.Count + " scheduled send time(s) received from API (showing local villages only).");
            foreach (var kv in sendTimes)
                LogInfo("  Village " + kv.Key + " → send at " + kv.Value);

            if (!stateData.TryGetValue("attacks", out attacksObj) || !(attacksObj is System.Collections.ArrayList))
            {
                LogWarning("No attacks found in API state — cannot launch.");
                return;
            }

            int targetVid = GetInt(stateData, "target_village_id");
            _currentTargetVillageId = targetVid;

            object timerSettingsObj;
            var timerSettings = stateData.TryGetValue("timer_settings", out timerSettingsObj)
                ? timerSettingsObj as Dictionary<string, object>
                : null;

            if (timerSettings != null)
            {
                settings.StackDelaySeconds = GetInt(timerSettings, "stack_delay_seconds", 1);
                settings.FakeSendEnabled   = GetBool(timerSettings, "fake_send");
                settings.AutoCancelOnInterdict = GetBool(timerSettings, "auto_cancel_on_interdict", true);
            }

            foreach (var item in (System.Collections.ArrayList)attacksObj)
            {
                var ad = item as Dictionary<string, object>;
                if (ad == null) continue;
                if (GetStr(ad, "source_player", "") != myName) continue;
                if (!GetBool(ad, "selected", true)) continue;

                int srcVid = GetInt(ad, "source_village_id");
                string sendTimeStr;
                DateTime scheduledSend = DateTime.MaxValue;
                if (sendTimes.TryGetValue(srcVid, out sendTimeStr))
                {
                    DateTime utcSend;
                    if (DateTime.TryParse(sendTimeStr, null,
                        System.Globalization.DateTimeStyles.RoundtripKind, out utcSend))
                    {
                        // Adjust for clock offset between this client and the server
                        scheduledSend = utcSend.ToLocalTime().AddSeconds(-settings.ServerClockOffsetSeconds);
                    }
                }

                double travel = GetDouble(ad, "travel_time_seconds");
                DateTime estimatedArrival = scheduledSend == DateTime.MaxValue
                    ? DateTime.MaxValue
                    : scheduledSend.AddSeconds(travel);

                var entry = new BombAttackEntry
                {
                    SourceVillageId      = srcVid,
                    ParentVillageId      = GetInt(ad, "parent_village_id"),
                    TargetVillageId      = targetVid,
                    AttackType           = GetInt(ad, "attack_type", 11),
                    FormationName        = GetStr(ad, "formation", ""),
                    Stack                = GetInt(ad, "stack", 1),
                    TravelTimeSeconds    = travel,
                    CardType             = GetInt(ad, "card_type"),
                    CaptainsOnly         = GetBool(ad, "captains_only"),
                    Status               = "Queued",
                    ScheduledSendTime    = scheduledSend,
                    EstimatedArrivalTime = estimatedArrival,
                };
                LogInfo("Launch entry: village=" + srcVid + " stack=" + entry.Stack +
                    " sendTime=" + (scheduledSend == DateTime.MaxValue ? "NO_TIME" : scheduledSend.ToString("HH:mm:ss")) +
                    " travel=" + (int)entry.TravelTimeSeconds + "s formation='" + entry.FormationName + "'");
                myEntries.Add(entry);
            }

            if (myEntries.Count == 0)
            {
                LogInfo("No attacks assigned to this player — nothing to fire.");
                return;
            }

            // Every attack MUST have a scheduled send time — partial launch wastes the whole bomb.
            // Up to 3 total attempts (initial + 2 retries), waiting 60s between each.
            const int MaxLaunchAttempts = 3;
            for (int attempt = 1; attempt <= MaxLaunchAttempts; attempt++)
            {
                var missingTime = new List<int>();
                foreach (var e in myEntries)
                    if (e.ScheduledSendTime == DateTime.MaxValue) missingTime.Add(e.SourceVillageId);
                if (missingTime.Count == 0) break;

                if (attempt == MaxLaunchAttempts)
                {
                    LogError("Launch aborted — still missing send times after " + (MaxLaunchAttempts - 1) +
                        " retries: " + string.Join(", ", missingTime.ConvertAll(v => v.ToString()).ToArray()));
                    try { PostAction(settings, "cancel_attacks", new Dictionary<string, object>
                        { ["player_name"] = GetLocalPlayerName(), ["reason"] = "missing_send_times" }); }
                    catch { }
                    return;
                }

                LogWarning("Attempt " + attempt + "/" + MaxLaunchAttempts +
                    ": missing send times for village(s) " + string.Join(", ", missingTime.ConvertAll(v => v.ToString()).ToArray()) +
                    " — retrying in 60s...");
                System.Threading.Thread.Sleep(60000);

                if (settings.IsCoordinator)
                {
                    try { PostAction(settings, "start_timer", new Dictionary<string, object>
                    {
                        ["player_name"]              = GetLocalPlayerName(),
                        ["stack_delay_seconds"]      = settings.StackDelaySeconds,
                        ["fake_send"]                = settings.FakeSendEnabled,
                        ["auto_cancel_on_interdict"] = settings.AutoCancelOnInterdict,
                    }); } catch { }
                }

                try
                {
                    var retryResp = PostAction(settings, "get_status", new Dictionary<string, object>
                        { ["player_name"] = GetLocalPlayerName() });
                    object sdObj2; if (retryResp != null) retryResp.TryGetValue("state_data", out sdObj2); else sdObj2 = null;
                    var sd2 = sdObj2 as Dictionary<string, object>;
                    if (sd2 != null)
                    {
                        object stObj2; sd2.TryGetValue("scheduled_send_times", out stObj2);
                        var st2map = ParseSendTimesForPlayer(stObj2, myName);
                        if (st2map.Count > 0)
                        {
                            foreach (var e in myEntries)
                            {
                                if (e.ScheduledSendTime != DateTime.MaxValue) continue;
                                string sv; if (!st2map.TryGetValue(e.SourceVillageId, out sv) || sv == null) continue;
                                DateTime utc2;
                                if (DateTime.TryParse(sv, null,
                                    System.Globalization.DateTimeStyles.RoundtripKind, out utc2))
                                {
                                    e.ScheduledSendTime    = utc2.ToLocalTime().AddSeconds(-settings.ServerClockOffsetSeconds);
                                    e.EstimatedArrivalTime = e.ScheduledSendTime.AddSeconds(e.TravelTimeSeconds);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { LogError("Retry get_status failed: " + ex.Message); }
            }

            lock (_attacksLock) { _myAttacks = myEntries; }

            _interdictDetected = false;
            _fakeSendTriggered = false;
            _playedCardInstanceIds.Clear();
            _cardFreeAt = DateTime.MinValue;
            _sentArmies.Clear();
            _recallAcknowledged.Clear();
            DisposeFakeSendTimer();

            StopLaunchThread();
            _cancelLaunchEvent.Reset();
            _launching = true;

            _launchThread = new Thread(LaunchThreadProc);
            _launchThread.IsBackground = true;
            _launchThread.Name = "AutoBombMulti Launch";
            _launchThread.Start(new LaunchContext { Settings = settings, Attacks = myEntries });

            LogInfo("Multi-bomb launch thread started: " + myEntries.Count + " attack(s) scheduled.");
        }

        private void OnSessionCancelled(AutoBombMultiSettings settings)
        {
            if (!_launching) return;

            // If the local interdict, fake-send, or prepare-error recall was already
            // detected, the launch thread is already managing queue advancement —
            // do not set _cancelLaunchEvent again or it will abort the army-return wait.
            // Other players (non-coordinator) won't have these flags set locally
            // so they will still cancel and recall normally.
            if (_interdictDetected || _fakeSendTriggered || _prepareErrorCancel) return;

            LogWarning("Session cancelled by API — stopping launch.");
            _cancelLaunchEvent.Set();
            CancelRemainingLocalAttacks();
            // Recall armies only in these cases:
            // 1. ManualCancel: coordinator explicitly clicked Cancel (always recall)
            // 2. InterdictDetected + AutoCancelOnInterdict: auto-cancel on interdict (respects checkbox)
            // 3. FakeSendEnabled: intentional test recall
            if (settings.ManualCancel || (settings.AutoCancelOnInterdict && settings.InterdictDetected) || settings.FakeSendEnabled)
                RecallAll(settings);
        }

        // ── Launch thread ────────────────────────────────────────────────────

        private class LaunchContext
        {
            public AutoBombMultiSettings Settings;
            public List<BombAttackEntry> Attacks;
        }

        private static readonly Comparison<BombAttackEntry> SendTimeComparer = (a, b) => {
            if (a.ScheduledSendTime == DateTime.MaxValue && b.ScheduledSendTime == DateTime.MaxValue)
                return a.Stack.CompareTo(b.Stack);
            if (a.ScheduledSendTime == DateTime.MaxValue) return 1;
            if (b.ScheduledSendTime == DateTime.MaxValue) return -1;
            return a.ScheduledSendTime.CompareTo(b.ScheduledSendTime);
        };

        private void LaunchThreadProc(object obj)
        {
            var ctx      = (LaunchContext)obj;
            var settings = ctx.Settings;
            var attacks  = ctx.Attacks;

            try
            {
                bool cancelled = false;

                while (!cancelled)
                {
                    // Sort: earliest send time first; no-time attacks go last by stack.
                    attacks.Sort(SendTimeComparer);

                    LogInfo("[Thread] Firing batch: " + attacks.Count + " attack(s) for target " +
                        _currentTargetVillageId + ".");

                    // ── Fire all attacks in this batch ─────────────────────────
                    foreach (var entry in attacks)
                    {
                        if (_cancelLaunchEvent.WaitOne(0)) { cancelled = true; break; }

                        if (entry.ScheduledSendTime == DateTime.MaxValue)
                        {
                            LogWarning("[Thread] Village " + entry.SourceVillageId +
                                " has no scheduled send time — skipping.");
                            PostAttackEvent(settings, "attack_failed_prepare", entry.SourceVillageId,
                                "no scheduled time");
                            continue;
                        }

                        // If pre-refresh is enabled, wait until 13s before send time,
                        // fire a background village refresh, then wait the remaining 5s
                        // before the prepare window. The refresh is fire-and-forget so
                        // the 5s gap is enough for the server response to arrive.
                        if (settings.PreRefreshVillages)
                        {
                            TimeSpan waitForRefresh = entry.ScheduledSendTime - DateTime.Now - TimeSpan.FromSeconds(13);
                            if (waitForRefresh > TimeSpan.Zero)
                            {
                                if (_cancelLaunchEvent.WaitOne((int)waitForRefresh.TotalMilliseconds))
                                    { cancelled = true; break; }
                            }
                            if (!_cancelLaunchEvent.WaitOne(0))
                            {
                                var syncModule = Engine != null
                                    ? Engine.GetModule<VillageSyncModule>() : null;
                                if (syncModule != null)
                                    syncModule.RefreshVillageNow(entry.SourceVillageId);
                                else
                                    LogWarning("[Thread] VillageSyncModule not found — skipping pre-refresh.");
                            }
                        }

                        // Wait until 8s before send time, then prepare
                        TimeSpan waitBefore = entry.ScheduledSendTime - DateTime.Now - TimeSpan.FromSeconds(8);
                        if (waitBefore > TimeSpan.Zero)
                        {
                            LogInfo("[Thread] Stack " + entry.Stack + ": waiting " +
                                (int)waitBefore.TotalSeconds + "s before prepare.");
                            if (_cancelLaunchEvent.WaitOne((int)waitBefore.TotalMilliseconds))
                                { cancelled = true; break; }
                        }

                        if (_cancelLaunchEvent.WaitOne(0)) { cancelled = true; break; }

                        entry.Status = "Preparing...";
                        bool ok = PrepareAttackOnThread(entry, settings);
                        if (!ok)
                        {
                            PostAttackEvent(settings, "attack_failed_prepare", entry.SourceVillageId,
                                entry.Status);
                            // Interdict manages its own cancel flow via _interdictDetected.
                            // Any other prepare failure is treated as fatal: cancel the whole
                            // batch, recall any already-sent armies, and advance the queue.
                            if (!_interdictDetected)
                            {
                                LogError("[Thread] Village " + entry.SourceVillageId +
                                    " prepare failed (" + entry.Status +
                                    ") — cancelling batch and recalling armies.");
                                _prepareErrorCancel = true;
                                _cancelLaunchEvent.Set();
                                CancelRemainingLocalAttacks();
                                bool anySentNow = false;
                                lock (_attacksLock)
                                    { foreach (var ex in _myAttacks) if (ex.Sent) { anySentNow = true; break; } }
                                if (anySentNow) RecallAll(settings);
                                try { PostAction(settings, "cancel_attacks", new Dictionary<string, object>
                                    { ["player_name"] = GetLocalPlayerName(), ["reason"] = "prepare_failed" }); }
                                catch { }
                                cancelled = true;
                            }
                            break;
                        }
                        PostAttackEvent(settings, "attack_prepared", entry.SourceVillageId);

                        // ── Card management at T-3s (fire-and-forget) ─────────
                        if (settings.PlayCards)
                        {
                            TimeSpan waitCard = entry.ScheduledSendTime - DateTime.Now - TimeSpan.FromSeconds(3);
                            if (waitCard > TimeSpan.Zero)
                            {
                                if (_cancelLaunchEvent.WaitOne((int)waitCard.TotalMilliseconds))
                                    { cancelled = true; break; }
                            }
                            if (!_cancelLaunchEvent.WaitOne(0))
                            {
                                if (!EnsureCorrectCardActive(entry.CardType, settings, entry.ScheduledSendTime))
                                {
                                    LogError("[Thread] Card management failed for village " +
                                        entry.SourceVillageId + " — cancelling batch.");
                                    _prepareErrorCancel = true;
                                    _cancelLaunchEvent.Set();
                                    CancelRemainingLocalAttacks();
                                    bool anySentCard = false;
                                    lock (_attacksLock)
                                        { foreach (var ex in _myAttacks) if (ex.Sent) { anySentCard = true; break; } }
                                    if (anySentCard) RecallAll(settings);
                                    try { PostAction(settings, "cancel_attacks",
                                        new Dictionary<string, object>
                                        {
                                            ["player_name"] = GetLocalPlayerName(),
                                            ["reason"]      = "card_failed"
                                        }); }
                                    catch { }
                                    cancelled = true;
                                    break;
                                }
                            }
                        }

                        // Wait until the exact send time
                        TimeSpan waitFire = entry.ScheduledSendTime - DateTime.Now;
                        if (waitFire > TimeSpan.Zero)
                        {
                            if (_cancelLaunchEvent.WaitOne((int)waitFire.TotalMilliseconds))
                                { cancelled = true; break; }
                        }

                        if (_cancelLaunchEvent.WaitOne(0)) { cancelled = true; break; }

                        FirePreparedAttack(entry);
                        if (entry.Sent)
                            PostAttackEvent(settings, "attack_sent", entry.SourceVillageId);
                        else
                            PostAttackEvent(settings, "attack_failed", entry.SourceVillageId, entry.Status);

                        _cancelLaunchEvent.WaitOne(500); // brief anti-spam gap
                    }

                    // Distinguish manual cancel from an automatic recall (interdict / fake-send /
                    // prepare-error). On any auto-recall armies have been recalled and we should
                    // still wait for them to return and advance the queue. Manual cancel stops.
                    // Check both local flags (_interdictDetected) and API state (settings.InterdictDetected)
                    // in case a remote player detected the interdict.
                    bool autoRecalled = _interdictDetected || _fakeSendTriggered || _prepareErrorCancel || settings.InterdictDetected;
                    if (cancelled && !autoRecalled) break;

                    // ── Queue advancement (coordinator only) ───────────────────
                    if (!settings.TargetQueueEnabled || !settings.IsCoordinator)
                        break;

                    // Count sent armies so we know whether to wait for them to return
                    int sentCount = 0;
                    foreach (var e in attacks) if (e.Sent) sentCount++;

                    // Fake-send + queue: wait for the recall timer before trying to
                    // advance — otherwise WaitForArmiesReturn sees the armies still
                    // outbound and we'd never clear them.
                    if (settings.FakeSendEnabled && sentCount > 0 && !_fakeSendTriggered)
                    {
                        LogInfo("[Queue] Waiting for fake-send recall timer before advancing...");
                        _cancelLaunchEvent.WaitOne(); // timer, interdict, or manual cancel sets this
                        if (!_fakeSendTriggered && !_interdictDetected && !_prepareErrorCancel)
                        {
                            LogInfo("[Queue] Manually cancelled before fake-send timer. Stopping.");
                            break;
                        }
                        autoRecalled = true;
                    }

                    if (sentCount > 0)
                    {
                        // Reset so WaitForArmiesReturnMulti can block cleanly.
                        // Keep _interdictDetected intact — OnTick checks !_interdictDetected
                        // to avoid re-triggering on the same still-interdicted target.
                        _cancelLaunchEvent.Reset();
                        DisposeFakeSendTimer();
                        LogInfo("[Queue] Sent " + sentCount + " attack(s) — now waiting for all armies to return before advancing.");
                        if (!WaitForArmiesReturnMulti(settings, attacks))
                        {
                            LogInfo("[Queue] Cancelled during army-return wait — stopping queue.");
                            break;
                        }
                        LogInfo("[Queue] Army return wait complete — preparing to advance queue.");
                    }
                    else
                    {
                        _cancelLaunchEvent.Reset();
                        DisposeFakeSendTimer();
                    }

                    // Advance to the next queue entry
                    LogInfo("[Queue] Advancing to next target...");
                    var nextAttacks = TryAdvanceMultiQueue(settings);
                    if (nextAttacks == null || nextAttacks.Count == 0)
                    {
                        LogInfo("[Queue] No more targets in queue — batch complete.");
                        break;
                    }
                    LogInfo("[Queue] ✓ Queue advanced successfully to next target with " + nextAttacks.Count + " attack(s).");

                    attacks = nextAttacks;
                    lock (_attacksLock) { _myAttacks = nextAttacks; }
                    _interdictDetected = false;
                    _fakeSendTriggered = false;
                    _prepareErrorCancel = false;
                    _playedCardInstanceIds.Clear();
                    _cardFreeAt = DateTime.MinValue;
                    _sentArmies.Clear();
                    _recallAcknowledged.Clear();
                    cancelled = false; // allow next iteration to fire
                }
            }
            catch (Exception ex)
            {
                LogError("[Thread] Unhandled exception: " + ex.Message);
            }
            finally
            {
                _launching = false;

                // Non-coordinators: enable poll-thread army monitoring so they report
                // back to the API when their armies return (unblocks coordinator's queue wait).
                if (!settings.IsCoordinator && settings.TargetQueueEnabled)
                {
                    var sent = new HashSet<int>();
                    lock (_attacksLock)
                    {
                        foreach (var e in _myAttacks)
                            if (e.Sent) sent.Add(e.SourceVillageId);
                    }
                    if (sent.Count > 0)
                    {
                        _sentSourceVillages  = sent;
                        _monitoringArmyReturn = true;
                        _armyReturnReported   = false;
                        LogInfo("[Thread] Monitoring " + sent.Count +
                            " village(s) for army return — will report when home.");
                    }
                }

                LogInfo("[Thread] Launch thread exiting.");
            }
        }

        // ── Queue advancement helpers ─────────────────────────────────────────

        // Simpler queue advancement: wait based on tracked sent army times and recall confirmations.
        // Instead of polling the game world, we track when armies were sent and when they were
        // recalled, then wait until all armies have enough time to return home.
        private bool WaitForArmiesReturnMulti(AutoBombMultiSettings settings,
            List<BombAttackEntry> sentAttacks)
        {
            // Simple time-based logic: wait until all sent armies have had enough time to return home.
            // We track when each army was sent and when it was recalled, then wait the appropriate time.

            LogInfo("[Queue] Tracking " + _sentArmies.Count + " sent armies — waiting for all to return safely.");

            const int AdvanceBufferSeconds = 3; // Wait 3s after all armies could have returned

            while (true)
            {
                if (_cancelLaunchEvent.WaitOne(1000))
                {
                    LogInfo("[Queue] Cancelled while waiting for armies.");
                    return false;
                }

                // ── Calculate maximum return time from sent armies ───────────────
                DateTime maxReturnTime = DateTime.MinValue;
                foreach (var sent in _sentArmies)
                {
                    DateTime returnTime;
                    if (sent.RecalledTime.HasValue)
                    {
                        // Army was recalled: it needs time to return to home after recall
                        // Rough estimate: recall_time + (time already in flight)
                        TimeSpan inFlight = sent.RecalledTime.Value - sent.SentTime;
                        returnTime = sent.RecalledTime.Value.Add(inFlight);
                    }
                    else
                    {
                        // Army not yet recalled: assume it will be or will reach target and return
                        // Use: sent_time + 2×travel_time (go and come back)
                        returnTime = sent.SentTime.AddSeconds(sent.TravelSeconds * 2);
                    }

                    if (returnTime > maxReturnTime)
                        maxReturnTime = returnTime;
                }

                if (maxReturnTime == DateTime.MinValue)
                {
                    // No armies sent, proceed immediately
                    LogInfo("[Queue] No armies tracked — proceeding to advance.");
                    return true;
                }

                // ── Check if all armies have had time to return ───────────────────
                DateTime safeToAdvanceTime = maxReturnTime.AddSeconds(AdvanceBufferSeconds);
                if (DateTime.Now >= safeToAdvanceTime)
                {
                    LogInfo("[Queue] ✓ All sent armies have had time to return (max return: " +
                        maxReturnTime.ToString("HH:mm:ss") + "). Advancing queue.");
                    return true;
                }

                // Log progress periodically
                double secondsLeft = (safeToAdvanceTime - DateTime.Now).TotalSeconds;
                if (secondsLeft > 0 && secondsLeft < 5)
                    LogInfo("[Queue] Waiting " + secondsLeft.ToString("F1") + "s more before advancing...");
            }
        }

        private bool CheckLocalArmiesHome(HashSet<int> sourceVids)
        {
            if (sourceVids == null || sourceVids.Count == 0) return true;
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return false;
                foreach (WorldMap.LocalArmyData army in GameEngine.Instance.World.getArmyArray())
                {
                    if (army == null || army.dead) continue;
                    if (sourceVids.Contains(army.homeVillageID)) return false;
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Returns the base travel time (seconds) a remote player posted for one of their
        /// villages, from the latest connected-players data. Returns 0 if not found.
        /// </summary>
        private double GetPostedTravelForVillage(AutoBombMultiSettings settings, int villageId, bool captainsOnly)
        {
            if (settings.ConnectedPlayers != null)
                foreach (var pi in settings.ConnectedPlayers)
                    foreach (var vi in pi.Villages)
                        if (vi.VillageId == villageId)
                            return captainsOnly ? vi.TravelTimeCaptain : vi.TravelTimeArmy;
            return 0;
        }

        /// <summary>
        /// Builds an attack-config list for a target. Travel time is computed locally for the
        /// coordinator's own villages (accurate) and taken from the owning player's posted data
        /// for remote villages (the coordinator can't compute another player's village location).
        /// Card speed is applied. Entries are de-duplicated on player+village+vassal.
        /// </summary>
        private List<Dictionary<string, object>> BuildQueueAttackList(
            AutoBombMultiSettings settings, List<Dictionary<string, object>> sourceAttacks,
            int targetVid, string myName)
        {
            var list = new List<Dictionary<string, object>>();
            var seen = new HashSet<string>();
            foreach (var ad in sourceAttacks)
            {
                int srcVid = GetInt(ad, "source_village_id");
                string player = GetStr(ad, "source_player", "");
                bool isVassal = GetBool(ad, "is_vassal");
                string key = player + "|" + srcVid + "|" + (isVassal ? "v" : "p");
                if (srcVid <= 0 || !seen.Add(key))
                {
                    if (srcVid > 0) LogWarning("[Queue] Skipping duplicate attack " + key + ".");
                    continue;
                }
                bool captainsOnly = GetBool(ad, "captains_only");
                int cardType = GetInt(ad, "card_type");
                double baseTravel = (player == myName)
                    ? AutoBombModule.CalculateBaseTravelTime(srcVid, targetVid, captainsOnly)
                    : GetPostedTravelForVillage(settings, srcVid, captainsOnly);
                double newTravel = AutoBombModule.ApplyCardSpeed(baseTravel, cardType);
                list.Add(new Dictionary<string, object>
                {
                    ["source_player"]       = player,
                    ["source_village_id"]   = srcVid,
                    ["parent_village_id"]   = GetInt(ad, "parent_village_id"),
                    ["is_vassal"]           = isVassal,
                    ["formation"]           = GetStr(ad, "formation", ""),
                    ["stack"]               = GetInt(ad, "stack", 1),
                    ["card_type"]           = cardType,
                    ["captains_only"]       = captainsOnly,
                    ["attack_type"]         = GetInt(ad, "attack_type", 11),
                    ["travel_time_seconds"] = newTravel,
                    ["selected"]            = GetBool(ad, "selected", true),
                });
            }
            return list;
        }

        /// <summary>
        /// Waits until every remote player with a selected attack has recomputed its travel
        /// times for the given target (reported via the travel_target heartbeat field), or until
        /// timeout. Returns false only if cancelled. Coordinator's own villages need no wait.
        /// </summary>
        private bool WaitForRemoteTravelReady(AutoBombMultiSettings settings,
            List<Dictionary<string, object>> sourceAttacks, int targetVid, string myName, int timeoutMs)
        {
            var remotePlayers = new HashSet<string>();
            foreach (var ad in sourceAttacks)
            {
                if (!GetBool(ad, "selected", true)) continue;
                string p = GetStr(ad, "source_player", "");
                if (!string.IsNullOrEmpty(p) && p != myName) remotePlayers.Add(p);
            }
            if (remotePlayers.Count == 0) return true; // only own villages — nothing to wait for

            LogInfo("[Queue] Waiting for " + remotePlayers.Count +
                " remote player(s) to recompute travel for target " + targetVid + "...");
            DateTime deadline = DateTime.Now.AddMilliseconds(timeoutMs);
            while (DateTime.Now < deadline)
            {
                int ready = 0;
                var cp = settings.ConnectedPlayers;
                if (cp != null)
                    foreach (string p in remotePlayers)
                        foreach (var pi in cp)
                            if (pi.PlayerName == p && pi.TravelTarget == targetVid) { ready++; break; }

                if (ready >= remotePlayers.Count)
                {
                    LogInfo("[Queue] All remote players ready for target " + targetVid + ".");
                    return true;
                }
                if (_cancelLaunchEvent.WaitOne(1000)) return false; // cancelled
            }
            LogWarning("[Queue] Timed out waiting for remote players to recompute travel for target " +
                targetVid + " — proceeding with best-effort travel times.");
            return true;
        }

        private List<BombAttackEntry> TryAdvanceMultiQueue(AutoBombMultiSettings settings)
        {
            // Mark current target done
            foreach (TargetQueueEntry qe in settings.TargetQueue)
            {
                if (qe.VillageId == _currentTargetVillageId && !qe.Completed)
                {
                    qe.Completed = true;
                    LogInfo("[Queue] Target " + qe.VillageId + " marked complete.");
                    break;
                }
            }

            // Find next uncompleted entry
            TargetQueueEntry next = null;
            foreach (TargetQueueEntry qe in settings.TargetQueue)
                if (!qe.Completed) { next = qe; break; }

            if (next == null)
            {
                LogInfo("[Queue] All targets in queue completed.");
                return null;
            }

            int newTargetVid = next.VillageId;
            LogInfo("[Queue] Advancing to next target: " + newTargetVid +
                (string.IsNullOrEmpty(next.Label) ? "" : " (" + next.Label + ")"));

            settings.TargetVillageId = newTargetVid;
            _currentTargetVillageId  = newTargetVid;

            try
            {
                // Use the locally cached attack config rather than re-fetching from the API.
                // After an interdict cancel the PHP state can corrupt the attacks[] array
                // (sent villages get dropped or replaced with duplicates of the first entry).
                // The cache was set when PushAttackConfig was last called from the UI, and
                // is refreshed each time we advance the queue, so it's always up to date.
                List<Dictionary<string, object>> sourceAttacks;
                if (_cachedAttackConfig != null && _cachedAttackConfig.Count > 0)
                {
                    sourceAttacks = _cachedAttackConfig;
                    LogInfo("[Queue] Rebuilding from cached config (" + sourceAttacks.Count + " attack(s)).");
                }
                else
                {
                    // No cache yet — fall back to API fetch with deduplication.
                    LogInfo("[Queue] No local cache — fetching attack config from API.");
                    var resp0 = PostAction(settings, "get_status", new Dictionary<string, object>
                        { ["player_name"] = GetLocalPlayerName() });
                    if (resp0 == null) { LogError("[Queue] get_status (config) returned null."); return null; }
                    object sd0Obj; resp0.TryGetValue("state_data", out sd0Obj);
                    var sd0 = sd0Obj as Dictionary<string, object>;
                    if (sd0 == null) { LogError("[Queue] No state_data in config fetch."); return null; }
                    object ao0; sd0.TryGetValue("attacks", out ao0);
                    var aal0 = ao0 as System.Collections.ArrayList;
                    if (aal0 == null) { LogError("[Queue] No attacks array in state."); return null; }
                    sourceAttacks = new List<Dictionary<string, object>>();
                    var seenFb = new HashSet<string>();
                    foreach (var item0 in aal0)
                    {
                        var ad0 = item0 as Dictionary<string, object>;
                        if (ad0 == null) continue;
                        int v0 = GetInt(ad0, "source_village_id");
                        string key0 = GetStr(ad0, "source_player", "") + "|" + v0 + "|" +
                            (GetBool(ad0, "is_vassal") ? "v" : "p");
                        if (v0 <= 0 || !seenFb.Add(key0)) continue;
                        sourceAttacks.Add(ad0);
                    }
                }

                string myName = GetLocalPlayerName();

                // ── Phase 1: announce the new target ──────────────────────────────
                // Push a preliminary config so the new target_village_id reaches the API.
                // Remote clients see the target change on their next poll and immediately
                // recompute + repost their villages' travel times for the new target.
                // (Own villages use accurate local travel; remote use posted values, which
                //  are still for the previous target at this point — corrected in phase 2.)
                var prelimList = BuildQueueAttackList(settings, sourceAttacks, newTargetVid, myName);
                PostAction(settings, "set_attack_config", new Dictionary<string, object>
                {
                    ["player_name"]       = GetLocalPlayerName(),
                    ["target_village_id"] = newTargetVid,
                    ["attacks"]           = prelimList,
                });
                LogInfo("[Queue] Announced new target " + newTargetVid + " — waiting for remote travel times.");

                // ── Phase 2: wait for remote players, then push corrected config ─────
                // Wait until every remote player with a selected attack has recomputed travel
                // for the new target (or timeout). The background poll thread refreshes
                // ConnectedPlayers (incl. each player's TravelTarget) while we wait.
                if (!WaitForRemoteTravelReady(settings, sourceAttacks, newTargetVid, myName, 25000))
                    return null; // cancelled

                // Rebuild with now-fresh remote travel times and push the final config.
                var newAttackList = BuildQueueAttackList(settings, sourceAttacks, newTargetVid, myName);

                // Update cache so the next queue advance also has correct data
                _cachedAttackConfig = new List<Dictionary<string, object>>(newAttackList);

                foreach (var atk in newAttackList)
                    LogInfo("[Queue] Travel: village=" + GetInt(atk, "source_village_id") +
                        " player=" + GetStr(atk, "source_player", "") +
                        " travel=" + (int)GetDouble(atk, "travel_time_seconds") +
                        "s card=" + GetInt(atk, "card_type"));

                PostAction(settings, "set_attack_config", new Dictionary<string, object>
                {
                    ["player_name"]       = GetLocalPlayerName(),
                    ["target_village_id"] = newTargetVid,
                    ["attacks"]           = newAttackList,
                });
                LogInfo("[Queue] Final config pushed for target " + newTargetVid + ".");

                // Brief settle so non-coordinators poll the final config before the timer fires.
                if (_cancelLaunchEvent.WaitOne(2000)) return null;

                // Start the timer — sets state → launching with new send times + new launch_id
                PostAction(settings, "start_timer", new Dictionary<string, object>
                {
                    ["player_name"]              = GetLocalPlayerName(),
                    ["stack_delay_seconds"]      = settings.StackDelaySeconds,
                    ["fake_send"]                = settings.FakeSendEnabled,
                    ["auto_cancel_on_interdict"] = settings.AutoCancelOnInterdict,
                });
                LogInfo("[Queue] Timer started for target " + newTargetVid + ".");

                // Fetch resulting state (contains new scheduled_send_times and launch_id)
                Dictionary<string, object> resp = PostAction(settings, "get_status", new Dictionary<string, object>
                    { ["player_name"] = GetLocalPlayerName() });
                if (resp == null) { LogError("[Queue] get_status (post-timer) returned null."); return null; }

                object stateDataObj;
                if (!resp.TryGetValue("state_data", out stateDataObj)) return null;
                Dictionary<string, object> stateData = stateDataObj as Dictionary<string, object>;
                if (stateData == null) return null;

                // Record the new launch_id and update clock offset — but do NOT call
                // ApplyStateFromResponse here (we're already on the launch thread and
                // _launching == true; calling it would try to start another thread).
                object serverTimeObj;
                if (resp.TryGetValue("server_time", out serverTimeObj) && serverTimeObj != null)
                {
                    DateTime serverTime;
                    if (DateTime.TryParse(serverTimeObj.ToString(), null,
                        System.Globalization.DateTimeStyles.RoundtripKind, out serverTime))
                        settings.ServerClockOffsetSeconds = (DateTime.UtcNow - serverTime).TotalSeconds;
                }
                _lastProcessedLaunchId = GetStr(stateData, "launch_id", "");
                _lastSessionState = "launching"; // suppress poll re-trigger for this batch

                // Build this player's new BombAttackEntry list (myName declared earlier)
                // Parse scheduled send times for this player (keyed "player|vid" by the API)
                object sendTimesObj;
                stateData.TryGetValue("scheduled_send_times", out sendTimesObj);
                var sendTimes = ParseSendTimesForPlayer(sendTimesObj, myName);
                LogInfo("[Queue] " + sendTimes.Count + " send time(s) received for target " + newTargetVid + ".");
                foreach (var kv in sendTimes)
                    LogInfo("[Queue]   Village " + kv.Key + " → send at " + kv.Value);

                var newEntries = new List<BombAttackEntry>();

                object newAttacksObj;
                stateData.TryGetValue("attacks", out newAttacksObj);
                var attacksList = newAttacksObj as System.Collections.ArrayList;
                if (attacksList == null) return null;

                foreach (var item in attacksList)
                {
                    var ad = item as Dictionary<string, object>;
                    if (ad == null) continue;
                    if (GetStr(ad, "source_player", "") != myName) continue;
                    if (!GetBool(ad, "selected", true)) continue;

                    int srcVid = GetInt(ad, "source_village_id");
                    DateTime scheduledSend = DateTime.MaxValue;
                    string sendTimeStr;
                    if (sendTimes.TryGetValue(srcVid, out sendTimeStr))
                    {
                        DateTime utcSend;
                        if (DateTime.TryParse(sendTimeStr, null,
                            System.Globalization.DateTimeStyles.RoundtripKind, out utcSend))
                            scheduledSend = utcSend.ToLocalTime()
                                .AddSeconds(-settings.ServerClockOffsetSeconds);
                    }

                    double travel = GetDouble(ad, "travel_time_seconds");
                    newEntries.Add(new BombAttackEntry
                    {
                        SourceVillageId      = srcVid,
                        ParentVillageId      = GetInt(ad, "parent_village_id"),
                        TargetVillageId      = newTargetVid,
                        AttackType           = GetInt(ad, "attack_type", 11),
                        FormationName        = GetStr(ad, "formation", ""),
                        Stack                = GetInt(ad, "stack", 1),
                        TravelTimeSeconds    = travel,
                        CardType             = GetInt(ad, "card_type"),
                        CaptainsOnly         = GetBool(ad, "captains_only"),
                        Status               = "Queued",
                        ScheduledSendTime    = scheduledSend,
                        EstimatedArrivalTime = scheduledSend == DateTime.MaxValue
                            ? DateTime.MaxValue : scheduledSend.AddSeconds(travel),
                    });
                }

                if (newEntries.Count == 0)
                {
                    LogWarning("[Queue] No attacks for this player on target " + newTargetVid + " — skipping.");
                    return null;
                }

                // Every attack must have a send time — partial launch wastes the whole bomb.
                // Up to 3 total attempts (initial + 2 retries), waiting 60s between each.
                const int MaxQueueAttempts = 3;
                for (int attempt = 1; attempt <= MaxQueueAttempts; attempt++)
                {
                    var missingTime = new List<int>();
                    foreach (var e in newEntries)
                        if (e.ScheduledSendTime == DateTime.MaxValue) missingTime.Add(e.SourceVillageId);
                    if (missingTime.Count == 0) break;

                    if (attempt == MaxQueueAttempts)
                    {
                        LogError("[Queue] Launch aborted for target " + newTargetVid +
                            " — still missing send times after " + (MaxQueueAttempts - 1) +
                            " retries: " + string.Join(", ", missingTime.ConvertAll(v => v.ToString()).ToArray()));
                        try { PostAction(settings, "cancel_attacks", new Dictionary<string, object>
                            { ["player_name"] = GetLocalPlayerName(), ["reason"] = "missing_send_times" }); }
                        catch { }
                        return null;
                    }

                    LogWarning("[Queue] Attempt " + attempt + "/" + MaxQueueAttempts +
                        ": missing send times for village(s) " + string.Join(", ", missingTime.ConvertAll(v => v.ToString()).ToArray()) +
                        " — retrying in 60s...");
                    if (_cancelLaunchEvent.WaitOne(60000)) return null;

                    // Re-call start_timer and re-fetch
                    PostAction(settings, "start_timer", new Dictionary<string, object>
                    {
                        ["player_name"]              = GetLocalPlayerName(),
                        ["stack_delay_seconds"]      = settings.StackDelaySeconds,
                        ["fake_send"]                = settings.FakeSendEnabled,
                        ["auto_cancel_on_interdict"] = settings.AutoCancelOnInterdict,
                    });

                    resp = PostAction(settings, "get_status", new Dictionary<string, object>
                        { ["player_name"] = GetLocalPlayerName() });
                    if (resp == null) { LogError("[Queue] Retry get_status null."); return null; }
                    if (!resp.TryGetValue("state_data", out stateDataObj)) return null;
                    stateData = stateDataObj as Dictionary<string, object>;
                    if (stateData == null) return null;

                    _lastProcessedLaunchId = GetStr(stateData, "launch_id", "");
                    _lastSessionState      = "launching";

                    object stObj2; stateData.TryGetValue("scheduled_send_times", out stObj2);
                    sendTimes = ParseSendTimesForPlayer(stObj2, myName);

                    newEntries = new List<BombAttackEntry>();
                    object attacksObj2; stateData.TryGetValue("attacks", out attacksObj2);
                    var al2 = attacksObj2 as System.Collections.ArrayList;
                    if (al2 != null)
                    {
                        foreach (var item2 in al2)
                        {
                            var ad2 = item2 as Dictionary<string, object>;
                            if (ad2 == null) continue;
                            if (GetStr(ad2, "source_player", "") != myName) continue;
                            if (!GetBool(ad2, "selected", true)) continue;
                            int vid2 = GetInt(ad2, "source_village_id");
                            DateTime ss2 = DateTime.MaxValue;
                            string stStr2; if (sendTimes.TryGetValue(vid2, out stStr2))
                            { DateTime u2; if (DateTime.TryParse(stStr2, null, System.Globalization.DateTimeStyles.RoundtripKind, out u2))
                                ss2 = u2.ToLocalTime().AddSeconds(-settings.ServerClockOffsetSeconds); }
                            double tr2 = GetDouble(ad2, "travel_time_seconds");
                            newEntries.Add(new BombAttackEntry
                            {
                                SourceVillageId      = vid2,  TargetVillageId = newTargetVid,
                                ParentVillageId      = GetInt(ad2, "parent_village_id"),
                                AttackType           = GetInt(ad2, "attack_type", 11),
                                FormationName        = GetStr(ad2, "formation", ""),
                                Stack                = GetInt(ad2, "stack", 1),
                                TravelTimeSeconds    = tr2,   CardType = GetInt(ad2, "card_type"),
                                CaptainsOnly         = GetBool(ad2, "captains_only"),
                                Status               = "Queued",
                                ScheduledSendTime    = ss2,
                                EstimatedArrivalTime = ss2 == DateTime.MaxValue ? DateTime.MaxValue : ss2.AddSeconds(tr2),
                            });
                        }
                    }

                    LogInfo("[Queue] Retry " + attempt + " complete — " + sendTimes.Count +
                        " send time(s) for target " + newTargetVid + ".");
                }

                // Log per-entry detail so queue launches are visible in the log like initial launches
                foreach (var e in newEntries)
                    LogInfo("[Queue] Launch entry: village=" + e.SourceVillageId +
                        " stack=" + e.Stack +
                        " sendTime=" + e.ScheduledSendTime.ToString("HH:mm:ss") +
                        " travel=" + (int)e.TravelTimeSeconds +
                        "s formation='" + e.FormationName + "'");

                return newEntries;
            }
            catch (Exception ex)
            {
                LogError("[Queue] TryAdvanceMultiQueue failed: " + ex.Message);
                return null;
            }
        }

        // ── Prepare + Fire (adapted from AutoBombModule) ─────────────────────

        private bool PrepareAttackOnThread(BombAttackEntry entry, AutoBombMultiSettings settings)
        {
            try
            {
                if (GameEngine.Instance == null)
                {
                    entry.Status = "Error: No game engine";
                    entry.Cancelled = true;
                    return false;
                }

                _callbackEvent.Reset();
                _callbackResult = null;

                int parentVid = entry.ParentVillageId > 0 ? entry.ParentVillageId : entry.SourceVillageId;
                LogInfo("[Prepare] Village " + entry.SourceVillageId + ": dispatching PreAttackSetup (parent=" + parentVid + " target=" + entry.TargetVillageId + ", formation='" + entry.FormationName + "')");

                RemoteServices.Instance.set_PreAttackSetup_UserCallBack(
                    new RemoteServices.PreAttackSetup_UserCallBack(PreAttackSetupCallback));
                RemoteServices.Instance.PreAttackSetup(
                    parentVid, entry.SourceVillageId, entry.TargetVillageId,
                    0, 0, 0, 0, 0, 11, 0, 0);

                // Loop to discard callbacks belonging to other concurrent modules (e.g. AutoBombModule)
                // that share the same RemoteServices callback slot.
                PreAttackSetup_ReturnType rd = null;
                DateTime callbackDeadline = DateTime.Now.AddSeconds(30);
                while (true)
                {
                    int remaining = (int)(callbackDeadline - DateTime.Now).TotalMilliseconds;
                    if (remaining <= 0) remaining = 0;

                    int waitIdx = WaitHandle.WaitAny(
                        new WaitHandle[] { _callbackEvent, _cancelLaunchEvent }, remaining);

                    if (waitIdx != 0)
                    {
                        if (waitIdx == WaitHandle.WaitTimeout) entry.Status = "Error: Callback timeout";
                        else entry.Status = "Error: Cancelled";
                        entry.Cancelled = true;
                        LogInfo("[Prepare] Village " + entry.SourceVillageId + ": WaitAny exit=" +
                            (waitIdx == WaitHandle.WaitTimeout ? "TIMEOUT" : "CANCELLED"));
                        return false;
                    }

                    PreAttackSetup_ReturnType candidate = _callbackResult;
                    _callbackEvent.Reset();

                    if (candidate == null) continue;

                    if (candidate.attackingVillage != entry.SourceVillageId)
                    {
                        // attackingVillage == 0 means the server returned an error response
                        // for our request (village data wasn't populated). Treat it as our
                        // actual (failed) callback rather than looping forever until timeout.
                        if (candidate.attackingVillage == 0)
                        {
                            entry.Status = "Error: Server returned village 0 (errorCode=" +
                                candidate.m_errorCode + ")";
                            entry.Cancelled = true;
                            LogError("[Prepare] Village " + entry.SourceVillageId +
                                ": server returned attackingVillage=0 — treating as failed response.");
                            return false;
                        }
                        LogWarning("[Prepare] Discarding stale callback for village " +
                            candidate.attackingVillage + " (expected " + entry.SourceVillageId + ")");
                        continue;
                    }

                    rd = candidate;
                    LogInfo("[Prepare] Village " + entry.SourceVillageId + ": callback success=" + rd.Success +
                        " protected=" + rd.protectedVillage + " vacation=" + rd.vacationVillage +
                        " peace=" + rd.peaceVillage + " errorCode=" + rd.m_errorCode);
                    break;
                }

                if (rd.protectedVillage)
                {
                    entry.Status = "Interdicted";
                    if (settings.AutoCancelOnInterdict)
                    {
                        _interdictDetected = true;
                        LogWarning("[Thread] Interdict detected during prepare — cancelling.");
                        CancelRemainingLocalAttacks();
                        RecallAll(settings);
                        _cancelLaunchEvent.Set();
                        try { PostAction(settings, "cancel_attacks", new Dictionary<string, object>
                            { ["player_name"] = GetLocalPlayerName(), ["reason"] = "interdict" }); }
                        catch { }
                    }
                    else { entry.Cancelled = true; }
                    return false;
                }
                if (rd.vacationVillage)    { entry.Status = "Target on vacation";  entry.Cancelled = true; return false; }
                if (rd.vassalVacation)     { entry.Status = "Vassal on vacation";  entry.Cancelled = true; return false; }
                if (rd.peaceVillage)       { entry.Status = "Target in peace";     entry.Cancelled = true; return false; }
                if (rd.peaceAttacker)      { entry.Status = "Attacker in peace";   entry.Cancelled = true; return false; }
                if (rd.protectedAttacker)  { entry.Status = entry.ParentVillageId > 0 ? "Vassal interdicted" : "Attacker interdicted"; entry.Cancelled = true; return false; }
                if (!rd.Success)
                {
                    entry.Status = "Server error: " + rd.m_errorCode;
                    entry.Cancelled = true;
                    return false;
                }

                int honourCost = 0;
                if (rd.battleHonourData != null)
                {
                    rd.battleHonourData.attackType = 11;
                    if (!GameEngine.Instance.World.isCapital(rd.parentAttackingVillage))
                        honourCost = CastlesCommon.calcBattleHonourCost(
                            rd.battleHonourData,
                            GameEngine.Instance.LocalWorldData.Alternate_Ruleset,
                            GameEngine.Instance.LocalWorldData.EraWorld);
                }
                if (honourCost > 0 && GameEngine.Instance.World.getCurrentHonour() <= 0.0)
                {
                    entry.Status = "Error: Need honour";
                    entry.Cancelled = true;
                    return false;
                }

                int pillageLevel = ResearchData.pillageLevels[(int)GameEngine.Instance.World.UserResearchData.Research_Pillaging];
                int ransackLevel = ResearchData.ransackLevels[(int)GameEngine.Instance.World.UserResearchData.Research_Ransack];
                int attackType = 11;
                int pillagePercent = 0;
                if (GameEngine.Instance.World.IsPlayerVillage(rd.targetVillage))
                {
                    attackType = entry.AttackType;
                    if (attackType == 2 || attackType == 4 || attackType == 5 ||
                        attackType == 6 || attackType == 7)
                        pillagePercent = pillageLevel;
                    else if (attackType == 3)
                        pillagePercent = ransackLevel;
                }

                CastleMap castleMap = new CastleMap(-1, GameEngine.Instance.GFX, 1);
                castleMap.SetUsingCastleTroopsOK(true);
                castleMap.importDefenderSnapshot(
                    rd.castleMapSnapshot, rd.castleTroopsSnapshot, rd.keepLevel, true, rd.landType);
                castleMap.initRealSetup(
                    rd.attackingVillage, rd.targetVillage,
                    rd.numPeasants, rd.numArchers, rd.numPikemen, rd.numSwordsmen, rd.numCatapults,
                    attackType, pillagePercent, rd.captainsCommand, rd.parentAttackingVillage,
                    rd.numPeasantsInCastle, rd.numArchersInCastle, rd.numPikemenInCastle,
                    rd.numSwordsmenInCastle, rd.targetUserID, rd.targetUserName,
                    rd.battleHonourData, rd.numCaptainsInCastle, rd.numCaptains,
                    rd.capitalAttackRate);

                if (string.IsNullOrEmpty(entry.FormationName))
                {
                    entry.Status = "Error: No formation";
                    entry.Cancelled = true;
                    return false;
                }

                // Resolve the formation into a positioned element list (cloud preset or local .cas).
                List<CastleMap.RestoreCastleElement> formationElements = null;
                CastleMapPreset cloudPreset = AutoBombModule.FindCloudPreset(entry.FormationName);
                if (cloudPreset != null)
                    formationElements = AutoBombModule.PresetToElementList(cloudPreset);
                else
                    formationElements = castleMap.getAttackSetup(entry.FormationName);

                if (formationElements == null || formationElements.Count == 0)
                {
                    entry.Status = "Error: Formation not found";
                    entry.Cancelled = true;
                    return false;
                }

                int[] counts = AutoBombModule.GetTroopsCountArray12(formationElements);
                // HasEnoughTroopsToPlace fills counts[6..11] with the village's available troops.
                bool enough = castleMap.HasEnoughTroopsToPlace(ref counts);

                if (!enough)
                {
                    if (!settings.SendWithMissingTroops)
                    {
                        entry.Status = "Error: Not enough troops";
                        entry.Cancelled = true;
                        return false;
                    }

                    // Partial send: keep only the troops actually available, drop the rest.
                    int[] available6 = new int[6];
                    for (int i = 0; i < 6; i++) available6[i] = counts[i + 6];
                    int[] placedByType;
                    formationElements = AutoBombModule.FilterFormationToAvailable(
                        formationElements, available6, out placedByType);

                    LogWarning("[Prepare] Village " + entry.SourceVillageId +
                        ": insufficient troops for '" + entry.FormationName +
                        "' — sending partial (P=" + placedByType[0] + " A=" + placedByType[1] +
                        " Pk=" + placedByType[2] + " S=" + placedByType[3] +
                        " C=" + placedByType[4] + " Cap=" + placedByType[5] + ").");
                }

                castleMap.RestoreAttackSetupBG(formationElements);

                if (castleMap.attackNumPeasants == 0 && castleMap.attackNumArchers == 0 &&
                    castleMap.attackNumPikemen == 0 && castleMap.attackNumSwordsmen == 0 &&
                    castleMap.attackNumCatapults == 0 && castleMap.attackNumCaptains == 0)
                {
                    entry.Status = "Error: No troops placed";
                    entry.Cancelled = true;
                    return false;
                }

                entry.PreparedCastleMap      = castleMap;
                entry.PreparedAttackType     = attackType;
                entry.PreparedPillagePercent = pillagePercent;
                entry.Prepared = true;
                entry.Status   = "Ready (waiting to fire)";
                return true;
            }
            catch (Exception ex)
            {
                entry.Status = "Error: " + ex.Message;
                entry.Cancelled = true;
                LogError("[Thread] Prepare failed: " + ex.Message);
                return false;
            }
        }

        private void PreAttackSetupCallback(PreAttackSetup_ReturnType rd)
        {
            _callbackResult = rd;
            _callbackEvent.Set();
        }

        private void FirePreparedAttack(BombAttackEntry entry)
        {
            try
            {
                CastleMap castleMap = entry.PreparedCastleMap;
                int launchAttackType    = entry.PreparedAttackType;
                int launchPillagePercent = entry.PreparedPillagePercent;

                VillageData targetData = GameEngine.Instance.World.getVillageData(entry.TargetVillageId);
                if (targetData != null && targetData.special != 0)
                {
                    launchAttackType     = 11;
                    launchPillagePercent = 0;
                }

                castleMap.setupLaunchArmy(launchAttackType, launchPillagePercent, 0);
                castleMap.launchArmy(true);

                entry.Sent = true;
                entry.PreparedCastleMap = null;
                entry.Status = "Sent @ " + DateTime.Now.ToString("HH:mm:ss");
                LogInfo("[Thread] Stack " + entry.Stack + " SENT → " + entry.TargetVillageId);

                // Track this sent army for queue advancement timing
                _sentArmies.Add(new SentArmy
                {
                    VillageId = entry.SourceVillageId,
                    SentTime = DateTime.Now,
                    RecalledTime = null,
                    TravelSeconds = entry.TravelTimeSeconds
                });

                // Register with radar immediately so it's tracked before the game array
                // can drop it — prevents flicker-disappear on the world map.
                RadarModule radar = GetRadarModule();
                if (radar != null)
                {
                    var postLaunchArray = GameEngine.Instance.World.getArmyArray();
                    if (postLaunchArray != null)
                    {
                        foreach (WorldMap.LocalArmyData a in postLaunchArray)
                        {
                            if (a != null && !a.dead &&
                                a.homeVillageID == entry.SourceVillageId &&
                                a.targetVillageID == entry.TargetVillageId &&
                                a.lootType < 0)
                            {
                                radar.TrackArmy(a);
                                break;
                            }
                        }
                    }
                }

                StartFakeSendTimerIfNeeded();
            }
            catch (Exception ex)
            {
                entry.Status = "Error: " + ex.Message;
                entry.Cancelled = true;
                entry.PreparedCastleMap = null;
                LogError("[Thread] Fire failed: " + ex.Message);
            }
        }

        // ── Fake send timer ───────────────────────────────────────────────────

        private void StartFakeSendTimerIfNeeded()
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null || !settings.FakeSendEnabled || _fakeSendTriggered) return;
            if (_fakeSendTimer != null) return;
            _fakeSendTimer = new Timer(FakeSendTimerCallback, null,
                TimeSpan.FromMinutes(4), TimeSpan.FromMilliseconds(-1));
            LogInfo("[FakeSend] Recall timer started.");
        }

        private void FakeSendTimerCallback(object state)
        {
            if (_fakeSendTriggered) return;
            _fakeSendTriggered = true;
            AutoBombMultiSettings settings = Settings;
            LogWarning("[FakeSend] 4 minutes elapsed — recalling armies.");
            _cancelLaunchEvent.Set();
            CancelRemainingLocalAttacks();
            RecallAll(settings);
            DisposeFakeSendTimer();
        }

        private void DisposeFakeSendTimer()
        {
            if (_fakeSendTimer != null) { _fakeSendTimer.Dispose(); _fakeSendTimer = null; }
        }

        // ── Card management ───────────────────────────────────────────────────
        //
        // Card definition IDs (from all_cards.csv):
        //   2561 CARDTYPE_BASIC_DISCIPLINE     — x2 army speed, 3-hour duration
        //   2562 CARDTYPE_ADVANCED_DISCIPLINE  — x4 army speed, 3-hour duration
        //   2563 CARDTYPE_EXPERT_DISCIPLINE    — x6 army speed, 3-hour duration
        //   2694 CARDTYPE_LOGISTICS_BASIC      — x2 army speed, 1-use (consumed on send)
        //   2695 CARDTYPE_LOGISTICS_ADVANCED   — x3 army speed, 1-use
        //   2696 CARDTYPE_LOGISTICS_EXPERT     — x5 army speed, 1-use
        // Both families share cardCategory == 6; all have filter == 11 (military).

        /// <summary>
        /// Returns the game card definition ID (from all_cards.csv) for a given card_type value.
        /// card_type: 0=none, 1=x2-disc, 2=x4-disc, 3=x6-disc, 4=x2-log, 5=x3-log, 6=x5-log
        /// </summary>
        private static int GetCardDefId(int cardType)
        {
            switch (cardType)
            {
                case 1: return 2561; // Basic Discipline
                case 2: return 2562; // Advanced Discipline
                case 3: return 2563; // Expert Discipline
                case 4: return 2694; // Basic Logistics
                case 5: return 2695; // Advanced Logistics
                case 6: return 2696; // Expert Logistics
                default: return 0;
            }
        }

        /// <summary>
        /// Scans UserCardData for an active army speed card using exact definition IDs.
        /// Returns false if no discipline or logistics speed card is active.
        /// outInstanceId = value from UserCardData.cards[i] (pass to CancelCard).
        /// outDefId      = card definition ID (e.g. 2561 = Basic Discipline).
        /// </summary>
        private bool GetActiveSpeedCard(out int outInstanceId, out int outDefId)
        {
            outInstanceId = 0;
            outDefId = 0;
            try
            {
                var mgr = GameEngine.Instance != null ? GameEngine.Instance.cardsManager : null;
                if (mgr == null) return false;
                CardData cd = mgr.UserCardData;
                if (cd == null || cd.cards == null) return false;

                for (int i = 0; i < cd.cards.Length; i++)
                {
                    if (cd.cards[i] == 0) continue;
                    CardTypes.CardDefinition def = CardTypes.getCardDefinition(cd.cards[i]);
                    if (def == null) continue;

                    // Only match the six known army speed card definition IDs
                    if (def.id < 2561 || def.id > 2696) continue;
                    if (def.id != 2561 && def.id != 2562 && def.id != 2563 &&
                        def.id != 2694 && def.id != 2695 && def.id != 2696) continue;

                    outInstanceId = cd.cards[i];
                    outDefId = def.id;
                    LogDebug("[Card] Active speed card: defId=" + def.id +
                        " instanceId=" + outInstanceId);
                    return true;
                }
            }
            catch (Exception ex) { LogWarning("[Card] GetActiveSpeedCard: " + ex.Message); }
            return false;
        }

        /// <summary>
        /// Searches ProfileCards for a card matching the desired card_type by exact definition ID.
        /// Returns the user instance ID to pass to PlayUserCard, or 0 if not in inventory.
        /// </summary>
        private int FindInventoryCard(int cardType)
        {
            try
            {
                int targetDefId = GetCardDefId(cardType);
                if (targetDefId == 0) return 0;

                var mgr = GameEngine.Instance != null ? GameEngine.Instance.cardsManager : null;
                if (mgr == null) return 0;

                // Exclude instance IDs already active or already played this batch.
                // Logistics cards are consumed on send — ProfileCards won't reflect this until
                // the client syncs, so _playedCardInstanceIds tracks consumed instances locally.
                CardData cd = mgr.UserCardData;
                var activeIds = new System.Collections.Generic.HashSet<int>();
                if (cd != null && cd.cards != null)
                    foreach (int c in cd.cards)
                        if (c != 0) activeIds.Add(c);

                foreach (var kvp in mgr.ProfileCards)
                {
                    if (kvp.Value == null) continue;
                    if (kvp.Value.id != targetDefId) continue;
                    if (activeIds.Contains(kvp.Key)) continue;
                    if (_playedCardInstanceIds.Contains(kvp.Key)) continue;

                    LogDebug("[Card] FindInventoryCard: cardType=" + cardType +
                        " defId=" + targetDefId + " → instanceId=" + kvp.Key);
                    return kvp.Key;
                }

                LogWarning("[Card] Card not found in inventory: cardType=" + cardType +
                    " (defId=" + targetDefId + ").");
            }
            catch (Exception ex) { LogWarning("[Card] FindInventoryCard: " + ex.Message); }
            return 0;
        }

        /// <summary>
        /// Ensures the correct speed card is active just before an attack send (fire-and-forget).
        /// Called at T-3s per-village in LaunchThreadProc.
        /// Returns true if the state is acceptable (correct card active or no action needed),
        /// false only on an unrecoverable error (wrong card with auto-cancel off, or card not in inventory).
        /// </summary>
        private bool EnsureCorrectCardActive(int cardType, AutoBombMultiSettings settings,
            DateTime currentVillageSendTime)
        {
            try
            {
                int activeInstanceId;
                int activeDefId;
                bool speedCardActive = GetActiveSpeedCard(out activeInstanceId, out activeDefId);

                // ── card_type == 0: no card desired ─────────────────────────────
                if (cardType == 0)
                {
                    if (!speedCardActive) return true;
                    if (settings.AutoCancelWrongCard)
                    {
                        LogInfo("[Card] card_type=0 but speed card active (defId=" + activeDefId +
                            ") — cancelling.");
                        RemoteServices.Instance.CancelCard(activeInstanceId);
                    }
                    else
                    {
                        LogWarning("[Card] card_type=0 but speed card is active — " +
                            "AutoCancelWrongCard is off, leaving it.");
                    }
                    return true;
                }

                // ── card_type > 0: specific card desired ────────────────────────
                int desiredDefId = GetCardDefId(cardType);

                if (speedCardActive)
                {
                    // Check if this instance was already played for a prior village in this
                    // batch. If so, it will be consumed by that village's send before this
                    // village fires — don't treat it as available, play a fresh copy instead.
                    // Do NOT cancel it; it belongs to the prior village.
                    bool claimedByPrior = _playedCardInstanceIds.Contains(activeInstanceId);

                    if (activeDefId == desiredDefId && !claimedByPrior)
                    {
                        LogDebug("[Card] Correct card already active (cardType=" + cardType +
                            " defId=" + desiredDefId + ").");
                        return true;
                    }

                    if (claimedByPrior)
                    {
                        // Same card type but claimed by a prior village. That village will consume
                        // it when it sends. Only one speed card can be active at once, so we must
                        // wait until _cardFreeAt (= prior village's scheduled send time) before
                        // playing a new copy for this village.
                        TimeSpan waitForFree = _cardFreeAt - DateTime.Now;
                        if (waitForFree > TimeSpan.Zero)
                        {
                            LogInfo("[Card] Active card (instanceId=" + activeInstanceId +
                                ") claimed by prior village — waiting " +
                                (int)waitForFree.TotalSeconds + "s for it to be consumed.");
                            if (_cancelLaunchEvent.WaitOne((int)waitForFree.TotalMilliseconds))
                                return false; // cancelled while waiting
                        }
                        LogInfo("[Card] Card now free — playing fresh copy for cardType=" + cardType + ".");
                    }
                    else
                    {
                        // Wrong card type is active
                        if (!settings.AutoCancelWrongCard)
                        {
                            LogError("[Card] Wrong speed card active (defId=" + activeDefId +
                                ") for desired card_type=" + cardType + " (defId=" + desiredDefId +
                                ") — AutoCancelWrongCard is off. Cancelling batch.");
                            return false;
                        }

                        LogInfo("[Card] Wrong speed card active (defId=" + activeDefId +
                            ") — cancelling before playing card_type=" + cardType +
                            " (defId=" + desiredDefId + ").");
                        RemoteServices.Instance.CancelCard(activeInstanceId);
                    }
                }

                // Find and play the desired card from inventory
                int instanceId = FindInventoryCard(cardType);
                if (instanceId == 0)
                {
                    LogError("[Card] Card type " + cardType + " not found in inventory — cancelling batch.");
                    return false;
                }

                // Record before playing so if this is a logistics card it won't be picked
                // again by the next village even before ProfileCards is updated server-side.
                _playedCardInstanceIds.Add(instanceId);

                // Record when this card will be consumed so a subsequent village that needs
                // a card can wait until after this village sends before playing its own.
                _cardFreeAt = currentVillageSendTime;

                LogInfo("[Card] Playing card_type=" + cardType + " (instanceId=" + instanceId + ").");
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(
                    URLs.ProfileProtocol,
                    URLs.ProfileServerAddressCards,
                    URLs.ProfileServerPort,
                    URLs.ProfileCardPath);
                XmlRpcCardsRequest req = new XmlRpcCardsRequest(
                    RemoteServices.Instance.UserGuid.ToString().Replace("-", ""),
                    RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""),
                    instanceId.ToString(),
                    "-1",  // village = -1 → global (not village-specific)
                    RemoteServices.Instance.ProfileWorldID.ToString());
                provider.PlayUserCard(req,
                    delegate(ICardsProvider p, ICardsResponse r) { LogDebug("[Card] PlayUserCard response received."); },
                    (System.Windows.Forms.Control)InterfaceMgr.Instance.getDXBasePanel());

                return true;
            }
            catch (Exception ex)
            {
                LogError("[Card] EnsureCorrectCardActive: " + ex.Message);
                return false;
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        /// <summary>
        /// Returns true if any of this player's sent armies have been in flight for 5 or more minutes
        /// (i.e. past the server's recall window). In that case we let all sent armies proceed —
        /// a partial recall where some armies hit and some return would be worse than a full attack.
        /// </summary>
        // Check if all of this client's sent attacks are still within safe recall window.
        // Returns false if any attack is past the safe cutoff (4:50 = 290s, giving 10s buffer before actual 5-min hard limit).
        private bool CanSafelyRecallAllMyAttacks(double safeRecallSecondsBuffer = 290)
        {
            lock (_attacksLock)
            {
                foreach (var e in _myAttacks)
                {
                    if (!e.Sent || e.ScheduledSendTime == DateTime.MaxValue) continue;
                    double elapsedSeconds = (DateTime.Now - e.ScheduledSendTime).TotalSeconds;
                    if (elapsedSeconds >= safeRecallSecondsBuffer)
                        return false; // At least one attack is too far along to safely recall
                }
            }
            return true; // All sent attacks are still within recall window
        }

        private void RecallAll(AutoBombMultiSettings settings)
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;

                // If any sent army has been out for 4:50+ (290s, giving buffer before hard 5-min limit),
                // it's too risky to recall. Rather than a partial recall (some turn back, some hit), let them all go.
                if (!CanSafelyRecallAllMyAttacks(290))
                {
                    LogWarning("[Recall] One or more armies >= 4:50 out — too risky to recall. " +
                        "Letting all sent armies proceed to target.");
                    return;
                }

                int targetVid = _currentTargetVillageId;
                RemoteServices.Instance.set_CancelCastleAttack_UserCallBack(null);

                HashSet<long> recalled = new HashSet<long>();
                HashSet<int> recalledVillages = new HashSet<int>();
                foreach (WorldMap.LocalArmyData army in GameEngine.Instance.World.getArmyArray())
                {
                    if (army.targetVillageID == targetVid &&
                        GameEngine.Instance.World.isUserVillage(army.travelFromVillageID))
                    {
                        RemoteServices.Instance.CancelCastleAttack(army.armyID);
                        recalled.Add(army.armyID);
                        recalledVillages.Add(army.travelFromVillageID);
                    }
                }

                // Also recall via radar tracked armies — covers armies temporarily absent
                // from the game array due to server reconciliation.
                RadarModule radar = GetRadarModule();
                if (radar != null)
                {
                    foreach (WorldMap.LocalArmyData army in radar.GetTrackedOutboundUserArmies(targetVid))
                    {
                        if (recalled.Add(army.armyID))
                        {
                            RemoteServices.Instance.CancelCastleAttack(army.armyID);
                            recalledVillages.Add(army.travelFromVillageID);
                        }
                    }
                }

                // Record recall time for tracking villages
                DateTime recallTime = DateTime.Now;
                foreach (var sent in _sentArmies)
                {
                    if (recalledVillages.Contains(sent.VillageId) && !sent.RecalledTime.HasValue)
                        sent.RecalledTime = recallTime;
                }

                LogInfo("Recalled " + recalled.Count + " army/armies.");

                // Always acknowledge when we execute a recall, regardless of source
                // (API signal, local interdict detection, or any other trigger)
                // Coordinator needs to know all recall operations happening on all clients
                if (recalled.Count > 0 && settings != null)
                {
                    try
                    {
                        PostAction(settings, "acknowledge_recall", new Dictionary<string, object>
                        {
                            ["player_name"] = GetLocalPlayerName(),
                            ["signal_type"] = "local_recall",
                            ["armies_recalled"] = recalled.Count
                        });
                    }
                    catch (Exception ex)
                    {
                        LogWarning("Failed to acknowledge recall execution: " + ex.Message);
                    }
                }
            }
            catch (Exception ex) { LogError("Recall error: " + ex.Message); }
        }

        private RadarModule GetRadarModule()
        {
            if (Engine == null) return null;
            foreach (IBotModule module in Engine.Modules)
            {
                RadarModule rm = module as RadarModule;
                if (rm != null) return rm;
            }
            return null;
        }

        private void CancelRemainingLocalAttacks()
        {
            lock (_attacksLock)
            {
                foreach (var e in _myAttacks)
                    if (!e.Sent) { e.Cancelled = true; e.Status = "Cancelled"; }
            }
        }

        private void StopLaunchThread()
        {
            _cancelLaunchEvent.Set();
            if (_launchThread != null && _launchThread.IsAlive)
                _launchThread.Join(5000);
            _launchThread = null;
            _launching = false;
            DisposeFakeSendTimer();
        }

        private void PostPlayerReady(AutoBombMultiSettings settings)
        {
            try
            {
                string playerName = GetLocalPlayerName();
                var villages = BuildLocalVillageList(settings);
                PostAction(settings, "player_ready", new Dictionary<string, object>
                {
                    ["player_name"] = playerName,
                    ["villages"]    = villages,
                    // The target these villages' travel times were computed against. Lets the
                    // coordinator confirm a player has recomputed for the current target before
                    // starting a queued bomb's timer.
                    ["travel_target"] = settings.TargetVillageId,
                });
                LogInfo("Registered with API as '" + playerName + "' with " + villages.Count + " village(s).");
            }
            catch (Exception ex) { LogError("PlayerReady failed: " + ex.Message); }
        }

        /// <summary>
        /// Clears the village list cache, forcing a full rebuild on the next heartbeat.
        /// Call when the target village changes or when a village sync has just completed.
        /// </summary>
        public void InvalidateVillageListCache()
        {
            _cachedVillageList = null;
        }

        private List<Dictionary<string, object>> BuildLocalVillageList(AutoBombMultiSettings settings)
        {
            int targetVid = settings.TargetVillageId;

            // Return cached list if the target hasn't changed and it's still fresh.
            // With 200+ vassals each requiring two CalculateBaseTravelTime calls, rebuilding
            // every heartbeat (2 s during launch) is hundreds of world-map lookups per second.
            if (_cachedVillageList != null &&
                _cachedVillageListTargetVid == targetVid &&
                (DateTime.Now - _cachedVillageListTime).TotalSeconds < VillageListCacheSeconds)
            {
                return _cachedVillageList;
            }

            var result = new List<Dictionary<string, object>>();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return result;

            foreach (int vid in GameEngine.Instance.World.getUserVillageIDList())
            {
                string name = GameEngine.Instance.World.getVillageName(vid);
                double travelArmy    = 0;
                double travelCaptain = 0;
                if (targetVid > 0)
                {
                    travelArmy    = AutoBombModule.CalculateBaseTravelTime(vid, targetVid, false);
                    travelCaptain = AutoBombModule.CalculateBaseTravelTime(vid, targetVid, true);
                }

                int p = 0, a = 0, pk = 0, sw = 0, cat = 0, cap = 0;
                AutoBombModule.GetVillageTroops(vid, out p, out a, out pk, out sw, out cat, out cap);

                result.Add(new Dictionary<string, object>
                {
                    ["village_id"]          = vid,
                    ["village_name"]        = name ?? "",
                    ["peasants"]            = p,
                    ["archers"]             = a,
                    ["pikemen"]             = pk,
                    ["swordsmen"]           = sw,
                    ["catapults"]           = cat,
                    ["captains"]            = cap,
                    ["travel_time_army"]    = travelArmy,
                    ["travel_time_captain"] = travelCaptain,
                    ["is_vassal"]           = false,
                    ["parent_village_id"]   = 0,
                });
            }

            if (settings.IncludeVassals && GameEngine.Instance.vassalsManager != null)
            {
                var vassals = GameEngine.Instance.vassalsManager.GetVassals();
                if (vassals != null)
                {
                    foreach (var vi in vassals)
                    {
                        int vid = vi.villageID;
                        string name = GameEngine.Instance.World.getVillageName(vid);
                        double travelArmy    = 0;
                        double travelCaptain = 0;
                        if (targetVid > 0)
                        {
                            travelArmy    = AutoBombModule.CalculateBaseTravelTime(vid, targetVid, false);
                            travelCaptain = AutoBombModule.CalculateBaseTravelTime(vid, targetVid, true);
                        }
                        result.Add(new Dictionary<string, object>
                        {
                            ["village_id"]          = vid,
                            ["village_name"]        = (name ?? "") + " (V)",
                            ["peasants"]            = vi.stationed_Peasants,
                            ["archers"]             = vi.stationed_Archers,
                            ["pikemen"]             = vi.stationed_Pikemen,
                            ["swordsmen"]           = vi.stationed_Swordsmen,
                            ["catapults"]           = vi.stationed_Catapults,
                            ["captains"]            = 0,
                            ["travel_time_army"]    = travelArmy,
                            ["travel_time_captain"] = travelCaptain,
                            ["is_vassal"]           = true,
                            ["parent_village_id"]   = vi.yourVillageID,
                        });
                    }
                }
            }

            _cachedVillageList            = result;
            _cachedVillageListTargetVid   = targetVid;
            _cachedVillageListTime        = DateTime.Now;
            LogDebug("Village list rebuilt: " + result.Count + " entries (own + vassals).");
            return result;
        }

        /// <summary>
        /// Validates that a target village still exists and is owned by the expected player.
        /// Auto-updates the village name if it changed.
        /// Returns true if valid, false if it should be removed from the queue.
        /// </summary>
        public bool ValidateTargetVillage(int villageId, string expectedOwner, out string villageNameOut)
        {
            villageNameOut = "";
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                    return false;

                VillageData village = GameEngine.Instance.World.getVillageData(villageId);
                if (village == null)
                {
                    LogWarning("[Queue] Target " + villageId + " — REMOVED (village no longer exists)");
                    return false;
                }

                // Check if owned by expected player
                if (village.ownerID <= 0 || village.ownerName != expectedOwner)
                {
                    LogWarning("[Queue] Target " + villageId + " (" + village.Name + ") — REMOVED (captured by " +
                        (string.IsNullOrEmpty(village.ownerName) ? "barbarians" : village.ownerName) + ")");
                    return false;
                }

                villageNameOut = village.Name;
                return true;
            }
            catch (Exception ex)
            {
                LogWarning("[Queue] Error validating target " + villageId + ": " + ex.Message);
                return false;
            }
        }

        private bool PostAttackEvent(AutoBombMultiSettings settings, string action, int sourceVillageId)
        {
            return PostAttackEvent(settings, action, sourceVillageId, "");
        }

        private bool PostAttackEvent(AutoBombMultiSettings settings, string action, int sourceVillageId, string reason)
        {
            try
            {
                var payload = new Dictionary<string, object>
                {
                    ["player_name"]      = GetLocalPlayerName(),
                    ["source_village_id"] = sourceVillageId,
                };
                if (!string.IsNullOrEmpty(reason)) payload["reason"] = reason;
                PostAction(settings, action, payload);
                return true;
            }
            catch (Exception ex)
            {
                LogError("PostAttackEvent '" + action + "' failed: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Parses the API's scheduled_send_times into a (villageId → ISO time) map containing
        /// only the entries for the given player. The API keys these by "player|villageId" so a
        /// village sent by two different players (player attack + vassal attack) gets distinct
        /// send times. Falls back to a bare-villageId key for backward compatibility.
        /// Within a single player a village ID is unique, so a vid-keyed result is unambiguous.
        /// </summary>
        private Dictionary<int, string> ParseSendTimesForPlayer(object sendTimesObj, string playerName)
        {
            var result = new Dictionary<int, string>();
            var st = sendTimesObj as Dictionary<string, object>;
            if (st == null) return result;
            foreach (var kv in st)
            {
                int sep = kv.Key.IndexOf('|');
                if (sep <= 0)
                {
                    // Old format: keyed by village ID alone
                    int vidOld;
                    if (int.TryParse(kv.Key, out vidOld)) result[vidOld] = kv.Value.ToString();
                    continue;
                }
                if (kv.Key.Substring(0, sep) != playerName) continue;
                int vid;
                if (int.TryParse(kv.Key.Substring(sep + 1), out vid))
                    result[vid] = kv.Value.ToString();
            }
            return result;
        }

        // ── API call ──────────────────────────────────────────────────────────

        public Dictionary<string, object> PostAction(AutoBombMultiSettings settings, string action,
            Dictionary<string, object> payload)
        {
            payload["action"]      = action;
            payload["session_key"] = settings.SessionKey ?? "";

            string json = SimpleJson.Serialize(payload);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072; // TLS 1.2
            var request = (HttpWebRequest)WebRequest.Create(settings.ApiUrl);
            request.Method      = "POST";
            request.ContentType = "application/json";
            request.ContentLength = bytes.Length;
            request.Timeout     = 30000; // 30 seconds (poll can be slow with network latency or slow hosting)

            using (Stream s = request.GetRequestStream())
                s.Write(bytes, 0, bytes.Length);

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string responseJson = reader.ReadToEnd();
                int pos = 0;
                return SimpleJson.DeserializeObject(responseJson, ref pos) as Dictionary<string, object>;
            }
        }

        // ── Utility ───────────────────────────────────────────────────────────

        public static string GetLocalPlayerName()
        {
            try
            {
                string steamName = Program.GetLocalUserName();
                if (!string.IsNullOrEmpty(steamName)) return steamName;

                // Steam name unavailable — resolve from game world data
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                {
                    List<int> vids = GameEngine.Instance.World.getUserVillageIDList();
                    if (vids != null && vids.Count > 0)
                    {
                        int userId = GameEngine.Instance.World.getVillageUserID(vids[0]);
                        if (userId >= 0)
                        {
                            WorldMap.CachedUserInfo info = GameEngine.Instance.World.getStoredUserInfo(userId);
                            if (info != null && !string.IsNullOrEmpty(info.userName))
                                return info.userName;
                        }
                    }
                }
            }
            catch { }
            return "";
        }

        private static string GetStr(Dictionary<string, object> d, string key)
        {
            return GetStr(d, key, "");
        }
        private static string GetStr(Dictionary<string, object> d, string key, string def)
        {
            object v; return d.TryGetValue(key, out v) && v != null ? v.ToString() : def;
        }
        private static int GetInt(Dictionary<string, object> d, string key)
        {
            return GetInt(d, key, 0);
        }
        private static int GetInt(Dictionary<string, object> d, string key, int def)
        {
            object v;
            if (!d.TryGetValue(key, out v) || v == null) return def;
            if (v is long) return (int)(long)v;
            if (v is int) return (int)v;
            int r; return int.TryParse(v.ToString(), out r) ? r : def;
        }
        private static double GetDouble(Dictionary<string, object> d, string key)
        {
            return GetDouble(d, key, 0.0);
        }
        private static double GetDouble(Dictionary<string, object> d, string key, double def)
        {
            object v;
            if (!d.TryGetValue(key, out v) || v == null) return def;
            if (v is double) return (double)v;
            if (v is long) return (double)(long)v;
            double r; return double.TryParse(v.ToString(),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out r) ? r : def;
        }
        private static bool GetBool(Dictionary<string, object> d, string key)
        {
            return GetBool(d, key, false);
        }
        private static bool GetBool(Dictionary<string, object> d, string key, bool def)
        {
            object v;
            if (!d.TryGetValue(key, out v) || v == null) return def;
            if (v is bool) return (bool)v;
            bool r; return bool.TryParse(v.ToString(), out r) ? r : def;
        }

        // Simple JSON serializer/deserializer — no external dependencies, .NET 3.5 compatible
        private static class SimpleJson
        {
            public static object DeserializeObject(string json, ref int pos)
            {
                SkipWhitespace(json, ref pos);
                if (pos >= json.Length) return null;
                char c = json[pos];
                if (c == '{') return ParseObject(json, ref pos);
                if (c == '[') return ParseArray(json, ref pos);
                if (c == '"') return ParseString(json, ref pos);
                if (c == 't') { pos += 4; return true; }
                if (c == 'f') { pos += 5; return false; }
                if (c == 'n') { pos += 4; return null; }
                return ParseNumber(json, ref pos);
            }

            private static Dictionary<string, object> ParseObject(string json, ref int pos)
            {
                var dict = new Dictionary<string, object>();
                pos++; // skip '{'
                SkipWhitespace(json, ref pos);
                while (pos < json.Length && json[pos] != '}')
                {
                    SkipWhitespace(json, ref pos);
                    string key = ParseString(json, ref pos);
                    SkipWhitespace(json, ref pos);
                    if (pos < json.Length && json[pos] == ':') pos++;
                    SkipWhitespace(json, ref pos);
                    object val = DeserializeObject(json, ref pos);
                    dict[key] = val;
                    SkipWhitespace(json, ref pos);
                    if (pos < json.Length && json[pos] == ',') pos++;
                    SkipWhitespace(json, ref pos);
                }
                if (pos < json.Length) pos++; // skip '}'
                return dict;
            }

            private static System.Collections.ArrayList ParseArray(string json, ref int pos)
            {
                var list = new System.Collections.ArrayList();
                pos++; // skip '['
                SkipWhitespace(json, ref pos);
                while (pos < json.Length && json[pos] != ']')
                {
                    SkipWhitespace(json, ref pos);
                    list.Add(DeserializeObject(json, ref pos));
                    SkipWhitespace(json, ref pos);
                    if (pos < json.Length && json[pos] == ',') pos++;
                    SkipWhitespace(json, ref pos);
                }
                if (pos < json.Length) pos++; // skip ']'
                return list;
            }

            private static string ParseString(string json, ref int pos)
            {
                pos++; // skip opening '"'
                var sb = new StringBuilder();
                while (pos < json.Length)
                {
                    char c = json[pos++];
                    if (c == '"') break;
                    if (c == '\\' && pos < json.Length)
                    {
                        char esc = json[pos++];
                        if (esc == 'n') sb.Append('\n');
                        else if (esc == 'r') sb.Append('\r');
                        else if (esc == 't') sb.Append('\t');
                        else if (esc == 'u' && pos + 3 < json.Length)
                        {
                            string hex = json.Substring(pos, 4);
                            pos += 4;
                            int codePoint;
                            if (int.TryParse(hex, System.Globalization.NumberStyles.HexNumber,
                                System.Globalization.CultureInfo.InvariantCulture, out codePoint))
                                sb.Append((char)codePoint);
                            else
                                sb.Append('u').Append(hex);
                        }
                        else sb.Append(esc);
                    }
                    else sb.Append(c);
                }
                return sb.ToString();
            }

            private static object ParseNumber(string json, ref int pos)
            {
                int start = pos;
                while (pos < json.Length)
                {
                    char c = json[pos];
                    if (c == ',' || c == '}' || c == ']' || c == ' ' || c == '\r' || c == '\n') break;
                    pos++;
                }
                string s = json.Substring(start, pos - start);
                long l; double d;
                if (long.TryParse(s, out l)) return l;
                if (double.TryParse(s, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out d)) return d;
                return s;
            }

            private static void SkipWhitespace(string json, ref int pos)
            {
                while (pos < json.Length && (json[pos] == ' ' || json[pos] == '\t' ||
                    json[pos] == '\r' || json[pos] == '\n')) pos++;
            }

            public static string Serialize(object obj)
            {
                var sb = new StringBuilder();
                SerializeValue(obj, sb);
                return sb.ToString();
            }
            private static void SerializeValue(object val, StringBuilder sb)
            {
                if (val == null)       { sb.Append("null"); return; }
                if (val is bool)       { sb.Append((bool)val ? "true" : "false"); return; }
                if (val is string)
                {
                    sb.Append('"');
                    foreach (char ch in (string)val)
                    {
                        if (ch == '\\') sb.Append("\\\\");
                        else if (ch == '"') sb.Append("\\\"");
                        else if (ch == '\n') sb.Append("\\n");
                        else if (ch == '\r') sb.Append("\\r");
                        else if (ch == '\t') sb.Append("\\t");
                        else if (ch > 127) sb.AppendFormat("\\u{0:x4}", (int)ch);
                        else sb.Append(ch);
                    }
                    sb.Append('"');
                    return;
                }
                if (val is int || val is long || val is float || val is double)
                    { sb.Append(System.Convert.ToString(val, System.Globalization.CultureInfo.InvariantCulture)); return; }
                if (val is Dictionary<string, object>)
                {
                    var d = (Dictionary<string, object>)val;
                    sb.Append('{');
                    bool first = true;
                    foreach (var kv in d) { if (!first) sb.Append(','); sb.Append('"'); sb.Append(kv.Key); sb.Append("\":"); SerializeValue(kv.Value, sb); first = false; }
                    sb.Append('}');
                    return;
                }
                if (val is System.Collections.IList)
                {
                    var list = (System.Collections.IList)val;
                    sb.Append('[');
                    for (int i = 0; i < list.Count; i++) { if (i > 0) sb.Append(','); SerializeValue(list[i], sb); }
                    sb.Append(']');
                    return;
                }
                sb.Append('"'); sb.Append(val.ToString()); sb.Append('"');
            }
        }
    }

    // ── Data transfer type used by AutoBombMultiTab ───────────────────────────
    public class MultiAttackConfigEntry
    {
        public string SourcePlayerName = "";
        public int SourceVillageId;
        public int ParentVillageId;
        public bool IsVassal;
        public string FormationName = "";
        public int Stack = 1;
        public int CardType;
        public bool CaptainsOnly;
        public int AttackType = 11;
        public double TravelTimeSeconds;
        public bool Selected = true;
    }
}
