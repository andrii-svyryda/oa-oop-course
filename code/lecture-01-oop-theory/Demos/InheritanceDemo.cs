namespace Lecture01OopTheory.Demos;

public static class InheritanceDemo
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public Person(string name, int age, string address)
        {
            Name = name;
            Age = age;
            Address = address;
        }

        public void Display() => Console.WriteLine(ToString());

        public override string ToString() =>
            $"Name: {Name}, Age: {Age}, Address: {Address}";
    }

    class Student : Person
    {
        public string StudentEmail { get; }

        public Student(string name, int age, string address, string email)
            : base(name, age, address)
        {
            StudentEmail = email;
        }

        public void Enroll(string courseName) =>
            Console.WriteLine($"{Name} ({StudentEmail}) enrolled in {courseName}.");
    }

    class Teacher : Person
    {
        public string EmployeeId { get; set; }

        public Teacher(string name, int age, string address, string employeeId)
            : base(name, age, address)
        {
            EmployeeId = employeeId;
        }

        public void Teach(string courseName) =>
            Console.WriteLine($"{Name} is teaching {courseName}.");

        public void DisplayTeacher() =>
            Console.WriteLine($"{base.ToString()}, EmployeeId: {EmployeeId}");
    }

    public static void Run()
    {
        Console.WriteLine("--- Inheritance ---");

        var student = new Student("Andrii Stepanchuk", 20, "12 Mazepy St", "andrii.stepanchuk@edu.oa.com");
        student.Display();
        student.Enroll("OOP");

        var teacher = new Teacher("Melnychuk Oleksandr", 37, "25 Skovorody St", "TCH37OM");
        teacher.Display();
        teacher.Teach("Robotics");
        teacher.DisplayTeacher();
    }
}
