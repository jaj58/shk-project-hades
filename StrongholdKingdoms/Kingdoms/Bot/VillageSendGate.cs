using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot
{
    /// <summary>
    /// Single-flight gate for village-to-village merchant sends (VillageMap.sendResources),
    /// shared by every module that sends: Trade (routes / player routes) and Banquet Sender.
    ///
    /// Why single-flight across ALL villages, not per village: sendResources registers
    /// its callback in RemoteServices' one global SendMarketResources slot, and the
    /// callback clears inMarketSend on whichever VillageMap registered last. Two sends
    /// from different villages in flight at once means the first reply unlocks the
    /// wrong village and the other stays locked until its 45s stale expiry.
    ///
    /// Why owner routing: VillageMap.BotSendMarketResourcesResult is one static hook.
    /// The gate owns it and hands each result only to the module that claimed the send,
    /// so neither module mistakes the other's confirmation for its own.
    ///
    /// Usage: TryClaim before sendResources; Release if sendResources returned false.
    /// The claim is released automatically when the server result arrives, or after
    /// ClaimTimeoutSeconds if it never does.
    /// </summary>
    public static class VillageSendGate
    {
        // Matches VillageMap's own stale-lock expiry for inMarketSend.
        public const double ClaimTimeoutSeconds = 45.0;

        private static readonly object _lock = new object();
        private static readonly Dictionary<string, Action<SendMarketResources_ReturnType>> _handlers =
            new Dictionary<string, Action<SendMarketResources_ReturnType>>();
        private static string _owner;
        private static int _villageId = -1;
        private static DateTime _claimedAt = DateTime.MinValue;

        /// <summary>Subscribe a module to results of the sends it claims. Installs the VillageMap hook.</summary>
        public static void Register(string owner, Action<SendMarketResources_ReturnType> handler)
        {
            lock (_lock)
            {
                _handlers[owner] = handler;
                VillageMap.BotSendMarketResourcesResult = OnSendResult;
            }
        }

        public static void Unregister(string owner)
        {
            lock (_lock)
            {
                _handlers.Remove(owner);
                if (_owner == owner) ClearClaim();
            }
        }

        /// <summary>
        /// Reserve the send slot for one village. False while another send (by any
        /// module, from any village) is still waiting for its server result.
        /// </summary>
        public static bool TryClaim(string owner, int villageId)
        {
            lock (_lock)
            {
                if (_owner != null && (DateTime.Now - _claimedAt).TotalSeconds < ClaimTimeoutSeconds)
                    return false;
                _owner = owner;
                _villageId = villageId;
                _claimedAt = DateTime.Now;
                return true;
            }
        }

        /// <summary>Give the slot back without a result (sendResources returned false).</summary>
        public static void Release(string owner, int villageId)
        {
            lock (_lock)
            {
                if (_owner == owner && _villageId == villageId) ClearClaim();
            }
        }

        public static bool IsBusy
        {
            get
            {
                lock (_lock)
                {
                    return _owner != null && (DateTime.Now - _claimedAt).TotalSeconds < ClaimTimeoutSeconds;
                }
            }
        }

        // RPC callback thread. Handlers must only queue work for their tick thread.
        private static void OnSendResult(SendMarketResources_ReturnType result)
        {
            if (result == null) return;
            Action<SendMarketResources_ReturnType> handler = null;
            lock (_lock)
            {
                if (_owner != null && _villageId == result.villageID)
                {
                    _handlers.TryGetValue(_owner, out handler);
                    ClearClaim();
                }
            }
            // Unclaimed results (manual sends from the game UI, or a reply after the
            // claim timed out) belong to nobody and are ignored.
            if (handler != null)
            {
                try { handler(result); }
                catch { }
            }
        }

        private static void ClearClaim()
        {
            _owner = null;
            _villageId = -1;
            _claimedAt = DateTime.MinValue;
        }
    }
}
