using AdventLibrary;

namespace _2023;
public class Day03 : IDay
{
    private readonly string[] _lines;

    public Day03(InputReader reader)
    {
        _lines = reader.GetData("Day03.txt");
    }

    public int FirstPuzzle()
    {
        int output = 0;

        for (int row = 0; row < _lines.Length; row++)
        {
            for (int column = 0; column < _lines[row].Length; column++)
            {
                if (!char.IsDigit(_lines[row][column]))
                {
                    continue;
                }

                string cut = "";
                bool isPartNumber = false;

                while (column < _lines[row].Length && char.IsDigit(_lines[row][column]))
                {
                    cut += _lines[row][column];

                    if (!isPartNumber)
                    {
                        isPartNumber = IsSpecialCharacterAround(row, column);
                    }

                    column += 1;
                }

                if (isPartNumber)
                {
                    output += int.Parse(cut);
                }
            }
        }

        return output;
    }

    public int SecondPuzzle()
    {
        int output = 0;

        for (int row = 0; row < _lines.Length; row++)
        {
            for (int column = 0; column < _lines[row].Length; column++)
            {
                if (_lines[row][column] != '*')
                {
                    continue;
                }

                List<int> numbers = DigitsAround(row, column);

                if (numbers.Count == 2)
                {
                    output += (numbers[0] * numbers[1]);
                }
            }
        }

        return output;
    }

    private List<int> DigitsAround(int startRow, int startColumn)
    {
        List<int> output = new();

        for (int row = startRow - 1; row <= startRow + 1; row++)
        {
            bool canAdd = true;
            for (int column = startColumn - 1; column <= startColumn + 1; column++)
            {
                if ((row == startRow && column == startColumn) || !AreCoordinatesValid(row,column) || !char.IsDigit(_lines[row][column]))
                {
                    canAdd = true;
                    continue;
                }

                if (AreCoordinatesValid(row, column) && char.IsDigit(_lines[row][column]) && canAdd)
                {
                        output.Add(ExtractNumber(column, _lines[row]));
                        canAdd = false;
                }
            }
        }

        return output;
    }

    private static int ExtractNumber(int start, string line)
    {
        List<char> cut = new();

        int next = start - 1;
        while (next >= 0 && char.IsDigit(line[next]))
        {
            cut.Add(line[next]);
            next -= 1;
        }

        cut.Reverse();
        cut.Add(line[start]);

        next = start + 1;
        while (next < line.Length && char.IsDigit(line[next]))
        {
            cut.Add(line[next]);
            next += 1;
        }

        return int.Parse(string.Join("", cut));
    }

    private bool IsSpecialCharacterAround(int row, int column)
    {
        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = column - 1; j <= column + 1; j++)
            {
                if (i == row && j == column )
                {
                    continue;
                }

                if (AreCoordinatesValid(i,j) && _lines[i][j] != '.' && !char.IsDigit(_lines[i][j]))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool AreCoordinatesValid(int row, int column)
    {
        return column > 0 && row > 0 && column < _lines.Length && row < _lines[column].Length;
    }
}