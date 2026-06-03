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

        // Scout card type IDs (confirmed via CustomTooltipPanel.cs tooltip strings)
        // 2305-2307: Scout Speed - Horse Breeding (basic/standard/advanced)
        // 2308-2310: Scout Carrying Capacity - Scavenging (basic/standard/advanced)
        // 2691-2693: Scout Speed - Stables / newer tier variants
        private static readonly int[] ScoutCardTypes = new int[]
        {
            2305, 2306, 2307,   // Scout Speed
            2308, 2309, 2310,   // Scout Carrying Capacity
            2691, 2692, 2693    // Scout Speed (newer variants)
        };

        private bool _tradeCardsInPlay;
        private bool _scoutCardsInPlay;

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
            _tradeCardsInPlay = Engine != null && Engine.Settings != null
                && Engine.Settings.Trade.TradeCardsWereActive;
            _scoutCardsInPlay = Engine != null && Engine.Settings != null
                && Engine.Settings.Scout.ScoutCardsWereActive;
        }

        protected override void OnTick()
        {
            if (Engine == null || Engine.Settings == null)
                return;

            bool anyModuleCares = Engine.Settings.Trade.DisableOnTradeCardExpiry
                || Engine.Settings.Scout.DisableOnScoutCardExpiry;
            if (!anyModuleCares)
                return;

            CardData cardData = GetCardData();
            if (cardData == null)
                return;

            CheckTradeCards(cardData);
            CheckScoutCards(cardData);
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

        private void CheckTradeCards(CardData cardData)
        {
            // Each checker is independent — early-return when its own setting is off
            if (!Engine.Settings.Trade.DisableOnTradeCardExpiry) return;

            bool tradeActive = false;

            if (cardData.cards != null)
            {
                for (int i = 0; i < cardData.cards.Length; i++)
                {
                    if (cardData.cards[i] == 0) continue;
                    int cardType;
                    try { cardType = CardTypes.getCardType(cardData.cards[i]); }
                    catch { continue; }
                    if (IsTradeCard(cardType))
                        tradeActive = true;
                }
            }

            bool tradeCardsExpired = _tradeCardsInPlay && !tradeActive;
            _tradeCardsInPlay = tradeActive;
            Engine.Settings.Trade.TradeCardsWereActive = tradeActive;

            if (tradeCardsExpired)
                OnTradeCardsExpired();
        }

        private void CheckScoutCards(CardData cardData)
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

        private void OnTradeCardsExpired()
        {
            if (Engine == null || Engine.Settings == null) return;

            TradeSettings tradeSettings = Engine.Settings.Trade;
            if (!tradeSettings.DisableOnTradeCardExpiry) return;

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

        private static bool IsTradeCard(int cardType)
        {
            for (int i = 0; i < TradeCardTypes.Length; i++)
                if (TradeCardTypes[i] == cardType) return true;
            return false;
        }

        private static bool IsScoutCard(int cardType)
        {
            for (int i = 0; i < ScoutCardTypes.Length; i++)
                if (ScoutCardTypes[i] == cardType) return true;
            return false;
        }
    }
}
