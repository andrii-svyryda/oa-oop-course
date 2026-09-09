namespace Lecture04Solid.Demos;

public static class DipSalaryDemo
{
    interface ISalaryCalculator
    {
        float CalculateSalary(int hoursWorked, float hourlyRate);
    }

    class SalaryCalculator : ISalaryCalculator
    {
        public float CalculateSalary(int hoursWorked, float hourlyRate) =>
            hoursWorked * hourlyRate;
    }

    class EmployeeDetails
    {
        private readonly ISalaryCalculator _salaryCalculator;
        public int HoursWorked { get; init; }
        public float HourlyRate { get; init; }

        public EmployeeDetails(ISalaryCalculator salaryCalculator) =>
            _salaryCalculator = salaryCalculator;

        public float GetSalary() => _salaryCalculator.CalculateSalary(HoursWorked, HourlyRate);
    }

    public static void Run()
    {
        Console.WriteLine("--- DIP: do not new inside the business class ---");

        var employee = new EmployeeDetails(new SalaryCalculator())
        {
            HoursWorked = 40,
            HourlyRate = 15,
        };
        Console.WriteLine($"40h * 15 = {employee.GetSalary()}");
        Console.WriteLine("EmployeeDetails depends on ISalaryCalculator, not on a concrete new.");
    }
}
