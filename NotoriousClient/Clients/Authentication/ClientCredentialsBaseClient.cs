using Microsoft.Extensions.Options;

using NotoriousClient.Builder;
using NotoriousClient.Clients.Authentication.Models;
using NotoriousClient.Sender;

namespace NotoriousClient.Clients.Authentication
{
    public class ClientCredentialsBaseClient : BaseClient
    {
        protected IOptions<AuthorizationServerOptions> AuthenticationServerOptions { get; private init; }
        private readonly Endpoint DISCOVERY_ENDPOINT = new Endpoint("/.well-known/openid-configuration", Method.Get);

        public ClientCredentialsBaseClient(IRequestSender sender, IOptions<AuthorizationServerOptions> server) : base(sender, server.Value.Authority)
        {
            AuthenticationServerOptions = server ?? throw new ArgumentNullException(nameof(server));
        }

        protected override async Task<IRequestBuilder> GetBuilderAsync(string route, Method method = Method.Get, string? version = null)
        {
            string tokenEndpoint = string.Empty;
            DiscoveryDocument? discovery = await GetDiscoveryDocument();
            TokenEndpointResponse response = await GetToken(discovery);

            return (await base.GetBuilderAsync(route, method, version)).WithAuthentication(response.AccessToken);
        }

        protected virtual async Task<TokenEndpointResponse> GetToken(DiscoveryDocument? discovery)
        {
            HttpRequestMessage request = new RequestBuilder(discovery.TokenEndpoint, "", Method.Post)
            .WithContentBody(new FormUrlEncodedContent(new Dictionary<string, string>
            {
                    { "grant_type", "client_credentials" },
                    { "client_id", AuthenticationServerOptions.Value.ClientId },
                    { "client_secret", AuthenticationServerOptions.Value.ClientSecret },
                    { "audience", string.Join(" ", AuthenticationServerOptions.Value.Audiences) },
                    { "scope", string.Join(" ", AuthenticationServerOptions.Value.Scopes) }
            })).Build();

            return (await Sender.SendAsync(request)).ReadAs<TokenEndpointResponse>();
        }

        protected virtual async Task<DiscoveryDocument?> GetDiscoveryDocument()
        {
            HttpResponseMessage discoResponse = await Sender.SendAsync(GetBuilder(DISCOVERY_ENDPOINT).Build());
            discoResponse.EnsureSuccessStatusCode();

            return await discoResponse.ReadAsAsync<DiscoveryDocument>();
        }
    }
}
