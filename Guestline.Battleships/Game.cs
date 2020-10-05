namespace Guestline.Battleships
{
    using System.Collections.Generic;

    using Common;

    using Configuration;

    using Entities;

    using Interfaces;

    public class Game
    {
        private readonly IAttackingService _attackingService;
        private readonly Board _board;
        private readonly List<(Coordinates Coordinates, AttackResult Result)> _pastAttackResults;

        private Game(IAttackingService attackingService, Board board)
        {
            _attackingService = attackingService;
            _board = board;
            _pastAttackResults = new List<(Coordinates Coordinates, AttackResult Result)>();
        }

        public Result<AttackResult> Attack(Coordinates coordinates)
        {
            var result = _attackingService.AttackCoordinates(_board, coordinates);

            if (result.IsSuccess)
            {
                _pastAttackResults.Add((coordinates, result.Value));
            }

            return result;
        }

        public bool IsOver()
        {
            return !_board.AnyShipAlive();
        }

        public AttackResult?[,] GetAttackResultsTable()
        {
            var result = new AttackResult?[_board.Width, _board.Height];
            foreach (var (coordinates, attackResult) in _pastAttackResults)
            {
                result[coordinates.X, coordinates.Y] = attackResult;
            }

            return result;
        }

        public static Result<Game> Initialize(
            IBoardFactory boardFactory,
            IAttackingService attackingService,
            BoardConfiguration boardConfiguration)
        {
            var createBoardResult = boardFactory.Create(boardConfiguration);

            if (createBoardResult.IsSuccess)
            {
                return Result<Game>.Success(new Game(attackingService, createBoardResult.Value));
            }

            return Result<Game>.Error();
        }
    }
}
