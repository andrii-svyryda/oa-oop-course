namespace Lecture07GofStructural.Demos;

public static class FacadeDemo
{
    class Product
    {
        public string GetDetails() => "book";
    }

    class Payment
    {
        public string Charge() => "paid";
    }

    class Invoice
    {
        public string Send() => "invoice emailed";
    }

    class OrderFacade
    {
        public string PlaceOrder()
        {
            var product = new Product();
            var payment = new Payment();
            var invoice = new Invoice();
            return $"{product.GetDetails()}, {payment.Charge()}, {invoice.Send()}";
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- Facade ---");
        Console.WriteLine($"  {new OrderFacade().PlaceOrder()}");
        Console.WriteLine("Client calls one method. Facade talks to three classes.");
    }
}
