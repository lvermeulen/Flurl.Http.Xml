using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flurl.Http.Content;

namespace Flurl.Http.Xml
{
    /// <summary>
    /// Provides HTTP content based on a serialized XML object, with the XML string captured to a property
    /// so it can be read without affecting the read-once content stream.
    /// </summary>
    public class CapturedXmlContent : CapturedStringContent
    {
        public CapturedXmlContent(string xml) : base(xml, Encoding.UTF8, "application/xml") { }
    }
}
