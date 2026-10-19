using System;
using System.Collections.Generic;
using CommonTypes;

namespace Kingdoms.Bot
{
    /// <summary>
    /// Decides what the Research module should research next. The core (<see cref="Evaluate"/>)
    /// only reads the ResearchData it is given and sends nothing, so the Research tab can show
    /// live plan statuses even while the module is off.
    ///
    /// The gating mirrors the game's own research panel (ResearchPanel.initTab/applyData):
    ///   - everything is judged against a queue-adjusted copy (in-progress + queued levels added),
    ///     so a prerequisite that is only queued already counts;
    ///   - spendable points = research_points - queued entries (queued points aren't deducted yet);
    ///   - a slot is free when nothing is researching or canDoMoreResearch(premium) (6 / 1 slots);
    ///   - a step is startable when below getNumLevels(type, rank, world) and isResearchStepOpen.
    /// Only research types from the four player tabs are ever offered; 31/77/79/80/81/83 are
    /// parish-capital research (donation-based) and can't be started with DoResearch.
    /// </summary>
    public static class ResearchPlanner
    {
        public const int PremiumSlotLimit = 6;
        // Leadership tops out at 23 levels; anything above is clamped by getNumLevels anyway.
        public const int MaxTargetLevel = 30;
        private const int MaxPrereqDepth = 10;

        public class Category
        {
            public string Name;
            public int[] Types;

            public Category(string name, int[] types)
            {
                Name = name;
                Types = types ?? new int[0];
            }
        }

        public enum EntryState
        {
            Done,
            Queued,
            Next,
            Waiting,
            NeedsRank,
            Blocked,
            Disabled,
            Unavailable
        }

        public class EntryResult
        {
            public EntryState State;
            public int CurrentLevel;   // completed level
            public int QueuedLevel;    // level once the in-progress + queued research finish
            public int MaxLevel;
            public int Target;         // TargetLevel clamped to MaxLevel
            public string Text = "";
        }

        public class Pick
        {
            public int ResearchType;
            public int ToLevel;
            public int EntryIndex;
            // True when ResearchType is a prerequisite researched on behalf of the plan entry.
            public bool IsPrerequisite;
            public int ForType;
        }

        public class PlanEvaluation
        {
            public bool Premium;
            public int SlotsUsed;
            public int SlotLimit;
            public bool HasFreeSlot;
            public int SpendablePoints;
            public Pick NextPick;
            // Some unfinished entry is waiting on a higher rank.
            public bool BlockedOnRank;
            // Every enabled entry is done or already queued.
            public bool PlanComplete;
            public EntryResult[] Entries = new EntryResult[0];
            public ResearchData Queued;
        }

        // Result of resolving one research type (possibly through its prerequisite chain).
        private class Resolution
        {
            public int StartType = -1;
            public int RankNeeded = -1;
            public int RankFor = -1;      // type that needs the rank (when it's a prerequisite)
            public int MissingType = -1;  // prerequisite that isn't met (auto-prereqs off / unresolvable)
            public int MissingLevel;
        }

        // =====================================================================
        // Catalog
        // =====================================================================

        public static List<Category> PlayerTabs(bool alternateRuleset)
        {
            List<Category> tabs = new List<Category>();
            tabs.Add(new Category("Industry", ResearchData.industryResearchLayout));
            tabs.Add(new Category("Military", ResearchData.militaryResearchLayout));
            tabs.Add(new Category("Farming", ResearchData.farmingResearchLayout));
            tabs.Add(new Category("Education", alternateRuleset
                ? ResearchData.educationResearchLayout2 : ResearchData.educationResearchLayout));
            return tabs;
        }

        /// <summary>True if the type is a player research on any ruleset (used to validate saved plans).</summary>
        public static bool IsKnownPlayerResearch(int type)
        {
            return Contains(ResearchData.industryResearchLayout, type)
                || Contains(ResearchData.militaryResearchLayout, type)
                || Contains(ResearchData.farmingResearchLayout, type)
                || Contains(ResearchData.educationResearchLayout, type)
                || Contains(ResearchData.educationResearchLayout2, type);
        }

