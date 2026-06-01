using System;
using System.Windows.Forms;
using CommonTypes;
using Stronghold.AuthClient;

namespace Kingdoms.Bot.Modules
{
    public class AutoModuleSchedulerModule : BotModuleBase
    {
        public override string ModuleName { get { return "Auto Module Scheduler"; } }

        // After playing a card, wait this long before considering a replay, so we don't spam plays
        // while UserCardData is still catching up from the previous play.
        private const int CardPlayGraceSeconds = 90;

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

                bool hasCards = entry.PlayCardOnStart && entry.CardDefIds.Count > 0;

                if (inWindow)
                {
                    if (hasCards && entry.ReplayCardOnExpiry)
                        HandleCardManagedWindow(entry, targetModule, serverTime);
                    else
                        HandleSimpleWindow(entry, targetModule, hasCards);
                }
                else
                {
                    HandleOutOfWindow(entry, targetModule);
                }
            }
        }

        // Managed mode (ReplayCardOnExpiry on): keep the invariant "module is enabled only while ALL
        // configured cards are active". When a card expires, the module is PAUSED, the missing cards
        // are replayed, and the module is re-enabled only once they're confirmed active — so it can
        // never act (e.g. send traders) without its cards live.
        private void HandleCardManagedWindow(ModuleScheduleSettings entry, IBotModule targetModule, DateTime serverTime)
        {
            entry.WasAutoStarted = true;                 // we're managing it within this window
            entry.ManuallyDisabledDuringWindow = false;  // managed mode owns the module state

            CardData cd = GetCardData();
            if (cd == null) return; // can't read card state this tick — do nothing

            if (AllConfiguredCardsActive(entry, cd))
            {
                if (!targetModule.Enabled)
                {
                    targetModule.Enabled = true;
                    SyncModuleEnabledToSettings(entry.ModuleName, true);
                    LogInfo("'" + entry.ModuleName + "': all cards active — module enabled.");
                }
                return;
            }

            // One or more cards not active → pause the module so it can't run without them.
            if (targetModule.Enabled)
            {
                targetModule.Enabled = false;
                SyncModuleEnabledToSettings(entry.ModuleName, false);
                LogInfo("'" + entry.ModuleName + "': card(s) expired/inactive — module paused until replayed.");
            }

            // Replay the missing cards, but only once per grace period (UserCardData lags a play).
            if ((serverTime - entry.LastCardPlayTime).TotalSeconds >= CardPlayGraceSeconds)
            {
                int played = PlayMissingCards(entry, cd);
                entry.LastCardPlayTime = serverTime;
                if (played > 0)
                    LogInfo("'" + entry.ModuleName + "': played " + played + " card(s); module stays paused until they're active.");
                else
                    LogWarning("'" + entry.ModuleName + "': card(s) inactive and none available in inventory — module stays paused.");
            }
        }

        // Simple mode (no cards, or ReplayCardOnExpiry off): enable for the window, play configured
        // cards once on first start, keep running. Respects the user manually disabling it mid-window.
        private void HandleSimpleWindow(ModuleScheduleSettings entry, IBotModule targetModule, bool hasCards)
        {
            if (entry.ManuallyDisabledDuringWindow) return;
            if (targetModule.Enabled) return; // already running

            if (entry.WasAutoStarted)
            {
                // We enabled it earlier and it's now off, but we didn't disable it → the user did.
                entry.ManuallyDisabledDuringWindow = true;
                LogInfo("'" + entry.ModuleName + "': manually disabled during window — leaving off until next window.");
                return;
            }

            targetModule.Enabled = true;
            SyncModuleEnabledToSettings(entry.ModuleName, true);
            entry.WasAutoStarted = true;
            LogInfo("'" + entry.ModuleName + "': enabled for window.");
            if (hasCards)
                PlayAllCards(entry);
        }

        private void HandleOutOfWindow(ModuleScheduleSettings entry, IBotModule targetModule)
        {
            entry.ManuallyDisabledDuringWindow = false; // reset for the next window
            if (!entry.WasAutoStarted) return;

            if (targetModule.Enabled && entry.AutoDisableEnabled)
            {
                targetModule.Enabled = false;
                SyncModuleEnabledToSettings(entry.ModuleName, false);
                LogInfo("'" + entry.ModuleName + "': end of window — auto-disabled.");
            }
            entry.WasAutoStarted = false;
            entry.LastPlayedCardInstanceIds.Clear();
            entry.LastCardPlayTime = DateTime.MinValue;
        }

        /// <summary>True only if every configured card def ID currently has an active card of that type.</summary>
        private static bool AllConfiguredCardsActive(ModuleScheduleSettings entry, CardData cd)
        {
            for (int i = 0; i < entry.CardDefIds.Count; i++)
            {
                int defId = entry.CardDefIds[i];
                if (defId == 0) continue;
                if (!CardDefTypeIsActive(cd, defId)) return false;
            }
            return true;
        }

        /// <summary>Plays each configured card whose type is not currently active. Returns count played.</summary>
        private int PlayMissingCards(ModuleScheduleSettings entry, CardData cd)
        {
            int played = 0;
            for (int i = 0; i < entry.CardDefIds.Count; i++)
            {
                int defId = entry.CardDefIds[i];
                if (defId == 0) continue;
                if (CardDefTypeIsActive(cd, defId)) continue; // already active

                int instanceId = FindCardInstanceByDefId(defId);
                if (instanceId != 0)
                {
                    PlayCard(instanceId);
                    played++;
                    LogInfo("'" + entry.ModuleName + "': playing card (def " + defId + ", instance " + instanceId + ").");
                }
                else
                {
                    LogWarning("'" + entry.ModuleName + "': card (def " + defId + ") inactive and none in inventory.");
                }
            }
            return played;
        }

        /// <summary>Plays every card in the entry's list (used for the one-shot simple-mode start).</summary>
        private void PlayAllCards(ModuleScheduleSettings entry)
        {
            for (int i = 0; i < entry.CardDefIds.Count; i++)
            {
                int defId = entry.CardDefIds[i];
                if (defId == 0) continue;
                int instanceId = FindCardInstanceByDefId(defId);
                if (instanceId != 0)
                {
                    PlayCard(instanceId);
                    LogInfo("'" + entry.ModuleName + "': playing card " + (i + 1) + "/" + entry.CardDefIds.Count
                        + " (def " + defId + ", instance " + instanceId + ").");
                }
                else
                {
                    LogWarning("'" + entry.ModuleName + "': card " + (i + 1) + " (def " + defId + ") not found in inventory.");
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
