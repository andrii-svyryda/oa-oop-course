namespace Lecture02Generics.Demos;

public static class WhyGenericsDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Why generics: List<object> vs List<int> ---");

        var items = new List<object> { 10, "oops" };
        Console.WriteLine($"(int)items[0] = {(int)items[0]}");
        try
        {
            _ = (int)items[1];
        }
        catch (InvalidCastException)
        {
            Console.WriteLine("(int)items[1] where items[1] is string → InvalidCastException");
        }

        var numbers = new List<int> { 10, 20 };
        // numbers.Add("oops"); // does not compile
        Console.WriteLine($"List<int>: {string.Join(", ", numbers)} — string would not compile");
    }
}