        /// <summary>True if the type can be researched by the player on this world's ruleset.</summary>
        public static bool IsPlayerResearch(int type, bool alternateRuleset)
        {
            foreach (Category c in PlayerTabs(alternateRuleset))
                if (Contains(c.Types, type))
                    return true;
            return false;
        }

        public static string Name(int type)
        {
            try
            {
                string name = ResearchData.getResearchName(type);
                if (!string.IsNullOrEmpty(name))
                    return name;
            }
            catch
            {
            }
            return "Research " + type;
        }

        public static int MaxLevel(int type, int rank, WorldData wd)
        {
            return ResearchData.getNumLevels(type, rank, wd);
        }

        /// <summary>Max level at the top rank (only Leadership grows with rank: 13 → 23).</summary>
        public static int AbsoluteMaxLevel(int type, WorldData wd)
        {
            return ResearchData.getNumLevels(type, 22, wd);
        }

        /// <summary>
        /// Empty if the next level of the type can be started now (against the queue-adjusted
        /// data); otherwise a short reason ("max", "needs rank 12", "needs Tools 4").
        /// </summary>
        public static string StepBlockText(ResearchData queued, int type, int rank, int rankSubLevel, WorldData wd)
        {
            int level = queued.research[type];
            if (level >= MaxLevel(type, rank, wd))
                return "max";
            int rankNeeded = -1;
            bool special = false;
            if (queued.isResearchStepOpen(type, level, rank, rankSubLevel, ref rankNeeded, ref special, wd.EraWorld))
                return "";
            if (rankNeeded > 0)
                return "needs " + RankText(rankNeeded);
            ResearchData.BranchRequirement req = ResearchData.getBranchRequirement(type);
            if (req != null)
                return "needs " + Name(req.ResearchType) + " " + req.Level;
            return "locked";
        }

        /// <summary>Rank display text for isResearchStepOpen's rankNeeded (100+ = top-rank sub-levels).</summary>
        public static string RankText(int rankNeeded)
        {
            return rankNeeded < 100 ? "rank " + rankNeeded : "top-rank level " + (rankNeeded - 100 + 1);
        }

        /// <summary>
        /// Honour needed to rank up from (rank, subLevel), or -1 if there is no next rank.
        /// Mirrors RankingsPanel.setCurrentRankings.
        /// </summary>
        public static double HonourForNextRank(int rank, int subLevel, WorldData wd)
        {
            if (wd == null || wd.ranks_Levels == null || wd.ranks_HonourPerLevel == null)
                return -1.0;
            if (rank < 0 || rank >= wd.ranks_Levels.Length || rank >= wd.ranks_HonourPerLevel.Length)
                return -1.0;
            int nextRank = rank;
            int nextSub = subLevel + 1;
            if (nextSub >= wd.ranks_Levels[rank])
            {
                nextSub = 0;
                ++nextRank;
            }
            if (nextRank >= 23)
                return -1.0;
            double needed = wd.ranks_HonourPerLevel[rank];
            if (rank == 21 && subLevel >= 24)
                needed = 10000000.0;
            else if (rank == 22)
                needed = Rankings.calcHonourForCrownPrince(subLevel);
            return needed;
        }

        // =====================================================================
        // Evaluation
        // =====================================================================

        /// <summary>The research data as it will be once the in-progress and queued research finish.</summary>
        public static ResearchData BuildQueued(ResearchData data)
        {
            ResearchData queued = data;
            if (data.researchingType >= 0)
            {
                queued = data.copyAndAdd(data.researchingType, false);
                if (data.research_queueEntries != null)
                {
                    foreach (int entry in data.research_queueEntries)
                        queued = queued.copyAndAdd(entry, true);
                }
            }
            return queued;
        }

        public static int CountSlotsUsed(ResearchData data)
        {
            int used = data.researchingType >= 0 ? 1 : 0;
            if (data.research_queueEntries != null)
            {
                foreach (int entry in data.research_queueEntries)
                    if (entry >= 0)
                        ++used;
            }
            return used;
        }

