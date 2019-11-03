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
            CurrentHero.ExpPoints = 0;
            CurrentHero.CurrentSpeed = Hero.Speed.High;
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
            var key = Console.ReadKey(true).Key;
            CurrentHero.CurrentMoveAction = (Hero.MoveAction)key;
            CurrentHero.CurrentGameAction = (Hero.GameAction)key;
        }
        private void Logic()
        {
            //if we in the menu, everything became different
            //CurrentHero.DoMenuAction();
            //else

            if (CurrentHero.Move() == false)
            {
                CurrentHero.DoGameAction();
                return;
                //we do "return" here cause we don't need to redraw map
                //in case hero don't move
            }
            CurrentHero.HandleCollisions(Map[CurrentHero.Coords.Y][CurrentHero.Coords.X]);

            //loop over all monsters (probably at a distance x from hero)
            //MonsterId.Move; //some kind of monster identificator
            //ParseMonsterCollisions(MonsterId);

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
                    //need to Redraw() not only when PrevCoords!=Coords.
                    //For example, if we just killed a monster
            }
        }
    }
}