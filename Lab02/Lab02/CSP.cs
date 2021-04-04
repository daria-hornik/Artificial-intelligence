using System;
using System.Collections.Generic;

namespace Lab02
{
    public class CSP<V, D>
    {
        public List<V> Variables { get; set; }
        public Dictionary<V, List<D>> Domains { get; set; }
        public Dictionary<V, List<Constraint<V, D>>> Constraints { get; set; }

        public CSP(List<V> variables, Dictionary<V, List<D>> domains)
        {
            Variables = variables;
            Domains = domains;
            Constraints = new Dictionary<V, List<Constraint<V, D>>>();

            foreach (var variable in Variables)
                Constraints[variable] = new List<Constraint<V, D>>();
        }

        public bool AddConstraint(Constraint<V, D> constraint)
        {
            foreach (var variable in constraint.Variables)
            {
                if (!Variables.Contains(variable))
                {
                    Console.WriteLine("Podana zmienna nie została zdefiniowana w problemie.");
                }
                else
                    Constraints[variable].Add(constraint);
            }
            return true;
        }

        public bool IsCorrect(V variable, Dictionary<V, D> set)
        {
            foreach (var constraint in Constraints[variable])
            {
                if (!constraint.IsSatisfied(set)) 
                    return false;
            }
            return true;
        }


        public Dictionary<V, D> BackTrackingSearch(Dictionary<V, D> set)
        {
            if (set.Count == Variables.Count)
                return set;

            List<V> unassignedVariables = new List<V>();
            foreach (var variable in Variables)
            {
                if (!set.ContainsKey(variable))
                    unassignedVariables.Add(variable);
            }

            var first = unassignedVariables[0];
            foreach (var value in Domains[first])
            {
                set[first] = value;
                if (IsCorrect(first, set))
                {
                    var result = BackTrackingSearch(set);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}
