﻿using NotoriousClient.Builder;
using NotoriousClient.Sender;

namespace NotoriousClient.Clients
{
    /// <summary>
    /// Base class for HTTP Client.
    /// </summary>
    public abstract class BaseClient
    {
        private readonly string _url;

        /// <summary>
        /// Tool for sending HttpRequestMessage.
        /// </summary>
        protected IRequestSender Sender { get; }

        /// <summary>
        /// Initialize a new instance of <see cref="SynchronousBaseClient"/>.
        /// </summary>
        /// <param name="sender">Class used to send <see cref="HttpRequestMessage"/>.</param>
        /// <param name="url">Base URL of api (ex: https://myapi.com/).</param>
        protected BaseClient(IRequestSender sender, string url)
        {
            ArgumentNullException.ThrowIfNull(sender, nameof(sender));
            if(string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            Sender = sender;
            _url = url;
        }

        /// <summary>
        /// Get preconfigured <see cref="IRequestBuilder"/>.
        /// </summary>
        /// <param name="route">Request's Route.</param>
        /// <param name="method">Request's Method (GET, POST, PUT, DELETE...).</param>
        protected virtual IRequestBuilder GetBuilder(string route, Method method = Method.Get)
            => new RequestBuilder(_url, route, method);

        /// <summary>
        /// Get preconfigured <see cref="IRequestBuilder"/>.
        /// </summary>
        /// <param name="endpoint">Request's <see cref="Endpoint"/>.</param>
        protected IRequestBuilder GetBuilder(Endpoint endpoint)
            => GetBuilder(endpoint.Route, endpoint.Method);

        /// <summary>
        /// Get preconfigured <see cref="IRequestBuilder"/>.
        /// </summary>
        /// <param name="route">Request's Route.</param>
        /// <param name="method">Request's Method (GET, POST, PUT, DELETE...).</param>
        protected virtual async Task<IRequestBuilder> GetBuilderAsync(string route, Method method = Method.Get)
            => new RequestBuilder(_url, route, method);

        /// <summary>
        /// Get preconfigured <see cref="IRequestBuilder"/>.
        /// </summary>
        /// <param name="endpoint">Request's <see cref="Endpoint"/>.</param>
        protected Task<IRequestBuilder> GetBuilderAsync(Endpoint endpoint)
            => GetBuilderAsync(endpoint.Route, endpoint.Method);
    }
}
