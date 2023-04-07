using NotoriousClient.Builder;
using NotoriousClient.Sender;

namespace NotoriousClient.RestClients
{   
    /// <summary>
    /// Classe de base pour les clients utilisant l'authentification en Basic Auth.
    /// </summary>
    public abstract class BasicAuthBaseClient : BaseClient
    {
        private readonly string _login;
        private readonly string _password;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="BasicAuthBaseClient"/>.
        /// </summary>
        /// <param name="sender">Classe permettant d'envoyer les requête https.</param>
        /// <param name="url">Url de base de l'API.</param>
        /// <param name="login">Nom de l'utilisateur.</param>
        /// <param name="password">Mot de passe de l'utilisateur.</param>
        protected BasicAuthBaseClient(IRequestSender sender, string url, string login, string password) : base(sender, url)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(login)) throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            ArgumentNullException.ThrowIfNull(sender, nameof(sender));

            _login = login;
            _password = password;
        }

        /// <summary>
        /// Permet de récuperer une instance préconfiguré de <see cref="IRequestBuilder"/>.
        /// </summary>
        /// <param name="endpoint">Endpoint de l'appel en cours de construction.</param>
        /// <param name="method">Méthode de l'appel en cours de construction (GET, POST, PUT, DELETE...).</param>
        protected override IRequestBuilder GetBuilder(string endpoint, Method method = Method.Get) 
            => base.GetBuilder(endpoint, method).WithAuthentication(_login, _password);
    }
}
