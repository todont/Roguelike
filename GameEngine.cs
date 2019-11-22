using System;
using System.IO;
using CaveGenerator;

namespace Roguelike
{
    class GameEngine
    {
        private Hero CurrentHero;
        private bool GameOver = false;
        private Point MapOffset; //Coords of left top corner of MapBorder
        private Cave Map;
        //Maybe we will need it later
        public Rectangle HeroInfoBorder { get; set; }
        public Rectangle MapBorder { get; set; }
        public Rectangle InfoBorder { get; set; }
        private int ConsoleHeight = 0;
        private int ConsoleWidth = 0;

        private void Init()
        {
            Map = new Cave();
            Map.Build();
            Map.ConnectCaves();
            Map.WriteMapIntoFile();
            //Map = File.ReadAllLines($"Locations/location1.txt");
            CurrentHero = new Hero(new Point(10,10), 15, 0, Character.Speed.Normal, "Chiks-Chiriks");
        }

        private void Input()
        {
            var key = Console.ReadKey(true).Key;
            CurrentHero.CurrentMoveAction = (Hero.MoveAction)key;
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
            //loop over all monsters (probably at a distance x from hero)
            //MonsterId.Move(); //some kind of monster identificator
        }
        #region drawstuff
        private void Redraw()
        {
            Console.SetCursorPosition(CurrentHero.Coords.X + MapOffset.X, CurrentHero.Coords.Y + MapOffset.Y);
            Console.Write("@");
            Console.SetCursorPosition(CurrentHero.PrevCoords.X + MapOffset.X, CurrentHero.PrevCoords.Y + MapOffset.Y);
            Console.Write(Map.WorldAscii[CurrentHero.PrevCoords.Y][CurrentHero.PrevCoords.X]);
        }

        private void Draw()
        {
            Console.Clear();
            DrawAllBorders();
            for (int i = 0; i < MapBorder.Height - 2 && i < Map.WorldAscii.Length; i++)
            {
                Console.SetCursorPosition(MapOffset.X, MapOffset.Y + i);
                string mapstr = Map.WorldAscii[i].Length > MapBorder.Width - 2 ?
                 Map.WorldAscii[i].Substring(0, MapBorder.Width - 2) : Map.WorldAscii[i];  
                Console.WriteLine(mapstr);
            }
            Console.SetCursorPosition(CurrentHero.Coords.X + MapOffset.X, CurrentHero.Coords.Y + MapOffset.Y);
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
            MapOffset = new Point(MapBorder.Location.X + 1, MapBorder.Location.Y + 1);
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
            char symbol = Map.WorldAscii[point.Y][point.X];
            return symbol;
        }

        public void StartMenu()
        {
            Menu menu = new Menu();
            string selected = menu.Process();
            // У меню есть пункты - отдельные объекты, у каждого объекта есть свой метод doAction();
            // Т.е. при выборе пункта вызывается .DoACtion() вместо сравнения строк.
            if (selected == "New Game") PlayGame();
            if (selected == "Exit") Program.CleanUpAndExit();
        }

        public void PlayGame()
        {
            Init();
            Draw();
            while (!GameOver)
            {
                if (ConsoleWidth != Console.WindowWidth || ConsoleHeight != Console.WindowHeight)
                    Draw();
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