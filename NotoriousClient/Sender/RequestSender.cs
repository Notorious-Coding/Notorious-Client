namespace NotoriousClient.Sender
{
    /// <summary>
    /// Permet l'envoi d'une requête HTTP.
    /// </summary>
    public class RequestSender : IRequestSender
    {
        private readonly HttpClient _client;

        private const int MAX_BODY_SIZE = 256;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RequestSender"/>.
        /// </summary>
        /// <param name="factory"></param>brefj
        public RequestSender(IHttpClientFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            _client = factory.CreateClient();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RequestSender"/>.
        /// </summary>
        /// <param name="httpClient"></param>brefj
        public RequestSender(HttpClient client)
        {
            ArgumentNullException.ThrowIfNull(client, nameof(client));
            _client = client;
        }

        /// <summary>
        /// Permet d'envoyer une requête de manière asynchrone.
        /// </summary>
        /// <param name="request">Requête à envoyer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Réponse.</returns>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            byte[] content = await request.Content.ReadAsByteArrayAsync();
            HttpResponseMessage response =  await _client.SendAsync(request, cancellationToken);

            return response;
        }

        /// <summary>
        /// Permet d'envoyer une requête de manière synchrone.
        /// </summary>
        /// <param name="request">Requête à envoyer.</param>
        /// <returns>Réponse.</returns>
        public HttpResponseMessage Send(HttpRequestMessage request)
        {
            return _client.Send(request);
        }
    }
}
