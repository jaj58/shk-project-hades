using System.Collections.Generic;

namespace Kingdoms.Bot
{
    public struct ModuleCardDef
    {
        public int DefId;
        public string Name;

        public ModuleCardDef(int defId, string name)
        {
            DefId = defId;
            Name = name;
        }
    }

    // Curated list of cards relevant to each schedulable module, shown in the Modules sub-tab's
    // card checklist. Def IDs verified against CommonTypes.dll (see all_cards.csv).
    public static class ModuleCardCatalog
    {
        private static readonly Dictionary<string, ModuleCardDef[]> _byModule =
            new Dictionary<string, ModuleCardDef[]>
        {
            { "Trade", new[]
                {
                    new ModuleCardDef(1537, "Carters (x2 merchant speed)"),
                    new ModuleCardDef(1538, "Advanced Carters (x4 merchant speed)"),
                    new ModuleCardDef(1539, "Expert Carters (x8 merchant speed)"),
                    new ModuleCardDef(1541, "Trade Caravans (x2 market capacity)"),
                    new ModuleCardDef(1542, "Advanced Trade Caravans (x3 market capacity)"),
                    new ModuleCardDef(1543, "Expert Trade Caravans (x5 market capacity)"),
                } },

            { "Scout", new[]
                {
                    new ModuleCardDef(2305, "Horse Breeding (x2 scout speed)"),
                    new ModuleCardDef(2306, "Thoroughbreds (x4 scout speed)"),
                    new ModuleCardDef(2307, "Arab Stallions (x6 scout speed)"),
                    new ModuleCardDef(2308, "Basic Scavenging (x1.5 scout capacity)"),
                    new ModuleCardDef(2309, "Advanced Scavenging (x2 scout capacity)"),
                    new ModuleCardDef(2310, "Expert Scavenging (x3 scout capacity)"),
                } },

            { "VillageBuilder", new[]
                {
                    new ModuleCardDef(1800, "Better Construction (x3 build speed)"),
                    new ModuleCardDef(1801, "Advanced Construction (x5 build speed)"),
                    new ModuleCardDef(1802, "Expert Construction (x10 build speed)"),
                } },

            // The following are best-guess curations (the user only specified Trade/Scout/Builder).
            { "Recruiting", new[]
                {
                    new ModuleCardDef(2564, "Basic Recruitment"),
                    new ModuleCardDef(2565, "Advanced Recruitment"),
                    new ModuleCardDef(2566, "Expert Recruitment"),
                } },

            { "CastleRepair", new[]
                {
                    new ModuleCardDef(257, "Master Mason"),
                    new ModuleCardDef(258, "Masonry Team"),
                    new ModuleCardDef(259, "Expert Masons"),
                } },

            // Popularity: no obviously-relevant cards defined yet.
        };

        public static ModuleCardDef[] GetCards(string moduleName)
        {
            ModuleCardDef[] cards;
            if (moduleName != null && _byModule.TryGetValue(moduleName, out cards))
                return cards;
            return new ModuleCardDef[0];
        }
    }
}
