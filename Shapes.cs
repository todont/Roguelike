using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Rectangle
    {
        public Point Offset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Location { get; set; }
        public Rectangle(int width, int height, Point location)
        {
            Width = width;
            Height = height;
            Location = location;
        }

        public Rectangle(int width, int height, int x, int y)
        {
            Width = width;
            Height = height;
            Location.X = x;
            Location.Y = y;
        }

        public Rectangle() { }
    }
}
