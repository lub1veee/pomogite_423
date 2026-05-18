using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace ShutIKrol.Views.Pages
{
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            LoadAll();
        }

        private void LoadAll()
        {
            LoadComplaints();
            LoadRequests();
            LoadFrozen();
            LoadUsers();
        }

        private void LoadComplaints()
        {
            var db = Core.Context;
            var complaints = db.Complaints
                .Include(c => c.Users)
                .Include(c => c.Books)
                .Include(c => c.Reviews)
                .ToList();

            ComplaintsGrid.ItemsSource = complaints.Select(c => new
            {
                c.Id,
                FromUser = c.Users.Name,
                TypeText = c.BookId.HasValue ? "На книгу" : c.ReviewId.HasValue ? "На отзыв" : "На автора",
                c.ReasonText
            }).ToList();
        }

        private void LoadRequests()
        {
            var db = Core.Context;
            var requests = db.Requests
                .Include(r => r.Users)
                .Include(r => r.RequestTypes)
                .ToList();

            RequestsGrid.ItemsSource = requests.Select(r => new
            {
                r.Id,
                TypeName = r.RequestTypes.TypeName,
                UserName = r.Users.Name,
                Comment = r.Comment ?? ""
            }).ToList();
        }

        private void LoadFrozen()
        {
            var db = Core.Context;
            FrozenBooksGrid.ItemsSource = db.Books
                .Include(b => b.Users)
                .Where(b => b.IsFrozen)
                .Select(b => new { b.Id, b.Name, AuthorName = b.Users.Name })
                .ToList();

            FrozenUsersGrid.ItemsSource = db.Users
                .Where(u => u.IsFrozen)
                .Select(u => new { u.Id, u.Name, u.Login })
                .ToList();
        }

        private void LoadUsers()
        {
            var db = Core.Context;
            UsersGrid.ItemsSource = db.Users
                .Include(u => u.Roles)
                .Select(u => new { u.Id, u.Name, u.Login, RoleName = u.Roles.Name })
                .ToList();
        }

        private void BtnRefreshComplaints_Click(object sender, RoutedEventArgs e) => LoadComplaints();
        private void BtnRefreshRequests_Click(object sender, RoutedEventArgs e) => LoadRequests();
        private void BtnRefreshUsers_Click(object sender, RoutedEventArgs e) => LoadUsers();

        private void BtnAcceptComplaint_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var db = Core.Context;
                var c = db.Complaints.Find(id);
                if (c != null) { c.IsResolved = true; db.SaveChanges(); }
                LoadComplaints();
            }
        }

        private void BtnRejectComplaint_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var db = Core.Context;
                var c = db.Complaints.Find(id);
                if (c != null) { c.IsResolved = true; db.SaveChanges(); }
                LoadComplaints();
            }
        }

        private void BtnAcceptRequest_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var db = Core.Context;
                var r = db.Requests.Include(x => x.RequestTypes).Include(x => x.Users).FirstOrDefault(x => x.Id == id);
                if (r != null)
                {
                    r.IsApproved = true;
                    if (r.RequestTypes.TypeName == "Роль автора")
                    {
                        var authorRole = db.Roles.FirstOrDefault(ro => ro.Name == "Автор");
                        if (authorRole != null)
                            r.Users.RoleId = authorRole.Id;
                    }
                    if (r.RequestTypes.TypeName == "Снятие заморозки аккаунта")
                        r.Users.IsFrozen = false;

                    db.SaveChanges();
                }
                LoadRequests();
                LoadUsers();
            }
        }

        private void BtnRejectRequest_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var db = Core.Context;
                var r = db.Requests.Find(id);
                if (r != null) { r.IsApproved = false; db.SaveChanges(); }
                LoadRequests();
            }
        }

        private void BtnUnfreezeBook_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var db = Core.Context;
                var b = db.Books.Find(id);
                if (b != null) { b.IsFrozen = false; db.SaveChanges(); }
                LoadFrozen();
            }
        }

        private void BtnUnfreezeUser_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var db = Core.Context;
                var u = db.Users.Find(id);
                if (u != null) { u.IsFrozen = false; db.SaveChanges(); }
                LoadFrozen();
                LoadUsers();
            }
        }

        private void ChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var db = Core.Context;
                var user = db.Users.First(u => u.Id == id);
                var dialog = new ChangeUserDialog(user);
                if (dialog.ShowDialog() == true)
                {
                    user.RoleId = dialog.Role.Id;
                    user.Password = dialog.Password;
                    user.IsFrozen = dialog.IsFrozen;
                }
                Core.Context.SaveChanges();
                MessageBox.Show("Изменения применены!");
            }
        }
    }
}
