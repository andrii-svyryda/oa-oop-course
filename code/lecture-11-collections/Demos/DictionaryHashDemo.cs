namespace Lecture11Collections.Demos;

public static class DictionaryHashDemo
{
    static int ToyHash(string s, int buckets)
    {
        var total = 0;
        foreach (var c in s)
            total += c;
        return total % buckets;
    }

    public static void Run()
    {
        Console.WriteLine("--- Dictionary / hash ---");
        Console.WriteLine($"  toy hash of Hello: {ToyHash("Hello", 50)}");
        Console.WriteLine($"  toy hash of Hello!: {ToyHash("Hello!", 50)}");

        var ages = new Dictionary<string, int> { ["Ann"] = 20, ["Ira"] = 21 };
        Console.WriteLine($"  ages[Ann]={ages["Ann"]}");
        Console.WriteLine("Real Dictionary uses GetHashCode + Equals. Those two must stay in sync.");
    }
}
