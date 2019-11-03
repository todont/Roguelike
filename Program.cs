using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class Program
    {
        public static void CleanUpAndExit()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            Console.CancelKeyPress += delegate
            {
                CleanUpAndExit();
            };

            Console.CursorVisible = false;
            GameEngine gameEngine = new GameEngine();
            gameEngine.StartMenu();
            //gameEngine.StartGame();
        }
    }
}
