namespace laba
{
    public class XmlParserContext
    {
        private IXmlAnalyzer _strategy;

        // Конструктор, який приймає стратегію
        public XmlParserContext(IXmlAnalyzer strategy)
        {
            _strategy = strategy;
        }

        // Метод для зміни стратегії
        public void SetStrategy(IXmlAnalyzer strategy)
        {
            _strategy = strategy;
        }

        // Метод для виконання аналізу, що викликає метод Analyze у стратегії
        public List<StudentInfo> ExecuteParse(string filePath, string keyword)
        {
            return _strategy?.Analyze(keyword, filePath); // Викликаємо метод Analyze
        }
    }
}
