using System.Net.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

namespace Flurl.Http.Xml.Tests.Factories
{
    public abstract class BaseTestModelClientFactory : DefaultHttpClientFactory
    {
        private readonly string _responseBody;

        protected BaseTestModelClientFactory(string responseBody)
        {
            _responseBody = responseBody;
        }

        private HttpClient GetClient()
        {
            var builder = new WebHostBuilder().Configure(app =>
            {
                app.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync(_responseBody);
                });
            });

            var server = new TestServer(builder);
            return server.CreateClient();
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler) => GetClient();
    }
}
