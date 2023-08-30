using NotoriousClient.Builder;
using NotoriousClient.Sender;

namespace NotoriousClient.Clients
{
    /// <summary>
    /// Base class for HTTP Client preconfigured with Basic Authentication.
    /// </summary>
    public abstract class BasicAuthBaseClient : BaseClient
    {
        private readonly string _login;
        private readonly string _password;

        /// <summary>
        /// Initialize a new instance of <see cref="SynchronousBaseClient"/>.
        /// </summary>
        /// <param name="sender">Class used to send <see cref="HttpRequestMessage"/>.</param>
        /// <param name="url">Base URL of api (ex: https://myapi.com/).</param>
        /// <param name="login">User's login.</param>
        /// <param name="password">User's password.</param>
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
        /// Get preconfigured <see cref="IRequestBuilder"/> with Basic Authentication.
        /// </summary>
        /// <param name="route">Request's Route.</param>
        /// <param name="method">Request's method (GET, POST, PUT, DELETE...).</param>
        protected override IRequestBuilder GetBuilder(string route, Method method = Method.Get)
            => base.GetBuilder(route, method).WithAuthentication(_login, _password);

        /// <summary>
        /// Get preconfigured <see cref="IRequestBuilder"/> with Basic Authentication.
        /// </summary>
        /// <param name="route">Request's endpoint.</param>
        /// <param name="method">Request's method (GET, POST, PUT, DELETE...).</param>
        protected override IRequestBuilder GetBuilder(Endpoint endpoint)
            => base.GetBuilder(endpoint).WithAuthentication(_login, _password);
    }
}
