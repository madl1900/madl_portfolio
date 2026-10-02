public class PracticeEntry
{
    private Instrument _instrument;
    private Music _song;
    private int _duration;
    private DateTime _date = new DateTime();

    public PracticeEntry(Instrument instrument, Music song, int duration)
    {
        _instrument = instrument;
        _song = song;
        _duration = duration;
        _date = DateTime.Now;
    }

    public PracticeEntry(Instrument instrument, Music song, int duration, DateTime date)
    {
        _instrument = instrument;
        _song = song;
        _duration = duration;
        _date = date;
    }

    public string DisplayPracticeInfo()
    {
        return $"Date: {_date}, Duration: {_duration} minutes, {_song.DisplayInfo()}, {_instrument.DisplayInstrument()}";
    }

    public string SavePracticeInfo()
    {
        return $"PracticeEntry,{_date},{_duration}|{_instrument.SaveInstrument()}|{_song.SaveInfo()}";
    }
}