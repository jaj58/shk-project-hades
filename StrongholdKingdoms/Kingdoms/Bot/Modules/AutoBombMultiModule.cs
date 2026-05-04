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

        // Current set of attacks this client is responsible for
        private readonly object _attacksLock = new object();
        private List<BombAttackEntry> _myAttacks = new List<BombAttackEntry>();
        private int _currentTargetVillageId;

        // Last known session state to detect transitions
        private string _lastSessionState = "";
        private bool _pollStarted;
        private DateTime _launchThreadFinishedAt = DateTime.MinValue;

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

            if (settings.AutoCancelOnInterdict && !_interdictDetected)
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
                // Re-trigger only for genuine reconnect (not a rapid restart after thread exit).
                // 60s cooldown prevents the poll loop from spinning up a new thread every 200ms
                // when the launch thread exits before all attacks are marked sent.
                if ((DateTime.Now - _launchThreadFinishedAt).TotalSeconds > 60)
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

            int noTimeCnt = 0;
            foreach (var e in myEntries) if (e.ScheduledSendTime == DateTime.MaxValue) noTimeCnt++;
            if (noTimeCnt > 0)
                LogWarning(noTimeCnt + " attack(s) have no scheduled send time — they will be skipped. " +
                    "Ensure the PHP file is up to date and all attacks were pushed with correct travel times.");

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

        private void LaunchThreadProc(object obj)
        {
            var ctx = (LaunchContext)obj;
            var settings = ctx.Settings;
            var attacks  = ctx.Attacks;

            // Sort by scheduled send time ascending (earliest first) so each attack is
            // prepared and fired in sequence. Attacks without a scheduled time go last,
            // ordered by stack. This replaces the old two-phase (prepare-all then fire-all)
            // approach, which couldn't handle 1s stack delays where prepare takes ~10s.
            attacks.Sort((a, b) => {
                if (a.ScheduledSendTime == DateTime.MaxValue && b.ScheduledSendTime == DateTime.MaxValue)
                    return a.Stack.CompareTo(b.Stack);
                if (a.ScheduledSendTime == DateTime.MaxValue) return 1;
                if (b.ScheduledSendTime == DateTime.MaxValue) return -1;
                return a.ScheduledSendTime.CompareTo(b.ScheduledSendTime);
            });

            try
            {
                LogInfo("[Thread] Launching " + attacks.Count + " attack(s).");

                foreach (var entry in attacks)
                {
                    if (_cancelLaunchEvent.WaitOne(0)) break;

                    // Skip attacks that weren't assigned a send time by the server
                    // (e.g. because the PHP is outdated or travel time was 0 at push time).
                    if (entry.ScheduledSendTime == DateTime.MaxValue)
                    {
                        LogWarning("[Thread] Village " + entry.SourceVillageId +
                            " has no scheduled send time — skipping.");
                        PostAttackEvent(settings, "attack_failed_prepare", entry.SourceVillageId,
                            "no scheduled time");
                        continue;
                    }

                    // Wait until 8s before scheduled send time, then prepare
                    if (entry.ScheduledSendTime != DateTime.MaxValue)
                    {
                        TimeSpan waitTime = entry.ScheduledSendTime - DateTime.Now - TimeSpan.FromSeconds(8);
                        if (waitTime > TimeSpan.Zero)
                        {
                            LogInfo("[Thread] Stack " + entry.Stack + ": waiting " +
                                (int)waitTime.TotalSeconds + "s before prepare.");
                            if (_cancelLaunchEvent.WaitOne((int)waitTime.TotalMilliseconds)) break;
                        }
                    }

                    if (_cancelLaunchEvent.WaitOne(0)) break;

                    entry.Status = "Preparing...";
                    bool ok = PrepareAttackOnThread(entry, settings);
                    if (!ok)
                    {
                        PostAttackEvent(settings, "attack_failed_prepare", entry.SourceVillageId,
                            entry.Status);
                        continue;
                    }
                    PostAttackEvent(settings, "attack_prepared", entry.SourceVillageId);

                    // Wait until scheduled send time
                    if (entry.ScheduledSendTime != DateTime.MaxValue)
                    {
                        TimeSpan waitTime = entry.ScheduledSendTime - DateTime.Now;
                        if (waitTime > TimeSpan.Zero)
                        {
                            if (_cancelLaunchEvent.WaitOne((int)waitTime.TotalMilliseconds)) break;
                        }
                    }

                    if (_cancelLaunchEvent.WaitOne(0)) break;

                    FirePreparedAttack(entry);
                    if (entry.Sent)
                        PostAttackEvent(settings, "attack_sent", entry.SourceVillageId);
                    else
                        PostAttackEvent(settings, "attack_failed", entry.SourceVillageId, entry.Status);

                    // Brief anti-spam delay between consecutive fires
                    _cancelLaunchEvent.WaitOne(500);
                }
            }
            catch (Exception ex)
            {
                LogError("[Thread] Unhandled exception: " + ex.Message);
            }
            finally
            {
                _launching = false;
                _launchThreadFinishedAt = DateTime.Now;
                LogInfo("[Thread] Launch thread exiting.");
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
