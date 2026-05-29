using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    public class ScoutModule : BotModuleBase
    {
        private DateTime _lastSendTime = DateTime.MinValue;
        private DateTime _lastCycleTime = DateTime.MinValue;
        private bool _cycleComplete = true;

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
            _lastCycleTime = DateTime.MinValue;
            _cycleComplete = true;
        }

        protected override void OnTick()
        {
            ScoutSettings settings = Settings;
            if (settings == null) return;

            // Respect the delay between individual scout sends (within-cycle pacing)
            if ((DateTime.Now - _lastSendTime).TotalMilliseconds < settings.DelayBetweenSendsMs)
                return;

            // Once the current cycle drains to nothing, wait the full interval before starting again
            if (_cycleComplete && (DateTime.Now - _lastCycleTime).TotalSeconds < settings.CycleIntervalSeconds)
                return;

            List<WorldMap.UserVillageData> villages;
            try
            {
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
                villages = GameEngine.Instance.World.getUserVillageList();
            }
            catch { return; }

            if (villages == null) return;

            // Shuffle so we don't always favour the same village
            List<WorldMap.UserVillageData> shuffled = new List<WorldMap.UserVillageData>(villages);
            Random rng = new Random();
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                WorldMap.UserVillageData tmp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = tmp;
            }

            bool sentAnything = false;
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

                List<StashTarget> targets = GetStashTargets(uvd.villageID, vs,
                    settings.MaxScoutTimeSeconds, settings.Priority);

                foreach (StashTarget target in targets)
                {
                    if (village.m_numScouts <= 0) break;
                    if (SendScout(village, target, settings.SendOneScout, settings.SendOneOnNewStash))
                    {
                        // One send per tick — come back next tick for the next target
                        _lastSendTime = DateTime.Now;
                        _cycleComplete = false;
                        return;
                    }
                }
            }

            // Iterated everything without finding a target to send — cycle is done
            if (!sentAnything)
            {
                _cycleComplete = true;
                _lastCycleTime = DateTime.Now;
                LogDebug("Scout cycle complete. Next cycle in " + settings.CycleIntervalSeconds + "s.");
            }
        }

        private List<StashTarget> GetStashTargets(int villageId, VillageScoutSettings vs,
            int maxTimeSec, ScoutPriority priority)
        {
            List<StashTarget> result = new List<StashTarget>();

            VillageData[] worldVillages;
            try { worldVillages = GameEngine.Instance.World.getWorldVillageList(); }
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

                int resourceLevel;
                if (special == 100)
                {
                    // New/undiscovered stash — type and size unknown, no map data to fetch
                    resourceLevel = -1;
                }
                else
                {
                    // Known resource stash — level is visible on the map
                    try
                    {
                        WorldMap.SpecialVillageCache svc =
                            GameEngine.Instance.World.getSpecialVillageData(vd.id, false);
                        if (svc == null) continue; // map data not loaded yet, skip this cycle
                        resourceLevel = svc.resourceLevel;
                    }
                    catch { continue; }
                }

                result.Add(new StashTarget
                {
                    StashId    = vd.id,
                    TravelTime = travelTime,
                    ResourceType = special,
                    TypeIndex  = typeIndex,
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

        // mirrors reference smethod_1: resources one scout can carry from this stash type
        private static int CalculateCarryPerScout(int stashType)
        {
            int resourceCarryLevel = GameEngine.Instance.LocalWorldData.ScoutResourceCarryLevel;
            int researchForaging = (int)GameEngine.Instance.World.UserResearchData.Research_Foraging;
            int carry = CardTypes.adjustForagingLevel(GameEngine.Instance.cardsManager.UserCardData,
                            resourceCarryLevel)
                        * ResearchData.foragingResearch[researchForaging] / 2;

            // Rare resource types (weapons, luxury goods) carry 1/10 the base amount
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

        private static int CalculateOptimalScouts(int resourceLevel, int carryPerScout)
        {
            if (resourceLevel <= 0) return 1;
            int needed = resourceLevel / carryPerScout;
            if (resourceLevel % carryPerScout > 0) needed++;
            return Math.Max(1, needed);
        }

        // Count scouts from any user village already en route to this stash (outbound only)
        private static int GetScoutsEnRoute(int stashId)
        {
            int total = 0;
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    foreach (object obj in GameEngine.Instance.World.getArmyArray())
                    {
                        WorldMap.LocalArmyData army = (WorldMap.LocalArmyData)obj;
                        if (army.numScouts > 0
                            && army.lootType < 0   // outbound, not returning with loot
                            && army.targetVillageID == stashId
                            && GameEngine.Instance.World.isUserVillage(army.travelFromVillageID))
                        {
                            total += army.numScouts;
                        }
                    }
                    break;
                }
                catch (InvalidOperationException)
                {
                    total = 0;
                    attempts++;
                }
            }
            return total;
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

                int unitUsed = village.calcUnitUsages()
                    + village.LocallyMade_Scouts * GameEngine.Instance.LocalWorldData.UnitSize_Scout;
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
                LogDebug(string.Format("{0}: hiring {1} scout(s)",
                    GameEngine.Instance.World.getVillageName(village.VillageID), amount));
                village.makeTroops(76, amount, false);
            }
            catch (Exception ex)
            {
                LogError("TryHireScouts: " + ex.Message);
            }
        }

        private bool SendScout(VillageMap village, StashTarget target, bool sendOne, bool sendOneOnNew)
        {
            int enRoute = GetScoutsEnRoute(target.StashId);

            int count;

            if (target.ResourceType == 100)
            {
                // New/undiscovered stash — size unknown until scouted
                if (enRoute > 0) return false; // already being discovered
                count = sendOneOnNew ? 1 : village.m_numScouts;
            }
            else if (sendOne)
            {
                // Single-scout mode: drip-feed 1 scout at a time until stash is fully covered.
                // Keep sending as long as en-route scouts won't carry everything.
                if (target.ResourceLevel > 0)
                {
                    try
                    {
                        int carryPerScout = CalculateCarryPerScout(target.ResourceType);
                        if (enRoute * carryPerScout >= target.ResourceLevel)
                            return false; // en-route scouts already cover the full stash
                    }
                    catch { }
                }
                count = 1;
            }
            else
            {
                // Optimal mode: send exactly enough scouts to clear the remaining resources
                try
                {
                    int carryPerScout = CalculateCarryPerScout(target.ResourceType);
                    int remaining = target.ResourceLevel - enRoute * carryPerScout;
                    if (remaining <= 0) return false; // already fully covered by en-route scouts
                    count = CalculateOptimalScouts(remaining, carryPerScout);
                }
                catch
                {
                    return false;
                }
                count = Math.Min(count, village.m_numScouts);
            }

            if (count <= 0) return false;

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
            public int ResourceLevel; // -1 for type 100 (unknown); >0 for known resource stashes
        }
    }
}
