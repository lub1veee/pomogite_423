using Microsoft.Win32;
using ShutIKrol.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Data.Entity;

namespace ShutIKrol.Views.Pages
{
    public partial class EditBookPage : Page
    {
        private readonly int? _bookId;
        private ObservableCollection<ChapterEditVM> _chapters = new ObservableCollection<ChapterEditVM>();

        public EditBookPage(int? bookId)
        {
            InitializeComponent();
            _bookId = bookId;
            ChaptersList.ItemsSource = _chapters;
            LoadGenres();
            if (bookId.HasValue)
                LoadBook(bookId.Value);
            else
                TxtPageTitle.Text = "Добавить книгу";
        }

        private void LoadGenres()
        {
            var db = Core.Context;
            GenresList.ItemsSource = db.Genres.ToList();
            GenresList.DisplayMemberPath = "Name";
        }

        private void LoadBook(int id)
        {
            TxtPageTitle.Text = "Редактировать книгу";
            var db = Core.Context;
            var book = db.Books.Include(b => b.Genres).Include(b => b.Chapters).FirstOrDefault(b => b.Id == id);
            if (book == null) return;

            TxtName.Text = book.Name;
            TxtCoverPath.Text = book.CoverPath ?? "";

            foreach (ListBoxItem item in GenresList.Items)
            {
                if (item == null) continue;
            }
            // Select genres
            foreach (var genre in book.Genres)
            {
                foreach (var item in GenresList.Items)
                {
                    if (item is Genres g && g.Id == genre.Id)
                        GenresList.SelectedItems.Add(item);
                }
            }

            int num = 1;
            foreach (var ch in book.Chapters.OrderBy(c => c.Number))
            {
                _chapters.Add(new ChapterEditVM { Number = num++, Name = ch.Name, Path = ch.Path, ExistingId = ch.Id });
            }
        }

        private void BtnBrowseCover_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp" };
            if (dlg.ShowDialog() == true)
                TxtCoverPath.Text = dlg.FileName;
        }

        private void BtnAddChapter_Click(object sender, RoutedEventArgs e)
        {
            _chapters.Add(new ChapterEditVM { Number = _chapters.Count + 1, Name = $"Глава {_chapters.Count + 1}", Path = "" });
        }

        private void BtnRemoveChapter_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int num)
            {
                var ch = _chapters.FirstOrDefault(c => c.Number == num);
                if (ch != null) _chapters.Remove(ch);
                // Renumber
                for (int i = 0; i < _chapters.Count; i++)
                {
                    _chapters[i].Number = i + 1;
                    _chapters[i].Label = $"Гл. {i + 1}:";
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                MessageBox.Show("Введите название книги.");
                return;
            }

            var db = Core.Context;

            Books book;
            if (_bookId.HasValue)
            {
                book = db.Books.Include(b => b.Genres).Include(b => b.Chapters).First(b => b.Id == _bookId.Value);
                book.Genres.Clear();
            }
            else
            {
                book = new Books { AuthorId = Session.CurrentUser.Id };
                db.Books.Add(book);
            }

            book.Name = TxtName.Text;
            book.CoverPath = string.IsNullOrWhiteSpace(TxtCoverPath.Text) ? null : TxtCoverPath.Text;

            // Genres
            foreach (var selectedItem in GenresList.SelectedItems)
            {
                if (selectedItem is Genres g)
                {
                    var genre = db.Genres.Find(g.Id);
                    if (genre != null) book.Genres.Add(genre);
                }
            }

            db.SaveChanges();

            // Chapters - remove old, add new
            if (_bookId.HasValue)
            {
                var oldChapters = db.Chapters.Where(c => c.BookId == _bookId.Value).ToList();
                db.Chapters.RemoveRange(oldChapters);
            }

            int num = 1;
            foreach (var ch in _chapters)
            {
                db.Chapters.Add(new Chapters
                {
                    BookId = book.Id,
                    Number = num++,
                    Name = ch.Name,
                    Path = ch.Path
                });
            }

            db.SaveChanges();
            MessageBox.Show("Книга сохранена!");
            NavigationService?.GoBack();
        }
    }

    public class ChapterEditVM : INotifyPropertyChanged
    {
        private int _number;
        private string _name = "";
        private string _path = "";
        private string _label = "";

        public int? ExistingId { get; set; }

        public int Number
        {
            get => _number;
            set { _number = value; Label = $"Гл. {value}:"; OnPropertyChanged(nameof(Number)); }
        }
        public string Label
        {
            get => _label;
            set { _label = value; OnPropertyChanged(nameof(Label)); }
        }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string Path
        {
            get => _path;
            set { _path = value; OnPropertyChanged(nameof(Path)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
