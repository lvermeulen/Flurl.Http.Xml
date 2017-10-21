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
    /// FlurlRequestExtensions
    /// </summary>
    public static class FlurlRequestExtensions
    {
        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the XML response body deserialized to an object of type T.
        /// </returns>
        public static async Task<T> GetXmlAsync<T>(this IFlurlRequest request, CancellationToken cancellationToken)
        {
            return await request.SendAsync(HttpMethod.Get, cancellationToken: cancellationToken).ReceiveXml<T>();
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task whose result is the XML response body deserialized to an object of type T.</returns>
        public static async Task<T> GetXmlAsync<T>(this IFlurlRequest request)
        {
            return await request.SendAsync(HttpMethod.Get).ReceiveXml<T>();
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the XML response body parsed into an XDocument.
        /// </returns>
        public static async Task<XDocument> GetXDocumentAsync(this IFlurlRequest request, CancellationToken cancellationToken)
        {
            return await request.SendAsync(HttpMethod.Get, cancellationToken: cancellationToken).ReceiveXDocument();
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task whose result is the XML response body parsed into an XDocument.</returns>
        public static async Task<XDocument> GetXDocumentAsync(this IFlurlRequest request)
        {
            return await request.SendAsync(HttpMethod.Get).ReceiveXDocument();
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the XML response body parsed into a collection of XElements.
        /// </returns>
        public static async Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlRequest request, string expression, CancellationToken cancellationToken)
        {
            return await request.SendAsync(HttpMethod.Get, cancellationToken: cancellationToken).ReceiveXElementsFromXPath(expression);
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
        public static async Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlRequest request, string expression)
        {
            return await request.SendAsync(HttpMethod.Get).ReceiveXElementsFromXPath(expression);
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="resolver">The resolver.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the XML response body parsed into a collection of XElements.
        /// </returns>
        public static async Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlRequest request, string expression, IXmlNamespaceResolver resolver, CancellationToken cancellationToken)
        {
            return await request.SendAsync(HttpMethod.Get, cancellationToken: cancellationToken).ReceiveXElementsFromXPath(expression, resolver);
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
        public static async Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlRequest request, string expression, IXmlNamespaceResolver resolver)
        {
            return await request.SendAsync(HttpMethod.Get).ReceiveXElementsFromXPath(expression, resolver);
        }

        internal static string GetMediaType(this IFlurlRequest request)
        {
            var acceptHeaders = request.Headers
                .Where(x => x.Key == "Accept")
                .ToList();

            if (!acceptHeaders.Any() || acceptHeaders.All(x => x.Value == null))
            {
                // no accepted media type present, return default
                return "application/xml";
            }

            // return media type of first accepted media type containing "xml", else of first accepted media type
            var mediaTypes = acceptHeaders
                .Where(x => x.Value != null)
                .SelectMany(x => x.Value.ToString().Split(','))
                .Select(x => x.Trim())
                .ToList();

            return mediaTypes.First(x => x.IndexOf("xml", StringComparison.OrdinalIgnoreCase) >= 0) 
                ?? mediaTypes.First();
        }

        /// <summary>
        /// Sends an asynchronous HTTP request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="httpMethod">HTTP method of the request</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the received HttpResponseMessage.
        /// </returns>
        public static async Task<HttpResponseMessage> SendXmlAsync(this IFlurlRequest request, HttpMethod httpMethod, object data, CancellationToken cancellationToken)
        {
            var content = new CapturedXmlContent(request.Settings.XmlSerializer().Serialize(data), request.GetMediaType());
            return await request.SendAsync(httpMethod, content: content, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Sends an asynchronous HTTP request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="httpMethod">HTTP method of the request</param>
	    /// <param name="data">Contents of the request body.</param>
        /// <returns>
        /// A Task whose result is the received HttpResponseMessage.
        /// </returns>
        public static async Task<HttpResponseMessage> SendXmlAsync(this IFlurlRequest request, HttpMethod httpMethod, object data)
        {
            var content = new CapturedXmlContent(request.Settings.XmlSerializer().Serialize(data), request.GetMediaType());
            return await request.SendAsync(httpMethod, content: content);
        }

        /// <summary>
        /// Sends an asynchronous POST request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the received HttpResponseMessage.
        /// </returns>
        public static Task<HttpResponseMessage> PostXmlAsync(this IFlurlRequest request, object data, CancellationToken cancellationToken) =>
            SendXmlAsync(request, HttpMethod.Post, data, cancellationToken);

        /// <summary>
        /// Sends an asynchronous POST request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <returns>
        /// A Task whose result is the received HttpResponseMessage.
        /// </returns>
        public static Task<HttpResponseMessage> PostXmlAsync(this IFlurlRequest request, object data) =>
            SendXmlAsync(request, HttpMethod.Post, data);

        /// <summary>
        /// Sends an asynchronous PUT request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A Task whose result is the received HttpResponseMessage.
        /// </returns>
        public static Task<HttpResponseMessage> PutXmlAsync(this IFlurlRequest request, object data, CancellationToken cancellationToken) =>
            SendXmlAsync(request, HttpMethod.Put, data, cancellationToken);

        /// <summary>
        /// Sends an asynchronous PUT request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <returns>
        /// A Task whose result is the received HttpResponseMessage.
        /// </returns>
        public static Task<HttpResponseMessage> PutXmlAsync(this IFlurlRequest request, object data) =>
            SendXmlAsync(request, HttpMethod.Put, data);
    }
}
