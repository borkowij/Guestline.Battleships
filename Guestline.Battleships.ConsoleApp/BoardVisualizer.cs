namespace Guestline.Battleships.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Entities;

    public class BoardVisualizer
    {
        public void Display(TextWriter output, int boardWidth, int boardHeight, IReadOnlyDictionary<Coordinates, AttackResult> attackResults)
        {
            output.Write(" ");

            for (var i = 0; i < boardWidth; i++)
            {
                output.Write(i.ToString().PadLeft(3));
            }

            output.WriteLine();

            for (var y = 0; y < boardHeight; y++)
            {
                output.Write(Convert.ToChar(y + 65));

                for (var x = 0; x < boardWidth; x++)
                {
                    DisplayCellValue(output, x, y, attackResults);
                }

                output.WriteLine();
            }

            output.WriteLine();
        }

        private void DisplayCellValue(TextWriter output, int x, int y, IReadOnlyDictionary<Coordinates, AttackResult> attackResults)
        {
            var result = "o";

            if (attackResults.TryGetValue(new Coordinates(x, y), out var attackResult))
            {
                result = GetAttackResultDisplayValue(attackResult);
            }

            output.WriteLine(result.PadLeft(3));
        }

        private string GetAttackResultDisplayValue(AttackResult attackResult)
        {
            return attackResult switch
            {
                AttackResult.Miss => "m",
                AttackResult.Hit => "h",
                AttackResult.Sink => "s",
                _ => throw new ArgumentOutOfRangeException(nameof(attackResult), attackResult, null)
            };
        }
    }
}