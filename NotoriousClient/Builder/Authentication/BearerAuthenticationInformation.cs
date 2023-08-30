namespace NotoriousClient.Builder.Authentication
{
    /// <summary>
    /// Bearer token authentication information.
    /// </summary>
    public class BearerAuthenticationInformation : IAuthenticationInformation
    {
        private string _token;
        
        ///<inheritdoc/>
        public string Token => _token;

        ///<inheritdoc/>
        public string Scheme => "Bearer";

        /// <summary>
        /// Initialize a new instance of <see cref="BearerAuthenticationInformation"/>
        /// </summary>
        /// <param name="token">Bearer token.</param>
        public BearerAuthenticationInformation(string token)
        {
            _token = token;
        }
    }
}
