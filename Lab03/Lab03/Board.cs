using System;
using System.Collections.Generic;

namespace Lab03
{
    class Board
    {
        static int MINMAX_DEPTH = 3;
        public List<int> BoardCounter { get; set; }
        public Player You { get; set; }
        public Player Opponent { get; set; }

        public Board(bool youStart)
        {
            BoardCounter = new List<int>() { 6, 4, 4, 4, 4, 4, 0, 4, 4, 4, 4, 4, 4, 0 };
            You = new Player();
            Opponent = new Player();
            if (youStart)
                You.First = true;
            else
                Opponent.First = true;
        }

        private int GetIndexForOpponent(int m)
        {
            return BoardCounter.Count - (8 - m);
        }

        private int GetIndexForYou(int m)
        {
            return m - 1;
        }

        private bool IsCorrectMove(int m, bool isOpponent)
        {
            if (m < 0 || m > 7)
                return false;

            if (isOpponent)
            {
                if (BoardCounter[GetIndexForOpponent(m)] == 0)
                    return false;
            }
            else
            {
                if (BoardCounter[GetIndexForYou(m)] == 0)
                    return false;
            }

            return true;
        }

        public bool IsOpponentWell(bool isOpponent, int index)
        {
            if (isOpponent)
            {
                return index == BoardCounter.Count / 2 - 1;
            }
            else
                return index == BoardCounter.Count - 1;
        }

        public bool IsYourWell(bool isOpponent, int index)
        {
            if (isOpponent)
            {
                return index == BoardCounter.Count - 1;
            }
            else
                return index == BoardCounter.Count / 2 - 1;
        }

        public int Sow(int startIndex, bool isOpponent)
        {
            var seedNumber = BoardCounter[startIndex];
            int i;
            for (i = 0; i < seedNumber; i++)
            {
                BoardCounter[startIndex]--;

                if (!IsOpponentWell(isOpponent, (startIndex + i + 1)%BoardCounter.Count))
                    BoardCounter[(startIndex + 1 + i) % BoardCounter.Count]++;
                else
                {
                    i++;
                    BoardCounter[(startIndex + 1 + i) % BoardCounter.Count]++;
                    seedNumber++;
                }
            }
            return (startIndex + i) % BoardCounter.Count;
        }

        private bool MakeMoveAndRepeat(bool isOpponent)
        {
            int pit;
            do
            {
                Console.Write("Podaj numer dołka: ");
                pit = int.Parse(Console.ReadLine());
            } while (!IsCorrectMove(pit, isOpponent));

            int index;
            if (isOpponent)
                index = GetIndexForOpponent(pit);
            else
                index = GetIndexForYou(pit);

            var lastIndex = Sow(index, isOpponent);

            return IsYourWell(isOpponent, lastIndex);
        }

        public void DrawBoard()
        {
            Console.Write("     ");
            for (int i = 6; i > 0; i--)
                Console.Write($"   {i}    ");

            Console.Write("\n\n     ");
            for (int i = 1; i < 7; i++)
                Console.Write($"--------");

            Console.Write("\n    |");
            for (int i = BoardCounter.Count / 2 - 1; i > 0; i--)
            {
                Console.Write($"   {BoardCounter[GetIndexForOpponent(i)]}    ");
            }
            Console.WriteLine("|");

            Console.Write($"{BoardCounter[BoardCounter.Count - 1]}   |");
            for (int i = BoardCounter.Count / 2 - 1; i > 0; i--)
            {
                Console.Write("        ");
            }
            Console.WriteLine($"| {BoardCounter[BoardCounter.Count / 2 - 1]}");

            Console.Write("    |");
            for (int i = 0; i < BoardCounter.Count / 2 - 1; i++)
            {
                Console.Write($"   {BoardCounter[i]}    ");
            }
            Console.WriteLine("|");

            Console.Write("    |");
            for (int i = 1; i < 7; i++)
                Console.Write($"--------");

            Console.WriteLine("|");

            Console.Write("\n     ");
            for (int i = 1; i < 7; i++)
                Console.Write($"   {i}    ");
            Console.WriteLine();
        }

        public int RateTheBoard(bool isOpponent)
        {
            var score = CountScore();
            return isOpponent ? score.Item2 - score.Item1: score.Item1 - score.Item2; 
        }

        public (int, int) CountScore()
        {
            int yourScore = 0;
            int opponentScore = 0;
            for (int i = 0; i < BoardCounter.Count / 2; i++)
            {
                yourScore += BoardCounter[i];
            }

            for (int i = BoardCounter.Count / 2; i < BoardCounter.Count; i++)
            {
                opponentScore += BoardCounter[i];
            }

            return (yourScore, opponentScore);
        }

        public bool IsFinished()
        {
            int emptyPitsCounter = 0;

            for (int i = 1; i < BoardCounter.Count / 2 - 1; i++)
            {
                if (BoardCounter[i] == 0)
                    emptyPitsCounter++;
            }
            if (emptyPitsCounter == BoardCounter.Count / 2 - 1)
                return true;

            emptyPitsCounter = 0;

            for (int i = BoardCounter.Count / 2; i < BoardCounter.Count - 1; i++)
            {
                if (BoardCounter[i] == 0)
                    emptyPitsCounter++;
            }
            if (emptyPitsCounter == BoardCounter.Count / 2 - 1)
                return true;
            return false;
        }

        public void ShowResult()
        {
            var score = CountScore();
            if (score.Item1 == score.Item2)
                Console.WriteLine("Remis");
            else if (score.Item1 > score.Item2)
                Console.WriteLine("Zwyciężyłeś!");
            else
                Console.WriteLine("Przegrałeś!");
        }

        /// <summary>
        /// Metoda definiująca drzewo do podejmowania decyzji, minimalizująca zyski dla oponenta, maksymalizująca dla ciebie 
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="isOpponent"></param>
        /// <returns>Zwraca najlepszy ruch</returns>
       /* public (int, int) MinMax(int depth, bool isOpponent)
        {
            if (depth == 0 || IsFinished())
            {
                return;
            }

            if (isOpponent)
            {

            }
            else
            {

            }
        }*/

        public void Play()
        {
            DrawBoard();
            (int, int) score;
            while (!IsFinished())
            {
                if (You.First)
                {
                    bool repeat;
                    do
                    {
                        Console.WriteLine("Twoja kolej...");
                        repeat = MakeMoveAndRepeat(false);
                        DrawBoard();
                    } while (repeat);
                   

                    Console.WriteLine("Kolej przeciwnika...");
                    MakeMoveAndRepeat(true);
                    DrawBoard();
                }
                else
                {
                    Console.WriteLine("Kolej przeciwnika...");
                    MakeMoveAndRepeat(true);
                    DrawBoard();

                    Console.WriteLine("Twoja kolej...");
                    MakeMoveAndRepeat(false);
                    DrawBoard();
                }

                score = CountScore();
                Console.WriteLine($"Wyniki: {score.Item1}: {score.Item2}");
            }
            ShowResult();
        }
    }
}
