using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    abstract class Character
    {
        public enum Speed
        {
            Normal,
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
        public int RangeOfVision { get; set; }
        public char Symbol { get; set; }
        public bool IsMoved { get; set; }
        protected abstract bool HandleCollisions(char clashedSymbol);
        //sets enum GameAction, returns true if we can move, otherwise - false
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
        public void RestoreCoords()
        {
            Coords.X = PrevCoords.X;
            Coords.Y = PrevCoords.Y;
        }
        private void SetPrevCoords()
        {
            PrevCoords.X = Coords.X;
            PrevCoords.Y = Coords.Y;
        }
        private void SetPrevPlusMove(MoveAction action)
        {
            SetPrevCoords();
            switch(action)
            {
                case MoveAction.Up:
                    MoveUp();
                    break;
                case MoveAction.Down:
                    MoveDown();
                    break;
                case MoveAction.Left:
                    MoveLeft();
                    break;
                case MoveAction.Right:
                    MoveRight();
                    break;
            }
        }
        public bool Move() //returns true if character was moved, otherwise - false
        {
            switch (CurrentMoveAction)
            {
                case MoveAction.Up:
                    IsMoved = HandleCollisions(Program.GameEngine.GetMapSymbol(new Point(Coords.X, Coords.Y - 1)));
                    if (IsMoved) SetPrevPlusMove(MoveAction.Up);
                    break;
                case MoveAction.Down:
                    IsMoved = HandleCollisions(Program.GameEngine.GetMapSymbol(new Point(Coords.X, Coords.Y + 1)));
                    if (IsMoved) SetPrevPlusMove(MoveAction.Down);
                    break;
                case MoveAction.Left:
                    IsMoved = HandleCollisions(Program.GameEngine.GetMapSymbol(new Point(Coords.X - 1, Coords.Y)));
                    if (IsMoved) SetPrevPlusMove(MoveAction.Left);
                    break;
                case MoveAction.Right:
                    IsMoved = HandleCollisions(Program.GameEngine.GetMapSymbol(new Point(Coords.X + 1, Coords.Y)));
                    if (IsMoved) SetPrevPlusMove(MoveAction.Right);
                    break;
                default:
                    IsMoved = false;
                    break;
            }
            if(CurrentSpeed == Speed.High && IsMoved)
            {
                bool isDoubleMove;
                switch (CurrentMoveAction)
                {
                    case MoveAction.Up:
                        isDoubleMove = HandleCollisions(Program.GameEngine.GetMapSymbol(new Point(Coords.X, Coords.Y - 1)));
                        if (isDoubleMove) MoveUp();
                        break;
                    case MoveAction.Down:
                        isDoubleMove = HandleCollisions(Program.GameEngine.GetMapSymbol(new Point(Coords.X, Coords.Y + 1)));
                        if (isDoubleMove) MoveDown();
                        break;
                    case MoveAction.Left:
                        isDoubleMove = HandleCollisions(Program.GameEngine.GetMapSymbol(new Point(Coords.X - 1, Coords.Y)));
                        if (isDoubleMove) MoveLeft();
                        break;
                    case MoveAction.Right:
                        isDoubleMove = HandleCollisions(Program.GameEngine.GetMapSymbol(new Point(Coords.X + 1, Coords.Y)));
                        if (isDoubleMove) MoveRight();
                        break;
                }
            }
            return IsMoved;
        }
    }
}
