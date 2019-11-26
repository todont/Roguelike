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
            Program.GameEngine.Init();
            Program.GameEngine.PlayGame();
        }
    }

    class MenuItemResume : MenuItem
    {
        public MenuItemResume()
        {
            Name = "Resume";
        }
        public override void OnClick()
        {
            if (Program.GameEngine.GameStarted == true)
                Program.GameEngine.PlayGame();
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
