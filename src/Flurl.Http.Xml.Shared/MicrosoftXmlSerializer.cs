using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Flurl.Http.Configuration;

namespace Flurl.Http.Xml
{
    /// <summary>
    /// ISerializer implementation that uses Microsoft XmlSerializer.
    /// Default serializer used in calls to GetXmlAsync, PostXmlAsync, etc.
    /// </summary>
    public class MicrosoftXmlSerializer : ISerializer
    {
        private readonly XmlWriterSettings _xmlWriterSettings;

        public MicrosoftXmlSerializer(XmlWriterSettings xmlWriterSettings)
        {
            _xmlWriterSettings = xmlWriterSettings;
        }

        public string Serialize(object obj)
        {
            using (var writer = new Utf8StringWriter())
            using (var xmlWriter = XmlWriter.Create(writer, _xmlWriterSettings))
            {
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(xmlWriter, obj);
                return writer.ToString();
            }
        }

        private static string TextReaderToString(TextReader reader)
        {
            using (reader)
            {
                return reader.ReadToEnd();
            }
        }

        private static XDocument StringToXDocument(string s)
        {
            var doc = XDocument.Parse(s);
            if (doc.Declaration == null)
            {
                doc.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            }

            return doc;
        }

        private T DeserializeInternal<T>(TextReader reader)
        {
            string xml = TextReaderToString(reader);
            var doc = StringToXDocument(xml);
            string xmlWithDeclaration = doc.ToStringWithDeclaration();

            using (var textReader = new StringReader(xmlWithDeclaration))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(textReader);
            }
        }

        public T Deserialize<T>(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return default(T);
            }

            return DeserializeInternal<T>(new StringReader(s));
        }

        public T Deserialize<T>(Stream stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            return DeserializeInternal<T>(new StreamReader(stream));
        }
    }
}
