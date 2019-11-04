using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Roguelike
{
    class Hero : Character
    {
        public int ExpPoints { get; set; }        //next code is strange, mb Andrey will give me an advice        public enum MenuAction        {            Up = ConsoleKey.UpArrow,
            Down = ConsoleKey.DownArrow,
            Right = ConsoleKey.RightArrow,
            Left = ConsoleKey.LeftArrow,            Confirm = ConsoleKey.Enter,
            Exit = ConsoleKey.Escape        }
        public MenuAction CurrentMenuAction { get; set; }        public void DoMenuAction()
        {
            return;
        }
        //end of strange code
        public enum GameAction
        {
            OpenInventory = ConsoleKey.E,
            PickUpItem = ConsoleKey.G
        }        public GameAction CurrentGameAction { get; set; }        public void DoGameAction()
        {
            switch (CurrentGameAction)
            {
                case GameAction.OpenInventory:
                    //OpenInventory(Hero.Inventory)
                    //Hero.Inventory is a list, containing many lists of
                    //weapon, armor, potion and so on..
                    break;
                case GameAction.PickUpItem:
                    //Hero.AddItem(Item)
                    break;
                default:
                    break;
            }
        }        public void HandleCollisions(char clashedSymbol)
        {
            switch (clashedSymbol)
            {
                case '#':
                    StepBack();
                    //TODO: make this as log function that depends on symbol we switching
                    //Console.SetCursorPosition(0, 0);
                    //Console.WriteLine("You hit a wall!");
                    break;
                //case 'monster tile'
                // Attack()
                default:
                    //Console.SetCursorPosition(0, 0);
                    //Console.WriteLine(new string(' ', 60));
                    break;
            }
        }    }}
