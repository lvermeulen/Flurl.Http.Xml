using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Flurl.Http.Xml
{
    public static class FlurlClientExtensions
    {
        /// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>A Task whose result is the XML response body deserialized to an object of type T.</returns>
		public static Task<T> GetXmlAsync<T>(this FlurlClient client, CancellationToken cancellationToken)
        {
            return client.SendAsync(HttpMethod.Get, cancellationToken: cancellationToken).ReceiveXml<T>();
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <returns>A Task whose result is the XML response body deserialized to an object of type T.</returns>
        public static Task<T> GetXmlAsync<T>(this FlurlClient client)
        {
            return client.SendAsync(HttpMethod.Get).ReceiveXml<T>();
        }

        /// <summary>
		/// Sends an asynchronous GET request.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>A Task whose result is the XML response body parsed into an XDocument.</returns>
		public static Task<XDocument> GetXDocumentAsync(this FlurlClient client, CancellationToken cancellationToken)
        {
            return client.SendAsync(HttpMethod.Get, cancellationToken: cancellationToken).ReceiveXDocument();
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <returns>A Task whose result is the XML response body parsed into an XDocument.</returns>
        public static Task<XDocument> GetXDocumentAsync(this FlurlClient client)
        {
            return client.SendAsync(HttpMethod.Get).ReceiveXDocument();
        }

        /// <summary>
		/// Sends an asynchronous POST request.
		/// </summary>
		/// <param name="data">Contents of the request body.</param>
		/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
		/// <returns>A Task whose result is the received HttpResponseMessage.</returns>
		public static Task<HttpResponseMessage> PostXmlAsync(this FlurlClient client, object data, CancellationToken cancellationToken)
        {
            var content = new CapturedXmlContent(client.Settings.XmlSerializer().Serialize(data));
            return client.SendAsync(HttpMethod.Post, content: content, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Sends an asynchronous POST request.
        /// </summary>
        /// <param name="data">Contents of the request body.</param>
        /// <returns>A Task whose result is the received HttpResponseMessage.</returns>
        public static Task<HttpResponseMessage> PostXmlAsync(this FlurlClient client, object data)
        {
            var content = new CapturedXmlContent(client.Settings.XmlSerializer().Serialize(data));
            return client.SendAsync(HttpMethod.Post, content: content);
        }
    }
}
