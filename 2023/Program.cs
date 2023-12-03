using _2023;
using AdventLibrary;

InputReader reader = new();
IDay day = new Day03(reader);

int result = day.SecondPuzzle();

Console.WriteLine(result);
Console.ReadLine();