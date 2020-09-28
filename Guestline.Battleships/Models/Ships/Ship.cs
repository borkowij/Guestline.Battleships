namespace Guestline.Battleships.Models.Ships
{
    public abstract class Ship
    {
        public string Name { get; }

        public int NumberOfParts { get; }

        protected Ship(string name, int numberOfParts)
        {
            Name = name;
            NumberOfParts = numberOfParts;
        }
    }
}
