using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot
{
    /// <summary>
    /// Owns the single global UpdateVillageResourcesInfo callback slot and fans replies out
    /// by villageID.
    ///
    /// RemoteServices holds ONE UpdateVillageResourcesInfo_UserCallBack, not a per-request
    /// callback, and VillageMap.checkWeaponProduction installs its own handler every time a
    /// weapon batch finishes. Without a router the Village Info window and the game would
    /// steal the slot from each other and a reply could be delivered to whoever installed it
    /// last rather than to whoever asked for that village.
    ///
    /// Every reply still runs the game's own bookkeeping first - importing into the
    /// VillageMap, advancing the server clock and updating gold / honour - exactly as
    /// VillageMap.updateVillageResourcesInfoCallback did.
    ///
    /// Requests may come from any thread; replies arrive on the UI thread, because
    /// RemoteServices dispatches callbacks from processData() in the main game loop.
    /// </summary>
    internal static class VillageResourceRouter
    {
        private class Waiter
        {
            public int VillageID;
            public Action<UpdateVillageResourcesInfo_ReturnType> OnResult;
            public DateTime SentAt;
        }

        // The window only ever asks about one village at a time; the headroom is for the
        // game's own weapon-production refreshes landing at the same moment.
        private const int MaxInFlight = 2;
        private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(20);

        private static readonly object _lock = new object();
        private static readonly Dictionary<int, Waiter> _inFlight = new Dictionary<int, Waiter>();
        private static readonly List<Waiter> _queued = new List<Waiter>();

        /// <summary>
        /// Asks the server for one village's resources and stats. onResult runs on the UI
        /// thread and may be handed a null/failed result - callers must cope with not
        /// getting the data. Pass null to request purely for the game's own bookkeeping.
        /// Requesting a village that is already outstanding takes over its callback rather
        /// than sending a second RPC.
        /// </summary>
        public static void Request(int villageID, Action<UpdateVillageResourcesInfo_ReturnType> onResult)
        {
            if (villageID < 0) return;

            lock (_lock)
            {
                Waiter existing;
                if (_inFlight.TryGetValue(villageID, out existing))
                {
                    existing.OnResult = onResult;
                    return;
                }

                for (int i = 0; i < _queued.Count; i++)
                {
                    if (_queued[i].VillageID == villageID)
                    {
                        _queued[i].OnResult = onResult;
                        return;
                    }
                }

                Waiter waiter = new Waiter();
                waiter.VillageID = villageID;
                waiter.OnResult = onResult;
                _queued.Add(waiter);
            }

            PumpQueue();
        }

        /// <summary>
        /// Drops interest in a village's reply - used when the window closes or retargets
        /// with a request still outstanding. Any reply that does arrive is still applied to
        /// the game.
        /// </summary>
        public static void Cancel(int villageID)
        {
            lock (_lock)
            {
                Waiter waiter;
                if (_inFlight.TryGetValue(villageID, out waiter))
                    waiter.OnResult = null;

                for (int i = _queued.Count - 1; i >= 0; i--)
                {
                    if (_queued[i].VillageID == villageID)
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
                    _inFlight[waiter.VillageID] = waiter;
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
                RemoteServices.Instance.UpdateVillageResourcesInfo(waiter.VillageID);
            }
            catch (Exception)
            {
                // Couldn't get the request out - free the slot and tell the caller, so it
                // isn't left waiting on a reply that will never come.
                Action<UpdateVillageResourcesInfo_ReturnType> onResult;
                lock (_lock)
                {
                    onResult = waiter.OnResult;
                    _inFlight.Remove(waiter.VillageID);
                }
                Invoke(onResult, null);
            }
        }

        // Reasserts ownership of the shared slot. Nothing else should install its own
        // handler now that VillageMap routes through here, but the attack router learned
        // the hard way not to assume ours stays put.
        private static void Install()
        {
            RemoteServices.Instance.set_UpdateVillageResourcesInfo_UserCallBack(
                new RemoteServices.UpdateVillageResourcesInfo_UserCallBack(Callback));
        }

        private static void Callback(UpdateVillageResourcesInfo_ReturnType data)
        {
            // The game's own bookkeeping first - this is the body of the original
            // VillageMap.updateVillageResourcesInfoCallback.
            try
            {
                Install();
                if (data != null && data.Success)
                {
                    VillageMap village = GameEngine.Instance.getVillage(data.villageID);
                    if (village != null)
                        village.importResourcesAndStats(data.villageResourcesAndStats, data.currentTime);
                    VillageMap.setServerTime(data.currentTime);
                    GameEngine.Instance.World.setGoldData(data.currentGoldLevel, data.currentGoldRate);
                    GameEngine.Instance.World.setHonourData(data.currentHonourLevel, data.currentHonourRate);
                }
            }
            catch (Exception)
            {
            }

            Action<UpdateVillageResourcesInfo_ReturnType> onResult = null;

            lock (_lock)
            {
                Waiter waiter;
                if (data != null && data.Success && _inFlight.TryGetValue(data.villageID, out waiter))
                {
                    onResult = waiter.OnResult;
                    _inFlight.Remove(data.villageID);
                }
                else
                {
                    // A failure reply can't be trusted to carry the village id it was asked
                    // about, so retire the oldest in-flight request rather than let it hold
                    // a slot for the full timeout.
                    Waiter oldest = null;
                    foreach (Waiter w in _inFlight.Values)
                    {
                        if (oldest == null || w.SentAt < oldest.SentAt)
                            oldest = w;
                    }
                    if (oldest != null)
                    {
                        onResult = oldest.OnResult;
                        _inFlight.Remove(oldest.VillageID);
                    }
                }
            }

            Invoke(onResult, data);
            PumpQueue();
        }

        // Caller must hold _lock. Pulls timed-out requests out of _inFlight and returns
        // them so the caller can notify them once the lock is released.
        private static List<Waiter> TakeStale()
        {
            if (_inFlight.Count == 0) return null;

            DateTime cutoff = DateTime.Now - RequestTimeout;
            List<Waiter> stale = null;

            foreach (KeyValuePair<int, Waiter> kvp in _inFlight)
            {
                if (kvp.Value.SentAt < cutoff)
                {
                    if (stale == null) stale = new List<Waiter>();
                    stale.Add(kvp.Value);
                }
            }

            if (stale == null) return null;
            for (int i = 0; i < stale.Count; i++)
                _inFlight.Remove(stale[i].VillageID);
            return stale;
        }

        private static void Invoke(Action<UpdateVillageResourcesInfo_ReturnType> onResult,
            UpdateVillageResourcesInfo_ReturnType data)
        {
            if (onResult == null) return;
            try { onResult(data); }
            catch (Exception) { }
        }
    }
}
