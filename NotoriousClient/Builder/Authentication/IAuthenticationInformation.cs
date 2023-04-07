namespace NotoriousClient.Builder.Authentication
{
    /// <summary>
    /// Information d'authentification par token.
    /// </summary>
    public interface IAuthenticationInformation
    {
        /// <summary>
        /// Valeur du token de l'authentification.
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Valeur du schéma de l'authentification.
        /// </summary>
        string Scheme { get; }
    }
}
