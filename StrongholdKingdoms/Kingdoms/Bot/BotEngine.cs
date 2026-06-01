using System;
using System.Collections.Generic;

namespace Kingdoms.Bot
{
    public class BotEngine
    {
        public static BotEngine Instance;

        private readonly List<IBotModule> _modules = new List<IBotModule>();
        private BotSettings _settings;
        private int _worldId;

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

        public void Init(int worldId)
        {
            _worldId = worldId;
            _settings = BotSettings.Load(worldId);
            BotLogger.Log("BotEngine", BotLogLevel.Info, "Bot engine initializing for world " + worldId + "...");

            RegisterModule(new Modules.VillageSyncModule());
            RegisterModule(new Modules.RadarModule());
            RegisterModule(new Modules.RecruitingModule());
            RegisterModule(new Modules.CastleRepairModule());
            RegisterModule(new Modules.TradeModule());
            RegisterModule(new Modules.CardExpiryModule());
            RegisterModule(new Modules.VillageBuilderModule());
            RegisterModule(new Modules.AutoBombModule());
            RegisterModule(new Modules.AutoBombMultiModule());
            RegisterModule(new Modules.PopularityModule());
            RegisterModule(new Modules.ScoutModule());
            RegisterModule(new Modules.FreeCardCollectorModule());
            RegisterModule(new Modules.AutoCardModule());
            RegisterModule(new Modules.AutoModuleSchedulerModule());

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
            BotLogger.Log("BotEngine", BotLogLevel.Info, "Bot engine initialized with " + _modules.Count + " module(s).");
        }

        public void RegisterModule(IBotModule module)
        {
            _modules.Add(module);
        }

        public T GetModule<T>() where T : class, IBotModule
        {
            foreach (IBotModule m in _modules)
                if (m is T) return (T)m;
            return null;
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
                else if (module is Modules.CastleRepairModule)
                    module.Enabled = _settings.CastleRepair.Enabled;
                else if (module is Modules.TradeModule)
                    module.Enabled = _settings.Trade.Enabled;
                else if (module is Modules.CardExpiryModule)
                    module.Enabled = true; // Always enabled — monitors cards for other modules
                else if (module is Modules.FreeCardCollectorModule)
                    module.Enabled = true; // Always enabled — on/off controlled by Misc.CollectFreeCards
                else if (module is Modules.VillageBuilderModule)
                    module.Enabled = _settings.VillageBuilder.Enabled;
                else if (module is Modules.AutoBombModule)
                    module.Enabled = _settings.AutoBomb.Enabled;
                else if (module is Modules.AutoBombMultiModule)
                    module.Enabled = _settings.AutoBombMulti.Enabled;
                else if (module is Modules.PopularityModule)
                    module.Enabled = _settings.Popularity.Enabled;
                else if (module is Modules.AutoCardModule || module is Modules.AutoModuleSchedulerModule)
                    module.Enabled = true; // Always enabled — gated internally per-good / per-schedule
                else if (module is Modules.ScoutModule)
                    module.Enabled = _settings.Scout.Enabled;
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
                else if (module is Modules.CastleRepairModule)
                    _settings.CastleRepair.Enabled = module.Enabled;
                else if (module is Modules.TradeModule)
                    _settings.Trade.Enabled = module.Enabled;
                else if (module is Modules.VillageBuilderModule)
                    _settings.VillageBuilder.Enabled = module.Enabled;
                else if (module is Modules.AutoBombModule)
                    _settings.AutoBomb.Enabled = module.Enabled;
                else if (module is Modules.PopularityModule)
                    _settings.Popularity.Enabled = module.Enabled;
                // Auto modules are always enabled and gated internally — nothing to sync back.
                else if (module is Modules.ScoutModule)
                    _settings.Scout.Enabled = module.Enabled;
            }

            _settings.Save();
            BotLogger.Log("BotEngine", BotLogLevel.Info, "Settings saved.");
        }

        public void ReloadSettings()
        {
            _settings = BotSettings.Load(_worldId);
            ApplySettings();
            BotLogger.Log("BotEngine", BotLogLevel.Info, "Settings reloaded from disk.");
        }

        public void Tick()
        {
            if (!IsRunning)
                return;

            // Priority: if AutoBomb is actively launching, tick it first and
            // skip all other modules so no network call can delay timing.
            bool bombActive = false;
            foreach (IBotModule module in _modules)
            {
                Modules.AutoBombModule bomb = module as Modules.AutoBombModule;
                if (bomb != null && bomb.IsLaunching)
                {
                    bombActive = true;
                    try
                    {
                        module.Tick();
                    }
                    catch (Exception ex)
                    {
                        BotLogger.Log(module.ModuleName, BotLogLevel.Error,
                            "Tick error: " + ex.Message);
                    }
                    break;
                }
            }

            if (bombActive)
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
