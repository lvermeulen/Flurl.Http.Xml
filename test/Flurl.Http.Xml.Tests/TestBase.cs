using System.Collections.Generic;
using System.Net.Http;
using Flurl.Http.Configuration;

namespace Flurl.Http.Xml.Tests;

public abstract class TestBase
{
    public FlurlClientBuilder XmlFlurlClientBuilder { get; }
    public FlurlClientBuilder JsonFlurlClientBuilder { get; }

    protected TestBase()
    {
        XmlFlurlClientBuilder = new FlurlClientBuilder()
            .AddMiddleware(() => new XmlTestModelHttpMiddleware()) as FlurlClientBuilder;
        JsonFlurlClientBuilder = new FlurlClientBuilder()
            .AddMiddleware(() => new JsonTestModelHttpMiddleware()) as FlurlClientBuilder;
    }

    public enum HttpMethodTypes
    {
        Post,
        Put
    }

    protected readonly Dictionary<HttpMethodTypes, HttpMethod> HttpMethodByType = new Dictionary<HttpMethodTypes, HttpMethod>
    {
        [HttpMethodTypes.Post] = HttpMethod.Post,
        [HttpMethodTypes.Put] = HttpMethod.Put
    };
}