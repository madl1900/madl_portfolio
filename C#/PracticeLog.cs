public class PracticeLog
{
    private List<PracticeEntry> _entries = new List<PracticeEntry>();

    public void AddEntry(Instrument pracInstr, Music pracMusic, int duration)
    {
        PracticeEntry myPractice = new PracticeEntry(pracInstr, pracMusic, duration);
        _entries.Add(myPractice);
    }

    public void DisplayEntries()
    {
        Console.WriteLine();
        foreach(PracticeEntry entry in _entries)
        {
            Console.WriteLine(entry.DisplayPracticeInfo());
        }
    }

    public void SaveEntries()
    {
        Console.WriteLine();
        Console.Write("Enter the name you want your file saved as: ");
        string filename = Console.ReadLine();
        
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (PracticeEntry e in _entries)
        {
            outputFile.WriteLine(e.SavePracticeInfo());
        }
        }
        
        Console.WriteLine("Your file has been saved!");
    }

    public void LoadEntries()
    {
        Console.WriteLine();
        Console.WriteLine("*If file is missing valid values, they may be given default values*");

        Console.Write("Enter the name of the file you want loaded: ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            _entries.Clear();

            string[] lines = System.IO.File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string[] parts = line.Split("|");
                string[] practiceInfo = parts[0].Split(",");

                if (practiceInfo[0] == "PracticeEntry")
                {
                    DateTime date = DateTime.Parse(practiceInfo[1]);
                    string durationStr = practiceInfo[2];
                    bool durationIsInt = int.TryParse(durationStr, out int duration);

                    if (!durationIsInt)
                    {
                        duration = 30;
                    }

                    string[] instrInfo = parts[1].Split(",");
                    string instrumentName = instrInfo[1];
                    string instrumentType = instrInfo[2];

                    Instrument savedInstr = new Instrument(instrumentName, instrumentType);

                    string[] songInfo = parts[2].Split(",");
                    if (songInfo[0] == "MusicScale")
                    {
                        string scaleName = songInfo[1];
                        string scaleTempoStr = songInfo[2];
                        bool scaleTempoIsInt = int.TryParse(scaleTempoStr, out int scaleTempo);

                        if (!scaleTempoIsInt)
                        {
                            scaleTempo = 88;
                        }

                        string scaleTimeSig = songInfo[3];
                        string scaleRhythmStr = songInfo[4];
                        bool rhythmIsInt = int.TryParse(scaleRhythmStr, out int scaleRhythm);

                        if (!rhythmIsInt)
                        {
                            scaleRhythm = 1;
                        }

                        MusicScale savedScale = new MusicScale(scaleName, scaleTempo, scaleTimeSig, scaleRhythm);

                        PracticeEntry savedPractice = new PracticeEntry(savedInstr, savedScale, duration, date);
                        _entries.Add(savedPractice);
                    }
                    else if (songInfo[0] == "MusicPiece")
                    {
                        string songName = songInfo[1];
                        string songTempoStr = songInfo[2];
                        bool songTempoIsInt = int.TryParse(songTempoStr, out int songTempo);

                        if (!songTempoIsInt)
                        {
                            songTempo = 88;
                        }

                        string songTimeSig = songInfo[3];
                        string songComposer = songInfo[4];

                        string[] instr2Info = parts[1].Split(",");
                        string instrumentName2 = instrInfo[1];
                        string instrumentType2 = instrInfo[2];

                        Instrument savedInstr2 = new Instrument(instrumentName2, instrumentType2);

                        MusicPiece savedSong = new MusicPiece(songName, songTempo, songTimeSig, savedInstr, songComposer);

                        PracticeEntry savedPractice = new PracticeEntry(savedInstr, savedSong, duration, date);
                        _entries.Add(savedPractice);
                    }
                }
            }

            Console.WriteLine("Your file has been loaded!");

            if (_entries.Count() == 0)
            {
                Console.WriteLine("That file did not have any practice entries.");
            }
        }
        
        else
        {
            Console.WriteLine("That file does not exist.");
        }
    }
}