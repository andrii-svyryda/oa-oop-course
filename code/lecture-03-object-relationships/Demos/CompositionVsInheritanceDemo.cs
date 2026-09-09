namespace Lecture03ObjectRelationships.Demos;

public static class CompositionVsInheritanceDemo
{
    class InheritedStack<T> : List<T>
    {
        public void Push(T item) => Add(item);

        public T Pop()
        {
            var item = this[^1];
            RemoveAt(Count - 1);
            return item;
        }
    }

    class ComposedStack<T>
    {
        private readonly List<T> _items = [];

        public void Push(T item) => _items.Add(item);

        public T Pop()
        {
            var item = _items[^1];
            _items.RemoveAt(_items.Count - 1);
            return item;
        }

        public int Count => _items.Count;
    }

    class Engine
    {
        public string Start() => "vroom";
    }

    class Car
    {
        private readonly Engine _engine = new();
        public string Drive() => $"{_engine.Start()}, go";
    }

    class Person
    {
        public string Name { get; }
        public Person(string name) => Name = name;
        public virtual string Introduce() => $"I am {Name}";
    }

    class Student : Person
    {
        public Student(string name) : base(name) { }
        public override string Introduce() => $"{base.Introduce()}, a student";
    }

    public static void Run()
    {
        Console.WriteLine("--- Composition vs inheritance ---");

        Console.WriteLine("BAD: stack is-a list — Insert breaks LIFO");
        var inherited = new InheritedStack<int>();
        inherited.Push(1);
        inherited.Push(2);
        inherited.Insert(0, 99);
        Console.WriteLine($"  after Insert(0, 99), Pop() = {inherited.Pop()}  (expected 2, got the inserted 99)");

        Console.WriteLine("GOOD: stack has-a list — only Push/Pop");
        var composed = new ComposedStack<int>();
        composed.Push(1);
        composed.Push(2);
        Console.WriteLine($"  Pop() = {composed.Pop()}  (2, LIFO holds)");

        Console.WriteLine("GOOD: car has-a engine, car is not an engine");
        Console.WriteLine($"  {new Car().Drive()}");

        Console.WriteLine("GOOD inheritance: student is-a person");
        Person person = new Student("Anna");
        Console.WriteLine($"  {person.Introduce()}");
    }
}
