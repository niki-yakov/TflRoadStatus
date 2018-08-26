using RoadStatus.REST;
using RoadStatus.REST.HttpRestClient;
using RoadStatus.REST.Interfaces;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;
using Xunit;
using Autofac;
using Moq;
using RoadStatus.Common.Tests;

namespace RoadStatus.Behaviour.Tests
{
    [Binding]
    public class HttpNotFoundErrorSteps
    {
        private int ExitCode;

        private readonly IRestClient RestClientMock;
        private readonly IPrint Print;
        private readonly IRoadStatusValidator RoadValidator;

        private readonly Mock<IConfig> ConfigMock;

        public HttpNotFoundErrorSteps()
        {
            ConfigMock = new Mock<IConfig>();
            ConfigMock.Setup(c => c.Url).Returns(() => "http://testurl/HttpStatus404");
            RestClientMock = new RestClient(ConfigMock.Object, new HttpClient(new DelegatingHandlerStub()));

            Print = new Print();
            RoadValidator = new RoadStatusValidator(RestClientMock, Print);
        }

        [Given(@"an invalid road ID (.*) is specified")]
        public void GivenAnInvalidRoadIDAIsSpecified(string road)
        {
            ExitCode = RoadValidator.GetRoadCurrentStatus(road);
        }

        [Then(@"the application should return an informative error")]
        public void ThenTheApplicationShouldReturnAnInformativeError()
        {
            var expectedHttpStatusCode = "404";
            var expectedHttpStatus = "NotFound";

            Assert.Equal(expectedHttpStatusCode, RoadValidator.HttpNotFoundError.httpStatusCode);
            Assert.Equal(expectedHttpStatus, RoadValidator.HttpNotFoundError.httpStatus);
        }

        [Then(@"the application should exit with a non-zero System Error code")]
        public void ThenTheApplicationShouldExitWithANon_ZeroSystemErrorCode()
        {
            var expectedExitCode = 1;
            Assert.Equal(expectedExitCode, ExitCode);
        }

    }
}