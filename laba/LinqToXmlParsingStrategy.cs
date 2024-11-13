using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using laba;

public class LinqXmlAnalyzer : IXmlAnalyzer
{
    public List<StudentInfo> Analyze(string keyword, string filePath)
    {
        XDocument doc = XDocument.Load(filePath);

        var students = doc.Descendants("Студент")
            .Where(s => string.IsNullOrEmpty(keyword) ||
                        s.Attribute("Факультет")?.Value.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true)
            .Select(s => new StudentInfo
            {
                Name = s.Element("Ім_я")?.Value,
                Faculty = s.Attribute("Факультет")?.Value,
                EducationLevel = s.Attribute("Рівень_освіти")?.Value,
                Course = s.Attribute("Курс")?.Value,
                Subjects = s.Elements("Предмет").Select(sub => new SubjectInfo
                {
                    Name = sub.Attribute("Назва")?.Value,
                    Grade = sub.Attribute("Оцінка")?.Value
                }).ToList()
            }).ToList();

        return students;
    }
}
