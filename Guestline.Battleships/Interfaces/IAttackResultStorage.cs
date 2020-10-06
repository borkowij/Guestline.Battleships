namespace Guestline.Battleships.Interfaces
{
    using System.Collections.Generic;

    using Entities;

    public interface IAttackResultStorage
    {
        void SaveAttackResult(Coordinates coordinates, AttackResult attackResult);

        IReadOnlyDictionary<Coordinates, AttackResult> GetAttackResults();
    }
}