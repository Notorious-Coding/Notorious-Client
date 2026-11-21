using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using NotoriousClient.Clients.Authentication.M2M;
using NotoriousClient.Sender;
using NotoriousClient.Tests.Unit.Attributes;

namespace NotoriousClient.Tests.Unit
{
    public class TokenClientUnitTests
    {
        private class FakeSender : IRequestSender
        {
            public List<HttpRequestMessage> Requests { get; } = new List<HttpRequestMessage>();
            public HttpStatusCode TokenStatusCode { get; set; } = HttpStatusCode.OK;

            public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
            {
                Requests.Add(request);

                bool isDiscovery = request.RequestUri!.AbsolutePath.EndsWith("/.well-known/openid-configuration");
                string json = isDiscovery
                    ? @"{""token_endpoint"":""https://auth.test/protocol/token""}"
                    : @"{""access_token"":""abc123"",""expires_in"":300,""token_type"":""Bearer""}";

                return Task.FromResult(new HttpResponseMessage(isDiscovery ? HttpStatusCode.OK : TokenStatusCode)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            }

            public HttpResponseMessage Send(HttpRequestMessage request) => SendAsync(request).GetAwaiter().GetResult();
        }

        private static IOptions<AuthorizationServerOptions> Options() => Microsoft.Extensions.Options.Options.Create(new AuthorizationServerOptions
        {
            Authority = "https://auth.test",
            ClientId = "client",
            ClientSecret = "secret",
            Audiences = new[] { "api" },
            Scopes = new[] { "read", "write" }
        });

        [GWTFact(
            given: "An authorization server exposing a token endpoint",
            when: "i get an access token",
            then: "the client credentials are posted to the discovered token endpoint")]
        public async Task GetAccessToken_Should_PostClientCredentialsToTokenEndpoint()
        {
            FakeSender sender = new FakeSender();
            TokenClient client = new TokenClient(sender, Options());

            string token = await client.GetAccessToken();

            Assert.Equal("abc123", token);
            Assert.Equal(2, sender.Requests.Count);

            HttpRequestMessage tokenRequest = sender.Requests[1];
            Assert.Equal(HttpMethod.Post, tokenRequest.Method);
            Assert.Equal("https://auth.test/protocol/token", tokenRequest.RequestUri!.AbsoluteUri);

            string body = await tokenRequest.Content!.ReadAsStringAsync();
            Assert.Contains("grant_type=client_credentials", body);
            Assert.Contains("client_id=client", body);
            Assert.Contains("client_secret=secret", body);
            Assert.Contains("scope=read+write", body);
        }

        [GWTFact(
            given: "An authorization server rejecting the credentials",
            when: "i get an access token",
            then: "an HttpRequestException is thrown")]
        public async Task GetAccessToken_Should_ThrowWhenTokenRequestFails()
        {
            FakeSender sender = new FakeSender { TokenStatusCode = HttpStatusCode.Unauthorized };
            TokenClient client = new TokenClient(sender, Options());

            await Assert.ThrowsAsync<HttpRequestException>(() => client.GetAccessToken());
        }
    }
}
