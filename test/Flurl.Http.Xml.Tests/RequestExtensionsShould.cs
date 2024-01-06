using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Flurl.Http.Xml.Tests.Models;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Flurl.Http.Xml.Tests;

public class RequestExtensionsShould : TestBase
{
    private void AssertTestModel(TestModel testModel, int expectedNumber, string expectedText)
    {
        Assert.NotNull(testModel);
        Assert.Equal(expectedNumber, testModel.Number);
        Assert.Equal(expectedText, testModel.Text);
    }

    private void AssertXDocument(XDocument document, int expectedNumber, string expectedText)
    {
        Assert.Equal(expectedNumber.ToString(), document?.Element("TestModel")?.Element("Number")?.Value);
        Assert.Equal(expectedText, document?.Element("TestModel")?.Element("Text")?.Value);
    }

    [Fact, Order(1)]
    public async Task GetXmlAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var request = await new FlurlRequest(new Url("https://some.url"))
            .WithVerb(HttpMethod.Get);
        var result = await flurlClient
            .SendAsync(request)
            .ReceiveXml<TestModel>();

        AssertTestModel(result, 3, "Test");
    }

    [Fact, Order(1)]
    public async Task GetXDocumentAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .GetXDocumentAsync();

        AssertXDocument(result, 3, "Test");
    }

    [Fact, Order(1)]
    public async Task GetXElementsFromXPathAsync()
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .GetXElementsFromXPath("/TestModel");

        AssertXDocument(result.FirstOrDefault()?.Document, 3, "Test");
    }

    [Fact, Order(1)]
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
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var method = HttpMethodByType[methodType];
        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .AllowAnyHttpStatus()
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

    [Theory]
    [InlineData("", null, "application/xml")]
    [InlineData("Accept", "application/json", "application/json")]
    [InlineData("Accept", "text/something+xml", "text/something+xml")]
    [InlineData("Accept", "text/xml, application/xml", "text/xml")]
    [InlineData("Accept", null, "application/xml")]
    public async Task ReceiveCorrectMediaType(string headerName, string acceptMediaType, string expectedContentType)
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .WithHeader(headerName, acceptMediaType)
            .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
            .ReceiveXmlResponseMessage();

        Assert.Equal(expectedContentType, result?.ResponseMessage.Content.Headers.ContentType?.MediaType);
    }

    [Theory]
    [InlineData("", null)]
    [InlineData("Accept", "application/json")]
    [InlineData("Accept", "text/something+xml")]
    [InlineData("Accept", "text/xml, application/xml")]
    [InlineData("Accept", null)]
    public async Task ReceiveCorrectMediaTypeWithXmlResponse(string headerName, string acceptMediaType)
    {
        using var flurlClient = XmlFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
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
        using var flurlClient = JsonFlurlClientBuilder.Build();

        var result = await flurlClient
            .Request(new Url("https://some.url"))
            .WithHeader(headerName, acceptMediaType)
            .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
            .ReceiveJson<TestModel>();

        AssertTestModel(result, 3, "Test");
    }
}
