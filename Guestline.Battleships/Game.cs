namespace Guestline.Battleships
{
    using Models;

    using Services;

    public class Game
    {
        private readonly IBoardService _boardService;
        private readonly IShipsCoordinatesGenerator _shipsCoordinatesGenerator;

        public Game(IBoardService boardService, IShipsCoordinatesGenerator shipsCoordinatesGenerator)
        {
            _boardService = boardService;
            _shipsCoordinatesGenerator = shipsCoordinatesGenerator;
        }

        public bool Initialize(GameConfiguration gameConfiguration)
        {
            var shipsCoordinatesGenerationResult = _shipsCoordinatesGenerator.Generate(
                gameConfiguration.ShipsConfigurations,
                gameConfiguration.Width,
                gameConfiguration.Height);

            if (!shipsCoordinatesGenerationResult.IsSuccess)
            {
                return false;
            }

            _boardService.ClearBoard();

            foreach (var shipCoordinates in shipsCoordinatesGenerationResult.Value)
            {
                if (!_boardService.AddShip(shipCoordinates))
                {
                    return false;
                }
            }

            return true;
        }

        public AttackResult Attack(Coordinates coordinates)
        {
            var result = _boardService.AttackCoordinates(coordinates);

            return result;
        }

        public bool IsOver()
        {
            return !_boardService.AnyShipAlive();
        }
    }
}
