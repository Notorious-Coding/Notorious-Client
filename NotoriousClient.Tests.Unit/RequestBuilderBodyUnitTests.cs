using NotoriousClient.Builder;
using NotoriousClient.Converters;
using NotoriousClient.Tests.Unit.Attributes;
using System.Collections.Specialized;

namespace NotoriousClient.Tests.Unit
{
    public class RequestBuilderBodyUnitTests
    {
        internal class User
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        [GWTFact(
            given: "A request with a json body",
            when: "i build a request",
            then: "request has correct json body")]
        public async Task RequestBuilder_Should_SerializeObjectAsJson()
        {

            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            var data = new
            {
                Id = 1,
                Name = "Toto"
            };

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .WithJsonBody(data);
            HttpRequestMessage request = requestBuilder.Build();

            string json = await request.Content.ReadAsStringAsync();

            Assert.Equal(@"{""Id"":1,""Name"":""Toto""}", json);
        }

        [GWTFact(
            given: "A request with a strream body",
            when: "i build a request",
            then: "request has correct stream body")]
        public async Task RequestBuilder_Should_AddStreamCorrectly()
        {

            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            Stream stream = new MemoryStream(GetRandomArray(20));

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .WithStreamBody(stream);
            HttpRequestMessage request = requestBuilder.Build();

            Stream result = await request.Content.ReadAsStreamAsync();
            Assert.Equal(typeof(StreamContent), request.Content.GetType());
            Assert.Equal(stream.Length, result.Length);
        }

        [GWTFact(
            given: "A request with a http content",
            when: "i build a request",
            then: "request has correct http content")]
        public async Task RequestBuilder_Should_AddHttpContentCorrectly()
        {

            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);


            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .WithContentBody(new StringContent("toto"));

            HttpRequestMessage request = requestBuilder.Build();

            Assert.Equal(typeof(StringContent), request.Content.GetType());
            Assert.Equal("toto", await request.Content.ReadAsStringAsync());
        }

        [GWTFact(
            given: "A multipart request with a json body and a stream body",
            when: "i build a multipart request",
            then: "request has correct section with json body and correct stream")]
        public async Task RequestBuilder_Should_CorrectlyBuildMultipartRequest()
        {

            string url = "https://toto.com";
            Endpoint endpoint = new Endpoint("/pandas", Method.Get);

            var data = new
            {
                Id = 1,
                Name = "Toto"
            };

            Stream stream = new MemoryStream(GetRandomArray(20));

            IRequestBuilder requestBuilder = new RequestBuilder(url, endpoint)
                .WithJsonMultipartBody(data, "data")
                .WithStreamMultipartBody(stream, "stream");
            HttpRequestMessage request = requestBuilder.Build();

            MultipartMemoryStreamProvider bodies = await request.Content.ReadAsMultipartAsync();

            Assert.Equal(2, bodies.Contents.Count);
            Assert.Equal(@"{""Id"":1,""Name"":""Toto""}", await bodies.Contents[0].ReadAsStringAsync());
            Assert.Equal(stream.Length, (await bodies.Contents[1].ReadAsStreamAsync()).Length);
        }

        [GWTFact(
            given: "A request with a json body and source generated metadata",
            when: "i build a request",
            then: "request has correct json body and content type")]
        public async Task RequestBuilder_Should_SerializeJsonBodyWithJsonTypeInfo()
        {
            var user = new HttpResponseMessageExtensionsUnitTests.User { Id = 1, Name = "Toto" };

            HttpRequestMessage request = new RequestBuilder("https://toto.com", new Endpoint("/pandas", Method.Post))
                .WithJsonBody(user, TestJsonContext.Default.User)
                .Build();

            Assert.Equal("application/json", request.Content!.Headers.ContentType!.MediaType);
            Assert.Equal(@"{""Id"":1,""Name"":""Toto""}", await request.Content.ReadAsStringAsync());
        }

        [GWTFact(
            given: "A multipart request with a json body and source generated metadata",
            when: "i build a multipart request",
            then: "request has correct section with json body")]
        public async Task RequestBuilder_Should_SerializeJsonMultipartBodyWithJsonTypeInfo()
        {
            var user = new HttpResponseMessageExtensionsUnitTests.User { Id = 1, Name = "Toto" };

            HttpRequestMessage request = new RequestBuilder("https://toto.com", new Endpoint("/pandas", Method.Post))
                .WithJsonMultipartBody(user, "data", TestJsonContext.Default.User)
                .WithStreamMultipartBody(new MemoryStream(GetRandomArray(1)), "stream")
                .Build();

            MultipartMemoryStreamProvider bodies = await request.Content!.ReadAsMultipartAsync();

            Assert.Equal(2, bodies.Contents.Count);
            Assert.Equal(@"{""Id"":1,""Name"":""Toto""}", await bodies.Contents[0].ReadAsStringAsync());
        }

        [GWTFact(
            given: "A request with a json body and a serializer built with a JsonSerializerContext",
            when: "i build a request",
            then: "request has correct json body")]
        public async Task RequestBuilder_Should_SerializeJsonBodyWithContextSerializer()
        {
            object user = new HttpResponseMessageExtensionsUnitTests.User { Id = 2, Name = "Titi" };

            HttpRequestMessage request = new RequestBuilder("https://toto.com", new Endpoint("/pandas", Method.Post))
                .WithJsonBody(user, new SystemTextJsonSerializer(TestJsonContext.Default))
                .Build();

            Assert.Equal(@"{""Id"":2,""Name"":""Titi""}", await request.Content!.ReadAsStringAsync());
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