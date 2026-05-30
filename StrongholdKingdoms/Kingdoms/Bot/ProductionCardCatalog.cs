using System.Collections.Generic;

namespace Kingdoms.Bot
{
    public struct ProductionGoodDef
    {
        public string GoodKey;
        public string Section;
        public int FilterId;
        public string[] Tiers;

        public ProductionGoodDef(string goodKey, string section, int filterId, string[] tiers)
        {
            GoodKey = goodKey;
            Section = section;
            FilterId = filterId;
            Tiers = tiers;
        }
    }

    public static class ProductionCardCatalog
    {
        // Card filter IDs sourced from ManageCardsPanel.cs addFilterButton calls.
        // Sub-filter ordering within a group follows game order (matches ascending definition ID).
        public static readonly ProductionGoodDef[] Goods = new ProductionGoodDef[]
        {
            // Resources
            new ProductionGoodDef("Wood",        "Resources", 8193,  new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Stone",       "Resources", 8194,  new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Iron",        "Resources", 8195,  new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Pitch",       "Resources", 8196,  new[]{"x3","x5","x10"}),

            // Haul — in Specialist section; sub-filter index TBD (262145 is best guess)
            new ProductionGoodDef("Haul",        "Haul",      262145, new[]{"x2","x3","x4"}),

            // Food
            new ProductionGoodDef("Apple",       "Food",      4097,  new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Cheese",      "Food",      4098,  new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Meat",        "Food",      4099,  new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Bread",       "Food",      4100,  new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Vegetables",  "Food",      4101,  new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Fish",        "Food",      4102,  new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Ale",         "Food",      4103,  new[]{"x3","x5","x10"}),

            // Farmer — in Specialist section; sub-filter index TBD (262146 is best guess)
            new ProductionGoodDef("Farmer",      "Farmer",    262146, new[]{"x3","x5","x10"}),

            // Banquet (standard)
            new ProductionGoodDef("Venison",     "Banquet",   16385, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Chairs",      "Banquet",   16386, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Metalware",   "Banquet",   16387, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Clothes",     "Banquet",   16388, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Wine",        "Banquet",   16389, new[]{"x3","x5","x10"}),

            // Banquet (premium)
            new ProductionGoodDef("Salt",        "Banquet",   16390, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Spice",       "Banquet",   16391, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Silk",        "Banquet",   16392, new[]{"x3","x5","x10"}),

            // Weapons
            new ProductionGoodDef("Bows",        "Weapons",   32769, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Pike",        "Weapons",   32770, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Armour",      "Weapons",   32771, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Swords",      "Weapons",   32772, new[]{"x3","x5","x10"}),
            new ProductionGoodDef("Catapults",   "Weapons",   32773, new[]{"x3","x5","x10"}),
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
    }
}
