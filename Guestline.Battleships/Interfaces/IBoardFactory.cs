namespace Guestline.Battleships.Interfaces
{
    using Common;

    using Configuration;

    using Entities;

    public interface IBoardFactory
    {
        Result<Board> Create(BoardConfiguration boardConfiguration);
    }
}