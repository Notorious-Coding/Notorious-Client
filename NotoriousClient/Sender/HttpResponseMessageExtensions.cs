using System.Net.Http.Formatting;

namespace NotoriousClient.Sender
{
    /// <summary>
    /// Extensions method for <see cref="HttpResponseMessage"/>.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        private static readonly IList<MediaTypeFormatter> MediaTypeFormatters = new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter()};

        /// <summary>
        /// Map response to <typeparamref name="T"/> asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of response.</typeparam>
        /// <param name="response">Response to map.</param>
        /// <returns>Mapped content.</returns>
        public static Task<T> ReadAsAsync<T>(this HttpResponseMessage response)
        {
            return response.Content.ReadAsAsync<T>(MediaTypeFormatters);
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
        /// <returns>Mapped content.</returns>
        public static T ReadAs<T>(this HttpResponseMessage response)
        {
            return ReadAsAsync<T>(response).GetAwaiter().GetResult();
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
