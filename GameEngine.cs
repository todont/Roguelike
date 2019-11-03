using System;
using System.IO;

namespace Roguelike
{
    class GameEngine
    {
        private Hero CurrentHero = new Hero();
        private bool GameOver = false;
        private string[] Map;
        //Remake offsets like class fields
        private Point MapOffset;
        private Point InfoOffset;

        private void Init()
        {
            Map = File.ReadAllLines($"Locations/location1.txt");
            CurrentHero.Coords = new Point(10, 10);
            CurrentHero.PrevCoords = new Point(10, 10);
            CurrentHero.HitPoints = 15; //should depend on class/hit dices
            CurrentHero.HpPoints = 0;   
            CurrentHero.CurrentSpeed = Character.Speed.High;
            CurrentHero.Name = "Chiks-Chiriks";
        }

        private void DrawAllBorders()
        {
            Rectangle mapBorder = new Rectangle
            {
                Width = Console.WindowWidth * 3 / 4,
                Height = Console.WindowHeight * 4 / 5
            };
            mapBorder.Location = new Point(Console.WindowWidth - mapBorder.Width, 0);
            MapOffset = new Point(mapBorder.Location.X + 1, mapBorder.Location.Y + 1);
            DrawBorder(mapBorder);

            Rectangle infoBorder = new Rectangle
            {
                Height = Console.WindowHeight - mapBorder.Height,
                Width = Console.WindowWidth,
                Location = new Point(0, mapBorder.Height)
            };
            InfoOffset = new Point(infoBorder.Location.X + 1, infoBorder.Location.Y + 1);
            DrawBorder(infoBorder);

            Rectangle heroInfoBorder = new Rectangle
            {
                Width = Console.WindowWidth - mapBorder.Width,
                Height = mapBorder.Height,
                Location = new Point(0, 0)
            };
            DrawBorder(heroInfoBorder);
        }

        private void Input()
        {
            var action = (Character.Action)Console.ReadKey(true).Key;
            CurrentHero.CurrentAction = action;
        }

        private void Logic()
        {
            CurrentHero.PrevCoords.X = CurrentHero.Coords.X;
            CurrentHero.PrevCoords.Y = CurrentHero.Coords.Y;
            switch (CurrentHero.CurrentAction)
            {
                case Character.Action.MoveUp:
                    CurrentHero.MoveUp();
                    break;
                case Character.Action.MoveDown:
                    CurrentHero.MoveDown();
                    break;
                case Character.Action.MoveLeft:
                    CurrentHero.MoveLeft();
                    break;
                case Character.Action.MoveRight:
                    CurrentHero.MoveRight();
                    break;
                case Character.Action.Exit: //rename
                    StartMenu();
                    break;
            }

            switch (Map[CurrentHero.Coords.Y][CurrentHero.Coords.X])
            {
                case '#':
                    CurrentHero.Coords.X = CurrentHero.PrevCoords.X;
                    CurrentHero.Coords.Y = CurrentHero.PrevCoords.Y;
                    //TODO: make this as log function that depends on symbol we switching
                    Console.SetCursorPosition(InfoOffset.X, InfoOffset.Y);
                    Console.WriteLine("You hit a wall!");
                    //break out of loop
                    break;
                default:
                    //TODO: make this as log function that depends on symbol we switching
                    Console.SetCursorPosition(InfoOffset.X, InfoOffset.Y);
                    Console.WriteLine(new string(' ', 60));
                    break;
            }
        }
        private void Redraw()
        {
            Console.SetCursorPosition(CurrentHero.Coords.X + MapOffset.X, CurrentHero.Coords.Y + MapOffset.Y);
            Console.Write("@");
            Console.SetCursorPosition(CurrentHero.PrevCoords.X + MapOffset.X, CurrentHero.PrevCoords.Y + MapOffset.Y);
            Console.Write(Map[CurrentHero.PrevCoords.Y][CurrentHero.PrevCoords.X]);
        }

        private void Draw()
        {
            //Console.SetWindowSize();
            //string[] locationArr = File.ReadAllLines($"Locations/{LastVisitedLocation}.txt");
            Console.Clear();
            DrawAllBorders();
            for (int i = 0; i < Map.Length; i++)
            {
                Console.SetCursorPosition(MapOffset.X, MapOffset.Y + i);
                Console.WriteLine(Map[i]);
            }
            Console.SetCursorPosition(CurrentHero.Coords.X + MapOffset.X, CurrentHero.Coords.Y + MapOffset.Y);
            Console.Write("@");
        }

        private void DrawBorder(Rectangle border)
        {
            int width = border.Width;
            int height = border.Height;
            Point location = border.Location;
            Console.SetCursorPosition(location.X, location.Y);
            //Console.WriteLine($"{Console.WindowWidth} {Console.WindowHeight}");
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
            //Console.W
        }

        public void StartMenu()
        {
            Menu menu = new Menu();
            string selected = menu.Process();
            if (selected == "New Game") PlayGame();
            if (selected == "Exit") Program.CleanUpAndExit();
        }

        public void PlayGame()
        {
            Init();
            Draw();
            while (!GameOver)
            {
                Input();
                Logic();
                if (CurrentHero.PrevCoords.X != CurrentHero.Coords.X
                || CurrentHero.PrevCoords.Y != CurrentHero.Coords.Y)
                    Redraw();
            }
        }
    }
}