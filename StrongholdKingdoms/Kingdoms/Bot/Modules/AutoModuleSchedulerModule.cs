using System;
using System.Windows.Forms;
using CommonTypes;
using Stronghold.AuthClient;

namespace Kingdoms.Bot.Modules
{
    public class AutoModuleSchedulerModule : BotModuleBase
    {
        public override string ModuleName { get { return "Auto Module Scheduler"; } }
        public override TimeSpan Interval { get { return TimeSpan.FromSeconds(60); } }

        protected override void OnInitialize() { }

        protected override void OnTick()
        {
            if (Engine == null || Engine.Settings == null || !Engine.Settings.Auto.Enabled)
                return;

            DateTime serverTime;
            try { serverTime = VillageMap.getCurrentServerTime(); }
            catch { return; }

            int currentHour = serverTime.Hour;

            foreach (ModuleScheduleSettings entry in Engine.Settings.Auto.ModuleSchedules)
            {
                IBotModule targetModule = FindModule(entry.ModuleName);
                if (targetModule == null) continue;

                bool inWindow = entry.HourlySchedule != null &&
                    entry.HourlySchedule.Length > currentHour &&
                    entry.HourlySchedule[currentHour];

                if (inWindow)
                {
                    if (!targetModule.Enabled && !entry.ManuallyDisabledDuringWindow)
                    {
                        // Module is off inside the window — either first start OR was disabled by
                        // CardExpiryModule (or similar) mid-window. Either way: re-enable it.
                        bool wasRunning = entry.WasAutoStarted;
                        targetModule.Enabled = true;
                        SyncModuleEnabledToSettings(entry.ModuleName, true);
                        entry.WasAutoStarted = true;

                        if (wasRunning)
                            LogInfo("Module '" + entry.ModuleName + "' was disabled mid-window (card expiry?) — re-enabling.");
                        else
                            LogInfo("Auto-enabled module '" + entry.ModuleName + "' at hour " + currentHour + ".");

                        // Play a card if configured (on first start, or on re-enable if ReplayCardOnExpiry is set)
                        bool shouldPlayCard = entry.PlayCardOnStart && entry.CardDefId != 0 &&
                            (!wasRunning || entry.ReplayCardOnExpiry);

                        if (shouldPlayCard)
                        {
                            int instanceId = FindCardInstanceByDefId(entry.CardDefId);
                            if (instanceId != 0)
                            {
                                PlayCard(instanceId);
                                entry.LastPlayedCardInstanceId = instanceId;
                                LogInfo("Playing card (def " + entry.CardDefId + ", instance " + instanceId + ") for module '" + entry.ModuleName + "'.");
                            }
                            else
                            {
                                LogWarning("Card def " + entry.CardDefId + " not found in inventory for module '" + entry.ModuleName + "'.");
                            }
                        }
                    }
                    else if (entry.WasAutoStarted && entry.PlayCardOnStart && entry.ReplayCardOnExpiry &&
                             entry.CardDefId != 0 && entry.LastPlayedCardInstanceId != 0)
                    {
                        // Module is running — check if the card expired mid-window and re-play if enabled
                        CardData cd = GetCardData();
                        if (cd != null && !CardInstanceIsActive(cd, entry.LastPlayedCardInstanceId))
                        {
                            entry.LastPlayedCardInstanceId = 0;
                            int instanceId = FindCardInstanceByDefId(entry.CardDefId);
                            if (instanceId != 0)
                            {
                                PlayCard(instanceId);
                                entry.LastPlayedCardInstanceId = instanceId;
                                LogInfo("Re-playing card for module '" + entry.ModuleName + "' after expiry.");
                            }
                        }
                    }
                }
                else
                {
                    // Not in scheduled window
                    entry.ManuallyDisabledDuringWindow = false;

                    if (entry.WasAutoStarted && !targetModule.Enabled)
                    {
                        // User manually disabled module during the window
                        entry.ManuallyDisabledDuringWindow = true;
                        entry.WasAutoStarted = false;
                        entry.LastPlayedCardInstanceId = 0;
                        LogInfo("Module '" + entry.ModuleName + "' was manually disabled — respecting override.");
                    }
                    else if (entry.WasAutoStarted && targetModule.Enabled && entry.AutoDisableEnabled)
                    {
                        targetModule.Enabled = false;
                        SyncModuleEnabledToSettings(entry.ModuleName, false);
                        entry.WasAutoStarted = false;
                        entry.LastPlayedCardInstanceId = 0;
                        LogInfo("Auto-disabled module '" + entry.ModuleName + "' at end of window (hour " + currentHour + ").");
                    }
                }
            }
        }

