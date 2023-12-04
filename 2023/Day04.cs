using AdventLibrary;

namespace _2023;
public class Day04 : IDay
{
    private readonly string[] _lines;
    private readonly List<Card> _cards;

    public Day04(InputReader reader)
    {
        _lines = reader.GetData("Day04.txt");
        _cards = ParseCards();
    }

    public int FirstPuzzle()
    {
        return _cards.Sum(x => x.Points);
    }

    public int SecondPuzzle()
    {
        Dictionary<int, int> copies = new();

        for (int i = 0; i < _cards.Count; i++)
        {
            if (!copies.TryAdd(_cards[i].Id, 1))
            {
                copies[_cards[i].Id] += 1;
            }

            for (int j = 0; j < _cards[i].Matches; j++)
            {
                for (int k = 0; k < copies[_cards[i].Id]; k++)
                {
                    if (!copies.TryAdd(_cards[i + j + 1].Id, 1))
                    {
                        copies[_cards[i + j + 1].Id] += 1;
                    }
                }
            }
        }

        return copies.Sum(x => x.Value);
    }

    private static List<int> ParseNumbers(string line)
    {
        List<int> output = new();

        string[] arr = line.Trim().Split(' ');

        foreach (string item in arr)
        {
            if (int.TryParse(item.Trim(), out int number))
            {
                output.Add(number);
            }
        }

        return output;
    }

    private List<Card> ParseCards()
    {
        List<Card> output = new();

        foreach (string line in _lines)
        {
            string[] halves = line.Split('|');
            string[] firstHalf = halves[0].Trim().Split(':');
            int id = int.Parse(firstHalf.First().Trim().Split(' ').Last());
            List<int> winningNumbers = ParseNumbers(firstHalf.Last());
            List<int> myNumbers = ParseNumbers(halves.Last());

            output.Add(new Card(id, winningNumbers, myNumbers));
        }

        return output;
    }
}

public record Card
{
    public int Id { get; init; }
    public List<int> WinningNumbers { get; init; }
    public List<int> MyNumbers { get; init; }
    public int Matches { get; init; }
    public int Points
    {
        get
        {
            int result = 0;

            for (int i = 0; i < Matches; i++)
            {
                if (i == 0)
                {
                    result = 1;
                    continue;
                }

                result *= 2;
            }

            return result;
        }
    }

    public Card(int id, List<int> winningNumbers, List<int> myNumbers)
    {
        Id = id;
        WinningNumbers = winningNumbers;
        MyNumbers = myNumbers;
        Matches = WinningNumbers.Intersect(MyNumbers).Count();
    }
}