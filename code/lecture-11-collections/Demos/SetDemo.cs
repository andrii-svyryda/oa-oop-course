namespace Lecture11Collections.Demos;

public static class SetDemo
{
    public static void Run()
    {
        Console.WriteLine("--- HashSet / SortedSet ---");
        var tags = new HashSet<string> { "oop", "csharp", "oop" };
        Console.WriteLine($"  HashSet unique: {string.Join(", ", tags)}");

        var a = new HashSet<int> { 1, 2, 3 };
        var b = new HashSet<int> { 3, 4 };
        a.IntersectWith(b);
        Console.WriteLine($"  intersect: {string.Join(", ", a)}");

        var sorted = new SortedSet<int> { 30, 10, 20 };
        Console.WriteLine($"  SortedSet: {string.Join(", ", sorted)}");
        Console.WriteLine("HashSet = unique, no order. SortedSet = unique and always sorted.");
    }
}
