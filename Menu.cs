using System;
using System.Collections.Generic;

namespace Roguelike
{ 
    public class Menu
    {
        private List<string> MenuItem = new List<string>
        {
                "Resume",
                "New Game",
                "Settings",
                "Exit"
        };
        private int CurIndex;
        private int Count;

        private bool CheckKey(ConsoleKey key)
        {
            switch(key)
            {
                case ConsoleKey.DownArrow:
                    if (CurIndex == Count - 1) CurIndex = 0;
                    else CurIndex++;
                    break;
                case ConsoleKey.UpArrow:
                    if (CurIndex == 0) CurIndex = Count - 1;
                    else CurIndex--;
                    break;
                case ConsoleKey.Enter:
                    return true;
            }
            return false;
        }

        public string Process()
        {
            Count = MenuItem.Count;
            while (true)
            {
                Console.Clear();
                Console.ResetColor();
                int width = Console.WindowWidth;
                int height = Console.WindowHeight;

                for (int i = 0; i < Count; i++)
                {
                    Console.SetCursorPosition((width - MenuItem[i].Length) / 2,
                                              (height - Count) / 2 + i);

                    if (i == CurIndex)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ResetColor();

                    Console.WriteLine(MenuItem[i]);
                }

                var key = Console.ReadKey(true).Key;
                if (CheckKey(key) == true) return MenuItem[CurIndex];
            }
        }
    }
}
