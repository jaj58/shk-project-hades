using System;

namespace Kingdoms.Bot
{
    public interface IBotModule
    {
        string ModuleName { get; }
        bool Enabled { get; set; }
        // True when the module has explicitly-triggered on-demand work in flight that
        // should tick to completion even if Enabled is false (e.g. a manual action
        // queued from the UI). Passive/scheduled work must still require Enabled.
        bool HasPendingWork { get; }
        TimeSpan Interval { get; }
        DateTime LastRun { get; }
        void Initialize(BotEngine engine);
        void Tick();
        void Shutdown();
    }
}
