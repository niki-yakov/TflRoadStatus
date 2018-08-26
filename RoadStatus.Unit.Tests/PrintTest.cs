using RoadStatus.REST;
using RoadStatus.REST.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RoadStatus.Unit.Tests
{
    public class PrintTest
    {
        private readonly IPrint Print;

        public PrintTest()
        {
            Print = new Print();
        }

        [Fact]
        public void When_AddHeader_Is_Executed_Returns_AddedText()
        {
            string text = "A2";
            string expectedText = "The status of the A2 is as follows\r\n";

            Print.AddHeader(text);

            var content = Print.Message.ToString();

            Assert.Equal(expectedText, content);
        }

        [Fact]
        public void When_ReportConstant_statusSeverity_Is_Executed_Returns_AddedText()
        {
            string expectedText = "Road Status\r\n";

            string statusSeverity = Print.ReportConstants["statusSeverity"];

            Assert.Equal(expectedText, expectedText);
        }

        [Fact]
        public void When_ReportConstant_statusSeverityDescription_Is_Executed_Returns_AddedText()
        {
            string expectedText = "Road Status Description\r\n";

            string statusSeverityDescription = Print.ReportConstants["statusSeverityDescription"];

            Assert.Equal(expectedText, expectedText);
        }

        [Fact]
        public void When_AddHeaderText_statusSeverity_Is_Executed_Returns_AddedText()
        {
            string key = "statusSeverity";
            string text = "Good";
            string expectedText = "\tRoad Status is Good\r\n";
            Print.AddRoadStatusText(key, text);

            var content = Print.Message.ToString();

            Assert.Equal(expectedText, content);
        }

        [Fact]
        public void When_AddHeaderText_statusSeverityDescription_Is_Executed_Returns_AddedText()
        {
            string key = "statusSeverityDescription";
            string text = "No Exceptional Delays";
            string expectedText = "\tRoad Status Description is No Exceptional Delays\r\n";
            Print.AddRoadStatusText(key, text);

            var content = Print.Message.ToString();

            Assert.Equal(expectedText, content);
        }
    }
}
