using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NotoriousClient.Converters
{
    /// <summary>
    /// Serialize object to JSON using System.Text.Json.
    /// </summary>
    public class SystemTextJsonSerializer : IJsonSerializer
    {
        internal const string ReflectionJsonMessage = "JSON serialization might require types that cannot be statically analyzed. Use the overload taking a JsonTypeInfo<T>, or a SystemTextJsonSerializer built with a JsonSerializerContext (System.Text.Json source generation), for trimmed or native AOT applications.";

        private readonly JsonSerializerContext? _context;

        /// <summary>
        /// Initialize a serializer based on reflection (not compatible with trimming / native AOT).
        /// </summary>
        [RequiresUnreferencedCode(ReflectionJsonMessage)]
        [RequiresDynamicCode(ReflectionJsonMessage)]
        public SystemTextJsonSerializer()
        {
        }

        /// <summary>
        /// Initialize a serializer based on source generated metadata (trimming and native AOT safe).
        /// </summary>
        /// <param name="context">Source generated context, which must contain the serialized types.</param>
        public SystemTextJsonSerializer(JsonSerializerContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            _context = context;
        }

        /// <inheritdoc/>
        public string ConvertToJson(object obj)
        {
            ArgumentNullException.ThrowIfNull(obj, nameof(obj));

            return _context != null
                ? JsonSerializer.Serialize(obj, obj.GetType(), _context)
                : SerializeWithReflection(obj);
        }

        [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "Only reachable when the serializer was built with the reflection constructor, which is annotated with RequiresUnreferencedCode.")]
        [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Only reachable when the serializer was built with the reflection constructor, which is annotated with RequiresDynamicCode.")]
        private static string SerializeWithReflection(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
