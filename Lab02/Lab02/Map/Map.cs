using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab02.Map
{
    public class Map
    {
        private static int VectorProduct(Point x, Point y, Point z)
        {
            int x1 = z.X - x.X;
            int y1 = z.Y - x.Y;
            int x2 = y.X - x.X;
            int y2 = y.Y - x.Y;
            return x1 * y2 - x2 * y1;
        }

        private static bool CheckIfPointBelongsToLine(Point x, Point y, Point z)
        {
            return Math.Min(x.X, y.X) <= z.X
                && z.X <= Math.Max(x.X, y.X)
                && Math.Min(x.Y, y.Y) <= z.Y
                && z.Y <= Math.Max(x.Y, y.Y);
        }

        public static bool IsIntersect(Point firstX, Point firstY, Point secondX, Point secondY)
        {
            var A = firstX;
            var B = firstY;
            var C = secondX;
            var D = secondY;

            int v1 = VectorProduct(C, D, A);
            int v2 = VectorProduct(C, D, B);
            int v3 = VectorProduct(A, B, C);
            int v4 = VectorProduct(A, B, D);

            //sprawdzenie czy siê przecinaj¹ 
            if ((v1 > 0 && v2 < 0 || v1 < 0 && v2 > 0) && (v3 > 0 && v4 < 0 || v3 < 0 && v4 > 0))
                return true;

            //sprawdzenie, czy koniec odcinka le¿y na drugim
            /*if (v1 == 0 && CheckIfPointBelongsToLine(C, D, A)) return true;
            if (v2 == 0 && CheckIfPointBelongsToLine(C, D, B)) return true;
            if (v3 == 0 && CheckIfPointBelongsToLine(A, B, C)) return true;
            if (v4 == 0 && CheckIfPointBelongsToLine(A, B, D)) return true;*/

            //odcinki nie maj¹ punktów wspólnych
            return false;
        }

        public static double CountDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        static void Main(string[] args)
        {
            List<Point> variables = new List<Point>();
            Random random = new Random();
            int n = 6;

            for (int i = 0; i < n; i++)
                variables.Add(new Point(random.Next(100), random.Next(100)));

            var domains = new Dictionary<Point, List<Color>>();
            foreach (var variable in variables)
            {
                domains[variable] = new List<Color>() { Color.Blue, Color.Green, Color.Red, Color.Grey };
            }

            /* for (int i=0; i< n-1; i ++)
             {
                 if (i < n - 2)
                 {
                     variables[i].Neighbours.Add(variables[i + 1]);
                     variables[i + 1].Neighbours.Add(variables[i]);
                     variables[i].Neighbours.Add(variables[i + 2]);
                     variables[i + 2].Neighbours.Add(variables[i]);
                 }
                 else
                 {
                     variables[i].Neighbours.Add(variables[i + 1]);
                     variables[i+1].Neighbours.Add(variables[i]);
                 }
             }
             Dictionary<Point, List<Point>> neighbours = new Dictionary<Point, List<Point>>();
             foreach (var variable in variables)
             {
                 neighbours[variable] = new List<Point>(variable.Neighbours);
             }
             */

            Dictionary<Point, List<Point>> neighbours = new Dictionary<Point, List<Point>>();
            foreach (var variable in variables)
            {
                neighbours[variable] = new List<Point>(variable.Neighbours);
            }


            //po³¹czene ka¿dego punktu z ka¿dym
            foreach (var variable1 in variables)
            {
                foreach (var variable2 in variables)
                {
                    if (!variable2.Equals(variable1))
                    {
                        neighbours[variable1].Add(variable2);
                    }
                }
            }

            //usuwanie przeciêæ
            foreach (var variable1 in variables)
            {
                foreach (var variable2 in variables)
                {
                    if (!variable2.Equals(variable1))
                    {
                        for (int i = 0; i < neighbours[variable1].Count; i++)
                        {
                            for (int j = 0; j < neighbours[variable2].Count; j++)
                            {
                                var endPoint1 = (neighbours[variable1])[i];
                                var endPoint2 = (neighbours[variable2])[j];

                                if (!IsTheSameLine(variable1, endPoint1, variable2, endPoint2))
                                {
                                    if (IsIntersect(variable1, endPoint1, variable2, endPoint2))
                                    {
                                        if (CountDistance(variable1, neighbours[variable1][i]) >= CountDistance(variable2, neighbours[variable2][j]))
                                        {
                                            neighbours[variable1].Remove(endPoint1);
                                            i--;
                                        }
                                        else
                                        {
                                            neighbours[variable2].Remove(endPoint2);
                                            j--;
                                        }
                                    };
                                }

                            }
                        }
                    }
                }
            }



            CSP<Point, Color> csp = new CSP<Point, Color>(variables, domains, neighbours);

            /*  foreach (var value in variables)
              {
                  foreach (var neighbour in value.Neighbours)
                      csp.AddConstraint(new MapConstraint(value, neighbour));
              }*/

            foreach (var variable in neighbours.Keys)
            {
                foreach (var neighbour in neighbours[variable])
                {
                    csp.AddConstraint(new MapConstraint(variable, neighbour));
                }
            }

            var solution = csp.BackTrackingSearch(new Dictionary<Point, Color>());
            if (solution is null)
                Console.WriteLine("brak rozwi¹zania");

            foreach (var dict in solution)
            {
                Console.WriteLine($"{dict.Key}: {dict.Value}");
            }
        }

        private static bool IsTheSameLine(Point variable1, Point endPoint1, Point variable2, Point endPoint2)
        {
            return variable1.X == endPoint2.X && variable1.Y == endPoint2.Y &&
                variable2.X == endPoint1.X && variable2.Y == endPoint1.Y;
        }
    }
}
