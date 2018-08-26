using Newtonsoft.Json;
using RoadStatus.REST;
using RoadStatus.REST.HttpRestClient;
using RoadStatus.REST.Interfaces;
using RoadStatus.REST.Models;
using System.Linq;
using System.Net.Http;
using Xunit;
using Autofac;

namespace RoadStatus.Integration.Tests
{
    public class RestClientTest
    {
        private readonly IRestClient RestClient;
        private string Road;

        public RestClientTest()
        {
            RestClient = RoadStatusContainer.Container.Resolve<IRestClient>();
        }

        [Fact]
        public void When_Execute_Request_With_Correct_Road_Then_Returns_Json()
        {
            this.Road = "A2";
            var expectedRoad = "A2";
            var result = RestClient.Get(this.Road);

            var road = JsonConvert.DeserializeObject<CorridorStatus[]>(result).First();

            Assert.NotNull(result);
            Assert.Equal(expectedRoad, road.displayName);
        }
    }
}
