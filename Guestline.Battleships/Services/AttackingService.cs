namespace Guestline.Battleships.Services
{
    using Common;
    using Entities;

    using Interfaces;

    public class AttackingService : IAttackingService
    {
        public Result<AttackResult> AttackCoordinates(Board board, Coordinates coordinates)
        {
            if (!board.ValidateCoordinates(coordinates))
            {
                return Result<AttackResult>.Error();
            }

            var ship = board.GetShipByCoordinates(coordinates);

            return Result<AttackResult>.Success(ship?.Damage(coordinates) ?? AttackResult.Miss);
        }
    }
}
