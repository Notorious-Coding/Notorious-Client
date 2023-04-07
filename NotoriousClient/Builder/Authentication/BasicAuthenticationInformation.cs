using System;
using System.Collections.Generic;
using System.Text;

namespace NotoriousClient.Framework.Web.Client.Builder.Authentication
{
    /// <summary>
    /// Information d'authentification Basic (Username et password).
    /// </summary>
    public class BasicAuthenticationInformation : IAuthenticationInformation
    {
        /// <summary>
        /// Nom de l'utilisateur.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Mot de passe.
        /// </summary>
        public string Password { get; private set; }

        ///<inheritdoc/>
        public string Token { get; }

        ///<inheritdoc/>
        public string Scheme => "Basic";

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RequestBuilder"/>.
        /// </summary>
        /// <param name="username">Nom d'utilisateur.</param>
        /// <param name="password">Mot de passe</param>
        public BasicAuthenticationInformation(string username, string password)
        {
            UserName = username;
            Password = password;
            Token = Base64Encode($"{username}:{password}");
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
