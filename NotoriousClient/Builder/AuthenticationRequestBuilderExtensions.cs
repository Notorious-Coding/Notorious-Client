using NotoriousClient.Framework.Web.Client.Builder.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotoriousClient.Framework.Web.Client.Builder
{
    /// <summary>
    /// Méthodes d'extensions sur la classe <see cref="IRequestBuilder"/>.
    /// </summary>
    public static class AuthenticationRequestBuilderExtensions
    {
        /// <summary>
        /// Permet de s'authentifier a partir d'un token Basic dans le header Authorization.
        /// </summary>
        /// <param name="builder">Builder.</param>
        /// <param name="username">Nom d'utilisateur.</param>
        /// <param name="password">Mot de passe.</param>
        public static IRequestBuilder WithAuthentication(this IRequestBuilder builder, string username, string password)
        {
            builder.WithAuthentication(new BasicAuthenticationInformation(username, password));
            return builder;
        }

        /// <summary>
        /// Permet de s'authentifier a partir d'un token Basic dans le header Authorization.
        /// </summary>
        /// <param name="builder">Builder.</param>
        /// <param name="jwt">Jeton d'authentification.</param>
        public static IRequestBuilder WithAuthentication(this IRequestBuilder builder, string jwt)
        {
            builder.WithAuthentication(new BearerAuthenticationInformation(jwt));
            return builder;
        }


    }
}
