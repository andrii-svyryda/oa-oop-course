# Колекції в C#

## На що звернути увагу

- Яку колекцію обрати за доступом (індекс, ключ, унікальність, LIFO / FIFO)
- Dictionary і хеш-функція, колізії
- HashSet / SortedSet
- Stack і Queue — коли саме

## Спробуйте самі

- Хеш-функція
- Порівняти `List<T>`, `Dictionary<TKey,TValue>`, `HashSet<T>` на одному наборі даних

У лекції 02 ви вже бачили, чому колекції generic і як це прибирає boxing. Сьогодні `<T>` не повторюємо — обираємо структуру за доступом.

## Пов’язані лабораторні

- [Загальний план, №7](../labs/general/lab-07-generic-linked-list.md)

---

### List

`List<T>` беріть, коли важливий порядок, потрібен індекс і розмір росте. Для ключа — `Dictionary`. На відміну від масиву, розмір змінюється динамічно.

```csharp
class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(docPath));

            foreach (var dir in dirs)
            {
                Console.WriteLine($"{dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1)}");
            }
            Console.WriteLine($"{dirs.Count} directories found.");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (PathTooLongException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
```

## **Dictionary**

`Dictionary<TKey, TValue>` — колекція пар ключ–значення. Пошук за ключем спирається на хеш-код; однакові ключі не допускаються. Нижче — як рахується простий хеш.

## Hash-функція

У мові **C#** хеш-функції використовуються для перетворення вхідних даних (наприклад, рядків) у числове значення фіксованого розміру, яке зазвичай називається **хеш-кодом**. Цей хеш-код можна використовувати для різних цілей, зокрема:

- отримання даних у хеш-таблицях,
- перевірки цілісності даних,
- забезпечення ефективного зберігання та виконання операцій порівняння.

```csharp
class Program {
        static void Main(string[] args) {
            string input = "Hello, Geeks!";
            string[] values = new string[50];
            int hashCode = HashFunction(input, values);
            values[hashCode] = input;
            Console.WriteLine("Hash Code using ASCII Values: {0}", hashCode);
        }

        static int HashFunction(string s, string[] array) {
            int total = 0;
            char[] c = s.ToCharArray();
            for (int k = 0; k < c.Length; k++)
                total += (int)c[k];
            return total % array.Length;
        }
    }
```

## Hashset

У мові **C#**, **HashSet** — це невпорядкована колекція унікальних елементів. Ця колекція була введена в **.NET 3.5**. Вона підтримує реалізацію множин і використовує хеш-таблицю для зберігання. HashSet є колекцією загального типу (generic type) і визначена в просторі імен **System.Collections.Generic**. Зазвичай використовується, коли необхідно уникнути дублювання елементів у колекції. Продуктивність **HashSet** значно краща порівняно зі списком (List).

### Важливі моменти, пов’язані з HashSet у C#:

1. **Клас HashSet реалізує інтерфейси:**
    - **ICollection**,
    - **IEnumerable**,
    - **IReadOnlyCollection**,
    - **ISet**,
    - **IDeserializationCallback**,
    - **ISerializable**.
2. У **HashSet** порядок елементів не визначений. Ви не можете сортувати елементи HashSet.
3. У **HashSet** елементи повинні бути унікальними.
4. У **HashSet** дублікати елементів не допускаються.
5. HashSet підтримує багато математичних операцій над множинами, таких як **перетин (intersection)**, **об’єднання (union)** і **різниця (difference)**.
6. **Ємність HashSet** визначає кількість елементів, які він може зберігати.
7. **HashSet** — це динамічна колекція, тобто її розмір автоматично збільшується при додаванні нових елементів.
8. У **HashSet** можна зберігати лише елементи одного типу.

```csharp
static public void Main()
    {
 
       
        HashSet<string> myhash1 = new HashSet<string>();
 
     
        myhash1.Add("C");
        myhash1.Add("C++");
        myhash1.Add("C#");
        myhash1.Add("Java");
        myhash1.Add("Ruby");
        Console.WriteLine("Elements of myhash1:");
 
      
        foreach(var val in myhash1)
        {
            Console.WriteLine(val);
        }
 
       
        HashSet<int> myhash2 = new HashSet<int>() {10,
                               100,1000,10000,100000};
                 
      
        Console.WriteLine("Elements of myhash2:");
        foreach(var value in myhash2)
        {
            Console.WriteLine(value);
        }
    }
```

