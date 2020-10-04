namespace Guestline.Battleships.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    public class Board
    {
        public int Width { get; }

        public int Height { get; }

        private readonly List<Ship> _ships;

        public Board(int width, int height, List<Ship> ships)
        {
            Width = width;
            Height = height;
            _ships = ships;
        }

        public Ship GetShipByCoordinates(Coordinates coordinates)
        {
            return _ships.SingleOrDefault(ship => ship.Coordinates.Any(coordinates.Equals));
        }

        public bool AnyShipAlive()
        {
            return _ships.Any(x => !x.IsDestroyed);
        }

        public bool ValidateCoordinates(Coordinates coordinates)
        {
            return coordinates.X >= 0 && coordinates.X < Width && coordinates.Y >= 0 && coordinates.Y < Height;
        }
    }
}