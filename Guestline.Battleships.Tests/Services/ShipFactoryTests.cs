namespace Guestline.Battleships.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Battleships.Entities;
    using Battleships.Services;

    using NUnit.Framework;

    public class ShipFactoryTests
    {
        private readonly ShipFactory _shipFactory = new ShipFactory();

        [Test]
        public void CreateShip_ShouldReturnShipWithCoordinatesProvided()
        {
            var coordinates = new List<Coordinates> { new Coordinates(0, 0), new Coordinates(1, 0) };

            var result = _shipFactory.CreateShip(coordinates);

            Assert.That(result.Coordinates.Count() == coordinates.Count);
            Assert.That(result.Coordinates.All(x => coordinates.Contains(x)));
        }
    }
}