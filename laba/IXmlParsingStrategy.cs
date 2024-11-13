using System.Collections.Generic;

namespace laba
{
    public interface IXmlAnalyzer
    {
        List<StudentInfo> Analyze(string keyword, string filePath);
    }
}

