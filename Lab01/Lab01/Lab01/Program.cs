using System;
j
namespace Lab01
{
    class Program
    {
        public static void Main(string[] args)
        {
            var (x, y, pointList) = Data.ReadDataFromFile();

            Console.WriteLine("Losowe rozwiązanie: ");
            PCB board = new PCB(x, y, pointList);
            board.BuildRandomPaths();
            board.PathsInfo();

            Environment env = new Environment();
            env.GetRandomPopulation(6);

            Console.WriteLine("\n\nTurniej:");
            var pcb1 = env.TournamentSelection();
            pcb1.PathsInfo();

            Console.WriteLine("\n\nRuletka:");
            var pcb2 = env.Roulette();
            pcb2.PathsInfo();

            Console.WriteLine("\n\nOperator krzyżowania");
            var pcb3 = env.Crossover(pcb1, pcb2);
            pcb3.PathsInfo();

            Console.WriteLine("\n\nMutacja: ");
            env.Mutation(pcb1).PathsInfo();
        }
    }
}
