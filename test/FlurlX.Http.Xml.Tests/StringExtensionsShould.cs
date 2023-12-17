using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Flurl.Http.Testing;
using FlurlX.Http.Xml.Tests.Models;
using Xunit;
using Xunit.Extensions.Ordering;

namespace FlurlX.Http.Xml.Tests
{
    public class StringExtensionsShould : TestBase
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

        [Fact, Order(2)]
        public async Task GetXmlAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await "https://some.url"
                .GetXmlAsync<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Fact, Order(2)]
        public async Task GetXDocumentAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await "https://some.url"
                .GetXDocumentAsync();

            AssertXDocument(result, 3, "Test");
        }

        [Fact, Order(2)]
        public async Task GetXElementsFromXPathAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await "https://some.url"
                .GetXElementsFromXPath("/TestModel");

            AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
        }

        [Fact, Order(2)]
        public async Task GetXElementsFromXPathNamespaceResolverAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await "https://some.url"
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
            var result = await "http://some.url"
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
            var result = await "http://some.url"
                .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task PostXmlToModelAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await "http://some.url"
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task PostXmlToXDocumentAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await "http://some.url"
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }

        [Fact]
        public async Task PutXmlToModelAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await "http://some.url"
                .PutXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            AssertTestModel(result, 3, "Test");
        }

        [Fact]
        public async Task PutXmlToXDocumentAsync()
        {
            using var httpTest = new HttpTest();

            httpTest.RespondWith(REQUEST_BODY_XML);

            var result = await "http://some.url"
                .PutXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument();

            AssertXDocument(result, 3, "Test");
        }
    }
}
