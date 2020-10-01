namespace Guestline.Battleships.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Models;

    public class RandomShipsCoordinatesGenerator : IShipsCoordinatesGenerator
    {
        private readonly Random _randomGenerator = new Random();
        private readonly int _maxRetries;

        public RandomShipsCoordinatesGenerator(int maxRetries)
        {
            _maxRetries = maxRetries;
        }

        public Result<List<List<Coordinates>>> Generate(IEnumerable<ShipConfiguration> shipsConfigurations, int boardWidth, int boardHeight)
        {
            var result = new List<List<Coordinates>>();

            foreach (var shipConfiguration in shipsConfigurations)
            {
                if (TryGenerateCoordinates(shipConfiguration.NumberOfParts, boardWidth, boardHeight, result, out var coordinates))
                {
                    result.Add(coordinates);
                }
                else
                {
                    return Result<List<List<Coordinates>>>.Error();
                }
            }

            return Result<List<List<Coordinates>>>.Success(result);
        }

        private bool TryGenerateCoordinates(int numberOfParts, int boardWidth, int boardHeight, List<List<Coordinates>> occupiedCoordinates, out List<Coordinates> coordinates)
        {
            var iterations = 0;
            while (iterations++ < _maxRetries)
            {
                var isHorizontal = _randomGenerator.Next(2) == 0;

                var startingPoint = GenerateStartingPoint(numberOfParts, boardWidth, boardHeight, isHorizontal);
                var newCoordinates = FillCoordinates(startingPoint, numberOfParts, isHorizontal);

                if (!occupiedCoordinates.Any(x => x.Intersect(newCoordinates).Any()))
                {
                    coordinates = newCoordinates;
                    return true;
                }
            }

            coordinates = null;
            return false;
        }

        private Coordinates GenerateStartingPoint(int numberOfParts, int boardWidth, int boardHeight, bool isHorizontal)
        {
            var maxStartingX = boardWidth - (isHorizontal ? numberOfParts : 0);
            var maxStartingY = boardHeight - (isHorizontal ? 0 : numberOfParts);

            var x = _randomGenerator.Next(0, maxStartingX);
            var y = _randomGenerator.Next(0, maxStartingY);

            return new Coordinates(x, y);
        }

        private List<Coordinates> FillCoordinates(Coordinates startingPoint, int numberOfParts, bool isHorizontal)
        {
            var coordinates = new List<Coordinates>()
            {
                startingPoint
            };

            for (var i = 1; i < numberOfParts; i++)
            {
                var x = startingPoint.X + (isHorizontal ? i : 0);
                var y = startingPoint.Y + (isHorizontal ? 0 : i);

                coordinates.Add(new Coordinates(x, y));
            }

            return coordinates;
        }
    }
}