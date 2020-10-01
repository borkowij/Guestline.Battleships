namespace Guestline.Battleships.Services
{
    using Models;

    public interface IBoardStorage
    {
        bool Add(Ship ship);
        Ship GetByCoordinates(Coordinates coordinates);
        void Clear();
        bool AnyShipAlive();
    }
}