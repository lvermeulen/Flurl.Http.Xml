using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Flurl.Http.Xml.Tests
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReceiveXmlFromJsonAsync<T>(this Task<HttpResponseMessage> response, Func<dynamic, string> xmlExtractor)
        {
            var resp = await response.ConfigureAwait(false);
            var json = await response.ReceiveJson();
            resp.Content = new CapturedXmlContent(xmlExtractor(json));

            return await Task.FromResult(resp).ReceiveXml<T>();
        }
    }
}
