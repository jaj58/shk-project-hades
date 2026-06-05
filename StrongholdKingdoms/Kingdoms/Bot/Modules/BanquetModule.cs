using System;
using System.Collections.Generic;

namespace Kingdoms.Bot.Modules
{
    public class BanquetModule : BotModuleBase
    {
        internal static readonly string[] GoodNames =
            { "Venison", "Furniture", "Metalware", "Clothes", "Wine", "Salt", "Spices", "Silk" };

        private DateTime _lastFullCycle = DateTime.MinValue;
        private List<int> _villageQueue = new List<int>();
        private int _currentVillageIndex;
        private DateTime _lastVillageAction = DateTime.MinValue;

        public override string ModuleName => "Banquet";

        // Ticks every second; cycle and inter-village delays are enforced via timestamps
        public override TimeSpan Interval => TimeSpan.FromSeconds(1);

        protected override void OnInitialize()
        {
            _currentVillageIndex = 0;
            _villageQueue.Clear();
        }

        protected override void OnTick()
        {
            BanquetSettings settings = Engine.Settings.Banquet;

            // Start a new cycle if the queue is exhausted and enough time has passed
            if (_villageQueue.Count == 0 || _currentVillageIndex >= _villageQueue.Count)
            {
                if ((DateTime.Now - _lastFullCycle).TotalSeconds < settings.CycleIntervalSeconds)
                    return;

                RefreshVillageQueue(settings);
                _currentVillageIndex = 0;
                _lastFullCycle = DateTime.Now;

                if (_villageQueue.Count == 0)
                    return;
            }

            // Honour the inter-village delay without blocking
            if ((DateTime.Now - _lastVillageAction).TotalMilliseconds < settings.DelayBetweenVillagesMs)
                return;

            int villageId = _villageQueue[_currentVillageIndex];
            _currentVillageIndex++;
            _lastVillageAction = DateTime.Now;

            ProcessVillage(villageId, settings);
        }

        private void RefreshVillageQueue(BanquetSettings settings)
        {
            _villageQueue.Clear();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            int researchLevel = (int)GameEngine.Instance.World.UserResearchData.Research_Craftsmanship;

            foreach (WorldMap.UserVillageData uvd in villages)
            {
                if (GameEngine.Instance.World.isCapital(uvd.villageID)) continue;

                VillageBanquetSettings vs = settings.GetVillageSettings(uvd.villageID);
                if (vs.EnabledGoods.Count == 0) continue;

                // Check at least one enabled good is within the research level
                bool hasUnlocked = false;
                foreach (int idx in vs.EnabledGoods)
                {
                    if (idx < researchLevel) { hasUnlocked = true; break; }
                }
                if (hasUnlocked)
                    _villageQueue.Add(uvd.villageID);
            }
        }

        private void ProcessVillage(int villageId, BanquetSettings settings)
        {
            int researchLevel = (int)GameEngine.Instance.World.UserResearchData.Research_Craftsmanship;
            VillageBanquetSettings vs = settings.GetVillageSettings(villageId);

            VillageMap village = GameEngine.Instance.getVillage(villageId);
            if (village == null)
            {
                LogWarning("Village not loaded: " + villageId);
                return;
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

            if (minAmt == int.MaxValue) return;

            // Use equal minimum amounts per selected good (matches old restriction-mode behaviour)
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

        public void RunNow()
        {
            _lastFullCycle = DateTime.MinValue;
            _villageQueue.Clear();
            _currentVillageIndex = 0;
            _lastVillageAction = DateTime.MinValue;
            LogInfo("Run now triggered.");
        }

        protected override void OnShutdown()
        {
            _villageQueue.Clear();
            _currentVillageIndex = 0;
        }
    }
}
