using ShutIKrol.Database;
using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Data.Entity;

namespace ShutIKrol.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            TxtLoginError.Visibility = Visibility.Collapsed;
            var login = TxtLogin.Text.Trim();
            var password = TxtPassword.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                TxtLoginError.Text = "Введите логин и пароль";
                TxtLoginError.Visibility = Visibility.Visible;
                return;
            }

            var db = Core.Context;
            var user = db.Users
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user == null)
            {
                TxtLoginError.Text = "Неверный логин или пароль";
                TxtLoginError.Visibility = Visibility.Visible;
                return;
            }

            Session.CurrentUser = user;
            var main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            TxtRegError.Visibility = Visibility.Collapsed;
            var name = TxtRegName.Text.Trim();
            var login = TxtRegLogin.Text.Trim();
            var email = TxtRegEmail.Text.Trim();
            var password = TxtRegPassword.Password;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TxtRegError.Text = "Заполните все поля";
                TxtRegError.Visibility = Visibility.Visible;
                return;
            }

            var db = Core.Context;

            if (db.Users.Any(u => u.Login == login))
            {
                TxtRegError.Text = "Логин уже занят";
                TxtRegError.Visibility = Visibility.Visible;
                return;
            }

            var readerRole = db.Roles.FirstOrDefault(r => r.Name == "Читатель") ?? db.Roles.First();

            var newUser = new Users
            {
                Name = name,
                Login = login,
                Email = email,
                Password = password,
                RoleId = readerRole.Id,
                IsFrozen = false,
                RegistrationDate = DateTime.Now
            };

            db.Users.Add(newUser);
            db.SaveChanges();

            MessageBox.Show("Регистрация успешна! Войдите в систему.", "Успех");
        }
    }
}
