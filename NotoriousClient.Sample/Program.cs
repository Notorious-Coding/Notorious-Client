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


//new RequestSender(null as IHttpClientFactory);


var services = new ServiceCollection();

services.AddHttpClient();
services.AddScoped<IRequestSender>((serviceProvider) => new RequestSender(serviceProvider.GetRequiredService<IHttpClientFactory>()));
services.AddScoped((serviceProvider) => new UserClient(serviceProvider.GetRequiredService<IRequestSender>(), "http://my.api.com/"));

ServiceProvider provider = services.BuildServiceProvider();
Console.WriteLine(provider);
UserClient userClient = provider.GetRequiredService<UserClient>();
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

interface ITotoBuilder : IRequestBuilder
{
    public ITotoBuilder AddToto();
}

public class TotoBuilder : RequestBuilder, ITotoBuilder
{
    string toto = "";
    public TotoBuilder(string url, string route, Method method) : base(url, route, method)
    {
    }

    public override HttpRequestMessage Build()
    {
        HttpRequestMessage request =  base.Build();
        request.Headers.Add("TOTO", toto);
        return request;
    }

    ITotoBuilder ITotoBuilder.AddToto()
    {
        toto = "toto";
        return this;
    }
}


public class TotoClient : BaseClient
{
    public TotoClient(IRequestSender sender, string url) : base(sender, url)
    {
    }

    public async Task GetToto()
    {
    }

    private ITotoBuilder GetTotoBuilder()
    {
        return new TotoBuilder("", "", method: Method.Get);
    }
}