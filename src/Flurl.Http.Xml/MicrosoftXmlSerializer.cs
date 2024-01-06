using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Flurl.Http.Configuration;

namespace Flurl.Http.Xml;

/// <summary>
/// ISerializer implementation that uses Microsoft XmlSerializer.
/// Default serializer used in calls to GetXmlAsync, PostXmlAsync, etc.
/// </summary>
public class MicrosoftXmlSerializer : ISerializer
{
    private readonly XmlWriterSettings _xmlWriterSettings;

    /// <summary>
    /// The default XML writer settings.
    /// </summary>
    public static XmlWriterSettings DefaultXmlWriterSettings { get; } = new XmlWriterSettings
    {
        Encoding = new UTF8Encoding(false, false),
        Indent = true,
        OmitXmlDeclaration = false
    };

    /// <summary>
    /// Returns a default instance of the serializer.
    /// </summary>
    public static MicrosoftXmlSerializer Default { get; } = new MicrosoftXmlSerializer(DefaultXmlWriterSettings);

    /// <summary>
    /// Initializes a new instance of the <see cref="MicrosoftXmlSerializer"/> class.
    /// </summary>
    /// <param name="xmlWriterSettings">The XML writer settings.</param>
    public MicrosoftXmlSerializer(XmlWriterSettings xmlWriterSettings) => _xmlWriterSettings = xmlWriterSettings;

    /// <summary>
    /// Serializes an object to a string representation.
    /// </summary>
    /// <param name="obj"></param>
    public string Serialize(object obj)
    {
        using var writer = new Utf8StringWriter();
        using var xmlWriter = XmlWriter.Create(writer, _xmlWriterSettings);
        var serializer = new XmlSerializer(obj.GetType());
        serializer.Serialize(xmlWriter, obj);
        return writer.ToString();
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
        doc.Declaration ??= new XDeclaration("1.0", "utf-8", "yes");

        return doc;
    }

    private static T DeserializeInternal<T>(TextReader reader)
    {
        var xml = TextReaderToString(reader);
        var doc = StringToXDocument(xml);
        var xmlWithDeclaration = doc.ToStringWithDeclaration();

        using var textReader = new StringReader(xmlWithDeclaration);
        var serializer = new XmlSerializer(typeof(T));
        return (T)serializer.Deserialize(textReader);
    }

    /// <summary>
    /// Deserializes an object from a string representation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="s">The string.</param>
    public T Deserialize<T>(string s) => string.IsNullOrWhiteSpace(s) ? default : DeserializeInternal<T>(new StringReader(s));

    /// <summary>
    /// Deserializes an object from a stream representation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    public T Deserialize<T>(Stream stream) => stream == null ? default : DeserializeInternal<T>(new StreamReader(stream));
}