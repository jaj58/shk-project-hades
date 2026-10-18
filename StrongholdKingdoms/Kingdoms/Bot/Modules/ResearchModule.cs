using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot.Modules
{
    /// <summary>
    /// Works through the Research tab's plan: keeps the player's research slots filled (1 without
    /// a premium token, up to 6 with one) with the first startable plan entry, researching missing
    /// prerequisites first when enabled. Optional extras when the plan is stuck: rank up (honour
    /// permitting) and buy research points with gold (capped).
    ///
    /// What to research comes from <see cref="ResearchPlanner"/>. This class only paces the
    /// sends: one request (research / buy point / rank up) in flight at a time, because each RPC
    /// has a single global callback slot and the game's own buttons use the same slots.
    /// Completion needs no polling here: GameEngine calls World.updateResearch every frame, which
    /// refetches the research data shortly after the current research finishes.
    /// </summary>
    public class ResearchModule : BotModuleBase
    {
        private enum PendingKind { None, Research, BuyPoint, RankUp }

        private const int PendingTimeoutSeconds = 30;
        private const int AfterSuccessDelaySeconds = 2;
        private const int AfterRejectDelaySeconds = 60;
        private const int ParkMinutes = 10;
        private const int ExtraBackoffMinutes = 5;

        private PendingKind _pending = PendingKind.None;
        private DateTime _pendingSince;
        private ResearchPlanner.Pick _pendingPick;
        private DateTime _nextCheck = DateTime.MinValue;
        private DateTime _lastForcedRefresh = DateTime.MinValue;
        private DateTime _rankUpBlockedUntil = DateTime.MinValue;
        private DateTime _buyBlockedUntil = DateTime.MinValue;
        private readonly Dictionary<int, DateTime> _parkedUntil = new Dictionary<int, DateTime>();
        private readonly Dictionary<int, int> _notAvailableRejects = new Dictionary<int, int>();
        private string _lastIdleReason = "";
        private string _status = "";

        public override string ModuleName
        {
            get { return "Research"; }
        }

        // Ticks every second; the check interval and post-send delays are enforced via _nextCheck.
        public override TimeSpan Interval
        {
            get { return TimeSpan.FromSeconds(1); }
        }

        private ResearchSettings Settings
        {
            get { return Engine != null && Engine.Settings != null ? Engine.Settings.Research : null; }
        }

        /// <summary>Last action or idle reason, for the Research tab.</summary>
        public string StatusText
        {
            get { return _status; }
        }

        /// <summary>Research types the server rejected as unavailable, skipped for a while.</summary>
        public ICollection<int> ParkedTypes
        {
            get
            {
                PruneParked();
                return new List<int>(_parkedUntil.Keys);
            }
        }

        protected override void OnInitialize()
        {
            _pending = PendingKind.None;
            _nextCheck = DateTime.MinValue;
            _parkedUntil.Clear();
            _notAvailableRejects.Clear();
            _lastIdleReason = "";
            _status = "";
        }

        protected override void OnEnable()
        {
            _nextCheck = DateTime.MinValue;
            _lastIdleReason = "";
        }

        protected override void OnTick()
        {
            ResearchSettings s = Settings;
            if (s == null)
                return;
            if (GameEngine.Instance == null || GameEngine.Instance.World == null
                || GameEngine.Instance.World.UserResearchData == null || GameEngine.Instance.LocalWorldData == null)
            {
                _status = "Waiting for world data";
                return;
            }

            DateTime now = DateTime.Now;
            if (_pending != PendingKind.None)
            {
                if ((now - _pendingSince).TotalSeconds < PendingTimeoutSeconds)
                    return;
                LogWarning("No reply to the " + PendingName(_pending) + " request after " + PendingTimeoutSeconds
                    + "s - refreshing research data and retrying.");
                _pending = PendingKind.None;
                GameEngine.Instance.World.updateResearch(true);
                _nextCheck = now.AddSeconds(5);
                return;
            }

            if (now < _nextCheck)
                return;
            _nextCheck = now.AddSeconds(Math.Max(3, s.CheckIntervalSeconds));
            RunCheck(s, GameEngine.Instance.World, GameEngine.Instance.LocalWorldData, now);
        }

        private void RunCheck(ResearchSettings s, WorldMap world, WorldData wd, DateTime now)
        {
            ResearchData data = world.UserResearchData;

            // The game's own refetch can stall (a failed GetResearchData leaves requestSent set),
            // which would leave a finished research showing as in progress forever.
            if (data.researchingType >= 0
                && VillageMap.getCurrentServerTime() > data.research_completionTime.AddSeconds(120.0)
                && (now - _lastForcedRefresh).TotalSeconds >= 60.0)
            {
                _lastForcedRefresh = now;
                LogDebug("Current research is overdue - forcing a research data refresh.");
                world.updateResearch(true);
                _nextCheck = now.AddSeconds(5);
                return;
            }

            // The player clicked research/buy in the game UI and that request is still pending.
            if (world.BotResearchRequestPending || world.BotBuyPointRequestPending)
            {
                SetIdle("Waiting for the game's own research request");
                return;
            }

            ResearchPlanner.PlanEvaluation eval = ResearchPlanner.Evaluate(data, s.Plan, world.getRank(),
                world.getRankSubLevel(), wd, ResearchPlanner.IsPremium(), s.MaxQueueSlots, s.AutoPrerequisites,
                ParkedTypes);

            if (s.RankUpMode == ResearchRankUpMode.WheneverPossible && TryRankUp(world, wd, now))
                return;

            if (eval.NextPick != null && eval.HasFreeSlot && eval.SpendablePoints > 0)
            {
                SendResearch(world, eval, now);
                return;
            }

            bool stuckOnPoints = eval.NextPick != null && eval.HasFreeSlot && eval.SpendablePoints <= 0;
            bool stuckOnRank = eval.NextPick == null && eval.BlockedOnRank;
            if (s.RankUpMode == ResearchRankUpMode.WhenNeeded && (stuckOnPoints || stuckOnRank)
                && TryRankUp(world, wd, now))
                return;
            string notBuyingReason = null;
            if (stuckOnPoints && s.BuyPointsWithGold && TryBuyPoint(world, data, wd, s, now, out notBuyingReason))
                return;

            if (s.Plan.Count == 0)
                SetIdle("Plan is empty");
            else if (eval.PlanComplete)
                SetIdle("Plan complete");
            else if (eval.NextPick != null && !eval.HasFreeSlot)
                SetIdle(eval.Premium
                    ? "Queue full (" + eval.SlotsUsed + "/" + eval.SlotLimit + ")"
                    : "Researching - no premium token, so 1 at a time");
            else if (stuckOnPoints)
                SetIdle(notBuyingReason ?? "Out of research points");
            else if (stuckOnRank)
                SetIdle("Remaining plan entries need a higher rank");
            else
                SetIdle("Nothing in the plan can be researched yet");
        }

        // =====================================================================
        // Research
        // =====================================================================

        private void SendResearch(WorldMap world, ResearchPlanner.PlanEvaluation eval, DateTime now)
        {
            ResearchPlanner.Pick pick = eval.NextPick;
            if (!world.botDoResearch(pick.ResearchType, OnResearchResult))
            {
                SetIdle("Waiting for the game's own research request");
                return;
            }
            _pending = PendingKind.Research;
            _pendingSince = now;
            _pendingPick = pick;
            _lastIdleReason = "";
            _status = "Requesting " + PickText(pick) + "...";
            LogDebug("Requesting " + PickText(pick) + " (slot " + (eval.SlotsUsed + 1) + "/" + eval.SlotLimit
                + ", " + eval.SpendablePoints + " point(s) spendable).");
        }

        private void OnResearchResult(DoResearch_ReturnType returnData)
        {
            ResearchPlanner.Pick pick = _pendingPick;
            if (_pending == PendingKind.Research)
                _pending = PendingKind.None;
            if (pick == null)
                return;

            DateTime now = DateTime.Now;
            if (returnData != null && returnData.Success)
            {
                _notAvailableRejects.Remove(pick.ResearchType);
                ResearchData data = GameEngine.Instance != null && GameEngine.Instance.World != null
                    ? GameEngine.Instance.World.UserResearchData : null;
                bool started = data != null && data.researchingType == pick.ResearchType
                    && (data.research_queueEntries == null || data.research_queueEntries.Length == 0);
                _status = (started ? "Started " : "Queued ") + PickText(pick);
                LogInfo(_status + ".");
                _nextCheck = now.AddSeconds(AfterSuccessDelaySeconds);
                return;
            }

            string error = ErrorText(returnData);
            _status = "Server rejected " + PickText(pick) + ": " + error;
            LogWarning(_status);
            if (returnData != null
                && returnData.m_errorCode == ErrorCodes.ErrorCode.RESEARCH_CANNOT_DO_RESEARCH_NOT_AVAILABLE)
            {
                int count;
                _notAvailableRejects.TryGetValue(pick.ResearchType, out count);
                _notAvailableRejects[pick.ResearchType] = ++count;
                if (count >= 2)
                {
                    _notAvailableRejects.Remove(pick.ResearchType);
                    _parkedUntil[pick.ResearchType] = now.AddMinutes(ParkMinutes);
                    LogWarning("Skipping " + ResearchPlanner.Name(pick.ResearchType) + " for " + ParkMinutes
                        + " minutes - the server keeps saying it isn't available.");
                }
            }
            // A rejected DoResearch doesn't update the game's research data, so refetch it before
            // deciding again.
            if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                GameEngine.Instance.World.updateResearch(true);
            _nextCheck = now.AddSeconds(AfterRejectDelaySeconds);
        }

        // =====================================================================
        // Rank up
        // =====================================================================

        private bool TryRankUp(WorldMap world, WorldData wd, DateTime now)
        {
            if (now < _rankUpBlockedUntil || world.WorldEnded)
                return false;
            int rank = world.getRank();
            int subLevel = world.getRankSubLevel();
            double needed = ResearchPlanner.HonourForNextRank(rank, subLevel, wd);
            if (needed < 0.0 || world.getCurrentHonour() < needed)
                return false;

            RemoteServices.Instance.set_UpgradeRank_UserCallBack(
                new RemoteServices.UpgradeRank_UserCallBack(OnRankUpResult));
            RemoteServices.Instance.UpgradeRank(rank, subLevel);
            _pending = PendingKind.RankUp;
            _pendingSince = now;
            _lastIdleReason = "";
            _status = "Ranking up...";
            LogInfo("Ranking up (" + needed.ToString("N0") + " honour).");
            return true;
        }

        // Same as RankingsPanel.upgradeRankCallBack's success branch, minus the panel itself.
        private void OnRankUpResult(UpgradeRank_ReturnType returnData)
        {
            if (_pending == PendingKind.RankUp)
                _pending = PendingKind.None;
            DateTime now = DateTime.Now;
            if (returnData == null || !returnData.Success)
            {
                _rankUpBlockedUntil = now.AddMinutes(ExtraBackoffMinutes);
                _status = "Rank up rejected: " + ErrorText(returnData);
                LogWarning(_status + " - not trying again for " + ExtraBackoffMinutes + " minutes.");
                return;
            }

            WorldMap world = GameEngine.Instance.World;
            world.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
            InterfaceMgr.Instance.setHonour(returnData.currentHonourLevel, returnData.rank);
            world.setRanking(returnData.rank, returnData.rankSubLevel);
            world.setResearchData(returnData.researchData);
            InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
            world.setPoints(returnData.currentPoints);
            if (returnData.rank == 1 && world.getTutorialStage() == 7)
                world.forceTutorialToBeShown();
            world.LastUpdatedCrowns = DateTime.MinValue;
            GameEngine.Instance.cardsManager.ResetPremiumOffers();
            GameEngine.Instance.cardsManager.RetrievePremiumOffers();

            _status = "Ranked up to rank " + (returnData.rank + 1) + " (level " + (returnData.rankSubLevel + 1) + ")";
            LogInfo(_status + ".");
            _nextCheck = now.AddSeconds(AfterSuccessDelaySeconds);
        }

        // =====================================================================
        // Buy research points
        // =====================================================================

        private bool TryBuyPoint(WorldMap world, ResearchData data, WorldData wd, ResearchSettings s, DateTime now,
            out string notBuyingReason)
        {
            notBuyingReason = null;
            if (now < _buyBlockedUntil)
                return false;
            double cost = data.calcPointGoldCost(GameEngine.Instance.cardsManager.UserCardData, wd);
            double gold = world.getCurrentGold();
            if (cost > s.MaxGoldPerPoint)
            {
                notBuyingReason = "Out of points - not buying: next point costs " + cost.ToString("N0")
                    + " gold (limit " + s.MaxGoldPerPoint.ToString("N0") + ")";
                return false;
            }
            if (gold - cost < s.MinGoldReserve)
            {
                notBuyingReason = "Out of points - not buying: a point costs " + cost.ToString("N0")
                    + " gold and " + s.MinGoldReserve.ToString("N0") + " is kept in reserve";
                return false;
            }
            if (!world.botBuyResearchPoint(OnBuyPointResult))
                return false;

            _pending = PendingKind.BuyPoint;
            _pendingSince = now;
            _lastIdleReason = "";
            _status = "Buying a research point...";
            LogInfo("Buying a research point for " + cost.ToString("N0") + " gold.");
            return true;
        }

        private void OnBuyPointResult(BuyResearchPoint_ReturnType returnData)
        {
            if (_pending == PendingKind.BuyPoint)
                _pending = PendingKind.None;
            DateTime now = DateTime.Now;
            if (returnData != null && returnData.Success)
            {
                _status = "Bought a research point";
                LogInfo(_status + ".");
                _nextCheck = now.AddSeconds(AfterSuccessDelaySeconds);
                return;
            }
            _buyBlockedUntil = now.AddMinutes(ExtraBackoffMinutes);
            _status = "Buying a research point failed: " + ErrorText(returnData);
            LogWarning(_status + " - not trying again for " + ExtraBackoffMinutes + " minutes.");
        }

        // =====================================================================
        // Helpers
        // =====================================================================

        private void SetIdle(string reason)
        {
            _status = reason;
            if (reason == _lastIdleReason)
                return;
            _lastIdleReason = reason;
            LogDebug(reason + ".");
        }

        private void PruneParked()
        {
            if (_parkedUntil.Count == 0)
                return;
            DateTime now = DateTime.Now;
            List<int> expired = new List<int>();
            foreach (KeyValuePair<int, DateTime> kv in _parkedUntil)
                if (now >= kv.Value)
                    expired.Add(kv.Key);
            foreach (int type in expired)
                _parkedUntil.Remove(type);
        }

        private static string PickText(ResearchPlanner.Pick pick)
        {
            string text = ResearchPlanner.Name(pick.ResearchType) + " " + pick.ToLevel;
            if (pick.IsPrerequisite)
                text += " (prerequisite for " + ResearchPlanner.Name(pick.ForType) + ")";
            return text;
        }

        private static string PendingName(PendingKind kind)
        {
            switch (kind)
            {
                case PendingKind.Research: return "research";
                case PendingKind.BuyPoint: return "buy research point";
                case PendingKind.RankUp: return "rank up";
                default: return "";
            }
        }

        private static string ErrorText(Common_ReturnData returnData)
        {
            if (returnData == null)
                return "no data";
            try
            {
                return ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID);
            }
            catch
            {
                return returnData.m_errorCode.ToString();
            }
        }
    }
}
