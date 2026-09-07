# Лабораторна робота 13. SqlBuilder (патерн Builder)

| | |
|---|---|
| **План** | Загальний навчальний план |
| **Номер** | 13 |
| **Лекція** | [Породжувальні патерни — Builder](../../lectures/06-gof-creational.md) |
| **Поняття** | Builder, fluent API, generics |

## Завдання

Розробіть клас-будівельник `SqlBuilder`, який підтримує вибірку даних із сортуванням і працює з PostgreSQL та MS SQL.

## Вимоги

- Fluent-інтерфейс на кшталт `SqlBuilder.Select<User>().OrderBy(_ => _.RegistrationDate).Take(10)`.
- Результат має давати `CommandText` і `CommandParameters` для виконання через з’єднання.
- Орієнтир API: https://maxtoroq.github.io/DbExtensions/docs/SqlBuilder.html
