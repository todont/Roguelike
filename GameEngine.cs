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
        enum Action
        {

        }

        private void Init()
        {

        }

        private void Input()
        {
            var action = (Action)Console.ReadKey(true).KeyChar;
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

        }

        public void PlayGame()
        {
            Init();
            Draw();
            while (true)
            {
                Input();
                Logic();
                Redraw();
            }
        }
    }
}
