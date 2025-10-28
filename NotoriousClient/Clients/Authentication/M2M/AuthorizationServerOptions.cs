namespace NotoriousClient.Clients.Authentication.M2M
{
    public class AuthorizationServerOptions
    {
        /// <summary>
        /// Gets or sets the unique identifier for the client.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret used for authentication.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the authority information for the current context.
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// Gets or sets the list of audiences that are allowed to access the resource.
        /// </summary>
        public string[] Audiences { get; set; }

        /// <summary>
        /// Gets or sets the scopes requested during the authorization process.
        /// </summary>
        public string[] Scopes { get; set; }

    }
}
