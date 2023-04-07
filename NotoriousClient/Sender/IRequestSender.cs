using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NotoriousClient.Sender
{
    /// <summary>
    /// Permet l'envoi d'une requête HTTP.
    /// </summary>
    public interface IRequestSender
    {
        /// <summary>
        /// Permet d'envoyer une requête de manière synchrone.
        /// </summary>
        /// <param name="request">Requête à envoyer.</param>
        /// <returns>Réponse.</returns>
        HttpResponseMessage Send(HttpRequestMessage request);

        /// <summary>
        /// Permet d'envoyer une requête de manière asynchrone.
        /// </summary>
        /// <param name="request">Requête à envoyer.</param>
        /// <param name="cancellationToken">Token d'annulation.</param>
        /// <returns>Réponse.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default);
    }
}