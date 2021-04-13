using System;
using System.Collections.Generic;
using Lab02;

namespace Lab02.MapColoring
{
    class Program
    {
       /* static void Main(string[] args)
        {
            List<string> variables = new List<string> {"Western Australia", "Northern Territory", "South Australia",
                             "Queensland", "New South Wales", "Victoria", "Tasmania"};

            var Domains = new Dictionary<string, List<string>>();
            foreach (var variable in variables)
            {
                Domains[variable] = new List<string>() { "red", "green", "blue"};
            }

            CSP<string, string> csp = new CSP<string, string>(variables, Domains);
            csp.AddConstraint(new MapConstraint("Western Australia", "Northern Territory"));
            csp.AddConstraint(new MapConstraint("Western Australia", "South Australia"));
            csp.AddConstraint(new MapConstraint("South Australia", "Northern Territory"));
            csp.AddConstraint(new MapConstraint("Queensland", "Northern Territory"));
            csp.AddConstraint(new MapConstraint("Queensland", "South Australia"));
            csp.AddConstraint(new MapConstraint("Queensland", "New South Wales"));
            csp.AddConstraint(new MapConstraint("New South Wales", "South Australia"));
            csp.AddConstraint(new MapConstraint("Victoria", "South Australia"));
            csp.AddConstraint(new MapConstraint("Victoria", "New South Wales"));
            csp.AddConstraint(new MapConstraint("Victoria", "Tasmania"));

            var solution = csp.BackTrackingSearch(new Dictionary<string, string>());
            if (solution == null)
                Console.WriteLine("brak rozwi¹zania");

            foreach (var dict in solution)
            {
                Console.WriteLine($"{dict.Key}: {dict.Value}");
            }
        }*/
    }
}

