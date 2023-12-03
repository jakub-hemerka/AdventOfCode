using AdventLibrary;

namespace _2023;
public class Day02 : IDay
{
    private readonly string[] _lines;
    private readonly List<Game> _games;

    public Day02(InputReader reader)
    {
        _lines = reader.GetData("Day02.txt");
        _games = ParseFile();
    }

    public int FirstPuzzle()
    {
        int output = 0;

        foreach (Game game in _games)
        {
            if (game.IsPossible)
            {
                output += game.Id;
            }
        }

        return output;
    }

    public int SecondPuzzle()
    {
        int output = 0;

        foreach (Game game in _games)
        {
            output += (game.MinimumRed * game.MinimumGreen * game.MinimumBlue);
        }

        return output;
    }

    private List<Game> ParseFile()
    {
        List<Game> games = new();

        foreach (string line in _lines)
        {
            games.Add(ParseGame(line));
        }

        return games;
    }

    private static Game ParseGame(string line)
    {
        string[] blocks = line.Split(':');
        int id = int.Parse(blocks[0].Split(' ').Last());
        string[] textShowings = blocks[1].Split(';');
        List<Showing> showings = new();

        foreach (string textShowing in textShowings)
        {
            showings.Add(ParseShowing(textShowing));
        }

        return new Game(id, showings);
    }

    private static Showing ParseShowing(string line)
    {
        string[] values = line.Split(',');
        int red = 0, green = 0, blue = 0;

        foreach (string val in values)
        {
            string[] items = val.Trim().Split(' ');

            switch (items[1].Trim().ToLower())
            {
                case "red":
                    red = int.Parse(items[0].Trim());
                    break;
                case "green":
                    green = int.Parse(items[0].Trim());
                    break;
                case "blue":
                    blue = int.Parse(items[0].Trim());
                    break;
                default:
                    break;
            }
        }

        return new Showing(red, green, blue);
    }
}

public record Game
{
    public int Id { get; init; }
    public List<Showing> Showings { get; init; }
    public int MinimumRed => Showings.Max(x => x.Red);
    public int MinimumGreen => Showings.Max(x => x.Green);
    public int MinimumBlue => Showings.Max(x => x.Blue);
    public bool IsPossible => Showings.All(x => x.IsPossible);

    public Game(int id, List<Showing> showings)
    {
        Id = id;
        Showings = showings;
    }
}

public record Showing
{
    public int Red { get; init; }
    public int Green { get; init; }
    public int Blue { get; init; }
    public bool IsPossible => Red <= 12 && Green <= 13 && Blue <= 14;

    public Showing(int red, int green, int blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }
}