        private IBotModule FindModule(string moduleName)
        {
            foreach (IBotModule m in Engine.Modules)
            {
                if (GetModuleKey(m) == moduleName) return m;
            }
            return null;
        }

        private static string GetModuleKey(IBotModule m)
        {
            if (m is TradeModule)          return "Trade";
            if (m is RecruitingModule)     return "Recruiting";
            if (m is VillageBuilderModule) return "VillageBuilder";
            if (m is CastleRepairModule)   return "CastleRepair";
            if (m is PopularityModule)     return "Popularity";
            if (m is ScoutModule)          return "Scout";
            return null;
        }

        private void SyncModuleEnabledToSettings(string moduleName, bool enabled)
        {
            if (Engine == null || Engine.Settings == null) return;
            switch (moduleName)
            {
                case "Trade":          Engine.Settings.Trade.Enabled = enabled; break;
                case "Recruiting":     Engine.Settings.Recruiting.Enabled = enabled; break;
                case "VillageBuilder": Engine.Settings.VillageBuilder.Enabled = enabled; break;
                case "CastleRepair":   Engine.Settings.CastleRepair.Enabled = enabled; break;
                case "Popularity":     Engine.Settings.Popularity.Enabled = enabled; break;
                case "Scout":          Engine.Settings.Scout.Enabled = enabled; break;
            }
        }

        private static CardData GetCardData()
        {
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.cardsManager == null)
                    return null;
                return GameEngine.Instance.cardsManager.UserCardData;
            }
            catch { return null; }
        }

        private static bool CardInstanceIsActive(CardData cardData, int instanceId)
        {
            if (cardData == null || cardData.cards == null) return false;
            for (int i = 0; i < cardData.cards.Length; i++)
                if (cardData.cards[i] == instanceId) return true;
            return false;
        }

        private static int FindCardInstanceByDefId(int defId)
        {
            try
            {
                var mgr = GameEngine.Instance != null ? GameEngine.Instance.cardsManager : null;
                if (mgr == null) return 0;

                CardData cd = mgr.UserCardData;
                var activeIds = new System.Collections.Generic.HashSet<int>();
                if (cd != null && cd.cards != null)
                    foreach (int c in cd.cards)
                        if (c != 0) activeIds.Add(c);

                foreach (var kvp in mgr.ProfileCards)
                {
                    if (kvp.Value != null && kvp.Value.id == defId && !activeIds.Contains(kvp.Key))
                        return kvp.Key;
                }
            }
            catch { }
            return 0;
        }

        private void PlayCard(int instanceId)
        {
            try
            {
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(
                    URLs.ProfileProtocol,
                    URLs.ProfileServerAddressCards,
                    URLs.ProfileServerPort,
                    URLs.ProfileCardPath);
                XmlRpcCardsRequest req = new XmlRpcCardsRequest(
                    RemoteServices.Instance.UserGuid.ToString().Replace("-", ""),
                    RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""),
                    instanceId.ToString(),
                    "-1",
                    RemoteServices.Instance.ProfileWorldID.ToString());
                provider.PlayUserCard(
                    req,
                    delegate(ICardsProvider p, ICardsResponse r)
                    {
                        if (r.SuccessCode.HasValue && r.SuccessCode.Value == 1)
                            LogInfo("Module card instance " + instanceId + " played successfully.");
                        else
                            LogWarning("Module card instance " + instanceId + " play failed: " + r.Message);
                    },
                    (Control)InterfaceMgr.Instance.getDXBasePanel());
            }
            catch (Exception ex)
            {
                LogError("PlayCard failed for instance " + instanceId + ": " + ex.Message);
            }
        }
    }
}
