using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System;
using ShutIKrol.Database;

namespace ShutIKrol.Views.Pages
{
    public partial class ReadListPage : Page
    {
        private int _currentStatusId;
        private List<BookViewModel> _allBooks = new List<BookViewModel>();
        private List<Button> _btns;

        public ReadListPage()
        {
            InitializeComponent();

            _btns = new List<Button> { b1, b2, b3, b4 };

            var statuses = Core.Context.ReadStatuses
                .Where(rs => rs.Name != "")
                .ToList();

            for (int i = 0; i < statuses.Count && i < _btns.Count; i++)
            {
                _btns[i].Content = statuses[i].Name;
                _btns[i].Tag = statuses[i].Id;
            }

            _currentStatusId = statuses.FirstOrDefault()?.Id ?? 0;

            CmbSort.SelectionChanged += CmbSort_SelectionChanged;
            CmbGenre.SelectionChanged += CmbGenre_SelectionChanged;
            TxtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += (s, e) =>
            {
                LoadGenres();
                LoadBooks();
            };
        }

        private void LoadGenres()
        {
            if (CmbGenre.Items.Count > 0)
                return;

            CmbGenre.Items.Add(new ComboBoxItem { Content = "Все жанры", IsSelected = true, Tag = 0 });
            foreach (var g in Core.Context.Genres.ToList())
                CmbGenre.Items.Add(new ComboBoxItem { Content = g.Name, Tag = g.Id });
        }

        private void LoadBooks()
        {
            var books = Core.Context.Books
                .Include(b => b.Users)
                .Include(b => b.Reviews)
                .Include(b => b.Genres)
                .Include(b => b.ReadList)
                .Where(b => b.ReadList.Any(rl => rl.UserId == Session.CurrentUser.Id))
                .ToList();

            _allBooks = books.Select(b => new BookViewModel(b)).ToList();

            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (BooksPanel == null)
                return;

            var result = _allBooks.AsEnumerable();

            var search = TxtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(search))
                result = result.Where(b =>
                    b.Name.ToLower().Contains(search) ||
                    b.AuthorName.ToLower().Contains(search));

            if (CmbGenre?.SelectedItem is ComboBoxItem ci && ci.Tag is int gid && gid > 0)
                result = result.Where(b => b.Genres.Contains(gid));

            if (_currentStatusId > 0)
                result = result.Where(b => b.StatusId == _currentStatusId);

            result = CmbSort.SelectedIndex == 1
                ? result.OrderByDescending(b => b.AvgRating)
                : result.OrderBy(b => b.Name);

            BooksPanel.ItemsSource = result.ToList();
        }

        private void BtnSwitchList_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int statusId)
            {
                _currentStatusId = statusId;
                ApplyFilters();
            }
        }

        private void BtnOpenBook_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
                NavigationService?.Navigate(new BookPage(id));
        }

        private void BtnMove_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                new AddToListDialog(id).ShowDialog();
                LoadBooks();
            }
        }

        private void CmbGenre_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();
        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilters();
    }
}