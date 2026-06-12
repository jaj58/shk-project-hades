using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class VillageBuilderModule : BotModuleBase
    {
        // =====================================================================
        // Build categories — type lists extracted from the game's own build menu
        // (VillageMapPanel placeBuilding tabs). Stockpile (2) and granary (3) are
        // NOT in any category: a village can't store resources without them, so
        // they form a hardcoded always-first tier above all category settings.
        // =====================================================================

        public class BuildCategory
        {
            public string Key;
            public string DisplayName;
            public int[] Types;

            public BuildCategory(string key, string displayName, int[] types)
            {
                Key = key;
                DisplayName = displayName;
                Types = types;
            }
        }

        public const int AlwaysFirstRank = -1;

        public static readonly int[] AlwaysFirstTypes = new int[] { 2, 3 }; // stockpile, granary

        public static readonly BuildCategory[] Categories = new BuildCategory[]
        {
            new BuildCategory("Food", "Food production", new int[]
                { 35, 13, 17, 16, 14, 15, 18, 12, 83, 84, 85, 86, 87, 88, 89 }),
            new BuildCategory("Hovels", "Hovels & popularity", new int[]
                { 1, 39, 40, 76, 77, 67, 68, 66, 69, 65 }),
            new BuildCategory("Industry", "Industry", new int[]
                { 6, 7, 8, 9, 79, 80, 81, 82 }),
            new BuildCategory("Markets", "Markets", new int[]
                { 78 }),
            new BuildCategory("Weapons", "Weapon production", new int[]
                { 29, 31, 28, 30, 32, 90, 91, 92, 93, 94 }),
            new BuildCategory("Banquet", "Banquet production", new int[]
                { 22, 21, 26, 19, 33, 23, 24, 25, 95, 96, 97, 98, 99, 100, 101, 102 }),
            new BuildCategory("Faith", "Faith production", new int[]
                { 34, 36, 37, 70, 71, 72, 73, 74, 75 }),
        };

        /// <summary>
        /// Returns the saved category priority normalized against the catalog:
        /// unknown keys dropped, missing categories appended (enabled) in default
        /// order. Safe to call with a null/empty saved list.
        /// </summary>
        public static List<BuilderCategoryPref> NormalizeCategoryPriority(List<BuilderCategoryPref> saved)
        {
            List<BuilderCategoryPref> result = new List<BuilderCategoryPref>();
            if (saved != null)
            {
                foreach (BuilderCategoryPref pref in saved)
                {
                    if (pref == null) continue;
                    bool known = false;
                    foreach (BuildCategory cat in Categories)
                        if (cat.Key == pref.Key) { known = true; break; }
                    bool duplicate = false;
                    foreach (BuilderCategoryPref existing in result)
                        if (existing.Key == pref.Key) { duplicate = true; break; }
                    if (known && !duplicate)
                        result.Add(pref);
                }
            }
            foreach (BuildCategory cat in Categories)
            {
                bool present = false;
                foreach (BuilderCategoryPref existing in result)
                    if (existing.Key == cat.Key) { present = true; break; }
                if (!present)
                {
                    BuilderCategoryPref pref = new BuilderCategoryPref();
                    pref.Key = cat.Key;
                    pref.Enabled = true;
                    result.Add(pref);
                }
            }
            return result;
        }

        /// <summary>
        /// Building type -> rank for the given settings. AlwaysFirstRank (-1) for
        /// stockpile/granary, 0..n-1 for enabled categories in priority order;
        /// types absent from the dictionary build last in layout order.
        /// </summary>
        public static Dictionary<int, int> BuildTypeRanks(VillageBuilderSettings settings)
        {
            Dictionary<int, int> ranks = new Dictionary<int, int>();
            foreach (int type in AlwaysFirstTypes)
                ranks[type] = AlwaysFirstRank;

            List<BuilderCategoryPref> prefs = NormalizeCategoryPriority(
                settings != null ? settings.CategoryPriority : null);
            int rank = 0;
            foreach (BuilderCategoryPref pref in prefs)
            {
                if (!pref.Enabled) continue;
                foreach (BuildCategory cat in Categories)
                {
                    if (cat.Key == pref.Key)
                    {
                        foreach (int type in cat.Types)
                            if (!ranks.ContainsKey(type))
                                ranks[type] = rank;
                        break;
                    }
                }
                rank++;
            }
            return ranks;
        }

        private Dictionary<int, int> _typeRank = new Dictionary<int, int>();

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
            _typeRank = BuildTypeRanks(settings); // pick up priority changes each cycle

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

            List<BuildingEntry> ordered = GetOrderedEntries(layout);
            for (int i = 0; i < ordered.Count; i++)
            {
                BuildingEntry entry = ordered[i];

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

        /// <summary>
        /// Processing order for a layout: always-first tier (stockpile/granary),
        /// then enabled categories by priority rank, then everything else — stable
        /// within each tier (original layout order). Does not mutate the stored list.
        /// </summary>
        private List<BuildingEntry> GetOrderedEntries(VillageBuildLayout layout)
        {
            List<BuildingEntry> ordered = new List<BuildingEntry>(layout.Buildings.Count);
            ordered.AddRange(layout.Buildings);
            // Indices snapshot for a stable sort (List<T>.Sort is unstable).
            Dictionary<BuildingEntry, int> originalIndex = new Dictionary<BuildingEntry, int>();
            for (int i = 0; i < layout.Buildings.Count; i++)
                originalIndex[layout.Buildings[i]] = i;

            ordered.Sort(delegate (BuildingEntry a, BuildingEntry b)
            {
                int rankA, rankB;
                if (!_typeRank.TryGetValue(a.BuildingType, out rankA)) rankA = int.MaxValue;
                if (!_typeRank.TryGetValue(b.BuildingType, out rankB)) rankB = int.MaxValue;
                if (rankA != rankB) return rankA.CompareTo(rankB);
                return originalIndex[a].CompareTo(originalIndex[b]);
            });
            return ordered;
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
