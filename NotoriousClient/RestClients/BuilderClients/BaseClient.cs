using NotoriousClient.Framework.Web.Client.Builder;
using NotoriousClient.Framework.Web.Client.Sender;

namespace NotoriousClient.Framework.Web.Client.RestClients
{
    /// <summary>
    /// Classe de base de client.
    /// </summary>
    public abstract class BaseClient
    {
        private readonly string _url;

        /// <summary>
        /// Outils d'envoi des requêtes HTTPs.
        /// </summary>
        protected IRequestSender Sender { get; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="BaseClient"/>.
        /// </summary>
        /// <param name="sender">Classe permettant d'envoyer les requête https.</param>
        /// <param name="url">Url de base de l'API.</param>
        protected BaseClient(IRequestSender sender, string url)
        {
            Sender = sender;
            _url = url;
        }

        /// <summary>
        /// Permet de récuperer une instance préconfiguré de <see cref="IRequestBuilder"/>.
        /// </summary>
        /// <param name="route">Route de l'appel en cours de construction.</param>
        /// <param name="method">Méthode de l'appel en cours de construction (GET, POST, PUT, DELETE...).</param>
        protected virtual IRequestBuilder GetBuilder(string route, Method method = Method.Get)
            => new RequestBuilder(_url, route, method);

        /// <summary>
        /// Permet de récuperer une instance préconfiguré de <see cref="IRequestBuilder"/>.
        /// </summary>
        /// <param name="endpoint">Endpoint de l'appel en cours de construction.</param>
        protected IRequestBuilder GetBuilder(Endpoint endpoint)
            => GetBuilder(endpoint.Route, endpoint.Method);
    }
}
