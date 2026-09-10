using System.Text;

namespace Lecture09BuiltinCsharpPatterns.Demos;

public static class StringBuilderDemo
{
    public static void Run()
    {
        Console.WriteLine("--- StringBuilder ---");
        var sql = new StringBuilder()
            .Append("SELECT * FROM Users")
            .Append(" ORDER BY Name")
            .Append(" LIMIT 10");
        Console.WriteLine($"  {sql}");
        Console.WriteLine("Same idea as lecture 06 Builder: steps, then the finished text.");
    }
}
