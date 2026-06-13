using System;
using System.Collections.Generic;
using System.Text;

namespace Kingdoms.Bot
{
    /// <summary>
    /// Pure, GameEngine-free search core for the Timing Tool.
    ///
    /// Given a flat list of candidate attacks (one per source-village / attack-type / card
    /// combination, each with its max-speed travel time in seconds), it finds the
    /// source-village selections whose attacks land in the smallest possible window
    /// (assuming all are launched simultaneously, so the window is the spread of travel
    /// times). Setups are returned ranked tightest-window-first.
    ///
    /// Per-village limits:
    ///   Vassal OFF — a village supplies at most one attack (captain OR normal).
    ///   Vassal ON  — a village supplies (1 captain + 1 normal) OR (2 normal): at most two
    ///                attacks with at most one captain.
    ///
    /// This class has no dependency on the game; it is driven entirely by the candidate list,
    /// which makes it unit-testable with hand-built data.
    /// </summary>
    public static class TimingSearch
    {
        public class Candidate
        {
            public int VillageId;
            public bool IsCaptain;   // true = captain attack, false = normal attack
            public int CardType;     // 0=none,1=x2,2=x4,3=x6,5=x3,6=x5
            public double Time;      // effective travel time in seconds

            public Candidate() { }
            public Candidate(int villageId, bool isCaptain, int cardType, double time)
            {
                VillageId = villageId; IsCaptain = isCaptain; CardType = cardType; Time = time;
            }
        }

        public class TtAttack
        {
            public int VillageId;
            public bool IsCaptain;
            public int CardType;
            public double Time;

            public TtAttack(int villageId, bool isCaptain, int cardType, double time)
            {
                VillageId = villageId; IsCaptain = isCaptain; CardType = cardType; Time = time;
            }
        }

        public class TtSetup
        {
            public double Window;                 // max-min travel time of the chosen attacks
            public List<TtAttack> Attacks = new List<TtAttack>();
        }

        /// <summary>
        /// Finds all minimum-window setups meeting the requirements, ranked by window ascending.
        /// Returns an empty list if nothing fits.
        /// </summary>
        public static List<TtSetup> FindSetups(List<Candidate> candidates, int minCap, int minNorm,
            double maxWindow, bool vassalOn)
        {
            List<TtSetup> results = new List<TtSetup>();
            if (candidates == null || candidates.Count == 0) return results;
            if (minCap + minNorm <= 0) return results;

            // Sort candidates by travel time ascending.
            List<Candidate> cand = new List<Candidate>(candidates);
            cand.Sort(delegate(Candidate a, Candidate b) { return a.Time.CompareTo(b.Time); });
            int n = cand.Count;

            // Two-pointer sweep. The window is cand[hi].Time - cand[lo].Time over the contiguous
            // slice [lo,hi] (equal times are contiguous, so ties advance hi at zero window cost).
            // Feasibility is monotonic in hi, and the minimal feasible hi is non-decreasing in lo,
            // so hi never moves backward.
            HashSet<string> seen = new HashSet<string>();
            int hi = 0;
            for (int lo = 0; lo < n; lo++)
            {
                if (hi < lo) hi = lo;

                while (hi < n
                       && (cand[hi].Time - cand[lo].Time) <= maxWindow
                       && !Feasible(cand, lo, hi, minCap, minNorm, vassalOn))
                {
                    hi++;
                }

                if (hi < n
                    && (cand[hi].Time - cand[lo].Time) <= maxWindow
                    && Feasible(cand, lo, hi, minCap, minNorm, vassalOn))
                {
                    TtSetup setup = Reconstruct(cand, lo, hi, minCap, minNorm, vassalOn);
                    if (setup != null && setup.Attacks.Count > 0)
                    {
                        string sig = Signature(setup);
                        if (seen.Add(sig))
                            results.Add(setup);
                    }
                }
            }

            results.Sort(delegate(TtSetup a, TtSetup b)
            {
                int c = a.Window.CompareTo(b.Window);
                if (c != 0) return c;
                return a.Attacks.Count.CompareTo(b.Attacks.Count);
            });
            return results;
        }

        // -------------------------------------------------------------------
        // Feasibility
        // -------------------------------------------------------------------

        private static bool Feasible(List<Candidate> cand, int lo, int hi, int minCap, int minNorm,
            bool vassalOn)
        {
            int C, N, B;
            ClassifyVillages(cand, lo, hi, out C, out N, out B);

            if (!vassalOn)
            {
                // At most one attack per village. Captains from C+B, normals from N+B, B shared.
                return (C + B) >= minCap
                    && (N + B) >= minNorm
                    && (C + N + B) >= (minCap + minNorm);
            }
            else
            {
                // Captains only from C+B (one per village).
                if ((C + B) < minCap) return false;
                int capFromC = Math.Min(minCap, C);
                int capFromB = minCap - capFromC;          // captains forced onto B villages
                if (capFromB < 0) capFromB = 0;
                // A B village used for a captain still gives 1 normal; unused B gives 2; N gives 2.
                int normalCap = (B - capFromB) * 2 + capFromB * 1 + N * 2;
                return normalCap >= minNorm;
            }
        }

        /// <summary>Counts captain-only (C), normal-only (N) and both-capable (B) villages in the slice.</summary>
        private static void ClassifyVillages(List<Candidate> cand, int lo, int hi, out int C, out int N, out int B)
        {
            Dictionary<int, bool[]> map = new Dictionary<int, bool[]>(); // [hasCap, hasNorm]
            for (int i = lo; i <= hi; i++)
            {
                Candidate c = cand[i];
                bool[] flags;
                if (!map.TryGetValue(c.VillageId, out flags))
                {
                    flags = new bool[2];
                    map[c.VillageId] = flags;
                }
                if (c.IsCaptain) flags[0] = true; else flags[1] = true;
            }

            C = 0; N = 0; B = 0;
            foreach (bool[] f in map.Values)
            {
                if (f[0] && f[1]) B++;
                else if (f[0]) C++;
                else N++;
            }
        }

