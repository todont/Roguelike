using System;
using System.Collections.Generic;

namespace Roguelike
{
    public class MenuBase
    {
        protected List<MenuItem> MenuItems;
        protected int CurIndex = 0;
        protected ConsoleColor FgColor = ConsoleColor.Green;

        protected virtual void Print(int offsetX, int offsetY)
        {
            for (int i = 0; i < MenuItems.Count; i++)
            {
                Console.SetCursorPosition(offsetX, offsetY + i);
                if (i == CurIndex)
                    Console.ForegroundColor = FgColor;
                Console.WriteLine(MenuItems[i].Name);
                Console.ResetColor();
            }
        }

        protected bool ProcessKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    if (CurIndex == MenuItems.Count - 1) CurIndex = 0;
                    else CurIndex++;
                    break;
                case ConsoleKey.UpArrow:
                    if (CurIndex == 0) CurIndex = MenuItems.Count - 1;
                    else CurIndex--;
                    break;
                case ConsoleKey.Enter:
                    return true;
            }
            return false;
        }

        public void Process(int offsetX = 0, int offsetY = 0)
        {
            while (true)
            {
                Print(offsetX, offsetY);
                var key = Console.ReadKey().Key;
                int prevIndex = CurIndex;

                if (ProcessKey(key) == true)
                    MenuItems[CurIndex].OnClick();
            }
        }
    }
}
