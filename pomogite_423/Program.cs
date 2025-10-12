using System;
using System.Collections.Generic;
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
    public void InitializeTestData()
    {
        AddBook(new Book("Лисья Нора", "Нора Сакавич", Genre.Fantasy, 2020, 550));
        AddBook(new Book("Лето в пионерском галстке", "Катерина Сильванова", Genre.Romance, 2017, 770));
        AddBook(new Book("Спеши любить", "Николас Спракс", Genre.Romance, 2019, 890));
        AddBook(new Book("Гордость и предубеждение", "Джейн Остин", Genre.Romance, 1813, 720m));
        AddBook(new Book("1984", "Джордж Оруэлл", Genre.ScienceFictio, 1949, 850));
    }
}
