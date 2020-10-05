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
        private readonly List<(Coordinates Coordinates, AttackResult Result)> _attackResultHistory;

        private Game(IAttackingService attackingService, Board board)
        {
            _attackingService = attackingService;
            _board = board;
            _attackResultHistory = new List<(Coordinates Coordinates, AttackResult Result)>();
        }

        public Result<AttackResult> Attack(Coordinates coordinates)
        {
            if (!_board.ValidateCoordinates(coordinates))
            {
                return Result<AttackResult>.Error();
            }

            var result = _attackingService.AttackCoordinates(_board, coordinates);
            _attackResultHistory.Add((coordinates, result));

            return Result<AttackResult>.Success(result);
        }

        public bool IsOver()
        {
            return !_board.AnyShipAlive();
        }

        public AttackResult?[,] GetAttackResultsTable()
        {
            var result = new AttackResult?[_board.Width, _board.Height];
            foreach (var (coordinates, attackResult) in _attackResultHistory)
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
