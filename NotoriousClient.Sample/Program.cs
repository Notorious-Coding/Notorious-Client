// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using NotoriousClient.Builder;
using NotoriousClient.Builder.Authentication;
using NotoriousClient.Clients;
using NotoriousClient.Converters;
using NotoriousClient.Sender;

Console.WriteLine("Hello, World!");


IRequestBuilder requestBuilder = new RequestBuilder("https://toto.com", "/users", Method.Get);

HttpRequestMessage request = requestBuilder
    .AddCustomHeader("X-API-TOKEN", "fjfdzsfzfazgaegaz")
    .WithAuthentication("username", "password")
    .Build();


new RequestSender(null as IHttpClientFactory);


var services = new ServiceCollection();

services.AddHttpClient();
services.AddScoped<IRequestSender, RequestSender>();
services.AddScoped((serviceProvider) => new UserClient(serviceProvider.GetRequiredService<IRequestSender>(), "http://my.api.com/"));



public class User
{

}
public class UserClient : BaseClient
{
    private Endpoint GET_USERS_ENDPOINT = new Endpoint("/api/users", Method.Get); 
    private Endpoint GET_USER_ENDPOINT = new Endpoint("/api/users/{id}", Method.Get);
    private Endpoint CREATE_USER_ENDPOINT = new Endpoint("/api/users", Method.Post);
    public UserClient(IRequestSender sender, string url) : base(sender, url)
    {
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        HttpRequestMessage request = GetBuilder(GET_USERS_ENDPOINT)
            .WithAuthentication("username", "password")
            .AddQueryParameter("limit", "100")
            .Build();

        HttpResponseMessage response = await Sender.SendAsync(request);

        return response.ReadAs<IEnumerable<User>>();
    }

    public async Task<IEnumerable<User>> GetUser(int id)
    {
        HttpRequestMessage request = GetBuilder(GET_USERS_ENDPOINT)
            .WithAuthentication("username", "password")
            .AddEndpointParameter("id", id.ToString())
            .Build();

        HttpResponseMessage response = await Sender.SendAsync(request);

        return response.ReadAs<IEnumerable<User>>();
    }

    public async Task<IEnumerable<User>> CreateUser(User user)
    {
        HttpRequestMessage request = GetBuilder(CREATE_USER_ENDPOINT)
            .WithAuthentication("username", "password")
            .WithJsonBody(user)
            .Build();

        HttpResponseMessage response = await Sender.SendAsync(request);

        return response.ReadAs<IEnumerable<User>>();
    }
}

public class BearerAuthClient : BaseClient
{
    public BearerAuthClient(IRequestSender sender, string url) : base(sender, url)
    {

    }

    protected override async Task<IRequestBuilder> GetBuilderAsync(string route, Method method = Method.Get)
    {
        string token = await GetToken();
        return (await base.GetBuilderAsync(route, method)).WithAuthentication(token);
    }

    public async Task<string> GetToken()
    {
        // Handle token here;
        return "token";
    }
}

public class UserClientBis : BearerAuthClient
{
    private Endpoint CREATE_USER_ENDPOINT = new Endpoint("/api/users", Method.Post);

    public UserClientBis(IRequestSender sender, string url) : base(sender, url)
    {
    }

    public async Task<IEnumerable<User>> CreateUser(User user)
    {
        // Every builded request will be configured with bearer authentication !
        HttpRequestMessage request = (await GetBuilderAsync(CREATE_USER_ENDPOINT))
            .WithJsonBody(user)
            .Build();

        HttpResponseMessage response = await Sender.SendAsync(request);

        return response.ReadAs<IEnumerable<User>>();
    }
}

public class NotoriousAuthentication : IAuthenticationInformation
{
    private readonly string _crownId;
    private readonly string _crownName;
    public NotoriousAuthentication(string crownId, string crownName)
    {
        _crownId = crownId;
        _crownName = crownName;
    }
    public string Token => _crownName + _crownId;

    public string Scheme => "Notorious";
}

public class CustomSerializer : IJsonSerializer