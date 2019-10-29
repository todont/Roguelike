using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class Character
    {
        public enum Speed { Low, Normal, High }
        public enum Action
        {
            MoveUp = ConsoleKey.UpArrow,
            MoveDown = ConsoleKey.DownArrow,
            MoveRight = ConsoleKey.RightArrow,
            MoveLeft = ConsoleKey.LeftArrow,
            OpenInventory = ConsoleKey.E,
            Confirm = ConsoleKey.Enter,
            PickUpItem = ConsoleKey.G,
            Exit = ConsoleKey.Escape
        }

        public string Name { get; set; }
        public Point Coords { get; set; }
        public Point PrevCoords { get; set; }
        public Action CurrentAction { get; set; }
        public Speed CurrentSpeed { get; set; }
        public int HitPoints { get; set; }
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
