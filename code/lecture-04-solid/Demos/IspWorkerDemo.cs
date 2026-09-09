namespace Lecture04Solid.Demos;

public static class IspWorkerDemo
{
    interface IWorkable
    {
        void Work();
    }

    interface IEatable
    {
        void Eat();
    }

    interface ISleepable
    {
        void Sleep();
    }

    class HumanWorker : IWorkable, IEatable, ISleepable
    {
        public void Work() => Console.WriteLine("Human works");
        public void Eat() => Console.WriteLine("Human eats");
        public void Sleep() => Console.WriteLine("Human sleeps");
    }

    class RobotWorker : IWorkable
    {
        public void Work() => Console.WriteLine("Robot works");
    }

    public static void Run()
    {
        Console.WriteLine("--- ISP: robot does not eat ---");

        IWorkable[] crew = [new HumanWorker(), new RobotWorker()];
        foreach (var worker in crew)
        {
            worker.Work();
        }

        IEatable human = new HumanWorker();
        human.Eat();
        Console.WriteLine("Robot implements only IWorkable. No fake Eat/Sleep.");
    }
}
