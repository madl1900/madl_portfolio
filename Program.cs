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

        myJournal.ShowMenu();
    }
}