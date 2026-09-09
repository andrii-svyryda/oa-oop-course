namespace Lecture02Generics.Demos;

public static class CastDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Cast: implicit, explicit, InvalidCastException ---");

        long wide = 42;
        int narrow = (int)wide;
        Console.WriteLine($"implicit int→long, explicit long→int: {wide} / {narrow}");

        object box = 10;
        int n = (int)box;
        Console.WriteLine($"(int)object that holds 10 = {n}");

        try
        {
            _ = (string)box;
        }
        catch (InvalidCastException)
        {
            Console.WriteLine("(string)object that holds int → InvalidCastException");
        }
    }
}
