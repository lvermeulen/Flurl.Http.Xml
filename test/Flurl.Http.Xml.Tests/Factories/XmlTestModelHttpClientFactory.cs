namespace Flurl.Http.Xml.Tests.Factories
{
    public class XmlTestModelHttpClientFactory : BaseTestModelClientFactory
    {
        private const string REQUEST_BODY_XML = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TestModel xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Number>3</Number>
  <Text>Test</Text>
</TestModel>";

        public XmlTestModelHttpClientFactory()
            : base(REQUEST_BODY_XML)
        { }
    }
}