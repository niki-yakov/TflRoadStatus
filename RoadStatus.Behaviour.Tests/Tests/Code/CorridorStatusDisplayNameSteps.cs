using RoadStatus.REST;
using RoadStatus.REST.HttpRestClient;
using RoadStatus.REST.Interfaces;
using System.Net.Http;
using TechTalk.SpecFlow;
using Xunit;
using Moq;
using RoadStatus.Common.Tests;

namespace RoadStatus.Behaviour.Tests
{
    [Binding]
    public class CorridorStatusDisplayNameSteps
    {
        private const string Severity = "Good";
        private const string SeverityDescription = "No Exceptional Delays";
        private const string Road = "A2";

        private const int ExpectedExitCode = 0;
        private string RequestRoad;
        private int ExitCode;

        private readonly IRestClient RestClientMock;
        private readonly IPrint Print;
        private readonly IRoadStatusValidator RoadValidator;

        private readonly Mock<IConfig> ConfigMock;

        public CorridorStatusDisplayNameSteps()
        {
            ConfigMock = new Mock<IConfig>();
            ConfigMock.Setup(c => c.Url).Returns(() => "http://testurl/HttpStatus200");
            RestClientMock = new RestClient(ConfigMock.Object, new HttpClient(new DelegatingHandlerStub()));

            Print = new Print();
            RoadValidator = new RoadStatusValidator(RestClientMock, Print);
        }

        [Given(@"a valid road ID (.*) is specified")]
        public void GivenIHaveEnteredARoad(string road)
        {
            this.RequestRoad = road;
        }

        [When(@"the client is run")]
        public void WhenIRunTheApplication()
        {
            ExitCode = RoadValidator.GetRoadCurrentStatus(this.RequestRoad);
        }

        [Then(@"the road ‘displayName’ should be displayed")]
        public void ThenTheRoadDisplayNameShouldBeDisplayed()
        {
            Assert.Equal(ExpectedExitCode, ExitCode);
            Assert.NotNull(RoadValidator.CorridorStatus.displayName);
            Assert.NotEmpty(RoadValidator.CorridorStatus.displayName);

            Assert.Equal(this.RequestRoad, RoadValidator.CorridorStatus.displayName);
        }

        [Then(@"the road ‘(.*)’ should be displayed as ‘(.*)’")]
        public void ThenTheRoadStatusSeverityShouldBeDisplayedAsRoadStatus(string key, string text)
        {
            Assert.Equal(RoadValidator.Print.ReportConstants[key], text);
            Assert.Equal(ExpectedExitCode, ExitCode);
            Assert.NotNull(RoadValidator.CorridorStatus.statusSeverity);
            Assert.NotEmpty(RoadValidator.CorridorStatus.statusSeverity);
        }
    }
}