        // -------------------------------------------------------------------
        // Reconstruction — concrete per-village picks for a feasible window
        // -------------------------------------------------------------------

        private static TtSetup Reconstruct(List<Candidate> cand, int lo, int hi, int minCap, int minNorm,
            bool vassalOn)
        {
            double mid = (cand[lo].Time + cand[hi].Time) / 2.0;

            // Per village, find the best (closest-to-mid) captain and normal option in the slice.
            Dictionary<int, Candidate> bestCap = new Dictionary<int, Candidate>();
            Dictionary<int, Candidate> bestNorm = new Dictionary<int, Candidate>();
            List<int> villageOrder = new List<int>();
            HashSet<int> seenVillage = new HashSet<int>();

            for (int i = lo; i <= hi; i++)
            {
                Candidate c = cand[i];
                if (seenVillage.Add(c.VillageId)) villageOrder.Add(c.VillageId);

                Dictionary<int, Candidate> target = c.IsCaptain ? bestCap : bestNorm;
                Candidate cur;
                if (!target.TryGetValue(c.VillageId, out cur)
                    || Math.Abs(c.Time - mid) < Math.Abs(cur.Time - mid))
                {
                    target[c.VillageId] = c;
                }
            }
            villageOrder.Sort(); // deterministic

            TtSetup setup = new TtSetup();
            int capNeeded = minCap;
            int normNeeded = minNorm;

            if (!vassalOn)
            {
                HashSet<int> used = new HashSet<int>();

                // Captain-only villages -> captains first.
                foreach (int v in villageOrder)
                {
                    if (capNeeded <= 0) break;
                    if (used.Contains(v)) continue;
                    if (bestCap.ContainsKey(v) && !bestNorm.ContainsKey(v))
                    { AddAttack(setup, bestCap[v]); used.Add(v); capNeeded--; }
                }
                // Normal-only villages -> normals first.
                foreach (int v in villageOrder)
                {
                    if (normNeeded <= 0) break;
                    if (used.Contains(v)) continue;
                    if (bestNorm.ContainsKey(v) && !bestCap.ContainsKey(v))
                    { AddAttack(setup, bestNorm[v]); used.Add(v); normNeeded--; }
                }
                // Both-capable villages fill remaining captains, then normals.
                foreach (int v in villageOrder)
                {
                    if (used.Contains(v)) continue;
                    if (capNeeded > 0 && bestCap.ContainsKey(v))
                    { AddAttack(setup, bestCap[v]); used.Add(v); capNeeded--; }
                    else if (normNeeded > 0 && bestNorm.ContainsKey(v))
                    { AddAttack(setup, bestNorm[v]); used.Add(v); normNeeded--; }
                }
            }
            else
            {
                HashSet<int> usedCaptain = new HashSet<int>();

                // Captains: prefer captain-only villages to spare both-capable for normals.
                foreach (int v in villageOrder)
                {
                    if (capNeeded <= 0) break;
                    if (bestCap.ContainsKey(v) && !bestNorm.ContainsKey(v))
                    { AddAttack(setup, bestCap[v]); usedCaptain.Add(v); capNeeded--; }
                }
                foreach (int v in villageOrder)
                {
                    if (capNeeded <= 0) break;
                    if (usedCaptain.Contains(v)) continue;
                    if (bestCap.ContainsKey(v))
                    { AddAttack(setup, bestCap[v]); usedCaptain.Add(v); capNeeded--; }
                }
                // Normals: each village up to 2, minus 1 if it already sent a captain.
                foreach (int v in villageOrder)
                {
                    if (normNeeded <= 0) break;
                    if (!bestNorm.ContainsKey(v)) continue;
                    int capacity = usedCaptain.Contains(v) ? 1 : 2;
                    while (capacity > 0 && normNeeded > 0)
                    {
                        AddAttack(setup, bestNorm[v]);
                        capacity--; normNeeded--;
                    }
                }
            }

            if (capNeeded > 0 || normNeeded > 0) return null; // shouldn't happen if Feasible passed

            // Displayed window = spread of the chosen attacks.
            double min = double.MaxValue, max = double.MinValue;
            foreach (TtAttack a in setup.Attacks)
            {
                if (a.Time < min) min = a.Time;
                if (a.Time > max) max = a.Time;
            }
            setup.Window = (setup.Attacks.Count > 0) ? (max - min) : 0;
            return setup;
        }

        private static void AddAttack(TtSetup setup, Candidate c)
        {
            setup.Attacks.Add(new TtAttack(c.VillageId, c.IsCaptain, c.CardType, c.Time));
        }

        private static string Signature(TtSetup setup)
        {
            List<TtAttack> copy = new List<TtAttack>(setup.Attacks);
            copy.Sort(delegate(TtAttack a, TtAttack b)
            {
                if (a.VillageId != b.VillageId) return a.VillageId.CompareTo(b.VillageId);
                if (a.IsCaptain != b.IsCaptain) return a.IsCaptain ? 1 : -1;
                if (a.CardType != b.CardType) return a.CardType.CompareTo(b.CardType);
                return a.Time.CompareTo(b.Time);
            });
            StringBuilder sb = new StringBuilder();
            foreach (TtAttack a in copy)
                sb.Append(a.VillageId).Append(a.IsCaptain ? 'C' : 'N').Append(a.CardType).Append(';');
            return sb.ToString();
        }
    }
}
