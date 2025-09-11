namespace NotoriousClient.Sender
{
    /// <summary>
    /// Class used to send <see cref="HttpRequestMessage"/>.
    /// </summary>
    public class RequestSender : IRequestSender
    {
        private readonly IHttpClientFactory _factory;

        /// <summary>
        /// Initialize a new instance of <see cref="RequestSender"/>.
        /// </summary>
        /// <param name="factory">HttpClient's factory</param>
        /// <param name="name">HttpClient's name</param>
        public RequestSender(IHttpClientFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            _factory = factory;
        }

        /// <inheritdoc/> 
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage response = await _factory.CreateClient().SendAsync(request, cancellationToken);

            return response;
        }

        /// <inheritdoc/> 
        public HttpResponseMessage Send(HttpRequestMessage request)
        {
            return _factory.CreateClient().Send(request);
        }
    }
}
