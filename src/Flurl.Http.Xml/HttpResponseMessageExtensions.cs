using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Flurl.Http.Xml
{
    /// <summary>
    /// IFlurlResponseExtensions
    /// </summary>
    public static class IFlurlResponseExtensions
    {
        private static FlurlCall GetHttpCall(HttpRequestMessage request)
        {
            if (request?.Properties != null && request.Properties.TryGetValue("FlurlHttpCall", out var obj) && obj is FlurlCall call)
            {
                return call;
            }
            return null;
        }

        private static string GetMediaType(HttpRequestMessage request)
        {
            if (request.Headers.Accept.Any())
            {
                // return media type of first accepted media type containing "xml", else of first accepted media type
                var acceptHeader = request.Headers.Accept.FirstOrDefault(x => x.MediaType.IndexOf("xml", StringComparison.OrdinalIgnoreCase) >= 0) 
                    ?? request.Headers.Accept.FirstOrDefault();

                return acceptHeader?.MediaType;
            }

            // no accepted media type present, return default
            return "application/xml";
        }

        /// <summary>
        /// Receives XML-formatted HTTP response body. Intended to chain off an async HTTP.
        /// </summary>
        /// <param name="responseMessage">The response.</param>
        /// <returns>A Task whose result is a response message containing data in the response body.</returns>
        /// <example>x = await url.PostAsync(data).ReceiveXmlResponseMessage()</example>
        public static async Task<IFlurlResponse> ReceiveXmlResponseMessage(this Task<IFlurlResponse> responseMessage)
        {
            var response = await responseMessage.ConfigureAwait(false);
            response.ResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(GetMediaType(response.ResponseMessage.RequestMessage));

            return response;
        }

        private static async Task<T> ReceiveFromXmlStream<T>(this Task<IFlurlResponse> response, Func<FlurlCall, Stream, T> streamHandler)
        {
            var resp = await ReceiveXmlResponseMessage(response);
            var call = GetHttpCall(resp.ResponseMessage.RequestMessage);

            try
            {
                using (var stream = await resp.ResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return streamHandler(call, stream);
                }
            }
            catch (Exception ex)
            {
                string s = await resp.ResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new FlurlHttpException(call, s, ex);
            }
        }

        /// <summary>
        /// Deserializes XML-formatted HTTP response body to object of type T. Intended to chain off an async HTTP.
        /// </summary>
        /// <typeparam name="T">A type whose structure matches the expected XML response.</typeparam>
        /// <param name="response">The response.</param>
        /// <returns>A Task whose result is an object containing data in the response body.</returns>
        /// <example>x = await url.PostAsync(data).ReceiveXml&lt;T&gt;()</example>
        public static async Task<T> ReceiveXml<T>(this Task<IFlurlResponse> response)
        {
            return await ReceiveFromXmlStream(response, (call, stm) => 
                call.Request.Settings.XmlSerializer().Deserialize<T>(stm));
        }

        /// <summary>
        /// Parses XML-formatted HTTP response body into an XDocument. Intended to chain off an async call.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>A Task whose result is an XDocument containing XML data from the response body.</returns>
        /// <example>d = await url.PostAsync(data).ReceiveXDocument()</example>
        public static async Task<XDocument> ReceiveXDocument(this Task<IFlurlResponse> response)
        {
            return await ReceiveFromXmlStream(response, (call, stm) =>
            {
                using (var streamReader = new StreamReader(stm))
                {
                    return XDocument.Parse(streamReader.ReadToEnd());
                }
            });
        }

        /// <summary>
        /// Parses XML-formatted HTTP response body into a collection of XElements. Intended to chain off an async call.
        /// </summary>
        /// <returns>A Task whose result is a collection of XElements from an XDocument containing XML data from the response body.</returns>
        /// <example>d = await url.PostAsync(data).ReceiveXElementsFromXPath(xpathExpression)</example>
        public static async Task<IEnumerable<XElement>> ReceiveXElementsFromXPath(this Task<IFlurlResponse> response, string expression)
        {
            var doc = await response.ReceiveXDocument().ConfigureAwait(false);
            return doc.XPathSelectElements(expression);
        }

        /// <summary>
        /// Parses XML-formatted HTTP response body into a collection of XElements. Intended to chain off an async call.
        /// </summary>
        /// <returns>A Task whose result is a collection of XElements from an XDocument containing XML data from the response body.</returns>
        /// <example>d = await url.PostAsync(data).ReceiveXElementsFromXPath(xpathExpression, namespaceResolver)</example>
        public static async Task<IEnumerable<XElement>> ReceiveXElementsFromXPath(this Task<IFlurlResponse> response, string expression, IXmlNamespaceResolver resolver)
        {
            var doc = await response.ReceiveXDocument().ConfigureAwait(false);
            return doc.XPathSelectElements(expression, resolver);
        }
    }
}
