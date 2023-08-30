namespace NotoriousClient.Builder.Authentication
{
    /// <summary>
    /// Authentication data.
    /// </summary>
    public interface IAuthenticationInformation
    {
        /// <summary>
        /// Authentication token.
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Authentication schema.
        /// </summary>
        string Scheme { get; }
    }
}
