using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class GameEngine
    {
        private Hero CurrentHero = new Hero();
        private bool GameOver = false;
        private string[] Map;
        private const int OffsetX = 20;
        private const int OffsetY = 8;

        private void Init()
        {
            //Process proc = new Process();
            Map = File.ReadAllLines($"Locations/location1.txt");
            CurrentHero.Coords = new Point(10, 10);
            CurrentHero.PrevCoords = new Point(10, 10);
            CurrentHero.HitPoints = 15; //should depend on class/hit dices
            CurrentHero.HpPoints = 0;
            CurrentHero.CurrentSpeed = Character.Speed.High;
            CurrentHero.Name = "Chiks-Chiriks";
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
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("You hit a wall!");
                    //break out of loop
                    break;
                default:
                    //TODO: make this as log function that depends on symbol we switching
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine(new string(' ', 60));
                    break;
            }
        }
        private void Redraw()
        {
            Console.SetCursorPosition(CurrentHero.Coords.X + OffsetX, CurrentHero.Coords.Y + OffsetY);
            Console.Write("@");
            Console.SetCursorPosition(CurrentHero.PrevCoords.X + OffsetX, CurrentHero.PrevCoords.Y + OffsetY);
            Console.Write(Map[CurrentHero.PrevCoords.Y][CurrentHero.PrevCoords.X]);
        }

        private void Draw()
        {
            Console.Clear();
            //Console.SetWindowSize();
            //string[] locationArr = File.ReadAllLines($"Locations/{LastVisitedLocation}.txt");
            for (int i = 0; i < Map.Length; i++)
            {
                Console.SetCursorPosition(OffsetX, OffsetY + i);
                Console.WriteLine(Map[i]);
            }
            Console.SetCursorPosition(CurrentHero.Coords.X + OffsetX, CurrentHero.Coords.Y + OffsetY);
            Console.Write("@");
        }

        public void StartMenu()
        {
            Menu menu = new Menu();
            string selected = menu.Process();
            if (selected == "New Game") PlayGame();
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