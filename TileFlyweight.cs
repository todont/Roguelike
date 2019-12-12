using System;
namespace Roguelike
{
    class TileFlyweight
    {
        public string Name { get; }
        public string Description { get; }
        public char Symbol { get; }
        public Type TileType { get; }

        public enum Type
        {
            Wall = 0,
            Ground = 1,
            Treasure = 2
        }

        public TileFlyweight(string name, string description, char symbol, Type type)
        {
            Name = name;    
            Description = description;
            Symbol = symbol;
            TileType = type;
        }
    }
}
