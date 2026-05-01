using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class PopularityModule : BotModuleBase
    {
        private DateTime _lastFullCycle = DateTime.MinValue;
        private List<int> _villageQueue = new List<int>();
        private int _currentVillageIndex;
        private DateTime _lastVillageAction = DateTime.MinValue;

        public override string ModuleName
        {
            get { return "Popularity"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(1); }
        }

        private PopularitySettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.Popularity;
                return null;
            }
        }

        protected override void OnInitialize()
        {
            _currentVillageIndex = 0;
            _villageQueue.Clear();
        }

        protected override void OnTick()
        {
            PopularitySettings settings = Settings;
            if (settings == null) return;

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

            if ((DateTime.Now - _lastVillageAction).TotalMilliseconds < settings.DelayBetweenVillagesMs)
                return;

            int villageId = _villageQueue[_currentVillageIndex];
            _currentVillageIndex++;
            _lastVillageAction = DateTime.Now;

            ProcessVillage(villageId, settings);
        }

        private void RefreshVillageQueue(PopularitySettings settings)
        {
            _villageQueue.Clear();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            foreach (WorldMap.UserVillageData uvd in villages)
            {
                VillagePopularitySettings vs = settings.GetVillageSettings(uvd.villageID);
                if (vs.Mode != PopularityMode.Disabled)
                    _villageQueue.Add(uvd.villageID);
            }
        }

        private void ProcessVillage(int villageId, PopularitySettings settings)
        {
            if (GameEngine.Instance == null) return;

            VillageMap village = GameEngine.Instance.getVillage(villageId);
            if (village == null)
            {
                LogWarning("Village " + villageId + " not loaded, skipping.");
                return;
            }

            VillagePopularitySettings vs = settings.GetVillageSettings(villageId);
            PopularityMode mode = vs.Mode;

            if (mode == PopularityMode.Auto)
                mode = village.m_totalPeople >= village.m_housingCapacity
                    ? PopularityMode.MaxGold
                    : PopularityMode.MaxPopularity;

            switch (mode)
            {
                case PopularityMode.MaxPopularity:
                    ApplyMaxPopularity(village, villageId);
                    break;
                case PopularityMode.MaxGold:
                    ApplyMaxGold(village, villageId);
                    break;
            }
        }

        private void ApplyMaxPopularity(VillageMap village, int villageId)
        {
            int taxDelta = -village.m_taxLevel;
            int rationsDelta = 6 - village.m_rationsLevel;
            int aleDelta = 4 - village.m_aleRationsLevel;

            if (taxDelta == 0 && rationsDelta == 0 && aleDelta == 0)
            {
                LogDebug("Village " + villageId + " already at max popularity settings.");
                return;
            }

            LogInfo("Village " + villageId + ": MaxPopularity — tax=" + (village.m_taxLevel + taxDelta)
                + " rations=" + (village.m_rationsLevel + rationsDelta)
                + " ale=" + (village.m_aleRationsLevel + aleDelta));
            village.changeStats(taxDelta, rationsDelta, aleDelta);
        }

        private void ApplyMaxGold(VillageMap village, int villageId)
        {
            int targetRations = village.GetAffordableRationsLevel();
            int targetAle = village.GetAffordableAleRationsLevel();

            int rationsDelta = targetRations - village.m_rationsLevel;
            int aleDelta = targetAle - village.m_aleRationsLevel;

            if (rationsDelta != 0 || aleDelta != 0)
            {
                LogInfo("Village " + villageId + ": MaxGold — setting rations=" + targetRations + " ale=" + targetAle);
                village.changeStats(0, rationsDelta, aleDelta);
            }

            int maxTaxLevel = CardTypes.getMaxTaxLevel(GameEngine.Instance.cardsManager.UserCardData);
            int currentTax = village.m_taxLevel;
            int basePopularity = (int)village.getPopularity() - VillageBuildingsData.getTaxPopularityLevel(currentTax);

            LogDebug("Village " + villageId + ": base popularity (no tax) = " + basePopularity
                + ", current tax = " + currentTax + ", maxTax = " + maxTaxLevel);

            int bestTax = 0;
            for (int i = maxTaxLevel; i >= 0; --i)
            {
                if (basePopularity + VillageBuildingsData.getTaxPopularityLevel(i) >= 0)
                {
                    bestTax = i;
                    break;
                }
            }

            int taxDelta = bestTax - currentTax;
            if (taxDelta != 0)
            {
                LogInfo("Village " + villageId + ": MaxGold — setting tax=" + bestTax + " (delta " + taxDelta + ")");
                village.changeStats(taxDelta, 0, 0);
            }
            else
            {
                LogDebug("Village " + villageId + ": MaxGold — already at optimal tax=" + currentTax);
            }
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
