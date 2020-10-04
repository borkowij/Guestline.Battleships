namespace Guestline.Battleships.Interfaces
{
    using System.Collections.Generic;

    using Entities;

    public interface IShipCoordinatesGenerator
    {
        List<Coordinates> GenerateCoordinates(int numberOfCoordinates, int maxWidth, int maxHeight);
    }
}