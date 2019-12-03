using System;

namespace Roguelike
{
    class BaseEntity
    {
        public string Name { get; set; }
        public Point Coords { get; set; }
        public Point PrevCoords { get; set; }
        public char Symbol { get; set; }
    }
}
