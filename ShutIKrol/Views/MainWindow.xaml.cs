using ShutIKrol.Views.Pages;
using System.Windows;

namespace ShutIKrol.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var user = Session.CurrentUser;

            if (Session.IsAdmin)
                BtnAdmin.Visibility = Visibility.Visible;

            if (Session.IsAuthor)
                BtnAuthor.Visibility = Visibility.Visible;

            if (Session.IsFrozen)
                BtnFrozen.Visibility = Visibility.Visible;

            NavigateTo(new CatalogPage());
        }

        public void NavigateTo(System.Windows.Controls.Page page)
        {
            MainFrame.Navigate(page);
        }

        private void BtnCatalog_Click(object sender, RoutedEventArgs e) => NavigateTo(new CatalogPage());
        private void BtnLists_Click(object sender, RoutedEventArgs e) => NavigateTo(new ReadListPage());
        private void BtnAuthor_Click(object sender, RoutedEventArgs e) => NavigateTo(new AuthorPage());
        private void BtnAdmin_Click(object sender, RoutedEventArgs e) => NavigateTo(new AdminPage());
        private void BtnProfile_Click(object sender, RoutedEventArgs e) => NavigateTo(new ProfilePage());
        private void BtnFrozen_Click(object sender, RoutedEventArgs e)
        {
            var user = Session.CurrentUser;
            MessageBox.Show($"Ваш аккаунт заморожен.\nЕсли хотите оспорить - перейдите в Профиль.", "Аккаунт заморожен", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
