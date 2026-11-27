using System.Diagnostics;
using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("----- Главное меню -----");
            Console.WriteLine("1. Отгадай ответ");
            Console.WriteLine("2. Об авторе");
            Console.WriteLine("3. Сортировка массива");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите пункт меню: ");

            string menu = Console.ReadLine();
            Console.Clear();

            switch (menu)
            {
                case "1":
                    Answer();
                    break;
                case "2":
                    Avtor();
                    break;
                case "3":
                    SortMenu();
                    break;
                case "4":
                    if (Leave()) exit = true;
                    break;
                default:
                    Console.WriteLine("Ошибка: неверный выбор. Введите 1, 2, 3 или 4.\n");
                    break;
            }
        }
    }

    // ================= ПУНКТ 1: ОТГАДАЙ ОТВЕТ =================
    static void Answer()
    {
        double a = ReadDoubleA("Введите значение для первого числа 'a' (в градусах): ");
        double b = ReadDoubleB("Введите значение для второго числа 'b': ", a);

        double correctAns = CalculateFunction(a, b);
        Console.WriteLine($"\nРезультат: {correctAns:F4}");

        GuessingGame(correctAns);

        Console.WriteLine("\nНажмите Enter для возврата в меню...");
        Console.ReadLine();
        Console.Clear();
    }

    // Отдельный метод для вычисления значения функции
    static double CalculateFunction(double a, double b)
    {
        double zRad = a * (Math.PI / 180.0);
        double funkchiya = Math.Sqrt(b + Math.Sin(zRad)) / Math.Pow(Math.Cos(zRad), 3);
        return Math.Log(funkchiya, b);
    }

    // Отдельный метод для логики игры "угадайка"
    static void GuessingGame(double correctAns)
    {
        int popitki = 3;
        bool guessed = false;

        while (popitki > 0 && !guessed)
        {
            Console.Write($"У вас осталось {popitki} попыток. Введите ваш ответ: ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out double userAns))
            {
                if (Math.Round(userAns, 2) == Math.Round(correctAns, 2))
                {
                    Console.WriteLine("\nПоздравляем! Вы угадали правильный ответ!\n");
                    guessed = true;
                }
                else
                {
                    popitki--;
                    if (popitki > 0) Console.WriteLine($"\nНеверный ответ. Осталось попыток: {popitki}.\n");
                }
            }
            else
            {
                popitki--;
                if (popitki > 0) Console.WriteLine($"\nОшибка: введите корректное число. Осталось попыток: {popitki}.\n");
            }
        }

        if (!guessed)
            Console.WriteLine($"\nК сожалению, вы проиграли. Правильный ответ: {Math.Round(correctAns, 2)}\n");
    }

    // ================= ПУНКТ 2: ОБ АВТОРЕ =================
    static void Avtor()
    {
        Console.WriteLine("--- Об авторе ---");
        Console.WriteLine("ФИО: Мамонтов Илья Алексеевич");
        Console.WriteLine("Группа: 6105-090301D\n");
        Console.WriteLine("Нажмите Enter для возврата в меню...");
        Console.ReadLine();
        Console.Clear();
    }

    // ================= ПУНКТ 3: СОРТИРОВКА МАССИВА =================
    static void SortMenu()
    {
        Console.WriteLine("--- Сортировка массива ---");

        int n = ReadArrayLength("Введите размер массива n: ");
        int[] original = CreateAndFillArray(n);

        Console.WriteLine("\nИсходный массив:");
        PrintArray(original);

        // Клонируем исходный массив для двух сортировок
        int[] arrayShell = CopyArray(original);
        int[] arraySelection = CopyArray(original);
        // Сортировка Шелла с замером времени
        Stopwatch sw1 = Stopwatch.StartNew();
        int[] sortedShell = ShellSort(arrayShell);
        sw1.Stop();

        // Сортировка выбором с замером времени
        Stopwatch sw2 = Stopwatch.StartNew();
        int[] sortedSelection = SelectionSort(arraySelection);
        sw2.Stop();

        Console.WriteLine("\nОтсортированный массив (Шелл):");
        PrintArray(sortedShell);

        Console.WriteLine("\nОтсортированный массив (Выбором):");
        PrintArray(sortedSelection);

        Console.WriteLine($"\nВремя сортировки Шелла: {sw1.Elapsed.TotalMilliseconds:F4} мс");
        Console.WriteLine($"Время сортировки выбором: {sw2.Elapsed.TotalMilliseconds:F4} мс");

        Console.WriteLine("\nНажмите Enter для возврата в меню...");
        Console.ReadLine();
        Console.Clear();
    }

    // Ввод длины массива с проверкой > 0
    static int ReadArrayLength(string prompt)
    {
        int length = 0;
        bool isValid = false;

        while (!isValid)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out length) && length > 0)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Ошибка: длина массива должна быть целым числом больше 0.\n");
            }
        }
        return length;
    }

    // Создание и инициализация массива случайными значениями
    static int[] CreateAndFillArray(int length)
    {
        int[] arr = new int[length];
        Random rnd = new Random();

        for (int i = 0; i < length; i++)
        {
            arr[i] = rnd.Next(-100, 101); // диапазон [-100; 100]
        }
        return arr;
    }

    // Создание копии массива
    static int[] CopyArray(int[] source)
    {
        int[] copy = new int[source.Length];
        for (int i = 0; i < source.Length; i++)
        {
            copy[i] = source[i];
        }
        return copy;
    }

    // Вывод массива на экран с проверкой длины
    static void PrintArray(int[] arr)
    {
        if (arr.Length > 10)
        {
            Console.WriteLine("Массивы не могут быть выведены на экран, так как длина массива больше 10");
            return;
        }

        Console.Write("[ ");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i]);
            if (i < arr.Length - 1) Console.Write(", ");
        }
        Console.WriteLine(" ]");
    }

    // Сортировка Шелла
    static int[] ShellSort(int[] arr)
    {
        int n = arr.Length;
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i++)
            {
                int temp = arr[i];
                int j;
                for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                {
                    arr[j] = arr[j - gap];
                }
                arr[j] = temp;
            }
        }
        return arr;
    }

    // Сортировка выбором
    static int[] SelectionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                int temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;
            }
        }
        return arr;
    }

    // ================= ПУНКТ 4: ВЫХОД =================
    static bool Leave()
    {
        bool done = false;
        bool exitProgram = false;

        while (!done)
        {
            Console.Write("Вы уверены, что хотите выйти? (д/н): ");
            string input = Console.ReadLine();
            if (input == "д")
            {
                exitProgram = true;
                done = true;
            }
            else if (input == "н")
            {
                done = true;
                Console.Clear();
            }
            else
            {
                Console.WriteLine("\nОшибка: введите 'д' для выхода или 'н' для отмены.\n");
            }
        }
        return exitProgram;
    }

    // ================= МЕТОДЫ ВВОДА С КОНТРОЛЕМ =================
    static double ReadDoubleA(string prompt)
    {
        bool isValid = false;
        double result = 0;

        while (!isValid)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double temp))
            {
                double cosZ = Math.Cos(temp * (Math.PI / 180.0));
                if (cosZ <= 1e-10)
                {
                    Console.WriteLine("\nОшибка: cos(a) <= 0, что недопустимо.\n");
                }
                else
                {
                    result = temp;
                    isValid = true;
                }
            }
            else
            {
                Console.WriteLine("\nОшибка: введите корректное число.\n");
            }
        }
        return result;
    }

    static double ReadDoubleB(string prompt, double number1Degrees)
    {
        bool isValid = false;
        double result = 0;
        double sinZ = Math.Sin(number1Degrees * (Math.PI / 180.0));

        while (!isValid)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double temp))
            {
                string error = temp <= 0 ? "Основание логарифма (b) должно быть > 0."
                    : Math.Abs(temp - 1.0) < 1e-10 ? "Основание логарифма (b) не может быть равно 1."
                    : temp + sinZ <= 0 ? "Выражение под корнем (b + sin(a)) должно быть > 0."
                    : null;

                if (error != null)
                {
                    Console.WriteLine("Ошибка: " + error);
                }
                else
                {
                    result = temp;
                    isValid = true;
                }
            }
            else
            {
                Console.WriteLine("Ошибка: введите корректное число.");
            }
        }
        return result;
    }
}