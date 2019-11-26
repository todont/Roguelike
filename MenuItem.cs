using System;
namespace Roguelike
{
    public class MenuItem
    {
        private string name;
        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }
        public virtual void OnClick()
        {
            //do nothing
        }
    }

    class MenuItemNewGame : MenuItem
    {
        public MenuItemNewGame()
        {
            Name = "New Game";
        }
        public override void OnClick()
        {
            Program.GameEngine.PlayGame();
        }
    }

    class MenuItemResume : MenuItem
    {
        public MenuItemResume()
        {
            Name = "Resume";
        }
    }

    class MenuItemSettings : MenuItem
    {
        public MenuItemSettings()
        {
            Name = "Settings";
        }
    }

    class MenuItemExit : MenuItem
    {
        public MenuItemExit()
        {
            Name = "Exit";
        }
        public override void OnClick()
        {
            Program.CleanUpAndExit();
        }
    }
}
