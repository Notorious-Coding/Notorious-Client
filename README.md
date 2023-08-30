## ![Logo](./Documentation/Images/NotoriousClient.png)

**Notorious Client** is meant to simplify the sending of HTTP requests through a fluent builder and an infinitely extensible client system.

## Support

- Net6/7

## Features

- Easy building of HttpRequestMessage
- Easy building of multipart/form-data requests
- Body serialisation as JSON, powered by Newtonsoft, but customisable.
- Easy handling of authentication in requests
- Infinitely extensible system.
- Permit clean code.

## Motivation

The goal is to provide a way to create API clients that is clear, fast, and above all, maintainable.

## Getting Started

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [NotoriousClient](https://www.nuget.org/packages/NotoriousClient/) from the package manager console:

```
PM> Install-Package NotoriousClient
```
Or from the .NET CLI as:
```
dotnet add package NotoriousClient
```


Then create a client, and inherit from BaseClient :

```csharp
using NotoriousClient.Clients;

public class UserClient : BaseClient, IUserClient
{
    public UserClient(IRequestSender sender, string url) : base(sender, url)
    {
    }
}
```

Add method within that client that uses RequestBuilder to build your request.

```csharp
public class UserClient : BaseClient, IUserClient
{
    // Define your endpoint
    private Endpoint GET_USERS_ENDPOINT = new Endpoint("/api/users", Method.Get);

    public UserClient(IRequestSender sender, string url) : base(sender, url)
    {
    }

    // Add call method.
    public async Task<IEnumerable<User>> GetUsers()
    {
        HttpRequestMessage request = GetBuilder(GET_USERS_ENDPOINT)
            .WithAuthentication("username", "password")
            .AddQueryParameter("limit", "100")
            .Build();

        HttpResponseMessage response = await Sender.SendAsync(request);

        return response.ReadAs<IEnumerable<User>>();
    }
}

```

Other example of API call.

```csharp
private Endpoint GET_USER_ENDPOINT = new Endpoint("/api/users/{id}", Method.Get);

public async Task<User> GetUser(int id)
{
    HttpRequestMessage request = GetBuilder(GET_USERS_ENDPOINT)
        .WithAuthentication("username", "password")
        .AddEndpointParameter("id", id.ToString())
        .Build();

    HttpResponseMessage response = await Sender.SendAsync(request);

    return response.ReadAs<User>();
}
```

```csharp
private Endpoint CREATE_USER_ENDPOINT = new Endpoint("/api/users", Method.Post);

public async Task<User> CreateUser(User user)
{
    HttpRequestMessage request = GetBuilder(CREATE_USER_ENDPOINT)
        .WithAuthentication("username", "password")
        .WithJsonBody(user)
        .Build();

    HttpResponseMessage response = await Sender.SendAsync(request);

    return response.ReadAs<User>();
}
```

Last but not least, add everything to your dependency injection.

```csharp
services.AddHttpClient();
services.AddScoped<IRequestSender, RequestSender>();
services.AddScoped<IUserClient>((serviceProvider) =>
{
    new UserClient(serviceProvider.GetRequiredService<IRequestSender>(), "http://my.api.com/");
});
```
