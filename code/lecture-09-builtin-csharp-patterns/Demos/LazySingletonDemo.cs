namespace Lecture09BuiltinCsharpPatterns.Demos;

public static class LazySingletonDemo
{
    sealed class AppConfig
    {
        private static readonly Lazy<AppConfig> _lazy = new(() => new AppConfig());

        public static AppConfig Instance => _lazy.Value;

        public string Title { get; }

        private AppConfig() => Title = "OOP course";
    }

    public static void Run()
    {
        Console.WriteLine("--- Lazy<T> singleton ---");
        Console.WriteLine($"  {AppConfig.Instance.Title}");
        Console.WriteLine($"  same instance: {ReferenceEquals(AppConfig.Instance, AppConfig.Instance)}");
        Console.WriteLine("Lazy<T> creates the object once and is thread-safe. Prefer this or AddSingleton.");
    }
}
