namespace Guestline.Battleships.Models
{
    using System;

    public class ShipConfiguration
    {
        public int NumberOfParts { get; }

        public ShipConfiguration(int numberOfParts)
        {
            if (numberOfParts <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(numberOfParts),
                    numberOfParts,
                    "Number of parts must be a positive number");
            }

            NumberOfParts = numberOfParts;
        }
    }
}
