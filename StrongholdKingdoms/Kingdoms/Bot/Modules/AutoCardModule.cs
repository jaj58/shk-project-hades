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

        // Interval is user-configurable via Auto.CardCheckIntervalSeconds (min 5s).
        public override TimeSpan Interval
        {
            get
            {
                int s = 30;
                if (Engine != null && Engine.Settings != null && Engine.Settings.Auto != null)
                    s = Engine.Settings.Auto.CardCheckIntervalSeconds;
                if (s < 5) s = 5;
                return TimeSpan.FromSeconds(s);
            }
        }

        // Grace period after playing a card before we trust an "inactive" reading. UserCardData
        // can lag a few seconds behind the play; without this we'd miscount cards as expired.
        private const int PlayConfirmGraceMinutes = 2;

        protected override void OnInitialize() { }

        protected override void OnTick()
        {
            if (Engine == null || Engine.Settings == null || Engine.Settings.Auto == null)
                return;

            DateTime serverTime;
            try { serverTime = VillageMap.getCurrentServerTime(); }
            catch { return; }

            int enabledGoods = 0;
            foreach (ProductionCardSettings pc in Engine.Settings.Auto.ProductionCards)
                if (pc.Enabled) enabledGoods++;
            LogDebug("Tick — checking " + enabledGoods + " enabled good(s) of "
                + Engine.Settings.Auto.ProductionCards.Count + " configured.");

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

                // Resolve the exact card def ID for this good + tier from the catalog
                int targetCardId = ProductionCardCatalog.GetCardId(p.GoodKey, p.TierIndex);
                if (targetCardId == 0)
                {
                    LogWarning(p.GoodKey + " tier " + p.TierIndex + ": no catalog entry.");
                    continue;
                }

                // Detect whether a card of this TYPE is in play. UserCardData.cards[] holds encoded
                // in-play values (not ProfileCards instance keys), so we decode each with
                // CardTypes.getCardType and compare the type — same approach as CardExpiryModule.
                bool typeActive = CardTypeIsActive(cardData, targetCardId);

                // Track the card we played through its lifecycle (LastPlayedInstanceId != 0 = in flight)
                if (p.LastPlayedInstanceId != 0)
                {
                    if (typeActive)
                    {
                        p.ConfirmedActive = true;
                        continue; // still running — nothing to do
                    }

                    if (p.ConfirmedActive)
                    {
                        // We saw the card active and now it's gone → genuinely expired
                        p.PlayedCount++;
                        p.LastPlayedInstanceId = 0;
                        p.ConfirmedActive = false;
                        LogInfo(p.GoodKey + " card expired. Played " + p.PlayedCount + "/" + p.TargetCount + ".");

                        if (p.PlayedCount >= p.TargetCount)
                        {
                            LogInfo(p.GoodKey + " reached target of " + p.TargetCount + " cards played.");
                            continue;
                        }
                        // else fall through to play the next one
                    }
                    else
                    {
                        // Played but the card type hasn't shown active yet — usually UserCardData
                        // refresh lag. Wait out the grace period; only then assume the play didn't
                        // take effect and retry, rather than stalling forever.
                        if ((serverTime - p.PlayAttemptTime).TotalMinutes < PlayConfirmGraceMinutes)
                            continue;
                        LogWarning(p.GoodKey + " card play not confirmed active within "
                            + PlayConfirmGraceMinutes + " min — the play may have failed; retrying.");
                        p.LastPlayedInstanceId = 0;
                        // fall through to retry
                    }
                }

                // Don't play while a card of this type is already active — only one of a type can be
                // active at once (the server rejects duplicates with "you do not have that card
                // available"). Wait for it to clear before playing the next one.
                if (typeActive)
                    continue;

                int instanceId = FindCardInstanceByDefId(targetCardId, cardData);
                if (instanceId == 0)
                {
                    LogDebug(p.GoodKey + " (card type " + targetCardId + "): none available in inventory.");
                    continue;
                }

                PlayCard(instanceId);
                p.LastPlayedInstanceId = instanceId;
                p.PlayAttemptTime = serverTime;
                p.ConfirmedActive = false;
                LogInfo("Playing " + p.GoodKey + " card type " + targetCardId + " (instance " + instanceId + "), "
                    + (p.PlayedCount + 1) + "/" + p.TargetCount + ".");
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

        // Returns true if any in-play card has the given definition ID (card type).
        // UserCardData.cards[] stores encoded values, so decode each with getCardType — matching
        // the proven approach in CardExpiryModule / AutoModuleSchedulerModule.
        private static bool CardTypeIsActive(CardData cardData, int defId)
        {
            if (cardData == null || cardData.cards == null) return false;
            for (int i = 0; i < cardData.cards.Length; i++)
            {
                if (cardData.cards[i] == 0) continue;
                try { if (CardTypes.getCardType(cardData.cards[i]) == defId) return true; }
                catch { }
            }
            return false;
        }

        // Finds an inventory card instance whose definition ID matches targetDefId and which is
        // not already active. Returns the user instance ID, or 0 if none available.
        private int FindCardInstanceByDefId(int targetDefId, CardData activeCardData)
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

                foreach (var kvp in mgr.ProfileCards)
                {
                    if (kvp.Value == null || kvp.Value.id != targetDefId) continue;
                    if (activeIds.Contains(kvp.Key)) continue;
                    return kvp.Key;
                }
            }
            catch (Exception ex) { LogWarning("FindCardInstanceByDefId error: " + ex.Message); }
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
