using ShutIKrol.Database;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShutIKrol.Views.Pages
{
    public partial class AuthorPage : Page
    {
        public AuthorPage()
        {
            InitializeComponent();
            LoadBooks();
        }

        private void LoadBooks()
        {
            var db = Core.Context;
            var userId = Session.CurrentUser.Id;
            var showFrozen = ChkShowFrozen.IsChecked == true;

            var books = db.Books
                .Where(b => b.AuthorId == userId && (showFrozen || !b.IsFrozen))
                .ToList();

            BooksList.ItemsSource = books.Select(b => new AuthorBookVM
            {
                Id = b.Id,
                Name = b.Name,
                CoverPath = b.CoverPath,
                StatusText = b.IsFrozen ? "Заморожена" : "Активна",
                StatusColor = b.IsFrozen ? Brushes.Red : Brushes.Green,
                AppealVisibility = b.IsFrozen ? Visibility.Visible : Visibility.Collapsed
            }).ToList();
        }

        private void BtnAddBook_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new EditBookPage(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
                NavigationService?.Navigate(new EditBookPage(id));
        }

        private void BtnAppeal_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int bookId)
            {
                var db = Core.Context;
                var freezeType = db.RequestTypes.FirstOrDefault(rt => rt.TypeName == "Снятие заморозки книги");
                if (freezeType == null)
                {
                    freezeType = new RequestTypes { TypeName = "Снятие заморозки книги" };
                    db.RequestTypes.Add(freezeType);
                    db.SaveChanges();
                }

                db.Requests.Add(new Requests
                {
                    TypeId = freezeType.Id,
                    UserId = Session.CurrentUser.Id,
                    Comment = $"Прошу снять заморозку с книги ID:{bookId}",
                    IsApproved = null
                });
                db.SaveChanges();
                MessageBox.Show("Заявка на снятие заморозки книги отправлена!");
            }
        }

        private void ChkShowFrozen_Changed(object sender, RoutedEventArgs e) => LoadBooks();
    }

    public class AuthorBookVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CoverPath { get; set; }
        public string StatusText { get; set; } = "";
        public Brush StatusColor { get; set; } = Brushes.Black;
        public Visibility AppealVisibility { get; set; }
    }
}
