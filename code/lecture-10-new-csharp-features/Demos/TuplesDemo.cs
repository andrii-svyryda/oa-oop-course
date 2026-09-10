namespace Lecture10NewCsharpFeatures.Demos;

public static class TuplesDemo
{
    static (string name, decimal price) Offer(string sku) => (sku, 19.99m);

    public static void Run()
    {
        Console.WriteLine("--- Tuples ---");
        var (name, price) = Offer("ABC");
        var t = (Title: "OOP", Hours: 16);
        Console.WriteLine($"  {name} costs {price}");
        Console.WriteLine($"  {t.Title}: {t.Hours} h");
        Console.WriteLine("Two values back from a method — no extra DTO class.");
    }
}
