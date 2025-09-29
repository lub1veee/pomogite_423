// See?нЕ ВИЖУ

using System;

class Analiz
{ 
    static List<string> all = new List<string>();
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("1 - Анализ текста");
            Console.WriteLine("2 - Статистика ласт текстов");
            Console.WriteLine("3 - зеенд");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Anal1z();
                    break;
                case "2":
                    history();
                    break;
                case "3":
                    return;
            }
        }
    }
static void Anal1z()
{
    string Text;
    while (true)
    {
    Console.WriteLine("Введите текст:");
    Text = Console.ReadLine();
    if (Text != null && Text.Length >= 100) break;
     Console.WriteLine("Миннимум 100 символов");

    }
    int word = countw(Text);
    string korotkii = findWord(Text, true);
    string dlinii = findWord(Text, false);
    int predlzh = predlozh(Text);
    int glas = countGlas(Text);
    int sogl = countSogl(Text);
    string stat = getStat(Text);

    string result = $" Слов:{word}  Предложений:{predlzh}  Гласных:{glas}  Согласных: {sogl}  Короткое слоов:{korotkii}  Длинное:{dlinii}  Статистика: {stat}";
    all.Add(result);
    Console.WriteLine(result);

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

    static string findWord(string text, bool findShortest)
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
        }
        if (current.Length > 0)
        {
            if (result.Length == 0) result = current;
            else if (findShortest && current.Length < result.Length) result = current;
            else if (!findShortest && current.Length > result.Length) result = current;
        }

            return result.Length > 0 ? result : "нет";
    }
    static int predlozh(string text)
    {
        int count = 0;
        bool predlzh = false;
        foreach (var  p in text)
        {
            if (char.IsLetterOrDigit(p))
            {
               predlzh = true;
            }
            else if ((p == '.' || p == '!' || p == '?') && predlzh)
            {
                count++;
                predlzh = false;
            }
    }
    if (predlzh) count++;
    return count;
    }

    static int countGlas(string text)
    {
        int count = 0;
        string glas = "аеёиоуыэюяАЕЁИОУЫЭЮЯ";
        foreach (var g in text)
        {
        if(glas.IndexOf(g)>= 0) count++;
        }
        return count;
        
    }
    static int countSogl(string text)
    {
        int count = 0;

        foreach(char s in text)
        {
            if(char.IsLetter(s) && countGlas(s.ToString()) == 0) count++;
        }
        return count;
    }
    static string getStat(string text)
    {
        Dictionary<char, int> stats = new Dictionary<char, int>();

        foreach(char s in text)
        {
            char lower = char.ToLower(s);
            if(char.IsLetter(lower))
            {
                if(stats.ContainsKey(lower)) stats[lower]++;
                else stats[lower] = 1;
            }
        }
        List<char> letters = new List<char>(stats.Keys);
        letters.Sort();

        string result = "";
        foreach(char letter in letters)
        {
            result += $" '{letter}': {stats[letter]}";
        }
        return result.Length > 0 ? result : " не найдено";
    }
    static void history()
    {
        if(all.Count == 0)
        {
            Console.WriteLine("Истории неи");
            return;
        }

        for (int i = 0; i < all.Count; i++)
        {
            Console.WriteLine (all[i]);
        }
    }
}
