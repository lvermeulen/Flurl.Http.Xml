using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Flurl.Http.Xml
{
    public static class UrlExtensions
    {
        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task whose result is the XML response body deserialized to an object of type T.</returns>
        public static Task<T> GetXmlAsync<T>(this Url url, CancellationToken cancellationToken)
        {
            return new FlurlClient(url, true).GetXmlAsync<T>(cancellationToken);
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <returns>A Task whose result is the XML response body deserialized to an object of type T.</returns>
        public static Task<T> GetXmlAsync<T>(this Url url)
        {
            return new FlurlClient(url, true).GetXmlAsync<T>();
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task whose result is the XML response body deserialized to a dynamic.</returns>
        public static Task<dynamic> GetXmlAsync(this Url url, CancellationToken cancellationToken)
        {
            return new FlurlClient(url, true).GetXmlAsync(cancellationToken);
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <returns>A Task whose result is the XML response body deserialized to a dynamic.</returns>
        public static Task<dynamic> GetXmlAsync(this Url url)
        {
            return new FlurlClient(url, true).GetXmlAsync();
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task whose result is the XML response body deserialized to a list of dynamics.</returns>
        public static Task<IList<dynamic>> GetXmlListAsync(this Url url, CancellationToken cancellationToken)
        {
            return new FlurlClient(url, true).GetXmlListAsync(cancellationToken);
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <returns>A Task whose result is the XML response body deserialized to a list of dynamics.</returns>
        public static Task<IList<dynamic>> GetXmlListAsync(this Url url)
        {
            return new FlurlClient(url, true).GetXmlListAsync();
        }

        /// <summary>
        /// Creates a FlurlClient from the URL and sends an asynchronous POST request.
        /// </summary>
        /// <param name="data">Contents of the request body.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task whose result is the received HttpResponseMessage.</returns>
        public static Task<HttpResponseMessage> PostXmlAsync(this Url url, object data, CancellationToken cancellationToken)
        {
            return new FlurlClient(url, true).PostXmlAsync(data, cancellationToken);
        }

        /// <summary>
        /// Creates a FlurlClient from the URL and sends an asynchronous POST request.
        /// </summary>
        /// <param name="data">Contents of the request body.</param>
        /// <returns>A Task whose result is the received HttpResponseMessage.</returns>
        public static Task<HttpResponseMessage> PostXmlAsync(this Url url, object data)
        {
            return new FlurlClient(url, true).PostXmlAsync(data);
        }

        ///// <summary>
        ///// Creates a FlurlClient from the URL and sends an asynchronous POST request.
        ///// </summary>
        ///// <param name="data">Contents of the request body.</param>
        ///// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        ///// <returns>A Task whose result is the received HttpResponseMessage.</returns>
        //public static Task<HttpResponseMessage> PostXmlAsync(this string url, object data, CancellationToken cancellationToken)
        //{
        //    return new FlurlClient(url, true).PostXmlAsync(data, cancellationToken);
        //}

        ///// <summary>
        ///// Creates a FlurlClient from the URL and sends an asynchronous POST request.
        ///// </summary>
        ///// <param name="data">Contents of the request body.</param>
        ///// <returns>A Task whose result is the received HttpResponseMessage.</returns>
        //public static Task<HttpResponseMessage> PostXmlAsync(this string url, object data)
        //{
        //    return new FlurlClient(url, true).PostXmlAsync(data);
        //}
    }
}
