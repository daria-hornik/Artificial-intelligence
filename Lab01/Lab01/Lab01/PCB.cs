using System;
using System.Collections.Generic;

namespace Lab01
{
    class PCB
    {
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public List<(Point, Point)> PointList { get; set; }
        public List<Path> Paths { get; set; }

        public PCB(int x, int y, List<(Point, Point)> pointList)
        {
            PointList = pointList;
            Paths = new List<Path>();
            BoardX = x;
            BoardY = y;
        }

        public int CountPenaltyFunction()
        {
            var sum = 0;
            foreach (var path in Paths)
                sum += path.GetPenalty();
            sum += CountIntersection();
            return sum;
        }

        public void BuildRandomPaths()
        {
            for (int i = 0; i < PointList.Count; i++)
                Paths.Add(new Path(PointList[i].Item1, PointList[i].Item2));

            for (int i = 0; i < PointList.Count; i++)
            {
                while (!Paths[i].IsPathFinished(PointList[i].Item2))
                {
                    Paths[i].CreateSegment();

                }
            }
        }

        public void PathsInfo()
        {
            int i = 1;
            foreach (var path in Paths)
            {
                Console.WriteLine($"{i}. ścieżka:");
                path.PathInfo();
                i++;
            }

            Console.WriteLine(CountPenaltyFunction());
            Console.WriteLine(CountIntersection());
        }

        public int CountIntersection()
        {
            int counter = 0;
            for (int i = 0; i < Paths.Count; i++)
            {
                for (int j = i + 1; j < Paths.Count; j++)
                {
                    for (int s1 = 0; s1 < Paths[i].SegmentList.Count; s1++)
                    {
                        for (int s2 = 0; s2 < Paths[j].SegmentList.Count; s2++)
                        {
                            Paths[i].SegmentList[s1].IsIntersect(Paths[j].SegmentList[s2]);
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }
    }
}
