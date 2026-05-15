using ShutIKrol.Database;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShutIKrol.Views.Pages
{
    public partial class AddToListDialog : Window
    {
        private readonly int _bookId;

        public AddToListDialog(int bookId)
        {
            InitializeComponent();
            _bookId = bookId;
            CmbStatus.ItemsSource = Core.Context.ReadStatuses.Select(rs => rs.Name).ToList();
            CmbStatus.SelectedItem = "В планах";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if ((string)CmbStatus.SelectedItem == "")
            {
                var readlist = Core.Context.ReadList.FirstOrDefault(rl => rl.UserId == Session.CurrentUser.Id && rl.BookId == _bookId);
                if (readlist != null)
                {
                    Core.Context.ReadList.Remove(readlist);
                    Core.Context.SaveChanges();
                    MessageBox.Show("Книга удалена из списка!");
                    DialogResult = true;
                }
                else DialogResult = false;
                return;
            }
            var readl = new ReadList
            {
                BookId = _bookId,
                StatusId = Core.Context.ReadStatuses.First(rs => rs.Name == (string)CmbStatus.SelectedItem).Id,
                UserId = Session.CurrentUser.Id
            };

            Core.Context.ReadList.Add(readl);
            Core.Context.SaveChanges();
            MessageBox.Show("Книга добавлена в список!");
            DialogResult = true;
        }
    }
}