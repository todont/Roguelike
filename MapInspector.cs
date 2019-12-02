using System;
namespace Roguelike
{
    class MapInspector : BaseCharacter
    {
        public MapInspector(string name, Point coords)
        {
            Name = name;
            Coords = coords;
            PrevCoords = new Point();
        }

        public override void Move()
        {
            int mapHeight = Program.GameEngine.GetMapHeight();
            int mapWidth = Program.GameEngine.GetMapWidth();
            switch (CurrentMoveAction)
            {
                case MoveAction.Up:
                    if (Coords.Y - 1 >= 0) SetPrevPlusMove(MoveAction.Up);
                    break;
                case MoveAction.Down:
                    if (Coords.Y + 1 < mapHeight) SetPrevPlusMove(MoveAction.Down);
                    break;
                case MoveAction.Left:
                    if (Coords.X >= 0) SetPrevPlusMove(MoveAction.Left);
                    break;
                case MoveAction.Right:
                    if (Coords.X < mapWidth - 1) SetPrevPlusMove(MoveAction.Right);
                    break;
            }
        }
    }
}
