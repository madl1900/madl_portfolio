using System;
/* I exceeded the requirements for this assignment by 
adding a feature in the DisplayJournal method of the 
Journal class that asks the user whether they want to
display all entries or only the ones from a certain date.
if they choose date, it then uses an if statement to only
display the entries that match the chosen date.
*/
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the journal app!");

        Journal myJournal = new Journal();

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
                    myJournal.SaveEntry();
                    break;
                case "2":
                    myJournal.DisplayJournal();
                    break;
                case "3":
                    myJournal.SaveJournal();
                    break;
                case "4":
                    myJournal.LoadJournal();
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