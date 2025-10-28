using NotoriousClient.Builder;
using NotoriousClient.Sender;

namespace NotoriousClient.Clients.Authentication.M2M
{
    public abstract class ClientCredentialsBaseClient : BaseClient
    {
        private readonly ITokenClient _tokenClient;

        public ClientCredentialsBaseClient(ITokenClient tokenClient, IRequestSender sender, string baseUrl) : base(sender, baseUrl)
        {
            _tokenClient = tokenClient;
        }

        protected override async Task<IRequestBuilder> GetBuilderAsync(string route, Method method = Method.Get, string? version = null)
        {
            string token = await _tokenClient.GetAccessToken();

            return (await base.GetBuilderAsync(route, method, version)).WithAuthentication(token);
        }
    }
}
