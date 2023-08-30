using NotoriousClient.Builder.Authentication;
using NotoriousClient.Converters;

namespace NotoriousClient.Builder
{
    /// <summary>
    /// Class for building HttpRequestMessage fluently.
    /// </summary>
    public interface IRequestBuilder
    {
        /// <summary>
        /// Add JSON to request's body.
        /// </summary>
        /// <param name="data">Data to add.</param>
        /// <param name="converter">Custom json converter.</param>
        IRequestBuilder WithJsonBody(object data, IJsonSerializer? converter = null);

        /// <summary>
        /// Add a Stream to request's body.
        /// </summary>
        /// <param name="stream">Stream to add.</param>
        IRequestBuilder WithStreamBody(Stream stream);

        /// <summary>
        /// Add a http <paramref name="content"/> to the request's body.
        /// </summary>
        /// <param name="content">Custom http content.</param>
        IRequestBuilder WithContentBody(HttpContent content);

        /// <summary>
        /// Add a <paramref name="stream"/> to multiplart request's bodies under <paramref name="section"/> name.
        /// </summary>
        /// <param name="stream">Stream to add.</param>
        /// <param name="section">Name of multipart section.</param>
        IRequestBuilder WithStreamMultipartBody(Stream stream, string section);

        /// <summary>
        /// Add a <paramref name="data"/> to multiplart request's bodies under <paramref name="section"/> name.
        /// </summary>
        /// <param name="data">Data to add.</param>
        /// <param name="section">Name of multipart section.</param>
        /// <param name="converter">Custom json converter.</param>
        IRequestBuilder WithJsonMultipartBody(object data, string section, IJsonSerializer? converter = null);

        /// <summary>
        /// Add a http <paramref name="content"/> to multiplart request's bodies under <paramref name="section"/> name.
        /// </summary>
        /// <param name="content">Custom http content</param>
        /// <param name="section">Name of multipart section.</param>
        IRequestBuilder WithContentMultipartBody(HttpContent content, string section);

        /// <summary>
        /// Add a custom header to request.
        /// </summary>
        /// <param name="key">Header key.</param>
        /// <param name="value">Header value.</param>
        IRequestBuilder AddCustomHeader(string key, string value);

        /// <summary>
        /// Add several custom headers to request.
        /// </summary>
        /// <param name="headers">Header list.</param>
        IRequestBuilder AddCustomHeaders(Dictionary<string, string> headers);

        /// <summary>
        /// Add several custom headers to request.
        /// </summary>
        /// <param name="headers">Listes des headers à ajouter.</param>
        IRequestBuilder AddCustomHeaders(IDictionary<string, string> headers);

        /// <summary>
        /// Add "Accept" header to precise authorized media type in request.
        /// </summary>
        /// <param name="type">Authorized media type.</param>
        IRequestBuilder WithCustomAcceptMediaType(string type);

        /// <summary>
        /// Add authentication to request.
        /// </summary>
        /// <param name="auth">Authentication information.</param>
        IRequestBuilder WithAuthentication(IAuthenticationInformation auth);

        /// <summary>
        /// Add a GET params to request's url.
        /// </summary>
        /// <param name="key">Params key.</param>
        /// <param name="value">Params value.</param>
        IRequestBuilder AddQueryParameter(string key, string value);

        /// <summary>
        /// Add several GET params to request's url.
        /// </summary>
        /// <param name="queryParams">Params list.</param>
        IRequestBuilder AddQueryParameters(Dictionary<string, string> queryParams);

        /// <summary>
        /// Permet d'ajouter plusieurs paramètres GET dans l'url.
        /// </summary>
        /// <param name="queryParams">Params list.</param>
        IRequestBuilder AddQueryParameters(IDictionary<string, string> queryParams);

        /// <summary>
        /// Interpolate <paramref name="key"/> surrounded by { } inside request url with <paramref name="value"/>.
        /// </summary>
        /// <param name="key">Key to interploate.</param>
        /// <param name="value">Value.</param>
        IRequestBuilder AddEndpointParameter(string key, string value);

        /// <summary>
        /// Interpolate several <paramref name="endpointParams"/> surrounded by { } inside request url with their value.
        /// </summary>
        /// <param name="endpointParams">Params list.</param>
        IRequestBuilder AddEndpointParameters(IDictionary<string, string> endpointParams);

        /// <summary>
        /// Interpolate several <paramref name="endpointParams"/> surrounded by { } inside request url with their value.
        /// </summary>
        /// <param name="endpointParams">Params list.</param>
        /// <returns></returns>
        IRequestBuilder AddEndpointParameters(Dictionary<string, string> endpointParams);

        /// <summary>
        /// Get configured <see cref="HttpRequestMessage"/>.
        /// </summary>
        /// <returns>Configured request.</returns>
        HttpRequestMessage Build();
    }
}
