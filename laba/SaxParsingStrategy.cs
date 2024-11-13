using System;
using System.Collections.Generic;
using System.Xml;

namespace laba
{
    public class SaxXmlAnalyzer : IXmlAnalyzer
    {
        public List<StudentInfo> Analyze(string keyword, string filePath)
        {
            List<StudentInfo> students = new List<StudentInfo>();
            StudentInfo currentStudent = null;

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "Студент":
                                currentStudent = new StudentInfo
                                {
                                    Faculty = reader.GetAttribute("Факультет"),
                                    EducationLevel = reader.GetAttribute("РівеньОсвіти"), // Зміна для коректного читання рівня освіти
                                    Course = reader.GetAttribute("Курс"),                 // Зміна для коректного читання курсу
                                    AverageRating = 0, // або обчисліть середній рейтинг, якщо потрібно
                                    Subjects = new List<SubjectInfo>()
                                };
                                break;
                            case "Ім_я":
                                if (currentStudent != null)
                                    currentStudent.Name = reader.ReadElementContentAsString();
                                break;
                            case "Предмет":
                                if (currentStudent != null)
                                {
                                    currentStudent.Subjects.Add(new SubjectInfo
                                    {
                                        Name = reader.GetAttribute("Назва"),
                                        Grade = reader.GetAttribute("Оцінка")
                                    });
                                }
                                break;
                        }
                    }

                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Студент")
                    {
                        if (currentStudent != null)
                            students.Add(currentStudent);
                    }
                }
            }

            return students;
        }
    }
}
