using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class ScoutModule : BotModuleBase
    {
        private DateTime _lastSendTime = DateTime.MinValue;
        private DateTime _lastCycleTime = DateTime.MinValue;
        private bool _cycleComplete = true;
        private readonly Random _rng = new Random();

        // Sentinel ResourceLevel values on StashTarget:
        private const int LEVEL_NEW_STASH   = -1; // type 100 — unknown type/size, no fetch possible
        private const int LEVEL_NEEDS_FETCH = -2; // known type but size not yet in the special-village cache
        private const int LEVEL_STALE       = -3; // cached size too old to send against — refresh, skip this pass

        // peekSpecialVillageData has no TTL of its own, so a size fetched once would stay frozen for
        // the whole session. Refresh anything older than this before sending against it. Must stay
        // above WorldMap's own 60s per-village SpecialVillageInfo suppressor.
        private const int STASH_SIZE_MAX_AGE_SEC = 90;
        // Don't re-ask for a stash size more often than WorldMap would actually honour the request.
        private const int STASH_FETCH_RETRY_SEC = 60;
        // A send with no callback after this long is resolved against the army array instead.
        private const int PENDING_SEND_TIMEOUT_SEC = 30;
        // Don't retry a recall on the same army sooner than this. TryCancelStrandedScout consumes a
        // whole tick, so retrying a recall that keeps failing would starve the send loop outright.
        private const int CANCEL_RETRY_SEC = 120;

        // What we have already committed to taking out of a stash, measured against the size reading
        // in SizeAsOf. Unlike GetScoutsEnRoute this does NOT decay when a scout flips to its return
        // leg — that decay is what made the module re-send into a stash it had already cleared.
        private class StashClaim
        {
            public int      ConfirmedAmount;
            public DateTime LastSendTime;
            public DateTime SizeAsOf;
        }

        // A send handed to the server but not yet acknowledged. Counted as claimed straight away so
        // the send -> callback window can't leak a duplicate batch, then promoted to the confirmed
        // claim or rolled back once the callback tells us what actually happened.
        private class PendingSend
        {
            public int      StashId;
            public int      SourceVillageId;
            public int      Count;
            public int      CarryPerScout;
            public DateTime SentAt;
        }

        // Per-pass diagnostics, reset each OnTick and logged when a pass ends without doing anything.
        // This is the "why did nothing happen this cycle" breadcrumb.
        private int _diagCloser;
        private int _diagNotFirst;
        private int _diagAwaitingSize;
        private int _diagCovered;
        private int _diagNoSpace;
        private int _diagNoScouts;

        // Touched from both the bot tick thread and the RPC callback thread.
        private readonly object _ledgerLock = new object();
        private readonly Dictionary<int, StashClaim> _stashClaims = new Dictionary<int, StashClaim>();
        private readonly List<PendingSend> _pendingSends = new List<PendingSend>();
        private readonly Dictionary<int, DateTime> _lastSizeFetchAttempt = new Dictionary<int, DateTime>();
        private readonly Dictionary<long, DateTime> _cancelAttempts = new Dictionary<long, DateTime>();

        public override string ModuleName
        {
            get { return "Scout"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(3); }
        }

        private ScoutSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.Scout;
                return null;
            }
        }

        protected override void OnInitialize()
        {
            _lastSendTime = DateTime.MinValue;
            _lastCycleTime = DateTime.MinValue;
            _cycleComplete = true;
            // Safe to start cold: SendScout falls back to the live outbound army scan whenever that
            // exceeds the ledger, so anything already in flight is still counted.
            lock (_ledgerLock)
            {
                _stashClaims.Clear();
                _pendingSends.Clear();
                _lastSizeFetchAttempt.Clear();
                _cancelAttempts.Clear();
            }
        }

        protected override void OnTick()
        {
            ScoutSettings settings = Settings;
            if (settings == null) return;

            // Resolve sends the server never acknowledged before the ledger is used for decisions.
            SweepPendingSends();

            // Respect the delay between individual scout actions (send / fetch / cancel)
            if ((DateTime.Now - _lastSendTime).TotalMilliseconds < settings.DelayBetweenSendsMs)
                return;

            // Once the current cycle drains to nothing, wait the full interval before starting again
            if (_cycleComplete && (DateTime.Now - _lastCycleTime).TotalSeconds < settings.CycleIntervalSeconds)
                return;

            // Recall scouts stranded on a despawned stash — at most one cancel per tick.
            if (TryCancelStrandedScout())
                return;

            List<int> villages;
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
                // Capitals (parish/county/province/country) cannot scout, and getUserVillageIDList
                // already excludes them — unlike getUserVillageList. Filtering here also neutralises
                // any capital left enabled in a settings file written before this was filtered.
                villages = GameEngine.Instance.World.getUserVillageIDList();
            }
            catch { return; }

            if (villages == null) return;

            // Shuffle so we don't always favour the same village
            List<int> shuffled = new List<int>(villages);
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                int tmp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = tmp;
            }

            // At most one stash-size request per pass, so a long list of stale targets can't burst.
            bool sizeRequestIssued = false;

            // Villages that could genuinely take a new stash right now. Deferring to whichever
            // village is nearest is only safe among villages that can actually act on it.
            List<int> newStashClaimants = BuildNewStashClaimants(settings);
            ResetCycleDiagnostics();

            foreach (int villageId in shuffled)
            {
                VillageScoutSettings vs = settings.GetVillageSettings(villageId);
                if (!vs.ScoutingEnabled) continue;

                VillageMap village;
                try { village = GameEngine.Instance.getVillage(villageId); }
                catch { continue; }
                if (village == null) continue;

                if (settings.AutoHireScouts > 0)
                    TryHireScouts(village, settings.AutoHireScouts);

                if (GetAvailableScouts(village) <= 0) { _diagNoScouts++; continue; }

                List<StashTarget> targets = GetStashTargets(villageId, vs,
                    settings.MaxScoutTimeSeconds, settings.Priority, newStashClaimants);

                foreach (StashTarget target in targets)
                {
                    if (GetAvailableScouts(village) <= 0) break;

                    // Known-type stash whose size isn't cached, or is cached too long ago to trust.
                    // Fetch at most one per tick from the server (never burst), then process it on a
                    // later tick from cache. A stale-but-present target is skipped rather than
                    // consuming the whole tick, so a stash whose refresh never lands can't starve
                    // the send loop.
                    if (target.ResourceLevel == LEVEL_NEEDS_FETCH || target.ResourceLevel == LEVEL_STALE)
                    {
                        bool firstFetch = target.ResourceLevel == LEVEL_NEEDS_FETCH;
                        _diagAwaitingSize++;
                        if (sizeRequestIssued || !ShouldAttemptSizeFetch(target.StashId))
                            continue;

                        try { GameEngine.Instance.World.getSpecialVillageData(target.StashId, true); }
                        catch { continue; }
                        lock (_ledgerLock) { _lastSizeFetchAttempt[target.StashId] = DateTime.Now; }
                        sizeRequestIssued = true;
                        LogDebug((firstFetch ? "Fetching" : "Refreshing") + " stash size for "
                            + target.StashId + " (" + GetStashDisplayName(target.StashId) + ")");

                        if (!firstFetch) continue;

                        _lastSendTime = DateTime.Now;
                        _cycleComplete = false;
                        return;
                    }

                    if (SendScout(village, target, GetAvailableScouts(village), settings))
                    {
                        // One send per tick — come back next tick for the next target
                        _lastSendTime = DateTime.Now;
                        _cycleComplete = false;
                        return;
                    }
                }
            }

            // Iterated everything without taking an action — cycle is done
            SweepStashClaims(settings);
            LogCycleDiagnostics();
            _cycleComplete = true;
            _lastCycleTime = DateTime.Now;
            LogDebug("Scout cycle complete. Next cycle in " + settings.CycleIntervalSeconds + "s.");
        }

        // Scouts genuinely available to send from this village right now.
        // m_numScouts is scouts *at home* (traveling ones live in the army array), so we must NOT
        // subtract traveling scouts from it. We do cap by (research max - traveling) to guard the
        // race where a VillageSync refresh writes a pre-send m_numScouts back as stale-high.
        private static int GetAvailableScouts(VillageMap village)
        {
            int traveling = GetTravelingScoutsFromVillage(village.VillageID);
            int maxScouts = (int)GameEngine.Instance.World.UserResearchData.Research_Scouts;
            return Math.Min(village.m_numScouts, Math.Max(0, maxScouts - traveling));
        }

        private List<StashTarget> GetStashTargets(int villageId, VillageScoutSettings vs,
            int maxTimeSec, ScoutPriority priority, List<int> newStashClaimants)
        {
            List<StashTarget> result = new List<StashTarget>();

            VillageData[] worldVillages;
            try { worldVillages = GameEngine.Instance.World.getWorldVillageList(); }
            catch { return result; }
            if (worldVillages == null) return result;

            for (int i = 0; i < worldVillages.Length; i++)
            {
                VillageData vd = worldVillages[i];
                int special = vd.special;
                if (special < 100 || special > 133) continue;
                if (vs.ResourceTypesToIgnore.Contains(special)) continue;
                int typeIndex = vs.ResourceTypesToScout.IndexOf(special);
                if (typeIndex < 0) continue;

                double travelTime;
                try { travelTime = CalculateTravelTime(villageId, vd.id); }
                catch { continue; }
                if (travelTime > maxTimeSec) continue;

                int resourceLevel;
                DateTime sizeAsOf = DateTime.MinValue;
                if (special == 100)
                {
                    // New/undiscovered stash. Only the closest village that can actually take it
                    // should claim it, and only if we'd arrive before any scout already heading there.
                    if (IsAnotherVillageCloser(villageId, vd.id, travelTime, newStashClaimants))
                    {
                        _diagCloser++;
                        continue;
                    }
                    if (!IsFirstToStash(vd.id, travelTime))
                    {
                        _diagNotFirst++;
                        continue;
                    }
                    resourceLevel = LEVEL_NEW_STASH;
                }
                else
                {
                    // Known resource stash — read size from cache only (never fire a server
                    // request here). Uncached stashes are marked for a one-at-a-time fetch in
                    // OnTick; cached-but-old ones are marked for a refresh, because peek has no
                    // TTL and a size read once would otherwise never be updated again.
                    WorldMap.SpecialVillageCache svc;
                    try { svc = GameEngine.Instance.World.peekSpecialVillageData(vd.id); }
                    catch { continue; }

                    if (svc == null)
                    {
                        resourceLevel = LEVEL_NEEDS_FETCH;
                    }
                    else
                    {
                        SyncClaimToSizeReading(vd.id, special, svc);
                        sizeAsOf = svc.lastUpdate;
                        resourceLevel = (DateTime.Now - svc.lastUpdate).TotalSeconds > STASH_SIZE_MAX_AGE_SEC
                            ? LEVEL_STALE
                            : svc.resourceLevel;
                    }
                }

                result.Add(new StashTarget
                {
                    StashId    = vd.id,
                    TravelTime = travelTime,
                    ResourceType = special,
                    TypeIndex  = typeIndex,
                    ResourceLevel = resourceLevel,
                    SizeAsOf   = sizeAsOf
                });
            }

            if (priority == ScoutPriority.ResourcePriority)
            {
                result.Sort((a, b) =>
                {
                    int c = a.TypeIndex.CompareTo(b.TypeIndex);
                    return c != 0 ? c : a.TravelTime.CompareTo(b.TravelTime);
                });
            }
            else
            {
                result.Sort((a, b) => a.TravelTime.CompareTo(b.TravelTime));
            }

            return result;
        }

        // Villages eligible to claim a freshly-spawned stash right now: scouting on, new stashes in
        // their list, and scouts actually available. Built once per pass because GetAvailableScouts
        // scans the army array — evaluating it per candidate stash would be far too expensive.
        private List<int> BuildNewStashClaimants(ScoutSettings settings)
        {
            List<int> result = new List<int>();
            foreach (VillageScoutSettings vs in settings.Villages)
            {
                if (!vs.ScoutingEnabled) continue;
                if (!vs.ResourceTypesToScout.Contains(100)) continue;
                if (vs.ResourceTypesToIgnore.Contains(100)) continue;
                try
                {
                    if (!GameEngine.Instance.World.isUserVillage(vs.VillageId)) continue;
                    VillageMap v = GameEngine.Instance.getVillage(vs.VillageId);
                    if (v == null) continue;
                    if (GetAvailableScouts(v) <= 0) continue;
                }
                catch { continue; }
                result.Add(vs.VillageId);
            }
            return result;
        }

        // Is some other village that could actually take this new stash closer to it than we are?
        // Ensures only the nearest village claims a freshly-spawned stash — but ONLY among villages
        // that can act on it. Deferring on distance alone left a stash claimed by a village with no
        // scouts (or one that doesn't scout new stashes at all) and ignored by everyone else.
        private bool IsAnotherVillageCloser(int ourVillageId, int stashId, double ourTime,
            List<int> claimants)
        {
            if (claimants == null) return false;
            for (int i = 0; i < claimants.Count; i++)
            {
                int otherId = claimants[i];
                if (otherId == ourVillageId) continue;
                try
                {
                    if (CalculateTravelTime(otherId, stashId) < ourTime)
                        return true;
                }
                catch { }
            }
            return false;
        }

        // Would we reach this stash before any scout army already heading there?
        // Conservative: on repeated enumeration failure, assume we are not first.
        private bool IsFirstToStash(int stashId, double ourTravelTime)
        {
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    DateTime now = VillageMap.getCurrentServerTime();
                    foreach (object obj in GameEngine.Instance.World.getArmyArray())
                    {
                        WorldMap.LocalArmyData army = (WorldMap.LocalArmyData)obj;
                        if (army.dead) continue;
                        if (army.numScouts <= 0 || army.lootType >= 0) continue;
                        if (army.targetVillageID != stashId) continue;

                        double theirRemaining = (army.serverEndTime - now).TotalSeconds;
                        // Already arrived. The client only flips an army off the outbound state when
                        // fakeEndTime passes, not serverEndTime, so an arrived army can still read
                        // lootType < 0 — and its negative remaining is always "sooner than ours",
                        // which used to make this stash look permanently lost on every pass.
                        if (theirRemaining <= 0.0) continue;
                        if (theirRemaining < ourTravelTime)
                            return false;
                    }
                    return true;
                }
                catch (InvalidOperationException)
                {
                    attempts++;
                }
            }
            return false;
        }

        private static double CalculateTravelTime(int fromVillageId, int toVillageId)
        {
            double dist = GameEngine.Instance.World.getDistance(fromVillageId, toVillageId);
            WorldData wd = GameEngine.Instance.LocalWorldData;
            double t = dist * (wd.ScoutsMoveSpeed * wd.gamePlaySpeed
                       * ResearchData.ScoutTimes[(int)GameEngine.Instance.World.UserResearchData.Research_Horsemanship]);
            t = GameEngine.Instance.World.adjustIfIslandTravel(t, fromVillageId, toVillageId);
            t *= CardTypes.getScoutSpeed(GameEngine.Instance.cardsManager.UserCardData);
            return t;
        }

        // mirrors reference smethod_1: resources one scout can carry from this stash type
        private static int CalculateCarryPerScout(int stashType)
        {
            int resourceCarryLevel = GameEngine.Instance.LocalWorldData.ScoutResourceCarryLevel;
            int researchForaging = (int)GameEngine.Instance.World.UserResearchData.Research_Foraging;
            int carry = CardTypes.adjustForagingLevel(GameEngine.Instance.cardsManager.UserCardData,
                            resourceCarryLevel)
                        * ResearchData.foragingResearch[researchForaging] / 2;

            // Rare resource types (weapons, luxury goods) carry 1/10 the base amount
            switch (stashType)
            {
                case 119: case 121: case 122: case 123: case 124:
                case 125: case 126: case 128: case 129: case 130:
                case 131: case 132: case 133:
                    carry /= 10;
                    break;
            }
            return Math.Max(1, carry);
        }

        private static int CalculateOptimalScouts(int resourceLevel, int carryPerScout)
        {
            if (resourceLevel <= 0) return 1;
            int needed = resourceLevel / carryPerScout;
            if (resourceLevel % carryPerScout > 0) needed++;
            return Math.Max(1, needed);
        }

        // Storage cap for one resource type at this village (research cap × card multiplier).
        // buildingType is the 0-based resource id (stashType - 100). Returns 0 if it can't be computed.
        private static int GetResourceCap(VillageMap village, int stashType)
        {
            try
            {
                int buildingType = stashType - 100;
                double baseCap = GameEngine.Instance.World.UserResearchData.getResourceCap(
                    GameEngine.Instance.LocalWorldData, buildingType, false);
                double mult = CardTypes.getResourceCapMultiplier(buildingType,
                    GameEngine.Instance.cardsManager.UserCardData);
                return (int)(baseCap * mult);
            }
            catch
            {
                return 0;
            }
        }

        // Resources of a given type already foraged and currently returning to this village.
        // Mirrors the loot half of the reference CalcMovingScouts. lootType is the 100-based special.
        private static int GetIncomingLoot(int villageId, int lootType)
        {
            int total = 0;
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    foreach (object obj in GameEngine.Instance.World.getArmyArray())
                    {
                        WorldMap.LocalArmyData army = (WorldMap.LocalArmyData)obj;
                        if (army.homeVillageID == villageId
                            && army.lootType == lootType
                            && army.lootLevel > 0.0)
                        {
                            total += (int)army.lootLevel;
                        }
                    }
                    break;
                }
                catch (InvalidOperationException)
                {
                    total = 0;
                    attempts++;
                }
            }
            return total;
        }

        // Count ALL scouts owned by a village (home + traveling). Traveling scouts live in the
        // army array; m_numScouts is home only. Used to size availability against the research cap.
        private static int GetTravelingScoutsFromVillage(int villageId)
        {
            int total = 0;
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    foreach (object obj in GameEngine.Instance.World.getArmyArray())
                    {
                        WorldMap.LocalArmyData army = (WorldMap.LocalArmyData)obj;
                        if (army.numScouts > 0 && army.homeVillageID == villageId)
                            total += army.numScouts;
                    }
                    break;
                }
                catch (InvalidOperationException)
                {
                    total = 0;
                    attempts++;
                }
            }
            return total;
        }

        // Count scouts from any user village already en route to this stash (outbound only)
        private static int GetScoutsEnRoute(int stashId)
        {
            int total = 0;
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    foreach (object obj in GameEngine.Instance.World.getArmyArray())
                    {
                        WorldMap.LocalArmyData army = (WorldMap.LocalArmyData)obj;
                        if (army.numScouts > 0
                            && army.lootType < 0   // outbound, not returning with loot
                            && army.targetVillageID == stashId
                            && GameEngine.Instance.World.isUserVillage(army.travelFromVillageID))
                        {
                            total += army.numScouts;
                        }
                    }
                    break;
                }
                catch (InvalidOperationException)
                {
                    total = 0;
                    attempts++;
                }
            }
            return total;
        }

        // ---------------------------------------------------------------------------------------
        // Dispatch ledger
        //
        // GetScoutsEnRoute alone is not enough to decide whether a stash is covered: it only counts
        // OUTBOUND armies, so a scout's claim vanishes the moment it picks up loot and turns for
        // home, while the cached stash size still reads full. The ledger records what we committed
        // at dispatch time and keeps it until a fresh server reading of the stash supersedes it.
        // ---------------------------------------------------------------------------------------

        // Everything committed to this stash since its last size reading: confirmed sends plus any
        // still waiting on a callback.
        private int GetClaimedAmount(int stashId)
        {
            lock (_ledgerLock)
            {
                int total = 0;
                StashClaim claim;
                if (_stashClaims.TryGetValue(stashId, out claim))
                    total = claim.ConfirmedAmount;
                for (int i = 0; i < _pendingSends.Count; i++)
                {
                    if (_pendingSends[i].StashId == stashId)
                        total += _pendingSends[i].Count * _pendingSends[i].CarryPerScout;
                }
                return total;
            }
        }

        // Any send to this stash still waiting on its callback. Used by the new-stash path, where
        // the claimed amount is always 0 because the stash size isn't known yet.
        private bool HasPendingSend(int stashId)
        {
            lock (_ledgerLock)
            {
                for (int i = 0; i < _pendingSends.Count; i++)
                {
                    if (_pendingSends[i].StashId == stashId) return true;
                }
                return false;
            }
        }

        // Caller must hold _ledgerLock.
        private StashClaim GetOrCreateClaim(int stashId)
        {
            StashClaim claim;
            if (!_stashClaims.TryGetValue(stashId, out claim))
            {
                claim = new StashClaim();
                _stashClaims[stashId] = claim;
            }
            return claim;
        }

        private bool RecentlyAttemptedCancel(long armyId)
        {
            lock (_ledgerLock)
            {
                DateTime last;
                if (!_cancelAttempts.TryGetValue(armyId, out last)) return false;
                return (DateTime.Now - last).TotalSeconds < CANCEL_RETRY_SEC;
            }
        }

        private bool ShouldAttemptSizeFetch(int stashId)
        {
            lock (_ledgerLock)
            {
                DateTime last;
                if (!_lastSizeFetchAttempt.TryGetValue(stashId, out last)) return true;
                return (DateTime.Now - last).TotalSeconds >= STASH_FETCH_RETRY_SEC;
            }
        }

        // A fresh server reading already accounts for every scout that has taken its loot, including
        // ones currently flying home. The only claim still outstanding against it is from scouts that
        // haven't arrived yet — which is exactly what the outbound scan measures. This is the backstop
        // that heals any mis-accounting: a claim can't stay wrong for longer than one refresh.
        private void SyncClaimToSizeReading(int stashId, int stashType, WorldMap.SpecialVillageCache svc)
        {
            lock (_ledgerLock)
            {
                StashClaim claim;
                if (!_stashClaims.TryGetValue(stashId, out claim)) return;
                if (svc.lastUpdate <= claim.SizeAsOf) return;

                int carryPerScout;
                try { carryPerScout = CalculateCarryPerScout(stashType); }
                catch { return; }

                claim.ConfirmedAmount = GetScoutsEnRoute(stashId) * carryPerScout;
                claim.SizeAsOf = svc.lastUpdate;
            }
        }

        // set_SendScouts_UserCallBack is a global slot that ScoutPopupPanel claims too, so a manual
        // scout can take it from us and our callback never arrives. Resolve those against the army
        // array rather than guessing: if the army exists the send happened, otherwise it didn't.
        private void SweepPendingSends()
        {
            List<PendingSend> expired = null;
            lock (_ledgerLock)
            {
                if (_pendingSends.Count == 0) return;
                DateTime cutoff = DateTime.Now.AddSeconds(-PENDING_SEND_TIMEOUT_SEC);
                for (int i = _pendingSends.Count - 1; i >= 0; i--)
                {
                    if (_pendingSends[i].SentAt > cutoff) continue;
                    if (expired == null) expired = new List<PendingSend>();
                    expired.Add(_pendingSends[i]);
                    _pendingSends.RemoveAt(i);
                }
            }
            if (expired == null) return;

            foreach (PendingSend p in expired)
            {
                if (HasOutboundScouts(p.SourceVillageId, p.StashId))
                {
                    lock (_ledgerLock)
                    {
                        StashClaim claim = GetOrCreateClaim(p.StashId);
                        claim.ConfirmedAmount += p.Count * p.CarryPerScout;
                    }
                    LogDebug("Unacknowledged send to " + p.StashId + " confirmed from army data ("
                        + p.Count + " scout(s))");
                }
                else
                {
                    LogDebug("Unacknowledged send to " + p.StashId + " dropped, no matching army ("
                        + p.Count + " scout(s))");
                }
            }
        }

        // Housekeeping only — the size-reading sync above is what carries correctness. Note there is
        // deliberately no timed blanket clear: wiping a claim while its scouts are on the return leg
        // would lose the only record of it (the army scan can't see them) and we'd re-send.
        private void SweepStashClaims(ScoutSettings settings)
        {
            List<int> dead = new List<int>();
            lock (_ledgerLock)
            {
                // Full round trip plus a margin. A stash at the edge of the travel budget is
                // MaxScoutTimeSeconds out and the same back, so anything shorter can evict a live
                // claim mid-return.
                double idleSec = Math.Max(600.0, settings.MaxScoutTimeSeconds * 2.0 + 300.0);

                foreach (KeyValuePair<int, StashClaim> kv in _stashClaims)
                {
                    int stashId = kv.Key;

                    bool pending = false;
                    for (int i = 0; i < _pendingSends.Count; i++)
                    {
                        if (_pendingSends[i].StashId == stashId) { pending = true; break; }
                    }
                    if (pending) continue;

                    // Despawned, or no longer a stash — a fully looted stash disappears.
                    if (!IsLiveStash(stashId)) { dead.Add(stashId); continue; }

                    if (GetScoutsEnRoute(stashId) > 0) continue;

                    WorldMap.SpecialVillageCache svc = null;
                    try { svc = GameEngine.Instance.World.peekSpecialVillageData(stashId); }
                    catch { }
                    if (svc != null && svc.resourceLevel <= 0) { dead.Add(stashId); continue; }

                    if ((DateTime.Now - kv.Value.LastSendTime).TotalSeconds > idleSec)
                        dead.Add(stashId);
                }
                foreach (int id in dead) _stashClaims.Remove(id);

                // These only exist to suppress a re-request for STASH_FETCH_RETRY_SEC, so anything
                // older has served its purpose. Keeps the map to stashes touched in the last minute
                // rather than every stash seen this session.
                List<int> staleAttempts = null;
                foreach (KeyValuePair<int, DateTime> kv in _lastSizeFetchAttempt)
                {
                    if ((DateTime.Now - kv.Value).TotalSeconds > STASH_FETCH_RETRY_SEC)
                    {
                        if (staleAttempts == null) staleAttempts = new List<int>();
                        staleAttempts.Add(kv.Key);
                    }
                }
                if (staleAttempts != null)
                {
                    foreach (int id in staleAttempts) _lastSizeFetchAttempt.Remove(id);
                }

                List<long> staleCancels = null;
                foreach (KeyValuePair<long, DateTime> kv in _cancelAttempts)
                {
                    if ((DateTime.Now - kv.Value).TotalSeconds > CANCEL_RETRY_SEC)
                    {
                        if (staleCancels == null) staleCancels = new List<long>();
                        staleCancels.Add(kv.Key);
                    }
                }
                if (staleCancels != null)
                {
                    foreach (long id in staleCancels) _cancelAttempts.Remove(id);
                }
            }

            if (dead.Count > 0)
                LogDebug("Released stash claims: " + dead.Count);
        }

        private void ResetCycleDiagnostics()
        {
            _diagCloser = 0;
            _diagNotFirst = 0;
            _diagAwaitingSize = 0;
            _diagCovered = 0;
            _diagNoSpace = 0;
            _diagNoScouts = 0;
        }

        // Logged only when a full pass ends without sending anything — i.e. exactly when you want to
        // know why. Each counter is a reason a candidate was rejected during that pass.
        private void LogCycleDiagnostics()
        {
            if (_diagCloser == 0 && _diagNotFirst == 0 && _diagAwaitingSize == 0
                && _diagCovered == 0 && _diagNoSpace == 0 && _diagNoScouts == 0)
                return;

            LogDebug(string.Format(
                "Cycle idle — newStashCloserVillage={0}, newStashNotFirst={1}, awaitingSize={2}, "
                + "alreadyCovered={3}, noStorageSpace={4}, villagesWithoutScouts={5}",
                _diagCloser, _diagNotFirst, _diagAwaitingSize,
                _diagCovered, _diagNoSpace, _diagNoScouts));
        }

        private static bool IsLiveStash(int stashId)
        {
            try
            {
                if (!GameEngine.Instance.World.isVillageVisible(stashId)) return false;
                int special = GameEngine.Instance.World.getSpecial(stashId);
                return special >= 100 && special <= 133;
            }
            catch { return false; }
        }

        // Does an outbound scout army from this village to this stash exist right now?
        private static bool HasOutboundScouts(int fromVillageId, int stashId)
        {
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    foreach (object obj in GameEngine.Instance.World.getArmyArray())
                    {
                        WorldMap.LocalArmyData army = (WorldMap.LocalArmyData)obj;
                        if (army.numScouts > 0
                            && army.lootType < 0
                            && army.targetVillageID == stashId
                            && army.travelFromVillageID == fromVillageId)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                catch (InvalidOperationException)
                {
                    attempts++;
                }
            }
            return false;
        }

        // Loot this village's own scouts are still going out to fetch, for one resource type.
        // GetScoutsEnRoute is per-stash across every village, so it both counts other villages'
        // scouts against our store and misses our own scouts heading to other stashes of this type.
        private static int GetOutboundClaimForVillage(int villageId, int stashType, int carryPerScout)
        {
            int scouts = 0;
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    scouts = 0;
                    foreach (object obj in GameEngine.Instance.World.getArmyArray())
                    {
                        WorldMap.LocalArmyData army = (WorldMap.LocalArmyData)obj;
                        if (army.numScouts <= 0) continue;
                        if (army.lootType >= 0) continue;   // returning — counted by GetIncomingLoot
                        if (army.homeVillageID != villageId) continue;

                        int special;
                        try { special = GameEngine.Instance.World.getSpecial(army.targetVillageID); }
                        catch { continue; }
                        if (special == stashType) scouts += army.numScouts;
                    }
                    break;
                }
                catch (InvalidOperationException)
                {
                    scouts = 0;
                    attempts++;
                }
            }
            return scouts * carryPerScout;
        }

        // Find one scout army stranded on a stash that has despawned (no longer in our map data)
        // and recall it. At most one cancel per tick to keep request pacing consistent.
        private bool TryCancelStrandedScout()
        {
            long armyId = -1;
            int targetId = -1;
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    DateTime now = VillageMap.getCurrentServerTime();
                    foreach (object obj in GameEngine.Instance.World.getArmyArray())
                    {
                        WorldMap.LocalArmyData army = (WorldMap.LocalArmyData)obj;
                        if (!army.dead
                            && army.lootType < 0          // outbound, hasn't foraged yet
                            && army.isScouts()
                            && GameEngine.Instance.World.isUserVillage(army.homeVillageID)
                            // Our own villages are marked visible as soon as the server's owner list
                            // lands. If the home village reads invisible the list simply hasn't loaded
                            // yet, and every target would look despawned — don't recall the world.
                            && GameEngine.Instance.World.isVillageVisible(army.homeVillageID)
                            && !GameEngine.Instance.World.isVillageVisible(army.targetVillageID)
                            && (army.serverEndTime - now).TotalSeconds > 15.0
                            && !RecentlyAttemptedCancel(army.armyID))
                        {
                            armyId = army.armyID;
                            targetId = army.targetVillageID;
                            break;
                        }
                    }
                    break;
                }
                catch (InvalidOperationException)
                {
                    armyId = -1;
                    attempts++;
                }
            }

            if (armyId < 0) return false;

            try
            {
                RemoteServices.Instance.set_CancelCastleAttack_UserCallBack(OnCancelScoutsCallback);
                RemoteServices.Instance.CancelCastleAttack(armyId);
                // Recorded before we know the outcome. A recall that fails — or whose global callback
                // slot gets stolen, so deleteArmy never runs — would otherwise be retried on every
                // tick forever, and since this runs ahead of the send loop the module would never
                // send another scout until a relog.
                lock (_ledgerLock) { _cancelAttempts[armyId] = DateTime.Now; }
                LogDebug("Recalling scouts from despawned stash " + targetId
                    + " (" + GetStashDisplayName(targetId) + ")");
                _lastSendTime = DateTime.Now;
                _cycleComplete = false;
                return true;
            }
            catch (Exception ex)
            {
                LogError("CancelStrandedScout error: " + ex.Message);
                return false;
            }
        }

        private void TryHireScouts(VillageMap village, int targetCount)
        {
            try
            {
                byte researchScouts = GameEngine.Instance.World.UserResearchData.Research_Scouts;
                int effectiveTarget = Math.Min(targetCount, (int)researchScouts);
                int current = village.calcTotalScouts() + village.LocallyMade_Scouts;
                if (current >= effectiveTarget) return;

                int toHire = effectiveTarget - current;

                int spareWorkers = village.m_spareWorkers;
                if (toHire > spareWorkers) toHire = spareWorkers;
                if (toHire <= 0) return;

                int unitUsed = village.calcUnitUsages()
                    + village.LocallyMade_Scouts * GameEngine.Instance.LocalWorldData.UnitSize_Scout;
                int unitSpace = GameEngine.Instance.LocalWorldData.Village_UnitCapacity - unitUsed;
                int unitSizeScout = GameEngine.Instance.LocalWorldData.UnitSize_Scout;
                if (unitSpace < toHire * unitSizeScout)
                    toHire = unitSpace / unitSizeScout;
                if (toHire <= 0) return;

                int gold = (int)GameEngine.Instance.World.getCurrentGold();
                int goldCost = GameEngine.Instance.LocalWorldData.ScoutGoldCost;
                if (gold < toHire * goldCost)
                    toHire = gold / goldCost;
                if (toHire <= 0) return;

                int amount = Math.Min(toHire, 4);
                LogDebug(string.Format("{0}: hiring {1} scout(s)",
                    GameEngine.Instance.World.getVillageName(village.VillageID), amount));
                village.makeTroops(76, amount, false);
            }
            catch (Exception ex)
            {
                LogError("TryHireScouts: " + ex.Message);
            }
        }

        private bool SendScout(VillageMap village, StashTarget target, int availableScouts,
            ScoutSettings settings)
        {
            if (availableScouts <= 0) return false;

            int enRoute = GetScoutsEnRoute(target.StashId);
            int count;
            int carryPerScout = 0;
            int claimed = 0;
            int remaining = 0;

            if (target.ResourceType == 100)
            {
                // New/undiscovered stash — size unknown until scouted
                if (enRoute > 0) return false;                    // already being discovered
                if (HasPendingSend(target.StashId)) return false; // sent, callback not back yet
                count = settings.SendOneOnNewStash ? 1 : Math.Min(2, availableScouts);
            }
            else
            {
                try { carryPerScout = CalculateCarryPerScout(target.ResourceType); }
                catch { return false; }

                // Everything committed since the last size reading, whether those scouts are still
                // outbound, foraging, or already carrying loot home. Falling back to the live
                // outbound scan keeps us honest when the ledger is cold (e.g. bot restarted with
                // scouts already in flight).
                claimed = Math.Max(GetClaimedAmount(target.StashId), enRoute * carryPerScout);
                remaining = target.ResourceLevel - claimed;

                if (target.ResourceLevel <= 0) return false;
                if (remaining <= 0)
                {
                    _diagCovered++;
                    LogDebug(string.Format("{0} ({1}) already covered: size {2}, claimed {3}",
                        target.StashId, GetStashDisplayName(target.StashId),
                        target.ResourceLevel, claimed));
                    return false;
                }

                // Respect village storage capacity if enabled — don't forage into a near-full store.
                int maxBySpace = int.MaxValue;
                if (settings.WaitForFreeSpace)
                {
                    int cap = GetResourceCap(village, target.ResourceType);
                    if (cap > 0)
                    {
                        int level = (int)village.getResourceLevel(target.ResourceType - 100);
                        int incoming = GetIncomingLoot(village.VillageID, target.ResourceType);
                        int outbound = GetOutboundClaimForVillage(village.VillageID,
                            target.ResourceType, carryPerScout);
                        int freeSpace = cap - level - incoming - outbound;
                        // Effectively full: bail only when free space can't fit a scout's carry AND
                        // is under 10% of cap (the cap/10 floor lets small-cap stores still top off).
                        if (freeSpace < carryPerScout && freeSpace < cap / 10)
                        {
                            _diagNoSpace++;
                            return false;
                        }
                        maxBySpace = Math.Max(1, freeSpace / carryPerScout);
                    }
                }

                if (settings.SendOneScout)
                {
                    // Single-scout mode: drip-feed 1 scout at a time until stash/space is covered
                    count = 1;
                }
                else
                {
                    count = CalculateOptimalScouts(remaining, carryPerScout);
                    if (maxBySpace != int.MaxValue)
                        count = Math.Min(count, maxBySpace);
                }
                count = Math.Min(count, availableScouts);
            }

            if (count <= 0) return false;

            try
            {
                RemoteServices.Instance.set_SendScouts_UserCallBack(OnSendScoutsCallback);
                RemoteServices.Instance.SendScouts(village.VillageID, target.StashId, count);

                // Claim the stash immediately. SendScouts is async, so the army won't appear in the
                // game's army array until OnSendScoutsCallback lands — without this the next tick
                // would re-evaluate the stash as untouched and send again. Rolled back in the
                // callback if the send turns out to have failed.
                lock (_ledgerLock)
                {
                    StashClaim claim = GetOrCreateClaim(target.StashId);
                    claim.LastSendTime = DateTime.Now;
                    if (target.SizeAsOf > claim.SizeAsOf) claim.SizeAsOf = target.SizeAsOf;
                    _pendingSends.Add(new PendingSend
                    {
                        StashId = target.StashId,
                        SourceVillageId = village.VillageID,
                        Count = count,
                        CarryPerScout = carryPerScout,
                        SentAt = DateTime.Now
                    });
                }

                village.addTroops(0, 0, 0, 0, 0, -count);
                AllVillagesPanel.travellersChanged();
                LogDebug(string.Format(
                    "{0}: sent {1} scout(s) to {2} ({3}) [size {4}, claimed {5}, remaining {6}, enRoute {7}]",
                    GameEngine.Instance.World.getVillageName(village.VillageID),
                    count, target.StashId, GetStashDisplayName(target.StashId),
                    target.ResourceLevel, claimed, remaining, enRoute));
                return true;
            }
            catch (Exception ex)
            {
                LogError("SendScout error: " + ex.Message);
                return false;
            }
        }

        private void OnSendScoutsCallback(SendScouts_ReturnType ret)
        {
            try
            {
                // May be someone else's result — set_SendScouts_UserCallBack is a global slot that
                // ScoutPopupPanel claims too. ResolvePendingSend leaves the ledger alone if nothing
                // in it matches.
                ResolvePendingSend(ret);

                if (ret.Success)
                {
                    ArmyReturnData[] data = new ArmyReturnData[] { ret.armyData };
                    GameEngine.Instance.World.doGetArmyData(
                        (IEnumerable<ArmyReturnData>)data,
                        (IEnumerable<ArmyReturnData>)null, false);
                    GameEngine.Instance.World.addExistingArmy(ret.armyData.armyID);
                    if (ret.cardData != null)
                        GameEngine.Instance.cardsManager.UserCardData = ret.cardData;
                }
                else if (ret.m_errorCode == ErrorCodes.ErrorCode.ATTACKING_VILLAGE_INTERDICT_PROTECTED)
                {
                    LogWarning("Cannot scout from interdicted village: " + ret.sourceVillage);
                }
                else
                {
                    LogError("SendScouts failed: "
                        + ErrorCodes.getErrorString(ret.m_errorCode, ret.m_errorID)
                        + " | from " + GameEngine.Instance.World.getVillageName(ret.sourceVillage)
                        + " to " + GameEngine.Instance.World.getVillageNameOrType(ret.targetVillage));
                }

                if (ret.numScoutsNotTaken > 0)
                {
                    VillageMap v = GameEngine.Instance.getVillage(ret.sourceVillage);
                    if (v != null) v.addTroops(0, 0, 0, 0, 0, ret.numScoutsNotTaken);
                }
            }
            catch (Exception ex)
            {
                LogError("SendScouts callback error: " + ex.Message);
            }
        }

        // Promote the optimistic claim recorded at dispatch, or roll it back. Without the rollback a
        // failed send (interdicted source, server error) would leave a phantom claim on the stash and
        // suppress it until the next size refresh.
        private void ResolvePendingSend(SendScouts_ReturnType ret)
        {
            PendingSend p = null;
            lock (_ledgerLock)
            {
                for (int i = 0; i < _pendingSends.Count; i++)
                {
                    // List is append-ordered, so the first match is the oldest.
                    if (_pendingSends[i].StashId == ret.targetVillage
                        && _pendingSends[i].SourceVillageId == ret.sourceVillage)
                    {
                        p = _pendingSends[i];
                        _pendingSends.RemoveAt(i);
                        break;
                    }
                }
            }
            if (p == null) return;

            if (!ret.Success)
            {
                LogDebug("Send to " + p.StashId + " failed, claim rolled back ("
                    + p.Count + " scout(s))");
                return;
            }

            // numScoutsNotTaken can be non-zero on success too — a partial send, whose troops are
            // refunded to the village below, so they must not stay claimed against the stash either.
            int actuallySent = p.Count - ret.numScoutsNotTaken;
            if (actuallySent <= 0)
            {
                LogDebug("Send to " + p.StashId + " took no scouts, claim rolled back");
                return;
            }
            if (actuallySent != p.Count)
            {
                LogDebug("Partial send to " + p.StashId + ": " + actuallySent
                    + " of " + p.Count + " scout(s)");
            }

            lock (_ledgerLock)
            {
                StashClaim claim = GetOrCreateClaim(p.StashId);
                claim.ConfirmedAmount += actuallySent * p.CarryPerScout;
            }
        }

        private void OnCancelScoutsCallback(CancelCastleAttack_ReturnType ret)
        {
            try
            {
                if (ret.Success)
                {
                    if (ret.armyData == null) return;
                    ArmyReturnData[] data = new ArmyReturnData[] { ret.armyData };
                    GameEngine.Instance.World.doGetArmyData(
                        (IEnumerable<ArmyReturnData>)data,
                        (IEnumerable<ArmyReturnData>)null, false);
                    GameEngine.Instance.World.addExistingArmy(ret.armyData.armyID);
                    GameEngine.Instance.World.deleteArmy(ret.oldArmyID);
                }
                else
                {
                    LogError("CancelScouts failed: "
                        + ErrorCodes.getErrorString(ret.m_errorCode, ret.m_errorID));
                }
            }
            catch (Exception ex)
            {
                LogError("CancelScouts callback error: " + ex.Message);
            }
        }

        private static string GetStashDisplayName(int stashId)
        {
            try
            {
                int special = GameEngine.Instance.World.getSpecial(stashId);
                if (special == 100) return "New Stash";
                if (special > 100 && special <= 133)
                    return VillageBuildingsData.getResourceNames(special - 100);
            }
            catch { }
            return "Stash " + stashId;
        }

        private struct StashTarget
        {
            public int StashId;
            public double TravelTime;
            public int ResourceType;
            public int TypeIndex;
            public int ResourceLevel; // -1 = new stash (type 100); -2 = size not cached; -3 = cached size stale; >=0 known
            public DateTime SizeAsOf; // lastUpdate of the cache entry ResourceLevel came from
        }
    }
}
