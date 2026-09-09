namespace Lecture02Generics.Demos;

public static class BoxingCastDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Boxing and unboxing ---");

        int n = 42;
        object box = n;
        int copy = (int)box;
        Console.WriteLine($"int {n} → object → int {copy}");

        try
        {
            _ = (long)box;
        }
        catch (InvalidCastException)
        {
            Console.WriteLine("Unboxing int as long throws InvalidCastException.");
        }
    }
}
