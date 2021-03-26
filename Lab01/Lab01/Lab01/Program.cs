using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab01
{
    class Program
    {
        public static void Main(string[] args)
        {
            var (x, y, pointList) = Data.ReadDataFromFile();
            int generation = 20;
            int populationSize = 30;
            double pm = 0.5;
            Console.WriteLine("Losowe rozwiazanie: ");
            Environment randomEnv = new Environment(populationSize);
            for (int i = 0; i < 10; i++)
            {
                randomEnv.GetRandomPopulation();
                var (best, worst, avg, std) = randomEnv.GestStatistic();
                Console.WriteLine("Najlepsze: "); best.PathsInfo();
                Console.WriteLine("Najgorsze: ");
                worst.PathsInfo();
                Console.WriteLine("Średnia : " + avg.ToString());
                Console.WriteLine("Std: " + std.ToString());

            }


             Environment envTournament = new Environment(populationSize);
             envTournament.GetRandomPopulation();
             Environment envRoulette = (Environment)envTournament.Clone();
             PCB parent1;
             PCB parent2;
             PCB bestPCD, worstPCB, avgPCB;
             PCB solutionRoulette = new PCB();
             PCB solutionTournament = new PCB();

             for (int i = 0; i < generation; i++)
             {
                 parent1 = envRoulette.Roulette();
                 parent2 = envRoulette.Roulette();
                 while (parent2==parent1)
                 {
                     parent2 = envRoulette.Roulette();
                 }

                 while (envRoulette.Parents.Count < populationSize)
                 {
                     var child = envRoulette.Crossover(parent1, parent2);
                     var mutatedChild = envRoulette.Mutation(child, pm);
                     envRoulette.Parents.Add(mutatedChild);
                 }
                 //statystyki
                 var (best, worst, avg, std) = envRoulette.GestStatistic();
                 Console.WriteLine("Najlepszy:");
                 best.PathsInfo();
                 Console.WriteLine("Najgorszy:");
                 worst.PathsInfo();

                 Console.WriteLine($"Srednia: {avg}");
                 Console.WriteLine($"Std: {std}");

                 envRoulette.Population = new List<PCB>(envRoulette.Parents);
                 envRoulette.Parents.Clear();
                 if (i == 0 || solutionRoulette.CountQuality() < best.CountQuality())
                 {
                     solutionRoulette = best;
                 }
             }

             //tournament
             for (int i = 0; i < generation; i++)
             {
                 parent1 = envTournament.TournamentSelection();
                 parent2 = envTournament.TournamentSelection();
                 while (parent2 == parent1)
                 {
                     parent2 = envTournament.TournamentSelection();
                 }
                 envTournament.Parents.Add(parent1);
                 envTournament.Parents.Add(parent2);

                 while (envTournament.Parents.Count < populationSize)
                 {
                     var child = envTournament.Crossover(parent1, parent2);
                     var mutatedChild = envTournament.Mutation(child, pm);
                     envTournament.Parents.Add(mutatedChild);
                 }
                 //statystyki
                 var (best, worst, avg, std) = envTournament.GestStatistic();
                 Console.WriteLine("Najlepszy:");
                 best.PathsInfo();
                 Console.WriteLine("Najgorszy:");
                 worst.PathsInfo();

                 Console.WriteLine($"Srednia: {avg}");
                 Console.WriteLine($"Std: {std}");
                 envTournament.Population = new List<PCB>(envTournament.Parents);
                 envTournament.Parents.Clear();
                 if (i == 0 || solutionTournament.CountQuality() < best.CountQuality())
                 {
                     solutionTournament = best;
                 }
             }
             Console.WriteLine("================");
             Console.WriteLine("Podsumowanie");
             Console.WriteLine("Ruletka:");
             solutionRoulette.PathsInfo();
             Console.WriteLine("\nTurniej:");
             solutionTournament.PathsInfo();

        }
    }
}
