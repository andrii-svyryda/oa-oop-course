namespace Lecture03ObjectRelationships.Demos;

public static class DiBridgeDemo
{
    interface IClock
    {
        DateTime Now { get; }
    }

    class SystemClock : IClock
    {
        public DateTime Now => DateTime.Now;
    }

    class Shift
    {
        private readonly IClock _clock;
        public Shift(IClock clock) => _clock = clock;
        public string Stamp(string who) => $"{who} checked in at {_clock.Now:HH:mm}";
    }

    public static void Run()
    {
        Console.WriteLine("--- Bridge to DI: do not new the dependency ---");

        var shift = new Shift(new SystemClock());
        Console.WriteLine(shift.Stamp("Anna"));
        Console.WriteLine("Shift does not create the clock. It receives it. Association, not composition — first step to DI.");
    }
}
