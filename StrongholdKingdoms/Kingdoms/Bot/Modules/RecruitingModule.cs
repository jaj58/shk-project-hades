using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class RecruitingModule : BotModuleBase
    {
        public static readonly string[] AllUnitKeys = new string[]
        {
            "Peasants",
            "Archers",
            "Pikemen",
            "Swordsmen",
            "Catapults",
            "Captains",
            "Scouts",
            "Monks",
            "Traders"
        };

        public static readonly string[] DisbandOptions = new string[]
        {
            "Peasants",
            "Traders",
            "Scouts",
            "Monks"
        };

        public static int GetTroopTypeId(string unitKey)
        {
            switch (unitKey)
            {
                case "Peasants": return 70;
                case "Archers": return 72;
                case "Pikemen": return 73;
                case "Swordsmen": return 71;
                case "Catapults": return 74;
                case "Captains": return 100;
                case "Scouts": return 76;
                case "Traders": return -5;
                default: return -1;
            }
        }

        public static bool IsPeopleType(string unitKey)
        {
            return unitKey == "Monks";
        }

        public static int GetPeopleTypeId(string unitKey)
        {
            if (unitKey == "Monks") return 4;
            return -1;
        }

        public static int GetDisbandTypeId(string unitKey)
        {
            switch (unitKey)
            {
                case "Peasants": return 70;
                case "Archers": return 72;
                case "Pikemen": return 73;
                case "Swordsmen": return 71;
                case "Catapults": return 74;
                case "Captains": return 100;
                case "Scouts": return 4;
                case "Monks": return 1;
                case "Traders": return 2;
                default: return -1;
            }
        }

        public static bool IsDisbandPeople(string unitKey)
        {
            return unitKey == "Monks" || unitKey == "Traders" || unitKey == "Scouts";
        }

        public static int GetCurrentCount(VillageMap village, string unitKey)
        {
            if (village == null) return 0;
            switch (unitKey)
            {
                case "Peasants": return village.m_numPeasants;
                case "Archers": return village.m_numArchers;
                case "Pikemen": return village.m_numPikemen;
                case "Swordsmen": return village.m_numSwordsmen;
                case "Catapults": return village.m_numCatapults;
                case "Captains": return village.m_numCaptains;
                case "Scouts": return village.m_numScouts;
                case "Monks": return village.calcTotalMonksAtHome();
                case "Traders": return village.calcTotalTradersAtHome();
                default: return 0;
            }
        }

        public static int GetUnitSize(string unitKey)
        {
            if (GameEngine.Instance == null) return 1;
            WorldData wd = GameEngine.Instance.LocalWorldData;
            if (wd == null) return 1;
            switch (unitKey)
            {
                case "Monks": return wd.UnitSize_Priests;
                case "Traders": return wd.UnitSize_Trader;
                case "Scouts": return wd.UnitSize_Scout;
                default: return 1;
            }
        }

        public static int GetSpareUnitSpace(VillageMap village)
        {
            if (village == null || GameEngine.Instance == null) return 0;
            int capacity = GameEngine.Instance.LocalWorldData.Village_UnitCapacity;
            int used = village.calcUnitUsages();
            return Math.Max(capacity - used, 0);
        }

        public static int GetSparePeasants(VillageMap village)
        {
            if (village == null) return 0;
            return Math.Max(village.m_spareWorkers, 0);
        }

        private DateTime _lastFullCycle = DateTime.MinValue;
        private int _currentVillageIndex;
        private List<int> _villageQueue = new List<int>();
        private DateTime _lastVillageAction = DateTime.MinValue;

        public override string ModuleName
        {
            get { return "Recruiting"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(3); }
        }

        private RecruitingSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.Recruiting;
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
            RecruitingSettings settings = Settings;
            if (settings == null) return;

            if (_villageQueue.Count == 0 ||
                _currentVillageIndex >= _villageQueue.Count)
            {
                if ((DateTime.Now - _lastFullCycle).TotalSeconds < settings.CycleIntervalSeconds)
                    return;

                RefreshVillageQueue();
                _currentVillageIndex = 0;
                _lastFullCycle = DateTime.Now;

                if (_villageQueue.Count == 0)
                {
                    LogDebug("No villages to recruit in.");
                    return;
                }
            }

            if ((DateTime.Now - _lastVillageAction).TotalMilliseconds < settings.DelayBetweenVillagesMs)
                return;

            int villageId = _villageQueue[_currentVillageIndex];
            _currentVillageIndex++;
            _lastVillageAction = DateTime.Now;

            ProcessVillage(villageId, settings);
        }

        private void RefreshVillageQueue()
        {
            _villageQueue.Clear();
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            foreach (WorldMap.UserVillageData uvd in villages)
            {
                int id = uvd.villageID;
                RecruitingSettings settings = Settings;
                if (settings != null && settings.IsVillageEnabled(id))
                    _villageQueue.Add(id);
            }
        }

        private void ProcessVillage(int villageId, RecruitingSettings settings)
        {
            VillageMap village = GameEngine.Instance.getVillage(villageId);
            if (village == null)
            {
                LogDebug("Village " + villageId + " not loaded, skipping.");
                return;
            }

            VillageRecruitSettings villageSetting = settings.GetVillageSettings(villageId);
            if (villageSetting == null) return;

            int sparePeasants = GetSparePeasants(village);
            int spareUnitSpace = GetSpareUnitSpace(village);

            if (sparePeasants <= 0)
            {
                LogDebug("Village " + villageId + " has no spare peasants, skipping.");
                return;
            }

            if (spareUnitSpace <= 0)
            {
                LogDebug("Village " + villageId + " has no spare unit space, skipping.");
                return;
            }

            List<UnitRecruitEntry> sorted = new List<UnitRecruitEntry>(villageSetting.Units);
            sorted.Sort(delegate(UnitRecruitEntry a, UnitRecruitEntry b)
            {
                return a.Priority.CompareTo(b.Priority);
            });

            foreach (UnitRecruitEntry entry in sorted)
            {
                if (entry.TargetCount <= 0) continue;

                int current = GetCurrentCount(village, entry.UnitKey);
                int needed = entry.TargetCount - current;
                if (needed <= 0) continue;

                int unitSize = GetUnitSize(entry.UnitKey);
                int maxBySpace = spareUnitSpace / unitSize;
                int maxByPeasants = sparePeasants;
                int canRecruit = Math.Min(needed, Math.Min(maxBySpace, maxByPeasants));

                if (canRecruit <= 0)
                {
                    LogDebug("Village " + villageId + ": want " + needed + " " + entry.UnitKey +
                             " but limited (space=" + maxBySpace + ", peasants=" + maxByPeasants + ")");
                    continue;
                }

                try
                {
                    if (IsPeopleType(entry.UnitKey))
                    {
                        int monkAmount = Math.Min(canRecruit, 4);
                        village.makePeople(1000 + monkAmount);
                        LogInfo("Recruiting " + monkAmount + " " + entry.UnitKey + " in village " + villageId +
                                " (have " + current + "/" + entry.TargetCount +
                                ", space=" + spareUnitSpace + ", peasants=" + sparePeasants + ")");
                    }
                    else if (entry.UnitKey == "Traders")
                    {
                        int traderAmount = Math.Min(canRecruit, 4);
                        village.makeTroops(-5, traderAmount, true);
                        LogInfo("Recruiting " + traderAmount + " " + entry.UnitKey + " in village " + villageId +
                                " (have " + current + "/" + entry.TargetCount +
                                ", space=" + spareUnitSpace + ", peasants=" + sparePeasants + ")");
                    }
                    else
                    {
                        int troopType = GetTroopTypeId(entry.UnitKey);
                        village.makeTroops(troopType, canRecruit, true);
                        LogInfo("Recruiting " + canRecruit + " " + entry.UnitKey + " in village " + villageId +
                                " (have " + current + "/" + entry.TargetCount +
                                ", space=" + spareUnitSpace + ", peasants=" + sparePeasants + ")");
                    }
                }
                catch (Exception ex)
                {
                    LogError("Failed to recruit " + entry.UnitKey + " in village " + villageId + ": " + ex.Message);
                }

                break;
            }
        }

        public void DisbandAll(string unitKey)
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null)
                return;

            List<WorldMap.UserVillageData> villages = GameEngine.Instance.World.getUserVillageList();
            if (villages == null) return;

            foreach (WorldMap.UserVillageData uvd in villages)
            {
                int id = uvd.villageID;
                VillageMap village = GameEngine.Instance.getVillage(id);
                if (village == null) continue;

                int count = GetCurrentCount(village, unitKey);
                if (count <= 0) continue;

                try
                {
                    if (IsDisbandPeople(unitKey))
                    {
                        int disbandType = GetDisbandTypeId(unitKey);
                        village.disbandPeople(disbandType, count);
                    }
                    else
                    {
                        int disbandType = GetDisbandTypeId(unitKey);
                        village.disbandTroops(disbandType, count);
                    }
                    LogInfo("Disbanded " + count + " " + unitKey + " in village " + id);
                }
                catch (Exception ex)
                {
                    LogError("Failed to disband " + unitKey + " in village " + id + ": " + ex.Message);
                }
            }
        }

        protected override void OnShutdown()
        {
            _villageQueue.Clear();
            _currentVillageIndex = 0;
        }
    }
}
