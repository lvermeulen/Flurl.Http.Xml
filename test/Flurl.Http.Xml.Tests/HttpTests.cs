using System.Threading.Tasks;
using System.Xml.Linq;
using Flurl.Http.Configuration;
using Flurl.Http.Xml.Tests.Models;
using Xunit;

namespace Flurl.Http.Xml.Tests
{
    public class HttpTests
    {
        [Fact]
        public async Task GetXDocument()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new DefaultHttpClientFactory());

            var result = await "https://query.yahooapis.com/v1/public/yql"
                .SetQueryParam("q", "select wind from weather.forecast where woeid=2460286")
                .SetQueryParam("format", "xml")
                .GetXDocumentAsync();

            string chill = result
                ?.Element("query")
                ?.Element("results")
                ?.Element("channel")
                ?.Element(XNamespace.Get("http://xml.weather.yahoo.com/ns/rss/1.0") + "wind")
                ?.Attribute("chill")
                ?.Value;

            Assert.NotNull(chill);
            Assert.NotEmpty(chill);
        }

        [Fact]
        public async Task PostXmlToModel()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new TestHttpClientFactory());

            var result = await "http://some.url"
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            Assert.Equal(3, result.Number);
            Assert.Equal("Test", result.Text);
        }

        [Fact]
        public async Task PostXmlToXDocument()
        {
            FlurlHttp.Configure(c => c.HttpClientFactory = new TestHttpClientFactory());

            var result = await "http://some.url"
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXDocument();

            Assert.Equal("3", result?.Element("TestModel")?.Element("Number")?.Value);
            Assert.Equal("Test", result?.Element("TestModel")?.Element("Text")?.Value);
        }
    }
}
