using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FlurlX.Http.Xml
{
    /// <summary>
    /// FlurlRequestExtensions
    /// </summary>
    public static class FlurlRequestExtensions
    {

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>
        /// A Task whose result is the XML response body parsed into an XDocument.
        /// </returns>
        public static Task<XDocument> GetXDocumentAsync(this IFlurlRequest request,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => request.SendAsync(HttpMethod.Get, null, cancellationToken: cancellationToken, completionOption: completionOption).ReceiveXDocument();

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>
        /// A Task whose result is the XML response body parsed into a collection of XElements.
        /// </returns>
        public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlRequest request, string expression,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => request.SendAsync(HttpMethod.Get, null, cancellationToken: cancellationToken, completionOption: completionOption).ReceiveXElementsFromXPath(expression);

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="resolver">The resolver.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>
        /// A Task whose result is the XML response body parsed into a collection of XElements.
        /// </returns>
        public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlRequest request,
            string expression,
            IXmlNamespaceResolver resolver,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => request.SendAsync(HttpMethod.Get, null, cancellationToken: cancellationToken, completionOption: completionOption).ReceiveXElementsFromXPath(expression, resolver);

        private static string GetMediaType(this IHeadersContainer request)
        {
            const string defaultDediaType = "application/xml";

            var acceptHeaders = request.Headers
                .Where(x => x.Name == "Accept")
                .ToList();

            if (acceptHeaders.Count == 0 || acceptHeaders.All(x => x.Value == null))
            {
                // no accepted media type present, return default
                return defaultDediaType;
            }

            // return media type of first accepted media type containing "xml", else of first accepted media type
            var mediaTypes = acceptHeaders
                .Where(x => x.Value != null)
                .SelectMany(x => x.Value.ToString().Split(','))
                .Select(x => x.Trim())
                .ToList();

            string? result = mediaTypes.FirstOrDefault(x => x.Contains("xml", StringComparison.OrdinalIgnoreCase))
                ?? mediaTypes.FirstOrDefault();

            return result ?? defaultDediaType;
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>
        /// A Task whose result is the XML response body deserialized to an object of type T.
        /// </returns>
        public static Task<T?> GetXmlAsync<T>(this IFlurlRequest request,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => request.SendAsync(HttpMethod.Get, null, cancellationToken: cancellationToken, completionOption: completionOption).ReceiveXml<T?>();

        /// <summary>
        /// Sends an asynchronous HTTP request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="httpMethod">HTTP method of the request</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>
        /// A Task whose result is the received IFlurlResponse.
        /// </returns>
        public static Task<IFlurlResponse> SendXmlAsync(this IFlurlRequest request,
            HttpMethod httpMethod,
            object data,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        )
        {
            var content = new CapturedXmlContent(FlurlHttpSettingsExtensions.XmlSerializer().Serialize(data), request.GetMediaType());
            return request.SendAsync(httpMethod, content, cancellationToken: cancellationToken, completionOption: completionOption);
        }

        /// <summary>
        /// Sends an asynchronous POST request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>
        /// A Task whose result is the received IFlurlResponse.
        /// </returns>
        public static Task<IFlurlResponse> PostXmlAsync(this IFlurlRequest request,
            object data,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => SendXmlAsync(request, HttpMethod.Post, data, completionOption, cancellationToken);

        /// <summary>
        /// Sends an asynchronous PUT request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>
        /// A Task whose result is the received IFlurlResponse.
        /// </returns>
        public static Task<IFlurlResponse> PutXmlAsync(this IFlurlRequest request,
            object data,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default
        ) => SendXmlAsync(request, HttpMethod.Put, data, completionOption, cancellationToken);
    }
}
