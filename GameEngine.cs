using System;
using System.IO;
using CaveGenerator;

namespace Roguelike
{
    class GameEngine
    {
        private Hero CurrentHero;
        private bool GameOver = false;
        public bool GameStarted { get; private set; }
        private Cave Map;
        public Rectangle HeroInfoBorder { get; set; }
        public Rectangle MapBorder { get; set; }
        public Rectangle InfoBorder { get; set; }
        private int ConsoleHeight = 0;
        private int ConsoleWidth = 0;

        public void Init()
        {
            Map = new Cave();
            Map.Build();
            Map.ConnectCaves();
            Map.WriteMapIntoFile();
            Map.Offset = new Point(0, 0);
            CurrentHero = new Hero(new Point(10, 10), 15, 0, Character.Speed.Normal, "Chiks-Chiriks");
            GameStarted = true;
        }

        private void Input()
        {
            var key = Console.ReadKey(true).Key;
            CurrentHero.CurrentMoveAction = (Character.MoveAction)key;
            CurrentHero.CurrentGameAction = (Hero.GameAction)key;
        }

        private void Logic()
        {
            if (CurrentHero.Move() == false)
            {
                CurrentHero.DoGameAction();
                return;
                //we do "return" here cause we don't need to redraw map
                //in case hero don't move
            }
            MoveMap();
            //loop over all monsters (probably at a distance x from hero)
            //MonsterId.Move; //some kind of monster identificator
        }

        enum MapMoveDirection
        {
            Top,
            Bot,
            Left,
            Right
        }

        private void MoveMap()
        {
            int distToTop = CurrentHero.Coords.Y + 1;
            int distToLeft = CurrentHero.Coords.X + 1;
            int distToRight = MapBorder.Width - CurrentHero.Coords.X - 2;
            int distToBot = MapBorder.Height - 2 - CurrentHero.Coords.Y;
            int critDistHor = MapBorder.Width / 8;
            int critDistVert = MapBorder.Height / 8;
            if (distToTop <= critDistVert)
                MoveMapToDir(MapMoveDirection.Bot);
            else if (distToBot <= critDistVert)
                MoveMapToDir(MapMoveDirection.Top);
            else if (distToRight <= critDistHor)
                MoveMapToDir(MapMoveDirection.Left);
            else if (distToLeft <= critDistHor)
                MoveMapToDir(MapMoveDirection.Right);
        }

        private void MoveMapToDir(MapMoveDirection direction)
        {
            int offset, length;
            switch (direction)
            {
                case MapMoveDirection.Top:
                    offset = Map.Offset.Y;
                    length = Map.WorldAscii.Length;
                    Map.Offset.Y = offset + 1 < length ? offset + 1 : offset;
                    CurrentHero.Coords.X = CurrentHero.PrevCoords.X;
                    CurrentHero.Coords.Y = CurrentHero.PrevCoords.Y;
                    Draw();
                    break;
                case MapMoveDirection.Bot:
                    offset = Map.Offset.Y;
                    Map.Offset.Y = offset - 1 >= 0 ? offset - 1 : offset;
                    CurrentHero.Coords.X = CurrentHero.PrevCoords.X;
                    CurrentHero.Coords.Y = CurrentHero.PrevCoords.Y;
                    Draw();
                    break;
                case MapMoveDirection.Left:
                    offset = Map.Offset.X;
                    length = Map.WorldAscii[0].Length;
                    Map.Offset.X = offset + 1 < length ? offset + 1 : offset;
                    CurrentHero.Coords.X = CurrentHero.PrevCoords.X;
                    CurrentHero.Coords.Y = CurrentHero.PrevCoords.Y;
                    Draw();
                    break;
                case MapMoveDirection.Right:
                    offset = Map.Offset.X;
                    Map.Offset.X = offset - 1 >= 0 ? offset - 1 : offset;
                    CurrentHero.Coords.X = CurrentHero.PrevCoords.X;
                    CurrentHero.Coords.Y = CurrentHero.PrevCoords.Y;
                    Draw();
                    break;
            }
        }

        #region drawstuff
        private void Redraw()
        {
            Console.SetCursorPosition(CurrentHero.Coords.X + MapBorder.Offset.X, CurrentHero.Coords.Y + MapBorder.Offset.Y);
            Console.Write("@");
            Console.SetCursorPosition(CurrentHero.PrevCoords.X + MapBorder.Offset.X, CurrentHero.PrevCoords.Y + MapBorder.Offset.Y);
            Console.Write(Map.WorldAscii[CurrentHero.PrevCoords.Y + Map.Offset.Y][CurrentHero.PrevCoords.X + Map.Offset.X]);
        }

