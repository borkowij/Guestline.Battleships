namespace Guestline.Battleships.Models
{
    public class ShipConfiguration
    {
        public int NumberOfParts { get; }

        public ShipConfiguration(int numberOfParts)
        {
            NumberOfParts = numberOfParts;
        }
    }
}
