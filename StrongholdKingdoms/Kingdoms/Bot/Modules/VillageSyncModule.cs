using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class VillageSyncModule : BotModuleBase
    {
        private int _currentIndex;
        private List<int> _activeVillageIds = new List<int>();
        private DateTime _lastVillageSync = DateTime.MinValue;
        private DateTime _lastVillageListRefresh = DateTime.MinValue;
        private DateTime _lastCycleEnd = DateTime.MinValue;
        private bool _cycleComplete = true;

        public override string ModuleName
        {
            get { return "Village Sync"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(5); }
        }

        private int DelayBetweenVillagesMs
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.VillageSync.DelayBetweenVillagesMs;
                return 3000;
            }
        }

        private int CycleIntervalSeconds
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.VillageSync.IntervalSeconds;
                return 120;
            }
        }

        protected override void OnInitialize()
        {
            _currentIndex = 0;
            _cycleComplete = true;
        }

        public List<int> GetAllKnownVillageIds()
        {
            List<int> result = new List<int>();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return result;

            try
            {
                List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
                if (villages != null)
                {
                    foreach (WorldMap.UserVillageData village in villages)
                        result.Add(village.villageID);
                }
            }
            catch
            {
            }
            return result;
        }

        public static string GetVillageTypeLabel(int villageId)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return "Village";

            VillageData data = GameEngine.Instance.World.getVillageData(villageId);
            if (data == null)
                return "Village";

            if (data.countryCapital)
                return "Country";
            if (data.provinceCapital)
                return "Province";
            if (data.countyCapital)
                return "County";
            if (data.regionCapital)
                return "Parish";
            return "Village";
        }

        protected override void OnTick()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            RefreshVillageListIfNeeded();

            if (_activeVillageIds.Count == 0)
                return;

            if (_cycleComplete)
            {
                if ((DateTime.Now - _lastCycleEnd).TotalSeconds < CycleIntervalSeconds)
                    return;

                _currentIndex = 0;
                _cycleComplete = false;
                LogInfo("Starting new sync cycle for " + _activeVillageIds.Count + " village(s).");
            }

            if (_currentIndex >= _activeVillageIds.Count)
            {
                _cycleComplete = true;
                _lastCycleEnd = DateTime.Now;
                return;
            }

            if ((DateTime.Now - _lastVillageSync).TotalMilliseconds < DelayBetweenVillagesMs)
                return;

            int villageId = _activeVillageIds[_currentIndex];
            string villageName = GameEngine.Instance.World.getVillageName(villageId);
            string typeLabel = GetVillageTypeLabel(villageId);

            LogInfo("Syncing " + typeLabel + " [" + villageId + "] " + villageName +
                    " (" + (_currentIndex + 1) + "/" + _activeVillageIds.Count + ")");

            try
            {
                _lastVillageSync = DateTime.Now;

                VillageMap village = GameEngine.Instance.getVillage(villageId);
                if (village == null)
                {
                    ForceRefresh(villageId);
                }
                else
                {
                    BackGroundRefresh(village);
                }
            }
            catch (Exception ex)
            {
                LogError("Failed to request sync for village " + villageId + ": " + ex.Message);
            }

            _currentIndex++;
        }

        private void BackGroundRefresh(VillageMap village)
        {
            LogInfo("Background refreshing village [" + village.VillageID + "] ");
            RemoteServices.Instance.set_VillageBuildingChangeRates_UserCallBack(new RemoteServices.VillageBuildingChangeRates_UserCallBack(village.villageBuildingChangeRatesCallback));
            RemoteServices.Instance.VillageBuildingChangeRates(village.VillageID, -1, -1, -1, -1);
        }

        private void ForceRefresh(int villageID)
        {
            LogInfo("Force refreshing village [" + villageID + "]");
            InterfaceMgr.Instance.setVillageNameBar(villageID);
            GameEngine.Instance.forceDownloadCurrentVillage();
        }

        private void RefreshVillageListIfNeeded()
        {
            if (_activeVillageIds.Count > 0 &&
                (DateTime.Now - _lastVillageListRefresh).TotalSeconds < CycleIntervalSeconds)
                return;

            _lastVillageListRefresh = DateTime.Now;

            try
            {
                List<int> allIds = GetAllKnownVillageIds();
                VillageSyncSettings settings = (Engine != null && Engine.Settings != null)
                    ? Engine.Settings.VillageSync
                    : null;

                _activeVillageIds.Clear();
                foreach (int id in allIds)
                {
                    if (settings == null || settings.IsVillageEnabled(id))
                        _activeVillageIds.Add(id);
                }

                LogDebug("Village list refreshed: " + _activeVillageIds.Count +
                         " enabled out of " + allIds.Count + " total.");
            }
            catch (Exception ex)
            {
                LogError("Failed to refresh village list: " + ex.Message);
            }
        }

        protected override void OnShutdown()
        {
            _activeVillageIds.Clear();
            _currentIndex = 0;
            _cycleComplete = true;
        }
    }
}
