using System;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        string mainMenuChoice = "";

        PracticeLog myPractices = new PracticeLog();
        MusicManager myMusic = new MusicManager();

        while (mainMenuChoice != "4")
        {
            Console.Clear();
            Console.WriteLine("Welcome to the practice room!");
        
            Console.WriteLine();
            Console.WriteLine("Please choose an option from the menu:");
            Console.WriteLine("1. Practice Log\n2. Music manager\n3. Metronome\n4. Quit");
            mainMenuChoice = Console.ReadLine();

            if (mainMenuChoice == "1")
            {
                string practiceMenuChoice = "";

                while (practiceMenuChoice != "5")
                {
                    Console.WriteLine();
                    Console.WriteLine("What would you like to do in the practice log?");
                    
                    Console.WriteLine();
                    Console.WriteLine("1. Create new practice entry\n2. Display current entries\n3. Save entries to file\n4. Load a file\n5. Exit Practice Log");
                    practiceMenuChoice = Console.ReadLine();

                    if (practiceMenuChoice == "1")
                    {
                        Console.WriteLine();
                        Console.WriteLine("1. Practice a random song\n2. Enter entry manually");
                        Console.Write("What would you like to do? ");
                        string entryChoice = Console.ReadLine();

                        if (entryChoice == "1")
                        {
                            Music randomSong = myMusic.GetRandomMusic();
                            Instrument songInstr;

                            if (randomSong is MusicPiece song)
                            {
                                songInstr = song.GetMusicInstr();
                            }
                            
                            else
                            {
                                songInstr = myMusic.GetRandomInstr();
                            }

                            Console.WriteLine();
                            Console.WriteLine("You will be practicing:");
                            Console.WriteLine(randomSong.DisplayInfo());
                            Console.WriteLine(songInstr.DisplayInstrument());

                            bool durationIsInt = false;
                            int practiceDuration = 0;
                                    
                            while (!durationIsInt)
                            {
                                Console.WriteLine();
                                Console.Write("How long are you practicing for? (in minutes) ");
                                string practiceDurationStr = Console.ReadLine();
                                durationIsInt = int.TryParse(practiceDurationStr, out practiceDuration);

                                if (!durationIsInt)
                                {
                                    Console.WriteLine("You must type a whole number.");
                                }
                            }
                            
                            myPractices.AddEntry(songInstr, randomSong, practiceDuration);                            
                        }

                        else if (entryChoice == "2")
                        {
                            Instrument practiceInstr;

                            if (myMusic.GetInstrListLength() != 0 && myMusic.GetMusicListLength() != 0)
                            {
                                bool songIsInt = false;
                                int practiceSongIndex = 0;

                                while (!songIsInt)
                                {
                                    myMusic.DisplayMusic();
                                    Console.Write("What song are you practicing? ");
                                    string practiceSongStr = Console.ReadLine();
                                    songIsInt = int.TryParse(practiceSongStr, out practiceSongIndex);

                                    if (!songIsInt)
                                    {
                                        Console.WriteLine("You must type a whole number.");
                                    }
                                }                                

                                Music practiceSong = myMusic.GetMusic(practiceSongIndex);

                                if (practiceSong is MusicPiece song)
                                {
                                    practiceInstr = song.GetMusicInstr();
                                }

                                else
                                {
                                    bool instrIsInt = false;
                                    int practiceInstrIndex = 0;

                                    while (!instrIsInt)
                                    {
                                        myMusic.DisplayInstruments();
                                        Console.Write("What instrument are you practicing? ");
                                        string practiceInstrStr = Console.ReadLine();
                                        instrIsInt = int.TryParse(practiceInstrStr, out practiceInstrIndex);

                                        if (!instrIsInt)
                                        {
                                            Console.WriteLine("You must type a whole number.");
                                        }
                                    }
                                    
                                    practiceInstr = myMusic.GetInstrument(practiceInstrIndex);
                                }

                                bool durationIsInt = false;
                                int practiceDuration = 0;

                                while (!durationIsInt)
                                {
                                    Console.Write("How long are you practicing for? (in minutes) ");
                                    string practiceDurationStr = Console.ReadLine();
                                    durationIsInt = int.TryParse(practiceDurationStr, out practiceDuration);

                                    if (!durationIsInt)
                                    {
                                        Console.WriteLine("You must type a whole number.");
                                    }
                                }

                                myPractices.AddEntry(practiceInstr, practiceSong, practiceDuration);
                            }

                            else
                            {
                                Console.WriteLine("You must enter at least one instrument and song into the music manager first.");
                            }
                        }
                                
                        else
                        {
                            Console.WriteLine("You must choose an option from the menu.");
                        }
                    }

                    else if (practiceMenuChoice == "2")
                    {
                        myPractices.DisplayEntries();
                    }

                    else if (practiceMenuChoice == "3")
                    {
                        myPractices.SaveEntries();
                    }

                    else if (practiceMenuChoice == "4")
                    {
                        myPractices.LoadEntries();
                    }

                    else if (practiceMenuChoice == "5")
                    {
                        break;
                    }

                    else
                    {
                        Console.WriteLine("You must choose an option from the menu.");
                    }
                }
            }

            else if (mainMenuChoice == "2")
            {
                string musicMenuChoice = "";

                while (musicMenuChoice != "7")
                {
                    Console.WriteLine();
                    Console.WriteLine("What would you like to do in the music manager?");

                    Console.WriteLine();
                    Console.WriteLine("1. Add new music\n2. Display all music\n3. Add an instrument\n4. Display all instruments\n5. Save music to file\n6. Load a file\n7. Exit Music Manager");
                    musicMenuChoice = Console.ReadLine();

                    if (musicMenuChoice == "1")
                    {
                        myMusic.AddMusic();
                    }
                    else if (musicMenuChoice == "2")
                    {
                        myMusic.DisplayMusic();
                    }
                    else if (musicMenuChoice == "3")
                    {
                        myMusic.AddInstrument();
                    }
                    else if (musicMenuChoice == "4")
                    {
                        myMusic.DisplayInstruments();
                    }
                    else if (musicMenuChoice == "5")
                    {
                        myMusic.SaveMusicFile();
                    }
                    else if (musicMenuChoice == "6")
                    {
                        myMusic.LoadMusicFile();
                    }
                    else if (musicMenuChoice == "7")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You must choose an option from the menu.");
                    }
                }
            }

            else if (mainMenuChoice == "3")
            {
                string metronomeChoice = "";
                int tempo = 0;
                string timeSignature = "";

                while (metronomeChoice != "4")
                {
                    Console.WriteLine();
                    Console.WriteLine("Welcome to the metronome!");

                    Console.WriteLine();
                    Console.WriteLine("1. Choose a song\n2. Get random song\n3. Enter tempo/rhythm manually\n4. Exit metronome");
                    Console.Write("How would you like to use the metronome? ");
                    metronomeChoice = Console.ReadLine();

                    if (metronomeChoice == "1")
                    {
                        if (myMusic.GetInstrListLength() != 0 && myMusic.GetMusicListLength() != 0)
                        {
                            bool songIsInt = false;
                            int songIndex = 0;

                            while (!songIsInt)
                            {
                                myMusic.DisplayMusic();
                                Console.Write("What song would you like to use? ");
                                string SongStr = Console.ReadLine();
                                songIsInt = int.TryParse(SongStr, out songIndex);

                                if (!songIsInt)
                                {
                                    Console.WriteLine("You must type a whole number.");
                                }
                            }                                

                            Music metronomeSong = myMusic.GetMusic(songIndex);
                            tempo = metronomeSong.GetTempo();
                            timeSignature = metronomeSong.GetTimeSignature();
                        }

                        else
                        {
                            Console.WriteLine("You must enter at least one song into the music manager first.");
                            Thread.Sleep(3000);
                            break;
                        }
                    }

                    else if (metronomeChoice == "2")
                    {

                        Music randomSong = myMusic.GetRandomMusic();
                        Console.WriteLine();
                        Console.WriteLine("The metronome will use:");
                        Console.WriteLine(randomSong.DisplayInfo());

                        tempo = randomSong.GetTempo();
                        timeSignature = randomSong.GetTimeSignature();

                    }

                    else if (metronomeChoice == "3")
                    {
                        Console.WriteLine();
                        bool tempoIsInt = false;

                        while (!tempoIsInt)
                        {
                            Console.Write("What tempo would you like the metronome to be? ");
                            string tempoString = Console.ReadLine();
                            tempoIsInt = int.TryParse(tempoString, out tempo);

                            if (!tempoIsInt)
                            {
                                Console.WriteLine("You must type a whole number.");
                            }
                        }

                        Console.WriteLine();
                        Console.Write("What time signature would you like? (ex: 4/4, 6/8) ");
                        timeSignature = Console.ReadLine();
                        Console.WriteLine("*If time signature input was not valid, metronome will default to 4/4*");
                    }

                    else if (metronomeChoice == "4")
                    {
                        break;
                    }

                    else
                    {
                        Console.WriteLine("You must choose an option from the menu.");
                    }

                    Console.WriteLine();

                    bool durationIsInt = false;
                    int duration = 0;

                    while (!durationIsInt)
                    {
                        Console.Write("How many seconds would you like the metronome to run? ");
                        string durationString = Console.ReadLine();
                        durationIsInt = int.TryParse(durationString, out duration);

                        if (!durationIsInt)
                        {
                            Console.WriteLine("You must type a whole number.");
                        }
                    }                

                    Console.WriteLine();
                    Console.WriteLine($"The metronome tempo will be {tempo} and the time signature is {timeSignature}.");
                    Thread.Sleep(3000);                    
                    
                    Metronome myMetronome = new Metronome(tempo, timeSignature, duration);

                    myMetronome.Run();
                }
            }
            else if (mainMenuChoice == "4")
            {
                break;
            }

            else
            {
                Console.WriteLine("You must choose an option from the menu.");
            }
        }
    }
}