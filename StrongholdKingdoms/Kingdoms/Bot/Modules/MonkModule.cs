using System;
using System.Collections.Generic;
using System.Threading;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class MonkModule : BotModuleBase
    {
        private const int SendTimeoutMs = 20000;
        private const int RefreshTimeoutMs = 10000;
        private const int RecruitTimeoutMs = 10000;
        private const int LoadTimeoutMs = 10000;

        // Guard for route background tick — prevents overlapping cycles.
        private volatile bool _routeTickRunning;
        // Set on disable/shutdown so a running cycle stops at its next send or pause.
        private volatile bool _routeCancel;
        // Route worker thread only: whether this cycle has sent yet, so the send delay
        // spaces sends apart without a pointless wait before the first or after the last.
        private bool _cycleHasSent;

        // Manual interdict cycle (Interdict sub-tab)
        private int _interdictRunning;              // 0/1, Interlocked guard
        private volatile bool _interdictCancel;
        private readonly Random _interdictRand = new Random();

        public bool InterdictRunning { get { return _interdictRunning != 0; } }

        public override string ModuleName => "Monk";

        public override TimeSpan Interval
        {
            get
            {
                try
                {
                    int secs = Engine.Settings.Monk.CycleIntervalSeconds;
                    return TimeSpan.FromSeconds(secs > 0 ? secs : 120);
                }
                catch
                {
                    return TimeSpan.FromSeconds(120);
                }
            }
        }

        public void RunNow() { Tick(); }

        protected override void OnShutdown()
        {
            _interdictCancel = true;
            _routeCancel = true;
        }

        protected override void OnDisable()
        {
            _routeCancel = true;
        }

        protected override void OnTick()
        {
            if (_routeTickRunning)
            {
                LogInfo("Previous route tick still running — skipping.");
                return;
            }

            MonkSettings settings = Engine.Settings.Monk;
            List<MonkRouteSettings> routes = new List<MonkRouteSettings>(settings.Routes);

            bool anyEnabled = false;
            foreach (MonkRouteSettings r in routes)
                if (r.Enabled) { anyEnabled = true; break; }
            if (!anyEnabled) return;

            _routeTickRunning = true;
            _routeCancel = false;
            Thread t = new Thread(delegate()
            {
                try { ProcessAllRoutes(routes, settings); }
                catch (Exception ex) { LogError("Route tick error: " + ex.Message); }
                finally { _routeTickRunning = false; }
            });
            t.IsBackground = true;
            t.Name = "Monk Routes";
            t.Start();
        }

        private void ProcessAllRoutes(List<MonkRouteSettings> routes, MonkSettings settings)
        {
            _cycleHasSent = false;
            foreach (MonkRouteSettings route in routes)
            {
                if (_routeCancel) { LogInfo("Route cycle stopped."); return; }
                if (!route.Enabled) continue;
                ProcessRoute(route, settings);
            }
        }

        private void ProcessRoute(MonkRouteSettings route, MonkSettings settings)
        {
            LogInfo("Processing route: " + route.Name);

            if (!IsCommandResearched(route.Command))
            {
                LogWarning("Command not researched — disabling route: " + route.Name);
                route.Enabled = false;
                return;
            }

            int questMonksNeeded = 0;
            if (route.StopCondition == MonkStopCondition.QuestCompletion)
            {
                questMonksNeeded = GetQuestMonksNeeded(route.Command);
                if (questMonksNeeded <= 0)
                {
                    LogInfo("Quest complete — disabling route: " + route.Name);
                    route.Enabled = false;
                    return;
                }
            }

            // Copies: the route editor can change these lists while this thread walks them.
            List<int> fromVillages = new List<int>(route.FromVillages);
            List<int> toTargets = new List<int>(route.ToTargets);

            // For SendXMonksEach, check upfront if all targets are already complete
            if (route.StopCondition == MonkStopCondition.SendXMonksEach
                && toTargets.Count > 0)
            {
                bool allDone = true;
                foreach (int tid in toTargets)
                {
                    if (route.GetProgress(tid) < route.ExtraParameter)
                    { allDone = false; break; }
                }
                if (allDone)
                {
                    LogInfo("All targets complete — disabling route: " + route.Name);
                    route.Enabled = false;
                    return;
                }
            }

            HashSet<int> processedTargets = new HashSet<int>();

            // RunOnCondition: monks each target still needs this cycle. Worked out once per
            // target from freshly downloaded target info minus monks already on their way,
            // then counted down as we send, so several villages can top up one target
            // without overshooting it.
            Dictionary<int, int> conditionNeed = new Dictionary<int, int>();

            foreach (int fromId in fromVillages)
            {
                if (_routeCancel) return;

                // Once every target has been handled, skip remaining from-villages
                if (processedTargets.Count >= toTargets.Count)
                    break;

                if (GameEngine.Instance.World.isCapital(fromId))
                {
                    // Capitals (parish/county/province/country) can't send monks — skip rather
                    // than spend every cycle retrying a send the server will always reject.
                    LogWarning(VName(fromId) + " is a capital and cannot send monks, skipping.");
                    continue;
                }
                if (GameEngine.Instance.World.isVillageExcommunicated(fromId))
                {
                    LogInfo(VName(fromId) + " is excommunicated, skipping.");
                    continue;
                }

                // Worked out (hiring if enabled) only once a target actually needs monks
                // from this village, so idle routes don't recruit.
                int available = -1;

                foreach (int targetId in BuildSortedTargets(fromId, route, toTargets))
                {
                    if (_routeCancel) return;
                    if (processedTargets.Contains(targetId))
                        continue;

                    int monksNeeded;
                    if (route.StopCondition == MonkStopCondition.QuestCompletion)
                    {
                        monksNeeded = questMonksNeeded;
                    }
                    else if (route.StopCondition == MonkStopCondition.SendXMonksEach)
                    {
                        int sent = route.GetProgress(targetId);
                        monksNeeded = route.ExtraParameter - sent;
                        LogInfo("Target " + VName(targetId) + " progress " + sent + "/" + route.ExtraParameter);
                    }
                    else if (!conditionNeed.TryGetValue(targetId, out monksNeeded))
                    {
                        monksNeeded = GetConditionMonksNeeded(targetId, route.Command, route);
                        conditionNeed[targetId] = monksNeeded;
                    }

                    if (monksNeeded <= 0)
                    {
                        LogInfo("Target " + VName(targetId) + " does not need monks.");
                        processedTargets.Add(targetId);
                        continue;
                    }

                    if (available < 0)
                        available = PrepareMonks(fromId, settings);
                    if (available <= 0)
                    {
                        LogInfo("No spare monks at " + VName(fromId) + ".");
                        break;
                    }

                    int toSend = Math.Min(available, monksNeeded);

                    // Pace sends using the delay the user configures — with a single route
                    // this is the only pacing control they have, so it governs every send.
                    if (_cycleHasSent && !Pause(settings.DelayBetweenRoutesMs)) return;
                    _cycleHasSent = true;

                    string err;
                    MonkSendResult result = MonkSender.SendAndWait(
                        fromId, targetId, toSend, (int)route.Command, SendTimeoutMs, out err);

                    if (result == MonkSendResult.NoResponse)
                    {
                        // The send may still have gone through — don't send to this target
                        // again until next cycle, when any in-flight monks will be counted.
                        LogWarning("No reply sending " + toSend + " " + route.Command + " monks from "
                            + VName(fromId) + " to " + VName(targetId) + " — skipping target this cycle.");
                        processedTargets.Add(targetId);
                        continue;
                    }
                    if (result == MonkSendResult.Failed)
                    {
                        LogError(err + " | from=" + VName(fromId) + " to=" + VName(targetId));

                        // "Not enough spare people / faith" is transient — move on to the next
                        // village rather than killing the route.
                        if (err.IndexOf("spare people", StringComparison.OrdinalIgnoreCase) >= 0
                            || err.IndexOf("not enough", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            LogInfo("Transient resource error — trying the next village.");
                            break;
                        }

                        LogInfo("Disabling route: " + route.Name);
                        route.Enabled = false;
                        return;
                    }

                    LogInfo(VName(fromId) + " sent " + toSend + " " + route.Command + " monks to " + VName(targetId));
                    available -= toSend;
                    route.AddProgress(targetId, toSend);

                    if (route.StopCondition == MonkStopCondition.QuestCompletion)
                    {
                        questMonksNeeded -= toSend;
                        if (questMonksNeeded <= 0) return;
                        processedTargets.Add(targetId);
                    }
                    else if (route.StopCondition == MonkStopCondition.SendXMonksEach)
                    {
                        if (route.GetProgress(targetId) >= route.ExtraParameter)
                            processedTargets.Add(targetId);
                    }
                    else // RunOnCondition
                    {
                        conditionNeed[targetId] = monksNeeded - toSend;
                        if (monksNeeded - toSend <= 0)
                            processedTargets.Add(targetId);
                    }

                    if (available <= 0)
                        break;
                }
            }
        }

        private bool IsCommandResearched(MonkCommand command)
        {
            ResearchData r = GameEngine.Instance.World.UserResearchData;
            switch (command)
            {
                case MonkCommand.Blessing:        return r.Research_Marriage > 0;
                case MonkCommand.Inquisition:     return r.Research_Confirmation > 0;
                case MonkCommand.Interdiction:
                    return r.Research_Eucharist > 0
                        && GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1;
                case MonkCommand.Restoration:     return r.Research_Baptism > 0;
                case MonkCommand.Absolution:      return r.Research_Confession > 0;
                case MonkCommand.Excommunication: return r.Research_ExtremeUnction > 0;
                case MonkCommand.Influence:       return true; // No specific research gate known
                default:                          return false;
            }
        }

        private int GetQuestMonksNeeded(MonkCommand command)
        {
            NewQuestsData questData = GameEngine.Instance.World.getNewQuestData();
            if (questData == null) { LogWarning("Quest data unavailable."); return 0; }

            NewQuests.NewQuestDefinition def = NewQuests.getNewQuestDef(questData.questID);
            if (def == null) { LogInfo("No active quest."); return 0; }

            // Verify the quest type matches this command
            bool wrongQuest = false;
            switch (command)
            {
                case MonkCommand.Blessing:        wrongQuest = def.questType != 0;  break;
                case MonkCommand.Inquisition:     wrongQuest = def.questType != 33; break;
                case MonkCommand.Interdiction:    wrongQuest = def.questType != 42; break;
                case MonkCommand.Restoration:     wrongQuest = def.questType != 15; break;
                case MonkCommand.Absolution:      wrongQuest = def.questType != 43; break;
                case MonkCommand.Excommunication: wrongQuest = def.questType != 44; break;
                default: wrongQuest = true; break;
            }

            if (wrongQuest) { LogWarning("Wrong quest type for command " + command); return 0; }

            double remaining = def.parameter - questData.data;
            if (remaining <= 0) return 0;

            LogInfo("Quest progress: " + questData.data + "/" + def.parameter);
            return CalculateEfficiencyAdjustedMonks(remaining, command);
        }

        private int GetConditionMonksNeeded(int targetId, MonkCommand command, MonkRouteSettings route)
        {
            // Influence has no condition check — always use SendXMonksEach or QuestCompletion
            if (command == MonkCommand.Influence) return 0;

            string name = VName(targetId);

            // Monks already on their way haven't changed the target yet, so the values read
            // below don't include them. Counted BEFORE the refresh: a monk landing in between
            // is then counted twice (under-send, topped up next cycle) instead of not at all
            // (over-send).
            int inTransit = MonkSender.CountInTransit(targetId, (int)command);
            if (inTransit < 0)
            {
                LogWarning("Couldn't read monks in transit to " + name + " — skipping it this cycle.");
                return 0;
            }

            double needed = 0;

            switch (command)
            {
                case MonkCommand.Blessing:
                case MonkCommand.Inquisition:
                {
                    VillageMap vm = GameEngine.Instance.getVillage(targetId);
                    if (vm == null)
                    {
                        LogWarning(name + " isn't downloaded — can't read its " + command + " level.");
                        return 0;
                    }
                    int eventType = (command == MonkCommand.Blessing) ? 10101 : 10102;
                    PopEventData[] events = vm.m_popEvents;
                    int current = 0;
                    if (events != null)
                    {
                        foreach (PopEventData e in events)
                            if (e.eventType == eventType) current += e.eventEffect;
                    }
                    LogInfo(command + " level at " + name + ": " + current + "/" + route.ExtraParameter);
                    needed = route.ExtraParameter - current;
                    break;
                }

                default:
                {
                    // Plague level and interdict/excommunication times only update when the
                    // game re-downloads the target's info (a map hover does it), so fetch it
                    // now — otherwise a parish the player hasn't looked at has no disease
                    // value at all, and one they have is whatever it was back then.
                    if (!MonkSender.RefreshTargetInfo(targetId, RefreshTimeoutMs, IsRouteCancelled))
                    {
                        LogWarning("Couldn't refresh " + name + " — skipping it this cycle.");
                        return 0;
                    }
                    DateTime now = VillageMap.getCurrentServerTime();

                    if (command == MonkCommand.Interdiction)
                    {
                        DateTime interdictEnd = GameEngine.Instance.World.getInterdictTime(targetId);
                        double existing = now < interdictEnd ? (interdictEnd - now).TotalHours : 0;
                        LogInfo(name + " has " + existing.ToString("0.#") + "h interdict, need " + route.ExtraParameter + "h");
                        needed = route.ExtraParameter - existing;
                    }
                    else if (command == MonkCommand.Restoration)
                    {
                        int disease = GameEngine.Instance.World.getParishPlagueLevel(targetId);
                        if (disease < 0)
                        {
                            LogWarning("No disease level for " + name + " — skipping it this cycle.");
                            return 0;
                        }
                        LogInfo("Disease at " + name + ": " + disease + ", target max: " + route.ExtraParameter);
                        needed = disease - route.ExtraParameter;
                    }
                    else if (command == MonkCommand.Absolution)
                    {
                        DateTime excommTime = GameEngine.Instance.World.getExcommunicationTime(targetId);
                        if (now < excommTime)
                        {
                            double hours = (excommTime - now).TotalHours;
                            LogInfo(name + " has " + hours.ToString("0.#") + "h excomm, threshold: " + route.ExtraParameter + "h");
                            needed = hours - route.ExtraParameter;
                        }
                    }
                    else if (command == MonkCommand.Excommunication)
                    {
                        DateTime excommTime = GameEngine.Instance.World.getExcommunicationTime(targetId);
                        double existing = now < excommTime ? (excommTime - now).TotalHours : 0;
                        LogInfo(name + " has " + existing.ToString("0.#") + "h excomm, want > " + route.ExtraParameter + "h");
                        needed = route.ExtraParameter - existing;
                    }
                    break;
                }
            }

            if (needed <= 0) return 0;

            if (inTransit > 0)
            {
                double covered = inTransit * GetPerMonkAmount(command);
                LogInfo(inTransit + " " + command + " monk(s) already on the way to " + name
                    + " (covers " + covered.ToString("0.#") + " of " + needed.ToString("0.#") + ").");
                needed -= covered;
                if (needed <= 0) return 0;
            }

            return CalculateEfficiencyAdjustedMonks(needed, command);
        }

        // How much one monk of this command achieves (hours, disease points, levels),
        // after research and cards.
        private double GetPerMonkAmount(MonkCommand command)
        {
            var cardData = GameEngine.Instance.cardsManager.UserCardData;
            switch (command)
            {
                case MonkCommand.Interdiction:
                    return CardTypes.adjustInterdictionLevel(cardData, 4);
                case MonkCommand.Restoration:
                {
                    int lvl = Math.Max(1, (int)GameEngine.Instance.World.UserResearchData.Research_Baptism);
                    int baseAmt = ResearchData.baptismRestoreAmount[lvl];
                    return CardTypes.adjustRestorationLevel(cardData, baseAmt);
                }
                case MonkCommand.Absolution:
                {
                    int lvl = Math.Max(1, (int)GameEngine.Instance.World.UserResearchData.Research_Confession);
                    double confTime = ResearchData.confessionTimes[lvl];
                    return CardTypes.adjustAbsolutionLevel(cardData, confTime);
                }
                case MonkCommand.Excommunication:
                {
                    int lvl = Math.Max(1, (int)GameEngine.Instance.World.UserResearchData.Research_ExtremeUnction);
                    double unctTime = ResearchData.extremeUnctionTimes[lvl];
                    return CardTypes.adjustExcommunicationLevel(cardData, unctTime);
                }
                default:
                    return 1.0;
            }
        }

        private int CalculateEfficiencyAdjustedMonks(double rawAmount, MonkCommand command)
        {
            if (rawAmount <= 0) return 0;

            double perMonk = GetPerMonkAmount(command);
            if (perMonk <= 0) perMonk = 1.0;

            int monks = (int)(rawAmount / perMonk);
            if (rawAmount % perMonk != 0) monks++;

            LogInfo("Efficiency " + perMonk + "/monk → need " + monks + " monks");
            return monks;
        }

        // Hires up to the auto-recruit target and waits for the recruits to actually turn up
        // (hiring is async — counting straight after it misses them), then returns how many
        // monks can be sent after the keep-back.
        private int PrepareMonks(int fromId, MonkSettings settings)
        {
            string name = VName(fromId);
            int total, atHome;
            if (!MonkSender.CountMonks(fromId, out total, out atHome))
            {
                LogWarning("Couldn't read the monks at " + name + ".");
                return 0;
            }

            int ordination = (int)GameEngine.Instance.World.UserResearchData.Research_Ordination;
            if (settings.AutoRecruitMonks > 0 && ordination > 0)
            {
                int cap = ResearchData.ordinationResearchMonkLevels[ordination];
                int want = Math.Min(settings.AutoRecruitMonks, cap) - total;
                if (want > 0)
                {
                    // Hiring needs the village map (spare workers, unit space); sending doesn't.
                    if (GameEngine.Instance.getVillage(fromId) == null)
                    {
                        LogInfo(name + " isn't loaded — loading it to hire monks.");
                        InterdictRunner.RequestLoad(fromId);
                        InterdictRunner.WaitForLoad(fromId, LoadTimeoutMs, IsRouteCancelled);
                    }

                    if (GameEngine.Instance.getVillage(fromId) != null)
                    {
                        int hired = MonkSender.RecruitAndWait(fromId, want, RecruitTimeoutMs, IsRouteCancelled);
                        if (hired > 0)
                            LogInfo("Hired " + hired + " monk(s) at " + name + ".");
                        MonkSender.CountMonks(fromId, out total, out atHome);
                    }
                    else
                    {
                        LogWarning(name + " still isn't loaded — sending only the monks already there.");
                    }
                }
            }

            int available = Math.Max(0, atHome - settings.MonksToKeep);
            LogInfo("Monks at " + name + ": " + atHome + " home, keep " + settings.MonksToKeep
                + ", " + available + " available");
            return available;
        }

        private List<int> BuildSortedTargets(int fromId, MonkRouteSettings route, List<int> toTargets)
        {
            List<int> targets = new List<int>(toTargets);

            if (route.IsDistanceLimited)
            {
                int maxSq = route.DistanceLimit * route.DistanceLimit;
                targets.RemoveAll(id => GameEngine.Instance.World.getSquareDistance(fromId, id) > maxSq);
            }

            targets.Sort((a, b) =>
            {
                int da = GameEngine.Instance.World.getSquareDistance(fromId, a);
                int db = GameEngine.Instance.World.getSquareDistance(fromId, b);
                return da.CompareTo(db);
            });

            return targets;
        }

        // Sleeps in short slices so disabling the module or shutting down stops a cycle
        // promptly. False if cancelled.
        private bool Pause(int ms)
        {
            int slept = 0;
            while (slept < ms)
            {
                if (_routeCancel) return false;
                int step = Math.Min(200, ms - slept);
                Thread.Sleep(step);
                slept += step;
            }
            return !_routeCancel;
        }

        private bool IsRouteCancelled() { return _routeCancel; }

        private static string VName(int villageId)
        {
            try { return GameEngine.Instance.World.getVillageName(villageId) + " [" + villageId + "]"; }
            catch { return "[" + villageId + "]"; }
        }

        // =================================================================
        // Manual interdict (Interdict sub-tab) — self-interdicts each
        // selected village on a background thread.
        // =================================================================

        public bool InterdictVillages(List<int> villageIds, int monkCount, bool allowHire, bool reduceIfInterdicted, int delayMs)
        {
            if (villageIds == null || villageIds.Count == 0)
            {
                LogWarning("Interdict: no villages selected.");
                return false;
            }
            if (Interlocked.CompareExchange(ref _interdictRunning, 1, 0) != 0)
            {
                LogWarning("Interdict: cycle already running.");
                return false;
            }

            List<int> ids = new List<int>(villageIds);
            if (monkCount < 1) monkCount = 1;
            if (delayMs < 0) delayMs = 0;
            _interdictCancel = false;

            Thread t = new Thread(delegate ()
            {
                try { InterdictCycle(ids, monkCount, allowHire, reduceIfInterdicted, delayMs); }
                catch (Exception ex) { LogError("Interdict cycle failed: " + ex.Message); }
                finally { Interlocked.Exchange(ref _interdictRunning, 0); }
            });
            t.IsBackground = true;
            t.Name = "Monk Interdict";
            t.Start();
            return true;
        }

        private void InterdictCycle(List<int> ids, int monkCount, bool allowHire, bool reduceIfInterdicted, int delayMs)
        {
            LogInfo("Interdict: starting cycle for " + ids.Count + " village(s), " + monkCount + " monk(s) each, "
                + delayMs + "ms delay between interdicts.");

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
            {
                LogWarning("Interdict: world not loaded.");
                return;
            }
            if (GameEngine.Instance.World.UserResearchData.Research_Ordination == 0)
            {
                LogWarning("Interdict: Ordination not researched. Cannot use monks.");
                return;
            }
            if (GameEngine.Instance.World.UserResearchData.Research_Eucharist <= 0)
            {
                LogWarning("Interdict: Eucharist not researched. Cannot interdict.");
                return;
            }
            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
            {
                LogWarning("Interdict: interdiction is disabled on this ruleset.");
                return;
            }

            int hoursPerMonk = CardTypes.adjustInterdictionLevel(
                GameEngine.Instance.cardsManager.UserCardData, 4);
            if (hoursPerMonk <= 0) hoursPerMonk = 4;

            for (int i = 0; i < ids.Count; i++)
            {
                if (_interdictCancel)
                {
                    LogInfo("Interdict: cycle cancelled.");
                    break;
                }

                int villageId = ids[i];
                string name = GameEngine.Instance.World.getVillageName(villageId);
                LogInfo("Interdict: checking village " + name);

                if (GameEngine.Instance.World.isCapital(villageId))
                {
                    LogInfo("Interdict: skipping capital " + name);
                    continue;
                }

                if (GameEngine.Instance.World.isVillageExcommunicated(villageId))
                {
                    LogWarning("Interdict: " + name + " is excommunicated, skipping.");
                    continue;
                }

                int desired = monkCount;

                // Top-up: only send enough monks to reach the desired duration
                if (reduceIfInterdicted)
                {
                    DateTime interdictEnd = GameEngine.Instance.World.getInterdictTime(villageId);
                    DateTime now = VillageMap.getCurrentServerTime();
                    if (interdictEnd > now)
                    {
                        int remainingSecs = (int)(interdictEnd - now).TotalSeconds;
                        int deficitSecs = monkCount * hoursPerMonk * 3600 - remainingSecs;
                        if (deficitSecs <= 0)
                        {
                            LogInfo("Interdict: " + name + " already has enough interdiction time, skipping.");
                            continue;
                        }
                        desired = deficitSecs / (hoursPerMonk * 3600);
                        if (desired < 8) desired++;
                        if (desired > monkCount) desired = monkCount;
                        LogInfo("Interdict: " + name + " has " + (remainingSecs / 3600) + "h interdict left, sending " + desired + " monk(s) to top up.");
                    }
                }

                // Hire, verify, send — with retry. Runner handles loading, faith,
                // recruit-wait and send confirmation.
                string err;
                bool ok = InterdictRunner.Run(villageId, desired, allowHire, 5, "Monk",
                                              delegate { return _interdictCancel; }, out err);
                if (ok)
                    LogInfo("Interdict: " + name + " interdicted successfully.");
                else
                    LogError("Interdict: could not interdict " + name + " — " + err);

                // User-set spacing plus a little jitter so sends aren't perfectly uniform;
                // skipped after the last village so the cycle ends promptly.
                if (i < ids.Count - 1)
                    Thread.Sleep(delayMs + _interdictRand.Next(400));
            }

            LogInfo("Interdict: cycle finished.");
        }

    }
}
