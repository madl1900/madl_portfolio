using System.Diagnostics.Contracts;

public class Entry
{
    public Prompt _entryPrompt = new Prompt();
    public string _date = "";
    public string _entry = "";

    public string GetDate()
    {
        Console.Write("What is today's date (mm/dd/yyyy)? ");
        _date = Console.ReadLine();

        return _date;

    }
    public string GetEntry()
    {
        GetDate();

        Console.WriteLine();
        Console.WriteLine("Please write your entry after the prompt:");
        Console.WriteLine();
        Console.WriteLine(_entryPrompt.GetPrompt());
        _entry = Console.ReadLine();

        return _entry;
    }
}
