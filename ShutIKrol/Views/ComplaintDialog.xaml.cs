using ShutIKrol.Database;
using System.Runtime.Remoting.Contexts;
using System.Windows;

namespace ShutIKrol.Views.Pages
{
    public partial class ComplaintDialog : Window
    {
        private readonly int _userId;
        private readonly int? _bookId;
        private readonly int? _reviewId;

        public ComplaintDialog(int userId, int? bookId, int? reviewId)
        {
            InitializeComponent();
            _userId = userId;
            _bookId = bookId;
            _reviewId = reviewId;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtReason.Text))
            {
                MessageBox.Show("Введите причину жалобы.");
                return;
            }

            var db = Core.Context;
            db.Complaints.Add(new Complaints
            {
                UserId = _userId,
                BookId = _bookId,
                ReviewId = _reviewId,
                ReasonText = TxtReason.Text,
                IsResolved = false
            });
            db.SaveChanges();
            MessageBox.Show("Жалоба отправлена!");
            this.Close();
        }
    }
}
