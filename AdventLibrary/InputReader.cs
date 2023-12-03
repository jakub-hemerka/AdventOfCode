namespace AdventLibrary;
public class InputReader
{
    public string[] GetData(string fileName)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "Inputs", fileName);

        if (File.Exists(filePath))
        {
            return File.ReadAllLines(filePath);
        }

        return Array.Empty<string>();
    }
}