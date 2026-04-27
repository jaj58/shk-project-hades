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
