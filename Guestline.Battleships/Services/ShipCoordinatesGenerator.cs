namespace Guestline.Battleships.Services
{
    using System;
    using System.Collections.Generic;

    using Entities;

    using Interfaces;

    public class ShipCoordinatesGenerator : IShipCoordinatesGenerator
    {
        private readonly Random _randomGenerator = new Random();

        public List<Coordinates> GenerateCoordinates(int numberOfCoordinates, int maxWidth, int maxHeight)
        {
            var isHorizontal = _randomGenerator.Next(2) == 0;

            var startingPoint = GenerateStartingPoint(numberOfCoordinates, maxWidth, maxHeight, isHorizontal);
            var coordinates = FillCoordinates(startingPoint, numberOfCoordinates, isHorizontal);
            return coordinates;
        }

        private Coordinates GenerateStartingPoint(int numberOfCoordinates, int maxWidth, int maxHeight, bool isHorizontal)
        {
            var maxStartingX = maxWidth - (isHorizontal ? numberOfCoordinates : 0);
            var maxStartingY = maxHeight - (isHorizontal ? 0 : numberOfCoordinates);

            var x = _randomGenerator.Next(0, maxStartingX);
            var y = _randomGenerator.Next(0, maxStartingY);

            return new Coordinates(x, y);
        }

        private List<Coordinates> FillCoordinates(Coordinates startingPoint, int numberOfCoordinates, bool isHorizontal)
        {
            var coordinates = new List<Coordinates>()
            {
                startingPoint
            };

            for (var i = 1; i < numberOfCoordinates; i++)
            {
                var x = startingPoint.X + (isHorizontal ? i : 0);
                var y = startingPoint.Y + (isHorizontal ? 0 : i);

                coordinates.Add(new Coordinates(x, y));
            }

            return coordinates;
        }
    }
}
