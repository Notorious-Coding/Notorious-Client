using NotoriousClient.Builder.Authentication;
using System;
using System.Net.Http.Headers;

namespace NotoriousClient.Builder
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
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException();

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
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException();
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
