# Принципи SOLID

## На що звернути увагу

Зверніть особливу увагу на кожен із п’яти принципів SOLID. Ви детально розберете принцип єдиної відповідальності (SRP), принцип відкритості/закритості (OCP), принцип підстановки Лісков (LSP), принцип розділення інтерфейсу (ISP) та принцип інверсії залежностей (DIP).

## Спробуйте самі

Спробуйте застосувати кожен принцип на практиці. Для SRP розділіть логіку відправки email та валідації адреси. Для OCP реалізуйте калькулятор площі або знижок, який легко розширювати. Щоб зрозуміти LSP, напишіть механізм запису у файл із підтримкою різних форматів. Для засвоєння ISP створіть базовий механізм та розділіть його на уточнені інтерфейси. Нарешті, для DIP спроєктуйте логер із рівнями Trace, Log, Information та Exception, використовуючи абстракції.

## Пов’язані лабораторні

- [Загальний план, №12](../labs/general/lab-12-library-solid-ioc.md)
- [Індивідуальний план, №15](../labs/individual/lab-15-library-solid-ioc.md)

Приклади коду до цієї лекції ви знайдете в директорії `code/lecture-04-solid`.

---

# SOLID

[Single Responsibility Principle (розбір)](https://dotnettutorials.net/lesson/single-responsibility-principle/)

**Принцип єдиної відповідальності (SRP):** клас має мати одну причину для зміни. Приклад — відправка email і валідація адреси / теми як окремі типи, а не один «бог-клас».

**Принцип відкритості/закритості (Open/Closed Principle - OCP)**: класи повинні бути відкритими для розширення, але закритими для модифікації. Це означає, що ви можете додавати нову функціональність, не змінюючи існуючий код.

violation

```csharp
public enum CustomerType
    {
        Regular,
        Premium,
        Newbie
    }
    public class DiscountCalculator
    {
        public double CalculateDiscount(double price, CustomerType customerType)
        {
            switch (customerType)
            {
                case CustomerType.Regular:
                    return price * 0.1;  // 10% discount for regular customers
                case CustomerType.Premium:
                    return price * 0.3;  // 30% discount for premium customers
                case CustomerType.Newbie:
                    return price * 0.05; // 5% discount for new customers
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
```

```csharp
    public interface IDiscountStrategy
    {
        double CalculateDiscount(double price);
    }
    
    
    public class RegularDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double price)
        {
            return price * 0.1;
        }
    }
    
    public class PremiumDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double price)
        {
            return price * 0.3;
        }
    }
    
    public class NewbieDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double price)
        {
            return price * 0.05;
        }
    }
    
    public class DiscountCalculator
    {
        private readonly IDiscountStrategy discountStrategy;
        public DiscountCalculator(IDiscountStrategy discountStrategy)
        {
            discountStrategy = discountStrategy;
        }
        public double CalculateDiscount(double price)
        {
            return discountStrategy.CalculateDiscount(price);
        }
    }
    

    public class Program
    {
        public static void Main()
        {
            var regularDiscount = new RegularDiscount();
            
            var calculator = new DiscountCalculator(regularDiscount);
            double discountedPrice = calculator.CalculateDiscount(100); // 10% discount applied
            
            var premiumDiscount = new PremiumDiscount();
            calculator = new DiscountCalculator(premiumDiscount);
            discountedPrice = calculator.CalculateDiscount(100); // 30% discount applied
            
            Console.ReadKey();
        }
    }
```

Принцип підстановки Лісков (LSP) вимагає, щоб батьківські класи можна було замінити на їхніх наслідників без впливу на виконання програми.

Якщо S є підтипом T, тоді об'єкти типу T у програмі можуть бути замінені на об'єкти типу S без зміни будь-яких бажаних властивостей цієї програми.

LSP violation:

```csharp
 class Program
    {
        static void Main(string[] args)
        {
            Apple apple = new Orange();
            Console.WriteLine(apple.GetColor());
        }
    }
    public class Apple
    {
        public virtual string GetColor()
        {
            return "Red";
        }
    }
    public class Orange : Apple
    {
        public override string GetColor()
        {
            return "Orange";
        }
    }
```

LSP:

```csharp
sing System;
namespace Solid
{
    class Program
    {
        static void Main(string[] args)
        {
            IFruit fruit = new Orange();
            Console.WriteLine($"Color of Orange: {fruit.GetColor()}");
            fruit = new Apple();
            Console.WriteLine($"Color of Apple: {fruit.GetColor()}");
            Console.ReadKey();
        }
    }
    public interface IFruit
    {
        string GetColor();
    }
    public class Apple : IFruit
    {
        public string GetColor()
        {
            return "Red";
        }
    }
    public class Orange : IFruit
    {
        public string GetColor()
        {
            return "Orange";
        }
    }
}
```

**Принцип розділення інтерфейсу (Interface Segregation Principle - ISP)**: клієнтський код не повинен залежати від інтерфейсів, яких він не використовує. Це означає, що великі інтерфейси потрібно розділяти на менші та специфічніші, щоб клієнти використовували лише ті інтерфейси, які їм необхідні.

Порушення принципу: interface segregation

```csharp
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}

public class HumanWorker : IWorker
{
    public void Work() {}
    public void Eat() {}
    public void Sleep() {}
}

public class RobotWorker : IWorker
{
    public void Work() {}
    public void Eat() {} // не доречно для робота (звісно якщо це не електрохарчування :) )
    public void Sleep() {} // не доречно для робота
}
```

Як би було вірно:

```csharp
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public interface ISleepable
{
    void Sleep();
}

public class HumanWorker : IWorkable, IEatable, ISleepable
{
    public void Work() {}
    public void Eat() {}
    public void Sleep() {}
}

public class RobotWorker : IWorkable
{
    public void Work() {}
}
```

**Принцип інверсії залежностей (Dependency Inversion Principle - DIP)**: модулі вищого рівня не повинні залежати від модулів нижчого рівня. Обидва типи модулів повинні залежати від абстракцій. Також абстракції не повинні залежати від деталей. Деталі повинні залежати від абстракцій.

Порушення принципу dependency inversion:

```csharp
public class SalaryCalculator
{
    public float CalculateSalary(int hoursWorked, float hourlyRate) => hoursWorked * hourlyRate;
}

public class EmployeeDetails
{
    public int HoursWorked { get; set; }
    public int HourlyRate { get; set; }
    public float GetSalary()
    {
        var salaryCalculator = new SalaryCalculator();
        return salaryCalculator.CalculateSalary(HoursWorked, HourlyRate);
    }
}
```

Як би було вірно:

```csharp
public interface ISalaryCalculator
{
    float CalculateSalary(int hoursWorked, float hourlyRate);
}

public class SalaryCalculatorModified : ISalaryCalculator
{
    public float CalculateSalary(int hoursWorked, float hourlyRate) => hoursWorked * hourlyRate;
}

public class EmployeeDetailsModified
{
    private readonly ISalaryCalculator salaryCalculator;

    public int HoursWorked { get; set; }
    public int HourlyRate { get; set; }
    
    // залежність передається ззовні
    public EmployeeDetailsModified(ISalaryCalculator salaryCalculator)
    {
        salaryCalculator = salaryCalculator;
    }
    
    public float GetSalary()
    {
        return salaryCalculator.CalculateSalary(HoursWorked, HourlyRate);
    }
}
```
