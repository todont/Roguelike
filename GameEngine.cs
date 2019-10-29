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
        public enum Action
        {
            MoveUp = ConsoleKey.UpArrow,
            MoveDown = ConsoleKey.DownArrow,
            MoveRight = ConsoleKey.RightArrow,
            MoveLeft = ConsoleKey.LeftArrow,
            OpenInventory = ConsoleKey.E,
            Confirm = ConsoleKey.Enter,
            PickUpItem = ConsoleKey.G,
            Esc = ConsoleKey.Escape
        }

        private void Init()
        {
            //Process proc = new Process();
            Map = File.ReadAllLines($"Locations/location1.txt");
            CurrentHero.Coords = new Point(10, 10);
            CurrentHero.PrevCoords = new Point(10, 10);
        }

        private void Input()
        {
            var action = (Action)Console.ReadKey(true).Key;
            CurrentHero.CurrentAction = action;
        }

        private void Logic()
        {
            CurrentHero.PrevCoords.X = CurrentHero.Coords.X;
            CurrentHero.PrevCoords.Y = CurrentHero.Coords.Y;
            switch (CurrentHero.CurrentAction)
            {
                case Action.MoveUp:
                    CurrentHero.MoveUp();
                    break;
                case Action.MoveDown:
                    CurrentHero.MoveDown();
                    break;
                case Action.MoveLeft:
                    CurrentHero.MoveLeft();
                    break;
                case Action.MoveRight:
                    CurrentHero.MoveRight();
                    break;
                case Action.Esc:
                    StartMenu();
                    break;
            }
            if (Map[CurrentHero.Coords.Y][CurrentHero.Coords.X] == '#')
            {
                CurrentHero.Coords.X = CurrentHero.PrevCoords.X;
                CurrentHero.Coords.Y = CurrentHero.PrevCoords.Y;
            }
        }

        private void Redraw()
        {
            Console.SetCursorPosition(CurrentHero.Coords.X, CurrentHero.Coords.Y);
            Console.Write("@");
            Console.SetCursorPosition(CurrentHero.PrevCoords.X, CurrentHero.PrevCoords.Y);
            Console.Write(Map[CurrentHero.PrevCoords.Y][CurrentHero.PrevCoords.X]);
        }

        private void Draw()
        {
            Console.Clear();
            //Console.SetWindowSize();
            //string[] locationArr = File.ReadAllLines($"Locations/{LastVisitedLocation}.txt");
            for (int i = 0; i < Map.Length; i++)
                Console.WriteLine(Map[i]);
            Console.SetCursorPosition(CurrentHero.Coords.X, CurrentHero.Coords.Y);
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