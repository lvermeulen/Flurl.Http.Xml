using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Flurl.Http.Configuration;

namespace Flurl.Http.Xml
{
    public static class FlurlHttpSettingsExtensions
    {
        private static XmlWriterSettings s_xmlWriterSettings = new XmlWriterSettings { Encoding = new UTF8Encoding(false, false), Indent = true, OmitXmlDeclaration = false };
        private static readonly Lazy<MicrosoftXmlSerializer> s_xmlSerializerInstance = new Lazy<MicrosoftXmlSerializer>(() => new MicrosoftXmlSerializer(s_xmlWriterSettings));

        public static MicrosoftXmlSerializer XmlSerializer(this FlurlHttpSettings settings)
        {
            return XmlSerializer(settings, s_xmlWriterSettings);
        }

        public static MicrosoftXmlSerializer XmlSerializer(this FlurlHttpSettings settings, XmlWriterSettings xmlWriterSettings)
        {
            s_xmlWriterSettings = xmlWriterSettings;
            return s_xmlSerializerInstance.Value;
        }
    }
}
