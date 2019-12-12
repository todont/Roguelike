using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Roguelike
{   [DataContract]
    class Monster : Character
    {
        public Monster(Point coords, int hitPoints, int rangeOfVision, Character.Speed speed, string name, char symbol)
        {
            Coords = coords;
            PrevCoords = new Point(coords.X, coords.Y);
            HitPoints = hitPoints; //should depend on class/hit dices
            RangeOfVision = rangeOfVision;
            CurrentSpeed = speed;
            Name = name;
            Symbol = symbol;
            IsMoved = false;
            ChanceToGoUp = 0.25;
            ChanceToGoDown = 0.25;
            ChanceToGoRight = 0.25;
            ChanceToGoLeft = 0.25;
            StandardDecreasingChance = 0.025;
            PrevDistance = 0;
        }
        //Tile MonsterTile (color and shape of 
        // symbol that represents monster)

        [DataMember]
        private double ChanceToGoUp;
        [DataMember]
        private double ChanceToGoDown;
        [DataMember]
        private double ChanceToGoRight;
        [DataMember]
        private double ChanceToGoLeft;
        [DataMember]
        private double StandardDecreasingChance;
        [DataMember]
        private double PrevDistance;
        public enum GameAction 
        {
            Attack
        }
        [DataMember]
        public GameAction CurrentGameAction { get; set; }
        private void DecreaseChances()
        {
            ChanceToGoUp -= StandardDecreasingChance;
            ChanceToGoDown -= StandardDecreasingChance;
            ChanceToGoRight -= StandardDecreasingChance;
            ChanceToGoLeft -= StandardDecreasingChance;
        }
        private void IncreaseChances()
        {
            ChanceToGoUp += StandardDecreasingChance;
            ChanceToGoDown += StandardDecreasingChance;
            ChanceToGoRight += StandardDecreasingChance;
            ChanceToGoLeft += StandardDecreasingChance;
        }
        private void SetChances(double distance, BaseCharacter.MoveAction direction)
        {
            if (distance < PrevDistance)
            {
                DecreaseChances();
                switch (direction)
                {
                    case MoveAction.Up:
                        ChanceToGoUp += StandardDecreasingChance * 4;
                        break;
                    case MoveAction.Down:
                        ChanceToGoDown += StandardDecreasingChance * 4;
                        break;
                    case MoveAction.Right:
                        ChanceToGoRight += StandardDecreasingChance * 4;
                        break;
                    case MoveAction.Left:
                        ChanceToGoLeft += StandardDecreasingChance * 4;
                        break;
                }
            }
            else
            {
                IncreaseChances();
                switch (direction)
                {
                    case MoveAction.Up:
                        ChanceToGoUp -= StandardDecreasingChance * 4;
                        break;
                    case MoveAction.Down:
                        ChanceToGoDown -= StandardDecreasingChance * 4;
                        break;
                    case MoveAction.Right:
                        ChanceToGoRight -= StandardDecreasingChance * 4;
                        break;
                    case MoveAction.Left:
                        ChanceToGoLeft -= StandardDecreasingChance * 4;
                        break;
                }
            }
        }
        private void SetStandardChances()
        {
            ChanceToGoUp = 0.25;
            ChanceToGoDown = 0.25;
            ChanceToGoRight = 0.25;
            ChanceToGoLeft = 0.25;
        }
        private void SetMoveAction()
        {
            double randnumber = Program.GameEngine.GameRandom.NextDouble();
            if (randnumber < ChanceToGoUp)
            {
                CurrentMoveAction = BaseCharacter.MoveAction.Up;
                return;
            }
            if (randnumber < ChanceToGoUp + ChanceToGoDown)
            {
                CurrentMoveAction = BaseCharacter.MoveAction.Down;
                return;
            }
            if (randnumber < ChanceToGoUp + ChanceToGoDown + ChanceToGoRight)
            {
                CurrentMoveAction = BaseCharacter.MoveAction.Right;
                return;
            }
            CurrentMoveAction = BaseCharacter.MoveAction.Left;
            Console.WriteLine(CurrentMoveAction);
        }

        public void MoveTo(BaseCharacter enemy)
        {
            double distance;

            if (Coords.GetDistance(enemy.Coords) > RangeOfVision)
            {
                IsMoved = false;
                return;
            }
            SetMoveAction();
            Move();
            distance = Coords.GetDistance(enemy.Coords);
            SetChances(distance, CurrentMoveAction);
            if (IsMoved) PrevDistance = distance;
        }
        protected override bool HandleCollisions(char mapSymbol, char entitySymbol)
        {
            switch (mapSymbol)
            {
                case '▒':
                    SetStandardChances();
                    return false;
                default:
                    return true;
            }
        }
    }
}
