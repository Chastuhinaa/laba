using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace laba
{
    public class DomXmlAnalyzer : IXmlAnalyzer
    {
        public List<StudentInfo> Analyze(string keyword, string filePath)
        {
            List<StudentInfo> students = new List<StudentInfo>();
            XDocument doc = XDocument.Load(filePath);

            var studentElements = doc.Descendants("Студент");
            foreach (var student in studentElements)
            {
                var studentInfo = new StudentInfo
                {
                    Name = student.Element("Ім_я")?.Value,
                    Faculty = student.Attribute("Факультет")?.Value,
                    EducationLevel = student.Attribute("РівеньОсвіти")?.Value,
                    Course = student.Attribute("Курс")?.Value,
                    Subjects = student.Elements("Предмет")
                                      .Select(s => new SubjectInfo
                                      {
                                          Name = s.Attribute("Назва")?.Value,
                                          Grade = s.Attribute("Оцінка")?.Value
                                      }).ToList()
                };

                // Додаємо студента, якщо його дані співпадають з ключовим словом
                if (string.IsNullOrEmpty(keyword) ||
                    studentInfo.Name?.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true ||
                    studentInfo.Faculty?.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true)
                {
                    students.Add(studentInfo);
                }
            }
            return students;
        }
    }
}
