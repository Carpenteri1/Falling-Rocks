using System;
using System.Threading;

class Program
{
    static int shipPositionX = Console.WindowWidth / 2 - 3;
    static int shipPositionY = Console.WindowHeight - 1;
    static string playerLook = "(o)"; // this have to be 5 symbols
    static string[] rocks = new string[] { "^", "@", "*", "&", "+", "%", "$", "#", "!", ".", ";", "-" };
    // 3 waves of rocks with 8 rocks on each wave
    static int[,] x = new int[,] { { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };
    static int[,] y = new int[,] { { -1, -1, -1, -1, -1, -1, -1, -1 }, { -10, -10, -10, -10, -10, -10, -10, -10 }, { -20, -20, -20, -20, -20, -20, -20, -20 } };
    static int[,] model = new int[,] { { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };
    static int[,] color = new int[,] { { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };
    static int score = 0; // calculate score on the number of rocks that you have escaped
    static Random randomGenerator = new Random();
    static bool YouAreAlive = true;


    static void ConsoleSetSettings()
    {
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;
        Console.Title = "Falling Rocks";
    }

    static void DrowTheShip(int x, int y, string playerLook)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(x, y);
        Console.Write(playerLook);
    }
    static void MoveShipLeft()
    {
        if (shipPositionX != 0)
        {
            shipPositionX--;
        }

    }
    static void MoveShipRight()
    {
        if (shipPositionX + 6 != Console.WindowWidth)
        {
            shipPositionX++;
        }
    }
    static void SpownRock(int wave)
    {
        for (int i = 0; i < 8; i++)
        {
            y[wave, i]++;
            if (y[wave, i] >= 0)
            {
                // if the rock gets aut of the screen generete new x, new model and new colour
                if (y[wave, i] == Console.WindowHeight) { y[wave, i] = 0; score++; }
                if (y[wave, i] == 0) { x[wave, i] = randomGenerator.Next(1, Console.WindowWidth); model[wave, i] = randomGenerator.Next(0, 11); color[wave, i] = randomGenerator.Next(1, 7); }

                Console.SetCursorPosition(x[wave, i], y[wave, i]);
                switch (color[wave, i])
                {
                    case 1: Console.ForegroundColor = ConsoleColor.Green; break;
                    case 2: Console.ForegroundColor = ConsoleColor.White; break;
                    case 3: Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case 4: Console.ForegroundColor = ConsoleColor.Cyan; break;
                    case 5: Console.ForegroundColor = ConsoleColor.Red; break;
                    case 6: Console.ForegroundColor = ConsoleColor.Magenta; break;
                    case 7: Console.ForegroundColor = ConsoleColor.Gray; break;
                }
                Console.Write(rocks[model[wave, i]]);
                Console.ForegroundColor = ConsoleColor.White;
                if (x[wave, i] >= shipPositionX && x[wave, i] <= shipPositionX + 5 && y[wave, i] == shipPositionY) { YouAreAlive = false; };
            }
        }

    }

    static void Main()
    {
        ConsoleSetSettings();

        while (YouAreAlive == true)
        {

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.LeftArrow) { MoveShipLeft(); }
                if (keyInfo.Key == ConsoleKey.RightArrow) { MoveShipRight(); }
            }

            Console.Clear();
            DrowTheShip(shipPositionX, shipPositionY, playerLook);
            SpownRock(0);
            SpownRock(1);
            SpownRock(2);
            Thread.Sleep(120);

        }

        Console.Clear();
        Console.SetCursorPosition(Console.WindowWidth / 2 - 6, Console.WindowHeight / 2);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You Died!");
        Console.SetCursorPosition(Console.WindowWidth / 2 - 6, Console.WindowHeight / 2 - 1);
        Console.WriteLine("Score: {0}", score);
        Console.SetCursorPosition(Console.WindowWidth / 2 - 15, Console.WindowHeight / 2 + 1);
        Thread.Sleep(60000);

    }
}