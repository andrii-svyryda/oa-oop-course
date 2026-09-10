namespace Lecture11Collections.Demos;

public static class ChooseCollectionDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Choose by access ---");
        var names = new List<string> { "Ann", "Bohdan", "Ira" };
        Console.WriteLine($"  List index 1: {names[1]}");

        var phones = new Dictionary<string, string> { ["Ann"] = "111" };
        Console.WriteLine($"  Dictionary key Ann: {phones["Ann"]}");

        Console.WriteLine("List = order + index. Dictionary = find by key. Do not pick List by habit.");
    }
}
