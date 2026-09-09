namespace Lecture02Generics.Demos;

public static class ValueReferenceDemo
{
    class User
    {
        public string Name { get; set; }
        public User(string name) => Name = name;
    }

    public static void Run()
    {
        Console.WriteLine("--- Value type vs reference type (memory) ---");

        int a = 42;
        int b = a;
        b = 7;
        Console.WriteLine($"int a={a}, b={b}  // two independent copies on the stack");

        var p = new User("Ann");
        var q = p;
        q.Name = "Bob";
        Console.WriteLine($"User p={p.Name}, q={q.Name}  // one object on the heap");

        string s1 = "Ann";
        string s2 = s1;
        s2 = "Bob";
        Console.WriteLine($"string s1={s1}, s2={s2}  // reference type, but immutable");
    }
}
