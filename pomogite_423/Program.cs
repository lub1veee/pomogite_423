// See?нЕ ВИЖУ

using System;

Menu();
var choice = Console.ReadLine();
while (true)
{
    switch (choice)
    {
        case "1":
            break;
        case "2":
            break;
        case "3":
            return;
    }
}

static void Menu()
{
    Console.WriteLine("Что вы хотите сделать?");
    Console.WriteLine("1 - Анализ текста");
    Console.WriteLine("2 - Статистика ласт текстов");
    Console.WriteLine("3 - зеенд");
}