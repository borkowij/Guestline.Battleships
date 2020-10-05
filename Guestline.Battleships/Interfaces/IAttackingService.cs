namespace Guestline.Battleships.Interfaces
{
    using Common;
    using Entities;

    public interface IAttackingService
    {
        Result<AttackResult> AttackCoordinates(Board board, Coordinates coordinates);
    }
}