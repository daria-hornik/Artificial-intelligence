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
            int repetitions = 6;
            int populationSize = 20;
            double pm = 0.5;

            // Console.WriteLine("Losowe rozwiązanie: ");
            PCB board = new PCB(x, y, pointList);
            board.BuildRandomPaths();
            // board.PathsInfo();

            Environment envTournament = new Environment(populationSize);
            envTournament.GetRandomPopulation();
            Environment envRoulette = (Environment)envTournament.Clone();
            PCB parent1;
            PCB parent2;
            PCB bestPCD, worstPCB, avgPCB;
            double std;

            for (int i = 0; i < repetitions; i++)
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
                var (best, worst, avg) = envRoulette.GestStatistic();
                Console.WriteLine("Najlepszy:");
                best.PathsInfo();
                Console.WriteLine("Najgorszy:");
                worst.PathsInfo();

                Console.WriteLine($"Srednia: {avg}");
                
                envRoulette.Population = new List<PCB>(envRoulette.Parents);
                envRoulette.Parents.Clear();
            }

            //tournament
            for (int i = 0; i < repetitions; i++)
            {
                parent1 = envRoulette.TournamentSelection();
                parent2 = envRoulette.TournamentSelection();
                while (parent2==parent1)
                {
                    parent2 = envRoulette.TournamentSelection();
                }

                while (envRoulette.Parents.Count < populationSize)
                {
                    var child = envTournament.Crossover(parent1, parent2);
                    var mutatedChild = envTournament.Mutation(child, pm);
                    envTournament.Parents.Add(mutatedChild);
                }
                //statystyki
                var (best, worst, avg) = envRoulette.GestStatistic();
                Console.WriteLine("Najlepszy:");
                best.PathsInfo();
                Console.WriteLine("Najgorszy:");
                worst.PathsInfo();

                Console.WriteLine($"Srednia: {avg}");
                envTournament.Population = new List<PCB>(envTournament.Parents);
                envTournament.Parents.Clear();
            }


            Console.WriteLine("\n\nTurniej:");
            var pcb1 = envTournament.TournamentSelection();
            pcb1.PathsInfo();

            Console.WriteLine("\n\nRuletka:");
            var pcb2 = envTournament.Roulette();
            pcb2.PathsInfo();

            Console.WriteLine("\n\nOperator krzyżowania");
            var pcb3 = envTournament.Crossover(pcb1, pcb2);
            pcb3.PathsInfo();

            Console.WriteLine("\n\nMutacja: ");
            envTournament.Mutation(pcb1, pm).PathsInfo();
        }
    }
}
