## ![Logo](./Documentation/Images/NotoriousClient.png)

**Notorious Client** est une librairie simplifiant l'envoie de requête HTTP grâce a un builder de requête et un système de client extensible a l'infini.

## Support

- Net6/7

## Fonctionnalitées

- Construire des HttpRequestMessage simplement grâce au builder
- Serialisation JSON des bodys automatique
- Gestion des requêtes multipart/form-data
- Gestion de l'authentification Basic Auth
- Personnalisable a l'infini
- Code clair et maintenable

## Motivation

L'objectif est de fournir une manière de créer des clients d'API de manière clair, rapide, et surtout maintenable.

## Getting Started

Installer le Nuget **NotoriousClient**.

Créer une classe héritant de BaseClient :

```csharp
using NotoriousClient.Clients;

public class UserClient : BaseClient, IUserClient
{
    public UserClient(IRequestSender sender, string url) : base(sender, url)
    {
    }
}
```

Ajouter des méthodes dans le client pour appeller l'api concerné.

```csharp
public class UserClient : BaseClient, IUserClient
{
    // Définissez vos endpoints
    private Endpoint GET_USERS_ENDPOINT = new Endpoint("/api/users", Method.Get);

    public UserClient(IRequestSender sender, string url) : base(sender, url)
    {
    }

    // Ajouter la méthode d'appel
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

D'autre exemple d'appel API:

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

Vous pouvez ensuite créer ce client en lui donnant un IRequestSender :

```csharp
// Vous pouvez récupérer un HttpClient depuis l'injection
services.AddHttpClient();
services.AddScoped<IRequestSender, RequestSender>();
services.AddScoped<IUserClient>((serviceProvider) =>
{
    new UserClient(serviceProvider.GetRequiredService<IRequestSender>(), "http://my.api.com/");
});
```
