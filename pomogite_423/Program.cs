using System;

Console.WriteLine("Сколько операций (от 2 до 40");
int n = Convert.ToInt32(Console.ReadLine());

string[] products = new string[n];
int[] money = new int[n];

Console.WriteLine("УЧЕТ ЕЖЕДНЕВНЫХ РАСХОДОВ");
Console.WriteLine("Введите название товара или услуги и их цену: ");

for (int i = 0; i < n; i++)
{
    Console.WriteLine($"{i + 1}.");
    string[] stroka = Console.ReadLine().Split(";");
    products[i] = stroka[0].Trim();
    money[i] = int.Parse(stroka[1].Trim());
}

void menu() 
{ 

Console.WriteLine("Меню:");
Console.WriteLine("1 - Вывод данных");
Console.WriteLine("2 - Статистика");
Console.WriteLine("3 - Сортировка по цене");
Console.WriteLine("4 - Конвертация валюты");
Console.WriteLine("5 - Поиск по названию");
Console.WriteLine("0 - Выход");

int choice = Convert.ToInt32(Console.ReadLine());
    switch (choice)
    {
        case 1:
            Console.WriteLine("Вы выбрали 1 опцию - Вывод данных");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Название: {products[i]}, сумма:{money[i]}");
            }
            break;
        case 2:
            Console.WriteLine("Вы выбрали 2 опцию - Статистика");
            if (n > 0)
            {
                int sum = 0;
                int min = money[0];
                int max = money[0];
                for (int i = 0; i < n; i++)
                {
                    sum += money[i];
                    if (money[i] < min) min = money[i];
                    if (money[i] > max) max = money[i];

                }
                double average = (double)sum / n;
                Console.WriteLine($"\nСумма: {sum} руб.");
                Console.WriteLine($"Среднее: {average} руб.");
                Console.WriteLine($"Максимум: {max} руб.");
                Console.WriteLine($"Минимум: {min} руб.");
            }
            break;
        case 3:
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (money[j] > money[j + 1])
                    {
                        int tempM = money[j];
                        money[j] = money[j + 1];
                        money[j + 1] = tempM;

                        string tempN = products[j];
                        products[j] = products[j + 1];
                        products[j + 1] = tempN;
                    }
            Console.WriteLine("Отсортировано!");
            break;
        case 4:
            Console.Write("Курс (рублей за 1 валюту): ");
            decimal rate = decimal.Parse(Console.ReadLine());
            Console.Write("Символ валюты: ");
            string sym = Console.ReadLine();

            Console.WriteLine($"\nВ {sym}:");
            foreach (decimal m in money)
                Console.WriteLine($"{m / rate} {sym}");
            break;
        case 5:
            Console.Write("Искать: ");
            string search = Console.ReadLine().ToLower();
            bool found = false;

            for (int i = 0; i < n; i++)
                if (products[i].ToLower().Contains(search))
                {
                    Console.WriteLine($"{products[i]} - {money[i]} руб.");
                    found = true;
                }

            if (!found) Console.WriteLine("Не найдено");
            break;
        case 0:
            return;
            //4
    }
    menu();
}
menu();

