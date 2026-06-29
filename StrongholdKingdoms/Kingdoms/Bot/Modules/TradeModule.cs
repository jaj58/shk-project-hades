using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class TradeModule : BotModuleBase
    {
        private DateTime _lastFullCycle = DateTime.MinValue;
        private int _currentVillageIndex;
        private List<int> _villageQueue = new List<int>();
        private DateTime _lastVillageAction = DateTime.MinValue;
        private readonly Random _random = new Random();

        // Auto-disable timer: stamped on the first tick after the module is enabled,
        // reset on disable. Drives the optional "auto-disable after X minutes" feature.
        private DateTime _enabledSince = DateTime.MinValue;
        // Set by the manual Trade-enabled checkbox path so a manual disable does NOT
        // trigger the auto-disband. Cleared (consumed) on the next OnDisable.
        private bool _suppressDisbandOnNextDisable;

        // Runtime disband state, read/cleared by the always-on TradeDisbandModule.
        // Not persisted — purely an in-session signal that an automatic disable
        // requested the deployed traders be returned home and disbanded.
        public bool DisbandPending { get; set; }
        public DateTime DisbandStartedAt { get; set; }

        /// <summary>
        /// Marks (pending=true) or clears (pending=false) the next Enabled true->false
        /// transition as a manual disable so the auto-disband is skipped. Called only
        /// from the UI Trade Enabled checkbox's Click handler (user interaction only —
        /// programmatic state syncs use the CheckedChanged event and must NOT suppress).
        /// The flag is consumed (cleared) on the next OnDisable.
        /// </summary>
        public void SetManualDisablePending(bool pending) { _suppressDisbandOnNextDisable = pending; }

        // Cached stock exchange data. Written from RPC callback threads and read
        // on the tick thread, so every access goes through _cacheLock.
        private readonly object _cacheLock = new object();
        private readonly Dictionary<int, StockExchangeCache> _stockExchangeCache =
            new Dictionary<int, StockExchangeCache>();

        // Cached capitals list
        private int[] _capitalsList;

        // For route processing: which routes involve a given village as sender
        private Dictionary<int, List<TradeRouteSettings>> _routesBySender =
            new Dictionary<int, List<TradeRouteSettings>>();

        // For player route processing: which player routes involve a given village as sender
        private Dictionary<int, List<PlayerTradeRouteSettings>> _playerRoutesBySender =
            new Dictionary<int, List<PlayerTradeRouteSettings>>();

        // Per-village step state. Each village drains its trade categories in
        // priority order, one server call per tick, before the queue advances.
        private int _activeVillageId = -1;
        private readonly List<TradeCategory> _remainingCategories = new List<TradeCategory>();
        private readonly Dictionary<TradeCategory, int> _dispatchCounts =
            new Dictionary<TradeCategory, int>();
        private DateTime _blockedSince = DateTime.MinValue;
        // Resources the server rejected with "not enough resources to send" this
        // cycle, keyed by village. A sell shortage is seller-side (our local
        // resource count was stale/overstated), not a market problem, so we stop
        // re-offering that resource instead of rotating it through every market
        // until the dispatch cap is hit. Cleared per village in BeginVillage;
        // only touched on the tick thread.
        private readonly Dictionary<int, HashSet<int>> _sellBlockedThisCycle =
            new Dictionary<int, HashSet<int>>();
        // Villages whose market trading is blocked for the rest of this cycle
        // because the server reported "not enough traders" (our local merchant
        // count was stale). Cleared per village in BeginVillage; tick-thread only.
        private readonly HashSet<int> _marketBlockedThisCycle = new HashSet<int>();
        // Safety valve: bounds how many dispatches one category of one village can
        // make per cycle, so a pathological config can't stall the whole queue.
        private const int MaxDispatchesPerCategory = 20;
        private const double BlockedTimeoutSeconds = 120.0;

        // Price fetch state
        private bool _waitingForPrices;
        private DateTime _priceFetchStarted = DateTime.MinValue;
        private int _priceFetchVillageId = -1;
        private List<int> _marketsToFetch = new List<int>();   // remaining markets to request
        private int _fetchingMarketId = -1;                     // market whose response is currently in-flight
        private bool _premiumFetch;
        private const double PriceFetchTimeoutSeconds = 15.0;
        private const double PriceCacheValidMinutes = 3.0;

        // Dispatch verification: stockExchangeTrade()/sendResources() returning true
        // only means the request was SENT — the server result arrives later via the
        // VillageMap callbacks, which swallow most errors. We track every dispatch
        // and only count it (stats, player-route progress) once the server confirms.
        private readonly object _pendingLock = new object();
        private readonly Dictionary<int, PendingDispatch> _pendingByVillage =
            new Dictionary<int, PendingDispatch>();
        private readonly List<TradeResult> _resultQueue = new List<TradeResult>();
        private const double PendingTimeoutSeconds = 90.0;

        // Session statistics
        private readonly TradeSessionStats _stats = new TradeSessionStats();

        private enum TradeCategory
        {
            Markets,
            Routes,
            PlayerRoutes
        }

        private enum TradeActionResult
        {
            Dispatched, // a server call was made; wait for the response before the next one
            NoWork,     // nothing (left) to do in this category this cycle
            Blocked     // the village's send lock is busy; retry next tick
        }

        private enum PendingKind
        {
            MarketSell,
            MarketBuy,
            Route,
            PlayerRoute
        }

        private class PendingDispatch
        {
            public PendingKind Kind;
            public int VillageId;
            public int TargetId;
            public int ResourceId;
            public int Amount;
            public string RouteName;
            public PlayerTradeRouteSettings PlayerRoute;
            public PlayerTradeResourceEntry PlayerEntry;
            public DateTime DispatchedAt;
        }

        private class TradeResult
        {
            public PendingDispatch Pending;
            public bool Success;
            public bool Unconfirmed; // no server response arrived before the timeout
            public string Error;
        }

        public override string ModuleName
        {
            get { return "Trade"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(3); }
        }

        private TradeSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.Trade;
                return null;
            }
        }

        public TradeSessionStats GetStats() { return _stats; }

        protected override void OnInitialize()
        {
            _currentVillageIndex = 0;
            _villageQueue.Clear();
            _waitingForPrices = false;
            _activeVillageId = -1;
            _remainingCategories.Clear();
            _dispatchCounts.Clear();
            _blockedSince = DateTime.MinValue;
            _stats.Reset();
            try { _stats.SessionStartGold = (long)GameEngine.Instance.World.getCurrentGold(); } catch { }
            _marketsToFetch.Clear();
            _fetchingMarketId = -1;
            lock (_cacheLock)
            {
                _stockExchangeCache.Clear();
            }
            lock (_pendingLock)
            {
                _pendingByVillage.Clear();
                _resultQueue.Clear();
            }

            // Hook the VillageMap trade callbacks so every dispatch is verified
            // against the actual server response instead of assumed successful.
            VillageMap.BotStockExchangeTradeResult =
                new Action<StockExchangeTrade_ReturnType>(OnStockExchangeTradeResult);
            VillageMap.BotSendMarketResourcesResult =
                new Action<SendMarketResources_ReturnType>(OnSendMarketResourcesResult);
        }

        protected override void OnShutdown()
        {
            VillageMap.BotStockExchangeTradeResult = null;
            VillageMap.BotSendMarketResourcesResult = null;
        }

        protected override void OnDisable()
        {
            // Reset the auto-disable timer so a later re-enable starts a fresh window.
            _enabledSince = DateTime.MinValue;

            // Consume the manual-disable flag: a manual untick suppresses the disband,
            // any other disable route (card expiry, the X-minute timer, the Auto
            // scheduler) leaves it false and so disbands when the option is on.
            bool suppress = _suppressDisbandOnNextDisable;
            _suppressDisbandOnNextDisable = false;

            TradeSettings s = Settings;
            if (s != null && s.DisbandTradersOnDisable && !suppress)
            {
                DisbandPending = true;
                DisbandStartedAt = DateTime.Now;
                LogInfo("Trade disabled — will disband traders once they return home.");
            }
        }

        protected override void OnTick()
        {
            TradeSettings settings = Settings;
            if (settings == null) return;

            // Auto-disable-after-X-minutes timer. _enabledSince is stamped the first
            // tick after the module becomes enabled (and reset in OnDisable), so the
            // window is measured from when trading actually started.
            if (_enabledSince == DateTime.MinValue)
                _enabledSince = DateTime.Now;
            if (settings.DisableAfterMinutes > 0 &&
                (DateTime.Now - _enabledSince).TotalMinutes >= settings.DisableAfterMinutes)
            {
                LogInfo("Auto-disabling trade after " + settings.DisableAfterMinutes + " minute(s).");
                Enabled = false;          // triggers OnDisable (which may queue the disband)
                settings.Enabled = false; // keep settings in sync with the runtime state
                return;
            }

            // Apply confirmed/failed trade results on the tick thread first so
            // player-route progress and merchant counts are current before we
            // decide on the next dispatch.
            DrainTradeResults(settings);

            if (_waitingForPrices)
            {
                HandlePriceWait(settings);
                return;
            }

            // If we're mid-cycle, process the current village
            if (_villageQueue.Count > 0 && _currentVillageIndex < _villageQueue.Count)
            {
                ProcessQueue(settings);
                return;
            }

            // Not mid-cycle: check if enough time has passed to start a new one
            double elapsed = (DateTime.Now - _lastFullCycle).TotalSeconds;
            if (elapsed < settings.CycleIntervalSeconds)
                return;

            _currentVillageIndex = 0;
            _activeVillageId = -1;
            _lastFullCycle = DateTime.Now;
            RefreshCapitalsList();
            BuildUnifiedVillageQueue(settings);

            LogInfo("Starting trade cycle. " + _villageQueue.Count + " village(s) to process. Priority: " +
                settings.Priority.ToString());

            if (_villageQueue.Count == 0)
            {
                LogDebug("No villages to trade.");
                return;
            }
        }

        // =====================================================================
        // Cycle / queue processing
        // =====================================================================

        private void ProcessQueue(TradeSettings settings)
        {
            int villageId = _villageQueue[_currentVillageIndex];

            if (_activeVillageId != villageId)
            {
                BeginVillage(villageId, settings);
                if (_waitingForPrices)
                    return; // price fetch in flight; processing resumes when it resolves
            }

            if (_remainingCategories.Count == 0)
            {
                AdvanceToNextVillage();
                return;
            }

            if ((DateTime.Now - _lastVillageAction).TotalMilliseconds < settings.DelayBetweenVillagesMs)
                return;

            StepVillage(villageId, settings);
        }

        private void BeginVillage(int villageId, TradeSettings settings)
        {
            _activeVillageId = villageId;
            _remainingCategories.Clear();
            _dispatchCounts.Clear();
            _sellBlockedThisCycle.Remove(villageId);
            _marketBlockedThisCycle.Remove(villageId);
            _blockedSince = DateTime.MinValue;

            VillageMap map = GameEngine.Instance.getVillage(villageId);
            if (map == null)
            {
                LogDebug("Village " + villageId + " not loaded, skipping.");
                return;
            }

            VillageMarketTradeInfo marketInfo = settings.GetVillageMarketInfo(villageId);
            bool hasMarkets = marketInfo.IsTrading && marketInfo.MarketTargets.Count > 0;
            bool hasRoutes = _routesBySender.ContainsKey(villageId);
            bool hasPlayerRoutes = _playerRoutesBySender.ContainsKey(villageId);

            // Categories run in priority order: each one drains fully (one server
            // call per tick) before the next is attempted.
            switch (settings.Priority)
            {
                case TradePriority.VillageRoutes:
                    if (hasRoutes) _remainingCategories.Add(TradeCategory.Routes);
                    if (hasMarkets) _remainingCategories.Add(TradeCategory.Markets);
                    if (hasPlayerRoutes) _remainingCategories.Add(TradeCategory.PlayerRoutes);
                    break;
                case TradePriority.PlayerRoutes:
                    if (hasPlayerRoutes) _remainingCategories.Add(TradeCategory.PlayerRoutes);
                    if (hasRoutes) _remainingCategories.Add(TradeCategory.Routes);
                    if (hasMarkets) _remainingCategories.Add(TradeCategory.Markets);
                    break;
                default: // MarketSellFirst / MarketBuyFirst
                    if (hasMarkets) _remainingCategories.Add(TradeCategory.Markets);
                    if (hasRoutes) _remainingCategories.Add(TradeCategory.Routes);
                    if (hasPlayerRoutes) _remainingCategories.Add(TradeCategory.PlayerRoutes);
                    break;
            }

            if (_remainingCategories.Count == 0)
            {
                LogDebug(GetVillageName(villageId) + ": No market trading, routes or player routes configured.");
                return;
            }

            LogDebug(GetVillageName(villageId) + ": Processing (markets=" + hasMarkets +
                ", routes=" + hasRoutes + ", playerRoutes=" + hasPlayerRoutes +
                ", merchants=" + map.m_numTradersAtHome + ")");

            if (settings.AutoHireMerchants)
                AutoHireMerchants(map, settings);

            if (hasMarkets && !HasFreshPricesForVillage(villageId, settings))
                RequestPrices(villageId, marketInfo, settings);
        }

        private void StepVillage(int villageId, TradeSettings settings)
        {
            // Wait for the previous dispatch to be confirmed before making the
            // next one — otherwise resource levels, merchant counts and player
            // route progress are stale and we'd double-send.
            if (HasPendingDispatch(villageId))
            {
                if (_blockedSince == DateTime.MinValue)
                {
                    _blockedSince = DateTime.Now;
                }
                else if ((DateTime.Now - _blockedSince).TotalSeconds > BlockedTimeoutSeconds)
                {
                    LogWarning(GetVillageName(villageId) + ": waited " + (int)BlockedTimeoutSeconds +
                        "s for a trade response, skipping village this cycle.");
                    AdvanceToNextVillage();
                }
                return;
            }

            VillageMap map = GameEngine.Instance.getVillage(villageId);
            if (map == null)
            {
                LogDebug("Village " + villageId + " not loaded, skipping.");
                AdvanceToNextVillage();
                return;
            }

            string villageName = GetVillageName(villageId);

            while (_remainingCategories.Count > 0)
            {
                TradeCategory category = _remainingCategories[0];

                if (GetDispatchCount(category) >= MaxDispatchesPerCategory)
                {
                    LogDebug(villageName + ": " + category + " dispatch cap reached for this cycle.");
                    _remainingCategories.RemoveAt(0);
                    continue;
                }

                TradeActionResult result = TryCategory(category, map, villageId, villageName, settings);

                if (result == TradeActionResult.Dispatched)
                {
                    _dispatchCounts[category] = GetDispatchCount(category) + 1;
                    _lastVillageAction = DateTime.Now;
                    _blockedSince = DateTime.MinValue;
                    return; // one server call per tick; same category gets retried next tick
                }

                if (result == TradeActionResult.Blocked)
                {
                    // The send lock is busy. Stop here rather than falling through
                    // to a lower-priority category, which would let it jump the
                    // queue once the lock clears (priority inversion).
                    if (_blockedSince == DateTime.MinValue)
                    {
                        _blockedSince = DateTime.Now;
                    }
                    else if ((DateTime.Now - _blockedSince).TotalSeconds > BlockedTimeoutSeconds)
                    {
                        LogWarning(villageName + ": send lock stuck for " + (int)BlockedTimeoutSeconds +
                            "s, skipping village this cycle.");
                        AdvanceToNextVillage();
                    }
                    return;
                }

                // NoWork: category exhausted for this cycle, move to the next one.
                _remainingCategories.RemoveAt(0);
                _blockedSince = DateTime.MinValue;
            }

            AdvanceToNextVillage();
        }

        private TradeActionResult TryCategory(TradeCategory category, VillageMap map,
            int villageId, string villageName, TradeSettings settings)
        {
            switch (category)
            {
                case TradeCategory.Markets:
                    return TryMarketTrade(map, villageId, villageName,
                        settings.GetVillageMarketInfo(villageId), settings);

                case TradeCategory.Routes:
                    List<TradeRouteSettings> routes;
                    if (!_routesBySender.TryGetValue(villageId, out routes))
                        return TradeActionResult.NoWork;
                    return TryRouteSend(map, villageId, villageName, routes, settings);

                default:
                    List<PlayerTradeRouteSettings> playerRoutes;
                    if (!_playerRoutesBySender.TryGetValue(villageId, out playerRoutes))
                        return TradeActionResult.NoWork;
                    return TryPlayerRouteSend(map, villageId, villageName, playerRoutes, settings);
            }
        }

        private int GetDispatchCount(TradeCategory category)
        {
            int count;
            if (_dispatchCounts.TryGetValue(category, out count))
                return count;
            return 0;
        }

        private void AdvanceToNextVillage()
        {
            _currentVillageIndex++;
            _activeVillageId = -1;
            _remainingCategories.Clear();
            _blockedSince = DateTime.MinValue;

            if (_currentVillageIndex >= _villageQueue.Count)
                LogInfo("Trade cycle complete. Processed " + _villageQueue.Count + " village(s).");
        }

        private void BuildUnifiedVillageQueue(TradeSettings settings)
        {
            _villageQueue.Clear();
            _routesBySender.Clear();
            _playerRoutesBySender.Clear();

            // Index routes by sender village
            foreach (TradeRouteSettings route in settings.Routes)
            {
                if (!route.Enabled || route.FromVillages.Count == 0 ||
                    route.ToVillages.Count == 0 || route.Resources.Count == 0)
                    continue;

                foreach (int senderId in route.FromVillages)
                {
                    List<TradeRouteSettings> list;
                    if (!_routesBySender.TryGetValue(senderId, out list))
                    {
                        list = new List<TradeRouteSettings>();
                        _routesBySender[senderId] = list;
                    }
                    list.Add(route);
                }
            }

            // Index player routes by sender village
            foreach (PlayerTradeRouteSettings route in settings.PlayerRoutes)
            {
                if (!route.Enabled || route.FromVillages.Count == 0 ||
                    route.TargetVillageId <= 0 || route.Resources.Count == 0)
                    continue;

                if (route.IsComplete())
                {
                    route.Enabled = false;
                    LogInfo("[Player Route '" + route.Name + "'] All resources delivered. Route disabled.");
                    continue;
                }

                foreach (int senderId in route.FromVillages)
                {
                    List<PlayerTradeRouteSettings> list;
                    if (!_playerRoutesBySender.TryGetValue(senderId, out list))
                    {
                        list = new List<PlayerTradeRouteSettings>();
                        _playerRoutesBySender[senderId] = list;
                    }
                    list.Add(route);
                }
            }

            // Collect unique villages from market villages, route senders, and player route senders
            Dictionary<int, bool> seen = new Dictionary<int, bool>();

            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
            {
                List<int> userVillages = GameEngine.Instance.World.getUserVillageIDList();
                if (userVillages != null)
                {
                    foreach (int id in userVillages)
                    {
                        VillageMarketTradeInfo info = settings.GetVillageMarketInfo(id);

                        // Re-validate market targets once per cycle: parish/faction
                        // changes can revoke trade rights, and the server silently
                        // rejects trades with markets we can no longer use.
                        if (info.IsTrading && info.MarketTargets.Count > 0)
                            RevalidateMarketTargets(id, info);

                        bool hasMarkets = info.IsTrading && info.MarketTargets.Count > 0;
                        bool hasRoutes = _routesBySender.ContainsKey(id);
                        bool hasPlayerRoutes = _playerRoutesBySender.ContainsKey(id);

                        if (hasMarkets || hasRoutes || hasPlayerRoutes)
                        {
                            if (!seen.ContainsKey(id))
                            {
                                _villageQueue.Add(id);
                                seen[id] = true;
                            }
                        }
                    }
                }
            }

            // Shuffle so the same village doesn't always trade (and spend the
            // shared gold pool) first every cycle.
            for (int i = _villageQueue.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                int tmp = _villageQueue[i];
                _villageQueue[i] = _villageQueue[j];
                _villageQueue[j] = tmp;
            }

            LogDebug("Queue built: " + _villageQueue.Count + " village(s). " +
                     _routesBySender.Count + " village(s) with routes. " +
                     _playerRoutesBySender.Count + " village(s) with player routes.");
        }

        private void RevalidateMarketTargets(int villageId, VillageMarketTradeInfo info)
        {
            for (int i = info.MarketTargets.Count - 1; i >= 0; i--)
            {
                int marketId = info.MarketTargets[i];
                bool allowed = false;
                try { allowed = GameEngine.Instance.World.allowExchangeTrade(marketId, villageId); }
                catch { allowed = true; } // don't drop markets on a transient lookup error
                if (!allowed)
                {
                    LogWarning(GetVillageName(villageId) + ": market " + GetVillageName(marketId) +
                        " is no longer tradeable, removing from targets.");
                    info.MarketTargets.RemoveAt(i);
                }
            }
        }

        private void RefreshCapitalsList()
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                {
                    _capitalsList = new int[0];
                    return;
                }

                List<int> caps = GameEngine.Instance.World.getCapitalList();
                if (caps == null)
                    _capitalsList = new int[0];
                else
                    _capitalsList = caps.ToArray();
            }
            catch
            {
                _capitalsList = new int[0];
            }
        }

        // =====================================================================
        // Price fetching
        // =====================================================================

        private bool HasFreshPricesForVillage(int villageId, TradeSettings settings)
        {
            VillageMarketTradeInfo info = settings.GetVillageMarketInfo(villageId);
            if (info.MarketTargets.Count == 0) return true;

            // Require ALL markets to have fresh cache before trading.
            // This ensures FindBestMarket can compare all candidates and pick
            // the best price, rather than trading with partial data.
            foreach (int marketId in info.MarketTargets)
            {
                if (!HasFreshCache(marketId))
                    return false;
            }
            return true;
        }

        private void HandlePriceWait(TradeSettings settings)
        {
            if (HasFreshPricesForVillage(_priceFetchVillageId, settings))
            {
                LogDebug("Price data received for " + GetVillageName(_priceFetchVillageId));
                _waitingForPrices = false;
                _priceFetchVillageId = -1;
                return;
            }

            if ((DateTime.Now - _priceFetchStarted).TotalSeconds > PriceFetchTimeoutSeconds)
            {
                LogWarning("Price fetch timed out for " + GetVillageName(_priceFetchVillageId) +
                    "; markets without fresh prices will be skipped (no blind trades).");
                _waitingForPrices = false;
                _priceFetchVillageId = -1;
                return;
            }

            // Chained fetching: when the in-flight request's data arrives,
            // immediately dispatch the next one.
            if (_fetchingMarketId != -1 && _marketsToFetch.Count > 0)
            {
                if (HasFreshCache(_fetchingMarketId))
                {
                    try { DispatchNextMarketFetch(); }
                    catch (Exception ex)
                    {
                        LogError("Failed to request stock exchange data: " + ex.Message);
                        _waitingForPrices = false;
                        _priceFetchVillageId = -1;
                    }
                }
            }
        }

        private void RequestPrices(int villageId, VillageMarketTradeInfo marketInfo, TradeSettings settings)
        {
            // Build the list of stale markets
            _marketsToFetch.Clear();
            _fetchingMarketId = -1;
            foreach (int marketId in marketInfo.MarketTargets)
            {
                if (!HasFreshCache(marketId))
                    _marketsToFetch.Add(marketId);
            }

            if (_marketsToFetch.Count == 0)
                return;

            _waitingForPrices = true;
            _priceFetchStarted = DateTime.Now;
            _priceFetchVillageId = villageId;
            try { _premiumFetch = GameEngine.Instance.World.isAccountPremium(); }
            catch { _premiumFetch = false; }

            // Nearest first, so the primary of the first batch is the closest market.
            int sourceId = villageId;
            _marketsToFetch.Sort(delegate(int a, int b)
            {
                int da = GameEngine.Instance.World.getSquareDistance(sourceId, a);
                int db = GameEngine.Instance.World.getSquareDistance(sourceId, b);
                return da.CompareTo(db);
            });

            LogDebug(GetVillageName(villageId) + ": Fetching prices for " + _marketsToFetch.Count +
                " market(s)" + (_premiumFetch ? " (premium batches)" : "") + "...");

            try
            {
                DispatchNextMarketFetch();
            }
            catch (Exception ex)
            {
                LogError("Failed to request stock exchange data: " + ex.Message);
                _waitingForPrices = false;
                _priceFetchVillageId = -1;
            }
        }

        private void DispatchNextMarketFetch()
        {
            if (_marketsToFetch.Count == 0) return;

            // Re-register every time: the game's own stock exchange panels can
            // overwrite this global callback at any moment.
            RemoteServices.Instance.set_GetStockExchangeData_UserCallBack(
                new RemoteServices.GetStockExchangeData_UserCallBack(OnStockExchangeDataReceived));

            _priceFetchStarted = DateTime.Now; // per-request timeout

            // The primary village of the request must itself be a market/capital —
            // the server returns THAT village's exchange data (plus closeVillages
            // for premium accounts). This mirrors StockExchangePanel; passing our
            // own village as the primary does not work.
            int primary = _marketsToFetch[0];
            _marketsToFetch.RemoveAt(0);
            _fetchingMarketId = primary;

            if (_premiumFetch && _marketsToFetch.Count > 0)
            {
                int batchSize = Math.Min(20, _marketsToFetch.Count);
                int[] batch = new int[batchSize];
                for (int i = 0; i < batchSize; i++)
                    batch[i] = _marketsToFetch[i];
                _marketsToFetch.RemoveRange(0, batchSize);
                RemoteServices.Instance.GetStockExchangePremiumData(primary, batch);
            }
            else
            {
                RemoteServices.Instance.GetStockExchangeData(primary, true);
            }
        }

        private void OnStockExchangeDataReceived(GetStockExchangeData_ReturnType returnData)
        {
            if (returnData == null) return;

            if (!returnData.Success)
            {
                string error;
                try { error = ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID); }
                catch { error = "unknown error"; }
                LogWarning("Price fetch failed for " + GetVillageName(returnData.villageID) + ": " + error);
                return;
            }

            UpdateStockExchangeCache(returnData.villageID, CopyExchangeInfo(returnData));

            // Premium multi-market data
            if (returnData.otherVillages != null)
            {
                foreach (GetStockExchangeData_ReturnType other in returnData.otherVillages)
                    UpdateStockExchangeCache(other.villageID, CopyExchangeInfo(other));
            }
        }

        private static StockExchangePanel.StockExchangeInfo CopyExchangeInfo(
            GetStockExchangeData_ReturnType data)
        {
            StockExchangePanel.StockExchangeInfo info = new StockExchangePanel.StockExchangeInfo();
            info.lastTime = DateTime.Now;
            info.villageID = data.villageID;
            info.woodLevel = data.woodLevel;
            info.stoneLevel = data.stoneLevel;
            info.ironLevel = data.ironLevel;
            info.pitchLevel = data.pitchLevel;
            info.aleLevel = data.aleLevel;
            info.applesLevel = data.applesLevel;
            info.breadLevel = data.breadLevel;
            info.meatLevel = data.meatLevel;
            info.cheeseLevel = data.cheeseLevel;
            info.vegLevel = data.vegLevel;
            info.fishLevel = data.fishLevel;
            info.bowsLevel = data.bowsLevel;
            info.pikesLevel = data.pikesLevel;
            info.swordsLevel = data.swordsLevel;
            info.armourLevel = data.armourLevel;
            info.catapultsLevel = data.catapultsLevel;
            info.furnitureLevel = data.furnitureLevel;
            info.clothesLevel = data.clothesLevel;
            info.saltLevel = data.saltLevel;
            info.venisonLevel = data.venisonLevel;
            info.silkLevel = data.silkLevel;
            info.spicesLevel = data.spicesLevel;
            info.metalwareLevel = data.metalwareLevel;
            info.wineLevel = data.wineLevel;
            return info;
        }

        // =====================================================================
        // Dispatch verification
        // =====================================================================

        private void OnStockExchangeTradeResult(StockExchangeTrade_ReturnType returnData)
        {
            if (returnData == null) return;
            string error = null;
            if (!returnData.Success)
            {
                try { error = ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID); }
                catch { error = "error code " + returnData.m_errorCode; }
            }
            else if (returnData.stockExchangeData != null && returnData.stockExchangeData.villageID > 0)
            {
                // The trade response carries the market's post-trade stock levels —
                // refresh the cache so the next trade sees what our purchase/sale
                // actually left behind instead of minutes-old data.
                try
                {
                    UpdateStockExchangeCache(returnData.stockExchangeData.villageID,
                        CopyExchangeInfo(returnData.stockExchangeData));
                }
                catch
                {
                }
            }
            QueueTradeResult(returnData.villageID, returnData.Success, error);
        }

        private void OnSendMarketResourcesResult(SendMarketResources_ReturnType returnData)
        {
            if (returnData == null) return;
            string error = null;
            if (!returnData.Success)
            {
                try { error = ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID); }
                catch { error = "error code " + returnData.m_errorCode; }
            }
            QueueTradeResult(returnData.villageID, returnData.Success, error);
        }

        // Runs on RPC threads: only queue the result; effects are applied on the
        // module's tick thread in DrainTradeResults.
        private void QueueTradeResult(int villageId, bool success, string error)
        {
            lock (_pendingLock)
            {
                PendingDispatch pending;
                if (!_pendingByVillage.TryGetValue(villageId, out pending))
                    return; // not a bot dispatch (e.g. a manual trade from the game UI)
                _pendingByVillage.Remove(villageId);

                TradeResult result = new TradeResult();
                result.Pending = pending;
                result.Success = success;
                result.Error = error;
                _resultQueue.Add(result);
            }
        }

        private void RecordPending(PendingKind kind, int villageId, int targetId, int resourceId,
            int amount, string routeName, PlayerTradeRouteSettings playerRoute,
            PlayerTradeResourceEntry playerEntry)
        {
            PendingDispatch pending = new PendingDispatch();
            pending.Kind = kind;
            pending.VillageId = villageId;
            pending.TargetId = targetId;
            pending.ResourceId = resourceId;
            pending.Amount = amount;
            pending.RouteName = routeName;
            pending.PlayerRoute = playerRoute;
            pending.PlayerEntry = playerEntry;
            pending.DispatchedAt = DateTime.Now;

            lock (_pendingLock)
            {
                _pendingByVillage[villageId] = pending;
            }
        }

        private bool HasPendingDispatch(int villageId)
        {
            lock (_pendingLock)
            {
                return _pendingByVillage.ContainsKey(villageId);
            }
        }

        private void DrainTradeResults(TradeSettings settings)
        {
            List<TradeResult> results = null;

            lock (_pendingLock)
            {
                // Expire dispatches whose response never arrived (lost response or
                // the global callback was overwritten by a game panel). Treat them
                // as sent — the pre-verification module did the same — but say so.
                List<int> expired = null;
                foreach (KeyValuePair<int, PendingDispatch> kv in _pendingByVillage)
                {
                    if ((DateTime.Now - kv.Value.DispatchedAt).TotalSeconds > PendingTimeoutSeconds)
                    {
                        if (expired == null) expired = new List<int>();
                        expired.Add(kv.Key);
                    }
                }
                if (expired != null)
                {
                    foreach (int villageId in expired)
                    {
                        TradeResult result = new TradeResult();
                        result.Pending = _pendingByVillage[villageId];
                        result.Success = true;
                        result.Unconfirmed = true;
                        _pendingByVillage.Remove(villageId);
                        _resultQueue.Add(result);
                    }
                }

                if (_resultQueue.Count > 0)
                {
                    results = new List<TradeResult>(_resultQueue);
                    _resultQueue.Clear();
                }
            }

            if (results == null) return;
            foreach (TradeResult result in results)
                ApplyTradeResult(result, settings);
        }

        private void ApplyTradeResult(TradeResult result, TradeSettings settings)
        {
            PendingDispatch pending = result.Pending;
            string resourceName = TradeModuleConstants.GetResourceName(pending.ResourceId);
            string label;
            switch (pending.Kind)
            {
                case PendingKind.MarketSell: label = "[Market] SELL"; break;
                case PendingKind.MarketBuy: label = "[Market] BUY"; break;
                case PendingKind.Route: label = "[Route '" + pending.RouteName + "']"; break;
                default: label = "[Player Route '" + pending.RouteName + "']"; break;
            }

            if (!result.Success)
            {
                LogError(GetVillageName(pending.VillageId) + " " + label + " " + pending.Amount + " " +
                    resourceName + " REJECTED by server: " + (result.Error ?? "unknown error"));

                // "Not enough Traders" means our local merchant count was stale (e.g.
                // the Recruit module just disbanded traders). EVERY market trade from
                // this village will fail this cycle until it resyncs — block the whole
                // village's market trading, leave the (innocent) markets cached, and
                // force a re-download so next cycle has the real merchant count.
                if (IsTraderShortageError(result.Error) &&
                    (pending.Kind == PendingKind.MarketBuy || pending.Kind == PendingKind.MarketSell))
                {
                    BlockMarketsThisCycle(pending.VillageId);
                    LogDebug(GetVillageName(pending.VillageId) +
                        ": server reports not enough traders — skipping market trades for the rest of this cycle.");
                    RequestStaleVillageRedownload(pending.VillageId);
                }
                // "Not enough resources to send" on a sell is seller-side: our local
                // resource count was stale/overstated, the target market is innocent.
                // Block this resource for the rest of the cycle so we don't re-offer
                // the same doomed sell against every other market in turn.
                else if (pending.Kind == PendingKind.MarketSell && IsResourceShortageError(result.Error))
                {
                    BlockSellThisCycle(pending.VillageId, pending.ResourceId);
                    LogDebug(GetVillageName(pending.VillageId) + ": not enough " + resourceName +
                        " to sell — skipping it for the rest of this cycle.");
                    RequestStaleVillageRedownload(pending.VillageId);
                }
                // Otherwise our cached view of that market was evidently wrong — drop
                // it so the next attempt re-fetches prices instead of repeating the
                // same rejected trade.
                else if (pending.Kind == PendingKind.MarketBuy || pending.Kind == PendingKind.MarketSell)
                {
                    InvalidateMarketCache(pending.TargetId);
                }
                return;
            }

            if (result.Unconfirmed)
                LogWarning(GetVillageName(pending.VillageId) + " " + label + " " + pending.Amount + " " +
                    resourceName + ": no server response after " + (int)PendingTimeoutSeconds +
                    "s — assuming it was sent.");
            else
                LogDebug(GetVillageName(pending.VillageId) + " " + label + " " + pending.Amount + " " +
                    resourceName + " confirmed.");

            switch (pending.Kind)
            {
                case PendingKind.MarketSell:
                    _stats.RecordSell(pending.ResourceId, pending.Amount);
                    break;

                case PendingKind.MarketBuy:
                    _stats.RecordBuy(pending.ResourceId, pending.Amount);
                    break;

                case PendingKind.Route:
                    _stats.RecordRoute(pending.ResourceId, pending.Amount);
                    break;

                case PendingKind.PlayerRoute:
                    _stats.RecordPlayerRoute(pending.ResourceId, pending.Amount);
                    if (pending.PlayerEntry != null)
                    {
                        pending.PlayerEntry.AmountSent += pending.Amount;
                        LogInfo("[Player Route '" + pending.RouteName + "'] " + resourceName +
                            " progress: " + pending.PlayerEntry.AmountSent + "/" +
                            pending.PlayerEntry.TotalAmount + ".");
                    }
                    if (pending.PlayerRoute != null && pending.PlayerRoute.IsComplete())
                    {
                        pending.PlayerRoute.Enabled = false;
                        LogInfo("[Player Route '" + pending.RouteName + "'] All resources delivered! Route auto-disabled.");
                    }
                    // Persist progress immediately so a map swap / relog doesn't reset AmountSent
                    if (settings.AutoSavePlayerRouteProgress)
                    {
                        try { Engine.SaveSettings(); }
                        catch (Exception ex) { LogError("Failed to save player route progress: " + ex.Message); }
                    }
                    break;
            }
        }

        // =====================================================================
        // Market Trading
        // =====================================================================

        /// <summary>
        /// Attempt ONE market trade for this village.
        /// Sell/buy order is determined by settings.Priority.
        /// </summary>
        private TradeActionResult TryMarketTrade(VillageMap map, int villageId, string villageName,
            VillageMarketTradeInfo villageInfo, TradeSettings settings)
        {
            // The server already rejected a trade here this cycle for lack of
            // traders — our merchant count is stale, so every market trade would
            // fail. Skip markets until the next cycle re-syncs the village.
            if (IsMarketBlockedThisCycle(villageId))
                return TradeActionResult.NoWork;

            int tradersAtHome = map.m_numTradersAtHome;
            int movingTraders = CountMovingMerchants(villageId, true);

            if (movingTraders >= settings.MerchantsTradeLimit || tradersAtHome < settings.MerchantsPerTrade)
            {
                LogDebug(villageName + " [Market]: Not enough merchants (home=" + tradersAtHome +
                         ", moving=" + movingTraders + "/" + settings.MerchantsTradeLimit + ")");
                return TradeActionResult.NoWork;
            }

            if (villageInfo.MarketTargets.Count == 0)
            {
                LogDebug(villageName + " [Market]: No markets assigned.");
                return TradeActionResult.NoWork;
            }

            bool areWeaponsAllowed = AreWeaponsAllowed();

            List<TradeTypeEntry> sortedTypes = new List<TradeTypeEntry>();
            foreach (TradeTypeEntry t in villageInfo.TradeTypes)
            {
                if (!t.Sell && !t.Buy) continue;
                if (!areWeaponsAllowed && TradeModuleConstants.IsWeapon(t.ResourceId)) continue;
                sortedTypes.Add(t);
            }

            if (sortedTypes.Count == 0)
            {
                LogDebug(villageName + " [Market]: No sell/buy resource types configured.");
                return TradeActionResult.NoWork;
            }

            sortedTypes.Sort(delegate(TradeTypeEntry a, TradeTypeEntry b)
            {
                double diffA = Math.Abs(map.getResourceLevel((int)a.ResourceId) -
                    (a.Sell ? (double)a.SellLimit : (double)a.BuyLimit));
                double diffB = Math.Abs(map.getResourceLevel((int)b.ResourceId) -
                    (b.Sell ? (double)b.SellLimit : (double)b.BuyLimit));
                double capA = (double)GetResourceCap((int)a.ResourceId);
                double capB = (double)GetResourceCap((int)b.ResourceId);
                if (capA <= 0) capA = 1;
                if (capB <= 0) capB = 1;
                return (diffB / capB).CompareTo(diffA / capA);
            });

            TradeActionResult first;
            if (settings.Priority == TradePriority.MarketBuyFirst)
            {
                first = TryMarketBuy(map, villageId, villageName, villageInfo, settings,
                    sortedTypes, tradersAtHome, movingTraders);
                if (first != TradeActionResult.NoWork) return first;
                return TryMarketSell(map, villageId, villageName, villageInfo, settings,
                    sortedTypes, tradersAtHome, movingTraders);
            }
            else
            {
                first = TryMarketSell(map, villageId, villageName, villageInfo, settings,
                    sortedTypes, tradersAtHome, movingTraders);
                if (first != TradeActionResult.NoWork) return first;
                return TryMarketBuy(map, villageId, villageName, villageInfo, settings,
                    sortedTypes, tradersAtHome, movingTraders);
            }
        }

        private TradeActionResult TryMarketSell(VillageMap map, int villageId, string villageName,
            VillageMarketTradeInfo villageInfo, TradeSettings settings,
            List<TradeTypeEntry> sortedTypes, int tradersAtHome, int movingTraders)
        {
            foreach (TradeTypeEntry tradeType in sortedTypes)
            {
                if (!tradeType.Sell) continue;
                if (tradersAtHome < settings.MerchantsPerTrade) break;
                if (movingTraders >= settings.MerchantsTradeLimit) break;

                int resourceId = (int)tradeType.ResourceId;
                // The server already told us this village can't actually supply this
                // resource this cycle — don't rotate it through the rest of the markets.
                if (IsSellBlockedThisCycle(villageId, resourceId)) continue;
                int currentAmount = (int)map.getResourceLevel(resourceId) +
                    GetPurchasedAmount(villageId, resourceId, settings.IgnoreCurrentTransactions);
                int carryLevel = GetCarryLevel(resourceId);
                int minLoad = carryLevel * settings.MerchantsPerTrade;

                int surplus = currentAmount - tradeType.SellLimit;
                if (surplus <= minLoad) continue;

                int marketStock;
                int bestMarket = FindBestMarket(villageInfo, tradeType.MinSellPrice, resourceId,
                    true, villageId, out marketStock);
                if (bestMarket <= 0) continue;

                int numMerchants = Math.Min(surplus / carryLevel, tradersAtHome);
                numMerchants = Math.Min(numMerchants, settings.MerchantsTradeLimit - movingTraders);
                if (numMerchants < settings.MerchantsPerTrade) continue;

                int amount = numMerchants * carryLevel;
                if (!map.stockExchangeTrade(bestMarket, resourceId, amount, false))
                {
                    LogDebug(villageName + " [Market]: send lock busy, retrying next tick.");
                    return TradeActionResult.Blocked;
                }

                RecordPending(PendingKind.MarketSell, villageId, bestMarket, resourceId, amount,
                    null, null, null);
                LogInfo(villageName + " [Market] SELL " + amount + " " +
                        TradeModuleConstants.GetResourceName(resourceId) +
                        " -> " + GetVillageName(bestMarket) +
                        " (" + numMerchants + " merchants, awaiting confirmation). " +
                        (tradersAtHome - numMerchants) + " merchants left.");
                return TradeActionResult.Dispatched;
            }
            return TradeActionResult.NoWork;
        }

        private TradeActionResult TryMarketBuy(VillageMap map, int villageId, string villageName,
            VillageMarketTradeInfo villageInfo, TradeSettings settings,
            List<TradeTypeEntry> sortedTypes, int tradersAtHome, int movingTraders)
        {
            foreach (TradeTypeEntry tradeType in sortedTypes)
            {
                if (!tradeType.Buy) continue;
                if (tradersAtHome < settings.MerchantsPerTrade) break;
                if (movingTraders >= settings.MerchantsTradeLimit) break;

                int resourceId = (int)tradeType.ResourceId;
                int currentAmount = (int)map.getResourceLevel(resourceId) +
                    GetPurchasedAmount(villageId, resourceId, settings.IgnoreCurrentTransactions);
                int carryLevel = GetCarryLevel(resourceId);
                int minLoad = carryLevel * settings.MerchantsPerTrade;

                // Clamp the buy target to the village's actual storage cap so a
                // limit set above the cap can't produce a deficit that never closes.
                int effectiveLimit = Math.Min(tradeType.BuyLimit, GetResourceCap(resourceId));
                int deficit = effectiveLimit - currentAmount;
                if (deficit <= minLoad) continue;

                int marketStock;
                int bestMarket = FindBestMarket(villageInfo, tradeType.MaxBuyPrice, resourceId,
                    false, villageId, out marketStock);
                if (bestMarket <= 0) continue;

                // The server rejects buys for more than the market holds.
                if (marketStock < minLoad)
                {
                    LogDebug(villageName + " [Market]: " + GetVillageName(bestMarket) + " only has " +
                        marketStock + " " + TradeModuleConstants.GetResourceName(resourceId) +
                        ", settings require " + minLoad + ". Skipping.");
                    continue;
                }

                int numMerchants = Math.Min(Math.Min(deficit, marketStock) / carryLevel, tradersAtHome);
                numMerchants = Math.Min(numMerchants, settings.MerchantsTradeLimit - movingTraders);
                if (numMerchants < settings.MerchantsPerTrade) continue;

                // Gold check (same formula as the game UI / old module): the server
                // rejects buys we can't afford, so clamp the merchant count first.
                int costPerMerchant = 0;
                try
                {
                    costPerMerchant = TradingCalcs.calcGoldCost(GameEngine.Instance.LocalWorldData,
                        marketStock, resourceId, marketStock - numMerchants * carryLevel);
                }
                catch { }
                if (costPerMerchant > 0)
                {
                    int currentGold = (int)GameEngine.Instance.World.getCurrentGold();
                    int totalCost = costPerMerchant * numMerchants;
                    if (totalCost >= currentGold)
                    {
                        int affordable = currentGold / costPerMerchant;
                        LogInfo(villageName + " [Market]: Not enough gold for " + numMerchants +
                            " merchant(s) of " + TradeModuleConstants.GetResourceName(resourceId) +
                            " (need " + totalCost + ", have " + currentGold + "). Reducing to " +
                            affordable + ".");
                        numMerchants = affordable;
                    }
                }
                if (numMerchants < settings.MerchantsPerTrade) continue;

                int amount = numMerchants * carryLevel;
                if (!map.stockExchangeTrade(bestMarket, resourceId, amount, true))
                {
                    LogDebug(villageName + " [Market]: send lock busy, retrying next tick.");
                    return TradeActionResult.Blocked;
                }

                RecordPending(PendingKind.MarketBuy, villageId, bestMarket, resourceId, amount,
                    null, null, null);
                LogInfo(villageName + " [Market] BUY " + amount + " " +
                        TradeModuleConstants.GetResourceName(resourceId) +
                        " <- " + GetVillageName(bestMarket) +
                        " (" + numMerchants + " merchants, awaiting confirmation). " +
                        (tradersAtHome - numMerchants) + " merchants left.");
                return TradeActionResult.Dispatched;
            }
            return TradeActionResult.NoWork;
        }

        /// <summary>
        /// Find the best market for a trade using cached stock levels:
        /// - Selling: lowest stock = highest price
        /// - Buying: highest stock = lowest price
        /// Markets without fresh price data are skipped — never trade blind.
        /// Returns the market id, or -1; bestStockOut holds its stock level.
        /// </summary>
        private int FindBestMarket(VillageMarketTradeInfo villageInfo, int controlPrice,
            int resourceId, bool selling, int sourceVillageId, out int bestStockOut)
        {
            bestStockOut = 0;
            if (villageInfo.MarketTargets.Count == 0)
                return -1;

            int bestMarket = -1;
            int bestStock = 0;
            double bestDistSq = double.MaxValue;
            int cachedCount = 0;
            int uncachedCount = 0;
            int priceFilteredCount = 0;

            string resourceName = TradeModuleConstants.GetResourceName(resourceId);

            foreach (int marketId in villageInfo.MarketTargets)
            {
                StockExchangeCache cache;
                bool hasFreshCache = TryGetCache(marketId, out cache) &&
                    (DateTime.Now - cache.LastUpdated).TotalMinutes < PriceCacheValidMinutes;

                if (!hasFreshCache)
                {
                    // No fresh price data — this market is not a candidate.
                    uncachedCount++;
                    continue;
                }

                cachedCount++;
                int stockLevel = cache.GetLevel(resourceId);

                // Check price control
                if (!CheckPrice(controlPrice, !selling, stockLevel, resourceId))
                {
                    priceFilteredCount++;
                    continue;
                }

                double distSq = GameEngine.Instance.World.getSquareDistance(sourceVillageId, marketId);

                bool better;
                if (bestMarket == -1)
                    better = true;
                else if (selling)
                    better = stockLevel < bestStock || (stockLevel == bestStock && distSq < bestDistSq);
                else
                    better = stockLevel > bestStock || (stockLevel == bestStock && distSq < bestDistSq);

                if (better)
                {
                    bestStock = stockLevel;
                    bestMarket = marketId;
                    bestDistSq = distSq;
                }
            }

            if (bestMarket > 0)
            {
                bestStockOut = bestStock;
                int bestPrice = 0;
                try
                {
                    bestPrice = TradingCalcs.calcSellCost(GameEngine.Instance.LocalWorldData, bestStock, resourceId);
                }
                catch { }
                LogDebug("  " + (selling ? "SELL" : "BUY") + " " + resourceName + " -> Best: " +
                    GetVillageName(bestMarket) + " stock=" + bestStock + " price=" + bestPrice +
                    " (cached " + cachedCount + "/" + villageInfo.MarketTargets.Count +
                    ", uncached " + uncachedCount + ", filtered " + priceFilteredCount + ")");
            }
            else if (cachedCount == 0 && uncachedCount > 0)
            {
                LogDebug("  " + resourceName + ": no fresh price data for any market — skipping (no blind trades).");
            }

            return bestMarket;
        }

        private bool CheckPrice(int controlPrice, bool buying, int stockAmount, int resourceId)
        {
            if (buying && controlPrice >= 150) return true;
            if (!buying && controlPrice <= 0) return true;

            try
            {
                int price = TradingCalcs.calcSellCost(GameEngine.Instance.LocalWorldData, stockAmount, resourceId);
                if (!buying)
                    return price >= controlPrice;
                else
                    return price <= controlPrice;
            }
            catch
            {
                return true;
            }
        }

        // =====================================================================
        // Village-to-Village Trading (Trade Routes)
        // =====================================================================

        /// <summary>
        /// Attempt ONE village-to-village send for this village across all applicable routes.
        /// </summary>
        private TradeActionResult TryRouteSend(VillageMap senderMap, int senderId, string senderName,
            List<TradeRouteSettings> routes, TradeSettings settings)
        {
            int tradersAtHome = senderMap.m_numTradersAtHome;
            int movingTraders = CountMovingMerchants(senderId, false);

            if (tradersAtHome < settings.MerchantsPerTrade)
            {
                LogDebug(senderName + " [Route]: No merchants at home (" + tradersAtHome + ").");
                return TradeActionResult.NoWork;
            }

            if (movingTraders >= settings.MerchantsExchangeLimit)
            {
                LogDebug(senderName + " [Route]: Exchange limit reached (" + movingTraders + "/" +
                    settings.MerchantsExchangeLimit + ").");
                return TradeActionResult.NoWork;
            }

            foreach (TradeRouteSettings route in routes)
            {
                if (!route.Enabled || route.ToVillages.Count == 0 || route.Resources.Count == 0)
                    continue;

                // Build sorted recipient list
                int squareDistLimit = route.DistanceLimit * route.DistanceLimit;
                List<int> sortedRecipients = new List<int>();
                foreach (int recipientId in route.ToVillages)
                {
                    if (recipientId == senderId) continue;
                    if (route.IsDistanceLimited)
                    {
                        int distSq = GameEngine.Instance.World.getSquareDistance(senderId, recipientId);
                        if (distSq > squareDistLimit) continue;
                    }
                    sortedRecipients.Add(recipientId);
                }

                sortedRecipients.Sort(delegate(int a, int b)
                {
                    int da = GameEngine.Instance.World.getSquareDistance(senderId, a);
                    int db = GameEngine.Instance.World.getSquareDistance(senderId, b);
                    return da.CompareTo(db);
                });

                // Sort resources by how much the sender has (most first)
                List<int> sortedResources = new List<int>(route.Resources);
                sortedResources.Sort(delegate(int a, int b)
                {
                    double la = senderMap.getResourceLevel(a);
                    double lb = senderMap.getResourceLevel(b);
                    return lb.CompareTo(la);
                });

                foreach (int recipientId in sortedRecipients)
                {
                    VillageMap recipientMap = GameEngine.Instance.getVillage(recipientId);
                    if (recipientMap == null) continue;

                    foreach (int resourceId in sortedResources)
                    {
                        double senderLevel = senderMap.getResourceLevel(resourceId);
                        if (senderLevel <= route.KeepMinimum) continue;

                        // Clamp the target to the recipient's storage cap so we
                        // don't overfill and waste resources.
                        double targetMax = Math.Min((double)route.SendMaximum,
                            (double)GetResourceCap(resourceId));

                        double recipientLevel = recipientMap.getResourceLevel(resourceId) +
                            GetPurchasedAmount(recipientId, resourceId, settings.IgnoreCurrentTransactions);
                        if (recipientLevel >= targetMax) continue;

                        double canSend = senderLevel - route.KeepMinimum;
                        double needed = targetMax - recipientLevel;
                        if (needed > canSend) needed = canSend;

                        int carryLevel = GetBaseCarryLevel(resourceId);
                        int minLoad = carryLevel * settings.MerchantsPerTrade;
                        if (needed < minLoad) continue;

                        int numMerchants = Math.Min((int)(needed / carryLevel), tradersAtHome);
                        numMerchants = Math.Min(numMerchants, route.MaxMerchantsPerTransaction);
                        numMerchants = Math.Min(numMerchants, settings.MerchantsExchangeLimit - movingTraders);

                        if (numMerchants <= 0) continue;

                        int amount = numMerchants * carryLevel;
                        if (!senderMap.sendResources(recipientId, resourceId, amount))
                        {
                            LogDebug(senderName + " [Route]: send lock busy, retrying next tick.");
                            return TradeActionResult.Blocked;
                        }

                        RecordPending(PendingKind.Route, senderId, recipientId, resourceId, amount,
                            route.Name, null, null);
                        LogInfo(senderName + " [Route '" + route.Name + "'] -> " +
                                GetVillageName(recipientId) +
                                ": " + amount + " " +
                                TradeModuleConstants.GetResourceName(resourceId) +
                                " (" + numMerchants + " merchants, awaiting confirmation). " +
                                (tradersAtHome - numMerchants) + " left.");
                        return TradeActionResult.Dispatched;
                    }
                }
            }

            return TradeActionResult.NoWork;
        }

        // =====================================================================
        // Player Route Trading
        // =====================================================================

        private TradeActionResult TryPlayerRouteSend(VillageMap senderMap, int senderId, string senderName,
            List<PlayerTradeRouteSettings> routes, TradeSettings settings)
        {
            int tradersAtHome = senderMap.m_numTradersAtHome;
            int movingTraders = CountMovingMerchants(senderId, false);

            if (tradersAtHome <= 0)
            {
                LogDebug(senderName + " [Player Route]: No merchants at home.");
                return TradeActionResult.NoWork;
            }

            if (movingTraders >= settings.MerchantsExchangeLimit)
            {
                LogDebug(senderName + " [Player Route]: Exchange limit reached (" + movingTraders + "/" +
                    settings.MerchantsExchangeLimit + ").");
                return TradeActionResult.NoWork;
            }

            foreach (PlayerTradeRouteSettings route in routes)
            {
                if (!route.Enabled || route.TargetVillageId <= 0)
                    continue;

                if (route.IsComplete())
                {
                    route.Enabled = false;
                    LogInfo("[Player Route '" + route.Name + "'] All resources delivered. Route disabled.");
                    continue;
                }

                foreach (PlayerTradeResourceEntry resEntry in route.Resources)
                {
                    if (resEntry.TotalAmount <= 0 || resEntry.Remaining <= 0)
                        continue;

                    int resourceId = resEntry.ResourceId;
                    double senderLevel = senderMap.getResourceLevel(resourceId);
                    if (senderLevel <= route.KeepMinimum) continue;

                    double canSend = senderLevel - route.KeepMinimum;
                    if (canSend > resEntry.Remaining) canSend = resEntry.Remaining;
                    if (canSend < 1) continue;

                    int carryLevel = GetBaseCarryLevel(resourceId);

                    // Honour "Min merchants/trade" here too, so the route doesn't dribble
                    // out tiny loads (e.g. 1 venison) whenever the village is barely above
                    // KeepMinimum. The one allowed exception is the final delivery: if what
                    // we can send now is the whole remaining amount, send it even if small.
                    // Capped by the route's own per-transaction max so a low max can't stall.
                    int minMerchants = Math.Min(settings.MerchantsPerTrade, route.MaxMerchantsPerTransaction);
                    if (minMerchants < 1) minMerchants = 1;
                    double minLoad = (double)minMerchants * carryLevel;
                    if (canSend < minLoad && canSend < resEntry.Remaining)
                    {
                        LogDebug(senderName + " [Player Route '" + route.Name + "']: only " +
                            (int)canSend + " " + TradeModuleConstants.GetResourceName(resourceId) +
                            " over KeepMinimum (< " + (int)minLoad + " min load) and route still needs " +
                            resEntry.Remaining + " — waiting for a fuller load.");
                        continue;
                    }

                    // Allow a final partial load (amount below carry level) so the
                    // route can actually finish instead of stalling on the remainder.
                    int numMerchants = (int)Math.Ceiling(canSend / (double)carryLevel);
                    numMerchants = Math.Min(numMerchants, tradersAtHome);
                    numMerchants = Math.Min(numMerchants, route.MaxMerchantsPerTransaction);
                    numMerchants = Math.Min(numMerchants, settings.MerchantsExchangeLimit - movingTraders);
                    if (numMerchants <= 0) continue;

                    int amount = (int)Math.Min(canSend, (double)(numMerchants * carryLevel));
                    if (amount <= 0) continue;

                    if (!senderMap.sendResources(route.TargetVillageId, resourceId, amount))
                    {
                        LogDebug(senderName + " [Player Route]: send lock busy, retrying next tick.");
                        return TradeActionResult.Blocked;
                    }

                    // Progress (AmountSent) is updated only when the server confirms
                    // the send — see ApplyTradeResult.
                    RecordPending(PendingKind.PlayerRoute, senderId, route.TargetVillageId,
                        resourceId, amount, route.Name, route, resEntry);

                    string resourceName = TradeModuleConstants.GetResourceName(resourceId);
                    LogInfo(senderName + " [Player Route '" + route.Name + "'] -> " +
                            GetVillageName(route.TargetVillageId) +
                            ": " + amount + " " + resourceName +
                            " (" + numMerchants + " merchants, awaiting confirmation; " +
                            resEntry.AmountSent + "/" + resEntry.TotalAmount + " confirmed so far). " +
                            (tradersAtHome - numMerchants) + " merchants left.");
                    return TradeActionResult.Dispatched;
                }
            }

            return TradeActionResult.NoWork;
        }

        // =====================================================================
        // Helpers
        // =====================================================================

        /// <summary>
        /// Takes a thread-safe snapshot of the live trader array.
        /// Retries only the snapshot acquisition if a background RPC thread modifies the
        /// collection mid-enumeration; all filtering then runs on the local copy which
        /// can never be concurrently modified, eliminating the spin-wait freeze.
        /// </summary>
        private static List<WorldMap.LocalTrader> SnapshotTraderArray()
        {
            SparseArray traderArray = GameEngine.Instance.World.getTraderArray();
            while (true)
            {
                try
                {
                    var snapshot = new List<WorldMap.LocalTrader>();
                    foreach (WorldMap.LocalTrader localTrader in traderArray)
                        snapshot.Add(localTrader);
                    return snapshot;
                }
                catch (InvalidOperationException)
                {
                    // Array modified mid-snapshot by a background thread; yield and retry.
                    System.Threading.Thread.Sleep(0);
                }
            }
        }

        private int GetPurchasedAmount(int villageId, int resourceId, bool ignoreTransactions)
        {
            if (ignoreTransactions)
                return 0;

            List<WorldMap.LocalTrader> traders = SnapshotTraderArray();
            int buying = 0;
            int incoming = 0;
            foreach (WorldMap.LocalTrader localTrader in traders)
            {
                MarketTraderData trader = localTrader.trader;
                if (trader.homeVillageID == villageId && trader.resource == resourceId &&
                    (trader.traderState == 5 || trader.traderState == 6))
                    buying += trader.amount;
                if (trader.targetVillageID == villageId && trader.resource == resourceId &&
                    trader.traderState == 1)
                    incoming += trader.amount;
            }
            return buying + incoming;
        }

        private int CountMovingMerchants(int villageId, bool toMarkets)
        {
            List<WorldMap.LocalTrader> traders = SnapshotTraderArray();
            int marketCount = 0;
            int villageCount = 0;
            foreach (WorldMap.LocalTrader localTrader in traders)
            {
                MarketTraderData trader = localTrader.trader;
                if (trader.homeVillageID == villageId)
                {
                    if (GameEngine.Instance.World.isCapital(trader.targetVillageID))
                        marketCount += trader.numTraders;
                    else
                        villageCount += trader.numTraders;
                }
            }
            return toMarkets ? marketCount : villageCount;
        }

        private void AutoHireMerchants(VillageMap map, TradeSettings settings)
        {
            try
            {
                int maxMerchants = Math.Min(settings.AutoHireMerchantsLimit,
                    map.countWorkingMarkets() *
                    ResearchData.numMerchantGuildsTraders[
                        (int)GameEngine.Instance.World.userResearchData.Research_Merchant_Guilds]);

                int currentMerchants = map.numTraders();
                if (currentMerchants >= maxMerchants) return;

                int needed = maxMerchants - currentMerchants;
                int spareWorkers = map.m_spareWorkers;
                if (needed > spareWorkers)
                    needed = spareWorkers;

                int unitSize = GameEngine.Instance.LocalWorldData.UnitSize_Trader;
                int usedCapacity = map.calcUnitUsages() + map.LocallyMade_Traders * unitSize;
                int freeCapacity = GameEngine.Instance.LocalWorldData.Village_UnitCapacity - usedCapacity;
                if (freeCapacity < needed * unitSize)
                    needed = freeCapacity / unitSize;

                int gold = (int)GameEngine.Instance.World.getCurrentGold();
                int costPerTrader = GameEngine.Instance.LocalWorldData.TraderGoldCost;
                if (gold < needed * costPerTrader)
                    needed = gold / costPerTrader;

                if (needed <= 0)
                {
                    LogDebug(GetVillageName(map.VillageID) + ": Cannot hire merchants (no gold/recruits/space).");
                    return;
                }

                map.makeTroops(-5, needed, false);
                LogInfo(GetVillageName(map.VillageID) + ": Hiring " + needed + " merchant(s).");
            }
            catch (Exception ex)
            {
                LogError("Auto-hire merchants failed: " + ex.Message);
            }
        }

        public void AddMarketsForAllVillages(double maxDistance)
        {
            TradeSettings settings = Settings;
            if (settings == null) return;

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            RefreshCapitalsList();
            double maxDistSq = maxDistance * maxDistance;

            List<int> ids = GameEngine.Instance.World.getUserVillageIDList();
            if (ids == null) return;

            foreach (int villageId in ids)
            {
                VillageMarketTradeInfo info = settings.GetVillageMarketInfo(villageId);
                AddMarketsForVillage(villageId, info, maxDistSq);
            }
        }

        public void AddMarketsForVillage(int villageId, VillageMarketTradeInfo info, double maxDistSq)
        {
            if (_capitalsList == null) return;

            info.MarketTargets.Clear();
            int count = 0;

            for (int i = 0; i < _capitalsList.Length; i++)
            {
                int capitalId = _capitalsList[i];
                if (GameEngine.Instance.World.allowExchangeTrade(capitalId, villageId) &&
                    GameEngine.Instance.World.getSquareDistance(villageId, capitalId) <= maxDistSq)
                {
                    info.MarketTargets.Add(capitalId);
                    count++;
                }
            }

            LogInfo(GetVillageName(villageId) + ": Added " + count + " markets.");
        }

        public void UpdateStockExchangeCache(int villageId, StockExchangePanel.StockExchangeInfo data)
        {
            if (data == null) return;
            StockExchangeCache cache = new StockExchangeCache();
            cache.LastUpdated = DateTime.Now;
            cache.Data = data;
            lock (_cacheLock)
            {
                _stockExchangeCache[villageId] = cache;
            }
        }

        private bool TryGetCache(int marketId, out StockExchangeCache cache)
        {
            lock (_cacheLock)
            {
                return _stockExchangeCache.TryGetValue(marketId, out cache);
            }
        }

        private bool HasFreshCache(int marketId)
        {
            StockExchangeCache cache;
            return TryGetCache(marketId, out cache) &&
                (DateTime.Now - cache.LastUpdated).TotalMinutes < PriceCacheValidMinutes;
        }

        private void InvalidateMarketCache(int marketId)
        {
            lock (_cacheLock)
            {
                _stockExchangeCache.Remove(marketId);
            }
        }

        // Whether a server rejection means the seller didn't have the resource it
        // tried to send (as opposed to a market/price problem). Matched on the
        // localized error text since the numeric code lives in CommonTypes.
        private static bool IsResourceShortageError(string error)
        {
            return error != null &&
                   error.ToLowerInvariant().IndexOf("enough resource") >= 0;
        }

        // Server says the village hasn't got the merchants the trade needs — our
        // local `m_numTradersAtHome` was stale (e.g. Recruit just disbanded some).
        private static bool IsTraderShortageError(string error)
        {
            return error != null &&
                   error.ToLowerInvariant().IndexOf("enough trader") >= 0;
        }

        private void BlockMarketsThisCycle(int villageId)
        {
            _marketBlockedThisCycle.Add(villageId);
        }

        private bool IsMarketBlockedThisCycle(int villageId)
        {
            return _marketBlockedThisCycle.Contains(villageId);
        }

        // A resource-shortage rejection proves this village's local resource count
        // was stale. If enabled, ask Village Sync to force a full re-download of it
        // so the next cycle works from authoritative data. The request is
        // deduplicated on the sync side, so calling it per rejection is fine.
        private void RequestStaleVillageRedownload(int villageId)
        {
            try
            {
                if (Engine == null || Engine.Settings == null ||
                    !Engine.Settings.VillageSync.AutoRefreshOnStaleError)
                    return;

                VillageSyncModule vsm = Engine.GetModule<VillageSyncModule>();
                if (vsm != null)
                {
                    vsm.RequestForceRedownload(villageId);
                    LogDebug(GetVillageName(villageId) +
                        ": queued full re-download (stale resource data).");
                }
            }
            catch { }
        }

        // Tick-thread only. Records / queries resources this village can't supply
        // this cycle; the set is reset per village in BeginVillage.
        private void BlockSellThisCycle(int villageId, int resourceId)
        {
            HashSet<int> blocked;
            if (!_sellBlockedThisCycle.TryGetValue(villageId, out blocked))
            {
                blocked = new HashSet<int>();
                _sellBlockedThisCycle[villageId] = blocked;
            }
            blocked.Add(resourceId);
        }

        private bool IsSellBlockedThisCycle(int villageId, int resourceId)
        {
            HashSet<int> blocked;
            return _sellBlockedThisCycle.TryGetValue(villageId, out blocked) &&
                   blocked.Contains(resourceId);
        }

        private bool AreWeaponsAllowed()
        {
            try
            {
                return GameEngine.Instance.LocalWorldData.EraWorld ||
                       !GameEngine.Instance.World.FourthAgeWorld ||
                       GameEngine.Instance.World.SixthAgeWorld;
            }
            catch
            {
                return true;
            }
        }

        private static int GetCarryLevel(int resourceId)
        {
            try
            {
                return CardTypes.adjustTraderCarryLevels(
                    GameEngine.Instance.cardsManager.UserCardData,
                    GameEngine.Instance.LocalWorldData.traderCarryingLevels[resourceId]);
            }
            catch
            {
                return 1;
            }
        }

        private static int GetBaseCarryLevel(int resourceId)
        {
            try
            {
                return GameEngine.Instance.LocalWorldData.traderCarryingLevels[resourceId];
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// The village's actual storage cap for a resource (research + cards),
        /// matching what the game's own panels display. Falls back to rough
        /// constants if the lookup fails.
        /// </summary>
        private static int GetResourceCap(int resourceId)
        {
            try
            {
                double cap = GameEngine.Instance.World.UserResearchData.getResourceCap(
                        GameEngine.Instance.LocalWorldData, resourceId, false) *
                    CardTypes.getResourceCapMultiplier(resourceId,
                        GameEngine.Instance.cardsManager.UserCardData);
                if (cap > 0) return (int)cap;
            }
            catch
            {
            }

            switch (resourceId)
            {
                case 6: case 7: case 8: case 9:
                    return 50000;
                default:
                    return 10000;
            }
        }

        private static string GetVillageName(int villageId)
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                return GameEngine.Instance.World.getVillageName(villageId);
            return "Village " + villageId;
        }

        private class StockExchangeCache
        {
            public DateTime LastUpdated = DateTime.MinValue;
            public StockExchangePanel.StockExchangeInfo Data;

            public int GetLevel(int resourceId)
            {
                if (Data == null) return 0;
                return Data.getLevel(resourceId);
            }
        }

        // =====================================================================
        // Session Statistics
        // =====================================================================

        public class TradeSessionStats
        {
            public DateTime SessionStart = DateTime.Now;
            public long SessionStartGold = 0;
            public readonly Dictionary<int, long> SoldByResource   = new Dictionary<int, long>();
            public readonly Dictionary<int, long> BoughtByResource = new Dictionary<int, long>();
            public readonly Dictionary<int, long> SentByResource   = new Dictionary<int, long>();
            public int TotalSellTrades;
            public int TotalBuyTrades;
            public int TotalRouteSends;
            public int TotalPlayerRouteSends;

            public void RecordSell(int resourceId, int amount)
            {
                AddTo(SoldByResource, resourceId, amount);
                TotalSellTrades++;
            }

            public void RecordBuy(int resourceId, int amount)
            {
                AddTo(BoughtByResource, resourceId, amount);
                TotalBuyTrades++;
            }

            public void RecordRoute(int resourceId, int amount)
            {
                AddTo(SentByResource, resourceId, amount);
                TotalRouteSends++;
            }

            public void RecordPlayerRoute(int resourceId, int amount)
            {
                AddTo(SentByResource, resourceId, amount);
                TotalPlayerRouteSends++;
            }

            private static void AddTo(Dictionary<int, long> d, int key, long value)
            {
                if (!d.ContainsKey(key)) d[key] = 0;
                d[key] += value;
            }

            public void Reset()
            {
                SessionStart = DateTime.Now;
                SessionStartGold = 0;
                SoldByResource.Clear();
                BoughtByResource.Clear();
                SentByResource.Clear();
                TotalSellTrades = 0;
                TotalBuyTrades = 0;
                TotalRouteSends = 0;
                TotalPlayerRouteSends = 0;
            }
        }
    }
}
