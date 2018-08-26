using Moq;
using RoadStatus.Common.Tests;
using RoadStatus.REST;
using RoadStatus.REST.HttpRestClient;
using RoadStatus.REST.Interfaces;
using System.Net.Http;
using System.Text;
using Xunit;

namespace RoadStatus.Unit.Tests
{
    public class RoadStatusValidatorTest
    {
        private string Road;
        private readonly IRestClient RestClientMock;
        private readonly Mock<IPrint> PrintMock;
        private readonly RoadStatusValidator Validator;
        private readonly Mock<IConfig> ConfigMock;
        private int ExpectedExitCode;

        public RoadStatusValidatorTest()
        {
            ConfigMock = new Mock<IConfig>();
            RestClientMock = new RestClient(ConfigMock.Object, new HttpClient(new DelegatingHandlerStub()));

            PrintMock = new Mock<IPrint>();
            PrintMock.Object.Message = new System.Text.StringBuilder();
            Validator = new RoadStatusValidator(RestClientMock, PrintMock.Object);
        }

        [Fact]
        public void When_RoadStatus_Valid_Request_Is_Executed_Returns_RoadStatus()
        {
            Road = "A2";
            ExpectedExitCode = 0;
            ConfigMock.Setup(c => c.Url).Returns(() => "http://testurl/HttpStatus200");

            var returnCode = this.ExecutMethod();

            Assert.Equal(ExpectedExitCode, returnCode);

            PrintMock.Verify(c => c.AddHeader(this.Road), Times.Once);
            PrintMock.Verify(c => c.PringStatus(), Times.Once);
        }

        [Fact]
        public void When_RoadStatus_Invalid_Request_Is_Executed_Returns_NotFound()
        {
            Road = "A223";
            ExpectedExitCode = 1;
            ConfigMock.Setup(c => c.Url).Returns(() => "http://testurl/HttpStatus404");

            var returnCode = this.ExecutMethod();

            Assert.Equal(ExpectedExitCode, returnCode);
            PrintMock.Verify(c => c.AddError(this.Road), Times.Once);
            PrintMock.Verify(c => c.PringStatus(), Times.Once);
        }

        private int ExecutMethod()
        {
            return Validator.GetRoadCurrentStatus(this.Road);
        }
    }
}
