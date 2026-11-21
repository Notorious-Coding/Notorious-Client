using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace NotoriousClient.Builder
{
    /// <summary>
    /// JSON body extensions on <see cref="IRequestBuilder"/>, based on System.Text.Json source generation (trimming and native AOT safe).
    /// </summary>
    public static class JsonRequestBuilderExtensions
    {
        /// <summary>
        /// Add JSON to request's body, using source generated metadata.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="builder">Current instance of <see cref="IRequestBuilder"/>.</param>
        /// <param name="data">Data to add.</param>
        /// <param name="jsonTypeInfo">Source generated metadata for <typeparamref name="T"/>.</param>
        public static IRequestBuilder WithJsonBody<T>(this IRequestBuilder builder, T data, JsonTypeInfo<T> jsonTypeInfo)
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            ArgumentNullException.ThrowIfNull(jsonTypeInfo, nameof(jsonTypeInfo));

            return builder.WithContentBody(new StringContent(JsonSerializer.Serialize(data, jsonTypeInfo), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Add <paramref name="data"/> as JSON to multipart request's bodies under <paramref name="section"/> name, using source generated metadata.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="builder">Current instance of <see cref="IRequestBuilder"/>.</param>
        /// <param name="data">Data to add.</param>
        /// <param name="section">Name of multipart section.</param>
        /// <param name="jsonTypeInfo">Source generated metadata for <typeparamref name="T"/>.</param>
        public static IRequestBuilder WithJsonMultipartBody<T>(this IRequestBuilder builder, T data, string section, JsonTypeInfo<T> jsonTypeInfo)
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            ArgumentNullException.ThrowIfNull(jsonTypeInfo, nameof(jsonTypeInfo));

            return builder.WithContentMultipartBody(new StringContent(JsonSerializer.Serialize(data, jsonTypeInfo)), section);
        }
    }
}
