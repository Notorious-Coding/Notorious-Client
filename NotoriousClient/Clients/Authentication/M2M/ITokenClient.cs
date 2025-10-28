namespace NotoriousClient.Clients.Authentication.M2M
{
    public interface ITokenClient
    {
        Task<string> GetAccessToken();
    }
}