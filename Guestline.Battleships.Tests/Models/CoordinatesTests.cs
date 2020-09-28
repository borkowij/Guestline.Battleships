namespace Guestline.Battleships.Tests.Models
{
    using Battleships.Models;
    using NUnit.Framework;

    public class CoordinatesTests
    {
        [Test]
        public void Equals_WhenCoordinatesHaveSameXAndYValues_ShouldReturnTrue()
        {
            var coordinate1 = new Coordinates(2, 4);
            var coordinate2 = new Coordinates(2, 4);

            var result = coordinate1.Equals(coordinate2);

            Assert.True(result);
        }

        [TestCase(1, 1)]
        [TestCase(2, 5)]
        [TestCase(5, 4)]
        public void Equals_WhenCoordinatesHaveDifferentXAndYValues_ShouldReturnFalse(int x, int y)
        {
            var coordinate1 = new Coordinates(2, 4);
            var coordinate2 = new Coordinates(x, y);

            var result = coordinate1.Equals(coordinate2);

            Assert.False(result);
        }
    }
}