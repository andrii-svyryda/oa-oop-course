# Приклади з лекцій

Тут зібрані консольні проєкти з кодом, який ви бачите на слайдах. Запустіть потрібний проєкт і проходьте секції паралельно з презентацією.

## Що вам знадобиться

- [.NET 8 SDK](https://dotnet.microsoft.com/download) — перевірте в терміналі команду `dotnet --version`. Якщо вона працює, усе вже готово.

## Як запускати

```powershell
cd code

dotnet run --project lecture-01-oop-theory
dotnet run --project lecture-02-generics
dotnet run --project lecture-03-object-relationships
dotnet run --project lecture-04-solid
dotnet run --project lecture-05-dependency-injection
dotnet run --project lecture-06-gof-creational
dotnet run --project lecture-07-gof-structural
dotnet run --project lecture-08-gof-behavioral
dotnet run --project lecture-09-builtin-csharp-patterns
dotnet run --project lecture-10-new-csharp-features
dotnet run --project lecture-11-collections
dotnet run --project lecture-12-multithreading
```

Або ви можете зібрати весь розв’язок одразу:

```powershell
dotnet build OopCourse.sln
```
