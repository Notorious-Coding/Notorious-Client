## ![Logo](./Documentation/Images/NotoriousClient.png)

**Notorious Client** is meant to simplify the sending of HTTP requests through a fluent builder and an infinitely extensible client system.

[![NuGet](http://img.shields.io/nuget/vpre/NotoriousClient.svg?label=NuGet)](https://www.nuget.org/packages/NotoriousClient/)

## Summary

- [Support](#support)
- [Features](#features)
- [Motivation](#motivation)
- [Getting started](#getting-started)
- [How could i use RequestBuilder ?](#how-could-i-use-requestbuilder)
- [How could i read responses ?](#how-could-i-read-responses)
- [How could i implement a custom BaseClient](#how-could-i-implement-a-custom-baseclient)
- [How could i call an API secured with client credentials ?](#how-could-i-call-an-api-secured-with-client-credentials)
- [How could i use requestBuilder in Standalone ?](#how-can-i-use-the-requests-builder-in-standalone)
- [Migrating from 2.x to 3.0](#migrating-from-2x-to-30)

## Support

- .NET 10+

## Features

- Easy building of HttpRequestMessage
- Easy building of multipart/form-data requests
- Body serialisation as JSON, powered by System.Text.Json, but customisable.
- Response deserialisation powered by System.Text.Json, with trimming / native AOT friendly overloads.
- Easy handling of request's authentication (basic, bearer, custom schemes, OAuth2 client credentials).
- Infinitely extensible system.
- Allow you to build maintanable and testable API Client.

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
    HttpRequestMessage request = GetBuilder(GET_USER_ENDPOINT)
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
services.AddScoped<IRequestSender>((serviceProvider) => new RequestSender(serviceProvider.GetRequiredService<IHttpClientFactory>()));
services.AddScoped((serviceProvider) => new UserClient(serviceProvider.GetRequiredService<IRequestSender>(), "http://my.api.com/"));
```

## How could i use RequestBuilder ?

Lets dive into the possibity of the RequestBuilder !

### Configure URN, URL, and http verb.

```csharp
new RequestBuilder("https://toto.com", "/users", Method.Get);
```

Or with `Endpoint` class :

```csharp
Endpoint endpoint = new Endpoint("/users", Method.Get);
new RequestBuilder("https://toto.com", endpoint);
```

or with `VersionedEndpoint` class :

```csharp
VersionedEndpoint endpoint = new VersionedEndpoint("/users", Method.Get, "v1.0");
new RequestBuilder("https://toto.com/api", endpoint); // Will result in "https://toto.com/api/v1.0/users"
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

## How could i read responses ?

`NotoriousClient.Sender` provides extension methods on `HttpResponseMessage`, each one with a synchronous and an asynchronous version.

```csharp
HttpResponseMessage response = await Sender.SendAsync(request);

User user = await response.ReadAsAsync<User>();     // or response.ReadAs<User>()
string raw = await response.ReadAsStringAsync();     // or response.ReadAsString()
byte[] bytes = await response.ReadAsByteArrayAsync(); // or response.ReadAsByteArray()
Stream stream = await response.ReadAsStreamAsync();  // or response.ReadAsStream()
```

**JSON deserialization**

`ReadAs<T>` / `ReadAsAsync<T>` are powered by System.Text.Json. By default, they use `JsonSerializerDefaults.Web` options, so property names are matched **case-insensitively** (`{"name": "Toto"}` maps to `User.Name`).

You can provide your own options :

```csharp
JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
{
    Converters = { new JsonStringEnumConverter() }
};

User user = await response.ReadAsAsync<User>(options);
```

> :warning: Responses must have a JSON media type (`application/json`, `application/problem+json`, ...) or no `Content-Type` at all, otherwise an `InvalidOperationException` is thrown.

**Trimming and native AOT**

The overloads above rely on reflection and are annotated with `[RequiresUnreferencedCode]` / `[RequiresDynamicCode]`. If your application is trimmed or published with native AOT, use [System.Text.Json source generation](https://learn.microsoft.com/dotnet/standard/serialization/system-text-json/source-generation) and pass the generated `JsonTypeInfo<T>` :

```csharp
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(IEnumerable<User>))]
internal partial class UserJsonContext : JsonSerializerContext
{
}

User user = await response.ReadAsAsync(UserJsonContext.Default.User);
IEnumerable<User> users = response.ReadAs(UserJsonContext.Default.IEnumerableUser);
```

## How could i implement a custom BaseClient ?

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

    protected override async Task<IRequestBuilder> GetBuilderAsync(string route, Method method = Method.Get, string? version = null)
    {
        // Get your token every time you create a request.
        string token = await GetToken();

        // Return a preconfigured builder with your token !
        return (await base.GetBuilderAsync(route, method, version)).WithAuthentication(token);
    }

    public async Task<string> GetToken()
    {
        // Handle token logic here.
        return await _tokenClient.GetAccessToken();
    }
}

public class UserClient : BearerAuthClient
{
    private Endpoint CREATE_USER_ENDPOINT = new Endpoint("/api/users", Method.Post);

    public UserClient(IRequestSender sender, string url, ITokenClient tokenClient) : base(sender, url, tokenClient)
    {
    }

    public async Task<User> CreateUser(User user)
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

> :bulb: For simple cases, `BasicAuthBaseClient` (basic authentication) and `ClientCredentialsBaseClient` (see below) are provided out of the box.

## How could i call an API secured with client credentials ?

For machine-to-machine calls, inherit from `ClientCredentialsBaseClient`. Every request built with `GetBuilderAsync` gets a bearer token obtained through the OAuth2 **client credentials** flow.

```csharp
public class UserClient : ClientCredentialsBaseClient
{
    private Endpoint GET_USERS_ENDPOINT = new Endpoint("/api/users", Method.Get);

    public UserClient(ITokenClient tokenClient, IRequestSender sender, string url) : base(tokenClient, sender, url)
    {
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        // Use GetBuilderAsync : GetBuilder does not add the token.
        HttpRequestMessage request = (await GetBuilderAsync(GET_USERS_ENDPOINT)).Build();

        HttpResponseMessage response = await Sender.SendAsync(request);

        return await response.ReadAsAsync<IEnumerable<User>>();
    }
}
```

The token endpoint is discovered from the authority's `/.well-known/openid-configuration` document. Configure your authorization server :

```json
{
  "AuthorizationServer": {
    "Authority": "https://auth.my-company.com/realms/my-realm",
    "ClientId": "my-client",
    "ClientSecret": "my-secret",
    "Audiences": [ "users-api" ],
    "Scopes": [ "users.read" ]
  }
}
```

Then register everything :

```csharp
services.AddHttpClient();
services.AddMemoryCache();
services.Configure<AuthorizationServerOptions>(configuration.GetSection("AuthorizationServer"));

services.AddScoped<IRequestSender>((serviceProvider) => new RequestSender(serviceProvider.GetRequiredService<IHttpClientFactory>()));
services.AddScoped<ITokenClient, MemoryCachedTokenClient>();
services.AddScoped((serviceProvider) => new UserClient(
    serviceProvider.GetRequiredService<ITokenClient>(),
    serviceProvider.GetRequiredService<IRequestSender>(),
    "http://my.api.com/"));
```

Two token clients are available :

- `TokenClient` : requests a new token on every call.
- `MemoryCachedTokenClient` : caches the discovery document (24h) and the token in `IMemoryCache` until it expires (minus a 60 seconds margin). Recommended.

## How can i use the request's builder in standalone ?

You can create a standalone builder by instantiating RequestBuilder.

```csharp

// Dont forget to use IRequestBuilder to have access to extensions method !
IRequestBuilder requestBuilder = new RequestBuilder("http://my.api.com/", "api/v1.0/users", Method.Get);

```

Then, you will have access to all same method that you were using in Client !

## Migrating from 2.x to 3.0

Version 3.0 contains breaking changes :

- **.NET 10 only** : .NET 8 is no longer supported. Stay on 2.x if you can't upgrade yet.
- **No more `Microsoft.AspNet.WebApi.Client` / Newtonsoft.Json** : responses are now deserialized with System.Text.Json.
  - `ReadAsAsync<T>(IList<MediaTypeFormatter>)` is replaced by `ReadAsAsync<T>(JsonSerializerOptions?)`. Calls without arguments (`response.ReadAs<User>()`) still compile.
  - If your models rely on Newtonsoft attributes (`[JsonProperty]`, `[JsonIgnore]` from `Newtonsoft.Json`, ...), replace them with their System.Text.Json equivalents (`[JsonPropertyName]`, `[JsonIgnore]` from `System.Text.Json.Serialization`, ...).
  - If you were using `HttpContent` extensions from `System.Net.Http.Formatting` (`ReadAsAsync`, `ReadAsMultipartAsync`, ...) through NotoriousClient, reference `Microsoft.AspNet.WebApi.Client` directly in your project.

See the [CHANGELOG](CHANGELOG.md) for details.
