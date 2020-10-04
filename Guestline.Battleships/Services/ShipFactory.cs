namespace Guestline.Battleships.Services
{
    using System.Collections.Generic;

    using System.Linq;

    using Entities;

    using Interfaces;

    public class ShipFactory : IShipFactory
    {
        public Ship CreateShip(IEnumerable<Coordinates> coordinates)
        {
            var shipParts = coordinates.Select(x => new ShipPart(x)).ToList();
            
            return new Ship(shipParts);
        }
    }
}
