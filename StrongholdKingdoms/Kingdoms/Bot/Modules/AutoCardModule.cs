using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonTypes;
using Stronghold.AuthClient;

namespace Kingdoms.Bot.Modules
{
    public class AutoCardModule : BotModuleBase
    {
        public override string ModuleName { get { return "Auto Card"; } }
        public override TimeSpan Interval { get { return TimeSpan.FromSeconds(30); } }

        protected override void OnInitialize() { }

        protected override void OnTick()
        {
            if (Engine == null || Engine.Settings == null || !Engine.Settings.Auto.Enabled)
                return;

            DateTime serverTime;
            try { serverTime = VillageMap.getCurrentServerTime(); }
            catch { return; }

            foreach (ProductionCardSettings p in Engine.Settings.Auto.ProductionCards)
            {
                if (!p.Enabled) continue;

                // Reset progress if target count changed externally
                if (p.PreviousTargetCount != -1 && p.PreviousTargetCount != p.TargetCount)
                {
                    p.PlayedCount = 0;
                    p.LastPlayedInstanceId = 0;
                    p.ScheduledStartTime = DateTime.MinValue;
                }
                p.PreviousTargetCount = p.TargetCount;

                if (p.PlayedCount >= p.TargetCount) continue;

                // Set scheduled start time the first time we process this entry
                if (p.ScheduledStartTime == DateTime.MinValue)
                    p.ScheduledStartTime = serverTime.AddMinutes(p.StartDelayMinutes);

                if (serverTime < p.ScheduledStartTime) continue;

                CardData cardData = GetCardData();
                if (cardData == null) continue;

                // Check if the card we last played is still active
                if (p.LastPlayedInstanceId != 0)
                {
                    if (CardInstanceIsActive(cardData, p.LastPlayedInstanceId))
                        continue; // card still running — nothing to do

                    // Card expired — count it
                    p.PlayedCount++;
                    p.LastPlayedInstanceId = 0;
                    LogInfo(p.GoodKey + " card expired. Played " + p.PlayedCount + "/" + p.TargetCount + ".");

                    if (p.PlayedCount >= p.TargetCount)
                    {
                        LogInfo(p.GoodKey + " reached target of " + p.TargetCount + " cards played.");
                        continue;
                    }
                }

                // Find and play the next card instance
                int instanceId = FindCardInstance(p.CardFilterId, p.TierIndex, cardData);
                if (instanceId == 0)
                {
                    LogDebug(p.GoodKey + " tier " + p.TierIndex + ": no matching card in inventory.");
                    continue;
                }

                PlayCard(instanceId);
                p.LastPlayedInstanceId = instanceId;
                LogInfo("Playing " + p.GoodKey + " card (instance " + instanceId + "), " +
                    (p.PlayedCount + 1) + "/" + p.TargetCount + ".");
            }
        }

        private CardData GetCardData()
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.cardsManager == null)
                    return null;
                return GameEngine.Instance.cardsManager.UserCardData;
            }
            catch { return null; }
        }

        private static bool CardInstanceIsActive(CardData cardData, int instanceId)
        {
            if (cardData == null || cardData.cards == null) return false;
            for (int i = 0; i < cardData.cards.Length; i++)
                if (cardData.cards[i] == instanceId) return true;
            return false;
        }

        // Finds a card instance in ProfileCards matching the given filter ID and tier index.
        // Cards within a filter group are sorted by definition ID ascending; tierIndex selects which.
        private int FindCardInstance(int filterId, int tierIndex, CardData activeCardData)
        {
            try
            {
                var mgr = GameEngine.Instance != null ? GameEngine.Instance.cardsManager : null;
                if (mgr == null) return 0;

                // Build set of currently active instance IDs to exclude
                var activeIds = new HashSet<int>();
                if (activeCardData != null && activeCardData.cards != null)
                    foreach (int c in activeCardData.cards)
                        if (c != 0) activeIds.Add(c);

                // Collect candidates from ProfileCards matching filterId, grouped by definition ID
                var byDefId = new SortedDictionary<int, List<int>>(); // defId → list of instanceIds
                foreach (var kvp in mgr.ProfileCards)
                {
                    if (kvp.Value == null || kvp.Value.cardFilter != filterId) continue;
                    if (activeIds.Contains(kvp.Key)) continue;
                    List<int> list;
                    if (!byDefId.TryGetValue(kvp.Value.id, out list))
                    {
                        list = new List<int>();
                        byDefId[kvp.Value.id] = list;
                    }
                    list.Add(kvp.Key);
                }

                if (byDefId.Count == 0)
                {
                    // No match — log what filter IDs ARE present so the catalog can be corrected
                    var presentFilters = new SortedDictionary<int, int>(); // filterId → count
                    foreach (var kvp in mgr.ProfileCards)
                    {
                        if (kvp.Value == null || activeIds.Contains(kvp.Key)) continue;
                        int f = kvp.Value.cardFilter;
                        if (!presentFilters.ContainsKey(f)) presentFilters[f] = 0;
                        presentFilters[f]++;
                    }
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (var kv in presentFilters)
                        sb.Append("filter=" + kv.Key + "(x" + kv.Value + ") ");
                    LogWarning("No card for filterId=" + filterId + " tier=" + tierIndex
                        + ". Filters in inventory: " + (sb.Length > 0 ? sb.ToString() : "(none)"));
                    return 0;
                }

                // Sorted ascending by defId; tierIndex picks the tier
                int idx = 0;
                foreach (var entry in byDefId)
                {
                    if (idx == tierIndex && entry.Value.Count > 0)
                        return entry.Value[0];
                    idx++;
                }
            }
            catch (Exception ex) { LogWarning("FindCardInstance error: " + ex.Message); }
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
                    delegate(ICardsProvider p, ICardsResponse r)
                    {
                        if (r.SuccessCode.HasValue && r.SuccessCode.Value == 1)
                            LogInfo("Card instance " + instanceId + " played successfully.");
                        else
                            LogWarning("Card instance " + instanceId + " play failed: " + r.Message);
                    },
                    (Control)InterfaceMgr.Instance.getDXBasePanel());
            }
            catch (Exception ex)
            {
                LogError("PlayCard failed for instance " + instanceId + ": " + ex.Message);
            }
        }
    }
}
