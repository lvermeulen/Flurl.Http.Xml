using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Flurl.Http.Xml;

/// <summary>
/// UrlExtensions
/// </summary>
public static class UrlExtensions
{
    /// <summary>
    /// Sends an asynchronous GET request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url">The URL.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>
    /// A Task whose result is the XML response body deserialized to an object of type T.
    /// </returns>
    public static Task<T> GetXmlAsync<T>(this Url url, 
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) => 
        new FlurlRequest(url).GetXmlAsync<T>(completionOption, cancellationToken);

    /// <summary>
    /// Sends an asynchronous GET request.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>A Task whose result is the XML response body parsed into an XDocument.</returns>
    public static Task<XDocument> GetXDocumentAsync(this Url url, 
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) => 
        new FlurlRequest(url).GetXDocumentAsync(completionOption, cancellationToken);

    /// <summary>
    /// Sends an asynchronous GET request.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
    public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this Url url, string expression, 
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) => 
        new FlurlRequest(url).GetXElementsFromXPath(expression, cancellationToken, completionOption);

    /// <summary>
    /// Sends an asynchronous GET request.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="resolver">The resolver.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>A Task whose result is the XML response body parsed into a collection of XElements.</returns>
    public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this Url url, string expression, IXmlNamespaceResolver resolver, 
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) => 
        new FlurlRequest(url).GetXElementsFromXPath(expression, resolver, cancellationToken, completionOption);

    /// <summary>
    /// Creates a FlurlClient from the URL and sends an asynchronous HTTP request.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="method">HTTP method of the request</param>
    /// <param name="data">Contents of the request body.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>A Task whose result is the received IFlurlResponse.</returns>
    public static Task<IFlurlResponse> SendXmlAsync(this Url url, HttpMethod method, object data, 
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) => 
        new FlurlRequest(url).SendXmlAsync(method, data, cancellationToken, completionOption);

    /// <summary>
    /// Creates a FlurlClient from the URL and sends an asynchronous POST request.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="data">Contents of the request body.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>A Task whose result is the received IFlurlResponse.</returns>
    public static Task<IFlurlResponse> PostXmlAsync(this Url url, object data, 
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) => 
        new FlurlRequest(url).PostXmlAsync(data, cancellationToken, completionOption);

    /// <summary>
    /// Creates a FlurlClient from the URL and sends an asynchronous PUT request.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="data">Contents of the request body.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>A Task whose result is the received IFlurlResponse.</returns>
    public static Task<IFlurlResponse> PutXmlAsync(this Url url, object data, 
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) => 
        new FlurlRequest(url).PutXmlAsync(data, cancellationToken, completionOption);
}