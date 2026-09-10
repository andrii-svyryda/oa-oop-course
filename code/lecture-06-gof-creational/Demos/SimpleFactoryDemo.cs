namespace Lecture06GofCreational.Demos;

public static class SimpleFactoryDemo
{
    interface INotifier
    {
        string Send(string text);
    }

    class EmailNotifier : INotifier
    {
        public string Send(string text) => $"email: {text}";
    }

    class SmsNotifier : INotifier
    {
        public string Send(string text) => $"sms: {text}";
    }

    static class NotifierFactory
    {
        public static INotifier Create(string channel) => channel switch
        {
            "email" => new EmailNotifier(),
            "sms" => new SmsNotifier(),
            _ => throw new ArgumentOutOfRangeException(nameof(channel))
        };
    }

    public static void Run()
    {
        Console.WriteLine("--- Simple factory ---");
        Console.WriteLine(NotifierFactory.Create("email").Send("hello"));
        Console.WriteLine(NotifierFactory.Create("sms").Send("hello"));
        Console.WriteLine("New channel = edit this switch. That is OCP from lecture 04.");
    }
}
