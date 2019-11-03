using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class Character
    {
        public enum Speed 
        { 
            Low,
            Medium,
            High
        }
        public enum MoveAction
        {
            Up = ConsoleKey.UpArrow,
            Down = ConsoleKey.DownArrow,
            Right = ConsoleKey.RightArrow,
            Left = ConsoleKey.LeftArrow
        }

        public string Name { get; set; }
        public Point Coords { get; set; }
        public Point PrevCoords { get; set; }
        public MoveAction CurrentMoveAction { get; set; }
        public Speed CurrentSpeed { get; set; }
        public int HitPoints { get; set; }
        private void SetPrevCoords()
        {
            PrevCoords.X = Coords.X;
            PrevCoords.Y = Coords.Y;
        }
        public void MoveUp()
        {
            SetPrevCoords();
            --Coords.Y;
        }
        public void MoveDown()
        {
            SetPrevCoords();
            ++Coords.Y;
        }
        public void MoveLeft()
        {
            SetPrevCoords();
            --Coords.X;
        }
        public void MoveRight()
        {
            SetPrevCoords();
            ++Coords.X;
        }
        public void StepBack()
        {
            Coords.X = PrevCoords.X;
            Coords.Y = PrevCoords.Y;
        }

        public bool Move()
        {
            switch (CurrentMoveAction)
            {
                case MoveAction.Up:
                    MoveUp();
                    return true;
                case MoveAction.Down:
                    MoveDown();
                    return true;
                case MoveAction.Left:
                    MoveLeft();
                    return true;
                case MoveAction.Right:
                    MoveRight();
                    return true;
                default:
                    return false;
            }
        }
    }
}
