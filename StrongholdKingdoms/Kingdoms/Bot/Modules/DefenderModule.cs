using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonTypes;
using Stronghold.AuthClient;

namespace Kingdoms.Bot.Modules
{
    public class DefenderModule : BotModuleBase
    {
        public override string ModuleName { get { return "Defender"; } }

        public override TimeSpan Interval
        {
            get { return TimeSpan.Zero; }
        }

        private bool _spamActive = false;
        private DateTime _spamEndTime = DateTime.MinValue;
        private int _targetVillageId = 0;

        // Only tracks instances the server has confirmed are genuinely consumed:
        //   • server response success (code 1)
        //   • "you do not have that card available" (instance removed from inventory)
        //
        // "More than one of this type may not be played" is NOT added here —
        // the instance is still valid; it must be retried once the currently-active
        // card of that type expires (i.e. after the next attack lands).
        private readonly HashSet<int> _consumedInstanceIds = new HashSet<int>();
        private readonly object _consumedLock = new object();

        private DefenderSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.Defender;
                return null;
            }
        }

        protected override void OnInitialize()
        {
            _spamActive = false;
            _spamEndTime = DateTime.MinValue;
            _targetVillageId = 0;
            lock (_consumedLock) { _consumedInstanceIds.Clear(); }
        }

        protected override void OnTick()
        {
            DefenderSettings s = Settings;
            if (s == null || !s.Enabled) return;
            if (!_spamActive) return;

            if (DateTime.Now >= _spamEndTime)
            {
                _spamActive = false;
                MyMessageBox.Suppress = false;
                lock (_consumedLock) { _consumedInstanceIds.Clear(); }
                LogInfo("Defender spam finished.");
                return;
            }

            // ── 1. Cards (fire-and-forget, no village switch required) ──────────
            //
            // For each configured card type we snapshot ALL instance IDs from the
            // local inventory and fire a PlayCard request for every non-consumed one.
            // The server accepts at most one per type; the rest return "more than
            // one of this type" and are silently ignored.  As soon as an attack lands
            // and the active card expires, the first non-consumed instance will
            // succeed on the very next tick.
            if (s.KnightsCardDefId > 0)   TryPlayCard(s.KnightsCardDefId);
            if (s.LastStandCardDefId > 0) TryPlayCard(s.LastStandCardDefId);
            if (s.SpamDesperateDefence)    TryPlayCard(263);

            // ── 2. Castle actions (require the correct village to be loaded) ─────
            if (_targetVillageId <= 0) return;

            if (GameEngine.Instance == null || GameEngine.Instance.Castle == null ||
                GameEngine.Instance.Castle.VillageID != _targetVillageId)
            {
                // Switch to the target village; the castle will be ready on the next tick.
                try
                {
                    InterfaceMgr.Instance.setVillageNameBar(_targetVillageId);
                    GameEngine.Instance.downloadCurrentVillage();
                }
                catch { }
                return;
            }

            // Castle is loaded for the correct village.
            // Priority order: troops → repair → infrastructure.
            try
            {
                if (s.RestoreTroops)
                {
                    GameEngine.Instance.Castle.restoreTroops();
                    GameEngine.Instance.Castle.commitCastle();
                }

                if (s.AutoRepair)
                    GameEngine.Instance.Castle.autoRepairCastle();

                if (s.RestoreInfrastructure)
                {
                    GameEngine.Instance.Castle.restoreInfrastructure();
                    GameEngine.Instance.Castle.commitCastle();
                }
            }
            catch { }
        }

        // =================================================================
        // Public API — called from the UI
        // =================================================================

        public void StartSpam(int durationSeconds, int targetVillageId)
        {
            _targetVillageId = targetVillageId;
            _spamActive = true;
            _spamEndTime = DateTime.Now.AddSeconds(durationSeconds);
            lock (_consumedLock) { _consumedInstanceIds.Clear(); }
            // Suppress "Castle Placement Error" popups that fire when the server
            // rejects rapid commitCastle() calls during an active battle.
            MyMessageBox.Suppress = true;
            LogInfo("Defender spam started for " + durationSeconds + "s targeting village " + targetVillageId + ".");
        }

        public void StopSpam()
        {
            _spamActive = false;
            MyMessageBox.Suppress = false;
            lock (_consumedLock) { _consumedInstanceIds.Clear(); }
            LogInfo("Defender spam stopped manually.");
        }

        public bool IsSpamActive
        {
            get { return _spamActive && DateTime.Now < _spamEndTime; }
        }

        public int SecondsRemaining
        {
            get { return (int)Math.Max(0, (_spamEndTime - DateTime.Now).TotalSeconds); }
        }

        // =================================================================
        // Card playing
        // =================================================================

        /// <summary>
        /// Fires a PlayCard request for every non-consumed instance of
        /// <paramref name="defId"/> found in the local ProfileCards inventory.
        /// All requests are fire-and-forget; the server accepts at most one per
        /// card type at a time.  Only the accepted instance is marked consumed.
        /// </summary>
        private void TryPlayCard(int defId)
        {
            try
            {
                var mgr = GameEngine.Instance != null ? GameEngine.Instance.cardsManager : null;
                if (mgr == null) return;

                // Snapshot matching instance IDs so the live dictionary is not
                // held across the async PlayCard calls below.
                var instances = new List<int>();
                foreach (var kvp in mgr.ProfileCards)
                    if (kvp.Value != null && kvp.Value.id == defId)
                        instances.Add(kvp.Key);

                if (instances.Count == 0) return;

                foreach (int instanceId in instances)
                {
                    bool consumed;
                    lock (_consumedLock) { consumed = _consumedInstanceIds.Contains(instanceId); }
                    if (consumed) continue;

                    PlayCard(instanceId, _targetVillageId);
                }
            }
            catch { }
        }

        private void PlayCard(int instanceId, int targetVillageId)
        {
            try
            {
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(
                    URLs.ProfileProtocol,
                    URLs.ProfileServerAddressCards,
                    URLs.ProfileServerPort,
                    URLs.ProfileCardPath);

                string villageTarget = targetVillageId > 0 ? targetVillageId.ToString() : "-1";

                XmlRpcCardsRequest req = new XmlRpcCardsRequest(
                    RemoteServices.Instance.UserGuid.ToString().Replace("-", ""),
                    RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""),
                    instanceId.ToString(),
                    villageTarget,
                    RemoteServices.Instance.ProfileWorldID.ToString());

                provider.PlayUserCard(
                    req,
                    delegate(ICardsProvider p, ICardsResponse r)
                    {
                        if (r == null) return;

                        bool ok = r.SuccessCode.HasValue && r.SuccessCode.Value == 1;
                        if (ok)
                        {
                            lock (_consumedLock) { _consumedInstanceIds.Add(instanceId); }
                            LogInfo("Card instance " + instanceId + " played OK.");
                            return;
                        }

                        // "You do not have that card available" — instance is genuinely
                        // gone from inventory; mark consumed so we stop trying it.
                        //
                        // "More than one of this type may not be played" — a card of this
                        // type is currently active on the server; do NOT mark consumed.
                        // The same instance will be retried every tick until the active
                        // card expires (after the next attack lands) and the play succeeds.
                        //
                        // All other failures are also silent — no log spam.
                        string msg = r.Message ?? string.Empty;
                        if (msg.IndexOf("do not have", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            lock (_consumedLock) { _consumedInstanceIds.Add(instanceId); }
                        }
                    },
                    (Control)InterfaceMgr.Instance.getDXBasePanel());
            }
            catch { }
        }

        protected override void OnShutdown()
        {
            _spamActive = false;
            MyMessageBox.Suppress = false;
            lock (_consumedLock) { _consumedInstanceIds.Clear(); }
        }
    }
}
