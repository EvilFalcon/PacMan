using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace C_Light
{
    class Program
    {
        private static string MapPath = "map.txt";

        static void Main(string[] args)
        {

            const string ComandStartGame = "1";
            const string ComandExitGame = "3";

            bool isProgramOlpen = true;
            char[,] map = null;
            map = ReadMap("map.txt");

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
            char pacMan = 'ᗧ';
            int pacManPositionX = 1;
            int pacManPositionY =32;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacManPositionX, pacManPositionY);
                Console.Write(pacMan);

                Console.ForegroundColor= ConsoleColor.Red;
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                HandeleInput(pressedKey, ref pacManPositionX, ref pacManPositionY);

            }
        }

        private static void DrawMap(char[,] map)
        {

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Console.Write(map[x, y]);
                }

                Console.WriteLine();
            }

        }

        private static void HandeleInput(ConsoleKeyInfo pressedKey, ref int pacManPositionX,ref int pacManPositionY)
        {
            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                pacManPositionY--;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                pacManPositionY++;
            }
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                pacManPositionX--;
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                pacManPositionX++;
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
