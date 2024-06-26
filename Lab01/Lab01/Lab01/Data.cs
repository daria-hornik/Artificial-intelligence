﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Int32;

namespace Lab01
{
    class Data
    {
        public static (int, int, List<(Point, Point)>) ReadDataFromFile()
        {
            using (var sr = new StreamReader(@"C:\Users\horni\source\repos\Artificial-intelligence\Lab01\lab01_problemy_testowe\zad2.txt"))
            {
                var boardDimension = sr.ReadLine();
                int x = Parse(boardDimension.Split(';')[0]);
                var y = Parse(boardDimension.Split(';')[1]);

                List<(Point, Point)> pointsList = new List<(Point, Point)>();
                foreach (var line in sr.ReadToEnd().Split('\n'))
                {
                    var coordinates = line.Split(';').Select(Parse).ToList(); ;
                    pointsList.Add((new Point(coordinates[0], coordinates[1]), new Point(coordinates[2], coordinates[3])));
                }
                return (x, y, pointsList);
            }
        }
    }
}
