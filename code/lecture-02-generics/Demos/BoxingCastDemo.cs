namespace Lecture02Generics.Demos;

public static class BoxingCastDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Value type, boxing, cast ---");

        int n = 42;
        object box = n;
        int copy = (int)box;
        Console.WriteLine($"boxed int: {box}, unboxed: {copy}");

        try
        {
            _ = (long)box;
        }
        catch (InvalidCastException)
        {
            Console.WriteLine("Unboxing int as long throws InvalidCastException.");
        }

        var items = new List<object> { 10, "oops" };
        Console.WriteLine($"(int)items[0] = {(int)items[0]}");

        try
        {
            _ = (int)items[1]!;
        }
        catch (InvalidCastException)
        {
            Console.WriteLine("Casting string to int throws InvalidCastException.");
        }

        var numbers = new List<int> { 10, 20 };
        Console.WriteLine($"List<int> keeps ints without boxing: {string.Join(", ", numbers)}");
    }
}
