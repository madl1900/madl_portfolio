using System.Dynamic;

public class MusicManager
{
    private List<Music> _music = new List<Music>();
    private List<Instrument> _instruments = new List<Instrument>();

    public void AddMusic()
    {
        Console.WriteLine();
        Console.WriteLine("1. Song\n2. Scale");
        Console.Write("What type of music would you like to add? ");
        string musicChoice = Console.ReadLine();

        if (musicChoice == "1")
        {
            if (_instruments.Count() == 0)
            {
                Console.WriteLine();
                Console.WriteLine("You must add an instrument first:");
                AddInstrument();
            }
            
            bool instrIsInt = false;
            int instrChoiceIndex = 0;

            while (!instrIsInt)
            {
                DisplayInstruments();
                Console.Write("What instrument is this song for? ");
                string InstrChoice = Console.ReadLine();
                instrIsInt = int.TryParse(InstrChoice, out instrChoiceIndex);

                if (!instrIsInt)
                {
                    Console.WriteLine("You must type a whole number.");
                }
            }

            Instrument songInstr = GetInstrument(instrChoiceIndex);

            Console.WriteLine();
            Console.Write("What is the name of the song? ");
            string name = Console.ReadLine();

            Console.Write("Who is the composer? ");
            string composer = Console.ReadLine();

            bool tempoIsInt = false;
            int tempo = 0;

            while (!tempoIsInt)
            {
                Console.Write("What is the song's tempo? ");
                string tempoStr = Console.ReadLine();
                tempoIsInt = int.TryParse(tempoStr, out tempo);

                if (!tempoIsInt)
                {
                    Console.WriteLine("You must type a whole number.");
                }
            }

            Console.Write("What is the time signature? ");
            string timeSignature = Console.ReadLine();

            MusicPiece mySong = new MusicPiece(name, tempo, timeSignature, songInstr, composer);
            _music.Add(mySong);
        }

        else if (musicChoice == "2")
        {
            Console.WriteLine();
            Console.Write("What is the name of the scale? ");
            string name = Console.ReadLine();

            bool tempoIsInt = false;
            int tempo = 0;

            while (!tempoIsInt)
            {
                Console.Write("What tempo will the scale be? ");
                string tempoStr = Console.ReadLine();
                tempoIsInt = int.TryParse(tempoStr, out tempo);

                if (!tempoIsInt)
                {
                    Console.WriteLine("You must type a whole number.");
                }
            }            

            Console.Write("What is the time signature? ");
            string timeSignature = Console.ReadLine();

            bool rhythmIsInt = false;
            int rhythm = 0;

            while (!rhythmIsInt)
            {
                Console.Write("How many beats will each note get? ");
                string rhythmStr = Console.ReadLine();
                rhythmIsInt = int.TryParse(rhythmStr, out rhythm);

                if (!rhythmIsInt)
                {
                    Console.WriteLine("You must type a whole number.");
                }
            }            

            MusicScale myScale = new MusicScale(name, tempo, timeSignature, rhythm);
            _music.Add(myScale);
        }
    }
    public void DisplayMusic()
    {
        Console.WriteLine();

        int i = 0;
        foreach (Music m in _music)
        {
            i++;
            Console.WriteLine($"{i}. {m.DisplayInfo()}");
        }
    }

    public Music GetRandomMusic()
    {
        if (_music.Count == 0)
        {
            Console.WriteLine("You don't have any music yet, please add one:");
            AddMusic();
        }
        
        Random randomSong = new Random();

        Music song = _music[randomSong.Next(_music.Count)];

        return song;
    }

