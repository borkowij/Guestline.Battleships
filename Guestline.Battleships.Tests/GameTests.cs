namespace Guestline.Battleships.Tests
{
    using System.Collections.Generic;

    using Battleships.Entities;

    using Common;

    using Configuration;

    using Interfaces;

    using NSubstitute;

    using NUnit.Framework;

    public class GameTests
    {
        private readonly BoardConfiguration _boardConfiguration = new BoardConfiguration(
            10,
            10,
            new List<ShipConfiguration>
            {
                new ShipConfiguration(2)
            });

        private Ship _ship;
        private Board _board;
        private Game _game;

        private IAttackingService _attackingService;
        private IAttackResultStorage _attackResultStorage;
        private IBoardFactory _boardFactory;

        [SetUp]
        public void Setup()
        {
            _attackingService = Substitute.For<IAttackingService>();
            _attackResultStorage = Substitute.For<IAttackResultStorage>();
            _boardFactory = Substitute.For<IBoardFactory>();

            _ship = TestsHelper.CreateShip(2);

            _board = new Board(
                _boardConfiguration.BoardWidth,
                _boardConfiguration.BoardHeight,
                new List<Ship>
                {
                    _ship
                });

            _boardFactory
                .Create(Arg.Any<BoardConfiguration>())
                .Returns(Result<Board>.Success(_board));

            _game = Game.Initialize(_boardFactory, _attackingService, _attackResultStorage, _boardConfiguration).Value;
        }

        [Test]
        public void Initialize_WhenBoardCreated_ShouldReturnSuccess()
        {
            _boardFactory
                .Create(Arg.Any<BoardConfiguration>())
                .Returns(Result<Board>.Success(_board));

            var result = Game.Initialize(_boardFactory, _attackingService, _attackResultStorage, _boardConfiguration);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public void Initialize_WhenFailedToCreateBoard_ShouldReturnFailed()
        {
            _boardFactory
                .Create(Arg.Any<BoardConfiguration>())
                .Returns(Result<Board>.Error());

            var result = Game.Initialize(_boardFactory, _attackingService, _attackResultStorage, _boardConfiguration);

            Assert.False(result.IsSuccess);
        }

        [Test]
        public void IsOver_WhenAnyShipIsAlive_ShouldReturnFalse()
        {
            var result = _game.IsOver();

            Assert.False(result);
        }

        [Test]
        public void IsOver_WhenNoShipsAlive_ShouldReturnTrue()
        {
            foreach (var shipCoordinates in _ship.Coordinates)
            {
                _ship.Damage(shipCoordinates);
            }

            var result = _game.IsOver();

            Assert.True(result);
        }

        [Test]
        public void GetAttackResults_ShouldPassValueFromAttackResultStorage()
        {
            var expectedResult = new Dictionary<Coordinates, AttackResult>
            {
                {new Coordinates(0,0), AttackResult.Hit },
                {new Coordinates(1,0), AttackResult.Miss },
                {new Coordinates(2,0), AttackResult.Sink }
            };

            _attackResultStorage.GetAttackResults().Returns(expectedResult);

            var result = _game.GetAttackResults();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Attack_WhenAttackFailed_ShouldReturnFailure()
        {
            _attackingService.AttackCoordinates(Arg.Any<Board>(), Arg.Any<Coordinates>())
                .Returns(Result<AttackResult>.Error());

            var result = _game.Attack(new Coordinates(0, 1));

            Assert.False(result.IsSuccess);
        }

        [TestCase(AttackResult.Miss)]
        [TestCase(AttackResult.Hit)]
        [TestCase(AttackResult.Sink)]
        public void Attack_WhenAttackSuccedeed_ShouldReturnAttackResultFromAttackingService(AttackResult attackResult)
        {
            _attackingService.AttackCoordinates(Arg.Any<Board>(), Arg.Any<Coordinates>())
                .Returns(Result<AttackResult>.Success(attackResult));

            var result = _game.Attack(new Coordinates(0, 1));

            Assert.True(result.IsSuccess);
            Assert.AreEqual(attackResult, result.Value);
        }

        [Test]
        public void Attack_WhenAttackFailed_ShouldNotSaveAttackResultInStorage()
        {
            _attackingService.AttackCoordinates(Arg.Any<Board>(), Arg.Any<Coordinates>())
                .Returns(Result<AttackResult>.Error());

            _game.Attack(new Coordinates(0, 1));

            _attackResultStorage.DidNotReceive().SaveAttackResult(Arg.Any<Coordinates>(), Arg.Any<AttackResult>());
        }

        [Test]
        public void Attack_WhenAttackSuccedeed_ShouldSaveAttackResultInStorage()
        {
            var coordinates = new Coordinates(5, 5);
            var attackResult = AttackResult.Hit;
            _attackingService.AttackCoordinates(Arg.Any<Board>(), Arg.Any<Coordinates>())
                .Returns(Result<AttackResult>.Success(attackResult));

            _game.Attack(coordinates);

            _attackResultStorage.Received(1).SaveAttackResult(coordinates, attackResult);
        }
    }
}