using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class ScoutModule : BotModuleBase
    {
        private DateTime _lastSendTime = DateTime.MinValue;

        public override string ModuleName
        {
            get { return "Scout"; }
        }

        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(3); }
        }

        private ScoutSettings Settings
        {
            get
            {
                if (Engine != null && Engine.Settings != null)
                    return Engine.Settings.Scout;
                return null;
            }
        }

        protected override void OnInitialize()
        {
            _lastSendTime = DateTime.MinValue;
        }

        protected override void OnTick()
        {
            ScoutSettings settings = Settings;
            if (settings == null) return;

            if ((DateTime.Now - _lastSendTime).TotalMilliseconds < settings.DelayBetweenSendsMs)
                return;

            List<WorldMap.UserVillageData> villages;
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
                villages = GameEngine.Instance.World.getUserVillageList();
            }
            catch { return; }

            if (villages == null) return;

            List<WorldMap.UserVillageData> shuffled = new List<WorldMap.UserVillageData>(villages);
            Random rng = new Random();
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                WorldMap.UserVillageData tmp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = tmp;
            }

            foreach (WorldMap.UserVillageData uvd in shuffled)
            {
                VillageScoutSettings vs = settings.GetVillageSettings(uvd.villageID);
                if (!vs.ScoutingEnabled) continue;

                VillageMap village;
                try { village = GameEngine.Instance.getVillage(uvd.villageID); }
                catch { continue; }
                if (village == null) continue;

                if (settings.AutoHireScouts > 0)
                    TryHireScouts(village, settings.AutoHireScouts);

                if (village.m_numScouts <= 0) continue;

                List<StashTarget> targets = GetStashTargets(uvd.villageID, vs, settings.MaxScoutTimeSeconds, settings.Priority);
                foreach (StashTarget target in targets)
                {
                    if (village.m_numScouts <= 0) break;
                    if (SendScout(village, target, settings.SendOneScout))
                    {
                        _lastSendTime = DateTime.Now;
                        return;
                    }
                }
            }
        }

        private List<StashTarget> GetStashTargets(int villageId, VillageScoutSettings vs, int maxTimeSec, ScoutPriority priority)
        {
            List<StashTarget> result = new List<StashTarget>();

            VillageData[] worldVillages;
            try
            {
                worldVillages = GameEngine.Instance.World.getWorldVillageList();
            }
            catch { return result; }

            if (worldVillages == null) return result;

            for (int i = 0; i < worldVillages.Length; i++)
            {
                VillageData vd = worldVillages[i];
                int special = vd.special;
                if (special < 100 || special > 133) continue;
                if (vs.ResourceTypesToIgnore.Contains(special)) continue;
                int typeIndex = vs.ResourceTypesToScout.IndexOf(special);
                if (typeIndex < 0) continue;

                double travelTime;
                try { travelTime = CalculateTravelTime(villageId, vd.id); }
                catch { continue; }

                if (travelTime > maxTimeSec) continue;

                // Try to get cached resource level — no download, avoids blocking
                int resourceLevel = -1;
                try
                {
                    WorldMap.SpecialVillageCache svc = GameEngine.Instance.World.getSpecialVillageData(vd.id, false);
                    if (svc != null)
                        resourceLevel = svc.resourceLevel;
                    else
                        GameEngine.Instance.World.getSpecialVillageData(vd.id, true); // trigger async fetch for next cycle
                }
                catch { }

                result.Add(new StashTarget
                {
                    StashId = vd.id,
                    TravelTime = travelTime,
                    ResourceType = special,
                    TypeIndex = typeIndex,
                    ResourceLevel = resourceLevel
                });
            }

            if (priority == ScoutPriority.ResourcePriority)
            {
                result.Sort((a, b) =>
                {
                    int c = a.TypeIndex.CompareTo(b.TypeIndex);
                    return c != 0 ? c : a.TravelTime.CompareTo(b.TravelTime);
                });
            }
            else
            {
                result.Sort((a, b) => a.TravelTime.CompareTo(b.TravelTime));
            }

            return result;
        }

        private static double CalculateTravelTime(int fromVillageId, int toVillageId)
        {
            double dist = GameEngine.Instance.World.getDistance(fromVillageId, toVillageId);
            WorldData wd = GameEngine.Instance.LocalWorldData;
            double t = dist * (wd.ScoutsMoveSpeed * wd.gamePlaySpeed
                       * ResearchData.ScoutTimes[(int)GameEngine.Instance.World.UserResearchData.Research_Horsemanship]);
            t = GameEngine.Instance.World.adjustIfIslandTravel(t, fromVillageId, toVillageId);
            t *= CardTypes.getScoutSpeed(GameEngine.Instance.cardsManager.UserCardData);
            return t;
        }

        // Mirrors reference smethod_1: how many resources one scout can carry from this stash type
        private static int CalculateCarryPerScout(int stashType)
        {
            int resourceCarryLevel = GameEngine.Instance.LocalWorldData.ScoutResourceCarryLevel;
            int researchForaging = (int)GameEngine.Instance.World.UserResearchData.Research_Foraging;
            int carry = CardTypes.adjustForagingLevel(GameEngine.Instance.cardsManager.UserCardData, resourceCarryLevel)
                        * ResearchData.foragingResearch[researchForaging] / 2;

            // Rare types carry 1/10 of base amount
            switch (stashType)
            {
                case 119: case 121: case 122: case 123: case 124:
                case 125: case 126: case 128: case 129: case 130:
                case 131: case 132: case 133:
                    carry /= 10;
                    break;
            }
            return Math.Max(1, carry);
        }

        // Minimum scouts needed to fully clear a stash of the given resource level
        private static int CalculateOptimalScouts(int resourceLevel, int carryPerScout)
        {
            if (resourceLevel <= 0) return 1;
            int needed = resourceLevel / carryPerScout;
            if (resourceLevel % carryPerScout > 0) needed++;
            return Math.Max(1, needed);
        }

        private void TryHireScouts(VillageMap village, int targetCount)
        {
            try
            {
                byte researchScouts = GameEngine.Instance.World.UserResearchData.Research_Scouts;
                int effectiveTarget = Math.Min(targetCount, (int)researchScouts);
                int current = village.calcTotalScouts() + village.LocallyMade_Scouts;
                if (current >= effectiveTarget) return;

                int toHire = effectiveTarget - current;

                int spareWorkers = village.m_spareWorkers;
                if (toHire > spareWorkers) toHire = spareWorkers;
                if (toHire <= 0) return;

                int unitUsed = village.calcUnitUsages() + village.LocallyMade_Scouts * GameEngine.Instance.LocalWorldData.UnitSize_Scout;
                int unitSpace = GameEngine.Instance.LocalWorldData.Village_UnitCapacity - unitUsed;
                int unitSizeScout = GameEngine.Instance.LocalWorldData.UnitSize_Scout;
                if (unitSpace < toHire * unitSizeScout)
                    toHire = unitSpace / unitSizeScout;
                if (toHire <= 0) return;

                int gold = (int)GameEngine.Instance.World.getCurrentGold();
                int goldCost = GameEngine.Instance.LocalWorldData.ScoutGoldCost;
                if (gold < toHire * goldCost)
                    toHire = gold / goldCost;
                if (toHire <= 0) return;

                int amount = Math.Min(toHire, 4);
                LogDebug(string.Format("{0}: hiring {1} scout(s)", GameEngine.Instance.World.getVillageName(village.VillageID), amount));
                village.makeTroops(76, amount, false);
            }
            catch (Exception ex)
            {
                LogError("TryHireScouts: " + ex.Message);
            }
        }

        private bool SendScout(VillageMap village, StashTarget target, bool sendOne)
        {
            int count;
            if (sendOne || target.ResourceLevel < 0)
            {
                // Either forced single-scout mode, or stash size unknown (data not yet cached)
                count = 1;
            }
            else
            {
                try
                {
                    int carryPerScout = CalculateCarryPerScout(target.ResourceType);
                    count = CalculateOptimalScouts(target.ResourceLevel, carryPerScout);
                }
                catch
                {
                    count = 1;
                }
                count = Math.Min(count, village.m_numScouts);
            }

            try
            {
                RemoteServices.Instance.set_SendScouts_UserCallBack(OnSendScoutsCallback);
                RemoteServices.Instance.SendScouts(village.VillageID, target.StashId, count);
                village.addTroops(0, 0, 0, 0, 0, -count);
                AllVillagesPanel.travellersChanged();
                LogDebug(string.Format("{0}: sent {1} scout(s) to {2} ({3})",
                    GameEngine.Instance.World.getVillageName(village.VillageID),
                    count, target.StashId, GetStashDisplayName(target.StashId)));
                return true;
            }
            catch (Exception ex)
            {
                LogError("SendScout error: " + ex.Message);
                return false;
            }
        }

        private void OnSendScoutsCallback(SendScouts_ReturnType ret)
        {
            try
            {
                if (ret.Success)
                {
                    ArmyReturnData[] data = new ArmyReturnData[] { ret.armyData };
                    GameEngine.Instance.World.doGetArmyData(
                        (IEnumerable<ArmyReturnData>)data,
                        (IEnumerable<ArmyReturnData>)null, false);
                    GameEngine.Instance.World.addExistingArmy(ret.armyData.armyID);
                    if (ret.cardData != null)
                        GameEngine.Instance.cardsManager.UserCardData = ret.cardData;
                }
                else if (ret.m_errorCode == ErrorCodes.ErrorCode.ATTACKING_VILLAGE_INTERDICT_PROTECTED)
                {
                    LogWarning("Cannot scout from interdicted village: " + ret.sourceVillage);
                }

                if (ret.numScoutsNotTaken > 0)
                {
                    VillageMap v = GameEngine.Instance.getVillage(ret.sourceVillage);
                    if (v != null) v.addTroops(0, 0, 0, 0, 0, ret.numScoutsNotTaken);
                }
            }
            catch (Exception ex)
            {
                LogError("SendScouts callback error: " + ex.Message);
            }
        }

        private static string GetStashDisplayName(int stashId)
        {
            try
            {
                int special = GameEngine.Instance.World.getSpecial(stashId);
                if (special == 100) return "New Stash";
                if (special > 100 && special <= 133)
                    return VillageBuildingsData.getResourceNames(special - 100);
            }
            catch { }
            return "Stash " + stashId;
        }

        private struct StashTarget
        {
            public int StashId;
            public double TravelTime;
            public int ResourceType;
            public int TypeIndex;
            public int ResourceLevel; // -1 = not yet cached from server
        }
    }
}
