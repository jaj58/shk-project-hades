using System;
using System.Collections.Generic;
using System.Drawing;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot
{
    /// <summary>The Village Overview's column groups, each a toggle above the grid.</summary>
    internal enum OverviewGroup
    {
        Info = 0,        // always shown
        Resources = 1,
        Military = 2,
        Health = 3,
        Units = 4,
    }

    /// <summary>
    /// One Village Overview column. The grid, sorting, the totals row, copy-to-clipboard and
    /// the column menu all read from these, so a column is defined once here and nowhere
    /// else. Value doubles as the sort key and the total; NaN means unknown.
    /// </summary>
    internal class OverviewColumn
    {
        public string Key;
        public string Header;
        public string HeaderTip;
        public OverviewGroup Group;
        public int Width = 62;
        public bool LeftAlign;
        public bool Summable;
        public bool HiddenByDefault;

        public Func<VillageOverviewRow, double> Value;
        // Text columns sort on this instead of Value.
        public Func<VillageOverviewRow, string> SortText;
        public Func<VillageOverviewRow, DateTime, string> Text;
        public Func<VillageOverviewRow, DateTime, Color> Colour;
        public Func<VillageOverviewRow, DateTime, string> CellTip;

        public string FormatValue(VillageOverviewRow row, DateTime now)
        {
            if (Text != null) return Text(row, now);
            if (Value == null) return "";
            return VillageOverviewColumns.FormatNumber(Value(row));
        }
    }

    internal static class VillageOverviewColumns
    {
        public static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        public static readonly Color TextDim = Color.FromArgb(120, 124, 138);
        public static readonly Color Warn = Color.FromArgb(255, 180, 50);
        public static readonly Color Bad = Color.FromArgb(240, 80, 80);
        public static readonly Color Good = Color.FromArgb(80, 200, 120);

        public const string Dash = "—";

        private static List<OverviewColumn> _all;

        public static List<OverviewColumn> All
        {
            get
            {
                if (_all == null) _all = Build();
                return _all;
            }
        }

        public static string GroupName(OverviewGroup group)
        {
            switch (group)
            {
                case OverviewGroup.Resources: return "Resources";
                case OverviewGroup.Military: return "Military";
                case OverviewGroup.Health: return "Village";
                case OverviewGroup.Units: return "Units & incoming";
                default: return "Info";
            }
        }

        public static string FormatNumber(double value)
        {
            if (double.IsNaN(value)) return Dash;
            return ((long)Math.Floor(value)).ToString("N0");
        }

        public static string FormatSpan(double seconds)
        {
            if (double.IsNaN(seconds) || double.IsInfinity(seconds)) return Dash;
            if (seconds > 99 * 86400.0) return "99d+";
            return Kingdoms.Bot.UI.VillageStatsRow.FormatSpan(TimeSpan.FromSeconds(seconds));
        }

        private static List<OverviewColumn> Build()
        {
            List<OverviewColumn> c = new List<OverviewColumn>();

            // ---------------- Info ----------------
            OverviewColumn name = Col(c, "name", "Village", OverviewGroup.Info, 150);
            name.LeftAlign = true;
            name.SortText = delegate(VillageOverviewRow r) { return r.Name; };
            name.Text = delegate(VillageOverviewRow r, DateTime now) { return r.Name; };
            name.CellTip = delegate(VillageOverviewRow r, DateTime now)
            {
                return "[" + r.VillageId + "] " + r.Name + "\nDouble-click to show it on the map, right-click for more.";
            };

            OverviewColumn id = Col(c, "id", "ID", OverviewGroup.Info, 56);
            id.HiddenByDefault = true;
            id.Value = delegate(VillageOverviewRow r) { return r.VillageId; };
            id.Text = delegate(VillageOverviewRow r, DateTime now) { return r.VillageId.ToString(); };

            OverviewColumn type = Col(c, "type", "Type", OverviewGroup.Info, 56);
            type.LeftAlign = true;
            type.SortText = delegate(VillageOverviewRow r) { return r.IsCapital ? "Capital" : "Village"; };
            type.Text = delegate(VillageOverviewRow r, DateTime now) { return r.IsCapital ? "Capital" : "Village"; };

            OverviewColumn parish = Col(c, "parish", "Parish", OverviewGroup.Info, 110);
            parish.LeftAlign = true;
            parish.SortText = delegate(VillageOverviewRow r) { return r.Parish; };
            parish.Text = delegate(VillageOverviewRow r, DateTime now) { return r.Parish; };

            OverviewColumn data = Col(c, "data", "Data", OverviewGroup.Info, 58);
            data.HeaderTip = "Where the row's numbers come from: Live = downloaded village, an age = last server reply, P = premium overview batch.";
            data.Value = DataAge;
            data.Text = delegate(VillageOverviewRow r, DateTime now)
            {
                string text;
                switch (r.Source)
                {
                    case OverviewSource.Live: text = "Live"; break;
                    case OverviewSource.Snapshot: text = ShortAge(now - r.DataTime); break;
                    case OverviewSource.Premium: text = "P " + ShortAge(now - r.DataTime); break;
                    default: text = Dash; break;
                }
                return r.RequestPending ? text + " …" : text;
            };
            data.Colour = delegate(VillageOverviewRow r, DateTime now)
            {
                if (r.Source == OverviewSource.Live) return Good;
                if (r.Source == OverviewSource.None) return TextDim;
                return (now - r.DataTime).TotalMinutes > 15 ? Warn : TextPri;
            };
            data.CellTip = delegate(VillageOverviewRow r, DateTime now)
            {
                string tip;
                switch (r.Source)
                {
                    case OverviewSource.Live: tip = "Downloaded village - live values from the client."; break;
                    case OverviewSource.Snapshot: tip = "Server snapshot from " + FormatSpan((now - r.DataTime).TotalSeconds) + " ago. Production and building columns need the village downloaded."; break;
                    case OverviewSource.Premium: tip = "Premium overview batch from " + FormatSpan((now - r.DataTime).TotalSeconds) + " ago."; break;
                    default: tip = "No data yet - waiting for a server reply."; break;
                }
                return r.RequestPending ? tip + "\nA refresh is on its way." : tip;
            };

            // ---------------- Resources ----------------
            Res(c, "wood", "Wood", VillageOverviewRow.ResWood);
            Res(c, "stone", "Stone", VillageOverviewRow.ResStone);
            Res(c, "iron", "Iron", VillageOverviewRow.ResIron);
            Res(c, "pitch", "Pitch", VillageOverviewRow.ResPitch);

            OverviewColumn nextFull = Col(c, "nextfull", "Next full", OverviewGroup.Resources, 92);
            nextFull.HeaderTip = "The first of wood, stone, iron and pitch to fill its stockpile, at the current production rate. Downloaded villages only.";
            nextFull.Value = delegate(VillageOverviewRow r) { int t; return NextFull(r, out t); };
            nextFull.Text = delegate(VillageOverviewRow r, DateTime now)
            {
                int t;
                double seconds = NextFull(r, out t);
                if (double.IsNaN(seconds)) return Dash;
                if (seconds <= 0) return ResourceName(t) + " full";
                return ResourceName(t) + " " + FormatSpan(seconds);
            };
            nextFull.Colour = delegate(VillageOverviewRow r, DateTime now)
            {
                int t;
                double seconds = NextFull(r, out t);
                if (double.IsNaN(seconds)) return TextDim;
                if (seconds <= 0) return Bad;
                return seconds < 3 * 3600 ? Warn : TextPri;
            };
            nextFull.CellTip = delegate(VillageOverviewRow r, DateTime now)
            {
                return r.Downloaded ? "" : "Needs the village downloaded (production rates).";
            };

            OverviewColumn foodCol = Col(c, "food", "Food", OverviewGroup.Resources, 66);
            foodCol.Summable = true;
            foodCol.HeaderTip = "All six food types together, against the granary's combined capacity.";
            foodCol.Value = delegate(VillageOverviewRow r) { return r.FoodLevel; };
            foodCol.Colour = delegate(VillageOverviewRow r, DateTime now) { return FillColour(r.FoodLevel, r.FoodCap); };
            foodCol.CellTip = delegate(VillageOverviewRow r, DateTime now) { return FillTip(r.FoodLevel, r.FoodCap, double.NaN); };

            OverviewColumn foodDay = Col(c, "foodday", "Food/day", OverviewGroup.Resources, 66);
            foodDay.Summable = true;
            foodDay.HeaderTip = "Food produced minus food eaten per day. Downloaded villages only.";
            foodDay.Value = delegate(VillageOverviewRow r) { return Net(r.FoodProdPerDay, r.FoodConsPerDay); };
            foodDay.Text = delegate(VillageOverviewRow r, DateTime now) { return Signed(Net(r.FoodProdPerDay, r.FoodConsPerDay)); };
            foodDay.Colour = delegate(VillageOverviewRow r, DateTime now) { return NetColour(Net(r.FoodProdPerDay, r.FoodConsPerDay)); };
            foodDay.CellTip = delegate(VillageOverviewRow r, DateTime now)
            {
                return "Produced " + FormatNumber(r.FoodProdPerDay) + ", eaten " + FormatNumber(r.FoodConsPerDay) + " per day";
            };

            OverviewColumn foodLasts = Col(c, "foodlasts", "Food lasts", OverviewGroup.Resources, 70);
            foodLasts.HeaderTip = "How long the food lasts when the village eats more than it makes.";
            foodLasts.Value = delegate(VillageOverviewRow r) { return Lasts(r.FoodLevel, Net(r.FoodProdPerDay, r.FoodConsPerDay)); };
            foodLasts.Text = delegate(VillageOverviewRow r, DateTime now) { return FormatSpan(Lasts(r.FoodLevel, Net(r.FoodProdPerDay, r.FoodConsPerDay))); };
            foodLasts.Colour = delegate(VillageOverviewRow r, DateTime now) { return LastsColour(Lasts(r.FoodLevel, Net(r.FoodProdPerDay, r.FoodConsPerDay))); };

            Res(c, "ale", "Ale", VillageOverviewRow.ResAle);

            OverviewColumn aleDay = Col(c, "aleday", "Ale/day", OverviewGroup.Resources, 62);
            aleDay.Summable = true;
            aleDay.HeaderTip = "Ale brewed minus ale drunk per day. Downloaded villages only.";
            aleDay.Value = delegate(VillageOverviewRow r) { return Net(r.ProdPerDay[VillageOverviewRow.ResAle], r.AleConsPerDay); };
            aleDay.Text = delegate(VillageOverviewRow r, DateTime now) { return Signed(Net(r.ProdPerDay[VillageOverviewRow.ResAle], r.AleConsPerDay)); };
            aleDay.Colour = delegate(VillageOverviewRow r, DateTime now) { return NetColour(Net(r.ProdPerDay[VillageOverviewRow.ResAle], r.AleConsPerDay)); };

            Res(c, "venison", "Venison", VillageOverviewRow.ResVenison);
            Res(c, "furniture", "Furniture", VillageOverviewRow.ResFurniture);
            Res(c, "metalware", "Metal", VillageOverviewRow.ResMetalware);
            Res(c, "clothes", "Clothes", VillageOverviewRow.ResClothes);
            Res(c, "wine", "Wine", VillageOverviewRow.ResWine);
            Res(c, "salt", "Salt", VillageOverviewRow.ResSalt);
            Res(c, "spices", "Spices", VillageOverviewRow.ResSpices);
            Res(c, "silk", "Silk", VillageOverviewRow.ResSilk);

            // ---------------- Military ----------------
            Count(c, "peasants", "Peasants", OverviewGroup.Military, delegate(VillageOverviewRow r) { return r.Peasants; }, "At home");
            Count(c, "archers", "Archers", OverviewGroup.Military, delegate(VillageOverviewRow r) { return r.Archers; }, "At home");
            Count(c, "pikemen", "Pikemen", OverviewGroup.Military, delegate(VillageOverviewRow r) { return r.Pikemen; }, "At home");
            Count(c, "swordsmen", "Swords", OverviewGroup.Military, delegate(VillageOverviewRow r) { return r.Swordsmen; }, "Swordsmen at home");
            Count(c, "catapults", "Catapults", OverviewGroup.Military, delegate(VillageOverviewRow r) { return r.Catapults; }, "At home");

            OverviewColumn captains = Count(c, "captains", "Captains", OverviewGroup.Military, delegate(VillageOverviewRow r) { return r.Captains; }, "At home");
            captains.CellTip = delegate(VillageOverviewRow r, DateTime now) { return r.CaptainInTraining ? "One more in training" : ""; };
            captains.Text = delegate(VillageOverviewRow r, DateTime now)
            {
                return FormatNumber(r.Captains) + (r.CaptainInTraining ? "+" : "");
            };

            Count(c, "out", "Out", OverviewGroup.Military, delegate(VillageOverviewRow r) { return VillageOverviewRow.Sum(r.TroopsOut, r.CaptainsOut); }, "Troops and captains away in armies (attacking or coming home)");
            Count(c, "reinf", "Reinf", OverviewGroup.Military, delegate(VillageOverviewRow r) { return r.Reinforcing; }, "Troops sent to reinforce other villages");
            OverviewColumn placed = Count(c, "placed", "Castle", OverviewGroup.Military, delegate(VillageOverviewRow r) { return r.Placed; }, "Troops placed in the castle - needs a premium token or the castle loaded");
            placed.CellTip = delegate(VillageOverviewRow r, DateTime now)
            {
                return double.IsNaN(r.Placed) ? "Needs a premium token or the castle loaded." : "";
            };
            Count(c, "stationed", "Stationed", OverviewGroup.Military, delegate(VillageOverviewRow r) { return r.Stationed; }, "Other players' troops stationed here");

            Weapon(c, "bows", "Bows", VillageOverviewRow.ResBows);
            Weapon(c, "pikes", "Pikes", VillageOverviewRow.ResPikes);
            Weapon(c, "swordswpn", "Swords (w)", VillageOverviewRow.ResSwords);
            Weapon(c, "armour", "Armour", VillageOverviewRow.ResArmour);
            Weapon(c, "catswpn", "Cats (w)", VillageOverviewRow.ResCatapults);

            OverviewColumn units = Col(c, "units", "Units", OverviewGroup.Military, 86);
            units.Summable = true;
            units.HeaderTip = "Unit capacity used (troops, scouts, traders and monks) out of the village's limit.";
            units.Value = delegate(VillageOverviewRow r) { return r.UnitsUsed; };
            units.Text = delegate(VillageOverviewRow r, DateTime now)
            {
                if (double.IsNaN(r.UnitsUsed)) return Dash;
                return FormatNumber(r.UnitsUsed) + " / " + FormatNumber(r.UnitsCap);
            };
            units.Colour = delegate(VillageOverviewRow r, DateTime now) { return FillColour(r.UnitsUsed, r.UnitsCap); };
            units.CellTip = delegate(VillageOverviewRow r, DateTime now)
            {
                return double.IsNaN(r.Placed) && !double.IsNaN(r.UnitsUsed)
                    ? "Castle troops aren't known for this village, so this may be low." : "";
            };

            // ---------------- Village ----------------
            OverviewColumn pop = Col(c, "popularity", "Popularity", OverviewGroup.Health, 66);
            pop.Value = delegate(VillageOverviewRow r) { return r.Popularity; };
            pop.Colour = delegate(VillageOverviewRow r, DateTime now)
            {
                if (double.IsNaN(r.Popularity)) return TextDim;
                return r.Popularity < 0 ? Bad : (r.Popularity < 20 ? Warn : TextPri);
            };

            Count(c, "tax", "Tax", OverviewGroup.Health, delegate(VillageOverviewRow r) { return r.Tax; }, "Tax level").Summable = false;

            OverviewColumn rations = Col(c, "rations", "Rations", OverviewGroup.Health, 56);
            rations.Value = delegate(VillageOverviewRow r) { return r.RationsEffective; };
            rations.Text = delegate(VillageOverviewRow r, DateTime now) { return RationsText(r.RationsEffective); };
            rations.Colour = delegate(VillageOverviewRow r, DateTime now) { return Mismatch(r.RationsSet, r.RationsEffective) ? Bad : TextPri; };
            rations.CellTip = delegate(VillageOverviewRow r, DateTime now)
            {
                return Mismatch(r.RationsSet, r.RationsEffective)
                    ? "Set to " + RationsText(r.RationsSet) + " but only " + RationsText(r.RationsEffective) + " is being served" : "";
            };

            OverviewColumn ale = Col(c, "alerations", "Ale rat.", OverviewGroup.Health, 56);
            ale.Value = delegate(VillageOverviewRow r) { return r.AleEffective; };
            ale.Text = delegate(VillageOverviewRow r, DateTime now) { return AleText(r.AleEffective); };
            ale.Colour = delegate(VillageOverviewRow r, DateTime now) { return Mismatch(r.AleSet, r.AleEffective) ? Bad : TextPri; };
            ale.CellTip = delegate(VillageOverviewRow r, DateTime now)
            {
                return Mismatch(r.AleSet, r.AleEffective)
                    ? "Set to " + AleText(r.AleSet) + " but only " + AleText(r.AleEffective) + " is being served" : "";
            };

            OverviewColumn gold = Col(c, "gold", "Gold/day", OverviewGroup.Health, 70);
            gold.Summable = true;
            gold.Value = delegate(VillageOverviewRow r) { return r.GoldPerDay; };
            gold.Text = delegate(VillageOverviewRow r, DateTime now) { return Signed(r.GoldPerDay); };

            OverviewColumn people = Col(c, "people", "People", OverviewGroup.Health, 80);
            people.Summable = true;
            people.HeaderTip = "Peasants living in the village, out of the housing they have.";
            people.Value = delegate(VillageOverviewRow r) { return r.People; };
            people.Text = delegate(VillageOverviewRow r, DateTime now)
            {
                if (double.IsNaN(r.People)) return Dash;
                return FormatNumber(r.People) + " / " + FormatNumber(r.Housing);
            };
            people.Colour = delegate(VillageOverviewRow r, DateTime now)
            {
                return !double.IsNaN(r.People) && !double.IsNaN(r.Housing) && r.People > r.Housing ? Warn : TextPri;
            };

            Count(c, "spare", "Spare", OverviewGroup.Health, delegate(VillageOverviewRow r) { return r.Spare; }, "Spare workers");

            OverviewColumn keep = Col(c, "keep", "Keep", OverviewGroup.Health, 66);
            keep.HeaderTip = "Whether the castle keep is enclosed by walls.";
            keep.Value = delegate(VillageOverviewRow r) { return r.KeepEnclosed; };
            keep.Text = delegate(VillageOverviewRow r, DateTime now)
            {
                if (double.IsNaN(r.KeepEnclosed)) return Dash;
                return r.KeepEnclosed > 0 ? "Enclosed" : "Open";
            };
            keep.Colour = delegate(VillageOverviewRow r, DateTime now) { return r.KeepEnclosed == 0 ? Warn : TextPri; };

            OverviewColumn damaged = Col(c, "damaged", "Damaged", OverviewGroup.Health, 62);
            damaged.HeaderTip = "Castle damage - needs a premium token or the castle loaded.";
            damaged.Value = delegate(VillageOverviewRow r) { return r.CastleDamaged; };
            damaged.Text = delegate(VillageOverviewRow r, DateTime now)
            {
                if (double.IsNaN(r.CastleDamaged)) return Dash;
                return r.CastleDamaged > 0 ? "Damaged" : "OK";
            };
            damaged.Colour = delegate(VillageOverviewRow r, DateTime now) { return r.CastleDamaged > 0 ? Bad : TextPri; };

            OverviewColumn buildings = Count(c, "buildings", "Buildings", OverviewGroup.Health, delegate(VillageOverviewRow r) { return r.Buildings; }, "Buildings in the village - downloaded villages or premium only");
            buildings.Summable = false;
            OverviewColumn queue = Count(c, "queue", "Queue", OverviewGroup.Health, delegate(VillageOverviewRow r) { return r.BuildQueue; }, "Buildings under construction - downloaded villages only");
            queue.Summable = false;

            Countdown(c, "interdict", "Interdict", delegate(VillageOverviewRow r) { return r.InterdictEnd; }, "Interdiction protection left");
            Countdown(c, "peace", "Peace", delegate(VillageOverviewRow r) { return r.PeaceEnd; }, "Peace time left");
            OverviewColumn excom = Countdown(c, "excom", "Excom", delegate(VillageOverviewRow r) { return r.ExcomEnd; }, "Excommunication left");
            excom.Colour = delegate(VillageOverviewRow r, DateTime now) { return Remaining(r.ExcomEnd, now) > 0 ? Bad : TextPri; };

            // ---------------- Units & incoming ----------------
            OverviewColumn traders = Col(c, "traders", "Traders", OverviewGroup.Units, 70);
            traders.Summable = true;
            traders.HeaderTip = "Traders at home / total.";
            traders.Value = delegate(VillageOverviewRow r) { return r.TradersHome; };
            traders.Text = delegate(VillageOverviewRow r, DateTime now) { return Pair(r.TradersHome, r.TradersTotal); };

            Count(c, "tradersout", "Trading", OverviewGroup.Units, delegate(VillageOverviewRow r) { return r.TradersOut; }, "Traders out on the road");

            OverviewColumn monks = Col(c, "monks", "Monks", OverviewGroup.Units, 62);
            monks.Summable = true;
            monks.HeaderTip = "Monks at home / total.";
            monks.Value = delegate(VillageOverviewRow r) { return r.MonksHome; };
            monks.Text = delegate(VillageOverviewRow r, DateTime now) { return Pair(r.MonksHome, r.MonksTotal); };

            OverviewColumn scouts = Col(c, "scouts", "Scouts", OverviewGroup.Units, 62);
            scouts.Summable = true;
            scouts.HeaderTip = "Scouts at home / total.";
            scouts.Value = delegate(VillageOverviewRow r) { return r.ScoutsHome; };
            scouts.Text = delegate(VillageOverviewRow r, DateTime now) { return Pair(r.ScoutsHome, r.ScoutsTotal); };

            OverviewColumn incoming = Count(c, "incoming", "Incoming", OverviewGroup.Units, delegate(VillageOverviewRow r) { return r.Incoming; }, "Hostile armies on their way to this village");
            incoming.Colour = delegate(VillageOverviewRow r, DateTime now) { return r.Incoming > 0 ? Bad : TextDim; };

            OverviewColumn nextHit = Col(c, "nexthit", "Next hit", OverviewGroup.Units, 70);
            nextHit.HeaderTip = "When the first incoming army lands.";
            nextHit.Value = delegate(VillageOverviewRow r) { return RemainingOrNaN(r.NextIncoming, VillageMap.getCurrentServerTime()); };
            nextHit.Text = delegate(VillageOverviewRow r, DateTime now) { return FormatSpan(RemainingOrNaN(r.NextIncoming, now)); };
            nextHit.Colour = delegate(VillageOverviewRow r, DateTime now)
            {
                double left = RemainingOrNaN(r.NextIncoming, now);
                return double.IsNaN(left) ? TextDim : (left < 3600 ? Bad : Warn);
            };

            return c;
        }

        // =================================================================
        // Column builders
        // =================================================================

        private static OverviewColumn Col(List<OverviewColumn> list, string key, string header,
            OverviewGroup group, int width)
        {
            OverviewColumn col = new OverviewColumn();
            col.Key = key;
            col.Header = header;
            col.Group = group;
            col.Width = width;
            list.Add(col);
            return col;
        }

        private static OverviewColumn Count(List<OverviewColumn> list, string key, string header,
            OverviewGroup group, Func<VillageOverviewRow, double> value, string tip)
        {
            OverviewColumn col = Col(list, key, header, group, 62);
            col.Summable = true;
            col.HeaderTip = tip;
            col.Value = value;
            return col;
        }

        private static OverviewColumn Res(List<OverviewColumn> list, string key, string header, int resourceId)
        {
            int t = resourceId;
            OverviewColumn col = Col(list, key, header, OverviewGroup.Resources, 62);
            col.Summable = true;
            col.Value = delegate(VillageOverviewRow r) { return r.Level[t]; };
            col.Colour = delegate(VillageOverviewRow r, DateTime now) { return FillColour(r.Level[t], r.Cap[t]); };
            col.CellTip = delegate(VillageOverviewRow r, DateTime now) { return FillTip(r.Level[t], r.Cap[t], r.ProdPerDay[t]); };
            return col;
        }

        private static OverviewColumn Weapon(List<OverviewColumn> list, string key, string header, int resourceId)
        {
            int t = resourceId;
            OverviewColumn col = Res(list, key, header, resourceId);
            col.Group = OverviewGroup.Military;
            col.HeaderTip = ResourceName(t) + " in the armoury, including what has been made of a batch in production.";
            col.Text = delegate(VillageOverviewRow r, DateTime now)
            {
                string text = FormatNumber(r.Level[t]);
                return r.InProduction[t] > 0 ? text + "+" : text;
            };
            col.CellTip = delegate(VillageOverviewRow r, DateTime now)
            {
                string tip = FillTip(r.Level[t], r.Cap[t], double.NaN);
                if (r.InProduction[t] > 0) tip += "\n" + FormatNumber(r.InProduction[t]) + " more still being made";
                return tip;
            };
            return col;
        }

        private static OverviewColumn Countdown(List<OverviewColumn> list, string key, string header,
            Func<VillageOverviewRow, DateTime> when, string tip)
        {
            OverviewColumn col = Col(list, key, header, OverviewGroup.Health, 66);
            col.HeaderTip = tip;
            col.Value = delegate(VillageOverviewRow r) { return RemainingOrNaN(when(r), VillageMap.getCurrentServerTime()); };
            col.Text = delegate(VillageOverviewRow r, DateTime now) { return FormatSpan(RemainingOrNaN(when(r), now)); };
            col.Colour = delegate(VillageOverviewRow r, DateTime now) { return double.IsNaN(RemainingOrNaN(when(r), now)) ? TextDim : TextPri; };
            return col;
        }

        // =================================================================
        // Helpers
        // =================================================================

        private static double DataAge(VillageOverviewRow r)
        {
            if (r.Source == OverviewSource.Live) return 0;
            if (r.Source == OverviewSource.None) return double.NaN;
            return (VillageMap.getCurrentServerTime() - r.DataTime).TotalSeconds;
        }

        private static string ShortAge(TimeSpan age)
        {
            double seconds = age.TotalSeconds;
            if (seconds < 60) return "now";
            if (seconds < 3600) return ((int)(seconds / 60)) + "m";
            if (seconds < 86400) return ((int)(seconds / 3600)) + "h";
            return ((int)(seconds / 86400)) + "d";
        }

        private static Color FillColour(double level, double cap)
        {
            if (double.IsNaN(level)) return TextDim;
            if (double.IsNaN(cap) || cap <= 0) return TextPri;
            double ratio = level / cap;
            if (ratio >= 0.95) return Bad;
            if (ratio >= 0.70) return Warn;
            return TextPri;
        }

        private static string FillTip(double level, double cap, double perDay)
        {
            if (double.IsNaN(level)) return "Not known yet";
            string tip = FormatNumber(level);
            if (!double.IsNaN(cap) && cap > 0)
                tip += " of " + FormatNumber(cap) + " (" + ((int)(level * 100.0 / cap)) + "%)";
            if (!double.IsNaN(perDay))
                tip += "\n+" + FormatNumber(perDay) + " per day";
            return tip;
        }

        // Seconds until the first stockpile resource fills; 0 if one already has, NaN when
        // the production rates aren't known.
        private static double NextFull(VillageOverviewRow r, out int resource)
        {
            resource = VillageOverviewRow.ResWood;
            double best = double.NaN;
            int[] types = { VillageOverviewRow.ResWood, VillageOverviewRow.ResStone,
                VillageOverviewRow.ResIron, VillageOverviewRow.ResPitch };

            for (int i = 0; i < types.Length; i++)
            {
                int t = types[i];
                double level = r.Level[t];
                int cap = r.Cap[t];
                if (double.IsNaN(level) || cap <= 0) continue;

                double seconds;
                if (level >= cap - 0.5)
                {
                    // Production stops at the cap, so a full stockpile reads as a zero rate -
                    // check fullness before the rate.
                    seconds = 0;
                }
                else
                {
                    double perDay = r.ProdPerDay[t];
                    if (double.IsNaN(perDay) || perDay <= 0) continue;
                    seconds = (cap - level) / perDay * 86400.0;
                }

                if (double.IsNaN(best) || seconds < best)
                {
                    best = seconds;
                    resource = t;
                }
            }

            return best;
        }

        private static double Net(double produced, double consumed)
        {
            if (double.IsNaN(produced) || double.IsNaN(consumed)) return double.NaN;
            return produced - consumed;
        }

        // Seconds a stock lasts at a falling net rate; NaN when it isn't falling.
        private static double Lasts(double level, double netPerDay)
        {
            if (double.IsNaN(level) || double.IsNaN(netPerDay) || netPerDay >= 0) return double.NaN;
            return level / -netPerDay * 86400.0;
        }

        private static Color LastsColour(double seconds)
        {
            if (double.IsNaN(seconds)) return TextDim;
            if (seconds < 12 * 3600) return Bad;
            if (seconds < 48 * 3600) return Warn;
            return TextPri;
        }

        private static string Signed(double value)
        {
            if (double.IsNaN(value)) return Dash;
            return (value >= 0 ? "+" : "-") + FormatNumber(Math.Abs(value));
        }

        private static Color NetColour(double value)
        {
            if (double.IsNaN(value)) return TextDim;
            return value < 0 ? Bad : TextPri;
        }

        private static string Pair(double home, double total)
        {
            if (double.IsNaN(home) && double.IsNaN(total)) return Dash;
            return FormatNumber(home) + " / " + FormatNumber(total);
        }

        private static bool Mismatch(double set, double effective)
        {
            if (double.IsNaN(set) || double.IsNaN(effective)) return false;
            return Math.Abs(set - effective) > 0.01;
        }

        // The premium overview's labels.
        private static string RationsText(double level)
        {
            if (double.IsNaN(level)) return Dash;
            if (level < 1.0) return "0";
            if (level < 2.0) return "1/4";
            if (level < 3.0) return "1/2";
            if (level < 4.0) return "x1";
            if (level < 5.0) return "x2";
            if (level < 6.0) return "x3";
            return "x4";
        }

        private static string AleText(double level)
        {
            if (double.IsNaN(level)) return Dash;
            if (level < 1.0) return "0";
            if (level < 2.0) return "x1";
            if (level < 3.0) return "x2";
            if (level < 4.0) return "x3";
            return "x4";
        }

        private static double Remaining(DateTime when, DateTime now)
        {
            if (VillageOverviewRow.IsUnset(when)) return 0;
            return (when - now).TotalSeconds;
        }

        private static double RemainingOrNaN(DateTime when, DateTime now)
        {
            double left = Remaining(when, now);
            return left > 0 ? left : double.NaN;
        }

        public static string ResourceName(int resourceId)
        {
            string name = null;
            try { name = TradeModuleConstants.GetResourceName(resourceId); }
            catch (Exception) { }
            if (!string.IsNullOrEmpty(name)) return name;

            switch (resourceId)
            {
                case VillageOverviewRow.ResWood: return "Wood";
                case VillageOverviewRow.ResStone: return "Stone";
                case VillageOverviewRow.ResIron: return "Iron";
                case VillageOverviewRow.ResPitch: return "Pitch";
                case VillageOverviewRow.ResAle: return "Ale";
                case VillageOverviewRow.ResBows: return "Bows";
                case VillageOverviewRow.ResPikes: return "Pikes";
                case VillageOverviewRow.ResSwords: return "Swords";
                case VillageOverviewRow.ResArmour: return "Armour";
                case VillageOverviewRow.ResCatapults: return "Catapults";
                default: return "Resource " + resourceId;
            }
        }
    }
}
