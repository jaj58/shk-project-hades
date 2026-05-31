using System;
using System.Windows.Forms;
using CommonTypes;
using Stronghold.AuthClient;

namespace Kingdoms.Bot.Modules
{
    public class AutoModuleSchedulerModule : BotModuleBase
    {
        public override string ModuleName { get { return "Auto Module Scheduler"; } }

        // Interval is user-configurable via Auto.ModuleCheckIntervalSeconds (min 10s).
        public override TimeSpan Interval
        {
            get
            {
                int s = 60;
                if (Engine != null && Engine.Settings != null && Engine.Settings.Auto != null)
                    s = Engine.Settings.Auto.ModuleCheckIntervalSeconds;
                if (s < 10) s = 10;
                return TimeSpan.FromSeconds(s);
            }
        }

        protected override void OnInitialize() { }

        protected override void OnTick()
        {
            if (Engine == null || Engine.Settings == null || Engine.Settings.Auto == null)
                return;

            DateTime serverTime;
            try { serverTime = VillageMap.getCurrentServerTime(); }
            catch { return; }

            int currentHour = serverTime.Hour;

            int scheduledCount = 0;
            foreach (ModuleScheduleSettings ms in Engine.Settings.Auto.ModuleSchedules)
                if (ms.HourlySchedule != null && currentHour < ms.HourlySchedule.Length && ms.HourlySchedule[currentHour])
                    scheduledCount++;
            LogDebug("Tick — server hour " + currentHour + ": " + scheduledCount + " module(s) scheduled this hour of "
                + Engine.Settings.Auto.ModuleSchedules.Count + " configured.");

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
                        bool wasRunning = entry.WasAutoStarted;
                        targetModule.Enabled = true;
                        SyncModuleEnabledToSettings(entry.ModuleName, true);
                        entry.WasAutoStarted = true;

                        if (wasRunning)
                            LogInfo("Module '" + entry.ModuleName + "' was disabled mid-window (card expiry?) — re-enabling.");
                        else
                            LogInfo("Auto-enabled module '" + entry.ModuleName + "' at hour " + currentHour + ".");

                        bool shouldPlay = entry.PlayCardOnStart && entry.CardDefIds.Count > 0
                            && (!wasRunning || entry.ReplayCardOnExpiry);
                        if (shouldPlay)
                            PlayAllCards(entry);
                    }
                    else if (entry.WasAutoStarted && entry.PlayCardOnStart && entry.ReplayCardOnExpiry
                             && entry.CardDefIds.Count > 0)
                    {
                        // Re-play any card from the list that has expired mid-window
                        CardData cd = GetCardData();
                        if (cd != null)
                            ReplayExpiredCards(entry, cd);
                    }
                }
                else
                {
                    entry.ManuallyDisabledDuringWindow = false;

                    if (entry.WasAutoStarted && !targetModule.Enabled)
                    {
                        entry.ManuallyDisabledDuringWindow = true;
                        entry.WasAutoStarted = false;
                        entry.LastPlayedCardInstanceIds.Clear();
                        LogInfo("Module '" + entry.ModuleName + "' was manually disabled — respecting override.");
                    }
                    else if (entry.WasAutoStarted && targetModule.Enabled && entry.AutoDisableEnabled)
                    {
                        targetModule.Enabled = false;
                        SyncModuleEnabledToSettings(entry.ModuleName, false);
                        entry.WasAutoStarted = false;
                        entry.LastPlayedCardInstanceIds.Clear();
                        LogInfo("Auto-disabled module '" + entry.ModuleName + "' at end of window (hour " + currentHour + ").");
                    }
                }
            }
        }

        /// <summary>Play every card in the entry's CardDefIds list and track the instance IDs.</summary>
        private void PlayAllCards(ModuleScheduleSettings entry)
        {
            entry.LastPlayedCardInstanceIds.Clear();
            for (int i = 0; i < entry.CardDefIds.Count; i++)
            {
                int defId = entry.CardDefIds[i];
                if (defId == 0) continue;
                int instanceId = FindCardInstanceByDefId(defId);
                if (instanceId != 0)
                {
                    PlayCard(instanceId);
                    entry.LastPlayedCardInstanceIds.Add(instanceId);
                    LogInfo("Playing card " + (i + 1) + "/" + entry.CardDefIds.Count
                        + " (def " + defId + ", instance " + instanceId + ") for module '" + entry.ModuleName + "'.");
                }
                else
                {
                    entry.LastPlayedCardInstanceIds.Add(0); // placeholder so indices stay aligned
                    LogWarning("Card " + (i + 1) + " (def " + defId + ") not found in inventory for module '" + entry.ModuleName + "'.");
                }
            }
        }

        /// <summary>Re-play any card from the list whose definition type is no longer active.</summary>
        private void ReplayExpiredCards(ModuleScheduleSettings entry, CardData cd)
        {
            // Ensure tracking list is same length as CardDefIds
            while (entry.LastPlayedCardInstanceIds.Count < entry.CardDefIds.Count)
                entry.LastPlayedCardInstanceIds.Add(0);

            for (int i = 0; i < entry.CardDefIds.Count; i++)
            {
                int defId = entry.CardDefIds[i];
                if (defId == 0) continue;

                // Check by definition TYPE rather than exact instance ID.
                // UserCardData may not be refreshed immediately after PlayCard(), so checking
                // the tracked instance ID directly would always return false (triggering
                // replays every scheduler tick). Checking the type correctly handles lag.
                if (CardDefTypeIsActive(cd, defId))
                    continue; // card of this type is still active

                int instanceId = FindCardInstanceByDefId(defId);
                if (instanceId != 0)
                {
                    PlayCard(instanceId);
                    entry.LastPlayedCardInstanceIds[i] = instanceId;
                    LogInfo("Re-playing card " + (i + 1) + " (def " + defId + ") for module '"
                        + entry.ModuleName + "' after expiry.");
                }
            }
        }

        /// <summary>
        /// Returns true if any currently active card instance has the given definition ID.
        /// More reliable than instance-ID matching since UserCardData refresh can lag behind PlayCard().
        /// </summary>
        private static bool CardDefTypeIsActive(CardData cd, int defId)
        {
            if (cd == null || cd.cards == null) return false;
            for (int i = 0; i < cd.cards.Length; i++)
            {
                if (cd.cards[i] == 0) continue;
                try
                {
                    if (CardTypes.getCardType(cd.cards[i]) == defId) return true;
                }
                catch { }
            }
            return false;
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
