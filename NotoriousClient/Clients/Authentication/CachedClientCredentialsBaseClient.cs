using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using NotoriousClient.Builder;
using NotoriousClient.Clients.Authentication.Models;
using NotoriousClient.Sender;

namespace NotoriousClient.Clients.Authentication
{
    public class MemoryCachedClientCredentialsBaseClient : ClientCredentialsBaseClient
    {
        private const int SKEW_IN_SECONDS = 60;
        private const int MinimumCacheExpiry = 1;
        private readonly IMemoryCache _cache;

        public MemoryCachedClientCredentialsBaseClient(IRequestSender sender, IMemoryCache cache, IOptions<AuthorizationServerOptions> server) : base(sender, server)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <summary>
        /// Get a preconfigured <see cref="IRequestBuilder"/> with Bearer Authentication using ClientCredentials OAuth flow.
        /// </summary>
        protected override async Task<IRequestBuilder> GetBuilderAsync(string route, Method method = Method.Get, string? version = null)
        {
            DiscoveryDocument? discovery = await GetDiscoveryDocument();
            TokenEndpointResponse response = await GetToken(discovery);

            return (await base.GetBuilderAsync(route, method, version)).WithAuthentication(response.AccessToken);
        }

        protected override async Task<TokenEndpointResponse> GetToken(DiscoveryDocument? discovery)

        {
            IOrderedEnumerable<string> scopes = AuthenticationServerOptions.Value.Scopes.OrderBy(s => s);
            IOrderedEnumerable<string> audiences = AuthenticationServerOptions.Value.Audiences.OrderBy(a => a);
            string authority = AuthenticationServerOptions.Value.Authority;
            string clientId = AuthenticationServerOptions.Value.ClientId;

            string cacheKey = $"{authority}::{clientId}::{string.Join("|", audiences)}::{string.Join("|", scopes)}";

            if (!_cache.TryGetValue<TokenEndpointResponse>(cacheKey, out var cachedToken))
            {
                TokenEndpointResponse response = await base.GetToken(discovery);

                int expirySeconds = Math.Max(MinimumCacheExpiry, response.ExpiresIn - SKEW_IN_SECONDS);
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirySeconds)
                };

                _cache.Set(cacheKey, response, cacheOptions);
                return response;
            }

            return cachedToken;
        }

        protected override async Task<DiscoveryDocument?> GetDiscoveryDocument()
        {
            string key = $"{AuthenticationServerOptions.Value.Authority}::discovery";
            if (!_cache.TryGetValue<DiscoveryDocument>(key, out var discovery))
            {
                discovery = await base.GetDiscoveryDocument();
                _cache.Set(key, discovery, TimeSpan.FromHours(24));
            }

            return discovery;
        }
    }
}
