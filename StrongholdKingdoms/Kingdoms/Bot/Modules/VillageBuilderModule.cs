using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class VillageBuilderModule : BotModuleBase
    {
        private DateTime _lastCycleEnd = DateTime.MinValue;
        private bool _cycleComplete = true;
        private int _currentVillageIndex;
        private List<int> _villageQueue = new List<int>();
        private DateTime _lastVillageAction = DateTime.MinValue;
        private readonly HashSet<int> _completeLayoutsLogged = new HashSet<int>();

        // In-flight placement awaiting its server response. The RemoteServices user
        // callback slot is global, so at most one bot placement is in flight at a time.
        private bool _placementPending;
        private int _pendingVillageId;
        private int _pendingBuildingType;
        private int _pendingX;
        private int _pendingY;
        private BuildingEntry _pendingEntry;
        private DateTime _pendingSentAt;

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
            _cycleComplete = true;
            _placementPending = false;
            _pendingEntry = null;
        }

        protected override void OnTick()
        {
            VillageBuilderSettings settings = Settings;
            if (settings == null) return;

            // Expire a placement whose response never arrived so the cycle can't wedge.
            if (_placementPending)
            {
                if ((DateTime.Now - _pendingSentAt).TotalSeconds < 30)
                    return;
                if (_pendingEntry != null)
                    _pendingEntry.Status = "No response (timed out)";
                LogWarning("Placement response timed out for village " + _pendingVillageId + ".");
                _placementPending = false;
                _pendingEntry = null;
            }

            if (_cycleComplete)
            {
                if ((DateTime.Now - _lastCycleEnd).TotalSeconds < settings.CycleIntervalSeconds)
                    return;

                BuildVillageQueue(settings);
                _currentVillageIndex = 0;
                _cycleComplete = false;

                if (_villageQueue.Count == 0)
                {
                    LogDebug("No villages with building layouts enabled.");
                    _cycleComplete = true;
                    _lastCycleEnd = DateTime.Now;
                    return;
                }
            }

            // Process the whole queue before starting the cycle-interval wait, so
            // villages late in the list are never starved by the cycle timer.
            if (_currentVillageIndex >= _villageQueue.Count)
            {
                _cycleComplete = true;
                _lastCycleEnd = DateTime.Now;
                return;
            }

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
                    // Clear Placed flags each cycle so demolished buildings are re-evaluated
                    foreach (BuildingEntry entry in layout.Buildings)
                        entry.Placed = false;

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

                // Validate tile — terrain, overlap, and building count limits
                Point mapTile = new Point(entry.X, entry.Y);
                int tileError = village.BotCheckPlacementTile(entry.BuildingType, mapTile);
                if (tileError == 1)
                {
                    entry.Status = "Invalid tile (terrain/overlap)";
                    LogDebug(villageName + ": " + GetBuildingName(entry.BuildingType) +
                        " at (" + entry.X + "," + entry.Y + ") — terrain or overlap error, skipping.");
                    continue;
                }
                if (tileError == 2)
                {
                    entry.Status = "Count limit reached";
                    LogDebug(villageName + ": " + GetBuildingName(entry.BuildingType) + " — count limit reached.");
                    entry.Placed = true; // no point retrying this cycle
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

                // Place the building. Mirror the client flow: register the response
                // callback on the target village, optimistically add a pending
                // building (keeps queue count and occupancy map honest in-flight),
                // then send. The entry is only marked Placed when the server confirms
                // via OnPlacementResult — dispatch is not success.
                string buildingName = GetBuildingName(entry.BuildingType);

                try
                {
                    RemoteServices.Instance.set_PlaceVillageBuilding_UserCallBack(
                        new RemoteServices.PlaceVillageBuilding_UserCallBack(village.botBuildingPlacedCallback));
                    village.BotAddPendingBuilding(entry.BuildingType, mapTile);
                    RemoteServices.Instance.PlaceVillageBuilding(villageId, entry.BuildingType, mapTile);

                    _placementPending = true;
                    _pendingVillageId = villageId;
                    _pendingBuildingType = entry.BuildingType;
                    _pendingX = entry.X;
                    _pendingY = entry.Y;
                    _pendingEntry = entry;
                    _pendingSentAt = DateTime.Now;

                    entry.Status = "Placing...";
                    LogInfo(villageName + ": Placing " + buildingName + " at (" + entry.X + ", " + entry.Y + ")...");

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

            // Nothing left to place this pass. The layout stays enabled (maintain
            // mode): Placed flags are cleared every cycle, so demolished buildings
            // are detected and rebuilt. Log completion only on the transition.
            bool allDone = true;
            int atCountLimit = 0;
            for (int i = 0; i < layout.Buildings.Count; i++)
            {
                BuildingEntry e = layout.Buildings[i];
                if (!e.Placed)
                {
                    allDone = false;
                    break;
                }
                if (e.Status == "Count limit reached")
                    atCountLimit++;
            }

            if (allDone)
            {
                if (!_completeLayoutsLogged.Contains(villageId))
                {
                    _completeLayoutsLogged.Add(villageId);
                    LogInfo(villageName + ": Layout complete (" + (layout.Buildings.Count - atCountLimit) +
                        " built, " + atCountLimit + " at count limit). Maintaining.");
                }
            }
            else
            {
                _completeLayoutsLogged.Remove(villageId);
            }
        }

        // Called from VillageMap.botBuildingPlacedCallback on the main thread once the
        // server has answered the placement request sent above.
        public void OnPlacementResult(PlaceVillageBuilding_ReturnType returnData)
        {
            if (!_placementPending || returnData == null)
                return;

            Point loc = (Point)returnData.buildingLocation;
            if (returnData.villageID != _pendingVillageId ||
                returnData.buildingType != _pendingBuildingType ||
                loc.X != _pendingX || loc.Y != _pendingY)
                return; // response to a manual placement, not ours

            BuildingEntry entry = _pendingEntry;
            _placementPending = false;
            _pendingEntry = null;

            string villageName = GetVillageName(returnData.villageID);
            string buildingName = GetBuildingName(returnData.buildingType);

            if (returnData.Success)
            {
                if (entry != null)
                {
                    entry.Placed = true;
                    entry.Status = "Queued";
                }
                LogInfo(villageName + ": Placed " + buildingName + " at (" + loc.X + ", " + loc.Y + ").");
            }
            else
            {
                if (entry != null)
                    entry.Status = "Rejected: " + returnData.m_errorCode;
                LogWarning(villageName + ": Server rejected " + buildingName + " at (" + loc.X + ", " + loc.Y + "): " +
                    returnData.m_errorCode);

                if (returnData.m_errorCode == ErrorCodes.ErrorCode.VILLAGE_BUILDINGS_NO_LONGER_OWNER)
                {
                    VillageBuilderSettings settings = Settings;
                    VillageBuildLayout layout = settings != null ? settings.GetLayout(returnData.villageID) : null;
                    if (layout != null)
                    {
                        layout.Enabled = false;
                        LogWarning(villageName + ": No longer owner — layout disabled.");
                    }
                }
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
            int terrainType;
            return ImportLayoutFromFile(filePath, out terrainType);
        }

        // terrainType is parsed from the old-format header line "selected;terrainType;name"
        // when present, otherwise -1 (file gives no terrain information).
        public static List<BuildingEntry> ImportLayoutFromFile(string filePath, out int terrainType)
        {
            terrainType = -1;
            List<BuildingEntry> buildings = new List<BuildingEntry>();
            if (!File.Exists(filePath))
                return buildings;

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                if (line.Contains(";"))
                {
                    string[] headerParts = line.Split(';');
                    int parsed;
                    if (terrainType == -1 && headerParts.Length >= 2 && int.TryParse(headerParts[1], out parsed))
                        terrainType = parsed;
                    continue;
                }

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

            // Old-format header carries the terrain type so imports can be validated.
            // Both importers (this bot and the old one) skip ';' lines for buildings.
            int terrainType = 0;
            string villageName = "";
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
            {
                terrainType = GameEngine.Instance.World.getVillageTerrainType(village.VillageID);
                villageName = GameEngine.Instance.World.getVillageName(village.VillageID);
            }
            lines.Add("1;" + terrainType + ";" + villageName);

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
