using System;
using System.Threading;
using CommonTypes;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot
{
    /// <summary>
    /// Shared "interdict one village, with verification + retry" routine used by both
    /// the manual Monk Interdict cycle and the Radar auto-interdict.
    ///
    /// Why this exists: a raw self-interdict often fails because <see cref="VillageMap.makePeople"/>
    /// is asynchronous — freshly hired monks don't exist locally until the recruit
    /// callback lands, so an immediate send is rejected for "not enough monks". This
    /// helper hires, waits for the recruits to actually arrive, sends, then waits for
    /// the send callback to confirm success — retrying the whole sequence up to
    /// <c>maxAttempts</c> times before giving up.
    /// </summary>
    public static class InterdictRunner
    {
        // Set once by BotControlForm so background callers can marshal the village-load
        // request onto the UI/game thread. Null => best-effort direct call.
        public static Action<Action> UiInvoke;

        // Only one interdict send may be in flight globally, because the SendPeople
        // callback is a single shared slot on RemoteServices.
        private static readonly object _sendLock = new object();
        private static readonly AutoResetEvent _sendDone = new AutoResetEvent(false);
        private static volatile bool _lastSuccess;
        private static volatile int _expectedVillage = -1;
        private static volatile string _lastErr = "";
        private static readonly Random _rand = new Random();

        /// <summary>
        /// Attempts to self-interdict <paramref name="villageId"/> with up to
        /// <paramref name="desiredMonks"/> monks, hiring (and force-loading the village)
        /// as needed, retrying up to <paramref name="maxAttempts"/> times.
        /// Returns true on a confirmed successful send; otherwise false with
        /// <paramref name="lastError"/> describing why.
        /// </summary>
        public static bool Run(int villageId, int desiredMonks, bool allowHire,
                               int maxAttempts, string logModule,
                               Func<bool> cancelled, out string lastError)
        {
            lastError = "";
            if (desiredMonks < 1) desiredMonks = 1;
            if (maxAttempts < 1) maxAttempts = 1;

            // ---- account / village level aborts (won't change across retries) ----
            try
            {
                WorldMap world = GameEngine.Instance != null ? GameEngine.Instance.World : null;
                if (world == null) { lastError = "world not loaded"; return false; }
                if (world.isCapital(villageId)) { lastError = "village is a capital"; return false; }
                if (world.isVillageExcommunicated(villageId)) { lastError = "village is excommunicated"; return false; }
                if (world.UserResearchData.Research_Ordination == 0) { lastError = "Ordination not researched"; return false; }
                if (world.UserResearchData.Research_Eucharist <= 0) { lastError = "Eucharist not researched"; return false; }
                if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1) { lastError = "interdiction disabled on this ruleset"; return false; }
            }
            catch (Exception ex)
            {
                lastError = "validation error: " + ex.Message;
                return false;
            }

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                if (cancelled != null && cancelled()) { lastError = "cancelled"; return false; }

                BotLogger.Log(logModule, BotLogLevel.Info,
                    "Interdict attempt " + attempt + "/" + maxAttempts + " for "
                    + SafeVillageName(villageId) + ".");

                try
                {
                    // 1) ensure the village is loaded (force-load + poll if needed)
                    VillageMap village = GameEngine.Instance.getVillage(villageId);
                    if (village == null)
                    {
                        BotLogger.Log(logModule, BotLogLevel.Info,
                            SafeVillageName(villageId) + " not loaded — requesting load.");
                        RequestLoad(villageId);
                        village = WaitForLoad(villageId, 10000, cancelled);
                        if (village == null)
                        {
                            _lastErr = "village not loaded";
                            if (Backoff(attempt, maxAttempts, cancelled, out lastError)) continue;
                            return false;
                        }
                    }

                    // 2) faith point check (re-checked every attempt)
                    int costPerMonk = TradingCalcs.adjustInterdictionCostByTargetRank(
                        GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Interdicts,
                        GameEngine.Instance.World.getRank(),
                        GameEngine.Instance.World.SecondAgeWorld);
                    if ((double)(costPerMonk * desiredMonks) > GameEngine.Instance.World.getCurrentFaithPoints())
                    {
                        _lastErr = "not enough faith points";
                        if (Backoff(attempt, maxAttempts, cancelled, out lastError)) continue;
                        return false;
                    }

                    // 3) hire missing monks and wait for them to actually arrive
                    int before = village.calcTotalMonksAtHome();
                    if (before < desiredMonks && allowHire)
                    {
                        int made = MakeMonksMore(villageId, desiredMonks - before);
                        if (made > 0)
                        {
                            BotLogger.Log(logModule, BotLogLevel.Info,
                                "Hiring " + made + " monk(s) at " + SafeVillageName(villageId)
                                + " — waiting for recruits...");
                            WaitForMonks(village, before + made, 8000, cancelled);
                        }
                    }

                    int atHome = village.calcTotalMonksAtHome();
                    if (atHome <= 0)
                    {
                        _lastErr = "no monks available";
                        if (Backoff(attempt, maxAttempts, cancelled, out lastError)) continue;
                        return false;
                    }

                    int toSend = Math.Min(desiredMonks, atHome);

                    // 4) send + wait for the confirming callback
                    if (SendAndWait(villageId, toSend, logModule))
                    {
                        BotLogger.Log(logModule, BotLogLevel.Info,
                            "Interdict confirmed for " + SafeVillageName(villageId)
                            + " (" + toSend + " monk(s)).");
                        return true;
                    }

                    if (Backoff(attempt, maxAttempts, cancelled, out lastError)) continue;
                    return false;
                }
                catch (Exception ex)
                {
                    _lastErr = ex.Message;
                    if (Backoff(attempt, maxAttempts, cancelled, out lastError)) continue;
                    return false;
                }
            }

            lastError = "failed after " + maxAttempts + " attempts: " + _lastErr;
            return false;
        }

        // Sleeps a random backoff between attempts. Returns true if the caller should
        // retry (attempts remain), false if exhausted — in which case lastError is set.
        private static bool Backoff(int attempt, int maxAttempts, Func<bool> cancelled, out string lastError)
        {
            if (attempt >= maxAttempts)
            {
                lastError = "failed after " + maxAttempts + " attempts: " + _lastErr;
                return false;
            }
            lastError = "";
            int slept = 0;
            int target = 1500 + _rand.Next(1000);
            while (slept < target)
            {
                if (cancelled != null && cancelled()) { lastError = "cancelled"; return false; }
                Thread.Sleep(200);
                slept += 200;
            }
            return true;
        }

        // Sends the self-interdict and blocks until the callback confirms, fails, or
        // times out. Serialized so only one send uses the shared callback slot at a time.
        private static bool SendAndWait(int villageId, int monks, string logModule)
        {
            lock (_sendLock)
            {
                _sendDone.WaitOne(0);              // drain any stale signal
                _expectedVillage = villageId;
                _lastSuccess = false;
                _lastErr = "no response";

                RemoteServices.Instance.set_SendPeople_UserCallBack(
                    new RemoteServices.SendPeople_UserCallBack(SendPeopleCallback));
                RemoteServices.Instance.SendPeople(villageId, villageId, 4, monks, 4, -1);

                if (!_sendDone.WaitOne(20000))
                {
                    _lastErr = "no response within 20s";
                    return false;
                }
                return _lastSuccess;
            }
        }

        private static void SendPeopleCallback(SendPeople_ReturnType result)
        {
            try
            {
                // A late callback from a previously timed-out attempt can arrive here;
                // it targets the same village, so honouring it is harmless.
                if (result.Success)
                {
                    GameEngine.Instance.World.importOrphanedPeople(result.people, result.currentTime, -2);
                    GameEngine.Instance.World.setFaithPointsData(
                        result.currentFaithPointsLevel, result.currentFaithPointsRate);
                    _lastSuccess = true;
                }
                else
                {
                    _lastSuccess = false;
                    _lastErr = ErrorCodes.getErrorString(result.m_errorCode, result.m_errorID);
                }
            }
            catch (Exception ex)
            {
                _lastSuccess = false;
                _lastErr = ex.Message;
            }
            finally
            {
                if (result.targetVillageID == _expectedVillage)
                    _sendDone.Set();
            }
        }

        // Polls until the village reports at least `target` monks at home, or timeout.
        private static void WaitForMonks(VillageMap village, int target, int timeoutMs, Func<bool> cancelled)
        {
            int waited = 0;
            while (waited < timeoutMs)
            {
                if (cancelled != null && cancelled()) return;
                if (village.calcTotalMonksAtHome() >= target) return;
                Thread.Sleep(200);
                waited += 200;
            }
        }

        // Polls until the village map is loaded into memory, or timeout.
        private static VillageMap WaitForLoad(int villageId, int timeoutMs, Func<bool> cancelled)
        {
            int waited = 0;
            while (waited < timeoutMs)
            {
                if (cancelled != null && cancelled()) return null;
                VillageMap v = GameEngine.Instance.getVillage(villageId);
                if (v != null) return v;
                Thread.Sleep(250);
                waited += 250;
            }
            return GameEngine.Instance.getVillage(villageId);
        }

        // Requests a force-load of the village, marshaled onto the UI/game thread
        // because it touches InterfaceMgr/GameEngine view state.
        private static void RequestLoad(int villageId)
        {
            Action load = delegate
            {
                try
                {
                    VillageSyncModule vsm = BotEngine.Instance != null
                        ? BotEngine.Instance.GetModule<VillageSyncModule>() : null;
                    if (vsm != null)
                    {
                        vsm.RefreshVillageNow(villageId);
                    }
                    else
                    {
                        InterfaceMgr.Instance.setVillageNameBar(villageId);
                        GameEngine.Instance.forceDownloadCurrentVillage();
                    }
                }
                catch (Exception ex)
                {
                    BotLogger.Log("Interdict", BotLogLevel.Warning,
                        "Force-load of village " + villageId + " failed: " + ex.Message);
                }
            };

            Action<Action> inv = UiInvoke;
            if (inv != null) inv(load);
            else load();
        }

        /// <summary>
        /// Recruits <paramref name="more"/> additional monks at the village (on top of
        /// the current total), respecting ordination cap, spare workers and unit space.
        /// Returns the number it attempted to make. The send is async — callers must poll
        /// <see cref="VillageMap.calcTotalMonksAtHome"/> to confirm arrival.
        /// </summary>
        internal static int MakeMonksMore(int villageId, int more)
        {
            try
            {
                if (more <= 0) return 0;

                int researchMonkLevel = ResearchData.ordinationResearchMonkLevels[
                    (int)GameEngine.Instance.World.UserResearchData.Research_Ordination];

                int athome = 0;
                int totalMonks = GameEngine.Instance.World.countVillagePeople(villageId, 4, ref athome);
                int canMake = researchMonkLevel - totalMonks;
                canMake = Math.Min(canMake, more);
                if (canMake <= 0) return 0;

                VillageMap village = GameEngine.Instance.getVillage(villageId);
                if (village == null) return 0;

                int spareWorkers = village.m_spareWorkers;
                if (canMake > spareWorkers) canMake = spareWorkers;

                int unitCapacity = GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
                int currentUsage = village.calcUnitUsages();
                int unitSizeMonk = GameEngine.Instance.LocalWorldData.UnitSize_Priests;
                if (unitSizeMonk > 0)
                {
                    int freeSpace = unitCapacity - currentUsage;
                    if (freeSpace < canMake * unitSizeMonk)
                        canMake = freeSpace / unitSizeMonk;
                }

                if (canMake <= 0) return 0;

                if (canMake == 1)
                    village.makePeople(4);
                else
                    village.makePeople(1000 + canMake);

                return canMake;
            }
            catch (Exception ex)
            {
                BotLogger.Log("Interdict", BotLogLevel.Error, "MakeMonksMore failed: " + ex.Message);
                return 0;
            }
        }

        private static string SafeVillageName(int villageId)
        {
            try { return GameEngine.Instance.World.getVillageName(villageId) + " [" + villageId + "]"; }
            catch { return "[" + villageId + "]"; }
        }
    }
}
