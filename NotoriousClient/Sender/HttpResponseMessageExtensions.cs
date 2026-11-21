using System.Net.Http.Json;
using System.Text.Json;

namespace NotoriousClient.Sender
{
    /// <summary>
    /// Extensions method for <see cref="HttpResponseMessage"/>.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Default options used to deserialize JSON responses (case-insensitive property names).
        /// </summary>
        private static readonly JsonSerializerOptions DefaultJsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        /// <summary>
        /// Map JSON response to <typeparamref name="T"/> asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of response.</typeparam>
        /// <param name="response">Response to map.</param>
        /// <param name="options">Serializer options. Defaults to <see cref="JsonSerializerDefaults.Web"/> (case-insensitive).</param>
        /// <returns>Mapped content.</returns>
        public static async Task<T> ReadAsAsync<T>(this HttpResponseMessage response, JsonSerializerOptions? options = null)
        {
            return (await response.Content.ReadFromJsonAsync<T>(options ?? DefaultJsonOptions))!;
        }

        /// <summary>
        /// Map response to a string asynchronously.
        /// </summary>
        /// <param name="response">Response to map.</param>
        /// <returns>Mapped content.</returns>
        public static Task<string> ReadAsStringAsync(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Map response to a byte array asynchronously.
        /// </summary>
        /// <param name="response">Response to map.</param>
        /// <returns>Mapped content as byte array.</returns>
        public static Task<byte[]> ReadAsByteArrayAsync(this HttpResponseMessage response)
        {
            return response.Content.ReadAsByteArrayAsync();
        }
        /// <summary>
        /// Map response to a byte array asynchronously.
        /// </summary>
        /// <param name="response">Response to map.</param>
        /// <returns>Mapped content as stream.</returns>
        public static Task<Stream> ReadAsStreamAsync(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Map response to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of response.</typeparam>
        /// <param name="response">Response to map.</param>
        /// <param name="options">Serializer options. Defaults to <see cref="JsonSerializerDefaults.Web"/> (case-insensitive).</param>
        /// <returns>Mapped content.</returns>
        public static T ReadAs<T>(this HttpResponseMessage response, JsonSerializerOptions? options = null)
        {
            return ReadAsAsync<T>(response, options).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Map response to a string.
        /// </summary>
        /// <param name="response">Response to map.</param>
        /// <returns>Mapped content.</returns>
        public static string ReadAsString(this HttpResponseMessage response)
        {
            return ReadAsStringAsync(response).Result;
        }

        /// <summary>
        /// Map response to a byte array.
        /// </summary>
        /// <param name="response">Response to map.</param>
        /// <returns>Mapped content as stream.</returns>
        public static byte[] ReadAsByteArray(this HttpResponseMessage response)
        {
            return ReadAsByteArrayAsync(response).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Map response to a byte array asynchronously.
        /// </summary>
        /// <param name="response">Response to map.</param>
        /// <returns>Mapped content as stream.</returns>
        public static Stream ReadAsStream(this HttpResponseMessage response)
        {
            return ReadAsStreamAsync(response).GetAwaiter().GetResult();
        }
    }
}
