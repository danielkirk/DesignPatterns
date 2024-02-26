using System.Diagnostics;

public class Journal
{
    private readonly List<string> entries = new List<string>();

    private static int count = 0;
    public int AddEntry(string text)
    {
        entries.Add($"{++count}: {text}");
        return count;
    }

    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }
}

public class Persistance
{
    public void SaveToFile(Journal j, string fileName, bool overwrite = false)
    {
        if(overwrite || !File.Exists(fileName))
            File.WriteAllText(fileName, j.ToString());
    }
}

public class Demo
{
    static void Main(string[] args)
    {
        var j = new Journal();
        j.AddEntry("I cried and ate a bug");
        j.AddEntry("It was actually okay tbh");
        Console.WriteLine(j);

        var p = new Persistance();
        var filename = @"c:\temp\journal.txt";
        p.SaveToFile(j, filename, true);

        Process.Start(new ProcessStartInfo { FileName = filename, UseShellExecute = true });
    }
}