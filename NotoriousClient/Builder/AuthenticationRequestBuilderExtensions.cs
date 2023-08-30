using NotoriousClient.Builder.Authentication;

namespace NotoriousClient.Builder
{
    /// <summary>
    /// Méthodes d'extensions sur la classe <see cref="IRequestBuilder"/>.
    /// </summary>
    public static class AuthenticationRequestBuilderExtensions
    {
        /// <summary>
        /// Add basic authentication to request.
        /// </summary>
        /// <param name="builder">Current instance of <see cref="IRequestBuilder"/>.</param>
        /// <param name="username">User's login.</param>
        /// <param name="password">User's password.</param>
        public static IRequestBuilder WithAuthentication(this IRequestBuilder builder, string username, string password)
        {
            builder.WithAuthentication(new BasicAuthenticationInformation(username, password));
            return builder;
        }

        /// <summary>
        /// Add bearer authentication to request.
        /// </summary>
        /// <param name="builder">Builder.</param>
        /// <param name="jwt">JWT token.</param>
        public static IRequestBuilder WithAuthentication(this IRequestBuilder builder, string jwt)
        {
            builder.WithAuthentication(new BearerAuthenticationInformation(jwt));
            return builder;
        }
    }
}
