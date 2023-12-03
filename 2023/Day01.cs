using AdventLibrary;

namespace _2023;
public class Day01 : IDay
{
    private readonly string[] _lines;

    public Day01(InputReader reader)
    {
        _lines = reader.GetData("Day01.txt");
    }

    public int FirstPuzzle()
    {
        int output = 0;

        foreach (string line in _lines)
        {
            output += int.Parse($"{line.First(char.IsDigit)}{line.Last(char.IsDigit)}");
        }

        return output;
    }

    public int SecondPuzzle()
    {
        List<int> digits = new();
        int output = 0;
        Dictionary<string, int> table = new()
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9,
        };

        foreach (string line in _lines)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    digits.Add((int)char.GetNumericValue(line[i]));
                    continue;
                }

                foreach (KeyValuePair<string, int> row in table)
                {
                    if (line[i..].StartsWith(row.Key))
                    {
                        digits.Add(row.Value);
                    }
                }
            }

            int number = digits.First() * 10 + digits.Last();
            output += number;
            digits.Clear();
        }

        return output;
    }
}