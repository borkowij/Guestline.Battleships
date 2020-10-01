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

        [Test]
        public void TryParse_WhenLowercaseOrUppercaseInput_ShouldReturnSameCoordinates()
        {
            var boardWidth = 2;
            var boardHeight = 3;

            var lowerCaseResult = Coordinates.TryParse("b1", boardWidth, boardHeight, out var lowerCaseCoordinates);
            var upperCaseResult = Coordinates.TryParse("B1", boardWidth, boardHeight, out var upperCaseCoordinates);

            Assert.True(lowerCaseResult);
            Assert.True(upperCaseResult);
            Assert.AreEqual(lowerCaseCoordinates.X, upperCaseCoordinates.X);
            Assert.AreEqual(lowerCaseCoordinates.Y, upperCaseCoordinates.Y);
        }

        [TestCase("A0", 0, 0)]
        [TestCase("C0", 0, 2)]
        [TestCase("A1", 1, 0)]
        [TestCase("C1", 1, 2)]
        public void TryParse_WhenValidInput_ShouldReturnTrueWithProperCoordinates(string input, int x, int y)
        {
            var boardWidth = 2;
            var boardHeight = 3;

            var result = Coordinates.TryParse(input, boardWidth, boardHeight, out var coordinates);

            Assert.True(result);
            Assert.AreEqual(x, coordinates.X);
            Assert.AreEqual(y, coordinates.Y);
        }

        [TestCase("-0")]
        [TestCase("A-")]
        [TestCase("AA")]
        [TestCase("00")]
        [TestCase("")]
        public void TryParse_WhenInvalidInput_ShouldReturnFalseWithNoCoordinates(string input)
        {
            var boardWidth = 2;
            var boardHeight = 3;

            var result = Coordinates.TryParse(input, boardWidth, boardHeight, out var coordinates);

            Assert.False(result);
            Assert.IsNull(coordinates);
        }

        [TestCase("D0")]
        [TestCase("A3")]
        [TestCase("D3")]
        public void TryParse_WhenCoordinatesExceedBoardSize_ShouldReturnFalseWithNoCoordinates(string input)
        {
            var boardWidth = 2;
            var boardHeight = 3;

            var result = Coordinates.TryParse(input, boardWidth, boardHeight, out var coordinates);

            Assert.False(result);
            Assert.IsNull(coordinates);
        }
    }
}