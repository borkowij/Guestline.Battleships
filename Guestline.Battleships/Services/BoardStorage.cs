namespace Guestline.Battleships.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Models;

    public class BoardStorage : IBoardStorage
    {
        private readonly List<Ship> _ships;

        public BoardStorage()
        {
            _ships = new List<Ship>();
        }

        public bool Add(Ship ship)
        {
            if (ship.Parts.Any(p => !IsCellEmpty(p.Coordinates)))
            {
                return false;
            }

            _ships.Add(ship);

            return true;
        }

        public Ship GetByCoordinates(Coordinates coordinates)
        {
            return _ships.SingleOrDefault(s => s.Parts.Any(
                p => coordinates.Equals(p.Coordinates)));
        }

        public void Clear()
        {
            _ships.Clear();
        }

        public bool AnyShipAlive()
        {
            return _ships.Any(x => !x.IsDestroyed);
        }

        private bool IsCellEmpty(Coordinates coordinates)
        {
            return GetByCoordinates(coordinates) == null;
        }
    }
}