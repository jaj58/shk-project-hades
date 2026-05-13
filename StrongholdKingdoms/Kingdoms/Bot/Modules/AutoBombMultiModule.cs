using CommonTypes;
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
                    LogWarning("Interdict detected on target! Cancelling all attacks.");
                    _cancelLaunchEvent.Set();
                    CancelRemainingLocalAttacks();
                    RecallAll(settings);
                    // Notify API so other players also cancel
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

                PostAction(settings, "set_attack_config", new Dictionary<string, object>
                {
                    ["player_name"]       = GetLocalPlayerName(),
                    ["target_village_id"] = targetVillageId,
                    ["attacks"]           = attackList,
                });
                LogInfo("Attack config pushed to API for target " + targetVillageId + ".");
            }
            catch (Exception ex) { LogError("PushAttackConfig failed: " + ex.Message); }
        }

        public void StartTimer()
        {
            AutoBombMultiSettings settings = Settings;
            if (settings == null || !settings.IsCoordinator) return;
            try
            {
                PostAction(settings, "start_timer", new Dictionary<string, object>
                {
                    ["player_name"]            = GetLocalPlayerName(),
                    ["stack_delay_seconds"]    = settings.StackDelaySeconds,
                    ["fake_send"]              = settings.FakeSendEnabled,
                    ["auto_cancel_on_interdict"] = settings.AutoCancelOnInterdict,
                });
                LogInfo("Timer started — all clients will receive launch times on next poll.");
            }
            catch (Exception ex) { LogError("StartTimer failed: " + ex.Message); }
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
            try
            {
                PostAction(settings, "cancel_attacks", new Dictionary<string, object>
                    { ["player_name"] = GetLocalPlayerName() });
            }
            catch (Exception ex) { LogError("CancelAll API call failed: " + ex.Message); }
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
            settings.InterdictDetected = GetBool(stateData, "interdict_detected");

            object coordObj;
            stateData.TryGetValue("coordinator", out coordObj);
            string coordinator = coordObj != null ? coordObj.ToString() : "";
            settings.IsCoordinator = (coordinator == GetLocalPlayerName());

            // ── Target village ────────────────────────────────────────────────
            settings.TargetVillageId = GetInt(stateData, "target_village_id");

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
            Dictionary<int, string> sendTimes = new Dictionary<int, string>();

            object sendTimesObj;
            if (stateData.TryGetValue("scheduled_send_times", out sendTimesObj) && sendTimesObj != null)
            {
                var st = sendTimesObj as Dictionary<string, object>;
                if (st != null)
                    foreach (var kv in st)
                    {
                        int vid;
                        if (int.TryParse(kv.Key, out vid)) sendTimes[vid] = kv.Value.ToString();
                    }
            }

            LogInfo("Launching: " + sendTimes.Count + " scheduled send time(s) received from API.");
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
                        var st2 = stObj2 as Dictionary<string, object>;
                        if (st2 != null)
                        {
                            foreach (var e in myEntries)
                            {
                                if (e.ScheduledSendTime != DateTime.MaxValue) continue;
                                object sv; if (!st2.TryGetValue(e.SourceVillageId.ToString(), out sv) || sv == null) continue;
                                DateTime utc2;
                                if (DateTime.TryParse(sv.ToString(), null,
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
            if (settings.InterdictDetected || settings.FakeSendEnabled)
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
                                if (!EnsureCorrectCardActive(entry.CardType, settings))
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
                    bool autoRecalled = _interdictDetected || _fakeSendTriggered || _prepareErrorCancel;
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
                        if (!WaitForArmiesReturnMulti(settings, attacks)) break;
                    }
                    else
                    {
                        _cancelLaunchEvent.Reset();
                        DisposeFakeSendTimer();
                    }

                    // Advance to the next queue entry
                    var nextAttacks = TryAdvanceMultiQueue(settings);
                    if (nextAttacks == null || nextAttacks.Count == 0) break;

                    attacks = nextAttacks;
                    lock (_attacksLock) { _myAttacks = nextAttacks; }
                    _interdictDetected = false;
                    _fakeSendTriggered = false;
                    _prepareErrorCancel = false;
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

        // Returns true when all players with tracked attacks have reported armies home.
        // The coordinator both monitors its own armies (and reports) AND polls the API for
        // all players. Non-coordinators report independently via DoPoll.
        private bool WaitForArmiesReturnMulti(AutoBombMultiSettings settings,
            List<BombAttackEntry> sentAttacks)
        {
            // Build the set of source villages this player sent from
            var mySourceVids = new HashSet<int>();
            foreach (var e in sentAttacks)
                if (e.Sent) mySourceVids.Add(e.SourceVillageId);

            // If we sent nothing, report immediately so we don't block the coordinator
            bool ownReported = mySourceVids.Count == 0;
            if (ownReported)
            {
                try { PostAction(settings, "report_armies_status", new Dictionary<string, object>
                    { ["player_name"] = GetLocalPlayerName(), ["status"] = "returned" }); }
                catch { }
            }

            // Time floor: armies cannot be home before they've traveled back the distance they covered.
            // ScheduledSendTime ≈ actual send time. Time in flight at recall = Now - sendTime.
            // After recall the army needs the same duration to return.
            DateTime floorReturnTime = DateTime.MinValue;
            foreach (var e in sentAttacks)
            {
                if (!e.Sent || e.ScheduledSendTime == DateTime.MaxValue) continue;
                TimeSpan inFlight = DateTime.Now - e.ScheduledSendTime;
                if (inFlight < TimeSpan.Zero) inFlight = TimeSpan.Zero;
                DateTime estReturn = DateTime.Now.Add(inFlight).AddSeconds(15); // 15s margin
                if (estReturn > floorReturnTime) floorReturnTime = estReturn;
            }
            if (floorReturnTime == DateTime.MinValue) floorReturnTime = DateTime.Now;
            LogInfo("[Queue] Army return floor: " + floorReturnTime.ToString("HH:mm:ss"));

            LogInfo("[Queue] Waiting for all players' armies to return home...");

            int pollCount = 0;
            int consecutiveClean = 0;
            const int RequiredCleanPolls = 2;

            while (true)
            {
                if (_cancelLaunchEvent.WaitOne(5000))
                {
                    LogInfo("[Queue] Cancelled while waiting for armies.");
                    return false;
                }
                pollCount++;

                // ── Check and report own armies ────────────────────────────────
                if (!ownReported)
                {
                    if (DateTime.Now >= floorReturnTime && CheckLocalArmiesHome(mySourceVids))
                    {
                        if (++consecutiveClean >= RequiredCleanPolls)
                        {
                            ownReported = true;
                            LogInfo("[Queue] Coordinator's armies home — reporting.");
                            try { PostAction(settings, "report_armies_status",
                                new Dictionary<string, object>
                                {
                                    ["player_name"] = GetLocalPlayerName(),
                                    ["status"]      = "returned",
                                }); }
                            catch (Exception ex)
                            {
                                LogError("[Queue] report_armies_status failed: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        consecutiveClean = 0;
                        // Retry recall on any still-outbound armies (interdict/fake-send path)
                        try
                        {
                            var outbound = new List<WorldMap.LocalArmyData>();
                            if (GameEngine.Instance?.World != null)
                            {
                                foreach (WorldMap.LocalArmyData army in
                                    GameEngine.Instance.World.getArmyArray())
                                {
                                    if (army == null || army.dead) continue;
                                    if (!mySourceVids.Contains(army.homeVillageID)) continue;
                                    if (army.lootType < 0 && army.targetVillageID >= 0 &&
                                        army.targetVillageID != army.homeVillageID)
                                        outbound.Add(army);
                                }
                            }
                            if (outbound.Count > 0)
                            {
                                RemoteServices.Instance.set_CancelCastleAttack_UserCallBack(null);
                                foreach (var army in outbound)
                                    RemoteServices.Instance.CancelCastleAttack(army.armyID);
                            }
                        }
                        catch { }
                    }
                }

                // ── Poll API for all-players status ────────────────────────────
                try
                {
                    var resp = PostAction(settings, "get_status", new Dictionary<string, object>
                        { ["player_name"] = GetLocalPlayerName() });
                    if (resp == null) continue;

                    object stateDataObj;
                    resp.TryGetValue("state_data", out stateDataObj);
                    var stateData = stateDataObj as Dictionary<string, object>;
                    if (stateData == null) continue;

                    object armyStatusObj;
                    stateData.TryGetValue("armies_return_status", out armyStatusObj);
                    var armyStatus = armyStatusObj as Dictionary<string, object>;

                    if (armyStatus == null || armyStatus.Count == 0)
                    {
                        LogInfo("[Queue] No army-return tracking in state — proceeding.");
                        return true;
                    }

                    int waiting = 0;
                    foreach (var kv in armyStatus)
                        if (kv.Value.ToString() != "returned") waiting++;

                    if (waiting == 0)
                    {
                        LogInfo("[Queue] All players' armies returned. Advancing queue.");
                        return true;
                    }

                    if (pollCount % 4 == 0)
                        LogInfo("[Queue] " + waiting + " player(s) still waiting for armies...");
                }
                catch (Exception ex) { LogError("[Queue] WaitForArmiesReturnMulti poll error: " + ex.Message); }
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
                    var seenFb = new HashSet<int>();
                    foreach (var item0 in aal0)
                    {
                        var ad0 = item0 as Dictionary<string, object>;
                        if (ad0 == null) continue;
                        int v0 = GetInt(ad0, "source_village_id");
                        if (v0 <= 0 || !seenFb.Add(v0)) continue;
                        sourceAttacks.Add(ad0);
                    }
                }

                // Rebuild attack list with recalculated travel times for the new target.
                // Card speed must be applied so the API schedules arrival at the correct time.
                // Deduplicate village IDs as a safety net even when using the cache.
                var newAttackList = new List<Dictionary<string, object>>();
                var seenVids = new HashSet<int>();
                foreach (var ad in sourceAttacks)
                {
                    int srcVid = GetInt(ad, "source_village_id");
                    if (srcVid <= 0 || !seenVids.Add(srcVid))
                    {
                        if (srcVid > 0) LogWarning("[Queue] Skipping duplicate source_village_id " + srcVid + ".");
                        continue;
                    }
                    bool captainsOnly = GetBool(ad, "captains_only");
                    int cardType = GetInt(ad, "card_type");
                    double newTravel = AutoBombModule.ApplyCardSpeed(
                        AutoBombModule.CalculateBaseTravelTime(srcVid, newTargetVid, captainsOnly),
                        cardType);
                    newAttackList.Add(new Dictionary<string, object>
                    {
                        ["source_player"]       = GetStr(ad, "source_player", ""),
                        ["source_village_id"]   = srcVid,
                        ["formation"]           = GetStr(ad, "formation", ""),
                        ["stack"]               = GetInt(ad, "stack", 1),
                        ["card_type"]           = cardType,
                        ["captains_only"]       = captainsOnly,
                        ["attack_type"]         = GetInt(ad, "attack_type", 11),
                        ["travel_time_seconds"] = newTravel,
                        ["selected"]            = GetBool(ad, "selected", true),
                    });
                }

                // Update cache so the next queue advance also has correct data
                _cachedAttackConfig = new List<Dictionary<string, object>>(newAttackList);

                // Log recalculated travel times for verification
                foreach (var atk in newAttackList)
                    LogInfo("[Queue] Travel recalc: village=" + GetInt(atk, "source_village_id") +
                        " travel=" + (int)GetDouble(atk, "travel_time_seconds") +
                        "s card=" + GetInt(atk, "card_type"));

                // Push updated config to API
                PostAction(settings, "set_attack_config", new Dictionary<string, object>
                {
                    ["player_name"]       = GetLocalPlayerName(),
                    ["target_village_id"] = newTargetVid,
                    ["attacks"]           = newAttackList,
                });
                LogInfo("[Queue] Config pushed for target " + newTargetVid + ".");

                // Give non-coordinator clients time to poll and see the new config
                // before the timer fires. Poll interval is 2000ms so 6s = ~3 polls.
                if (_cancelLaunchEvent.WaitOne(6000)) return null;

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

                // Parse scheduled send times
                var sendTimes = new Dictionary<int, string>();
                object sendTimesObj;
                if (stateData.TryGetValue("scheduled_send_times", out sendTimesObj) && sendTimesObj != null)
                {
                    var st = sendTimesObj as Dictionary<string, object>;
                    if (st != null)
                        foreach (var kv in st)
                        {
                            int vid; if (int.TryParse(kv.Key, out vid)) sendTimes[vid] = kv.Value.ToString();
                        }
                }
                LogInfo("[Queue] " + sendTimes.Count + " send time(s) received for target " + newTargetVid + ".");
                foreach (var kv in sendTimes)
                    LogInfo("[Queue]   Village " + kv.Key + " → send at " + kv.Value);

                // Build this player's new BombAttackEntry list
                string myName = GetLocalPlayerName();
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

                    sendTimes = new Dictionary<int, string>();
                    object stObj2; stateData.TryGetValue("scheduled_send_times", out stObj2);
                    var st2 = stObj2 as Dictionary<string, object>;
                    if (st2 != null)
                        foreach (var kv2 in st2)
                        { int v2; if (int.TryParse(kv2.Key, out v2)) sendTimes[v2] = kv2.Value.ToString(); }

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

                LogInfo("[Prepare] Village " + entry.SourceVillageId + ": dispatching PreAttackSetup (target=" + entry.TargetVillageId + ", formation='" + entry.FormationName + "')");

                RemoteServices.Instance.set_PreAttackSetup_UserCallBack(
                    new RemoteServices.PreAttackSetup_UserCallBack(PreAttackSetupCallback));
                RemoteServices.Instance.PreAttackSetup(
                    entry.SourceVillageId, entry.SourceVillageId, entry.TargetVillageId,
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
                if (rd.vacationVillage)    { entry.Status = "Target on vacation"; entry.Cancelled = true; return false; }
                if (rd.peaceVillage)       { entry.Status = "Target in peace";    entry.Cancelled = true; return false; }
                if (rd.peaceAttacker)      { entry.Status = "Attacker in peace";  entry.Cancelled = true; return false; }
                if (rd.protectedAttacker)  { entry.Status = "Attacker interdicted"; entry.Cancelled = true; return false; }
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

                bool placed = false;

                CastleMapPreset cloudPreset = AutoBombModule.FindCloudPreset(entry.FormationName);
                if (cloudPreset != null)
                {
                    int[] presetCounts = AutoBombModule.PresetTroopsCount(cloudPreset);
                    if (castleMap.HasEnoughTroopsToPlace(ref presetCounts))
                    {
                        castleMap.RestoreAttackPresetBG(cloudPreset);
                        placed = true;
                    }
                    else
                    {
                        entry.Status = "Error: Not enough troops";
                        entry.Cancelled = true;
                        return false;
                    }
                }

                if (!placed)
                {
                    List<CastleMap.RestoreCastleElement> attackSetup = castleMap.getAttackSetup(entry.FormationName);
                    if (attackSetup == null || attackSetup.Count == 0)
                    {
                        entry.Status = "Error: Formation not found";
                        entry.Cancelled = true;
                        return false;
                    }
                    int[] localCounts = AutoBombModule.GetTroopsCountArray12(attackSetup);
                    if (castleMap.HasEnoughTroopsToPlace(ref localCounts))
                    {
                        castleMap.RestoreAttackSetupBG(attackSetup);
                        placed = true;
                    }
                    else
                    {
                        entry.Status = "Error: Not enough troops";
                        entry.Cancelled = true;
                        return false;
                    }
                }

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

        /// <summary>
        /// Returns the desired army travel-time multiplier for a given card_type value.
        /// card_type: 0=none, 1=x2-disc(3h), 2=x4-disc(3h), 3=x6-disc(3h),
        ///            4=x2-log(1use), 5=x3-log(1use), 6=x5-log(1use)
        /// </summary>
        private static double GetDesiredCardMultiplier(int cardType)
        {
            switch (cardType)
            {
                case 1: case 4: return 0.5;
                case 2:         return 0.25;
                case 3:         return 1.0 / 6.0;
                case 5:         return 1.0 / 3.0;
                case 6:         return 0.2;
                default:        return 1.0;
            }
        }

        /// <summary>
        /// Returns true if cardType represents a one-use logistics card (values 4, 5, 6).
        /// </summary>
        private static bool IsLogisticsCardType(int cardType)
        {
            return cardType >= 4 && cardType <= 6;
        }

        /// <summary>
        /// Scans UserCardData for an active army speed card.
        /// Army speed cards are identified by category 6 or 7 (per CardBarGDI section 9 grouping).
        /// Discipline vs logistics is determined via the cardsExpiry heuristic:
        ///   expiry > 60 min from now → discipline (3-hour card); else → logistics.
        /// Returns false if no speed card is active.
        /// </summary>
        private bool GetActiveSpeedCard(out int outInstanceId, out double outMultiplier, out bool outIsLogistics)
        {
            outInstanceId = 0;
            outMultiplier = 1.0;
            outIsLogistics = false;
            try
            {
                var mgr = GameEngine.Instance != null ? GameEngine.Instance.cardsManager : null;
                if (mgr == null) return false;
                CardData cd = mgr.UserCardData;
                if (cd == null || cd.cards == null) return false;

                double overallSpeed = CardTypes.getArmySpeed(cd);
                if (Math.Abs(overallSpeed - 1.0) < 0.001)
                    return false; // no speed bonus active

                for (int i = 0; i < cd.cards.Length; i++)
                {
                    if (cd.cards[i] == 0) continue;
                    CardTypes.CardDefinition def = CardTypes.getCardDefinition(cd.cards[i]);
                    if (def == null) continue;
                    if (def.category != 6 && def.category != 7) continue;

                    outInstanceId = cd.cards[i];
                    outMultiplier = overallSpeed;

                    // Discipline: ~3h duration → expiry well over an hour from now.
                    // Logistics:  consumed on send → usually short or no meaningful expiry window.
                    if (cd.cardsExpiry != null && i < cd.cardsExpiry.Length)
                    {
                        double minsRemaining = (cd.cardsExpiry[i] - DateTime.Now).TotalMinutes;
                        outIsLogistics = minsRemaining < 60;
                        LogDebug("[Card] Active speed card: instanceId=" + outInstanceId +
                            " category=" + def.category +
                            " mult=" + overallSpeed.ToString("F3") +
                            " expiryMins=" + (int)minsRemaining +
                            " isLogistics=" + outIsLogistics);
                    }
                    else
                    {
                        // No expiry info — fall back to category guess (7 = logistics tentative)
                        outIsLogistics = (def.category == 7);
                        LogDebug("[Card] Active speed card: instanceId=" + outInstanceId +
                            " category=" + def.category + " (no expiry data, isLogistics=" + outIsLogistics + ")");
                    }
                    return true;
                }

                // Speed bonus detected but no category-6/7 card found — log for diagnosis
                LogWarning("[Card] Army speed active (mult=" + overallSpeed.ToString("F3") +
                    ") but no category-6/7 card found. Active card categories:");
                for (int i = 0; i < cd.cards.Length; i++)
                {
                    if (cd.cards[i] == 0) continue;
                    CardTypes.CardDefinition d = CardTypes.getCardDefinition(cd.cards[i]);
                    if (d != null)
                        LogWarning("[Card]   cards[" + i + "] instanceId=" + cd.cards[i] + " category=" + d.category);
                }
            }
            catch (Exception ex) { LogWarning("[Card] GetActiveSpeedCard: " + ex.Message); }
            return false;
        }

        /// <summary>
        /// Searches ProfileCards for a card matching the desired card_type.
        /// Within each speed-category group, cards are sorted ascending by definition ID
        /// (assumed ordering: basic < advanced < expert).
        /// Returns the user instance ID to pass to PlayUserCard, or 0 if not found.
        /// </summary>
        private int FindInventoryCard(int cardType)
        {
            try
            {
                var mgr = GameEngine.Instance != null ? GameEngine.Instance.cardsManager : null;
                if (mgr == null) return 0;

                bool wantLogistics = IsLogisticsCardType(cardType);
                // Tier index within the category: x2→0, x4/x3→1, x6/x5→2
                int tierIndex = (cardType <= 3 ? cardType : cardType - 3) - 1;

                // Exclude instance IDs already active so we don't double-play
                CardData cd = mgr.UserCardData;
                var activeIds = new System.Collections.Generic.HashSet<int>();
                if (cd != null && cd.cards != null)
                    foreach (int c in cd.cards)
                        if (c != 0) activeIds.Add(c);

                // Collect ProfileCards with the target category (6=discipline, 7=logistics — tentative)
                int targetCategory = wantLogistics ? 7 : 6;
                var candidates = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, CardTypes.CardDefinition>>();
                foreach (var kvp in mgr.ProfileCards)
                {
                    if (kvp.Value == null) continue;
                    if (kvp.Value.category != targetCategory) continue;
                    if (activeIds.Contains(kvp.Key)) continue; // already active
                    candidates.Add(kvp);
                }
                // Sort by definition ID ascending (basic < advanced < expert within category)
                candidates.Sort((a, b) => a.Value.id.CompareTo(b.Value.id));

                LogDebug("[Card] FindInventoryCard: cardType=" + cardType +
                    " wantLogistics=" + wantLogistics + " tierIndex=" + tierIndex +
                    " candidates=" + candidates.Count);
                for (int i = 0; i < candidates.Count; i++)
                    LogDebug("[Card]   [" + i + "] instanceId=" + candidates[i].Key +
                        " defId=" + candidates[i].Value.id + " category=" + candidates[i].Value.category);

                if (tierIndex >= 0 && tierIndex < candidates.Count)
                    return candidates[tierIndex].Key;

                LogWarning("[Card] No card found for type " + cardType +
                    " (wantLogistics=" + wantLogistics + ", tierIndex=" + tierIndex +
                    ", candidates=" + candidates.Count + ").");
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
        private bool EnsureCorrectCardActive(int cardType, AutoBombMultiSettings settings)
        {
            try
            {
                int activeId;
                double activeMult;
                bool activeIsLogistics;
                bool speedCardActive = GetActiveSpeedCard(out activeId, out activeMult, out activeIsLogistics);

                // ── card_type == 0: no card desired ─────────────────────────────
                if (cardType == 0)
                {
                    if (!speedCardActive) return true;
                    if (settings.AutoCancelWrongCard)
                    {
                        LogInfo("[Card] card_type=0 but speed card active (id=" + activeId + ") — cancelling.");
                        RemoteServices.Instance.CancelCard(activeId);
                    }
                    else
                    {
                        LogWarning("[Card] card_type=0 but speed card is active — AutoCancelWrongCard is off, leaving it.");
                    }
                    return true;
                }

                // ── card_type > 0: specific card desired ────────────────────────
                double desiredMult = GetDesiredCardMultiplier(cardType);
                bool desiredIsLogistics = IsLogisticsCardType(cardType);

                if (speedCardActive)
                {
                    bool multMatch     = Math.Abs(activeMult - desiredMult) < 0.001;
                    bool typeMatch     = (activeIsLogistics == desiredIsLogistics);

                    if (multMatch && typeMatch)
                    {
                        LogDebug("[Card] Correct card already active (type=" + cardType + ").");
                        return true;
                    }

                    // Wrong card is active
                    if (!settings.AutoCancelWrongCard)
                    {
                        LogError("[Card] Wrong speed card active (activeMult=" + activeMult.ToString("F3") +
                            " activeIsLogistics=" + activeIsLogistics + ") for desired card_type=" + cardType +
                            " — AutoCancelWrongCard is off. Cancelling batch.");
                        return false;
                    }

                    LogInfo("[Card] Wrong speed card active (id=" + activeId + ", mult=" + activeMult.ToString("F3") +
                        ") — cancelling before playing card_type=" + cardType + ".");
                    RemoteServices.Instance.CancelCard(activeId);
                }

                // Find and play the desired card from inventory
                int instanceId = FindInventoryCard(cardType);
                if (instanceId == 0)
                {
                    LogError("[Card] Card type " + cardType + " not found in inventory — cancelling batch.");
                    return false;
                }

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
                    delegate(string r) { LogDebug("[Card] PlayUserCard callback: " + r); },
                    InterfaceMgr.Instance.getDXBasePanel());

                return true;
            }
            catch (Exception ex)
            {
                LogError("[Card] EnsureCorrectCardActive: " + ex.Message);
                return false;
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private void RecallAll(AutoBombMultiSettings settings)
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
                int targetVid = _currentTargetVillageId;
                RemoteServices.Instance.set_CancelCastleAttack_UserCallBack(null);
                int recalled = 0;
                foreach (WorldMap.LocalArmyData army in GameEngine.Instance.World.getArmyArray())
                {
                    if (army.targetVillageID == targetVid &&
                        GameEngine.Instance.World.isUserVillage(army.travelFromVillageID))
                    {
                        RemoteServices.Instance.CancelCastleAttack(army.armyID);
                        recalled++;
                    }
                }
                LogInfo("Recalled " + recalled + " army/armies.");
            }
            catch (Exception ex) { LogError("Recall error: " + ex.Message); }
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
                });
                LogInfo("Registered with API as '" + playerName + "' with " + villages.Count + " village(s).");
            }
            catch (Exception ex) { LogError("PlayerReady failed: " + ex.Message); }
        }

        private List<Dictionary<string, object>> BuildLocalVillageList(AutoBombMultiSettings settings)
        {
            var result = new List<Dictionary<string, object>>();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return result;

            int targetVid = settings.TargetVillageId;
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
                });
            }
            return result;
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
            request.Timeout     = 10000;

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
        public string FormationName = "";
        public int Stack = 1;
        public int CardType;
        public bool CaptainsOnly;
        public int AttackType = 11;
        public double TravelTimeSeconds;
        public bool Selected = true;
    }
}
