# Узагальнення (generics) у C#

## На що звернути увагу

- Для чого потрібні шаблони
- Шаблонні типи, методи, класи та інтерфейси
- Шаблонні дерева виразів
- Обмеження шаблонів

## Спробуйте самі

- Метод обміну значеннями двох змінних
- Калькулятор для різних числових типів
- Шаблонний список (List / LinkedList)
- Огляд стандартних колекцій: List, Dictionary, Queue, Stack
- Шаблонні делегати Func / Action
- Обмеження notnull, class, default, Enum, new()

## Пов’язані лабораторні

- [Загальний план, №7, №11](../labs/general/lab-07-generic-linked-list.md)
- [Індивідуальний план, №12, №14](../labs/individual/lab-12-generic-linked-list.md)

---

# Узагальнення в C# 🎯

Механізм узагальнень дозволяє створювати інтерфейси, класи, методи та делегати, які підтримують будь-який тип даних. Вам не потрібно писати та дублювати код під кожен конкретний тип. Узагальнення вирішують проблему з приведенням типів, а тому підвищують безпеку типів під час компіляції.

Основні переваги узагальнень це:

- безпека типів
- краща продуктивність (не потрібно приводити тип)
- повторне використання

Розгляньмо приклад узагальненого методу, який обмінює значення двох змінних.

```csharp
	class Program
	{
		// тип узагальнення вказуємо в кутових дужках <T>
		// ref вказує, що змінну передаємо по посиланню 
		public static void Swap<T>(ref T one, ref T second)
		{
			T tmp = one;
			one = second;
			second = tmp;
		}

		static void Main(string[] args)
		{
			int a = 1;
			int b = 2;

			float c = 1.1F;
			float d = 2.3F;

			// викликаємо з явно вказаним типом int, хоча можна і без нього
			// оскільки компілятор виведе тип з аргументів, які ми передали в метод
			Swap<int>(ref a, ref b);
			Swap(ref c, ref d);

			Console.WriteLine($"a: {a} b: {b}");
			Console.WriteLine($"c: {c} d: {d}");
		}
	}
```

Якщо ви подивитеся на будь-яку колекцію на кшталт `List<>`, `Dictionary<>`, то побачите, що найчастіше узагальнення працюють із класами. Розгляньмо приклад простого класу, який реалізує кеш-функцію:

```csharp
public class UserCacheItem
	{
		public string Id { get; set; }
		public List<string> Roles { get; set; }
	}

	// generic клас для кешування елементів (дуже спрощений)
	public class VerySimpleAndNaiveCache<T>
	{
		private Dictionary<string, T> cache = new Dictionary<string, T>();

		// створює чи повертає об'єкт, якщо він є в кеші
		public T GetOrCreate(string key, Func<T> createItem)
		{
			if (!cache.ContainsKey(key))
			{
				cache[key] = createItem();
			}
			return cache[key];
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var userIdA = Guid.NewGuid().ToString();
			var userIdB = Guid.NewGuid().ToString();

			var usersCache = new VerySimpleAndNaiveCache<UserCacheItem>();
			
			// користувацький код
			var userA = usersCache.GetOrCreate(userIdA, () => new UserCacheItem { Id = userIdA, Roles = new List<string> {"Admin", "User"} });

			var userB = usersCache.GetOrCreate(userIdB, () => new UserCacheItem { Id = userIdB, Roles = new List<string> { "User", "Operator" } });
			
		}
	}
```

Зверніть увагу, що делегат Func<T> теж приймає узагальнений тип.

Розгляньмо складніший приклад із запитом на сервер GitHub, який покаже узагальнення, інкапсуляцію та наслідування:

```csharp
   // оголошуємо клас, який містить дані репозиторія github
   public class Repository
   {
       // атрибут для звязування імені json поля з іменем властивості класу
       [JsonPropertyName("name")]
       public string Name { get; set; }

       [JsonPropertyName("description")] 
       public string Description { get; set; }

       [JsonPropertyName("html_url")]
       public Uri GitHubHomeUrl { get; set; }
   
       [JsonPropertyName("homepage")] 
       public Uri Homepage { get; set; }

       [JsonPropertyName("watchers")]
       public int Watchers { get; set; }
   }

   // узагальнений інтерфейс з відповіддю на запит
   interface IResponse<T>
   {
       // явно вказуємо, що відповідь сервера не змінна
       // доступна лише для читання 
       public T Data { get; }
   }

   // імплементація класа теж узагальнена і теж породжена від узагальненого інтерфейса
   class Response<T> : IResponse<T>
   {
       // відповідь треба встановити, а сам клас не доступний для клієнтського коду!
       public T Data { get; set; }
   }

   internal class Program
   {
       // узагальнений метод який парсить рядок в клас відповідь
       static IResponse<List<T>> ParseResponse<T>(string responseString)
       {
           var data = JsonSerializer.Deserialize<List<T>>(responseString);
           return new Response<List<T>> { Data = data };
       }

       static void Main(string[] args)
       {
           // рутинний код, який робить запит на сервер git
           using HttpClient client = new();
           client.DefaultRequestHeaders.Accept.Clear();
           client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
           client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

           var json = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

           json.Wait();
           var resp = ParseResponse<Repository>(json.Result);

           foreach (var repo in resp.Data)
           {
               Console.WriteLine($"Name: {repo.Name} Watchers: {repo.Watchers}");
           }
       }
   }
```

