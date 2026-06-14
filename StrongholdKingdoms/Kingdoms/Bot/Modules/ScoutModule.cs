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
        private readonly Random _rng = new Random();

        // Sentinel ResourceLevel values on StashTarget:
        private const int LEVEL_NEW_STASH   = -1; // type 100 — unknown type/size, no fetch possible
        private const int LEVEL_NEEDS_FETCH = -2; // known type but size not yet in the special-village cache

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

            // Respect the delay between individual scout actions (send / fetch / cancel)
            if ((DateTime.Now - _lastSendTime).TotalMilliseconds < settings.DelayBetweenSendsMs)
                return;

            // Once the current cycle drains to nothing, wait the full interval before starting again
            if (_cycleComplete && (DateTime.Now - _lastCycleTime).TotalSeconds < settings.CycleIntervalSeconds)
                return;

            // Recall scouts stranded on a despawned stash — at most one cancel per tick.
            if (TryCancelStrandedScout())
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
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
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

                if (GetAvailableScouts(village) <= 0) continue;

                List<StashTarget> targets = GetStashTargets(uvd.villageID, vs,
                    settings.MaxScoutTimeSeconds, settings.Priority);

                foreach (StashTarget target in targets)
                {
                    if (GetAvailableScouts(village) <= 0) break;

                    // Known-type stash whose size isn't cached yet — fetch exactly one per tick
                    // from the server (never burst), then process it on a later tick from cache.
                    if (target.ResourceLevel == LEVEL_NEEDS_FETCH)
                    {
                        try { GameEngine.Instance.World.getSpecialVillageData(target.StashId, true); }
                        catch { continue; }
                        LogDebug("Fetching stash size for " + target.StashId
                            + " (" + GetStashDisplayName(target.StashId) + ")");
                        _lastSendTime = DateTime.Now;
                        _cycleComplete = false;
                        return;
                    }

                    if (SendScout(village, target, GetAvailableScouts(village), settings))
                    {
                        // One send per tick — come back next tick for the next target
                        _lastSendTime = DateTime.Now;
                        _cycleComplete = false;
                        return;
                    }
                }
            }

            // Iterated everything without taking an action — cycle is done
            _cycleComplete = true;
            _lastCycleTime = DateTime.Now;
            LogDebug("Scout cycle complete. Next cycle in " + settings.CycleIntervalSeconds + "s.");
        }

        // Scouts genuinely available to send from this village right now.
        // m_numScouts is scouts *at home* (traveling ones live in the army array), so we must NOT
        // subtract traveling scouts from it. We do cap by (research max - traveling) to guard the
        // race where a VillageSync refresh writes a pre-send m_numScouts back as stale-high.
        private static int GetAvailableScouts(VillageMap village)
        {
            int traveling = GetTravelingScoutsFromVillage(village.VillageID);
            int maxScouts = (int)GameEngine.Instance.World.UserResearchData.Research_Scouts;
            return Math.Min(village.m_numScouts, Math.Max(0, maxScouts - traveling));
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
                    // New/undiscovered stash. Only the closest enabled village should claim it,
                    // and only if we'd arrive before any scout already heading there.
                    if (IsAnotherVillageCloser(villageId, vd.id, travelTime)) continue;
                    if (!IsFirstToStash(vd.id, travelTime)) continue;
                    resourceLevel = LEVEL_NEW_STASH;
                }
                else
                {
                    // Known resource stash — read size from cache only (never fire a server
                    // request here). Uncached stashes are marked for a one-at-a-time fetch in OnTick.
                    WorldMap.SpecialVillageCache svc;
                    try { svc = GameEngine.Instance.World.peekSpecialVillageData(vd.id); }
                    catch { continue; }
                    resourceLevel = svc != null ? svc.resourceLevel : LEVEL_NEEDS_FETCH;
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

        // Is some other enabled user village closer to this (new) stash than we are?
        // Ensures only the nearest village claims a freshly-spawned stash.
        private bool IsAnotherVillageCloser(int ourVillageId, int stashId, double ourTime)
        {
            ScoutSettings settings = Settings;
            if (settings == null) return false;
            foreach (VillageScoutSettings other in settings.Villages)
            {
                if (other.VillageId == ourVillageId) continue;
                if (!other.ScoutingEnabled) continue;
                try
                {
                    if (!GameEngine.Instance.World.isUserVillage(other.VillageId)) continue;
                    if (CalculateTravelTime(other.VillageId, stashId) < ourTime)
                        return true;
                }
                catch { }
            }
            return false;
        }

        // Would we reach this stash before any scout army already heading there?
        // Conservative: on repeated enumeration failure, assume we are not first.
        private bool IsFirstToStash(int stashId, double ourTravelTime)
        {
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    DateTime now = VillageMap.getCurrentServerTime();
                    foreach (object obj in GameEngine.Instance.World.getArmyArray())
                    {
                        WorldMap.LocalArmyData army = (WorldMap.LocalArmyData)obj;
                        if (army.numScouts > 0 && army.lootType < 0 && army.targetVillageID == stashId)
                        {
                            double theirRemaining = (army.serverEndTime - now).TotalSeconds;
                            if (theirRemaining < ourTravelTime)
                                return false;
                        }
                    }
                    return true;
                }
                catch (InvalidOperationException)
                {
                    attempts++;
                }
            }
            return false;
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

        // Storage cap for one resource type at this village (research cap × card multiplier).
        // buildingType is the 0-based resource id (stashType - 100). Returns 0 if it can't be computed.
        private static int GetResourceCap(VillageMap village, int stashType)
        {
            try
            {
                int buildingType = stashType - 100;
                double baseCap = GameEngine.Instance.World.UserResearchData.getResourceCap(
                    GameEngine.Instance.LocalWorldData, buildingType, false);
                double mult = CardTypes.getResourceCapMultiplier(buildingType,
                    GameEngine.Instance.cardsManager.UserCardData);
                return (int)(baseCap * mult);
            }
            catch
            {
                return 0;
            }
        }

        // Resources of a given type already foraged and currently returning to this village.
        // Mirrors the loot half of the reference CalcMovingScouts. lootType is the 100-based special.
        private static int GetIncomingLoot(int villageId, int lootType)
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
                        if (army.homeVillageID == villageId
                            && army.lootType == lootType
                            && army.lootLevel > 0.0)
                        {
                            total += (int)army.lootLevel;
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

        // Count ALL scouts owned by a village (home + traveling). Traveling scouts live in the
        // army array; m_numScouts is home only. Used to size availability against the research cap.
        private static int GetTravelingScoutsFromVillage(int villageId)
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
                        if (army.numScouts > 0 && army.homeVillageID == villageId)
                            total += army.numScouts;
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

        // Find one scout army stranded on a stash that has despawned (no longer in our map data)
        // and recall it. At most one cancel per tick to keep request pacing consistent.
        private bool TryCancelStrandedScout()
        {
            long armyId = -1;
            int targetId = -1;
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    DateTime now = VillageMap.getCurrentServerTime();
                    foreach (object obj in GameEngine.Instance.World.getArmyArray())
                    {
                        WorldMap.LocalArmyData army = (WorldMap.LocalArmyData)obj;
                        if (!army.dead
                            && army.lootType < 0          // outbound, hasn't foraged yet
                            && army.isScouts()
                            && GameEngine.Instance.World.isUserVillage(army.homeVillageID)
                            && !GameEngine.Instance.World.isVillageVisible(army.targetVillageID)
                            && (army.serverEndTime - now).TotalSeconds > 15.0)
                        {
                            armyId = army.armyID;
                            targetId = army.targetVillageID;
                            break;
                        }
                    }
                    break;
                }
                catch (InvalidOperationException)
                {
                    armyId = -1;
                    attempts++;
                }
            }

            if (armyId < 0) return false;

            try
            {
                RemoteServices.Instance.set_CancelCastleAttack_UserCallBack(OnCancelScoutsCallback);
                RemoteServices.Instance.CancelCastleAttack(armyId);
                LogDebug("Recalling scouts from despawned stash " + targetId
                    + " (" + GetStashDisplayName(targetId) + ")");
                _lastSendTime = DateTime.Now;
                _cycleComplete = false;
                return true;
            }
            catch (Exception ex)
            {
                LogError("CancelStrandedScout error: " + ex.Message);
                return false;
            }
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

        private bool SendScout(VillageMap village, StashTarget target, int availableScouts,
            ScoutSettings settings)
        {
            if (availableScouts <= 0) return false;

            int enRoute = GetScoutsEnRoute(target.StashId);
            int count;

            if (target.ResourceType == 100)
            {
                // New/undiscovered stash — size unknown until scouted
                if (enRoute > 0) return false; // already being discovered
                count = settings.SendOneOnNewStash ? 1 : Math.Min(2, availableScouts);
            }
            else
            {
                int carryPerScout;
                try { carryPerScout = CalculateCarryPerScout(target.ResourceType); }
                catch { return false; }

                int enRouteClaim = enRoute * carryPerScout;
                int remaining = target.ResourceLevel - enRouteClaim;
                if (target.ResourceLevel > 0 && remaining <= 0)
                    return false; // en-route scouts already cover the full stash

                // Respect village storage capacity if enabled — don't forage into a near-full store.
                int maxBySpace = int.MaxValue;
                if (settings.WaitForFreeSpace)
                {
                    int cap = GetResourceCap(village, target.ResourceType);
                    if (cap > 0)
                    {
                        int level = (int)village.getResourceLevel(target.ResourceType - 100);
                        int incoming = GetIncomingLoot(village.VillageID, target.ResourceType);
                        int freeSpace = cap - level - incoming - enRouteClaim;
                        // Effectively full: bail only when free space can't fit a scout's carry AND
                        // is under 10% of cap (the cap/10 floor lets small-cap stores still top off).
                        if (freeSpace < carryPerScout && freeSpace < cap / 10)
                            return false;
                        maxBySpace = Math.Max(1, freeSpace / carryPerScout);
                    }
                }

                if (settings.SendOneScout)
                {
                    // Single-scout mode: drip-feed 1 scout at a time until stash/space is covered
                    count = 1;
                }
                else
                {
                    if (target.ResourceLevel <= 0) return false;
                    count = CalculateOptimalScouts(remaining, carryPerScout);
                    if (maxBySpace != int.MaxValue)
                        count = Math.Min(count, maxBySpace);
                }
                count = Math.Min(count, availableScouts);
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
                else
                {
                    LogError("SendScouts failed: "
                        + ErrorCodes.getErrorString(ret.m_errorCode, ret.m_errorID)
                        + " | from " + GameEngine.Instance.World.getVillageName(ret.sourceVillage)
                        + " to " + GameEngine.Instance.World.getVillageNameOrType(ret.targetVillage));
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

        private void OnCancelScoutsCallback(CancelCastleAttack_ReturnType ret)
        {
            try
            {
                if (ret.Success)
                {
                    if (ret.armyData == null) return;
                    ArmyReturnData[] data = new ArmyReturnData[] { ret.armyData };
                    GameEngine.Instance.World.doGetArmyData(
                        (IEnumerable<ArmyReturnData>)data,
                        (IEnumerable<ArmyReturnData>)null, false);
                    GameEngine.Instance.World.addExistingArmy(ret.armyData.armyID);
                    GameEngine.Instance.World.deleteArmy(ret.oldArmyID);
                }
                else
                {
                    LogError("CancelScouts failed: "
                        + ErrorCodes.getErrorString(ret.m_errorCode, ret.m_errorID));
                }
            }
            catch (Exception ex)
            {
                LogError("CancelScouts callback error: " + ex.Message);
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
            public int ResourceLevel; // -1 = new stash (type 100); -2 = known type, size not cached; >=0 known
        }
    }
}
