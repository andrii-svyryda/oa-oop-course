namespace Lecture08GofBehavioral.Demos;

public static class StateDemo
{
    interface IOrderState
    {
        string Pay(Order order);
    }

    class Draft : IOrderState
    {
        public string Pay(Order order)
        {
            order.Become(new Paid());
            return "draft -> paid";
        }
    }

    class Paid : IOrderState
    {
        public string Pay(Order order) => "already paid, ignore";
    }

    class Order
    {
        private IOrderState _state = new Draft();

        public string Pay() => _state.Pay(this);

        internal void Become(IOrderState next) => _state = next;
    }

    public static void Run()
    {
        Console.WriteLine("--- State ---");
        var order = new Order();
        Console.WriteLine($"  {order.Pay()}");
        Console.WriteLine($"  {order.Pay()}");
        Console.WriteLine("State changes itself. Strategy would wait for the client to swap the algorithm.");
    }
}
