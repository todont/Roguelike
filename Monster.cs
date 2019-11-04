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

        }
        public GameAction CurrentGameAction { get; set; }
        public void DetectCollisions(char clashedSymbol)
        {
            switch (clashedSymbol)
            {
                case '#':
                    StepBack();
                    break;
                //case 'hero tile'
                // Attack()
                default:
                    break;
            }
        }
    }
}
