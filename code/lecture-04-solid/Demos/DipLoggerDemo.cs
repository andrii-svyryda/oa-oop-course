namespace Lecture04Solid.Demos;

public static class DipLoggerDemo
{
    interface IAppLogger
    {
        void Trace(string message);
        void Information(string message);
        void Log(string message);
        void Exception(string message);
    }

    class ConsoleAppLogger : IAppLogger
    {
        public void Trace(string message) => Console.WriteLine($"TRACE {message}");
        public void Information(string message) => Console.WriteLine($"INFO  {message}");
        public void Log(string message) => Console.WriteLine($"LOG   {message}");
        public void Exception(string message) => Console.WriteLine($"EX    {message}");
    }

    class Checkout
    {
        private readonly IAppLogger _logger;
        public Checkout(IAppLogger logger) => _logger = logger;

        public void Pay(decimal amount)
        {
            _logger.Trace("start pay");
            _logger.Information($"charge {amount}");
            _logger.Log("payment accepted");
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- DIP: logger Trace / Log / Information / Exception ---");

        var checkout = new Checkout(new ConsoleAppLogger());
        checkout.Pay(19.99m);
        Console.WriteLine("Swap ConsoleAppLogger for a file logger later — Checkout stays the same.");
    }
}
