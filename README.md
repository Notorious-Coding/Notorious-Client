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

## How can i use the request's builder in standalone ? 

You can create a standalone builder by instantiating RequestBuilder.
```csharp

// Dont forget to use IRequestBuilder to have access to extensions method !
new RequestBuilder("http://my.api.com/", "api/v1.0/users", Method.GET);

```

Then, you will have access to all same method that you were using in Client !
## How could i use RequestBuilder : 

Lets dive into the possibity of the RequestBuilder ! 

### Configure URN, URL, and http verb.

```csharp
new RequestBuilder("https://toto.com", "/users", Method.Get);
```

### Configure URI parameters

**Add URL parameters**
```csharp
new RequestBuilder("https://toto.com", "/users/{id}", Method.Get).AddEndpointParameter("id", "myfakeid");

```

```csharp
IDictionary<string, string> endpointsParams = ...;
new RequestBuilder("https://toto.com", "/users/{id}/{name}", Method.Get).AddEndpointParameters(endpointsParams);
```

**Add Query Parameters**

```csharp
new RequestBuilder("https://toto.com", "/users", Method.Get).AddQueryParameter("id", "myfakeid");

```

```csharp
IDictionary<string, string> queryParams = ...;
new RequestBuilder("https://toto.com", "/users", Method.Get).AddQueryParameters(queryParams);
```

### Configure request's headers

**Add custom headers**
```csharp
new RequestBuilder("https://toto.com", "/users", Method.Get).AddCustomHeader("id", "myfakeid");

```

```csharp
IDictionary<string, string> headers = ...;
new RequestBuilder("https://toto.com", "/users", Method.Get).AddCustomHeaders(headers);
```

**Add accept header**
```csharp
new RequestBuilder("https://toto.com", "/users", Method.Get).WithCustomAcceptMediaType("application/json");

```

### Authentication

**Add basic authentication**
```csharp
new RequestBuilder("https://toto.com", "/users", Method.Get).WithAuthentication("login", "password");

```

**Add bearer authentication**
```csharp
new RequestBuilder("https://toto.com", "/users", Method.Get).WithAuthentication("token");

```

**Add custom scheme authentication**
```csharp
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

new RequestBuilder("https://toto.com", "/users", Method.Get).WithAuthentication(new NotoriousAuthentication("1997", "BIG"));
```

### Add body to classic request

**Add body as JSON**
```csharp
User user = GetUsersFromDb()

new RequestBuilder("https://toto.com", "/users", Method.Get).WithJsonBody(user);

```

**Add body as JSON with a custom serializer**
```csharp

public class CustomSerializer : IJsonSerializer
{
    public string ConvertToJson(object obj)
    {
        // Your serilization logic here...
    }
}

User user = GetUsersFromDb()

new RequestBuilder("https://toto.com", "/users", Method.Get).WithJsonBody(user, new CustomSerializer());

```

**Add body as Stream**
```csharp
Stream stream = GetFileStream("C:/Crown/BIG.png")

new RequestBuilder("https://toto.com", "/users", Method.Get).WithStreamBody(stream);

```

**Add body as HTTP Content**
```csharp
new RequestBuilder("https://toto.com", "/users", Method.Get).WithContentBody(new StringContent("MyCustomContent"));
```

> :warning: Note that you could use any type of content handled by .NET, such as StringContent, StreamContent, HttpContent, etc...

### Add body to multipart request

> :warning: Note that you CAN'T use multipart bodies if you already added a classic body to the request


**Add body as JSON**
```csharp
User user = GetUsersFromDb()

new RequestBuilder("https://toto.com", "/users", Method.Get).WithJsonMultipartBody(user, "USER_SECTION");

```

**Add body as JSON with a custom serializer**
```csharp

public class CustomSerializer : IJsonSerializer
{
    public string ConvertToJson(object obj)
    {
        // Your serilization logic here...
    }
}

User user = GetUsersFromDb()

new RequestBuilder("https://toto.com", "/users", Method.Get).WithJsonMultipartBody(user, "USER_SECTION", new CustomSerializer());

```

**Add body as Stream**
```csharp
Stream stream = GetFileStream("C:/Crown/BIG.png")

new RequestBuilder("https://toto.com", "/users", Method.Get).WithStreamMultipartBody(stream, "STREAM_SECTION");

```

**Add body as HTTP Content**
```csharp
new RequestBuilder("https://toto.com", "/users", Method.Get).WithContentMultipartBody(new StringContent("MyCustomContent"), "CUSTOM_CONTENT_SECTION");
```

## How could i implement a custom BaseClient.

**NotoriousClient** is entirely designed to be infinitely extensible. 

Let's say you need to get a token from an API before every request.

```csharp
public class BearerAuthClient : BaseClient
{
    private readonly ITokenClient _tokenClient;

    public BearerAuthClient(IRequestSender sender, string url, ITokenClient tokenClient) : base(sender, url)
    {
        ArgumentNullException.ThrowIfNull(tokenClient, nameof(tokenClient));
        _tokenClient = tokenClient;
    }

    protected override async Task<IRequestBuilder> GetBuilderAsync(string route, Method method = Method.Get)
    {
        // Get your token every time you create a request. 
        string token = await GetToken();
        
        // Return a preconfigured builder with your token !
        return (await base.GetBuilderAsync(route, method)).WithAuthentication(token);
    }

    public async Task<string> GetToken()
    {
        // Handle token logic here.
        return await _tokenClient.GetToken();
    }
}

public class UserClient : BearerAuthClient
{
    private Endpoint CREATE_USER_ENDPOINT = new Endpoint("/api/users", Method.Post);

    public UserClient(IRequestSender sender, string url) : base(sender, url)
    {
    }

    public async Task<IEnumerable<User>> CreateUser(User user)
    {
        // Every builded request will be configured with bearer authentication !
        HttpRequestMessage request = (await GetBuilderAsync(CREATE_USER_ENDPOINT))
            .WithJsonBody(user)
            .Build();

        HttpResponseMessage response = await Sender.SendAsync(request);

        return response.ReadAs<User>();
    }
}
```
This is your turn to play with it, you could image everything you want, adding custom authentication, custom company headers, logging !