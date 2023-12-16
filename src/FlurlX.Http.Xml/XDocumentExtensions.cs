using System.Text;
using System.Xml.Linq;

namespace FlurlX.Http.Xml
{
    /// <summary>
    /// XDocumentExtensions
    /// </summary>
    public static class XDocumentExtensions
    {
        /// <summary>
        /// To the string with declaration.
        /// </summary>
        /// <param name="doc">The document.</param>
        public static string ToStringWithDeclaration(this XDocument doc)
        {
            var sb = new StringBuilder();
            using (var writer = new Utf8StringWriter(sb))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
    }
}