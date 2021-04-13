using System.Collections.Generic;

namespace Lab02.Map
{
    class MapConstraint : Constraint<Point, Color>
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }

        public MapConstraint(Point point1, Point point2) : base(new List<Point> { point1, point2 })
        {

            Point1 = point1;
            Point2 = point2;
        }

        public override bool IsSatisfied(Dictionary<Point, Color> assigment)
        {
            if (!assigment.ContainsKey(Point1) || !assigment.ContainsKey(Point2))
            {
                return true;
            }

            return assigment[Point1] != assigment[Point2];
        }
    }
}