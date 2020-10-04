namespace Guestline.Battleships.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Common;

    using Configuration;

    using Entities;

    using Interfaces;

    public class BoardFactory : IBoardFactory
    {
        private readonly IShipFactory _shipFactory;
        private readonly IShipCoordinatesGenerator _shipCoordinatesGenerator;
        private readonly int _maxRetries;

        public BoardFactory(
            IShipFactory shipFactory,
            IShipCoordinatesGenerator shipCoordinatesGenerator,
            int maxRetries)
        {
            _shipFactory = shipFactory;
            _shipCoordinatesGenerator = shipCoordinatesGenerator;
            _maxRetries = maxRetries;
        }

        public Result<Board> Create(BoardConfiguration boardConfiguration)
        {
            var occupiedCoordinates = new List<Coordinates>();
            var ships = new List<Ship>();

            foreach (var shipConfiguration in boardConfiguration.ShipsConfigurations)
            {
                if (TryCreateShip(
                    shipConfiguration.NumberOfParts,
                    boardConfiguration.BoardWidth,
                    boardConfiguration.BoardHeight,
                    occupiedCoordinates,
                    out var ship))
                {
                    ships.Add(ship);
                }
                else
                {
                    return Result<Board>.Error();
                }
            }

            return Result<Board>.Success(new Board(boardConfiguration.BoardWidth, boardConfiguration.BoardHeight, ships));
        }

        private bool TryCreateShip(
            int numberOfParts,
            int boardWidth,
            int boardHeight,
            List<Coordinates> occupiedCoordinates,
            out Ship ship)
        {
            var iterations = 0;
            while (iterations++ < _maxRetries)
            {
                var coordinates = _shipCoordinatesGenerator.GenerateCoordinates(
                    numberOfParts,
                    boardWidth,
                    boardHeight);

                if (!occupiedCoordinates.Any(x => coordinates.Any(y => y.Equals(x))))
                {
                    occupiedCoordinates.AddRange(coordinates);
                    ship = _shipFactory.CreateShip(coordinates);
                    return true;
                }
            }

            ship = null;
            return false;
        }
    }
}