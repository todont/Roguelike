using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Roguelike
{
    class Hero : Character
    {
        public Hero(Point coords, int hitPoints, int expPoints, Hero.Speed speed, string name)
        {
            Coords = coords;
            PrevCoords = coords;
            HitPoints = hitPoints; //should depend on class/hit dices
            ExpPoints = expPoints;
            CurrentSpeed = speed;
            Name = name;
        }
        public int ExpPoints { get; set; }
        public enum GameAction
        {
            OpenInventory = ConsoleKey.E,
            PickUpItem = ConsoleKey.G,
            Exit = ConsoleKey.Escape,
            Attack,
            DropItem
        }        public GameAction CurrentGameAction { get; set; }        public void DoGameAction()
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
                case GameAction.Attack:
                    //attack
                    break;
                case GameAction.DropItem:
                    //drop item
                    break;
                default:
                    break;
            }
        }        protected override bool HandleCollisions(char clashedSymbol)
        {
            switch (clashedSymbol)
            {
                case '▒':
                    //make this as log function that depends on symbol we switching
                    Console.SetCursorPosition(Program.GameEngine.InfoBorder.Offset.X, Program.GameEngine.InfoBorder.Offset.Y);
                    Console.WriteLine("You hit a wall!");
                    return false;
                case 'S': //snake
                    //CurrentGameAction = attack
                    return false;
                case '$': //money
                    //CurrentGameAction = pick up item
                    return true;
                default:
                    Console.SetCursorPosition(Program.GameEngine.InfoBorder.Offset.X, Program.GameEngine.InfoBorder.Offset.Y);
                    Console.WriteLine(new string(' ', 20));
                    return true;
            }
        }    }}