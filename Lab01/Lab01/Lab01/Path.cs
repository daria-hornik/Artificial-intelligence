using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab01
{
    class Path
    {
        private static int STEP = 1;
        public List<Segment> SegmentList { get; set; }
        public Point ActualPoint { get; set; }
        public Point EndPoint { get; set; }

        public Path(Point startPoint, Point endPoint)
        {
            ActualPoint = startPoint;
            EndPoint = endPoint;
            SegmentList = new List<Segment>();
        }


        public int GetPathLength()
        {
            var sum = 0;
            foreach (var segment in SegmentList)
            {
                sum += segment.Length;
            }
            return sum;
        }

        public int GetNumberOfSegments()
        {
            return SegmentList.Count();
        }

        public void CreateSegment()
        {
            Segment newSegment = new Segment()
            {
                StartPoint = (Point) ActualPoint.Clone(),
                Length = STEP,
                Direction = GetTheBestDirection(),
            };
            AddSegment(newSegment);
            ChangeLocationOfActualPoint(newSegment.Direction);
        }

        private struct DirectionWithWeight
        {
            public int Weight;
            public Direction Direction;

            public DirectionWithWeight(Direction direction, int weight)
            {
                Weight = weight;
                Direction = direction;
            }
        }

        private Direction GetWeightedRandomDirection(List<DirectionWithWeight> directions, int accumulatedWeight)
        {
            Random rand = new Random();
            double r = rand.NextDouble() * accumulatedWeight;

            foreach (DirectionWithWeight direction in directions)
            { 
                if (direction.Weight >= r) 
                    return direction.Direction;
            }
            return default(Direction); 
        }

        public Direction GetTheBestDirection()
        {
            List<DirectionWithWeight> directionsWithWeight = new List<DirectionWithWeight>();
            int accumulatedWeight = 0;
            int xLevelDifference = ActualPoint.X - EndPoint.X;
            int yLevelDifference = ActualPoint.Y - EndPoint.Y;

            int weightX = Math.Abs(xLevelDifference);
            accumulatedWeight += weightX;

            int weightY = Math.Abs(yLevelDifference);
            accumulatedWeight += weightY;

            if (xLevelDifference > 0)
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Left, weightX));
            else
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Right, weightX));

            if (yLevelDifference > 0) 
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Down, weightY));
            else
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Up, weightY));

            return GetWeightedRandomDirection(directionsWithWeight, accumulatedWeight);
        }
  

        public void AddSegment(Segment segment)
        {
            if (SegmentList.Count() > 0)
            {
                var lastSegment = SegmentList.Last();
                if (lastSegment.IsHorizontalSegmenet() == segment.IsHorizontalSegmenet())
                {
                    if (lastSegment.Direction == segment.Direction)
                    {
                        lastSegment.Length++;
                        return;
                    }
                    else
                    {
                        lastSegment.Length--;
                        if (lastSegment.Length == 0)
                            SegmentList.Remove(lastSegment);
                        return;
                    }
                }
            }
            SegmentList.Add(segment);
        }

        private void ChangeLocationOfActualPoint(Direction direction)
        {
            switch(direction)
            {
                case Direction.Down:
                    ActualPoint.Y--;
                    break;
                case Direction.Up:
                    ActualPoint.Y++;
                    break;
                case Direction.Left:
                    ActualPoint.X--;
                    break;
                case Direction.Right:
                    ActualPoint.X++;
                    break;
            }
        }

        public bool IsPathFinished(Point actualPoint)
        {
            return ActualPoint.Equals(actualPoint);
        }

        public int GetPenalty()
        {
            return GetPathLength() + GetNumberOfSegments();
        }

        public void PathInfo()
        {
            Console.WriteLine("\t Długość: " + GetPathLength());
            Console.WriteLine("\t Liczba segmentów: " + GetNumberOfSegments());
            Console.WriteLine("\t Kara: " + GetPenalty());
            Console.WriteLine("\t Segmenty:");
            for (var i = 0; i < SegmentList.Count; i++)
            {
                Console.WriteLine($"\t\t {i + 1}. {SegmentList[i]}");
            }
        }
    }
}
