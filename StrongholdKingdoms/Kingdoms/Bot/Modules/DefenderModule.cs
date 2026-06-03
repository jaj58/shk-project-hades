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
            get { return TimeSpan.FromMilliseconds(200); }
        }

        private bool _spamActive = false;
        private DateTime _spamEndTime = DateTime.MinValue;
        private int _targetVillageId = 0;

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
        }

        protected override void OnTick()
        {
            DefenderSettings s = Settings;
            if (s == null || !s.Enabled) return;
            if (!_spamActive) return;

            if (DateTime.Now >= _spamEndTime)
            {
                _spamActive = false;
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
            LogInfo("Defender spam started for " + durationSeconds + "s targeting village " + targetVillageId + ".");
        }

        public void StopSpam()
        {
            _spamActive = false;
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
        // Card playing — fire-and-forget, no duplicate guard intentional
        // =================================================================

        private void TryPlayCard(int defId)
        {
            try
            {
                CardData cardData = GetCardData();
                if (cardData == null) return;

                int instanceId = FindCardInstanceByDefId(defId, cardData);
                if (instanceId == 0) return;

                PlayCard(instanceId);
            }
            catch { }
        }

        private static CardData GetCardData()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.cardsManager == null)
                return null;
            return GameEngine.Instance.cardsManager.UserCardData;
        }

        private static int FindCardInstanceByDefId(int targetDefId, CardData activeCardData)
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
                    if (activeIds.Contains(kvp.Key)) continue;
                    return kvp.Key;
                }
            }
            catch { }
            return 0;
        }

        private void PlayCard(int instanceId)
        {
            try
            {
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(
                    URLs.ProfileProtocol,
                    URLs.ProfileServerAddressCards,
                    URLs.ProfileServerPort,
                    URLs.ProfileCardPath);
                XmlRpcCardsRequest req = new XmlRpcCardsRequest(
                    RemoteServices.Instance.UserGuid.ToString().Replace("-", ""),
                    RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""),
                    instanceId.ToString(),
                    "-1",
                    RemoteServices.Instance.ProfileWorldID.ToString());
                provider.PlayUserCard(
                    req,
                    delegate(ICardsProvider p, ICardsResponse r) { },
                    (Control)InterfaceMgr.Instance.getDXBasePanel());
            }
            catch { }
        }

        protected override void OnShutdown()
        {
            _spamActive = false;
        }
    }
}
