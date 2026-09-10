namespace Lecture09BuiltinCsharpPatterns.Demos;

public static class ActivatorFactoryDemo
{
    public class ScoreCard
    {
        public int Score { get; set; }
    }

    public static void Run()
    {
        Console.WriteLine("--- Activator as factory ---");
        var created = (ScoreCard)Activator.CreateInstance(typeof(ScoreCard))!;
        created.Score = 10;
        Console.WriteLine($"  created {created.GetType().Name} with score={created.Score}");
        Console.WriteLine("CreateInstance builds an object from a type. That is a factory, not a new pattern.");
    }
}
