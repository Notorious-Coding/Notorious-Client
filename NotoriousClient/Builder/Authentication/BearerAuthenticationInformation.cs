namespace NotoriousClient.Builder.Authentication
{
    /// <summary>
    /// Information d'authentification Bearer Token (Bearer).
    /// </summary>
    public class BearerAuthenticationInformation : IAuthenticationInformation
    {
        private string _token;
        
        ///<inheritdoc/>
        public string Token => _token;

        ///<inheritdoc/>
        public string Scheme => "Bearer";

        /// <summary>
        /// Permet de créer une instance de la classe <see cref="BearerAuthenticationInformation"/>
        /// </summary>
        /// <param name="token"></param>
        public BearerAuthenticationInformation(string token)
        {
            _token = token;
        }
    }
}
