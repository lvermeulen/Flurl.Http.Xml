using Flurl.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FlurlX.Http.Xml
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
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.  Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>
        /// A Task whose result is the XML response body deserialized to an object of type T.
        /// </returns>
        public static Task<T?> GetXmlAsync<T>(this string url,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => new FlurlRequest(url).GetXmlAsync<T>(completionOption, cancellationToken);

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>A Task whose result is the XML response body parsed into an XDocument.</returns>
        public static Task<XDocument> GetXDocumentAsync(this string url,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => new FlurlRequest(url).GetXDocumentAsync(completionOption, cancellationToken);

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
        public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this string url, string expression,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => new FlurlRequest(url).GetXElementsFromXPath(expression, completionOption, cancellationToken);

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="resolver">The resolver.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
        public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this string url, string expression, IXmlNamespaceResolver resolver,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => new FlurlRequest(url).GetXElementsFromXPath(expression, resolver, completionOption, cancellationToken);

        /// <summary>
        /// Creates a FlurlClient from the URL and sends an asynchronous HTTP request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>A Task whose result is the received IFlurlResponse.</returns>
        public static Task<IFlurlResponse> SendXmlAsync(this string url, HttpMethod method, object data,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
         ) => new FlurlRequest(url).SendXmlAsync(method, data, completionOption, cancellationToken);

        /// <summary>
        /// Creates a FlurlClient from the URL and sends an asynchronous POST request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>A Task whose result is the received IFlurlResponse.</returns>
        public static Task<IFlurlResponse> PostXmlAsync(this string url, object data,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => new FlurlRequest(url).PostXmlAsync(data, completionOption, cancellationToken);

        /// <summary>
        /// Creates a FlurlClient from the URL and sends an asynchronous PUT request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>A Task whose result is the received IFlurlResponse.</returns>
        public static Task<IFlurlResponse> PutXmlAsync(this string url, object data,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
         ) => new FlurlRequest(url).PutXmlAsync(data, completionOption, cancellationToken);
    }
}