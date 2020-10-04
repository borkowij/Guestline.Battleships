namespace Guestline.Battleships.Tests.Services
{
    using System;

    using Battleships.Services;

    using NUnit.Framework;

    public class ShipCoordinatesGeneratorTests
    {
        private readonly ShipCoordinatesGenerator _shipCoordinatesGenerator = new ShipCoordinatesGenerator();

        [Test]
        public void GenerateCoordinates_ShouldReturnRequiredNumberOfCoordinates()
        {
            var numberOfCoordinates = 2;

            var result = _shipCoordinatesGenerator.GenerateCoordinates(numberOfCoordinates, 5, 5);

            Assert.AreEqual(numberOfCoordinates, result.Count);
        }

        [Test]
        public void GenerateCoordinates_ShouldReturnAdjacentCoordinates()
        {
            var result = _shipCoordinatesGenerator.GenerateCoordinates(5, 5, 5);

            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.That(Math.Abs(result[i].X - result[i + 1].X) == 1 ^ Math.Abs(result[i].Y - result[i + 1].Y) == 1);
            }
        }
    }
}