namespace Lecture09BuiltinCsharpPatterns.Demos;

public static class EventObserverDemo
{
    class Shop
    {
        public event Action<string>? ProductArrived;

        public void Restock(string name) => ProductArrived?.Invoke(name);
    }

    public static void Run()
    {
        Console.WriteLine("--- event as Observer ---");
        var shop = new Shop();
        void Anna(string name) => Console.WriteLine($"  Anna: {name} is back");
        void Bohdan(string name) => Console.WriteLine($"  Bohdan: {name} is back");
        shop.ProductArrived += Anna;
        shop.ProductArrived += Bohdan;
        shop.Restock("keyboard");
        shop.ProductArrived -= Bohdan;
        shop.Restock("mouse");
        Console.WriteLine("event is Observer in the language. IObservable<T> is the same idea in the library.");
    }
}
