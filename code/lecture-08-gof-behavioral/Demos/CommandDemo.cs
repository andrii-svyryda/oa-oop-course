namespace Lecture08GofBehavioral.Demos;

public static class CommandDemo
{
    class Light
    {
        public bool On { get; private set; }

        public void TurnOn() => On = true;

        public void TurnOff() => On = false;
    }

    interface ICommand
    {
        void Execute();
        void Undo();
    }

    class LightOn : ICommand
    {
        private readonly Light _light;

        public LightOn(Light light) => _light = light;

        public void Execute() => _light.TurnOn();

        public void Undo() => _light.TurnOff();
    }

    public static void Run()
    {
        Console.WriteLine("--- Command ---");
        var light = new Light();
        ICommand command = new LightOn(light);
        command.Execute();
        Console.WriteLine($"  after Execute: on={light.On}");
        command.Undo();
        Console.WriteLine($"  after Undo: on={light.On}");
        Console.WriteLine("Invoker holds ICommand, not Light. Request is an object.");
    }
}
