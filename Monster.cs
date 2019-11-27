using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class Monster : Character
    {
        public Monster(Point coords, int hitPoints, int rangeOfVision, Character.Speed speed, string name, char symbol)
        {
            Coords = coords;
            PrevCoords = new Point(coords.X, coords.Y);
            HitPoints = hitPoints; //should depend on class/hit dices
            RangeOfVision = rangeOfVision;
            CurrentSpeed = speed;
            Name = name;
            Symbol = symbol;
            IsMoved = false;
        }
        //Tile MonsterTile (color and shape of 
        // symbol that represents monster)
        public enum GameAction 
        {
            Attack
        }
        public GameAction CurrentGameAction { get; set; }
        protected override bool HandleCollisions(char clashedSymbol)
        {
            switch (clashedSymbol)
            {
                case '▒':
                    return false;
                default:
                    return true;
            }
        }
    }
}
