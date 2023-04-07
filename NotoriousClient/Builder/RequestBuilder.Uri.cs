using System.Collections.Specialized;
using System.Web;

namespace NotoriousClient.Framework.Web.Client.Builder
{
    public partial class RequestBuilder : IRequestBuilder
    {
        private string _url;
        private string _route;
        private Dictionary<string, string> _queryParams = new Dictionary<string, string>();
        private Dictionary<string, string> _endpointParams = new Dictionary<string, string>();
        private const char START_ENDPOINT_PARAM = '{';
        private const char END_ENDPOINT_PARAM = '}';
        ///<inheritdoc/>
        public IRequestBuilder AddQueryParameter(string key, string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            //On gère directement via l'index pour laisser la possibilité d'ovveride un param existant dans le builder
            //Par exemple dans le cas ou on hérite d'un BaseClient qui set un paramètre qu'on voudrais remplacer pour une fois.
            _queryParams[key] = value;
            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder AddQueryParameters(Dictionary<string, string> queryParams)
        {
            AddQueryParameters(queryParams as IDictionary<string, string>);
            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder AddQueryParameters(IDictionary<string, string> queryParams)
        {
            foreach (var queryParam in queryParams)
            {
                AddQueryParameter(queryParam.Key, queryParam.Value);
            }
            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder AddEndpointParameter(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (_endpointParams.ContainsKey(key))
                throw new ArgumentException($"La clé {key} a déja été précisé.");
            _endpointParams.Add(key, value);
            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder AddEndpointParameters(Dictionary<string, string> endpointParams)
        {
            AddEndpointParameters(endpointParams as IDictionary<string, string>);
            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder AddEndpointParameters(IDictionary<string, string> endpointParams)
        {
            foreach (var param in endpointParams)
            {
                AddEndpointParameter(param.Key, param.Value);
            }
            return this;
        }

        #region Private Methods
        private string BuildUri()
        {
            string uri = GetUri();
            uri = HandleUriQueryParams(uri, _queryParams);
            uri = HandleUriEndPointParams(uri, _endpointParams);
            return uri;
        }

        private string HandleUriQueryParams(string uri, Dictionary<string, string> queryParams)
        {
            string queryParamsString = string.Join("&", queryParams.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
            return string.Format(uri + "?{0}", HttpUtility.UrlEncode(queryParamsString));
        }

        private string HandleUriEndPointParams(string uri, Dictionary<string, string> endPointParams)
        {
            foreach(var param in endPointParams)
            {
                var token = GetReplacementToken(param.Key);
                if (uri.Contains(token))
                {
                    uri = uri.Replace(token, param.Value);
                }
                else
                {
                    throw new KeyNotFoundException($"La clé {param.Key} n'a pas été trouvée, pensez à bien entourer la clé par {{ }} au sein de l'endpoint");
                }
            }

            return uri;
        }

        private string GetReplacementToken(string key)
        {
            return START_ENDPOINT_PARAM + key + END_ENDPOINT_PARAM;
        }

        private string GetUri()
        {
            var url = !_url.EndsWith("/") ? _url : _url.Substring(0, _url.Length - 1);
            var endpoint = _route.StartsWith("/") ? _route : $"/{_route}";

            return url + endpoint;
        }
        #endregion
    }
}
