// See https://aka.ms/new-console-template for more information

using NotoriousClient.Framework.Web.Client.Builder;
using NotoriousClient.Framework.Web.Client.Sender;

Console.WriteLine("Hello, World!");


IRequestBuilder requestBuilder = new RequestBuilder("https://toto.com", "/users", Method.Get);

HttpRequestMessage request = requestBuilder
    .AddCustomHeader("X-API-TOKEN", "fjfdzsfzfazgaegaz")
    .WithAuthentication("username", "password")
    .Build();


new RequestSender(null);
