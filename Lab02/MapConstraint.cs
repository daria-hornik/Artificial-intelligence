using Lab02;
using System;
using System.Collections.Generic;

namespace Lab02.MapColoring

{

    class MapConstraint : Constraint<string, string>
    {
        public string Place1 { get; set; }
        public string Place2 { get; set; }

        public MapConstraint(string place1, string place2) : base(new List<string> { place1, place2 })
        {

            Place1 = place1;
            Place2 = place2;
        }

        public bool IsSatisfied(Dictionary<V, D> assigment)
        {
            if (!assigment.ContainsKey(Place1) || !assigment.ContainsKey(Place2))
            {
                return true;
            }
            return assigment[Place1] != assigment[Place2]
        }
    }
}