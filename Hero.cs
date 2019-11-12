using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Roguelike
{
    class Hero : Character
    {
        public int ExpPoints { get; set; }
        public enum GameAction
        {
            OpenInventory = ConsoleKey.E,
            PickUpItem = ConsoleKey.G,
            Exit = ConsoleKey.Escape
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
                case GameAction.Exit:
                    Program.GameEngine.StartMenu();
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
