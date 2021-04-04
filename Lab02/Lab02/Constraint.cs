using System;
using System.Collections.Generic;

namespace Lab02
{
    public abstract class Constraint<V, D> 
    {
        public List<V> Variables { get; set; }

        public Constraint(List<V> variables)
        {
            Variables = variables;
        }

        public abstract bool IsSatisfied(Dictionary<V, D> set);
    }
}