Як видно з приклада вище, за допомогою узагальнень можна перевикористовувати один і той же код для безлічі типів.

**Обмеження узагальнень**

Дуже часто виникають ситуації, коли вам потрібно зробити обмеження для узагальненого типу. Це може бути обмеження, яке вкаже, що метод має приймати лише value type, або тип, який наслідує певний клас, інтерфейс чи делегат. Розгляньмо обмеження для value type:

```csharp
    // тип узагальнення вказуємо в кутових дужках <T>
		// ref вказує, що змінну передаємо по посиланню 
		// вказуємо обмеження, що метод приймає лише value type
		public static void Swap<T>(ref T one, ref T second) where T : struct
		{
			T tmp = one;
			one = second;
			second = tmp;
		}

		static void Main(string[] args)
		{
			string x = "a";
			string y = "b";
			int a = 1;
			int b = 2;

			float c = 1.1F;
			float d = 2.3F;

			// викликаємо з явно вказаним типом int, хоча можна і без нього
			// оскільки компілятор виведе тип з аргументів, які ми передали в метод
			Swap<int>(ref a, ref b);
			Swap(ref c, ref d);
			
			// The type 'string' must be a non-nullable value type in order to use it as parameter 'T' in the generic type or method			
			Swap(ref x, ref y);

			Console.WriteLine($"a: {a} b: {b}");
			Console.WriteLine($"c: {c} d: {d}");
		}
	}
```

Як бачимо, щоб зробити обмеження достатньо написати ключове слово where після назви метода, вказати тип і саме обмеження:

 

```csharp
 where T : struct
```

читається як: ”де Т є value type“.

Якщо скомпілювати приклад вище, то бачимо, що буде помилка компіляції, яка вкаже на те, що параметр T має бути value type.

Також часто доводиться вказувати обмеження для узагальненого типу, щоб явно викликати той чи інший метод від узагальненого типу, як на прикладі нижче:

```csharp
	// простий консольний логер
	interface ILogger
	{
		public void Log(string message);
	}

	class ConsoleLogger : ILogger
	{
		public void Log(string message)
		{
			Console.WriteLine(message);
		}
	}

	// generic клас для кешування елементів (дуже спрощений)
	// тип TLogger має підтримувати контракт ILogger 
	class VerySimpleAndNaiveCache<T, TLogger> where TLogger : ILogger
	{
		private TLogger logger;

		public VerySimpleAndNaiveCache(TLogger logger)
		{
			this.logger = logger;
		}

		private Dictionary<string, T> cache = new Dictionary<string, T>();

		// створює чи повертає об'єкт, якщо він є в кеші
		public T GetOrCreate(string key, Func<T> createItem)
		{
			if (!cache.ContainsKey(key))
			{
				var item = createItem();
				cache[key] = item;
				// оскільки тип TLogger має підтримує контракт ILogger, тому можемо викликати метод Log
				logger.Log($"Cache item {key} added to the cache {item}");
				return item;
			}

			var existedItem = cache[key];
			// оскільки тип TLogger має підтримує контракт ILogger, тому можемо викликати метод Log
			logger.Log($"Cache item {key} already exists in cache {existedItem}");
			return existedItem;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var userIdA = Guid.NewGuid().ToString();
			var userIdB = Guid.NewGuid().ToString();

			var usersCache = new VerySimpleAndNaiveCache<string, ILogger>(new ConsoleLogger());

			// користувацький код
			var userA = usersCache.GetOrCreate(userIdA, () => userIdA.ToUpper());

			var userB = usersCache.GetOrCreate(userIdB, () => userIdB.ToUpper());
		}
	}
}
```

Якби ми не вказали обмеження where TLogger : ILogger, тоді не змоглиб викликати: logger.Log($"Cache item {key} added to the cache {item}");

Інколи є необхідність створити узагальнений екземпляр усередині generic-методу, але ви не можете знати наперед, чи матиме узагальнений тип відкритий конструктор за замовчуванням. Для цього випадку теж існує обмеження:

