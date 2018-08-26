using RoadStatus.REST.Interfaces;
using System;
using TechTalk.SpecFlow;
using Autofac;
using Xunit;

namespace RoadStatus.e2e.Tests
{
    [Binding]
    public class CorridorStatusDisplayNameSteps
    {
        private IRoadStatusValidator RoadStatusValidator;
        private const string Severity = "Good";
        private const string SeverityDescription = "No Exceptional Delays";

        private const int ExpectedExitCode = 0;
        private string RequestRoad = "A2";
        private int ExitCode;

        public string Road { get; private set; }

        public CorridorStatusDisplayNameSteps()
        {
            RoadStatusValidator = RoadStatusContainer.Container.Resolve<IRoadStatusValidator>();
        }
        [Given(@"a valid road ID (.*) is specified")]
        public void GivenAValidRoadIDAIsSpecified(string road)
        {
            this.Road = road;
        }
        
        [When(@"the client is run")]
        public void WhenTheClientIsRun()
        {
            ExitCode = RoadStatusValidator.GetRoadCurrentStatus(this.Road);
        }

        [Then(@"the road ‘displayName’ should be displayed")]
        public void ThenTheRoadDisplayNameShouldBeDisplayed()
        {
            Assert.Equal(ExpectedExitCode, ExitCode);
            Assert.NotNull(RoadStatusValidator.CorridorStatus.displayName);
            Assert.NotEmpty(RoadStatusValidator.CorridorStatus.displayName);

            Assert.Equal(this.RequestRoad, RoadStatusValidator.CorridorStatus.displayName);
        }

        [Then(@"the road ‘(.*)’ should be displayed as ‘(.*)’")]
        public void ThenTheRoadStatusSeverityShouldBeDisplayedAsRoadStatus(string key, string text)
        {
            Assert.Equal(RoadStatusValidator.Print.ReportConstants[key], text);
            Assert.Equal(ExpectedExitCode, ExitCode);
            Assert.NotNull(RoadStatusValidator.CorridorStatus.statusSeverity);
            Assert.NotEmpty(RoadStatusValidator.CorridorStatus.statusSeverity);
        }
    }
}
