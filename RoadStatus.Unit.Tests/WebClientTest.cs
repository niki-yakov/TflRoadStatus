using Moq;
using Newtonsoft.Json;
using RoadStatus.Common.Tests;
using RoadStatus.REST.HttpRestClient;
using RoadStatus.REST.Interfaces;
using RoadStatus.REST.Models;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace RoadStatus.Unit.Tests
{
    public class WebClientTest
    {
        private readonly Mock<IConfig> ConfigMock;
        private readonly HttpClient Client;
        private readonly IRestClient RestClient;
        private string Road;

        public WebClientTest()
        {
            ConfigMock = new Mock<IConfig>();
            Client = new HttpClient(new DelegatingHandlerStub());
            RestClient = new RestClient(ConfigMock.Object, Client);

        }

        [Fact]
        public void When_Execute_Request_With_Correct_Url()
        {
            this.Road = "A2";
            var expectedRoad = "A2";
            var expectedSeverity = "Good";

            SetupConfigUri("http://testurl/HttpStatus200");

            var roadStatus = GetRoadStatus<CorridorStatus[]>().First();

            Assert.NotNull(roadStatus);

            Assert.Equal(expectedRoad, roadStatus.displayName);
            Assert.Equal(expectedSeverity, roadStatus.statusSeverity);
        }

        [Fact]
        public void When_Execute_Request_With_Wrong_Url()
        {
            this.Road = "A2";
            SetupConfigUri("http://testurl/HttpStatus400");

            Assert.Throws<HttpRequestException>(() => RestClient.Get(this.Road));
        }

        [Fact]
        public void When_Execute_Request_With_Invalid_Road_Then_Returns_Json()
        {
            this.Road = "A223";
            var expectedHttpStatus = "NotFound";
            var expectedHttpStatusCode = "404";
            SetupConfigUri("http://testurl/HttpStatus404");

            var roadStatus = GetRoadStatus<NotFoundError>();

            Assert.NotNull(roadStatus);

            Assert.Equal(expectedHttpStatus, roadStatus.httpStatus);
            Assert.Equal(expectedHttpStatusCode, roadStatus.httpStatusCode);
        }

        private T GetRoadStatus<T>()
        {
            var result = RestClient.Get(this.Road);
            return JsonConvert.DeserializeObject<T>(result);
        }

        private void SetupConfigUri(string uri)
        {
            ConfigMock.Setup(c => c.Url).Returns(() => uri);
        }

    }
}