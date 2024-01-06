using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Flurl.Http.Xml.Tests;

public class XmlTestModelHttpMiddleware : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }


        return Task.FromResult(new HttpResponseMessage
        {
            Content = new StringContent(@"<?xml version=""1.0"" encoding=""utf-8""?>
<TestModel xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Number>3</Number>
  <Text>Test</Text>
</TestModel>")
        });
    }
}