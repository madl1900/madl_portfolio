public class Prompt
{
    public List<string> _prompts = new List<string>();
    public string _prompt = ""; 

    public string GetPrompt()
    {
        Random randomGenerator = new Random();
        _prompt = _prompts[randomGenerator.Next(_prompts.Count)];
        return _prompt;
    }
}