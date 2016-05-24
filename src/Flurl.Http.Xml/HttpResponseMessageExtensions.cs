using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Flurl.Http.Xml
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Deserializes XML-formatted HTTP response body to object of type T. Intended to chain off an async HTTP.
        /// </summary>
        /// <typeparam name="T">A type whose structure matches the expected XML response.</typeparam>
        /// <returns>A Task whose result is an object containing data in the response body.</returns>
        /// <example>x = await url.PosAsync(data).ReceiveXml&lt;T&gt;()</example>
        public static async Task<T> ReceiveXml<T>(this Task<HttpResponseMessage> response)
        {
            var resp = await response.ConfigureAwait(false);
            var call = HttpCall.Get(resp.RequestMessage);
            try
            {
                using (var stream = await resp.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return call.Settings.XmlSerializer().Deserialize<T>(stream);
                }
            }
            catch (Exception ex)
            {
                string s = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
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
            var call = HttpCall.Get(resp.RequestMessage);
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
                string s = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                call.Exception = ex;
                throw new FlurlHttpException(call, s, ex);
            }
        }
    }
}
