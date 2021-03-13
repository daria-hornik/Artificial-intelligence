using System;

namespace Lab01
{
    class Point: ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if ((obj as Point).X == X)
                if ((obj as Point).Y == Y)
                    return true;
            
            return false;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
