using NotoriousClient.Framework.Web.Client.Builder.Authentication;
using System.Net.Http.Headers;

namespace NotoriousClient.Framework.Web.Client.Builder
{
    public partial class RequestBuilder : IRequestBuilder
    {
        private readonly string ACCEPT_HEADER_NAME = "Accept";
        private Dictionary<string, string> _headers = new Dictionary<string, string>();
        private IAuthenticationInformation _authorizationInformation = null;

        ///<inheritdoc/>
        public IRequestBuilder WithAuthentication(IAuthenticationInformation authenthicationInformation)
        {
            _authorizationInformation = authenthicationInformation;
            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder AddCustomHeader(string key, string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));
            //On gère directement via l'index pour laisser la possibilité d'ovveride un header existant dans le builder
            //Par exemple dans le cas ou on hérite d'un BaseClient qui set un header qu'on voudrais remplacer pour une fois.
            _headers[key] = value;

            return this;
        }


        ///<inheritdoc/>
        public IRequestBuilder AddCustomHeaders(Dictionary<string, string> headers)
        {

            AddCustomHeaders(headers as IDictionary<string, string>);

            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder AddCustomHeaders(IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                AddCustomHeader(header.Key, header.Value);
            }

            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder WithCustomAcceptMediaType(string type)
        {
            ArgumentException.ThrowIfNullOrEmpty(type, nameof(type));
            AddCustomHeader(ACCEPT_HEADER_NAME, type);
            return this;
        }

        #region Private Methods
        private void BuildHeader(HttpRequestMessage request)
        {
            foreach (var header in _headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            if (_authorizationInformation != null)
                request.Headers.Authorization = new AuthenticationHeaderValue(_authorizationInformation.Scheme, _authorizationInformation.Token);
        }
        #endregion
    }
}
