namespace Lecture09BuiltinCsharpPatterns.Demos;

public static class IteratorYieldDemo
{
    static IEnumerable<int> Odds(int max)
    {
        for (var i = 1; i <= max; i += 2)
            yield return i;
    }

    public static void Run()
    {
        Console.WriteLine("--- Iterator / yield ---");
        Console.WriteLine($"  {string.Join(", ", Odds(7))}");
        Console.WriteLine("foreach walks IEnumerable. yield builds the iterator for you.");
    }
}
