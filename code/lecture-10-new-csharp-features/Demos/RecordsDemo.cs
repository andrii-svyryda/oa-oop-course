namespace Lecture10NewCsharpFeatures.Demos;

public static class RecordsDemo
{
    public record Person(string Name, int Age);

    public static void Run()
    {
        Console.WriteLine("--- record ---");
        var a = new Person("Ann", 20);
        var b = a with { Age = 21 };
        Console.WriteLine($"  a={a}");
        Console.WriteLine($"  b={b}");
        Console.WriteLine($"  a == new Person(\"Ann\", 20): {a == new Person("Ann", 20)}");
        Console.WriteLine("Records compare by values. with makes a copy with one field changed.");
    }
}
