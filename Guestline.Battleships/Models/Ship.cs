namespace Guestline.Battleships.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Ship
    {
        public IReadOnlyCollection<ShipPart> Parts { get; }

        public bool IsDestroyed => Parts.All(x => x.IsDamaged);

        public Ship(IReadOnlyCollection<ShipPart> parts)
        {
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
