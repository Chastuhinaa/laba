using System.Xml.Xsl;
namespace laba
{

    public class XmlToHtmlTransformer
    {
        public void Transform(string xmlFilePath, string xslFilePath, string outputHtmlFilePath)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xslFilePath);
            xslt.Transform(xmlFilePath, outputHtmlFilePath);
        }
    }
}
