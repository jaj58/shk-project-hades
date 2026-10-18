using System;

namespace Kingdoms.Bot
{
    /// <summary>Where a Village Overview row's stats came from, best first.</summary>
    internal enum OverviewSource
    {
        None = 0,
        Premium = 1,    // the premium-token PremiumOverview batch
        Snapshot = 2,   // a VillageBuildingChangeRates reply for this village
        Live = 3,       // the client's own downloaded VillageMap
    }

    /// <summary>
    /// One village's numbers for the Village Overview grid. Everything numeric is a double
    /// so the column table can sort, total and format every column the same way; NaN means
    /// "not known for this village" (for example production for a village that was never
    /// downloaded) and shows as a dash.
    /// </summary>
    internal class VillageOverviewRow
    {
        // Building type ids double as resource ids (wood = 6 ... wine = 33).
        public const int ResourceSlots = 34;

        public const int ResWood = 6, ResStone = 7, ResIron = 8, ResPitch = 9;
        public const int ResAle = 12, ResApples = 13, ResBread = 14, ResVeg = 15;
        public const int ResMeat = 16, ResCheese = 17, ResFish = 18, ResClothes = 19;
        public const int ResFurniture = 21, ResVenison = 22, ResSalt = 23, ResSpices = 24;
        public const int ResSilk = 25, ResMetalware = 26, ResPikes = 28, ResBows = 29;
        public const int ResSwords = 30, ResArmour = 31, ResCatapults = 32, ResWine = 33;

        public static readonly int[] FoodTypes = { ResApples, ResBread, ResVeg, ResMeat, ResCheese, ResFish };

        public int VillageId;
        public string Name = "";
        public bool IsCapital;
        public string Parish = "";

        public OverviewSource Source;
        public bool Downloaded;
        // Server time the stats describe; MinValue when there are none.
        public DateTime DataTime = DateTime.MinValue;
        public bool RequestPending;

        public readonly double[] Level = new double[ResourceSlots];
        public readonly int[] Cap = new int[ResourceSlots];
        public readonly double[] ProdPerDay = new double[ResourceSlots];
        // Weapons still to come out of a production batch already paid for.
        public readonly double[] InProduction = new double[ResourceSlots];

        public double FoodLevel, FoodCap, FoodProdPerDay, FoodConsPerDay;
        public double AleConsPerDay;

        public double Peasants, Archers, Pikemen, Swordsmen, Catapults, Captains, ScoutsHome;
        public bool CaptainInTraining;
        public double TroopsOut, CaptainsOut, Reinforcing, Placed, Stationed, ScoutsOut;
        public double UnitsUsed, UnitsCap;

        public double Popularity, Tax, RationsSet, RationsEffective, AleSet, AleEffective;
        public double GoldPerDay, People, Housing, Spare;
        public double KeepEnclosed, CastleDamaged, Buildings, BuildQueue;
        public DateTime InterdictEnd = DateTime.MinValue;
        public DateTime PeaceEnd = DateTime.MinValue;
        public DateTime ExcomEnd = DateTime.MinValue;

        public double TradersHome, TradersOut, MonksHome, MonksTotal;
        public double Incoming;
        public DateTime NextIncoming = DateTime.MinValue;

        public VillageOverviewRow(int villageId)
        {
            VillageId = villageId;
            Clear();
        }

        /// <summary>Forgets every value, ready for the builder to fill the row again.</summary>
        public void Clear()
        {
            Source = OverviewSource.None;
            Downloaded = false;
            DataTime = DateTime.MinValue;

            for (int i = 0; i < ResourceSlots; i++)
            {
                Level[i] = double.NaN;
                Cap[i] = 0;
                ProdPerDay[i] = double.NaN;
                InProduction[i] = 0.0;
            }

            FoodLevel = FoodCap = FoodProdPerDay = FoodConsPerDay = double.NaN;
            AleConsPerDay = double.NaN;

            Peasants = Archers = Pikemen = Swordsmen = Catapults = Captains = ScoutsHome = double.NaN;
            CaptainInTraining = false;
            TroopsOut = CaptainsOut = Reinforcing = Placed = Stationed = ScoutsOut = double.NaN;
            UnitsUsed = UnitsCap = double.NaN;

            Popularity = Tax = RationsSet = RationsEffective = AleSet = AleEffective = double.NaN;
            GoldPerDay = People = Housing = Spare = double.NaN;
            KeepEnclosed = CastleDamaged = Buildings = BuildQueue = double.NaN;
            InterdictEnd = PeaceEnd = ExcomEnd = DateTime.MinValue;

            TradersHome = TradersOut = MonksHome = MonksTotal = double.NaN;
            Incoming = double.NaN;
            NextIncoming = DateTime.MinValue;
        }

        public bool HasStats
        {
            get { return Source != OverviewSource.None; }
        }

        public double TradersTotal
        {
            get { return Sum(TradersHome, TradersOut); }
        }

        public double ScoutsTotal
        {
            get { return Sum(ScoutsHome, ScoutsOut); }
        }

        public double TroopsHome
        {
            get { return Sum(Sum(Sum(Peasants, Archers), Sum(Pikemen, Swordsmen)), Catapults); }
        }

        // NaN only when both sides are unknown - a known count plus an unknown one is still
        // the best figure there is.
        public static double Sum(double a, double b)
        {
            if (double.IsNaN(a)) return b;
            if (double.IsNaN(b)) return a;
            return a + b;
        }

        public static bool IsUnset(DateTime when)
        {
            return when == DateTime.MinValue || when.Year < 2000;
        }
    }
}
