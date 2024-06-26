﻿using System;
using System.ComponentModel;

namespace Lab01
{
    enum Direction
    {
        Down = -1,
        Left = 0,
        Up = 1,
        Right = 2
    }

    class Segment : ICloneable
    {
        public Point StartPoint { get; set; }
        public int Length { get; set; }
        public Direction Direction { get; set; }

        public Segment()
        {
        }

        public Segment(Point point, Direction direction)
        {
            StartPoint = point;
            Length = 1;
            Direction = direction;
        }

        public Point GetEndPoint()
        {
            return GetNthSegmentPoint(Length);
        }

        public Point GetNthSegmentPoint(int n)
        {
            if (n > Length)
                return null;

            switch (Direction)
            {
                case Direction.Down:
                    return new Point(StartPoint.X, StartPoint.Y - n);
                case Direction.Up:
                    return new Point(StartPoint.X, StartPoint.Y + n);
                case Direction.Left:
                    return new Point(StartPoint.X - n, StartPoint.Y);
                case Direction.Right:
                    return new Point(StartPoint.X + n, StartPoint.Y);
            }

            return null;
        }

        public bool IsHorizontalSegmenet()
        {
            return Direction == Direction.Left || Direction == Direction.Right;
        }

        public override string ToString()
        {
            return $"Start: {StartPoint}, Kierunek: {Direction}, Koniec: {GetEndPoint()}";
        }

        private int VectorProduct(Point x, Point y, Point z)
        {
            int x1 = z.X - x.X;
            int y1 = z.Y - x.Y;
            int x2 = y.X - x.X;
            int y2 = y.Y - x.Y;
            return x1 * y2 - x2 * y1;
        }

        private bool CheckIfPointBelongsToSegment(Point x, Point y, Point z)
        {
            return Math.Min(x.X, y.X) <= z.X && z.X <= Math.Max(x.X, y.X) && Math.Min(x.Y, y.Y) <= z.Y &&
                   z.Y <= Math.Max(x.Y, y.Y);
        }

        public bool IsIntersect(Segment s)
        {
            var A = StartPoint;
            var B = GetEndPoint();
            var C = s.StartPoint;
            var D = s.GetEndPoint();

            int v1 = VectorProduct(C, D, A);
            int v2 = VectorProduct(C, D, B);
            int v3 = VectorProduct(A, B, C);
            int v4 = VectorProduct(A, B, D);

            //sprawdzenie czy się przecinają 
            if ((v1 > 0 && v2 < 0 || v1 < 0 && v2 > 0) && (v3 > 0 && v4 < 0 || v3 < 0 && v4 > 0)) return true;

            //sprawdzenie, czy koniec odcinka leży na drugim
            if (v1 == 0 && CheckIfPointBelongsToSegment(C, D, A)) return true;
            if (v2 == 0 && CheckIfPointBelongsToSegment(C, D, B)) return true;
            if (v3 == 0 && CheckIfPointBelongsToSegment(A, B, C)) return true;
            if (v4 == 0 && CheckIfPointBelongsToSegment(A, B, D)) return true;

            //odcinki nie mają punktów wspólnych
            return false;
        }

        public Direction GetPerpendicularDirection()
        {
            Random rn = new Random();
            var r = rn.Next(0, 2);
            if (IsHorizontalSegmenet())
            {
                if (r == 1)
                    return Direction.Up;
                return Direction.Down;
            }
            else
            {
                if (r == 1)
                    return Direction.Right;
                return Direction.Left;
            }
        }

        public bool IsPointBeforTheLast(Point point)
        {
            return GetNthSegmentPoint(Length - 1).Equals(point);
        }

        public bool IsSecondPoint(Point point)
        {
            return GetNthSegmentPoint(1).Equals(point);
        }

        public object Clone()
        {
            Segment copySegment = new Segment();
            copySegment.StartPoint = StartPoint.Clone() as Point;
            copySegment.Length = Length;
            copySegment.Direction = Direction;
            return copySegment;
        }

        public Direction GetOpposedDirection()
        {
            switch (Direction)
            {
                case Direction.Down:
                    return Direction.Up;
                case Direction.Up:
                    return Direction.Down;
                case Direction.Left:
                    return Direction.Right;
                default:
                    return Direction.Left;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if ((obj as Segment).StartPoint.Equals(StartPoint) && (obj as Segment).Length == Length &&
                (obj as Segment).Direction == Direction)
                return true;
            return false;
        }
    }
}
