using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Flurl.Http.Xml
{
	/// <summary>
	/// StringExtensions
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="url">The URL.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads 
		/// to receive notice of cancellation.</param>
		/// <returns>
		/// A Task whose result is the XML response body deserialized to an object of type T.
		/// </returns>
		public static Task<T> GetXmlAsync<T>(this string url, CancellationToken cancellationToken) 
			=> new FlurlClient(url, true).GetXmlAsync<T>(cancellationToken);

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <returns>A Task whose result is the XML response body deserialized to an object of type T.</returns>
		public static Task<T> GetXmlAsync<T>(this string url) => new FlurlClient(url, true).GetXmlAsync<T>();

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads 
		/// to receive notice of cancellation.</param>
		/// <returns>A Task whose result is the XML response body parsed into an XDocument.</returns>
		public static Task<XDocument> GetXDocumentAsync(this string url, CancellationToken cancellationToken) 
			=> new FlurlClient(url, true).GetXDocumentAsync(cancellationToken);

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <returns>A Task whose result is the XML response body parsed into an XDocument.</returns>
		public static Task<XDocument> GetXDocumentAsync(this string url) => new FlurlClient(url, true).GetXDocumentAsync();

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this string url, string expression, 
			CancellationToken cancellationToken) 
			=> new FlurlClient(url, true).GetXElementsFromXPath(expression, cancellationToken);

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this string url, string expression) 
			=> new FlurlClient(url, true).GetXElementsFromXPath(expression);

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="resolver">The resolver.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this string url, string expression, 
			IXmlNamespaceResolver resolver, CancellationToken cancellationToken) 
			=> new FlurlClient(url, true).GetXElementsFromXPath(expression, resolver, cancellationToken);

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this string url, string expression, 
			IXmlNamespaceResolver resolver) => new FlurlClient(url, true).GetXElementsFromXPath(expression, resolver);

		/// <summary>
		/// Creates a FlurlClient from the URL and sends an asynchronous POST request.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="data">Contents of the request body.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>A Task whose result is the received HttpResponseMessage.</returns>
		public static Task<HttpResponseMessage> PostXmlAsync(this string url, object data, 
			CancellationToken cancellationToken) => new FlurlClient(url, false).PostXmlAsync(data, cancellationToken);

		/// <summary>
		/// Creates a FlurlClient from the URL and sends an asynchronous POST request.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="data">Contents of the request body.</param>
		/// <returns>A Task whose result is the received HttpResponseMessage.</returns>
		public static Task<HttpResponseMessage> PostXmlAsync(this string url, object data) 
			=> new FlurlClient(url, false).PostXmlAsync(data);
	}
}