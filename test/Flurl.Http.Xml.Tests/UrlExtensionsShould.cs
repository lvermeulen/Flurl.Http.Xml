using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Flurl.Http.Xml.Tests.Factories;
using Flurl.Http.Xml.Tests.Models;
using Xunit;

namespace Flurl.Http.Xml.Tests
{
    public class UrlExtensionsShould : TestBase
    {
        private readonly CancellationToken _ct;

        public UrlExtensionsShould()
        {
            var cts = new CancellationTokenSource();
            _ct = cts.Token;
        }

        private void AssertTestModel(TestModel testModel, int expectedNumber, string expectedText)
        {
            Assert.Equal(expectedNumber, testModel.Number);
            Assert.Equal(expectedText, testModel.Text);
        }

        private void AssertXDocument(XDocument document, int expectedNumber, string expectedText)
        {
            Assert.Equal(expectedNumber.ToString(), document?.Element("TestModel")?.Element("Number")?.Value);
            Assert.Equal(expectedText, document?.Element("TestModel")?.Element("Text")?.Value);
        }

        [Fact]
        public async Task GetXmlAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new TestModelHttpClientFactory());

            var result = await new Url("https://some.url")
                .GetXmlAsync<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task GetXmlWithCancellationTokenAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new TestModelHttpClientFactory());

            var result = await new Url("https://some.url")
                .GetXmlAsync<TestModel>(_ct);

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task GetXDocumentAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new TestModelHttpClientFactory());

            var result = await new Url("https://some.url")
                .GetXDocumentAsync();

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task GetXDocumentWithCancellationTokenAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new TestModelHttpClientFactory());

            var result = await new Url("https://some.url")
                .GetXDocumentAsync(_ct);

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task GetXElementsFromXPathAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new TestModelHttpClientFactory());

            var result = await new Url("https://some.url")
                .GetXElementsFromXPath("/TestModel");

            AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
        }

        [Fact]
        public async Task GetXElementsFromXPathNamespaceResolverAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new TestModelHttpClientFactory());

            var result = await new Url("https://some.url")
                .GetXElementsFromXPath("/TestModel", new XmlNamespaceManager(new NameTable()));

            AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
        }

        [Fact]
        public async Task GetXElementsFromXPathWithCancellationTokenAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new TestModelHttpClientFactory());

            var result = await new Url("https://some.url")
                .GetXElementsFromXPath("/TestModel", _ct);

            AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
        }

        [Fact]
        public async Task GetXElementsFromXPathNamespaceResolverWithCancellationTokenAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new TestModelHttpClientFactory());

            var result = await new Url("https://some.url")
                .GetXElementsFromXPath("/TestModel", new XmlNamespaceManager(new NameTable()), _ct);

            AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
        }

        [Theory]
        [InlineData(HttpMethodTypes.Post)]
        [InlineData(HttpMethodTypes.Put)]
        public async Task SendXmlToModelAsync(HttpMethodTypes methodType)
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var method = HttpMethodByType[methodType];
            var result = await new Url("https://some.url")
                .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Theory]
        [InlineData(HttpMethodTypes.Post)]
        [InlineData(HttpMethodTypes.Put)]
        public async Task SendXmlToModelWithCancellationTokenAsync(HttpMethodTypes methodType)
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var method = HttpMethodByType[methodType];
            var result = await new Url("https://some.url")
                .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" }, _ct)
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Theory]
        [InlineData(HttpMethodTypes.Post)]
        [InlineData(HttpMethodTypes.Put)]
        public async Task SendXmlToXDocumentAsync(HttpMethodTypes methodType)
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var method = HttpMethodByType[methodType];
            var result = await new Url("https://some.url")
                .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Theory]
        [InlineData(HttpMethodTypes.Post)]
        [InlineData(HttpMethodTypes.Put)]
        public async Task SendXmlToXDocumentWithCancellationTokenAsync(HttpMethodTypes methodType)
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var method = HttpMethodByType[methodType];
            var result = await new Url("https://some.url")
                .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" }, _ct)
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task PostXmlToModelAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var result = await new Url("https://some.url")
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task PostXmlToModelWithCancellationTokenAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var result = await new Url("https://some.url")
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" }, _ct)
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task PostXmlToXDocumentAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var result = await new Url("https://some.url")
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task PostXmlToXDocumentWithCancellationTokenAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var result = await new Url("https://some.url")
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" }, _ct)
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task PutXmlToModelAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var result = await new Url("https://some.url")
                .PutXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task PutXmlToModelWithCancellationTokenAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var result = await new Url("https://some.url")
                .PutXmlAsync(new TestModel { Number = 3, Text = "Test" }, _ct)
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task PutXmlToXDocumentAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var result = await new Url("https://some.url")
                .PutXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task PutXmlToXDocumentWithCancellationTokenAsync()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var result = await new Url("https://some.url")
                .PutXmlAsync(new TestModel { Number = 3, Text = "Test" }, _ct)
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Theory]
        [InlineData("text/xml", "text/xml")]
        [InlineData("text/something+xml", "text/something+xml")]
        [InlineData(null, "application/xml")]
        public async Task ReceiveCorrectMediaType(string acceptMediaType, string expectedContentType)
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new EchoHttpClientFactory());

            var result = await new Url("https://some.url")
                .WithHeader("Accept", acceptMediaType)
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXmlResponseMessage();

            Assert.Equal(expectedContentType, result?.Content?.Headers?.ContentType?.MediaType);
        }
    }
}
