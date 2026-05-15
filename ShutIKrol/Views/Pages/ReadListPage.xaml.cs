using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace ShutIKrol.Views.Pages
{
    public partial class ReadListPage : Page
    {
        private int _currentStatusId = 1;
        private List<BookViewModel> _allBooks = new List<BookViewModel>();

        public ReadListPage()
        {
            InitializeComponent();

            CmbSort.SelectionChanged += CmbSort_SelectionChanged;
            CmbGenre.SelectionChanged +=  CmbGenre_SelectionChanged;
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
            CmbGenre.Items.Add(new ComboBoxItem { Content = "Все жанры", IsSelected = true, Tag = 0 });
            foreach (var g in db.Genres.ToList())
                CmbGenre.Items.Add(new ComboBoxItem { Content = g.Name, Tag = g.Id });
        }

        private void LoadBooks()
        {
            var db = Core.Context;
            var userId = Session.CurrentUser.Id;

            var items = db.ReadList
                .Include(rl => rl.Books.Users)
                .Include(rl => rl.Books.Reviews)
                .Include(rl => rl.Books.Genres)
                .Where(rl => rl.UserId == userId && rl.StatusId == _currentStatusId)
                .ToList();

            _allBooks = items.Select(rl => new BookViewModel
            {
                Id = rl.Books.Id,
                Name = rl.Books.Name,
                CoverPath = rl.Books.CoverPath,
                AuthorName = rl.Books.Users?.Name ?? string.Empty,
                AvgRating = rl.Books.Reviews.Any() ? $"Оценка: {rl.Books.Reviews.Average(r => r.Rate):F1}" : "Нет оценок",
                Genres = rl.Books.Genres.Select(g => g.Id).ToList()
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

        private void BtnSwitchList_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tagStr && int.TryParse(tagStr, out int statusId))
            {
                _currentStatusId = statusId;
                LoadBooks();
            }
        }

        private void Filter_Changed(object sender, object e) => ApplyFilters();

        private void BtnOpenBook_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
                NavigationService?.Navigate(new BookPage(id));
        }

        private void BtnMove_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var dlg = new AddToListDialog(id);
                dlg.ShowDialog();
                LoadBooks();
            }
        }
        private void CmbGenre_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilters();
    }
}
