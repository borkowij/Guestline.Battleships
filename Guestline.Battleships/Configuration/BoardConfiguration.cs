namespace Guestline.Battleships.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BoardConfiguration
    {
        public int BoardWidth { get; }

        public int BoardHeight { get; }

        public IReadOnlyCollection<ShipConfiguration> ShipsConfigurations { get; }

        public BoardConfiguration(int boardWidth, int boardHeight, IReadOnlyCollection<ShipConfiguration> shipsConfigurations)
        {
            if (boardWidth <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(boardWidth), "Board width must be a positive number");
            }

            if (boardHeight <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(boardHeight), "Board height must be a positive number");
            }

            if (shipsConfigurations.Count == 0)
            {
                throw new ArgumentException("At least one ship is required in game configuration", nameof(shipsConfigurations));
            }

            if (shipsConfigurations.Any(x => x.NumberOfParts > boardWidth && x.NumberOfParts > boardHeight))
            {
                throw new ArgumentException("Number of ship parts cannot exceed board size", nameof(shipsConfigurations));
            }

            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
            ShipsConfigurations = shipsConfigurations;
        }
    }
}
