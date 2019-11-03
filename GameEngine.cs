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
        private const int OffsetX = 40;
        private const int OffsetY = 8;

        private void Init()
        {
            //Process proc = new Process();
            Map = File.ReadAllLines($"Locations/location1.txt");
            CurrentHero.Coords = new Point(10, 10);
            CurrentHero.PrevCoords = new Point(10, 10);
            CurrentHero.HitPoints = 15; //should depend on class/hit dices
            CurrentHero.ExpPoints = 0;
            CurrentHero.CurrentSpeed = Hero.Speed.High;
            CurrentHero.Name = "Chiks-Chiriks";
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
                    //need to Redraw() not only when PrevCoords!=Coords.
                    //For example, if we just killed a monster
            }
        }
    }
}