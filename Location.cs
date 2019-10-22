using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Roguelike
{
    class Location
    {
        public void Redraw(Hero hero)
        {
            Console.SetCursorPosition(hero.Coords.X, hero.Coords.Y);
            Console.Write("@");
            //Console.SetCursorPosition(HeroPrevCoords.X, HeroPrevCoords.Y);
            Console.Write(".");
            //HeroPrevCoords = new Point(hero.Coords.X, hero.Coords.Y);
        }
    }
}
