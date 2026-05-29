using System;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class CardExpiryModule : BotModuleBase
    {
        // Trade card type IDs (from CardTypes.getCardType)
        private static readonly int[] TradeCardTypes = new int[]
        {
            1537, 1538, 1539, 1541, 1542, 1543
        };

        // Scout card type IDs (reserved for future use)
        private static readonly int[] ScoutCardTypes = new int[]
        {
            2305, 2306, 2307, 2308, 2309, 2310
        };

        private bool _tradeCardsInPlay;
        private bool _scoutCardsInPlay;
        private DateTime _nextCheckTime = DateTime.MinValue;

        public override string ModuleName
        {
            get { return "Card Expiry"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(10); }
        }

        protected override void OnInitialize()
        {
            // Seed from persisted state so expiry is detected even after a game restart
            _tradeCardsInPlay = Engine != null && Engine.Settings != null
                && Engine.Settings.Trade.TradeCardsWereActive;
            _scoutCardsInPlay = Engine != null && Engine.Settings != null
                && Engine.Settings.Scout.ScoutCardsWereActive;
            _nextCheckTime = DateTime.MinValue;
        }

        protected override void OnTick()
        {
            if (Engine == null || Engine.Settings == null)
                return;

            // Only run if at least one module cares about card expiry
            bool anyModuleCares = Engine.Settings.Trade.DisableOnTradeCardExpiry
                || Engine.Settings.Scout.DisableOnScoutCardExpiry;
            if (!anyModuleCares)
                return;

            // Skip check if we know cards won't expire soon
            DateTime serverTime;
            try
            {
                serverTime = VillageMap.getCurrentServerTime();
            }
            catch
            {
                return;
            }

            if (_nextCheckTime > serverTime.AddSeconds(10))
                return;

            CardData cardData = GetCardData();
            if (cardData == null)
                return;

            CheckTradeCards(cardData, serverTime);
            CheckScoutCards(cardData, serverTime);
        }

        private CardData GetCardData()
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.cardsManager == null)
                    return null;
                return GameEngine.Instance.cardsManager.UserCardData;
            }
            catch
            {
                return null;
            }
        }

        private void CheckTradeCards(CardData cardData, DateTime serverTime)
        {
            bool tradeActive = false;
            DateTime earliestExpiry = DateTime.MaxValue;

            if (cardData.cards != null)
            {
                for (int i = 0; i < cardData.cards.Length; i++)
                {
                    if (cardData.cards[i] == 0)
                        continue;

                    int cardType;
                    try
                    {
                        cardType = CardTypes.getCardType(cardData.cards[i]);
                    }
                    catch
                    {
                        continue;
                    }

                    if (IsTradeCard(cardType))
                    {
                        tradeActive = true;
                        if (cardData.cardsExpiry != null && i < cardData.cardsExpiry.Length
                            && cardData.cardsExpiry[i] < earliestExpiry)
                        {
                            earliestExpiry = cardData.cardsExpiry[i];
                        }
                    }
                }
            }

            bool tradeCardsExpired = _tradeCardsInPlay && !tradeActive;
            _tradeCardsInPlay = tradeActive;
            Engine.Settings.Trade.TradeCardsWereActive = tradeActive;

            // Schedule next check
            if (tradeActive)
            {
                _nextCheckTime = earliestExpiry;
                LogDebug("Trade cards active. Next expiry check at " + earliestExpiry.ToString("HH:mm:ss"));
            }
            else
            {
                // No cards in play � check again in 3 hours (nothing to monitor)
                _nextCheckTime = serverTime.AddHours(3);
            }

            // Handle expiry
            if (tradeCardsExpired)
                OnTradeCardsExpired();
        }

        private void OnTradeCardsExpired()
        {
            if (Engine == null || Engine.Settings == null)
                return;

            TradeSettings tradeSettings = Engine.Settings.Trade;
            if (!tradeSettings.DisableOnTradeCardExpiry)
                return;

            // Find and disable the trade module
            foreach (IBotModule m in Engine.Modules)
            {
                TradeModule tradeModule = m as TradeModule;
                if (tradeModule != null && tradeModule.Enabled)
                {
                    tradeModule.Enabled = false;
                    tradeSettings.Enabled = false;
                    LogWarning("Trade cards expired! Trade module has been disabled.");
                    return;
                }
            }
        }

        private void CheckScoutCards(CardData cardData, DateTime serverTime)
        {
            if (!Engine.Settings.Scout.DisableOnScoutCardExpiry) return;

            bool scoutActive = false;

            if (cardData.cards != null)
            {
                for (int i = 0; i < cardData.cards.Length; i++)
                {
                    if (cardData.cards[i] == 0) continue;
                    int cardType;
                    try { cardType = CardTypes.getCardType(cardData.cards[i]); }
                    catch { continue; }
                    if (IsScoutCard(cardType))
                        scoutActive = true;
                }
            }

            bool scoutCardsExpired = _scoutCardsInPlay && !scoutActive;
            _scoutCardsInPlay = scoutActive;
            Engine.Settings.Scout.ScoutCardsWereActive = scoutActive;

            if (scoutCardsExpired)
                OnScoutCardsExpired();
        }

        private void OnScoutCardsExpired()
        {
            if (Engine == null || Engine.Settings == null) return;

            ScoutSettings scoutSettings = Engine.Settings.Scout;
            if (!scoutSettings.DisableOnScoutCardExpiry) return;

            foreach (IBotModule m in Engine.Modules)
            {
                ScoutModule scoutModule = m as ScoutModule;
                if (scoutModule != null && scoutModule.Enabled)
                {
                    scoutModule.Enabled = false;
                    scoutSettings.Enabled = false;
                    LogWarning("Scout cards expired! Scout module has been disabled.");
                    return;
                }
            }
        }

        private static bool IsScoutCard(int cardType)
        {
            for (int i = 0; i < ScoutCardTypes.Length; i++)
            {
                if (ScoutCardTypes[i] == cardType) return true;
            }
            return false;
        }

        private static bool IsTradeCard(int cardType)
        {
            for (int i = 0; i < TradeCardTypes.Length; i++)
            {
                if (TradeCardTypes[i] == cardType) return true;
            }
            return false;
        }
    }
}
