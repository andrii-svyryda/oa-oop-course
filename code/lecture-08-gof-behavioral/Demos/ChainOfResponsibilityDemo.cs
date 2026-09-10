namespace Lecture08GofBehavioral.Demos;

public static class ChainOfResponsibilityDemo
{
    abstract class Handler
    {
        protected Handler? Next;

        public Handler SetNext(Handler next)
        {
            Next = next;
            return next;
        }

        public virtual string Handle(string role) => Next?.Handle(role) ?? "end";
    }

    class AuthHandler : Handler
    {
        public override string Handle(string role)
        {
            if (role == "anon")
                return "auth stopped: not signed in";
            return $"auth ok -> {base.Handle(role)}";
        }
    }

    class RoleHandler : Handler
    {
        public override string Handle(string role)
        {
            if (role != "admin")
                return "role stopped: not admin";
            return $"role ok -> {base.Handle(role)}";
        }
    }

    class AuditHandler : Handler
    {
        public override string Handle(string role) => $"audit logged for {role}";
    }

    public static void Run()
    {
        Console.WriteLine("--- Chain of responsibility ---");
        var auth = new AuthHandler();
        auth.SetNext(new RoleHandler()).SetNext(new AuditHandler());
        Console.WriteLine($"  {auth.Handle("anon")}");
        Console.WriteLine($"  {auth.Handle("user")}");
        Console.WriteLine($"  {auth.Handle("admin")}");
        Console.WriteLine("A link may stop the chain. Decorator always wraps and continues.");
    }
}
