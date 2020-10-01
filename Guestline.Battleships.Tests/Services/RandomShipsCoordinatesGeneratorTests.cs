namespace Guestline.Battleships.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Battleships.Models;
    using Battleships.Services;

    using NUnit.Framework;

    public class RandomShipsCoordinatesGeneratorTests
    {
        private RandomShipsCoordinatesGenerator _generator;

        private const int Width = 10;
        private const int Height = 10;

        [SetUp]
        public void Setup()
        {
            _generator = new RandomShipsCoordinatesGenerator(1000);
        }

        [Test]
        public void Generate_WhenManagedToCreateUniqueCoordinatesForEveryShip_ShouldReturnSuccess()
        {
            var ships = new List<ShipConfiguration>
            {
                new ShipConfiguration(2)
            };

            var result = _generator.Generate(ships, Width, Height);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public void Generate_WhenCouldntCreateUniqueCoordinatesForEveryShip_ShouldReturnFailure()
        {
            var boardSize = 3;
            var ships = new List<ShipConfiguration>();
            for (var i = 0; i <= boardSize; i++)
            {
                ships.Add(new ShipConfiguration(3));
            }

            var result = _generator.Generate(ships, boardSize, boardSize);

            Assert.False(result.IsSuccess);
        }

        [Test]
        public void Generate_ShouldCreateCoordinatesForEveryShipConfiguration()
        {
            var ships = new List<ShipConfiguration>
            {
                new ShipConfiguration(4),
                new ShipConfiguration(5)
            };

            var result = _generator.Generate(ships, Width, Height);

            Assert.AreEqual(2, result.Value.Count);
        }

        [Test]
        public void Generate_ShouldCreateCoordinatesForEveryPart()
        {
            var ships = new List<ShipConfiguration>
            {
                new ShipConfiguration(4),
                new ShipConfiguration(5)
            };

            var result = _generator.Generate(ships, Width, Height);

            Assert.AreEqual(ships[0].NumberOfParts, result.Value[0].Count);
            Assert.AreEqual(ships[1].NumberOfParts, result.Value[1].Count);
        }

        [Test]
        public void Generate_ShouldCreateUniqueCoordinates()
        {
            var ships = new List<ShipConfiguration>
            {
                new ShipConfiguration(4),
                new ShipConfiguration(5)
            };

            var result = _generator.Generate(ships, Width, Height).Value.SelectMany(c => c).ToList();

            var duplicatesExists = result.Any(x => result.Count(r => r.Equals(x)) > 1);
            Assert.IsFalse(duplicatesExists);
        }
    }
}