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
            _tradeCardsInPlay = false;
            _nextCheckTime = DateTime.MinValue;
        }

        protected override void OnTick()
        {
            if (Engine == null || Engine.Settings == null)
                return;

            // Only run if at least one module cares about card expiry
            if (!Engine.Settings.Trade.DisableOnTradeCardExpiry)
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

            // Schedule next check
            if (tradeActive)
            {
                _nextCheckTime = earliestExpiry;
                LogDebug("Trade cards active. Next expiry check at " + earliestExpiry.ToString("HH:mm:ss"));
            }
            else
            {
                // No cards in play — check again in 3 hours (nothing to monitor)
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
