using System.Collections.Generic;
using System.Net.Http;

namespace FlurlX.Http.Xml.Tests
{
    public abstract class TestBase
    {
        protected const string REQUEST_BODY_XML = @"<?xml version=""1.0"" encoding=""utf-8""?>
        <TestModel xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
          <Number>3</Number>
          <Text>Test</Text>
        </TestModel>";

        protected const string REQUEST_BODY_JSON = @"{
          ""number"": 3,
          ""text"": ""Test""
        }";

        public enum HttpMethodTypes
        {
            Post,
            Put
        }

        protected readonly Dictionary<HttpMethodTypes, HttpMethod> HttpMethodByType = new()
        {
            [HttpMethodTypes.Post] = HttpMethod.Post,
            [HttpMethodTypes.Put] = HttpMethod.Put
        };
    }
}
