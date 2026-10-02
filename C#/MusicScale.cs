public class MusicScale : Music
{
    private int _rhythm;

    public MusicScale(string name, int tempo, string timeSignature, int rhythm) : base(name, tempo, timeSignature)
    {
        _rhythm = rhythm;
    }

    public override string DisplayInfo()
    {
        return $"Scale: {_name}, Rhythm: {_rhythm}";
    }

    public override string SaveInfo()
    {
        return $"MusicScale,{_name},{_tempo},{_timeSignature},{_rhythm}";
    }
}