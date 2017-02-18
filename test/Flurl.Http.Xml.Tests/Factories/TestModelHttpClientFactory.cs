using System.Net.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

namespace Flurl.Http.Xml.Tests.Factories
{
    public class TestModelHttpClientFactory : DefaultHttpClientFactory
    {
        private const string REQUEST_BODY = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TestModel xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Number>3</Number>
  <Text>Test</Text>
</TestModel>";

        private HttpClient GetClient()
        {
            var builder = new WebHostBuilder().Configure(app =>
            {
                app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync(REQUEST_BODY);
                });
            });

            var server = new TestServer(builder);
            return server.CreateClient();
        }

        public override HttpClient CreateClient(Url url, HttpMessageHandler handler) => GetClient();
    }
}