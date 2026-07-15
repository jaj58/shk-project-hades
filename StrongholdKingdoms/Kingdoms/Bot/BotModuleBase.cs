using System;

namespace Kingdoms.Bot
{
    public abstract class BotModuleBase : IBotModule
    {
        private DateTime _lastRun = DateTime.MinValue;
        private BotEngine _engine;

        public abstract string ModuleName { get; }
        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled && !value)
                    OnDisable();
                _enabled = value;
            }
        }
        public abstract TimeSpan Interval { get; }

        // Modules with on-demand work override this to keep ticking while disabled.
        public virtual bool HasPendingWork { get { return false; } }

        public DateTime LastRun
        {
            get { return _lastRun; }
        }

        protected BotEngine Engine
        {
            get { return _engine; }
        }

        public void Initialize(BotEngine engine)
        {
            _engine = engine;
            LogInfo("Module initialized.");
            OnInitialize();
        }

        public void Tick()
        {
            _lastRun = DateTime.Now;
            OnTick();
        }

        public void Shutdown()
        {
            LogInfo("Module shutting down.");
            OnShutdown();
        }

        protected virtual void OnInitialize() { }
        protected abstract void OnTick();
        protected virtual void OnShutdown() { }

        /// <summary>
        /// Called when Enabled transitions from true to false at runtime.
        /// Override to stop any background work (threads, timers) that should
        /// not continue running after the module is disabled.
        /// </summary>
        protected virtual void OnDisable() { }

        /// <summary>
        /// Removes a card instance from the local ProfileCards inventory after the
        /// server confirmed it was consumed. Mirrors what the game's own UI does
        /// (PlayCardsPanel/CardBarGDI call removeProfileCard on play) — without this
        /// the played instance lingers in ProfileCards and gets re-selected even
        /// though the server no longer accepts it. Missing IDs are ignored, so it
        /// is safe to call from any play-response callback.
        /// </summary>
        protected void RemoveCardFromLocalInventory(int instanceId)
        {
            try
            {
                var mgr = GameEngine.Instance != null ? GameEngine.Instance.cardsManager : null;
                if (mgr == null) return;
                if (mgr.ProfileCards.ContainsKey(instanceId))
                {
                    mgr.removeProfileCard(instanceId);
                    LogDebug("Removed played card instance " + instanceId + " from local inventory.");
                }
            }
            catch (Exception ex)
            {
                LogWarning("Could not remove card instance " + instanceId + " from local inventory: " + ex.Message);
            }
        }

        protected void LogDebug(string message)
        {
            BotLogger.Log(ModuleName, BotLogLevel.Debug, message);
        }

        protected void LogInfo(string message)
        {
            BotLogger.Log(ModuleName, BotLogLevel.Info, message);
        }

        protected void LogWarning(string message)
        {
            BotLogger.Log(ModuleName, BotLogLevel.Warning, message);
        }

        protected void LogError(string message)
        {
            BotLogger.Log(ModuleName, BotLogLevel.Error, message);
        }
    }
}
