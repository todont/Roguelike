using System;
namespace Roguelike
{
    class Tile
    {
        public TileFlyweight.Type Type { get; set; }
        public BaseEntity Object { get; set; }

        public Tile(TileFlyweight.Type type, BaseEntity obj = null)
        {
            Type = type;
            Object = obj;
        }
    }
}
