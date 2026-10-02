public class MusicPiece : Music
{
    private Instrument _instrument;
    private string _composer;

    public MusicPiece(string name, int tempo, string timeSignature, Instrument instrument, string composer) : base(name, tempo, timeSignature)
    {
        _instrument = instrument;
        _composer = composer;
    }

    public override string DisplayInfo()
    {
        return $"Song: {_name}, Composer: {_composer}";
    }

    public override string SaveInfo()
    {
        return $"MusicPiece,{_name},{_tempo},{_timeSignature},{_composer}|{_instrument.SaveInstrument()}";
    }

    public Instrument GetMusicInstr()
    {
        return _instrument;
    }
}