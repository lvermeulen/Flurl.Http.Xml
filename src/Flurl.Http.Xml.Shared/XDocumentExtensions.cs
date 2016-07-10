using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Flurl.Http.Xml
{
    public static class XDocumentExtensions
    {
        public static string ToStringWithDeclaration(this XDocument doc)
        {
            var sb = new StringBuilder();
            using (TextWriter writer = new Utf8StringWriter(sb))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
    }
}
