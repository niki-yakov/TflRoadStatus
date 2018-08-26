using System;
using TechTalk.SpecFlow;
using Autofac;
using RoadStatus.REST.Interfaces;
using Xunit;

namespace RoadStatus.e2e.Tests
{
    [Binding]
    public class HttpNotFoundErrorAndExitCodeSteps
    {
        private IRoadStatusValidator RoadStatusValidator;
        private const string Severity = "Good";
        private const string SeverityDescription = "No Exceptional Delays";

        private const int ExpectedExitCode = 1;
        private int ExitCode;

        public string Road { get; private set; }

        public HttpNotFoundErrorAndExitCodeSteps()
        {
            RoadStatusValidator = RoadStatusContainer.Container.Resolve<IRoadStatusValidator>();
        }

        [Given(@"an invalid road ID (.*) is specified")]
        public void GivenAnInvalidRoadIDAIsSpecified(string road)
        {
            ExitCode = RoadStatusValidator.GetRoadCurrentStatus(road);
        }

        [Then(@"the application should return an informative error")]
        public void ThenTheApplicationShouldReturnAnInformativeError()
        {
            var expectedHttpStatusCode = "404";
            var expectedHttpStatus = "NotFound";

            Assert.Equal(expectedHttpStatusCode, RoadStatusValidator.HttpNotFoundError.httpStatusCode);
            Assert.Equal(expectedHttpStatus, RoadStatusValidator.HttpNotFoundError.httpStatus);
        }

        [Then(@"the application should exit with a non-zero System Error code")]
        public void ThenTheApplicationShouldExitWithANon_ZeroSystemErrorCode()
        {
            var expectedExitCode = 1;
            Assert.Equal(expectedExitCode, ExitCode);
        }
    }
}
