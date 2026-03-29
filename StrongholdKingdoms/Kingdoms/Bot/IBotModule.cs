using System;

namespace Kingdoms.Bot
{
    public interface IBotModule
    {
        string ModuleName { get; }
        bool Enabled { get; set; }
        TimeSpan Interval { get; }
        DateTime LastRun { get; }
        void Initialize(BotEngine engine);
        void Tick();
        void Shutdown();
    }
}
