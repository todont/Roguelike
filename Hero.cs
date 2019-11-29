using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Roguelike
{
    class Hero : Character
    {
        public Hero(Point coords, int hitPoints, int expPoints, int rangeOfVision, Character.Speed speed, string name)
        {
            Coords = coords;
            PrevCoords = new Point(coords.X, coords.Y);
            HitPoints = hitPoints; //should depend on class/hit dices
            ExpPoints = expPoints;
            RangeOfVision = rangeOfVision;
            CurrentSpeed = speed;
            Name = name;
            Symbol = '@';
            IsMoved = false;
        }
        public enum GameAction
        {
            OpenInventory = ConsoleKey.E,
            PickUpItem = ConsoleKey.G,
            Exit = ConsoleKey.Escape,
            Attack,
            DropItem
        }        public int ExpPoints { get; set; }        public GameAction CurrentGameAction { get; set; }        public void DoGameAction()
        {
            switch (CurrentGameAction)
            {
                case GameAction.OpenInventory:
                    //OpenInventory(Hero.Inventory)
                    //Hero.Inventory is a list, containing many lists of
                    //weapon, armor, potion and so on..
                    Console.SetCursorPosition(Program.GameEngine.InfoBorder.Offset.X, Program.GameEngine.InfoBorder.Offset.Y);
                    Console.WriteLine("You open an inventory");
                    break;
                case GameAction.PickUpItem:
                    //Hero.AddItem(Item)
                    Console.SetCursorPosition(Program.GameEngine.InfoBorder.Offset.X, Program.GameEngine.InfoBorder.Offset.Y);
                    Console.WriteLine("You pick up an item");
                    break;
                case GameAction.Exit:
                    Program.GameEngine.StartMenu();
                    break;
                case GameAction.Attack:
                    //attack
                    break;
                case GameAction.DropItem:
                    //drop item
                    Console.SetCursorPosition(Program.GameEngine.InfoBorder.Offset.X, Program.GameEngine.InfoBorder.Offset.Y);
                    Console.WriteLine("You drop an item");
                    break;
                default:
                    //Console.SetCursorPosition(Program.GameEngine.InfoBorder.Offset.X, Program.GameEngine.InfoBorder.Offset.Y);
                    //Console.WriteLine("You do nothing");
                    break;
            }
        }        protected override bool HandleCollisions(char clashedSymbol)
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