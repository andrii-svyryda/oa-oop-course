namespace Lecture02Generics.Demos;

public static class GenericCacheDemo
{
    class UserCacheItem
    {
        public string Id { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = [];
    }

    class VerySimpleAndNaiveCache<T>
    {
        private readonly Dictionary<string, T> cache = new();

        public T GetOrCreate(string key, Func<T> createItem)
        {
            if (!cache.ContainsKey(key))
            {
                cache[key] = createItem();
            }

            return cache[key];
        }

        public void Add<U>(Dictionary<string, U> items) where U : T
        {
            foreach (var item in items)
            {
                cache[item.Key] = item.Value;
            }
        }
    }

    class SameUserCacheItem : UserCacheItem;

    public static void Run()
    {
        Console.WriteLine("--- Generic cache ---");

        var userIdA = Guid.NewGuid().ToString();
        var userIdB = Guid.NewGuid().ToString();

        var usersCache = new VerySimpleAndNaiveCache<UserCacheItem>();

        var userA = usersCache.GetOrCreate(userIdA, () => new UserCacheItem
        {
            Id = userIdA,
            Roles = ["Admin", "User"],
        });

        var userB = usersCache.GetOrCreate(userIdB, () => new UserCacheItem
        {
            Id = userIdB,
            Roles = ["User", "Operator"],
        });

        Console.WriteLine($"User A: {userA.Id}, roles=[{string.Join(", ", userA.Roles)}]");
        Console.WriteLine($"User B: {userB.Id}, roles=[{string.Join(", ", userB.Roles)}]");

        var extraItems = new Dictionary<string, SameUserCacheItem>
        {
            [userIdA] = new SameUserCacheItem { Id = userIdA, Roles = ["XYZ", "ABC"] },
        };
        usersCache.Add(extraItems);
        Console.WriteLine("Added derived type items via where U : T constraint.");
    }
}
