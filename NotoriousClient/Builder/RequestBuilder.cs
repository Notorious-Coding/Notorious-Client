namespace NotoriousClient.Builder
{
    ///<inheritdoc/>
    public partial class RequestBuilder : IRequestBuilder
    {
        private Method _method;        
        
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RequestBuilder"/>.
        /// </summary>
        public RequestBuilder(string url, string route, Method method)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(route)) throw new ArgumentNullException();
            _url = url;
            _route = route;
            _method = method;
        }

        public RequestBuilder(string url, Endpoint endpoint)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException();
            ArgumentNullException.ThrowIfNull(endpoint, nameof(endpoint));

            _url = url;
            _route = endpoint.Route;
            _method = endpoint.Method;
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
