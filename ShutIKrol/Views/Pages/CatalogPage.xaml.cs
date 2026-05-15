using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace ShutIKrol.Views.Pages
{
    public partial class CatalogPage : Page
    {
        private List<BookViewModel> _allBooks = new List<BookViewModel>();

        public CatalogPage()
        {
            InitializeComponent();

            CmbSort.SelectionChanged += CmbSort_SelectionChanged;
            CmbGenre.SelectionChanged += CmbGenre_SelectionChanged;
            TxtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += (s,e) =>
            {
                LoadGenres();
                LoadBooks();
            };
        }

        private void LoadGenres()
        {
            var db = Core.Context;
            var genres = db.Genres.ToList();
            CmbGenre.Items.Add(new ComboBoxItem { Content = "Все жанры", IsSelected = true, Tag = 0 });
            foreach (var g in genres)
                CmbGenre.Items.Add(new ComboBoxItem { Content = g.Name, Tag = g.Id });
        }

        private void LoadBooks()
        {
            var db = Core.Context;
            var books = db.Books
                .Include(b => b.Users)
                .Include(b => b.Reviews)
                .Include(b => b.Genres)
                .Where(b => !b.IsFrozen)
                .ToList();

            _allBooks = books.Select(b => new BookViewModel
            {
                Id = b.Id,
                Name = b.Name,
                CoverPath = b.CoverPath,
                AuthorName = b.Users.Name,
                AvgRating = b.Reviews.Any() ? $"Оценка: {b.Reviews.Average(r => r.Rate):F1}" : "Нет оценок",
                Genres = b.Genres.Select(g => g.Id).ToList()
            }).ToList();

            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (BooksPanel == null)
                return;

            var search = TxtSearch.Text.Trim().ToLower();
            var result = _allBooks.AsEnumerable();

            if (!string.IsNullOrEmpty(search))
                result = result.Where(b => b.Name.ToLower().Contains(search) || b.AuthorName.ToLower().Contains(search));

            if (CmbGenre?.SelectedItem is ComboBoxItem ci && ci.Tag is int gid && gid > 0)
                result = result.Where(b => b.Genres.Contains(gid));

            if (CmbSort.SelectedIndex == 1)
                result = result.OrderByDescending(b => b.AvgRating);
            else
                result = result.OrderBy(b => b.Name);

            BooksPanel.ItemsSource = result?.ToList();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchHint.Visibility = string.IsNullOrEmpty(TxtSearch.Text) ? Visibility.Visible : Visibility.Collapsed;
            ApplyFilters();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();
        private void CmbGenre_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();

        private void BtnOpenBook_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var page = new BookPage(id);
                NavigationService?.Navigate(page);
            }
        }

        private void BtnAddToList_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var dlg = new AddToListDialog(id);
                dlg.ShowDialog();
            }
        }
    }

    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CoverPath { get; set; }
        public string AuthorName { get; set; } = "";
        public string AvgRating { get; set; } = "";
        public List<int> Genres { get; set; } = new List<int>();
    }
}
