using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Lab01
{
    class Environment
    {
        private static int TOURNAMENT_SIZE = 3;

        public List<PCB> Population  { get; set; }
        public List<PCB> Parents { get; set; }

        public Environment()
        {
            Population = new List<PCB>();
            Parents = new List<PCB>();
        }

        public void GetRandomPopulation(int k)
        {
            for (int i = 0; i < k; i++)
            {
                var (x, y, pointList) = Data.ReadDataFromFile();
                PCB board = new PCB(x, y, pointList);
                board.BuildRandomPaths();
                Population.Add(board);
            }
        }

        public PCB TournamentSelection()
        {
            Random rm = new Random();
            List<PCB> selectedIndividuals = new List<PCB>();
            var populationSize = Population.Count;
            for (int i = 0; i < TOURNAMENT_SIZE; i++)
            {
                var index = rm.Next(populationSize);
                selectedIndividuals.Add(Population[index]);
               
                var temp = Population[index];
                Population[index] = Population[populationSize - 1];
                Population[populationSize - 1] = temp;
                populationSize--;
            }

            var minValue = selectedIndividuals[0].CountPenaltyFunction();
            PCB bestIndividual = selectedIndividuals[0];
            foreach (var individual in selectedIndividuals)
            {
                int penaltyFunction = individual.CountPenaltyFunction();
                if (penaltyFunction < minValue)
                {
                    minValue = penaltyFunction;
                    bestIndividual = individual;
                }
                              
            }
            Parents.Add(bestIndividual);
            return bestIndividual;
        }

        //do poprawy, 2 z jednego, 3 z drugiego 
        public PCB Crossover(PCB pcb1, PCB pcb2)
        {
            PCB pcbChild = (PCB) pcb1.Clone();
            for (int i = 0; i < pcb1.Paths.Count; i++)
            {
                var temp = pcbChild.Paths[i];
                pcbChild.Paths[i] = (Path) pcb2.Paths[i].Clone();
                if (pcbChild.CountIntersection() != 0)
                    pcbChild.Paths[i] = temp;
                else
                    return pcbChild;
            }

            return null;
        }

        public PCB Mutation(PCB pcb)
        {
            Random rn = new Random();
            var pathIndex = rn.Next(0, pcb.Paths.Count);
            var segmentIndex = rn.Next(pcb.Paths[pathIndex].SegmentList.Count-1);

            var path = pcb.Paths[pathIndex];
            var segment = path.SegmentList[segmentIndex];

            Direction randomDirection = segment.GetPerpendicularDirection();
            var startPoint = segment.StartPoint;

            Console.WriteLine($"Mutacja ścieżki {pathIndex+1}, segmentu {segmentIndex+1} w {randomDirection}.");
            //ustawianie nowego segmentu: czyli start pointu dla starego segmentu
            switch (randomDirection)
            {
                case Direction.Down:
                    segment.StartPoint = new Point(startPoint.X, startPoint.Y-1);
                    break;
                case Direction.Up:
                    segment.StartPoint = new Point(startPoint.X, startPoint.Y+1);
                    break;
                case Direction.Right:
                    segment.StartPoint = new Point(startPoint.X + 1, startPoint.Y);
                    break;
                case Direction.Left:
                    segment.StartPoint = new Point(startPoint.X - 1, startPoint.Y);
                    break;
            //tylko jeden segment
            }
            /*if (path.SegmentList.Count == 1)
            {
                var newStartSegment = new Segment((Point)path.GetStartPoint().Clone(), randomDirection);
                path.SegmentList.Insert(0, newStartSegment);
                path.SegmentList.Add(new Segment(segment.GetEndPoint(), newStartSegment.GetOpposedDirection()));

                return pcb;
            }*/
            Segment prevSegment;
            Segment nextSegment;


            //pierwszy segment
            if (segmentIndex == 0)
            {
                var newStartSegment = new Segment((Point) pcb.PointList[pathIndex].Item1.Clone(), randomDirection);
                path.SegmentList.Insert(0, newStartSegment);

                if (path.SegmentList.Count == 2)
                {
                    path.SegmentList.Add(new Segment(segment.GetEndPoint(), newStartSegment.GetOpposedDirection()));
                }
                else
                {
                    nextSegment = path.SegmentList[segmentIndex + 2];
                    path.ConnectSegmentEnd(segment, nextSegment);
                }

                return pcb;
            }


            prevSegment = path.SegmentList[segmentIndex - 1];
            nextSegment = path.SegmentList[segmentIndex + 1];

            //naprawiamy pierwszą część
            path.ConnectSegmentBegin(prevSegment, segment);

            //naprawiamy drugą część
            if (nextSegment != null)
                path.ConnectSegmentEnd(segment, nextSegment);

            return pcb;
        }

        public PCB Roulette()
        {
            Random rn = new Random();
            var indicator = rn.NextDouble();
            var sum = 0;

            foreach (var individual in Population)
                sum += individual.CountQuality();
            var selected = indicator * sum;

            double secondIndicator = 0;
            foreach (var pcb in Population)
            {
                secondIndicator += pcb.Quality * indicator;
                if (secondIndicator >= selected)
                    return pcb;
            }

            return Population.Last();
        }
    }
}