## **Sorted set**

**SortedSet** — це колекція об'єктів, відсортованих у порядку зростання. Вона є колекцією загального типу (generic type) і визначена в просторі імен **System.Collections.Generic**. **SortedSet** також підтримує багато математичних операцій над множинами, таких як **перетин (intersection)**, **об’єднання (union)** і **різниця (difference)**. Це динамічна колекція, тобто її розмір автоматично збільшується при додаванні нових елементів.

### Важливі моменти:

1. **Клас SortedSet реалізує інтерфейси:**
    - **ICollection**,
    - **IEnumerable**,
    - **IReadOnlyCollection**,
    - **ISet**,
    - **IDeserializationCallback**,
    - **ISerializable**.
2. **Ємність SortedSet** — це кількість елементів, які вона може зберігати.
3. У **SortedSet** елементи повинні бути унікальними.
4. У **SortedSet** порядок елементів завжди **зростаючий**.
5. **SortedSet** зазвичай використовується, коли потрібно зберігати унікальні елементи та підтримувати їх порядок за зростанням.
6. У **SortedSet** можна зберігати лише елементи одного типу.

```csharp
// Creating SortedSet
        // Using SortedSet class
        SortedSet<int> my_Set1 = new SortedSet<int>();
 
        // Add the elements in SortedSet
        // Using Add method
        my_Set1.Add(101);
        my_Set1.Add(1001);
        my_Set1.Add(10001);
        my_Set1.Add(100001);
        Console.WriteLine("Elements of my_Set1:");
 
        // Accessing elements of SortedSet
        // Using foreach loop
        foreach(var val in my_Set1)
        {
            Console.WriteLine(val);
        }
 
        // Creating another SortedSet
        // using collection initializer
        // to initialize SortedSet
        SortedSet<int> my_Set2 = new SortedSet<int>() {
                                202,2002,20002,200002};
                 
        // Display elements of my_Set2
        Console.WriteLine("Elements of my_Set2:");
        foreach(var value in my_Set2)
        {
            Console.WriteLine(value);
        }
```

## **Stack**

**Стек** являє собою колекцію об'єктів за принципом «останнім прийшов — першим пішов» (**LIFO**). Використовується, коли вам потрібен доступ до елементів за принципом «останнім доданий — першим вилучений».

![image.png](assets/11-collections/image.png)

```csharp
  Stack my_stack = new Stack();
 
        // Adding elements in the Stack
        // Using Push method
        my_stack.Push("Geeks");
        my_stack.Push("geeksforgeeks");
        my_stack.Push("geeks23");
        my_stack.Push("GeeksforGeeks");
 
        Console.WriteLine("Total elements present in"+
                     " my_stack: {0}",my_stack.Count);
                                                    
        // Obtain the topmost element 
        // of my_stack Using Pop method
        Console.WriteLine("Topmost element of my_stack"
                          + " is: {0}",my_stack.Pop());
                           
        Console.WriteLine("Total elements present in"+
                    " my_stack: {0}", my_stack.Count);
                          
        // Obtain the topmost element 
        // of my_stack Using Peek method
        Console.WriteLine("Topmost element of my_stack "+
                              "is: {0}",my_stack.Peek());
                           
 
        Console.WriteLine("Total elements present "+
                 "in my_stack: {0}",my_stack.Count);
 
        // Accessing the elements
        // of my_stack Stack
        // Using foreach loop
        foreach(var elem in my_stack)
        {
            Console.WriteLine(elem);
        }
```

## Queue

**Черга (Queue)** являє собою колекцію об'єктів за принципом «першим прийшов — першим пішов» (**FIFO**). Використовується, коли вам потрібен доступ до елементів за принципом «першим доданий — першим вилучений». Додавання елемента в чергу називається **enqueue**, а вилучення елемента — **dequeue**.

```csharp
  Queue myQueue = new Queue(); 
  
        // Inserting the elements into the Queue 
        myQueue.Enqueue("Geeks"); 
        myQueue.Enqueue("Geeks Classes"); 
        myQueue.Enqueue("Noida"); 
        myQueue.Enqueue("Data Structures"); 
        myQueue.Enqueue("GeeksforGeeks"); 
  
        // Converting the Queue 
        // into object array 
        Object[] arr = myQueue.ToArray(); 
  
        // Displaying the elements in array 
        foreach(Object obj in arr) 
        { 
            Console.WriteLine(obj); 
        } 
```
