namespace Lecture06GofCreational.Demos;

public static class SingletonDemo
{
    sealed class UnsafeCache
    {
        private static UnsafeCache? _instance;

        private UnsafeCache() { }

        public static UnsafeCache Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UnsafeCache();
                return _instance;
            }
        }
    }

    sealed class LazyCache
    {
        private static readonly Lazy<LazyCache> _lazy = new(() => new LazyCache());

        private LazyCache() { }

        public static LazyCache Instance => _lazy.Value;
    }

    public static void Run()
    {
        Console.WriteLine("--- Singleton ---");
        Console.WriteLine($"  unsafe same instance: {ReferenceEquals(UnsafeCache.Instance, UnsafeCache.Instance)}");
        Console.WriteLine($"  Lazy<T> same instance: {ReferenceEquals(LazyCache.Instance, LazyCache.Instance)}");
        Console.WriteLine("Unsafe getter is not thread-safe. In a project use Lazy<T> or AddSingleton from lecture 05.");
    }
}
