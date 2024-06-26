﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab01
{
    class Environment : ICloneable
    {
        private static int TOURNAMENT_SIZE = 10;

        public List<PCB> Population { get; set; }
        public List<PCB> Parents { get; set; }
        public int PopulationSize { get; set; }

        public Environment(int populationSize)
        {
            Population = new List<PCB>();
            Parents = new List<PCB>();
            PopulationSize = populationSize;
        }

        public void GetRandomPopulation()
        {
            var (x, y, pointList) = Data.ReadDataFromFile();
            PCB board;
            for (int i = 0; i < PopulationSize; i++)
            {
                Console.WriteLine("Sciezka " + i);
                board = new PCB(x, y, pointList);
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

            return bestIndividual;
        }

        public PCB Crossover(PCB pcb1, PCB pcb2)
        {
            Random rn = new Random();
            var crossoverPoint = rn.Next(pcb1.Paths.Count());
            PCB pcbChild = (PCB) pcb1.Clone();
            for (int i = crossoverPoint; i < pcb1.Paths.Count; i++)
            {
                pcbChild.Paths[i] = (Path) pcb2.Paths[i].Clone();
            }

            return pcbChild;
        }

        public PCB Mutation(PCB pcb, double pm)
        {
            Random rn = new Random();
            if (rn.NextDouble() < pm)
            {
                var pathIndex = rn.Next(0, pcb.Paths.Count);
                var segmentIndex = rn.Next(pcb.Paths[pathIndex].SegmentList.Count - 1);

                var path = pcb.Paths[pathIndex];
                var segment = path.SegmentList[segmentIndex];

                Direction randomDirection = segment.GetPerpendicularDirection();
                var startPoint = segment.StartPoint;

                Console.WriteLine($"Mutacja ścieżki {pathIndex + 1}, segmentu {segmentIndex + 1} w {randomDirection}.");
                //ustawianie nowego segmentu: czyli start pointu dla starego segmentu
                switch (randomDirection)
                {
                    case Direction.Down:
                        segment.StartPoint = new Point(startPoint.X, startPoint.Y - 1);
                        break;
                    case Direction.Up:
                        segment.StartPoint = new Point(startPoint.X, startPoint.Y + 1);
                        break;
                    case Direction.Right:
                        segment.StartPoint = new Point(startPoint.X + 1, startPoint.Y);
                        break;
                    case Direction.Left:
                        segment.StartPoint = new Point(startPoint.X - 1, startPoint.Y);
                        break;
                    //tylko jeden segment
                }

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
            }

            return pcb;
        }

        public PCB Roulette()
        {
            Random rn = new Random();
            var indicator = rn.NextDouble();
            var sum = 0.0;

            foreach (var individual in Population)
                sum += individual.CountQuality();

            var sum2 = 0.0;
            foreach (var pcb in Population)
            {
                sum2 += pcb.CountQuality();
                if (sum2/sum >= indicator)
                    return pcb;
            }

            return Population.Last();
        }

        public (PCB, PCB, double, double) GestStatistic()
        {
            var minValue = Population[0].CountPenaltyFunction();
            var maxValue = minValue;
            PCB bestIndividual = Population[0];
            var worstIndividual = bestIndividual;
            var penaultySum = 0;
            foreach (var individual in Population)
            {
                int penaltyFunction = individual.CountPenaltyFunction();
                penaultySum += penaltyFunction;
                if (penaltyFunction < minValue)
                {
                    minValue = penaltyFunction;
                    bestIndividual = individual;
                }

                if (penaltyFunction > maxValue)
                {
                    maxValue = penaltyFunction;
                    worstIndividual = individual;
                }
            }
            var average = penaultySum / PopulationSize;
            var sum = 0.0;
            foreach (var individual in Population)
            {
                int penaltyFunction = individual.CountPenaltyFunction();
                sum += (penaltyFunction-average)* (penaltyFunction - average);
            }
            var std = Math.Sqrt(sum / Population.Count);

            return (bestIndividual, worstIndividual, average, std);
        }

        public Object Clone()
        {
            var enviroment = new Environment(PopulationSize);
            foreach (var parent in Parents)
            {
                enviroment.Parents.Add((PCB) parent.Clone());
            }

            foreach (var population in Population)
            {
                enviroment.Population.Add((PCB)population.Clone());
            }

            return enviroment;
        }
    }
}