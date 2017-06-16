using System.Collections.Generic;
using System.Net.Http;

namespace Flurl.Http.Xml.Tests
{
    public abstract class TestBase
    {
        public enum HttpMethodTypes
        {
            Post,
            Put
        }

        protected readonly Dictionary<HttpMethodTypes, HttpMethod> HttpMethodByType = new Dictionary<HttpMethodTypes, HttpMethod>
        {
            [HttpMethodTypes.Post] = HttpMethod.Post,
            [HttpMethodTypes.Put] = HttpMethod.Put
        };
    }
}
