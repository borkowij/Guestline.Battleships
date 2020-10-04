namespace Guestline.Battleships.Entities
{
    using System;
    using System.Text.RegularExpressions;

    public class Coordinates
    {
        public int X { get; }

        public int Y { get; }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool TryParse(string input, out Coordinates coordinates)
        {
            if (string.IsNullOrEmpty(input) || !Regex.IsMatch(input, "^[a-zA-Z][0-9]+$"))
            {
                coordinates = null;
                return false;
            }

            var y = char.ToUpper(input[0]) - 65;
            var x = Convert.ToInt32(input.Substring(1));

            coordinates = new Coordinates(x, y);
            return true;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Coordinates;

            if (item == null)
            {
                return false;
            }

            return (X == item.X) && (Y == item.Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
