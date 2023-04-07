namespace NotoriousClient.Framework.Web.Client.Builder
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
            ArgumentException.ThrowIfNullOrEmpty(url, nameof(url));
            ArgumentException.ThrowIfNullOrEmpty(route, nameof(route));
            _url = url;
            _route = route;
            _method = method;
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
