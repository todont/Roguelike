using System;
namespace Roguelike
{
    class BaseCharacter : BaseEntity
    {
        public enum MoveAction
        {
            Up = ConsoleKey.UpArrow,
            Down = ConsoleKey.DownArrow,
            Right = ConsoleKey.RightArrow,
            Left = ConsoleKey.LeftArrow
        }

        public MoveAction CurrentMoveAction { get; set; }

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

        protected void SetPrevCoords()
        {
            PrevCoords.X = Coords.X;
            PrevCoords.Y = Coords.Y;
        }

        protected void SetPrevPlusMove(MoveAction action)
        {
            SetPrevCoords();
            switch (action)
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

        public virtual void Move() //sets IsMoved
        {
            switch (CurrentMoveAction)
            {
                case MoveAction.Up:
                    SetPrevPlusMove(MoveAction.Up);
                    break;
                case MoveAction.Down:
                    SetPrevPlusMove(MoveAction.Down);
                    break;
                case MoveAction.Left:
                    SetPrevPlusMove(MoveAction.Left);
                    break;
                case MoveAction.Right:
                    SetPrevPlusMove(MoveAction.Right);
                    break;
            }
        }
    }
}