using System.Collections.Generic;

namespace Lab02.EinsteinRiddle
{
    class EinsteinRiddle
    {
       /* public static void Main(string[] args)
        {
            var problemSize = 5;
            List<Variable> variables = new List<Variable>();

            for (int i = 0; i < problemSize; i++)
            {
                variables.Add(new Variable()
                {
                    HauseNumber = HauseNumber.Drugi //.(i + 1)
                });
            }

            Dictionary<Variable, List<List<int>>> domains = new Dictionary<Variable, List<List<int>>>();
            foreach (var value in variables)
            {
                List<List<int>> singleDomain = new List<List<int>>();
                for (int i=0; i<problemSize; i++)
                    singleDomain[i] = new List<int>();
                domains.Add(value, singleDomain);
            }

            //hint
            
            
            CSP<Variable, List<int>> csp = new CSP<Variable, List<int>>(variables, domains);

            Variable norvegian_firstHause = new Variable();
            norvegian_firstHause.Person = (Person) 4;
            norvegian_firstHause.HauseNumber = (HauseNumber)1;


           // csp.AddConstraint(new ER_Constraint(norvegian_firstHause), List<int>(){1});
            csp.BackTrackingSearch();
        }*/
    }
}
