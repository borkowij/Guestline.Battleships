namespace Guestline.Battleships.ConsoleApp
{
    using System;
    using System.Collections.Generic;

    using Models;

    using Services;

    class Program
    {
        private const int BoardWidth = 10;
        private const int BoardHeight = 10;
        private static readonly List<ShipConfiguration> ShipsConfigurations = new List<ShipConfiguration>
        {
            new ShipConfiguration(4),
            new ShipConfiguration(4),
            new ShipConfiguration(5)
        };

        static void Main()
        {
            if (!TryInitializeGame(out var game))
            {
                return;
            }

            Play(game);
        }

        private static bool TryInitializeGame(out Game game)
        {
            var boardStorage = new BoardStorage();
            var boardService = new BoardService(boardStorage);
            var shipCoordinatesGenerator = new RandomShipsCoordinatesGenerator(1000);

            var newGame = new Game(boardService, shipCoordinatesGenerator);

            while (true)
            {
                var gameConfiguration = new GameConfiguration(BoardWidth, BoardHeight, ShipsConfigurations);
                var initializationSucceeded = newGame.Initialize(gameConfiguration);

                if (initializationSucceeded)
                {
                    game = newGame;
                    return true;
                }

                Console.WriteLine("Failed to initialize game. Press 'Y' for retry.");

                if (Console.ReadKey(true).Key != ConsoleKey.Y)
                {
                    Console.WriteLine("Game over");
                    Console.ReadKey();

                    game = null;
                    return false;
                }
            }
        }

        private static void Play(Game game)
        {
            var currentTurn = 0;
            var boardVisualizer = new BoardVisualizer(BoardWidth, BoardHeight);
            boardVisualizer.Display(Console.Out);

            do
            {
                Console.Write($"Turn {currentTurn}: ");

                var input = Console.ReadLine();

                if (!Coordinates.TryParse(input, BoardWidth, BoardHeight, out var coordinates))
                {
                    Console.WriteLine("Invalid coordinates. Please try again. First symbol must be a letter followed be a number.");
                    continue;
                }

                var attackResult = game.Attack(coordinates);
                boardVisualizer.SaveAttackResult(coordinates, attackResult);

                Console.Clear();
                boardVisualizer.Display(Console.Out);
                Console.WriteLine($"Turn {currentTurn}: {input} - {attackResult.ToString()}");
                currentTurn++;
            } while (!game.IsOver());

            Console.WriteLine("Game over");
            Console.ReadKey();
        }
    }
}