        private void Draw()
        {
            //Console.Clear();
            //DrawAllBorders();
            for (int i = Map.Offset.Y, j = 0; j < MapBorder.Height - 2 && i < Map.WorldAscii.Length; i++, j++)
            {
                Console.SetCursorPosition(MapBorder.Offset.X, MapBorder.Offset.Y + j);
                string mapstr = Map.WorldAscii[i].Length > MapBorder.Width - 2 ?
                 Map.WorldAscii[i].Substring(Map.Offset.X, MapBorder.Width - 2) :
                 Map.WorldAscii[i].Substring(Map.Offset.X);
                Console.WriteLine(mapstr);
            }
            Console.SetCursorPosition(CurrentHero.Coords.X + MapBorder.Offset.X, CurrentHero.Coords.Y + MapBorder.Offset.Y);
            Console.Write("@");
        }

        private void DrawAllBorders()
        {
            MapBorder = new Rectangle
            {
                Width = Console.WindowWidth * 3 / 4,
                Height = Console.WindowHeight * 4 / 5
            };
            MapBorder.Location = new Point(Console.WindowWidth - MapBorder.Width, 0);
            MapBorder.Offset = new Point(MapBorder.Location.X + 1, MapBorder.Location.Y + 1);
            DrawBorder(MapBorder);

            InfoBorder = new Rectangle
            {
                Height = Console.WindowHeight - MapBorder.Height,
                Width = Console.WindowWidth,
                Location = new Point(0, MapBorder.Height),
                Offset = new Point(1, MapBorder.Height + 1)
            };
            DrawBorder(InfoBorder);

            HeroInfoBorder = new Rectangle
            {
                Width = Console.WindowWidth - MapBorder.Width,
                Height = MapBorder.Height,
                Location = new Point(0, 0),
                Offset = new Point(1, 1)
            };
            DrawBorder(HeroInfoBorder);
        }

        private void DrawBorder(Rectangle border)
        {
            int width = border.Width;
            int height = border.Height;
            Point location = border.Location;
            Console.SetCursorPosition(location.X, location.Y);
            Console.Write("╔");
            Console.SetCursorPosition(location.X + 1, location.Y);
            Console.Write(new string('═', width - 2));
            Console.SetCursorPosition(location.X + width - 1, location.Y);
            Console.WriteLine("╗");
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(location.X, location.Y + i);
                Console.Write("║");
                Console.SetCursorPosition(location.X + 1, location.Y + i);
                Console.Write(new string(' ', width - 2));
                Console.SetCursorPosition(location.X + width - 1, location.Y + i);
                Console.WriteLine("║");
            }
            Console.SetCursorPosition(location.X, location.Y + height - 1);
            Console.Write("╚");
            Console.SetCursorPosition(location.X + 1, location.Y + height - 1);
            Console.Write(new string('═', width - 2));
            Console.SetCursorPosition(location.X + width - 1, location.Y + height - 1);
            Console.Write("╝");
        }
        #endregion

        public char GetMapSymbol(Point point)
        {
            char symbol = Map.WorldAscii[point.Y + Map.Offset.Y][point.X + Map.Offset.X];
            return symbol;
        }

        public void StartMenu()
        {
            StartingMenu startingMenu = new StartingMenu();
            startingMenu.Process();
        }

        public void PlayGame()
        {
            Console.Clear();
            DrawAllBorders();
            Draw();
            while (!GameOver)
            {
                if (ConsoleWidth != Console.WindowWidth || ConsoleHeight != Console.WindowHeight)
                {
                    Console.Clear();
                    DrawAllBorders();
                    Draw();
                }
                ConsoleWidth = Console.WindowWidth;
                ConsoleHeight = Console.WindowHeight;

                Input();
                Logic();
                if (CurrentHero.PrevCoords.X != CurrentHero.Coords.X
                || CurrentHero.PrevCoords.Y != CurrentHero.Coords.Y)
                    Redraw();
                    //need to Redraw() not only when PrevCoords!=Coords.
                    //For example, if we just killed a monster
            }
        }
    }
}
