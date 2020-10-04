namespace Guestline.Battleships.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    public class Ship
    {
        private readonly IReadOnlyCollection<ShipPart> _parts;

        public Ship(IReadOnlyCollection<ShipPart> parts)
        {
            _parts = parts;
        }

        public bool IsDestroyed => _parts.All(x => x.IsDamaged);

        public IEnumerable<Coordinates> Coordinates =>
            _parts.Select(x => x.Coordinates);

        public AttackResult Damage(Coordinates coordinates)
        {
            var partToDamage = _parts.SingleOrDefault(x => x.Coordinates.Equals(coordinates));

            if (partToDamage == null)
            {
                return AttackResult.Miss;
            }

            partToDamage.Damage();

            return IsDestroyed ? AttackResult.Sink : AttackResult.Hit;
        }
    }
}
