namespace Guestline.Battleships.Tests.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    using Battleships.Entities;

    using NUnit.Framework;

    public class BoardTests
    {
        private const int Width = 10;
        private const int Height = 10;
        private Ship _ship1;
        private Ship _ship2;

        private Board _board;

        [SetUp]
        public void Setup()
        {
            _ship1 = TestsHelper.CreateShip(2, 1);

            _ship2 = TestsHelper.CreateShip(2, 0, 1, false);

            _board = new Board(Width, Height, new List<Ship> { _ship1, _ship2 });
        }

        [Test]
        public void GetShipByCoordinates_WhenCoordinatesAreWrong_ShouldReturnNull()
        {
            var result = _board.GetShipByCoordinates(new Coordinates(100, 100));

            Assert.IsNull(result);
        }

        [Test]
        public void GetShipByCoordinates_WhenCoordinatesAreCorrect_ShouldReturnShip()
        {
            var result = _board.GetShipByCoordinates(_ship1.Coordinates.First());

            Assert.AreEqual(_ship1, result);
        }

        [Test]
        public void AnyShipAlive_WhenNoShips_ShouldReturnFalse()
        {
            _board = new Board(Width, Height, new List<Ship>());
            var result = _board.AnyShipAlive();

            Assert.False(result);
        }

        [Test]
        public void AnyShipAlive_WhenOnlyHealthyShipsAreOnBoard_ShouldReturnTrue()
        {
            var result = _board.AnyShipAlive();

            Assert.True(result);
        }

        [Test]
        public void AnyShipAlive_WhenOneShipIsDestroyedButOtherIsAlive_ShouldReturnTrue()
        {
            foreach (var coordinates in _ship1.Coordinates)
            {
                _ship1.Damage(coordinates);
            }

            var result = _board.AnyShipAlive();

            Assert.True(result);
        }

        [Test]
        public void AnyShipAlive_WhenAllShipsAreDestroyed_ShouldReturnFalse()
        {
            foreach (var coordinates in _ship1.Coordinates)
            {
                _ship1.Damage(coordinates);
            }

            foreach (var coordinates in _ship2.Coordinates)
            {
                _ship2.Damage(coordinates);
            }

            var result = _board.AnyShipAlive();

            Assert.False(result);
        }

        [TestCase(0, 0)]
        [TestCase(0, 9)]
        [TestCase(9, 0)]
        [TestCase(9, 9)]
        [TestCase(4, 4)]
        public void ValidateCoordinates_WhenCoordinatesInsideBoard_ShouldReturnTrue(int x, int y)
        {
            var result = _board.ValidateCoordinates(new Coordinates(x, y));

            Assert.True(result);
        }

        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(10, 0)]
        [TestCase(0, 10)]
        public void ValidateCoordinates_WhenCoordinatesExceedBoardSize_ShouldReturnFalse(int x, int y)
        {
            var result = _board.ValidateCoordinates(new Coordinates(x, y));

            Assert.False(result);
        }
    }
}