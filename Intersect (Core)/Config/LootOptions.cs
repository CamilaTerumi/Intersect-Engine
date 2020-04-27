﻿namespace Intersect.Config
{
    /// <summary>
    /// Contains configurable options pertaining to the way loot (item) drops are handled by the engine.
    /// </summary>
    public class LootOptions
    {

        /// <summary>
        /// Defines how long (in ms) an item drop will be ''owned'' by a player and their party.
        /// </summary>
        public int ItemOwnershipTime = 5000;

        /// <summary>
        /// Defines whether players can see items they do not ''own'' on the map.
        /// </summary>
        public bool ShowUnownedItems = false;
    }
}
