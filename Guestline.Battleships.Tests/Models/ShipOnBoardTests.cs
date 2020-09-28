namespace Guestline.Battleships.Tests.Models
{
    using System.Linq;

    using Battleships.Models;
    using Battleships.Models.Ships;

    using NUnit.Framework;

    public class ShipOnBoardTests
    {
        [Test]
        public void Damage_WhenShipOccupiesCoordinatesProvided_ShouldDestroyRelatedPart()
        {
            var shipOnBoard = TestsHelper.CreateShipOnBoard<Destroyer>();
            var coordinates = shipOnBoard.Parts.First().Coordinates;

            shipOnBoard.Damage(coordinates);

            foreach (var shipPart in shipOnBoard.Parts)
            {
                Assert.AreEqual(coordinates.Equals(shipPart.Coordinates), shipPart.IsDamaged);
            }
        }

        [Test]
        public void Damage_WhenShipOccupiesCoordinatesProvidedButOtherPartsAreNotDamagedYet_ShouldReturnHit()
        {
            var shipOnBoard = TestsHelper.CreateShipOnBoard<Destroyer>();
            var coordinates = shipOnBoard.Parts.First().Coordinates;

            var result = shipOnBoard.Damage(coordinates);

            Assert.AreEqual(AttackResult.Hit, result);
        }

        [Test]
        public void Damage_WhenShipOccupiesCoordinatesProvidedAndOtherPartsAreAlreadyDamaged_ShouldReturnSink()
        {
            var shipOnBoard = TestsHelper.CreateShipOnBoard<Destroyer>();
            var coordinates = shipOnBoard.Parts.Last().Coordinates;

            for (var i = 0; i < shipOnBoard.Parts.Count - 1; i++)
            {
                shipOnBoard.Damage(shipOnBoard.Parts.ElementAt(i).Coordinates);
            }

            var result = shipOnBoard.Damage(coordinates);

            Assert.AreEqual(AttackResult.Sink, result);
        }

        [Test]
        public void Damage_WhenShipDoesntOccupyCoordinatesProvided_ShouldntDestroyAnyPart()
        {
            var shipOnBoard = TestsHelper.CreateShipOnBoard<Destroyer>();
            var coordinates = new Coordinates(100, 100);

            shipOnBoard.Damage(coordinates);

            foreach (var shipPart in shipOnBoard.Parts)
            {
                Assert.False(shipPart.IsDamaged);
            }
        }

        [Test]
        public void Damage_WhenShipDoesntOccupyCoordinatesProvided_ShouldReturnMiss()
        {
            var shipOnBoard = TestsHelper.CreateShipOnBoard<Destroyer>();
            var coordinates = new Coordinates(100, 100);

            var result = shipOnBoard.Damage(coordinates);

            Assert.AreEqual(AttackResult.Miss, result);
        }

        [Test]
        public void IsDestroyed_WhenAllPartsAreDamaged_ShouldReturnTrue()
        {
            var shipOnBoard = TestsHelper.CreateShipOnBoard<Destroyer>();

            foreach (var shipPart in shipOnBoard.Parts)
            {
                shipOnBoard.Damage(shipPart.Coordinates);
            }

            Assert.True(shipOnBoard.IsDestroyed);
        }

        [Test]
        public void IsDestroyed_WhenNotAllPartsAreDamaged_ShouldReturnFalse()
        {
            var shipOnBoard = TestsHelper.CreateShipOnBoard<Destroyer>();

            for (var i = 0; i < shipOnBoard.Parts.Count - 1; i++)
            {
                shipOnBoard.Damage(shipOnBoard.Parts.ElementAt(i).Coordinates);
            }

            Assert.False(shipOnBoard.IsDestroyed);
        }
    }
}