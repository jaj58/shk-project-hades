using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    /// <summary>
    /// Sender (formerly Banquet Sender) — shares goods between players in a group:
    /// banquet goods, resources, food and weapons (see SenderGoods for the catalogue).
    ///
    /// The API (api/banquet_api.php) does all the planning, per category. This module:
    ///   1. Reports every non-capital village (storage buildings and caps, goods, production,
    ///      merchants, when it was last downloaded) and every catalogue-good merchant on the road.
    ///   2. Executes the send orders leased to this player, one per tick, through
    ///      VillageSendGate so it never collides with the Trade module.
    ///   3. Reports each order's outcome with the real merchant ID and arrival time.
    ///   4. Re-downloads villages the API says have arrived goods it can't see yet.
    ///
    /// Each category can be switched off and given its own merchant reserve in the bot; the
    /// API is told, so it neither sends to nor plans from this player for that category.
    ///
    /// Game objects are only touched on the tick thread. HTTP runs on a short-lived
    /// background thread; its response is picked up on the next tick.
    /// </summary>
    public class BanquetSenderModule : BotModuleBase
    {
        private const string GateOwner = "BanquetSender";
        private const double SendTimeoutSeconds = 60.0;
        private const double LeaseSafetySeconds = 10.0;
        private const double RefreshThrottleSeconds = 120.0;
        private const int MaxBlockedAttempts = 30;
        private const int MaxActivityLines = 200; // shared by every category's sub-tab
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        private class Order
        {
            public long OrderId;
            public int FromVillageId;
            public int ToVillageId;
            public string ToVillageName;
            public string ToPlayerName;
            public int Good;
            public string Category;
            public int Amount;
            public DateTime LeaseExpires;
            public int BlockedAttempts;
        }

        private class PendingSend
        {
            public Order Order;
            public int ResourceId;
            public int Amount;
            public DateTime DispatchedAt;
        }

        private class RefreshWanted
        {
            public long AfterGame;
            public DateTime LastAttempt = DateTime.MinValue;
            public int Attempts;
        }

        /// <summary>One category's numbers, for its sub-tab.</summary>
        public class CategoryStatus
        {
            // null until the first sync; "" when the API is too old to know this category.
            public string Mode;
            public int OpenOrders;
            public int SentCount;
            public long SentAmount;

            public CategoryStatus Copy()
            {
                return (CategoryStatus)MemberwiseClone();
            }
        }

        public class ActivityLine
        {
            public string Category; // "" = not tied to a category
            public string Text;
        }

        public class Status
        {
            public bool Configured;
            public DateTime LastSyncOk = DateTime.MinValue;
            public string LastError = "";
            public int PlayersOnline;
            public int InFlight;
            public int OpenOrders;
            public int SentCount;
            public long SentAmount;
            public string Current = "";
            public string CurrentCategory = "";
            public List<ActivityLine> Activity = new List<ActivityLine>();
            public Dictionary<string, CategoryStatus> Categories = NewCategoryStatuses();

            public CategoryStatus Category(string category)
            {
                CategoryStatus c;
                return Categories.TryGetValue(category, out c) ? c : new CategoryStatus();
            }
        }

        private static Dictionary<string, CategoryStatus> NewCategoryStatuses()
        {
            Dictionary<string, CategoryStatus> d = new Dictionary<string, CategoryStatus>();
            foreach (string c in SenderGoods.Categories) d[c] = new CategoryStatus();
            return d;
        }

        // ── Tick-thread state ────────────────────────────────────────────────
        private readonly List<Order> _orders = new List<Order>();
        private readonly Dictionary<long, DateTime> _handledOrders = new Dictionary<long, DateTime>();
        private readonly List<Dictionary<string, object>> _outcomes = new List<Dictionary<string, object>>();
        private readonly Dictionary<int, RefreshWanted> _refreshWanted = new Dictionary<int, RefreshWanted>();
        private PendingSend _pending;
        private DateTime _lastSyncStarted = DateTime.MinValue;
        private bool _syncSoon = true;
        private double _secPerTile;

        // ── Cross-thread state ───────────────────────────────────────────────
        private readonly object _lock = new object();
        private readonly List<SendMarketResources_ReturnType> _sendResults = new List<SendMarketResources_ReturnType>();
        private bool _syncInFlight;
        private int _syncOutcomeCount;
        private Dictionary<string, object> _syncResponse;
        private string _syncError;
        private readonly Status _status = new Status();

        public override string ModuleName
        {
            get { return "Sender"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(2); }
        }

        private BanquetSenderSettings Settings
        {
            get { return Engine != null && Engine.Settings != null ? Engine.Settings.BanquetSender : null; }
        }

        // =====================================================================
        // Lifecycle
        // =====================================================================

        protected override void OnInitialize()
        {
            ResetRuntimeState();
            VillageSendGate.Register(GateOwner, OnSendResult);
        }

        protected override void OnShutdown()
        {
            VillageSendGate.Unregister(GateOwner);
            if (Enabled) PostLeaveAsync();
        }

        protected override void OnEnable()
        {
            _syncSoon = true;
        }

        protected override void OnDisable()
        {
            PostLeaveAsync();
            _orders.Clear();
            _refreshWanted.Clear();
            // A send still waiting for its reply can't be recalled; the API adopts the
            // merchant from our trader list on the next sync after re-enable.
            _pending = null;
            SetCurrent("");
        }

        private void ResetRuntimeState()
        {
            _orders.Clear();
            _handledOrders.Clear();
            _outcomes.Clear();
            _refreshWanted.Clear();
            _pending = null;
            _syncSoon = true;
            lock (_lock)
            {
                _sendResults.Clear();
                _syncResponse = null;
                _syncError = null;
            }
        }

        // =====================================================================
        // Public API for the UI
        // =====================================================================

        public Status GetStatus()
        {
            lock (_lock)
            {
                Status s = new Status();
                s.Configured = _status.Configured;
                s.LastSyncOk = _status.LastSyncOk;
                s.LastError = _status.LastError;
                s.PlayersOnline = _status.PlayersOnline;
                s.InFlight = _status.InFlight;
                s.OpenOrders = _orders.Count + (_pending != null ? 1 : 0);
                s.SentCount = _status.SentCount;
                s.SentAmount = _status.SentAmount;
                s.Current = _status.Current;
                s.CurrentCategory = _status.CurrentCategory;
                s.Activity = new List<ActivityLine>(_status.Activity);
                foreach (KeyValuePair<string, CategoryStatus> kv in _status.Categories)
                {
                    CategoryStatus c = kv.Value.Copy();
                    c.OpenOrders = 0;
                    s.Categories[kv.Key] = c;
                }
                foreach (Order o in _orders) s.Category(o.Category).OpenOrders++;
                if (_pending != null) s.Category(_pending.Order.Category).OpenOrders++;
                return s;
            }
        }

        public void SyncNow()
        {
            _syncSoon = true;
        }

        /// <summary>The group website for an API URL (…/api/banquet_api.php → …/api/banquet/).</summary>
        public static string WebsiteUrlFor(string apiUrl)
        {
            if (string.IsNullOrEmpty(apiUrl)) return "";
            int slash = apiUrl.LastIndexOf('/');
            return slash >= 0 ? apiUrl.Substring(0, slash + 1) + "banquet/" : apiUrl;
        }

        // =====================================================================
        // Tick
        // =====================================================================

        protected override void OnTick()
        {
            BanquetSenderSettings settings = Settings;
            if (settings == null) return;

            bool configured = !string.IsNullOrEmpty(settings.ApiUrl) &&
                              settings.GroupKey != null && settings.GroupKey.Trim().Length >= 6;
            lock (_lock) _status.Configured = configured;
            if (!configured) return;
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;

            DrainSendResults();
            CheckSendTimeout();
            ConsumeSyncResponse();

            bool syncDue = _syncSoon ||
                (DateTime.Now - _lastSyncStarted).TotalSeconds >= Math.Max(10, settings.SyncIntervalSeconds);
            bool syncInFlight;
            lock (_lock) syncInFlight = _syncInFlight;
            if (syncDue && !syncInFlight) StartSync(settings);

            ExecuteNextOrder(settings);
            HandleRefreshRequests(settings);
        }

        // =====================================================================
        // Sync (report + receive orders)
        // =====================================================================

        private void StartSync(BanquetSenderSettings settings)
        {
            Dictionary<string, object> payload;
            int outcomeCount;
            try
            {
                payload = BuildSyncPayload(settings);
            }
            catch (Exception ex)
            {
                LogWarning("Could not build report: " + ex.Message);
                _lastSyncStarted = DateTime.Now;
                return;
            }
            if (payload == null) return; // villages not loaded yet

            outcomeCount = _outcomes.Count;
            payload["results"] = new List<object>(_outcomes.ToArray());

            _lastSyncStarted = DateTime.Now;
            _syncSoon = false;
            lock (_lock)
            {
                _syncInFlight = true;
                _syncOutcomeCount = outcomeCount;
            }

            string url = settings.ApiUrl.Trim();
            string key = settings.GroupKey.Trim();
            Thread t = new Thread(delegate ()
            {
                Dictionary<string, object> resp = null;
                string error = null;
                try
                {
                    resp = PostJson(url, key, "sync", payload);
                    if (resp == null) error = "Empty response from API.";
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                lock (_lock)
                {
                    _syncResponse = resp;
                    _syncError = error;
                    _syncInFlight = false;
                }
            });
            t.IsBackground = true;
            t.Name = "BanquetSender Sync";
            t.Start();
        }

        private Dictionary<string, object> BuildSyncPayload(BanquetSenderSettings settings)
        {
            WorldMap world = GameEngine.Instance.World;
            WorldData worldData = GameEngine.Instance.LocalWorldData;
            CardData cardData = GameEngine.Instance.cardsManager.UserCardData;
            List<WorldMap.UserVillageData> userVillages = world.getUserVillageList();
            if (userVillages == null || userVillages.Count == 0) return null;

            DateTime serverNow = VillageMap.getCurrentServerTime();
            long gameNow = ToUnix(serverNow);

            // Merchant travel time per tile, the same formula the game's transfer panel uses.
            double t1000 = 1000.0 * worldData.traderMoveSpeed * worldData.gamePlaySpeed;
            t1000 = world.UserResearchData.adjustTradeTimes(t1000) * CardTypes.cards_adjustTradeTimes(cardData);
            t1000 = CardTypes.cards_adjustTradeTimesCompleteDelivery(cardData, t1000);
            _secPerTile = t1000 / 1000.0;

            List<object> carry = new List<object>();
            for (int g = 0; g < SenderGoods.Count; g++)
                carry.Add(Math.Max(1, worldData.traderCarryingLevels[SenderGoods.ResourceIds[g]]));

            // Potential production per banquet good across all downloaded villages, with the
            // cards in play and with none — for the website's totals (what halls make while running).
            CardData noCards = new CardData();
            noCards.cards = new int[0];
            noCards.cardsExpiry = new DateTime[0];
            double[] prodWithCards = new double[SenderGoods.BanquetCount];
            double[] prodWithoutCards = new double[SenderGoods.BanquetCount];

            List<object> villages = new List<object>();
            List<object> allIds = new List<object>();
            HashSet<int> ownIds = new HashSet<int>();
            foreach (WorldMap.UserVillageData uvd in userVillages)
            {
                int vid = uvd.villageID;
                ownIds.Add(vid);
                if (world.isCapital(vid)) continue;
                allIds.Add(vid);

                VillageMap map = GameEngine.Instance.getVillage(vid);
                if (map == null) continue; // not downloaded yet: the API keeps its last report

                villages.Add(BuildVillageReport(map, vid, settings, worldData, cardData, gameNow));
                AddPotentialProduction(map, worldData, cardData, noCards, prodWithCards, prodWithoutCards);
            }

            Dictionary<string, object> player = new Dictionary<string, object>();
            player["user_id"] = RemoteServices.Instance.UserID;
            player["name"] = AutoBombMultiModule.GetLocalPlayerName();
            player["world"] = Program.WorldName ?? "";
            player["game_time"] = gameNow;
            player["craftsmanship"] = (int)world.UserResearchData.Research_Craftsmanship;
            player["sec_per_tile"] = _secPerTile;
            player["carry"] = carry;
            player["client_version"] = System.Windows.Forms.Application.ProductVersion ?? "";
            // Which categories this player takes part in, and the merchants each keeps back.
            Dictionary<string, object> client = new Dictionary<string, object>();
            foreach (string category in SenderGoods.Categories)
            {
                Dictionary<string, object> c = new Dictionary<string, object>();
                c["enabled"] = settings.IsCategoryEnabled(category);
                c["reserve"] = Math.Max(0, settings.GetCategoryReserve(category));
                client[category] = c;
            }
            player["client"] = client;
            Dictionary<string, object> cards = new Dictionary<string, object>();
            cards["hall_multiplier"] = CardTypes.getResourceCapMultiplier(SenderGoods.ResourceIds[0], cardData);
            double tradeTimeFactor = CardTypes.cards_adjustTradeTimes(cardData);
            cards["merchant_speed"] = tradeTimeFactor > 0 ? Math.Round(1.0 / tradeTimeFactor, 2) : 1.0;
            List<object> withCards = new List<object>();
            List<object> withoutCards = new List<object>();
            for (int g = 0; g < SenderGoods.BanquetCount; g++)
            {
                withCards.Add(Math.Round(prodWithCards[g], 1));
                withoutCards.Add(Math.Round(prodWithoutCards[g], 1));
            }
            cards["prod_with_cards"] = withCards;
            cards["prod_without_cards"] = withoutCards;
            cards["in_play"] = BuildCardsInPlay(cardData, serverNow);
            player["cards"] = cards;

            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload["player"] = player;
            payload["villages"] = villages;
            payload["all_village_ids"] = allIds;
            payload["outbound"] = BuildOutbound(ownIds, serverNow);
            return payload;
        }

        /// <summary>
        /// Adds each finished banquet-good building's production per day, as if it is running,
        /// once with the cards in play and once with none. Same formula as the game's own
        /// per-day figure when placing a building (VillageMap.updatePlacementText): 86400 /
        /// production time to the Village Hall × payload. Production cards multiply the payload.
        /// Unlike building.calcRate this doesn't drop to 0 when the hall is full.
        /// </summary>
        private static void AddPotentialProduction(VillageMap map, WorldData worldData, CardData cardData,
            CardData noCards, double[] withCards, double[] withoutCards)
        {
            try
            {
                VillageMapBuilding hall = map.findBuildingType(0);
                if (hall == null) return;
                ResearchData research = GameEngine.Instance.World.UserResearchData;

                foreach (VillageMapBuilding b in new List<VillageMapBuilding>(map.Buildings))
                {
                    int g = SenderGoods.GoodIndex(b.buildingType);
                    if (g < 0 || g >= SenderGoods.BanquetCount || !b.complete) continue;

                    double travel = VillageBuildingsData.calcTravelTimeTiled(worldData, hall.buildingLocation,
                        b.buildingLocation).TotalSeconds;
                    double payload = worldData.getPayloadSize(b.buildingType);

                    double time = VillageBuildingsData.calcProductionTime(worldData, research, b.buildingType, travel,
                        0.0, 1, map.VillageMapType, map.m_parishCapitalResearchData, cardData);
                    if (time > 0)
                        withCards[g] += 86400.0 / time * CardTypes.adjustPayloadSize(cardData, payload, b.buildingType);

                    double baseTime = VillageBuildingsData.calcProductionTime(worldData, research, b.buildingType, travel,
                        0.0, 1, map.VillageMapType, map.m_parishCapitalResearchData, noCards);
                    if (baseTime > 0)
                        withoutCards[g] += 86400.0 / baseTime * CardTypes.adjustPayloadSize(noCards, payload, b.buildingType);
                }
            }
            catch
            {
                // Building list changed mid-read or the village isn't fully loaded: skip it this sync.
            }
        }

        /// <summary>
        /// Cards in play that matter to banquet goods, with seconds left at sync time.
        /// Type IDs verified against CommonTypes.dll (production x3/x5/x10 via adjustPayloadSize,
        /// Lavish Banqueting via getBanquetHonourValue, 2822 via getResourceCapMultiplier on venison).
        /// </summary>
        private static List<object> BuildCardsInPlay(CardData cardData, DateTime serverNow)
        {
            List<object> list = new List<object>();
            if (cardData == null || cardData.cards == null) return list;
            for (int i = 0; i < cardData.cards.Length; i++)
            {
                if (cardData.cards[i] == 0) continue;
                int type;
                try { type = CardTypes.getCardType(cardData.cards[i]); }
                catch { continue; }

                string name = BanquetCardName(type);
                if (name == null) continue;

                long left = 0;
                if (cardData.cardsExpiry != null && i < cardData.cardsExpiry.Length)
                    left = (long)(cardData.cardsExpiry[i] - serverNow).TotalSeconds;
                if (left <= 0) continue;

                Dictionary<string, object> c = new Dictionary<string, object>();
                c["id"] = type;
                c["name"] = name;
                c["expires_in_sec"] = left;
                list.Add(c);
            }
            return list;
        }

        private static string BanquetCardName(int type)
        {
            // 1284..1307: three tiers per banquet good, in SenderGoods (banquet) order.
            if (type >= 1284 && type <= 1307)
            {
                string[] tiers = { "x3", "x5", "x10" };
                return SenderGoods.Name((type - 1284) / 3) + " production " + tiers[(type - 1284) % 3];
            }
            switch (type)
            {
                case 1281: return "Lavish Banqueting (honour x1.2)";
                case 1282: return "Advanced Lavish Banqueting (honour x2)";
                case 1283: return "Expert Lavish Banqueting (honour x3)";
                case 1537: return "Carters (merchants x2)";
                case 1538: return "Advanced Carters (merchants x4)";
                case 1539: return "Expert Carters (merchants x8)";
                case 2822: return "Expanded Keep Storage (hall x2)";
                default: return null;
            }
        }

        private static Dictionary<string, object> BuildVillageReport(VillageMap map, int vid,
            BanquetSenderSettings settings, WorldData worldData, CardData cardData, long gameNow)
        {
            WorldMap world = GameEngine.Instance.World;

            // Which storage buildings exist: goods sent to a village without one can't be held.
            Dictionary<string, object> storage = new Dictionary<string, object>();
            foreach (string s in SenderGoods.Storages) storage[s] = SenderGoods.HasStorage(map, s);
            bool hasHall = (bool)storage[SenderGoods.Hall];

            int n = SenderGoods.Count;
            double[] prod = new double[n];
            int[] buildings = new int[n];
            int hallCap = int.MaxValue;
            List<object> caps = new List<object>();
            for (int g = 0; g < n; g++)
            {
                int cap = VillageCaps.GetResourceCap(SenderGoods.ResourceIds[g], false);
                caps.Add(cap);
                if (g < SenderGoods.BanquetCount) hallCap = Math.Min(hallCap, cap);
            }

            List<VillageMapBuilding> snapshot;
            try { snapshot = new List<VillageMapBuilding>(map.Buildings); }
            catch { snapshot = new List<VillageMapBuilding>(); }
            foreach (VillageMapBuilding b in snapshot)
            {
                // Production buildings have the type id of the good they make.
                int g = SenderGoods.GoodIndex(b.buildingType);
                if (g < 0 || !b.complete) continue;
                buildings[g]++;
                if (b.calcRate > 0 && SenderGoods.ReportsProduction(g))
                {
                    prod[g] += 86400.0 / b.calcRate * CardTypes.adjustPayloadSize(cardData,
                        worldData.getPayloadSize(b.buildingType), b.buildingType);
                }
            }

            List<object> levels = new List<object>();
            List<object> prodOut = new List<object>();
            List<object> buildingsOut = new List<object>();
            for (int g = 0; g < n; g++)
            {
                bool stored = (bool)storage[SenderGoods.Storage(g)];
                levels.Add(stored ? (int)Math.Floor(LevelOf(map, g)) : 0);
                prodOut.Add(Math.Round(prod[g], 2));
                buildingsOut.Add(buildings[g]);
            }
            int home = Math.Max(0, map.m_numTradersAtHome);

            Point loc = world.getVillageLocation(vid);
            Dictionary<string, object> v = new Dictionary<string, object>();
            v["village_id"] = vid;
            v["name"] = world.getVillageName(vid) ?? "";
            v["x"] = loc.X;
            v["y"] = loc.Y;
            // has_hall / hall_cap are what APIs from before every good was covered read.
            v["has_hall"] = hasHall;
            v["hall_cap"] = hallCap == int.MaxValue ? 0 : hallCap;
            v["caps"] = caps;
            v["storage"] = storage;
            v["levels"] = levels;
            v["prod"] = prodOut;
            v["buildings"] = buildingsOut;
            v["levels_at_game"] = gameNow;
            // m_lastServerReply starts as the LOCAL clock until the first server reply; never
            // let a PC clock ahead of the game server claim a download newer than now (that
            // would settle in-flight shipments before their goods are really visible).
            v["snapshot_at_game"] = Math.Min(ToUnix(map.m_lastServerReply), gameNow);
            // merchants_free: older APIs (banquet reserve). merchants_home: the API takes off each
            // category's reserve from the player's client settings.
            v["merchants_free"] = Math.Max(0, home - Math.Max(0, settings.GetCategoryReserve(SenderGoods.Banquet)));
            v["merchants_home"] = home;
            return v;
        }

        /// <summary>Every merchant of ours carrying a catalogue good to a non-capital village.</summary>
        private static List<object> BuildOutbound(HashSet<int> ownIds, DateTime serverNow)
        {
            List<object> list = new List<object>();
            WorldMap world = GameEngine.Instance.World;
            foreach (WorldMap.LocalTrader lt in TradeModule.SnapshotTraderArray())
            {
                MarketTraderData t = lt.trader;
                if (t == null || !ownIds.Contains(t.homeVillageID)) continue;
                if (t.traderState != 1 || t.amount <= 0 || t.endTime <= serverNow) continue;
                int g = SenderGoods.GoodIndex(t.resource);
                if (g < 0 || world.isCapital(t.targetVillageID)) continue;

                Dictionary<string, object> o = new Dictionary<string, object>();
                o["trader_id"] = t.traderID;
                o["from_village_id"] = t.homeVillageID;
                o["to_village_id"] = t.targetVillageID;
                o["good"] = g;
                o["amount"] = t.amount;
                o["eta_game"] = ToUnix(t.endTime);
                list.Add(o);
            }
            return list;
        }

        private void ConsumeSyncResponse()
        {
            Dictionary<string, object> resp;
            string error;
            int sentOutcomes;
            lock (_lock)
            {
                if (_syncInFlight || (_syncResponse == null && _syncError == null)) return;
                resp = _syncResponse;
                error = _syncError;
                sentOutcomes = _syncOutcomeCount;
                _syncResponse = null;
                _syncError = null;
            }

            if (resp != null && !GetBool(resp, "ok"))
                error = "API: " + GetStr(resp, "error", "unknown error");

            if (error != null)
            {
                SetError(error);
                LogWarning("Sync failed: " + error);
                return; // outcomes stay queued and go out with the next sync
            }

            // The server has applied the outcomes that went out with this sync.
            _outcomes.RemoveRange(0, Math.Min(sentOutcomes, _outcomes.Count));

            // Each category's mode. An API from before every good was covered only sends
            // "mode", which is the banquet one.
            Dictionary<string, object> modes = resp.ContainsKey("modes") ? resp["modes"] as Dictionary<string, object> : null;
            lock (_lock)
            {
                _status.LastSyncOk = DateTime.Now;
                _status.LastError = "";
                _status.PlayersOnline = GetInt(resp, "players_online");
                _status.InFlight = GetInt(resp, "in_flight");
                foreach (string c in SenderGoods.Categories)
                {
                    string m = modes != null ? GetStr(modes, c, "off")
                        : (c == SenderGoods.Banquet ? GetStr(resp, "mode", "off") : "");
                    _status.Category(c).Mode = m;
                }
            }

            // Replace the order queue with what the server currently leases to us, minus
            // anything already dispatched/skipped (its outcome may still be in transit).
            // The server only leases orders for categories that are switched on.
            HashSet<long> offered = new HashSet<long>();
            List<Order> fresh = new List<Order>();
            IList orders = resp.ContainsKey("orders") ? resp["orders"] as IList : null;
            if (orders != null)
            {
                foreach (object item in orders)
                {
                    Dictionary<string, object> o = item as Dictionary<string, object>;
                    if (o == null) continue;
                    Order order = new Order();
                    order.OrderId = GetLong(o, "order_id");
                    order.FromVillageId = GetInt(o, "from_village_id");
                    order.ToVillageId = GetInt(o, "to_village_id");
                    order.ToVillageName = GetStr(o, "to_village_name", "");
                    order.ToPlayerName = GetStr(o, "to_player_name", "");
                    order.Good = GetInt(o, "good");
                    order.Amount = GetInt(o, "amount");
                    order.LeaseExpires = DateTime.Now.AddSeconds(GetInt(o, "lease_remaining_sec"));
                    if (order.OrderId <= 0 || order.Good < 0 || order.Good >= SenderGoods.Count) continue;
                    order.Category = SenderGoods.Category(order.Good);
                    offered.Add(order.OrderId);
                    if (_handledOrders.ContainsKey(order.OrderId)) continue;
                    if (_pending != null && _pending.Order.OrderId == order.OrderId) continue;

                    // Keep the retry counter of an order we already had queued.
                    foreach (Order existing in _orders)
                        if (existing.OrderId == order.OrderId) { order.BlockedAttempts = existing.BlockedAttempts; break; }
                    fresh.Add(order);
                }
            }
            lock (_lock)
            {
                _orders.Clear();
                _orders.AddRange(fresh);
            }

            // Forget handled orders the server no longer offers once they're old enough
            // that no in-transit response could still contain them.
            List<long> forget = new List<long>();
            foreach (KeyValuePair<long, DateTime> kv in _handledOrders)
                if (!offered.Contains(kv.Key) && (DateTime.Now - kv.Value).TotalMinutes > 10) forget.Add(kv.Key);
            foreach (long id in forget) _handledOrders.Remove(id);

            IList refresh = resp.ContainsKey("refresh") ? resp["refresh"] as IList : null;
            if (refresh != null)
            {
                foreach (object item in refresh)
                {
                    Dictionary<string, object> r = item as Dictionary<string, object>;
                    if (r == null) continue;
                    int vid = GetInt(r, "village_id");
                    RefreshWanted want;
                    if (!_refreshWanted.TryGetValue(vid, out want))
                    {
                        want = new RefreshWanted();
                        _refreshWanted[vid] = want;
                    }
                    want.AfterGame = GetLong(r, "after_game");
                }
            }

            if (fresh.Count > 0)
                LogDebug("Sync: " + fresh.Count + " order(s) to send.");
        }

        // =====================================================================
        // Executing orders
        // =====================================================================

        private void ExecuteNextOrder(BanquetSenderSettings settings)
        {
            if (_pending != null || VillageSendGate.IsBusy) return;

            while (_orders.Count > 0)
            {
                Order order = _orders[0];
                if ((order.LeaseExpires - DateTime.Now).TotalSeconds < LeaseSafetySeconds)
                {
                    Skip(order, "lease about to expire");
                    continue;
                }

                // Switched off here since the last sync: the next sync tells the API.
                if (!settings.IsCategoryEnabled(order.Category))
                {
                    Skip(order, SenderGoods.CategoryName(order.Category) + " is off in the bot");
                    continue;
                }

                WorldMap world = GameEngine.Instance.World;
                VillageMap map = GameEngine.Instance.getVillage(order.FromVillageId);
                if (map == null || !world.isUserVillage(order.FromVillageId))
                {
                    Skip(order, "sending village not loaded");
                    continue;
                }

                int resourceId = SenderGoods.ResourceIds[order.Good];
                int carry = Math.Max(1, GameEngine.Instance.LocalWorldData.traderCarryingLevels[resourceId]);
                string storage = SenderGoods.Storage(order.Good);
                if (!SenderGoods.HasStorage(map, storage))
                {
                    Skip(order, "sending village has no " + storage);
                    continue;
                }
                int level = (int)Math.Floor(LevelOf(map, order.Good));
                int merchants = map.m_numTradersAtHome - Math.Max(0, settings.GetCategoryReserve(order.Category));

                // Local data can be fresher than what the planner saw: never send more
                // than we have, and shrink to whole merchant loads when we have to shrink.
                int amount = Math.Min(order.Amount, Math.Min(level, Math.Max(0, merchants) * carry));
                if (amount < order.Amount) amount = amount / carry * carry;
                if (amount <= 0)
                {
                    Skip(order, "not enough " + SenderGoods.Name(order.Good) + " or merchants (have " + level +
                        ", " + Math.Max(0, merchants) + " merchants)");
                    continue;
                }

                if (!VillageSendGate.TryClaim(GateOwner, order.FromVillageId))
                    return; // another module's send is in flight

                if (!map.sendResources(order.ToVillageId, resourceId, amount))
                {
                    VillageSendGate.Release(GateOwner, order.FromVillageId);
                    if (++order.BlockedAttempts >= MaxBlockedAttempts)
                        Skip(order, "village send lock stayed busy");
                    return;
                }

                lock (_lock) _orders.RemoveAt(0);
                _handledOrders[order.OrderId] = DateTime.Now;
                _pending = new PendingSend();
                _pending.Order = order;
                _pending.ResourceId = resourceId;
                _pending.Amount = amount;
                _pending.DispatchedAt = DateTime.Now;

                SetCurrent(Describe(order, amount) + " — waiting for server", order.Category);
                LogInfo(Describe(order, amount) + " dispatched, awaiting confirmation.");
                return; // one send per tick
            }
        }

        private void Skip(Order order, string reason)
        {
            lock (_lock) _orders.Remove(order);
            _handledOrders[order.OrderId] = DateTime.Now;
            AddOutcome(order.OrderId, "skipped", null, 0, 0, reason);
            LogDebug("Order " + order.OrderId + " skipped: " + reason);
        }

        // RPC thread — queue only.
        private void OnSendResult(SendMarketResources_ReturnType result)
        {
            lock (_lock) _sendResults.Add(result);
        }

        private void DrainSendResults()
        {
            List<SendMarketResources_ReturnType> results;
            lock (_lock)
            {
                if (_sendResults.Count == 0) return;
                results = new List<SendMarketResources_ReturnType>(_sendResults);
                _sendResults.Clear();
            }

            foreach (SendMarketResources_ReturnType r in results)
            {
                PendingSend p = _pending;
                if (p == null || r.villageID != p.Order.FromVillageId) continue;
                _pending = null;
                SetCurrent("");

                if (!r.Success)
                {
                    string error;
                    try { error = ErrorCodes.getErrorString(r.m_errorCode, r.m_errorID); }
                    catch { error = "error code " + r.m_errorCode; }
                    AddOutcome(p.Order.OrderId, "rejected", null, p.Amount, 0, error);
                    LogError(Describe(p.Order, p.Amount) + " REJECTED by server: " + error);
                    AddActivity(p.Order.Category, "REJECTED " + Describe(p.Order, p.Amount) + ": " + error);
                    // Our local goods/merchant counts were evidently stale.
                    if (error != null && error.IndexOf("enough", StringComparison.OrdinalIgnoreCase) >= 0)
                        RequestRedownload(p.Order.FromVillageId);
                    continue;
                }

                long traderId;
                long eta = FindNewTraderEta(r, p, out traderId);
                AddOutcome(p.Order.OrderId, "sent", traderId > 0 ? (object)traderId : null, p.Amount, eta, null);
                lock (_lock)
                {
                    _status.SentCount++;
                    _status.SentAmount += p.Amount;
                    CategoryStatus cs = _status.Category(p.Order.Category);
                    cs.SentCount++;
                    cs.SentAmount += p.Amount;
                }
                string etaText = eta > 0 ? " (arrives in " + FormatDuration(eta - ToUnix(VillageMap.getCurrentServerTime())) + ")" : "";
                LogInfo(Describe(p.Order, p.Amount) + " confirmed" + etaText + ".");
                AddActivity(p.Order.Category, "Sent " + Describe(p.Order, p.Amount) + etaText);
                _syncSoon = true; // let the planner know quickly
            }
        }

        /// <summary>The merchant this send created and its arrival (game unix time), or an estimate.</summary>
        private long FindNewTraderEta(SendMarketResources_ReturnType r, PendingSend p, out long traderId)
        {
            traderId = 0;
            MarketTraderData best = null;
            if (r.traders != null)
            {
                HashSet<long> justStarted = new HashSet<long>();
                if (r.tradersJustStarting != null)
                    foreach (long id in r.tradersJustStarting) justStarted.Add(id);

                foreach (MarketTraderData t in r.traders)
                {
                    if (t == null || t.targetVillageID != p.Order.ToVillageId || t.resource != p.ResourceId) continue;
                    if (justStarted.Count > 0 && !justStarted.Contains(t.traderID)) continue;
                    // Prefer the exact amount, then the latest start.
                    if (best == null
                        || (t.amount == p.Amount && best.amount != p.Amount)
                        || ((t.amount == p.Amount) == (best.amount == p.Amount) && t.startTime > best.startTime))
                        best = t;
                }
            }
            if (best != null)
            {
                traderId = best.traderID;
                return ToUnix(best.endTime);
            }

            // Fall back to an estimate from distance; the API corrects it when the
            // merchant shows up in our outbound list.
            try
            {
                Point a = GameEngine.Instance.World.getVillageLocation(p.Order.FromVillageId);
                Point b = GameEngine.Instance.World.getVillageLocation(p.Order.ToVillageId);
                double dist = Math.Sqrt((double)(a.X - b.X) * (a.X - b.X) + (double)(a.Y - b.Y) * (a.Y - b.Y));
                return ToUnix(VillageMap.getCurrentServerTime()) + (long)Math.Ceiling(dist * _secPerTile);
            }
            catch
            {
                return 0;
            }
        }

        private void CheckSendTimeout()
        {
            if (_pending == null) return;
            if ((DateTime.Now - _pending.DispatchedAt).TotalSeconds < SendTimeoutSeconds) return;
            // No outcome is reported: if the merchant did leave, the API adopts it from
            // our trader list; if it didn't, the lease expires and is re-planned.
            LogWarning(Describe(_pending.Order, _pending.Amount) + ": no server response after " +
                (int)SendTimeoutSeconds + "s.");
            _pending = null;
            SetCurrent("");
        }

        private void AddOutcome(long orderId, string outcome, object traderId, int amount, long eta, string error)
        {
            Dictionary<string, object> o = new Dictionary<string, object>();
            o["order_id"] = orderId;
            o["outcome"] = outcome;
            if (traderId != null) o["trader_id"] = traderId;
            if (amount > 0) o["amount"] = amount;
            if (eta > 0) o["eta_game"] = eta;
            if (error != null) o["error"] = error;
            _outcomes.Add(o);
        }

        // =====================================================================
        // Refreshing villages with arrived goods
        // =====================================================================

        private void HandleRefreshRequests(BanquetSenderSettings settings)
        {
            if (_refreshWanted.Count == 0) return;
            if (!settings.RefreshArrivedVillages)
            {
                _refreshWanted.Clear();
                return;
            }

            List<int> done = new List<int>();
            bool refreshedOne = false;
            foreach (KeyValuePair<int, RefreshWanted> kv in _refreshWanted)
            {
                VillageMap map = GameEngine.Instance.getVillage(kv.Key);
                if (map != null && ToUnix(map.m_lastServerReply) >= kv.Value.AfterGame)
                {
                    done.Add(kv.Key); // fresh enough — the next sync settles it
                    _syncSoon = true;
                    continue;
                }
                if (refreshedOne || (DateTime.Now - kv.Value.LastAttempt).TotalSeconds < RefreshThrottleSeconds)
                    continue;

                kv.Value.LastAttempt = DateTime.Now;
                kv.Value.Attempts++;
                VillageSyncModule sync = Engine.GetModule<VillageSyncModule>();
                if (sync == null) continue;
                // Light refresh first; fall back to an authoritative re-download (which
                // needs Village Sync enabled to drain) if the snapshot doesn't move.
                if (kv.Value.Attempts <= 2 || !sync.Enabled)
                    sync.RefreshVillageNow(kv.Key);
                else
                    sync.RequestForceRedownload(kv.Key);
                refreshedOne = true;
                LogDebug("Refreshing " + GameEngine.Instance.World.getVillageName(kv.Key) +
                    " so the API can see delivered goods (attempt " + kv.Value.Attempts + ").");
            }
            foreach (int vid in done) _refreshWanted.Remove(vid);
        }

        private void RequestRedownload(int villageId)
        {
            try
            {
                VillageSyncModule sync = Engine.GetModule<VillageSyncModule>();
                if (sync != null) sync.RequestForceRedownload(villageId);
            }
            catch { }
        }

        // =====================================================================
        // HTTP
        // =====================================================================

        private void PostLeaveAsync()
        {
            BanquetSenderSettings settings = Settings;
            if (settings == null || string.IsNullOrEmpty(settings.ApiUrl) || string.IsNullOrEmpty(settings.GroupKey))
                return;
            string url = settings.ApiUrl.Trim();
            string key = settings.GroupKey.Trim();
            int userId;
            try { userId = RemoteServices.Instance.UserID; }
            catch { return; }

            Thread t = new Thread(delegate ()
            {
                try
                {
                    Dictionary<string, object> payload = new Dictionary<string, object>();
                    payload["user_id"] = userId;
                    PostJson(url, key, "leave", payload);
                }
                catch { }
            });
            t.IsBackground = true;
            t.Name = "BanquetSender Leave";
            t.Start();
        }

        private static Dictionary<string, object> PostJson(string url, string key, string action,
            Dictionary<string, object> payload)
        {
            payload["action"] = action;
            payload["key"] = key;
            byte[] bytes = Encoding.UTF8.GetBytes(AutoBombMultiModule.SimpleJson.Serialize(payload));

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = bytes.Length;
            request.Timeout = 20000;

            using (Stream s = request.GetRequestStream())
                s.Write(bytes, 0, bytes.Length);

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                // The API answers errors with a JSON body and a 4xx/5xx status.
                response = ex.Response as HttpWebResponse;
                if (response == null) throw;
            }
            using (response)
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string json = reader.ReadToEnd();
                int pos = 0;
                Dictionary<string, object> parsed =
                    AutoBombMultiModule.SimpleJson.DeserializeObject(json, ref pos) as Dictionary<string, object>;
                if (parsed == null)
                    throw new Exception("HTTP " + (int)response.StatusCode + ": not a JSON response.");
                return parsed;
            }
        }

        // =====================================================================
        // Helpers
        // =====================================================================

        /// <summary>A good's level in a village right now (production since the last download included, capped).</summary>
        private static double LevelOf(VillageMap map, int good)
        {
            try { return Math.Max(0.0, map.getResourceLevel(SenderGoods.ResourceIds[good])); }
            catch { return 0.0; }
        }

        private static long ToUnix(DateTime gameTime)
        {
            return (long)(gameTime - UnixEpoch).TotalSeconds;
        }

        private static string FormatDuration(long seconds)
        {
            if (seconds < 0) seconds = 0;
            TimeSpan ts = TimeSpan.FromSeconds(seconds);
            return ts.TotalHours >= 1
                ? string.Format("{0}h {1:00}m", (int)ts.TotalHours, ts.Minutes)
                : string.Format("{0}m {1:00}s", ts.Minutes, ts.Seconds);
        }

        private static string Describe(Order order, int amount)
        {
            string from = GameEngine.Instance.World.getVillageName(order.FromVillageId);
            string to = !string.IsNullOrEmpty(order.ToVillageName) ? order.ToVillageName : "village " + order.ToVillageId;
            if (!string.IsNullOrEmpty(order.ToPlayerName)) to += " (" + order.ToPlayerName + ")";
            return amount + " " + SenderGoods.Name(order.Good) + " " + from + " -> " + to;
        }

        private void SetCurrent(string text)
        {
            SetCurrent(text, "");
        }

        private void SetCurrent(string text, string category)
        {
            lock (_lock)
            {
                _status.Current = text;
                _status.CurrentCategory = category ?? "";
            }
        }

        private void SetError(string error)
        {
            lock (_lock) _status.LastError = error;
        }

        private void AddActivity(string category, string line)
        {
            ActivityLine a = new ActivityLine();
            a.Category = category ?? "";
            a.Text = DateTime.Now.ToString("HH:mm:ss") + "  " + line;
            lock (_lock)
            {
                _status.Activity.Insert(0, a);
                if (_status.Activity.Count > MaxActivityLines)
                    _status.Activity.RemoveRange(MaxActivityLines, _status.Activity.Count - MaxActivityLines);
            }
        }

        private static string GetStr(Dictionary<string, object> d, string key, string def)
        {
            object v;
            return d.TryGetValue(key, out v) && v != null ? v.ToString() : def;
        }

        private static long GetLong(Dictionary<string, object> d, string key)
        {
            object v;
            if (!d.TryGetValue(key, out v) || v == null) return 0;
            if (v is long) return (long)v;
            if (v is double) return (long)(double)v;
            long r;
            return long.TryParse(v.ToString(), out r) ? r : 0;
        }

        private static int GetInt(Dictionary<string, object> d, string key)
        {
            return (int)GetLong(d, key);
        }

        private static bool GetBool(Dictionary<string, object> d, string key)
        {
            object v;
            return d.TryGetValue(key, out v) && v is bool && (bool)v;
        }
    }
}
