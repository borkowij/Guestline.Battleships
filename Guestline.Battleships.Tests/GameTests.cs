namespace Guestline.Battleships.Tests
{
    using System.Collections.Generic;

    using Battleships.Models;
    using Battleships.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class GameTests
    {
        private Game _game;
        private IBoardService _boardService;
        private IShipsCoordinatesGenerator _shipsCoordinatesGenerator;
        private GameConfiguration _gameConfiguration;

        [SetUp]
        public void Setup()
        {
            _gameConfiguration = new GameConfiguration(
                10,
                10,
                new List<ShipConfiguration>
                {
                    new ShipConfiguration(4)
                });

            _boardService = Substitute.For<IBoardService>();
            _shipsCoordinatesGenerator = Substitute.For<IShipsCoordinatesGenerator>();

            _game = new Game(_boardService, _shipsCoordinatesGenerator);

            _shipsCoordinatesGenerator.Generate(Arg.Any<IEnumerable<ShipConfiguration>>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(Result<List<List<Coordinates>>>.Success(new List<List<Coordinates>>(){
                        new List<Coordinates>
                        {
                            new Coordinates(0,0),
                            new Coordinates(1,0),
                            new Coordinates(2,0),
                            new Coordinates(3,0),
                        }
                    })
            );
        }

        [Test]
        public void Initialize_ShouldClearBoard()
        {
            _game.Initialize(_gameConfiguration);

            _boardService.Received(1).ClearBoard();
        }

        [Test]
        public void Initialize_ShouldPlaceShipsOnBoard()
        {
            _game.Initialize(_gameConfiguration);

            _boardService.Received(1).AddShip(Arg.Any<IEnumerable<Coordinates>>());
        }

        [Test]
        public void Initialize_WhenCouldntGenerateShipsCoordinates_ShouldReturnFalse()
        {
            _shipsCoordinatesGenerator.Generate(
                Arg.Any<IEnumerable<ShipConfiguration>>(),
                Arg.Any<int>(),
                Arg.Any<int>()).Returns(Result<List<List<Coordinates>>>.Error());

            var result = _game.Initialize(_gameConfiguration);

            Assert.False(result);
        }

        [Test]
        public void Initialize_WhenCouldntAddShip_ShouldReturnFalse()
        {
            _boardService.AddShip(Arg.Any<IEnumerable<Coordinates>>()).Returns(false);

            var result = _game.Initialize(_gameConfiguration);

            Assert.False(result);
        }

        [Test]
        public void Initialize_WhenAllShipsAdded_ShouldReturnTrue()
        {
            _boardService.AddShip(Arg.Any<IEnumerable<Coordinates>>()).Returns(true);

            var result = _game.Initialize(_gameConfiguration);

            Assert.True(result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsOver_ShouldReturnProperValue(bool anyShipAlive)
        {
            _boardService.AnyShipAlive().Returns(anyShipAlive);

            var result = _game.IsOver();

            Assert.AreEqual(!anyShipAlive, result);
        }

        [TestCase(AttackResult.Miss)]
        [TestCase(AttackResult.Hit)]
        [TestCase(AttackResult.Sink)]
        public void Attack_ShouldReturnProperValue(AttackResult attackResult)
        {
            _boardService.AttackCoordinates(Arg.Any<Coordinates>()).Returns(attackResult);

            _game.Initialize(_gameConfiguration);

            var result = _game.Attack(new Coordinates(0, 0));

            Assert.AreEqual(attackResult, result);
        }
    }
}