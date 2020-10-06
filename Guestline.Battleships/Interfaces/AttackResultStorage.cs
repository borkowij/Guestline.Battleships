namespace Guestline.Battleships.Interfaces
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Entities;

    public class AttackResultStorage : IAttackResultStorage
    {
        private readonly Dictionary<Coordinates, AttackResult> _attackResults;

        public AttackResultStorage()
        {
            _attackResults = new Dictionary<Coordinates, AttackResult>();
        }

        public void SaveAttackResult(Coordinates coordinates, AttackResult attackResult)
        {
            _attackResults[coordinates] = attackResult;
        }

        public IReadOnlyDictionary<Coordinates, AttackResult> GetAttackResults()
        {
            return new ReadOnlyDictionary<Coordinates, AttackResult>(_attackResults);
        }
    }
}