namespace Guestline.Battleships.Services
{
    using System.Collections.Generic;

    using Models;

    public interface IShipsCoordinatesGenerator
    {
        Result<List<List<Coordinates>>> Generate(IEnumerable<ShipConfiguration> shipsConfigurations, int boardWidth, int boardHeight);
    }
}