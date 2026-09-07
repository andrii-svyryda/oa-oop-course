# Впровадження залежностей (Dependency Injection)

## На що звернути увагу

- Що таке залежності
- Способи передачі залежностей
- IoC-контейнери

## Спробуйте самі

- Клас Worker, який обробляє дані залежно від типу роботи
- Вбудовані засоби IoC у C# / .NET
- Життєві цикли: Transient, Scoped, Singleton

## Пов’язані лабораторні

- [Загальний план, №12, №14](../labs/general/lab-12-library-solid-ioc.md)
- [Індивідуальний план, №15, №17](../labs/individual/lab-15-library-solid-ioc.md)

---

# Dependency injection🪄

**Щільне зв'язування (Tight Coupling)**

Об'єкт із щільним зв'язуванням — це об'єкт, який потребує знань про багато інших об'єктів і зазвичай має високу залежність від їхніх інтерфейсів. Зміна одного об'єкта в додатку із щільним зв'язуванням часто вимагає змін у багатьох інших об'єктах. У малому додатку ви можете легко ідентифікувати зміни, і є менший шанс щось пропустити. Проте у великих додатках ці взаємозалежності не завжди відомі кожному розробнику, або є шанс проігнорувати зміни. Натомість об'єкти зі слабким зв'язуванням менш залежні один від одного.

![Untitled](assets/05-dependency-injection/Untitled.png)

```csharp
public class LogManager
{
    public void LogToConsole(string message)
    {
        Console.WriteLine($"Logging to console: {message}");
    }

    public void LogToFile(string message)
    {
        Console.WriteLine($"Logging to file: {message}");
    }
}

public class MessageProcessorForTightCoupling
{
    private LogManager logManager; 

    public MessageProcessorForTightCoupling()
    { 
        this.logManager = new LogManager();
    }

    public void ProcessMessage(string message)
    {
        Console.WriteLine($"Processing message: {message}");
        this.logManager.LogToFile($"Message processed: {message}");
    }
 }
 
MessageProcessorForTightCoupling messageProcessor = new MessageProcessorForTightCoupling();
messageProcessor.ProcessMessage("Sample message");
```

**Щоб усунути такі проблеми, слід внести структурні зміни в відносини між класами, і ці відносини слід надавати через абстрактні класи або інтерфейси.**

**Слабке зв'язування**

Слабке зв'язування — це ціль проєктування, яка прагне зменшити взаємозалежність між компонентами системи, щоб знизити ризик того, що зміни в одному компоненті потребуватимуть змін у будь-якому іншому. Слабке зв'язування є набагато загальнішою концепцією, спрямованою на підвищення гнучкості системи, її легшого обслуговування та стабільності всієї платформи.

Впровадження залежностей має на меті відокремити створення об'єктів від їх використання, що призводить до слабкозв'язних програм. Цей шаблон гарантує, що об'єкт або функція, яка хоче використовувати певний сервіс, не повинна знати, як цей сервіс конструювати

