using System.Collections.Generic;

namespace Game.Features.Composite
{
    public class GameState
    {
        public Dictionary<string, int> ItemCounts = new Dictionary<string, int>();
        public HashSet<string> DefeatedEnemies = new HashSet<string>();
        public HashSet<string> ActivatedObjects = new HashSet<string>();
        public HashSet<string> BrokenTiles = new HashSet<string>();
        public int TotalTiles;

        public GameState()
        {
            // mock initial state
            ItemCounts["red_gem"] = 3;
            ItemCounts["golden_key"] = 0;
            TotalTiles = 12;
        }
    }
}