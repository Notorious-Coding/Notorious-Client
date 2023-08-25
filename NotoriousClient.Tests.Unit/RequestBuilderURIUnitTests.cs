using NotoriousClient.Framework.Web.Client.Builder;
using NotoriousClient.Tests.Unit.Attributes;
using System.ComponentModel;

namespace NotoriousClient.Tests.Unit
{
    public class RequestBuilderURIUnitTests
    {
        #region URI
        [GWTFact(given: "a url, an endpoint, and an HTTP Verb", 
                 when: "i build a request", 
                 then: "request has right url, endpoint and verb")]
        [Trait("Category", "Uri")]
        public void RequestBuilder_Should_HaveRightUrlEndpointAndVerb()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            RequestBuilder requestBuilder = new RequestBuilder(url, endpoint);
            HttpRequestMessage request  = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas", request.RequestUri!.ToString());
        }

        [GWTFact(given: "a url with and end slash, an endpoint with a start slash, and an HTTP Verb",
                 when: "i build a request",
                 then: "request has right url, endpoint and verb")]
        [Trait("Category", "Uri")]
        public void RequestBuilder_Should_HandleUrlSlashProperly()
        {
            string url = "https://toto.com/";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            RequestBuilder requestBuilder = new RequestBuilder(url, endpoint);
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas", request.RequestUri!.ToString());
        }

        [GWTFact(given: "a url, an endpoint, and an HTTP Verb",
         when: "i build a request",
         then: "request has right url, endpoint and verb")]
        [Trait("Category", "Uri")]
        public void RequestBuilder_Should_AddUrlSlashProperly()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("pandas", Method.Get);

            RequestBuilder requestBuilder = new RequestBuilder(url, endpoint);
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas", request.RequestUri!.ToString());
        }
        #endregion

        #region QueryParams
        [GWTFact(given: "a request with one query parameters",
         when: "i build a request",
         then: "request has one query params")]
        [Trait("Category", "Query Parameters")]
        public void RequestBuilder_Should_HaveOneQueryParams()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .AddQueryParameter("toto", "toto");
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas?toto%3dtoto", request.RequestUri!.ToString());
        }

        [GWTFact(given: "a request with two query parameters",
                 when: "i build a request",
                 then: "request has two query params")]
        [Trait("Category", "Query Parameters")]
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
            Assert.Equal("https://toto.com/pandas?toto%3dtoto%26toto2%3dtoto2", request.RequestUri!.ToString());
        }
        #endregion

        #region EndpointParams
        [GWTFact(given: "a request with one endpoint parameters",
         when: "i build a request",
         then: "request has correct endpoint params")]
        [Trait("Category", "Endpoint Parameters")]
        public void RequestBuilder_Should_HandleEndpointParams()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas/{id}", Method.Get);

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .AddEndpointParameter("id", "1");
            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.NotNull(request.RequestUri);
            Assert.Equal("https://toto.com/pandas/1", request.RequestUri!.ToString());
        }

        [GWTFact(given: "a request with one endpoint parameters and an endpoint without replacement token",
         when: "i build a request",
         then: "it throws a KeyNotFoundException")]
        [Trait("Category", "Endpoint Parameters")]
        public void RequestBuilder_Should_ThrowKeyNotFoundExceptionWhenNoToken()
        {
            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .AddEndpointParameter("id", "1");

            Assert.Throws<KeyNotFoundException>(() => requestBuilder.Build());
        }
        #endregion
    }
}