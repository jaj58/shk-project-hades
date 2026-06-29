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

        // Priority queue of villages to FORCE a full (authoritative) re-download
        // of — drained one-per-throttle ahead of the normal lightweight sync.
        // Fed by stale-data errors (Trade), the periodic timer and the manual
        // button. May be enqueued from other module threads, hence the lock.
        private readonly List<int> _forceRedownloadQueue = new List<int>();
        private readonly object _queueLock = new object();
        private DateTime _lastForceRedownloadCycle = DateTime.MinValue;

        private int ForceRedownloadIntervalMinutes
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.VillageSync.ForceRedownloadIntervalMinutes;
                return 0;
            }
        }

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

            // The periodic full re-download just feeds the priority queue; the
            // queue itself is what actually does the (throttled) downloads.
            EnqueuePeriodicForceRedownloadIfDue();

            // Force re-downloads take priority over the lightweight background
            // cycle. Process at most one per throttle so we never fire two heavy
            // village downloads back to back.
            if (ProcessForceRedownloadQueue())
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

        /// <summary>
        /// Requests an immediate refresh of the specified village.
        /// Uses a background (non-UI) refresh if the village map is already loaded,
        /// or a force-download if it hasn't been loaded yet.
        /// Safe to call from any thread for already-loaded villages.
        /// </summary>
        public void RefreshVillageNow(int villageId)
        {
            try
            {
                VillageMap village = GameEngine.Instance != null
                    ? GameEngine.Instance.getVillage(villageId) : null;
                if (village != null)
                    BackGroundRefresh(village);
                else
                    ForceRefresh(villageId);
                LogInfo("Refreshed village [" + villageId + "] on request.");
            }
            catch (Exception ex)
            {
                LogWarning("RefreshVillageNow [" + villageId + "] failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Queue a single village for an authoritative full re-download. Safe to
        /// call from any module thread; the download itself runs on this module's
        /// tick. Deduplicated, so repeated calls for the same village coalesce.
        /// </summary>
        public void RequestForceRedownload(int villageId)
        {
            lock (_queueLock)
            {
                if (!_forceRedownloadQueue.Contains(villageId))
                    _forceRedownloadQueue.Add(villageId);
            }
        }

        /// <summary>
        /// Queue every enabled village for a full re-download (manual button /
        /// periodic timer).
        /// </summary>
        public void RequestForceRedownloadAll()
        {
            lock (_queueLock)
            {
                foreach (int id in _activeVillageIds)
                {
                    if (!_forceRedownloadQueue.Contains(id))
                        _forceRedownloadQueue.Add(id);
                }
            }
            LogInfo("Queued full re-download of " + _activeVillageIds.Count + " village(s).");
        }

        public int PendingForceRedownloadCount
        {
            get { lock (_queueLock) { return _forceRedownloadQueue.Count; } }
        }

        private void EnqueuePeriodicForceRedownloadIfDue()
        {
            int intervalMin = ForceRedownloadIntervalMinutes;
            if (intervalMin <= 0)
                return;

            // Seed the timer on first run so we don't fire immediately at startup.
            if (_lastForceRedownloadCycle == DateTime.MinValue)
            {
                _lastForceRedownloadCycle = DateTime.Now;
                return;
            }

            if ((DateTime.Now - _lastForceRedownloadCycle).TotalMinutes < intervalMin)
                return;

            _lastForceRedownloadCycle = DateTime.Now;
            LogInfo("Periodic full re-download due (every " + intervalMin + " min).");
            RequestForceRedownloadAll();
        }

        private bool ProcessForceRedownloadQueue()
        {
            int villageId;
            lock (_queueLock)
            {
                if (_forceRedownloadQueue.Count == 0)
                    return false;

                if ((DateTime.Now - _lastVillageSync).TotalMilliseconds < DelayBetweenVillagesMs)
                    return true; // work pending but throttled — hold the queue's priority

                villageId = _forceRedownloadQueue[0];
                _forceRedownloadQueue.RemoveAt(0);
            }

            _lastVillageSync = DateTime.Now;
            try
            {
                string villageName = GameEngine.Instance.World.getVillageName(villageId);
                LogInfo("Full re-download [" + villageId + "] " + villageName + " (queued).");
                ForceRefresh(villageId);
            }
            catch (Exception ex)
            {
                LogError("Force re-download of village " + villageId + " failed: " + ex.Message);
            }
            return true;
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
            lock (_queueLock) { _forceRedownloadQueue.Clear(); }
        }
    }
}
