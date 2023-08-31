using NotoriousClient.Builder;
using NotoriousClient.Tests.Unit.Attributes;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace NotoriousClient.Tests.Unit
{
    public class RequestBuilderHeaderUnitTests
    {
        internal class User
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        [GWTFact(
            given: "A request with a custom header",
            when: "i build a request",
            then: "request has correct header")]
        public async Task RequestBuilder_Should_HandleHeadersCorrectly()
        {

            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);


            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .AddCustomHeader("toto", "totoValue")
                .AddCustomHeader("toto2", "toto2Value");
            HttpRequestMessage request = requestBuilder.Build();


            Assert.Equal(2, request.Headers.Count());
            Assert.True(request.Headers.Contains("toto"));
            Assert.Equal("totoValue", request.Headers.GetValues("toto").First());
            Assert.True(request.Headers.Contains("toto2"));
            Assert.Equal("toto2Value", request.Headers.GetValues("toto2").First());
        }


        [GWTFact(
            given: "A request with accept header",
            when: "i build a request",
            then: "request has correct accept header")]
        public async Task RequestBuilder_Should_HandleAcceptHeaderProperly()
        {

            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);


            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .WithCustomAcceptMediaType(MediaTypeNames.Application.Json);
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(MediaTypeWithQualityHeaderValue.Parse(MediaTypeNames.Application.Json), request.Headers.Accept.First());
        }

        [GWTFact(
            given: "A request with basic auth header",
            when: "i build a request",
            then: "request has correct basic token in authorization header")]
        public async Task RequestBuilder_Should_HandleBasicAuthProperly()
        {

            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);


            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .WithAuthentication("username", "password");
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal("Basic", request.Headers.Authorization.Scheme);
            Assert.Equal("dXNlcm5hbWU6cGFzc3dvcmQ=", request.Headers.Authorization.Parameter);
        }

        [GWTFact(
            given: "A request with bearer auth header",
            when: "i build a request",
            then: "request has correct bearer token in authorization header")]
        public async Task RequestBuilder_Should_HandleBearerAuthProperly()
        {

            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            string token = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTY5MzUxMTgzNiwiaWF0IjoxNjkzNTExODM2fQ.Krb2tFVlLba4iLYlWdmd4o97N19XMNHwLpxHz1LOiI0";
            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .WithAuthentication(token);
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal("Bearer", request.Headers.Authorization.Scheme);
            Assert.Equal(token, request.Headers.Authorization.Parameter);
        }

        #region Private Methods
        private byte[] GetRandomArray(int sizeInKb)
        {

            var rnd = new Random();
            var bytes = new Byte[sizeInKb * 1024];
            rnd.NextBytes(bytes);
            return bytes;
        }
        #endregion
    }
}