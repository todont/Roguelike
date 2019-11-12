using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Roguelike
{
    class Map
    {
        // the world grid: a 2d array of tiles
        private static bool[,] world;
        public string[] WorldAscii { get; private set; }

        // size in the world in sprite tilese
        private static int WorldWidth = 300;
        private static int WorldHeight = 300;
        
        private double ChanceToStartAlive = 0.4d;
        private int DeathLimit = 3;
        private int BirthLimit = 4;
        private int NumberOfSteps = 2;

        public void Build()
        {
            world = GenerateMap();
            WriteMapIntoFile();
        }

        private double GetRandomNum()
        {
            var rand = new Random();
            return rand.NextDouble();
        }
        //Used to create a new world - it grabs the values from
        //the HTML form so you can affect the world gen :)
        /*function recreate(form)
        {
            birthLimit = form.birthLimit.value;
            deathLimit = form.deathLimit.value;
            chanceToStartAlive = form.initialChance.value;
            numberOfSteps = form.numSteps.value;
 
            world = generateMap();
            redraw();
        }*/

        private bool[,] GenerateMap()
        {
            //Create a new map
            bool[,] cellmap = new bool[WorldHeight, WorldWidth];
            //Set up the map with random values
            cellmap = InitialiseMap(cellmap);
            //And now run the simulation for a set number of steps
            for (int i = 0; i < NumberOfSteps; i++)
            {
                cellmap = DoSimulationStep(cellmap);
            }

            return cellmap;
        }

        private bool[,] InitialiseMap(bool[,] map)
        {
            for (int x = 0; x < WorldHeight; x++)
            {
                for (int y = 0; y < WorldWidth; y++)
                {
                    if (GetRandomNum() < ChanceToStartAlive)
                    {
                        map[x, y] = true;
                    }
                }
            }
            return map;
        }



        private bool[,] DoSimulationStep(bool[,] oldMap)
        {
            bool[,] newMap = new bool[oldMap.GetLength(0), oldMap.GetLength(1)];
            //Loop over each row and column of the map
            for (int x = 0; x < oldMap.GetLength(0); x++)
            {
                for (int y = 0; y < oldMap.GetLength(1); y++)
                {
                    int nbs = CountAliveNeighbours(oldMap, x, y);
                    //The new value is based on our simulation rules
                    //First, if a cell is alive but has too few neighbours, kill it.
                    if (oldMap[x, y])
                    {
                        if (nbs < DeathLimit)
                        {
                            newMap[x, y] = false;
                        }
                        else
                        {
                            newMap[x, y] = true;
                        }
                    } //Otherwise, if the cell is dead now, check if it has the right number of neighbours to be 'born'
                    else
                    {
                        if (nbs > BirthLimit)
                        {
                            newMap[x, y] = true;
                        }
                        else
                        {
                            newMap[x, y] = false;
                        }
                    }
                }
            }
            return newMap;
        }

        //This function counts the number of solid neighbours a tile has
        //Returns the number of cells in a ring around (x,y) that are alive.
        private int CountAliveNeighbours(bool[,] map, int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int neighbour_x = x + i;
                    int neighbour_y = y + j;
                    //If we're looking at the middle point
                    if (i == 0 && j == 0)
                    {
                        //Do nothing, we don't want to add ourselves in!
                    }
                    //In case the index we're looking at it off the edge of the map
                    else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= map.GetLength(0) || neighbour_y >= map.GetLength(1))
                    {
                        count = count + 1;
                    }
                    //Otherwise, a normal check of the neighbour
                    else if (map[neighbour_x, neighbour_y])
                    {
                        count = count + 1;
                    }
                }
            }
            return count;
        }

        /*
        Extra credit assignment! Let's loop through the
        map and place treasure in the nooks and crannies.
        */
        /*public void placeTreasure(bool[, ] world)
         {
             //How hidden does a spot need to be for treasure?
             //I find 5 or 6 is good. 6 for very rare treasure.
             int treasureHiddenLimit = 5;
             for (int x = 0; x < WorldWidth; x++)
             {
                 for (int y = 0; y < WorldHeight; y++)
                 {
                     if (!world[x,y])
                     {
                         int nbs = CountAliveNeighbours(world, x, y);
                         if (nbs >= treasureHiddenLimit)
                         {
                             placeTreasure(x, y);
                         }
                     }
                 }
             }
         } */

        private void WriteMapIntoFile()
        {
            // clear the screen
            Console.Clear();
            WorldAscii = new string[WorldHeight];
            for (var x = 0; x < WorldHeight; x++)
            {
                for (var y = 0; y < WorldWidth; y++)
                {
                    //truee == wall, false == ".", 2 == treasure; 0 == wall, 1 == ".".
                    if (world[x, y])
                        WorldAscii[x] += "▒";
                    //Console.Write("#");
                    //The colour of treasure!
                    else
                        WorldAscii[x] += ".";
                    //Console.Write(".");
                }
                //Console.WriteLine();
            }
            File.WriteAllLines("Locations/location1.txt", WorldAscii);
            //return WorldAscii;
        }
    }
}