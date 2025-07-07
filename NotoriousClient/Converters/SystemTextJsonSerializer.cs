using System.Text.Json;

namespace NotoriousClient.Converters
{
    /// <summary>
    /// Serialize object to JSON using NewtonsoftJson.
    /// </summary>
    public class SystemTextJsonSerializer : IJsonSerializer
    {
        public string ConvertToJson(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
