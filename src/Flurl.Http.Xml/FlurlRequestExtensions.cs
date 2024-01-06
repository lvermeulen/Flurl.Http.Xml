using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Flurl.Http.Xml;

/// <summary>
/// FlurlRequestExtensions
/// </summary>
public static class FlurlRequestExtensions
{
    /// <summary>
    /// Sends an asynchronous GET request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request">The request.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <returns>
    /// A Task whose result is the XML response body deserialized to an object of type T.
    /// </returns>
    public static Task<T> GetXmlAsync<T>(this IFlurlRequest request,
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        return request.SendAsync(HttpMethod.Get, null, completionOption, cancellationToken).ReceiveXml<T>();
    }

    /// <summary>
    /// Sends an asynchronous GET request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>
    /// A Task whose result is the XML response body parsed into an XDocument.
    /// </returns>
    public static Task<XDocument> GetXDocumentAsync(this IFlurlRequest request, 
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
    {
        return request.SendAsync(HttpMethod.Get, null, completionOption, cancellationToken).ReceiveXDocument();
    }

    /// <summary>
    /// Sends an asynchronous GET request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>
    /// A Task whose result is the XML response body parsed into a collection of XElements.
    /// </returns>
    public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlRequest request, string expression,
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
    {
        return request.SendAsync(HttpMethod.Get, null, completionOption, cancellationToken).ReceiveXElementsFromXPath(expression);
    }

    /// <summary>
    /// Sends an asynchronous GET request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="resolver">The resolver.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>
    /// A Task whose result is the XML response body parsed into a collection of XElements.
    /// </returns>
    public static Task<IEnumerable<XElement>> GetXElementsFromXPath(this IFlurlRequest request, string expression, IXmlNamespaceResolver resolver,
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
    {
        return request.SendAsync(HttpMethod.Get, null, completionOption, cancellationToken).ReceiveXElementsFromXPath(expression, resolver);
    }

    private static string GetMediaType(this IFlurlRequest request)
    {
        var acceptHeaders = request.Headers
            .Where(x => x.Name == "Accept")
            .ToList();

        if (!acceptHeaders.Any() || acceptHeaders.All(x => x.Value == null))
        {
            // no accepted media type present, return default
            return "application/xml";
        }

        // return media type of first accepted media type containing "xml", else of first accepted media type
        var mediaTypes = acceptHeaders
            .Where(x => x.Value != null)
            .SelectMany(x => x.Value.ToString().Split(','))
            .Select(x => x.Trim())
            .ToList();

        return mediaTypes.FirstOrDefault(x => x.IndexOf("xml", StringComparison.OrdinalIgnoreCase) >= 0)
            ?? mediaTypes.FirstOrDefault();
    }

    /// <summary>
    /// Sends an asynchronous HTTP request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="httpMethod">HTTP method of the request</param>
    /// <param name="data">Contents of the request body.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>
    /// A Task whose result is the received IFlurlResponse.
    /// </returns>
    public static Task<IFlurlResponse> SendXmlAsync(this IFlurlRequest request, HttpMethod httpMethod, object data, 
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
    {
        var content = new CapturedXmlContent(MicrosoftXmlSerializer.Default.Serialize(data), request.GetMediaType());
        return request.SendAsync(httpMethod, content, completionOption, cancellationToken);
    }

    /// <summary>
    /// Sends an asynchronous POST request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="data">Contents of the request body.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>
    /// A Task whose result is the received IFlurlResponse.
    /// </returns>
    public static Task<IFlurlResponse> PostXmlAsync(this IFlurlRequest request, object data, 
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
        SendXmlAsync(request, HttpMethod.Post, data, cancellationToken, completionOption);

    /// <summary>
    /// Sends an asynchronous PUT request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="data">Contents of the request body.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation. Optional.</param>
    /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
    /// <returns>
    /// A Task whose result is the received IFlurlResponse.
    /// </returns>
    public static Task<IFlurlResponse> PutXmlAsync(this IFlurlRequest request, object data, 
        CancellationToken cancellationToken = default, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
        SendXmlAsync(request, HttpMethod.Put, data, cancellationToken, completionOption);

    /// <summary>
    /// Sets the verb associated with this request
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="verb">The verb.</param>
    /// <returns>The request.</returns>
    public static Task<IFlurlRequest> WithVerb(this IFlurlRequest request, HttpMethod verb)
    {
        request.Verb = verb;
        return Task.FromResult(request);
    }
}