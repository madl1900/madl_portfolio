public class Prompt
{
    public List<string> _prompts = new List<string>();
    public string _prompt = "";

    public void MakePrompts()
    {
        _prompts.Add("Who was the most interesting person I interacted with today?");
        _prompts.Add("What was the best part of my day?");
        _prompts.Add("How did I see the hand of the Lord in my life today?");
        _prompts.Add("If I had one thing I could do over today, what would it be?");
        _prompts.Add("What is one accomplishment I had from today?");
        _prompts.Add("What is something I did that I am proud of today?");
        _prompts.Add("What am I most looking forward to for tomorrow?");
    } 

    public string GetPrompt()
    {
        MakePrompts();

        Random randomGenerator = new Random();
        _prompt = _prompts[randomGenerator.Next(_prompts.Count)];
        return _prompt;
    }
}