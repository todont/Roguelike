using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class GameEngine
    {
        private Hero hero;
        private bool GameOver = false;
        public enum Action
        {
            MoveUp = ConsoleKey.UpArrow,
            MoveDown = ConsoleKey.DownArrow,
            MoveRight = ConsoleKey.RightArrow,
            MoveLeft = ConsoleKey.LeftArrow,
            OpenInventory = ConsoleKey.E,
            Confirm = ConsoleKey.Enter,
            PickUpItem = ConsoleKey.G,
        }

        private void Init()
        {

        }

        private void Input()
        {
            var action = (Action)Console.ReadKey(true).Key;
            Console.WriteLine(action);
            switch (action)
            {
                case Action.MoveUp:
                    break;

            }
        }

        private void Logic()
        {

        }

        private void Redraw()
        {

        }

        private void Draw()
        {

        }

        public void StartMenu()
        {
            Input();
        }

        public void PlayGame()
        {
            Init();
            Draw();
            while (!GameOver)
            {
                Input();
                Logic();
                Redraw();
            }
        }
    }
}