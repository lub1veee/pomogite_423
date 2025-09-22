using System;
using System.Collections.Generic;
using System.Linq;
/*Console.WriteLine("Введите код товара");
Console.WriteLine("Введите название товара ");
Console.WriteLine("Введите цену товара: ");
Console.WriteLine("Количество: ");
Console.WriteLine("Остался ли товар еще на складе");
*/

public enum Category
{
    Электроника,
    Одежда,
    Еда,
    Книги,
    Учеба
}
public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool InStock => Quantity > 0;
    public Category Category { get; set; }

    public override string ToString()
    {
        return $"{Code} | {Name} | {Price} руб. | {Quantity} шт. | {(InStock ? "В наличии" : "Нет в наличии")} | {Category}";
    }
}
class Program
{
    static List<Product> products = new List<Product>();
    static int nextId = 1001;

    static void Main(string[] args)
    {
        AddTestProducts();

        while (true)
        {
            ShowMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    RemoveProduct();
                    break;
                case "3":
                    OrderSupply();
                    break;
                case "4":
                    SellProduct();
                    break;
                case "5":
                    SearchProducts();
                    break;
                case "6":
                    //ShowAllProducts();
                    break;
                case "7":
                    Console.WriteLine("Выход из программы");
                    return;
                default:
                    Console.WriteLine("Кроме этого, ничего не может быть, ало, ты че");
                    break;
            }

            Console.WriteLine("Продолжить");
            Console.ReadLine();
            Console.Clear();
        }
    }
    static void ShowMenu()
    {
        Console.WriteLine("ПРИЛОЖЕНИЕ ДЛЯ УЧЁТА ТОВАРОВ В МАГАИЗНЕ");
        Console.WriteLine("Что вы хотите сделать?");
        Console.WriteLine("1 - добавить товар");
        Console.WriteLine("2 - удалить товар");
        Console.WriteLine("3 - заказать поставку товара");
        Console.WriteLine("4 - продать товар");
        Console.WriteLine("5 - поиск товаров");
        Console.WriteLine("6. Показать все товары");
        Console.WriteLine("7. Выход");
        Console.Write("Выберите действие: ");
    }
    static void AddTestProducts()
    {
        products.Add(new Product { Code = "11001", Name = "ПокоX3", Price = 1500, Quantity = 10, Category = Category.Электроника });
        products.Add(new Product { Code = "11002", Name = "Митенки", Price = 500, Quantity = 50, Category = Category.Одежда });
        products.Add(new Product { Code = "11003", Name = "Вкусный кофе, приготовленный мной", Price = 190, Quantity = 100, Category = Category.Еда });
        products.Add(new Product { Code = "11004", Name = "Лето в пионерском галстуке", Price = 400000, Quantity = 25, Category = Category.Книги });
        products.Add(new Product { Code = "11005", Name = "Книга по немецкому языку", Price = 1200, Quantity = 15, Category = Category.Учеба });
    }
    static void AddProduct()
    {
        Console.WriteLine("Добавление товара");

        Console.Write("Название товара: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Ало, название не может быть пустым");
            return;
        }
        Console.WriteLine("Цена:");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
        {
            Console.WriteLine("Ноу ноу, неверная цена:");
            return;
        }

        Console.WriteLine("Количество:");
        {
            Console.WriteLine("Откуда такие цифры, неверное количество");
            return;
        }

        Console.WriteLine("Категории: 0 - Электроника, 1 - Одежда, 2 - Еда, 3 - Книги, 4 - Учеба");
        Console.Write("Выберите категорию (0-4): ");
        if (!int.TryParse(Console.ReadLine(), out int categoryNum) || categoryNum < 0 || categoryNum > 4)
        {
            Console.WriteLine("Неверная категория");
            return;
        }
        Category category = (Category)categoryNum;
        string code = "1" + nextId++;

        products.Add(new Product { Code = code, Name = name, Price = price, Quantity = quantity, Category = category });
        Console.WriteLine($"Товар добавлен, код: {code}");
    }

    static void RemoveProduct()
    {
        Console.WriteLine("Удаление товара");
        Console.Write("Введите код товара: ");
        string code = Console.ReadLine();

        var product = products.FirstOrDefault(p => p.Code == code);
        if (product != null)
        {
            products.Remove(product);
            Console.WriteLine("Товар удален!");
        }
        else
        {
            Console.WriteLine("Товар не найден!");
        }
    }
    static void OrderSupply()
    {
        Console.WriteLine("Поставка товаров");
        Console.Write("Введите код товара: ");
        string code = Console.ReadLine();

        var product = products.FirstOrDefault(p => p.Code == code);
        if (product == null)
        {
            Console.WriteLine("Товар не найден");
            return;
        }

        Console.Write("Количество для поставки: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Неверное количество");
            return;
        }

        product.Quantity += quantity;
        Console.WriteLine($"Все круто супер чудно, новое количество: {product.Quantity}");
    }

    static void SellProduct()
    {
        Console.WriteLine("Продажа товара");
        Console.WriteLine("Введите код товара:");
        string code = Console.ReadLine(); 
        var product = products.FirstOrDefault(p => p.Code == code);
        if (product == null)
        {
            Console.WriteLine("Товара нет, гуляй");
            return;
        }

        Console.WriteLine("Количество для продажи:");
        if (!int.TryParse (Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Та откуда такое количество, неверно");
            return;
        }

        if (product.Quantity >= quantity)
        {
            product.Quantity -= quantity;
            decimal total = product.Price * quantity;
            Console.WriteLine($"Чудно, продано. Остаток: {product.Quantity}, Сумма: {total} руб.");
        }
        {
            Console.WriteLine($"Недостаточно товара, доступно: {product.Quantity}");
        }
    }

    static void SearchProducts()
    {
        Console.WriteLine("Поиск товаров");
        Console.WriteLine("1 - По коду");
        Console.WriteLine("2 - По названию");
        Console.WriteLine("3 - По категории");
        Console.Write("Выберите тип поиска: ");

        string choice = Console.ReadLine();
        List<Product> results = new List<Product>();

        switch (choice)
        {
            case "1":
                Console.Write("Введите код: ");
                string code = Console.ReadLine();
                results = products.Where(p => p.Code == code).ToList();
                break;

            case "2":
                Console.Write("Введите название: ");
                string name = Console.ReadLine();
                results = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                break;

            case "3":
                Console.WriteLine("Категории: 0 - Электроника, 1 - Одежда, 2 - Еда, 3 - Книги, 4 - Учеба");
                Console.Write("Выберите категорию: ");
                if (int.TryParse(Console.ReadLine(), out int catNum) && catNum >= 0 && catNum <= 4)
                {
                    results = products.Where(p => p.Category == (Category)catNum).ToList();
                }
                break;

            default:
                Console.WriteLine("Нет, пока, неверно");
                return;
        }

        if (results.Count > 0)
        {
            Console.WriteLine("Найдены товары:");
            foreach (var product in results)
            {
                Console.WriteLine(product);
            }
        }
        else
        {
            Console.WriteLine("Товары не найдены");
        }
    }


}