        public static int SpendablePoints(ResearchData data)
        {
            int points = data.research_points;
            if (data.research_queueEntries != null)
                points -= data.research_queueEntries.Length;
            return points;
        }

        public static PlanEvaluation Evaluate(
            ResearchData data,
            IList<ResearchPlanEntry> plan,
            int rank,
            int rankSubLevel,
            WorldData wd,
            bool premium,
            int maxSlots,
            bool autoPrerequisites,
            ICollection<int> parkedTypes)
        {
            PlanEvaluation eval = new PlanEvaluation();
            eval.Premium = premium;
            if (data == null || wd == null)
                return eval;

            bool alternate = wd.Alternate_Ruleset == 1;
            bool eraWorld = wd.EraWorld;
            ResearchData queued = BuildQueued(data);
            eval.Queued = queued;
            eval.SlotsUsed = CountSlotsUsed(data);
            eval.SlotLimit = premium ? Math.Max(1, Math.Min(PremiumSlotLimit, maxSlots)) : 1;
            eval.HasFreeSlot = eval.SlotsUsed < eval.SlotLimit
                && (data.researchingType < 0 || data.canDoMoreResearch(premium));
            eval.SpendablePoints = SpendablePoints(data);

            int count = plan != null ? plan.Count : 0;
            eval.Entries = new EntryResult[count];
            bool anyUnfinished = false;

            for (int i = 0; i < count; i++)
            {
                ResearchPlanEntry entry = plan[i];
                EntryResult r = new EntryResult();
                eval.Entries[i] = r;
                int type = entry != null ? entry.ResearchType : -1;

                if (entry == null || !IsPlayerResearch(type, alternate) || type >= data.research.Length)
                {
                    r.State = EntryState.Unavailable;
                    r.Text = "Not available on this world";
                    continue;
                }

                r.CurrentLevel = data.research[type];
                r.QueuedLevel = queued.research[type];
                r.MaxLevel = MaxLevel(type, rank, wd);
                r.Target = Math.Min(entry.TargetLevel, r.MaxLevel);

                if (r.CurrentLevel >= r.Target)
                {
                    r.State = EntryState.Done;
                    r.Text = r.Target < entry.TargetLevel ? "Done (max " + r.MaxLevel + ")" : "Done";
                    continue;
                }
                if (r.QueuedLevel >= r.Target)
                {
                    r.State = EntryState.Queued;
                    r.Text = "In game queue";
                    continue;
                }
                if (!entry.Enabled)
                {
                    r.State = EntryState.Disabled;
                    r.Text = "Disabled";
                    continue;
                }

                anyUnfinished = true;
                Resolution res = Resolve(queued, type, rank, rankSubLevel, wd, eraWorld, alternate,
                    autoPrerequisites, parkedTypes, 0, new HashSet<int>());

                if (res.StartType >= 0)
                {
                    bool isPrereq = res.StartType != type;
                    string step = Name(res.StartType) + " " + (queued.research[res.StartType] + 1);
                    if (eval.NextPick == null)
                    {
                        eval.NextPick = new Pick
                        {
                            ResearchType = res.StartType,
                            ToLevel = queued.research[res.StartType] + 1,
                            EntryIndex = i,
                            IsPrerequisite = isPrereq,
                            ForType = type
                        };
                        r.State = EntryState.Next;
                        r.Text = "Next: " + step + (isPrereq ? " (prerequisite)" : "");
                        if (!eval.HasFreeSlot)
                            r.Text += " - queue full";
                        else if (eval.SpendablePoints <= 0)
                            r.Text += " - no research points";
                    }
                    else
                    {
                        r.State = EntryState.Waiting;
                        r.Text = isPrereq ? "Waiting (then " + step + ")" : "Waiting";
                    }
                    continue;
                }

                if (res.RankNeeded > 0)
                {
                    r.State = EntryState.NeedsRank;
                    r.Text = "Needs " + RankText(res.RankNeeded)
                        + (res.RankFor >= 0 && res.RankFor != type ? " (for " + Name(res.RankFor) + ")" : "");
                    eval.BlockedOnRank = true;
                    continue;
                }

                r.State = EntryState.Blocked;
                if (parkedTypes != null && parkedTypes.Contains(type))
                    r.Text = "Rejected by server - retrying later";
                else if (res.MissingType >= 0)
                    r.Text = "Needs " + Name(res.MissingType) + " " + res.MissingLevel;
                else
                    r.Text = "Blocked";
            }

            eval.PlanComplete = !anyUnfinished;
            return eval;
        }

