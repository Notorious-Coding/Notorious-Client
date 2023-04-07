namespace NotoriousClient.Builder
{
    /// <summary>
    /// Objets représentant un endpoint HTTP.
    /// </summary>
    public class Endpoint
    {
        /// <summary>
        /// Route de l'endpoint.
        /// </summary>
        public string Route { get; }

        /// <summary>
        /// Method de l'endpoint.
        /// </summary>
        public Method Method { get; }


        /// <summary>
        /// Créer un endpoint.
        /// </summary>
        /// <param name="route">Route de l'endpoint.</param>
        /// <param name="method">Method de l'endpoint.</param>
        public Endpoint(string route, Method method)
        {
            Route = route;
            Method = method;
        }
    }
}
