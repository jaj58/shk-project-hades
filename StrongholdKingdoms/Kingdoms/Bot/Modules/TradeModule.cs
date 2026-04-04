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

        // Cached stock exchange data
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

        // Price fetch state
        private bool _waitingForPrices;
        private DateTime _priceFetchStarted = DateTime.MinValue;
        private int _priceFetchVillageId = -1;
        private const double PriceFetchTimeoutSeconds = 15.0;
        private const double PriceCacheValidMinutes = 3.0;

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

        protected override void OnInitialize()
        {
            _currentVillageIndex = 0;
            _villageQueue.Clear();
            _waitingForPrices = false;
        }

        protected override void OnTick()
        {
            TradeSettings settings = Settings;
            if (settings == null) return;

            // If waiting for price data, check timeout
            if (_waitingForPrices)
            {
                if ((DateTime.Now - _priceFetchStarted).TotalSeconds > PriceFetchTimeoutSeconds)
                {
                    LogWarning("Price fetch timed out for village " + _priceFetchVillageId + ", proceeding without prices.");
                    _waitingForPrices = false;
                }
                else
                {
                    // Check if we got the data
                    if (HasFreshPricesForVillage(_priceFetchVillageId, settings))
                    {
                        LogDebug("Price data received for " + GetVillageName(_priceFetchVillageId));
                        _waitingForPrices = false;
                    }
                    else
                    {
                        return; // Still waiting
                    }
                }

                // Now process the village we were waiting on
                _lastVillageAction = DateTime.Now;
                ProcessVillage(_priceFetchVillageId, settings);
                _priceFetchVillageId = -1;

                if (_currentVillageIndex >= _villageQueue.Count)
                    LogInfo("Trade cycle complete. Processed " + _villageQueue.Count + " village(s).");
                return;
            }

            // If we're mid-cycle, process next village
            if (_villageQueue.Count > 0 && _currentVillageIndex < _villageQueue.Count)
            {
                if ((DateTime.Now - _lastVillageAction).TotalMilliseconds < settings.DelayBetweenVillagesMs)
                    return;

                int villageId = _villageQueue[_currentVillageIndex];
                _currentVillageIndex++;

                // Check if this village needs market trading and needs price data
                VillageMarketTradeInfo marketInfo = settings.GetVillageMarketInfo(villageId);
                if (marketInfo.IsTrading && marketInfo.MarketTargets.Count > 0 &&
                    !HasFreshPricesForVillage(villageId, settings))
                {
                    // Need to fetch prices first
                    RequestPrices(villageId, marketInfo, settings);
                    return;
                }

                _lastVillageAction = DateTime.Now;
                ProcessVillage(villageId, settings);

                if (_currentVillageIndex >= _villageQueue.Count)
                    LogInfo("Trade cycle complete. Processed " + _villageQueue.Count + " village(s).");
                return;
            }

            // Not mid-cycle: check if enough time has passed to start a new one
            double elapsed = (DateTime.Now - _lastFullCycle).TotalSeconds;
            if (elapsed < settings.CycleIntervalSeconds)
                return;

            _currentVillageIndex = 0;
            _lastFullCycle = DateTime.Now;
            RefreshCapitalsList();
            BuildUnifiedVillageQueue(settings);

            LogInfo("Starting trade cycle. " + _villageQueue.Count + " village(s) to process. Priority: " +
                (settings.PrioritiseMarkets ? "Markets" : "Village Routes"));

            if (_villageQueue.Count == 0)
            {
                LogDebug("No villages to trade.");
                return;
            }
        }

        private bool HasFreshPricesForVillage(int villageId, TradeSettings settings)
        {
            VillageMarketTradeInfo info = settings.GetVillageMarketInfo(villageId);
            if (info.MarketTargets.Count == 0) return true;

            // Check if at least one market has fresh cache
            foreach (int marketId in info.MarketTargets)
            {
                StockExchangeCache cache;
                if (_stockExchangeCache.TryGetValue(marketId, out cache) &&
                    (DateTime.Now - cache.LastUpdated).TotalMinutes < PriceCacheValidMinutes)
                    return true;
            }
            return false;
        }

        private void RequestPrices(int villageId, VillageMarketTradeInfo marketInfo, TradeSettings settings)
        {
            _waitingForPrices = true;
            _priceFetchStarted = DateTime.Now;
            _priceFetchVillageId = villageId;

            // Build list of markets we need data for (up to 20, sorted by distance)
            List<int> marketsToFetch = new List<int>();
            foreach (int marketId in marketInfo.MarketTargets)
            {
                StockExchangeCache cache;
                if (!_stockExchangeCache.TryGetValue(marketId, out cache) ||
                    (DateTime.Now - cache.LastUpdated).TotalMinutes >= PriceCacheValidMinutes)
                {
                    marketsToFetch.Add(marketId);
                }
            }

            if (marketsToFetch.Count > 20)
                marketsToFetch.RemoveRange(20, marketsToFetch.Count - 20);

            LogDebug(GetVillageName(villageId) + ": Fetching prices from " + marketsToFetch.Count + " market(s)...");

            try
            {
                RemoteServices.Instance.set_GetStockExchangeData_UserCallBack(
                    new RemoteServices.GetStockExchangeData_UserCallBack(OnStockExchangeDataReceived));

                if (marketsToFetch.Count > 0 && GameEngine.Instance.World.isAccountPremium())
                {
                    // Premium: fetch multiple at once
                    RemoteServices.Instance.GetStockExchangePremiumData(villageId, marketsToFetch.ToArray());
                }
                else
                {
                    // Standard: fetch just the first market
                    int targetMarket = marketsToFetch.Count > 0 ? marketsToFetch[0] : marketInfo.MarketTargets[0];
                    RemoteServices.Instance.GetStockExchangeData(targetMarket, true);
                }
            }
            catch (System.Exception ex)
            {
                LogError("Failed to request stock exchange data: " + ex.Message);
                _waitingForPrices = false;
            }
        }

        private void OnStockExchangeDataReceived(GetStockExchangeData_ReturnType returnData)
        {
            if (returnData == null || !returnData.Success) return;

            // Store the main market data
            StockExchangePanel.StockExchangeInfo info = new StockExchangePanel.StockExchangeInfo();
            info.lastTime = DateTime.Now;
            info.villageID = returnData.villageID;
            info.woodLevel = returnData.woodLevel;
            info.stoneLevel = returnData.stoneLevel;
            info.ironLevel = returnData.ironLevel;
            info.pitchLevel = returnData.pitchLevel;
            info.aleLevel = returnData.aleLevel;
            info.applesLevel = returnData.applesLevel;
            info.breadLevel = returnData.breadLevel;
            info.meatLevel = returnData.meatLevel;
            info.cheeseLevel = returnData.cheeseLevel;
            info.vegLevel = returnData.vegLevel;
            info.fishLevel = returnData.fishLevel;
            info.bowsLevel = returnData.bowsLevel;
            info.pikesLevel = returnData.pikesLevel;
            info.swordsLevel = returnData.swordsLevel;
            info.armourLevel = returnData.armourLevel;
            info.catapultsLevel = returnData.catapultsLevel;
            info.furnitureLevel = returnData.furnitureLevel;
            info.clothesLevel = returnData.clothesLevel;
            info.saltLevel = returnData.saltLevel;
            info.venisonLevel = returnData.venisonLevel;
            info.silkLevel = returnData.silkLevel;
            info.spicesLevel = returnData.spicesLevel;
            info.metalwareLevel = returnData.metalwareLevel;
            info.wineLevel = returnData.wineLevel;
            UpdateStockExchangeCache(returnData.villageID, info);

            // Store premium multi-market data if available
            if (returnData.otherVillages != null)
            {
                foreach (GetStockExchangeData_ReturnType other in returnData.otherVillages)
                {
                    StockExchangePanel.StockExchangeInfo otherInfo = new StockExchangePanel.StockExchangeInfo();
                    otherInfo.lastTime = DateTime.Now;
                    otherInfo.villageID = other.villageID;
                    otherInfo.woodLevel = other.woodLevel;
                    otherInfo.stoneLevel = other.stoneLevel;
                    otherInfo.ironLevel = other.ironLevel;
                    otherInfo.pitchLevel = other.pitchLevel;
                    otherInfo.aleLevel = other.aleLevel;
                    otherInfo.applesLevel = other.applesLevel;
                    otherInfo.breadLevel = other.breadLevel;
                    otherInfo.meatLevel = other.meatLevel;
                    otherInfo.cheeseLevel = other.cheeseLevel;
                    otherInfo.vegLevel = other.vegLevel;
                    otherInfo.fishLevel = other.fishLevel;
                    otherInfo.bowsLevel = other.bowsLevel;
                    otherInfo.pikesLevel = other.pikesLevel;
                    otherInfo.swordsLevel = other.swordsLevel;
                    otherInfo.armourLevel = other.armourLevel;
                    otherInfo.catapultsLevel = other.catapultsLevel;
                    otherInfo.furnitureLevel = other.furnitureLevel;
                    otherInfo.clothesLevel = other.clothesLevel;
                    otherInfo.saltLevel = other.saltLevel;
                    otherInfo.venisonLevel = other.venisonLevel;
                    otherInfo.silkLevel = other.silkLevel;
                    otherInfo.spicesLevel = other.spicesLevel;
                    otherInfo.metalwareLevel = other.metalwareLevel;
                    otherInfo.wineLevel = other.wineLevel;
                    UpdateStockExchangeCache(other.villageID, otherInfo);
                }
            }
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
                        bool hasMarkets = info.IsTrading;
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

            LogDebug("Queue built: " + _villageQueue.Count + " village(s). " +
                     _routesBySender.Count + " village(s) with routes. " +
                     _playerRoutesBySender.Count + " village(s) with player routes.");
        }

        /// <summary>
        /// Process one village: do the prioritised trade type first, then the other if merchants remain.
        /// Only ONE server call per type (respecting the 45-second cooldown on VillageMap).
        /// </summary>
        private void ProcessVillage(int villageId, TradeSettings settings)
        {
            VillageMap map = GameEngine.Instance.getVillage(villageId);
            if (map == null)
            {
                LogDebug("Village " + villageId + " not loaded, skipping.");
                return;
            }

            string villageName = GetVillageName(villageId);

            if (settings.AutoHireMerchants)
                AutoHireMerchants(map, settings);

            VillageMarketTradeInfo marketInfo = settings.GetVillageMarketInfo(villageId);
            bool hasMarkets = marketInfo.IsTrading;
            List<TradeRouteSettings> villageRoutes;
            bool hasRoutes = _routesBySender.TryGetValue(villageId, out villageRoutes);
            List<PlayerTradeRouteSettings> playerRoutes;
            bool hasPlayerRoutes = _playerRoutesBySender.TryGetValue(villageId, out playerRoutes);

            if (!hasMarkets && !hasRoutes && !hasPlayerRoutes)
            {
                LogDebug(villageName + ": No market trading, routes or player routes configured.");
                return;
            }

            LogDebug(villageName + ": Processing (markets=" + hasMarkets + ", routes=" + hasRoutes +
                ", playerRoutes=" + hasPlayerRoutes + ", merchants=" + map.m_numTradersAtHome + ")");

            if (settings.PrioritiseMarkets)
            {
                if (hasMarkets)
                    TryMarketTrade(map, villageId, villageName, marketInfo, settings);
                if (hasRoutes)
                    TryRouteSend(map, villageId, villageName, villageRoutes, settings);
            }
            else
            {
                if (hasRoutes)
                    TryRouteSend(map, villageId, villageName, villageRoutes, settings);
                if (hasMarkets)
                    TryMarketTrade(map, villageId, villageName, marketInfo, settings);
            }

            // Player routes always processed last (they're one-off deliveries)
            if (hasPlayerRoutes)
                TryPlayerRouteSend(map, villageId, villageName, playerRoutes, settings);
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
        // Market Trading
        // =====================================================================

        /// <summary>
        /// Attempt ONE market trade (buy or sell) for this village.
        /// Returns true if a trade was dispatched.
        /// </summary>
        private bool TryMarketTrade(VillageMap map, int villageId, string villageName,
            VillageMarketTradeInfo villageInfo, TradeSettings settings)
        {
            int tradersAtHome = map.m_numTradersAtHome;
            int movingTraders = CountMovingMerchants(villageId, true);

            if (movingTraders >= settings.MerchantsTradeLimit || tradersAtHome < settings.MerchantsPerTrade)
            {
                LogDebug(villageName + " [Market]: Not enough merchants (home=" + tradersAtHome +
                         ", moving=" + movingTraders + "/" + settings.MerchantsTradeLimit + ")");
                return false;
            }

            if (villageInfo.MarketTargets.Count == 0)
            {
                LogDebug(villageName + " [Market]: No markets assigned.");
                return false;
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
                return false;
            }

            sortedTypes.Sort(delegate(TradeTypeEntry a, TradeTypeEntry b)
            {
                double diffA = Math.Abs(map.getResourceLevel((int)a.ResourceId) -
                    (a.Sell ? (double)a.SellLimit : (double)a.BuyLimit));
                double diffB = Math.Abs(map.getResourceLevel((int)b.ResourceId) -
                    (b.Sell ? (double)b.SellLimit : (double)b.BuyLimit));
                double capA = GetResourceCap((int)a.ResourceId);
                double capB = GetResourceCap((int)b.ResourceId);
                if (capA <= 0) capA = 1;
                if (capB <= 0) capB = 1;
                return (diffB / capB).CompareTo(diffA / capA);
            });

            foreach (TradeTypeEntry tradeType in sortedTypes)
            {
                if (tradersAtHome < settings.MerchantsPerTrade) break;
                if (movingTraders >= settings.MerchantsTradeLimit) break;

                int resourceId = (int)tradeType.ResourceId;
                int currentAmount = (int)map.getResourceLevel(resourceId) +
                    GetPurchasedAmount(villageId, resourceId, settings.IgnoreCurrentTransactions);

                int carryLevel = GetCarryLevel(resourceId);
                int minLoad = carryLevel * settings.MerchantsPerTrade;
                string resourceName = TradeModuleConstants.GetResourceName(resourceId);

                // Sell logic
                int surplus = currentAmount - tradeType.SellLimit;
                if (tradeType.Sell && surplus > minLoad)
                {
                    int bestMarket = FindBestMarket(villageInfo, tradeType.MinSellPrice, resourceId,
                        true, villageId);
                    if (bestMarket > 0)
                    {
                        int numMerchants = Math.Min(surplus / carryLevel, tradersAtHome);
                        numMerchants = Math.Min(numMerchants, settings.MerchantsTradeLimit - movingTraders);
                        if (numMerchants > 0)
                        {
                            int amount = numMerchants * carryLevel;
                            bool sent = map.stockExchangeTrade(bestMarket, resourceId, amount, false);
                            if (sent)
                            {
                                LogInfo(villageName + " [Market] SELL " + amount + " " + resourceName +
                                        " -> " + GetVillageName(bestMarket) +
                                        ". " + (tradersAtHome - numMerchants) + " merchants left.");
                                return true;
                            }
                            else
                            {
                                LogDebug(villageName + " [Market]: Cooldown active, skipping.");
                                return false;
                            }
                        }
                    }
                    continue;
                }

                // Buy logic
                int deficit = tradeType.BuyLimit - currentAmount;
                if (tradeType.Buy && deficit > minLoad)
                {
                    int bestMarket = FindBestMarket(villageInfo, tradeType.MaxBuyPrice, resourceId,
                        false, villageId);
                    if (bestMarket > 0)
                    {
                        int numMerchants = Math.Min(deficit / carryLevel, tradersAtHome);
                        numMerchants = Math.Min(numMerchants, settings.MerchantsTradeLimit - movingTraders);
                        if (numMerchants > 0)
                        {
                            int amount = numMerchants * carryLevel;
                            bool sent = map.stockExchangeTrade(bestMarket, resourceId, amount, true);
                            if (sent)
                            {
                                LogInfo(villageName + " [Market] BUY " + amount + " " + resourceName +
                                        " <- " + GetVillageName(bestMarket) +
                                        ". " + (tradersAtHome - numMerchants) + " merchants left.");
                                return true;
                            }
                            else
                            {
                                LogDebug(villageName + " [Market]: Cooldown active, skipping.");
                                return false;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Find the best market for a trade. Uses stock levels to calculate prices:
        /// - Selling: pick the market where the price is highest (lowest stock = highest price)
        /// - Buying: pick the market where the price is lowest (highest stock = lowest price)
        /// Falls back to nearest market if no cache data available.
        /// </summary>
        private int FindBestMarket(VillageMarketTradeInfo villageInfo, int controlPrice,
            int resourceId, bool selling, int sourceVillageId)
        {
            if (villageInfo.MarketTargets.Count == 0)
                return -1;

            int bestMarket = -1;
            int bestStock = selling ? int.MaxValue : int.MinValue;
            double bestDistSq = double.MaxValue;
            bool foundCachedMarket = false;

            foreach (int marketId in villageInfo.MarketTargets)
            {
                StockExchangeCache cache;
                bool hasFreshCache = _stockExchangeCache.TryGetValue(marketId, out cache) &&
                    (DateTime.Now - cache.LastUpdated).TotalMinutes < PriceCacheValidMinutes;

                if (hasFreshCache)
                {
                    int stockLevel = cache.GetLevel(resourceId);

                    // Check price control
                    if (!CheckPrice(controlPrice, !selling, stockLevel, resourceId))
                        continue;

                    double distSq = GameEngine.Instance.World.getSquareDistance(sourceVillageId, marketId);

                    if (selling)
                    {
                        // For selling: lower stock = higher price = better for seller
                        if (!foundCachedMarket || stockLevel < bestStock ||
                            (stockLevel == bestStock && distSq < bestDistSq))
                        {
                            bestStock = stockLevel;
                            bestMarket = marketId;
                            bestDistSq = distSq;
                            foundCachedMarket = true;
                        }
                    }
                    else
                    {
                        // For buying: higher stock = lower price = better for buyer
                        if (!foundCachedMarket || stockLevel > bestStock ||
                            (stockLevel == bestStock && distSq < bestDistSq))
                        {
                            bestStock = stockLevel;
                            bestMarket = marketId;
                            bestDistSq = distSq;
                            foundCachedMarket = true;
                        }
                    }
                }
                else if (!foundCachedMarket)
                {
                    // Fallback: no cache, just pick nearest
                    double distSq = GameEngine.Instance.World.getSquareDistance(sourceVillageId, marketId);
                    if (distSq < bestDistSq)
                    {
                        bestDistSq = distSq;
                        bestMarket = marketId;
                    }
                }
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
        /// Returns true if a send was dispatched.
        /// </summary>
        private bool TryRouteSend(VillageMap senderMap, int senderId, string senderName,
            List<TradeRouteSettings> routes, TradeSettings settings)
        {
            int tradersAtHome = senderMap.m_numTradersAtHome;
            int movingTraders = CountMovingMerchants(senderId, false);

            if (tradersAtHome < settings.MerchantsPerTrade)
            {
                LogDebug(senderName + " [Route]: No merchants at home (" + tradersAtHome + ").");
                return false;
            }

            if (movingTraders >= settings.MerchantsExchangeLimit)
            {
                LogDebug(senderName + " [Route]: Exchange limit reached (" + movingTraders + "/" + settings.MerchantsExchangeLimit + ").");
                return false;
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

                        double recipientLevel = recipientMap.getResourceLevel(resourceId) +
                            GetPurchasedAmount(recipientId, resourceId, settings.IgnoreCurrentTransactions);
                        if (recipientLevel >= route.SendMaximum) continue;

                        double canSend = senderLevel - route.KeepMinimum;
                        double needed = route.SendMaximum - recipientLevel;
                        if (needed > canSend) needed = canSend;

                        int carryLevel = GetCarryLevel(resourceId);
                        int minLoad = carryLevel * settings.MerchantsPerTrade;
                        if (needed < minLoad) continue;

                        int numMerchants = Math.Min((int)(needed / carryLevel), tradersAtHome);
                        numMerchants = Math.Min(numMerchants, route.MaxMerchantsPerTransaction);
                        numMerchants = Math.Min(numMerchants, settings.MerchantsExchangeLimit - movingTraders);

                        if (numMerchants <= 0) continue;

                        int amount = numMerchants * carryLevel;
                        senderMap.sendResources(recipientId, resourceId, amount);

                        LogInfo(senderName + " [Route '" + route.Name + "'] -> " +
                                GetVillageName(recipientId) +
                                ": " + amount + " " +
                                TradeModuleConstants.GetResourceName(resourceId) +
                                " (" + numMerchants + " merchants). " +
                                (tradersAtHome - numMerchants) + " left.");
                        return true; // Only ONE send per village per tick
                    }
                }
            }

            return false;
        }

        // =====================================================================
        // Player Route Trading
        // =====================================================================

        private bool TryPlayerRouteSend(VillageMap senderMap, int senderId, string senderName,
            List<PlayerTradeRouteSettings> routes, TradeSettings settings)
        {
            int tradersAtHome = senderMap.m_numTradersAtHome;
            int movingTraders = CountMovingMerchants(senderId, false);

            if (tradersAtHome <= 0)
            {
                LogDebug(senderName + " [Player Route]: No merchants at home.");
                return false;
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
                    int remaining = resEntry.Remaining;
                    if (canSend > remaining) canSend = remaining;

                    int carryLevel = GetCarryLevel(resourceId);
                    if (canSend < carryLevel) continue;

                    // Override min merchants — send even with 1 merchant to ensure all goods are sent
                    int numMerchants = Math.Min((int)(canSend / carryLevel), tradersAtHome);
                    numMerchants = Math.Min(numMerchants, route.MaxMerchantsPerTransaction);
                    numMerchants = Math.Min(numMerchants, settings.MerchantsExchangeLimit - movingTraders);

                    if (numMerchants <= 0) continue;

                    int amount = numMerchants * carryLevel;
                    // Don't send more than remaining
                    if (amount > remaining)
                    {
                        numMerchants = (int)System.Math.Ceiling((double)remaining / carryLevel);
                        if (numMerchants <= 0) numMerchants = 1;
                        numMerchants = Math.Min(numMerchants, tradersAtHome);
                        amount = numMerchants * carryLevel;
                    }

                    senderMap.sendResources(route.TargetVillageId, resourceId, amount);
                    resEntry.AmountSent += amount;

                    string resourceName = TradeModuleConstants.GetResourceName(resourceId);
                    LogInfo(senderName + " [Player Route '" + route.Name + "'] -> " +
                            GetVillageName(route.TargetVillageId) +
                            ": " + amount + " " + resourceName +
                            " (" + resEntry.AmountSent + "/" + resEntry.TotalAmount + " sent). " +
                            (tradersAtHome - numMerchants) + " merchants left.");

                    if (route.IsComplete())
                    {
                        route.Enabled = false;
                        LogInfo("[Player Route '" + route.Name + "'] All resources delivered! Route auto-disabled.");
                    }

                    return true; // One send per village per tick
                }
            }

            return false;
        }

        // =====================================================================
        // Helpers
        // =====================================================================

        private int GetPurchasedAmount(int villageId, int resourceId, bool ignoreTransactions)
        {
            if (ignoreTransactions)
                return 0;

            SparseArray traderArray = GameEngine.Instance.World.getTraderArray();
            int buying = 0;
            int incoming = 0;
            bool done;

            do
            {
                try
                {
                    buying = 0;
                    incoming = 0;
                    foreach (WorldMap.LocalTrader localTrader in traderArray)
                    {
                        MarketTraderData trader = localTrader.trader;
                        if (trader.homeVillageID == villageId && trader.resource == resourceId &&
                            (trader.traderState == 5 || trader.traderState == 6))
                            buying += trader.amount;
                        if (trader.targetVillageID == villageId && trader.resource == resourceId &&
                            trader.traderState == 1)
                            incoming += trader.amount;
                    }
                    done = true;
                }
                catch (InvalidOperationException)
                {
                    done = false;
                }
            }
            while (!done);

            return buying + incoming;
        }

        private int CountMovingMerchants(int villageId, bool toMarkets)
        {
            SparseArray traderArray = GameEngine.Instance.World.getTraderArray();
            int marketCount = 0;
            int villageCount = 0;
            bool done;

            do
            {
                try
                {
                    marketCount = 0;
                    villageCount = 0;
                    foreach (WorldMap.LocalTrader localTrader in traderArray)
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
                    done = true;
                }
                catch (InvalidOperationException)
                {
                    done = false;
                }
            }
            while (!done);

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
            _stockExchangeCache[villageId] = cache;
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

        private static double GetResourceCap(int resourceId)
        {
            try
            {
                switch (resourceId)
                {
                    case 6: case 7: case 8: case 9:
                        return 50000;
                    default:
                        return 10000;
                }
            }
            catch
            {
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
    }
}
