using System.Text.Json.Serialization;

namespace NotoriousClient.Clients.Authentication.M2M.Models
{
    /// <summary>
    /// Source generated JSON metadata for authorization server responses (trimming and AOT safe).
    /// </summary>
    [JsonSerializable(typeof(DiscoveryDocument))]
    [JsonSerializable(typeof(TokenEndpointResponse))]
    internal partial class AuthenticationJsonContext : JsonSerializerContext
    {
    }
}
