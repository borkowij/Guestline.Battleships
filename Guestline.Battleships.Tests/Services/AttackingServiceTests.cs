namespace Guestline.Battleships.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Battleships.Entities;
    using Battleships.Services;

    using NUnit.Framework;

    public class AttackingServiceTests
    {
        private Ship _ship;
        private Board _board;

        private AttackingService _attackingService;

        [SetUp]
        public void Setup()
        {
            _ship = TestsHelper.CreateShip(2);

            _board = new Board(
                10,
                10,
                new List<Ship>
                {
                    _ship
                });

            _attackingService = new AttackingService();
        }

        [Test]
        public void Attack_WhenInvalidCoordinates_ShouldReturnFailure()
        {
            var result = _attackingService.AttackCoordinates(_board, new Coordinates(100, 100));

            Assert.False(result.IsSuccess);
        }

        [Test]
        public void Attack_WhenShipIsHitAndAlive_ShouldReturnHit()
        {
            var result = _attackingService.AttackCoordinates(_board, _ship.Coordinates.First());

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(AttackResult.Hit, result.Value);
        }

        [Test]
        public void Attack_WhenShipIsHitAndDestroyed_ShouldReturnSink()
        {
            _ship.Damage(_ship.Coordinates.First());

            var result = _attackingService.AttackCoordinates(_board, _ship.Coordinates.Last());

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(AttackResult.Sink, result.Value);
        }

        [Test]
        public void Attack_WhenNoShipFoundUnderCoordinatesProvided_ShouldReturnMiss()
        {
            var result = _attackingService.AttackCoordinates(_board, new Coordinates(5, 5));

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(AttackResult.Miss, result.Value);
        }
    }
}