using System;
namespace Roguelike
{
    class Tile
    {
        public Point Coords { get; set; }
        public TileFlyweight.Type Type { get; set; }

        public Tile(Point coords, TileFlyweight.Type type)
        {
            Coords = coords;
            Type = type;
        }
    }
}
