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
        private bool _waitingForCallback;
        private DateTime _lastVillageListRefresh = DateTime.MinValue;

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
            _waitingForCallback = false;
        }

        public List<int> GetAllKnownVillageIds()
        {
            List<int> result = new List<int>();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return result;

            try
            {
                List<int> ids = GameEngine.Instance.World.getUserVillageIDList();
                if (ids != null)
                    result.AddRange(ids);
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

            if (_waitingForCallback)
                return;

            RefreshVillageListIfNeeded();

            if (_activeVillageIds.Count == 0)
            {
                LogDebug("No villages enabled for sync.");
                return;
            }

            if (_currentIndex >= _activeVillageIds.Count)
            {
                if ((DateTime.Now - _lastVillageListRefresh).TotalSeconds < CycleIntervalSeconds)
                    return;

                _currentIndex = 0;
                _lastVillageListRefresh = DateTime.Now;
                LogInfo("Starting new sync cycle for " + _activeVillageIds.Count + " village(s).");
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
                _waitingForCallback = true;
                _lastVillageSync = DateTime.Now;

                RemoteServices.Instance.set_UpdateVillageResourcesInfo_UserCallBack(
                    new RemoteServices.UpdateVillageResourcesInfo_UserCallBack(SyncCallback));
                RemoteServices.Instance.UpdateVillageResourcesInfo(villageId);
            }
            catch (Exception ex)
            {
                _waitingForCallback = false;
                LogError("Failed to request sync for village " + villageId + ": " + ex.Message);
            }
        }

        private void SyncCallback(UpdateVillageResourcesInfo_ReturnType returnData)
        {
            _waitingForCallback = false;

            if (returnData.Success)
            {
                int villageId = returnData.villageID;
                string villageName = GameEngine.Instance.World.getVillageName(villageId);

                VillageMap village = GameEngine.Instance.getVillage(villageId);
                if (village != null)
                {
                    village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                    LogInfo("Synced [" + villageId + "] " + villageName + " successfully.");
                }
                else
                {
                    LogDebug("[" + villageId + "] " + villageName +
                             " data received but no local VillageMap exists.");
                }

                _currentIndex++;
            }
            else
            {
                LogWarning("Sync failed for village index " + _currentIndex + ", will retry next cycle.");
                _currentIndex++;
            }
        }

        private void RefreshVillageListIfNeeded()
        {
            if (_activeVillageIds.Count > 0 &&
                (DateTime.Now - _lastVillageListRefresh).TotalSeconds < CycleIntervalSeconds)
                return;

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

                if (_currentIndex >= _activeVillageIds.Count)
                    _currentIndex = 0;

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
            _waitingForCallback = false;
        }
    }
}
