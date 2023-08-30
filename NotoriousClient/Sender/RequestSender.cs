namespace NotoriousClient.Sender
{
    /// <summary>
    /// Class used to send <see cref="HttpRequestMessage"/>.
    /// </summary>
    public class RequestSender : IRequestSender
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initialize a new instance of <see cref="RequestSender"/>.
        /// </summary>
        /// <param name="factory">HttpClient's factory</param>
        public RequestSender(IHttpClientFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            _client = factory.CreateClient();
        }

        /// <summary>
        /// Initialize a new instance of <see cref="RequestSender"/>.
        /// </summary>
        /// <param name="client">Http client.</param>
        public RequestSender(HttpClient client)
        {
            ArgumentNullException.ThrowIfNull(client, nameof(client));
            _client = client;
        }

        /// <inheritdoc/> 
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            byte[] content = await request.Content.ReadAsByteArrayAsync();
            HttpResponseMessage response =  await _client.SendAsync(request, cancellationToken);

            return response;
        }

        /// <inheritdoc/> 
        public HttpResponseMessage Send(HttpRequestMessage request)
        {
            return _client.Send(request);
        }
    }
}
