# Runnable lecture examples

Console apps with code from the lecture slides. Run one project and walk the sections as you go through the deck.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download) (you have it if `dotnet --version` works)

## Run

```powershell
cd code

dotnet run --project lecture-01-oop-theory
dotnet run --project lecture-02-generics
dotnet run --project lecture-03-object-relationships
dotnet run --project lecture-04-solid
```

Or build the whole solution:

```powershell
dotnet build OopCourse.sln
```
