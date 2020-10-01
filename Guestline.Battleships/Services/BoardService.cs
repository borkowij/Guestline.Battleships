namespace Guestline.Battleships.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Models;

    public class BoardService : IBoardService
    {
        private readonly IBoardStorage _board;

        public BoardService(IBoardStorage board)
        {
            _board = board;
        }

        public bool AddShip(IEnumerable<Coordinates> coordinates)
        {
            var shipParts = coordinates.Select(x => new ShipPart(x)).ToList();
            var ship = new Ship(shipParts);

            return _board.Add(ship);
        }

        public AttackResult AttackCoordinates(Coordinates coordinates)
        {
            var ship = _board.GetByCoordinates(coordinates);

            return ship == null ? AttackResult.Miss : ship.Damage(coordinates);
        }

        public bool AnyShipAlive()
        {
            return _board.AnyShipAlive();
        }

        public void ClearBoard()
        {
            _board.Clear();
        }
    }
}
