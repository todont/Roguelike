using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class GameEngine
    {
        public void StartGame()
        {
            //здесь должны быть настройки, lastVisitedLocation ??
            Location location = new Location();
	        Hero hero = new Hero();
            location.Draw(hero);
            while (true)
            {
                //hero.Move();
                //location.Redraw();
            }
        }
    }
}
