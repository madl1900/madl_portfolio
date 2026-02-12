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
                Console.WriteLine($"\nDate: {e._date}\nPrompt: {e._entryPrompt._prompt}\nResponse: {e._entry}");
            }
        }
        else if (response == "date")
        {   
            Console.Write("What date would you like to view (mm/dd/yyyy)? ");
            string searchDate = Console.ReadLine();
            
            foreach (Entry e in _entries)
            {
                if (e._date == searchDate)
                {
                    Console.WriteLine($"\nDate: {e._date}\nPrompt: {e._entryPrompt._prompt}\nResponse: {e._entry}");
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
            loadEntry._date = parts[0];
            loadEntry._entryPrompt._prompt = parts[1];
            loadEntry._entry = parts[2];

            _entries.Add(loadEntry);
        }
        DisplayJournal();
        return _entries;
    }

    public void ShowMenu()
    {
        string menuChoice = "";
        while (menuChoice != "5")
        {   
            Console.WriteLine();
            Console.WriteLine("Select an option from the menu: ");
            Console.WriteLine("1. Write a journal entry");
            Console.WriteLine("2. Display current journal entries");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load a previous journal file");
            Console.WriteLine("5. Exit the journal");
            menuChoice = Console.ReadLine();
            switch (menuChoice)
            {
                case "1":
                    SaveEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournal();
                    break;
                case "4":
                    LoadJournal();
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Please enter a number from the list");
                    break;
            }
        }
    }
}