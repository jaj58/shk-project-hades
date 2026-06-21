using System;
using System.Collections.Generic;
using System.Threading;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class MonkModule : BotModuleBase
    {
        // Guard for route background tick — prevents overlapping cycles.
        private volatile bool _routeTickRunning;

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
            bool first = true;
            for (int i = 0; i < routes.Count; i++)
            {
                MonkRouteSettings route = routes[i];
                if (!route.Enabled) continue;

                if (!first && settings.DelayBetweenRoutesMs > 0)
                    Thread.Sleep(settings.DelayBetweenRoutesMs);
                first = false;

                ProcessRoute(route, settings, i);
            }
        }

        private void ProcessRoute(MonkRouteSettings route, MonkSettings settings, int routeIndex)
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

            // For SendXMonksEach, check upfront if all targets are already complete
            if (route.StopCondition == MonkStopCondition.SendXMonksEach
                && route.ToTargets.Count > 0)
            {
                bool allDone = true;
                foreach (int tid in route.ToTargets)
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

            // Monks dispatched this cycle but whose send-callback hasn't fired yet.
            // countVillagePeople reads stale local state until the callback updates it
            // (~seconds later), so without this we'd double-spend the same monks.
            Dictionary<int, int> committed = new Dictionary<int, int>();

            foreach (int fromId in route.FromVillages)
            {
                // Once every target has been handled, skip remaining from-villages
                if (processedTargets.Count >= route.ToTargets.Count)
                    break;

                if (GameEngine.Instance.getVillage(fromId) == null)
                {
                    LogWarning("Village not loaded: " + fromId);
                    continue;
                }
                if (GameEngine.Instance.World.isCapital(fromId))
                {
                    // Capitals (parish/county/province/country) can't send monks — skip rather
                    // than spend every cycle retrying a send the server will always reject.
                    LogWarning("Village " + fromId + " is a capital and cannot send monks, skipping.");
                    continue;
                }
                if (GameEngine.Instance.World.isVillageExcommunicated(fromId))
                {
                    LogInfo("Village " + fromId + " is excommunicated, skipping.");
                    continue;
                }

                List<int> sortedTargets = BuildSortedTargets(fromId, route);

                foreach (int targetId in sortedTargets)
                {
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
                        LogInfo("Target " + targetId + " progress " + sent + "/" + route.ExtraParameter);
                    }
                    else
                    {
                        monksNeeded = GetConditionMonksNeeded(targetId, route.Command, route);
                    }

                    if (monksNeeded <= 0)
                    {
                        LogInfo("Target " + targetId + " does not need monks.");
                        processedTargets.Add(targetId);
                        continue;
                    }

                    int toSend = PrepareMonks(fromId, monksNeeded, settings, committed);
                    if (toSend <= 0)
                    {
                        LogInfo("No monks available at " + fromId);
                        continue;
                    }

                    // Record the commitment before the send fires so the next target in this
                    // loop sees the correct available count from the same village.
                    if (!committed.ContainsKey(fromId)) committed[fromId] = 0;
                    committed[fromId] += toSend;

                    // Capture per-send so the callback can correctly identify the route
                    // even if other sends (or ticks) overlap before the response arrives.
                    MonkRouteSettings capturedRoute = route;
                    int capturedIdx = routeIndex;
                    RemoteServices.Instance.set_SendPeople_UserCallBack(
                        delegate(SendPeople_ReturnType r) { OnSendPeopleCallback(r, capturedIdx, capturedRoute); });
                    RemoteServices.Instance.SendPeople(fromId, targetId, 4, toSend, (int)route.Command, -1);

                    LogInfo(fromId + " sent " + toSend + " " + route.Command + " monks to " + targetId);

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
                        processedTargets.Add(targetId);
                    }

                    // Pace sends using the same delay the user configures for between-route
                    // spacing — with a single route this is the only pacing control they have,
                    // so it should govern every send, not just route-to-route transitions.
                    if (settings.DelayBetweenRoutesMs > 0)
                        Thread.Sleep(settings.DelayBetweenRoutesMs);
                }
            }
        }

        private void OnSendPeopleCallback(SendPeople_ReturnType result, int routeIndex, MonkRouteSettings route)
        {
            try
            {
                if (result.Success)
                {
                    GameEngine.Instance.World.importOrphanedPeople(
                        result.people, result.currentTime, -2);
                    GameEngine.Instance.World.setFaithPointsData(
                        result.currentFaithPointsLevel, result.currentFaithPointsRate);
                }
                else
                {
                    string err = ErrorCodes.getErrorString(result.m_errorCode, result.m_errorID);
                    LogError(err + " | from=" + GameEngine.Instance.World.getVillageName(result.villageID)
                        + " to=" + GameEngine.Instance.World.getVillageName(result.targetVillageID));

                    // "Not enough spare people" is transient — the committed-monks guard in
                    // PrepareMonks should prevent it, but if it fires anyway don't kill the route.
                    if (err.IndexOf("spare people", StringComparison.OrdinalIgnoreCase) >= 0
                        || err.IndexOf("not enough", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        LogInfo("Transient resource error — route will retry next cycle.");
                        return;
                    }

                    LogInfo("Disabling route: " + route.Name);
                    route.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogError("Callback error: " + ex.Message);
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
            return CalculateEfficiencyAdjustedMonks(remaining, command, -1);
        }

        private int GetConditionMonksNeeded(int targetId, MonkCommand command, MonkRouteSettings route)
        {
            double needed = 0;

            switch (command)
            {
                case MonkCommand.Blessing:
                case MonkCommand.Inquisition:
                {
                    VillageMap vm = GameEngine.Instance.getVillage(targetId);
                    if (vm == null)
                    {
                        LogInfo("No loaded village/parish " + targetId);
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
                    LogInfo(command + " level at " + targetId + ": " + current + "/" + route.ExtraParameter);
                    needed = route.ExtraParameter - current;
                    break;
                }

                case MonkCommand.Interdiction:
                {
                    WorldMap.VillageRolloverInfo villageInfo = null;
                    WorldMap.CachedUserInfo userInfo = null;
                    GameEngine.Instance.World.retrieveUserData(targetId, -1, ref villageInfo, ref userInfo, true, false);

                    if (villageInfo == null)
                    {
                        VillageData vd = GameEngine.Instance.World.getVillageData(targetId);
                        if (vd != null && vd.rolloverInfo != null)
                            villageInfo = vd.rolloverInfo;
                        else
                        {
                            LogWarning("Cannot get interdict time for " + GameEngine.Instance.World.getVillageName(targetId));
                            return 0;
                        }
                    }

                    needed = route.ExtraParameter;
                    DateTime now = VillageMap.getCurrentServerTime();
                    if (now < villageInfo.interdictionTime)
                    {
                        double existing = (villageInfo.interdictionTime - now).TotalHours;
                        LogInfo(targetId + " has " + existing + "h interdict, need " + route.ExtraParameter + "h");
                        needed -= existing;
                    }
                    else
                    {
                        LogInfo(targetId + " is not interdicted.");
                    }
                    break;
                }

                case MonkCommand.Restoration:
                {
                    WorldMap.VillageRolloverInfo villageInfo = null;
                    WorldMap.CachedUserInfo userInfo = null;
                    GameEngine.Instance.World.retrieveUserData(targetId, -1, ref villageInfo, ref userInfo, true, false);
                    double disease = GameEngine.Instance.World.getParishPlagueLevel(targetId);
                    LogInfo("Disease at " + targetId + ": " + disease + ", target max: " + route.ExtraParameter);
                    needed = disease - route.ExtraParameter;
                    break;
                }

                case MonkCommand.Absolution:
                {
                    DateTime excommTime = GameEngine.Instance.World.getExcommunicationTime(targetId);
                    DateTime now = VillageMap.getCurrentServerTime();
                    if (now < excommTime)
                    {
                        double hours = (excommTime - now).TotalHours;
                        LogInfo(targetId + " has " + hours + "h excomm, threshold: " + route.ExtraParameter + "h");
                        needed = hours - route.ExtraParameter;
                    }
                    break;
                }

                case MonkCommand.Excommunication:
                {
                    DateTime excommTime = GameEngine.Instance.World.getExcommunicationTime(targetId);
                    DateTime now = VillageMap.getCurrentServerTime();
                    double existing = now < excommTime ? (excommTime - now).TotalHours : 0;
                    LogInfo(targetId + " has " + existing + "h excomm, want > " + route.ExtraParameter + "h");
                    needed = route.ExtraParameter - existing;
                    break;
                }

                case MonkCommand.Influence:
                    // Influence has no condition check — always use SendXMonksEach or QuestCompletion
                    return 0;
            }

            if (needed <= 0) return 0;
            return CalculateEfficiencyAdjustedMonks(needed, command, targetId);
        }

        private int CalculateEfficiencyAdjustedMonks(double rawAmount, MonkCommand command, int targetId)
        {
            double perMonk = 1.0;
            var cardData = GameEngine.Instance.cardsManager.UserCardData;

            switch (command)
            {
                case MonkCommand.Interdiction:
                    perMonk = CardTypes.adjustInterdictionLevel(cardData, 4);
                    break;
                case MonkCommand.Restoration:
                {
                    int lvl = Math.Max(1, (int)GameEngine.Instance.World.UserResearchData.Research_Baptism);
                    int baseAmt = ResearchData.baptismRestoreAmount[lvl];
                    perMonk = CardTypes.adjustRestorationLevel(cardData, baseAmt);
                    break;
                }
                case MonkCommand.Absolution:
                {
                    int lvl = Math.Max(1, (int)GameEngine.Instance.World.UserResearchData.Research_Confession);
                    double confTime = ResearchData.confessionTimes[lvl];
                    perMonk = CardTypes.adjustAbsolutionLevel(cardData, confTime);
                    break;
                }
                case MonkCommand.Excommunication:
                {
                    int lvl = Math.Max(1, (int)GameEngine.Instance.World.UserResearchData.Research_ExtremeUnction);
                    double unctTime = ResearchData.extremeUnctionTimes[lvl];
                    perMonk = CardTypes.adjustExcommunicationLevel(cardData, unctTime);
                    break;
                }
            }

            if (rawAmount <= 0) return 0;

            int monks = (int)(rawAmount / perMonk);
            if (rawAmount % perMonk != 0) monks++;

            LogInfo("Efficiency " + perMonk + "/monk → need " + monks + " monks");
            return monks;
        }

        private int PrepareMonks(int fromVillageId, int needed, MonkSettings settings, Dictionary<int, int> committed)
        {
            LogInfo("Preparing monks at " + fromVillageId + ", need " + needed);

            // Auto-recruit monks up to the configured target before checking availability
            if (settings.AutoRecruitMonks > 0)
                MakeMonks(fromVillageId, settings.AutoRecruitMonks);

            int atHome = 0;
            GameEngine.Instance.World.countVillagePeople(fromVillageId, 4, ref atHome);
            int alreadyCommitted = committed.ContainsKey(fromVillageId) ? committed[fromVillageId] : 0;
            int available = atHome - alreadyCommitted - settings.MonksToKeep;
            LogInfo("At home: " + atHome + ", committed: " + alreadyCommitted + ", keep: " + settings.MonksToKeep + ", available: " + available);
            if (available <= 0) return 0;
            return Math.Min(available, needed);
        }

        // Recruits monks at the village up to `target` total at-home count.
        // Respects ordination research cap, spare-worker limit, and unit space.
        // Returns the number actually recruited.
        private int MakeMonks(int villageId, int target)
        {
            try
            {
                if (GameEngine.Instance.World.UserResearchData.Research_Ordination == 0)
                    return 0;

                int researchCap = ResearchData.ordinationResearchMonkLevels[
                    (int)GameEngine.Instance.World.UserResearchData.Research_Ordination];
                int wantTotal = Math.Min(target, researchCap);

                int atHome = 0;
                int totalMonks = GameEngine.Instance.World.countVillagePeople(villageId, 4, ref atHome);
                int canMake = Math.Min(wantTotal - totalMonks, researchCap - totalMonks);
                if (canMake <= 0) return 0;

                VillageMap village = GameEngine.Instance.getVillage(villageId);
                if (village == null) return 0;

                // Spare worker limit
                int spareWorkers = village.m_spareWorkers;
                canMake = Math.Min(canMake, spareWorkers);

                // Unit-space limit
                int unitSizeMonk = GameEngine.Instance.LocalWorldData.UnitSize_Priests;
                int unitCapacity  = GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
                int freeSpace     = unitCapacity - village.calcUnitUsages();
                if (unitSizeMonk > 0)
                    canMake = Math.Min(canMake, freeSpace / unitSizeMonk);

                if (canMake <= 0) return 0;

                LogInfo("MakeMonks: recruiting " + canMake + " monk(s) at " + villageId);
                if (canMake == 1)
                    village.makePeople(4);
                else
                    village.makePeople(1000 + canMake);

                return canMake;
            }
            catch (Exception ex)
            {
                LogError("MakeMonks failed: " + ex.Message);
                return 0;
            }
        }

        private List<int> BuildSortedTargets(int fromId, MonkRouteSettings route)
        {
            List<int> targets = new List<int>(route.ToTargets);

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
