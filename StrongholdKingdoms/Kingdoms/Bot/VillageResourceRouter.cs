using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot
{
    /// <summary>
    /// Owns the single global VillageBuildingChangeRates callback slot and fans replies out
    /// by villageID.
    ///
    /// RemoteServices holds ONE VillageBuildingChangeRates_UserCallBack, and seven places
    /// want it: VillageMap's stats panel (four sites - one applying the tax / rations
    /// sliders, three refreshing after a migration, consumption change or popularity event),
    /// VillageSyncModule's background refresh, AutoBombModule's force refresh, and the
    /// Village Info window. Before this router they all overwrote each other's handler, so a
    /// reply could easily be delivered to whoever installed the slot last rather than to
    /// whoever asked for that village.
    ///
    /// Every reply is still handed to the target village's own
    /// villageBuildingChangeRatesCallback first, so the game's resource, building and
    /// gold / honour bookkeeping keeps working exactly as before. Resolving the village from
    /// the reply rather than from whoever installed the slot also fixes a latent bug in that
    /// method: it initialises storage buildings against `this`, which was not necessarily
    /// the village the reply was about.
    ///
    /// Requests may come from any thread (the bot modules run on their own); replies arrive
    /// on the UI thread, because RemoteServices dispatches callbacks from processData() in
    /// the main game loop.
    /// </summary>
    internal static class VillageResourceRouter
    {
        // The rate arguments double as "leave this alone" when negative - every caller that
        // only wants to read the village's current state passes -1 for all four.
        private const int NoChange = -1;

        private class Waiter
        {
            public int VillageID;
            public int TaxLevel;
            public int RationsLevel;
            public int AleRationsLevel;
            public int CapitalTaxRate;
            public bool IsWrite;
            public Action<VillageBuildingChangeRates_ReturnType> OnResult;
            public DateTime SentAt;
        }

        // At most this many requests are on the wire at once. The game refreshes villages
        // through this slot on its own schedule, so leave room for those alongside a window
        // asking for one.
        private const int MaxInFlight = 4;
        private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(20);

        private static readonly object _lock = new object();
        private static readonly List<Waiter> _inFlight = new List<Waiter>();
        private static readonly List<Waiter> _queued = new List<Waiter>();

        /// <summary>
        /// Asks the server for one village's current resources, stats and buildings without
        /// changing anything. onResult runs on the UI thread and may be handed a null/failed
        /// result - callers must cope with not getting the data, and may pass null to request
        /// purely for the game's own bookkeeping. Requesting a village that already has a
        /// read outstanding takes over its callback rather than sending a second RPC.
        /// </summary>
        public static void Request(int villageID, Action<VillageBuildingChangeRates_ReturnType> onResult)
        {
            Enqueue(villageID, NoChange, NoChange, NoChange, NoChange, false, onResult);
        }

        /// <summary>
        /// Applies new tax / rations / ale rations / capital tax rates to a village. Pass -1
        /// for any rate that should be left alone. Never deduplicated or dropped - unlike a
        /// read, losing one of these would silently discard the player's change.
        /// </summary>
        public static void RequestRates(int villageID, int taxLevel, int rationsLevel,
            int aleRationsLevel, int capitalTaxRate,
            Action<VillageBuildingChangeRates_ReturnType> onResult)
        {
            Enqueue(villageID, taxLevel, rationsLevel, aleRationsLevel, capitalTaxRate, true, onResult);
        }

        private static void Enqueue(int villageID, int taxLevel, int rationsLevel,
            int aleRationsLevel, int capitalTaxRate, bool isWrite,
            Action<VillageBuildingChangeRates_ReturnType> onResult)
        {
            if (villageID < 0) return;

            lock (_lock)
            {
                // Only reads collapse into each other; a write always goes out on its own.
                if (!isWrite)
                {
                    for (int i = 0; i < _inFlight.Count; i++)
                    {
                        if (_inFlight[i].VillageID == villageID && !_inFlight[i].IsWrite)
                        {
                            _inFlight[i].OnResult = onResult;
                            return;
                        }
                    }

                    for (int i = 0; i < _queued.Count; i++)
                    {
                        if (_queued[i].VillageID == villageID && !_queued[i].IsWrite)
                        {
                            _queued[i].OnResult = onResult;
                            return;
                        }
                    }
                }

                Waiter waiter = new Waiter();
                waiter.VillageID = villageID;
                waiter.TaxLevel = taxLevel;
                waiter.RationsLevel = rationsLevel;
                waiter.AleRationsLevel = aleRationsLevel;
                waiter.CapitalTaxRate = capitalTaxRate;
                waiter.IsWrite = isWrite;
                waiter.OnResult = onResult;
                _queued.Add(waiter);
            }

            PumpQueue();
        }

        /// <summary>
        /// Drops interest in a village's reply - used when a window closes or retargets with
        /// a request still outstanding. Any reply that does arrive is still applied to the
        /// game, and queued rate changes are left alone.
        /// </summary>
        public static void Cancel(int villageID)
        {
            lock (_lock)
            {
                for (int i = 0; i < _inFlight.Count; i++)
                {
                    if (_inFlight[i].VillageID == villageID)
                        _inFlight[i].OnResult = null;
                }

                for (int i = _queued.Count - 1; i >= 0; i--)
                {
                    if (_queued[i].VillageID == villageID && !_queued[i].IsWrite)
                        _queued.RemoveAt(i);
                }
            }
        }

        private static void PumpQueue()
        {
            List<Waiter> expired;
            List<Waiter> toSend = null;

            lock (_lock)
            {
                expired = TakeStale();

                while (_queued.Count > 0 && _inFlight.Count < MaxInFlight)
                {
                    Waiter waiter = _queued[0];
                    _queued.RemoveAt(0);
                    waiter.SentAt = DateTime.Now;
                    _inFlight.Add(waiter);
                    if (toSend == null) toSend = new List<Waiter>();
                    toSend.Add(waiter);
                }
            }

            if (expired != null)
            {
                for (int i = 0; i < expired.Count; i++)
                    Invoke(expired[i].OnResult, null);
            }

            if (toSend != null)
            {
                for (int i = 0; i < toSend.Count; i++)
                    Send(toSend[i]);
            }
        }

        private static void Send(Waiter waiter)
        {
            try
            {
                Install();
                RemoteServices.Instance.VillageBuildingChangeRates(waiter.VillageID,
                    waiter.TaxLevel, waiter.RationsLevel, waiter.AleRationsLevel, waiter.CapitalTaxRate);
            }
            catch (Exception)
            {
                // Couldn't get the request out - free the slot and tell the caller, so it
                // isn't left waiting on a reply that will never come.
                Action<VillageBuildingChangeRates_ReturnType> onResult;
                lock (_lock)
                {
                    onResult = waiter.OnResult;
                    _inFlight.Remove(waiter);
                }
                Invoke(onResult, null);
            }
        }

        // Reasserts ownership of the shared slot. Nothing else should install its own handler
        // now that every call site routes through here, but the attack router learned the
        // hard way not to assume ours stays put.
        private static void Install()
        {
            RemoteServices.Instance.set_VillageBuildingChangeRates_UserCallBack(
                new RemoteServices.VillageBuildingChangeRates_UserCallBack(Callback));
        }

        private static void Callback(VillageBuildingChangeRates_ReturnType data)
        {
            // The game's own bookkeeping first - it imports resources and stats, refreshes
            // the village's storage buildings and updates gold and honour off this reply.
            try
            {
                Install();
                if (data != null && data.Success)
                {
                    VillageMap village = GameEngine.Instance.getVillage(data.villageID);
                    if (village != null)
                    {
                        village.villageBuildingChangeRatesCallback(data);
                    }
                    else
                    {
                        // No VillageMap means this is a village the client never downloaded
                        // - only the Village Info window asks about those, every other
                        // caller here holds a loaded village. Take the clock, which is the
                        // server's and so village-independent, but deliberately NOT the
                        // gold and honour: it is unverified whose totals a reply about
                        // someone else's village carries, and guessing wrong would corrupt
                        // the player's own displayed gold.
                        VillageMap.setServerTime(data.currentTime);
                    }
                }
            }
            catch (Exception)
            {
            }

            Action<VillageBuildingChangeRates_ReturnType> onResult = null;

            lock (_lock)
            {
                Waiter match = null;

                if (data != null && data.Success)
                {
                    for (int i = 0; i < _inFlight.Count; i++)
                    {
                        if (_inFlight[i].VillageID == data.villageID)
                        {
                            match = _inFlight[i];
                            break;
                        }
                    }
                }

                // A failure reply can't be trusted to carry the village id it was asked
                // about, so retire the oldest in-flight request rather than let it hold a
                // slot for the full timeout.
                if (match == null)
                {
                    for (int i = 0; i < _inFlight.Count; i++)
                    {
                        if (match == null || _inFlight[i].SentAt < match.SentAt)
                            match = _inFlight[i];
                    }
                }

                if (match != null)
                {
                    onResult = match.OnResult;
                    _inFlight.Remove(match);
                }
            }

            Invoke(onResult, data);
            PumpQueue();
        }

        // Caller must hold _lock. Pulls timed-out requests out of _inFlight and returns them
        // so the caller can notify them once the lock is released.
        private static List<Waiter> TakeStale()
        {
            if (_inFlight.Count == 0) return null;

            DateTime cutoff = DateTime.Now - RequestTimeout;
            List<Waiter> stale = null;

            for (int i = _inFlight.Count - 1; i >= 0; i--)
            {
                if (_inFlight[i].SentAt < cutoff)
                {
                    if (stale == null) stale = new List<Waiter>();
                    stale.Add(_inFlight[i]);
                    _inFlight.RemoveAt(i);
                }
            }

            return stale;
        }

        private static void Invoke(Action<VillageBuildingChangeRates_ReturnType> onResult,
            VillageBuildingChangeRates_ReturnType data)
        {
            if (onResult == null) return;
            try { onResult(data); }
            catch (Exception) { }
        }
    }
}
