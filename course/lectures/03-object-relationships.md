# Відносини між об’єктами в C#

## На що звернути увагу

- Асоціація
- Композиція
- Агрегація

## Спробуйте самі

- Приклади на базі класів: менеджер, робітник, проєкти

## Пов’язані лабораторні

- Практичні приклади з лекції теорії ООП (автосалон, гаманець) використовують композицію та асоціацію.

---

## Відношення між об’єктами: асоціація, агрегація, композиція

Розберіть зв’язки на класах **менеджер**, **робітник**, **проєкт**.

**Асоціація** — об’єкти знають один про одного і можуть взаємодіяти, але час життя не пов’язаний. Менеджер *керує* робітниками: видалення менеджера не знищує робітників.

**Агрегація** — «ціле і частини», але частина може існувати окремо від цілого. Проєкт (батько) і робітник (дитина): проєкт закрили — робітник лишається в компанії.

**Композиція** — сильніше володіння: частина не існує без цілого. Дім і кімната: без дому кімнати немає. У коді це зазвичай `private` поле, яке створюється всередині власника і не «витікає» назовні.

```csharp
public class Worker
{
    public string Name { get; }

    public Worker(string name) => Name = name;
}

public class Project
{
    // агрегація: робітники живуть і поза проєктом
    public List<Worker> Team { get; } = new();
}

public class Manager
{
    public string Name { get; }
    public List<Project> Projects { get; } = new(); // асоціація / агрегація

    public Manager(string name) => Name = name;
}

public class Company
{
    // композиція: організаційна структура не існує окремо від компанії
    private readonly List<Manager> _managers = new();

    public Manager HireManager(string name)
    {
        var manager = new Manager(name);
        _managers.Add(manager);
        return manager;
    }
}
```

Корисні розбори:

- [Difference between Composition and Aggregation](https://www.c-sharpcorner.com/article/difference-between-composition-and-aggregation/)
- [C# code for association, aggregation, composition](https://stackoverflow.com/questions/12604031/c-sharp-code-for-association-aggregation-composition)
