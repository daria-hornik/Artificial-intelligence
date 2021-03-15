using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Lab01
{
    class Path
    {
        private static int STEP = 1;
        private static int DEFAULT_PROBABILITY = 2;
        private static int BETTER_PROPABILITY = 3;
        private static int WORST_PROPABILITY = 1;

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

        public Segment CreateSegment()
        {
            return new Segment()
            {
                StartPoint = (Point) ActualPoint.Clone(),
                Length = STEP,
                Direction = GetWeightedRandomDirection(),
            };
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

        private Direction RandomDirection(List<DirectionWithWeight> directions)
        {
            Random rand = new Random();
            var sum = 0;

            foreach (var direction in directions)
                sum += direction.Weight;

            Direction[] tempTab = new Direction[sum];
            var index = 0;
            foreach (var direction in directions)
            {
                for (int j = 0; j < direction.Weight; j++)
                {
                    tempTab[index] = direction.Direction;
                    index++;
                }
            }
          
            int r = rand.Next(0, sum);
            return tempTab[r]; 
        }

        public Direction GetWeightedRandomDirection()
        {
            List<DirectionWithWeight> directionsWithWeight = new List<DirectionWithWeight>();
            int xLevelDifference = ActualPoint.X - EndPoint.X;
            int yLevelDifference = ActualPoint.Y - EndPoint.Y;

            if (xLevelDifference > 0)
            {
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Left, BETTER_PROPABILITY));
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Right, WORST_PROPABILITY));
            }
            else if (xLevelDifference == 0)
            {
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Left, DEFAULT_PROBABILITY));
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Right, DEFAULT_PROBABILITY));
            }
            else
            {
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Left, WORST_PROPABILITY));
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Right, BETTER_PROPABILITY));
            }

            if (yLevelDifference > 0)
            {
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Down, BETTER_PROPABILITY));
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Up, WORST_PROPABILITY));
            }
            else if (yLevelDifference == 0)
            {
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Down, DEFAULT_PROBABILITY));
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Up, DEFAULT_PROBABILITY));
            }
            else 
            {
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Down, WORST_PROPABILITY));
                directionsWithWeight.Add(new DirectionWithWeight(Direction.Up, BETTER_PROPABILITY));
            }
            return RandomDirection(directionsWithWeight);
        }

        public bool IsSegmentsOverlap(Segment segment)
        {
            if (SegmentList.Count() > 0)
            {
                var lastSegment = SegmentList.Last();
                if (lastSegment.IsHorizontalSegmenet() == segment.IsHorizontalSegmenet())
                {
                    if (lastSegment.Direction != segment.Direction)
                        return true;
                }
            }

            return false;
        }

        public void AddSegment(Segment segment)
        {
            ChangeLocationOfActualPoint(segment.Direction);

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

        public bool IsPathFinished()
        {
            return ActualPoint.Equals(EndPoint);
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

        public int CountIntersects(Segment segment)
        {
            if (SegmentList.Count == 0)
                return 0;

            var counter = 0;
            for (int i = 0; i < SegmentList.Count; i++)
            {
                if (SegmentList[i].IsIntersect(segment))
                    counter++;
            }
            return counter;
        }
    }
}
