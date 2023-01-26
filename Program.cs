using System;
using System.IO;

namespace C_Light
{
    class Program
    {

        static void Main(string[] args)
        {

            const string ComandStartGame = "1";
            const string ComandExitGame = "2";

            bool isProgramOlpen = true;
            char[,] map = ReadMap("map.txt");

            Console.WriteLine($"{ComandStartGame}<--играть");
            Console.WriteLine($"{ComandExitGame}<--выход из игры ");

            while (isProgramOlpen)
            {
                switch (Console.ReadLine())
                {
                    case ComandStartGame:
                        StartGame(map);
                        break;

                    case ComandExitGame:
                        isProgramOlpen = false;
                        break;
                }
            }
        }

        private static void StartGame(char[,] map)
        {
            Console.CursorVisible = false;
            char pacMan = '©';
            int pacManPositionX = 1;
            int pacManPositionY = 1;
            int score = 0;
            int winScore = 11225;
            Console.Clear();

            while (score!=winScore)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);
                DrowScore(score, map.GetLength(0));

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacManPositionX, pacManPositionY);
                Console.Write(pacMan);

                ConsoleKeyInfo pressedKey = Console.ReadKey();
                HandeleInput(pressedKey, ref pacManPositionX, ref pacManPositionY, map, ref score);

            }
        }

        private static void PickupItem(ref int score, char[,] map, int pacManPositionX, int pacManPositionY)
        {
            if (map[pacManPositionY, pacManPositionX] == '·')
            {
                score += 25;
                map[pacManPositionY, pacManPositionX] = ' ';

            }
        }

        private static void DrowScore(int score, int lengthMap)
        {
            Console.SetCursorPosition(lengthMap, 0);
            Console.Write($"Рекорд:  {score}");
        }

        private static void DrawMap(char[,] map)
        {
            Console.SetCursorPosition(0, 0);
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Console.Write(map[x, y]);
                }

                Console.WriteLine();
            }

        }

        private static void HandeleInput(ConsoleKeyInfo pressedKey, ref int pacManPositionX, ref int pacManPositionY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(pressedKey);
            int nextPacmanPositionX = pacManPositionX + direction[1];
            int nextPacmanPositionY = pacManPositionY + direction[0];

            if (map[nextPacmanPositionY, nextPacmanPositionX] == ' ' || map[nextPacmanPositionY, nextPacmanPositionX] == '·')
            {
                pacManPositionX = nextPacmanPositionX;
                pacManPositionY = nextPacmanPositionY;
                PickupItem(ref score, map, pacManPositionX, pacManPositionY);

            }
        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                direction[0]--;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                direction[0]++;
            }
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                direction[1]--;
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                direction[1]++;
            }

            return direction;
        }

        private static char[,] ReadMap(string path)
        {
            string[] fileMap = File.ReadAllLines(path);
            char[,] map = new char[fileMap.Length, GetMaxLengthOfLines(fileMap)];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = fileMap[x][y];
                }

            }
            return map;

        }

        private static int GetMaxLengthOfLines(string[] lines)
        {
            int maxLines = lines[0].Length;

            foreach (var line in lines)
            {
                if (line.Length > maxLines)
                {
                    maxLines = line.Length;

                }
            }

            return maxLines;
        }

    }
}