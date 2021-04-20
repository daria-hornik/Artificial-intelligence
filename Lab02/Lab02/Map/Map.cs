using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Lab02.Map
{

    public class Map
    {
        public static bool SaveData(CSP<Point, Color> csp)
        {
            using StreamWriter file = new StreamWriter(@"C:\Users\horni\source\repos\Artificial-intelligence\Lab02\Data.txt");
            foreach (var variable in csp.Variables)
            {
                file.WriteLine(variable.X.ToString() + ',' + variable.Y.ToString());
            }
            file.WriteLine();

            foreach (var variable in csp.Variables)
            {
                file.WriteLine(variable.X.ToString() + ',' + variable.Y.ToString());
                foreach (Point neighbour in csp.Neighbours[variable])
                {
                    file.WriteLine(neighbour.X.ToString() + ',' + neighbour.Y.ToString());
                }
                file.WriteLine();
            }
            return true;
        }

        public static (List<Point>, Dictionary<Point, List<Point>>) LoadData()
        {
            string fileName = @"C:\Users\horni\source\repos\Artificial-intelligence\Lab02\Data.txt";
            var variables = new List<Point>();
            var neighbours = new Dictionary<Point, List<Point>>();

            using (StreamReader streamReader = File.OpenText(fileName))
            {
                string text = streamReader.ReadToEnd();
                string[] lines = text.Split(Environment.NewLine);
                int i = 0;

                while (i < lines.Length)
                {
                    if (lines[i]=="")
                    {
                        i++;
                        break;
                    }
                    Point variable = new Point(int.Parse(lines[i].Split(',')[0]), int.Parse(lines[i].Split(',')[1]));
                    variables.Add(variable);
                    neighbours.Add(variable, new List<Point>());
                    i++;
                }

                while (i < lines.Length-1)
                {
                    var variable = variables.Find(x => x.X == int.Parse(lines[i].Split(',')[0]) && x.Y == int.Parse(lines[i].Split(',')[1]));
                    i++;
                    while (lines[i] != "\n" && lines[i] != "")
                    {
                        var neighbour = variables.Find(x => x.X == int.Parse(lines[i].Split(',')[0]) && x.Y == int.Parse(lines[i].Split(',')[1]));
                        neighbours[variable].Add(neighbour);
                        i++;
                    }
                    i++;
                }
            }
            return (variables, neighbours);
        }

        private static int VectorProduct(Point x, Point y, Point z)
        {
            int x1 = z.X - x.X;
            int y1 = z.Y - x.Y;
            int x2 = y.X - x.X;
            int y2 = y.Y - x.Y;
            return x1 * y2 - x2 * y1;
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

            //sprawdzenie czy się przecinają 
            if ((v1 > 0 && v2 < 0 || v1 < 0 && v2 > 0) && (v3 > 0 && v4 < 0 || v3 < 0 && v4 > 0))
                return true;

            return false;
        }

        public static double CountDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        private static bool IsTheSameLine(Point variable1, Point endPoint1, Point variable2, Point endPoint2)
        {
            return variable1.X == endPoint2.X && variable1.Y == endPoint2.Y &&
                variable2.X == endPoint1.X && variable2.Y == endPoint1.Y;
        }

        public static CSP<Point, Color> GetProblem(int n)
        {
            List<Point> variables = new List<Point>();
            Random random = new Random();
            for (int i = 0; i < n; i++)
                variables.Add(new Point(random.Next(100), random.Next(100)));

            var domains = new Dictionary<Point, List<Color>>();
            foreach (var variable in variables)
                domains[variable] = new List<Color>() { Color.Blue, Color.Green, Color.Red, Color.Grey };


            Dictionary<Point, List<Point>> neighbours = new Dictionary<Point, List<Point>>();
            foreach (var variable in variables)
                neighbours[variable] = new List<Point>(variable.Neighbours);


            //połączene każdego punktu z każdym
            foreach (var variable1 in variables)
            {
                foreach (var variable2 in variables)
                {
                    if (!variable2.Equals(variable1))
                        neighbours[variable1].Add(variable2);
                }
            }

            //usuwanie przecięć
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
                                            break;
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

            foreach (var variable in variables)
            {
                foreach (var neighbour in neighbours[variable])
                {
                    csp.AddConstraint(new MapConstraint(variable, neighbour));
                }
            }

            return csp;
        }

        static void Main(string[] args)
        {
           // var MyCsp = GetProblem(6);
           // SaveData(MyCsp);

            var (variables, neighbours) = LoadData();
            var domains = new Dictionary<Point, List<Color>>();
            foreach (var variable in variables)
                domains[variable] = new List<Color>() { Color.Blue, Color.Green, Color.Red, Color.Grey };

            CSP<Point, Color> csp = new CSP<Point, Color>(variables, domains, neighbours);

            foreach (var variable in variables)
            {
                foreach (var neighbour in neighbours[variable])
                {
                    csp.AddConstraint(new MapConstraint(variable, neighbour));
                }
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var solution = csp.BackTrackingSearch(new Dictionary<Point, Color>());
            sw.Stop();

            if (solution is null)
            {
                Console.WriteLine("brak rozwiazania");
                return;
            }

            Console.WriteLine("BT: Czas potrzebny do znalezienia rozwiązania: {0}", sw.Elapsed.TotalSeconds*10000000);
            Console.WriteLine(CSP<Point, Color>.NodeCounter);
            foreach (var dict in solution)
            {
                Console.WriteLine($"{dict.Key}: {dict.Value}");
            }

            var counter = CSP<Point, Color>.NodeCounter;
            var (variables1, neighbours1) = LoadData();
            var domains1 = new Dictionary<Point, List<Color>>();
            foreach (var variable1 in variables1)
                domains1[variable1] = new List<Color>() { Color.Blue, Color.Green, Color.Red, Color.Grey };

            CSP<Point, Color> csp1 = new CSP<Point, Color>(variables1, domains1, neighbours1);

            foreach (var variable1 in variables1)
            {
                foreach (var neighbour1 in neighbours1[variable1])
                {
                    csp1.AddConstraint(new MapConstraint(variable1, neighbour1));
                }
            }

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            var solution1 = csp.ForwardCheck(new Dictionary<Point, Color>());
            sw1.Stop();

            if (solution1 is null)
            {
                Console.WriteLine("brak rozwi¹zania");
                return;
            }

            Console.WriteLine("FC: Czas potrzebny do znalezienia rozwiązania: {0}", sw1.Elapsed.TotalSeconds*10000000);
            Console.WriteLine(CSP<Point, Color>.NodeCounter - counter);
            foreach (var dict in solution1)
            {
                Console.WriteLine($"{dict.Key}: {dict.Value}");
            }
        }
    }
}
