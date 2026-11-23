using System;

namespace Kingdoms.Bot
{
    /// <summary>
    /// The Goods Sender's good catalogue: every good it can share, in the order the API
    /// uses for its per-good arrays and shipment rows. Mirrors BQ_GOODS in
    /// api/banquet_planner.php — keep the two in step. Banquet goods keep indices 0..7
    /// (Banqueting.resourceLevels / Craftsmanship order); the rest were appended after them.
    /// </summary>
    internal static class SenderGoods
    {
        public const string Banquet = "banquet";
        public const string Resources = "resources";
        public const string Food = "food";
        public const string Weapons = "weapons";

        /// <summary>Category ids, in the API's default planning order.</summary>
        public static readonly string[] Categories = { Banquet, Resources, Food, Weapons };
        public static readonly string[] CategoryNames = { "Banquet", "Resources", "Food", "Weapons" };

        // Storage building each good lives in. Building types from VillageMap.getXxxLevels:
        // village hall 0, stockpile 2, granary 3, armoury 4, inn 35.
        public const string Hall = "hall";
        public const string Stockpile = "stockpile";
        public const string Granary = "granary";
        public const string Inn = "inn";
        public const string Armoury = "armoury";
        public static readonly string[] Storages = { Hall, Stockpile, Granary, Inn, Armoury };
        private static readonly int[] StorageBuildingTypes = { 0, 2, 3, 35, 4 };

        /// <summary>Good index -> game resource id (= the building type that produces it).</summary>
        public static readonly int[] ResourceIds =
        {
            22, 21, 26, 19, 33, 23, 24, 25,   // banquet: venison, furniture, metalware, clothes, wine, salt, spices, silk
            6, 7, 8, 9,                       // resources: wood, stone, iron, pitch
            13, 14, 15, 16, 17, 18, 12,       // food: apples, bread, veg, meat, cheese, fish, ale
            29, 28, 30, 31, 32                // weapons: bows, pikes, swords, armour, catapults
        };

        public static readonly string[] Names =
        {
            "Venison", "Furniture", "Metalware", "Clothes", "Wine", "Salt", "Spices", "Silk",
            "Wood", "Stone", "Iron", "Pitch",
            "Apples", "Bread", "Veg", "Meat", "Cheese", "Fish", "Ale",
            "Bows", "Pikes", "Swords", "Armour", "Catapults"
        };

        public static int Count
        {
            get { return ResourceIds.Length; }
        }

        public const int BanquetCount = 8;

        /// <summary>The good index for a game resource id, or -1 if the sender doesn't handle it.</summary>
        public static int GoodIndex(int resourceId)
        {
            for (int g = 0; g < ResourceIds.Length; g++)
                if (ResourceIds[g] == resourceId) return g;
            return -1;
        }

        public static string Category(int good)
        {
            if (good < 0) return "";
            if (good < 8) return Banquet;
            if (good < 12) return Resources;
            if (good < 19) return Food;
            if (good < 24) return Weapons;
            return "";
        }

        public static int CategoryIndex(string category)
        {
            return Array.IndexOf(Categories, category);
        }

        public static string CategoryName(string category)
        {
            int i = CategoryIndex(category);
            return i >= 0 ? CategoryNames[i] : category;
        }

        public static string Storage(int good)
        {
            if (good == 18) return Inn; // ale
            switch (Category(good))
            {
                case Banquet: return Hall;
                case Resources: return Stockpile;
                case Food: return Granary;
                case Weapons: return Armoury;
                default: return "";
            }
        }

        public static string Name(int good)
        {
            return good >= 0 && good < Names.Length ? Names[good] : "good " + good;
        }

        /// <summary>
        /// Whether a village has the building a good is stored in. Goods sent to a village
        /// without it can't be held (the game reports them as 0 there).
        /// </summary>
        public static bool HasStorage(VillageMap map, string storage)
        {
            int i = Array.IndexOf(Storages, storage);
            if (map == null || i < 0) return false;
            try { return map.findBuildingType(StorageBuildingTypes[i]) != null; }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Whether the bot reports a production rate for a good, for the API to extrapolate.
        /// Food and ale are eaten and weapons come from a queue, so a building rate would
        /// overstate them: those are reported as 0 (the API then keeps the reported level).
        /// </summary>
        public static bool ReportsProduction(int good)
        {
            string c = Category(good);
            return c == Banquet || c == Resources;
        }
    }
}
