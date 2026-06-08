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

        // Tracks instance IDs we have already fired this spam session so we never
        // re-submit a consumed card before ProfileCards refreshes from the server.
        private readonly HashSet<int> _playedInstanceIds = new HashSet<int>();

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
            _playedInstanceIds.Clear();
        }

        protected override void OnTick()
        {
            DefenderSettings s = Settings;
            if (s == null || !s.Enabled) return;
            if (!_spamActive) return;

            if (DateTime.Now >= _spamEndTime)
            {
                _spamActive = false;
                _playedInstanceIds.Clear();
                LogInfo("Defender spam finished.");
                return;
            }

            // Cards are global — no village switch required
            if (s.KnightsCardDefId > 0) TryPlayCard(s.KnightsCardDefId);
            if (s.LastStandCardDefId > 0) TryPlayCard(s.LastStandCardDefId);
            if (s.SpamDesperateDefence) TryPlayCard(263);

            // Castle actions require the correct village to be loaded
            if (_targetVillageId <= 0) return;

            if (GameEngine.Instance == null || GameEngine.Instance.Castle == null ||
                GameEngine.Instance.Castle.VillageID != _targetVillageId)
            {
                // Switch to the target village and wait for the castle to load
                try
                {
                    InterfaceMgr.Instance.setVillageNameBar(_targetVillageId);
                    GameEngine.Instance.downloadCurrentVillage();
                }
                catch { }
                return;
            }

            // Castle is loaded for the correct village — spam all configured actions
            try
            {
                if (s.AutoRepair)
                    GameEngine.Instance.Castle.autoRepairCastle();

                if (s.RestoreTroops)
                {
                    GameEngine.Instance.Castle.restoreTroops();
                    GameEngine.Instance.Castle.commitCastle();
                }

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
            _playedInstanceIds.Clear();
            LogInfo("Defender spam started for " + durationSeconds + "s targeting village " + targetVillageId + ".");
        }

        public void StopSpam()
        {
            _spamActive = false;
            _playedInstanceIds.Clear();
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
        // Card playing — fire-and-forget, no duplicate guard intentional.
        // Defense cards (SA, LS, DD) require the target village ID, not "-1".
        // =================================================================

        private void TryPlayCard(int defId)
        {
            try
            {
                CardData cardData = GetCardData();
                if (cardData == null)
                {
                    LogDebug("TryPlayCard defId=" + defId + ": UserCardData is null.");
                    return;
                }

                int inventoryCount = 0;
                var mgr = GameEngine.Instance != null ? GameEngine.Instance.cardsManager : null;
                if (mgr != null)
                    foreach (var kvp in mgr.ProfileCards)
                        if (kvp.Value != null && kvp.Value.id == defId) inventoryCount++;

                if (inventoryCount == 0)
                {
                    LogDebug("TryPlayCard defId=" + defId + ": none in inventory.");
                    return;
                }

                int instanceId = FindCardInstanceByDefId(defId, cardData, _playedInstanceIds);
                if (instanceId == 0)
                {
                    LogDebug("TryPlayCard defId=" + defId + ": all " + inventoryCount + " instance(s) already played or active.");
                    return;
                }

                // Mark as played immediately so subsequent ticks don't re-submit this instance
                // before ProfileCards refreshes from the server.
                _playedInstanceIds.Add(instanceId);
                LogInfo("Playing card defId=" + defId + " instanceId=" + instanceId + " on village " + _targetVillageId + ".");
                PlayCard(instanceId, _targetVillageId);
            }
            catch (Exception ex)
            {
                LogWarning("TryPlayCard defId=" + defId + " error: " + ex.Message);
            }
        }

        private static CardData GetCardData()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.cardsManager == null)
                return null;
            return GameEngine.Instance.cardsManager.UserCardData;
        }

        private static int FindCardInstanceByDefId(int targetDefId, CardData activeCardData, HashSet<int> alreadyPlayed)
        {
            try
            {
                var mgr = GameEngine.Instance != null ? GameEngine.Instance.cardsManager : null;
                if (mgr == null) return 0;

                var activeIds = new HashSet<int>();
                if (activeCardData != null && activeCardData.cards != null)
                    foreach (int c in activeCardData.cards)
                        if (c != 0) activeIds.Add(c);

                foreach (var kvp in mgr.ProfileCards)
                {
                    if (kvp.Value == null || kvp.Value.id != targetDefId) continue;
                    if (activeIds.Contains(kvp.Key)) continue;       // already active on server
                    if (alreadyPlayed != null && alreadyPlayed.Contains(kvp.Key)) continue; // fired this session
                    return kvp.Key;
                }
            }
            catch { }
            return 0;
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
                        if (r != null)
                        {
                            bool ok = r.SuccessCode.HasValue && r.SuccessCode.Value == 1;
                            if (ok)
                                LogInfo("Card instance " + instanceId + " played OK.");
                            else
                                LogWarning("Card instance " + instanceId + " play failed: " + r.Message);
                        }
                    },
                    (Control)InterfaceMgr.Instance.getDXBasePanel());
            }
            catch (Exception ex)
            {
                LogWarning("PlayCard instance " + instanceId + " error: " + ex.Message);
            }
        }

        protected override void OnShutdown()
        {
            _spamActive = false;
            _playedInstanceIds.Clear();
        }
    }
}
