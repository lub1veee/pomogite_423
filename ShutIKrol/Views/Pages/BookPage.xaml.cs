using ShutIKrol.Database;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.Entity;

namespace ShutIKrol.Views.Pages
{
    public partial class BookPage : Page
    {
        private readonly int _bookId;
        private Books _book;

        public BookPage(int bookId)
        {
            InitializeComponent();
            _bookId = bookId;
            LoadBook();
        }

        private void LoadBook()
        {
            var db = Core.Context;
            _book = db.Books
                .Include(b => b.Users)
                .Include(b => b.Reviews.Select(r => r.Users))
                .Include(b => b.Genres)
                .FirstOrDefault(b => b.Id == _bookId);

            if (_book == null) return;

            TxtTitle.Text = _book.Name;
            TxtAuthor.Text = $"Автор: {_book.Users.Name}";
            TxtGenres.Text = "Жанры: " + string.Join(", ", _book.Genres.Select(g => g.Name));

            if (!string.IsNullOrEmpty(_book.CoverPath) && File.Exists(_book.CoverPath))
            {
                var bmp = new BitmapImage(new Uri(_book.CoverPath, UriKind.Absolute));
                ImgCover.Source = bmp;
            }

            if (Session.IsAdmin)
                BtnFreezeBook.Visibility = Visibility.Visible;

            var reviews = _book.Reviews.Where(r => r.IsFrozen == null || r.IsFrozen == false).Select(r => new ReviewViewModel
            {
                Id = r.Id,
                UserName = r.Users.Name,
                Rate = r.Rate,
                Text = r.Text,
                Date = r.CreationDate.ToString("dd.MM.yyyy"),
                FreezeVisibility = Session.IsAdmin ? Visibility.Visible : Visibility.Collapsed
            }).ToList();

            ReviewsList.ItemsSource = reviews;
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            var db = Core.Context;
            var chapters = db.Chapters.Where(c => c.BookId == _bookId).OrderBy(c => c.Number).ToList();
            if (!chapters.Any())
            {
                MessageBox.Show("У этой книги нет глав.", "Читать");
                return;
            }
            var readPage = new ReadBookPage(_bookId);
            NavigationService?.Navigate(readPage);
        }

        private void BtnComplainBook_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ComplaintDialog(Session.CurrentUser.Id, _bookId, null);
            dlg.ShowDialog();
        }

        private void BtnComplainAuthor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Жалоба на автора отправлена.", "Жалоба");
        }

        private void BtnFreezeBook_Click(object sender, RoutedEventArgs e)
        {
            var db = Core.Context;
            var book = db.Books.Find(_bookId);
            if (book != null)
            {
                book.IsFrozen = !book.IsFrozen;
                db.SaveChanges();
                MessageBox.Show(book.IsFrozen ? "Книга заморожена." : "Книга разморожена.");
                LoadBook();
            }
        }

        private void BtnSubmitReview_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TxtRate.Text, out int rate) || rate < 1 || rate > 10)
            {
                MessageBox.Show("Введите оценку от 1 до 10.");
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtReviewText.Text))
            {
                MessageBox.Show("Введите текст отзыва.");
                return;
            }

            var db = Core.Context;
            var review = new Reviews
            {
                UserId = Session.CurrentUser.Id,
                BookId = _bookId,
                Rate = (byte)rate,
                Text = TxtReviewText.Text,
                CreationDate = DateTime.Now,
                IsFrozen = false
            };
            db.Reviews.Add(review);
            db.SaveChanges();
            MessageBox.Show("Отзыв добавлен!");
            TxtRate.Text = "";
            TxtReviewText.Text = "";
            LoadBook();
        }

        private void BtnComplainReview_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int reviewId)
            {
                var dlg = new ComplaintDialog(Session.CurrentUser.Id, null, reviewId);
                dlg.ShowDialog();
            }
        }

        private void BtnFreezeReview_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int reviewId)
            {
                var db = Core.Context;
                var review = db.Reviews.First(r => r.Id == reviewId);
                if (review != null)
                {
                    review.IsFrozen = review.IsFrozen == null ? true : !review.IsFrozen;
                    db.SaveChanges();
                    MessageBox.Show(review.IsFrozen == true? "Отзыв заморожен." : "Отзыв разморожен.");
                    LoadBook();
                }
            }
        }
    }

    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "";
        public int Rate { get; set; }
        public string Text { get; set; } = "";
        public string Date { get; set; } = "";
        public Visibility FreezeVisibility { get; set; }
    }
}
