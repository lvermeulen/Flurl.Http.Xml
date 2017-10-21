using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Flurl.Http.Xml
{
	/// <summary>
	/// UrlExtensions
	/// </summary>
	public static class UrlExtensions
	{
        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the XML response body deserialized to an object of type T.
        /// </returns>
        public static Task<T> GetXmlAsync<T>(this Url url, CancellationToken cancellationToken) => 
            new FlurlClient(url).GetXmlAsync<T>(cancellationToken);

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A Task whose result is the XML response body deserialized to an object of type T.</returns>
        public static Task<T> GetXmlAsync<T>(this Url url) => 
            new FlurlClient(url).GetXmlAsync<T>();

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task whose result is the XML response body parsed into an XDocument.</returns>
        public static Task<XDocument> GetXDocumentAsync(this Url url, CancellationToken cancellationToken) => 
            new FlurlClient(url).GetXDocumentAsync(cancellationToken);

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A Task whose result is the XML response body parsed into an XDocument.</returns>
        public static Task<XDocument> GetXDocumentAsync(this Url url) => 
            new FlurlClient(url).GetXDocumentAsync();

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this Url url, string expression, CancellationToken cancellationToken) => 
            new FlurlClient(url).GetXElementsFromXPath(expression, cancellationToken);

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this Url url, string expression) => 
            new FlurlClient(url).GetXElementsFromXPath(expression);

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="resolver">The resolver.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this Url url, string expression, IXmlNamespaceResolver resolver, CancellationToken cancellationToken) => 
            new FlurlClient(url).GetXElementsFromXPath(expression, resolver, cancellationToken);

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this Url url, string expression, IXmlNamespaceResolver resolver) => 
            new FlurlClient(url).GetXElementsFromXPath(expression, resolver);

	    /// <summary>
	    /// Creates a FlurlClient from the URL and sends an asynchronous HTTP request.
	    /// </summary>
	    /// <param name="url">The URL.</param>
	    /// <param name="method">HTTP method of the request</param>
	    /// <param name="data">Contents of the request body.</param>
	    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
	    /// <returns>A Task whose result is the received HttpResponseMessage.</returns>
	    public static Task<HttpResponseMessage> SendXmlAsync(this Url url, HttpMethod method, object data, CancellationToken cancellationToken) => 
            new FlurlClient(url).SendXmlAsync(method, data, cancellationToken);

        /// <summary>
        /// Creates a FlurlClient from the URL and sends an asynchronous HTTP request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="method">HTTP method of the request</param>
		/// <param name="data">Contents of the request body.</param>
        /// <returns>A Task whose result is the received HttpResponseMessage.</returns>
        public static Task<HttpResponseMessage> SendXmlAsync(this Url url, HttpMethod method, object data) => 
            new FlurlClient(url).SendXmlAsync(method, data);

	    /// <summary>
	    /// Creates a FlurlClient from the URL and sends an asynchronous POST request.
	    /// </summary>
	    /// <param name="url">The URL.</param>
	    /// <param name="data">Contents of the request body.</param>
	    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
	    /// <returns>A Task whose result is the received HttpResponseMessage.</returns>
	    public static Task<HttpResponseMessage> PostXmlAsync(this Url url, object data, CancellationToken cancellationToken) => 
            new FlurlClient(url).PostXmlAsync(data, cancellationToken);

	    /// <summary>
	    /// Creates a FlurlClient from the URL and sends an asynchronous POST request.
	    /// </summary>
	    /// <param name="url">The URL.</param>
	    /// <param name="data">Contents of the request body.</param>
	    /// <returns>A Task whose result is the received HttpResponseMessage.</returns>
	    public static Task<HttpResponseMessage> PostXmlAsync(this Url url, object data) => 
            new FlurlClient(url).PostXmlAsync(data);

	    /// <summary>
	    /// Creates a FlurlClient from the URL and sends an asynchronous PUT request.
	    /// </summary>
	    /// <param name="url">The URL.</param>
	    /// <param name="data">Contents of the request body.</param>
	    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
	    /// <returns>A Task whose result is the received HttpResponseMessage.</returns>
	    public static Task<HttpResponseMessage> PutXmlAsync(this Url url, object data, CancellationToken cancellationToken) => 
            new FlurlClient(url).PutXmlAsync(data, cancellationToken);

	    /// <summary>
	    /// Creates a FlurlClient from the URL and sends an asynchronous PUT request.
	    /// </summary>
	    /// <param name="url">The URL.</param>
	    /// <param name="data">Contents of the request body.</param>
	    /// <returns>A Task whose result is the received HttpResponseMessage.</returns>
	    public static Task<HttpResponseMessage> PutXmlAsync(this Url url, object data) => 
            new FlurlClient(url).PutXmlAsync(data);
	}
}