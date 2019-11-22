using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class Monster : Character
    {
        //Tile MonsterTile (color and shape of 
        // symbol that represents monster)

        //Attack(). this function mb will be decorated by 
        //monster sub-classes

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