Ш[аблон проєктування програмного забезпечення](https://uk.wikipedia.org/wiki/%D0%A8%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD%D0%B8_%D0%BF%D1%80%D0%BE%D1%94%D0%BA%D1%82%D1%83%D0%B2%D0%B0%D0%BD%D0%BD%D1%8F_%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BD%D0%BE%D0%B3%D0%BE_%D0%B7%D0%B0%D0%B1%D0%B5%D0%B7%D0%BF%D0%B5%D1%87%D0%B5%D0%BD%D0%BD%D1%8F), що передбачає надання зовнішньої залежності [програмному компоненту](https://uk.wikipedia.org/wiki/%D0%9A%D0%BE%D0%BC%D0%BF%D0%BE%D0%BD%D0%B5%D0%BD%D1%82%D0%BD%D0%BE-%D0%BE%D1%80%D1%96%D1%94%D0%BD%D1%82%D0%BE%D0%B2%D0%B0%D0%BD%D0%B5_%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D1%83%D0%B2%D0%B0%D0%BD%D0%BD%D1%8F), використовуючи [«інверсію керування»](https://uk.wikipedia.org/wiki/%D0%86%D0%BD%D0%B2%D0%B5%D1%80%D1%81%D1%96%D1%8F_%D0%BA%D0%B5%D1%80%D1%83%D0%B2%D0%B0%D0%BD%D0%BD%D1%8F) ([англ.](https://uk.wikipedia.org/wiki/%D0%90%D0%BD%D0%B3%D0%BB%D1%96%D0%B9%D1%81%D1%8C%D0%BA%D0%B0_%D0%BC%D0%BE%D0%B2%D0%B0) *Inversion of control*, IoC) для розв'язання (отримання) залежностей.

Впровадження — це передача [залежності](https://uk.wikipedia.org/wiki/%D0%97%D0%B2%27%D1%8F%D0%B7%D0%BD%D1%96%D1%81%D1%82%D1%8C_(%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D1%83%D0%B2%D0%B0%D0%BD%D0%BD%D1%8F)) (тобто, сервісу) залежному [об'єкту](https://uk.wikipedia.org/wiki/%D0%9E%D0%B1%27%D1%94%D0%BA%D1%82_(%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D1%83%D0%B2%D0%B0%D0%BD%D0%BD%D1%8F)) (тобто, клієнту). Передавати залежності клієнту замість того, щоб дозволити клієнту створювати сервіс самостійно, є фундаментальною вимогою до цього [шаблону проєктування](https://uk.wikipedia.org/wiki/%D0%A8%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD%D0%B8_%D0%BF%D1%80%D0%BE%D1%94%D0%BA%D1%82%D1%83%D0%B2%D0%B0%D0%BD%D0%BD%D1%8F_%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BD%D0%BE%D0%B3%D0%BE_%D0%B7%D0%B0%D0%B1%D0%B5%D0%B7%D0%BF%D0%B5%D1%87%D0%B5%D0%BD%D0%BD%D1%8F).

```csharp
internal class Program
{
	static void Main(string[] args)
	{
		using IHost host = Host.CreateDefaultBuilder()
							   .ConfigureServices((_, services) =>
							   {
							   	services.AddSingleton<IEmailNotification, EmailNotification>();
							   	services.AddSingleton<NotificationService>();
							   })
							   .Build();

		using var scope = host.Services.CreateScope();

		var services = scope.ServiceProvider;

		var emailSender = services.GetService<IEmailNotification>();

		var notificationSender = services.GetService<NotificationService>();

		notificationSender.PushNotification("dependency injection is not equal dependency inversion");
	}

	public interface IEmailNotification
	{
		public void Notify(string message);
	}

	public class EmailNotification : IEmailNotification
	{
		public EmailNotification()
		{
			Console.WriteLine("EmailNotification created");
		}

		public void Notify(string message)
		{
			Console.WriteLine("Email sent: " + message);
		}
	}

	public class NotificationService
	{
		private readonly IEmailNotification notificationProvider;

		public NotificationService(IEmailNotification customerService)
		{
			notificationProvider = customerService;
		}

		public void PushNotification(string message)
		{
			notificationProvider.Notify(message);
		}
	}

}
```

## Life time

There are three lifetimes that can be used with Microsoft Dependency Injection Container, they are:

- **Transient** — Services are created **each time they are requested**. It gets a new instance of the injected object, on each request of this object. For each time you inject this object is injected in the class, it will create a new instance.
- **Scoped** — Services are created **on each request** (once per request). This is most recommended for WEB applications. So for example, if during a request you use the same dependency injection, in many places, you will use the same instance of that object, it will make reference to the same memory allocation.
- **Singleton** — Services are created **once for the lifetime of the application**. It uses the same instance for the whole application.

[https://jason.sultana.net.au/dotnet/testing/2022/08/06/dependency-injection-in-console-app.html](https://jason.sultana.net.au/dotnet/testing/2022/08/06/dependency-injection-in-console-app.html)
