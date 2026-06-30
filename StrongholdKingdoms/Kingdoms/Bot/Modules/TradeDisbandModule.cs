using System;
using System.Collections.Generic;
using System.Text;

namespace Kingdoms.Bot.Modules
{
    /// <summary>
    /// Always-on watcher that drains and disbands the Trade module's merchants
    /// after an automatic disable. The Trade module itself stops ticking once it
    /// is disabled (the engine skips disabled modules), so this separate module
    /// carries out the disband on the main tick thread.
    ///
    /// It activates only when <see cref="TradeModule.DisbandPending"/> is set
    /// (raised in TradeModule.OnDisable when "Disband traders on disable" is on and
    /// the disable was not a manual untick). Each pass it disbands any traders that
    /// have returned home; it keeps retrying until none remain anywhere, the Trade
    /// module is re-enabled (cancels the disband), or a safety cap elapses.
    /// </summary>
    public class TradeDisbandModule : BotModuleBase
    {
        // Disbanding holds a single ~45 s server-side lock per village, so there is
        // no point ticking faster than that.
        private const int TickSeconds = 45;
        private const int DisbandThrottleSeconds = 45;
        // Safety cap: traders out on long trips must come home before they can be
        // disbanded; if some never return, give up after this long and warn.
        private const int MaxDrainMinutes = 60;
        // peopleType for Traders (matches RecruitingModule.GetDisbandTypeId("Traders")).
        private const int TraderPeopleType = 2;

        private readonly Dictionary<int, DateTime> _lastDisband = new Dictionary<int, DateTime>();

        public override string ModuleName
        {
            get { return "TradeDisband"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(TickSeconds); }
        }

        protected override void OnTick()
        {
            TradeModule trade = Engine != null ? Engine.GetModule<TradeModule>() : null;
            if (trade == null || !trade.DisbandPending)
                return;

            // Cancel if trading was turned back on (manual re-enable, or the Auto
            // scheduler replaying cards / starting a new window). Don't strand the
            // merchants the user just put back to work.
            if (trade.Enabled)
            {
                trade.DisbandPending = false;
                _lastDisband.Clear();
                LogInfo("Trade re-enabled — auto-disband cancelled.");
                return;
            }

            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null)
                return;

            int totalRemaining = 0;
            List<int> stillOut = new List<int>();

            foreach (WorldMap.UserVillageData uvd in villages)
            {
                int villageId = uvd.villageID;
                VillageMap map = GameEngine.Instance.getVillage(villageId);
                if (map == null)
                    continue;

                int total = map.numTraders();           // home + deployed
                totalRemaining += total;
                if (total > 0)
                    stillOut.Add(villageId);

                int atHome = map.numFreeTraders();       // disbandable now
                if (atHome <= 0)
                    continue;

                // Per-village throttle: disbandPeople holds a private ~45 s lock, so
                // a second call within the window would silently no-op.
                DateTime last;
                if (_lastDisband.TryGetValue(villageId, out last) &&
                    (DateTime.Now - last).TotalSeconds < DisbandThrottleSeconds)
                    continue;

                try
                {
                    map.disbandPeople(TraderPeopleType, atHome);
                    _lastDisband[villageId] = DateTime.Now;
                    LogInfo("Village " + villageId + ": disbanding " + atHome + " trader(s) (" +
                            total + " still owned).");
                }
                catch (Exception ex)
                {
                    LogError("Village " + villageId + ": failed to disband traders: " + ex.Message);
                }
            }

            if (totalRemaining == 0)
            {
                trade.DisbandPending = false;
                _lastDisband.Clear();
                LogInfo("All traders disbanded.");
                return;
            }

            if ((DateTime.Now - trade.DisbandStartedAt).TotalMinutes >= MaxDrainMinutes)
            {
                trade.DisbandPending = false;
                _lastDisband.Clear();
                StringBuilder ids = new StringBuilder();
                for (int i = 0; i < stillOut.Count; i++)
                {
                    if (i > 0) ids.Append(", ");
                    ids.Append(stillOut[i]);
                }
                LogWarning("Disband timed out after " + MaxDrainMinutes + " min; " + totalRemaining +
                           " trader(s) still out (village(s): " + ids + ").");
            }
        }
    }
}
