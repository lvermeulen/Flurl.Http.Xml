using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Flurl.Http.Xml
{
	/// <summary>
	/// FlurlClientExtensions
	/// </summary>
	public static class FlurlClientExtensions
	{
	    /// <summary>
	    /// Sends an asynchronous GET request.
	    /// </summary>
	    /// <typeparam name="T"></typeparam>
	    /// <param name="client">The client.</param>
	    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
	    /// <returns>
	    /// A Task whose result is the XML response body deserialized to an object of type T.
	    /// </returns>
	    public static Task<T> GetXmlAsync<T>(this IFlurlClient client, CancellationToken cancellationToken)
		{
			return client.SendAsync(HttpMethod.Get, cancellationToken: cancellationToken).ReceiveXml<T>();
		}

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>A Task whose result is the XML response body deserialized to an object of type T.</returns>
        public static Task<T> GetXmlAsync<T>(this IFlurlClient client)
		{
			return client.SendAsync(HttpMethod.Get).ReceiveXml<T>();
		}

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the XML response body parsed into an XDocument.
        /// </returns>
        public static Task<XDocument> GetXDocumentAsync(this IFlurlClient client, CancellationToken cancellationToken)
		{
			return client.SendAsync(HttpMethod.Get, cancellationToken: cancellationToken).ReceiveXDocument();
		}

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>A Task whose result is the XML response body parsed into an XDocument.</returns>
        public static Task<XDocument> GetXDocumentAsync(this IFlurlClient client)
		{
			return client.SendAsync(HttpMethod.Get).ReceiveXDocument();
		}

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>
		/// A Task whose result is the XML response body parsed into a collection of XElements.
		/// </returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this FlurlClient client, string expression, CancellationToken cancellationToken)
		{
			return client.SendAsync(HttpMethod.Get, cancellationToken: cancellationToken).ReceiveXElementsFromXPath(expression);
		}

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlClient client, string expression)
		{
			return client.SendAsync(HttpMethod.Get).ReceiveXElementsFromXPath(expression);
		}

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="resolver">The resolver.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>
		/// A Task whose result is the XML response body parsed into a collection of XElements.
		/// </returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlClient client, string expression, IXmlNamespaceResolver resolver, CancellationToken cancellationToken)
		{
			return client.SendAsync(HttpMethod.Get, cancellationToken: cancellationToken).ReceiveXElementsFromXPath(expression, resolver);
		}

		/// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
		public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlClient client, string expression, IXmlNamespaceResolver resolver)
		{
			return client.SendAsync(HttpMethod.Get).ReceiveXElementsFromXPath(expression, resolver);
		}

        private static string GetMediaType(this IFlurlClient client)
	    {
	        if (client.HttpClient.DefaultRequestHeaders.Accept.Any())
	        {
	            // return media type of first accepted media type containing "xml", else of first accepted media type
	            var requestHeaders = client.HttpClient.DefaultRequestHeaders;
                var accept = requestHeaders.Accept.First(x => x.MediaType.IndexOf("xml", StringComparison.OrdinalIgnoreCase) >= 0)
	                ?? requestHeaders.Accept.First();

	            return accept.MediaType;
	        }

            // no accepted media type present, return default
	        return "application/xml";
        }

	    /// <summary>
	    /// Sends an asynchronous HTTP request.
	    /// </summary>
	    /// <param name="client">The client.</param>
	    /// <param name="httpMethod">HTTP method of the request</param>
	    /// <param name="data">Contents of the request body.</param>
	    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
	    /// <returns>
	    /// A Task whose result is the received HttpResponseMessage.
	    /// </returns>
	    public static Task<HttpResponseMessage> SendXmlAsync(this IFlurlClient client, HttpMethod httpMethod, object data, CancellationToken cancellationToken)
	    {
	        var content = new CapturedXmlContent(client.Settings.XmlSerializer().Serialize(data), client.GetMediaType());
	        return client.SendAsync(httpMethod, content: content, cancellationToken: cancellationToken);
	    }

        /// <summary>
        /// Sends an asynchronous HTTP request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="httpMethod">HTTP method of the request</param>
	    /// <param name="data">Contents of the request body.</param>
        /// <returns>
        /// A Task whose result is the received HttpResponseMessage.
        /// </returns>
        public static Task<HttpResponseMessage> SendXmlAsync(this IFlurlClient client, HttpMethod httpMethod, object data)
	    {
	        var content = new CapturedXmlContent(client.Settings.XmlSerializer().Serialize(data), client.GetMediaType());
	        return client.SendAsync(httpMethod, content: content);
	    }

        /// <summary>
        /// Sends an asynchronous POST request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the received HttpResponseMessage.
        /// </returns>
        public static Task<HttpResponseMessage> PostXmlAsync(this IFlurlClient client, object data, CancellationToken cancellationToken) => 
            SendXmlAsync(client, HttpMethod.Post, data, cancellationToken);

	    /// <summary>
		/// Sends an asynchronous POST request.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <param name="data">Contents of the request body.</param>
		/// <returns>
		/// A Task whose result is the received HttpResponseMessage.
		/// </returns>
		public static Task<HttpResponseMessage> PostXmlAsync(this IFlurlClient client, object data) =>
	        SendXmlAsync(client, HttpMethod.Post, data);

        /// <summary>
        /// Sends an asynchronous PUT request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the received HttpResponseMessage.
        /// </returns>
        public static Task<HttpResponseMessage> PutXmlAsync(this IFlurlClient client, object data, CancellationToken cancellationToken) =>
            SendXmlAsync(client, HttpMethod.Put, data, cancellationToken);

        /// <summary>
        /// Sends an asynchronous PUT request.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <returns>
        /// A Task whose result is the received HttpResponseMessage.
        /// </returns>
        public static Task<HttpResponseMessage> PutXmlAsync(this IFlurlClient client, object data) =>
	        SendXmlAsync(client, HttpMethod.Put, data);
	}
}