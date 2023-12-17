using System;
using System.Text;
using System.Xml;

namespace FlurlX.Http.Xml
{
    /// <summary>
    /// FlurlHttpSettingsExtensions
    /// </summary>
    public static class FlurlHttpSettingsExtensions
    {
        private static XmlWriterSettings s_xmlWriterSettings = new XmlWriterSettings { Encoding = new UTF8Encoding(false, false), Indent = true, OmitXmlDeclaration = false };
        private static readonly Lazy<MicrosoftXmlSerializer> s_xmlSerializerInstance = new Lazy<MicrosoftXmlSerializer>(() => new MicrosoftXmlSerializer(s_xmlWriterSettings));

        /// <summary>
        /// XMLs the serializer.
        /// </summary>
        public static MicrosoftXmlSerializer XmlSerializer()
        {
            return s_xmlSerializerInstance.Value;
        }
    }
}
