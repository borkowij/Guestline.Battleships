namespace Guestline.Battleships.Tests
{
    using System.Collections.Generic;

    using Battleships.Entities;

    public static class TestsHelper
    {
        public static Ship CreateShip(int numberOfParts, int startX = 0, int startY = 0, bool isHorizontal = true)
        {
            var parts = new List<ShipPart>();
            for (var i = 0; i < numberOfParts; i++)
            {
                parts.Add(new ShipPart(new Coordinates(startX + (isHorizontal ? i : 0), startY + (isHorizontal ? 0 : i))));
            }

            return new Ship(parts);
        }
    }
}
