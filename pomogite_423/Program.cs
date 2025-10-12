using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

public enum Genre
{
    Fantasy,
    Romance,
    Mystery,
    Thriller,
    ScienceFictio
}

public class Book
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public Genre Genre { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }

    public Book(string title, string author, Genre genre, int year, decimal price)
    {
        Id = _nextId++;
        Title = title;
        Author = author;
        Genre = genre;
        Year = year;
        Price = price;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Название: {Title}, Автор: {Author}, Жанр: {Genre}, Год: {Year}, Цена: {Price:C}";
    }
}

public class Book
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public Genre Genre { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }

    public Book(string title, string author, Genre genre, int year, decimal price)
    {
        Id = _nextId++;
        Title = title;
        Author = author;
        Genre = genre;
        Year = year;
        Price = price;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Название: \"{Title}\", Автор: {Author}, Жанр: {Genre}, Год: {Year}, Цена: {Price:C}";
    }
}

public class Library
{
    private List<Book> _books = new List<Book>();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public bool RemoveBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            _books.Remove(book);
            return true;
        }
        return false;
    }

    public List<Book> GetAllBooks()
    {
        return new List<Book>(_books);
    }

    public List<Book> FindBooksByTitle(string title)
    {
        return _books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    public List<Book> FindBooksByAuthor(string author)
    {
        return _books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    public List<Book> FindBooksByGenre(Genre genre)
    {
        return _books.Where(b => b.Genre == genre).ToList();
    }
    public void InitializeTestData()
    {
        AddBook(new Book("Лисья Нора", "Нора Сакавич", Genre.Fantasy, 2020, 550));
        AddBook(new Book("Лето в пионерском галстке", "Катерина Сильванова", Genre.Romance, 2017, 770));
        AddBook(new Book("Спеши любить", "Николас Спракс", Genre.Romance, 2019, 890));
        AddBook(new Book("Гордость и предубеждение", "Джейн Остин", Genre.Romance, 1813, 720m));
        AddBook(new Book("1984", "Джордж Оруэлл", Genre.ScienceFictio, 1949, 850));
    }
}
class programm
{


    private static Library _library = new Library();

    static void Main(string[] args)
    {
        _library.InitializeTestData();

        while (true)
        {
            Console.Clear();
            Menu();
            var choice = GetUserChoice();
            ProcessChoice(choice);
        }
    }
    static void Menu()
    {
        Console.WriteLine("СИСТЕМА УПРАВЛЕНИЯ БИБЛИОТЕКОЙ");
        Console.WriteLine("1. Добавить книгу");
        Console.WriteLine("2. Удалить книгу по ID");
        Console.WriteLine("3. Найти книги");
        Console.WriteLine("4. Сортировать книги");
        Console.WriteLine("5. Самая дорогая и дешёвая книга");
        Console.WriteLine("6. Группировка книг по авторам");
        Console.WriteLine("7. Показать все книги");
        Console.WriteLine("0. Выход");
        Console.Write("Выберите действие: ");
    }
    static int GetUserChoice()
    {
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            return choice;
        }
        return -1;
    }

    static void ProcessChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                AddBook();
                break;
            case 2:
                RemoveBook();
                break;
            case 3:
                SearchBooks();
                break;
            case 4:
                SortBooks();
                break;
            case 5:
                ShowPriceExtremes();
                break;
            case 6:
                GroupBooksByAuthor();
                break;
            case 7:
                ShowAllBooks();
                break;
            case 0:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Неверный выбор! Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                break;
        }
    }
    static void AddBook()
    {
        Console.Clear();
        Console.WriteLine("ДОБАВЛЕНИЕ НОВОЙ КНИГИ");
        Console.ReadKey();
    }
    static void RemoveBook()
    {
        Console.Clear();
        Console.WriteLine("УДАЛЕНИЕ КНИГИ");

            