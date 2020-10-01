namespace Guestline.Battleships.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GameConfiguration
    {
        public int Width { get; }

        public int Height { get; }

        public IReadOnlyCollection<ShipConfiguration> ShipsConfigurations { get; }

        public GameConfiguration(int width, int height, IReadOnlyCollection<ShipConfiguration> shipsConfigurations)
        {
            if (width <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Board width must be a positive number");
            }

            if (height <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Board height must be a positive number");
            }

            if (shipsConfigurations.Count == 0)
            {
                throw new ArgumentException("At least one ship is required in game configuration", nameof(shipsConfigurations));
            }

            if (shipsConfigurations.Any(x => x.NumberOfParts > width && x.NumberOfParts > height))
            {
                throw new ArgumentException("Number of ship parts cannot exceed board size", nameof(shipsConfigurations));
            }

            Width = width;
            Height = height;
            ShipsConfigurations = shipsConfigurations;
        }
    }
}
