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
    Спорт
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
        // Добавляем тестовые товары
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
                    ShowAllProducts();
                    break;
                case "7":
                    Console.WriteLine("Выход из программы...");
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }

            Console.WriteLine("Нажмите Enter чтобы продолжить...");
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
        Console.WriteLine("4 - продать товар);
        Console.WriteLine("5 - поиск товаров");
        Console.WriteLine("6. Показать все товары");
        Console.WriteLine("7. Выход");
        Console.Write("Выберите действие: ");
    }
//4






