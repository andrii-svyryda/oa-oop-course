namespace Lecture08GofBehavioral.Demos;

public static class ObserverDemo
{
    class Product
    {
        public required string Name { get; init; }
        public required bool IsAvailable { get; init; }
    }

    interface IObserver
    {
        void Update(Product product);
    }

    interface ISubject
    {
        void Register(IObserver observer);
        void Remove(IObserver observer);
        void Notify(Product product);
    }

    class Shop : ISubject
    {
        private readonly List<IObserver> _observers = [];

        public void Register(IObserver observer) => _observers.Add(observer);

        public void Remove(IObserver observer) => _observers.Remove(observer);

        public void Notify(Product product)
        {
            foreach (var observer in _observers)
                observer.Update(product);
        }
    }

    class Customer : IObserver
    {
        private readonly string _name;

        public Customer(string name) => _name = name;

        public void Update(Product product) =>
            Console.WriteLine($"  {_name} got: {product.Name} available={product.IsAvailable}");
    }

    public static void Run()
    {
        Console.WriteLine("--- Observer ---");
        var shop = new Shop();
        var anna = new Customer("Anna");
        var bohdan = new Customer("Bohdan");
        shop.Register(anna);
        shop.Register(bohdan);
        shop.Notify(new Product { Name = "keyboard", IsAvailable = true });
        shop.Remove(bohdan);
        shop.Notify(new Product { Name = "keyboard", IsAvailable = false });
        Console.WriteLine("Subject talks to IObserver only. event in the language is lecture 09.");
    }
}
