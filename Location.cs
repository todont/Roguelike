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
        //private string LastVisitedLocation = "location1";
        public readonly string[] AsciiView = File.ReadAllLines($"Locations/location1.txt");
        public void Draw(Hero hero)
        {
            Console.CursorVisible = false;
            //Console.SetWindowSize();
            //string[] locationArr = File.ReadAllLines($"Locations/{LastVisitedLocation}.txt");
            for (int i = 0; i < AsciiView.Length; i++)
                Console.WriteLine(AsciiView[i]);
            Console.SetCursorPosition(hero.Coords.X, hero.Coords.Y);
            Console.Write("@");
        }
    }
}