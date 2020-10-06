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
        private readonly IAttackResultStorage _attackResultStorage;
        private readonly Board _board;

        private Game(IAttackingService attackingService, IAttackResultStorage attackResultStorage, Board board)
        {
            _attackingService = attackingService;
            _attackResultStorage = attackResultStorage;
            _board = board;
        }

        public Result<AttackResult> Attack(Coordinates coordinates)
        {
            var result = _attackingService.AttackCoordinates(_board, coordinates);

            if (result.IsSuccess)
            {
                _attackResultStorage.SaveAttackResult(coordinates, result.Value);
            }

            return result;
        }

        public IReadOnlyDictionary<Coordinates, AttackResult> GetAttackResults()
        {
            return _attackResultStorage.GetAttackResults();
        }

        public bool IsOver()
        {
            return !_board.AnyShipAlive();
        }

        public static Result<Game> Initialize(
            IBoardFactory boardFactory,
            IAttackingService attackingService,
            IAttackResultStorage attackResultStorage,
            BoardConfiguration boardConfiguration)
        {
            var createBoardResult = boardFactory.Create(boardConfiguration);

            if (createBoardResult.IsSuccess)
            {
                return Result<Game>.Success(new Game(attackingService, attackResultStorage, createBoardResult.Value));
            }

            return Result<Game>.Error();
        }
    }
}
