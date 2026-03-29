using System;

namespace Kingdoms.Bot
{
    public abstract class BotModuleBase : IBotModule
    {
        private DateTime _lastRun = DateTime.MinValue;
        private BotEngine _engine;

        public abstract string ModuleName { get; }
        public bool Enabled { get; set; }
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
