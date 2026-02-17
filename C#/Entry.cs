using System.Diagnostics.Contracts;

public class Entry
{
    public Prompt _entryPrompt = new Prompt();
    public DateTime _date = DateTime.Now;
    public string _entry = "";

    public void MakePrompts(Prompt p)
    {
        p._prompts.Add("Who was the most interesting person I interacted with today?");
        p._prompts.Add("What was the best part of my day?");
        p._prompts.Add("How did I see the hand of the Lord in my life today?");
        p._prompts.Add("If I had one thing I could do over today, what would it be?");
        p._prompts.Add("What is one accomplishment I had from today?");
        p._prompts.Add("What is something I did that I am proud of today?");
        p._prompts.Add("What am I most looking forward to for tomorrow?");
    }
    public void DisplayDate()
    {
        Console.WriteLine($"Date: {_date}");
    }
    public string GetEntry()
    {
        DisplayDate();
        MakePrompts(_entryPrompt);
        Console.WriteLine();
        Console.WriteLine("Please write your entry after the prompt:");
        Console.WriteLine();
        Console.WriteLine(_entryPrompt.GetPrompt());
        _entry = Console.ReadLine();

        return _entry;
    }

    public void DisplayEntry()
    {
        Console.WriteLine($"\nDate: {_date}\nPrompt: {_entryPrompt._prompt}\nResponse: {_entry}");
    }
}