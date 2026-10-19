using System;
using CommonTypes;

namespace Kingdoms.Bot
{
    /// <summary>
    /// Storage caps for the local player's own villages. A cap is player-wide - storage
    /// research times the active cards' multiplier - with capitals on their own table, so
    /// it needs no downloaded VillageMap.
    /// </summary>
    internal static class VillageCaps
    {
        /// <summary>
        /// The cap for one resource (building type id), or 0 if the research or card data
        /// isn't available yet.
        /// </summary>
        public static int GetResourceCap(int resourceId, bool isCapital)
        {
            try
            {
                double cap = GameEngine.Instance.World.UserResearchData.getResourceCap(
                        GameEngine.Instance.LocalWorldData, resourceId, isCapital) *
                    CardTypes.getResourceCapMultiplier(resourceId,
                        GameEngine.Instance.cardsManager.UserCardData);
                if (cap > 0) return (int)cap;
            }
            catch (Exception)
            {
            }

            return 0;
        }
    }
}
