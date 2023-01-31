using System;
using System.IO;

namespace PacMan
{
    class Program
    {
        static void Main()
        {
            const string ComandStartGame = "1";
            const string ComandExitGame = "2";

            bool isProgramOlpen = true;

            while (isProgramOlpen)
            {
                char[,] map = ReadMap("map.txt");

                Console.WriteLine($"{ComandStartGame}<--играть");
                Console.WriteLine($"{ComandExitGame}<--выход из игры ");

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
            char pacMan = '©';
            int pacManPositionX = 1;
            int pacManPositionY = 1;
            int score = 0;
            int coinScore = 25;
            int winScore = 1000;

            Console.Clear();

            while (score != winScore)
            {
                Console.CursorVisible = false;

                DrawMap(map);
                DrawScore(score, map.GetLength(0));

                DrawPacman(pacMan, pacManPositionX, pacManPositionY);

                ConsoleKeyInfo pressedKey = Console.ReadKey();
                HandelInput(pressedKey, ref pacManPositionX, ref pacManPositionY, map, ref score, coinScore);
                HandelInput(pressedKey, ref pacManPositionX, ref pacManPositionY, map, ref score, coinScore);
            }

            Console.Clear();
            Console.WriteLine("Вы победили");
            Console.ReadKey();
        }

        private static void DrawPacman(char pacMan, int pacManPositionX, int pacManPositionY)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(pacManPositionX, pacManPositionY);
            Console.Write(pacMan);
        }

        private static int PickupItem(int score, char[,] map, int pacManPositionX, int pacManPositionY, char emptySpaceCharacter, char coinSymbol, int coinScore)
        {

            if (map[pacManPositionY, pacManPositionX] == coinSymbol)
            {
                score += coinScore;
                map[pacManPositionY, pacManPositionX] = emptySpaceCharacter;
            }

            return score;
        }

        private static void DrawScore(int score, int lengthMap)
        {
            Console.SetCursorPosition(lengthMap, 0);
            Console.Write($"Рекорд:  {score}");
        }

        private static void DrawMap(char[,] map)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
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

        private static void HandelInput(ConsoleKeyInfo pressedKey, ref int pacManPositionX, ref int pacManPositionY, char[,] map, ref int score, int coinScore)
        {
            int positionX = 0;
            int positionY = 0;
            char emptySpaceCharacter = ' ';
            char coinSymbol = '·';
            GetDirection(pressedKey, ref positionX, ref positionY);
            int nextPacmanPositionX = pacManPositionX + positionX;
            int nextPacmanPositionY = pacManPositionY + positionY;

            if (map[nextPacmanPositionY, nextPacmanPositionX] == emptySpaceCharacter || map[nextPacmanPositionY, nextPacmanPositionX] == coinSymbol)
            {
                pacManPositionX = nextPacmanPositionX;
                pacManPositionY = nextPacmanPositionY;
                score = PickupItem(score, map, pacManPositionX, pacManPositionY, emptySpaceCharacter, coinSymbol, coinScore);
            }
        }

        private static void GetDirection(ConsoleKeyInfo pressedKey, ref int positionX, ref int positionY)
        {
            ConsoleKey moveUpCommand = ConsoleKey.UpArrow;
            ConsoleKey moveDownCommand = ConsoleKey.DownArrow;
            ConsoleKey moveLeftCommand = ConsoleKey.LeftArrow;
            ConsoleKey moveRightCommand = ConsoleKey.RightArrow;

            if (pressedKey.Key == moveUpCommand)
            {
                positionY--;
            }
            else if (pressedKey.Key == moveDownCommand)
            {
                positionY++;
            }
            else if (pressedKey.Key == moveLeftCommand)
            {
                positionX--;
            }
            else if (pressedKey.Key == moveRightCommand)
            {
                positionX++;
            }
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
