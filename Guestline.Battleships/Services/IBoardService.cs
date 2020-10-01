namespace Guestline.Battleships.Services
{
    using System.Collections.Generic;

    using Models;

    public interface IBoardService
    {
        bool AddShip(IEnumerable<Coordinates> coordinates);
        AttackResult AttackCoordinates(Coordinates coordinates);
        bool AnyShipAlive();
        void ClearBoard();
    }
}