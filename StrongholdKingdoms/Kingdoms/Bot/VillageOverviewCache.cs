using System;
using System.Collections;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot
{
    /// <summary>
    /// The Village Overview's memory of the player's villages, and the code that turns it
    /// into grid rows.
    ///
    /// Three sources, best last so it wins:
    ///  - PremiumOverview: one RPC for every village, only sent while a premium token is in
    ///    play (exactly as the game's own overview screen does).
    ///  - Snapshots: VillageBuildingChangeRates replies, one village each. Collected
    ///    passively from every reply VillageResourceRouter sees (Village Sync's background
    ///    refresh, the game's own stats refreshes, the Village Info window), and asked for
    ///    while the overview is open for villages the client never downloaded.
    ///  - The downloaded VillageMap itself, which the game and Village Sync keep current.
    ///
    /// Everything here runs on the UI thread - router replies and RPC callbacks are
    /// dispatched from the main loop, and the overview drives the scheduler from a WinForms
    /// timer - so nothing is marshalled. The lock only guards against a bot module touching
    /// the cache from its own thread in future.
    /// </summary>
    internal static class VillageOverviewCache
    {
        private class Snapshot
        {
            public VillageResourceAndStatsReturnData Stats;
            public DateTime Time;
        }

        private class Pending
        {
            public DateTime SentAt;
            public Action<VillageBuildingChangeRates_ReturnType> Handler;
        }

        private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(60);
        private static readonly TimeSpan PremiumMinInterval = TimeSpan.FromSeconds(30);

        private static readonly object _lock = new object();
        private static readonly Dictionary<int, Snapshot> _snapshots = new Dictionary<int, Snapshot>();
        private static readonly Dictionary<int, Pending> _pending = new Dictionary<int, Pending>();
        // Local time each village was last asked for, so failures back off.
        private static readonly Dictionary<int, DateTime> _lastAttempt = new Dictionary<int, DateTime>();
        private static bool _subscribed;

        private static Dictionary<int, VillageSummaryData> _premiumSummary = new Dictionary<int, VillageSummaryData>();
        private static Dictionary<int, VillageResourceReturnData> _premiumResources = new Dictionary<int, VillageResourceReturnData>();
        private static DateTime _premiumTime = DateTime.MinValue;   // server time of the last batch
        private static DateTime _premiumAskedAt = DateTime.MinValue; // local time it was asked for
        private static bool _premiumPending;

        public static DateTime PremiumTime
        {
            get { lock (_lock) return _premiumTime; }
        }

        public static int PendingCount
        {
            get { lock (_lock) return _pending.Count; }
        }

        /// <summary>
        /// Forgets everything and (re)starts listening to the router. Called on every world
        /// start, where every village id held here is about to mean something else.
        /// </summary>
        public static void Reset()
        {
            lock (_lock)
            {
                _snapshots.Clear();
                _pending.Clear();
                _lastAttempt.Clear();
                _premiumSummary = new Dictionary<int, VillageSummaryData>();
                _premiumResources = new Dictionary<int, VillageResourceReturnData>();
                _premiumTime = DateTime.MinValue;
                _premiumAskedAt = DateTime.MinValue;
                _premiumPending = false;
            }

            if (!_subscribed)
            {
                VillageResourceRouter.ReplyObserved += OnRouterReply;
                _subscribed = true;
            }
        }

        public static bool IsPending(int villageId)
        {
            lock (_lock) return _pending.ContainsKey(villageId);
        }

        // =================================================================
        // Collecting replies
        // =================================================================

        private static void OnRouterReply(VillageBuildingChangeRates_ReturnType data)
        {
            if (data == null || data.villageResourcesAndStats == null || data.villageID < 0) return;

            try
            {
                // The Village Info window reads other players' villages through the same
                // router; only the player's own belong in the overview.
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
                if (!GameEngine.Instance.World.isUserVillage(data.villageID)) return;
            }
            catch (Exception)
            {
                return;
            }

            lock (_lock)
            {
                Snapshot snapshot = new Snapshot();
                snapshot.Stats = data.villageResourcesAndStats;
                snapshot.Time = data.currentTime;
                _snapshots[data.villageID] = snapshot;
            }
        }

        private static void OnRequestDone(int villageId, VillageBuildingChangeRates_ReturnType data)
        {
            // The data itself already arrived through OnRouterReply; this only frees the slot.
            lock (_lock)
            {
                _pending.Remove(villageId);
            }
        }

        // =================================================================
        // Asking for data
        // =================================================================

        /// <summary>
        /// Sends at most one request for the stalest village the client hasn't downloaded
        /// whose snapshot is older than staleBefore (server time), keeping no more than
        /// maxInFlight of the overview's own requests out at once. Downloaded villages are
        /// skipped: the game and Village Sync keep those current, and a stats reply for one
        /// that isn't on screen pushes its numbers into the village panel. Returns how many
        /// villages are still waiting for a request, so the caller can tell when a sweep is
        /// done. Call from the UI thread.
        /// </summary>
        public static int PumpRequests(IList<int> villageIds, DateTime staleBefore, int maxInFlight)
        {
            if (villageIds == null || villageIds.Count == 0) return 0;
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return 0;

            DateTime nowLocal = DateTime.Now;
            List<Pending> expired = null;
            List<int> expiredIds = null;

            int best = -1;
            int waiting = 0;
            DateTime bestTime = DateTime.MaxValue;

            lock (_lock)
            {
                foreach (KeyValuePair<int, Pending> pair in _pending)
                {
                    if (nowLocal - pair.Value.SentAt > RequestTimeout)
                    {
                        if (expired == null) { expired = new List<Pending>(); expiredIds = new List<int>(); }
                        expired.Add(pair.Value);
                        expiredIds.Add(pair.Key);
                    }
                }

                if (expiredIds != null)
                {
                    for (int i = 0; i < expiredIds.Count; i++)
                        _pending.Remove(expiredIds[i]);
                }

                for (int i = 0; i < villageIds.Count; i++)
                {
                    int id = villageIds[i];
                    if (_pending.ContainsKey(id)) continue;
                    if (GameEngine.Instance.getVillage(id) != null) continue;

                    Snapshot snapshot;
                    DateTime have = _snapshots.TryGetValue(id, out snapshot) ? snapshot.Time : DateTime.MinValue;
                    if (have != DateTime.MinValue && have >= staleBefore) continue;

                    // A village whose last request came back empty or timed out waits a
                    // while before the next try, rather than being asked for every tick.
                    DateTime attempted;
                    if (_lastAttempt.TryGetValue(id, out attempted) && nowLocal - attempted < RetryDelay)
                        continue;

                    waiting++;
                    if (have < bestTime)
                    {
                        bestTime = have;
                        best = id;
                    }
                }

                if (_pending.Count >= maxInFlight) best = -1;
            }

            if (expired != null)
            {
                for (int i = 0; i < expired.Count; i++)
                    VillageResourceRouter.Cancel(expiredIds[i], expired[i].Handler);
            }

            if (best >= 0)
            {
                Request(best);
                waiting--;
            }

            return waiting;
        }

        /// <summary>Asks for one village now, whether or not its snapshot is stale.</summary>
        public static void Request(int villageId)
        {
            Pending pending = new Pending();
            pending.SentAt = DateTime.Now;
            int id = villageId;
            pending.Handler = delegate(VillageBuildingChangeRates_ReturnType data) { OnRequestDone(id, data); };

            lock (_lock)
            {
                if (_pending.ContainsKey(villageId)) return;
                _pending[villageId] = pending;
                _lastAttempt[villageId] = pending.SentAt;
            }

            // Our own villages only, so always the harmless "change nothing" form - never
            // RequestForeign, whose 1,1,1,1 would really be applied here.
            VillageResourceRouter.Request(villageId, pending.Handler);
        }

        /// <summary>Drops every outstanding request - used when the window closes.</summary>
        public static void CancelAll()
        {
            List<KeyValuePair<int, Pending>> all;
            lock (_lock)
            {
                all = new List<KeyValuePair<int, Pending>>(_pending);
                _pending.Clear();
                _premiumPending = false;
            }

            for (int i = 0; i < all.Count; i++)
                VillageResourceRouter.Cancel(all[i].Key, all[i].Value.Handler);
        }

        /// <summary>
        /// Asks for the premium batch if a token is in play and the last one is older than
        /// minInterval (never more often than every 30s, the game's own limit).
        /// </summary>
        public static void PumpPremium(TimeSpan minInterval, bool force)
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
                if (!GameEngine.Instance.World.isAccountPremium()) return;

                // The game's own overview screen sets the same single callback slot before
                // each of its calls. Asking while it's on screen could take its reply, so
                // leave it alone - it is showing the same data anyway.
                if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_ALL_VILLAGES) return;

                if (minInterval < PremiumMinInterval) minInterval = PremiumMinInterval;
                DateTime now = DateTime.Now;

                lock (_lock)
                {
                    if (_premiumPending)
                    {
                        // Our reply went to someone else's handler, or never came.
                        if (now - _premiumAskedAt < RequestTimeout) return;
                        _premiumPending = false;
                    }

                    TimeSpan since = now - _premiumAskedAt;
                    if (since < PremiumMinInterval) return;
                    if (!force && since < minInterval) return;

                    _premiumPending = true;
                    _premiumAskedAt = now;
                }

                RemoteServices.Instance.set_PremiumOverview_UserCallBack(
                    new RemoteServices.PremiumOverview_UserCallBack(OnPremiumOverview));
                RemoteServices.Instance.PremiumOverview();
            }
            catch (Exception ex)
            {
                lock (_lock) _premiumPending = false;
                UniversalDebugLog.Log("VillageOverviewCache: premium request failed: " + ex.Message);
            }
        }

        private static void OnPremiumOverview(PremiumOverview_ReturnType returnData)
        {
            Dictionary<int, VillageSummaryData> summary = null;
            Dictionary<int, VillageResourceReturnData> resources = null;

            if (returnData != null && returnData.Success && returnData.summaryData != null)
            {
                summary = new Dictionary<int, VillageSummaryData>();
                foreach (VillageSummaryData data in returnData.summaryData)
                {
                    if (data != null) summary[data.villageID] = data;
                }

                resources = new Dictionary<int, VillageResourceReturnData>();
                if (returnData.resourceData != null)
                {
                    foreach (VillageResourceReturnData data in returnData.resourceData)
                    {
                        if (data != null) resources[data.villageID] = data;
                    }
                }
            }

            lock (_lock)
            {
                _premiumPending = false;
                if (summary != null)
                {
                    _premiumSummary = summary;
                    _premiumResources = resources;
                    _premiumTime = VillageMap.getCurrentServerTime();
                }
            }
        }

        // =================================================================
        // Building rows
        // =================================================================

        /// <summary>
        /// Per-village totals from the world's army, reinforcement, trader and people lists,
        /// gathered in one walk over each list rather than one walk per village.
        /// </summary>
        internal class WorldTotals
        {
            public int TroopsOut, CaptainsOut, Reinforcing, ScoutsOut, TradersOut;
            public int MonksTotal, MonksHome, Incoming;
            public DateTime NextIncoming = DateTime.MaxValue;
        }

        internal class WorldPass
        {
            public readonly Dictionary<int, WorldTotals> Totals = new Dictionary<int, WorldTotals>();
            // Caps are player-wide, so they're looked up once per pass rather than per row.
            public readonly int[] VillageCaps = new int[VillageOverviewRow.ResourceSlots];
            public readonly int[] CapitalCaps = new int[VillageOverviewRow.ResourceSlots];

            public WorldTotals Get(int villageId)
            {
                WorldTotals totals;
                if (!Totals.TryGetValue(villageId, out totals))
                {
                    totals = new WorldTotals();
                    Totals[villageId] = totals;
                }
                return totals;
            }
        }

        public static WorldPass BuildWorldPass()
        {
            WorldPass pass = new WorldPass();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return pass;

            WorldMap world = GameEngine.Instance.World;
            DateTime now = VillageMap.getCurrentServerTime();

            for (int i = 0; i < VillageOverviewRow.ResourceSlots; i++)
            {
                if (i < VillageOverviewRow.ResWood) continue;
                pass.VillageCaps[i] = Kingdoms.Bot.VillageCaps.GetResourceCap(i, false);
                pass.CapitalCaps[i] = Kingdoms.Bot.VillageCaps.GetResourceCap(i, true);
            }

            try
            {
                SparseArray armies = world.getArmyArray();
                if (armies != null)
                {
                    foreach (WorldMap.LocalArmyData army in armies)
                    {
                        if (army == null) continue;

                        // Same rules as WorldMap.countYourArmyTroops / countYourArmyCaptains /
                        // countYourArmyScouts, which the game's own overview uses.
                        if (army.travelFromVillageID == army.homeVillageID && army.homeVillageID >= 0)
                        {
                            WorldTotals home = pass.Get(army.homeVillageID);
                            home.TroopsOut += army.numPeasants + army.numArchers + army.numPikemen
                                + army.numSwordsmen + army.numCatapults;
                        }
                        if (army.travelFromVillageID >= 0 && army.numCaptains > 0)
                            pass.Get(army.travelFromVillageID).CaptainsOut += army.numCaptains;
                        if (army.homeVillageID >= 0 && army.numScouts > 0)
                            pass.Get(army.homeVillageID).ScoutsOut += army.numScouts;

                        if (army.targetVillageID >= 0
                            && Kingdoms.Bot.UI.VillageRadarForm.IsHostileIncoming(army)
                            && army.serverEndTime > now)
                        {
                            WorldTotals target = pass.Get(army.targetVillageID);
                            target.Incoming++;
                            if (army.serverEndTime < target.NextIncoming)
                                target.NextIncoming = army.serverEndTime;
                        }
                    }
                }

                SparseArray reinforcements = world.getReinforcementsArray();
                if (reinforcements != null)
                {
                    foreach (WorldMap.LocalArmyData army in reinforcements)
                    {
                        if (army == null || army.homeVillageID < 0) continue;
                        pass.Get(army.homeVillageID).Reinforcing += army.numPeasants + army.numArchers
                            + army.numPikemen + army.numSwordsmen + army.numCatapults;
                    }
                }

                SparseArray traders = world.getTraderArray();
                if (traders != null)
                {
                    foreach (WorldMap.LocalTrader trader in traders)
                    {
                        if (trader == null || trader.trader == null) continue;
                        pass.Get(trader.trader.homeVillageID).TradersOut += trader.trader.numTraders;
                    }
                }

                SparseArray people = world.getPeopleArray();
                if (people != null)
                {
                    foreach (WorldMap.LocalPerson person in people)
                    {
                        if (person == null || person.person == null) continue;
                        if (person.person.personType != 4) continue;   // monks
                        WorldTotals home = pass.Get(person.person.homeVillageID);
                        home.MonksTotal++;
                        if (person.person.state == 0) home.MonksHome++;
                    }
                }
            }
            catch (Exception ex)
            {
                // The lists are only ever changed on this thread, but a half-built pass is
                // still better than none.
                UniversalDebugLog.Log("VillageOverviewCache: world pass failed: " + ex.Message);
            }

            return pass;
        }

        /// <summary>The player's villages, capitals included, in the game's own order.</summary>
        public static List<int> GetVillageIds(List<int> capitalsOut)
        {
            List<int> ids = new List<int>();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return ids;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return ids;

            for (int i = 0; i < villages.Count; i++)
            {
                if (villages[i] == null) continue;
                ids.Add(villages[i].villageID);
                if (villages[i].capital && capitalsOut != null)
                    capitalsOut.Add(villages[i].villageID);
            }
            return ids;
        }

        /// <summary>Refills a row from everything known about its village.</summary>
        public static void BuildRow(VillageOverviewRow row, bool isCapital, WorldPass pass)
        {
            row.Clear();
            row.IsCapital = isCapital;

            int id = row.VillageId;
            WorldMap world = GameEngine.Instance.World;

            try { row.Name = world.getVillageNameOnly(id) ?? ""; }
            catch (Exception) { row.Name = ""; }
            if (row.Name.Length == 0) row.Name = "Village " + id;

            try { row.Parish = world.getParishNameFromVillageID(id) ?? ""; }
            catch (Exception) { row.Parish = ""; }

            Snapshot snapshot;
            VillageSummaryData summary;
            VillageResourceReturnData premiumResources;
            DateTime premiumTime;

            lock (_lock)
            {
                _snapshots.TryGetValue(id, out snapshot);
                _premiumSummary.TryGetValue(id, out summary);
                _premiumResources.TryGetValue(id, out premiumResources);
                premiumTime = _premiumTime;
                row.RequestPending = _pending.ContainsKey(id);
            }

            // Oldest first, so the freshest source overwrites what they have in common.
            bool snapshotNewer = snapshot != null && (summary == null || snapshot.Time >= premiumTime);
            if (snapshot != null && !snapshotNewer) ApplySnapshot(row, snapshot);
            if (summary != null) ApplyPremium(row, summary, premiumResources, premiumTime);
            if (snapshot != null && snapshotNewer) ApplySnapshot(row, snapshot);

            VillageMap village = GameEngine.Instance.getVillage(id);
            if (village != null)
                ApplyVillageMap(row, village);

            ApplyWorld(row, pass);
            ApplyCastle(row, id);
            ApplyDerived(row, isCapital, pass);
        }

        private static void ApplySnapshot(VillageOverviewRow row, Snapshot snapshot)
        {
            VillageResourceAndStatsReturnData d = snapshot.Stats;
            row.Source = OverviewSource.Snapshot;
            row.DataTime = snapshot.Time;

            double[] l = row.Level;
            l[VillageOverviewRow.ResWood] = d.woodLevel;
            l[VillageOverviewRow.ResStone] = d.stoneLevel;
            l[VillageOverviewRow.ResIron] = d.ironLevel;
            l[VillageOverviewRow.ResPitch] = d.pitchLevel;
            l[VillageOverviewRow.ResAle] = d.aleLevel;
            l[VillageOverviewRow.ResApples] = d.applesLevel;
            l[VillageOverviewRow.ResBread] = d.breadLevel;
            l[VillageOverviewRow.ResVeg] = d.vegLevel;
            l[VillageOverviewRow.ResMeat] = d.meatLevel;
            l[VillageOverviewRow.ResCheese] = d.cheeseLevel;
            l[VillageOverviewRow.ResFish] = d.fishLevel;
            l[VillageOverviewRow.ResSalt] = d.saltLevel;
            l[VillageOverviewRow.ResVenison] = d.venisonLevel;
            l[VillageOverviewRow.ResWine] = d.wineLevel;
            l[VillageOverviewRow.ResFurniture] = d.furnitureLevel;
            l[VillageOverviewRow.ResClothes] = d.clothesLevel;
            l[VillageOverviewRow.ResSpices] = d.spicesLevel;
            l[VillageOverviewRow.ResSilk] = d.silkLevel;
            l[VillageOverviewRow.ResMetalware] = d.metalwareLevel;

            DateTime now = VillageMap.getCurrentServerTime();
            SetWeapon(row, VillageOverviewRow.ResBows, d.bowsLevel, d.toBeMade_Bows, d.productionRate_Bows, d.productionStart_Bows, now);
            SetWeapon(row, VillageOverviewRow.ResPikes, d.pikesLevel, d.toBeMade_Pikes, d.productionRate_Pikes, d.productionStart_Pikes, now);
            SetWeapon(row, VillageOverviewRow.ResSwords, d.swordsLevel, d.toBeMade_Swords, d.productionRate_Swords, d.productionStart_Swords, now);
            SetWeapon(row, VillageOverviewRow.ResArmour, d.armourLevel, d.toBeMade_Armour, d.productionRate_Armour, d.productionStart_Armour, now);
            SetWeapon(row, VillageOverviewRow.ResCatapults, d.catapultLevel, d.toBeMade_Catapults, d.productionRate_Catapults, d.productionStart_Catapults, now);

            row.Peasants = d.numTroops_Peasants;
            row.Archers = d.numTroops_Archers;
            row.Pikemen = d.numTroops_Pikemen;
            row.Swordsmen = d.numTroops_Swordsmen;
            row.Catapults = d.numTroops_Catapults;
            row.Captains = d.numTroops_Captains;
            row.ScoutsHome = d.numTroops_Scouts;
            row.CaptainInTraining = d.captainCreating;
            row.Stationed = d.numStationedTroops_Peasants + d.numStationedTroops_Archers
                + d.numStationedTroops_Pikemen + d.numStationedTroops_Swordsmen + d.numStationedTroops_Catapults;

            row.Popularity = d.popularityLevel;
            row.Tax = d.taxLevel;
            row.RationsSet = d.rationsLevel;
            row.RationsEffective = d.effectiveRationsLevel;
            row.AleSet = d.aleRationsLevel;
            row.AleEffective = d.effectiveAleRationsLevel;
            row.People = d.totalPeople;
            row.Housing = d.housingCapacity;
            row.Spare = d.sparePeople;
            row.KeepEnclosed = d.castleEnclosed ? 1.0 : 0.0;
            row.InterdictEnd = d.interdictProtectionEndTime;
            row.PeaceEnd = d.peaceTime;
            row.ExcomEnd = d.excommunicationEndTime;
            row.TradersHome = d.numTraders;
        }

        private static void ApplyPremium(VillageOverviewRow row, VillageSummaryData s,
            VillageResourceReturnData r, DateTime time)
        {
            if (s.fake) return;

            if (row.Source < OverviewSource.Premium || row.DataTime < time)
            {
                if (row.Source < OverviewSource.Premium) row.Source = OverviewSource.Premium;
                row.DataTime = time;
            }

            row.Peasants = s.numLocalPeasants;
            row.Archers = s.numLocalArchers;
            row.Pikemen = s.numLocalPikemen;
            row.Swordsmen = s.numLocalSwordsmen;
            row.Catapults = s.numLocalCatapults;
            row.Captains = s.numLocalCaptains;
            row.ScoutsHome = s.numLocalScouts;
            row.Placed = s.numPlacedPeasants + s.numPlacedArchers + s.numPlacedPikemen
                + s.numPlacedSwordsmen + s.numPlacedCaptains;

            row.Popularity = s.popularityLevel;
            row.Tax = s.setTaxLevel;
            row.RationsSet = s.setRationsLevel;
            row.RationsEffective = s.currentRationsLevel;
            row.AleSet = s.setAleLevel;
            row.AleEffective = s.currentAleLevel;
            row.People = s.totalPeople;
            row.Housing = s.housingCapacity;
            row.Spare = s.sparePeople;
            row.KeepEnclosed = s.enclosedKeep ? 1.0 : 0.0;
            row.CastleDamaged = s.castleDamaged ? 1.0 : 0.0;
            row.Buildings = s.numBuildings;
            row.InterdictEnd = s.interdictProtectionEndTime;
            row.PeaceEnd = s.peaceTimeEndTime;
            row.ExcomEnd = s.excommunicationEndTime;
            row.TradersHome = s.numMerchantsAtHome;

            if (r == null) return;

            double[] l = row.Level;
            l[VillageOverviewRow.ResWood] = r.woodLevel;
            l[VillageOverviewRow.ResStone] = r.stoneLevel;
            l[VillageOverviewRow.ResIron] = r.ironLevel;
            l[VillageOverviewRow.ResPitch] = r.pitchLevel;
            l[VillageOverviewRow.ResAle] = r.aleLevel;
            l[VillageOverviewRow.ResApples] = r.applesLevel;
            l[VillageOverviewRow.ResBread] = r.breadLevel;
            l[VillageOverviewRow.ResVeg] = r.vegLevel;
            l[VillageOverviewRow.ResMeat] = r.meatLevel;
            l[VillageOverviewRow.ResCheese] = r.cheeseLevel;
            l[VillageOverviewRow.ResFish] = r.fishLevel;
            l[VillageOverviewRow.ResSalt] = r.saltLevel;
            l[VillageOverviewRow.ResVenison] = r.venisonLevel;
            l[VillageOverviewRow.ResWine] = r.wineLevel;
            l[VillageOverviewRow.ResFurniture] = r.furnitureLevel;
            l[VillageOverviewRow.ResClothes] = r.clothesLevel;
            l[VillageOverviewRow.ResSpices] = r.spicesLevel;
            l[VillageOverviewRow.ResSilk] = r.silkLevel;
            l[VillageOverviewRow.ResMetalware] = r.metalwareLevel;
            // The premium batch carries no catapult kits.
            l[VillageOverviewRow.ResBows] = r.bowsLevel;
            l[VillageOverviewRow.ResPikes] = r.pikesLevel;
            l[VillageOverviewRow.ResSwords] = r.swordsLevel;
            l[VillageOverviewRow.ResArmour] = r.armourLevel;
        }

        private static void ApplyVillageMap(VillageOverviewRow row, VillageMap v)
        {
            row.Source = OverviewSource.Live;
            row.Downloaded = true;
            row.DataTime = VillageMap.getCurrentServerTime();

            for (int t = VillageOverviewRow.ResWood; t < VillageOverviewRow.ResourceSlots; t++)
            {
                if (!IsTrackedResource(t)) continue;
                try { row.Level[t] = v.getResourceLevel(t); }
                catch (Exception) { }
            }

            DateTime now = row.DataTime;
            row.InProduction[VillageOverviewRow.ResBows] = Remaining(v.m_toBeMade_Bows, v.m_productionRate_Bows, v.m_productionStart_Bows, now);
            row.InProduction[VillageOverviewRow.ResPikes] = Remaining(v.m_toBeMade_Pikes, v.m_productionRate_Pikes, v.m_productionStart_Pikes, now);
            row.InProduction[VillageOverviewRow.ResSwords] = Remaining(v.m_toBeMade_Swords, v.m_productionRate_Swords, v.m_productionStart_Swords, now);
            row.InProduction[VillageOverviewRow.ResArmour] = Remaining(v.m_toBeMade_Armour, v.m_productionRate_Armour, v.m_productionStart_Armour, now);
            row.InProduction[VillageOverviewRow.ResCatapults] = Remaining(v.m_toBeMade_Catapults, v.m_productionRate_Catapults, v.m_productionStart_Catapults, now);

            try
            {
                int[] produced = { VillageOverviewRow.ResWood, VillageOverviewRow.ResStone,
                    VillageOverviewRow.ResIron, VillageOverviewRow.ResPitch,
                    VillageOverviewRow.ResClothes, VillageOverviewRow.ResFurniture,
                    VillageOverviewRow.ResVenison, VillageOverviewRow.ResSalt,
                    VillageOverviewRow.ResSpices, VillageOverviewRow.ResSilk,
                    VillageOverviewRow.ResMetalware, VillageOverviewRow.ResWine };
                for (int i = 0; i < produced.Length; i++)
                    row.ProdPerDay[produced[i]] = v.getResourceProductionPerDay(produced[i]);
                row.ProdPerDay[VillageOverviewRow.ResAle] = v.getAleProductionPerDay();
                row.FoodProdPerDay = v.getFoodProductionPerDay();
            }
            catch (Exception) { }

            row.Peasants = v.m_numPeasants;
            row.Archers = v.m_numArchers;
            row.Pikemen = v.m_numPikemen;
            row.Swordsmen = v.m_numSwordsmen;
            row.Catapults = v.m_numCatapults;
            row.Captains = v.m_numCaptains;
            row.ScoutsHome = v.m_numScouts;
            try
            {
                DateTime complete = DateTime.MinValue;
                row.CaptainInTraining = v.isCreatingCaptain(ref complete);
            }
            catch (Exception) { }
            row.Stationed = v.m_numStationedPeasants + v.m_numStationedArchers + v.m_numStationedPikemen
                + v.m_numStationedSwordsmen + v.m_numStationedCatapults;

            row.Popularity = v.m_popularityLevel;
            row.Tax = v.m_taxLevel;
            row.RationsSet = v.m_rationsLevel;
            row.RationsEffective = v.m_effectiveRationsLevel;
            row.AleSet = v.m_aleRationsLevel;
            row.AleEffective = v.m_effectiveAleRationsLevel;
            row.People = v.m_totalPeople;
            row.Housing = v.m_housingCapacity;
            row.Spare = v.m_spareWorkers;
            row.KeepEnclosed = v.m_castleEnclosed ? 1.0 : 0.0;
            row.TradersHome = v.m_numTradersAtHome;

            try
            {
                if (v.Buildings != null) row.Buildings = v.Buildings.Count;
                row.BuildQueue = v.countNumBuildingsConstructing();
            }
            catch (Exception) { }
        }

        private static void ApplyWorld(VillageOverviewRow row, WorldPass pass)
        {
            int id = row.VillageId;
            WorldTotals totals;
            if (!pass.Totals.TryGetValue(id, out totals)) totals = new WorldTotals();

            row.TroopsOut = totals.TroopsOut;
            row.CaptainsOut = totals.CaptainsOut;
            row.Reinforcing = totals.Reinforcing;
            row.ScoutsOut = totals.ScoutsOut;
            row.TradersOut = totals.TradersOut;
            row.MonksTotal = totals.MonksTotal;
            row.MonksHome = totals.MonksHome;
            row.Incoming = totals.Incoming;
            row.NextIncoming = totals.Incoming > 0 ? totals.NextIncoming : DateTime.MinValue;

            // The world map's protection times are refreshed by more than one reply, so take
            // them when the stats didn't carry any.
            WorldMap world = GameEngine.Instance.World;
            try
            {
                if (VillageOverviewRow.IsUnset(row.InterdictEnd)) row.InterdictEnd = world.getInterdictTime(id);
                if (VillageOverviewRow.IsUnset(row.PeaceEnd)) row.PeaceEnd = world.getPeaceTime(id);
                if (VillageOverviewRow.IsUnset(row.ExcomEnd)) row.ExcomEnd = world.getExcommunicationTime(id);
            }
            catch (Exception) { }
        }

        // Castle-placed troops and damage are only known for a castle the client has loaded,
        // or from the premium batch.
        private static void ApplyCastle(VillageOverviewRow row, int id)
        {
            try
            {
                CastleMap castle = GameEngine.Instance.GetCastleByVillageID(id);
                if (castle == null) return;
                row.Placed = castle.countOwnPlacedTroops();
                row.CastleDamaged = castle.castleDamaged ? 1.0 : 0.0;
            }
            catch (Exception) { }
        }

        private static void ApplyDerived(VillageOverviewRow row, bool isCapital, WorldPass pass)
        {
            int[] caps = isCapital ? pass.CapitalCaps : pass.VillageCaps;
            for (int t = 0; t < VillageOverviewRow.ResourceSlots; t++)
            {
                row.Cap[t] = caps[t];
                if (!double.IsNaN(row.Level[t]) && row.Level[t] < 0) row.Level[t] = 0;
            }

            double food = double.NaN;
            double foodCap = 0;
            for (int i = 0; i < VillageOverviewRow.FoodTypes.Length; i++)
            {
                int t = VillageOverviewRow.FoodTypes[i];
                food = VillageOverviewRow.Sum(food, row.Level[t]);
                foodCap += row.Cap[t];
            }
            row.FoodLevel = food;
            row.FoodCap = foodCap;

            if (!row.HasStats) return;

            try
            {
                WorldData worldData = GameEngine.Instance.LocalWorldData;

                // VillageMap.showStats' daily consumption formulas.
                if (!double.IsNaN(row.People) && !double.IsNaN(row.RationsEffective) && worldData.foodConsumptionRate > 0)
                {
                    double eff = row.RationsEffective;
                    double factor = eff <= 2.0 ? eff / 4.0 : (eff < 3.0 ? (eff - 2.0) / 2.0 + 0.5 : eff - 2.0);
                    row.FoodConsPerDay = row.People / (worldData.foodConsumptionRate / 24.0) * factor;
                }
                if (!double.IsNaN(row.People) && !double.IsNaN(row.AleEffective) && worldData.aleConsumptionRate > 0)
                    row.AleConsPerDay = row.People / (worldData.aleConsumptionRate / 24.0) * row.AleEffective;

                if (!isCapital && !double.IsNaN(row.People) && !double.IsNaN(row.Tax))
                {
                    double taxed = Math.Min(row.People, double.IsNaN(row.Housing) ? row.People : row.Housing);
                    row.GoldPerDay = taxed * VillageBuildingsData.getTaxIncomeLevel((int)row.Tax,
                        GameEngine.Instance.cardsManager.UserCardData) * worldData.goldIncomeRate;
                }

                // VillageMap.calcUnitUsages, with the troops out and in the castle taken from
                // the world lists rather than whichever castle happens to be on screen.
                if (!isCapital && !double.IsNaN(row.TroopsHome))
                {
                    double troops = row.TroopsHome + Zero(row.Captains) + Zero(row.TroopsOut)
                        + Zero(row.CaptainsOut) + Zero(row.Reinforcing) + Zero(row.Placed);
                    row.UnitsUsed = troops
                        + Zero(row.ScoutsTotal) * worldData.UnitSize_Scout
                        + Zero(row.TradersTotal) * worldData.UnitSize_Trader
                        + Zero(row.MonksTotal) * worldData.UnitSize_Priests;
                    row.UnitsCap = worldData.Village_UnitCapacity;
                }
            }
            catch (Exception) { }
        }

        private static void SetWeapon(VillageOverviewRow row, int resourceId, double level,
            double toBeMade, double rate, DateTime start, DateTime now)
        {
            double remaining = Remaining(toBeMade, rate, start, now);
            row.Level[resourceId] = level + (toBeMade > 0 ? toBeMade - remaining : 0);
            row.InProduction[resourceId] = remaining;
        }

        // Same maths as VillageMap.getArmouryLevels: the level at the start of the batch plus
        // rate x elapsed, capped at the batch size.
        private static double Remaining(double toBeMade, double rate, DateTime start, DateTime now)
        {
            if (toBeMade <= 0.0 || rate <= 0.0) return 0.0;
            double made = rate * (now - start).TotalSeconds;
            if (made < 0.0) made = 0.0;
            if (made > toBeMade) made = toBeMade;
            return Math.Floor(toBeMade - made);
        }

        private static bool IsTrackedResource(int t)
        {
            switch (t)
            {
                case 10: case 11: case 20: case 27:
                    return false;
                default:
                    return t >= VillageOverviewRow.ResWood && t <= VillageOverviewRow.ResWine;
            }
        }

        private static double Zero(double value)
        {
            return double.IsNaN(value) ? 0.0 : value;
        }
    }
}
