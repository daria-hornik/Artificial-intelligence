using Lab03.AlOperators;
using System;
using System.Collections.Generic;

namespace Lab03
{
    class Board : ICloneable
    {
        static int MINMAX_DEPTH = 5;
        static int OPPONENT_WELL = 13;
        static int YOUR_WELL = 6;

        public List<int> BoardCounter { get; set; }
        public Player You { get; set; }
        public Player Opponent { get; set; }

        public Board(bool youStart)
        {
            BoardCounter = new List<int>() { 4, 4, 4, 4, 4, 4, 0, 4, 4, 4, 4, 4, 4, 0 };
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

        public void Sow(int startIndex, bool isOpponent)
        {
            var seedNumber = BoardCounter[startIndex];
            int i;
            for (i = 0; i < seedNumber; i++)
            {
                BoardCounter[startIndex]--;

                if (!IsOpponentWell(isOpponent, (startIndex + i + 1) % BoardCounter.Count))
                    BoardCounter[(startIndex + 1 + i) % BoardCounter.Count]++;
                else
                {
                    i++;
                    BoardCounter[(startIndex + 1 + i) % BoardCounter.Count]++;
                    seedNumber++;
                }
            }
        }

        public int GetTheLastIndexForSowing(int startIndex)
        {
            var seedNumber = BoardCounter[startIndex];
            return (startIndex + seedNumber) % BoardCounter.Count;
        }

        public bool RepeatMove(int index, bool isOpponent)
        {
            int lastIndex = GetTheLastIndexForSowing(index);
            return isOpponent ? lastIndex == OPPONENT_WELL : lastIndex == YOUR_WELL;
        }

        public int MakeMove(bool isOpponent)
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

            Sow(index, isOpponent);

            return index;
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

            Console.Write("     ");
            for (int i = 1; i < 7; i++)
                Console.Write($"--------");

            Console.WriteLine(" ");

            Console.Write("\n     ");
            for (int i = 1; i < 7; i++)
                Console.Write($"   {i}    ");
            Console.WriteLine();
        }

        public int RateTheBoard(bool isOpponent)
        {
            var score = CountScores();
            return isOpponent ? score.Item2 - score.Item1 : score.Item1 - score.Item2;
        }

        public (int, int) CountScores()
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
            var score = CountScores();
            if (score.Item1 == score.Item2)
                Console.WriteLine("Remis!");
            else if (score.Item1 > score.Item2)
                Console.WriteLine("Zwyciężyłeś!");
            else
                Console.WriteLine("Przegrałeś!");
        }

        public List<int> GetAllPossibleMoves(bool isOpponent)
        {
            int startIndex, endIndex;
            startIndex = isOpponent ? YOUR_WELL + 1 : 0;
            endIndex = isOpponent ? OPPONENT_WELL : YOUR_WELL;
            List<int> indexList = new List<int>();

            for (int i = startIndex; i < endIndex; i++)
            {
                if (BoardCounter[i] != 0)
                    indexList.Add(i - startIndex);
            }
            return indexList;
        }


        /// <summary>
        /// metoda zwraca najlepszy ruch 
        /// </summary>
        /// <param name="deep"></param>
        /// <param name="isOpponent"></param>
        /// <returns></returns>
        public (int, int) FindBestMove(int deep, bool isOpponent, Node root = null)
        {
            if (deep == 0)
                return (root.Value.RateTheBoard(isOpponent), root.SelectedMoveIndex);

            if (root == null)
            {
                root = new Node((Board)this.Clone());
            }

            var allPossibleMoves = GetAllPossibleMoves(isOpponent);
            root.BuildNextLevel(allPossibleMoves, isOpponent);

            if (isOpponent)
            {
                int minValue = 1000;
                int minMove = 0;
                Node kid;
                for (int i = 0; i < root.Children.Count; i++)
                {
                    kid = root.Children[i];
                    var actualValue = kid.Value.FindBestMove(deep - 1, isOpponent, kid).Item1;
                    minValue = minValue < actualValue ? actualValue : minValue;
                    minMove = kid.SelectedMoveIndex;
                }
                return (minValue, minMove);
            }
            else
            {
                int maxValue = -1000;
                int maxMove = 0; ;
                Node kid;
                for (int i = 0; i < root.Children.Count; i++)
                {
                    kid = root.Children[i];
                    var actualValue = kid.Value.FindBestMove(deep - 1, isOpponent, kid).Item1;
                    maxValue = maxValue < actualValue ? actualValue : maxValue;
                    maxMove = kid.SelectedMoveIndex;
                }
                return (maxValue, maxMove);
            }
        }

        public void Play()
        {
            DrawBoard();
            (int, int) score;
            while (!IsFinished())
            {
                if (You.First)
                {
                    int index;
                    do
                    {
                        Console.WriteLine("Twoja kolej...");
                        Console.WriteLine($"Najlpszy ruch: {FindBestMove(MINMAX_DEPTH, false).Item2 + 1}");
                        index = MakeMove(false);
                        DrawBoard();
                    } while (RepeatMove(index, false));


                    Console.WriteLine("Kolej przeciwnika...");
                    Console.WriteLine($"Najlpszy ruch: {FindBestMove(MINMAX_DEPTH, true).Item2 + 1}");
                    MakeMove(true);
                    DrawBoard();
                }
                else
                {
                    Console.WriteLine("Kolej przeciwnika...");
                    Console.WriteLine("Najlpszy ruch: " + FindBestMove(MINMAX_DEPTH, true));
                    MakeMove(true);
                    DrawBoard();

                    Console.WriteLine("Twoja kolej...");
                    Console.WriteLine("Najlpszy ruch: " + FindBestMove(MINMAX_DEPTH, false));
                    MakeMove(false);
                    DrawBoard();
                }

                score = CountScores();
                Console.WriteLine($"Wyniki: {score.Item1}: {score.Item2}");
            }
            ShowResult();
        }

        public object Clone()
        {
            Board CopyBoard = You.First ? new Board(true) : new Board(false);
            CopyBoard.BoardCounter = new List<int>(BoardCounter);
            return CopyBoard;
        }
    }
}
