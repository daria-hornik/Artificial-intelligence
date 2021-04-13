using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab02
{
    public class CSP<V, D>
    {
        public List<V> Variables { get; set; }
        public Dictionary<V, List<D>> Domains { get; set; }
        public Dictionary<V, List<Constraint<V, D>>> Constraints { get; set; }
        public Dictionary<V, List<V>> Neighbours { get; set; }

        public CSP(List<V> variables, Dictionary<V, List<D>> domains, Dictionary<V, List<V>> neighbours)
        {
            Variables = variables;
            Domains = domains;
            Neighbours = neighbours;
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

            if(!AC3())
                return null;

            var first = MRV(set);
            foreach (var value in LCV(first, set))
            {
                var localSet = new Dictionary<V, D>(set);
                localSet[first] = value;
                if (IsCorrect(first, localSet))
                {
                    ForwardChecking(first, value, localSet);
                    var result = BackTrackingSearch(localSet);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        public Dictionary<V, D> BackTrackingSearch()
        {
            return BackTrackingSearch(new Dictionary<V, D>());
        }

        public bool ForwardChecking(V variable, D value, Dictionary<V, D> set)
        {
            foreach (V neighbour in Neighbours[variable])
            {
                if (!set.ContainsKey(neighbour))
                {
                    if (Domains[neighbour].Contains(value))
                    {
                        var localSet = new Dictionary<V, D>(set);
                        localSet.Add(neighbour, value);
                        if (!IsCorrect(neighbour, localSet))
                        {
                            Domains[neighbour].Remove(value);
                        }
                    }

                    if (Domains[neighbour].Count == 0)
                        return false;
                }
            }
            return true;
        }

        //Heurystyki wyboru zmiennej 

        /// <summary>
        /// Minimum remaining values heuristic.
        /// Wybierz zmienną z możliwie najmniejszą liczbą wartości.
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public V MRV(Dictionary<V, D> set)
        {
            V choosenVariable = Variables[0];
            int minCounter = Domains[choosenVariable].Count;

            foreach (V variable in Variables)
            {
                if (!set.ContainsKey(variable))
                {
                    if (Domains[variable].Count < minCounter)
                    {
                        minCounter = Domains[variable].Count;
                        choosenVariable = variable;
                    }
                }
            }
            return choosenVariable;
        }

        public V RandomVariable(Dictionary<V, D> set)
        {
            Random rn = new Random();
            var variable = Variables[rn.Next(Variables.Count)];
            while (set.ContainsKey(variable))
            {
                variable = Variables[rn.Next(Variables.Count)];
            }
            return variable;
        }

        //Heurystyki wyboru wartości 
        private int CountConflict(V variable, D value, Dictionary<V, D> set)
        {
            int conflictNumber = 0;
            foreach (V neighbour in Neighbours[variable])
            {
                var localSet = new Dictionary<V, D>(set);
                if (!set.ContainsKey(neighbour)) { 
                localSet.Add(neighbour, value);
                if (IsCorrect(neighbour, localSet))
                    conflictNumber++;
                }
            }
            return conflictNumber;
        }

        /// <summary>
        /// Least constraining values heuristic.
        /// Wybierz wartość, która wyklucza najmniejszą liczbę wartości w zmiennych.
        /// </summary>
        /// <returns></returns>
        public List<D> LCV(V variable, Dictionary<V, D> set)
        {
            Dictionary<D, int> conflictsCounter = new Dictionary<D, int>();

            List<D> domains = new List<D>();
            foreach (D value in Domains[variable])
            {
                conflictsCounter.Add(value, CountConflict(variable, value, set));
            }
            return conflictsCounter.OrderByDescending(x => x.Value).Select(i => i.Key).ToList();
        }

        public List<D> RandomValue(V variable)
        {
            return Domains[variable];
        }


        /// <summary>
        /// jeśli x kłóci się z każdą wartością y, usunąć x 
        /// </summary>
        /// <param name="xi"></param>
        /// <param name="xj"></param>
        /// <returns></returns>
        public bool RemoveInconsistentValue(V xi, V xj)
        {
            for (int i = 0; i < Domains[xi].Count; i++)
            {
                int conflictCounter = 0;
                var valueI = Domains[xi][i];
                var set = new Dictionary<V, D>();
                set.Add(xi, valueI);
                foreach (var valueJ in Domains[xj])
                {
                    var localSet = new Dictionary<V, D>(set);
                    localSet.Add(xj, valueJ);
                    foreach (var constraint in Constraints[xi])
                    {
                        if (!constraint.IsSatisfied(localSet))
                        {
                            conflictCounter++;
                            break;
                        }
                    }                    
                }

                if (conflictCounter == Domains[xj].Count)
                {
                    Domains[xi].Remove(valueI);
                    return true;
                }
            }
            return false;
        }

        public bool AC3()
        {
            Queue<(V, V)> queue = new Queue<(V, V)>();
            if (queue.Count == 0)
            {
                foreach (var variable in Variables)
                {
                    foreach (var neighbour in Neighbours[variable])
                    {
                        queue.Enqueue((variable, neighbour));
                    }
                }
            }

            while (queue.Count > 0)
            {
                (V xi, V xj) = queue.Dequeue();
                if (RemoveInconsistentValue(xi, xj))
                {
                    foreach (var xk in Neighbours[xi])
                        queue.Enqueue((xi, xk));
                }

                if ((Domains[xi]).Count == 0)
                    return false;
            }
            return true;
        }
    }
}
