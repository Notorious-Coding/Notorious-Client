using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NotoriousClient.Sender
{
    /// <summary>
    /// Class used to send <see cref="HttpRequestMessage"/>.
    /// </summary>
    public interface IRequestSender
    {
        /// <summary>
        /// Send a <paramref name="request"/> asynchronously.
        /// </summary>
        /// <param name="request">Request to send.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Request response.</returns>
        HttpResponseMessage Send(HttpRequestMessage request);

        /// <summary>
        /// Send a <paramref name="request"/> synchronously.
        /// </summary>
        /// <param name="request">Request to send.</param>
        /// <returns>Request response.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default);
    }
}