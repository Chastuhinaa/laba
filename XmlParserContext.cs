namespace laba
{
    public class XmlParserContext
    {
        private IXmlAnalyzer _strategy;

        
        public XmlParserContext(IXmlAnalyzer strategy)
        {
            _strategy = strategy;
        }

        
        public void SetStrategy(IXmlAnalyzer strategy)
        {
            _strategy = strategy;
        }

        
        public List<StudentInfo> ExecuteParse(string filePath, string keyword)
        {
            return _strategy?.Analyze(keyword, filePath); 
        }
    }
}
