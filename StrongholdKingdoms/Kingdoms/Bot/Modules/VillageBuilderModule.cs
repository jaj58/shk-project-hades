using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class VillageBuilderModule : BotModuleBase
    {
        private DateTime _lastFullCycle = DateTime.MinValue;
        private int _currentVillageIndex;
        private List<int> _villageQueue = new List<int>();
        private DateTime _lastVillageAction = DateTime.MinValue;

        public override string ModuleName
        {
            get { return "Village Builder"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(5); }
        }

        private VillageBuilderSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.VillageBuilder;
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
            VillageBuilderSettings settings = Settings;
            if (settings == null) return;

            double elapsed = (DateTime.Now - _lastFullCycle).TotalSeconds;
            if (elapsed >= settings.CycleIntervalSeconds || _villageQueue.Count == 0)
            {
                BuildVillageQueue(settings);
                _lastFullCycle = DateTime.Now;
                _currentVillageIndex = 0;

                if (_villageQueue.Count == 0)
                {
                    LogDebug("No villages with building layouts enabled.");
                    return;
                }
            }

            if (_currentVillageIndex >= _villageQueue.Count)
                return;

            double delaySec = settings.DelayBetweenVillagesMs / 1000.0;
            if ((DateTime.Now - _lastVillageAction).TotalSeconds < delaySec)
                return;

            int villageId = _villageQueue[_currentVillageIndex];
            _currentVillageIndex++;
            _lastVillageAction = DateTime.Now;

            ProcessVillage(villageId, settings);
        }

        private void BuildVillageQueue(VillageBuilderSettings settings)
        {
            _villageQueue.Clear();

            foreach (VillageBuildLayout layout in settings.Layouts)
            {
                if (layout.Enabled && layout.Buildings.Count > 0)
                {
                    if (!_villageQueue.Contains(layout.VillageId))
                        _villageQueue.Add(layout.VillageId);
                }
            }

            LogDebug("Queue built: " + _villageQueue.Count + " village(s) with building layouts.");
        }

        private void ProcessVillage(int villageId, VillageBuilderSettings settings)
        {
            VillageBuildLayout layout = settings.GetLayout(villageId);
            if (layout == null || !layout.Enabled || layout.Buildings.Count == 0)
                return;

            VillageMap village = GameEngine.Instance.getVillage(villageId);
            if (village == null)
            {
                LogDebug("Village " + villageId + " not loaded, skipping.");
                return;
            }

            string villageName = GetVillageName(villageId);

            int queueMax = village.getMaxBuildingQueueLength();
            int queueCurrent = village.countNumBuildingsConstructing();

            if (queueCurrent >= queueMax)
            {
                LogDebug(villageName + ": Build queue full (" + queueCurrent + "/" + queueMax + ").");
                return;
            }

            for (int i = 0; i < layout.Buildings.Count; i++)
            {
                BuildingEntry entry = layout.Buildings[i];

                if (entry.Placed)
                    continue;

                // Check if building already exists at this location
                if (IsBuildingAtLocation(village, entry.BuildingType, entry.X, entry.Y))
                {
                    entry.Placed = true;
                    entry.Status = "Already built";
                    continue;
                }

                // Check if building is being constructed at this location
                if (IsBuildingConstructingAt(village, entry.BuildingType, entry.X, entry.Y))
                {
                    entry.Status = "Constructing";
                    continue;
                }

                // Check research
                try
                {
                    if (!VillageBuildingsData.isThisBuildingTypeAvailable(entry.BuildingType,
                        village.VillageMapType, GameEngine.Instance.World.UserResearchData))
                    {
                        entry.Status = "Not researched";
                        continue;
                    }
                }
                catch
                {
                    entry.Status = "Research check failed";
                    continue;
                }

                // Check if we can afford it
                if (!village.canAffordBuilding(entry.BuildingType))
                {
                    if (settings.WaitForResources)
                    {
                        entry.Status = "Waiting for resources";
                        LogDebug(villageName + ": Waiting for resources for " +
                            GetBuildingName(entry.BuildingType));
                        return; // Stop processing this village
                    }
                    entry.Status = "Cannot afford";
                    continue;
                }

                // Re-check queue space
                queueCurrent = village.countNumBuildingsConstructing();
                if (queueCurrent >= queueMax)
                {
                    LogDebug(villageName + ": Build queue full after placing buildings.");
                    return;
                }

                // Place the building
                string buildingName = GetBuildingName(entry.BuildingType);
                Point mapTile = new Point(entry.X, entry.Y);

                try
                {
                    RemoteServices.Instance.PlaceVillageBuilding(villageId, entry.BuildingType, mapTile);

                    entry.Status = "Placed";
                    LogInfo(villageName + ": Placed " + buildingName + " at (" + entry.X + ", " + entry.Y + ").");

                    // Only place one building per tick to avoid rate limiting
                    return;
                }
                catch (Exception ex)
                {
                    entry.Status = "Error: " + ex.Message;
                    LogError(villageName + ": Failed to place " + buildingName + ": " + ex.Message);
                    return;
                }
            }

            // All buildings processed
            bool allDone = true;
            for (int i = 0; i < layout.Buildings.Count; i++)
            {
                if (!layout.Buildings[i].Placed)
                {
                    allDone = false;
                    break;
                }
            }

            if (allDone)
            {
                LogInfo(villageName + ": All buildings in layout have been placed!");
                layout.Enabled = false;
            }
        }

        private bool IsBuildingAtLocation(VillageMap village, int buildingType, int x, int y)
        {
            try
            {
                foreach (VillageMapBuilding b in village.Buildings)
                {
                    if (b.buildingType == buildingType &&
                        b.buildingLocation.X == x &&
                        b.buildingLocation.Y == y &&
                        b.complete)
                        return true;
                }
            }
            catch { }
            return false;
        }

        private bool IsBuildingConstructingAt(VillageMap village, int buildingType, int x, int y)
        {
            try
            {
                foreach (VillageMapBuilding b in village.Buildings)
                {
                    if (b.buildingType == buildingType &&
                        b.buildingLocation.X == x &&
                        b.buildingLocation.Y == y &&
                        !b.complete)
                        return true;
                }
            }
            catch { }
            return false;
        }

        private static string GetVillageName(int villageId)
        {
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                return GameEngine.Instance.World.getVillageName(villageId);
            return "Village " + villageId;
        }

        public static string GetBuildingName(int buildingType)
        {
            try
            {
                return VillageBuildingsData.getBuildingName(buildingType);
            }
            catch
            {
                return "Building " + buildingType;
            }
        }

        // =====================================================================
        // Layout file import/export
        // =====================================================================

        public static List<BuildingEntry> ImportLayoutFromFile(string filePath)
        {
            List<BuildingEntry> buildings = new List<BuildingEntry>();
            if (!File.Exists(filePath))
                return buildings;

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.Contains(";"))
                    continue; // Skip header lines

                string[] parts = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3) continue;

                int buildingType, x, y;
                if (int.TryParse(parts[0], out buildingType) &&
                    int.TryParse(parts[1], out x) &&
                    int.TryParse(parts[2], out y))
                {
                    BuildingEntry entry = new BuildingEntry();
                    entry.BuildingType = buildingType;
                    entry.X = x;
                    entry.Y = y;
                    buildings.Add(entry);
                }
            }

            return buildings;
        }

        public static void ExportLayoutToFile(string filePath, VillageMap village)
        {
            List<string> lines = new List<string>();

            foreach (VillageMapBuilding b in village.Buildings)
            {
                if (b.buildingType != 0)
                {
                    lines.Add(b.buildingType + "," + b.buildingLocation.X + "," + b.buildingLocation.Y);
                }
            }

            File.WriteAllLines(filePath, lines.ToArray());
        }

        public static List<BuildingEntry> ImportFromVillage(VillageMap village)
        {
            List<BuildingEntry> buildings = new List<BuildingEntry>();

            foreach (VillageMapBuilding b in village.Buildings)
            {
                if (b.buildingType != 0)
                {
                    BuildingEntry entry = new BuildingEntry();
                    entry.BuildingType = b.buildingType;
                    entry.X = b.buildingLocation.X;
                    entry.Y = b.buildingLocation.Y;
                    buildings.Add(entry);
                }
            }

            return buildings;
        }
    }
}
