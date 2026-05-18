using ShutIKrol.Database;
using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Data.Entity;

namespace ShutIKrol.Views.Pages
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadProfile();
        }

        private void LoadProfile()
        {
            var db = Core.Context;
            var user = db.Users
                .Include(u => u.Roles)
                .Include(u => u.Reviews.Select(r => r.Books))
                .FirstOrDefault(u => u.Id == Session.CurrentUser.Id);

            if (user == null) return;

            TxtName.Text = user.Name;
            TxtLogin.Text = user.Login;
            TxtEmail.Text = user.Email;
            TxtRole.Text = user.Roles.Name;

            if (user.IsFrozen)
            {
                FrozenGroup.Visibility = Visibility.Visible;
                TxtFreezeReason.Text = "Ваш аккаунт был заморожен администратором.";
            }

            var existingRequest = db.Requests
                .Include(r => r.RequestTypes)
                .Where(r => r.UserId == user.Id)
                .OrderByDescending(r => r.Id)
                .FirstOrDefault(r => r.RequestTypes.TypeName == "Роль автора");

            if (user.Roles.Name == "Автор" || user.Roles.Name == "Администратор")
            {
                TxtAuthorStatus.Text = "Вы уже являетесь автором.";
                BtnApplyAuthor.IsEnabled = false;
            }
            else if (existingRequest != null)
            {
                if (existingRequest.IsApproved == null)
                    TxtAuthorStatus.Text = "Заявка на роль автора находится на рассмотрении.";
                else if (existingRequest.IsApproved == true)
                    TxtAuthorStatus.Text = "Заявка одобрена.";
                else
                    TxtAuthorStatus.Text = "Заявка отклонена. Можете подать повторно.";

                BtnApplyAuthor.IsEnabled = existingRequest.IsApproved == false;
            }
            else
            {
                TxtAuthorStatus.Text = "Вы можете подать заявку на роль Автора.";
            }

            ReviewsList.ItemsSource = user.Reviews.Select(r => new
            {
                BookName = r.Books.Name,
                r.Rate,
                r.Text,
                Date = r.CreationDate.ToString("dd.MM.yyyy")
            }).ToList();
        }

        private void BtnApplyAuthor_Click(object sender, RoutedEventArgs e)
        {
            var db = Core.Context;
            var authorRequestType = db.RequestTypes.FirstOrDefault(rt => rt.TypeName == "Роль автора");
            if (authorRequestType == null)
            {
                authorRequestType = new RequestTypes { TypeName = "Роль автора" };
                db.RequestTypes.Add(authorRequestType);
                db.SaveChanges();
            }

            var request = new Requests
            {
                TypeId = authorRequestType.Id,
                UserId = Session.CurrentUser.Id,
                Comment = "Заявка на роль автора",
                IsApproved = null
            };
            db.Requests.Add(request);
            db.SaveChanges();
            MessageBox.Show("Заявка на роль Автора отправлена!");
            LoadProfile();
        }

        private void BtnAppealFreeze_Click(object sender, RoutedEventArgs e)
        {
            var db = Core.Context;
            var unfreezeType = db.RequestTypes.FirstOrDefault(rt => rt.TypeName == "Снятие заморозки аккаунта");
            if (unfreezeType == null)
            {
                unfreezeType = new RequestTypes { TypeName = "Снятие заморозки аккаунта" };
                db.RequestTypes.Add(unfreezeType);
                db.SaveChanges();
            }

            var existing = db.Requests.Any(r => r.UserId == Session.CurrentUser.Id && r.TypeId == unfreezeType.Id && r.IsApproved == null);
            if (existing)
            {
                MessageBox.Show("Заявка уже подана и находится на рассмотрении.");
                return;
            }

            db.Requests.Add(new Requests
            {
                TypeId = unfreezeType.Id,
                UserId = Session.CurrentUser.Id,
                Comment = "Прошу снять заморозку аккаунта",
                IsApproved = null
            });
            db.SaveChanges();
            MessageBox.Show("Заявка на снятие заморозки отправлена!");
        }
    }
}
