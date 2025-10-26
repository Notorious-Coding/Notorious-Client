namespace NotoriousClient.Builder
{
    ///<inheritdoc/>
    public partial class RequestBuilder : IRequestBuilder
    {
        private Method _method;

        /// <summary>
        /// Initialize a new instance of <see cref="RequestBuilder"/>.
        /// </summary>
        public RequestBuilder(string url, string route, Method method)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(route)) throw new ArgumentNullException();
            _url = url;
            _route = route;
            _method = method;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="RequestBuilder"/>.
        /// </summary>
        public RequestBuilder(string url, Endpoint endpoint) : this(url, endpoint.Route, endpoint.Method)
        {
        }

        /// <summary>
        /// Initialize a new instance of <see cref="RequestBuilder"/>.
        /// </summary>
        public RequestBuilder(string url, string route, string? version, Method method)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(route)) throw new ArgumentNullException();
            _url = url;
            _route = route;
            _method = method;
            _version = version;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="RequestBuilder"/>.
        /// </summary>
        public RequestBuilder(string url, VersionedEndpoint endpoint) : this(url, endpoint.Route, endpoint.Version, endpoint.Method)
        {
        }

        ///<inheritdoc/>
        public virtual HttpRequestMessage Build()
        {
            var request = new HttpRequestMessage(new HttpMethod(_method.ToString()), BuildUri());
            BuildHeader(request);
            BuildBody(request);

            return request;
        }
    }
}
