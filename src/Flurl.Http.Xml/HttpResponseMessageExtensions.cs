using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Flurl.Http.Xml
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Deserializes XML-formatted HTTP response body to object of type T. Intended to chain off an async HTTP.
        /// </summary>
        /// <typeparam name="T">A type whose structure matches the expected XML response.</typeparam>
        /// <returns>A Task whose result is an object containing data in the response body.</returns>
        /// <example>x = await url.PostAsync(data).ReceiveXml&lt;T&gt;()</example>
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
        /// Deserializes XML-formatted HTTP response body to a dynamic object. Intended to chain off an async call.
        /// </summary>
        /// <returns>A Task whose result is a dynamic object containing data in the response body.</returns>
        /// <example>d = await url.PostAsync(data).ReceiveXml()</example>
        public static async Task<dynamic> ReceiveXml(this Task<HttpResponseMessage> response)
        {
            return await response.ReceiveXml<ExpandoObject>().ConfigureAwait(false);
        }

        /// <summary>
        /// Deserializes XML-formatted HTTP response body to a list of dynamic objects. Intended to chain off an async call.
        /// </summary>
        /// <returns>A Task whose result is a list of dynamic objects containing data in the response body.</returns>
        /// <example>d = await url.PostAsync(data).ReceiveXmlList()</example>
        public static async Task<IList<dynamic>> ReceiveXmlList(this Task<HttpResponseMessage> response)
        {
            dynamic[] d = await response.ReceiveXml<ExpandoObject[]>().ConfigureAwait(false);
            return d;
        }
    }
}
