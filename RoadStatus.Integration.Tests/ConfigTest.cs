using RoadStatus.REST;
using RoadStatus.REST.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Autofac;

namespace RoadStatus.Integration.Tests
{
    public class ConfigTest
    {
        private readonly IConfig Config;
        public ConfigTest()
        {
            Config = RoadStatusContainer.Container.Resolve<IConfig>();
        }
        [Fact]
        public void When_Config_Instance_Is_Created_Than_Correct_Values_Found()
        {
            var expectedUrl = "https://api.tfl.gov.uk/Road/";
            var expectedAppId = "988cd1a8";
            var expectedAppKey = "267fdb82c1aa3696fef3d6e33f46a36a";

            Assert.NotNull(Config);
            Assert.Equal(expectedUrl, Config.Url);
            Assert.Equal(expectedAppId, Config.AppID);
            Assert.Equal(expectedAppKey, Config.AppKey);
        }
    }
}
