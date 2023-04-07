namespace NotoriousClient.Builder
{
    internal class Body
    {
        public HttpContent Content { get; set; }
        public string Section { get; set; } = "";


        public Body(HttpContent data, string section = "")
        {
            Section = section;
            Content = data;
        }
    }
}
