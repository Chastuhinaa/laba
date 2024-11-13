namespace laba
{
    public class StudentInfo
    {
        public string Name { get; set; }             // Ім'я студента
        public string Faculty { get; set; }          // Факультет
        public string EducationLevel { get; set; }   // Рівень освіти
        public string Course { get; set; }           // Курс
        public double AverageRating { get; set; }    // Середній рейтинг
        public List<SubjectInfo> Subjects { get; set; } = new List<SubjectInfo>(); // Список предметів
    }
}
