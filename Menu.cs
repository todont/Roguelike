using System;
using System.Collections.Generic;

namespace Roguelike
{ 
    public class Menu
    {
        private int CurIndex = 0;
        private List<string> menuItem = new List<string>()
        {
                "Resume",
                "New Game",
                "Settings",
                "Exit"
        };

        private void CheckKey(ConsoleKey key)
        {
            if (key == ConsoleKey.DownArrow)
            {
                if (CurIndex == menuItem.Count - 1) CurIndex = 0;
                else CurIndex++;
            }
            else if (key == ConsoleKey.UpArrow)
            {
                if (CurIndex == 0) CurIndex = menuItem.Count - 1;
                else CurIndex--;
            }
            else if (key == ConsoleKey.Enter)
            {

            }
        }

        public string Process()
        {
            while (true)
            {
                Console.Clear();
                Console.ResetColor();
                for (int i = 0; i < menuItem.Count; i++)
                {
                    Console.SetCursorPosition((Console.WindowWidth - 
                                               menuItem[i].Length) / 2, i);
                    if (i == CurIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;

                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine(menuItem[i]);
                }

                var key = (GameEngine.Action)Console.ReadKey(true).Key;

                if (key == GameEngine.Action.MoveDown)
                {
                    if (CurIndex == menuItem.Count - 1) CurIndex = 0;
                    else CurIndex++;
                }
                else if (key == GameEngine.Action.MoveUp)
                {
                    if (CurIndex == 0) CurIndex = menuItem.Count - 1;
                    else CurIndex--;
                }
                else if (key == GameEngine.Action.Confirm)
                {
                    return menuItem[CurIndex];
                }
            }
        }
    }
}
