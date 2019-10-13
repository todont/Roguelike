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

        public Hero()
        {
            Coords = new Point(10, 10);
        }

        public void Move(Location location)
        {
            var key = Console.ReadKey(true).KeyChar;
            switch (key)
            {
                case 'w':
                    if (location.AsciiView[Coords.X][Coords.Y + 1] != '#')
                        ++Coords.Y;
                    break;
                case 's':
                    if (location.AsciiView[Coords.X][Coords.Y - 1] != '#')
                        --Coords.Y;
                    break;
                case 'a':
                    if (location.AsciiView[Coords.X - 1][Coords.Y] != '#')
                        --Coords.X;
                    break;
                case 'd':
                    if (location.AsciiView[Coords.X + 1][Coords.Y] != '#')
                        ++Coords.X;
                    break;
            }

        }
    }
}
