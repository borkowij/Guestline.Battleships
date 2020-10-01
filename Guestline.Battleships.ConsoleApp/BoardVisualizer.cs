namespace Guestline.Battleships.ConsoleApp
{
    using System;
    using System.IO;

    using Models;

    public class BoardVisualizer
    {
        private readonly char[,] _attackResults;

        public BoardVisualizer(int boardWidth, int boardHeight)
        {
            _attackResults = new char[boardWidth, boardHeight];

            ClearAttackResults();
        }

        public void SaveAttackResult(Coordinates coordinates, AttackResult attackResult)
        {
            _attackResults[coordinates.X, coordinates.Y] = GetCharValue(attackResult);
        }

        public void Display(TextWriter output)
        {
            output.Write(" ");
            for (var i = 0; i < _attackResults.GetLength(0); i++)
            {
                output.Write(i.ToString().PadLeft(3));
            }

            output.WriteLine();

            for (var y = 0; y < _attackResults.GetLength(1); y++)
            {
                output.Write(Convert.ToChar(y + 65));

                for (var x = 0; x < _attackResults.GetLength(0); x++)
                {
                    output.Write(_attackResults[x, y].ToString().PadLeft(3));
                }

                output.WriteLine();
            }

            output.WriteLine();
        }

        private void ClearAttackResults()
        {
            for (var y = 0; y < _attackResults.GetLength(1); y++)
            {
                for (var x = 0; x < _attackResults.GetLength(0); x++)
                {
                    _attackResults[x, y] = 'o';
                }
            }
        }

        private char GetCharValue(AttackResult attackResult)
        {
            return attackResult switch
            {
                AttackResult.Miss => 'm',
                AttackResult.Hit => 'h',
                AttackResult.Sink => 's',
                _ => throw new ArgumentOutOfRangeException(nameof(attackResult), attackResult, null)
            };
        }
    }
}