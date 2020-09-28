namespace Guestline.Battleships.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ships;

    public class ShipOnBoard
    {
        public Ship Ship { get; }

        public IReadOnlyCollection<ShipPart> Parts { get; }

        public bool IsDestroyed => Parts.All(x => x.IsDamaged);

        public ShipOnBoard(Ship ship, IReadOnlyCollection<ShipPart> parts)
        {
            if (ship == null)
            {
                throw new ArgumentNullException(nameof(ship));
            }

            if (parts.Count != ship.NumberOfParts)
            {
                throw new Exception();
            }

            Ship = ship;
            Parts = parts;
        }

        public AttackResult Damage(Coordinates coordinates)
        {
            var partToDamage = Parts.SingleOrDefault(x => x.Coordinates.Equals(coordinates));

            if (partToDamage == null)
            {
                return AttackResult.Miss;
            }

            partToDamage.Damage();

            return IsDestroyed ? AttackResult.Sink : AttackResult.Hit;
        }
    }
}
