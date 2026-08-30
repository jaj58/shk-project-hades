using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot
{
    /// <summary>
    /// Owns the single global RetrieveAttackResult callback slot and fans replies out
    /// by armyID.
    ///
    /// RemoteServices holds ONE RetrieveAttackResult_UserCallBack, not a per-request
    /// callback. Before this router the radar module, the map's army-select panel and
    /// WorldMap itself all overwrote each other's handler, so a reply could easily be
    /// delivered to whoever installed the slot last rather than to whoever asked for
    /// that army. Everything goes through here now.
    ///
    /// Every reply is still forwarded to World.retrieveAttackResultCallback first, so
    /// the game's own army / village / card bookkeeping keeps working exactly as before.
    ///
    /// Requests may come from any thread (the bot modules run on their own); replies
    /// arrive on the UI thread, because RemoteServices dispatches callbacks from
    /// processData() in the main game loop.
    /// </summary>
    internal static class AttackResultRouter
    {
        private class Waiter
        {
            public long ArmyID;
            public Action<RetrieveAttackResult_ReturnType> OnResult;
            public DateTime SentAt;
        }

        // At most this many requests are on the wire at once; the rest wait in _queued.
        // A window listing 30 incoming armies must not fire 30 RPCs in one go. Keep this
        // generous enough that a queued radar lookup still gets answered inside the 5s
        // timeout in RadarModule.ProcessPendingLookups, or its notifications would start
        // going out with map-only troop counts.
        private const int MaxInFlight = 6;
        private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(20);

        private static readonly object _lock = new object();
        private static readonly Dictionary<long, Waiter> _inFlight = new Dictionary<long, Waiter>();
        private static readonly List<Waiter> _queued = new List<Waiter>();

        /// <summary>
        /// Asks the server for one army's details. onResult runs on the UI thread and
        /// may be handed a null/failed result - callers must cope with not getting the
        /// counts. Requesting an army that is already outstanding just takes over its
        /// callback rather than sending a second RPC.
        /// </summary>
        public static void Request(long armyID, Action<RetrieveAttackResult_ReturnType> onResult)
        {
            lock (_lock)
            {
                Waiter existing;
                if (_inFlight.TryGetValue(armyID, out existing))
                {
                    existing.OnResult = onResult;
                    return;
                }

                for (int i = 0; i < _queued.Count; i++)
                {
                    if (_queued[i].ArmyID == armyID)
                    {
                        _queued[i].OnResult = onResult;
                        return;
                    }
                }

                Waiter waiter = new Waiter();
                waiter.ArmyID = armyID;
                waiter.OnResult = onResult;
                _queued.Add(waiter);
            }

            PumpQueue();
        }

        /// <summary>
        /// Drops interest in an army's reply - used when a window closes with requests
        /// still outstanding. Any reply that does arrive is still forwarded to the game.
        /// </summary>
        public static void Cancel(long armyID)
        {
            lock (_lock)
            {
                Waiter waiter;
                if (_inFlight.TryGetValue(armyID, out waiter))
                    waiter.OnResult = null;

                for (int i = _queued.Count - 1; i >= 0; i--)
                {
                    if (_queued[i].ArmyID == armyID)
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
                    _inFlight[waiter.ArmyID] = waiter;
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
                RemoteServices.Instance.RetrieveAttackResult(
                    waiter.ArmyID, GameEngine.Instance.World.StoredVillageFactionPos);
            }
            catch (Exception)
            {
                // Couldn't get the request out - free the slot and tell the caller, so it
                // isn't left waiting on a reply that will never come.
                Action<RetrieveAttackResult_ReturnType> onResult;
                lock (_lock)
                {
                    onResult = waiter.OnResult;
                    _inFlight.Remove(waiter.ArmyID);
                }
                Invoke(onResult, null);
            }
        }

        // Reasserts ownership of the shared slot. WorldMap.retrieveArmies() installs its
        // own handler on a full army refresh, so we cannot assume ours stays put.
        private static void Install()
        {
            RemoteServices.Instance.set_RetrieveAttackResult_UserCallBack(
                new RemoteServices.RetrieveAttackResult_UserCallBack(Callback));
        }

        private static void Callback(RetrieveAttackResult_ReturnType data)
        {
            // The game's own bookkeeping first - it updates the army array, village
            // data, points and card data off the back of this reply.
            try
            {
                Install();
                GameEngine.Instance.World.retrieveAttackResultCallback(data);
            }
            catch (Exception)
            {
            }

            Action<RetrieveAttackResult_ReturnType> onResult = null;

            lock (_lock)
            {
                if (data != null && data.armyData != null)
                {
                    Waiter waiter;
                    if (_inFlight.TryGetValue(data.armyData.armyID, out waiter))
                    {
                        onResult = waiter.OnResult;
                        _inFlight.Remove(data.armyData.armyID);
                    }
                }
                else
                {
                    // A failure reply carries no armyData, so there is nothing to match
                    // on. Retire the oldest in-flight request rather than let it hold a
                    // slot for the full timeout.
                    Waiter oldest = null;
                    foreach (Waiter w in _inFlight.Values)
                    {
                        if (oldest == null || w.SentAt < oldest.SentAt)
                            oldest = w;
                    }
                    if (oldest != null)
                    {
                        onResult = oldest.OnResult;
                        _inFlight.Remove(oldest.ArmyID);
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

            foreach (KeyValuePair<long, Waiter> kvp in _inFlight)
            {
                if (kvp.Value.SentAt < cutoff)
                {
                    if (stale == null) stale = new List<Waiter>();
                    stale.Add(kvp.Value);
                }
            }

            if (stale == null) return null;
            for (int i = 0; i < stale.Count; i++)
                _inFlight.Remove(stale[i].ArmyID);
            return stale;
        }

        private static void Invoke(Action<RetrieveAttackResult_ReturnType> onResult,
            RetrieveAttackResult_ReturnType data)
        {
            if (onResult == null) return;
            try { onResult(data); }
            catch (Exception) { }
        }
    }
}
