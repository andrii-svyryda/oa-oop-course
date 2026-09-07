namespace Lecture01OopTheory.Demos;

public static class EncapsulationDemo
{
    class Account
    {
        private readonly string accountNumber;
        private decimal balance;

        public Account(string accountNumber, decimal balance)
        {
            this.accountNumber = accountNumber;
            this.balance = balance;
        }

        public void Deposit(decimal amount) => balance += amount;

        public void Withdraw(decimal amount)
        {
            if (balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds. Please check balance");
            }

            balance -= amount;
        }

        public string AccountNumber => accountNumber;
        public decimal Balance => balance;
    }

    public static void Run()
    {
        Console.WriteLine("--- Encapsulation ---");

        var account = new Account("11212122", 100m);
        account.Withdraw(10m);

        Console.WriteLine($"Account {account.AccountNumber}, balance={account.Balance}");

        try
        {
            account.Withdraw(1000m);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Withdraw failed: {ex.Message}");
        }
    }
}
