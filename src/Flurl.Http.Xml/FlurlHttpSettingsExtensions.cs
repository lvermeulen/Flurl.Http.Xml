using System;
using System.Text;
using System.Xml;
using Flurl.Http.Configuration;

namespace Flurl.Http.Xml
{
	/// <summary>
	/// FlurlHttpSettingsExtensions
	/// </summary>
	public static class FlurlHttpSettingsExtensions
	{
		private static XmlWriterSettings _sXmlWriterSettings = new XmlWriterSettings { Encoding = new UTF8Encoding(false, false), Indent = true, OmitXmlDeclaration = false };
		private static readonly Lazy<MicrosoftXmlSerializer> SXmlSerializerInstance = new Lazy<MicrosoftXmlSerializer>(() => new MicrosoftXmlSerializer(_sXmlWriterSettings));

		/// <summary>
		/// XMLs the serializer.
		/// </summary>
		/// <param name="settings">The settings.</param>
		public static MicrosoftXmlSerializer XmlSerializer(this FlurlHttpSettings settings)
		{
			return XmlSerializer(settings, _sXmlWriterSettings);
		}

		/// <summary>
		/// XMLs the serializer.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <param name="xmlWriterSettings">The XML writer settings.</param>
		public static MicrosoftXmlSerializer XmlSerializer(this FlurlHttpSettings settings, XmlWriterSettings xmlWriterSettings)
		{
			_sXmlWriterSettings = xmlWriterSettings;
			return SXmlSerializerInstance.Value;
		}
	}
}
