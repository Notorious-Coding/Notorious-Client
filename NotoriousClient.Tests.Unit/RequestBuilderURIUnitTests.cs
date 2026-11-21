using NotoriousClient.Builder;
using NotoriousClient.Tests.Unit.Attributes;

namespace NotoriousClient.Tests.Unit
{
    public class RequestBuilderURIUnitTests
    {
        #region URI
        [GWTFact(given: "a url, an endpoint, and an HTTP Verb",
                 when: "i build a request",
                 then: "request has right url, endpoint and verb")]
        public void RequestBuilder_Should_HaveRightUrlEndpointAndVerb()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            RequestBuilder requestBuilder = new RequestBuilder(url, endpoint);
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas", request.RequestUri.AbsoluteUri);
        }

        [GWTFact(given: "a url with and end slash, an endpoint with a start slash, and an HTTP Verb",
                 when: "i build a request",
                 then: "request has right url, endpoint and verb")]
        public void RequestBuilder_Should_HandleUrlSlashProperly()
        {
            string url = "https://toto.com/";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            RequestBuilder requestBuilder = new RequestBuilder(url, endpoint);
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas", request.RequestUri.AbsoluteUri);
        }

        [GWTFact(given: "a url, an endpoint, and an HTTP Verb",
         when: "i build a request",
         then: "request has right url, endpoint and verb")]
        public void RequestBuilder_Should_AddUrlSlashProperly()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("pandas", Method.Get);

            RequestBuilder requestBuilder = new RequestBuilder(url, endpoint);
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas", request.RequestUri.AbsoluteUri);
        }

        [GWTFact(given: "a url, an endpoint, an HTTP Verb and a version",
                 when: "i build a request",
                 then: "request has right url, endpoint, verb, and version")]
        public void RequestBuilder_Should_HandleVersionProperly()
        {
            string url = "https://toto.com/api";
            VersionedEndpoint endpoint = new VersionedEndpoint("pandas", Method.Get, "v1.0");

            RequestBuilder requestBuilder = new RequestBuilder(url, endpoint);
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/api/v1.0/pandas", request.RequestUri.AbsoluteUri);
        }
        #endregion

        #region QueryParams
        [GWTFact(given: "a request with one query parameters",
         when: "i build a request",
         then: "request has one query params")]
        public void RequestBuilder_Should_HaveOneQueryParams()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .AddQueryParameter("toto", "toto");
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas?toto=toto", request.RequestUri.AbsoluteUri);
        }

        [GWTFact(given: "a request with two query parameters",
                 when: "i build a request",
                 then: "request has two query params")]
        public void RequestBuilder_Should_HaveTwoQueryParams()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .AddQueryParameter("toto", "toto")
                .AddQueryParameter("toto2", "toto2");
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas?toto=toto&toto2=toto2", request.RequestUri.AbsoluteUri);
        }
        #endregion

        #region EndpointParams
        [GWTFact(given: "a request with one endpoint parameters",
         when: "i build a request",
         then: "request has correct endpoint params")]
        public void RequestBuilder_Should_HandleEndpointParams()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas/{id}", Method.Get);

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .AddEndpointParameter("id", "1");
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas/1", request.RequestUri.AbsoluteUri);
        }

        [GWTFact(given: "a request with one endpoint parameters and an endpoint without replacement token",
         when: "i build a request",
         then: "it throws a KeyNotFoundException")]
        public void RequestBuilder_Should_ThrowKeyNotFoundExceptionWhenNoToken()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .AddEndpointParameter("id", "1");

            Assert.Throws<KeyNotFoundException>(() => requestBuilder.Build());
        }

        [GWTFact(given: "a request with an endpoint parameter and a query parameter",
         when: "i build a request",
         then: "both are applied")]
        public void RequestBuilder_Should_HandleEndpointAndQueryParameters()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas/{id}", Method.Get);

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .AddEndpointParameter("id", "1")
                .AddQueryParameter("toto", "toto");
            HttpRequestMessage request = requestBuilder.Build();

            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas/1?toto=toto", request.RequestUri.AbsoluteUri);
        }

        [GWTFact(given: "a request with a query parameter value containing braces",
         when: "i build a request",
         then: "the value is kept as is")]
        public void RequestBuilder_Should_NotReplaceTokensInQueryParameters()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas/{id}", Method.Get);

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .AddEndpointParameter("id", "1")
                .AddQueryParameter("filter", "{id}");
            HttpRequestMessage request = requestBuilder.Build();

            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas/1?filter={id}", request.RequestUri.OriginalString);
        }
        #endregion
    }
}