using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class Hero
    {
        public Point Coords { get; set; }
        public Point PrevCoords { get; set; }
        public GameEngine.Action CurrentAction { get; set; }
        enum Direction
        {
            Up = 'w',
            Down = 's',
            Left ='a',
            Right = 'd'
        }

        public void MoveUp()
        {
            --Coords.Y;
        }

        public void MoveDown()
        {
            ++Coords.Y;
        }

        public void MoveLeft()
        {
            --Coords.X;
        }

        public void MoveRight()
        {
            ++Coords.X;
        }
    }
}
