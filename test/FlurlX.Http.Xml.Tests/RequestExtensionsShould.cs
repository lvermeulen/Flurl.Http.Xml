using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Flurl;
using Flurl.Http;
using Flurl.Http.Testing;
using FlurlX.Http.Xml.Tests.Models;
using Xunit;
using Xunit.Extensions.Ordering;

namespace FlurlX.Http.Xml.Tests
{
    public class RequestExtensionsShould : TestBase
    {
        private static void AssertTestModel(TestModel testModel, int expectedNumber, string expectedText)
        {
            Assert.Equal(expectedNumber, testModel.Number);
            Assert.Equal(expectedText, testModel.Text);
        }

        private static void AssertXDocument(XDocument document, int expectedNumber, string expectedText)
        {
            Assert.Equal(expectedNumber.ToString(), document?.Element("TestModel")?.Element("Number")?.Value);
            Assert.Equal(expectedText, document?.Element("TestModel")?.Element("Text")?.Value);
        }

        [Fact, Order(1)]
        public async Task GetXmlAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await FlurlRequestExtensions.GetXmlAsync<TestModel>(new Url("https://some.url")
                .AllowAnyHttpStatus()
);

            AssertTestModel(result, 3, "Test");
        }

        [Fact, Order(1)]
        public async Task GetXDocumentAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .AllowAnyHttpStatus()
                .GetXDocumentAsync();

            AssertXDocument(result, 3, "Test");
        }

        [Fact, Order(1)]
        public async Task GetXElementsFromXPathAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .AllowAnyHttpStatus()
                .GetXElementsFromXPath("/TestModel");

            AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
        }

        [Fact, Order(1)]
        public async Task GetXElementsFromXPathNamespaceResolverAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .AllowAnyHttpStatus()
                .GetXElementsFromXPath("/TestModel", new XmlNamespaceManager(new NameTable()));

            AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
        }

        [Theory]
        [InlineData(HttpMethodTypes.Post)]
        [InlineData(HttpMethodTypes.Put)]
        public async Task SendXmlToModelAsync(HttpMethodTypes methodType)
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var method = HttpMethodByType[methodType];
            var result = await new Url("https://some.url")
                .AllowAnyHttpStatus()
                .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Theory]
        [InlineData(HttpMethodTypes.Post)]
        [InlineData(HttpMethodTypes.Put)]
        public async Task SendXmlToXDocumentAsync(HttpMethodTypes methodType)
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var method = HttpMethodByType[methodType];
            var result = await new Url("https://some.url")
                .AllowAnyHttpStatus()
                .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task PostXmlToModelAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .AllowAnyHttpStatus()
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task PostXmlToXDocumentAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .AllowAnyHttpStatus()
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task PutXmlToModelAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .AllowAnyHttpStatus()
                .PutXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task PutXmlToXDocumentAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .AllowAnyHttpStatus()
                .PutXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Theory]
        [InlineData("", null, "application/xml")]
        [InlineData("Accept", "application/json", "application/json")]
        [InlineData("Accept", "text/something+xml", "text/something+xml")]
        [InlineData("Accept", "text/xml, application/xml", "text/xml")]
        [InlineData("Accept", null, "application/xml")]
        public async Task ReceiveCorrectMediaType(string headerName, string acceptMediaType, string expectedContentType)
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .WithHeader(headerName, acceptMediaType)
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXmlResponseMessage();

            Assert.Equal(expectedContentType, result?.ResponseMessage.Content?.Headers?.ContentType?.MediaType);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("Accept", "application/json")]
        [InlineData("Accept", "text/something+xml")]
        [InlineData("Accept", "text/xml, application/xml")]
        [InlineData("Accept", null)]
        public async Task ReceiveCorrectMediaTypeWithXmlResponse(string headerName, string acceptMediaType)
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await new Url("https://some.url")
                .WithHeader(headerName, acceptMediaType)
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Theory, Order(4)]
        [InlineData("", null)]
        [InlineData("Accept", "application/json")]
        [InlineData("Accept", "text/something+xml")]
        [InlineData("Accept", "text/xml, application/xml")]
        [InlineData("Accept", null)]
        public async Task ReceiveCorrectMediaTypeWithJsonResponse(string headerName, string acceptMediaType)
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_JSON);

            var result = await new Url("https://some.url")
                .WithHeader(headerName, acceptMediaType)
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveJson<TestModel>();

            AssertTestModel(result, 3, "Test");
        }
    }
}
