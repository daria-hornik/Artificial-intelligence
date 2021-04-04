using System;
using System.Collections.Generic;
using System.Text;

namespace Lab02.Map
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<Point> Neighbours { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
            Neighbours = new List<Point>();
        }

        public override string ToString()
        {
            return $"({X}, {Y})" ;
        }
    }
}
