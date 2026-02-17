using System.IO;
using System.IO.Enumeration;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void SaveEntry()
    {
        Entry _myEntry = new Entry();
        _myEntry.GetEntry();
        _entries.Add(_myEntry);
    }

    public void DisplayJournal()
    {
        Console.Write("Would you like to display all entries or a specific date? (all/date) ");
        string response = Console.ReadLine();

        if (response == "all")
        {
            foreach (Entry e in _entries)
            {
                e.DisplayEntry();
            }
        }
        else if (response == "date")
        {   
            Console.Write("What date would you like to view (mm/dd/yyyy)? ");
            
            string userDate = Console.ReadLine();
            
            string[] parts = userDate.Split("/");
            int month = int.Parse(parts[0]);
            int day = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);

            DateTime searchDate = new DateTime(year, month, day);
            
            foreach (Entry e in _entries)
            {
                bool isMatch = searchDate.Date == e._date.Date;

                if (isMatch)
                {
                    e.DisplayEntry();
                }
            }
        }
        else
        {
            Console.WriteLine("That is not a valid option.");
        }    
    }

    public void SaveJournal()
    {
        Console.Write("Enter the name you want your file saved as: ");
        string filename = Console.ReadLine();
        
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry e in _entries)
        {
            outputFile.WriteLine($"{e._date}|{e._entryPrompt._prompt}|{e._entry}");
        }
        }
    }

    public List<Entry> LoadJournal()
    {
        Console.Write("Enter the name of the file you want loaded: ");
        string filename = Console.ReadLine();
        
        _entries.Clear();

        string[] lines = System.IO.File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            Entry loadEntry = new Entry();

            string[] parts = line.Split("|");
            loadEntry._date = DateTime.Parse(parts[0]);
            loadEntry._entryPrompt._prompt = parts[1];
            loadEntry._entry = parts[2];

            _entries.Add(loadEntry);
        }
        DisplayJournal();
        return _entries;
    }
}