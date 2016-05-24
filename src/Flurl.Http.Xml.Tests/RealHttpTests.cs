using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http.Xml.Tests.Models;
using NUnit.Framework;

namespace Flurl.Http.Xml.Tests
{
    [TestFixture]
    public class RealHttpTests
    {
        [Test]
        public async Task GetXml()
        {
            var result = await "https://query.yahooapis.com/v1/public/yql"
                .SetQueryParam("q", "select wind from weather.forecast where woeid=2460286")
                .SetQueryParam("format", "xml")
                .GetXDocumentAsync();

            Assert.IsNotNull(result);
            //var z = result
            //    .Element("query")
            //    ?.Element("results")
            //    ?.Element("channel")
            //    ?.Element("yweather:wind")
            //    ;
            //string s = z.Value;

            //Assert.IsNotNullOrEmpty(
            //    s

            //    //?.Element("channel")
            //    //?.Element("wind")
            //    //?.Element("chill")
            //    //?.Value
            //);
        }

        [Test]
        public async Task PostXml()
        {
            var result = await "http://httpbin.org/post"
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXmlFromJsonAsync<TestModel>(json => json.data);

            Assert.AreEqual(3, result.Number);
            Assert.AreEqual("Test", result.Text);
        }
    }
}
