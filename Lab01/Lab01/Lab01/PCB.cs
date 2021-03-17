using System;
using System.Collections.Generic;

namespace Lab01
{
    class PCB: ICloneable
    {
        private static int INTERSECTION_WEIGHT = 20;
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

        //Funkcje ograniczające
        public bool IsIntersectWithOthers(Path path, Segment segment)
        {
            foreach (var pathInBoard in Paths)
            {
                if (!pathInBoard.Equals(path))
                {
                    if (pathInBoard.CountIntersects(segment) != 0)
                        return true;
                }
            }
            return false;
        }

        public bool IsIntersecToMyself(Path path, Segment segment)
        {
            if (path.IsSegmentsOverlap(segment))
                return false;

            foreach (var pathInBoard in Paths)
            {
                if (pathInBoard.Equals(path))
                {
                    var intersectCount = pathInBoard.CountIntersects(segment);
                    if (intersectCount == 1 || intersectCount == 0)
                        return false;
                    else 
                        return true;
                }
            }
            return true;
        }

        public bool IsActualPointAStartEndPointOtherPaths(Path path, Point point)
        {
            for (var i = 0; i<Paths.Count; i++)
            {
                if (Paths[i] != path)
                {
                    if (PointList[i].Item2.Equals(point) || PointList[i].Item1.Equals(point))
                        return true;
                }
            }
            return false;
        }

        private bool IsInBoard(Point point)
        {
            return point.X < BoardX && point.Y < BoardY && point.X >= 0 && point.Y >= 0;
        }

        public bool IsAllConstraintCorrect(Path path, Segment segment)
        {
            return !IsIntersectWithOthers(path, segment) && IsInBoard(segment.GetEndPoint()) &&
                   !IsIntersecToMyself(path, segment)
                   && !IsActualPointAStartEndPointOtherPaths(path, segment.GetEndPoint());
        }

        public void BuildRandomPaths()
        {
            for (int i = 0; i < PointList.Count; i++)
                Paths.Add(new Path( (Point) PointList[i].Item1.Clone(), (Point) PointList[i].Item2.Clone()));

            for (int i = 0; i < Paths.Count; i++)
            {
                while (!Paths[i].IsPathFinished())
                {
                    var isCorrectSegment = false;
                    while (!isCorrectSegment)
                    {
                        Segment newSegment = Paths[i].CreateSegment();
                        if (IsAllConstraintCorrect(Paths[i], newSegment))
                        {
                            Paths[i].AddSegment(newSegment);
                            isCorrectSegment = true;
                        }
                    }
                }
            }
        }


        public void PathsInfo()
        {
            int i = 1;
            foreach (var path in Paths)
            {
                Console.WriteLine("===========================");
                Console.WriteLine($"{i}. ścieżka:");
                path.PathInfo();
                i++;
            }

            Console.WriteLine($"Całkowita kara: {CountPenaltyFunction()}");
            Console.WriteLine($"Kara za przecięcia: {CountIntersection()}");
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
                            if (Paths[i].SegmentList[s1].IsIntersect(Paths[j].SegmentList[s2]))
                                counter++;
                        }
                    }
                }
            }
            return INTERSECTION_WEIGHT * counter;
        }

        public object Clone()
        {
            foreach (var valueTuple in PointList)
            {
                valueTuple.Item1.Clone();
                valueTuple.Item2.Clone();
            }
            PCB copyPcb = new PCB(BoardX, BoardY, PointList);
            return copyPcb;
        }
    }
}
