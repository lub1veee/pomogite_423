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


static void Anal1z()
{
    string Text;
    Console.WriteLine("Введите текст:");
    Text = Console.ReadLine();
    if (Text.Length >= 100) break;
    Console.WriteLine("Миннимум 100 символов");
}

static int countw(string text)
{
    int count = 0;
    bool word = false;
    foreach (char t in text)
    {
        if (char.IsLetterOrDigit(t))
        {


            if (!word)
            {
                count++;
                word = true;
            }
        }
        else { word = false; }
    }
    return count;
}

static string korotkii(string text, bool findShortest)
{
    string result = "";
    string current = "";

    foreach (char c in text)
    {
        if (char.IsLetterOrDigit(c))
        {
            current += c;
        }
        else
        {
            if (current.Length > 0)
            {
                if (result.Length == 0) result = current;
                else if (findShortest && current.Length < result.Length) result = current;
                else if (!findShortest && current.Length > result.Length) result = current;
            }
            current = "";
        }
        if (current.Length > 0)
        {
            if (result.Length == 0) result = current;
            else if (findShortest && current.Length < result.Length) result = current;
            else if (!findShortest && current.Length > result.Length) result = current;
        }

        return result.Length > 0 ? result : "нет";
    }
}