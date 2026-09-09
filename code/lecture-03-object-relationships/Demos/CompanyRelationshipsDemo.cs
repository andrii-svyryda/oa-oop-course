namespace Lecture03ObjectRelationships.Demos;

public static class CompanyRelationshipsDemo
{
    public class Worker
    {
        public string Name { get; }
        public Worker(string name) => Name = name;
        public override string ToString() => Name;
    }

    public class Project
    {
        public string Title { get; }
        public List<Worker> Team { get; } = [];

        public Project(string title) => Title = title;

        public void Add(Worker worker) => Team.Add(worker);
    }

    public class Manager
    {
        public string Name { get; }
        public List<Worker> Reports { get; } = [];
        public List<Project> Projects { get; } = [];

        public Manager(string name) => Name = name;

        public void Manage(Worker worker) => Reports.Add(worker);
        public void Lead(Project project) => Projects.Add(project);
    }

    public class Company
    {
        private readonly List<Manager> _managers = [];

        public IReadOnlyList<Manager> Managers => _managers;

        public Manager HireManager(string name)
        {
            var manager = new Manager(name);
            _managers.Add(manager);
            return manager;
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- Company: association / aggregation / composition ---");

        var anna = new Worker("Anna");
        var bogdan = new Worker("Bogdan");
        Console.WriteLine($"Workers exist on their own: {anna}, {bogdan}");

        var company = new Company();
        var ira = company.HireManager("Ira");
        Console.WriteLine($"Composition: Company created manager {ira.Name}. Managers={company.Managers.Count}");

        ira.Manage(anna);
        ira.Manage(bogdan);
        Console.WriteLine($"Association: {ira.Name} manages {string.Join(", ", ira.Reports)}");

        var mobile = new Project("Mobile app");
        mobile.Add(anna);
        mobile.Add(bogdan);
        ira.Lead(mobile);
        Console.WriteLine($"Aggregation: project '{mobile.Title}' team=[{string.Join(", ", mobile.Team)}]");

        Console.WriteLine("Close the project — workers stay in the company:");
        mobile.Team.Clear();
        Console.WriteLine($"  project team now empty, workers still here: {anna}, {bogdan}");

        Console.WriteLine("Fire the manager — workers still exist:");
        ira.Reports.Clear();
        Console.WriteLine($"  manager reports empty, workers still here: {anna}, {bogdan}");
    }
}
