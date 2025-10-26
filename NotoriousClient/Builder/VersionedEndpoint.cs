namespace NotoriousClient.Builder
{
    public class VersionedEndpoint : Endpoint
    {
        public string Version { get; set; }
        public VersionedEndpoint(string route, Method method, string version) : base(route, method)
        {
            Version = version;
        }
    }
}
