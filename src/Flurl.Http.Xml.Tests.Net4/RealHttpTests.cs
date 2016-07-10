using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Flurl.Http.Xml.Tests.Models;
using NUnit.Framework;

namespace Flurl.Http.Xml.Tests
{
    [TestFixture]
    public class RealHttpTests
    {
        [Test]
        public async Task GetXDocument()
        {
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

            Assert.IsNotNullOrEmpty(chill);
        }

        [Test]
        public async Task PostXmlToModel()
        {
            var result = await "http://putsreq.com/JMG62khbf6IWRR8Bz9nU"
                .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
                .ReceiveXml<TestModel>();

            Assert.AreEqual(3, result.Number);
            Assert.AreEqual("Test", result.Text);
        }

        [Test]
        public async Task PostXmlToXDocument()
        {
            var result = await "http://putsreq.com/JMG62khbf6IWRR8Bz9nU"
                .PostXmlAsync(new TestModel {Number = 3, Text = "Test"})
                .ReceiveXDocument();

            Assert.AreEqual("3", result?.Element("TestModel")?.Element("Number")?.Value);
            Assert.AreEqual("Test", result?.Element("TestModel")?.Element("Text")?.Value);
        }
    }
}
