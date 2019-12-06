using System;
using System.Collections.Generic;

namespace Roguelike
{
    class TileFactory
    {
        static public Dictionary<TileFlyweight.Type, TileFlyweight> tiles = new Dictionary<TileFlyweight.Type, TileFlyweight>();
       
        public TileFactory()
        {
            if (!tiles.ContainsKey(TileFlyweight.Type.Ground))
                tiles.Add(TileFlyweight.Type.Ground, new TileFlyweight("Ground", "Ground", '.', TileFlyweight.Type.Ground));
            if (!tiles.ContainsKey(TileFlyweight.Type.Wall))
                tiles.Add(TileFlyweight.Type.Wall, new TileFlyweight("Wall", "Common wall", '▒', TileFlyweight.Type.Wall));
            if (!tiles.ContainsKey(TileFlyweight.Type.Treasure))
                tiles.Add(TileFlyweight.Type.Treasure, new TileFlyweight("Treasure", "Unusual treasure", 'T', TileFlyweight.Type.Treasure));
        }

        public TileFlyweight GetTile(TileFlyweight.Type key)
        {
            if (tiles.ContainsKey(key))
                return tiles[key];
            return null;
        }
    }
}
