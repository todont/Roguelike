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
            Location location = new Location();
            location.Draw();
	        Hero hero = new Hero();
            while (true)
            {
                hero.Move(); 
                location.Redraw();
            }
        }
    }
}
