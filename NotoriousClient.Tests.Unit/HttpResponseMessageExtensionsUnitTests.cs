using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NotoriousClient.Sender;
using NotoriousClient.Tests.Unit.Attributes;

namespace NotoriousClient.Tests.Unit
{
    public class HttpResponseMessageExtensionsUnitTests
    {
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        private static HttpResponseMessage JsonResponse(string json)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }

        [GWTFact(
            given: "A json response with camelCase properties",
            when: "i read it as an object",
            then: "properties are mapped case-insensitively")]
        public async Task ReadAsAsync_Should_DeserializeCaseInsensitive()
        {
            HttpResponseMessage response = JsonResponse(@"{""id"":1,""name"":""Toto""}");

            User user = await response.ReadAsAsync<User>();

            Assert.Equal(1, user.Id);
            Assert.Equal("Toto", user.Name);
        }

        [GWTFact(
            given: "A json response",
            when: "i read it synchronously with custom options",
            then: "options are applied")]
        public void ReadAs_Should_UseProvidedOptions()
        {
            HttpResponseMessage response = JsonResponse(@"{""Id"":1,""Name"":""Toto""}");

            User user = response.ReadAs<User>(new JsonSerializerOptions());

            Assert.Equal(1, user.Id);
            Assert.Equal("Toto", user.Name);
        }

        [GWTFact(
            given: "A json response",
            when: "i read it with source generated metadata",
            then: "response is deserialized")]
        public async Task ReadAsAsync_Should_DeserializeWithJsonTypeInfo()
        {
            HttpResponseMessage response = JsonResponse(@"{""Id"":2,""Name"":""Titi""}");

            User user = await response.ReadAsAsync(TestJsonContext.Default.User);

            Assert.Equal(2, user.Id);
            Assert.Equal("Titi", user.Name);
        }
    }

    [JsonSerializable(typeof(HttpResponseMessageExtensionsUnitTests.User))]
    internal partial class TestJsonContext : JsonSerializerContext
    {
    }
}
