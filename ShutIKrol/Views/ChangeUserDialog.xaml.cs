using ShutIKrol.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShutIKrol.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangeUserDialog.xaml
    /// </summary>
    public partial class ChangeUserDialog : Window
    {
        public Roles Role;
        public string Password;
        public bool IsFrozen;
        public ChangeUserDialog(Users user)
        {
            InitializeComponent();
            RolesCombobox.ItemsSource = Core.Context.Roles.Select(r => r.Name).ToList();
            RolesCombobox.SelectedValue = user.Roles.Name;
            PasswordBox.Text = user.Password;
            IsFrozen = user.IsFrozen;
            FreezeButton.Content = IsFrozen ? "Разморозить" : "Заморозить";
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Role = Core.Context.Roles.First(r => r.Name == (string)RolesCombobox.SelectedItem);
            Password = PasswordBox.Text;
            DialogResult = true;
        }

        private void FreezeButton_Click(object sender, RoutedEventArgs e)
        {
            IsFrozen = !IsFrozen;
            FreezeButton.Content = IsFrozen ? "Разморозить" : "Заморозить";
        }
    }
}
