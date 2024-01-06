using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Flurl.Http.Xml.Tests;

public class JsonTestModelHttpMiddleware : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }


        return Task.FromResult(new HttpResponseMessage
        {
            Content = new StringContent(@"{
  ""number"": 3,
  ""text"": ""Test""
}")
        });
    }
}
