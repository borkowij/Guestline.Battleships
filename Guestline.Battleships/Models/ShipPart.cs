namespace Guestline.Battleships.Models
{
    public class ShipPart
    {
        public Coordinates Coordinates { get; }

        public bool IsDamaged { get; private set; }

        public ShipPart(Coordinates coordinates)
        {
            Coordinates = coordinates;
            IsDamaged = false;
        }

        public void Damage()
        {
            IsDamaged = true;
        }
    }
}