    public void SaveMusicFile()
    {
        Console.WriteLine();
        Console.WriteLine("*Any instruments that are not connected to a song will be lost*");
        Console.WriteLine();
        Console.Write("Enter the name you want your file saved as: ");
        string filename = Console.ReadLine();
        
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Music m in _music)
        {
            outputFile.WriteLine(m.SaveInfo());
        }
        }

        Console.WriteLine("Your file has been saved!");
    }

    public void LoadMusicFile()
    {
        Console.WriteLine();
        Console.WriteLine("*If file is missing valid values, they may be given default values*");        
        Console.Write("Enter the name of the file you want loaded: ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            _instruments.Clear();
            _music.Clear();

            string[] lines = System.IO.File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string[] parts = line.Split("|");
                string[] musicInfo = parts[0].Split(",");

                if (musicInfo[0] == "MusicScale")
                {
                    string scaleName = musicInfo[1];
                    
                    string tempoStr = musicInfo[2];
                    bool tempoIsInt = int.TryParse(tempoStr, out int scaleTempo);

                    if (!tempoIsInt)
                    {
                        scaleTempo = 88;
                    }

                    string scaleTimeSig = musicInfo[3];
                
                    string rhythmStr = musicInfo[4];
                    bool rhythmIsInt = int.TryParse(rhythmStr, out int scaleRhythm);

                    if (!rhythmIsInt)
                    {
                        scaleRhythm = 1;
                    }

                    MusicScale savedScale = new MusicScale(scaleName, scaleTempo, scaleTimeSig, scaleRhythm);
                    _music.Add(savedScale);
                }

                else if (musicInfo[0] == "MusicPiece")
                {
                    string songName = musicInfo[1];

                    string tempoStr = musicInfo[2];
                    bool tempoIsInt = int.TryParse(tempoStr, out int songTempo);

                    if (!tempoIsInt)
                    {
                        songTempo = 88;
                    }

                    string songTimeSig = musicInfo[3];
                    string songComposer = musicInfo[4];

                    string[] instrInfo = parts[1].Split(",");
                    string instrumentName = instrInfo[1];
                    string instrumentType = instrInfo[2];

                    Instrument savedInstr = new Instrument(instrumentName, instrumentType);
                    bool instrExists = false;
                    foreach (Instrument i in _instruments)
                    {
                        if(savedInstr.GetInstrName == i.GetInstrName && savedInstr.GetInstrType == i.GetInstrType)
                        {
                            instrExists = true;
                            savedInstr = i;
                            break;
                        }
                    }

                    if (!instrExists)
                    {
                        _instruments.Add(savedInstr);
                    }

                    MusicPiece savedSong = new MusicPiece(songName, songTempo, songTimeSig, savedInstr, songComposer);
                    _music.Add(savedSong);
                }
            }

            Console.WriteLine("Your file has been loaded!");

            if (_music.Count() == 0)
            {
                Console.WriteLine("That file did not have any music");
            }
        }

        else
        {
            Console.WriteLine("That file does not exist.");
        }
    }

    public void AddInstrument()
    {
        Console.WriteLine();
        Console.WriteLine("What instrument would you like to add? ");
        string instr = Console.ReadLine().ToLower();

        Console.WriteLine("What type of instrument is it? (ex: woodwind) ");
        string type = Console.ReadLine().ToLower();

        Instrument myInstr = new Instrument(instr, type);
        _instruments.Add(myInstr);
    }

    public void DisplayInstruments()
    {
        Console.WriteLine();

        int i = 0;

        foreach (Instrument instr in _instruments)
        {
            i++;
            Console.WriteLine($"{i}. {instr.DisplayInstrument()}");
        }
    }

    public Instrument GetInstrument(int i)
    {
        if (i <= _instruments.Count)
        {
            return _instruments[i-1];
        }
        else
        {
            Console.WriteLine("You do not have that many instruments. The first instrument was chosen instead.");
            return _instruments[0];
        }
    }

    public Music GetMusic(int i)
    {
        if (i <= _music.Count)
        {
            return _music[i-1];    
        }
        else
        {
            Console.WriteLine("You do not have that many songs. The first song was chosen instead.");
            return _music[0];
        }
        
    }

    public int GetMusicListLength()
    {
        return _music.Count();
    }

    public int GetInstrListLength()
    {
        return _instruments.Count();
    }

    public Instrument GetRandomInstr()
    {
        if (_instruments.Count == 0)
        {
            Console.WriteLine("You don't have any instruments yet, please add one:");
            AddInstrument();
        }
        
        Random randomGen = new Random();

        Instrument instr = _instruments[randomGen.Next(_instruments.Count)];

        return instr;
    }
}