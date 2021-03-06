﻿namespace Guestline.Battleships.ConsoleApp
{
    using System;
    using System.Collections.Generic;

    using Configuration;

    using Entities;
    using Interfaces;
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
            if (TryInitializeGame(out var game))
            {
                Play(game);
            }

            Console.WriteLine("Game over");
            Console.ReadKey();
        }

        private static bool TryInitializeGame(out Game game)
        {
            var shipFactory = new ShipFactory();
            var shipCoordinatesGenerator = new ShipCoordinatesGenerator();
            var boardFactory = new BoardFactory(shipFactory, shipCoordinatesGenerator, 1000);
            var attackingService = new AttackingService();
            var attackResultStorage = new AttackResultStorage();

            while (true)
            {
                var boardConfiguration = new BoardConfiguration(BoardWidth, BoardHeight, ShipsConfigurations);
                var result = Game.Initialize(boardFactory, attackingService, attackResultStorage, boardConfiguration);

                if (result.IsSuccess)
                {
                    game = result.Value;
                    return true;
                }

                Console.WriteLine("Failed to initialize the board. Press 'Y' for retry.");

                if (Console.ReadKey(true).Key != ConsoleKey.Y)
                {
                    game = null;
                    return false;
                }
            }
        }

        private static void Play(Game game)
        {
            var currentTurn = 0;
            var boardVisualizer = new BoardVisualizer();

            boardVisualizer.Display(Console.Out, BoardWidth, BoardHeight, game.GetAttackResults());

            do
            {
                TakeTurn(game, currentTurn);

                boardVisualizer.Display(Console.Out, BoardWidth, BoardHeight, game.GetAttackResults());

                currentTurn++;
            } while (!game.IsOver());
        }

        private static void TakeTurn(Game game, int currentTurn)
        {
            while (true)
            {
                Console.Write($"Turn {currentTurn}: ");

                var input = Console.ReadLine();
                if (!Coordinates.TryParse(input, out var coordinates))
                {
                    Console.WriteLine(
                        $"Invalid coordinates. Please try again. First symbol must be a letter followed be a number.{Environment.NewLine}");
                    continue;
                }

                var attackResult = game.Attack(coordinates);

                if (!attackResult.IsSuccess)
                {
                    Console.WriteLine($"Invalid coordinates. Please try again. Coordinates cannot exceed board size.{Environment.NewLine}");
                    continue;
                }

                Console.WriteLine($"{attackResult.Value.ToString()}{Environment.NewLine}");

                return;
            }
        }
    }
}
