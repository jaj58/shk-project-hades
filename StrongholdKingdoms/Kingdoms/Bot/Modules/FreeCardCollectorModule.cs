using System;

namespace Kingdoms.Bot.Modules
{
    public class FreeCardCollectorModule : BotModuleBase
    {
        private DateTime _lastCollectAttempt = DateTime.MinValue;
        private DateTime _nextAvailableAt = DateTime.MinValue;

        public override string ModuleName
        {
            get { return "Free Card Collector"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(30); }
        }

        private MiscSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.Misc;
                return null;
            }
        }

        protected override void OnInitialize()
        {
            _lastCollectAttempt = DateTime.MinValue;
            _nextAvailableAt = DateTime.MinValue;
        }

        protected override void OnTick()
        {
            MiscSettings settings = Settings;
            if (settings == null || !settings.CollectFreeCards) return;

            // Don't retry collection more often than once per 5 minutes
            if ((DateTime.Now - _lastCollectAttempt).TotalMinutes < 5) return;

            // If we know when the next card is available, wait until then
            if (_nextAvailableAt > DateTime.Now) return;

            if (!FreeCardsPanel.IsFreeCardAvailable())
            {
                TimeSpan wait = FreeCardsPanel.TimeUntilNextFreeCard();
                if (wait != TimeSpan.MaxValue && wait.TotalSeconds > 0)
                {
                    _nextAvailableAt = DateTime.Now.Add(wait);
                    LogDebug("Next free card available in " + (int)wait.TotalMinutes + "m " + (wait.Seconds) + "s.");
                }
                return;
            }

            _lastCollectAttempt = DateTime.Now;
            _nextAvailableAt = DateTime.MinValue;

            LogInfo("Free card available — collecting.");
            bool sent = FreeCardsPanel.BotCollectFreeCard();
            if (sent)
                LogInfo("Free card collect request sent.");
            else
                LogWarning("Free card collect request failed (guard active or game not ready).");
        }

        protected override void OnShutdown() { }
    }
}
