namespace Flurl.Http.Xml.Tests.Factories
{
    public class JsonTestModelHttpClientFactory : BaseTestModelClientFactory
    {
        private const string REQUEST_BODY_JSON = @"{
  ""number"": 3,
  ""text"": ""Test""
}";

        public JsonTestModelHttpClientFactory()
            : base(REQUEST_BODY_JSON)
        { }
    }
}