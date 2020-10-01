namespace Guestline.Battleships.Tests.Models
{
    using System.Linq;

    using Battleships.Models;

    using NUnit.Framework;

    public class ShipTests
    {
        [Test]
        public void Damage_WhenShipOccupiesCoordinatesProvided_ShouldDestroyRelatedPart()
        {
            var ship = TestsHelper.CreateShip(2);
            var coordinates = ship.Parts.First().Coordinates;

            ship.Damage(coordinates);

            foreach (var shipPart in ship.Parts)
            {
                Assert.AreEqual(coordinates.Equals(shipPart.Coordinates), shipPart.IsDamaged);
            }
        }

        [Test]
        public void Damage_WhenShipOccupiesCoordinatesProvidedButOtherPartsAreNotDamagedYet_ShouldReturnHit()
        {
            var ship = TestsHelper.CreateShip(2);
            var coordinates = ship.Parts.First().Coordinates;

            var result = ship.Damage(coordinates);

            Assert.AreEqual(AttackResult.Hit, result);
        }

        [Test]
        public void Damage_WhenShipOccupiesCoordinatesProvidedAndOtherPartsAreAlreadyDamaged_ShouldReturnSink()
        {
            var ship = TestsHelper.CreateShip(2);
            var coordinates = ship.Parts.Last().Coordinates;

            for (var i = 0; i < ship.Parts.Count - 1; i++)
            {
                ship.Damage(ship.Parts.ElementAt(i).Coordinates);
            }

            var result = ship.Damage(coordinates);

            Assert.AreEqual(AttackResult.Sink, result);
        }

        [Test]
        public void Damage_WhenShipDoesntOccupyCoordinatesProvided_ShouldntDestroyAnyPart()
        {
            var ship = TestsHelper.CreateShip(2);
            var coordinates = new Coordinates(100, 100);

            ship.Damage(coordinates);

            foreach (var shipPart in ship.Parts)
            {
                Assert.False(shipPart.IsDamaged);
            }
        }

        [Test]
        public void Damage_WhenShipDoesntOccupyCoordinatesProvided_ShouldReturnMiss()
        {
            var ship = TestsHelper.CreateShip(2);
            var coordinates = new Coordinates(100, 100);

            var result = ship.Damage(coordinates);

            Assert.AreEqual(AttackResult.Miss, result);
        }

        [Test]
        public void IsDestroyed_WhenAllPartsAreDamaged_ShouldReturnTrue()
        {
            var ship = TestsHelper.CreateShip(2);

            foreach (var shipPart in ship.Parts)
            {
                ship.Damage(shipPart.Coordinates);
            }

            Assert.True(ship.IsDestroyed);
        }

        [Test]
        public void IsDestroyed_WhenNotAllPartsAreDamaged_ShouldReturnFalse()
        {
            var ship = TestsHelper.CreateShip(2);

            for (var i = 0; i < ship.Parts.Count - 1; i++)
            {
                ship.Damage(ship.Parts.ElementAt(i).Coordinates);
            }

            Assert.False(ship.IsDestroyed);
        }
    }
}