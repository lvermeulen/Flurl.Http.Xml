using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Flurl.Http.Xml
{
	/// <summary>
	/// HttpResponseMessageExtensions
	/// </summary>
	public static class HttpResponseMessageExtensions
	{
	    private static HttpCall GetHttpCall(HttpRequestMessage request)
	    {
            object obj;
            if (request?.Properties != null && request.Properties.TryGetValue("FlurlHttpCall", out obj) && obj is HttpCall)
            {
                return (HttpCall)obj;
            }
	        return null;
        }

		/// <summary>
		/// Deserializes XML-formatted HTTP response body to object of type T. Intended to chain off an async HTTP.
		/// </summary>
		/// <typeparam name="T">A type whose structure matches the expected XML response.</typeparam>
		/// <returns>A Task whose result is an object containing data in the response body.</returns>
		/// <example>x = await url.PosAsync(data).ReceiveXml&lt;T&gt;()</example>
		public static async Task<T> ReceiveXml<T>(this Task<HttpResponseMessage> response)
		{
			var resp = await response.ConfigureAwait(false);
			var call = GetHttpCall(resp.RequestMessage);
			try
			{
				using (var stream = await resp.Content.ReadAsStreamAsync().ConfigureAwait(false))
				{
					return call.Settings.XmlSerializer().Deserialize<T>(stream);
				}
			}
			catch (Exception ex)
			{
				var s = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
				call.Exception = ex;
				throw new FlurlHttpException(call, s, ex);
			}
		}

		/// <summary>
		/// Parses XML-formatted HTTP response body into an XDocument. Intended to chain off an async call.
		/// </summary>
		/// <returns>A Task whose result is an XDocument containing XML data from the response body.</returns>
		/// <example>d = await url.PostAsync(data).ReceiveXDocument()</example>
		public static async Task<XDocument> ReceiveXDocument(this Task<HttpResponseMessage> response)
		{
			var resp = await response.ConfigureAwait(false);
			var call = GetHttpCall(resp.RequestMessage);
			try
			{
				using (var stream = await resp.Content.ReadAsStreamAsync().ConfigureAwait(false))
				using (var streamReader = new StreamReader(stream))
				{
					return XDocument.Parse(streamReader.ReadToEnd());
				}
			}
			catch (Exception ex)
			{
				var s = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
				call.Exception = ex;
				throw new FlurlHttpException(call, s, ex);
			}
		}

		/// <summary>
		/// Parses XML-formatted HTTP response body into a collection of XElements. Intended to chain off an async call.
		/// </summary>
		/// <returns>A Task whose result is a collection of XElements from an XDocument containing XML data from the response body.</returns>
		/// <example>d = await url.PostAsync(data).ReceiveXElementsFromXPath(xpathExpression)</example>
		public static async Task<IEnumerable<XElement>> ReceiveXElementsFromXPath(this Task<HttpResponseMessage> response, string expression)
		{
			var doc = await response.ReceiveXDocument().ConfigureAwait(false);
			return doc.XPathSelectElements(expression);
		}

		/// <summary>
		/// Parses XML-formatted HTTP response body into a collection of XElements. Intended to chain off an async call.
		/// </summary>
		/// <returns>A Task whose result is a collection of XElements from an XDocument containing XML data from the response body.</returns>
		/// <example>d = await url.PostAsync(data).ReceiveXElementsFromXPath(xpathExpression, namespaceResolver)</example>
		public static async Task<IEnumerable<XElement>> ReceiveXElementsFromXPath(this Task<HttpResponseMessage> response, string expression, IXmlNamespaceResolver resolver)
		{
			var doc = await response.ReceiveXDocument().ConfigureAwait(false);
			return doc.XPathSelectElements(expression, resolver);
		}
	}
}
