using System;
using System.Collections.Generic;
using System.Threading;

namespace Kingdoms.Bot.Modules
{
    public class BanquetModule : BotModuleBase
    {
        internal static readonly string[] GoodNames =
            { "Venison", "Furniture", "Metalware", "Clothes", "Wine", "Salt", "Spices", "Silk" };

        public override string ModuleName => "Banquet";

        public override TimeSpan Interval =>
            TimeSpan.FromSeconds(Engine.Settings.Banquet.CycleIntervalSeconds > 0
                ? Engine.Settings.Banquet.CycleIntervalSeconds : 300);

        protected override void OnTick()
        {
            BanquetSettings settings = Engine.Settings.Banquet;
            int researchLevel = (int)GameEngine.Instance.World.UserResearchData.Research_Craftsmanship;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            bool first = true;
            foreach (WorldMap.UserVillageData uvd in villages)
            {
                VillageBanquetSettings vs = settings.GetVillageSettings(uvd.villageID);
                if (vs.EnabledGoods.Count == 0) continue;

                if (!first && settings.DelayBetweenVillagesMs > 0)
                    Thread.Sleep(settings.DelayBetweenVillagesMs);
                first = false;

                VillageMap village = GameEngine.Instance.getVillage(uvd.villageID);
                if (village == null)
                {
                    LogWarning("Village not loaded: " + uvd.villageID);
                    continue;
                }

                village.banqueting.updateLevels(true);

                // Find the minimum resource level across all enabled + unlocked goods with production.
                // Goods beyond the research cap are already zeroed by updateLevels().
                int minAmt = int.MaxValue;
                foreach (int goodIdx in vs.EnabledGoods)
                {
                    if (goodIdx >= researchLevel) continue;
                    int level = village.banqueting.resourceLevels[goodIdx];
                    if (level > 0 && level < minAmt)
                        minAmt = level;
                }

                if (minAmt == int.MaxValue) continue;

                // Use equal minimum amounts per selected good (matches old restriction-mode behaviour).
                int[] amounts = new int[8];
                foreach (int goodIdx in vs.EnabledGoods)
                {
                    if (goodIdx >= researchLevel) continue;
                    if (village.banqueting.resourceLevels[goodIdx] >= minAmt)
                        amounts[goodIdx] = minAmt;
                }

                RemoteServices.Instance.VillageHoldBanquet(
                    village.VillageID,
                    amounts[0], amounts[1], amounts[2], amounts[3],
                    amounts[4], amounts[5], amounts[6], amounts[7]);

                LogInfo(string.Format("Village {0}: banquet [{1} {2} {3} {4} {5} {6} {7} {8}]",
                    village.VillageID,
                    amounts[0], amounts[1], amounts[2], amounts[3],
                    amounts[4], amounts[5], amounts[6], amounts[7]));
            }
        }

        public void RunNow()
        {
            Tick();
        }
    }
}
