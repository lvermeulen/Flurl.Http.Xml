using Flurl.Http.Content;

namespace FlurlX.Http.Xml
{
    /// <summary>
    /// Provides HTTP content based on a serialized XML object, with the XML string captured to a property
    /// so it can be read without affecting the read-once content stream.
    /// </summary>
    public class CapturedXmlContent : CapturedStringContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CapturedXmlContent"/> class.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="mediaType">The media-type.</param>
        public CapturedXmlContent(string xml, string mediaType) : base(xml, mediaType) { }
    }
}