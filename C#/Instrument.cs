public class Instrument
{
    private string _name;
    private string _type;
    
    public Instrument(string name, string type)
    {
        _name = name;
        _type = type;
    }

    public string DisplayInstrument()
    {
        return $"Instrument: {_name} Type: {_type}";
    }

    public string SaveInstrument()
    {
        return $"Instrument,{_name},{_type}";
    }

    public string GetInstrName()
    {
        return _name;
    }

    public string GetInstrType()
    {
        return _type;
    }
}