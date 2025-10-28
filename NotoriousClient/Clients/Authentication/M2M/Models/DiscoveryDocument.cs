using System.Text.Json.Serialization;

namespace NotoriousClient.Clients.Authentication.M2M.Models
{
    public class DiscoveryDocument
    {
        [JsonPropertyName("token_endpoint")]
        public string TokenEndpoint { get; set; }
    }
}
