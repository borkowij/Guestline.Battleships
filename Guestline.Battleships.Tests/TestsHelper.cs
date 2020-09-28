
namespace Guestline.Battleships.Tests
{
    using Battleships.Models;
    using Battleships.Models.Ships;

    using System.Collections.Generic;

    public static class TestsHelper
    {
        public static ShipOnBoard CreateShipOnBoard<T>(int startX = 0, int startY = 0, bool isHorizontal = true) where T : Ship, new()
        {
            var ship = new T();
            var parts = new List<ShipPart>();
            for (var i = 0; i < ship.NumberOfParts; i++)
            {
                parts.Add(new ShipPart(new Coordinates(startX + (isHorizontal ? i : 0), startY + (isHorizontal ? 0 : i))));
            }

            return new ShipOnBoard(ship, parts);
        }
    }
}
