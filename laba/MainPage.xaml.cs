using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Xsl;
using System.Xml;

namespace laba
{
    public partial class MainPage : ContentPage
    {
        private IXmlAnalyzer _xmlAnalyzer;
        private ObservableCollection<StudentInfo> _students;

        public MainPage()
        {
            InitializeComponent();
            _students = new ObservableCollection<StudentInfo>();
            resultsTable.ItemsSource = _students;
        }

        private void OnSaxParsingClicked(object sender, EventArgs e)
        {
            _xmlAnalyzer = new SaxXmlAnalyzer();
            ExecuteParse();
        }

        private void OnDomParsingClicked(object sender, EventArgs e)
        {
            _xmlAnalyzer = new DomXmlAnalyzer();
            ExecuteParse();
        }

        private void OnLinqParsingClicked(object sender, EventArgs e)
        {
            _xmlAnalyzer = new LinqXmlAnalyzer();
            ExecuteParse();
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            // Отримуємо текст для пошуку з поля вводу
            string searchText = searchField.Text?.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                // Якщо поле пошуку порожнє, то відобразити всіх студентів
                resultsTable.ItemsSource = _students;
            }
            else
            {
                // Фільтруємо список студентів за введеним текстом
                var filteredStudents = _students.Where(student =>
             // Перевірка кожного поля з умовою на null і приведенням до bool
             (student.Name?.ToLower().Contains(searchText) ?? false) ||
             (student.Faculty?.ToLower().Contains(searchText) ?? false) ||
             (student.EducationLevel?.ToLower().Contains(searchText) ?? false) ||
             (student.Course?.ToLower().Contains(searchText) ?? false) ||
             // Пошук у колекції Subjects, якщо вона не пуста
             student.Subjects.Any(subject =>
                 (subject.Name?.ToLower().Contains(searchText) ?? false) ||
                 (subject.Grade?.ToLower().Contains(searchText) ?? false)
             )
         ).ToList();

                // Встановлюємо відфільтрований список у TableView
                resultsTable.ItemsSource = filteredStudents;
            }
        }


        private void ExecuteParse()
        {
            string filePath = "D:\\laba\\laba\\Resources\\XML\\student.xml";

            try
            {
                var parsedStudents = _xmlAnalyzer.Analyze(searchField.Text, filePath);

                _students.Clear();
                foreach (var student in parsedStudents)
                {
                    student.AverageRating = student.Subjects.Count > 0
                        ? student.Subjects.Average(s => double.TryParse(s.Grade, out double grade) ? grade : 0)
                        : 0;

                    _students.Add(student);
                }

                DisplayAlert("Успіх", "Парсинг завершено.", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Помилка", $"Помилка парсингу: {ex.Message}", "OK");
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            searchField.Text = string.Empty;
            _students.Clear();
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool confirmExit = await DisplayAlert("Підтвердження", "Чи дійсно ви хочете завершити роботу?", "Так", "Ні");
            if (confirmExit)
            {
                Environment.Exit(0);
            }
        }

        private async void OnTransformToHtmlClicked(object sender, EventArgs e)
        {
            string xmlPath = "D:\\laba\\laba\\Resources\\XML\\student.xml";
            string xslPath = "D:\\laba\\laba\\Resources\\XML\\student.xsl";
            string htmlPath = "D:\\laba\\laba\\Resources\\Splash\\HTML\\output.html";

            try
            {
                if (!File.Exists(xmlPath) || !File.Exists(xslPath))
                {
                    await DisplayAlert("Помилка", "XML або XSL файл не знайдено.", "OK");
                    return;
                }

                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xslPath);

                using (FileStream outputStream = new FileStream(htmlPath, FileMode.Create))
                using (XmlReader xmlReader = XmlReader.Create(xmlPath))
                using (XmlWriter writer = XmlWriter.Create(outputStream))
                {
                    xslt.Transform(xmlReader, writer);
                }

                await DisplayAlert("Успіх", $"Файл перетворено в HTML: {htmlPath}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", $"Помилка трансформації: {ex.Message}", "OK");
            }
        }
    }
}
