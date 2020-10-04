namespace Guestline.Battleships.Tests.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    using Battleships.Entities;

    using NUnit.Framework;

    public class ShipTests
    {
        private Ship _ship;

        [SetUp]
        public void Setup()
        {
            _ship = TestsHelper.CreateShip(2);
        }

        [Test]
        public void Damage_WhenShipOccupiesCoordinatesProvidedButOtherPartsAreNotDamagedYet_ShouldReturnHit()
        {
            var result = _ship.Damage(_ship.Coordinates.First());

            Assert.AreEqual(AttackResult.Hit, result);
        }

        [Test]
        public void Damage_WhenShipOccupiesCoordinatesProvidedAndOtherPartsAreAlreadyDamaged_ShouldReturnSink()
        {
            var ship = TestsHelper.CreateShip(2);
            var coordinates = _ship.Coordinates.ToList();
            for (var i = 0; i < coordinates.Count - 1; i++)
            {
                ship.Damage(coordinates[i]);
            }

            var result = ship.Damage(coordinates.Last());

            Assert.AreEqual(AttackResult.Sink, result);
        }

        [Test]
        public void Damage_WhenShipDoesntOccupyCoordinatesProvided_ShouldReturnMiss()
        {
            var coordinates = new Coordinates(100, 100);

            var result = _ship.Damage(coordinates);

            Assert.AreEqual(AttackResult.Miss, result);
        }

        [Test]
        public void IsDestroyed_WhenAllPartsAreDamaged_ShouldReturnTrue()
        {
            var ship = TestsHelper.CreateShip(2);

            foreach (var coordinates in _ship.Coordinates)
            {
                ship.Damage(coordinates);
            }

            Assert.True((bool)ship.IsDestroyed);
        }

        [Test]
        public void IsDestroyed_WhenNotAllPartsAreDamaged_ShouldReturnFalse()
        {
            var coordinates = _ship.Coordinates.ToList();
            for (var i = 0; i < coordinates.Count - 1; i++)
            {
                _ship.Damage(coordinates[i]);
            }

            Assert.False(_ship.IsDestroyed);
        }
    }
}