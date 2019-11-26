using System;
using System.Collections.Generic;

namespace Roguelike
{ 
    public class StartingMenu : MenuBase
    {
        public StartingMenu()
        {
            MenuItems = new List<MenuItem>
            {
                new MenuItemNewGame(),
                new MenuItemResume(),
                new MenuItemSettings(),
                new MenuItemExit()
            };
        }

        protected override void Print(int offsetX, int offsetY)
        {
            Console.Clear();
            for (int i = 0; i < MenuItems.Count; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - MenuItems[i].Name.Length) / 2,
                                          Console.WindowHeight / 2 + i);
                if (i == CurIndex)
                    Console.ForegroundColor = FgColor;
                Console.WriteLine(MenuItems[i].Name);
                Console.ResetColor();
            }
        }
    }
}
