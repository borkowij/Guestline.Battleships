namespace Guestline.Battleships.Interfaces
{
    using Entities;

    public interface IAttackingService
    {
        AttackResult AttackCoordinates(Board board, Coordinates coordinates);
    }
}