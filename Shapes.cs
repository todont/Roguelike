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
    class Point
    {   [DataMember]
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Point() { }

        public bool IsPointEqual(Point x)
        {
            return ((x.X == this.X) && (x.Y == this.Y));
        }
        public void SetValue(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public void SetValue(Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }
        public double GetDistance(Point x)
        {
            return Math.Sqrt(Math.Pow(x.X - this.X, 2) + Math.Pow(x.Y - this.Y, 2));
        }
    }
    [DataContract]
    class Rectangle
    {   [DataMember]
        public Point Offset { get; set; }
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
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
