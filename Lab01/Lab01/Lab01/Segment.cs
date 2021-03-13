using System;

namespace Lab01
{
    enum Direction
    {
        Down = -1,
        Left = 0,
        Up = 1,
        Right = 2
    }

    class Segment
    {
        public Point StartPoint { get; set; }
        public int Length { get; set; }
        public Direction Direction { get; set; }
       

        public Point GetEndPoint()
        {
            switch (Direction)
            {
                case Direction.Down:
                    return new Point(StartPoint.X, StartPoint.Y - Length);
                case Direction.Up:
                    return new Point(StartPoint.X, StartPoint.Y + Length);
                case Direction.Left:
                    return new Point(StartPoint.X - Length, StartPoint.Y);
                case Direction.Right:
                    return new Point(StartPoint.X + Length, StartPoint.Y);
            }
            return null;
        }
        public bool IsVerticalSegment()
        {
            return Direction == Direction.Down || Direction == Direction.Up;
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

        private bool CheckIsPointBelongsToSegment(Point x, Point y, Point z)
        {
            return Math.Min(x.X, y.X) <= z.X && z.X <= Math.Max(x.X, y.X) && Math.Min(x.Y, y.Y) <= z.Y && z.Y <= Math.Max(x.Y, y.Y);
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
            if (v1 == 0 && CheckIsPointBelongsToSegment(C, D, A)) return true;
            if (v2 == 0 && CheckIsPointBelongsToSegment(C, D, B)) return true;
            if (v3 == 0 && CheckIsPointBelongsToSegment(A, B, C)) return true;
            if (v4 == 0 && CheckIsPointBelongsToSegment(A, B, D)) return true;

            //odcinki nie mają punktów wspólnych
            return false;
        }

    }
}
