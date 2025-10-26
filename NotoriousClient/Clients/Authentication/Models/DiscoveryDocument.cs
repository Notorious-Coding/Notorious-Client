using System.Text.Json.Serialization;

namespace NotoriousClient.Clients.Authentication.Models
{
    public class DiscoveryDocument
    {
        [JsonPropertyName("token_endpoint")]
        public string TokenEndpoint { get; set; }
    }
}
