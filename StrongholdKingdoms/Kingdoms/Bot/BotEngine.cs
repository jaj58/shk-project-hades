using System;
using System.Collections.Generic;

namespace Kingdoms.Bot
{
    public class BotEngine
    {
        public static BotEngine Instance;

        private readonly List<IBotModule> _modules = new List<IBotModule>();
        private BotSettings _settings;
        private bool _initialized;

        public BotSettings Settings
        {
            get { return _settings; }
        }

        public List<IBotModule> Modules
        {
            get { return _modules; }
        }

        public bool IsRunning
        {
            get { return _settings != null && _settings.BotEnabled; }
        }

        public void Init()
        {
            if (_initialized)
                return;

            _settings = BotSettings.Load();
            BotLogger.Log("BotEngine", BotLogLevel.Info, "Bot engine initializing...");

            RegisterModule(new Modules.VillageSyncModule());
            RegisterModule(new Modules.RadarModule());
            RegisterModule(new Modules.RecruitingModule());

            foreach (IBotModule module in _modules)
            {
                try
                {
                    module.Initialize(this);
                }
                catch (Exception ex)
                {
                    BotLogger.Log("BotEngine", BotLogLevel.Error,
                        "Failed to initialize module '" + module.ModuleName + "': " + ex.Message);
                }
            }

            ApplySettings();
            _initialized = true;
            BotLogger.Log("BotEngine", BotLogLevel.Info, "Bot engine initialized with " + _modules.Count + " module(s).");
        }

        public void RegisterModule(IBotModule module)
        {
            _modules.Add(module);
        }

        public void ApplySettings()
        {
            if (_settings == null)
                return;

            foreach (IBotModule module in _modules)
            {
                if (module is Modules.VillageSyncModule)
                    module.Enabled = _settings.VillageSync.Enabled;
                else if (module is Modules.RadarModule)
                    module.Enabled = _settings.Radar.Enabled;
                else if (module is Modules.RecruitingModule)
                    module.Enabled = _settings.Recruiting.Enabled;
            }
        }

        public void SaveSettings()
        {
            if (_settings == null)
                return;

            // Sync module enabled states back to settings before saving
            foreach (IBotModule module in _modules)
            {
                if (module is Modules.VillageSyncModule)
                    _settings.VillageSync.Enabled = module.Enabled;
                else if (module is Modules.RadarModule)
                    _settings.Radar.Enabled = module.Enabled;
                else if (module is Modules.RecruitingModule)
                    _settings.Recruiting.Enabled = module.Enabled;
            }

            _settings.Save();
            BotLogger.Log("BotEngine", BotLogLevel.Info, "Settings saved.");
        }

        public void ReloadSettings()
        {
            _settings = BotSettings.Load();
            ApplySettings();
            BotLogger.Log("BotEngine", BotLogLevel.Info, "Settings reloaded from disk.");
        }

        public void Tick()
        {
            if (!IsRunning)
                return;

            foreach (IBotModule module in _modules)
            {
                if (!module.Enabled)
                    continue;

                if ((DateTime.Now - module.LastRun) < module.Interval)
                    continue;

                try
                {
                    module.Tick();
                }
                catch (Exception ex)
                {
                    BotLogger.Log(module.ModuleName, BotLogLevel.Error,
                        "Tick error: " + ex.Message);
                }
            }
        }

        public void Shutdown()
        {
            BotLogger.Log("BotEngine", BotLogLevel.Info, "Bot engine shutting down...");
            foreach (IBotModule module in _modules)
            {
                try
                {
                    module.Shutdown();
                }
                catch
                {
                }
            }
            SaveSettings();
        }
    }
}
