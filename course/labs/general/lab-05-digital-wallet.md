# Лабораторна робота 5. DigitalWallet і провайдери автентифікації

| | |
|---|---|
| **План** | Загальний навчальний план |
| **Номер** | 5 |
| **Лекція** | [Теорія ООП — інкапсуляція та інтерфейси](../../lectures/01-oop-theory.md) |
| **Поняття** | інкапсуляція, інтерфейси, залежності |

## Завдання

Опишіть клас `DigitalWallet` (баланс, логін, хешований пароль — лише під час створення, список транзакцій). Публічний інтерфейс: депозит, зняття, перевірка балансу, журнал транзакцій. Керування через консольне меню. Клієнтський код має працювати через `IDigitalWallet`.

## Вимоги

- `ILoginProvider.Validate(string login, string password)`.
- `GmailAuthProvider` — конструктор (gmail, password).
- `Privat24AuthProvider` — конструктор (телефон, пароль банкінгу).
- `DigitalWallet`: `GetTransactionLog()`, `CheckBalance()`, `Withdraw`, `Deposit`, `SetAuthProvider`.
- Якщо користувач не автентифікований — `UnauthorizedAccessException("Invalid credentials")`.
