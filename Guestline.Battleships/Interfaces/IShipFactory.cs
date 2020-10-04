namespace Guestline.Battleships.Interfaces
{
    using System.Collections.Generic;

    using Entities;

    public interface IShipFactory
    {
        Ship CreateShip(IEnumerable<Coordinates> coordinates);
    }
}