namespace Lecture07GofStructural.Demos;

public static class AdapterDemo
{
    interface ILogger
    {
        void Log(string msg);
    }

    class LegacyLog
    {
        public void WriteLine(string x) => Console.WriteLine($"  legacy: {x}");
    }

    class LoggerAdapter : ILogger
    {
        private readonly LegacyLog _old;

        public LoggerAdapter(LegacyLog old) => _old = old;

        public void Log(string msg) => _old.WriteLine(msg);
    }

    public static void Run()
    {
        Console.WriteLine("--- Adapter ---");
        ILogger logger = new LoggerAdapter(new LegacyLog());
        logger.Log("checkout failed");
        Console.WriteLine("Client speaks ILogger. Adapter translates to WriteLine.");
    }
}