```csharp
	class ConsoleLogger
	{
		public ConsoleLogger()
		{
		}

		public void Log(string message)
		{
			Console.WriteLine(message);
		}
	}

	class Program
	{
		// тип Т має бути reference type та мати конструктор по замовчуванню
		static T CreateInstance<T>(T logger = null) where T : class, new()
		{
			return logger ?? new T();
		}

		static void Main(string[] args)
		{
			var new_instance = CreateInstance<ConsoleLogger>();
			var old_instance = CreateInstance<ConsoleLogger>(new ConsoleLogger());
		}
	}
```

більше прикладів з обмеженням за посиланням: [https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters)

Тип параметра як обмеження:

```csharp
 public class UserCacheItem
 {
		public string Id { get; set; }
		public List<string> Roles { get; set; }
 }

	public class SameUserCacheItem : UserCacheItem
	{	
	}

	public class VerySimpleAndNaiveCache<T>
	{
		private Dictionary<string, T> cache = new Dictionary<string, T>();

		// створює чи повертає об'єкт, якщо він є в кеші
		public T GetOrCreate(string key, Func<T> createItem)
		{
			if (!cache.ContainsKey(key))
			{
				cache[key] = createItem();
			}
			return cache[key];
		}

		// обмеження на будь який тип U, але щоб був породжений від T
		public void Add<U>(Dictionary<string, U> items) where U : T
		{
			foreach (var item in items)
			{
				cache[item.Key] = item.Value;
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var userIdA = Guid.NewGuid().ToString();
			var userIdB = Guid.NewGuid().ToString();

			var items = new Dictionary<string, SameUserCacheItem>()
			{
				{ userIdA, new SameUserCacheItem { Id = userIdA, Roles = new List<string> { "XYZ", "ABC" } } },
				{ userIdA, new SameUserCacheItem { Id = userIdA, Roles = new List<string> { "ZYC", "GHT" } } }
			};

			var usersCache = new VerySimpleAndNaiveCache<UserCacheItem>();

			// користувацький код
			var userA = usersCache.GetOrCreate(userIdA, () => new UserCacheItem { Id = userIdA, Roles = new List<string> { "Admin", "User" } });

			var userB = usersCache.GetOrCreate(userIdB, () => new UserCacheItem { Id = userIdB, Roles = new List<string> { "User", "Operator" } });

			// працюєм оскільки SameUserCacheItem унаслідуваний від
			usersCache.Add(items);

		}
	}
```

Також можна вказувати enum як обмеження типу:

```csharp
	class Program
	{
		// де тип є enum
		public static Dictionary<int, string> EnumNamedValues<T>() where T : System.Enum
		{
			var result = new Dictionary<int, string>();
			var values = Enum.GetValues(typeof(T));

			foreach (int item in values)
			{
				result.Add(item, Enum.GetName(typeof(T), item));
			}
			return result;
		}

		static void Main(string[] args)
		{
			var map = EnumNamedValues<ConsoleColor>();

			foreach (var pair in map)
			{
				Console.WriteLine($"{pair.Key}:\t{pair.Value}");
			}
		}
	}
```

### Огляд колекцій C#

- List
- Dictionary
- Stack
- Queue

**List**

list - типізований список обєктів з доступом по індексу. 

```csharp
class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            List<string> dirs = new List<string>(Directory.EnumerateDirectories(docPath));

            foreach (var dir in dirs)
            {
                Console.WriteLine($"{dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1)}");
            }
            Console.WriteLine($"{dirs.Count} directories found.");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (PathTooLongException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
```

dictionary - колекція з ключа і значення

stack - структура даних, яка працює за принципом (дисципліною) «останнім прийшов — першим пішов» (LIFO, англ. last in, first out)

```csharp
Stack<string> numbers = new Stack<string>();
        numbers.Push("one");
        numbers.Push("two");
        numbers.Push("three");
        numbers.Push("four");
        numbers.Push("five");

				// стек можна обійти без зміни внутрішнього стану
        foreach( string number in numbers )
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("\nPopping '{0}'", numbers.Pop());
        Console.WriteLine("Peek at next item to destack: {0}",
            numbers.Peek());
        Console.WriteLine("Popping '{0}'", numbers.Pop());
```

Queue - представляє звичайну чергу, яка працює за алгоритмом FIFO (“перший зайшов – перший вийшов”).

```csharp
  Queue<string> numbers = new Queue<string>();
        numbers.Enqueue("one");
        numbers.Enqueue("two");
        numbers.Enqueue("three");
        numbers.Enqueue("four");
        numbers.Enqueue("five");

        // чергу можна обійти без зміни внутрішнього стану
        foreach( string number in numbers )
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("\nDequeuing '{0}'", numbers.Dequeue());
        Console.WriteLine("Peek at next item to dequeue: {0}",
            numbers.Peek());
```

[https://www.hangfire.io](https://www.hangfire.io/)
