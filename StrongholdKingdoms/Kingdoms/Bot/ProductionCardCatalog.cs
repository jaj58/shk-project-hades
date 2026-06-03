using System.Collections.Generic;

namespace Kingdoms.Bot
{
    public struct ProductionGoodDef
    {
        public string GoodKey;
        public string Section;
        public int[] CardIds;     // explicit card definition IDs, one per tier (index 0 = smallest)
        public string[] Tiers;    // tier labels, parallel to CardIds

        public ProductionGoodDef(string goodKey, string section, int[] cardIds, string[] tiers)
        {
            GoodKey = goodKey;
            Section = section;
            CardIds = cardIds;
            Tiers = tiers;
        }
    }

    public static class ProductionCardCatalog
    {
        // Card definition IDs sourced directly from CommonTypes.dll's named production-card arrays
        // (woodcutterProductionCards, appleProductionCards, etc.). Each good lists its three tiers
        // basic / advanced / expert in ascending order. Matching is done by exact card def ID via
        // CardTypes.getCardType — the cardFilter field is too coarse (all food share 3, all
        // resources share 4) to distinguish individual goods.
        public static readonly ProductionGoodDef[] Goods = new ProductionGoodDef[]
        {
            // Resources
            new ProductionGoodDef("Wood",        "Resources", new[]{769, 770, 771}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Stone",       "Resources", new[]{772, 773, 774}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Iron",        "Resources", new[]{775, 776, 777}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Pitch",       "Resources", new[]{778, 779, 780}, new[]{"x3","x5","x10"}),

            // Haul (shared booster — boosts all resources). Tiers: Haulage / Heavy / Super.
            new ProductionGoodDef("Haul",        "Haul",      new[]{781, 782, 783}, new[]{"x2","x3","x4"}),

            // Food
            new ProductionGoodDef("Apple",       "Food",      new[]{513, 514, 515}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Cheese",      "Food",      new[]{516, 517, 518}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Meat",        "Food",      new[]{519, 520, 521}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Bread",       "Food",      new[]{522, 523, 524}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Vegetables",  "Food",      new[]{528, 529, 530}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Fish",        "Food",      new[]{531, 532, 533}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Ale",         "Food",      new[]{537, 538, 539}, new[]{"x3","x5","x10"}),

            // Farmer (shared booster — boosts all food). Tiers: Veteran / Retired / Ancient.
            new ProductionGoodDef("Farmer",      "Farmer",    new[]{525, 526, 527}, new[]{"Veteran","Retired","Ancient"}),

            // Banquet
            new ProductionGoodDef("Venison",     "Banquet",   new[]{1284, 1285, 1286}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Chairs",      "Banquet",   new[]{1287, 1288, 1289}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Metalware",   "Banquet",   new[]{1290, 1291, 1292}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Clothes",     "Banquet",   new[]{1293, 1294, 1295}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Wine",        "Banquet",   new[]{1296, 1297, 1298}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Salt",        "Banquet",   new[]{1299, 1300, 1301}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Spice",       "Banquet",   new[]{1302, 1303, 1304}, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Silk",        "Banquet",   new[]{1305, 1306, 1307}, new[]{"x3","x5","x10"}),

            // Weapons (multipliers are x4 / x6 / x12, not x3/x5/x10)
            new ProductionGoodDef("Bows",        "Weapons",   new[]{1025, 1026, 1027}, new[]{"x4","x6","x12"}),
            new ProductionGoodDef("Pike",        "Weapons",   new[]{1028, 1029, 1030}, new[]{"x4","x6","x12"}),
            new ProductionGoodDef("Armour",      "Weapons",   new[]{1034, 1035, 1036}, new[]{"x4","x6","x12"}),
            new ProductionGoodDef("Swords",      "Weapons",   new[]{1031, 1032, 1033}, new[]{"x4","x6","x12"}),
            new ProductionGoodDef("Catapults",   "Weapons",   new[]{1037, 1038, 1039}, new[]{"x4","x6","x12"}),
        };

        private static Dictionary<string, ProductionGoodDef> _byKey;

        public static bool TryGetDef(string goodKey, out ProductionGoodDef def)
        {
            if (_byKey == null)
            {
                _byKey = new Dictionary<string, ProductionGoodDef>();
                foreach (ProductionGoodDef g in Goods)
                    _byKey[g.GoodKey] = g;
            }
            return _byKey.TryGetValue(goodKey, out def);
        }

        /// <summary>Returns the card definition ID for the given good + tier, or 0 if not found.</summary>
        public static int GetCardId(string goodKey, int tierIndex)
        {
            ProductionGoodDef def;
            if (TryGetDef(goodKey, out def) && def.CardIds != null
                && tierIndex >= 0 && tierIndex < def.CardIds.Length)
                return def.CardIds[tierIndex];
            return 0;
        }
    }
}
