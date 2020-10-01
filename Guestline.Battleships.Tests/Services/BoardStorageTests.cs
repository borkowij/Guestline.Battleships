namespace Guestline.Battleships.Tests.Services
{
    using Battleships.Models;
    using Battleships.Services;

    using NUnit.Framework;

    public class BoardStorageTests
    {
        private BoardStorage _storage;

        [SetUp]
        public void Setup()
        {
            _storage = new BoardStorage();
        }

        [Test]
        public void Add_WhenPutOnUnoccupiedCells_ShouldReturnFalse()
        {
            var ship = TestsHelper.CreateShip(4);

            _storage.Add(ship);

            Assert.Pass();
        }

        [Test]
        public void Add_WhenPutOnOccupiedCells_ShouldReturnFalse()
        {
            var ship = TestsHelper.CreateShip(4);
            _storage.Add(ship);
            var ship2 = TestsHelper.CreateShip(4, 3);

            var result = _storage.Add(ship2);

            Assert.IsFalse(result);
        }

        [Test]
        public void GetByCoordinates_WhenCoordinatesAreWrong_ShouldReturnNull()
        {
            var ship = TestsHelper.CreateShip(4,1);
            _storage.Add(ship);

            var result = _storage.GetByCoordinates(new Coordinates(0, 0));

            Assert.IsNull(result);
        }

        [Test]
        public void GetByCoordinates_WhenCoordinatesAreCorrect_ShouldReturnShip()
        {
            var ship = TestsHelper.CreateShip(4);
            _storage.Add(ship);

            var result = _storage.GetByCoordinates(new Coordinates(1, 0));

            Assert.AreEqual(ship, result);
        }

        [Test]
        public void AnyShipAlive_WhenNoShips_ShouldReturnFalse()
        {
            var result = _storage.AnyShipAlive();

            Assert.False(result);
        }

        [Test]
        public void AnyShipAlive_WhenOnlyHealthyShipsAreOnBoard_ShouldReturnTrue()
        {
            var ship = TestsHelper.CreateShip(4);
            var ship2 = TestsHelper.CreateShip(4, 0, 1);
            _storage.Add(ship);
            _storage.Add(ship2);

            var result = _storage.AnyShipAlive();

            Assert.True(result);
        }

        [Test]
        public void AnyShipAlive_WhenOneShipIsDestroyedButOtherIsAlive_ShouldReturnTrue()
        {
            var ship = TestsHelper.CreateShip(4);
            var ship2 = TestsHelper.CreateShip(4, 0, 1);
            _storage.Add(ship);
            _storage.Add(ship2);
            foreach (var shipPart in ship.Parts)
            {
                shipPart.Damage();
            }

            var result = _storage.AnyShipAlive();

            Assert.True(result);
        }

        [Test]
        public void AnyShipAlive_WhenAllShipsAreDestroyed_ShouldReturnFalse()
        {
            var ship = TestsHelper.CreateShip(4);
            _storage.Add(ship);
            foreach (var shipPart in ship.Parts)
            {
                shipPart.Damage();
            }

            var result = _storage.AnyShipAlive();

            Assert.False(result);
        }

        [Test]
        public void Clear_WhenAnyShipsOnBoard_ShouldRemoveAllShipsFromBoard()
        {
            var ship = TestsHelper.CreateShip(4);
            _storage.Add(ship);

            _storage.Clear();

            Assert.False(_storage.AnyShipAlive());
        }
    }
}