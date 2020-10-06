namespace Guestline.Battleships.Tests.Services
{
    using Battleships.Entities;

    using Interfaces;

    using NUnit.Framework;

    public class AttackResultStorageTests
    {
        private AttackResultStorage _attackResultStorage;

        [SetUp]
        public void Setup()
        {
            _attackResultStorage = new AttackResultStorage();
        }

        [Test]
        public void GetAttackResults_WhenNoAttacksSaved_ShouldReturnEmptyDictionary()
        {
            var result = _attackResultStorage.GetAttackResults();

            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAttackResults_WhenSomeAttacksSaved_ShouldReturnDictionaryWithProperValues()
        {
            var attackResult1 = (Coordinates: new Coordinates(0, 0), AttackResult: AttackResult.Sink);
            var attackResult2 = (Coordinates: new Coordinates(1, 0), AttackResult: AttackResult.Hit);
            var attackResult3 = (Coordinates: new Coordinates(2, 0), AttackResult: AttackResult.Miss);
            _attackResultStorage.SaveAttackResult(attackResult1.Coordinates, attackResult1.AttackResult);
            _attackResultStorage.SaveAttackResult(attackResult2.Coordinates, attackResult2.AttackResult);
            _attackResultStorage.SaveAttackResult(attackResult3.Coordinates, attackResult3.AttackResult);

            var result = _attackResultStorage.GetAttackResults();

            Assert.AreEqual(attackResult1.AttackResult, result[attackResult1.Coordinates]);
            Assert.AreEqual(attackResult2.AttackResult, result[attackResult2.Coordinates]);
            Assert.AreEqual(attackResult3.AttackResult, result[attackResult3.Coordinates]);
        }
    }
}