        private static Resolution Resolve(
            ResearchData queued, int type, int rank, int rankSubLevel, WorldData wd, bool eraWorld,
            bool alternate, bool autoPrerequisites, ICollection<int> parkedTypes, int depth, HashSet<int> visited)
        {
            Resolution res = new Resolution();
            int level = queued.research[type];
            if (level >= MaxLevel(type, rank, wd))
                return res;

            int rankNeeded = -1;
            bool special = false;
            bool open = queued.isResearchStepOpen(type, level, rank, rankSubLevel, ref rankNeeded, ref special, eraWorld);
            bool parked = parkedTypes != null && parkedTypes.Contains(type);
            if (open && !parked)
            {
                res.StartType = type;
                return res;
            }
            if (open)
                return res;

            if (rankNeeded > 0)
            {
                res.RankNeeded = rankNeeded;
                res.RankFor = type;
            }

            // The step is closed by its prerequisite (possibly as well as by rank). Even when this
            // type is rank-locked, researching its missing prerequisite now is never wasted.
            if (queued.isResearchBranchOpen(type))
                return res;
            ResearchData.BranchRequirement req = ResearchData.getBranchRequirement(type);
            if (req == null || req.ResearchType < 0 || req.ResearchType >= queued.research.Length)
                return res;
            if (queued.research[req.ResearchType] >= req.Level)
                return res;
            if (res.RankNeeded <= 0)
            {
                res.MissingType = req.ResearchType;
                res.MissingLevel = req.Level;
            }
            if (!autoPrerequisites || depth >= MaxPrereqDepth || !visited.Add(type)
                || !IsPlayerResearch(req.ResearchType, alternate))
                return res;

            Resolution sub = Resolve(queued, req.ResearchType, rank, rankSubLevel, wd, eraWorld, alternate,
                autoPrerequisites, parkedTypes, depth + 1, visited);
            if (sub.StartType >= 0)
            {
                res.StartType = sub.StartType;
                return res;
            }
            if (res.RankNeeded <= 0 && sub.RankNeeded > 0)
            {
                res.RankNeeded = sub.RankNeeded;
                res.RankFor = sub.RankFor;
            }
            return res;
        }

        /// <summary>
        /// Evaluates the plan against the live game state, or returns null when not in a world.
        /// </summary>
        public static PlanEvaluation EvaluateLive(ResearchSettings settings, ICollection<int> parkedTypes)
        {
            if (settings == null || GameEngine.Instance == null || GameEngine.Instance.World == null)
                return null;
            WorldMap world = GameEngine.Instance.World;
            ResearchData data = world.UserResearchData;
            WorldData wd = GameEngine.Instance.LocalWorldData;
            if (data == null || wd == null)
                return null;
            return Evaluate(data, settings.Plan, world.getRank(), world.getRankSubLevel(), wd,
                IsPremium(), settings.MaxQueueSlots, settings.AutoPrerequisites, parkedTypes);
        }

        public static bool IsPremium()
        {
            try
            {
                return GameEngine.Instance != null && GameEngine.Instance.World != null
                    && GameEngine.Instance.cardsManager != null
                    && GameEngine.Instance.cardsManager.UserCardData != null
                    && GameEngine.Instance.World.isAccountPremium();
            }
            catch
            {
                return false;
            }
        }

        private static bool Contains(int[] arr, int value)
        {
            if (arr == null)
                return false;
            foreach (int v in arr)
                if (v == value)
                    return true;
            return false;
        }
    }
}
