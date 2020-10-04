namespace Guestline.Battleships.ConsoleApp
{
    using System;
    using System.IO;

    using Entities;

    public class BoardVisualizer
    {
        public void Display(TextWriter output, AttackResult?[,] attackResultsTable)
        {
            output.Write(" ");

            for (var i = 0; i < attackResultsTable.GetLength(0); i++)
            {
                output.Write(i.ToString().PadLeft(3));
            }

            output.WriteLine();

            for (var y = 0; y < attackResultsTable.GetLength(1); y++)
            {
                output.Write(Convert.ToChar(y + 65));

                for (var x = 0; x < attackResultsTable.GetLength(0); x++)
                {
                    output.Write(GetCharValue(attackResultsTable[x, y]).ToString().PadLeft(3));
                }

                output.WriteLine();
            }

            output.WriteLine();
        }

        private char GetCharValue(AttackResult? attackResult)
        {
            return attackResult switch
            {
                null => 'o',
                AttackResult.Miss => 'm',
                AttackResult.Hit => 'h',
                AttackResult.Sink => 's',
                _ => throw new ArgumentOutOfRangeException(nameof(attackResult), attackResult, null)
            };
        }
    }
}