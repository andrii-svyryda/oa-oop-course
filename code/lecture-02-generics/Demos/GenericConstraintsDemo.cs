namespace Lecture02Generics.Demos;

public static class GenericConstraintsDemo
{
    interface ILogger
    {
        void Log(string message);
    }

    class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
    }

    class VerySimpleAndNaiveCache<T, TLogger> where TLogger : ILogger
    {
        private readonly TLogger logger;
        private readonly Dictionary<string, T> cache = new();

        public VerySimpleAndNaiveCache(TLogger logger) => this.logger = logger;

        public T GetOrCreate(string key, Func<T> createItem)
        {
            if (!cache.ContainsKey(key))
            {
                var item = createItem();
                cache[key] = item;
                logger.Log($"Cache item {key} added: {item}");
                return item;
            }

            var existedItem = cache[key];
            logger.Log($"Cache item {key} already exists: {existedItem}");
            return existedItem;
        }
    }

    class ConsoleLoggerWithDefaultCtor
    {
        public void Log(string message) => Console.WriteLine(message);
    }

    static T CreateInstance<T>(T? instance = null) where T : class, new() =>
        instance ?? new T();

    public static Dictionary<int, string> EnumNamedValues<T>() where T : struct, Enum
    {
        var result = new Dictionary<int, string>();
        foreach (int item in Enum.GetValues(typeof(T)))
        {
            result[item] = Enum.GetName(typeof(T), item)!;
        }

        return result;
    }

    public static void Run()
    {
        Console.WriteLine("--- Generic constraints ---");

        var cache = new VerySimpleAndNaiveCache<string, ILogger>(new ConsoleLogger());
        var userId = Guid.NewGuid().ToString();
        _ = cache.GetOrCreate(userId, () => userId.ToUpper());
        _ = cache.GetOrCreate(userId, () => userId.ToUpper());

        var logger = CreateInstance<ConsoleLoggerWithDefaultCtor>();
        logger.Log("Created via where T : class, new()");

        Console.WriteLine("ConsoleColor enum map (first 5):");
        foreach (var pair in EnumNamedValues<ConsoleColor>().Take(5))
        {
            Console.WriteLine($"  {pair.Key}: {pair.Value}");
        }
    }
}
