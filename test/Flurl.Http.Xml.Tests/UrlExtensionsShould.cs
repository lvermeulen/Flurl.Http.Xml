using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Flurl.Http.Xml.Tests.Models;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Flurl.Http.Xml.Tests;

public class UrlExtensionsShould : TestBase
{
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

    [Fact, Order(3)]
    public async Task GetXmlAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .GetXmlAsync<TestModel>();

        AssertTestModel(result, 3, "Test");
    }

    [Fact, Order(3)]
    public async Task GetXDocumentAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .GetXDocumentAsync();

        AssertXDocument(result, 3, "Test");
    }

    [Fact, Order(3)]
    public async Task GetXElementsFromXPathAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .GetXElementsFromXPath("/TestModel");

        AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
    }

    [Fact, Order(3)]
    public async Task GetXElementsFromXPathNamespaceResolverAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .GetXElementsFromXPath("/TestModel", new XmlNamespaceManager(new NameTable()));

        AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
    }

    [Theory]
    [InlineData(HttpMethodTypes.Post)]
    [InlineData(HttpMethodTypes.Put)]
    public async Task SendXmlToModelAsync(HttpMethodTypes methodType)
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var method = HttpMethodByType[methodType];
        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" })
            .ReceiveXml<TestModel>();

        AssertTestModel(result, 3, "Test");
    }

    [Theory]
    [InlineData(HttpMethodTypes.Post)]
    [InlineData(HttpMethodTypes.Put)]
    public async Task SendXmlToXDocumentAsync(HttpMethodTypes methodType)
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var method = HttpMethodByType[methodType];
        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .SendXmlAsync(method, new TestModel { Number = 3, Text = "Test" })
            .ReceiveXDocument();

        AssertXDocument(result, 3, "Test");
    }

    [Fact]
    public async Task PostXmlToModelAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
            .ReceiveXml<TestModel>();

        AssertTestModel(result, 3, "Test");
    }

    [Fact]
    public async Task PostXmlToXDocumentAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
            .ReceiveXDocument();

        AssertXDocument(result, 3, "Test");
    }

    [Fact]
    public async Task PutXmlToModelAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .PutXmlAsync(new TestModel { Number = 3, Text = "Test" })
            .ReceiveXml<TestModel>();

        AssertTestModel(result, 3, "Test");
    }

    [Fact]
    public async Task PutXmlToXDocumentAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .PutXmlAsync(new TestModel { Number = 3, Text = "Test" })
            .ReceiveXDocument();

        AssertXDocument(result, 3, "Test");
    }
}