namespace NotoriousClient.Builder
{
    /// <summary>
    /// HTTP Endpoint.
    /// </summary>
    public class Endpoint
    {
        /// <summary>
        /// Endpoint's route.
        /// </summary>
        public string Route { get; }

        /// <summary>
        /// Endpoint's method.
        /// </summary>
        public Method Method { get; }


        /// <summary>
        /// Initialize a new <see cref="Endpoint"/>.
        /// </summary>
        /// <param name="route">Endpoint's route.</param>
        /// <param name="method">Endpoint's method.</param>
        public Endpoint(string route, Method method)
        {
            Route = route;
            Method = method;
        }
    }
}
