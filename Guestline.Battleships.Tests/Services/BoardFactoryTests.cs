namespace Guestline.Battleships.Tests.Services
{
    using System.Collections.Generic;

    using Battleships.Entities;
    using Battleships.Services;

    using Configuration;

    using Interfaces;

    using NSubstitute;

    using NUnit.Framework;

    public class BoardFactoryTests
    {
        private readonly BoardConfiguration _boardConfiguration = new BoardConfiguration(
            10,
            10,
            new List<ShipConfiguration>
            {
                new ShipConfiguration(5),
                new ShipConfiguration(4)
            });

        private IShipFactory _shipFactory;
        private IShipCoordinatesGenerator _shipCoordinatesGenerator;
        private BoardFactory _creator;

        [SetUp]
        public void Setup()
        {
            _shipFactory = Substitute.For<IShipFactory>();
            _shipCoordinatesGenerator = Substitute.For<IShipCoordinatesGenerator>();
            _creator = new BoardFactory(_shipFactory, _shipCoordinatesGenerator, 1000);

            _shipFactory
                .CreateShip(Arg.Any<IEnumerable<Coordinates>>())
                .Returns(TestsHelper.CreateShip(2));
        }

        [Test]
        public void Create_WhenGeneratedUniqueCoordinates_ShouldReturnSuccess()
        {
            _shipCoordinatesGenerator
                .GenerateCoordinates(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(
                    new List<Coordinates>
                    {
                        new Coordinates(1, 0),
                        new Coordinates(2, 0)
                    },
                    new List<Coordinates>
                    {
                        new Coordinates(0, 1),
                        new Coordinates(0, 2)
                    }
                );

            var result = _creator.Create(_boardConfiguration);

            Assert.True(result.IsSuccess);
        }

        [Test]
        public void Create_WhenFailedToGenerateUniqueCoordinates_ShouldReturnFailure()
        {
            _shipCoordinatesGenerator
                .GenerateCoordinates(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(
                    new List<Coordinates>
                    {
                        new Coordinates(1, 0),
                        new Coordinates(2, 0)
                    },
                    new List<Coordinates>
                    {
                        new Coordinates(1, 0),
                        new Coordinates(2, 0)
                    }
                );

            var result = _creator.Create(_boardConfiguration);

            Assert.False(result.IsSuccess);
        }
    }
}