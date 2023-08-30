using System.Text;

namespace NotoriousClient.Builder.Authentication
{
    /// <summary>
    /// Basic authentication information.
    /// </summary>
    public class BasicAuthenticationInformation : IAuthenticationInformation
    {
        /// <summary>
        /// User login.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; private set; }

        ///<inheritdoc/>
        public string Token { get; }

        ///<inheritdoc/>
        public string Scheme => "Basic";

        /// <summary>
        /// Initialize a new instance of <see cref="BasicAuthenticationInformation"/>.
        /// </summary>
        /// <param name="username">User's login.</param>
        /// <param name="password">User's password</param>
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
