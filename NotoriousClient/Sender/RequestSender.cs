using Newtonsoft.Json;
using System.Net;

namespace NotoriousClient.Framework.Web.Client.Sender
{
    /// <summary>
    /// Permet l'envoi d'une requête HTTP.
    /// </summary>
    public class RequestSender : IRequestSender
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RequestSender"/>.
        /// </summary>
        /// <param name="factory"></param>
        public RequestSender(IHttpClientFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            _client = factory.CreateClient();
        }

        /// <summary>
        /// Permet d'envoyer une requête de manière asynchrone.
        /// </summary>
        /// <param name="request">Requête à envoyer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Réponse.</returns>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            return await _client.SendAsync(request, cancellationToken);
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
