namespace NotoriousClient.Builder
{
    /// <summary>
    /// Body is not compatible with current configured request.
    /// </summary>
    public class BodyNotCompatibleException : Exception
    {
        public BodyNotCompatibleException() : base("Body is not compatible with current configured request, it often occurs when you try adding classic body to multipart request.")
        {
            
        }
    }
}
