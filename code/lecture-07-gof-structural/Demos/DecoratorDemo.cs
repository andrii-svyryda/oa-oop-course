namespace Lecture07GofStructural.Demos;

public static class DecoratorDemo
{
    interface IMessenger
    {
        void Send(string message);
    }

    class RealMessenger : IMessenger
    {
        public void Send(string message) => Console.WriteLine($"  sent: {message}");
    }

    class LoggingMessenger : IMessenger
    {
        private readonly IMessenger _inner;

        public LoggingMessenger(IMessenger inner) => _inner = inner;

        public void Send(string message)
        {
            Console.WriteLine("  log: sending");
            _inner.Send(message);
        }
    }

    class RetryMessenger : IMessenger
    {
        private readonly IMessenger _inner;

        public RetryMessenger(IMessenger inner) => _inner = inner;

        public void Send(string message)
        {
            Console.WriteLine("  retry: first try");
            _inner.Send(message);
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- Decorator ---");
        IMessenger m = new RetryMessenger(new LoggingMessenger(new RealMessenger()));
        m.Send("hello");
        Console.WriteLine("Client still calls IMessenger. Wrappers add log, then retry.");
    }
}
