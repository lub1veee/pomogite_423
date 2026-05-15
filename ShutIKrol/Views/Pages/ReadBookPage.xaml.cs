using ShutIKrol.Database;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace ShutIKrol.Views.Pages
{
    public partial class ReadBookPage : Page
    {
        private readonly int _bookId;
        private List<Chapters> _chapters = new List<Chapters>();

        public ReadBookPage(int bookId)
        {
            InitializeComponent();
            _bookId = bookId;
            LoadChapters();
        }

        private void LoadChapters()
        {
            var db = Core.Context;
            var book = db.Books.Include(b => b.Chapters).FirstOrDefault(b => b.Id == _bookId);
            if (book == null) return;

            TxtBookTitle.Text = book.Name;
            _chapters = book.Chapters.OrderBy(c => c.Number).ToList();

            CmbChapters.ItemsSource = _chapters.Select(c => $"Глава {c.Number}: {c.Name}").ToList();
            if (_chapters.Any())
                CmbChapters.SelectedIndex = 0;
        }

        private void CmbChapters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbChapters.SelectedIndex < 0 || CmbChapters.SelectedIndex >= _chapters.Count) return;
            var chapter = _chapters[CmbChapters.SelectedIndex];

            if (!string.IsNullOrEmpty(chapter.Path) && File.Exists(chapter.Path))
            {
                TxtContent.Text = File.ReadAllText(chapter.Path);
            }
            else
            {
                TxtContent.Text = "[Файл главы не найден. Путь: " + chapter.Path + "]";
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
