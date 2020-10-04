namespace Guestline.Battleships.Services
{
    using Entities;

    using Interfaces;

    public class AttackingService : IAttackingService
    {
        public AttackResult AttackCoordinates(Board board, Coordinates coordinates)
        {
            var ship = board.GetShipByCoordinates(coordinates);

            return ship?.Damage(coordinates) ?? AttackResult.Miss;
        }
    }
}
