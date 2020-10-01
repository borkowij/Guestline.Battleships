namespace Guestline.Battleships.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Battleships.Models;
    using Battleships.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class BoardServiceTests
    {
        private BoardService _boardService;
        private IBoardStorage _storage;

        [SetUp]
        public void Setup()
        {
            _storage = Substitute.For<IBoardStorage>();
            _boardService = new BoardService(_storage);
        }

        [Test]
        public void AddShip_ShouldAddProperShipToStorage()
        {
            var coordinates = new List<Coordinates>
            {
                new Coordinates(1,0),
                new Coordinates(2,0)
            };

            _boardService.AddShip(coordinates);

            _storage.Received(1).Add(
                Arg.Is<Ship>(
                    x => x.Parts.Count == coordinates.Count &&
                         x.Parts.All(part => coordinates.Contains(part.Coordinates))));
        }

        [Test]
        public void ClearBoard_ShouldClearStorage()
        {
            _boardService.ClearBoard();

            _storage.Received(1).Clear();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AnyShipAlive_WhenNoShips_ShouldPassResultFromStorage(bool anyShipAlive)
        {
            _storage.AnyShipAlive().Returns(anyShipAlive);

            var result = _boardService.AnyShipAlive();

            Assert.AreEqual(anyShipAlive, result);
        }

        [Test]
        public void AttackCoordinates_WhenNoShipsAtCoordinates_ShouldReturnMiss()
        {
            var coordinates = new Coordinates(1, 1);

            _storage.GetByCoordinates(coordinates).Returns((Ship)null);

            var result = _boardService.AttackCoordinates(coordinates);

            Assert.AreEqual(AttackResult.Miss, result);
        }

        [Test]
        public void AttackCoordinates_WhenShipsAtCoordinates_ShouldReturnHit()
        {
            var coordinates = new Coordinates(1, 1);
            var shipOnBoard = TestsHelper.CreateShip(2, coordinates.X, coordinates.Y);
            _storage.GetByCoordinates(coordinates).Returns(shipOnBoard);

            var result = _boardService.AttackCoordinates(coordinates);

            Assert.AreEqual(AttackResult.Hit, result);
        }

        [Test]
        public void AttackCoordinates_WhenLastHealthyPartOfShipAtCoordinates_ShouldReturnSink()
        {
            var coordinates = new Coordinates(1, 1);
            var ship = TestsHelper.CreateShip(2, coordinates.X, coordinates.Y);

            ship.Parts.Last().Damage();

            _storage.GetByCoordinates(coordinates).Returns(ship);

            var result = _boardService.AttackCoordinates(coordinates);

            Assert.AreEqual(AttackResult.Sink, result);
        }

        [Test]
        public void AttackCoordinates_WhenShipsAtCoordinates_ShouldDamagePart()
        {
            var coordinates = new Coordinates(1, 1);
            var ship = TestsHelper.CreateShip(2, coordinates.X, coordinates.Y);

            _storage.GetByCoordinates(coordinates).Returns(ship);

            var result = _boardService.AttackCoordinates(coordinates);

            Assert.IsTrue(ship.Parts.First().IsDamaged);
        }
    }
}