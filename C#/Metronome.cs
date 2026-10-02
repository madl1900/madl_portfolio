public class Metronome
{
    private int _tempo;
    private int _duration;
    private string _timeSignature;
    private DateTime _endTime;
    
    public Metronome(int tempo, string timeSignature, int duration)
    {
        _tempo = tempo;
        _timeSignature = timeSignature;
        _duration = duration;
    }

    public int ConvertTempo()
    {
        if (_tempo == 0)
        {
            _tempo = 88;            
        }

        return 60000/88;
    }

    public void SetEndTime()
    {
        DateTime startTime = DateTime.Now;
        _endTime = startTime.AddSeconds(_duration);
    }
    public void Run()
    {        
        string[] timeSigParts = _timeSignature.Split("/");
        int topTime;

        if (timeSigParts.Count() < 2)
        {
            topTime = 4;
        }

        else
        {
            string topTimeStr = timeSigParts[0];
            bool topIsInt = int.TryParse(topTimeStr, out topTime);

            if (!topIsInt | topTime == 0)
            {
                topTime = 4;
            }
        }

        int convertedTempo = ConvertTempo();

        SetEndTime();
        while (DateTime.Now < _endTime)
        {
            Console.Clear();
            Console.WriteLine("\\ /");
            Console.WriteLine("-o-");
            Console.WriteLine("/ \\");
            Thread.Sleep(convertedTempo);
            
            for (int i = 1; i < topTime; i++)
            {
                Console.WriteLine(" o");
                Thread.Sleep(convertedTempo);
            }
        }
    }
}